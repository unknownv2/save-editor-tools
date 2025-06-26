using System;
using System.Runtime.InteropServices;
using System.IO;

namespace LZO
{
    public static class LZO1X
    {
        /* Decompressor Functions */
        public static int Decompress(byte[] Input, MemoryStream MS)
        {
            int nOpCode = 0x00, n = Input.Length, chunk = 0x8000, OpCode = 0x100;

            bool bExitDecompression = false;

            var input_r = new EndianReader(new MemoryStream(Input), EndianType.BigEndian);
            var output_w = new EndianWriter(MS, EndianType.BigEndian);

            nOpCode = input_r.ReadByte();

            if (nOpCode > 0x11)
            {
                int ctr = nOpCode - 0x11;
                OpCode = 0x200;
                for (var x = 0; x < ctr; x++)
                {
                    output_w.Write(input_r.ReadByte());
                }
            }
            else
            {
                input_r.BaseStream.Position -= 1;
            }

            while (!bExitDecompression)
            {
                nOpCode = input_r.ReadByte();
                OpCode += nOpCode;

                if (OpCode <= 0x2FF)
                {
                    if (OpCode <= 15)
                    {
                        if ((OpCode % 4) == 0)
                        {
                            OpCode = D11(input_r, output_w, OpCode >> 2);
                        }
                        else
                        {
                            OpCode = D12(input_r, output_w, OpCode >> 2, OpCode % 2);
                        }
                    }
                    if ((OpCode >= 272 && OpCode < 512) || (OpCode >= 528 && OpCode <= 767))
                    {
                        OpCode %= 256;
                    }
                    if (OpCode >= 16 && OpCode < 25)
                    {
                        if ((OpCode % 8) == 0)
                        {
                            OpCode = D9(input_r, output_w, OpCode == 16 ? chunk / 2 : chunk, 0);
                        }
                        else
                        {
                            OpCode = D10(input_r, output_w,OpCode - 14, OpCode == 17);
                            bExitDecompression = OpCode == -1 ? true : bExitDecompression;
                        }
                    }
                    else if (OpCode >= 25 && OpCode < 32)
                    {
                        OpCode = D2(input_r, output_w, (OpCode - 25) + 3, 0, chunk);
                    }
                    else if (OpCode == 32)
                    {
                        OpCode = D8(input_r, output_w);
                    }
                    else if (OpCode >= 33 && OpCode < 64)
                    {
                        OpCode = D2(input_r, output_w, (OpCode - 33) + 3, 1, 0);
                    }
                    else if (OpCode >= 64 && OpCode < 256)
                    {
                        if ((OpCode % 4) == 0)
                        {
                            OpCode = D4(input_r, output_w, (OpCode >> 5) + 1, ((OpCode % 32) / 4) + 1);
                        }
                        else
                        {
                            OpCode = D3(input_r, output_w, (OpCode >> 5) + 1, ((OpCode % 32) / 4) + 1, OpCode % 4);
                        }
                    }
                    else if (OpCode == 256)
                    {
                        OpCode = D5(input_r, output_w, ref nOpCode);
                    }
                    else if (OpCode >= 257 && OpCode < 272)
                    {
                        OpCode = D1(input_r, output_w, (OpCode - 257) + 4);
                    }
                    else if (OpCode >= 512 && OpCode < 528)
                    {
                        if ((OpCode % 4) == 0)
                        {
                            OpCode = D6(input_r, output_w, (OpCode - 512) / 4);
                        }
                        else
                        {
                            OpCode = D7(input_r, output_w, (OpCode - 512) / 4, OpCode % 4);
                        }
                    }
                }
                else
                {
                    DF(input_r, output_w);
                }
            }

            int dlen = (int)output_w.BaseStream.Length;
            input_r.Close();
            output_w.Close();

            return dlen; // return the number of bytes in the decompressed buffer
        }
        private static int DF(EndianReader input, EndianWriter output)
        {
            input.BaseStream.Position -= 2;
            int ctr = input.ReadByte() & 3;
            input.BaseStream.Position += 1;

            if (ctr != 0)
            {
                output.Write(input.ReadBytes(ctr));
                return 0x00;
            }
            return 0x100;
        }
        private static int D11(EndianReader input, EndianWriter output, int offset)
        {
            long origPos = output.BaseStream.Position, position = (origPos - ROTL(input.ReadByte(), 2)) - offset;

            strmcpy(output, position, origPos, 2);
            return 0x100;
        }
        private static int D12(EndianReader input, EndianWriter output, int offset, int count)
        {
            long origPos = output.BaseStream.Position, position = (origPos - ROTL(input.ReadByte(), 2)) - offset;

            strmcpy(output, position, origPos, count);
            return 0x00;
        }
        private static int D1(EndianReader input, EndianWriter output, int count)
        {
            output.Write(input.ReadBytes(count));
            return 0x200;
        }
        private static int D2(EndianReader input, EndianWriter output, int count, int startIndex, int chunkSize)
        {
            byte a = input.ReadByte(), b = input.ReadByte();
            long origPos = output.BaseStream.Position, position = ((((origPos - ROTL(b, 6)) - (a >> 2)) - chunkSize) - startIndex);

            strmcpy(output, position, origPos, count);

            return DF(input, output);
        }
        private static int D3(EndianReader input, EndianWriter output, int count, int startIndex, int copyCount)
        {
            long origPos = output.BaseStream.Position, position = (origPos - ROTL(input.ReadByte(), 3)) - startIndex;

            strmcpy(output, position, origPos, count);

            output.Write(input.ReadBytes(copyCount));

            return 0x00;
        }
        private static int D4(EndianReader input, EndianWriter output, int count, int startIndex)
        {
            long origPos = output.BaseStream.Position, position = (origPos - ROTL(input.ReadByte(), 3)) - startIndex;

            strmcpy(output, position, origPos, count);

            return 0x100;
        }
        private static int D5(EndianReader input, EndianWriter output, ref int nOpCode)
        {
            byte c = 0;
            do
            {
                nOpCode += 0xFF;
            } while ((c = input.ReadByte()) == 0);

            int ctr = (nOpCode + c) - 0xFF;
            output.Write(input.ReadBytes(0x12 + ctr));

            return 0x200;
        }
        private static int D6(EndianReader input, EndianWriter output, int startIndex)
        {
            long origPos = output.BaseStream.Position, position = (origPos - ROTL(input.ReadByte(), 2)) - (startIndex + 0x801);
            strmcpy(output, position, origPos, 3);

            return 0x100;
        }
        private static int D7(EndianReader input, EndianWriter output, int startIndex, int copyCount)
        {
            long origPos = output.BaseStream.Position, position = (origPos - ROTL(input.ReadByte(), 2)) - (startIndex + 0x801);
            strmcpy(output, position, origPos, 3);

            output.Write(input.ReadBytes(copyCount));

            return 0x00;
        }
        private static int D8(EndianReader input, EndianWriter output)
        {
            int OpCode = 0;
            byte c = 0;
            do
            {
                OpCode += 0xFF;
            } while ((c = input.ReadByte()) == 0);

            byte a = input.ReadByte(), b = input.ReadByte();
            long origPos = output.BaseStream.Position, position = ((origPos - ROTL(b, 6)) - (a >> 2)) - 1;

            int ctr = (OpCode + c) - 0xFF;

            strmcpy(output, position, origPos, 0x21 + ctr);

            return DF(input, output);
        }
        private static int D9(EndianReader input, EndianWriter output, int chunk, int startIndex)
        {
            int OpCode = 0;
            byte c = 0;
            do
            {
                OpCode += 0xFF;
            } while ((c = input.ReadByte()) == 0);

            byte a = input.ReadByte(), b = input.ReadByte();
            long origPos = output.BaseStream.Position, position = ((origPos - ROTL(b, 6)) - (a >> 2)) - chunk;
            int ctr = (OpCode + c) - 0xFF;

            strmcpy(output, position, origPos, 0x09 + ctr);

            return DF(input, output);
        }
        private static int D10(EndianReader input, EndianWriter output, int count, bool exiting)
        {
            byte a = input.ReadByte(), b = input.ReadByte();
            long origPos = output.BaseStream.Position, position = ((origPos - ROTL(b, 6)) - (a >> 2));

            if (exiting && (origPos == position))
            {
                return -1;
            }
            else
            {
                position -= 0x4000;
                strmcpy(output, position, origPos, count);
                return DF(input, output);
            }
        }
        private static long ROTL(int value, int bits)
        {
            return ((value << bits) | (value >> (32 - bits)));
        }
        private static int ROTL32(int value, int bits)
        {
            return (int)(((value << bits) | (value >> (32 - bits))) & 0xFFFFFFFF);
        }
        private static int LOAD32(byte[] buffer, int offset)
        {
            int val = 0;
            for (int j = 0; j != 4; j++)
            {
                val = (val << 8) | buffer[offset + j];
            }
            return val;
        }

