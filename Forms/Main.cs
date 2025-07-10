using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml.XPath;
using Horizon.Forms;
using System.Diagnostics;
using Horizon.Server;
using Horizon.Functions;
using Horizon.Properties;
using mshtml;
using Horizon.PackageEditors.Package_Manager;
using System.Net;

namespace Horizon.Forms
{
    internal partial class Main : Office2007RibbonForm
    {
        internal static volatile Main mainForm;
        internal Main(string newsURL)
        {
            InitializeComponent();
            mainForm = this;
            if (newsURL.Length != 0)
                setNewsURL(newsURL);
            loadFormStyle();
            cmdBackups.Checked = Settings.Default.Backup;
            cmdSafeMode.Checked = Settings.Default.SafeMode;
            cmdDock.Checked = Settings.Default.Docked;
            switchDev.Value = Settings.Default.Dev;
            if (!Connection.isOnline)
            {
                cmdStatus.Text = "Offline";
                tabLogin.Visible = false;
                tabGameMods.Select();
            }
            FormConfig.populateForms();
            FormConfig.populateTabs();
            TaskbarManager.setWindowHandle(Handle);
            FatxHandle.initializeHandle();
            if (Connection.isOnline)
                Client.doAutoLogin();
            Program.killLoadingLogo();
            if (!Connection.isOnline)
                cmdStatus_Click(null, null);
            Program.doneLoading = true;
            #if PNET
            switchDev.Visible = true;
            #endif
            #if INT2
            cmdLoadDump.Visible = true;
            cmdLoadUsbDump.Visible = true;
            cmdFatxUnload.Visible = true;
            #endif
            if (FatxHandle.openOnOpen)
                FatxPanelExpanded = true;
            nextClipboardViewer = (IntPtr)ClipboardHelper.SetClipboardViewer(Handle.ToInt32());
            if (User.isLogged)
            {
                webAd.Visible = false;
            }
            //new WebClient().DownloadStringAsync(new Uri(Config.baseURL + "keys.aes"));
            //SaveSharer.Open();
        }

        private IntPtr nextClipboardViewer;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x030D:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        ClipboardHelper.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;
                case 0x308:
                    string currentClip = null;
                    IDataObject clipData = Clipboard.GetDataObject();
                    if (clipData.GetDataPresent(DataFormats.Rtf))
                    {
                        RichTextBox tempTextBox = new RichTextBox();
                        tempTextBox.Rtf = (string)clipData.GetData(DataFormats.Rtf);
                        currentClip = tempTextBox.Text;
                    }
                    else if (clipData.GetDataPresent(DataFormats.Text))
                        currentClip = (string)clipData.GetData(DataFormats.Text);
                    if (currentClip != null)
                        ClipboardHelper.parseClipboardText(currentClip);
                    ClipboardHelper.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        ~Main()
        {
            ClipboardHelper.ChangeClipboardChain(this.Handle, nextClipboardViewer);
        }

        internal void setNewsURL(string base64NewsURL)
        {
            Connection.isOnline = true;
        }

        internal static void doFlashColors()
        {
            Color original = StyleManager.ColorTint;
            foreach (Color color in new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet })
            {
                StyleManager.ColorTint = color;
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            StyleManager.ColorTint = original;
        }

        internal static void doFlash()
        {
            for (byte x = 0; x < 9; x++)
            {
                mainForm.Opacity = 0;
                Application.DoEvents();
                System.Threading.Thread.Sleep(150);
                mainForm.Opacity = 100;
            }
        }

        // Opens your web browser to the link you clicked in the news box.
        private void txtNews_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        // Login button click. Attemps to log you in with a MD5 of your password.
        internal void cmdLogin_Click(object sender, EventArgs e)
        {
            if (User.isLogged)
                Client.logOut();
            else
            {
                txtUsername.Text = txtUsername.Text.Trim();
                if (txtUsername.Text.Length == 0)
                    UI.messageBox("Enter a username!", "No Username", MessageBoxIcon.Error);
                else if (txtPassword.Text.Length == 0)
                    UI.messageBox("Enter a password!", "No Password", MessageBoxIcon.Error);
                else
                {
                    string hashPass = txtPassword.Text.Hash(HashType.MD5);
                    if (User.tryLogin(txtUsername.Text, hashPass))
                    {
                        webAd.Visible = false;
                        Settings.Default.AutoLogin = ckAutoLogin.Checked;
                        if (ckAutoLogin.Checked)
                        {
                            Settings.Default.LoginUsername = txtUsername.Text;
                            Settings.Default.LoginPassword = Client.encodePassword(hashPass);
                        }
                        Settings.Default.Save();
                    }
                    else
                    {
                        UI.messageBox("Username/password combination incorrect!", "Invalid Login", MessageBoxIcon.Warning);
                        txtPassword.Clear();
                    }
                }
            }
        }

        // Hitting the enter key in the password field will click the Login button.
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                cmdLogin_Click(null, null);
        }

