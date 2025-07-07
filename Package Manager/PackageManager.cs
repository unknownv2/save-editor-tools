using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XContent;
using Horizon.Functions;
using System.IO;
using System.Collections.Specialized;
using Horizon.Properties;
using DevComponents.DotNetBar;
using System.Globalization;
using Horizon.Library.Systems.FATX;
using Horizon.Forms;
using DevComponents.AdvTree;
using System.Threading;

namespace Horizon.PackageEditors.Package_Manager
{
    public partial class PackageManager : EditorControl
    {
        public PackageManager()
        {
            InitializeComponent();
            hideShowPackageManagerIcon(false);
            noBackups = true;
            pbIcon2.Image = pbIcon1.Image = Resources.Console;
        }

        public void readOnly(bool only)
        {
            cmdSave.Visible = cmdInject.Enabled = cmdOverwrite.Enabled
                = cmdRename.Enabled = cmdDelete.Enabled = gpProfile.Enabled
                = replaceToolStripMenuItem.Enabled = !only;
            txtPID.ReadOnly = txtDID.ReadOnly = txtCID.ReadOnly
                = txtDisplayName.ReadOnly = txtTitleName.ReadOnly
                = only;
            rbPackageEditor.Refresh();
        }

        private byte metaIndex = 0;
        public override bool Entry()
        {
            cmdSaveToDevice.Visible = !Meta.IsFatx;
            bool isReadOnly = Package.Header.Metadata.VolumeType == XContentVolumeType.SVOD_Volume || Package.StfsContentPackage.VolumeExtension.ReadOnly;
            readOnly(isReadOnly);
            #if PNET
            if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
            {
                switchAccount.Visible = true;
                var ta = new XProfile.XProfileAccount(Package.StfsContentPackage.ExtractFileToArray("Account"));
                switchAccount.Value = ta.DeveloperAccount;
            }
            else
                switchAccount.Visible = false;
            #endif
            txtDisplayName.Text = Package.Header.Metadata.DisplayName;
            txtTitleName.Text = Package.Header.Metadata.TitleName;
            isBusy = true;
            txtPID.Text = Package.Header.Metadata.Creator.ToString("X16");
            isBusy = false;
            txtDID.Text = Package.Header.Metadata.DeviceId.ToHexString().ToUpper();
            txtCID.Text = Package.Header.Metadata.ConsoleId.ToHexString().ToUpper();
            txtTID.Text = Package.Header.Metadata.ExecutionId.TitleId.ToString("X8");
            cmdModPackage.Text = "Mod Package";
            cmdModPackage.AutoExpandOnClick = false;
            galStfs.SubItems.Clear();
            realTitleId = TitleControl.GetProperTitleID(Package);

            string tempId = realTitleId.ToString("X8");
            if (tempId == FormID.Crysis2Profile)
                tempId = FormID.Crysis2Save;

            metaIndex = FormConfig.getFormMetaIndex(tempId);
            bool goodToMod = false;
            if (Package.Header.Metadata.VolumeType == XContentVolumeType.STFS_Volume && !Package.StfsContentPackage.VolumeExtension.ReadOnly)
            {
                if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
                    goodToMod = true;
                if (metaIndex != 255 && Package.Header.Metadata.ContentType == ContentTypes.SavedGame)
                {
                    if (FormConfig.formList[metaIndex].Type == FormConfig.FormType.Game_Modder)
                    {
                        if (TitleControl.ValidSaveGamePackage(Package, realTitleId, ref metaIndex))
                        {
                            goodToMod = metaIndex != 255;
                        }
                    }
                }
            }
            cmdShare.Enabled = false;
            if (Package.Header.Metadata.ContentType == ContentTypes.SavedGame || Package.Header.Metadata.ContentType == ContentTypes.Profile)
            {
                cmdShare.Enabled = true;
            }
            cmdModPackage.Enabled = goodToMod;
            txtTitleName.ReadOnly = (Package.Header.Metadata.ExecutionId.TitleId == 0 && goodToMod) || isReadOnly;
            if (goodToMod)
            {
                cmdModPackage.Image = Package.Header.Metadata.ContentType == ContentTypes.SavedGame
                    ? FormConfig.formList[metaIndex].Thumbnail : Resources.Profile_Thumb;
                if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
                {
                    cmdModPackage.AutoExpandOnClick = true;
                    List<FormConfig.FormMeta> formList = FormConfig.formList.FindAll(meta =>
                        meta.Type == FormConfig.FormType.Profile_Modder || (meta.Type == FormConfig.FormType.GPD_Modder
                        && Package.StfsContentPackage.GetDirectoryEntryIndex(TitleControl.GetProperTitleIDForGpd(meta.ID) + ".gpd") != -1)
                        || meta.ID == FormID.GamerPictureManager);
                    foreach (FormConfig.FormMeta meta in formList)
                    {
                        ButtonItem bI = new ButtonItem();
                        if (meta.ID == FormID.GamerPictureManager)
                            bI.Text = "Change Gamer Picture";
                        else
                            bI.Text = meta.FullName;
                        bI.Image = meta.Thumbnail;
                        bI.ImagePaddingVertical = 9;
                        bI.ImagePosition = eImagePosition.Top;
                        bI.Shape = new RoundRectangleShapeDescriptor();
                        bI.ColorTable = eButtonColor.OrangeWithBackground;
                        bI.Click += new EventHandler(cmdModPackage_Selected);
                        bI.Tag = meta.ID;
                        galStfs.SubItems.Add(bI);
                    }
                }
            }
            else if (Package.Header.Metadata.ContentType == ContentTypes.ThematicSkin)
            {
                cmdModPackage.Enabled = true;
                cmdModPackage.Image = Resources.Theme_Thumb;
                cmdModPackage.Text = "Create Theme";
            }
            else if (Package.Header.Metadata.ContentType == ContentTypes.GamerPicture)
            {
                cmdModPackage.Enabled = true;
                cmdModPackage.Image = Resources.GamerPictureManager_Thumb;
                cmdModPackage.Text = "Create Gamer Pic Pack";
            }
            else
                cmdModPackage.Image = Resources.QuestionMarkWide;
            loadThumbnails();
            refreshProfile(Package.Header.Metadata.Creator);
            cmdSwitchProfile.Enabled = Package.Header.Metadata.ContentType != ContentTypes.Profile;
            if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
            {
                txtPID.ReadOnly = true;
                Text = (Account == null ? String.Empty : (Account.Info.GamerTag + " - ")) + Package.Header.Metadata.DisplayName;
            }
            else if (Package.Header.Metadata.TitleName.Length != 0 && Package.Header.Metadata.DisplayName.Length != 0)
            {
                if (Package.Header.Metadata.TitleName == Package.Header.Metadata.DisplayName)
                    Text = Package.Header.Metadata.DisplayName;
                else
                    Text = Package.Header.Metadata.TitleName + " - " + Package.Header.Metadata.DisplayName;
            }
            else if (Package.Header.Metadata.TitleName.Length != 0)
                Text = Package.Header.Metadata.TitleName;
            else if (Package.Header.Metadata.DisplayName.Length != 0)
                Text = Package.Header.Metadata.DisplayName;
            else
                Text = "Package Manager";
            if (cmdViewContents.Enabled = tabContents.Visible = Package.Header.Metadata.VolumeType == XContentVolumeType.STFS_Volume)
            {
                treeFolders.Nodes.Clear();
                listFiles.Items.Clear();
                TreeNode rootNode = new TreeNode("Root");
                rootNode.Tag = (ushort)0xffff;
                for (ushort x = new ushort(); x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                    if (Package.StfsContentPackage.DirectoryEntries[x].IsDirectory
                        && Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex == 0xffff)
                        recursiveNode(ref rootNode, x);
                treeFolders.Nodes.Add(rootNode);
                treeFolders.ExpandAll();
                treeFolders.SelectedNode = treeFolders.Nodes[0];
                tabContents.Visible = true;
            }
            rbPackageEditor.Refresh();
            return true;
        }

        internal void refreshProfile()
        {
            if (panelMain.Enabled)
            {
                cmdSwitchProfile.Expanded = false;
                txtPID_TextChanged(null, null);
            }
        }

        private void refreshProfile(ulong profileId)
        {
            if (profileId == 0)
            {
                pbGamerpic.Image = Resources.QuestionMark;
                lblGamertag.Text = "Public Profile";
            }
            else
            {
                ProfileData profileCache = ProfileManager.fetchProfile(profileId);
                if (profileCache == null)
                {
                    pbGamerpic.Image = Resources.QuestionMark;
                    lblGamertag.Text = "Unknown Profile";
                }
                else
                {
                    pbGamerpic.Image = Image.FromStream(new MemoryStream(profileCache.Gamerpicture));
                    lblGamertag.Text = profileCache.Gamertag;
                }
            }
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listFiles.Items.Clear();
            ushort i = (ushort)e.Node.Tag;
            for (int x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                if (!Package.StfsContentPackage.DirectoryEntries[x].IsDirectory
                    && Package.StfsContentPackage.DirectoryEntries[x].FileName.Length != 0
                    && Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex == i)
                    addFileToList(x);
        }

        private void recursiveNode(ref TreeNode rootNode, ushort nI)
        {
            if (Package.StfsContentPackage.DirectoryEntries[nI].IsEntryBound)
            {
                TreeNode newNode = new TreeNode(Package.StfsContentPackage.DirectoryEntries[nI].FileName);
                newNode.Tag = nI;
                for (ushort x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                    if (Package.StfsContentPackage.DirectoryEntries[x].IsDirectory
                        && Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex == nI)
                        recursiveNode(ref newNode, x);
                rootNode.Nodes.Add(newNode);
            }
        }

        public override void Save()
        {
            ulong newProfileId;
            byte[] newDeviceId, newConsoleId;
            try
            {
                newProfileId = txtPID.TextLength == 0 ? 0 : ulong.Parse(txtPID.Text, NumberStyles.HexNumber);
                newDeviceId = txtDID.TextLength == 0 ? new byte[20] : Global.hexStringToArray(txtDID.Text.ToLower().PadLeft(txtDID.MaxLength, '0'));
                newConsoleId = txtCID.TextLength == 0 ? new byte[5] : Global.hexStringToArray(txtCID.Text.ToLower().PadLeft(txtCID.MaxLength, '0'));
            }
            catch
            {
                UI.messageBox("One or more of the fields contains illegal characters!\nSome fields only accept hexidecimal characters (0-9, A-F)", "Illegal Characters", MessageBoxIcon.Error);
                return;
            }
            if (Meta.IsFatx && Package.Header.Metadata.Creator != newProfileId)
            {
                string fileName = Meta.FatxPath.Substring(Meta.FatxPath.LastIndexOf('\\') + 1);
                string profileId = newProfileId.ToString("X16");
                string newPath = FatxHandle.buildDirectoryPath(newProfileId, realTitleId, Package.Header.Metadata.ContentType);

                var device = FatxHandle.Devices[Meta.DeviceIndex];

                if (device.IsFat32)
                {
                    var newFilename = newPath + "\\" + fileName;
                    if (File.Exists(device.Fat32Drive.Name + newFilename))
                    {
                        if (UI.messageBox(
                            "A package with the same name already exists under this profile on your device!\n\nDo you want to overwrite it?",
                            "Package Exists", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel,
                            MessageBoxDefaultButton.Button3) != DialogResult.Yes)
                            throw new IgnoreException();

                        Node devNode = FatxHandle.getDeviceNodeFromIndex(Meta.DeviceIndex);
                        if (devNode != null)
                        {
                            Node pNode = FatxHandle.getPackageNodeFromFat32Filename(devNode, device.Fat32Drive.Name + Meta.FatxPath);
                            if (pNode != null)
                                pNode.Remove();
                        }
                        File.Delete(device.Fat32Drive.Name + newFilename);
                    }

                    Package.IO.Close();
                    var newFile = device.Fat32Drive.Name + newFilename;
                    Directory.CreateDirectory(Path.GetDirectoryName(newFile));
                    File.Move(device.Fat32Drive.Name + Meta.FatxPath, newFile);
                    Package.IO = new EndianIO(newFile, EndianType.BigEndian, true);
                    Package.StfsContentPackage.IO = Package.IO;
                    Meta.FatxPath = newFilename;
                }
                else
                {
                    List<FatxDirectoryEntry> entries = FatxHandle.Devices[Meta.DeviceIndex].Handle.GetNestedDirectoryEntries(newPath);
                    FatxDirectoryEntry entry = null;
                    if (entries != null)
                        entry = FatxHandle.getFileEntry(fileName, entries);
                    bool packageExists = (entries != null && entry != null);
                    if (!packageExists || UI.messageBox("A package with the same name already exists under this profile on your device!\n\nDo you want to overwrite it?",
                        "Package Exists", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                    {
                        newPath += @"\" + fileName;
                        if (packageExists)
                        {
                            Node devNode = FatxHandle.getDeviceNodeFromIndex(Meta.DeviceIndex);
                            if (devNode != null)
                            {
                                Node pNode = FatxHandle.getPackageNodeFromStartingCluster(devNode, entry.FirstClusterNumber);
                                if (pNode != null)
                                    pNode.Remove();
                            }
                            FatxHandle.Devices[Meta.DeviceIndex].Handle.DeleteFile(newPath);
                        }
                        FatxHandle.Devices[Meta.DeviceIndex].Handle.MoveFile(Meta.FatxPath, newPath);
                        Meta.FatxPath = newPath;
                    }
                    else
                        throw new IgnoreException();
                }
            }
            XContentHeader Header = Package.Header;
            Header.Metadata.Creator = newProfileId;
            Header.Metadata.DeviceId = newDeviceId;
            Header.Metadata.ConsoleId = newConsoleId;
            Header.Metadata.DisplayName = txtDisplayName.Text;
            Header.Metadata.TitleName = txtTitleName.Text;
            Package.Header = Header;
            #if PNET
                if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
                {
                    XProfile.XProfileAccount ta = new XProfile.XProfileAccount(Package.StfsContentPackage.ExtractFileToArray("Account"));
                    ta.DeveloperAccount = switchAccount.Value;
                    Package.StfsContentPackage.InjectFileFromArray("Account", ta.Save());
                }
            #endif
            Package.StfsContentPackage.Rehash();
        }

        private void addFileToList(int x)
        {
            if (Package.StfsContentPackage.DirectoryEntries[x].IsEntryBound)
            {
                ListViewItem item = new ListViewItem(Package.StfsContentPackage.DirectoryEntries[x].FileName);
                if (Package.StfsContentPackage.DirectoryEntries[x].FirstBlockNumber == 0)
                    item.SubItems.Add("N/A");
                else
                    item.SubItems.Add("0x" + (Package.StfsContentPackage.VolumeExtension.BackingFileOffset
                        + StfsDevice.GetBlockOffset(Package.StfsContentPackage.StfsComputeBackingDataBlockNumber(
                        Package.StfsContentPackage.DirectoryEntries[x].FirstBlockNumber), 0)).ToString("X"));
                item.SubItems.Add(Global.getFormatFromBytes(Package.StfsContentPackage.DirectoryEntries[x].FileBounds.Filesize));
                item.Tag = x;
                listFiles.Items.Add(item);
            }
        }

        private void loadThumbnails()
        {
            pbIcon1.ContextMenuStrip = null;
            if (Package.Header.Metadata.ThumbnailSize == 0)
                pbIcon1.Image = Resources.Console;
            else
                try
                {
                    pbIcon1.Image = Image.FromStream(new MemoryStream(Package.Header.Metadata.Thumbnail));
                    pbIcon1.ContextMenuStrip = menuIcon1;
                }
                catch
                {
                    pbIcon1.Image = Resources.Console;
                }
            try
            {
                if (Package.Header.Metadata.ContentType == ContentTypes.Profile && DoesFileExist("pp_64.png"))
                    pbIcon2.Image = Image.FromStream(Package.StfsContentPackage.GetFileStream("pp_64.png"));
                else if (Package.Header.Metadata.TitleThumbnailSize == 0)
                    pbIcon2.Image = Resources.Console;
                else
                    pbIcon2.Image = Image.FromStream(new MemoryStream(Package.Header.Metadata.TitleThumbnail));
            }
            catch
            {
                pbIcon2.Image = Resources.Console;
            }
        }

        private void enableForm(bool enable)
        {
            tabMain.Enabled
                = cmdSave.Enabled
                = cmdOpen.Enabled
                = cmdExtractSelected.Enabled
                = cmdExtractAll.Enabled
                = treeFolders.Enabled
                = listFiles.Enabled
                = enable;
            if (!Meta.IsFatx)
                cmdSaveToDevice.Visible = enable;
            txtSearch.ReadOnly = !enable;
            rbPackageEditor.Refresh();
        }

        private void cmdExtractAll_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            if (Package.StfsContentPackage.DirectoryEntries.Count > 0 && fbd.ShowDialog() == DialogResult.OK)
            {
                List<int> files = new List<int>();
                for (ushort x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                    if (Package.StfsContentPackage.DirectoryEntries[x].IsEntryBound
                        && !Package.StfsContentPackage.DirectoryEntries[x].IsDirectory)
                        files.Add(x);
                extractFiles(files, fbd.SelectedPath, true);
                //if (Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex == 0xffff)
                //recursiveExtract(x, 0xffff, fbd.SelectedPath, String.Empty);
                //extractedMessage();
            }
        }

        private Stream fs;
        private void extractFiles(List<int> files, string localPath, bool makeDirs)
        {
            WorkerThread = new Thread((ThreadStart)delegate
                {
                    bool errorThrown = true;
                    List<string> filePaths = new List<string>();
                    try
                    {
                        int totalLength = 0;
                        foreach (int file in files)
                        {
                            totalLength += (int)Package.StfsContentPackage.DirectoryEntries[file].FileBounds.Filesize;
                            filePaths.Add(getFilePath(file));
                        }
                        Main.mainForm.Invoke((MethodInvoker)delegate
                        {
                            enableForm(false);
                            progress.Maximum = totalLength;
                            progress.Value = 0;
                        });
                        foreach (string fileName in filePaths)
                        {
                            Stream io = Package.StfsContentPackage.GetFileStream(fileName);
                            string newFileName = makeDirs ? fileName : fileName.Substring(fileName.LastIndexOf('\\') + 1);
                            string newPath = String.Format(@"{0}\{1}", localPath, newFileName);
                            if (makeDirs)
                                Directory.CreateDirectory(newPath.Substring(0, newPath.LastIndexOf('\\')));
                            if (files.Count == 1 && overrideFileName != null)
                            {
                                newPath = overrideFileName;
                                overrideFileName = null;
                            }
                            fs = new FileStream(newPath, FileMode.Create, FileAccess.Write);
                            byte[] buffer = new byte[5242880];
                            int numRead;
                            while ((numRead = io.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, numRead);
                                if (cancelExtract)
                                    break;
                                Main.mainForm.Invoke((MethodInvoker)delegate
                                {
                                    progress.Value += numRead;
                                });
                            }
                            fs.Close();
                            io.Close();
                            if (cancelExtract)
                                break;
                        }
                        errorThrown = false;
                    }
                    catch (FileNotFoundException)
                    {
                        if (fs != null)
                            fs.Close();
                        return;
                    }
                    catch (Exception ex)
                    {
                        UI.errorBox("An error has occured while extracting from the package!\n\nDetails:\n" + ex.Message);
                    }
                    if (!cancelExtract)
                    {
                        Main.mainForm.Invoke((MethodInvoker)delegate
                        {
                            if (!errorThrown)
                                UI.messageBox(String.Format("File{0} extracted successfully!",
                                    filePaths.Count == 1 ? String.Empty : "s"), "Extracted", MessageBoxIcon.Information);
                            progress.Value = 0;
                            enableForm(true);
                        });
                    }
                    else
                    {
                        IgnoreWorkerOnExit = true;
                        Main.mainForm.Invoke((MethodInvoker)delegate { this.Close(); });
                    }
                });
            WorkerThread.Start();
        }

        private bool cancelExtract = false;
        protected internal override void EditorControl_FormClosing(object s, FormClosingEventArgs e)
        {
            if (!IgnoreWorkerOnExit && WorkerRunning)
            {
                DialogResult res = UI.messageBox("Cancel file extraction?", "Operation Running",
                    MessageBoxIcon.Warning, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3);
                if (res == DialogResult.Yes)
                    cancelExtract = true;
                e.Cancel = true;
            }
            else
                base.EditorControl_FormClosing(s, e);
        }

        /*private void recursiveExtract(ushort eI, ushort dirIndex, string basePath, string curPath)
        {
            if (Package.StfsContentPackage.DirectoryEntries[eI].IsEntryBound)
            {
                curPath += @"\" + Package.StfsContentPackage.DirectoryEntries[eI].FileName;
                if (Package.StfsContentPackage.DirectoryEntries[eI].IsDirectory)
                {
                    Directory.CreateDirectory(basePath + curPath);
                    for (ushort x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                        if (Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex == eI)
                            recursiveExtract(x, eI, basePath, curPath);
                }
                else
                {
                    Application.DoEvents();
                    Package.StfsContentPackage.GetFileStream(curPath.Substring(1)).Save(basePath + curPath);
                }
            }
        }*/

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listFiles.Items.Clear();
            for (int x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                if (!Package.StfsContentPackage.DirectoryEntries[x].IsDirectory
                    && Package.StfsContentPackage.DirectoryEntries[x].FileName.Length != 0
                    && Package.StfsContentPackage.DirectoryEntries[x].FileName.ToLower().Contains(txtSearch.Text.ToLower()))
                    addFileToList(x);
        }

        private string getFilePath(int x)
        {
            if (Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex == 0xffff)
                return Package.StfsContentPackage.DirectoryEntries[x].FileName;
            return getFilePath(String.Empty, Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex).Substring(1)
                + @"\" + Package.StfsContentPackage.DirectoryEntries[x].FileName;
        }

        private string getDirectoryPath(ushort x)
        {
            if (x == 0xffff)
                return String.Empty;
            return getFilePath(String.Empty, x).Substring(1) + @"\";
        }

        private string getFilePath(string filePath, ushort parentDir)
        {
            for (int x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                if (x == parentDir)
                    filePath = getFilePath(filePath, Package.StfsContentPackage.DirectoryEntries[x].DirectoryIndex)
                        + @"\" + Package.StfsContentPackage.DirectoryEntries[x].FileName;
            return filePath;
        }

        private string overrideFileName = null;
        private void cmdExtract_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 1)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Extract File from Package";
                sfd.FileName = listFiles.SelectedItems[0].SubItems[0].Text;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    List<int> file = new List<int>();
                    file.Add((int)listFiles.SelectedItems[0].Tag);
                    overrideFileName = sfd.FileName;
                    extractFiles(file, sfd.FileName.Substring(0, sfd.FileName.LastIndexOf('\\') + 1), false);
                }
            }
            else if (listFiles.SelectedItems.Count > 1)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowNewFolderButton = false;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    List<int> files = new List<int>();
                    for (int x = 0; x < listFiles.SelectedItems.Count; x++)
                        files.Add((int)listFiles.SelectedItems[x].Tag);
                    extractFiles(files, fbd.SelectedPath, true);
                }
            }
        }

