using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.AdvTree;
using System.IO;
using Forza4;

namespace Horizon.PackageEditors.Forza_4
{
    public partial class Forza4Livery : EditorControl
    {
        //public static readonly string FID = "4D53091U";

        private byte[] Creator;
        private Forza4KeyMarshal KeyMarshal;
        List<List<string>> LiveryFolders;
        private Forza4CarSetup CarSetups;
        private Forza4LiveryCatalog LivCatalog;
        private Forza4LiveryGroup LivGroups;
        private Forza4LiveryDatabase Database;
        private int FileIndex, FolderIndex;

        private ForzaMotorsport.ForzaFileTypes LoadedLiveryType;
        public Forza4Livery()
        {
            InitializeComponent();
            InitializeControls();
            TitleID = FormID.Forza4Profile;
        }
        private void InitializeControls()
        {
            this.cmbLiveryType.Items.AddRange(new string[] 
            {
                "CarSetup",
                "LivGroups",
                "LivCatalog"
            });
        }
        public override bool Entry()
        {
            this.LoadForzaLiveryKeys();

            // Keep a copy of the Profile ID for decryption/encryption
            this.Creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(this.Package.Header.Metadata.Creator));

            // Populate the list of livery files
            /*
            int LiveryCount = 0;
            uint Search = 0x00;

            
            for (var x = 0; x < this.Package.StfsContentPackage.DirectoryEntries.Count; x++)
            {
                var DirectoryEntry = this.Package.StfsContentPackage.DirectoryEntries[x];
                if (DirectoryEntry.IsDirectory && DirectoryEntry.DirectoryIndex == 0xFFFF && DirectoryEntry.IsEntryBound)
                {
                    var FolderFcb = this.Package.StfsContentPackage.GetFileStream(DirectoryEntry.FileName).Fcb;
                    Search = 0x00;

                    while(this.Package.StfsContentPackage.StfsFindNextDirectoryEntry(FolderFcb, Search, null, ref DirectoryEntry) == 0)
                    {
                        LiveryFolders.Add(new List<string>());
                        LiveryFolders[LiveryCount].Add(FolderFcb.FileName);
                        LiveryFolders[LiveryCount].Add(DirectoryEntry.FileName);

                        XContent.StfsFcb Fcb = FolderFcb;
                        var LiveryDirectoryEntry = DirectoryEntry;
                        bool Continue = DirectoryEntry.IsDirectory;
                        if (Continue)
                        {
                            Fcb = this.Package.StfsContentPackage.GetFileStream(FolderFcb.FileName + "\\" + LiveryFolders[LiveryCount][1]).Fcb;

                            if (this.Package.StfsContentPackage.StfsFindNextDirectoryEntry(Fcb, Search, null, ref LiveryDirectoryEntry) == 0)
                            {
                                LiveryFolders[LiveryCount].Add(this.Package.StfsContentPackage.StfsFindNextDirectoryName(Fcb, 0x00));
                            }
                        }

                        var FullName = new StringBuilder();
                        for (var i = 0; i < LiveryFolders[LiveryCount].Count; i++)
                        {
                            FullName.Append(LiveryFolders[LiveryCount][i] + (i != (LiveryFolders[LiveryCount].Count - 1) ? "\\" : string.Empty));
                        }

                        LiveryFolders[LiveryCount].Add(FullName.ToString());
                        Search = LiveryDirectoryEntry.DirectoryEntryByteOffset + 0x40;
                        uint OwnerSearch = Search;

                        while (this.Package.StfsContentPackage.StfsFindNextDirectoryEntry(Fcb, OwnerSearch, null, ref DirectoryEntry) == 0)
                        {
                            string Filename = DirectoryEntry.FileName;
                            if (Filename.Contains(".owner"))
                            {
                                LiveryFolders[LiveryCount++].Add(Filename.Remove(Filename.IndexOf(".owner")));
                                break;
                            }
                            OwnerSearch = DirectoryEntry.DirectoryEntryByteOffset + 0x40;
                        }
                        if(this.LiveryFolders.Count != LiveryCount) 
                            LiveryCount++;
                        if (!Continue) break;
                    }
                }
            }
            */

