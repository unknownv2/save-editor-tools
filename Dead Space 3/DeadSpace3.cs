using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DeadSpace;

namespace Horizon.PackageEditors.Dead_Space_3
{
    public partial class DeadSpace3 : EditorControl
    {
        private struct Item
        {
            internal Guid Id;
            internal string Name;
        }

        //public static readonly string FID = "4541099D";
        private DeadSpace3Save _gameSave;
        private List<Item> InventoryItems;
        private List<Item> Suits; 
        private DeadSpace3Save.InventoryItem CurrentItem;
        private int LastSelectedItemIndex;
        public DeadSpace3()
        {
            InitializeComponent();
            TitleID = FormID.DeadSpace3;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            _gameSave = new DeadSpace3Save(IO);
            DisplaySaveData();
#if INT22
            InitializeInventoryItems();
            InitializeSuitList();
            ribbonTabItem1.Visible = ribbonTabItem2.Visible = btnInject.Visible = true;
#endif
            return true;
        }

        public override void Save()
        {
            // retrieve new stats
            _gameSave.Tungsten = intTungsten.Value;
            _gameSave.Semiconductor = intSemiconductor.Value;
            _gameSave.ScrapMetal = intScrapMetal.Value;
            _gameSave.SomaticGel = intSomaticGel.Value;
            _gameSave.Transducer = intTransducer.Value;
            _gameSave.RationSeals = intRationSeals.Value;

            _gameSave.OpenSlotCount = intSlotCount.Value;
            // create the new inventory

            // update last inventory item
            //UpdateCurrentInventoryItem();
            // set the equipped suit
            //UpdateCurrentlyEquippedSuit();
            // final save call
            _gameSave.Save();
        }

        private void DisplaySaveData()
        {
            // resources
            intTungsten.Value = _gameSave.Tungsten;
            intSemiconductor.Value = _gameSave.Semiconductor;
            intScrapMetal.Value = _gameSave.ScrapMetal;
            intSomaticGel.Value = _gameSave.SomaticGel;
            intTransducer.Value = _gameSave.Transducer;
            intRationSeals.Value = _gameSave.RationSeals;

            intSlotCount.Value = _gameSave.OpenSlotCount;
#if INT22
            // inventory
            foreach (var item in _gameSave.RigInventory)
            {
                inventoryListiew.Items.Add(new ListViewItem
                                               {
                                                   Text = InventoryItems.Find(inv => inv.Id == item.Guid).Name,
                                                   Tag = item.Guid
                                               });
            }
            // suits collected
            foreach (var suit in _gameSave.SafeInventory)
            {
                var name = Suits.Find(st => st.Id == suit.Guid).Name;
                if (string.IsNullOrEmpty(name)) continue;
                
                suitListView.Items.Add(new ListViewItem
                                           {
                                               Text = name,
                                               Tag = suit
                                           });
            }
            // current suit
            txtEquippedSuit.Text = cmbSuits.Text = Suits.Find(suit => suit.Id == _gameSave.EquippedSuit).Name;
#endif
        }

        private void InitializeInventoryItems()
        {
            cmbInvItem.Items.Clear();
            inventoryListiew.Items.Clear();
            LastSelectedItemIndex = -1;
            InventoryItems = new List<Item>();
#if !INT2
            TextReader reader = new StreamReader(new MemoryStream(new WebClient().DownloadData("RigInventory.txt"), false));
#else
            TextReader reader = new StreamReader(@"E:\Game Projects\Dead Space 3\Saves\InventoryList.txt");
#endif
            string line;
            var names = new List<string>();
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string guid = Regex.Match(line, "(?<=\").*?(?=\")").Value;
                string name = line.Substring(guid.Length + 3);
                names.Add(name);
                InventoryItems.Add(new Item {Id = new Guid(guid), Name = name});
            }
            reader.Close();
            cmbInvItem.Items.AddRange(names.ToArray());
        }

