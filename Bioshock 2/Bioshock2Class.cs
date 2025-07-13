using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Bioshock_2
{
    public class Bioshock2Class
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values

        public List<byte[]> Compressed_Blocks { get; set; }
        public BaseBlockInfoStructure Base_Info { get; set; }
        private uint Last_Block = 0x00;
        #endregion

        #region Constructor

        public Bioshock2Class(EndianIO io)
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
            //Initialize our compressed block array
            Compressed_Blocks = new List<byte[]>();
            //Go to our table offset
            IO.In.BaseStream.Position = 0x40;
            int count = IO.In.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                //Go to our position
                IO.In.BaseStream.Position = 0x44 + (i * 4);
                //Read our pointer
                uint pointer = IO.In.ReadUInt32();
                //Go to our pointer
                IO.In.BaseStream.Position = pointer;
                //Read our size
                uint size = IO.In.ReadUInt32();
                //Read our value
                Last_Block = IO.In.ReadUInt32();
                //Read our data
                Compressed_Blocks.Add(IO.In.ReadBytes(size));
            }
            //Set our base info
            Base_Info = new BaseBlockInfoStructure(Ionic.Zlib.ZlibStream.UncompressBuffer(Compressed_Blocks[0]));
        }

        public void Write()
        {
            //Set our first compressed block
            Base_Info.Write();
            Compressed_Blocks[0] = Ionic.Zlib.ZlibStream.CompressBuffer(Base_Info.ToArray());
             
            //Go to our count offset
            IO.Out.BaseStream.Position = 0x40;
            //Write our count
            IO.Out.Write((uint)Compressed_Blocks.Count);
            //Calculate our values
            int firstOffset = 0x44 + (Compressed_Blocks.Count * 4);
            int priorDataSize = 0;
            //Loop for each item
            for (int i = 0; i < Compressed_Blocks.Count; i++)
            {
                //Calculate our offset
                int offset = firstOffset + priorDataSize + (i * 8);
                //Add to our prior data Size
                priorDataSize += Compressed_Blocks[i].Length;
                //Write our offset
                IO.Out.Write((uint)offset);
            }
            //Loop again for each item
            for (int i = 0; i < Compressed_Blocks.Count; i++)
            {
                //Write our size
                IO.Out.Write((uint)Compressed_Blocks[i].Length);
                //Write our constant.
                if (i != Compressed_Blocks.Count - 1)
                    IO.Out.Write((uint)0x00008000);
                else
                    IO.Out.Write(Last_Block);
                //Write our data
                IO.Out.Write(Compressed_Blocks[i]);
            }
            //Set our stream length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }

        #endregion
        #region Classes
        public class BaseBlockInfoStructure
        {
            public string LevelName { get; set; }
            private byte[] Data { get; set; }
            private int CreditsOffset = -1;
            public uint Credits { get; set; }
            public BaseBlockInfoStructure(byte[] data)
            {
                //Set our data
                Data = data;
                //Read
                Read();
            }
            public void Read()
            {
                //Create our IO
                EndianIO IO = new EndianIO(Data, EndianType.BigEndian);
                //Open our IO.
                IO.Open();
                //Search for our data
                CreditsOffset = FindCredits();
                //Go to our credits offset
                IO.In.BaseStream.Position = CreditsOffset;
                Credits = IO.In.ReadUInt32();
                //Close our IO
                IO.Close();
            }
            private int FindCredits()
            {
                //Create our IO
                EndianIO IO = new EndianIO(Data, EndianType.BigEndian);
                //Open our IO.
                IO.Open();


                //Loop for our count
                for(int i = 0x20; i < IO.In.BaseStream.Length - 0x20; i++)
                {
                    //Go to our position
                    IO.In.BaseStream.Position = i;
                    //If our int is 0x0000003A
                    if (IO.In.ReadUInt16() == 0x000)
                    {
                        if (IO.In.ReadUInt32() == 0x00002440)
                        {
                            IO.In.BaseStream.Position -= 0x1E;
                            if (IO.In.ReadUInt32() == 0x3A)
                            {
                                IO.In.BaseStream.Position += 0x1E;
                                int startS = (int)IO.In.BaseStream.Position;
                                for (int x = startS; x < startS + 32; x++)
                                {
                                    IO.In.BaseStream.Position = x;
                                    //If our read int is 0x22
                                    if (IO.In.ReadUInt32() == 0x00000022)
                                    {
                                        //Close our IO
                                        IO.Close();
                                        //Set our credits offset
                                        return x + 4;
                                    }
                                }
                            }
                        }
                    }
                }

                //Close our IO
                IO.Close();
                //We didn't find anything.
                throw new Exception("Could not find credits offset.");
                //return -1;
            }
            public void Write()
            {
                //Create our IO
                EndianIO IO = new EndianIO(Data, EndianType.BigEndian);
                //Open our IO.
                IO.Open();
                //Go to our credits offset
                IO.Out.BaseStream.Position = CreditsOffset;
                IO.Out.Write(Credits);
                //Close our IO
                IO.Close();
            }
            public byte[] ToArray() { return Data; }
        }
        #endregion
    }
}
