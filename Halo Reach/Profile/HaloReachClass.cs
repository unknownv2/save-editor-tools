using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using Halo;
using Horizon.Functions;

namespace HaloReachClass
{
    public class ReachSettings : HaloSettings
    {        
        public struct ReachProfile
        {
            public uint Credits;
            public string ServiceTag;
        }

        public ReachProfile ProfileSettings;
        public ReachCampaign CampaignProgress;
        public ReachUnlocks ProfileUnlocks;

        public ReachSettings(EndianIO io)
            : base(io, HaloGame.HaloReach)
        {
            ProfileSettings = new ReachProfile();

            ReadSettings();
        }

        private void ReadSettings()
        {
            //SettingsIO.SeekTo(0x2c);
            //CampaignProgress = new ReachCampaign(SettingsIO.In);
            ProfileUnlocks = new ReachUnlocks(SettingsIO);

            // read known settings
            SettingsIO.In.SeekTo(0x1a0);
            ProfileSettings.Credits = SettingsIO.In.ReadUInt32();
            SettingsIO.In.SeekTo(0x154);
            ProfileSettings.ServiceTag = SettingsIO.In.ReadUnicodeNullTermString();
        }
        private void SaveSettings()
        {
            //this.SettingsIO.Out.SeekTo(0x2c);
            //this.SettingsIO.Out.Write(this.CampaignProgress.Serialize());
            //this.SettingsIO.Out.SeekTo(0x51);
            //this.SettingsIO.Out.Write(this.CampaignProgress.Serialize());

            this.SettingsIO.Out.SeekTo(0x1a0);
            this.SettingsIO.Out.Write(this.ProfileSettings.Credits);
            uint syncId = this.SettingsIO.In.ReadUInt32() + 1;
            this.SettingsIO.Out.SeekTo(0x1a4);
            this.SettingsIO.Out.Write(syncId);
            this.SettingsIO.Out.SeekTo(0x154);
            this.SettingsIO.Out.WriteUnicodeNullTermString(this.ProfileSettings.ServiceTag);
            this.SettingsIO.Out.WriteUnicodeNullTermString(this.ProfileSettings.ServiceTag);
        }
        public void Save()
        {
            // write setting data
            SaveSettings();
            SaveToFile();
        }
    }

