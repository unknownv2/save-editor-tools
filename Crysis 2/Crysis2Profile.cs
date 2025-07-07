using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Crysis2;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Crysis_2
{
    public partial class Crysis2Profile : EditorControl
    {
        //public static readonly string FID = "454108E3";
        private Profile Profile;
        private uint[] DataSize = new uint[3] { 0x3E0, 0x3E0, 0x3E0 };

        public Crysis2Profile()
        {
            InitializeComponent();
            TitleID = FormID.Crysis2Profile;
            
        }

        public override bool Entry()
        {
            if (!loadAllTitleSettings(EndianType.BigEndian))
                return false;

            Crysis2.Profile.typeTable = SettingAsByteArray(85);

            this.Profile = new Crysis2.Profile(this.IO, SettingAsByteArray(36));

            this.DisplayStatistics();

            return true;
        }

        private void DisplayStatistics()
        {
            this.intXp.Value = (int)this.Profile.PlayerStatistics["XP"];
            this.intPower.Value = (int)this.Profile.PlayerStatistics["Power"];
            this.intArmor.Value = (int)this.Profile.PlayerStatistics["Armor"];
            this.intStealth.Value = (int)this.Profile.PlayerStatistics["Stealth"];

            this.intPowerModuleUnlocks.Value = (int)this.Profile.PlayerStatistics["PowerModuleUnlocks"];
            this.intArmorModuleUnlocks.Value = (int)this.Profile.PlayerStatistics["ArmorModuleUnlocks"];
            this.intStealthModuleUnlocks.Value = (int)this.Profile.PlayerStatistics["StealthModuleUnlocks"];
            this.intWeaponUnlocks.Value = (int)this.Profile.PlayerStatistics["WeaponUnlocks"];
            this.intClassUnlocks.Value = (int)this.Profile.PlayerStatistics["ClassUnlocks"];
            this.intDogTagUnlocks.Value = (int)this.Profile.PlayerStatistics["DogTagUnlocks"];

            this.intFelineAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["FelineAttachmentUnlocks"];
            this.intKVoltAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["KVoltAttachmentUnlocks"];
            this.intScarAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["ScarAttachmentUnlocks"];
            this.intGrendelAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["GrendelAttachmentUnlocks"];
            this.intScarabAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["ScarabAttachmentUnlocks"];
            this.intDSG1AttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["Dsg1AttachmentUnlocks"];
            this.intM2014GaussAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["Ms2014GaussAttachmentUnlocks"];
            this.intJackalAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["JackalAttachmentUnlocks"];
            this.intMarshallAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["MarshallAttachmentUnlocks"];
            this.intMK60Mod0AttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["MK60Mod0AttachmentUnlocks"];

            this.intM12NovaAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["M12NovaAttachmentUnlocks"];
            this.intHammerAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["HammerAttachmentUnlocks"];
            this.intAY69AttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["AY69AttachmentUnlocks"];
            this.intMajesticAttachmentUnlocks.Value = (int)this.Profile.PlayerStatistics["MajesticAttachmentUnlocks"];
        }

        public override void Save()
        {
            this.Profile.PlayerStatistics["XP"] = (uint)this.intXp.Value;
            this.Profile.PlayerStatistics["Power"] = (uint)this.intPower.Value;
            this.Profile.PlayerStatistics["Armor"] = (uint)this.intArmor.Value;
            this.Profile.PlayerStatistics["Stealth"] = (uint)this.intStealth.Value;

            this.Profile.PlayerStatistics["PowerModuleUnlocks"] = (uint)this.intPowerModuleUnlocks.Value;
            this.Profile.PlayerStatistics["ArmorModuleUnlocks"] = (uint)this.intArmorModuleUnlocks.Value;
            this.Profile.PlayerStatistics["StealthModuleUnlocks"] = (uint)this.intStealthModuleUnlocks.Value;
            this.Profile.PlayerStatistics["WeaponUnlocks"] = (uint)this.intWeaponUnlocks.Value;
            this.Profile.PlayerStatistics["ClassUnlocks"] = (uint)this.intClassUnlocks.Value;
            this.Profile.PlayerStatistics["DogTagUnlocks"] = (uint)this.intDogTagUnlocks.Value;

            this.Profile.PlayerStatistics["FelineAttachmentUnlocks"] = (uint)this.intFelineAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["KVoltAttachmentUnlocks"] = (uint)this.intKVoltAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["ScarAttachmentUnlocks"] = (uint)this.intScarAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["GrendelAttachmentUnlocks"] = (uint)this.intGrendelAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["ScarabAttachmentUnlocks"] = (uint)this.intScarabAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["Dsg1AttachmentUnlocks"] = (uint)this.intDSG1AttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["Ms2014GaussAttachmentUnlocks"] = (uint)this.intM2014GaussAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["JackalAttachmentUnlocks"] = (uint)this.intJackalAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["MarshallAttachmentUnlocks"] = (uint)this.intMarshallAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["MK60Mod0AttachmentUnlocks"] = (uint)this.intMK60Mod0AttachmentUnlocks.Value;

            this.Profile.PlayerStatistics["M12NovaAttachmentUnlocks"] = (uint)this.intM12NovaAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["HammerAttachmentUnlocks"] = (uint)this.intHammerAttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["AY69AttachmentUnlocks"] = (uint)this.intAY69AttachmentUnlocks.Value;
            this.Profile.PlayerStatistics["MajesticAttachmentUnlocks"] = (uint)this.intMajesticAttachmentUnlocks.Value;

            writeAllTitleSettings(this.Profile.Save(), DataSize);
        }

        private void cmdMax_Click(object sender, EventArgs e)
        {
            Control parentControl = ((Control)sender).Parent;
            for (int x = 0; x < parentControl.Controls.Count; x++)
                if (parentControl.Controls[x].GetType() == typeof(IntegerInput))
                    ((IntegerInput)parentControl.Controls[x]).Value = ((IntegerInput)parentControl.Controls[x]).MaxValue;
        }

        private void cmdNotice_Click(object sender, EventArgs e)
        {
            Functions.UI.messageBox("If your stats do not get updated, try joining and leaving an online match.\n\n"
                + "Your experience compliments your module values. If you max out your XP, you MUST max out the modules as well!\n\n"
                + "A single online game must be played for the leaderboards to update. You can quit right when the match starts.",
                "Crysis 2 Notice", MessageBoxIcon.Information, MessageBoxButtons.OK);
        }
    }
}
