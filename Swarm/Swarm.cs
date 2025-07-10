using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XboxDataBaseFile;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Swarm
{
    public partial class Swarm : EditorControl
    {
        //public static readonly string FID = "58410B07";
        public Swarm()
        {
            InitializeComponent();
            TitleID = FormID.Swarm;
            
        }

        public override bool Entry()
        {
            if (!loadTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, System.IO.EndianType.BigEndian))
                return false;
            IO.Stream.Position = 0x48;
            intBurned.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intCrushed.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intImpaled.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intElectrocuted.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intTrapped.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intDismembered.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intAsphyxiated.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intVaporized.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intAbyss.Value = IO.In.ReadInt32();

            IO.Stream.Position = 0xD0;
            intLevel1.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel2.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel3.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel4.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel5.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel6.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel7.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel8.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel9.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel10.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel11.Value = IO.In.ReadInt32();
            IO.Stream.Position += 4;
            intLevel12.Value = IO.In.ReadInt32();
            return true;
        }

        public override void Save()
        {
            IO.Stream.Position = 0x48;
            IO.Out.Write(intBurned.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intCrushed.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intImpaled.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intElectrocuted.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intTrapped.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intDismembered.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intAsphyxiated.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intVaporized.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intAbyss.Value);

            IO.Stream.Position = 0xD0;
            IO.Out.Write(intLevel1.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel2.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel3.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel4.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel5.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel6.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel7.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel8.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel9.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel10.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel11.Value);
            IO.Stream.Position += 4;
            IO.Out.Write(intLevel12.Value);

            writeTitleSetting(XProfileIds.XPROFILE_TITLE_SPECIFIC1, IO.ToArray());
        }

        private void cmdMaxDeathMedals_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < gpDeathMedals.Controls.Count; x++)
                if (gpDeathMedals.Controls[x].GetType() == typeof(IntegerInput))
                    ((IntegerInput)gpDeathMedals.Controls[x]).Value = Int32.MaxValue;
        }

        private void cmdMaxScore_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < gpScore.Controls.Count; x++)
                if (gpScore.Controls[x].GetType() == typeof(IntegerInput))
                    ((IntegerInput)gpScore.Controls[x]).Value = Int32.MaxValue;
        }
    }
}
