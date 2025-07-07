using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SaintsRow3
{
    public class SaintsRow3GameSave
    {
        public struct WeaponEntry
        {
            public uint WeaponIdent;
            public uint Weapon1AmmoClip;
            public uint WeaponAmmoBarrel;
            public uint Weapon2AmmoClip;

            public uint Level;
            public bool InfiniteAmmo;
            public bool DualWield;
        }
        public struct GarageEntry
        {
            public int GarageIndex;
            public int DataSize;
            public short Ident;
            public byte Unknown1;
            public byte Flags;
        }

        private EndianIO IO;
        public List<WeaponEntry> Weapons;
        public List<GarageEntry> Vehicles;

        private const int MaxWeaponCount = 54;
        private Dictionary<uint, string> _WeaponList;
        private Dictionary<uint, string> WeaponList
        {
            get
            {
                return _WeaponList;
            }
            set
            {
                if (value != null)
                {
                    _WeaponList = value;
                }
            }
        }
        private Dictionary<short, string> _VehicleList;
        private Dictionary<short, string> VehicleList
        {
            get
            {
                return _VehicleList;
            }
            set
            {
                if (value != null)
                {
                    _VehicleList = value;
                }
            }
        }

        public Dictionary<uint, bool[]> WeaponUpgradeTable;

        private byte[] UpgradesList, UpgradesUnlockTable;
        private const int UpgradeCount = 312;
        public Dictionary<uint, bool> GangAbilities_UnlockTable;

        public int Money;
        public int RespectLevel, TotalRespectEarned, LevelUpRespectRequired;
        public bool UnlimitedSprint = false, UnlockAllWeapons = false, UpgradeAllWeapons = false;

        public SaintsRow3GameSave(EndianIO IO)
        {
            this.IO = IO;

            this.CreateWeaponsList();
            this.CreateVehicleList();

            // verify save data before we load it
            if (CalculateSaveChecksum() != this.IO.In.SeekNReadUInt32(00))
            {
                throw new Exception("Saints Row 3: save data has been tampered with.");
            }
            this.Read();
        }

        private void CreateWeaponsList()
        {
            WeaponList = new Dictionary<uint, string>();

            //Pistol
            WeaponList.Add(0xE89FF93D, "45 Shepherd");
            WeaponList.Add(0xDCF52EE9, "KA-1 Kobra");

            //SMG
            WeaponList.Add(0xB20742BC, "Tek Z-10");
            WeaponList.Add(0x35B6FFCC, "D4TH Blossom");
            WeaponList.Add(0x634F1478, "Cyber Blaster");

            //shotgun
            WeaponList.Add(0x24FE9255, "Grave Digger");
            WeaponList.Add(0xABB1BBF7, "AS3 Ultimax");
            WeaponList.Add(0xF0576D43, "S3X Hammer");
            WeaponList.Add(0xEDAE1C09, "Shark-O-Matic");

            //rifles
            WeaponList.Add(0x11A5E83A, "K-8 Krukov");
            WeaponList.Add(0x316DEFBC, "AR-55");
            WeaponList.Add(0xC50C172C, "Viper Laser Rifle");
            WeaponList.Add(0x55BC6D16, "TOGO-13");

            // explosives
            WeaponList.Add(0xAA1127D0, "Annihilator");
            WeaponList.Add(0x87F2BD7D, "Mollusk Launcher");
            WeaponList.Add(0x21A320A2, "Satchel Charges");

            // grenades
            WeaponList.Add(0x519A6525, "Flashbangs");
            WeaponList.Add(0xF78C3CCF, "Grenades");
            WeaponList.Add(0xDECCDDE8, "Electric Grenades");
            WeaponList.Add(0x24BF8E5E, "Molotovs");

            //Specials
            WeaponList.Add(0xBC21AE05, "Cyber Buster");
            WeaponList.Add(0x64C1DEFE, "Sonic Boom");
            WeaponList.Add(0x5CADD275, "SA-3 Airstrike");
            WeaponList.Add(0x7994319C, "Reaper Drone");
            WeaponList.Add(0x1F6BEBAE, "RC Possessor");
            WeaponList.Add(0xC54E0699, "McManus 2015");

            // temp guns
            WeaponList.Add(0x20479900, "Incinerator");
            WeaponList.Add(0x5C9790F7, "Mini-Gun");
            WeaponList.Add(0x24DB87F6, "GL G20");
            WeaponList.Add(0x1F3D7223, "Shock Hammer");
            WeaponList.Add(0x05C10BA1, "Riot Shield");

            // melee
            WeaponList.Add(0xF9890F00, "The Penetrator");
            WeaponList.Add(0xCBB32BBB, "Apoca-Fist");
            WeaponList.Add(0x8E7E8459, "Woodsman");
            WeaponList.Add(0x35A90295, "Baseball Bat");
            WeaponList.Add(0x9F91EB0F, "Nocturne");
            WeaponList.Add(0x61A134A8, "Stun Gun");
            //other
            WeaponList.Add(0x26F45573, "Bling Shotgun");

            if (WeaponList.Count > 94)
                throw new Exception("Saints Row 3: invalid weapon count.");
        }

        private void CreateVehicleList()
        {
            VehicleList = new Dictionary<short, string>();
            VehicleList.Add(0x00000000, "Kenshin");
            VehicleList.Add(0x00000001, "Miami");
            VehicleList.Add(0x00000002, "Commander");
            VehicleList.Add(0x00000003, "Shark");
            VehicleList.Add(0x00000004, "Halberd");
            VehicleList.Add(0x00000005, "Emu");
            VehicleList.Add(0x00000006, "Attrazione");
            VehicleList.Add(0x00000007, "Vortex");
            VehicleList.Add(0x00000008, "Nelson");
            VehicleList.Add(0x00000009, "Sovereign");
            VehicleList.Add(0x0000000A, "Hammerhead");
            VehicleList.Add(0x0000000B, "Go!");
            VehicleList.Add(0x0000000C, "Bootlegger");
            VehicleList.Add(0x0000000D, "Phoenix");
            VehicleList.Add(0x0000000E, "Torch");
            VehicleList.Add(0x0000000F, "Cosmos");
            VehicleList.Add(0x00000010, "Peacemaker");
            VehicleList.Add(0x00000011, "The Duke");
            VehicleList.Add(0x00000012, "Wakazashi");
            VehicleList.Add(0x00000013, "Neuron");
            VehicleList.Add(0x00000014, "Taxi");
            VehicleList.Add(0x00000015, "Oppressor");
            VehicleList.Add(0x00000016, "Tornado");
            VehicleList.Add(0x00000017, "Vulture");
            VehicleList.Add(0x00000018, "Eagle");
            VehicleList.Add(0x00000019, "Eagle (Variant)");
            VehicleList.Add(0x0000001A, "Stork");
            VehicleList.Add(0x0000001B, "Westbury");
            VehicleList.Add(0x0000001C, "Giant_Plane");
            VehicleList.Add(0x0000001D, "Woodpecker");
            VehicleList.Add(0x0000001E, "ASP");
            VehicleList.Add(0x0000001F, "F-69 VTOL");
            VehicleList.Add(0x00000020, "F-69 VTOL");
            VehicleList.Add(0x00000021, "F-69 VTOL");
            VehicleList.Add(0x00000022, "Condor");
            VehicleList.Add(0x00000023, "N-Forcer");
            VehicleList.Add(0x00000024, "Nordberg");
            VehicleList.Add(0x00000025, "Kayak");
            VehicleList.Add(0x00000026, "Lockdown");
            VehicleList.Add(0x00000027, "Anchor");
            VehicleList.Add(0x00000028, "Keystone");
            VehicleList.Add(0x00000029, "Sandstorm");
            VehicleList.Add(0x0000002A, "Kaneda");
            VehicleList.Add(0x0000002B, "Specter");
            VehicleList.Add(0x0000002C, "Ultor Interceptor");
            VehicleList.Add(0x0000002D, "Widowmaker");
            VehicleList.Add(0x0000002E, "Justice");
            VehicleList.Add(0x0000002F, "Infuego");
            VehicleList.Add(0x00000030, "Ambulance");
            VehicleList.Add(0x00000031, "Bear");
            VehicleList.Add(0x00000032, "Knoxville");
            VehicleList.Add(0x00000033, "Status Quo");
            VehicleList.Add(0x00000034, "Ball");
            VehicleList.Add(0x00000035, "Challenger");
            VehicleList.Add(0x00000036, "Quasar");
            VehicleList.Add(0x00000037, "Titan");
            VehicleList.Add(0x00000038, "Scrubber");
            VehicleList.Add(0x00000039, "Steelport Municipal");
            VehicleList.Add(0x0000003A, "Mule");
            VehicleList.Add(0x0000003B, "Thorogood");
            VehicleList.Add(0x0000003C, "Compensator");
            VehicleList.Add(0x0000003D, "Criminal");
            VehicleList.Add(0x0000003E, "Temptress");
            VehicleList.Add(0x0000003F, "Blade");
            VehicleList.Add(0x00000040, "Zimos");
            VehicleList.Add(0x00000041, "AB Destroyer");
            VehicleList.Add(0x00000042, "Solar");
            VehicleList.Add(0x00000043, "Reaper");
            VehicleList.Add(0x00000044, "Atlantica");
            VehicleList.Add(0x00000045, "Estrada");
            VehicleList.Add(0x00000046, "Raycaster");
            VehicleList.Add(0x00000047, "Churchill");
            VehicleList.Add(0x00000048, "Thompson");
            VehicleList.Add(0x00000049, "Snipes 57");
            VehicleList.Add(0x0000004A, "Alaskan");
            VehicleList.Add(0x0000004B, "Bulldog");
            VehicleList.Add(0x0000004C, "Toad");
            VehicleList.Add(0x0000004D, "Donovan");
            VehicleList.Add(0x0000004E, "Longhauler");
            VehicleList.Add(0x0000004F, "Blaze");
            VehicleList.Add(0x00000050, "Peterliner");
            VehicleList.Add(0x00000051, "Side Shooter");
            VehicleList.Add(0x00000052, "Crusader");
            VehicleList.Add(0x00000053, "Saints Raider");
            VehicleList.Add(0x00000054, "Cargo_Heist");
            VehicleList.Add(0x00000055, "Pony Cart");
            VehicleList.Add(0x00000056, "Bulldog (Test)");
            VehicleList.Add(0x00000057, "Genki Manapult");
            VehicleList.Add(0x00000058, "Flatbed");
            VehicleList.Add(0x00000059, "Helims05");
            VehicleList.Add(0x0000005A, "Gat Mobile");
            VehicleList.Add(0x0000005B, "Hammer");
            VehicleList.Add(0x0000005C, "Car 57");
            VehicleList.Add(0x0000005D, "Giant_Plane2");
            VehicleList.Add(0x0000005E, "Wraith");
            VehicleList.Add(0x0000005F, "Cargo2_M9");
            VehicleList.Add(0x00000060, "X-2 Phantom");
            VehicleList.Add(0x00000061, "Crusader (Prototype)");
            VehicleList.Add(0x00000062, "N-Forcer (Test)");
            VehicleList.Add(0x00000063, "Saints VTOL");
            VehicleList.Add(0x00000064, "Saints N-Forcer");
            VehicleList.Add(0x00000065, "Saints Crusader");
            VehicleList.Add(0x00000066, "Bloody Cannoness");
            VehicleList.Add(0x00000067, "Nyte Blayde");
        }

        private uint CalculateSaveChecksum()
        {
            this.IO.In.SeekTo(0x04);
            return Checksum.CRC32.Calculate(this.IO.In.ReadBytes(0x1A9FC), 0x00);
        }

        private void Read()
        {
            IO.In.SeekTo(0x4404);
            var allyCount = IO.In.ReadInt16();
            IO.In.SeekTo(0x4324);
            GangAbilities_UnlockTable = new Dictionary<uint, bool>();
            for (int x = 0; x < allyCount; x++)
            {                
                GangAbilities_UnlockTable.Add(IO.In.ReadUInt32(),  (IO.In.ReadInt32() & 1) == 1);
            }

            IO.In.SeekTo(0x4408);
            TotalRespectEarned = IO.In.ReadInt32();
            LevelUpRespectRequired = IO.In.ReadInt32();
            RespectLevel = IO.In.ReadInt32();
            IO.In.BaseStream.Position += 4;

            // read upgrade tables
            UpgradesList = IO.In.ReadBytes(UpgradeCount * 4);
            IO.In.SeekTo(0x4990);
            UpgradesUnlockTable = IO.In.ReadBytes(0x2C * 3);
            IO.In.SeekTo(0x4A40);
            UnlimitedSprint = IO.In.ReadSingle() < 0;
            this.Money = this.IO.In.SeekNReadInt32(0x4A60) / 0x64;

            // read weapons cache
            this.Weapons = new List<WeaponEntry>();
            uint weaponCount = this.IO.In.ReadUInt32();
            if (weaponCount > 0 && weaponCount <= MaxWeaponCount)
            {
                for (int x = 0; x < weaponCount; x++)
                {
                    WeaponEntry entry = new WeaponEntry {WeaponIdent = this.IO.In.ReadUInt32()};
                    this.IO.In.BaseStream.Position += 4;
                    entry.Weapon1AmmoClip = this.IO.In.ReadUInt32();
                    entry.WeaponAmmoBarrel = this.IO.In.ReadUInt32();
                    entry.Weapon2AmmoClip = this.IO.In.ReadUInt32();
                    IO.In.BaseStream.Position += 4;
                    entry.DualWield = Convert.ToBoolean(this.IO.In.ReadByte());
                    entry.InfiniteAmmo = Convert.ToBoolean(this.IO.In.ReadByte());
                    this.IO.In.BaseStream.Position += 2;

                    this.Weapons.Add(entry);
                }
            }
            WeaponUpgradeTable = new Dictionary<uint, bool[]>();
            for (int x = 0; x < 0x32; x++)
            {
                IO.In.SeekTo(0x4BB8 + (x * 4));
                uint ident = 0;
                if ((ident = IO.In.ReadUInt32()) != 0)
                {
                    IO.In.SeekTo(0x4C80 + (x * 4));
                    WeaponUpgradeTable.Add(ident, Horizon.Functions.BitHelper.ProduceBitmask(IO.In.ReadInt32()));
                }
            }
            //read garage vehicles
            IO.In.SeekTo(0xB750);
            this.Vehicles = new List<GarageEntry>();
            int vehicleCount = IO.In.ReadInt32();
            if (vehicleCount > 0)
            {
                for (int x = 0; x < vehicleCount; x++)
                {
                    GarageEntry vehicle = new GarageEntry
                        {
                            GarageIndex = this.IO.In.ReadInt32(),
                            DataSize = this.IO.In.ReadInt32()
                        };
                    this.IO.In.BaseStream.Position += 4;
                    vehicle.Ident = this.IO.In.ReadInt16();
                    this.IO.In.BaseStream.Position += 4;
                    vehicle.Unknown1 = this.IO.In.ReadByte();
                    IO.In.BaseStream.Position += (0x20 + 0x39);
                    vehicle.Flags = this.IO.In.ReadByte();
                    IO.In.BaseStream.Position += 3;
                    this.Vehicles.Add(vehicle);
                }
            }
        }

        public byte[][] GetUpgradeData()
        {
            return new byte[2][] { this.UpgradesList, UpgradesUnlockTable};            
        }

        public string GetWeaponNameFromIdent(uint ident)
        {
            if (WeaponList.ContainsKey(ident))
                return WeaponList[ident];

            return string.Format("Unknown [{0:X4}]", ident);
        }

        public string GetVehicleNameFromIdent(short ident)
        {
            if (VehicleList.ContainsKey(ident))
                return VehicleList[ident];

            return string.Format("Unknown [{0:X4}]", ident);
        }

        public void SetWeaponsList(List<WeaponEntry> newWeaponList)
        {
            if (newWeaponList != null && newWeaponList.Count > 0)
            {
                this.Weapons.Clear();
                this.Weapons = new List<WeaponEntry>(newWeaponList);
            }
        }

        public void SetVehicleList(List<GarageEntry> newVehicleList)
        {
            if (newVehicleList != null)
            {
                this.Vehicles.Clear();
                this.Vehicles = new List<GarageEntry>(newVehicleList);
            }
        }

        public void Save()
        {
            if (UnlimitedSprint)
            {
                IO.Out.SeekTo(0x4A40);
                IO.Out.Write(0xBC23D70A);
            }
            if (UnlockAllWeapons)
            {
                IO.Out.SeekTo(0x00004F4C);
                for (int x = 0; x < (0x52 / 8) + 1; x++)
                    IO.Out.WriteByte(0xFF);
            }
            if (UpgradeAllWeapons)
            {
                Weapons_MaxAllWeaponLevels();
            }

            Weapons_WriteWeaponLevels();
            
            if (GangAbilities_UnlockTable.Count > 0)
            {
                IO.Out.SeekTo(0x4324);
                foreach(KeyValuePair<uint, bool> gang_ability in GangAbilities_UnlockTable)
                {
                    if (IO.In.ReadUInt32() == gang_ability.Key)
                    {
                        IO.Out.Write(Convert.ToInt32(gang_ability.Value));
                    }
                }
            }
            IO.Out.SeekTo(0x4408);
            IO.Out.Write(TotalRespectEarned);
            IO.Out.Write(LevelUpRespectRequired);
            IO.Out.Write(RespectLevel);
            this.IO.Out.SeekTo(0x4A60);
            this.IO.Out.Write(this.Money * 0x64);
            this.WriteWeaponsList();

            // wriite back save checksum
            uint sum = CalculateSaveChecksum();
            this.IO.Out.SeekTo(0x00);
            this.IO.Out.Write(sum);
        }
        private void Weapons_MaxAllWeaponLevels()
        {
            foreach (KeyValuePair<uint, string> weapon in this.WeaponList)
            {
                if (!this.WeaponUpgradeTable.ContainsKey(weapon.Key))
                {
                    this.WeaponUpgradeTable.Add(weapon.Key, Horizon.Functions.BitHelper.ProduceBitmask(0x07));
                }
                else
                {
                    this.WeaponUpgradeTable[weapon.Key] = Horizon.Functions.BitHelper.ProduceBitmask(0x07);
                }
            }
        }
        private void Weapons_WriteWeaponLevels()
        {
            for (int x = 0; x < this.WeaponUpgradeTable.Count; x++)
            {
                IO.Out.SeekTo(0x4BB8 + (x * 4));
                IO.Out.Write(this.WeaponUpgradeTable.ElementAt(x).Key);
                IO.Out.SeekTo(0x4C80 + (x * 4));
                IO.Out.Write(Horizon.Functions.BitHelper.ConvertToWriteableInteger(this.WeaponUpgradeTable.ElementAt(x).Value));
            }
        }

        private void WriteWeaponsList()
        {
            this.IO.Out.Write(this.Weapons.Count);
            foreach(WeaponEntry entry in this.Weapons)
            {
                this.IO.Out.Write(entry.WeaponIdent);
                this.IO.Out.BaseStream.Position += 4;
                this.IO.Out.Write(entry.Weapon1AmmoClip);
                this.IO.Out.Write(entry.WeaponAmmoBarrel);
                this.IO.Out.Write(entry.Weapon2AmmoClip);
                this.IO.Out.BaseStream.Position += 4;
                this.IO.Out.WriteByte(entry.DualWield);
                this.IO.Out.WriteByte(entry.InfiniteAmmo);
                this.IO.Out.BaseStream.Position += 2;
            }
        }

        private void WriteGarageList()
        {
            if (this.Vehicles.Count > 0)
            {
                IO.Out.SeekTo(0xB750);
                IO.Out.Write(this.Vehicles.Count);
                foreach (GarageEntry vehicle in this.Vehicles)
                {
                    IO.Out.Write(vehicle.GarageIndex);
                    IO.Out.BaseStream.Position += 8;
                    IO.Out.Write(vehicle.Ident);
                    IO.Out.BaseStream.Position += 4;
                    IO.Out.Write(vehicle.Unknown1);
                    IO.Out.BaseStream.Position += (0x20 + 0x39);
                    long FlagPosition = IO.Out.BaseStream.Position;
                    byte Flag = IO.In.SeekNReadByte(FlagPosition);
                    IO.Out.SeekTo(FlagPosition);
                    IO.Out.Write(Flag | vehicle.Flags);
                    IO.Out.BaseStream.Position += 3;
                }
            }
        }

        public bool Vehicle_InsertVehicleData(short OriginalVehicleId, int VehicleIndex, byte[] VehicleBuffer)
        {
            if (OriginalVehicleId == VehicleBuffer.ReadInt16(0x0C))
            {
                int vehicleIndex = VehicleBuffer.ReadInt32(0);
                if (vehicleIndex <= this.Vehicles.Count && (vehicleIndex == VehicleIndex))
                {
                    IO.Out.SeekTo(0xB754 + ((VehicleIndex - 1) * 0x70));
                    if(VehicleBuffer.Length == 0x70)
                        IO.Out.Write(VehicleBuffer);

                    return true;
                }
            }
            return false;
        }
        public byte[] Vehicle_ExtractVehicleData(int VehicleIndex)
        {
            if (VehicleIndex <= this.Vehicles.Count)
            {
                IO.In.SeekTo(0xB754  + (VehicleIndex * 0x70));
                return IO.In.ReadBytes(0x70);
            }
            return null;
        }

        public void SaveUpgradeTable(byte[] TableBuffer, bool IsDiamond)
        {
            /* Unlocks
                IO.Out.SeekTo(0x4990);
                IO.Out.Write(TableBuffer);
            */
            // Available
            if (IsDiamond)
            {
                IO.Out.SeekTo(0x49BC);
                IO.Out.Write(TableBuffer);
            }
        }
    }
}