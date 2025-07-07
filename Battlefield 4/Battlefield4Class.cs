using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicArts;
using System.IO;

namespace Horizon.PackageEditors.Battlefield_4
{
    public class Battlefield4Class
    {
        private EndianIO IO { get; set; }
        private EA EA_Header;
        // Each SaveEntry[] in the array represents a different category (audio, graphics, controls, gameplay, online, campaign).
        public SaveEntry[][] SaveEntries;

        public Battlefield4Class(EndianIO io)
        {
            this.IO = io;
            this.EA_Header = new EA(IO);
            this.Read();
        }
        private void Read()
        {
            this.IO.In.SeekTo(0x20);
            uint EntryPoolCount = IO.In.ReadUInt32();
            this.IO.In.BaseStream.Position += 4;

            this.SaveEntries = new SaveEntry[EntryPoolCount][];
            for (var x = 0; x < this.SaveEntries.Length; x++)
            {
                uint count = IO.In.ReadUInt32();
                this.SaveEntries[x] = new SaveEntry[count];
                for (var y = 0; y < this.SaveEntries[x].Length; y++)
                    this.SaveEntries[x][y] = new SaveEntry(IO);
            }
        }
        public void Write()
        {
            this.IO.Out.SeekTo(0x20);
            IO.Out.Write((uint)this.SaveEntries.Length);
            this.IO.Out.BaseStream.Position += 4;

            for (var x = 0; x < this.SaveEntries.Length; x++)
            {
                IO.Out.Write((uint)this.SaveEntries[x].Length);
                for (var y = 0; y < this.SaveEntries[x].Length; y++)
                    this.SaveEntries[x][y].Write(IO);
            }

            // Check if we should pad the rest of the save, or extend its size.
            if (this.IO.Out.BaseStream.Position < this.IO.Out.BaseStream.Length)
                IO.Out.Write(new byte[this.IO.Out.BaseStream.Length - this.IO.Out.BaseStream.Position]);
            else
            {
                // Write our new length since we extended the stream.
                IO.Out.BaseStream.Position = 4;
                IO.Out.Write((uint)IO.Out.BaseStream.Length);
                IO.Out.BaseStream.Position += 4;
                IO.Out.Write(((uint)IO.Out.BaseStream.Length) - 0x24);
            }
             
            this.FixChecksums();
        }
        private void FixChecksums()
        {
            this.IO.In.SeekTo(EA.EA_Header.HEADER_SIZE + 4);
            uint crc1 = EACRC32.Calculate_Alt2(this.IO.In.ReadBytes(this.EA_Header.Header.Block1len - 4),
                this.EA_Header.Header.Block1len - 4, 0x12345678);
            this.IO.In.SeekTo(EA.EA_Header.HEADER_SIZE + this.EA_Header.Header.Block1len + 4);
            uint crc2 = EACRC32.Calculate_Alt2(this.IO.In.ReadBytes(this.EA_Header.Header.Block2len - 4),
                this.EA_Header.Header.Block2len - 4, 0x12345678);
            this.IO.Out.SeekTo(EA.EA_Header.HEADER_SIZE);
            this.IO.Out.Write(crc1);
            this.IO.Out.SeekTo(EA.EA_Header.HEADER_SIZE + this.EA_Header.Header.Block1len);
            this.IO.Out.Write(crc2);
        }

        public class SaveEntry
        {
            private SaveEntryType _entrytype;
            public SaveEntryType EntryType { get { return _entrytype; } }
            private uint EntryNameLength { get; set; }
            public string EntryName { get; set; }
            public object EntryValue { get; set; }

            public SaveEntry(EndianIO IO)
            {
                Read(IO);
            }
            public void Read(EndianIO IO)
            {
                _entrytype = (SaveEntryType)IO.In.ReadUInt32();
                EntryNameLength = IO.In.ReadUInt32();
                EntryName = IO.In.ReadStringNullTerminated();
                uint EntryValueLength = IO.In.ReadUInt32();
                switch (EntryType)
                {
                    case SaveEntryType.Integer:
                        this.EntryValue = int.Parse(IO.In.ReadStringNullTerminated());
                        break;
                    case SaveEntryType.Float:
                        this.EntryValue = float.Parse(IO.In.ReadStringNullTerminated());
                        break;
                    case SaveEntryType.String:
                        this.EntryValue = IO.In.ReadStringNullTerminated();
                        break;
                    case SaveEntryType.Data:
                        this.EntryValue = IO.In.ReadBytes(EntryValueLength);
                        break;
                }
            }
            public void Write(EndianIO IO)
            {
                IO.Out.Write((uint)_entrytype);
                IO.Out.Write(EntryName.Length + 1);
                IO.Out.WriteAsciiString(EntryName, EntryName.Length + 1);
                switch (EntryType)
                {
                    case SaveEntryType.Integer:
                        int entryVal = (int)EntryValue;
                        string entryValStr = entryVal.ToString();
                        IO.Out.Write((uint)entryValStr.Length + 1);
                        IO.Out.WriteAsciiString(entryValStr, entryValStr.Length + 1);
                        break;
                    case SaveEntryType.Float:
                        float entryVal2 = (float)EntryValue;
                        string entryValStr2 = entryVal2.ToString("n6");
                        IO.Out.Write((uint)entryValStr2.Length + 1);
                        IO.Out.WriteAsciiString(entryValStr2, entryValStr2.Length + 1);
                        break;
                    case SaveEntryType.String:
                        string entryValStr3 = (string)EntryValue;
                        IO.Out.Write((uint)entryValStr3.Length + 1);
                        IO.Out.WriteAsciiString(entryValStr3, entryValStr3.Length + 1);
                        break;
                    case SaveEntryType.Data:
                        byte[] data = (byte[])EntryValue;
                        IO.Out.Write((uint)data.Length);
                        IO.Out.Write(data);
                        break;
                }
            }
            public enum SaveEntryType : uint
            {
                Float = 1,
                Integer = 2,
                String = 4,
                Data = 5
            }
        }
        public enum SaveEntryCategory : uint
        {
            Audio,
            Graphics,
            Controls,
            Unknown0,
            Game,
            Campaign,
            Campaign_Level,
            Unknown1,
            Unknown2,
            Unknown3,
            Unknown4
        }
    }
}
