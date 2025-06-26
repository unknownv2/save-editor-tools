using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Horizon.Functions;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using System.IO;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;

namespace Horizon.PackageEditors.Gears_of_War_3
{
    public partial class PlayerData : EditorControl
    {
        //public static readonly string FID = "4D5308AX";
        public PlayerData()
        {
            InitializeVars();

            InitializeComponent();
            TitleID = FormID.GearsOfWar3;

            cmdOnlineMods1.Visible = true;
            cmdOnlineMods2.Visible = true;
            cmdOnlineMods3.Visible = true;
            cmdOnlineMods4.Visible = true;
            cmdOnlineMods5.Visible = true;
            cmdOnlineMods6.Visible = true;
            cmdOnlineMods7.Visible = true;

#if INT2
            cmdDumpStats.Visible = true;
            cmdPackage.Visible = true;
            cmdLoadRaw.Visible = true;
            cmdDecompress.Visible = true;
#endif

            GameModeCombos = new ComboBoxEx[]
            {
                cbModeTotals,
                cbModeWeapons,
                cbModeExecutions
            };

            foreach (ComboBoxEx cb in GameModeCombos)
            {
                cb.Items.AddRange(new object[]
                {
                    "Campaign",
                    "Versus",
                    "Horde",
                    "Beast"
                });
                cb.SelectedIndex = 0;
            }

            string[][] VersusGameModes = new string[][]
            {
                new string[] { "Capture the Leader", "CTL" },
                new string[] { "King of the Hill", "KOTH" },
                new string[] { "Warzone", "Warzone" },
                new string[] { "Execution", "Execution" },
                new string[] { "Team Deathmatch", "TDM" },
                new string[] { "Wingman", "Wingman" }
            };

            for (int x = 0; x < VersusGameModes.Length; x++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = VersusGameModes[x][0];
                item.Tag = string.Format("Versus_{0}_", VersusGameModes[x][1]);
                cbGameType.Items.Add(item);
            }

            // Populate Main Stats
            intXp.Tag = EntryID.Experience;
            registerIntPanel(panelMainStats);
            for (int x = 1; x < 101; x++)
                cbRank.Items.Add(x);
            registerIntPanel(panelTotals);

            intCampaignTotalScore.Tag = EntryID.Campaign_Total_Score;
            intCampaignCinematicsPlayed.Tag = EntryID.Campaign_Cinematic_Chapters_Played;
            intCampaignArcadesPlayed.Tag = EntryID.Campaign_Arcade_Chapters_Played;
            registerIntPanel(panelCampaign);

            intVersusTotalScore.Tag = EntryID.Total_Score_Versus;
            intVersusMatchesPlayed.Tag = EntryID.Versus_Matches_Played;
            intVersusMatchesWon.Tag = EntryID.Versus_Matches_Won;
            intVersusMatchesLost.Tag = EntryID.Versus_Matches_Lost;
            intVersusMatchMVPs.Tag = EntryID.Versus_Match_MVPs;
            intVersusRoundsWon.Tag = EntryID.Versus_Rounds_Won;
            intVersusRoundsLost.Tag = EntryID.Versus_Rounds_Lost;
            intVersusRoundsDrawn.Tag = EntryID.Versus_Rounds_Drawn;
            intVersusRoundMVPs.Tag = EntryID.Versus_Round_MVPs;
            registerIntPanel(panelVersus);

            intHordeHighScore.Tag = EntryID.High_Score_Horde;
            intHordeMatchesWon.Tag = EntryID.Horde_Matches_Won;
            intHordeWavesWon.Tag = EntryID.Horde_Waves_Won;
            intHordeWavesLost.Tag = EntryID.Horde_Waves_Lost;
            intHordeCashEarned.Tag = EntryID.Horde_Cash_Earned;
            intHordeCashSpent.Tag = EntryID.Horde_Cash_Spent;
            intHordeFlagInvestment.Tag = EntryID.Horde_Flag_Investment;
            registerIntPanel(panelHorde);

            intBeastMatchesWon.Tag = EntryID.Beast_Matches_Won;
            intBeastWavesWon.Tag = EntryID.Beast_Waves_Won;
            intBeastWavesLost.Tag = EntryID.Beast_Waves_Lost;
            intBeastBarriersDestroyed.Tag = EntryID.Beast_Barrier_Destroyed;
            intBeastDecoysDestroyed.Tag = EntryID.Beast_Decoy_Destroyed;
            intBeastSentriesDestroyed.Tag = EntryID.Beast_Sentry_Destroyed;
            intBeastSilverbacksDestroyed.Tag = EntryID.Beast_Silverback_Destroyed;
            intBeastTurretsDestroyed.Tag = EntryID.Beast_Turret_Destroyed;
            registerIntPanel(panelBeast);

            registerIntPanel(panelExecutions);

            RegisterSkin(cmdSkinGold, "Lancer", EntryID.Skin_Gold_Lancer);
            RegisterSkin(cmdSkinGold, "Hammerburst", EntryID.Skin_Gold_Hammerburst);
            RegisterSkin(cmdSkinGold, "Retro Lancer", EntryID.Skin_Gold_Retro_Lancer);
            RegisterSkin(cmdSkinGold, "Gnasher Shotgun", EntryID.Skin_Gold_Gnasher);
            RegisterSkin(cmdSkinGold, "Sawed-off Shotgun", EntryID.Skin_Gold_SawedOff);
            RegisterSkin(cmdSkinGold, "Gold Omen Set", EntryID.Skin_Gold_Omen_Set);

            RegisterSkin(cmdSkinFlaming, "Lancer", EntryID.Skin_Flaming_Lancer);
            RegisterSkin(cmdSkinFlaming, "Hammerburst", EntryID.Skin_Flaming_Hammerburst);
            RegisterSkin(cmdSkinFlaming, "Gnasher Shotgun", EntryID.Skin_Flaming_Gnasher_Shotgun);
            RegisterSkin(cmdSkinFlaming, "Sawed-off Shotgun", EntryID.Skin_Flaming_SawedOff_Shotgun);

            RegisterSkin(cmdSkinTeamInsignia, "Lancer", EntryID.Skin_Team_Insignia_Lancer);
            RegisterSkin(cmdSkinTeamInsignia, "Hammerburst", EntryID.Skin_Team_Insignia_Hammerburst);
            RegisterSkin(cmdSkinTeamInsignia, "Retro Lancer", EntryID.Skin_Team_Insignia_Retro_Lancer);
            RegisterSkin(cmdSkinTeamInsignia, "Shotgun Set", EntryID.Skin_Team_Insignia_Shotgun_Set);

            RegisterSkin(cmdSkinCrimsonOmen, "Hammerburst", EntryID.Skin_Crimson_Omen_Hammerburst);
            RegisterSkin(cmdSkinCrimsonOmen, "Retro Lancer", EntryID.Skin_Crimson_Omen_Retro_Lancer);
            RegisterSkin(cmdSkinCrimsonOmen, "Gnasher Shotugn", EntryID.Skin_Crimson_Omen_Gnasher);
            RegisterSkin(cmdSkinCrimsonOmen, "Sawed-off Shotgun", EntryID.Skin_Crimson_Omen_SawedOff);

            RegisterSkin(cmdSkinChrome, "Lancer", EntryID.Skin_Chrome_Lancer);
            RegisterSkin(cmdSkinChrome, "Hammerburst", EntryID.Skin_Chrome_Hammerburst);
            RegisterSkin(cmdSkinChrome, "Retro Lancer", EntryID.Skin_Chrome_Retro_Lancer);
            RegisterSkin(cmdSkinChrome, "Gnasher Shotgun", EntryID.Skin_Chrome_Gnasher_Shotgun);
            RegisterSkin(cmdSkinChrome, "Sawed-off Shotgun", EntryID.Skin_Chrome_SawedOff_Shotgun);

            RegisterSkin(cmdSkinOnyx, "Lancer", EntryID.Skin_Onyx_Lancer);
            RegisterSkin(cmdSkinOnyx, "Hammerburst", EntryID.Skin_Onyx_Hammerburst);
            RegisterSkin(cmdSkinOnyx, "Retro Lancer", EntryID.Skin_Onyx_Retro_Lancer);
            RegisterSkin(cmdSkinOnyx, "Gnasher Shotgun", EntryID.Skin_Onyx_Gnasher_Shotgun);
            RegisterSkin(cmdSkinOnyx, "Sawed-off Shotgun", EntryID.Skin_Onyx_SawedOff_Shotgun);

            intFortBarrierInvestment.Tag = EntryID.Horde_Barrier_Investment;
            RegisterFortification(cbFortBarrierLevel, new EntryID[]
            {
                EntryID.Horde_Barrier_Level_1,
                EntryID.Horde_Barrier_Level_2,
                EntryID.Horde_Barrier_Level_3,
                EntryID.Horde_Barrier_Level_4,
                EntryID.Horde_Barrier_Level_5,
                EntryID.Horde_Barrier_Level_6,
                EntryID.Horde_Barrier_Level_7,
                EntryID.Horde_Barrier_Level_8
            }, cmdFortBarrier, intFortBarrierInvestment);

            intFortDecoyInvestment.Tag = EntryID.Horde_Decoy_Investment;
            RegisterFortification(cbFortDecoyLevel, new EntryID[]
            {
                EntryID.Horde_Decoy_Level_1,
                EntryID.Horde_Decoy_Level_2,
                EntryID.Horde_Decoy_Level_3,
                EntryID.Horde_Decoy_Level_4,
                EntryID.Horde_Decoy_Level_5,
                EntryID.Horde_Decoy_Level_6,
                EntryID.Horde_Decoy_Level_7,
                EntryID.Horde_Decoy_Level_8
            }, cmdFortDecoy, intFortDecoyInvestment);

            intFortSentryInvestment.Tag = EntryID.Horde_Sentry_Investment;
            RegisterFortification(cbFortSentryLevel, new EntryID[]
            {
                EntryID.Horde_Sentry_Level_1,
                EntryID.Horde_Sentry_Level_2,
                EntryID.Horde_Sentry_Level_3,
                EntryID.Horde_Sentry_Level_4,
                EntryID.Horde_Sentry_Level_5,
                EntryID.Horde_Sentry_Level_6,
                EntryID.Horde_Sentry_Level_7
            }, cmdFortSentry, intFortSentryInvestment);

            intFortSilverbackInvestment.Tag = EntryID.Horde_Silverback_Investment;
            RegisterFortification(cbFortSilverbackLevel, new EntryID[]
            {
                EntryID.Horde_Silverback_Level_1,
                EntryID.Horde_Silverback_Level_2,
                EntryID.Horde_Silverback_Level_3,
                EntryID.Horde_Silverback_Level_4,
                EntryID.Horde_Silverback_Level_5
            }, cmdFortSilverback, intFortSilverbackInvestment);

            intFortTurretInvestment.Tag = EntryID.Horde_Turret_Investment;
            RegisterFortification(cbFortTurretLevel, new EntryID[]
            {
                EntryID.Horde_Turret_Level_1,
                EntryID.Horde_Turret_Level_2,
                EntryID.Horde_Turret_Level_3,
                EntryID.Horde_Turret_Level_4,
                EntryID.Horde_Turret_Level_5,
                EntryID.Horde_Turret_Level_6,
                EntryID.Horde_Turret_Level_7,
                EntryID.Horde_Turret_Level_8
            }, cmdFortTurret, intFortTurretInvestment);

            intFortCommandCenterInvestment.Tag  = EntryID.Horde_Command_Center_Investment;
            RegisterFortification(cbFortCommandCenter, new EntryID[]
            {
                EntryID.Horde_Command_Center_Level_1,
                EntryID.Horde_Command_Center_Level_2,
                EntryID.Horde_Command_Center_Level_3,
                EntryID.Horde_Command_Center_Level_4
            }, cmdFortCommandCenter, intFortCommandCenterInvestment);

            registerIntPanel(panelFortifications);
        }

