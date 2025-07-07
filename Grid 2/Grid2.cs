using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dirt;
using Codemasters;
using System.IO;

namespace Horizon.PackageEditors.Grid_2
{
    public partial class Grid2 : EditorControl
    {
        private DirtSecurityHelper SaveHelper;
        private DirtSecuritySave.SecurityInfoFile SecurityFile;
        private Grid2Save progressData;
        private Grid2Save settingsData;
        /*
         * In both progress and settings (post-decompression), ignore last 32-bits (zero) as part of file data.
             * Progress Data Layout:
                 * <GAME DATA>
                 * <0x10 BYTES (unkData)>
                 * <32-bit Zero>
             * Settings Data Layout:
                 * <0x10 BYTES (unkData)>
                 * <GAME DATA>
                 * <32-bit Zero>
         * ALL THESE PARTS ARE THE SAME IN BOTH PROGRESS AND SETTINGS, JUST ORDER IS DIFFERENT.
         */
        public Grid2()
        {
            InitializeComponent();
        }

        public override bool Entry()
        {
            SaveHelper = new DirtSecurityHelper(new [] { "SECUINFO", "SETTINGS.DAT", "PROGRESS.DAT" });
            if (OpenStfsFile(SaveHelper.GetObfuscatedNameFromFilename("SECUINFO")))
            {
                SecurityFile = new DirtSecuritySave.SecurityInfoFile(IO, SaveHelper.GetFileListing());
                progressData = new Grid2Save(Package.StfsContentPackage.GetEndianIO(SaveHelper.GetObfuscatedNameFromFilename("PROGRESS.DAT"), true),
                    SecurityFile.GetFileEntry("PROGRESS.DAT"));
                progressData.Read();
                settingsData = new Grid2Save(Package.StfsContentPackage.GetEndianIO(SaveHelper.GetObfuscatedNameFromFilename("SETTINGS.DAT"), true),
                    SecurityFile.GetFileEntry("SETTINGS.DAT"));
            }

            intFans.Value = progressData.Fans;

            return true;
        }

        public override void Save()
        {
            progressData.Fans = intFans.Value;

            Package.StfsContentPackage.InjectFileFromArray(SaveHelper.GetObfuscatedNameFromFilename("SETTINGS.DAT"), settingsData.Save());

            SecurityFile.UpdateSecurityEntry(settingsData.FileInfo);

            // Flush the security data
            SecurityFile.Save();
        }

        private void BtnClickExtractProgress(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllBytes(sfd.FileName, progressData.Extract());
        }

        private void BtnClickExtractSettings(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllBytes(sfd.FileName, settingsData.Extract());
        }
    }
}
