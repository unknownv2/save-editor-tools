
using System;
using System.IO;
using System.IO.Compression;

namespace Dishonored
{
    public struct FCompressedChunkHeader
    {
        public uint Tag;
        public int BlockLength;
        public int CompressedDataLength;
        public int DecompressedDataLength;
        public int CLength;
        public int DLength;
    }
    public struct FCompressedChunkBlock
    {
        
    }

    public class GameSave
    {
        private readonly EndianIO _io;
        private EndianIO _saveIO;

        public GameSave(EndianIO io)
        {
            if (io == null)
                return;

            _io = io;
            DecompressSave();
            Read();
        }

        private void DecompressSave()
        {
            _saveIO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            _io.SeekTo(0x28);
            while (_io.Stream.Position < _io.Stream.Length)
            {
                int chunkLength = _io.In.ReadInt32();
                do
                {

                    FCompressedChunkHeader header = new FCompressedChunkHeader()
                                                        {
                                                            Tag = _io.In.ReadUInt32(),
                                                            BlockLength = _io.In.ReadInt32(),
                                                            CompressedDataLength = _io.In.ReadInt32(),
                                                            DecompressedDataLength = _io.In.ReadInt32(),
                                                            CLength = _io.In.ReadInt32(),
                                                            DLength = _io.In.ReadInt32()
                                                        };

                    _saveIO.Out.Write(Ionic.Zlib.ZlibStream.UncompressBuffer(_io.In.ReadBytes(header.CLength)));
                    chunkLength -= (header.CLength + 0x18);
                } while (chunkLength > 0x00);
            }

        }

        private void Read()
        {
            
        }
    }
}
