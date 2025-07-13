using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FarCry
{
    internal class FarCry4Save
    {
        private EndianIO _io, _saveFile;
        private readonly long _saveDataPosition;
        public FarCry3SaveEntry MainEntry;

        internal FarCry4Save(EndianIO io)
        {
            if(io == null)
                throw new FarCry4Exception("invalid save stream detected!");

            _saveFile = io;

            int version = io.In.ReadInt32();
            if (version != 0x13)
                throw new FarCry4Exception("invalid save file version detected");

            io.In.SeekTo(0x14);
            string world = io.In.ReadAsciiString(io.In.ReadInt32());
            string subWorld = io.In.ReadAsciiString(io.In.ReadInt32());

            io.In.BaseStream.Position += 0x24;
            int var1 = io.In.ReadInt32();
            int var2 = io.In.ReadInt32();
            int var3 = io.In.ReadInt32();
            int var4 = io.In.ReadInt32();

            var thumbSize = (((var4 * var3) * var2) * var1) >> 0x03;

            io.Stream.Position += thumbSize;

            int stringTableCount = io.In.ReadInt32();
            for (int i = 0; i < stringTableCount; i++)
            {
                io.In.ReadAsciiString(io.In.ReadInt32());
                io.In.ReadAsciiString(io.In.ReadInt32());
            }
            io.In.ReadInt32();

            io.In.BaseStream.Position += 1;
            
            _saveDataPosition = io.Position;

            //int retLen = LZO.LZO1X.Decompress(io.In.ReadBytes(io.Length - io.Position), ms);
            _io = new EndianIO(io.In.ReadBytes(io.Length - io.Position), EndianType.LittleEndian, true);

            MainEntry = new FarCry3SaveEntry { Children = new List<FarCry3SaveEntry>() };

            ReadSave();
        }

        internal void Save()
        {
            _saveFile.SeekTo(_saveDataPosition);
            _saveFile.Out.Write(_io.ToArray());

            _saveFile.Stream.SetLength(_saveDataPosition + _io.Length);
        }

        internal byte[] Export()
        {
            return null;
        }
        private void ReadSave()
        {
            if (_io.In.ReadUInt32() != 0x4643626E)
                throw new FarCry3Exception("invalid save magic");
            if (_io.In.ReadInt16() != 0x02)
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

                entry.Attributes.Add(itemId, new FarCry3SaveAttributes { ItemId = itemId, Position = _io.Position, ItemLength = (itemLen) });
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
    internal class FarCry4Exception : Exception
    {
        internal FarCry4Exception(string message)
            : base("Far Cry 4: " + message)
        {

        }
    }
}
