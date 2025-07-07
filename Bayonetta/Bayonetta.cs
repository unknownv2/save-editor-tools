using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Bayonetta;

namespace Horizon.PackageEditors.Bayonetta
{
    public partial class Bayonetta : EditorControl
    {
        //public static readonly string FID = "53450813";
        public Bayonetta()
        {
            InitializeComponent();
            TitleID = FormID.Bayonetta;
            
        }

        private Save save;
        private int maxHealth = 0x7FFFFFFF;
        public override bool Entry()
        {
            if (!this.OpenStfsFile("data00"))
                return false;

            save = new Save();
            save.LoadSave(IO);

            integerInput1.Value = save.Halos;

            buttonX2.Checked = false;
            buttonX2.Enabled = save.MaxHealth != maxHealth || save.Health != maxHealth;
            return true;
        }

        public override void Save()
        {
            if (buttonX2.Checked)
                save.Health = save.MaxHealth = maxHealth;

            save.Halos = integerInput1.Value;
            save.WriteSave(IO);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            integerInput1.Value = integerInput1.MaxValue;
        }
    }
}