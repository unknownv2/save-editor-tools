using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Horizon.Properties;
using DevComponents.DotNetBar;
using Horizon.Server;
using Horizon.Functions;
using Horizon.PackageEditors;
using System.Xml.XPath;
using Horizon.Forms;

namespace Horizon
{
    internal static class FormConfig
    {
        // Add forms to the list. All modders and misc forms should be listed here.
        internal static List<FormMeta> formList = new List<FormMeta>();
        internal static void populateForms()
        {
            #if PNET // Incomplete and Private Editors
            addForm(FormID.Battlefield4, typeof(PackageEditors.Battlefield_4.Battlefield4), "* Battlefield 4", FormType.Game_Modder, Resources.BBC2_Thumb, FormAccess.Anyone);
            addForm(FormID.Diablo3, typeof(PackageEditors.Diablo_III.DiabloIII), "* Diablo III", FormType.Game_Modder, Resources.Darksiders_Thumb, FormAccess.Anyone);
            addForm(FormID.Grid2, typeof(PackageEditors.Grid_2.Grid2), "* GRID 2", FormType.Game_Modder, Resources.Forza4_Thumb_New, FormAccess.Anyone);
            addForm(FormID.MetroLastLight, typeof(PackageEditors.Metro_Last_Light.MetroLastLightConfig), "* Metro: Last Light", FormType.Game_Modder, Resources.Metro_2033_Thumb, FormAccess.Anyone);
            //addForm(FormID.HReachMegaloEditor, typeof(PackageEditors.Halo_Reach.MP.HReachMegaloEditor), "* Halo Reach Game Types", FormType.Game_Modder, Resources.HaloReach_Thumb, FormAccess.Anyone);
            //addForm(FormID.H4MegaloEditor, typeof(PackageEditors.Halo_4.MP.H4MegaloEditor), "* Halo 4 Megalo", FormType.Game_Modder, Resources.Halo4_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Skyrim.Skyrim.FID, "* Skyrim", FormType.Game_Modder, Resources.Prototype_Thumb, FormAccess.Anyone);

            //addForm(PackageEditors._PROTOTYPE_.Prototype.FID, "* [PROTOTYPE]", FormType.Game_Modder, Resources.Prototype_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Battlefield_BFC2.BattlefieldBC2.FID, "* Battlefield: BC 2", FormType.Game_Modder, Resources.BBC2_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Captain_America.CaptainAmerica.FID, "* Captain America: Super Soldier", FormType.Game_Modder, Resources.AC2_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Darksiders.Darksiders.FID, "* Darksiders", FormType.Game_Modder, Resources.Darksiders_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.FEAR_3.FEAR3_SaveEditor.FID, "* FEAR 3", FormType.Game_Modder, Resources.FEAR_2_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Homefront.Homefront.FID, "* Homefront", FormType.Game_Modder, Resources.Blur_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Halo_Anniversary.HaloAnniversary.FID, "* Halo: CEA", FormType.Game_Modder, Resources.Blur_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Joe_Danger_SE.JoeDangerSE.FID, "* Joe Danger SE", FormType.Game_Modder, Resources.Blur_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Mercury_Hg.MercuryHg.FID, "* Mercury Hg", FormType.Game_Modder, Resources.MercuryHg_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.NHL_12.NHL12_CardEditor.FID, "* NHL 12", FormType.Game_Modder, Resources.MercuryHg_Thumb, FormAccess.Anyone);
            //addForm(PackageEditors.Lost_Odyssey.LostOdyssey.FID, "* Lost Odyssey", formType.Game_Modder, Resources.LostOdyssey_Thumb, formAccess.Anyone);
            //addForm(PackageEditors.Bioshock_2.Bioshock2.FID, "* Bioshock 2", formType.Game_Modder, Resources.Bioshock_2_Thumb, formAccess.Anyone);
            #endif

            #if INT2 // Private Forms - Devs Only
            addForm(FormID.TitleCrawler, null, "Game Adder Helper", FormType.Tool, Resources.Adder_Thumb, FormAccess.Anyone);
            addForm(FormID.TitleSettingsManager, null, "Title Settings Manager", FormType.Tool, Resources.AvatarAward_Thumb, FormAccess.Anyone);
            #endif

            // Tools
            addForm(FormID.FATX, null, "Device Explorer", FormType.Tool, Resources.DeviceExplorer_Thumb, FormAccess.Anyone);
            addForm(FormID.GamerPictureManager, typeof(PackageEditors.Gamer_Picture_Manager.GamerPictureManager), "Gamer Pic Pack Creator", FormType.Tool, Resources.GamerPictureManager_Thumb, FormAccess.Anyone);
            addForm(FormID.GamercardViewer, typeof(GamercardViewer), "Gamercard Viewer", FormType.Tool, Resources.GamercardViewer_Thumb, FormAccess.Anyone);
            addForm(FormID.PackageManager, typeof(PackageEditors.Package_Manager.PackageManager), "Package Manager", FormType.Tool, Resources.Manager_Thumb, FormAccess.Anyone);
            addForm(FormID.ThemeCreator, typeof(PackageEditors.Theme_Creator.ThemeCreator), "Theme Creator", FormType.Tool, Resources.Theme_Thumb, FormAccess.Anyone);
            addForm(FormID.TitleIDFinder, typeof(TitleIDFinder), "Title ID Finder", FormType.Tool, Resources.FindTID_Thumb, FormAccess.Anyone);

            // Profile Modders
            addForm(FormID.AccountEditor, typeof(PackageEditors.Account_Editor.AccountEditor), "Account Editor", FormType.Profile_Modder, Resources.Account_Thumb, FormAccess.Anyone);
            addForm(FormID.AchievementUnlocker, typeof(PackageEditors.Achievement_Unlocker.AchievementUnlocker), "Achievement Unlocker", FormType.Profile_Modder, Resources.Unlocker_Thumb, FormAccess.Diamond, false);
            addForm(FormID.AvatarAwardUnlocker, typeof(PackageEditors.Avatar_Award_Unlocker.AvatarAwardUnlocker), "Avatar Award Unlocker", FormType.Profile_Modder, Resources.AvatarAward_Thumb, FormAccess.Diamond);
            addForm(FormID.AvatarColorEditor, typeof(PackageEditors.Avatar_Color_Editor.AvatarColorEditor), "Avatar Color Editor", FormType.Profile_Modder, Resources.AvatarEditor_Thumb, FormAccess.Anyone);
            addForm(FormID.GameAdder, typeof(PackageEditors.Game_Adder.GameAdder), "Game Adder", FormType.Profile_Modder, Resources.Adder_Thumb, FormAccess.Diamond);
            addForm(FormID.ProfileDataEditor, typeof(PackageEditors.Profile_Data_Editor.ProfileDataEditor), "Profile Data Editor", FormType.Profile_Modder, Resources.ProfileEditor_Thumb, FormAccess.Anyone);
            addForm(FormID.BattleBlockTheater, typeof(PackageEditors.BattleBlock_Theater.BattleBlockTheater), "BattleBlock Theater", FormType.GPD_Modder, Resources.BattleBlock_Theater_Thumb, FormAccess.Diamond);
            addForm(FormID.CastleCrashers, typeof(PackageEditors.Castle_Crashers.CastleCrashers), "Castle Crashers", FormType.GPD_Modder, Resources.CC_Thumb, FormAccess.Anyone);
            addForm(FormID.Crysis2Profile, typeof(PackageEditors.Crysis_2.Crysis2Profile), "Crysis 2 Online", FormType.GPD_Modder, Resources.Crysis2_Thumb, FormAccess.Diamond);
            addForm(FormID.GearsOfWar3ProfileData, typeof(PackageEditors.Gears_of_War_3.ProfileData), "Gears of War 3 Profile", FormType.GPD_Modder, Resources.GoW3_Profile_Thumb_New, FormAccess.Diamond);
            addForm(FormID.GearsOfWarJudgmentProfileEditor, typeof(PackageEditors.Gears_of_War_Judgment.Profile.ProfileEditor), "GoW: Judgment Profile", FormType.GPD_Modder, Resources.GoWJ_Stats_Thumb_New, FormAccess.Diamond);
            addForm(FormID.Halo4Profile, typeof(PackageEditors.Halo_4.Halo4Profile), "Halo 4 Profile", FormType.GPD_Modder, Resources.Halo4_Thumb_New, FormAccess.Diamond);
            addForm(FormID.MarbleBlastUltra, typeof(PackageEditors.Marble_Blast_Ultra.MarbleBlastUltra), "Marble Blast Ultra", FormType.GPD_Modder, Resources.MarbleBlastUltra_Thumb, FormAccess.Anyone);
            addForm(FormID.NPlus, typeof(PackageEditors.NPlus.NPlus), "N+", FormType.GPD_Modder, Resources.NPlus_Thumb, FormAccess.Anyone);
            addForm(FormID.HaloReachSettings, typeof(PackageEditors.Halo_Reach.HaloReachSettings), "Reach Credit Editor", FormType.GPD_Modder, Resources.HaloReach_Thumb_New, FormAccess.Anyone);
            addForm(FormID.RedFactionGuerrilla, typeof(PackageEditors.Red_Faction_Guerrilla.RedFactionGuerrilla), "Red Faction: Guerrilla", FormType.GPD_Modder, Resources.RedFaction_Thumb, FormAccess.Anyone);
            addForm(FormID.Swarm, typeof(PackageEditors.Swarm.Swarm), "Swarm", FormType.GPD_Modder, Resources.Swarm_Thumb, FormAccess.Anyone);

            // Game Editors
            addForm(FormID.AlanWake, typeof(PackageEditors.Alan_Wake.AlanWake), "Alan Wake", FormType.Game_Modder, Resources.AlanWake_Thumb, FormAccess.Diamond);
            addForm(FormID.AssassinsCreedII, typeof(PackageEditors.Assassins_Creed_II.AssassinsCreedII), "Assassin's Creed II", FormType.Game_Modder, Resources.AC2_Thumb, FormAccess.Anyone);
            addForm(FormID.AssassinsCreedIII, typeof(PackageEditors.Assassin_s_Creed_III.ACIII), "Assassin's Creed III", FormType.Game_Modder, Resources.AC3_Thumb, FormAccess.Anyone);
            addForm(FormID.AssassinsCreedRogue, typeof(PackageEditors.Assassin_s_Creed_Rogue.ACRogue), "Assassin's Creed Rogue", FormType.Game_Modder, Resources.ACRogue_Thumb, FormAccess.Anyone);
            addForm(FormID.AssassinsCreedBrotherhood, typeof(PackageEditors.Assassin_s_Creed_Brotherhood.ACBrotherhood), "AC: Brotherhood", FormType.Game_Modder, Resources.ACBrotherhood_Thumb, FormAccess.Anyone);
            addForm(FormID.ACRevelations, typeof(PackageEditors.Assassin_s_Creed_Revelations.ACRevelations), "AC: Revelations", FormType.Game_Modder, Resources.ACR_Thumb, FormAccess.Anyone);
            addForm(FormID.AssassinsCreedIV, typeof(PackageEditors.Assassin_s_Creed_IV.ACIV), "AC IV: Black Flag", FormType.Game_Modder, Resources.AC4_Thumb, FormAccess.Diamond);
            addForm(FormID.Bastion, typeof(PackageEditors.Bastion.Bastion), "Bastion", FormType.Game_Modder, Resources.Bastion_Thumb, FormAccess.Anyone);
            addForm(FormID.Bayonetta, typeof(PackageEditors.Bayonetta.Bayonetta), "Bayonetta", FormType.Game_Modder, Resources.Bayonetta_Thumb, FormAccess.Anyone);
            addForm(FormID.BionicCommando, typeof(PackageEditors.Bionic_Commando.BionicCommando), "Bionic Commando", FormType.Game_Modder, Resources.Bionic_Commando_Thumb, FormAccess.Anyone);
            addForm(FormID.BioshockInfinite, typeof(PackageEditors.BioShock_Infinite.BioShockInfinite), "BioShock Infinite", FormType.Game_Modder, Resources.BioShock_Infinite_Thumb, FormAccess.Diamond);
            addForm(FormID.Borderlands, typeof(PackageEditors.Borderlands.Borderlands), "Borderlands", FormType.Game_Modder, Resources.Borderlands_Thumb, FormAccess.Anyone);
            addForm(FormID.Brink, typeof(PackageEditors.Brink.Brink), "Brink", FormType.Game_Modder, Resources.Brink_Thumb, FormAccess.Anyone);
            addForm(FormID.BrutalLegend, typeof(PackageEditors.Brutal_Legend.BrutalLegend), "Brutal Legend", FormType.Game_Modder, Resources.BrutalLegend_Thumb, FormAccess.Anyone);
            addForm(FormID.Bulletstorm, typeof(PackageEditors.Bulletstorm.Bulletstorm), "Bulletstorm", FormType.Game_Modder, Resources.Bulletstorm_Thumb, FormAccess.Anyone);
            addForm(FormID.CallofDutyAdvancedWarfare, typeof(PackageEditors.Call_of_Duty_Advanced_Warfare.CoDAdvancedWarfare), "CoD: Advanced Warfare", FormType.Game_Modder, Resources.CoDAdvancedWarfare_Thumb, FormAccess.Diamond);
            addForm(FormID.CallofDutyGhosts, typeof(PackageEditors.Call_of_Duty_Ghosts.CoDGhosts), "Call of Duty: Ghosts", FormType.Game_Modder, Resources.CoDGhostsThumb, FormAccess.Diamond);
            addForm(FormID.Crackdown, typeof(PackageEditors.Crackdown.Crackdown), "Crackdown", FormType.Game_Modder, Resources.Crackdown_Thumb, FormAccess.Anyone);
            addForm(FormID.Crackdown2, typeof(PackageEditors.Crackdown_2.Crackdown2), "Crackdown 2", FormType.Game_Modder, Resources.Crackdown2_Thumb, FormAccess.Anyone);
            addForm(FormID.Crysis2Save, typeof(PackageEditors.Crysis_2.Crysis2Save), "Crysis 2", FormType.Game_Modder, Resources.Crysis2_Thumb, FormAccess.Anyone);
            addForm(FormID.Crysis3SaveGame, typeof(PackageEditors.Crysis_3.Crysis3SaveGame), "Crysis 3", FormType.Game_Modder, Resources.Crysis3_Thumb, FormAccess.Diamond);
            addForm(FormID.DeadRising2, typeof(PackageEditors.Dead_Rising_2.DeadRising2), "Dead Rising 2", FormType.Game_Modder, Resources.DeadRising2_Thumb, FormAccess.Anyone);
            addForm(FormID.DeadSpace, typeof(PackageEditors.Dead_Space.DeadSpace), "Dead Space", FormType.Game_Modder, Resources.DS_Thumb, FormAccess.Anyone);
            addForm(FormID.DeadSpace2, typeof(PackageEditors.Dead_Space_2.DeadSpace2), "Dead Space 2", FormType.Game_Modder, Resources.DS2_Thumb, FormAccess.Anyone);
            addForm(FormID.DeadSpace3, typeof(PackageEditors.Dead_Space_3.DeadSpace3), "Dead Space 3", FormType.Game_Modder, Resources.DS3_Thumb, FormAccess.ServerDiamond);
            addForm(FormID.DMC3, typeof(PackageEditors.Devil_May_Cry_3.DMC3), "Devil May Cry 3", FormType.Game_Modder, Resources.DevilMayCry3_Thumb, FormAccess.Anyone);
            addForm(FormID.DevilMayCry4, typeof(PackageEditors.Devil_May_Cry_4.DevilMayCry4), "Devil May Cry 4", FormType.Game_Modder, Resources.DevilMayCry4_Thumb, FormAccess.Anyone);
            addForm(FormID.DevilMayCry, typeof(PackageEditors.Devil_May_Cry.DevilMayCry), "DmC: Devil May Cry", FormType.Game_Modder, Resources.DevilMayCry5_Thumb, FormAccess.Anyone);
            addForm(FormID.Dirt2, typeof(PackageEditors.Dirt_2.Dirt2), "DiRT 2", FormType.Game_Modder, Resources.DiRT2_Thumb, FormAccess.Anyone);
            addForm(FormID.Dirt3, typeof(PackageEditors.Dirt_3.Dirt3), "DiRT 3", FormType.Game_Modder, Resources.DiRT3_Thumb, FormAccess.Anyone);
            addForm(FormID.DirtShowdown, typeof(PackageEditors.Dirt_Showdown.DirtShowdown), "DiRT Showdown", FormType.Game_Modder, Resources.DiRTShowdown_Thumb, FormAccess.Diamond);
            addForm(FormID.DragonsDogma, typeof(PackageEditors.Dragons_Dogma.DragonsDogma), "Dragon's Dogma", FormType.Game_Modder, Resources.DragonsDogma_Thumb, FormAccess.Anyone);
            addForm(FormID.DragonballXenoVerse, typeof(PackageEditors.Dragonball_XenoVerse.DragonballXenoVerse), "Dragon Ball XenoVerse", FormType.Game_Modder, Resources.DragonBallXenoVerse_Thumb, FormAccess.Anyone);
            addForm(FormID.FEAR, typeof(PackageEditors.FEAR.FEAR), "F.E.A.R.", FormType.Game_Modder, Resources.FEAR_Thumb, FormAccess.Anyone);
            addForm(FormID.FEAR2, typeof(PackageEditors.FEAR_2.FEAR2), "F.E.A.R. 2", FormType.Game_Modder, Resources.FEAR_2_Thumb, FormAccess.Diamond);
            addForm(FormID.Fable2, typeof(PackageEditors.Fable_2.Fable2), "Fable 2", FormType.Game_Modder, Resources.Fable2_Thumb_New, FormAccess.Anyone);
            addForm(FormID.FarCry3, typeof(PackageEditors.Far_Cry_3.FarCry3), "Far Cry 3", FormType.Game_Modder, Resources.FarCry3_Thumb, FormAccess.Diamond);
            addForm(FormID.FarCry3BloodDragon, typeof(PackageEditors.Far_Cry_3_Blood_Dragon.FarCry3BloodDragon), "Far Cry 3: Blood Dragon", FormType.Game_Modder, Resources.FarCry3BloodDragon_Thumb, FormAccess.Diamond);
            addForm(FormID.FarCry4, typeof(PackageEditors.Far_Cry_4.FarCry4), "Far Cry 4", FormType.Game_Modder, Resources.FarCry4_Thumb, FormAccess.Diamond);
            addForm(FormID.FIFA11, typeof(PackageEditors.FIFA_11.FIFA11), "FIFA Soccer 11", FormType.Game_Modder, Resources.FIFA11_Thumb, FormAccess.Anyone);
            addForm(FormID.FIFA12, typeof(PackageEditors.FIFA_12.FIFA12), "FIFA Soccer 12", FormType.Game_Modder, Resources.FIFA12_Thumb, FormAccess.Diamond);
            addForm(FormID.FIFA13, typeof(PackageEditors.FIFA_13.FIFA13), "FIFA Soccer 13", FormType.Game_Modder, Resources.FIFA13_Thumb, FormAccess.Anyone);
            addForm(FormID.FIFA14, typeof(PackageEditors.FIFA_14.FIFA14), "FIFA Soccer 14", FormType.Game_Modder, Resources.FIFA14_Thumb, FormAccess.Anyone);
            addForm(FormID.FIFA15, typeof(PackageEditors.FIFA_15.FIFA15), "FIFA Soccer 15", FormType.Game_Modder, Resources.FIFA15_Thumb, FormAccess.Anyone);
            addForm(FormID.Forza3, typeof(PackageEditors.Forza_3.Forza3), "Forza Motorsport 3", FormType.Game_Modder, Resources.Forza3_Thumb_New, FormAccess.Diamond);
            addForm(FormID.Forza3SS, typeof(PackageEditors.Forza_3.Forza3SS), "Forza 3 Screenshot", FormType.Game_Modder, Resources.Forza3_SS_Thumb_New, FormAccess.Diamond);
            addForm(FormID.Forza4Profile, typeof(PackageEditors.Forza_4.Forza4Profile), "Forza Motorsport 4", FormType.Game_Modder, Resources.Forza4_Thumb_New, FormAccess.Diamond);
            addForm(FormID.Forza4Ss, typeof(PackageEditors.Forza_4.Forza4Ss), "Forza 4 Screenshot", FormType.Game_Modder, Resources.Forza4_SS_Thumb_New, FormAccess.Diamond);
            addForm(FormID.Forza4Livery, typeof(PackageEditors.Forza_4.Forza4Livery), "Forza 4 Livery Unlocker", FormType.Game_Modder, Resources.Forza4_Livery_Thumb_New, FormAccess.Diamond);
            addForm(FormID.ForzaHorizonProfile, typeof(PackageEditors.Forza_Horizon.ForzaHorizonProfile), "Forza Horizon", FormType.Game_Modder, Resources.ForzaHorizon_Thumb_New, FormAccess.Diamond);
            addForm(FormID.ForzaHorizonLivery, typeof(PackageEditors.Forza_Horizon.ForzaHorizonLivery), "Forza Horizon Livery", FormType.Game_Modder, Resources.ForzaHorizon_Livery_Thumb_New, FormAccess.Diamond);
            addForm(FormID.ForzaHorizonSS, typeof(PackageEditors.Forza_Horizon.ForzaHorizonSS), "Forza Horizon Screenshot", FormType.Game_Modder, Resources.ForzaHorizon_SS_Thumb_New, FormAccess.Diamond);
            addForm(FormID.ForzaHorizon2Profile, typeof(PackageEditors.Forza_Horizon_2.ForzaHorizon2Profile), "Forza Horizon 2", FormType.Game_Modder, Resources.ForzaHorizon2_Thumb_New, FormAccess.ServerDiamond);
            //addForm(FormID.ForzaHorizon2FnFProfile, typeof(PackageEditors.Forza_Horizon_2.ForzaHorizon2FastNFurious), "FH2: Fast & Furious", FormType.Game_Modder, Resources.ForzaHorizon2_FnF_Thumb, FormAccess.Anyone);
            addForm(FormID.GearsOfWar, typeof(PackageEditors.Gears_of_War.GearsOfWar), "Gears of War", FormType.Game_Modder, Resources.GoW_Thumb_New, FormAccess.Anyone);
            addForm(FormID.GearsOfWar2, typeof(PackageEditors.Gears_of_War_2.GearsOfWar2), "Gears of War 2", FormType.Game_Modder, Resources.GoW2_Thumb_New, FormAccess.Anyone);
            addForm(FormID.GearsOfWar3, typeof(PackageEditors.Gears_of_War_3.GearsOfWar3), "Gears of War 3", FormType.Game_Modder, Resources.GoW3_Thumb_New, FormAccess.Anyone);
            addForm(FormID.GearsOfWar3PlayerData, typeof(PackageEditors.Gears_of_War_3.PlayerData), "Gears of War 3 Stats", FormType.Game_Modder, Resources.GoW3_Stats_Thumb_New, FormAccess.User);
            addForm(FormID.GearsOfWarJudgment, typeof(PackageEditors.Gears_of_War_Judgment.Campaign.GearsOfWarJudgment), "Gears of War: Judgment", FormType.Game_Modder, Resources.GoWJ_Thumb_New, FormAccess.Anyone);
            addForm(FormID.GearsOfWarJudgmentStatsEditor, typeof(PackageEditors.Gears_of_War_Judgment.Stats.StatsEditor), "GoW: Judgment Stats", FormType.Game_Modder, Resources.GoWJ_Stats_Thumb_New, FormAccess.Diamond);
            addForm(FormID.GrandTheftAutoIV, typeof(PackageEditors.Grand_Theft_Auto_IV.GrandTheftAutoIV), "Grand Theft Auto IV", FormType.Game_Modder, Resources.GTAIV_Thumb, FormAccess.Anyone);
            addForm(FormID.GrandTheftAutoV, typeof(PackageEditors.Grand_Theft_Auto_V.GrandTheftAutoV), "Grand Theft Auto V", FormType.Game_Modder, Resources.GTAV_Thumb, FormAccess.User);
            addForm(FormID.Halo3, typeof(PackageEditors.Halo_3.Halo3), "Halo 3", FormType.Game_Modder, Resources.Halo3_Thumb_New, FormAccess.User);
            addForm(FormID.Halo3ODST, typeof(PackageEditors.Halo_3_ODST.Halo3ODST), "Halo 3: ODST", FormType.Game_Modder, Resources.Halo3ODST_Thumb_New, FormAccess.Anyone);
            addForm(FormID.HaloReach, typeof(PackageEditors.Halo_Reach.HaloReach), "Halo: Reach", FormType.Game_Modder, Resources.HaloReach_Thumb_New, FormAccess.Diamond);
            addForm(FormID.HReachMegaloEditor, typeof(PackageEditors.Halo_Reach.MP.HReachMegaloEditor), "Halo Reach Game Types", FormType.Game_Modder, Resources.HaloReach_Megalo_Thumb_New, FormAccess.Anyone);
            addForm(FormID.Halo4CampaignEditor, typeof(PackageEditors.Halo_4.Halo4CampaignEditor), "Halo 4", FormType.Game_Modder, Resources.Halo4_Thumb_New, FormAccess.Diamond);
            addForm(FormID.H4MegaloEditor, typeof(PackageEditors.Halo_4.MP.H4MegaloEditor), "Halo 4 Game Types", FormType.Game_Modder, Resources.Halo4_Megalo_Thumb_New, FormAccess.Anyone);
            addForm(FormID.InjusticeGodsAmongUs, typeof(PackageEditors.Injustice_Gods_Among_Us.InjusticeGAU), "Injustice: Gods Among Us", FormType.Game_Modder, Resources.InjusticeGAU_Thumb, FormAccess.Anyone);
            addForm(FormID.JustCause2, typeof(PackageEditors.Just_Cause_2.JustCause2), "Just Cause 2", FormType.Game_Modder, Resources.Just_Cause_Thumb, FormAccess.Anyone);
            addForm(FormID.Left4Dead2, typeof(PackageEditors.Left_4_Dead_2.Left4Dead2), "Left 4 Dead 2", FormType.Game_Modder, Resources.L4D2_Thumb, FormAccess.Anyone);
            addForm(FormID.LIMBO, typeof(PackageEditors.LIMBO.LIMBO), "LIMBO", FormType.Game_Modder, Resources.LIMBO_Thumb, FormAccess.Anyone);
            addForm(FormID.Metro2033, typeof(PackageEditors.Metro_2033.Metro2033), "Metro 2033", FormType.Game_Modder, Resources.Metro_2033_Thumb, FormAccess.Anyone);
            addForm(FormID.MW3CampaignSave, typeof(PackageEditors.Modern_Warfare_3.MW3CampaignSave), "Modern Warfare 3", FormType.Game_Modder, Resources.MW3_Thumb, FormAccess.Anyone);
            addForm(FormID.MotocrossMadness, typeof(PackageEditors.Motorcross_Madness.MotocrossMadness), "Motocross Madness", FormType.Game_Modder, Resources.MotocrossMadness_Thumb, FormAccess.Anyone);
            addForm(FormID.MLB2K13, typeof(PackageEditors.MLB_2K13.MLB2K13), "MLB 2K13", FormType.Game_Modder, Resources.MLB_2k13_Thumb, FormAccess.Anyone);
            addForm(FormID.NarutoUltimateNinjaStorm2, typeof(PackageEditors.Naruto_Ultimate_Ninja_Storm_2.NarutoUltimateNinjaStorm2), "Naruto Ninja Storm 2", FormType.Game_Modder, Resources.NarutoStorm2_Thumb, FormAccess.Anyone);
            addForm(FormID.NarutoUNS3, typeof(PackageEditors.Naruto_Ultimate_Ninja_Storm_3.NarutoUNS3), "Naruto Ninja Storm 3", FormType.Game_Modder, Resources.NarutoStorm3_Thumb, FormAccess.Anyone);
            addForm(FormID.NaughtyBear, typeof(PackageEditors.Naughty_Bear.NaughtyBear), "Naughty Bear", FormType.Game_Modder, Resources.NaughtyBear_Thumb, FormAccess.Anyone);
            addForm(FormID.NBA2K13, typeof(PackageEditors.NBA_2K13.NBA2K13), "NBA 2K13", FormType.Game_Modder, Resources.NBA_2K13Thumb, FormAccess.Anyone);
            addForm(FormID.NBA2K14, typeof(PackageEditors.NBA_2K14.NBA2K14), "NBA 2K14", FormType.Game_Modder, Resources.NBA_2K14Thumb, FormAccess.Anyone);
            addForm(FormID.NBA2K15, typeof(PackageEditors.NBA_2K15.NBA2K15), "NBA 2K15", FormType.Game_Modder, Resources.NBA_2K15_Thumb, FormAccess.Anyone);
            addForm(FormID.NeedForSpeedHP, typeof(PackageEditors.Need_for_Speed_HP.NeedForSpeedHP), "NFS: Hot Pursuit", FormType.Game_Modder, Resources.NeedForSpeedHP_Thumb, FormAccess.Anyone);
            addForm(FormID.Oblivion, typeof(PackageEditors.Oblivion.Oblivion), "Oblivion", FormType.Game_Modder, Resources.Oblivion_Thumb, FormAccess.Anyone);
            addForm(FormID.ProjectGothamRacing4, typeof(PackageEditors.Project_Gotham_Racing_4.PGR4), "PGR 4", FormType.Game_Modder, Resources.PGR4_Thumb, FormAccess.Anyone);
            addForm(FormID.PlantsvsZombies, typeof(PackageEditors.Plants_vs_Zombies.PlantsvsZombies), "Plants vs. Zombies", FormType.Game_Modder, Resources.PlantsVsZombies_Thumb, FormAccess.Anyone);
            addForm(FormID.QuakeArenaArcade, typeof(PackageEditors.Quake_Arena_Arcade.QuakeArenaArcade), "Quake Arena Arcade", FormType.Game_Modder, Resources.QuakeArenaArcade_Thumb, FormAccess.Anyone);
            addForm(FormID.ResidentEvil5, typeof(PackageEditors.Resident_Evil_5.ResidentEvil5), "Resident Evil 5", FormType.Game_Modder, Resources.RE5_Thumb, FormAccess.Anyone);
            addForm(FormID.ResidentEvil6, typeof(PackageEditors.Resident_Evil_6.ResidentEvil6), "Resident Evil 6", FormType.Game_Modder, Resources.RE6_Thumb, FormAccess.Diamond);
            addForm(FormID.ResidetEvil_CVX, typeof(PackageEditors.Resident_Evil_Code_Veronica_X_HD.CodeVeronicaX), "RE Code: Veronica X HD", FormType.Game_Modder, Resources.CodeVeronicaX_Thumb, FormAccess.Anyone);
            addForm(FormID.ResidentEvil_ORC, typeof(PackageEditors.Resident_Evil_ORC.ResidentEvil_ORC), "Resident Evil ORC", FormType.Game_Modder, Resources.REORC_Thumb, FormAccess.Anyone);
            addForm(FormID.Saboteur, typeof(PackageEditors.The_Saboteur.Saboteur), "The Saboteur", FormType.Game_Modder, Resources.Saboteur_Thumb, FormAccess.Anyone);
            addForm(FormID.SaintsRow3, typeof(PackageEditors.SaintsRow.SaintsRow3), "Saints Row: The Third", FormType.Game_Modder, Resources.SaintsRow3_Thumb, FormAccess.ServerDiamond);
            addForm(FormID.SaintsRow4, typeof(PackageEditors.Saint_s_Row_IV.SaintsRowIV), "Saints Row IV", FormType.Game_Modder, Resources.SaintsRow4_Thumb, FormAccess.Anyone);
            addForm(FormID.SaintsRowGOOH, typeof(PackageEditors.Saint_s_Row_GOOH.SaintsRowGOOH), "Saints Row: GooH", FormType.Game_Modder, Resources.SaintsRowGOOH_Thumb, FormAccess.Diamond);
            //addForm(FormID.Skyrim, typeof(PackageEditors.Skyrim.Skyrim), "Skyrim", FormType.Game_Modder, Resources.Skyrim_Thumb, FormAccess.Anyone);
            addForm(FormID.SleepingDogs, typeof(PackageEditors.Sleeping_Dogs.SleepingDogs), "Sleeping Dogs", FormType.Game_Modder, Resources.SleepingDogs_Thumb, FormAccess.Anyone);
            addForm(FormID.SonicTheHedgehog, typeof(PackageEditors.Sonic_The_Hedgehog.SonicTheHedgehog), "Sonic The Hedgehog", FormType.Game_Modder, Resources.Sonic_Thumb, FormAccess.Anyone);
            addForm(FormID.SSXTricky, typeof(PackageEditors.SSX_Tricky.SSXTricky), "SSX 2012", FormType.Game_Modder, Resources.SSXTricky_Thumb, FormAccess.Anyone);
            addForm(FormID.StarWarsTFUII, typeof(PackageEditors.Star_Wars_TFU_II.StarWarsTFUII), "Star Wars: TFU II", FormType.Game_Modder, Resources.StarWarsTFUII_Thumb, FormAccess.Anyone);
            addForm(FormID.StateofDecay, typeof(PackageEditors.State_of_Decay.StateofDecay), "State of Decay", FormType.Game_Modder, Resources.StateOfDecay_Thumb, FormAccess.Diamond);
            addForm(FormID.SuperMeatBoy, typeof(PackageEditors.Super_Meat_Boy.SuperMeatBoy), "Super Meat Boy", FormType.Game_Modder, Resources.SMB_Thumb, FormAccess.User);
            addForm(FormID.TestDriveUnlimited, typeof(PackageEditors.Test_Drive_Unlimited.TestDriveUnlimited), "Test Drive Unlimited", FormType.Game_Modder, Resources.TestDrive_Thumb, FormAccess.Anyone);
            addForm(FormID.TestDriveUnlimited2, typeof(PackageEditors.Test_Drive_Unlimited_2.TestDriveUnlimited2), "Test Drive Unlimited 2", FormType.Game_Modder, Resources.TestDrive2_Thumb, FormAccess.Anyone);
            addForm(FormID.TigerWoodsPGATour14, typeof(PackageEditors.TW_PGA_Tour_14.TWPGATour14), "Tiger Woods PGA Tour 14", FormType.Game_Modder, Resources.TigerWoods14_Thumb, FormAccess.Anyone);
            addForm(FormID.TombRaider, typeof(PackageEditors.Tomb_Raider.TombRaider), "Tomb Raider", FormType.Game_Modder, Resources.TombRaider_Thumb, FormAccess.Anyone);
            addForm(FormID.WatchDogs, typeof(PackageEditors.Watch_Dogs.WatchDogs), "Watch Dogs", FormType.Game_Modder, Resources.WatchDogs_Thumb, FormAccess.Diamond);
            addForm(FormID.WET, typeof(PackageEditors.WET.WET), "WET", FormType.Game_Modder, Resources.WET_Thumb, FormAccess.Anyone);
            addForm(FormID.YuGiOh5Ds, typeof(PackageEditors.Yu_Gi_Oh_5Ds.YuGiOh5Ds), "Yu-Gi-Oh! 5D's", FormType.Game_Modder, Resources.YuGiOh_5DS_Thumb, FormAccess.User);
            addForm(FormID.YuGiOhMD, typeof(PackageEditors.Yu_Gi_Oh_MD.YuGiOhMD), "Yu-Gi-Oh! MD", FormType.Game_Modder, Resources.YuGiOh_MD_Thumb, FormAccess.User);
            
            // Misc
            addForm(FormID.About, null, "About", FormType.Misc, Resources.About_Thumb, FormAccess.Anyone);
            addForm(FormID.Visit, null, "https://github.com/unknownv2", FormType.Misc, Resources.VisitUs_Thumb, FormAccess.Anyone);
        }

