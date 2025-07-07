using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors._PROTOTYPE_
{
    public partial class Prototype : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "4156084E";

        /// <summary>
        /// Our prototype class editor.
        /// </summary>
        public PrototypeClass PROTOTYPE_CLASS { get; set; }

        public Prototype()
        {
            InitializeComponent();
            TitleID = FormID.Prototype;
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile(0))
                return false;

            //Read our save
            PROTOTYPE_CLASS = new PrototypeClass(IO);

   
            comboDifficulty.Items.Clear();
            foreach (PrototypeClass.DifficultyOption dO in (PrototypeClass.DifficultyOption[])Enum.GetValues(typeof(PrototypeClass.DifficultyOption)))
                comboDifficulty.Items.Add(dO.ToString());
            comboDifficulty.SelectedIndex = (int)PROTOTYPE_CLASS.DIFFICULTY;

            //GENERAL
            //      TODO: Time Played
            //      TODO: Missions Completed
            //      TODO: Missions Without Alert
            intWebofIntrigueNodes.Value = PROTOTYPE_CLASS.GENERAL_WEB_OF_INTRIGUE_NODES;
            intLandmarks.Value = PROTOTYPE_CLASS.GENERAL_LANDMARKS;
            intHints.Value = PROTOTYPE_CLASS.GENERAL_HINTS;
            intTotalEPCollected.Value = PROTOTYPE_CLASS.GENERAL_TOTAL_EP_COLLECTED;
            intCurrentEP.Value = PROTOTYPE_CLASS.GENERAL_EP_POINTERS;
            intAlertsCaused.Value = PROTOTYPE_CLASS.GENERAL_ALERTS_CAUSED;
            intAlertsEscaped.Value = PROTOTYPE_CLASS.GENERAL_ALERTS_ESCAPED;
            intStrikeTeamDestroyed.Value = PROTOTYPE_CLASS.GENERAL_STRIKE_TEAMS_DESTROYED;
            intStrikeTeamsEvaded.Value = PROTOTYPE_CLASS.GENERAL_STRIKE_TEAMS_EVADED;
            intDeaths.Value = PROTOTYPE_CLASS.GENERAL_DEATHS;
            intDeathsByInfected.Value = PROTOTYPE_CLASS.GENERAL_DEATHS_BY_INFECTED;
            intDeathsByMilitary.Value = PROTOTYPE_CLASS.GENERAL_DEATHS_BY_MILITARY;

            //INFECTED
            intInfectedCiviliansKilled.Value = PROTOTYPE_CLASS.INFECTED_INFECTED_CIVILIANS_KILLED;
            intEvolvedInfectedKilled.Value = PROTOTYPE_CLASS.INFECTED_EVOLVED_INFECTED_KILLED;
            intHuntersKilled.Value = PROTOTYPE_CLASS.INFECTED_HUNTERS_KILLED;
            intLeadersKilled.Value = PROTOTYPE_CLASS.INFECTED_LEADERS_KILLED;
            intHydrasKilled.Value = PROTOTYPE_CLASS.INFECTED_HYDRAS_KILLED;
            intInfectedWaterTowers.Value = PROTOTYPE_CLASS.INFECTED_INFECTED_WATERTOWERS_DESTROYED;
            intInfectedHivesDestroyed.Value = PROTOTYPE_CLASS.INFECTED_INFECTED_HIVES_DESTROYED;

            //MILITARY
            intBlackwatchTroopersKilled.Value = PROTOTYPE_CLASS.MILITARY_BLACKWATCH_TROOPERS_KILLED;
            intBlackwatchCommandersKilled.Value = PROTOTYPE_CLASS.MILITARY_BLACKWATCH_COMMANDERS_KILLED;
            intSupersoldiersKilled.Value = PROTOTYPE_CLASS.MILITARY_SUPERSOLDIERS_KILLED;
            intScientistsKilled.Value = PROTOTYPE_CLASS.MILITARY_SCIENTISTS_KILLED;
            intMarinesKilled.Value = PROTOTYPE_CLASS.MILITARY_MARINES_KILLED;
            intCommandersKilled.Value = PROTOTYPE_CLASS.MILITARY_COMMANDERS_KILLED;
            intMilitaryPatsied.Value = PROTOTYPE_CLASS.MILITARY_MILITARY_PATSIED;
            intPilotsKilled.Value = PROTOTYPE_CLASS.MILITARY_PILOTS_KILLED;
            intUAVsDestroyed.Value = PROTOTYPE_CLASS.MILITARY_UAVS_DESTROYED;
            intViralsDetectorsDestroyed.Value = PROTOTYPE_CLASS.MILITARY_VIRAL_DETECTORS_DESTROYED;
            intGunTurretsDestroyed.Value = PROTOTYPE_CLASS.MILITARY_GUN_TURRETS_DESTROYED;
            intBlackwatchBasesDestroyed.Value = PROTOTYPE_CLASS.MILITARY_BLACKWATCH_BASES_DESTROYED;
            intBasesInfiltrated.Value = PROTOTYPE_CLASS.MILITARY_BASES_INFILTRATED;

            //VEHICLES
            intGunshipsDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_GUNSHIPS_DESTROYED;
            intTransportsDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_TRANSPORTS_DESTROYED;
            intTanksDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_TANKS_DESTROYED;
            intAPCsDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_APCS_DESTROYED;
            intThermobaricTanksDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_THERMOBARIC_TANKS_DESTROYED;
            intHelicoptersDestroyedUsingHelicopters.Value = PROTOTYPE_CLASS.VEHICLES_HELICOPTERS_DESTROYED_USING_A_HELICOPTER;
            intArmorDestroyedWhileInHelicopters.Value = PROTOTYPE_CLASS.VEHICLES_ARMOR_DESTROYED_WHILE_IN_HELICOPTERS;
            intHelicoptersDestroyedWhileInArmor.Value = PROTOTYPE_CLASS.VEHICLES_HELICOPSTERS_DESTROYED_WHILE_IN_ARMOR;
            intCarsCrushedWithTank.Value = PROTOTYPE_CLASS.VEHICLES_CARS_CRUSHED_WITH_TANK;
            intCivilianVehiclesDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_CIVILIAN_VEHICLES_DESTROYED;
            intMilitaryTrucksDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_MILITARY_TRUCKS_DESTROYED;
            intTotalVehiclesDestroyed.Value = PROTOTYPE_CLASS.VEHICLES_TOTAL_VEHICLES_DESTROYED;

            //CONSUMES
            intBlackwatchTroopersConsumed.Value = PROTOTYPE_CLASS.CONSUMES_BLACKWATCH_TROOPERS_CONSUMED;
            intBlackwatchCommandersConsumed.Value = PROTOTYPE_CLASS.CONSUMES_BLACKWATCH_COMMANDERS_CONSUMED;
            intMarinesConsumed.Value = PROTOTYPE_CLASS.CONSUMES_MARINES_CONSUMED;
            intMarineCommandersConsumed.Value = PROTOTYPE_CLASS.CONSUMES_MARINE_COMMANDERS_CONSUMED;
            intScientistsConsumed.Value = PROTOTYPE_CLASS.CONSUMES_SCIENTISTS_CONSUMED;
            intPilotsConsumed.Value = PROTOTYPE_CLASS.CONSUMES_PILOTS_CONSUMED;
            intInfectedConsumed.Value = PROTOTYPE_CLASS.CONSUMES_INFECTED_CONSUMED;
            intEvolvedInfectedConsumed.Value = PROTOTYPE_CLASS.CONSUMES_EVOLVED_INFECTED_CONSUMED;
            intHuntersConsumed.Value = PROTOTYPE_CLASS.CONSUMES_HUNTERS_CONSUMED;
            intLeadersConsumed.Value = PROTOTYPE_CLASS.CONSUMES_LEADERS_CONSUMED;
            intPedestriansConsumed.Value = PROTOTYPE_CLASS.CONSUMES_PEDESTRIANS_CONSUMED;
            intStealthConsumes.Value = PROTOTYPE_CLASS.CONSUMES_STEALTH_CONSUMES;
            intTotalConsumes.Value = PROTOTYPE_CLASS.CONSUMES_TOTAL_CONSUMES;

            //HIJACKS
            intGunshipsHijacked.Value = PROTOTYPE_CLASS.HIJACKS_GUNSHIPS_HIJACKED;
            intTransportsHijacked.Value = PROTOTYPE_CLASS.HIJACKS_TRANSPORTS_HIJACKED;
            intTanksHijacked.Value = PROTOTYPE_CLASS.HIJACKS_TANKS_HIJACKED;
            intAPCsHijacked.Value = PROTOTYPE_CLASS.HIJACKS_APCS_HIJACKED;
            intThermobaricTanksHijacked.Value = PROTOTYPE_CLASS.HIJACKS_THERMOBARIC_TANKS_HIJACKED;
            intTotalVehiclesHijacked.Value = PROTOTYPE_CLASS.HIJACKS_TOTAL_VEHICLES_HIJACKED;

            //KILLS
            intKillsUsingHelicopters.Value = PROTOTYPE_CLASS.KILLS_USING_HELICOPTERS;
            intKillsUsingTanks.Value = PROTOTYPE_CLASS.KILLS_USING_TANKS;
            intKillsUsingAPCs.Value = PROTOTYPE_CLASS.KILLS_USING_APCS;
            intKillsUsingThermobaricTank.Value = PROTOTYPE_CLASS.KILLS_USING_THERMOBARIC_TANK;
            intKillsSpeedBumps.Value = PROTOTYPE_CLASS.KILLS_SPEED_BUMPS;
            intKillsDismemberment.Value = PROTOTYPE_CLASS.KILLS_DISMEMBERMENTS;
            intKillsPedestriansKilled.Value = PROTOTYPE_CLASS.KILLS_PEDESTRIANS_KILLED;

            //WEAPONS
            intAssaultRifleShots.Value = PROTOTYPE_CLASS.WEAPONS_ASSAULT_RIFLE_SHOTS;
            intMachineGunShots.Value = PROTOTYPE_CLASS.WEAPONS_MACHINE_GUN_SHOTS;
            intGrenadeShots.Value = PROTOTYPE_CLASS.WEAPONS_GRENADE_SHOTS;
            intMissileShots.Value = PROTOTYPE_CLASS.WEAPONS_MISSILE_SHOTS;
            intHomingMissleShots.Value = PROTOTYPE_CLASS.WEAPONS_HOMING_MISSILE_SHOTS;


            //Our file is read correctly.
            return true;
        }
        public override void Save()
        {
            //Set our index
            PROTOTYPE_CLASS.DIFFICULTY = (PrototypeClass.DifficultyOption)comboDifficulty.SelectedIndex;

            //GENERAL
            PROTOTYPE_CLASS.GENERAL_WEB_OF_INTRIGUE_NODES = intWebofIntrigueNodes.Value;
            PROTOTYPE_CLASS.GENERAL_LANDMARKS = intLandmarks.Value;
            PROTOTYPE_CLASS.GENERAL_HINTS = intHints.Value;
            PROTOTYPE_CLASS.GENERAL_TOTAL_EP_COLLECTED = intTotalEPCollected.Value;
            PROTOTYPE_CLASS.GENERAL_EP_POINTERS = intCurrentEP.Value;
            PROTOTYPE_CLASS.GENERAL_ALERTS_CAUSED = intAlertsCaused.Value;
            PROTOTYPE_CLASS.GENERAL_ALERTS_ESCAPED = intAlertsEscaped.Value;
            PROTOTYPE_CLASS.GENERAL_STRIKE_TEAMS_DESTROYED = intStrikeTeamDestroyed.Value;
            PROTOTYPE_CLASS.GENERAL_STRIKE_TEAMS_EVADED = intStrikeTeamsEvaded.Value;
            PROTOTYPE_CLASS.GENERAL_DEATHS = intDeaths.Value;
            PROTOTYPE_CLASS.GENERAL_DEATHS_BY_INFECTED = intDeathsByInfected.Value;
            PROTOTYPE_CLASS.GENERAL_DEATHS_BY_MILITARY = intDeathsByMilitary.Value;

            //INFECTED
            PROTOTYPE_CLASS.INFECTED_INFECTED_CIVILIANS_KILLED = intInfectedCiviliansKilled.Value;
            PROTOTYPE_CLASS.INFECTED_EVOLVED_INFECTED_KILLED = intEvolvedInfectedKilled.Value;
            PROTOTYPE_CLASS.INFECTED_HUNTERS_KILLED = intHuntersKilled.Value;
            PROTOTYPE_CLASS.INFECTED_LEADERS_KILLED = intLeadersKilled.Value;
            PROTOTYPE_CLASS.INFECTED_HYDRAS_KILLED = intHydrasKilled.Value;
            PROTOTYPE_CLASS.INFECTED_INFECTED_WATERTOWERS_DESTROYED = intInfectedWaterTowers.Value;
            PROTOTYPE_CLASS.INFECTED_INFECTED_HIVES_DESTROYED = intInfectedHivesDestroyed.Value;

            //MILITARY
            PROTOTYPE_CLASS.MILITARY_BLACKWATCH_TROOPERS_KILLED = intBlackwatchTroopersKilled.Value;
            PROTOTYPE_CLASS.MILITARY_BLACKWATCH_COMMANDERS_KILLED = intBlackwatchCommandersKilled.Value;
            PROTOTYPE_CLASS.MILITARY_SUPERSOLDIERS_KILLED = intSupersoldiersKilled.Value;
            PROTOTYPE_CLASS.MILITARY_SCIENTISTS_KILLED = intScientistsKilled.Value;
            PROTOTYPE_CLASS.MILITARY_MARINES_KILLED = intMarinesKilled.Value;
            PROTOTYPE_CLASS.MILITARY_COMMANDERS_KILLED = intCommandersKilled.Value;
            PROTOTYPE_CLASS.MILITARY_MILITARY_PATSIED = intMilitaryPatsied.Value;
            PROTOTYPE_CLASS.MILITARY_PILOTS_KILLED = intPilotsKilled.Value;
            PROTOTYPE_CLASS.MILITARY_UAVS_DESTROYED = intUAVsDestroyed.Value;
            PROTOTYPE_CLASS.MILITARY_VIRAL_DETECTORS_DESTROYED = intViralsDetectorsDestroyed.Value;
            PROTOTYPE_CLASS.MILITARY_GUN_TURRETS_DESTROYED = intGunTurretsDestroyed.Value;
            PROTOTYPE_CLASS.MILITARY_BLACKWATCH_BASES_DESTROYED = intBlackwatchBasesDestroyed.Value;
            PROTOTYPE_CLASS.MILITARY_BASES_INFILTRATED = intBasesInfiltrated.Value;

            //VEHICLES
            PROTOTYPE_CLASS.VEHICLES_GUNSHIPS_DESTROYED = intGunshipsDestroyed.Value;
            PROTOTYPE_CLASS.VEHICLES_TRANSPORTS_DESTROYED = intTransportsDestroyed.Value;
            PROTOTYPE_CLASS.VEHICLES_TANKS_DESTROYED = intTanksDestroyed.Value;
            PROTOTYPE_CLASS.VEHICLES_APCS_DESTROYED = intAPCsDestroyed.Value;
            PROTOTYPE_CLASS.VEHICLES_THERMOBARIC_TANKS_DESTROYED = intThermobaricTanksDestroyed.Value;
            PROTOTYPE_CLASS.VEHICLES_HELICOPTERS_DESTROYED_USING_A_HELICOPTER = intHelicoptersDestroyedUsingHelicopters.Value;
            PROTOTYPE_CLASS.VEHICLES_ARMOR_DESTROYED_WHILE_IN_HELICOPTERS = intArmorDestroyedWhileInHelicopters.Value;
            PROTOTYPE_CLASS.VEHICLES_HELICOPSTERS_DESTROYED_WHILE_IN_ARMOR = intHelicoptersDestroyedWhileInArmor.Value;
            PROTOTYPE_CLASS.VEHICLES_CARS_CRUSHED_WITH_TANK = intCarsCrushedWithTank.Value;
            PROTOTYPE_CLASS.VEHICLES_CIVILIAN_VEHICLES_DESTROYED = intCivilianVehiclesDestroyed.Value;
            PROTOTYPE_CLASS.VEHICLES_MILITARY_TRUCKS_DESTROYED = intMilitaryTrucksDestroyed.Value;
            PROTOTYPE_CLASS.VEHICLES_TOTAL_VEHICLES_DESTROYED = intTotalVehiclesDestroyed.Value;

            //CONSUMES
            PROTOTYPE_CLASS.CONSUMES_BLACKWATCH_TROOPERS_CONSUMED = intBlackwatchTroopersConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_BLACKWATCH_COMMANDERS_CONSUMED = intBlackwatchCommandersConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_MARINES_CONSUMED = intMarinesConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_MARINE_COMMANDERS_CONSUMED = intMarineCommandersConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_SCIENTISTS_CONSUMED = intScientistsConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_PILOTS_CONSUMED = intPilotsConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_INFECTED_CONSUMED = intInfectedConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_EVOLVED_INFECTED_CONSUMED = intEvolvedInfectedConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_HUNTERS_CONSUMED = intHuntersConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_LEADERS_CONSUMED = intLeadersConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_PEDESTRIANS_CONSUMED = intPedestriansConsumed.Value;
            PROTOTYPE_CLASS.CONSUMES_STEALTH_CONSUMES = intStealthConsumes.Value;
            PROTOTYPE_CLASS.CONSUMES_TOTAL_CONSUMES = intTotalConsumes.Value;

            //HIJACKS
            PROTOTYPE_CLASS.HIJACKS_GUNSHIPS_HIJACKED = intGunshipsHijacked.Value;
            PROTOTYPE_CLASS.HIJACKS_TRANSPORTS_HIJACKED = intTransportsHijacked.Value;
            PROTOTYPE_CLASS.HIJACKS_TANKS_HIJACKED = intTanksHijacked.Value;
            PROTOTYPE_CLASS.HIJACKS_APCS_HIJACKED = intAPCsHijacked.Value;
            PROTOTYPE_CLASS.HIJACKS_THERMOBARIC_TANKS_HIJACKED = intThermobaricTanksHijacked.Value;
            PROTOTYPE_CLASS.HIJACKS_TOTAL_VEHICLES_HIJACKED = intTotalVehiclesHijacked.Value;

            //KILLS
            PROTOTYPE_CLASS.KILLS_USING_HELICOPTERS = intKillsUsingHelicopters.Value;
            PROTOTYPE_CLASS.KILLS_USING_TANKS = intKillsUsingTanks.Value;
            PROTOTYPE_CLASS.KILLS_USING_APCS = intKillsUsingAPCs.Value;
            PROTOTYPE_CLASS.KILLS_USING_THERMOBARIC_TANK = intKillsUsingThermobaricTank.Value;
            PROTOTYPE_CLASS.KILLS_SPEED_BUMPS = intKillsSpeedBumps.Value;
            PROTOTYPE_CLASS.KILLS_DISMEMBERMENTS = intKillsDismemberment.Value;
            PROTOTYPE_CLASS.KILLS_PEDESTRIANS_KILLED = intKillsPedestriansKilled.Value;

            //WEAPONS
            PROTOTYPE_CLASS.WEAPONS_ASSAULT_RIFLE_SHOTS = intAssaultRifleShots.Value;
            PROTOTYPE_CLASS.WEAPONS_MACHINE_GUN_SHOTS = intMachineGunShots.Value;
            PROTOTYPE_CLASS.WEAPONS_GRENADE_SHOTS = intGrenadeShots.Value;
            PROTOTYPE_CLASS.WEAPONS_MISSILE_SHOTS = intMissileShots.Value;
            PROTOTYPE_CLASS.WEAPONS_HOMING_MISSILE_SHOTS = intHomingMissleShots.Value;


            PROTOTYPE_CLASS.Write();
        }

        private void integerInput12_ValueChanged(object sender, EventArgs e)
        {

        }

        private void integerInput6_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
