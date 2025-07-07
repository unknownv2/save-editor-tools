using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.FEAR
{
    public partial class FEAR : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "565507D9";

        private FEARClass FEAR_Class { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public FEAR()
        {
            InitializeComponent();
            TitleID = FormID.FEAR;
            //Set our title ID
            
            //Disable our box and button
            textBoxX1.Enabled = false;
            cmdSetValue.Enabled = false;
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile("checkpoint.sav"))
                return false;

            //Initialize our fear class
            FEAR_Class = new FEARClass(IO);
            //Set our level name
            lblName.Text = FEAR_Class.Info_Struct.LevelName;
            //Clear our tree.
            listValues.Nodes.Clear();

            //Loop for each value
            foreach (string key in FEAR_Class.Info_Struct.Values.Keys)
            {
                //Create our node
                Node node = new Node(key);
                //Get our value
                string value = FEAR_Class.Info_Struct.Values[key];
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
            FEAR_Class.Write();
        }

        private void listValues_Click(object sender, EventArgs e)
        {

        }

        private void listValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If our index is above -1
            if (listValues.SelectedIndex > -1)
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
                    textBoxX1.Text = "0" + textBoxX1.Text;
                //Set it
                listValues.SelectedNode.Cells[1].Text = textBoxX1.Text;
                FEAR_Class.Info_Struct.Values[listValues.SelectedNode.Text] = textBoxX1.Text;
            }
        }

        private void panelMain_Click(object sender, EventArgs e)
        {

        }
    }
}
