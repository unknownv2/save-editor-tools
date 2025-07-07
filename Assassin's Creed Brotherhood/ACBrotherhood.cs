using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ubisoft;

namespace Horizon.PackageEditors.Assassin_s_Creed_Brotherhood
{
    public partial class ACBrotherhood : EditorControl
    {
        private ACBrotherhoodSaveGame saveGame;

        public ACBrotherhood()
        {
            InitializeComponent();
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            saveGame = new ACBrotherhoodSaveGame(IO);
            intMoney.Value = saveGame.ObjectManager.Money;

            return true;
        }

        public override void Save()
        {
            saveGame.ObjectManager.Money = intMoney.Value;

            saveGame.Save();
        }

        private void BtnClick_MaxMoney(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
        }
    }
}
