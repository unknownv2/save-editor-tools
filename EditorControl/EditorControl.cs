using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using System.Threading;
using Horizon.Functions;
using XContent;
using XboxDataBaseFile;

namespace Horizon.PackageEditors
{
    public partial class EditorControl : Office2007RibbonForm, IDisposable
    {
        private int _configIndex;
        internal FormHandle.FormConfig Meta
        {
            get { return FormHandle.Forms[_configIndex]; }
        }
        protected internal XContentPackage Package
        {
            get { return FormHandle.Forms[_configIndex].Package; }
            set { FormHandle.Forms[_configIndex].Package = value; }
        }
        protected internal EndianIO IO;
        protected internal DataFile GPD;
        protected internal string TitleID;
        protected internal List<string> AlternateTitleIds;
 
        private bool needsBackup = false;
        protected internal bool wasDiamondOnStart, wasSafeModeOnStart;
        private string TitleIDAsGPD { get { return TitleID + ".gpd"; } }
        private static readonly string[]
            InvalidTitleIdMsg = { "Invalid Title ID detected for current editor.", "Invalid Package" },
            InvalidPackageEntry = { "The selected package does not support this editor.", "Invalid Entry Name" },
            InvalidProfile = { "The selected file is not an Xbox 360 profile!", "Not a Profile" },
            InvalidGPD = { "Failed to load GPD from profile!", "GPD Load Error" },
            NotAGamesave = { "The selected file is not a gamesave!", "Not a Gamesave" },
            NotAValidGamesave = { "The selected file is not a valid gamesave for this editor!", "Not a Valid Gamesave" },
            NoGPDInProfile = { "You must play this game before modding it!", "Game Never Played" },
            NoLongerDiamond = { "You are no longer logged in as a Diamond member!", "Not Diamond" },
            FileNotLoaded = { "Open a file first!", "No File Loaded" },
            SavedRehashedResigned = { "Saved, rehashed, and resigned", "Saved" },
            SavedSuccessfully = { "File saved successfully!", "Saved" },
            StatusChanged = { "Your signin status has changed! Please close and reopen this form.", "Status Changed" },
            OperationExecuting = { "An operation is currently being executed!\n\nWait until that is finished to continue.", "Operation Executing" },
            TellDeveloper = { "\nIf you see a developer, the following message will help him resolve the issue:\n\n" };
        internal protected static readonly string[]
            GPDSettingsNotFound = { "The settings for this game do not exist! You must play the game if it was added with the game adder.", "Settings Not Found" };

        internal void initiateForm(int x)
        {
            _configIndex = x;
            Meta.KillLastForm(this);
            _transferringForms = false;
            if (Meta.Meta.UseMDI)
                MdiParent = Horizon.Forms.Main.mainForm;
            AllowDrop = true;
            DragEnter += new DragEventHandler(EditorControl_DragEnter);
            DragDrop += new DragEventHandler(EditorControl_DragDrop);
            wasDiamondOnStart = Server.User.isLogged && Server.User.isDiamond;
            wasSafeModeOnStart = Forms.Main.mainForm.cmdSafeMode.Checked;
            enablePanels(Forms.Main.mainForm.exFatx.Expanded = false);
            Initialize();
            Show();
            if (Meta.Package.IsLoaded)
            {
                revertForm();
                if (Meta.Meta.Type == FormConfig.FormType.GPD_Modder && !readGPD())
                {
                    closeAndUnlinkFatx();
                    UI.messageBox(this, InvalidGPD[0], InvalidGPD[1], MessageBoxIcon.Error);
                }
                else
                {
#if !INT2
                    try
                    {
#endif
                    enablePanels(true);
                    bool entry = DoEntry();
                    if (!entry)
                    {
                        enablePanels(false);
                        Package.CloseIO(true);
                    }
#if !INT2
                    }
                    catch (Exception e)
                    {
                        closeAndUnlinkFatx();
                        UI.messageBox(this, e.Message, "File Error", MessageBoxIcon.Error);
                    }
#endif
                }
            }
        }

        private void closeAndUnlinkFatx()
        {
            Meta.FatxPath = null;
            Package.CloseIO(true);
        }

