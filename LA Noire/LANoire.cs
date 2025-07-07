using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Horizon.PackageEditors.LA_Noire
{
    public partial class LANoire : EditorControl
    {
        //public static readonly string FID = "5454086C";
        public LANoire()
        {
            InitializeComponent();
            TitleID = FormID.LANoire;
        }

        private void LANoire_Load(object sender, EventArgs e)
        {

        }
    }
}