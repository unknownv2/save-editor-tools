using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevilMayCry4;

namespace Horizon.PackageEditors.Devil_May_Cry_4
{
    public partial class DevilMayCry4 : EditorControl
    {
        //public static readonly string FID = "434307DF";
        private Save save;

        public DevilMayCry4()
        {
            InitializeComponent();
            TitleID = FormID.DevilMayCry4;
            
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("savegame.txt"))
                return false;

            save = new Save();
            save.LoadSave(IO);

            comboBoxEx1.Items.Clear();

            foreach (Save.Slot slot in save.SaveSlots)
            {
                if (slot.Level == 0)
                    break;
                comboBoxEx1.Items.Add("Level " + slot.Level.ToString());
            }

            comboBoxEx1.SelectedIndex = 0;

            return true;
        }

        public override void Save()
        {
            save.WriteSave(IO);
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            integerInput1.Value = save.SaveSlots[comboBoxEx1.SelectedIndex].RedOrbs;
            integerInput2.Value = save.SaveSlots[comboBoxEx1.SelectedIndex].Orbs;
            integerInput3.Value = save.SaveSlots[comboBoxEx1.SelectedIndex].Score;
        }

        private void integerInput1_ValueChanged(object sender, EventArgs e)
        {
            save.SaveSlots[comboBoxEx1.SelectedIndex].RedOrbs = integerInput1.Value;
        }

        private void integerInput2_ValueChanged(object sender, EventArgs e)
        {
            save.SaveSlots[comboBoxEx1.SelectedIndex].Orbs = integerInput2.Value;
        }

        private void integerInput3_ValueChanged(object sender, EventArgs e)
        {
            save.SaveSlots[comboBoxEx1.SelectedIndex].Score = integerInput3.Value;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            integerInput1.Value = integerInput1.MaxValue;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            integerInput2.Value = integerInput2.MaxValue;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            integerInput3.Value = integerInput3.MaxValue;
        }
    }
}