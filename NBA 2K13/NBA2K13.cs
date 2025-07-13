using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBA2K;

namespace Horizon.PackageEditors.NBA_2K13
{
    public partial class NBA2K13 : EditorControl
    {
        private NBA2K13Save _saveFile;

        public NBA2K13()
        {
            InitializeComponent();
            TitleID = FormID.NBA2K13;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            _saveFile = new NBA2K13Save(IO);
            intSkillPoints.Value = (int) _saveFile.SkillPoints;
            intFans.Value = (int) _saveFile.Fans;

            return true;
        }

        public override void Save()
        {
            _saveFile.SkillPoints = (uint) intSkillPoints.Value;
            _saveFile.Fans = (uint) intFans.Value;

            _saveFile.Save();
        }

        private void BtnClickMaxSP(object sender, EventArgs e)
        {
            intSkillPoints.Value = intSkillPoints.MaxValue;
        }

        private void BtnClickMaxFans(object sender, EventArgs e)
        {
            intFans.Value = intFans.MaxValue;
        }
    }
}
