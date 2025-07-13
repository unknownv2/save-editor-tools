using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FarCry;
using Ubisoft;

namespace WatchDogs
{
    internal class WatchDogsDataBase
    {
        private EndianIO _io;
        internal WatchDogsSaveEntry MainEntry;
        internal WatchDogsDataBase(EndianIO io)
        {
            if (io == null)
                throw new Exception("invalid save file I/O detected.");

            io.SetEndian(EndianType.LittleEndian);
            _io = io;

            // read root
            if (_io.In.ReadUInt32() != 0x4643626E)
                throw new Exception("invalid save magic");
            if (_io.In.ReadInt16() != 0x03)
                throw new Exception("invalid save header data");

            var zeros = _io.In.ReadInt16();
            var maxEntryCount = _io.In.ReadInt32();
            var currentEntryCount = _io.In.ReadInt32();

            MainEntry = new WatchDogsSaveEntry { Children = new List<WatchDogsSaveEntry>() };

            ReadAttributes(MainEntry.Children);
        }

        private void ReadAttributes(List<WatchDogsSaveEntry> parent)
        {
            var entry = new WatchDogsSaveEntry { Attributes = new Dictionary<uint, WatchDogsSaveAttributes>(), Children = new List<WatchDogsSaveEntry>() };
            bool subChildEntry;

            int childrenCount = _io.In.ReadByte();
            if (childrenCount >= 0xFE)
            {
                subChildEntry = childrenCount == 0xFE;
                childrenCount = _io.In.ReadInt32();
            }
            else
            {
                subChildEntry = false;
            }
            if(subChildEntry)
                return;

            uint itemId = _io.In.ReadUInt32();
            entry.ItemId = itemId;
            /*
            if (subChildEntry)
            {
                int subInfoLen = _io.In.ReadInt32();
                
                _io.Position += subInfoLen;
            }
            */
            int attributeCount = _io.In.ReadByte();
            if (attributeCount == 0xFF)
                attributeCount = _io.In.ReadInt32();

            for (int i = 0; i < attributeCount; i++)
            {
                itemId = _io.In.ReadUInt32();
                int itemLen = _io.In.ReadByte();
                switch (itemLen)
                {
                    case 0xFE:
                        itemLen = 0x04;
                        break;
                    case 0xFF:
                        itemLen = _io.In.ReadInt32();
                        break;
                }

                entry.Attributes.Add(itemId, new WatchDogsSaveAttributes { ItemId = itemId, Position = _io.Position, ItemLength = (itemLen) });

                _io.Position += itemLen;
                
            }
            for (uint x = 0; x < childrenCount; x++)
                ReadAttributes(entry.Children);

            parent.Add(entry);
        }
        public byte[] GetEntryData(WatchDogsSaveAttributes entry)
        {
            _io.SeekTo(entry.Position);
            return _io.In.ReadBytes(entry.ItemLength);
        }
    }

    internal class WatchDogsSaveEntry 
    {
        public uint ItemId;
        public Dictionary<uint, WatchDogsSaveAttributes> Attributes;
        public List<WatchDogsSaveEntry> Children;
    }
    internal class WatchDogsSaveAttributes
    {
        public uint ItemId;
        public long Position;
        public int ItemLength;
        internal bool Modified;
        public byte[] Data;
    }
    internal enum WatchDogsContainerEntryType
    {
        savegame = 0x02,
        userdata = 0x20,
        directorydata = 0x80
    }
    internal class WatchDogsContainerEntry
    {
        public int BlobSize;
        public byte Unknown1;
        public byte Type;

        internal byte[] blobData;

        internal WatchDogsContainerEntry(EndianIO io)
        {
            // Header data
            blobData = io.In.ReadBytes(0x54);
        }
    }
    internal class WatchDogsSave
    {
        private EndianIO _io;
        public WatchDogsSaveEntry MainEntry;
        public Dictionary<uint, WatchDogsContainerEntry> ContainerEntries;

