using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Bionic_Commando
{
    public class BionicCommandoClass
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values
        public BionicEntry Unknown_Value { get; set; }
        private const uint VALUE_CONST = 0xE0FF5CB4;
        private const uint END_CONST = 0x266B84F2;
        public List<BionicEntry> Values { get; set; }
        #endregion

        #region Constructor

        public BionicCommandoClass(EndianIO io)
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
            //Initialize our values
            Values = new List<BionicEntry>();

            //Go to our position
            IO.In.BaseStream.Position = 0;
            //Read our unknown value.
            Unknown_Value = new BionicEntry(IO, false);
            //Loop.
            while (true)
            {
                //Read our value
                uint val = IO.In.ReadUInt32();
                //If it's our end value
                if (val == END_CONST)
                    //Stop
                    break;

                //If it's another value
                if (val == VALUE_CONST)
                {
                    //Add our new value
                    Values.Add(new BionicEntry(IO, true));
                }
                else
                {
                    //Its not our end or our value constant, just keep reading until we hit either or.
                    IO.In.BaseStream.Position -= 3;
                }
            }

        }

        public void Write()
        {
            //Go to the beginning
            IO.Out.BaseStream.Position = 0;
            //Write our unknown value
            Unknown_Value.Write(IO);
            //Loop for each value
            foreach (BionicEntry val in Values)
            {
                //Write our value constant
                IO.Out.Write(VALUE_CONST);
                //Write our value.
                val.Write(IO);
            }
            //Write our 4 bytes of 0s.
            IO.Out.Write((int)0x00000000);
            //Write our end constant
            IO.Out.Write(END_CONST);
            //Set our length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }

        #endregion

        #region Classes
        public class BionicEntry
        {
            public string Name { get; set; }
            private int Address { get; set; }
            private bool HasName { get; set; }
            public List<BionicValue> Values { get; set; }
            public BionicEntry(EndianIO IO, bool hasName)
            {
                //Set our hasname bool
                HasName = hasName;
                //Read
                Read(IO);
            }
            public void Read(EndianIO IO)
            {
                //Init our list
                Values = new List<BionicValue>();
                //Set our address
                Address = (int)IO.In.BaseStream.Position;
                //If it has a name
                if (HasName)
                {
                    //Read our name
                    Name = IO.In.ReadNullTerminatedString();
                }
                //Loop.
                while (IO.In.BaseStream.Position < IO.In.BaseStream.Length - 8)
                {
                    //Read our value
                    uint val = IO.In.ReadUInt32();
                    //Go back
                    IO.In.BaseStream.Position -= 4;
                    //While we don't hit another value or the ending
                    if (val == VALUE_CONST || val == END_CONST)
                    {
                        //Get out of the loop
                        break;
                    }
                    //Otherwise we have a value.. Read it
                    Values.Add(new BionicValue(IO));
                }
            }
            public void Write(EndianIO IO)
            {
                //If we have a name
                if (HasName)
                {
                    //Write our name
                    IO.Out.WriteAsciiString(Name, Name.Length + 1);
                }
                //Loop for each value
                foreach (BionicValue val in Values)
                {
                    //Write it
                    val.Write(IO);
                }
            }
        }
        public class BionicValue
        {
            public ValueType Type { get; set; }
            public string Value { get; set; }
            public BionicValue(EndianIO IO)
            {
                //Read using our io.
                Read(IO);
            }
            public void Read(EndianIO IO)
            {
                //Read our type
                Type = (ValueType)IO.In.ReadUInt32();
                //Determine how to read our value
                switch (Type)
                {
                    case ValueType.Boolean:
                        Value = IO.In.ReadByte().ToString();
                        break;
                    case ValueType.Float:
                        Value = IO.In.ReadSingle(EndianType.LittleEndian).ToString();
                        break;
                    case ValueType.Integer:
                        Value = IO.In.ReadInt32(EndianType.LittleEndian).ToString();
                        break;
                    case ValueType.Text:
                        Value = IO.In.ReadNullTerminatedString();
                        break;
                }
            }
            public void Write(EndianIO IO)
            {
                //Write our type
                IO.Out.Write((uint)Type);
                //Write our value
                switch (Type)
                {
                    case ValueType.Boolean:
                        IO.Out.Write(byte.Parse(Value));
                        break;
                    case ValueType.Float:
                        IO.Out.Write(float.Parse(Value), EndianType.LittleEndian);
                        break;
                    case ValueType.Integer:
                        IO.Out.Write(int.Parse(Value), EndianType.LittleEndian);
                        break;
                    case ValueType.Text:
                        IO.Out.WriteAsciiString(Value, Value.Length + 1);
                        break;
                }
            }
            public enum ValueType : uint
            {
                Boolean = 0x4FC6E284,
                Float = 0x8A24BCB1,
                Integer = 0xDEE1B9AA,
                Text = 0xE0FF5CB4
            }
        }
        #endregion
    }
}
