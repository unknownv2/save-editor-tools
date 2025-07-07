using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Oblivion;
using Horizon.Functions;

namespace Horizon.PackageEditors.Oblivion
{
    public partial class Oblivion : EditorControl
    {
        //public static readonly string FID = "425307D1";
        public Oblivion()
        {
            InitializeComponent();
            TitleID = FormID.Oblivion;
            ofd.Filter = "Oblivion Saves|*";
            SearchTimer.Interval = 1800;
            SearchTimer.Tick += new System.EventHandler(SearchTimer_Tick);
            
        }

        public override void Initialize()
        {
            
        }

        bool isPCSave;
        OpenFileDialog ofd = new OpenFileDialog();
        public override bool Entry()
        {
            isPCSave = false;
            return OpenStfsFile("gamedata.dat") && loadSave();
        }

        private void cmdLoadPCSave_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                isPCSave = true;
                enablePanels(loadSave());
            }
        }

        OblivionSave XSave;
        OblivionSave.SaveRecord.PlayerRecord.InventoryItem Gold;
        OblivionSave.SaveRecord.PlayerRecord.InventoryItem Lockpicks;
        private bool loadSave()
        {
            if (isPCSave)
            {
                IO = new EndianIO(ofd.FileName, EndianType.LittleEndian);
                IO.Open();
                if (IO.In.ReadAsciiString(12) != "TES4SAVEGAME")
                {
                    IO.Close();
                    return false;
                }
                setPackageOverride(ofd.FileName);
            }
            else
                IO.EndianType = EndianType.LittleEndian;
            tabMain.Select();
            tabClick(tabMain, null);
            rbPackageEditor.Refresh();
            ItemList.Items.Clear();
            try
            {
                XSave = new OblivionSave();
                XSave.LoadSave(IO);
                pbThumbnail.Image = XSave.GetThumbnail();
                AddToInventory((uint)OblivionSave.FormID.Gold);
                Gold = XSave.GetInventoryItemFromFormID((uint)OblivionSave.FormID.Gold);
                intGold.Value = (decimal)Gold.NumberOfStackedItems;
                AddToInventory((uint)OblivionSave.FormID.Lockpick);
                Lockpicks = XSave.GetInventoryItemFromFormID((uint)OblivionSave.FormID.Lockpick);
                intLockpicks.Value = (decimal)Lockpicks.NumberOfStackedItems;
                intLocationID.Value = (decimal)XSave.Globals.PlayerPosition.FormIDOfCell;
                intPlayerX.Value = (decimal)XSave.Globals.PlayerPosition.PlayerX;
                intPlayerY.Value = (decimal)XSave.Globals.PlayerPosition.PlayerY;
                intPlayerZ.Value = (decimal)XSave.Globals.PlayerPosition.PlayerZ;
                loadPlayerRecord();
                loadPlayerAttributes();
                populateInventory();
            }
            catch
            {
                UI.errorBox("An error has occured while loading your save!\n\nMake sure you are at least out of the sewers in the game!");
                IO.Close();
                return false;
            }
            return true;
        }

        private void AddToInventory(uint FormID)
        {
            if (!XSave.InventoryItemExists(FormID))
            {
                Array.Resize(ref XSave.FormIDs, XSave.FormIDs.Length + 1);
                XSave.FormIDs[XSave.FormIDs.Length - 1] = FormID;
                OblivionSave.SaveRecord.PlayerRecord Record = XSave.GetPlayerRecord();
                Array.Resize(ref Record.InventoryItems, Record.InventoryItems.Length + 1);
                Record.InventoryItems[Record.InventoryItems.Length - 1].Delete = false;
                Record.InventoryItems[Record.InventoryItems.Length - 1].NumberOfStackedItems = 1;
                Record.InventoryItems[Record.InventoryItems.Length - 1].NumberOfItemsWithChangedProperties = 0;
                Record.InventoryItems[Record.InventoryItems.Length - 1].IRef = (uint)XSave.FormIDs.Length - 1;
                XSave.SetPlayerRecord(Record);
            }
        }

        private void AddItemToInventory(uint FormID)
        {
            AddToInventory(FormID);
            populateInventory();
        }

        private void populateInventory()
        {
            ItemGroupList.Items.Clear();
            AddItemList.Items.Clear();
            OblivionSave.SaveRecord.PlayerRecord PlayerRecord = XSave.GetPlayerRecord();
            foreach (OblivionSave.SaveRecord.PlayerRecord.InventoryItem Item in PlayerRecord.InventoryItems)
            {
                string ItemName;
                try
                {
                    ItemName = FixItemName(XSave.GetInventoryItemName(Item.IRef));
                }
                catch { ItemName = "0x" + Item.IRef.ToString("X"); }
                if (Item.NumberOfStackedItems != 4294967295 && ItemName != "Gold" && ItemName != "Lockpick" && !Item.Delete)
                {
                    ListViewItem li = new ListViewItem(ItemName);
                    li.SubItems.Add(Item.IRef.ToString());
                    ItemGroupList.Items.Add(li);
                }
            }
            foreach (string ItemName in Enum.GetNames(typeof(OblivionSave.FormID)))
            {
                bool Added = false;
                string NewItemName = FixItemName(ItemName);
                for (int x = 0; x < ItemGroupList.Items.Count; x++)
                    if (ItemGroupList.Items[0].Text == NewItemName || NewItemName == "Gold" || NewItemName == "Lockpick")
                        Added = true;
                if (!Added)
                {
                    ListViewItem li = new ListViewItem(NewItemName);
                    li.SubItems.Add(((uint)Enum.Parse(typeof(OblivionSave.FormID), ItemName)).ToString());
                    AddItemList.Items.Add(li);
                }
            }
        }

        private string FixItemName(string Item)
        {
            return System.Text.RegularExpressions.Regex.Replace(Item, "[_]{2,}", String.Empty).Replace('_', ' ');
        }

        private void loadPlayerRecord()
        {
            OblivionSave.SaveRecord.PlayerRecord PlayerRecord = XSave.GetPlayerRecord();
            colorHair.SelectedColor = Color.FromArgb(255, PlayerRecord.Attributes.Hair.Color.R, PlayerRecord.Attributes.Hair.Color.G, PlayerRecord.Attributes.Hair.Color.B);
            txtName.Text = PlayerRecord.Attributes.Name;
            stat1.Value = PlayerRecord.Attributes.Stats[0];
            stat2.Value = PlayerRecord.Attributes.Stats[2];
            stat3.Value = PlayerRecord.Attributes.Stats[3];
            stat4.Value = PlayerRecord.Attributes.Stats[4];
            stat5.Value = PlayerRecord.Attributes.Stats[5];
            stat6.Value = PlayerRecord.Attributes.Stats[6];
            stat7.Value = PlayerRecord.Attributes.Stats[7];
            stat15.Value = PlayerRecord.Attributes.Stats[8];
            stat8.Value = PlayerRecord.Attributes.Stats[9];
            stat9.Value = PlayerRecord.Attributes.Stats[10];
            stat10.Value = PlayerRecord.Attributes.Stats[11];
            stat11.Value = PlayerRecord.Attributes.Stats[12];
            stat12.Value = PlayerRecord.Attributes.Stats[13];
            stat13.Value = PlayerRecord.Attributes.Stats[14];
            stat14.Value = PlayerRecord.Attributes.Stats[15];
            stat20.Value = PlayerRecord.Attributes.Stats[16];
            stat16.Value = PlayerRecord.Attributes.Stats[17];
            stat17.Value = PlayerRecord.Attributes.Stats[18];
            stat18.Value = PlayerRecord.Attributes.Stats[19];
            stat19.Value = PlayerRecord.Attributes.Stats[23];
            stat21.Value = PlayerRecord.Attributes.Stats[26];
        }

        OblivionSave.SaveRecord.PlayerAttributesRecord PlayerAttributesRecord;
        private void loadPlayerAttributes()
        {
            PlayerAttributesRecord = XSave.GetPlayerAttributesRecord();
            if (XSave.GameHeader.Level > intLevel.Maximum)
                intLevel.Value = intLevel.Maximum;
            else
                intLevel.Value = XSave.GameHeader.Level;
            int1.Value = PlayerAttributesRecord.Attributes[0];
            int2.Value = PlayerAttributesRecord.Attributes[1];
            int3.Value = PlayerAttributesRecord.Attributes[2];
            int4.Value = PlayerAttributesRecord.Attributes[3];
            int5.Value = PlayerAttributesRecord.Attributes[4];
            int6.Value = PlayerAttributesRecord.Attributes[5];
            int7.Value = PlayerAttributesRecord.Attributes[6];
            int8.Value = PlayerAttributesRecord.Attributes[7];
            num1.Value = PlayerAttributesRecord.Skills[0];
            num2.Value = PlayerAttributesRecord.Skills[1];
            num3.Value = PlayerAttributesRecord.Skills[2];
            num4.Value = PlayerAttributesRecord.Skills[3];
            num5.Value = PlayerAttributesRecord.Skills[4];
            num6.Value = PlayerAttributesRecord.Skills[5];
            num7.Value = PlayerAttributesRecord.Skills[6];
            num8.Value = PlayerAttributesRecord.Skills[7];
            num9.Value = PlayerAttributesRecord.Skills[8];
            num10.Value = PlayerAttributesRecord.Skills[9];
            num11.Value = PlayerAttributesRecord.Skills[10];
            num12.Value = PlayerAttributesRecord.Skills[11];
            num13.Value = PlayerAttributesRecord.Skills[12];
            num14.Value = PlayerAttributesRecord.Skills[13];
            num15.Value = PlayerAttributesRecord.Skills[14];
            num16.Value = PlayerAttributesRecord.Skills[15];
            num17.Value = PlayerAttributesRecord.Skills[16];
            num18.Value = PlayerAttributesRecord.Skills[17];
            num19.Value = PlayerAttributesRecord.Skills[18];
            num20.Value = PlayerAttributesRecord.Skills[19];
            num21.Value = PlayerAttributesRecord.Skills[20];
        }

        private void writePlayerAttributes()
        {
            PlayerAttributesRecord.BaseData.Level = (short)intLevel.Value;
            PlayerAttributesRecord.Attributes[0] = (byte)int1.Value;
            PlayerAttributesRecord.Attributes[1] = (byte)int2.Value;
            PlayerAttributesRecord.Attributes[2] = (byte)int3.Value;
            PlayerAttributesRecord.Attributes[3] = (byte)int4.Value;
            PlayerAttributesRecord.Attributes[4] = (byte)int5.Value;
            PlayerAttributesRecord.Attributes[5] = (byte)int6.Value;
            PlayerAttributesRecord.Attributes[6] = (byte)int7.Value;
            PlayerAttributesRecord.Attributes[7] = (byte)int8.Value;
            PlayerAttributesRecord.Skills[0] = (byte)num1.Value;
            PlayerAttributesRecord.Skills[1] = (byte)num2.Value;
            PlayerAttributesRecord.Skills[2] = (byte)num3.Value;
            PlayerAttributesRecord.Skills[3] = (byte)num4.Value;
            PlayerAttributesRecord.Skills[4] = (byte)num5.Value;
            PlayerAttributesRecord.Skills[5] = (byte)num6.Value;
            PlayerAttributesRecord.Skills[6] = (byte)num7.Value;
            PlayerAttributesRecord.Skills[7] = (byte)num8.Value;
            PlayerAttributesRecord.Skills[8] = (byte)num9.Value;
            PlayerAttributesRecord.Skills[9] = (byte)num10.Value;
            PlayerAttributesRecord.Skills[10] = (byte)num11.Value;
            PlayerAttributesRecord.Skills[11] = (byte)num12.Value;
            PlayerAttributesRecord.Skills[12] = (byte)num13.Value;
            PlayerAttributesRecord.Skills[13] = (byte)num14.Value;
            PlayerAttributesRecord.Skills[14] = (byte)num15.Value;
            PlayerAttributesRecord.Skills[15] = (byte)num16.Value;
            PlayerAttributesRecord.Skills[16] = (byte)num17.Value;
            PlayerAttributesRecord.Skills[17] = (byte)num18.Value;
            PlayerAttributesRecord.Skills[18] = (byte)num19.Value;
            PlayerAttributesRecord.Skills[19] = (byte)num20.Value;
            PlayerAttributesRecord.Skills[20] = (byte)num21.Value;
            XSave.SetPlayerAttributesRecord(PlayerAttributesRecord);
        }

        private void writePlayerRecord()
        {
            OblivionSave.SaveRecord.PlayerRecord PlayerRecord = XSave.GetPlayerRecord();
            PlayerRecord.Attributes.Hair.Color.R = colorHair.SelectedColor.R;
            PlayerRecord.Attributes.Hair.Color.G = colorHair.SelectedColor.G;
            PlayerRecord.Attributes.Hair.Color.B = colorHair.SelectedColor.B;
            PlayerRecord.Attributes.Name = txtName.Text;
            PlayerRecord.Attributes.Stats[0] = (uint)stat1.Value;
            PlayerRecord.Attributes.Stats[2] = (uint)stat2.Value;
            PlayerRecord.Attributes.Stats[3] = (uint)stat3.Value;
            PlayerRecord.Attributes.Stats[4] = (uint)stat4.Value;
            PlayerRecord.Attributes.Stats[5] = (uint)stat5.Value;
            PlayerRecord.Attributes.Stats[6] = (uint)stat6.Value;
            PlayerRecord.Attributes.Stats[7] = (uint)stat7.Value;
            PlayerRecord.Attributes.Stats[8] = (uint)stat15.Value;
            PlayerRecord.Attributes.Stats[9] = (uint)stat8.Value;
            PlayerRecord.Attributes.Stats[10] = (uint)stat9.Value;
            PlayerRecord.Attributes.Stats[11] = (uint)stat10.Value;
            PlayerRecord.Attributes.Stats[12] = (uint)stat11.Value;
            PlayerRecord.Attributes.Stats[13] = (uint)stat12.Value;
            PlayerRecord.Attributes.Stats[14] = (uint)stat13.Value;
            PlayerRecord.Attributes.Stats[15] = (uint)stat14.Value;
            PlayerRecord.Attributes.Stats[16] = (uint)stat20.Value;
            PlayerRecord.Attributes.Stats[17] = (uint)stat16.Value;
            PlayerRecord.Attributes.Stats[18] = (uint)stat17.Value;
            PlayerRecord.Attributes.Stats[19] = (uint)stat18.Value;
            PlayerRecord.Attributes.Stats[23] = (uint)stat19.Value;
            PlayerRecord.Attributes.Stats[26] = (uint)stat21.Value;
            XSave.SetPlayerRecord(PlayerRecord);
        }

        public override void Save()
        {
            XSave.GameHeader.Level = (short)intLevel.Value;
            Lockpicks.NumberOfStackedItems = (uint)intLockpicks.Value;
            XSave.WriteInventoryItem(Lockpicks);
            Gold.NumberOfStackedItems = (uint)intGold.Value;
            XSave.WriteInventoryItem(Gold);
            XSave.GameHeader.CharacterName = txtName.Text;
            writePlayerAttributes();
            XSave.Globals.PlayerPosition.FormIDOfCell = (uint)intLocationID.Value;
            XSave.Globals.PlayerPosition.PlayerX = (float)intPlayerX.Value;
            XSave.Globals.PlayerPosition.PlayerY = (float)intPlayerY.Value;
            XSave.Globals.PlayerPosition.PlayerZ = (float)intPlayerZ.Value;
            writePlayerRecord();
            if (isPCSave)
            {
                IO.Close();
                EndianIO PCIO = XSave.WriteSave();
                File.Delete(ofd.FileName);
                EndianIO NewPCIO = new EndianIO(ofd.FileName, EndianType.LittleEndian);
                NewPCIO.Open();
                NewPCIO.Out.Write(PCIO.In.ReadBytes((int)PCIO.Stream.Length));
                NewPCIO.Close();
                PCIO.Close();
            }
            else
            {
                EndianIO xIO = XSave.WriteSave();
                IO.Stream.Position = 0;
                IO.Out.Write(xIO.In.ReadBytes((int)xIO.Stream.Length));
                xIO.Close();
            }
        }

        private bool Working = false;
        private OblivionSave.SaveRecord.PlayerRecord.InventoryItem CurrentItem;
        private void ItemGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Working = true;
            try
            {
                ItemList.Items.Clear();
                CurrentItem = XSave.GetInventoryItem(uint.Parse(ItemGroupList.SelectedItems[0].SubItems[1].Text));
                for (int x = 0; x < ((CurrentItem.NumberOfStackedItems == 0) ? 1 : CurrentItem.NumberOfStackedItems); x++)
                {
                    ListViewItem li = new ListViewItem(x.ToString());
                    li.SubItems.Add(ItemGroupList.SelectedItems[0].SubItems[0].Text);
                    if (x < CurrentItem.NumberOfItemsWithChangedProperties)
                    {
                        li.SubItems.Add(x.ToString());
                        if (!CurrentItem.MasterProperties[x].Delete)
                            ItemList.Items.Add(li);
                    }
                    else
                    {
                        li.SubItems.Add("N");
                        ItemList.Items.Add(li);
                    }
                }
                intAddItems.Value = 0;
                intAddItems.Maximum = (CurrentItem.NumberOfStackedItems == 0) ? 4294967294 : 4294967295 - CurrentItem.NumberOfStackedItems;
                ItemList.Enabled = true;
            }
            catch { ItemList.Enabled = false; }
            intItemHealth.Value = 0;
            intAddItems.Value = 0;
            intItemHealth.Enabled = false;
            Working = false;
        }

        private void cmdAddItem_Click(object sender, EventArgs e)
        {
            try { AddItemToInventory(uint.Parse(AddItemList.SelectedItems[0].SubItems[1].Text)); }
            catch { }
        }

        private void cmdAddItemID_Click(object sender, EventArgs e)
        {
            try { AddItemToInventory((uint)intItemID.Value); }
            catch { }
        }

        private Timer SearchTimer = new Timer();
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchTimer.Stop();
            SearchTimer.Start();
        }

        private void cmdDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ItemList.Items.Clear();
                ItemList.Enabled = false;
                OblivionSave.SaveRecord.PlayerRecord.InventoryItem Item = XSave.GetInventoryItem(uint.Parse(ItemGroupList.SelectedItems[0].SubItems[1].Text));
                Item.Delete = true;
                XSave.WriteInventoryItem(Item);
                populateInventory();
            }
            catch { }
        }

        private void intItemHealth_ValueChanged(object sender, EventArgs e)
        {
            if (Working) return;
            if (ItemList.SelectedItems[0].SubItems[2].Text != "N")
            {
                bool HealthFound = false;
                for (int x = 0; x < CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties.Length; x++)
                {
                    if (CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties[x].Type == OblivionSave.SaveRecord.PlayerRecord.PropertyType.ItemHealth)
                    {
                        CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties[x].ItemHealth = Convert.ToSingle(intItemHealth.Value);
                        HealthFound = true;
                    }
                }
                if (!HealthFound)
                {
                    Array.Resize(ref CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties, CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties.Length + 1);
                    CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties[CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties.Length - 1].Type = OblivionSave.SaveRecord.PlayerRecord.PropertyType.ItemHealth;
                    CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties[CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties.Length - 1].ItemHealth = Convert.ToSingle(intItemHealth.Value);
                }
            }
            else
            {
                if (CurrentItem.NumberOfItemsWithChangedProperties == 0)
                {
                    CurrentItem.NumberOfItemsWithChangedProperties = 1;
                    CurrentItem.MasterProperties = new OblivionSave.SaveRecord.PlayerRecord.MasterProperty[1];
                    CurrentItem.MasterProperties[0].SlaveProperties = new OblivionSave.SaveRecord.PlayerRecord.SlaveProperty[1];
                    CurrentItem.MasterProperties[0].SlaveProperties[0].Type = OblivionSave.SaveRecord.PlayerRecord.PropertyType.ItemHealth;
                    CurrentItem.MasterProperties[0].SlaveProperties[0].ItemHealth = Convert.ToSingle(intItemHealth.Value);
                }
                else
                {
                    CurrentItem.NumberOfItemsWithChangedProperties++;
                    Array.Resize(ref CurrentItem.MasterProperties, CurrentItem.MasterProperties.Length + 1);
                    CurrentItem.MasterProperties[CurrentItem.MasterProperties.Length - 1].SlaveProperties = new OblivionSave.SaveRecord.PlayerRecord.SlaveProperty[1];
                    CurrentItem.MasterProperties[CurrentItem.MasterProperties.Length - 1].SlaveProperties[0].Type = OblivionSave.SaveRecord.PlayerRecord.PropertyType.ItemHealth;
                    CurrentItem.MasterProperties[CurrentItem.MasterProperties.Length - 1].SlaveProperties[0].ItemHealth = Convert.ToSingle(intItemHealth.Value);
                }
                ItemList.SelectedItems[0].SubItems[2].Text = (CurrentItem.MasterProperties.Length - 1).ToString();
            }
            XSave.WriteInventoryItem(CurrentItem);
        }

        private void cmdAddItems_Click(object sender, EventArgs e)
        {
            if (!ItemList.Enabled)
                return;
            try
            {
                CurrentItem.NumberOfStackedItems += (uint)intAddItems.Value;
                intAddItems.Maximum = (CurrentItem.NumberOfStackedItems == 0) ? 4294967294 : 4294967295 - CurrentItem.NumberOfStackedItems;
                XSave.WriteInventoryItem(CurrentItem);
                ItemGroupList_SelectedIndexChanged(null, null);
            }
            catch { }
        }

        private void cmdDeleteItem_Click(object sender, EventArgs e)
        {
            if (!ItemList.Enabled)
                return;
            try
            {
                for (int x = 0; x < ItemList.SelectedItems.Count; x++)
                {
                    if (ItemList.SelectedItems[0].SubItems[2].Text != "N")
                    {
                        CurrentItem.NumberOfItemsWithChangedProperties--;
                        CurrentItem.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].Delete = true;
                    }
                    CurrentItem.NumberOfStackedItems--;
                    ItemList.SelectedItems[0].Remove();
                }
                if (ItemList.Items.Count == 0)
                {
                    ItemList.Enabled = false;
                    CurrentItem.Delete = true;
                    XSave.WriteInventoryItem(CurrentItem);
                    populateInventory();
                }
                else
                {
                    XSave.WriteInventoryItem(CurrentItem);
                    ItemGroupList_SelectedIndexChanged(null, null);
                }
            }
            catch { }
        }

        private void tabClick(object sender, EventArgs e)
        {
            Size = new Size((((DevComponents.DotNetBar.RibbonTabItem)sender).Name == "tabInventory") ? 636 : 481, Size.Height);
        }

        private void cmdExtractImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Thumbnail";
            sfd.Filter = "Bitmap Files|*.bmp";
            sfd.FileName = "Oblivion - " + XSave.GameHeader.CharacterName + ".bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
                pbThumbnail.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            SearchTimer.Stop();
            AddItemList.Items.Clear();
            foreach (string ItemName in Enum.GetNames(typeof(OblivionSave.FormID)))
            {
                bool Added = false;
                string NewItemName = FixItemName(ItemName);
                for (int x = 0; x < ItemGroupList.Items.Count; x++)
                    if (ItemGroupList.Items[0].Text == NewItemName || NewItemName == "Gold" || NewItemName == "Lockpick")
                        Added = true;
                if (!Added && NewItemName.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    ListViewItem li = new ListViewItem(NewItemName);
                    li.SubItems.Add(((uint)Enum.Parse(typeof(OblivionSave.FormID), ItemName)).ToString());
                    AddItemList.Items.Add(li);
                }
            }
        }

        private void ItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Working = true;
            try
            {
                OblivionSave.SaveRecord.PlayerRecord.InventoryItem Item = XSave.GetInventoryItem(uint.Parse(ItemGroupList.SelectedItems[0].SubItems[1].Text));
                if (ItemList.SelectedItems[0].SubItems[2].Text != "N")
                {
                    bool Found = false;
                    foreach (OblivionSave.SaveRecord.PlayerRecord.SlaveProperty Slave in Item.MasterProperties[int.Parse(ItemList.SelectedItems[0].SubItems[2].Text)].SlaveProperties)
                    {
                        if (Slave.Type == OblivionSave.SaveRecord.PlayerRecord.PropertyType.ItemHealth)
                        {
                            intItemHealth.Value = Convert.ToDecimal(Slave.ItemHealth);
                            Found = true;
                        }
                    }
                    if (!Found)
                        intItemHealth.Value = 0;
                }
                else
                    intItemHealth.Value = 0;
                intItemHealth.Enabled = true;
            }
            catch { intItemHealth.Enabled = false; }
            Working = false;
        }

        private void cmdMaxSkills_Click(object sender, EventArgs e)
        {
            foreach (Control con in panelSkills.Controls)
                if (con is NumericUpDown)
                    ((NumericUpDown)con).Value = ((NumericUpDown)con).Maximum;
        }

        private void cmdMaxStats_Click(object sender, EventArgs e)
        {
            foreach (Control con in panelStats.Controls)
                if (con is NumericUpDown)
                    ((NumericUpDown)con).Value = ((NumericUpDown)con).Maximum;
        }

        protected internal override void EditorControl_FormClosing(object s, FormClosingEventArgs e)
        {
            if (isPCSave && IO != null && IO.Opened)
                IO.Close();
            base.EditorControl_FormClosing(s, e);
        }
    }
}
