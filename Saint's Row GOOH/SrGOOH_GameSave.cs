using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Volition
{
    internal enum SRGOOHPowersEnum
    {
        Summons,
        Stomp,
        Aura,
        Blast,
        ForceShield,
        SuperSprint,
        Flight,
        DeathFromAbove
    }
    internal class SRGOOHPowers : List<bool>
    {
        private const int NumberOfPowers = 8;

        internal bool this[SRGOOHPowersEnum id]
        {
            get { return this[(int)id]; }
            set { this[(int)id] = value; }
        }

        internal SRGOOHPowers(EndianReader reader)
        {
            for (int i = 0; i < NumberOfPowers; i++)
            {
                Add(reader.ReadBoolean());
            }
        }
        internal new byte[] ToArray()
        {
            var io = new EndianIO(new byte[NumberOfPowers], EndianType.BigEndian, true);
            for (int i = 0; i < NumberOfPowers; i++)
            {
                io.Out.Write(this[i]);
            }
            return io.ToArray();
        }
    }
    internal enum SRGOOHMultipliersTypes
    {
        NotorietyDecayRate,
        SmallArmsDamageRate,
        ExplosiveDamageRate,
        FireDamageRate,
        MeleeDamageRate,
        SprintBonus,
        StaminaBonus,
        HealthRegenerationRate,
        XP_Multiplier,
        MeleeStrength,
        HourlyCacheTransferRate
    }
    internal class SRGOOHMultipliers
    {
        internal Dictionary<SRGOOHMultipliersTypes, float> Values = new Dictionary<SRGOOHMultipliersTypes, float>();
        private readonly EndianIO io;

        internal SRGOOHMultipliers(byte[] input)
        {
            io = new EndianIO(input, EndianType.BigEndian, true);
            Values.Add(SRGOOHMultipliersTypes.NotorietyDecayRate, io.In.SeekNReadSingle(0x00));

            //io.In.SeekTo(0x04);
            Values.Add(SRGOOHMultipliersTypes.SmallArmsDamageRate, io.In.ReadSingle()); // 0x04
            Values.Add(SRGOOHMultipliersTypes.ExplosiveDamageRate, io.In.ReadSingle()); // 0x08
            //io.Position += 4;
            Values.Add(SRGOOHMultipliersTypes.FireDamageRate, io.In.SeekNReadSingle(0x10));
            //io.Position += 4;
            Values.Add(SRGOOHMultipliersTypes.MeleeDamageRate, io.In.SeekNReadSingle(0x18));
            //io.Position += 4;
            Values.Add(SRGOOHMultipliersTypes.HealthRegenerationRate, io.In.SeekNReadSingle(0x20));

            Values.Add(SRGOOHMultipliersTypes.StaminaBonus, io.In.SeekNReadSingle(0x24));

            Values.Add(SRGOOHMultipliersTypes.XP_Multiplier, io.In.SeekNReadSingle(0x30));

            Values.Add(SRGOOHMultipliersTypes.MeleeStrength, io.In.SeekNReadSingle(0x34));

            //Values.Add(SRGOOHMultipliersTypes.HourlyCacheTransferRate, io.In.SeekNReadUInt16(0x3E));
        }

        internal byte[] ToArray()
        {
            io.Out.SeekNWrite(0x00, Values[SRGOOHMultipliersTypes.NotorietyDecayRate]);

            io.Out.Write(Values[SRGOOHMultipliersTypes.SmallArmsDamageRate]);
            io.Out.Write(Values[SRGOOHMultipliersTypes.ExplosiveDamageRate]);

            io.Out.SeekNWrite(0x10, Values[SRGOOHMultipliersTypes.FireDamageRate]);
            io.Out.SeekNWrite(0x18, Values[SRGOOHMultipliersTypes.MeleeDamageRate]);

            io.Out.SeekNWrite(0x20, Values[SRGOOHMultipliersTypes.HealthRegenerationRate]);

            
            io.Out.SeekNWrite(0x24, Values[SRGOOHMultipliersTypes.StaminaBonus]);
            io.Out.SeekNWrite(0x30, Values[SRGOOHMultipliersTypes.XP_Multiplier]);
            io.Out.SeekNWrite(0x34, Values[SRGOOHMultipliersTypes.MeleeStrength]);

            //io.Out.SeekNWrite(0x3E, (ushort)Values[SRGOOHMultipliersTypes.HourlyCacheTransferRate]);
            return io.ToArray();
        }
    }
    internal class SRGOOHGameSave
    {
        private EndianIO IO;
        internal int Money;
        internal int SoulClusters;
        internal bool CheatsEnabled;
        //internal List<uint> Cheats = new List<uint>();

        internal SRIVInventory Inventory = new SRIVInventory();
        internal SaintsRowUpgradeData UpgradeData;
        internal const int MaxUpgradeCount = 390;
        internal int TotalExperience, WrathLevel;
        internal SRGOOHPowers Powers;
        internal SRGOOHMultipliers GlobalMultipliers;

        internal SRGOOHGameSave(EndianIO io)
        {
            if(io == null)
                throw new Exception("invalid I/O detected!");

            IO = io;

            // verify data checksum
            if (CalculateSaveChecksum() != IO.In.SeekNReadUInt32(00))
            {
                throw new Exception("Saints Row GOOH: save data has been tampered with.");
            }

            Read();
        }

        internal void Save()
        {
            /*
            // write cheats enabled flag for save data
            IO.SeekTo(0xC8);
            IO.Out.Write(CheatsEnabled);

            // write cheats table
            IO.SeekTo(0xCC);
            IO.Out.Write(Cheats.Count);
            foreach (uint cheat in Cheats)
            {
                IO.Out.Write(cheat);
            }
            */

            // write back player stats
            IO.SeekTo(0xF888);
            IO.Out.Write(TotalExperience);
            IO.Out.Write(TotalExperience);
            IO.Out.Write(WrathLevel);

            // write money and clusters data
            IO.SeekTo(0xFF90);
            IO.Out.Write(Money * 100);
            IO.Out.Write(SoulClusters);

            // write upgrade table
            IO.SeekTo(0x0000FEB0);
            IO.Out.Write(UpgradeData.ToArray());

            // write weapon list
            IO.SeekTo(0xFF98);
            IO.Out.Write(Inventory.Count);
            foreach (WeaponEntry weaponEntry in Inventory)
            {
                IO.Out.Write(weaponEntry.ToArray());
            }
            // write powers
            IO.SeekTo(0x0001B090);
            IO.Out.Write(Powers.ToArray());

            // write multipliers listing
            IO.SeekTo(0xFF44);
            IO.Out.Write(GlobalMultipliers.ToArray());

            // we always make sure one slot is "un-armed"
            // so, we advance to the end of the weapon inventory list
            IO.SeekTo(0xFF9C + (Inventory.Count * 0x1C));
            // write empty weapon slot
            IO.Out.Write(new byte[0x1C]);
            // all good :)

            // write back the data checksum
            IO.Out.SeekNWrite(0x00, CalculateSaveChecksum());
        }

        private void Read()
        {
            // read player stats
            IO.SeekTo(0xF888);
            TotalExperience = IO.In.ReadInt32();
            IO.Position += 4;
            WrathLevel = IO.In.ReadInt32();

            // read money and soul clusters
            Money = IO.In.SeekNReadInt32(0xFF90) / 100;
            SoulClusters = IO.In.ReadInt32();

            // read powers
            IO.SeekTo(0x1B090);
            Powers = new SRGOOHPowers(IO.In);
            /*
            // read cheats data (WIP)
            IO.SeekTo(0xC8);
            CheatsEnabled = IO.In.ReadBoolean();

            var numOfCheats = IO.In.SeekNReadInt32(0xCC);
            for (var i = 0; i < numOfCheats; i++)
            {
                Cheats.Add(IO.In.ReadUInt32());
            }
            */
            // read inventory list
            IO.SeekTo(0xFF98);
            var weaponCount = IO.In.ReadInt32();
            for (var i = 0; i < weaponCount; i++)
            {
                var entry = new WeaponEntry { WeaponIdent = IO.In.ReadUInt32() };
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

            // read the list of unlocks for their identifiers
            IO.SeekTo(0xF898);
            var unlockIdents = new List<uint>();
            for (int i = 0; i < MaxUpgradeCount; i++)
            {
                var ident = IO.In.ReadUInt32();
                if(ident == 0)
                    break;

                unlockIdents.Add(ident);
            }

            // read the actual unlock table
            IO.SeekTo(0x0000FEB0);
            byte[] unlockTableData = IO.In.ReadBytes(0x31 * 3);
            UpgradeData = new SaintsRowUpgradeData(unlockTableData, unlockIdents, MaxUpgradeCount, SaintsRowGameType.SaintsRowGOOH);

            // read rates and multipliers
            IO.SeekTo(0xFF44);
            GlobalMultipliers = new SRGOOHMultipliers(IO.In.ReadBytes(0x40));
        }

        private uint CalculateSaveChecksum()
        {
            IO.In.SeekTo(0x04);
            return Checksum.CRC32.Calculate(IO.In.ReadBytes(IO.Stream.Length - 4), 0x00);
        }
    }
}