        // this is static and should be updated if needed
        private Dictionary<uint, string> Containers = new Dictionary<uint, string>{{0xE4FAF63A, "savegame"}, {0x0B8E8B12, "userdata"}};
        private int saveGameBlobLen;
        private int MaxEntryCount, CurrentEntryCount;
        internal WatchDogsSave(EndianIO io)
        {
            if (io == null)
                throw new Exception("invalid save file I/O detected.");

            io.SetEndian(EndianType.LittleEndian);

            _io = io;

            MainEntry = new WatchDogsSaveEntry { Children = new List<WatchDogsSaveEntry>() };
            ContainerEntries = new Dictionary<uint, WatchDogsContainerEntry>();
            
            ReadSave();
        }
        internal byte[] Save(bool saveArray)
        {
            EndianIO io = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);
            io.Out.Write(0x4643626E);
            io.Out.Write((short)0x03);
            io.Out.Write((short)0);
            io.Out.Write(MaxEntryCount);
            io.Out.Write(CurrentEntryCount);

            WriteAttributes(MainEntry.Children, io);

            // fix the blob length information for the 'savegame' header
            io.SeekTo(0x21);
            io.Out.Write(saveGameBlobLen);
            io.Out.Write(saveGameBlobLen - 4);

            if(saveArray)
                io.Stream.Save(@"E:\Projects\Watch Dogs\Saves\Thierry\1\savegame_test2.bin");

            return io.ToArray();
        }

        private void ReadSave()
        {
            // read root
            if (_io.In.ReadUInt32() != 0x4643626E)
                throw new Exception("invalid save magic");
            if (_io.In.ReadInt16() != 0x03)
                throw new Exception("invalid save header data");

            var zeros = _io.In.ReadInt16();
            MaxEntryCount = _io.In.ReadInt32();
            CurrentEntryCount = _io.In.ReadInt32();

            // read items and data
            ReadAttributes(MainEntry.Children);
        }
        private void ReadAttributes(List<WatchDogsSaveEntry> parent)
        {
            var entry = new WatchDogsSaveEntry { Attributes = new Dictionary<uint, WatchDogsSaveAttributes>(), Children = new List<WatchDogsSaveEntry>() };

            int childrenCount = _io.In.ReadByte();
            if (childrenCount == 0xFF)
                childrenCount = _io.In.ReadInt32();

            uint itemId = _io.In.ReadUInt32();
            entry.ItemId = itemId;
            int attributeCount = _io.In.ReadByte();
            if (attributeCount == 0xFF)
                attributeCount = _io.In.ReadInt32();

            for (int i = 0; i < attributeCount; i++)
            {
                itemId = _io.In.ReadUInt32();
                int itemLen = _io.In.ReadByte();
                if (itemLen == 0xFF)
                    itemLen = _io.In.ReadInt32();

                entry.Attributes.Add(itemId, new WatchDogsSaveAttributes { ItemId = itemId, Position = _io.Position, ItemLength = (itemLen) });

                // Disrupt Game Engine (use Dunia Engine 3.0) contains entries under the "Root"={0xB6C65665} entry
                if (entry.ItemId == 0xE4FAF63A || entry.ItemId == 0x0B8E8B12)
                    // "savegame"={0xE4FAF63A}
                    // "userdata"={0x0B8E8B12}
                {
                    ContainerEntries.Add(entry.ItemId, new WatchDogsContainerEntry(_io));

                    //_io.Position += 0x44;

                    /*
                    if (_io.In.ReadUInt32() != 0x4643626E)
                        throw new Exception("invalid save magic");
                    if (_io.In.ReadInt16() != 0x03)
                        throw new Exception("invalid save header data");

                    var zeros = _io.In.ReadInt16();
                    var maxEntryCount = _io.In.ReadInt32();
                    var currentEntryCount = _io.In.ReadInt32();
                    */

                    ReadAttributes(entry.Children);
                }
                else
                {
                    _io.Position += itemLen;
                }
            }
            for (uint x = 0; x < childrenCount; x++)
                ReadAttributes(entry.Children);

            parent.Add(entry);
        }
        private void WriteAttributes(IEnumerable<WatchDogsSaveEntry> parent, EndianIO io)
        {
            foreach (WatchDogsSaveEntry saveEntry in parent)
            {
                var entry = saveEntry;

                // get the length of the 'savegame' data now that we've reached the 'userdata' entry
                if (entry.ItemId == 0x0B8E8B12)
                {
                    saveGameBlobLen = (int)(io.Position - (0x25));
                }

                int childrenCount = Containers.ContainsKey(entry.ItemId) ? 0 : entry.Children.Count;
                if (childrenCount > 0xFE)
                {
                    io.Out.WriteByte(0xFF);
                    io.Out.Write(childrenCount);
                }
                else
                    io.Out.WriteByte(childrenCount);

                io.Out.Write(entry.ItemId);
                int attributeCount = entry.Attributes.Count;
                if (attributeCount > 0xFE)
                {
                    io.Out.WriteByte(0xFF);
                    io.Out.Write(attributeCount);
                }
                else
                    io.Out.WriteByte(attributeCount);

                foreach (var attribute in entry.Attributes)
                {
                    io.Out.Write(attribute.Key);
                    int itemLen = attribute.Value.Modified ? attribute.Value.Data.Length : attribute.Value.ItemLength;
                    if (itemLen > 0xFE)
                    {
                        io.Out.WriteByte(0xFF);
                        io.Out.Write(itemLen);
                    }
                    else
                        io.Out.WriteByte(itemLen);

                    if (Containers.ContainsKey(entry.ItemId))
                    {
                        io.Out.Write(ContainerEntries[entry.ItemId].blobData);

                        // we are only editing the 'savegame' data, not 'userdata' (at-the-moment)
                        if (entry.ItemId == 0xE4FAF63A)
                        {

                        }
   
                        //WriteAttributes(entry.Children, io);
                    }
                    else
                    {
                        io.Out.Write(attribute.Value.Modified ? attribute.Value.Data : GetEntryData(attribute.Value));
                    }
                }
                WriteAttributes(entry.Children, io);
                
            }
        }
        public byte[] GetEntryData(WatchDogsSaveAttributes entry)
        {
            // if the entry has been modified, then the data will be stored in the entry
            if (entry.Modified)
            {
                return entry.Data;
            }
            // otherwise the data should be retrieved from the save stream
            _io.SeekTo(entry.Position);
            return _io.In.ReadBytes(entry.ItemLength);
            
        }
        public WatchDogsSaveEntry GetEntryFromItemId(uint itemId, WatchDogsSaveEntry parent, int startIndex)
        {
            int index = parent.Children.FindIndex(startIndex, entry => entry.ItemId == itemId);
            if (index == -1)
                return null;
            return parent.Children[index];
        }
        public WatchDogsSaveEntry GetEntryFromItemIndex(int index, WatchDogsSaveEntry parent)
        {
            return parent.Children[index];
        }

