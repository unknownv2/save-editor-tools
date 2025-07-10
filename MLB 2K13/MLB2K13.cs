using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MLB;

namespace Horizon.PackageEditors.MLB_2K13
{
    public partial class MLB2K13 : EditorControl
    {
        //public static readonly string FID = "545408AB";
        private MLB2K13Save SaveGame;

        public MLB2K13()
        {
            InitializeComponent();
            TitleID = FormID.MLB2K13;
            
        }
                
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            SaveGame = new MLB2K13Save(IO);

            intBattingPoints.Value = SaveGame.BattingSP;
            intFieldingPoints.Value = SaveGame.FieldingSP;
            intBaserunningPoints.Value = SaveGame.BaserunningSP;
            intPitchingPoints.Value = SaveGame.PitchingSP;

            return true;
        }

        public override void Save()
        {
            SaveGame.BattingSP = intBattingPoints.Value;
            SaveGame.FieldingSP = intFieldingPoints.Value;
            SaveGame.BaserunningSP = intBaserunningPoints.Value;
            SaveGame.PitchingSP = intPitchingPoints.Value;

            SaveGame.Save();
        }

        private void BtnClickMaxBattingSP(object sender, EventArgs e)
        {
            intBattingPoints.Value = intBattingPoints.MaxValue;
        }
        private void BtnClickMaxFieldingSP(object sender, EventArgs e)
        {
            intFieldingPoints.Value = intFieldingPoints.MaxValue;
        }
        private void BtnClickMaxBaserunningSP(object sender, EventArgs e)
        {
            intBaserunningPoints.Value = intBaserunningPoints.MaxValue;
        }
        private void BtnClickMaxPitchingSP(object sender, EventArgs e)
        {
            intPitchingPoints.Value = intPitchingPoints.MaxValue;
        }
    }
}