        // This function is called when a form button is clicked.
        internal static void formOpenClick(object sender, EventArgs e)
        {
            var bMeta = (ButtonMeta)(sender.GetType() == typeof(ButtonItem) ? ((ButtonItem)sender).Tag : ((ButtonX)sender).Tag);
            if (bMeta.DeviceIndex != -1 && FormHandle.isFatxFileLoaded(bMeta.DeviceIndex, bMeta.FatxPath) != null)
                UI.errorBox("This file is currently open in another editor!");
            else if (FatxHandle.isDeviceWorkerAvailable(bMeta.DeviceIndex))
            {
                FormMeta fMeta = formList[bMeta.FormMetaIndex];
                if (goodDiamondForm(fMeta.Access))
                    reqDiamondForm(FormHandle.createNewFormConfig(bMeta.FormMetaIndex, bMeta.DeviceIndex, bMeta.FatxPath, bMeta.CachePartition));
                else if (fMeta.Access == FormAccess.Diamond && User.isLogged)
                    showUpgradeMessage();
                else if (!mustBeLoggedIn(fMeta.Access))
                    loadForm(FormHandle.createNewFormConfig(bMeta.FormMetaIndex, bMeta.DeviceIndex, bMeta.FatxPath, bMeta.CachePartition));
            }
        }

