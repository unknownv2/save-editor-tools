using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Horizon.Functions;
using System.Diagnostics;
using System.Net;

namespace Horizon.Forms
{
    public partial class GamercardViewer : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public GamercardViewer()
        {
            InitializeComponent();
            MdiParent = Main.mainForm;
        }

        private static string[,] goodNames = 
        {
            { "cheater912", "Cheater912" },
            { "unknown v2", "Unknown" },
            { "modified", "N v The Master" },
            { "ttg sean", "TTG SEAN" },
            { "zrueda", "Zrueda" },
            { "nookie", "NookiesaMillion" },
            { "clk", "Squaill" }
        };

        internal static string baseUrl = "http://gamercard.xbox.com";
        private string currentGamertag;
        private string lastURL = "/en-US/MyXbox/Profile?gamertag=Cheater912";
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            string searchTag = txtGamertag.Text.Trim();
            if (searchTag.Length == 0)
                UI.messageBox("Enter a gamertag!", "No Gamertag", MessageBoxIcon.Warning);
            else
            {
                bool isGood = false;
                for (int x = 0; x < goodNames.Length / 2; x++)
                    if (goodNames[x,0] == searchTag.ToLower())
                    {
                        searchTag = goodNames[x,1];
                        isGood = true;
                        break;
                    }
                string docHtml;
                try
                {
                    docHtml = new WebClient().DownloadString(baseUrl + "/en-US/" + searchTag + ".card");
                }
                catch
                {
                    doesNotExist();
                    return;
                }
                string gamerPic = splitHtml(docHtml, "id=\"Gamerpic\" src=\"", "\"");
                if (gamerPic.Contains("m//"))
                    doesNotExist();
                else
                {
                    currentGamertag = splitHtml(docHtml, "<title>", "<");
                    lastURL = splitHtml(docHtml, "a href=\"", "\"");
                    pbGamerpic.ImageLocation = gamerPic;
                    string avatarLocation = "http://avatar.xboxlive.com/avatar/" + (cmdGamertag.Text = currentGamertag) + "/avatar";
                    pbAvatar.ImageLocation = avatarLocation + "-body.png";
                    pbAvatarSmall.ImageLocation = avatarLocation + "pic-l.png";
                    wbGamercard.Dispose();
                    wbGamercard = new WebBrowser();
                    SuspendLayout();
                    wbGamercard.AllowNavigation = false;
                    wbGamercard.AllowWebBrowserDrop = false;
                    wbGamercard.IsWebBrowserContextMenuEnabled = false;
                    wbGamercard.Location = new Point(11, 90);
                    wbGamercard.MinimumSize = new Size(20, 20);
                    wbGamercard.ScriptErrorsSuppressed = true;
                    wbGamercard.ScrollBarsEnabled = false;
                    wbGamercard.Size = new Size(200, 135);
                    wbGamercard.Url = new Uri(baseUrl + "/en-US/" + searchTag + ".card", UriKind.Absolute);
                    wbGamercard.WebBrowserShortcutsEnabled = false;
                    Controls.Add(this.wbGamercard);
                    ResumeLayout(false);
                    rbGamercardViewer.Refresh();
                    if (isGood)
                        Main.doFlashColors();
                }
            }
        }

        private void doesNotExist()
        {
            UI.messageBox("This gamertag doesn't exist!", "Invalid Gamertag", MessageBoxIcon.Exclamation);
        }

        private static string splitHtml(string doc, string start, string end)
        {
            return doc.Split(start)[1].Split(end)[0];
        }

        private void txtGamertag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                cmdSearch_Click(sender, e);
        }

        private void cmdGamertag_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(lastURL);
            }
            catch
            {

            }
        }

        private void cmdExtractAllImages_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                pbGamerpic.Image.Save(fbd.SelectedPath + @"\" + currentGamertag + " - Gamerpic.png");
                pbAvatarSmall.Image.Save(fbd.SelectedPath + @"\" + currentGamertag + " - AvatarPic.png");
                pbAvatar.Image.Save(fbd.SelectedPath + @"\" + currentGamertag + " - Avatar.png");
                UI.messageBox("Images saved successfully!", "Saved", MessageBoxIcon.Information);
            }
        }
    }
}
