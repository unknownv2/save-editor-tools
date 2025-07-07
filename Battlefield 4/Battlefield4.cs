using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ElectronicArts;
using DevComponents.AdvTree;
using System.IO;

namespace Horizon.PackageEditors.Battlefield_4
{
    public partial class Battlefield4 : EditorControl
    {
        private Battlefield4Class GameSave;
        //public static readonly string FID = "454108A8";
        public Battlefield4()
        {
            InitializeComponent();
            TitleID = FormID.Battlefield4;
            
        }
        public override bool Entry()
        {
            if (!this.OpenStfsFile("PROFSAVE"))
            {
                return false;
            }

            // Read our gamesave
            GameSave = new Battlefield4Class(IO);

            // Load our value categories.
            comboCategory.Items.Clear();
            for (int x = 0; x < GameSave.SaveEntries.Length; x++)
                if (GameSave.SaveEntries[x].Length > 0 && ((Battlefield4Class.SaveEntryCategory)x) != Battlefield4Class.SaveEntryCategory.Unknown1)
                    comboCategory.Items.Add(((Battlefield4Class.SaveEntryCategory)x).ToString().Replace('_',' '));
            
            // Select our first item.
            if (comboCategory.Items.Count > 0)
                comboCategory.SelectedIndex = 0;

            return true;
        }
        public override void Save()
        {
            GameSave.Write();
        }

        private int GetSaveEntryIndexFromCurrentCategory()
        {
            var category = (Battlefield4Class.SaveEntryCategory)Enum.Parse(typeof(Battlefield4Class.SaveEntryCategory), comboCategory.SelectedItem.ToString().Replace(' ', '_'));
            return (int)category;
        }
        private void comboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get our index for our category
            int categoryIndex = this.GetSaveEntryIndexFromCurrentCategory();
            listValues.Nodes.Clear();
            foreach (Battlefield4Class.SaveEntry saveEntry in GameSave.SaveEntries[categoryIndex])
            {
                // If it's a data value, skip it
                if (saveEntry.EntryType == Battlefield4Class.SaveEntry.SaveEntryType.Data)
                    continue;

                // Add our item to the list of values
                Node node = new Node(saveEntry.EntryName);
                node.Cells.Add(new Cell(saveEntry.EntryType.ToString()));
                node.Cells.Add(new Cell(saveEntry.EntryValue.ToString()));
                listValues.Nodes.Add(node);
            }
        }
        private void MakeVisible(Control c)
        {
            //Make all our other value constrols invisible.
            txtValue.Visible = false;
            intValue.Visible = false;
            floatValue.Visible = false;
            //If our control is set
            if (c != null)
                //Set our control to visible
                c.Visible = true;
        }

        private void listValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get our current category index
            int categoryIndex = GetSaveEntryIndexFromCurrentCategory();

            //If our selected index is valid
            if (listValues.SelectedIndex > -1)
            {
                // Get our value
                Battlefield4Class.SaveEntry val = GameSave.SaveEntries[categoryIndex][listValues.SelectedNode.Index];
                
                // Determine our control to display
                switch (val.EntryType)
                {
                    case Battlefield4Class.SaveEntry.SaveEntryType.Float:
                        MakeVisible(floatValue);
                        floatValue.Value = (float)val.EntryValue;
                        break;
                    case Battlefield4Class.SaveEntry.SaveEntryType.Integer:
                        //Set our data
                        MakeVisible(intValue);
                        intValue.Value = (int)val.EntryValue;
                        break;
                    case Battlefield4Class.SaveEntry.SaveEntryType.String:
                        //Set our data
                        MakeVisible(txtValue);
                        txtValue.Text = (string)val.EntryValue;
                        break;
                }
            }
        }

        private void cmdSetValue_Click(object sender, EventArgs e)
        {
            // Get our current category index
            int categoryIndex = GetSaveEntryIndexFromCurrentCategory();

            // Make sure we have a valid item selected.
            if (listValues.SelectedIndex > -1)
            {
                // Get our value
                Battlefield4Class.SaveEntry val = GameSave.SaveEntries[categoryIndex][listValues.SelectedNode.Index];
                
                // Determine how to save changes.
                switch (val.EntryType)
                {
                    case Battlefield4Class.SaveEntry.SaveEntryType.Float:
                        val.EntryValue = (float)floatValue.Value;
                        listValues.SelectedNode.Cells[2].Text = val.EntryValue.ToString();
                        break;
                    case Battlefield4Class.SaveEntry.SaveEntryType.Integer:
                        val.EntryValue = intValue.Value;
                        listValues.SelectedNode.Cells[2].Text = val.EntryValue.ToString();
                        break;
                    case Battlefield4Class.SaveEntry.SaveEntryType.String:
                        val.EntryValue = txtValue.Text;
                        listValues.SelectedNode.Cells[2].Text = val.EntryValue.ToString();
                        break;
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BIN Files (.bin)|*.bin";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                byte[] data = GameSave.SaveEntries[(uint)Battlefield4Class.SaveEntryCategory.Unknown1][0].EntryValue as byte[];
                File.WriteAllBytes(sfd.FileName, data);
            }
        }
    }
}
