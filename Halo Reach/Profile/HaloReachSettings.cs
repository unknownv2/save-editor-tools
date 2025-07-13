using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using HaloReachClass;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.Halo_Reach
{
    public partial class HaloReachSettings : EditorControl
    {
        //public static readonly string FID = "4D53085X";
        private ReachSettings _settings;
        public HaloReachSettings()
        {
            InitializeComponent();
            TitleID = FormID.HaloReach;
#if INT2
            btnExtract.Visible = true;
#endif
        }
        public override bool Entry()
        {
            if (!OpenStfsFile(TitleID + ".gpd"))
                return false;
            try
            {
                _settings = new ReachSettings(IO);
                numCredits.Value = 0;
                DisplayUnlockables();
                return true;
            }
            catch
            {
                Functions.UI.messageBox(GPDSettingsNotFound[0], GPDSettingsNotFound[1], MessageBoxIcon.Warning);
            }
            return false;
        }

        public override void Save()
        {
            _settings.Save();
        }

        private void DisplayUnlockables()
        {
            advUnlockablesTree.Nodes.Clear();

            var armorNode = new Node("The Armory");
            armorNode.Nodes.Add(FillNodeFromEnumArray("Helmet", Enum.GetValues(typeof(ReachUnlocks.Helmet))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Left Shoulder", Enum.GetValues(typeof(ReachUnlocks.LeftShoulder))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Right Shoulder", Enum.GetValues(typeof(ReachUnlocks.RightShoulder))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Chest", Enum.GetValues(typeof(ReachUnlocks.Chest))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Wrist", Enum.GetValues(typeof(ReachUnlocks.Wrist))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Utility", Enum.GetValues(typeof(ReachUnlocks.Utility))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Visor Color", Enum.GetValues(typeof(ReachUnlocks.VisorColor))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Knee Guards", Enum.GetValues(typeof(ReachUnlocks.KneeGuards))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Armor Effect", Enum.GetValues(typeof(ReachUnlocks.ArmorEffect))));
            armorNode.Nodes.Add(FillNodeFromEnumArray("Firefight Voice", Enum.GetValues(typeof(ReachUnlocks.FirefightVoice))));

            var eliteArmor = new Node("Elite Armor");
            eliteArmor.Nodes.Add(FillNodeFromEnumArray("Armor Class", Enum.GetValues(typeof(ReachUnlocks.EliteArmor))));

            advUnlockablesTree.Nodes.Add(armorNode);
            advUnlockablesTree.Nodes.Add(eliteArmor);
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
                        Checked = _settings.ProfileUnlocks.IsUnlocked(item),
                        Tag = item
                    });
            }
            return itemNode;
        }
        private static void CheckAllChildNodes(Node treeNode, bool nodeChecked)
        {
            foreach (Node node in treeNode.Nodes)
            {
                if (node.Tag != null)
                    node.Checked = nodeChecked;

                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        private void AdvUnlockablesAfterCheck(object sender, AdvTreeCellEventArgs e)
        {
            if (e.Cell.Tag == null)
                return;

            var tag = (int)e.Cell.Tag;
            var isChecked = e.Cell.Checked;

            _settings.ProfileUnlocks.SetLock(tag, isChecked);
        }
        private void BtnClickUnlockAll(object sender, EventArgs e)
        {
            foreach (Node node in advUnlockablesTree.Nodes)
            {
                CheckAllChildNodes(node, true);
            }
            Functions.UI.messageBox("Unlocked all Spartan and Elite Armor!", "Success!", MessageBoxIcon.Information);
        }
        private void CmdMaxClick(object sender, EventArgs e)
        {
            _settings.ProfileSettings.Credits += (uint)this.numCredits.Value;
            numCredits.Value = 0;
        }

        private void BtnClickExtractProfileData(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog {FileName = "profile.bin"};
            if(sfd.ShowDialog() != DialogResult.OK)
                return;
            File.WriteAllBytes(sfd.FileName, _settings.Export());
        }
    }
}
