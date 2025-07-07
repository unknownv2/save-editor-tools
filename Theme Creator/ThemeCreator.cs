using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Drawing.Imaging;
using Horizon.Properties;
using Horizon.Functions;

namespace Horizon.PackageEditors.Theme_Creator
{
    public partial class ThemeCreator : EditorControl
    {
        private string[] sphereColors = new string[] { "Default", "Gray", "Black", "Red-Pink", "Yellow", "Blue-Green", "Baby Blue",
            "Gray-Blue","Highlight Pink", "Tan", "Brown", "Gold", "Green", "Maroon", "Blue", "Violet", "Light Gray" };
        private bool[] imagesSet = new bool[] { false, false, false, false };
        public ThemeCreator()
        {
            InitializeComponent();
            hideShowPackageManagerIcon(false);
            foreach (string color in sphereColors)
                comboColor.Items.Add(color);
            comboColor.SelectedIndex = 0;
        }

        public override void enablePanels(bool enable)
        {
            
        }

        private MemoryStream getWallpaperStream(byte x)
        {
            string cap = String.Format(wallpaper, "W", x);
            if (Package.StfsContentPackage.GetDirectoryEntryIndex(cap) != -1)
                return new MemoryStream(Package.StfsContentPackage.GetFileStream(cap).ToArray());
            cap = String.Format(wallpaper, "w", x);
            if (Package.StfsContentPackage.GetDirectoryEntryIndex(cap) != -1)
                return new MemoryStream(Package.StfsContentPackage.GetFileStream(cap).ToArray());
            return (x != 1 && Package.StfsContentPackage.GetDirectoryEntryIndex(String.Format(wallpaper, "w", 1)) != -1)
                ? getWallpaperStream(1) : null;
        }

        private string wallpaper = "{0}allpaper{1}";
        public override bool Entry()
        {
            if (Package.Header.Metadata.ContentType == XContent.ContentTypes.ThematicSkin)
            {
                MemoryStream w = getWallpaperStream(1);
                if (w != null)
                {
                    pb1.BackgroundImage = Image.FromStream(w);
                    pb1.BackgroundImageLayout = ImageLayout.Stretch;
                    imagesSet[0] = true;
                }
                w = getWallpaperStream(2);
                if (w != null)
                {
                    pb2.BackgroundImage = Image.FromStream(w);
                    pb2.BackgroundImageLayout = ImageLayout.Stretch;
                    imagesSet[1] = true;
                }
                w = getWallpaperStream(3);
                if (w != null)
                {
                    pb3.BackgroundImage = Image.FromStream(w);
                    pb3.BackgroundImageLayout = ImageLayout.Stretch;
                    imagesSet[2] = true;
                }
                w = getWallpaperStream(4);
                if (w != null)
                {
                    pb4.BackgroundImage = Image.FromStream(w);
                    pb4.BackgroundImageLayout = ImageLayout.Stretch;
                    imagesSet[3] = true;
                }
            }
            else
                Functions.UI.messageBox("Invalid package loaded!\n\nYou must load a theme file!", "Invalid Package", MessageBoxIcon.Error);
            Package.CloseIO(true);
            Meta.FatxPath = null;
            enableTemplates();
            return true;
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.png;*.jpg;*.gif;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ButtonX but = (ButtonX)sender;
                Image newImage = (Image)(new Bitmap(Image.FromFile(ofd.FileName), 1280, 720));
                switch (but.TabIndex)
                {
                    case 0:
                        pb1.BackgroundImageLayout = ImageLayout.Stretch;
                        pb1.BackgroundImage = newImage;
                        break;
                    case 1:
                        pb2.BackgroundImageLayout = ImageLayout.Stretch;
                        pb2.BackgroundImage = newImage;
                        break;
                    case 2:
                        pb3.BackgroundImageLayout = ImageLayout.Stretch;
                        pb3.BackgroundImage = newImage;
                        break;
                    case 3:
                        pb4.BackgroundImageLayout = ImageLayout.Stretch;
                        pb4.BackgroundImage = newImage;
                        break;
                }
                imagesSet[but.TabIndex] = true;
                enableTemplates();
            }
        }

