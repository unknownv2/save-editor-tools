using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Capcom;
namespace Horizon.PackageEditors.Devil_May_Cry_3
{
    public partial class DMC3 : EditorControl
    {
        //public static readonly string FID = "4343081E";

        private DMC3Save GameSave;
        private DMC3Save.SaveSlot CurrentSlot;

        private int MissionDifficulty = -1;

        public DMC3()
        {
            InitializeComponent();
            TitleID = FormID.DMC3;

            LoadDisplayComponents();

        }

        private void LoadDisplayComponents()
        {
            this.cmbMissDifficulty.Items.AddRange(new string[] {
                "Easy",
                "Normal",
                "Hard",
                "Very Hard",
                "Dante Must Die",
                "Heaven or Hell"
            });

            string[] Ranks = new string[] {
                "D Ranking",
                "C Ranking",
                "B Ranking",
                "A Ranking",
                "S Ranking",
                "SS Ranking",
                "None"
            };

            this.cmbM1.Items.AddRange(Ranks);
            this.cmbM2.Items.AddRange(Ranks);
            this.cmbM3.Items.AddRange(Ranks);
            this.cmbM4.Items.AddRange(Ranks);
            this.cmbM5.Items.AddRange(Ranks);
            this.cmbM6.Items.AddRange(Ranks);
            this.cmbM7.Items.AddRange(Ranks);
            this.cmbM8.Items.AddRange(Ranks);
            this.cmbM9.Items.AddRange(Ranks);
            this.cmbM10.Items.AddRange(Ranks);
            this.cmbM11.Items.AddRange(Ranks);
            this.cmbM12.Items.AddRange(Ranks);
            this.cmbM13.Items.AddRange(Ranks);
            this.cmbM14.Items.AddRange(Ranks);
            this.cmbM15.Items.AddRange(Ranks);
            this.cmbM16.Items.AddRange(Ranks);
            this.cmbM17.Items.AddRange(Ranks);
            this.cmbM18.Items.AddRange(Ranks);
            this.cmbM19.Items.AddRange(Ranks);
            this.cmbM20.Items.AddRange(Ranks);
        }

        public override bool Entry()
        {
            if(!this.OpenStfsFile("save.bin"))
                return false;

            this.GameSave = new DMC3Save(this.IO);
            this.DisplayStats();

            return true;
        }

        public override void Save()
        {
            this.SetStats();

            this.GameSave.Character_Vergil_IsUnlocked = this.chkBxUnlockVergil.Checked;

           this.GameSave.Save();
        }

        private void SetStats()
        {
            if (this.CurrentSlot != null)
            {
                this.CurrentSlot.RedOrbs = this.intRedOrbs.Value;
                this.CurrentSlot.GoldOrb = (byte)(this.intGoldOrbs.Value & 0xFF);
                this.CurrentSlot.BlueOrb = (byte)(this.intBlueOrbs.Value & 0xFF);

                this.CurrentSlot.VitalStarL = (byte)(this.intVitalL.Value & 0xFF);
                this.CurrentSlot.VitalStarS = (byte)(this.intVitalS.Value & 0xFF);
                this.CurrentSlot.HolyWater = (byte)(this.intHolyWater.Value & 0xFF);

                this.CurrentSlot.HighestMissionAttained = this.intHighestMission.Value;
                this.CurrentSlot.PreviousMission = (this.CurrentSlot.CurrentMission = this.intCurrentMission.Value) - 1;
            }
        }

        private void DisplayStats()
        {
            this.chkBxUnlockVergil.Checked = this.GameSave.Character_Vergil_IsUnlocked;

            this.ReloadSaveSlots(this.cmbCurrentMissSlot, false);
            this.ReloadSaveSlots(this.cmbCurrentSlot, true);
        }

