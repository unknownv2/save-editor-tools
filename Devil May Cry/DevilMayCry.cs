using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevilMayCry;
using Horizon.Server;

namespace Horizon.PackageEditors.Devil_May_Cry
{
    public partial class DevilMayCry : EditorControl
    {
        //public static readonly string FID = "43430824";
        private DevilMayCrySaveProfile _profile;
        private static Dictionary<string, string> _upgrades;
        private bool _unlockAllUpgrades;
        private static readonly string[] StyleRanks = {"D", "C", "B", "A", "S", "SS", "SSS", "None"};
        private DevilMayCrySaveProfile.MissionProgress _mission;

        private bool IsDLC = false;
        public DevilMayCry()
        {
            InitializeComponent();
            TitleID = FormID.DevilMayCry;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("INFO"))
                return false;

            LoadUnlocks();
            LoadSave();
            LoadUpgrades();
            LoadMissionProgress();
#if INT2
            btnItemExtract.Visible = btnItemInject.Visible = true;
#endif
            vergilTabItem.Visible = IsDLC;
            unlocksTabItem.Visible = !IsDLC;

            RefreshUnlockPanel(IsDLC ? rbPanelVergil : rbPanelUnlocks);

            Refresh();
            return true;
        }

        private void LoadUnlocks()
        {
            // for Dante
            cmdAngelBoost.Tag = Upgrades.AngelBoost;
            cmdAngelEvade.Tag = Upgrades.AngelEvade;
            cmdAquila.Tag = Upgrades.Aquila;
            cmdArbiter.Tag = Upgrades.Arbiter;
            cmdDemonEvade.Tag = Upgrades.DemonEvade;
            cmdDevilTrigger.Tag = Upgrades.DevilTrigger;
            cmdEbonyAndIvory.Tag = Upgrades.EbonyAndIvory;
            cmdEryx.Tag = Upgrades.Eryx;
            cmdKablooey.Tag = Upgrades.Kablooey;
            cmdAngelLiftAndLeap.Tag = Upgrades.OphionAngelLiftAndLeap;
            cmdDemonPull.Tag = Upgrades.OphionDemonPull;
            cmdOsiris.Tag = Upgrades.Osiris;
            cmdRevenant.Tag = Upgrades.Revenant;

            // for Vergil
            cmdYamatoAngel.Tag = VergilUpgrades.YamatoAngelMode;
            cmdYamatoDemon.Tag = VergilUpgrades.YamatoDemonMode;
            cmdSwordIllusion.Tag = VergilUpgrades.SwordIllusion;
            cmdVergilDT.Tag = VergilUpgrades.DevilTrigger;
            cmdDoppleganger.Tag = VergilUpgrades.Doppleganger;
        }