        private void cmdSaveImage_Click(object sender, EventArgs e)
        {
            if (imagesSet[((ButtonX)sender).TabIndex])
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image|*.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    switch (((ButtonX)sender).TabIndex)
                    {
                        case 0:
                            pb1.BackgroundImage.Save(sfd.FileName);
                            break;
                        case 1:
                            pb2.BackgroundImage.Save(sfd.FileName);
                            break;
                        case 2:
                            pb3.BackgroundImage.Save(sfd.FileName);
                            break;
                        case 3:
                            pb4.BackgroundImage.Save(sfd.FileName);
                            break;
                    }
                    Functions.UI.messageBox("Image saved successfully!", "Image Saved", MessageBoxIcon.Information);
                }
            }
            else
                Functions.UI.messageBox("Load an image first!", "No Image", MessageBoxIcon.Warning);
        }

        private string parameters = "SphereColor={0}" + Environment.NewLine
            + "AvatarLightingDirectional=0.5,-0.6123,-1.0,0"
            + Environment.NewLine + "AvatarLightingAmbient=0"
            + Environment.NewLine;

        internal override void cmdSave_Click(object sender, EventArgs e)
        {
            if (readyToSave())
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = txtName.Text.Length == 0 ? txtName.WatermarkText : txtName.Text;
                if (sfd.ShowDialog() == DialogResult.OK && createPackage(sfd.FileName))
                    Functions.UI.messageBox("Your theme was created successfully!", "Theme Saved", MessageBoxIcon.Information);
            }
        }

        private bool readyToSave()
        {
            for (byte x = 0; x < imagesSet.Length; x++)
                if (!imagesSet[x])
                {
                    Functions.UI.messageBox("You must load all 4 images to create a theme!", "Load Images", MessageBoxIcon.Error);
                    return false;
                }
            return true;
        }

        private bool createPackage(string fileName)
        {
            try
            {
                XContent.XContentMetadata meta = new XContent.XContentMetadata();
                meta.TitleName = "Horizon";
                string fullName = txtName.Text.Length == 0 ? txtName.WatermarkText : txtName.Text;
                meta.SetAllDisplayNames(fullName);
                meta.SetAllDescriptions(fullName);
                meta.DescriptionEx = meta.DisplayNameEx = meta.Publisher = String.Empty;
                meta.ContentType = XContent.ContentTypes.ThematicSkin;
                meta.ExecutionId.TitleId = 4294838225;
                byte[] thumbnail = Resources.Logo64.ToByteArray();
                meta.SetThumbnail(thumbnail);
                meta.SetTitleThumbnail(thumbnail);
                meta.DeviceId = new byte[20];
                meta.ConsoleId = new byte[5];
                meta.ContentMetadataVersion = 2;
                meta.Reserved2 = new byte[44];
                meta.TransferFlags.bTransferFlags = 192;
                meta.TransferFlags.DeviceTransfer = 1;
                meta.TransferFlags.ProfileTransfer = 1;
                meta.AvatarAssetData.AssetId = new byte[16];
                meta.AvatarAssetData.Reserved = new byte[11];
                meta.MediaData.SeasonId = new byte[16];
                meta.Reserved2 = new byte[44];

                Package = new XContent.XContentPackage();
                Package.CreatePackage(fileName, meta);

                Package.StfsContentPackage.CreateFileFromArray(String.Format(wallpaper, "W", 1), pb1.BackgroundImage.ToByteArray());
                Package.StfsContentPackage.CreateFileFromArray(String.Format(wallpaper, "W", 2), pb2.BackgroundImage.ToByteArray());
                Package.StfsContentPackage.CreateFileFromArray(String.Format(wallpaper, "W", 3), pb3.BackgroundImage.ToByteArray());
                Package.StfsContentPackage.CreateFileFromArray(String.Format(wallpaper, "W", 4), pb4.BackgroundImage.ToByteArray());

                Package.StfsContentPackage.CreateFileFromArray("DashStyle", new byte[4]);
                Package.StfsContentPackage.CreateFileFromArray("parameters.ini",
                    Encoding.ASCII.GetBytes(String.Format(parameters, comboColor.SelectedIndex)));

                Package.Flush();
                Package.Header.Metadata.ContentSize = (ulong)(Package.IO.Stream.Length - Package.StfsContentPackage.VolumeExtension.BackingFileOffset);
                Package.Save(true);
                Package.CloseIO(true);
                return true;
            }
            catch (Exception e)
            {
                UI.errorBox("An error has occured while building your theme!\n\n" + e.Message);
            }
            return false;
        }

        private void cmdTemplate_CheckedChanged(object sender, EventArgs e)
        {
            enableTemplates();
            if (!cmdTemplate.Checked)
                pb4.Image = pb3.Image = pb2.Image = pb1.Image = null;
        }

        private void enableTemplates()
        {
            if (cmdTemplate.Checked)
            {
                if (imagesSet[0])
                    pb1.Image = Resources.Theme_Main;
                if (imagesSet[1])
                    pb2.Image = Resources.Theme_Game;
                if (imagesSet[2])
                    pb3.Image = Resources.Theme_Other;
                if (imagesSet[3])
                    pb4.Image = Resources.Theme_Other;
            }
        }

        private void cmdSaveToDevice_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            cmdSaveToDevice.SubItems.Clear();
            ButtonItem[] deviceButtons = FatxHandle.createDeviceButtonItems();
            for (int x = 0; x < deviceButtons.Length; x++)
                deviceButtons[x].Click += new EventHandler(packageManagerDevice_Click);
            cmdSaveToDevice.SubItems.AddRange(deviceButtons);
        }

        private void packageManagerDevice_Click(object sender, EventArgs e)
        {
            if (readyToSave())
            {
                string fileName = txtName.Text.Length == 0 ? txtName.WatermarkText : txtName.Text;
                if (Library.Systems.FATX.FatxDevice.FatxIsValidFatFileName(fileName))
                {
                    int x = (int)((ButtonItem)sender).Tag;
                    if (FatxHandle.isDeviceWorkerAvailable(x))
                    {
                        fileName = Path.GetTempPath() + @"\" + fileName;
                        File.Delete(fileName);
                        if (createPackage(fileName))
                            FatxHandle.injectPackages(x, new string[] { fileName }, this);
                    }
                }
                else
                    Functions.UI.errorBox("Your theme name contains invalid characters!");
            }
        }
    }
}
