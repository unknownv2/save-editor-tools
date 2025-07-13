using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicArts;
using System.IO;

namespace MedalOfHonor
{
    /*
     * Please Note:
     * After time spent mapping this, I found out if you mod grenades, 
     * ammo in clip, total ammo, etc it will only max it out..
     * 
     * Therefore if you make it so you have 2000 grenades, but the max is 2,
     * it will default to 2 as soon as you throw one grenade..
     * 
     * Making these values pointless so far.
     * */
    class MedalOfHonorSave
    {
        /// <summary>
        /// Our IO to edit our save file.
        /// </summary>
        public EndianIO IO { get; set; }

        /// <summary>
        /// Our MC02 security header, used by most EA games, in variation.
        /// </summary>
        private EA EaHeader;

        /// <summary>
        /// Our count of file entries in this save.
        /// </summary>
        private int File_Entry_Count { get; set; }

        /// <summary>
        /// Our header containing basic information of our save.
        /// </summary>
        public MOHHeader MOH_Header { get; set; }
        /// <summary>
        /// Our filetable containing file information for each file in this save.
        /// </summary>
        public MOHFileTable MOH_FileTable { get; set; }
        public test2 test { get; set; }

        /// <summary>
        /// A default initializer for this class with no parameters or processing done.
        /// </summary>
        public MedalOfHonorSave()
        {

        }

        /// <summary>
        /// Our entry point to initialize this component.
        /// </summary>
        /// <param name="SaveIO">Our save file IO to edit the file.</param>
        /// <param name="SaveEaHeader">Our MC02 security header from the save.</param>
        public MedalOfHonorSave(EndianIO SaveIO, EA SaveEaHeader)
        {
            //Set our IO
            this.IO = SaveIO;

            //Set our EA MC02 Header
            this.EaHeader = SaveEaHeader;

            //Go to our save offset
            this.IO.SeekTo(EA.EA_Header.HEADER_SIZE + SaveEaHeader.Header.Block1len);

            //Set our MOH_Header
            MOH_Header = new MOHHeader(IO);

            //Set our file table
            MOH_FileTable = new MOHFileTable(IO);

            test = new test2(IO);
        }

        /// <summary>
        /// This function will save any changes we made to our file and fix appropriate signatures.
        /// </summary>
        public void Save()
        {
            //Fix our signature/checksum
            this.FixChecksums();
        }

        /// <summary>
        /// This function will fix any checksums on our savegame file.
        /// </summary>
        private void FixChecksums()
        {
            //Go to the end of our header.
            this.IO.In.SeekTo(EA.EA_Header.HEADER_SIZE);
            //Read our first block, calculate our signature with that data.
            uint crc1 = EACRC32.Calculate(this.IO.In.ReadBytes(this.EaHeader.Header.Block1len), this.EaHeader.Header.Block1len);
            //Go to our second block position.
            this.IO.In.SeekTo(EA.EA_Header.HEADER_SIZE + this.EaHeader.Header.Block1len);
            //Read our second block.
            uint crc2 = EACRC32.Calculate(this.IO.In.ReadBytes(this.EaHeader.Header.Block2len), this.EaHeader.Header.Block2len);
            //Go to our signature offsets.
            this.IO.Out.SeekTo(0x10);
            //Write both of our signatures.
            this.IO.Out.Write(crc1);
            this.IO.Out.Write(crc2);
            //Go to our header
            IO.In.SeekTo(0x00);
            //Calculate our second block..
            uint crcHead = EACRC32.Calculate(IO.In.ReadBytes(0x18), 0x18);
            IO.Out.SeekTo(0x18);
            //Write it
            IO.Out.Write(crcHead);
        }

        public class MOHHeader
        {
            /// <summary>
            /// Our header magic.
            /// </summary>
            public const int Magic = 0x47564153;

            /// <summary>
            /// The version for our save file structure. It's always 1.
            /// </summary>
            public const int Version = 0x01;

            public int Unknown0 { get; set; }
            public int Unknown1 { get; set; }

            public MOHHeader(EndianIO IO)
            {
                //Read.
                Read(IO);
            }

            /// <summary>
            /// This function reads our header structure
            /// </summary>
            /// <param name="IO">Our IO to handle file reading and writing</param>
            public void Read(EndianIO IO)
            {
                //Read our magic
                if (IO.In.ReadInt32() != Magic)
                    throw new Exception("Not a valid Medal of Honor save. Magic mismatch.");

                //Read our version, lets not throw an exception if it's wrong, incase it's not actually version
                IO.In.ReadInt32();

                //Read our unknowns.
                Unknown0 = IO.In.ReadInt32();
                Unknown1 = IO.In.ReadInt32();
            }
        }

        public class MOHFileEntry
        {
            /// <summary>
            /// Get our filename for the file entry.
            /// </summary>
            public string FileName { get; set; }
        }

        public class MOHFileTable : List<MOHFileEntry>
        {
            /// <summary>
            /// The length of our filetable name
            /// </summary>
            public uint FileTableNameLength { get; set; }
            /// <summary>
            /// Our filetable name.
            /// </summary>
            public string FileTableName { get; set; }