        internal static void loadNewEditor(int x, EditorControl currentForm, string newFID)
        {
            byte newIndex = getFormMetaIndex(newFID);
            FormAccess newAccess = formList[newIndex].Access;
            if (goodDiamondForm(formList[newIndex].Access))
            {
                FormHandle.Forms[x].ActiveForm = currentForm;
                FormHandle.Forms[x].MetaIndex = newIndex;
                reqDiamondForm(x);
            }
            else if (formList[newIndex].Access == FormAccess.Diamond && User.isLogged)
                showUpgradeMessage();
            else if (!mustBeLoggedIn(formList[newIndex].Access))
            {
                FormHandle.Forms[x].ActiveForm = currentForm;
                FormHandle.Forms[x].MetaIndex = newIndex;
                loadForm(x);
            }
        }

        private static bool goodDiamondForm(FormAccess access)
        {
            return (access == FormAccess.Diamond || access == FormAccess.ServerDiamond) && User.isLogged && User.isDiamond;
        }

        private static bool mustBeLoggedIn(FormAccess access)
        {
            if ((access == FormAccess.Diamond && !User.isLogged) || (access == FormAccess.User && !User.isLogged))
            {
                DialogResult loggedIn =  UI.messageBox("You must be logged in to use this!\n\n",
                    "Not Logged In", MessageBoxIcon.Information, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button2);
                if (loggedIn == DialogResult.Yes)
                {
                    UI.messageBox("Please use the \"Login\" tab to login to your account.");
                    Main.mainForm.tabLogin.Select();
                    Main.mainForm.txtUsername.Select();
                }
                else if (loggedIn == DialogResult.No)
                    Main.mainForm.cmdStatus_Click(null, null);
                return true;
            }
            return false;
        }