            try
            {
                Database = new Forza4LiveryDatabase(Package.StfsContentPackage.GetEndianIO("DatabaseReservedName*"), KeyMarshal, Creator);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public override void Save()
        {
            this.Database.Save(true);
        }
        private void LoadForzaLiveryKeys()
        {
            if (KeyMarshal == null) // so we dont re-initialize the key marshal when a new file is opened in an old form
            {
                KeyMarshal = new Forza4KeyMarshal();

                // CarSetup - Tunes Keys
                this.KeyMarshal.AddKey("CarSetupAES", SettingAsByteArray(155));
                this.KeyMarshal.AddKey("CarSetupSHA", SettingAsByteArray(187));
                this.KeyMarshal.AddKey("LiveryCatalogAES", SettingAsByteArray(111));
                this.KeyMarshal.AddKey("LiveryCatalogSHA", SettingAsByteArray(26));
                this.KeyMarshal.AddKey("LiveryGroupAES", SettingAsByteArray(125));
                this.KeyMarshal.AddKey("LiveryGroupSHA", SettingAsByteArray(73));
                this.KeyMarshal.AddKey("DatabaseAES", SettingAsByteArray(212));
                this.KeyMarshal.AddKey("DatabaseSHA", SettingAsByteArray(147));
            }
        }
        private bool LoadLiveries()
        {
            var Liveries = Database.GetFilenames(LoadedLiveryType);
            if (Liveries.Count <= 0)
            {
                return false;
            }
            var Authors = Database.GetOwners(LoadedLiveryType);
            var SaveIds = Database.GetSaveIds();
            //var Locks = Database.GetLocks();
            LiveryFolders = new List<List<string>>();
            for (int x = 0; x < Liveries.Count; x++)
            {
                List<string> livery = new List<string>();
                livery.Add(Liveries[x]);
                livery.Add(Authors[x]);
                livery.Add(SaveIds[x]);
                //livery.Add(Locks[x]);

                LiveryFolders.Add(livery);

                //LiveryFolders[x][0].Substring(LiveryFolders[x][0].LastIndexOf('\\') + 1)
                this.addSetting(Liveries[x], LiveryFolders[x][1].Replace(":", string.Empty), this.advTree1);
            }
            return true;
        }

        private void AttemptToFindLiveryType()
        {
            try
            {
                FileIndex = 3;
                new Forza4CarSetup(this.Package.StfsContentPackage.GetEndianIO(this.LiveryFolders[0][FileIndex]), KeyMarshal, this.Creator);
                LoadedLiveryType = ForzaMotorsport.ForzaFileTypes.CarSetups;
                cmbLiveryType.SelectedIndex = 0;
                this.cmbLiveryType.Enabled = false;
                return;
            }
            catch {}
            try
            {
                FileIndex = 2;
                new Forza4LiveryGroup(this.Package.StfsContentPackage.GetEndianIO(this.LiveryFolders[0][FileIndex]), KeyMarshal, this.Creator);
                LoadedLiveryType = ForzaMotorsport.ForzaFileTypes.Vinyls;
                cmbLiveryType.SelectedIndex = 1;
                this.cmbLiveryType.Enabled = false;

                return;
            }
            catch {}
            try
            {
                FileIndex = 3;
                LivCatalog = new Forza4LiveryCatalog(this.Package.StfsContentPackage.GetEndianIO(this.LiveryFolders[0][FileIndex]), KeyMarshal, this.Creator);
                LoadedLiveryType = ForzaMotorsport.ForzaFileTypes.Liveries;
                cmbLiveryType.SelectedIndex = 2;
                this.cmbLiveryType.Enabled = false;
                return;
            }
            catch  {}
        }

        private int FindLiveryIndex(string Name)
        {
            for (int x = 0; x < this.LiveryFolders.Count; x++)
            {
                if (this.LiveryFolders[x][0] == Name)
                    return x;
            }
            return -1;
        }
        private void CombBox_LiveryTypeChanged(object sender, EventArgs e)
        {
            switch ((sender as DevComponents.DotNetBar.Controls.ComboBoxEx).Text)
            {
                case "CarSetup":
                    LoadedLiveryType = ForzaMotorsport.ForzaFileTypes.CarSetups;
                    break;
                case "LivGroups":
                    LoadedLiveryType = ForzaMotorsport.ForzaFileTypes.Vinyls;
                    break;
                case "LivCatalog":
                    LoadedLiveryType = ForzaMotorsport.ForzaFileTypes.Liveries;
                    break;
            }

            if (LoadLiveries())
            {
                // Disable future livery type modification
                this.cmbLiveryType.Enabled = false;

                // Enable livery controls
                this.btnLiveryIsUnlocked.Enabled = true;
                this.btnExtractClick.Enabled = true;
                this.btnInjectClick.Enabled = true;
                this.btnSaveLivery.Enabled = true;
                this.btnUnlockAllLivs.Enabled = true;
            }
            else
            {
                Horizon.Functions.UI.messageBox("Failed to load any liveries. Try a different type.");
            }
        }

        private void BtnClick_ExtractLivery(object sender, EventArgs e)
        {
            if (this.cmbLiveryType.Text == string.Empty && this.advTree1.SelectedNode == null)
            {
                Horizon.Functions.UI.messageBox("Please select a file and its Livery-type before attempting to extract a file.");
            }
            else
            {
                byte[] SaveData = null;
                switch (LoadedLiveryType)
                {
                    case ForzaMotorsport.ForzaFileTypes.CarSetups:
                        SaveData = CarSetups.Extract();
                        break;
                    case  ForzaMotorsport.ForzaFileTypes.Vinyls:
                        SaveData = LivGroups.Extract();
                        break;
                    case  ForzaMotorsport.ForzaFileTypes.Liveries:
                        SaveData = LivCatalog.Extract();
                        break;
                }

                if (SaveData != null)
                {
                    var sfd = new SaveFileDialog();
                    sfd.FileName = this.advTree1.SelectedNodes[0].Cells[0].Text + "." + this.cmbLiveryType.Text.ToLower();
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        SaveData.Save(sfd.FileName);
                    }
                }
            }
        }
        private void BtnClick_InjectLivery(object sender, EventArgs e)
        {
            if (this.cmbLiveryType.Text != string.Empty && this.advTree1.SelectedIndex != -1)
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    byte[] SaveData = null, FileData = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read).ToArray();
                    switch (LoadedLiveryType)
                    {
                        case ForzaMotorsport.ForzaFileTypes.CarSetups:
                            SaveData = CarSetups.Inject(FileData);
                            break;
                        case ForzaMotorsport.ForzaFileTypes.Vinyls:
                            SaveData = LivGroups.Inject(FileData);
                            break;
                        case ForzaMotorsport.ForzaFileTypes.Liveries:
                            SaveData = LivCatalog.Inject(FileData);
                            break;
                    }
                    if (SaveData != null)
                    {
                        Package.StfsContentPackage.InjectFileFromArray(LiveryFolders[FolderIndex][FileIndex], SaveData);
                    }
                }
            }
        }


        private void BtnClick_SaveLivery(object sender, EventArgs e)
        {
            this.UnlockLiveries();
            this.LoadLiveryFromType();
        }
        private void BtnClick_LiveryLockClick(object sender, EventArgs e)
        {
            if (CarSetups == null && LivGroups == null && LivCatalog == null)
            {
                Horizon.Functions.UI.messageBox("Please select a livery file before you attempt to unlock one.");
            }
            else
            {
                this.btnLiveryIsUnlocked.Checked = !this.btnLiveryIsUnlocked.Checked;
            }
        }
        private void BtnClick_LiveryLockChanged(object sender, EventArgs e)
        {
            switch (LoadedLiveryType)
            {
                case ForzaMotorsport.ForzaFileTypes.CarSetups:
                    if(CarSetups != null)
                        CarSetups.IsUnlocked = (sender as DevComponents.DotNetBar.ButtonX).Checked;
                    break;
                case ForzaMotorsport.ForzaFileTypes.Vinyls:
                    if (LivGroups != null)
                        LivGroups.IsUnlocked = (sender as DevComponents.DotNetBar.ButtonX).Checked;
                    break;
                case ForzaMotorsport.ForzaFileTypes.Liveries:
                    if (LivCatalog != null)
                        LivCatalog.IsUnlocked = (sender as DevComponents.DotNetBar.ButtonX).Checked;
                    break;
            }
            
        }
        private void BtnClick_UnlockAll(object sender, EventArgs e)
        {
            this.CloseLiveries();

            int unlockedCount = 0;
            var DataTable = Database.GetDataTable("MetaData");
            if (DataTable != null)
            {
                for (int x = 0; x < this.LiveryFolders.Count; x++)
                {
                    bool IsLiveryLocked = Convert.ToBoolean(Database.SearchTableForSetting(this.LiveryFolders[x][2], "IsLocked", DataTable));
                    var LiveryIO = this.Package.StfsContentPackage.GetEndianIO(this.LiveryFolders[x][FileIndex], true, true);
                    if (IsLiveryLocked)
                    {
                        switch (LoadedLiveryType)
                        {
                            case ForzaMotorsport.ForzaFileTypes.CarSetups:
                                CarSetups = new Forza4CarSetup(LiveryIO, KeyMarshal, this.Creator);
                                if (!CarSetups.IsUnlocked)
                                {
                                    this.CarSetups.UnlockLockCarSetup(true);
                                    this.CarSetups.Save();
                                }

                                break;
                            case ForzaMotorsport.ForzaFileTypes.Vinyls:
                                LivGroups = new Forza4LiveryGroup(LiveryIO, KeyMarshal, this.Creator);
                                if (!LivGroups.IsUnlocked)
                                {
                                    Package.StfsContentPackage.InjectFileFromArray(LiveryFolders[x][FileIndex], this.LivGroups.Inject(this.LivGroups.UnlockLockLivery(true)), true);
                                }
                                break;
                            case ForzaMotorsport.ForzaFileTypes.Liveries:
                                LivCatalog = new Forza4LiveryCatalog(LiveryIO, KeyMarshal, this.Creator);
                                if (!LivCatalog.IsUnlocked)
                                {
                                    this.LivCatalog.UnlockLockLivery(true);
                                    this.LivCatalog.Save();
                                }
                                break;
                        }
                        this.Database.UpdateSetting("IsLocked", this.LiveryFolders[x][2], "0"); // 0 = false/unlocked, 1 = true/locked
                        unlockedCount++;
                    }
                }

                if (unlockedCount > 0)
                {
                    LoadLiveryFromType();
                    Horizon.Functions.UI.messageBox(string.Format("Successfully unlocked {0} liveries!", unlockedCount));
                }
                else
                {
                    Horizon.Functions.UI.messageBox("Could not find any locked liveries.");
                }
            }
            else
            {
                Horizon.Functions.UI.messageBox("Could not read livery database.");
            }
        }
        private void TreeClick_LiveryIndexChanged(object sender, EventArgs e)
        {
            CloseLiveries();
            LoadLiveryFromType();
        }

        private void LoadLiveryFromType()
        {
            if (this.advTree1.SelectedIndex != -1)
            {
                var Node = this.advTree1.SelectedNode;
                if (Node.Parent != null)
                {
                    int index = FindLiveryIndex(Node.Name.Replace(";", "\\"));
                    if (index != -1)
                    {
                        FolderIndex = index;
                        var LiveryIO = this.Package.StfsContentPackage.GetEndianIO(this.LiveryFolders[index][0], true, true);
                        switch (LoadedLiveryType)
                        {
                            case ForzaMotorsport.ForzaFileTypes.CarSetups:
                                try
                                {
                                    CarSetups = new Forza4CarSetup(LiveryIO, KeyMarshal, this.Creator);
                                }
                                catch
                                {
                                    Horizon.Functions.UI.messageBox("Error: Selected file is not a valid tune setup. Try to select a different type.");
                                }
                                break;
                            case ForzaMotorsport.ForzaFileTypes.Vinyls:
                                try
                                {
                                    LivGroups = new Forza4LiveryGroup(LiveryIO, KeyMarshal, this.Creator);
                                }
                                catch
                                {
                                    Horizon.Functions.UI.messageBox("Error: Selected file is not a valid vinyl design. Try to select a different type.");
                                }
                                break;
                            case ForzaMotorsport.ForzaFileTypes.Liveries:
                                try
                                {
                                    LivCatalog = new Forza4LiveryCatalog(LiveryIO, KeyMarshal, this.Creator);
                                }
                                catch
                                {
                                    Horizon.Functions.UI.messageBox("Error: Selected file is not a valid livery design. Try to select a different type.");
                                }
                                break;
                            default:
                                Horizon.Functions.UI.messageBox("Please selecte a livery file type.");
                                break;
                        }
                        this.btnLiveryIsUnlocked.Checked = !Convert.ToBoolean(Database.SearchMetaDataTableForSetting(this.LiveryFolders[index][2], "IsLocked"));
                    }
                }
            }
        }

        private void UnlockLiveries()
        {
            bool Locked = false;
            switch (LoadedLiveryType)
            {
                case ForzaMotorsport.ForzaFileTypes.CarSetups:
                    {
                        if (CarSetups != null)
                        {
                            this.CarSetups.UnlockLockCarSetup();
                            this.CarSetups.Save();
                            Locked = this.CarSetups.IsUnlocked;
                        }
                    }
                    break;
                case ForzaMotorsport.ForzaFileTypes.Vinyls:
                    {
                        if (LivGroups != null)
                        {
                            Package.StfsContentPackage.InjectFileFromArray(LiveryFolders[FolderIndex][FileIndex], this.LivGroups.Inject(this.LivGroups.UnlockLockLivery()), true);
                            Locked = this.LivGroups.IsUnlocked;
                        }
                    }
                    break;
                case ForzaMotorsport.ForzaFileTypes.Liveries:
                    {
                        if (LivCatalog != null)
                        {
                            this.LivCatalog.UnlockLockLivery();
                            this.LivCatalog.Save();
                            Locked = this.LivCatalog.IsUnlocked;
                        }
                    }
                    break;
            }
            this.Database.UpdateSetting("IsLocked", this.LiveryFolders[FolderIndex][2], Convert.ToUInt32(!Locked).ToString());
        }
        private void CloseLiveries()
        {
            switch (LoadedLiveryType)
            {
                case ForzaMotorsport.ForzaFileTypes.CarSetups:
                    if (CarSetups != null)
                        CarSetups.Close();
                    break;
                case ForzaMotorsport.ForzaFileTypes.Vinyls:
                    if (LivGroups != null)
                        LivGroups.Close();
                    break;
                case ForzaMotorsport.ForzaFileTypes.Liveries:
                    if (LivCatalog != null)
                        LivCatalog.Close();
                    break;
            }
        }

        private void addSetting(string Setting, string Value, AdvTree Tree)
        {
            string[] setChilds = Setting.Split('\\');
            string curPath = String.Empty;
            Node parentNode = null;
            for (int x = 0; x < setChilds.Length; x++)
            {
                curPath += setChilds[x];
                Node findNode = Tree.FindNodeByName(curPath);
                if (findNode == null)
                {
                    Node newNode = new Node(setChilds[x]);
                    newNode.Name = curPath;
                    newNode.ExpandVisibility = eNodeExpandVisibility.Auto;
                    if (x == setChilds.Length - 1)
                    {
                        Cell newCell = new Cell(Value);
                        newCell.Editable = true;
                        newNode.Cells.Add(newCell);
                    }
                    parentNode = parentNode == null ? Tree.Nodes[Tree.Nodes.Add(newNode)] : parentNode.Nodes[parentNode.Nodes.Add(newNode)];
                }
                else
                    parentNode = findNode;
                curPath += Tree.PathSeparator;
            }            
        }
    }
}