        private void cmdOverwrite_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Overwrite File in Package";
                ofd.FileName = listFiles.SelectedItems[0].SubItems[0].Text;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Package.StfsContentPackage.InjectFileFromArray(getFilePath((int)listFiles.SelectedItems[0].Tag), File.ReadAllBytes(ofd.FileName));
                        UI.messageBox("File replaced successfully!", "Replaced", MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        UI.messageBox(ex.Message, "Error", MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 1
                && UI.messageBox("Are you sure you want to delete this file?", "Delete Entry", MessageBoxIcon.Question,
                MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                Package.StfsContentPackage.DeleteFile(getFilePath((int)listFiles.SelectedItems[0].Tag));
                listFiles.SelectedItems[0].Remove();
            }
        }

        private void cmdInject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Package.StfsContentPackage.CreateFileFromArray(getDirectoryPath((ushort)treeFolders.SelectedNode.Tag) + ofd.SafeFileName, File.ReadAllBytes(ofd.FileName));
                }
                catch (Exception ex)
                {
                    UI.errorBox(ex.Message);
                }
            }
        }

        private void cmdRename_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 1 && (Package.Header.Metadata.ContentType != ContentTypes.Profile
                || UI.messageBox("Renaming files in a profile can result in corruption!\n\nContinue anyway?",
                "Profile Warning", MessageBoxIcon.Warning, MessageBoxButtons.YesNoCancel,
                MessageBoxDefaultButton.Button3) == DialogResult.Yes))
            {
                listFiles.LabelEdit = true;
                listFiles.SelectedItems[0].BeginEdit();
            }
        }

        private void cmdViewContents_Click(object sender, EventArgs e)
        {
            tabContents.Select();
        }

        private uint realTitleId;
        private void cmdModPackage_Click(object sender, EventArgs e)
        {
            if (Package.Header.Metadata.ContentType == ContentTypes.ThematicSkin)
                transferToNewEditor(FormID.ThemeCreator);
            else if (Package.Header.Metadata.ContentType == ContentTypes.GamerPicture)
                transferToNewEditor(FormID.GamerPictureManager);
            else if (metaIndex != 255)
                transferToNewEditor(FormConfig.formList[metaIndex].ID);
        }

        private void cmdModPackage_Selected(object sender, EventArgs e)
        {
            transferToNewEditor((string)((ButtonItem)sender).Tag);
        }

        private void listFiles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            listFiles.LabelEdit = false;
            if (e.Label != lastLabel)
            {
                try
                {
                    int dirEntryIndex = (int)listFiles.SelectedItems[0].Tag;
                    Package.StfsContentPackage.RenameFile(getFilePath(dirEntryIndex), e.Label);
                    Package.StfsContentPackage.DirectoryEntries[dirEntryIndex].FileName = e.Label;
                    Package.StfsContentPackage.DirectoryEntries[dirEntryIndex].FileNameLength = (byte)e.Label.Length;
                }
                catch
                {
                    UI.messageBox("The new filename that you specified is invalid!", "Rename Error", MessageBoxIcon.Error);
                    e.CancelEdit = true;
                }
            }
        }

        private string lastLabel;
        private void listFiles_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            lastLabel = e.Label;
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = pngFilter;
            sfd.FileName = "Thumbnail";
            if (sfd.ShowDialog() == DialogResult.OK)
                pbIcon1.Image.Save(sfd.FileName);
        }

        private string pngFilter = "PNG Images|*.png";
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = pngFilter;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (Package.Header.Metadata.SetThumbnail(File.ReadAllBytes(ofd.FileName)))
                    loadThumbnails();
                else
                    UI.messageBox("The image you selected is too big to inject!", "Image File Size", MessageBoxIcon.Error);
            }
        }

        private void cmdSaveToDevice_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            cmdSaveToDevice.SubItems.Clear();

            var fileDrive = Package.PackageFilePath[0].ToString().ToLower();
            var exclude = -1;

            for (var x = 0; x < FatxHandle.Devices.Count; x++)
            {
                if (FatxHandle.Devices[x].IsFat32 &&
                    FatxHandle.Devices[x].Fat32Drive.Name[0].ToString().ToLower() == fileDrive)
                {
                    exclude = x;
                    break;
                }
            }

            ButtonItem[] deviceButtons = FatxHandle.createDeviceButtonItems(exclude);
            for (int x = 0; x < deviceButtons.Length; x++)
                deviceButtons[x].Click += new EventHandler(packageManagerDevice_Click);
            cmdSaveToDevice.SubItems.AddRange(deviceButtons);
        }

        private void packageManagerDevice_Click(object sender, EventArgs e)
        {
            int x = (int)((ButtonItem)sender).Tag;
            string fileName = Package.PackageFilePath.Substring(Package.PackageFilePath.LastIndexOf('\\') + 1);
            if (Library.Systems.FATX.FatxDevice.FatxIsValidFatFileName(fileName))
            {
                if (FatxHandle.isDeviceWorkerAvailable(x))
                {
                    DialogResult res = DialogResult.Yes;
                    if (Package.Header.Metadata.VolumeType == XContentVolumeType.STFS_Volume && !Package.StfsContentPackage.VolumeExtension.ReadOnly
                        && (res = UI.messageBox("Save changes before transferring?", "Save Changes?", MessageBoxIcon.Question,
                        MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3)) == DialogResult.Yes)
                    {
                        Save();
                        Package.Flush();
                        Package.Save(true);
                    }
                    if (res == DialogResult.Yes || res == DialogResult.No)
                    {
                        FatxHandle.DeviceOperationInjectionParameters i = new FatxHandle.DeviceOperationInjectionParameters()
                        {
                            ToDeviceIndex = x,
                            Sender = this
                        };
                        i.addPackage(Package, fileName);
                        FatxHandle.injectPackages(i);
                    }
                }
            }
            else
                UI.errorBox("This package cannot be injected because its file name contains invalid characters!");
        }

        private void cmdManageProfiles_Click(object sender, EventArgs e)
        {
            if (ProfileManager.activeWindow == null)
                new ProfileManager().Show();
            else
                ProfileManager.activeWindow.BringToFront();
        }

        private bool isBusy = false;
        private void txtPID_TextChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (txtPID.Text.Length == 0)
                refreshProfile(0);
            else
            {
                ulong newProfileId;
                if (ulong.TryParse(txtPID.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out newProfileId))
                    refreshProfile(newProfileId);
                else
                {
                    pbGamerpic.Image = Resources.QuestionMark;
                    lblGamertag.Text = "Unknown Profile";
                }
            }
        }

        private void cmdSwitchProfile_PopupOpen(object sender, EventArgs e)
        {
            List<ButtonItem> profileButtons = ProfileManager.createFavoriteButtons();
            while (cmdSwitchProfile.SubItems.Count > 1)
                cmdSwitchProfile.SubItems.RemoveAt(1);
            for (int x = 0; x < profileButtons.Count; x++)
            {
                ulong tempID;
                if (ulong.TryParse(txtPID.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out tempID)
                    && (ulong)profileButtons[x].Tag == tempID)
                    profileButtons.RemoveAt(x--);
                else
                    profileButtons[x].Click += new EventHandler(profileButton_Click);
            }
            cmdSwitchProfile.SubItems.AddRange(profileButtons.ToArray());
            cmdNoProfiles.Visible = profileButtons.Count == 0;
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            ButtonItem profile = (ButtonItem)sender;
            if (profile.Tag != null)
                txtPID.Text = ((ulong)profile.Tag).ToString("X16");
        }
    }
}
