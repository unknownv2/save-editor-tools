using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Horizon.PackageEditors.Quake_Arena_Arcade
{
    public partial class QuakeArenaArcade : EditorControl
    {
        //public static readonly string FID = "584108C8";
        public QuakeArenaArcade()
        {
            InitializeComponent();
            TitleID = FormID.QuakeArenaArcade;
            
        }

        public override void enablePanels(bool enable)
        {
            panelMain.Enabled = enable;
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;
            IO.Stream.Position = 16;
            txtConfig.Text = Encoding.ASCII.GetString(QuakeArenaArcadeSave.Decrypt(IO.In.ReadBytes(3200))).Replace("\0", String.Empty);
            return true;
        }

        public override void Save()
        {
            IO.Stream.Position = 16;
            byte[] dec = new byte[3200];
            Encoding.ASCII.GetBytes(txtConfig.Text, 0, txtConfig.Text.Length, dec, 0);
            IO.Out.Write(QuakeArenaArcadeSave.Encrypt(dec));
        }
    }
}
