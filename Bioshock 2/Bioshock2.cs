using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Bioshock_2
{
    public partial class Bioshock2 : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "54540861";

        /// <summary>
        /// This class handles our Bioshock save.
        /// </summary>
        public Bioshock2Class Bioshock_Class { get; set; }

        /// <summary>
        /// Our default constructor.
        /// </summary>
        public Bioshock2()
        {
            InitializeComponent();
            TitleID = FormID.Bioshock2;
            //Set our title ID
            
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {

            
            //Open our file.
            if (!this.OpenStfsFile(Package.StfsContentPackage.DirectoryEntries[0].FileName + "\\" + "mainSave.bsg"))
                return false;

            //Initialize our class
            Bioshock_Class = new Bioshock2Class(IO);

            //Our file is read correctly.
            return true;
        }

        public override void Save()
        {
            //Save our gamesave
            Bioshock_Class.Base_Info.Credits = 999;
            Bioshock_Class.Write();
        }
    }
}
