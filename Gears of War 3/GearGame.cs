using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnrealEngine;

namespace Horizon.PackageEditors.Gears_of_War_3
{
    internal class GearGame
    {
        internal Chapter CurrentChapter;
        internal Dictionary<string, ushort> Checkpoints;
        internal Dictionary<string, Struct> Structs;

        private int UnrealEngineBuild;
        internal UnrealData Data;

        internal GearGame(EndianIO IO)
        {
            UnrealEngineBuild = IO.In.ReadInt32();

            CurrentChapter = new Chapter(IO);

            // Read the checkpoint list
            int numCheckpoints = IO.In.ReadInt32();
            Checkpoints = new Dictionary<string, ushort>(numCheckpoints);

            for (int x = 0; x < numCheckpoints; x++)
                Checkpoints.Add(IO.In.ReadString(IO.In.ReadInt32()), IO.In.ReadUInt16());

            // Read all the structs into a list
            int numStructs = IO.In.ReadInt32();
            Structs = new Dictionary<string, Struct>(numStructs);

            for (int x = 0; x < numStructs; x++)
            {
                string structName = IO.In.ReadString(IO.In.ReadInt32());
                string structType = IO.In.ReadString(IO.In.ReadInt32());
                byte[] data = IO.In.ReadBytes(IO.In.ReadInt32());

                Struct gearStruct;
                switch (structType)
                {
                    case "GearGame.GearPC_SP":
                        gearStruct = new GearPC_SP();
                        break;
                    default:
                        gearStruct = (structType.Length > 0x10
                            && structType.Substring(0, 0x10) == "GearGame.GearAI_")
                            ? new GearAI() : new Struct();
                        break;
                }
                gearStruct.Type = structType;
                gearStruct.Data = data;
                gearStruct.Read();
                Structs.Add(structName, gearStruct);
            }

            Data = new UnrealData(IO.In.ReadBytes(IO.In.ReadInt32()));
        }

        internal class Chapter
        {
            internal string Name;
            internal float[] Float1 = new float[4];
            internal int[] Int1 = new int[4];
            internal int UnknownInt;
            internal byte UnknownByte;
            internal float[] Float2 = new float[3];

            internal Chapter(EndianIO IO)
            {
                Name = IO.In.ReadString(IO.In.ReadInt32());
                for (int x = 0; x < 4; x++)
                    Float1[x] = IO.In.ReadSingle();
                UnknownInt = IO.In.ReadInt32();
                UnknownByte = IO.In.ReadByte();
                for (int x = 0; x < 4; x++)
                    Int1[x] = IO.In.ReadInt32();
                for (int x = 0; x < 3; x++)
                    Float2[x] = IO.In.ReadSingle();
            }

            internal void Write(EndianIO IO)
            {
                int tempLength = Name.Length + 1;
                IO.Out.Write(tempLength);
                IO.Out.WriteAsciiString(Name, tempLength);

                for (int x = 0; x < 4; x++)
                    IO.Out.Write(Float1[x]);

                IO.Out.Write(UnknownInt);
                IO.Out.Write(UnknownByte);

                for (int x = 0; x < 4; x++)
                    IO.Out.Write(Int1[x]);

                for (int x = 0; x < 3; x++)
                    IO.Out.Write(Float2[x]);
            }
        }

        internal class GearPC_SP : Struct
        {
            private byte[] Header, Unknown1, Unknown2, Unknown3;
            internal string CurrentScene;
            internal string Model1, Model2, Model3, Model4;
            internal Coordinates Location;
            internal List<GearWeap> Weapons, Weapons2;
            internal List<Objective> Objectives;

            private string[] UnknownAttributes1 = new string[2];
            private string[] UnknownAttributes2 = new string[3];
            private ushort Unknown8;

            private byte[] Unknown6, Unknown7;
            private byte UnknownByte;
            private string UnknownString2, UnknownString3, UnknownString4;
            internal string[] Attributes1 = new string[3];
            internal List<Attribute> Attributes2;
            internal string[] Attributes3 = new string[3];

            internal string[] SpawnableItems = new string[4];

