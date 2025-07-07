
using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Crysis;
using Crysis3;

namespace Horizon.PackageEditors.Crysis_3
{
    public partial class Crysis3SaveGame : EditorControl
    {
        //public static readonly string FID = "4541098E";
        private XmlDocument XmlDocument;
        private Functions.DatagridViewColumnSorter DatagridSorter;
        private bool DidSimpleUserEdit;
        private bool DidAdvancedUserEdit;
        private Crysis3.Crysis3SaveGame _saveGame;

        public Crysis3SaveGame()
        {
            InitializeComponent();
            TitleID = FormID.Crysis3SaveGame;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("profile.xml"))
                return false;

            var crypto = new CrysisCryptek(SettingAsByteArray(63), 0x74);

            _saveGame = new Crysis3.Crysis3SaveGame(IO, crypto);

            try
            {

                var memoryStream = _saveGame.ExtractDataBuffer();

                this.rtCryProfileAdv.Text = System.Text.Encoding.ASCII.GetString(memoryStream.ToArray());

                this.XmlDocument = new XmlDocument();
                this.XmlDocument.Load(memoryStream);

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

            _saveGame.Save(MS.ToArray());
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
