using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Test_Drive_Unlimited_2
{
    public partial class TestDriveUnlimited2 : EditorControl
    {
        //public static readonly string FID = "49470804";
        public TestDriveUnlimited2()
        {
            InitializeComponent();
            TitleID = FormID.TestDriveUnlimited2;
            
        }

        private int moneyOffset = 0x4362;
        private int pointsOffset = 0x4813;
        private int casinoOffset = 0x49b8;
        public override bool Entry()
        {
            if (!OpenStfsFile("DATA"))
                return false;
            IO.Stream.Position = moneyOffset;
            intMoney.Value = IO.In.ReadInt32();
            IO.Stream.Position = pointsOffset;
            intCompetitionPoints.Value = IO.In.ReadInt32();
            intCollectionPoints.Value = IO.In.ReadInt32();
            intSocialPoints.Value = IO.In.ReadInt32();
            intIbiza1.Value = IO.In.ReadInt32();
            intIbiza2.Value = IO.In.ReadInt32();
            intHawaii1.Value = IO.In.ReadInt32();
            intHawaii2.Value = IO.In.ReadInt32();
            intHawaii3.Value = IO.In.ReadInt32();
            intHawaii4.Value = IO.In.ReadInt32();
            IO.Stream.Position = casinoOffset;
            intCasinoChips.Value = IO.In.ReadInt32();
            return true;
        }

        public override void Save()
        {
            IO.Stream.Position = moneyOffset;
            IO.Out.Write(intMoney.Value);
            IO.Stream.Position = pointsOffset;
            IO.Out.Write(intCompetitionPoints.Value);
            IO.Out.Write(intCollectionPoints.Value);
            IO.Out.Write(intSocialPoints.Value);
            IO.Out.Write(intIbiza1.Value);
            IO.Out.Write(intIbiza2.Value);
            IO.Out.Write(intHawaii1.Value);
            IO.Out.Write(intHawaii2.Value);
            IO.Out.Write(intHawaii3.Value);
            IO.Out.Write(intHawaii4.Value);
            IO.Stream.Position = casinoOffset;
            IO.Out.Write(intCasinoChips.Value);
            if (cmdUnlockAllPlaces.Checked || cmdLockAllPlaces.Checked)
            {
                IO.Stream.Position = 0x5df8;
                int lockFlags = (int)(cmdLockAllPlaces.Checked ? 0x00 : 0x7fff);
                for (int x = 0; x < 0x2cd0; x++)
                    IO.Out.Write(lockFlags);
            }
        }

        private void cmdMaxMoney_Click(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
        }

        private void cmdMaxCasinoChips_Click(object sender, EventArgs e)
        {
            intCasinoChips.Value = intCasinoChips.MaxValue;
        }

        private void cmdMaxPoints_Click(object sender, EventArgs e)
        {
            intCollectionPoints.Value
                = intCompetitionPoints.Value
                = intSocialPoints.Value
                = intSocialPoints.MaxValue;
        }

        private void cmdMaxIbiza_Click(object sender, EventArgs e)
        {
            intIbiza1.Value = intIbiza2.Value = intIbiza2.MaxValue;
        }

        private void cmdMaxHawaii_Click(object sender, EventArgs e)
        {
            intHawaii1.Value
                = intHawaii2.Value
                = intHawaii3.Value
                = intHawaii4.Value
                = intHawaii4.MaxValue;
        }

        private void cmdUnlockAllPlaces_CheckedChanged(object sender, EventArgs e)
        {
            if (cmdUnlockAllPlaces.Checked)
                cmdLockAllPlaces.Checked = false;
        }

        private void cmdLockAllPlaces_CheckedChanged(object sender, EventArgs e)
        {
            if (cmdLockAllPlaces.Checked)
                cmdUnlockAllPlaces.Checked = false;
        }
    }
}
