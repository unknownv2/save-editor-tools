using System;
using System.Collections.Generic;
using System.IO;

namespace SleepingDogs
{
    public class SleepingDogsSave
    {
        internal struct PsStatTableList
        {
            internal long Address;
            internal int Count;
            internal int EndOfEntry;
        }
        //internal struct PsStatEntryList
        //{
        //    internal uint Id;
        //    internal long Address;
        //}

        private EndianIO IO;
        private EndianIO SaveIO;

        internal PsGameStats PlayerStats;

        internal bool CollectAll;

        internal List<PsStatTableList> StatTableList;
        private Dictionary<uint, long> StatEntryList; // PsStatEntryList
 
        internal const int StatTableCount = 0x10;
        public SleepingDogsSave(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            //verify save data
            if (CalculateSum() != IO.In.SeekNReadUInt32(IO.In.SeekNReadInt32(0x30) - 4))
                throw new Exception("Sleeping Dogs: invalid save data detected!");

            var saveDataLen = IO.In.SeekNReadInt32(0x30);
            SaveIO = new EndianIO(IO.In.ReadBytes(saveDataLen), EndianType.BigEndian, true);

            // read stat data entry table
            StatTableList = new List<PsStatTableList>();
            for (var i = 0; i < StatTableCount; i++)
            {
                StatTableList.Add(new PsStatTableList { Address = SaveIO.Position, Count = SaveIO.In.ReadInt32(), EndOfEntry = SaveIO.In.ReadInt32()});
            }
            SaveIO.SeekTo(0x80);
            StatEntryList = new Dictionary<uint, long>();
            foreach (var psStatTableList in StatTableList)
            {
                for (var i = 0; i < psStatTableList.Count; i++)
                {
                    SaveIO.Position += 0x0C;
                    var id = SaveIO.In.ReadUInt32();
                    var pos = SaveIO.Position + SaveIO.In.ReadInt32();
                    StatEntryList.Add(id, pos) ;
                }
            }

            var stats = new Dictionary<string, GameStat>
                {
                    {"HK", new GameStat {DataType = SettingsDataType.Int32, Address = StatEntryList[0xC4D582F2]}},
                    {"PoliceXP", new GameStat {DataType = SettingsDataType.Int32, Address = StatEntryList[0xB723C6CA]}},
                    {"TriadXP", new GameStat {DataType = SettingsDataType.Int32, Address = StatEntryList[0xF1FC0263]}},
                    {"FaceXP", new GameStat {DataType = SettingsDataType.Int32, Address = StatEntryList[0xF96E9138]}},
                    {"CurrentHealth", new GameStat {DataType = SettingsDataType.Float, Address = StatEntryList[0x43AA4772] + 0x04}},
                    {"MeleeTrainingPoints", new GameStat { DataType = SettingsDataType.Int32, Address = StatEntryList[0xBC790A40]}},
                    {"TriadPoints", new GameStat { DataType = SettingsDataType.Int32, Address = StatEntryList[0x9C7185D3]}},
                    {"CopPoints", new GameStat { DataType = SettingsDataType.Int32, Address = StatEntryList[0xB8DC9803]}}
                };

            var upgradeAddressStart = StatEntryList[0x504AD7B0];
            // Create Melee Upgrades
            var upgrades = new [] { "TackleStrike", "LegBreak", "DisarmTackle", "StunGrappleFollowUp", "ArmBreak" };
            AddGameStats(upgrades, upgradeAddressStart, stats);
            upgrades = new [] {"SweepKick", "ChargeKneeStun", "SunStrikeFollowUp", "SpinningHeelKick", "JumpingPowerRoundhouseKick", "DoubleJumpKick", "DimMak"};
            AddGameStats(upgrades, upgradeAddressStart+=4, stats);
            // Triad Upgrades
            upgrades = new [] {"StrikeResistance", "RisingKick", "MeleeWeaponResistance", "CounterRecovery", "ClimbingElbowStrike"};
            AddGameStats(upgrades, upgradeAddressStart += 4, stats);
            upgrades = new [] {"StrikeDamageBonus", "SupriseExchange", "MeleeWeaponSprintAttacks", "ChargeKickFollowup", "MeleeWeaponBoost"};
            AddGameStats(upgrades, upgradeAddressStart += 4, stats);
            // create Police Upgrade stat entries
            upgrades = new[] {"SlimJim", "ActionDismount", "ActionHijackReducedHeat", "IncreasedRammingDamage", "PoliceTrunkKey"};
            AddGameStats(upgrades, upgradeAddressStart += 8, stats);
            upgrades = new [] {"FastDisarm", "SlowMotionFollowUp", "RecoilCompensator", "IncreasedFocus", "OverpressureAmmo"};
            AddGameStats(upgrades, upgradeAddressStart + 4, stats);

            PlayerStats = new PsGameStats(stats, SaveIO);
        }
        private static void AddGameStats(string[] statArray, long address, Dictionary<string, GameStat> gameStats)
        {
            for (var i = 0; i < statArray.Length; i++)
                gameStats.Add(statArray[i],new GameStat { Address = address, BitIndex = i, DataType = SettingsDataType.Bit });
        }