        private void LoadMissionRanks(int DifficultyIndex)
        {
            var MissionRanks = this.CurrentSlot.MissionRanking[DifficultyIndex];

            this.cmbM1.SelectedIndex = MissionRanks[0];
            this.cmbM2.SelectedIndex = MissionRanks[1];
            this.cmbM3.SelectedIndex = MissionRanks[2];
            this.cmbM4.SelectedIndex = MissionRanks[3];
            this.cmbM5.SelectedIndex = MissionRanks[4];
            this.cmbM6.SelectedIndex = MissionRanks[5];
            this.cmbM7.SelectedIndex = MissionRanks[6];
            this.cmbM8.SelectedIndex = MissionRanks[7];
            this.cmbM9.SelectedIndex = MissionRanks[8];
            this.cmbM10.SelectedIndex = MissionRanks[9];
            this.cmbM11.SelectedIndex = MissionRanks[10];
            this.cmbM12.SelectedIndex = MissionRanks[11];
            this.cmbM13.SelectedIndex = MissionRanks[12];
            this.cmbM14.SelectedIndex = MissionRanks[13];
            this.cmbM15.SelectedIndex = MissionRanks[14];
            this.cmbM16.SelectedIndex = MissionRanks[15];
            this.cmbM17.SelectedIndex = MissionRanks[16];
            this.cmbM18.SelectedIndex = MissionRanks[17];
            this.cmbM19.SelectedIndex = MissionRanks[18];
            this.cmbM20.SelectedIndex = MissionRanks[19];
        }

        private void SetMissionRanks(int DifficultyIndex)
        {
            var MissionRanks = this.CurrentSlot.MissionRanking[DifficultyIndex];

            MissionRanks[0] = this.cmbM1.SelectedIndex;
            MissionRanks[1] = this.cmbM2.SelectedIndex;
            MissionRanks[2] = this.cmbM3.SelectedIndex;
            MissionRanks[3] = this.cmbM4.SelectedIndex;
            MissionRanks[4] = this.cmbM5.SelectedIndex;
            MissionRanks[5] = this.cmbM6.SelectedIndex;
            MissionRanks[6] = this.cmbM7.SelectedIndex;
            MissionRanks[7] = this.cmbM8.SelectedIndex;
            MissionRanks[8] = this.cmbM9.SelectedIndex;
            MissionRanks[9] = this.cmbM10.SelectedIndex;
            MissionRanks[10] = this.cmbM11.SelectedIndex;
            MissionRanks[11] = this.cmbM12.SelectedIndex;
            MissionRanks[12] = this.cmbM13.SelectedIndex;
            MissionRanks[13] = this.cmbM14.SelectedIndex;
            MissionRanks[14] = this.cmbM15.SelectedIndex;
            MissionRanks[15] = this.cmbM16.SelectedIndex;
            MissionRanks[16] = this.cmbM17.SelectedIndex;
            MissionRanks[17] = this.cmbM18.SelectedIndex;
            MissionRanks[18] = this.cmbM19.SelectedIndex;
            MissionRanks[19] = this.cmbM20.SelectedIndex;
        }

        private void SlotChangedCallback(int SlotIndex)
        {
            this.SetStats();

            this.CurrentSlot = this.GameSave.SaveSlots[SlotIndex];

            this.intRedOrbs.Value = this.CurrentSlot.RedOrbs;
            this.intGoldOrbs.Value = this.CurrentSlot.GoldOrb;
            this.intBlueOrbs.Value = this.CurrentSlot.BlueOrb;
            this.intVitalS.Value = this.CurrentSlot.VitalStarS;
            this.intVitalL.Value = this.CurrentSlot.VitalStarL;
            this.intHolyWater.Value = this.CurrentSlot.HolyWater;

            this.intCurrentMission.Value = this.CurrentSlot.CurrentMission;
            this.intHighestMission.Value = this.CurrentSlot.HighestMissionAttained;

            if (this.CurrentSlot != null)
            {
                this.LoadMissionRanks(0);
            }
        }

        private void ReloadSaveSlots(DevComponents.DotNetBar.Controls.ComboBoxEx SlotBox, bool ResetIndex)
        {
            SlotBox.Items.Clear();

            for (int x = 0; x < this.GameSave.SaveSlots.Count; x++)
                if(!GameSave.SaveSlots[x].IsEmpty)
                    SlotBox.Items.Add(string.Format("{0}", GameSave.SaveSlots[x].SlotNumber));

            if(ResetIndex)
                SlotBox.SelectedIndex = 0;
        }

