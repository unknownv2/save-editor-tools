using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using XboxDataBaseFile;
using System.IO;
using Horizon.Functions;

namespace Horizon.Forms
{
    public partial class TitleSettingsManager : Office2007RibbonForm
    {
        public TitleSettingsManager()
        {
            MdiParent = Main.mainForm;
            InitializeComponent();
            comboType.SelectedIndex = 0;
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdInject.Enabled = cmdExtract.Enabled = comboType.SelectedIndex != 0;
        }

        private DataFile Gpd;
        private void cmdOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataFile newGpd = new DataFile(new EndianIO(ofd.FileName, EndianType.BigEndian, true));
                try
                {
                    newGpd.Read();
                    Gpd = newGpd;
                    panelType.Enabled = true;
                }
                catch
                {
                    panelType.Enabled = false;
                }
            }
        }

        private ulong SelectedSetting
        {
            get
            {
                switch (comboType.SelectedIndex)
                {
                    case 1:
                        return (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC1;
                    case 2:
                        return (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC2;
                    case 3:
                        return (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC3;
                    case 4:
                        return 1;
                    default:
                        return 0;
                }
            }
        }

        private void cmdExtract_Click(object sender, EventArgs e)
        {
            ulong id = SelectedSetting;
            if (id == 0)
                return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "TitleSetting";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (id == 1)
                    {
                        EndianIO io = new EndianIO(new FileStream(sfd.FileName, FileMode.Create, FileAccess.ReadWrite), EndianType.BigEndian, true);
                        io.Out.Write(Gpd.ReadTitleSetting(new DataFileId() { Id = (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC1, Namespace = Namespace.SETTINGS }));
                        try
                        {
                            io.Out.Write(Gpd.ReadTitleSetting(new DataFileId() { Id = (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC2, Namespace = Namespace.SETTINGS }));
                            io.Out.Write(Gpd.ReadTitleSetting(new DataFileId() { Id = (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC3, Namespace = Namespace.SETTINGS }));
                        }
                        catch
                        {

                        }
                        io.Close();
                    }
                    else
                    {
                        File.WriteAllBytes(sfd.FileName, Gpd.ReadTitleSetting(new DataFileId() { Id = id, Namespace = Namespace.SETTINGS }));
                    }
                }
                catch
                {
                    UI.errorBox("This GPD does not contain this title setting!");
                }
            }
        }

        private void cmdInject_Click(object sender, EventArgs e)
        {
            ulong id = SelectedSetting;
            if (id == 0)
                return;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (id == 1)
                    {
                        FileStream input = new FileStream(ofd.FileName, FileMode.Open);

                        int readLength = (int)(input.Length - input.Position);
                        byte[] buffer = new byte[readLength > 1000 ? 1000 : readLength];

                        if (buffer.Length == 0)
                            return;
                        input.Read(buffer, 0, buffer.Length);
                        Gpd.WriteTitleSetting(new DataFileId() { Id = (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC1, Namespace = Namespace.SETTINGS }, buffer);

                        if (buffer.Length < 1000)
                            return;
                        readLength = (int)(input.Length - input.Position);
                        buffer = new byte[readLength > 1000 ? 1000 : readLength];

                        if (buffer.Length == 0)
                            return;

                        input.Read(buffer, 0, buffer.Length);
                        Gpd.WriteTitleSetting(new DataFileId() { Id = (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC2, Namespace = Namespace.SETTINGS }, buffer);

                        if (buffer.Length < 1000)
                            return;

                        readLength = (int)(input.Length - input.Position);
                        buffer = new byte[readLength > 1000 ? 1000 : readLength];

                        if (buffer.Length == 0)
                            return;

                        input.Read(buffer, 0, buffer.Length);
                        Gpd.WriteTitleSetting(new DataFileId() { Id = (ulong)XProfileIds.XPROFILE_TITLE_SPECIFIC3, Namespace = Namespace.SETTINGS }, buffer);
                    }
                    else
                    {
                        Gpd.WriteTitleSetting(new DataFileId() { Id = id, Namespace = Namespace.SETTINGS }, File.ReadAllBytes(ofd.FileName));
                    }
                }
                catch
                {
                    UI.errorBox("This GPD does not contain this title setting!");
                }
            }
        }
    }
}
