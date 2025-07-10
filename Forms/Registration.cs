using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Horizon.Functions;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Diagnostics;
using Horizon.Server;
using Horizon.Properties;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;

namespace Horizon.Forms
{
    public partial class Registration : Office2007Form
    {
        public Registration(string tempUsername)
        {
            InitializeComponent();
            MdiParent = Main.mainForm;
            cmdRefresh_Click(null, null);
            txtUsername.Text = tempUsername;
            usernameTimer.Interval = 1200;
            usernameTimer.Tick += new EventHandler(usernameTimer_Tick);
            Show();
        }

        private System.Windows.Forms.Timer usernameTimer = new System.Windows.Forms.Timer();
        internal void regCallback(XPathNavigator nav)
        {
            int value = nav.ValueAsInt;
            nav.MoveToFirstAttribute();
            switch (nav.ValueAsInt)
            {
                case 1: // Username check
                    pbUsername.Image = value == 1 ? Resources.GreenDot : Resources.RedDot;
                    break;
                case 2: // Registration
                    switch (value)
                    {
                        case 1: // Invalid username
                            UI.messageBox("Invalid username entered! It may contain illegal characters or it is already in use.", "Invalid Username", MessageBoxIcon.Error);
                            break;
                        case 2:
                            UI.messageBox("This email address is already being used by another member!", "Email Address In Use", MessageBoxIcon.Error);
                            break;
                        case 3: // Too many registrations from the same IP OR invalid MySQL INSERT
                            UI.messageBox("A fatal error has occured while processing your new account! Please try again.", "Fatal Error", MessageBoxIcon.Error);
                            cmdRefresh_Click(null, null);
                            break;
                        case 4: // Registration complete
                            UI.messageBox("Congratulations! Your " + Config.siteName + " account has been set up successfully!\n\nClick OK to login now.", "Registration Complete", MessageBoxIcon.Information);
                            Main.mainForm.txtUsername.Text = txtUsername.Text;
                            Main.mainForm.txtPassword.Text = txtPassword1.Text;
                            Main.mainForm.cmdLogin_Click(null, null);
                            closeOverride = true;
                            Close();
                            break;
                    }
                    break;
            }
            nav.MoveToParent();
        }

        internal bool closeOverride = false;
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            pbPassword.Image = goodPassword() ? Resources.GreenDot : Resources.RedDot;
        }

        private bool goodPassword()
        {
            return (txtPassword1.Text == txtPassword2.Text && txtPassword1.Text.Length >= 5);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            pbEmail.Image = goodEmail() ? Resources.GreenDot : Resources.RedDot;
        }

        private bool goodEmail()
        {
            return (txtEmail.Text == txtEmail2.Text && txtEmail.Text.Length > 5 &&
            new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").IsMatch(txtEmail.Text));
        }

        internal bool isHidden = false;
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            isHidden = !(e.Cancel = !closeOverride && UI.messageBox("Are you sure you want to cancel registration?", "Cancel Registration",
                MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) != DialogResult.Yes);
        }

        private void cmdHumanQuestion_Click(object sender, EventArgs e)
        {
            UI.messageBox("This is used to make sure you're a human and not a robot trying to take over the world!", "Human Verification", MessageBoxIcon.Information);
        }

        private void cmdTheRules_Click(object sender, EventArgs e)
        {
            Process.Start(Config.serverURL + "forum/10-general-discussion/26-rules.html");
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            usernameTimer.Stop();
            pbUsername.Image = goodUsername() ? Resources.GreenDot : Resources.RedDot;
            usernameTimer.Start();
        }

        void usernameTimer_Tick(object sender, EventArgs e)
        {
            usernameTimer.Stop();
            if (goodUsername())
            {
                Request req = new Request("reg");
                req.addParam("r", "user");
                req.addParam("u", txtUsername.Text);
                req.doRequestAsync();
            }
        }

        private bool goodUsername()
        {
            return (txtUsername.Text.Trim().Length > 2 && filterUsername());
        }

        private bool filterUsername()
        {
            for (byte x = 0; x < txtUsername.Text.Length; x++)
                if (!((txtUsername.Text[x] > 96 && txtUsername.Text[x] < 123)
                    || (txtUsername.Text[x] > 64 && txtUsername.Text[x] < 91)
                    || (txtUsername.Text[x] > 47 && txtUsername.Text[x] < 58)
                    || txtUsername.Text[x] == '_' || txtUsername.Text[x] == ' '))
                    return false;
            return true;
        }

        private void cmdRegister_Click(object sender, EventArgs e)
        {
            if (goodUsername() && goodPassword() && goodEmail())
            {
                if (txtHuman.Text.Trim().ToLower().Hash(HashType.SHA256) == lastHash)
                {
                    Request req = new Request("reg");
                    req.addParam("r", "add");
                    req.addParam("u", txtUsername.Text);
                    req.addParam("p", txtPassword1.Text.Hash(HashType.MD5));
                    req.addParam("e", txtEmail.Text);
                    req.doRequest();
                }
                else
                    UI.messageBox("Image verification is incorrect!", "Human Verification Check", MessageBoxIcon.Error);
            }
            else
                UI.messageBox("Some fields have incomplete or invalid values!", "Invalid Form", MessageBoxIcon.Error);
        }

        private string lastHash;
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            string capText = Global.generateCaptchaText();
            lastHash = capText.Hash(HashType.SHA256);
            pbHuman.Image = Global.createCaptchaImage(capText);
        }
    }
}
