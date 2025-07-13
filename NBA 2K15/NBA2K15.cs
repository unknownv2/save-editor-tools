using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBA2K;

namespace Horizon.PackageEditors.NBA_2K15
{
    public partial class NBA2K15 : EditorControl
    {
        private NBA2K15Class saveGame;

        public NBA2K15()
        {
            InitializeComponent();
            TitleID = FormID.NBA2K15;
        }
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            saveGame = new NBA2K15Class(IO);
            intSkillPoints.Value = (int)saveGame.SkillPoints;
            intFans.Value = (int)saveGame.Fans;

            return true;
        }

        public override void Save()
        {
            saveGame.SkillPoints = (uint)intSkillPoints.Value;
            saveGame.Fans = (uint)intFans.Value;

            saveGame.Save();
        }
        private void BtnClickMaxSP(object sender, EventArgs e)
        {
            intSkillPoints.Value = intSkillPoints.MaxValue;
        }

        private void BtnClickMaxFans(object sender, EventArgs e)
        {
            intFans.Value = intFans.MaxValue;
        }
    }
}