    public class ReachUnlocks
    {
        public enum Helmet
        {
            MarkV_B = 0x00,
            MarkV_B_UA = 0x01,
            MarkV_B_UA_HUL = 0x02,
            CQC = 0x03,
            CQC_CBRN = 0x04,
            CQC_UA_HUL = 0x05,
            ODST = 0x06,
            ODST_UA_CNM = 0x07,
            ODST_CBRN_HUL = 0x08,
            HAZOP = 0x09,
            HAZOP_CBRN_HUL = 0x0A,
            HAZOP_CNM_I = 0x0B,
            EOD,
            EOD_CNM,
            EOD_UA_HUL,
            OPERATOR,
            OPERATOR_UA_HUL,
            OPERATOR_CNM,
            GRENADIER,
            GRENADIER_UA,
            GRENADIER_UA_FC,
            AIR_ASSAULT,
            AIR_ASSAULT_UA_CNM,
            AIR_ASSAULT_FC_I,
            SCOUT,
            SCOUT_HU_RS,
            SCOUT_CBRN_CNM,
            EVA,
            EVA_CNM,
            EVA_UA_HUL_3,
            JFO,
            JFO_HUL_I,
            JFO_UA,
            COMMANDO,
            COMMANDO_CBRN_CNM,
            COMMANDO_UA_FC_I_2,
            EVA_C = 0x3D,
            EVA_C_CNM = 0x3E,
            EVA_C_UA_HUL_3 = 0x3F,
            MJOLNIR_MK_V = 0x24,
            MJOLNIR_MK_V_CNM = 0x25,
            MJOLNIR_MK_V_UA = 0x26,
            PILOT = 0x27,
            PILOT_HUL_3 = 0x28,
            PILOT_UA_HUL_3 = 0x29,
            PILOT_HAUNTED = 0x2A,
            RECON = 0x3A,
            RECON_HUL = 0x3B,
            RECON_UA_HUL_3 = 0x3C,
            MJOLNIR_MK_VI = 0x2E,
            MJOLNIR_MK_VI_FC_I_2 = 0x2F,
            MJOLNIR_MK_VI_UA_HUL_I = 0x30,
            GUNGNIR = 0x37,
            GUNGNIR_HU_RS = 0x38,
            GUNGNIR_CBRN = 0x39,
            SECURITY = 0x2B,
            SECURITY_UA_HUL = 0x2C,
            SECURITY_CBRN_CNM = 0x2D,
            MILITARY_POLICE = 0x31,
            MILITARY_POLICE_CBRN_HU_RS = 0x32,
            MILITARY_POLICE_HU_RS_CNM = 0x33,
            CQB,
            CQB_HU_RS_CNM,
            CQB_UA_HUL
        }
        public enum LeftShoulder
        {
            DEFAULT = 0x40,
            FJ_PARA,
            HAZOP,
            JFO,
            RECON,
            UA_MULTI_THREAT,
            JUMP_JET,
            EVA,
            GUNGNIR,
            ODST = 0x51,
            UA_BASE_SECURITY = 0x49,
            CQC,
            OPERATOR,
            COMMANDO,
            GRENADIER,
            SNIPER,
            MJOLNIR_Mk_V,
            SECURITY
        }
        public enum RightShoulder
        {
            DEFAULT = 0x52,
            FJ_PARA,
            HAZOP,
            JFO,
            RECON,
            UA_MULTI_THREAT,
            JUMP_JET,
            EVA,
            GUNGNIR,
            ODST = 0x63,
            UA_BASE_SECURITY = 0x5B,
            CQC,
            OPERATOR,
            COMMANDO,
            GRENADIER,
            SNIPER,
            MJOLNIR_Mk_V,
            SECURITY
        }
        public enum Chest
        {
            DEFAULT = 0x64,
            HP_HALO = 0x67,
            UA_COUNTERASSAULT,
            TACTICAL_LRP,
            UA_ODST = 0x77,
            TACTICAL_RECON = 0x6A,
            COLLAR_GRENADIER,
            TACTICAL_PATROL,
            COLLAR_BREACHER,
            ASSAULT_SAPPER,
            ASSAULT_COMMANDO,
            HP_PARAFOIL,
            COLLAR_GRENADIER_UA,
            UA_MULTI_THREAT_W,
            UA_BASE_SECURITY_W,
            COLLAR_BREACHER_R,
            HP_PARAFOIL_R,
            ASSAULT_SAPPER_R
        }
        public enum Wrist
        {
            DEFAULT = 0x78,
            UA_BUCKLER,
            UA_BRACER,
            TACTICAL_TACPAD,
            TACTICAL_UGPS,
            ASSAULT_BREACHER
        }
        public enum Utility
        {
            DEFAULT = 0x7E,
			UA_CHOBHAM = 0x80,
			TACTICAL_HARD_CASE = 0x83,
			UA_NxRA = 0x81,
			TACTICAL_TRAUMA_KIT = 0x82,
			TACTICAL_SOFT_CASE = 0x7F
        }
        public enum VisorColor
        {
            Default = 0x97,
            Silver,
            Blue,
            Black,
            Gold
        }
        public enum KneeGuards
        {
            DEFAULT = 0x84,
            FJ_PARA,
            GUNGNIR,
            GRENADIER
        }
        public enum ArmorEffect
        {
            Default = 0x88,
            Legendary = 0x89,
            Eternal = 0x8A,
            BirthdayParty = 0x8E,
            HeartAttack = 0x8B,
            Pestilence = 0x8D,
            InclementWeather = 0x8C
        }
        public enum FirefightVoice
        {
            NobleSix = 0x9C,
            Jorge_S_052 = 0xA7,
            Jun_S_266 = 0xA5,
            Emile_S_239 = 0xA6,
            Kat_S_320 = 0xA4,
            Auntie_Dot_AI = 0x9D,
            Carter_S_259 = 0xA3,
            GYSGT_Stacker = 0xA2,
            GYSGT_Buck = 0xA0,
            SGTMAJ_Johnson = 0xA1,
            Cortana_AI = 0x9E,
            John_S_117 = 0x9F
        }

        public enum EliteArmor
        {
            MINOR = 0x8F,
            SPEC_OPS,
            RANGER,
            ULTRA,
            ZEALOT,
            GENERAL,
            FIELD_MARSHALL,
            OFFICER
        }

        private readonly EndianIO _io;

        public ReachUnlocks(EndianIO io)
        {
            _io = io;
        }

