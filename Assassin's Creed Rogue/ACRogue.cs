using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.Editors;
using Ubisoft;

namespace Horizon.PackageEditors.Assassin_s_Creed_Rogue
{
    public partial class ACRogue : EditorControl
    {
        private struct CharacterValue
        {
            internal ACRogueSaveGame.Characters Character;
            internal uint Key;
        }

        private ACRogueSaveGame saveGame;

        public ACRogue()
        {
            InitializeComponent();
            TitleID = FormID.AssassinsCreedRogue;
        }
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            saveGame = new ACRogueSaveGame(IO);

            var ids = new Dictionary<uint, string>
                {
                    {0x12F6B6F1, "Reales"},
                    {0x654B51DC, "Metal"},
                    {0x1F8D672A, "Wood"},
                    {0xBC09A03F, "Stone"},
                    {0x87D8CEAB, "Cloth"},
                    {0x54C4070C, "Tobacco"},
                    
                    // stone
                    // tobacco
                };
            var character = new Node("  Shay Patrick Cormac");
            if (saveGame.ObjectManager.Inventory.Count == 0)
            {
                return false;
            }
            foreach (KeyValuePair<uint, string> id in ids)
            {
                character.Nodes.Add(CreateIntNode(new CharacterValue { Character = ACRogueSaveGame.Characters.Shay, Key = id.Key }, id.Value,
                    id.Key == 0x12F6B6F1 ? 999999999 : 2000));
            }

            listInventory.Nodes.Add(character);
            listInventory.ExpandAll();

            return true;
        }

        public override void Save()
        {
            saveGame.Save();
        }

        private void cmdMaxAllInventoryItems_Click(object sender, EventArgs e)
        {
            foreach (Node charNode in listInventory.Nodes)
            {
                foreach (Node invNode in charNode.Nodes)
                {
                    var intInput = (invNode.Cells[1].HostedControl as IntegerInput);
                    if (intInput != null)
                    {
                        intInput.Value = intInput.MaxValue;
                    }
                }
            }
        }

        private Node CreateIntNode(CharacterValue key, string title, int maxValue)
        {
            var intInput = new IntegerInput
            {
                Tag = key,
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true,
                Value = saveGame.ObjectManager.Inventory[(int)key.Character][key.Key].Value,
            };

            intInput.ValueChanged += NodeIntInput_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = intInput });

            return node;
        }
        private void NodeIntInput_ValueChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            IntegerInput i = sender as IntegerInput;
            if (i == null)
                return;

            CharacterValue key = (CharacterValue)i.Tag;

            saveGame.ObjectManager.Inventory[(int)key.Character][key.Key].Value = i.Value;

        }
        private void BtnClick_InjectData(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            saveGame.ObjectManager = new ACRogueSaveGame.ObjectDeserializer(new EndianIO(File.ReadAllBytes(ofd.FileName), EndianType.BigEndian, true));
        }
    }
}
