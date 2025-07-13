using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ElectronicArts;

namespace NBA2K
{
    public class NBA2K13Save
    {

        private EndianIO _io;
        public long SkillPoints;
        public uint Fans;

        public NBA2K13Save(EndianIO io)
        {
            _io = io;

            _io.In.SeekTo(0x04);
            var crc = EACRC32.Calculate_Alt2(_io.In.ReadBytes(_io.Length - 4), (int)_io.Length - 4, 0x00);
            if(crc != _io.In.SeekNReadUInt32(0x00))
                throw new Exception("NBA 2K13: invalid save data detected!");

            io.SeekTo(0x00684358);
            SkillPoints = io.In.ReadInt64()>> 0x02;

            Fans = io.In.SeekNReadUInt32(0x0068835C);
        }

        public void Save()
        {
            _io.Out.SeekTo(0x00684358);
            _io.Out.Write(SkillPoints << 0x02);

            _io.Out.SeekNWrite(0x0068835C, Fans);

            _io.In.SeekTo(0x04);
            _io.Out.SeekNWrite(0x00, EACRC32.Calculate_Alt2(_io.In.ReadBytes(_io.Length - 4), (int)_io.Length - 4, 0x00));
        }
    }
}
