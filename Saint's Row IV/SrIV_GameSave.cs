using System;
using System.Collections.Generic;
using System.IO;
using Horizon.Functions;

namespace Volition
{
    internal class WeaponEntry
    {
        public uint WeaponIdent;
        public uint Weapon1AmmoClip;
        public uint WeaponAmmoBarrel;
        public uint Weapon2AmmoClip;

        public bool HasInfiniteAmmo;
        public bool IsDualWieldable;

        public byte[] ToArray()
        {
            EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.Out.Write(WeaponIdent);
            io.Out.Write(new byte[4]);
            io.Out.Write(Weapon1AmmoClip);
            io.Out.Write(WeaponAmmoBarrel);
            io.Out.Write(Weapon2AmmoClip);
            io.Out.Write(new byte[4]);
            io.Out.Write(IsDualWieldable);
            io.Out.Write(HasInfiniteAmmo);
            io.Out.Write(new byte[2]);

            return io.ToArray();
        }
    }

    internal class SRIVInventory : List<WeaponEntry>
    {
        internal WeaponEntry this[uint id]
        {
            get { return Find(w => w.WeaponIdent == id); }
        }
    }
    internal enum SRIVPowersEnum
    {
        Telekinesis,
        Stomp,
        Buff,
        Blast,
        ForceShield,
        SuperSprint,
        SuperJump,
        DeathFromAbove
    }

    internal class SRIVPowers : List<bool>
    {
        private const int NumberOfPowers = 8;

        internal bool this[SRIVPowersEnum id]
        {
            get { return this[(int) id]; }
            set { this[(int) id] = value; }
        }

        internal SRIVPowers(EndianReader reader)
        {
            for (int i = 0; i < NumberOfPowers; i++)
            {
                Add(reader.ReadBoolean());
            }
        }
        internal new byte[] ToArray()
        {
            var io = new EndianIO(new byte[NumberOfPowers], EndianType.BigEndian, true );
            for (int i = 0; i < NumberOfPowers; i++)
            {
                io.Out.Write(this[i]);
            }
            return io.ToArray();
        }
    }

    // 'p' stands for placeholder
    internal enum MultipliersTypes
    {
        NotorietyDecayRate,
        SmallArmsDamageRate,
        ExplosiveDamageRate,
        FireDamageRate,
        SprintBonus,
        //StaminaBonus,
        XPMultiplier,
        MeleeStrength,
        HourlyCacheTransferRate
    }

    internal class Multipliers
    {
        internal Dictionary<MultipliersTypes, float> Values = new Dictionary<MultipliersTypes, float>();
        private readonly EndianIO io;

        internal Multipliers(byte[] input)
        {
            io = new EndianIO(input, EndianType.BigEndian, true);
            Values.Add(MultipliersTypes.NotorietyDecayRate, io.In.SeekNReadSingle(0x00));
            Values.Add(MultipliersTypes.SmallArmsDamageRate, io.In.ReadSingle());
            Values.Add(MultipliersTypes.ExplosiveDamageRate, io.In.ReadSingle());
            Values.Add(MultipliersTypes.FireDamageRate, io.In.SeekNReadSingle(0x10));
            Values.Add(MultipliersTypes.SprintBonus, io.In.SeekNReadSingle(0x20));
            Values.Add(MultipliersTypes.XPMultiplier, io.In.SeekNReadSingle(0x2C));
            Values.Add(MultipliersTypes.MeleeStrength, io.In.ReadSingle());
            Values.Add(MultipliersTypes.HourlyCacheTransferRate, io.In.SeekNReadUInt16(0x3E));
        }

        internal byte[] ToArray()
        {
            io.Out.SeekNWrite(0x00, Values[MultipliersTypes.NotorietyDecayRate]);
            io.Out.Write(Values[MultipliersTypes.SmallArmsDamageRate]);
            io.Out.Write(Values[MultipliersTypes.ExplosiveDamageRate]);
            io.Out.SeekNWrite(0x10, Values[MultipliersTypes.FireDamageRate]);
            io.Out.SeekNWrite(0x20, Values[MultipliersTypes.SprintBonus]);
            io.Out.SeekNWrite(0x2C, Values[MultipliersTypes.XPMultiplier]);
            io.Out.Write(Values[MultipliersTypes.MeleeStrength]);
            io.Out.SeekNWrite(0x3E, (ushort)Values[MultipliersTypes.HourlyCacheTransferRate]);
            return io.ToArray();
        }
    }
    internal class SRIVGameSave
    {
        private EndianIO IO;

        internal int Money;
        internal int DataClusters;
        internal bool CheatsEnabled;

        internal SRIVInventory Inventory = new SRIVInventory();
        internal SaintsRowUpgradeData UpgradeData;
        internal SRIVPowers Powers;
        internal List<uint> Cheats = new List<uint>();
        internal Multipliers GlobalMultipliers;
        internal int TotalRespectEarned, LevelUpRespectRequired, RespectLevel;

        internal const int MaxUpgradeCount = 378;

        /* Powers 

        // Active
        internal bool Telekinesis;
        internal bool Stomp;
        internal bool Buff;
        internal bool Blast;

        // Passive
        internal bool ForceShield;
        internal bool SuperSprint;
        internal bool SuperJump;
        internal bool DeathFromAbove;
         
        */

