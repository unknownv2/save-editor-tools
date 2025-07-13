using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Need_for_Speed_HP
{
    public partial class NeedForSpeedHP : EditorControl
    {
        //public static readonly string FID = "45410903";
        public NeedForSpeedHP()
        {
            InitializeComponent();
            TitleID = FormID.NeedForSpeedHP;
        }

        private int[][] saveValues = new int[10][];
        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
                return false;
            IO.Stream.Position = 0x1CA14;
            for (byte x = 0; x < 10; x++)
            {
                saveValues[x] = new int[] { IO.In.ReadInt32(), IO.In.ReadInt32() };
                if (saveValues[x][0] == 0x52434254)
                    intRacer.Value = saveValues[x][1];
                else if (saveValues[x][0] == 0x43504254)
                    intCop.Value = saveValues[x][1];
            }
            return true;
        }

        public override void Save()
        {
            IO.Stream.Position = 0x1CA14;
            for (byte x = 0; x < 10; x++)
            {
                if (saveValues[x][0] == 0x52434254)
                    saveValues[x][1] = intRacer.Value;
                else if (saveValues[x][0] == 0x43504254)
                    saveValues[x][1] = intCop.Value;
                IO.Out.Write(saveValues[x][0]);
                IO.Out.Write(saveValues[x][1]);
            }
        }

        private void cmdMax_Click(object sender, EventArgs e)
        {
            intRacer.Value = intRacer.MaxValue;
            intCop.Value = intCop.MaxValue;
        }
    }
}
