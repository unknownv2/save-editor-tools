using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Crysis2;

namespace Horizon.PackageEditors.Crysis_2
{
    public partial class Crysis2Campaign : EditorControl
    {
        //public static readonly string FID = "454108EU";

        private GameSave CampaignSave;
        public Crysis2Campaign()
        {
            InitializeComponent();
            TitleID = FormID.Crysis2Profile;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("Nomad.CSF"))
                return false;

            CampaignSave = new GameSave(IO);

            return true;
        }
        public override void Save()
        {
            
        }

        private void Btn_ExtractClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, this.CampaignSave.GetFileData());
            }
        }
    }
}
