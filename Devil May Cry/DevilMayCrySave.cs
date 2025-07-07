using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace DevilMayCry
{
    public abstract class DevilMayCrySaveFile
    {
        public EndianIO IO { get; set; }
        protected EndianIO SaveIO;

        protected DevilMayCrySaveFile(EndianIO io)
        {
            IO = io;

            Verify();
        }

        protected virtual void Read()
        {

        }

        public byte[] Extract()
        {
            if (SaveIO == null)
                throw new DevilGameException("ivalid save data stream.");

            SaveIO.SeekTo(0x18);
            int len = SaveIO.In.ReadInt32();
            if(len > SaveIO.Length - 0x1C)
                throw new DevilGameException("invalid save data length");

            return SaveIO.In.ReadBytes(len);
        }

        public void Inject(byte[] input)
        {
            if (input == null || input.Length == 0x00 || SaveIO == null)
                return;

            using (var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true))
            {
                io.Out.Write(SaveIO.In.ReadBytes(0x18));
                io.Out.Write(input.Length + 0x1C);
                io.Out.Write(input);

                SaveIO = new EndianIO(io.ToArray(), EndianType.BigEndian, true);
            }
        }

        protected byte[] ProcessSaveData(byte[] input, bool isEncrypting)
        {
            BlowfishEngine engine = new BlowfishEngine();
            BufferedBlockCipher cipher = new BufferedBlockCipher(engine);
            cipher.Init(isEncrypting,
                        new KeyParameter(new byte[]
                                             {
                                                 0x4B, 0x41, 0x5E, 0x33, 0x6B, 0x40, 0x32, 0x31, 0x27, 0x33, 0x38, 0x67,
                                                 0x64, 0x6A, 0x66, 0x67, 0x45, 0x46, 0x4A, 0x23
                                             }));

            var outBytes = new byte[input.Length];
            int len1 = cipher.ProcessBytes(input, 0, input.Length, outBytes, 0);
            cipher.DoFinal(outBytes, len1);
            return outBytes;
        }

        protected void Verify()
        {
            SaveIO = new EndianIO(ProcessSaveData(IO.ToArray(), false), EndianType.BigEndian, true);
            SaveIO.In.SeekTo(0x04);
            byte[] saveHash = SaveIO.In.ReadBytes(0x14);
            int hashDataLen = SaveIO.In.SeekNReadInt32(0x18);

            if (!HorizonCrypt.ArrayEquals(saveHash, HorizonCrypt.SHA1(SaveIO.In.ReadBytes(hashDataLen))))
                throw new Exception("Devil May Cry: invalid save data detected");
        }

        public void Save()
        {
            int hashDataLen = SaveIO.In.SeekNReadInt32(0x18);
            var hash = HorizonCrypt.SHA1(SaveIO.In.ReadBytes(hashDataLen));
            SaveIO.SeekTo(0x04);
            SaveIO.Out.Write(hash);
            var saveData = SaveIO.ToArray();
            IO.SeekTo(0x00);
            IO.Stream.SetLength(saveData.Length);
            IO.Out.Write(ProcessSaveData(saveData, true));
        }

        public void Close()
        {
            if(IO != null)
                IO.Close();
            if(SaveIO != null)
                SaveIO.Close();
        }
    }

    public enum Upgrades
    {
        AngelEvade = 0x00,
        DemonEvade,
        AngelBoost,
        OphionAngelLiftAndLeap = 0x05,
        OphionDemonPull = 0x0A,
        EbonyAndIvory = 0x0F,
        Revenant,
        Kablooey,
        Osiris,
        Arbiter,
        Aquila,
        Eryx,
        DevilTrigger,
    }
    public enum VergilUpgrades
    {      
        YamatoAngelMode = 0x12,
        YamatoDemonMode,
        DevilTrigger = 0x16,
        Doppleganger,
        SwordIllusion = 0x1C
    }

    internal enum StyleRank
    {
        None = -1,
        Dirty = 0,
        Cruel,
        Brutal,
        Anarchic,
        Savage,
        SSadistic,
        SSSensational,
    }
    internal enum MissionDifficulty
    {
        Human,
        DevilHunter,
        Nephilim,
        SonOfSparda,
        DanteMustDie,
        HeavenOrHell,
        HellAndHell
    }
    public class DevilMayCrySaveProfile : DevilMayCrySaveFile
    {
        public struct Upgrade
        {
            public int NameLength;
            public string Name;
            public int Attributes;
        }
        internal class DifficultyProgress
        {
            internal List<MissionProgress> MissionCompletion = new List<MissionProgress>();
            internal bool DifficultyCompleted 
            {
                get
                { return MissionCompletion.All(missionProgress => missionProgress.IsCompleted); }
                set
                {
                    if(!value)return;

                    foreach (var missionProgress in MissionCompletion)
                    {
                        missionProgress.IsCompleted = true;
                    }
                }
            }
        }
        internal class MissionProgress
        {
            internal bool IsCompleted;
            internal StyleRank StyleRank;
            internal int StylePoints;
            internal int TimeCompleted; // In Seconds
            internal int CompletionPercentage;
            internal int KeysFound;

            internal MissionProgress(EndianReader reader)
            {
                IsCompleted = reader.ReadBoolean();
                reader.BaseStream.Position += 4;
                StyleRank = (StyleRank)reader.ReadInt32();
                StylePoints = reader.ReadInt32();
                reader.BaseStream.Position += 4;
                TimeCompleted = reader.ReadInt32();
                reader.BaseStream.Position += 4;
                CompletionPercentage = reader.ReadInt32();
                KeysFound = reader.ReadInt32();
            }
            internal void ToArray(EndianWriter writer)
            {
                writer.Write(IsCompleted);
                writer.BaseStream.Position += 4;
                if (StyleRank == StyleRank.None)
                {
                    writer.Write(-1);
                }
                else
                {
                    writer.Write((int)StyleRank);
                }
                writer.Write(StylePoints);
                writer.BaseStream.Position += 4;
                writer.Write(TimeCompleted);
                writer.BaseStream.Position += 4;
                writer.Write(CompletionPercentage);
                writer.Write(KeysFound);
            }
        }

        public struct ProfileGameStat
        {
            public ProfileGameStatType Type { get; set; }
            public object Value { get; set; }
            public long Position { get; set; }
        }
        public enum ProfileGameStatType
        {
            Boolean = 0x00,
            Int = 0x14,
            Double = 0x15,
            Single = 0x16,
            DoubleBag = 0x17
        }

        public int Version { get; set; }
        public Dictionary<string, ProfileGameStat> GameStats { get; set; }
        public List<Upgrade> Upgrades { get; set; }
        public List<int> Unlocks; 
        private long _upgradePosition, _statsPosition, _unlocksPosition;
        internal List<DifficultyProgress> Missions { get; set; }
 
        public DevilMayCrySaveProfile(EndianIO io) : base(io)
        {
            Read();
        }

        protected override void Read()
        {
            int count;
            GameStats = new Dictionary<string, ProfileGameStat>();

            SaveIO.SeekTo(0x1C);

            Version = SaveIO.In.ReadInt32();
            // load the mission completion progress for each difficulty
            Missions = new List<DifficultyProgress>();
            int numOfDifficulties = SaveIO.In.ReadInt32();
            for (int i = 0; i < numOfDifficulties; i++)
            {
                int numOfMissions = SaveIO.In.ReadInt32();
                var difficulty = new DifficultyProgress();
                for (int x = 0; x < numOfMissions; x++)
                {
                    difficulty.MissionCompletion.Add(new MissionProgress(SaveIO.In));
                }
                Missions.Add(difficulty);
            }

            ReadStruct1();
            ReadStruct1();
            // read enabled upgrades
            _upgradePosition = SaveIO.Position;
            SaveIO.In.ReadInt32();
            int upgradeCount = SaveIO.In.ReadInt32();
            if (upgradeCount > 0x00)
            {
                Upgrades = new List<Upgrade>();
                for (int i = 0; i < upgradeCount; i++)
                {
                    var upgrade = new Upgrade {NameLength = SaveIO.In.ReadInt32()};
                    upgrade.Name = SaveIO.In.ReadAsciiString(upgrade.NameLength);
                    upgrade.Attributes = SaveIO.In.ReadInt32();
                    Upgrades.Add(upgrade);
                }
            }

            // read upgrade counts
            SaveIO.Position += 4;
            GameStats.Add("Orb_White_Acquired", new ProfileGameStat { Position = SaveIO.Position, Type = ProfileGameStatType.Int });
            SaveIO.Position += 4;
            GameStats.Add("Orb_White", new ProfileGameStat { Position = SaveIO.Position, Type = ProfileGameStatType.Int });
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            // unlockables
            _unlocksPosition = SaveIO.Position;
            count = SaveIO.In.ReadInt32();
            Unlocks = new List<int>();
            for (int i = 0; i < count; i++)
            {
                Unlocks.Add(SaveIO.In.ReadInt32());
            }
            SaveIO.In.ReadByte();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadByte();
            for (int i = 0; i < 0x28 + 5; i++)
            {
                SaveIO.In.ReadByte();
            }
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                ReadStruct2();
            }
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                ReadStruct2();
            }
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                ReadStruct2();
                SaveIO.In.ReadByte();
            }
            for (int i = 0; i < 0x04; i++)
            {
                count = SaveIO.In.ReadInt32();
                if (count >= 0)
                {
                    SaveIO.In.ReadBytes(count);
                }
                else
                {
                    SaveIO.In.ReadBytes(count << 1);
                }
            }
            SaveIO.In.ReadBytes(0x08);
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                SaveIO.In.ReadInt32();
            }
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                ReadStruct2();
            }
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                ReadStruct2();
            }
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                SaveIO.In.ReadInt32();
            }
            count = SaveIO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                SaveIO.In.ReadInt32();
            }
            SaveIO.In.ReadByte();
            SaveIO.In.ReadByte();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadByte();

            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadByte();
            SaveIO.In.ReadByte();
            SaveIO.In.ReadInt32();
            SaveIO.In.ReadByte();

            ReadStruct3();
            ReadStruct3();

            _statsPosition = SaveIO.Position;
            ReadGameStats();
        }

        private void ReadStruct1()
        {
            int secondary = SaveIO.In.ReadInt32();
            for (int j = 0; j < secondary; j++)
            {
                SaveIO.In.ReadByte();
                for (int k = 0; k < 0x08; k++)
                {
                    SaveIO.In.ReadInt32();
                }
            }
        }
        private void ReadStruct2() // 8256AF38
        {
            var sec = SaveIO.In.ReadInt32();
            if (sec >= 0)
            {
                SaveIO.In.ReadBytes(sec);
            }
            else
            {
                SaveIO.In.ReadBytes(sec << 1);
            }
        }
        private void ReadStruct3() // 825B4230 - reading strings/'C' arrays
        {
            int ctr = SaveIO.In.ReadInt32();

                if (ctr > 0)
                    SaveIO.In.ReadBytes(ctr);
        }

        private void ReadGameStats()
        {
            SaveIO.Position = _statsPosition;
            int statCount = SaveIO.In.ReadInt32();
            for (int i = 0; i < statCount; i++)
            {
                string statName = SaveIO.In.ReadAsciiString(SaveIO.In.ReadInt32());
                var valueType = (ProfileGameStatType)SaveIO.In.ReadByte();
                long position = SaveIO.Position;
                switch (valueType)
                {
                        case ProfileGameStatType.Double:
                        case ProfileGameStatType.DoubleBag:
                            //value = SaveIO.In.ReadBytes(0x08);
                        SaveIO.Position += 0x08;
                            break;
                        case ProfileGameStatType.Boolean:
                        case ProfileGameStatType.Int:
                        case ProfileGameStatType.Single:
                        //value = SaveIO.In.ReadBytes(0x04);
                        SaveIO.Position += 0x04;
                        break;
                        default:
                            throw new Exception("unhandled type detected.");
                }
                GameStats.Add(statName, new ProfileGameStat { Type = valueType, Position = position });
            }
        }

        public object ReadStat(string statName)
        {
            var stat = GameStats[statName];
            SaveIO.SeekTo(stat.Position);
            switch (stat.Type)
            {
                case ProfileGameStatType.Boolean:
                    return SaveIO.In.ReadInt32() != 0x00;
                case ProfileGameStatType.Int:
                    return SaveIO.In.ReadInt32();
                    case ProfileGameStatType.Single:
                    return SaveIO.In.ReadSingle();
                    case ProfileGameStatType.Double:
                    case ProfileGameStatType.DoubleBag:
                    return SaveIO.In.ReadDouble();
            }
            return null;
        }

        public void WriteStat(string statName, object statValue)
        {
            var stat = GameStats[statName];
            SaveIO.SeekTo(stat.Position);
            switch (stat.Type)
            {
                case ProfileGameStatType.Boolean:
                    SaveIO.Out.Write(Convert.ToInt32(Convert.ToBoolean(statValue)));
                    break;
                case ProfileGameStatType.Int:
                    SaveIO.Out.Write(Convert.ToInt32(statValue));
                    break;
                case ProfileGameStatType.Single:
                    SaveIO.Out.Write(Convert.ToSingle(statValue));
                    break;
                case ProfileGameStatType.Double:
                case ProfileGameStatType.DoubleBag:
                    SaveIO.Out.Write(Convert.ToDouble(statValue));
                    break;
            }
        }

        public bool IsUpgradeUnlocked(string upgradeName)
        {
            if (Upgrades == null)
                return false;

            return Upgrades.Any(t => Upgrades.Contains(new Upgrade
                                                           {
                                                               Attributes = 0, Name = upgradeName, NameLength = upgradeName.Length + 1
                                                           }));
        }
       
        public void SaveMissionProgress()
        {
            SaveIO.Out.SeekTo(0x20);
            SaveIO.Out.Write(Missions.Count);
            foreach (var difficulty in Missions)
            {
                SaveIO.Out.Write(difficulty.MissionCompletion.Count);
                foreach (var mission in difficulty.MissionCompletion)
                {
                    mission.ToArray(SaveIO.Out);
                }
            }
        }

        public void WriteUpgrades(List<string> upgrades)
        {
            var ms = new MemoryStream();
            var writer = new EndianWriter(ms, EndianType.BigEndian);
            writer.Write(upgrades.Count);
            foreach (var upgrade in upgrades)
            {
                writer.Write(upgrade.Length + 1);
                writer.WriteAsciiString(upgrade, upgrade.Length + 1);
                writer.Write(0);
            }
            var upgradeData = ms.ToArray();
            writer.Close();
            writer = new EndianWriter(ms = new MemoryStream(), EndianType.BigEndian);
            writer.Write(Unlocks.Count);
            foreach (var unlock in Unlocks)
            {
                writer.Write(unlock);
            }
            var unlockData = ms.ToArray();
            writer.Close();
            
            int oldUpgradeLen = (SaveIO.In.ReadInt32(_upgradePosition) + 4);
            int oldUnlocksLen = (SaveIO.In.ReadInt32(_unlocksPosition) + 1) * 4;

            var newLength = (_upgradePosition + upgradeData.Length + 0x18 + unlockData.Length +
                (SaveIO.Length - ((_upgradePosition + oldUpgradeLen) + 0x18 + oldUnlocksLen)));

            SaveIO.SeekTo(0);
            byte[] header = SaveIO.In.ReadBytes(_upgradePosition);
            //skip the old upgrade and unlocks data
            SaveIO.Position += oldUpgradeLen;
            var inter = SaveIO.In.ReadBytes(0x18);
            SaveIO.Position += oldUnlocksLen;
            byte[] footer = SaveIO.In.ReadBytes(SaveIO.Length - ((_upgradePosition + oldUpgradeLen) + 0x18 + oldUnlocksLen));

            writer = new EndianWriter(ms = new MemoryStream(), EndianType.BigEndian);
            writer.Write(header);
            writer.Write(upgradeData.Length);
            writer.Write(upgradeData);
            writer.Write(inter);
            writer.Write(unlockData);
            writer.Write(footer);
            writer.SeekNWrite(0x18, (uint)newLength);
            writer.BaseStream.SetLength((int)(((newLength + 0x1C) + 7) & 0xFFFFFFF8));
            writer.Flush();
            /*
            long upgradeOrbPosition = _upgradePosition + upgradeData.Length;
            
            GameStats.Clear();
            GameStats = new Dictionary<string, ProfileGameStat>
                            {
                                {
                                    "Orb_White_Acquired",
                                    new ProfileGameStat
                                        {
                                            Position =
                                                upgradeOrbPosition + 4,
                                            Type =
                                                ProfileGameStatType.Int
                                        }
                                },
                                {
                                    "Orb_White",
                                    new ProfileGameStat
                                        {
                                            Position =
                                                upgradeOrbPosition + 8,
                                            Type =
                                                ProfileGameStatType.Int
                                        }
                                }
                            };
            
            ReadGameStats();
            */
            SaveIO = new EndianIO(ms, EndianType.BigEndian, true);
            Read();
        }
    }
    internal class DevilGameException : Exception
    {
        internal DevilGameException(string message)
            : base("Devil May Cry: " + message)
        {

        }
    }
}