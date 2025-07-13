using System;
using System.Collections.Generic;
using System.IO;
using Halo;
using Horizon.Functions;
using BKSystem.IO;

namespace Halo4
{
    public class Halo4Campaign
    {
        public readonly static object[] CampaignLevels = new object[] 
            { 
                "Prologue",
                "Dawn",
                "Requiem",
                "Forerunner",
                "Infinity",
                "Reclaimer",
                "Shutdown",
                "Composer",
                "Midnight"                
            };

        public List<CampaignLevelProgress> CampaignProgress;
        private EndianIO _io;
        public Halo4Campaign(EndianIO io)
        {
            _io = io;

            io.SeekTo(0x2C);
            CampaignProgress = new List<CampaignLevelProgress>();
            io.SeekTo(0x86);
            var campaignUnlocks = io.In.ReadInt16();
            int idx = 0x01;
            for (int i = 0; i < 0x04; i++)
            {
                io.SeekTo(0x33 - i);
                byte soloLevel = io.In.ReadByte();
                io.SeekTo(0x53 - i);
                byte coopLevel = io.In.ReadByte();
                for (int j = 0; j < 0x02; j++)
                {
                    CampaignProgress.Add( new CampaignLevelProgress
                       {
                           Solo = BitHelper.ProduceBitmask(soloLevel & 0x0F),
                           CoOp = BitHelper.ProduceBitmask(coopLevel & 0x0F),
                           LevelName = (string) CampaignLevels[CampaignProgress.Count],
                           Unlocked = (campaignUnlocks & idx) != 0x00
                       });

                    idx <<= 0x01;
                    soloLevel >>= 0x04;
                    coopLevel >>= 0x04;
                }
            }

            CampaignProgress.Add(new CampaignLevelProgress
                     {
                         Solo = BitHelper.ProduceBitmask(io.In.SeekNReadByte(0x37) & 0x0F),
                         CoOp = BitHelper.ProduceBitmask(io.In.SeekNReadByte(0x57) & 0x0F),
                         LevelName =  (string) CampaignLevels[0x08],
                         Unlocked = (campaignUnlocks & idx) != 0x00
                     });
        }

        public void Save()
        {
            // write the mission completion difficulties
            byte solo,coop;
            for (int i = 0; i < 0x04; i++)
            {
                byte soloLevel = 0x00;
                byte coopLevel = 0x00;
                for (int j = 0; j < 0x02; j++)
                {
                    solo = BitHelper.ConvertToWriteableByte(CampaignProgress[i*2 + j].Solo);
                    soloLevel |= j == 0x00 ? solo : (byte)(solo << 0x04);
                    coop = BitHelper.ConvertToWriteableByte(CampaignProgress[i*2 + j].CoOp);
                    coopLevel |= j == 0x00 ? coop : (byte)(coop << 0x04);
                }
                _io.SeekTo(0x33 - i);
                _io.Out.Write(soloLevel);
                _io.SeekTo(0x53 - i);
                _io.Out.Write(coopLevel);
            }
            coop = BitHelper.ConvertToWriteableByte(CampaignProgress[0x08].CoOp);
            solo = BitHelper.ConvertToWriteableByte(CampaignProgress[0x08].Solo);
            _io.Out.SeekNWrite(0x37, solo);
            _io.Out.SeekNWrite(0x57, coop);

            // write the mission availability
            var campaignUnlocks = new bool[0x9];
            for (int i = 0; i < 0x09; i++)
                campaignUnlocks[i] = CampaignProgress[i].Unlocked;
            
            _io.Out.SeekNWrite(0x84, BitHelper.ConvertToWriteableInteger(campaignUnlocks));
        }
    }

