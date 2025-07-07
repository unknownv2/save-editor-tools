using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Capcom;

namespace Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD.Controls
{
    public partial class CVXItem : UserControl
    {
        private CodeVeronicaXItemSlot _slotItem;

        internal CVXItem(CodeVeronicaXItemSlot slotItem)
        {
            InitializeComponent();

            _slotItem = slotItem;

            intItemCount.Value = slotItem.ItemCount;
            bIsCountInfinite.Checked = slotItem.IsInfinite;
        }
    }
}
