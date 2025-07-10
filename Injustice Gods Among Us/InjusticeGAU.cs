using System;
using DCComics;

namespace Horizon.PackageEditors.Injustice_Gods_Among_Us
{
    public partial class InjusticeGAU : EditorControl
    {
        private InjusticeGAUSave _gameSave;

        public InjusticeGAU()
        {
            InitializeComponent();
            TitleID = FormID.InjusticeGodsAmongUs;

            btnMaxExp.Tag = intExperience;
            btnMaxAccessCards.Tag = intAccessCards;
            btnMaxArmoryKeys.Tag = intArmoryKeys;
            btnMaxTotalStars.Tag = intTotalStars;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            _gameSave = new InjusticeGAUSave(IO);

            intExperience.Value = _gameSave.Experience;
            intAccessCards.Value = _gameSave.AccessCards;
            intArmoryKeys.Value = _gameSave.ArmoryKeys;
            intTotalStars.Value = _gameSave.TotalStars;

            return true;
        }

        public override void Save()
        {
            _gameSave.Experience = intExperience.Value;
            _gameSave.AccessCards = intAccessCards.Value;
            _gameSave.ArmoryKeys = intArmoryKeys.Value;
            _gameSave.TotalStars = intTotalStars.Value;

            _gameSave.Save();
        }

        private void BtnClickMaxValue(object sender, EventArgs e)
        {
            if (sender == null) return;
            var input = (sender as DevComponents.DotNetBar.ButtonX).Tag as DevComponents.Editors.IntegerInput;
            if (input == null) return;
            input.Value = input.MaxValue;
        }

        private void BtnClickUnlockAll(object sender, EventArgs e)
        {
            _gameSave.UnlockAll();
            Functions.UI.messageBox(
                "Unlocked all Backgrounds, Icons, Portaits, Costumes, Battles, S.T.A.R Labs missions, and items in the Archives!");
        }
    }
}
