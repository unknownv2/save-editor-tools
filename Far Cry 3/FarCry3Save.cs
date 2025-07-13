using System;
using System.Collections.Generic;
using System.IO;

namespace FarCry
{
    public enum Ammunition
    {
        Handgun_Rounds,
        LMG_Rounds,
        Sniper_Rifle_Rounds,
    }

    public class FarCry3SaveAttributes
    {
        public uint ItemId;
        public long Position;
        public int ItemLength;

        //public byte[] ItemData = null;
    }
    public class FarCry3SaveEntry
    {
        public uint ItemId;
        public Dictionary<uint, FarCry3SaveAttributes> Attributes;
        public List<FarCry3SaveEntry> Children;
    }

    public class FarCry3Save
    {
        private readonly EndianIO _io, _saveFile;
        public FarCry3SaveEntry MainEntry;
        private readonly long _saveDataPosition;

        public FarCry3Save(EndianIO io)
        {
            _saveFile = io;

            int version = io.In.ReadInt32();
            if(version != 0xF)
                throw new FarCry3Exception("invalid save file version detected");

            io.In.SeekTo(0x14);
            string world = io.In.ReadAsciiString(io.In.ReadInt32());
            string subWorld = io.In.ReadAsciiString(io.In.ReadInt32());

            io.In.BaseStream.Position += 0x20;
            int var1 = io.In.ReadInt32();
            int var2 = io.In.ReadInt32();
            int var3 = io.In.ReadInt32();
            int var4 = io.In.ReadInt32();

            var thumbSize = (((var4*var3)*var2)*var1) >> 0x03;

            io.Stream.Position += thumbSize;

            int stringTableCount = io.In.ReadInt32();
            for (int i = 0; i < stringTableCount; i++)
            {
                io.In.ReadAsciiString(io.In.ReadInt32());
                io.In.ReadAsciiString(io.In.ReadInt32());
            }
            io.In.ReadInt32();
            if(io.In.ReadByte() < 1)
                return;

            _saveDataPosition = io.Position;

            int compressedBuferLen = io.In.ReadInt32();
            int decompressedBufferLen = io.In.ReadInt32();
            var ms = new MemoryStream();
            int retLen = LZO.LZO1X.Decompress(io.In.ReadBytes(compressedBuferLen), ms);
            _io = new EndianIO(ms.ToArray(), EndianType.LittleEndian, true);

            MainEntry = new FarCry3SaveEntry{ Children = new List<FarCry3SaveEntry>()};

            ReadSave();
        }

        public void Save()
        {
            var ms = new MemoryStream();
            var decompressedLen = (uint) _io.Stream.Length;
            var compressedLen = LZO.LZO1X.Compress(_io.ToArray(), decompressedLen, ms);

            _saveFile.SeekTo(_saveDataPosition);
            _saveFile.Out.Write(compressedLen);
            _saveFile.Out.Write(decompressedLen);
            _saveFile.Out.Write(ms.ToArray());

            _saveFile.Stream.SetLength(_saveDataPosition + compressedLen + 8);
        }

        public byte[] Export()
        {
            return _io.ToArray();
        }

        private void ReadSave()
        {
            if(_io.In.ReadUInt32() != 0x4643626E)
                throw new FarCry3Exception("invalid save magic");
            if(_io.In.ReadInt16() != 0x02)
                throw new FarCry3Exception("invalid save header data");

            var zeros = _io.In.ReadInt16();
            var maxEntryCount = _io.In.ReadInt32();
            var currentEntryCount = _io.In.ReadInt32();

            // read items and data
            ReadAttributes(MainEntry.Children);
        }

        private void ReadAttributes(List<FarCry3SaveEntry> parent)
        {
            var entry = new FarCry3SaveEntry { Attributes = new Dictionary<uint, FarCry3SaveAttributes>(), Children = new List<FarCry3SaveEntry>() };

            int childrenCount = _io.In.ReadByte();
            if (childrenCount == 0xFF)
                childrenCount = _io.In.ReadInt32();
            
            uint itemId = _io.In.ReadUInt32();
            entry.ItemId = itemId;
            int attributeCount = _io.In.ReadByte();
            for (int i = 0; i < attributeCount; i++)
            {
                itemId = _io.In.ReadUInt32();
                int itemLen = _io.In.ReadByte();
                if (itemLen == 0xFF)
                    itemLen = _io.In.ReadInt32();
                
                entry.Attributes.Add(itemId, new FarCry3SaveAttributes {ItemId = itemId, Position = _io.Position, ItemLength = (itemLen)});
                _io.Position += itemLen;
            }
            for (uint x = 0; x < childrenCount; x++)
                ReadAttributes(entry.Children);

            parent.Add(entry);
        }
        public byte[] GetEntryData(FarCry3SaveAttributes entry)
        {
            _io.SeekTo(entry.Position);
            return _io.In.ReadBytes(entry.ItemLength);
        }
        public FarCry3SaveEntry GetEntryFromItemId(uint itemId, FarCry3SaveEntry parent, int startIndex)
        {
            int index = parent.Children.FindIndex(startIndex, entry => entry.ItemId == itemId);
            if (index == -1)
                return null;
            return parent.Children[index];
        }
        public FarCry3SaveEntry GetEntryFromItemIndex(int index, FarCry3SaveEntry parent)
        {
            return parent.Children[index];
        }

        public void SetAttribute(FarCry3SaveEntry entry, bool value, uint attribute)
        {
            _io.Out.SeekNWrite(entry.Attributes[attribute].Position, Convert.ToByte(value));
        }
        public void SetAttribute(FarCry3SaveEntry entry, int value, uint attribute)
        {
            _io.Out.SeekNWrite(entry.Attributes[attribute].Position, value);
        }
        public void SetAttribute(FarCry3SaveEntry entry, float value, uint attribute)
        {
            _io.Out.SeekTo(entry.Attributes[attribute].Position);
            _io.Out.Write(value);
        }
    }
    internal class FarCry3Exception : Exception
    {
        internal FarCry3Exception(string message)
            : base("Far Cry 3: " + message)
        {

        }
    }
}