        private void LoadUpgrades()
        {
            if(_upgrades != null)
                return; 

            _upgrades = new Dictionary<string, string>();
            TextReader reader =
                new StreamReader(new MemoryStream(new WebClient().DownloadData(Config.baseURL + "dmc/upgrades"), false));
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string name = Regex.Match(line, "(?<=\").*?(?=\")").Value;
                string intName = line.Substring(name.Length + 3);
                _upgrades.Add(name, intName);
                dgvUpgrades.Rows.Add(new object[] {name, _profile.IsUpgradeUnlocked(intName)});
            }
        }

        private void LoadSave()
        {
            if (_profile != null)
                _profile.Close();

            _profile = new DevilMayCrySaveProfile(Package.StfsContentPackage.GetEndianIO("PROFILE", true));

            // load the advanced editor
            advListView.Items.Clear();
            foreach (var stat in _profile.GameStats)
            {
                advListView.Items.Add(new ListViewItem(new[] {stat.Key, ReadStat(stat.Key).ToString()}));
            }

            // load the 'basic' editor
            intRedOrbs.Value = (int) ReadStat("Orb_Red");
            intGoldOrbs.Value = (int) ReadStat("Orb_Gold");
            intVitalS.Value = (int) ReadStat("VitalStar_Small");
            intVitalL.Value = (int) ReadStat("VitalStar_Large");
            intDTStarS.Value = (int) ReadStat("DevilTriggerStar_Small");
            intDTStarL.Value = (int) ReadStat("DevilTriggerStar_Large");
            intUpgradePoints.Value = 0;

            sliderHealth.Value = (float) ReadStat("MAXHEALTH_PLAYER");
            sliderHealth.Checked = (bool) ReadStat("MAXHEALTH_SET");

            sliderDevilTrigger.Value = (float) ReadStat("MAXTRIGGERPOINTS_PLAYER");
            sliderDevilTrigger.Checked = (bool) ReadStat("MAXTRIGGERPOINTS_SET");

            txtValue.ReadOnly = false;
        }

        private void LoadMissionProgress()
        {
            int missionCount = _profile.Missions[0].MissionCompletion.Count;
            IsDLC = missionCount == 06;

            if (cmbMission.Items.Count == 0)
            {
                for (int i = 0; i < missionCount; i++)
                {
                    cmbMission.Items.Add((i + 1).ToString(CultureInfo.InvariantCulture));
                }
                var difficulties = Enum.GetValues(typeof (MissionDifficulty));
                foreach (var difficulty in difficulties)
                {
                    cmbDifficulty.Items.Add(Regex.Replace(difficulty.ToString(), "([a-z])([A-Z])", "$1 $2"));
                }
                cmbMissionRank.Items.AddRange(StyleRanks);
            }

            // set default mission
            _mission = _profile.Missions[0].MissionCompletion[0];
            cmbDifficulty.SelectedIndex = 0;
            cmbMission.SelectedIndex = 0;

            // load mission progress unlocks
            btnSonOfSparda.Checked = _profile.Missions[(int) MissionDifficulty.Nephilim].DifficultyCompleted
                                     | _profile.Missions[(int) MissionDifficulty.DevilHunter].DifficultyCompleted
                                     | _profile.Missions[(int) MissionDifficulty.Human].DifficultyCompleted;

            btnDanteMustDie.Checked = _profile.Missions[(int) MissionDifficulty.SonOfSparda].DifficultyCompleted;
            btnHeavenOrHell.Checked = _profile.Missions[(int) MissionDifficulty.DanteMustDie].DifficultyCompleted;
            btnHellAndHell.Checked = _profile.Missions[(int) MissionDifficulty.HeavenOrHell].DifficultyCompleted;
        }

        public override void Save()
        {
            // write the settings from the listing
            foreach (ListViewItem stat in advListView.Items)
            {
                WriteStat(stat.Text, stat.SubItems[1].Text);
            }
            // write the settings from the 'Simple Edit' Tab
            WriteStat("Orb_Red", intRedOrbs.Value);
            WriteStat("Orb_Gold", intGoldOrbs.Value);
            long orbCheck = (int) ReadStat("Orb_White_Acquired") + intUpgradePoints.Value;
            WriteStat("Orb_White_Acquired", orbCheck >= intUpgradePoints.MaxValue ? intUpgradePoints.MaxValue : (int)ReadStat("Orb_White_Acquired") + intUpgradePoints.Value);
            orbCheck = (int)ReadStat("Orb_White") + intUpgradePoints.Value;
            WriteStat("Orb_White", orbCheck >= intUpgradePoints.MaxValue ? intUpgradePoints.MaxValue : (int)ReadStat("Orb_White") + intUpgradePoints.Value);
            WriteStat("VitalStar_Small", intVitalS.Value);
            WriteStat("VitalStar_Large", intVitalL.Value);
            WriteStat("DevilTriggerStar_Small", intDTStarS.Value);
            WriteStat("DevilTriggerStar_Large", intDTStarL.Value);

            if (sliderHealth.Checked)
            {
                WriteStat("MAXHEALTH_PLAYER", sliderHealth.Value);
                WriteStat("MAXHEALTH_SET", true);
            }

            if (sliderDevilTrigger.Checked)
            {
                WriteStat("MAXTRIGGERPOINTS_PLAYER", sliderDevilTrigger.Value);
                WriteStat("MAXTRIGGERPOINTS_SET", true);
            }

            _profile.Missions[(int) MissionDifficulty.Human].DifficultyCompleted = btnSonOfSparda.Checked;
            _profile.Missions[(int) MissionDifficulty.DevilHunter].DifficultyCompleted = btnSonOfSparda.Checked;
            _profile.Missions[(int) MissionDifficulty.Nephilim].DifficultyCompleted = btnSonOfSparda.Checked;
            _profile.Missions[(int) MissionDifficulty.SonOfSparda].DifficultyCompleted = btnDanteMustDie.Checked;
            _profile.Missions[(int) MissionDifficulty.DanteMustDie].DifficultyCompleted = btnHeavenOrHell.Checked;
            _profile.Missions[(int) MissionDifficulty.HeavenOrHell].DifficultyCompleted = btnHellAndHell.Checked;

            // mission progress
            UpdateCurrentMission();

            _profile.SaveMissionProgress();
            // upgrades
            SaveUpgrades();
            // flush file changes
            _profile.Save();
        }

        private void SaveUpgrades()
        {
            _profile.WriteUpgrades(_unlockAllUpgrades ? _upgrades.Values.ToList() : RetrieveUpgradesFromGrid());
        }

        private List<string> RetrieveUpgradesFromGrid()
        {
            return (from DataGridViewRow upgrade in dgvUpgrades.Rows
                    where Convert.ToBoolean(upgrade.Cells[0x01].Value)
                    select _upgrades[upgrade.Cells[0x00].Value.ToString()]).ToList();
        }

        public object ReadStat(string statName)
        {
            if (_profile == null)
                throw new Exception("Profile class has not yet loaded!");

            return _profile.ReadStat(statName);
        }

        public void WriteStat(string name, object value)
        {
            if (_profile != null)
                _profile.WriteStat(name, value);
        }

        private void DisplayCurrentMission()
        {
            btnCmpgnUnlocked.Checked = _mission.IsCompleted;
            cmbMissionRank.SelectedIndex = _mission.StyleRank == StyleRank.None
                                               ? StyleRanks.Length - 1
                                               : (int) _mission.StyleRank;
        }

        private void UpdateCurrentMission()
        {
            if (cmbMission.SelectedIndex == -1 || _mission == null)
                return;

            _mission.IsCompleted = btnCmpgnUnlocked.Checked;
            _mission.StyleRank = cmbMissionRank.Text == "None"
                                     ? StyleRank.None
                                     : (StyleRank) cmbMissionRank.SelectedIndex;
            _mission = _profile.Missions[cmbDifficulty.SelectedIndex].MissionCompletion[cmbMission.SelectedIndex];
                // = cmbDifficulty.SelectedIndex;
        }

        private void RibbonTabChanged(object sender, EventArgs e)
        {
            RefreshUnlockPanel(IsDLC ? rbPanelVergil : rbPanelUnlocks);
        }

        private void RefreshUnlockPanel(RibbonPanel panel)
        {
            if (_profile == null || _profile.Unlocks == null || _profile.Unlocks.Count == 0)
                return;

            foreach (var control in panel.Controls)
            {
                if (!(control is ButtonX)) continue;
                var unlockBtn = (ButtonX) control;
                if (unlockBtn.Tag == null) continue;
                unlockBtn.Checked = _profile.Unlocks.Contains((int) unlockBtn.Tag);
            }
        }

        private void SelectedDifficultyChanged(object sender, EventArgs e)
        {
            UpdateCurrentMission();
            DisplayCurrentMission();
        }

        private void SelectedMissionChanged(object sender, EventArgs e)
        {
            UpdateCurrentMission();
            DisplayCurrentMission();
        }

        private void ButtonClickUnlock(object sender, EventArgs e)
        {
            var button = (ButtonX) sender;
            if (button.Tag == null)
                return;

            var unlock = (int) button.Tag;

            if (button.Checked)
            {
                _profile.Unlocks.Add(unlock);
            }
            else
            {
                _profile.Unlocks.Remove(unlock);
            }
        }

        private void BtnClickExtract(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog {FileName = "profile.bin"};
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            _profile.Extract().Save(sfd.FileName);
        }

        private void BtnClickInject(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            _profile.Inject(File.ReadAllBytes(ofd.FileName));
            _profile.Save();

            // reload the save file after injecting new data
            LoadSave();
        }

        private void BtnClickUnlockAllUpgrades(object sender, EventArgs e)
        {
            _unlockAllUpgrades = !_unlockAllUpgrades;
        }

        private void StatsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (advListView.SelectedItems.Count == 0)
                return;

            txtValue.Text = advListView.SelectedItems[0].SubItems[1].Text;
        }

        private void StatsValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValue.Text))
                return;

            advListView.SelectedItems[0].SubItems[1].Text = txtValue.Text;
        }

        private void cmdMaxRedOrbs_Click(object sender, EventArgs e)
        {
            intRedOrbs.Value = intRedOrbs.MaxValue;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            intGoldOrbs.Value = intGoldOrbs.MaxValue;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            intVitalS.Value = intVitalS.MaxValue;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            intVitalL.Value = intVitalL.MaxValue;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            intDTStarS.Value = intDTStarS.MaxValue;
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            intUpgradePoints.Value = intUpgradePoints.MaxValue;
        } 

        private void buttonX5_Click(object sender, EventArgs e)
        {
            intDTStarL.Value = intDTStarL.MaxValue;
        }

        private void TxtStatSearch(object sender, EventArgs e)
        {
            ListViewItem foundItem = advListView.FindItemWithText(txtStat.Text, false, 0, true);
            if (foundItem != null)
            {
                advListView.TopItem = foundItem;
                txtValue.Text = string.Empty;
            }
        }

        private void unlocksTabItem_Click(object sender, EventArgs e)
        {

        }
    }
}
