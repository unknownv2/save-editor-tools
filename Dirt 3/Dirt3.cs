using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dirt;

namespace Horizon.PackageEditors.Dirt_3
{
    public partial class Dirt3 : EditorControl
    {
        //public static readonly string FID = "434D083D";

        private global::Dirt3.RaceHistory RaceHistory;
        private DirtSecuritySave.SecurityInfoFile SecurityFile;
        private DirtSecurityHelper SaveHelper;

        public Dirt3()
        {
            InitializeComponent();
            TitleID = FormID.Dirt3;
            
        }

        public override bool Entry()
        {
            SaveHelper = new DirtSecurityHelper();

            if (this.OpenStfsFile(SaveHelper.GetObfuscatedNameFromFilename("SECUINFO")))
            {
                this.SecurityFile = new DirtSecuritySave.SecurityInfoFile(this.IO, SaveHelper.GetFileListing());

                this.RaceHistory = new global::Dirt3.RaceHistory(this.Package.StfsContentPackage.GetEndianIO(
                    SaveHelper.GetObfuscatedNameFromFilename("RACEHISTORY"), true),
                    this.SecurityFile.GetFileEntry("RACEHISTORY"));

                this.intBalance.Value = this.RaceHistory.Points;

                return true;
            }
            return false;
        }

        public override void Save()
        {
            this.RaceHistory.Points = this.intBalance.Value;
            this.RaceHistory.Save();

            this.SecurityFile.UpdateSecurityEntry(RaceHistory.FileInfo);
            this.SecurityFile.Save();
        }

        private void cmdMaxBalance_Click(object sender, EventArgs e)
        {
            intBalance.Value = intBalance.MaxValue;
        }
    }
}
