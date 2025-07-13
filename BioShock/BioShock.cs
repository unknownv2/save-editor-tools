using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IrrationalGames;

namespace Horizon.PackageEditors.BioShock
{
    public partial class BioShock : EditorControl
    {
        private BioShockClass bioshockSave;

        public BioShock()
        {
            InitializeComponent();
            TitleID = FormID.BioShock;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(Package.StfsContentPackage.DirectoryEntries[0].FileName + "\\" + "mainSave.bsg"))
                return false;

            bioshockSave = new BioShockClass(IO);

            return true;
        }

        public override void Save()
        {
            
        }

        private void BtnClickExtractData(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            bioshockSave.ExtractData().Save(sfd.FileName);
        }
    }
}
