using System;
using DevComponents.AdvTree;
using DevComponents.Editors;
using Ubisoft;

namespace Horizon.PackageEditors.Assassin_s_Creed_III
{
    public partial class ACIII : EditorControl
    {
        private struct CharacterValue
        {
            internal ACIIISaveGame.Characters Character;
            internal uint Key;
        }

        private ACIIISaveGame saveGame;

        public ACIII()
        {
            InitializeComponent();
            TitleID = FormID.AssassinsCreedIII;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            saveGame = new ACIIISaveGame(IO);
            // 0x12f6b6f1
            listCharacters.Nodes.Add(
                CreateIntNode(new CharacterValue {Character = ACIIISaveGame.Characters.Haytham, Key = 0x12F6B6F1},
                              "Haytham", 999999999));
            listCharacters.Nodes.Add(
                CreateIntNode(new CharacterValue {Character = ACIIISaveGame.Characters.Connor, Key = 0x12F6B6F1},
                              "Connor", 999999999));

            return true;
        }

        public override void Save()
        {
            saveGame.Save();
        }

        private void cmdMaxAllCharacters_Click(object sender, EventArgs e)
        {
            foreach (Node invNode in listCharacters.Nodes)
            {
                var intInput = (invNode.Cells[1].HostedControl as IntegerInput);
                if (intInput != null)
                {
                    intInput.Value = intInput.MaxValue;
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
    }
}
