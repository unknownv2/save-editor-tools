using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Horizon.PackageEditors.FEAR2
{
    //SAVE SIZE MUST BE DIVISIBLE BY 0xFFF4
    public class FEAR2Class
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values
        private int Editting_Start = -1;

        private uint Unknown0 { get; set; }
        public string Level_Name { get; set; }
        public string Checkpoint_Name { get; set; }
        private uint Unknown1 { get; set; }
        private uint Unknown2 { get; set; }
        private uint CRC_Block_Count { get; set; }
        private List<byte[]> Compressed_Blocks { get; set; }
        public BaseBlockInfoStructure Info_Struct { get; set; }
        #endregion

        #region Constructor

        public FEAR2Class(EndianIO io)
        {
            //Set our IO
            IO = io;
            //Read our gamesave
            Read();
        }

        #endregion

        #region Functions

        public void Read()
        {
            //Set our position
            IO.In.BaseStream.Position = 0;
            //Read our unknown.
            Unknown0 = IO.In.ReadUInt32();
            //Read our level name
            Level_Name = IO.In.ReadAsciiString(IO.In.ReadUInt16());
            //Read our checkpoint name
            Checkpoint_Name = IO.In.ReadUnicodeString(IO.In.ReadUInt16());
            //Read our 0 byte
            IO.In.ReadByte();
            //Read our unknowns
            Unknown1 = IO.In.ReadUInt32();
            Unknown2 = IO.In.ReadUInt32();
            //Set our editting start for this position
            Editting_Start = (int)IO.In.BaseStream.Position;
            //Read our value
            CRC_Block_Count = IO.In.ReadUInt32();
            //Initialize our array
            Compressed_Blocks = new List<byte[]>();
            //Create our hash section variable.
            int hashSection = 0;
            bool readHash = false;
            //Loop for our zlib block count
            while (true)
            {
                //Determine if we read our hash for this section
                if (!readHash)
                {
                    //Read our hash
                    IO.In.ReadUInt32();
                    //Set our bool
                    readHash = true;
                }

                //Sometimes here instead of being count theres another variable before.. Simple temporary check for now..
                uint compressedSize = IO.In.ReadUInt32();

                //If our compressed size is 0..
                if (compressedSize == 0)
                    break;

                //Read our data
                Compressed_Blocks.Add(IO.In.ReadBytes(compressedSize));

                //Determine our hash section
                int newHashSection = (int)IO.In.BaseStream.Position / 0xFFF4;
                //If our hash sections aren't the same
                if (newHashSection != hashSection)
                {
                    //Set our hash section, set our read hash bool aswell
                    readHash = false;
                    hashSection = newHashSection;
                }
            }

            //Read our info structure.
            Info_Struct = new BaseBlockInfoStructure(Ionic.Zlib.ZlibStream.UncompressBuffer(Compressed_Blocks[0]));

        }

        public void Write()
        {
            //Set our first block
            Info_Struct.Write();
            Compressed_Blocks[0] = Ionic.Zlib.ZlibStream.CompressBuffer(Info_Struct.ToArray());
            
            //Go to the position
            IO.In.BaseStream.Position = Editting_Start + 0x08;
            //Get our old block size
            uint blockSize = IO.In.ReadUInt32();
            //If our size is bad
            if (blockSize < Compressed_Blocks[0].Length)
                throw new Exception("The new saved block is bigger than the original. This is not supported at the moment. Please inform Horizon Staff to update this.");
            //Go to our block position
            IO.Out.BaseStream.Position = Editting_Start + 0x0C;
            IO.Out.Write(Compressed_Blocks[0]);
            //Go to the position
            IO.In.BaseStream.Position = Editting_Start + 0x08;
            //Determine our size to calculate for
            int size = 0xFFF4;
            //Compute our CRC
            uint crc = Compute(IO.In.ReadBytes(size));
            //Go to our CRC position
            IO.Out.BaseStream.Position = Editting_Start + 0x04;
            //Write our crc
            IO.Out.Write(crc);
        }

        /// <summary>
        /// Unlike the other write method, this one rebuilds the save. But currently, it does not hash the other blocks properly.
        /// </summary>
        public void Write_New()
        {
            //Set our first block
            Compressed_Blocks[0] = Ionic.Zlib.ZlibStream.CompressBuffer(Info_Struct.ToArray());

            //Set our position
            IO.Out.BaseStream.Position = 0;
            //Write our checksum count
            IO.Out.Write(Unknown0);

            //Create our list of CRC offsets
            List<int> CRC_Offsets = new List<int>();
            //Create our current hashSection variable
            int hashSection = 0;
            bool writtenHash = false;
            //Loop for each compressed block
            for (int i = 0; i < Compressed_Blocks.Count; i++)
            {
                //Determine if we read our hash for this section
                if (!writtenHash)
                {
                    //Add our CRC offset
                    CRC_Offsets.Add((int)IO.Out.BaseStream.Position);
                    //Write our variable that we will overwrite, so this can be anything.. Cause we'll calculate our signature later
                    IO.Out.Write((uint)0x00);
                    //Set our bool
                    writtenHash = true;
                }

                //Write our compressed size
                IO.Out.Write((uint)Compressed_Blocks[i].Length);

                //Write our data
                IO.Out.Write(Compressed_Blocks[i]);

                //Determine our hash section
                int newHashSection = (int)IO.Out.BaseStream.Position / 0xFFF4;
                //If our hash sections aren't the same
                if (newHashSection != hashSection)
                {
                    //Set our hash section, set our read hash bool aswell
                    writtenHash = false;
                    hashSection = newHashSection;
                }
            }
            //Calculate our current last block size
            int lastBlockPaddingSize = 0xFFF4 - ((int)IO.Out.BaseStream.Position - (CRC_Offsets[CRC_Offsets.Count - 1] + 4));
            //Write our padding
            IO.Out.Write(new byte[lastBlockPaddingSize]);
            //Set our length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
            //Loop for each offset
            foreach (int crc_offset in CRC_Offsets)
            {
                //Go to the position
                IO.In.BaseStream.Position = crc_offset + 4;
                //Determine our size to calculate for
                int size = 0xFFF4;
                //If our size is too long for this block
                if (size + IO.In.BaseStream.Position > IO.In.BaseStream.Length)
                    size = (int)(IO.In.BaseStream.Length - IO.In.BaseStream.Position);
                //Compute our CRC
                uint crc = Compute(IO.In.ReadBytes(size));
                //Go to our CRC position
                IO.Out.BaseStream.Position = crc_offset;
                //Write our crc
                IO.Out.Write(crc);
            }
        }

        /// <summary>
        /// Our CRC computing function.
        /// </summary>
        /// <param name="Bytes">The bytes to compute the CRC for.</param>
        /// <returns>Returns the CRC.</returns>
        internal static uint num4;
        internal static uint num7;
        public uint Compute(byte[] Bytes)
        {
            int[] crc32Table = new int[0x101];
            int index = 0x0;
            do
            {
                int num3 = index;
                uint num9 = num7;
                do
                {
                    if ((num3 & 0x1) > 0x0)
                    {
                        num3 = (int)((((long)(num3 & 0xfffffffe)) / 0x2L) & 0x7fffffffL);
                        num3 ^= (int)num4;
                    }
                    else
                    {
                        num3 = (int)((((long)(num3 & 0xfffffffe)) / 0x2L) & 0x7fffffffL);
                    }
                    num9 += 0xffffffff;
                }
                while (num9 >= 0x1);
                crc32Table[index] = num3;
                index++;
            }
            while (index <= 0xff);
            MemoryStream stream = new MemoryStream(Bytes);
            uint num2 = 0xffffffff;
            byte[] buffer = new byte[0x401];
            int count = 0x400;
            for (int i = stream.Read(buffer, 0x0, count); i > 0x0; i = stream.Read(buffer, 0x0, count))
            {
                int num10 = i - 0x1;
                for (index = 0x0; index <= num10; index++)
                {
                    int num6 = (int)(num2 & 0xff) ^ buffer[index];
                    num2 = ((num2 & 0xffffff00) / 0x100) & 0xffffff;
                    num2 ^= (uint)crc32Table[num6];
                }
            }
            return ~num2;
        }

        #endregion
        #region Classes
        public class BaseBlockInfoStructure
        {
            public string LevelName { get; set; }
            public Dictionary<string, string> Values { get; set; }
            private byte[] Data { get; set; }
            private int PropOff = -1;
            public BaseBlockInfoStructure(byte[] data)
            {
                //Set our data
                Data = data;
                //Initialize our values dictionary.
                Values = new Dictionary<string, string>();
                //Read
                Read();
            }
            private int SearchFor(string str, int startOff)
            {
                //Create our IO
                EndianIO IO = new EndianIO(Data, EndianType.BigEndian);
                //Open our IO.
                IO.Open();

                //Loop for the first half of the file
                for (int i = startOff; i < Data.Length / 2; i++)
                {
                    IO.In.BaseStream.Position = i;
                    if (IO.In.ReadAsciiString(str.Length) == str)
                    {
                        IO.Close();
                        return i - 3;
                    }
                }

                //Close our IO
                IO.Close();
                throw new Exception("Could not find properties start.");
            }
            public void Read()
            {
                //Create our IO
                EndianIO IO = new EndianIO(Data, EndianType.BigEndian);
                //Open our IO.
                IO.Open();

                //Go to our position
                IO.In.BaseStream.Position = 0;
                //Read our level name
                LevelName = IO.In.ReadAsciiString(IO.In.ReadUInt16());
                //Jump the distance!
                IO.In.BaseStream.Position += 0x206;
                PropOff = (int)IO.In.BaseStream.Position;
                  Beginning: ;
                try
                {
                    //Go to propoff
                    IO.In.BaseStream.Position = PropOff;
                    //Loop for the count
                    for (int i = 0; true; i++)
                    {
                        //Read our byte
                        byte booln = IO.In.ReadByte(); //1
                        if (booln != 1)
                            break;
                        //Read our key
                        string key = IO.In.ReadAsciiString(IO.In.ReadInt16());
                        //Read our value
                        string value = IO.In.ReadAsciiString(IO.In.ReadInt16());
                        //Set our value
                        Values[key] = value;
                    }
                }
                catch
                {
                    Values.Clear();
                }
                if (Values.Count == 0)
                {
                    PropOff = SearchFor("PRBForceCP", LevelName.Length + 8);
                    goto Beginning;
                }
                //Close our IO
                IO.Close();
            }
            public void Write()
            {
                //Create our IO
                EndianIO IO = new EndianIO(Data, EndianType.BigEndian);
                //Open our IO.
                IO.Open();
                //Jump the distance!
                IO.Out.BaseStream.Position = PropOff;
                //Loop for each dictionary item
                foreach (string key in Values.Keys)
                {
                    //Write our byte
                    IO.Out.Write((byte)0x01);
                    //Write our key length
                    IO.Out.Write((ushort)key.Length);
                    //Write our key
                    IO.Out.WriteAsciiString(key, key.Length);
                    //Write our value length
                    IO.Out.Write((ushort)Values[key].Length);
                    //Write our value
                    IO.Out.WriteAsciiString(Values[key], Values[key].Length);
                }

                //Close our IO
                IO.Close();
            }
            public byte[] ToArray() { return Data; }
        }
        #endregion
    }
}
