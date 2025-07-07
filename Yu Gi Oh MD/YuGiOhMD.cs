using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YuGiOhMilleniumDuels;

namespace Horizon.PackageEditors.Yu_Gi_Oh_MD
{
    public partial class YuGiOhMD : EditorControl
    {
        private SaveGame saveGame;

        public YuGiOhMD()
        {
            InitializeComponent();
            TitleID = FormID.YuGiOhMD;
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
