using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Brutal_Legend
{
    public partial class BrutalLegend : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "454108C5";
        private BrutalLegendCodeHandler BrutalLegend_Class { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public BrutalLegend()
        {
            InitializeComponent();
            TitleID = FormID.BrutalLegend;
            //Set our title ID
            
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Clear our combobox
            comboFilenames.Items.Clear();
            //Loop for each file entry
            foreach (XContent.StfsDirectoryEntry dE in Package.StfsContentPackage.DirectoryEntries)
                if (!dE.IsDirectory)
                    comboFilenames.Items.Add(dE.FileName);

            //Select our first item
            comboFilenames.SelectedIndex = 0;

            //Our file is read correctly.
            return true;
        }


        public override void Save()
        {
            //Set our data
            BrutalLegend_Class.DATA = BrutalLegend_Class.FormatCodeToString(richTextBox1.Text);
            //Save with our darksiders class
            BrutalLegend_Class.Write(IO);
        }

        private void comboFilenames_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Open our save file.
            if (!this.OpenStfsFile(comboFilenames.SelectedItem.ToString()))
                throw new Exception("Failed to open file: " + comboFilenames.SelectedItem.ToString());

            //Initialize our darksiders class
            BrutalLegend_Class = new BrutalLegendCodeHandler(IO);
            richTextBox1.Text = BrutalLegend_Class.FormatStringToCode(BrutalLegend_Class.DATA);
        }
    }
}
