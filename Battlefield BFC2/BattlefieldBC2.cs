using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ElectronicArts;

namespace Horizon.PackageEditors.Battlefield_BFC2
{
    public partial class BattlefieldBC2 : EditorControl
    {
        private EA SaveHeader;
        private Battlefield.BattlefieldBC2 GameSave;
        //public static readonly string FID = "454108A8";
        public BattlefieldBC2()
        {
            InitializeComponent();
            TitleID = FormID.BattlefieldBC2;
            
        }
        public override bool Entry()
        {
            if (!this.OpenStfsFile("PROF_SAVE"))
            {
                return false;
            }
            this.ReadSaveHeader();

            this.DisplayComponents();

            return true;
        }
        public override void Save()
        {
            this.GameSave.Save();
        }

        private void ReadSaveHeader()
        {
            this.SaveHeader = new EA(this.IO);
            this.GameSave = new Battlefield.BattlefieldBC2(this.IO, this.SaveHeader);
        }
        private void DisplayComponents()
        {
            for (var x = 0; x < this.GameSave.SaveEntries.Count; x++)
            {
                this.comboBoxEx1.Items.Add(this.GameSave.SaveEntries[x].EntryName);
            }
        }

        private void panelMain_Click(object sender, EventArgs e)
        {

        }
    }
}