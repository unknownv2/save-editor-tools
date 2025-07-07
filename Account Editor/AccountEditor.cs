using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XProfile;
using Horizon.Functions;

namespace Horizon.PackageEditors.Account_Editor
{
    public partial class AccountEditor : EditorControl
    {
        public AccountEditor()
        {
            InitializeComponent();
            cbService.Items.Add("Xbox LIVE");
            cbService.Items.Add("PartnerNet");
            cbService.Items.Add("Other...");
            Array codes = Enum.GetValues(typeof(XProfileAccount.XOnlinePassCodeTypes));
            for (int x = 0; x < codes.Length; x++)
            {
                string fCode = getFriendlyPassCode((XProfileAccount.XOnlinePassCodeTypes)codes.GetValue(x));
                cbP1.Items.Add(fCode);
                cbP2.Items.Add(fCode);
                cbP3.Items.Add(fCode);
                cbP4.Items.Add(fCode);
            }
            #if PNET
            txtXuid.ReadOnly = false;
            #endif
        }

        public override bool Entry()
        {
            if (Account == null)
            {
                Functions.UI.errorBox("Account file not found!");
                return false;
            }
            ckDevAccount.Checked = Account.DeveloperAccount;
            loadService();
            txtGamertag.Text = Account.Info.GamerTag;
            txtXuid.Text = Account.Info.XuidOnline.ToString("X16");
            txtDomain.Text = Account.Info.OnlineDomain;
            txtKerberosRealm.Text = Account.Info.OnlineKerberosRealm;
            ckLiveEnabled.Checked = Account.Info.XboxLiveEnabled;
            ckRecovering.Checked = Account.Info.Recovering;
            ckLiveEnabled_CheckedChanged(null, null);
            ckPasswordProtected.Checked = Account.Info.PasswordProtected;
            if (cbService.SelectedIndex == 0 || cbService.SelectedIndex == 1)
                txtService.Enabled = false;
            loadPasscodes();
            return true;
        }

        private void loadService()
        {
            txtService.Text = Encoding.ASCII.GetString(BitConverter.GetBytes(Account.Info.OnlineServiceNetworkId)).Reverse();
            if (txtService.Text == "PROD")
                cbService.SelectedIndex = 0;
            else if (txtService.Text == "PART")
                cbService.SelectedIndex = 1;
            else if (txtService.Text.Length == 0)
                cbService.SelectedIndex = ckDevAccount.Checked ? 1 : 0;
            else
                cbService.SelectedIndex = 2;
        }

        private void loadPasscodes()
        {
            cbP1.Text = getFriendlyPassCode(Account.PassCode[0]);
            cbP2.Text = getFriendlyPassCode(Account.PassCode[1]);
            cbP3.Text = getFriendlyPassCode(Account.PassCode[2]);
            cbP4.Text = getFriendlyPassCode(Account.PassCode[3]);
        }

        private string getFriendlyPassCode(XProfile.XProfileAccount.XOnlinePassCodeTypes code)
        {
            return code == XProfileAccount.XOnlinePassCodeTypes.XOnlinePassCodeNone ? "--" : ((PassCodeTypes)((byte)code)).ToString();
        }

        private enum PassCodeTypes
        {
            NA = 0x00,
            Du = 0x1,
            Dd = 0x2,
            Dl = 0x3,
            Dr = 0x4,
            X = 0x5,
            Y = 0x6,
            LT = 0x9,
            RT = 0xA,
            LB = 0xB,
            RB = 0xC
        };

        private XProfileAccount.XOnlinePassCodeTypes getPassCodeFromString(string code)
        {
            if (code == "--")
                return XProfileAccount.XOnlinePassCodeTypes.XOnlinePassCodeNone;
            return (XProfileAccount.XOnlinePassCodeTypes)Enum.Parse(typeof(PassCodeTypes), code);
        }

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtService.Enabled = cbService.SelectedIndex == 2;
            if (cbService.SelectedIndex == 0)
                txtService.Text = "PROD";
            else if (cbService.SelectedIndex == 1)
                txtService.Text = "PART";
        }

        public override void Save()
        {
            if (cbService.Enabled && txtService.Text.Length != 4)
            {
                UI.throwError("The service ID must be 4 characters!");
                return;
            }
            else if (ckPasswordProtected.Checked
                && (cbP1.SelectedIndex == 0 || cbP2.SelectedIndex == 0
                || cbP3.SelectedIndex == 0 || cbP4.SelectedIndex == 0))
                UI.throwError("You must set all 4 passcodes!");
            Account.Info.GamerTag = txtGamertag.Text;
            Account.DeveloperAccount = ckDevAccount.Checked;
            if (cbService.Enabled)
                Account.Info.OnlineServiceNetworkId = BitConverter.ToUInt32(Encoding.ASCII.GetBytes(txtService.Text.Reverse()), 0);
            else
                Account.Info.OnlineServiceNetworkId = 0;
            Account.Info.XboxLiveEnabled = ckLiveEnabled.Checked;
            Account.Info.Recovering = ckRecovering.Checked;
            Account.Info.PasswordProtected = ckPasswordProtected.Checked;
            Account.Info.XuidOnline = ulong.Parse(txtXuid.Text, System.Globalization.NumberStyles.HexNumber);
            Account.SetPasscode(0, getPassCodeFromString(cbP1.Text));
            Account.SetPasscode(1, getPassCodeFromString(cbP2.Text));
            Account.SetPasscode(2, getPassCodeFromString(cbP3.Text));
            Account.SetPasscode(3, getPassCodeFromString(cbP4.Text));
            Account.Info.OnlineDomain = txtDomain.Text;
            Account.Info.OnlineKerberosRealm = txtKerberosRealm.Text;
            Package.StfsContentPackage.InjectFileFromArray("Account", Account.Save());
            ProfileData pData = ProfileManager.fetchProfile(Package.Header.Metadata.Creator);
            if (pData != null)
                ProfileManager.addProfileToCache(Package, Account);
        }

        private bool enablePasswords(bool enable)
        {
            return cbP1.Enabled = cbP2.Enabled = cbP3.Enabled = cbP4.Enabled = enable;
        }

        private void ckPasswordProtected_CheckedChanged(object sender, EventArgs e)
        {
            if (enablePasswords(ckPasswordProtected.Checked))
                loadPasscodes();
            else
                cbP1.Text = cbP2.Text = cbP3.Text = cbP4.Text = "--";
        }

        private void ckLiveEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (cbService.Enabled = txtService.Enabled = ckRecovering.Enabled = ckLiveEnabled.Checked)
            {
                ckRecovering.Checked = Account.Info.Recovering;
                loadService();
            }
            else
            {
                cbService.SelectedIndex = 2;
                txtService.Text = String.Empty;
                txtService.Enabled = ckRecovering.Checked = false;
            }
        }

        private static bool showWarning = true;
        private void txtGamertag_Click(object sender, EventArgs e)
        {
            if (showWarning)
            {
                UI.messageBox("This will not let you change your online gamertag for free.\nIt only changes the information in the account file.\n\nModifying this information may prohibit you from signing into Xbox LIVE.", "Account Info", MessageBoxIcon.Information);
                showWarning = false;
            }
        }
    }
}
