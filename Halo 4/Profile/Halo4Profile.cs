using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using Halo4;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.Halo_4
{

    public partial class Halo4Profile : EditorControl
    {    
        internal class LoadoutWeaponEntry
        {
            internal int Weapon;
            internal int Skin;
        }

        //public static readonly string FID = "4D53091X";
        private Halo4Settings _haloSettings;
        private int _lastLevelIndex = -1, _lastLoadoutIndex = -1;
        private bool _isSolo = true;
        private bool _importedSettings;
        private ProfileLoadouts.Loadout _currentLoadout;
        private static Dictionary<string, object> _grenades, _armorAbilities, _supportAndTactical; 

        public Halo4Profile()
        {
            InitializeComponent();
            TitleID = FormID.Halo4CampaignEditor;
            InitializeControls();
#if INT2
            btnExtract.Visible = true;
            btnInject.Visible = true;
#endif
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(TitleID + ".gpd"))
                return false;

            try
            {
                _haloSettings = new Halo4Settings(IO);
                LoadStats();

                return true;
            }
             catch
            {
                Functions.UI.messageBox(GPDSettingsNotFound[0], GPDSettingsNotFound[1], MessageBoxIcon.Warning);
            }


            return true;
        }

        public override void Save()
        {
            if (!_importedSettings)
            {
                UpdateCampaignLevel();
                UpdateLoadout();
                _haloSettings.Save();
            }
            else
            {
                _haloSettings.SaveToFile();
            }
        }

        private void LoadStats()
        {
            intExperience.Value = intSP.Value = 0x00;
            btnSolo.Checked = true;
            cmbCampaignLevel.SelectedIndex = 0x00;

            DisplayLoadouts();
            DisplayUnlockables();
        }

        private void BtnClickAddXp(object sender, EventArgs e)
        {
            _haloSettings.Profile.Experience += (uint)intExperience.Value;
            intExperience.Value = 0;
        }

        private void BtnClickAddSp(object sender, EventArgs e)
        {
            _haloSettings.Profile.SpartanPoints += (uint) intSP.Value;
            intSP.Value = 0x00;
        }

        private void BtnClickExtractProfileData(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog {FileName = "profile.bin"};
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            _haloSettings.Export().Save(sfd.FileName);
        }

        private void BtnClickInjectProfileData(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if(ofd.ShowDialog() != DialogResult.OK)
                return;

            _haloSettings.Import(System.IO.File.ReadAllBytes(ofd.FileName));
            _importedSettings = true;
        }

        // Campaign Progress functions
        private void CmbCampaignLevelIndexChanged(object sender, EventArgs e)
        {
            UpdateCampaignLevel();
            SelectCampaignLevel();
        }

        private void BtnClickCoOpSelected(object sender, EventArgs e)
        {
            UpdateCampaignLevel();
            btnSolo.Checked = false;
            _isSolo = false;
            SelectCampaignLevel();
        }

        private void BtnClickSoloSelected(object sender, EventArgs e)
        {
            UpdateCampaignLevel();
            btnCoOp.Checked = false;
            _isSolo = true;
            SelectCampaignLevel();
        }

        private void SelectCampaignLevel()
        {
            var level = _haloSettings.Campaign.CampaignProgress[cmbCampaignLevel.SelectedIndex];

            btnLevelEasy.Checked = _isSolo ? level.EasySolo : level.EasyCoOp;
            btnLevelNormal.Checked = _isSolo ? level.NormalSolo : level.NormalCoOp;
            btnLevelHeroic.Checked = _isSolo ? level.HeroicSolo : level.HeroicCoOp;
            btnLevelLegendary.Checked = _isSolo ? level.LegendarySolo : level.LegendaryCoOp;
            btnCmpgnUnlocked.Checked = level.Unlocked;

            _lastLevelIndex = cmbCampaignLevel.SelectedIndex;
        }

        private void UpdateCampaignLevel()
        {
            if(_lastLevelIndex == -1)
                return;

            var level = _haloSettings.Campaign.CampaignProgress[_lastLevelIndex];
            if (_isSolo)
            {
                level.EasySolo = btnLevelEasy.Checked;
                level.NormalSolo = btnLevelNormal.Checked;
                level.HeroicSolo = btnLevelHeroic.Checked;
                level.LegendarySolo = btnLevelLegendary.Checked;
            }
            else
            {
                level.EasyCoOp = btnLevelEasy.Checked;
                level.NormalCoOp = btnLevelNormal.Checked;
                level.HeroicCoOp = btnLevelHeroic.Checked;
                level.LegendaryCoOp = btnLevelLegendary.Checked;
            }
            level.Unlocked = btnCmpgnUnlocked.Checked;
        }

        private void InitializeControls()
        {
            if (Halo4Campaign.CampaignLevels != null) cmbCampaignLevel.Items.AddRange(Halo4Campaign.CampaignLevels);
            if (_grenades == null || _armorAbilities == null || _supportAndTactical == null)
            {
                _grenades = new Dictionary<string, object>();
                _armorAbilities = new Dictionary<string, object>();
                _supportAndTactical = new Dictionary<string, object>();

                foreach (var grenade in Enum.GetValues(typeof (ProfileLoadouts.Grenade)).Cast<ProfileLoadouts.Grenade>()
                    )
                {
                    _grenades.Add(Regex.Replace(grenade.ToString(), "([a-z])([A-Z])", "$1 $2"), grenade);
                }
                foreach (
                    var armorAbility in
                        Enum.GetValues(typeof (ProfileLoadouts.ArmorAbility)).Cast<ProfileLoadouts.ArmorAbility>())
                {
                    _armorAbilities.Add(Regex.Replace(armorAbility.ToString(), "([a-z])([A-Z])", "$1 $2"), armorAbility);
                }
                foreach (
                    var tacticalPackage in
                        Enum.GetValues(typeof (ProfileLoadouts.TacticalPackage)).Cast<ProfileLoadouts.TacticalPackage>()
                    )
                {
                    _supportAndTactical.Add(Regex.Replace(tacticalPackage.ToString(), "([a-z])([A-Z])", "$1 $2"),
                                          tacticalPackage);
                }
                foreach (
                    var supportUpgrade in
                        Enum.GetValues(typeof (ProfileLoadouts.SupportUpgrade)).Cast<ProfileLoadouts.SupportUpgrade>())
                {
                    _supportAndTactical.Add(Regex.Replace(supportUpgrade.ToString(), "([a-z])([A-Z])", "$1 $2"),
                                         supportUpgrade);
                }
            }

            foreach (
                var primaryWeapon in
                    Enum.GetValues(typeof (ProfileUnlockables.PrimaryWeapon))
                        .Cast<ProfileUnlockables.PrimaryWeapon>())
            {
                string weaponName;
                try
                {
                    //_weaponsList.Add(Regex.Replace(primaryWeapon.ToString(), "([a-z])([A-Z])", "$1 $2").Replace('_', ' '));
                    var name = Regex.Replace(primaryWeapon.ToString(), "([a-z])([A-Z])", "$1 $2");
                    var cmbItem = new ComboItem(name.Replace('_', ' '));
                    weaponName = primaryWeapon.ToString();
                    var loadoutWeap = new LoadoutWeaponEntry();

                    if (name.Contains("_"))
                    {
                        weaponName = weaponName.Substring(0, weaponName.IndexOf("_"));
                        loadoutWeap.Skin =
                            (int)
                            ((ProfileLoadouts.PrimaryWeaponSkin)
                             Enum.Parse(typeof(ProfileLoadouts.PrimaryWeaponSkin), primaryWeapon.ToString()));
                    }
                    loadoutWeap.Weapon = (int)((ProfileLoadouts.Weapon)(Enum.Parse(typeof(ProfileLoadouts.Weapon), weaponName)));
                    cmbItem.Tag = loadoutWeap;
                    cmbPrimaryWeapon.Items.Add(cmbItem);
                    cmbSecondaryWeapon.Items.Add(cmbItem);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            foreach (
                var secondaryWeapon in
                    Enum.GetValues(typeof (ProfileUnlockables.SecondaryWeapon))
                        .Cast<ProfileUnlockables.SecondaryWeapon>())
            {
                var name = Regex.Replace(secondaryWeapon.ToString(), "([a-z])([A-Z])", "$1 $2");
                var cmbItem = new ComboItem(name.Replace('_', ' '));
                var weaponName = secondaryWeapon.ToString();
                var loadoutWeap = new LoadoutWeaponEntry();

                if (name.Contains("_"))
                {
                    weaponName = weaponName.Substring(0, weaponName.IndexOf("_"));
                    loadoutWeap.Skin =
                        (int)
                        ((ProfileLoadouts.SecondaryWeaponSkin)
                         Enum.Parse(typeof(ProfileLoadouts.SecondaryWeaponSkin), secondaryWeapon.ToString()));
                }
                loadoutWeap.Weapon = (int)((ProfileLoadouts.Weapon)Enum.Parse(typeof(ProfileLoadouts.Weapon), weaponName));
                cmbItem.Tag = loadoutWeap;
                cmbPrimaryWeapon.Items.Add(cmbItem);
                cmbSecondaryWeapon.Items.Add(cmbItem);
            }
            
            cmbGrenade.Items.AddRange(_grenades.Keys.ToArray());
            cmbArmorAbility.Items.AddRange(_armorAbilities.Keys.ToArray());
            cmbTacticalPackage.Items.AddRange(_supportAndTactical.Keys.ToArray());
            cmbSupportUpgrade.Items.AddRange(_supportAndTactical.Keys.ToArray());}

        // Loadouts functions
        private void DisplayLoadouts()
        {
            cmbLoadout.Items.Clear();

            // Create loadout listing
            for (int i = 0; i < _haloSettings.Loadouts.AvailableLoadouts; i++)
                cmbLoadout.Items.Add(i.ToString());

            cmbLoadout.SelectedIndex = 0x00;
        }
        
        private void SelectLoadout()
        {
            _currentLoadout = _haloSettings.Loadouts.Loadouts[_lastLoadoutIndex = cmbLoadout.SelectedIndex];

            cmbPrimaryWeapon.SelectedIndex = SelectWeapon(_currentLoadout.PrimaryWeapon, true);// != -1 ? weaponIndex : SelectSecondaryWeapon(_currentLoadout.PrimaryWeapon);
            cmbSecondaryWeapon.SelectedIndex = SelectWeapon(_currentLoadout.SecondaryWeapon, false);// != -1 ? weaponIndex : SelectSecondaryWeapon(_currentLoadout.SecondaryWeapon);
            cmbGrenade.Text = GetItemNameFromValue(_grenades, _currentLoadout.Grenade);

            cmbArmorAbility.Text = GetItemNameFromValue(_armorAbilities, _currentLoadout.ArmorAbility);
            cmbTacticalPackage.Text = GetItemNameFromValue(_supportAndTactical, _currentLoadout.TacticalPackage);
            cmbSupportUpgrade.Text = GetItemNameFromValue(_supportAndTactical, _currentLoadout.SupportUpgrade);
        }

        private void UpdateLoadout()
        {
            if(_lastLoadoutIndex == -1)
                return;

            var weapon = SetWeapon(true);
            _currentLoadout.PrimaryWeapon = weapon[0x00];
            _currentLoadout.PrimaryWeaponSkin = weapon[0x01];
            weapon = SetWeapon(false);
            _currentLoadout.SecondaryWeapon = weapon[0x00];
            _currentLoadout.SecondaryWeaponSkin = weapon[0x01];

            // for the grenades the index can be set this way
            _currentLoadout.Grenade = (byte)cmbGrenade.SelectedIndex;

            _currentLoadout.ArmorAbility = (byte) _armorAbilities[cmbArmorAbility.Text];
            _currentLoadout.TacticalPackage = (byte)_supportAndTactical[cmbTacticalPackage.Text];
            _currentLoadout.SupportUpgrade = (byte)_supportAndTactical[cmbSupportUpgrade.Text];
        }

        private byte[] SetWeapon(bool primary)
        {
            var weaponInfo = new byte[0x02]; // weapon, weapon skin
            var cmbBox = primary ? cmbPrimaryWeapon : cmbSecondaryWeapon;
            var item = (ComboItem)cmbBox.Items[cmbBox.SelectedIndex];
            var loadoutWeapon = (LoadoutWeaponEntry)item.Tag;
            weaponInfo[0] = (byte) loadoutWeapon.Weapon;
            weaponInfo[1] = (byte) loadoutWeapon.Skin;

            return weaponInfo;
        }

        private int SelectWeapon(byte weapon, bool primary)
        {
            var index = -1;
            for (var i = 0; i < cmbPrimaryWeapon.Items.Count; i++)
            {
                var item = (ComboItem)cmbPrimaryWeapon.Items[i];
                
                var loadoutWeapon = (LoadoutWeaponEntry)item.Tag;
                if (loadoutWeapon.Weapon == weapon &&
                    loadoutWeapon.Skin == (primary ? _currentLoadout.PrimaryWeaponSkin : _currentLoadout.SecondaryWeaponSkin))
                {
                    index = i;
                }
            }
            return index;
        }

        private static string GetItemNameFromValue(Dictionary<string, object> dictionary, byte value)
        {
            var keys = from entry in dictionary
                       where (byte)entry.Value == value
                       select entry.Key;
            if(!keys.Any())
                throw new Exception("attempted to retrieve an invalid item");

            return keys.ElementAt(0x00);
        }

        private void CmbLoadoutIndexChanged(object sender, EventArgs e)
        {
            UpdateLoadout();
            SelectLoadout();
        }

        // Unlockables functions
        private void DisplayUnlockables()
        {
            advUnlockablesTree.Nodes.Clear();

            // read 'Loadouts' unlockables
            var loadoutsNode = new Node("Loadouts");
            loadoutsNode.Nodes.Add(FillNodeFromEnumArray("Available Loadouts",
                                                         Enum.GetValues(typeof (ProfileUnlockables.ProfileLoadout))));
            loadoutsNode.Nodes.Add(FillNodeFromEnumArray("Primary Weapons", Enum.GetValues(typeof(ProfileUnlockables.PrimaryWeapon))));
            loadoutsNode.Nodes.Add(FillNodeFromEnumArray("Secondary Weapons", Enum.GetValues(typeof(ProfileUnlockables.SecondaryWeapon))));
            loadoutsNode.Nodes.Add(FillNodeFromEnumArray("Grenades", Enum.GetValues(typeof(ProfileUnlockables.Grenade))));
            loadoutsNode.Nodes.Add(FillNodeFromEnumArray("Armor Abilities", Enum.GetValues(typeof(ProfileUnlockables.ArmorAbility))));
            loadoutsNode.Nodes.Add(FillNodeFromEnumArray("Tactical Packages",  Enum.GetValues(typeof(ProfileUnlockables.TacticalPackage))));
            loadoutsNode.Nodes.Add(FillNodeFromEnumArray("Support Upgrades",  Enum.GetValues(typeof(ProfileUnlockables.SupportUpgrade))));

            var armorNode = new Node("Spartan Armor");
            armorNode.Nodes.Add(FillNodeFromEnumArray("Helmet", Enum.GetValues(typeof (ProfileUnlockables.Helmet))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Torso", Enum.GetValues(typeof(ProfileUnlockables.Torso))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Left Shoulder", Enum.GetValues(typeof(ProfileUnlockables.LeftShoulder))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Right Shoulder", Enum.GetValues(typeof(ProfileUnlockables.RightShoulder))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Forearms", Enum.GetValues(typeof(ProfileUnlockables.Forearms))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Legs", Enum.GetValues(typeof(ProfileUnlockables.Legs))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Visor", Enum.GetValues(typeof(ProfileUnlockables.Visor))));

            var spartanId = new Node("Spartan ID");
            spartanId.Nodes.Add(FillNodeFromEnumArray("Stance", Enum.GetValues(typeof (ProfileUnlockables.Stance))));

            var emblems = new Node("Emblems");
            emblems.Nodes.Add(FillNodeFromEnumArray("Foreground", Enum.GetValues(typeof(ProfileUnlockables.EmblemForeground)),
                Enum.GetNames(typeof(ProfileUnlockables.EmblemForeground))));
            emblems.Nodes.Add(FillNodeFromEnumArray("Background",
                                                    Enum.GetValues(typeof (ProfileUnlockables.EmblemBackground)),
                                                    Enum.GetNames(typeof (ProfileUnlockables.EmblemBackground))));

            advUnlockablesTree.Nodes.Add(loadoutsNode);
            advUnlockablesTree.Nodes.Add(armorNode);
            advUnlockablesTree.Nodes.Add(spartanId);
            advUnlockablesTree.Nodes.Add(emblems);
        }

        private Node FillNodeFromEnumArray(string nodeText, Array enumArray, Array nameArray)
        {
            var itemNode = new Node(nodeText);
            for (int x = 0; x < enumArray.Length; x++ )
            {
                itemNode.Nodes.Add(
                    new Node(Regex.Replace(nameArray.GetValue(x).ToString(), "([a-z])([A-Z])", "$1 $2").Replace('_', ' '))
                    {
                        CheckBoxVisible = true,
                        Checked = _haloSettings.Unlockables.IsUnlocked(enumArray.GetValue(x)),
                        Tag = enumArray.GetValue(x)
                    });
            }
            return itemNode;
        }

        private Node FillNodeFromEnumArray(string nodeText, Array enumArray)
        {
            var itemNode = new Node(nodeText);
            foreach (var item in enumArray)
            {
                itemNode.Nodes.Add(
                    new Node(Regex.Replace(item.ToString(), "([a-z])([A-Z])", "$1 $2").Replace('_', ' '))
                    {
                        CheckBoxVisible = true,
                        Checked = _haloSettings.Unlockables.IsUnlocked(item),
                        Tag = item
                    });
            }
            return itemNode;
        }

        private void AdvUnlockablesAfterCheck(object sender, AdvTreeCellEventArgs e)
        {
            if(e.Cell.Tag == null)
                return;

            var selectedNode = advUnlockablesTree.SelectedNode;
            var tag = (int)e.Cell.Tag;
            var isChecked = e.Cell.Checked;

            _haloSettings.Unlockables.SetLock(tag, isChecked);
            if (selectedNode != null && selectedNode.Parent != null && selectedNode.Parent.Parent != null && selectedNode.Parent.Parent.Text == "Emblems")
                CheckAllChildNodesByTag(selectedNode.Parent, isChecked, tag);
        }

        private void BtnClickUnlockAll(object sender, EventArgs e)
        {
            foreach (Node node in advUnlockablesTree.Nodes)
            {
                CheckAllChildNodes(node, true);
            }
            Functions.UI.messageBox("Unlocked all Loadouts, Spartan Armor, Stances, and Emblems!", "Success!", MessageBoxIcon.Information);
        }

        private static void CheckAllChildNodes(Node treeNode, bool nodeChecked)
        {
            foreach (Node node in treeNode.Nodes)
            {
                if(node.Tag != null)
                    node.Checked = nodeChecked;

                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        private static void CheckAllChildNodesByTag(Node treeNode, bool nodeChecked, int tag)
        {
            foreach (Node node in treeNode.Nodes)
            {
                if (node.Tag != null && Convert.ToInt32(node.Tag) == tag)
                    node.Checked = nodeChecked;

                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
    }
}