        public static void StaticSetAttribute(WatchDogsSaveEntry entry, object value, uint attribute)
        {
            entry.Attributes[attribute].Modified = true;
            var integerValue = Convert.ToInt64(value);
            if (integerValue > int.MaxValue)
                throw  new Exception("Invalid value exceeding range detected!");

            if (integerValue < 0xFE)
            {
                entry.Attributes[attribute].ItemLength = 0x01;
                entry.Attributes[attribute].Data = new []{(Convert.ToByte(value))};
            }
            else if (integerValue < 0xFFFE)
            {
                entry.Attributes[attribute].ItemLength = 0x02;
                entry.Attributes[attribute].Data = BitConverter.GetBytes(Convert.ToUInt16(value));
            }
            else
            {
                entry.Attributes[attribute].ItemLength = 0x04;
                entry.Attributes[attribute].Data = BitConverter.GetBytes(Convert.ToUInt32(value));
            }
            
            /*
            switch (entry.Attributes[attribute].ItemLength)
            {
                case 1:
                    _io.Out.SeekNWrite(entry.Attributes[attribute].Position, Convert.ToByte(value));
                    break;
                case 2:
                    _io.Out.SeekNWrite(entry.Attributes[attribute].Position, Convert.ToInt16(value));
                    break;
                case 4:
                    _io.Out.SeekNWrite(entry.Attributes[attribute].Position, Convert.ToInt32(value));
                    break;
            }
            */
        }
        public void SetAttribute(WatchDogsSaveEntry entry, bool value, uint attribute)
        {
            _io.Out.SeekNWrite(entry.Attributes[attribute].Position, Convert.ToByte(value));
        }
        public void SetAttribute(WatchDogsSaveEntry entry, int value, uint attribute)
        {
            _io.Out.SeekNWrite(entry.Attributes[attribute].Position, value);
        }
        public void SetAttribute(WatchDogsSaveEntry entry, float value, uint attribute)
        {
            _io.Out.SeekTo(entry.Attributes[attribute].Position);
        }