        internal protected bool readGPD()
        {
            if (OpenStfsFile(TitleIDAsGPD))
            {
                GPD = new DataFile(IO);
                GPD.Read();
                return true;
            }
            return false;
        }

        internal protected void hideShowPackageManagerIcon(bool show)
        {
            cmdOpenPackageManager.Visible = show;
        }

        internal protected void movePackageManagerIcon(eItemAlignment alignment)
        {
            cmdOpenPackageManager.ItemAlignment = alignment;
        }

        internal protected void EditorControl_DragDrop(object sender, DragEventArgs e)
        {
            string fileName = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (fileName.Contains(FatxHandle.dragTempFileFilter.Substring(0, 14)))
                UI.messageBox(this, "Use the mod button to open a package from your device!", "Device Explorer", MessageBoxIcon.Warning);
            else
                openFile(fileName);
        }

        internal protected void EditorControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
                BringToFront();
            }
        }

        public virtual void enablePanels(bool enable)
        {
            foreach (Control control in rbPackageEditor.Controls)
                if (control is RibbonPanel)
                    control.Enabled = enable;
        }

        internal protected XProfile.XProfileAccount Account;
        internal protected void cmdOpen_Click(object sender, EventArgs e) { openFile(String.Empty); }
        private void openFile(string fileName)
        {
            if (WorkerRunning)
                UI.messageBox(this, OperationExecuting[0], OperationExecuting[1], MessageBoxIcon.Warning);
            else if (checkSignInStatus())
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.FileName = fileName;
                if ((fileName.Length != 0 || openFileDialog.ShowDialog() == DialogResult.OK) && openFileDialog.FileName != Meta.FileName)
                {
                    if (Package.IsLoaded)
                        Package.CloseIO(Meta.HasToRebuild = true);
                    Meta.DeviceIndex = -1;
                    Meta.FatxPath = null;
                    if (!Package.CloseIO(!loadPackage(openFileDialog.FileName)))
                    {
                        packageOverride = false;
                        Server.User.doCheckUp();
                    }
                }
            }
        }

        private bool checkSignInStatus()
        {
            if (wasDiamondOnStart && !Server.User.isDiamond)
            {
                UI.messageBox(this, StatusChanged[0], StatusChanged[1], MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private string _backupFilePath;
        private string backupFilePath
        {
            get { return _backupFilePath; }
            set
            {
                if (File.Exists(_backupFilePath))
                    File.Delete(_backupFilePath);
                _backupFilePath = value;
            }
        }

        #if PROD
        private Thread logThread;
        #endif

        private bool DoEntry()
        {
            if (!Meta.IsFatx && Package.Header.Metadata.VolumeType == XContentVolumeType.STFS_Volume
                && !Package.StfsContentPackage.VolumeExtension.ReadOnly)
            {
                File.Copy(Meta.FileName, backupFilePath = Path.GetTempFileName(), true);
                if (!noBackups && backupOnOpen && Horizon.Forms.Main.mainForm.cmdBackups.Checked)
                    File.Copy(backupFilePath, Meta.FileName + ".bak", true);
            }
            if (Meta.HasToRebuild && wasSafeModeOnStart)
                rebuildPackage();
            if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
            {
                string acc = "Account";
                try
                {
                    if (DoesFileExist(acc))
                        Account = new XProfile.XProfileAccount(Package.StfsContentPackage.ExtractFileToArray(acc));
                }
                catch
                {
                    Account = null;
                }
                if (!Meta.IsFatx && Account != null)
                    ProfileManager.addProfileToCache(Package, Account);
            }
            bool goodEntry = needsBackup = Entry();
            #if PROD
            if (goodEntry && Server.User.isDiamond && FormConfig.isDiamondForm(Meta.Meta.ID))
                (logThread = new Thread((ThreadStart)delegate
                {
                    var req = new Server.Request("log");
                    req.addParam("f", Meta.Meta.ID);
                    req.addParam("p", Package.Header.Metadata.Creator.ToString("x"));
                    req.addParam("c", Package.Header.Metadata.ConsoleId.ToHexString());
                    if ((Meta.Meta.Type == FormConfig.FormType.Profile_Modder
                        || Meta.Meta.Type == FormConfig.FormType.GPD_Modder) && Account != null)
                        {
                            req.addParam("g", Account.Info.GamerTag);
                            req.addParam("x", Account.Info.XuidOnline.ToString("X16"));
                        }
                    else
                    {
                        ProfileData dat = ProfileManager.fetchRealProfile(Package.Header.Metadata.Creator);
                        if (dat != null)
                        {
                            req.addParam("g", dat.Gamertag);
                            req.addParam("x", dat.XUID.ToString("X16"));
                        }
                    }
                    if (!req.doRequest())
                        this.Invoke((MethodInvoker)Close);
                })).Start();
            #endif
            return goodEntry;
        }

        private void rebuildPackage()
        {
            string newPackageFile = Path.GetTempFileName();
            XContentPackage oldPackage = new XContentPackage();
            oldPackage.LoadPackage(backupFilePath);
            XContentPackage newPackage = new XContentPackage();
            newPackage.RebuildPackage(oldPackage, newPackageFile);
            oldPackage.CloseIO(newPackage.CloseIO(Package.CloseIO(true)));
            File.Delete(Meta.FileName);
            File.Move(newPackageFile, Meta.FileName);
            Package = new XContentPackage();
            Package.LoadPackage(Meta.FileName);
            Meta.HasToRebuild = false;
        }

        private bool loadPackage(string filePath)
        {
            Package = new XContentPackage();
            try
            {
                if (Package.LoadPackage(filePath) && isValidPackage(true))
                {
                    revertForm();
                    Meta.FileName = filePath;
                    enablePanels(true);
                    bool entry = DoEntry();
                    if (!entry)
                        enablePanels(false);
                    return entry;
                }

                Package.IsLoaded = false;
            }
            catch (Exception e)
            {
                UI.messageBox(this, e.Message, "File Error", MessageBoxIcon.Error);
            }
            Package.CloseIO(true);
            return false;
        }

        internal protected bool loadTitleSetting(XProfileIds Id, EndianType endianType)
        {
            return loadTitleSetting(Id, endianType, true);
        }

        internal protected bool loadTitleSetting(XProfileIds Id, EndianType endianType, bool showErrors)
        {
            try
            {
                IO = new EndianIO(new MemoryStream(GPD.ReadTitleSetting(new DataFileId() { Id = (ulong)Id, Namespace = Namespace.SETTINGS })), endianType);
                IO.Open();
                IO.Stream.Position = 0;
                return true;
            }
            catch
            {
                if (showErrors)
                    UI.messageBox(this, GPDSettingsNotFound[0], GPDSettingsNotFound[1], MessageBoxIcon.Warning);
            }
            return false;
        }

        internal protected void writeTitleSetting(XProfileIds Id, byte[] data)
        {
            GPD.WriteTitleSetting(new DataFileId() { Id = (ulong)Id, Namespace = Namespace.SETTINGS }, data);
        }

        internal protected bool loadAllTitleSettings(EndianType endianType)
        {
            if ((!loadTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, endianType)))
                return false;
            EndianIO io = new EndianIO(new MemoryStream(), endianType, true);
            io.Out.Write(IO.ToArray());
            if ((loadTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC2, endianType, false)))
            {
                io.Out.Write(IO.ToArray());
                if ((loadTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, endianType, false)))
                    io.Out.Write(IO.ToArray());
            }
            IO.Close();
            IO = io;
            IO.Stream.Position = 0;
            return true;
        }

        internal protected void writeAllTitleSettings(byte[] data, uint[] DataSizes)
        {
            EndianIO io = new EndianIO(data, EndianType.BigEndian, true);

            loadTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, EndianType.BigEndian);
            IO.Out.Write(io.In.ReadBytes(DataSizes[0]));
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, IO.ToArray());

            loadTitleSetting(XboxDataBaseFile.XProfileIds.XPROFILE_TITLE_SPECIFIC2, EndianType.BigEndian);
            IO.Out.Write(io.In.ReadBytes(DataSizes[1]));
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC2, IO.ToArray());

            loadTitleSetting(XboxDataBaseFile.XProfileIds.XPROFILE_TITLE_SPECIFIC3, EndianType.BigEndian);
            IO.Out.Write(io.In.ReadBytes(DataSizes[2]));
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, IO.ToArray());

            IO = io;
        }

        internal protected void writeAllTitleSettings(byte[] data)
        {
            EndianIO io = new EndianIO(data, EndianType.BigEndian, true);

            int readLength = (int)(io.Length - io.Position);
            byte[] buffer = new byte[readLength > 1000 ? 1000 : readLength];

            if (buffer.Length == 0)
            {
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, buffer);
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC2, buffer);
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, buffer);
                return;
            }
            io.In.Read(buffer, 0, buffer.Length);
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, buffer);

            if (buffer.Length < 1000)
            {
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC2, new byte[0]);
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, new byte[0]);
                return;
            }
            readLength = (int)(io.Length - io.Position);
            buffer = new byte[readLength > 1000 ? 1000 : readLength];

            if (buffer.Length == 0)
            {
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC2, buffer);
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, buffer);
                return;
            }

            io.In.Read(buffer, 0, buffer.Length);
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC2, buffer);

            if (buffer.Length < 1000)
            {
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, new byte[0]);
                return;
            }

            readLength = (int)(io.Length - io.Position);
            buffer = new byte[readLength > 1000 ? 1000 : readLength];

            if (buffer.Length == 0)
            {
                writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, buffer);
                return;
            }

            io.In.Read(buffer, 0, buffer.Length);
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC3, buffer);
        }

        private bool isValidPackage(bool showMessages)
        {
            if (Meta.Meta.Type == FormConfig.FormType.GPD_Modder)
            {
                if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
                {
                    if (DoesFileExist(TitleIDAsGPD) && OpenStfsFile(TitleIDAsGPD))
                    {
                        GPD = new DataFile(IO);
                        GPD.Read();
                        return true;
                    }

                    if (showMessages)
                        UI.messageBox(this, NoGPDInProfile[0], NoGPDInProfile[1], MessageBoxIcon.Error); 
                }
                else if (showMessages)
                        UI.messageBox(this, InvalidProfile[0], InvalidProfile[1], MessageBoxIcon.Error);
            }
            else if (Meta.Meta.Type == FormConfig.FormType.Profile_Modder)
            {
                if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
                    return true;

                if (showMessages)
                    UI.messageBox(this, InvalidProfile[0], InvalidProfile[1], MessageBoxIcon.Error);
            }
            else
            {
                if (TitleID == null)
                    return true;

                uint titleId = TitleControl.GetProperTitleID(Package);
                if (titleId == uint.Parse(TitleID, System.Globalization.NumberStyles.HexNumber) || (AlternateTitleIds != null && AlternateTitleIds.Contains(titleId.ToString("X"))))
                {
                    if (Package.Header.Metadata.ContentType == ContentTypes.SavedGame)
                    {
                        byte metaIndex = Meta.MetaIndex;
                        if (Meta.IsFatx || TitleControl.ValidSaveGamePackage(Package, titleId, ref metaIndex))
                            return metaIndex == Meta.MetaIndex;
                        UI.messageBox(this, NotAValidGamesave[0], NotAValidGamesave[1], MessageBoxIcon.Error);
                    }
                    else if (showMessages)
                        UI.messageBox(this, NotAGamesave[0], NotAGamesave[1], MessageBoxIcon.Error);
                }
                else if (showMessages)
                    UI.messageBox(this, InvalidTitleIdMsg[0], InvalidTitleIdMsg[1], MessageBoxIcon.Error);
            }
            return false;
        }

        internal protected bool OpenStfsFile(string filePath)
        {
            closeFileStream(false);
            Package.Flush();
            try
            {
                IO = Package.StfsContentPackage.GetEndianIO(filePath);
                IO.Open();
                return true;
            }
            catch { }
            return false;
        }

        internal protected bool OpenStfsFile(int Index)
        {
            if (Index != -1 && Index < Package.StfsContentPackage.DirectoryEntries.Count)
            {
                closeFileStream(false);
                Package.Flush();
                IO = Package.StfsContentPackage.GetEndianIO(Index);
                IO.Open();
                return true;
            }
            return false;
        }

        private bool packageOverride = false;
        internal protected void setPackageOverride(string fileName)
        {
            packageOverride = true;
            Meta.FileName = fileName;
        }

        internal protected bool noBackups = false, backupOnOpen = false;
        internal virtual void cmdSave_Click(object sender, EventArgs e)
        {
            if (WorkerRunning)
                UI.messageBox(this, OperationExecuting[0], OperationExecuting[1], MessageBoxIcon.Warning);
            else if ((Package.IsLoaded || packageOverride) && checkSignInStatus())
            {
                if (Horizon.Forms.Main.mainForm.cmdBackups.Checked && !noBackups && !backupOnOpen
                    && (packageOverride || !Package.StfsContentPackage.VolumeExtension.ReadOnly)
                    && needsBackup && !Meta.IsFatx)
                {
                    File.Copy(backupFilePath, Meta.FileName + ".bak", true);
                    needsBackup = false;
                }
                if (!Meta.IsFatx || FatxHandle.isDeviceWorkerAvailable(Meta.DeviceIndex))
                {
#if !INT2
                    try
                    {
#endif
                        Save();
                        if (!packageOverride)
                        {
                            closeFileStream(!_isClosing);
                            Package.Flush();
                            Package.Save(true);
                            if (Meta.IsFatx)
                                FatxHandle.updateNode(Meta.DeviceIndex, Package, Meta.FatxPath);
                            if (!_isClosing)
                                UI.messageBox(this, SavedRehashedResigned[0] + (Meta.IsFatx ? " to device!" : "!"), SavedRehashedResigned[1], MessageBoxIcon.Information);
                        }
                        else if (!_isClosing)
                        {
                            if (Meta.IsFatx)
                                FatxHandle.updateNode(Meta.DeviceIndex, Package, Meta.FatxPath);
                            UI.messageBox(this, SavedSuccessfully[0], SavedSuccessfully[1], MessageBoxIcon.Information);
                        }
#if !INT2
                    }
                    catch (FatxException ex)
                    {
                        UI.errorBox(this, "An error has occured while writing to your device!" + TellDeveloper[0] + ex.Message);
                    }
                    catch (StfsException ex)
                    {
                        saveErrorRebuild(ex);
                    }
                    catch (IgnoreException)
                    {

                    }
                    catch (Exception ex)
                    {
                        showWriteErrorMessage(ex.Message);
                    }
#endif
                }
            }
            else if (!_isClosing)
                UI.messageBox(this, FileNotLoaded[0], FileNotLoaded[1], MessageBoxIcon.Exclamation);
        }

        private void showWriteErrorMessage(string message)
        {
            UI.errorBox(this, "An error has occured while writing back to the loaded Package!" + TellDeveloper[0] + message);
        }

        internal protected void saveErrorRebuild(Exception ex)
        {
            if (Meta.HasToRebuild && !wasSafeModeOnStart && ex.Message.Substring(0, 4) == "STFS")
            {
                if (UI.messageBox(this, ex.Message + "\n\nAn error has occured while saving! This may be due to an "
                    + "improper Package that was modified by another tool.\n\n"
                    + "Horizon can attempt to rebuild your Package to suppress this error.\n\n"
                    + "Press Yes to continue rebuilding or press No to close this editor and lose all changes."
                    , "Rebuild Package?", MessageBoxIcon.Error, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    enablePanels(false);
                    rebuildPackage();
                    _transferringForms = true;
                    FormConfig.loadForm(_configIndex);
                }
                else
                    Close();
            }
            else
                showWriteErrorMessage(ex.Message);
        }

        internal protected void transferToNewEditor(string newFID)
        {
            _transferringForms = true;
            FormConfig.loadNewEditor(_configIndex, this, newFID);
        }

        private void cmdOpenPackageManager_Click(object sender, EventArgs e)
        {
            if (WorkerRunning)
                UI.messageBox(this, OperationExecuting[0], OperationExecuting[1], MessageBoxIcon.Warning);
            else
                transferToNewEditor(FormID.PackageManager);
        }

        private void closeFileStream(bool openAgain)
        {
            if ((_isClosing || Meta.Meta.Type != FormConfig.FormType.GPD_Modder) && IO != null && IO.Opened)
            {
                IO.Out.Close();
                if (openAgain)
                    IO.Open();
            }
        }

        private bool _isClosing = false;
        internal protected bool IgnoreWorkerOnExit = false;
        internal protected Thread WorkerThread;
        internal bool WorkerRunning
        {
            get
            {
                return WorkerThread != null && WorkerThread.IsAlive;
            }
        }
        internal protected virtual void EditorControl_FormClosing(object s, FormClosingEventArgs e)
        {
            if (!IgnoreWorkerOnExit && WorkerRunning)
            {
                UI.messageBox(this, OperationExecuting[0], OperationExecuting[1], MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else
            {
                _isClosing = true;
                if (backupOnOpen && (!Meta.IsFatx || FatxHandle.Devices[Meta.DeviceIndex].Drive.Mounted))
                    cmdSave_Click(null, null);
                backupFilePath = null;
                if (_transferringForms && Package.IsLoaded)
                    Package.ReloadPackage();
                else if (!_transferringForms)
                {
                    Package.CloseIO(true);
                    Package = null;
                    if (Meta.IsFatx && FormHandle.Forms.Count == 1 && Forms.Main.mainForm.cmdDock.Checked
                        && Forms.Main.mainForm.listFatx.Nodes.Count != 0)
                        Forms.Main.mainForm.FatxPanelExpanded = true;
                    if (FormHandle.Forms.ContainsKey(_configIndex))
                        FormHandle.Forms.Remove(_configIndex);
                }
            }
        }

        internal EditorControl()
        {
            MaximizeBox = EnableGlass = FormHandle.tempRefGlass;
            InitializeComponent();
        }

        internal protected void MaxValues(Control panel)
        {
            MaxValues(panel, new List<Control>());
        }

        internal protected void MaxValues(Control panel, Control excludeList)
        {
            MaxValues(panel, new List<Control>() { excludeList });
        }

        internal protected void MaxValues(Control panel, List<Control> excludeList)
        {
            if (panel != null && panel.Controls != null)
            {
                foreach (Control c in panel.Controls)
                {
                    if (!excludeList.Contains(c))
                    {
                        if (c is NumericUpDown)
                        {
                            NumericUpDown num = (NumericUpDown)c;
                            num.Value = num.Maximum;
                        }
                        else if (c is DevComponents.Editors.IntegerInput)
                        {
                            DevComponents.Editors.IntegerInput num = (DevComponents.Editors.IntegerInput)c;
                            num.Value = num.MaxValue;
                        }
                        else if (c is DevComponents.DotNetBar.Controls.Slider)
                        {
                            DevComponents.DotNetBar.Controls.Slider s = (DevComponents.DotNetBar.Controls.Slider)c;
                            s.Value = s.Maximum;
                        }
                        else
                            MaxValues(c, excludeList);
                    }
                }
            }
        }

        private object Setting(byte index)
        {
            if (!Server.User.isDiamond)
            {
                UI.messageBox(this, NoLongerDiamond[0], NoLongerDiamond[1], MessageBoxIcon.Exclamation);
                this.Close();
                return null;
            }
            try
            {
                return FormSettings.getSetting(Meta.Meta.ID, (byte)((char)Server.Config.getSetting("form_xor") ^ index));
            }
            catch
            {
                this.Close();
                return null;
            }
        }
        internal protected byte[] SettingAsByteArray(byte index) { return (byte[])Setting(index); }
        internal protected uint[] SettingAsUIntArray(byte index) { return (uint[])Setting(index); }
        internal protected string SettingAsString(byte index) { return (string)Setting(index); }
        internal protected bool SettingAsBool(byte index) { return (bool)Setting(index); }
        internal protected long SettingAsLong(byte index) { return (long)Setting(index); }
        internal protected ulong SettingAsULong(byte index) { return (ulong)Setting(index); }
        internal protected int SettingAsInt(byte index) { return (int)Setting(index); }
        internal protected uint SettingAsUInt(byte index) { return (uint)((long)Setting(index)); }

        internal protected bool DoesFileExist(string p) { return Package.StfsContentPackage.GetDirectoryEntryIndex(p) != -1; }
        private bool _transferringForms = false;
        public virtual void Initialize() { }
        public virtual void revertForm() { }
        public virtual bool Entry() { return false; }
        public virtual void Save() { }
    }
}