    public class ProfileUnlockables
    {
        // Armor
        public enum Helmet
        {
            Recruit = 0x61,
            Recruit_PRME = 0xC3,
            Warrior = 0x00,
            Warrior_MTRX = 0xE3,
            AirAssault = 0x2E,
            AirAssault_VERG = 0xAB,
            AirAssault_VISN = 0x193,
            Soldier = 0x0B,
            Soldier_ZNTH = 0xCB,
            Aviator = 0x3E,
            Aviator_BOND = 0xAF,
            Defender = 0x69,
            Defender_CTRL = 0x103,
            Defender_CLMN = 0x1B3,
            Recon = 0x0F,
            Recon_SURG = 0xD7,
            EVA = 0x1B,
            EVA_BRCH = 0x18F,
            WarMaster = 0x65,
            WarMaster_PRML = 0xC7,
            Scout = 0x07,
            Scout_APEX = 0xBB,
            Scout_TOXC = 0x1BC,
            Orbital = 0x27,
            Orbital_AEON = 0xCF,
            Orbital_SWFT = 0x1A4,
            Infiltrator = 0x23,
            Infltrator_TRAC = 0xFF,
            Infiltrator_PYTN = 0x19C,
            Hazop = 0x13,
            Hazop_FRST = 0xE7,
            EOD = 0x17,
            EOD_SHDW = 0xF3,
            Oceanic = 0x6D,
            Oceanic_CRCT = 0xDF,
            Oceanic_SLID = 0x197,
            Enforcer = 0x36,
            Enforcer_TRBL = 0xB7,
            Enforcer_TRCR = 0x1A0,
            Wetwork = 0x75,
            Wetwork_SHRD = 0x107,
            Operator = 0x79,
            Operator_SRFC = 0x10B,
            Pioneer = 0x1F,
            Pioneer_ADPT = 0x10F,
            Pathfinder = 0x42,
            Pathfinder_CORE = 0x113,
            Engineer = 0x8B,
            Engineer_EDGE = 0x117,
            Stalker = 0x8F,
            Stalker_CRSH = 0x11B,
            Rogue = 0x93,
            Rogue_FCUS = 0x11F,
            Tracker = 0x97,
            Tracker_ADRT = 0x123,
            Vanguard = 0x32,
            Vanguard_CNVG = 0xEB,
            CIO = 0x46,
            CIO_WEB = 0xD3,
            CIO_RUSH = 0x1AF,
            CIO_RUIN = 0x1B8,
            Venator = 0x5D,
            Venator_RPTR = 0xBF,
            Venator_BYNT = 0x1A9,
            Venator_RFCT = 0x1C2,
            Gungnir = 0x3A,
            Gungnir_PULS = 0xEF,
            Commando = 0x71,
            Commando_FRCT = 0xFB,
            Ranger = 0x7D,
            Ranger_STRK = 0xF7,
            Protector = 0x81,
            Protector_DRFT = 0xDB,
            Mark_VI = 0x85,
            Raider = 0x51,
            Raider_DSTT = 0xB3,
            Deadeye = 0x127,
            Locus = 0x128,
            Scanner = 0x129,
            Strider = 0x12A,
            FOTUS = 0x4A,
            MarkV = 0x1C8,
            ODST = 0x1CE,
            Prefect = 0x1D4,
            Ricochet = 0x1DA,
            Ricochet_HMTM = 0x1E0
        }

        public enum Torso
        {
            Recruit = 0x64,
            Recruit_PRME = 0xC4,
            Warrior = 0x03,
            Warrior_MTRX = 0xE4,
            AirAssault = 0x2F,
            AirAssault_VERG = 0xAC,
            AirAssault_VISN = 0x194,
            Soldier = 0x0E,
            Soldier_ZNTH = 0xCC,
            Aviator = 0x3F,
            Aviator_BOND = 0xB0,
            Defender = 0x6C,
            Defender_CTRL = 0x104,
            Defender_CLMN = 0x1B4,
            Recon = 0x10,
            Recon_SURG = 0xD8,
            EVA = 0x1C,
            EVA_BRCH = 0x190,
            WarMaster = 0x68,
            WarMaster_PRML = 0xC8,
            Scout = 0x0A,
            Scout_APEX = 0xBC,
            Scout_TOXC = 0x1BD,
            Orbital = 0x28,
            Orbital_AEON = 0xD0,
            Orbital_SWFT = 0x1A5,
            Infiltrator = 0x24,
            Infltrator_TRAC = 0x100,
            Infiltrator_PYTN = 0x19D,
            Hazop = 0x14,
            Hazop_FRST = 0xE8,
            EOD = 0x18,
            EOD_SHDW = 0xF4,
            Oceanic = 0x70,
            Oceanic_CRCT = 0xE0,
            Oceanic_SLID = 0x198,
            Enforcer = 0x37,
            Enforcer_TRBL = 0xB8,
            Enforcer_TRCR = 0x1A1,
            Wetwork = 0x76,
            Wetwork_SHRD = 0x108,
            Operator = 0x7A,
            Operator_SRFC = 0x10C,
            Pioneer = 0x20,
            Pioneer_ADPT = 0x110,
            Pathfinder = 0x43,
            Pathfinder_CORE = 0x114,
            Engineer = 0x8C,
            Engineer_EDGE = 0x118,
            Stalker = 0x90,
            Stalker_CRSH = 0x11C,
            Rogue = 0x94,
            Rogue_FCUS = 0x120,
            Tracker = 0x98,
            Tracker_ADRT = 0x124,
            Vanguard = 0x33,
            Vanguard_CNVG = 0xEC,
            CIO = 0x47,
            CIO_WEB = 0xD4,
            CIO_RUSH = 0x1B0,
            CIO_RUIN = 0x1B9,
            Venator = 0x60,
            Venator_RPTR = 0xC0,
            Venator_BYNT = 0x1AA,
            Venator_RFCT = 0x1C3,
            Gungnir = 0x3B,
            Gungnir_PULS = 0xF0,
            Commando = 0x74,
            Commando_FRCT = 0xFC,
            Ranger = 0x7E,
            Ranger_STRK = 0xF8,
            Protector = 0x82,
            Protector_DRFT = 0xDC,
            Mark_VI = 0x86,
            Raider = 0x54,
            Raider_DSTT = 0xB4,
            FOTUS = 0x4B,
            MarkV = 0x1C9,
            ODST = 0x1CF,
            Prefect = 0x1D5,
            Ricochet = 0x1DB
        }

