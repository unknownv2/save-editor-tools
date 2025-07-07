using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bayonetta
{
    class Save
    {
        public int Halos;
        public int Health;
        public int MaxHealth;

        public void LoadSave(EndianIO io)
        {
            io.SeekTo(0xEF54);
            Halos = io.In.ReadInt32();

            // Seek to and read the health
            io.SeekTo(0xEEB0);
            Health = io.In.ReadInt32();
            MaxHealth = io.In.ReadInt32();
        }

        public void WriteSave(EndianIO io)
        {
            io.SeekTo(0xEF54);
            io.Out.Write(Halos);

            // Seek to and write the health
            io.SeekTo(0xEEB0);
            io.Out.Write(Health);
            io.Out.Write(MaxHealth);
            
            // Seek to the start and write the checksum
            io.Stream.Position = 0;
            uint[] checksum = BayonettaChecksum(io.In.ReadBytes((int)io.Stream.Length), (int)io.Stream.Length);
            io.Stream.Position = 0xc;
            foreach (uint sum in checksum)
            {
                io.Out.Write(sum);
            }
        }

        private uint[] BayonettaChecksum(byte[] data, int size)
        {
	        uint r28 = (uint)size >> 2;
	        uint r11 = r28 - 0xa;
	        uint r10 = 0x1c;
	        r11 >>= 1;
	        r11 += 1;
	        uint r31 = r11 + 4;
	        uint r25 = r31 << 1;
	        uint ctr = r11;
	        uint r9 = 0;
	        uint r24 = 0;
	        uint r30 = 0;
	        uint r29 = 0;
	        uint r6 = 0;
	        uint r8 = 0;
	        uint r7 = 0;
	        uint r5 = 0;
	        for(int i = 0; i < ctr; i++)
	        {
		        r30 = ((uint)data[r10 + 4] << 24)|((uint)data[r10 + 5] << 16)|((uint)data[r10 + 6] << 8)|((uint)data[r10 + 7]);
                r11 = ((uint)data[r10 + 8] << 24) | ((uint)data[r10 + 9] << 16) | ((uint)data[r10 + 10] << 8) | ((uint)data[r10 + 11]);
                r10 += 8;
		        r24 = r30 ^ r9;
		        r9 = r11 & 0xFFFF;
		        r31 = r30 & 0xFFFF;
		        r30 = r30 >> 16;
		        r29 = r11 >> 16;
		        r6 = r9 + r6;
		        r8 = r31 + r8;
		        r7 = r30 + r7;
		        r5 = r29 + r5;
		        r9 = r11 ^ r24;
	        }

            uint r26 = 0;
            uint r27 = 0;
            if (r25 < r28)
            {
                r11 = r25 << 2;
                r10 = ((uint)data[r11] << 24) | ((uint)data[r11 + 1] << 16) | ((uint)data[r11 + 2] << 8) | ((uint)data[r11 + 3]);
                r26 = r10 & 0xFFFF0000;
                r27 = r10 >> 16;
                r9 = r10 ^ r9;
            }

            r10 = r5 + r7;
            r11 = r6 + r8;
            r27 = r10 + r27;
            r26 = r11 + r26;

            return new uint[] { r26, r27, r9 };
        }
    }
}
