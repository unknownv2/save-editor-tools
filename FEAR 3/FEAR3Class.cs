using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.FEAR_3
{
    //SAVE SIZE MUST BE DIVISIBLE BY 0xFFF4
    public class FEAR3Class
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Constructor

        public FEAR3Class(EndianIO io)
        {
            //Set our IO
            IO = io;
            //Read our gamesave
            Read();
        }

        #endregion

        #region Functions

        public void Read()
        {
            IO.In.BaseStream.Position = 0x10;
            int count = 0;
            while (true)
            {
                int mod = IO.In.BaseStream.Position == 0x10 ? 0 : 5;
                int size = (int)IO.In.ReadInt32();
                if (size == 0 | size + IO.In.BaseStream.Position + mod >= IO.In.BaseStream.Length)
                    break;
                IO.In.BaseStream.Position += size + mod;
                count++;
            }
        }

        public void Write()
        {

        }
        #endregion
    }
}
