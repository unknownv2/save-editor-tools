using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeadRising2
{
    public class DeadRisingII
    {
        public EndianIO IO;

        // variables

        public int Money;
        public int PP;
        public int Level;
        public int MaxHealth;
        public int Health;
        public DeadRisingII(EndianIO io)
        {
            this.IO = io;

            this.Read();
        }
        private void Read()
        {
            this.IO.In.SeekTo(0xBC0);
            this.Level = this.IO.In.ReadInt32();
            this.PP = this.IO.In.ReadInt32();
            this.Money = this.IO.In.ReadInt32();
            this.IO.In.SeekTo(0xBD4);
            this.MaxHealth = this.IO.In.ReadInt32();
            this.Health = this.IO.In.ReadInt32();
        }
        public void Save()
        {
            this.IO.Out.SeekTo(0xBC0);
            this.IO.Out.Write(this.Level);
            this.IO.Out.Write(this.PP);
            this.IO.Out.Write(this.Money);

            this.IO.SeekTo(0xBD4);
            this.IO.Out.Write(this.MaxHealth);
            this.IO.Out.Write(this.Health);

            this.IO.In.SeekTo(4);
            uint checksum = Checksum(this.IO.Stream.Length - 8);

            this.IO.Out.SeekTo(0);
            this.IO.Out.Write(checksum);
            this.IO.Out.SeekTo(this.IO.Stream.Length - 4);
            this.IO.Out.Write(checksum);
        }
        internal static uint Poly;
        public uint Checksum(object DataLength)
        {
            uint num = 0, num1 = 0, num2 = 0;
            uint DwordCount = ShiftRightAlgebraicWord(Convert.ToUInt32(DataLength), 2);
            
            for (var x = 0; x < DwordCount; x++)
            {
                num2 = SwapInt32(this.IO.In.ReadUInt32());
                for (int j = 0; j < 0x20; j++)
                {
                    num1 = ShiftRightAlgebraicWord(num, 0x1f);
                    num <<= 1;
                    num1 ^= num2;
                    if ((num1 & 1) != 0)
                    {
                        num ^= Poly;
                    }
                    num2 = ShiftRightAlgebraicWord(num2, 1);
                }
            }
            return num;
        }
        private static uint ShiftRightAlgebraicWord(uint val, int n)
        {
            uint r = RotateLeft(val, 32 - n);
            int m = (int)GenerateMask64(n + 32, 63); 
            return (uint)((r & m) | ((r & ~m)));
        }
        private static UInt64 GenerateMask64(int MB, int ME)
        {

	        if(	MB <  0 || ME <  0 ||
		        MB > 63 || ME > 63 )
	        {
		        //msg("Error with paramters GenerateMask64(%d, %d)\n", MB, ME);
		        return 0;
	        }
        	
	        UInt64 mask = 0;
	        if(MB < ME+1)
	        {
		        // normal mask
		        for(int i=MB; i<=ME; i=i+1)
		        {
			        mask |= (ulong)((ulong)1 << (int)(63-i));
		        }
	        }
	        else if(MB == ME+1)
	        {
		        // all mask bits set
		        mask = 0xFFFFFFFFFFFFFFFF;
	        }
	        else if(MB > ME+1)
	        {
		        // split mask
		        UInt64 mask_lo = GenerateMask64(0, ME);
		        UInt64 mask_hi = GenerateMask64(MB, 63);
		        mask = mask_lo | mask_hi;
	        }
        	
	        return mask;
        }
        public static uint RotateLeft(uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }
        private static UInt32 SwapInt32(UInt32 inValue)
        {
            byte[] byteArray = BitConverter.GetBytes(inValue);
            Array.Reverse(byteArray);
            return BitConverter.ToUInt32(byteArray, 0);
        }
        private static uint ReadUInt32(byte[] buf)
        {
            return (uint)(((buf[3]) & 0xFF) | (((buf[2]) & 0xFF) << 8) | (((buf[1]) & 0xFF) << 16) | (((buf[0]) & 0xFF) << 24));
        }
    }
}
