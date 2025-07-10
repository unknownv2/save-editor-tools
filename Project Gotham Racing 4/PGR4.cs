using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectGothamRacing;

namespace Horizon.PackageEditors.Project_Gotham_Racing_4
{
    public partial class PGR4 : EditorControl
    {
        private PGR4SaveGame _saveGame;

        public PGR4()
        {
            InitializeComponent();
            TitleID = FormID.ProjectGothamRacing4;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("savegame"))
                return false;

            _saveGame = new PGR4SaveGame(IO);

            DisplayData();

            return true;
        }

        public override void Save()
        {
            _saveGame.Kudos = intKudosPoints.Value;

            _saveGame.Save();
        }

        private void DisplayData()
        {
            intKudosPoints.Value = _saveGame.Kudos;
        }

        private void BtnClick_MaxKudos(object sender, EventArgs e)
        {
            intKudosPoints.Value = intKudosPoints.MaxValue;
        }
    }
}
