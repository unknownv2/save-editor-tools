using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Horizon.Properties;
using Horizon.Functions;
using System.IO;
using XContent;
using System.Net;
using System.Globalization;
using DevComponents.AdvTree;
using System.Threading;
using DevComponents.DotNetBar.Controls;
using System.Reflection;
using Horizon.Server;

namespace Horizon
{
    public partial class ProfileManager : Office2007Form
    {
        public ProfileManager()
        {
            activeWindow = this;
            MdiParent = Forms.Main.mainForm;
            InitializeComponent();
            refreshList();
            if (listProfiles.Nodes.Count > 0)
                listProfiles.SelectedIndex = 0;
        }

        internal static ProfileManager activeWindow;
        internal static List<ProfileData> profileData;

        internal static void refreshList()
        {
            if (activeWindow == null)
                return;
            activeWindow.listProfiles.Nodes.Clear();
            foreach (ProfileData profile in profileData)
                activeWindow.listProfiles.Nodes.Add(createProfileNode(profile));
            activeWindow.listProfiles.Nodes.Sort();
        }

        private static byte[] createSerializedDataHeader()
        {
            EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.Out.Write(new byte[]
            { 
                    0x00, 0x01, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF,
                    0xFF, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x0C, 0x02
            });
            Assembly horizon = Assembly.GetCallingAssembly();
            string headerString = String.Format("Horizon, Version={0}, Culture=neutral, PublicKeyToken=6d2e1f41111aca92", Config.clientVersion);
            io.Out.Write(headerString.Length);
            io.Out.Write(Encoding.ASCII.GetBytes(headerString));
            io.Out.Write(new byte[] { 0x04, 0x01 });
            headerString = String.Format("System.Collections.Generic.List`1[[Horizon.ProfileData, Horizon, Version={0}, Culture=neutral, PublicKeyToken=6d2e1f41111aca92]]", Config.clientVersion);
            io.Out.Write(headerString.Length);
            io.Out.Write((byte)0x01);
            io.Out.Write(Encoding.ASCII.GetBytes(headerString));
            byte[] header = io.ToArray();
            io.Close();
            return header;
        }

        internal static void InitializeCache()
        {
            if (Settings.Default.ProfileCache != null && Settings.Default.ProfileCache.Length != 0
                && createCacheHash(Settings.Default.ProfileCache) == Settings.Default.ProfileCacheCheck)
            {
                try
                {
                    byte[] header = createSerializedDataHeader();
                    byte[] data = Convert.FromBase64String(Settings.Default.ProfileCache);
                    MemoryStream ms = new MemoryStream();
                    ms.Write(header, 0, header.Length);
                    ms.Write(data, 0, data.Length);
                    data = ms.ToArray();
                    ms.Close();
                    profileData = (List<ProfileData>)data.ToObject();
                }
                catch { }
            }
            if (profileData == null)
                profileData = new List<ProfileData>();
        }

        internal static string createCacheHash(string cache)
        {
            return (cache + Server.Config.clientSalt + "Cache").Hash(HashType.SHA1);
        }

        internal static void SaveCache()
        {
            byte[] arr = Global.objectToByteArray(profileData);
            int headerLength = 0xDE + (Config.clientVersion.Length * 2);
            byte[] newData = new byte[arr.Length - headerLength];
            Array.Copy(arr, headerLength, newData, 0, newData.Length);
            Settings.Default.ProfileCache = Convert.ToBase64String(newData);
            Settings.Default.ProfileCacheCheck = createCacheHash(Settings.Default.ProfileCache);
            Settings.Default.Save();
        }

        internal static ProfileData fetchProfile(ulong profileId)
        {
            return profileData.Find(profile => profile.ProfileId == profileId);
        }

        internal static ProfileData fetchRealProfile(ulong profileId)
        {
            return profileData.Find(profile => profile.FromDevice && profile.ProfileId == profileId);
        }

        internal static void addProfileToCache(XContentPackage package, XProfile.XProfileAccount account)
        {
            string tagFilter = FatxHandle.filterGamertag(account.Info.GamerTag);
            if (tagFilter != null)
                addProfileToCache(package.Header.Metadata.Creator, tagFilter, account.Info.XuidOnline, package.Header.Metadata.Thumbnail, true);
        }

