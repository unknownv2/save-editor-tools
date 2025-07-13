using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XboxDataBaseFile;

namespace Horizon.PackageEditors.Marble_Blast_Ultra
{
    public partial class MarbleBlastUltra : EditorControl
    {
        //public static readonly string FID = "584107D7";
        public MarbleBlastUltra()
        {
            InitializeComponent();
            TitleID = FormID.MarbleBlastUltra;
            
            for (int x = 1; x < 61; x++)
                comboLevel.Items.Add(x.ToString());
        }

        private uint[] levelTimes;
        public override bool Entry()
        {
            if (!loadTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, System.IO.EndianType.BigEndian))
                return false;
            levelTimes = new uint[60];
            IO.Stream.Position = 0x3C;
            for (int x = 0; x < levelTimes.Length; x++)
                levelTimes[x] = IO.In.ReadUInt32();
            comboLevel.SelectedIndex = 0;
            comboLevel_SelectedIndexChanged(null, null);
            return true;
        }

        public override void Save()
        {
            saveTime(true);
            IO.Stream.Position = 0x3C;
            for (int x = 0; x < levelTimes.Length; x++)
                IO.Out.Write(levelTimes[x]);
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, IO.ToArray());
        }

        private void comboLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveTime(false);
            lastSelection = comboLevel.SelectedIndex;
            txtTime.Text = getTimeSpan(levelTimes[comboLevel.SelectedIndex]);
        }

        private string getTimeSpan(uint milliseconds)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);
            return String.Format("{0:D2}:{1:D2}.{2:D2}", time.Minutes, time.Seconds, time.Milliseconds);
        }

        private int lastSelection = -1;
        private void saveTime(bool revert)
        {
            if (lastSelection != -1)
            {
                string[] times = txtTime.Text.Split(new char[] { ':', '.' });
                if (times.Length == 3)
                {
                    int minutes, seconds, milliseconds;
                    if (int.TryParse(times[0], out minutes) && int.TryParse(times[1], out seconds) && int.TryParse(times[2], out milliseconds))
                        levelTimes[lastSelection] = (uint)new TimeSpan(0, 0, minutes, seconds, milliseconds).TotalMilliseconds;
                    else if (revert)
                        txtTime.Text = getTimeSpan(levelTimes[lastSelection]);
                }
                else
                    txtTime.Text = getTimeSpan(levelTimes[lastSelection]);
            }
        }
    }
}