        /* Compression Functions */
        public static int Compress(byte[] input, uint in_len, MemoryStream output)
        {
            const uint M4_MARKER = 16;
            const int D_BITS = 14;

            var ip = new EndianReader(new MemoryStream(input), EndianType.LittleEndian);
            uint t = 0, l = in_len, out_len = 0;
            var op = new EndianWriter(output, EndianType.LittleEndian);
            byte[] wrkmem = new byte[0x8000];
            while (l > 20)
            {
                uint ll = lzo_min(l, 0xc000);

                uint ll_end = (uint)ip.BaseStream.Position + ll;

                if ((ll_end + ((t + ll) >> 5)) <= ll_end || (ll_end + ((t + ll) >> 5)) <= ip.BaseStream.Position + ll)
                    break;

                Array.Clear(wrkmem, 0, (1 << D_BITS) * 2);

                long origpos = ip.BaseStream.Position;
                t = do_compress(ip, ll, op, out out_len, t, wrkmem);
                ip.SeekTo(origpos + ll);
                l -= ll;
            }

            t += l;
            if (t > 0)
            {
                uint ii = in_len - t;
                if (op.BaseStream.Position == 0 && t <= 238)
                    op.WriteByte(17 + t);
                else if (t <= 3)
                {
                    stpwrite(op, -2, t);
                }
                else if (t <= 18)
                    op.Write((byte)(t - 3));
                else
                {
                    uint tt = t - 18;
                    op.WriteByte(0);
                    while (tt > 255)
                    {
                        tt -= 255;
                        op.WriteByte(0);
                    }
                    System.Diagnostics.Debug.Assert(tt > 0);
                    op.WriteByte(tt);
                }
                do { copy(op, ip, ii++, 1); } while (--t > 0);
            }

            op.WriteByte(M4_MARKER | 1);
            op.WriteByte(0);
            op.WriteByte(0);

            out_len = (uint)op.BaseStream.Length;

            ip.Close();
            op.Close();

            return (int)out_len; // return the number of bytes in the compressed buffer
        }
        private static uint do_compress(EndianReader input, uint in_len, EndianWriter output, out uint out_len, uint ti, byte[] wrkmem)
        {
            const int D_BITS = 14, D_MASK = ((1 << (D_BITS)) - 1);
            const uint M4_MARKER = 16, M2_MAX_LEN = 8, M2_MAX_OFFSET = 0x0800, M3_MAX_LEN = 33, M3_MAX_OFFSET = 0x4000, M3_MARKER = 32,
            M4_MAX_LEN = 9;

            out_len = 0;
            uint inp = (uint)input.BaseStream.Position, in_end = inp + in_len, ip_end = inp + in_len - 20, ii = inp, outp = (uint)output.BaseStream.Position;
            var dict = new EndianIO(new MemoryStream(wrkmem), EndianType.LittleEndian, true);
            var ip = input;
            var op = output;

            ip.BaseStream.Position += (ti < 4 ? 4 - ti : 0);

            while (true)
            {
                uint m_off, m_len, m_pos = 0;

                uint dv, dindex;

            literal:
                ip.BaseStream.Position += (1 + ((ip.BaseStream.Position - ii) >> 5));
            next:
                if (ip.BaseStream.Position >= ip_end)
                    break;

                dv = read32(ip, ip.BaseStream.Position);

                dindex = lzo_dindex(dv, D_BITS, D_MASK);

                m_pos = dict.In.SeekNReadUInt16(dindex * 2) + inp;

                dict.Out.SeekNWrite(dindex * 2, (short)(ip.BaseStream.Position - inp));
                if (dv != read32(ip, m_pos))
                    goto literal;

                ii -= ti; ti = 0;
                {
                    uint t = (uint)ip.BaseStream.Position - ii;
                    if (t != 0)
                    {
                        if (t <= 3)
                        {
                            stpwrite(op, -2, t);
                            copy32(op, ip, ii);
                            op.BaseStream.Position += (t);
                        }
                        else if (t <= 16)
                        {
                            op.WriteByte(t - 3);

                            copy32(op, ip, ii);
                            op.BaseStream.Position += 4;
                            copy32(op, ip, ii + 4);
                            op.BaseStream.Position += 4;
                            copy32(op, ip, ii + 8);
                            op.BaseStream.Position += 4;
                            copy32(op, ip, ii + 12);
                            op.BaseStream.Position += 4;

                            op.BaseStream.Position -= (16 - t);
                        }
                        else
                        {
                            if (t <= 18)
                                op.WriteByte(t - 3);
                            else
                            {
                                uint tt = t - 18;
                                op.WriteByte(0);
                                while (tt > 255)
                                {
                                    tt -= 255;
                                    op.WriteByte(0);
                                }
                                System.Diagnostics.Debug.Assert(tt > 0);
                                op.WriteByte(tt);
                            }
                            do
                            {
                                copy32(op, ip, ii);
                                op.BaseStream.Position += 4;
                                copy32(op, ip, ii + 4);
                                op.BaseStream.Position += 4;
                                copy32(op, ip, ii + 8);
                                op.BaseStream.Position += 4;
                                copy32(op, ip, ii + 12);
                                op.BaseStream.Position += 4;

                                ii += 16; t -= 16;
                            } while (t >= 16);

                            if (t > 0)
                            {
                                do { copy(op, ip, ii++, 1); } while (--t > 0);
                            }
                        }
                    }
                }
                m_len = 4;
                {
                    uint a = read32(ip, ip.BaseStream.Position + m_len), b = read32(ip, m_pos + m_len), v = a ^ b;
                    if (v == 0)
                    {
                        do
                        {
                            m_len += 4;
                            v = read32(ip, ip.BaseStream.Position + m_len) ^ read32(ip, m_pos + m_len);
                            if ((ip.BaseStream.Position + m_len) >= ip_end)
                                goto m_len_done;
                        } while (v == 0);
                    }
                    uint bitpos = lzo_bitscanforward(v);
                    m_len += (bitpos) / 8;
                }
            m_len_done:
                m_off = (uint)ip.BaseStream.Position - m_pos;
                ip.BaseStream.Position += m_len;
                ii = (uint)ip.BaseStream.Position;
                if (m_len <= M2_MAX_LEN && m_off <= M2_MAX_OFFSET)
                {
                    m_off -= 1;
                    op.WriteByte(((m_len - 1) << 5) | ((m_off & 7) << 2));
                    op.WriteByte(m_off >> 3);
                }
                else if (m_off <= M3_MAX_OFFSET)
                {
                    m_off -= 1;
                    if (m_len <= M3_MAX_LEN)
                        op.WriteByte(M3_MARKER | (m_len - 2));
                    else
                    {
                        m_len -= M3_MAX_LEN;
                        op.WriteByte(M3_MARKER);
                        while (m_len > 255)
                        {
                            m_len -= 255;
                            op.WriteByte(0);
                        }
                        op.WriteByte(m_len);
                    }
                    op.WriteByte(m_off << 2);
                    op.WriteByte(m_off >> 6);
                }
                else
                {
                    m_off -= 0x4000;
                    if (m_len <= M4_MAX_LEN)
                        op.WriteByte(M4_MARKER | ((m_off >> 11) & 8) | (m_len - 2));
                    else
                    {
                        m_len -= M4_MAX_LEN;
                        op.WriteByte(M4_MARKER | ((m_off >> 11) & 8));
                        while (m_len > 255)
                        {
                            m_len -= 255;
                            op.WriteByte(0);
                        }
                        op.WriteByte(m_len);
                    }
                    op.WriteByte(m_off << 2);
                    op.WriteByte(m_off >> 6);
                }
                goto next;
            }

            out_len = (uint)op.BaseStream.Length - outp;

            dict.Close();
            return (in_end - (ii - ti));
        }
        private static void copy(EndianWriter output, EndianReader input, long readpos, int count)
        {
            long origpos = input.BaseStream.Position;
            input.SeekTo(readpos);
            if (count == 1)
                output.WriteByte(input.ReadByte());
            else
                output.Write(input.ReadBytes(count));
            input.SeekTo(origpos);
        }
        private static void copy32(EndianWriter output, EndianReader input, long readpos)
        {
            long origpos = input.BaseStream.Position;
            output.Write(input.SeekNReadUInt32(readpos));
            output.BaseStream.Position -= 4;
            input.SeekTo(origpos);
        }
        private static uint read32(EndianReader input, long position)
        {
            long originalposition = input.BaseStream.Position;
            uint val = input.SeekNReadUInt32(position);
            input.SeekTo(originalposition);
            return val;
        }
        private static void strmcpy(EndianWriter output, long readPos, long writePos, int count)
        {
            for (int x = 0; x < count; x++)
            {
                output.BaseStream.Position = readPos++;
                int a = output.BaseStream.ReadByte();

                output.SeekTo(writePos++);
                output.WriteByte(a);
            }
        }
        private static void stpwrite(EndianWriter output, int index, uint val)
        {
            output.BaseStream.Position += index;
            uint n = (uint)output.BaseStream.ReadByte() | val;
            output.BaseStream.Position -= 1;
            output.WriteByte(n);
            output.BaseStream.Position += (~index);
        }
        private static uint lzo_dindex(uint dv, int D_BITS, int D_MASK)
        {
            return (uint)(((dv * 0x1824429d) >> (32 - D_BITS)) & D_MASK);
        }
        private static uint lzo_min(uint a, uint b)
        {
            return ((a) <= (b) ? (a) : (b));
        }
        private static uint lzo_bitscanforward(uint value)
        {
            for (uint x = 0; x < 32; x++)
            {
                if ((value & (1 << (int)x)) != 0)
                    return x;
            }
            return 0xffffffff;
        }
    }
    
