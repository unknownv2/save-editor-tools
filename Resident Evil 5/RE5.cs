using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ResidentEvil5
{
    public struct Inventory
    {
        public enum Class
        {
	        Dummy = 0x00,
	        Weapon = 0x01,
	        Ammunition = 0x02,
	        Accessory = 0x03,
	        Treasure = 0x04,
	        Key = 0x05,
	        Other = 0x06,
	        File = 0x07
        }

        public enum Dummy
        {
            DUMMY
        }
        public enum Weapons
        {
	        W_NONE,
	        W_KNIFE_CHRIS,
	        W_HAND_M92F, 						// Beretta M92F
	        W_MACHINE_SCORPION, 				// Škorpion vz. 61
	        W_SHOT_ITHAKA, 
	        W_RIFLE_SAKO,
	        W_GRENADE_NORMAL,
	        W_GRENADE_FIRE,
	        W_GRENADE_FLASH,
	        W_MACHINE_SIG_556,
	        W_MINE_B,
	        W_MAGNUM_SW_M29,
	        W_LAUNCHER_GRENADE,
	        W_LAUNCHER_ROCKET,
	        W_KNIFE_SHEBA,
	        W_BOW,
            W_HAND_HK_P8,
	        W_HAND_SIG_P226,
	        W_HAND_BARRY_CUSTOM,
	        W_MACHINE_MP5,
	        W_KNIFE_EXCELLA,
	        W_MACHINE_GATLING = 0x15,
	        W_SHOT_BENELLI_M3,
	        W_SHOT_STRIKER,
	        W_SHOT_MADMAX = 0x19,
	        W_MAGNUM_DESERTEAGLE,
	        W_MAGNUM_SW_M500,
	        W_RIFLE_PSG1,
	        W_MACHINE_AK74,
	        W_HAND_93R,
	        W_HAND_PX4,
	        W_RIFLE_DRAGUNOVA,
	        W_FLAMETHROWER = 0x21,
	        W_STUNROD,
	        W_KNIFE_WESKER,
	        W_KNIFE_JILL,
	        W_LAUNCHER_GRENADE_N,
	        W_LAUNCHER_GRENADE_S,
	        W_LAUNCHER_GRENADE_I = 0x27,
	        W_HAND_SAMURAI = 0x29,
	        W_SHELL_COMMON = 0x2B,
	        W_TORPEDO,
	        W_JEEP_GRENADE,
	        W_JEEP_GATLING,
	        W_GUN_EMPLACEMENT,
	        W_LIGHT,
	        W_TORPEDO_S703,
	        W_SHELL_JEEP_CHRIS_S111,
	        W_SHELL_JEEP_SHEBA_S111,
	        W_SATELLITE_LASER,
	        W_NOCTOVISION_ROCKET,
	        W_EGG_X,
	        W_S100_HAND,
	        W_INFINITY_ROCKET,
	        W_LAUNCHER_GRENADE_F,
	        W_LAUNCHER_GRENADE_H,
	        W_LAUNCHER_GRENADE_L,
	        W_EGG_W,
	        W_EGG_B,
	        W_EGG_G,
	        W_EM_AXE = 0x50,
	        W_EM_SICKLE,
	        W_EM_BOWGUN,
	        W_EM_FORK,
	        W_EM_DYNAMITE,
	        W_EM_HATCHET,
	        W_EM_SHOT_GUN,
	        W_EM_LAUNCHER_GRENADE,
	        W_EM_HUGE_AXE,
	        W_EM_SPEAR = 0x5F,
	        W_EM_FIRE_BOTTLE = 0x6C
        }
        public enum Ammunition
        {
            B_NONE,
            B_HAND_GUN,
            B_MACHINE_GUN,
            B_SHOT_GUN,
            B_RIFLE,
            B_LAUNCHER_N = 0x06,
            B_LAUNCHER_S,
            B_LAUNCHER_I,
            B_MAGNUM,
            B_LAUNCHER_F = 0x0E,
            B_LAUNCHER_H,
            B_LAUNCHER_L,
            B_NOCT_ROCKET
        }
        public enum Accessories
        {
	        HERB_NONE,
	        HERB_G,
	        HERB_R,
	        SPRAY = 0x04, // First Aid Spray
	        TESTTUBE_G, 
	        TESTTUBE_R,
	        TESTTUBE_GG = 0x07,
	        TESTTUBE_GR = 0x09
        }
        public enum Treasure
        {
            MONEY_NONE,
            MONEY_M,
            MONEY_S,
            MONEY_L,
            MONEY_LOCAL_01 = 0x17,
            MONEY_LOCAL_02,
            MONEY_LOCAL_03,
            MONEY_LOCAL_04,
            MONEY_LOCAL_05,
            MONEY_LOCAL_06,
            MONEY_LOCAL_07,
            MONEY_LOCAL_08,
            MONEY_LOCAL_09,
            MONEY_LOCAL_10,
            MONEY_LOCAL_11,
            MONEY_LOCAL_12,
            MONEY_LOCAL_13,
            MONEY_LOCAL_14,
            MONEY_LOCAL_15,
            MONEY_LOCAL_16,
            MONEY_DC_SCORE_ITEM_S,
            MONEY_DC_SCORE_ITEM_M,
            MONEY_DC_SCORE_ITEM_L,
            MONEY_MONY_2A,
            MONEY_MONY_2B,
            MONEY_MONY_2C,
            MONEY_MONY_2D,
            MONEY_MONY_2E,
            MONEY_MONY_2F,
            MONEY_JEWEL_0_00 = 0x50,
            MONEY_JEWEL_0_01,
            MONEY_JEWEL_0_02,
            MONEY_JEWEL_0_03,
            MONEY_JEWEL_0_04,
            MONEY_JEWEL_1_00 = 0x57,
            MONEY_JEWEL_1_01,
            MONEY_JEWEL_1_02,
            MONEY_JEWEL_1_03,
            MONEY_JEWEL_1_04,
            MONEY_JEWEL_2_00 = 0x5E,
            MONEY_JEWEL_2_01,
            MONEY_JEWEL_2_02,
            MONEY_JEWEL_2_03,
            MONEY_JEWEL_2_04,
            MONEY_JEWEL_3_00 = 0x65,
            MONEY_JEWEL_3_01,
            MONEY_JEWEL_3_02,
            MONEY_JEWEL_3_03,
            MONEY_JEWEL_3_04,
            MONEY_JEWEL_4_00 = 0x6C,
            MONEY_JEWEL_4_01,
            MONEY_JEWEL_4_02,
            MONEY_JEWEL_4_03,
            MONEY_JEWEL_4_04,
            MONEY_JEWEL_5_00 = 0x73,
            MONEY_JEWEL_5_01,
            MONEY_JEWEL_5_02,
            MONEY_JEWEL_5_03,
            MONEY_JEWEL_5_04,
            MONEY_JEWEL_6_00 = 0x7A,
            MONEY_JEWEL_6_01,
            MONEY_JEWEL_6_02,
            MONEY_JEWEL_6_03,
            MONEY_JEWEL_6_04
        }
        public enum Keys
        {
            KEY_NONE,
            KEY_03 = 0x03,
            KEY_06 = 0x06,
            KEY_07,
            KEY_08,
            KEY_09,
            KEY_10 = 0x10,
            KEY_12 = 0x12,
            KEY_13,
            KEY_14,
            KEY_15,
            KEY_16,
            KEY_17,
            KEY_18,
            KEY_19,
            KEY_1A,
            KEY_1B,
            KEY_1C,
            KEY_1D,
            KEY_1E,
            KEY_1F,
            KEY_20,
            KEY_21,
            KEY_22,
            KEY_23,
            KEY_24,
            KEY_25,
            KEY_26,
            KEY_27,
            KEY_28,
            KEY_29,
            KEY_2A,
            KEY_2B,
            KEY_2C,
            KEY_2D,
            KEY_2E,
            KEY_2F,
            KEY_30,
            KEY_31,
            KEY_32,
            KEY_33,
            KEY_34,
            KEY_35,
            KEY_36,
            KEY_37,
            KEY_38,
            KEY_39,
            KEY_3A,
            KEY_3B,
            KEY_3C,
            KEY_3D,
            KEY_3E,
            KEY_3F
        }
        public enum Other
        {
            ETC_NONE,
            ETC_ARMOR,
            ETC_02,
            ETC_03,
            ETC_04,
            ETC_PDA,
            ETC_ARMOR_D,
            ETC_MAP_S200,
            ETC_HOURGLASS,
            ETC_09,
            ETC_0A,
            ETC_0B,
            ETC_0C,
            ETC_0D,
            ETC_0E,
            ETC_0F
        }
        public enum File
        {
            FILE_NONE,
            FILE_01,
            FILE_02,
            FILE_03,
            FILE_04,
            FILE_05,
            FILE_06,
            FILE_07,
            FILE_08,
            FILE_09,
            FILE_0A,
            FILE_0B,
            FILE_0C,
            FILE_0D,
            FILE_0E,
            FILE_0F
        }

        public struct OrganizerEntry
        {
            public string ItemClass;
            public string ItemName;
            public short Amount;
        }

        public class Entry
        {
            public Class ClassId
            {
                get
                {
                    return (Class)ClassIdent;
                }
                set
                {
                    ClassIdent = (byte)value;
                }
            }

            public byte ClassIdent;
            public byte Ident;
            public short Amount;
            public long Flags;
            public bool Equals(Entry Item)
            {
                var StrEnt = ConvertEntryForDisplay(this);
                var StrItem = ConvertEntryForDisplay(Item);

                if (string.Compare(StrItem[0], StrEnt[0]) == 0 && string.Compare(StrItem[1], StrEnt[1]) == 0)
                {
                    return this.Amount == Item.Amount;
                }
                return false;
            }
            public bool Equals(OrganizerEntry Item)
            {
                var StrEnt = ConvertEntryForDisplay(this);

                if (string.Compare(Item.ItemClass, StrEnt[0]) == 0 && string.Compare(Item.ItemName, StrEnt[1]) == 0)
                {
                    return this.Amount == Item.Amount;
                }
                return false;
            }
        }

        public static int GetItemIndex(List<Entry> Inventory, string ItemName, string ItemClass, short Amount)
        {
            return Inventory.FindIndex(ent => ent.Equals(new OrganizerEntry()
             {
                 ItemName = ItemName,
                 ItemClass = ItemClass,
                 Amount = Amount
             }));
        }

        private static string ConvertWeaponNameForDisplay(Weapons Weapon)
        {
            switch (Weapon)
            {
                case Weapons.W_HAND_M92F:
                    return "M92F (HG)";
                case Weapons.W_HAND_HK_P8:
                    return "Heckler & Koch P8";
                case Weapons.W_HAND_SIG_P226:
                    return "Sig P226";
                case Weapons.W_HAND_93R:
                    return "Beretta M93R";
                case Weapons.W_HAND_SAMURAI:
                    return "Samurai Edge";
                case Weapons.W_HAND_PX4:
                    return "Px4";

                case Weapons.W_MAGNUM_SW_M29:
                    return "S&W M29";
                case Weapons.W_MAGNUM_DESERTEAGLE:
                    return "L. Hawk";
                case Weapons.W_MAGNUM_SW_M500:
                    return "S&W M500";

                case Weapons.W_SHOT_ITHAKA:
                    return "Ithaca M37";
                case Weapons.W_SHOT_BENELLI_M3:
                    return "Benelli M3";
                case Weapons.W_SHOT_STRIKER:
                    return "Jail Breaker";
                case Weapons.W_SHOT_MADMAX:
                    return "Hydra";
                case Weapons.W_RIFLE_SAKO:
                    return "Sako 75";
                case Weapons.W_RIFLE_DRAGUNOVA:
                    return "Dragunov SVD";
                case Weapons.W_RIFLE_PSG1:
                    return "H&K PSG1";
                case Weapons.W_MACHINE_SCORPION:
                    return "Skorpion vz. 61";
                case Weapons.W_MACHINE_MP5:
                    return "MP5A5";
                case Weapons.W_MACHINE_AK74:
                    return "AK-74";
                case Weapons.W_MACHINE_SIG_556:
                    return "SIG 556";
                case Weapons.W_LAUNCHER_GRENADE_H:
                    return "Grenade Launcher (FLASH)";
                case Weapons.W_LAUNCHER_GRENADE_N:
                    return "Grenade Launcher (EXPLOSIVE)";
                case Weapons.W_LAUNCHER_GRENADE_F:
                    return "Grenade Launcher (FLAME)";
                case Weapons.W_LAUNCHER_GRENADE_L:
                    return "Grenade Launcher";
                case Weapons.W_LAUNCHER_GRENADE_I:
                    return "Grenade Launcher (ICE)";
                case Weapons.W_LAUNCHER_GRENADE_S:
                    return "Grenade Launcher (ACID)";

                case Weapons.W_LAUNCHER_ROCKET:
                    return "RPG-7 RL";
                case Weapons.W_INFINITY_ROCKET:
                    return "RPG-7 RL INF";
                case Weapons.W_MINE_B:
                    return "Proximity Bomb";
                case Weapons.W_STUNROD:
                    return "Stun Rod";
                case Weapons.W_BOW:
                    return "Longbow";
                case Weapons.W_FLAMETHROWER:
                    return "Flamethrower";
                case Weapons.W_MACHINE_GATLING:
                    return "Gatling gun";

                case Weapons.W_EGG_X:
                    return "Rotten Egg";
                case Weapons.W_EGG_W:
                    return "White Egg";
                case Weapons.W_EGG_G:
                    return "Gold Egg";
                case Weapons.W_EGG_B:
                    return "Brown Egg";

                case Weapons.W_GRENADE_NORMAL:
                    return "Hand Grenade";
                case Weapons.W_GRENADE_FLASH:
                    return "Flash Grenade";
                case Weapons.W_GRENADE_FIRE:
                    return "Incendiary Grenade";
                default:
                     return string.Empty;
            }
        }
        private static string ConvertAccessoryNameForDisplay(Accessories Accessory)
        {
            switch (Accessory)
            {
                case Accessories.HERB_G:
                    return "Dummy Green Herb";
                case Accessories.SPRAY:
                    return "First aid spray";
                case Accessories.TESTTUBE_G:
                    return "Herb (Green)";
                case Accessories.TESTTUBE_R:
                    return "Herb (Red)";
                case Accessories.TESTTUBE_GG:
                    return "Herb (Green+Green)";
                case Accessories.TESTTUBE_GR:
                    return "Herb (Green+Red)";
                default:
                    return string.Empty;
            }
        }
        private static string ConvertAmmunitionNameForDisplay(Ammunition Ammo)
        {
            switch (Ammo)
            {
                case Ammunition.B_HAND_GUN:
                    return "Handgun Ammo";
                case Ammunition.B_MACHINE_GUN:
                    return "Machine Gun Ammo";
                case Ammunition.B_SHOT_GUN:
                    return "Shotgun Ammo";
                case Ammunition.B_RIFLE:
                    return "Rifle Ammo";
                case Ammunition.B_MAGNUM:
                    return "Magnum Ammo";
                case Ammunition.B_LAUNCHER_N:
                    return "Explosive Rounds";
                case Ammunition.B_LAUNCHER_S:
                    return "Acid Rounds";
                case Ammunition.B_LAUNCHER_I:
                    return "Nitrogen Rounds";
                case Ammunition.B_LAUNCHER_F:
                    return "Flame Rounds";
                case Ammunition.B_LAUNCHER_H:
                    return "Flash Rounds";
                case Ammunition.B_LAUNCHER_L:
                    return "Electric Rounds";
                default:
                    return string.Empty;
            }
        }
        private static string ConvertOtherNameForDisplay(Other other)
        {
            switch (other)
            {
                case Other.ETC_ARMOR:
                    return "Melee Vest";
                case Other.ETC_ARMOR_D:
                    return "Bulletproof Vest";

                default:
                    return string.Empty;
            }
        }
        private static string ConvertKeyNameForDisplay(Keys Key)
        {
            return string.Format("Key_{0:X2}", (byte)Key);
        }
        private static string ConvertFileNameForDisplay(File File)
        {
            return string.Format("File_{0:X2}", (byte)File);
        }
        public static string[] ConvertEntryForDisplay(Entry InvEntry)
        {
            var InventoryItemStr = new string[2] { "Empty", "Empty"};

            InventoryItemStr[0] = Enum.GetName(InvEntry.ClassId.GetType(), InvEntry.ClassIdent);

            switch(InvEntry.ClassId)
            {
                case Class.Weapon:
                    InventoryItemStr[1] = ConvertWeaponNameForDisplay((Weapons)InvEntry.Ident);
                    break;
                case Class.Ammunition:
                    InventoryItemStr[1] = ConvertAmmunitionNameForDisplay((Ammunition)InvEntry.Ident);
                    break;
                case Class.Accessory:
                    InventoryItemStr[1] = ConvertAccessoryNameForDisplay((Accessories)InvEntry.Ident);
                    break;
                case Class.Key:
                    InventoryItemStr[1] = ConvertKeyNameForDisplay((Keys)InvEntry.Ident);
                    break;
                case Class.Other:
                    InventoryItemStr[1] = ConvertOtherNameForDisplay((Other)InvEntry.Ident);
                    break;
                case Class.File:
                    InventoryItemStr[1] = ConvertFileNameForDisplay((File)InvEntry.Ident);
                    break;
            }
            return InventoryItemStr;
        }

    }
    public class GameSave
    {
        public struct SaveData
        {
            public int Gold;
            public int ExchangePoints;
        }

        private EndianIO IO;
        public SaveData SaveFile;

        public List<Inventory.Entry> MainInventory, TreasureInventory, ChrisInventory, ShevaInventory;

        public GameSave(EndianIO IO)
        {
            if (!IO.Opened)
                IO.Open();

            this.IO = IO;
        }

        ~GameSave()
        {
            if (this.IO != null && this.IO.Opened)
                this.IO.Close();
        }

        public void Read()
        {
            int Checksum = this.CalculateChecksum();

            this.IO.In.SeekTo(0x08);

            if (Checksum != this.IO.In.ReadInt32())
            {
                #if INT2
                System.Diagnostics.Debug.WriteLine("Resident Evil 5: Savedata is invalid or has been tampered with.");
                #endif
            }

            this.ReadStats();
            this.ReadMainInventory();
            this.ReadCharacterInventory();
        }

        public void Save()
        {
            this.WriteStats();

            int Checksum = this.CalculateChecksum();

            this.IO.Out.SeekTo(0x08);
            this.IO.Out.Write(Checksum);
        }

        private void ReadStats()
        {          
            this.IO.In.SeekTo(0xE0);

            this.SaveFile.Gold = this.IO.In.ReadInt32();
            this.SaveFile.ExchangePoints = this.IO.In.ReadInt32();
        }
        private void ReadMainInventory()
        {
            this.FillInventory(0x3158, this.MainInventory = new List<Inventory.Entry>(), false);
            this.FillInventory(0x3158 + 0x288, this.TreasureInventory = new List<Inventory.Entry>(), false);
        }
        private void ReadCharacterInventory()
        {
            /* Each character has a 9 item inventory */

            // Chris' Inventory
            this.FillInventory(0x3690, this.ChrisInventory = new List<Inventory.Entry>(), true);
            // Sheva's Inventory
            this.FillInventory(0x3690 + 0x420, this.ShevaInventory = new List<Inventory.Entry>(), true);
        }
        private void FillInventory(int Offset, List<Inventory.Entry> Inventory, bool Character)
        {
            if (Inventory != null)
            {
                this.IO.In.SeekTo(Offset);
                if (Character)
                {
                    for (var x = 0; x < 9; x++)
                    {
                        Inventory.Add(new Inventory.Entry()
                        {
                            ClassIdent = (byte)this.IO.In.ReadInt24(),
                            Ident = this.IO.In.ReadByte(),
                            Amount = (short)this.IO.In.ReadInt32()
                        });
                        this.IO.In.BaseStream.Position += 0x24;
                    }
                }
                else
                {
                    for (var x = 0; x < 54; x++)
                    {
                        Inventory.Add(new Inventory.Entry()
                        {
                            ClassIdent = this.IO.In.ReadByte(),
                            Ident = this.IO.In.ReadByte(),
                            Amount = this.IO.In.ReadInt16()
                        });

                        this.IO.In.BaseStream.Position += 0x08;
                    }
                }
            }
        }
        private void WriteInventory(int Offset, List<Inventory.Entry> Inventory, bool Character)
        {
            if (Inventory != null)
            {
                this.IO.Out.SeekTo(Offset);
                if (Character)
                {
                    for (var x = 0; x < 9; x++)
                    {
                        this.IO.Out.WriteInt24(Inventory[x].ClassIdent);
                        this.IO.Out.Write(Inventory[x].Ident);
                        this.IO.Out.Write((int)Inventory[x].Amount);

                        this.IO.Out.BaseStream.Position += 0x24;
                    }
                }
                else
                {
                    for (var x = 0; x < 54; x++)
                    {
                        this.IO.Out.Write(Inventory[x].ClassIdent);
                        this.IO.Out.Write(Inventory[x].Ident);
                        this.IO.Out.Write(Inventory[x].Amount);

                        this.IO.In.BaseStream.Position += 0x08;
                    }
                }
            }
        }
        private void WriteStats()
        {
            this.IO.Out.SeekTo(0xE0);

            this.IO.Out.Write(this.SaveFile.Gold);
            this.IO.Out.Write(this.SaveFile.ExchangePoints);
        }
        public void WriteMainInventory(List<Inventory.Entry> InventoryItems, List<Inventory.Entry> TreasureItems)
        {
            if (InventoryItems != null && InventoryItems.Count > 0)
            {
                this.WriteInventory(0x3158, InventoryItems, false);
            }

            if (TreasureItems != null && TreasureItems.Count > 0)
            {
                this.WriteInventory(0x3158 + 0x288, TreasureInventory, false);
            }
        }
        public void WriteCharacterInventory(List<Inventory.Entry> ChrisInventory, List<Inventory.Entry> ShevaInventory)
        {
            if (ChrisInventory != null && ChrisInventory.Count > 0)
            {
                this.WriteInventory(0x3690, ChrisInventory, true);
            }

            if (ShevaInventory != null && ShevaInventory.Count > 0)
            {
                this.WriteInventory(0x369 + 0x420, ShevaInventory, true);
            }
        }
        private int CalculateChecksum()
        {
            int sum = 0;
            this.IO.In.SeekTo(0x10);
            for (var x = 0; x < 0x4BB0; x += 4)
            {
                sum += this.IO.In.ReadInt32();
            }
            return sum;
        }
    }
}