using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horizon.Functions;
using System.Net;

namespace Horizon.Forms
{
    public partial class TitleIDFinder : DevComponents.DotNetBar.Office2007RibbonForm
    {
        internal static string mainURL = "http://marketplace.xbox.com/en-US/";
        public TitleIDFinder()
        {
            InitializeComponent();
            MdiParent = Main.mainForm;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0)
                UI.messageBox("Enter something to search for!", "No Text", MessageBoxIcon.Error);
            else
            {
                if (txtSearch.Text.ToLower() == "modded warfare")
                    Main.doFlash();
                listGames.Items.Clear();
                foreach (ListViewItem title in doSearchTitle(txtSearch.Text))
                    listGames.Items.Add(title);
            }
        }

        private static string fixTitleName(string input)
        {
            return input.Replace("â„¢", String.Empty).Replace("Â", String.Empty).Replace("&amp;", "&&");
        }

        internal static List<ListViewItem> doSearchTitle(string search)
        {
            string uu = mainURL + "Search?query="
                + search.UrlEncode() + "&DownloadType=Game";
            WebClient tidClient = new WebClient();
            tidClient.Encoding = Encoding.UTF8;
            tidClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.13 (KHTML, like Gecko) Chrome/10.0.497.91 Safari/534.13");
            string[] html = tidClient.DownloadString(uu).Split("ProductBox\" href=\"/en-US/Product/");
            List<ListViewItem> titleList = new List<ListViewItem>();
            for (int x = 1; x < html.Length; x++)
            {
                string[] subHTML = html[x].Split('"');
                ListViewItem game = new ListViewItem(fixTitleName(subHTML[2]));
                game.SubItems.Add(subHTML[0].Substring(subHTML[0].Length - 19, 8).ToUpper());
                game.Tag = subHTML[6];
                titleList.Add(game);
            }
            return titleList;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) && Char.IsDigit(e.KeyChar) && Char.IsWhiteSpace(e.KeyChar) && Char.IsControl(e.KeyChar);
            if ((Keys)e.KeyChar == Keys.Enter)
                cmdSearch_Click(sender, e);
        }

        private void pbGameImage_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pbGameImage.Image);
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            if (listGames.SelectedItems.Count == 1)
            {
                Clipboard.Clear();
                Clipboard.SetText(listGames.SelectedItems[0].SubItems[1].Text);
                pbGameImage.ImageLocation = (string)listGames.SelectedItems[0].Tag;
            }
        }
    }
}
