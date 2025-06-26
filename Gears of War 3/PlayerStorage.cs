using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Horizon.Functions;

namespace Horizon.PackageEditors.Gears_of_War_3
{
    internal class Entry
    {
        internal DataType DataType;
        internal EntryID ID;

        internal bool bData;
        internal int nData;
        internal float fData;

        internal byte Unknown;
        internal byte Unknown2;
    }

    internal enum DataType : byte
    {
        Bool = 0,
        Int32 = 1,
        Float = 5
    }

    internal enum EntryID : int
    {
        Selected_COG_Character = 0x002C,
        Selected_Locust_Character = 0x002D,

        Selected_Assault_Rifle = 0x0032,
        Selected_Shotgun = 0x0082,

        Selected_Medal = 0x0062,

        Lancer_Skin = 0x006A,
        Hammerburst_Skin = 0x006B,
        Retro_Lancer_Skin = 0x006C,
        Gnasher_Shotgun_Skin = 0x006D,
        SawedOff_Shotgun_Skin = 0x006E,

        Selected_Title = 0x006F,

        SyncID = 0x0001,

        Medal_Seriously = 0x0002,
        Medal_Old_Guard = 0x0003,
        Medal_It_Has_Begun = 0x00E9, // It Has Begun!
        Medal_Beta_Tester = 0x0F4,
        Medal_Epic = 0x00F5,
        Medal_Gears_of_War_3_Developer = 0x0108,
        Medal_Embry_Star = 0x0134,

        Medal_Vengeful_0 = 0x0004,
        Medal_Vengeful_1 = 0x0005,
        Medal_Vengeful_2 = 0x0006,
        Medal_Vengeful_3 = 0x0007,
        Medal_Captor_0 = 0x0008,
        Medal_Captor_1 = 0x0009,
        Medal_Captor_2 = 0x000A,
        Medal_Captor_3 = 0x000B,
        Medal_War_Supporter_0 = 0x000C,
        Medal_War_Supporter_1 = 0x000D,
        Medal_War_Supporter_2 = 0x000E,
        Medal_War_Supporter_3 = 0x000F,
        Medal_MVP_0 = 0x0011,
        Medal_MVP_1 = 0x0012,
        Medal_MVP_2 = 0x0013,
        Medal_MVP_3 = 0x0014,
        Medal_Veteran_0 = 0x0015,
        Medal_Veteran_1 = 0x0016,
        Medal_Veteran_2 = 0x0017,
        Medal_Veteran_3 = 0x0018,
        Medal_Match_Winner_0 = 0x0019,
        Medal_Match_Winner_1 = 0x001A,
        Medal_Match_Winner_2 = 0x001B,
        Medal_Match_Winner_3 = 0x001C,
        Medal_Headshot_0 = 0x001D,
        Medal_Headshot_1 = 0x001E,
        Medal_Headshot_2 = 0x001F,
        Medal_Headshot_3 = 0x0020,
        Medal_Heavy_Weapons_0 = 0x0021,
        Medal_Heavy_Weapons_1 = 0x0022,
        Medal_Heavy_Weapons_2 = 0x0023,
        Medal_Heavy_Weapons_3 = 0x0024,
        Medal_Explosives_0 = 0x0025,
        Medal_Explosives_1 = 0x0026,
        Medal_Explosives_2 = 0x0027,
        Medal_Explosives_3 = 0x0028,
        Medal_Finisher_0 = 0x0029,
        Medal_Finisher_1 = 0x002A,
        Medal_Finisher_2 = 0x002B,
        Medal_Finisher_3 = 0x002C,
        Medal_Skunker_0 = 0x002E,
        Medal_Skunker_1 = 0x002F,
        Medal_Skunker_2 = 0x0030,
        Medal_Skunker_3 = 0x0031,
        Medal_Leader_0 = 0x0033,
        Medal_Leader_1 = 0x0034,
        Medal_Leader_2 = 0x0035,
        Medal_Leader_3 = 0x0036,
        Medal_Abductor_0 = 0x0038,
        Medal_Abductor_1 = 0x0039,
        Medal_Abductor_2 = 0x003A,
        Medal_Abductor_3 = 0x003B,
        Medal_Assistant_0 = 0x003C,
        Medal_Assistant_1 = 0x003D,
        Medal_Assistant_2 = 0x003E,
        Medal_Assistant_3 = 0x003F,
        Medal_Medic_0 = 0x0041,
        Medal_Medic_1 = 0x0042,
        Medal_Medic_2 = 0x0043,
        Medal_Medic_3 = 0x0044,
        Medal_Cover_0 = 0x0045,
        Medal_Cover_1 = 0x0046,
        Medal_Cover_2 = 0x0047,
        Medal_Cover_3 = 0x0048,
        Medal_Active_Reloader_0 = 0x0049,
        Medal_Active_Reloader_1 = 0x004A,
        Medal_Active_Reloader_2 = 0x004B,
        Medal_Active_Reloader_3 = 0x004C,
        Medal_Lancer_0 = 0x004D,
        Medal_Lancer_1 = 0x004E,
        Medal_Lancer_2 = 0x004F,
        Medal_Lancer_3 = 0x0050,
        Medal_Hammerburst_0 = 0x0051,
        Medal_Hammerburst_1 = 0x0052,
        Medal_Hammerburst_2 = 0x0053,
        Medal_Hammerburst_3 = 0x0054,
        Medal_Retro_Lancer_0 = 0x0055,
        Medal_Retro_Lancer_1 = 0x0056,
        Medal_Retro_Lancer_2 = 0x0057,
        Medal_Retro_Lancer_3 = 0x0058,
        Medal_Gnasher_Shotgun_0 = 0x0059,
        Medal_Gnasher_Shotgun_1 = 0x005A,
        Medal_Gnasher_Shotgun_2 = 0x005B,
        Medal_Gnasher_Shotgun_3 = 0x005C,
        Medal_SawedOff_Shotgun_0 = 0x005D, //Sawed-off Shotgun
        Medal_SawedOff_Shotgun_1 = 0x005E, //Sawed-off Shotgun
        Medal_SawedOff_Shotgun_2 = 0x005F, //Sawed-off Shotgun
        Medal_SawedOff_Shotgun_3 = 0x0060, //Sawed-off Shotgun
        Medal_Pistols_0 = 0x0061,
        Medal_Pistols_1 = 0x0062,
        Medal_Pistols_2 = 0x0063,
        Medal_Pistols_3 = 0x0064,
        Medal_Spotter_0 = 0x0065,
        Medal_Spotter_1 = 0x0066,
        Medal_Spotter_2 = 0x0067,
        Medal_Spotter_3 = 0x0068,
        Medal_Pyro_0 = 0x0069,
        Medal_Pyro_1 = 0x006A,
        Medal_Pyro_2 = 0x006B,
        Medal_Pyro_3 = 0x006C,
        Medal_Sapper_0 = 0x006D,
        Medal_Sapper_1 = 0x006E,
        Medal_Sapper_2 = 0x006F,
        Medal_Sapper_3 = 0x0070,
        Medal_Guardian_0 = 0x0071,
        Medal_Guardian_1 = 0x0072,
        Medal_Guardian_2 = 0x0073,
        Medal_Guardian_3 = 0x0074,
        Medal_CTL_0 = 0x0075,
        Medal_CTL_1 = 0x0076,
        Medal_CTL_2 = 0x0077,
        Medal_CTL_3 = 0x0078,
        Medal_KOTH_0 = 0x0079,
        Medal_KOTH_1 = 0x007A,
        Medal_KOTH_2 = 0x007B,
        Medal_KOTH_3 = 0x007C,
        Medal_Warzone_0 = 0x007D,
        Medal_Warzone_1 = 0x007E,
        Medal_Warzone_2 = 0x007F,
        Medal_Warzone_3 = 0x0080,
        Medal_Execution_0 = 0x0081,
        Medal_Execution_1 = 0x0082,
        Medal_Execution_2 = 0x0083,
        Medal_Execution_3 = 0x0084,
        Medal_TDM_0 = 0x0085,
        Medal_TDM_1 = 0x0086,
        Medal_TDM_2 = 0x0087,
        Medal_TDM_3 = 0x0088,
        Medal_Wingman_0 = 0x0089,
        Medal_Wingman_1 = 0x008A,
        Medal_Wingman_2 = 0x008B,
        Medal_Wingman_3 = 0x008C,
        Medal_Allfathers_0 = 0x008D,
        Medal_Allfathers_1 = 0x008E,
        Medal_Allfathers_2 = 0x008F,
        Medal_Allfathers_3 = 0x0090,
        Medal_Master_at_Arms_0 = 0x0091, //Master-at-Arms
        Medal_Master_at_Arms_1 = 0x0092, //Master-at-Arms
        Medal_Master_at_Arms_2 = 0x0093, //Master-at-Arms
        Medal_Master_at_Arms_3 = 0x0094, //Master-at-Arms
        Medal_Rifleman_0 = 0x0095,
        Medal_Rifleman_1 = 0x0096,
        Medal_Rifleman_2 = 0x0097,
        Medal_Rifleman_3 = 0x0098,
        Medal_Hard_Target_0 = 0x009A,
        Medal_Hard_Target_1 = 0x009B,
        Medal_Hard_Target_2 = 0x009C,
        Medal_Hard_Target_3 = 0x009D,
        Medal_Shock_Trooper_0 = 0x00A0,
        Medal_Shock_Trooper_1 = 0x00A1,
        Medal_Shock_Trooper_2 = 0x00A2,
        Medal_Shock_Trooper_3 = 0x00A3,
        Medal_Old_Schooler_0 = 0x00A4,
        Medal_Old_Schooler_1 = 0x00A5,
        Medal_Old_Schooler_2 = 0x00A6,
        Medal_Old_Schooler_3 = 0x00A7,
        Medal_Battle_Mistress_0 = 0x00A8,
        Medal_Battle_Mistress_1 = 0x00A9,
        Medal_Battle_Mistress_2 = 0x00AA,
        Medal_Battle_Mistress_3 = 0x00AB,
        Medal_Sovereign_0 = 0x00AC,
        Medal_Sovereign_1 = 0x00AD,
        Medal_Sovereign_2 = 0x00AE,
        Medal_Sovereign_3 = 0x00AF,
        Medal_Special_Teams_0 = 0x0B0,
        Medal_Special_Teams_1 = 0x0B1,
        Medal_Special_Teams_2 = 0x0B2,
        Medal_Special_Teams_3 = 0x0B3,
        Medal_Field_Service_0 = 0x0103,
        Medal_Field_Service_1 = 0x0104,
        Medal_Field_Service_2 = 0x0105,
        Medal_Field_Service_3 = 0x0106,
        Medal_For_the_Horde_0 = 0x0135, //For the Horde!
        Medal_For_the_Horde_1 = 0x0136, //For the Horde!
        Medal_For_the_Horde_2 = 0x0137, //For the Horde!
        Medal_For_the_Horde_3 = 0x0138, //For the Horde!
        Medal_Horder_0 = 0x00139,
        Medal_Horder_1 = 0x0013A,
        Medal_Horder_2 = 0x0013B,
        Medal_Horder_3 = 0x013C,
        Medal_Architect_0 = 0x013D,
        Medal_Architect_1 = 0x013E,
        Medal_Architect_2 = 0x013F,
        Medal_Architect_3 = 0x0140,
        Medal_Squad_Leader_0 = 0x0141,
        Medal_Squad_Leader_1 = 0x0142,
        Medal_Squad_Leader_2 = 0x0143,
        Medal_Squad_Leader_3 = 0x0144,
        Medal_Field_Engineer_0 = 0x0145,
        Medal_Field_Engineer_1 = 0x0146,
        Medal_Field_Engineer_2 = 0x0147,
        Medal_Field_Engineer_3 = 0x0148,
        Medal_Big_Money_0 = 0x0149,
        Medal_Big_Money_1 = 0x014A,
        Medal_Big_Money_2 = 0x014B,
        Medal_Big_Money_3 = 0x014C,
        Medal_Loot_Courtesan_0 = 0x014D,
        Medal_Loot_Courtesan_1 = 0x014E,
        Medal_Loot_Courtesan_2 = 0x014F,
        Medal_Loot_Courtesan_3 = 0x0150,
        Medal_Im_a_Beast_0 = 0x0151, //I'm a Beast!
        Medal_Im_a_Beast_1 = 0x0152, //I'm a Beast!
        Medal_Im_a_Beast_2 = 0x0153, //I'm a Beast!
        Medal_Im_a_Beast_3 = 0x0154, //I'm a Beast!
        Medal_Beastly_0 = 0x0155,
        Medal_Beastly_1 = 0x0156,
        Medal_Beastly_2 = 0x0157,
        Medal_Beastly_3 = 0x0158,
        Medal_Motivator_0 = 0x0159,
        Medal_Motivator_1 = 0x015A,
        Medal_Motivator_2 = 0x015B,
        Medal_Motivator_3 = 0x015C,
        Medal_Dismantler_0 = 0x015D,
        Medal_Dismantler_1 = 0x015E,
        Medal_Dismantler_2 = 0x015F,
        Medal_Dismantler_3 = 0x0160,
        Medal_Ruthless_0 = 0x0161,
        Medal_Ruthless_1 = 0x0162,
        Medal_Ruthless_2 = 0x0163,
        Medal_Ruthless_3 = 0x0164,
        Medal_Investor_0 = 0x0165,
        Medal_Investor_1 = 0x0166,
        Medal_Investor_2 = 0x0167,
        Medal_Investor_3 = 0x0168,
        Medal_High_Roller_0 = 0x0169,
        Medal_High_Roller_1 = 0x016A,
        Medal_High_Roller_2 = 0x016B,
        Medal_High_Roller_3 = 0x016C,
        Medal_King_of_COG_0 = 0x016D,
        Medal_King_of_COG_1 = 0x016E,
        Medal_King_of_COG_2 = 0x016F,
        Medal_King_of_COG_3 = 0x0170,
        Medal_Number_1_0 = 0x0171,
        Medal_Number_1_1 = 0x0172,
        Medal_Number_1_2 = 0x0173,
        Medal_Number_1_3 = 0x0174,
        Medal_Warmonger_0 = 0x0175,
        Medal_Warmonger_1 = 0x0176,
        Medal_Warmonger_2 = 0x0177,