    internal enum LZOCompressionType
    {
        LZO1,
        LZO1A,
        LZO1B,
        LZO1C,
        LZO1F,
        LZO1X,
        LZO1Y,
        LZO1Z,
        LZO2A
    }

    internal class LZOCompressor
    {
        #region Dll-Imports
        [DllImport("lzo.dll")]
        private static extern int __lzo_init_v2(uint v, int s1, int s2, int s3, int s4, int s5,
                          int s6, int s7, int s8, int s9);

        [DllImport("lzo.dll")]
        private static extern string lzo_version_string();
        [DllImport("lzo.dll")]
        private static extern string lzo_version_date();
        [DllImport("lzo.dll")]
        private static extern int lzo1x_1_compress(
            byte[] src,
            int src_len,
            byte[] dst,
            ref int dst_len,
            byte[] wrkmem
            );
        [DllImport("lzo.dll")]
        private static extern int lzo1x_decompress(
            byte[] src,
            int src_len,
            byte[] dst,
            ref int dst_len,
            byte[] wrkmem);

        [DllImport("lzo.dll")]
        private static extern int lzo2a_decompress(
            byte[] src,
            int src_len,
            byte[] dst,
            ref int dst_len,
            byte[] wrkmem);
        
        [DllImport("lzo.dll")]
        private static extern int lzo2a_999_compress(
            byte[] src,
            int src_len,
            byte[] dst,
            ref int dst_len,
            byte[] wrkmem);
        #endregion

