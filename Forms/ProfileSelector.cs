using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using XContent;
using DevComponents.AdvTree;

namespace Horizon.Forms
{
    public partial class ProfileSelector : Office2007Form
    {
        private FatxHandle.DeviceOperationInjectionParameters i;
        internal ProfileSelector(FatxHandle.DeviceOperationInjectionParameters oP)
        {
            oP.HideSender();
            MdiParent = Main.mainForm;
            InitializeComponent();
            i = oP;
            Text += i.PackageData.Count == 1 ? "this Saved Game" : "these Saved Games";
            Main.mainForm.FatxPanelExpanded = false;
            List<ulong> usedKeys = new List<ulong>();
            List<Node> corruptedNodes = new List<Node>();
            foreach (KeyValuePair<ulong, FatxHandle.ProfileInfo> profile in FatxHandle.Devices[i.ToDeviceIndex].ProfileCache)
            {
                Node node = new Node();
                node.Image = profile.Value.Gamerpic;
                node.Text = profile.Value.Gamertag + FatxHandle.lineBreak + FatxHandle.makeGrayText(profile.Key.ToString("X16"));
                node.Tag = profile.Key;
                if (profile.Value.UnknownOrCorrupted)
                    corruptedNodes.Add(node);
                else
                {
                    usedKeys.Add(profile.Key);
                    listProfiles.Nodes.Add(node);
                }
            }
            foreach (ProfileData profile in ProfileManager.profileData)
                if (profile.Favorite && !usedKeys.Contains(profile.ProfileId))
                {
                    Node node = new Node();
                    node.Image = Image.FromStream(new System.IO.MemoryStream(profile.Gamerpicture));
                    node.Text = profile.Gamertag + FatxHandle.lineBreak + FatxHandle.makeGrayText(profile.ProfileId.ToString("X16"));
                    node.Tag = profile.ProfileId;
                    usedKeys.Add(profile.ProfileId);
                    listProfiles.Nodes.Add(node);
                }
            foreach (Node corruptedNode in corruptedNodes)
                if (!usedKeys.Contains((ulong)corruptedNode.Tag))
                    listProfiles.Nodes.Add(corruptedNode);
            listProfiles.Nodes.Sort();
            listProfiles.SelectedIndex = 0;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            i.ShowSender();
            Close();
        }

        private void cmdSelectProfile_Click(object sender, EventArgs e)
        {
            if (listProfiles.SelectedNodes.Count == 1)
            {
                ulong newProfile = (ulong)listProfiles.SelectedNode.Tag;
                for (int x = 0; x < i.PackageData.Count; x++)
                    if (i.PackageData[x].Package.Header.Metadata.ContentType == ContentTypes.SavedGame
                        && i.PackageData[x].Package.Header.Metadata.VolumeType == XContentVolumeType.STFS_Volume
                        && !i.PackageData[x].Package.StfsContentPackage.VolumeExtension.ReadOnly)
                    {
                        i.PackageData[x].ChangeId = true;
                        i.PackageData[x].NewProfileId = newProfile;
                    }
                cmdSkip_Click(null, null);
            }
        }

        private void listProfiles_NodeDoubleClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            cmdSelectProfile_Click(null, null);
        }

        private void SelectProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (i.Sender == null)
                Main.mainForm.FatxPanelExpanded = true;
            if (!injecting)
            {
                if (i.Sender == null)
                    for (int x = 0; x < i.PackageData.Count; x++)
                        i.PackageData[x].Package.CloseIO(true);
                i.ShowSender();
            }
        }

        private bool injecting = false;
        private void cmdSkip_Click(object sender, EventArgs e)
        {
            if (FatxHandle.injectPackages(i))
            {
                injecting = true;
                Close();
            }
        }
    }
}
