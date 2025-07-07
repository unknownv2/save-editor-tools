using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using ResidentEvil5;

/* 
 - Fill weapon enums
*/
namespace Horizon.PackageEditors.Resident_Evil_5
{
    public partial class ResidentEvil5 : EditorControl
    {
        //public static readonly string FID = "434307D4";

        private GameSave GameSave;

        //private Horizon.Functions.DatagridViewColumnSorter InventoryDatagridSorter, TreasureDatagridSorter;

        private bool LoadingTreasures = false;
        // Our form's inventory, seperate from the save.
        private List<Inventory.Entry> mInventory, tInventory, cInventory, sInventory;
        private List<List<Inventory.Entry>> Inventories;

        private List<string> KeyItems, FileItems;

        public ResidentEvil5()
        {
            InitializeComponent();
            TitleID = FormID.ResidentEvil5;
            
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
                return false;

            //this.InventoryDatagridSorter = new Horizon.Functions.DatagridViewColumnSorter(SortOrder.Ascending);
            //this.TreasureDatagridSorter = new Horizon.Functions.DatagridViewColumnSorter(SortOrder.Ascending);

            this.GameSave = new GameSave(this.IO);

            this.GameSave.Read();

            this.mInventory = new List<Inventory.Entry>(this.GameSave.MainInventory);

            if (LoadingTreasures)
                this.tInventory = new List<Inventory.Entry>(this.GameSave.TreasureInventory);
            
            this.cInventory = new List<Inventory.Entry>(this.GameSave.ChrisInventory);
            this.sInventory = new List<Inventory.Entry>(this.GameSave.ShevaInventory);

            this.Inventories = new List<List<Inventory.Entry>>();
            this.Inventories.Add(this.mInventory);

            if (LoadingTreasures)
                this.Inventories.Add(this.tInventory);

            this.Inventories.Add(this.cInventory);
            this.Inventories.Add(this.sInventory);

            this.KeyItems = new List<string>();
            this.FileItems = new List<string>();

            for (int j = 0; j < 0x40; j++)
                this.KeyItems.Add(string.Format("Key_{0:X2}", j));

            for (int j = 0; j < 0x10; j++)
                this.FileItems.Add(string.Format("File_{0:X2}", j));

            this.InitiateControls();

            this.DisplaySaveFile();

            this.cmbInventories.SelectedIndex = 0;

            return true;
        }

        public override void Save()
        {
            this.GameSave.SaveFile.Gold = this.intInpGold.Value;
            this.GameSave.SaveFile.ExchangePoints = this.intInpExchPts.Value;

            this.GameSave.WriteMainInventory(this.Inventories[0], this.LoadingTreasures ? Inventories[1] : null);
            this.GameSave.WriteCharacterInventory(this.Inventories[this.LoadingTreasures ? 2 : 1], this.Inventories[this.LoadingTreasures ? 3 : 2]);

            this.GameSave.Save();
        }

        private void InitiateControls()
        {            
            // Populate the control with the list of available classes
            if (this.LoadingTreasures)
            {
                this.cmbClass.Items.AddRange(new string[]
                {
                    "Dummy",
                    "Weapon",
                    "Ammunition",
                    "Accessory",      
                    "Treasure",
                    "Key",
                    "Other",
                    "File"
                });

                this.cmbInventories.Items.AddRange(new string[]
                {
                    "Inventory",
                    "Treasures",
                    "Chris",
                    "Sheva"
                });
            }
            else
            {
                this.cmbClass.Items.AddRange(new string[]
                {
                    "Dummy",
                    "Weapon",
                    "Ammunition",
                    "Accessory",      
                    "Key",
                    "Other",
                    "File"
                });

                    this.cmbInventories.Items.AddRange(new string[]
                {
                    "Inventory",
                    "Chris",
                    "Sheva"
                });
            }
        }

