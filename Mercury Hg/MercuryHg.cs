using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Mercury_Hg
{
    public partial class MercuryHg : EditorControl
    {
        //public static readonly string FID = "58410B8B";
        public MercuryHg()
        {
            InitializeComponent();
            TitleID = FormID.MercuryHg;
            
        }

        private MercuryHgSave Merc;
        private LevelsRecord Levels;
        private StatsRecord Stats;

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            Merc = new MercuryHgSave(IO);

            if (!Merc.ContainsKey(RecordTag.Levels))
                throw new Exception("Level record not present in save!");

            if (!Merc.ContainsKey(RecordTag.Stats))
                throw new Exception("Stats record not present in save!");

            Levels = new LevelsRecord(Merc[RecordTag.Levels]);

            Stats = new StatsRecord(Merc[RecordTag.Stats]);
            
            Levels[LevelTag.T001].Attempts = 9;

            return true;
        }

        public override void Save()
        {
            Merc[RecordTag.Levels].Data = Levels.ToArray();
            Merc[RecordTag.Stats].Data = Stats.ToArray();

            IO.Stream.Position = 0;
            IO.Out.Write(Merc.ToArray());
        }
    }
}
