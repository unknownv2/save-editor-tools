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
using DevComponents.DotNetBar;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD.Controls
{
    public partial class CVXItemList : UserControl
    {
        private CodeVeronicaXSaveSlot _saveSlot;
        private List<CodeVeronicaXItemSlot> _items;

        internal CVXItemList(CodeVeronicaXSaveSlot saveSlot, CodeVeronicaXCharacterInventory inventory, string characterName)
        {
            _saveSlot = saveSlot;
            _items = inventory.Items;

            InitializeComponent();

            groupPanel1.Text = characterName;
            cmbSelectedItem.Items.AddRange(CodeVeronicaXData.ItemList.ToArray());
            cmbSelectedItem.SelectedIndex = 0;

            Display();
        }

        internal CVXItemList(CodeVeronicaXSaveSlot saveSlot,List<CodeVeronicaXItemSlot> itemList)
        {
            _saveSlot = saveSlot;
            _items = itemList;

            InitializeComponent();

            cmbSelectedItem.Items.AddRange(CodeVeronicaXData.ItemList.ToArray());
            cmbSelectedItem.SelectedIndex = 0;

            Display();
        }

        internal void Write()
        {
            foreach (Node node in treeItemList.Nodes)
            {
                var item = (node.Tag as CodeVeronicaXItemSlot);
                item.ItemCount = (ushort) (node.Cells[1].HostedControl as IntegerInput).Value;
                //item.IsInfinite = (node.Cells[2].HostedItem as CheckBoxItem).Checked;
            }
        }
        private void Display()
        {
            foreach (var item in _items)
            {
                var node = new Node(CodeVeronicaXData.ItemList[item.ItemId]) {Tag = item};
                //CreateComboBoxNode(node, item.ItemId);
                CreateIntegerNode(node, item.ItemId, item.ItemCount, 999);
                InsertBoolNode(node,item, item.IsInfinite);

                treeItemList.Nodes.Add(node);
            }
            treeItemList.Update();
        }
        /*
        private void CreateComboBoxNode(Node node, int id)
        {
            var comboBoxItem = new ComboBoxItem();
            comboBoxItem.Items.AddRange(CodeVeronicaXData.ItemList.ToArray());
            comboBoxItem.SelectedIndex = id;

            //node.HostedItem = comboBoxItem;
            node.Cells.Add(new Cell { HostedItem = comboBoxItem });
        }
        */
        private void CreateIntegerNode(Node node, int id,int value, int maxValue)
        {
            var integerInput = new IntegerInput();

            integerInput.Value = value;
            integerInput.MaxValue = maxValue;
            integerInput.ShowUpDown = true;

            node.Cells.Add(new Cell { HostedControl = integerInput });
        }

        private void InsertBoolNode(Node node, CodeVeronicaXItemSlot id, bool isChecked)
        {
            var ckInput = new CheckBoxItem {Tag = id};
            ckInput.Checked = isChecked;
            ckInput.CheckedChanged += ckInputIsInfinite_CheckedChanged;
            //var node = new Node { HostedItem = ckInput };
            node.Cells.Add(new Cell { HostedItem = ckInput });
            //node.Cells.Add(new Cell() {HostedControl = ckInput});
        }

        private void ckInputIsInfinite_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBoxItem)sender;

            // checks to make sure we don't have any problems
            if (checkBox.CheckState == CheckState.Indeterminate || checkBox.Tag == null || (checkBox.Tag as CodeVeronicaXItemSlot) == null)
                return;

            var item = (checkBox.Tag as CodeVeronicaXItemSlot);
            
            item.IsInfinite = checkBox.Checked;

            _saveSlot.Modified = true;
        }

        private void TreeItemList_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            if (e.Node != null || (e.Node.Tag as CodeVeronicaXItemSlot) != null)
            {
                cmbSelectedItem.SelectedIndex = (e.Node.Tag as CodeVeronicaXItemSlot).ItemId;
            }
        }

        private void cmbCurrentItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeItemList.SelectedNode == null || (treeItemList.SelectedNode.Tag as CodeVeronicaXItemSlot) == null)
                return;

            treeItemList.SelectedNode.Text = cmbSelectedItem.Text;

            var item = (treeItemList.SelectedNode.Tag as CodeVeronicaXItemSlot);
            item.ItemId = (byte) cmbSelectedItem.SelectedIndex;

            _saveSlot.Modified = true;
        }

        private void BtnClick_MaxAll(object sender, EventArgs e)
        {
            foreach (Node node in treeItemList.Nodes)
            {
                var item = (node.Tag as CodeVeronicaXItemSlot);
                // empty item, skip
                if(item.ItemId == 0) 
                    continue; 

                (node.Cells[1].HostedControl as IntegerInput).Value = int.MaxValue;
            }
        }

        private void BtnClick_MakeAllInfinite(object sender, EventArgs e)
        {
            foreach (Node node in treeItemList.Nodes)
            {
                var item = (node.Tag as CodeVeronicaXItemSlot);
                // empty item, skip
                if (item.ItemId == 0)
                    continue;

                (node.Cells[2].HostedItem as CheckBoxItem).Checked = !btnMakeAllInfinite.Checked;
            }
            btnMakeAllInfinite.Checked = !btnMakeAllInfinite.Checked;
        }
    }
}
