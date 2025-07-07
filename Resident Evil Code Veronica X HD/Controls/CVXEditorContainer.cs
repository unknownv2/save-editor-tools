using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Capcom;
using XContent;

namespace Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD.Controls
{
    public partial class CVXEditorContainer : UserControl
    {
        internal CodeVeronicaXEditorTypes EditorType;
        internal CVXEditorContainer(CodeVeronicaXEditorTypes editorType, object cvxItem)
        {
            EditorType = editorType;

            InitializeComponent();
            Display(editorType, cvxItem);
        }

        private void Add(Control c)
        {
            c.Dock = DockStyle.Top;
            pnlControls.Controls.Add(c);
            c.BringToFront();
        }

        internal void Display(CodeVeronicaXEditorTypes editorType, object linkedItem)
        {
            pnlControls.Controls.Clear();

            switch (editorType)
            {
                case CodeVeronicaXEditorTypes.ItemList:
                    {
                        var saveSlot = (CodeVeronicaXSaveSlot) linkedItem;
                        Add(new CVXSaveEntryControl(saveSlot));
                        for (var i = 0; i < saveSlot.CharacterInventories.Count; i++)
                        {
                            Add(new CVXItemList(saveSlot, saveSlot.CharacterInventories[i], Enum.GetName(typeof(CodeVeronicaXCharacters), i)));
                        }
                    }
                    break;

                    case CodeVeronicaXEditorTypes.ItemBox:
                    {
                        var itemBoxSaveSlot = (CodeVeronicaXSaveSlot)linkedItem;
                        Add(new CVXItemList(itemBoxSaveSlot,  itemBoxSaveSlot.ItemBox));
                    }
                    break;

                case CodeVeronicaXEditorTypes.Item:
                    {
                        var itemSlotObject = new CVXItem((CodeVeronicaXItemSlot) linkedItem);
                        Add(itemSlotObject);
                    }
                    break;
            }
        }

        public void Write()
        {
            foreach (Control c in pnlControls.Controls)
            {
                if (c.GetType() == typeof(CVXItemList))
                    ((CVXItemList)c).Write();
                else if (c.GetType() == typeof(CVXSaveEntryControl))
                    ((CVXSaveEntryControl)c).Write();
            }
        }
    }
}