        private void InitializeSuitList()
        {
            Suits = new List<Item>();
#if !INT2
            TextReader reader = new StreamReader(new MemoryStream(new WebClient().DownloadData("SuitList.txt"), false));
#else
            TextReader reader = new StreamReader(@"E:\Game Projects\Dead Space 3\Saves\SuitList.txt");
#endif
            string line;
            var names = new List<string>();
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string guid = Regex.Match(line, "(?<=\").*?(?=\")").Value;
                string name = line.Substring(guid.Length + 3);
                names.Add(name);
                Suits.Add(new Item { Id = new Guid(guid), Name = name });
            }
            reader.Close();
            cmbSuits.Items.AddRange(names.ToArray());
        }

        private void InventoryListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (inventoryListiew.SelectedItems.Count < 1)
                return;

            if (inventoryListiew.SelectedItems.Count > 1)
                throw new Exception("Dead Space 3: invalid amount of items selected from the inventory for editing.");

            // update selected entry before loading new one
            UpdateCurrentInventoryItem();

            var selectedItem = inventoryListiew.SelectedItems[0];

            // load the new inventory item
            CurrentItem = _gameSave.RigInventory.Find(item => item.Guid == (Guid) (selectedItem.Tag));
            cmbInvItem.Text = selectedItem.Text;
            intInvItemCount.Value = CurrentItem.Count;
        }

        void UpdateCurrentInventoryItem()
        {
            if(CurrentItem == null)
                return;

            switch (cmbInvItem.Text) // item name
            {
                case "None":
                    inventoryListiew.Items.RemoveAt(LastSelectedItemIndex);
                    _gameSave.RigInventory.Remove(CurrentItem);
                    CurrentItem = null;
                    break;

                default:
                    CurrentItem.Guid = InventoryItems.Find(item => item.Name == cmbInvItem.Text).Id;
                    CurrentItem.Count = intInvItemCount.Value;
                    LastSelectedItemIndex = inventoryListiew.SelectedIndices[0];
                    break;
            }
        }

        void UpdateCurrentSuit()
        {
            if (CurrentItem == null)
                return;

            switch (cmbInvItem.Text) // item name
            {
                case "None":
                    inventoryListiew.Items.RemoveAt(LastSelectedItemIndex);
                    _gameSave.RigInventory.Remove(CurrentItem);
                    CurrentItem = null;
                    break;

                default:
                    CurrentItem.Guid = InventoryItems.Find(item => item.Name == cmbInvItem.Text).Id;
                    CurrentItem.Count = intInvItemCount.Value;
                    LastSelectedItemIndex = inventoryListiew.SelectedIndices[0];
                    break;
            }
        }

        void UpdateCurrentlyEquippedSuit()
        {
            if(!string.IsNullOrEmpty(cmbSuits.SelectedItem as string))
                _gameSave.EquippedSuit = Suits.Find(suit => suit.Name == cmbSuits.SelectedItem as string).Id;
        }

        void CmbSuitItemChanged(object sender, EventArgs e)
        {
            
        }

        void BtnEquipSuit(object sender, EventArgs e)
        {
            UpdateCurrentlyEquippedSuit();
            txtEquippedSuit.Text = cmbSuits.Text;
        }

        void BtnAddSuit(object sender, EventArgs e)
        {
            var item = new DeadSpace3Save.InventoryItem
            {
                Count = 0x01,
                Guid = Suits.Find(t => t.Name == cmbSuits.Text).Id,
                Unknown1 = false,
                Unknown2 = new byte[] { 0x58, 0x31, 0x38, 0x00, 0x00, 0x00, 0x00 }
            };
            _gameSave.SafeInventory.Add(item);
            suitListView.Items.Add(new ListViewItem
                {
                    Text = Suits.Find(inv => inv.Id == item.Guid).Name,
                    Tag = item.Guid
                });
        }

        void BtnAddNewInventoryItem(object sender, EventArgs e)
        {
            var item = new DeadSpace3Save.InventoryItem
                                        {
                                            Count = intInvItemCount.Value,
                                            Guid = InventoryItems.Find(t => t.Name == cmbInvItem.Text).Id,
                                            Unknown1 = false
                                        };

            _gameSave.RigInventory.Add(item);
            inventoryListiew.Items.Add(new ListViewItem
                                           {
                                               Text = InventoryItems.Find(inv => inv.Id == item.Guid).Name,
                                               Tag = item.Guid
                                           });
        }

        private void BtnTungstenMaxClick(object sender, EventArgs e)
        {
            intTungsten.Value = intTungsten.MaxValue;
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            intSemiconductor.Value = intSemiconductor.MaxValue;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            intScrapMetal.Value = intScrapMetal.MaxValue;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            intSomaticGel.Value = intSomaticGel.MaxValue;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            intTransducer.Value = intTransducer.MaxValue;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            intRationSeals.Value = intRationSeals.MaxValue;
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            //_gameSave.WriteToFile(new SaveBlockManager(new EndianReader(File.ReadAllBytes(ofd.FileName),EndianType.BigEndian)).Export());

            /*
            Package.StfsContentPackage.InjectFileFromArray("ds3_v1_slot_01", IO.ToArray());

            IO.Close();

            if (!OpenStfsFile(0))
                return;

            _gameSave = new DeadSpace3Save(IO);
            */
        }
    }
}