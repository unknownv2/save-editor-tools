using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ResidentEvil;

namespace Horizon.PackageEditors.Resident_Evil_ORC
{
    public partial class ResidentEvil_ORC : EditorControl
    {
        //public static readonly string FID = "4343081D";

        private OCRSaveGame SaveGame;

        public ResidentEvil_ORC()
        {
            InitializeComponent();
            TitleID = FormID.ResidentEvil_ORC;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("rerc.dat"))
                return false;

            SaveGame = new OCRSaveGame(this.IO);

            this.DisplayStats();

            return true;
        }

        public override void Save()
        {
            this.SaveGame.Experience.Value = this.intXp.Value;
            
            SaveGame.Save();
        }

        private void DisplayStats()
        {
            this.intXp.Value = this.SaveGame.Experience.Value;
        }

        private void BtnClick_SetMaxXP(object sender, EventArgs e)
        {
            intXp.Value = intXp.MaxValue;
        }
    }
}