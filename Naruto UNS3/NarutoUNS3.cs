using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NarutoUNS3;

namespace Horizon.PackageEditors.Naruto_Ultimate_Ninja_Storm_3
{
    public partial class NarutoUNS3 : EditorControl
    {
        //public static readonly string FID = "4E4D085C";
        private NarutoUNS3Save GameSave;

        public NarutoUNS3()
        {
            InitializeComponent();
            TitleID = FormID.NarutoUNS3;
            

            btnUnlockNinjaInfoCards.Tag = CollectionItemType.NinjaInfoCard;
            btnUnlockTitles.Tag = CollectionItemType.Title;
            btnUnlockSubItems.Tag = CollectionItemType.SubstitutionItem;
            btnUnlockAudio.Tag = CollectionItemType.Audio;
            btnUnlockMusic.Tag = CollectionItemType.Music;
            btnUnlockUltimateJutsu.Tag = CollectionItemType.UltimateJutsu;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            // max for ryo is 0x98967F (9999999)
            GameSave = new NarutoUNS3Save(IO);

            intRyo.Value = GameSave.Ryo;
            intHeroPoints.Value = GameSave.HeroPoints;
            intLegendPoints.Value = GameSave.LegendPoints;

            return true;
        }

        public override void Save()
        {
            GameSave.Ryo = GameSave.AcquiredRyo = intRyo.Value;
            GameSave.HeroPoints = intHeroPoints.Value;
            GameSave.LegendPoints = intLegendPoints.Value;

            GameSave.Save();
        }

        private void BtnClickMaxRyo(object sender, EventArgs e)
        {
            intRyo.Value = intRyo.MaxValue;
        }

        private void BtnClickMaxHeroPoints(object sender, EventArgs e)
        {
            intHeroPoints.Value = intHeroPoints.MaxValue;
        }

        private void BtnClickMaxLengedPoints(object sender, EventArgs e)
        {
            intLegendPoints.Value = intLegendPoints.MaxValue;
        }

        private void BtnUnlockAll(object sender, EventArgs e)
        {
            if ((sender as DevComponents.DotNetBar.ButtonX).Tag != null)
                GameSave.CollectionManager.UnlockAll((CollectionItemType)((sender as DevComponents.DotNetBar.ButtonX).Tag));
        }
    }
}