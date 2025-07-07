using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using LZO;

namespace Ubisoft
{
    internal class ACBrotherhoodSaveGame
    {
        private EndianIO IO;
        internal ObjectDeserializer ObjectManager;
        internal List<SG_CompressedSegment> CompressedSegments;
        internal List<int> CompressedSegmentSizes;
        private Compressor _compressEngine;

        internal ACBrotherhoodSaveGame(EndianIO io)
        {
            if (io == null)
            {
                throw new ACException("AC: Brotherhood invalid save stream detected");
            }

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
            if ((AssassinsCreedGameVerison)IO.In.ReadInt32() != AssassinsCreedGameVerison.Brotherhood || IO.In.ReadInt32() != 0x00FEDBAC)
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

                //LZODecompressor(sgMainSeg.CompressionType, sgMainSeg.InputSize, ms, outLen);
                _compressEngine.ProcessDecompress(sgMainSeg.CompressionType, IO.In.ReadBytes(sgMainSeg.InputSize), ms, outLen);
                fileio.Out.Write(ms.ToArray());
            }
            else
            {
                throw new Exception("AC: could not find a main save segment for the save file.");
            }

            ms.Close();
            CompressedSegmentSizes = new List<int>();
            // read sub-segments
            while (IO.Position < IO.Length)
            {
                var segmentSize = IO.In.ReadInt32(EndianType.BigEndian);
                if (segmentSize == 0) continue;

                if (IO.In.ReadInt32(EndianType.BigEndian) != 0x01 || IO.In.ReadInt32(EndianType.BigEndian) != 0x00CAFE00)
                    throw new ACException("invalid save sub-segment header detected.");

                var totalOutLen = IO.In.ReadInt32();
                if (totalOutLen == 0x00) continue;

                CompressedSegmentSizes.Add(totalOutLen);

                var sgSubSeg = new SG_CompressedSegment(IO.In);
                CompressedSegments.Add(sgSubSeg);
                ms = new MemoryStream();
                _compressEngine.ProcessDecompress(sgSubSeg.CompressionType, IO.In.ReadBytes(sgSubSeg.InputSize) , ms, sgSubSeg.OutputSize);
                fileio.Out.Write(ms.ToArray());
                totalOutLen -= sgSubSeg.OutputSize;
                while (totalOutLen > 0 && IO.In.ReadByte() != 0x00)
                {
                    var inLen = IO.In.ReadInt32();
                    outLen = IO.In.ReadInt32();
                    IO.Position += 4; // checksum
                    ms = new MemoryStream();
                    _compressEngine.ProcessDecompress(sgSubSeg.CompressionType, IO.In.ReadBytes(inLen), ms, outLen);
                    //LZODecompressor(sgSubSeg.CompressionType, inLen, ms, outLen);
                    fileio.Out.Write(ms.ToArray());
                    ms.Close();

                    totalOutLen -= outLen;
                }
            }

            fileio.Position = 0;
            ObjectManager = new ObjectDeserializer(fileio);
        }

