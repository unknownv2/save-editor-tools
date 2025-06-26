using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Gears_of_War_3
{
    public partial class GearsOfWar3 : EditorControl
    {
        //public static readonly string FID = "4D5308AB";
        public GearsOfWar3()
        {
            InitializeComponent();
            TitleID = FormID.GearsOfWar3;
            

            foreach (string[] weapon in Weapons)
            {
                cbWeapon1.Items.Add(weapon[1]);
                cbWeapon2.Items.Add(weapon[1]);
                cbWeapon3.Items.Add(weapon[1]);
                cbWeapon4.Items.Add(weapon[1]);
            }

            foreach (string[] model in Models)
                cbModel.Items.Add(model[1]);

            #if INT2
            cmdExtractStructs.Visible = true;
            cmdExtractState.Visible = true;
            #endif
        }

        private struct ComboMeta
        {
            internal GearGame GameSave;
            internal string FileName;
        }

        private GearGame GameSave;
        private List<ComboMeta> GameSaves;

        public override bool Entry()
        {
            GameSaves = new List<ComboMeta>();
            for (int x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                if (OpenStfsFile(x) && IO.Stream.Length != 0)
                    GameSaves.Add(new ComboMeta()
                    {
                        GameSave = new GearGame(IO),
                        FileName = Package.StfsContentPackage.DirectoryEntries[x].FileName
                    });

            if (GameSaves.Count == 0)
                throw new Exception("No checkpoints found in save!");

            comboSaves.Items.Clear();
            for (int x = 1; x < GameSaves.Count + 1; x++)
                comboSaves.Items.Add("Checkpoint " + x);

            isBusy = true;
            comboSaves.SelectedIndex = 0;
            isBusy = false;

            loadCheckpoint(0);

            return true;
        }

        private string tagToName(string[][] collection, string tag)
        {
            foreach (string[] item in collection)
                if (item[0] == tag)
                    return item[1];
            return tag;
        }

        private string nameToTag(string[][] collection, string name)
        {
            foreach (string[] item in collection)
                if (item[1] == name)
                    return item[0];
            return name;
        }

        private readonly static string[][] Models = new string[][]
        {
            new string[] { "GearGameContent.GearPawn_LocustDrone", "Drone" },
            new string[] { "GearGameContent.GearPawn_LocustGrapplingDrone", "Grappling Drone" },
            new string[] { "GearGameContent.GearPawn_LocustSniper", "Locust Sniper" },
            new string[] { "GearGameContent.GearPawn_LocustNemaslug", "Nemaslug" },
            new string[] { "GearGameContent.GearPawn_LocustHunterBase", "Hunter" },
            new string[] { "GearGameContent.GearPawn_LocustHunterNoArmorNoGrenades_Nightmare", "Hunter - No Armor, No Grenades" },
            new string[] { "GearGameContent.GearPawn_LocustFlameDrone_Nightmare", "Flame Drone - Nightmare" },
            new string[] { "GearGameContent.GearPawn_LocustBoomer_Nightmare", "Boomer - Nightmare" },
            new string[] { "GearGameContent.GearPawn_LocustTheron", "Theron" },
            new string[] { "GearGameContent.GearPawn_LocustPalaceGuard", "Palace Guard" },
            new string[] { "GearGameContent.GearPawn_LocustDroneSavage", "Drone Savage" },
            new string[] { "GearGameContent.GearPawn_LocustDroneSavageSiege", "Drone Savage Siege" },
            new string[] { "GearGameContent.GearPawn_LocustHunterSavageFlame", "Hunter Savage Flame" },
            new string[] { "GearGameContent.GearPawn_LocustHunterSavageSiege", "Hunter Savage Siege" },
            new string[] { "GearGameContent.GearPawn_SavageTheronWithCleaver", "Savage Theron w/Cleaver" },
            new string[] { "GearGameContent.GearPawn_LocustDroneSavageUnderground", "Drone Savage Underground" },
            new string[] { "GearGameContent.GearPawn_LocustGeneralRaam", "General Raam" },
            new string[] { "GearGameContent.GearPawn_COGHoffman", "Hoffman" },
            new string[] { "GearGameContent.GearPawn_LocustGunker", "Gunker" },
            new string[] { "GearGameContent.GearPawn_LocustGunker_CloseQuartersNoGib", "Gunker - Close Quarters" },
            new string[] { "GearGameContent.GearPawn_LocustBoomerGatling", "Boomer Gatling" },
            new string[] { "GearGameContent.GearPawn_LocustDarkWretch", "Dark Wretch" },
            new string[] { "GearGameContent.GearPawn_LambentDrone", "Lambent Drone" },
            new string[] { "GearGameContent.GearPawn_LambentLocustFormer", "Lambent Locust Former" },
            new string[] { "GearGameContent.GearPawn_Drudge", "Drudge" },
            new string[] { "GearGameContent.GearPawn_DrudgePolypSpawning", "Drudge Polyp Spawning" },
            new string[] { "GearGameContent.GearPawn_DrudgeHeadWorm", "Drudge Head Worm" },
            new string[] { "GearGameContent.GearPawn_LocustBerserker", "Berserker" },
            new string[] { "GearGameContent.GearPawn_LocustReaverDriver", "Reaver Driver" },
            new string[] { "GearGameContent.GearPawn_LocustReaverDriver_PalaceGuard", "Reaver Driver Palace Guard" },
            new string[] { "GearGameContent.GearPawn_LocustLeviathan", "Leviathan" },
            new string[] { "GearGameContent.GearPawn_LocustIthycus", "Ithycus" },
            new string[] { "GearGameContent.GearPawn_COGDom", "Dom" },
            new string[] { "GearGameContent.GearPawn_Stranded", "Stranded" },
            new string[] { "GearGameContent.GearPawn_LocustBrumak", "Brumak" },
            new string[] { "GearGameContent.GearPawn_LocustBrumak_ResistSmallArms", "Brumak - Resist Small Arms" },
            new string[] { "GearGameContent.GearPawn_LocustBrumakPlayer", "Brumak Player" },
            new string[] { "GearGameContent.GearPawn_LocustBrumakDriver", "Brumak Driver" },
            new string[] { "GearGameContent.GearPawn_NPCCOGBase", "NPC COG" },
            new string[] { "GearGameContent.GearPawn_Chicken", "Chicken" },
            new string[] { "GearGameContent.GearPawn_Chicken_Attack", "Chicken - Attack" },
            new string[] { "GearGameContent.GearPawn_Redshirt", "Redshirt" },
            new string[] { "GearGameContent.GearPawn_LocustTicker", "Ticker" },
            new string[] { "GearGameContent.GearPawn_LocustNakedTicker", "Naked Ticker" },
            new string[] { "GearGameContent.GearPawn_LocustNakedTickerNoAmmo", "Naked Ticker - No Ammo" },
            new string[] { "GearGameContent.GearPawn_LocustSerapede", "Serapede" },
            new string[] { "GearGameContent.GearPawn_LocustSkorge", "Skorge" },
            new string[] { "GearGameContent.GearPawn_LocustMyrrah", "Myrrah" },
            new string[] { "GearGameContent.GearPawn_LocustKantusArmor", "Kantus Armor" },
            new string[] { "GearGameContent.GearPawn_LambentPolyp", "Lambent Polyp" },
            new string[] { "GearGameContent.GearPawn_LocustCorpserLarvaUndergroundNakedContent", "Corpser Larva - Underground, Naked" },
            new string[] { "GearGameContent.GearPawn_LocustCorpserLarvaLockedUndergroundContent", "Corpser Larva - Locked Underground" },
            new string[] { "GearGameContent.GearPawn_SecurityBotFlying", "Security Bot Flying" },
            new string[] { "GearGameContent.GearPawn_SecurityBotFlying_Tempest", "Security Bot Flying - Tempest" },
            new string[] { "GearGameContent.GearPawn_SecurityBotStationary", "Security Bot - Stationary" },
            new string[] { "GearGameContent.GearPawn_LocustSpeedy", "Speedy" },
            new string[] { "GearGameContent.GearPawn_LocustMinigunner", "Locust Minigunner" },
            new string[] { "GearGameContent.GearPawn_LocustFlameDrone", "Flame Drone" },
            new string[] { "GearGameContent.GearPawn_SilverbackArmed", "Silverback Armed" },
            new string[] { "GearGameContent.GearPawn_LocustTempest", "Tempest" },
            new string[] { "GearGameContent.GearPawn_LocustTempestBoss", "Tempest Boss" },
            new string[] { "GearGameContent.GearPawn_LocustTempestBlimp", "Tempest Blimp" },
            new string[] { "GearGameContent.GearPawn_COGGusStadium", "Gus Stadium" },
            new string[] { "GearGameContent.GearPawn_LocustBoomer", "Boomer" },
            new string[] { "GearGameContent.GearPawn_LocustBoomerSavage", "Boomer - Savage" },
            new string[] { "GearGameContent.GearPawn_LocustBoomerFlame", "Boomer - Flame" },
            new string[] { "GearGameContent.GearPawn_LocustBoomerMechanic", "Boomer - Mechanic" },
            new string[] { "GearGameContent.GearPawn_LocustKantus", "Kantus" },
            new string[] { "GearGameContent.GearPawn_NPCGriffin", "NPC Griffin" },
            new string[] { "GearGameContent.GearPawn_COGRedShirt", "COG Red Shirt" },
            new string[] { "GearGame.GearPawn_COGMarcus", "Marcus" },
            new string[] { "GearGameContent.GearPawn_COGJace", "Jace" },
            new string[] { "GearGameContent.GearPawn_COGAnya", "Anya" },
            new string[] { "GearGameContent.GearPawn_COGSam", "Sam" },
            new string[] { "GearGameContent.GearPawn_COGBaird", "Baird" },
            new string[] { "GearGameContent.GearPawn_CCarmine", "C. Carmine" },
            new string[] { "GearGameContent.GearPawn_COGGus", "Cole" },
            new string[] { "GearGameContent.GearPawn_COGSummerRedshirt", "Summer - Red Shirt" },
            new string[] { "GearGameContent.GearPawn_NPCRavensHoodieArmored", "Ravens - Armored Hoodie" },
            new string[] { "GearGameContent.GearPawn_NPCRavensMechanic", "Ravens - Mechanic" },
            new string[] { "GearGameContent.GearPawn_NPCRavensMechanic2", "Ravens - Mechanic 2" },
            new string[] { "GearGameContent.GearPawn_NPCHanoverMale1", "Hanover Male 1" },
            new string[] { "GearGameContent.GearPawn_NPCHanoverMale2", "Hanover Male 2" },
            new string[] { "GearGameContent.GearPawn_NPCRavensRedPants", "Ravens - Red Pants" },
            new string[] { "GearGameContent.GearPawn_NPCRavensScarf", "Ravens - Red Scarf" },
            new string[] { "GearGameContent.GearPawn_COGAdamFenixUnarmed", "Adam Fenix" },
            new string[] { "GearGameContent.GearPawn_COGBenjaminCarmine", "B. Carmine" },
            new string[] { "GearGameContent.GearPawn_COGDomG2", "Dom G2" },
            new string[] { "GearGameContent.Vehicle_Jack", "Jack" },
            new string[] { "GearGameContent.Vehicle_ReaverNoGunner", "Reaver - No Gunner" }
        };

        private readonly static string[][] Weapons = new string[][]
        {
            new string[] { "GearGame.GearWeap_CorpserMelee", "Corpser Melee" },
            new string[] { "GearGame.GearWeap_FragGrenade", "Frag Grenade" },
            new string[] { "GearGameContentWeapons.GearWeap_InkGrenade", "Ink Grenade" },
            new string[] { "GearGameContentWeapons.GearWeap_IncendiaryGrenade", "Incendiary Grenade" },
            new string[] { "GearGame.GearWeap_COGPistol", "Snub Pistol" },
            new string[] { "GearGame.GearWeap_SniperRifle", "Sniper Rifle" },
            new string[] { "GearGame.GearWeap_LocustAssaultRifle", "Hammerburst" },
            new string[] { "GearGame.GearWeap_AssaultRifle", "Lancer" },
            new string[] { "GearGame.GearWeap_Shotgun", "Gnasher" },
            new string[] { "GearGame.GearWeap_Shotgun_SawedOff", "Sawed-Off" },
            new string[] { "GearGame.GearWeap_SmokeGrenade", "Smoke Grenade" },
            new string[] { "GearGameContentWeapons.GearWeap_SmokeGrenade", "Smoke Grenade - Content" },
            new string[] { "GearGame.GearWeap_AssaultRifle_MultiTurret", "Lancer Multi Turret" },
            new string[] { "GearGame.GearWeap_BoomshotBase", "Boomshot Base" },
            new string[] { "GearGame.GearWeap_Boomshot", "Boomshot" },
            new string[] { "GearGameContentWeapons.GearWeap_Boomer_Boomshot", "Boomer Boomshot" },
            new string[] { "GearGameContentWeapons.GearWeap_Boomer_Boomshot_Siege", "Boomer Boomshot - Siege" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyFlail", "Heavy Flail" },
            new string[] { "GearGameContentWeapons.GearWeap_Cleaver", "Cleaver" },
            new string[] { "GearGameContentWeapons.GearWeap_LocustBurstPistol_Kantus", "Gorgon Burst Pistol - Kantus" },
            new string[] { "GearGameContentWeapons.GearWeap_LocustPistol_Kantus", "Gorgon Pistol" },
            new string[] { "GearGameContentWeapons.GearWeap_LocustDualPistol_Kantus", "Kantus Dual Pistol" },
            new string[] { "GearGameContentWeapons.GearWeap_InkGrenadeKantus", "Ink Grenade - Kantus" },
            new string[] { "GearGameContentWeapons.GearWeap_InkGrenadeKantusCover", "Ink Grenade - Kantus, Cover" },
            new string[] { "GearGameContentWeapons.GearWeap_InkGrenadeSkorge", "Ink Grenade - Skorge" },
            new string[] { "GearGameContentWeapons.GearWeap_FragGrenade_Siege", "Frag Grenade - Siege" },
            new string[] { "GearGameContentWeapons.GearWeap_SandGrenade", "Sand Grenade" },
            new string[] { "GearGameContentWeapons.GearWeap_LambentAssaultRifle", "Lambent Assault Rifle" },
            new string[] { "GearGameContentWeapons.GearWeap_LocustBurstPistol_Skorge", "Gorgon Burst Pistol - Skorge" },
            new string[] { "GearGameContentWeapons.GearWeap_LocustTempest", "Tempest" },
            new string[] { "GearGameContentWeapons.GearWeap_Harpoon", "Harpoon" },
            new string[] { "GearGameContentWeapons.GearWeap_FlameThrower_Nightmare", "Flame Thrower - Nightmare" },
            new string[] { "GearGameContentWeapons.GearWeap_FireExtinguisher", "Fire Extinguisher" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyMiniGun", "Heavy Mini Gun" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyCoopMG", "Heavy Co-op MG" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyCoopMGAmmoBox", "Heavy Co-op MG Ammo Box" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyCoopMG_Convoy", "Heavy Co-op MG Convoy" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavySniper", "One-Shot" },
            new string[] { "GearGameContentWeapons.GearWeap_BrumakDummyFire", "Brumak Dummy Fire" },
            new string[] { "GearGameContentWeapons.GearWeap_Boomer_Minigun", "Mulcher" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyMortar", "Heavy Mortar" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyRocketLauncher_Content", "Heavy Rocket Launcher" },
            new string[] { "GearGameContentWeapons.GearWeap_HeavyMortarDummyFire", "Heavy Mortar Dummy Fire" },
            new string[] { "GearGameContentWeapons.GearWeap_Hydra_Minigun", "Hydra Minigun" },
            new string[] { "GearGameContentWeapons.GearWeap_HydraRocketLauncher", "Hydra Rocket Launcher" },
            new string[] { "GearGameContentWeapons.GearWeap_LocustAATurret", "AA Turret" },
            new string[] { "GearGameContentWeapons.GearWeap_LambentPolypAttack", "Polyp Attack" },
            new string[] { "GearGameContentWeapons.GearWeap_Boomer_Digger", "Digger" },
            new string[] { "GearGameContentWeapons.GearWeap_FlameThrower", "Flame Thrower" },
            new string[] { "GearGameContentWeapons.GearWeap_LocustBurstPistol", "Gorgon Burst Pistol" },
            new string[] { "GearGameContentWeapons.GearWeap_GunkerBombDummyFire", "Gunker Bomb Dummy Fire" },
            new string[] { "GearGameContentWeapons.GearWeap_DiggerLauncher_Content", "Digger Launcher - Content" },
            new string[] { "GearGame.GearWeap_Boomshot_MultiTurret", "Boomshot Multi Turret" },
            new string[] { "GearGame.GearWeap_Boomshot_MultiTurret_GasBarge", "Boomshot Multi Turret - Gas Barge" },
            new string[] { "GearGame.GearWeap_ChickenLauncherBase", "Chicken Launcher" },
            new string[] { "GearGame.GearWeap_Boomer_FlailBase", "Flail" },
            new string[] { "GearGame.GearWeap_BowBase", "Torque Bow Base" },
            new string[] { "GearGameContentWeapons.GearWeap_Bow" , "Torque Bow - Content" },
            new string[] { "GearGame.GearWeap_Bow", "Torque Bow" },
            new string[] { "GearGame.GearWeap_RipperBase", "Ripper Base" },
            new string[] { "GearGame.GearWeap_PistolBase", "Pistol Base" },
            new string[] { "GearGame.GearWeap_GrenadeBase", "Grenade Base" },
            new string[] { "GearGame.GearWeap_HODBase", "Hammer of Dawn Base" },
            new string[] { "GearGame.GearWeap_TempestHODBase", "Hammer of Dawn Base - Tempest" },
            new string[] { "GearGameContentWeapons.GearWeap_HOD", "Hammer of Dawn" },
            new string[] { "GearGame.GearWeap_LocustAssaultRifle_MultiTurret", "Hammerburst Multi Turret" },
            new string[] { "GearGame.GearWeap_RetroLancer", "Retro Lancer" },
            new string[] { "GearGame.GearWeap_RetroLancer_MultiTurret", "Retro Lancer Multi Turret" },
            new string[] { "GearGame.GearWeap_LocustPistol", "Boltok Pistol" },
            new string[] { "GearGame.GearWeap_LocustBurstPistolBase", "Locust Burst Pistol Base" },
            new string[] { "GearGame.GearWeap_Shotgun_MultiTurret", "Gnasher Multi Turret" },
            new string[] { "GearGame.GearWeap_TroikaBase", "Troika Base" },
            new string[] { "GearGame.GearWeap_BlimpBallBase", "Blimp Ball Base" },
            new string[] { "GearGame.GearWeap_Silverback_MinigunBase", "Silverback Minigun Base" },
            new string[] { "GearGame.GearWeap_Silverback_FlameThrowerBase", "Silverback FlameThrower Base" },
            new string[] { "GearGame.GearWeap_Silverback_RocketLauncherBase", "Silverback Rocket Launcher Base" },
            new string[] { "GearGame.GearWeap_BabyCorpserMelee", "Baby Corpser Melee" },
            new string[] { "GearGame.GearWeap_WretchMelee", "Wretch Melee" },
            new string[] { "GearGame.GearWeap_DarkWretchMelee", "Dark Wretch Melee" },
            new string[] { "GearGame.GearWeap_SilverbackLoaderMelee", "Silverback Loader Melee" },
            new string[] { "GearGame.GearWeap_TickerMelee", "Ticker Melee" },
            new string[] { "GearGame.GearWeap_FlameThrowerBase", "Flame Thrower Base" },
            new string[] { "GearGame.GearWeap_DrudgeFire", "Drudge Fire" },
            new string[] { "GearGame.GearWeap_HeavyBase", "Heavy Base" },
            new string[] { "GearGame.GearWeap_HeavySniperBase", "Heavy Sniper Base" },
            new string[] { "GearGame.GearWeap_BloodMountMelee", "Blood Mount Melee" },
            new string[] { "GearGame.GearWeap_LambentHumanMelee", "Lambent Human Melee" },
            new string[] { "GearGame.GearWeap_RockWormMelee", "Rock Worm Melee" },
            new string[] { "GearGame.GearWeap_NemaSlugMelee", "Nema Slug Melee" },
            new string[] { "GearGame.GearWeap_DiggerLauncher", "Digger Launcher" },
            new string[] { "GearGame.GearWeap_DLC_Cobra", "Cobra (DLC)" },
            new string[] { "GearGameContentWeapons.GearWeap_TempestHOD", "Hammer of Dawn - Tempest" },
            new string[] { "GearGameContent.GearWeap_SecurityBotGunFlying", "Security Bot Gun" },
            new string[] { null, "-- None --" }
        };

        private List<GearGame.GearPC_SP> PC;
        private List<GearGame.GearAI> AI;

        private void loadCheckpoint(int saveNum)
        {
            GameSave = GameSaves[saveNum].GameSave;
            PC = GameSave.GetPlayableCharacterStructs();
            AI = GameSave.GetAIStructs();

            isBusy = true;

            listChars.Nodes.Clear();

            for (int x = 0; x < PC.Count; x++)
                listChars.Nodes.Add(createCharacterNode(tagToName(Models, PC[x].Model1), PC[x]));

            for (int x = 0; x < AI.Count; x++)
                listChars.Nodes.Add(createCharacterNode((AI[x].Name.Length == 0) ? tagToName(Models, AI[x].Model) : AI[x].Name, AI[x]));

            listChars.SelectedIndex = 0;

            isBusy = false;

            if (PC.Count != 0)
                loadCharacter(PC[0]);
            else if (AI.Count != 0)
                loadCharacter(AI[0]);
            else
                throw new Exception("No characters found in checkpoint!");
        }

        private void listChars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listChars.SelectedIndex == -1 || isBusy)
                return;

            loadCharacter(listChars.Nodes[listChars.SelectedIndex].Tag);
        }

        private GearGame.GearPC_SP CurrentPlayer = null;
        private GearGame.GearAI CurrentAI = null;

        private void loadCharacter(object charStruct)
        {
            saveCurrentData();

            if (charStruct is GearGame.GearPC_SP)
            {
                CurrentPlayer = (GearGame.GearPC_SP)charStruct;
                CurrentAI = null;

                populateWeapons(CurrentPlayer.Weapons);
                string model = tagToName(Models, CurrentPlayer.Model1) ?? CurrentPlayer.Model1;
                if (!cbModel.Items.Contains(model))
                    cbModel.Items.Add(model);
                cbModel.SelectedItem = model;
                txtName.Text = "Not Available";
                txtName.ReadOnly = true;
                loadLocation(CurrentPlayer.Location);
            }
            else
            {
                CurrentAI = (GearGame.GearAI)charStruct;
                CurrentPlayer = null;

                populateWeapons(CurrentAI.Weapons);
                string model = tagToName(Models, CurrentAI.Model) ?? CurrentAI.Model;
                if (!cbModel.Items.Contains(model))
                    cbModel.Items.Add(model);
                cbModel.SelectedItem = model;
                if (CurrentAI.Name.Length == 0)
                {
                    txtName.Text = "Not Available";
                    txtName.ReadOnly = true;
                }
                else
                {
                    txtName.ReadOnly = false;
                    txtName.Text = CurrentAI.Name;
                }
                loadLocation(CurrentAI.Location);
            }
        }

        private void saveCurrentData()
        {
            if (CurrentPlayer != null)
            {
                CurrentPlayer.Location = getCurrentLocation();
                CurrentPlayer.Model1 = nameToTag(Models, (string)cbModel.SelectedItem);
                saveWeapons(CurrentPlayer.Weapons);
            }
            else if (CurrentAI != null)
            {
                CurrentAI.Location = getCurrentLocation();
                if (txtName.TextLength == 0)
                    Functions.UI.errorBox("A blank name will not be saved!");
                else
                    CurrentAI.Name = txtName.Text;
                CurrentAI.Model = nameToTag(Models, (string)cbModel.SelectedItem);
                saveWeapons(CurrentAI.Weapons);
            }
        }

        private void saveWeapons(List<GearGame.GearWeap> weapons)
        {
            saveWeapon(weapons, GearGame.WeaponSlot.Top, cbWeapon1.SelectedIndex,
                cbWeapon1.SelectedItem, intAmmo1.Value, rbWeapon1.Checked);

            saveWeapon(weapons, GearGame.WeaponSlot.Left, cbWeapon2.SelectedIndex,
                cbWeapon2.SelectedItem, intAmmo2.Value, rbWeapon2.Checked);

            saveWeapon(weapons, GearGame.WeaponSlot.Bottom, cbWeapon3.SelectedIndex,
                cbWeapon3.SelectedItem, intAmmo3.Value, rbWeapon3.Checked);

            saveWeapon(weapons, GearGame.WeaponSlot.Right, cbWeapon4.SelectedIndex,
                cbWeapon4.SelectedItem, intAmmo4.Value, rbWeapon4.Checked);
        }

        private void saveWeapon(List<GearGame.GearWeap> weapons, GearGame.WeaponSlot slot,
            int index, object selectedWeapon, int ammo, bool equipped)
        {
            GearGame.GearWeap weapon = getWeapon(weapons, slot);
            if (weapon != null && index == 0)
                weapons.Remove(weapon);
            else if (index != 0)
            {
                if (weapon == null)
                {
                    weapon = new GearGame.GearWeap(slot);
                    weapons.Add(weapon);
                }
                weapon.Name = nameToTag(Weapons, (string)selectedWeapon);
                weapon.Ammo = ammo;
                weapon.Equipped = equipped;
            }
        }

        private GearGame.GearWeap getWeapon(List<GearGame.GearWeap> weapons, GearGame.WeaponSlot slot)
        {
            foreach (GearGame.GearWeap weapon in weapons)
                if (weapon.Slot == slot)
                    return weapon;
            return null;
        }

        private GearGame.Coordinates getCurrentLocation()
        {
            return new GearGame.Coordinates()
            {
                X = (float)floatX.Value,
                Y = (float)floatY.Value,
                Z = (float)floatZ.Value
            };
        }

        private void loadLocation(GearGame.Coordinates location)
        {
            floatX.Value = location.X;
            floatY.Value = location.Y;
            floatZ.Value = location.Z;
        }

        private void populateWeapon(List<GearGame.GearWeap> weapons, GearGame.WeaponSlot slot,
            ComboBoxEx comboBox, IntegerInput intAmmo, RadioButton radioButton)
        {
            GearGame.GearWeap weapon = getWeapon(weapons, slot);
            if (weapon == null)
                comboBox.SelectedIndex = 0;
            else
            {
                comboBox.SelectedItem = tagToName(Weapons, weapon.Name);
                intAmmo.Value = weapon.Ammo;
                radioButton.Checked = weapon.Equipped;
            }
        }

        private void populateWeapons(List<GearGame.GearWeap> weapons)
        {
            isBusy = true;
            populateWeapon(weapons, GearGame.WeaponSlot.Top, cbWeapon1, intAmmo1, rbWeapon1);
            populateWeapon(weapons, GearGame.WeaponSlot.Left, cbWeapon2, intAmmo2, rbWeapon2);
            populateWeapon(weapons, GearGame.WeaponSlot.Bottom, cbWeapon3, intAmmo3, rbWeapon3);
            populateWeapon(weapons, GearGame.WeaponSlot.Right, cbWeapon4, intAmmo4, rbWeapon4);
            isBusy = false;

            cbWeapon1_SelectedIndexChanged(null, null);
            cbWeapon2_SelectedIndexChanged(null, null);
            cbWeapon3_SelectedIndexChanged(null, null);
            cbWeapon4_SelectedIndexChanged(null, null);
        }

        private Node createCharacterNode(string charName, object tag)
        {
            Node node = new Node(charName);
            node.Tag = tag;
            return node;
        }

        private bool isBusy = false;
        private void comboSaves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSaves.SelectedIndex == -1 || isBusy)
                return;

            saveCurrentData();

            loadCheckpoint(comboSaves.SelectedIndex);
        }

        public override void Save()
        {
            saveCurrentData();
            foreach (ComboMeta c in GameSaves)
                Package.StfsContentPackage.InjectFileFromArray(c.FileName, c.GameSave.ToArray());
        }

        private void cmdExtractStructs_Click(object sender, EventArgs e)
        {
#if INT2
            //dumpTags("GearGameContentWeapons.GearWeap_");
            //return;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (IO == null || fbd.ShowDialog() != DialogResult.OK)
                return;

            foreach (KeyValuePair<string, GearGame.Struct> s in GameSave.Structs)
                System.IO.File.WriteAllBytes(fbd.SelectedPath + "\\" + s.Key.Replace(':', '.'), s.Value.Data);
#endif
        }

#if INT2
        private void dumpTags(string prefix)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            string bin = System.IO.File.ReadAllText(ofd.FileName).Replace("\0\0", "|").Replace("\0", "");
            int f, l = 0;
            List<string> sb = new List<string>();
            while ((f = bin.IndexOf(prefix, l)) != -1)
            {
                string mod = bin.Substring(f, bin.IndexOf('|', f) - f);
                if (!sb.Contains(mod))
                    sb.Add(mod);
                l = f + 1;
            }
            System.IO.File.WriteAllLines("C:\\tag_dump.txt", sb.ToArray());
        }
