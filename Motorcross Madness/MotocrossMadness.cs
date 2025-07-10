using System;
using XBLA;

namespace Horizon.PackageEditors.Motorcross_Madness
{
    public partial class MotocrossMadness : EditorControl
    {
        private MotorcrossMadnessSave _gameSave;
        public MotocrossMadness()
        {
            InitializeComponent();
            TitleID = FormID.MotocrossMadness;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            _gameSave = new MotorcrossMadnessSave(IO);

            intExperience.Value = _gameSave.Experience;
            intSkillLevel.Value = _gameSave.SkillLevel;
            intCash.Value = _gameSave.Cash;

            return true;
        }

        public override void Save()
        {
            _gameSave.Experience = intExperience.Value;
            _gameSave.SkillLevel = intSkillLevel.Value;
            _gameSave.Cash = intCash.Value;

            _gameSave.Save();
        }

        private void BtnClickMaxExp(object sender, EventArgs e)
        {
            intExperience.Value = intExperience.MaxValue;
        }

        private void BtnClickSkillLevel(object sender, EventArgs e)
        {
            intSkillLevel.Value = intSkillLevel.MaxValue;
        }

        private void BtnClickCash(object sender, EventArgs e)
        {
            intCash.Value = intCash.MaxValue;
        }
    }
}
