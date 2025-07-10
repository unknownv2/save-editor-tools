using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Homefront;

namespace Horizon.PackageEditors.Homefront
{
    public partial class Homefront : EditorControl
    {
        //public static readonly string FID = "54510846";
        private HomefrontSave HomefrontSave;
        public Homefront()
        {
            InitializeComponent();
            TitleID = FormID.Homefront;
        }
        public override bool Entry()
        {
            if (!OpenStfsFile("FFOWData"))
                return false;

            this.HomefrontSave = new HomefrontSave(this.IO);

            return true;
        }
        public override void Save()
        {
            this.HomefrontSave.Save();
        }
    }
}