using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Ubisoft;

namespace Horizon.PackageEditors.Assassin_s_Creed_Revelations
{
    public partial class ACRevelations : EditorControl
    {
        private ACRevelationsSaveGame GameSave;
        public ACRevelations()
        {
            InitializeComponent();
            TitleID = FormID.ACRevelations;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            GameSave = new ACRevelationsSaveGame(IO);

            intMoney.Value = GameSave.ObjectManager.Money;

            return true;
        }

        public override void Save()
        {
            GameSave.ObjectManager.Money = intMoney.Value;

            GameSave.Save();
        }

        private void BtnClick_MaxMoney(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
        }
    }
}