            protected internal override void Parse(EndianIO IO)
            {
                Header = IO.In.ReadBytes(0x10);
                CurrentScene = IO.In.ReadAsciiString(IO.In.ReadInt32());

                Unknown1 = IO.In.ReadBytes(0x08);
                Model1 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Model2 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Unknown2 = IO.In.ReadBytes(0x10);
                Model3 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Model4 = IO.In.ReadAsciiString(IO.In.ReadInt32());

                Location = new Coordinates()
                {
                    X = IO.In.ReadSingle(),
                    Z = IO.In.ReadSingle(),
                    Y = IO.In.ReadSingle()
                };

                Unknown3 = IO.In.ReadBytes(0x08);

                for (int x = 0; x < UnknownAttributes1.Length; x++)
                    UnknownAttributes1[x] = IO.In.ReadAsciiString(IO.In.ReadInt32());

                Unknown8 = IO.In.ReadUInt16();

                for (int x = 0; x < UnknownAttributes2.Length; x++)
                    UnknownAttributes2[x] = IO.In.ReadAsciiString(IO.In.ReadInt32());

                int numTemp = IO.In.ReadInt32();
                Weapons = new List<GearWeap>(numTemp);
                for (int x = 0; x < numTemp; x++)
                    Weapons.Add(new GearWeap(IO));

                numTemp = IO.In.ReadInt32();
                Weapons2 = new List<GearWeap>(numTemp);
                for (int x = 0; x < numTemp; x++)
                    Weapons2.Add(new GearWeap(IO));

                numTemp = IO.In.ReadInt32();
                Objectives = new List<Objective>(numTemp);
                for (int x = 0; x < numTemp; x++)
                    Objectives.Add(new Objective(IO));

                UnknownString4 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                UnknownByte = IO.In.ReadByte();
                UnknownString2 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Unknown6 = IO.In.ReadBytes(0x16);

                for (int x = 0; x < Attributes1.Length; x++)
                    Attributes1[x] = IO.In.ReadAsciiString(IO.In.ReadInt32());

                numTemp = IO.In.ReadInt32();
                Attributes2 = new List<Attribute>(numTemp);
                for (int x = 0; x < numTemp; x++)
                    Attributes2.Add(new Attribute(IO));

                for (int x = 0; x < Attributes3.Length; x++)
                    Attributes3[x] = IO.In.ReadAsciiString(IO.In.ReadInt32());

                Unknown7 = IO.In.ReadBytes(0x2C);

                for (int x = 0; x < SpawnableItems.Length; x++)
                    SpawnableItems[x] = IO.In.ReadAsciiString(IO.In.ReadInt32());

                UnknownString3 = IO.In.ReadAsciiString(IO.In.ReadInt32());
            }

            internal protected override void Write(EndianIO IO)
            {
                IO.Out.Write(Header);

                WriteString(IO, CurrentScene);

                IO.Out.Write(Unknown1);

                WriteString(IO, Model1);
                WriteString(IO, Model2);

                IO.Out.Write(Unknown2);

                WriteString(IO, Model3);
                WriteString(IO, Model4);

                IO.Out.Write(Location.X);
                IO.Out.Write(Location.Z);
                IO.Out.Write(Location.Y);

                IO.Out.Write(Unknown3);

                for (int x = 0; x < UnknownAttributes1.Length; x++)
                    WriteString(IO, UnknownAttributes1[x]);

                IO.Out.Write(Unknown8);

                for (int x = 0; x < UnknownAttributes2.Length; x++)
                    WriteString(IO, UnknownAttributes2[x]);

                IO.Out.Write(Weapons.Count);
                for (int x = 0; x < Weapons.Count; x++)
                    Weapons[x].Write(IO);

                IO.Out.Write(Weapons2.Count);
                for (int x = 0; x < Weapons2.Count; x++)
                    Weapons2[x].Write(IO);

                IO.Out.Write(Objectives.Count);
                for (int x = 0; x < Objectives.Count; x++)
                    Objectives[x].Write(IO);

                WriteString(IO, UnknownString4);
                IO.Out.Write(UnknownByte);
                WriteString(IO, UnknownString2);
                IO.Out.Write(Unknown6);

                for (int x = 0; x < Attributes1.Length; x++)
                    WriteString(IO, Attributes1[x]);

                IO.Out.Write(Attributes2.Count);
                for (int x = 0; x < Attributes2.Count; x++)
                    Attributes2[x].Write(IO);;

                for (int x = 0; x < Attributes3.Length; x++)
                    WriteString(IO, Attributes3[x]);

                IO.Out.Write(Unknown7);

                for (int x = 0; x < SpawnableItems.Length; x++)
                    WriteString(IO, SpawnableItems[x]);

                WriteString(IO, UnknownString3);
            }
        }