            public uint Item_Count { get; set; }
            public int Unknown0 { get; set; }
            /// <summary>
            /// The length of our filetable name
            /// </summary>
            public uint FileTableNameLength2 { get; set; }
            /// <summary>
            /// Our filetable name.
            /// </summary>
            public string FileTableName2 { get; set; }




            public MOHFileTable(EndianIO IO)
            {
                //Clear our list
                this.Clear();
                //Read
                Read(IO);
            }

            public void Read(EndianIO IO)
            {
                //Read our file table name length.
                FileTableNameLength = IO.In.ReadUInt32();

                //Read our filetable name.
                FileTableName = IO.In.ReadAsciiString((int)FileTableNameLength);

                //Read our item count
                Item_Count = IO.In.ReadUInt32();

                //Loop for each item
                for (int i = 0; i < Item_Count; i++)
                {
                    //Create our MOH_File_Entry
                    MOHFileEntry file = new MOHFileEntry();

                    //Set our filename
                    file.FileName = IO.In.ReadAsciiString((int)IO.In.ReadUInt32());

                    //Add it to our list.
                    this.Add(file);
                }
                //Read our unknown
                Unknown0 = IO.In.ReadInt32();

                //Read our file table name length.
                FileTableNameLength2 = IO.In.ReadUInt32();

                //Read our filetable name.
                FileTableName2 = IO.In.ReadAsciiString((int)FileTableNameLength2);
            }
        }

        public class test2
        {
            public uint TimeStamp_Year { get; set; }
            public uint TimeStamp_Month { get; set; }
            public uint TimeStamp_Day { get; set; }
            public uint Unknown0 { get; set; }
            public uint MemAddr1 { get; set; }
            public uint MemAddr2 { get; set; }
            public uint MemAddr3 { get; set; }
            public uint Struct1Count { get; set; }
            public uint[] Structure1 { get; set; }
            public uint Struct2Count { get; set; }
            public moh_struct2[] Structure2 { get; set; }
            public uint Struct3Count { get; set; }//this struct is a set of voids.
            public moh_struct3[] Structure3 { get; set; }
            public uint Struct4Count { get; set; }//this struct contains current ammo in clips..
            public moh_struct4[] Structure4 { get; set; }
            public uint Struct5Count { get; set; } //this struct is like struct3, but reversed kinda..
            public moh_struct5[] Structure5 { get; set; }
            public uint Struct6Count { get; set; } //this struct is identical to struct5 im 80% sure.. worked out in all the saves ive seen.
            public moh_struct5[] Structure6 { get; set; }
            public uint Struct7Count { get; set; } //this struct is identical to struct5 im 80% sure.. worked out in all the saves ive seen.
            public uint[] Structure7 { get; set; } //never got anything other than 0s here, so im just going to treat it as padding..
            public uint Struct8Count { get; set; } 
            public moh_struct8[] Structure8 { get; set; } //Contains grenades, total ammo for weapon 1 and 2.
            public class moh_struct2
            {
                public uint Indexer { get; set; }
                public short Unknown0 { get; set; }
                public uint Flags { get; set; }
                public moh_struct2(EndianIO IO)
                {
                    //Read our structure
                    Read(IO);
                }
                public void Read(EndianIO IO)
                {
                    //Read our indexer
                    Indexer = IO.In.ReadUInt32();
                    //Read our unknown
                    Unknown0 = IO.In.ReadInt16();
                    //Read our flags
                    Flags = IO.In.ReadUInt32();
                }
                public void Write(EndianIO IO)
                {
                    //Write our indexer
                    IO.Out.Write(Indexer);
                    IO.Out.Write(Unknown0);
                    IO.Out.Write(Flags);
                }
            }
            public class moh_struct3
            {
                public uint Indexer { get; set; }
                public byte[] Data { get; set; }
                public uint Footer { get; set; }
                public moh_struct3(EndianIO IO)
                {
                    //Read our structure
                    Read(IO);
                }
                public void Read(EndianIO IO)
                {
                    //Read our indexer
                    Indexer = IO.In.ReadUInt32();
                    //Read our spare byte
                    IO.In.ReadByte();
                    //Read our data length, then our data
                    Data = IO.In.ReadBytes((int)IO.In.ReadUInt32());
                    //Read our footer
                    Footer = IO.In.ReadUInt32();
                }
                public void Write(EndianIO IO)
                {

                }
            }
            public class moh_struct4
            {
                public uint[] Values { get; set; }
                public moh_struct4(EndianIO IO)
                {
                    //Read our structure
                    Read(IO);
                }
                public void Read(EndianIO IO)
                {
                    //Init our array
                    Values = new uint[6];
                    //Loop for each element
                    for(int i = 0; i < Values.Length; i++)
                        //Set our element
                        Values[i] = IO.In.ReadUInt32();
                }
            }
            public class moh_struct5 : List<moh_insidestruct5>
            {
                public moh_struct5(EndianIO IO)
                {
                    //Read our structure
                    Read(IO);
                }
                public void Read(EndianIO IO)
                {
                    //Read our struct count.
                    uint structcount = IO.In.ReadUInt32();

                    //Loop for our count
                    for (int i = 0; i < structcount; i++)
                    {
                        //Add a structure.
                        this.Add(new moh_insidestruct5(IO));
                    }
                }
            }
            public class moh_insidestruct5
            {
                public uint Indexer { get; set; }
                public byte[] Data { get; set; }
                public moh_insidestruct5(EndianIO IO)
                {
                    //Read our structure
                    Read(IO);
                }
                public void Read(EndianIO IO)
                {
                    //Read our spare byte
                    IO.In.ReadByte();
                    //Read our data length, then our data
                    Data = IO.In.ReadBytes((int)IO.In.ReadUInt32());
                    //Read our indexer
                    Indexer = IO.In.ReadUInt32();
                    //Read our spare byte
                    IO.In.ReadByte();
                }
            }
            public class moh_struct8
            {
                public uint Indexer { get; set; }
                public uint Value0 { get; set; }
                public uint Value1 { get; set; }

