using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.AdvTree;
using NinjaGaiden;

namespace Horizon.PackageEditors.Ninja_Gaiden_3_RE
{
    public partial class NinjaGaiden3RE : EditorControl
    {
        private NinjaGaiden3RESave GameSave;

        public NinjaGaiden3RE()
        {
            InitializeComponent();
            TitleID = FormID.NinjaGaiden3RE;

            btnMaxKarmaRyu.Tag = intKarmaRyu;
            btnMaxKarmaAyane.Tag = intKarmaAyane;
            btnMaxKarmaScore.Tag = intKarmaScore;
        }

        public override bool Entry()
        {
            if ((!OpenStfsFile(0)))
                return false;

            GameSave = new NinjaGaiden3RESave(IO);

            intKarmaRyu.Value = GameSave.KarmaCurrentRyu;
            intKarmaAyane.Value = GameSave.KarmaCurrentAyane;
            intKarmaScore.Value = GameSave.KarmaScore;

            treeUnlocks.Nodes.Add(FillNodeFromEnumArray("Ryu", Enum.GetValues(typeof (NinjaGaiden3SkillRyu))));

            treeUnlocks.Nodes.Add(FillNodeFromEnumArray("Ayane", Enum.GetValues(typeof (NinjaGaiden3SkillsAyane))));

            return true;
        }

        private Node FillNodeFromEnumArray(string nodeText, Array enumArray)
        {
            var itemNode = new Node(nodeText);
            foreach (var item in enumArray)
            {
                itemNode.Nodes.Add(
                    new Node(Regex.Replace(Regex.Replace(item.ToString(), "([a-z])([A-Z])", "$1 $2"),
                                           "(?<=[0-9])(?=[A-Za-z])|(?<=[A-Za-z])(?=[0-9])", " "))
                    {
                        CheckBoxVisible = true,
                        Checked = GameSave.UnlockManager.IsUnlocked(item),
                        Tag = item
                    });
            }
            return itemNode;
        }

        public override void Save()
        {
            GameSave.KarmaCurrentRyu = intKarmaRyu.Value;
            GameSave.KarmaCurrentAyane = intKarmaAyane.Value;
            GameSave.KarmaScore = intKarmaScore.Value;

            GameSave.Save();
        }

        private void UnlockTreeNodeAfterCheck(object sender, AdvTreeCellEventArgs e)
        {
            if (sender == null || e.Cell.Tag == null) return;

            GameSave.UnlockManager.SetLock((int)e.Cell.Tag, e.Cell.Checked);
        }

        private void BtnClickUnlockAll(object sender, EventArgs e)
        {
            foreach (Node node in treeUnlocks.Nodes)
            {
                CheckAllChildNodes(node, true);
            }
            Functions.UI.messageBox("Unlocked all Skills!", "Success!", MessageBoxIcon.Information);
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
   
        private void BtnClickMaxKarma(object sender, EventArgs e)
        {
            if (sender == null) return;
            var input = (sender as DevComponents.DotNetBar.ButtonX).Tag as DevComponents.Editors.IntegerInput;
            if(input == null) return;
            input.Value = input.MaxValue;
        }

    }
}
