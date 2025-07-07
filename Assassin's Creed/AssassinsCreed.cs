using System;
using Ubisoft;

namespace Horizon.PackageEditors.Assassin_s_Creed
{
    public partial class AssassinsCreed : EditorControl
    {
        private ACISaveGame saveGame = null;

        public AssassinsCreed()
        {
            InitializeComponent();
        }
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;


            return true;
        }

        public override void Save()
        {
            
            saveGame.Save();
        }
        private void BtnClick_MaxMoney(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
        }
    }
}
