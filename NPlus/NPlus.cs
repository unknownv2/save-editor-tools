using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Horizon.PackageEditors.NPlus
{
    public partial class NPlus : EditorControl
    {
        //public static readonly string FID = "5841085C";
        public NPlus()
        {
            InitializeComponent();
            TitleID = FormID.NPlus;
            
        }

        private NPlusSave save;
        private int cur;
        public override bool Entry()
        {
            if (!loadAllTitleSettings(EndianType.BigEndian))
                return false;
            save = new NPlusSave(IO.ToArray());
            for (int x = 0; x < save.Episodes.Count; x++)
                comboEpisode.Items.Add(save.Episodes[x].episodeString);
            comboEpisode.SelectedIndex = 0;
            isBusy = false;
            comboEpisode_SelectedIndexChanged(comboEpisode, null);
            return true;
        }

        private bool shownMessage = false;
        private bool isBusy = false;
        private void comboEpisode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                string curS = (string)comboEpisode.Items[comboEpisode.SelectedIndex];
                for (int x = 0; x < save.Episodes.Count; x++)
                    if (save.Episodes[x].episodeString == curS)
                        cur = x;
                fSolo.Enabled = ckSoloUnlocked.Enabled = ckSolo.Enabled = !save.Episodes[cur].isCoOp;
                fMultiplayer.Enabled = ckMultiplayerUnlocked.Enabled = ckMultiplayer.Enabled = save.Episodes[cur].hasMultiplayer;
                if (ckSolo.Checked != save.Episodes[cur].completedSolo)
                    ckSolo.Checked = save.Episodes[cur].completedSolo;
                else
                    ckSolo_CheckedChanged(null, null);
                if (ckMultiplayer.Checked != save.Episodes[cur].completedMultiplayer)
                    ckMultiplayer.Checked = save.Episodes[cur].completedMultiplayer;
                else
                    ckMultiplayer_CheckedChanged(null, null);
                if (ckSoloUnlocked.Checked != save.Episodes[cur].unlockedSolo)
                    ckSoloUnlocked.Checked = save.Episodes[cur].unlockedSolo;
                else
                    ckSoloUnlocked_CheckedChanged(null, null);
                if (ckMultiplayerUnlocked.Checked != save.Episodes[cur].unlockedMultiplayer)
                    ckMultiplayerUnlocked.Checked = save.Episodes[cur].unlockedMultiplayer;
                else
                    ckMultiplayerUnlocked_CheckedChanged(null, null);
                fSolo.Value = (decimal)save.Episodes[cur].soloTime;
                fMultiplayer.Value = (decimal)save.Episodes[cur].multiplayerTime;
                if (!shownMessage && save.Episodes[cur].episodeType == NPlusSave.Episode.EpisodeType.Exp)
                {
                    Functions.UI.messageBox("All episodes must be unlocked and completed in order for\nthe Expert Challanges to show up in-game!", "Expert Challanges", MessageBoxIcon.Information);
                    shownMessage = true;
                }
            }
        }

        private void ckSolo_CheckedChanged(object sender, EventArgs e)
        {
            if (save.Episodes[cur].completedSolo = fSolo.Enabled = ckSolo.Checked)
                fSolo.Value = (decimal)save.Episodes[cur].soloTime;
        }

        private void ckMultiplayer_CheckedChanged(object sender, EventArgs e)
        {
            if (save.Episodes[cur].completedMultiplayer = fMultiplayer.Enabled = ckMultiplayer.Checked)
                fMultiplayer.Value = (decimal)save.Episodes[cur].multiplayerTime;
        }

        private void ckSoloUnlocked_CheckedChanged(object sender, EventArgs e)
        {
            save.Episodes[cur].unlockedSolo = ckSolo.Enabled = ckSoloUnlocked.Checked;
            if (!ckSoloUnlocked.Checked)
                ckSolo.Checked = false;
        }

        private void ckMultiplayerUnlocked_CheckedChanged(object sender, EventArgs e)
        {
            save.Episodes[cur].unlockedMultiplayer = ckMultiplayer.Enabled = ckMultiplayerUnlocked.Checked;
            if (!ckMultiplayerUnlocked.Checked)
                ckMultiplayer.Checked = false;
        }

        private void fSolo_ValueChanged(object sender, EventArgs e)
        {
            save.Episodes[cur].soloTime = (float)fSolo.Value;
        }

        private void fMultiplayer_ValueChanged(object sender, EventArgs e)
        {
            save.Episodes[cur].multiplayerTime = (float)fMultiplayer.Value;
        }

        private void cmdComplete_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < save.Episodes.Count; x++)
            {
                bool isCo = save.Episodes[x].isCoOp;
                save.Episodes[x].unlockedSolo = !isCo;
                save.Episodes[x].completedSolo = !isCo;
                if (isCo)
                    save.Episodes[x].soloTime = 0;
                else if (save.Episodes[x].soloTime == 0)
                    save.Episodes[x].soloTime = 500;
                if (save.Episodes[x].hasMultiplayer)
                {
                    save.Episodes[x].completedMultiplayer = save.Episodes[x].unlockedMultiplayer = true;
                    if (save.Episodes[x].multiplayerTime == 0)
                        save.Episodes[x].multiplayerTime = 500;
                }
            }
            comboEpisode_SelectedIndexChanged(comboEpisode, null);
            Functions.UI.messageBox("All levels completed!", "Completed", MessageBoxIcon.Information);
        }

        public override void Save()
        {
            writeAllTitleSettings(save.ToArray(), new uint[3] { 1000, 1000, 1000 });
        }
    }
}
