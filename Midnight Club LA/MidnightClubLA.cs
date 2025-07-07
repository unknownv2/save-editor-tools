using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rockstar;

namespace Horizon.PackageEditors.Midnight_Club_LA
{
    public partial class MidnightClubLA : EditorControl
    {
        private MCLASaveGame _saveGame;
                
        public MidnightClubLA()
        {
            InitializeComponent();
            TitleID = FormID.MidnightClubLA;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("mc4.sav"))
                return false;

            _saveGame = new MCLASaveGame(IO);
            DisplayData();

            return true;
        }

        public override void Save()
        {
            _saveGame.Save();
        }

        private void DisplayData()
        {
            
        }
    }
}
