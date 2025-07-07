using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ElectronicArts;

namespace ResidentEvil
{
    public class BitStream
    {
        public int DataSize;
        public int StreamSize;
        public int Position;

        public long RealPosition
        {
            get
            {
                return (Position >> 3);
            }
        }
        public EndianIO IO;
        public BitStream(EndianIO IO)
        {
            if (IO != null)
                this.IO = IO;

            StreamSize = DataSize = (int)(IO.Stream.Length << 3);
        }
        public int ReadInt32()
        {
            return BitConverter.ToInt32(Horizon.Functions.Global.convertToBigEndian(Read(0x20, true)), 0);
        }
        public uint ReadUInt32()
        {
            return BitConverter.ToUInt32(Horizon.Functions.Global.convertToBigEndian(Read(0x20, true)), 0);
        }
        public byte ReadByte(bool AdvancePosition)
        {
            IO.In.SeekTo(Position >> 3);
            if(AdvancePosition)
                Position++;

            return this.IO.In.ReadByte();
        }
        public byte ReadByte()
        {
            return this.ReadByte(true);
        }
        public byte[] ReadBytes(int count)
        {
            IO.In.SeekTo(Position >> 3);
            Position += count;
            return this.IO.In.ReadBytes(count >> 3);
        }
        public byte Read()
        {
            if((Position + 1) > DataSize)
                throw new Exception("invalid player data stream position detected.");
            int val, xer = 0, tmpval;
            val = IO.In.SeekNReadByte(Position >> 3);
            val = val & (0x80 >> (Position & 0x07));
            tmpval = (val + 0x0FFFF);
            if (tmpval > 0x0FFFF)
                xer = 1;
            val = ~tmpval + val + xer;
            Position++;
            return (byte)(val & 0xFF);
        }
        public byte[] Read(int count, bool StoreRemainder)
        {
            var io = new EndianIO(new byte[count >> 3], EndianType.BigEndian, true);
            int blocksize = count, remainder, alignment, idx = 0, bitshift, val;

            if ((Position + blocksize) > DataSize)
                throw new Exception("invalid player data stream position detected.");

            if (((alignment = (Position & 0x07)) == 0) && ((blocksize & 0x07) == 0))
            {
                io.Out.Write(IO.In.ReadBytes(blocksize >> 3)); // count = blocksize / 8
                Position += blocksize;
                return io.ToArray();
            }

            do
            {
                val = (io.In.SeekNReadByte(idx) | (ReadByte(false) << alignment));
                io.Out.SeekNWrite(idx, (byte)val);

                if (alignment != 0)
                {
                    if (blocksize > (bitshift = (8 - alignment)))
                    {
                        IO.In.SeekTo((Position >> 3) + 1);
                        io.Out.SeekNWrite(idx, (byte)((IO.In.ReadByte() >> (bitshift & 0xFF)) | (val & 0xFF)));
                    }
                }


                if (blocksize < 0x08)
                {
                    if ((remainder = (blocksize - 8)) >= 0)
                    {
                        Position += 8;
                    }
                    else
                    {
                        if (StoreRemainder)
                        {
                            io.Out.SeekNWrite(idx, (byte)(io.In.SeekNReadByte(idx) >> (~remainder & 0xFF)));
                        }
                        Position += (remainder + 8);
                    }

                    break;
                }

                idx++;
                Position += 8;

            } while ((blocksize -= 0x08) > 0);
            return io.ToArray();
        }
    }
    public class OCRSaveGame
    {
        public struct OCR_SaveSubHeaderEntry
        {
            public int Index;
            public int Flags;
            public int Address; // Section Offset
            public int Length; // Section Size
            public uint Checksum;
        }

        private enum SaveSectionType
        {
            PlayerData = 3
        }
        public struct SerializedObject
        {
            public int Value;
            public float FloatValue;
            public byte ByteValue;

            public long Address;
        }
        private EndianIO IO;
        private List<OCR_SaveSubHeaderEntry> SaveSections;

        // Player Data
        public SerializedObject Experience;

        public OCRSaveGame(EndianIO IO)
        {
            if(IO != null)
                this.IO = IO;

            if (!VerifySave())
#if INT2
                System.Diagnostics.Debug.WriteLine("save file is corrupted.");
#else
                throw new Exception("save file is corrupted.");
#endif

            this.Read();
        }

        private void Read()
        {
            this.ReadSaveSections();
        }

        public void Save()
        {
            this.SaveStatData();
            this.FixSaveHeader();
        }

