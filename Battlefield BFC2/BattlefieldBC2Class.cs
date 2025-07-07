using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ElectronicArts;

namespace Battlefield
{
    public class BattlefieldBC2
    {
        public struct SaveEntry
        {
            public uint EntryType;
            public uint EntryNameLength;
            public string EntryName;
            public uint EntryValueLength;
            //public byte[] EntryValue;
            public long Position;
        }
        public EndianIO IO { get; set; }
        private EndianReader Reader { get { return IO.In; } }
        private EA EaHeader;

        private uint EntryPoolCount;
        public List<SaveEntry> SaveEntries;

        public BattlefieldBC2(EndianIO SaveIO, EA SaveEaHeader)
        {
            this.IO = SaveIO;
            this.EaHeader = SaveEaHeader;

            this.SaveEntries = new List<SaveEntry>();

            this.Read();
        }
        private void Read()
        {
            this.IO.In.SeekTo(0x20);
            this.EntryPoolCount = Reader.ReadUInt32();
            this.Reader.BaseStream.Position += 4;

            for (var x = 0; x < EntryPoolCount; x++)
            {
                this.ReadSaveEntries();
            }

            this.ReadBody();
        }
        private void ReadSaveEntries()
        {
            uint count = Reader.ReadUInt32();
            for (var x = 0; x < count; x++)
            {
                SaveEntry sv = new SaveEntry();
                sv.EntryType = this.Reader.ReadUInt32();
                sv.EntryNameLength = this.Reader.ReadUInt32();
                sv.EntryName = this.Reader.ReadStringNullTerminated();
                sv.EntryValueLength = this.Reader.ReadUInt32();
                sv.Position = Reader.BaseStream.Position;
                Reader.BaseStream.Position += sv.EntryValueLength;

                //sv1.EntryValue = this.Reader.ReadBytes(sv1.EntryValueLength);

                this.SaveEntries.Add(sv);
            }
        }

        private void ReadBody()
        {
            var io = new EndianIO(this.Read(this.FindEntry("body")), EndianType.BigEndian, true);

        }
        private byte[] Read(SaveEntry entry)
        {
            this.Reader.BaseStream.Position = entry.Position;
            return this.Reader.ReadBytes(entry.EntryValueLength);
        }
        private SaveEntry FindEntry(string Name)
        {
            return this.SaveEntries.Find(delegate(SaveEntry se)
            {
                return se.EntryName == Name;
            });
        }

        /* Write Section */
        public void Save()
        {
            this.FixChecksums();
            this.EaHeader.FixChecksums();
        }
        private void FixChecksums()
        {
            this.IO.In.SeekTo(EA.EA_Header.HEADER_SIZE + 4);
            uint crc1 = EACRC32.Calculate_Alt2(this.IO.In.ReadBytes(this.EaHeader.Header.Block1len - 4),
                this.EaHeader.Header.Block1len - 4, 0x12345678);
            this.IO.In.SeekTo(EA.EA_Header.HEADER_SIZE + this.EaHeader.Header.Block1len + 4);
            uint crc2 = EACRC32.Calculate_Alt2(this.IO.In.ReadBytes(this.EaHeader.Header.Block2len - 4),
                this.EaHeader.Header.Block2len - 4, 0x12345678);
            this.IO.Out.SeekTo(EA.EA_Header.HEADER_SIZE);
            this.IO.Out.Write(crc1);
            this.IO.Out.SeekTo(EA.EA_Header.HEADER_SIZE + this.EaHeader.Header.Block1len);
            this.IO.Out.Write(crc2);
        }
    }
}