        public enum LeftShoulder
        {
            Recruit = 0x62,
            Recruit_PRME = 0xC5,
            Warrior = 0x01,
            Warrior_MTRX = 0xE5,
            AirAssault = 0x30,
            AirAssault_VERG = 0xAD,
            AirAssault_VISN = 0x195,
            Soldier = 0x0C,
            Soldier_ZNTH = 0xCD,
            Aviator = 0x40,
            Aviator_BOND = 0xB1,
            Defender = 0x6A,
            Defender_CTRL = 0x105,
            Defender_CLMN = 0x1B5,
            Recon = 0x11,
            Recon_SURG = 0xD9,
            EVA = 0x1D,
            EVA_BRCH = 0x191,
            WarMaster = 0x66,
            WarMaster_PRML = 0xC9,
            Scout = 0x08,
            Scout_APEX = 0xBD,
            Scout_TOXC = 0x1BE,
            Orbital = 0x29,
            Orbital_AEON = 0xD1,
            Orbital_SWFT = 0x1A6,
            Infiltrator = 0x25,
            Infltrator_TRAC = 0x101,
            Infiltrator_PYTN = 0x19E,
            Hazop = 0x15,
            Hazop_FRST = 0xE9,
            EOD = 0x19,
            EOD_SHDW = 0xF5,
            Oceanic = 0x6E,
            Oceanic_CRCT = 0xE1,
            Oceanic_SLID = 0x199,
            Enforcer = 0x38,
            Enforcer_TRBL = 0xB9,
            Enforcer_TRCR = 0x1A2,
            Wetwork = 0x77,
            Wetwork_SHRD = 0x109,
            Operator = 0x7B,
            Operator_SRFC = 0x10D,
            Pioneer = 0x21,
            Pioneer_ADPT = 0x111,
            Pathfinder = 0x44,
            Pathfinder_CORE = 0x115,
            Engineer = 0x8D,
            Engineer_EDGE = 0x119,
            Stalker = 0x91,
            Stalker_CRSH = 0x11D,
            Rogue = 0x95,
            Rogue_FCUS = 0x121,
            Tracker = 0x99,
            Tracker_ADRT = 0x125,
            Vanguard = 0x34,
            Vanguard_CNVG = 0xED,
            CIO = 0x48,
            CIO_WEB = 0xD5,
            CIO_RUSH = 0x1B1,
            CIO_RUIN = 0x1BA,
            Venator = 0x5E,
            Venator_RPTR = 0xC1,
            Venator_BYNT = 0x1AB,
            Venator_RFCT = 0x1C4,
            Gungnir = 0x3C,
            Gungnir_PULS = 0xF1,
            Commando = 0x72,
            Commando_FRCT = 0xFD,
            Ranger = 0x7F,
            Ranger_STRK = 0xF9,
            Protector = 0x83,
            Protector_DRFT = 0xDD,
            Mark_VI = 0x87,
            Raider = 0x52,
            Raider_DSTT = 0xB5,
            FOTUS = 0x4C,
            MarkV = 0x1CA,
            ODST = 0x1D0,
            Prefect = 0x1D6,
            Ricochet = 0x1DC
        }

        public enum RightShoulder
        {
            Recruit = 0x63,
            Recruit_PRME = 0xC6,
            Warrior = 0x02,
            Warrior_MTRX = 0xE6,
            AirAssault = 0x31,
            AirAssault_VERG = 0xAE,
            AirAssault_VISN = 0x196,
            Soldier = 0x0D,
            Soldier_ZNTH = 0xCE,
            Aviator = 0x41,
            Aviator_BOND = 0xB2,
            Defender = 0x6B,
            Defender_CTRL = 0x106,
            Defender_CLMN = 0x1B6,
            Recon = 0x12,
            Recon_SURG = 0xDA,
            EVA = 0x1E,
            EVA_BRCH = 0x192,
            WarMaster = 0x67,
            WarMaster_PRML = 0xCA,
            Scout = 0x09,
            Scout_APEX = 0xBE,
            Scout_TOXC = 0x1BF,
            Orbital = 0x2A,
            Orbital_AEON = 0xD2,
            Orbital_SWFT = 0x1A7,
            Infiltrator = 0x26,
            Infiltrator_TRAC = 0x102,
            Infiltrator_PYTN = 0x19F,
            Hazop = 0x16,
            Hazop_FRST = 0xEA,
            EOD = 0x1A,
            EOD_SHOW = 0xF6,
            Oceanic = 0x6F,
            Oceanic_CRCT = 0xE2,
            Oceanic_SLID = 0x19A,
            Enforcer = 0x39,
            Enforcer_TRBL = 0xBA,
            Enforcer_TRCR = 0x1A3,
            Wetwork = 0x78,
            Wetwork_SHRD = 0x10A,
            Operator = 0x7C,
            Operator_SRFC = 0x10E,
            Pioneer = 0x22,
            Pioneer_ADPT = 0x112,
            Pathfinder = 0x45,
            Pathfinder_CORE = 0x116,
            Engineer = 0x8E,
            Engineer_EDGE = 0x11A,
            Stalker = 0x92,
            Stalker_CRSH = 0x11E,
            Rogue = 0x96,
            Rogue_FCUS = 0x122,
            Tracker = 0x9A,
            Tracker_ADRT = 0x126,
            Vanguard = 0x35,
            Vanguard_CNVG = 0xEE,
            CIO = 0x49,
            CIO_WEB = 0xD6,
            CIO_RUSH = 0x1B2,
            CIO_RUIN = 0x1BB,
            Venator = 0x5F,
            Venator_RPTR = 0xC2,
            Venator_BYNT = 0x1AC,
            Venator_RFCT = 0x1C5,
            Gungnir = 0x3D,
            Gungnir_PULS = 0xF2,
            Commando = 0x73,
            Commando_FRCT = 0xFE,
            Ranger = 0x80,
            Ranger_STRK = 0xFA,
            Protector = 0x84,
            Protector_DRFT = 0xDE,
            Mark_VI = 0x88,
            Raider = 0x53,
            Raider_DSTT = 0xB6,
            FOTUS = 0x4D,
            MarkV = 0x1CB,
            ODST = 0x1D1,
            Prefect = 0x1D7,
            Ricochet = 0x1DD
        }

