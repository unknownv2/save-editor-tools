using System;
using System.IO;
using System.Windows.Forms;
using TheBehemoth;

namespace Horizon.PackageEditors.BattleBlock_Theater
{
    public partial class BattleBlockTheater : EditorControl
    {
        private BattleBlockTheaterProfile Profile;

        public BattleBlockTheater()
        {
            InitializeComponent();
            TitleID = FormID.BattleBlockTheater;

#if INT2
            btnExtractProfileData.Visible = true;
#endif
        }

        public override bool Entry()
        {
            if (!loadAllTitleSettings(EndianType.BigEndian))
                return false;

            BattleBlockTheaterProfile.KeyRounds = SettingAsInt(164);

            Profile = new BattleBlockTheaterProfile(IO, Package.Header.Metadata.Creator);

            intGems.Value = Profile.Gems;
            intYarn.Value = Profile.Yarn;

            return true;
        }

        public override void Save()
        {
            // set modified values
            Profile.Gems = intGems.Value;
            Profile.Yarn = intYarn.Value;

            // save the profile data and write it back as a title setting
            writeAllTitleSettings(Profile.Save());
        }

        private void BtnClickMaxGems(object sender, EventArgs e)
        {
            intGems.Value = intGems.MaxValue;
        }

        private void BtnClickMaxYarn(object sender, EventArgs e)
        {
            intYarn.Value = intYarn.MaxValue;
        }

        private void BtnClickExtractProfileData(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            Profile.ExportBuffer().Save(sfd.FileName);
        }
    }
}
