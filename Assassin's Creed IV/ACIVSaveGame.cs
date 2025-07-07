using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ubisoft
{
    internal class ACIVSaveGame
    {
        internal EndianIO IO;

        internal ObjectDeserializer ObjectManager;
        internal List<SG_CompressedSegment> CompressedSegments;
        internal List<int> CompressedSegmentSizes;
        private Compressor _compressEngine;

        internal EndianIO saveIo;

        internal ACIVSaveGame(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            int outLen;

            CompressedSegments = new List<SG_CompressedSegment>();
            _compressEngine = new Compressor();

            IO.Position = 0;
            IO.EndianType = EndianType.BigEndian;

            // read main segment
            if ((AssassinsCreedGameVerison)IO.In.ReadInt32() != AssassinsCreedGameVerison.IV || IO.In.ReadInt32() != 0x00FEDBAC || IO.In.ReadInt64() != 0x00)
                throw new ACException("invalid save main segment header detected.");

            var mainsegmentSize = IO.In.ReadInt32();

            IO.EndianType = EndianType.LittleEndian;

            var fileio = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            var ms = new MemoryStream();

            if (mainsegmentSize != 0x00)
            {
                outLen = IO.In.ReadInt32();
                var sgMainSeg = new SG_CompressedSegment(IO.In);
                CompressedSegments.Add(sgMainSeg);
                _compressEngine.ProcessDecompress(sgMainSeg.CompressionType, IO.In.ReadBytes(sgMainSeg.InputSize), ms, outLen);
                fileio.Out.Write(ms.ToArray());
            }
            ms.Close();
            CompressedSegmentSizes = new List<int>();

            // read sub-segments
            var segmentSize = IO.In.ReadInt32(EndianType.BigEndian);
            if (segmentSize == 0) return;

            if (IO.In.ReadInt32(EndianType.BigEndian) != 0x03 || IO.In.ReadInt32(EndianType.BigEndian) != 0x00CAFE00)
                throw new ACException("invalid save sub-segment header detected.");

            var totalOutLen = IO.In.ReadInt32();
            if (totalOutLen == 0x00) return;

            CompressedSegmentSizes.Add(totalOutLen);

            var sgSubSeg = new SG_CompressedSegment(IO.In);
            CompressedSegments.Add(sgSubSeg);
            ms = new MemoryStream();

            _compressEngine.ProcessDecompress(sgSubSeg.CompressionType, IO.In.ReadBytes(sgSubSeg.InputSize), ms, sgSubSeg.OutputSize);
            fileio.Out.Write(ms.ToArray());
            totalOutLen -= sgSubSeg.OutputSize;
            while (totalOutLen > 0 && IO.In.ReadByte() != 0x00)
            {
                var inLen = IO.In.ReadInt32();
                outLen = IO.In.ReadInt32();
                //sum = IO.In.ReadInt32();
                IO.Position += 4;
                ms = new MemoryStream();

                _compressEngine.ProcessDecompress(sgSubSeg.CompressionType, IO.In.ReadBytes(inLen), ms, outLen);
                fileio.Out.Write(ms.ToArray());
                ms.Close();

                totalOutLen -= outLen;
            }

            saveIo = fileio;

            ObjectManager = new ObjectDeserializer(fileio);
        }
        public void Save()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);

            var saveData = new EndianIO(ObjectManager.Save(), EndianType.BigEndian, true);

            io.Out.Write((int)AssassinsCreedGameVerison.IV, EndianType.BigEndian);
            io.Out.Write(0x00FEDBAC, EndianType.BigEndian);
            io.Out.Write((long)0);

            // write first main segment
            var segment = CompressedSegments[0];
            MemoryStream compressedStream;

            var compressedDataLen = _compressEngine.ProcessCompress(segment.CompressionType,
                                                        saveData.In.ReadBytes(segment.OutputSize), compressedStream = new MemoryStream());
            segment.InputSize = compressedDataLen;

            io.Out.Write(0x1C + compressedDataLen + 4, EndianType.BigEndian);
            io.Out.Write(segment.OutputSize);
            segment.Write(io.Out);
            io.Out.Write(compressedStream.ToArray());

            // write the sub-segment

            var segmentSizePosition = io.Position;
            io.Out.Write(0); // segment size total
            io.Out.Write(0x03, EndianType.BigEndian);
            io.Out.Write(0x00CAFE00, EndianType.BigEndian);

            var totalOutputLen = CompressedSegmentSizes[0];

            io.Out.Write(totalOutputLen);
            segment = CompressedSegments[1];

            compressedDataLen = _compressEngine.ProcessCompress(segment.CompressionType,
                                                     saveData.In.ReadBytes(segment.OutputSize), compressedStream = new MemoryStream());
            segment.InputSize = compressedDataLen;

            segment.Write(io.Out);
            io.Out.Write(compressedStream.ToArray());

            totalOutputLen -= segment.OutputSize;

            while (totalOutputLen > 0)
            {
                var outputSize = totalOutputLen >= 0x8000 ? 0x8000 : totalOutputLen;

                compressedDataLen = _compressEngine.ProcessCompress(segment.CompressionType,
                                                    saveData.In.ReadBytes(outputSize), compressedStream = new MemoryStream());
                io.Out.WriteByte(0x01);
                io.Out.Write(compressedDataLen);
                io.Out.Write(outputSize);
                io.Out.Write(0x00); // checksum
                io.Out.Write(compressedStream.ToArray());

                compressedStream.Close();

                totalOutputLen -= outputSize;
            }

            var pos = io.Position;
            io.EndianType = EndianType.BigEndian;
            io.Out.SeekNWrite(segmentSizePosition, (int)(io.Position - segmentSizePosition));
            io.EndianType = EndianType.LittleEndian;
            io.SeekTo(pos);

            // write save data to package file
            IO.Stream.SetLength(io.Length);
            IO.SeekTo(0);
            IO.Out.Write(io.ToArray());

            io.Close();
            //IO = io;
            Read();
        }

        internal enum Characters
        {
            Edward,
            Adewale
        }

        internal class ObjectDeserializer
        {
            internal List<Dictionary<uint, InventoryItemContainer>> Inventory = new List<Dictionary<uint, InventoryItemContainer>>();

            internal string PlayerName;

            internal enum ObjectType
            {
                Boolean = 0x00,

                Int32 = 0x07,

                Class = 0x12,

                Blob = 0x13,
                AsciiString = 0x1A,
                UnicodeString = 0x1B
            }

            EndianIO IO;

            public ObjectDeserializer(EndianIO io)
            {
                IO = io;

                IO.EndianType = EndianType.LittleEndian;
                IO.Position = 0;

                Read();
            }

            public void Read()
            {
                //read main save header
                uint Type;
                do
                {
                    var t = IO.In.ReadInt32();
                    var NbClassVersionsInfo = IO.In.ReadByte();
                    var ObjectName = IO.In.ReadUInt32();
                    var ObjectID = IO.In.ReadUInt32();
                    var InstancingMode = IO.In.ReadByte();
                    if (InstancingMode == 0x01)
                    {
                        var FatherID = IO.In.ReadUInt32();
                    }

                    Type = IO.In.ReadUInt32();
                    if (Type <= 0) continue;

                    var totalObjectSize = IO.In.ReadInt32();
                    var relativeObjectSize = IO.In.ReadInt32();
                    if (totalObjectSize > 0)
                    {
                        switch (Type)
                        {
                            case 0xBDBE3B52: // SaveGame
                                ReadSaveGameObject(IO.In);
                                break;
                            case 0x94D6F8F1:
                                ReadAssassinSaveGameDataObject();
                                break;
                        }
                    }
                } while (Type != 0x00);

            }

            public byte[] Save()
            {
                foreach (Dictionary<uint, InventoryItemContainer> itemContainers in Inventory)
                {
                    foreach (KeyValuePair<uint, InventoryItemContainer> inventoryItemContainer in itemContainers)
                    {
                        IO.SeekTo(inventoryItemContainer.Value.Position);
                        IO.Out.Write(inventoryItemContainer.Value.Value);
                    }
                }

                return IO.ToArray();
            }

            private void ReadSaveGameObject(EndianReader reader)
            {
                var gameObject = new ManagedObject();

                // 1-5 = Int32
                // 6 = Boolean
                // 7 = String
                for (int x = 0; x < 7; x++)
                    gameObject = new ManagedObject(reader);

                PlayerName = ObjectReader.ReadAsciiString(new EndianReader(gameObject.Value, EndianType.LittleEndian));

                for (int x = 0; x < 3; x++)
                    gameObject = new ManagedObject(reader);

                ReadDynamicProperties();
            }
            private void ReadPlayerProgressionSaveDataObject(EndianReader reader)
            {
                var ct = LoadObjectType1(reader);
                for (var x = 0; x < ct; x++)
                {
                    reader.ReadInt64();
                }
                ct = LoadObjectType1(reader);
                for (var x = 0; x < ct; x++)
                {
                    reader.ReadByte();
                    int a = reader.ReadByte();
                    for (int i = 0; i < a; i++)
                    {
                        reader.ReadInt32();
                        reader.ReadInt16();
                    }
                    reader.ReadInt32();
                    int objectName = reader.ReadInt32();
                    int objectId = reader.ReadInt32();
                    byte instancingMode = reader.ReadByte();
                    int type = reader.ReadInt32();
                    int objectSize = reader.ReadInt32();
                    if (x != 9 && x != 0x0C)
                        reader.BaseStream.Position += (objectSize);
                    else
                    {
                        // PlayerProgressionCharacterData
                        var pos = reader.BaseStream.Position;
                        var objDataLen = reader.ReadInt32();
                        // player data
                        var gameObject = new ManagedObject(reader);
                        gameObject = new ManagedObject(reader, true);
                        ReadLogicalInventory(reader);
                        reader.BaseStream.Position += (pos + objectSize - (reader.BaseStream.Position));
                    }
                }
            }
            private void ReadLogicalInventory(EndianReader reader)
            {
                var sbObj = new ObjectEntry(reader);
                var objCount = LoadObjectType1(reader);
                if(objCount <= 0)
                    return;

                Inventory.Add(new Dictionary<uint, InventoryItemContainer>());
                for (int x = 0; x < objCount; x++)
                {
                    reader.ReadByte();
                    int a = reader.ReadByte();
                    for (int i = 0; i < a; i++)
                    {
                        reader.ReadInt32();
                        reader.ReadInt16();
                    }
                    reader.ReadInt32();
                    int objectName = reader.ReadInt32();
                    int objectId = reader.ReadInt32();
                    byte instancingMode = reader.ReadByte();
                    int type = reader.ReadInt32();
                    int objectSize = reader.ReadInt32();
                    int propertiesSize = reader.ReadInt32();
                    switch (type)
                    {
                        case 0x8CE3886:
                            InventoryRechargeableContainer(reader, propertiesSize);
                            break;
                        default:
                            reader.BaseStream.Position += propertiesSize;
                            break;
                    }
                }

            }
            private void InventoryRechargeableContainer(EndianReader reader, int length)
            {
                var pos = reader.BaseStream.Position;

                InventoryItemContainer item = new InventoryItemContainer();
                // InventoryItemContainer
                var gameObjectId = new ManagedObject(reader);
     
                uint id = (uint)(gameObjectId.Value.ToLong(false) >> 0x20);

                var gameObject = new ManagedObject(reader);
                gameObject = new ManagedObject(reader);

                item.Position = reader.BaseStream.Position - 4;
                item.Value = gameObject.Value.ToInt32(false);

                Inventory[Inventory.Count - 1].Add(id, item);

                long pos2 = (length - (reader.BaseStream.Position - pos)) + 4;
                reader.BaseStream.Position += pos2;
            }
            private void ReadAssassinSaveGameDataObject()
            {
                ManagedObject gameObject;
                //SGObjectEntry objectEntry;

                gameObject = new ManagedObject(IO.In);
                gameObject = new ManagedObject(IO.In); // false


                for (int i = 0; i < 14; i++)
                {
                    gameObject = new ManagedObject(IO.In);
                }
                gameObject = new ManagedObject(IO.In, true);
                //var reader = new EndianReader(gameObject.Value, EndianType.LittleEndian);
                var sbObj = new ObjectEntry(IO.In);
                ReadPlayerProgressionSaveDataObject(IO.In);
            }
            private int LoadObjectType1(EndianReader reader)
            {
                var gameObject = new ManagedObject(reader, true);

                byte proc = reader.ReadByte();
                int size = 0;
                switch (proc)
                {
                    case 1:
                        size = reader.ReadInt32();
                        break;
                }

                return size & 0x3FFF;
            }

            private void ReadDynamicProperties()
            {
                int dynamicPropCount = IO.In.ReadInt32();
                for (int i = 0; i < dynamicPropCount; i++)
                {
                    // fill in
                }
            }
        }
        internal class ObjectEntry
        {
            public byte ClassVersion;
            public uint Name;
            public uint ID;
            public byte InstancingMode;
            public uint FatherID;
            public uint Type;
            public int ObjectLength;
            public int PropertiesLength;

            internal ObjectEntry(EndianReader reader)
            {
                reader.ReadUInt32();
                ClassVersion = reader.ReadByte();
                Name = reader.ReadUInt32();
                ID = reader.ReadUInt32();
                InstancingMode = reader.ReadByte();
                if (InstancingMode == 0x01)
                    FatherID = reader.ReadUInt32();
                Type = reader.ReadUInt32();
                ObjectLength = reader.ReadInt32();
                PropertiesLength = reader.ReadInt32();
            }
        }
    }
}
