using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Capcom;
using DevComponents.DotNetBar;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD.Controls
{
    public partial class CVXSaveEntryControl : UserControl
    {
        private readonly CodeVeronicaXSaveSlot _saveSlot;

        internal CVXSaveEntryControl(CodeVeronicaXSaveSlot saveSlot)
        {
            InitializeComponent();

            cmbCurrentCharacter.Items.AddRange(Enum.GetNames(typeof(CodeVeronicaXCharacters)));

            _saveSlot = saveSlot;

            intClaireHealth.Value = saveSlot.ClaireHealth;
            intChrisHealth.Value = saveSlot.ChrisHealth;
            intSteveHealth.Value = saveSlot.SteveHealth;
            intWeskerHealth.Value = saveSlot.WeskerHealth;

            cmdMaxClaireHealth.Tag = intClaireHealth;
            cmdMaxChrisHealth.Tag = intChrisHealth;
            cmdMaxSteveHealth.Tag = intSteveHealth;
            cmdMaxWeskerHealth.Tag = intWeskerHealth;

            cmbCurrentCharacter.SelectedIndex = (int) saveSlot.CurrentCharacter;
        }

        internal void Write()
        {
            if (_saveSlot.ClaireHealth != intClaireHealth.Value || _saveSlot.ChrisHealth != intChrisHealth.Value ||
                _saveSlot.SteveHealth != intSteveHealth.Value || _saveSlot.WeskerHealth != intWeskerHealth.Value
                ||(CodeVeronicaXCharacters)cmbCurrentCharacter.SelectedIndex != _saveSlot.CurrentCharacter)
                _saveSlot.Modified = true;

            if (!_saveSlot.Modified) return;

            _saveSlot.ClaireHealth = (short) intClaireHealth.Value;
            _saveSlot.ChrisHealth = (short) intChrisHealth.Value;
            _saveSlot.SteveHealth = (short) intSteveHealth.Value;
            _saveSlot.WeskerHealth = (short) intWeskerHealth.Value;
            _saveSlot.CurrentCharacter = (CodeVeronicaXCharacters) cmbCurrentCharacter.SelectedIndex;
        }

        private void BtnClickMaxHealth(object sender, EventArgs e)
        {
            var buttonX = sender as ButtonX;
            if (buttonX == null) return;

            var intInput = (buttonX.Tag as IntegerInput);
            if (intInput == null) return;

            intInput.Value = intInput.MaxValue;

            _saveSlot.Modified = true;
        }

    }
}