                public moh_struct8(EndianIO IO)
                {
                    //Read our struct
                    Read(IO);
                }
                public void Read(EndianIO IO)
                {
                    //Read our indexer
                    Indexer = IO.In.ReadUInt32();
                    //Read our empty byte
                    IO.In.ReadByte();
                    //Read our first value
                    Value0 = IO.In.ReadUInt32();
                    //Read our second value
                    Value1 = IO.In.ReadUInt32();
                }
            }
            public test2(EndianIO IO)
            {
                //Read the structure
                Read(IO);
            }

            public void Read(EndianIO IO)
            {
                //Skip to our timestamp
                IO.In.SeekTo(0x0C, SeekOrigin.Current);

                //Read our year
                TimeStamp_Year = IO.In.ReadUInt32();
                //Read our month
                TimeStamp_Month = IO.In.ReadUInt32();
                //Read our day
                TimeStamp_Day = IO.In.ReadUInt32();

                //Read our unknown
                Unknown0 = IO.In.ReadUInt32();

                //Read our memory addresses for loading our data to
                MemAddr1 = IO.In.ReadUInt32();
                MemAddr2 = IO.In.ReadUInt32();
                MemAddr3 = IO.In.ReadUInt32();

                //Read garbage
                IO.In.ReadBytes(0x1C);

                //Read our structure count
                Struct1Count = IO.In.ReadUInt32();
                //Init our struct
                Structure1 = new uint[Struct1Count];
                //Loop for our struct count
                for (int i = 0; i < Struct1Count; i++)
                {
                    //Read our variable.
                    Structure1[i] = IO.In.ReadUInt32();
                }

                //Read our structure count
                Struct2Count = IO.In.ReadUInt32();
                //Create our struct
                Structure2 = new moh_struct2[Struct2Count];
                //Loop for our struct count
                for (int i = 0; i < Struct2Count; i++)
                {
                    //Read our variable.
                    Structure2[i] = new moh_struct2(IO);
                }

                //Read garbage.
                IO.In.ReadBytes(0x45);

                //Read our structure count
                Struct3Count = IO.In.ReadUInt32();
                //Create our struct array
                Structure3 = new moh_struct3[Struct3Count];
                //Loop for our struct count
                for (int i = 0; i < Struct3Count; i++)
                {
                    //Read our structure.
                    Structure3[i] = new moh_struct3(IO);
                }

                //Read our structure count
                Struct4Count = IO.In.ReadUInt32();
                //Create our struct array
                Structure4 = new moh_struct4[Struct4Count];
                //Loop for our struct count
                for (int i = 0; i < Struct4Count; i++)
                {
                    //Read our structure.
                    Structure4[i] = new moh_struct4(IO);
                }


                //Read our structure count
                Struct5Count = IO.In.ReadUInt32();
                //Create our struct array
                Structure5 = new moh_struct5[Struct5Count];
                //Loop for our struct count
                for (int i = 0; i < Struct5Count; i++)
                {
                    //Read our structure.
                    Structure5[i] = new moh_struct5(IO);
                }

                //Read our structure count
                Struct6Count = IO.In.ReadUInt32();
                //Create our struct array
                Structure6 = new moh_struct5[Struct6Count];
                //Loop for our struct count
                for (int i = 0; i < Struct6Count; i++)
                {
                    //Read our structure.
                    Structure6[i] = new moh_struct5(IO);
                }

                //Read our structure count
                Struct7Count = IO.In.ReadUInt32();
                //Read a blank(always came up as blank?) uint.
                IO.In.ReadUInt32();
                //Create our struct array
                Structure7 = new uint[Struct7Count];
                //Loop for our struct count
                for (int i = 0; i < Struct7Count; i++)
                {
                    //Read our structure.
                    Structure7[i] = IO.In.ReadUInt32();
                }

                //Read our structure count
                Struct8Count = IO.In.ReadUInt32();
                //Create our struct array
                Structure8 = new moh_struct8[Struct8Count];
                //Loop for our struct count
                for (int i = 0; i < Struct8Count; i++)
                {
                    //Read our structure.
                    Structure8[i] = new moh_struct8(IO);
                }
            }
        }
    }
}