        internal static void addProfileToCache(ulong profileId, string gamertag, ulong xuid, byte[] gamerpic, bool fromDevice)
        {
            if (profileId != 0)
            {
                ProfileData newData = new ProfileData()
                    {
                        ProfileId = profileId,
                        Gamertag = gamertag,
                        Gamerpicture = gamerpic,
                        XUID = xuid,
                        FromDevice = fromDevice
                    };
                int x = profileData.FindIndex(profile => profile.ProfileId == profileId);
                if (x == -1)
                    newData.Favorite = false;
                else
                {
                    newData.Favorite = profileData[x].Favorite;
                    profileData.RemoveAt(x);
                }
                profileData.Add(newData);
                if (Program.doneLoading && Forms.Main.mainForm.InvokeRequired)
                    Forms.Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        refreshPackageManagers();
                        refreshList();
                    });
                else
                {
                    refreshPackageManagers();
                    refreshList();
                }
            }
        }

        private static void refreshPackageManagers()
        {
            foreach (KeyValuePair<int, FormHandle.FormConfig> fc in FormHandle.Forms)
                if (fc.Value.Meta.ID == FormID.PackageManager
                    && fc.Value.ActiveForm.GetType() == typeof(PackageEditors.Package_Manager.PackageManager))
                    ((PackageEditors.Package_Manager.PackageManager)fc.Value.ActiveForm).refreshProfile();
        }

        private static Node createProfileNode(ProfileData profile)
        {
            Node node = new Node(profile.Gamertag + FatxHandle.lineBreak
                + FatxHandle.makeGrayText(profile.ProfileId.ToString("X16")));
            node.Image = Image.FromStream(new MemoryStream(profile.Gamerpicture));
            CheckBoxItem favBox = new CheckBoxItem();
            favBox.Text = "Favorite";
            favBox.Tag = profile.ProfileId;
            favBox.Checked = profile.Favorite;
            favBox.CheckedChanged += new CheckBoxChangeEventHandler(favBox_CheckedChanged);
            node.Cells.Add(new Cell()
            {
                HostedItem = favBox
            });
            return node;
        }

        static void favBox_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            CheckBoxItem favBox = (CheckBoxItem)sender;
            ulong profileId = (ulong)favBox.Tag;
            for (int x = 0; x < profileData.Count; x++)
                if (profileData[x].ProfileId == profileId)
                {
                    profileData[x].Favorite = favBox.Checked;
                    break;
                }
        }

        internal static bool isFavorite(ulong profileId)
        {
            foreach (ProfileData profile in profileData)
                if (profile.Favorite && profile.ProfileId == profileId)
                    return true;
            return false;
        }

        internal static List<ButtonItem> createFavoriteButtons()
        {
            List<ButtonItem> items = new List<ButtonItem>();
            foreach (ProfileData profile in profileData)
                if (profile.Favorite)
                {
                    ButtonItem button = new ButtonItem();
                    button.Text = profile.Gamertag + FatxHandle.lineBreak
                        + FatxHandle.makeGrayText(profile.ProfileId.ToString("X16"));
                    button.Image = Image.FromStream(new MemoryStream(profile.Gamerpicture));
                    button.ImageFixedSize = new Size(32, 32);
                    button.Tag = profile.ProfileId;
                    button.CanCustomize = false;
                    items.Add(button);
                }
            items.Sort(compareButtonItems);
            return items;
        }

        private static int compareButtonItems(ButtonItem x, ButtonItem y)
        {
            return x.Text.CompareTo(y.Text);
        }

        /*private Thread addThread;
        private void cmdAddProfile_Click(object sender, EventArgs e)
        {
            if (txtGamertag.Text.Length == 0)
            {
                UI.messageBox("Enter a gamertag to add!", "No Gamertag", MessageBoxIcon.Warning);
                return;
            }
            if (txtGamertag.Text[0] == ' ' || txtGamertag.Text[txtGamertag.Text.Length - 1] == ' ')
            {
                UI.errorBox("A gamertag cannot begin or end with a space.");
                return;
            }
            if ((txtGamertag.Text[0] < 'A' || txtGamertag.Text[0] > 'Z')
                && (txtGamertag.Text[0] < 'a' || txtGamertag.Text[0] > 'z'))
            {
                UI.errorBox("A gamertag can only begin with a letter.");
                return;
            }
            char last = '0';
            for (int x = 0; x < txtGamertag.Text.Length; x++)
            {
                char cur = txtGamertag.Text[x];
                if (last == ' ' && cur == ' ')
                {
                    UI.errorBox("A gamertag can't have 2 spaces in a row.");
                    return;
                }
                last = cur;
                if ((cur < 'A' || cur > 'Z') && (cur < 'a' && cur > 'z') && (cur < '0' && cur > '9'))
                {
                    UI.errorBox("A gamertag must only contain alpha-numeric characters!");
                }
            }
            progressAdd.Visible = true;
            (addThread = new Thread((ThreadStart)delegate
            {
                try
                {
                    if (Server.Security.isDebugging() || Server.Security.isRunningBadProcess())
                        throw new Exception();
                    WebClient wb = new WebClient();
                    wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.13 (KHTML, like Gecko) Chrome/9.0.597.98 Safari/534.13");
                    string xboxData = wb.DownloadString("http://gamercard.xbox.com/en-US/MyXbox/Profile?gamertag=" + txtGamertag.Text);
                    ProfileData newData = new ProfileData()
                    {
                        FromDevice = false
                    };
                    newData.ProfileId = ulong.Parse(xboxData.Split(new string[] { "<param name=\"initParams\" value=\"" }, StringSplitOptions.None)[1].Substring(1812, 16), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    ProfileData profile = fetchProfile(newData.ProfileId);
                    if (profile == null)
                        newData.Favorite = false;
                    else
                    {
                        if (UI.messageBox("This account already exists in Horizon.\n\nDo you want to replace it?", "Replace Profile?",
                            MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
                            return;
                        newData.Favorite = profile.Favorite;
                        profileData.Remove(profile);
                    }
                    newData.Gamertag = xboxData.Split("\"profileTitle\">")[1].Split("</h1>")[0];
                    newData.Gamerpicture = wb.DownloadData(wb.DownloadString(Forms.GamercardViewer.baseUrl + "/en-US/"
                        + newData.Gamertag + ".card").Split("\"Gamerpic\" src=\"")[1].Split("\"")[0]);
                    if (activeWindow != null)
                    {
                        profileData.Add(newData);
                        progressAdd.Invoke((MethodInvoker)delegate
                        {
                            refreshList();
                            refreshPackageManagers();
                            progressAdd.Visible = false;
                            txtGamertag.Clear();
                            cmdAddProfile.Focus();
                        });
                        UI.messageBox("Profile added successfully!", "Profile Added", MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    if (activeWindow != null)
                    {
                        progressAdd.Invoke((MethodInvoker)delegate
                        {
                            progressAdd.Visible = false;
                        });
                        UI.errorBox("This gamertag doesn't exist on Xbox LIVE.");
                    }
                }
            })).Start();
        }*/

        private void ProfileManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            activeWindow = null;
        }

        /*private void txtGamertag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                cmdAddProfile_Click(sender, e);
        }*/

        private void cmdClearCache_Click(object sender, EventArgs e)
        {
            if (listProfiles.Nodes.Count == 0)
                UI.messageBox("There are no profiles to clear!", "Empty Cache", MessageBoxIcon.Information);
            else if (UI.messageBox("This will remove all cached profiles from Horizon.\nThe cache will be rebuilt as you load new profiles.\n\nNote that this will NOT remove profiles that you currently have on any loaded device.\n\nContinue?",
                "Clear Profile Cache?", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3)
                == DialogResult.Yes)
            {
                List<ulong> fatxProfiles = new List<ulong>();
                for (int x = 0; x < FatxHandle.Devices.Count; x++)
                    if (FatxHandle.Devices[x].Drive.Mounted)
                        foreach (KeyValuePair<ulong, FatxHandle.ProfileInfo> profile in FatxHandle.Devices[x].ProfileCache)
                            if (!fatxProfiles.Contains(profile.Key))
                                fatxProfiles.Add(profile.Key);
                profileData.RemoveAll(profile => !fatxProfiles.Contains(profile.ProfileId));
                refreshPackageManagers();
                refreshList();
                UI.messageBox("Cache cleared.");
            }
        }
    }

    [Serializable]
    internal class ProfileData
    {
        internal bool FromDevice;
        internal ulong XUID;
        internal string Gamertag;
        internal byte[] Gamerpicture;
        internal ulong ProfileId;
        internal bool Favorite;
    }
}
