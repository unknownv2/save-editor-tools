using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ElectronicArts;

namespace Homefront
{
    public class HomefrontSave
    {
        private EndianIO IO;

        public HomefrontSave(EndianIO IO)
        {
            this.IO = IO;

            this.Read();
        }
        private void Read()
        {

        }
        public void Save()
        {
            this.FixChecksum();
        }
        private void FixChecksum()
        {
            this.IO.In.SeekTo(0x04);
            byte[] data = this.IO.In.ReadBytes(this.IO.Length - 4);
            this.IO.Out.SeekTo(0x00);
            this.IO.Out.Write(EACRC32.Calculate_Alt(data, data.Length, 0x00));
        }
    }
}