        Profile_SyncID = 0x0178,
        Medal_Warmonger_3 = 0x0178,

        Medal_Force_Multiplier_0 = 0x0179,
        Medal_Force_Multiplier_1 = 0x017A,
        Medal_Force_Multiplier_2 = 0x017B,
        Medal_Force_Multiplier_3 = 0x017C,
        Medal_Survivalist_0 = 0x017D,
        Medal_Survivalist_1 = 0x017E,
        Medal_Survivalist_2 = 0x017F,
        Medal_Survivalist_3 = 0x0180,
        Medal_Aficionado_0 = 0x0181,
        Medal_Aficionado_1 = 0x0182,
        Medal_Aficionado_2 = 0x0183,
        Medal_Aficionado_3 = 0x0184,
        Medal_Doorman_0 = 0x0185,
        Medal_Doorman_1 = 0x0186,
        Medal_Doorman_2 = 0x0187,
        Medal_Doorman_3 = 0x0188,
        Medal_Tour_of_Duty_0 = 0x0189,
        Medal_Tour_of_Duty_1 = 0x018A,
        Medal_Tour_of_Duty_2 = 0x018B,
        Medal_Tour_of_Duty_3 = 0x018C,


        Ribbon_MVP = 0x0010,
        Ribbon_Never_Had_a_Chance = 0x002D,
        Ribbon_Untouchable = 0x0032,
        Ribbon_Captive_ating = 0x0037, //Captive-ating
        Ribbon_Medic = 0x0040,
        Ribbon_Last_Man_Out = 0x0099,
        Ribbon_Better_Man = 0x009E,
        Ribbon_First_Blood = 0x009F,
        Ribbon_Clusterluck = 0x00B4,
        Ribbon_Not_So_Fast = 0x00B5,
        Ribbon_Negotiation_Over = 0x00B6,
        Ribbon_Retribution = 0x00B7,
        Ribbon_Death_from_Beyond = 0x00B8,
        Ribbon_Godlike = 0x00B9,
        Ribbon_Invincible = 0x00BA,
        Ribbon_Unstoppable = 0x00BB,
        Ribbon_Rampage = 0x00BC,
        Ribbon_Killing_Spree = 0x00BD,
        Ribbon_Personal_Assistant = 0x00BE,
        Ribbon_Stop_Thief = 0x00BF, //Stop Thief!
        Ribbon_FIFO = 0x00C0, //F.I.F.O.
        Ribbon_Lead_by_Example = 0x00C1,
        Ribbon_Want_Something_Done = 0x00C2,
        Ribbon_Unlucky_Bastard = 0x00C3,
        Ribbon_Team_Player = 0x00C4,
        Ribbon_Clear = 0x00C5, //Clear!
        Ribbon_Buttoned_Up = 0x00C6,
        Ribbon_Grenade_Hug = 0x00C7,
        Ribbon_Top_of_the_Hill = 0x00C8,
        Ribbon_Ring_Breaker = 0x00C9,
        Ribbon_Dead_Ringer = 0x00CA,
        Ribbon_Mortarfied = 0x00CB,
        Ribbon_Boombardier = 0x00CC,
        Ribbon_Kaboom = 0x00CD, //Kaboom!
        Ribbon_Roadblock = 0x00CE,
        Ribbon_Death_from_Below = 0x00CF,
        Ribbon_Oscar_Mike = 0x00D0,
        Ribbon_The_Super = 0x00D1,
        Ribbon_Survivor = 0x00D4,
        Ribbon_Ring_King = 0x00D5,
        Ribbon_Coup_de_Grace = 0x00D6,
        Ribbon_Military_Intelligence = 0x00D7,
        Ribbon_Final_Word = 0x00D8,
        Ribbon_The_Double = 0x00D9,
        Ribbon_The_Triple = 0x00DA,
        Ribbon_The_Quad = 0x00DB,
        Ribbon_The_Quinn = 0x00DC,
        Ribbon_Sapper_Star = 0x00DD,
        Ribbon_Hat_Trick = 0x00DE,
        Ribbon_Ole = 0x00DF, //Ole!
        Ribbon_First_to_Fight = 0x00E0,
        Ribbon_The_Cleaner = 0x00E1,
        Ribbon_Methodical = 0x00E2,
        Ribbon_Vigilant = 0x00E3,
        Ribbon_Solid = 0x00E4,
        Ribbon_Smooth_Operator = 0x00E5,
        Ribbon_Tough_Guy = 0x00E6,
        Ribbon_Rough_Day = 0x00E7,
        Ribbon_Executioner = 0x00E8,
        Ribbon_Nemesis = 0x00EA,
        Ribbon_Trick_Shot = 0x00EB,
        Ribbon_Hail_Mary = 0x00EC,
        Ribbon_Swift_Vengeance = 0x00EE,
        Ribbon_Avenged = 0x00EF,
        Ribbon_Clutch = 0x00F0,
        Ribbon_Rear_Guard = 0x00F1,
        Ribbon_Secret_Service = 0x00F2,
        Ribbon_Eye_on_the_Prize = 0x00F3,
        Ribbon_Evasive = 0x00F6,
        Ribbon_Contender = 0x00F7,
        Ribbon_Spray_and_Pray = 0x00F8,
        Ribbon_Headhunter = 0x00F9,
        Ribbon_Carmines_Star = 0x00FA, //Carmine's Star
        Ribbon_Grenadier = 0x00FB,
        Ribbon_Pistoleer = 0x00FC,
        Ribbon_Quick_Clips = 0x00FD,
        Ribbon_Well_Protected = 0x00FE,
        Ribbon_Guys_Hello = 0x00FF, //Guys? Hello?
        Ribbon_Under_the_Radar = 0x0100,
        Ribbon_Lumberjack = 0x0101,
        Ribbon_Charge = 0x0102, //Charge!
        Ribbon_No_Smoking = 0x0107,
        Ribbon_Denied = 0x0109, //Denied!
        Ribbon_Death_from_Above = 0x010A,
        Ribbon_Pop_Goes_the_Weasel = 0x010B,
        Ribbon_Indigestion = 0x010C,
        Ribbon_Monkey_Dog = 0x010D, //Monkey-Dog
        Ribbon_Meatshop = 0x010E,
        Ribbon_Team_Shaman = 0x010F,
        Ribbon_Team_Savior = 0x0110,
        Ribbon_Pillager = 0x0111,
        Ribbon_Test_Driver = 0x00112,
        Ribbon_Antihero = 0x0113,
        Ribbon_Ready_for_the_Heavies = 0x0114,
        Ribbon_Just_in_Time = 0x0115,
        Ribbon_Long_Hauler = 0x0116,
        Ribbon_Point_Man = 0x0117,
        Ribbon_Combat_Engineer = 0x0118,
        Ribbon_Founder = 0x0119,
        Ribbon_Financier = 0x011A,
        Ribbon_Reconnaissance = 0x011B,
        Ribbon_Observer = 0x011C,
        Ribbon_Phat_Loot = 0x011D,
        Ribbon_Last_Hope = 0x011E,
        Ribbon_Go_on_without_Me = 0x011F,
        Ribbon_High_ROI = 0x0120,
        Ribbon_I_Gotcha = 0x0121,
        Ribbon_Like_a_Boss = 0x0122,
        Ribbon_Rope_a_Dope = 0x0123, //Rope-a-Dope
        Ribbon_Botanist = 0x0124,
        Ribbon_Mech_Jockey = 0x0125,
        Ribbon_Flyswatter = 0x0126,
        Ribbon_Quick_Kicker = 0x0127,
        Ribbon_Pull = 0x0128, //Pull!
        Ribbon_Dewormer = 0x0129,
        Ribbon_Pruner = 0x012A,
        Ribbon_Pig_Sticker = 0x012B,
        Ribbon_Ace = 0x012C,
        Ribbon_Hand_Holder = 0x012D,
        Ribbon_Wingman = 0x012E,
        Ribbon_Stockpiler = 0x012F,
        Ribbon_Conservationist = 0x0130,
        Ribbon_Number_1 = 0x0131,
        Ribbon_Priority_Target = 0x0132,
        Ribbon_On_Your_Feet_Soldier = 0x0133, //On Your Feet, Soldier
        Ribbon_Special_Delivery = 0x018D,
        Ribbon_Sacrifice = 0x018E,
        Ribbon_No_Wait = 0x018F, //No, Wait!
        Ribbon_Pacifist = 0x0190,
        Ribbon_So_Close = 0x0191,
        Ribbon_Stay_Down = 0x0192,
        Ribbon_Never_Surrender = 0x0193,