        internal class Attribute
        {
            internal string Name;
            internal byte[] Data;

            internal Attribute(EndianIO IO)
            {
                Name = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Data = IO.In.ReadBytes(0x11);
            }

            internal void Write(EndianIO IO)
            {
                WriteString(IO, Name);
                IO.Out.Write(Data);
            }
        }

        internal class Objective
        {
            private int Unknown;
            internal string Position;
            internal string Name;
            internal byte[] Data;
            internal string[] Waypoints = new string[10];

            internal Objective(EndianIO IO)
            {
                Position = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Unknown = IO.In.ReadInt32();
                Name = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Data = IO.In.ReadBytes(0x0E);

                for (int x = 0; x < Waypoints.Length; x++)
                    Waypoints[x] = IO.In.ReadAsciiString(IO.In.ReadInt32());
            }

            internal void Write(EndianIO IO)
            {
                WriteString(IO, Position);
                IO.Out.Write(Unknown);

                WriteString(IO, Name);

                IO.Out.Write(Data);

                for (int x = 0; x < Waypoints.Length; x++)
                    WriteString(IO, Waypoints[x]);
            }
        }

        internal enum WeaponSlot : byte
        {
            Bottom = 0x00,
            Top = 0x01,
            Left = 0x02,
            Right = 0x03
        }

        internal class GearWeap
        {
            internal string Name;
            internal int Ammo;
            internal int ClipAmmo;
            internal WeaponSlot Slot;
            internal bool Equipped;

            internal GearWeap(WeaponSlot slot)
            {
                Slot = slot;
            }

            internal GearWeap(EndianIO IO)
            {
                Name = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Ammo = IO.In.ReadInt32();
                ClipAmmo = IO.In.ReadInt32();
                Slot = (WeaponSlot)IO.In.ReadByte();
                Equipped = IO.In.ReadBoolean();
            }

            internal void Write(EndianIO IO)
            {
                WriteString(IO, Name);
                IO.Out.Write(Ammo);
                IO.Out.Write(ClipAmmo);
                IO.Out.Write((byte)Slot);
                IO.Out.Write(Equipped);
            }
        }

        internal struct Coordinates
        {
            internal float X;
            internal float Y;
            internal float Z;
        }

        private static void WriteString(EndianIO IO, string str)
        {
            if (str == null || str.Length == 0)
                IO.Out.Write(0);
            else
            {
                int tempLength = str.Length + 1;
                IO.Out.Write(tempLength);
                IO.Out.WriteAsciiString(str, tempLength);
            }
        }

        internal class GearAI : Struct
        {
            private byte[] Header, Footer, Footer2, Unknown2;
            private ushort Unknown3;
            private int Unknown5;
            private string UnknownString, UnknownString2, UnknownString3;
            internal string Model, Model2, Model3, Model4;
            private byte Unknown4;
            internal Coordinates Location;
            internal List<GearWeap> Weapons;
            internal string Name;