        internal SRIVGameSave(EndianIO io)
        {
            if (io != null)
                IO = io;

            // verify data checksum
            if (CalculateSaveChecksum() != IO.In.SeekNReadUInt32(00))
            {
                throw new Exception("Saints Row 4: save data has been tampered with.");
            }

            // parse save data
            Read();
        }

        private void Read()
        {
            // cheats information
            IO.SeekTo(0xC8);
            CheatsEnabled = IO.In.ReadBoolean();

            int numOfCheats = IO.In.SeekNReadInt32(0xCC);
            for (int i = 0; i < numOfCheats; i++)
            {
                Cheats.Add(IO.In.ReadUInt32());
            }

            // player stats
            IO.SeekTo(0xCE50);
            TotalRespectEarned = IO.In.ReadInt32();
            LevelUpRespectRequired = IO.In.ReadInt32();
            RespectLevel = IO.In.ReadInt32();

            Money = IO.In.SeekNReadInt32(0xD528)/0x64;
            DataClusters = IO.In.SeekNReadInt32(0xD52C);

            // read weapon list
            IO.SeekTo(0xD530);
            var weaponCount = IO.In.ReadInt32();
            for (var i = 0; i < weaponCount; i++)
            {
                var entry = new WeaponEntry {WeaponIdent = IO.In.ReadUInt32()};
                IO.In.BaseStream.Position += 4;
                entry.Weapon1AmmoClip = IO.In.ReadUInt32();
                entry.WeaponAmmoBarrel = IO.In.ReadUInt32();
                entry.Weapon2AmmoClip = IO.In.ReadUInt32();

                IO.In.BaseStream.Position += 4;

                entry.IsDualWieldable = Convert.ToBoolean(IO.In.ReadByte());
                entry.HasInfiniteAmmo = Convert.ToBoolean(IO.In.ReadByte());
                IO.In.BaseStream.Position += 2;

                Inventory.Add(entry);
            }

            // read upgrade data identifiers
            IO.SeekTo(0xCE60);
            var unlockIdents = new List<uint>();
            for (int i = 0; i < MaxUpgradeCount; i++)
            {
                var ident = IO.In.ReadUInt32();
                if (ident == 0)
                    break;

                unlockIdents.Add(ident);
            }

            // read the actual unlock table
            IO.SeekTo(0xD450);
            byte[] unlockTableData = IO.In.ReadBytes(0x30*3);
            UpgradeData = new SaintsRowUpgradeData(unlockTableData, unlockIdents, MaxUpgradeCount, SaintsRowGameType.SaintsRowIV);

            // read rates and multipliers
            IO.SeekTo(0xD4E0);
            GlobalMultipliers = new Multipliers(IO.In.ReadBytes(0x40));

            // read powers
            IO.SeekTo(0x00018628);
            Powers = new SRIVPowers(IO.In);
        }

        internal void Save()
        {
            IO.SeekTo(0xC8);
            IO.Out.Write(CheatsEnabled);

            IO.SeekTo(0xCC);
            IO.Out.Write(Cheats.Count);
            foreach (uint cheat in Cheats)
            {
                IO.Out.Write(cheat);
            }
            
            // write player stats first
            IO.SeekTo(0xCE50);
            IO.Out.Write(TotalRespectEarned);
            IO.Out.Write(LevelUpRespectRequired);
            IO.Out.Write(RespectLevel);

            IO.Out.SeekNWrite(0xD528, Money * 0x64);
            IO.Out.SeekNWrite(0xD52C, DataClusters);

            // write weapon list
            IO.SeekTo(0xD530);
            IO.Out.Write(Inventory.Count);
            foreach (WeaponEntry weaponEntry in Inventory)
            {
                IO.Out.Write(weaponEntry.ToArray());
            }

            // always make sure one slot is "un-armed"

            // so, we advance to the end of the weapon inventory list
            IO.SeekTo(0xD534 + (Inventory.Count * 0x1C));
            // write empty weapon slot
            IO.Out.Write(new byte[0x1C]);
            // all good :)

            // write powers
            IO.SeekTo(0x00018628);
            IO.Out.Write(Powers.ToArray());

            // write upgrade table
            IO.SeekTo(0xD450);
            IO.Out.Write(UpgradeData.ToArray());

            // write multipliers listing
            IO.SeekTo(0xD4E0);
            IO.Out.Write(GlobalMultipliers.ToArray());

            // write back the data checksum
            IO.Out.SeekNWrite(0x00, CalculateSaveChecksum());
        }


        private uint CalculateSaveChecksum()
        {
            IO.In.SeekTo(0x04);
            return Checksum.CRC32.Calculate(IO.In.ReadBytes(IO.Stream.Length - 4), 0x00);
        }
    }


    internal class SRIVTableData
    {
        internal enum InventoryItemCategory
        {
            Rifle,
            SMG,
            Shotgun,
            Pistol,
            Explosive,
            Grenade,
            Melee,
            Special,
            Vehicle,
            TempPickup,
            WieldableProps,
            Other,
            SR2Ranged,
            SR2Melee,
            SR2Misc
        }

        internal struct InventoryItem
        {
            internal string Name;
            internal string DisplayName;
            internal InventoryItemCategory Category;
            internal int Cost;
        }
    }
}