        public enum Forearms
        {
            Recruit = 0x05,
            Recruit_PRME = 0x9B,
            Twin_plated = 0x2D,
            Twin_plated_AEON = 0x9C,
            Twin_plated_SWFT = 0x1A8,
            GV09Locking = 0x55,
            GV09Locking_FRST = 0x9D,
            Inner_plated = 0x2C,
            Inner_plated_WEB = 0x9E,
            Inner_plated_TOXC = 0x1C0,
            Outer_plated = 0x56,
            Outer_plated_RPTR = 0x9F,
            Outer_plated_BYNT = 0x1AD,
            Outer_plated_RFCT = 0x1C6,
            Contoured = 0x58,
            Contoured_SHRD = 0xA2,
            XV27Shifting = 0x57,
            XV27Shifting_PULS = 0xA0,
            XV27Shifting_CRCT = 0xA1,
            XV27Shifting_SLID = 0x19B,
            Mark_VI = 0x89,
            FOTUS = 0x4E,
            Mark_V = 0x1CC,
            ODST = 0x1D2,
            Prefect = 0x1D8,
            Ricochet = 0x1DE
        }

        public enum Legs
        {
            Recruit = 0x59,
            Recruit_TIGR = 0xA5,
            RG63Counter = 0x04,
            RG63Counter_PRME = 0xA3,
            RG63Counter_FRST = 0xA4,
            LG50Bulk = 0x5C,
            LG50Bulk_PULS = 0xA9,
            LG50Bulk_CLMN = 0x1B7,
            Outer_plated = 0x2B,
            Outer_plated_CRCT = 0xA6,
            Outer_plated_TOXC = 0x1C1,
            XG89Narrow = 0x5A,
            XG89NarrowRPTR = 0xA7,
            XG89Narrow_BYNT = 0x1AE,
            XG89Narrow_RFCT = 0x1C7,
            Contoured = 0x5B,
            Contoured_SHRD = 0xA8,
            Over_locking = 0x50,
            Over_locking_WEB = 0xAA,
            Mark_VI = 0x8A,      
            FOTUS = 0x4F,
            Mark_V = 0x1CD,
            ODST = 0x1D3,
            Prefect = 0x1D9,
            Ricochet = 0x1DF
        }

        public enum Visor
        {
            Recruit = 0x06,
            Solar = 0x12B,
            Frost = 0x12C,
            Midnight = 0x12D,
            Cyan = 0x12E,
            Blindside = 0x12F,
            Sunspot = 0x130,
            Verdant = 0x131,
            Legendary = 0x132,
            Wetwork = 0x133,
            Operator = 0x134,
            Pioneer = 0x135,
            Pathfinder = 0x136,
            Engineer = 0x137,
            Stalker = 0x138,
            Rogue = 0x139,
            Tacker = 0x13A,
        }

        public enum Stance
        {
            Recruit = 0x13B,
            Heroic = 0x13C,
            Assassin = 0x13D,
            LastStand = 0x13E,
            Breach = 0x13F,
            StandOff = 0x140,
            Believe = 0x141,
            Assault = 0x142,
            Menace = 0x1E1,
            Grandstand = 0x1E2,
            Seasoned = 0x1E3,
            Determined = 0x1E4,
            Confident = 0x1E5,
            Lookout = 0x1E6,
            Drawn = 0x1E7,
            Loyalty = 0x1E8,
            Flank = 0x1E9
        }

        // Loadouts
        public enum ProfileLoadout
        {
            All_Loadouts = 0x1C7
        }

        public enum PrimaryWeapon
        {
            AssaultRifle = 0x1EA,
            AssaultRifle_PRD = 0x21F,
            AssaultRifle_PRM = 0x220,
            AssaultRifle_TRM = 0x22A,
            AssaultRifle_TTH = 0x22B,
            AssaultRifle_RPT = 0x22C,
            AssaultRifle_TBN = 0x22D,
            BattleRifle = 0x203,
            BattleRifle_DNE = 0x228,
            BattleRifle_ACT = 0x229,
            BattleRifle_BLS = 0x231,
            BattleRifle_SLV = 0x232,
            BattleRifle_STM = 0x233,
            DMR = 0x1F1,
            DMR_NBL = 0x222,
            DMR_STP = 0x234,
            DMR_BNS = 0x235,
            DMR_CTE = 0x236,
            DMR_MCL = 0x237,
            StormRifle = 0x1F9,
            StormRifle_CMP = 0x23A,
            StormRifle_CHL = 0x23B,
            CovenantCarbine = 0x1FD,
            CovenantCarbine_RGN = 0x225,
            CovenantCarbine_LMV = 0x23C,
            CovenantCarbine_ENG = 0x23D,
            Suppressor = 0x204,
            Suppressor_SHA = 0x227,
            Suppressor_MCN = 0x23E,
            Suppressor_RFC = 0x23F, 
            LightRifle = 0x1F8,
            LightRifle_IMP = 0x224,
            LightRifle_IDT = 0x240
        }

