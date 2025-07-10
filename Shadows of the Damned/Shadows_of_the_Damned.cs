using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ShadowsoftheDamned;

namespace Horizon.PackageEditors.Shadows_of_the_Damned
{
    public partial class Shadows_of_the_Damned : EditorControl
    {
        //public static readonly string FID = "4541092A";
        public Shadows_of_the_Damned()
        {
            InitializeComponent();
            TitleID = FormID.Shadows_of_the_Damned;
            
        }

        Save save;
        public override bool Entry()
        {
            if (!OpenStfsFile("AUTOSAVE0.sav"))
                return false;

            save = new Save();
            save.LoadSave(this.IO);

            integerInput1.Value = save.whiteGems;
            integerInput2.Value = save.redGems;
            integerInput3.Value = save.blueGems;

            numericUpDown1.Value = (decimal)save.playerx;
            numericUpDown2.Value = (decimal)save.playery;
            numericUpDown3.Value = (decimal)save.playerz;

            integerInput4.Value = save.tequilla;
            integerInput5.Value = save.sake;

            return true;
        }

        public override void Save()
        {
            save.whiteGems = integerInput1.Value;
            save.redGems = integerInput2.Value;
            save.blueGems = integerInput3.Value;
            save.playerx = (float)numericUpDown1.Value;
            save.playery = (float)numericUpDown2.Value;
            save.playerz = (float)numericUpDown3.Value;
            save.tequilla = integerInput4.Value;
            save.sake = integerInput5.Value;
            save.WriteSave();
        }

        private void integerInput1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            integerInput1.Value = 999;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            integerInput2.Value = 999;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            integerInput3.Value = 999;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            integerInput4.Value = 999;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            integerInput5.Value = 999;
        }
    }
}