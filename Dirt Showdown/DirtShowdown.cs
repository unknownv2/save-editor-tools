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
using DirtShowdown;

namespace Horizon.PackageEditors.Dirt_Showdown
{
    public partial class DirtShowdown : EditorControl
    {
        //public static readonly string FID = "434D0845";
        private DirtSecuritySave.SecurityInfoFile SecurityFile;
        private RaceHistory RaceHistory;
        private DirtSecurityHelper SaveHelper;

        public DirtShowdown()
        {
            InitializeComponent();
            TitleID = FormID.DirtShowdown;
            
        }

        public override bool Entry()
        {
            SaveHelper = new DirtSecurityHelper();

            if (this.OpenStfsFile(SaveHelper.GetObfuscatedNameFromFilename("SECUINFO")))
            {
                this.SecurityFile = new DirtSecuritySave.SecurityInfoFile(this.IO, SaveHelper.GetFileListing());

                this.RaceHistory = new RaceHistory(this.Package.StfsContentPackage.GetEndianIO(SaveHelper.GetObfuscatedNameFromFilename("RACEHISTORY"), true), 
                    this.SecurityFile.GetFileEntry("RACEHISTORY"));

                this.DisplaySaveData();

                return true;
            }
            return false;
        }

        public override void Save()
        {
            // collect all possible data from the editor
            this.SetSaveData();

            // Save all edited files and update their security info
            this.RaceHistory.Save();
            this.SecurityFile.UpdateSecurityEntry(RaceHistory.FileInfo);

            // Flush the security data
            this.SecurityFile.Save();
        }

        private void DisplaySaveData()
        {
            this.intMoney.Value = this.RaceHistory.Money;
        }

        private void SetSaveData()
        {
            this.RaceHistory.Money = this.intMoney.Value;
        }

        private void cmdMaxBalance_Click(object sender, EventArgs e)
        {
            this.intMoney.Value = this.intMoney.MaxValue;
        }
    }
}