        private byte[] _workMemory = new byte[16384L * 4];

        static LZOCompressor()
        {
            int init = __lzo_init_v2(0x2060, sizeof(short), 4, 4,4,4, 4, 4, 4, 0x18);
            if (init != 0)
            {
                throw new Exception("Initialization of LZO-Compressor failed !");
            }
        }

        private LZOCompressionType _type;

        /// <summary>
        /// Constructor.
        /// </summary>
        internal LZOCompressor()
        {
            
        }

        internal LZOCompressor(LZOCompressionType type)
        {
            _type = type;
        }

        /// <summary>
        /// Version string of the compression library.
        /// </summary>
        public string Version
        {
            get
            {
                return lzo_version_string();
            }
        }

        /// <summary>
        /// Version date of the compression library
        /// </summary>
        public string VersionDate
        {
            get
            {
                return lzo_version_date();
            }
        }

        /// <summary>
        /// Compresses a byte array and returns the compressed data in a new
        /// array. You need the original length of the array to decompress it.
        /// </summary>
        /// <param name="src">Source array for compression</param>
        /// <returns>Byte array containing the compressed data</returns>
        public byte[] Compress(byte[] src, LZOCompressionType type)
        {
            var dst = new byte[src.Length + src.Length / 64 + 16 + 3 + 4];
            int outlen = 0;
            switch (type)
            {
                case LZOCompressionType.LZO1X:
                    lzo1x_1_compress(src, src.Length, dst, ref outlen, _workMemory);
                    break;
                case LZOCompressionType.LZO2A:
                    byte[] wrkmem = new byte[16384 * 8 * 2];
                    lzo2a_999_compress(src, src.Length, dst, ref outlen, wrkmem);
                    break;
            }
            /*
            var ret = new byte[outlen + 4];
            Array.Copy(dst, 0, ret, 0, outlen);
            byte[] outlenarr = BitConverter.GetBytes(src.Length);
            Array.Copy(outlenarr, 0, ret, outlen, 4);
            */
            return dst.ReadBytes(0, outlen);
        }
        public byte[] Compress(byte[] src)
        {
            return Compress(src, _type);
        }
        /// <summary>
        /// Decompresses compressed data to its original state.
        /// </summary>
        /// <param name="src">Source array to be decompressed</param>
        /// <returns>Decompressed data</returns>
        public byte[] Decompress(byte[] src, int dstlen)
        {
            return Decompress(src, _type, dstlen);
        }
        public byte[] Decompress(byte[] src, LZOCompressionType type, int dstlen)
        {
            byte[] dst = new byte[dstlen];
            int outlen = dstlen;
            switch (type)
            {
                case LZOCompressionType.LZO1X:
                    lzo1x_decompress(src, src.Length - 4, dst, ref outlen, _workMemory);
                    break;
                case LZOCompressionType.LZO2A:
                    lzo2a_decompress(src, src.Length - 4, dst, ref outlen, null);
                    break;
            }

            return dst;
        }
    }
}