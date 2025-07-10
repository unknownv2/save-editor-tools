using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectGothamRacing
{
    internal class PGR4SaveGame
    {
        internal int Kudos;
        private EndianIO _io;

        internal PGR4SaveGame(EndianIO io)
        {
            _io = io;
            Read();
        }

        internal void Save()
        {
            _io.Out.SeekNWrite(0x0002015D, Kudos);
        }

        internal void Read()
        {
            Kudos = _io.In.SeekNReadInt32(0x0002015D);
        }
    }
}