#endif

        private void cmdMaxAmmo_Click(object sender, EventArgs e)
        {
            if (intAmmo1.Enabled)
                intAmmo1.Value = intAmmo1.MaxValue;
            if (intAmmo2.Enabled)
                intAmmo2.Value = intAmmo2.MaxValue;
            if (intAmmo3.Enabled)
                intAmmo3.Value = intAmmo3.MaxValue;
            if (intAmmo4.Enabled)
                intAmmo4.Value = intAmmo4.MaxValue;
        }

        private void updateEquippedWeapons()
        {
            if (!rbWeapon1.Checked && !rbWeapon2.Checked && !rbWeapon3.Checked && !rbWeapon4.Checked)
            {
                if (rbWeapon1.Enabled)
                    rbWeapon1.Checked = true;
                else if (rbWeapon2.Enabled)
                    rbWeapon2.Checked = true;
                else if (rbWeapon3.Enabled)
                    rbWeapon3.Checked = true;
                else if (rbWeapon4.Enabled)
                    rbWeapon4.Checked = true;
            }
        }

        private void cbWeapon1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon1.SelectedIndex == 0)
            {
                intAmmo1.Value = 0;
                intAmmo1.Enabled = false;
                rbWeapon1.Checked = false;
                rbWeapon1.Enabled = false;
            }
            else
            {
                intAmmo1.Enabled = true;
                rbWeapon1.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cbWeapon2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon2.SelectedIndex == 0)
            {
                intAmmo2.Value = 0;
                intAmmo2.Enabled = false;
                rbWeapon2.Checked = false;
                rbWeapon2.Enabled = false;
            }
            else
            {
                intAmmo2.Enabled = true;
                rbWeapon2.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cbWeapon3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon3.SelectedIndex == 0)
            {
                intAmmo3.Value = 0;
                intAmmo3.Enabled = false;
                rbWeapon3.Checked = false;
                rbWeapon3.Enabled = false;
            }
            else
            {
                intAmmo3.Enabled = true;
                rbWeapon3.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cbWeapon4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon4.SelectedIndex == 0)
            {
                intAmmo4.Value = 0;
                intAmmo4.Enabled = false;
                rbWeapon4.Checked = false;
                rbWeapon4.Enabled = false;
            }
            else
            {
                intAmmo4.Enabled = true;
                rbWeapon4.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            Functions.UI.messageBox("The game will not function correctly if you choose a weapon or model that is not loaded\n"
                + "in the current chapter. For example, there are no Kantus' in the first chapter, so the model\nwon't show in-game. "
                + "Lambent, however, are present in the first chapter, so they can be used.", "Gears of War 3 Notice", MessageBoxIcon.Information);
        }

        private void cmdExtractState_Click(object sender, EventArgs e)
        {
#if INT2
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Segment.bin";
            if (IO == null || sfd.ShowDialog() != DialogResult.OK)
                return;

            GameSave.Data.GetSegmentIO().ToArray().Save(sfd.FileName);
            //GameSave.Data.GetSingleSegmentIO(0).ToArray().Save(sfd.FileName);
#endif
        }
    }
}
