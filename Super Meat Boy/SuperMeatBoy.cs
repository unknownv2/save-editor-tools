using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horizon.Functions;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;

namespace Horizon.PackageEditors.Super_Meat_Boy
{
    public partial class SuperMeatBoy : EditorControl
    {
        //public static readonly string FID = "58410A5A";
        public SuperMeatBoy()
        {
            InitializeComponent();
            TitleID = FormID.SuperMeatBoy;
            
        }

        public override void revertForm()
        {
            tabLevel.Visible = false;
            rbPackageEditor.Refresh();
        }

        private SuperMeatBoySave save;
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
            {
                UI.messageBox("You must complete 1 level before modding this save!", "Empty Save", MessageBoxIcon.Warning);
                return false;
            }
            save = new SuperMeatBoySave(IO);
            intTotalDeaths.Value = save.TotalDeaths;
            isBusy = true;
            for (int x = 0; x < panelChapters.Controls.Count; x++)
                if (panelChapters.Controls[x] is CheckBoxX)
                    ((CheckBoxX)panelChapters.Controls[x]).Checked = save.getChapterUnlock(((CheckBoxX)panelChapters.Controls[x]).TabIndex);
            for (int x = 0; x < panelCharacters.Controls.Count; x++)
                if (panelCharacters.Controls[x] is ButtonX && panelCharacters.Controls[x].TabIndex < 27)
                    ((ButtonX)panelCharacters.Controls[x]).Checked = save.getCharacterUnlock(((ButtonX)panelCharacters.Controls[x]).TabIndex);
            isBusy = false;
            tabMain.Select();
            rbPackageEditor.Refresh();
            return true;
        }

        private bool isBusy = false;
        public override void Save()
        {
            save.TotalDeaths = intTotalDeaths.Value;
            for (int x = 0; x < panelCharacters.Controls.Count; x++)
                if (panelCharacters.Controls[x] is ButtonX && panelCharacters.Controls[x].TabIndex < 27)
                    save.setCharacterUnlock(panelCharacters.Controls[x].TabIndex, ((ButtonX)panelCharacters.Controls[x]).Checked);
            save.Write(IO);
        }

        private void chX_CheckedChanged(object sender, EventArgs e)
        {
            int bI = ((CheckBoxX)sender).TabIndex + 9;
            for (int x = 0; x < panelChapters.Controls.Count; x++)
                if (panelChapters.Controls[x].TabIndex == bI)
                {
                    ((ButtonX)panelChapters.Controls[x]).Enabled = ((CheckBoxX)sender).Checked;
                    if (((CheckBoxX)sender).TabIndex == currentChapter)
                    {
                        tabLevel.Visible = false;
                        rbPackageEditor.Refresh();
                    }
                }
            if (!isBusy)
                save.setChapterUnlock(((CheckBoxX)sender).TabIndex, ((CheckBoxX)sender).Checked);
        }

        private int currentChapter;
        private void chXb_Click(object sender, EventArgs e)
        {
            currentChapter = ((ButtonX)sender).TabIndex - 9;
            tabLevel.Text = ((ButtonX)sender).Text;
            tabLevel.Visible = true;
            rbPackageEditor.Refresh();
            tabLevel.Select();
            isBusy = true;
            cmdLevelGlitchUnlocked.Checked = save.Chapters[currentChapter].MemoryGlitchUnlocked;
            cmdLevelBossComplete.Checked = save.Chapters[currentChapter].BossComplete;
            panelBoss.Enabled = panelGlitch.Enabled = currentChapter != 6;
            comboLevels.Items.Clear();
            comboLevels.Items.AddRange(save.getLevelTags(currentChapter));
            isBusy = false;
            comboLevels.SelectedIndex = 0;
        }

