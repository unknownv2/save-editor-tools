using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Horizon.PackageEditors.Joe_Danger_SE
{
    public partial class JoeDangerSE : EditorControl
    {
        //public static readonly string FID = "584111F5";
        public JoeDangerSE()
        {
            InitializeComponent();
            TitleID = FormID.JoeDangerSE;
        }

        private Save save;
        public override bool Entry()
        {
            if (!OpenStfsFile("Profile.dat"))
                return false;

            this.save = new Save(this.IO);
            this.save.LoadSave();

            for (int i = 0; i < 0x50; i++)
                listBox1.Items.Add("Level " + i + " Score: " + save.levels[i].score);

            this.listBox2.Enabled = false;

            return true;
        }

        public override void Save()
        {
            this.save.WriteSave();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEx1.SelectedIndex = save.levels[listBox1.SelectedIndex].medal;

            listBox2.Items.Clear();
            listBox2.Enabled = true;

            foreach (Save.Score score in save.levels[listBox1.SelectedIndex].highScores)
                listBox2.Items.Add(score.name);

            listBox2.SelectedItem = 0;

            integerInput2.Value = (int)save.levels[listBox1.SelectedIndex].score;

            if (save.levels[listBox1.SelectedIndex].flags == 0)
                radioButton1.Checked = true;
            else if (save.levels[listBox1.SelectedIndex].flags == 1)
                radioButton2.Checked = true;
            else if (save.levels[listBox1.SelectedIndex].flags == 3)
                radioButton3.Checked = true;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            integerInput1.Value = (int)save.levels[listBox1.SelectedIndex].highScores[listBox2.SelectedIndex].score;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                save.levels[listBox1.SelectedIndex].flags = 0;
            else if (radioButton2.Checked == true)
                save.levels[listBox1.SelectedIndex].flags = 1;
            else if (radioButton3.Checked == true)
                save.levels[listBox1.SelectedIndex].flags = 3;

            save.levels[listBox1.SelectedIndex].score = (long)integerInput2.Value;
            save.levels[listBox1.SelectedIndex].medal = comboBoxEx1.SelectedIndex;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 0x50; i++)
                save.levels[i].flags = 3;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxX1.Checked = save.tours.Contains(listBox3.SelectedItem.ToString());
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
            {
                if (!save.tours.Contains(listBox3.SelectedItem.ToString()))
                    save.tours.Add(listBox3.SelectedItem.ToString());
            }
            else
                save.tours.Remove(listBox3.SelectedItem.ToString());
        }
    }
}