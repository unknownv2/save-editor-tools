using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SSX;

namespace Horizon.PackageEditors.SSX_Tricky
{
    public partial class SSXTricky : EditorControl
    {
        private SSXGameSave GameSave;
        //public static readonly string FID = "4541096D";

        public SSXTricky()
        {
            InitializeComponent();
            TitleID = FormID.SSXTricky;
            
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
                return false;

            GameSave = new SSXGameSave(this.IO);

            DisplayStats();

            return true;
        }

        public override void Save()
        {
            this.GameSave.Credits = this.intCredits.Value;

            this.GameSave.Save();
        }

        private void DisplayStats()
        {
            this.intCredits.Value = this.GameSave.Credits;
        }

        private void BtnClick_SetMaxCredits(object sender, EventArgs e)
        {
            this.intCredits.Value = intCredits.MaxValue;
        }
    }
}