        public bool IsUnlocked(object itemId)
        {
            return (_io.In.SeekNPeekByte(0x2A8 + Convert.ToInt32(itemId)) & 0x09) != 0x00;
        }

        public void SetLock(int itemId, bool unlocked)
        {
            var value = _io.In.SeekNPeekByte(0x2A8 + itemId);
            _io.Out.SeekNWrite(0x2A8 + itemId, (byte)(unlocked ? value | 0x08 : value & 0xF7));
        }
    }

    public class ReachCampaign
    {
        private static String[] CampaignLevels = new String[] 
            { 
                "Noble Actual", "Winter Contingency", "ONI : Sword Base", "Nightfall", "Tip of the Spear",
                "Long Night of Solace", "Exodus", "New Alexandria", "The Package", "The Pillar of Autumn", "Lone Wolf"
            };

        public struct LevelProgress
        {
            private static int Easy = 0, Normal = 1, Heroic = 2, Legendary = 3;
            public bool[] Solo;
            public bool[] CoOp;
            public bool[] RallyPoint;

            // completed difficulties
            public bool EasySolo
            {
                get
                {
                    return Solo[Easy];
                }
                set
                {
                    Solo[Easy] = value;
                }
            }
            public bool NormalSolo
            {
                get
                {
                    return Solo[Normal];
                }
                set
                {
                    Solo[Normal] = value;
                }
            }
            public bool HeroicSolo
            {
                get
                {
                    return Solo[Heroic];
                }
                set
                {
                    Solo[Heroic] = value;
                }
            }
            public bool LegendarySolo
            {
                get
                {
                    return Solo[Legendary];
                }
                set
                {
                    Solo[Legendary] = value;
                }
            }
            public bool EasyCoOp
            {
                get
                {
                    return CoOp[Easy];
                }
                set
                {
                    CoOp[Easy] = value;
                }
            }
            public bool NormalCoOp
            {
                get
                {
                    return CoOp[Normal];
                }
                set
                {
                    CoOp[Normal] = value;
                }
            }
            public bool HeroicCoOp
            {
                get
                {
                    return CoOp[Heroic];
                }
                set
                {
                    CoOp[Heroic] = value;
                }
            }
            public bool LegendaryCoOp
            {
                get
                {
                    return CoOp[Legendary];
                }
                set
                {
                    CoOp[Legendary] = value;
                }
            }

            public string LevelName;
        }