        public enum SecondaryWeapon
        {
            Magnum = 0x1EB,
            Magnum_STC = 0x221,
            Magnum_IND = 0x22E,
            Magnum_FLR = 0x22F,
            Magnum_CBN = 0x230,
            PlasmaPistol = 0x1F5,
            PlasmaPistol_FCT = 0x223,
            PlasmaPistol_BPL = 0x238,
            Boltshot = 0x1FF,
            Boltshot_PST = 0x226,
            Boltshot_ENG = 0x239
        }

        public enum Grenade
        {
            FragGrenade = 0x1EC,
            PlasmaGrenade = 0x1ED,
            PulseGrenade = 0x200
        }

        public enum ArmorAbility
        {
            None = 0x21D,
            PrometheanVision = 0x1F6,
            ThrusterPack = 0x1FE,
            Hologram = 0x1F4,
            JetPack = 0x1F2,
            HardlightShield = 0x1F7,
            ActiveCamouflage = 0x1F3,
            Autosentry = 0x1FA,
            RegenerationField = 0x205
        }

        public enum TacticalPackage
        {
            None = 0x217,
            Mobility = 0x206,
            Shielding = 0x20B,
            Resupply = 0x20C,
            AAEfficiency = 0x207,
            Grenadier = 0x1FC,
            Firepower = 0x212,
            FastTrack = 0x216,
            Requisition = 0x215,
            Wheelman = 0x213,
            Resistor = 0x241
        }

        public enum SupportUpgrade
        {
            None = 0x218,
            Ammo = 0x202,
            Dexterity = 0x20A,
            Sensor = 0x201,
            Awareness = 0x208,
            Explosives = 0x211,
            OrdnancePriority = 0x209,
            Stability = 0x20E,
            Gunner = 0x214,
            Stealth = 0x20D,
            Nemesis = 0x20F,
            DropRecon = 0x210,
            Recharge = 0x242,
            Survivor = 0x243,
        }
        
        // Emblems
        public enum EmblemForeground
        {
            Recruit = 0x181,
            Tomcat = 0x181,
            Triad = 0x181,
            BearClaw = 0x181,
            Wasp = 0x181,
            Campafire = 0x181,
            CupOfDeath = 0x181,
            ActiveRooster = 0x181,
            BullTrue = 0x181,
            Leo = 0x181,
            Drone = 0x181,
            Atomic = 0x148,
            Grunt = 0x148,
            DogTags = 0x148,
            YingYang = 0x148,
            NoCamping = 0x148,
            Radioactive = 0x148,
            Crosshairs = 0x148,
            Anchor = 0x148,
            Runes = 0x148,
            Coned = 0x148, // Cone'D
            FlamingNinja = 0x149,
            Stuck = 0x149,
            Halt = 0x149,
            BlackWidow = 0x149,
            FlamingHorns = 0x149,
            Wolf = 0x149,
            Valkyrie = 0x149,
            SpartanHelmet = 0x149,
            Pirate = 0x149,
            Snake = 0x149,
            Headshot = 0x14A,
            SkullKing = 0x14A,
            SpartanSwords = 0x14A,
            Horse = 0x14A,
            CrossedSwords = 0x14A,
            Helmet = 0x14A,
            JollyRoger = 0x14A,
            SpartanLeague = 0x14A,
            RankUp = 0x14B,
            Wetworks = 0x14D,
            Stealth = 0x14D,
            ArrowOnTarget = 0x14D,
            KillerBee = 0x14D,
            Operator = 0x14F,
            Winged = 0x14F,
            Anchored = 0x14F,
            HogTire = 0x14F,
            Pioneer = 0x151,
            Arrowhead = 0x151,
            Compass = 0x151,
            Missile = 0x151,
            Pathfinder = 0x153,
            Lens = 0x153,
            Grid = 0x153,
            ReconBolt = 0x153,
            Engineer = 0x155,
            Screw = 0x155,
            Wrench = 0x155,
            Network = 0x155,
            Rogue = 0x159,
            Muzzled = 0x159,
            Patch = 0x159,
            Avian = 0x159,
            Tracker = 0x15B,
            Planetary = 0x15B,
            TheTrail = 0x15B,
            Celestial = 0x15B,
            Stalker = 0x157,
            Foxed = 0x157,
            EvilStare = 0x157,
            TheEye = 0x157,
            Mastery = 0x184,
            Projectile = 0x15D,
            Energy = 0x15E,
            Hardlight = 0x15F,
            Splatter = 0x160,
            Covenant = 0x161,
            Promethean = 0x162,
            Vehicular = 0x163,
            OneOneSeven = 0x164,
            Ordnance = 0x165,
            TheSlayer = 0x166,
            Kingslayer = 0x167,
            Flag = 0x168,
            Extracted = 0x169,
            TheHill = 0x16A,
            Baller = 0x16B,
            Infected = 0x16C,
            Dominated = 0x16D,
            OnYourShield = 0x16E,
            Raider = 0x16F,
            RaiderDistort = 0x170,
            Wiseguy = 0x171,
            IkClub = 0x172,
            LASO = 0x173,
            Overachiever = 0x174,
            ONI = 0x175,
            Guilty = 0x176,
            FlyingColors = 0x177,
            Corbulo = 0x178,
            Prime = 0x17E,
            Unicorn = 0x17F,
            Falcon = 0x180,
            Bonebreaker = 0x179,
            Assassin = 0x17A,
            Bulletproof = 0x17B,
            Spartan = 0x17C,
            Mjolnir = 0x17D,
            Shark = 0x143,
            Stingray = 0x143,
            Jaguar = 0x143,
            Tiger = 0x143,
            Eagle = 0x143,
            Rhino = 0x143,
            T_Rex = 0x143,
            Viper = 0x143
        }

