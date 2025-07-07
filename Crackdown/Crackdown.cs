using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crackdown;

namespace Horizon.PackageEditors.Crackdown
{
    public partial class Crackdown : EditorControl
    {
        //public static readonly string FID = "4D5307DC";
        public Crackdown()
        {
            InitializeComponent();
            TitleID = FormID.Crackdown;
            
        }

        CrackdownClass XSave;
        public override bool Entry()
        {
            if (!OpenStfsFile("Crackdown.sav"))
                return false;
            XSave = new CrackdownClass();
            XSave.LoadSave(IO);
            intHidden.Value = XSave.HiddenOrbs;
            intAgility.Value = XSave.AgilityOrbs;
            return true;
        }

        public override void Save()
        {
            XSave.WriteStats(intAgility.Value, intHidden.Value);
            if (radioButton1.Checked)
                XSave.WriteOrbFlags(0);
            if (radioButton2.Checked)
                XSave.WriteOrbFlags(1);
            XSave.WriteSave();
        }

        private void cmdOrbMax_Click(object sender, EventArgs e)
        {
            intHidden.Value = intHidden.MaxValue;
            intAgility.Value = intAgility.MaxValue;
        }
    }
}
