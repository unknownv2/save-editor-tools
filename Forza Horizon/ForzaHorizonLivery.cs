using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DevComponents.AdvTree;
using ForzaMotorsport;

namespace Horizon.PackageEditors.Forza_Horizon
{
    public partial class ForzaHorizonLivery : EditorControl
    {
        //public static readonly string FID = "4D5309CU";
        private ForzaLiveryDatabase _database;
        private static ForzaKeyMarshal _keyMarshal;
        private byte[] _creator;
        private int _fileIndex;

        private List<List<string>> _liveryFolders;
        private ForzaFileTypes _loadedLiveryType;
        private ForzaLivery _liveryFile;

        public ForzaHorizonLivery()
        {
            InitializeComponent();
            InitializeControls();
            TitleID = FormID.ForzaHorizonProfile;
        }

        private void InitializeControls()
        {
            cmbLiveryType.Items.AddRange(new object[] 
            {
                "CarSetup",
                "LivGroups",
                "LivCatalog"
            });
        }

        public override bool Entry()
        {
            _creator = Functions.Global.convertToBigEndian(BitConverter.GetBytes(Package.Header.Metadata.Creator));
            InitiateKeys();
            try
            {
                _database = new ForzaLiveryDatabase(Package.StfsContentPackage.GetEndianIO("DatabaseReservedName*"),
                    Encoding.ASCII.GetBytes(" at this time.\0D"), Encoding.ASCII.GetBytes("ct to the other player's console"),
                    Package.Header.Metadata.Creator, null, ForzaVersion.ForzaHorizon);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override void Save()
        {
            // close any remaining files that are open
            if(_liveryFile != null)
                _liveryFile.Close();

            _database.Save(true);
        }

        private void InitiateKeys()
        {
            if (_keyMarshal != null) return;
            _keyMarshal = new ForzaKeyMarshal();
            _keyMarshal.AddKey("LiveryGroupAES", GlobalForzaSecurity.TransformHorizonSessionKey(SettingAsByteArray(108), _creator, 0x00));
            _keyMarshal.AddKey("LiveryGroupSHA", GlobalForzaSecurity.TransformHorizonSessionKey(SettingAsByteArray(228), _creator, 0x03));
            _keyMarshal.AddKey("CarSetupAES", GlobalForzaSecurity.TransformHorizonSessionKey(SettingAsByteArray(253), _creator, 0x02));
            _keyMarshal.AddKey("CarSetupSHA", GlobalForzaSecurity.TransformHorizonSessionKey(SettingAsByteArray(213), _creator, -1));
            _keyMarshal.AddKey("LiveryCatalogAES", GlobalForzaSecurity.TransformHorizonSessionKey(SettingAsByteArray(240), _creator, 1));
            _keyMarshal.AddKey("LiveryCatalogSHA", GlobalForzaSecurity.TransformHorizonSessionKey(SettingAsByteArray(13), _creator, -3));
        }

        private bool LoadLiveries()
        {
            var liveries = _database.GetFilenames(_loadedLiveryType);
            if (liveries.Count <= 0)
                return false;
            
            var authors = _database.GetOwners(_loadedLiveryType);
            var saveIds = _database.GetSaveIds();

            _liveryFolders = new List<List<string>>();
            for (int x = 0; x < liveries.Count; x++)
            {
                List<string> livery = new List<string> {liveries[x], authors[x], saveIds[x]};

                _liveryFolders.Add(livery);

                AddSetting(livery[0], null, advLiveryTree);
            }
            return true;
        }

        private void CloseLiveryFile()
        {
            if(_liveryFile == null)
                return;
            _liveryFile.Close();
        }

        private void OpenLiveryFile()
        {
            if (advLiveryTree.SelectedIndex == -1)
                return;
            var liveryNode = advLiveryTree.SelectedNode;
            if(liveryNode == null)
                return;
            var liveryFileName = liveryNode.Name.Replace(";", "\\");
            int liveryIndex = FindLiveryIndex(liveryFileName);
            if (liveryIndex == -1)
                return;
            _fileIndex = liveryIndex;
            var liveryFile = Package.StfsContentPackage.GetEndianIO(_liveryFolders[_fileIndex][0], true);
            switch (_loadedLiveryType)
            {
                case ForzaFileTypes.Vinyls:
                    _liveryFile = new ForzaVinylGroup(liveryFile, _keyMarshal, ForzaVersion.ForzaHorizon);
                break;
                    case ForzaFileTypes.CarSetups:
                    _liveryFile = new ForzaCarSetup(liveryFile, _keyMarshal, ForzaVersion.ForzaHorizon);
                break;
                    case ForzaFileTypes.Liveries:
                    _liveryFile = new ForzaLiveryCatalog(liveryFile, _keyMarshal, ForzaVersion.ForzaHorizon);
                break;
            }
            btnLiveryIsUnlocked.Checked = _liveryFile.IsUnlocked;
        }

        private void UnlockLivery(bool unlockLivery)
        {
            if(_liveryFile == null)
                return;

            _liveryFile.LockUnlock(unlockLivery);
            _liveryFile.Save();
            _database.UpdateSetting("IsLocked", _liveryFolders[_fileIndex][2], Convert.ToUInt32(!unlockLivery).ToString());
        }

        private void UnlockAllLiveries()
        {
            int unlockedCount = 0x00;
            advLiveryTree.SelectedIndex = -1;
            var metaDataTable = _database.GetDataTable("MetaData");
            if(metaDataTable == null)
                Functions.UI.messageBox("Could not read livery metadata table.");

            foreach (var liveryFolder in _liveryFolders)
            {
                bool isLiveryLocked = Convert.ToBoolean(_database.SearchTableForSetting(liveryFolder[2], "IsLocked", metaDataTable));
                if(!isLiveryLocked) continue;
                var liveryFile = Package.StfsContentPackage.GetEndianIO(liveryFolder[0], true);
                switch (_loadedLiveryType)
                {
                    case ForzaFileTypes.Vinyls:
                        _liveryFile = new ForzaVinylGroup(liveryFile, _keyMarshal, ForzaVersion.ForzaHorizon);
                        break;
                    case ForzaFileTypes.CarSetups:
                        _liveryFile = new ForzaCarSetup(liveryFile, _keyMarshal, ForzaVersion.ForzaHorizon);
                        break;
                    case ForzaFileTypes.Liveries:
                        _liveryFile = new ForzaLiveryCatalog(liveryFile, _keyMarshal, ForzaVersion.ForzaHorizon);
                        break;
                }
                UnlockLivery(true);
                unlockedCount++;
            }
            if (unlockedCount > 0)
            {
                OpenLiveryFile();
                Functions.UI.messageBox(string.Format("Successfully unlocked {0} liveries!", unlockedCount));
            }
            else
            {
                Functions.UI.messageBox("Could not find any locked liveries.");
            }
        }

        private int FindLiveryIndex(string name)
        {
            return _liveryFolders.FindIndex(liveryName => string.Compare(name, liveryName[0x00], true) == 0x00);
        }

        private void AddSetting(string Setting, string Value, AdvTree Tree)
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
                        if(!string.IsNullOrEmpty(Value))
                        {
                            Node newCell = new Node(Value);
                            newCell.Editable = true;
                            newNode.Nodes.Add(newCell);
                        }
                    }
                    parentNode = parentNode == null ? Tree.Nodes[Tree.Nodes.Add(newNode)] : parentNode.Nodes[parentNode.Nodes.Add(newNode)];
                }
                else
                    parentNode = findNode;
                curPath += Tree.PathSeparator;
            }
        }

        private void CombBoxLiveryTypeChanged(object sender, EventArgs e)
        {
            switch ((sender as DevComponents.DotNetBar.Controls.ComboBoxEx).Text)
            {
                case "CarSetup":
                    _loadedLiveryType = ForzaFileTypes.CarSetups;
                    break;
                case "LivGroups":
                    _loadedLiveryType = ForzaFileTypes.Vinyls;
                    break;
                case "LivCatalog":
                    _loadedLiveryType = ForzaFileTypes.Liveries;
                    break;
            }

            advLiveryTree.Nodes.Clear();
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

        private void TreeViewSelectedLiveryFileChanged(object sender, EventArgs e)
        {
            CloseLiveryFile();
            OpenLiveryFile();
        }

        private void BtnClickExtractLivery(object sender,EventArgs e)
        {
            if(_liveryFile == null)
                return;

            var sfd = new SaveFileDialog
                  {
                      FileName = advLiveryTree.SelectedNodes[0].Cells[0].Text + "." + cmbLiveryType.Text.ToLower()
                  };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var liveryData = _liveryFile.Extract();
            if(liveryData == null)
                return;

            liveryData.Save(sfd.FileName);
        }

        private void BtnClickInjectLivery(object sender, EventArgs e)
        {
            if (_liveryFile == null)
                return;

            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var fileData = File.ReadAllBytes(ofd.FileName);
            _liveryFile.Inject(fileData);
        }

        private void BtnClickSaveLivery(object sender, EventArgs e)
        {
            UnlockLivery(btnLiveryIsUnlocked.Checked);
        }

        private void BtnClickUnlockAllLiveries(object sender, EventArgs e)
        {
            UnlockAllLiveries();
        }
    }
}
