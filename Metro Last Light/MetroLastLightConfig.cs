using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horizon.PackageEditors.Metro_2033.Controls;

namespace Horizon.PackageEditors.Metro_Last_Light
{
    public partial class MetroLastLightConfig : EditorControl
    {
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public MetroLastLightConfig()
        {
   
            InitializeComponent();
            TitleID = FormID.MetroLastLight;
        }


        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file.
            if (!this.OpenStfsFile("user.cfg"))
                return false;

            //Read all the text into a string
            string data = IO.In.ReadAsciiString((int)IO.In.BaseStream.Length);
            //Load our setting list
            this.metroUserConfigControl1.Value = data;
            //Our file is read correctly.
            return true;
        }

        public override void Save()
        {
            string data = this.metroUserConfigControl1.Value.Replace("\n", "\r\n");
            IO.Out.BaseStream.Position = 0;
            //Write our data
            IO.Out.WriteAsciiString(data, data.Length);
            //Set our data length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }
    }
}
