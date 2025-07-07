using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using System.IO;
using System.IO.Compression;
using Forza3;
using ForzaMotorsport;

namespace Horizon.PackageEditors.Forza_3
{
    public partial class Forza3 : EditorControl
    {
        private Forza3Profile Profile;
        /*private PlayerDatabase PlayerDatabase;
        private ForzaSQLite PlayerDatabaseSQLite;

        private EndianIO PlayerDatabaseIO;

        private string TemporaryDatabase;*/

        //public static readonly string FID = "4D53084D";
        public Forza3()
        {
            InitializeComponent();
            TitleID = FormID.Forza3;
            
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile(SettingAsString(138)))
                return false;

            Forza3Profile.HmacKeyStack = new byte[3][] { SettingAsByteArray(170), SettingAsByteArray(145), SettingAsByteArray(194) };
            Forza3Profile.AesKeyStack = new byte[1][] { SettingAsByteArray(88) };

            var reader = new EndianReader(Package.StfsContentPackage.GetFileStream(SettingAsString(195)), EndianType.BigEndian);
            reader.SeekTo(SettingAsInt(110));
            int Version = reader.ReadInt32();
            reader.Close();

            this.Profile = new Forza3Profile(this.IO, Version, this.Package.Header.Metadata.Creator);

            this.LoadSaveData();
            this.LoadPropertyTreeData();

            return true;
        }
        public override void Save()
        {
            this.UpdateNode("Main;Credits", this.numCredits.Value.ToString());
            this.UpdateNode("Main;XP", this.numXP.Value.ToString());
            this.UpdateNode("Main;Level", this.numLevel.Value.ToString());

            this.WritePropertyTreeData();

            this.Profile.Save();
        }

        private void LoadSaveData()
        {
            this.Profile.SaveIO.In.SeekTo(this.Profile.BaseProfile.GetEntryAddress(SettingAsString(152)));
            this.numCredits.Value = this.Profile.SaveIO.In.ReadUInt32();
            this.Profile.SaveIO.In.SeekTo(this.Profile.BaseProfile.GetEntryAddress(SettingAsString(191)));
            this.numXP.Value = this.Profile.SaveIO.In.ReadUInt32();
            this.Profile.SaveIO.In.SeekTo(this.Profile.BaseProfile.GetEntryAddress(SettingAsString(98)));
            this.numLevel.Value = this.Profile.SaveIO.In.ReadUInt32();
        }