            protected internal override void Parse(EndianIO IO)
            {
                Header = IO.In.ReadBytes(0x14);
                Model = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Model2 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Model3 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Model4 = IO.In.ReadAsciiString(IO.In.ReadInt32());

                Location = new Coordinates()
                {
                    X = IO.In.ReadSingle(),
                    Z = IO.In.ReadSingle(),
                    Y = IO.In.ReadSingle()
                };

                Unknown2 = IO.In.ReadBytes(0x0C);
                UnknownString = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Unknown4 = IO.In.ReadByte();
                UnknownString2 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Unknown3 = IO.In.ReadUInt16();
                UnknownString3 = IO.In.ReadAsciiString(IO.In.ReadInt32());
                Unknown5 = IO.In.ReadInt32();

                int numTemp = IO.In.ReadInt32();
                Weapons = new List<GearWeap>(numTemp);
                for (int x = 0; x < numTemp; x++)
                    Weapons.Add(new GearWeap(IO));

                long currentPosition = IO.Stream.Position;

                IO.Stream.Position = IO.Stream.Length - 0x1A;
                while (IO.In.ReadByte() != 0x00)
                    IO.Stream.Position -= 2;
                long namePosition = IO.Stream.Position - 3;

                IO.Stream.Position = currentPosition;
                Footer = IO.In.ReadBytes(namePosition - currentPosition);

                Name = IO.In.ReadAsciiString(IO.In.ReadInt32());

                Footer2 = IO.In.ReadBytes(IO.Stream.Length - IO.Stream.Position);
            }

            internal protected override void Write(EndianIO IO)
            {
                IO.Out.Write(Header);

                WriteString(IO, Model);
                WriteString(IO, Model2);
                WriteString(IO, Model3);
                WriteString(IO, Model4);

                IO.Out.Write(Location.X);
                IO.Out.Write(Location.Z);
                IO.Out.Write(Location.Y);
                IO.Out.Write(Unknown2);

                WriteString(IO, UnknownString);
                IO.Out.Write(Unknown4);
                WriteString(IO, UnknownString2);
                IO.Out.Write(Unknown3);
                WriteString(IO, UnknownString3);
                IO.Out.Write(Unknown5);

                IO.Out.Write(Weapons.Count);
                for (int x = 0; x < Weapons.Count; x++)
                    Weapons[x].Write(IO);

                IO.Out.Write(Footer);

                WriteString(IO, Name);

                IO.Out.Write(Footer2);
            }
        }

        internal class Struct
        {
            internal string Type;
            internal byte[] Data;

            internal void Read()
            {
                EndianIO IO = new EndianIO(Data, EndianType.BigEndian, true);
                Parse(IO);
                IO.Close();
            }

            internal protected virtual void Parse(EndianIO IO)
            {

            }

            internal byte[] ToArray()
            {
                EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
                Write(IO);
                Data = IO.ToArray();
                IO.Close();
                return Data;
            }

            internal protected virtual void Write(EndianIO IO)
            {
                IO.Out.Write(Data);
            }
        }

        internal List<GearPC_SP> GetPlayableCharacterStructs()
        {
            List<GearPC_SP> pc = new List<GearPC_SP>();
            foreach (KeyValuePair<string, Struct> s in Structs)
                if (s.Value is GearPC_SP)
                    pc.Add((GearPC_SP)s.Value);
            return pc;
        }

        internal List<GearAI> GetAIStructs()
        {
            List<GearAI> ai = new List<GearAI>();
            foreach (KeyValuePair<string, Struct> s in Structs)
                if (s.Value is GearAI)
                    ai.Add((GearAI)s.Value);
            return ai;
        }

        internal byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            IO.Out.Write(UnrealEngineBuild);

            CurrentChapter.Write(IO);

            int tempLength;
            IO.Out.Write(Checkpoints.Count);
            foreach (KeyValuePair<string, ushort> c in Checkpoints)
            {
                tempLength = c.Key.Length + 1;
                IO.Out.Write(tempLength);
                IO.Out.WriteAsciiString(c.Key, tempLength);
                IO.Out.Write(c.Value);
            }

            IO.Out.Write(Structs.Count);
            foreach (KeyValuePair<string, Struct> s in Structs)
            {
                tempLength = s.Key.Length + 1;
                IO.Out.Write(tempLength);
                IO.Out.WriteAsciiString(s.Key, tempLength);
                tempLength = s.Value.Type.Length + 1;
                IO.Out.Write(tempLength);
                IO.Out.WriteAsciiString(s.Value.Type, tempLength);
                byte[] st = s.Value.ToArray();
                IO.Out.Write(st.Length);
                IO.Out.Write(st);
            }

            byte[] stateData = Data.ToArray();

            IO.Out.Write(stateData.Length);
            IO.Out.Write(stateData);

            byte[] ret = IO.ToArray();
            IO.Close();

            return ret;
        }
    }
}
