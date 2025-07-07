using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Oblivion
{
    class OblivionSave
    {
        public class SaveFileHeader
        {
            public string Magic;
            public byte MajorVersion, MinorVersion;
            public byte[] SaveTimeStamp;

            public void Read(EndianIO io)
            {
                Magic = io.In.ReadAsciiString(12);
                MajorVersion = io.In.ReadByte();
                MinorVersion = io.In.ReadByte();
                SaveTimeStamp = io.In.ReadBytes(16);
            }

            public void Write(EndianIO io)
            {
                io.Out.WriteAsciiString(Magic, 12);
                io.Out.Write(MajorVersion);
                io.Out.Write(MinorVersion);
                io.Out.Write(SaveTimeStamp);
            }
        }

        public class SaveGameHeader
        {
            public byte[] GameTime, Thumbnail;
            public short Level;
            public uint HeaderVersion, HeaderSize, SaveNumber, GameTicks;
            public int ThumbnailWidth, ThumbnailHeight;
            public string CharacterName, Location;
            public float GameDays;
            public string[] Plugins;

            public void Read(EndianIO io)
            {
                HeaderVersion = io.In.ReadUInt32();
                HeaderSize = io.In.ReadUInt32();
                SaveNumber = io.In.ReadUInt32();
                CharacterName = io.In.ReadAsciiString(io.In.ReadByte());
                Level = io.In.ReadInt16();
                Location = io.In.ReadAsciiString(io.In.ReadByte());
                GameDays = io.In.ReadSingle();
                GameTicks = io.In.ReadUInt32();
                GameTime = io.In.ReadBytes(16);
                int ThumbnailSize = io.In.ReadInt32();
                ThumbnailWidth = io.In.ReadInt32();
                ThumbnailHeight = io.In.ReadInt32();
                Thumbnail = io.In.ReadBytes(ThumbnailSize - 8);
                Plugins = new string[io.In.ReadByte()];
                for (int x = 0; x < Plugins.Length; x++)
                    Plugins[x] = io.In.ReadAsciiString(io.In.ReadByte());
            }

            public void Write(EndianIO io)
            {
                io.Out.Write(HeaderVersion);
                io.Out.Write(HeaderSize);
                io.Out.Write(SaveNumber);
                io.Out.Write((byte)(CharacterName.Length + 1));
                io.Out.WriteAsciiString(CharacterName, CharacterName.Length);
                io.Out.Write((byte)0x00);
                io.Out.Write(Level);
                io.Out.Write((byte)(Location.Length + 1));
                io.Out.WriteAsciiString(Location, Location.Length);
                io.Out.Write((byte)0x00);
                io.Out.Write(GameDays);
                io.Out.Write(GameTicks);
                io.Out.Write(GameTime);
                io.Out.Write(Thumbnail.Length + 8);
                io.Out.Write(ThumbnailWidth);
                io.Out.Write(ThumbnailHeight);
                io.Out.Write(Thumbnail);
                HeaderSize = (uint)(io.Stream.Position - 38);
                io.Out.Write((byte)Plugins.Length);
                foreach (string s in Plugins)
                {
                    io.Out.Write((byte)s.Length);
                    io.Out.Write(s);
                }
            }
        }

        public class SaveGlobals
        {
            public byte[] TESClassData, ProcessesData, SpecEventData, WeatherData, QuickKeyData, ReticuleData, InterfaceData;
            public uint NumberOfRecords, FormIDOffset, NextObjectID, WorldID, WorldX, WorldY, Unknown;
            public PCPosition PlayerPosition;
            public Region[] Regions;
            public GlobalVariable[] GlobalVariables;
            public CustomRecord[] CustomRecords;

            public struct PCPosition
            {
                public uint FormIDOfCell;
                public float PlayerX;
                public float PlayerY;
                public float PlayerZ;
            }

            public struct GlobalVariable
            {
                public uint IRefOfGlobal;
                public float Value;
            }

            public struct Region
            {
                public uint IRef;
                public uint Flags;
            }

            public class CustomRecord
            {
                public byte[] Data;
                public RecordType Type;
                public uint Flag1, Flag2, FormID;
                public Weapon WEAP;
                public Armor ARMO;
                public Clothing CLOT;
                public Enchantment ENCH;

                public void Read(EndianIO io)
                {
                    Type = (RecordType)io.In.ReadUInt32();
                    uint DataSize = io.In.ReadUInt32();
                    Flag1 = io.In.ReadUInt32();
                    FormID = io.In.ReadUInt32();
                    Flag2 = io.In.ReadUInt32();
                    switch (Type)
                    {
                        case RecordType.Armor:
                            ARMO = new Armor();
                            ARMO.Read(io, DataSize);
                            break;
                        case RecordType.Weapon:
                            WEAP = new Weapon();
                            WEAP.Read(io, DataSize);
                            break;
                        case RecordType.Enchantment:
                            ENCH = new Enchantment();
                            ENCH.Read(io, DataSize);
                            break;
                        case RecordType.Clothing:
                            CLOT = new Clothing();
                            CLOT.Read(io, DataSize);
                            break;
                        default:
                            Data = io.In.ReadBytes((int)DataSize);
                            break;
                    }
                }

                public void Write(EndianIO io)
                {
                    io.Out.Write((uint)Type);
                    switch (Type)
                    {
                        case RecordType.Armor:
                            ARMO.Write(ref Data);
                            break;
                        case RecordType.Weapon:
                            WEAP.Write(ref Data);
                            break;
                        case RecordType.Enchantment:
                            ENCH.Write(ref Data);
                            break;
                        case RecordType.Clothing:
                            CLOT.Write(ref Data);
                            break;
                    }  
                    io.Out.Write(Data.Length);
                    io.Out.Write(Flag1);
                    io.Out.Write(FormID);
                    io.Out.Write(Flag2);
                    io.Out.Write(Data);
                }

                public enum RecordType : uint
                {
                    Armor = 0x4F4D5241,
                    Clothing = 0x544F4C43,
                    Enchantment = 0x48434E45,
                    Weapon = 0x50414557
                }

                public enum RecordBMDT : uint
                {
                    Head = 1,
                    Hair = 2,
                    Upper_Body = 4,
                    Lower_Body = 8,
                    Hand = 16,
                    Foot = 32,
                    Right_Ring = 64,
                    Left_Ring = 128,
                    Amulet = 256,
                    Weapon = 512,
                    Back_Weapon = 1024,
                    Side_Weapon = 2048,
                    Quiver = 4096,
                    Shield = 8192,
                    Torch = 16384,
                    Tail = 32768,
                    Hide_Rings = 65536,
                    Hide_Amulets = 131072,
                    Non_Playable = 4194304,
                    Heavy_Armor = 8388608,
                    Default = 3439329280,
                }

                public class Clothing
                {
                    public string EDID, FULL, MODL, MOD2, MOD3, MOD4, ICON, ICO2;
                    public byte[] MODB, MO2B, MO3B, MO4B, MODT, MO2T, MO3T, MO4T;
                    public ushort? ANAM;
                    public uint? SCRI, ENAM;
                    public RecordBMDT? BMDT;
                    public ArmorDATA DATA;

                    public struct ArmorDATA
                    {
                        public uint Value;
                        public float? Weight;
                    }

                    public enum SubRecordType : uint
                    {
                        EDID = 0x44494445,
                        FULL = 0x4C4C5546,
                        SCRI = 0x49524353,
                        BMDT = 0x54444D42,
                        MODL = 0x4C444F4D,
                        MOD2 = 0x32444F4D,
                        MOD3 = 0x33444F4D,
                        MOD4 = 0x34444F4D,
                        MODB = 0x42444F4D,
                        MO2B = 0x42324F4D,
                        MO3B = 0x42334F4D,
                        MO4B = 0x42344F4D,
                        MODT = 0x54444F4D,
                        MO2T = 0x54324F4D,
                        MO3T = 0x54334F4D,
                        MO4T = 0x54344F4D,
                        ICON = 0x4E4F4349,
                        ICO2 = 0x324F4349,
                        ANAM = 0x4D414E41,
                        ENAM = 0x4D414E45,
                        DATA = 0x41544144
                    }

                    public void Read(EndianIO io, uint DataSize)
                    {
                        long EndOffset = io.Stream.Position + DataSize;
                        while (io.Stream.Position < EndOffset)
                            switch ((SubRecordType)io.In.ReadUInt32())
                            {
                                case SubRecordType.EDID:
                                    EDID = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.FULL:
                                    FULL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.SCRI:
                                    io.Stream.Position += 2;
                                    SCRI = io.In.ReadUInt32();
                                    break;
                                case SubRecordType.BMDT:
                                    io.Stream.Position += 2;
                                    BMDT = (RecordBMDT)io.In.ReadUInt32();
                                    break;
                                case SubRecordType.MODL:
                                    MODL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MOD2:
                                    MOD2 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MOD3:
                                    MOD3 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MOD4:
                                    MOD4 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MODB:
                                    MODB = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO2B:
                                    MO2B = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO3B:
                                    MO3B = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO4B:
                                    MO4B = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MODT:
                                    MODT = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO2T:
                                    MO2T = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO3T:
                                    MO3T = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO4T:
                                    MO4T = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.ICON:
                                    ICON = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.ICO2:
                                    ICO2 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.ENAM:
                                    io.Stream.Position += 2;
                                    ENAM = io.In.ReadUInt32();
                                    break;
                                case SubRecordType.ANAM:
                                    io.Stream.Position += 2;
                                    ANAM = io.In.ReadUInt16();
                                    break;
                                case SubRecordType.DATA:
                                    io.Stream.Position += 2;
                                    DATA.Value = io.In.ReadUInt32();
                                    DATA.Weight = io.In.ReadSingle();
                                    break;
                                default:
                                    io.Stream.Position -= 4;
                                    break;
                            }
                    }

                    public void Write(ref byte[] Data)
                    {
                        MemoryStream MS = new MemoryStream();
                        EndianIO io = new EndianIO(MS, EndianType.LittleEndian);
                        io.Open();
                        if (EDID != null)
                        {
                            io.Out.Write((uint)SubRecordType.EDID);
                            WriteString(io, EDID);
                        }
                        if (FULL != null)
                        {
                            io.Out.Write((uint)SubRecordType.FULL);
                            WriteString(io, FULL);
                        }
                        if (SCRI != null)
                        {
                            io.Out.Write((uint)SubRecordType.SCRI);
                            io.Out.Write((ushort)4);
                            io.Out.Write(SCRI.Value);
                        }
                        if (ENAM.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.ENAM);
                            io.Out.Write((ushort)4);
                            io.Out.Write(ENAM.Value);
                        }
                        if (ANAM.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.ANAM);
                            io.Out.Write((ushort)2);
                            io.Out.Write(ANAM.Value);
                        }
                        if (BMDT != null)
                        {
                            io.Out.Write((uint)SubRecordType.BMDT);
                            io.Out.Write((ushort)4);
                            io.Out.Write((uint)BMDT);
                        }
                        if (MODL != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODL);
                            WriteString(io, MODL);
                        }
                        if (MODB != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODB);
                            WriteBytes(io, MODB);
                        }
                        if (MO2B != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO2B);
                            WriteBytes(io, MO2B);
                        }
                        if (MO3B != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO3B);
                            WriteBytes(io, MO3B);
                        }
                        if (MO4B != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO4B);
                            WriteBytes(io, MO4B);
                        }
                        if (MOD2 != null)
                        {
                            io.Out.Write((uint)SubRecordType.MOD2);
                            WriteString(io, MOD2);
                        }
                        if (MOD3 != null)
                        {
                            io.Out.Write((uint)SubRecordType.MOD3);
                            WriteString(io, MOD3);
                        }
                        if (MOD4 != null)
                        {
                            io.Out.Write((uint)SubRecordType.MOD4);
                            WriteString(io, MOD4);
                        }
                        if (MODT != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODT);
                            WriteBytes(io, MODT);
                        }
                        if (MO2T != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO2T);
                            WriteBytes(io, MO2T);
                        }
                        if (MO3T != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO3T);
                            WriteBytes(io, MO3T);
                        }
                        if (MO4T != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO4T);
                            WriteBytes(io, MO4T);
                        }
                        if (ICON != null)
                        {
                            io.Out.Write((uint)SubRecordType.ICON);
                            WriteString(io, ICON);
                        }
                        if (ICO2 != null)
                        {
                            io.Out.Write((uint)SubRecordType.ICO2);
                            WriteString(io, ICO2);
                        }
                        if (DATA.Weight.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.DATA);
                            io.Out.Write((ushort)8);
                            io.Out.Write(DATA.Value);
                            io.Out.Write(DATA.Weight.Value);
                        }
                        io.Stream.Position = 0;
                        Data = io.In.ReadBytes((int)io.Stream.Length);
                        io.Close();
                    }

                    public void WriteString(EndianIO io, string Input)
                    {
                        io.Out.Write((ushort)(Input.Length + 1));
                        io.Out.WriteAsciiString(Input, Input.Length);
                        io.Out.Write((byte)0x00);
                    }

                    public void WriteBytes(EndianIO io, byte[] Input)
                    {
                        io.Out.Write((ushort)Input.Length);
                        io.Out.Write(Input);
                    }
                }

                public class Enchantment
                {
                    public string EDID;
                    public string FULL;
                    public EnchantmentENIT ENIT;
                    public EnchantmentEffect[] Effects;

                    public struct EnchantmentEffect
                    {
                        public string EFID;
                        public EnchantmentEffectEFIT EFIT;
                        public EnchantmentSCIT SCIT;
                    }

                    public struct EnchantmentSCIT
                    {
                        public uint ScriptFormID;
                        public SpellSchool School;
                        public string VisualEffect;
                        public uint Flags;
                        public string FULL;
                    }

                    public enum SpellSchool : uint
                    {
                        Alteration,
                        Conjuration,
                        Destruction,
                        Illusion,
                        Mysticism,
                        Restoration
                    }

                    public struct EnchantmentEffectEFIT
                    {
                        public string EffectID;
                        public uint Magnitude;
                        public uint Area;
                        public uint Duration;
                        public TypeEFIT Type;
                        public uint ActorValue;
                    }

                    public enum TypeEFIT : uint
                    {
                        Self,
                        Touch,
                        Target
                    }

                    public struct EnchantmentENIT
                    {
                        public TypeENIT Type;
                        public uint? ChargeAmount;
                        public uint EnchantmentCost;
                        public uint Flags;
                    }

                    public enum TypeENIT : uint
                    {
                        Scroll,
                        Staff,
                        Weapon,
                        Apparel
                    }

                    public enum SubRecordType : uint
                    {
                        EDID = 0x44494445,
                        FULL = 0x4C4C5546,
                        ENIT = 0x54494E45,
                        EFID = 0x44494645,
                        EFIT = 0x54494645,
                        SKIT = 0x54494353
                    }

                    public void Read(EndianIO io, uint DataSize)
                    {
                        int x = 0;
                        long EndOffset = io.Stream.Position + DataSize;
                        while (io.Stream.Position < EndOffset)
                        {
                            switch ((SubRecordType)io.In.ReadUInt32())
                            {
                                case SubRecordType.EDID:
                                    EDID = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.FULL:
                                    FULL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.ENIT:
                                    io.Stream.Position += 2;
                                    ENIT.Type = (TypeENIT)io.In.ReadUInt32();
                                    ENIT.ChargeAmount = io.In.ReadUInt32();
                                    ENIT.EnchantmentCost = io.In.ReadUInt32();
                                    ENIT.Flags = io.In.ReadUInt32();
                                    break;
                                case SubRecordType.EFID:
                                    if (x == 0)
                                        Effects = new EnchantmentEffect[1];
                                    else
                                        Array.Resize(ref Effects, x + 1);
                                    Effects[x].EFID = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    switch ((SubRecordType)io.In.ReadUInt32())
                                    {
                                        case SubRecordType.EFIT:
                                            io.Stream.Position += 2;
                                            Effects[x].EFIT.EffectID = io.In.ReadAsciiString(4);
                                            Effects[x].EFIT.Magnitude = io.In.ReadUInt32();
                                            Effects[x].EFIT.Area = io.In.ReadUInt32();
                                            Effects[x].EFIT.Duration = io.In.ReadUInt32();
                                            Effects[x].EFIT.Type = (TypeEFIT)io.In.ReadUInt32();
                                            Effects[x].EFIT.ActorValue = io.In.ReadUInt32();
                                            break;
                                        case SubRecordType.SKIT:
                                            io.Stream.Position += 2;
                                            Effects[x].SCIT.ScriptFormID = io.In.ReadUInt32();
                                            Effects[x].SCIT.School = (SpellSchool)io.In.ReadUInt32();
                                            Effects[x].SCIT.VisualEffect = io.In.ReadAsciiString(4);
                                            Effects[x].SCIT.Flags = io.In.ReadUInt32();
                                            if ((SubRecordType)io.In.ReadUInt32() == SubRecordType.FULL)
                                                Effects[x].SCIT.FULL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                            else
                                                io.Stream.Position -= 4;
                                            break;
                                        default:
                                            io.Stream.Position -= 4;
                                            break;
                                    }
                                    x++;
                                    break;
                                default:
                                    io.Stream.Position -= 4;
                                    break;
                            }
                        }
                    }

                    public void Write(ref byte[] Data)
                    {
                        MemoryStream MS = new MemoryStream();
                        EndianIO io = new EndianIO(MS, EndianType.LittleEndian);
                        io.Open();
                        if (EDID != null)
                        {
                            io.Out.Write((uint)SubRecordType.EDID);
                            WriteString(io, EDID);
                        }
                        if (FULL != null)
                        {
                            io.Out.Write((uint)SubRecordType.FULL);
                            WriteString(io, FULL);
                        }
                        if (ENIT.ChargeAmount.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.ENIT);
                            io.Out.Write((ushort)16);
                            io.Out.Write((uint)ENIT.Type);
                            io.Out.Write(ENIT.ChargeAmount.Value);
                            io.Out.Write(ENIT.EnchantmentCost);
                            io.Out.Write(ENIT.Flags);
                        }
                        if (Effects != null)
                        {
                            foreach (EnchantmentEffect Effect in Effects)
                            {
                                io.Out.Write((uint)SubRecordType.EFID);
                                io.Out.Write((ushort)4);
                                io.Out.WriteAsciiString(Effect.EFID, 4);
                                if (Effect.EFIT.EffectID != null)
                                {
                                    io.Out.Write((uint)SubRecordType.EFIT);
                                    io.Out.Write((ushort)24);
                                    io.Out.WriteAsciiString(Effect.EFIT.EffectID, 4);
                                    io.Out.Write(Effect.EFIT.Magnitude);
                                    io.Out.Write(Effect.EFIT.Area);
                                    io.Out.Write(Effect.EFIT.Duration);
                                    io.Out.Write((uint)Effect.EFIT.Type);
                                    io.Out.Write(Effect.EFIT.ActorValue);
                                }
                                if (Effect.SCIT.VisualEffect != null)
                                {
                                    io.Out.Write((uint)SubRecordType.SKIT);
                                    io.Out.Write((ushort)16);
                                    io.Out.Write(Effect.SCIT.ScriptFormID);
                                    io.Out.Write((uint)Effect.SCIT.School);
                                    io.Out.WriteAsciiString(Effect.SCIT.VisualEffect, 4);
                                    io.Out.Write(Effect.SCIT.Flags);
                                    if (Effect.SCIT.FULL != null)
                                    {
                                        io.Out.Write((uint)SubRecordType.FULL);
                                        WriteString(io, Effect.SCIT.FULL);
                                    }
                                }
                            }
                        }
                        io.Stream.Position = 0;
                        Data = io.In.ReadBytes((int)io.Stream.Length);
                        io.Close();
                    }

                    public void WriteString(EndianIO io, string Input)
                    {
                        io.Out.Write((ushort)(Input.Length + 1));
                        io.Out.WriteAsciiString(Input, Input.Length);
                        io.Out.Write((byte)0x00);
                    }
                }

                public class Weapon
                {
                    public string EDID, FULL, MODL, ICON;
                    public uint? ENAM;
                    public uint? SCRI;
                    public WeaponDATA DATA;
                    public byte[] MODT;
                    public ushort? ANAM;
                    public float? MODB;

                    public struct WeaponDATA
                    {
                        public WeaponType Type;
                        public float? Speed;
                        public float Reach;
                        public uint Flags;
                        public uint Value;
                        public uint Health;
                        public float Weight;
                        public ushort Damage;
                    }

                    public enum WeaponType : uint
                    {
                        Blade_One_Hand,
                        Blade_Two_Hand,
                        Blunt_One_Hand,
                        Blunt_Two_Hand,
                        Staff,
                        Bow
                    }

                    public enum SubRecordType : uint
                    {
                        EDID = 0x44494445,
                        FULL = 0x4C4C5546,
                        SCRI = 0x49524353,
                        MODL = 0x4C444F4D,
                        MODB = 0x42444F4D,
                        MODT = 0x54444F4D,
                        ICON = 0x4E4F4349,
                        ANAM = 0x4D414E41,
                        ENAM = 0x4D414E45,
                        DATA = 0x41544144
                    }

                    public void Read(EndianIO io, uint DataSize)
                    {
                        long EndOffset = io.Stream.Position + DataSize;
                        while (io.Stream.Position < EndOffset)
                            switch ((SubRecordType)io.In.ReadUInt32())
                            {
                                case SubRecordType.EDID:
                                    EDID = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.FULL:
                                    FULL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.SCRI:
                                    io.Stream.Position += 2;
                                    SCRI = io.In.ReadUInt32();
                                    break;
                                case SubRecordType.MODL:
                                    MODL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MODB:
                                    io.Stream.Position += 2;
                                    MODB = io.In.ReadSingle();
                                    break;
                                case SubRecordType.MODT:
                                    MODT = io.In.ReadBytes(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.ICON:
                                    ICON = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.ANAM:
                                    io.Stream.Position += 2;
                                    ANAM = io.In.ReadUInt16();
                                    break;
                                case SubRecordType.ENAM:
                                    io.Stream.Position += 2;
                                    ENAM = io.In.ReadUInt32();
                                    break;
                                case SubRecordType.DATA:
                                    io.Stream.Position += 2;
                                    DATA.Type = (WeaponType)io.In.ReadUInt32();
                                    DATA.Speed = io.In.ReadSingle();
                                    DATA.Reach = io.In.ReadSingle();
                                    DATA.Flags = io.In.ReadUInt32();
                                    DATA.Value = io.In.ReadUInt32();
                                    DATA.Health = io.In.ReadUInt32();
                                    DATA.Weight = io.In.ReadSingle();
                                    DATA.Damage = io.In.ReadUInt16();
                                    break;
                                default:
                                    io.Stream.Position -= 4;
                                    break;
                            }
                    }

                    public void Write(ref byte[] Data)
                    {
                        MemoryStream MS = new MemoryStream();
                        EndianIO io = new EndianIO(MS, EndianType.LittleEndian);
                        io.Open();
                        if (EDID != null)
                        {
                            io.Out.Write((uint)SubRecordType.EDID);
                            WriteString(io, EDID);
                        }
                        if (FULL != null)
                        {
                            io.Out.Write((uint)SubRecordType.FULL);
                            WriteString(io, FULL);
                        }
                        if (SCRI.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.SCRI);
                            io.Out.Write((ushort)4);
                            io.Out.Write(SCRI.Value);
                        }
                        if (MODL != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODL);
                            WriteString(io, MODL);
                        }
                        if (MODB.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.MODB);
                            io.Out.Write((ushort)4);
                            io.Out.Write(MODB.Value);
                        }
                        if (MODT != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODT);
                            WriteBytes(io, MODT);
                        }
                        if (ICON != null)
                        {
                            io.Out.Write((uint)SubRecordType.ICON);
                            WriteString(io, ICON);
                        }
                        if (ENAM.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.ENAM);
                            io.Out.Write((ushort)4);
                            io.Out.Write(ENAM.Value);
                        }
                        if (ANAM.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.ANAM);
                            io.Out.Write((ushort)2);
                            io.Out.Write(ANAM.Value);
                        }
                        if (DATA.Speed.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.DATA);
                            io.Out.Write((ushort)30);
                            io.Out.Write((uint)DATA.Type);
                            io.Out.Write(DATA.Speed.Value);
                            io.Out.Write(DATA.Reach);
                            io.Out.Write(DATA.Flags);
                            io.Out.Write(DATA.Value);
                            io.Out.Write(DATA.Health);
                            io.Out.Write(DATA.Weight);
                            io.Out.Write(DATA.Damage);
                        }
                        io.Stream.Position = 0;
                        Data = io.In.ReadBytes((int)io.Stream.Length);
                        io.Close();
                    }

                    public void WriteString(EndianIO io, string Input)
                    {
                        io.Out.Write((ushort)(Input.Length + 1));
                        io.Out.WriteAsciiString(Input, Input.Length);
                        io.Out.Write((byte)0x00);
                    }

                    public void WriteBytes(EndianIO io, byte[] Input)
                    {
                        io.Out.Write((ushort)Input.Length);
                        io.Out.Write(Input);
                    }
                }

                public class Armor
                {
                    public string EDID, FULL, MODL, MOD2, MOD3, MOD4, ICON, ICO2;
                    public byte[] MODB, MO2B, MO3B, MO4B, MODT, MO2T, MO3T, MO4T;
                    public ushort? ANAM;
                    public uint? SCRI, ENAM;
                    public RecordBMDT? BMDT;
                    public ArmorDATA DATA;

                    public struct ArmorDATA
                    {
                        public ushort Armor;
                        public uint Value;
                        public uint Health;
                        public float? Weight;
                    }

                    public enum SubRecordType : uint
                    {
                        EDID = 0x44494445,
                        FULL = 0x4C4C5546,
                        SCRI = 0x49524353,
                        BMDT = 0x54444D42,
                        MODL = 0x4C444F4D,
                        MOD2 = 0x32444F4D,
                        MOD3 = 0x33444F4D,
                        MOD4 = 0x34444F4D,
                        MODB = 0x42444F4D,
                        MO2B = 0x42324F4D,
                        MO3B = 0x42334F4D,
                        MO4B = 0x42344F4D,
                        MODT = 0x54444F4D,
                        MO2T = 0x54324F4D,
                        MO3T = 0x54334F4D,
                        MO4T = 0x54344F4D,
                        ICON = 0x4E4F4349,
                        ICO2 = 0x324F4349,
                        ANAM = 0x4D414E41,
                        ENAM = 0x4D414E45,
                        DATA = 0x41544144
                    }

                    public void Read(EndianIO io, uint DataSize)
                    {
                        long EndOffset = io.Stream.Position + DataSize;
                        while (io.Stream.Position < EndOffset)
                            switch ((SubRecordType)io.In.ReadUInt32())
                            {
                                case SubRecordType.EDID:
                                    EDID = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.FULL:
                                    FULL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.SCRI:
                                    io.Stream.Position += 2;
                                    SCRI = io.In.ReadUInt32();
                                    break;
                                case SubRecordType.BMDT:
                                    io.Stream.Position += 2;
                                    BMDT = (RecordBMDT)io.In.ReadUInt32();
                                    break;
                                case SubRecordType.MODL:
                                    MODL = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MOD2:
                                    MOD2 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MOD3:
                                    MOD3 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MOD4:
                                    MOD4 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.MODB:
                                    MODB = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO2B:
                                    MO2B = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO3B:
                                    MO3B = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO4B:
                                    MO4B = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MODT:
                                    MODT = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO2T:
                                    MO2T = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO3T:
                                    MO3T = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.MO4T:
                                    MO4T = io.In.ReadBytes(io.In.ReadInt16());
                                    break;
                                case SubRecordType.ICON:
                                    ICON = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.ICO2:
                                    ICO2 = io.In.ReadAsciiString(io.In.ReadUInt16());
                                    break;
                                case SubRecordType.ANAM:
                                    io.Stream.Position += 2;
                                    ANAM = io.In.ReadUInt16();
                                    break;
                                case SubRecordType.ENAM:
                                    io.Stream.Position += 2;
                                    ENAM = io.In.ReadUInt32();
                                    break;
                                case SubRecordType.DATA:
                                    io.Stream.Position += 2;
                                    DATA.Armor = io.In.ReadUInt16();
                                    DATA.Value = io.In.ReadUInt32();
                                    DATA.Health = io.In.ReadUInt32();
                                    DATA.Weight = io.In.ReadSingle();
                                    break;
                                default:
                                    io.Stream.Position -= 4;
                                    break;
                            }
                    }

                    public void Write(ref byte[] Data)
                    {
                        MemoryStream MS = new MemoryStream();
                        EndianIO io = new EndianIO(MS, EndianType.LittleEndian);
                        io.Open();
                        if (EDID != null)
                        {
                            io.Out.Write((uint)SubRecordType.EDID);
                            WriteString(io, EDID);
                        }
                        if (FULL != null)
                        {
                            io.Out.Write((uint)SubRecordType.FULL);
                            WriteString(io, FULL);
                        }
                        if (SCRI != null)
                        {
                            io.Out.Write((uint)SubRecordType.SCRI);
                            io.Out.Write((ushort)4);
                            io.Out.Write(SCRI.Value);
                        }
                        if (BMDT != null)
                        {
                            io.Out.Write((uint)SubRecordType.BMDT);
                            io.Out.Write((ushort)4);
                            io.Out.Write((uint)BMDT);
                        }
                        if (MODL != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODL);
                            WriteString(io, MODL);
                        }
                        if (MOD2 != null)
                        {
                            io.Out.Write((uint)SubRecordType.MOD2);
                            WriteString(io, MOD2);
                        }
                        if (MOD3 != null)
                        {
                            io.Out.Write((uint)SubRecordType.MOD3);
                            WriteString(io, MOD3);
                        }
                        if (MOD4 != null)
                        {
                            io.Out.Write((uint)SubRecordType.MOD4);
                            WriteString(io, MOD4);
                        }
                        if (MODB != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODB);
                            WriteBytes(io, MODB);
                        }
                        if (MO2B != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO2B);
                            WriteBytes(io, MO2B);
                        }
                        if (MO3B != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO3B);
                            WriteBytes(io, MO3B);
                        }
                        if (MO4B != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO4B);
                            WriteBytes(io, MO4B);
                        }
                        if (MODT != null)
                        {
                            io.Out.Write((uint)SubRecordType.MODT);
                            WriteBytes(io, MODT);
                        }
                        if (MO2T != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO2T);
                            WriteBytes(io, MO2T);
                        }
                        if (MO3T != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO3T);
                            WriteBytes(io, MO3T);
                        }
                        if (MO4T != null)
                        {
                            io.Out.Write((uint)SubRecordType.MO4T);
                            WriteBytes(io, MO4T);
                        }
                        if (ICON != null)
                        {
                            io.Out.Write((uint)SubRecordType.ICON);
                            WriteString(io, ICON);
                        }
                        if (ICO2 != null)
                        {
                            io.Out.Write((uint)SubRecordType.ICO2);
                            WriteString(io, ICO2);
                        }
                        if (ENAM.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.ENAM);
                            io.Out.Write((ushort)4);
                            io.Out.Write(ENAM.Value);
                        }
                        if (ANAM.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.ANAM);
                            io.Out.Write((ushort)2);
                            io.Out.Write(ANAM.Value);
                        }
                        if (DATA.Weight.HasValue)
                        {
                            io.Out.Write((uint)SubRecordType.DATA);
                            io.Out.Write((ushort)14);
                            io.Out.Write(DATA.Armor);
                            io.Out.Write(DATA.Value);
                            io.Out.Write(DATA.Health);
                            io.Out.Write(DATA.Weight.Value);
                        }
                        io.Stream.Position = 0;
                        Data = io.In.ReadBytes((int)io.Stream.Length);
                        io.Close();
                    }

                    public void WriteString(EndianIO io, string Input)
                    {
                        io.Out.Write((ushort)(Input.Length + 1));
                        io.Out.WriteAsciiString(Input, Input.Length);
                        io.Out.Write((byte)0x00);
                    }

                    public void WriteBytes(EndianIO io, byte[] Input)
                    {
                        io.Out.Write((ushort)Input.Length);
                        io.Out.Write(Input);
                    }
                }
            }

            public void Read(EndianIO io)
            {
                io.Stream.Position += 4;
                NumberOfRecords = io.In.ReadUInt32();
                NextObjectID = io.In.ReadUInt32();
                WorldID = io.In.ReadUInt32();
                WorldX = io.In.ReadUInt32();
                WorldY = io.In.ReadUInt32();
                PlayerPosition.FormIDOfCell = io.In.ReadUInt32();
                PlayerPosition.PlayerX = io.In.ReadSingle();
                PlayerPosition.PlayerY = io.In.ReadSingle();
                PlayerPosition.PlayerZ = io.In.ReadSingle();
                GlobalVariables = new GlobalVariable[io.In.ReadUInt16()];
                for (int x = 0; x < GlobalVariables.Length; x++)
                {
                    GlobalVariables[x].IRefOfGlobal = io.In.ReadUInt32();
                    GlobalVariables[x].Value = io.In.ReadSingle();
                }
                TESClassData = io.In.ReadBytes(io.In.ReadUInt16());
                ProcessesData = io.In.ReadBytes(io.In.ReadUInt16());
                SpecEventData = io.In.ReadBytes(io.In.ReadUInt16());
                WeatherData = io.In.ReadBytes(io.In.ReadUInt16());
                Unknown = io.In.ReadUInt32();
                CustomRecords = new CustomRecord[io.In.ReadUInt32()];
                for (int x = 0; x < CustomRecords.Length; x++)
                {
                    CustomRecords[x] = new CustomRecord();
                    CustomRecords[x].Read(io);
                }
                QuickKeyData = io.In.ReadBytes(io.In.ReadUInt16());
                ReticuleData = io.In.ReadBytes(io.In.ReadUInt16());
                InterfaceData = io.In.ReadBytes(io.In.ReadUInt16());
                io.Stream.Position += 2;
                Regions = new Region[io.In.ReadUInt16()];
                for (int x = 0; x < Regions.Length; x++)
                {
                    Regions[x].IRef = io.In.ReadUInt32();
                    Regions[x].Flags = io.In.ReadUInt32();
                }
            }

            public void Write(EndianIO io)
            {
                FormIDOffset = (uint)io.Stream.Position;
                io.Out.Write(FormIDOffset);
                io.Out.Write(NumberOfRecords);
                io.Out.Write(NextObjectID);
                io.Out.Write(WorldID);
                io.Out.Write(WorldX);
                io.Out.Write(WorldY);
                io.Out.Write(PlayerPosition.FormIDOfCell);
                io.Out.Write(PlayerPosition.PlayerX);
                io.Out.Write(PlayerPosition.PlayerY);
                io.Out.Write(PlayerPosition.PlayerZ);
                io.Out.Write((ushort)GlobalVariables.Length);
                foreach (GlobalVariable GlobalVar in GlobalVariables)
                {
                    io.Out.Write(GlobalVar.IRefOfGlobal);
                    io.Out.Write(GlobalVar.Value);
                }
                io.Out.Write((ushort)TESClassData.Length);
                io.Out.Write(TESClassData);
                io.Out.Write((ushort)ProcessesData.Length);
                io.Out.Write(ProcessesData);
                io.Out.Write((ushort)SpecEventData.Length);
                io.Out.Write(SpecEventData);
                io.Out.Write((ushort)WeatherData.Length);
                io.Out.Write(WeatherData);
                io.Out.Write(Unknown);
                io.Out.Write(CustomRecords.Length);
                foreach (CustomRecord Record in CustomRecords)
                    Record.Write(io);
                io.Out.Write((ushort)QuickKeyData.Length);
                io.Out.Write(QuickKeyData);
                io.Out.Write((ushort)ReticuleData.Length);
                io.Out.Write(ReticuleData);
                io.Out.Write((ushort)InterfaceData.Length);
                io.Out.Write(InterfaceData);
                io.Out.Write((ushort)(Regions.Length * 8 + 2));
                io.Out.Write((ushort)Regions.Length);
                foreach (Region CurrentRegion in Regions)
                {
                    io.Out.Write(CurrentRegion.IRef);
                    io.Out.Write(CurrentRegion.Flags);
                }
            }
        }

        public class SaveRecord
        {
            public uint FormID;
            public RecordType Type;
            public uint Flags;
            public byte Version;
            public PlayerRecord Player;
            public PlayerAttributesRecord PlayerAttributes;
            public byte[] Data;

            [Flags]
            public enum RecordType : byte
            {
                FACT = 6,
                APPA = 19,
                ARMO,
                BOOK,
                CLOT,
                INGR = 25,
                LIGH,
                MISC,
                WEAP = 33,
                NPC_ = 35,
                CREA,
                KEYM = 39,
                ALCH,
                CELL = 48,
                REFR,
                ACHR,
                ACRE,
                INFO = 58,
                QUST,
                PACK = 61
            }

            public class PlayerRecord
            {
                public string[] EditorIDsOfMagicEffects;
                public byte[] Moved, InventoryPadding, SubRecords, Footer;
                public byte[] Unknown1, Unknown2, Unknown3, Unknown5, Unknown7, Unknown8;
                public uint ActiveQuest;
                public uint[] KnownTopics, Unknown4, Unknown6;
                public float[] ExpPoints;
                public InventoryItem[] InventoryItems;
                public RandomOblivionDoor[] RandomOblivionDoors;
                public ReferencedObject[] ReferencedObjects;
                public PCAdvancement Advancements;
                public OpenQuest[] OpenQuests;
                public PCAttributes Attributes;

                public struct PCAttributes
                {
                    public string Name;
                    public byte[] FaceGeometrySymmetricData, FaceGeometryAsymmetricData, FaceTextureSymmetricData;
                    public uint BirthSign, Class, Race, Eyes;
                    public uint[] Stats;
                    public PCHair Hair;
                    public PCGender Gender;
                }

                public enum PCGender : byte
                {
                    Male,
                    Female
                }

                public struct PCHair
                {
                    public uint IRef;
                    public float Length;
                    public byte Flag;
                    public PCHairColor Color;
                }

                public struct PCHairColor
                {
                    public byte R;
                    public byte G;
                    public byte B;
                }

                public struct OpenQuest
                {
                    public uint QuestIRef;
                    public byte QuestStage, LogEntry;
                }

                public struct PCAdvancement
                {
                    public byte SpecializationAdvancesCombat, SpecializationAdvancesMagic, SpecializationAdvancesStealth, Unknown;
                    public byte[,] NumberOfSkillAdvancesForEachAttribute;
                    public uint NumberOfAdvancementsSinceLastPCLevelUP, MajorSkillAdvancements;
                    public uint[] NumberOfTimesSkillWasAdvanced;
                }

                public struct ReferencedObject
                {
                    public uint ObjectIRef;
                    public byte Flag;
                    public byte[] Unknown;
                }

                public struct RandomOblivionDoor
                {
                    public uint DoorIRef;
                    public byte Flag;
                }

                public struct InventoryItem
                {
                    public bool Delete;
                    public uint IRef, NumberOfStackedItems, NumberOfItemsWithChangedProperties;
                    public MasterProperty[] MasterProperties;
                }

                public struct MasterProperty
                {
                    public bool Delete;
                    public SlaveProperty[] SlaveProperties;
                }

                public struct SlaveProperty
                {
                    public byte ContainedSoul, ShortcutKey;
                    public byte[] Unknown, xMarkerHeadingRef, AIPack, Trespass;
                    public ushort AffectedItems;
                    public uint PlacedObject, GlobalVariable, FactionRank, Owner, Poison;
                    public PropertyType Type;
                    public float ItemHealth, TimeItemWasUsed, EnchantmentPoints, ItemScale;
                    public PropertyScriptRef ScriptRef;
                }

                public enum PropertyType : byte
                {
                    Worldspace = 0x11,
                    Equipped = 0x1b,
                    Equipped2 = 0x1c,
                    xMarkerHeadingRef = 0x1e,
                    AIPack = 0x1f,
                    Trespass = 0x20,
                    Disabled = 0x25,
                    Owner = 0x27,
                    NumberOfItemsAffectedByRecord = 0x2a,
                    ItemHealth = 0x2b,
                    TimeItemWasUsed = 0x2d,
                    EnchantmentPoints = 0x2e,
                    ContainedSoul = 0x2f,
                    ScriptReference = 0x12,
                    PlacedObject = 0x22,
                    GlobalVariable = 0x28,
                    FactionRank = 0x29,
                    Unknown = 0x36,
                    ItemScale = 0x37,
                    Poison = 0x48,
                    BoundItem = 0x50,
                    ShortcutKey = 0x55
                }

                public struct PropertyScriptRef
                {
                    public uint ScriptIRef;
                    public ushort NumberOfValuesSet;
                    public PropertyScriptVars[] ScriptVars;
                    public byte RecordEnd;
                }

                public struct PropertyScriptVars
                {
                    public ushort VariableIndex, VariableType;
                    public uint ReferenceVariable;
                    public double LocalVariable;
                }

                public void Read(ref byte[] Data)
                {
                    EndianIO io = new EndianIO(Data, EndianType.LittleEndian, true);
                    Moved = io.In.ReadBytes(28);
                    InventoryPadding = io.In.ReadBytes(877);
                    InventoryItems = new InventoryItem[io.In.ReadUInt16()];
                    for (int x = 0; x < InventoryItems.Length; x++)
                    {
                        InventoryItems[x].Delete = false;
                        InventoryItems[x].IRef = io.In.ReadUInt32();
                        InventoryItems[x].NumberOfStackedItems = io.In.ReadUInt32();
                        InventoryItems[x].NumberOfItemsWithChangedProperties = io.In.ReadUInt32();
                        if (InventoryItems[x].NumberOfItemsWithChangedProperties != 0)
                        {
                            InventoryItems[x].MasterProperties = new MasterProperty[InventoryItems[x].NumberOfItemsWithChangedProperties];
                            for (int m = 0; m < InventoryItems[x].MasterProperties.Length; m++)
                            {
                                InventoryItems[x].MasterProperties[m].Delete = false;
                                InventoryItems[x].MasterProperties[m].SlaveProperties = new SlaveProperty[io.In.ReadUInt16()];
                                for (int s = 0; s < InventoryItems[x].MasterProperties[m].SlaveProperties.Length; s++)
                                {
                                    InventoryItems[x].MasterProperties[m].SlaveProperties[s].Type = (PropertyType)io.In.ReadByte();
                                    switch (InventoryItems[x].MasterProperties[m].SlaveProperties[s].Type)
                                    {
                                        case PropertyType.NumberOfItemsAffectedByRecord:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].AffectedItems = io.In.ReadUInt16();
                                            break;
                                        case PropertyType.ItemHealth:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ItemHealth = io.In.ReadSingle();
                                            break;
                                        case PropertyType.TimeItemWasUsed:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].TimeItemWasUsed = io.In.ReadSingle();
                                            break;
                                        case PropertyType.EnchantmentPoints:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].EnchantmentPoints = io.In.ReadSingle();
                                            break;
                                        case PropertyType.ContainedSoul:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ContainedSoul = io.In.ReadByte();
                                            break;
                                        case PropertyType.ScriptReference:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.ScriptIRef = io.In.ReadUInt32();
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.NumberOfValuesSet = io.In.ReadUInt16();
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.ScriptVars = new PropertyScriptVars[InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.NumberOfValuesSet];
                                            for (ushort v = 0; v < InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.NumberOfValuesSet; v++)
                                            {
                                                InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.ScriptVars[v].VariableIndex = io.In.ReadUInt16();
                                                InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.ScriptVars[v].VariableType = io.In.ReadUInt16();
                                                if (InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.ScriptVars[v].VariableType == 0)
                                                    InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.ScriptVars[v].LocalVariable = io.In.ReadDouble();
                                                else
                                                    InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.ScriptVars[v].ReferenceVariable = io.In.ReadUInt32();
                                            }
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ScriptRef.RecordEnd = io.In.ReadByte();
                                            break;
                                        case PropertyType.PlacedObject:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].PlacedObject = io.In.ReadUInt32();
                                            break;
                                        case PropertyType.GlobalVariable:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].GlobalVariable = io.In.ReadUInt32();
                                            break;
                                        case PropertyType.FactionRank:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].FactionRank = io.In.ReadUInt32();
                                            break;
                                        case PropertyType.Unknown:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].Unknown = io.In.ReadBytes(5);
                                            break;
                                        case PropertyType.ItemScale:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ItemScale = io.In.ReadSingle();
                                            break;
                                        case PropertyType.ShortcutKey:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].ShortcutKey = io.In.ReadByte();
                                            break;
                                        case PropertyType.xMarkerHeadingRef:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].xMarkerHeadingRef = io.In.ReadBytes(20);
                                            break;
                                        case PropertyType.AIPack:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].AIPack = io.In.ReadBytes(14);
                                            break;
                                        case PropertyType.Trespass:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].Trespass = io.In.ReadBytes(63);
                                            break;
                                        case PropertyType.Owner:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].Owner = io.In.ReadUInt32();
                                            break;
                                        case PropertyType.Poison:
                                            InventoryItems[x].MasterProperties[m].SlaveProperties[s].Poison = io.In.ReadUInt32();
                                            break;
                                        case PropertyType.Equipped:
                                        case PropertyType.Equipped2:
                                        case PropertyType.BoundItem:
                                        case PropertyType.Disabled:
                                            break;
                                        default:
                                            throw new Exception("Unknown property type 0x"
                                                + ((byte)InventoryItems[x].MasterProperties[m].SlaveProperties[s].Type).ToString("X")
                                                + "!");
                                    }
                                }
                            }
                        }
                    }
                    int SubRecordStartOffset = (int)io.Stream.Position;
                    bool FoundHigh = false;
                    while (!FoundHigh)
                        if (io.In.ReadUInt16() == 17132)
                        {
                            io.Stream.Position += 19;
                            if (io.In.ReadUInt16() == 17046)
                            {
                                io.Stream.Position -= 21;
                                FoundHigh = true;
                            }
                            else
                                io.Stream.Position -= 20;
                        }
                        else
                            io.Stream.Position -= 1;
                    int SubRecordEndOffset = (int)io.Stream.Position - 2;
                    io.Stream.Position = SubRecordStartOffset;
                    SubRecords = io.In.ReadBytes(SubRecordEndOffset - SubRecordStartOffset);
                    Unknown1 = io.In.ReadBytes(21);
                    Unknown2 = io.In.ReadBytes(30);
                    Attributes.Stats = new uint[34];
                    for (int x = 0; x < 34; x++)
                        Attributes.Stats[x] = io.In.ReadUInt32();
                    Unknown3 = io.In.ReadBytes(10);
                    Unknown4 = new uint[io.In.ReadUInt16()];
                    for (int x = 0; x < Unknown4.Length; x++)
                        Unknown4[x] = io.In.ReadUInt32();
                    Unknown5 = io.In.ReadBytes(22);
                    Attributes.BirthSign = io.In.ReadUInt32();
                    Unknown6 = new uint[14];
                    for (int x = 0; x < 14; x++)
                        Unknown6[x] = io.In.ReadUInt32();
                    Unknown7 = io.In.ReadBytes(2);
                    RandomOblivionDoors = new RandomOblivionDoor[io.In.ReadUInt16()];
                    for (int x = 0; x < RandomOblivionDoors.Length; x++)
                    {
                        RandomOblivionDoors[x].DoorIRef = io.In.ReadUInt32();
                        RandomOblivionDoors[x].Flag = io.In.ReadByte();
                    }
                    Unknown8 = io.In.ReadBytes(2);
                    ReferencedObjects = new ReferencedObject[io.In.ReadUInt16()];
                    for (int x = 0; x < ReferencedObjects.Length; x++)
                    {
                        ushort StructLength = io.In.ReadUInt16();
                        ReferencedObjects[x].ObjectIRef = io.In.ReadUInt32();
                        ReferencedObjects[x].Flag = io.In.ReadByte();
                        ReferencedObjects[x].Unknown = io.In.ReadBytes(StructLength);
                    }
                    ExpPoints = new float[21];
                    for (int x = 0; x < 21; x++)
                        ExpPoints[x] = io.In.ReadSingle();
                    Advancements.NumberOfAdvancementsSinceLastPCLevelUP = io.In.ReadUInt32();
                    Advancements.NumberOfSkillAdvancesForEachAttribute =
                        new byte[Advancements.NumberOfAdvancementsSinceLastPCLevelUP, 8];
                    for (int x = 0; x < Advancements.NumberOfAdvancementsSinceLastPCLevelUP; x++)
                        for (int i = 0; i < 8; i++)
                            Advancements.NumberOfSkillAdvancesForEachAttribute[x, i] = io.In.ReadByte();
                    Advancements.SpecializationAdvancesCombat = io.In.ReadByte();
                    Advancements.SpecializationAdvancesMagic = io.In.ReadByte();
                    Advancements.SpecializationAdvancesStealth = io.In.ReadByte();
                    Advancements.NumberOfTimesSkillWasAdvanced = new uint[21];
                    for (int x = 0; x < 21; x++)
                        Advancements.NumberOfTimesSkillWasAdvanced[x] = io.In.ReadUInt32();
                    Advancements.MajorSkillAdvancements = io.In.ReadUInt32();
                    Advancements.Unknown = io.In.ReadByte();
                    ActiveQuest = io.In.ReadUInt32();
                    KnownTopics = new uint[io.In.ReadUInt16()];
                    for (int x = 0; x < KnownTopics.Length; x++)
                        KnownTopics[x] = io.In.ReadUInt32();
                    OpenQuests = new OpenQuest[io.In.ReadUInt16()];
                    for (int x = 0; x < OpenQuests.Length; x++)
                    {
                        OpenQuests[x].QuestIRef = io.In.ReadUInt32();
                        OpenQuests[x].QuestStage = io.In.ReadByte();
                        OpenQuests[x].LogEntry = io.In.ReadByte();
                    }
                    EditorIDsOfMagicEffects = new string[io.In.ReadUInt32()];
                    for (int x = 0; x < EditorIDsOfMagicEffects.Length; x++)
                        EditorIDsOfMagicEffects[x] = io.In.ReadAsciiString(4);
                    Attributes.FaceGeometrySymmetricData = io.In.ReadBytes(200);
                    Attributes.FaceGeometryAsymmetricData = io.In.ReadBytes(120);
                    Attributes.FaceTextureSymmetricData = io.In.ReadBytes(200);
                    Attributes.Race = io.In.ReadUInt32();
                    Attributes.Hair.IRef = io.In.ReadUInt32();
                    Attributes.Eyes = io.In.ReadUInt32();
                    Attributes.Hair.Length = io.In.ReadSingle();
                    Attributes.Hair.Color.R = io.In.ReadByte();
                    Attributes.Hair.Color.G = io.In.ReadByte();
                    Attributes.Hair.Color.B = io.In.ReadByte();
                    Attributes.Hair.Flag = io.In.ReadByte();
                    Attributes.Gender = (PCGender)io.In.ReadByte();
                    Attributes.Name = io.In.ReadAsciiString(io.In.ReadByte());
                    Attributes.Class = io.In.ReadUInt32();
                    Footer = io.In.ReadBytes((int)(io.Stream.Length - io.Stream.Position));
                    io.Close();
                }

                public void Write(ref byte[] Data)
                {
                    MemoryStream MS = new MemoryStream();
                    EndianIO io = new EndianIO(MS, EndianType.LittleEndian);
                    io.Open();
                    io.Out.Write(Moved);
                    io.Out.Write(InventoryPadding);
                    ushort NumberofInventoryItems = 0;
                    foreach (InventoryItem CurrentItem in InventoryItems)
                        if (!CurrentItem.Delete)
                            NumberofInventoryItems++;
                    io.Out.Write(NumberofInventoryItems);
                    foreach (InventoryItem CurrentItem in InventoryItems)
                    {
                        if (!CurrentItem.Delete)
                        {
                            io.Out.Write(CurrentItem.IRef);
                            io.Out.Write(CurrentItem.NumberOfStackedItems);
                            io.Out.Write(CurrentItem.NumberOfItemsWithChangedProperties);
                            if (CurrentItem.NumberOfItemsWithChangedProperties != 0)
                            {
                                foreach (MasterProperty Master in CurrentItem.MasterProperties)
                                {
                                    if (!Master.Delete)
                                    {
                                        io.Out.Write((ushort)Master.SlaveProperties.Length);
                                        foreach (SlaveProperty Slave in Master.SlaveProperties)
                                        {
                                            io.Out.Write((byte)Slave.Type);
                                            switch (Slave.Type)
                                            {
                                                case PropertyType.NumberOfItemsAffectedByRecord:
                                                    io.Out.Write(Slave.AffectedItems);
                                                    break;
                                                case PropertyType.ItemHealth:
                                                    io.Out.Write(Slave.ItemHealth);
                                                    break;
                                                case PropertyType.TimeItemWasUsed:
                                                    io.Out.Write(Slave.TimeItemWasUsed);
                                                    break;
                                                case PropertyType.EnchantmentPoints:
                                                    io.Out.Write(Slave.EnchantmentPoints);
                                                    break;
                                                case PropertyType.ContainedSoul:
                                                    io.Out.Write(Slave.ContainedSoul);
                                                    break;
                                                case PropertyType.ScriptReference:
                                                    io.Out.Write(Slave.ScriptRef.ScriptIRef);
                                                    io.Out.Write(Slave.ScriptRef.NumberOfValuesSet);
                                                    foreach (PropertyScriptVars Var in Slave.ScriptRef.ScriptVars)
                                                    {
                                                        io.Out.Write(Var.VariableIndex);
                                                        io.Out.Write(Var.VariableType);
                                                        if (Var.VariableType == 0)
                                                            io.Out.Write(Var.LocalVariable);
                                                        else
                                                            io.Out.Write(Var.ReferenceVariable);
                                                    }
                                                    io.Out.Write(Slave.ScriptRef.RecordEnd);
                                                    break;
                                                case PropertyType.PlacedObject:
                                                    io.Out.Write(Slave.PlacedObject);
                                                    break;
                                                case PropertyType.GlobalVariable:
                                                    io.Out.Write(Slave.GlobalVariable);
                                                    break;
                                                case PropertyType.FactionRank:
                                                    io.Out.Write(Slave.FactionRank);
                                                    break;
                                                case PropertyType.Unknown:
                                                    io.Out.Write(Slave.Unknown);
                                                    break;
                                                case PropertyType.ItemScale:
                                                    io.Out.Write(Slave.ItemScale);
                                                    break;
                                                case PropertyType.ShortcutKey:
                                                    io.Out.Write(Slave.ShortcutKey);
                                                    break;
                                                case PropertyType.xMarkerHeadingRef:
                                                    io.Out.Write(Slave.xMarkerHeadingRef);
                                                    break;
                                                case PropertyType.AIPack:
                                                    io.Out.Write(Slave.AIPack);
                                                    break;
                                                case PropertyType.Trespass:
                                                    io.Out.Write(Slave.Trespass);
                                                    break;
                                                case PropertyType.Owner:
                                                    io.Out.Write(Slave.Owner);
                                                    break;
                                                case PropertyType.Poison:
                                                    io.Out.Write(Slave.Poison);
                                                    break;
                                                default:
                                                 if (Slave.Type != PropertyType.Equipped
                                                     && Slave.Type != PropertyType.Equipped2
                                                     && Slave.Type != PropertyType.BoundItem
                                                     && Slave.Type != PropertyType.Disabled)
                                                    io.Stream.Position -= 1;
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    io.Out.Write(SubRecords);
                    io.Out.Write(Unknown1);
                    io.Out.Write(Unknown2);
                    foreach (uint Stat in Attributes.Stats)
                        io.Out.Write(Stat);
                    io.Out.Write(Unknown3);
                    io.Out.Write((ushort)Unknown4.Length);
                    foreach (uint Unknown in Unknown4)
                        io.Out.Write(Unknown);
                    io.Out.Write(Unknown5);
                    io.Out.Write(Attributes.BirthSign);
                    foreach (uint Unknown in Unknown6)
                        io.Out.Write(Unknown);
                    io.Out.Write(Unknown7);
                    io.Out.Write((ushort)RandomOblivionDoors.Length);
                    foreach (RandomOblivionDoor RandomDoor in RandomOblivionDoors)
                    {
                        io.Out.Write(RandomDoor.DoorIRef);
                        io.Out.Write(RandomDoor.Flag);
                    }
                    io.Out.Write(Unknown8);
                    io.Out.Write((ushort)ReferencedObjects.Length);
                    foreach (ReferencedObject Object in ReferencedObjects)
                    {
                        io.Out.Write((ushort)Object.Unknown.Length);
                        io.Out.Write(Object.ObjectIRef);
                        io.Out.Write(Object.Flag);
                        io.Out.Write(Object.Unknown);
                    }
                    foreach (float Exp in ExpPoints)
                        io.Out.Write(Exp);
                    io.Out.Write(Advancements.NumberOfAdvancementsSinceLastPCLevelUP);
                    for (int x = 0; x < Advancements.NumberOfAdvancementsSinceLastPCLevelUP; x++)
                    {
                        for (int i = 0; i < 8; i++)
                            io.Out.Write(Advancements.NumberOfSkillAdvancesForEachAttribute[x, i]);
                    }
                    io.Out.Write(Advancements.SpecializationAdvancesCombat);
                    io.Out.Write(Advancements.SpecializationAdvancesMagic);
                    io.Out.Write(Advancements.SpecializationAdvancesStealth);
                    foreach (uint Amount in Advancements.NumberOfTimesSkillWasAdvanced)
                        io.Out.Write(Amount);
                    io.Out.Write(Advancements.MajorSkillAdvancements);
                    io.Out.Write(Advancements.Unknown);
                    io.Out.Write(ActiveQuest);
                    io.Out.Write((ushort)KnownTopics.Length);
                    foreach (uint Topic in KnownTopics)
                        io.Out.Write(Topic);
                    io.Out.Write((ushort)OpenQuests.Length);
                    foreach (OpenQuest Quest in OpenQuests)
                    {
                        io.Out.Write(Quest.QuestIRef);
                        io.Out.Write(Quest.QuestStage);
                        io.Out.Write(Quest.LogEntry);
                    }
                    io.Out.Write(EditorIDsOfMagicEffects.Length);
                    foreach (string Editor in EditorIDsOfMagicEffects)
                        io.Out.WriteAsciiString(Editor, 4);
                    io.Out.Write(Attributes.FaceGeometrySymmetricData);
                    io.Out.Write(Attributes.FaceGeometryAsymmetricData);
                    io.Out.Write(Attributes.FaceTextureSymmetricData);
                    io.Out.Write(Attributes.Race);
                    io.Out.Write(Attributes.Hair.IRef);
                    io.Out.Write(Attributes.Eyes);
                    io.Out.Write(Attributes.Hair.Length);
                    io.Out.Write(Attributes.Hair.Color.R);
                    io.Out.Write(Attributes.Hair.Color.G);
                    io.Out.Write(Attributes.Hair.Color.B);
                    io.Out.Write(Attributes.Hair.Flag);
                    io.Out.Write((byte)Attributes.Gender);
                    io.Out.Write((byte)(Attributes.Name.Length + 1));
                    io.Out.WriteAsciiString(Attributes.Name, Attributes.Name.Length);
                    io.Out.Write((byte)0x00);
                    io.Out.Write(Attributes.Class);
                    io.Out.Write(Footer);
                    io.Stream.Position = 0;
                    Data = io.In.ReadBytes((int)io.Stream.Length);
                }
            }

            public class PlayerAttributesRecord
            {
                public byte[] Attributes, Skills;
                public PCBaseData BaseData;

                public struct PCBaseData
                {
                    public short Level;
                    public ushort BaseSpell, Fatigue, Unknown, MinimalCalculatedLevel, MaximalCalculatedLevel;
                    public uint Flag;
                }

                public void Read(ref byte[] Data)
                {
                    EndianIO io = new EndianIO(Data, EndianType.LittleEndian, true);
                    long StartPosition = io.Stream.Position;
                    Attributes = new byte[8];
                    for (int x = 0; x < 8; x++)
                        Attributes[x] = io.In.ReadByte();
                    BaseData.Flag = io.In.ReadUInt32();
                    BaseData.BaseSpell = io.In.ReadUInt16();
                    BaseData.Fatigue = io.In.ReadUInt16();
                    BaseData.Unknown = io.In.ReadUInt16();
                    BaseData.Level = io.In.ReadInt16();
                    BaseData.MinimalCalculatedLevel = io.In.ReadUInt16();
                    BaseData.MaximalCalculatedLevel = io.In.ReadUInt16();
                    io.Stream.Position = (Data.Length - 21);
                    Skills = new byte[21];
                    for (int x = 0; x < 21; x++)
                        Skills[x] = io.In.ReadByte();
                    io.Close();
                }

                public void Write(ref byte[] Data)
                {
                    EndianIO io = new EndianIO(Data, EndianType.LittleEndian);
                    io.Open();
                    io.Out.Write(Attributes);
                    io.Out.Write(BaseData.Flag);
                    io.Out.Write(BaseData.BaseSpell);
                    io.Out.Write(BaseData.Fatigue);
                    io.Out.Write(BaseData.Unknown);
                    io.Out.Write(BaseData.Level);
                    io.Out.Write(BaseData.MinimalCalculatedLevel);
                    io.Out.Write(BaseData.MaximalCalculatedLevel);
                    io.Stream.Position = (Data.Length - 21);
                    io.Out.Write(Skills);
                }
            }

            public void Read(EndianIO io)
            {
                FormID = io.In.ReadUInt32();
                Type = (RecordType)io.In.ReadByte();
                Flags = io.In.ReadUInt32();
                Version = io.In.ReadByte();
                Data = io.In.ReadBytes(io.In.ReadUInt16());
                if (Type == RecordType.ACHR && FormID == 20)
                {
                    Player = new PlayerRecord();
                    Player.Read(ref Data);
                }
                else if (Type == RecordType.NPC_ && FormID == 7)
                {
                    PlayerAttributes = new PlayerAttributesRecord();
                    PlayerAttributes.Read(ref Data);
                }
            }

            public void Write(EndianIO io)
            {
                io.Out.Write(FormID);
                io.Out.Write((byte)Type);
                io.Out.Write(Flags);
                io.Out.Write((byte)Version);
                if (Type == RecordType.ACHR && FormID == 20)
                    Player.Write(ref Data);
                else if (Type == RecordType.NPC_ && FormID == 7)
                    PlayerAttributes.Write(ref Data);
                io.Out.Write((ushort)Data.Length);
                io.Out.Write(Data);
            }
        }

        public EndianIO io;
        public SaveFileHeader FileHeader;
        public SaveGameHeader GameHeader;
        public SaveGlobals Globals;
        public SaveRecord[] Records;
        public byte[] TemporaryEffects;
        public uint[] FormIDs, WorldSpaces;

        public void LoadSave(EndianIO InStream)
        {
            io = InStream;
            io.SeekTo(0);
            FileHeader = new SaveFileHeader();
            FileHeader.Read(io);
            GameHeader = new SaveGameHeader();
            GameHeader.Read(io);
            Globals = new SaveGlobals();
            Globals.Read(io);
            Records = new SaveRecord[Globals.NumberOfRecords];
            for (int x = 0; x < Records.Length; x++)
            {
                Records[x] = new SaveRecord();
                Records[x].Read(io);
            }
            TemporaryEffects = io.In.ReadBytes(io.In.ReadInt32());
            FormIDs = new uint[io.In.ReadUInt32()];
            for (int x = 0; x < FormIDs.Length; x++)
                FormIDs[x] = io.In.ReadUInt32();
            WorldSpaces = new uint[io.In.ReadUInt32()];
            for (int x = 0; x < WorldSpaces.Length; x++)
                WorldSpaces[x] = io.In.ReadUInt32();
        }

        public EndianIO WriteSave()
        {
            MemoryStream ms = new MemoryStream();
            io = new EndianIO(ms, EndianType.LittleEndian);
            io.Open();
            FileHeader.Write(io);
            GameHeader.Write(io);
            Globals.Write(io);
            foreach (SaveRecord Record in Records)
                Record.Write(io);
            io.Out.Write(TemporaryEffects.Length);
            io.Out.Write(TemporaryEffects);
            uint FormIDOffset = (uint)io.Stream.Position;
            io.Out.Write(FormIDs.Length);
            foreach (uint FormID in FormIDs)
                io.Out.Write(FormID);
            io.Out.Write(WorldSpaces.Length);
            foreach (uint WorldSpace in WorldSpaces)
                io.Out.Write(WorldSpace);
            io.Stream.Position = 34;
            io.Out.Write(GameHeader.HeaderSize);
            io.Stream.Position = Globals.FormIDOffset;
            io.Out.Write(FormIDOffset);
            io.Stream.Position = 0;
            return io;
        }

        public SaveGlobals.CustomRecord GetCustomRecord(uint IRef)
        {
            foreach (SaveGlobals.CustomRecord Record in Globals.CustomRecords)
                if (Record.FormID == IRef)
                    return Record;
            throw new Exception("Custom record not found! IRef: " + IRef.ToString());
        }

        public void SetCustomRecord(SaveGlobals.CustomRecord CustomRecord)
        {
            for (int x = 0; x < Globals.CustomRecords.Length; x++)
                if (CustomRecord.Type == Globals.CustomRecords[x].Type && CustomRecord.FormID == Globals.CustomRecords[x].FormID)
                    Globals.CustomRecords[x] = CustomRecord;
        }

        public SaveRecord.PlayerRecord GetPlayerRecord()
        {
            return GetRecord(20, SaveRecord.RecordType.ACHR).Player;
        }

        public SaveRecord.PlayerRecord.InventoryItem GetInventoryItem(uint IRef)
        {
            SaveRecord Record = GetRecord(20, SaveRecord.RecordType.ACHR);
            foreach (SaveRecord.PlayerRecord.InventoryItem Item in Record.Player.InventoryItems)
                if (Item.IRef == IRef)
                    return Item;
            throw new Exception("Inventory Item not found! IRef: " + IRef.ToString());
        }

        public SaveRecord.PlayerRecord.InventoryItem GetInventoryItemFromFormID(uint FormID)
        {
            SaveRecord Record = GetRecord(20, SaveRecord.RecordType.ACHR);
            for (uint x = 0; x < FormIDs.Length; x++)
                if (FormIDs[x] == FormID)
                    foreach (SaveRecord.PlayerRecord.InventoryItem Item in Record.Player.InventoryItems)
                        if (Item.IRef == x)
                            return Item;
            throw new Exception("Inventory Item not found! FormID: " + FormID.ToString());
        }

        public bool InventoryItemExists(uint FormID)
        {
            for (uint x = 0; x < FormIDs.Length; x++)
                if (FormIDs[x] == FormID)
                {
                    SaveRecord Record = GetRecord(20, SaveRecord.RecordType.ACHR);
                    foreach (SaveRecord.PlayerRecord.InventoryItem Item in Record.Player.InventoryItems)
                        if (Item.IRef == x)
                            return true;
                }
            return false;
        }

        public void WriteInventoryItem(SaveRecord.PlayerRecord.InventoryItem Item)
        {
            SaveRecord.PlayerRecord Record = GetPlayerRecord();
            for (int x = 0; x < Record.InventoryItems.Length; x++)
                if (Record.InventoryItems[x].IRef == Item.IRef)
                    Record.InventoryItems[x] = Item;
        }

        public void SetPlayerRecord(SaveRecord.PlayerRecord PlayerRecord)
        {
            SaveRecord Record = GetRecord(20, SaveRecord.RecordType.ACHR);
            Record.Player = PlayerRecord;
            SetRecord(Record);
        }

        public SaveRecord.PlayerAttributesRecord GetPlayerAttributesRecord()
        {
            SaveRecord Record = GetRecord(7, SaveRecord.RecordType.NPC_);
            return Record.PlayerAttributes;
        }

        public void SetPlayerAttributesRecord(SaveRecord.PlayerAttributesRecord PlayerRecord)
        {
            SaveRecord Record = GetRecord(7, SaveRecord.RecordType.NPC_);
            Record.PlayerAttributes = PlayerRecord;
            SetRecord(Record);
        }

        public SaveRecord GetRecord(uint FormID, SaveRecord.RecordType Type)
        {
            foreach (SaveRecord Record in Records)
                if (Record.FormID == FormID && Record.Type == Type)
                    return Record;
            throw new Exception("Record not found! FormID: " + FormID.ToString());
        }

        public void SetRecord(SaveRecord Record)
        {
            for (int x = 0; x < Records.Length; x++)
                if (Record.Type == Records[x].Type && Record.FormID == Records[x].FormID)
                    Records[x] = Record;
        }

        public string GetInventoryItemName(uint IRef)
        {
            if (IsCustomRecord(IRef))
            {
                SaveGlobals.CustomRecord Record = GetCustomRecord(IRef);
                switch (Record.Type)
                {
                    case SaveGlobals.CustomRecord.RecordType.Armor:
                        return Record.ARMO.FULL;
                    case SaveGlobals.CustomRecord.RecordType.Clothing:
                        return Record.CLOT.FULL;
                    case SaveGlobals.CustomRecord.RecordType.Enchantment:
                        return Record.ENCH.FULL;
                    case SaveGlobals.CustomRecord.RecordType.Weapon:
                        return Record.WEAP.FULL;
                }
            }
            else
                return ((FormID)FormIDs[IRef]).ToString();
            throw new Exception("Inventory item not found! IRef: " + IRef.ToString());
        }

        public bool IsCustomRecord(uint IRef)
        {
            return (IRef > 4278190079);
        }

        public Image GetThumbnail()
        {
            Size size = new Size(GameHeader.ThumbnailWidth, GameHeader.ThumbnailHeight);
            Bitmap newImage = new Bitmap(size.Width, size.Height);
            BitmapData bitmapdata = newImage.LockBits(new Rectangle(new Point(0, 0), size), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            IntPtr destination = bitmapdata.Scan0;
            int length = 3 * size.Width;
            EndianIO tio = new EndianIO(GameHeader.Thumbnail, EndianType.LittleEndian, true);
            for (int i = 0; i < size.Height; i++)
            {
                Marshal.Copy(tio.In.ReadBytes(length), 0, destination, length);
                destination = new IntPtr(destination.ToInt64() + bitmapdata.Stride);
            }
            tio.Close();
            newImage.UnlockBits(bitmapdata);
            return newImage;
        }

        public enum FormID : uint
        {
            Akavari_Sunderblade = 0x000CA154,
            Akavari_Warblade = 0x000CA155,
            Akaviri_Dai___Katana = 0x0009DAC5,
            Akaviri_Katana = 0x00024DCA,
            Akaviri_Katana_ = 0x000977C9,
            Akaviri_Warhammer_Burden = 0x000387B5,
            Ancient_Akaviri_Katana = 0x000E78E5,
            Apotheosis = 0x000CA153,
            Axe_of_Hazards = 0x0003CCFF,
            Axe_of_Icy_Darkness = 0x0003BF64,
            Ayleid_Long_Sword = 0x00014F14,
            Ayleid_Mace = 0x00014F15,
            Battle_Axe_of_Absorption = 0x0003841F,
            Battle_Axe_of_Beguilement = 0x0003539C,
            Battle_Axe_of_Blizzards = 0x0002308B,
            Battle_Axe_of_Cold = 0x000220A8,
            Battle_Axe_of_Damnation = 0x000353C2,
            Battle_Axe_of_Decay = 0x0003B407,
            Battle_Axe_of_Deception = 0x0003539A,
            Battle_Axe_of_Depletion = 0x000387AE,
            Battle_Axe_of_Diminishing = 0x0003D7D3,
            Battle_Axe_of_Dispel = 0x0003D7CB,
            Battle_Axe_of_Dissolution = 0x0003B40A,
            Battle_Axe_of_Embers = 0x0003DAE7,
            Battle_Axe_of_Enfeeblement = 0x0003CD09,
            Battle_Axe_of_Feeding = 0x0003B402,
            Battle_Axe_of_Fire = 0x0003DB0C,
            Battle_Axe_of_Flames = 0x0003DB03,
            Battle_Axe_of_Freezing = 0x000225D9,
            Battle_Axe_of_Frost = 0x0003DB8F,
            Battle_Axe_of_Jinxing = 0x0003AB6F,
            Battle_Axe_of_Lightning = 0x0002C278,
            Battle_Axe_of_Putrification = 0x0003B40C,
            Battle_Axe_of_Rending = 0x0003B40E,
            Battle_Axe_of_Sapping = 0x0003D7E2,
            Battle_Axe_of_Scorching = 0x0003DAFA,
            Battle_Axe_of_Shocking = 0x0002B8D6,
            Battle_Axe_of_Siphoning = 0x0003B3FF,
            Battle_Axe_of_Soul_Snares = 0x000353CA,
            Battle_Axe_of_Soul_Traps = 0x00035397,
            Battle_Axe_of_Souls = 0x00035C27,
            Battle_Axe_of_Sparks = 0x0002B561,
            Battle_Axe_of_Storms = 0x0002C286,
            Battle_Axe_of_the_Blaze = 0x0003DB15,
            Battle_Axe_of_the_Dynamo = 0x0002BFFA,
            Battle_Axe_of_the_Glacier = 0x00022738,
            Battle_Axe_of_the_Inferno = 0x0003DB1E,
            Battle_Axe_of_Transmogrify = 0x0003B403,
            Battle_Axe_of_Voltage = 0x0002BDDD,
            Battle_Axe_of_Weakness = 0x0003AB43,
            Battle_Axe_of_Winter = 0x000230D9,
            Battleaxe_of_Hatred = 0x000CA152,
            Baurus__s_Akaviri_Katana = 0x0006B843,
            Black_Bow = 0x000908AD,
            Blackwater_Blade = 0x0006B697,
            Blackwater_Blade_ = 0x0006B698,
            Blackwater_Blade__ = 0x0006B699,
            Blackwater_Blade___ = 0x0006B69A,
            Blackwater_Blade____ = 0x0006B69B,
            Blackwater_Blade_____ = 0x0000C201,
            Blade_of_Fiery_Light = 0x0003BF60,
            Blade_of_Woe = 0x00022BA8,
            Blade_of_Woe_ = 0x000918FF,
            Blade_of_Woe__ = 0x00091900,
            Blade_of_Woe___ = 0x00091901,
            Blade_of_Woe____ = 0x00091902,
            Blade_of_Woe_____ = 0x00091903,
            Blade_of_Woe______ = 0x00091904,
            Blade_of_Woe_______ = 0x00091905,
            Blizzard_Bow = 0x0003CD06,
            Boreal = 0x0003CD05,
            Bound_Axe = 0x00026276,
            Bound_Bow = 0x0002627D,
            Bound_Dagger = 0x0002627C,
            Bound_Mace = 0x00026274,
            Bound_Sword = 0x00026273,
            Bow_of_Blizzards = 0x0002308D,
            Bow_of_Burning = 0x0003DAF2,
            Bow_of_Cold = 0x000220F6,
            Bow_of_Curses = 0x0003AB87,
            Bow_of_Despair = 0x0003AC88,
            Bow_of_Embers = 0x0003DAE9,
            Bow_of_Fire = 0x0003DB0D,
            Bow_of_Flames = 0x0003DB04,
            Bow_of_Freezing = 0x000225DA,
            Bow_of_Frost = 0x0003DB91,
            Bow_of_Gloom = 0x0003AC89,
            Bow_of_Harm = 0x0003AB6C,
            Bow_of_Infernal_Frost = 0x00082DE4,
            Bow_of_Infliction = 0x000CA156,
            Bow_of_Jolts = 0x0002B585,
            Bow_of_Lightning = 0x0002C27A,
            Bow_of_Numbing = 0x0003DB7B,
            Bow_of_Quietus = 0x0002B558,
            Bow_of_Scorching = 0x0003DAFB,
            Bow_of_Shocking = 0x0002B8D7,
            Bow_of_Silence = 0x0002B559,
            Bow_of_Sparks = 0x0002B562,
            Bow_of_Storms = 0x0002C287,
            Bow_of_the_Blaze = 0x0003DB16,
            Bow_of_the_Dynamo = 0x0002BFFB,
            Bow_of_the_Glacier = 0x00022854,
            Bow_of_the_Inferno = 0x0003DB1F,
            Bow_of_Voltage = 0x0002BEB2,
            Bow_of_Weariness = 0x0003AB6B,
            Bow_of_Winter = 0x000230DD,
            Broken_Sword = 0x0002D72B,
            Brusef_Amelion__s_Sword = 0x000091FB,
            Burden_of_Agnosticism = 0x00014953,
            Burden_of_Anger = 0x0001494F,
            Burden_of_Arrogance = 0x00014950,
            Burden_of_Flesh = 0x00014952,
            Burden_of_Secrecy = 0x00014951,
            Burden_of_Sin = 0x00014954,
            Burz__s_Glass_Mace = 0x000C760E,
            Burz__s_Glass_Mace_ = 0x000C760F,
            Burz__s_Glass_Mace__ = 0x000C7610,
            Caelia__s_Steel_Longsword = 0x0006AA99,
            Calliben__s_Grim_Retort = 0x000CB6F3,
            Captain_Kordan__s_Saber = 0x000CA158,
            Ceremonial_Dagger = 0x0008DA4A,
            Chillrend = 0x00068BFC,
            Chillrend_ = 0x00068BFD,
            Chillrend__ = 0x00068BFE,
            Chillrend___ = 0x00068BFF,
            Chillrend____ = 0x00068C00,
            Chillrend_____ = 0x00068C01,
            Claymore_of_Blizzards = 0x00023095,
            Claymore_of_Brittleness = 0x0003D7BB,
            Claymore_of_Burning = 0x0003DAF3,
            Claymore_of_Cold = 0x0002224D,
            Claymore_of_Depletion = 0x0003AB75,
            Claymore_of_Depletion_ = 0x0003D7E8,
            Claymore_of_Disintegration = 0x0003D7C7,
            Claymore_of_Dispel = 0x0003D7CC,
            Claymore_of_Embers = 0x0003DAEA,
            Claymore_of_Fire = 0x0003DB0E,
            Claymore_of_Flames = 0x0003DB05,
            Claymore_of_Fracturing = 0x0003D7BF,
            Claymore_of_Fragments = 0x0003D7B8,
            Claymore_of_Freezing = 0x000225F6,
            Claymore_of_Frost = 0x0003DB73,
            Claymore_of_Jinxing = 0x0003AB74,
            Claymore_of_Jolts = 0x0002B586,
            Claymore_of_Lightning = 0x0002C27B,
            Claymore_of_Numbing = 0x0003DB7C,
            Claymore_of_Pain = 0x0003D7DE,
            Claymore_of_Sapping = 0x0003D7E3,
            Claymore_of_Scorching = 0x0003DAFC,
            Claymore_of_Shattering = 0x0003D7C3,
            Claymore_of_Shocking = 0x0002B8D8,
            Claymore_of_Sparks = 0x0002B563,
            Claymore_of_Storms = 0x0002C289,
            Claymore_of_the_Blaze = 0x0003DB17,
            Claymore_of_the_Dynamo = 0x0002BFFC,
            Claymore_of_the_Glacier = 0x00022A2B,
            Claymore_of_the_Inferno = 0x0003DB20,
            Claymore_of_Voltage = 0x0002BFF3,
            Claymore_of_Weariness = 0x0003D7DA,
            Claymore_of_Winter = 0x000230DE,
            Club = 0x00159829,
            Club_ = 0x0003B375,
            Cursed_Mace = 0x00026F98,
            Daedric_Battle_Axe = 0x00035E77,
            Daedric_Bow = 0x000733DD,
            Daedric_Bow_ = 0x00035E7C,
            Daedric_Claymore = 0x00035E78,
            Daedric_Dagger = 0x00035E72,
            Daedric_Longsword = 0x000733D8,
            Daedric_Longsword_ = 0x00035E76,
            Daedric_Mace = 0x00035E75,
            Daedric_Shortsword = 0x00035E73,
            Daedric_War_Axe = 0x00035E74,
            Daedric_Warhammer = 0x00035E79,
            Dagger_of_Absorption = 0x0003841A,
            Dagger_of_Blizzards = 0x00023098,
            Dagger_of_Brittleness = 0x0003D7BC,
            Dagger_of_Burning = 0x0003DAF4,
            Dagger_of_Cold = 0x0002257F,
            Dagger_of_Depletion = 0x000387AA,
            Dagger_of_Depletion_ = 0x0003D7E9,
            Dagger_of_Diminishing = 0x0003D7D4,
            Dagger_of_Discipline = 0x000C891F,
            Dagger_of_Disintegration = 0x0003D7C8,
            Dagger_of_Dispel = 0x0003D7CD,
            Dagger_of_Embers = 0x0003DAEB,
            Dagger_of_Enfeeblement = 0x000389E6,
            Dagger_of_Fire = 0x0003DB0F,
            Dagger_of_Flames = 0x0003DB06,
            Dagger_of_Fracturing = 0x0003D7C0,
            Dagger_of_Fragments = 0x0003D7B7,
            Dagger_of_Freezing = 0x000225FC,
            Dagger_of_Frost = 0x0003DB74,
            Dagger_of_Jinxing = 0x0003AB6E,
            Dagger_of_Jolts = 0x0002B588,
            Dagger_of_Lightning = 0x0002C27D,
            Dagger_of_Numbing = 0x0003DB7D,
            Dagger_of_Pacification = 0x0003BF61,
            Dagger_of_Pain = 0x0003D7DF,
            Dagger_of_Paralysis = 0x0002B560,
            Dagger_of_Sapping = 0x0003D7E4,
            Dagger_of_Scorching = 0x0003DAFD,
            Dagger_of_Shattering = 0x0003D7C4,
            Dagger_of_Shocking = 0x0002B8D9,
            Dagger_of_Sparks = 0x0002B564,
            Dagger_of_Storms = 0x0002C28D,
            Dagger_of_the_Blaze = 0x0003DB18,
            Dagger_of_the_Dynamo = 0x0002BFFD,
            Dagger_of_the_Glacier = 0x00022A2C,
            Dagger_of_the_Inferno = 0x0003DB21,
            Dagger_of_Voltage = 0x0002BFF4,
            Dagger_of_Weakness = 0x0003AB3F,
            Dagger_of_Weariness = 0x0003D7DB,
            Dagger_of_Winter = 0x000230DF,
            Dalvilu_Cermonial_Dagger = 0x0001ECE5,
            Debaser = 0x0006BD81,
            Debaser_ = 0x0006BD82,
            Debaser__ = 0x0006BD83,
            Debaser___ = 0x0006BD84,
            Debaser____ = 0x0006BD85,
            Debaser_____ = 0x0001D0B4,
            Defiler = 0x0003CB6B,
            Destarine__s_Cleaver = 0x000CA159,
            Dragon__s_Bow = 0x0003CD04,
            Dremora_Bow = 0x000872AA,
            Dremora_Claymore = 0x0003E9C4,
            Dremora_Heavy_Bow = 0x000872AB,
            Dremora_Light_Bow = 0x000872A9,
            Dremora_Longsword = 0x0003E9C3,
            Dremora_Mace = 0x0003E9C2,
            Dwarven_Battle_Axe = 0x00035DCD,
            Dwarven_Bow = 0x000733DB,
            Dwarven_Bow_ = 0x00035DCE,
            Dwarven_Bow_Replica = 0x000C7979,
            Dwarven_Claymore = 0x00035DCF,
            Dwarven_Claymore_ = 0x000977CA,
            Dwarven_Claymore_Replica = 0x000C5606,
            Dwarven_Dagger = 0x00035DD0,
            Dwarven_Longsword = 0x000732B6,
            Dwarven_Longsword_ = 0x00035DD1,
            Dwarven_Mace = 0x00035DD2,
            Dwarven_Shortsword = 0x00035DD3,
            Dwarven_War_Axe = 0x00035DD4,
            Dwarven_War_Axe_Replica = 0x000C5602,
            Dwarven_Warhammer = 0x00035DD5,
            Ebony_Battle_Axe = 0x00035E6F,
            Ebony_Blade = 0x00027109,
            Ebony_Bow = 0x00035E7B,
            Ebony_Bow_ = 0x000977D0,
            Ebony_Claymore = 0x00035E70,
            Ebony_Dagger = 0x00035E6A,
            Ebony_Dagger_Replica = 0x000C5616,
            Ebony_Longsword = 0x000732DE,
            Ebony_Longsword_ = 0x00035E6E,
            Ebony_Mace = 0x00035E6D,
            Ebony_Mace_ = 0x000977CE,
            Ebony_Mace_Replica = 0x000C561F,
            Ebony_Shortsword = 0x00035E6B,
            Ebony_Shortsword_Replica = 0x000C798A,
            Ebony_War_Axe = 0x00035E6C,
            Ebony_War_Axe_ = 0x000977CF,
            Ebony_Warhammer = 0x00035E71,
            Ebony_Warhammer_Replica = 0x000C5604,
            Elven_Battle_Axe = 0x00035E67,
            Elven_Bow = 0x000229BF,
            Elven_Bow_ = 0x000977CC,
            Elven_Claymore = 0x00035E68,
            Elven_Dagger = 0x00035E63,
            Elven_Long_Sword = 0x000229B3,
            Elven_Long_Sword_ = 0x000977CD,
            Elven_Long_Sword_Replica = 0x000C55FF,
            Elven_Mace = 0x00035E66,
            Elven_Shortsword = 0x00035E64,
            Elven_Shortsword_Replica = 0x000C7985,
            Elven_War_Axe = 0x00035E65,
            Elven_Warhammer = 0x00035E69,
            Enchanted_Dagger = 0x0003FBCE,
            Essence_of_Regret = 0x00014963,
            Fine_Iron_Battle_Axe = 0x0003A85F,
            Fine_Iron_Bow = 0x0003A860,
            Fine_Iron_Claymore = 0x0003A861,
            Fine_Iron_Dagger = 0x0003A862,
            Fine_Iron_Longsword = 0x0003A863,
            Fine_Iron_Mace = 0x0003A864,
            Fine_Iron_Shortsword = 0x0003A865,
            Fine_Iron_War_Axe = 0x0003A866,
            Fine_Iron_Warhammer = 0x0003A867,
            Fine_Steel_Battle_Axe = 0x0003A856,
            Fine_Steel_Bow = 0x0003A857,
            Fine_Steel_Claymore = 0x0003A858,
            Fine_Steel_Dagger = 0x0003A859,
            Fine_Steel_Longsword = 0x0003A85A,
            Fine_Steel_Mace = 0x0003A85B,
            Fine_Steel_Shortsword = 0x0003A85C,
            Fine_Steel_War_Axe = 0x0003A85D,
            Fine_Steel_Warhammer = 0x0003A85E,
            Frostwyrm_Bow = 0x000C55E4,
            Ghost_Axe = 0x0003CD07,
            Gift_of_Flame = 0x0001494C,
            Gladiator__s_Sword = 0x0003E45B,
            Glass_Battle_Axe = 0x00035E60,
            Glass_Battle_Axe_Replica = 0x000C5608,
            Glass_Bow = 0x000733DC,
            Glass_Bow_ = 0x00035E7A,
            Glass_Claymore = 0x00035E61,
            Glass_Dagger = 0x00035E5B,
            Glass_Dagger_Replica = 0x000C7973,
            Glass_Longsword = 0x000732DD,
            Glass_Longsword_ = 0x00035E5F,
            Glass_Longsword_Replica = 0x000C5601,
            Glass_Mace = 0x00035E5E,
            Glass_Shortsword = 0x00035E5C,
            Glass_Shortsword_Replica = 0x000C7989,
            Glass_War_Axe = 0x00035E5D,
            Glass_Warhammer = 0x00035E62,
            Glass_Warhammer_ = 0x000977CB,
            Glenroy__s_Akaviri_Katana = 0x00022F82,
            Goblin_Shaman_Staff = 0x00066C45,
            Goblin_Totem_Staff = 0x000AA01F,
            Goldbrand = 0x00027105,
            Greater_Staff_of_Anarchy = 0x00091322,
            Greater_Staff_of_Apathy = 0x000912EC,
            Greater_Staff_of_Blundering = 0x000912D4,
            Greater_Staff_of_Burden = 0x000912BF,
            Greater_Staff_of_Calm = 0x000912C2,
            Greater_Staff_of_Catastrophe = 0x000912E3,
            Greater_Staff_of_Charm = 0x000912C8,
            Greater_Staff_of_Confusion = 0x000912F2,
            Greater_Staff_of_Corrosion = 0x000912FC,
            Greater_Staff_of_Demoralize = 0x000912F6,
            Greater_Staff_of_Dispel = 0x000912FF,
            Greater_Staff_of_Domination = 0x000912D1,
            Greater_Staff_of_Fatigue = 0x000912DA,
            Greater_Staff_of_Feeblemind = 0x000912D7,
            Greater_Staff_of_Fire = 0x00091326,
            Greater_Staff_of_Fireball = 0x00091329,
            Greater_Staff_of_Fragility = 0x0009131F,
            Greater_Staff_of_Frailty = 0x000912EF,
            Greater_Staff_of_Frost = 0x0009132C,
            Greater_Staff_of_Ice_Storm = 0x0009132F,
            Greater_Staff_of_Lethargy = 0x0009131C,
            Greater_Staff_of_Light = 0x00091332,
            Greater_Staff_of_Lightning = 0x000914B6,
            Greater_Staff_of_Open = 0x00091335,
            Greater_Staff_of_Ruin = 0x00091302,
            Greater_Staff_of_Severing = 0x000912E6,
            Greater_Staff_of_Sickness = 0x000912DD,
            Greater_Staff_of_Silence = 0x000914BC,
            Greater_Staff_of_Stopping = 0x00091338,
            Greater_Staff_of_Storms = 0x000914B9,
            Greater_Staff_of_Sundering = 0x00091316,
            Greater_Staff_of_Taming = 0x000912CE,
            Greater_Staff_of_Telekinesis = 0x000914BF,
            Greater_Staff_of_the_Dazed = 0x00091310,
            Greater_Staff_of_the_Doomed = 0x00091313,
            Greater_Staff_of_the_Grave = 0x000914C2,
            Greater_Staff_of_the_Oaf = 0x00091305,
            Greater_Staff_of_the_Pariah = 0x000912E9,
            Greater_Staff_of_the_Plague = 0x00091308,
            Greater_Staff_of_the_Weary = 0x0009130A,
            Greater_Staff_of_Weakness = 0x000912E1,
            Hatreds_Heart = 0x00014836,
            Hatreds_Heart_ = 0x00014C6C,
            Hatreds_Heart__ = 0x00014C6D,
            Hatreds_Heart___ = 0x00014C6E,
            Hatreds_Heart____ = 0x00014C6F,
            Hatreds_Heart_____ = 0x00014C70,
            Hatreds_Soul = 0x00014D65,
            Hatreds_Soul_ = 0x00014D66,
            Hatreds_Soul__ = 0x00014D67,
            Hatreds_Soul___ = 0x00014D68,
            Hatreds_Soul____ = 0x00014D69,
            Hatreds_Soul_____ = 0x00014D6A,
            Heat_of_Sinfulness = 0x00014960,
            Honorblade_of_Chorrol = 0x00028BA0,
            Hrormir__s_Icestaff = 0x00002DDF,
            Hrormir__s_Icestaff_ = 0x00047372,
            Immolator = 0x0003CD01,
            Iron_Battle_Axe = 0x00000D7F,
            Iron_Bow = 0x00025231,
            Iron_Bow_ = 0x000977C6,
            Iron_Claymore = 0x0001C6CD,
            Iron_Dagger = 0x00019171,
            Iron_Longsword = 0x00000C0C,
            Iron_Longsword_ = 0x000977D1,
            Iron_Mace = 0x00000D82,
            Iron_Shortsword = 0x00000C0D,
            Iron_War_Axe = 0x00000D81,
            Iron_War_Axe_ = 0x000977C8,
            Iron_Warhammer = 0x00019172,
            Languorwine_Blade = 0x00028277,
            Lesser_Staff_of_Anarchy = 0x00091323,
            Lesser_Staff_of_Apathy = 0x000912ED,
            Lesser_Staff_of_Blundering = 0x000912D2,
            Lesser_Staff_of_Burden = 0x000912D8,
            Lesser_Staff_of_Calm = 0x000912C0,
            Lesser_Staff_of_Catastrophe = 0x000912E4,
            Lesser_Staff_of_Charm = 0x000912C6,
            Lesser_Staff_of_Confusion = 0x000912F3,
            Lesser_Staff_of_Corrosion = 0x000912FA,
            Lesser_Staff_of_Demoralize = 0x000912F4,
            Lesser_Staff_of_Dispel = 0x000912FD,
            Lesser_Staff_of_Domination = 0x000912CF,
            Lesser_Staff_of_Fatigue = 0x000912DB,
            Lesser_Staff_of_Feeblemind = 0x000912E0,
            Lesser_Staff_of_Fire = 0x00091324,
            Lesser_Staff_of_Fireball = 0x00091327,
            Lesser_Staff_of_Fragility = 0x00091320,
            Lesser_Staff_of_Frailty = 0x000912F0,
            Lesser_Staff_of_Frost = 0x0009132A,
            Lesser_Staff_of_Ice_Storm = 0x0009132D,
            Lesser_Staff_of_Lethargy = 0x0009131D,
            Lesser_Staff_of_Light = 0x00091330,
            Lesser_Staff_of_Lightning = 0x000914CA,
            Lesser_Staff_of_Open = 0x00091334,
            Lesser_Staff_of_Ruin = 0x00091300,
            Lesser_Staff_of_Severing = 0x000912E7,
            Lesser_Staff_of_Sickness = 0x000912DE,
            Lesser_Staff_of_Silence = 0x000914BA,
            Lesser_Staff_of_Stopping = 0x00091336,
            Lesser_Staff_of_Storms = 0x000914B7,
            Lesser_Staff_of_Sundering = 0x00091317,
            Lesser_Staff_of_Taming = 0x000912CC,
            Lesser_Staff_of_Telekinesis = 0x000914BD,
            Lesser_Staff_of_the_Dazed = 0x00091311,
            Lesser_Staff_of_the_Doomed = 0x00091314,
            Lesser_Staff_of_the_Grave = 0x000914C0,
            Lesser_Staff_of_the_Oaf = 0x00091303,
            Lesser_Staff_of_the_Pariah = 0x000912EA,
            Lesser_Staff_of_the_Plague = 0x00091306,
            Lesser_Staff_of_the_Weary = 0x00091309,
            Lesser_Staff_of_Weakness = 0x000912D5,
            Long_Sword_of_Brittleness = 0x0003D7BD,
            Long_Sword_of_Depletion = 0x0003D7EA,
            Long_Sword_of_Flames = 0x0003DB07,
            Long_Sword_of_Freezing = 0x0002263D,
            Long_Sword_of_Voltage = 0x0002BFF5,
            Longsword_of_Absorption = 0x0003841D,
            Longsword_of_Blizzards = 0x000230A6,
            Longsword_of_Burning = 0x0003DAF5,
            Longsword_of_Cold = 0x00022580,
            Longsword_of_Depletion = 0x000387AC,
            Longsword_of_Diminishing = 0x0003D7D5,
            Longsword_of_Disintegration = 0x0003D7C9,
            Longsword_of_Dispel = 0x0003D7CE,
            Longsword_of_Embers = 0x0003DAEC,
            Longsword_of_Enfeeblement = 0x000389E8,
            Longsword_of_Fire = 0x0003DB10,
            Longsword_of_Fracturing = 0x0003D7C1,
            Longsword_of_Fragments = 0x0003D7B9,
            Longsword_of_Frost = 0x000B0705,
            Longsword_of_Frost_ = 0x0003DB75,
            Longsword_of_Jinxing = 0x0003AB70,
            Longsword_of_Jolts = 0x0002B589,
            Longsword_of_Lightning = 0x0002C27E,
            Longsword_of_Numbing = 0x0003DB7E,
            Longsword_of_Pain = 0x0003D7E0,
            Longsword_of_Sapping = 0x0003D7E5,
            Longsword_of_Scorching = 0x0003DAFE,
            Longsword_of_Shattering = 0x0003D7C5,
            Longsword_of_Shocking = 0x0002B8DD,
            Longsword_of_Sparks = 0x0002B565,
            Longsword_of_Storms = 0x0002C28F,
            Longsword_of_the_Blaze = 0x0003DB19,
            Longsword_of_the_Dynamo = 0x0002C00C,
            Longsword_of_the_Glacier = 0x00022A2E,
            Longsword_of_the_Inferno = 0x0003DB22,
            Longsword_of_Weakness = 0x0003AB41,
            Longsword_of_Weariness = 0x0003D7DC,
            Longsword_of_Winter = 0x000230E0,
            Mace_of_Abeyance = 0x000359E7,
            Mace_of_Absorption = 0x0003841C,
            Mace_of_Aversion = 0x000353D1,
            Mace_of_Blizzards = 0x000230AB,
            Mace_of_Burden = 0x000387B6,
            Mace_of_Burning = 0x0003DAF6,
            Mace_of_Cold = 0x0002258C,
            Mace_of_Deadweight = 0x000387BE,
            Mace_of_Decay = 0x0003AF07,
            Mace_of_Depletion = 0x000387AD,
            Mace_of_Diminishing = 0x0003D7D6,
            Mace_of_Dispel = 0x0003D7CF,
            Mace_of_Dissolution = 0x0003AF08,
            Mace_of_Embers = 0x0003DAED,
            Mace_of_Encumbrance = 0x000387BA,
            Mace_of_Enfeeblement = 0x000389E9,
            Mace_of_Excess = 0x000387BC,
            Mace_of_Feeding = 0x000387A8,
            Mace_of_Fire = 0x0003DB11,
            Mace_of_Flames = 0x0003DB08,
            Mace_of_Fracturing = 0x0003AF0A,
            Mace_of_Freezing = 0x00022642,
            Mace_of_Frost = 0x0003DB76,
            Mace_of_Holy_Light = 0x00035A64,
            Mace_of_Jinxing = 0x0003AB73,
            Mace_of_Jolts = 0x0002B58A,
            Mace_of_Legion = 0x0003BF65,
            Mace_of_Lightning = 0x0002C280,
            Mace_of_Molag_Bal = 0x00027117,
            Mace_of_Numbing = 0x0003DB7F,
            Mace_of_Overload = 0x000387B7,
            Mace_of_Putrification = 0x0003AF0D,
            Mace_of_Rending = 0x0003AF0E,
            Mace_of_Repelling = 0x000353CC,
            Mace_of_Sapping = 0x0003D7E6,
            Mace_of_Scorching = 0x0003DAFF,
            Mace_of_Shocking = 0x0002BAAA,
            Mace_of_Siphoning = 0x000387A7,
            Mace_of_Sparks = 0x0002B566,
            Mace_of_Storms = 0x0002C290,
            Mace_of_Strain = 0x000387B4,
            Mace_of_the_Blaze = 0x0003DB1A,
            Mace_of_the_Dynamo = 0x0002C00D,
            Mace_of_the_Glacier = 0x00022A2F,
            Mace_of_the_Inferno = 0x0003DB23,
            Mace_of_the_Undead = 0x00035AB3,
            Mace_of_Transmogrify = 0x0003B404,
            Mace_of_Turning = 0x000353D4,
            Mace_of_Voltage = 0x0002BFF6,
            Mace_of_Warding = 0x000353D0,
            Mace_of_Weakness = 0x0003AB40,
            Mace_of_Weight = 0x000387B1,
            Mace_of_Winter = 0x000230E1,
            Mage__s_Mace = 0x0003E45D,
            Mages_Staff = 0x0002C6A8,
            Mage__s_Staff_of_Charm = 0x000E9442,
            Mage__s_Staff_of_Charm_ = 0x000E9443,
            Mage__s_Staff_of_Charm__ = 0x000E9444,
            Mage__s_Staff_of_Charm___ = 0x0001FE11,
            Mage__s_Staff_of_Dispel = 0x000E9445,
            Mage__s_Staff_of_Dispel_ = 0x000E9446,
            Mage__s_Staff_of_Dispel__ = 0x000E9447,
            Mage__s_Staff_of_Dispel___ = 0x0001FE16,
            Mage__s_Staff_of_Fire = 0x000E9448,
            Mage__s_Staff_of_Fire_ = 0x000E9449,
            Mage__s_Staff_of_Fire__ = 0x000E944A,
            Mage__s_Staff_of_Fire___ = 0x000E944B,
            Mage__s_Staff_of_Frost = 0x000E944C,
            Mage__s_Staff_of_Frost_ = 0x000E944D,
            Mage__s_Staff_of_Frost__ = 0x000E944E,
            Mage__s_Staff_of_Frost___ = 0x0001FE0F,
            Mage__s_Staff_of_Paralysis = 0x000E944F,
            Mage__s_Staff_of_Paralysis_ = 0x000E9450,
            Mage__s_Staff_of_Paralysis__ = 0x000E9451,
            Mage__s_Staff_of_Paralysis___ = 0x0001FE12,
            Mage__s_Staff_of_Shock = 0x000E9452,
            Mage__s_Staff_of_Shock_ = 0x000E9453,
            Mage__s_Staff_of_Shock__ = 0x000E9454,
            Mage__s_Staff_of_Shock___ = 0x0001FE10,
            Mage__s_Staff_of_Silence = 0x000E9455,
            Mage__s_Staff_of_Silence_ = 0x000E9456,
            Mage__s_Staff_of_Silence__ = 0x000E9457,
            Mage__s_Staff_of_Silence___ = 0x0001FE13,
            Mage__s_Staff_of_Soul_Trap = 0x000E9458,
            Mage__s_Staff_of_Soul_Trap_ = 0x000E9459,
            Mage__s_Staff_of_Soul_Trap__ = 0x000E945A,
            Mage__s_Staff_of_Soul_Trap___ = 0x0001FE14,
            Mage__s_Staff_of_Telekinesis = 0x000E945B,
            Mage__s_Staff_of_Telekinesis_ = 0x000E945C,
            Mage__s_Staff_of_Telekinesis__ = 0x000E945D,
            Mage__s_Staff_of_Telekinesis___ = 0x0001FE15,
            Mankar_Camoran__s_Staff = 0x000477FC,
            Mankar_Camoran__s_Staff_ = 0x000BE320,
            Mankar_Camoran__s_Staff__ = 0x000BE321,
            Mankar_Camoran__s_Staff___ = 0x000BE322,
            Mishaxhi__s_Cleaver = 0x00187BC0,
            Mishaxhi__s_Cleaver_ = 0x00187BC5,
            Mishaxhi__s_Cleaver__ = 0x00187BC6,
            Mishaxhi__s_Cleaver___ = 0x00187BC7,
            Naked_Axe = 0x0003E45E,
            Nearness_of_Evil = 0x0001493D,
            Northwind = 0x00187BC1,
            Northwind_ = 0x00187BC2,
            Northwind__ = 0x00187BC3,
            Northwind___ = 0x00187BC4,
            Oblivion__s_Caress = 0x00014962,
            Oblivion__s_Embrace = 0x000149E2,
            Penance_of_Animosity = 0x00014966,
            Penance_of_Deception = 0x0001496A,
            Penance_of_Faithlessness = 0x0001496B,
            Penance_of_Hatred = 0x00014967,
            Penance_of_Larceny = 0x00014968,
            Penance_of_Pride = 0x00014969,
            Perdition__s_Wrath = 0x00082DE3,
            Pounder = 0x0003BF66,
            Purging_Flame = 0x00014961,
            Quality_Battle_Axe_Ember = 0x0003DAF1,
            Quality_Battle_Axe_Shiver = 0x0003DB7A,
            Quality_Battle_Axe_Soul_Trap = 0x00035385,
            Quality_Battle_Axe_Spark = 0x0002B573,
            Quintessence_of_Remorse = 0x000149E5,
            Redwave = 0x00095A39,
            Redwave_ = 0x00095A3A,
            Redwave__ = 0x00095A3B,
            Redwave___ = 0x00095A3C,
            Redwave____ = 0x00095A3D,
            Redwave_____ = 0x00095A3E,
            Redwave______ = 0x00095A3F,
            Renault__s_Akaviri_Katana = 0x00066C44,
            Retaliation_of_Blood = 0x000149ED,
            Retaliation_of_Spirit = 0x000149EF,
            Retribution_of_Aggression = 0x000149E7,
            Retribution_of_Cowardness = 0x000149E6,
            Retribution_of_Hubris = 0x000149E9,
            Retribution_of_Hypocrisy = 0x000149EA,
            Retribution_of_Rapacity = 0x000149E8,
            Retribution_of_Treachery = 0x000149EC,
            Revealer_of_Iniquity = 0x0001494E,
            Rockshatter = 0x00061421,
            Rockshatter_ = 0x0000CF1A,
            Rockshatter__ = 0x0000CF1B,
            Rockshatter___ = 0x0000CF1C,
            Rockshatter____ = 0x0000CF1D,
            Rockshatter_____ = 0x0000CF1E,
            Rockshatter______ = 0x0000CF1F,
            Rockshatter_______ = 0x00014EC2,
            Rockshatter________ = 0x00014EC3,
            Rockshatter_________ = 0x00014EC4,
            Rockshatter__________ = 0x00014EC5,
            Rockshatter___________ = 0x00014EC6,
            Rohssan__s_Antique_Cutlass = 0x0001C14B,
            Rugdumph__s_Sword = 0x0000BEA6,
            Ruined_Akaviri_Katana = 0x000980DF,
            Ruma__s_Staff = 0x0001FB1C,
            Rusty_Iron_Bow = 0x00047ACA,
            Rusty_Iron_Dagger = 0x00047AC2,
            Rusty_Iron_Dagger_ = 0x00090616,
            Rusty_Iron_Mace = 0x00090614,
            Rusty_Iron_Shortsword = 0x00090615,
            Rusty_Iron_War_Axe = 0x00090613,
            Sanguine_Rose = 0x000228EF,
            Shadowhunt = 0x00034897,
            Shadowhunt_ = 0x00034898,
            Shadowhunt__ = 0x00034899,
            Shadowhunt___ = 0x0003489A,
            Shadowhunt____ = 0x0003489B,
            Shadowhunt_____ = 0x0003489C,
            Shadowhunt______ = 0x0003489D,
            Sharpened_Cutlass = 0x000055AB,
            Shimmerstrike = 0x0002990E,
            Shortsword_of_Absorption = 0x0003841E,
            Shortsword_of_Blizzards = 0x000230AC,
            Shortsword_of_Brittleness = 0x0003D7BE,
            Shortsword_of_Burning = 0x0003DAF7,
            Shortsword_of_Cold = 0x0002259C,
            Shortsword_of_Depletion = 0x000387AB,
            Shortsword_of_Depletion_ = 0x0003D7EB,
            Shortsword_of_Diminishing = 0x0003D7D7,
            Shortsword_of_Disintegration = 0x0003D7CA,
            Shortsword_of_Dispel = 0x0003D7D0,
            Shortsword_of_Embers = 0x0003DAEE,
            Shortsword_of_Enfeeblement = 0x000389E7,
            Shortsword_of_Fire = 0x0003DB12,
            Shortsword_of_Flames = 0x0003DB09,
            Shortsword_of_Fracturing = 0x0003D7C2,
            Shortsword_of_Fragments = 0x0003D7BA,
            Shortsword_of_Freezing = 0x00022643,
            Shortsword_of_Frost = 0x0003DB77,
            Shortsword_of_Jinxing = 0x0003AB71,
            Shortsword_of_Jolts = 0x0002D72E,
            Shortsword_of_Lightning = 0x0002C281,
            Shortsword_of_Numbing = 0x0003DB80,
            Shortsword_of_Pain = 0x0003D7E1,
            Shortsword_of_Paralysis = 0x0002B55F,
            Shortsword_of_Sapping = 0x0003D7E7,
            Shortsword_of_Scorching = 0x0003DB00,
            Shortsword_of_Shattering = 0x0003D7C6,
            Shortsword_of_Shocking = 0x0002BB17,
            Shortsword_of_Sparks = 0x0002B567,
            Shortsword_of_Storms = 0x0002C292,
            Shortsword_of_the_Blaze = 0x0003DB1B,
            Shortsword_of_the_Dynamo = 0x0002C00E,
            Shortsword_of_the_Fang = 0x0003CB6A,
            Shortsword_of_the_Glacier = 0x00022EBD,
            Shortsword_of_the_Inferno = 0x0003DB24,
            Shortsword_of_Voltage = 0x0002BFF7,
            Shortsword_of_Weakness = 0x0003AB42,
            Shortsword_of_Weariness = 0x0003D7DD,
            Shortsword_of_Winter = 0x000230E2,
            Silver_BattleAxe = 0x00025221,
            Silver_BattleAxe_ = 0x000977D2,
            Silver_Bow = 0x00025227,
            Silver_Bow_ = 0x000733DA,
            Silver_Claymore = 0x00025226,
            Silver_Dagger = 0x00025224,
            Silver_Longsword = 0x0002521F,
            Silver_Longsword_ = 0x000977C7,
            Silver_Mace = 0x00025223,
            Silver_Mace_ = 0x000977C4,
            Silver_Shortsword = 0x00025220,
            Silver_Shortsword_ = 0x000977C5,
            Silver_War_Axe = 0x00025222,
            Silver_Warhammer = 0x00025225,
            Sinweaver = 0x0000172E,
            Skull_of_Corruption = 0x00027116,
            Spellbreaker = 0x00027112,
            Staff_of_Anarchy = 0x00091321,
            Staff_of_Apathy = 0x000912EB,
            Staff_of_Banishment = 0x000914D2,
            Staff_of_Blundering = 0x000912D3,
            Staff_of_Burden = 0x000912BE,
            Staff_of_Calm = 0x000912C1,
            Staff_of_Catastrophe = 0x000912E2,
            Staff_of_Charm = 0x000912C7,
            Staff_of_Conflagration = 0x000914C5,
            Staff_of_Confusion = 0x000912F1,
            Staff_of_Corrosion = 0x000912FB,
            Staff_of_Demoralize = 0x000912F5,
            Staff_of_Diminishing = 0x000914C4,
            Staff_of_Discord = 0x000914CD,
            Staff_of_Dispel = 0x000912FE,
            Staff_of_Domination = 0x000912D0,
            Staff_of_Fatigue = 0x000912D9,
            Staff_of_Feeblemind = 0x000912DF,
            Staff_of_Fire = 0x00091325,
            Staff_of_Fireball = 0x00091328,
            Staff_of_Flame = 0x0001CECC,
            Staff_of_Fragility = 0x0009131E,
            Staff_of_Frailty = 0x000912EE,
            Staff_of_Frost = 0x0009132B,
            Staff_of_Hoarfrost = 0x000914C6,
            Staff_of_Ice_Storm = 0x0009132E,
            Staff_of_Indarys = 0x0006B66D,
            Staff_of_Indarys_ = 0x0006B66E,
            Staff_of_Indarys__ = 0x0006B66F,
            Staff_of_Indarys___ = 0x0006B670,
            Staff_of_Indarys____ = 0x0006B671,
            Staff_of_Indarys_____ = 0x000335AF,
            Staff_of_Legion = 0x000914CC,
            Staff_of_Lethargy = 0x0009131B,
            Staff_of_Light = 0x00091331,
            Staff_of_Lightning = 0x000914B5,
            Staff_of_Nenalata = 0x00058EEC,
            Staff_of_Nenalata_ = 0x000BE5DC,
            Staff_of_Nenalata__ = 0x000BE5DD,
            Staff_of_Nenalata___ = 0x000BE5DE,
            Staff_of_Nenalata____ = 0x000BE5DF,
            Staff_of_Nenalata_____ = 0x000BE5E0,
            Staff_of_Open = 0x00091333,
            Staff_of_Pacification = 0x000914C9,
            Staff_of_Paralyzing_Burst = 0x00091337,
            Staff_of_Ruin = 0x00091301,
            Staff_of_Severing = 0x000912E5,
            Staff_of_Sickness = 0x000912DC,
            Staff_of_Silence = 0x000914BB,
            Staff_of_Storms = 0x000914B8,
            Staff_of_Submission = 0x000914CB,
            Staff_of_Sundering = 0x00091315,
            Staff_of_Taming = 0x000912CD,
            Staff_of_Telekinesis = 0x000914BE,
            Staff_of_the_Battlemage = 0x000493BD,
            Staff_of_the_Dazed = 0x0009130F,
            Staff_of_the_Doomed = 0x00091312,
            Staff_of_the_Everscamp = 0x0004F790,
            Staff_of_the_Grave = 0x000914C1,
            Staff_of_the_Healer = 0x000914D1,
            Staff_of_the_Mundane = 0x000914D0,
            Staff_of_the_Oaf = 0x00091304,
            Staff_of_the_Pariah = 0x000912E8,
            Staff_of_the_Plague = 0x00091307,
            Staff_of_the_Weary = 0x0009130B,
            Staff_of_Thunderbolts = 0x000914C7,
            Staff_of_Unholy_Terror = 0x00056E50,
            Staff_of_Vulnerability = 0x000914CF,
            Staff_of_Weakness = 0x000912D6,
            Staff_of_Weakness_ = 0x000914C8,
            Staff_of_Withering = 0x000914C3,
            Staff_of_Worms = 0x0004A24E,
            Steel_Battle_Axe = 0x000229B6,
            Steel_Bow = 0x000229B7,
            Steel_Bow_ = 0x0018ABF9,
            Steel_Bow__ = 0x000733D9,
            Steel_Claymore = 0x000229B8,
            Steel_Cutlass = 0x0003AA82,
            Steel_Dagger = 0x000229B9,
            Steel_Longsword = 0x000229BA,
            Steel_Longsword_ = 0x0018ABF7,
            Steel_Longsword__ = 0x000977C3,
            Steel_Mace = 0x000229BB,
            Steel_Shortsword = 0x000229BC,
            Steel_Shortsword_ = 0x00047ABF,
            Steel_War_Axe = 0x000229BD,
            Steel_Warhammer = 0x000229BE,
            Steel_Warhammer_ = 0x0018ABF8,
            Storm_Bow = 0x0003CD08,
            Sufferthorn = 0x000347EB,
            Sufferthorn_ = 0x000347EC,
            Sufferthorn__ = 0x000347ED,
            Sufferthorn___ = 0x000347EE,
            Sufferthorn____ = 0x000347EF,
            Sufferthorn_____ = 0x000347F0,
            Sufferthorn______ = 0x000347F1,
            Sword_of_Submission = 0x0003BF63,
            Sword_of_Wounding = 0x0003CD02,
            Tempest = 0x0003CCFE,
            Thieve__s_Dagger = 0x0003E45C,
            Thornblade = 0x0006B661,
            Thornblade_ = 0x0006B662,
            Thornblade__ = 0x0006B663,
            Thornblade___ = 0x0006B664,
            Thornblade____ = 0x0006B665,
            Thornblade_____ = 0x000335AE,
            Truncheon_of_Submission = 0x000CA157,
            Umbra = 0x00026B22,
            Unfinished_Staff = 0x000355A6,
            Volendrung = 0x00027108,
            Volendrung_ = 0x0009DB4F,
            Voltag = 0x0003BF62,
            Wabbajack = 0x000228F0,
            War_Axe_of_Absorption = 0x00038420,
            War_Axe_of_Beguilement = 0x0004F036,
            War_Axe_of_Blizzards = 0x000230AD,
            War_Axe_of_Burning = 0x0003DAF8,
            War_Axe_of_Cold = 0x000225B4,
            War_Axe_of_Damnation = 0x000353C5,
            War_Axe_of_Decay = 0x0003B408,
            War_Axe_of_Deception = 0x0003539B,
            War_Axe_of_Depletion = 0x000387AF,
            War_Axe_of_Diminishing = 0x0003D7D8,
            War_Axe_of_Dispel = 0x0003D7D1,
            War_Axe_of_Dissolution = 0x0003B409,
            War_Axe_of_Embers = 0x0003DAEF,
            War_Axe_of_Enfeeblement = 0x000389EB,
            War_Axe_of_Feeding = 0x0003B401,
            War_Axe_of_Fire = 0x0003DB13,
            War_Axe_of_Flames = 0x0003DB0A,
            War_Axe_of_Fracturing = 0x0003B40B,
            War_Axe_of_Freezing = 0x00022647,
            War_Axe_of_Frost = 0x0003DB78,
            War_Axe_of_Jinxing = 0x0003AB72,
            War_Axe_of_Jolts = 0x0002B58B,
            War_Axe_of_Lightning = 0x0002C283,
            War_Axe_of_Numbing = 0x0003DB81,
            War_Axe_of_Putrification = 0x0003B40D,
            War_Axe_of_Rending = 0x0003B40F,
            War_Axe_of_Sapping = 0x0004F038,
            War_Axe_of_Scorching = 0x0003DB01,
            War_Axe_of_Seduction = 0x00035396,
            War_Axe_of_Shocking = 0x0002BD0F,
            War_Axe_of_Siphoning = 0x0003B400,
            War_Axe_of_Soul_Snares = 0x000353CB,
            War_Axe_of_Soul_Traps = 0x00035398,
            War_Axe_of_Souls = 0x0003537A,
            War_Axe_of_Sparks = 0x0002B571,
            War_Axe_of_Storms = 0x0002C296,
            War_Axe_of_the_Blaze = 0x0003DB1C,
            War_Axe_of_the_Dynamo = 0x0002C020,
            War_Axe_of_the_Glacier = 0x00023003,
            War_Axe_of_the_Inferno = 0x0003DB25,
            War_Axe_of_Transmogrify = 0x0003B405,
            War_Axe_of_Voltage = 0x0002BFF8,
            War_Axe_of_Weakness = 0x0003AB44,
            War_Axe_of_Winter = 0x000230E3,
            Warhammer_of_Abeyance = 0x00035A24,
            Warhammer_of_Absorption = 0x0003841B,
            Warhammer_of_Aversion = 0x000353D2,
            Warhammer_of_Blizzards = 0x000230D3,
            Warhammer_of_Burning = 0x0003DAF9,
            Warhammer_of_Cold = 0x000225D8,
            Warhammer_of_Deadweight = 0x000387BD,
            Warhammer_of_Decay = 0x0003AF06,
            Warhammer_of_Depletion = 0x000387B0,
            Warhammer_of_Diminishing = 0x0003D7D9,
            Warhammer_of_Dispel = 0x0003D7D2,
            Warhammer_of_Dissolution = 0x0003AF09,
            Warhammer_of_Embers = 0x0003DAF0,
            Warhammer_of_Encumbrance = 0x000387B9,
            Warhammer_of_Enfeeblement = 0x000389EA,
            Warhammer_of_Excess = 0x000387BB,
            Warhammer_of_Feeding = 0x000387A9,
            Warhammer_of_Fire = 0x0003DB14,
            Warhammer_of_Flames = 0x0003DB0B,
            Warhammer_of_Fracturing = 0x0003AF0B,
            Warhammer_of_Freezing = 0x00022650,
            Warhammer_of_Frost = 0x0003DB79,
            Warhammer_of_Holy_Light = 0x00035AAE,
            Warhammer_of_Jinxing = 0x0003AB6D,
            Warhammer_of_Jolts = 0x0002B58C,
            Warhammer_of_Lightning = 0x0002C284,
            Warhammer_of_Numbing = 0x0003DB82,
            Warhammer_of_Overload = 0x000387B8,
            Warhammer_of_Putrification = 0x0003AF0C,
            Warhammer_of_Rending = 0x0003AF0F,
            Warhammer_of_Sapping = 0x0004F03A,
            Warhammer_of_Scorching = 0x0003DB02,
            Warhammer_of_Shocking = 0x0002BD6B,
            Warhammer_of_Siphoning = 0x000387A6,
            Warhammer_of_Sparks = 0x0002B572,
            Warhammer_of_Storms = 0x0002C298,
            Warhammer_of_Strain = 0x000387B3,
            Warhammer_of_the_Blaze = 0x0003DB1D,
            Warhammer_of_the_Dynamo = 0x0002C025,
            Warhammer_of_the_Glacier = 0x00023087,
            Warhammer_of_the_Grave = 0x000353CE,
            Warhammer_of_the_Inferno = 0x0003DB26,
            Warhammer_of_the_Undead = 0x00035AB6,
            Warhammer_of_Transmogrify = 0x0003B406,
            Warhammer_of_Turning = 0x00035644,
            Warhammer_of_Voltage = 0x0002BFF9,
            Warhammer_of_Warding = 0x000353CF,
            Warhammer_of_Weakness = 0x0003AB3E,
            Warhammer_of_Weight = 0x000387B2,
            Warhammer_of_Winter = 0x000230E4,
            Weight_of_Guilt = 0x0001494D,
            Witsplinter = 0x0006B1B9,
            Witsplinter_ = 0x0006B1C0,
            Witsplinter__ = 0x0006B1C1,
            Witsplinter___ = 0x0006B1C2,
            Witsplinter____ = 0x0006B1C3,
            Witsplinter_____ = 0x0006B1C4,
            Aegis_of_Reflection = 0x00053D77,
            Aegis_of_the_Apocalypse = 0x000CA117,
            Agronak__s_Raiment = 0x0003B1DC,
            Ambassador__s_Cuirass = 0x0004965F,
            Ancient_Blades_Helmet = 0x000C8540,
            Ancient_Blades_Shield = 0x000C8541,
            Ancient_Elven_Helm = 0x000150BA,
            Annealed_Cuirass = 0x0004939B,
            Anvil_Cuirass = 0x0002766D,
            Anvil_Shield = 0x000352C3,
            Apron_of_Adroitness = 0x000C5B4A,
            Apron_of_Adroitness_ = 0x000C5B4B,
            Apron_of_Adroitness__ = 0x000C5B4C,
            Apron_of_Adroitness___ = 0x000C5B4D,
            Apron_of_Adroitness____ = 0x000C5B4E,
            Apron_of_Adroitness_____ = 0x000C5B4F,
            Archer__s_Gauntlets = 0x00049170,
            Arena_Heavy_Raiment = 0x000236EF,
            Arena_Heavy_Raiment_ = 0x00029921,
            Arena_Light_Raiment = 0x000236F0,
            Arena_Light_Raiment_ = 0x00029920,
            Armor_Crafter__s_Gauntlets = 0x0004F3F7,
            Armor_of_Tiber_Septim = 0x0001FECF,
            Ayleid_Crown_of_Lindai = 0x000A55FA,
            Ayleid_Crown_of_Lindai_ = 0x000A933E,
            Ayleid_Crown_of_Lindai__ = 0x000BE5D1,
            Ayleid_Crown_of_Lindai___ = 0x000BE5D2,
            Ayleid_Crown_of_Lindai____ = 0x000BE5D3,
            Ayleid_Crown_of_Lindai_____ = 0x000BE5D4,
            Ayleid_Crown_of_Lindai______ = 0x000BE5DA,
            Ayleid_Crown_of_Nenalata = 0x000A55FB,
            Ayleid_Crown_of_Nenalata_ = 0x000BE5D5,
            Ayleid_Crown_of_Nenalata__ = 0x000BE5D6,
            Ayleid_Crown_of_Nenalata___ = 0x000BE5D7,
            Ayleid_Crown_of_Nenalata____ = 0x000BE5D8,
            Ayleid_Crown_of_Nenalata_____ = 0x000BE5D9,
            Bands_of_Kwang_Lao = 0x000C47B1,
            Bands_of_the_Chosen = 0x0003C803,
            Barkeep__s_Gauntlets = 0x0004950B,
            Battle_Medic__s_Cuirass = 0x00049362,
            Battlehunter_Gauntlets = 0x00049685,
            Battlehunter_Helmet = 0x00049686,
            Beveled_Gauntlets = 0x00049397,
            Birthright_of_Astalon = 0x000CA110,
            Black_Marsh_Helmet = 0x0004951A,
            Blackwood_Boots = 0x00038514,
            Blackwood_Cuirass = 0x00038510,
            Blackwood_Gauntlets = 0x00038511,
            Blackwood_Greaves = 0x00038512,
            Blackwood_Helmet = 0x00038513,
            Blackwood_Shield = 0x0003647D,
            Bladefall_Cuirass = 0x00049389,
            Blades_Boots = 0x00022F20,
            Blades_Boots_ = 0x00023921,
            Blades_Cuirass = 0x00022F65,
            Blades_Cuirass_ = 0x00023318,
            Blades_Gauntlets = 0x00022F6F,
            Blades_Gauntlets_ = 0x0002391C,
            Blades_Greaves = 0x00022F70,
            Blades_Greaves_ = 0x00023329,
            Blades_Helmet = 0x00000C09,
            Blades_Helmet_ = 0x00022F71,
            Blades_Helmet__ = 0x000738D6,
            Blades_Shield = 0x00022F7F,
            Blades_Shield_ = 0x00023922,
            Blades_Shield__ = 0x000738D7,
            Bloodworm_Helm = 0x00014673,
            Bloodworm_Helm_ = 0x0007BE3F,
            Bloodworm_Helm__ = 0x0007BE40,
            Bloodworm_Helm___ = 0x0007BE41,
            Bloodworm_Helm____ = 0x0007BE42,
            Bloodworm_Helm_____ = 0x0007BE43,
            Blower__s_Cuirass = 0x000A577F,
            Bonfire_Greaves = 0x0004F401,
            Boots_of_Bloody_Bounding = 0x000348A6,
            Boots_of_Bloody_Bounding_ = 0x000348A7,
            Boots_of_Bloody_Bounding__ = 0x000348A8,
            Boots_of_Bloody_Bounding___ = 0x000348A9,
            Boots_of_Bloody_Bounding____ = 0x000348AA,
            Boots_of_Bloody_Bounding_____ = 0x000348AB,
            Boots_of_Bloody_Bounding______ = 0x000348AC,
            Boots_of_Grounding = 0x0004917A,
            Boots_of_Insulation = 0x00049160,
            Boots_of_Legerity = 0x0004916A,
            Boots_of_Plain_Striding = 0x0004F3F2,
            Boots_of_Running = 0x00049509,
            Boots_of_Shock_Resistance = 0x00049370,
            Boots_of_Silence = 0x00049173,
            Boots_of_Soft_Walking = 0x000A5785,
            Boots_of_the_Atronach = 0x00047892,
            Boots_of_the_Calming_Sea = 0x00049392,
            Boots_of_the_Cheetah = 0x0004899E,
            Boots_of_the_Cutpurse = 0x0004914E,
            Boots_of_the_Eel = 0x000489AF,
            Boots_of_the_Forest_Stalker = 0x000A5784,
            Boots_of_the_Harbinger = 0x00049652,
            Boots_of_the_Jument = 0x00049667,
            Boots_of_the_Olympian = 0x0004788D,
            Boots_of_the_Savannah = 0x0004F3F8,
            Boots_of_the_Shark = 0x000489B3,
            Boots_of_the_Siltrunner = 0x00049377,
            Boots_of_the_Storm = 0x0002C23F,
            Boots_of_the_Swift_Merchant = 0x000CA111,
            Boots_of_the_Taskmaster = 0x0002C21E,
            Boots_of_the_Thrall = 0x0002C215,
            Boots_of_the_Tiger = 0x000489A6,
            Boots_of_the_Unburdened = 0x0004951D,
            Borosilicate_Boots = 0x000493A6,
            Bound_Boots = 0x0002626A,
            Bound_Cuirass = 0x0002626B,
            Bound_Gauntlets = 0x0002626C,
            Bound_Greaves = 0x00026270,
            Bound_Helmet = 0x00026272,
            Bound_Helmet_ = 0x00051B47,
            Bound_Mythic_Dawn_Armor = 0x00033525,
            Bound_Shield = 0x00026263,
            Bravil_Cuirass = 0x00027673,
            Bravil_Shield = 0x000352C5,
            Broadhead_Gauntlets = 0x000A577A,
            Broken_Shield = 0x0006E2E1,
            Bruma_Cuirass = 0x00027677,
            Bruma_Shield = 0x000352C7,
            Brusef_Amelion__s_Boots = 0x0012DD1B,
            Brusef_Amelion__s_Cuirass = 0x000091FA,
            Brusef_Amelion__s_Gauntlets = 0x0012DD1A,
            Brusef_Amelion__s_Greaves = 0x0012DD18,
            Brusef_Amelion__s_Helmet = 0x0012DD19,
            Brusef_Amelion__s_Shield = 0x0012DD1C,
            Burning_Shield = 0x00049688,
            Canopy_Helmet = 0x00049388,
            Cave_Diver__s_Helmet = 0x00049684,
            Cave_Scout_Helmet = 0x00049676,
            Cavern_Guide_Gauntlets = 0x00049674,
            Chainmail_Boots = 0x0001C6D4,
            Chainmail_Cuirass = 0x0001C6D3,
            Chainmail_Gauntlets = 0x0001C6D6,
            Chainmail_Greaves = 0x0001C6D5,
            Chainmail_Helmet = 0x000977BC,
            Chainmail_Helmet_ = 0x0001C6D7,
            Chameleon_Cuirass = 0x00049165,
            Cheydinhal_Cuirass = 0x00027678,
            Cheydinhal_Shield = 0x000352C9,
            Chorrol_Cuirass = 0x00027679,
            Chorrol_Shield = 0x000352CB,
            Clannfear_Hide_Shield = 0x0004F3F9,
            Clear_Sight_Gauntlets = 0x000A577C,
            Copperhead_Cuirass = 0x0004915F,
            Crystal_Greaves = 0x0004939A,
            Crystalline_Cuirass = 0x00049393,
            Cuirass_of_Anu__s_Blessing = 0x00049380,
            Cuirass_of_Battle = 0x00049672,
            Cuirass_of_Cleansing = 0x000493A5,
            Cuirass_of_Fortitude = 0x0002C218,
            Cuirass_of_Health = 0x0004916C,
            Cuirass_of_Lifeblood = 0x0004966C,
            Cuirass_of_Natural_Assimilation = 0x0004938B,
            Cuirass_of_Pain_Resistance = 0x0004969B,
            Cuirass_of_Poison_Blood = 0x0004969D,
            Cuirass_of_Protection = 0x00049179,
            Cuirass_of_Resilience = 0x00049520,
            Cuirass_of_Resistance = 0x00049515,
            Cuirass_of_Skill = 0x0004950C,
            Cuirass_of_Tenacity = 0x0004968A,
            Cuirass_of_the_Assassin = 0x00049147,
            Cuirass_of_the_Bear = 0x0004899F,
            Cuirass_of_the_Blood_Legion = 0x0002C223,
            Cuirass_of_the_Cameleon = 0x00048997,
            Cuirass_of_the_Cave_Viper = 0x0004967C,
            Cuirass_of_the_Cobra = 0x000489AE,
            Cuirass_of_the_Diplomat = 0x00049660,
            Cuirass_of_the_Elephant = 0x000489AD,
            Cuirass_of_the_Farlands_Trader = 0x000A577E,
            Cuirass_of_the_Fox = 0x000489A4,
            Cuirass_of_the_Herald = 0x00049521,
            Cuirass_of_the_Juggernaut = 0x00049692,
            Cuirass_of_the_Pit_Viper = 0x0002C23B,
            Cuirass_of_the_Ranger = 0x00049378,
            Cuirass_of_the_Spy = 0x00049359,
            Cuirass_of_the_Thiefcatcher = 0x0004914F,
            Cuirass_of_the_Undefeated = 0x0002C21B,
            Cuirass_of_Vitality = 0x0004966A,
            Cured_Shield = 0x0004914C,
            Daedric_Boots = 0x00036359,
            Daedric_Cuirass = 0x0003635B,
            Daedric_Gauntlets = 0x0000C582,
            Daedric_Gauntlets_ = 0x00036358,
            Daedric_Greaves = 0x0003635A,
            Daedric_Helmet = 0x000733F4,
            Daedric_Helmet_ = 0x00036357,
            Daedric_Shield = 0x000733F0,
            Daedric_Shield_ = 0x0003635C,
            Darksplitter_Helmet = 0x00049696,
            Deathmarch_Greaves = 0x0002C21A,
            Deepdweller__s_Shield = 0x00049668,
            Deer_Skin_Gauntlets = 0x00048999,
            Deer_Skin_Helmet = 0x0004899B,
            Defender__s_Shield = 0x00049670,
            Dondoran__s_Juggernaut = 0x000CA10F,
            Dremora_Caitiff_Boots = 0x0003E9BA,
            Dremora_Caitiff_Cuirass = 0x0003E9BE,
            Dremora_Caitiff_Greaves = 0x0003E9BF,
            Dremora_Caitiff_Helmet = 0x0008B882,
            Dremora_Caitiff_Robe = 0x00066A34,
            Dremora_Caitiff_Shield = 0x0003E9C0,
            Dremora_Churl_Boots = 0x0003E9B9,
            Dremora_Churl_Cuirass = 0x0003E9BD,
            Dremora_Churl_Greaves = 0x0003E9BC,
            Dremora_Churl_Robe = 0x00066A33,
            Dremora_Kynmarcher_Boots = 0x0003E9B3,
            Dremora_Kynmarcher_Cuirass = 0x0003E9B4,
            Dremora_Kynmarcher_Greaves = 0x0003E9B5,
            Dremora_Kynmarcher_Helmet = 0x0008B88C,
            Dremora_Kynmarcher_Helmet_ = 0x0008B88D,
            Dremora_Kynmarcher_Robe = 0x00066A38,
            Dremora_Kynreeve_Boots = 0x0003E9AF,
            Dremora_Kynreeve_Cuirass = 0x0003E9B0,
            Dremora_Kynreeve_Greaves = 0x0003E9B1,
            Dremora_Kynreeve_Helmet = 0x0008B88A,
            Dremora_Kynreeve_Helmet_ = 0x0000C57D,
            Dremora_Kynreeve_Robe = 0x00066A36,
            Dremora_Kynreeve_Shield = 0x0000C57E,
            Dremora_Kynreeve_Shield_ = 0x0003E9B2,
            Dremora_Kynval_Boots = 0x0003E9BB,
            Dremora_Kynval_Cuirass = 0x0003E9C1,
            Dremora_Kynval_Greaves = 0x0003E9AE,
            Dremora_Kynval_Helmet = 0x0008B888,
            Dremora_Kynval_Robe = 0x00066A35,
            Dremora_Markynaz_Boots = 0x0000C57F,
            Dremora_Markynaz_Boots_ = 0x0003E9B6,
            Dremora_Markynaz_Cuirass = 0x0000C580,
            Dremora_Markynaz_Cuirass_ = 0x0003E9B7,
            Dremora_Markynaz_Greaves = 0x0000C581,
            Dremora_Markynaz_Greaves_ = 0x0003E9B8,
            Dremora_Markynaz_Robe = 0x00066A39,
            Dremora_Valkynaz_Robe = 0x00066A3A,
            Dwarven_Boots = 0x00036347,
            Dwarven_Ceremonial_Shield = 0x000C5610,
            Dwarven_Cuirass = 0x00036349,
            Dwarven_Gauntlets = 0x00036346,
            Dwarven_Greaves = 0x00036348,
            Dwarven_Helmet = 0x000733F1,
            Dwarven_Helmet_ = 0x00036345,
            Dwarven_Shield = 0x000733EE,
            Dwarven_Shield_ = 0x0003634A,
            Dwarvenskin_Shield = 0x0002C220,
            Eagle_Feather_Shield = 0x000489AA,
            Ebony_Boots = 0x00036353,
            Ebony_Ceremonial_Gauntlets = 0x000C5615,
            Ebony_Ceremonial_Shield = 0x000C561E,
            Ebony_Cuirass = 0x0002AD85,
            Ebony_Gauntlets = 0x00036352,
            Ebony_Greaves = 0x00036354,
            Ebony_Helmet = 0x000733F3,
            Ebony_Helmet_ = 0x00036351,
            Ebony_Shield = 0x000733EF,
            Ebony_Shield_ = 0x00036356,
            Elven_Boots = 0x0002299F,
            Elven_Boots_ = 0x00014F13,
            Elven_Ceremonial_Cuirass = 0x000C55FE,
            Elven_Ceremonial_Helmet = 0x000CAB65,
            Elven_Ceremonial_Shield = 0x000C7984,
            Elven_Cuirass = 0x0002299C,
            Elven_Cuirass_ = 0x00014F0D,
            Elven_Gauntlets = 0x0002299E,
            Elven_Gauntlets_ = 0x00014F10,
            Elven_Greaves = 0x0002299D,
            Elven_Greaves_ = 0x00014F11,
            Elven_Helmet = 0x000229A1,
            Elven_Helmet_ = 0x000733E5,
            Elven_Helmet__ = 0x00014F12,
            Elven_Shield = 0x000229A0,
            Elven_Shield_ = 0x000977C1,
            Emperor__s_Boots = 0x0001FECE,
            Emperor__s_Cuirass = 0x0003ABB9,
            Emperor__s_Gauntlets = 0x0001FED0,
            Emperor__s_Greaves = 0x0001FED1,
            Emperor__s_Helmet = 0x0001FED2,
            Emperor__s_Robe = 0x0000C4D5,
            Enchanterbane_Helmet = 0x00049514,
            Escutcheon_of_Chorrol = 0x0008B07D,
            Escutcheon_of_Chorrol_ = 0x0006BDFA,
            Escutcheon_of_Chorrol__ = 0x0006BDFB,
            Escutcheon_of_Chorrol___ = 0x0006BDFC,
            Escutcheon_of_Chorrol____ = 0x0006BDFD,
            Escutcheon_of_Chorrol_____ = 0x0006BDFE,
            Extinguishing_Shield = 0x0004951E,
            Eyes_of_Akatosh = 0x0004951B,
            Fence_Cuirass = 0x00049154,
            Ferocious_Cuirass = 0x0004968C,
            Fin_Gleam = 0x00082DD8,
            Fire_Greaves = 0x00049512,
            Fire_Ritual_Greaves = 0x00049699,
            Firewalker_Greaves = 0x00049176,
            Fists_of_the_Drunkard = 0x000CA11A,
            Flamewalker_Greaves = 0x0002C229,
            Fleetfoot_Boots = 0x00049360,
            Flowing_Greaves = 0x0004939C,
            Footpad__s_Boots = 0x00049156,
            Forgemaster__s_Gauntlets = 0x00049523,
            Fortress_Shield = 0x00049653,
            Frost_Shield = 0x00049656,
            Frozen_Shield = 0x00049693,
            Fur_Boots = 0x00024767,
            Fur_Cuirass = 0x00024766,
            Fur_Gauntlets = 0x00024765,
            Fur_Greaves = 0x00024764,
            Fur_Helmet = 0x00024768,
            Fur_Helmet_ = 0x000733E2,
            Fur_Shield = 0x00025056,
            Fur_Shield_ = 0x000977BE,
            Gauntlets_of_Blinding_Speed = 0x00047891,
            Gauntlets_of_Brutality = 0x00049691,
            Gauntlets_of_Brutality_ = 0x0002C217,
            Gauntlets_of_Force = 0x0004F3F3,
            Gauntlets_of_Gluttony = 0x000CA11C,
            Gauntlets_of_Infiltration = 0x00049155,
            Gauntlets_of_Life_Detection = 0x00049167,
            Gauntlets_of_Life_Seeing = 0x0004935C,
            Gauntlets_of_Life_Sight = 0x0004937C,
            Gauntlets_of_Lockbreaking = 0x000A5780,
            Gauntlets_of_Might = 0x00049669,
            Gauntlets_of_Passing = 0x000A5781,
            Gauntlets_of_Potence = 0x0004951F,
            Gauntlets_of_Punishment = 0x0002C221,
            Gauntlets_of_Revelation = 0x00049507,
            Gauntlets_of_Survival = 0x0004969A,
            Gauntlets_of_the_Battlemage = 0x00047894,
            Gauntlets_of_the_Equinox = 0x0004936C,
            Gauntlets_of_the_Forge = 0x00049524,
            Gauntlets_of_the_Fray = 0x00049671,
            Gauntlets_of_the_Gladiator = 0x00049654,
            Gauntlets_of_the_Horker = 0x000489AB,
            Gauntlets_of_the_Hunter = 0x0004F3F0,
            Gauntlets_of_the_North = 0x0004965D,
            Gauntlets_of_the_Pugilist = 0x0004789B,
            Gauntlets_of_the_Rat = 0x000489A5,
            Gauntlets_of_the_Scout = 0x0004914A,
            Gauntlets_of_the_Sentinel = 0x00049665,
            Gauntlets_of_the_Tundra = 0x0004915C,
            Gauntlets_of_the_Weaponmaster = 0x0004788C,
            Gauntlets_of_the_Woodsman = 0x000489A3,
            Gauntlets_of_Vigor = 0x00049500,
            Gauntlets_of_Winter = 0x0004F402,
            General__s_Cuirass = 0x00049655,
            General__s_Shield = 0x00049369,
            Glass_Boots = 0x00036341,
            Glass_Ceremonial_Cuirass = 0x000C560B,
            Glass_Ceremonial_Gauntlets = 0x000C560D,
            Glass_Ceremonial_Shield = 0x000C560E,
            Glass_Cuirass = 0x00036343,
            Glass_Gauntlets = 0x00036340,
            Glass_Greaves = 0x00036342,
            Glass_Helmet = 0x000733E6,
            Glass_Helmet_ = 0x0003633F,
            Glass_Shield = 0x000733E1,
            Glass_Shield_ = 0x00036344,
            Gloves_of_the_Caster = 0x00047893,
            Greaves_of_Canyon_Striding = 0x00049364,
            Greaves_of_Fire_Resistance = 0x0004936B,
            Greaves_of_Fluid_Motion = 0x000493A4,
            Greaves_of_Free_Movement = 0x0004936F,
            Greaves_of_Freedom = 0x0004F404,
            Greaves_of_Grace = 0x0004937D,
            Greaves_of_Legerity = 0x00049169,
            Greaves_of_Movement = 0x0004938A,
            Greaves_of_Poise = 0x00049398,
            Greaves_of_Proficiency = 0x00049365,
            Greaves_of_Protection = 0x00049382,
            Greaves_of_Purity = 0x000493AB,
            Greaves_of_Quickness = 0x0004914D,
            Greaves_of_Resilient_Flesh = 0x000478A3,
            Greaves_of_Shaded_Rest = 0x00049387,
            Greaves_of_Skill = 0x00049152,
            Greaves_of_Spell_Absorption = 0x0004917E,
            Greaves_of_Spell_Consumption = 0x00049376,
            Greaves_of_the_Acrobat = 0x0004916E,
            Greaves_of_the_Cat = 0x0004899D,
            Greaves_of_the_Deep_Dweller = 0x00049679,
            Greaves_of_the_Everlasting = 0x0004937F,
            Greaves_of_the_Flame = 0x0004965C,
            Greaves_of_the_Footsoldier = 0x0004916F,
            Greaves_of_the_Kiln = 0x000493A1,
            Greaves_of_the_Laborer = 0x0004966B,
            Greaves_of_the_Monkey = 0x000489A1,
            Greaves_of_the_Rhino = 0x000489A2,
            Greaves_of_the_Sun = 0x000478A1,
            Greaves_of_the_Tree_Runner = 0x00049381,
            Greaves_of_the_Tumbler = 0x00049151,
            Greaves_of_the_Unstoppable = 0x0004969C,
            Greaves_of_the_Warmonger = 0x0004968B,
            Greaves_of_Well___Being = 0x0004788F,
            Grounded_Boots = 0x000478A2,
            Guard_Helmet = 0x0002767C,
            Hammerfell_Shield = 0x0004950A,
            Hand_of_Akatosh = 0x00049657,
            Hands_of_Midnight = 0x00082DDF,
            Hands_of_the_Atronach = 0x000CA118,
            Hardened_Shield = 0x000493A9,
            Heavy_Raiment_of_Valor = 0x000355FA,
            Helm_of_Ferocity = 0x000CA11B,
            Helm_of_Oreyn_Bearclaw = 0x000A5659,
            Helm_of_the_Deep_Delver = 0x000CA119,
            Helmet_of_Arkay = 0x00049506,
            Helmet_of_Enlightenment = 0x00049363,
            Helmet_of_Exposition = 0x00049396,
            Helmet_of_Life_Detection = 0x00049168,
            Helmet_of_Life_Seeing = 0x0004935D,
            Helmet_of_Life_Sight = 0x0004937B,
            Helmet_of_Magicka_Resistance = 0x0004936D,
            Helmet_of_Night_Eye = 0x00049175,
            Helmet_of_Power = 0x00049522,
            Helmet_of_Spell_Resistence = 0x0004F403,
            Helmet_of_the_Apprentice = 0x0004915D,
            Helmet_of_the_Deep = 0x00049664,
            Helmet_of_the_Drowned = 0x000496A5,
            Helmet_of_the_Flood = 0x0002C24E,
            Helmet_of_the_Hunter = 0x0004F3F1,
            Helmet_of_the_Lemur = 0x000489AC,
            Helmet_of_the_Mage = 0x0004789E,
            Helmet_of_the_Mind = 0x00047890,
            Helmet_of_the_Owl = 0x000489A0,
            Helmet_of_the_Scout = 0x0004914B,
            Helmet_of_the_Sentinel = 0x00049666,
            High_Rock_Helmet = 0x00049503,
            Horselord__s_Cuirass = 0x0004F3FB,
            Huntsman_Gauntlets = 0x000A577B,
            Ice_Cuirass = 0x000478A0,
            Imperial_Dragon_Boots = 0x000ADD4E,
            Imperial_Dragon_Boots_ = 0x000ADDA3,
            Imperial_Dragon_Cuirass = 0x000ADD50,
            Imperial_Dragon_Cuirass_ = 0x000ADDAA,
            Imperial_Dragon_Gauntlets = 0x000ADD51,
            Imperial_Dragon_Gauntlets_ = 0x000ADE26,
            Imperial_Dragon_Greaves = 0x000ADD52,
            Imperial_Dragon_Greaves_ = 0x000ADE27,
            Imperial_Dragon_Helmet = 0x000ADDA2,
            Imperial_Dragon_Helmet_ = 0x000ADE2A,
            Imperial_Horseman_Helmet = 0x0009416A,
            Imperial_Palace_Cuirass = 0x00064F75,
            Imperial_Watch_Boots = 0x0018AE4B,
            Imperial_Watch_Cuirass = 0x0018AE4C,
            Imperial_Watch_Gauntlets = 0x0018AE4D,
            Imperial_Watch_Greaves = 0x0018AE4E,
            Imperial_Watch_Helmet = 0x0018AE4F,
            Imperial_Watch_Shield = 0x000653F7,
            Infiltrator__s_Gauntlets = 0x00049172,
            Inquisitor__s_Gauntlets = 0x0002C214,
            Inquisitor__s_Helmet = 0x0002C212,
            Insulated_Shield = 0x000493AA,
            Iron_Boots = 0x0001C6CF,
            Iron_Cuirass = 0x0001C6D1,
            Iron_Gauntlets = 0x0001C6D2,
            Iron_Greaves = 0x0001C6D0,
            Iron_Helmet = 0x000733EB,
            Iron_Helmet_ = 0x0001C6CE,
            Iron_Mountain_Shield = 0x00049681,
            Iron_Shield = 0x000733ED,
            Iron_Shield_ = 0x000352C1,
            Ironheart_Cuirass = 0x00049502,
            Knights_of_the_Thorn_Shield = 0x0012DD1D,
            Kvatch_Cuirass = 0x000C49BF,
            Kvatch_Cuirass_ = 0x000C49C0,
            Kvatch_Cuirass__ = 0x000C49C1,
            Kvatch_Cuirass___ = 0x000C49C2,
            Kvatch_Cuirass____ = 0x000C49C3,
            Kvatch_Cuirass_____ = 0x000C49C4,
            Kvatch_Cuirass______ = 0x0002767A,
            Kvatch_Shield = 0x000352CD,
            Leather_Boots = 0x0002319B,
            Leather_Bracer = 0x000229A9,
            Leather_Cuirass = 0x0002319A,
            Leather_Cuirass_ = 0x0000C1D6,
            Leather_Gauntlets = 0x00023199,
            Leather_Greaves = 0x00023198,
            Leather_Greaves_ = 0x0015985E,
            Leather_Helmet = 0x0002319C,
            Leather_Helmet_ = 0x000733E3,
            Leather_Shield = 0x00025058,
            Leather_Shield_ = 0x000977BF,
            Legion_Boots = 0x00028ADE,
            Legion_Cuirass = 0x00028ADF,
            Legion_Gauntlets = 0x00028AE0,
            Legion_Greaves = 0x00028AE1,
            Legion_Helmet = 0x00028AE2,
            Legion_Shield = 0x000352D3,
            Leyawiin_Cuirass = 0x0002767B,
            Leyawiin_Shield = 0x000352CF,
            Light_Iron_Shield = 0x00032D49,
            Light_Iron_Shield_ = 0x000733E0,
            Light_Raiment_of_Valor = 0x0003563E,
            Lightfoot_Boots = 0x00049508,
            Lightning_Run_Boots = 0x00049399,
            Lightning_Strider_Boots = 0x0004F405,
            Lightning_Strider_Shield = 0x0002ADCF,
            Lion__s_Paw_Gauntlets = 0x0004F3FA,
            Mage_Fighter__s_Greaves = 0x00049683,
            Magebane_Greaves = 0x0002C247,
            Magehunter__s_Helmet = 0x00049178,
            Magekiller_Greaves = 0x000496A4,
            Mage__s_Helmet = 0x0004916D,
            Mageslayer__s_Helmet = 0x0002C235,
            Magnifying_Gauntlets = 0x000A5782,
            Marathon_Greaves = 0x0004916B,
            Masque_of_Clavicus_Vile = 0x000228EE,
            Master_Forge_Gauntlets = 0x0004966E,
            Merchant__s_Cuirass = 0x00049171,
            Mercury_Shield = 0x00049678,
            Miner__s_Boots = 0x0004966F,
            Mirror_Shield = 0x0004789F,
            Mithril_Boots = 0x0002C0FC,
            Mithril_Cuirass = 0x0002C0FE,
            Mithril_Gauntlets = 0x0002C100,
            Mithril_Greaves = 0x0002C102,
            Mithril_Helmet = 0x0002C104,
            Mithril_Helmet_ = 0x000733E4,
            Mithril_Shield = 0x000352BF,
            Mithril_Shield_ = 0x000977C0,
            Monkeypants = 0x000CA112,
            Monolithic_Shield = 0x0002C244,
            Moonlight_Gauntlets = 0x0004F3FE,
            Moonlight_Shield = 0x0004F3FD,
            Moonshadow_Gauntlets = 0x0002C224,
            Moonshadow_Shield = 0x0002C226,
            Mountaineer__s_Gauntlets = 0x0004967A,
            Mountaineer__s_Shield = 0x00049673,
            Mudcrab_Shield = 0x000489B2,
            Nighteye_Helmet = 0x00049368,
            Nimble_Greaves = 0x0004935F,
            Nord_Gauntlets = 0x00049177,
            Nordslayer_Gauntlets = 0x0002C233,
            Ogre_Skin_Shield = 0x0002ADCE,
            Orcish_Boots = 0x0003634D,
            Orcish_Cuirass = 0x0003634F,
            Orcish_Gauntlets = 0x0003634C,
            Orcish_Greaves = 0x0003634E,
            Orcish_Helmet = 0x000733F2,
            Orcish_Helmet_ = 0x0003634B,
            Orcish_Shield = 0x00036350,
            Orcish_Shield_ = 0x000977BD,
            Outrider_Shield = 0x00049163,
            Peak_Climber__s_Boots = 0x0004968F,
            Pinarus___Iron_Cuirass = 0x000CBD4F,
            Pit_Boots = 0x00008A78,
            Pit_Cuirass = 0x00008A76,
            Pit_Gauntlets = 0x00008A79,
            Pit_Greaves = 0x00008A77,
            Pit_Helmet = 0x00008A7A,
            Quartz_Cuirass = 0x000493A3,
            Quicksilver_Boots = 0x000CA113,
            Rasheda__s_Special = 0x000CA114,
            Reflecting_Helmet = 0x000493A2,
            Retributive_Justice = 0x00049511,
            Riverwalking_Boots = 0x00049164,
            Rough_Leather_Boots = 0x0015985B,
            Rough_Leather_Cuirass = 0x0015985C,
            Rough_Leather_Gauntlets = 0x0015985D,
            Rough_Leather_Helmet = 0x0015985F,
            Rough_Leather_Shield = 0x00047AC8,
            Royal_Cuirass = 0x00049516,
            Rugged_Cuirass = 0x00049501,
            Ruined_Akaviri_Shield = 0x0001C161,
            Rusty_Iron_Cuirass = 0x000661C1,
            Rusty_Iron_Gauntlets = 0x000661C2,
            Rusty_Iron_Greaves = 0x000661C3,
            Rusty_Iron_Helmet = 0x000661C4,
            Rusty_Iron_Shield = 0x000661C5,
            Salamander_Scale_Shield = 0x0004899C,
            Salubrious_Cuirass = 0x0004F3F6,
            Savage_Gauntlets = 0x00049689,
            Saviour__s_Hide = 0x00027107,
            Seastrider__s_Helmet = 0x0002ADD1,
            Shaman_Helmet = 0x0004968D,
            Shield_of_Animus = 0x0002C228,
            Shield_of_Elsweyr = 0x0004950E,
            Shield_of_Frost = 0x00049157,
            Shield_of_Grounding = 0x00053D79,
            Shield_of_Justice = 0x00053D76,
            Shield_of_Lightning = 0x000496A3,
            Shield_of_Mirrors = 0x000493A0,
            Shield_of_Nature__s_Vengence = 0x00049385,
            Shield_of_Reflection = 0x0004F400,
            Shield_of_Retribution = 0x00049677,
            Shield_of_Retributive_Strike = 0x0004F3FF,
            Shield_of_Returning = 0x0004936A,
            Shield_of_Shattering = 0x0004939F,
            Shield_of_Storms = 0x00049375,
            Shield_of_Summer = 0x00049366,
            Shield_of_the_Divine = 0x0004965A,
            Shield_of_the_Elements = 0x0004789D,
            Shield_of_the_Empire = 0x00049510,
            Shield_of_the_Flame = 0x00053D74,
            Shield_of_the_North = 0x00053D75,
            Shield_of_the_Pathfinder = 0x00049675,
            Shield_of_the_Red_Mountain = 0x0004935E,
            Shield_of_the_Sun = 0x00049658,
            Shield_of_the_Tower = 0x00049519,
            Shield_of_the_Turtle = 0x00053D78,
            Shield_of_the_Unbroken = 0x000496A2,
            Shield_of_the_Undefeated = 0x00049663,
            Shield_of_the_Unrelenting = 0x00049690,
            Shield_of_the_Untamed = 0x00049698,
            Shield_of_Vengence = 0x0002C227,
            Shield_of_Vindication = 0x0004915A,
            Shield_of_Winter_Solstice = 0x00049383,
            Shrouded_Armor = 0x000347F7,
            Shrouded_Hood = 0x000347F4,
            Silica_Boots = 0x000493AC,
            Skingrad_Cuirass = 0x0001DC4B,
            Skingrad_Shield = 0x000352D1,
            Skyrim_Gauntlets = 0x00049513,
            Slavemaster__s_Greaves = 0x0002C239,
            Smelter_Shield = 0x000494FF,
            Smuggler__s_Boots = 0x000A5783,
            Sniper_Gauntlets = 0x00049153,
            Snowblind_Gauntlets = 0x00049694,
            Snowblind_Shield = 0x00049695,
            Solid_Shield = 0x00049374,
            Spell_Breaker = 0x000897C2,
            Spellbinder_Greaves = 0x0002ADD0,
            Spellblocker_Shield = 0x0004915B,
            Spelltaker__s_Greaves = 0x00049391,
            Spiked_Shield = 0x00049697,
            Stalwart_Cuirass = 0x0004F3F4,
            Steel_Boots = 0x000229A5,
            Steel_Cuirass = 0x000229A2,
            Steel_Gauntlets = 0x0001C6D8,
            Steel_Greaves = 0x000229A3,
            Steel_Helmet = 0x000229A4,
            Steel_Helmet_ = 0x0006AA9B,
            Steel_Helmet__ = 0x000733EC,
            Steel_Shield = 0x00023923,
            Steel_Shield_ = 0x000977C2,
            Storm_Stomper_Boots = 0x0004969E,
            Stormhammer_Boots = 0x0004967D,
            Stormhammer_Shield = 0x00049682,
            Stormlord__s_Shield = 0x0002C245,
            Stormrider_Boots = 0x0004938C,
            Stormrider_Shield = 0x00049390,
            Sunburst_Gauntlets = 0x0004950D,
            Swamp_Boots = 0x00049687,
            Sylvan_Barkshield = 0x0004938F,
            Sylvan_Scout_Boots = 0x0004937E,
            Tempered_Greaves = 0x0004939D,
            The_Gray_Aegis = 0x0002996A,
            Thiefhunter__s_Gauntlets = 0x0004951C,
            Threefold_Shield = 0x00049386,
            Tiger_Fang_Shield = 0x000489A9,
            Tireless_Greaves = 0x00049361,
            Tireless_Greaves_ = 0x0004F3F5,
            Tower_of_the_Nine = 0x000CA116,
            Umbra__s_Ebony_Boots = 0x0000A307,
            Umbra__s_Ebony_Boots_ = 0x0000A30D,
            Umbra__s_Ebony_Cuirass = 0x0000A308,
            Umbra__s_Ebony_Cuirass_ = 0x0000A30E,
            Umbra__s_Ebony_Gauntlets = 0x0000A309,
            Umbra__s_Ebony_Gauntlets_ = 0x0000A30F,
            Umbra__s_Ebony_Greaves = 0x0000A30A,
            Umbra__s_Ebony_Greaves_ = 0x0000A310,
            Umbra__s_Ebony_Helmet = 0x0000A30B,
            Umbra__s_Ebony_Helmet_ = 0x0000A311,
            Umbra__s_Ebony_Shield = 0x0000A30C,
            Umbra__s_Ebony_Shield_ = 0x0000A312,
            Unyielding_Cuirass = 0x0004936E,
            Valdemar__s_Shield = 0x00187BB9,
            Valdemar__s_Shield_ = 0x00187BBA,
            Valdemar__s_Shield__ = 0x00187BBB,
            Valdemar__s_Shield___ = 0x00187BBC,
            Viperbane_Cuirass = 0x00036355,
            Vvardenfell_Trader__s_Cuirass = 0x000A577D,
            Warforger__s_Gauntlets = 0x0002C21D,
            Warmage__s_Helmet = 0x0004966D,
            Warmaster_Gauntlets = 0x0004968E,
            Waterwalking_Boots = 0x0004917F,
            Weaponbane_Cuirass = 0x0004967B,
            Weaponward_Cuirass = 0x0004915E,
            Winterbane_Shield = 0x0004F3FC,
            Witchfinder__s_Shield = 0x0004965B,
            Witchhunter_Helmet = 0x0004965E,
            Wizard__s_Helmet = 0x00049150,
            Arrow_of_Blizzards = 0x0004BF0B,
            Arrow_of_Brilliance = 0x00008A4D,
            Arrow_of_Burning = 0x0004BEFB,
            Arrow_of_Cleansing = 0x000CD53F,
            Arrow_of_Cold = 0x0004BF02,
            Arrow_of_Cowardice = 0x0003DFDB,
            Arrow_of_Discord = 0x0003BF67,
            Arrow_of_Dispel = 0x0004BF11,
            Arrow_of_Drain_Magicka = 0x0004BF00,
            Arrow_of_Embers = 0x0004BF10,
            Arrow_of_Extrication = 0x00022DB5,
            Arrow_of_Fatigue = 0x0003DFD6,
            Arrow_of_Fear = 0x0003DFD8,
            Arrow_of_Fire = 0x0004BF07,
            Arrow_of_Flames = 0x0004BF04,
            Arrow_of_Freezing = 0x0004BF05,
            Arrow_of_Frost = 0x0004BEF9,
            Arrow_of_Harm = 0x0003DFD7,
            Arrow_of_Hexing = 0x00159676,
            Arrow_of_Illumination = 0x00008A4C,
            Arrow_of_Immolation = 0x000CD542,
            Arrow_of_Jinxing = 0x0004BEFF,
            Arrow_of_Jolts = 0x0004BEFD,
            Arrow_of_Light = 0x00008A4F,
            Arrow_of_Lightning = 0x0004BF0C,
            Arrow_of_Misery = 0x0003DFD9,
            Arrow_of_Numbing = 0x0004BEFC,
            Arrow_of_Qualms = 0x0003DFDA,
            Arrow_of_Savage_Frost = 0x000CD540,
            Arrow_of_Scorching = 0x0004BF01,
            Arrow_of_Shocking = 0x0004BF03,
            Arrow_of_Silence = 0x0003DFDC,
            Arrow_of_Sparks = 0x0004BEFA,
            Arrow_of_Stillness = 0x0003DFDD,
            Arrow_of_Storm_Strike = 0x000CD543,
            Arrow_of_Storms = 0x0004BF0F,
            Arrow_of_Sunlight = 0x0000BAD9,
            Arrow_of_the_Blaze = 0x0004BF0A,
            Arrow_of_the_Dynamo = 0x0004BF09,
            Arrow_of_the_Glacier = 0x0004BF08,
            Arrow_of_the_Inferno = 0x0004BF0D,
            Arrow_of_the_North_Winds = 0x000CD545,
            Arrow_of_Voltage = 0x0004BF06,
            Arrow_of_Winter = 0x0004BF0E,
            Arrow_of_Withering = 0x0003CB6C,
            Daedric_Arrow = 0x0001EFD3,
            Dremora_Barbed_Arrow = 0x000872A7,
            Dremora_Broadhead_Arrow = 0x000872A6,
            Dremora_Field_Arrow = 0x000872A5,
            Dwarven_Arrow = 0x00022BE2,
            Ebony_Arrow = 0x0001EFD5,
            Elven_Arrow = 0x000229C0,
            Flare_Arrow = 0x00008A4E,
            Glass_Arrow = 0x00022BE1,
            Hatreds_Soul_Arrow = 0x00014EB3,
            Iron_Arrow = 0x00017829,
            Magebane_Arrow = 0x000CD544,
            Rose_of_Sithis = 0x0003266D,
            Silver_Arrow = 0x0001EFD4,
            Steel_Arrow = 0x000229C1,
            Stormcall_Arrow = 0x000CD541,
            Evening_Star_v12 = 0x000243DB,
            First_Seed_v3 = 0x000243D7,
            Frostfall_v10 = 0x000243F5,
            Hearth_Fire_v9 = 0x000243F4,
            Last_Seed_v8 = 0x00024547,
            MidYear_v6 = 0x00024402,
            Morning_Star_v1 = 0x000243E4,
            Rain__s_Hand_v4 = 0x0002453F,
            Second_Seed_v5 = 0x0002454D,
            Sun__s_Dawn_v2 = 0x00024538,
            Sun__s_Dusk_v11 = 0x000243D3,
            Sun__s_Height_v7 = 0x00024534,
            A_Bloody_Journal = 0x0002FF32,
            A_Children__s_Anuad = 0x00024576,
            A_Dance_in_Fire_v_7 = 0x00024536,
            A_Dance_in_Fire_v1 = 0x000243CB,
            A_Dance_in_Fire_v2 = 0x000243EA,
            A_Dance_in_Fire_v3 = 0x000243DF,
            A_Dance_in_Fire_v4 = 0x000243CC,
            A_Dance_in_Fire_v5 = 0x00024411,
            A_Dance_in_Fire_v6 = 0x00024535,
            A_Game_at_Dinner = 0x000243CF,
            A_Hypothetical_Treachery = 0x000243F9,
            A_Less_Rude_Song = 0x00024569,
            A_Life_of_Uriel_Septim_VII = 0x000AA07D,
            A_New_Guild_for_Fighters = 0x00098682,
            A_Poorly_Scrawled_Note = 0x000C45B3,
            Absorb_Agility = 0x000849F7,
            Absorb_Endurance = 0x000849D2,
            Absorb_Fatigue = 0x000849D8,
            Absorb_Health = 0x000849D9,
            Absorb_Intelligence = 0x000849D3,
            Absorb_Luck = 0x000849D4,
            Absorb_Magicka = 0x0000A94E,
            Absorb_Major_Magicka = 0x0000A94F,
            Absorb_Maximal_Magicka = 0x0000A950,
            Absorb_Minimal_Magicka = 0x000849DD,
            Absorb_Minor_Magicka = 0x0000A94D,
            Absorb_Skill_____Acrobatics = 0x000849DE,
            Absorb_Skill_____Alchemy = 0x000849DF,
            Absorb_Skill_____Alteration = 0x000849E0,
            Absorb_Skill_____Armorer = 0x000849E1,
            Absorb_Skill_____Athletics = 0x000849E2,
            Absorb_Skill_____Blade = 0x000849E3,
            Absorb_Skill_____Block = 0x000849E4,
            Absorb_Skill_____Blunt = 0x000849E5,
            Absorb_Skill_____Conjuration = 0x000849E6,
            Absorb_Skill_____Destruction = 0x000849E7,
            Absorb_Skill_____Hand_to_Hand = 0x000849E8,
            Absorb_Skill_____Heavy_Armor = 0x000849E9,
            Absorb_Skill_____Illusion = 0x000849EA,
            Absorb_Skill_____Light_Armor = 0x000849ED,
            Absorb_Skill_____Marksman = 0x000849EC,
            Absorb_Skill_____Mercantile = 0x000849EB,
            Absorb_Skill_____Mysticism = 0x000849EE,
            Absorb_Skill_____Restoration = 0x000849EF,
            Absorb_Skill_____Security = 0x000849F0,
            Absorb_Skill_____Sneak = 0x000849F1,
            Absorb_Skill_____Speechcraft = 0x000849F2,
            Absorb_Speed = 0x000849D5,
            Absorb_Strength = 0x000849D6,
            Absorb_Willpower = 0x000849D7,
            Adamus_Phillida_Slain = 0x0006D6ED,
            Advances_in_Lock_Picking = 0x00073A65,
            Aegis = 0x0008991F,
            Aevar_Stone___Singer = 0x00024543,
            Agnar__s_Journal = 0x000C55DF,
            Ahzirr_Traajijazeri = 0x000243FE,
            Akaviri_Diary_Translation = 0x0001C162,
            Alluring_Gaze = 0x00084AEF,
            Alval_Uvani__s_Schedule = 0x00066200,
            Amantius_Allectus___Diary = 0x000355ED,
            Ancotar__s_Journal = 0x00185934,
            Andre__s_Letter = 0x000C56D9,
            Annal_of_the_Fire_Nexus = 0x000CA11F,
            Anvil_Tarts_Thwarted = 0x00066CD5,
            Aquatic_Adaptation = 0x0008ADA8,
            Aquatic_Evolution = 0x0008ADAA,
            Aquatic_Transcendence = 0x0008ADAB,
            Arcana_Restored = 0x00024584,
            Arctic_Blow = 0x000888EE,
            Assassination = 0x000274EE,
            Ayleid_Reference_Text = 0x0003353B,
            Azura_and_the_Box = 0x0002453B,
            Battle_of_Sancre_Tor = 0x00073A61,
            Beast_of_Burden = 0x000888B9,
            Before_the_Ages_of_Man = 0x00073A63,
            Beggar = 0x000243E1,
            Beggar_Prince = 0x0001FB53,
            Beguile = 0x000150B9,
            Beguiling_Touch = 0x00084AF1,
            Bible_of_the_Deep_Ones = 0x000C7B33,
            Biography_of_Barenziah_v_1 = 0x00024550,
            Biography_of_Barenziah_v_2 = 0x00024551,
            Biography_of_Barenziah_v_3 = 0x00024552,
            Biography_of_Barenziah_v_3_ = 0x00024553,
            Biography_of_the_Wolf_Queen = 0x0002454B,
            Blazing_Spear = 0x000888C0,
            Blizzard = 0x000888E5,
            Bound_Axe_ = 0x000849F3,
            Bound_Boots_ = 0x000849F4,
            Bound_Bow_ = 0x000849F5,
            Bound_Cuirass_ = 0x000849F6,
            Bound_Dagger_ = 0x000849F8,
            Bound_Gauntlets_ = 0x000849F9,
            Bound_Greaves_ = 0x000849FA,
            Bound_Helmet__ = 0x000849FB,
            Bound_Mace_ = 0x000849FC,
            Bound_Shield_ = 0x000849FD,
            Bound_Sword_ = 0x000849FE,
            Brenus_Astis___Journal = 0x0002A577,
            Brief_History_of_the_Empire_v_1 = 0x00024554,
            Brief_History_of_the_Empire_v_2 = 0x00024555,
            Brief_History_of_the_Empire_v_3 = 0x00024556,
            Brief_History_of_the_Empire_v_4 = 0x00024557,
            Burdening_Touch = 0x00084AE2,
            Burning_Touch = 0x000888C3,
            Calcinator_Treatise = 0x00073A5F,
            Calming_Touch = 0x00084AEA,
            Candlelight = 0x000898EE,
            Chameleon = 0x00084AEB,
            Cherim__s_Heart_of_Anequina = 0x000243DC,
            Cheydinhal_Heir_Saved = 0x00066CD3,
            Children_of_the_Sky = 0x00024587,
            Chimarvamidium = 0x00024403,
            Chronicle_of_Sacrifice = 0x000CA11D,
            Cleansing_of_the_Fane = 0x0002C8DD,
            Cloak = 0x00084AED,
            Cold_Touch = 0x000888EB,
            Command_Creature = 0x00084AF3,
            Command_Humanoid = 0x00084AF5,
            Commanding_Touch = 0x00084AF7,
            Consume_Health = 0x000849DB,
            Convalescence = 0x000C7666,
            Corrode_Armor = 0x00088891,
            Corrode_Weapon = 0x00088893,
            Crumpled_Note = 0x00074A8A,
            Crumpled_Piece_of_Paper = 0x000624D1,
            crumpled_piece_of_paper = 0x000624D2,
            Crumpled_Piece_of_Paper_ = 0x000AA07E,
            Crumpled_Piece_of_Paper__ = 0x000AA07F,
            Crumpled_Piece_of_Paper___ = 0x000AA080,
            Crumpled_Piece_of_Paper____ = 0x000AA081,
            Crumpled_Piece_of_Paper_____ = 0x000AA082,
            Crumpled_Piece_of_Paper______ = 0x000AA083,
            Crumpled_Piece_of_Paper_______ = 0x0008DC4A,
            Crumpled_Piece_of_Paper________ = 0x0008DC4C,
            Cure_Disease = 0x00087293,
            Cure_Paralysis = 0x00087294,
            Cure_Poison = 0x00087295,
            Damage_Agility = 0x00087296,
            Damage_Fatigue = 0x0008729D,
            Damage_Intelligence = 0x00087298,
            Damage_Luck = 0x00087299,
            Damage_Speed = 0x0008729A,
            Damage_Strength = 0x0008729B,
            Damage_Willpower = 0x0008729C,
            Darkest_Darkness = 0x00024564,
            Dar___Ma__s_Diary = 0x000280A9,
            Daughter_of_the_Niben = 0x000243D4,
            Daylight = 0x000898F0,
            De_Rerum_Dirennis = 0x000243D2,
            Dead_Drop_Orders_1 = 0x0002FB3E,
            Dead_Drop_Orders_2 = 0x00031B2A,
            Dead_Drop_Orders_3 = 0x0002FB3B,
            Dead_Drop_Orders_4 = 0x00030146,
            Dead_Drop_Orders_5 = 0x00030194,
            Dead_Drop_Orders_6 = 0x00030195,
            Dead_Drop_Orders_7 = 0x000301AD,
            Dead_Drop_Orders_8 = 0x0000396B,
            Death_Blow_of_Abernanit = 0x000243E8,
            Debilitate = 0x000898FB,
            Deed_to_Benirus_Manor = 0x0000A1BC,
            Defend = 0x0008991C,
            Devour_Health = 0x000849DC,
            Diary_of_Springheel_Jak = 0x000152FC,
            Dire_Enervation = 0x000888A4,
            Dire_Sever_Magicka = 0x00088885,
            Dire_Wound = 0x000888A7,
            Dirty_Scroll = 0x0006BFAC,
            Disintegrate_Armor = 0x00088892,
            Disintegrate_Weapon = 0x00088894,
            Dismiss_Undead = 0x0008ADA6,
            Dispel = 0x00088895,
            Dispel_Other = 0x00088899,
            Divining_the_Elder_Scolls = 0x0000A254,
            Document_of_Purile_Banter = 0x000CA11E,
            Dominate_Creature = 0x00084AF4,
            Dominate_Humanoid = 0x00084AF6,
            Dominating_Touch = 0x00084AF8,
            Draconis_Gift_List = 0x00002DAB,
            Drain_Skill_____Alteration = 0x000888AD,
            Drain_Skill_____Blade = 0x000888AE,
            Drain_Skill_____Block = 0x0008DC82,
            Drain_Skill_____Blunt = 0x0008DC81,
            Drain_Skill_____Conjuration = 0x000888AF,
            Drain_Skill_____Destruction = 0x000888B0,
            Drain_Skill_____Hand_to_Hand = 0x000888B1,
            Drain_Skill_____Heavy_Armor = 0x000888B2,
            Drain_Skill_____Heavy_Armor_ = 0x0008DC83,
            Drain_Skill_____Illusion = 0x000888B3,
            Drain_Skill_____Marksman = 0x000888B4,
            Drain_Skill_____Mysticism = 0x0008DC84,
            Drain_Skill_____Restoration = 0x000888B5,
            Drain_Skill_____Sneak = 0x0008DC85,
            Dwemer_History_and_Culture = 0x00022B17,
            Earana__s_Notes = 0x000277AE,
            Ease_Burden = 0x000888B6,
            Elder_Scroll = 0x00022DB0,
            Electric_Shell = 0x0008992C,
            Electric_Shield = 0x0008992D,
            Electric_Touch = 0x00089929,
            Electrocution = 0x00089927,
            Elevate_Magicka = 0x00008874,
            Encumbering_Touch = 0x00084AE4,
            Enthralling_Presence = 0x00084AEE,
            Entropic_Bolt = 0x0008729E,
            Entropic_Touch = 0x00088881,
            Eyes_of_Eventide = 0x000898F5,
            Eyes_of_Midnight = 0x000898F6,
            Fall_of_the_Snow_Prince = 0x00024544,
            Father_Of_The_Niben = 0x00024530,
            Fearful_Gaze = 0x00088888,
            Feyfolken_I = 0x000243E7,
            Feyfolken_II = 0x000243ED,
            Feyfolken_III = 0x000243F1,
            Fighters_Guild_History_1st_Ed = 0x000A915C,
            Fingers_of_the_Mountain = 0x00001101,
            Fire_and_Darkness = 0x000243E5,
            Fire_Ball = 0x000888BA,
            Fire_Shield = 0x000888C9,
            Fire_Storm = 0x000888BB,
            Five_Songs_of_King_Wulfharth = 0x00024588,
            Flame_Shield = 0x000888C8,
            Flame_Tempest = 0x000888BC,
            Flame_Touch = 0x000888C4,
            Flare = 0x000888BE,
            Flash_Bolt = 0x000888BF,
            Folded_Page = 0x000624D3,
            Followers_of_the_Gray_Fox = 0x00024595,
            Forged_List_of_Candidates = 0x0000C229,
            Fortify_Health = 0x000888DB,
            Fortify_Magicka = 0x000888DD,
            Fragment_____On_Artaeum = 0x00024589,
            Fragment_____Song_of_Hrormir = 0x0000A256,
            Frenzy = 0x000888DF,
            Frontier_Conquest = 0x00024566,
            Frost_Bolt = 0x000888E8,
            Frost_Shell = 0x000888EF,
            Frost_Touch = 0x000888EC,
            Fundaments_of_Alchemy = 0x00024567,
            Galerion_The_Mystic = 0x00024568,
            Gelebourne__s_Journal = 0x00038447,
            Ghost_Walk = 0x000898EA,
            Gills = 0x0008ADA9,
            Glacial_Wall = 0x000888F1,
            Glarthir__s_Notes = 0x000831B0,
            Glarthir__s_Notes_ = 0x000831B1,
            Glarthir__s_Notes__ = 0x000831B2,
            Glarthir__s_Notes___ = 0x000831B3,
            Glarthir__s_Notes____ = 0x000831B4,
            Glarthir__s_Notes_____ = 0x000831B5,
            Glarthir__s_Notes______ = 0x000831B6,
            Glarthir__s_Notes_______ = 0x000831B7,
            Glarthir__s_Notes________ = 0x000831B8,
            Glarthir__s_Notes_________ = 0x000831B9,
            Glories_and_Laments = 0x0000A2B3,
            Gods_and_Worship = 0x0002456F,
            Grantham_Blakeley__s_Map = 0x000366B3,
            Grasp_of_Terror = 0x0008888B,
            Gray_Fox_Unmasked = 0x0006D6EF,
            Gray_Fox_Man_or_Myth = 0x0006D6EE,
            Greater_Convalescence = 0x000C7667,
            Greater_Detect_Life = 0x0008888F,
            Greater_Dispel = 0x00088896,
            Greater_Dispel_Other = 0x0008889A,
            Greater_Fortify_Fatigue = 0x000888DA,
            Greater_Fortify_Health = 0x000888DC,
            Greater_Fortify_Magicka = 0x000888DE,
            Greater_Restore_Agility = 0x00089906,
            Greater_Restore_Endurance = 0x00089908,
            Greater_Restore_Intelligence = 0x0008990A,
            Greater_Restore_Luck = 0x0008990C,
            Greater_Restore_Personality = 0x0008990E,
            Greater_Restore_Speed = 0x00089910,
            Greater_Restore_Strength = 0x00089912,
            Greater_Restore_Willpower = 0x00089914,
            Greater_Soul_Trap = 0x0008AD94,
            Greater_Spell_Reflection = 0x00089902,
            Greatest_Painter_Safe = 0x00066CD4,
            Guard = 0x0008991D,
            Guide_to_Anvil = 0x0002455B,
            Guide_to_Bravil = 0x0002455F,
            Guide_to_Bruma = 0x0002455E,
            Guide_to_Cheydinhal = 0x00024561,
            Guide_to_Chorrol = 0x0002455D,
            Guide_to_Leyawiin = 0x00024560,
            Guide_to_Skingrad = 0x0002455C,
            Guide_to_the_Imperial_City = 0x00024562,
            Hail_Storm = 0x000888E3,
            Hailstone = 0x000888E7,
            Hallgerd__s_Tale = 0x00024401,
            Handbill = 0x0006B5C8,
            Handbill_ = 0x0006B5C9,
            Handbill__ = 0x0006B5CA,
            Handbill___ = 0x0006B5CB,
            Handbill____ = 0x0006B5CC,
            Handbill_____ = 0x0006B5CD,
            Handbill______ = 0x0006B5CE,
            Handbill_______ = 0x0006B5CF,
            Handbill________ = 0x0006B5D0,
            Handbill_________ = 0x0006B5D1,
            Handbill__________ = 0x0006B5D2,
            Handbill___________ = 0x0006B5D3,
            Handbill____________ = 0x0006B5D4,
            Handbill_____________ = 0x0006B5D5,
            Handbill______________ = 0x0006B5D6,
            Handbill_______________ = 0x0006B5D7,
            Handbill________________ = 0x0006B5D8,
            Handbill_________________ = 0x0006B5D9,
            Handbill__________________ = 0x00071765,
            Handbill___________________ = 0x00071766,
            Handbill____________________ = 0x00071767,
            Handbill_____________________ = 0x00071768,
            Handbill______________________ = 0x00071769,
            Handbill_______________________ = 0x0007176A,
            Handbill________________________ = 0x0007176B,
            Handbill_________________________ = 0x0007176C,
            Handbill__________________________ = 0x0007176D,
            Handbill___________________________ = 0x0007176E,
            Handbill____________________________ = 0x0007176F,
            Handbill_____________________________ = 0x00071770,
            Handbill______________________________ = 0x00071771,
            Handbill_______________________________ = 0x00071772,
            Handbill________________________________ = 0x00071773,
            Handbill_________________________________ = 0x00071774,
            Handbill__________________________________ = 0x00071775,
            Handbill___________________________________ = 0x00071776,
            Handbill____________________________________ = 0x00071777,
            Handbill_____________________________________ = 0x00071778,
            Handbill______________________________________ = 0x00071779,
            Handbill_______________________________________ = 0x0007177A,
            Handbill________________________________________ = 0x0007177B,
            Handbill_________________________________________ = 0x0007177C,
            Handbill__________________________________________ = 0x0007177D,
            Handbill___________________________________________ = 0x0007177E,
            Handbill____________________________________________ = 0x0007177F,
            Handbill_____________________________________________ = 0x00071780,
            Handbill______________________________________________ = 0x00071781,
            Handbill_______________________________________________ = 0x00071782,
            Handbill________________________________________________ = 0x00071783,
            Handbill_________________________________________________ = 0x00071784,
            Handbill__________________________________________________ = 0x00071785,
            Handbill___________________________________________________ = 0x00071786,
            Handbill____________________________________________________ = 0x00071787,
            Handbill_____________________________________________________ = 0x00071788,
            Handbill______________________________________________________ = 0x00071789,
            Handbill_______________________________________________________ = 0x0007178A,
            Handbill________________________________________________________ = 0x0007178B,
            Handwritten_Note = 0x000C7631,
            Handwritten_Note_ = 0x0018BC65,
            Handwritten_Note__ = 0x0008DC48,
            Handwritten_Note___ = 0x000908C7,
            Hanging_Gardens = 0x0002458A,
            Hastily_Scrawled_Note = 0x00033DEC,
            Heat_Burst = 0x000888C1,
            Heat_Shell = 0x000888C7,
            Heavy_Armor_Repair = 0x00073A68,
            Heroic_Touch = 0x00089901,
            Heroism = 0x000898FF,
            Hiding_with_the_Shadow = 0x0001FB52,
            Hindering_Touch = 0x00084AE3,
            History_of_Lock_Picking = 0x0001FB51,
            History_of_the_Fighters_Guild = 0x00024405,
            House_Balcony_Area = 0x000B1626,
            House_Balcony_Upgrade = 0x000B1627,
            House_Bedroom_Area = 0x000B1581,
            House_Bedroom_Area_ = 0x000B1593,
            House_Bedroom_Area__ = 0x000B15AD,
            House_Bedroom_Area___ = 0x000B15DA,
            House_Bedroom_Area____ = 0x000B1628,
            House_Den_Area = 0x000B1624,
            House_Dining_Area = 0x00090EB3,
            House_Dining_Area_ = 0x000B157F,
            House_Dining_Area__ = 0x000B158D,
            House_Dining_Area___ = 0x000B15B3,
            House_Dining_Area____ = 0x000B15D8,
            House_Dining_Area_____ = 0x000B1621,
            House_Dining_Area______ = 0x00092022,
            House_Dining_Upgrade = 0x000B15D9,
            House_Display_Case_Upgrade = 0x000B162B,
            House_Dressing_Area = 0x000B15AE,
            House_Kitchen_Area = 0x00090EB2,
            House_Kitchen_Area_ = 0x000B1583,
            House_Kitchen_Area__ = 0x000B158E,
            House_Kitchen_Area___ = 0x000B15B1,
            House_Kitchen_Area____ = 0x000B15D5,
            House_Kitchen_Area_____ = 0x000B1622,
            House_Kitchen_Area______ = 0x0009202C,
            House_Lower_Storage_Area = 0x000B1594,
            House_Lower_Wall_Hangings = 0x000B1595,
            House_Lower_Wall_Hangings_ = 0x000B15B5,
            House_Lower_Wall_Hangings__ = 0x000B15DD,
            House_Lower_Wall_Hangings___ = 0x000B162E,
            House_Middle_Wall_Hangings = 0x000B15DE,
            House_Racks_Assortment = 0x00090EB5,
            House_Reading_Area = 0x00090629,
            House_Reading_Area_ = 0x000B1580,
            House_Servants_Quarters = 0x000B15DB,
            House_Servants_Quarters_ = 0x000B162D,
            House_Sitting_Area = 0x000B15B2,
            House_Sitting_Area_ = 0x000B15D6,
            House_Sitting_Area__ = 0x000B1623,
            House_Sitting_Area___ = 0x0009202E,
            House_Storage_Area = 0x00090EB4,
            House_Storage_Area_ = 0x000B1584,
            House_Storage_Area__ = 0x000B15B0,
            House_Storage_Area___ = 0x000B162C,
            House_Storage_Area____ = 0x00092023,
            House_Study_Area = 0x000B1582,
            House_Study_Area_ = 0x000B1592,
            House_Study_Area__ = 0x000B15B4,
            House_Study_Area___ = 0x000B15D7,
            House_Study_Area____ = 0x000B162A,
            House_Suite_Area = 0x000B15DC,
            House_Upper_Hall_Area = 0x000B15AF,
            House_Upper_Hall_Area_ = 0x000B1625,
            House_Upper_Sitting_Area = 0x000B1590,
            House_Upper_Sitting_Area_ = 0x000B1629,
            House_Upper_Storage_Area = 0x000B158F,
            House_Upper_Wall_Hangings = 0x000B1591,
            House_Upper_Wall_Hangings_ = 0x000B15B6,
            House_Upper_Wall_Hangings__ = 0x000B15DF,
            House_Upper_Wall_Hangings___ = 0x000B162F,
            House_Wall_Hangings = 0x00090EB6,
            House_Wall_Hangings_ = 0x000B1585,
            House_Wall_Hangings__ = 0x00092027,
            How_Orsinium_Passed_to_Orcs = 0x00024404,
            Hush = 0x0008AD8F,
            Ice_and_Chitin = 0x0002440C,
            Ice_Blast = 0x000888EA,
            Ice_Bolt = 0x000888E9,
            Ice_Shield = 0x000888F0,
            Ice_Storm = 0x000888E4,
            Imbel_Genealogy = 0x000152FD,
            Immobilize = 0x000898FC,
            Immolating_Blast = 0x000888C2,
            Immortal_Blood = 0x000243FC,
            Incident_in_Necrom = 0x00024408,
            Inspiration = 0x000898FE,
            Inspiring_Touch = 0x00089900,
            Instructions = 0x0006B5C6,
            Instructions_____the_Gray_Cowl = 0x00014740,
            Invitation_from_Umbacano = 0x0002B458,
            Jearl__s_Orders = 0x0000C026,
            Journal_of_Claudius_Arcadia = 0x00072296,
            Journal_of_the_Lord_Lovidicus = 0x00038B2F,
            King = 0x000243F0,
            Knightfall = 0x00022E65,
            Lake_Stride = 0x0008ADAD,
            Last_Scabbard_of_Akrash = 0x000243DA,
            Leech_Health = 0x000849DA,
            Legend_of_Krately_House = 0x00024549,
            Legendary_Detect_Life = 0x00088890,
            Legendary_Dispel = 0x00088898,
            Legendary_Magicka_Drain = 0x000888AC,
            Legendary_Soul_Trap = 0x0008AD96,
            Legendary_Spell_Absorption = 0x0008AD99,
            Legendary_Spell_Reflection = 0x00089904,
            Letter = 0x000C654D,
            Letter_ = 0x0006B5C0,
            Letter__ = 0x0006B5C1,
            Letter___ = 0x0006B5C2,
            Letter____ = 0x0006B5C3,
            Letter_____ = 0x0006B5C4,
            Letter______ = 0x0006B5C5,
            Letter_______ = 0x0006B5C7,
            Letter_from_Branwen = 0x000A9668,
            Letter_to_Mother = 0x000AA084,
            Letter_to_the_Guild_of_Mages = 0x000624D4,
            Letter_to_the_Guild_of_Mages_ = 0x000624DB,
            Letter_to_the_Guild_of_Mages__ = 0x000624DC,
            Light_Armor_Repair = 0x00073A67,
            Lighten_Load = 0x000888B7,
            Lightning_Ball = 0x00089921,
            Lightning_Blast = 0x00089926,
            Lightning_Bolt = 0x00089925,
            Lightning_Grasp = 0x0008992A,
            Lightning_Storm = 0x00089922,
            Lightning_Surge = 0x0008992B,
            Lightning_Wall = 0x0008992E,
            Liminal_Bridges = 0x00073A60,
            List_of_Candidates = 0x0000C04A,
            List_of_Death = 0x00028D78,
            List_of_Death_ = 0x00028D79,
            List_of_Death__ = 0x00028D7B,
            List_of_Death___ = 0x00028D7C,
            List_of_Death____ = 0x00028D7F,
            List_of_Death_____ = 0x00028D80,
            List_of_Death______ = 0x00028D82,
            Lithnilian__s_Research_Notes = 0x00185377,
            Log_of_the_Emma_May = 0x000366B1,
            Long_Forgotten_Note = 0x000B6C0A,
            Lord_Jornibret__s_Last_Dance = 0x0002440D,
            Lost_Histories_of_Tamriel = 0x000C4A2B,
            Lost_Histories_of_Tamriel_ = 0x0000BF8C,
            Love_Letter_from_Relfina = 0x0001E084,
            Lynch__s_Instructions = 0x00015727,
            Macabre_Manifest = 0x0001D046,
            Mace_Etiquette = 0x00073A66,
            Mages_Guild_Charter = 0x00026D8B,
            Magic_from_the_Sky = 0x00078563,
            Major_Detect_Life = 0x0008888D,
            Major_Enervation = 0x000888A3,
            Major_Heal_Other = 0x000B11FF,
            Major_Magicka_Drain = 0x000888AA,
            Major_Sever_Magicka = 0x00088884,
            Major_Soul_Trap = 0x0008AD93,
            Major_Wound = 0x000888A6,
            Mannimarco_King_of_Worms = 0x000243D0,
            Manual_of_Armor = 0x000AA288,
            Manual_of_Arms = 0x0002456A,
            Manual_of_Spellcraft = 0x0002456B,
            Manual_of_Spellcraft_ = 0x0006DDA1,
            March_of_the_Sea = 0x0008ADAE,
            Master_Zoaraym__s_Tale = 0x00024400,
            Mesmerizing_Grasp = 0x00084AF2,
            Messenger__s_Diary = 0x0001C16C,
            Minor_Detect_Life = 0x0008888C,
            Minor_Enervation = 0x000888A2,
            Minor_Heal_Other = 0x000B11FE,
            Minor_Magicka_Drain = 0x000888A9,
            Minor_Soul_Trap = 0x0008AD92,
            Minor_Wound = 0x000888A5,
            Mixed_Unit_Tactics = 0x0002456C,
            Modern_Heretics = 0x00026B1D,
            Moonlight = 0x000898ED,
            More_Than_Mortal = 0x0002453A,
            Movement_Mastery = 0x0008ADA2,
            Mute = 0x0008AD90,
            Mysterious_Akavir = 0x0002456E,
            Mysterious_Note = 0x000355E0,
            Mysterious_Scroll = 0x00094085,
            Mysterium_Xarxes = 0x00005599,
            Mysterium_Xarxes_ = 0x0008B60A,
            Mystery_of_Talara_v_1 = 0x000243CE,
            Mystery_of_Talara_v_2 = 0x00024540,
            Mystery_of_Talara_v_3 = 0x000243FB,
            Mystery_of_Talara_v_4 = 0x0002440A,
            Mystery_of_Talara_v_5 = 0x00024580,
            Mysticism = 0x0002458B,
            Myth_or_Menace = 0x0001F113,
            Mythic_Dawn_Commentaries_1 = 0x00022B04,
            Mythic_Dawn_Commentaries_2 = 0x00022B05,
            Mythic_Dawn_Commentaries_3 = 0x00022B06,
            Mythic_Dawn_Commentaries_4 = 0x00022B07,
            Necromancer__s_Moon = 0x00002DD1,
            Nerevar_Moon_and_Star = 0x0002458C,
            New_Doomstones_Series = 0x0006BD47,
            New_Watch_Captain_Named = 0x0006D6F2,
            N__Gasta_Kvata_Kvakis = 0x0002458D,
            Night_Falls_on_Sentinel = 0x000243EF,
            Night_Mother_Rituals = 0x0007BEA0,
            Nirnroot_Missive = 0x0004E95E,
            Note = 0x00068C10,
            Note_ = 0x00068C11,
            Note__ = 0x00068C12,
            Note___ = 0x00068C13,
            Note____ = 0x00068C14,
            Note_____ = 0x00068C15,
            Note______ = 0x00068C16,
            Note_______ = 0x00068C17,
            Note________ = 0x0002C524,
            Note_________ = 0x000B073C,
            Note_from_First_Mate_Filch = 0x000738D8,
            Note_from_Gray_Fox = 0x0000A1B1,
            Note_from_Raminus_Polus = 0x0000A23B,
            Note_of_Bounty = 0x000B073D,
            Note_of_Exception = 0x000C6548,
            Note_to_Gwinas = 0x00022B83,
            Notes_on_Racial_Phylogeny = 0x0002453D,
            Notes_____Captain_Montrose = 0x000950E2,
            Oceanic_Journey = 0x0008ADAF,
            Oghma_Infinium = 0x000228F1,
            On_Morrowind = 0x0002456D,
            On_Oblivion = 0x0002457E,
            Open_Average_Lock = 0x000898F9,
            Open_Easy_Lock = 0x000898F8,
            Open_Hard_Lock = 0x000898FA,
            Open_Very_Easy_Lock = 0x000898F7,
            Open_Very_Hard_Lock = 0x000CBF7B,
            Oppressing_Grasp = 0x00084AE5,
            Orders_From_Lucien_Lachance = 0x00035E03,
            Origin_of_the_Mages_Guild = 0x0002458F,
            Pacification = 0x00084AE8,
            Pack_Mule = 0x000888B8,
            Palace_Break___In = 0x0006D6F4,
            Pale_Pass_Discovery = 0x00066CD2,
            Pale_Pass_Map = 0x00066D69,
            Palla_volume_1 = 0x00024409,
            Palla_volume_2 = 0x000243DD,
            Paralyze = 0x000898FD,
            Parchment = 0x000CAA9A,
            Pension_of_the_Ancestor_Moth = 0x000982F0,
            Plan_for_the_Big_Heist = 0x00022DB4,
            Poor_Burdened_by_Taxes = 0x0006D6F0,
            Pranks_Spoils_Society_Gathering = 0x00098689,
            Proper_Lock_Design = 0x00073A64,
            Protect = 0x0008991B,
            Provinces_of_Tamriel = 0x0002457F,
            Psychic_Motion = 0x0008ADA0,
            Public_Notice = 0x00071D50,
            Purloined_Shadows = 0x0002454A,
            Rage = 0x000888E0,
            Rain_of_Burning_Dogs = 0x00098683,
            Ramblings_of_Audens_Avidius = 0x0003D06B,
            Reality_and_Other_Falsehoods = 0x00073A69,
            Rebuke_Undead = 0x0008ADA5,
            Recipe = 0x0006DBBA,
            Recipe_ = 0x0006DBBB,
            Recipe__ = 0x0006DBBC,
            Remanada = 0x000BF1CF,
            Remote_Manipulation = 0x0008AD9F,
            Report_____Disaster_at_Ionith = 0x00024558,
            Repulse_Undead = 0x0008ADA4,
            Response_to_Bero__s_Speech = 0x000243F8,
            Restore_Agility = 0x00089905,
            Restore_Endurance = 0x00089907,
            Restore_Intelligence = 0x00089909,
            Restore_Luck = 0x0008990B,
            Restore_Personality = 0x0008990D,
            Restore_Speed = 0x0008990F,
            Restore_Strength = 0x00089911,
            Restore_Willpower = 0x00089913,
            Reverse_Invisibility = 0x0003001B,
            Rislav_The_Righteous = 0x0002440F,
            River_Walk = 0x0008ADAC,
            Ruins_of_Kemel___Ze = 0x00024575,
            Sacred_Witness = 0x00024548,
            Scorching_Blow = 0x000888C6,
            Scrap_from_Lorgren__s_Diary = 0x00003A9B,
            Sealed_Forged_Candidate_List = 0x0000C22A,
            Sealed_Note = 0x00022173,
            Searing_Grasp = 0x000888C5,
            Seductive_Charm = 0x00084AF9,
            Serenity = 0x00084AE7,
            Sever_Magicka = 0x00088883,
            Shadow = 0x00084AEC,
            Shadow_Shape = 0x000898E9,
            Shield = 0x0008991E,
            Shock = 0x00089924,
            Shocking_Burst = 0x00089920,
            Shocking_Touch = 0x00089928,
            Shop_Hours = 0x000C4284,
            Shopping_List = 0x0006DBBD,
            Shopping_List_ = 0x0006DBBE,
            Shopping_List__ = 0x00154CD6,
            Shopping_List___ = 0x00154CD7,
            Shopping_List____ = 0x00154CD8,
            Shopping_List_____ = 0x00154CD9,
            Shopping_List______ = 0x00154CDA,
            Shopping_List_______ = 0x00154CDB,
            Shopping_List________ = 0x00154CDC,
            Shopping_List_________ = 0x00154CDD,
            Silence = 0x0008AD91,
            Sithis = 0x000243D6,
            Sketch_of_the_High_Fane = 0x0002AF00,
            Slythe__s_Journal_page_1 = 0x0018BC23,
            Slythe__s_Journal_page_2 = 0x0018D25B,
            Slythe__s_Journal_page_3 = 0x0018D25D,
            Snowball = 0x000888E6,
            Song_Of_Hrormir = 0x000243E6,
            Song_of_the_Alchemists = 0x000243D1,
            Soothing_Touch = 0x00084AE9,
            Souls_Black_and_White = 0x00073A6B,
            Spark = 0x00089923,
            Spectral_Form = 0x000898EB,
            Spell_Absorption = 0x0008AD97,
            Spirit_of_the_Daedra = 0x00024582,
            Starlight = 0x000898EC,
            Stormrider_Scroll = 0x000CA120,
            Suicide_Note = 0x001778D9,
            Summon_Clannfear = 0x00015AD9,
            Summon_Daedroth = 0x00015ADA,
            Summon_Dremora = 0x00015AD8,
            Summon_Flame_Atronach = 0x00015AD7,
            Summon_Frost_Atronach = 0x00015ADC,
            Summon_Ghost = 0x00015AD0,
            Summon_Headless_Zombie = 0x0008AD9E,
            Summon_Lich = 0x00015AD3,
            Summon_Rufio__s_Ghost = 0x0009190F,
            Summon_Scamp = 0x00015AD5,
            Summon_Skeleton = 0x00015ACF,
            Summon_Skeleton_Archer = 0x0008AD9A,
            Summon_Skeleton_Champ = 0x0008AD9B,
            Summon_Skeleton_Hero = 0x0008AD9C,
            Summon_Spider_Daedra = 0x00015AD6,
            Summon_Storm_Atronach = 0x00015ADD,
            Summon_Wraith = 0x00015AD2,
            Summon_Wraith_Gloom = 0x0008AD9D,
            Summon_Xivilai = 0x00015AD4,
            Summon_Zombie = 0x00015AD1,
            Summoning_Dremora_Lord = 0x00015ADB,
            Superior_Convalescence = 0x000C7668,
            Superior_Detect_Life = 0x0008888E,
            Superior_Magicka_Drain = 0x000888AB,
            Superior_Self = 0x00088897,
            Superior_Soul_Trap = 0x0008AD95,
            Superior_Spell_Absorption = 0x0008AD98,
            Superior_Spell_Reflection = 0x00089903,
            Superior_Wound = 0x000888A8,
            Surfeit_of_Thieves = 0x00024545,
            Suspicious_Letter = 0x0003C37E,
            Tamrielic_Lore = 0x0002457A,
            Tavern_Hours = 0x000C47BD,
            Telaendril__s_Ocheeva_Note = 0x00175F62,
            Telekinesis = 0x0008ADA1,
            Ten_Commands_____Nine_Divines = 0x00024577,
            Terrifying_Presence = 0x00088889,
            The_Amulet_of_Kings = 0x00024578,
            The_Argonian_Account_Book_1 = 0x000243E2,
            The_Argonian_Account_Book_2 = 0x00024559,
            The_Argonian_Account_Book_3 = 0x00024407,
            The_Argonian_Account_Book_4 = 0x0002455A,
            The_Armorer__s_Challenge = 0x000243D9,
            The_Art_of_War_Magic = 0x000243FA,
            The_Black_Arrow_v_1 = 0x000243CD,
            The_Black_Arrow_v_2 = 0x00024531,
            The_Black_Arts_On_Trial = 0x00024539,
            The_Book_of_Daedra = 0x00024563,
            The_Brothers_of_Darkness = 0x00024586,
            The_Buying_Game = 0x00024532,
            The_Doors_of_Oblivion = 0x000243F2,
            The_Dragon_Break = 0x000243D5,
            The_Eastern_Provinces = 0x00024565,
            The_Exodus = 0x0002453E,
            The_Firmament = 0x0002457B,
            The_Firsthold_Revolt = 0x00024537,
            The_Five_Tenets = 0x00024596,
            The_Gold_Ribbon_of_Merit = 0x00024410,
            The_Horrors_of_Castle_Xyr = 0x000243F7,
            The_Importance_of_Where = 0x000243EE,
            The_Last_King_of_the_Ayleids = 0x00058EEE,
            The_Legendary_Sancre_Tor = 0x00073A62,
            The_Legendary_Scourge = 0x00024583,
            The_Locked_Room = 0x00024541,
            The_Lunar_Lorkhan = 0x000243D8,
            The_Lusty_Argonian_Maid = 0x00078562,
            The_Madness_of_Pelagius = 0x0002457D,
            The_Mirror = 0x000243E9,
            The_Old_Ways = 0x0002458E,
            The_Path_of_Transcendence = 0x0003647E,
            The_Pig_Children = 0x00024590,
            The_Posting_of_the_Hunt = 0x00024585,
            The_Ransom_of_Zarek = 0x000243DE,
            The_Real_Barenziah_v_1 = 0x00024570,
            The_Real_Barenziah_v_2 = 0x00024571,
            The_Real_Barenziah_v_3 = 0x00024572,
            The_Real_Barenziah_v_4 = 0x00024573,
            The_Real_Barenziah_v_5 = 0x00024574,
            The_Rear_Guard = 0x0002440B,
            The_Red_Book_of_Riddles = 0x00024591,
            The_Red_Kitchen_Reader = 0x000243E0,
            The_Refugees = 0x0002440E,
            The_Seed = 0x000243FF,
            The_Third_Door = 0x000243F3,
            The_True_Nature_of_Orcs = 0x00024592,
            The_Warp_in_the_West = 0x000243EC,
            The_Warrior__s_Charge = 0x000243F6,
            The_Waters_of_Oblivion = 0x00024593,
            The_Wild_Elves = 0x00024594,
            The_Wolf_Queen_v_1 = 0x00024542,
            The_Wolf_Queen_v_2 = 0x000243FD,
            The_Wolf_Queen_v_3 = 0x00024406,
            The_Wolf_Queen_v_4 = 0x00024533,
            The_Wolf_Queen_v_5 = 0x0002454C,
            The_Wolf_Queen_v_6 = 0x00024546,
            The_Wolf_Queen_v_7 = 0x0002454E,
            The_Wolf_Queen_v_8 = 0x00024581,
            Thief = 0x000243CA,
            Thief_of_Virtue = 0x0001F112,
            Tome_of_Unlife = 0x00003AA3,
            Torchlight = 0x000898EF,
            Touch_of_Fear = 0x0008888A,
            Touch_of_Frenzy = 0x000888E1,
            Touch_of_Rage = 0x000888E2,
            Tragic_Accident_Baenlin_Dead = 0x000732B7,
            Traitor__s_Diary = 0x00003968,
            Transfer_Orders = 0x000C4A29,
            Transfer_Orders_ = 0x000982F1,
            Trials_of_St_Alessia = 0x00024579,
            Turn_Undead = 0x0008ADA3,
            Undelivered_Letter = 0x000C794B,
            Vampire_Nest_in_the_City = 0x0006D6F3,
            Varieties_of_Daedra = 0x0002457C,
            Vernaccus_and_Bourlor = 0x0002452F,
            Vicente__s_Note_to_Ocheeva = 0x000693D2,
            Voice_of_Dread = 0x00088887,
            Voice_of_Rapture = 0x00084AF0,
            Wanted_Poster = 0x000982EF,
            Warrior = 0x000243EB,
            Water_Breathing = 0x0008ADA7,
            Waterfront_Raid_Fails = 0x0006D6F1,
            Waterfront_Tax_Records = 0x000C4A2A,
            Waterfront_Tax_Records_ = 0x00034875,
            Way_of_the_Exposed_Palm = 0x00073A6A,
            Weakness_to_Disease = 0x0008ADB0,
            Weakness_to_Fire = 0x0008ADB1,
            Weakness_to_Frost = 0x0008ADB2,
            Weakness_to_Magicka = 0x0008ADB3,
            Weakness_to_Poison = 0x0008ADB5,
            Weakness_to_Shock = 0x0008ADB6,
            Weakness_to_Weapons = 0x0008ADB4,
            Weathered_Journal = 0x000624D9,
            Winter__s_Grasp = 0x000888ED,
            Withering_Bolt = 0x0008729F,
            Withering_Touch = 0x00088882,
            Withershins = 0x0002453C,
            Words_and_Philosophy = 0x000243E3,
            Worn_Faded_Note = 0x0002C500,
            Acrobatics_Pants = 0x00048981,
            Acrobat__s_Amulet = 0x00098457,
            Aegis_Robe = 0x0000552C,
            Ahdarji__s_Ring = 0x00035E95,
            Amulet_of_Absorption = 0x0009843E,
            Amulet_of_Axes = 0x00098453,
            Amulet_of_Illusion = 0x00092AC6,
            Amulet_of_Interrogation = 0x000C04D6,
            Amulet_of_Kings = 0x000250A0,
            Amulet_of_Luck = 0x00091AD5,
            Amulet_of_Reflection = 0x0009845B,
            Amulet_of_the_Ansei = 0x00187BBD,
            Amulet_of_the_Ansei_ = 0x00187BBE,
            Amulet_of_the_Ansei__ = 0x00187BBF,
            Ancotar__s_Ring_of_Protection = 0x00030135,
            Apron_of_Adroitness______ = 0x0006B67E,
            Apron_of_Adroitness_______ = 0x0006B67F,
            Apron_of_Adroitness________ = 0x0006B680,
            Apron_of_Adroitness_________ = 0x0006B681,
            Apron_of_Adroitness__________ = 0x0006B682,
            Apron_of_Adroitness___________ = 0x00094ECB,
            Apron_of_the_Master_Artisan = 0x000CA122,
            Aqua_Silk_Hood = 0x00071022,
            Aqua_Silk_Robes = 0x00071021,
            Arch___Mage__s_Hood = 0x00064FE0,
            Arch___Mage__s_Robe = 0x00064FDF,
            Arnora__s_Amulet = 0x00093551,
            Arnora__s_True_Amulet = 0x00053787,
            Astia__s_Necklace = 0x000CBD53,
            Base_Amulet_of_Absorption = 0x0009843D,
            Base_Amulet_of_Illusion = 0x00092AC5,
            Base_Amulet_of_Luck = 0x00091AD4,
            Base_Amulet_of_Reflection = 0x000937FA,
            Base_Necklace_of_Mercantile = 0x00092AAD,
            Base_Necklace_of_Personality = 0x00091AD1,
            Base_Necklace_of_Seawalking = 0x00098443,
            Base_Necklace_of_Speechcraft = 0x00092AB6,
            Base_Necklace_of_the_Sea = 0x00098440,
            Base_Ring_of_Acrobatics = 0x00091C3C,
            Base_Ring_of_Aegis = 0x00098437,
            Base_Ring_of_Agility = 0x00091AC8,
            Base_Ring_of_Alchemy = 0x00092AB9,
            Base_Ring_of_Alteration = 0x00092ABC,
            Base_Ring_of_Athletics = 0x00091C2A,
            Base_Ring_of_Blades = 0x00091C2D,
            Base_Ring_of_Block = 0x00091C30,
            Base_Ring_of_Blunt_Force = 0x00091C33,
            Base_Ring_of_Brawling = 0x00091C36,
            Base_Ring_of_Conjuration = 0x00092ABF,
            Base_Ring_of_Destruction = 0x0009DED3,
            Base_Ring_of_Detect_Life = 0x00091AB6,
            Base_Ring_of_Endurance = 0x00091ACE,
            Base_Ring_of_Feather = 0x00091AB9,
            Base_Ring_of_Fire_Shield = 0x00091ABD,
            Base_Ring_of_Firewalking = 0x00098419,
            Base_Ring_of_Fortitude = 0x0009845D,
            Base_Ring_of_Freedom = 0x00098425,
            Base_Ring_of_Frost_Shield = 0x000937EF,
            Base_Ring_of_Health = 0x00091C21,
            Base_Ring_of_Heavy_Armor = 0x00091C39,
            Base_Ring_of_Intelligence = 0x00091AC2,
            Base_Ring_of_Light = 0x000937F2,
            Base_Ring_of_Light_Armor = 0x00091C3F,
            Base_Ring_of_Magicka = 0x00091C24,
            Base_Ring_of_Mysticism = 0x00092AC8,
            Base_Ring_of_Nighteye = 0x000937F5,
            Base_Ring_of_Nihilism = 0x0009841F,
            Base_Ring_of_Restoration = 0x00092ACB,
            Base_Ring_of_Retribution = 0x00091AAF,
            Base_Ring_of_Security = 0x00092AB0,
            Base_Ring_of_Shadows = 0x00091AAE,
            Base_Ring_of_Shock_Shield = 0x0009843A,
            Base_Ring_of_Sneak = 0x00092AB3,
            Base_Ring_of_Speed = 0x00091ACB,
            Base_Ring_of_Steelskin = 0x00098422,
            Base_Ring_of_Storms = 0x0009842B,
            Base_Ring_of_Strength = 0x00091ABF,
            Base_Ring_of_the_Archer = 0x00092AAA,
            Base_Ring_of_the_Armorer = 0x00091C27,
            Base_Ring_of_the_North = 0x0009841C,
            Base_Ring_of_the_Viper = 0x00098428,
            Base_Ring_of_Vigor = 0x00091AD7,
            Base_Ring_of_Willpower = 0x00091AC5,
            Beggar__s_Shirt = 0x00064F7E,
            Belted_Braies = 0x0002B90B,
            Belted_Vest = 0x0002B90C,
            Black_and_Burgundy_Outfit = 0x0001C887,
            Black_Band = 0x0002FDDB,
            Black_Band_ = 0x0002FDDC,
            Black_Band__ = 0x0002FDDD,
            Black_Band___ = 0x0002FDF1,
            Black_Band____ = 0x0002FDF8,
            Black_Band_____ = 0x0002FF33,
            Black_Band______ = 0x0002FF34,
            Black_Hand_Hood = 0x000651D3,
            Black_Hand_Robe = 0x000651D2,
            Black_Hood = 0x00064FE3,
            Black_Robe = 0x00064FE2,
            Black_Wide_Pants = 0x00064F7B,
            Blackheart__s_Ring = 0x0002C633,
            Blacksmith__s_Apron = 0x000229A6,
            Blacksmith__s_Pants = 0x000229A7,
            Blackwood_Ring_of_Silence = 0x00098175,
            Blue_and_Green_Outfit = 0x0001C884,
            Blue_Collar_Shirt = 0x0000C59E,
            Blue_Silk_Shirt = 0x00064FE5,
            Blue_Silks = 0x00064FE4,
            Blue_Suede_Shoes = 0x0002319E,
            Blue_Velvet_Outfit = 0x0002319D,
            Boots_of_Springheel_Jak = 0x000148D4,
            Braided_Leather_Sandals = 0x0002B90D,
            Brass_Pearl_Ring = 0x00038017,
            Brass_Ring = 0x00038011,
            Brass_Topaz_Ring = 0x00038018,
            Breeches = 0x00028587,
            Bronze_Amulet = 0x0003802D,
            Bronze_Necklace = 0x00038027,
            Brown_Shirt = 0x000229AD,
            Buckled_Shoes = 0x00028733,
            Burgundy_Linen_Shirt = 0x00000B86,
            Burgundy_Linens = 0x0001047C,
            Burlap_Vest = 0x0002C0F8,
            Chameleon_Robe = 0x0000844D,
            Child_Overalls = 0x000CAAA1,
            Circlet_of_Omnipotence = 0x00088FED,
            Clogs = 0x0002858E,
            Coarse_Linen_Shirt = 0x0001C82E,
            Coarse_Linens = 0x0001C82C,
            Collared_Shirt = 0x00028732,
            Colovian_Signet_Ring = 0x00032DA0,
            Copper_Amulet = 0x0003802E,
            Copper_Necklace = 0x00038028,
            Copper_Pearl_Ring = 0x00038019,
            Copper_Ring = 0x00038012,
            Copper_Ruby_Ring = 0x0003801B,
            Copper_Topaz_Ring = 0x0003801A,
            Councilor__s_Hood = 0x000CA12C,
            Cowl_of_the_Druid = 0x000CA121,
            Cruelty__s_Heart = 0x000347DD,
            Cruelty__s_Heart_ = 0x000347DE,
            Cruelty__s_Heart__ = 0x000347DF,
            Cruelty__s_Heart___ = 0x000347E0,
            Cruelty__s_Heart____ = 0x000347E1,
            Cruelty__s_Heart_____ = 0x000347E2,
            Cruelty__s_Heart______ = 0x000347E3,
            Dark_Green_Shirt = 0x000229AA,
            Dark_Shirt = 0x00064F7D,
            Doeskin_Shoes = 0x0001C883,
            Draconian_Madstone = 0x0007304D,
            Draconian_Madstone_ = 0x0001C172,
            Dreamworld_Amulet = 0x00089008,
            Dreamworld_Amulet_ = 0x0002CDC6,
            Ebony_Diamond_Ring = 0x00038026,
            Ebony_Emerald_Ring = 0x00038025,
            Ebony_Ring = 0x00038016,
            Elemental_Ring = 0x00098450,
            Emperor__s_Robe_ = 0x00023D33,
            Emperor__s_Robe__ = 0x0000BC2D,
            Emperor__s_Shoes = 0x00023D53,
            Ernest__s_Best_Shirt = 0x00064F76,
            Ernest__s_Fancy_Pants = 0x000CBD37,
            Ernest__s_Shoes = 0x000CBD39,
            Eye_of_Sithis = 0x00082DE0,
            Feather_Shoes = 0x00048991,
            Fishing_Waders = 0x0002ECAC,
            Flame_Ring = 0x0009844C,
            Flax_Tunic = 0x0002C0FA,
            Forester__s_Shirt = 0x00064F79,
            Fortify_Fatigue_Pants = 0x00048992,
            Fortify_Magicka_Pants = 0x0000552D,
            Frost_Ring = 0x0009844D,
            Gold_Amulet = 0x00038031,
            Gold_Diamond_Ring = 0x00038024,
            Gold_Emerald_Ring = 0x00038023,
            Gold_Necklace = 0x0003802B,
            Gold_Ring = 0x00038015,
            Gold_Sapphire_Ring = 0x00038022,
            Gold_Trimmed_Shoes = 0x0001C888,
            Grand_Amulet_of_Absorption = 0x0009843F,
            Grand_Amulet_of_Illusion = 0x00092AC7,
            Grand_Amulet_of_Luck = 0x00091AD6,
            Grand_Amulet_of_Reflection = 0x0009845C,
            Grand_Necklace_of_Mercantile = 0x00092AAF,
            Grand_Necklace_of_Personality = 0x00091AD3,
            Grand_Necklace_of_Seawalking = 0x00098445,
            Grand_Necklace_of_Speechcraft = 0x00092AB8,
            Grand_Necklace_of_the_Sea = 0x00098441,
            Grand_Ring_of_Acrobatics = 0x00091C3E,
            Grand_Ring_of_Aegis = 0x00098439,
            Grand_Ring_of_Agility = 0x00091ACA,
            Grand_Ring_of_Alchemy = 0x00092ABB,
            Grand_Ring_of_Alteration = 0x00092ABE,
            Grand_Ring_of_Athletics = 0x00091C2C,
            Grand_Ring_of_Blades = 0x00091C2F,
            Grand_Ring_of_Block = 0x00091C32,
            Grand_Ring_of_Blunt_Force = 0x00091C35,
            Grand_Ring_of_Brawling = 0x00091C38,
            Grand_Ring_of_Conjuration = 0x00092AC1,
            Grand_Ring_of_Destruction = 0x00092AC4,
            Grand_Ring_of_Detect_Life = 0x00091AB8,
            Grand_Ring_of_Endurance = 0x00091AD0,
            Grand_Ring_of_Feather = 0x00091ABB,
            Grand_Ring_of_Fire_Shield = 0x00091ABE,
            Grand_Ring_of_Firewalking = 0x0009841B,
            Grand_Ring_of_Fortitude = 0x00098418,
            Grand_Ring_of_Freedom = 0x00098427,
            Grand_Ring_of_Frost_Shield = 0x000937F1,
            Grand_Ring_of_Health = 0x00091C23,
            Grand_Ring_of_Heavy_Armor = 0x00091C3B,
            Grand_Ring_of_Intelligence = 0x00091AC4,
            Grand_Ring_of_Light = 0x000937F4,
            Grand_Ring_of_Light_Armor = 0x00091C41,
            Grand_Ring_of_Magicka = 0x00091C26,
            Grand_Ring_of_Mysticism = 0x00092ACA,
            Grand_Ring_of_Nighteye = 0x000937F7,
            Grand_Ring_of_Nihilism = 0x00098421,
            Grand_Ring_of_Restoration = 0x00092ACD,
            Grand_Ring_of_Retribution = 0x000937F9,
            Grand_Ring_of_Security = 0x00092AB2,
            Grand_Ring_of_Shadows = 0x00091AB2,
            Grand_Ring_of_Shock_Shield = 0x0009843C,
            Grand_Ring_of_Sneak = 0x00092AB5,
            Grand_Ring_of_Speed = 0x00091ACD,
            Grand_Ring_of_Steelskin = 0x00098424,
            Grand_Ring_of_Storms = 0x0009842D,
            Grand_Ring_of_Strength = 0x00091AC1,
            Grand_Ring_of_the_Archer = 0x00092AAC,
            Grand_Ring_of_the_Armorer = 0x00091C29,
            Grand_Ring_of_the_North = 0x0009841E,
            Grand_Ring_of_the_Viper = 0x0009842A,
            Grand_Ring_of_Vigor = 0x00091AD9,
            Grand_Ring_of_Willpower = 0x00091AC7,
            Gray_Cowl_of_Nocturnal = 0x00022E81,
            Greater_Amulet_of_Interrogation = 0x000C04D7,
            Green_Brocade_Doublet = 0x000229B0,
            Green_Felt_Linens = 0x00028731,
            Green_Robe = 0x0007101B,
            Green_Robe_Hood = 0x0007101C,
            Green_Silk_Garment = 0x000229B1,
            Green_Velvet__Shoes = 0x000229B2,
            Green_Wool_Shirt = 0x0002ECB0,
            Grey_Robe = 0x0012DD1E,
            Grey_Robe_Hood = 0x0012DD1F,
            Heinrich__s_Pants = 0x000CBD3D,
            Heinrich__s_Shirt = 0x000CBD3F,
            Heinrich__s_Shoes = 0x000CBD47,
            Highwayman__s_Shirt = 0x00064FE6,
            Hood_of_the_Apprentice = 0x0006A82D,
            Huntsman_Leather_Pants = 0x00000857,
            Huntsman_Moccasin = 0x0001C82B,
            Huntsman_Vest = 0x00000883,
            Imperial_Breeches = 0x000CA125,
            Indarys_Signet_Ring = 0x000335A8,
            Jade_Amulet = 0x00189CFE,
            Jade_Amulet_ = 0x0003802F,
            Jade_Necklace = 0x00038029,
            Jade_Ring = 0x00038013,
            Jade_Ruby_Ring = 0x0003801D,
            Jade_Sapphire_Ring = 0x0003801E,
            Jade_Topaz_Ring = 0x0003801C,
            Jewel_of_the_Rumare = 0x000856EF,
            Jewel_of_the_Rumare_ = 0x000C89CB,
            Jeweled_Amulet = 0x00038032,
            Jeweled_Necklace = 0x0003802C,
            King_of_Worms___Hood = 0x001885CC,
            King_of_Worms___Robes = 0x001885CA,
            Knights_of_the_Thorn_Medallion = 0x0006B676,
            Knights_of_the_Thorn_Medallion_ = 0x0006B677,
            Knights_of_the_Thorn_Medallion__ = 0x000335A9,
            Kylius_Lonavo__s_Ring = 0x00189181,
            Laced_Leather_Pants = 0x000229AB,
            Lesser_Amulet_of_Interrogation = 0x000C04D5,
            Lesser_Chameleon_Robe = 0x00048990,
            Light_Armor_Vest = 0x00048982,
            Light_Brown_Linens = 0x000229AE,
            Mage__s_Hood = 0x00064FE1,
            Mage__s_Robe = 0x00064F7F,
            Manduin__s_Amulet = 0x000347C8,
            Mankar_Camoran__s_Robe = 0x000BE31C,
            Mankar_Camoran__s_Robe_ = 0x000BE31D,
            Mankar_Camoran__s_Robe__ = 0x000BE31E,
            Mankar_Camoran__s_Robe___ = 0x000BE31F,
            Mantle_of_the_Woodsman = 0x000CA129,
            Marksman_Quilted_Doublet = 0x00048983,
            Mercantile_Black_Outfit = 0x00048984,
            Mind_and_Body_Ring = 0x000366BF,
            Mind_and_Body_Ring_ = 0x000366C0,
            Mind_and_Body_Ring__ = 0x000366C1,
            Monk_Robe = 0x0001E7FF,
            Mundane_Amulet = 0x00038B07,
            Mundane_Ring = 0x0009844B,
            Mythic_Dawn_Hood = 0x0008D755,
            Mythic_Dawn_Robe = 0x00024DE2,
            Necklace_of_Mercantile = 0x00092AAE,
            Necklace_of_Personality = 0x00091AD2,
            Necklace_of_Seawalking = 0x00098444,
            Necklace_of_Speechcraft = 0x00092AB7,
            Necklace_of_Swords = 0x00098452,
            Necklace_of_the_Sea = 0x00098442,
            Necromancer__s_Amulet = 0x000146C6,
            Necromancer__s_Amulet_ = 0x0007BE27,
            Necromancer__s_Amulet__ = 0x0007BE28,
            Necromancer__s_Amulet___ = 0x0007BE29,
            Necromancer__s_Amulet____ = 0x0007BE2A,
            Necromancer__s_Amulet_____ = 0x0007BE2B,
            Necromancer__s_Hood = 0x001885CB,
            Necromancer__s_Robes = 0x001885C9,
            Nistor__s_Boots = 0x000CA12B,
            Oiled_Linen_Shoes = 0x0001C82F,
            Olive__Vest = 0x0002C0F4,
            Pants = 0x00000015,
            Patched_Vest = 0x0002ECAD,
            Phylactery_of_Litheness = 0x0006B648,
            Phylactery_of_Litheness_ = 0x0006B649,
            Phylactery_of_Litheness__ = 0x0006B64A,
            Phylactery_of_Litheness___ = 0x0006B64B,
            Phylactery_of_Litheness____ = 0x0006B64C,
            Phylactery_of_Litheness_____ = 0x000385A0,
            Pigskin_Shoes = 0x000229AF,
            Pinarus___Shirt = 0x000CBD51,
            Plaid_Shirt = 0x00064F78,
            Quilted_Doublet = 0x0001C831,
            Quilted_Shoes = 0x0001C886,
            Red_Silk_Hood = 0x00071020,
            Red_Silk_Robes = 0x0007101F,
            Red_Velvet_Blouse = 0x00003A94,
            Red_Velvet_Garment = 0x00003A93,
            Red_Velvet_Outfit = 0x000A498E,
            Resist_Cold_Pants = 0x0000844E,
            Resist_Disease_Burgundy_Linen_Shirt = 0x00048989,
            Resist_Fire_Burgundy_Linens = 0x0004898A,
            Resist_Normal_Weapons_Quilted_Doublet = 0x0004898B,
            Resist_Poison_Blue_and_Green_Outfit = 0x0004898C,
            Ring_of_Acrobatics = 0x00091C3D,
            Ring_of_Aegis = 0x00098438,
            Ring_of_Agility = 0x00091AC9,
            Ring_of_Alchemy = 0x00092ABA,
            Ring_of_Alteration = 0x00092ABD,
            Ring_of_Athletics = 0x00091C2B,
            Ring_of_Blades = 0x00091C2E,
            Ring_of_Block = 0x00091C31,
            Ring_of_Blunt_Force = 0x00091C34,
            Ring_of_Brawling = 0x00091C37,
            Ring_of_Burden = 0x0002E5AC,
            Ring_of_Conjuration = 0x00092AC0,
            Ring_of_Destruction = 0x00092AC3,
            Ring_of_Detect_Life = 0x00091AB7,
            Ring_of_Eidolon__s_Edge = 0x0006BD6B,
            Ring_of_Eidolon__s_Edge_ = 0x0006BD6C,
            Ring_of_Eidolon__s_Edge__ = 0x0006BD6D,
            Ring_of_Eidolon__s_Edge___ = 0x0006BD6E,
            Ring_of_Eidolon__s_Edge____ = 0x0006BD6F,
            Ring_of_Eidolon__s_Edge_____ = 0x0004F9E5,
            Ring_of_Endurance = 0x00091ACF,
            Ring_of_Feather = 0x00091ABA,
            Ring_of_Fire_Shield = 0x00091ABC,
            Ring_of_Firewalking = 0x0009841A,
            Ring_of_Fortitude = 0x00098417,
            Ring_of_Freedom = 0x00098426,
            Ring_of_Frost_Shield = 0x000937F0,
            Ring_of_Health = 0x00091C22,
            Ring_of_Heavy_Armor = 0x00091C3A,
            Ring_of_Intelligence = 0x00091AC3,
            Ring_of_Khajiiti = 0x00027110,
            Ring_of_Light = 0x000937F3,
            Ring_of_Light_Armor = 0x00091C40,
            Ring_of_Magicka = 0x00091C25,
            Ring_of_Mysticism = 0x00092AC9,
            Ring_of_Namira = 0x0001C10A,
            Ring_of_Nighteye = 0x000937F6,
            Ring_of_Nihilism = 0x00098420,
            Ring_of_Perfection = 0x00098446,
            Ring_of_Restoration = 0x00092ACC,
            Ring_of_Retribution = 0x000937F8,
            Ring_of_Security = 0x00092AB1,
            Ring_of_Shadows = 0x00091AB1,
            Ring_of_Shock_Shield = 0x0009843B,
            Ring_of_Skimming = 0x00098456,
            Ring_of_Sneak = 0x00092AB4,
            Ring_of_Speed = 0x00091ACC,
            Ring_of_Stamina = 0x00098447,
            Ring_of_Steelskin = 0x00098423,
            Ring_of_Storms = 0x0009842C,
            Ring_of_Strength = 0x00091AC0,
            Ring_of_Sunfire = 0x0006B689,
            Ring_of_Sunfire_ = 0x0006B68A,
            Ring_of_Sunfire__ = 0x0006B68B,
            Ring_of_Sunfire___ = 0x0006B68C,
            Ring_of_Sunfire____ = 0x0006B68D,
            Ring_of_Sunfire_____ = 0x0001E0FA,
            Ring_of_the_Archer = 0x00092AAB,
            Ring_of_the_Armorer = 0x00091C28,
            Ring_of_the_Gray = 0x0000CCC8,
            Ring_of_the_Iron_Fist = 0x00098458,
            Ring_of_the_North = 0x0009841D,
            Ring_of_the_Viper = 0x00098429,
            Ring_of_the_Vipereye = 0x0006B654,
            Ring_of_the_Vipereye_ = 0x0006B655,
            Ring_of_the_Vipereye__ = 0x0006B656,
            Ring_of_the_Vipereye___ = 0x0006B657,
            Ring_of_the_Vipereye____ = 0x0006B658,
            Ring_of_the_Vipereye_____ = 0x0001C4DA,
            Ring_of_Thieves = 0x00098455,
            Ring_of_Transmutation = 0x000CA126,
            Ring_of_Treachery = 0x00098449,
            Ring_of_Vigor = 0x00091AD8,
            Ring_of_Vitality = 0x0009844F,
            Ring_of_War = 0x00098448,
            Ring_of_Willpower = 0x00091AC6,
            Ring_of_Wizardry = 0x0009844A,
            Ring_of_Wortcraft = 0x000CA128,
            Robe_of_Creativity = 0x000CA127,
            Robe_of_Defense = 0x00005529,
            Robe_of_Deflection = 0x00005519,
            Robe_of_Glib_Tongues = 0x000478A5,
            Robe_of_Protection = 0x00005527,
            Robe_of_the_Apprentice = 0x000C8534,
            Robe_of_the_Apprentice_ = 0x000C8535,
            Robe_of_the_Apprentice__ = 0x000C8536,
            Robe_of_the_Conjurer = 0x000748A6,
            Robe_of_the_Conjurer_ = 0x000748A7,
            Robe_of_the_Conjurer__ = 0x000748A8,
            Robe_of_the_Conjurer___ = 0x000748A9,
            Robe_of_the_Conjurer____ = 0x000748AA,
            Robe_of_the_Conjurer_____ = 0x000748AB,
            Robe_of_Warding = 0x0000551C,
            Rough_Leather_Shoes = 0x000229A8,
            Rugged_Pants = 0x00064F7A,
            Russet_Felt_Outfit = 0x000352BA,
            Russet_Felt_Shirt_of_Blade_Turning = 0x0000844C,
            Russet_Felt_Shoes = 0x000352BB,
            Sack_Cloth_Pants = 0x00027318,
            Sack_Cloth_Sandals = 0x0002731A,
            Sack_Cloth_Shirt = 0x00027319,
            Security_Russet_Felt_Shirt = 0x00048985,
            Shield_Ring = 0x0002EE74,
            Shield_Ring_ = 0x0007B7A5,
            Shield_Ring__ = 0x0007B7A6,
            Shirt = 0x00000017,
            Shirt_with_Suspenders = 0x00064F77,
            Shirt_with_Suspenders_ = 0x0002858B,
            Shoes = 0x00000016,
            Shop_Keep__s_Shirt = 0x000CBD33,
            Short_Britches = 0x00064F7C,
            Silver_Amulet = 0x00038030,
            Silver_Emerald_Ring = 0x00038021,
            Silver_Necklace = 0x0003802A,
            Silver_Ring = 0x0003801F,
            Silver_Ruby_Ring = 0x00038014,
            Silver_Sapphire_Ring = 0x00038020,
            Sneak_Blue_Suede_Shoes = 0x00048986,
            Sorcerer__s_Ring = 0x00098454,
            Spectre_Ring = 0x000CA12A,
            Speechcraft_Green_Brocade_Doublet = 0x00048987,
            Spelldrinker_Amulet = 0x00095A6B,
            Spelldrinker_Amulet_ = 0x00095A6C,
            Spelldrinker_Amulet__ = 0x00095A6D,
            Spelldrinker_Amulet___ = 0x00095A6E,
            Spelldrinker_Amulet____ = 0x00095A6F,
            Spelldrinker_Amulet_____ = 0x00095A70,
            Spelldrinker_Amulet______ = 0x00095A71,
            Stitched_Green_Shirt = 0x0002C0F6,
            Stitched_Leather_Shoes = 0x000229AC,
            Storm_Ring = 0x0009844E,
            Tan_Linens = 0x0001C830,
            Tan_Robe = 0x00071019,
            Tan_Robe_Hood = 0x0007101A,
            Tattered_Pants = 0x0003EAAB,
            Tattered_Robe = 0x0007101D,
            Tattered_Robe_Hood = 0x0007101E,
            Tattered_Shirt = 0x0003EAAC,
            The_Deceiver__s_Finery = 0x0003489F,
            The_Deceiver__s_Finery_ = 0x000348A0,
            The_Deceiver__s_Finery__ = 0x000348A1,
            The_Deceiver__s_Finery___ = 0x000348A2,
            The_Deceiver__s_Finery____ = 0x000348A3,
            The_Deceiver__s_Finery_____ = 0x000348A4,
            The_Deceiver__s_Finery______ = 0x000348A5,
            Thick_Cowhide_Shoes = 0x00000BEA,
            Ulfgar_Family_Ring = 0x0001ECE4,
            Veil_of_the_Seer = 0x000CA124,
            Vest_of_the_Bard = 0x000CA123,
            Vest_of_Warding = 0x00005530,
            Waterwalking_Gold_Trimmed_Shoes = 0x0004898F,
            Weatherward_Circlet = 0x0018A88D,
            Weatherward_Circlet_ = 0x0006BD71,
            Weatherward_Circlet__ = 0x0006BD72,
            Weatherward_Circlet___ = 0x0006BD73,
            Weatherward_Circlet____ = 0x0006BD74,
            Weatherward_Circlet_____ = 0x0006BD75,
            Wedding_Ring = 0x00022E6D,
            White_Mage__s_Robes = 0x000A498F,
            White_Mage__s_Shoes = 0x000A4990,
            White_Monk_Robe = 0x00071047,
            Wrist_Irons = 0x000BE335,
            Ale = 0x000B1200,
            Beer = 0x000B1202,
            Cheap_Wine = 0x00037F7F,
            Cure_for_Vampirism = 0x000977E4,
            Cyrodilic_Brandy = 0x00033569,
            Grand_Elixir_of_Exploration = 0x0004E93A,
            Hist_Sap = 0x0003356A,
            Human_Blood = 0x00098309,
            Mead = 0x000B1201,
            Moderate_Elixir_of_Exploration = 0x0004E938,
            Newheim__s_Special_Brew = 0x000B1241,
            Philter_of_Frostward = 0x0007BCC9,
            Poison_of_Affliction = 0x0008DC5C,
            Poison_of_Apathy = 0x00009283,
            Poison_of_Burden = 0x000984A5,
            Poison_of_Catastrophe = 0x00009281,
            Poison_of_Clumsiness = 0x0008DC58,
            Poison_of_Confusion = 0x0008DC5E,
            Poison_of_Cowardice = 0x00056E54,
            Poison_of_Debilitation = 0x0000927D,
            Poison_of_Fatigue = 0x0008DC70,
            Poison_of_Feeblemind = 0x00009280,
            Poison_of_Frailty = 0x00009285,
            Poison_of_Fright = 0x0008DC6D,
            Poison_of_Fumbling = 0x0000927C,
            Poison_of_Illness = 0x0008DC73,
            Poison_of_Misfortune = 0x0008DC61,
            Poison_of_Paralysis = 0x00009302,
            Poison_of_Repulsion = 0x00009282,
            Poison_of_Separation = 0x00009284,
            Poison_of_Severing = 0x0008DC76,
            Poison_of_Sickness = 0x0000927F,
            Poison_of_Silence = 0x00009320,
            Poison_of_Slowing = 0x0008DC67,
            Poison_of_the_Fool = 0x0008DC64,
            Poison_of_Weakness = 0x0008DC6A,
            Poison_of_Weariness = 0x0000927E,
            Potion_of_Absorption = 0x00009321,
            Potion_of_Agility = 0x00098471,
            Potion_of_Alacrity = 0x0009849B,
            Potion_of_Antivenom = 0x00009308,
            Potion_of_Chameleon = 0x00088B1C,
            Potion_of_Charisma = 0x0009849A,
            Potion_of_Cure_Disease = 0x0000920E,
            Potion_of_Cure_Paralysis = 0x0000922E,
            Potion_of_Cure_Poison = 0x0000920F,
            Potion_of_Dedication = 0x00056E55,
            Potion_of_Detect_Life = 0x000984A6,
            Potion_of_Disbelief = 0x00009307,
            Potion_of_Dispel = 0x0000927B,
            Potion_of_Endurance = 0x00098472,
            Potion_of_Fatigue = 0x00098473,
            Potion_of_Feather = 0x000092E6,
            Potion_of_Fire_Shield = 0x000092E7,
            Potion_of_Fortitude = 0x00098494,
            Potion_of_Fortune = 0x00098498,
            Potion_of_Frost_Shield = 0x000092FE,
            Potion_of_Grace = 0x00098493,
            Potion_of_Grounding = 0x00009309,
            Potion_of_Healing = 0x00098496,
            Potion_of_Health = 0x00098474,
            Potion_of_Insight = 0x00098497,
            Potion_of_Insulation = 0x00009305,
            Potion_of_Intelligence = 0x00098475,
            Potion_of_Invisibility = 0x000092FF,
            Potion_of_Light = 0x00009300,
            Potion_of_Luck = 0x00098476,
            Potion_of_Magicka = 0x00098477,
            Potion_of_Might = 0x0009849C,
            Potion_of_Nighteye = 0x00009301,
            Potion_of_Personality = 0x00098478,
            Potion_of_Reflection = 0x00009303,
            Potion_of_Resistance = 0x00009304,
            Potion_of_Respite = 0x00098495,
            Potion_of_Seastride = 0x000984A4,
            Potion_of_Shock_Shield = 0x00009AE3,
            Potion_of_Sorcery = 0x00098499,
            Potion_of_Speed = 0x00098479,
            Potion_of_Strength = 0x0009847A,
            Potion_of_the_Sea = 0x000984A3,
            Potion_of_Warmth = 0x00009306,
            Potion_of_Willpower = 0x00056E53,
            Rosethorn_Mead = 0x000B97EA,
            Shadowbanish_Wine = 0x00185DCD,
            Skooma = 0x0004E0A9,
            Strong_Elixir_of_Exploration = 0x0004E939,
            Strong_Poison_of_Affliction = 0x0008DC5D,
            Strong_Poison_of_Apathy = 0x0009846A,
            Strong_Poison_of_Burden = 0x0000920D,
            Strong_Poison_of_Catastrophe = 0x00098464,
            Strong_Poison_of_Clumsiness = 0x0008DC5A,
            Strong_Poison_of_Confusion = 0x0008DC60,
            Strong_Poison_of_Cowardice = 0x00056E56,
            Strong_Poison_of_Debilitation = 0x000984AC,
            Strong_Poison_of_Fatigue = 0x0008DC72,
            Strong_Poison_of_Feeblemind = 0x00098462,
            Strong_Poison_of_Frailty = 0x0009846C,
            Strong_Poison_of_Fright = 0x0008DC6F,
            Strong_Poison_of_Fumbling = 0x000984AA,
            Strong_Poison_of_Illness = 0x0008DC75,
            Strong_Poison_of_Misfortune = 0x0008DC63,
            Strong_Poison_of_Paralysis = 0x00098484,
            Strong_Poison_of_Repulsion = 0x00098468,
            Strong_Poison_of_Separation = 0x00098466,
            Strong_Poison_of_Severing = 0x0008DC78,
            Strong_Poison_of_Sickness = 0x00098460,
            Strong_Poison_of_Silence = 0x0009849E,
            Strong_Poison_of_Slowing = 0x0008DC69,
            Strong_Poison_of_the_Fool = 0x0008DC66,
            Strong_Poison_of_Weakness = 0x0008DC6C,
            Strong_Poison_of_Weariness = 0x000984AE,
            Strong_Potion_of_Absorption = 0x000984A0,
            Strong_Potion_of_Agility = 0x000092E9,
            Strong_Potion_of_Alacrity = 0x00009319,
            Strong_Potion_of_Antivenom = 0x00098490,
            Strong_Potion_of_Chameleon = 0x00088B1E,
            Strong_Potion_of_Charisma = 0x00009317,
            Strong_Potion_of_Dedication = 0x00056E58,
            Strong_Potion_of_Detect_Life = 0x00009230,
            Strong_Potion_of_Disbelief = 0x0009848E,
            Strong_Potion_of_Dispel = 0x000984A8,
            Strong_Potion_of_Endurance = 0x000092EB,
            Strong_Potion_of_Fatigue = 0x000092ED,
            Strong_Potion_of_Feather = 0x0009846E,
            Strong_Potion_of_Fire_Shield = 0x00098470,
            Strong_Potion_of_Fortitude = 0x0000930D,
            Strong_Potion_of_Fortune = 0x00009315,
            Strong_Potion_of_Frost_Shield = 0x0009847C,
            Strong_Potion_of_Grace = 0x0000930B,
            Strong_Potion_of_Grounding = 0x00098492,
            Strong_Potion_of_Healing = 0x00009311,
            Strong_Potion_of_Health = 0x000092EF,
            Strong_Potion_of_Insight = 0x00009313,
            Strong_Potion_of_Insulation = 0x00098489,
            Strong_Potion_of_Intelligence = 0x000092F1,
            Strong_Potion_of_Invisibility = 0x0009847E,
            Strong_Potion_of_Light = 0x00098480,
            Strong_Potion_of_Luck = 0x000092F3,
            Strong_Potion_of_Magicka = 0x000092F9,
            Strong_Potion_of_Might = 0x0000931D,
            Strong_Potion_of_Nighteye = 0x00098482,
            Strong_Potion_of_Personality = 0x000092F5,
            Strong_Potion_of_Reflection = 0x00098486,
            Strong_Potion_of_Resistance = 0x00098488,
            Strong_Potion_of_Respite = 0x0000930F,
            Strong_Potion_of_Seastride = 0x00009327,
            Strong_Potion_of_Shock_Shield = 0x00009AE4,
            Strong_Potion_of_Sorcery = 0x0000931B,
            Strong_Potion_of_Speed = 0x000092F7,
            Strong_Potion_of_Strength = 0x000092FB,
            Strong_Potion_of_the_Sea = 0x00009325,
            Strong_Potion_of_Warmth = 0x0009848C,
            Strong_Potion_of_Willpower = 0x00056E57,
            Surilie_Brothers_Vintage_399 = 0x00037F84,
            Surilie_Brothers_Vintage_415 = 0x00037F82,
            Surilie_Brothers_Wine = 0x00037F80,
            Tamika_Vintage_399 = 0x00037F7E,
            Tamika_Vintage_415 = 0x00037F83,
            Tamika__s_West_Weald_Wine = 0x00037F81,
            Turpentine = 0x0018BD5A,
            Turpentine_ = 0x000CD2DB,
            Turpentine__ = 0x000CD2DC,
            Turpentine___ = 0x000CD2DD,
            Turpentine____ = 0x000CD2DE,
            Turpentine_____ = 0x000CD2DF,
            Weak_Elixir_of_Exploration = 0x0004E937,
            Weak_Poison_of_Affliction = 0x0008DC5B,
            Weak_Poison_of_Apathy = 0x00098469,
            Weak_Poison_of_Burden = 0x0000920C,
            Weak_Poison_of_Catastrophe = 0x00098463,
            Weak_Poison_of_Clumsiness = 0x0008DC59,
            Weak_Poison_of_Confusion = 0x0008DC5F,
            Weak_Poison_of_Cowardice = 0x00056E59,
            Weak_Poison_of_Debilitation = 0x000984AB,
            Weak_Poison_of_Fatigue = 0x0008DC71,
            Weak_Poison_of_Feeblemind = 0x00098461,
            Weak_Poison_of_Frailty = 0x0009846B,
            Weak_Poison_of_Fright = 0x0008DC6E,
            Weak_Poison_of_Fumbling = 0x000984A9,
            Weak_Poison_of_Illness = 0x0008DC74,
            Weak_Poison_of_Misfortune = 0x0008DC62,
            Weak_Poison_of_Paralysis = 0x00098483,
            Weak_Poison_of_Repulsion = 0x00098467,
            Weak_Poison_of_Separation = 0x00098465,
            Weak_Poison_of_Severing = 0x0008DC77,
            Weak_Poison_of_Sickness = 0x000984AF,
            Weak_Poison_of_Silence = 0x0009849D,
            Weak_Poison_of_Slowing = 0x0008DC68,
            Weak_Poison_of_the_Fool = 0x0008DC65,
            Weak_Poison_of_Weakness = 0x0008DC6B,
            Weak_Poison_of_Weariness = 0x000984AD,
            Weak_Potion_of_Absorption = 0x0009849F,
            Weak_Potion_of_Agility = 0x000092E8,
            Weak_Potion_of_Alacrity = 0x00009318,
            Weak_Potion_of_Antivenom = 0x0009848F,
            Weak_Potion_of_Chameleon = 0x00088B1D,
            Weak_Potion_of_Charisma = 0x00009316,
            Weak_Potion_of_Dedication = 0x00056E52,
            Weak_Potion_of_Detect_Life = 0x0000922F,
            Weak_Potion_of_Disbelief = 0x0009848D,
            Weak_Potion_of_Dispel = 0x000984A7,
            Weak_Potion_of_Endurance = 0x000092EA,
            Weak_Potion_of_Fatigue = 0x000092EC,
            Weak_Potion_of_Feather = 0x0009846D,
            Weak_Potion_of_Fire_Shield = 0x0009846F,
            Weak_Potion_of_Fortitude = 0x0000930C,
            Weak_Potion_of_Fortune = 0x00009314,
            Weak_Potion_of_Frost_Shield = 0x0009847B,
            Weak_Potion_of_Grace = 0x0000930A,
            Weak_Potion_of_Grounding = 0x00098491,
            Weak_Potion_of_Healing = 0x00009310,
            Weak_Potion_of_Health = 0x000092EE,
            Weak_Potion_of_Insight = 0x00009312,
            Weak_Potion_of_Insulation = 0x0009848A,
            Weak_Potion_of_Intelligence = 0x000092F0,
            Weak_Potion_of_Invisibility = 0x0009847D,
            Weak_Potion_of_Light = 0x0009847F,
            Weak_Potion_of_Luck = 0x000092F2,
            Weak_Potion_of_Magicka = 0x000092F8,
            Weak_Potion_of_Might = 0x0000931C,
            Weak_Potion_of_Nighteye = 0x00098481,
            Weak_Potion_of_Personality = 0x000092F4,
            Weak_Potion_of_Reflection = 0x00098485,
            Weak_Potion_of_Resistance = 0x00098487,
            Weak_Potion_of_Respite = 0x0000930E,
            Weak_Potion_of_Seastride = 0x00009326,
            Weak_Potion_of_Shock_Shield = 0x00009AE0,
            Weak_Potion_of_Sorcery = 0x0000931A,
            Weak_Potion_of_Speed = 0x000092F6,
            Weak_Potion_of_Strength = 0x000092FA,
            Weak_Potion_of_the_Sea = 0x00009324,
            Weak_Potion_of_Warmth = 0x0009848B,
            Weak_Potion_of_Willpower = 0x00056E51,
            Alkanet_Flower = 0x0003365C,
            Aloe_Vera_Leaves = 0x000A7924,
            Ambrosia = 0x000704A0,
            Apple = 0x0003365D,
            Arrowroot = 0x0003365E,
            Ashes_of_Hindaril = 0x000977DD,
            Beef = 0x0003365F,
            Bergamot_Seeds = 0x000A7933,
            Blackberry = 0x00033663,
            Bloodgrass = 0x00033664,
            Boar_Meat = 0x00033665,
            Bog_Beacon_Asco_Cap = 0x0008446C,
            Bonemeal = 0x00048734,
            Bonemeal_ = 0x0001EBFF,
            Bread_Loaf = 0x00023D89,
            Cairn_Bolete_Cap = 0x0006251E,
            Carrot = 0x00033666,
            Carrot_of_Seeing = 0x00082DE2,
            Cheese_Wedge = 0x00033668,
            Cheese_Wheel = 0x00033669,
            Cinnabar_Polypore_Red_Cap = 0x0008529C,
            Cinnabar_Polypore_Yellow_Cap = 0x0008529B,
            Clannfear_Claws = 0x0003366A,
            Clouded_Funnel_Cap = 0x00084472,
            Columbine_Root_Pulp = 0x000A7925,
            Corn = 0x0003366B,
            Crab_Meat = 0x0003366C,
            Daedra_Heart = 0x0001EC8F,
            Daedra_Silk = 0x00033670,
            Daedra_Venin = 0x00033671,
            Daedroth_Teeth = 0x00033672,
            Dragon__s_Tongue = 0x00025039,
            Dreugh_Wax = 0x00033673,
            Dryad_Saddle_Polypore_Cap = 0x0008529D,
            Ectoplasm = 0x0001EBFE,
            Elf_Cup_Cap = 0x0008529E,
            Emetic_Russula_Cap = 0x0008529F,
            Fennel_Seeds = 0x000A7926,
            Fire_Salts = 0x00033675,
            Flax_Seeds = 0x000A7927,
            Flour = 0x00033674,
            Fly_Amanita_Cap = 0x00084471,
            Foxglove_Nectar = 0x00033687,
            Frost_Salts = 0x00022E5B,
            Garlic = 0x00033677,
            Ginkgo_Leaf = 0x00033678,
            Ginseng = 0x00033679,
            Glow_Dust = 0x0001EBE8,
            Grapes = 0x0003367B,
            Green_Stain_Cup_Cap = 0x0008446A,
            Green_Stain_Shelf_Cap = 0x0008529A,
            Ham = 0x0003367C,
            Harrada = 0x0003367D,
            Human_Heart = 0x000CD51C,
            Human_Heart_ = 0x00071F36,
            Human_Skin = 0x00071F35,
            Imp_Fluid = 0x000549BE,
            Imp_Gall = 0x0002EE72,
            Ironwood_Nut = 0x0003367E,
            Jumbo_Potato = 0x00177A2A,
            Lady__s_Mantle_Leaves = 0x000A7928,
            Lady__s_Smock_Leaves = 0x000A7929,
            Lavender_Sprig = 0x000A792A,
            Leek = 0x00033680,
            Lettuce = 0x00033681,
            Lichor = 0x0007049E,
            Mandrake_Root = 0x00033683,
            Milk_Thistle_Seeds = 0x000A792C,
            Minotaur_Horn = 0x00033568,
            Monkshood_Root_Pulp = 0x000A792E,
            Morning_Glory_Root_Pulp = 0x000A792F,
            Mort_Flesh = 0x00033685,
            Motherwort_Sprig = 0x000A7930,
            Mugwort_Seeds = 0x000A7931,
            Mutton = 0x00033686,
            Nightshade = 0x00033688,
            Nirnroot = 0x0004E940,
            Ogre__s_Teeth = 0x00033689,
            Onion = 0x0003368A,
            Orange = 0x0007588E,
            Painted_Troll_Fat = 0x0009209E,
            Pear = 0x0003368B,
            Peony_Seeds = 0x000A7932,
            Pinarus___Prize_Minotaur_Horn = 0x000CBD45,
            Poisoned_Apple = 0x000918F0,
            Potato = 0x0003368C,
            Primrose_Leaves = 0x000A7934,
            Pumpkin = 0x0003368D,
            Radish = 0x0003368E,
            Rat_Meat = 0x0003368F,
            Rat_Poison = 0x00026B08,
            Redwort_Flower = 0x0002503A,
            Refined_Frost_Salts = 0x00022F1A,
            Rice = 0x00033690,
            Root_Pulp = 0x00033691,
            Rumare_Slaughterfish_Scales = 0x00185FE2,
            Sacred_Lotus_Seeds = 0x000A7936,
            Scales = 0x00033692,
            Scamp_Skin = 0x00033693,
            Shepherd__s_Pie = 0x000B97E9,
            S__jirra__s_Famous_Potato_Bread = 0x00177A29,
            Somnalius_Frond = 0x00033696,
            Spiddal_Stick = 0x00033697,
            St_Jahn__s_Wort_Nectar = 0x000A7939,
            Steel___Blue_Entoloma_Cap = 0x0008446B,
            Stinkhorn_Cap = 0x0008446D,
            Strawberry = 0x00033699,
            Summer_Bolete_Cap = 0x00084470,
            Sweetcake = 0x0003369A,
            Sweetroll = 0x0003369B,
            Taproot = 0x000AF06E,
            Tiger_Lily_Nectar = 0x000A792B,
            Tinder_Polypore_Cap = 0x0008446F,
            Tobacco = 0x0003369D,
            Tomato = 0x0003369E,
            Troll_Fat = 0x00026B5C,
            Unicorn_Horn = 0x0001EC5B,
            Vampire_Dust = 0x0004872C,
            Vampire_Dust_ = 0x00009182,
            Venison = 0x0002229B,
            Viper__s_Bugloss_Leaves = 0x000A793B,
            Void_Salts = 0x0003369F,
            Water_Hyacinth_Nectar = 0x000A793C,
            Watermelon = 0x000336A0,
            Wheat_Grain = 0x000336A1,
            White_Seed_Pod = 0x000336A2,
            Wisp_Stalk_Caps = 0x0006251F,
            Wormwood_Leaves = 0x000A793E,
            A_Rusty_Key = 0x00024FE9,
            A_Warlock__s_Luck_Key = 0x0000A076,
            Abandoned_House_Key = 0x000034F3,
            Adrian_Decanius___House = 0x0001777A,
            Aelwin__s_Key = 0x00067DD4,
            Agarmir__s_House_Key = 0x00022BB6,
            Agnete__s_Key = 0x00029187,
            Agronak__s_Mysterious_Key = 0x00038EE9,
            Ahdarji___s_House_Key = 0x00035989,
            Akaviri_Fort_Key = 0x0001C17C,
            Alberic_Litte__s_Key = 0x0002921A,
            Aldos_Othran__s_Key = 0x000034E3,
            Algot__s_House_Key = 0x0001D35C,
            Allectus___Key = 0x0001DA89,
            Alval_Uvani__s_Key = 0x00035982,
            Ancestor_Moth_Key = 0x0009DAC6,
            Ancestor_Moth_Temple = 0x00098307,
            Andragil__s_Key = 0x0000A080,
            Angelie__s_House_Key = 0x00022BB2,
            Antoinetta_Marie__s_Key = 0x000693D9,
            Anvil_Chapel_Key = 0x0000A261,
            Anvil_Dungeon_Key = 0x0000A264,
            Arcane_Univ_Enchanter__s_Key = 0x00092155,
            Archer__s_Paradox_Key = 0x0000A077,
            Arch___Mage__s_Key = 0x00005297,
            Aredil__s_House_Key = 0x00022BDF,
            Aren__s_Strange_Key = 0x00090746,
            Aren__s_Tower_Key = 0x00098308,
            Arkay_Chapel_Undercroft_Key = 0x00091ADA,
            Arnora__s_Chest_Key = 0x00093555,
            Arnora__s_Key = 0x00035E51,
            Arriana_Valga__s_Key = 0x000242B6,
            Arriana_Valga__s_Quarters_Key = 0x000C5581,
            Arvena_Thelas___Key = 0x0000A26A,
            Arvin_Dalvilu__s_Key = 0x0009824D,
            Astinia_Atius___Key = 0x00022BC3,
            Athram_House_Key = 0x00022BB9,
            Ayleid_Cask_Key = 0x000A6570,
            Baenlin__s_Key = 0x00035E48,
            Baeralorn__s_Key = 0x0000C0F4,
            Bantien__s_Key = 0x00022BB4,
            Barash_House_Key = 0x000513C1,
            Basement_Key = 0x00035E50,
            Baurus___Key = 0x0001E702,
            Beldaburo_Key = 0x0005E182,
            Benirus_Manor_Key = 0x00003A97,
            Bernadette_Peneles___Key = 0x00029185,
            Bit_and_Bridle_Key = 0x0001D1FF,
            Black_Brugo__s_Key = 0x00085960,
            Blackwood_Co_Basement_Key = 0x00035705,
            Bleak_Mine_Key = 0x00098251,
            Borba__s_Goods_Key = 0x000034E7,
            Bradon__s_Key = 0x000385A4,
            Bradus___Key = 0x0000A268,
            Bralin__s_Key = 0x000034EE,
            Branwen_and_Saliith__s_Key = 0x000C47B3,
            Bravil_Castle_Key = 0x0000A073,
            Bravil_Chapel_Key = 0x0000A078,
            Bravil_Dungeon_Key = 0x0000A113,
            Brolus___Key = 0x00035E4D,
            Brotch_Calus___Key = 0x00035E56,
            Bruma_Chapel_Key = 0x00036243,
            Bruma_Dungeon_Key = 0x00036261,
            Caminalda__s_Key = 0x0000A95A,
            Canne__s_Key = 0x00029184,
            Captain_Patneim__s_Key = 0x000738E0,
            Captain__s_Key = 0x00049357,
            Carandial__s_Key = 0x0000A07A,
            Casta_Scribonia__s_Key = 0x0002921C,
            Castle_Anvil_Interior_Key = 0x0000A263,
            Castle_Anvil_Key = 0x0000A262,
            Castle_Bravil_Interior_Key = 0x0000A074,
            Castle_Bruma_Interior_Key = 0x00035E45,
            Castle_Bruma_Key = 0x00035E44,
            Castle_Leyawiin_Interior_Key = 0x0003597E,
            Castle_Leyawiin_Key = 0x0003597D,
            Catacombs_Key = 0x0000C585,
            Chanel__s_Key = 0x000242B5,
            Charcoal_Cave_Key = 0x0008BA68,
            Cheydinhal_Bridge_Inn_Key = 0x000034E6,
            Cheydinhal_Castle = 0x000034EB,
            Cheydinhal_Castle_Interior = 0x000034EC,
            Cheydinhal_Chapel_Key = 0x000034EA,
            Cheydinhal_Dungeon_Key = 0x000055D7,
            Chorrol_Castle_Private_Area = 0x000242B2,
            Chorrol_Chapel_Undercroft_Key = 0x00029214,
            Chorrol_Dungeon_Key = 0x0002917D,
            Chorrol_Jail_Key = 0x0002AD2A,
            Cingor__s_Key = 0x00035988,
            City___Swimmer__s_Key = 0x0000A07D,
            Claudius_Arcadia__s_House_Key = 0x00022BDE,
            Coast_Guard_Station_Key = 0x0003598C,
            Commander__s_Chest_Key = 0x00093485,
            Commerce_Office_Key = 0x0004798A,
            Curtis___House_Key = 0x0001D5B2,
            Cyronin_Sintav__s_House_Key = 0x0001781C,
            Dagon_Shrine_Key = 0x00033910,
            Damian_Magius___Key = 0x000534F3,
            Dar_Jee__s_Key = 0x00035980,
            Dareloth__s_Key = 0x000534F4,
            Dark_Fissure_Cell_Key = 0x0005BEA0,
            Derics___Key = 0x00035983,
            Display_Case_Key = 0x00049D21,
            Display_Case_Key_ = 0x0008DD5F,
            Divine_Elegance_Key = 0x0001D205,
            Dorian__s_House_Key = 0x00022BC6,
            Dovyn_Aren__s_Key = 0x000177D3,
            Dro__shanji__s_Key = 0x0002CF34,
            Dul_gro___Shug__s_Key = 0x00017779,
            Dust_Eater_Cell_Key = 0x0006507C,
            Dynari_Amnis___Key = 0x00022BBF,
            Echo_Cave_Key = 0x0001648F,
            Edgar__s_Discount_Spells = 0x00022A72,
            Escape_Route_Key = 0x00028EDE,
            Estelle_Renoit__s_Key = 0x00029212,
            Eugal_Belette__s_Key = 0x0002921B,
            Falcar__s_Key = 0x0002E99F,
            Faregyl_Inn_Key = 0x0000CBDC,
            Fathis_Ules___Key = 0x0001781A,
            Feed_Bag_Key = 0x00022A71,
            Fighters_Guild_Key = 0x0002229E,
            Fighting_Chance_Key = 0x0001D157,
            First_Edition_Key = 0x0001D155,
            First_Hunter__s_Run_Key = 0x0018D13E,
            Flanau_Hlaalu__s_Key = 0x0002917B,
            Fo__c__s__le_Key = 0x0000BC68,
            Forgotten_Key = 0x00074A8F,
            Fort_Blueblood_Key = 0x0003CD2D,
            Fort_Grief_Door_Key = 0x0001FEEE,
            Fort_Grief_Real_Key = 0x0001FEF0,
            Fort_Ontus_Key = 0x0004ECEC,
            Fort_Redman_Key = 0x0009D2D6,
            Fort_Sutch_Gate_Key = 0x00032665,
            Francois_Motierre__s_Key = 0x00029218,
            Fyre_Light_Cave_Key = 0x0005B47F,
            Gallenus_Rosentia__s_Key = 0x00033C71,
            Ganredhel__s_Key = 0x000034F2,
            Garrus_Darelliun__s_Key = 0x0003C2C1,
            Geem_Jasaiin__s_Key = 0x0001EB81,
            Gelebourne__s_Key = 0x000385A2,
            Gharz_House_Key = 0x000034E2,
            Gilen_Norvalo__s_Key = 0x0001D35E,
            Glarthir__s_Key = 0x0001DC48,
            Glarthir__s_Secret_Key = 0x000831BB,
            Gogan__s_House_Key = 0x00064202,
            Gogron_gro___Bolmog__s_Key = 0x0006C677,
            Graman__s_House_Key = 0x0001D35D,
            Grey_Throat__s_House_Key = 0x0001DA91,
            Guildmaster__s_Key = 0x0018D83F,
            Guildmaster__s_Key_ = 0x0006E2EA,
            Gundalas___Key = 0x0003598D,
            Gunder__s_Key = 0x00029186,
            Gweden_Basement_Key = 0x0008696D,
            Hackdirt_Key = 0x00028B7B,
            Hagaer__s_House_Key = 0x0001DA94,
            Hame_Dungeon_Key = 0x00085083,
            Hammer_and_Axe_Key = 0x00035E43,
            Harm__s_Folly_Key = 0x00189D0F,
            Hastrel_Ottus___House_Key = 0x0001D35B,
            Helvius_Cecia__s_Key = 0x00035E54,
            Helvo_Atius___Key = 0x00022BB0,
            Henantier__s_Key = 0x0002CF33,
            Herminia_Cinna__s_House_Key = 0x000177F2,
            High_Fane_Key = 0x0002AF06,
            Honmund__s_Key = 0x00035E47,
            Horn_Cave_Cellar_Key = 0x0005B609,
            Horn_Cave_Storage_Key = 0x0005BC2A,
            Hrol_Ulfgar__s_Key = 0x00098293,
            Ida_Vlinorman__s_House_Key = 0x000177EE,
            Imbel_Family_Crypt_Key = 0x000152FF,
            Imbel_House_Key = 0x0009DB17,
            Imperial_City_Lighthouse_Key = 0x0009096C,
            Imperial_City_Sewers_Key = 0x0004F61A,
            Imperial_Palace_Key = 0x00115E05,
            Imperial_Prison_Key = 0x00092D8A,
            Imperial_Prison_Key_ = 0x000950C2,
            Imperial_Sewer_Key = 0x00027477,
            Imperial_Trading_Company_Key = 0x000534F5,
            Imperial_Watch_Key = 0x00015EAF,
            Imperial_Watch_Office_Key = 0x00086967,
            Iniel_Sintav__s_House_Key = 0x00017775,
            Inventius___Key = 0x0000A267,
            Irene_Metric__s_House_Key = 0x0001777C,
            Iron_Key = 0x00159826,
            Jair__s_Key = 0x000477E4,
            Jakben_Imbel__s_House_Key = 0x00022BC8,
            Jastia_Sintav__s_House_Key = 0x00017819,
            J__bari__s_Key = 0x00035981,
            Jearl__s_Key = 0x0002007F,
            Jeetum_Ze__s_Room_Key = 0x000A5650,
            Jenseric__s_Cabin_Key = 0x00064615,
            Jenseric__s_House_Key = 0x0001DB08,
            Jensine__s_Merchandise = 0x0001D201,
            Jesan_Sextius___Key = 0x0000A26D,
            Jewelry_Box_Key = 0x0000A9D7,
            J__Ghasta__s_Key = 0x00035E55,
            Jirolin_Doran__s_Key = 0x00029219,
            J__mhad__s_House_Key = 0x00022BE0,
            Kalthar__s_Key = 0x0000CF21,
            Kastus_Sintav__s_House_Key = 0x00017817,
            Key = 0x0006DF2E,
            Key_ = 0x0003EE5D,
            Key_of_Hidden_Wealth = 0x000C59A2,
            King_and_Queen_Tavern_Key = 0x00022B6E,
            Kvatch_Guard_House_Key = 0x00031D31,
            Kvinchal__s_Key = 0x000477E3,
            Lazare_Milvan__s_Key = 0x00029190,
            Lelles___Quality_Mercandise_Key = 0x0000A25F,
            Leyawiin_Chapel_Key = 0x00069B16,
            Leyawiin_City_Watch_Key = 0x0003597F,
            Leyawiin_Dungeon_Key = 0x00035F8C,
            Leyawiin_Secret_Room_Key = 0x00092156,
            Lighthouse_Cellar_Key = 0x00054031,
            Lighthouse_Key = 0x0000A25E,
            Lindai__s_Royal_Tomb_Key = 0x000A55FC,
            Lirrians___Key = 0x00035E4B,
            Llevana_Nedaren__s_Key = 0x000034E4,
            Loche__s_House_Key = 0x0000A081,
            Lorkmir__s_House_Key = 0x00017776,
            Lorkmir__s_House_Key_ = 0x00017833,
            Lost_Boy__s_Key = 0x000998CF,
            Luciana_Galena__s_Key = 0x0000A07F,
            Luronk__s_House_Key = 0x0001DA8F,
            Lyra_Rosentia__s_Key = 0x00035E49,
            Mach___Na__s_Key = 0x000034E8,
            Mages_Guild_Key = 0x0002229C,
            Magul_House_Key = 0x000034E1,
            Malyani_Dalvilu__s_Key = 0x00098298,
            Marana_Rian__s_House_Key = 0x0001DA96,
            Margarte__s_Key = 0x00035985,
            Marinus_Catiotus___House = 0x000177EF,
            Matthias___House_Key = 0x00022BBB,
            Medrike__s_Key = 0x0000C32A,
            Methredhel__s_House_Key = 0x0005202F,
            Mine_Key = 0x0000295D,
            Mirabelle_Monet__s_Key = 0x0000A265,
            Miscarcand_Key = 0x000C58B6,
            Modryn_Oreyn__s_Key = 0x00029220,
            Moranda_Gate_Key = 0x00051871,
            Mraaj___Dar__s_Key = 0x0006FDA7,
            Murgakh_gro___Ushag__s_Key = 0x0002921E,
            My_Bravil_House_Key = 0x0004ED80,
            My_Bruma_House_Key = 0x0004ED88,
            My_Cheydinhal_House_Key = 0x0004ED87,
            My_Chorrol_House_Key = 0x0004ED8A,
            My_Imperial_City_House_Key = 0x0005319E,
            My_Leyawiin_House_Key = 0x0004ED86,
            My_Skingrad_House_Key = 0x0004ED89,
            Mystic_Emporium_Key = 0x0001D1FE,
            NE_Watch_Tower_Key = 0x00053FF3,
            Necromancer__s_Moss_Rock_Key = 0x0005B912,
            Neville__s_Chest_Key = 0x000852DB,
            Newheim__s_Key = 0x0000A269,
            Newlands_Lodge_Key = 0x000034E5,
            Ninedava_Gate_Key = 0x00051F8D,
            Nivan_Dalvilu__s_Key = 0x0009829E,
            Nord_Winds_Key = 0x00035E3F,
            Novaroma_Key = 0x00035E3E,
            NW_Watch_Tower_Key = 0x00053FF4,
            Oaken___Hull__s_Key = 0x0000A266,
            Odiil_Farm_Key = 0x000C89DD,
            Ohtimbar__s_Key = 0x000034F1,
            Old_Key = 0x00074A8E,
            Ongar__s_Key = 0x00035E4E,
            Ontus_Vanin__s_Key = 0x00022BBA,
            Ormil__s_Cabin_Key = 0x0000C1FE,
            Ormil__s_Cabin_Key_ = 0x0007BAE0,
            Orthe__s_Key = 0x0000C329,
            Orum_House_Key = 0x000034EF,
            Othrelos___House_Key = 0x000177F0,
            Otumeel__s_Key = 0x0003598A,
            Pennus_Mallius___House_Key = 0x0001D5B3,
            Plotius___Key = 0x0003598B,
            Pot_Hole_Cell_Key = 0x0005BD64,
            Prison_Cell_Key = 0x00023FFD,
            Quill___Weave__s_Key = 0x0000A26B,
            Ra__Jahirr__s_Key = 0x00035987,
            Ra__jhan__s_House_Key = 0x000177F3,
            Ra__jiradh__s_House_Key = 0x00022BC1,
            Ranaline__s_Key = 0x0000A07E,
            Rasheda__s_Key = 0x00029213,
            Raynil__s_Key = 0x000385A3,
            Raynil__s_Room_Key = 0x00038449,
            Redas_Dalvilu__s_Key = 0x000982A3,
            Redwater_Slough_Key = 0x00070248,
            Regner__s_Key = 0x00035E53,
            Reman_Broder__s_Key = 0x00029188,
            Reynald_Jemane__s_Key = 0x0002921D,
            Rimalus_Bruiant__s_Key = 0x00029216,
            Rindir__s_Staffs_Key = 0x0001D152,
            Ritual_Key = 0x00033911,
            Riverview_Key = 0x000034F0,
            Ri__Zakar__s_Room_Key = 0x000A564F,
            Roderic_Pierrane__s_Key = 0x00017816,
            Roebeck_Bridge_Room_Key = 0x00026CD5,
            Ruslan__s_House_Key = 0x0001DA92,
            Rusty_Key = 0x00074A8D,
            Rythe__s_Studio_Key = 0x0001BC2C,
            Salmo__s_Key = 0x00029189,
            Salomon_Geonette__s_House_Key = 0x0001DA8B,
            Sancre_Tor_Key = 0x0000A23F,
            Sanctuary_Well_Key = 0x0000B258,
            Satha_Dalvilu__s_Key = 0x000982A7,
            Scinia_Crypt_Key = 0x000568AD,
            Sea_Tub_Clarabella_Key = 0x000738DF,
            Second_Hunter__s_Run_Key = 0x0018D13F,
            Secret_Cell_Key = 0x000838CE,
            Seed___Neeus___Key = 0x00029211,
            Seridur__s_House_Key = 0x0001DA8E,
            Serpent__s_Wake_Captain__s_Key = 0x0007B73E,
            Serpent__s_Wake_Key = 0x0000A26E,
            Severius_Atius___Key = 0x00022BAF,
            Sewer_Gate_Key = 0x000274F5,
            Shameer__s_Key = 0x0002918A,
            Shardrock_Key = 0x0018BA0E,
            Shiny_Ogre_Cage_Key = 0x0001EC48,
            Shrine_of_Dagon_Key = 0x0001F14C,
            Sigil_Keep_Key = 0x00093EF5,
            Sigil_Key = 0x0009504C,
            Sigil_Key_ = 0x0009C225,
            Sinderion__s_Key = 0x00068B05,
            Skingrad_Castle_Key = 0x0002C461,
            Skingrad_Castle_Silver_Key = 0x0002C460,
            Skingrad_Chapel_Key = 0x00069B17,
            Skingrad_Dungeon_Key = 0x00035F8D,
            Skingrad_Guardhouse_Key = 0x0002ADAC,
            S__krivva__s_Key = 0x0000A07C,
            Slash___N_Smash_Key = 0x0001D200,
            Sleeping_Mare_Key = 0x0000525D,
            Small_Key = 0x000366BC,
            Smoke_Tomb_Inner_Key = 0x00083FB5,
            Smoke_Tomb_Outer_Key = 0x00083FB4,
            Smuggler__s_Key = 0x000A943C,
            Soris_Arenim__s_Key = 0x0002C677,
            South_Watch_Tower_Key = 0x00053FF2,
            Southern_Books_Key = 0x00026085,
            S__rathad__s_House_Key = 0x00022BC5,
            Stantus_Varrid__s_House_Key = 0x0001D359,
            Stonewall_Shields_Key = 0x0001D156,
            Storage_Room_Key = 0x0000C1FC,
            Storeroom_Key = 0x00025099,
            Summitmist_Manor_Key = 0x000837D1,
            Sunken_Sewers_Key = 0x0009588D,
            Surilie__s_House_Key = 0x0002918B,
            Surius_Afranius___House_Key = 0x0001DA98,
            Talasma__s_Key = 0x0009853D,
            Tamika__s_Key = 0x0002918C,
            Tarnished_Ogre_Cage_Key = 0x0001EC41,
            Teekeeus___Key = 0x00025067,
            Teinaava__s_Key = 0x000693DA,
            Telaendril__s_Key = 0x0006FDA8,
            Tertius_Favonius___House_Key = 0x000177F1,
            Thamriel__s_House_Key = 0x00022BC0,
            The_Best_Defense_Key = 0x0001D204,
            The_Bloodwork__s_Sewer_Key = 0x000677DB,
            The_Copious_Coinpurse_Key = 0x0001D154,
            The_Fair_Deal_Key = 0x0000A075,
            The_First_Key_of_Dark_Hate = 0x00014D63,
            The_Gilded_Carafe_Key = 0x0001D202,
            The_Main_Ingredient_Key = 0x0001D153,
            The_March_Rider_Key = 0x000034E9,
            The_Second_Key_of_Dark_Hate = 0x00014D64,
            Three_Brothers_Trade_Goods_Key = 0x0001D206,
            Toadstool_Grog_Key = 0x000974E8,
            Top_Deck_Key = 0x0000C1FD,
            Toutius_Sextius___Key = 0x0002918D,
            Trentius_Mausoleum_Key = 0x000CA22C,
            Trenus_Duronius___House_Key = 0x0001D35A,
            Tun___Zeeus___Key = 0x0003597C,
            Ulrich_Leland__s_Key = 0x0003F31A,
            Ulrika_Ulfgar__s_Key = 0x000982AA,
            Umbacano_Manor_Key = 0x0002B455,
            Umbacano__s_Key = 0x0002B456,
            Undena_Orethi__s_Key = 0x0002918E,
            Underpall_Gate_Key = 0x0004E151,
            Ungolim__s_Key = 0x0000A07B,
            Urasek_Lockup_Key = 0x000CA6DF,
            Usheeja__s_House_Key = 0x00022BC4,
            Uuras___Key = 0x0002918F,
            Vahtacen_Ruins_Key = 0x0005401E,
            Valen_Dreth__s_Cell_Key = 0x000950C0,
            Valtieri__s_Key = 0x000693D8,
            Valus_Odiil__s_Key = 0x0002921F,
            Varel_Morvayn__s_Key = 0x0000A260,
            Varon_Vamori__s_Key = 0x0000A079,
            Vilena_Donton__s_Key = 0x00029217,
            Vilverin_Chamber_Key = 0x0006C323,
            Vilverin_Sepulcher_Key = 0x000C76CD,
            Warden_Kastav__s_Key = 0x00035DAB,
            Warehouse_Key = 0x0001D203,
            Wariel__s_Key = 0x00026836,
            Waterfront_Sewers_Key = 0x00026089,
            Weebam___Na__s_Key = 0x00035986,
            Well_Key = 0x000084D3,
            Wellspring_Cave_Key = 0x0006B396,
            West_Weald_Inn_Key = 0x000979ED,
            White_Stallion_Lodge_Key = 0x000908C1,
            Whitmond_Farm_Key = 0x0000CBEC,
            Willow_Bank_Key = 0x000034ED,
            Wumeek__s_Key = 0x0001EB7E,
            Apprentice_Alembic = 0x0006E310,
            Apprentice_Calcinator = 0x0006E311,
            Apprentice_Mortar_and_Pestle = 0x0006E312,
            Apprentice_Retort = 0x0006E313,
            Expert_Alembic = 0x0006EE5C,
            Expert_Calcinator = 0x0006EE5E,
            Expert_Mortar_and_Pestle = 0x0006EE60,
            Expert_Retort = 0x0006EE62,
            Journeyman_Alembic = 0x0006EE52,
            Journeyman_Calcinator = 0x0006EE55,
            Journeyman_Mortar_and_Pestle = 0x0006EE57,
            Journeyman_Retort = 0x0006EE59,
            Master_Alembic = 0x0006EE64,
            Master_Calcinator = 0x0006EE66,
            Master_Mortar_and_Pestle = 0x0006EE68,
            Master_Retort = 0x0006EE6A,
            Novice_Alembic = 0x00010604,
            Novice_Calcinator = 0x0001057D,
            Novice_Mortar_and_Pestle = 0x000C7968,
            Novice_Mortar_and_Pestle_ = 0x000105E3,
            Novice_Retort = 0x00000C4F,
            Azura__s_Star = 0x00000193,
            Black_Soul_Gem = 0x00000192,
            Black_Soul_Gem_ = 0x000382E0,
            Black_Soul_Gem__ = 0x0003C7FC,
            Black_Soul_Gem___ = 0x0001EC14,
            Colossal_Black_Soul_Gem = 0x000C6988,
            Colossal_Black_Soul_Gem_ = 0x00015208,
            Common_Soul_Gem = 0x00015B8B,
            Common_Soul_Gem_ = 0x000382D4,
            Common_Soul_Gem__ = 0x000382D5,
            Common_Soul_Gem___ = 0x000382D6,
            Grand_Soul_Gem = 0x00015B8E,
            Grand_Soul_Gem_ = 0x000382DB,
            Grand_Soul_Gem__ = 0x000382DC,
            Grand_Soul_Gem___ = 0x000382DD,
            Grand_Soul_Gem____ = 0x000382DE,
            Grand_Soul_Gem_____ = 0x000382DF,
            Greater_Soul_Gem = 0x00015B8C,
            Greater_Soul_Gem_ = 0x000382D7,
            Greater_Soul_Gem__ = 0x000382D8,
            Greater_Soul_Gem___ = 0x000382D9,
            Greater_Soul_Gem____ = 0x000382DA,
            Lesser_Soul_Gem = 0x00023D69,
            Lesser_Soul_Gem_ = 0x000382D2,
            Lesser_Soul_Gem__ = 0x000382D3,
            Petty_Soul_Gem = 0x00023D67,
            Petty_Soul_Gem_ = 0x000382D1,
            Agarmir__s_Shovel = 0x0001D0AF,
            Akaviri_Orders = 0x000785E0,
            Akaviri_Orders_ = 0x0001C173,
            Argonian_Heart = 0x0003558F,
            Ayleid_Statue = 0x000844C4,
            Ayleid_Statue_ = 0x0002AB48,
            Basket = 0x00024D91,
            Basket_ = 0x00024D92,
            Bear_Pelt = 0x000228E3,
            Blood_Potion = 0x001192AE,
            Blue_Cheese = 0x000D3B25,
            Bone = 0x000223A8,
            Bone_ = 0x00023880,
            Bone__ = 0x00023881,
            Bone___ = 0x000180DE,
            Bone____ = 0x0001DE26,
            Bones = 0x00023882,
            Bouquet_of_Flowers = 0x00049CFD,
            Bowl = 0x000105D5,
            Bowl_ = 0x000105D7,
            Bowl__ = 0x000105D9,
            Bowl___ = 0x0001F1D9,
            Bowl____ = 0x0001F1DA,
            Bowl_____ = 0x0001F1DB,
            Broom = 0x000250FF,
            Broom_ = 0x00019113,
            Brush_Jar = 0x0001BBCB,
            Brush_of_Truepaint = 0x0001BB36,
            Calipers = 0x0001C66C,
            Carved_Panel = 0x0002AF03,
            Ceramic_Bowl = 0x0002507A,
            Ceramic_Cup = 0x0002507B,
            Ceramic_Goblet = 0x0002507C,
            Ceramic_Pitcher = 0x0002507D,
            Ceramic_Pitcher_ = 0x0002507E,
            Ceramic_Plate = 0x0002507F,
            Ceramic_Tankard = 0x0002724F,
            Ceramic_Urn = 0x00025080,
            Ceramic_Vase = 0x00025081,
            Cheddar_Cheese = 0x000D3B2F,
            Clay_Bowl = 0x00022295,
            Clay_Bowl_ = 0x00022298,
            Clay_Bowl__ = 0x00022299,
            Clay_Cup = 0x00022538,
            Clay_Cup_ = 0x00022539,
            Clay_Goblet = 0x0002253C,
            Clay_Pitcher = 0x00022543,
            Clay_Pitcher_ = 0x00022544,
            Clay_Pitcher__ = 0x00022545,
            Clay_Plate = 0x0002254C,
            Clay_Plate_ = 0x0002254D,
            Clay_Tankard = 0x00022559,
            Clay_Urn = 0x00022553,
            Clay_Vase = 0x00022557,
            Cloth = 0x0000087A,
            Cloth_ = 0x0000088D,
            Cloth__ = 0x0000089F,
            Cloth___ = 0x000008D8,
            Cloth____ = 0x000008E6,
            Colossal_Black_Soul_Gem__ = 0x00007E9C,
            Covered_Pot = 0x000105DF,
            Covered_Pot_ = 0x000105E0,
            Crumpled_Piece_of_Paper_________ = 0x000A9353,
            Crumpled_Piece_of_Paper__________ = 0x000A9355,
            Crumpled_Piece_of_Paper___________ = 0x000A9357,
            Crumpled_Piece_of_Paper____________ = 0x000BF14D,
            Crystal_Ball = 0x0001048C,
            Crystal_Ball_ = 0x00035F86,
            Crystal_Ball__ = 0x0001C60D,
            Diamond = 0x00038BA7,
            Dirty_Shoes = 0x0009F8FD,
            Element_of_Courage = 0x0002C6EF,
            Element_of_Patience = 0x0002C6EE,
            Element_of_Perception = 0x0002C6F0,
            Element_of_Resolve = 0x0002C6ED,
            Emerald = 0x00038BA4,
            Empty_Hist_Bottle = 0x0008996E,
            Eye_of_Nocturnal = 0x00026AC9,
            Filled_Colossal_Black_Soul_Gem = 0x00007E9D,
            Finger_of_Adamus_Phillida = 0x001778DB,
            Fingers_of_the_Mountain_ = 0x0000510D,
            Flawed_Diamond = 0x00038BA8,
            Flawed_Emerald = 0x00038BA6,
            Flawed_Pearl = 0x00038B99,
            Flawed_Ruby = 0x00038B9F,
            Flawed_Sapphire = 0x00038BA2,
            Flawed_Topaz = 0x00038B9C,
            Flawless_Diamond = 0x00038BA9,
            Flawless_Emerald = 0x00038BA5,
            Flawless_Pearl = 0x00038B9A,
            Flawless_Ruby = 0x00038BA0,
            Flawless_Sapphire = 0x00038BA3,
            Flawless_Topaz = 0x00038B9D,
            Folded_Cloth = 0x000008DA,
            Folded_Cloth_ = 0x000008DC,
            Folded_Cloth__ = 0x00000D41,
            Folded_Cloth___ = 0x00000D52,
            Folded_Cloth____ = 0x00000D76,
            Fork = 0x00019116,
            Garridan__s_Tear = 0x00025B33,
            Garridan__s_Tear_ = 0x0007BE48,
            Glass_of_Time = 0x0002A60B,
            Gold = 0x0000000F,
            Gold_Nugget = 0x00049808,
            Great_Sigil_Stone = 0x0003844E,
            Great_Welkynd_Stone = 0x000345AF,
            Hand_Scythe = 0x00025100,
            Hermaeus_Mora__s_Soul_Gem = 0x0008B61E,
            Hoe = 0x00025101,
            Hourglass = 0x0001C666,
            Imperial_Legion_Seal = 0x0000C22C,
            Inkwell = 0x00023D61,
            Key___Shaped_Arrowhead = 0x0001482C,
            Knife = 0x00019117,
            Ladle = 0x000105DB,
            Languorwine_Antidote = 0x00028276,
            Lion_Pelt = 0x000228E4,
            Llathasa__s_Bust = 0x00008032,
            Llathasa__s_Bust_ = 0x0001E861,
            Lockpick = 0x0000000A,
            Loose_Pipe = 0x00089783,
            Magicka_Potion = 0x001192AF,
            Marana_Rian__s_Coin = 0x000613C0,
            Marble_Cheese = 0x000D3B2A,
            Metal_Bowl = 0x000CD400,
            Metal_Goblet = 0x000CD401,
            Metal_Plate = 0x000CD402,
            Metal_Tankard = 0x000CD403,
            Metal_Tankard_ = 0x000CD404,
            Metal_Tankard__ = 0x000CD405,
            Metal_Tankard___ = 0x000CD406,
            Metal_Tankard____ = 0x000CD407,
            Metal_Urn = 0x000CD4DB,
            Metal_Urn_ = 0x000CD4DC,
            Metal_Urn__ = 0x000CD4E8,
            Metal_Urn___ = 0x000CD4E9,
            Mother__s_Head = 0x0006AAA5,
            Newheim__s_Heirloom = 0x00002E90,
            Oaken___Hall_Heirloom = 0x000CBD41,
            Oar = 0x00025102,
            Olroy_Cheese = 0x0008C26A,
            Orb_of_Vaermina = 0x0001EC18,
            Paint_Brush = 0x0001BBC5,
            Paint_Brush_ = 0x0001BBC6,
            Paint_Brush__ = 0x0001BBC7,
            Paint_Brush___ = 0x0001BBC8,
            Paint_Brush____ = 0x0001BBC9,
            Paint_Brush_____ = 0x0001BBCA,
            Paint_Palette = 0x0001BBCC,
            Pearl = 0x00038B98,
            Pewter_Bowl = 0x0001C624,
            Pewter_Bowl_ = 0x0001C625,
            Pewter_Cup = 0x000104C0,
            Pewter_Cup_ = 0x000104C1,
            Pewter_Cup__ = 0x0001C616,
            Pewter_Cup___ = 0x0001C619,
            Pewter_Fork = 0x0001C622,
            Pewter_Knife = 0x0001C620,
            Pewter_Mug = 0x0000FD93,
            Pewter_Mug_ = 0x0000FDA4,
            Pewter_Mug__ = 0x0000FDAC,
            Pewter_Pitcher = 0x0001C61F,
            Pewter_Plate = 0x0001C626,
            Pewter_Plate_ = 0x0001C627,
            Pewter_Pot = 0x0001C61A,
            Pewter_Pot_ = 0x0001C61B,
            Pewter_Pot__ = 0x0001C61C,
            Pewter_Spoon = 0x0001C623,
            Pickaxe = 0x00025103,
            Pickaxe_ = 0x000180E0,
            Pitchfork = 0x00025113,
            Planter = 0x00015BB0,
            Planter_ = 0x00015BB3,
            Planter__ = 0x00015BB4,
            Planter___ = 0x00015BB5,
            Planter____ = 0x00015BB6,
            Planter_____ = 0x00015BB7,
            Planter______ = 0x00015BB8,
            Planter_______ = 0x00015BBA,
            Planter________ = 0x00015BBB,
            Plate = 0x00019118,
            Quill = 0x00023D63,
            Rake = 0x00025116,
            Recently_Used_Pickaxe = 0x0009F8FA,
            Red_Bowl = 0x000836CE,
            Repair_Hammer = 0x0000000C,
            Reward_Painting = 0x00034E62,
            Roderick__s_Medicine = 0x00030FD7,
            Roderick__s_Poison = 0x00030FD6,
            Rolled_Up_Portrait = 0x00034E52,
            Ruby = 0x00038B9E,
            Rufio__s_Skull = 0x000918FA,
            Sands_of_Resolve = 0x0002FB3F,
            Sapphire = 0x00038BA1,
            Savilla__s_Stone = 0x000C4A2C,
            Savilla__s_Stone_ = 0x00014706,
            Scales_ = 0x000105F3,
            Scales_of_Pitiless_Justice = 0x000918FD,
            Scythe = 0x0002511A,
            Shears = 0x0002511C,
            Shovel = 0x000251A8,
            Silver_Bowl = 0x00000C08,
            Silver_Bowl_ = 0x000105E7,
            Silver_Carafe = 0x00019107,
            Silver_Fork = 0x0001924A,
            Silver_Glass = 0x0001DBFC,
            Silver_Goblet = 0x0001DCCC,
            Silver_Goblet_ = 0x0001DCCF,
            Silver_Knife = 0x00023C13,
            Silver_Nugget = 0x00049809,
            Silver_Pitcher = 0x00023C15,
            Silver_Pitcher_ = 0x00023C17,
            Silver_Plate = 0x00023C19,
            Silver_Spoon = 0x00023C1B,
            Silver_Tankard = 0x00023C1D,
            Silver_Urn = 0x00023C1F,
            Silver_Urn_ = 0x00023C21,
            Silver_Vase = 0x00023C23,
            Skeletal_Hand = 0x00003A99,
            Skeleton_Key = 0x0000000B,
            Skull = 0x00023F6E,
            Spoon = 0x00019119,
            Statuette_of_a_Dog = 0x00026B25,
            Statuette_of_a_Dog_ = 0x0007B7B0,
            Stone_Brick = 0x00025946,
            Stone_Brick_ = 0x00025947,
            Stone_Brick__ = 0x00025948,
            Stone_Brick___ = 0x00025949,
            Stone_Brick____ = 0x0002594A,
            Stone_Cup = 0x000104C7,
            Stone_Cup_ = 0x000104C9,
            Stone_Cup__ = 0x00019115,
            Stone_Mug = 0x000104CB,
            Stone_of_St_Alessia = 0x00003949,
            Stone_Pitcher = 0x000105DD,
            Svenja__s_Arm = 0x000C55E1,
            Svenja__s_Head = 0x000C55E2,
            Svenja__s_Leg = 0x000C55E0,
            Svenja__s_Torso = 0x000C55E3,
            Tan_Bowl = 0x00022535,
            Tan_Bowl_ = 0x00022536,
            Tan_Bowl__ = 0x00022537,
            Tan_Bowl___ = 0x000836CF,
            Tan_Cup = 0x0002253A,
            Tan_Cup_ = 0x0002253B,
            Tan_Cup__ = 0x0001F1DC,
            Tan_Cup___ = 0x0001F1DD,
            Tan_Cup____ = 0x0001F1DE,
            Tan_Goblet = 0x00022542,
            Tan_Jug = 0x0001F1DF,
            Tan_Jug_ = 0x0001F1E0,
            Tan_Jug__ = 0x0001F1E1,
            Tan_Jug___ = 0x0001F1E2,
            Tan_Jug____ = 0x0001F1E3,
            Tan_Jug_____ = 0x0001F1E4,
            Tan_Mug = 0x0001F1E5,
            Tan_Pitcher = 0x00022546,
            Tan_Pitcher_ = 0x00022547,
            Tan_Pitcher__ = 0x00022548,
            Tan_Pitcher___ = 0x0001F1E6,
            Tan_Plate = 0x0002254E,
            Tan_Plate_ = 0x0002254F,
            Tan_Plate__ = 0x0001F1E7,
            Tan_Tankard = 0x00022550,
            Tan_Urn = 0x00022554,
            Tan_Urn_ = 0x00022555,
            Tan_Urn__ = 0x00022556,
            Tan_Vase = 0x00022558,
            Tongs = 0x000251AC,
            Tongs_ = 0x0001C66F,
            Topaz = 0x00038B9B,
            Urn = 0x00022551,
            Urn_ = 0x00022552,
            Vampirism_Cure_Potion = 0x0009812D,
            Varla_Stone = 0x00000194,
            Varulae__s_Crystal_Ball = 0x00095A38,
            Wedding_Gift = 0x000CBD43,
            Weight = 0x0001C5ED,
            Welkynd_Stone = 0x00000191,
            West_Weald_Bear_Fang = 0x0018AE3F,
            Wolf_Pelt = 0x000228E2,
            Yarn = 0x00025205,
            Yarn_ = 0x00025206,
            Torch = 0x00084AA2,
            Torch_ = 0x0002CF9F,
            Amber_Arrows_Matrix = 0x00016CF1,
            Amber_Boots_Matrix = 0x00016561,
            Amber_Bow_Matrix = 0x00016CF2,
            Amber_Cuirass_Matrix = 0x00016562,
            Amber_Gauntlets_Matrix = 0x00016560,
            Amber_Greaves_Matrix = 0x0001657F,
            Amber_Hammer_Matrix = 0x00016CF3,
            Amber_Helmet_Matrix = 0x0001657E,
            Amber_Mace_Matrix = 0x0001F3E8,
            Amber_Shield_Matrix = 0x0001657D,
            Amber_Sword_Matrix = 0x00016CF4,
            Amber = 0x00016568,
            Blind_Watchers_Eye = 0x0004340E,
            Dagger_of_Friendship = 0x00081E65,
            Deformed_Swamp_Tentacle = 0x00043410,
            Diadem_of_Euphoria = 0x0007B6B1,
            Dins_Ashes = 0x00081E61,
            Hounds_Tooth_Key = 0x00081E63,
            Madness_Arrows_Matrix = 0x0001F3D5,
            Madness_Axe_Matrix = 0x0001F3EE,
            Madness_Boots_Matrix = 0x0001F3D6,
            Madness_Bow_Matrix = 0x0001F3D4,
            Madness_Claymore_Matrix = 0x0001F3D3,
            Madness_Cuirass_Matrix = 0x0001F3D3,
            Madness_Gauntlets_Matrix = 0x0001F3D2,
            Madness_Greaves_Matrix = 0x0001F3D1,
            Madness_Helmet_Matrix = 0x0001F3CF,
            Madness_Ore = 0x00012D33,
            Madness_Shield_Matrix = 0x0001F3CE,
            Madness_Sword_Matrix = 0x0001F3CD,
            Mixing_Bowl = 0x00081E6E,
            Mute_Screaming_Maw = 0x00081E9A,
            Nerveshatter = 0x0007E09D,
            Pelvis_of_Pelagius = 0x0006D0F3,
            Ring_of_Disrobing = 0x00081E68,
            Shadowrend_Axe = 0x0009535E,
            Shadowrend_Sword = 0x00095360,
            Sheogorath_Shaped_Amber = 0x00078977,
            Soul_Tomato = 0x00081E60,
            Two_Headed_Coin = 0x00081E5E
        }
    }
}
