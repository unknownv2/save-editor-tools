using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.Star_Wars_TFU_II
{
    public partial class StarWarsTFUII : EditorControl
    {
        //public static readonly string FID = "4C4107F2";
        public StarWarsTFUII()
        {
            InitializeComponent();
            TitleID = FormID.StarWarsTFUII;
            listValues.PathSeparator = ".";
        }

        public override void revertForm()
        {
            listValues.BeginUpdate();
            listValues.Nodes.Clear();
            listValues.EndUpdate();
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;
            string[] values = Encoding.ASCII.GetString(IO.In.ReadBytes(IO.Stream.Length)).Split('\0')[0].Split('\n');
            listValues.BeginUpdate();
            foreach (string setting in values)
            {
                if (setting.Length != 0)
                {
                    string[] val = setting.Split(" = ");
                    addSetting(val[0], val[1].Remove(val[1].Length - 1));
                }
            }
            listValues.EndUpdate();
            if (listValues.Nodes.Count != 0)
                listValues.Nodes[0].Expand();
            return true;
        }

        private void addSetting(string setting, string value)
        {
            string[] setChilds = setting.Split('.');
            string curPath = String.Empty;
            Node parentNode = null;
            for (int x = 0; x < setChilds.Length; x++)
            {
                curPath += setChilds[x];
                Node findNode = listValues.FindNodeByName(curPath);
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
                    parentNode = parentNode == null ? listValues.Nodes[listValues.Nodes.Add(newNode)] : parentNode.Nodes[parentNode.Nodes.Add(newNode)];
                }
                else
                    parentNode = findNode;
                curPath += listValues.PathSeparator;
            }
        }

        public override void Save()
        {
            IO.Stream.Position = 0;
            foreach (Node rootNode in listValues.Nodes)
                recursiveWrite(rootNode);
            IO.Out.Write((byte)0x00);
        }

        private const string settingTemplate = "{0} = {1};\n";
        private void recursiveWrite(Node parentNode)
        {
            foreach (Node childNode in parentNode.Nodes)
            {
                if (childNode.Cells.Count == 2)
                    IO.Out.Write(Encoding.ASCII.GetBytes(String.Format(settingTemplate, childNode.Name, childNode.Cells[1].Text)));
                else
                    recursiveWrite(childNode);
            }
        }

        private void listValues_CellEditEnding(object sender, CellEditEventArgs e)
        {
            const char q = '"';
            if (e.NewText.Length == 0)
            {
                Functions.UI.messageBox("You must enter a value!", "No Value", MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else if (e.NewText.Contains(q) && (e.NewText.IndexOf(q) == e.NewText.LastIndexOf(q) || e.NewText.LastIndexOf(q) != e.NewText.Length - 1))
            {
                Functions.UI.messageBox("Invalid string entered!", "Invalid", MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
