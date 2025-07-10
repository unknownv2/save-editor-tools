using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Reflection;
using Ionic.Zlib;

namespace Horizon.PackageEditors.Skyrim
{
    class SkyrimSave
    {
        private const string Magic = "TESV_SAVEGAME";

        public SkyHeader Header;
        public byte[] Thumbnail;
        public byte FormVersion;
        public SkyPluginList PluginList;
        public SkyLocationTable LocationTable;
        public GlobalDataList DataTable1, DataTable2, DataTable3;
        public ChangeFormList ChangeForms;
        public SkyFormIDList FormIDs;

        private StringTable _stringTable;
        private uint[] _unknownTable1 = null;

        public const byte LatestVersion = 0x4A;

        public SkyrimSave(EndianIO IO)
        {
            if (IO.In.ReadAsciiString(0x0D) != Magic)
                throw new Exception("Invalid savegame magic!");

            IO.In.ReadInt32(); // Header length
            this.Header = new SkyHeader(IO);

            this.Thumbnail = IO.In.ReadBytes(Header.ThumbnailSize.Width * Header.ThumbnailSize.Height * 3);

            this.FormVersion = IO.In.ReadByte();
            if (FormVersion > LatestVersion)
                throw new Exception("Skyrim: current save version is newer than latest supported version. Please contact a developer.");

            IO.Position += 4; // list size
            this.PluginList = new SkyPluginList(IO);

            this.LocationTable = new SkyLocationTable(IO);

            IO.Position = this.LocationTable.FormIDs;
            this.FormIDs = new SkyFormIDList(IO);
            
            int unkTblLen = IO.In.ReadInt32();
            this._unknownTable1 = new uint[unkTblLen];
            for (int x = 0; x < unkTblLen; x++)
                this._unknownTable1[x] = IO.In.ReadUInt32();
            
            IO.Position = this.LocationTable.FooterStringTable;
            IO.Position += 4; // table size
            this._stringTable = new StringTable(IO);

            IO.Position = this.LocationTable.GlobalDataTable1;
            this.DataTable1 = new GlobalDataList(IO, this.LocationTable.GlobalDataTable1Count);

            IO.Position = this.LocationTable.GlobalDataTable2;
            this.DataTable2 = new GlobalDataList(IO, this.LocationTable.GlobalDataTable2Count);

            IO.Position = this.LocationTable.ChangeForms;
            this.ChangeForms = new ChangeFormList(IO, this.LocationTable.ChangeFormsCount);

            // Holds data for Skyrim's Papyrus VM Engine 
            IO.Position = this.LocationTable.GlobalDataTable3;
            this.DataTable3 = new GlobalDataList(IO, this.LocationTable.GlobalDataTable3Count + 1);

        }

        public void Write(EndianIO IO)
        {
            IO.Out.Write(Encoding.ASCII.GetBytes(Magic));

            byte[] temp = this.Header.ToArray();
            IO.Out.Write(temp.Length);
            IO.Out.Write(temp);

            IO.Out.Write(this.Thumbnail);

            IO.Out.Write(this.FormVersion);

            temp = this.PluginList.ToArray();
            IO.Out.Write(temp.Length);
            IO.Out.Write(temp);

            long locationTableAddress = IO.Position;
            IO.Position += SkyLocationTable.Length;

            this.LocationTable.GlobalDataTable1 = (uint)IO.Position;
            this.LocationTable.GlobalDataTable1Count = this.DataTable1.Count;
            this.DataTable1.Write(IO);

            this.LocationTable.GlobalDataTable2 = (uint)IO.Position;
            this.LocationTable.GlobalDataTable2Count = this.DataTable2.Count;
            this.DataTable2.Write(IO);

            this.LocationTable.ChangeForms = (uint)IO.Position;
            this.LocationTable.ChangeFormsCount = this.ChangeForms.Count;
            this.ChangeForms.Write(IO);

            this.LocationTable.GlobalDataTable3 = (uint)IO.Position;
            this.LocationTable.GlobalDataTable3Count = this.DataTable3.Count - 1;
            this.DataTable3.Write(IO);

            this.LocationTable.FormIDs = (uint)IO.Position;
            this.FormIDs.Write(IO);
            
            IO.Out.Write(this._unknownTable1.Length);
            for (int x = 0; x < this._unknownTable1.Length; x++)
                IO.Out.Write(this._unknownTable1[x]);
            
            this.LocationTable.FooterStringTable = (uint)IO.Position;
            temp = this._stringTable.ToArray();
            IO.Out.Write(temp.Length);
            IO.Out.Write(temp);

            IO.Position = locationTableAddress;
            this.LocationTable.Write(IO);
        }
    }

    public class StringTable : List<string>
    {
        public StringTable(EndianIO IO)
        {
            int numStrings = IO.In.ReadInt32();
            this.Capacity = numStrings;

            for (int x = 0; x < numStrings; x++)
                this.Add(IO.In.ReadAsciiString(IO.In.ReadInt16()));
        }