        Skin_Flaming_Hammerburst = 0x0196,
        Skin_Flaming_Lancer = 0x0197,
        Skin_Flaming_Gnasher_Shotgun = 0x0198,
        Skin_Flaming_SawedOff_Shotgun = 0x0199, // Flaming Sawed-off Shotgun

        Skin_Chrome_SawedOff_Shotgun = 0x019A, // Chrome Sawed-off Shotgun
        Skin_Chrome_Gnasher_Shotgun = 0x019B,

        Skin_Onyx_Lancer = 0x01A1,
        Skin_Onyx_Hammerburst = 0x01A2,
        Skin_Onyx_Retro_Lancer = 0x01A3,
        Skin_Onyx_Gnasher_Shotgun = 0x01A4,
        Skin_Onyx_SawedOff_Shotgun = 0x01A5, // Onyx Sawed-off Shotgun

        Skin_Gold_Retro_Lancer = 0x0195,
        Skin_Gold_Lancer = 0x01A6,
        Skin_Gold_Hammerburst = 0x01A7,
        Skin_Gold_Gnasher = 0x01A8,
        Skin_Gold_SawedOff = 0x01A9,
        Skin_Gold_Omen_Set = 0x01AA,

        Skin_Team_Insignia_Lancer = 0x01AB,
        Skin_Team_Insignia_Hammerburst = 0x01AC,
        Skin_Team_Insignia_Retro_Lancer = 0x01AD,
        Skin_Team_Insignia_Shotgun_Set = 0x01AE,

        Skin_Chrome_Retro_Lancer = 0x01AF,
        Skin_Chrome_Lancer = 0x01B0,
        Skin_Chrome_Hammerburst = 0x01B1,

        Skin_Crimson_Omen_Gnasher = 0x01B2,
        Skin_Crimson_Omen_Hammerburst = 0x01B3,
        Skin_Crimson_Omen_Retro_Lancer = 0x01B4,
        Skin_Crimson_Omen_SawedOff = 0x01B5, // Crimson Omen Sawed-off Shotgun

        Execution_Lancer_Rifle = 0x01B6,
        Execution_Hammerburst = 0x01B7,
        Execution_Retro_Lancer = 0x01B8,
        Execution_Gnasher_Shotgun = 0x01B9,
        Execution_SawedOff_Shotgun = 0x01BA, // Sawed-off
        Execution_Gorgon_Pistol = 0x01BB,
        Execution_Boltok_Pistol = 0x01BC,
        Execution_Boomshot = 0x01BD,
        Execution_Digger = 0x01BE,
        Execution_Hammer_of_Dawn = 0x01BF,
        Execution_Mulcher = 0x01C0,
        Execution_Mortar = 0x01C1,
        Execution_OneShot = 0x01C2,
        Execution_Scorcher = 0x01C3,
        Execution_Torque_Bow = 0x01C4,
        Execution_Longshot = 0x01C5,
        Execution_Cleaver = 0x01C6,

        Character_Cole_Train = 0x0194,
        Character_Locust_Miner = 0x01C7,
        Character_Beast_Rider = 0x01C8,
        Character_Locust_Hunter = 0x01C9,
        Character_Theron_Guard = 0x01CA,
        Character_Locust_Spotter = 0x01CB,
        Character_Flame_Grenadier = 0x01CC,
        Character_Locust_Grenadier = 0x01CD,
        Character_Locust_Hunder_Elite = 0x01CE,
        Character_Golden_Miner = 0x01CF,
        Character_Locust_Golden_Hunter = 0x01D0,
        Character_Locust_Sniper = 0x01D1,
        Character_Locust_Kantus = 0x01D2,
        Character_Savage_Theron_Guard = 0x01D3,
        Character_COG_Gear = 0x01D4,
        Character_Samantha_Byrne = 0x01D5,
        Character_Dizzy_Wallin = 0x01D6,
        Character_Jace_Stratton = 0x01D7,
        Character_Clayton_Carmine = 0x01D8,
        Character_Classic_Dom = 0x01D9,
        Character_Classic_Cole = 0x01DA,
        Character_Classic_Baird = 0x01DB,
        Character_Benjamin_Carmine = 0x01DC,
        Character_Civilian_Anya = 0x01DD,
        Character_Colonel_Hoffman = 0x01DE,
        Character_Anthony_Carmine = 0x01DF,
        Character_Classic_Marcus = 0x01E0,
        Character_Superstar_Cole = 0x01E1,
        Character_Golden_Gear = 0x01E2,
        Character_Chairman_Prescott = 0x01E4,
        Character_Unarmored_Marcus = 0x01E5,
        Character_Aaron_Griffin = 0x01E6,

        Mutator_Instagib_Melee = 0x01E7,
        Mutator_Super_Reload = 0x01E8,
        Mutator_Infinite_Ammo = 0x01E9,
        Mutator_Big_Explosions = 0x01EA,
        Mutator_Enemy_Regeneration = 0x01EB,
        Mutator_Vampire = 0x01EC,
        Mutator_Must_Active_Reload = 0x01ED,
        Mutator_Friendly_Fire = 0x01EE,
        Mutator_Big_Head = 0x01EF,
        Mutator_Pinata = 0x01F0,
        Mutator_Flower_Blood = 0x01F1,
        Mutator_Comet = 0x01F2,
        Mutator_Laugh_Track = 0x01F3,