        private bool VerifySave()
        {
            IO.In.SeekTo(0x00);
            uint storedSum = IO.In.ReadUInt32();
            byte[] saveData = IO.In.ReadBytes(IO.Stream.Length - 4);
            uint sum = EACRC32.Calculate_Alt3(saveData, saveData.Length, 0);

            if(sum != storedSum)
                throw new Exception("save file header is corrupted.");

            IO.In.SeekTo(0x08);
            int subSectionCount = IO.In.ReadInt32();
            if(subSectionCount > 0)
            {
                SaveSections = new List<OCR_SaveSubHeaderEntry>();
                for (int x = 0; x < subSectionCount; x++)
                {
                    IO.In.SeekTo(0x0C + (0x14 * x));
                    OCR_SaveSubHeaderEntry ocr_sub = new OCR_SaveSubHeaderEntry()
                    {
                        Index = IO.In.ReadInt32(),
                        Flags = IO.In.ReadInt32(),
                        Address = IO.In.ReadInt32(),
                        Length = IO.In.ReadInt32(),
                        Checksum = IO.In.ReadUInt32()
                    };

                    IO.In.SeekTo(ocr_sub.Address);
                    saveData = IO.In.ReadBytes(ocr_sub.Length);
                    if (EACRC32.Calculate_Alt3(saveData, saveData.Length, 0) != ocr_sub.Checksum)
                        throw new Exception("save file section is corrupted.");
                    SaveSections.Add(ocr_sub);
                }
            }

            return true;
        }

        private void ReadSaveSections()
        {
            // Read player data section
            var PlayerDataSection = SaveSections[0x03];
            IO.In.SeekTo(PlayerDataSection.Address);
            BitStream PlayerStream = new BitStream(new EndianIO(IO.In.ReadBytes(SaveSections[0x03].Length), EndianType.BigEndian, true));

            int EntryCount, tail, subcount, ident, value_type;
            EntryCount = PlayerStream.ReadInt32();
            if (EntryCount > 0)
            {
                for (int x = 0; x < EntryCount; x++)
                {
                    uint entry_function = PlayerStream.ReadUInt32();
                    bool SectionIsFilled = false;
                    if ((PlayerStream.Position + 1) <= PlayerStream.DataSize)
                    {
                        SectionIsFilled = PlayerStream.Read() == 1;
                    }

                    if (SectionIsFilled)
                    {
                        subcount = PlayerStream.ReadInt32();
                        for (int i = 0; i < subcount; i++)
                        {
                            ident = PlayerStream.ReadInt32();
                            value_type = PlayerStream.ReadInt32();
                            switch (value_type)
                            {
                                case 0:
                                    tail = PlayerStream.ReadInt32();
                                    break;
                                case 1:
                                    float f_tail = BitConverter.ToSingle(Horizon.Functions.Global.convertToBigEndian(PlayerStream.Read(0x20, true)), 0);
                                    break;
                                case 2:
                                    byte b_tail = PlayerStream.Read();
                                    break;
                            }
                        }
                    }
                    else
                    {
                        value_type = PlayerStream.ReadInt32();
                        long pos = PlayerStream.RealPosition;
                        switch (value_type)
                        {
                            case 0:
                                tail = PlayerStream.ReadInt32();
                                {
                                    LoadStatData(entry_function, tail, pos);
                                }
                                break;
                            case 1:
                                float f_tail = BitConverter.ToSingle(Horizon.Functions.Global.convertToBigEndian(PlayerStream.Read(0x20, true)), 0);
                                break;
                            case 2:
                                byte b_tail = PlayerStream.Read();
                                break;
                        }
                    }
                }
            }
        }

        private void LoadStatData(uint Ident, int value, long Position)
        {
            switch (Ident)
            {
                case 0x5c242888:
                    Experience = new SerializedObject()
                        {
                            Value = value,
                            Address = Position,
                            ByteValue = 0,
                            FloatValue = 0
                        };
                    break;
            }
        }

        private void SaveStatData()
        {
            // Write XP value
            this.IO.Out.SeekTo(SaveSections[0x03].Address + Experience.Address);
            this.IO.Out.Write(Experience.Value);
        }

        private void FixSaveHeader()
        {
            // Fix sub-section data sums
            for (int x = 0; x < this.SaveSections.Count; x++)
            {
                IO.In.SeekTo(SaveSections[x].Address);
                byte[] Savedata = IO.In.ReadBytes(SaveSections[x].Length);
                uint sum = EACRC32.Calculate_Alt3(Savedata, Savedata.Length, 0);
                IO.Out.SeekTo(0x0C + (0x14 * SaveSections[x].Index) + 0x10);
                IO.Out.Write(sum);
            }

            // Fix global save checksum
            IO.In.SeekTo(0x04);
            byte[] GameSave = IO.In.ReadBytes(IO.Stream.Length - 4);
            IO.Out.SeekTo(0x00);
            IO.Out.Write(EACRC32.Calculate_Alt3(GameSave, GameSave.Length, 0x00));
        }
    }
}