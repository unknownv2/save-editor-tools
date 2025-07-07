using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Volition
{
    internal class SRIVSaveData
    {
        internal static List<XboxTableDefinition> Cheats = new List<XboxTableDefinition>
            {
                new XboxTableDefinition
                    {
                        Hash = 0xB5030926,
                        DisplayName = "UNLOCK TELEKINESIS",
                        Description = "Unlocks the Telekinesis super power.",
                        Name = "supertk"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x0D8CA306,
                        DisplayName = "UNLOCK STOMP",
                        Description = "Unlocks the Stomp super power.",
                        Name = "superstomp"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x1C13B488,
                        DisplayName = "UNLOCK BLAST",
                        Description = "Unlocks the Blast super power.",
                        Name = "superblast"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x4EBE00F6,
                        DisplayName = "UNLOCK BUFF",
                        Description = "Unlocks the Buff super power.",
                        Name = "superbuff"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x41850A07,
                        DisplayName = "UNLOCK DFA",
                        Description = "Unlocks the Death From Above super power.",
                        Name = "superdfa"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xF6FF0765,
                        DisplayName = "ALL SUPER UPGRADES",
                        Description = "Unlocks and purchases all Super Power Upgrades.",
                        Name = "dlc_sosuper"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xE3EF7415,
                        DisplayName = "100%% SUPER POWERS",
                        Description = "Maxes out Super Power Strength.",
                        Name = "dlc_superduper"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xF6E9972E,
                        DisplayName = "DISABLE SUPER MOVEMENT",
                        Description = "Disables super powered movement abilities.",
                        Name = "nosupermove"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xB69DB4F0,
                        DisplayName = "DISABLE SUPER POWERS",
                        Description = "Disables elemental super powers.",
                        Name = "nosuperpowers"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x13C7B526,
                        DisplayName = "HEAVEN BOUND",
                        Description = "The dead all go to Heaven.",
                        Name = "fryhole"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x5C0CF060,
                        DisplayName = "GIVE CASH",
                        Description = "Instantly gives you cash.",
                        Name = "cheese"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x02380284,
                        DisplayName = "INFINITE SPRINT",
                        Description = "You can sprint forever.",
                        Name = "runfast"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xC96A70BA,
                        DisplayName = "CLEAR NOTORIETY",
                        Description = "Removes all existing Notoriety.",
                        Name = "goodygoody"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xCC47044D,
                        DisplayName = "VEHICLE SMASH",
                        Description = "Your vehicle smashes other vehicles.",
                        Name = "isquishyou"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x290918B4,
                        DisplayName = "BLOODY MESS",
                        Description = "Everyone you kill explodes.",
                        Name = "notrated"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xD14B51E3,
                        DisplayName = "GOLDEN GUN",
                        Description = "One shot firearm kills.",
                        Name = "goldengun"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xB4F60BFC,
                        DisplayName = "REPAIR CAR",
                        Description = "Repairs all damage to your vehicle.",
                        Name = "repaircar"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x660667C0,
                        DisplayName = "WEAPONS",
                        Description = "Gives you a full suite of weapons.",
                        Name = "letsrock"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x21E3DA75,
                        DisplayName = "GIVE ALIEN HOVERCAR",
                        Description = "Puts this vehicle into your garage.",
                        Name = "givehovercar"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xD197ECC2,
                        DisplayName = "GIVE ALIEN TANK",
                        Description = "Puts this vehicle into your garage.",
                        Name = "givehovertank"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xA71024F4,
                        DisplayName = "GIVE ALIEN UFO",
                        Description = "Puts this vehicle into your garage.",
                        Name = "giveufo"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x9F8C290C,
                        DisplayName = "ALL UPGRADES",
                        Description = "Unlocks and purchases all non-super Upgrades.",
                        Name = "unlockitall"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xEED11580,
                        DisplayName = "SLOW MOTION",
                        Description = "Time slows down.",
                        Name = "slowmo"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x1D8DA3B7,
                        DisplayName = "FAST FORWARD",
                        Description = "Time speeds up.",
                        Name = "fastforward"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xE922FE73,
                        DisplayName = "GIVE ALIEN HOVERBIKE",
                        Description = "Puts this vehicle into your garage.",
                        Name = "givetrouble"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xF69DA10B,
                        DisplayName = "GIVE MONSTER TRUCK",
                        Description = "Puts this vehicle into your garage.",
                        Name = "givemonster"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x17182DE4,
                        DisplayName = "ASCII MODE",
                        Description = "ASCII MODE",
                        Name = "ascii"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x3C3566E4,
                        DisplayName = "NO WARDEN",
                        Description = "Notoriety no longer spawns Wardens.",
                        Name = "nowardens"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xC8241634,
                        DisplayName = "INSTANT WARDEN",
                        Description = "Instantly spawns a Warden.",
                        Name = "instantwarden"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x962EFA3F,
                        DisplayName = "MASCOTS",
                        Description = "All pedestrians are mascots.",
                        Name = "mascot"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x32F5A33C,
                        DisplayName = "PIMPS AND HOS",
                        Description = "All pedestrians are pimps and hos.",
                        Name = "hohoho"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xD97EEF91,
                        DisplayName = "INSANE CITY",
                        Description = "Everyone has gone insane!",
                        Name = "insanecity"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xD0094D18,
                        DisplayName = "EVIL CARS",
                        Description = "All cars try to run you over.",
                        Name = "evilcars"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x779D63A9,
                        DisplayName = "NO GLITCHES",
                        Description = "We fixed the glitch.",
                        Name = "noglitchcity"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xDE5CA1A7,
                        DisplayName = "BIG HEAD MODE",
                        Description = "Big Head Mode.",
                        Name = "bigheadmode"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x24EAF2FF,
                        DisplayName = "CAR MASS HOLE",
                        Description = "Your vehicle has infinite mass and smashes other vehicles out of the way.",
                        Name = "dlc_car_mass"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x6F55B932,
                        DisplayName = "NEVER DIE",
                        Description = "You will never die.",
                        Name = "dlc_never_die"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x9EBE4C56,
                        DisplayName = "INFINITE AMMO",
                        Description = "Gives you infinite ammo for all weapons.",
                        Name = "dlc_unlimited_ammo"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0xDC11261B,
                        DisplayName = "UNLIMITED CLIP",
                        Description = "You never have to reload because your clip runneth over forever.",
                        Name = "dlc_unlimited_clip"
                    },
                new XboxTableDefinition
                    {
                        Hash = 0x25499BE8,
                        DisplayName = "VEHICLE NO DAMAGE",
                        Description = "Your vehicle is immune to damage.",
                        Name = "vroom"
                    }
            };
        static internal Dictionary<uint, SRIVTableData.InventoryItem> InventoryItems = new Dictionary
    <uint, SRIVTableData.InventoryItem>
        {
{ 0x316DEFBC, new SRIVTableData.InventoryItem { Name = "Rifle-NG", DisplayName = "BURST RIFLE", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x11A5E83A, new SRIVTableData.InventoryItem { Name = "Rifle-Gang", DisplayName = "AUTOMATIC RIFLE", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0xC54E0699, new SRIVTableData.InventoryItem { Name = "Special-SniperRifle", DisplayName = "SNIPER RIFLE", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0xAA1127D0, new SRIVTableData.InventoryItem { Name = "Explosive-RocketLauncher", DisplayName = "RPG", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0xDCF52EE9, new SRIVTableData.InventoryItem { Name = "Pistol-Police", DisplayName = "QUICKSHOT PISTOL", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0x24FE9255, new SRIVTableData.InventoryItem { Name = "Shotgun-Gang", DisplayName = "PUMP-ACTION SHOTGUN", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0x3F736485, new SRIVTableData.InventoryItem { Name = "Shotgun-Police-Silence", DisplayName = "SEMI-AUTO SHOTGUN", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0xB20742BC, new SRIVTableData.InventoryItem { Name = "SMG-Gang", DisplayName = "HEAVY SMG", Category = SRIVTableData.InventoryItemCategory.SMG, Cost = 100}},
{ 0x35B6FFCC, new SRIVTableData.InventoryItem { Name = "SMG-Storm", DisplayName = "RAPID-FIRE SMG", Cost = 100, Category = SRIVTableData.InventoryItemCategory.SMG}},
{ 0x21A320A2, new SRIVTableData.InventoryItem { Name = "satchel", DisplayName = "SATCHEL CHARGES", Cost = 10000, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0x519A6525, new SRIVTableData.InventoryItem { Name = "flashbang", DisplayName = "FLASHBANGS", Cost = 25, Category = SRIVTableData.InventoryItemCategory.Grenade}},
{ 0xFC16BE14, new SRIVTableData.InventoryItem { Name = "CyberEnergyBall", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x31F11B9C, new SRIVTableData.InventoryItem { Name = "baseball_bat", DisplayName = "BASEBALL BAT", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x8E7E8459, new SRIVTableData.InventoryItem { Name = "chainsaw", DisplayName = "WOODSMAN", Category = SRIVTableData.InventoryItemCategory.Melee, Cost = 100000}},
{ 0xCEDCB131, new SRIVTableData.InventoryItem { Name = "heli_fighter_01_w", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x50345770, new SRIVTableData.InventoryItem { Name = "plane_fighter01_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x554EE0DE, new SRIVTableData.InventoryItem { Name = "sp_novelty01_w", DisplayName = "THIS IS A PROTOTYPE, IF YOU SEE ME, PLEASE BUG ME", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xF43051D8, new SRIVTableData.InventoryItem { Name = "RiotShield", DisplayName = "RIOT SHIELD", Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0xC3651D82, new SRIVTableData.InventoryItem { Name = "sp_tank01_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xCBA2D691, new SRIVTableData.InventoryItem { Name = "sp_tank01_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x99FE980E, new SRIVTableData.InventoryItem { Name = "suv_4dr_luxury05_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x4060FCC8, new SRIVTableData.InventoryItem { Name = "vehicle_50cal", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xB0A30431, new SRIVTableData.InventoryItem { Name = "vehicle_flamethrower", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x6CEB14B8, new SRIVTableData.InventoryItem { Name = "vehicle_grenade_launcher", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x45A09FAD, new SRIVTableData.InventoryItem { Name = "vehicle_minigun", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xBA7BCEAF, new SRIVTableData.InventoryItem { Name = "vehicle_rpg_annihilator", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x7BD584DC, new SRIVTableData.InventoryItem { Name = "vehicle_rpg_launcher", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xCD58655F, new SRIVTableData.InventoryItem { Name = "heli_fighter_03_w", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xCDDAA623, new SRIVTableData.InventoryItem { Name = "sp_vtol01_w", DisplayName = "LASER BEAM", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xC123A3DB, new SRIVTableData.InventoryItem { Name = "sp_tank02_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xCF9C187A, new SRIVTableData.InventoryItem { Name = "sp_vtol02_w", DisplayName = "LASER BEAM", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x2C4C7B37, new SRIVTableData.InventoryItem { Name = "suv_4dr_02_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x61A134A8, new SRIVTableData.InventoryItem { Name = "stun_gun", DisplayName = "STUN GUN", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x963DC16C, new SRIVTableData.InventoryItem { Name = "WP_blowup_doll", DisplayName = "BLOW-UP DOLL", Category = SRIVTableData.InventoryItemCategory.WieldableProps}},
{ 0xC0E1C9EC, new SRIVTableData.InventoryItem { Name = "sp_tank03_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x61AB1E1A, new SRIVTableData.InventoryItem { Name = "sp_tank03_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xF9890F00, new SRIVTableData.InventoryItem { Name = "dildo_bat", DisplayName = "DILDO BAT", Cost = 50000, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0xACD228EA, new SRIVTableData.InventoryItem { Name = "decker_sword", DisplayName = "NOCTURNE", Category = SRIVTableData.InventoryItemCategory.Melee, Cost = 50000}},
{ 0x26F45573, new SRIVTableData.InventoryItem { Name = "Shotgun-Bling", DisplayName = "BLING SHOTGUN", Category = SRIVTableData.InventoryItemCategory.Shotgun, Cost = 0}},
{ 0xCC9A0F68, new SRIVTableData.InventoryItem { Name = "heli_fighter_02_w", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x2D98D96D, new SRIVTableData.InventoryItem { Name = "WP_baseball_bat", DisplayName = "BASEBALL BAT", Category = SRIVTableData.InventoryItemCategory.WieldableProps}},
{ 0x5272E929, new SRIVTableData.InventoryItem { Name = "plane_fighter02_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xCBB32BBB, new SRIVTableData.InventoryItem { Name = "Killbane_gloves", DisplayName = "APOCA-FIST", Cost = 100000, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0xC6F026BD, new SRIVTableData.InventoryItem { Name = "sp_gat01_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x94AF88AF, new SRIVTableData.InventoryItem { Name = "Killbane_gloves2", DisplayName = "APOCA-FIST", Cost = 1000, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0xF0F9C616, new SRIVTableData.InventoryItem { Name = "truck_2dr_classic01_w", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xC678AD23, new SRIVTableData.InventoryItem { Name = "satchel_remote", DisplayName = "SATCHEL CHARGES", Cost = 10000, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0x01689963, new SRIVTableData.InventoryItem { Name = "bike_jet01_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x634F1478, new SRIVTableData.InventoryItem { Name = "SMG-Cyberspace", DisplayName = "CYBER BLASTER", Category = SRIVTableData.InventoryItemCategory.SMG, Cost = 100000}},
{ 0x87F2BD7D, new SRIVTableData.InventoryItem { Name = "Explosive-Genki", DisplayName = "MOLLUSK LAUNCHER", Cost = 0, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0x36D9416D, new SRIVTableData.InventoryItem { Name = "sp_tank03_Reward_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xFBE29524, new SRIVTableData.InventoryItem { Name = "sp_tank03_Reward_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xA585B938, new SRIVTableData.InventoryItem { Name = "Explosive-GrenadeLauncher", DisplayName = "M2 GRENADE LAUNCHER", Category = SRIVTableData.InventoryItemCategory.Explosive, Cost = 0}},
{ 0xEDAE1C09, new SRIVTableData.InventoryItem { Name = "Shotgun-Chum", DisplayName = "SHARK-O-MATIC", Category = SRIVTableData.InventoryItemCategory.Shotgun, Cost = 0}},
{ 0x1B4FD4B4, new SRIVTableData.InventoryItem { Name = "Rifle-Thumpgun", DisplayName = "THUMPGUN", Category = SRIVTableData.InventoryItemCategory.Shotgun, Cost = 35000}},
{ 0x97A81A02, new SRIVTableData.InventoryItem { Name = "sp_vtol01_saints_w", DisplayName = "LASER BEAM", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x70C419A5, new SRIVTableData.InventoryItem { Name = "sp_tank03_saints_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x9A1C59C3, new SRIVTableData.InventoryItem { Name = "suv_4dr_02_saints_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x6E1BC2FE, new SRIVTableData.InventoryItem { Name = "sp_tank03_saints_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x55BC6D16, new SRIVTableData.InventoryItem { Name = "Rifle-Killshot", DisplayName = "TOGO-13", Cost = 1000000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x290E0FFA, new SRIVTableData.InventoryItem { Name = "Rifle-Laser", DisplayName = "ALIEN RIFLE", Category = SRIVTableData.InventoryItemCategory.Rifle, Cost = 35000}},
{ 0xB793A048, new SRIVTableData.InventoryItem { Name = "Pistol-Laser", DisplayName = "ALIEN SMG", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0x8984E7E1, new SRIVTableData.InventoryItem { Name = "Rifle-Disintegrator", DisplayName = "DISINTEGRATOR", Category = SRIVTableData.InventoryItemCategory.Rifle, Cost = 35000}},
{ 0xB255A2FD, new SRIVTableData.InventoryItem { Name = "Light_Sword", DisplayName = "ENERGY SWORD", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x3F9A95CB, new SRIVTableData.InventoryItem { Name = "Tentacle_Bat", DisplayName = "TENTACLE BAT", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x358FAC66, new SRIVTableData.InventoryItem { Name = "laser_minigun", DisplayName = "LASERGUN ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x94AED15D, new SRIVTableData.InventoryItem { Name = "Pistol-WardenSuit", DisplayName = "ALIEN SMG", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0x8A070E91, new SRIVTableData.InventoryItem { Name = "hovercraft_4dr_01_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xD7858C08, new SRIVTableData.InventoryItem { Name = "Special-Abduction", DisplayName = "ABDUCTION GUN", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 20000}},
{ 0x6FA56838, new SRIVTableData.InventoryItem { Name = "Special-BlackHoleGun", DisplayName = "BLACK HOLE LAUNCHER", Cost = 50000, Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0x51D7FF96, new SRIVTableData.InventoryItem { Name = "giant_mascot_laser_eyes", DisplayName = "TAUNTS", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x800D356E, new SRIVTableData.InventoryItem { Name = "grenade_suppression", DisplayName = "ELECTRIC GRENADES", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Grenade}},
{ 0x48DA139E, new SRIVTableData.InventoryItem { Name = "EnergyShield", DisplayName = "ZINTEK FORCE SHIELD", Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x4E18AE4B, new SRIVTableData.InventoryItem { Name = "Minefield_Gun", DisplayName = "MINETHROWER ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x1E3864D7, new SRIVTableData.InventoryItem { Name = "Mech_suit_weapon", DisplayName = "MECH SUIT", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x1B948960, new SRIVTableData.InventoryItem { Name = "Temp-Locust", DisplayName = "TINY PISTOL", Category = SRIVTableData.InventoryItemCategory.TempPickup, Cost = 0}},
{ 0x167791D9, new SRIVTableData.InventoryItem { Name = "anal_probe", DisplayName = "RECTIFIER PROBE", Cost = 50000, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x671A6C1E, new SRIVTableData.InventoryItem { Name = "sp_tank_hover_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x32BF768F, new SRIVTableData.InventoryItem { Name = "sp_tank_hover_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xA5D7F7AE, new SRIVTableData.InventoryItem { Name = "Ladle", DisplayName = "GIANT LADLE", Cost = 250, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x78B0977C, new SRIVTableData.InventoryItem { Name = "Pistol-Revolver", DisplayName = "ALIEN PISTOL", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0x3F00E91F, new SRIVTableData.InventoryItem { Name = "sp_ufo03_w", DisplayName = "LASER BEAM", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xBD7D988F, new SRIVTableData.InventoryItem { Name = "sp_tank_vr_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xB56DB2A9, new SRIVTableData.InventoryItem { Name = "sp_tank_vr_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xC39222F7, new SRIVTableData.InventoryItem { Name = "sp_turret_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x8FCC4270, new SRIVTableData.InventoryItem { Name = "Special-FreezeBlast", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 0}},
{ 0xB0B398D5, new SRIVTableData.InventoryItem { Name = "m06_baseball_bat", DisplayName = "BASEBALL BAT", Cost = 250, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x0BC7239E, new SRIVTableData.InventoryItem { Name = "Special-FreezeBlast-Shatter", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0x955CDAA3, new SRIVTableData.InventoryItem { Name = "Special-FireBlast", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 0}},
{ 0x6FD82489, new SRIVTableData.InventoryItem { Name = "Special-GlitchBlast", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 0}},
{ 0x45627D3F, new SRIVTableData.InventoryItem { Name = "Special-WardenHaduoken", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0x561A8231, new SRIVTableData.InventoryItem { Name = "Special-HomieHaduoken", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0xDC0D91B0, new SRIVTableData.InventoryItem { Name = "Murderbot_Minigun", DisplayName = "LASERGUN ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x54D39247, new SRIVTableData.InventoryItem { Name = "Murderbot_Minethrower", DisplayName = "MINETHROWER ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0xA1DD3D7D, new SRIVTableData.InventoryItem { Name = "Special-InflatoRay", DisplayName = "INFLATO-RAY", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 10000}},
{ 0x8607F38C, new SRIVTableData.InventoryItem { Name = "Rifle-M00", DisplayName = "BURST RIFLE", Cost = 10000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x983A5618, new SRIVTableData.InventoryItem { Name = "Rifle-GangM00", DisplayName = "AUTOMATIC RIFLE", Cost = 10000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x11B2A72E, new SRIVTableData.InventoryItem { Name = "SMG-GangM00", DisplayName = "HEAVY SMG", Category = SRIVTableData.InventoryItemCategory.SMG, Cost = 5000}},
{ 0xA84E9C16, new SRIVTableData.InventoryItem { Name = "SMG-StormM00", DisplayName = "RAPID-FIRE SMG", Cost = 5000, Category = SRIVTableData.InventoryItemCategory.SMG}},
{ 0x68D566A4, new SRIVTableData.InventoryItem { Name = "Shotgun-M00", DisplayName = "SEMI-AUTO SHOTGUN", Cost = 10000, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0xE3CF7EDA, new SRIVTableData.InventoryItem { Name = "Special-Bounce", DisplayName = "BOUNCE RIFLE", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 35000}},
{ 0xBCBEEE42, new SRIVTableData.InventoryItem { Name = "sp_turret_wh_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x50B9CFD2, new SRIVTableData.InventoryItem { Name = "sp_turret_wh_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x0E95E02E, new SRIVTableData.InventoryItem { Name = "CombatKnife_LARGE", DisplayName = "BASEBALL BAT", Cost = 250, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x3AD13D6D, new SRIVTableData.InventoryItem { Name = "Rifle-Commander", DisplayName = "'MERICA", Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0xD9858F54, new SRIVTableData.InventoryItem { Name = "Explosive-PlasmaCannon", DisplayName = "ALIEN RPG", Cost = 50000, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0x89891AC7, new SRIVTableData.InventoryItem { Name = "sp_crib_turrets_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xCD5E8F89, new SRIVTableData.InventoryItem { Name = "sp_crib_turrets_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xF5596EA8, new SRIVTableData.InventoryItem { Name = "sp_turretball_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x9EA7E06A, new SRIVTableData.InventoryItem { Name = "Special-Dubstep", DisplayName = "DUBSTEP GUN", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 35000}},
{ 0x734F5E7A, new SRIVTableData.InventoryItem { Name = "Special-EnemyHaduoken", DisplayName = "", Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0xE2755F40, new SRIVTableData.InventoryItem { Name = "zinyak_mech_suit_minigun", DisplayName = "LASERGUN ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x86F18439, new SRIVTableData.InventoryItem { Name = "sp_turretball_w_char", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xB64EA1E7, new SRIVTableData.InventoryItem { Name = "Murderbot_Minigun-PC", DisplayName = "LASERGUN ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x4C83682A, new SRIVTableData.InventoryItem { Name = "Murderbot_Minethrower-PC", DisplayName = "MINETHROWER ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x11FFC916, new SRIVTableData.InventoryItem { Name = "sp_tank_vr_red_w", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0x3C121B18, new SRIVTableData.InventoryItem { Name = "sp_tank_vr_red_w2", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xAB24F570, new SRIVTableData.InventoryItem { Name = "Explosive-PlasmaCannon-Zinyak", DisplayName = "ALIEN RPG", Cost = 15000, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0x970D0974, new SRIVTableData.InventoryItem { Name = "sp_crib_ship_w_new", DisplayName = "LASER BEAM", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xE672C72F, new SRIVTableData.InventoryItem { Name = "Pistol-Gang-HeavyCaptain", DisplayName = "HEAVY PISTOL", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0xC57249E4, new SRIVTableData.InventoryItem { Name = "sp_tank_hover_w_M21", DisplayName = "MOUNTED .50 CAL", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xE89FF93D, new SRIVTableData.InventoryItem { Name = "Pistol-Gang", DisplayName = "HEAVY PISTOL", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0xFE7D8657, new SRIVTableData.InventoryItem { Name = "Explosive-RocketLauncher-1-Guitar", DisplayName = "RPG", Cost = 15000, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0xE9489044, new SRIVTableData.InventoryItem { Name = "WP_RoninSword", DisplayName = "RONIN SWORD", Category = SRIVTableData.InventoryItemCategory.WieldableProps}},
{ 0xBEE489EA, new SRIVTableData.InventoryItem { Name = "WP_cricket_bat", DisplayName = "CRICKET BAT", Category = SRIVTableData.InventoryItemCategory.WieldableProps}},
{ 0x596F6E89, new SRIVTableData.InventoryItem { Name = "Special-IronSaint", DisplayName = "SUIT BLASTER", Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0xC6529DC1, new SRIVTableData.InventoryItem { Name = "SMG-StormM11", DisplayName = "TAC SMG", Cost = 5000, Category = SRIVTableData.InventoryItemCategory.SMG}},
{ 0xB209E416, new SRIVTableData.InventoryItem { Name = "Murderbot_Mini-Unlockable", DisplayName = "LASERGUN ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0xB703A997, new SRIVTableData.InventoryItem { Name = "Murderbot_Mine-Unlockable", DisplayName = "MINETHROWER ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0x05A84E09, new SRIVTableData.InventoryItem { Name = "Space_Dive_Blaster", DisplayName = "EXO-SUIT", Category = SRIVTableData.InventoryItemCategory.Rifle, Cost = 20000}},
{ 0x5BD3722A, new SRIVTableData.InventoryItem { Name = "Rifle-GangM00-NPC", DisplayName = "AUTOMATIC RIFLE", Cost = 10000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x071017BF, new SRIVTableData.InventoryItem { Name = "Rifle-Gang-LMG", DisplayName = "AUTOMATIC RIFLE", Cost = 10000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x4A022AB2, new SRIVTableData.InventoryItem { Name = "Pistol-Police-Redshirt", DisplayName = "QUICKSHOT PISTOL", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0x5AE57729, new SRIVTableData.InventoryItem { Name = "Pistol-Police-Smuggler", DisplayName = "QUICKSHOT PISTOL", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0x8917EB39, new SRIVTableData.InventoryItem { Name = "Pistol-Gang-Cumia", DisplayName = "HEAVY PISTOL", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0x806ACEA5, new SRIVTableData.InventoryItem { Name = "Pistol-Gang-DEKRD", DisplayName = "HEAVY PISTOL", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Pistol}},
{ 0xAC50E7F4, new SRIVTableData.InventoryItem { Name = "Explosive-RocketLauncher-Soopa", DisplayName = "RPG", Cost = 15000, Category = SRIVTableData.InventoryItemCategory.Explosive}},
{ 0x1497E83E, new SRIVTableData.InventoryItem { Name = "baseball_ratstick", DisplayName = "BASEBALL BAT", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Melee}},
{ 0x0B1C20ED, new SRIVTableData.InventoryItem { Name = "Shotgun-Police-Ion", DisplayName = "SEMI-AUTO SHOTGUN", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0x8CAE2752, new SRIVTableData.InventoryItem { Name = "Rifle-NG-Pulse", DisplayName = "BURST RIFLE", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x85B2F60F, new SRIVTableData.InventoryItem { Name = "Special-SniperRifle-Lever", DisplayName = "SNIPER RIFLE", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0x77795977, new SRIVTableData.InventoryItem { Name = "Rifle-NG-Soaker", DisplayName = "AUTOMATIC RIFLE", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x02FDE599, new SRIVTableData.InventoryItem { Name = "SMG-Gang-Rubber", DisplayName = "HEAVY SMG", Category = SRIVTableData.InventoryItemCategory.SMG, Cost = 100}},
{ 0xB30DB46D, new SRIVTableData.InventoryItem { Name = "Shotgun-Gang-Blunder", DisplayName = "PUMP-ACTION SHOTGUN", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0xABB1BBF7, new SRIVTableData.InventoryItem { Name = "Shotgun-Police", DisplayName = "SEMI-AUTO SHOTGUN", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0x20D57603, new SRIVTableData.InventoryItem { Name = "Shotgun-Police-Hunt", DisplayName = "SEMI-AUTO SHOTGUN", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0xFBE508A1, new SRIVTableData.InventoryItem { Name = "Shotgun-Gang-Laser", DisplayName = "PUMP-ACTION SHOTGUN", Cost = 100, Category = SRIVTableData.InventoryItemCategory.Shotgun}},
{ 0x001A9652, new SRIVTableData.InventoryItem { Name = "SMG-Storm-Robo", DisplayName = "RAPID-FIRE SMG", Cost = 100, Category = SRIVTableData.InventoryItemCategory.SMG}},
{ 0xB857D657, new SRIVTableData.InventoryItem { Name = "SMG-Gang-Tommy", DisplayName = "HEAVY SMG", Category = SRIVTableData.InventoryItemCategory.SMG, Cost = 100}},
{ 0x6AB6DB0E, new SRIVTableData.InventoryItem { Name = "SMG-Storm-Nail", DisplayName = "RAPID-FIRE SMG", Cost = 100, Category = SRIVTableData.InventoryItemCategory.SMG}},
{ 0x954B057D, new SRIVTableData.InventoryItem { Name = "Rifle-Gang-Rail", DisplayName = "AUTOMATIC RIFLE", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x47B753ED, new SRIVTableData.InventoryItem { Name = "Special-SniperRifle-WW2", DisplayName = "SNIPER RIFLE", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0x110B4DFB, new SRIVTableData.InventoryItem { Name = "Special-SniperRifle-Craft", DisplayName = "SNIPER RIFLE", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Special}},
{ 0x90F256F8, new SRIVTableData.InventoryItem { Name = "Murderbot_Mini-M17", DisplayName = "LASERGUN ARM", Category = SRIVTableData.InventoryItemCategory.TempPickup}},
{ 0xE4A7B5F7, new SRIVTableData.InventoryItem { Name = "SMG-StormM11-B", DisplayName = "TAC SMG", Cost = 5000, Category = SRIVTableData.InventoryItemCategory.SMG}},
{ 0xB4F8B20B, new SRIVTableData.InventoryItem { Name = "Special-Dubstep-VarA", DisplayName = "DUBSTEP GUN", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 35000}},
{ 0x2DF1E3B1, new SRIVTableData.InventoryItem { Name = "Special-Dubstep-VarB", DisplayName = "DUBSTEP GUN", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 35000}},
{ 0x5AF6D327, new SRIVTableData.InventoryItem { Name = "Special-Dubstep-VarC", DisplayName = "DUBSTEP GUN", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 35000}},
{ 0x5953CA73, new SRIVTableData.InventoryItem { Name = "boat_speed04_w", DisplayName = "MINIGUN/ROCKETS", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xF998D0E4, new SRIVTableData.InventoryItem { Name = "sp_vtol01_w_Eagle", DisplayName = "LASER BEAM", Category = SRIVTableData.InventoryItemCategory.Vehicle}},
{ 0xB8CAB560, new SRIVTableData.InventoryItem { Name = "Special-Dubstep-M19", DisplayName = "DUBSTEP GUN", Category = SRIVTableData.InventoryItemCategory.Special, Cost = 35000}},
{ 0x6C0C99DC, new SRIVTableData.InventoryItem { Name = "SMG-Gang_Cutscene", DisplayName = "HEAVY SMG", Category = SRIVTableData.InventoryItemCategory.SMG, Cost = 100}},
{ 0x0D72BA5A, new SRIVTableData.InventoryItem { Name = "Rifle-Gang_Cutscene", DisplayName = "AUTOMATIC RIFLE", Cost = 20000, Category = SRIVTableData.InventoryItemCategory.Rifle}},
{ 0x9125973E, new SRIVTableData.InventoryItem { Name = "Rifle-Laser_Cutscene", DisplayName = "ALIEN RIFLE", Category = SRIVTableData.InventoryItemCategory.Rifle, Cost = 35000}},
{ 0x79A242A6, new SRIVTableData.InventoryItem { Name = "Tentacle_Bat-SPFC", DisplayName = "TENTACLE BAT", Cost = 35000, Category = SRIVTableData.InventoryItemCategory.Melee}},

        };

        internal static Dictionary<uint, string> UnlockablesDescriptions = new Dictionary<uint, string>
            {
                {
                    0x95D074EE,
                    "Create a vacuum around you and your bullets that drains the life force out of nearby enemies and adds it to your own!\n\nDouble tap {SUPER_QUICK_SHIELD_IMG} to quick-switch between Buff Elements. Or switch Elements in the Powers menu."
                },
                {0x496AB96D, "50%% lower vehicle customization cost from Matt Millers' hacking"},
                {0xBA264E0F, "25%% lower weapon store prices from Matt Millers' hacking"},
                {0x8F7735BE, "When called, wipes all Notoriety."},
                {0xA2D2F0B8, "Receive a 15%% discount at clothing stores"},
                {0xB5FFA4E1, "10%% bonus to ALL XP earned"},
                {0x13007154, "10%% bonus to ALL Cache earned."},
                {
                    0xDF1943E4,
                    "Simply having Buff equipped gives a chance for the elemental damage to be applied to an attack."
                },
                {0x17274D6D, "Telekinesis can catch objects thrown at you!"},
                {0xC8394568, "You will never be ragdolled when hit by explosions."},
                {0x5B7B59EF, "Cars can no longer knock you down even when standing still!"},
                {0x747ED2BB, "You will not take ANY damage from fire."},
                {
                    0x8E8C8D51,
                    "@@TK_FORCE^^Increases damage to vehicles thrown.@@TK_LIGHTNING^^Increases distance that lightning arcs between objects.@@TK_LIFE_STEAL^^Increases the health stolen per second."
                },
                {
                    0x1785DCEB,
                    "@@TK_FORCE^^Greatly increases damage to vehicles thrown.@@TK_LIGHTNING^^Greatly increases distance that lightning arcs between objects.@@TK_LIFE_STEAL^^Greatly increases the health stolen per second."
                },
                {0x07F46A99, "Become as light as a feather to greatly increase how far you can glide."},
                {0xEFABC4AB, "Charged Super Jumps fly farther and faster."},
                {
                    0xD464AAE2,
                    "Super Sprinting now works on water. Hold {SPRINT_IMG} as you enter water to sprint across it!"
                },
                {0xCEF219F6, "Super Sprinting reaches maximum speed faster!"},
                {
                    0x9F645A53,
                    "Genki Backup can be found in your HUB  PHONE menu. The Genki Girls will arrive to help in combat."
                },
                {0x4ED97058, "Receive more free ammo when you access your Gateway Weapon Cache."},
                {0xD7D021E2, "Receive much more free ammo when you access your Gateway Weapon Cache."},
                {
                    0x201D0636,
                    "Give your enemies a close encounter with the Abduct-O-Matic!\nEquip it at your Gateway Weapon Cache."
                },
                {0x2E19ECD6, "A collection of Monster Trucks is now available at your Gateway Garage."},
                {
                    0xEEF80FAF,
                    "Super Backup can be found in your HUB  PHONE menu. A squad of Super Saints will arrive to help in combat."
                },
                {0x134F5337, "Increase cache and XP received from defeating Flashpoints."},
                {0x49E0B158, "Unlimited Sprinting."},
                {0x383554FE, "The Cyber Gunslinger is now available at your Gateway garage."},
                {0xF6EDC7C7, "Increase cache and XP received from hacking stores."},
                {0xF883FEB3, "Increase the length of the Stamina bar."},
                {0x618AAF09, "Greatly increase the length of the Stamina bar."},
                {0x168D9F9F, "Maximize the length of the Stamina bar."},
                {0xDA97CA35, "Recruit One Saint Gang Member as a Follower."},
                {0x439E9B8F, "Recruit Two Saint followers. Press {RECRUIT_DISMISS_IMG}  to recruit nearby Saints."},
                {0x3499AB19, "Recruit Three Saint followers. Press {RECRUIT_DISMISS_IMG}  to recruit nearby Saints."},
                {0xAF7FE7A1, "You can now have Strippers in your gang."},
                {0x5AC1D681, "You can now have Space Saints in your gang."},
                {0x27AB6FC7, "You can now have Mascots in your gang."},
                {0x464010DD, "You can now have Hos in your gang."},
                {0x712F8B68, "You can now have Guardsmen in your gang."},
                {0x7E91F271, "You can now have Gimps in your gang."},
                {0xCD041A3D, "You can now have Cops in your gang."},
                {
                    0x4B9959D5,
                    "New Gateway unlocked on Stanfield Island.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0x4D90A4DB,
                    "New Gateway unlocked on Carver Island.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0xD317F79A,
                    "New Gateway unlocked on New Colvin Island.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0x3EAF9590,
                    "New Gateway unlocked in Downtown.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0x837E7C28,
                    "New Gateway unlocked in Downtown.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0x85EC8FB1,
                    "Tanya has been reconstructed as an Ally and can be found in your HUB  PHONE menu. She will help the Saints in combat."
                },
                {
                    0xE005616F,
                    "Maero has been reconstructed as an Ally and can be found in your HUB  PHONE menu. He will help the Saints in combat."
                },
                {
                    0x8954359C,
                    "Kinzie can also be called into the simulation from your HUB  PHONE menu. She will help the Saints in combat."
                },
                {
                    0x41D2A114,
                    "Julius Little has been reconstructed as an Ally and can be found in your HUB  PHONE menu. He will help the Saints in combat."
                },
                {
                    0x25DF2B1B,
                    "DJ Veteran Child  has been reconstructed as an Ally and can be found in your HUB  PHONE menu. He will help the Saints in combat."
                },
                {
                    0x88B1DF03,
                    "Cyrus Temple has been reconstructed as an Ally and can be found in your HUB  PHONE menu. He will help the Saints in combat."
                },
                {
                    0x5D270503,
                    "C.I.D. can be called into the simulation using your HUB  PHONE menu. The A.I. will help the Saints in combat."
                },
                {
                    0xDB3C32C6,
                    "The MECH SUIT can be found in your HUB  PHONE menu. Call to temporarily pilot the Mech Suit anytime you want!"
                },
                {0x9454F3E9, "Shaundi can be found in your HUB  PHONE menu. She will help the Saints in combat."},
                {0xC3B8F9F7, "Pierce can be found in your HUB  PHONE menu. He will help the Saints in combat."},
                {
                    0x71DE8763,
                    "Collect your homies' Audio Logs which have been scattered throughout the simulation. Listen to retrieved entries on the Ship's rec room computer."
                },
                {0x1D77E524, "Increase all Cache received by 5%%"},
                {0x847EB49E, "Increase all Cache received by 10%%"},
                {0xF3798408, "Increase all Cache received by 15%%"},
                {0x45CE9793, "Twice as much Cache is dropped by deleted simulations"},
                {0x3FA8EF63, "Player health is increased by 25%%"},
                {0xA6A1BED9, "Player health is increased by 50%%"},
                {0xD1A68E4F, "Player health is increased by 75%%"},
                {0x4FC21BEC, "Player health is increased by 100%%"},
                {0x16F53E13, "Small increase to time before followers bleed out."},
                {0x8FFC6FA9, "Medium increase to time before followers bleed out."},
                {0xC61CB72B, "Hourly Cache Transfer of {game_cash}500"},
                {0x5F15E691, "Hourly Cache Transfer of {game_cash}1,000"},
                {0x2812D607, "Hourly Cache Transfer of {game_cash}1,500"},
                {0x95B3AE06, "Every vehicle you drive has nitrous.  Press {NITRO_IMG} to engage."},
                {0x4B133C83, "Carry 25%% more SMG ammo"},
                {0xD21A6D39, "Carry 50%% more SMG ammo"},
                {0xA51D5DAF, "Carry 75%% more SMG ammo"},
                {0x811EA413, "Carry 25%% more shotgun ammo"},
                {0x1817F5A9, "Carry 50%% more shotgun ammo"},
                {0x6F10C53F, "Carry 75%% more shotgun ammo"},
                {0xDA910C8C, "Carry 25%% more rifle ammo"},
                {0x43985D36, "Carry 50%% more rifle ammo"},
                {0x349F6DA0, "Carry 75%% more rifle ammo"},
                {0x464AEBAF, "Carry 25%% more pistol ammo"},
                {0xDF43BA15, "Carry 50%% more pistol ammo"},
                {0xA8448A83, "Carry 75%% more pistol ammo"},
                {0x8E701A72, "Unlimited Stamina. Just keep flying to your heart's content."},
                {0xE3EFB696, "Carefully bump into people to steal their cache!"},
                {0xFCE59D8B, "Minor reduction in damage taken from bullets and lasers."},
                {0x65ECCC31, "Moderate reduction in damage taken from bullets and lasers."},
                {0x12EBFCA7, "Great reduction in damage taken from bullets and lasers."},
                {0xDCE83355, "Minor reduction in damage taken from explosions."},
                {0x45E162EF, "Moderate reduction in damage taken from explosions."},
                {0x32E65279, "Great reduction in damage taken from explosions."},
                {0x04142634, "Minor reduction in damage taken from fire."},
                {0x52DADA8D, "Dual Wield Pistols"},
                {0x70AE48F9, "Dual Wield SMGS"},
                {0x3EB11B90, "Convert Cache to 4,000 XP"},
                {0xA7B84A2A, "Convert Cache to 8,000 XP"},
                {0xD0BF7ABC, "Convert Cache to 12,000 XP"},
                {0xBDD6F187, "Customize vehicles"},
                {0xF4ABE2FD, "5%% bonus to ALL XP earned"},
                {0x9D1D778E, "Moderate reduction in damage taken from fire."},
                {0xEA1A4718, "Great reduction in damage taken from fire."},
                {0x0EA99DE5, "Your Notoriety will decay at a slightly faster rate"},
                {0x97A0CC5F, "Your Notoriety will decay at a faster rate"},
                {0xE0A7FCC9, "Your Notoriety will decay at a much faster rate"},
                {0x75E544CD, "Upgrade your gang members to carry SMGS"},
                {0xC1E8602D, "Upgrade your gang members to carry Shotguns"},
                {0x48B53CC3, "Upgrade your gang members to carry Rifles"},
                {0x729F596F, "Saints Backup can be found in your HUB  PHONE menu. Saints will arrive to help in combat"},
                {0x776A984A, "Revive followers faster."},
                {0xE7B23F83, "Gang Customization is now available at Gateways."},
                {0x6DA2B347, "10%% bonus to ALL XP earned"},
                {0x1AA583D1, "15%% bonus to ALL XP earned"},
                {0xDB8ECEDB, "You now have a set of Watercraft available at your Gateway Garage"},
                {0x83F64DF6, "Homies get a small health boost"},
                {0x1AFF1C4C, "Homies get a medium health boost"},
                {0x6DF82CDA, "Homies get a large health boost"},
                {0xBC5BF92A, "Carry 75%% more explosive ammo"},
                {0xCB5CC9BC, "Carry 50%% more explosive ammo"},
                {0x52559806, "Carry 25%% more explosive ammo"},
                {0x1922280B, "Carry 75%% more special ammo"},
                {0x6E25189D, "Carry 50%% more special ammo"},
                {0xF72C4927, "Carry 25%% more special ammo"},
                {0xCCEA827A, "Collectible items are highlighted in the world"},
                {0xA8B04439, "Purchase new Abilities, Bonuses, and Skills in the UPGRADES menu on your Cell Phone."},
                {
                    0x81EEE65C,
                    "Harder instances of the TANK MAYHEM Activity are available in the simulation. \nCheck your MAP for locations."
                },
                {
                    0x1151FBCD,
                    "Harder instances of the BLAZIN Activity are available in the simulation. \nCheck your MAP for locations."
                },
                {
                    0xD7B441DA,
                    "Harder instances of PROF. GENKI'S M.O.M. Activity are available in the simulation. \nCheck your MAP for locations."
                },
                {
                    0x6A84BBCB,
                    "Hard and varied instances of the MAYHEM Activity are available in the simulation. \nCheck your MAP for locations."
                },
                {
                    0xE95053C8,
                    "Harder instances of the FRAUD Activity are available in the simulation. \nCheck your MAP for locations."
                },
                {
                    0xA0AAF9CB,
                    "Harder instances of the BLAZIN Activity are available in the simulation. \nCheck your MAP for locations."
                },
                {
                    0x0702E57D,
                    "Take to the sky and glide across the city!\n\nHOLD {SPRINT_IMG} after an Air Dash to Glide."
                },
                {0x2F803673, "Take less damage when Super Sprinting."},
                {0x96EA63BD, "Telekinesis can pick up tanks and aircraft!"},
                {0x7B986FAF, "Stomp Energy recharges faster."},
                {0xE2913E15, "Stomp Energy recharges even faster."},
                {0x95960E83, "Stomp Energy recharges very quickly."},
                {0xFB23FAFA, "Telekinesis Energy recharges faster."},
                {0x622AAB40, "Telekinesis Energy recharges even faster."},
                {0x152D9BD6, "Telekinesis Energy recharges very quickly."},
                {0xF1EB19B6, "Twice as much time to complete store hacking."},
                {0xB87DF4AF, "You can now run faster than cars with Super Sprint!"},
                {
                    0xCAF554E5,
                    "You can now pick up and throw around people/objects with Telekinesis!\n\nUse {SUPER_QUICK_TELEKINESIS_IMG} to quick-switch to Telekinesis."
                },
                {0x496E73F0, "Dashing generates a shield that can block all incoming damage and reflect projectiles!"},
                {
                    0xC5AEFF50,
                    "You can now smash your foot into the ground and send everything nearby flying with Super Stomp!\n\nUse {SUPER_QUICK_STOMP_IMG} to quick-switch to Stomp."
                },
                {
                    0x692792B1,
                    "You can now climb vertical surfaces with Wall Jump or jump higher than buildings with your charged Super Jump!\n\nHOLD {JUMP_CLIMB_IMG} to charge your jumps to leap higher and farther."
                },
                {0x918990F8, "Gain a boost of speed in midair by pressing {SPRINT_IMG}."},
                {0x0880C142, "Dash twice per Super Jump."},
                {0x7F87F1D4, "Dash three times per Super Jump."},
                {
                    0x116C5502,
                    "You can now shoot energy at enemies with Blast!\n\nUse {SUPER_QUICK_FREEZE_IMG} to quick-switch to Blast."
                },
                {
                    0xBBB3D6C4,
                    "You can now charge your body and weapons with energy!\n\nUse {SUPER_QUICK_SHIELD_IMG} to quick-switch to Buff."
                },
                {
                    0x67615E53,
                    "Hover in midair by HOLDING {SHOULDER_CAMERA_IMG}, then press {ATTACK_PRIMARY_IMG} smash into the ground at super speed."
                },
                {0xB202AB6B, "You now have the alien UFO available at your Gateway Garage."},
                {0xBBCEC998, "You now have Cyrus' VTOL available at your Gateway Garage"},
                {0xF150FD03, "You now have the Genkimobile available at your Gateway Garage."},
                {
                    0x52201E31,
                    "You now have the Genki Manapult, Angry Tiger, Sexy Kitten, and Sad Panda available at your Gateway Garage."
                },
                {0x0FF84B36, "You now have the cyberspace car and bike available at your Gateway Garage"},
                {0x5C6CBA2A, "You now have the cyber tank available at your Gateway Garage"},
                {
                    0x983E1C6C,
                    "Harder instances of the UFO MAYHEM Activity are available in the simulation. \nCheck your MAP for locations."
                },
                {
                    0x3CAB7420,
                    "Harder instances of the MECH SUIT MAYHEM Activity are available in the simulation.\nCheck your MAP for locations."
                },
                {0x83A745F7, "You now have the alien Hover Tank available at your Gateway Garage."},
                {0xFB2A24D4, "You now have the Genkimobile available at your Gateway Garage."},
                {
                    0xFA41996D,
                    "Charge up the Tyrant and let the explosive plasma fly!\nEquip it at your Gateway Weapon Cache."
                },
                {0x16A39BE4, "Super Sprinting now creates a Tornado effect that knocks over objects in player's wake!"},
                {0x5D0C55A9, "New Downloadable Content is available in your Gateway Weapon Cache."},
                {0x3A4D439E, "The Zin's secret weapon is at your disposal!"},
                {0xB4C430FD, "Dash Shield applies extra damage on contact."},
                {
                    0x308D7277,
                    "Delete simulants once and for all with the Disintegrator!\nEquip it at your Gateway Weapon Cache."
                },
                {
                    0x85C5E278,
                    "The Bounce Rifle fires social butterfliesŭ very deadly social butterflies. Equip it at your Gateway Weapon Cache."
                },
                {0x7A081ADE, "It's BIG in Japan!\nEquip the Violator at your Gateway Weapon Cache."},
                {0x7FB3E6A6, "Blast Energy recharges faster."},
                {0xE6BAB71C, "Blast Energy recharges even faster."},
                {0x91BD878A, "Blast Energy recharges very quickly."},
                {0x9CF9C858, "Targets remain affected by Blast longer."},
                {
                    0x342195CD,
                    "@@BLAST_FREEZE^^Targets take even more damage while frozen.@@BLAST_FIRE^^Targets take more damage per second while on fire.@@BLAST_GLITCH^^Increases the amount of targets that can be controlled at once."
                },
                {0x8B490E75, "Telekinesis no longer uses Energy."},
                {
                    0x15879046,
                    "@@TK_FORCE^^No energy is consumed while holding an object with Telekinesis.@@TK_LIGHTNING^^Only half as much energy is consumed while holding an object with Telekinesis.@@TK_LIFE_STEAL^^Only half as much energy is consumed while holding an object with Telekinesis."
                },
                {0x5759E9DE, "Increased height on charged Super Jumps."},
                {0x82B27823, "Extreme collision damage to other vehicles while using nitrous."},
                {0x6D9D487D, "Recover your powers a bit faster after being hit by a Power Suppression Grenade."},
                {
                    0x74004C2F,
                    "Press {JUMP_CLIMB_IMG} to immediately get back up from getting knocked down.\n\nA must-have for any Saint walking into super powered combat."
                },
                {0xC2BB3548, "Stomp affects a wider area."},
                {0xF49419C7, "Recover your powers faster after being hit by a Power Suppression Grenade."},
                {0x83932951, "Recover your powers much faster after being hit by a Power Suppression Grenade."},
                {0xC63584F1, "Slightly extend the amount of time you can spend in the Mech Suit."},
                {
                    0xA72BE0E3,
                    "Turn anything you pick up into a devastating ball of lightning!\n\nDouble tap {SUPER_QUICK_TELEKINESIS_IMG} to quick-switch between Elements. Or switch Elements in the Powers menu."
                },
                {
                    0xDDBAB5EC,
                    "Suck the digital life out of any living thing you grab and add it to your own!\n\nDouble tap {SUPER_QUICK_TELEKINESIS_IMG} to quick-switch between Elements. Or switch Elements in the Powers menu."
                },
                {
                    0x4CFAAD54,
                    "Lift your enemies into the air and lock them in a stasis field. Like shooting fish in a barrel!\n\nDouble tap {SUPER_QUICK_STOMP_IMG} to quick-switch between Elements. Or switch Elements in the Powers menu."
                },
                {
                    0xB63542F8,
                    "Cause your enemies to literally shrink in horror!\n\nDouble tap {SUPER_QUICK_STOMP_IMG} to quick-switch between Elements. Or switch Elements in the Powers menu."
                },
                {
                    0xD56B5825,
                    "Let pyromania ensue as you rain down hellfire on your enemies!\n\nDouble tap {SUPER_QUICK_FREEZE_IMG} to quick-switch between Blast Elements. Or switch Elements in the Powers menu."
                },
                {
                    0x19BA4848,
                    "Create a massive data surge around you that causes enemies to glitch out and fight for you!\n\nDouble tap {SUPER_QUICK_FREEZE_IMG} to quick-switch between Blast Elements. Or switch Elements in the Powers menu."
                },
                {
                    0x7FDCA944,
                    "Charge your bullets and the air around you with electricity. Lightning arcs between enemies for additional damage!\n\nDouble tap {SUPER_QUICK_SHIELD_IMG} to quick-switch between Buff Elements. Or switch Elements in the Powers menu."
                },
                {
                    0xE851A3F3,
                    "Become one with the simulation and use your mind to throw enemies around! Force does additional damage to targets upon impact!"
                },
                {
                    0x0C55C72E,
                    "Increased force is used when stomping the ground, causing extra damage to targets sent flying!"
                },
                {0xB4C4141E, "Freeze your enemies, then use your guns on them for double damage!"},
                {0xA35C7F5A, "Envelop yourself and your bullets in a lethal firewall!"},
                {0xCE50B864, "An even greater height on charged Super Jumps."},
                {0xB95788F2, "Maximum height on charged Super Jumps. You can leap tall buildings in a single bound!"},
                {0x89DE009A, "Increased speed while Super Sprinting. "},
                {0x10D75120, "Even greater speed while Super Sprinting. "},
                {0x67D061B6, "Maximum speed while Super Sprinting. You're practically faster than a speeding bullet!"},
                {0xDDDCD93B, "Increases Blast's area of effect range."},
                {0x44D58881, "Greatly increases Blast's area of effect range."},
                {0xDAF86527, "Increases range and stun time of Stomp."},
                {0x43F1349D, "Greatly increases range and stun time of Stomp."},
                {0x57DB19FD, "Increases how far objects can be thrown with Telekinesis."},
                {0x0BF29B20, "Stomp no longer uses Energy."},
                {0xA97793B3, "Buff Energy recharges faster."},
                {0x307EC209, "Buff Energy recharges even faster."},
                {0x4779F29F, "Buff Energy recharges very quickly."},
                {0xD91D673C, "Buff no longer uses Energy."},
                {
                    0x5A6D0C87,
                    "New Gateway unlocked in Downtown.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0xB30EA9B2,
                    "New Gateway unlocked on Stanfield Island.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0x5D00C89E,
                    "New Gateway unlocked on New Colvin Island.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0xCDBFD50F,
                    "New Gateway unlocked on Carver Island.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0xDD12A865,
                    "New Gateway unlocked on Arapice Island.\nReturn to the Ship, access your weapons cache, clothes, vehicles, and gang customization here."
                },
                {
                    0x0628A8D1,
                    "Vehicle Delivery can be found in your HUB  PHONE menu. Use it to instantly retrieve that have been scanned using {SCAN_IMG}."
                },
                {0x56481333, "The Adoration of America unlocked!"},
                {
                    0xB501DC47,
                    "Presidency of the United States unlocked! \n\nPress all buttons to initiate global thermonuclear war!"
                },
                {0xE641009A, "Chief of Staff - Benjamin King unlocked! \n\nBenjamin Motherf$@#ing King!"},
                {0xC58A5504, "Vice President - Keith David unlocked! \n\nYou ready for this Playa?"},
                {0x34F6040B, "Maximizes the range and stun time of Stomp. Smash entire city blocks!"},
                {0xCED24847, "Greatly increases how far objects can be thrown with Telekinesis."},
                {0x33D2B817, "Maximizes Blast's area of effect range. Blast entire intersections!"},
                {0xB9D578D1, "Maximizes how far objects can be thrown with Telekinesis."},
                {0x0FD91229, "Blast no longer uses Energy."},
                {
                    0x2575C252,
                    "Cause some non-spontaneous combustion with the Inflato-Ray! Can you dig it? Equip it at your Gateway Weapon Cache."
                },
                {0xD3365F86, "Inflato-Ray victims now explode, damaging nearby enemies."},
                {0xABC3C976, "Hit em so hard they melt with the Energy Sword!\nEquip it at your Gateway Weapon Cache."},
                {
                    0x4D9143D6,
                    "Create deadly black holes with the Singularity Gun!\nEquip it at your Gateway Weapon Cache."
                },
                {
                    0x2D847E46,
                    "Enemies now drop twice as much health when killed with the Energy Sword. Slice enemies up into delicious health chunks!"
                },
                {0x342BCBD0, "It's gotta be purple."},
                {0x89550D1D, "Try not. Do, or do not. There is no try."},
                {
                    0xF8550F41,
                    "You'll want to change your middle name to \"Motherfucking\" when you put these clothes on. This suit is now available at your Gateway Wardrobe."
                },
                {0x2A485D90, "Lightbulb not included. This outfit is now available at your Gateway Wardrobe."},
                {
                    0x4B6DDADD,
                    "Now you'll really understand how cold and lonely it is inside that shell. The CID suit is now available at your Gateway Wardrobe."
                },
                {
                    0x0B4F424C,
                    "The pants are not as fancy as they look. Matt Miller's outfit is now available at your Gateway Wardrobe."
                },
                {
                    0x4928B67A,
                    "@@STOMP_ROCK^^Increases the damage done to targets sent flying.@@STOMP_GRAVITY^^Damage taken by one target is spread to all targets in stasis.@@STOMP_SHRINK^^Shrunken targets take even more damage."
                },
                {
                    0x951AFC5C,
                    "@@STOMP_ROCK^^Increases how far targets are sent flying.@@STOMP_GRAVITY^^Increases how long targets remain affected by stasis.@@STOMP_SHRINK^^Increases how long targets remain shrunken."
                },
                {
                    0xFF098257,
                    "@@BUFF_FIRE^^Nearby homies become immune to fire while the buff is active.@@BLAST_FREEZE^^Nearby homies have a chance of firing freeze bullets.@@BUFF_LIGHTNING^^Nearby homies have a chance of firing lightning bullets."
                },
                {0xF84860B3, "Increases how long Buff remains active."},
                {0xD4D71330, "Increases walking speed while Buff is active."},
                {0xEC7D1920, "Increases the size of the area that Buff affects."},
                {
                    0x2785B9BF,
                    "@@BLAST_FREEZE^^Targets explode when shattered, damaging everything around them.@@BLAST_FIRE^^Targets explode when they die, spreading the fire to anyone nearby.@@BLAST_GLITCH^^Targets explode when they die, glitching nearby enemies."
                },
                {0x29E20B62, "Telekinesis uses less energy when throwing objects."},
                {
                    0x18C2DCFE,
                    "Lower the temperature around you and your bullets to absolute zero to freeze your enemies and deal extra damage.\n\nDouble tap {SUPER_QUICK_SHIELD_IMG} to quick-switch between Buff Elements. Or switch Elements in the Powers menu."
                },
                {0xBB4EC5AD, "Slightly increase the range at which health pickups will be sucked toward you."},
                {0x22479417, "Moderately increase the range at which health pickups will be sucked toward you."},
                {0x5540A481, "Greatly increases the range at which health pickups will be sucked toward you."},
                {0xBFAC8098, "Slightly increase the value of all health pickups."},
                {0x26A5D122, "Moderately increase the value of all health pickups."},
                {0x51A2E1B4, "Greatly increase the value of all health pickups."},
                {0xB7365520, "Train your homies to flinch less in response to getting shot."},
                {0x2E3F049A, "Train your homies to not flinch at all when getting shot."},
                {0xC4246256, "Pedestrians now drop health pickups when they die."},
                {0x7E75A4B9, "Remove some security protocols and allow the Mech Suit's weapons to recharge faster."},
                {
                    0xE77CF503,
                    "Remove even more security protocols and allow the Mech Suit's weapons to recharge even faster."
                },
                {0x907BC595, "Remove all the limiters and allow the Mech Suit's weapons to reach their full potential."},
                {0x08CAADF4, "Slightly decrease the cost of hovering while in the Mech Suit."},
                {0x91C3FC4E, "Moderately decrease the cost of hovering while in the Mech Suit."},
                {0xE6C4CCD8, "Greatly decrease the cost of hovering while in the Mech Suit."},
                {
                    0x35731CC1,
                    "Greatly increase the hovering speed of the Mech Suit. You should probably look into painting it red."
                },
                {
                    0x1EA2BB88,
                    "Harder instances of the FIGHT CLUB Activity are available in the simulation.\nCheck your MAP for locations."
                },
                {0x25478F3F, "Fun Shaundi can be found in your HUB  PHONE menu. She will help the Saints in combat."},
                {
                    0x7B02F740,
                    "Keith David can be called into the simulation from your HUB  PHONE menu. He will help the Saints in combat."
                },
                {
                    0x6A32F740,
                    "Matt can be found in your HUB  PHONE menu. He will (reluctantly) help the Saints in combat."
                },
                {
                    0xB4EC8650,
                    "Asha can be called into the simulation using your HUB  PHONE menu. She will help the Saints in combat."
                },
                {
                    0x8F4037B3,
                    "Gat can be called into the simulation using your HUB  PHONE menu. He will gladly help the Saints in combat."
                },
                {
                    0xD132DA77,
                    "Ben King can be called into the simulation using your HUB  PHONE menu. He will help the Saints in combat."
                },
                {
                    0xFB684FFF,
                    "Pierce is now super excellent! Pierce has reached a state of Zen necessary to take on Zinyak. Call Pierce from the HUB  PHONE menu to see him in his new gear!"
                },
                {
                    0x9AB7AB6D,
                    "Shaundi is now super powered! She is one with herself and ready to take on Zinyak. Call Shaundi from the HUB  PHONE menu to see her in her new gear!"
                },
                {0xB7385C73, "Fun Shaundi is now super fun!"},
                {
                    0xAD1D091D,
                    "Asha is now a super secret agent! She is geared up to take on Zinyak. Call Asha from the HUB  PHONE menu to see her in her new gear!"
                },
                {
                    0x3BE97E16,
                    "Ben King is now super powered! He's now ready to write the final chapter in Zinyak's book. Call Ben King from the HUB  PHONE menu to see him in his new gear!"
                },
                {
                    0x12F90B25,
                    "Killstreak unlocked, Johnny Gat is now super powered! Call Gat from the HUB  PHONE menu to see him in his new gear!"
                },
                {
                    0xB1848394,
                    "Kinzie is now super powered! Kinzie is now ready to crack the final code in Zinyak's mothership. Call Kinzie from the HUB  PHONE menu to see her in her new gear!"
                },
                {
                    0x73C3780D,
                    "No amount of canon can stop Matt from thinking of himself as a super hero! Call Matt from the HUB  PHONE menu to see him in his new gear!"
                },
                {
                    0x4CF91774,
                    "Fill the world with music and explosions with the Dubstep Gun. Equip it at your Gateway Weapon Cache."
                },
                {0x5F3CD54B, "Moderately extend the amount of time you can spend in the Mech Suit."},
                {
                    0x283BE5DD,
                    "Greatly extend the amount of time you can spend in the Mech Suit. You might want to bring some snacks for this one."
                },
                {
                    0xF73E5E4B,
                    "You can now devastate entire sections of the city with Nuke!\n\nUse Death From Above from a high altitude to create the nuclear blast!"
                },
                {
                    0x5FF08230,
                    "Jump onto the side of a building and hold {SPRINT_IMG} to sprint up the wall!\n\nA must-have for any super powered Saint."
                },
                {
                    0xC87EA2F9,
                    "Now if only you had the bagpipes to go with it. This outfit is now available at your Gateway Wardrobe."
                },
                {
                    0x4B5B3EC9,
                    "The Murderbot's miniguns have been reverse engineered. Sweep death across the battlefield with the Exterminator!\nEquip it at your Gateway Weapon Cache."
                },
                {
                    0x9494F72A,
                    "The Murderbot's minethrower has been reverse engineered. Cover the city in proximity mines with the Minethrower!\nEquip it at your Gateway Weapon Cache."
                },
                {
                    0xC1A99367,
                    "Roddy Piper can be found in your HUB  PHONE menu. Forget what Keith said, you can call Roddy any time for help."
                },
                {
                    0xE97D240C,
                    "Keith David is now super powered! Is there a role Keith David can't play? Call Keith David from the HUB  PHONE menu to see him in his new gear!"
                },
                {0xE8BCBFE0, "The Singularity Gun now holds twice as many black holes."},
                {0x71B5EE5A, "The Singularity Gun recharges its black holes far quicker."},
                {0x0D5B3FC2, "The Propaganda Truck is now available in your garage."},
                {0xA141FB22, "The Cyber Monster Truck is now available at your Gateway garage."},
                {0x846C6824, "The Wireframe Phantom is now available at your Gateway garage."},
                {0x941AB6FE, "The Wireframe Peacemaker is now available at your Gateway garage."},
                {0xF9A9EFF7, "The Wireframe Assert is now available at your Gateway garage."},
                {0xB59F1867, "The Cyber Estrada is now available at your Gateway garage."},
                {0xC24AA42E, "The Chrome Rattler is now available at your Gateway garage."},
                {0xD64B3BFB, "The Chrome Void is now available at your Gateway garage."},
                {0xEC4B23F3, "The Chrome Lockdown is now available at your Gateway garage."},
                {
                    0x0705495A,
                    "Harder instances of the TELEKINESIS MAYHEM Activity are available in the simulation.\nCheck your MAP for new locations."
                },
                {
                    0xE4170551,
                    "The A.I. has more Rifts hidden throughout the city. Find the Rifts and complete the challenges that the A.I. has laid out in your Quest Log for further rewards."
                },
                {0x03C07F5B, "The real American Dream."},
                {
                    0x6CE44F34,
                    "New Downloadable Content is available in your Gateway Weapon Cache, Wardrobe, and Virtual Garage."
                },
                {0x3DF539B1, "We want YOU to wear this outfit."},
                {0xE6291221, "Air Force One has nothing on this."},
                {0xCA934480, "New Downloadable Content available in your Gateway Wardrobe."},
                {0xEDCDC30D, "The grim metal visage will fill your enemies with fear!"},
                {0x74C492B7, "The Silver Age Princess costume as introduced in Queen Amazonia #128!"},
                {0x0F347A0E, "New Cheats are available in the HUB."},
                {0x700C4490, "Super Strength! Increases the power of your punches and how far you can throw people."},
                {0xE905152A, "Further increases the power of your punches and how far you can throw people."},
                {
                    0x9E0225BC,
                    "Fisticuffs much? Your punches are now very powerful, and you can throw people much further."
                },
                {0xA0EACD56, "Gliding uses less Stamina."},
                {0x39E39CEC, "Gliding uses far less Stamina."},
                {0x4EE4AC7A, "Gliding no longer uses Stamina."},
                {
                    0x8A4EBCF5,
                    "Loyalty missions become available in the QUESTS list when you rescue someone from the Simulation.\nCompleting these missions grants that person Super Powers in the Simulation. "
                },
                {
                    0xA94D6F23,
                    "The Ship is now available. Return to the Ship to talk to your crew, receive quests, listen to audio log entries, and read collected text adventures."
                },
                {
                    0x27D3EEFD,
                    "MI6 Agent Asha Odekar has been recovered and is now on the Ship! Visit her to learn how to get trained to use new weapons."
                },
                {
                    0x6BB0358E,
                    "The A.I. has been uploaded into a C.I.D. on the Ship. Visit the A.I. to learn how to further improve your super powers."
                },
                {
                    0xB9D7073E,
                    "Johnny Gat has been recovered and is now on the Ship! Visit him to learn how to create a weapon of mass destruction."
                },
                {
                    0xE6E62A8C,
                    "Chief of Staff Ben King has been recovered and can be found on the Ship. Visit him to learn how to further improve your super powers."
                },
                {
                    0xF90D9FED,
                    "MI6 Agent Matt Miller has been recovered and can now be found in the Ship. Visit him to learn how to further improve your super powers."
                },
                {
                    0x89F728E4,
                    "Vice President Keith David is now available on the Ship. Visit him to learn how to control the simulation."
                },
                {
                    0xD1948D6D,
                    "Kinzie is now available on the Ship. Visit her for quests that can further improve your super powers."
                },
                {
                    0x9B784106,
                    "Pierce has been recovered and is now on the Ship! Visit him to learn how to have a good time."
                },
                {
                    0x5EB6F1DB,
                    "Shaundi has been recovered and is now on the Ship! Visit her to help her reconcile with her past."
                },
                {
                    0x138B1F2B,
                    "Kinzie and Vice President Keith David are now available on the ship. Visit them to receive quests to learn how to damage the simulation and disrupt Zinyak's operation."
                },
                {0xFC50D921, "Restore health when you use your Ninja Reflexes."},
                {0xE53AA440, "Deep Silver weapon costume for the Shokolov AR."},
                {0x07659AE1, "100,000 Cache."},
                {0xC9A389F0, "10%% bonus to all XP earned."},
                {0xCE99C2F3, "10%% bonus to ALL Cache earned."},
                {0x005FD1E2, "10,000 XP."},
                {0x7C33F5FA, "Deep Silver weapon costume for the Guardsman AR."},
                {0x0B34C56C, "Deep Silver weapon costume for the Deacon 12-Gauge."},
                {0x955050CF, "Deep Silver weapon costume for the SWAT SMG."},
                {0xE2576059, "Deep Silver weapon costume for the Magna 10mm."},
                {0x7B5E31E3, "Deep Silver weapon costume for the McManus 2020."},
                {
                    0x248327FD, "Control the time of day of the simulation by accessing the computer terminal on the ship."
                },
                {0x5C996DF0, "Exactly what it says on the tin: just keep blasting away."},
                {0x29FE8E54, "You now have a Saints flavor alien Hover Bike available at your Gateway Garage."},
                {
                    0x9F173562,
                    "These came from that loveable psycho-Man Cat's personal stash:\n\nHomer, Kardak Lasershot."
                },
                {
                    0x5C38A5FB,
                    "Sure it isn't actually shooting flames, but I'm pretty sure the guys you're shooting don't care about that anymore."
                },
                {
                    0x07CF3DEA,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {0xCB1DECD1, "Fun Bob hairstyle."},
                {0x5214BD6B, "Hi Fauxhawk hairstyle."},
                {0x71C593BE, "You have unlocked the Bloody Mess cheat."},
                {0xE8CCC204, "You have unlocked the Golden Gun cheat."},
                {0x9FCBF292, "You have unlocked the Slow Motion cheat."},
                {
                    0xEE555B60,
                    "Pierce is now super excellent! Pierce has reached a state of Zen necessary to take on Zinyak. Call Pierce from the HUB  PHONE menu to see him in his new gear!"
                },
                {
                    0xE6BDD194,
                    "Shaundi is now super powered! She is one with herself and ready to take on Zinyak. Call Shaundi from the HUB  PHONE menu to see her in her new gear!"
                },
                {
                    0xBCE59857,
                    "Asha is now a super secret agent! She is geared up to take on Zinyak. Call Asha from the HUB  PHONE menu to see her in her new gear!"
                },
                {
                    0xA80BC7F3,
                    "Ben King is now super powered! He's now ready to write the final chapter in Zinyak's book. Call Ben King from the HUB  PHONE menu to see him in his new gear!"
                },
                {
                    0x4A0B6990,
                    "Killstreak unlocked, Johnny Gat is now super powered! Call Gat from the HUB  PHONE menu to see him in his new gear!"
                },
                {
                    0x9642C2C9,
                    "Kinzie is now super powered! Kinzie is now ready to crack the final code in Zinyak's mothership. Call Kinzie from the HUB  PHONE menu to see her in her new gear!"
                },
                {
                    0x972A3887,
                    "No amount of canon can stop Matt from thinking of himself as a super hero! Call Matt from the HUB  PHONE menu to see him in his new gear!"
                },
                {0xDB877270, "You can now have Wrestlers in your gang."},
                {
                    0xBC31C40C,
                    "Chapters of Zinyak's life have been scatted across virtual Steelport. Collect them all to learn Zinyak's backstory. Access collected entries from the rec room computer on the Ship."
                },
                {
                    0x17A702DD,
                    "Zinyak has placed statues all over virtual Steelport. Destroy them for bonus cache and XP."
                },
                {0x6BB847B2, "The Iron Saint outfit is now available at your Gateway Wardrobe."},
                {
                    0x90041119,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {
                    0x090D40A3,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {
                    0x7E0A7035,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {
                    0xE06EE596,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {
                    0x880F95DB,
                    "It is said that Midas had a love for guns:\n\nTyrant, Bounce Rifle, Abduct-o-matic, Disintegrator."
                },
                {
                    0x9769D500,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {
                    0x0E6084BA,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {
                    0x7967B42C,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                },
                {
                    0x6D8513B0,
                    "These came from that loveable psycho-Man Cat's personal stash:\n\nHomer, Kardak Lasershot."
                },
                {
                    0xF48C420A,
                    "These came from that loveable psycho-Man Cat's personal stash:\n\nHomer, Kardak Lasershot."
                },
                {
                    0x3D097288,
                    "Sure it isn't actually shooting flames, but I'm pretty sure the guys you're shooting don't care about that anymore."
                },
                {
                    0xA4002332,
                    "Sure it isn't actually shooting flames, but I'm pretty sure the guys you're shooting don't care about that anymore."
                },
                {
                    0xD30713A4,
                    "Sure it isn't actually shooting flames, but I'm pretty sure the guys you're shooting don't care about that anymore."
                },
                {0x4C97ABF9, "Unlimited special ammo"},
                {0xEB618A98, "Unlimited SMG ammo"},
                {0xEE52C7FB, "Unlimited shotgun ammo"},
                {0xF3A5E4E1, "Unlimited rifle ammo"},
                {0x512F469D, "Unlimited pistol ammo"},
                {0xB0AB4B8C, "Unlimited explosive ammo"},
                {
                    0xE9D8A9BD,
                    "It is said that Midas had a love for guns:\n\nZ9 Handcannon, Xenoblaster, Thumpgun, Dominator."
                }
            };
        internal static Dictionary<uint, string> UnlockablesNames = new Dictionary<uint, string>
            {
                {0x95D074EE, "BUFF ELEMENT - LIFE STEAL"},
                {0x496AB96D, "DISCOUNT - VEHICLES"},
                {0xBA264E0F, "DISCOUNT - WEAPONS"},
                {0x8F7735BE, "NOTORIETY WIPE - ALL"},
                {0xA2D2F0B8, "CLOTHING STORE DISCOUNT"},
                {0xB5FFA4E1, "BONUS - XP"},
                {0x13007154, "BONUS - CASH"},
                {0xDF1943E4, "BUFF - PASSIVE AGGRESSIVE"},
                {0x17274D6D, "TELEKINESIS - CATCH"},
                {0xC8394568, "EXPLOSIONS - NO RAGDOLL"},
                {0x5B7B59EF, "IMMOVABLE OBJECT"},
                {0x747ED2BB, "DAMAGE - FIRE 4"},
                {0x8E8C8D51, "TELEKINESIS - POWER UP"},
                {0x1785DCEB, "TELEKINESIS - POWER UP 2"},
                {0x07F46A99, "GLIDE - DISTANCE"},
                {0xEFABC4AB, "JUMP - DISTANCE"},
                {0xD464AAE2, "SPRINT - WATER RUNNING"},
                {0xCEF219F6, "SPRINT - HASTE"},
                {0x9F645A53, "HOMIE - GENKI BACKUP"},
                {0x4ED97058, "GATEWAY CACHE AMMO"},
                {0xD7D021E2, "GATEWAY CACHE AMMO 2"},
                {0x201D0636, "WEAPON - ABDUCT-O-MATIC"},
                {0x2E19ECD6, "VEHICLE - MONSTER TRUCKS"},
                {0xEEF80FAF, "HOMIE - SUPER BACKUP"},
                {0x134F5337, "BONUS - FLASHPOINT REWARD"},
                {0x49E0B158, "SPRINT - UNLIMITED SPRINTING"},
                {0x383554FE, "VEHICLE - CYBER GUNSLINGER"},
                {0xF6EDC7C7, "BONUS - HACKING REWARD"},
                {0xF883FEB3, "STAMINA - INCREASE"},
                {0x618AAF09, "STAMINA - INCREASE 2"},
                {0x168D9F9F, "STAMINA - INCREASE 3"},
                {0xDA97CA35, "GANG - FOLLOWERS"},
                {0x439E9B8F, "GANG - FOLLOWERS 2"},
                {0x3499AB19, "GANG - RECRUITMENT"},
                {0xAF7FE7A1, "GANG CUSTOMIZATION"},
                {0x5AC1D681, "GANG CUSTOMIZATION"},
                {0x27AB6FC7, "GANG CUSTOMIZATION"},
                {0x464010DD, "GANG CUSTOMIZATION"},
                {0x712F8B68, "GANG CUSTOMIZATION"},
                {0x7E91F271, "GANG CUSTOMIZATION"},
                {0xCD041A3D, "GANG CUSTOMIZATION"},
                {0x4B9959D5, "GATEWAY - EAST STANFIELD"},
                {0x4D90A4DB, "GATEWAY - EAST CARVER ISLAND"},
                {0xD317F79A, "GATEWAY - EAST NEW COLVIN"},
                {0x3EAF9590, "GATEWAY - CENTRAL DOWNTOWN"},
                {0x837E7C28, "GATEWAY - NORTH DOWNTOWN"},
                {0x85EC8FB1, "HOMIE - TANYA"},
                {0xE005616F, "HOMIE - MAERO"},
                {0x8954359C, "HOMIE - KINZIE KENSINGTON"},
                {0x41D2A114, "HOMIE - JULIUS"},
                {0x25DF2B1B, "HOMIE - DJ VETERAN CHILD"},
                {0x88B1DF03, "HOMIE - CYRUS TEMPLE"},
                {0x5D270503, "HOMIE - C.I.D."},
                {0xDB3C32C6, "MECH SUIT"},
                {0x9454F3E9, "HOMIE - SHAUNDI"},
                {0xC3B8F9F7, "HOMIE - PIERCE WASHINGTON"},
                {0x71DE8763, "AUDIO LOGS"},
                {0x1D77E524, "BONUS - CACHE BOOST"},
                {0x847EB49E, "BONUS - CACHE BOOST 2"},
                {0xF3798408, "BONUS - CACHE BOOST 3"},
                {0x45CE9793, "SKILL - SCAVENGER"},
                {0x3FA8EF63, "HEALTH - UPGRADE"},
                {0xA6A1BED9, "HEALTH - UPGRADE 2"},
                {0xD1A68E4F, "HEALTH - UPGRADE 3"},
                {0x4FC21BEC, "HEALTH - UPGRADE 4"},
                {0x16F53E13, "GANG - REVIVE TIMER"},
                {0x8FFC6FA9, "GANG - REVIVE TIMER 2"},
                {0xC61CB72B, "BONUS - CACHE TRANSFER RATE"},
                {0x5F15E691, "BONUS - CACHE TRANSFER RATE 2"},
                {0x2812D607, "BONUS - CACHE TRANSFER RATE 3"},
                {0x95B3AE06, "SKILL - NITROUS"},
                {0x4B133C83, "AMMO - SMG"},
                {0xD21A6D39, "AMMO - SMG 2"},
                {0xA51D5DAF, "AMMO - SMG 3"},
                {0x811EA413, "AMMO - SHOTGUN"},
                {0x1817F5A9, "AMMO - SHOTGUN 2"},
                {0x6F10C53F, "AMMO - SHOTGUN 3"},
                {0xDA910C8C, "AMMO - RIFLE"},
                {0x43985D36, "AMMO - RIFLE 2"},
                {0x349F6DA0, "AMMO - RIFLE 3"},
                {0x464AEBAF, "AMMO - PISTOL"},
                {0xDF43BA15, "AMMO - PISTOL 2"},
                {0xA8448A83, "AMMO - PISTOL 3"},
                {0x8E701A72, "STAMINA - INCREASE 4"},
                {0xE3EFB696, "SKILL - PICKPOCKET"},
                {0xFCE59D8B, "DAMAGE - SMALL ARMS"},
                {0x65ECCC31, "DAMAGE - SMALL ARMS 2"},
                {0x12EBFCA7, "DAMAGE - SMALL ARMS 3"},
                {0xDCE83355, "DAMAGE - EXPLOSIVE"},
                {0x45E162EF, "DAMAGE - EXPLOSIVE 2"},
                {0x32E65279, "DAMAGE - EXPLOSIVE 3"},
                {0x04142634, "DAMAGE - FIRE"},
                {0x52DADA8D, "DUAL WIELD - PISTOLS"},
                {0x70AE48F9, "DUAL WIELD - SMGS"},
                {0x3EB11B90, "SWAP - CACHE FOR XP"},
                {0xA7B84A2A, "SWAP - CACHE FOR XP 2"},
                {0xD0BF7ABC, "SWAP - CACHE FOR XP 3"},
                {0xBDD6F187, "VEHICLE CUSTOMIZE"},
                {0xF4ABE2FD, "BONUS - XP"},
                {0x9D1D778E, "DAMAGE - FIRE 2"},
                {0xEA1A4718, "DAMAGE - FIRE 3"},
                {0x0EA99DE5, "NOTORIETY - FASTER DECAY"},
                {0x97A0CC5F, "NOTORIETY - FASTER DECAY 2"},
                {0xE0A7FCC9, "NOTORIETY - FASTER DECAY 3"},
                {0x75E544CD, "GANG - WEAPONS - SMGS"},
                {0xC1E8602D, "GANG - WEAPONS - SHOTGUNS"},
                {0x48B53CC3, "GANG - WEAPONS - RIFLES"},
                {0x729F596F, "HOMIE - SAINTS BACKUP"},
                {0x776A984A, "REVIVE - SPEED"},
                {0xE7B23F83, "GANG CUSTOMIZATION"},
                {0x6DA2B347, "BONUS - XP 2"},
                {0x1AA583D1, "BONUS - XP 3"},
                {0xDB8ECEDB, "VEHICLE - WATERCRAFT"},
                {0x83F64DF6, "HOMIE - HEALTH INCREASE 1"},
                {0x1AFF1C4C, "HOMIE - HEALTH INCREASE 2"},
                {0x6DF82CDA, "HOMIE - HEALTH INCREASE 3"},
                {0xBC5BF92A, "AMMO - EXPLOSIVE 3"},
                {0xCB5CC9BC, "AMMO - EXPLOSIVE 2"},
                {0x52559806, "AMMO - EXPLOSIVE"},
                {0x1922280B, "AMMO - SPECIAL 3"},
                {0x6E25189D, "AMMO - SPECIAL 2"},
                {0xF72C4927, "AMMO - SPECIAL"},
                {0xCCEA827A, "COLLECTIBLE FINDER"},
                {0xA8B04439, "UPGRADES"},
                {0x81EEE65C, "TANK MAYHEM"},
                {0x1151FBCD, "BLAZIN"},
                {0xD7B441DA, "PROF. GENKI'S M.O.M."},
                {0x6A84BBCB, "MAYHEM"},
                {0xE95053C8, "FRAUD"},
                {0xA0AAF9CB, "BLAZIN"},
                {0x0702E57D, "JUMP - GLIDE"},
                {0x2F803673, "SPRINT - REDUCED DAMAGE"},
                {0x96EA63BD, "TELEKINESIS - SPECIAL VEHICLES"},
                {0x7B986FAF, "STOMP - RECHARGE"},
                {0xE2913E15, "STOMP - RECHARGE 2"},
                {0x95960E83, "STOMP - RECHARGE 3"},
                {0xFB23FAFA, "TELEKINESIS - RECHARGE"},
                {0x622AAB40, "TELEKINESIS - RECHARGE 2"},
                {0x152D9BD6, "TELEKINESIS - RECHARGE 3"},
                {0xF1EB19B6, "HACKING - EXTRA TIME"},
                {0xB87DF4AF, "SUPER SPRINT"},
                {0xCAF554E5, "TELEKINESIS"},
                {0x496E73F0, "DASH SHIELD"},
                {0xC5AEFF50, "STOMP"},
                {0x692792B1, "SUPER JUMP"},
                {0x918990F8, "JUMP - AIR DASH"},
                {0x0880C142, "JUMP - AIR DASH 2"},
                {0x7F87F1D4, "JUMP - AIR DASH 3"},
                {0x116C5502, "BLAST"},
                {0xBBB3D6C4, "BUFF"},
                {0x67615E53, "DEATH FROM ABOVE"},
                {0xB202AB6B, "VEHICLE - UFO"},
                {0xBBCEC998, "VEHICLE - CYRUS' VTOL"},
                {0xF150FD03, "VEHICLE - GENKIMOBILE"},
                {0x52201E31, "VEHICLES - GENKI"},
                {0x0FF84B36, "VEHICLES - CYBERSPACE"},
                {0x5C6CBA2A, "VEHICLE - CYBER TANK"},
                {0x983E1C6C, "UFO MAYHEM"},
                {0x3CAB7420, "MECH SUIT MAYHEM"},
                {0x83A745F7, "VEHICLE - HOVER TANK"},
                {0xFB2A24D4, "VEHICLE - GENKIMOBILE"},
                {0xFA41996D, "WEAPON - TYRANT"},
                {0x16A39BE4, "SPRINT - TORNADO"},
                {0x5D0C55A9, "THE RECTIFIER"},
                {0x3A4D439E, "WEAPON - THE RECTIFIER"},
                {0xB4C430FD, "SHIELD - DAMAGE"},
                {0x308D7277, "WEAPON - DISINTEGRATOR"},
                {0x85C5E278, "WEAPON - BOUNCE RIFLE"},
                {0x7A081ADE, "WEAPON - VIOLATOR"},
                {0x7FB3E6A6, "BLAST - RECHARGE"},
                {0xE6BAB71C, "BLAST - RECHARGE 2"},
                {0x91BD878A, "BLAST - RECHARGE 3"},
                {0x9CF9C858, "BLAST - DURATION"},
                {0x342195CD, "BLAST - DAMAGE"},
                {0x8B490E75, "TELEKINESIS - RECHARGE 4"},
                {0x15879046, "TELEKINESIS - HOLD COST"},
                {0x5759E9DE, "JUMP - HEIGHT"},
                {0x82B27823, "NITROUS - DAMAGE"},
                {0x6D9D487D, "SUPPRESSION - POWER RECOVERY"},
                {0x74004C2F, "NINJA REFLEXES"},
                {0xC2BB3548, "STOMP - AREA"},
                {0xF49419C7, "SUPPRESSION - POWER RECOVERY 2"},
                {0x83932951, "SUPPRESSION - POWER RECOVERY 3"},
                {0xC63584F1, "MECH SUIT - DURATION"},
                {0xA72BE0E3, "TELEKINESIS ELEMENT - LIGHTNING"},
                {0xDDBAB5EC, "TELEKINESIS ELEMENT - LIFE STEAL"},
                {0x4CFAAD54, "STOMP ELEMENT - GRAVITY"},
                {0xB63542F8, "STOMP ELEMENT - SHRINK"},
                {0xD56B5825, "BLAST ELEMENT - FIRE"},
                {0x19BA4848, "BLAST ELEMENT - MIND CONTROL"},
                {0x7FDCA944, "BUFF ELEMENT - LIGHTNING"},
                {0xE851A3F3, "TELEKINESIS ELEMENT - FORCE"},
                {0x0C55C72E, "STOMP ELEMENT - ROCK"},
                {0xB4C4141E, "BLAST ELEMENT - FREEZE"},
                {0xA35C7F5A, "BUFF ELEMENT - FIRE"},
                {0xCE50B864, "JUMP - HEIGHT 2"},
                {0xB95788F2, "JUMP - HEIGHT 3"},
                {0x89DE009A, "SPRINT - SPEED"},
                {0x10D75120, "SPRINT - SPEED 2"},
                {0x67D061B6, "SPRINT - SPEED 3"},
                {0xDDDCD93B, "BLAST - AREA"},
                {0x44D58881, "BLAST - AREA 2"},
                {0xDAF86527, "STOMP - DISTANCE"},
                {0x43F1349D, "STOMP - DISTANCE 2"},
                {0x57DB19FD, "TELEKINESIS - THROW DISTANCE"},
                {0x0BF29B20, "STOMP - RECHARGE 4"},
                {0xA97793B3, "BUFF - RECHARGE"},
                {0x307EC209, "BUFF - RECHARGE 2"},
                {0x4779F29F, "BUFF - RECHARGE 3"},
                {0xD91D673C, "BUFF - RECHARGE 4"},
                {0x5A6D0C87, "GATEWAY - SOUTH DOWNTOWN"},
                {0xB30EA9B2, "GATEWAY - WEST STANFIELD"},
                {0x5D00C89E, "GATEWAY - SOUTH NEW COLVIN"},
                {0xCDBFD50F, "GATEWAY - WEST CARVER ISLAND"},
                {0xDD12A865, "GATEWAY - ARAPICE ISLAND"},
                {0x0628A8D1, "HACK - VEHICLE DELIVERY"},
                {0x56481333, "ADORATION UNLOCKED"},
                {0xB501DC47, "PRESIDENCY UNLOCKED"},
                {0xE641009A, "ADVISOR UNLOCKED"},
                {0xC58A5504, "ADVISOR UNLOCKED"},
                {0x34F6040B, "STOMP - DISTANCE 3"},
                {0xCED24847, "TELEKINESIS - THROW DISTANCE 2"},
                {0x33D2B817, "BLAST - AREA 3"},
                {0xB9D578D1, "TELEKINESIS - THROW DISTANCE 3"},
                {0x0FD91229, "BLAST - RECHARGE 4"},
                {0x2575C252, "WEAPON - INFLATO-RAY"},
                {0xD3365F86, "INFLATO-RAY UPGRADE"},
                {0xABC3C976, "WEAPON - ENERGY SWORD"},
                {0x4D9143D6, "WEAPON - SINGULARITY GUN"},
                {0x2D847E46, "WEAPON - ENERGY SWORD HEALTH"},
                {0x342BCBD0, "NFLATO-RAY COSTUME"},
                {0x89550D1D, "ENERGY SWORD COSTUME"},
                {0xF8550F41, "OUTFIT - BEN KING SUIT"},
                {0x2A485D90, "OUTFIT - FUN SHAUNDI SUIT"},
                {0x4B6DDADD, "OUTFIT - CID SUIT"},
                {0x0B4F424C, "OUTFIT - MATT MILLER SUIT"},
                {0x4928B67A, "STOMP - DAMAGE"},
                {0x951AFC5C, "STOMP - DURATION"},
                {0xFF098257, "BUFF - TEAM PLAYER"},
                {0xF84860B3, "BUFF - DURATION"},
                {0xD4D71330, "BUFF - SPEED DEMON"},
                {0xEC7D1920, "BUFF - AREA"},
                {0x2785B9BF, "BLAST - EXPLOSIVE DEATHS"},
                {0x29E20B62, "TELEKINESIS - THROW COST"},
                {0x18C2DCFE, "BUFF ELEMENT - FREEZE"},
                {0xBB4EC5AD, "HEALTH - VACUUM"},
                {0x22479417, "HEALTH - VACUUM 2"},
                {0x5540A481, "HEALTH - VACUUM 3"},
                {0xBFAC8098, "HEALTH - PICKUPS VALUE"},
                {0x26A5D122, "HEALTH - PICKUPS VALUE 2"},
                {0x51A2E1B4, "HEALTH - PICKUPS VALUE 3"},
                {0xB7365520, "HOMIE - TOUGHEN UP"},
                {0x2E3F049A, "HOMIE - TOUGHEN UP 2"},
                {0xC4246256, "HEALTH - PEDESTRIAN CARNAGE"},
                {0x7E75A4B9, "MECH SUIT - OVERCLOCK"},
                {0xE77CF503, "MECH SUIT - OVERCLOCK 2"},
                {0x907BC595, "MECH SUIT - OVERCLOCK 3"},
                {0x08CAADF4, "MECH SUIT - HOVER COST"},
                {0x91C3FC4E, "MECH SUIT - HOVER COST 2"},
                {0xE6C4CCD8, "MECH SUIT - HOVER COST 3"},
                {0x35731CC1, "MECH SUIT - HOVER SPEED"},
                {0x1EA2BB88, "FIGHT CLUB"},
                {0x25478F3F, "HOMIE - FUN SHAUNDI"},
                {0x7B02F740, "HOMIE - KEITH DAVID"},
                {0x6A32F740, "HOMIE - MATT MILLER"},
                {0xB4EC8650, "HOMIE - ASHA ODEKAR"},
                {0x8F4037B3, "HOMIE - JOHNNY GAT"},
                {0xD132DA77, "HOMIE - BEN KING"},
                {0xFB684FFF, "SUPER HOMIE - PIERCE"},
                {0x9AB7AB6D, "SUPER HOMIE - SHAUNDI"},
                {0xB7385C73, "SUPER HOMIE - FUN SHAUNDI"},
                {0xAD1D091D, "SUPER HOMIE - ASHA"},
                {0x3BE97E16, "SUPER HOMIE - BEN KING"},
                {0x12F90B25, "SUPER HOMIE - JOHNNY GAT"},
                {0xB1848394, "SUPER HOMIE - KINZIE"},
                {0x73C3780D, "SUPER HOMIE - MATT MILLER"},
                {0x4CF91774, "WEAPON - DUBSTEP GUN"},
                {0x5F3CD54B, "MECH SUIT - DURATION 2"},
                {0x283BE5DD, "MECH SUIT - DURATION 3"},
                {0xF73E5E4B, "DEATH FROM ABOVE - NUKE"},
                {0x5FF08230, "SPRINT - WALL RUNNING"},
                {0xC87EA2F9, "OUTFIT - RODDY SUIT"},
                {0x4B5B3EC9, "WEAPON - MURDERBOT MINIGUN"},
                {0x9494F72A, "WEAPON - MURDERBOT MINETHROWER"},
                {0xC1A99367, "HOMIE - RODDY PIPER"},
                {0xE97D240C, "SUPER HOMIE - KEITH DAVID"},
                {0xE8BCBFE0, "SINGULARITY GUN AMMO UPGRADE"},
                {0x71B5EE5A, "SINGULARITY GUN RECHARGE UPGRADE"},
                {0x0D5B3FC2, "VEHICLE - PROPAGANDA TRUCK"},
                {0xA141FB22, "VEHICLE - CYBER MONSTER TRUCK"},
                {0x846C6824, "VEHICLE - WIREFRAME PHANTOM"},
                {0x941AB6FE, "VEHICLE - WIREFRAME PEACEMAKER"},
                {0xF9A9EFF7, "VEHICLE - WIREFRAME ASSERT"},
                {0xB59F1867, "VEHICLE - CYBER ESTRADA"},
                {0xC24AA42E, "VEHICLE - CHROME RATTLER"},
                {0xD64B3BFB, "VEHICLE - CHROME VOID"},
                {0xEC4B23F3, "VEHICLE - CHROME LOCKDOWN"},
                {0x0705495A, "TELEKINESIS MAYHEM"},
                {0xE4170551, "RIFTS"},
                {0x03C07F5B, "WEAPON - 'MERICA WEAPON"},
                {0x6CE44F34, "COMMANDER-IN-CHIEF PACK"},
                {0x3DF539B1, "OUTFIT - UNCLE SAM SUIT"},
                {0xE6291221, "VEHICLE - SCREAMIN' EAGLE JET"},
                {0xCA934480, "VOLITION COMICS PACK"},
                {0xEDCDC30D, "OUTFIT - THE IRON ROGUE"},
                {0x74C492B7, "OUTFIT - QUEEN AMAZONIA"},
                {0x0F347A0E, "EXECUTIVE PRIVILEGE PACK"},
                {0x700C4490, "MELEE - SUPER STRENGTH"},
                {0xE905152A, "MELEE - SUPER STRENGTH 2"},
                {0x9E0225BC, "MELEE - SUPER STRENGTH 3"},
                {0xA0EACD56, "GLIDE - EFFICIENCY"},
                {0x39E39CEC, "GLIDE - EFFICIENCY 2"},
                {0x4EE4AC7A, "GLIDE - EFFICIENCY 3"},
                {0x8A4EBCF5, "LOYALTY MISSIONS"},
                {0xA94D6F23, "THE SHIP"},
                {0x27D3EEFD, "CREWMATE - ASHA ODEKAR"},
                {0x6BB0358E, "CREWMATE - C.I.D."},
                {0xB9D7073E, "CREWMATE - JOHNNY GAT"},
                {0xE6E62A8C, "CREWMATE - BEN KING"},
                {0xF90D9FED, "CREWMATE - MATT MILLER"},
                {0x89F728E4, "CREWMATE - KEITH DAVID"},
                {0xD1948D6D, "CREWMATE - KINZIE KENSINGTON"},
                {0x9B784106, "CREWMATE - PIERCE WASHINGTON"},
                {0x5EB6F1DB, "CREWMATE - SHAUNDI"},
                {0x138B1F2B, "CREWMATES - KINZIE AND KEITH"},
                {0xFC50D921, "NINJA REFLEXES - HEALTH"},
                {0xE53AA440, "SILVERLINK - WEAPON COSTUME"},
                {0x07659AE1, "SILVERLINK - CACHE"},
                {0xC9A389F0, "SILVERLINK - XP BOOST"},
                {0xCE99C2F3, "SILVERLINK - CACHE BOOST"},
                {0x005FD1E2, "SILVERLINK - XP"},
                {0x7C33F5FA, "SILVERLINK - WEAPON COSTUME"},
                {0x0B34C56C, "SILVERLINK - WEAPON COSTUME"},
                {0x955050CF, "SILVERLINK - WEAPON COSTUME"},
                {0xE2576059, "SILVERLINK - WEAPON COSTUME"},
                {0x7B5E31E3, "SILVERLINK - WEAPON COSTUME"},
                {0x248327FD, "TIME OF DAY"},
                {0x5C996DF0, "WEAPON - SEMI-AUTO SHOTGUN"},
                {0x29FE8E54, "VEHICLE - SAINTS XOR"},
                {0x9F173562, "GENKI WEAPON COSTUMES"},
                {0x5C38A5FB, "FLAME WEAPON COSTUMES"},
                {0x07CF3DEA, "GOLDEN WEAPON COSTUMES"},
                {0xCB1DECD1, "SILVERLINK - HAIRSTYLE"},
                {0x5214BD6B, "SILVERLINK - HAIRSTYLE"},
                {0x71C593BE, "SILVERLINK - CHEAT"},
                {0xE8CCC204, "SILVERLINK - CHEAT"},
                {0x9FCBF292, "SILVERLINK - CHEAT"},
                {0xEE555B60, "SUPER HOMIE - PIERCE"},
                {0xE6BDD194, "SUPER HOMIE - SHAUNDI"},
                {0xBCE59857, "SUPER HOMIE - ASHA"},
                {0xA80BC7F3, "SUPER HOMIE - BEN KING"},
                {0x4A0B6990, "SUPER HOMIE - JOHNNY GAT"},
                {0x9642C2C9, "SUPER HOMIE - KINZIE"},
                {0x972A3887, "SUPER HOMIE - MATT MILLER"},
                {0xDB877270, "GANG CUSTOMIZATION"},
                {0xBC31C40C, "TEXT ADVENTURE"},
                {0x17A702DD, "ZINYAK STATUES"},
                {0x6BB847B2, "OUTFIT - IRON SAINT"},
                {0x90041119, "GOLDEN WEAPON COSTUMES"},
                {0x090D40A3, "GOLDEN WEAPON COSTUMES"},
                {0x7E0A7035, "GOLDEN WEAPON COSTUMES"},
                {0xE06EE596, "GOLDEN WEAPON COSTUMES"},
                {0x880F95DB, "GOLDEN WEAPON COSTUMES"},
                {0x9769D500, "GOLDEN WEAPON COSTUMES"},
                {0x0E6084BA, "GOLDEN WEAPON COSTUMES"},
                {0x7967B42C, "GOLDEN WEAPON COSTUMES"},
                {0x6D8513B0, "GENKI WEAPON COSTUMES"},
                {0xF48C420A, "GENKI WEAPON COSTUMES"},
                {0x3D097288, "FLAME WEAPON COSTUMES"},
                {0xA4002332, "FLAME WEAPON COSTUMES"},
                {0xD30713A4, "FLAME WEAPON COSTUMES"},
                {0x4C97ABF9, "AMMO - SPECIAL 4"},
                {0xEB618A98, "AMMO - SMG 4"},
                {0xEE52C7FB, "AMMO - SHOTGUN 4"},
                {0xF3A5E4E1, "AMMO - RIFLE 4"},
                {0x512F469D, "AMMO - PISTOL 4"},
                {0xB0AB4B8C, "AMMO - EXPLOSIVE 4"},
                {0xE9D8A9BD, "GOLDEN WEAPON COSTUMES"}
            };

    }
}