        Avatar_Award_Marcus_Doo_Rag = 0x01F4, // Marcus's Doo-Rag
        Avatar_Award_Locust_Drone_Mask = 0x01F5,
        Avatar_Award_Horde_Shirt = 0x01F6,

        Experience = 0x020F,
        Rank = 0x0210,

        Total_Score_Versus = 0x0211,
        High_Score_Horde = 0x0212,

        Kills_Campaign = 0x0214,
        Kills_Versus = 0x0215,
        Kills_Horde = 0x0216,
        Kills_Beast = 0x0217,

        Revives_Campaign = 0x0218,
        Revives_Versus = 0x0219,
        Revives_Horde = 0x021A,
        Revives_Beast = 0x021B,

        Downs_Campaign = 0x021C,
        Downs_Versus = 0x021D,
        Downs_Horde = 0x021E,
        Downs_Beast = 0x021F,

        Deaths_Campaign = 0x0220,
        Deaths_Versus = 0x0221,
        Deaths_Horde = 0x0222,
        Deaths_Beast = 0x0223,

        Assists_Campaign = 0x0224,
        Assists_Versus = 0x0225,
        Assists_Horde = 0x0226,
        Assists_Beast = 0x0227,

        Headshots_Campaign = 0x0228,
        Headshots_Versus = 0x0229,
        Headshots_Horde = 0x022A,
        Headshots_Beast = 0x022B,

        Holding_Captive_Kills_Campaign = 0x022C,
        Holding_Captive_Kills_Versus = 0x022D,
        Holding_Captive_Kills_Horde = 0x022E,


        Players_Spotted_Campaign = 0x0237,
        Players_Spotted_Versus = 0x0238,
        Players_Spotted_Horde = 0x0239,
        Players_Spotted_Beast = 0x023A,

        Shotgun_Kills_Campaign = 0x023B,
        Shotgun_Kills_Versus = 0x023C,
        Shotgun_Kills_Horde = 0x023D,
        Shotgun_Kills_Beast = 0x023E,

        Heavy_Weapon_Kills_Campaign = 0x023F,
        Heavy_Weapon_Kills_Versus = 0x0240,
        Heavy_Weapon_Kills_Horde = 0x0241,

        Pistol_Kills_Campaign = 0x0242,
        Pistol_Kills_Versus = 0x0243,
        Pistol_Kills_Horde = 0x0244,
        Pistol_Kills_Beast = 0x0245,

        Rifle_Kills_Campaign = 0x0246,
        Rifle_Kills_Versus = 0x0247,
        Rifle_Kills_Horde = 0x0248,
        Rifle_Kills_Beast = 0x0249,

        Picked_Up_Weapon_Kills_Campaign = 0x024A,
        Picked_Up_Weapon_Kills_Versus = 0x024B,
        Picked_Up_Weapon_Kills_Horde = 0x024C,

        Melee_Knockdowns_Campaign = 0x024D,
        Melee_Knockdowns_Versus = 0x024E,
        Melee_Knockdowns_Horde = 0x024F,
        Melee_Knockdowns_Beast = 0x0250,



        Versus_Matches_Played = 0x0258,
        Versus_Matches_Won = 0x0259,
        Versus_Matches_Lost = 0x025A,
        Versus_Match_MVPs = 0x025B,

        Versus_Rounds_Won = 0x025C,
        Versus_Rounds_Lost = 0x025D,
        Versus_Rounds_Drawn = 0x025E,
        Versus_Round_MVPs = 0x025F,

        Versus_CTL_Matches_Played = 0x0260,
        Versus_CTL_Matches_Won = 0x0261,
        Versus_CTL_Matches_Lost = 0x0262,
        Versus_CTL_Leader_Captures = 0x0263,
        Versus_CTL_Leader_Rescues = 0x0264,
        Versus_CTL_Rounds_Won_As_Leader = 0x0265,
        Versus_CTL_Rounds_Lost_As_Leader = 0x0266,
        Versus_CTL_Rounds_Kills_As_Leader = 0x0267,

        Versus_KOTH_Matches_Played = 0x0268,
        Versus_KOTH_Matches_Won = 0x0269,
        Versus_KOTH_Matches_Lost = 0x026A,
        Versus_KOTH_Ring_Captures = 0x026B,
        Versus_KOTH_Ring_Breaks = 0x026C,

        Versus_Warzone_Matches_Played = 0x026D,
        Versus_Warzone_Matches_Won = 0x026E,
        Versus_Warzone_Matches_Lost = 0x026F,

        Versus_Execution_Matches_Played = 0x0270,
        Versus_Execution_Matches_Won = 0x0271,
        Versus_Execution_Matches_Lost = 0x0272,

        Versus_TDM_Matches_Played = 0x0273,
        Versus_TDM_Matches_Won = 0x0274,
        Versus_TDM_Matches_Lost = 0x0275,

        Versus_Wingman_Matches_Played = 0x0276,
        Versus_Wingman_Matches_Won = 0x0277,
        Versus_Wingman_Matches_Lost = 0x0278,

        Horde_Matches_Won = 0x0279,
        Horde_Waves_Won = 0x027A,
        Horde_Waves_Lost = 0x027B,

        Beast_Matches_Won = 0x027C,
        Beast_Waves_Won = 0x027D,
        Beast_Waves_Lost = 0x027E,

        //?
        Versus_Special_Events_Played = 0x0282,

        Executions_Campaign = 0x0283,
        Executions_Versus = 0x0284,
        Executions_Horde = 0x0285,
        Executions_Beast = 0x0286,

        Curb_Stomp_Executions_Campaign = 0x0288,
        Curb_Stomp_Executions_Versus = 0x0289,
        Curb_Stomp_Executions_Horde = 0x028A,
        Curb_Stomp_Executions_Beast = 0x028B,

        Face_Punch_Executions_Campaign = 0x028C,
        Face_Punch_Executions_Versus = 0x028D,
        Face_Punch_Executions_Horde = 0x028E,
        Face_Punch_Executions_Beast = 0x028F,


        Shield_Bash_Executions_Campaign = 0x0294,
        Shield_Bash_Executions_Versus = 0x0295,
        Shield_Bash_Executions_Horde = 0x0296,
        Shield_Bash_Executions_Beast = 0x0297,

        Gnasher_Shotgun_Executions_Campaign = 0x0298,
        Gnasher_Shotgun_Executions_Versus = 0x0299,
        Gnasher_Shotgun_Executions_Horde = 0x029A,
        Gnasher_Shotgun_Executions_Beast = 0x029B,

        Boltok_Pistol_Executions_Campaign = 0x029C,
        Boltok_Pistol_Executions_Versus = 0x029D,
        Boltok_Pistol_Executions_Horde = 0x029E,
        Boltok_Pistol_Executions_Beast = 0x029F,

        Torque_Bow_Executions_Campaign = 0x02A0,
        Torque_Bow_Executions_Versus = 0x02A1,
        Torque_Bow_Executions_Horde = 0x02A2,
        Torque_Bow_Executions_Beast = 0x02A3,

        Longshot_Executions_Campaign = 0x02A4,
        Longshot_Executions_Versus = 0x02A5,
        Longshot_Executions_Horde = 0x02A6,
        Longshot_Executions_Beast = 0x02A7,

        Gorgon_Pistol_Executions_Campaign = 0x02A8,
        Gorgon_Pistol_Executions_Versus = 0x02A9,
        Gorgon_Pistol_Executions_Horde = 0x02AA,
        Gorgon_Pistol_Executions_Beast = 0x02AB,

        Boomshot_Executions_Campaign = 0x02AC,
        Boomshot_Executions_Versus = 0x02AD,
        Boomshot_Executions_Horde = 0x02AE,
        Boomshot_Executions_Beast = 0x02AF,

        Digger_Launcher_Executions_Campaign = 0x02B0,
        Digger_Launcher_Executions_Versus = 0x02B1,
        Digger_Launcher_Executions_Horde = 0x02B2,
        Digger_Launcher_Executions_Beast = 0x02B3,

        OneShot_Executions_Campaign = 0x02B4,
        OneShot_Executions_Versus = 0x02B5,
        OneShot_Executions_Horde = 0x02B6,
        OneShot_Executions_Beast = 0x02B7,

        Scorcher_Executions_Campaign = 0x02B8,
        Scorcher_Executions_Versus = 0x02B9,
        Scorcher_Executions_Horde = 0x02BA,
        Scorcher_Executions_Beast = 0x02BB,

        Mortar_Executions_Campaign = 0x02BC,
        Mortar_Executions_Versus = 0x02BD,
        Mortar_Executions_Horde = 0x02BE,
        Mortar_Executions_Beast = 0x02BF,


        Retro_Lancer_Executions_Campaign = 0x02C0,
        Retro_Lancer_Executions_Versus = 0x02C1,
        Retro_Lancer_Executions_Horde = 0x02C2,
        Retro_Lancer_Executions_Beast = 0x02C3,

        SawedOff_Shotgun_Executions_Campaign = 0x02C4,
        SawedOff_Shotgun_Executions_Versus = 0x02C5,
        SawedOff_Shotgun_Executions_Horde = 0x02C6,
        SawedOff_Shotgun_Executions_Beast = 0x02C7,


