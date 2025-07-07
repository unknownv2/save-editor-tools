using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Dirt;
using Ionic.Zlib;

namespace Codemasters
{
    public class Grid2Save
    {
        private EndianIO IO;
        //private EndianIO FileIO;
        public DirtSecuritySave.SecurityInfoFile.SecEntry FileInfo;

        public int Fans;

        public Grid2Save(EndianIO io, DirtSecuritySave.SecurityInfoFile.SecEntry securityInfo)
        {
            var saveData = io.In.ReadBytes(securityInfo.FileSize);
            IO = new EndianIO(DeflateStream.UncompressBuffer(saveData), EndianType.BigEndian, true);
            FileInfo = securityInfo;

            if (CalculateChecksum(saveData) != FileInfo.Checksum)
                throw new Dirt.DirtException("invalid save data detected.");

            io.Close();
        }

        public void Read()
        {

            IO.SeekTo(0x519A);
            Fans = IO.In.ReadInt32();
        }

        public byte[] Save()
        {
            //IO.Out.SeekNWrite(0x519A, Fans);

            var dataStream = new MemoryStream(IO.ToArray());
            var memorystream = new MemoryStream();
            var ms = new MemoryStream();
            using (var compressionStream = new DeflateStream(ms, CompressionMode.Compress, CompressionLevel.Level8))
            {
                compressionStream.Write(IO.ToArray(), 0, (int) IO.Length);
            }
            //var saveData = DeflateStream.CompressBuffer(IO.ToArray());
            var saveData = ms.ToArray();
            var len = (saveData.Length + 0xF) & 0xFFFFFFF0;
            var remainder = len - saveData.Length;
            memorystream.Write(saveData, 0, saveData.Length);
            if (remainder != 0x00)
            {
                var confounder = new byte[remainder];
                new Random().NextBytes(confounder);
                memorystream.Write(confounder, 0x00, confounder.Length);
            }
            memorystream.Flush();
            var fullData = memorystream.ToArray();
            FileInfo.Checksum = CalculateChecksum(saveData);
            FileInfo.FileSize = saveData.Length;

            return fullData;
        }

        public byte[] Extract()
        {
            return IO.ToArray();
        }
        private uint CalculateChecksum(byte[] saveData)
        {
            return ElectronicArts.EACRC32.Calculate_Alt2(saveData, saveData.Length, DirtSave.Crc_Seed);
        }
    }
}
