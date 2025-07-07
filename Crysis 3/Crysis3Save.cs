using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Crysis;

namespace Crysis3
{
    public class Crysis3SaveGame
    {
        private readonly EndianIO _io;
        private readonly EndianIO _saveFileIO;
        private readonly CrysisCryptek _cryptek;

        internal Crysis3SaveGame(EndianIO io, CrysisCryptek cryptek)
        {
            _io = io;
            _cryptek = cryptek;

            if (io.In.ReadAsciiString(7) != "C3KeXB1")
            {
                throw new Exception("Crysis 3: invalid CryEngine 3 savegame header.");
            }
            var ms = new MemoryStream();
            cryptek.Crypt(io.In.ReadBytes(io.Length - 0x7), ms, false);
            _saveFileIO = new EndianIO(ms, EndianType.BigEndian, true);
        }

        public void Save(byte[] buffer)
        {
            var ms = new MemoryStream();
            _cryptek.Crypt(buffer, ms, true);

            long fullLength = buffer.Length + 7;
            if (fullLength != _io.Stream.Length)
            {
                _io.Stream.SetLength(fullLength);
            }

            _io.Out.SeekTo(0x0);
            _io.Out.WriteAsciiString("C3KeXB1", 7);
            _io.Out.Write(ms.ToArray());

            _io.Stream.Flush();
        }
        public MemoryStream ExtractDataBuffer()
        {
            return new MemoryStream(_saveFileIO.ToArray());
        }
        public void ExtractDataBufferToFile(string filename)
        {
            _saveFileIO.Stream.Save(filename);
        }
    }
}
