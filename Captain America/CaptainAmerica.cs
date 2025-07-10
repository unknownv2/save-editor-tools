using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CaptainAmerica;

namespace Horizon.PackageEditors.Captain_America
{
    public partial class CaptainAmerica : EditorControl
    {
        //public static readonly string FID = "53450858";
        public CaptainAmerica()
        {
            InitializeComponent();
            TitleID = FormID.CaptainAmerica;
            
        }

        private CaptainAmericaSave save;
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            save = new CaptainAmericaSave();
            save.LoadSave(this.IO);

            integerInput1.Value = save.IntelPoints;

            integerInput2.Value = save.Diaries;

            //integerInput3.Value = save.Eggs;

            return true;
        }

        public override void Save()
        {
            save.IntelPoints = integerInput1.Value;
            save.Diaries = integerInput2.Value;
            //save.Eggs = integerInput3.Value;
            save.WriteSave(this.IO);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            integerInput1.Value = Int32.MaxValue;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            integerInput2.Value = 10;
        }
    }
}