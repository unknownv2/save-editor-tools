using System;
using System.IO;
using LZO;

namespace Ubisoft
{
    internal enum AssassinsCreedGameVerison
    {
        II = 0x10,
        Brotherhood = 0x16,
        Revelations = 0x16,
        III = 0x21,
        IV = 0x23,
        Rogue = 0x23
    }
    internal class InventoryItemContainer
    {
        internal long Position;
        internal int Value;
    }
    internal class Compressor
    {
        private readonly LZOCompressor _lzoCompressor = new LZOCompressor();

        internal void ProcessDecompress(CompressionType compressionType, byte[] srcData, MemoryStream memoryStream, int outputSize)
        {
            byte[] dest;
            switch (compressionType)
            {
                case CompressionType.LZO1X:
                case CompressionType.LZO1X_999:
                    dest = _lzoCompressor.Decompress(srcData, LZOCompressionType.LZO1X, outputSize);
                    break;
                case CompressionType.LZO2A:
                    dest = _lzoCompressor.Decompress(srcData, LZOCompressionType.LZO2A, outputSize);
                    break;
                default:
                    throw new ACException("invalid compression type detected.");
            }
            if (dest != null)
            {
                memoryStream.Write(dest, 0, dest.Length);
            }
            else
            {
                throw new ACException("decompression failed");
            }
        }
        internal int ProcessCompress(CompressionType compressionType, byte[] srcData, MemoryStream memStream)
        {
            byte[] dst = null;
            switch (compressionType)
            {
                case CompressionType.LZO1X:
                    case CompressionType.LZO1X_999:
                dst = _lzoCompressor.Compress(srcData, LZOCompressionType.LZO1X);
                    break;
                    case CompressionType.LZO2A:
                dst = _lzoCompressor.Compress(srcData, LZOCompressionType.LZO2A);
                    break;
            }
            if (dst != null)
            {
                memStream.Write(dst, 0, dst.Length);
            }
            else
            {
                throw new ACException("compression failed");
            }
            return dst.Length;
        }
    }
    internal class ObjectReader
    {
        internal static int ReadInt32(EndianReader reader)
        {
            return reader.ReadInt32();
        }

        internal static bool ReadBoolean(EndianReader reader)
        {
            return Convert.ToBoolean(reader.ReadByte());
        }

        internal static string ReadAsciiString(EndianReader reader)
        {
            var stringLength = reader.ReadInt32();
            return reader.ReadAsciiString(stringLength + 1);
        }

        internal static uint ReadClassId(EndianReader reader)
        {
            return reader.ReadUInt32();
        }
    }
    internal enum ObjectType
    {
        Boolean = 0x00,

        Int32 = 0x07,

        Class = 0x12,

        Blob = 0x13,
        AsciiString = 0x1A,
        UnicodeString = 0x1B
    }

    internal struct ManagedObject
    {
        internal int Size;

        internal uint Name;
        internal long Properties;

        internal bool Owned;
        internal bool Final;

        internal byte[] Value;

        internal ObjectType Type;

        internal ManagedObject(EndianReader reader, bool bReadHeaderOnly)
        {
            int len = reader.PeekInt32();
            if (len > 0)
            {
                Size = reader.ReadInt32();
                Name = reader.ReadUInt32();

                Properties = reader.ReadInt64();
                byte flags = reader.ReadByte();
                Final = (flags & 1) == 1;
                Owned = ((flags >> 1) & 1) == 1;

                Value = !bReadHeaderOnly ? reader.ReadBytes(Size - 0xD) : null;
            }
            else
            {
                Properties = reader.ReadInt64();
                byte flags = reader.ReadByte();
                Final = (flags & 1) == 1;
                Owned = ((flags >> 1) & 1) == 1;

                Name = 0x00;
                Value = null;
                Size = 0x00;
            }
            Type = (ObjectType)((Properties & 0x7fff000000007fff) >> 0x30);
        }
        internal ManagedObject(EndianReader reader)
            : this(reader, false)
        {

        }
    }
    internal class ACException : Exception
    {
        internal ACException(string message)
            : base("Assassin's Creed: " + message)
        {

        }
    }
}
