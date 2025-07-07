using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ForzaMotorsport;

namespace Horizon.PackageEditors.Forza_Horizon
{
    public partial class ForzaHorizonSS : EditorControl
    {
        //public static readonly string FID = "4D5309CX";

        private ForzaScreenshot _screenshot;
        private List<List<string>> _imageFolders;
        private ulong _creator;
        private string _imageFolderName, _screenshotPath;

        private byte[] _aesKey, _hmacKey;

        public ForzaHorizonSS()
        {
            InitializeComponent();
            TitleID = FormID.ForzaHorizonProfile;

            AlternateTitleIds = new List<string> {FormID.ForzaHorizon2Profile};
        }

        public override bool Entry()
        {
            cmbBoxScreenshotIndex.Items.Clear();

            _creator = Package.Header.Metadata.Creator;
            _imageFolders = new List<List<string>>();
            int imagecount = 0;
            foreach (var directoryEntry in Package.StfsContentPackage.DirectoryEntries)
            {
                if (!directoryEntry.IsDirectory || !directoryEntry.IsEntryBound) continue;
                _imageFolders.Add(new List<string>());
                _imageFolders[imagecount].Add(directoryEntry.FileName);
                _imageFolders[imagecount].Add(Package.StfsContentPackage.StfsFindNextDirectoryName(Package.StfsContentPackage.GetFileStream(directoryEntry.FileName).Fcb, 0x00));
                cmbBoxScreenshotIndex.Items.Add(imagecount++.ToString());
            }

            if (_imageFolders.Count <= 0)
                return false;

            _aesKey = SettingAsByteArray(74);
            _hmacKey = SettingAsByteArray(92);

            pictureBox1.BackgroundImage = null;
            cmbBoxScreenshotIndex.SelectedIndex = 0x00;
            return true;
        }

        private void ScreenshotIndexChanged(object sender, EventArgs e)
        {
            _imageFolderName = _imageFolders[cmbBoxScreenshotIndex.SelectedIndex][0];

            _screenshotPath = _imageFolderName + "\\" + _imageFolders[cmbBoxScreenshotIndex.SelectedIndex][1];

            OpenScreenshot();

            pictureBox1.Image = Image.FromStream(Package.StfsContentPackage.GetFileStream(_imageFolderName + "\\thumb"));
        }
        private void BtnClickExtract(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.FileName = "Forza Horizon Screenshot";
            sfd.Filter = "JPEG Files (*.jpg)|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _screenshot.Read().Save(sfd.FileName);
                Functions.UI.messageBox("Successfully extracted a screenshot!", "Done!", MessageBoxIcon.Information);
            }
        }
        private void BtnClickInject(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;

            // inject the full image
            var realImage = ResizeImage(Image.FromStream(new MemoryStream(File.ReadAllBytes(ofd.FileName))), new Size { Width = 1280, Height = 720 });
            Package.StfsContentPackage.InjectFileFromArray(_screenshotPath, _screenshot.Write(realImage.ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg)));

            OpenScreenshot();

            // inject the thumbnail
            var thumbnailImage = realImage.GetThumbnailImage(416, 234, null, IntPtr.Zero);
            Package.StfsContentPackage.InjectFileFromArray(_imageFolderName + "\\thumb", thumbnailImage.ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg));
            pictureBox1.Image = thumbnailImage;
            Functions.UI.messageBox("Successfully injected screenshot!", "Done!", MessageBoxIcon.Information);
        }
        private void OpenScreenshot()
        {
            // re-open the screenshot's EndianIO
            OpenStfsFile(_screenshotPath);

            _screenshot = new ForzaScreenshot(IO, _creator, _aesKey, _hmacKey, ForzaVersion.ForzaHorizon);
        }
        private static Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercentW = (size.Width / (float)sourceWidth);
            float nPercentH = (size.Height / (float)sourceHeight);

            float nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }
    }
}
