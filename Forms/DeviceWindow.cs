using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using Horizon.Properties;

namespace Horizon.Forms
{
    internal partial class DeviceWindow : Office2007Form
    {
        private Main Main;
        new internal static DeviceWindow Handle;
        internal DeviceWindow(Main mainForm)
        {
            Handle = this;
            Main = mainForm;
            oldHeight = Main.exFatx.Size.Height;
            Main.exFatx.Expanded = false;

            AddControlData(Main.panelFatx);
            AddControlData(Main.rbFatx);
            AddControlData(Main.progressFatx);
            AddControlData(Main.cmdFatxContract);
            AddControlData(Main.cmdFatxExpand);
            AddControlData(Main.listFatx);
            AddControlData(Main.cmdFatxGear);

            this.Location = new Point(65, 65);

            InitializeComponent();

            oldSize = this.Size;

            int width = (int)(Settings.Default.DeviceSize >> 32);
            int height = (int)(Settings.Default.DeviceSize << 32 >> 32);
            if (width >= this.MinimumSize.Width && height >= this.MinimumSize.Height)
                this.Size = new Size(width, height);

            Main.panelFatx.SuspendLayout();
            Controls.Add(Main.panelFatx);
            Main.panelFatx.ResumeLayout(false);
            Main.panelFatx.Visible = true;
            Main.rbFatx.Dock = DockStyle.Top;
            Main.cmdFatxContract.Visible = false;
            Main.cmdFatxExpand.Visible = false;
            Main.progressFatx.Dock = DockStyle.Bottom;
            Main.listFatx.Size = new Size(this.Size.Width - 16, this.Size.Height - 135);
            Main.cmdFatxGear.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        }

        private List<ControlData> ControlCache = new List<ControlData>();
        private void AddControlData(Control control)
        {
            ControlCache.Add(new ControlData(control));
        }

        private void RevertControl(ControlData data)
        {
            data.Control.Location = data.Location;
            data.Control.Size = data.Size;
            data.Control.Dock = data.Dock;
            data.Control.Anchor = data.Anchor;
        }

        private class ControlData
        {
            internal Control Control;
            internal Point Location;
            internal Size Size;
            internal DockStyle Dock;
            internal AnchorStyles Anchor;

            internal ControlData(Control control)
            {
                Control = control;
                Location = control.Location;
                Size = control.Size;
                Dock = control.Dock;
                Anchor = control.Anchor;
            }
        }

        private static int oldHeight;
        internal static void Open(Main mainForm)
        {
            if (Handle == null)
                new DeviceWindow(mainForm).Show();
            else
            {
                Handle.Show();
                Handle.WindowState = FormWindowState.Normal;
                Handle.BringToFront();
            }
        }

        private static Size oldSize;
        private void DeviceWindow_Resize(object sender, EventArgs e)
        {
            ButtonX gearButton = Main.cmdFatxGear;
            int offset = this.Size.Width - oldSize.Width;
            gearButton.Size = new Size(gearButton.Size.Width + offset, gearButton.Size.Height);
            oldSize = this.Size;
        }

        private void DeviceWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            long width = (long)this.Size.Width;
            long height = (long)this.Size.Height;
            Settings.Default.DeviceSize = (width << 32) | height;
            Settings.Default.Save();
            if (Main.cmdDock.Checked)
            {
                for (int x = 0; x < ControlCache.Count; x++)
                    RevertControl(ControlCache[x]);
                Main.cmdFatxExpand.Visible = true;
                Main.cmdFatxContract.Visible = true;
                Main.panelFatx.SuspendLayout();
                Main.exFatx.Controls.Add(Main.panelFatx);
                Main.panelFatx.ResumeLayout(false);
                int offset = Main.exFatx.Size.Height - oldHeight;
                if (!Main.fatxExpanded)
                    offset += 3;
                Main.listFatx.Size = new Size(Main.listFatx.Size.Width, Main.listFatx.Size.Height + offset);
                Main.progressFatx.Location = new Point(Main.progressFatx.Location.X, Main.progressFatx.Location.Y + offset);
                Main.cmdFatxExpand.Location = new Point(Main.cmdFatxExpand.Location.X, Main.cmdFatxExpand.Location.Y + offset);
                Main.cmdFatxContract.Location = new Point(Main.cmdFatxContract.Location.X, Main.cmdFatxContract.Location.Y + offset);
                Handle = null;
            }
            else
            {
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}
