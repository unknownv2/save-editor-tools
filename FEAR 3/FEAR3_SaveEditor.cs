using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.FEAR_3
{
    public partial class FEAR3_SaveEditor : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "57520800";

        private FEAR3Class FEAR3_Class { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public FEAR3_SaveEditor()
        {
            InitializeComponent();
            TitleID = FormID.FEAR3_SaveEditor;
            //Set our title ID
            
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file.
            if (!this.OpenStfsFile(0))
                return false;

            FEAR3_Class = new FEAR3Class(IO);

            //Our file is read correctly.
            return true;
        }


        public override void Save()
        {

        }
    }
}
