using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ElectronicArts;

namespace NBA2K
{
    internal class NBA2K14Class
    {
        private EndianIO IO;
        internal ulong SkillPoints;
        internal uint Fans;

        internal NBA2K14Class(EndianIO io)
        {
            if(io != null)
                IO = io;
            else
                throw new Exception("NBA 2K14: invalid File I/O detected");

            // verify save data integrity
            int fileSize = IO.In.SeekNReadInt32(0x0C) - 4;
            IO.SeekTo(0x04);

            var crc = EACRC32.Calculate_Alt2(IO.In.ReadBytes(fileSize), fileSize, 0x00);
            if (crc != IO.In.SeekNReadUInt32(0x00))
                throw new Exception("NBA 2K14: invalid save data detected");

            // read MyCareer information
            Read();
        }

        private void Read()
        {
            IO.SeekTo(0x007004FF);
            SkillPoints = IO.In.ReadUInt64() >> 2;
            Fans = IO.In.SeekNReadUInt32(0x70455F);
        }

        internal void Save()
        {
            IO.SeekTo(0x007004FF);
            IO.Out.Write(SkillPoints << 2);
            IO.Out.SeekNWrite(0x70455F, Fans);

            // write checksum back to file
            IO.In.SeekTo(0x04);
            IO.Out.SeekNWrite(0x00, EACRC32.Calculate_Alt2(IO.In.ReadBytes(IO.Length - 4), (int)IO.Length - 4, 0x00));
        }
    }
}