        private class FortificationData
        {
            internal IntegerInput Investment;
            internal ComboBoxEx Levels;
            internal EntryID[] LevelEntries;
            internal ButtonX Unlocked;
        }

        private void RegisterFortification(ComboBoxEx fortCombo, EntryID[] entryIds, ButtonX enabledButton, IntegerInput investmentControl)
        {
            FortificationData dat = new FortificationData();
            dat.Unlocked = enabledButton;
            dat.Investment = investmentControl;
            dat.LevelEntries = entryIds;
            dat.Levels = fortCombo;

            fortCombo.Tag = dat;
            for (int x = 0; x < entryIds.Length; x++)
                fortCombo.Items.Add(x + 1);
            enabledButton.Tag = dat;

            if (entryIds[0] != EntryID.Horde_Barrier_Level_1)
                enabledButton.CheckedChanged += new EventHandler(FortificationButton_CheckedChanged);
        }

        private void FortificationButton_CheckedChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            isBusy = true;

            FortificationData dat = (FortificationData)((ButtonX)sender).Tag;
            if (dat.Unlocked.Checked)
            {
                dat.Investment.Value = Stats[(EntryID)dat.Investment.Tag];
                dat.Investment.Enabled = true;
                dat.Levels.Enabled = true;
                for (int x = 0; x < dat.LevelEntries.Length; x++)
                    Stats[dat.LevelEntries[x]] = x <= dat.Levels.SelectedIndex ? 1 : 0;
            }
            else
            {
                dat.Levels.SelectedIndex = 0;
                dat.Levels.Enabled = false;
                dat.Investment.Value = 0;
                Stats[(EntryID)dat.Investment.Tag] = 0;
                dat.Investment.Enabled = false;
                for (int x = 0; x < dat.LevelEntries.Length; x++)
                    Stats[dat.LevelEntries[x]] = 0;
            }

            isBusy = false;
        }

        private void RegisterSkin(ButtonX skinButton, string skinName, EntryID entryId)
        {
            CheckBoxItem item = new CheckBoxItem();
            item.Text = skinName;
            item.Tag = entryId;
            item.CheckedChanged += new CheckBoxChangeEventHandler(Skin_CheckedChanged);
            skinButton.SubItems.Add(item);
        }

        private void Skin_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (isBusy)
                return;