        Arm_Rip_Executions_Campaign = 0x02CC,
        Arm_Rip_Executions_Versus = 0x02CD,
        Arm_Rip_Executions_Horde = 0x02CE,
        Arm_Rip_Executions_Beast = 0x02CF,

        Lancer_Executions_Campaign = 0x02D0,
        Lancer_Executions_Versus = 0x02D1,
        Lancer_Executions_Horde = 0x02D2,
        Lancer_Executions_Beast = 0x02D3,

        Hammerburst_Executions_Campaign = 0x02D4,
        Hammerburst_Executions_Versus = 0x02D5,
        Hammerburst_Executions_Horde = 0x02D6,
        Hammerburst_Executions_Beast = 0x02D7,

        Mulcher_Executions_Campaign = 0x02D8,
        Mulcher_Executions_Versus = 0x02D9,
        Mulcher_Executions_Horde = 0x02DA,
        Mulcher_Executions_Beast = 0x02DB,

        Cleaver_Executions_Campaign = 0x02DC,
        Cleaver_Executions_Versus = 0x02DD,
        Cleaver_Executions_Horde = 0x02DE,
        Cleaver_Executions_Beast = 0x02DF,

        Hammer_of_Dawn_Executions_Campaign = 0x02E0,
        Hammer_of_Dawn_Executions_Versus = 0x02E1,
        Hammer_of_Dawn_Executions_Horde = 0x02E2,
        Hammer_of_Dawn_Executions_Beast = 0x02E3,

        Grenade_Sapped_Versus = 0x02E4,

        Grenade_Kills_Campaign = 0x02E5,
        Grenade_Kills_Versus = 0x02E6,
        Grenade_Kills_Horde = 0x02E7,
        Grenade_Kills_Beast = 0x02E8,

        Frag_Grenade_Kills_Campaign = 0x02E9,
        Frag_Grenade_Kills_Versus = 0x02EA,
        Frag_Grenade_Kills_Horde = 0x02EB,

        Incendiary_Grenade_Kills_Campaign = 0x02EC,
        Incendiary_Grenade_Kills_Versus = 0x02ED,
        Incendiary_Grenade_Kills_Horde = 0x02EE,

        Ink_Grenade_Kills_Campaign = 0x02EF,
        Ink_Grenade_Kills_Versus = 0x02F0,
        Ink_Grenade_Kills_Horde = 0x02F1,


        Frag_Grenade_Tag_Kills_Campaign = 0x0304,
        Frag_Grenade_Tag_Kills_Versus = 0x0305,
        Frag_Grenade_Tag_Kills_Horde = 0x0306,

        Incendiary_Grenade_Tag_Kills_Campaign = 0x0307,
        Incendiary_Grenade_Tag_Kills_Versus = 0x0308,
        Incendiary_Grenade_Tag_Kills_Horde = 0x0309,

        Ink_Grenade_Tag_Kills_Campaign = 0x030A,
        Ink_Grenade_Tag_Kills_Versus = 0x030B,
        Ink_Grenade_Tag_Kills_Horde = 0x030C,

        Frag_Grenade_Plant_Kills_Campaign = 0x030D,
        Frag_Grenade_Plant_Kills_Versus = 0x030E,
        Frag_Grenade_Plant_Kills_Horde = 0x030F,

        Incendiary_Grenade_Plant_Kills_Campaign = 0x0310,
        Incendiary_Grenade_Plant_Kills_Versus = 0x0311,
        Incendiary_Grenade_Plant_Kills_Horde = 0x0312,

        Ink_Grenade_Plant_Kills_Campaign = 0x0313,
        Ink_Grenade_Plant_Kills_Versus = 0x0314,
        Ink_Grenade_Plant_Kills_Horde = 0x0315,

        Frag_Grenade_Martyr_Kills_Campaign = 0x0316,
        Frag_Grenade_Martyr_Kills_Versus = 0x0317,
        Frag_Grenade_Martyr_Kills_Horde = 0x0318,

        Incendiary_Grenade_Martyr_Kills_Campaign = 0x0319,
        Incendiary_Grenade_Martyr_Kills_Versus = 0x031A,
        Incendiary_Grenade_Martyr_Kills_Horde = 0x031B,

        Ink_Grenade_Martyr_Kills_Campaign = 0x031C,
        Ink_Grenade_Martyr_Kills_Versus = 0x031D,
        Ink_Grenade_Martyr_Kills_Horde = 0x031E,

        Active_Reload_Attempts_Campaign = 0x031F,
        Active_Reload_Attempts_Versus = 0x0320,
        Active_Reload_Attempts_Horde = 0x0321,
        Active_Reload_Attempts_Beast = 0x0322,


        Perfect_Active_Reloads_Campaign = 0x0327,
        Perfect_Active_Reloads_Versus = 0x0328,
        Perfect_Active_Reloads_Horde = 0x0329,
        Perfect_Active_Reloads_Beast = 0x032A,



        Lancer_Rifle_Kills_Campaign = 0x032B,
        Lancer_Rifle_Kills_Versus = 0x032C,
        Lancer_Rifle_Kills_Horde = 0x032D,

        Hammerburst_Rifle_Kills_Campaign = 0x032E,
        Hammerburst_Rifle_Kills_Versus = 0x032F,
        Hammerburst_Rifle_Kills_Horde = 0x0330,

        Snub_Pistol_Kills_Campaign = 0x0331,
        Snub_Pistol_Kills_Versus = 0x0332,
        Snub_Pistol_Kills_Horde = 0x0333,



        Boomshot_Kills_Campaign = 0x0337,
        Boomshot_Kills_Versus = 0x0338,
        Boomshot_Kills_Horde = 0x0339,

        Scorcher_Kills_Campaign = 0x033A,
        Scorcher_Kills_Versus = 0x033B,
        Scorcher_Kills_Horde = 0x033C,

        Torque_Bow_Kills_Campaign = 0x033D,
        Torque_Bow_Kills_Versus = 0x033E,
        Torque_Bow_Kills_Horde = 0x033F,

        Cleaver_Kills_Campaign = 0x0340,
        Cleaver_Kills_Versus = 0x0341,
        Cleaver_Kills_Horde = 0x0342,

        Digger_Launcher_Kills_Campaign = 0x0343,
        Digger_Launcher_Kills_Versus = 0x0344,
        Digger_Launcher_Kills_Horde = 0x0345,

        Vulcan_Minigun_Kills_Campaign = 0x0346,
        Vulcan_Minigun_Kills_Versus = 0x0347,
        Vulcan_Minigun_Kills_Horde = 0x0348,

        Mulcher_Kills_Campaign = 0x0349,
        Mulcher_Kills_Versus = 0x034A,
        Mulcher_Kills_Horde = 0x034B,

        Mortar_Kills_Campaign = 0x034C,
        Mortar_Kills_Versus = 0x034D,
        Mortar_Kills_Horde = 0x034E,

        Longshot_Kills_Campaign = 0x034F,
        Longshot_Kills_Versus = 0x0350,
        Longshot_Kills_Horde = 0x0351,

        OneShot_Kills_Campaign = 0x0352,
        OneShot_Kills_Versus = 0x0353,
        OneShot_Kills_Horde = 0x0354,

        Hammer_of_Dawn_Kills_Campaign = 0x0355,
        Hammer_of_Dawn_Kills_Versus = 0x0356,
        Hammer_of_Dawn_Kills_Horde = 0x0357,

        Gorgon_Pistol_Kills_Campaign = 0x0358,
        Gorgon_Pistol_Kills_Versus = 0x0359,
        Gorgon_Pistol_Kills_Horde = 0x035A,

        Boltok_Pistol_Kills_Campaign = 0x035B,
        Boltok_Pistol_Kills_Versus = 0x035C,
        Boltok_Pistol_Kills_Horde = 0x035D,

        Retro_Lancer_Kills_Campaign = 0x035E,
        Retro_Lancer_Kills_Versus = 0x035F,
        Retro_Lancer_Kills_Horde = 0x0360,

        Gnasher_Shotgun_Kills_Campaign = 0x0361,
        Gnasher_Shotgun_Kills_Versus = 0x0362,
        Gnasher_Shotgun_Kills_Horde = 0x0363,

        SawedOff_Shotgun_Kills_Campaign = 0x0364,
        SawedOff_Shotgun_Kills_Versus = 0x0365,
        SawedOff_Shotgun_Kills_Horde = 0x0366,

        Ripper_Kills_Campaign = 0x0367,
        Ripper_Kills_Versus = 0x0368,
        Ripper_Kills_Horde = 0x0369,


        Lancer_Perfect_Active_Reloads_Campaign = 0x03E8,
        Lancer_Perfect_Active_Reloads_Versus = 0x03E9,
        Lancer_Perfect_Active_Reloads_Horde = 0x03EA,

        Hammerburst_Perfect_Active_Reloads_Campaign = 0x03EB,
        Hammerburst_Perfect_Active_Reloads_Versus = 0x03EC,
        Hammerburst_Perfect_Active_Reloads_Horde = 0x03ED,

        Snub_Pistol_Perfect_Active_Reloads_Campaign = 0x03EE,
        Snub_Pistol_Perfect_Active_Reloads_Versus = 0x03EF,
        Snub_Pistol_Perfect_Active_Reloads_Horde = 0x03F0,


