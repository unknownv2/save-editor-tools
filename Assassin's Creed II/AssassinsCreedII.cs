using System;
using System.Windows.Forms;
using Horizon.Functions;
using Ubisoft;

namespace Horizon.PackageEditors.Assassins_Creed_II
{
    public partial class AssassinsCreedII : EditorControl
    {
        //public static readonly string FID = "5553083B";
        private ACIISaveGame saveGame;

        public AssassinsCreedII()
        {
            InitializeComponent();
            TitleID = FormID.AssassinsCreedII;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            saveGame = new ACIISaveGame(IO);
            intMoney.Value = saveGame.ObjectManager.Money;
            if(saveGame.ObjectManager.MoneyPosition == -1){

                UI.messageBox("Money not found! Failed to load save.",
                                        "Load Error!", MessageBoxIcon.Error);
                return false;
            }
            
            return true;
        }

        public override void Save()
        {
            saveGame.ObjectManager.Money = intMoney.Value;

            saveGame.Save();
        }

        private void cmdMaxMoney_Click(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
        }
    }
}