        public WatchDogsSaveEntry GetSaveEntry(WatchDogsSaveEntry entry, List<uint> accessIds, string attribute)
        {
            Dictionary<uint, int[]> searchDict = new Dictionary<uint, int[]>();
            WatchDogsSaveEntry originalEntry = entry;
            int accessSearchIndex = 0x00;
            while ((entry = FindItemEntryFromId(entry, accessIds, searchDict, ref accessSearchIndex)) == null)
            {
                entry = originalEntry;
            }
            return entry;
        }
        public WatchDogsSaveEntry FindItemEntryFromId(WatchDogsSaveEntry entry, List<uint> accessIds, Dictionary<uint, int[]> searchDict, ref int accessSearchIndex)
        {
            uint currentSearchId = accessIds[accessSearchIndex];
            foreach (uint accessId in accessIds)
            {
                if (!searchDict.ContainsKey(accessId) && entry != null)
                    searchDict.Add(accessId, new[] { 0x00, entry.Children.Count });

                entry = GetEntryFromItemId(accessId, entry, searchDict.ContainsKey(accessId) ? searchDict[accessId][0x00] : 0x00);

                if (entry != null) continue;
                if (searchDict[currentSearchId][0x00] < (searchDict[currentSearchId][0x01] - 1))
                    searchDict[accessIds[accessSearchIndex]][0x00]++;
                else
                {
                    searchDict[accessIds[accessSearchIndex]][0x00] = 0x00;
                    accessSearchIndex++;
                }

                break;
            }
            return entry;
        }

        public byte[] GetItemAttribute(List<uint> accessIds, string attribute)
        {
            var entry = MainEntry;
            Dictionary<uint, int[]> searchDict = new Dictionary<uint, int[]>();
            WatchDogsSaveEntry originalEntry = entry;
            int accessSearchIndex = 0x00;
            while ((entry = FindItemEntryFromId(entry, accessIds, searchDict, ref accessSearchIndex)) == null)
            {
                entry = originalEntry;
            }
            return GetEntryData(entry.Attributes[FarCry3Attribute.GetIdent(attribute)]);
        }

        public void SetItemAttribute(List<uint> accessIds, object value, string property, fValue.ObjectAttributes attributes)
        {
            var entry = MainEntry;
            Dictionary<uint, int[]> searchDict = new Dictionary<uint, int[]>();
            WatchDogsSaveEntry originalEntry = entry;
            int accessSearchIndex = 0x00;
            while ((entry = FindItemEntryFromId(entry, accessIds, searchDict, ref accessSearchIndex)) == null)
            {
                entry = originalEntry;
            }
            switch (attributes)
            {
                case fValue.ObjectAttributes.None:
                    StaticSetAttribute(entry, value, FarCry3Attribute.GetIdent(property));
                    break;
                case fValue.ObjectAttributes.Int32:
                    SetAttribute(entry, Convert.ToInt32(value), FarCry3Attribute.GetIdent(property));
                    break;
                case fValue.ObjectAttributes.Boolean:
                    SetAttribute(entry, Convert.ToBoolean(value), FarCry3Attribute.GetIdent(property));
                    break;
                case fValue.ObjectAttributes.Float:
                    SetAttribute(entry, Convert.ToSingle(value), FarCry3Attribute.GetIdent(property));
                    break;
            }
        }

        public void SetItemAttribute(WatchDogsSaveEntry entry, object value, uint attribute, fValue.ObjectAttributes attributes)
        {
            switch (attributes)
            {
                case fValue.ObjectAttributes.None:
                case fValue.ObjectAttributes.Byte:
                    StaticSetAttribute(entry, value, attribute);
                    break;
                case fValue.ObjectAttributes.Int32:
                    SetAttribute(entry, Convert.ToInt32(value), attribute);
                    break;
                case fValue.ObjectAttributes.Boolean:
                    SetAttribute(entry, Convert.ToBoolean(value), attribute);
                    break;
                case fValue.ObjectAttributes.Float:
                    SetAttribute(entry, Convert.ToSingle(value), attribute);
                    break;
            }
        }
    }
}