        private static void showUpgradeMessage()
        {
            if (UI.messageBox("You must be a Diamond member to use this!\nUpgrade now?", "No Access",
                MessageBoxIcon.Information, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                System.Diagnostics.Process.Start(Config.serverURL + "diamond");
        }

        internal struct ButtonMeta
        {
            public byte FormMetaIndex;
            public int DeviceIndex;
            public string FatxPath;
            public bool CachePartition;
        }

        private static void reqDiamondForm(int x)
        {
            Request req = new Request("form");
            req.addParam("f", FormHandle.Forms[x].Meta.ID);
            req.addParam("i", x.ToString());
            if (FormSettings.formExists(FormHandle.Forms[x].Meta.ID))
                req.addParam("h", (string)FormSettings.getSetting(FormHandle.Forms[x].Meta.ID, 0));
            switch (FormHandle.Forms[x].Meta.ID)
            {
                case FormID.GameAdder:
                    req.addParam("mh", Settings.Default.GameAdder.Hash(HashType.MD5));
                    break;
            }
            req.doRequest();
        }

        internal static Forms.About aboutBox;
        internal static void loadForm(int x)
        {
            if (Main.mainForm.WindowState == FormWindowState.Minimized)
                Main.mainForm.WindowState = FormWindowState.Normal;
            if (FormHandle.Forms[x].Meta.ID == FormID.FATX)
            {
                if (Main.mainForm.cmdDock.Checked)
                {
                    if (Main.mainForm.exFatx.Expanded)
                        for (byte i = 0; i < 3; i++)
                        {
                            System.Threading.Thread.Sleep(200);
                            Main.mainForm.cmdFatxDevicesLoaded.ColorTable = eButtonColor.Orange;
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(200);
                            Main.mainForm.cmdFatxDevicesLoaded.ColorTable = eButtonColor.Blue;
                            Application.DoEvents();
                        }
                    else
                        Main.mainForm.exFatx.Expanded = true;
                }
                else
                    DeviceWindow.Open(Main.mainForm);

                return;
            }

            Main.mainForm.exFatx.Expanded = false;

            switch (FormHandle.Forms[x].Meta.ID)
            {
                case FormID.About:
                    if (aboutBox == null || !(bool)aboutBox.Tag)
                        (aboutBox = new About()).Show();
                    else
                        aboutBox.BringToFront();
                    break;
                #if INT2
                    case FormID.TitleCrawler:
                        new TitleCrawler().Show();
                        break;
                    case FormID.TitleSettingsManager:
                        new TitleSettingsManager().Show();
                        break;
                #endif
                case FormID.Visit:
                    System.Diagnostics.Process.Start(Config.serverURL);
                    break;
                case FormID.GamercardViewer:
                    new GamercardViewer().Show();
                    break;
                case FormID.TitleIDFinder:
                    new TitleIDFinder().Show();
                    break;
                default:
                    FormHandle.tempRefGlass = !FormHandle.Forms[x].Meta.UseMDI;
                    var editorControl = (EditorControl)FormHandle.Forms[x].Meta.ClassType.GetConstructor(new Type[0]).Invoke(null);
                    editorControl.initiateForm(x);
                    break;
            }
        }

        internal static void loadDiamondForm(XPathNavigator nav)
        {
            if (!User.isLogged || !User.isDiamond || !Config.settingExists("session_id"))
                return;
            string data = nav.Value;
            nav.MoveToFirstAttribute();
            string hash = nav.Value;
            nav.MoveToNextAttribute();
            int x = int.Parse(nav.Value);
            nav.MoveToParent();
            if (FormSettings.validHash(FormHandle.Forms[x].Meta.ID, hash))
                loadForm(x);
            else
            {
                if (FormSettings.formExists(FormHandle.Forms[x].Meta.ID))
                    FormSettings.removeForm(FormHandle.Forms[x].Meta.ID);
                FormSettings.addForm(FormHandle.Forms[x].Meta.ID, hash);
                string xml = Security.decryptFormXML(FormHandle.Forms[x].Meta.ID, hash, data);
                if (Security.validFormHash(FormHandle.Forms[x].Meta.ID, xml, hash))
                {
                    Request.parseFormXML(FormHandle.Forms[x].Meta.ID, xml);
                    loadForm(x);
                }
            }
        }

        internal static bool isDiamondForm(string id)
        {
            return formList.FindIndex(curForm => curForm.ID == id
                && (curForm.Access == FormAccess.Diamond
                || curForm.Access == FormAccess.ServerDiamond)) != -1;
        }

        private static void addForm(string ID, Type formType, string FullName, FormType Type, Image Thumbnail, FormAccess Access)
        { addForm(ID, formType, FullName, Type, Thumbnail, Access, true); }
        private static void addForm(string ID, Type formType, string FullName, FormType Type, Image Thumbnail, FormAccess Access, bool UseMDI)
        {
            if (Connection.isOnline || Access == FormAccess.Anyone || Access == FormAccess.ServerDiamond)
            {
                formList.Add(new FormMeta
                                 {
                                     ID = ID,
                                     ClassType = formType,
                                     FullName = FullName,
                                     UseMDI = UseMDI,
                                     Type = Type,
                                     Thumbnail = Thumbnail,
                                     Access = Access
                                 });
            }
        }

        // Populates the tabs with all the forms in the formList.
        internal static void populateTabs()
        {
            #if PNET
            Main.mainForm.tabWIPEditors.Visible = true;
            #endif
            for (byte x = 0; x < formList.Count; x++)
            {
                ButtonMeta handle = new ButtonMeta();
                handle.FormMetaIndex = x;
                handle.DeviceIndex = -1;
                RibbonBar newRibbon = new RibbonBar();
                newRibbon.AutoOverflowEnabled = false;
                newRibbon.Dock = DockStyle.Left;
                newRibbon.MinimumSize = new Size(138, 0);
                newRibbon.Name = "rib" + formList[x].ID;
                newRibbon.Text = formList[x].FullName;
                newRibbon.Location = new Point(x, 0);
                ButtonItem newButton = new ButtonItem();
                newButton.Shape = new RoundRectangleShapeDescriptor();
                newButton.ColorTable = eButtonColor.OrangeWithBackground;
                newButton.Image = formList[x].Thumbnail;
                newButton.Name = "but" + formList[x].ID;
                newButton.Tag = handle;
                newButton.Click += new EventHandler(formOpenClick);
                newRibbon.Items.Add(newButton);
                #if PNET
                if (formList[x].FullName.Contains("* "))
                    Main.mainForm.panelWIPMods.Controls.Add(newRibbon);
                else
                #endif
                    switch (formList[x].Type)
                    {
                        case FormType.Game_Modder:
                            Main.mainForm.panelGameMods.Controls.Add(newRibbon);
                            break;
                        case FormType.GPD_Modder:
                        case FormType.Profile_Modder:
                            Main.mainForm.panelProfileMods.Controls.Add(newRibbon);
                            break;
                        case FormType.Tool:
                            Main.mainForm.panelTools.Controls.Add(newRibbon);
                            break;
                        case FormType.Misc:
                            Main.mainForm.panelMisc.Controls.Add(newRibbon);
                            break;
                    }
            }
        }

        internal static byte getFormMetaIndex(string fid)
        {
            int x = formList.FindIndex(curForm => curForm.ID == fid);
            if (x == -1)
                return 255;
            return (byte)x;
        }

        // Different types of forms go in different tabs.
        internal enum FormType
        {
            Game_Modder,
            GPD_Modder,
            Profile_Modder,
            Tool,
            Misc
        }

        internal enum FormAccess
        {
            Anyone,
            ServerDiamond,
            User,
            Diamond
        }

        // Data stored for each form in the project.
        internal struct FormMeta
        {
            public string ID;
            public Type ClassType;
            public string FullName;
            public bool UseMDI;
            public FormType Type;
            public Image Thumbnail;
            public FormAccess Access;
        }
    }
}
