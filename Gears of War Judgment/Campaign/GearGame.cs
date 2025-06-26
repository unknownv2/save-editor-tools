using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Horizon.PackageEditors.Gears_of_War_Judgment.Campaign
{
    internal class GearGame
    {
        internal int SlotIndex;
        internal string ChapterName;
        internal byte[] SaveGuid;
        internal int ChapterNumber;
        internal int CheckpointInChapter;
        internal DifficultyLevel Difficulty;
        internal UEDateTime SaveTime;
        internal Vector CheckpointLocation;
        internal List<CheckpointData> Checkpoints;
        internal List<List<string>> EnemyListRecords;
        internal List<ActorRecord> ActorRecords;
        internal byte[] KismetData;

        internal GearGame(EndianIO io)
        {
            SlotIndex = io.In.ReadInt32();
            ChapterName = io.In.ReadString(io.In.ReadInt32());
            SaveGuid = io.In.ReadBytes(16);
            ChapterNumber = io.In.ReadInt32();
            CheckpointInChapter = io.In.ReadInt32();
            Difficulty = (DifficultyLevel)io.In.ReadByte();
            SaveTime = new UEDateTime(io);
            CheckpointLocation = new Vector(io);

            int numCheckpoints = io.In.ReadInt32();
            Checkpoints = new List<CheckpointData>(numCheckpoints);
            while (numCheckpoints-- != 0)
                Checkpoints.Add(new CheckpointData(io));

            int stringTables = io.In.ReadInt32();
            EnemyListRecords = new List<List<string>>(stringTables);
            while (stringTables-- != 0)
            {
                int numStrings = io.In.ReadInt32();
                var strings = new List<string>(numStrings);
                while (numStrings-- != 0)
                    strings.Add(io.In.ReadString(io.In.ReadInt32()));
                EnemyListRecords.Add(strings);
            }

            int numRecords = io.In.ReadInt32();
            ActorRecords = new List<ActorRecord>(numRecords);
            while (numRecords-- != 0)
            {
                var recordName = io.In.ReadString(io.In.ReadInt32());
                var recordType = io.In.ReadString(io.In.ReadInt32());
                var recordData = io.In.ReadBytes(io.In.ReadInt32());

                ActorRecord r;

                switch (recordType.Split('_')[0])
                {
                    case "GearGame.GearPC":
                        r = new GearPC();
                        break;
                    case "GearGame.GearAI":
                        r = new GearAI();
                        break;
                    default:
                        r = new ActorRecord();
                        break;
                }

                r.Name = recordName;
                r.Type = recordType;
                r.Read(recordData);

                ActorRecords.Add(r);
            }

            //KismetData = io.In.ReadBytes(io.In.ReadInt32());

            KismetData = io.In.ReadBytes(io.Length - io.Position);
        }

        internal List<GearPC> GetGearPCRecords()
        {
            return this.ActorRecords.OfType<GearPC>().ToList();
        }

        internal List<GearAI> GetGearAIRecords()
        {
            return this.ActorRecords.OfType<GearAI>().ToList();
        }

        internal void Write(EndianIO io)
        {
            io.Out.Write(SlotIndex);

            var t = ChapterName.Length + 1;
            io.Out.Write(t);
            io.Out.WriteAsciiString(ChapterName, t);

            if (SaveGuid.Length != 16)
                throw new Exception("GoWJ: Invalid GUID length.");

            io.Out.Write(SaveGuid);
            io.Out.Write(ChapterNumber);
            io.Out.Write(CheckpointInChapter);
            io.Out.Write((byte)Difficulty);
            SaveTime.Write(io);
            CheckpointLocation.Write(io);

            io.Out.Write(Checkpoints.Count);
            foreach (var c in Checkpoints)
                c.Write(io);

            io.Out.Write(EnemyListRecords.Count);
            foreach (var l in EnemyListRecords)
            {
                io.Out.Write(l.Count);
                foreach (var s in l)
                {
                    t = s.Length + 1;
                    io.Out.Write(t);
                    io.Out.WriteAsciiString(s, t);
                }
            }

            io.Out.Write(ActorRecords.Count);
            foreach (var r in ActorRecords)
                r.Write(io);

            io.Out.Write(KismetData);
        }
    }

    internal class ActorRecord
    {
        internal string Name;
        internal string Type;

        private byte[] _data;

        internal byte[] GetData()
        {
            return this._data;
        }

        protected virtual void Deserialize(EndianIO io)
        {

        }

        protected virtual void Serialize(EndianIO io)
        {
            io.Out.Write(_data);
        }

        protected string ReadString(EndianIO io)
        {
            if (io.In.ReadInt32() == 0)
                return "";

            return io.In.ReadNullTerminatedString();
        }

        protected void WriteString(EndianIO io, string str)
        {
            if (string.IsNullOrEmpty(str))
                io.Out.Write(0);
            else
            {
                var t = str.Length + 1;
                io.Out.Write(t);
                io.Out.WriteAsciiString(str, t);
            }
        }

        internal void Read(byte[] data)
        {
            var mio = new EndianIO(new MemoryStream(data, false), EndianType.BigEndian, true);
            this.Deserialize(mio);
            mio.Close();

            _data = data;
        }

        internal void Write(EndianIO io)
        {
            var t = Name.Length + 1;
            io.Out.Write(t);
            io.Out.WriteAsciiString(Name, t);

            t = Type.Length + 1;
            io.Out.Write(t);
            io.Out.WriteAsciiString(Type, t);

            var ms = new MemoryStream();
            var mio = new EndianIO(ms, EndianType.BigEndian, true);
            this.Serialize(mio);
            mio.Close();

            _data = ms.ToArray();

            io.Out.Write(_data.Length);
            io.Out.Write(_data);
        }
    }

    internal struct CheckpointData
    {
        internal string CheckpointName;
        internal bool ShouldBeLoaded;
        internal bool ShouldBeVisible;

        internal CheckpointData(EndianIO io)
        {
            CheckpointName = io.In.ReadString(io.In.ReadInt32());
            ShouldBeLoaded = io.In.ReadBoolean();
            ShouldBeVisible = io.In.ReadBoolean();
        }

        internal void Write(EndianIO io)
        {
            var t = CheckpointName.Length + 1;
            io.Out.Write(t);
            io.Out.WriteAsciiString(CheckpointName, t);
            io.Out.Write(ShouldBeLoaded);
            io.Out.Write(ShouldBeVisible);
        }
    }
}
