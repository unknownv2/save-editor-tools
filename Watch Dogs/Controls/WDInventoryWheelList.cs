using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeadSpace;
using DevComponents.AdvTree;
using DevComponents.Editors;
using FarCry;
using WatchDogs;

namespace Horizon.PackageEditors.Watch_Dogs.Controls
{
    public partial class WDInventoryWheelList : UserControl
    {
        private readonly WatchDogsSave _saveGame;
        private WatchDogsSaveEntry _inventoryEntry,_itemsBought;

        internal WDInventoryWheelList(WatchDogsSave saveGame)
        {
            InitializeComponent();

            _saveGame = saveGame;

            var inventoryItems = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\PlayerInventory\Items"));
            _inventoryEntry = _saveGame.GetSaveEntry(_saveGame.MainEntry, inventoryItems, string.Empty);

            inventoryItems = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\PlayerProgression\ItemBought"));
            _itemsBought = _saveGame.GetSaveEntry(_saveGame.MainEntry, inventoryItems, string.Empty);


            foreach (WatchDogsSaveEntry inventoryItem in _inventoryEntry.Children)
            {
                var itemId = _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")]).ToUInt32(false);
                if (WatchDogsData.BulletList.Contains(itemId) || WatchDogsData.ProjectileList.Contains(itemId))
                {
                    var bulletCount = saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("Item")]);
                    treeInventoryItems.Nodes.Add(CreateIntegerNode(itemId, ConvertVariableByteArrayToInt(bulletCount), WatchDogsData.Items[itemId], int.MaxValue));
                }
            }
            treeInventoryItems.Nodes.Sort();
        }

        internal void Save()
        {
            // save item values just in case when exiting
            foreach (Node ammoNode in treeInventoryItems.Nodes)
            {
                if (ammoNode.Tag == null || ammoNode.Cells[1].HostedControl == null || (ammoNode.Cells[1].HostedControl as IntegerInput) == null)
                {
                    throw new Exception("Attempted to write an invalid node in the Ammunition Tree.");
                }

                ModifyItemInInventory((uint)ammoNode.Tag, BitConverter.GetBytes((ammoNode.Cells[1].HostedControl as IntegerInput).Value));
            }
        }
        private void UnlockAllOutfits(object sender, EventArgs e)
        {
            bool unlockULC = false;
            /*
            var result = Functions.UI.messageBox("Do you also want to unlock ULC Pack (Watch Dogs DLC) clothing?",
                "Continue?", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel);
            
            switch (result)
            {
                case DialogResult.Yes:
                    unlockULC = false; // leave for now
                    break;
                case DialogResult.Cancel:
                    return;
            }
            */
            var outfitsList = (from item in WatchDogsData.Items where item.Value.Contains("Clothing_SP.") select item.Key).ToList();
            foreach (var inventoryItem in _inventoryEntry.Children)
            {
                var itemId = _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")]).ToUInt32(false);
                if (outfitsList.Contains(itemId))
                    outfitsList.Remove(itemId);
            }
            if (unlockULC)
            {
                var outfitsListULC = (from item in WatchDogsData.Items where item.Value.Contains("Clothing_ULC.") select item.Key).ToList();
                foreach (var inventoryItem in _inventoryEntry.Children)
                {
                    var itemId = _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")]).ToUInt32(false);
                    if (outfitsListULC.Contains(itemId))
                        outfitsListULC.Remove(itemId);
                }
                outfitsList.AddRange(outfitsListULC);
            }

            Functions.UI.messageBox(string.Format("Successfully unlocked {0} outfits!", AddListToInventory(outfitsList, new byte[] { 0x01 })));
        }

        private void UnlockAllSkillsInProgressTree(object sender, EventArgs e)
        {
            var inventoryItems = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\PlayerProgression\PerksUnlocked"));
            var perksUnlocked = _saveGame.GetSaveEntry(_saveGame.MainEntry, inventoryItems, string.Empty);

            inventoryItems = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\PlayerProgression\PerksBought"));
            var perksBought = _saveGame.GetSaveEntry(_saveGame.MainEntry, inventoryItems, string.Empty);

            var playerProgressionList = (from item in WatchDogsData.PlayerProgression select item.Key).ToList();//new List<uint>(WatchDogsData.SkillsList);
            foreach (var perkUnlocked in perksUnlocked.Children)
            {
                var itemId = _saveGame.GetEntryData(perkUnlocked.Attributes[FarCry3Attribute.GetIdent("PerkUnlocked")]).ToUInt32(false);
                if (playerProgressionList.Contains(itemId))
                    playerProgressionList.Remove(itemId);
            }
            foreach (uint itemId in playerProgressionList)
            {
                // create inventory entry
                var entry = new WatchDogsSaveEntry { Attributes = new Dictionary<uint, WatchDogsSaveAttributes>(), Children = new List<WatchDogsSaveEntry>() };

                // 0x01F4EACC = FarCry3Attribute.GetIdent("WeaponState");

                entry.ItemId = 0xD7F84FED;

                entry.Attributes.Add(0xD7F84FED, new WatchDogsSaveAttributes { ItemId = 0xD7F84FED, Modified = true, Data = BitConverter.GetBytes(itemId), ItemLength = 4 }); // item id

                // update weapon state 
                perksUnlocked.Children.Add(entry);
            }
            playerProgressionList = (from item in WatchDogsData.PlayerProgression select item.Key).ToList();//new List<uint>(WatchDogsData.SkillsList);
            foreach (var perkBought in perksBought.Children)
            {
                var itemId = _saveGame.GetEntryData(perkBought.Attributes[FarCry3Attribute.GetIdent("PerkBought")]).ToUInt32(false);
                if (playerProgressionList.Contains(itemId))
                    playerProgressionList.Remove(itemId);
            }
            foreach (uint itemId in playerProgressionList)
            {
                // create inventory entry
                var entry = new WatchDogsSaveEntry { Attributes = new Dictionary<uint, WatchDogsSaveAttributes>(), Children = new List<WatchDogsSaveEntry>() };

                // 0x01F4EACC = FarCry3Attribute.GetIdent("WeaponState");

                entry.ItemId = 0x9969365D;

                entry.Attributes.Add(0x9969365D, new WatchDogsSaveAttributes { ItemId = 0x9969365D, Modified = true, Data = BitConverter.GetBytes(itemId), ItemLength = 4 }); // item id

                // update weapon state 
                perksBought.Children.Add(entry);
            }
            playerProgressionList = (from item in WatchDogsData.PlayerProgression select item.Key).ToList();//new List<uint>(WatchDogsData.SkillsList);
            foreach (var inventoryItem in _inventoryEntry.Children)
            {
                var itemId = _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")]).ToUInt32(false);
                if (playerProgressionList.Contains(itemId))
                    playerProgressionList.Remove(itemId);
            }
            AddListToInventory(playerProgressionList, new byte[] {0x01});
            
            Functions.UI.messageBox(string.Format("Successfully unlocked all skills!"));
        }

        private void UnlockAllWeapons(object sender, EventArgs e)
        {
            var weaponsList = new List<uint>(WatchDogsData.WeaponsList);
            foreach (var inventoryItem in _inventoryEntry.Children)
            {
                var itemId = _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")]).ToUInt32(false);
                if (weaponsList.Contains(itemId))
                    weaponsList.Remove(itemId);
            }
            // add ammo for the weapon categories that were created
            var bulletList = new List<uint>(WatchDogsData.BulletList);
            foreach (var inventoryItem in _inventoryEntry.Children)
            {
                var itemId = _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")]).ToUInt32(false);
                if (bulletList.Contains(itemId))
                    bulletList.Remove(itemId);
            }
            AddListToInventory(bulletList, BitConverter.GetBytes((short)1000));

            var projectileList = new List<uint>(WatchDogsData.ProjectileList);
            foreach (var inventoryItem in _inventoryEntry.Children)
            {
                var itemId = _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")]).ToUInt32(false);
                if (projectileList.Contains(itemId))
                    projectileList.Remove(itemId);
            }
            AddListToInventory(projectileList, BitConverter.GetBytes((short)1000));

            int weaponsAddedCount = AddListToInventory(weaponsList, new byte[] {0x01});

            var inventoryItems = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\PlayerInventory\WeaponsState"));
            var weaponsState = _saveGame.GetSaveEntry(_saveGame.MainEntry, inventoryItems, string.Empty);
            foreach (uint itemId in weaponsList)
            {
                // create inventory entry
                var entry = new WatchDogsSaveEntry { Attributes = new Dictionary<uint, WatchDogsSaveAttributes>(), Children = new List<WatchDogsSaveEntry>() };

                // 0x01F4EACC = FarCry3Attribute.GetIdent("WeaponState");

                entry.ItemId = 0x01F4EACC;

                entry.Attributes.Add(0xDBD8F646, new WatchDogsSaveAttributes { ItemId = 0xDBD8F646, Modified = true, Data = BitConverter.GetBytes(itemId), ItemLength = 4 }); // item id
                entry.Attributes.Add(0xC2705C51, new WatchDogsSaveAttributes { ItemId = 0xC2705C51, Modified = true, Data = new byte[] { 0x7F}, ItemLength = 1 }); // current ammo count (reverts so it just needs to be a high value)

                // update weapon state 
                weaponsState.Children.Add(entry);
            }
            inventoryItems = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "Root", @"savegame\NexusSaveGame\NexusSecondaryGameData\PlayerInventory\ItemsInWheelNormalLoadOut"));
            var itemsInWheelNormalLoadOut = _saveGame.GetSaveEntry(_saveGame.MainEntry, inventoryItems, string.Empty);
            
            // 0xB254B247 = FarCry3Attribute.GetIdent("ItemId");
            // newly added weapon categories need to be filled so the user can scroll through them, projectiles do not
            for (int i = 0; i < itemsInWheelNormalLoadOut.Children.Count; i++)
            {
                WatchDogsSaveEntry itemInWheel = itemsInWheelNormalLoadOut.Children[i];
                // means the entry is empty
                if (itemInWheel.Attributes[0xB254B247].ItemLength != 0x01 ||_saveGame.GetEntryData(itemInWheel.Attributes[0xB254B247])[0x00] != 0xFF) continue;

                uint weaponId = WatchDogsData.DefaultWeaponWheeList[i];

                itemInWheel.Attributes[0xB254B247].ItemLength = 0x04;
                itemInWheel.Attributes[0xB254B247].Data = BitConverter.GetBytes(weaponId);
                itemInWheel.Attributes[0xB254B247].Modified = true;
            }

            // add new created ammo categories to the Ammo treeView
            foreach (uint bulletId in bulletList)
            {
                treeInventoryItems.Nodes.Add(CreateIntegerNode(bulletId, 1000, WatchDogsData.Items[bulletId], int.MaxValue));
            }


            // report success to the user
            Functions.UI.messageBox(string.Format("Successfully unlocked {0} weapons! Newly added weapon categories will load with ammo set to '1000'.", weaponsAddedCount));

        }

        private int AddListToInventory(IEnumerable<uint> itemList, byte[] itemValue)
        {
            int i = 0;
            
            foreach (uint itemId in itemList)
            {
                // create inventory entry
                var entry = new WatchDogsSaveEntry { Attributes = new Dictionary<uint, WatchDogsSaveAttributes>(), Children = new List<WatchDogsSaveEntry>() };

                // 0x94C93671 = FarCry3Attribute.GetIdent("SavedItem")
                // 0xBF298A20 = FarCry3Attribute.GetIdent("Item");
                // 0x89616A1A = FarCry3Attribute.GetIdent("StringId");
                // 0xECC7C745 = FarCry3Attribute.GetIdent("ItemBought");

                entry.ItemId = 0x94C93671;

                entry.Attributes.Add(0xBF298A20, new WatchDogsSaveAttributes { ItemId = 0xBF298A20, Modified = true, Data = itemValue, ItemLength = itemValue.Length });
                entry.Attributes.Add(0x89616A1A, new WatchDogsSaveAttributes { ItemId = 0x89616A1A, Modified = true, Data = BitConverter.GetBytes(itemId), ItemLength = 4 });

                _inventoryEntry.Children.Add(entry);

                // create item bought entry
                entry = new WatchDogsSaveEntry { Attributes = new Dictionary<uint, WatchDogsSaveAttributes>(), Children = new List<WatchDogsSaveEntry>() };
                entry.ItemId = 0xECC7C745;
                entry.Attributes.Add(0xECC7C745, new WatchDogsSaveAttributes { ItemId = 0xECC7C745, Modified = true, Data = BitConverter.GetBytes(itemId), ItemLength = 4 });

                _itemsBought.Children.Add(entry);
                i++;
            }
            return i;
        }

        private void ModifyItemInInventory(uint itemId, byte[] itemValue)
        {
            foreach (var inventoryItem in _inventoryEntry.Children)
            {
                if (
                    _saveGame.GetEntryData(inventoryItem.Attributes[FarCry3Attribute.GetIdent("StringId")])
                        .ToUInt32(false) == itemId)
                {
                    var attribute = inventoryItem.Attributes[0xBF298A20];
                    attribute.Modified = true;
                    attribute.Data = itemValue;
                }
            }
        }

        private void BtnClick_MaxAllAmmunition(object sender, EventArgs e)
        {
            foreach (Node ammoNode in treeInventoryItems.Nodes)
            {
                if (ammoNode.Tag == null || ammoNode.Cells[1].HostedControl == null)
                {
                    throw new Exception("Attempted to write an invalid node in the Ammunition Tree.");
                }

                (ammoNode.Cells[1].HostedControl as IntegerInput).Value = int.MaxValue;

                uint itemId = (uint)ammoNode.Tag;
                //int value = (ammoNode.HostedControl as IntegerInput).Value;
                ModifyItemInInventory(itemId, BitConverter.GetBytes(int.MaxValue));
            }
        }
        private Node CreateIntegerNode(uint key, int value, string title, int maxValue)
        {
            var integerInput = new IntegerInput();

            integerInput.Value = value;
            integerInput.MaxValue = maxValue;
            integerInput.ShowUpDown = true;

            var node = new Node(title) { Tag = key};
            node.Cells.Add(new Cell { HostedControl = integerInput });
            
            return node;
        }

        private int ConvertVariableByteArrayToInt(byte[] arrayValue)
        {
            int value = 0;
            switch (arrayValue.Length)
            {
                case 0x00:
                    break;
                case 1:
                    value = arrayValue[0x00];
                    break;
                case 2:
                    value = BitConverter.ToInt16(arrayValue, 0);
                    break;
                case 4:
                    value = BitConverter.ToInt32(arrayValue, 0);
                    break;
            }
            return value;
        }
    }
}