        public void Save()
        {
            if (CollectAll)
            {
                SetUnlockForItem(0xC4CDAAA6);
                SetUnlockForItem(0x5133AFCD);
                SetUnlockForItem(0xB8E0E139);
                SetUnlockForItem(0x3CD97790);
                SetUnlockForItem(0xE96F08F5);
                SetUnlockForItem(0x7C910D9E);
                SetUnlockForItem(0x9542436A);
                SetUnlockForItem(0x117BD5C3);
                SetUnlockForItem(0x64992F59);
            }
            // save player stats
            PlayerStats.Save();

            // write back the save data before the final save
            IO.SeekTo(0x34);
            IO.Out.Write(SaveIO.ToArray());

            // fix checksum
            IO.Out.SeekNWrite((IO.In.SeekNReadInt32(0x30) - 4), CalculateSum());
        }
        private void SetUnlockForItem(uint id)
        {
            SaveIO.SeekTo(StatEntryList[id] + 4);
            var count = SaveIO.In.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                var ct = SaveIO.In.ReadUInt64();
                SaveIO.Position -= 8;
                SaveIO.Out.Write(ct | 0x1000000);
            }
        }
        private uint CalculateSum()
        {
            IO.In.SeekTo(0x30);
            var slength = IO.In.ReadInt32();
            var sdata = IO.In.ReadBytes(slength - 0x38);
            return ElectronicArts.EACRC32.Calculate_AltNoXor(sdata, sdata.Length, 0xFFFFFFFF);
        }