        private void btnClick_ClearSlotData(object sender, EventArgs e)
        {
            if (this.CurrentSlot != null && !this.CurrentSlot.IsEmpty)
            {
                int activeSlotCount = 0;
                for (int x = 0; x < this.GameSave.SaveSlots.Count; x++)
                    if (!this.GameSave.SaveSlots[x].IsEmpty)
                        activeSlotCount++;

                bool CanDelete = activeSlotCount > 1 ? true : false;
                if (CanDelete)
                {
                    this.CurrentSlot.IsEmpty = true;
                    this.GameSave.DeleteSaveSlot(this.CurrentSlot.SlotIndex);
                }
                else
                {
                    Horizon.Functions.UI.messageBox("You cannot delete the only save slot.");
                }
            }
            else
            {
                Horizon.Functions.UI.messageBox("Cannot access the current slot.");
            }
        }

        private void btnClick_CopySlotData(object sender, EventArgs e)
        {
            int newSlotIndex = this.intNewSlot.Value - 1;

            if (!this.GameSave.SaveSlots[newSlotIndex].IsEmpty)
            {
                if (MessageBox.Show("You are attempting to overwrite a save slot that is not empty. Continue?", "Yes", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            if (this.CurrentSlot != null && !this.CurrentSlot.IsEmpty)
            {
                int oldSlot = this.CurrentSlot.SlotNumber;
                int newSlot = this.GameSave.SaveSlots[newSlotIndex].SlotNumber;

                if (oldSlot != newSlot)
                {
                    this.GameSave.CopySaveSlot(oldSlot - 1, newSlot - 1);
                    this.GameSave.SaveSlots[newSlotIndex].CopySlot(this.CurrentSlot);

                    Horizon.Functions.UI.messageBox(string.Format("Successfully copied slot {0} to slot {1}!", oldSlot, newSlot));

                    this.ReloadSaveSlots(this.cmbCurrentSlot, true);
                    this.ReloadSaveSlots(this.cmbCurrentMissSlot, false);
                }
                else
                {
                    Horizon.Functions.UI.messageBox("You cannot copy a slot to the same slot number.");
                }
            }
            else
            {
                Horizon.Functions.UI.messageBox("Cannot access the current slot.");
            }
        }

        private void cmbMissDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrentSlot != null)
            {
                int idx = (sender as DevComponents.DotNetBar.Controls.ComboBoxEx).SelectedIndex;

                if(this.MissionDifficulty != -1)
                    this.SetMissionRanks(this.MissionDifficulty);

                this.LoadMissionRanks(idx);
                this.MissionDifficulty = idx;
            }
        }

        private void cmbCurrentMissSlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbCurrentSlot.SelectedIndex = (sender as DevComponents.DotNetBar.Controls.ComboBoxEx).SelectedIndex;
        }
        
        private void cmbSlotIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = (sender as DevComponents.DotNetBar.Controls.ComboBoxEx).SelectedIndex;

            if (this.cmbMissDifficulty.SelectedIndex != -1)
            {
                this.SetMissionRanks(this.cmbMissDifficulty.SelectedIndex);
            }

            this.cmbMissDifficulty.SelectedIndex = 0;

            this.SlotChangedCallback(idx);

            this.cmbCurrentMissSlot.SelectedIndex = idx;
        }

        private void cmdMaxRedOrbs_Click(object sender, EventArgs e)
        {
            intRedOrbs.Value = intRedOrbs.MaxValue;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            intGoldOrbs.Value = intGoldOrbs.MaxValue;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            intBlueOrbs.Value = intBlueOrbs.MaxValue;
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            intPurpleOrbs.Value = intPurpleOrbs.MaxValue;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            intVitalS.Value = intVitalS.MaxValue;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            intVitalL.Value = intVitalL.MaxValue;
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            intDevilStar.Value = intDevilStar.MaxValue;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            intHolyWater.Value = intHolyWater.MaxValue;
        }
    }
}