            CheckBoxItem item = (CheckBoxItem)sender;
            Stats[(EntryID)item.Tag] = item.Checked ? 1 : 0;
        }

        private static Dictionary<string, string> Weapons;
        private void LoadWeapons()
        {
            isBusy = true;

            Dictionary<string, Node> WeaponNodes = new Dictionary<string, Node>();

            string suffixTag = (string)cbModeWeapons.SelectedItem;
            lblWeapons.Text = suffixTag + " Mode Executions";
            foreach (Entry entry in Stats)
            {
                if (entry.ID < EntryID.Gnasher_Shotgun_Executions_Campaign || entry.ID > EntryID.Silverback_Rocket_Kills_Beast)
                    continue;

                string strTag = entry.ID.ToString();
                string[] splitTag = strTag.Split('_');
                if (splitTag.Length < 3 || splitTag[splitTag.Length - 1] != suffixTag)
                    continue;

                int typeID;
                bool isWeapon = false;
                string weaponTag = splitTag[0];
                if (Weapons.ContainsKey(weaponTag))
                    typeID = 1;
                else
                {
                    for (typeID = 1; typeID < splitTag.Length - 1; typeID++)
                    {
                        weaponTag += "_" + splitTag[typeID];
                        if (Weapons.ContainsKey(weaponTag))
                        {
                            typeID++;
                            isWeapon = true;
                            break;
                        }
                    }
                    if (!isWeapon)
                        continue;
                }

                StringBuilder statType = new StringBuilder(splitTag[typeID++]);
                for (int x = typeID; x < splitTag.Length - 1; x++)
                {
                    statType.Append(' ');
                    statType.Append(splitTag[x]);
                }

                Node typeNode = new Node(statType.ToString());

                Cell intCell = new Cell();
                IntegerInput i = new IntegerInput();
                i.MinValue = 0;
                i.MaxValue = 999999999;
                i.ShowUpDown = true;
                i.Value = entry.nData;
                i.Tag = entry.ID;
                i.ValueChanged += new EventHandler(WeaponStat_ValueChanged);
                intCell.HostedControl = i;
                typeNode.Cells.Add(intCell);

                if (WeaponNodes.ContainsKey(weaponTag))
                    WeaponNodes[weaponTag].Nodes.Add(typeNode);
                else
                {
                    Node weaponNode = new Node(Weapons[weaponTag]);
                    weaponNode.Cells.Add(new Cell());
                    weaponNode.Nodes.Add(typeNode);
                    WeaponNodes.Add(weaponTag, weaponNode);
                }
            }

            listWeapons.Nodes.Clear();
            listWeapons.BeginUpdate();

            foreach (KeyValuePair<string, Node> weaponNode in WeaponNodes)
            {
                weaponNode.Value.Nodes.Sort();
                listWeapons.Nodes.Add(weaponNode.Value);
            }

            listWeapons.Nodes.Sort();
            listWeapons.EndUpdate();

            isBusy = false;
        }

        private void WeaponStat_ValueChanged(object sender, EventArgs e)
        {
            IntegerInput i = (IntegerInput)sender;
            Stats[(EntryID)i.Tag] = i.Value;
        }

        private void LoadBeastMode()
        {
            populateIntPanel(panelBeast);

            isBusy = true;

            listBeastEnemy.Rows.Clear();
            listBeastKills.Rows.Clear();

            for (int x = 0; x < Stats.Count; x++)
            {
                string idStr = Stats[x].ID.ToString();
                string[] splitTag = idStr.Split('_');
                if (splitTag.Length > 2)
                {
                    string lastTag = splitTag[splitTag.Length - 1];
                    if (splitTag[0] == "Beast" && Stats[x].ID >= EntryID.Beast_Wild_Ticker_Kills
                        && Stats[x].ID <= EntryID.Beast_Savage_Boomer_Spawns)
                    {
                        StringBuilder sb = new StringBuilder(splitTag[1]);
                        for (int i = 2; i < splitTag.Length - 1; i++)
                        {
                            sb.Append(' ');
                            sb.Append(splitTag[i]);
                        }
                        string charName = sb.ToString();

                        DataGridViewRow enRow = null;
                        foreach (DataGridViewRow row in listBeastEnemy.Rows)
                        {
                            if ((string)row.Cells[0].Value == charName)
                            {
                                enRow = row;
                                break;
                            }
                        }
                        if (enRow == null)
                        {
                            enRow = new DataGridViewRow();
                            enRow.CreateCells(listBeastEnemy);
                            enRow.Cells[0].Value = charName;
                            listBeastEnemy.Rows.Add(enRow);
                        }
                        if (lastTag == "Kills")
                        {
                            enRow.Cells[1].Tag = Stats[x].ID;
                            enRow.Cells[1].Value = Stats[x].nData;
                        }
                        else if (lastTag == "Spawns")
                        {
                            enRow.Cells[2].Tag = Stats[x].ID;
                            enRow.Cells[2].Value = Stats[x].nData;
                        }
                    }
                    else if (Stats[x].ID >= EntryID.Stranded_Kills_Beast && Stats[x].ID <= EntryID.Chairman_Prescott_Kills_Beast
                        && lastTag == "Beast" && splitTag[splitTag.Length - 2] == "Kills")
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(listBeastKills);
                        StringBuilder sb = new StringBuilder(splitTag[0]);
                        for (int i = 1; i < splitTag.Length - 2; i++ )
                        {
                            sb.Append(' ');
                            sb.Append(splitTag[i]);
                        }
                        row.Cells[0].Value = sb.ToString();
                        row.Cells[1].Tag = Stats[x].ID;
                        row.Cells[1].Value = Stats[x].nData;
                        listBeastKills.Rows.Add(row);
                    }
                }
            }

            SortGrid(listBeastEnemy);
            SortGrid(listBeastKills);

            isBusy = false;
        }

        private void LoadFortifications()
        {
            populateIntPanel(panelFortifications);

            isBusy = true;

            populateFortification(cbFortBarrierLevel);
            populateFortification(cbFortDecoyLevel);
            populateFortification(cbFortSentryLevel);
            populateFortification(cbFortSilverbackLevel);
            populateFortification(cbFortTurretLevel);
            populateFortification(cbFortCommandCenter);

            isBusy = false;
        }

        private void populateFortification(ComboBoxEx fortCombo)
        {
            fortCombo.SelectedIndex = 0;
            FortificationData dat = (FortificationData)fortCombo.Tag;
            if (Stats[dat.LevelEntries[0]] == 0 && fortCombo.Name != "cbFortBarrierLevel")
            {
                fortCombo.Enabled = false;
                dat.Unlocked.Checked = false;
            }
            else
            {
                fortCombo.Enabled = true;
                dat.Unlocked.Checked = true;
                for (int x = 1; x < dat.LevelEntries.Length; x++)
                    if (Stats[dat.LevelEntries[x]] != 0)
                        fortCombo.SelectedIndex++;
            }
        }

        private void cbFortLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            FortificationData dat = (FortificationData)((ComboBoxEx)sender).Tag;
            for (int x = 0; x < dat.LevelEntries.Length; x++)
                Stats[dat.LevelEntries[x]] = x <= dat.Levels.SelectedIndex ? 1 : 0;
        }

        private void registerIntPanel(Panel panel)
        {
            for (int x = 0; x < panel.Controls.Count; x++)
                if (panel.Controls[x] is IntegerInput)
                    ((IntegerInput)panel.Controls[x]).ValueChanged += new EventHandler(PlayerData_ValueChanged);
        }

        private void PlayerData_ValueChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            IntegerInput input = (IntegerInput)sender;
            if (input.Tag == null)
                return;
            Stats[(EntryID)input.Tag] = input.Value;

            if (input.Name == "intKills" || input.Name == "intDeaths")
                lblKd.Text = ((decimal)intKills.Value / (intDeaths.Value == 0 ? 1 : intDeaths.Value)).ToString("0.0");
        }

        private bool isBusy = true;
        private ComboBoxEx[] GameModeCombos;
        private static Dictionary<EntryID, string> RibbonDescriptions;
        internal static List<MedalData> Medals;
        private static bool _cctorCalled;

        internal class MedalData
        {
            internal MedalData(EntryID baseId, string[] medalTitles)
            {
                Titles = medalTitles;
                string medalString = baseId.ToString();
                Name = (Titles.Length == 1 ? medalString.Substring(6) : medalString.Substring(6, medalString.LastIndexOf('_') - 6)).Replace('_', ' ');
                EntryIDs = new EntryID[Titles.Length];
                for (int x = 0; x < EntryIDs.Length; x++)
                    EntryIDs[x] = baseId + x;
            }

            internal string Name;
            internal string[] Titles;
            internal EntryID[] EntryIDs;
        }

        internal static void InitializeVars()
        {
            if (_cctorCalled)
            {
                return;
            }

            _cctorCalled = true;

            Weapons = new Dictionary<string, string>();
            Weapons.Add("Boltok_Pistol", "Boltok Pistol");
            Weapons.Add("Boomshot", "Boomshot");
            Weapons.Add("Cleaver", "Cleaver");
            Weapons.Add("Digger_Launcher", "Digger Launcher");
            Weapons.Add("Frag_Grenade", "Frag Grenade");
            Weapons.Add("Gnasher_Shotgun", "Gnasher Shotgun");
            Weapons.Add("Gorgon_Pistol", "Gorgon Pistol");
            Weapons.Add("Hammer_of_Dawn", "Hammer of Dawn");
            Weapons.Add("Hammerburst", "Hammerburst");
            Weapons.Add("Incendiary_Grenade", "Incendiary Grenade");
            Weapons.Add("Ink_Grenade", "Ink Grenade");
            Weapons.Add("Lancer", "Lancer");
            Weapons.Add("Longshot", "Longshot");
            Weapons.Add("Mortar", "Mortar");
            Weapons.Add("Mulcher", "Mulcher");
            Weapons.Add("OneShot", "OneShot");
            Weapons.Add("Retro_Lancer", "Retro Lancer");
            Weapons.Add("SawedOff_Shotgun", "Sawed-off Shotgun");
            Weapons.Add("Scorcher", "Scorcher");
            Weapons.Add("Silverback", "Silverback");
            Weapons.Add("Snub_Pistol", "Snub Pistol");
            Weapons.Add("Torque_Bow", "Torque Bow");
            Weapons.Add("Turret", "Turret");
            Weapons.Add("Vulcan_Minigun", "Vulcan Minigun");

            RibbonDescriptions = new Dictionary<EntryID, string>();
            RibbonDescriptions.Add(EntryID.Ribbon_MVP, "Highest point total for the match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Never_Had_a_Chance, "Win every round in the match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Untouchable, "Never captured when playing as the leader.");
            RibbonDescriptions.Add(EntryID.Ribbon_Captive_ating, "Captured the enemy leader.");
            RibbonDescriptions.Add(EntryID.Ribbon_Medic, "Revived 5 teammates in a round..");
            RibbonDescriptions.Add(EntryID.Ribbon_Last_Man_Out, "Last man standing on your team.");
            RibbonDescriptions.Add(EntryID.Ribbon_Better_Man, "Won a sudden death showdown.");
            RibbonDescriptions.Add(EntryID.Ribbon_First_Blood, "Earned the first kill of the round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Clusterluck, "Killed multiple opponents with one grenade.");
            RibbonDescriptions.Add(EntryID.Ribbon_Not_So_Fast, "Killed an opponent who was executing a teammate.");
            RibbonDescriptions.Add(EntryID.Ribbon_Negotiation_Over, "Headshot an opponent with a meatshield.");
            RibbonDescriptions.Add(EntryID.Ribbon_Retribution, "Killed your nemesis.");
            RibbonDescriptions.Add(EntryID.Ribbon_Death_from_Beyond, "Killed an opponent after you have died.");
            RibbonDescriptions.Add(EntryID.Ribbon_Godlike, "Killed 25 opponents without dying.");
            RibbonDescriptions.Add(EntryID.Ribbon_Invincible, "Killed 20 opponents without dying.");
            RibbonDescriptions.Add(EntryID.Ribbon_Unstoppable, "Killed 15 opponents without dying.");
            RibbonDescriptions.Add(EntryID.Ribbon_Rampage, "Killed 10 opponents without dying.");
            RibbonDescriptions.Add(EntryID.Ribbon_Killing_Spree, "Killed 5 opponents without dying.");
            RibbonDescriptions.Add(EntryID.Ribbon_Personal_Assistant, "Assisted 10 kills in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Stop_Thief, "5 kills stolen by others in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_FIFO, "First to die in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Lead_by_Example, "Killed 5 opponents as the leader in the round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Want_Something_Done, "Captured the enemy leader when playing as the leader.");
            RibbonDescriptions.Add(EntryID.Ribbon_Unlucky_Bastard, "Only player on your team to die in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Team_Player, "Most assists in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Clear, "Most revives in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Buttoned_Up, "Spent the most time in cover.");
            RibbonDescriptions.Add(EntryID.Ribbon_Grenade_Hug, "Killed an opponent after being grenade tagged.");
            RibbonDescriptions.Add(EntryID.Ribbon_Top_of_the_Hill, "Killed 5 opponents from inside the ring.");
            RibbonDescriptions.Add(EntryID.Ribbon_Ring_Breaker, "Broke opposing ring 3 times in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Dead_Ringer, "Won a round of King Of The Hill by shutting out the opposing team.");
            RibbonDescriptions.Add(EntryID.Ribbon_Mortarfied, "Killed multiple opponents with a single Mortar shot.");
            RibbonDescriptions.Add(EntryID.Ribbon_Boombardier, "Killed multiple opponents with a single Boomshot blast.");
            RibbonDescriptions.Add(EntryID.Ribbon_Kaboom, "Killed multiple opponents with a single Sawed-Off Shotgun blast.");
            RibbonDescriptions.Add(EntryID.Ribbon_Roadblock, "Stopped a Retro charge with the Sawed-Off Shotgun.");
            RibbonDescriptions.Add(EntryID.Ribbon_Death_from_Below, "Killed an opponent with a grenade while downed.");
            RibbonDescriptions.Add(EntryID.Ribbon_Oscar_Mike, "Killed a Roadie Running opponent with a headshot.");
            RibbonDescriptions.Add(EntryID.Ribbon_The_Super, "Killed an evading opponent with a headshot.");
            RibbonDescriptions.Add(EntryID.Ribbon_Survivor, "Revived yourself 5 times in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Ring_King, "Captured a ring 3 times in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Coup_de_Grace, "Final kill with an execution.");
            RibbonDescriptions.Add(EntryID.Ribbon_Military_Intelligence, "5 opponents spotted ending in a kill.");
            RibbonDescriptions.Add(EntryID.Ribbon_Final_Word, "Final kill of the match.");
            RibbonDescriptions.Add(EntryID.Ribbon_The_Double, "2 quick kills in a row.");
            RibbonDescriptions.Add(EntryID.Ribbon_The_Triple, "3 quick kills in a row.");
            RibbonDescriptions.Add(EntryID.Ribbon_The_Quad, "4 quick kills in a row.");
            RibbonDescriptions.Add(EntryID.Ribbon_The_Quinn, "5 quick kills in a row.");
            RibbonDescriptions.Add(EntryID.Ribbon_Sapper_Star, "Killed an opponent with the opponent’s own planted frag grenade.");
            RibbonDescriptions.Add(EntryID.Ribbon_Hat_Trick, "Scored 3 headshots in a row without dying.");
            RibbonDescriptions.Add(EntryID.Ribbon_Ole, "Grenade tagged a Retro charging opponent.");
            RibbonDescriptions.Add(EntryID.Ribbon_First_to_Fight, "First kill in every round of a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_The_Cleaner, "Final kill in every round of a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Methodical, "5 executions in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Vigilant, "Won a match with no deaths and 10+ kills.");
            RibbonDescriptions.Add(EntryID.Ribbon_Solid, "More kills than deaths in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Smooth_Operator, "Highest K/D ratio in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Tough_Guy, "Fewest deaths in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Rough_Day, "Most deaths in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Executioner, "Most executions in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Nemesis, "Killed same opponent 5 times.");
            RibbonDescriptions.Add(EntryID.Ribbon_Trick_Shot, "1 Torque Bow headshot leading to a double kill.");
            RibbonDescriptions.Add(EntryID.Ribbon_Hail_Mary, "Boomshot kill from over 200 feet.");
            RibbonDescriptions.Add(EntryID.Ribbon_Swift_Vengeance, "Quickly killed your last killer.");
            RibbonDescriptions.Add(EntryID.Ribbon_Avenged, "Killed your Wingman’s assassin.");
            RibbonDescriptions.Add(EntryID.Ribbon_Clutch, "Killed 3 or more as last man standing to win the round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Rear_Guard, "Survived every round of Wingman.");
            RibbonDescriptions.Add(EntryID.Ribbon_Secret_Service, "Most leader rescues in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Eye_on_the_Prize, "Most points earned in the ring.");
            RibbonDescriptions.Add(EntryID.Ribbon_Evasive, "Least damage taken in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Contender, "Most melee hits in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Spray_and_Pray, "Most blindfire kills in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Headhunter, "Most headshot kills in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Carmines_Star, "Most headshot deaths in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Grenadier, "Most grenade kills in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Pistoleer, "Most pistol kills in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Quick_Clips, "Most perfect Active Reloads in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Well_Protected, "Most revived player in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Guys_Hello, "Most time down but not out in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Under_the_Radar, "Earned no other ribbons in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Lumberjack, "Chainsawed 3 opponents in a row.");
            RibbonDescriptions.Add(EntryID.Ribbon_Charge, "Retro charged 3 opponents in a row.");
            RibbonDescriptions.Add(EntryID.Ribbon_No_Smoking, "Killed an opponent with a smoke grenade.");
            RibbonDescriptions.Add(EntryID.Ribbon_Denied, "Ended an opponent’s kill streak.");
            RibbonDescriptions.Add(EntryID.Ribbon_Death_from_Above, "Killed multiple opponents with a single Hammer of Dawn blast.");
            RibbonDescriptions.Add(EntryID.Ribbon_Pop_Goes_the_Weasel, "Blew up 3 enemies at once (Ticker).");
            RibbonDescriptions.Add(EntryID.Ribbon_Indigestion, "Killed an enemy with a swallowed grenade (Wild Ticker).");
            RibbonDescriptions.Add(EntryID.Ribbon_Monkey_Dog, "Multiple enemies you stunned were killed (Wretch).");
            RibbonDescriptions.Add(EntryID.Ribbon_Meatshop, "Killed 4 enemies without dying (Butcher).");
            RibbonDescriptions.Add(EntryID.Ribbon_Team_Shaman, "Healed 4 teammates at once (Kantus).");
            RibbonDescriptions.Add(EntryID.Ribbon_Team_Savior, "Revived 3 teammates at once (Kantus).");
            RibbonDescriptions.Add(EntryID.Ribbon_Pillager, "Destroyed 5 fortifications in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Test_Driver, "Played as 5 different Locust in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Antihero, "Killed 5 different Heroes in a round.");
            RibbonDescriptions.Add(EntryID.Ribbon_Ready_for_the_Heavies, "Unlocked the final row of Locust.");
            RibbonDescriptions.Add(EntryID.Ribbon_Just_in_Time, "Completed the wave with only 1 second left.");
            RibbonDescriptions.Add(EntryID.Ribbon_Long_Hauler, "Completed all 50 waves in one session.");
            RibbonDescriptions.Add(EntryID.Ribbon_Point_Man, "Earned the most cash in the wave.");
            RibbonDescriptions.Add(EntryID.Ribbon_Combat_Engineer, "Worked on 5 fortifications in one deployment.");
            RibbonDescriptions.Add(EntryID.Ribbon_Founder, "Established a COG base.");
            RibbonDescriptions.Add(EntryID.Ribbon_Financier, "Gave $5,000 to teammates.");
            RibbonDescriptions.Add(EntryID.Ribbon_Reconnaissance, "In Ghost Cam, spotted 3 weapons before they were picked up.");
            RibbonDescriptions.Add(EntryID.Ribbon_Observer, "Survived the wave but with no kills.");
            RibbonDescriptions.Add(EntryID.Ribbon_Phat_Loot, "Completed a wave Challenge Objective.");
            RibbonDescriptions.Add(EntryID.Ribbon_Last_Hope, "Survived the wave as the last one alive.");
            RibbonDescriptions.Add(EntryID.Ribbon_Go_on_without_Me, "Completed the wave as the only one dead.");
            RibbonDescriptions.Add(EntryID.Ribbon_High_ROI, "Killed 5 enemies with a weapon you purchased.");
            RibbonDescriptions.Add(EntryID.Ribbon_I_Gotcha, "Revived all 4 teammates in one wave.");
            RibbonDescriptions.Add(EntryID.Ribbon_Like_a_Boss, "Survived a Boss Wave without going down or dying.");
            RibbonDescriptions.Add(EntryID.Ribbon_Rope_a_Dope, "Killed 3 enemies in a wave that were attacking a decoy.");
            RibbonDescriptions.Add(EntryID.Ribbon_Botanist, "Shot 5 Lambent Pods in a chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_Mech_Jockey, "Killed 10 enemies with a Silverback.");
            RibbonDescriptions.Add(EntryID.Ribbon_Flyswatter, "Killed 5 Shriekers.");
            RibbonDescriptions.Add(EntryID.Ribbon_Quick_Kicker, "Kicked 5 small enemies.");
            RibbonDescriptions.Add(EntryID.Ribbon_Pull, "Killed a ground bursting enemy in the air.");
            RibbonDescriptions.Add(EntryID.Ribbon_Dewormer, "Killed 3 Lambent Headsnakes.");
            RibbonDescriptions.Add(EntryID.Ribbon_Pruner, "Sever 5 Lambent mutant arms.");
            RibbonDescriptions.Add(EntryID.Ribbon_Ace, "Most kills in an Arcade chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_Hand_Holder, "Most revives in an Arcade chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_Wingman, "Most assists in an Arcade chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_Stockpiler, "Most ammo taken in an Arcade chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_Conservationist, "Least ammo taken in an Arcade chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_Number_1, "Earned the highest score in an Arcade chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_Priority_Target, "Highest score for a single kill in an Arcade chapter.");
            RibbonDescriptions.Add(EntryID.Ribbon_On_Your_Feet_Soldier, "Completed Arcade chapter without going down but not out.");
            RibbonDescriptions.Add(EntryID.Ribbon_Special_Delivery, "Killed an opponent with a bag & tag.");
            RibbonDescriptions.Add(EntryID.Ribbon_Sacrifice, "Broke the ring alone while down but not out.");
            RibbonDescriptions.Add(EntryID.Ribbon_No_Wait, "Killed an opponent while they reloaded.");
            RibbonDescriptions.Add(EntryID.Ribbon_Pacifist, "More revives than kills in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_So_Close, "Killed while recovering from down but not out.");
            RibbonDescriptions.Add(EntryID.Ribbon_Stay_Down, "More downs than kills in a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Never_Surrender, "Came from defeat to win a match.");
            RibbonDescriptions.Add(EntryID.Ribbon_Pig_Sticker, "Retro charge 4 Formers in one charge.");

            Medals = new List<MedalData>();
            addMedal(EntryID.Medal_Vengeful_0, "K.C.", "Best Served Cold", "Lambda Lambda Lambda", "My Name is Inigo Montoya");
            addMedal(EntryID.Medal_Captor_0, "Pickup Artist", "Body Snatcher", "Meat Shielder", "Dances with Death");
            addMedal(EntryID.Medal_War_Supporter_0, "Wall Flower", "Social Butterfly", "Partygoer", "Party Animal");
            addMedal(EntryID.Medal_MVP_0, "First Star", "MVP", "All Star", "Brutal Legend");
            addMedal(EntryID.Medal_Field_Service_0, "Gears Adept", "Gears Expert", "Gears Elite", "Gears God");
            addMedal(EntryID.Medal_Veteran_0, "Seasoned Gear", "Battle-Tested Gear", "Battle-Hardened Gear", "Veteran Gear");
            addMedal(EntryID.Medal_Match_Winner_0, "Maker of Chicken Dinner", "Sir Wins-a-Lot", "Opposite of Loser", "Victory is Mine!");
            addMedal(EntryID.Medal_Headshot_0, "Marksman", "Sharpshooter", "Headhunter", "Lobotomizer");
            addMedal(EntryID.Medal_Heavy_Weapons_0, "Heavy Weapons Specialist", "Heavy Weapons Expert", "Heavy Weapons Artisan", "Heavy Weapons Master");
            addMedal(EntryID.Medal_Explosives_0, "Explosives Specialist", "Explosives Expert", "Explosives Artisan", "Explosives Master");
            addMedal(EntryID.Medal_Finisher_0, "Finisher", "Assassin", "Grand Executioner", "Angel of Death");
            addMedal(EntryID.Medal_Skunker_0, "Streak Sweeper", "Steamroller", "Dominator", "P3P3 L3 P3W P3W");
            addMedal(EntryID.Medal_Leader_0, "Absconder", "Vigilant Warrior", "Quick like a Bunny", "Untouchable");
            addMedal(EntryID.Medal_Abductor_0, "Mate Checker", "Bounty Hunter", "Trophy Hunter", "From Stockholm with Love");
            addMedal(EntryID.Medal_Assistant_0, "Personal Assistant", "Plays Well with Others", "K/D Indifferent", "Team Player");
            addMedal(EntryID.Medal_Medic_0, "Medic", "Lifesaver", "Field Surgeon", "God Complex");
            addMedal(EntryID.Medal_Cover_0, "Pop and Stopper", "Got it Covered", "Undercover Agent", "Lee's B.F.F.");
            addMedal(EntryID.Medal_Active_Reloader_0, "Weaksauce", "Hotsauce", "Winsauce", "Awesomesauce");
            addMedal(EntryID.Medal_Lancer_0, "Lancer Specialist", "Lancer Expert", "Lancer Artisan", "Sir Lanceres-a-Lot ");
            addMedal(EntryID.Medal_Hammerburst_0, "Hammerburst Specialist", "Hammerburst Expert", "Hammerburst Artisan", "Hammerburst Master ");
            addMedal(EntryID.Medal_Retro_Lancer_0, "Retro Specialist", "Retro Expert", "Retro Artisan", "Retro Master");
            addMedal(EntryID.Medal_Gnasher_Shotgun_0, "Gnasher Specialist", "Gnasher Expert", "Gnasher Artisan", "Gnasher Master");
            addMedal(EntryID.Medal_SawedOff_Shotgun_0, "Sawed-off Specialist", "Sawed-off Expert", "Sawed-off Artisan", "Sawed-off Master");
            addMedal(EntryID.Medal_Pistols_0, "Pistol Specialist", "Pistol Expert", "Pistol Artisan", "Pistol Master");
            addMedal(EntryID.Medal_Spotter_0, "The Designator", "Eagle Eye", "Eye in the Sky", "Captain Obvious");
            addMedal(EntryID.Medal_Pyro_0, "Fire Starter", "Arsonist", "Pyromaniac", "Hellfire & Brimstone");
            addMedal(EntryID.Medal_Sapper_0, "Sapper", "Assault Pioneer", "EOD Technician", "EOD Master");
            addMedal(EntryID.Medal_Guardian_0, "Henchman", "Body Guard", "Private Security", "Guardian Angel");
            addMedal(EntryID.Medal_CTL_0, "CTL Specialist", "CTL Expert", "CTL Elite", "CTL Master");
            addMedal(EntryID.Medal_KOTH_0, "KOTH Specialist", "KOTH Expert", "KOTH Artisan", "Hail to the King");
            addMedal(EntryID.Medal_Warzone_0, "Warzone Specialist", "Warzone Expert", "Warzone Artisan", "Warzone Master");
            addMedal(EntryID.Medal_Execution_0, "Execution Specialist", "Execution Expert", "Execution Artisan", "Execution Master");
            addMedal(EntryID.Medal_TDM_0, "TDM Specialist", "TDM Expert", "TDM Artisan", "TDM Master");
            addMedal(EntryID.Medal_Wingman_0, "Wingman Specialist", "Wingman Expert", "Wingman Artisan", "Wingman Master");
            addMedal(EntryID.Medal_Allfathers_0, "Allfather", "Senior Allfather", "Executive Allfather", "Supreme Allfather");
            addMedal(EntryID.Medal_Master_at_Arms_0, "Man-at-Arms", "Master-at-Arms", "Master-at-Arms Elite", "God-at-Arms");
            addMedal(EntryID.Medal_Rifleman_0, "Rifle Specialist", "Rifle Expert", "Rifle Artisan", "Rifle Master");
            addMedal(EntryID.Medal_Hard_Target_0, "F.I.L.O.", "Hard Target", "Last of a Dying Breed", "I Am Legend");
            addMedal(EntryID.Medal_Shock_Trooper_0, "Eager Beaver", "Shock Trooper", "First to Fight", "Tip of the Spear");
            addMedal(EntryID.Medal_Old_Schooler_0, "Bootstrapper", "Romper Stomper", "Curbing Enthusiast", "Kickin' it Old School");
            addMedal(EntryID.Medal_Battle_Mistress_0, "Amazonian", "She-Devil", "Battle Mistress", "Warrior Goddess");
            addMedal(EntryID.Medal_Sovereign_0, "High Value Target", "Military Advisor", "Field Commander", "Supreme Commander");
            addMedal(EntryID.Medal_Special_Teams_0, "Resourceful", "Big Gun Runner", "Power Collector", "Lives off the Land");
            addMedal(EntryID.Medal_For_the_Horde_0, "Horde Specialist", "Horde Expert", "Horde Artisan", "Horde Master");
            addMedal(EntryID.Medal_Horder_0, "Horder", "Active Horder", "Compulsive Horder", "Pathological Horder");
            addMedal(EntryID.Medal_Architect_0, "Founder", "Flagstaffer", "Team Patron", "Epic Architect");
            addMedal(EntryID.Medal_Squad_Leader_0, "Team Leader", "Squad Leader", "Platoon Leader", "Leads by Example");
            addMedal(EntryID.Medal_Field_Engineer_0, "Tinkerer", "Base Builder", "Combat Engineer", "Fortification Master");
            addMedal(EntryID.Medal_Big_Money_0, "Mind on My Money", "Makin' it Rain", "Soldier of Fortune", "I'm Rich, *****!");
            addMedal(EntryID.Medal_Loot_Courtesan_0, "Common Looter", "Uncommon Looter", "Rare Looter", "Epic Looter");
            addMedal(EntryID.Medal_Im_a_Beast_0, "COG Tease", "COG Bite", "COG Blocker", "COG Killer");
            addMedal(EntryID.Medal_Beastly_0, "Myrrah's Minion", "For RAAM!", "For Skorge!", "For the Queen!");
            addMedal(EntryID.Medal_Motivator_0, "Chanter", "Witch Doctor", "Shaman", "Faith Healer");
            addMedal(EntryID.Medal_Dismantler_0, "Demolitions Expert", "Wrecking Ball", "Agent of Destruction", "Ripping and Tearing");
            addMedal(EntryID.Medal_Ruthless_0, "Savage", "Butcher", "Genocidal", "Evil Incarnate");
            addMedal(EntryID.Medal_Investor_0, "Investor", "Floor Trader", "Fund Manager", "Power Broker");
            addMedal(EntryID.Medal_High_Roller_0, "H.E.N.R.Y.", "Top Tier Beast", "Taster of Blood", "Like a Truck");
            addMedal(EntryID.Medal_King_of_COG_0, "On the Board", "High Scorer", "Screen Killer", "King of COG");
            addMedal(EntryID.Medal_Number_1_0, "First Among Equals", "Top Gear", "#1 With a Bullet", "Numero Uno");
            addMedal(EntryID.Medal_Warmonger_0, "Casual Competitor", "Friendly Rival", "Die-hard Challenger", "Crazy Adversary");
            addMedal(EntryID.Medal_Force_Multiplier_0, "All for One", "Maximizing Potential", "Perpetually Optimistic", "Force Multiplier");
            addMedal(EntryID.Medal_Survivalist_0, "Still Standing", "Standing Strong", "Never Down, Never Out", "I'm a Survivor");
            addMedal(EntryID.Medal_Aficionado_0, "Arcade Mouse", "Arcade Rat", "Arcade Junkie", "Misspent Youth");
            addMedal(EntryID.Medal_Doorman_0, "Doorman", "Button Masher", "Gate Keeper", "I've Got the Codes");
            addMedal(EntryID.Medal_Tour_of_Duty_0, "Proud to Serve", "Served with Honor", "Hardcore Soldier", "Major Malfunction");

            addMedal(EntryID.Medal_Beta_Tester, "Beta Tester");
            addMedal(EntryID.Medal_Epic, "I am EPIC!");
            addMedal(EntryID.Medal_Gears_of_War_3_Developer, "I Made This!");
            addMedal(EntryID.Medal_Old_Guard, "Member of the Old Guard");
            addMedal(EntryID.Medal_Seriously, "Extremely Serious");
            addMedal(EntryID.Medal_Embry_Star, "War Hero");
            addMedal(EntryID.Medal_It_Has_Begun, "Just Getting Started");
        }

        private static void addMedal(EntryID baseId, string medalTitle1, string medalTitle2, string medalTitle3, string medalTitle4)
        {
            Medals.Add(new MedalData(baseId, new string[] { medalTitle1, medalTitle2, medalTitle3, medalTitle4 }));
        }

        private static void addMedal(EntryID baseId, string medalTitle)
        {
            Medals.Add(new MedalData(baseId, new string[] { medalTitle }));
        }

        private PlayerStorage Stats;
        public override bool Entry()
        {
            if (!OpenStfsFile("PlayerStorage.dat"))
                return false;

            Horizon.ProfileData profileData = ProfileManager.fetchRealProfile(Package.Header.Metadata.Creator);
            if (profileData == null)
            {
                if (UI.messageBox("This tool requires data from your saved game's corresponding Gamer Profile.\n\n"
                    + "If you have this profile on a memory device, insert it into your computer, let it load, then press OK.\n\n"
                    + "If you have this profile on your computer, hit OK to open it.", "Profile Not Found",
                    MessageBoxIcon.Exclamation, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                openProfile:
                    profileData = ProfileManager.fetchRealProfile(Package.Header.Metadata.Creator);
                    if (profileData == null)
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.FileName = Package.Header.Metadata.Creator.ToString("X16");
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            XContent.XContentPackage tempPackage = new XContent.XContentPackage();
                            if (!tempPackage.LoadPackage(ofd.FileName))
                                goto openProfile;
                            if (tempPackage.Header.Metadata.Creator == Package.Header.Metadata.Creator)
                            {
                                try
                                {
                                    ProfileManager.addProfileToCache(tempPackage, new XProfile.XProfileAccount(tempPackage.StfsContentPackage.ExtractFileToArray("Account")));
                                    profileData = ProfileManager.fetchRealProfile(tempPackage.Header.Metadata.Creator);
                                }
                                catch
                                {
                                    UI.errorBox("Corrupted gamer profile selected!");
                                }
                            }
                            else
                                UI.errorBox("You have selected the incorrect gamer profile!");
                            tempPackage.CloseIO(true);
                        }
                    }
                }
            }

            if (profileData == null)
                return false;

            Stats = new PlayerStorage(IO, profileData.XUID, true);

            isBusy = true;

            LoadAll();

            isBusy = false;

            return true;
        }

        private void LoadEnemyKills()
        {
            isBusy = true;

            listKills.Rows.Clear();

            foreach (Entry entry in Stats)
            {
                if (entry.ID < EntryID.Bloodmount_Kills_Campaign || entry.ID > EntryID.Palace_Guard_Kills_Horde)
                    continue;
                string[] entryTag = entry.ID.ToString().Split('_');
                if (entryTag.Length < 3)
                    continue;
                string gameMode = entryTag[entryTag.Length - 1];
                if (entryTag[entryTag.Length - 2] != "Kills" || ((gameMode != "Campaign" && gameMode != "Horde")))
                    continue;

                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < entryTag.Length - 2; x++)
                {
                    sb.Append(entryTag[x]);
                    sb.Append(' ');
                }
                sb.Append("Kills");
                string charName = sb.ToString();

                DataGridViewRow charRow = null;

                foreach (DataGridViewRow row in listKills.Rows)
                {
                    if ((string)row.Cells[0].Value == charName)
                    {
                        charRow = row;
                        break;
                    }
                }
                if (charRow == null)
                {
                    charRow = new DataGridViewRow();
                    charRow.CreateCells(listKills);
                    charRow.Cells[0].Value = charName;
                    listKills.Rows.Add(charRow);
                }
                if (gameMode == "Campaign")
                {
                    charRow.Cells[1].Tag = entry.ID;
                    charRow.Cells[1].Value = entry.nData;
                }
                else if (gameMode == "Horde")
                {
                    charRow.Cells[2].Tag = entry.ID;
                    charRow.Cells[2].Value = entry.nData;
                }
            }
            SortGrid(listKills);

            isBusy = false;
        }

        private void SortGrid(DataGridView grid)
        {
            grid.Sort(grid.Columns[0], ListSortDirection.Ascending);
            grid.FirstDisplayedScrollingRowIndex = 0;
            while (grid.SelectedCells.Count != 0)
                grid.SelectedCells[0].Selected = false;
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!isBusy && e.RowIndex != -1 && e.ColumnIndex != -1 && e.ColumnIndex != 0)
            {
                DataGridViewCell cell = ((DataGridViewX)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value is int)
                    Stats[(EntryID)cell.Tag] = (int)cell.Value;
                else if (cell.Value is string)
                {
                    int tryInt;
                    if (int.TryParse((string)cell.Value, out tryInt))
                        Stats[(EntryID)cell.Tag] = tryInt;
                    else
                        Stats[(EntryID)cell.Tag] = (string)cell.Value == "Y" ? 1 : 0;
                }
                else if (cell.Value is bool)
                    Stats[(EntryID)cell.Tag] = (bool)cell.Value ? 1 : 0;
            }
        }

        public override void Save()
        {
            Stats[EntryID.SyncID] += 10;

            Stats[EntryID.Rank] = cbRank.SelectedIndex + 1;

            /*int[] kills = new int[3];
            for (EntryID x = EntryID.Lancer_Rifle_Kills_Campaign; x <= EntryID.Ripper_Kills_Horde; x++)
            {
                if (x > EntryID.Snub_Pistol_Kills_Horde && x < EntryID.Boomshot_Kills_Campaign)
                    continue;
                kills[0] += Stats[x++];
                kills[1] += Stats[x++];
                kills[2] += Stats[x];
            }
            for (EntryID x = EntryID.Melee_Kills_Campaign; x <= EntryID.Holding_Shield_Kills_Beast; x++)
            {
                kills[0] += Stats[x++];
                kills[1] += Stats[x++];
                kills[2] += Stats[x++];
            }
            Stats[EntryID.Kills_Campaign] = kills[0];
            Stats[EntryID.Kills_Versus] = kills[1];
            Stats[EntryID.Kills_Horde] = kills[2];

            int[] perfectReloads = new int[3];
            for (EntryID x = EntryID.Hammerburst_Perfect_Active_Reloads_Campaign; x <= EntryID.Ripper_Perfect_Active_Reloads_Horde; x++)
            {
                perfectReloads[0] += Stats[x++];
                perfectReloads[1] += Stats[x++];
                perfectReloads[2] += Stats[x];
            }
            Stats[EntryID.Perfect_Active_Reloads_Campaign] = perfectReloads[0];
            Stats[EntryID.Perfect_Active_Reloads_Versus] = perfectReloads[1];
            Stats[EntryID.Perfect_Active_Reloads_Horde] = perfectReloads[2];

            int[] deaths = new int[3];
            for (EntryID x = EntryID.Lancer_Deaths_Holding_Campaign; x <= EntryID.SawedOff_Shotugn_Downs_Horde; x++)
            {
                deaths[0] += Stats[x++];
                deaths[1] += Stats[x++];
                deaths[2] += Stats[x];
            }
            Stats[EntryID.Deaths_Campaign] = deaths[0];
            Stats[EntryID.Deaths_Versus] = deaths[1];
            Stats[EntryID.Deaths_Horde] = deaths[2];

            int beastKills = 0;
            for (EntryID x = EntryID.Stranded_Kills_Beast; x <= EntryID.Chairman_Prescott_Kills_Beast; x++)
                beastKills += Stats[x];
            Stats[EntryID.Kills_Beast] = beastKills;

            int[] executions = new int[4];
            for (EntryID x = EntryID.Curb_Stomp_Executions_Campaign; x <= EntryID.Hammer_of_Dawn_Executions_Beast; x++)
            {
                executions[0] += Stats[x++];
                executions[1] += Stats[x++];
                executions[2] += Stats[x++];
                executions[3] += Stats[x++];
            }
            Stats[EntryID.Executions_Campaign] = executions[0];
            Stats[EntryID.Executions_Versus] = executions[1];
            Stats[EntryID.Executions_Horde] = executions[2];
            Stats[EntryID.Executions_Beast] = executions[3];

            Stats[EntryID.Grenade_Kills_Campaign] =
                Stats[EntryID.Frag_Grenade_Kills_Campaign]
                + Stats[EntryID.Incendiary_Grenade_Kills_Campaign]
                + Stats[EntryID.Ink_Grenade_Kills_Campaign];

            Stats[EntryID.Grenade_Kills_Versus] =
                Stats[EntryID.Frag_Grenade_Kills_Versus]
                + Stats[EntryID.Incendiary_Grenade_Kills_Versus]
                + Stats[EntryID.Ink_Grenade_Kills_Versus];

            Stats[EntryID.Grenade_Kills_Horde] =
                Stats[EntryID.Frag_Grenade_Kills_Horde]
                + Stats[EntryID.Incendiary_Grenade_Kills_Horde]
                + Stats[EntryID.Ink_Grenade_Kills_Horde];

            foreach (Entry entry in Stats)
                if ((int)entry.ID < 0x0608 && entry.nData > 999999999)
                    entry.nData = 999999999;*/

            Package.StfsContentPackage.InjectFileFromArray("PlayerStorage.dat", Stats.ToArray(true));
        }

        private void LoadMainStats()
        {
            populateIntPanel(panelMainStats);
            cbRank.SelectedIndex = Stats[EntryID.Rank] - 1;
        }

        private void populateIntPanel(Panel panel)
        {
            isBusy = true;
            for (int x = 0; x < panel.Controls.Count; x++)
                if (panel.Controls[x] is IntegerInput)
                    ((IntegerInput)panel.Controls[x]).Value = Stats[(EntryID)panel.Controls[x].Tag];
            isBusy = false;
        }

        private void LoadWeaponSkins()
        {
            isBusy = true;

            ButtonX[] skinButtons = new ButtonX[]
            {
                cmdSkinChrome,
                cmdSkinCrimsonOmen,
                cmdSkinFlaming,
                cmdSkinGold,
                cmdSkinOnyx,
                cmdSkinTeamInsignia
            };
            foreach (ButtonX skinButton in skinButtons)
            {
                for (int x = 0; x < skinButton.SubItems.Count; x++)
                {
                    CheckBoxItem item = (CheckBoxItem)skinButton.SubItems[x];
                    item.Checked = Stats[(EntryID)item.Tag] != 0;
                }
            }

            isBusy = false;
        }

        private void LoadAll()
        {
            isBusy = true;

            LoadMainStats();

            cbModeTotals.SelectedIndex = 0;
            LoadTotals();

            LoadEnemyKills();

            populateIntPanel(panelCampaign);

            LoadVersus();

            populateIntPanel(panelHorde);

            LoadFortifications();

            LoadBeastMode();

            LoadMiscExecutions();

            LoadWeapons();

            LoadMutators();

            LoadExecutions();

            LoadCharacters();

            LoadWeaponSkins();

            LoadRibbons();

            LoadMedals();

            isBusy = false;
        }

        private void updateControlLink(ComboBoxEx combo, IntegerInput input, EntryID baseId)
        {
            baseId = baseId + combo.SelectedIndex;
            input.Value = Stats[baseId];
            input.Tag = baseId;
        }

        private void LoadTotals()
        {
            isBusy = true;

            lblTotals.Text = (string)cbModeTotals.SelectedItem + " Mode Totals";

            updateControlLink(cbModeTotals, intRevives, EntryID.Revives_Campaign);
            updateControlLink(cbModeTotals, intAssists, EntryID.Assists_Campaign);
            updateControlLink(cbModeTotals, intDowns, EntryID.Downs_Campaign);
            updateControlLink(cbModeTotals, intKillsHoldingCaptive, EntryID.Headshots_Campaign);
            updateControlLink(cbModeTotals, intHeadshots, EntryID.Headshots_Campaign);
            updateControlLink(cbModeTotals, intPlayersSpotted, EntryID.Players_Spotted_Campaign);
            updateControlLink(cbModeTotals, intKills, EntryID.Kills_Campaign);
            updateControlLink(cbModeTotals, intDeaths, EntryID.Deaths_Campaign);
            if (cbModeTotals.SelectedIndex == 3)
            {
                intKnockdowns.Value = 0;
                intKnockdowns.Enabled = false;
            }
            else
            {
                intKnockdowns.Enabled = true;
                updateControlLink(cbModeTotals, intKnockdowns, EntryID.Holding_Captive_Kills_Campaign);
            }

            lblKd.Text = ((decimal)intKills.Value / (intDeaths.Value == 0 ? 1 : intDeaths.Value)).ToString("0.0");

            isBusy = false;
        }

        private void LoadRibbons()
        {
            isBusy = true;

            listRibbons.Rows.Clear();

            bool foundNever = false;

            string[] currentEntry;
            for (int x = 0; x < Stats.Count; x++)
            {
                if (Stats[x].ID >= EntryID.Ribbon_MVP && Stats[x].ID <= EntryID.Ribbon_Never_Surrender)
                {
                    currentEntry = Stats[x].ID.ToString().Split('_');
                    if (currentEntry[0] == "Ribbon")
                    {
                        if (Stats[x].ID == EntryID.Ribbon_Never_Had_a_Chance)
                            foundNever = true;

                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i < currentEntry.Length; i++)
                        {
                            sb.Append(currentEntry[i]);
                            sb.Append(' ');
                        }
                        
                        listRibbons.Rows.Add(createRibbonRow(Stats[x].ID, sb.ToString(), Stats[x].nData));
                    }
                }
            }

            if (!foundNever)
            {
                listRibbons.Rows.Add(createRibbonRow(EntryID.Ribbon_Never_Had_a_Chance,
                    "Never Had a Chance", Stats[EntryID.Ribbon_Never_Had_a_Chance]));
            }

            SortGrid(listRibbons);

            isBusy = false;
        }

        private DataGridViewRow createRibbonRow(EntryID entryId, string ribbonName, int nData)
        {
            StringBuilder sb = new StringBuilder("<b>");
            sb.Append(ribbonName);
            sb.Append("</b><br></br>");
            if (RibbonDescriptions.ContainsKey(entryId))
                sb.Append(RibbonDescriptions[entryId]);
            DataGridViewRow row = new DataGridViewRow();
            row.Height = 35;
            row.CreateCells(listRibbons);
            row.Cells[0].Value = sb.ToString();
            row.Cells[1].Value = nData;
            row.Cells[1].Tag = entryId;
            return row;
        }

        private void LoadCurrentMedal()
        {
            isBusy = true;

            MedalData medal = (MedalData)((ComboBoxItem)cbMedal.SelectedItem).Tag;
            lblMedalBronze.Text = medal.Titles[0];
            cmdMedalBronze.Tag = medal.EntryIDs[0];
            if (medal.EntryIDs.Length == 1)
            {
                intMedal.Value = 0;
                intMedal.Enabled = false;
                cmdMedalBronze.Checked = Stats[medal.EntryIDs[0]] != 0;
                lblMedalSilver.Text = "N/A";
                lblMedalGold.Text = "N/A";
                lblMedalOnyx.Text = "N/A";
                intMedal.Enabled = false;
                cmdMedalBronze.Text = "Unlocked";
                cmdMedalSilver.Enabled = false;
                cmdMedalGold.Enabled = false;
                cmdMedalOnyx.Enabled = false;
            }
            else
            {
                cmdMedalSilver.Tag = medal.EntryIDs[1];
                cmdMedalGold.Tag = medal.EntryIDs[2];
                cmdMedalOnyx.Tag = medal.EntryIDs[3];
                lblMedalSilver.Text = medal.Titles[1];
                lblMedalGold.Text = medal.Titles[2];
                lblMedalOnyx.Text = medal.Titles[3];
                cmdMedalBronze.Text = "Bronze";
                cmdMedalSilver.Enabled = true;
                cmdMedalGold.Enabled = true;
                cmdMedalOnyx.Enabled = true;

                int maxValue = 1;

                int tempInt = Stats[medal.EntryIDs[0]];
                if (tempInt == 0)
                    cmdMedalBronze.Checked = false;
                else
                {
                    cmdMedalBronze.Checked = true;
                    maxValue = tempInt;
                }

                tempInt = Stats[medal.EntryIDs[1]];
                if (tempInt == 0)
                    cmdMedalSilver.Checked = false;
                else
                {
                    cmdMedalSilver.Checked = true;
                    maxValue = tempInt;
                }

                tempInt = Stats[medal.EntryIDs[2]];
                if (tempInt == 0)
                    cmdMedalGold.Checked = false;
                else
                {
                    cmdMedalGold.Checked = true;
                    maxValue = tempInt;
                }

                tempInt = Stats[medal.EntryIDs[3]];
                if (tempInt == 0)
                {
                    intMedal.Enabled = false;
                    cmdMedalOnyx.Checked = false;
                }
                else
                {
                    intMedal.Enabled = true;
                    cmdMedalOnyx.Checked = true;
                    maxValue = tempInt;
                }

                intMedal.Tag = medal.EntryIDs[3];
                intMedal.Value = Stats[medal.EntryIDs[3]];
            }

            isBusy = false;
        }

        private void LoadMedals()
        {
            isBusy = true;

            cbMedal.Items.Clear();
            foreach (MedalData medal in Medals)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = medal.Name;
                item.Tag = medal;
                cbMedal.Items.Add(item);
            }
            cbMedal.Sorted = true;

            cbMedal.SelectedIndex = 0;

            LoadCurrentMedal();

            isBusy = false;
        }

        private void cbModeTotals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            LoadTotals();
        }

        private void cmdPackage_Click(object sender, EventArgs e)
        {
#if INT2
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            Stats = new PlayerStorage(new EndianIO(ofd.FileName, EndianType.BigEndian, true), 0, false);

            for (int x = 0; x < Stats.Count; x++)
            {
                /*if (Stats[x].ID == EntryID.Rank)
                    Stats[x].nData = 100;
                else if (Stats[x].ID == EntryID.Experience)
                    Stats[x].nData = 1234567899;
                if ((int)Stats[x].ID >= 2 && (int)Stats[x].ID <= 0x0193)
                    Stats[x].nData = 1;
                else if ((int)Stats[x].ID >= 404 && (int)Stats[x].ID <= 503)
                    Stats[x].nData = 1;
                else if ((int)Stats[x].ID > 1471 && (int)Stats[x].ID <= 1543)
                    Stats[x].nData = 0;
                if (Stats[x].ID == EntryID.Horde_Barrier_Investment
                    || Stats[x].ID == EntryID.Horde_Decoy_Investment
                    || Stats[x].ID == EntryID.Horde_Sentry_Investment
                    || Stats[x].ID == EntryID.Horde_Silverback_Investment
                    || Stats[x].ID == EntryID.Horde_Turret_Investment)
                    Stats[x].nData = 100863999;*/

                if (Stats[x].ID == EntryID.SyncID)
                    Stats[x].nData++;
                else if ((int)Stats[x].ID >= 2 && (int)Stats[x].ID <= 0x0193)
                    Stats[x].nData = 2;

                /*// Ribbons and Medals
                if ((int)Stats[x].ID >= 2 && (int)Stats[x].ID <= 0x0193)
                    Stats[x].nData = 0;

                // Skins, Collectables, and Challenges
                else if ((int)Stats[x].ID >= 404 && (int)Stats[x].ID <= 503)
                {
                    //if (Stats[x].ID == EntryID.Skin_IDK)
                    //    Stats[x].nData = 1;
                    //else
                    //    Stats[x].nData = 0;
                }

                // Weapon stats
                else if ((int)Stats[x].ID >= 531 && (int)Stats[x].ID <= 1375)
                {
                    Stats[x].nData = 0;
                }

                // Horde stats
                else if ((int)Stats[x].ID >= 1455 && (int)Stats[x].ID <= 1471)
                {
                    Stats[x].nData = 0;//(int)Stats[x].ID + 170000;
                }

                // Footer unlocks
                else if ((int)Stats[x].ID > 1471 && (int)Stats[x].ID <= 1543)
                {
                    if (Stats[x].ID == EntryID.Horde_Barrier_Level_1
                        || Stats[x].ID == EntryID.Horde_Decoy_Level_1
                        || Stats[x].ID == EntryID.Horde_Sentry_Level_1
                        || Stats[x].ID == EntryID.Horde_Turret_Level_1
                        || Stats[x].ID == EntryID.Horde_Silverback_Level_1
                        || Stats[x].ID == EntryID.Horde_Barrier_Level_2
                        || Stats[x].ID == EntryID.Horde_Decoy_Level_2
                        || Stats[x].ID == EntryID.Horde_Sentry_Level_2
                        || Stats[x].ID == EntryID.Horde_Turret_Level_2
                        || Stats[x].ID == EntryID.Horde_Silverback_Level_2)
                        Stats[x].nData = 1;
                    else if (Stats[x].ID == EntryID.Horde_IDK)
                        Stats[x].nData = 1;
                    else
                        Stats[x].nData = 1;
                }*/
            }

            File.WriteAllBytes(ofd.FileName + ".cmp", Stats.ToArray(true));
#endif
        }

        private void cmdDecompress_Click(object sender, EventArgs e)
        {
#if INT2
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            EndianIO IO = new EndianIO(ofd.FileName, EndianType.BigEndian, true);
            IO.Stream.Position = 0x18;
            byte[] data = IO.In.ReadBytes(IO.Stream.Length - 0x18);
            IO.Close();
            MemoryStream decompressionStream = new MemoryStream();
            LZO.LZO1X.Decompress(data, decompressionStream);
            File.WriteAllBytes(ofd.FileName + ".dec", decompressionStream.ToArray());
#endif
        }

        private void cmdLoadRaw_Click(object sender, EventArgs e)
        {
#if INT2
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            Stats = new PlayerStorage(new EndianIO(ofd.FileName, EndianType.BigEndian, true), 0, false);

            enablePanels(true);

            LoadAll();
#endif
        }

        private void cmdDumpStats_Click(object sender, EventArgs e)
        {
#if INT2
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            PlayerStorage pd = new PlayerStorage(new EndianIO(ofd.FileName, EndianType.BigEndian, true), 0, false);
            pd.dumpEntriesToFile(ofd.FileName + ".dump.txt");
#endif
        }

        private void LoadVersus()
        {
            isBusy = true;

            populateIntPanel(panelVersus);

            cbGameType.SelectedIndex = 0;
            LoadVersusGameTypes();

            isBusy = false;
        }

        private void LoadVersusGameTypes()
        {
            isBusy = true;

            listVersus.Rows.Clear();

            string typePrefix = (string)((ComboBoxItem)cbGameType.SelectedItem).Tag;
            for (int x = 0; x < Stats.Count; x++)
            {
                string idStr = Stats[x].ID.ToString();
                if (idStr.Length <= typePrefix.Length || idStr.Substring(0, typePrefix.Length) != typePrefix)
                    continue;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(listVersus);
                row.Cells[0].Value = idStr.Substring(typePrefix.Length).Replace('_', ' ');
                row.Cells[1].Tag = Stats[x].ID;
                row.Cells[1].Value = Stats[x].nData;
                listVersus.Rows.Add(row);
            }

            SortGrid(listVersus);

            isBusy = false;
        }

        private void cbGameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            LoadVersusGameTypes();
        }

        private void cmdMaxHorde_Click(object sender, EventArgs e)
        {
            foreach (Control x in panelHorde.Controls)
            {
                if (x is IntegerInput)
                {
                    IntegerInput i = (IntegerInput)x;
                    i.Value = i.MaxValue;
                }
            }
            foreach (Control x in panelFortifications.Controls)
            {
                if (x is ButtonX)
                    ((ButtonX)x).Checked = true;
                else if (x is ComboBoxEx)
                {
                    ComboBoxEx i = (ComboBoxEx)x;
                    i.SelectedIndex = i.Items.Count - 1;
                    i.Enabled = true;
                }
                else if (x is IntegerInput)
                {
                    IntegerInput i = (IntegerInput)x;
                    i.Value = i.MaxValue;
                    i.Enabled = true;
                }
            }
        }

        private void LoadMiscExecutions()
        {
            isBusy = true;

            lblMiscExecutions.Text = (string)cbModeExecutions.SelectedItem + " Executions";

            updateControlLink(cbModeExecutions, intCurbStomps, EntryID.Curb_Stomp_Executions_Campaign);
            updateControlLink(cbModeExecutions, intFacePunches, EntryID.Face_Punch_Executions_Campaign);
            updateControlLink(cbModeExecutions, intArmRips, EntryID.Arm_Rip_Executions_Campaign);
            updateControlLink(cbModeExecutions, intShieldBashes, EntryID.Shield_Bash_Executions_Campaign);

            isBusy = false;
        }

        private void LoadMutators()
        {
            PopulateGrid(listMutators, EntryID.Mutator_Instagib_Melee, EntryID.Mutator_Laugh_Track, true);
        }

        private void LoadExecutions()
        {
            PopulateGrid(listExecutions, EntryID.Execution_Lancer_Rifle, EntryID.Execution_Cleaver, true);
        }

        private void LoadCharacters()
        {
            listCharacters.Rows.Clear();

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(listCharacters);
            row.Cells[0].Value = "Cole Train";
            row.Cells[1].Value = Stats[EntryID.Character_Cole_Train] == 0 ? "N" : "Y";
            row.Cells[1].Tag = EntryID.Character_Cole_Train;
            listCharacters.Rows.Add(row);

            PopulateGrid(listCharacters, EntryID.Character_Locust_Miner, EntryID.Character_Aaron_Griffin, false);
        }

        private void PopulateGrid(DataGridView grid, EntryID min, EntryID max, bool clear)
        {
            isBusy = true;

            if (clear)
                grid.Rows.Clear();

            for (int x = 0; x < Stats.Count; x++)
            {
                if (Stats[x].ID >= min && Stats[x].ID <= max)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(grid);
                    string idStr = Stats[x].ID.ToString();
                    row.Cells[0].Value = idStr.Substring(idStr.IndexOf('_') + 1).Replace('_', ' ');
                    row.Cells[1].Value = Stats[x].nData == 0 ? "N" : "Y";
                    row.Cells[1].Tag = Stats[x].ID;
                    grid.Rows.Add(row);
                }
            }

            SortGrid(grid);

            isBusy = false;
        }

        private void cbModeExecutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            LoadMiscExecutions();
        }

        private void cmdMaxRibbons_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listRibbons.Rows.Count; x++)
                listRibbons.Rows[x].Cells[1].Value = 999999999;
        }

        private void cmdMaxCampaign_Click(object sender, EventArgs e)
        {
            foreach (Control x in panelCampaign.Controls)
            {
                if (x is IntegerInput)
                {
                    IntegerInput i = (IntegerInput)x;
                    i.Value = i.MaxValue;
                }
            }
        }

        private void cmdMaxVersus_Click(object sender, EventArgs e)
        {
            foreach (Control x in panelVersus.Controls)
            {
                if (x is IntegerInput)
                {
                    IntegerInput i = (IntegerInput)x;
                    i.Value = i.MaxValue;
                }
            }
            for (int x = 0; x < Stats.Count; x++)
                if (Stats[x].ID >= EntryID.Versus_Matches_Played && Stats[x].ID <= EntryID.Versus_Wingman_Matches_Lost)
                    Stats[x].nData = 999999999;
            for (int x = 0; x < listVersus.Rows.Count; x++)
                listVersus.Rows[x].Cells[1].Value = 999999999;
        }

        private void Medal_CheckedChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            intMedal.Enabled = cmdMedalOnyx.Checked;

            ButtonX medalButton = (ButtonX)sender;
            if (medalButton.Checked)
                Stats[(EntryID)medalButton.Tag] = medalButton.Text[0] == 'O' ? intMedal.Value : 1;
            else
                Stats[(EntryID)medalButton.Tag] = 0;
        }

        private void cmdMaxMedals_Click(object sender, EventArgs e)
        {
            isBusy = true;

            intMedal.Value = intMedal.MaxValue;

            cmdMedalBronze.Checked = true;
            cmdMedalSilver.Checked = true;
            cmdMedalGold.Checked = true;
            cmdMedalOnyx.Checked = true;

            foreach (MedalData medal in Medals)
                for (int x = 0; x < medal.EntryIDs.Length; x++)
                    Stats[medal.EntryIDs[x]] = intMedal.MaxValue;
            
            isBusy = false;
        }

        private void cbMedal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            LoadCurrentMedal();
        }

        private void cmdMaxWeapons_Click(object sender, EventArgs e)
        {
            foreach (Control x in panelExecutions.Controls)
            {
                if (x is IntegerInput)
                {
                    IntegerInput i = (IntegerInput)x;
                    i.Value = i.MaxValue;
                }
            }
            foreach (Node weaponNode in listWeapons.Nodes)
            {
                foreach (Node typeNode in weaponNode.Nodes)
                {
                    IntegerInput i = (IntegerInput)typeNode.Cells[1].HostedControl;
                    i.Value = i.MaxValue;
                }
            }
        }

        private void cbModeWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            LoadWeapons();
        }

        private void intMedal_ValueChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            IntegerInput i = (IntegerInput)sender;
            Stats[(EntryID)i.Tag] = i.Value;
        }

        private void cmdMaxChallenges_Click(object sender, EventArgs e)
        {
            foreach (Control x in panelSkins.Controls)
            {
                if (x is ButtonX)
                {
                    ButtonX b = (ButtonX)x;
                    foreach (BaseItem i in b.SubItems)
                        ((CheckBoxItem)i).Checked = true;
                }
            }

            for (int x = 0; x < listMutators.Rows.Count; x++)
                listMutators.Rows[x].Cells[1].Value = "Y";
            
            for (int x = 0; x < listExecutions.Rows.Count; x++)
                listExecutions.Rows[x].Cells[1].Value = "Y";

            for (int x = 0; x < listCharacters.Rows.Count; x++)
                listCharacters.Rows[x].Cells[1].Value = "Y";
        }

        private void cmdMaxBeast_Click(object sender, EventArgs e)
        {
            foreach (Control x in panelBeast.Controls)
            {
                if (x is IntegerInput)
                {
                    IntegerInput i = (IntegerInput)x;
                    i.Value = i.MaxValue;
                }
            }
            for (int x = 0; x < listBeastEnemy.Rows.Count; x++)
            {
                listBeastEnemy.Rows[x].Cells[1].Value = 999999999;
                listBeastEnemy.Rows[x].Cells[2].Value = 999999999;
            }
            for (int x = 0; x < listBeastKills.Rows.Count; x++)
                listBeastKills.Rows[x].Cells[1].Value = 999999999;
        }

        private static readonly int[] rankXp = new int[]
        {
            0,
            1000,
            2500,
            4500,
            7000,
            10000,
            13500,
            17500,
            22000,
            27125,
            32875,
            39250,
            46250,
            53875,
            62125,
            71000,
            80500,
            90625,
            101375,
            112875,
            125125,
            138125,
            151875,
            166375,
            181625,
            197625,
            214375,
            231875,
            250125,
            269125,
            288875,
            309375,
            330325,
            352625,
            375375,
            398875,
            423125,
            448125,
            473875,
            500375,
            527625,
            555625,
            584375,
            613875,
            644125,
            675125,
            706875,
            739375,
            772625,
            806750,
            841750,
            877625,
            914375,
            952000,
            990500,
            1029875,
            1070125,
            1111250,
            1153250,
            1196250,
            1240250,
            1285250,
            1331250,
            1378250,
            1426250,
            1475250,
            1525250,
            1576250,
            1628250,
            1681250,
            1735250,
            1790250,
            1846250,
            1903250,
            1961250,
            2020250,
            2080250,
            2141250,
            2203250,
            2266750,
            2331750,
            2398250,
            2466250,
            2535750,
            2606750,
            2679250,
            2753250,
            2828750,
            2905750,
            2984250,
            3064250,
            3145750,
            3228750,
            3313250,
            3399250,
            3486750,
            3575750,
            3666250,
            3758250,
            3851750,
            10000000
        };

        private void cbRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            intXp.MinValue = rankXp[cbRank.SelectedIndex];
            intXp.MaxValue = rankXp[cbRank.SelectedIndex + 1] - 1;
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            if (UI.messageBox("This will reset all of your stats to 0 and lock all of your challenges, ribbons, and medals.\n\nContinue?",
                "Reset and Lock Stats", MessageBoxIcon.Warning, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3)
                != DialogResult.Yes)
                return;

            foreach (Entry entry in Stats)
            {
                int entryInt = (int)entry.ID;
                if (entryInt >= 0x0002 && entryInt <= 0x01F6)
                    entry.nData = 0;
                else if (entryInt == 0x020F)
                    entry.nData = 0;
                else if (entryInt == 0x0210)
                    entry.nData = 1;
                else if (entryInt >= 0x0211 && entryInt <= 0x05F7)
                    entry.nData = 0;
            }

            LoadAll();
        }

        private void cmdOnlineMods1_Click(object sender, EventArgs e)
        {
            var meta = new FormConfig.ButtonMeta();
            meta.FormMetaIndex = FormConfig.getFormMetaIndex(FormID.GearsOfWar3ProfileData);
            meta.DeviceIndex = -1;
            meta.FatxPath = null;
            meta.CachePartition = false;

            FormConfig.openForm(meta);
        }
    }
}
