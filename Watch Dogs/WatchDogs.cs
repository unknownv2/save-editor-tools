using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using Horizon.PackageEditors.Watch_Dogs.Controls;
using WatchDogs;
using FarCry;

using System.IO;

namespace Horizon.PackageEditors.Watch_Dogs
{
    public partial class WatchDogs : EditorControl
    {
        private WatchDogsSave SaveGame;
        private List<fValue> _saveProperties;
        private static string _campaignSavePlugin;
        CustomClass myProperties = new CustomClass();

        public WatchDogs()
        {
            InitializeComponent();
            TitleID = FormID.WatchDogs;

            Size = new Size(466, 441);
        }
        public override bool Entry()
        {
            if (!OpenStfsFile("SaveGame.dat"))
                return false;


            SaveGame = new WatchDogsSave(IO);

            CreateComponents();

            return true;
        }

        public override void Save()
        {
            // call save function for all tabs in the Inventory control
            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcInventory.Tabs)
            {
                var obj = sti.AttachedControl.Controls[0] as WDEditorContainer;
                if (obj != null)
                    obj.Save();
            }

            for (var i = 0; i < myProperties.Count; i++)
            {
                var attribute = myProperties[i];
                //var bagAccess = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "CampaignSave", attribute.Category));
                SetItemAttribute(SaveGame.MainEntry, attribute.Path, attribute.Value, attribute.Name, attribute.Attributes);
            }

            //IO.SeekTo(0x00);
            //IO.Out.Write(SaveGame.Save());
            var saveData = SaveGame.Save(false);

            IO.Close();

            Package.StfsContentPackage.InjectFileFromArray("SaveGame.dat", saveData);

            if (!OpenStfsFile("SaveGame.dat"))
                throw new Exception("Could not open the save data!");

            SaveGame = new WatchDogsSave(IO);
        }

        private void CreateComponents()
        {
            Ubisoft.XmlParser parser;


            // load inventory items
            treeInventory.Nodes.Clear();
            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcInventory.Tabs)
            {
                sti.Tag = null;
                sti.AttachedControl.Controls.Clear();
            }
            stcInventory.Tabs.Clear();

            var inventoryItems =
                FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\PlayerInventory\Items"));
            var inventoryEntry = SaveGame.GetSaveEntry(SaveGame.MainEntry, inventoryItems, string.Empty);
            // add inventory items to tree
            var node = new Node("Inventory") {Tag = WatchDogsContainerType.Inventory};

            foreach (var inventoryItem in inventoryEntry.Children)
            {
                var itemId =
                    SaveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")])
                        .ToUInt32(false);

                string itemName = WatchDogsData.Items.ContainsKey(itemId)
                    ? WatchDogsData.Items[itemId]
                    : WatchDogsData.PlayerProgression.ContainsKey(itemId) ? WatchDogsData.PlayerProgression[itemId] : itemId.ToString("X");

                node.Nodes.Add(new Node(itemName));
            }
            node.Nodes.Sort();

            treeInventory.Nodes.Add(node);

            node = new Node("Cars On Demand");

            var carsOnDemandBag = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\CarsOnDemand\Cars"));
            var carsOnDemandEntry = SaveGame.GetSaveEntry(SaveGame.MainEntry, carsOnDemandBag, string.Empty);
            node.Tag = WatchDogsContainerType.CarsOnDemand;

            foreach (var car in carsOnDemandEntry.Children)
            {
                var itemId = SaveGame.GetEntryData(car.Attributes[0x4573FB22]).ToUInt32(false);

                string itemName = WatchDogsData.Items.ContainsKey(itemId) ? WatchDogsData.Items[itemId] : itemId.ToString("X");

                node.Nodes.Add(new Node(itemName));
            }
            node.Nodes.Sort();
            
            treeInventory.Nodes.Add(node);

