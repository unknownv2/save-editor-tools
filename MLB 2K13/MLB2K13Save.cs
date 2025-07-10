using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MLB
{
    public class MLB2K13Save
    {
        private EndianIO IO;

        public int BattingSP { get; set; }
        public int FieldingSP { get; set; }
        public int BaserunningSP { get; set; }
        public int PitchingSP { get; set; }

        public MLB2K13Save(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            IO.SeekTo(0xC);
            BattingSP = IO.In.ReadInt32();
            FieldingSP = IO.In.ReadInt32();
            BaserunningSP = IO.In.ReadInt32();
            PitchingSP = IO.In.ReadInt32();
        }

        public void Save()
        {
            IO.SeekTo(0x0C);
            IO.Out.Write(BattingSP);
            IO.Out.Write(FieldingSP);
            IO.Out.Write(BaserunningSP);
            IO.Out.Write(PitchingSP);
        }
    }
}