        // Blue file button clicked. Register or upgrade to V.I.P.
        internal void cmdStatus_Click(object sender, EventArgs e)
        {
            if (Connection.isOnline)
            {
                if (User.isLogged)
                    Process.Start(Config.serverURL + (User.isDiamond ? "forum/" : "diamond"));
                else
                    cmdRegister_Click(null, null);
            }
            else
                UI.messageBox("Horizon is currently running in offline mode!\nYou have limited access to modding tools.", "Offline Mode", MessageBoxIcon.Warning);
        }

        // Opens the default browser to the forgot password page.
        private void cmdForgotPassword_Click(object sender, EventArgs e)
        {
            Process.Start(Config.serverURL + "login.php?do=lostpw");
        }

        internal Registration regForm = null;
        private void cmdRegister_Click(object sender, EventArgs e)
        {
            /*if (regForm == null || regForm.isHidden)
                (regForm = new Registration(txtUsername.Text)).Show();
            else
            {
                this.BringToFront();
                regForm.BringToFront();
            }*/
            Process.Start(Config.serverURL + "signup.php");
        }

        private void cmdMyProfile_Click(object sender, EventArgs e)
        {
            Process.Start(Config.serverURL + "members/" + ((string)Config.getSetting("user_name")).ToLower() + "/");
        }

