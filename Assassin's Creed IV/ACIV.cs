using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.Editors;
using Ubisoft;

namespace Horizon.PackageEditors.Assassin_s_Creed_IV
{
    public partial class ACIV : EditorControl
    {
        private struct CharacterValue
        {
            internal ACIVSaveGame.Characters Character;
            internal uint Key;
        }

        private ACIVSaveGame saveGame;

        public ACIV()
        {
            InitializeComponent();
            TitleID = FormID.AssassinsCreedIV;
#if INT2
            btnInjectData.Visible = true;
#endif
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            saveGame = new ACIVSaveGame(IO);

            

            /*var ids = new Dictionary<uint, string>
                {
                    {0x12F6B6F1, "Reals"},
                    {0x0EED0FEA, "Sugar"},
                    {0x00666D8D, "Rum"},
                    {0x87D8CEAB, "Cloth"},
                    {0x1F8D672A, "Wood"},
                    {0x654B51DC, "Metal"}

                };*/

            var ids = SettingAsString(30).Split(';').Select(s => s.Split(',')).ToDictionary(t => uint.Parse(t[0]), t => t[1]);

            var character = new Node("  Edward Kenway");

            foreach (KeyValuePair<uint, string> id in ids)
            {
                character.Nodes.Add(CreateIntNode(new CharacterValue { Character = ACIVSaveGame.Characters.Edward, Key = id.Key }, id.Value, 
                    id.Key == 0x12F6B6F1 ? 999999999 : 2500));
            }

            listInventory.Nodes.Add(character);

            if (saveGame.ObjectManager.Inventory.Count > 1)
            {
                character = new Node("  Adéwalé (Freedom Cry)");

                foreach (KeyValuePair<uint, string> id in ids)
                {
                    character.Nodes.Add(
                        CreateIntNode(new CharacterValue {Character = ACIVSaveGame.Characters.Adewale, Key = id.Key},
                                      id.Value,
                                      id.Key == 0x12F6B6F1 ? 999999999 : 2500));
                }

                listInventory.Nodes.Add(character);
            }
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
            if(sender == null)
                return;

            IntegerInput i = sender as IntegerInput;
            if(i == null)
                return;

            CharacterValue key = (CharacterValue)i.Tag;

            saveGame.ObjectManager.Inventory[(int)key.Character][key.Key].Value = i.Value;
            
        }

        private void BtnClick_InjectData(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() != DialogResult.OK)
                return;

            saveGame.ObjectManager = new ACIVSaveGame.ObjectDeserializer(new EndianIO(File.ReadAllBytes(ofd.FileName), EndianType.BigEndian, true));
        }
    }
}
