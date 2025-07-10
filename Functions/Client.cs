using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.XPath;
using Horizon.Server;
using DevComponents.DotNetBar;
using Horizon.Properties;
using Horizon.Forms;
using System.Diagnostics;

namespace Horizon.Functions
{
    internal static class Client
    {
        internal static bool doUpdate(XPathNavigator nav)
        {
            Program.updateBatch = Global.base64Decode(nav.Value);
            nav.MoveToFirstAttribute();
            Program.updateVersion = nav.Value;
            nav.MoveToNextAttribute();
            #if INT2
                Program.updateURL = null;
                Program.killLoadingLogo();
                nav.MoveToFirstAttribute();
                UI.messageBox(String.Format("Update assembly version to {0}.", Program.updateVersion), "Update Version", MessageBoxIcon.Warning);
                nav.MoveToParent();
                return false;
            #else
                Program.updateURL = nav.Value;
                nav.MoveToParent();
                if (Program.doneLoading)
                {
                    if (UI.messageBox("An update is available for Horizon!\nThe program will have limited functionality until you install this update.\n\nWould you like to update now?", "Horizon v" + Program.updateVersion, MessageBoxIcon.Information, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(Program.updateURL);
                        Application.Exit();
                    }
                }
                else
                    Program.killLoadingLogo();
                return true;
            #endif
        }

        // Triggered by the server. Checks some variables and triggers changeForm.
        internal static bool doLogin(XPathNavigator nav)
        {
            if (nav.Value != (string)Config.getSetting("client_code"))
                return false;
            User.isLogged = true;
            nav.MoveToFirstAttribute();
            User.isDiamond = nav.ValueAsInt == 1;
            nav.MoveToParent();
            changeForm();
            return true;
        }

        // If autologin is enabled, try to login.
        internal static void doAutoLogin()
        {
            if (Settings.Default.AutoLogin)
                if (User.tryLogin(Settings.Default.LoginUsername, decodePassword(Settings.Default.LoginPassword)))
                    Main.mainForm.ckAutoLogin.Checked = User.wasAutoLoggedIn = true;
                else
                {
                    Settings.Default.AutoLogin = false;
                    Settings.Default.Save();
                    Main.mainForm.webAd.Visible = false;
                    Program.killLoadingLogo();
                    UI.messageBox("Auto login failed!", "Login Failed", MessageBoxIcon.Warning);
                }
        }

        internal static void chatLogout()
        {
            if (User.isLogged)
            {
                Request req = new Request("pm");
                req.addParam("do", "logout");
                req.doRequestAsync();
            }
        }

        // Manipulates the UI to the current user settings.
        private static void changeForm()
        {
            Main main = Main.mainForm;
            if (main.rbProfile.Visible = User.isLogged)
            {
                if (main.regForm != null)
                {
                    main.regForm.closeOverride = true;
                    main.regForm.Close();
                }
                main.tabGameMods.Select();
                main.tabLogin.Text = main.txtUsername.Text = (string)Config.getSetting("user_name");
                main.cmdLogin.Text = "Logout";
                if (User.isDiamond)
                {
                    main.tabLogin.Image = Resources.Trophy;
                    main.cmdStatus.ColorTable = eButtonColor.Orange;
                }
                else
                {
                    main.tabLogin.Image = null;
                    main.cmdStatus.ColorTable = eButtonColor.Blue;
                }
            }
            else
            {
                main.tabLogin.Image = null;
                main.tabLogin.Text = Config.siteName + " Login";
                main.cmdLogin.Text = "Login";
                main.cmdStatus.ColorTable = eButtonColor.Blue;
            }
            main.txtUsername.Enabled
                = main.rbRegister.Visible
                = main.rbForgotPassword.Visible
                = main.txtPassword.Enabled
                = !User.isLogged;
            main.cmdStatus.Text = (string)Config.getSetting("user_group");
            main.cmdStatus.Refresh();
            main.ribbonMain.Refresh();
        }

        // Auto Login checkbox checked changed.
        internal static void autoLoginCheck(bool enable)
        {
            if (User.isLogged && enable)
            {
                if (!User.wasAutoLoggedIn)
                {
                    Settings.Default.LoginUsername = Main.mainForm.txtUsername.Text;
                    Settings.Default.LoginPassword = encodePassword(Main.mainForm.txtPassword.Text.Hash(HashType.MD5));
                }
                Settings.Default.AutoLogin = true;
            }
            else
                Settings.Default.AutoLogin = false;
            Settings.Default.Save();
        }

        // Encodes the password to store in the settings.
        internal static string encodePassword(string pass)
        {
            return pass.Base64Encode().ToHexString().Reverse();
        }

        // Decodes a password from a saved auto login.
        internal static string decodePassword(string pass)
        {
            string decoded = null;
            try { decoded = Global.base64Decode(Global.arrayToString(Global.hexStringToArray(Settings.Default.LoginPassword.Reverse()))); }
            catch { } return decoded;
        }

        // Logs the user out and destroys the local session.
        internal static void logOut()
        {
            User.isDiamond = User.isLogged = false;
            FormSettings.clearDictionary();
            Config.removeSetting("session_id");
            Config.removeSetting("chat_vip");
            Config.addSetting("user_group", "Register");
            Main.mainForm.txtPassword.Text = String.Empty;
            Main.mainForm.ckAutoLogin.Checked = false;
            Main.mainForm.webAd.Visible = true;
            changeForm();
        }
    }
}
