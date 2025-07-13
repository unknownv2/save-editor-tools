using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.Bionic_Commando
{
    public partial class BionicCommando : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "434307DD";

        /// <summary>
        /// This class handles our BionicCommando save.
        /// </summary>
        public BionicCommandoClass BionicCommando_Class { get; set; }

        /// <summary>
        /// Our default constructor.
        /// </summary>
        public BionicCommando()
        {
            InitializeComponent();
            TitleID = FormID.BionicCommando;
            //Set our title ID
            
            //Set our textbox to visible
            MakeVisible(txtValue);
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile(0))
                return false;

            //Initialize our BionicCommando
            BionicCommando_Class = new BionicCommandoClass(IO);

            //Clear our list
            listValues.Nodes.Clear();
            //Load our values into our list.
            //Loop for each value
            foreach (BionicCommandoClass.BionicEntry entry in BionicCommando_Class.Values)
            {
                //If our entry only has one value
                if (entry.Values.Count == 1)
                {
                    //Create our node
                    Node node = new Node(entry.Name);
                    //Add our type
                    node.Cells.Add(new Cell(entry.Values[0].Type.ToString()));
                    //If our type is bool
                    if (entry.Values[0].Type == BionicCommandoClass.BionicValue.ValueType.Boolean)
                    {
                        //Add our value
                        if(byte.Parse(entry.Values[0].Value) == 0)
                            node.Cells.Add(new Cell("False"));
                        else
                            node.Cells.Add(new Cell("True"));
                    }
                    else
                    {
                        //Add our value
                        node.Cells.Add(new Cell(entry.Values[0].Value));
                    }
                    //Set it's tag
                    node.Tag = BionicCommando_Class.Values.IndexOf(entry);
                    //Add our node to our values list
                    listValues.Nodes.Add(node);
                }
            }

            //Our file is read correctly.
            return true;
        }

        public override void Save()
        {
            //Save our gamesave
            BionicCommando_Class.Write();
        }

        private void MakeVisible(Control c)
        {
            //Make all our other value constrols invisible.
            txtValue.Visible = false;
            intValue.Visible = false;
            floatValue.Visible = false;
            cmbxValue.Visible = false;
            //If our control is set
            if(c != null)
                //Set our control to visible
                c.Visible = true;
        }

        private void listValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If our selected index is valid
            if (listValues.SelectedIndex > -1)
            {
                //Get our value
                BionicCommandoClass.BionicValue val = BionicCommando_Class.Values[(int)listValues.SelectedNode.Tag].Values[0];
                //Determine our control
                switch (val.Type)
                {
                    case BionicCommandoClass.BionicValue.ValueType.Boolean:
                        //Set our data
                        MakeVisible(cmbxValue);
                        cmbxValue.SelectedIndex = int.Parse(val.Value);
                        break;
                    case BionicCommandoClass.BionicValue.ValueType.Float:
                        //Set our data
                        MakeVisible(floatValue);
                        floatValue.Value = float.Parse(val.Value);
                        break;
                    case BionicCommandoClass.BionicValue.ValueType.Integer:
                        //Set our data
                        MakeVisible(intValue);
                        intValue.Value = int.Parse(val.Value);
                        break;
                    case BionicCommandoClass.BionicValue.ValueType.Text:
                        //Set our data
                        MakeVisible(txtValue);
                        txtValue.Text = val.Value;
                        break;
                }
            }
        }

        private void cmdSetValue_Click(object sender, EventArgs e)
        {
            //If our selected index is valid
            if (listValues.SelectedIndex > -1)
            {
                //Get our value
                BionicCommandoClass.BionicValue val = BionicCommando_Class.Values[(int)listValues.SelectedNode.Tag].Values[0];
                //Determine our control
                switch (val.Type)
                {
                    case BionicCommandoClass.BionicValue.ValueType.Boolean:
                        //Set our data
                        val.Value = cmbxValue.SelectedIndex.ToString();
                        listValues.SelectedNode.Cells[2].Text = cmbxValue.SelectedItem.ToString();
                        break;
                    case BionicCommandoClass.BionicValue.ValueType.Float:
                        //Set our data
                        val.Value = floatValue.Value.ToString();
                        listValues.SelectedNode.Cells[2].Text = val.Value;
                        break;
                    case BionicCommandoClass.BionicValue.ValueType.Integer:
                        //Set our data
                        val.Value = intValue.Value.ToString();
                        listValues.SelectedNode.Cells[2].Text = val.Value;
                        break;
                    case BionicCommandoClass.BionicValue.ValueType.Text:
                        //Set our data
                        val.Value = txtValue.Text;
                        listValues.SelectedNode.Cells[2].Text = val.Value;
                        break;
                }
            }
        }
  
    }
}