        Boomshot_Perfect_Active_Reloads_Campaign = 0x03F4,
        Boomshot_Perfect_Active_Reloads_Versus = 0x03F5,
        Boomshot_Perfect_Active_Reloads_Horde = 0x03F6,

        Scorcher_Perfect_Active_Reloads_Campaign = 0x03F7,
        Scorcher_Perfect_Active_Reloads_Versus = 0x03F8,
        Scorcher_Perfect_Active_Reloads_Horde = 0x03F9,

        Torque_Bow_Perfect_Active_Reloads_Campaign = 0x03FA,
        Torque_Bow_Perfect_Active_Reloads_Versus = 0x03FB,
        Torque_Bow_Perfect_Active_Reloads_Horde = 0x03FC,

        Cleaver_Perfect_Active_Reloads_Campaign = 0x03FD,
        Cleaver_Perfect_Active_Reloads_Versus = 0x03FE,
        Cleaver_Perfect_Active_Reloads_Horde = 0x03FF,

        Digger_Launcher_Perfect_Active_Reloads_Campaign = 0x0400,
        Digger_Launcher_Perfect_Active_Reloads_Versus = 0x0401,
        Digger_Launcher_Perfect_Active_Reloads_Horde = 0x0402,

        Vulcan_Minigun_Perfect_Active_Reloads_Campaign = 0x0403,
        Vulcan_Minigun_Perfect_Active_Reloads_Versus = 0x0404,
        Vulcan_Minigun_Perfect_Active_Reloads_Horde = 0x0405,

        Mulcher_Perfect_Active_Reloads_Campaign = 0x0406,
        Mulcher_Perfect_Active_Reloads_Versus = 0x0407,
        Mulcher_Perfect_Active_Reloads_Horde = 0x0408,

        Mortar_Perfect_Active_Reloads_Campaign = 0x0409,
        Mortar_Perfect_Active_Reloads_Versus = 0x040A,
        Mortar_Perfect_Active_Reloads_Horde = 0x040B,

        Longshot_Perfect_Active_Reloads_Campaign = 0x040C,
        Longshot_Perfect_Active_Reloads_Versus = 0x040D,
        Longshot_Perfect_Active_Reloads_Horde = 0x040E,

        OneShot_Perfect_Active_Reloads_Campaign = 0x040F,
        OneShot_Perfect_Active_Reloads_Versus = 0x0410,
        OneShot_Perfect_Active_Reloads_Horde = 0x0411,

        Hammer_of_Dawn_Perfect_Active_Reloads_Campaign = 0x0412,
        Hammer_of_Dawn_Perfect_Active_Reloads_Versus = 0x0413,
        Hammer_of_Dawn_Perfect_Active_Reloads_Horde = 0x0414,

        Gorgon_Pistol_Perfect_Active_Reloads_Campaign = 0x0415,
        Gorgon_Pistol_Perfect_Active_Reloads_Versus = 0x0416,
        Gorgon_Pistol_Perfect_Active_Reloads_Horde = 0x0417,

        Boltok_Pistol_Perfect_Active_Reloads_Campaign = 0x0418,
        Boltok_Pistol_Perfect_Active_Reloads_Versus = 0x0419,
        Boltok_Pistol_Perfect_Active_Reloads_Horde = 0x041A,

        Retro_Lancer_Perfect_Active_Reloads_Campaign = 0x041B,
        Retro_Lancer_Perfect_Active_Reloads_Versus = 0x041C,
        Retro_Lancer_Perfect_Active_Reloads_Horde = 0x041D,

        Gnasher_Shotgun_Perfect_Active_Reloads_Campaign = 0x041E,
        Gnasher_Shotgun_Perfect_Active_Reloads_Versus = 0x041F,
        Gnasher_Shotgun_Perfect_Active_Reloads_Horde = 0x0420,

        SawedOff_Shotgun_Perfect_Active_Reloads_Campaign = 0x0421,
        SawedOff_Shotgun_Perfect_Active_Reloads_Versus = 0x0422,
        SawedOff_Shotgun_Perfect_Active_Reloads_Horde = 0x0423,

        Ripper_Perfect_Active_Reloads_Campaign = 0x0424,
        Ripper_Perfect_Active_Reloads_Versus = 0x0425,
        Ripper_Perfect_Active_Reloads_Horde = 0x0426,


        Lancer_Deaths_Holding_Campaign = 0x04A5,
        Lancer_Deaths_Holding_Versus = 0x04A6,
        Lancer_Deaths_Holding_Horde = 0x04A7,

        Hammerburst_Deaths_Holding_Campaign = 0x04A8,
        Hammerburst_Deaths_Holding_Versus = 0x04A9,
        Hammerburst_Deaths_Holding_Horde = 0x04AA,

        Snub_Pistol_Deaths_Holding_Campaign = 0x04AB,
        Snub_Pistol_Deaths_Holding_Versus = 0x04AC,
        Snub_Pistol_Deaths_Holding_Horde = 0x04AD,


        Boomshot_Deaths_Holding_Campaign = 0x04B1,
        Boomshot_Deaths_Holding_Versus = 0x04B2,
        Boomshot_Deaths_Holding_Horde = 0x04B3,

        Scorcher_Deaths_Holding_Campaign = 0x04B4,
        Scorcher_Deaths_Holding_Versus = 0x04B5,
        Scorcher_Deaths_Holding_Horde = 0x04B6,

        Torque_Bow_Deaths_Holding_Campaign = 0x04B7,
        Torque_Bow_Deaths_Holding_Versus = 0x04B8,
        Torque_Bow_Deaths_Holding_Horde = 0x04B9,

        Cleaver_Deaths_Holding_Campaign = 0x04BA,
        Cleaver_Deaths_Holding_Versus = 0x04BB,
        Cleaver_Deaths_Holding_Horde = 0x04BC,

        Digger_Launcher_Deaths_Holding_Campaign = 0x04BD,
        Digger_Launcher_Deaths_Holding_Versus = 0x04BE,
        Digger_Launcher_Deaths_Holding_Horde = 0x04BF,

        Vulcan_Minigun_Deaths_Holding_Campaign = 0x04C0,
        Vulcan_Minigun_Deaths_Holding_Versus = 0x04C1,
        Vulcan_Minigun_Deaths_Holding_Horde = 0x04C2,

        Mulcher_Deaths_Holding_Campaign = 0x04C3,
        Mulcher_Deaths_Holding_Versus = 0x04C4,
        Mulcher_Deaths_Holding_Horde = 0x04C5,

        Mortar_Deaths_Holding_Campaign = 0x04C6,
        Mortar_Deaths_Holding_Versus = 0x04C7,
        Mortar_Deaths_Holding_Horde = 0x04C8,

        Longshot_Deaths_Holding_Campaign = 0x04C9,
        Longshot_Deaths_Holding_Versus = 0x04CA,
        Longshot_Deaths_Holding_Horde = 0x04CB,

        OneShot_Deaths_Holding_Campaign = 0x04CC,
        OneShot_Deaths_Holding_Versus = 0x04CD,
        OneShot_Deaths_Holding_Horde = 0x04CE,

        Hammer_of_Dawn_Deaths_Holding_Campaign = 0x04CF,
        Hammer_of_Dawn_Deaths_Holding_Versus = 0x04D0,
        Hammer_of_Dawn_Deaths_Holding_Horde = 0x04D1,

        Gorgon_Pistol_Deaths_Holding_Campaign = 0x04D2,
        Gorgon_Pistol_Deaths_Holding_Versus = 0x04D3,
        Gorgon_Pistol_Deaths_Holding_Horde = 0x04D4,

        Boltok_Pistol_Deaths_Holding_Campaign = 0x04D5,
        Boltok_Pistol_Deaths_Holding_Versus = 0x04D6,
        Boltok_Pistol_Deaths_Holding_Horde = 0x04D7,

        Retro_Lancer_Deaths_Holding_Campaign = 0x04D8,
        Retro_Lancer_Deaths_Holding_Versus = 0x04D9,
        Retro_Lancer_Deaths_Holding_Horde = 0x04DA,

        Gnasher_Shotugn_Deaths_Holding_Campaign = 0x04DB,
        Gnasher_Shotugn_Deaths_Holding_Versus = 0x04DC,
        Gnasher_Shotugn_Deaths_Holding_Horde = 0x04DD,

        SawedOff_Shotugn_Deaths_Holding_Campaign = 0x04DE,
        SawedOff_Shotugn_Deaths_Holding_Versus = 0x04DF,
        SawedOff_Shotugn_Deaths_Holding_Horde = 0x04E0,

        Ripper_Deaths_Holding_Campaign = 0x04E1,
        Ripper_Deaths_Holding_Versus = 0x04E2,
        Ripper_Deaths_Holding_Horde = 0x04E3,

        Lancer_Rifle_Downs_Campaign = 0x04E4,
        Lancer_Rifle_Downs_Versus = 0x04E5,
        Lancer_Rifle_Downs_Horde = 0x04E6,

        Hammerburst_Rifle_Downs_Campaign = 0x04E7,
        Hammerburst_Rifle_Downs_Versus = 0x04E8,
        Hammerburst_Rifle_Downs_Horde = 0x04E9,

