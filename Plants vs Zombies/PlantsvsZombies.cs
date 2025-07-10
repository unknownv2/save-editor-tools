using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Horizon.PackageEditors.Plants_vs_Zombies
{
    public partial class PlantsvsZombies : EditorControl
    {
        //public static readonly string FID = "584109FF";
        public PlantsvsZombies()
        {
            InitializeComponent();
            TitleID = FormID.PlantsvsZombies;
            
        }

        private int sunPosition = 0x5443;
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;
            IO.Stream.Position = sunPosition;
            intSuns.Value = IO.In.ReadInt32();
            return true;
        }

        public override void Save()
        {
            IO.Stream.Position = sunPosition;
            IO.Out.Write(intSuns.Value);
            IO.Stream.Position = 20;
            byte[] sha1 = new SHA1CryptoServiceProvider().ComputeHash(IO.Stream);
            IO.Stream.Position = 0;
            IO.Out.Write(sha1);
        }

        private void cmdMax_Click(object sender, EventArgs e)
        {
            intSuns.Value = intSuns.MaxValue;
        }
    }
}
