using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Naruto_Ultimate_Ninja_Storm_2
{
    public partial class NarutoUltimateNinjaStorm2 : EditorControl
    {
        //public static readonly string FID = "4E4D0819";
        public NarutoUltimateNinjaStorm2()
        {
            InitializeComponent();
            TitleID = FormID.NarutoUltimateNinjaStorm2;
            
        }

        private int ryo = 0x42E4, sp = 0x193E6;
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;
            IO.Stream.Position = ryo;
            intRyo.Value = IO.In.ReadInt32();
            IO.Stream.Position = sp;
            intSP.Value = IO.In.ReadInt32();
            return true;
        }

        public override void Save()
        {
            IO.Stream.Position = ryo;
            IO.Out.Write(intRyo.Value);
            IO.Stream.Position = sp;
            IO.Out.Write(intSP.Value);
        }

        private void cmdMaxRyo_Click(object sender, EventArgs e)
        {
            intRyo.Value = intRyo.MaxValue;
        }

        private void cmdMaxSP_Click(object sender, EventArgs e)
        {
            intSP.Value = intSP.MaxValue;
        }
    }
}
