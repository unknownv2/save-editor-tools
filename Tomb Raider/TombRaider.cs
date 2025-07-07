using System;
using System.Windows.Forms;
using TombRaider;

namespace Horizon.PackageEditors.Tomb_Raider
{
    public partial class TombRaider : EditorControl
    {
        //public static readonly string FID = "53510802";

        private TombRaiderSave SaveGame;
        public TombRaider()
        {
            InitializeComponent();
            TitleID = FormID.TombRaider;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            SaveGame = new TombRaiderSave(IO);

            intSalvage.Value = InitItem(0x8720EBCE);
            intSkillPoints.Value = SaveGame.SkillPoints;

            intArrows.Value = InitItem(0x8863BA99);
            intHandgun.Value = InitItem(0xB62B6E6C);
            intRifle.Value = InitItem(0x5C522579);
            intShotgun.Value = InitItem(0xA230E397);

            return true;
        }

        public override void Save()
        {
            SaveGame.PlayerItems[0x8720EBCE] = intSalvage.Value;
            SaveGame.PlayerItems[0x8863BA99] = intArrows.Value;
            SaveGame.PlayerItems[0xB62B6E6C] = intHandgun.Value;
            SaveGame.PlayerItems[0x5C522579] = intRifle.Value;
            SaveGame.PlayerItems[0xA230E397] = intShotgun.Value;

            SaveGame.SkillPoints = intSkillPoints.Value;
            SaveGame.Save();
        }

        private int InitItem(uint id)
        {
            if (SaveGame == null || SaveGame.PlayerItems == null)
                throw new Exception("Tomb Raider: SaveGame was not initialized properly!");
			
			if(SaveGame.PlayerItems.ContainsKey(id))
				return SaveGame.PlayerItems[id];
			
			SaveGame.PlayerItems.Add(id, 0);
			return 0;
		}

        private void BtnClickMaxSalvage(object sender, EventArgs e)
        {
            intSalvage.Value = intSalvage.MaxValue;
        }

        private void BtnClickMaxSkillPoints(object sender, EventArgs e)
        {
            intSkillPoints.Value = intSkillPoints.MaxValue;
        }

        private void BtnClickMaxArrows(object sender, EventArgs e)
        {
            intArrows.Value = intArrows.MaxValue;
        }
        private void BtnClickMaxHandgun(object sender, EventArgs e)
        {
            intHandgun.Value = intHandgun.MaxValue;
        }

        private void BtnClickMaxRifle(object sender, EventArgs e)
        {
            intRifle.Value = intRifle.MaxValue;
        }
        private void BtnClickMaxShotgun(object sender, EventArgs e)
        {
            intShotgun.Value = intShotgun.MaxValue;
        }
    }
}