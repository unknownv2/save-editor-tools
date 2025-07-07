using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Horizon.PackageEditors.Darksiders
{
    public partial class Darksiders : EditorControl
    {
        
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "545107E6";
        private DarksidersClass Darksiders_Class { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public Darksiders()
        {
            InitializeComponent();
            TitleID = FormID.Darksiders;
            //Set our title ID
            
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our save file.
            if (!this.OpenStfsFile(0))
                return false;

            //Initialize our darksiders class
            Darksiders_Class = new DarksidersClass(IO);

            //Our file is read correctly.
            return true;
        }


        public override void Save()
        {
           //Save with our darksiders class
            Darksiders_Class.Write();
        }
    }
}
