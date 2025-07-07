using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Dirt2
{
    public class Dirt2Save
    {
        internal static byte[] AESKEY;

        private EndianIO IO;

        public int Balance;

        public Dirt2Save(EndianReader reader)
        {
            this.IO = new EndianIO(this.Decrypt(reader), EndianType.BigEndian, true);

            this.Read();
        }
        private void Read()
        {
            this.IO.In.SeekTo(0x10);
            this.Balance = (this.IO.In.ReadInt32() / 1000) * 1500;

        }
        public byte[] Save()
        {
            this.IO.Out.SeekTo(0x10);
            this.IO.Out.Write((Balance / 1500) * 1000);

            return Encrypt();
        }

        private int dirt_checksum(long length, EndianReader er)
        {
            er.SeekTo(0);
            long ctr = ((((length >> 2) - 2) >> 1) + 1), num = 0, num2 = 0, num3 = (length >> 2);
            for (int x = 0; x < ctr; ++x)
            {
                num3 -= 2;
                num += er.ReadInt32();
                num2 += er.ReadInt32();
            }
            if (num3 != 0)
            {
                er.SeekTo(er.BaseStream.Length - 4);
                return (int)(er.ReadInt32() + num + num2);
            }
            else return (int)(num + num2);
        }
        public byte[] Decrypt(EndianReader reader)
        {
            //Initialize the decrypter
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = AESKEY;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;
            ICryptoTransform cTransform = aes.CreateDecryptor();
            //Initialize the mem stream that will hold our decrypted data
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);
            reader.SeekTo(0x08);
            int blockCount = reader.ReadInt32() / 0x404;

            for (int x = 0; x < blockCount; ++x)
            {
                int blockSize = reader.ReadInt32();
                byte[] data = reader.ReadBytes(blockSize);
                cTransform.TransformBlock(data, 0, blockSize, data, 0);
                ew.Write(data); //write decrypted data
            }
            ew.Close();
            return ms.ToArray();
        }
        private byte[] Encrypt()
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = AESKEY;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;
            ICryptoTransform cTransform = aes.CreateEncryptor();

            MemoryStream ms = new MemoryStream();
            EndianIO stream = new EndianIO(ms, EndianType.BigEndian, true);
            this.IO.In.SeekTo(0x0);

            int blockCount = (int)this.IO.In.BaseStream.Length / 0x400, blockSize = 0x400;

            for (int x = 0; x < blockCount; ++x)
            {
                byte[] data = this.IO.In.ReadBytes(blockSize);
                cTransform.TransformBlock(data, 0, blockSize, data, 0);
                stream.Out.Write(blockSize); //write our block size
                stream.Out.Write(data); //write encrypted data
            }
            int sum = dirt_checksum(stream.In.BaseStream.Length, stream.In);
            EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.Out.Write(sum);
            io.Out.Write(0x026E);
            io.Out.Write((int)stream.In.BaseStream.Length);
            io.Out.Write(ms.ToArray());
            stream.Close();
            return io.ToArray();
        }
    }
}
