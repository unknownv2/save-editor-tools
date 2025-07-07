using System;
using System.Text.RegularExpressions;
using Bandai;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Dragonball_XenoVerse
{
    public partial class DragonballXenoVerse : EditorControl
    {
        private DBXenoVerseSaveGame _saveGame;

        public DragonballXenoVerse()
        {
            InitializeComponent();
            TitleID = FormID.DragonballXenoVerse;
            
            // initialize the tags for each button unlock list
            bUnlockAllUpperBody.Tag = BagItemType.CostumeTopItem;
            bUnlockAllUpperBody.Tag = BagItemType.CostumeBottomItem;
            bUnlockAllHands.Tag = BagItemType.CostumeGlove;
            bUnlockAllFeet.Tag = BagItemType.CostumeShoe;
            bUnlockAllAccessory.Tag = BagItemType.Accessories;
            bUnlockAllZSoul.Tag = BagItemType.ZSoul;

            bUnlockAllCapsules.Tag = BagItemType.Capsule;
            bUnlockAllMixingItems.Tag = BagItemType.MixingItem;
            bUnlockAllImportantItems.Tag = BagItemType.ImportantItem;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("savegame.txt"))
                return false;

            _saveGame = new DBXenoVerseSaveGame(IO);

            LoadStats();
            cmbTransplantChIndex.SelectedIndex = 0;

            return true;
        }

        public override void Save()
        {
            UnlockItemsInPanel(gpBattle);
            UnlockItemsInPanel(gpEquipment);

            _saveGame.CharacterSlotsUnlocked = chkUnlockChSlots.Checked;

            _saveGame.Zeni = (uint) intZeni.Value;
            _saveGame.Save();
        }

        // unlock selected items and max the count in the bag
        private void UnlockItemsInPanel(GroupPanel groupPanel)
        {
            foreach (var control in groupPanel.Controls)
            {
                var buttonItem = control as ButtonX;

                if (buttonItem == null)
                {
                    throw new Exception("DB XenoVerse: invalid item found in the controls list.");
                }
                if (buttonItem.Checked)
                {
                    _saveGame.UnlockAllItemsByType((BagItemType) buttonItem.Tag);
                }
            }
        }

        private void LoadStats()
        {
            // clear any old data in the editor
            listPlayerStats.Nodes.Clear();
            cmbPlayerIndex.Items.Clear();
            cmbTransplantChIndex.Items.Clear();

            chkUnlockChSlots.Checked = _saveGame.CharacterSlotsUnlocked;
            for (int i = 0; i < _saveGame.CharacterEntries.Count; i++)
            {
                var characterEntry = _saveGame.CharacterEntries[i];
                var cmbEntry = string.Format("{0} : {1}", i + 1,
                    string.IsNullOrEmpty(characterEntry.Name) ? "New" : characterEntry.Name);
                cmbPlayerIndex.Items.Add(cmbEntry);
                cmbTransplantChIndex.Items.Add(cmbEntry);
            }

            intZeni.Value = (int) _saveGame.Zeni;
            cmbPlayerIndex.SelectedIndex = 0;
        }

        private Node CreateIntStatsNode(DBXVPlayerStats key, string title, int maxValue)
        {
            var intInput = new IntegerInput
            {
                Tag = key,
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true,
                Value = (int) _saveGame.CharacterEntries[cmbPlayerIndex.SelectedIndex].PlayerStats[key]
            };

            intInput.ValueChanged += NodeIntStatsInput_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell {HostedControl = intInput});

            return node;
        }

        private Node CreateIntAttributesNode(DBXVAttributes key, string title, int maxValue)
        {
            var intInput = new IntegerInput
            {
                Tag = key,
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true,
                Value = (int) _saveGame.CharacterEntries[cmbPlayerIndex.SelectedIndex].PlayerAttributes[key]
            };

            intInput.ValueChanged += NodeIntAttributesInput_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell {HostedControl = intInput});

            return node;
        }

        private void NodeIntStatsInput_ValueChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            var i = sender as IntegerInput;
            if (i == null)
            {
                throw new Exception("DB XenoVerse: invalid IntegerInput found!");
            }

            var key = (DBXVPlayerStats) i.Tag;

            _saveGame.CharacterEntries[cmbPlayerIndex.SelectedIndex].PlayerStats[key] = (uint) i.Value;
        }

        private void NodeIntAttributesInput_ValueChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            var i = sender as IntegerInput;
            if (i == null)
            {
                throw new Exception("DB XenoVerse: invalid IntegerInput found!");
            }

            var key = (DBXVAttributes) i.Tag;

            _saveGame.CharacterEntries[cmbPlayerIndex.SelectedIndex].PlayerAttributes[key] = (uint) i.Value;
        }

        private void BtnClick_MaxAllStats(object sender, EventArgs e)
        {
            foreach (Node invNode in listPlayerStats.Nodes)
            {
                var intInput = (invNode.Cells[1].HostedControl as IntegerInput);
                if (intInput != null)
                {
                    intInput.Value = intInput.MaxValue;
                }
            }
        }

        private void BtnClick_MaxZeni(object sender, EventArgs e)
        {
            intZeni.Value = intZeni.MaxValue;
        }
        private void TxtChanged_CharacterName(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPlayerName.Text))
                return;

            _saveGame.CharacterEntries[cmbPlayerIndex.SelectedIndex].Name = txtPlayerName.Text;
            var cmbEntry = string.Format("{0} : {1}", cmbPlayerIndex.SelectedIndex + 1, string.IsNullOrEmpty(txtPlayerName.Text) ? "Empty" : txtPlayerName.Text);
            cmbPlayerIndex.Items[cmbPlayerIndex.SelectedIndex] = cmbEntry;
            cmbTransplantChIndex.Items[cmbPlayerIndex.SelectedIndex] = cmbEntry;
        }

        private void CmbCharacterIndexChanged(object sender, EventArgs e)
        {
            // Create Player Stats Nodes
            listPlayerStats.Nodes.Clear();

            listPlayerStats.Nodes.Add(CreateIntStatsNode(DBXVPlayerStats.Level, "Level", 99));
            listPlayerStats.Nodes.Add(CreateIntStatsNode(DBXVPlayerStats.Experience, "Experience", 999999999));
            listPlayerStats.Nodes.Add(CreateIntStatsNode(DBXVPlayerStats.AttributePoints, "Attribute Points", 99999));
            // Create Player Attributes Nodes
            foreach (DBXVAttributes attribute in Enum.GetValues(typeof(DBXVAttributes)))
            {
                listPlayerStats.Nodes.Add(CreateIntAttributesNode(attribute, Regex.Replace(attribute.ToString(), "([a-z])([A-Z])", "$1 $2"), 99999));
            }

            txtPlayerName.Text = _saveGame.CharacterEntries[cmbPlayerIndex.SelectedIndex].Name;
        }

        private void BtnClick_TransplantCharacter(object sender, EventArgs e)
        {
            if (cmbPlayerIndex.SelectedIndex == cmbTransplantChIndex.SelectedIndex)
            {
                Functions.UI.messageBox("Please select a different slot to copy the character to.");
            }
            else
            {
                // copy the new player data 
                int originalChIndex = cmbPlayerIndex.SelectedIndex;
                int newChIndex = cmbTransplantChIndex.SelectedIndex;

                _saveGame.CharacterEntries[originalChIndex].Clone(_saveGame.CharacterEntries[newChIndex]);

                _saveGame.Transplant(originalChIndex, newChIndex, _saveGame.CharacterEntries[newChIndex]);

                _saveGame.CharacterEntries[newChIndex].PlayerStats[DBXVPlayerStats.Level] = 1;
                //_saveGame.CharacterEntries[newChIndex].Name = "Test";

                // reload stats
                LoadStats();

                Functions.UI.messageBox("Successfully copied the character.");
            }
        }
    }
}
