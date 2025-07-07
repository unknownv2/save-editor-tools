using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Borderlands
{
    public class BorderlandsClass
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values
        private const string MAGIC = "WSG";
        private const uint Version = 0x00000002;
        public PlayerStructure Player_Struct { get; set; }
        #endregion
    
        #region Constructor

        public BorderlandsClass(EndianIO io)
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
            //Read our magic
            if (IO.In.ReadAsciiString(3) != MAGIC)
                throw new Exception("Header file magic mismatch. This may be an invalid save.");
            //Read our version
            IO.In.ReadUInt32();
            //Read our player structure
            Player_Struct = new PlayerStructure(IO);
           
        }

        public void Write()
        {
            //Write our magic
            IO.Out.BaseStream.Position = 0;
            IO.Out.WriteAsciiString(MAGIC, MAGIC.Length);
            //Skip our version
            IO.Out.BaseStream.Position += 4;
            //Save our player structure
            Player_Struct.Write(IO);
        }


        #endregion

        #region Classes
        /// <summary>
        /// This structure contains our player class.. and enumerator?
        /// </summary>
        public class PlayerStructure
        {
            /// <summary>
            /// Our magic for our player structure. Read "PLYR".
            /// </summary>
            private const uint MAGIC = 0x504C5952;
            public uint Unknown0 { get; set; }
            /// <summary>
            /// Our internal character class name.
            /// </summary>
            public string Character_Class { get; set; }
            public uint Level { get; set; }
            public uint XP_Points { get; set; }
            public uint Skill_Points { get; set; }
            public uint Unknown4 { get; set; }
            public uint Money { get; set; }
            public uint Unknown5 { get; set; }
            public List<KeyValue> KeyValues { get; set; }
            public PlayerStructure(EndianIO IO)
            {
                //Read our data
                Read(IO);
            }

            public void Read(EndianIO IO)
            {
                //Initialize our values
                KeyValues = new List<KeyValue>();

                //Read our magic
                if (IO.In.ReadUInt32() != MAGIC)
                    throw new Exception("Player Data magic mismatch. Player data may not exist, or it may be shifted.");
                //Read our unknown
                Unknown0 = IO.In.ReadUInt32();
                //Read our character class
                Character_Class = IO.In.ReadAsciiString((int)IO.In.ReadUInt32());
                //Read our level
                Level = IO.In.ReadUInt32();
                //Read our XP points
                XP_Points = IO.In.ReadUInt32();
                //Read our Skill Points
                Skill_Points = IO.In.ReadUInt32();
                //Read our unknown
                Unknown4 = IO.In.ReadUInt32();
                //Read our money
                Money = IO.In.ReadUInt32();
                //Read our unknown
                Unknown5 = IO.In.ReadUInt32();
                //Read our KeyValue count
                uint keyValueCount = IO.In.ReadUInt32();
                //Loop for each key value.
                for (int i = 0; i < keyValueCount; i++)
                {
                    //Add our key value
                    KeyValues.Add(new KeyValue(IO));
                }
            }

            public void Write(EndianIO IO)
            {
                //Write our magic
                IO.Out.Write(MAGIC);
                //Write our unknown
                IO.Out.Write(Unknown0);
                //Write our string length
                IO.Out.Write((uint)Character_Class.Length + 1);
                //Write our string
                IO.Out.WriteAsciiString(Character_Class, Character_Class.Length + 1);
                //Write our level
                IO.Out.Write(Level);
                //Write our XP Points
                IO.Out.Write(XP_Points);
                //Write our skill points
                IO.Out.Write(Skill_Points);
                //Write our unknown
                IO.Out.Write(Unknown4);
                //Write our money
                IO.Out.Write(Money);
                //Write our unknown
                IO.Out.Write(Unknown5);
                //Write our count of KeyValues
                IO.Out.Write((uint)KeyValues.Count);
                //Loop for each keyvalue
                for (int i = 0; i < KeyValues.Count; i++)
                    //Write our keyvalue
                    KeyValues[i].Write(IO);
            }

            public void SetKeyValue(string key, uint value)
            {
                //Set our key
                foreach (KeyValue KV in KeyValues)
                    //If this is our key
                    if (KV.Key == key)
                        //Set our value
                        KV.Value = value;
            }

            /// <summary>
            /// This class is used to store advanced values.
            /// </summary>
            public class KeyValue
            {
                public string Key { get; set; }
                public uint Value { get; set; }
                public uint Unknown0 { get; set; }
                public uint Unknown1 { get; set; }

                public KeyValue(EndianIO IO)
                {
                    //Read our keyvalue
                    Read(IO);
                }

                public void Read(EndianIO IO)
                {
                    //Read our key
                    Key = IO.In.ReadAsciiString((int)IO.In.ReadUInt32());
                    //Read our value
                    Value = IO.In.ReadUInt32();
                    //Read our unknowns
                    Unknown0 = IO.In.ReadUInt32();
                    Unknown1 = IO.In.ReadUInt32();
                }

                public void Write(EndianIO IO)
                {
                    //Write our key length
                    IO.Out.Write((uint)Key.Length + 1);
                    //Write our key
                    IO.Out.WriteAsciiString(Key, Key.Length + 1);
                    //Write our value
                    IO.Out.Write(Value);
                    //Write our unknowns
                    IO.Out.Write(Unknown0);
                    IO.Out.Write(Unknown1);
                }
            }
        }
        #endregion
    }
}
