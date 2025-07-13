using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IrrationalGames
{
    public class BioShockClass
    {
        class Bsg_Unk4
        {
            internal byte Unk0;
            internal byte Unk1;
            internal int Unk2;
            internal int Unk3;
            internal int Unk4;
            internal int Unk5;
        }
        private EndianIO IO;

        private EndianIO SaveIO;

        public BioShockClass(EndianIO io)
        {
            IO = io;
            SaveIO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.SeekTo(0x40);
            while (IO.Position < IO.Length)
            {
                SaveIO.Out.Write(Ionic.Zlib.ZlibStream.UncompressBuffer(IO.In.ReadBytes(IO.In.ReadInt32())));
            }

            Read();
        }
        private void Read()
        {
            int t;
            var r = SaveIO.In;
            // start save parsing
            r.SeekTo(0);

            t = ReadUnk1();

            if (t == 2)
                r.ReadInt32();

            t = ReadUnk1();

            for (var i = 0; i < 3; i++)
                r.ReadInt32();

            r.ReadInt64();
            ReadUnk2();
            r.ReadInt32();
            ReadUnk2();
            var f = 0;
            do
            {
                ReadUnk3(t);
                if (r.ReadInt32() == 0)
                    f = 0;
            } while (f != 0);

            var struct1 = new Bsg_Unk4();
            do
            {
                ReadUnk4(struct1);
                SaveIO.In.ReadInt32();
            } while (true);
        }

        private int ReadUnk1()
        {
            var f = SaveIO.In.ReadInt32();
            var t = SaveIO.In.ReadInt32();
            if (f == 3 || f == 2)
                SaveIO.In.ReadInt32();

            return t;
        }

        private int ReadUnk2()
        {
            int j = 0;
            byte a, b, c, d;
            if (((a = SaveIO.In.ReadByte()) & 0x40) != 0)
            {
                if (((b = SaveIO.In.ReadByte()) & 0x80) != 0)
                {
                    if (((c = SaveIO.In.ReadByte()) & 0x80) != 0)
                    {
                        if (((d = SaveIO.In.ReadByte()) & 0x80) != 0)
                        {
                            j = SaveIO.In.ReadByte();
                        }
                        j = (d & 0x7F) + (j << 7);
                    }
                    j = (c & 0x7F) + (j << 7);
                }
                j = (b & 0x7F) + (j << 7);
            }

            j = (a & 0x3F) + (j << 6);
            if ((a & 0x80) != 0)
                j = ~j;

            return j;
        }

        private void ReadUnk3(int t)
        {
            ReadUnk2();
            ReadUnk2();
            if(t < 2)
                ReadUnk2();
            SaveIO.In.ReadInt32();
            ReadUnk2();
        }

        private void ReadUnk4(Bsg_Unk4 struct1)
        {
            int a, b, c, d, e;
            ReadUnk5(out a, out b);
            if(a == 0 && b == 0)
                return;
                       
            struct1.Unk2 = a;
            struct1.Unk3 = b;

            // section 2
            c = SaveIO.In.ReadByte();
            struct1.Unk1 = (byte)c;
            var cb = c & 0x0F;
            struct1.Unk0 = (byte) cb;
            if(cb == 0x0A)
                ReadUnk5(out d, out e);

            var f = (c & 0x70);
            
            var v = f;
            bool storev = true;
            switch (f)
            {
                case 0x00:
                    v = 0x01;
                    break;
                case 0x10:
                    v = 0x2;
                    break;
                case 0x20:
                    v = 0x04;
                    break;
                case 0x30:
                    v = 0x0C;
                    break;
                case 0x40:
                    v = 0x10;
                    break;
                case 0x50:
                    v = SaveIO.In.ReadByte();
                    break;
                case 0x60:
                    v = SaveIO.In.ReadInt16();
                    break;
                case 0x70:
                    v = SaveIO.In.ReadInt32();
                    break;
                default:
                    storev = false;
                    break;
            }
            if (storev)
                struct1.Unk4 = v;

            v = 0;
            if ((c & 0x80) != 0 && (cb != 0x03))
            {
                var t = struct1.Unk5;
                if (struct1.Unk5 > 0x3FFF)
                {
                    t = (t >> 0x18) + 0xC0;
                }
                else
                {
                    t = (t >> 8) + 0x80;
                }
                v = t;
                v = SaveIO.In.ReadByte();
                if ((v & 0x80) != 0)
                {
                    int x, y, z;
                    if ((v & 0xC0) == 0x80)
                    {
                        z = SaveIO.In.ReadByte();
                        v = (v << 8) & 0x7F00;
                        v +=z;
                    }
                    else
                    {
                        x = SaveIO.In.ReadByte();
                        y = SaveIO.In.ReadByte();
                        z = SaveIO.In.ReadByte();

                        v = (v << 8) & 0x3F00;
                        v += x;
                        v = ((v << 8) + y) << 8;
                        v += z;
                    }
                }
            }
            struct1.Unk5 = v;
        }

        private void ReadUnk5(out int a, out int b)
        {
            a = ReadUnk2();
            b = SaveIO.In.ReadInt32();
        }

        public byte[] ExtractData()
        {
            return SaveIO.ToArray();
        }
    }
}