        public enum EmblemBackground
        {
            Recruit = 0x181,
            Circle = 0x181,
            Diamond = 0x181,
            Plus = 0x181,
            Square = 0x181,
            Triangle = 0x181,
            VerticalStripe = 0x181,
            HorizontalStripe = 0x181,
            Cleft = 0x181,
            CrissCross = 0x181,
            BuzzSaw = 0x181,
            Star = 0x182,
            CowboyHat = 0x182,
            FourDiamonds = 0x182,
            Sun = 0x182,
            Hexagon = 0x182,
            Chalice = 0x182,
            Cog = 0x182,
            Octagon = 0x182,
            Crown = 0x182,
            Cancel = 0x182,
            HorizontalStripes = 0x183,
            Gradient = 0x183,
            HorizontalGradient = 0x183,
            Oval = 0x183,
            VerticalOval = 0x183,
            BluntDiamond = 0x183,
            BluntDiamondTall = 0x183,
            Asterisk = 0x183,
            Shield = 0x183,
            BallOFire = 0x183,
            MovingUp = 0x14C,
            Doused = 0x14E,
            Operated = 0x150,
            PieIsNear = 0x152,
            A_Asterisk = 0x154,
            Engineered = 0x156,
            GoingRogue = 0x15A,
            FoundYou = 0x15C,
            StalkingYou = 0x158,
            Maximum = 0x185,
            Live = 0x186,
            Limited = 0x18C,
            FOTUS = 0x18D,
            Structure = 0x18E,
            Vortex = 0x187,
            Chevron = 0x188,
            Angles = 0x189,
            Panels = 0x18A,
            Power = 0x18B,
            Shark = 0x144,
            Stingray = 0x144,
            Jaguar = 0x144,
            Tiger = 0x144,
            Eagle = 0x144,
            Rhino = 0x144,
            T_Rex = 0x144,
            Viper = 0x144
        }

        private readonly EndianIO _io;
        internal List<bool[]> unlockables;
        internal BitStream stream;

        public ProfileUnlockables(EndianIO io)
        {           
            var endianIo = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.SeekTo(0x170);
            endianIo.Out.Write(io.In.ReadBytes(8));
            endianIo.Out.Write(io.In.ReadBytes(0x150));
            endianIo.Out.Write(TransformBlock(io.In.ReadBytes(0x16C), true));
            endianIo.Out.Write(io.In.ReadBytes(0x150));
            endianIo.Out.Write(TransformBlock(io.In.ReadBytes(0x16C), true));
            endianIo.Out.Write(io.In.ReadBytes(0x10));
            endianIo.Out.Write(TransformBlock(io.In.ReadBytes(0x16C), true));

            _io = new EndianIO(endianIo.ToArray(), EndianType.BigEndian, true);
            _io.SeekTo(0x158);
            
            stream = new BitStream(SwapBitArray(_io.In.ReadBytes(0x16C)));

            unlockables = new List<bool[]>();
            for (int i = 0; i < (0x16C * 8) / 5; i++)
            {
                bool[] uBits = new bool[5];
                stream.Read(uBits);
                unlockables.Add(uBits);
            }
        }
        static uint Reverse(uint x)
        {
            uint y = 0;
            for (int i = 0; i < 32; ++i)
            {
                y <<= 1;
                y |= (x & 1);
                x >>= 1;
            }
            return y;
        }

        internal byte[] Save()
        {
            var @out = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            stream.Position = 0;
            foreach (bool[] unlock in unlockables)
            {
                stream.Write(unlock);
            }

            // transform and write back profile data

            _io.SeekTo(0);
            @out.Out.Write(_io.In.ReadBytes(0x158));
            @out.Out.Write(TransformBlock(SwapBitArray(stream.ToByteArray()), false));
            @out.Out.Write(_io.In.ReadBytes(0x150));
            @out.Out.Write(TransformBlock(_io.In.ReadBytes(0x16C), false));
            @out.Out.Write(_io.In.ReadBytes(0x10));
            @out.Out.Write(TransformBlock(_io.In.ReadBytes(0x16C), false));

            return @out.ToArray();
        }
        readonly int[] transformTable1 = new [] { 0x00, 0x1EA, 0x193, 0x22A };
        readonly int[][] tranformTable2 = new [] { new [] {0x00, 0x192}, new [] {0x193, 0x1D2}, new [] {0x1D3, 0x229}, new []{0x22A, 0x243}};

