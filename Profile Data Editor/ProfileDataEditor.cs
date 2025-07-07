using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XboxDataBaseFile;
using Horizon.Properties;

namespace Horizon.PackageEditors.Profile_Data_Editor
{
    public partial class ProfileDataEditor : EditorControl
    {
        //public static readonly string FID = "PROFILE";
        public ProfileDataEditor()
        {
            InitializeComponent();
            cbZone.Items.Add("None");
            cbZone.Items.Add("Recreation");
            cbZone.Items.Add("Pro");
            cbZone.Items.Add("Family");
            cbZone.Items.Add("Underground");
            cbZone.SelectedIndex = 1;
        }

        ProfileFile Profile;
        private bool shown = true;
        public override bool Entry()
        {
            Profile = new ProfileFile(Package, 0xfffe07d1);
            Profile.Read();
            SettingRecord rec = new SettingRecord();
            cbZone.SelectedIndex = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_ZONE))
                ? rec.nData : 1;
            txtMotto.Text = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_MOTTO))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : String.Empty;
            txtName.Text = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_NAME))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : String.Empty;
            txtLocation.Text = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_LOCATION))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : String.Empty;
            txtBio.Text = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_BIO))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : String.Empty;
            numRep.Value = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_REP))
                ? (decimal)rec.fData : 100;
            pushCrown(rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_TENURE_LEVEL))
                ? rec.nData : 0);
            lblGamertag.Text = Account == null ? "Unknown" : Account.Info.GamerTag;
            shown = false;
            return true;
        }

        public override void Save()
        {
            SettingRecord rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_ZONE)))
            {
                rec.nData = cbZone.SelectedIndex;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_ZONE, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_ZONE, 0x01);
                rec.nData = rec.nData = cbZone.SelectedIndex;
                Profile.SettingsTracker.CreateSetting((ulong)XProfileIds.XPROFILE_GAMERCARD_ZONE, rec.ToArray());
            }

            rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_MOTTO)))
            {
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtMotto.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_MOTTO, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_MOTTO, 0x04);
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtMotto.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.CreateSetting((ulong)XProfileIds.XPROFILE_GAMERCARD_MOTTO, rec.ToArray());
            }

            rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_NAME)))
            {
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtName.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_USER_NAME, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_USER_NAME, 0x04);
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtName.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.CreateSetting((ulong)XProfileIds.XPROFILE_GAMERCARD_USER_NAME, rec.ToArray());
            }

            rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_LOCATION)))
            {
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtLocation.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_USER_LOCATION, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_USER_LOCATION, 0x04);
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtLocation.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.CreateSetting((ulong)XProfileIds.XPROFILE_GAMERCARD_USER_LOCATION, rec.ToArray());
            }

            rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_BIO)))
            {
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtBio.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_USER_BIO, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_USER_BIO, 0x04);
                rec.varData = UnicodeEncoding.BigEndianUnicode.GetBytes(txtBio.Text + "\0");
                rec.cbData = (uint)rec.varData.Length;
                Profile.SettingsTracker.CreateSetting((ulong)XProfileIds.XPROFILE_GAMERCARD_USER_BIO, rec.ToArray());
            }

            rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_REP)))
            {
                rec.fData = (float)numRep.Value;
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_REP, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_REP, 0x05);
                rec.fData = (float)numRep.Value;
                Profile.SettingsTracker.CreateSetting((ulong)XProfileIds.XPROFILE_GAMERCARD_REP, rec.ToArray());
            }

            rec = new SettingRecord();
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_TENURE_LEVEL)))
            {
                rec.nData = getCrown();
                Profile.SettingsTracker.WriteSetting(XProfileIds.XPROFILE_GAMERCARD_TENURE_LEVEL, rec.ToArray());
            }
            else
            {
                rec = new SettingRecord((uint)XProfileIds.XPROFILE_GAMERCARD_TENURE_LEVEL, 0x01);
                rec.nData = getCrown();
                Profile.SettingsTracker.CreateSetting((ulong)XProfileIds.XPROFILE_GAMERCARD_TENURE_LEVEL, rec.ToArray());
            }
        }

        private void crownClick(object sender, EventArgs e)
        {
            pushCrown(((PictureBox)sender).TabIndex);
        }

        private void pushCrown(int crown)
        {
            foreach (Control pb in panelCrowns.Controls)
                ((PictureBox)pb).BorderStyle = (pb.TabIndex == crown && ((PictureBox)pb).BorderStyle == BorderStyle.None) ? BorderStyle.Fixed3D : BorderStyle.None;
        }

        private int getCrown()
        {
            foreach (Control pb in panelCrowns.Controls)
                if (((PictureBox)pb).BorderStyle == BorderStyle.Fixed3D)
                    return pb.TabIndex;
            return 0;
        }

        private void numRep_ValueChanged(object sender, EventArgs e)
        {
            if (!shown)
            {
                Functions.UI.messageBox("Note: Your reputation will NOT sync with the XBL servers!", "Won't Sync", MessageBoxIcon.Information);
                shown = true;
            }
        }
    }
}
