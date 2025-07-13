using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DeadRising2;

namespace Horizon.PackageEditors.Dead_Rising_2
{
    public partial class DeadRising2 : EditorControl
    {
        //public static readonly string FID = "434307EC";
        DeadRisingII dr2;

        public DeadRising2()
        {
            InitializeComponent();
            TitleID = FormID.DeadRising2;
            
        }
        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
                return false;
            DeadRisingII.Poly = 3988292384;
            dr2 = new DeadRisingII(this.IO);
            this.Display();

            return true;
        }
        private void Display()
        {
            this.numPP.Value = dr2.PP;
            this.numMoney.Value = dr2.Money;
        }
        public override void Save()
        {
            dr2.Level = 1;
            dr2.Money = numMoney.Value;
            dr2.PP = numPP.Value;

            dr2.Save();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            numPP.Value = numPP.MaxValue;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            numMoney.Value = numMoney.MaxValue;
        }
    }
}
