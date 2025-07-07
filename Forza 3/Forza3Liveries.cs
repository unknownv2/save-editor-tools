using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Forza3;
using ForzaMotorsport;

namespace Horizon.PackageEditors.Forza_3
{
    public partial class Forza3Liveries : EditorControl
    {
        //public static readonly string FID = "4D53084U";

        private Forza3Livery Livery;

        private string Creator;

        List<List<string>> LiveryFolders;

        public Forza3Liveries()
        {
            InitializeComponent();
            TitleID = FormID.Forza3;
        }
        public override bool Entry()
        {
            this.Creator = this.Package.Header.Metadata.Creator.ToString("X");

            LiveryFolders = new List<List<string>>();
            /*
            for (var x = 0; x < this.Package.StfsContentPackage.DirectoryEntries.Count; x++)
            {
                var DirectoryEntry = this.Package.StfsContentPackage.DirectoryEntries[x];
                if (DirectoryEntry.IsDirectory && DirectoryEntry.DirectoryIndex == 0xFFFF)
                {
                    LiveryFolders.Add(new List<string>());
                    LiveryFolders[LiveryCount].Add(DirectoryEntry.FileName);
                    LiveryFolders[LiveryCount].Add(this.Package.StfsContentPackage.StfsFindNextDirectoryName(this.Package.StfsContentPackage.GetFileStream(DirectoryEntry.FileName).Fcb));
                    LiveryFolders[LiveryCount].Add(this.Package.StfsContentPackage.StfsFindNextDirectoryName(this.Package.StfsContentPackage.GetFileStream(DirectoryEntry.FileName + "\\" + LiveryFolders[LiveryCount][1]).Fcb));

                    var FullName = new StringBuilder();
                    for (var i = 0; i < 3; i++)
                    {      
                        FullName.Append(LiveryFolders[LiveryCount][i] + (i != 2 ? "\\" : string.Empty));   
                    }

                    LiveryFolders[LiveryCount].Add(FullName.ToString());

                    this.cmbLiveryIndex.Items.Add(LiveryFolders[LiveryCount++][2]);
                }
            }
            */
            if (LiveryFolders.Count > 0)
            {
                this.cmbLiveryIndex.SelectedIndex = 0x00;

                return true;
            }
            else
            {
                return false;
            }
        }
        public override void Save()
        {
            
        }

        private void CmbSelectedIndexChange(object sender, EventArgs e)
        {
            this.Livery = new Forza3Livery(
                this.Creator, 
                this.Package.StfsContentPackage.ExtractFileToArray(this.LiveryFolders[this.cmbLiveryIndex.SelectedIndex][3]),
                ForzaFileTypes.CarSetups
                );
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (this.Livery != null)
            {
                var sfd = new SaveFileDialog();
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                File.WriteAllBytes(sfd.FileName, this.Livery.ExtractFileData());
            }
        }
    }
}