        public int CampaignProgress;
        public List<LevelProgress> Levels;
        public ReachCampaign(EndianReader reader)
        {
            Levels = new List<LevelProgress>();

            CampaignProgress = reader.ReadInt32();
            for (var x = 0; x < 11; x++)
            {
                LevelProgress prog = new LevelProgress
                                         {
                                             Solo = BitHelper.ProduceBitmask(reader.ReadByte()),
                                             LevelName = CampaignLevels[x]
                                         };

                Levels.Add(prog);
            }
        }
        public byte[] Serialize()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            for (var x = 0; x < Levels.Count; x++)
            {
                ew.Write(BitHelper.ConvertToWriteableByte(Levels[x].Solo));
            }
            return ms.ToArray();
        }
    }

    public class ReachSave
    {
        public EndianIO IO;
        public ReachSave(EndianIO IO)
        {
            if (IO != null)
            {
                this.IO = IO;
                if (!this.IO.Opened)
                    this.IO.Open();

                this.Read();
            }
        }
        private void Read()
        {
            this.IO.In.SeekTo(0x76844c);
            Pool ent = new Pool(this.IO.In);

            /*
            this.IO.In.SeekTo(0x7602F8);
            DataEntry Entry = new DataEntry(this.IO.In);
            for (int x = 0; x < Entry.MetaData.TotalAllocatedEntries; x++)
            {
                this.IO.In.BaseStream.Position += 0x10;
            }
            */
        }
        public void FixSaveHash()
        {
            this.IO.Out.SeekTo(0x1E708);
            this.IO.Out.Write(new byte[0x14]);
            SHA1Managed sha = new SHA1Managed();
            sha.TransformBlock(new byte[] {
                0xED, 0xD4, 0x30, 0x09,
                0x66, 0x6D, 0x5C, 0x4A,
                0x5C, 0x36, 0x57, 0xFA,
                0xB4, 0x0E, 0x02, 0x2F,
                0x53, 0x5A, 0xC6, 0xC9,
                0xEE, 0x47, 0x1F, 0x01,
                0xF1, 0xA4, 0x47, 0x56,
                0xB7, 0x71, 0x4F, 0x1C,
                0x36, 0xEC
            }, 0, 0x22, null, 0);

            this.IO.In.SeekTo(0);
            sha.TransformFinalBlock(this.IO.In.ReadBytes(0xA70000), 0, 0xA70000);
            this.IO.Out.SeekTo(0x1E708);
            this.IO.Out.Write(sha.Hash);
        }
    }
    public class DataEntry
    {
        public string Name;
        public uint EntrySize;
        public uint Type;
        public uint Flags;
        public byte Unknown2;
        public byte Unknown3;
        public ushort Unknown4;
        public uint FunctionPointer;
        public uint MemoryStartLocation;
        public uint MemoryEndLocation;
        public uint HeaderSize;
        public uint FullEntrySize;
        public uint DataSize;
        public DataEntryMetaData MetaData;

        public struct DataEntryMetaData
        {
            public uint EntriesInUse;
            public uint TotalAllocatedEntries;
            public uint FilledAllocatedEntries;
            public uint Id;

            //public DataEntryMetaData(byte[] Metadata) : this(new EndianReader(new MemoryStream(Metadata), EndianType.BigEndian)) { }
            public DataEntryMetaData(EndianReader reader)
            {
                this.EntriesInUse = reader.ReadUInt32();
                this.TotalAllocatedEntries = reader.ReadUInt32();
                this.FilledAllocatedEntries = reader.ReadUInt32();
                this.Id = reader.ReadUInt32();
            }
        }

        public DataEntry(EndianReader reader)
        {
            this.Name = reader.ReadAsciiString(0x20);
            this.EntrySize = reader.ReadUInt32();
            this.Type = reader.ReadUInt32();
            if (Type != 0x64407440)
                throw new Exception(string.Format("Halo Reach: Invalid data entry type detected. Found: 0x{X8}", Type));

            this.Flags = reader.ReadUInt32();
            this.Unknown2 = reader.ReadByte();
            this.Unknown3 = reader.ReadByte();
            this.Unknown4 = reader.ReadUInt16();
            this.FunctionPointer = reader.ReadUInt32();
            this.MetaData = new DataEntryMetaData(reader);
            this.MemoryStartLocation = reader.ReadUInt32();
            this.MemoryEndLocation = reader.ReadUInt32();
            this.HeaderSize = reader.ReadUInt32();
            this.FullEntrySize = reader.ReadUInt32();
            this.DataSize = this.FullEntrySize - HeaderSize;
        }
    }
    public class Pool
    {
        public uint Id;
        public uint Flags;
        public string Name;
        public uint FunctionPointer;
        public uint Unknown;
        public uint MaxSize;
        public uint Size;
        public uint AllocatedHeaderSize;
        public uint ReservedHeaderSize;
        public uint MaxEntryCount;
        public uint CurrentEntryCount;

        // sizeof(Pool) == 0x48
        public Pool(EndianReader reader)
        {
            this.Id = reader.ReadUInt32();
            if (this.Id != 0x706F6F6C)
                throw new Exception(string.Format("Halo Reach: Invalid pool ID found. Found: 0x{X8}.", this.Id));

            this.Flags = reader.ReadUInt32();
            this.Name = reader.ReadAsciiString(0x20);
            this.FunctionPointer = reader.ReadUInt32();
            this.Unknown = reader.ReadUInt32();
            this.MaxSize = reader.ReadUInt32();
            this.Size = reader.ReadUInt32();
            this.AllocatedHeaderSize = reader.ReadUInt32();
            this.ReservedHeaderSize = reader.ReadUInt32();
            this.MaxEntryCount = reader.ReadUInt32();
            this.CurrentEntryCount = reader.ReadUInt32();
        }
        public class ObjectMemoryTableEntry
        {
            public uint Identity;
            public uint Flags;
            public uint Unknown1;
            public uint MemoryLocation;

            public ObjectMemoryTableEntry(EndianReader reader)
            {
                this.Identity = reader.ReadUInt32();
                this.Flags = reader.ReadUInt32();
                this.Unknown1 = reader.ReadUInt32();
                this.MemoryLocation = reader.ReadUInt32();
            }
        }
    }
}
