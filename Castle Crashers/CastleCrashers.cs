using System;
using System.Text.RegularExpressions;
using TheBehemoth;

namespace Horizon.PackageEditors.Castle_Crashers
{
    public partial class CastleCrashers : EditorControl
    {
        private CastleCrashersProfile Profile;
        private CastleCrashersCharacter CurrentCharacter;

        public CastleCrashers()
        {
            InitializeComponent();
            TitleID = FormID.CastleCrashers;

            cmbSkull.Items.AddRange(new object[] { "None", "Silver Skull", "Gold Skull" });

            var characters = Enum.GetNames(typeof(CastleCrashersCharacters));

            foreach (var c in characters)
                cmbCharacter.Items.Add(c.Replace('_', ' '));
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(TitleID + ".gpd"))
                return false;

            Profile = new CastleCrashersProfile(IO);

            intGold.Value = Profile.Gold;

            cmbCharacter.SelectedIndex = 0;

            return true;
        }

        public override void Save()
        {
            Profile.Gold = intGold.Value;
            Profile.AllWeaponsUnlocked = btnUnlockAllWpnsAndUpgds.Checked;
            SetCharacter();

            Profile.Save();
        }

        private void LoadCharacter(int characterIndex)
        {
            if(characterIndex == -1)
                return;

            if(CurrentCharacter != null)
                SetCharacter();

            // load the character class
            CurrentCharacter = Profile.Characters[characterIndex];

            intLevel.Value = CurrentCharacter.Level + 1; // zero-based index for the level
            intExperience.Value = CurrentCharacter.Experience;

            intStrength.Value = CurrentCharacter.Strength;
            intDefense.Value = CurrentCharacter.Defense;
            intMagic.Value = CurrentCharacter.Magic;
            intAgility.Value = CurrentCharacter.Agility;
            intPotions.Value = CurrentCharacter.Potions;
            intBombs.Value = CurrentCharacter.Bombs;
            intSandwiches.Value = CurrentCharacter.Sandwiches;
            cmbSkull.SelectedIndex = CurrentCharacter.Skull;

            chkBxNrmlBossUnlocked.Checked = CurrentCharacter.IsBossUnlocked;
            
            chkBxInsaneDiffUnlocked.Checked = CurrentCharacter.InsaneModeEnabled;
            chkBxIsnBossUnlocked.Checked = CurrentCharacter.InsaneModeIsBossUnlocked;

            chkBxCharUnlocked.Checked = CurrentCharacter.IsUnlocked;
        }

        private void SetCharacter()
        {
            if(CurrentCharacter == null)
                return;

            CurrentCharacter.Level = intLevel.Value - 1; // adjustment for the zero-based index
            CurrentCharacter.Experience = intExperience.Value;

            CurrentCharacter.Strength = intStrength.Value;
            CurrentCharacter.Defense = intDefense.Value;
            CurrentCharacter.Magic = intMagic.Value;
            CurrentCharacter.Agility =  intAgility.Value;
            CurrentCharacter.Potions = intPotions.Value;
            CurrentCharacter.Bombs = intBombs.Value;
            CurrentCharacter.Sandwiches = intSandwiches.Value;
            CurrentCharacter.Skull = cmbSkull.SelectedIndex;

            CurrentCharacter.InsaneModeEnabled = chkBxInsaneDiffUnlocked.Checked;
            CurrentCharacter.Collectibles = chkBxAllCollectibles.Checked ? 0xFE : CurrentCharacter.Collectibles;
            CurrentCharacter.IsUnlocked = chkBxCharUnlocked.Checked;

            CurrentCharacter.IsBossUnlocked = chkBxNrmlBossUnlocked.Checked;
            CurrentCharacter.LevelProgress1 = chkBxNrmlLeftOfDock.Checked ? 0xFF : CurrentCharacter.LevelProgress1;
            CurrentCharacter.LevelProgress2 = chkBxNrmlRightOfDock.Checked ? 0xFF : CurrentCharacter.LevelProgress2;


            CurrentCharacter.InsaneModeEnabled = chkBxInsaneDiffUnlocked.Checked;
            CurrentCharacter.InsaneModeIsBossUnlocked = chkBxIsnBossUnlocked.Checked;
            CurrentCharacter.InsaneModeLevelProgress1 = chkBxIsnLeftOfDock.Checked
                                                            ? 0xFF
                                                            : CurrentCharacter.InsaneModeLevelProgress1;
            CurrentCharacter.InsaneModeLevelProgress2 = chkBxIsnRightOfDock.Checked
                                                            ? 0xFF
                                                            : CurrentCharacter.InsaneModeLevelProgress2;

        }

        private void CmbCharacterChanged(object sender, EventArgs e)
        {
            LoadCharacter(cmbCharacter.SelectedIndex);
        }

        private void BtnClickMaxStrength(object sender, EventArgs e)
        {
            intStrength.Value = intStrength.MaxValue;
        }

        private void BtnClickMaxMagic(object sender, EventArgs e)
        {
            intMagic.Value = intMagic.MaxValue;
        }

        private void BtnClickMaxDefense(object sender, EventArgs e)
        {
            intDefense.Value = intDefense.MaxValue;
        }

        private void BtnClickMaxAgility(object sender, EventArgs e)
        {
            intAgility.Value = intAgility.MaxValue;
        }
        
        private void BtnClickMaxPotions(object sender, EventArgs e)
        {
            intPotions.Value = intPotions.MaxValue;
        }

        private void BtnClickMaxBombs(object sender, EventArgs e)
        {
            intBombs.Value = intBombs.MaxValue;
        }

        private void BtnClickMaxSandwiches(object sender, EventArgs e)
        {
            intSandwiches.Value = intSandwiches.MaxValue;
        }

        private void BtnClickMaxLevel(object sender, EventArgs e)
        {
            intLevel.Value = intLevel.MaxValue;
        }

        private void BtnClickMaxExperience(object sender, EventArgs e)
        {
            intExperience.Value = intExperience.MaxValue;
        }

        private void BtnClickMaxGold(object sender, EventArgs e)
        {
            intGold.Value = intGold.MaxValue;
        }
    }
}