        Snub_Pistol_Downs_Campaign = 0x04EA,
        Snub_Pistol_Downs_Versus = 0x04EB,
        Snub_Pistol_Downs_Horde = 0x04EC,

        Retro_Lancer_Downs_Campaign = 0x04ED,
        Retro_Lancer_Downs_Versus = 0x04EE,
        Retro_Lancer_Downs_Horde = 0x04EF,

        Gnasher_Shotugn_Downs_Campaign = 0x04F0,
        Gnasher_Shotugn_Downs_Versus = 0x04F1,
        Gnasher_Shotugn_Downs_Horde = 0x04F2,

        SawedOff_Shotugn_Downs_Campaign = 0x04F3,
        SawedOff_Shotugn_Downs_Versus = 0x04F4,
        SawedOff_Shotugn_Downs_Horde = 0x04F5,

        Melee_Kills_Campaign = 0x04F6,
        Melee_Kills_Versus = 0x04F7,
        Melee_Kills_Horde = 0x04F8,
        Melee_Kills_Beast = 0x04F9,

        Lancer_Chainsaw_Kills_Campaign = 0x04FA,
        Lancer_Chainsaw_Kills_Versus = 0x04FB,
        Lancer_Chainsaw_Kills_Horde = 0x04FC,
        Lancer_Chainsaw_Kills_Beast = 0x04FD,

        Retro_Bayonet_Kills_Campaign = 0x04FE,
        Retro_Bayonet_Kills_Versus = 0x04FF,
        Retro_Bayonet_Kills_Horde = 0x0500,
        Retro_Bayonet_Kills_Beast = 0x0501,

        Turret_Kills_Campaign = 0x0502,
        Turret_Kills_Versus = 0x0503,
        Turret_Kills_Horde = 0x0504,
        Turret_Kills_Beast = 0x0505,

        Silverback_Minigun_Kills_Campaign = 0x0506,
        Silverback_Minigun_Kills_Versus = 0x0507,
        Silverback_Minigun_Kills_Horde = 0x0508,
        Silverback_Minigun_Kills_Beast = 0x0509,

        Silverback_Rocket_Kills_Campaign = 0x050A,
        Silverback_Rocket_Kills_Versus = 0x050B,
        Silverback_Rocket_Kills_Horde = 0x050C,
        Silverback_Rocket_Kills_Beast = 0x050D,

        Holding_Shield_Kills_Campaign = 0x050E,
        Holding_Shield_Kills_Versus = 0x050F,
        Holding_Shield_Kills_Horde = 0x0510,
        Holding_Shield_Kills_Beast = 0x0511,



        Bloodmount_Kills_Campaign = 0x051A,
        Bloodmount_Kills_Horde = 0x051B,

        Wretch_Kills_Campaign = 0x051C,
        Wretch_Kills_Horde = 0x051D,

        Drone_Kills_Campaign = 0x051E,
        Drone_Kills_Horde = 0x051F,

        Sniper_Kills_Campaign = 0x0520,
        Sniper_Kills_Horde = 0x0521,

        Wild_Ticker_Kills_Campaign = 0x0522,
        Wild_Ticker_Kills_Horde = 0x0523,

        Ticker_Kills_Campaign = 0x0524,
        Ticker_Kills_Horde = 0x0525,

        Lambent_Gunker_Kills_Campaign = 0x0526,
        Lambent_Gunker_Kills_Horde = 0x0527,

        Grenadier_Kills_Campaign = 0x0528,
        Grenadier_Kills_Horde = 0x0529,

        Theron_Guard_Kills_Campaign = 0x052A,
        Theron_Guard_Kills_Horde = 0x052B,

        Kantus_Kills_Campaign = 0x052C,
        Kantus_Kills_Horde = 0x052D,

        Armored_Kantus_Kills_Campaign = 0x052E,
        Armored_Kantus_Kills_Horde = 0x052F,

        Boomer_Kills_Campaign = 0x0530,
        Boomer_Kills_Horde = 0x0531,

        Flame_Boomer_Kills_Campaign = 0x0532, // not in game
        Flame_Boomer_Kills_Horde = 0x0533,

        Mauler_Kills_Campaign = 0x0534,
        Mauler_Kills_Horde = 0x0535,

        Grinder_Kills_Campaign = 0x0536,
        Grinder_Kills_Horde = 0x0537,

        Butcher_Kills_Campaign = 0x0538,
        Butcher_Kills_Horde = 0x0539,

        Berserker_Kills_Campaign = 0x053A,
        Berserker_Kills_Horde = 0x053B,

        Lambent_Zerker_Kills_Campaign = 0x053C,
        Lambent_Zerker_Kills_Horde = 0x053D,

        Corpser_Hatchling_Kills_Campaign = 0x053E,
        Corpser_Hatchling_Kills_Horde = 0x053F,

        Savage_Corpser_Kills_Campaign = 0x0540,
        Savage_Corpser_Kills_Horde = 0x0541,

        Savage_Drone_Kills_Campaign = 0x0542,
        Savage_Drone_Kills_Horde = 0x0543,

        Savage_Boomer_Kills_Campaign = 0x0544,
        Savage_Boomer_Kills_Horde = 0x0545,

        Savage_Grenadier_Kills_Campaign = 0x0546,
        Savage_Grenadier_Kills_Horde = 0x0547,

        Savage_Theron_Guard_Kills_Campaign = 0x0548,
        Savage_Theron_Guard_Kills_Horde = 0x0549,

        Giant_Serapede_Kills_Campaign = 0x054A,
        Giant_Serapede_Kills_Horde = 0x054B,

        Lambent_Polyp_Kills_Campaign = 0x054C,
        Lambent_Polyp_Kills_Horde = 0x054D,

        Lambent_Wretch_Kills_Campaign = 0x054E,
        Lambent_Wretch_Kills_Horde = 0x054F,

        Former_Kills_Campaign = 0x0550,
        Former_Kills_Horde = 0x0551,

        Lambent_Drone_Kills_Campaign = 0x0552,
        Lambent_Drone_Kills_Horde = 0x0553,

        Lambent_Drudge_Kills_Campaign = 0x0554,
        Lambent_Drudge_Kills_Horde = 0x0555,

        Drudge_Headsnake_Kills_Campaign = 0x0556,
        Drudge_Headsnake_Kills_Horde = 0x0557,

        Reaver_Kills_Campaign = 0x0558,
        Reaver_Kills_Horde = 0x0559,

        Shrieker_Kills_Campaign = 0x055A,
        Shrieker_Kills_Horde = 0x055B,

        Brumak_Kills_Campaign = 0x055C,
        Brumak_Kills_Horde = 0x055D,

        Palace_Guard_Kills_Campaign = 0x055E,
        Palace_Guard_Kills_Horde = 0x055F,

        Campaign_Arcade_Chapters_Played = 0x0563,
        Campaign_Total_Score = 0x0563,

        Campaign_Cinematic_Chapters_Played = 0x0566,

        Stranded_Kills_Beast = 0x0569,
        COG_Gear_Kills_Beast = 0x056A,
        Onyx_Guard_Kills_Beast = 0x056B,
        Aaron_Griffin_Kills_Beast = 0x056C,
        Dizzy_Wallin_Kills_Beast = 0x056D,
        Jace_Stratton_Kills_Beast = 0x056E,
        Sam_Byrne_Kills_Beast = 0x056F,
        Anya_Stroud_Kills_Beast = 0x0570,
        Clayton_Carmine_Kills_Beast = 0x0571,
        Damon_Kills_Beast = 0x0572,
        Augustus_Cole_Kills_Beast = 0x0573,
        Dom_Santiago_Kills_Beast = 0x0574,
        Marcus_Fenix_Kills_Beast = 0x0575,
        Colonel_Hoffman_Kills_Beast = 0x0576,
        Chairman_Prescott_Kills_Beast = 0x0577,

        Beast_Cash_Earned = 0x0578,
        Beast_Cash_Spent = 0x0579,

        Beast_Wild_Ticker_Kills = 0x057A,
        Beast_Ticker_Kills = 0x057B,
        Beast_Giant_Serapede_Kills = 0x057C,
        Beast_Berserker_Kills = 0x057D,
        Beast_Wretch_Kills = 0x057E,
        Beast_Kantus_Kills = 0x057F,
        Beast_Savage_Corpser_Kills = 0x0580,
        Beast_Armored_Kantus_Kills = 0x0581,
        Beast_Savage_Drone_Kills = 0x0582,
        Beast_Bloodmount_Kills = 0x0583,
        Beast_Savage_Grenadier_Kills = 0x0584,
        Beast_Butcher_Kills = 0x0585,
        Beast_Mauler_Kills = 0x0586,
        Beast_Boomer_Kills = 0x0587,
        Beast_Savage_Boomer_Kills = 0x0588,

        Beast_Wild_Ticker_Spawns = 0x0589,
        Beast_Ticker_Spawns = 0x058A,
        Beast_Giant_Serapede_Spawns = 0x058B,
        Beast_Berserker_Spawns = 0x058C,
        Beast_Wretch_Spawns = 0x058D,
        Beast_Kantus_Spawns = 0x058E,
        Beast_Savage_Corpser_Spawns = 0x058F,
        Beast_Armored_Kantus_Spawns = 0x0590,
        Beast_Savage_Drone_Spawns = 0x0591,
        Beast_Bloodmount_Spawns = 0x0592,
        Beast_Savage_Grenadier_Spawns = 0x0593,
        Beast_Butcher_Spawns = 0x0594,
        Beast_Mauler_Spawns = 0x0595,
        Beast_Boomer_Spawns = 0x0596,
        Beast_Savage_Boomer_Spawns = 0x0597,


