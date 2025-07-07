using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Alan_Wake
{
    public partial class AlanWake : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "4D530805";

        /// <summary>
        /// Our alan wake class editor.
        /// </summary>
        public AlanWakeClass Alan_Wake_Class { get; set; }

        public AlanWake()
        {
            InitializeComponent();
            TitleID = FormID.AlanWake;
            //Set our title ID
            
            intKillsRevolver.MaxValue = int.MaxValue;
            intKillsFlaregun.MaxValue = int.MaxValue;
            intKillsFlashbang.MaxValue = int.MaxValue;
            intKillsHuntingRifle.MaxValue = int.MaxValue;
            intKillsVehicles.MaxValue = int.MaxValue;
            intIndirectKills.MaxValue = int.MaxValue;
            intBirdsKilled.MaxValue = int.MaxValue;
            intPoltergeistDestroyed.MaxValue = int.MaxValue;
            intKillsShotgun.MaxValue = int.MaxValue;
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile(SettingAsString(202)))
                return false;

            //Read our save
            Alan_Wake_Class = new AlanWakeClass(IO);

            //Select our first item in our unlocked levels.
            comboDifficultyLevelUnlock.SelectedIndex = 0;

            //Clear our list
            lstManuscriptPages.Items.Clear();

            //Loop for each level
            foreach (AlanWakeClass.ManuscriptPage mpage in Enum.GetValues(typeof(AlanWakeClass.ManuscriptPage)))
            {
                //Create our list view item
                ListViewItem lvi = new ListViewItem(AlanWakeClass.EnumStrToStr(mpage.ToString()));
                //Determine our checked status
                lvi.Checked = (Alan_Wake_Class.Manuscript_Pages.Contains(mpage));
                //Set it's tag
                lvi.Tag = (int)mpage;
                //Add it to our list
                lstManuscriptPages.Items.Add(lvi);
            }
            
            //Load our stats labels.
            LoadStatsLabels();
            //Load our found labels.
            LoadFoundLabels();

            //Our file is read correctly.
            return true;
        }
        public override void Save()
        {
            //Set our values
            Alan_Wake_Class.Kills_With_Revolver = intKillsRevolver.Value;
            Alan_Wake_Class.Kills_With_Flaregun = intKillsFlaregun.Value;
            Alan_Wake_Class.Kills_With_Flashbangs = intKillsFlashbang.Value;
            Alan_Wake_Class.Kills_With_Hunting_Rifle = intKillsHuntingRifle.Value;
            Alan_Wake_Class.Kills_With_Vehicles = intKillsVehicles.Value;
            Alan_Wake_Class.Indirect_Kills = intIndirectKills.Value;
            Alan_Wake_Class.Poltergeist_Objects_Destroyed = intPoltergeistDestroyed.Value;
            Alan_Wake_Class.Birds_Killed = intBirdsKilled.Value;
            Alan_Wake_Class.Kills_With_Shotgun = intKillsShotgun.Value;
            //Save
            Alan_Wake_Class.Write();
        }
        private void LoadStatsLabels()
        {
            intKillsRevolver.Value = Alan_Wake_Class.Kills_With_Revolver;
            intKillsFlaregun.Value = Alan_Wake_Class.Kills_With_Flaregun;
            intKillsFlashbang.Value = Alan_Wake_Class.Kills_With_Flashbangs;
            intKillsHuntingRifle.Value = Alan_Wake_Class.Kills_With_Hunting_Rifle;
            intKillsVehicles.Value = Alan_Wake_Class.Kills_With_Vehicles;
            intIndirectKills.Value = Alan_Wake_Class.Indirect_Kills;
            intPoltergeistDestroyed.Value = Alan_Wake_Class.Poltergeist_Objects_Destroyed;
            intBirdsKilled.Value = Alan_Wake_Class.Birds_Killed;
            intKillsShotgun.Value = Alan_Wake_Class.Kills_With_Shotgun;
        }
        private void LoadFoundLabels()
        {
            string none = SettingAsString(241);
            lblChestsFound.Text = Alan_Wake_Class.Chests_Found == null ? none : Alan_Wake_Class.Chests_Found.Count + " / " + Enum.GetValues(typeof(AlanWakeClass.ChestsOption)).Length;
            lblRadiosFound.Text = Alan_Wake_Class.Radios_Found == null ? none : Alan_Wake_Class.Radios_Found.Count + " / " + Enum.GetValues(typeof(AlanWakeClass.RadioShowOption)).Length;
            lblSignsFound.Text = Alan_Wake_Class.Signs_Found == null ? none : Alan_Wake_Class.Signs_Found.Count + " / " + Enum.GetValues(typeof(AlanWakeClass.SignOption)).Length;
            lblThermosesFound.Text = Alan_Wake_Class.Thermoses_Found == null ? none : Alan_Wake_Class.Thermoses_Found.Count + " / " + Enum.GetValues(typeof(AlanWakeClass.CoffeeThermosOption)).Length;
            lblTVShowsFound.Text = Alan_Wake_Class.TVShows_Found == null ? none : Alan_Wake_Class.TVShows_Found.Count + " / " + Enum.GetValues(typeof(AlanWakeClass.TVShowOption)).Length;
            lblCanPyramidFound.Text = Alan_Wake_Class.CanPyramids_Found == null ? none : Alan_Wake_Class.CanPyramids_Found.Count + " / " + Enum.GetValues(typeof(AlanWakeClass.CanPyramidOption)).Length;
        }

        private void comboDifficultyLevelUnlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear our listview items
            lstUnlockedLevels.Items.Clear();

            //Create our list variable
            List<AlanWakeClass.LevelOption> Unlocked_Levels = null;
            //Get our list based off difficulty
            switch (comboDifficultyLevelUnlock.SelectedIndex)
            {
                case 0:
                    Unlocked_Levels = Alan_Wake_Class.Normal_Unlocked_Levels;
                    break;
                case 1:
                    Unlocked_Levels = Alan_Wake_Class.Hard_Unlocked_Levels;
                    break;
                case 2:
                    Unlocked_Levels = Alan_Wake_Class.Nightmare_Unlocked_Levels;
                    break;
            }
            //Loop for each level
            foreach (AlanWakeClass.LevelOption level in Enum.GetValues(typeof(AlanWakeClass.LevelOption)))
            {
                //Create our list view item
                ListViewItem lvi = new ListViewItem(AlanWakeClass.EnumStrToStr(level.ToString()));
                //Determine our checked status
                lvi.Checked = (Unlocked_Levels.Contains(level));
                //Set it's tag
                lvi.Tag = (int)level;
                //Add it to our list
                lstUnlockedLevels.Items.Add(lvi);
            }
        }


        private void cmdUnlockAllForCurrentDif_Click(object sender, EventArgs e)
        {
            CheckAllItems();
        }
        private void CheckAllItems()
        {
            //Check all the items in our list
            for (int i = 0; i < lstUnlockedLevels.Items.Count; i++)
                //Check it.
                lstUnlockedLevels.Items[i].Checked = true;
        }
        private void cmdUnlockAllForAllDif_Click(object sender, EventArgs e)
        {
            //Get our selected index
            int selIndex = comboDifficultyLevelUnlock.SelectedIndex;
            //Loop for each item
            for (int i = 0; i < comboDifficultyLevelUnlock.Items.Count; i++)
            {
                //Select our item.
                comboDifficultyLevelUnlock.SelectedIndex = i;
                //Check all our items
                CheckAllItems();
            }
            //Select our original index
            comboDifficultyLevelUnlock.SelectedIndex = selIndex;
        }

        private void cmdMaxKillsRevolver_Click(object sender, EventArgs e)
        {
            intKillsRevolver.Value = intKillsRevolver.MaxValue;
        }

        private void cmdMaxKillsFlaregun_Click(object sender, EventArgs e)
        {
            intKillsFlaregun.Value = intKillsFlaregun.MaxValue;
        }

        private void cmdMaxKillsHuntingRifle_Click(object sender, EventArgs e)
        {
            intKillsHuntingRifle.Value = intKillsHuntingRifle.MaxValue;
        }

        private void cmdMaxKillsFlashbang_Click(object sender, EventArgs e)
        {
            intKillsFlashbang.Value = intKillsFlashbang.MaxValue;
        }

        private void cmdMaxKillsVehicles_Click(object sender, EventArgs e)
        {
            intKillsVehicles.Value = intKillsVehicles.MaxValue;
        }

        private void cmdMaxKillsIndirect_Click(object sender, EventArgs e)
        {
            intIndirectKills.Value = intIndirectKills.MaxValue;
        }

        private void cmdMaxPoltergeistDestroyed_Click(object sender, EventArgs e)
        {
            intPoltergeistDestroyed.Value = intPoltergeistDestroyed.MaxValue;
        }

        private void cmdMaxKillsBirds_Click(object sender, EventArgs e)
        {
            intBirdsKilled.Value = intBirdsKilled.MaxValue;
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            intKillsRevolver.Value = intKillsRevolver.MaxValue;
            intKillsFlaregun.Value = intKillsFlaregun.MaxValue;
            intKillsHuntingRifle.Value = intKillsHuntingRifle.MaxValue;
            intKillsFlashbang.Value = intKillsFlashbang.MaxValue;
            intKillsVehicles.Value = intKillsVehicles.MaxValue;
            intIndirectKills.Value = intIndirectKills.MaxValue;
            intPoltergeistDestroyed.Value = intPoltergeistDestroyed.MaxValue;
            intBirdsKilled.Value = intBirdsKilled.MaxValue;
            intKillsShotgun.Value = intKillsShotgun.MaxValue;
        }

        private void AddAllChests()
        {
            //Refresh our struct.
            Alan_Wake_Class.Chests_Found = new List<AlanWakeClass.ChestsOption>();
            //Loop for each option
            foreach (AlanWakeClass.ChestsOption opt in Enum.GetValues(typeof(AlanWakeClass.ChestsOption)))
                //Add our option
                Alan_Wake_Class.Chests_Found.Add(opt);
        }

        private void AddAllRadios()
        {
            //Refresh our struct.
            Alan_Wake_Class.Radios_Found = new List<AlanWakeClass.RadioShowOption>();
            //Loop for each option
            foreach (AlanWakeClass.RadioShowOption opt in Enum.GetValues(typeof(AlanWakeClass.RadioShowOption)))
                //Add our option
                Alan_Wake_Class.Radios_Found.Add(opt);
        }

        private void AddAllSigns()
        {
            //Refresh our struct.
            Alan_Wake_Class.Signs_Found = new List<AlanWakeClass.SignOption>();
            //Loop for each option
            foreach (AlanWakeClass.SignOption opt in Enum.GetValues(typeof(AlanWakeClass.SignOption)))
                //Add our option
                Alan_Wake_Class.Signs_Found.Add(opt);
        }

        private void AddAllThermoses()
        {
            //Refresh our struct.
            Alan_Wake_Class.Thermoses_Found = new List<AlanWakeClass.CoffeeThermosOption>();
            //Loop for each option
            foreach (AlanWakeClass.CoffeeThermosOption opt in Enum.GetValues(typeof(AlanWakeClass.CoffeeThermosOption)))
                //Add our option
                Alan_Wake_Class.Thermoses_Found.Add(opt);
        }

        private void AddAllTVShows()
        {
            //Refresh our struct.
            Alan_Wake_Class.TVShows_Found = new List<AlanWakeClass.TVShowOption>();
            //Loop for each option
            foreach (AlanWakeClass.TVShowOption opt in Enum.GetValues(typeof(AlanWakeClass.TVShowOption)))
                //Add our option
                Alan_Wake_Class.TVShows_Found.Add(opt);
        }

        private void AddAllPyramid()
        {
            //Refresh our struct.
            Alan_Wake_Class.CanPyramids_Found = new List<AlanWakeClass.CanPyramidOption>();
            //Loop for each option
            foreach (AlanWakeClass.CanPyramidOption opt in Enum.GetValues(typeof(AlanWakeClass.CanPyramidOption)))
                //Add our option
                Alan_Wake_Class.CanPyramids_Found.Add(opt);
        }

        private void cmdAddAllChests_Click(object sender, EventArgs e)
        {
            //Add our data
            AddAllChests();
            //Reload our UI
            LoadFoundLabels();
        }

        private void cmdAddAllRadios_Click(object sender, EventArgs e)
        {
            //Add our data
            AddAllRadios();
            //Reload our UI
            LoadFoundLabels();
        }

        private void cmdAddAllSigns_Click(object sender, EventArgs e)
        {
            //Add our data
            AddAllSigns();
            //Reload our UI
            LoadFoundLabels();
        }

        private void cmdAddAllThermoses_Click(object sender, EventArgs e)
        {
            //Add our data
            AddAllThermoses();
            //Reload our UI
            LoadFoundLabels();
        }

        private void cmdAddAllTVShows_Click(object sender, EventArgs e)
        {
            //Add our data
            AddAllTVShows();
            //Reload our UI
            LoadFoundLabels();
        }

        private void cmdAddAllPyramid_Click(object sender, EventArgs e)
        {
            //Add our data
            AddAllPyramid();
            //Reload our UI
            LoadFoundLabels();
        }

        private void cmdAddAllChestsRadiosSignsThermosShows_Click(object sender, EventArgs e)
        {
            AddAllChests();
            AddAllRadios();
            AddAllSigns();
            AddAllThermoses();
            AddAllTVShows();
            AddAllPyramid();
            //Reload our UI
            LoadFoundLabels();
        }

        private void cmdMaxKillsShotgun_Click(object sender, EventArgs e)
        {
            intKillsShotgun.Value = intKillsShotgun.MaxValue;
        }

        private void cmdUnlockAllManuscripts_Click(object sender, EventArgs e)
        {
            //Loop for each manuscript item.
            for (int i = 0; i < lstManuscriptPages.Items.Count; i++)
                //Check the indexed item.
                lstManuscriptPages.Items[i].Checked = true;
        }

        private void lstUnlockedLevels_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
            //Get our level option
            AlanWakeClass.LevelOption opt = (AlanWakeClass.LevelOption)((int)lstUnlockedLevels.Items[e.Item.Index].Tag);

            //Get our list based off difficulty
            switch (comboDifficultyLevelUnlock.SelectedIndex)
            {
                case 0:
                    if (e.Item.Checked)
                    {
                        if (!Alan_Wake_Class.Normal_Unlocked_Levels.Contains(opt))
                            Alan_Wake_Class.Normal_Unlocked_Levels.Add(opt);
                    }
                    else
                    {
                        Alan_Wake_Class.Normal_Unlocked_Levels.Remove(opt);
                    }
                    break;
                case 1:
                    if (e.Item.Checked)
                    {
                        if (!Alan_Wake_Class.Hard_Unlocked_Levels.Contains(opt))
                            Alan_Wake_Class.Hard_Unlocked_Levels.Add(opt);
                    }
                    else
                    {
                        Alan_Wake_Class.Hard_Unlocked_Levels.Remove(opt);
                    }
                    break;
                case 2:
                    if (e.Item.Checked)
                    {
                        if (!Alan_Wake_Class.Nightmare_Unlocked_Levels.Contains(opt))
                            Alan_Wake_Class.Nightmare_Unlocked_Levels.Add(opt);
                    }
                    else
                    {
                        Alan_Wake_Class.Nightmare_Unlocked_Levels.Remove(opt);
                    }
                    break;
            }
        }

        private void lstManuscriptPages_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //Get our manuscript page
            AlanWakeClass.ManuscriptPage opt = (AlanWakeClass.ManuscriptPage)((int)lstManuscriptPages.Items[e.Item.Index].Tag);

            //If our item is checked.
            if (e.Item.Checked)
            {
                //If it doesn't contain our item.
                if (!Alan_Wake_Class.Manuscript_Pages.Contains(opt))
                    //Add it.
                    Alan_Wake_Class.Manuscript_Pages.Add(opt);
            }
            else
            {
                //Remove our item.
                Alan_Wake_Class.Manuscript_Pages.Remove(opt);
            }

        }
        private void CheckEpisodes(string filter)
        {
            //Loop for each manuscript item.
            for (int i = 0; i < lstManuscriptPages.Items.Count; i++)
                //If it contains our ep string
                if(lstManuscriptPages.Items[i].Text.ToString().Contains(filter))
                    //Check the indexed item.
                    lstManuscriptPages.Items[i].Checked = true;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            CheckEpisodes(SettingAsString(34));
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            CheckEpisodes(SettingAsString(33));
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            CheckEpisodes(SettingAsString(205));
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            CheckEpisodes(SettingAsString(228));
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            CheckEpisodes(SettingAsString(84));
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            CheckEpisodes(SettingAsString(101));
        }
    }
}
