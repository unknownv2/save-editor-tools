using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XboxDataBaseFile;

namespace Horizon.PackageEditors.Red_Faction_Guerrilla
{
    public partial class RedFactionGuerrilla : EditorControl
    {
        //public static readonly string FID = "5451080D";
        public RedFactionGuerrilla()
        {
            InitializeComponent();
            TitleID = FormID.RedFactionGuerrilla;
            
        }

        public override bool Entry()
        {
            if (!loadTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, System.IO.EndianType.BigEndian))
                return false;
            IO.Stream.Position += 8;
            int xp = IO.In.ReadInt32();
            intXP.Value = xp > intXP.MaxValue ? intXP.MaxValue : xp;
            return true;
        }

        public override void Save()
        {
            IO.Stream.Position -= 0x04;
            IO.Out.Write(intXP.Value);
            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, IO.ToArray());
        }

        private void cmdMaxValue_Click(object sender, EventArgs e)
        {
            intXP.Value = intXP.MaxValue;
        }
    }
}
