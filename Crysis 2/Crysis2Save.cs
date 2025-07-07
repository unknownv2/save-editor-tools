using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections;
using System.Xml.XPath;
using Crysis2;

namespace Horizon.PackageEditors.Crysis_2
{
    public partial class Crysis2Save : EditorControl
    {
        //public static readonly string FID = "454108EX";

        private ProfileSave CrySave;
        private XmlDocument XmlDocument;

        private Horizon.Functions.DatagridViewColumnSorter DatagridSorter;

        private bool DidSimpleUserEdit;
        private bool DidAdvancedUserEdit;

        public Crysis2Save()
        {
            InitializeComponent();
            TitleID = FormID.Crysis2Profile;
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("profile.xml"))
                return false;

            try
            {
                this.CrySave = new ProfileSave(this.IO);

                var MemoryStream = this.CrySave.ExtractDataBuffer();

                this.rtCryProfileAdv.Text = System.Text.ASCIIEncoding.ASCII.GetString(MemoryStream.ToArray());

                this.XmlDocument = new XmlDocument();
                this.XmlDocument.Load(MemoryStream);

                this.DatagridSorter = new Horizon.Functions.DatagridViewColumnSorter(SortOrder.Ascending);

                this.Display();
            }
            catch
            {
                string.Format("CryEngine: failed while loading the save data.");
            }

            return true;
        }
        public override void Save()
        {
            var Navigator = this.XmlDocument.CreateNavigator();
            var Iterator = Navigator.Select("/Profile/Attributes/Attr");

            int CellIndex = 0;

            if (this.DidSimpleUserEdit || this.DidAdvancedUserEdit)
            {
                while (Iterator.MoveNext())
                {
                    // XML editing is done this way because Crysis 2 uses forward slashes '/' in the Attribute 'name' which creates an exception in XmlElement.SetAttribute
                    string Name = this.dataGridViewX1.Rows[CellIndex].Cells[0].Value.ToString();
                    string Value = this.dataGridViewX1.Rows[CellIndex++].Cells[1].Value.ToString();

                    if (string.Compare(Iterator.Current.GetAttribute("name", string.Empty), Name) == 0)
                    {
                        if (!string.IsNullOrEmpty(Value))
                        {
                            Iterator.Current.MoveToAttribute("value", string.Empty);
                            Iterator.Current.SetValue(Value);
                        }
                    }
                }
            }

            var MS = new MemoryStream();

            this.XmlDocument.Save(MS);

            this.CrySave.Save(MS.ToArray());
        }

        private void Display()
        {
            try
            {
                this.dataGridViewX1.Rows.Clear();
                var Navigator = this.XmlDocument.CreateNavigator();
                var Iterator = Navigator.Select("/Profile/Attributes/Attr");

                while (Iterator.MoveNext())
                {
                    this.dataGridViewX1.Rows.Add(Iterator.Current.GetAttribute("name", string.Empty), Iterator.Current.GetAttribute("value", string.Empty));
                }
            }
            catch
            {
                throw new Exception("CryEngine: failed while reading attributes from 'profile.xml'.");
            }
        }

        private void EventAttributeEditValidate(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                this.dataGridViewX1.Rows[e.RowIndex].ErrorText = "Please enter a valid value for this attribute.";
                e.Cancel = true;
            }            
        }
        private void dataGridViewX1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridViewX1.Rows[e.RowIndex].ErrorText = string.Empty;
        }
        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0x00 && e.ColumnIndex >= 0x00)
            {
                try
                {
                    var Navigator = this.XmlDocument.CreateNavigator();

                    var Iterator = Navigator.Select(string.Format("/Profile/Attributes/Attr[@name='{0}']", this.dataGridViewX1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString()));
                    Iterator.MoveNext();
                    Iterator.Current.MoveToAttribute("value", string.Empty);
                    Iterator.Current.SetValue(this.dataGridViewX1.Rows[e.RowIndex].Cells[1].EditedFormattedValue.ToString());

                    this.DidSimpleUserEdit = true;
                }
                catch
                {
                    throw new Exception(string.Format("CryEngine: could not save the value of {0} to the XML buffer.", this.dataGridViewX1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString()));
                }
            }
        }

        private void dataGridViewX1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewX1.IsCurrentCellDirty)
            {
                dataGridViewX1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LoadSimpleTab(object sender, EventArgs e)
        {
            if (this.DidAdvancedUserEdit)
            {
                if (!string.IsNullOrEmpty(this.rtCryProfileAdv.Text))
                {
                    this.XmlDocument = new XmlDocument();
                    this.XmlDocument.Load(new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(this.rtCryProfileAdv.Text)));

                    this.Display();

                    this.DidSimpleUserEdit = false;
                    this.DidAdvancedUserEdit = false;
                }
            }
        }
        private void LoadAdvancedTab(object sender, EventArgs e)
        {
            if (this.DidSimpleUserEdit)
            {
                var ms = new MemoryStream();

                this.XmlDocument.Save(ms);

                this.rtCryProfileAdv.Text = System.Text.ASCIIEncoding.ASCII.GetString(ms.ToArray());

                this.DidSimpleUserEdit = false;
                this.DidAdvancedUserEdit = false;
            }
        }

        private void RichTextProfileXmlChange(object sender, EventArgs e)
        {
            this.DidAdvancedUserEdit = true;
        }

        private void dataGridViewX1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == this.DatagridSorter.SortColumn)
            {
                if (this.DatagridSorter.Order == SortOrder.Ascending)
                {
                    DatagridSorter.Order = SortOrder.Descending;
                }
                else if (DatagridSorter.Order == SortOrder.Descending)
                {
                    DatagridSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                this.DatagridSorter.SortColumn = e.ColumnIndex;
                this.DatagridSorter.Order = SortOrder.Descending;
            }
            this.dataGridViewX1.Sort(this.DatagridSorter);
        }
    }
}