        private void ckAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            Client.autoLoginCheck(ckAutoLogin.Checked);
        }

        private void cmdQuickFix_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a STFS Package to Rehash and Resign";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
                fixFiles(ofd.FileNames);
        }

        internal static void fixFiles(string[] fileNames)
        {
            if (fileNames.Length > 0)
            {
                int amountFixed = new int();
                foreach (string fileName in fileNames)
                    if (fixFile(fileName))
                        amountFixed++;
                    else if (Program.doneLoading)
                        UI.messageBox("Not a valid CON STFS package!\n" + fileName, "Invalid", MessageBoxIcon.Error);
                if (!Program.doneLoading)
                    Program.killLoadingLogo();
                if (amountFixed > 0)
                    UI.messageBox("Rehashed and resigned " + amountFixed.ToString() + " file" + ((amountFixed == 1) ? String.Empty : "s") + "!", "Saved", MessageBoxIcon.Information);
            }
        }

        private static bool fixFile(string path)
        {
            XContent.XContentPackage Package = new XContent.XContentPackage();
            if (!Package.LoadPackage(path, false))
                return false;
            if (Package.Header.Metadata.VolumeType == XContent.XContentVolumeType.STFS_Volume
                && Package.Header.SignatureType == XContent.XContentSignatureType.CONSOLE_SIGNED)
            {
                Package.StfsContentPackage.Rehash();
                Package.Flush();
                Package.Save();
                Package.CloseIO(true);
                return true;
            }
            Package.CloseIO(true);
            return false;
        }

        private void loadFormStyle()
        {
            switch (Settings.Default.StyleType)
            {
                case 0:
                    styleMain.ManagerStyle = eStyle.Office2010Silver;
                    styleMain.ManagerColorTint = Color.Black;
                    break;
                case 1:
                    styleMain.ManagerStyle = eStyle.Office2010Black;
                    styleMain.ManagerColorTint = Color.FromArgb(-9211021);
                    break;
                case 2:
                    styleMain.ManagerStyle = eStyle.Office2010Blue;
                    styleMain.ManagerColorTint = Color.Black;
                    break;
            }
        }

        private void cmdRotateTint_Click(object sender, EventArgs e)
        {
            byte curTint = Settings.Default.StyleType;
            curTint++;
            if (curTint == 3)
                curTint = 0;
            Settings.Default.StyleType = curTint;
            Settings.Default.Save();
            loadFormStyle();
        }

        private void switchDev_ValueChanged(object sender, EventArgs e)
        {
            #if PNET
            Config.devTag = switchDev.Value ? "_dev" : String.Empty;
            Settings.Default.Dev = switchDev.Value;
            Settings.Default.Save();
            #endif
        }

        private void panelStatus_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
                panelStatus.Style.BackColor1.Color = Color.Gold;
            }
        }

        private void panelStatus_DragLeave(object sender, EventArgs e)
        {
            panelStatus.Style.BackColor1.ColorSchemePart = eColorSchemePart.PanelBackground;
        }

        private void panelStatus_DragDrop(object sender, DragEventArgs e)
        {
            panelStatus_DragLeave(null, null);
            string[] dataFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dataFiles.Length > 0 && dataFiles[0].Contains(FatxHandle.dragTempFileFilter.Substring(0, 14)))
                UI.messageBox("Use the gear icon to open this file in the package manager.", "Device Explorer", MessageBoxIcon.Warning, MessageBoxButtons.OK);
            else
                fixFiles((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            int failed = 0;
            foreach (string File in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (File.Contains(FatxHandle.dragTempFileFilter.Substring(0, 14)))
                {
                    if (cmdFatxGear.Enabled)
                        FormConfig.formOpenClick(cmdFatxGear, null);
                }
                else
                {
                    XContent.XContentPackage Package = new XContent.XContentPackage();
                    if (Package.LoadPackage(File))
                        FormHandle.initializeNewPackageManager(Package);
                    else
                    {
                        Package.CloseIO(true);
                        failed++;
                    }
                }
            }
            if (failed > 1)
                UI.messageBox("Failed to open " + failed.ToString() + " file" + (failed > 1 ? "s" : String.Empty) + "!", "Package Error", MessageBoxIcon.Error);
        }

        private void cmdSafeMode_Click(object sender, EventArgs e)
        {
            if (!cmdSafeMode.Checked && UI.messageBox("Other modding tools may not save your files correctly.<br></br><br></br>"
                + "If this happens, one of Horizon's editors may fail to mod your package and an error will pop up.<br></br><br></br>"
                + "When Safe Mode is enabled, Horizon will rebuild your packages to prevent these errors from occuring.<br></br>"
                + "The only drawback is that it may take an extra few seconds to open your files into the editors.<br></br><br></br>"
                + "<b>ONLY USE SAFE MODE IF YOU ARE HAVING ISSUES WITH YOUR PACKAGES!</b><br></br><br></br>"
                + "Please note that this does NOT apply to files loaded from a Xbox 360 hard drive, memory card, or flash drive.<br></br><br></br>"
                + "Enable Safe Mode?", "Enable Safe Mode?", MessageBoxIcon.Information, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3)
                == DialogResult.Yes)
                cmdSafeMode.Checked = true;
            else
                cmdSafeMode.Checked = false;
        }

        private void cmdSafeMode_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.doneLoading)
            {
                Settings.Default.SafeMode = cmdSafeMode.Checked;
                Settings.Default.Save();
            }
        }

        private void cmdBackups_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.doneLoading)
            {
                Settings.Default.Backup = cmdBackups.Checked;
                Settings.Default.Save();
            }
        }

        private void cmdBackups_Click(object sender, EventArgs e)
        {
            if (!cmdBackups.Checked && UI.messageBox("Enabling backups will create a copy of your files before saving. This ensures that you will\n"
                + "have no loss of data if an error occurs while writing.\n\n"
                + "The backed up filename will be the same as the original, but with \".bak\" at the end.\n\n"
                + "Please note that this does NOT apply to files loaded from a Xbox 360 hard drive, memory card, or flash drive.\n\n"
                + "Enable Backups?", "Enable Backups?", MessageBoxIcon.Information, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3)
                == DialogResult.Yes)
                cmdBackups.Checked = true;
            else
                cmdBackups.Checked = false;
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            expandContract(true);
        }

        private void cmdContract_Click(object sender, EventArgs e)
        {
            expandContract(false);
        }

        internal void expandContract(bool expand)
        {
            int size = expand ? 25 : -25;
            exFatx.Size = new Size(exFatx.Size.Width + size, exFatx.Size.Height);
            rbFatx.Size = new Size(rbFatx.Size.Width + size, rbFatx.Size.Height);
            progressFatx.Size = new Size(progressFatx.Size.Width + size, progressFatx.Size.Height);
            cmdFatxGear.Size = new Size(cmdFatxGear.Size.Width + size, cmdFatxGear.Size.Height);
            cmdFatxContract.Enabled = exFatx.Size.Width != 375;
            cmdFatxExpand.Enabled = exFatx.Size.Width != 575;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.shuttingDown = true;
            FatxHandle.unloadHandle();
            if (DeviceWindow.Handle != null)
                DeviceWindow.Handle.Close();
        }

        private void cmdFatxDevicesLoaded_Click(object sender, EventArgs e)
        {
            if (Environment.OSVersion.Version.Major > 5)
                UI.messageBox("If you do not see all of your connected devices, be sure you are running Horizon as an Administrator!",
                    "User Account Control", MessageBoxIcon.Information);
        }

        private void cmdDock_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.doneLoading)
            {
                if (cmdDock.Checked)
                {
                    if (DeviceWindow.Handle != null)
                        DeviceWindow.Handle.Close();
                    exFatx.Expanded = true;
                }
                else
                    new DeviceWindow(this).Show();
                Settings.Default.Docked = cmdDock.Checked;
                Settings.Default.Save();
            }
            exFatx.ExpandButtonVisible = cmdDock.Checked;
        }

        internal bool FatxPanelExpanded
        {
            set
            {
                if (cmdDock.Checked)
                    exFatx.Expanded = value;
                else
                    DeviceWindow.Open(this);
            }
        }

        internal static bool fatxExpanded = false;
        private void exFatx_ExpandedChanging(object sender, ExpandedChangeEventArgs e)
        {
            if (e.NewExpandedValue)
            {
                if (cmdDock.Checked)
                    fatxExpanded = true;
                else
                {
                    DeviceWindow.Open(this);
                    e.Cancel = true;
                }
            }
        }
    }
}
