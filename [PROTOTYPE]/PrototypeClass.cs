using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors._PROTOTYPE_
{
    /// <summary>
    /// This class will handle our PROTOTYPE gamesaves
    /// </summary>
    public class PrototypeClass
    {
        //Gotta finish up Skills and Unlocks, then do UI..

        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values

        //Difficulty Option
        public DifficultyOption DIFFICULTY { get; set; }

        /*
         * 0x2524 - freL02;freL01 [null term]
0x2E6C - missions complete? e01m01 [null term] [0x20 per entry]

0x560 - int - weather/Scene related?
0x6BC - int - missionsCompleted
         * */
        public string[] C_MISSION_VALUES = {
                                                    "e01m01", "e01m02", "e02m01",
                                                    "e02m02", "e02m05", "e03m01",
                                                    "e03m02", "e04m01", "e04m02",
                                                    "e04m03", "e04m04", "e04m05",
                                                    "e05m01", "e05m02", "e05m03",
                                                    "e06m01", "e06m02", "e06m03",
                                                    "e06m04", "e06m05", "e07m01",
                                                    "e07m02", "e07m04", "e08m02",
                                                    "e08m03", "e08m04", "e09m01",
                                                    "e09m03", "e10m04", "e11m01"
                                                };
        public string[] C_MISSION_NAMES = {
                                              "INTRO",
                                              "MEMORY IN DEATH", "UNEXPECTED FAMILY", "PAST AND PRESENT",
                                              "BEHIND THE GLASS", "THE WHEEL OF CHANCE", "A NEW ORDER",
                                              "OPEN CONSPIRACY", "IN THE WEB", "THE ALTERED WORLD",
                                              "ERRAND BOY", "CONFESSIONS", "UNDER THE KNIFE",
                                              "THE STOLEN BODY", "BIOLOGICAL IMPERATIVE", "THE DOOR IN THE WALL",
                                              "FIRST AND LAST THINGS", "THE FIRST MONSTER", "MAKING THE FUTURE",
                                              "THE SUPREME HUNTER", "MEN LIKE GODS", "A DREAM OF ARMAGEDDON",
                                              "THE WORLD SET FREE", "THINGS TO COME", "DEFEAT ELIZABETH GREENE",
                                              "SHOCK AND AWE", "THE LAST MAN", "TWO TICKETS", "ONE THOUSAND SUNS", 
                                              "END"
                                          };

        //General Settings
        public int GENERAL_ALERTS_ESCAPED { get; set; }
        public int GENERAL_DEATHS { get; set; }
        public int GENERAL_EP_POINTERS { get; set; }
        public int GENERAL_LANDMARKS { get; set; }
        public int GENERAL_HINTS { get; set; }
        public int GENERAL_TOTAL_EP_COLLECTED { get; set; }
        public int GENERAL_WEB_OF_INTRIGUE_NODES { get; set; }
        public int GENERAL_DEATHS_BY_MILITARY { get; set; }
        public int GENERAL_DEATHS_BY_INFECTED { get; set; }
        public int GENERAL_STRIKE_TEAMS_DESTROYED { get; set; }
        public int GENERAL_ALERTS_CAUSED { get; set; }
        public int GENERAL_STRIKE_TEAMS_EVADED { get; set; }

        public int INFECTED_HYDRAS_KILLED { get; set; }
        public int INFECTED_LEADERS_KILLED { get; set; }
        public int INFECTED_HUNTERS_KILLED { get; set; }
        public int INFECTED_INFECTED_WATERTOWERS_DESTROYED { get; set; }
        public int INFECTED_INFECTED_HIVES_DESTROYED { get; set; }
        public int INFECTED_INFECTED_CIVILIANS_KILLED { get; set; }
        public int INFECTED_EVOLVED_INFECTED_KILLED { get; set; }

        public int MILITARY_BLACKWATCH_COMMANDERS_KILLED { get; set; }
        public int MILITARY_PILOTS_KILLED { get; set; }
        public int MILITARY_BLACKWATCH_BASES_DESTROYED { get; set; }
        public int MILITARY_COMMANDERS_KILLED { get; set; }
        public int MILITARY_BASES_INFILTRATED { get; set; }
        public int MILITARY_MILITARY_PATSIED { get; set; }
        public int MILITARY_VIRAL_DETECTORS_DESTROYED { get; set; }
        public int MILITARY_BLACKWATCH_TROOPERS_KILLED { get; set; }
        public int MILITARY_UAVS_DESTROYED { get; set; }
        public int MILITARY_SCIENTISTS_KILLED { get; set; }
        public int MILITARY_GUN_TURRETS_DESTROYED { get; set; }
        public int MILITARY_MARINES_KILLED { get; set; }
        public int MILITARY_SUPERSOLDIERS_KILLED { get; set; }

        public int VEHICLES_ARMOR_DESTROYED_WHILE_IN_HELICOPTERS { get; set; }
        public int VEHICLES_CIVILIAN_VEHICLES_DESTROYED { get; set; }
        public int VEHICLES_TANKS_DESTROYED { get; set; }
        public int VEHICLES_TOTAL_VEHICLES_DESTROYED { get; set; }
        public int VEHICLES_APCS_DESTROYED { get; set; }
        public int VEHICLES_MILITARY_TRUCKS_DESTROYED { get; set; }
        public int VEHICLES_TRANSPORTS_DESTROYED { get; set; }
        public int VEHICLES_CARS_CRUSHED_WITH_TANK { get; set; }
        public int VEHICLES_GUNSHIPS_DESTROYED { get; set; }
        public int VEHICLES_THERMOBARIC_TANKS_DESTROYED { get; set; }
        public int VEHICLES_HELICOPTERS_DESTROYED_USING_A_HELICOPTER { get; set; }
        public int VEHICLES_HELICOPSTERS_DESTROYED_WHILE_IN_ARMOR { get; set; }

        public int CONSUMES_STEALTH_CONSUMES { get; set; }
        public int CONSUMES_SCIENTISTS_CONSUMED { get; set; }
        public int CONSUMES_TOTAL_CONSUMES { get; set; }
        public int CONSUMES_BLACKWATCH_TROOPERS_CONSUMED { get; set; }
        public int CONSUMES_INFECTED_CONSUMED { get; set; }
        public int CONSUMES_BLACKWATCH_COMMANDERS_CONSUMED { get; set; }
        public int CONSUMES_HUNTERS_CONSUMED { get; set; }
        public int CONSUMES_LEADERS_CONSUMED { get; set; }
        public int CONSUMES_PEDESTRIANS_CONSUMED { get; set; }
        public int CONSUMES_PILOTS_CONSUMED { get; set; }
        public int CONSUMES_MARINES_CONSUMED { get; set; }
        public int CONSUMES_MARINE_COMMANDERS_CONSUMED { get; set; }
        public int CONSUMES_EVOLVED_INFECTED_CONSUMED { get; set; }

        public int HIJACKS_TANKS_HIJACKED { get; set; }
        public int HIJACKS_TRANSPORTS_HIJACKED { get; set; }
        public int HIJACKS_TOTAL_VEHICLES_HIJACKED { get; set; }
        public int HIJACKS_THERMOBARIC_TANKS_HIJACKED { get; set; }
        public int HIJACKS_GUNSHIPS_HIJACKED { get; set; }
        public int HIJACKS_APCS_HIJACKED { get; set; }

        public int KILLS_DISMEMBERMENTS { get; set; }
        public int KILLS_USING_TANKS { get; set; }
        public int KILLS_SPEED_BUMPS { get; set; }
        public int KILLS_USING_HELICOPTERS { get; set; }
        public int KILLS_USING_APCS { get; set; }
        public int KILLS_USING_THERMOBARIC_TANK { get; set; }
        public int KILLS_PEDESTRIANS_KILLED { get; set; }

        public int WEAPONS_GRENADE_SHOTS { get; set; }
        public int WEAPONS_HOMING_MISSILE_SHOTS { get; set; }
        public int WEAPONS_MACHINE_GUN_SHOTS { get; set; }
        public int WEAPONS_MISSILE_SHOTS { get; set; }
        public int WEAPONS_ASSAULT_RIFLE_SHOTS { get; set; }
        #endregion

        #region Constructor

        public PrototypeClass(EndianIO io)
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
            //Read our difficulty
            IO.In.BaseStream.Position = 0x1930;
            DIFFICULTY = (DifficultyOption)IO.In.ReadInt32();

            //Read our general stats.
            IO.In.BaseStream.Position = 0x2DF8;
            GENERAL_ALERTS_ESCAPED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xA44;
            GENERAL_DEATHS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xD00;
            GENERAL_EP_POINTERS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1558;
            GENERAL_LANDMARKS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1A60;
            GENERAL_HINTS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1C7C;
            GENERAL_TOTAL_EP_COLLECTED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1D50;
            GENERAL_WEB_OF_INTRIGUE_NODES = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x21F0;
            GENERAL_DEATHS_BY_MILITARY = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x23E8;
            GENERAL_DEATHS_BY_INFECTED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x29B0;
            GENERAL_STRIKE_TEAMS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x918;
            GENERAL_ALERTS_CAUSED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3684;
            GENERAL_STRIKE_TEAMS_EVADED = IO.In.ReadInt32();

            //Read our infected stats
            IO.In.BaseStream.Position = 0x8C0;
            INFECTED_HYDRAS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xF58;
            INFECTED_LEADERS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1704;
            INFECTED_HUNTERS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1740;
            INFECTED_INFECTED_WATERTOWERS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x237C;
            INFECTED_INFECTED_HIVES_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2C30;
            INFECTED_INFECTED_CIVILIANS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3890;
            INFECTED_EVOLVED_INFECTED_KILLED = IO.In.ReadInt32();

            //Read our military stats
            IO.In.BaseStream.Position = 0x560;
            MILITARY_BLACKWATCH_COMMANDERS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x7E0;
            MILITARY_PILOTS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xABC;
            MILITARY_BLACKWATCH_BASES_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x114C;
            MILITARY_COMMANDERS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x17AC;
            MILITARY_BASES_INFILTRATED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1804;
            MILITARY_MILITARY_PATSIED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x18D8;
            MILITARY_VIRAL_DETECTORS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2A4C;
            MILITARY_BLACKWATCH_TROOPERS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2A84;
            MILITARY_UAVS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2AD8;
            MILITARY_SCIENTISTS_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2C4C;
            MILITARY_GUN_TURRETS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3808;
            MILITARY_MARINES_KILLED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x36C0;
            MILITARY_SUPERSOLDIERS_KILLED = IO.In.ReadInt32();

            //Read our vehicles stats
            IO.In.BaseStream.Position = 0x50C;
            VEHICLES_ARMOR_DESTROYED_WHILE_IN_HELICOPTERS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x864;
            VEHICLES_CIVILIAN_VEHICLES_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x8A4;
            VEHICLES_TANKS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xB10;
            VEHICLES_TOTAL_VEHICLES_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xBC8;
            VEHICLES_APCS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xF20;
            VEHICLES_MILITARY_TRUCKS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1114;
            VEHICLES_TRANSPORTS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1420;
            VEHICLES_CARS_CRUSHED_WITH_TANK = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x165C;
            VEHICLES_GUNSHIPS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1B7C;
            VEHICLES_THERMOBARIC_TANKS_DESTROYED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1D88;
            VEHICLES_HELICOPTERS_DESTROYED_USING_A_HELICOPTER = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2450;
            VEHICLES_HELICOPSTERS_DESTROYED_WHILE_IN_ARMOR = IO.In.ReadInt32();

            //Read our consumes stats
            IO.In.BaseStream.Position = 0x604;
            CONSUMES_STEALTH_CONSUMES = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x9D0;
            CONSUMES_SCIENTISTS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xA80;
            CONSUMES_TOTAL_CONSUMES = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xAD8;
            CONSUMES_BLACKWATCH_TROOPERS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xB5C;
            CONSUMES_INFECTED_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xD3C;
            CONSUMES_BLACKWATCH_COMMANDERS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xD58;
            CONSUMES_HUNTERS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xE4C;
            CONSUMES_LEADERS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x143C;
            CONSUMES_PEDESTRIANS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x17C8;
            CONSUMES_PILOTS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x199C;
            CONSUMES_MARINES_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2CF0;
            CONSUMES_MARINE_COMMANDERS_CONSUMED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2E34;
            CONSUMES_EVOLVED_INFECTED_CONSUMED = IO.In.ReadInt32();

            //Read our hijacks stats
            IO.In.BaseStream.Position = 0x640;
            HIJACKS_TANKS_HIJACKED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x7FC;
            HIJACKS_TRANSPORTS_HIJACKED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1068;
            HIJACKS_TOTAL_VEHICLES_HIJACKED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x18F4;
            HIJACKS_THERMOBARIC_TANKS_HIJACKED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1AD4;
            HIJACKS_GUNSHIPS_HIJACKED = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2C14;
            HIJACKS_APCS_HIJACKED = IO.In.ReadInt32();



            //Read our kills stats
            IO.In.BaseStream.Position = 0x728;
            KILLS_DISMEMBERMENTS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xF04;
            KILLS_USING_TANKS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x139C;
            KILLS_SPEED_BUMPS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2100;
            KILLS_USING_HELICOPTERS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x21A4;
            KILLS_USING_APCS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x2ABC;
            KILLS_USING_THERMOBARIC_TANK = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3844;
            KILLS_PEDESTRIANS_KILLED = IO.In.ReadInt32();

            //Read our weapons stats
            IO.In.BaseStream.Position = 0x7C4;
            WEAPONS_GRENADE_SHOTS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0xAF4;
            WEAPONS_HOMING_MISSILE_SHOTS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1168;
            WEAPONS_MACHINE_GUN_SHOTS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x11B4;
            WEAPONS_MISSILE_SHOTS = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x1C60;
            WEAPONS_ASSAULT_RIFLE_SHOTS = IO.In.ReadInt32();
        }

        public void Write()
        {
            //Write our difficulty
            IO.Out.BaseStream.Position = 0x1930;
            IO.Out.Write((int)DIFFICULTY);
            IO.Out.BaseStream.Position = 0x84;
            IO.Out.WriteUnicodeString(DIFFICULTY.ToString().ToUpper(), 0x06);

            //Write our general stats.
            IO.Out.BaseStream.Position = 0x2DF8;
            IO.Out.Write(GENERAL_ALERTS_ESCAPED);
            IO.Out.BaseStream.Position = 0xA44;
            IO.Out.Write(GENERAL_DEATHS);
            IO.Out.BaseStream.Position = 0xD00;
            IO.Out.Write(GENERAL_EP_POINTERS);
            IO.Out.BaseStream.Position = 0x1558;
            IO.Out.Write(GENERAL_LANDMARKS);
            IO.Out.BaseStream.Position = 0x1A60;
            IO.Out.Write(GENERAL_HINTS);
            IO.Out.BaseStream.Position = 0x1C7C;
            IO.Out.Write(GENERAL_TOTAL_EP_COLLECTED);
            IO.Out.BaseStream.Position = 0x1D50;
            IO.Out.Write(GENERAL_WEB_OF_INTRIGUE_NODES);
            IO.Out.BaseStream.Position = 0x21F0;
            IO.Out.Write(GENERAL_DEATHS_BY_MILITARY);
            IO.Out.BaseStream.Position = 0x23E8;
            IO.Out.Write(GENERAL_DEATHS_BY_INFECTED);
            IO.Out.BaseStream.Position = 0x29B0;
            IO.Out.Write(GENERAL_STRIKE_TEAMS_DESTROYED);
            IO.Out.BaseStream.Position = 0x918;
            IO.Out.Write(GENERAL_ALERTS_CAUSED);
            IO.Out.BaseStream.Position = 0x3684;
            IO.Out.Write(GENERAL_STRIKE_TEAMS_EVADED);

            //Write our infected stats
            IO.Out.BaseStream.Position = 0x8C0;
            IO.Out.Write(INFECTED_HYDRAS_KILLED);
            IO.Out.BaseStream.Position = 0xF58;
            IO.Out.Write(INFECTED_LEADERS_KILLED);
            IO.Out.BaseStream.Position = 0x1704;
            IO.Out.Write(INFECTED_HUNTERS_KILLED);
            IO.Out.BaseStream.Position = 0x1740;
            IO.Out.Write(INFECTED_INFECTED_WATERTOWERS_DESTROYED);
            IO.Out.BaseStream.Position = 0x237C;
            IO.Out.Write(INFECTED_INFECTED_HIVES_DESTROYED);
            IO.Out.BaseStream.Position = 0x2C30;
            IO.Out.Write(INFECTED_INFECTED_CIVILIANS_KILLED);
            IO.Out.BaseStream.Position = 0x3890;
            IO.Out.Write(INFECTED_EVOLVED_INFECTED_KILLED);

            //Write our military stats
            IO.Out.BaseStream.Position = 0x560;
            IO.Out.Write(MILITARY_BLACKWATCH_COMMANDERS_KILLED);
            IO.Out.BaseStream.Position = 0x7E0;
            IO.Out.Write(MILITARY_PILOTS_KILLED);
            IO.Out.BaseStream.Position = 0xABC;
            IO.Out.Write(MILITARY_BLACKWATCH_BASES_DESTROYED);
            IO.Out.BaseStream.Position = 0x114C;
            IO.Out.Write(MILITARY_COMMANDERS_KILLED);
            IO.Out.BaseStream.Position = 0x17AC;
            IO.Out.Write(MILITARY_BASES_INFILTRATED);
            IO.Out.BaseStream.Position = 0x1804;
            IO.Out.Write(MILITARY_MILITARY_PATSIED);
            IO.Out.BaseStream.Position = 0x18D8;
            IO.Out.Write(MILITARY_VIRAL_DETECTORS_DESTROYED);
            IO.Out.BaseStream.Position = 0x2A4C;
            IO.Out.Write(MILITARY_BLACKWATCH_TROOPERS_KILLED);
            IO.Out.BaseStream.Position = 0x2A84;
            IO.Out.Write(MILITARY_UAVS_DESTROYED);
            IO.Out.BaseStream.Position = 0x2AD8;
            IO.Out.Write(MILITARY_SCIENTISTS_KILLED);
            IO.Out.BaseStream.Position = 0x2C4C;
            IO.Out.Write(MILITARY_GUN_TURRETS_DESTROYED);
            IO.Out.BaseStream.Position = 0x3808;
            IO.Out.Write(MILITARY_MARINES_KILLED);
            IO.Out.BaseStream.Position = 0x36C0;
            IO.Out.Write(MILITARY_SUPERSOLDIERS_KILLED);

            //Write our vehicles stats
            IO.Out.BaseStream.Position = 0x50C;
            IO.Out.Write(VEHICLES_ARMOR_DESTROYED_WHILE_IN_HELICOPTERS);
            IO.Out.BaseStream.Position = 0x864;
            IO.Out.Write(VEHICLES_CIVILIAN_VEHICLES_DESTROYED);
            IO.Out.BaseStream.Position = 0x8A4;
            IO.Out.Write(VEHICLES_TANKS_DESTROYED);
            IO.Out.BaseStream.Position = 0xB10;
            IO.Out.Write(VEHICLES_TOTAL_VEHICLES_DESTROYED);
            IO.Out.BaseStream.Position = 0xBC8;
            IO.Out.Write(VEHICLES_APCS_DESTROYED);
            IO.Out.BaseStream.Position = 0xF20;
            IO.Out.Write(VEHICLES_MILITARY_TRUCKS_DESTROYED);
            IO.Out.BaseStream.Position = 0x1114;
            IO.Out.Write(VEHICLES_TRANSPORTS_DESTROYED);
            IO.Out.BaseStream.Position = 0x1420;
            IO.Out.Write(VEHICLES_CARS_CRUSHED_WITH_TANK);
            IO.Out.BaseStream.Position = 0x165C;
            IO.Out.Write(VEHICLES_GUNSHIPS_DESTROYED);
            IO.Out.BaseStream.Position = 0x1B7C;
            IO.Out.Write(VEHICLES_THERMOBARIC_TANKS_DESTROYED);
            IO.Out.BaseStream.Position = 0x1D88;
            IO.Out.Write(VEHICLES_HELICOPTERS_DESTROYED_USING_A_HELICOPTER);
            IO.Out.BaseStream.Position = 0x2450;
            IO.Out.Write(VEHICLES_HELICOPSTERS_DESTROYED_WHILE_IN_ARMOR);

            //Write our consumes stats
            IO.Out.BaseStream.Position = 0x604;
            IO.Out.Write(CONSUMES_STEALTH_CONSUMES);
            IO.Out.BaseStream.Position = 0x9D0;
            IO.Out.Write(CONSUMES_SCIENTISTS_CONSUMED);
            IO.Out.BaseStream.Position = 0xA80;
            IO.Out.Write(CONSUMES_TOTAL_CONSUMES);
            IO.Out.BaseStream.Position = 0xAD8;
            IO.Out.Write(CONSUMES_BLACKWATCH_TROOPERS_CONSUMED);
            IO.Out.BaseStream.Position = 0xB5C;
            IO.Out.Write(CONSUMES_INFECTED_CONSUMED);
            IO.Out.BaseStream.Position = 0xD3C;
            IO.Out.Write(CONSUMES_BLACKWATCH_COMMANDERS_CONSUMED);
            IO.Out.BaseStream.Position = 0xD58;
            IO.Out.Write(CONSUMES_HUNTERS_CONSUMED);
            IO.Out.BaseStream.Position = 0xE4C;
            IO.Out.Write(CONSUMES_LEADERS_CONSUMED);
            IO.Out.BaseStream.Position = 0x143C;
            IO.Out.Write(CONSUMES_PEDESTRIANS_CONSUMED);
            IO.Out.BaseStream.Position = 0x17C8;
            IO.Out.Write(CONSUMES_PILOTS_CONSUMED);
            IO.Out.BaseStream.Position = 0x199C;
            IO.Out.Write(CONSUMES_MARINES_CONSUMED);
            IO.Out.BaseStream.Position = 0x2CF0;
            IO.Out.Write(CONSUMES_MARINE_COMMANDERS_CONSUMED);
            IO.Out.BaseStream.Position = 0x2E34;
            IO.Out.Write(CONSUMES_EVOLVED_INFECTED_CONSUMED);

            //Write our hijacks stats
            IO.Out.BaseStream.Position = 0x640;
            IO.Out.Write(HIJACKS_TANKS_HIJACKED);
            IO.Out.BaseStream.Position = 0x7FC;
            IO.Out.Write(HIJACKS_TRANSPORTS_HIJACKED);
            IO.Out.BaseStream.Position = 0x1068;
            IO.Out.Write(HIJACKS_TOTAL_VEHICLES_HIJACKED);
            IO.Out.BaseStream.Position = 0x18F4;
            IO.Out.Write(HIJACKS_THERMOBARIC_TANKS_HIJACKED);
            IO.Out.BaseStream.Position = 0x1AD4;
            IO.Out.Write(HIJACKS_GUNSHIPS_HIJACKED);
            IO.Out.BaseStream.Position = 0x2C14;
            IO.Out.Write(HIJACKS_APCS_HIJACKED);

            //Write our kills stats
            IO.Out.BaseStream.Position = 0x728;
            IO.Out.Write(KILLS_DISMEMBERMENTS);
            IO.Out.BaseStream.Position = 0xF04;
            IO.Out.Write(KILLS_USING_TANKS);
            IO.Out.BaseStream.Position = 0x139C;
            IO.Out.Write(KILLS_SPEED_BUMPS);
            IO.Out.BaseStream.Position = 0x2100;
            IO.Out.Write(KILLS_USING_HELICOPTERS);
            IO.Out.BaseStream.Position = 0x21A4;
            IO.Out.Write(KILLS_USING_APCS);
            IO.Out.BaseStream.Position = 0x2ABC;
            IO.Out.Write(KILLS_USING_THERMOBARIC_TANK);
            IO.Out.BaseStream.Position = 0x3844;
            IO.Out.Write(KILLS_PEDESTRIANS_KILLED);

            //Write our weapons stats
            IO.Out.BaseStream.Position = 0x7C4;
            IO.Out.Write(WEAPONS_GRENADE_SHOTS);
            IO.Out.BaseStream.Position = 0xAF4;
            IO.Out.Write(WEAPONS_HOMING_MISSILE_SHOTS);
            IO.Out.BaseStream.Position = 0x1168;
            IO.Out.Write(WEAPONS_MACHINE_GUN_SHOTS);
            IO.Out.BaseStream.Position = 0x11B4;
            IO.Out.Write(WEAPONS_MISSILE_SHOTS);
            IO.Out.BaseStream.Position = 0x1C60;
            IO.Out.Write(WEAPONS_ASSAULT_RIFLE_SHOTS);
        }

        #endregion

        #region Classes
        public enum DifficultyOption : int
        {
            Easy = 0,
            Normal = 1,
            Hard = 2,
        }
        #endregion
    }
}
