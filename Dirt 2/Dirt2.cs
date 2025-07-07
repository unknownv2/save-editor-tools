using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dirt2;

namespace Horizon.PackageEditors.Dirt_2
{
    public partial class Dirt2 : EditorControl
    {
        //public static readonly string FID = "434D0819";
        private Dirt2Save Dirt2Save;
        public Dirt2()
        {
            InitializeComponent();
            TitleID = FormID.Dirt2;
            
        }
        public override bool Entry()
        {
            if (!this.OpenStfsFile("Autosave\\career manager sdu name"))
                return false;

            Dirt2Save.AESKEY = new byte[]
                                   {
                                       0xBF, 0x23, 0x8E, 0x52, 0x20, 0x82, 0x61, 0xB1,
                                       0x1F, 0xB5, 0x09, 0x01, 0xE7, 0x8E, 0x45, 0xAC,
                                       0x46, 0x60, 0x15, 0x35, 0x65, 0xF0, 0x92, 0x95,
                                       0x30, 0x54, 0x84, 0xE1, 0xF0, 0x51, 0x66, 0xEC
                                   };

            Dirt2Save = new Dirt2Save(IO.In);

            this.DisplayComponents();

            return true;
        }
        private void DisplayComponents()
        {
            intBalance.Value = this.Dirt2Save.Balance;
        }
        public override void Save()
        {
            this.Dirt2Save.Balance = intBalance.Value;

            IO.Stream.Position = SettingAsInt(237);
            this.IO.Out.Write(Dirt2Save.Save());
        }

        private void cmdMaxBalance_Click(object sender, EventArgs e)
        {
            intBalance.Value = intBalance.MaxValue;
        }
    }
}