        Beast_Barrier_Destroyed = 0x05A8,

        Beast_Decoy_Destroyed = 0x05AA,

        Beast_Sentry_Destroyed = 0x05AC,
        Beast_Silverback_Destroyed = 0x05AD,
        Beast_Turret_Destroyed = 0x05AE,


        Horde_Cash_Earned = 0x05AF,
        Horde_Cash_Spent = 0x05B0,

        Horde_Flag_Investment = 0x05B5,
        Horde_Barrier_Investment = 0x05B9,
        Horde_Command_Center_Investment = 0x05BA,
        Horde_Decoy_Investment = 0x05BB,
        Horde_Sentry_Investment = 0x05BD,
        Horde_Silverback_Investment = 0x05BE,
        Horde_Turret_Investment = 0x05BF,

        Horde_Barrier_Level_1 = 0x05C0,
        Horde_Command_Center_Level_1 = 0x05C1,
        Horde_Decoy_Level_1 = 0x05C2,
        Horde_Sentry_Level_1 = 0x05C4,
        Horde_Silverback_Level_1 = 0x05C5,
        Horde_Turret_Level_1 = 0x05C6,

        Horde_Barrier_Level_2 = 0x05C7,
        Horde_Command_Center_Level_2 = 0x05C8,
        Horde_Decoy_Level_2 = 0x05C9,
        Horde_Sentry_Level_2 = 0x05CB,
        Horde_Silverback_Level_2 = 0x05CC,
        Horde_Turret_Level_2 = 0x05CD,

        Horde_Barrier_Level_3 = 0x05CE,
        Horde_Command_Center_Level_3 = 0x05CF,
        Horde_Decoy_Level_3 = 0x05D0,
        Horde_Sentry_Level_3 = 0x05D2,
        Horde_Silverback_Level_3 = 0x05D3,
        Horde_Turret_Level_3 = 0x05D4,

        Horde_Barrier_Level_4 = 0x05D5,
        Horde_Command_Center_Level_4 = 0x05D6,
        Horde_Decoy_Level_4 = 0x05D7,
        Horde_Sentry_Level_4 = 0x05D9,
        Horde_Silverback_Level_4 = 0x05DA,
        Horde_Turret_Level_4 = 0x05DB,

        Horde_Barrier_Level_5 = 0x05DC,
        Horde_Decoy_Level_5 = 0x05DE,
        Horde_Sentry_Level_5 = 0x05E0,
        Horde_Silverback_Level_5 = 0x05E1,
        Horde_Turret_Level_5 = 0x05E2,

        Horde_Barrier_Level_6 = 0x05E3,
        Horde_Decoy_Level_6 = 0x05E5,
        Horde_Sentry_Level_6 = 0x05E7,
        Horde_Turret_Level_6 = 0x05E9,

        Horde_Barrier_Level_7 = 0x05EA,
        Horde_Decoy_Level_7 = 0x05EC,
        Horde_Sentry_Level_7 = 0x05EE,
        Horde_Turret_Level_7 = 0x05F0,

        Horde_Barrier_Level_8 = 0x05F1,
        Horde_Decoy_Level_8 = 0x05F3,
        Horde_Turret_Level_8 = 0x05F7
    }

    internal enum GameMode : byte
    {
        Null = 0,
        Campaign = 1,
        Versus = 2,
        Horde = 4,
        Beast = 8
    }

    internal class PlayerStorage : List<Entry>
    {
        private ulong onlineXuid;

        internal Entry FromID(EntryID entryId)
        {
            for (int x = 0; x < this.Count; x++)
                if (this[x].ID == entryId)
                    return this[x];
            return null;
        }

        internal int this[EntryID entryId]
        {
            get
            {
                for (int x = 0; x < this.Count; x++)
                    if (this[x].ID == entryId && this[x].DataType == DataType.Int32)
                        return this[x].nData;
                return 0;
            }
            set
            {
                for (int x = 0; x < this.Count; x++)
                {
                    if (this[x].ID == entryId)
                    {
                        this[x].DataType = DataType.Int32;
                        this[x].nData = value;
                        return;
                    }
                }
                this.Add(entryId, value, 2, 0);
            }
        }

        internal void Add(EntryID entryId, int nData, byte u1, byte u2)
        {
            Add(new Entry()
            {
                ID = entryId,
                DataType = DataType.Int32,
                nData = nData,
                Unknown = u1,
                Unknown2 = u2
            });
        }

        private Dictionary<long, byte[]> NullData = new Dictionary<long,byte[]>();
        internal PlayerStorage(EndianIO cIO, ulong onlineXuid, bool packaged)
        {
            this.onlineXuid = onlineXuid;

            EndianIO IO;
            if (packaged)
            {
                byte[] hash = cIO.In.ReadBytes(0x14);
                int decompressedSize = cIO.In.ReadInt32();
                byte[] data = cIO.In.ReadBytes(cIO.Stream.Length - 0x18);
                cIO.Close();

                if (!Global.compareArray(HashData(decompressedSize, data), hash))
                    throw new Exception("PlayerStorage: Header corrupted");

                MemoryStream decompressionStream = new MemoryStream();
                try
                {
                    LZO.LZO1X.Decompress(data, decompressionStream);
                }
                catch
                {
                    decompressionStream.Close();
                    throw new Exception("PlayerStorage: Data corrupted");
                }
                IO = new EndianIO(decompressionStream.ToArray(), EndianType.BigEndian, true);
            }
            else
                IO = cIO;

            int numEntries = IO.In.ReadInt32();
            for (int x = 0; x < numEntries; x++)
            {
                byte unk = IO.In.ReadByte();
                if (unk == 0x00)
                    NullData.Add(IO.Stream.Position - 1, IO.In.ReadBytes(0x06));
                else
                {
                    Entry entry = new Entry();
                    entry.Unknown = unk;
                    entry.ID = (EntryID)IO.In.ReadInt32();
                    entry.DataType = (DataType)IO.In.ReadByte();

                    switch (entry.DataType)
                    {
                        case DataType.Bool:
                            entry.bData = IO.In.ReadBoolean();
                            continue;
                        case DataType.Int32:
                            entry.nData = IO.In.ReadInt32();
                            break;
                        case DataType.Float:
                            entry.fData = IO.In.ReadSingle();
                            break;
                        default:
                            throw new Exception(string.Format("PlayerStorage: Unknown data type [0x{0:X2}]", (byte)entry.DataType));
                    }

                    entry.Unknown2 = IO.In.ReadByte();
                    this.Add(entry);
                }
            }
            IO.Close();
        }

#if INT2
        internal void dumpEntriesToFile(string fileName)
        {
            List<Entry> inOrder = this.ToList();
            inOrder.Sort((e1, e2) => e1.ID == e2.ID ? 0 : (e1.ID > e2.ID ? 1 : -1));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------");
            sb.AppendLine("ID\tValue\t\tName");
            sb.AppendLine("-------------------------------------------");
            foreach (Entry entry in inOrder)
            {
                string valueString;
                if (entry.DataType == DataType.Float)
                    valueString = entry.fData.ToString();
                else if (entry.DataType == DataType.Bool)
                    valueString = entry.bData.ToString();
                else
                    valueString = entry.nData.ToString();

                sb.AppendLine("0x" + ((int)entry.ID).ToString("X4")
                    + "\t" + valueString
                    + "\t" + ((int)entry.ID).ToString()
                    + "\t" + entry.ID.ToString());
            }
            File.WriteAllText(fileName, sb.ToString());
        }
#endif
        internal byte[] ToArray(bool package)
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(this.Count + NullData.Count);
            foreach (Entry entry in this)
            {
                while (NullData.ContainsKey(IO.Stream.Position))
                {
                    IO.Out.Write((byte)0x00);
                    IO.Out.Write(NullData[IO.Stream.Position - 1]);
                }
                IO.Out.Write(entry.Unknown);
                IO.Out.Write((int)entry.ID);
                IO.Out.Write((byte)entry.DataType);

                switch (entry.DataType)
                {
                    case DataType.Int32:
                        IO.Out.Write(entry.nData);
                        break;
                    case DataType.Float:
                        IO.Out.Write(entry.fData);
                        break;
                }

                IO.Out.Write(entry.Unknown2);
            }
            byte[] data = IO.ToArray();
            IO.Close();

            if (!package)
                return data;

            MemoryStream ms = new MemoryStream();
            LZO.LZO1X.Compress(data, (uint)data.Length, ms);

            // Compress data
            byte[] playerData = ms.ToArray();

            IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(HashData(data.Length, playerData));
            IO.Out.Write(data.Length);
            IO.Out.Write(playerData);
            playerData = IO.ToArray();
            IO.Close();

            return playerData;
        }

        private byte[] HashData(int decompressedSize, byte[] data)
        {
            EndianIO hashStream = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            hashStream.Out.Write(onlineXuid);
            hashStream.Out.Write(decompressedSize);
            hashStream.Out.Write(data);
            byte[] hashData = hashStream.ToArray();
            hashStream.Close();
            return hashData.Hash(HashType.SHA1);
        }
    }
}
