using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Horizon.Forms;
using System.Threading;
using System.Globalization;
using Horizon.Functions;
using System.IO;
using DevComponents.DotNetBar;
using Horizon.Properties;
using XContent;
using XboxDataBaseFile;
using XProfile;

namespace Horizon.PackageEditors.Gamer_Picture_Manager
{
    public partial class GamerPictureManager : EditorControl
    {
        public GamerPictureManager()
        {
            InitializeComponent();
            hideShowPackageManagerIcon(false);
            gamerPics.ImageSize = new Size(64, 64);
            gamerPics.ColorDepth = ColorDepth.Depth32Bit;
            listMyPics.LargeImageList = listPics.LargeImageList = gamerPics;
        }

        public override void enablePanels(bool enable)
        {
            
        }

        private ProfileFile Profile;
        public override bool Entry()
        {
            if (Package.Header.Metadata.ContentType == ContentTypes.GamerPicture)
            {
                listMyPics.BeginUpdate();
                foreach (StfsDirectoryEntry entry in Package.StfsContentPackage.DirectoryEntries)
                    if (entry.FileName[0] == '6')
                    {
                        string titleId = entry.FileName.Substring(3, 8).ToLower();
                        if (!usedIds.ContainsKey(titleId))
                            usedIds.Add(titleId, new List<short>());
                        short imageId;
                        if (short.TryParse(entry.FileName.Substring(15, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out imageId)
                            && !usedIds[titleId].Contains(imageId))
                        {
                            try
                            {
                                byte[] imageArray = Package.StfsContentPackage.ExtractFileToArray(entry.FileName);
                                gamerPics.Images.Add(Image.FromStream(new MemoryStream(imageArray)));
                                imageData.Add(imageArray);
                                listPics.Items.Add(new ListViewItem(String.Empty, gamerPics.Images.Count - 1) { Tag = titleId + imageId });
                                usedIds[titleId].Add(imageId);
                            }
                            catch { }
                        }
                    }
                listMyPics.EndUpdate();
                if (listPics.Items.Count > 0)
                    cmdAddAll.Enabled = true;
                panelProfile.Visible = false;
                cmdSaveToDevice.Visible = true;
                hideShowPackageManagerIcon(false);
                rbPackageEditor.Refresh();
                Package.CloseIO(true);
                Meta.FatxPath = null;
            }
            else if (Package.Header.Metadata.ContentType == ContentTypes.Profile)
            {
                hideShowPackageManagerIcon(true);
                cmdAddAll.Enabled = cmdSaveToDevice.Visible = false;
                pbSelected.Tag = pbCurrent.Tag = String.Empty;
                pbCurrent.Image = Resources.Console;
                pbSelected.Image = Resources.Console;
                Profile = new ProfileFile(Package, 0xfffe07d1);
                Profile.Read();
                SettingRecord rec = new SettingRecord();
                if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_PICTURE_KEY)))
                {
                    string imageData = UnicodeEncoding.BigEndianUnicode.GetString(rec.varData);
                    string titleId = imageData.Substring(0, 8).ToLower();
                    if (titleId == "fffe0854")
                        try
                        {
                            pbCurrent.Image = Image.FromStream(new MemoryStream(Package.Header.Metadata.Thumbnail));
                        }
                        catch { }
                    else
                    {
                        short imageId;
                        if (short.TryParse(imageData.Substring(12, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out imageId))
                        {
                            try
                            {
                                pbCurrent.Image = Image.FromStream(new WebClient().OpenRead(buildGamerPicUrl(titleId, 2, imageId)));
                                pbCurrent.Tag = titleId + imageId;
                            }
                            catch
                            {
                                UI.errorBox("The gamerpicture in your profile is no longer available on Xbox LIVE!");
                            }
                        }
                    }
                }
                while (listMyPics.Items.Count > 0)
                    addToPics(0);
                exTitleIdFinder.Expanded = false;
                cmdOpenTitleIdFinder.Image = Resources.DownArrow;
                panelProfile.Visible = true;
                rbPackageEditor.Refresh();
            }
            else
            {
                Functions.UI.messageBox("Invalid package loaded!\n\nYou must load a Profile or a Gamer Picture Pack!", "Invalid Package", MessageBoxIcon.Error);
                Package.CloseIO(true);
            }
            return true;
        }

