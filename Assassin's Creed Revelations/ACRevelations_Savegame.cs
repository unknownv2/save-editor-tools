using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ubisoft
{
    public class ACRevelationsSaveGame
    {
        EndianIO IO;

        internal ObjectSerializer ObjectManager;
        internal List<SG_CompressedSegment> CompressedSegments;
        internal List<int> CompressedSegmentSizes;
        private Compressor _compressEngine;

        public ACRevelationsSaveGame(EndianIO io)
        {
            if (io == null)
            {
                throw new ACException("AC: Revelations invalid save stream detected");
            }

            IO = io;

            ReadSaveData();
        }

        public void ReadSaveData()
        {
            int outLen;

            CompressedSegments = new List<SG_CompressedSegment>();
            _compressEngine = new Compressor();

            IO.Position = 0;
            IO.EndianType = EndianType.BigEndian;

            // read main segment
            if ((AssassinsCreedGameVerison)IO.In.ReadInt32() != AssassinsCreedGameVerison.Revelations || IO.In.ReadInt32() != 0x00FEDBAC)
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

                _compressEngine.ProcessDecompress(sgSubSeg.CompressionType, IO.In.ReadBytes(sgSubSeg.InputSize), ms, sgSubSeg.OutputSize);

                fileio.Out.Write(ms.ToArray());
                totalOutLen -= sgSubSeg.OutputSize;
                while (totalOutLen > 0 && IO.In.ReadByte() != 0x00)
                {
                    var inLen = IO.In.ReadInt32();
                    outLen = IO.In.ReadInt32();

                    IO.Position += 4; // checksum
                    ms = new MemoryStream();

                    _compressEngine.ProcessDecompress(sgSubSeg.CompressionType, IO.In.ReadBytes(inLen), ms, outLen);
                    fileio.Out.Write(ms.ToArray());
                    ms.Close();

                    totalOutLen -= outLen;
                }
            }

            fileio.Position = 0;
            //fileio.ToArray().Save(@"F:\Projects\Assassin's Creed\Assassin's Creed Revelations\Saves\Bugs\1\save.bin");
            ObjectManager = new ObjectSerializer(fileio);
        }

        public void Save()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);

            var saveData = new EndianIO(ObjectManager.Save(), EndianType.BigEndian, true);

            io.Out.Write((int)AssassinsCreedGameVerison.Revelations, EndianType.BigEndian);
            io.Out.Write(0x00FEDBAC, EndianType.BigEndian);

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

            // write the 3 sub-segments
            for (var i = 0; i < 0x03; i++)
            {
                var segmentSizePosition = io.Position;
                io.Out.Write(0); // segment size total
                io.Out.Write(0x01, EndianType.BigEndian);
                io.Out.Write(0x00CAFE00, EndianType.BigEndian);

                var totalOutputLen = CompressedSegmentSizes[i];

                io.Out.Write(totalOutputLen);
                segment = CompressedSegments[i + 1];

                compressedDataLen = _compressEngine.ProcessCompress(segment.CompressionType,
                                                         saveData.In.ReadBytes(segment.OutputSize), compressedStream = new MemoryStream());
                
                segment.InputSize = compressedDataLen;

                segment.Write(io.Out);
                io.Out.Write(compressedStream.ToArray());

                totalOutputLen -= segment.OutputSize;

                while (totalOutputLen > 0)
                {
                    var outputSize = totalOutputLen >= 0x8000  ? 0x8000 : totalOutputLen;

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
            }
            
            // write save data to package file
            IO.Stream.SetLength(io.Length);
            IO.SeekTo(0);
            IO.Out.Write(io.ToArray());

            io.Close();
            //IO = io;
            ReadSaveData();
        }

        internal class ObjectSerializer
        {
            internal long MoneyPosition = -1;
            public int Money = -1;
            internal string PlayerName;

            EndianIO IO;

            public ObjectSerializer(EndianIO io)
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
                if(MoneyPosition == -1)
                    throw new ACException("invalid save position found!");

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
            private void ReadAccomplishmentEntryObject(EndianReader reader)
            {
                ManagedObject gameObject;

                for (int x = 0; x < 4; x++)
                    gameObject = new ManagedObject(reader);
            }
            private void ReadPlayerProgressionSaveDataObject(EndianReader reader)
            {
                var ct = LoadObjectType1(reader);
                for (var x = 0; x < ct; x++)
                {
                    reader.ReadSingle();
                }
                ct = LoadObjectType1(reader);
                for (var x = 0; x < ct; x++)
                { 
                    // 82420750
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
                    if (x != 0x03)
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
                
                if(MoneyPosition == -1)
                {
                    MoneyPosition = reader.BaseStream.Position - 4;
                    Money = gameObject.Value.ToInt32(false);
                }

                reader.BaseStream.Position += (length - (pos + 4));
                //gameObject = new ManagedObject(reader);
            }
            private void ReadAssassinSaveGameDataObject()
            {
                ManagedObject gameObject;

                gameObject = new ManagedObject(IO.In);
                gameObject = new ManagedObject(IO.In); // false

                for (int i = 0; i < 14; i++)
                {
                    gameObject = new ManagedObject(IO.In);
                }
                gameObject = new ManagedObject(IO.In, true);
                //var reader = new EndianReader(gameObject.Value, EndianType.LittleEndian);
                var sbObj = new SGObjectEntry(IO.In);
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
    }
    internal enum CompressionType
    {
        LZO1X,
        LZO1X_999,
        LZO2A,
        LZX
    }
    internal class SG_CompressedSegment
    {
        public ulong Magic;
        public short Version;
        public CompressionType CompressionType;
        public int Flags;
        public bool InputBufferAvaiable;
        public int InputSize;
        public int OutputSize;
        public int Checksum;
        //public byte[] Input; // lzo-2a buffer

        public SG_CompressedSegment(EndianReader reader)
        {
            Magic = reader.ReadUInt64();

            if (Magic != 0x1004FA9957FBAA33)
                throw new ACException("invalid save segment data buffer magic detected.");

            Version = reader.ReadInt16();
            CompressionType = (CompressionType)reader.ReadByte();

            //if (CompressionType != 0x02)
                //throw new ACException("invalid save segment data type detected.");

            Flags = reader.ReadInt32();
            InputBufferAvaiable = reader.ReadByte() != 0x00;
            InputSize = reader.ReadInt32();
            OutputSize = reader.ReadInt32();
            Checksum = reader.ReadInt32();
            //Input = reader.ReadBytes(InputSize);
        }
        public void Write(EndianWriter writer)
        {
            writer.Write(Magic);
            writer.Write(Version);
            writer.Write((byte)CompressionType);
            writer.Write(Flags);
            writer.WriteByte(InputBufferAvaiable);
            writer.Write(InputSize);
            writer.Write(OutputSize);
            writer.Write(Checksum);
        }
    }
    internal class SGObjectEntry
    {
        public byte ClassVersion;
        public uint Name;
        public uint ID;
        public byte InstancingMode;
        public uint FatherID;
        public uint Type;
        public int ObjectLength;
        public int PropertiesLength;

        internal SGObjectEntry(EndianReader reader)
        {
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
    internal class PPGObjectEntry
    {
        public byte ClassVersion;
        public uint Name;
        public uint ID;
        public byte InstancingMode;
        public uint FatherID;
        public uint Type;
        public int ObjectLength;
        public int PropertiesLength;

        internal PPGObjectEntry(EndianReader reader)
        {
            ClassVersion = reader.ReadByte();
            int a = reader.ReadByte();
            for (int i = 0; i < a; i++)
            {
                reader.ReadInt32();
                reader.ReadInt16();
            }
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