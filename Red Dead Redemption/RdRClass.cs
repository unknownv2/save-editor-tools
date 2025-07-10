using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace RedDeadRedemption
{
    public class RdR
    {
        public EndianIO IO;
        internal static byte[] Key = new byte[]{0xB7,0x62,0xDF,0xB6,0xE2,0xB2,0xC6,0xDE,0xAF,0x72,0x2A,0x32,0xD2,0xFB,0x6F,0x0C,
			      0x98,0xA3,0x21,0x74,0x62,0xC9,0xC4,0xED,0xAD,0xAA,0x2E,0xD0,0xDD,0xF9,0x2F,0x10};

        public RdR(EndianIO io)
        {
            io.SeekTo(8);
            IO = new EndianIO(RedDeadCrypt(io.In.ReadBytes(io.Stream.Length - 8), false), EndianType.BigEndian, true);
            this.Read();
        }
        private void Read()
        {

        }
        public byte[] Save()
        {
            return RedDeadCrypt(this.IO.ToArray(), true);
        }
        public byte[] RedDeadCrypt(byte[] dataIn, bool Encrypt)
        {
            byte[] data = new byte[dataIn.Length];
            dataIn.CopyTo(data, 0);

            // Create our Rijndael class
            Rijndael rj = Rijndael.Create();
            rj.BlockSize = 128;
            rj.KeySize = 256;
            rj.Mode = CipherMode.ECB;
            rj.Key = Key;
            rj.IV = new byte[16];
            rj.Padding = PaddingMode.None;

            ICryptoTransform transform = null;

            if (!Encrypt) transform = rj.CreateDecryptor();
            else transform = rj.CreateEncryptor();

            int dataLen = data.Length & ~0x0F; // byte align 16

            if (dataLen > 0)
            {
                for (int i = 0; i < 16; i++) // decrypt/encrypt 16 times (note : unnecessary)
                {
                    transform.TransformBlock(data, 0, dataLen, data, 0);
                }
            }

            return data;
        }
    }
}