            // display global values
            //parser = new Ubisoft.XmlParser(@"E:\Projects\Watch Dogs\CampaignSave.wd", true);
            parser = new Ubisoft.XmlParser(Encoding.UTF8.GetString(Convert.FromBase64String(SettingAsString(30))), false);
            _saveProperties = parser.ValuesList;
            foreach (fPropertyBag propertyBag in _saveProperties)
            {
                if (!propertyBag.Visible)
                    continue;

                var bagAccess = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", propertyBag.Name));
                foreach (fValue attribute in propertyBag.Values)
                {
                    AddValueToPropertyGrid(attribute, propertyBag, bagAccess);
                }
            }
            propGridCampaignSave.SelectedObject = myProperties;

        }
        private void AddValueToPropertyGrid(fValue attribute, fPropertyBag propertyBag, List<uint> bagAccess)
        {
            if (!attribute.Visible)
                return;

            if (string.IsNullOrEmpty(propertyBag.Name))
                propertyBag.Name = "Root";

            switch (attribute.Attributes)
            {
                case fValue.ObjectAttributes.None:
                    {
                        byte[] data = GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name);
                        object value = null;
                        switch(data.Length)
                        {
                            case 0:
                                value = 0;
                                break;
                            case 1:
                                value = data[0x00];
                                break;
                            case 2:
                                value = BitConverter.ToInt16(data,0) & 0xFFFF;
                                break;
                            case 4:
                                value = data.ToInt32(false);
                                break;
                        }
                        myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name, Convert.ToInt32(value), false, attribute.Visible, attribute.Attributes, bagAccess));
                    }
                    break;

                case fValue.ObjectAttributes.Boolean:
                    myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                        Convert.ToBoolean(GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name)[0x00]), false, attribute.Visible, attribute.Attributes, bagAccess));
                    break;
                case fValue.ObjectAttributes.Int32:
                    myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                        GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name).ToInt32(false), false, attribute.Visible, attribute.Attributes, bagAccess));
                    break;
                case fValue.ObjectAttributes.Float:
                    myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                        new EndianReader(GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name), EndianType.LittleEndian).ReadSingle(), false, attribute.Visible, attribute.Attributes, bagAccess));
                    break;
                case fValue.ObjectAttributes.Ident:
                    myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                        GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name).ToInt32(false).ToString("X"), false, attribute.Visible, attribute.Attributes, bagAccess));
                    break;
                case fValue.ObjectAttributes.Array:
                    switch (attribute.Name)
                    {
                        default:
                            myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                                GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name), false, true, attribute.Attributes, bagAccess));
                            break;
                    }

                    break;
                case fValue.ObjectAttributes.PropertyBag:
                    {
                        var mainProperty = (fPropertyBag)attribute;
                        var tempAccess = new List<uint>(bagAccess);
                        tempAccess.AddRange(FarCry3Attribute.GetAccessIdsFromPath(attribute.Name));
                        foreach (fValue childAttribute in mainProperty.Values)
                        {
                            AddValueToPropertyGrid(childAttribute, mainProperty, tempAccess);
                        }
                    }
                    break;
            }
        }
  
        private byte[] GetItemAttribute(WatchDogsSaveEntry entry, List<uint> accessIds, string attribute)
        {
            Dictionary<uint, int[]> searchDict = new Dictionary<uint, int[]>();
            WatchDogsSaveEntry originalEntry = entry;
            int accessSearchIndex = 0x00;
            while ((entry = FindItemEntryFromId(entry, accessIds, searchDict, ref accessSearchIndex)) == null)
            {
                entry = originalEntry;
            }
            return SaveGame.GetEntryData(entry.Attributes[FarCry3Attribute.GetIdent(attribute)]);
        }

        private WatchDogsSaveEntry FindItemEntryFromId(WatchDogsSaveEntry entry, List<uint> accessIds, Dictionary<uint, int[]> searchDict, ref int accessSearchIndex)
        {
            uint currentSearchId = accessIds[accessSearchIndex];
            foreach (uint accessId in accessIds)
            {
                if (!searchDict.ContainsKey(accessId) && entry != null)
                    searchDict.Add(accessId, new[] { 0x00, entry.Children.Count });

                entry = SaveGame.GetEntryFromItemId(accessId, entry, searchDict.ContainsKey(accessId) ? searchDict[accessId][0x00] : 0x00);

                if (entry != null) continue;
                if (searchDict[currentSearchId][0x00] < (searchDict[currentSearchId][0x01] - 1))
                    searchDict[accessIds[accessSearchIndex]][0x00]++;
                else
                {
                    searchDict[accessIds[accessSearchIndex]][0x00] = 0x00;
                    accessSearchIndex++;
                }

                break;
            }
            return entry;
        }
        private void SetItemAttribute(WatchDogsSaveEntry entry, List<uint> accessIds, object value, string property, fValue.ObjectAttributes attributes)
        {
            Dictionary<uint, int[]> searchDict = new Dictionary<uint, int[]>();
            WatchDogsSaveEntry originalEntry = entry;
            int accessSearchIndex = 0x00;
            while ((entry = FindItemEntryFromId(entry, accessIds, searchDict, ref accessSearchIndex)) == null)
            {
                entry = originalEntry;
            }
            switch (attributes)
            {
                case fValue.ObjectAttributes.None:
                    WatchDogsSave.StaticSetAttribute(entry, value, FarCry3Attribute.GetIdent(property)); 
                    break;
                case fValue.ObjectAttributes.Int32:
                    SaveGame.SetAttribute(entry, Convert.ToInt32(value), FarCry3Attribute.GetIdent(property));
                    break;
                case fValue.ObjectAttributes.Boolean:
                    SaveGame.SetAttribute(entry, Convert.ToBoolean(value), FarCry3Attribute.GetIdent(property));
                    break;
                case fValue.ObjectAttributes.Float:
                    SaveGame.SetAttribute(entry, Convert.ToSingle(value), FarCry3Attribute.GetIdent(property));
                    break;
            }
        }

        private void TreePlayer_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            var tree = sender as AdvTree;
            if(tree == null)
                return;
            if (treeInventory.SelectedNode.Tag == null)
                return;

            var superTabControl = stcInventory;
            DevComponents.DotNetBar.SuperTabItem sti = null;
            foreach (DevComponents.DotNetBar.SuperTabItem sti2 in superTabControl.Tabs)
            {
                if (sti2.Tag == tree.SelectedNode.Tag)
                {
                    sti = sti2;
                    sti.AttachedControl.Controls.Clear();
                    break;
                }
            }

            if (sti == null)
                sti = superTabControl.CreateTab(tree.SelectedNode.Text);

            var objectType = (WatchDogsContainerType)treeInventory.SelectedNode.Tag;
            /*
            WatchDogsContainerType objectType;
            switch (objectTag.ItemId)
            {
                case 0x35F5B22A:
                    objectType = WatchDogsContainerType.CarsOnDemand;
                    break;
                default:
                    objectType = WatchDogsContainerType.None;
                    break;
            }
            */
            var editor = new WDEditorContainer(objectType, SaveGame) {Dock = DockStyle.Fill};
            sti.AttachedControl.Controls.Add(editor);
            sti.Tag = treeInventory.SelectedNode.Tag;
            stcInventory.SelectedTab = sti;
        }
        private void SuperTabControl_TabItemClose(object sender, DevComponents.DotNetBar.SuperTabStripTabItemCloseEventArgs e)
        {
            if (e.Tab == null || (e.Tab as DevComponents.DotNetBar.SuperTabItem == null)) return;

            var obj = (e.Tab as DevComponents.DotNetBar.SuperTabItem).AttachedControl.Controls[0] as WDEditorContainer;
            if (obj != null)
                obj.Save();
        }
        private void BtnTab_Click(object sender, EventArgs e)
        {
            Size = new Size((((DevComponents.DotNetBar.RibbonTabItem)sender).Name != "tabMain") ? 717 : 466,
                            Size.Height);
        }
    }
}
