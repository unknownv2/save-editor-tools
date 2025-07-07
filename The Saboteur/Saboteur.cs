using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;


namespace Horizon.PackageEditors.The_Saboteur
{
    public partial class Saboteur : EditorControl
    {
        private Save.Saboteur GameSave;
        private int FileSize;
        //public static readonly string FID = "4541088F";
        public Saboteur()
        {
            InitializeComponent();
            TitleID = FormID.Saboteur;
            
        }
        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
            {
                return false;
            }
            this.IO.Open();
            this.IO.In.SeekTo(0x08);
            this.FileSize = this.IO.In.ReadInt32();
            this.IO.In.SeekTo(0x858);
            this.GameSave = new Save.Saboteur(new EndianIO(this.IO.In.ReadBytes(0x267A0), EndianType.BigEndian));
            this.LoadFormComponentData();
            return true;
          
        }
        public override void Save()
        {
            this.IO.Out.SeekTo(0x858);
            this.IO.Out.Write(this.GameSave.Save());

            //fix EA's checksum
            this.FixChecksum();
        }
        private void FixChecksum()
        {
            this.IO.In.SeekTo(0x04);
            uint check = ElectronicArts.EACRC32.Calculate_Alt(this.IO.In.ReadBytes(this.FileSize - 4),
                this.FileSize - 4, 0x811C9DC5);
            this.IO.Out.SeekTo(0x00);
            this.IO.Out.Write(check);
        }
        public void LoadFormComponentData()
        {
            this.cmbWeapons.Items.Clear();

            this.numContraband.Value = this.GameSave.Contraband;
            string[] weaponIds = new string[this.GameSave.Save1.Ammunition.Length/2];
            for (int x = new int(); x < this.GameSave.Save1.Ammunition.Length/2; x++)
            {
                string AmmoName = this.GameSave.GetAmmoName(this.GameSave.Save1.Ammunition[x, 0]);
                if (AmmoName != null)
                    weaponIds[x] = AmmoName;
                else weaponIds[x] = this.GameSave.Save1.Ammunition[x, 0].ToString("X");
            }
            this.cmbWeapons.Items.AddRange(weaponIds);
            this.cmbWeapons.SelectedIndex = 0;
        }

        private void cmbWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.numAmmo.Value = this.GameSave.Save1.Ammunition[this.cmbWeapons.SelectedIndex, 1];
        }

        private void numContraband_ValueChanged(object sender, EventArgs e)
        {
            this.GameSave.Contraband = numContraband.Value;
        }

        private void numAmmo_ValueChanged(object sender, EventArgs e)
        {
            this.GameSave.Save1.Ammunition[this.cmbWeapons.SelectedIndex, 1] = (uint)this.numAmmo.Value;
        }
    }
}
