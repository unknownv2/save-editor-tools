using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Test_Drive_Unlimited
{
    public partial class TestDriveUnlimited : EditorControl
    {
        //public static readonly string FID = "494707D4";
        public TestDriveUnlimited()
        {
            InitializeComponent();
            TitleID = FormID.TestDriveUnlimited;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("playersave"))
                return false;
            IO.Stream.Position += 0x0C;
            tabMain.Text = IO.In.ReadAsciiString(IO.In.ReadUInt16()).Replace("\0", String.Empty);
            rbPackageEditor.Refresh();
            numCredits.Value = IO.In.ReadInt32();
            if (!OpenStfsFile("commondt.sav"))
                return false;
            seekToStake();
            numStake.Value = IO.In.ReadInt32();
            return true;
        }

        private void seekToStake()
        {
            IO.Stream.Position = 0x52;
        }

        public override void Save()
        {
            OpenStfsFile("playersave");
            IO.Stream.Position += 0x0C;
            ushort rd = IO.In.ReadUInt16();
            IO.Stream.Position += rd;
            IO.Out.Write(numCredits.Value);
            OpenStfsFile("commondt.sav");
            seekToStake();
            IO.Out.Write(numStake.Value);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            numCredits.Value = numCredits.MaxValue;
            numStake.Value = numStake.MaxValue;
        }
    }
}
