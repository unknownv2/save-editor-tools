using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Darksiders
{
    public class DarksidersClass
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values

        private static uint DSAV_MAGIC = 0x44534156;
        private uint Version { get; set; }
        public SaveStruct Save_Structure { get; set; }
        private byte[] Other_Data { get; set; }
        #endregion
    
        #region Constructor

        public DarksidersClass(EndianIO io)
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
            //Read our magic
            if (IO.In.ReadUInt32() != DSAV_MAGIC)
                throw new Exception("Header magic mismatch. This may not be a valid Darksiders save.");
            //Read our values
            IO.In.ReadByte();
            Version = IO.In.ReadUInt32();
            uint FileSize = IO.In.ReadUInt32(EndianType.LittleEndian);
            //Go to our compressed data offset
            IO.In.BaseStream.Position = 0x35;

            //Read our data
            byte[] Decompressed_Data = Ionic.Zlib.ZlibStream.UncompressBuffer(IO.In.ReadBytes(FileSize));
            Other_Data = IO.In.ReadBytes(IO.In.BaseStream.Length - IO.In.BaseStream.Position);
            //Read our struct.
            Save_Structure = new SaveStruct(Decompressed_Data);
        }

        public void Write()
        {
            //Get our data
            byte[] data = Ionic.Zlib.ZlibStream.CompressBuffer(Save_Structure.ToArray());
            //Go to our checksum offset
            IO.Out.BaseStream.Position = 0x09;
            //Write our filesize.
            uint FileSize = (uint)data.Length;
            IO.Out.Write(FileSize, EndianType.LittleEndian);
            //Go to our data offset
            IO.Out.BaseStream.Position = 0x35;
            //Write our data.
            IO.Out.Write(data);
            IO.Out.Write(Other_Data);

            //Set our length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }
        #endregion
        #region Classes
        public class SaveStruct
        {
            
            private byte[] Data { get; set; }
            public byte[] ToArray() { return Data; }
            public SaveStruct(byte[] data)
            {
                Data = data;
            }
            public void Read(EndianIO IO)
            {
            }
            public void Write(EndianIO IO)
            {
            }
        }
        public class DarksidersValue
        {
            /// <summary>
            /// Our 9 bytes of unknown prior to our value name length
            /// </summary>
            public byte[] Unknown { get; set; }
            public string Name { get; set; }
            public DarksidersValueType Type { get; set; }
            public object Value { get; set; }
        }
        public enum DarksidersValueType
        {
            UINT32 = 0x02,
            FLOAT = 0x03,
            BOOL = 0x04,
            FIVE_BYTES = 0x07,
            BYTE_ARRAY = 0x09,
            STRING = 0x0F,
            THREE_BYTES = 0x12 //uhh?
        }
        #endregion
    }
}