        internal void Save()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);

            var saveData = new EndianIO(ObjectManager.Save(), EndianType.BigEndian, true);

            io.Out.Write((int)AssassinsCreedGameVerison.Brotherhood, EndianType.BigEndian);
            io.Out.Write(0x00FEDBAC, EndianType.BigEndian);

            // write first main segment
            var segment = CompressedSegments[0];
            MemoryStream compressedStream;

            //var compressedDataLen = LZO.LZO2A.Compress(saveData.In.ReadBytes(segment.OutputSize), compressedStream, 0x00);
            //var compressedDataLen = LZO.LZO1X.Compress(saveData.In.ReadBytes(segment.OutputSize), (uint)segment.OutputSize,
                                                   //compressedStream);

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
            io.Out.Write(0x01, EndianType.BigEndian);
            io.Out.Write(0x00CAFE00, EndianType.BigEndian);

            var totalOutputLen = CompressedSegmentSizes[0];

            io.Out.Write(totalOutputLen);
            segment = CompressedSegments[1];

            //compressedDataLen = LZO.LZO2A.Compress(saveData.In.ReadBytes(segment.OutputSize), compressedStream, 0x00);
            //compressedDataLen = LZO.LZO1X.Compress(saveData.In.ReadBytes(segment.OutputSize), (uint) segment.OutputSize,
                                                   //compressedStream);

            compressedDataLen = _compressEngine.ProcessCompress(segment.CompressionType,
                                                        saveData.In.ReadBytes(segment.OutputSize), compressedStream = new MemoryStream());
           
            segment.InputSize = compressedDataLen;

            segment.Write(io.Out);
            io.Out.Write(compressedStream.ToArray());
            compressedStream.Close();
            totalOutputLen -= segment.OutputSize;

            while (totalOutputLen > 0)
            {
                var outputSize = totalOutputLen >= 0x8000 ? 0x8000 : totalOutputLen;

                //compressedDataLen = LZO.LZO2A.Compress(saveData.In.ReadBytes(outputSize), compressedStream, 0x00);

                //compressedDataLen = LZO.LZO1X.Compress(saveData.In.ReadBytes(outputSize), (uint)outputSize,
                                                       //compressedStream);

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

        internal class ObjectDeserializer
        {
            internal long MoneyPosition = -1;
            public int Money = -1;
            internal string PlayerName;

            EndianIO IO;

            public ObjectDeserializer(EndianIO io)
            {
                IO = io;

                IO.EndianType = EndianType.LittleEndian;
                //IO.Position = 0;

                Read();
            }

            public void Read()
            {
                //read main save header
                uint Type;
                do
                {
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
                IO.SeekTo(MoneyPosition);
                IO.Out.Write(Money);

                return IO.ToArray();
            }

            private void ReadSaveGameObject(EndianReader reader)
            {
                var gameObject = new ManagedObject();

                // 1-6 = Int32
                // 7 = Boolean
                for (int x = 0; x < 7; x++)
                    gameObject = new ManagedObject(reader);

                PlayerName = ObjectReader.ReadAsciiString(new EndianReader(gameObject.Value, EndianType.LittleEndian));

                for (int x = 0; x < 5; x++)
                    gameObject = new ManagedObject(reader);

                ReadDynamicProperties();
            }
            private void ReadPlayerProgressionSaveDataObject(EndianReader reader)
            {
                var ct = LoadObjectType1(reader);
                for (var x = 0; x < ct; x++)
                {
                    reader.ReadInt32();
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
                    int objectName = reader.ReadInt32();
                    int objectId = reader.ReadInt32();
                    byte instancingMode = reader.ReadByte();
                    int type = reader.ReadInt32();
                    int objectSize = reader.ReadInt32();
                    //int propertiesSize = reader.ReadInt32();
                    if (x != 0x02)
                        reader.BaseStream.Position += (objectSize);
                    else
                    {
                        var pos = reader.BaseStream.Position;
                        var objDataLen = reader.ReadInt32();
                        // player data
                        var gameObject = new ManagedObject(reader);
                        gameObject = new ManagedObject(reader, true);
                        ReadLogicalInventory(reader);//new EndianReader(gameObject.Value, EndianType.LittleEndian));
                        reader.BaseStream.Position += (pos + objectSize - (reader.BaseStream.Position));
                    }
                }
            }
            private void ReadLogicalInventory(EndianReader reader)
            {
                var sbObj = new SGObjectEntry(reader);
                var objCount = LoadObjectType1(reader);
                for (int x = 0; x < objCount; x++)
                {
                    reader.ReadByte();
                    int a = reader.ReadByte();
                    for (int i = 0; i < a; i++)
                    {
                        reader.ReadInt32();
                        reader.ReadInt16();
                    }
                    int objectName = reader.ReadInt32();
                    int objectId = reader.ReadInt32();
                    byte instancingMode = reader.ReadByte();
                    int type = reader.ReadInt32();
                    int objectSize = reader.ReadInt32();
                    int propertiesSize = reader.ReadInt32();
                    switch (type)
                    {
                        case 0x8CE3886:
                            InventoryRechargeableContainer(reader, objectSize);
                            break;
                    }
                }

            }
            private void InventoryRechargeableContainer(EndianReader reader, int length)
            {
                var pos = reader.BaseStream.Position;

                // InventoryItemContainer
                var gameObject = new ManagedObject(reader);
                gameObject = new ManagedObject(reader);
                gameObject = new ManagedObject(reader);

                if (MoneyPosition == -1)
                {
                    MoneyPosition = reader.BaseStream.Position - 4;
                    Money = gameObject.Value.ToInt32(false);
                }

                reader.BaseStream.Position += (length - (pos + 4));

            }
            private void ReadAssassinSaveGameDataObject()
            {
                ManagedObject gameObject;

                gameObject = new ManagedObject(IO.In);
                gameObject = new ManagedObject(IO.In); // false

                for (int i = 0; i < 12; i++)
                {
                    gameObject = new ManagedObject(IO.In);
                }
                gameObject = new ManagedObject(IO.In, true);
                //var reader = new EndianReader(gameObject.Value, EndianType.LittleEndian);
                var sbObj = new SGObjectEntry(IO.In);
                ReadPlayerProgressionSaveDataObject(IO.In);
                for (int i = 0; i < 7; i++)
                {
                    gameObject = new ManagedObject(IO.In);
                }
                
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
    }
}