        // ForzaProfile Property Setting editing functions
        private void LoadPropertyTreeData()
        {
            this.advPropertyTree.Nodes.Clear();
            this.advPropertyTree.BeginUpdate();

            for (int i = 0; i < this.Profile.BaseProfile.ProfileSchemaEntries.Count; )
            {
                string Parent = this.Profile.BaseProfile.ProfileSchemaEntries[i].Name;
                if (this.Profile.BaseProfile.ProfileSchemaEntries[i].Type == ForzaTypes.PropertyBag)
                {
                    int ChildCount = this.Profile.BaseProfile.ProfileSchemaEntries[i++].PropertyChildCount;
                    for (int x = 0; x < ChildCount; x++)
                    {
                        this.addSetting(Parent + "\\" + this.Profile.BaseProfile.ProfileSchemaEntries[i].Name, FormatSettingValue(this.Profile.BaseProfile.ProfileSchemaEntries[i++]), advPropertyTree);
                    }
                }
                else
                {
                    this.addSetting("Other\\" + this.Profile.BaseProfile.ProfileSchemaEntries[i].Name, FormatSettingValue(this.Profile.BaseProfile.ProfileSchemaEntries[i++]), advPropertyTree);
                }
            }

            this.advPropertyTree.EndUpdate();
        }
        private void WritePropertyTreeData()
        {
            foreach (Node PropTree in this.advPropertyTree.Nodes)
            {
                this.SavePropertySetting(PropTree);
            }
        }
        private void SavePropertySetting(Node PropertyTree)
        {
            foreach (Node propertyNode in PropertyTree.Nodes)
            {
                var Property = this.Profile.BaseProfile.GetEntry(propertyNode.Name.Split(';')[1]);
                this.Profile.SaveIO.SeekTo(Property.Address);
                this.WritePropertyValue(this.Profile.SaveIO.Out, Property.Type, propertyNode.Cells[1].Text);
            }
        }
        private string FormatSettingValue(ForzaProfileEntry Entry)
        {
            switch (Entry.Type)
            {
                case ForzaTypes.CarId:
                case ForzaTypes.UInt32:
                    return BitConverter.ToUInt32(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.Bool:
                    return BitConverter.ToBoolean(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.Uint16:
                    return BitConverter.ToUInt16(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.UInt8:
                    return Entry.Value[0].ToString();
                case ForzaTypes.Float32:
                    return BitConverter.ToSingle(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.UInt64:
                    return BitConverter.ToUInt64(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                default:
                    throw new ForzaException(string.Format("could not format property type {0:D}", Entry.Type));
            }
        }
        private void WritePropertyValue(EndianWriter writer, ForzaTypes Type, string Value)
        {
            switch (Type)
            {
                case ForzaTypes.CarId:
                case ForzaTypes.UInt32:
                    writer.Write(uint.Parse(Value));
                    break;
                case ForzaTypes.Bool:
                    writer.Write(bool.Parse(Value));
                    break;
                case ForzaTypes.Uint16:
                    writer.Write(ushort.Parse(Value));
                    break;
                case ForzaTypes.UInt8:
                    writer.Write(byte.Parse(Value));
                    break;
                case ForzaTypes.Float32:
                    writer.Write(float.Parse(Value));
                    break;
                case ForzaTypes.UInt64:
                    writer.Write(ulong.Parse(Value));
                    break;
                default:
                    throw new ForzaException(string.Format("could not parse property type {0:D}", Type));
            }
        }
        // Sync the values from the 'Simple' Tab to the 'Advanced' Tab
        private void EditPropCrValidated(object sender, EventArgs e)
        {
            this.UpdateNode("Main;Credits", (sender as NumericUpDown).Value.ToString());
        }
        private void EditPropXPValidated(object sender, EventArgs e)
        {
            this.UpdateNode("Main;XP", (sender as NumericUpDown).Value.ToString());
        }
        private void EditPropLvlValidated(object sender, EventArgs e)
        {
            this.UpdateNode("Main;Level", (sender as NumericUpDown).Value.ToString());
        }
        private void UpdateNode(string NodeName, string Value)
        {
            var node = this.advPropertyTree.Nodes.Find(NodeName, true)[0];
            node.BeginEdit();
            node.Cells[1].Text = Value.ToString();
            node.EndEdit(false);
        }

        // Sync the values from the 'Advanced' Tab to the 'Simple' Tab
        private void EditPropertyTreeCellEdit(object sender, CellEditEventArgs e)
        {
            if (e.Cell.Parent.Cells[0].Text != (e.Cell.Text))
            {
                switch (e.Cell.Parent.Name)
                {
                    case "Main;Credits":
                        this.numCredits.Value = decimal.Parse(e.NewText);
                        break;
                    case "Main;XP":
                        this.numXP.Value = decimal.Parse(e.NewText);
                        break;
                    case "Main;Level":
                        this.numLevel.Value = decimal.Parse(e.NewText);
                        break;
                }
            }
        }
        private void addSetting(string setting, string value, AdvTree Tree)
        {
            string[] setChilds = setting.Split('\\');
            string curPath = String.Empty;
            Node parentNode = null;
            for (int x = 0; x < setChilds.Length; x++)
            {
                curPath += setChilds[x];
                Node findNode = Tree.FindNodeByName(curPath);
                if (findNode == null)
                {
                    Node newNode = new Node(setChilds[x]);
                    newNode.Name = curPath;
                    newNode.ExpandVisibility = eNodeExpandVisibility.Auto;
                    if (x == setChilds.Length - 1)
                    {
                        Cell newCell = new Cell(value);
                        newCell.Editable = true;
                        newNode.Cells.Add(newCell);
                    }
                    parentNode = parentNode == null ? Tree.Nodes[Tree.Nodes.Add(newNode)] : parentNode.Nodes[parentNode.Nodes.Add(newNode)];
                }
                else
                    parentNode = findNode;
                curPath += Tree.PathSeparator;
            }
        }

        // Player Database functions

        private void BtnClickExportPlayerDatabase(object sender, EventArgs e)
        {            
            /* var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, PlayerDatabase.ExtractDatabase());
            } */    
        }
        private void BtnClickInjectPlayerDatabase(object sender, EventArgs e)
        {
            /* var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {                
                PlayerDatabase.InjectDB(File.ReadAllBytes(ofd.FileName), this.PlayerDatabaseIO);
            } */
        }
        private void CmbTableSelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            string TableName = this.comboBoxEx1.Text;

            var DataTable = PlayerDatabaseSQLite.RetrieveTableData(TableName);

            var ColumnNames = PlayerDatabaseSQLite.RetrieveTableEntries(TableName);

            this.advTree1.BeginUpdate();

            this.advTree1.Nodes.Clear();

            for (var x = 0; x < DataTable.Rows.Count; x++)
            {
                var Node = new DevComponents.AdvTree.Node();

                Node.Text = DataTable.Rows[x]["CarId"].ToString();

                if (ColumnNames.Count > 0)
                {
                    for (var i = 0; i < ColumnNames.Count; i++)
                    {
                        var tNode = new DevComponents.AdvTree.Node();
                        tNode.Text = ColumnNames[i];

                        var cell = new Cell(DataTable.Rows[x][ColumnNames[i]].ToString());
                        cell.Editable = true;
                        tNode.Cells.Add(cell);

                        Node.Nodes.Add(tNode);
                    }
                }
                this.advTree1.Nodes.Add(Node);
            }

            this.advTree1.EndUpdate();
            */
        }

        private void ReadPlayerDatabase()
        {
            /*
            this.PlayerDatabaseIO = this.Package.StfsContentPackage.GetEndianIO("PlayerDatabase", true);

            PlayerDatabase = new PlayerDatabase(new MemoryStream(this.PlayerDatabaseIO.ToArray()), Horizon.Functions.Global.hexStringToArray(this.Package.Header.Metadata.Creator.ToString("X")));

            PlayerDatabase.Read();

            this.TemporaryDatabase = this.PlayerDatabase.CreateDatabase();

            this.PlayerDatabaseSQLite = new ForzaSQLite(this.TemporaryDatabase);

            var TableNames = PlayerDatabaseSQLite.RetrieveTables();

            for (var j = 0; j < TableNames.Count; j++)
            {
                this.comboBoxEx1.Items.Add(TableNames[j]);
            }*/

            /*
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var outIO = new EndianIO(sfd.FileName, EndianType.BigEndian, true);
                var inIO = this.Package.StfsContentPackage.GetEndianIO("Thumbnails\\Thumbnail_1.xdc");
                inIO.Open();

                inIO.SeekTo(0x0C);
                int CompressedSize = inIO.In.ReadInt32();
                inIO.Stream.Position += 4;
                var DeflateStream = new DeflateStream(new MemoryStream(inIO.In.ReadBytes(CompressedSize)), CompressionMode.Decompress);
                var data = new byte[4096];
                int length = 0;
                while ( (length = DeflateStream.Read(data, 0, 4096)) > 0)
                {
                    outIO.Out.Write(data, 0, length);
                }
                outIO.Close();
                //DeflateStream.Read(DecompressedData, 0, DecompressedSize);
                DeflateStream.Close();
            }
            */
        }
        private void SaveToPlayerDatabase(Node Node)
        {

        }
        
        private void Forza3_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean up the temporary file created for the player SQLite database
            /*if (File.Exists(this.TemporaryDatabase))
            {
                File.Delete(this.TemporaryDatabase);
            }*/
        }
    }
}
