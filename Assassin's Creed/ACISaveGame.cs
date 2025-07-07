using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ubisoft
{
    internal class ACISaveGame
    {
        private EndianIO IO;
        internal ACISaveGame(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            
        }

        internal void Save()
        {
            
        }
    }
}
