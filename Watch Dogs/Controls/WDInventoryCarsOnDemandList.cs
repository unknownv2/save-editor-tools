using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using FarCry;
using WatchDogs;

namespace Horizon.PackageEditors.Watch_Dogs.Controls
{
    public partial class WDInventoryCarsOnDemandList : UserControl
    {
        private readonly WatchDogsSaveEntry _carOnDemandDogsSaveEntry;
        private readonly WatchDogsSave _saveGame;
        internal WDInventoryCarsOnDemandList(WatchDogsSave saveGame)
        {
            InitializeComponent();

            _saveGame = saveGame;
            _carOnDemandDogsSaveEntry = _saveGame.GetSaveEntry(_saveGame.MainEntry,
                FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\CarsOnDemand\Cars")),
                string.Empty);

            LoadList();
        }

        private void LoadList()
        {
            foreach (var car in _carOnDemandDogsSaveEntry.Children)
            {
                var itemId = _saveGame.GetEntryData(car.Attributes[0x4573FB22]).ToUInt32(false);

                string itemName = WatchDogsData.Items.ContainsKey(itemId) ? WatchDogsData.Items[itemId] : itemId.ToString("X");

                var ckInput = new CheckBoxItem();

                byte[] data = _saveGame.GetEntryData(car.Attributes[0x4535EF9B]);
                if (data.Length > 0)
                    ckInput.Checked = (data[0x00] == 0x02);

                ckInput.CheckedChanged += UnlockCar_CheckedChanged;
                ckInput.Text = itemName;
                ckInput.Tag = car;
                var node = new Node { HostedItem = ckInput };
                
                treeCarList.Nodes.Add(node);
            }
            treeCarList.Nodes.Sort(new NodeSorter());
        }

        private void BtnClick_UnlockAllCarsOnDemand(object sender, EventArgs e)
        {
            foreach (var car in _carOnDemandDogsSaveEntry.Children)
            {
                _saveGame.SetItemAttribute(car, 0x02, 0x4535EF9B, fValue.ObjectAttributes.Byte);
            }

            foreach (Node node in treeCarList.Nodes)
            {
                if ((node.HostedItem as CheckBoxItem) != null)
                    (node.HostedItem as CheckBoxItem).Checked = true;
            }
        }

        private void UnlockCar_CheckedChanged(object sender, EventArgs e)
        {
            var ckInput = (sender as CheckBoxItem);
            if (ckInput == null || ckInput.Tag == null)
                return;

            var car = (ckInput.Tag as WatchDogsSaveEntry);
            if (car == null)
                return;

            _saveGame.SetItemAttribute(car, ckInput.Checked ? 0x02: 0x00, 0x4535EF9B, fValue.ObjectAttributes.Byte);
        }

        public class NodeSorter : IComparer
        {
            // Compare the length of the strings, or the strings 
            // themselves, if they are the same length. 
            public int Compare(object x, object y)
            {
                CheckBoxItem tx = (x as Node).HostedItem as CheckBoxItem;
                CheckBoxItem ty = (y as Node).HostedItem as CheckBoxItem;

                // If they are the same length, call Compare. 
                return string.Compare(tx.Text, ty.Text);
            }
        }
    }
}
