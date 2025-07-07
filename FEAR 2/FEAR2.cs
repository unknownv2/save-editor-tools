using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horizon.PackageEditors.FEAR2;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.FEAR_2
{
    public partial class FEAR2 : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "575207D3";

        private FEAR2Class FEAR2_Class { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public FEAR2()
        {
            InitializeComponent();
            TitleID = FormID.FEAR2;
            //Set our title ID
            
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile(SettingAsString(102)))
                return false;

            //Set variables used to calculate the checksum from the server
            FEAR2Class.num4 = (uint)SettingAsLong(108);
            FEAR2Class.num7 = (uint)SettingAsInt(128);

            //Initialize our fear class
            FEAR2_Class = new FEAR2Class(IO);

            //Set our level name
            lblName.Text = FEAR2_Class.Info_Struct.LevelName;
            //Clear our tree.
            listValues.Nodes.Clear();

            //Loop for each value
            foreach (string key in FEAR2_Class.Info_Struct.Values.Keys)
            {
                //Create our node
                Node node = new Node(key);
                //Get our value
                string value = FEAR2_Class.Info_Struct.Values[key];
                //Add our value to the column
                node.Cells.Add(new Cell(value));
                //If our value can be parsed as an int or decimal
                if (IsEdittableValue(value))
                    //Add our node
                    listValues.Nodes.Add(node);
            }

            //Auto resize our column.
            listValues.Columns[0].AutoSize();

            //Our file is read correctly.
            return true;
        }
        private bool IsEdittableValue(string value)
        {
            int result1 = 0;
            float result2 = 0;
            return int.TryParse(value, out result1) | float.TryParse(value, out result2);
        }

        public override void Save()
        {
            //Use our FEAR class to save
            FEAR2_Class.Write();
        }

        private void listValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If our index is above -1
            if (listValues.SelectedIndex > SettingAsInt(129))
            {
                //Enable our button and text
                textBoxX1.Enabled = true;
                cmdSetValue.Enabled = true;
                //Set our text
                textBoxX1.Text = listValues.SelectedNode.Cells[1].Text;
                //Set our maximum length of our value
                textBoxX1.MaxLength = textBoxX1.Text.Length;
            }
        }

        private void cmdSetValue_Click(object sender, EventArgs e)
        {
            //If it's an edittable integer
            if (IsEdittableValue(textBoxX1.Text))
            {
                //While our lengths aren't equal
                while (listValues.SelectedNode.Cells[1].Text.Length > textBoxX1.Text.Length)
                    //Add to the front.
                    textBoxX1.Text = SettingAsString(47) + textBoxX1.Text;
                //Set it
                listValues.SelectedNode.Cells[1].Text = textBoxX1.Text;
                FEAR2_Class.Info_Struct.Values[listValues.SelectedNode.Text] = textBoxX1.Text;
            }
        }
    }
}