        private byte[] TransformBlock(byte[] srcBlock, bool reading)
        {
            int c, d, e, g, h, k;

            const uint a = 1;
            byte[] dstBlock = new byte[0x16C];
            if(reading)
            {
                for (int i = 0; i < 4; i++)
                {
                    int[] b = tranformTable2[i];
                    c = b[0];
                    d = b[1] + (b[1] << 2);
                    e = (c + (c << 2));
                    uint f = a.RotateLeft(e);
                    if (e <= d)
                    {
                        g = transformTable1[i];
                        d -= e;
                        c = g - c;
                        k = d + 1;
                        h = c + (c << 2);
                        for (int j = 0; j < k; j++)
                        {
                            d = e >> 5;
                            c = h + e;
                            g = d << 2;
                            d = srcBlock.ReadInt32(g);
                            g = (int) (d & f);
                            d = c >> 5;
                            d <<= 2;
                            c &= 0x1F;
                            c = 1 << c;
                            var z = dstBlock.ReadInt32(d);
                            if (g == 0)
                            {
                                c = z & (~c);
                            }
                            else
                            {
                                c |= z;
                            }
                            dstBlock.WriteInt32(d, c);
                            e++;
                            f = f.RotateLeft(1);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    int[] b = tranformTable2[i];
                    c = b[0];
                    e = (c + (c << 2));
                    d = (b[1] + (b[1] << 2));
                    uint f = a.RotateLeft(e);
                    if (e <= d)
                    {
                        g = transformTable1[i];
                        d -= e;
                        c = g - c;
                        k = d + 1;
                        g = c + (c << 2);
                        for (int j = 0;  j < k; j++)
                        {
                            c = g + e;
                            d = c >> 5;
                            c &= 0x1F;
                            d <<= 2;
                            c = 1 << c;
                            d = srcBlock.ReadInt32(d);
                            int l = c & d;
                            c = e >> 5;
                            c <<= 2;
                            int z = dstBlock.ReadInt32(c);
                            if (l == 0)
                            {
                                z = (int) (z & (~f));
                            }
                            else 
                            {
                                z = (int) ((uint)z | f);
                            }
                            dstBlock.WriteInt32(c, z);
                            e++;
                            f = f.RotateLeft(1);
                        }
                    }
                }
            }
            return dstBlock;
        }

        /*
        private int GetItemUnlockOffset(int itemId, int bitIndex)
        {
            int index = (itemId << 0x02) + itemId + bitIndex;
            int mask = 0x01 << (index & 0x1F);
            index = ((index >> 0x05) + 0x56) << 0x02;
            _io.SeekTo(index);
            return mask;
        }
        */
        /// <summary>
        ///  Endian swap bit array since the Xbox360 is Big Endian
        /// </summary>
        /// <param name="input">Data stream to swap by Integer</param>
        /// <returns></returns>
        private byte[] SwapBitArray(byte[] input)
        {
            EndianIO swapIO = new EndianIO(input, EndianType.BigEndian, true);
            for (int i = 0; i < swapIO.Length / 4; i++)
            {
                swapIO.SeekTo(i * 4);
                uint v = swapIO.In.ReadUInt32();
                swapIO.SeekTo(i * 4);
                uint t = Reverse(v);
                swapIO.Out.Write(t);
            }

            return swapIO.ToArray();
        }
        public bool IsUnlocked(object item)
        {
            int index = Convert.ToInt32(item);
            return unlockables[index][3] | unlockables[index][0];
        }
        
        public void SetLock(object itemId, bool isUnlocked)
        {
            // set bits in lock table
            int index = Convert.ToInt32(itemId);
            unlockables[index][0] = unlockables[index][3] = true;
        }
    }

    public class ProfileLoadouts
    {
        public enum Weapon : byte 
        {
            DMR = 0x00,
            AssaultRifle = 0x01,
            PlasmaPistol = 0x02,
            Magnum = 0x05,
            LightRifle = 0x18,
            StormRifle = 0x1E,
            CovenantCarbine = 0x22,
            Boltshot = 0x27,
            BattleRifle = 0x2B,
            Suppressor = 0x2C,
        }

        public enum PrimaryWeaponSkin : byte
        {
            AssaultRifle_PRD = 0x01,
            AssaultRifle_PRM = 0x02,
            AssaultRifle_TRM = 0x03,
            AssaultRifle_TTH = 0x04,
            AssaultRifle_RPT = 0x05,
            AssaultRifle_TBN = 0x06,

            BattleRifle_ACT = 0x01,
            BattleRifle_DNE = 0x02,
            BattleRifle_BLS = 0x03,
            BattleRifle_SLV = 0x04,
            BattleRifle_STM = 0x05,

            DMR_NBL = 0x01,
            DMR_STP = 0x02,
            DMR_BNS = 0x03,
            DMR_CTE = 0x04,
            DMR_MCL = 0x05,

            StormRifle_CMP = 0x01,
            StormRifle_CHL = 0x02,

            CovenantCarbine_RGN = 0x01,
            CovenantCarbine_LMV = 0x02,
            CovenantCarbine_ENG = 0x03,

            Suppressor_SHA = 0x01,
            Suppressor_MCN = 0x03,
            Suppressor_RFC = 0x04,

            LightRifle_IMP = 0x01,
            LightRifle_IDT = 0x03
        }

        public enum SecondaryWeaponSkin : byte
        {
            Magnum_STC = 0x01,
            Magnum_IND = 0x02,
            Magnum_FLR = 0x03,
            Magnum_CBN = 0x04,
            PlasmaPistol_FCT = 0x01,
            PlasmaPistol_BPL = 0x02,
            Boltshot_PST = 0x01,
            Boltshot_ENG = 0x02
        }

        public enum Grenade : byte
        {
            FragGrenade,
            PlasmaGrenade,
            PulseGrenade
        }

        public enum ArmorAbility : byte
        {
            None = 0x15,
            PrometheanVision = 0x11,
            ThrusterPack = 0x12,
            Hologram = 0x07,
            JetPack = 0x01,
            HardlightShield = 0x0C,
            ActiveCamouflage = 0x04,
            Autosentry = 0x0E,
            RegenerationField = 0x14
        }

        public enum TacticalPackage : byte
        {
            NoneTactical = 0x15,
            Mobility = 0x01,
            Shielding = 0x06,
            Resupply = 0x08,
            AAEfficiency = 0x07,
            Grenadier = 0x02,
            Firepower = 0x09,
            FastTrack = 0x13,
            Requisition = 0x12,
            Wheelman = 0x10,
            Resistor = 0x19
        }

        public enum SupportUpgrade : byte
        {
            NoneSupport = 0x16,
            Ammo = 0x0B,
            Dexterity = 0x03,
            Sensor = 0x00,
            Awareness = 0x05,
            Explosives = 0x14,
            OrdnancePriority = 0x04,
            Stability = 0x0D,
            Gunner = 0x11,
            Stealth = 0x0C,
            Nemesis = 0x0E,
            DropRecon = 0x0F,
            Survivor = 0x17,
            Recharge = 0x18
        }

        public class Loadout
        {
            public byte TacticalPackage;
            public byte SupportUpgrade;
            public byte PrimaryWeapon;
            public byte SecondaryWeapon;
            public byte PrimaryWeaponSkin;
            public byte SecondaryWeaponSkin;
            public byte ArmorAbility;
            public byte Grenade;
        }

        private readonly EndianIO _io;

        public int AvailableLoadouts;
        public List<Loadout> Loadouts;
 
        public ProfileLoadouts(EndianIO io)
        {
            _io = io;
            Read();
        }

        private void Read()
        {
            AvailableLoadouts = _io.In.SeekNReadInt32(0xA48);
            if(AvailableLoadouts <= 0x00) return;
            Loadouts = new List<Loadout>();
            for (int i = 0; i < AvailableLoadouts; i++)
            {
                Loadouts.Add(new Loadout
                 {
                     TacticalPackage = _io.In.ReadByte(),
                     SupportUpgrade = _io.In.ReadByte(),
                     PrimaryWeapon = _io.In.ReadByte(),
                     SecondaryWeapon = _io.In.ReadByte(),
                     PrimaryWeaponSkin = _io.In.ReadByte(),
                     SecondaryWeaponSkin = _io.In.ReadByte(),
                     ArmorAbility = _io.In.ReadByte(),
                     Grenade = _io.In.ReadByte()
                 });
            }
        }

        public void Save()
        {
            _io.Out.SeekTo(0xA4C);
            foreach (Loadout loadout in Loadouts)
            {
                _io.Out.WriteByte(loadout.TacticalPackage);
                _io.Out.WriteByte(loadout.SupportUpgrade);
                _io.Out.WriteByte(loadout.PrimaryWeapon);
                _io.Out.WriteByte(loadout.SecondaryWeapon);
                _io.Out.WriteByte(loadout.PrimaryWeaponSkin);
                _io.Out.WriteByte(loadout.SecondaryWeaponSkin);
                _io.Out.WriteByte(loadout.ArmorAbility);
                _io.Out.WriteByte(loadout.Grenade);
            }
        }
    }

    public class Halo4Settings : HaloSettings
    {
        public struct Halo4Profile
        {
            public uint Experience;
            public uint SpartanPoints; // Lifetime SP
        }

        public Halo4Profile Profile;
        public Halo4Campaign Campaign;
        public ProfileLoadouts Loadouts;
        public ProfileUnlockables Unlockables;

        public Halo4Settings(EndianIO io)
            : base(io, HaloGame.Halo4)
        {
            Profile.SpartanPoints = SettingsIO.In.SeekNReadUInt32(0x178);
            Profile.Experience = SettingsIO.In.SeekNReadUInt32(0x180);
            Campaign = new Halo4Campaign(SettingsIO);

            Loadouts = new ProfileLoadouts(SettingsIO);
            Unlockables = new ProfileUnlockables(SettingsIO);
        }

        public void Save()
        {
            SettingsIO.SeekTo(0x170);
            SettingsIO.Out.Write(Unlockables.Save());

            SettingsIO.Out.SeekNWrite(0x178, Profile.SpartanPoints);
            SettingsIO.Out.SeekNWrite(0x180, Profile.Experience);

            Campaign.Save();
            Loadouts.Save();

            // flush data
            SaveToFile();
        }
    }
}
