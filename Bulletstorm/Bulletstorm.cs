using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bulletstorm;

namespace Horizon.PackageEditors.Bulletstorm
{
    public partial class Bulletstorm : EditorControl
    {

        //public static readonly string FID = "454108EF";
        public Bulletstorm()
        {
            InitializeComponent();
            TitleID = FormID.Bulletstorm;
            
        }

        private Save save;
        public override bool Entry()
        {
            if (!OpenStfsFile("Checkpoint.sav"))
                return false;
            save = new Save();
            save.LoadSave(IO);
            integerInput1.Value = save.skillPoints;
            return true;
        }

        public override void Save()
        {
            save.skillPoints = integerInput1.Value;
            save.WriteSave(IO);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            integerInput1.Value = integerInput1.MaxValue;
        }
    }
}