        private volatile ImageList gamerPics = new ImageList();
        private static string buildGamerPicUrl(string titleId, int tileSize, short imageId)
        {
            return String.Format("http://image.xboxlive.com/global/t.{0}/tile/0/{1}{2:x4}", titleId, tileSize, imageId);
        }

        private static string buildEntryName(string titleId, int size, short imageId)
        {
            string name = String.Format("{0}0002{1:x4}0001{1:x4}", titleId, imageId);
            if (size == 0)
                return name;
            return String.Format("{0}_{1}.png", size, name);
        }


        private void listPics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPics.SelectedIndices.Count == 1)
                listPics.SelectedItems[0].Selected = false;
        }

        private void listMyPics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMyPics.SelectedIndices.Count == 1)
                listMyPics.SelectedItems[0].Selected = false;
        }

        private List<byte[]> imageData = new List<byte[]>();
        private int selectedIndex = 0;
        private void listPics_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewItem gPic = listPics.GetItemAt(e.X, e.Y);
            if (gPic != null)
            {
                if (panelProfile.Visible)
                {
                    pbSelected.Image = gamerPics.Images[listPics.Items[gPic.Index].ImageIndex];
                    pbSelected.Tag = listPics.Items[gPic.Index].Tag;
                    selectedIndex = listPics.Items[gPic.Index].ImageIndex;
                }
                else
                {
                    addToMyPics(gPic.Index);
                    enableClearAdd();
                }
            }
        }

        private void enableClearAdd()
        {
            cmdAddAll.Enabled = listPics.Items.Count > 0 && !panelProfile.Visible;
            cmdClearPics.Enabled = listMyPics.Items.Count > 0;
        }

        private void listMyPics_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewItem gPic = listMyPics.GetItemAt(e.X, e.Y);
            if (gPic != null)
            {
                if (panelProfile.Visible)
                {
                    pbSelected.Image = gamerPics.Images[listMyPics.Items[gPic.Index].ImageIndex];
                    pbSelected.Tag = listMyPics.Items[gPic.Index].Tag;
                }
                else
                {
                    addToPics(gPic.Index);
                    enableClearAdd();
                }
            }
        }

        private void addToPics(int x)
        {
            listPics.Items.Add(new ListViewItem(String.Empty, listMyPics.Items[x].ImageIndex) { Tag = listMyPics.Items[x].Tag });
            listMyPics.Items.RemoveAt(x);
        }

        private void addToMyPics(int x)
        {
            listMyPics.Items.Add(new ListViewItem(String.Empty, listPics.Items[x].ImageIndex) { Tag = listPics.Items[x].Tag });
            listPics.Items.RemoveAt(x);
        }

        private void cmdOpenTitleIdFinder_Click(object sender, EventArgs e)
        {
            exTitleIdFinder.Expanded = !exTitleIdFinder.Expanded;
            cmdOpenTitleIdFinder.Image = exTitleIdFinder.Expanded ? Resources.UpArrow : Resources.DownArrow;
        }

        private Thread lowerThread, upperThread;
        private Dictionary<string, short> lowerPics = new Dictionary<string, short>(), 
            upperPics = new Dictionary<string, short>();
        private Dictionary<string, List<short>> usedIds = new Dictionary<string, List<short>>();
        private void cmdGetGamerpics_Click(object sender, EventArgs e)
        {
            uint titleId;
            if (txtTitleId.Text.Length == 8 && uint.TryParse(txtTitleId.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out titleId))
            {
                while (killThreads > 0)
                    Thread.Sleep(10);
                string titleIdString = txtTitleId.Text.ToLower();
                cmdGetGamerpics.Enabled = false;
                cmdStopSearch.Enabled = txtTitleId.ReadOnly = true;
                if (!usedIds.ContainsKey(titleIdString))
                    usedIds.Add(titleIdString, new List<short>());
                if (!lowerPics.ContainsKey(titleIdString))
                {
                    lowerPics.Add(titleIdString, -32768);
                    upperPics.Add(titleIdString, 0);
                }
                lowerThread = new Thread(new ParameterizedThreadStart(searchForPics));
                lowerThread.Start(new SearchParameters()
                {
                    TitleID = titleIdString,
                    IsUpper = false,
                    EndID = -1
                });
                upperThread = new Thread(new ParameterizedThreadStart(searchForPics));
                upperThread.Start(new SearchParameters()
                {
                    TitleID = titleIdString,
                    IsUpper = true,
                    EndID = 32767
                });
                progressSearch.Style = ProgressBarStyle.Marquee;
            }
            else if (txtTitleId.Text.Length != 0)
            {
                txtGameName.Text = txtTitleId.Text;
                exTitleIdFinder.Expanded = true;
                cmdOpenTitleIdFinder.Image = Resources.UpArrow;
                cmdSearchForTitle_Click(null, null);
            }
        }

        private struct SearchParameters
        {
            internal bool IsUpper;
            internal short EndID;
            internal string TitleID;
        }

        private void searchForPics(object param)
        {
            killThreads = 0;
            SearchParameters par = (SearchParameters)param;
            short startId = par.IsUpper ? upperPics[par.TitleID] : lowerPics[par.TitleID];
            for (short x = startId; x <= par.EndID; x++)
            {
                if (killThreads > 0)
                {
                    killThreads--;
                    break;
                }
                if (par.TitleID == "41560855" && x > 1 && x < 0x400)
                    x = 0x400;
                if (!usedIds[par.TitleID].Contains(x))
                {
                    try
                    {
                        byte[] imageArray = new WebClient().DownloadData(buildGamerPicUrl(par.TitleID, 2, x));
                        Image gPicStream = Image.FromStream(new MemoryStream(imageArray));
                        listPics.Invoke((MethodInvoker)delegate
                        {
                            gamerPics.Images.Add(gPicStream);
                            byte[] newArray = new byte[imageArray.Length];
                            Array.Copy(imageArray, newArray, imageArray.Length);
                            imageData.Add(newArray);
                            listPics.Items.Add(new ListViewItem(String.Empty, gamerPics.Images.Count - 1) { Tag = par.TitleID + x });
                            cmdAddAll.Enabled = !panelProfile.Visible;
                        });
                        usedIds[par.TitleID].Add(x);
                        if (par.IsUpper)
                            upperPics[par.TitleID] = (short)(x + 1);
                        else
                            lowerPics[par.TitleID] = (short)(x + 1);
                        Thread.Sleep(10);
                    }
                    catch { }
                }
            }
        }

        private void cmdStopSearch_Click(object sender, EventArgs e)
        {
            enableControls();
        }

        private int killThreads = 0;
        private void enableControls()
        {
            if (upperThread != null && upperThread.IsAlive)
                killThreads++;
            if (lowerThread != null && lowerThread.IsAlive)
                killThreads++;
            progressSearch.Style = ProgressBarStyle.Blocks;
            txtTitleId.ReadOnly = cmdStopSearch.Enabled = false;
            cmdGetGamerpics.Enabled = true;
        }

        private void GamerPicturePackCreator_FormClosing(object sender, FormClosingEventArgs e)
        {
            killThreads = 2;
        }

        private void cmdAddAll_Click(object sender, EventArgs e)
        {
            while (listPics.Items.Count > 0)
                addToMyPics(0);
            exTitleIdFinder.Expanded = cmdAddAll.Enabled = false;
            cmdOpenTitleIdFinder.Image = Resources.DownArrow;
            cmdClearPics.Enabled = true;
        }

        private void cmdSaveToDevice_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
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
                string fileName = txtPackName.Text.Length == 0 ? txtPackName.WatermarkText : txtPackName.Text;
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
                    Functions.UI.errorBox("Your pack name contains invalid characters!");
            }
        }

        private void cmdSearchForTitle_Click(object sender, EventArgs e)
        {
            if (txtGameName.Text.Length != 0)
            {
                List<ListViewItem> titleList = TitleIDFinder.doSearchTitle(txtGameName.Text);
                if (titleList.Count == 0)
                    UI.messageBox("No titles found!");
                else
                {
                    listTitles.Items.Clear();
                    foreach (ListViewItem title in titleList)
                        listTitles.Items.Add(title);
                }
            }
        }

        private void cmdUseThisGame_Click(object sender, EventArgs e)
        {
            if (listTitles.SelectedIndices.Count == 1)
            {
                enableControls();
                txtTitleId.Text = listTitles.SelectedItems[0].SubItems[1].Text;
                cmdOpenTitleIdFinder_Click(null, null);
                cmdGetGamerpics_Click(null, null);
            }
        }

        private bool readyToSave()
        {
            if (listMyPics.Items.Count > 1000)
            {
                UI.errorBox("You have selected too many images! You currently have " + listMyPics.Items.Count
                    + " on your list.\n\nTo create a picture pack you must select 1000 or less!");
                return false;
            }
            if (listMyPics.Items.Count == 0)
            {
                UI.errorBox("You must have at least one image on the right list to build a gamerpic package!");
                return false;
            }
            return true;
        }

        private bool createPackage(string fileName)
        {
            int x = 0;
            XContentMetadata meta = new XContentMetadata();
            meta.TitleName = "Horizon";
            string fullName = txtPackName.Text.Length == 0 ? txtPackName.WatermarkText : txtPackName.Text;
            meta.SetAllDisplayNames(fullName);
            meta.SetAllDescriptions(fullName);
            meta.DescriptionEx = meta.DisplayNameEx = meta.Publisher = String.Empty;
            meta.ContentType = ContentTypes.GamerPicture;
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
            Package = new XContentPackage();
            Package.CreatePackage(fileName, meta);
            try
            {
                foreach (ListViewItem pic in listMyPics.Items)
                {
                    x++;
                    string titleId = ((string)pic.Tag).Substring(0, 8);
                    short imageId = short.Parse(((string)pic.Tag).Substring(8));
                    Package.StfsContentPackage.CreateFileFromArray(buildEntryName(titleId, 64, imageId), imageData[pic.ImageIndex]);
                    Package.StfsContentPackage.CreateFileFromArray(buildEntryName(titleId, 32, imageId), toImage32(gamerPics.Images[pic.ImageIndex]).ToByteArray());
                }
                Package.Flush();
                Package.Header.Metadata.ContentSize = (ulong)(Package.IO.Stream.Length - Package.StfsContentPackage.VolumeExtension.BackingFileOffset);
                Package.Save(true);
                Package.CloseIO(true);
                return true;
            }
            catch (Exception e)
            {
                Functions.UI.errorBox("An error has occured while building your package!\n\nOn file #" + x + "\n\n" + e.Message);
            }
            Package.CloseIO(true);
            return false;
        }

        private Image toImage32(Image image)
        {
            Bitmap b = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0, 32, 32);
            g.Dispose();
            return (Image)b;
        }

        internal override void cmdSave_Click(object sender, EventArgs e)
        {
            if (panelProfile.Visible)
            {
                string imageTag = (string)pbSelected.Tag;
                if (imageTag.Length == 0)
                    UI.errorBox("You must select an image first!");
                else
                    base.cmdSave_Click(null, null);
            }
            else if (readyToSave())
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = txtPackName.Text.Length == 0 ? txtPackName.WatermarkText : txtPackName.Text;
                if (sfd.ShowDialog() == DialogResult.OK && createPackage(sfd.FileName))
                    Functions.UI.messageBox("Your Gamer Picture Pack was created successfully!", "Picture Pack Saved", MessageBoxIcon.Information);
            }
        }

        public override void Save()
        {
            byte[] image64 = imageData[selectedIndex];
            byte[] image32 = toImage32(pbSelected.Image).ToByteArray();
            Package.Header.Metadata.SetThumbnail(image64);
            var DirectoryEntries = Package.StfsContentPackage.DirectoryEntries.ToList();
            foreach (StfsDirectoryEntry entry in DirectoryEntries)
                if (!entry.IsDirectory)
                    switch (entry.FileName)
                    {
                        case "tile_64.png":
                            //Package.StfsContentPackage.DeleteFile("tile_64.png");
                            Package.StfsContentPackage.InjectFileFromArray("tile_64.png", image64);
                            break;
                        case "tile_32.png":
                            //Package.StfsContentPackage.DeleteFile("tile_32.png");
                            Package.StfsContentPackage.InjectFileFromArray("tile_32.png", image32);
                            break;
                        case "pp_64.png":
                            //Package.StfsContentPackage.DeleteFile("pp_64.png");
                            Package.StfsContentPackage.InjectFileFromArray("pp_64.png", image64);
                            break;
                        case "pp_32.png":
                            //Package.StfsContentPackage.DeleteFile("pp_32.png");
                            Package.StfsContentPackage.InjectFileFromArray("pp_32.png", image32);
                            break;
                    }
            string imageTag = buildEntryName(((string)pbSelected.Tag).Substring(0, 8), 0, short.Parse(((string)pbSelected.Tag).Substring(8)));
            SettingRecord rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_PICTURE_KEY)))
            {
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(imageTag + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_PICTURE_KEY, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_PICTURE_KEY, 0x04);
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(imageTag + "\0");
                rec.cbData = (uint)rec.varData.Length;
            }
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_PERSONAL_PICTURE)))
            {
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(imageTag + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_PERSONAL_PICTURE, rec.ToArray());
            }
            ProfileData pData = ProfileManager.fetchProfile(Package.Header.Metadata.Creator);
            if (pData != null)
                ProfileManager.addProfileToCache(Package, Account);
            pbCurrent.Image = pbSelected.Image;
        }

        private void cmdCreatePack_Click(object sender, EventArgs e)
        {
            if (UI.messageBox("This will close your loaded profile.\n\nContinue?", "Closing Profile",
                MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                Package.CloseIO(true);
                Meta.DeviceIndex = -1;
                Meta.FatxPath = null;
                cmdSaveToDevice.Visible = true;
                hideShowPackageManagerIcon(false);
                panelProfile.Visible = false;
                if (listPics.Items.Count > 0)
                    cmdAddAll.Enabled = true;
                rbPackageEditor.Refresh();
            }
        }

        private void listTitles_ItemActivate(object sender, EventArgs e)
        {
            cmdUseThisGame_Click(null, null);
        }

        private void txtGameName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) && Char.IsDigit(e.KeyChar) && Char.IsWhiteSpace(e.KeyChar) && Char.IsControl(e.KeyChar);
            if ((Keys)e.KeyChar == Keys.Enter)
                cmdSearchForTitle_Click(sender, e);
        }

        private void txtTitleId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) && Char.IsDigit(e.KeyChar) && Char.IsWhiteSpace(e.KeyChar) && Char.IsControl(e.KeyChar);
            if ((Keys)e.KeyChar == Keys.Enter)
                cmdGetGamerpics_Click(sender, e);
        }

        private void cmdClearPics_Click(object sender, EventArgs e)
        {
            if (UI.messageBox("Are you sure you want to clear your current picture pack?",
                "Clear Picture Pack", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel)
                == DialogResult.Yes)
            {
                while (listMyPics.Items.Count > 0)
                    addToPics(0);
                cmdClearPics.Enabled = false;
                cmdAddAll.Enabled  = !panelProfile.Visible;
            }
        }
    }
}
