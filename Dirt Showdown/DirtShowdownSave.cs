using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace DirtShowdown
{
    public class RaceHistory : Dirt.DirtSecuritySave
    {
        public int Money;

        public RaceHistory(EndianIO io, SecurityInfoFile.SecEntry securityInfo)
            : base(io, securityInfo)
        {
            this.Read();
        }

        private void Read()
        {
            this.IO.In.SeekTo(0x19C);
            this.Money = this.IO.In.ReadInt32();
        }

        public override void Flush()
        {
            this.IO.Out.SeekTo(0x19C);
            this.IO.Out.Write(this.Money);
        }
    }
}