        public void Inject(byte[] data)
        {
            IO.SeekTo(0x00);
            IO.Out.Write(data);
        }
    }


    internal class PsGameStats : GameStats
    {
        public PsGameStats(Dictionary<string, GameStat> stats, EndianIO io) : base(stats, io)
        {
            
        }

        public void Save()
        {
            SaveStats();
        }
    }
    internal class GameStat
    {
        internal SettingsDataType DataType;
        public long Address;
        public object Value;
        public int BitIndex;
    }

    internal enum SettingsDataType
    {
        Empty,
        Int32,
        Int64,
        Double,
        String,
        Float,
        Blob,
        DateTime,
        Bit
    }
    internal  abstract class GameStats
    {
        public int this[string key]
        {         
            get
            {
                var map = StatMap[key];

                if (map == null)
                    return 0;

                if (map.DataType != SettingsDataType.Int32)
                    throw new Exception();

                return (int)map.Value;
            }
            set
            {
                var map = StatMap[key];

                if (map.DataType != SettingsDataType.Int32)
                    throw new Exception();

                map.Value = value;
            }
        }

        //protected readonly List<GameStat> Stats;

        private readonly Dictionary<string, GameStat> StatMap;

        protected EndianIO IO;

        internal GameStats(Dictionary<string, GameStat> statMap, EndianIO io)
        {
            IO = io;

            StatMap = statMap;

            foreach (var gameStat in statMap.Values)
            {
                io.SeekTo(gameStat.Address);
                switch (gameStat.DataType)
                {
                    case SettingsDataType.Empty:
                        gameStat.Value = null;
                        break;
                    case SettingsDataType.Int32:
                        gameStat.Value = io.In.ReadInt32();
                        break;
                    case SettingsDataType.Int64:
                        gameStat.Value = io.In.ReadInt64();
                        break;
                    case SettingsDataType.Double:
                        gameStat.Value = io.In.ReadDouble();
                        break;
                    case SettingsDataType.String:
                        gameStat.Value = io.In.ReadStringNullTerminated();
                        break;
                    case SettingsDataType.Float:
                        gameStat.Value = io.In.ReadSingle();
                        break;
                    case SettingsDataType.Blob:
                        gameStat.Value = io.In.ReadBytes(io.In.ReadInt32());
                        break;
                    case SettingsDataType.Bit:
                        gameStat.Value = BitIsSet(IO.In.SeekNReadInt32(gameStat.Address), gameStat.BitIndex);
                        break;
                    default:
                        throw new Exception(string.Format("GameStats: Unknown data type [0x{0:X2}]",
                                                          (byte) gameStat.DataType));
                }
            }
        }

        internal void Set(string key, object value)
        {
            var map = StatMap[key];

            if (map == null)
            {
                map = new GameStat();

                if (value == null)
                    map.DataType = SettingsDataType.Empty;
                else if (value is int || value is Enum)
                    map.DataType = SettingsDataType.Int32;
                else if (value is long)
                    map.DataType = SettingsDataType.Int64;
                else if (value is float)
                    map.DataType = SettingsDataType.Float;
                else if (value is double)
                    map.DataType = SettingsDataType.Double;
                else if (value is string)
                    map.DataType = SettingsDataType.String;
                else if (value is byte[])
                    map.DataType = SettingsDataType.Blob;
                else if (value is DateTime)
                    map.DataType = SettingsDataType.DateTime;
                else
                    throw new Exception(string.Format("GameStats: Unknown data type ({0})", map.Value.GetType()));

                StatMap.Add(key, map);
            }

            map.Value = value;
        }

        protected object ObjectValue(string key)
        {
            var map = StatMap[key];

            return map == null ? null : map.Value;
        }

        protected int Int32Value(string key)
        {
            var map = StatMap[key];

            if (map == null)
                return 0;

            return (int)map.Value;
        }

        protected long Int64Value(string key)
        {
            var map = StatMap[key];

            if (map == null)
                return 0;

            return (long)map.Value;
        }

        protected double DoubleValue(string key)
        {
            var map = StatMap[key];

            if (map == null)
                return 0;

            return (double)map.Value;
        }

        protected string StringValue(string key)
        {
            var map = StatMap[key];

            if (map == null)
                return "";

            return (string)map.Value;
        }

        internal float FloatValue(string key)
        {
            var map = StatMap[key];

            if (map == null)
                return 0;

            return (float)map.Value;
        }

        internal byte[] BlobValue(string key)
        {
            var map = StatMap[key];

            if (map == null)
                return null;

            return (byte[])map.Value;
        }

        protected DateTime DateTimeValue(string key)
        {
            var map = StatMap[key];

            if (map == null)
                return DateTime.MinValue;

            return (DateTime)map.Value;
        }

        internal bool BitValue(string key)
        {
            var map = StatMap[key];
            return (bool) map.Value;
        }

        protected bool BitIsSet(int integerValue, int bitIndex)
        {
            return (integerValue & (1 << bitIndex)) != 0;
        }

        protected int GetNumBitsSet(string key)
        {
            var bitCount = 0;
            var val = Int32Value(key);

            while (val != 0)
            {
                val &= val - 1;
                bitCount++;
            }

            return bitCount;
        }

        internal void SaveStats()
        {
            foreach (var gameStat in StatMap.Values)
            {
                IO.SeekTo(gameStat.Address);
                switch (gameStat.DataType)
                {
                    case SettingsDataType.Int32:
                        IO.Out.Write((int)gameStat.Value);
                        break;
                    case SettingsDataType.Int64:
                        IO.Out.Write((long)gameStat.Value);
                        break;
                    case SettingsDataType.Float:
                        IO.Out.Write((float)gameStat.Value);
                        break;
                    case SettingsDataType.Double:
                        IO.Out.Write((double)gameStat.Value);
                        break;
                    case SettingsDataType.String:
                        var str = (string)gameStat.Value;
                        if(str.Length != 0)
                            IO.Out.WriteAsciiString(str, str.Length);
                        break;
                    case SettingsDataType.Blob:
                        var arr = (byte[])gameStat.Value;
                        IO.Out.Write(arr.Length);
                        IO.Out.Write(arr);
                        break;
                    case SettingsDataType.Bit:
                        var array = IO.In.SeekNReadInt32(gameStat.Address);
                        IO.Out.SeekNWrite(gameStat.Address, (bool)gameStat.Value ? array | (1 << gameStat.BitIndex) : array & ~(1 << gameStat.BitIndex));
                        break;
                }
            }
        }
    }
}