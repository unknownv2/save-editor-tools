using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YuGiOhDecadeDuels;

namespace Horizon.PackageEditors.Yu_Gi_Oh_5Ds
{
    public partial class YuGiOh5Ds : EditorControl
    {
        private SaveGame saveGame;

        public YuGiOh5Ds()
        {
            InitializeComponent();
            TitleID = FormID.YuGiOh5Ds;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("savegame.dat"))
                return false;

            saveGame = new SaveGame(IO);

            return saveGame.Read();
        }

        public override void Save()
        {
            if (btnUnlockAll.Checked)
            {
                foreach (var card in saveGame.UnlockedCards)
                {
                    card.UnlockAll();
                }
            }

            saveGame.Save();
        }
    }
}
