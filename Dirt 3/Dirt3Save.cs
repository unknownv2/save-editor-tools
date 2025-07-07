using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Dirt;

namespace Dirt3
{
    public class RaceHistory : DirtSecuritySave
    {
        public int Points;

        public RaceHistory(EndianIO IO, SecurityInfoFile.SecEntry SecurityInfo)
            : base(IO, SecurityInfo)
        {
            this.Read();
        }

        private void Read()
        {
            this.Points = IO.In.SeekNReadInt32(0x14C);
        }

        public override void Flush()
        {
            this.IO.Out.SeekNWrite(0x14C, this.Points);
        }
    }
}