using System;
using NBA2K;

namespace Horizon.PackageEditors.NBA_2K14
{
    public partial class NBA2K14 : EditorControl
    {
        private NBA2K14Class saveGame;

        public NBA2K14()
        {
            InitializeComponent();
            TitleID = FormID.NBA2K14;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            saveGame = new NBA2K14Class(IO);
            intSkillPoints.Value = (int) saveGame.SkillPoints;
            intFans.Value = (int)saveGame.Fans;

            return true;
        }

        public override void Save()
        {
            saveGame.SkillPoints = (uint) intSkillPoints.Value;
            saveGame.Fans = (uint) intFans.Value;

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
