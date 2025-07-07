using System;
using TigerWoods;

namespace Horizon.PackageEditors.TW_PGA_Tour_14
{
    public partial class TWPGATour14 : EditorControl
    {
        private PGATour14Save _saveGame;

        public TWPGATour14()
        {
            InitializeComponent();
            TitleID = FormID.TigerWoodsPGATour14;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            _saveGame = new PGATour14Save(IO);

            txtGolfer.Text = _saveGame.Golfer;
            txtProfile.Text = _saveGame.Profile;

            intExperience.Value = _saveGame.Experience;
            intAttributePoints.Value = _saveGame.AttributePoints;
            intPower.Value = _saveGame.Power;
            intAccuracy.Value = _saveGame.Accuracy;
            intWorkability.Value = _saveGame.Workability;
            intSpin.Value = _saveGame.Spin;
            intRecovery.Value = _saveGame.Recovery;
            intPutting.Value = _saveGame.Putting;


            return true;
        }

        public override void Save()
        {
            _saveGame.Golfer = txtGolfer.Text;
            _saveGame.Profile = txtProfile.Text;

            _saveGame.Experience = intExperience.Value;
            _saveGame.AttributePoints = intAttributePoints.Value;
            _saveGame.Power = intPower.Value;
            _saveGame.Accuracy = intAccuracy.Value;
            _saveGame.Workability = intWorkability.Value;
            _saveGame.Spin = intSpin.Value;
            _saveGame.Recovery = intRecovery.Value;
            _saveGame.Putting = intPutting.Value;

            _saveGame.Save();
        }

        private void BtnClickMaxExperience(object sender, EventArgs e)
        {
            intExperience.Value = intExperience.MaxValue;
        }

        private void BtnClickMaxAttributePts(object sender, EventArgs e)
        {
            intAttributePoints.Value = intAttributePoints.MaxValue;
        }

        private void BtnClickMaxPower(object sender, EventArgs e)
        {
            intPower.Value = intPower.MaxValue;
        }

        private void BtnClickMaxAccuracy(object sender, EventArgs e)
        {
            intAccuracy.Value = intAccuracy.MaxValue;
        }

        private void BtnClickMaxWorkability(object sender, EventArgs e)
        {
            intWorkability.Value = intWorkability.MaxValue;
        }

        private void BtnClickMaxSpin(object sender, EventArgs e)
        {
            intSpin.Value = intSpin.MaxValue;
        }

        private void BtnClickMaxRecovery(object sender, EventArgs e)
        {
            intRecovery.Value = intRecovery.MaxValue;
        }

        private void BtnClickMaxPutting(object sender, EventArgs e)
        {
            intPutting.Value = intPutting.MaxValue;
        }
    }
}
