using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Mercury_Hg
{
    internal enum RecordTag : uint
    {
        Settings = 0x6F93A9BD,
        Levels = 0xE3E3BDE2,
        Stats = 0xFCF9368B
    }

    internal enum LevelTag : uint
    {
        T001 = 0x0D7CEE81,
        T002 = 0xD7C1437C,
        T003 = 0xBD08F10E,
        T004 = 0x601AA803,
        T005 = 0xACFCA7A0,
        T006 = 0x9682E921,
        T007 = 0x02EDCC48,
        T008 = 0xD72C3C71,

        L001 = 0xFFE9D35D,
        L003 = 0xA1617620,
        L004 = 0x12063B7E,
        L011 = 0x7E170EE2,
        L012 = 0xF42CB4DC,
        L019 = 0x4578B821,
        L020 = 0x6B30DE0C,
        L037 = 0xB40F4B7F,
        L038 = 0x48C9080D
    }

    internal enum StatTag : uint
    {
        LevelsComplete,
        LevelsComplete_Main,
        LevelsComplete_DLC1,
        LevelsComplete_DLC2,
        LevelsComplete_DLC3,
        AtomsDiscovered,
        AtomsDiscovered_Main,
        AtomsDiscovered_DLC1,
        AtomsDiscovered_DLC2,
        AtomsDiscovered_DLC3,
        LevelTargets,
        ElementGroupsDiscovered,
        ChallengesUnlocked,
        ChallengesUnlocked_Main,
        ChallengesUnlocked_DLC1,
        ChallengesUnlocked_DLC2,
        ChallengesUnlocked_DLC3,
        ChallengeStagesComplete,
        ChallengeStagesComplete_Main,
        ChallengeStagesComplete_DLC1,
        ChallengeStagesComplete_DLC2,
        ChallengeStagesComplete_DLC3,
        BonusLevelsUnlocked,
        BonusLevelsUnlocked_Main,
        BonusLevelsUnlocked_DLC1,
        BonusLevelsUnlocked_DLC2,
        BonusLevelsUnlocked_DLC3,
        BonusLevelsComplete,
        BonusLevelsComplete_Main,
        BonusLevelsComplete_DLC1,
        BonusLevelsComplete_DLC2,
        BonusLevelsComplete_DLC3,
        TutorialLevelsComplete,
        PercentComplete = 0x3A3ECCDA,
        SixaxisLevelsPlayed,
        OnlineGhostsRaced,
        OwnMusicPlayed,
        MercuryBallsLost
    }

    internal sealed class MercuryHgSave : Dictionary<RecordTag, Record>
    {
        private const uint Magic = 0x0CA6F4C9;

        internal MercuryHgSave(EndianIO IO)
        {
            if (IO.In.ReadInt32() != IO.Stream.Length)
                throw new Exception("Invalid savegame length!");

            if (IO.In.ReadInt32() != Magic)
                throw new Exception("Savegame format not supported!");

            int numRecords = IO.In.ReadInt32();
            for (int x = 0; x < numRecords; x++)
                this.Add((RecordTag)IO.In.ReadUInt32(), new Record(IO));
        }

        internal byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(0);
            IO.Out.Write(Magic);
            IO.Out.Write(this.Count);
            foreach (KeyValuePair<RecordTag, Record> rec in this)
            {
                IO.Out.Write((uint)rec.Key);
                IO.Out.Write(rec.Value.ToArray());
            }
            IO.Stream.Position = 0;
            IO.Out.Write(IO.Stream.Length);
            byte[] arr = IO.ToArray();
            IO.Close();
            return arr;
        }
    }

    internal class Record
    {
        internal protected int Flags;
        internal protected byte[] Data;
        private int Zero;

        internal Record(EndianIO IO)
        {
            Flags = IO.In.ReadInt32();
            int dataLength = IO.In.ReadInt32();
            Zero = IO.In.ReadInt32();
            Data = IO.In.ReadBytes(dataLength);
        }

        internal byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(Flags);
            IO.Out.Write(Zero);
            IO.Out.Write(Data.Length);
            IO.Out.Write(Data);
            byte[] arr = IO.ToArray();
            IO.Close();
            return arr;
        }
    }

    internal struct SettingsRecord
    {
        internal float MusicVolume, SFXVolume, Brightness, Unknown;
        internal int ControllerLayout, CameraRotation, CameraElevation,
            BlobSplitFocus, BlobSplitIndicator, BlobCycle;

        internal SettingsRecord(Record rec)
        {
            EndianIO IO = new EndianIO(rec.Data, EndianType.BigEndian, true);
            MusicVolume = IO.In.ReadSingle();
            SFXVolume = IO.In.ReadSingle();
            Brightness = IO.In.ReadSingle();
            ControllerLayout = IO.In.ReadInt32();
            CameraRotation = IO.In.ReadInt32();
            CameraElevation = IO.In.ReadInt32();
            BlobSplitFocus = IO.In.ReadInt32();
            BlobSplitIndicator = IO.In.ReadInt32();
            BlobCycle = IO.In.ReadInt32();
            Unknown = IO.In.ReadSingle();
        }

        internal byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(MusicVolume);
            IO.Out.Write(SFXVolume);
            IO.Out.Write(Brightness);
            IO.Out.Write(ControllerLayout);
            IO.Out.Write(CameraRotation);
            IO.Out.Write(CameraElevation);
            IO.Out.Write(BlobSplitFocus);
            IO.Out.Write(BlobSplitIndicator);
            IO.Out.Write(BlobCycle);
            byte[] arr = IO.ToArray();
            IO.Close();
            return arr;
        }
    }

    internal class LevelsRecord : Dictionary<LevelTag, LevelEntry>
    {
        private readonly byte[] NullEntry = new byte[16];

        internal LevelsRecord(Record rec)
        {
            EndianIO IO = new EndianIO(rec.Data, EndianType.BigEndian, true);
            int numLevels = IO.In.ReadInt32();
            for (int x = 0; x < numLevels; x++)
                this.Add((LevelTag)IO.In.ReadUInt32(), new LevelEntry(IO));
        }

        internal byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(this.Count);
            foreach (KeyValuePair<LevelTag, LevelEntry> level in this)
            {
                IO.Out.Write((uint)level.Key);
                level.Value.Write(IO);
            }
            for (int x = this.Count; x < 512; x++)
                IO.Out.Write(NullEntry);
            byte[] arr = IO.ToArray();
            IO.Close();
            return arr;
        }
    }

    internal class LevelEntry
    {
        private byte Unknown;
        internal byte Attempts, Bonuses, PercentMercury;
        internal float Time;
        internal int Score;

        internal bool Complete
        {
            get
            {
                return Score != 0;
            }
        }

        internal LevelEntry(EndianIO IO)
        {
            Unknown = IO.In.ReadByte();
            Attempts = IO.In.ReadByte();
            Bonuses = IO.In.ReadByte();
            PercentMercury = IO.In.ReadByte();
            Time = IO.In.ReadSingle();
            Score = IO.In.ReadInt32();
        }

        internal void Write(EndianIO IO)
        {
            IO.Out.Write(Unknown);
            IO.Out.Write(Attempts);
            IO.Out.Write(Bonuses);
            IO.Out.Write(PercentMercury);
            IO.Out.Write(Time);
            IO.Out.Write(Score);
        }
    }

    internal class StatsRecord : Dictionary<StatTag, int>
    {
        internal StatsRecord(Record rec)
        {
            EndianIO IO = new EndianIO(rec.Data, EndianType.BigEndian, true);
            int numStats = IO.In.ReadInt32();
            for (int x = 0; x < numStats; x++)
                this.Add((StatTag)IO.In.ReadUInt32(), IO.In.ReadInt32());
        }

        internal byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(this.Count);
            foreach (KeyValuePair<StatTag, int> stat in this)
            {
                IO.Out.Write((uint)stat.Key);
                IO.Out.Write(stat.Value);
            }
            byte[] arr = IO.ToArray();
            IO.Close();
            return arr;
        }
    }
}