        private void LoadItems(string Class)
        {
            switch (Class)
            {
                case "Dummy":
                    this.LoadDummy();
                    break;
                case "Accessory":
                    this.LoadAccessories();
                    break;
                case "Weapon":
                    this.LoadWeapons();
                    break;
                case "Ammunition":
                    this.LoadAmmunition();
                    break;
                case "Key":
                    this.LoadKeys();
                    break;
                case "Other":
                    this.LoadOther();
                    break;
                case "File":
                    this.LoadFiles();
                    break;
            }
        }
        private void LoadItems(int ClassIndex)
        {
            if (this.LoadingTreasures)
            {
                switch (ClassIndex)
                {
                    case 0:
                        this.LoadDummy();
                        break;
                    case 1:
                        this.LoadWeapons();
                        break;
                    case 2:
                        this.LoadAmmunition();
                        break;
                    case 3:
                        this.LoadAccessories();
                        break;
                    // 4 - Treasures;
                    case 5:
                        this.LoadKeys();
                        break;
                    case 6:
                        this.LoadOther();
                        break;
                    case 7:
                        this.LoadFiles();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (ClassIndex)
                {
                    case 0:
                        this.LoadDummy();
                        break;
                    case 1:
                        this.LoadWeapons();
                        break;
                    case 2:
                        this.LoadAmmunition();
                        break;
                    case 3:
                        this.LoadAccessories();
                        break;
                    case 4:
                        this.LoadKeys();
                        break;
                    case 5:
                        this.LoadOther();
                        break;
                    case 6:
                        this.LoadFiles();
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadDummy()
        {
            this.ClearAndAdd(new string[] {
                "Empty"
            }, this.cmbItem);
            
        }
        private void LoadAccessories()
        {
            this.ClearAndAdd(new string[] {
                "Empty", "First aid spray", "Herb (Green)", "Herb (Red)", "Herb (Green+Green)", "Herb (Green+Red)"
            }, this.cmbItem);
        }
        private void LoadWeapons()
        {
            this.ClearAndAdd(new string[] {
                "Empty",
                "M92F (HG)", "Heckler & Koch P8", "Sig P226", "Beretta M93R", "Samurai Edge", "Px4",
                "S&W M29", "L. Hawk", "S&W M500",
                "Ithaca M37", "Benelli M3", "Jail Breaker", "Hydra",
                "Sako 75", "Dragunov SVD", "H&K PSG1",
                "Skorpion vz. 61", "MP5A5", "AK-74", "SIG 556",
                "Grenade Launcher (FLASH)", "Grenade Launcher (EXPLOSIVE)", "Grenade Launcher Ammo (FLAME)", "Grenade Launcher", "Grenade Launcher (ICE)", "Grenade Launcher (ACID)",  "RPG-7 RL", "Proximity Bomb",
                "Stun Rod", "Longbow", "Flamethrower", "Gatling gun", 
                "Rotten Egg", "White Egg", "Gold Egg", "Brown Egg",
                "Hand Grenade", "Flash Grenade", "Incendiary Grenade"
            }, this.cmbItem);            
        }
        private void LoadAmmunition()
        {
            this.ClearAndAdd(new string[] {
                "Empty", "Handgun Ammo", "Machine Gun Ammo", "Shotgun Ammo", "Rifle Ammo", "Magnum Ammo", 
                "Explosive Rounds", "Acid Rounds", "Nitrogen Rounds", "Flame Rounds", "Flash Rounds", "Electric Rounds",
            }, this.cmbItem);
        }
        private void LoadOther()
        {
            this.ClearAndAdd(new string[] 
            { 
                "Empty", "Melee Vest", "Bulletproof Vest"
            }, this.cmbItem);
        }
        private void LoadKeys()
        {
            this.ClearAndAdd(this.KeyItems.ToArray(), this.cmbItem);
        }
        private void LoadFiles()
        {
            this.ClearAndAdd(this.FileItems.ToArray(), this.cmbItem);
        }

        private void ClearAndAdd(object[] Items, DevComponents.DotNetBar.Controls.ComboBoxEx ComboBox)
        {
            ComboBox.Items.Clear();
            ComboBox.Items.AddRange(Items);
        }

        private byte GetItemValue(int Class, int Index)
        {
            switch (Class)
            {
                case 1:
                    return (byte)GetWeaponValue(Index);
                case 2:
                    return (byte)GetAmmoValue(Index); // done
                case 3:
                    return GetAccessoryValue(Index); // done
                    // 4 = Treasures
                case 5:
                    return (byte)Index; // Key
                case 6:
                    return (byte)GetOtherValue(Index); // done
                case 7:
                    return (byte)Index; // File
                default:
                    return 0x00;
            }
            
        }

        private byte GetAccessoryValue(int Index)// done
        {
            switch (Index)
            {
                case 1:
                    return 0x04;
                case 2:
                    return 0x05;
                case 3:
                    return 0x06;
                case 4:
                    return 0x07;
                case 5:
                    return 0x09;
                default:
                    return 0x00;
            }
        }
        private Inventory.Weapons GetWeaponValue(int Index)
        {
            switch (Index)
            {
                // Handguns
                case 1:
                    return Inventory.Weapons.W_HAND_M92F;
                case 2:
                    return Inventory.Weapons.W_HAND_HK_P8;
                case 3:
                    return Inventory.Weapons.W_HAND_SIG_P226;
                case 4:
                    return Inventory.Weapons.W_HAND_93R;
                case 5:
                    return Inventory.Weapons.W_HAND_SAMURAI;
                case 6:
                    return Inventory.Weapons.W_HAND_PX4;

                // Magnums
                case 7:
                    return Inventory.Weapons.W_MAGNUM_SW_M29;
                case 8:
                    return Inventory.Weapons.W_MAGNUM_DESERTEAGLE;
                case 9:
                    return Inventory.Weapons.W_MAGNUM_SW_M500;

                // Shotguns
                case 10:
                    return Inventory.Weapons.W_SHOT_ITHAKA;
                case 11:
                    return Inventory.Weapons.W_SHOT_BENELLI_M3;
                case 12:
                    return Inventory.Weapons.W_SHOT_STRIKER;
                case 13:
                    return Inventory.Weapons.W_SHOT_MADMAX;
                
                // Sniper Rifles
                case 14:
                    return Inventory.Weapons.W_RIFLE_SAKO;
                case 15:
                    return Inventory.Weapons.W_RIFLE_DRAGUNOVA;
                case 16:
                    return Inventory.Weapons.W_RIFLE_PSG1;

                // Machine Guns
                case 17:
                    return Inventory.Weapons.W_MACHINE_SCORPION;
                case 18:
                    return Inventory.Weapons.W_MACHINE_MP5;
                case 19:
                    return Inventory.Weapons.W_MACHINE_AK74;
                case 20:
                    return Inventory.Weapons.W_MACHINE_SIG_556;

                // Explosives
                case 21:
                    return Inventory.Weapons.W_LAUNCHER_GRENADE_H;
                case 22:
                    return Inventory.Weapons.W_LAUNCHER_GRENADE_N;
                case 23:
                    return Inventory.Weapons.W_LAUNCHER_GRENADE_F;
                case 24:
                    return Inventory.Weapons.W_LAUNCHER_GRENADE_L;
                case 25:
                    return Inventory.Weapons.W_LAUNCHER_GRENADE_I;
                case 26:
                    return Inventory.Weapons.W_LAUNCHER_GRENADE_S;
                case 27:
                    return Inventory.Weapons.W_LAUNCHER_ROCKET;
                //case 26:
                    //return Inventory.Weapons.W_INFINITY_ROCKET;
                case 28:
                    return Inventory.Weapons.W_MINE_B;

                // Miscelaneous
                case 29:
                    return Inventory.Weapons.W_STUNROD;
                case 30:
                    return Inventory.Weapons.W_BOW;
                case 31:
                    return Inventory.Weapons.W_FLAMETHROWER;
                case 32:
                    return Inventory.Weapons.W_MACHINE_GATLING;

                case 33:
                    return Inventory.Weapons.W_EGG_X;
                case 34:
                    return Inventory.Weapons.W_EGG_W;
                case 35:
                    return Inventory.Weapons.W_EGG_G;
                case 36:
                    return Inventory.Weapons.W_EGG_B;

                case 37:
                    return Inventory.Weapons.W_GRENADE_NORMAL;
                case 38:
                    return Inventory.Weapons.W_GRENADE_FLASH;
                case 39:
                    return Inventory.Weapons.W_GRENADE_FIRE;
                default:
                    return 0x00;
            }
        }
        private Inventory.Ammunition GetAmmoValue(int Index)// done
        {
            switch (Index)
            {
                case 1:
                    return Inventory.Ammunition.B_HAND_GUN;
                case 2:
                    return Inventory.Ammunition.B_MACHINE_GUN;
                case 3:
                    return Inventory.Ammunition.B_SHOT_GUN;
                case 4:
                    return Inventory.Ammunition.B_RIFLE;
                case 5:
                    return Inventory.Ammunition.B_MAGNUM;
                case 6:
                    return Inventory.Ammunition.B_LAUNCHER_N;
                case 7:
                    return Inventory.Ammunition.B_LAUNCHER_S;
                case 8:
                    return Inventory.Ammunition.B_LAUNCHER_I;
                case 9:
                    return Inventory.Ammunition.B_LAUNCHER_F;
                case 10:
                    return Inventory.Ammunition.B_LAUNCHER_H;
                case 11:
                    return Inventory.Ammunition.B_LAUNCHER_L;
                default:
                    return 0x00;
            }
        }
        private Inventory.Other GetOtherValue(int Index)// done
        {
            switch (Index)
            {
                case 1:
                    return Inventory.Other.ETC_ARMOR;
                case 2:
                    return Inventory.Other.ETC_ARMOR_D;
                default:
                    return 0x00;
            }
        }

        private void DisplaySaveFile()
        {
            this.intInpGold.Value = this.GameSave.SaveFile.Gold;
            this.intInpExchPts.Value = this.GameSave.SaveFile.ExchangePoints;
        }
        private void DisplayInventory(List<Inventory.Entry> Inv)
        {
            if (this.advTree1.Nodes.Count > 0x0)
            {
                this.advTree1.Nodes.Clear();
            }

            foreach (Inventory.Entry entry in Inv)
            {
                var DisplayNames = Inventory.ConvertEntryForDisplay(entry);
                var Node = new DevComponents.AdvTree.Node(DisplayNames[0]);
                Node.Nodes.Add(new DevComponents.AdvTree.Node(DisplayNames[1]));
                Node.Nodes.Add(new DevComponents.AdvTree.Node(entry.Amount.ToString()));
                this.advTree1.Nodes.Add(Node);
            }
        }

        private void cmbInventories_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayInventory(this.Inventories[(sender as DevComponents.DotNetBar.ComboBoxItem).SelectedIndex]);
        }

        private void advTree1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.advTree1.SelectedNode != null && (this.advTree1.SelectedNode.Nodes.Count > 1))
            {
                var DisplayNames = Inventory.ConvertEntryForDisplay(this.Inventories[this.cmbInventories.SelectedIndex][this.advTree1.SelectedNode.Index]);
                this.cmbClass.Text = DisplayNames[0];
                this.LoadItems(this.cmbClass.Text);
                this.cmbItem.Text = DisplayNames[1];
                this.intInpAmount.Value = (int)this.Inventories[this.cmbInventories.SelectedIndex][this.advTree1.SelectedNode.Index].Amount;
            }
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems((sender as DevComponents.DotNetBar.Controls.ComboBoxEx).SelectedIndex);
            this.cmbItem.SelectedIndexChanged -= new System.EventHandler(this.cmbItem_SelectedIndexChanged);
            this.cmbItem.SelectedIndex = 0;
            this.cmbItem.SelectedIndexChanged += new System.EventHandler(this.cmbItem_SelectedIndexChanged);
        }

        private void intInpAmount_ValueChanged(object sender, EventArgs e)
        {
            if ((this.intInpAmount.Value != 0) && (this.advTree1.SelectedNode.Nodes.Count > 1))
            {
                this.advTree1.SelectedNode.Nodes[1].Text = this.intInpAmount.Value.ToString();
                var Entry = this.Inventories[this.cmbInventories.SelectedIndex][this.advTree1.SelectedNode.Index];
                Entry.Amount = (short)this.intInpAmount.Value;
            }
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.advTree1.SelectedNode.Nodes.Count > 1)
            {
                var Entry = this.Inventories[this.cmbInventories.SelectedIndex][this.advTree1.SelectedNode.Index];
                Entry.ClassId = (Inventory.Class)Enum.Parse(typeof(Inventory.Class), this.cmbClass.Text, true);
                Entry.Ident = this.GetItemValue(Entry.ClassIdent, this.cmbItem.SelectedIndex);
                this.advTree1.SelectedNode.Text = this.cmbClass.Text;
                this.advTree1.SelectedNode.Nodes[0].Text = Inventory.ConvertEntryForDisplay(Entry)[1];
            }
        }
    }
}