        private int currentLevel;
        private void comboLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLevel = comboLevels.SelectedIndex;
            if (!isBusy)
            {
                isBusy = true;
                cmdLevelBandage.Checked = save.Chapters[currentChapter].Levels[currentLevel].BandageObtained;
                cmdLevelWarpZone.Checked = save.Chapters[currentChapter].Levels[currentLevel].WarpZoneUnlocked;
                cmdLevelComplete.Checked = save.Chapters[currentChapter].Levels[currentLevel].Completed;
                fixTime();
                isBusy = false;
            }
        }

        private void cmdUnlockChapters_Click(object sender, EventArgs e)
        {
            if (UI.messageBox("Unlock and complete everything in the game?",
                "Beat the Game", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel,
                MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                for (int x = 0; x < panelChapters.Controls.Count; x++)
                    if (panelChapters.Controls[x] is CheckBoxX)
                        ((CheckBoxX)panelChapters.Controls[x]).Checked = true;
                cmdUnlockCharacters_Click(null, null);
                for (int x = 0; x < save.Chapters.Length; x++)
                    if (x != 7)
                    {
                        save.Chapters[x].MemoryGlitchUnlocked = save.Chapters[x].BossComplete = true;
                        for (int i = 0; i < save.Chapters[x].Levels.Length; i++)
                        {
                            save.Chapters[x].Levels[i].WarpZoneUnlocked = save.Chapters[x].Levels[i].BandageObtained = x != 6;
                            if (!save.Chapters[x].Levels[i].Completed)
                                save.Chapters[x].Levels[i].TimeCompleted = 2;
                            save.Chapters[x].Levels[i].Completed = true;
                        }
                    }
                tabLevel.Visible = false;
                rbPackageEditor.Refresh();
                UI.messageBox("Game complete! All uncompleted levels were set to 2.0 seconds.", "Game Complete", MessageBoxIcon.Information);
            }
            
        }

        private void cmdUnlockCharacters_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < panelCharacters.Controls.Count; x++)
                if (panelCharacters.Controls[x] is ButtonX && panelCharacters.Controls[x].TabIndex != 30)
                    ((ButtonX)panelCharacters.Controls[x]).Checked = true;
        }

        private void cmdLevelComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                save.Chapters[currentChapter].Levels[currentLevel].Completed = cmdLevelComplete.Checked;
                fixTime();
            }
            
        }

        private void fixTime()
        {
            numLevelTime.Enabled = cmdLevelComplete.Checked;
            if (numLevelTime.Enabled && save.Chapters[currentChapter].Levels[currentLevel].TimeCompleted == (float)numLevelTime.Maximum)
                numLevelTime.Value = 2;
            else
                numLevelTime.Value = (decimal)save.Chapters[currentChapter].Levels[currentLevel].TimeCompleted;
        }

        private void cmdLevelBandage_CheckedChanged(object sender, EventArgs e)
        {
            if (!isBusy)
                save.Chapters[currentChapter].Levels[currentLevel].BandageObtained = cmdLevelBandage.Checked;
        }

        private void cmdLevelWarpZone_CheckedChanged(object sender, EventArgs e)
        {
            if (!isBusy)
                save.Chapters[currentChapter].Levels[currentLevel].WarpZoneUnlocked = cmdLevelWarpZone.Checked;
        }

        private void numLevelTime_ValueChanged(object sender, EventArgs e)
        {
            save.Chapters[currentChapter].Levels[currentLevel].TimeCompleted = (float)numLevelTime.Value;
        }

        private void cmdLevelGlitchUnlocked_CheckedChanged(object sender, EventArgs e)
        {
            if (!isBusy)
                save.Chapters[currentChapter].MemoryGlitchUnlocked = cmdLevelGlitchUnlocked.Checked;
        }

        private void cmdLevelBossComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (!isBusy)
                save.Chapters[currentChapter].BossComplete = cmdLevelBossComplete.Checked;
        }

        private void ch7b_Click(object sender, EventArgs e)
        {
            UI.messageBox("Saving is disabled in Demo mode! It uses the same block as Chapter 1.", "Saving Disabled", MessageBoxIcon.Information);
        }

        private void cmdBossInfo_Click(object sender, EventArgs e)
        {
            UI.messageBox("The boss stage can be flagged as completed, but it won't\nshow up unless you complete the required number of levels!", "Boss Stage Info", MessageBoxIcon.Information);
        }
    }
}
