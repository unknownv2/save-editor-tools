using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Capcom;
using DevComponents.AdvTree;
using Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD.Controls;

namespace Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD
{
    public partial class CodeVeronicaX : EditorControl
    {
        private CodeVeronicaXSave _saveGame;
        private bool _reloadingSlot;

        public CodeVeronicaX()
        {
            InitializeComponent();
            TitleID = FormID.ResidetEvil_CVX;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("backupdata.txt"))
            {
                return false;
            }

            _saveGame = new CodeVeronicaXSave(IO);
            DisplaySave();

            return true;
        }

        public override void Save()
        {
            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcItemList.Tabs)
            {
                var obj = sti.AttachedControl.Controls[0] as CVXEditorContainer;
                if (obj == null)
                    continue;

                if (obj.EditorType == CodeVeronicaXEditorTypes.ItemList || obj.EditorType == CodeVeronicaXEditorTypes.ItemBox)
                {
                    obj.Write();
                }
            }

            _saveGame.Save();
        }

        private void DisplaySave()
        {
            for (int i = 0; i < _saveGame.SaveSlots.Count; i++)
            {
                var saveSlot = _saveGame.SaveSlots[i];

                if (saveSlot.IsEmpty) continue;

                var node = new Node(string.Format("Save Slot {0}", i)) {Tag = saveSlot};
                node.Nodes.Add(new Node(string.Format("Item Box {0}", i)) { Tag = CodeVeronicaXEditorTypes.ItemBox });
                /*
                var characterNode = new Node("Claire");
                foreach (var item in saveSlot.Items)
                {
                    characterNode.Nodes.Add(new Node(CodeVeronicaXData.ItemList[item.ItemId]) { Tag = item });
                }
                */
                treeSaveSlots.Nodes.Add(node);
            }
        }

        private void TreeSaveData_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            var tree = sender as AdvTree;
            if (tree == null)
                return;
            if (treeSaveSlots.SelectedNode.Tag == null)
                return;

            var superTabControl = stcItemList;
            DevComponents.DotNetBar.SuperTabItem sti = null;
            foreach (DevComponents.DotNetBar.SuperTabItem sti2 in superTabControl.Tabs)
            {
                if (sti2.Tag != tree.SelectedNode.Tag) continue;

                sti = sti2;
                sti.AttachedControl.Controls.Clear();

                break;
            }

            if (sti == null)
                sti = superTabControl.CreateTab(tree.SelectedNode.Text);

            var editorType = CodeVeronicaXEditorTypes.None;
            object cvxItem = treeSaveSlots.SelectedNode.Tag;
            if((treeSaveSlots.SelectedNode.Tag as CodeVeronicaXItemSlot) != null)
            {
                editorType = CodeVeronicaXEditorTypes.Item;
            }
            else if ((treeSaveSlots.SelectedNode.Tag as CodeVeronicaXSaveSlot) != null)
            {
                editorType = CodeVeronicaXEditorTypes.ItemList;
            }
            else if ((CodeVeronicaXEditorTypes)treeSaveSlots.SelectedNode.Tag == CodeVeronicaXEditorTypes.ItemBox)
            {
                editorType = CodeVeronicaXEditorTypes.ItemBox;
                cvxItem = treeSaveSlots.SelectedNode.Parent.Tag;
            }
            if(cvxItem == null)
                throw new Exception("Could not find a valid save slot.");

            var editor = new CVXEditorContainer(editorType, cvxItem) { Dock = DockStyle.Fill };
            sti.AttachedControl.Controls.Add(editor);
            sti.Tag = treeSaveSlots.SelectedNode.Tag;
            stcItemList.SelectedTab = sti;
        }

        private void TreeSaveData_AfterNodeDeselect(object sender, AdvTreeNodeEventArgs e)
        {
            // so we know we it's not an 'Item' node
            if (e.Node.Parent != null || _reloadingSlot)
                return;

            //var slotNode = e.Node;

            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcItemList.Tabs)
            {
                var obj = sti.AttachedControl.Controls[0] as CVXEditorContainer;
                if(obj == null)
                    continue;
                
                if (obj.EditorType == CodeVeronicaXEditorTypes.ItemList)
                {
                    obj.Write();
                }
            }
            var saveSlot = (e.Node.Tag as CodeVeronicaXSaveSlot);
            if(saveSlot == null)
                throw new Exception("Invalid slot class detected!");

            // no need to update the UI if the item list is not modified
            if (!saveSlot.Modified) return;

            _reloadingSlot = true;
            var slotNode = e.Node;
            // remove all old item nodes
            slotNode.Nodes.Clear();
            // add new item nodes
            /*
            foreach (var item in saveSlot.Items)
            {
                slotNode.Nodes.Add(new Node(CodeVeronicaXData.ItemList[item.ItemId]) { Tag = item });
            }
            */
            // close all opened tabs
            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcItemList.Tabs)
            {
                sti.Tag = null;
                sti.AttachedControl.Controls.Clear();
            }
            stcItemList.Tabs.Clear();

            _reloadingSlot = false;
        }
    }
}