        public new byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);
            IO.Out.Write(this.Count);
            foreach (string str in this)
            {
                IO.Out.Write((short)str.Length);
                IO.Out.Write(Encoding.ASCII.GetBytes(str));
            }
            IO.Close();
            return IO.ToArray();
        }
    }

    public enum FormOrigin
    {
        Index = 0,
        Skyrim = 1,
        Plugin = 2,
    }

    public class RefID
    {
        private int _formId;

        public int FormID
        {
            get { return _formId & 0x3FFFFF; }
            set { _formId = (value & 0x3FFFFF); }
        }
        public int ID
        {
            get { return (FormID | ((int) FormType << 22)); }
        }
        public FormOrigin FormType;
        public RefID(){}
        public RefID(int refId)
        {
            FormType = (FormOrigin)(refId >> 22);
            FormID = (refId & 0x3FFFFF);
        }

        public int ToInt24()
        {
            return ID;
        }
    }

    public struct SkyHeader
    {
        public int Version, SaveNumber, Level;
        public string Name, Location, TimestampS, Race;
        public Size ThumbnailSize;
        public short Unknown;
        public float UnknownF1, UnknownF2;
        public DateTime Timestamp;

        public SkyHeader(EndianIO IO)
        {
            this.Version = IO.In.ReadInt32();
            this.SaveNumber = IO.In.ReadInt32();
            this.Name = IO.In.ReadAsciiString(IO.In.ReadInt16());
            this.Level = IO.In.ReadInt32();
            this.Location = IO.In.ReadAsciiString(IO.In.ReadUInt16());
            this.TimestampS = IO.In.ReadAsciiString(IO.In.ReadUInt16());
            this.Race = IO.In.ReadAsciiString(IO.In.ReadUInt16());

            this.Unknown = IO.In.ReadInt16();
            this.UnknownF1 = IO.In.ReadSingle();
            this.UnknownF2 = IO.In.ReadSingle();

            this.Timestamp = DateTime.FromFileTime(IO.In.ReadInt64());

            int thumbnailWidth = IO.In.ReadInt32();
            int thumbnailHeight = IO.In.ReadInt32();

            this.ThumbnailSize = new Size(thumbnailWidth, thumbnailHeight);
        }

        public byte[] ToArray()
        {
            var IO = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);
            IO.Out.Write(this.Version);
            IO.Out.Write(this.SaveNumber);
            IO.Out.WriteInt16String(this.Name);
            IO.Out.Write(this.Level);
            IO.Out.WriteInt16String(this.Location);
            IO.Out.WriteInt16String(this.TimestampS);
            IO.Out.WriteInt16String(this.Race);

            IO.Out.Write(this.Unknown);
            IO.Out.Write(this.UnknownF1);
            IO.Out.Write(this.UnknownF2);

            IO.Out.Write(this.Timestamp.ToFileTime());

            IO.Out.Write(this.ThumbnailSize.Width);
            IO.Out.Write(this.ThumbnailSize.Height);

            IO.Close();

            return IO.ToArray();
        }
    }
    public struct SkyFormID
    {
        public RefID Id;
        public byte PluginIndex;

        public void Save(EndianWriter output)
        {
            output.WriteInt24(Id.ToInt24());
            output.Write(PluginIndex);
        }
    }
    public class SkyFormIDList : List<SkyFormID>
    {
        public SkyFormIDList(EndianIO IO)
        {
            int numIds = IO.In.ReadInt32();
            this.Capacity = numIds;

            for (int x = 0; x < numIds; x++)
                this.Add(new SkyFormID { Id = new RefID(IO.In.ReadInt24()), PluginIndex = IO.In.ReadByte()});
        }

        public void Write(EndianIO IO)
        {
            IO.Out.Write(this.Count);
            for (int x = 0; x < this.Count; x++)
                this[x].Save(IO.Out);
        }

        public int Add(int formId, int index)
        {
            this.Add(new SkyFormID() { Id = new RefID(formId & 0xFFFFFF), PluginIndex = (byte)index});
            return this.Count; // returns index
        }
        public bool Exists(int formId, int index)
        {
            return this.Exists(skyFormId => skyFormId.Id.FormID == (formId & 0x3FFFFF) && skyFormId.PluginIndex == index);
        }
        public int FindIndex(int formId, int index)
        {
            // not zero-based so we add one
            return this.FindIndex(skyFormId => skyFormId.Id.FormID == (formId & 0x3FFFFF) && skyFormId.PluginIndex == index) + 1;
        }
        public int FindFormId(int formId, int index)
        {
            return this.Find(skyFormId => skyFormId.Id.FormID == (formId & 0x3FFFFF) && skyFormId.PluginIndex == index).Id.FormID;
        }
    }

    public class SkyPluginList : List<string>
    {
        public SkyPluginList(EndianIO IO)
        {
            int pluginCount = IO.In.ReadByte();
            this.Capacity = pluginCount;

            for (int x = 0; x < pluginCount; x++)
                this.Add(IO.In.ReadAsciiString(IO.In.ReadInt16()));
        }

        public new byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);
            IO.Out.Write((byte)this.Count);

            foreach (string plugin in this)
                IO.Out.WriteInt16String(plugin);

            IO.Close();
            return IO.ToArray();
        }

        public int FindIndex(string pluginName)
        {
            return this.FindIndex(match => match == pluginName);
        }
    }

    public struct SkyLocationTable
    {
        public uint FormIDs;
        public uint FooterStringTable; // unknown
        public uint GlobalDataTable1; // to misc stats
        public uint GlobalDataTable2; // unknown
        public uint ChangeForms;
        public uint GlobalDataTable3; // vars

        public int GlobalDataTable1Count;
        public int GlobalDataTable2Count;
        public int GlobalDataTable3Count;
        public int ChangeFormsCount;

        private uint[] Reserved; // 15

        public const int Length = 100;

        public SkyLocationTable(EndianIO IO)
        {
            this.FormIDs = IO.In.ReadUInt32();
            this.FooterStringTable = IO.In.ReadUInt32();
            this.GlobalDataTable1 = IO.In.ReadUInt32();
            this.GlobalDataTable2 = IO.In.ReadUInt32();
            this.ChangeForms = IO.In.ReadUInt32();
            this.GlobalDataTable3 = IO.In.ReadUInt32();

            this.GlobalDataTable1Count = IO.In.ReadInt32();
            this.GlobalDataTable2Count = IO.In.ReadInt32();
            this.GlobalDataTable3Count = IO.In.ReadInt32();
            this.ChangeFormsCount = IO.In.ReadInt32();

            uint[] reserved = new uint[15];
            for (int x = 0; x < 15; x++)
                reserved[x] = IO.In.ReadUInt32();
            this.Reserved = reserved;
        }

        public void Write(EndianIO IO)
        {
            IO.Out.Write(this.FormIDs);
            IO.Out.Write(this.FooterStringTable);
            IO.Out.Write(this.GlobalDataTable1);
            IO.Out.Write(this.GlobalDataTable2);
            IO.Out.Write(this.ChangeForms);
            IO.Out.Write(this.GlobalDataTable3);

            IO.Out.Write(this.GlobalDataTable1Count);
            IO.Out.Write(this.GlobalDataTable2Count);
            IO.Out.Write(this.GlobalDataTable3Count);
            IO.Out.Write(this.ChangeFormsCount);

            for (int x = 0; x < 15; x++)
                IO.Out.Write(this.Reserved[x]);
        }
    }

    public enum GlobalDataType : uint
    {
        Misc_Stats = 0,
        Player_Location = 1,
        TES = 2,
        Global_Variables = 3,
        Created_Objects = 4,
        Effects = 5,
        Weather = 6,
        Audio = 7,
        Sky_Cells = 8,
        Process_Lists = 100,
        Combat = 101,
        Interface = 102,
        Actor_Causes = 103,
        Detection_Manager = 104,
        Location_Meta_Data = 105,
        Quest_Static_Data = 106,
        Story_Teller = 107,
        Magic_Favorites = 108,
        Player_Controls = 109,
        Story_Event_Manager = 110,
        Ingredient_Shared = 111,
        Menu_Controls = 112,
        Menu_Topic_Manager = 113,
        Unknown114 = 114,
        Temp_Effects = 1000,
        Papyrus = 1001,
        Anim_Objects = 1002,
        Timer = 1003,
        Synchronized_Animations = 1004,
        Main = 1005
    }

    public class GlobalDataList : List<GlobalData>
    {
        public GlobalDataList(EndianIO IO, int count)
        {
            this.Capacity = count;

            for (int x = 0; x < count; x++)
                this.Add(new GlobalData(IO));
        }

        public void Inject(EndianIO IO)
        {
            for (int x = 0; x < Count; x++)
            {
                this[x].Inject(IO.In.ReadBytes(this[x].Data.Length));
            }
        }

        public void Write(EndianIO IO)
        {
            for (int x = 0; x < this.Count; x++)
                this[x].Write(IO);
        }

        public void Save(EndianIO IO)
        {
            for (int x = 0; x < Count; x++)
            {
                this.Save(IO, x);
            }
        }

        public void Save(EndianIO IO, int Index)
        {
            this[Index].Save(IO);
            IO.Stream.Flush();
            //IO.Close();
        }
    }

    public struct GlobalData
    {
        public GlobalDataType Type;
        public byte[] Data;

        public GlobalData(EndianIO IO)
        {
            Type = (GlobalDataType)IO.In.ReadUInt32();
            Data = IO.In.ReadBytes(IO.In.ReadInt32());
        }

        public void Inject(byte[] data)
        {
            Data = data;
        }
        public void Write(EndianIO IO)
        {
            IO.Out.Write((uint)Type);
            IO.Out.Write(Data.Length);
            IO.Out.Write(Data);
        }
        public void Save(EndianIO IO)
        {
            IO.Out.Write(Data);
        }
    }

    public class ChangeFormList : List<ChangeForm>
    {
        public ChangeFormList(EndianIO IO, int count)
        {
            this.Capacity = count;
            //var writer = new StreamWriter(@"F:\Projects\Skyrim\Saves\Mine\3\Stamina_Up\tu10\changeRecords.txt", false);

            for (int x = 0; x < count; x++)
            {
                this.Add(new ChangeForm(IO));
                //writer.WriteLine("0x{0:X8}", this[x].Length);
            }

            //writer.Close();
        }

        public void Write(EndianIO IO)
        {
            for (int x = 0; x < this.Count; x++)
                this[x].Write(IO);
        }

        public ChangeForm FindForm(FormType formType, int formId)
        {
            return Find(form => form.Type == formType && form.Id.FormID == formId);
        }
        public int FindFormIndex(FormType formType, int formId)
        {
            return FindIndex(form => form.Type == formType && form.Id.FormID == formId);
        }
    }

    public enum FormType
    {
        REFR, ACHR, PMIS, PGRE, PBEA, PFLA, CELL, INFO,
        QUST, NPC_, ACTI, TACT, ARMO, BOOK, CONT, DOOR,
        INGR, LIGH, MISC, APPA, STAT, MSTT, FURN, WEAP,
        AMMO, KEYM, ALCH, IDLM, NOTE, ECZN, CLAS, FACT,
        PACK, NAVM, WOOP, MGEF, SMQN, SCEN, LCTN, RELA,
        PHZD, PBAR, PCON, FLST, LVLN, LVLI, LVSP, PARW,
        ENCH
    }

    public class ChangeForm
    {
        public RefID Id;
        public int Flags;
        public FormType Type;
        public byte Version;

        private byte[] _data;
        private DataType _dataLengthType;
        private int _uncompressedLength;
        public int Length;
        public void SetData(byte[] data, bool compress)
        {
            if (compress)
            {
                this._uncompressedLength = data.Length;
                data = ZlibStream.CompressBuffer(data);
            }
            else
                this._uncompressedLength = 0;

            this._data = data;
            // update data length type
            _dataLengthType = DataType.Int8;
            if (data.Length >= 0xFF || _uncompressedLength >= 0xFF)
            {
                _dataLengthType = DataType.Int16;
            }
            else if (data.Length >= 0xFFFF || _uncompressedLength >= 0xFFFF)
            {
                _dataLengthType = DataType.Int32;
            }
        }

        public byte[] GetData()
        {
            if (!this.Compressed)
                return this._data;

            return ZlibStream.UncompressBuffer(this._data);
        }

        public bool Compressed
        {
            get
            {
                return this._uncompressedLength != 0;
            }
        }

        public enum DataType
        {
            Int8 = 0,
            Int16 = 1,
            Int32 = 2
        }

        public ChangeForm(DataType lengthType)
        {
            _dataLengthType = lengthType;
        }

        public ChangeForm(EndianIO IO)
        {
            this.Id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
            this.Flags = IO.In.ReadInt32();

            byte typeFlag = IO.In.ReadByte();
            this._dataLengthType = (DataType)(typeFlag >> 6);
            this.Type = (FormType)(typeFlag & 0x3F);

            this.Version = IO.In.ReadByte();

            int dataLength;
            switch (this._dataLengthType)
            {
                case DataType.Int8:
                    dataLength = IO.In.ReadByte();
                    this._uncompressedLength = IO.In.ReadByte();
                    break;
                case DataType.Int16:
                    dataLength = IO.In.ReadUInt16();
                    this._uncompressedLength = IO.In.ReadUInt16();
                    break;
                case DataType.Int32:
                    dataLength = IO.In.ReadInt32();
                    this._uncompressedLength = IO.In.ReadInt32();
                    break;
                default:
                    throw new Exception(string.Format("SKYRIM: Unknown change form data length type [{0}]", (byte)this._dataLengthType));
            }

            this._data = IO.In.ReadBytes(( Length = dataLength));

            //Console.WriteLine("0x{0:X8}", dataLength);
        }

        public void Write(EndianIO IO)
        {
            IO.Out.WriteInt24(this.Id.ToInt24(), EndianType.BigEndian);
            IO.Out.Write(this.Flags);
            IO.Out.Write((byte)((byte)this.Type | ((byte)this._dataLengthType << 6)));
            IO.Out.Write(this.Version);

            switch (this._dataLengthType)
            {
                case DataType.Int8:
                    IO.Out.Write((byte)this._data.Length);
                    IO.Out.Write((byte)this._uncompressedLength);
                    break;
                case DataType.Int16:
                    IO.Out.Write((ushort)this._data.Length);
                    IO.Out.Write((ushort)this._uncompressedLength);
                    break;
                case DataType.Int32:
                    IO.Out.Write(this._data.Length);
                    IO.Out.Write(this._uncompressedLength);
                    break;
            }

            IO.Out.Write(this._data);
        }
    }
    public class NPCRecord
    {
        private EndianIO IO;
        private ChangeForm _record;

        public List<RefID> NPCDragonShouts = new List<RefID>();

        public int Level = 1;
        public string Name;

        private bool CreateLevel;

        private readonly long PropertiesStartLocation = -1, PlayerNameStartLocation = -1;
        private readonly long PropertiesEndLocation = -1, PlayerNameEndLocation = -1;

        public NPCRecord(ChangeForm record)
        {
            int t;
            RefID id;

            _record = record;

            IO = new EndianIO(record.GetData(), EndianType.LittleEndian, true);

            if ((_record.Flags & 1) != 0)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt16();
            }

            if (((_record.Flags >> 1) & 1) != 0)
            {
                var buf = IO.In.ReadBytes(0x18);
                Level = BitConverter.ToInt32(buf.ReadBytes(8, 4), 0);
            }

            if (((_record.Flags >> 6) & 1) != 0)
            {
                t = PlayerRecord.ReadVariableSize(IO.In);
                for (int i = 0; i < t; i++)
                {
                    PlayerRecord.ReadAndValidateRefId(IO.In, out id);
                    IO.In.ReadByte();
                }
            }
            if (((_record.Flags >> 4) & 1) != 0)
            {
                t = PlayerRecord.ReadVariableSize(IO.In);
                for (int i = 0; i < t; i++)
                {
                    PlayerRecord.ReadAndValidateRefId(IO.In, out id);
                }

                t = PlayerRecord.ReadVariableSize(IO.In);
                for (int i = 0; i < t; i++)
                {
                    PlayerRecord.ReadAndValidateRefId(IO.In, out id);
                }

                PropertiesStartLocation = IO.Position;
                t = PlayerRecord.ReadVariableSize(IO.In);
                for (int i = 0; i < t; i++)
                {
                    PlayerRecord.ReadAndValidateRefId(IO.In, out id);
                    NPCDragonShouts.Add(id);
                }
                PropertiesEndLocation = IO.Position;
            }

            if (((_record.Flags >> 3) & 1) != 0)
            {
                IO.In.ReadBytes(0x14);
            }

            if (((_record.Flags >> 5) & 1) != 0)
            {
                PlayerNameStartLocation = IO.Position;
                Name = IO.In.ReadAsciiString(IO.In.ReadInt16());
                PlayerNameEndLocation = IO.Position;
            }

            if (((_record.Flags >> 9) & 1) != 0)
            {
                IO.In.ReadBytes(0x34);
            }
            if (((_record.Flags >> 10) & 1) != 0)
            {
                PlayerRecord.ReadAndValidateRefId(IO.In, out id);
            }
        }

        public void SetLevel(int level)
        {
            Level = level;
            if ((((_record.Flags >> 1) & 1) == 0) && Level > 1)
                CreateLevel = true;
        }

        public void Save()
        {
            var output = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);

            IO.SeekTo(0);

            if (CreateLevel)
            {
                if ((_record.Flags & 1) != 0)
                {
                    output.Out.Write(IO.In.ReadBytes(6));
                }

                output.Out.Write(0x30);
                output.Out.Write((short)0x32);
                output.Out.Write((short)0x32);
                output.Out.Write(Level);
                output.Out.Write((short)0x64);
                output.Out.Write((short)0x64);
                output.Out.Write(0);
                output.Out.Write(0x32);

                IO.SeekTo(((_record.Flags & 1) != 0) ? 6 : 0);
                if (PropertiesStartLocation != -1)
                    output.Out.Write(IO.In.ReadBytes(PropertiesStartLocation - (((_record.Flags & 1) != 0) ? 6 : 0)));

                _record.Flags |= 2;
            }
            else if (((_record.Flags >> 1) & 1) != 0)
            {
                if (PropertiesStartLocation != -1) 
                    output.Out.Write(IO.In.ReadBytes(PropertiesStartLocation));

                output.SeekTo(0);
                if ((_record.Flags & 1) != 0)
                    output.Out.SeekTo(6);

                output.Position += 8;
                output.Out.Write(Level);
                IO.Position = output.Position += 0x0C;

                if (PropertiesStartLocation != -1)
                    output.Position = PropertiesStartLocation;
            }

            if (PropertiesStartLocation != -1)
            {
                PlayerRecord.WriteVariableSize(output.Out, NPCDragonShouts.Count);
                foreach (RefID npcProperty in NPCDragonShouts)
                {
                    output.Out.WriteInt24(npcProperty.ToInt24(), EndianType.BigEndian);
                }
                IO.SeekTo(PropertiesEndLocation);
            }

            output.Out.Write(IO.In.ReadBytes(PlayerNameStartLocation - IO.Position));

            output.Out.Write((short)Name.Length);
            output.Out.WriteAsciiString(Name, Name.Length);

            IO.Position = PlayerNameEndLocation;
            output.Out.Write(IO.In.ReadBytes(IO.Length - IO.Position));

            _record.SetData(output.ToArray(), true);

            output.Close();
        }

        public byte[] ToArray()
        {
            return IO.ToArray();
        }
    }

    public class PlayerRecord
    {
        private ChangeForm _record;

        public byte Version;
        private bool _hasSixBlock
        {
            get
            {
                return (this._record.Flags & 0x04) > 0;
            }
        }

        public RefID PlayerIRef;
        public float[] Location; //6
        public StatList Stats;
        private EndianIO IO;
        RefID id;
        int i, t, s, x, v;

        private readonly List<uint> FormIds = new List<uint>();
        public PlayerInventory Inventory;
        public PlayerSkills Skills;
        public PlayerPerks Perks;
        public PlayerSpells Spells;
        public PlayerEffects Effects;

        // Addresses
        public long InventoryLocation = -1, InventoryEndLocation = -1;
        public long SkillsLocation = -1;
        public long SpellsLocation = -1, SpellsEndLocation = -1;
        public long PerksStartLocation = -1, PerksEndLocation = -1;
        public long EffectsStartLocation = -1, EffectsEndLocation = -1;

        public PlayerRecord(ChangeForm record)
        {
            this._record = record;

            Version = _record.Version;

            byte[] playerData = record.GetData();

            IO = new EndianIO(playerData, EndianType.LittleEndian, true);

            this.PlayerIRef = new RefID(IO.In.ReadInt24(EndianType.BigEndian));

            this.Location = new float[6];
            for (x = 0; x < 6; x++)
                this.Location[x] = IO.In.ReadSingle();

            if(((_record.Flags >> 2 & 1) != 0))
            {
                var ct = ReadVariableSize(IO.In);
                IO.In.ReadBytes(ct);
            }
            
            // 82750050
            IO.In.ReadInt32();
            IO.In.ReadInt32();

            if ((this._record.Flags & 0x01) != 0x00)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt16();
            }

            if (((this._record.Flags >> 0x07) & 0x01) != 0x00)
            {
                id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
            }

            if (((this._record.Flags >> 0x04) & 0x01) != 0x00)
            {
                if (Version == 0x37)
                    ReadHalfFloatArray(IO.In, 0x04);
                else
                    IO.In.ReadInt32();
            }

            if ((this._record.Flags & 0xA6061840) != 0x00)
            {
                var ms = new MemoryStream();
                FormFunctionParser(IO, ms);
            }

            // Read Inventory Items
            this.LoadInventory();

            if (((this._record.Flags >> 28) & 0x1) != 0x00)
            {
                t = ReadVariableSize(IO.In);
                byte[] inp = IO.In.ReadBytes(t);
            }

            // first box jump
            IO.In.ReadInt32();
            IO.In.ReadInt32();

            t = IO.In.ReadByte();
            if (t != 0x00)
            {
                for (x = 0; x < 0x0C; x++)
                {
                    IO.In.ReadInt32();
                }
                for (x = 0; x < 0x02; x++)
                {
                    s = IO.In.ReadInt32();
                    for (i = 0; i < s; i++)
                    {
                        IO.In.ReadInt32();
                        IO.In.ReadInt32();
                        IO.In.ReadByte();
                    }
                }
            }

            ReadAndLoadRefForm(IO.In);

            IO.In.ReadByte();
            IO.In.ReadInt32();

            ReadAndValidateRefId(IO.In, out id);

            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt16();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            ReadAndValidateRefId(IO.In, out id);

            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; t++)
            {
                ReadAndValidateRefId(IO.In, out id);
            }

            // Equipped Items
            t = IO.In.ReadInt32();
            for (i = 0; i < t; i++)
            {
                ReadAndValidateRefId(IO.In, out id); // Item/Weapon/Magic
                ReadAndValidateRefId(IO.In, out id); // Type of Equipment
            }

            // 827BFD00
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            IO.In.ReadByte();
            ReadAndValidateRefId(IO.In, out id);

            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();

            for (i = 0; i < 0x03; i++)
            {
                IO.In.ReadInt32();
            }

            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();

            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt16();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();

            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);

            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);

            ReadAndValidateRefId(IO.In, out id);

            for (i = 0; i < 0x03; i++)
            {
                IO.In.ReadInt32();
            }
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();

            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; t++)
            {
                ReadAndValidateRefId(IO.In, out id);
            }

            ReadAndLoadRefForm(IO.In);

            // Load Active Effects And Perks Effects
            LoadEffects();

            ReadAndValidateRefId(IO.In, out id);
            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; )
            {
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadInt32();
                throw new Exception(ERROR_CODE_INCOMPLETE);
                // fill in
            }

            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            ReadAndValidateRefId(IO.In, out id);
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();

            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();

            if (_record.Version >= 0x08)
                IO.In.ReadByte();
            if (_record.Version >= 0x0D)
                IO.In.ReadByte();

            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt16();

            for (i = 0; i < 0x09; i++) // IND
                IO.In.ReadInt32();

            IO.In.ReadInt16();
            IO.In.ReadInt16();
            IO.In.ReadInt16();
            IO.In.ReadByte();

            BGSGameBuffer_ReadIntArray(IO.In, 0x0C);
            BGSGameBuffer_ReadIntArray(IO.In, 0x0C);

            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();

            BGSGameBuffer_ReadIntArray(IO.In, 0x04);
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();

            if (_record.Version >= 0x34)
                IO.In.ReadByte(); // 827C0CC4 

            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();

            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);

            // 827C0E40
            for (i = 0; i < 6; i++)
            {
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadByte();
            }

            t = IO.In.ReadByte();
            if (t != 0x00)
            {
                // 826F0170
                s = ReadVariableSize(IO.In);
                for (i = 0; i < s; i++)
                {
                    // 826F0690
                    BGSGameBuffer_ReadSzShortArray(IO.In);
                    BGSGameBuffer_ReadSzShortArray(IO.In);
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                    IO.In.ReadByte();
                    ReadAndValidateRefId(IO.In, out id);
                    ReadAndValidateRefId(IO.In, out id);
                    ReadAndValidateRefId(IO.In, out id);
                    IO.In.ReadByte();
                }
                IO.In.ReadInt16();
                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
            }
            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; i++)
            {
                IO.In.ReadInt32();
                ReadAndValidateRefId(IO.In, out id);
            }

            t = IO.In.ReadByte();
            if (t != 0x00)
            {
                IO.In.ReadInt32();
                IO.In.ReadBytes(0x0C);
                IO.In.ReadSingle();
                ReadAndValidateRefId(IO.In, out id);
            }
            t = ReadVariableSize(IO.In);
            if (t != 0x00)
            {
                IO.In.ReadBytes(t);
            }

            IO.In.ReadByte();
            IO.In.ReadByte();
            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; i++)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt32();
                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
            }
            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; i++)
            {
                ReadAndValidateRefId(IO.In, out id);

                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadInt32();
                IO.In.ReadSingle();
                s = IO.In.ReadByte();
                if (s != 0x00)
                {
                    IO.In.ReadInt32();
                    IO.In.ReadByte();
                    IO.In.ReadByte();
                    IO.In.ReadByte();
                    BGSGameBuffer_ReadIntArray(IO.In, 0x0c);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x0c);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x0c);
                    IO.In.ReadSingle();
                    IO.In.ReadSingle();
                    IO.In.ReadSingle();
                }
            }

            IO.In.ReadInt32();
            ReadAndValidateRefId(IO.In, out id);
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();

            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);

            // 827C13F8
            BGSGameBuffer_ReadSzShortArray(IO.In);

            t = IO.In.ReadByte();
            if (t != 0x00)
            {
                IO.In.ReadByte();
                IO.In.ReadByte();
                t = IO.In.ReadByte();
                if (t != 0x00)
                {
                    BGSGameBuffer_ReadSzShortArray(IO.In);
                    IO.In.ReadInt32();
                }
                if (_record.Version >= 0x32)
                {
                    t = IO.In.ReadByte();
                    if (t != 0x00)
                    {
                        BGSGameBuffer_ReadSzShortArray(IO.In);
                    }
                }


                for (i = 0; i < 2; i++)
                {
                    t = IO.In.ReadByte();
                    if (t != 0x00)
                    {
                        IO.In.ReadByte();
                        if (_record.Version >= 0x35)
                        {
                            IO.In.ReadByte();
                        }
                        t = IO.In.ReadInt32();
                        for (x = 0; x < t; x++)
                        {
                            IO.In.ReadInt32();
                            IO.In.ReadInt32();
                            IO.In.ReadInt32();
                        }
                    }
                }

                IO.In.ReadInt16();
                IO.In.ReadInt16();
                if (_record.Version >= 0x32)
                {
                    IO.In.ReadInt32();
                    IO.In.ReadByte();
                }
            }

            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            BGSGameBuffer_ReadIntArray(IO.In, 0x04);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();

            if (_record.Version >= 0x0D)
            {
                IO.In.ReadByte();
            }
            if (_record.Version >= 0x10)
            {
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadByte();
                IO.In.ReadByte();
            }
            if (_record.Version >= 0x17)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt32();
            }
            if (_record.Version >= 0x25)
            {
                IO.In.ReadByte();
            }
            if (_record.Version >= 0x27)
            {
                IO.In.ReadByte();
            }
            if (record.Version >= 0x3E)
            {
                IO.In.ReadByte();
            }

            // after the first box jump
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            for (x = 0; x < 0x0b; x++)
                IO.In.ReadInt32();

            ReadAndValidateRefId(IO.In, out id);
            // 8278C310
            IO.In.ReadInt16();
            for (x = 0; x < 0x10; x++)
                IO.In.ReadByte();

            ReadAndValidateRefId(IO.In, out id);
            var f = IO.In.ReadByte();
            for (i = 0; i < 0x04; i++)
            {
                if (((1 << i) & f) == 0) continue;
                
                // 826D9D78
                IO.In.ReadInt32();
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadInt32();
                IO.In.ReadInt32();
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadInt32();

                IO.In.ReadInt32();
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadInt32();
                BGSGameBuffer_ReadSzByteArray(IO.In);
            }
            for (x = 0; x < 0x04; x++)
            {
                ReadAndValidateRefId(IO.In, out id);
            }

            // 827506AC
            ReadAndValidateRefId(IO.In, out id);

            // 827506C8
            for (i = 0; i < 0x04; i++)
            { 
                t = IO.In.ReadInt32(); x = 0;
                do
                {
                    if (x < t)
                        IO.In.ReadInt32();
                } while (++x < 3);
            }

            // Read Skills and Attributes
            LoadSkills();

            // Read Spell Items
            LoadSpells();

            BGSGameBuffer_Subroutine1();

            // back to main function here
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();

            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadBytes(0x0C);
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            if (record.Version >= 0x0C)
            {
                IO.In.ReadByte();
            }
            else
            {
                IO.In.ReadByte();
            }
            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; i++)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt32();
            }
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadInt32();

            IO.In.ReadByte();
            IO.In.ReadByte();
            BGSGameBuffer_ReadIntArray(IO.In, 0x04);
            for (int j = 0; j < 0x7; j++)
                IO.In.ReadInt32();

            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            for (int j = 0; j < 0x7; j++)
                BGSGameBuffer_ReadIntArray(IO.In, 0x04);

            IO.In.ReadInt32();
            IO.In.ReadInt32();
            t = IO.In.ReadInt32();
            for (i = 0; i < t; i++)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt32();
                IO.In.ReadInt32();
                if (_record.Version >= 0x4A)
                {
                    IO.In.ReadInt32();
                }
            }
            if (t < 0x12)
            {
                for (i = 0; i < t; i++)
                {
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                }
            }

            BGSGameBuffer_ReadIntArray(IO.In, 0x0C);
            IO.In.ReadInt32();
            t = ReadVariableSize(IO.In);
            for (i = 0; i < t; i++)
            {
                BGSGameBuffer_ReadIntArray(IO.In, 0x0C);
                IO.In.ReadInt32();
            }

            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);

            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);

            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                if ((v = IO.In.ReadInt32()) > 0)
                {
                    if (v < 0x0F)
                    {
                        IO.In.ReadInt32();
                        ReadAndValidateRefId(IO.In, out id);
                    }
                }
            }

            // Read Quests
            LoadQuests();

            BGSGameBuffer_ReadIntArray(IO.In, 0x04);
            IO.In.ReadSingle();
            IO.In.ReadSingle();
            IO.In.ReadSingle();
            IO.In.ReadSingle();

            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadInt32();
            IO.In.ReadInt32();

            //0x68
            {
                IO.In.ReadInt32();
                if (record.Version >= 0x15)
                {
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                    IO.In.ReadByte();
                    IO.In.ReadByte();
                }
            }
            //0x8C
            {
                BGSGameBuffer_ReadUnknownS5(IO.In);
            }
            //0x90
            {
                BGSGameBuffer_ReadUnknownS5(IO.In);
                IO.In.ReadByte();
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadInt32();
            }
            //0x94
            {
                BGSGameBuffer_ReadUnknownS5(IO.In);
            }
            //0x88
            {
                BGSGameBuffer_ReadUnknownS5(IO.In);
            }
            // 0x9C
            {
                if (record.Version >= 0x41)
                {
                    IO.In.ReadBytes(0x0C);
                    IO.In.ReadInt32();
                    IO.In.ReadInt64();
                    IO.In.ReadByte();
                    IO.In.ReadInt32();
                    BGSGameBuffer_ReadSzShortArray(IO.In);
                    IO.In.ReadByte();
                    ReadAndValidateRefId(IO.In, out id);
                    IO.In.ReadInt32();
                }
            }
            IO.In.ReadInt32();
            t = IO.In.ReadInt32();
            for (i = 0; i < t; i++)
            {
                IO.In.ReadInt32();
            }

            if (record.Version >= 0x13)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt32();
                IO.In.ReadByte();
            }

            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    ReadAndValidateRefId(IO.In, out id);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x04);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x04);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x04);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x04);
                }
            }
            ReadAndValidateRefId(IO.In, out id);
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            ReadAndValidateRefId(IO.In, out id);
            IO.In.ReadInt32();
            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    ReadAndValidateRefId(IO.In, out id);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x04);
                    BGSGameBuffer_ReadIntArray(IO.In, 0x04);
                }
            }

            /* 
             * Skill/Perk Related - Next 3 Sections
             * Such as newly added perks
            */
            LoadAbilitiesAndPerks();

            IO.In.ReadByte();
            if ((v = IO.In.ReadByte()) != 0xFF)
            {
                IO.In.ReadInt16();
            }

            ReadAndValidateRefId(IO.In, out id);
            BGSGameBuffer_ReadIntArray(IO.In, 0x0C);
            IO.In.ReadInt32();
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            ReadAndValidateRefId(IO.In, out id);
            if (_record.Version >= 0x1C)
            {
                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
            }
            else
            {
                ReadAndValidateRefId(IO.In, out id);
                ReadAndValidateRefId(IO.In, out id);
            }
            if (_record.Version >= 0x5)
            {
                BGSGameBuffer_ReadIntArray(IO.In, 0x04);
            }
            if ((t = IO.In.ReadInt32()) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    if ((v = IO.In.ReadInt16()) >= 0x00)
                    {
                        IO.In.ReadInt32();
                        IO.In.ReadInt32();
                    }
                }
            }
            if ((t = IO.In.ReadInt32()) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    if ((v = IO.In.ReadInt16()) >= 0x00)
                    {
                        IO.In.ReadInt32();
                        IO.In.ReadInt32();
                    }
                }
            }
            ReadAndValidateRefId(IO.In, out id);
            IO.In.ReadInt32();
            ReadAndValidateRefId(IO.In, out id);
            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    ReadAndValidateRefId(IO.In, out id);
                    for (int j = 0; j < 0x04; j++)
                    {
                        IO.In.ReadInt16();
                    }
                }
            }
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();
            if (_record.Version >= 0x07)
            {
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
            }
            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    IO.In.ReadInt32();
                }
            }
            for (int j = 0; j < 0x02; j++)
            {
                ReadAndValidateRefId(IO.In, out id);
            }
            IO.In.ReadByte();
            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    ReadAndValidateRefId(IO.In, out id);
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                }
            }
            if (_record.Version >= 0xE)
            {
                IO.In.ReadInt32();
            }

            if (_record.Version >= 0x12)
            {
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
            }
            if (_record.Version >= 0x3B)
            {
                IO.In.ReadInt32();
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadByte();
            }
            if (_record.Version >= 0x3D)
            {
                if ((v = IO.In.ReadByte()) != 0x00)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        ReadAndValidateRefId(IO.In, out id);
                    }
                    ReadAndValidateRefId(IO.In, out id);
                    if(_record.Version >= 0x3F)
                    {
                        ReadAndValidateRefId(IO.In, out id);
                        ReadAndValidateRefId(IO.In, out id);
                    }
                    if(_record.Version >= 0x40)
                        ReadAndValidateRefId(IO.In, out id);
                }
            }
            if (_record.Version >= 0x48)
                IO.In.ReadInt32();
            else if (_record.Version >= 0x44)
                IO.In.ReadByte();

            if (_record.Version >= 0x46)
                IO.In.ReadByte();

            if (_record.Version >= 0x47)
            {
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadInt32();
            }
        }

        private void BGSGameBuffer_Subroutine1()
        {
            t = IO.In.ReadByte();
            if (t != 0x00)
            {
                s = IO.In.ReadInt32();
                for (x = 0; x < s; x++)
                {
                    t = IO.In.ReadInt32();
                    if (t != 0x00)
                    {
                        throw new Exception(ERROR_CODE_INCOMPLETE);
                    }// fill in
                }
                if (this._record.Version >= 0x39)
                {
                    s = IO.In.ReadInt32();
                    for (x = 0; x < s; )
                    {
                        throw new Exception(ERROR_CODE_INCOMPLETE);
                    }
                }

                IO.In.ReadInt32();
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
                int ct = IO.In.ReadInt32();
                s = 0x01; i = 0x00;
                read_struct t1;
                do
                {
                    Arbiters Arbiter_Index = Arbiters.NULL;
                    if (s != 0x01)
                        break;

                    if (this._record.Version >= 0x36)
                        IO.In.ReadByte();
                    /*
                     * Structure Index
                     * 0 - Path Manager Arbiter
                     * 1 - Planner Arbiter
                     * 2 - Handler Arbiter
                     * 3 - Tweener Arbiter
                     * 4 - Post Update Arbiter
                     */
                    Arbiter_Index = (Arbiters)i;
                    switch (Arbiter_Index)
                    {
                        case Arbiters.Handler:
                            break;
                        case Arbiters.PathManager:
                            {
                                IO.In.ReadInt32();
                                BGSGame_LoadPathDirector(IO.In);
                            }
                            break;
                        case Arbiters.Planner:
                            {
                                IO.In.ReadByte();
                            }
                            break;
                        case Arbiters.Tweener:
                            {
                                t1 = (reader) =>
                                {
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                };

                                v = IO.In.ReadInt32();
                                if (v != 0x00)
                                {
                                    for (int j = 0; j < v; j++)
                                    {
                                        t1(IO.In);
                                    }
                                }
                                else
                                {
                                    int p = 0x00;
                                    while (p++ < v)
                                    {
                                        t1(IO.In);
                                    }
                                }
                                IO.In.ReadInt32();
                                for (int j = 0; j < 0x0a; j++)
                                {
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                }
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                for (int j = 0; j < 0x03; j++)
                                {
                                    t1(IO.In);
                                }
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                            }
                            break;
                        case Arbiters.PostUpdate:
                            {
                                for (int j = 0; j < 0x0E; j++)
                                {
                                    IO.In.ReadInt32();
                                }
                            }
                            break;
                    }
                } while (++i < ct);

                // Read Game Triggers
                ct = IO.In.ReadInt32();
                s = 0x01; i = 0x00;
                do
                {
                     if (this._record.Version >= 0x36)
                        IO.In.ReadByte();

                    var trigger = (EventTriggers)i;
                    switch (trigger)
                    {
                        case EventTriggers.Sprint:
                            {
                                if (this._record.Version >= 0x36)
                                    IO.In.ReadInt32();

                                IO.In.ReadByte();
                                IO.In.ReadByte();
                            }
                            break;
                        case EventTriggers.TranslationsController:
                            {
                                IO.In.ReadByte();
                                IO.In.ReadInt32();
                            }
                            break;
                        case EventTriggers.PathFollower:
                            {
                                v = IO.In.ReadInt32();
                                BGSGame_LoadPathDirector(IO.In);
                                BGSGameBuffer_ReadUnknownS1(IO.In);
                                BGSGameBuffer_ReadUnknownS1(IO.In);

                                IO.In.ReadInt32();

                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();

                                IO.In.ReadInt32();
                                IO.In.ReadInt32();

                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                {
                                    t = IO.In.ReadByte();
                                    if (t != 0x00)
                                    {
                                        BGSGameBuffer_ReadSzShortArray(IO.In);
                                    }
                                    IO.In.ReadInt32();
                                }
                                {
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                }
                                {
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    IO.In.ReadInt32();
                                }
                                {
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                }
                            }
                            break;
                        case EventTriggers.ActorState:
                            {
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                BGSGameBuffer_ReadUnknownS1(IO.In);
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                if (this._record.Version >= 0x1B)
                                {
                                    IO.In.ReadInt32();
                                }
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                            }
                            break;
                        case EventTriggers.AnimationDriven:
                            {
                                IO.In.ReadInt32();
                            }
                            break;
                        case EventTriggers.LoadedAreaMonitor:
                            {
                                BGSGameBuffer_ReadUnknownS2(IO.In);
                            }
                            break;
                        case EventTriggers.PlayerControls:
                            {
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadByte();
                            }
                            break;
                        case EventTriggers.NavMeshBounds:
                            {
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                t = IO.In.ReadInt32();
                                for (int j = 0; j < t; j++)
                                {
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    v = IO.In.ReadInt32();
                                    for (int h = 0; h < v; h++)
                                    {
                                        BGSGameBuffer_ReadUnknownS1(IO.In);
                                    }
                                    IO.In.ReadInt32();
                                }
                                BGSGameBuffer_ReadUnknownS2(IO.In);
                            }
                            break;
                        case EventTriggers.StaticAvoider:
                            {
                                if (this._record.Version >= 0x1A)
                                    IO.In.ReadSingle();

                                t = IO.In.ReadInt32();
                                for (int j = 0; j < t; j++)
                                {
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    IO.In.ReadSingle();
                                }
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();

                                t = IO.In.ReadInt32();
                                for (int j = 0; j < t; j++)
                                {
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    IO.In.ReadSingle();
                                    IO.In.ReadSingle();
                                    IO.In.ReadInt32();
                                }
                                BGSGameBuffer_ReadUnknownS3(IO.In);
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                if (this._record.Version >= 0x11)
                                    IO.In.ReadInt32();
                            }
                            break;
                        case EventTriggers.ActorAvoider:
                            {
                                BGSGameBuffer_ReadUnknownS3(IO.In);
                                t = IO.In.ReadInt32();
                                for (int j = 0; j < t; j++)
                                {
                                    IO.In.ReadInt24();
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    IO.In.ReadSingle();
                                    IO.In.ReadSingle();
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                    IO.In.ReadSingle();
                                    IO.In.ReadInt32();
                                    IO.In.ReadInt32();
                                    IO.In.ReadByte();
                                }
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                            }
                            break;
                        case EventTriggers.AvoidBox:
                            {
                                IO.In.ReadInt32();
                                IO.In.ReadSingle();
                                BGSGameBuffer_ReadUnknownS4(IO.In);
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                IO.In.ReadByte();
                                BGSGameBuffer_LoadIntegerRef2(IO.In);
                                BGSGameBuffer_LoadIntegerRef1(IO.In);
                            }
                            break;
                        case EventTriggers.FixedDelta:
                            {
                                for (int j = 0; j < 0x06; j++)
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                                for (int j = 0; j < 0x03; j++)
                                    BGSGameBuffer_ReadUnknownS1(IO.In);
                            }
                            break;
                        case EventTriggers.LargeDeltaIdle:
                            {
                                IO.In.ReadInt32();
                            }
                            break;
                        case EventTriggers.PlannerKeepOffset:
                            {
                                ReadAndRetrieveRefId(IO.In);
                                BGSGameBuffer_ReadUnknownS1(IO.In);
                                BGSGameBuffer_ReadUnknownS1(IO.In);
                                IO.In.ReadSingle();
                                IO.In.ReadSingle();
                            }
                            break;
                        case EventTriggers.FailSafeWarp:
                            {
                                BGSGameBuffer_ReadUnknownS4(IO.In);
                                BGSGameBuffer_ReadUnknownS4(IO.In);
                                IO.In.ReadSingle();
                                ReadAndRetrieveRefId(IO.In);
                                IO.In.ReadInt32();
                                IO.In.ReadInt32();
                            }
                            break;

                    }

                } while (++i < ct);
                if ((t = IO.In.ReadByte()) != 0x00)
                {
                    if ((t = IO.In.ReadByte()) != 0x00)
                    {
                        IO.In.ReadByte();
                    }
                    if ((t = IO.In.ReadByte()) != 0x00)
                    {
                        IO.In.ReadInt32();
                    }
                }

                v = 1; // figure out how its generated - This is for the ActorRecord

                if ((v - 2) > 0x00)
                {
                    switch (v)
                    {
                        case 0x00:
                        case 0x06:
                        case 0x0A:
                        case 0x0B:
                            {
                                IO.In.ReadSingle();
                            }
                            break;
                    }
                }
            }
            IO.In.ReadInt32();
            t = IO.In.ReadByte();
            if (t != 0x00)
            {
                throw new Exception(ERROR_CODE_INCOMPLETE);
            }

            t = IO.In.ReadInt32();
            for (i = 0; i < t; i++)
            {
                ReadAndValidateRefId(IO.In, out id);
                IO.In.ReadSingle();
            }

            IO.In.ReadInt32();
            if (_record.Version >= 0x28)
                IO.In.ReadInt32();
            if (_record.Version >= 0x2B)
                ReadAndValidateRefId(IO.In, out id);
            if (_record.Version >= 0x2D)
                IO.In.ReadByte();
        }

        private void LoadInventory()
        {
            InventoryLocation = IO.Position;
            if (((_record.Flags & 0xFFFFFE0) & 0xF800003F) != 0x00)
            {
                var flags = _record.Flags;
                _record.Flags = 0x400;
                Inventory = new PlayerInventory(IO, this);
                _record.Flags = flags;
            }
            InventoryEndLocation = IO.Position;
        }

        private void LoadSkills()
        {
            SkillsLocation = IO.Position;
            Skills = new PlayerSkills(IO.In);
        }

        private void LoadAbilitiesAndPerks()
        {
            PerksStartLocation = IO.Position;
            Perks = new PlayerPerks(IO.In);
            PerksEndLocation = IO.Position;
        }

        private void LoadQuests()
        {
            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    ReadAndValidateRefId(IO.In, out id);
                    IO.In.ReadInt16();
                    IO.In.ReadByte();
                }
            }
            if ((t = ReadVariableSize(IO.In)) != 0x00)
            {
                for (i = 0; i < t; i++)
                {
                    ReadAndValidateRefId(IO.In, out id);
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                }
            }
        }
        
        private void LoadSpells()
        {
            // magic and spells
            SpellsLocation = IO.Position;

            Spells = new PlayerSpells(IO.In);

            SpellsEndLocation = IO.Position;
        }

        private void LoadEffects()
        {
            EffectsStartLocation = IO.Position;
            Effects = new PlayerEffects(IO.In);
            EffectsEndLocation = IO.Position;
        }

        delegate void read_struct(EndianReader reader);

        public enum Arbiters
        {
            NULL = -1,
            PathManager = 0x00,
            Planner,
            Handler,
            Tweener,
            PostUpdate
        }

        public enum EventTriggers
        {
            NULL = -1,
            Sprint = 0x00,
            PlayerControlsActionTrigger,
            TranslationsController,
            AngleController,
            DirectionPassThrough,
            AnglePassThrough,
            SpeedPassThrough,
            StrafingController,
            PathFollower,
            ActorState,
            AnimationDriven,
            LoadedAreaMonitor,
            PlayerControls,
            NavMeshBounds,
            StaticAvoider,
            ActorAvoider,
            AvoidBox,
            FixedDelta,
            NodeFollower, //empty
            DirectControl, //empty
            PlannerDirectControl, //empty
            LargeDeltaIdle,
            UntweenedMovementState, //empty
            PathFollowingMovementState, //empty
            PlannerKeepOffset,
            PlannerHorseControls, //empty
            StairsHelper,  //empty
            FailSafeWarp
        }

        delegate void TestDelegate(EndianReader reader);

        private void ReadHalfFloatArray(EndianReader reader, int cbFloatNum)
        {
            for (var x = 0; x < (cbFloatNum >> 2); x++)
            {
                var fsV = (float)reader.ReadInt16();
            }
        }

        private static void BGSGameBuffer_ReadIntArray(EndianReader reader, int cbIntNum)
        {
            for (int x = 0; x < (cbIntNum >> 2); x++)
            {
                var i = reader.ReadInt32();
            }
        }

        public static void BGSGameBuffer_ReadSzShortArray(EndianReader reader)
        {
            int len = reader.ReadUInt16();
            if (len != 0x00)
            {
                var bgsBuf = reader.ReadBytes(len);
            }
        }

        private void BGSGameBuffer_ReadSzByteArray(EndianReader reader)
        {
            int len = ReadVariableSize(reader);
            if (len != 0x00)
            {
                var bgsBuf = reader.ReadBytes(len);
            }
        }

        private void BGSGameBuffer_ReadUnknownS1(EndianReader reader)
        {
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
        }

        private void BGSGameBuffer_ReadUnknownS2(EndianReader reader) // at 0x30
        {
            uint t, memId = reader.ReadUInt32();
            if (memId != 0x00)
            {
                if (!FormIds.Contains(memId))
                {
                    if ((t = reader.ReadUInt32()) != 0x00)
                    {
                        if ((t = reader.ReadUInt32()) != 0x00)
                        {
                            for (uint j = 0; j < t; j++)
                            {
                                BGSGameBuffer_ReadUnknownS4(reader);
                                if (reader.ReadUInt32() != 0x00)
                                {
                                    reader.ReadInt32();
                                }
                            }
                        }
                        reader.ReadInt32();
                        reader.ReadInt32();
                        if ((t = reader.ReadUInt32()) != 0x00)
                        {
                            for (uint j = 0; j < t; j++)
                            {
                                BGSGameBuffer_ReadUnknownS4(reader);
                                BGSGameBuffer_ReadUnknownS1(reader);
                                if (reader.ReadUInt32() != 0x00)
                                {
                                    reader.ReadInt32();
                                }
                            }
                        }
                        reader.ReadByte();
                        reader.ReadByte();
                        if ((t = reader.ReadUInt32()) != 0x00)
                        {
                            for (uint j = 0; j < t; j++)
                            {
                                BGSGameBuffer_ReadUnknownS4(reader);
                                BGSGameBuffer_ReadUnknownS1(reader);
                                if ((t = reader.ReadUInt32()) != 0x00)
                                {
                                    reader.ReadInt32();
                                }
                            }
                        }
                        FormIds.Add(memId);
                    }
                }
            }

        }

        private void BGSGameBuffer_ReadUnknownS3(EndianReader reader) // at 0x2C
        {
            uint t, memId = reader.ReadUInt32();
            if (memId != 0x00)
            {
                if (!FormIds.Contains(memId))
                {
                    if ((t = reader.ReadUInt32()) != 0x00)
                    {
                        BGSGameBuffer_ReadUnknownS4(reader);
                        reader.ReadSingle();
                        BGSGameBuffer_ReadUnknownS4(reader);
                        reader.ReadSingle();
                        reader.ReadSingle();
                        BGSGameBuffer_ReadUnknownS1(reader);
                        reader.ReadSingle();
                        reader.ReadSingle();
                        if ((t = reader.ReadUInt32()) != 0x00)
                        {
                            for (int j = 0; j < t; j++)
                            {
                                BGSGameBuffer_ReadUnknownS1(reader);
                                BGSGameBuffer_ReadUnknownS1(reader);
                                reader.ReadSingle();
                                reader.ReadSingle();
                                reader.ReadInt32();
                            }
                        }
                        BGSGameBuffer_ReadUnknownS1(reader);
                        reader.ReadSingle();

                        reader.ReadSingle();
                        reader.ReadSingle();
                        reader.ReadInt16();
                        reader.ReadInt16();

                        reader.ReadSingle();
                        reader.ReadSingle();
                        reader.ReadInt32();
                        BGSGameBuffer_LoadIntegerRef1(reader);
                        reader.ReadInt24();
                        BGSGameBuffer_LoadIntegerRef3(reader);
                        t = reader.ReadUInt32();
                        for (uint i = 0; i < t; i++)
                        {
                            ReadAndLoadRefForm(reader);
                        }
                        reader.ReadInt32();
                    }
                    FormIds.Add(memId);
                }
            }            
        }

        private void BGSGameBuffer_ReadUnknownS4(EndianReader reader)
        {
            BGSGameBuffer_ReadUnknownS1(reader);
            BGSGameBuffer_LoadIntegerRef4(reader);

            ReadAndRetrieveRefId(reader);
            reader.ReadInt16();
            reader.ReadByte();
            reader.ReadByte();
        }

        private void BGSGameBuffer_ReadUnknownS5(EndianReader reader)
        {
            reader.ReadBytes(0x0c);
            reader.ReadInt32();
            for (int i = 0; i < 0x08; i++)
            {
                reader.ReadByte();
            }
            reader.ReadByte();
            reader.ReadInt32();
            BGSGameBuffer_ReadSzShortArray(reader);
        }

        private void BGSGameBuffer_LoadIntegerRef1(EndianReader reader)
        {
           uint uFormId = reader.ReadUInt32(), t;
           if (uFormId != 0x00)
           {
               switch (uFormId)
               {
                   case 0x5826A5DD:
                       {
                           t = reader.ReadByte();
                           if (t != 0x00)
                           {
                               ReadAndRetrieveRefId(reader);
                               if (this._record.Version >= 0x16)
                                   reader.ReadByte();
                           }
                       }
                       break;
               }
           }
        }

        private void BGSGameBuffer_LoadIntegerRef2(EndianReader reader)
        {
            uint uFormId = reader.ReadUInt32();
            if (uFormId != 0x00)
            {
                switch (uFormId)
                {
                    default:
                        throw new Exception(string.Format("Invalid IDENT : {0:X8} caught.", uFormId));
                }
            }
        }

        private void BGSGameBuffer_LoadIntegerRef3(EndianReader reader)
        {
            uint uFormId = reader.ReadUInt32();
            if (uFormId != 0x00)
            {
                for(int x = 0; x < 0x05; x++)
                    reader.ReadInt32();
            }
        }

        private void BGSGameBuffer_LoadIntegerRef4(EndianReader reader) // @0x0C
        {
            uint uFormId = reader.ReadUInt32();
            if (uFormId != 0x00)
            {
                switch (uFormId)
                {
                    case 0xA5E9A03C:
                        {
                            if (ReadAndRetrieveRefId(reader) != 0x00)
                            {
                                reader.ReadInt32();
                            }
                            else
                            {
                                ReadAndRetrieveRefId(reader);
                            }
                        } 
                        break;

                    default:
                        throw new Exception(string.Format("Invalid IDENT : {0:X8} caught.", uFormId));
                }
            }
        }

        private void BGSGame_LoadPathDirector(EndianReader reader) // Unk S3->S2 (0x2C->0x30)
        {
            BGSGameBuffer_ReadUnknownS3(reader);
            BGSGameBuffer_ReadUnknownS2(reader);
        }

        public static int ReadVariableSize(EndianReader reader)
        {
            int oVal = reader.PeekByte();
            switch (oVal & 0x03)
            {
                case 0x00:
                    oVal = reader.ReadByte() & 0xFF;
                    break;
                case 0x01:
                    oVal = reader.ReadInt16() & 0xFFFF;
                    break;
                case 0x02:
                    oVal = reader.ReadInt32();
                    break;
                case 0x03:
                    return 0x00;
            }
            return (oVal >> 2);
        }
        public static void WriteVariableSize(EndianWriter writer, int val)
        {
            if (val < 0x40)
            {
                writer.WriteByte((val << 2) & 0xFC);
            }
            else if (val < 0x4000)
            {
                val = ((val << 2) & 0xFFFC) | 0x01;
                //var a = (val >> 8) & 0xFF;
                //var b = (a & ~0xFF00) | (((a << 8) | (a >> 56)) & 0xFF00);
                writer.Write((short)val);
            }
            else if (val < 0x40000000)
            {
                var variable = ((long)val << 2) | 2;
                //var a = (val >> 24);
                writer.Write(variable);
            }
            else
            {
                throw new Exception(string.Format("Variable-sized value {0:X8} is too large to be stored.", val));
            }
        }

        private int FormFunctionParser(EndianIO IO, MemoryStream ms)
        {
            RefID id;
            int op, v, i, a, t, s;//, val;

            t = ReadVariableSize(IO.In);

            if (t != 0x00)
            {
                var writer = new EndianWriter(ms, EndianType.LittleEndian);
                for (var x = 0; x < t; x++)
                {
                    op = IO.In.ReadByte();
                    writer.WriteByte(op);
                    switch (op - 0x0C)
                    {
                        case 0x00:
                            {
                                writer.WriteInt24(IO.In.ReadInt24());
                                // 8257DBB8
                                {
                                    WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                    for (int j = 0; j < s; j++)
                                    {
                                        writer.Write(a = IO.In.ReadInt32());
                                        if (a == 0)
                                        {
                                            writer.Write(IO.In.ReadInt64());
                                        }
                                        else
                                        {
                                            writer.WriteInt24(IO.In.ReadInt24());
                                        }
                                    }
                                    writer.WriteByte(a = IO.In.ReadByte());
                                    if (a != 0)
                                    {
                                        writer.Write(IO.In.ReadBytes(0x8));
                                    }
                                    writer.WriteByte(IO.In.ReadByte());
                                }
                            }
                            break;
                        case 0x0C:
                            {
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.Write(IO.In.ReadBytes(0x0C));
                                writer.Write(IO.In.ReadInt32());
                            }
                            break;
                        case 0x0D:
                            {
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.Write(IO.In.ReadInt32());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteByte(IO.In.ReadByte());
                            }
                            break;
                        case 0x0E:
                            {
                                id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                                writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                                if (id.ToInt24() != 0x00)
                                {
                                    throw new Exception(ERROR_CODE_INCOMPLETE); // 8244BB34
                                }
                            }
                            break;
                        case 0x0F:
                            {
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (int j = 0; j < s; j++)
                                {
                                    writer.WriteInt24(IO.In.ReadInt24());
                                    writer.WriteByte(IO.In.ReadByte());
                                }
                            }
                            break;
                        case 0x11:
                            break;
                        case 0x13:
                            writer.WriteByte(IO.In.ReadByte());
                            break;
                        case 0x10:
                        case 0x15:
                        case 0x16:
                            id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            break;
                        case 0x18:
                            writer.Write(IO.In.ReadInt16());
                            break;
                        case 0x12:
                        case 0x17:
                        case 0x19:
                        case 0x1B:
                        case 0x1C:
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x1E:
                            writer.WriteByte(IO.In.ReadByte());
                            writer.WriteByte(IO.In.ReadByte());
                            writer.WriteInt24(IO.In.ReadInt24());
                            writer.Write(IO.In.ReadInt32());
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x1F:
                            // 8245FC88
                            {
                                writer.Write(IO.In.ReadBytes(0x0C));
                                writer.Write(IO.In.ReadBytes(0x0C));
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteInt24(IO.In.ReadInt24());
                            }
                            break;
                        case 0x20:
                            writer.WriteByte(IO.In.ReadByte());
                            break;
                        case 0x21:
                            writer.WriteInt24(IO.In.ReadInt24());
                            writer.WriteInt24(IO.In.ReadInt24());
                            writer.Write(IO.In.ReadInt32());
                            throw new Exception(ERROR_CODE_INCOMPLETE); // 8244C9E4
                        case 0x22:
                            writer.Write(IO.In.ReadInt32());
                            writer.WriteByte(IO.In.ReadByte());
                            break;
                        case 0x23:
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x25:
                            throw new Exception(ERROR_CODE_INCOMPLETE); // 8244BE28
                        case 0x26:
                            {
                                writer.WriteInt24(IO.In.ReadInt24());
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (int j = 0; j < s;)
                                {
                                    writer.WriteInt24(IO.In.ReadInt24());
                                    writer.WriteByte(IO.In.ReadByte());
                                    WriteVariableSize(writer, (a = ReadVariableSize(IO.In)));
                                    WriteVariableSize(writer, (v = ReadVariableSize(IO.In)));
                                    throw new Exception(ERROR_CODE_INCOMPLETE); // 826CADDC
                                }
                            }
                            break;
                        case 0x28:
                            WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                            for (int j = 0; j < s; j++)
                            {
                                writer.Write(IO.In.ReadInt32());
                                writer.Write(IO.In.ReadInt32());
                            }
                            break;
                        case 0x2C:
                            writer.WriteInt24(IO.In.ReadInt24());
                            break;
                        case 0x31:
                            break;
                        case 0x32:
                            {
                                writer.WriteInt24(IO.In.ReadInt24());
                                if (Version >= 3)
                                    writer.Write(IO.In.ReadBytes(4));
                            }
                            break;
                        case 0x38:
                            {
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (int j = 0; j < s; j++)
                                {
                                    writer.Write(IO.In.ReadInt32());
                                }
                            }
                            break;
                        case 0x39:
                        case 0x3C:
                            writer.WriteInt24(IO.In.ReadInt24());
                            break;
                        case 0x3D:
                            {
                                //s = 0x49;
                                writer.WriteByte(IO.In.ReadByte());
                            }
                            break;
                        case 0x40:
                            // 826F3A30
                            {
                                var f = IO.In.ReadInt16();
                                writer.Write(f);
                                writer.Write(IO.In.ReadBytes(f));
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.WriteInt24(IO.In.ReadInt24());
                            }
                            break;
                        case 0x41:
                            writer.WriteByte(IO.In.ReadByte());
                            break;
                        case 0x43:
                            writer.WriteByte(IO.In.ReadByte());
                            writer.WriteByte(IO.In.ReadByte());
                            break;
                        case 0x47:
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x48:
                            writer.WriteByte(IO.In.ReadByte());
                            break;
                        case 0x49:
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x4C:
                            writer.WriteInt24(IO.In.ReadInt24());
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x4D:
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x4F:
                            {
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (int j = 0; j < s; j++)
                                {
                                    writer.WriteInt24(IO.In.ReadInt24());
                                    writer.WriteByte(IO.In.ReadByte());
                                }
                                writer.WriteInt24(IO.In.ReadInt24());
                                if (Version >= 4)
                                    writer.WriteByte(IO.In.ReadByte());
                            }
                            break;
                        case 0x50:
                            {
                                writer.Write(IO.In.ReadInt16());
                                writer.Write(IO.In.ReadInt32());
                                writer.Write(IO.In.ReadInt32());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.WriteInt24(IO.In.ReadInt24());
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (int j = 0; j < s; j++)
                                {
                                    writer.WriteByte(IO.In.ReadByte());
                                    writer.WriteByte(IO.In.ReadByte());
                                    writer.WriteByte(IO.In.ReadByte());
                                    writer.WriteByte(IO.In.ReadByte());
                                    WriteVariableSize(writer, (a = ReadVariableSize(IO.In)));
                                    for (int k = 0; k < a; k++)
                                    {
                                        writer.WriteInt24(IO.In.ReadInt24());
                                    }
                                }
                            }
                            break;
                        case 0x51:
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x59:
                        case 0x5C:
                            writer.WriteInt24(IO.In.ReadInt24());
                            break;
                        case 0x5E:
                            {
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.Write(IO.In.ReadBytes(4));
                            }
                            break;
                        case 0x60:
                            {
                                writer.WriteByte(s = IO.In.ReadByte());
                                if (s != 0xFF)
                                {
                                    throw new Exception(ERROR_CODE_INCOMPLETE); // 8244B658
                                }
                            }
                            break;
                        case 0x63:
                            {
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (i = 0; i < s; i++)
                                {
                                    writer.Write(IO.In.ReadInt32());
                                    writer.Write(IO.In.ReadInt32());
                                }
                            }
                            break;
                        case 0x64:
                            id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            break;
                        case 0x65:
                            {
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.WriteByte(IO.In.ReadByte());
                                writer.Write(IO.In.ReadBytes(4));
                                writer.WriteInt24(IO.In.ReadInt24());
                                // 826F0170
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (i = 0; i < s; i++)
                                {
                                    // 826F0690
                                    for (int j = 0; j < 2; j++)
                                    {
                                        var f = IO.In.ReadInt16();
                                        writer.Write(f);
                                        writer.Write(IO.In.ReadBytes(f));
                                    }
                                    writer.Write(IO.In.ReadInt32());
                                    writer.Write(IO.In.ReadInt32());
                                    writer.Write(IO.In.ReadByte());
                                    for (int j = 0; j < 3; j++)
                                    {
                                        ReadAndValidateRefId(IO.In, out id);
                                        writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                                    }

                                    writer.Write(IO.In.ReadByte());
                                }
                                writer.Write(IO.In.ReadInt16());
                                for (int j = 0; j < 4; j++)
                                {
                                    ReadAndValidateRefId(IO.In, out id);
                                    writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                                }
                            }
                            break;
                        case 0x6C:
                            {
                                WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                                for (int j = 0; j < s; j++)
                                {
                                    writer.WriteInt24(IO.In.ReadInt24());
                                    writer.Write(IO.In.ReadInt32());
                                    writer.WriteByte(IO.In.ReadByte());
                                }
                            }
                            break;
                        case 0x79:
                            writer.WriteInt24(IO.In.ReadInt24());
                            break;
                        case 0x7B:
                            {
                                writer.Write(IO.In.ReadBytes(0x0C));
                                writer.WriteInt24(IO.In.ReadInt24());
                                writer.Write(IO.In.ReadBytes(4));
                                s = ReadVariableSize(IO.In);
                                WriteVariableSize(writer, s);
                                for (int j = 0; j < s; j++)
                                {
                                    writer.Write(IO.In.ReadBytes(0x0C));
                                    writer.WriteInt24(IO.In.ReadInt24());
                                    writer.Write(IO.In.ReadBytes(0x0C));
                                    writer.WriteInt24(IO.In.ReadInt24());
                                    writer.WriteByte(IO.In.ReadByte());
                                }

                            } break;
                        case 0x7C:
                            WriteVariableSize(writer, (s = ReadVariableSize(IO.In)));
                            for (i = 0; i < s; i++)
                            {
                                id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                                writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                                writer.Write(IO.In.ReadInt32());
                            }
                            break;
                        case 0x80:
                            s = ReadVariableSize(IO.In);
                            WriteVariableSize(writer, s);
                            for (var j = 0; j < s; j++)
                            {
                                writer.WriteInt24(IO.In.ReadInt24(EndianType.BigEndian), EndianType.BigEndian);
                            }
                            break;
                        case 0x82:
                        case 0x86:
                            id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            break;
                        case 0x89:
                            id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x8A:
                            writer.WriteByte(IO.In.ReadByte());
                            break;
                        case 0x8C:
                            WriteVariableSize(writer, s = ReadVariableSize(IO.In));
                            for (i = 0; i < s; i++)
                            {
                                // 8245A2C8
                                id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                                writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                                if (id.FormID != 0x00)
                                {
                                    writer.Write((short)(a = IO.In.ReadInt16()));
                                    if (a < 0xFFFF)
                                    {
                                        writer.Write(IO.In.ReadInt32());

                                        if (Version == 0x37)
                                        {
                                            writer.Write(IO.In.ReadBytes((0x0C >> 2) * 2));
                                            writer.Write(IO.In.ReadBytes((0x10 >> 2) * 2));
                                            writer.Write(IO.In.ReadBytes((0x4 >> 2) * 2));
                                        }
                                        else
                                        {
                                            writer.Write(IO.In.ReadBytes(0x0C));
                                            writer.Write(IO.In.ReadBytes(0x10));
                                            writer.Write(IO.In.ReadBytes(0x04));
                                        }
                                    }
                                }
                            }
                            writer.Write(IO.In.ReadInt16());
                            writer.Write(IO.In.ReadInt16());
                            break;
                        case 0x8D:
                            id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            var m_Id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(m_Id.ToInt24(), EndianType.BigEndian);
                            writer.Write(s = IO.In.ReadInt32());
                            if (id.ToInt24() == 0x00 && m_Id.ToInt24() == 0x00 && s == -2)
                            {
                                int m_bCount = IO.In.ReadInt16();
                                writer.Write((short)m_bCount);
                                byte[] m_rndData = IO.In.ReadBytes(m_bCount);
                                writer.Write(m_rndData);
                            }
                            break;
                        case 0x8F:
                            id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            writer.Write(IO.In.ReadInt16());
                            break;
                        case 0x90:
                            {
                                //s = 0x09C;

                                writer.WriteByte(IO.In.ReadByte());
                            }
                            break;
                        case 0x91:
                            {
                                id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                                writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            }
                            break;
                        case 0x93:
                            {
                                writer.Write(IO.In.ReadInt16());
                                writer.Write(IO.In.ReadInt32());
                            }
                            break;
                        case 0x94:
                            writer.Write(IO.In.ReadInt32());
                            break;
                        case 0x95:
                            {
                                for (var j = 0; j < 0x06; j++)
                                {
                                    writer.Write(Version == 0x37 ? IO.In.ReadBytes(6) : IO.In.ReadBytes(0xC));
                                }
                                for (var j = 0; j < 0x04; j++)
                                {
                                    writer.Write(IO.In.ReadInt32());
                                }
                            }
                            break;
                        case 0x98:
                            id = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                            writer.WriteInt24(id.ToInt24(), EndianType.BigEndian);
                            break;
                        case 0x9D:
                            {
                                writer.Write(IO.In.ReadInt32());
                                writer.WriteInt24(IO.In.ReadInt24(EndianType.BigEndian), EndianType.BigEndian);
                                writer.WriteInt24(IO.In.ReadInt24(EndianType.BigEndian), EndianType.BigEndian);
                                if (Version >= 9)
                                {
                                    writer.WriteByte(IO.In.ReadByte());
                                }
                                throw new Exception(ERROR_CODE_INCOMPLETE); // 8280E6F8
                            }
                            //break;
                        case 0xA2:
                            {
                                writer.Write(IO.In.ReadInt32());
                                writer.WriteInt24(IO.In.ReadInt24(EndianType.BigEndian), EndianType.BigEndian);
                                for (var j = 0; j < 2; j++)
                                {
                                    var sl = IO.In.ReadInt16();
                                    writer.Write(sl);
                                    writer.Write(IO.In.ReadBytes(sl));
                                }
                                for (var j = 0; j < 0x02; j++)
                                {
                                    writer.Write(Version == 0x37 ? IO.In.ReadBytes(6) : IO.In.ReadBytes(0xC));
                                }
                                writer.Write(IO.In.ReadInt32());
                                writer.Write(Version == 0x37 ? IO.In.ReadBytes(2) : IO.In.ReadBytes(0x4));
                            }
                            break;
                        case 0xA3:
                        case 0xA4:
                            {
                                writer.Write(s = IO.In.ReadInt32());
                                for (var j = 0; j < s; j++)
                                {
                                    writer.WriteInt24(IO.In.ReadInt24(EndianType.BigEndian), EndianType.BigEndian);
                                    writer.Write(IO.In.ReadInt32());
                                }
                            }
                            break;
                        case 0x01:
                        case 0x02:
                        case 0x03:
                        case 0x04:
                        case 0x05:
                        case 0x06:
                        case 0x07:
                        case 0x08:
                        case 0x09:
                        case 0x0A:
                        case 0x0B:
                        case 0x14:
                        case 0x1A:
                        case 0x1D:
                        case 0x24:
                        case 0x27:
                        case 0x29:
                        case 0x2A:
                        case 0x2B:
                        case 0x2D:
                        case 0x2E:
                        case 0x2F:
                        case 0x30:
                        case 0x46:
                        case 0x4A:
                        case 0x4B:
                        case 0x4E:
                        case 0x52:
                        case 0x53:
                        case 0x54:
                        case 0x55:
                        case 0x56:
                        case 0x57:
                        case 0x58:
                        case 0x5A:
                        case 0x5B:
                        case 0x5D:
                        case 0x5F:
                        case 0x61:
                        case 0x62:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 0x6A:
                        case 0x6B:
                        case 0x6D:
                        case 0x6E:
                        case 0x6F:
                        case 0x70:
                        case 0x71:
                        case 0x72:
                        case 0x73:
                        case 0x74:
                        case 0x75:
                        case 0x76:
                        case 0x77:
                        case 0x78:
                        case 0x7A:
                        case 0x7D:
                        case 0x7E:
                        case 0x7F:
                        case 0x81:
                        case 0x83:
                        case 0x84:
                        case 0x85:
                        case 0x87:
                        case 0x88:
                        case 0x8B:
                        case 0x8E:
                        case 0x92:
                        case 0x96:
                        case 0x97:
                        case 0x99:
                        case 0x9A:
                        case 0x9B:
                        case 0x9C:
                        case 0x9E:
                        case 0x9F:
                        case 0xA0:
                        case 0xA1:
                            break;
                        default:
                            throw new Exception(string.Format("Invalid op_code : {0:X8} caught.", op - 0x0C));
                    }
                }
                writer.Flush();
                writer.Close();
            }

            return t;
        }


        public static bool ReadAndValidateRefId(EndianReader reader, out RefID Id)
        {
            var id = reader.ReadInt24(EndianType.BigEndian);
            Id = new RefID(id);
            return (id != 0x00);
        }
        public static int ReadAndRetrieveRefId(EndianReader reader)
        {
            return new RefID(reader.ReadInt24(EndianType.BigEndian)).FormID;
        }
        public static void ReadAndLoadRefForm(EndianReader reader)
        {
            RefID id;
            int v;
            if (ReadAndValidateRefId(reader, out id))
            {
                v = reader.ReadByte();
                if (v != -1)
                {

                }

                v = reader.ReadByte();
                if (v != -1)
                {

                }

                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt16();
                reader.ReadByte();
                reader.ReadByte();
                id = new RefID(reader.ReadInt24(EndianType.BigEndian));
            }
        }

        public void Save()
        {
            var output = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);
            output.SeekTo(0);
            output.Out.WriteInt24(PlayerIRef.ToInt24(), EndianType.BigEndian);

            for (var x = 0; x < 6; x++)
                output.Out.Write(Location[x]);

            IO.Position = 0x1B;
            output.Out.Write(IO.In.ReadBytes(InventoryLocation - IO.Position));
            // inventory
            WriteInventory(output);

            IO.SeekTo(InventoryEndLocation);
            output.Out.Write(IO.In.ReadBytes(EffectsStartLocation - IO.Position));

            // write effects
            Effects.Save(output.Out);

            IO.SeekTo(EffectsEndLocation);
            output.Out.Write(IO.In.ReadBytes(SkillsLocation - IO.Position));

            // skills
            Skills.Save(output.Out);

            // spells
            Spells.Save(output.Out);

            IO.SeekTo(SpellsEndLocation);
            output.Out.Write(IO.In.ReadBytes(PerksStartLocation - IO.Position));

            // perks
            Perks.Save(output.Out);

            IO.SeekTo(PerksEndLocation);
            output.Out.Write(IO.In.ReadBytes(IO.Length - IO.Position));
            output.Stream.Flush();

            _record.SetData(output.ToArray(), true);
            output.Close();
        }

        private void WriteInventory(EndianIO IO)
        {
            if (((_record.Flags & 0xFFFFFE0) & 0xF800003F) != 0x00)
            {
                // seek to the inventory ( assuming we haven't modified anything else )
                //IO.Position = InventoryLocation;

                WriteVariableSize(IO.Out, Inventory.ItemCount);
                foreach (var invItem in Inventory.Items)
                {
                    IO.Out.WriteInt24(invItem.IRef.ToInt24(), EndianType.BigEndian);
                    IO.Out.Write(invItem.Value);

                    WriteVariableSize(IO.Out, invItem.Properties.Count);
                    foreach (var property in invItem.Properties)
                    {
                        WriteVariableSize(IO.Out, property.Count);
                        IO.Out.Write(property.Data);
                    }
                }
            }
        }

        public class InventoryItemProperty
        {
            public int Count;
            public byte[] Data;
        }

        public class InventoryItem
        {
            public RefID IRef;
            public int Value;
            public List<InventoryItemProperty> Properties = new List<InventoryItemProperty>();
        }

        public class PlayerInventory
        {
            private List<InventoryItem> _items = new List<InventoryItem>();
 
            public List<InventoryItem> Items
            {
                get { return _items; }
                set { _items = value; }
            }

            public PlayerInventory(EndianIO input, PlayerRecord playerRecord)
            {
                var itemCount = ReadVariableSize(input.In); // inventory item count
                for (var x = 0; x < itemCount; x++)
                {
                    var item = new InventoryItem();
                    var itemId = new RefID(input.In.ReadInt24(EndianType.BigEndian)); // Item ID
                    var itemValue = input.In.ReadInt32(); // Item Value

                    var extraDataCount = ReadVariableSize(input.In);
                    item.IRef = itemId;
                    item.Value = itemValue;
                    if (extraDataCount != 0x00) // Attached Extra Data
                    {
                        for (var i = 0; i < extraDataCount; i++)
                        {
                            var ms = new MemoryStream();
                            var propLen = playerRecord.FormFunctionParser(input, ms); // Parses Extra Data
                            item.Properties.Add(new InventoryItemProperty { Count = propLen, Data = ms.ToArray() });
                        }
                    }
                    _items.Add(item);
                }
            }

            public void Write(EndianWriter output)
            {
                
            }

            public InventoryItem this[FormId formId]
            {
                get { return Items.Find(item => item.IRef.FormID == (uint)formId); }
            }
            public InventoryItem this[int index]
            {
                get { return Items[index]; }
            }

            public int ItemCount
            {
                get { return _items.Count; }
            }

            public void AddItem(RefID iRefId, int value)
            {
                _items.Add(new InventoryItem { IRef = iRefId, Value = value});
            }
        }

        public class PlayerSkills
        {
            public class Skill
            {
                public PlayerAttribute Attribute;
                public double Value;
            }

            public class ExtraData
            {
                public int Unknown1;
                public float[] Unknown2 = new float[3];
            }

            public List<Skill> Skills;
            public List<ExtraData> UnknownData;

            public double this[PlayerAttribute attribute]
            {
                get { return Skills.Find(skill => skill.Attribute == attribute).Value; }
                set { Skills.Find(skill => skill.Attribute == attribute).Value = value; }
            }
            public PlayerSkills(EndianReader input)
            {
                Skills = new List<Skill>();
                UnknownData = new List<ExtraData>();

                var skillCount = ReadVariableSize(input);
                for (var x = 0; x < skillCount; x++)
                {
                    Skills.Add(new Skill { Attribute = (PlayerAttribute)input.ReadUInt32(), Value = input.ReadSingle()});
                }

                // Perhaps skill/attributes data
                var unknownDataCount = ReadVariableSize(input);
                for (var j = 0; j < unknownDataCount; j++)
                {
                    var extData = new ExtraData {Unknown1 = input.ReadInt32()};
                    for (var i = 0; i < 0x03; i++)
                    {
                        extData.Unknown2[i] = input.ReadSingle();
                    }
                    UnknownData.Add(extData);
                }
            }

            public void Save(EndianWriter output)
            {
                WriteVariableSize(output, Skills.Count);
                foreach (var skill in Skills)
                {
                    output.Write((uint) skill.Attribute);
                    output.Write((float)skill.Value);
                }

                WriteVariableSize(output, UnknownData.Count);
                foreach (var extraData in UnknownData)
                {
                    output.Write(extraData.Unknown1);
                    foreach (var unk2 in extraData.Unknown2)
                    {
                        output.Write(unk2);
                    }
                }
            }
        }

        public class PlayerPerks
        {
            public struct PerkTableEntry
            {
                public RefID Id;
                public byte Value;
            }
            public struct StoneAbility
            {
                public RefID Id;
                public int Value;
            }

            public List<PerkTableEntry> PerkTable = new List<PerkTableEntry>(); 

            public List<RefID> Perks = new List<RefID>();
            public List<StoneAbility> Abilities = new List<StoneAbility>();
 
            public PlayerPerks(EndianReader input)
            {
                int t;
                RefID id;
                // table of perks and stones ( abilities )
                if ((t = ReadVariableSize(input)) != 0x00)
                {
                    for (var i = 0; i < t; i++)
                    {
                        ReadAndValidateRefId(input, out id);
                        var f = input.ReadByte();

                        PerkTable.Add(new PerkTableEntry { Id = id, Value = f});
                    }
                }
                // perks
                if ((t = ReadVariableSize(input)) != 0x00)
                {
                    for (var i = 0; i < t; i++)
                    {
                        ReadAndValidateRefId(input, out id);

                        Perks.Add(id);
                    }
                }
                // stones (abilities)
                if ((t = ReadVariableSize(input)) != 0x00)
                {
                    for (var i = 0; i < t; i++)
                    {
                        ReadAndValidateRefId(input, out id);
                        
                        Abilities.Add(new StoneAbility { Id = id, Value = input.ReadInt32()});
                    }
                }
            }

            public void Save(EndianWriter output)
            {
                WriteVariableSize(output, PerkTable.Count);
                foreach (var tableEntry in PerkTable)
                {
                    output.WriteInt24(tableEntry.Id.ToInt24(), EndianType.BigEndian);
                    output.Write(tableEntry.Value);
                }
                WriteVariableSize(output, Perks.Count);
                foreach (var perk in Perks)
                {
                    output.WriteInt24(perk.ToInt24(), EndianType.BigEndian);
                }
                WriteVariableSize(output, Abilities.Count);
                foreach (var ability in Abilities)
                {
                    output.WriteInt24(ability.Id.ToInt24(), EndianType.BigEndian);
                    output.Write(ability.Value);
                }
            }

        }

        public class PlayerSpells
        {
            public List<RefID> SpellsList = new List<RefID>();
 
            public PlayerSpells(EndianReader input)
            {
                var spellCount = input.ReadInt32();
                for (var x = 0; x < spellCount; x++)
                {
                    RefID skillId;
                    ReadAndValidateRefId(input, out skillId);

                    SpellsList.Add(skillId);
                    //Console.WriteLine("{0:X8}", skillId.FormID);
                }
            }

            public void Save(EndianWriter output)
            {
                output.Write(SpellsList.Count);
                foreach (var spell in SpellsList)
                {
                    output.WriteInt24(spell.ToInt24(), EndianType.BigEndian);
                }
            }

            public void AddItem(RefID refId)
            {
                SpellsList.Add(refId);
            }
            public void RemoveItem(RefID refId)
            {
                SpellsList.Remove(refId);
            }
        }

        public class PlayerEffect
        {
            public RefID Id;
            public byte Unk1;
            public int VarSize1;
            public int VarSize2;
            public byte[] Data;

            public void Inject(byte[] data)
            {
                Data = data;
                VarSize2 = data.Length;
            }
        }

        public class PlayerEffects : List<PlayerEffect>
        {
            public PlayerEffects(EndianReader input)
            {
                int v, s, i;
                RefID id;
                var effectCount = ReadVariableSize(input);
                for (i = 0; i < effectCount; i++)
                {
                    var effect = new PlayerEffect();
                    var contProc = ReadAndValidateRefId(input, out id);

                    effect.Unk1 = input.ReadByte();
                    v = ReadVariableSize(input);
                    s = ReadVariableSize(input);

                    effect.Id = id;
                    effect.VarSize1 = v;
                    effect.VarSize2 = s;

                    var pos = input.BaseStream.Position;
                    var writer = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
                    if (contProc && s > 0)
                    {
                        writer.Out.Write(input.ReadBytes(s));
                    }
                    effect.Data = writer.ToArray();
                    this.Add(effect);
                }
            }

            public void Save(EndianWriter output)
            {
                WriteVariableSize(output, Count);
                foreach (var effect in this)
                {
                    output.WriteInt24(effect.Id.ToInt24(), EndianType.BigEndian);
                    output.Write(effect.Unk1);
                    WriteVariableSize(output, effect.VarSize1);
                    WriteVariableSize(output, effect.VarSize2);
                    output.Write(effect.Data);
                }
            }

            public void Add(RefID id)
            {
                var effect = new PlayerEffect();
                effect.Id = id;
                effect.Unk1 = 0x00;
                effect.VarSize1 = 0x22;

                var io = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);
                io.Out.Write(0);
                io.Out.Write(0);
                io.Out.Write(0);
                io.Out.Write(0x00014200);
                io.Out.WriteInt24(0x400014, EndianType.BigEndian);
                io.Out.WriteInt24(0x400014, EndianType.BigEndian);
                io.Out.WriteInt24(0, EndianType.BigEndian);
                io.Out.WriteInt24(0, EndianType.BigEndian);
                WriteVariableSize(io.Out, 0);
                io.Out.Write(-1);
                io.Out.Write((short)0);
                io.Out.Write(3, EndianType.BigEndian);
                io.Out.Write(new byte[3]);

                effect.Inject(io.ToArray());
                io.Close();

                this.Add(effect);
            }
            private void BGSGameBuffer_LoadEffect(EndianReader reader)
            {
                RefID id;
                int t, v, s;

                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();

                ReadAndValidateRefId(reader, out id);
                ReadAndValidateRefId(reader, out id);
                ReadAndValidateRefId(reader, out id);
                ReadAndValidateRefId(reader, out id);

                t = ReadVariableSize(reader);
                for (int x = 0; x < t; x++)
                {
                    v = reader.ReadByte();
                    switch (v)
                    {
                        case 0x09:
                            reader.ReadInt32();
                            reader.ReadInt32();
                            reader.ReadByte();
                            reader.ReadInt32();
                            s = reader.ReadByte();
                            if (s != 0x00)
                            {
                                ReadAndValidateRefId(reader, out id);
                                ReadAndValidateRefId(reader, out id);
                                ReadAndValidateRefId(reader, out id);
                                ReadAndValidateRefId(reader, out id);
                            }
                            ReadAndValidateRefId(reader, out id);
                            s = ReadVariableSize(reader);
                            if (s != 0x00)
                            {
                                reader.ReadBytes(s);
                            }
                            reader.ReadInt32();
                            break;

                        case 0x0A:
                            reader.ReadInt32();
                            reader.ReadInt32();
                            reader.ReadByte();
                            reader.ReadInt32();
                            s = reader.ReadByte();
                            if (s != 0x00)
                            {
                                ReadAndValidateRefId(reader, out id);
                                ReadAndValidateRefId(reader, out id);
                                ReadAndValidateRefId(reader, out id);
                                ReadAndValidateRefId(reader, out id);
                            }
                            reader.ReadInt32();
                            reader.ReadInt32();
                            reader.ReadInt32();
                            reader.ReadInt32();
                            reader.ReadInt32();
                            ReadAndValidateRefId(reader, out id);
                            ReadAndValidateRefId(reader, out id);
                            reader.ReadInt32();
                            break;
                    }
                }
                reader.ReadInt32();
                reader.ReadInt16();
                var remainder = reader.ReadInt32();
            }
        }
        public enum PlayerAttribute : uint // skills
        {
            OneHanded = 0x06,
            TwoHanded = 0x07,
            Archery,
            Block,
            Smithing,
            HeavyArmor,
            LightArmor = 0x0C,
            PickPocket = 0x0D,
            LockPicking,
            Sneak = 0x0F,
            Alchemy = 0x10,
            Speech,
            Alteration = 0x12,
            Conjuration,
            Destruction,
            Illusion,
            Restoration,
            Enchanting,

            Health = 0x18,
            Magicka = 0x19,
            Stamina = 0x1A,
            CurrentCarryWeight = 0x1F,
            BaseCarryWeight = 0x20,
            ArmorRating = 0x27,
            PerksToIncrease = 0x44
        }

        public struct StatEntry
        {
            public RefID StatID;
            public uint Value;
        }

        public class StatList : List<StatEntry>
        {
            private byte[] _unknownHeader; //10

            public StatList(EndianIO IO)
            {
                this._unknownHeader = IO.In.ReadBytes(0x0A);

                int numStats = IO.In.ReadByte() / 4;
                this.Capacity = numStats;

                for (int x = 0; x < numStats; x++)
                {
                    StatEntry stat = new StatEntry();
                    stat.StatID = new RefID(IO.In.ReadInt24(EndianType.BigEndian));
                    stat.Value = IO.In.ReadUInt32();
                    this.Add(stat);
                }
            }

            public void Write(EndianIO IO)
            {
                IO.Out.Write(this._unknownHeader);
                IO.Out.Write((byte)(this.Count * 4));

                for (int x = 0; x < this.Count; x++)
                {
                    IO.Out.WriteInt24(this[x].StatID.ToInt24(), EndianType.BigEndian);
                    IO.Out.Write(this[x].Value);
                }
            }
        }

        public enum FormId
        {
            Lockpick = 0x0000000A,
            Gold = 0x0000000F,
        }

        private static string ERROR_CODE_INCOMPLETE = "Function is not accesible";
    }
}