using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MaxPayne;

namespace Horizon.PackageEditors.Max_Payne_3
{
    public partial class MaxPayne3 : EditorControl
    {
        //public static readonly string FID = "5454086B";

        private MP3Save GameSave;

        public MaxPayne3()
        {
            InitializeComponent();
            TitleID = FormID.MaxPayne3;
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("MP3_PROGRESSION"))
                return false;

            this.GameSave = new MP3Save(IO);

            return true;
        }

        public override void Save()
        {
            
        }
    }
}
 