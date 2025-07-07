using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DragonsDogma;

namespace Horizon.PackageEditors.Dragons_Dogma
{
    public partial class DragonsDogma : EditorControl
    {
        //public static readonly string FID = "43430814";

        private DD_GameSave GameSave;

        public DragonsDogma()
        {
            InitializeComponent();
            TitleID = FormID.DragonsDogma;
            
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("DD_Savedata"))
                return false;

            this.GameSave = new DD_GameSave(this.IO);

            // display decompressed data
            this.ddSaveText.Text = Horizon.Functions.Global.arrayToString(this.GameSave.GetSaveData());

            return true;
        }

        public override void Save()
        {
            this.GameSave.Save(System.Text.ASCIIEncoding.ASCII.GetBytes(this.ddSaveText.Text));
        }
    }
}
