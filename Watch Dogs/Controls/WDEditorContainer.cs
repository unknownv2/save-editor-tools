using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatchDogs;

namespace Horizon.PackageEditors.Watch_Dogs.Controls
{
    public partial class WDEditorContainer : UserControl
    {
        internal WDEditorContainer(WatchDogsContainerType containerType, WatchDogsSave saveEntry)
        {
            InitializeComponent();
            Display(containerType, saveEntry);
        }
        private void Add(Control c)
        {
            c.Dock = DockStyle.Top;
            pnlControls.Controls.Add(c);
            c.BringToFront();
        }

        internal void Display(WatchDogsContainerType containerType, WatchDogsSave saveEntry)
        {
            pnlControls.Controls.Clear();

            switch (containerType)
            {
                case WatchDogsContainerType.CarsOnDemand:
                {
                    var carOnDemandListObject = new WDInventoryCarsOnDemandList(saveEntry);
                    Add(carOnDemandListObject);
                }
                    break;
                case WatchDogsContainerType.Inventory:
                {
                    var inventoryWheelListObject = new WDInventoryWheelList(saveEntry);
                    Add(inventoryWheelListObject);
                }
                break;
            }
        }

        internal void Save()
        {
            foreach (Control c in pnlControls.Controls)
            {
                if (c.GetType() == typeof(WDInventoryWheelList))
                    ((WDInventoryWheelList)c).Save();
            }
        }
    }
}
