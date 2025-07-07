using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace MaxPayne
{
    public class MP3Save
    {
        public enum MP3FileType
        {
            SP = 0x06
        }

        private Dictionary<uint, string> Grinds = new Dictionary<uint, string>();

        private EndianIO IO;

        private MP3SaveGame Save = new MP3SaveGame();

        private byte[] EncAesKey = new byte[0x20]{ 0x1A,0xB5,0x6F,0xED,0x7E,0xC3,0xFF,0x01,0x22,0x7B,0x69, 0x15, 0x33, 0x97, 0x5D, 0xCE,
                0x47, 0xD7, 0x69, 0x65, 0x3F, 0xF7, 0x75, 0x42, 0x6A, 0x96, 0xCD, 0x6D, 0x53, 0x07, 0x56, 0x5D};

        public MP3Save(EndianIO IO)
        {
            if (IO != null)
            {
                this.IO = IO;

                this.Init();

                this.Read();
            }
        }

        private void Init()
        {
            // Create the grind ids and names listing

            // Golden Guns
            Grinds.Add(0x35195E43, "Golden PT92");
            Grinds.Add(0x54A815E2, "Golden .38 Revolver");
            Grinds.Add(0x9D5A2A70, "Golden MINI-30");
            Grinds.Add(0xDCC54FFF, "Golden M82A1");
            Grinds.Add(0x582546C1, "Golden M10");
            Grinds.Add(0x3577F6E7, "Golden 1911");
            Grinds.Add(0xE8CFDAE7, "Golden M500");
            Grinds.Add(0xCE8EDFD2, "Golden SAF 40 CAL");
            Grinds.Add(0xD4586DA2, "Golden Micro 9MM");
            Grinds.Add(3675017569, "Golden LMG.30");
            Grinds.Add(1769364018, "Golden MD-97L");
            Grinds.Add(1008518143, "Golden Sawn-Off");
            Grinds.Add(4094243809, "Golden SPAS-15");
            Grinds.Add(1604588853, "Golden Auto 9MM");


            // Other Grinds
            Grinds.Add(0x98AFEAE7, "Blododbath");
            Grinds.Add(0xE60BE325, "Headmaster");
            Grinds.Add(0xA6A62BD5, "Arms Dealer");
            Grinds.Add(0xE8B8C8F3, "Leg Payne");
            Grinds.Add(0xCD0E48B2, "Below the Belt");
            Grinds.Add(0x45142B5D, "Dodge Brawl");
            Grinds.Add(0x2C18DCD5, "Take Your Time");
            Grinds.Add(0xE64E3C7F, "Bullet River");
            Grinds.Add(0x79DFB4C6, "Take a Load Off");
            Grinds.Add(0xDAD414AE, "Cam Lover");
            Grinds.Add(0x649D67FD, "The Turtle");
            Grinds.Add(0x155E3DBA, "Scrapper");
            Grinds.Add(0x320C66BF, "Pharmacist");
            Grinds.Add(0xFB9E4770, "Keep it Simple");
            Grinds.Add(0xF7229202, "Shotgun Dues");
            Grinds.Add(0x6CB848D2, "If It Ain't Broke");

        }

        private void Read()
        {
            IO.In.BaseStream.Position = 0;

            if (IO.In.ReadUInt32() != 0x6D703300 || IO.In.ReadInt32() != 0x1A )
                throw new MP3Exception("invalid header detected.");

            int saveBufferLen = IO.In.ReadInt32();
            int saveDataLen = IO.In.ReadInt32();

            if ((saveBufferLen + 0x1A) != saveDataLen)
                throw new MP3Exception("invalid save size detected.");

            int storedSum = IO.In.SeekNReadInt32(0x10), sum = 0;

            IO.In.SeekTo(0x44);

            for (int x = 0; x < saveBufferLen; x++)            
                sum += IO.In.ReadByte();

            if (sum != storedSum)
                throw new MP3Exception("save has been tampered with!");

            string StrProfileKey = string.Format("XEN43156A{0:D2}{1:D1}", 0x1A, 0x06);
            byte[] ProfileKey = new byte[0x32];
            Array.Copy(System.Text.ASCIIEncoding.ASCII.GetBytes(StrProfileKey), 0, ProfileKey, 0, StrProfileKey.Length);

            byte[] cData = this.DecryptSaveData(ProfileKey, saveBufferLen);
            var ms = new MemoryStream();

            int len = Mp3_DecompressData(cData, cData.Length, 0x000000000000c7bc, ms); // the target buffer should never be more than 0xC800

            Save.Open(ms.ToArray());

            //File.WriteAllBytes(@"C:\Users\Thierry\Desktop\Game Projects\Max Payne 3\Saves\Mine\3\MP3.save", ms.ToArray());
        }

        private byte[] DecryptSaveData(byte[] ProfileKey, int saveLen)
        {
            this.IO.In.SeekTo(0x18);
            byte[] pbData1 = IO.In.ReadBytes(0x2C);
            this.IO.In.SeekTo(0x44);
            byte[] saveData = IO.In.ReadBytes(saveLen);
            byte[] pbSaveDataLen = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(saveLen));

            HMACSHA1 sha = new HMACSHA1(ProfileKey);
            sha.TransformBlock(saveData, 0, saveLen, null, 0x00);
            sha.TransformFinalBlock(pbSaveDataLen, 0, 0x04);

            byte[] DigestKey = sha.Hash;
            byte[] hmacBuffer = new byte[0x10]{0x0F,0xC9,0x19,0xE8,0x9A,0x17,0xC4,0x5F,0xE7,0x16,0xD4,0x6C,0x3A,0x15,0x9C,0x75};
            byte[] AesKey = this.Mp3_HmacSha(DigestKey, 0x14, hmacBuffer, 0x10, 0x20, 0x7D0);

            DigestKey = Mp3_AesEcb(AesKey, pbData1, 0x2C, false);

            hmacBuffer = new byte[0x10] { 0xE1, 0x09, 0xA5, 0x42, 0xF6, 0x0A, 0x13, 0x3B, 0x81, 0xAC, 0x02, 0x55, 0xCC, 0x39, 0x40, 0x1B };
            byte[] HmacDigest = this.Mp3_HmacSha(DigestKey, 0x2C, hmacBuffer, 0x10, 0x20, 0x7D0);

            HmacDigest = Mp3_AesEcb(EncAesKey, HmacDigest, 0x20, true);

            saveData = Mp3_AesEcb2(HmacDigest, saveData, saveLen, false);

            // verifying the data
            SHA1 hasher = SHA1.Create();
            hasher.TransformBlock(ProfileKey, 0, 0x32, null, 0);
            hasher.TransformFinalBlock(pbSaveDataLen, 0, 4);

            Array.Copy(HmacSha(hasher.Hash, saveData), 0, HmacDigest, 0, 0x14);
            Array.Copy(DigestKey, 0x20, HmacDigest, 0x14, 0x04);
            Array.Copy(DigestKey, 0x24, HmacDigest, 0x18, 0x08);

            hmacBuffer = new byte[0x10] {0x15,0x08,0xE9,0x6F,0x47,0xB8,0x47,0xD1,0x3A,0x65,0x8C,0x71,0x00,0x00,0x00,0x00};
            hmacBuffer.WriteInt32(0x0C, DigestKey.ReadInt32(0x20));

            AesKey = this.Mp3_HmacSha(HmacDigest, 0x20, hmacBuffer, 0x10, 0x20, 0x7D0);
            byte[] pbData2 = new byte[0x20];
            Array.Copy(DigestKey, 0, pbData2, 0, 0x20);
            pbData2 = Mp3_AesEcb(EncAesKey, pbData2, 0x20, false);
            pbData2 = Mp3_AesEcb(AesKey, pbData2, 0x20, false);

            if(!HorizonCrypt.ArrayEquals(pbData2, HmacDigest))
                throw new MP3Exception("save data could not be verified.");

            return saveData;
        }

        private byte[] EncryptSaveData(byte[] Key, byte[] data, int savelen)
        {
            byte[] hash = null, digest, salt, keydata = new byte[0x2C];
            byte[] pbSaveDataLen = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(savelen));

            SHA1 hasher = SHA1.Create();
            hasher.TransformBlock(Key, 0, 0x32, null, 0x00);
            hasher.TransformFinalBlock(pbSaveDataLen, 0x00, 0x04);

            digest = new byte[0x20];

            Array.Copy(HmacSha(hasher.Hash, data), 0x00, digest, 0x00, 0x14);
            int seed = new Random().Next(int.MaxValue) & 0x7FFFFFFF;
            byte[] timestamp = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(DateTime.Now.ToBinary()));
            digest.WriteInt32(0x14, seed);
            Array.Copy(timestamp, 0, digest, 0x18, 0x08);
            salt = new byte[0x10] { 0x15, 0x08, 0xE9, 0x6F, 0x47, 0xB8, 0x47, 0xD1, 0x3A, 0x65, 0x8C, 0x71, 0x00, 0x00, 0x00, 0x00 };
            salt.WriteInt32(0x0C, seed);

            byte[] hmac_digest = Mp3_HmacSha(digest, 0x20, salt, 0x10, 0x20, 0x7D0);

            Array.Copy(Mp3_AesEcb(hmac_digest, digest, 0x20, false), 0x00,keydata, 0x00, 0x20);
            keydata = Mp3_AesEcb(EncAesKey, keydata, 0x20, true);

            keydata.WriteInt32(0x20, seed);
            Array.Copy(timestamp, 0x00, keydata, 0x24, 0x08);

            salt = new byte[0x10] { 0xE1, 0x09, 0xA5, 0x42, 0xF6, 0x0A, 0x13, 0x3B, 0x81, 0xAC, 0x02, 0x55, 0xCC, 0x39, 0x40, 0x1B };

            digest = Mp3_HmacSha(keydata, 0x2C, salt, 0x10, 0x20, 0x7D0);

            digest = Mp3_AesEcb(EncAesKey, digest, 0x20, true);
            byte[] savedata = Mp3_AesEcb3(digest, data, savelen, false);

           digest = HorizonCrypt.XeCryptHmacSha(Key, 0x32, savedata, savelen, pbSaveDataLen, 0x04, null, 0x00);

            salt = new byte[0x10] { 0x0F, 0xC9, 0x19, 0xE8, 0x9A, 0x17, 0xC4, 0x5F, 0xE7, 0x16, 0xD4, 0x6C, 0x3A, 0x15, 0x9C, 0x75 };

            digest = Mp3_HmacSha(digest, 0x14, salt, 0x20, 0x20, 0x7D0);

            Mp3_AesEcb(digest, keydata, 0x2C, false);

            return hash;
        }
        
        private byte[] Mp3_HmacSha(byte[] pbKey, int cbKey,  byte[] pbInp, int cbInp, int cbOut, int cbRounds)
        {
            if(cbOut == 0x00)
                return null;

            byte[] Hash = new byte[cbOut];
            HMACSHA1 sha;
            int idx = 1, len = cbOut, offset = 0;

            do
            {
                byte[] pbData1 = new byte[0x04];
                pbData1.WriteInt32(0, idx);

                sha = new HMACSHA1(pbKey);
                sha.TransformBlock(pbInp, 0, cbInp, null, 0x00);
                sha.TransformFinalBlock(pbData1, 0, 0x04);
                byte[] tmpHash = sha.Hash;
                byte[] Digest = tmpHash;

                if (cbRounds > 1)
                {
                    int rounds = cbRounds - 1;
                    do
                    {
                        sha = new HMACSHA1(pbKey);
                        sha.TransformFinalBlock(Digest, 0, 0x14);
                        Digest = sha.Hash;

                        int j = 0;
                        for (int x = 0; x < 4; x++)
                        {
                            int a = Digest[j];
                            int b = tmpHash[j];
                            int c = tmpHash[j + 1];
                            int d = Digest[j + 1];
                            int e = tmpHash[j + 2];
                            a ^= b;
                            int f = tmpHash[j + 3];
                            b = Digest[j + 3];
                            d ^= c;
                            int g = Digest[j + 2];
                            int h = tmpHash[j + 4];
                            b ^= f;
                            c = Digest[j + 4];
                            g ^= e;
                            tmpHash[j] = (byte)(a);
                            a = b & 0xFF;
                            h ^= c;
                            tmpHash[j + 1] = (byte)(d);
                            int i = g & 0xFF;
                            tmpHash[j + 3] = (byte)a;
                            d = h & 0xFF;
                            tmpHash[j + 2] = (byte)i;
                            tmpHash[j + 4] = (byte)d;
                            j += 5;
                        }

                    } while (--rounds != 0);
                }

                int dlen = len < 0x14 ? len : 0x14;

                idx++;
                Array.Copy(tmpHash, 0, Hash, offset, dlen);
                offset += dlen;
                len -= dlen;

            } while (len != 0);

            return Hash;
        }

        private byte[] HmacSha(byte[] Key, byte[] Buffer)
        {
            HMACSHA1 sha = new HMACSHA1(Key);
            sha.TransformFinalBlock(Buffer, 0, Buffer.Length);
            return sha.Hash;
        }

        private static byte[] Mp3_AesEcb(byte[] Key, byte[] pbInp,int cbInp, bool Encrypt)
        {
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

            int dataLen = pbInp.Length & ~0x0F;

            if (dataLen > 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    transform.TransformBlock(pbInp, 0, dataLen, pbInp, 0);
                }
            }
            return pbInp;
        }

        private static byte[] Mp3_AesEcb2(byte[] Key, byte[] pbInp, int cbInp, bool Encrypt)
        {
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

            int dataLen = pbInp.Length & ~0x0F;

            if (dataLen > 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    transform.TransformBlock(pbInp, cbInp - 0x10, 0x10, pbInp, cbInp - 0x10);
                }
                for (int i = 0; i < 16; i++)
                {
                    transform.TransformBlock(pbInp, 0, dataLen, pbInp, 0);
                }
            }
            return pbInp;
        }

        private static byte[] Mp3_AesEcb3(byte[] Key, byte[] pbInp, int cbInp, bool Encrypt)
        {
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

            int dataLen = pbInp.Length & ~0x0F;

            if (dataLen > 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    transform.TransformBlock(pbInp, 0, dataLen, pbInp, 0);
                }
                for (int i = 0; i < 16; i++)
                {
                    transform.TransformBlock(pbInp, cbInp - 0x10, 0x10, pbInp, cbInp - 0x10);
                }
            }
            return pbInp;
        }

        private static int Mp3_DecompressData(byte[] data, int len, int maxlen, MemoryStream outstream)
        {
            var op = new EndianIO(outstream, EndianType.BigEndian, true);
            var ip = new EndianReader(data, EndianType.BigEndian);

            int ip_end = len, a = ip.ReadByte(), b = ip.ReadByte(), err = 0, d, e = 0, f, m_pos;
            int c = Horizon.Functions.Global.ROTL32(b, 8);

            a += c;
            a += 2;
            if (a == len)
            {
                if (ip.BaseStream.Position >= ip_end)                
                    goto eof_found;
            start:
                a = ip.ReadByte();
                d = 0;

                if (ip.BaseStream.Position >= ip_end)
                    goto eof_found;

                m_pos = (int)ip.BaseStream.Position + 1;
                do
                {
                    if (d >= 8)
                        goto start;

                    c = a & 0x80;
                    e = a & 0xFF;

                    if (c == 0x00)
                    {
                        if (m_pos < ip_end)
                        {
                            a = ip.ReadByte();
                            c = ip.SeekNPeekByte(m_pos);
                            f = a >> 4;
                            c = Horizon.Functions.Global.ROTL32(c, 4);
                            f |= c;
                            c = (int)(op.Stream.Position - f) - 1;
                            if (c >= 0x00)
                            {
                                a &= 0xF;
                                a += 3;
                                m_pos += 2;
                                ip.BaseStream.Position += 1;
                                a &= 0xFF;
                                f = a;
                                if (f <= maxlen)
                                {
                                    do
                                    {
                                        f = op.In.SeekNPeekByte(c++);
                                        a = (a + 0xFF) & 0xFF;
                                        op.Out.WriteByte(f);
                                    } while(a != 0x00);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (op.Stream.Position < maxlen)
                        {
                            a = ip.ReadByte();
                            m_pos++;
                            op.Out.WriteByte(a);
                        }
                    }

                    d++;
                    a = (e << 1) & 0xFE;
                }while(ip.BaseStream.Position < ip_end);
            }

    eof_found:
            err = (int)(op.Stream.Position);
            ip.Close();
            op.Stream.Flush();

            return err;
        }

        private int Mp3_CompressData(byte[] data, int len, MemoryStream OutStream)
        {


            return 0x00;
        }
    }

    public class MP3Exception : Exception
    {
        internal MP3Exception(string Message)
            : base("Max Payne 3:" + Message)
        {
        }
    }

    // Max Payne 3 SaveGameData parser
    public class MP3SaveGame
    {
        private int DataType, LastReadValueType, ChildCount = 0x00;

        private EndianIO IO;

        private EndianReader reader
        {
            get { return IO.In; }
        }

        private List<string> Parents;

        public void Open(byte[] data)
        {
            if (data != null && data.Length > 0x04)
                IO = new EndianIO(data, EndianType.LittleEndian, true);
            else
                throw new Exception("[0xF0000001]: invalid save data detected while parsing.");

            Parents = new List<string>();

            BeginReading();
        }

        public void BeginReading()
        {
            bool exit = false;
            int k = 0xFFFC, j = 0xFFFC, i, value_type, strm_acc = 0;

            reader.SeekTo(0x00);
            if (reader.ReadString(0x4) != "RBF0")
                throw new MP3Exception("invalid savegame magic detected.");

            do
            {
                do
                {
                    j = reader.ReadInt16() & 0xFFFF;
                    if (j == 0xFFFC)
                    {
                        k = reader.ReadInt32();
                        byte[] data = reader.ReadBytes(k);
                    }
                } while (j == 0xFFFC);

                if (j != 0xFFFE)
                {
                    value_type = (j >> 12) & 0xF;
                    i = j & 0xFFF;
                }
                else
                {
                    value_type = reader.ReadInt32() >> 28;
                    i = value_type & 0xFFFFFFF;
                }

                if (value_type != 0xF && (i == strm_acc))
                {
                    k = reader.ReadInt16();
                    string Filename = reader.ReadString(k);

                    Parents.Add(Filename);
                    strm_acc++;
                }

                if (j == 0xFFFC)
                {

                }
                else if (j == 0xFFFD)
                {
                    int szlen = reader.ReadInt32(), len = szlen;
                    if (szlen != 0x00)
                    {
                        do
                        {
                            read_data_from_type(szlen, DataType, ref len);
                        } while (len != 0x00);
                        DataType = 0x00;
                    }
                }
                else if (j == 0xFFFF)
                {
                    ChildCount--;
                }
                else
                {
                    int i_value;
                    float f_value;
                    float[] fs_value;

                    if (value_type > 5)
                        LastReadValueType = 0x00;

                    else
                    {
                        switch (value_type)
                        {
                            case 0:
                                this.SeekToContent(ref strm_acc);
                                LastReadValueType = 0x00;
                                break;

                            case 1:
                                i_value = reader.ReadInt32();
                                LastReadValueType = 0x01;
                                break;

                            case 4:
                                f_value = reader.ReadSingle();
                                LastReadValueType = 0x01;
                                break;

                            case 5:
                                fs_value = new float[3];

                                for (int x = 0; x < 3; x++)
                                    fs_value[x] = reader.ReadSingle();

                                LastReadValueType = 0x01;
                                break;
                        }
                    }

                    ChildCount = LastReadValueType == 0x00 ? ChildCount + 1 : ChildCount;

                }

            } while (!exit);
        }

        private void SeekToContent(ref int strm_acces)
        {
            int a = reader.ReadInt16(), j, k, b = reader.ReadInt16(), f, c = reader.ReadInt16(), value_type, idx = 0, i;
            string Filename = string.Empty;
            if (c != 0x00)
            {
                do
                {
                    do
                    {
                        j = reader.ReadInt16() & 0xFFFF;
                        if (j == 0xFFFC)
                        {
                            k = reader.ReadInt32();
                            byte[] data = reader.ReadBytes(k);
                        }
                    } while (j == 0xFFFC);

                    if (j != 0xFFFE)
                    {
                        value_type = (j >> 12) & 0xF;
                        i = j & 0xFFF;
                    }
                    else
                    {
                        value_type = reader.ReadInt32() >> 28;
                        i = value_type & 0xFFFFFFF;
                    }

                    if (value_type != 0xF && (i == strm_acces))
                    {
                        k = reader.ReadInt16();
                        Filename = reader.ReadString(k);

                        Parents.Add(Filename);
                        strm_acces++;
                    }

                    value_type--;

                    if (value_type > 5)
                        throw new MP3Exception("invalid value detected while seeking to content.");

                    switch (value_type)
                    {
                        case 0:
                            f = reader.ReadInt32();
                            break;
                        case 5:
                            k = reader.ReadInt16();
                            string data = reader.ReadAsciiString(k);

                            if (Parents[i] == "content")
                                DataType = get_content_type(data);

                            break;
                    }

                } while (c != (++idx));
            }
        }

        private int get_content_type(string str_content)
        {
            switch (str_content)
            {
                case "text":
                case "ascii":
                    return 0x01;
                case "utf16":
                    return 0x02;
                case "binary":
                    return 0x03;
                case "raw_xml":
                    return 0x04;
                case "int_array":
                    return 0x05;
                case "char_array":
                    return 0x06;
                case "short_array":
                    return 0x07;
                case "float_array":
                    return 0x08;
                case "vector2_array":
                    return 0x09;
                case "vector3_array":
                    return 0x0A;
                case "vector4_array":
                    return 0x0B;
            }
            return -1;
        }

        private void read_data_from_type(int cblen, int data_type, ref int len)
        {
            switch (data_type)
            {
                case 0x02:
                    string str = reader.ReadUnicodeString(cblen >> 1);
                    break;
                // Binary & Char Array
                case 0x03:
                case 0x06:
                    byte[] data = reader.ReadBytes(cblen);
                    break;
                // Int, Float, Vecor2, Vector3, & Vector4 Array
                case 0x05:
                case 0x08:
                case 0x09:
                case 0x0A:
                case 0x0B:
                    for (int x = 0; x < cblen >> 2; x++)
                    {
                        int st = reader.ReadInt32();
                    }
                    break;
                case 0x07:
                    for (int x = 0; x < cblen >> 1; x++)
                    {
                        int st = reader.ReadInt32();
                    }
                    break;
            }
            len -= cblen;
        }
    }
}