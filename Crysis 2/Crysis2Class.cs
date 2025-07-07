using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.IO.Compression;

namespace Crysis2
{
    public class Profile
    {
        public enum Crysis2Types : byte
        {
            UInt32 = 1,
            UInt16 = 2,
            Uint8 = 3,
            Float = 5
        }
        internal byte[] AESKey;

        private EndianIO IO;

        private string[] PlayerStatisticsNames = new string[] 
        { 
            "XP", "Power", "Armor", "Stealth",
            "ArmorModuleUnlocks", "DogTagUnlocks", "ClassUnlocks", "PowerModuleUnlocks", "StealthModuleUnlocks", "WeaponUnlocks",
            "FelineAttachmentUnlocks", "KVoltAttachmentUnlocks", "ScarAttachmentUnlocks", "GrendelAttachmentUnlocks", "ScarabAttachmentUnlocks",
            "Dsg1AttachmentUnlocks", "Ms2014GaussAttachmentUnlocks", "JackalAttachmentUnlocks", "MarshallAttachmentUnlocks","MK60Mod0AttachmentUnlocks", 
            "M12NovaAttachmentUnlocks", "HammerAttachmentUnlocks", "AY69AttachmentUnlocks", "MajesticAttachmentUnlocks"
        };

        private uint[] PlayerStatisticsLocations = new uint[]
        { 
            0x0D, 0x16, 0x1A, 0x1E, 
            0x635, 0x644, 0x645, 0x646, 0x648, 0x649,
            0x643, 0x641, 0x640, 0x63E, 0x63F,
            0x63D, 0x63C, 0x63A, 0x63B, 0x639,
            0x638, 0x642, 0x636, 0x637
        };

        private Crysis2Types[] PlayerStatisticsCrysis2Types = new Crysis2Types[] 
        { 
            Crysis2Types.UInt32, Crysis2Types.UInt32, Crysis2Types.UInt32, Crysis2Types.UInt32, 
            Crysis2Types.Uint8,  Crysis2Types.Uint8, Crysis2Types.Uint8, Crysis2Types.Uint8, Crysis2Types.Uint8,  Crysis2Types.Uint8,
            Crysis2Types.Uint8,  Crysis2Types.Uint8, Crysis2Types.Uint8, Crysis2Types.Uint8, Crysis2Types.Uint8,
            Crysis2Types.Uint8,  Crysis2Types.Uint8, Crysis2Types.Uint8, Crysis2Types.Uint8, Crysis2Types.Uint8,
            Crysis2Types.Uint8,  Crysis2Types.Uint8, Crysis2Types.Uint8, Crysis2Types.Uint8
        };

        private int[] PlayerSettingsDataSizes = new int[]
        { 
            0x3DF, 0x3DD, 0x328
        };


        public Dictionary<string, uint> PlayerStatistics;
        private Dictionary<string, uint> PlayerStatisticsOffsets;
        private Dictionary<string, Crysis2Types> PlayerStatisticsTypes;

        internal static byte[] typeTable;

        public Profile(EndianIO EncryptedIO, byte[] AesKey)
        {            
            AESKey = AesKey;

            this.IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            for (var x = 0; x < 0x3; x++)
            {
                byte[] Data = this.CryptSetting(EncryptedIO.In.ReadBytes(0x3E0), true);
                EncryptedIO.In.BaseStream.Position += 8;
                this.IO.Stream.Write(Data, 0, PlayerSettingsDataSizes[x]);
            }

            this.CreateDictionary();

            this.Read();
        }

        private void CreateDictionary()
        {
            this.PlayerStatistics = new Dictionary<string, uint>();
            this.PlayerStatisticsOffsets = new Dictionary<string, uint>();
            this.PlayerStatisticsTypes = new Dictionary<string, Crysis2Types>();

            for (var x = 0; x < PlayerStatisticsNames.Length; x++)
            {
                this.PlayerStatisticsOffsets.Add(PlayerStatisticsNames[x], PlayerStatisticsLocations[x]);
                this.PlayerStatisticsTypes.Add(PlayerStatisticsNames[x], PlayerStatisticsCrysis2Types[x]);
            }
        }

        private void Read()
        {
            for (var x = 0; x < PlayerStatisticsOffsets.Count; x++)
            {
                this.IO.In.SeekTo(this.PlayerStatisticsOffsets[PlayerStatisticsNames[x]]);
                this.PlayerStatistics.Add(PlayerStatisticsNames[x], this.Read(this.PlayerStatisticsTypes[PlayerStatisticsNames[x]]));
            }
        }

        public byte[] Save()
        {
            for (var x = 0; x < PlayerStatisticsOffsets.Count; x++)
            {
                this.IO.Out.SeekTo(this.PlayerStatisticsOffsets[PlayerStatisticsNames[x]]);
                this.Write(this.PlayerStatisticsTypes[PlayerStatisticsNames[x]], this.PlayerStatistics[this.PlayerStatisticsNames[x]]);
            }
            
            return CryptoSave();
        }
        private byte[] CryptoSave()
        {
            var Checksums = this.CalculateChecksums();

            this.IO.Out.SeekTo(0x00);
            for (var x = 0; x < Checksums.Length; x++)
                this.IO.Out.Write(Checksums[x]);

            var Ms = new MemoryStream();

            this.IO.In.SeekTo(0x00);

            for (var x = 0; x < 0x3; x++)
            {
                var TempMs = new MemoryStream();
                var io = new EndianIO(TempMs, EndianType.BigEndian, true);

                io.Out.Write(this.IO.In.ReadBytes(PlayerSettingsDataSizes[x]));
                io.Out.Write(new byte[0x3E0 - PlayerSettingsDataSizes[x]]);

                Ms.Write(this.CryptSetting(io.ToArray(), false), 0, 0x3E0);
            }

            return Ms.ToArray();
        }
        private uint[] CalculateChecksums()
        {
            uint[] Sums = new uint[2];
            Int64 sum = 0x9E37, ctr = 0x3E4, num;
            var tempIO = new EndianIO(typeTable, EndianType.BigEndian, true);

            this.IO.In.SeekTo(0x08);
            tempIO.In.SeekTo(0x02);
		    for (int x = 0; x < ctr; x++)
		    {
                Crysis2Types Type = (Crysis2Types)(tempIO.In.ReadByte());
                num = this.Read(Type);			    
			    num += sum;
			    num += ((uint)(num & 0xFFFFFFFF) << 10);
			    num = ((int)(num & 0xFFFFFFFF) >> 6) ^ num;
                sum = num + ((uint)(num & 0xFFFFFFFF) << 3);
		    }

            Sums[0] = (uint)sum;

            this.IO.In.SeekTo(0x08);
            tempIO.In.SeekTo(0x02);
            ctr = 0xF8;
		    sum = 0x7D8B5;
            Int64 num1, num2, num3, num4, num5;		
		    for (int x = 0; x < ctr; x++)
		    {
			    uint[] nums = new uint[4];
			    for (int i = 0; i < 4; i++)
			    {
                    Crysis2Types Type = (Crysis2Types)(tempIO.In.ReadByte());
				    nums[i] = this.Read(Type);
			    }
			    num2 = nums[3];
                num3 = nums[1];
                num4 = nums[2];
                num5 = nums[0];
			    for(int j = 0; j < 0x10; j++)
			    {
				    sum += ((uint)num2 >> 11);
				    num1 = num4 ^ sum;
				    num5 = num1 + num5;
                    num3 = ((uint)num5 << 3) + num3;
                    num4 = ((uint)num3 >> 5) ^ num4;
				    num2 = (num2 + num4) + 0xE9;
			    }
			    num4 = (((num4 + num2) + num3) + num5);
			    sum += num4;
		    }
		    Sums[1] = (uint)sum;
            return Sums;
        }

        private uint Read(Crysis2Types Type)
        {
            uint Value = 0x00;
            switch (Type)
            {
                case Crysis2Types.UInt32:
                    Value = this.IO.In.ReadUInt32();
                    break;
                case Crysis2Types.UInt16:
                    Value = this.IO.In.ReadUInt16();
                    break;
                case Crysis2Types.Uint8:
                    Value = this.IO.In.ReadByte();
                    break;
                case Crysis2Types.Float:
                    float TempValue = this.IO.In.ReadSingle();
                    Value = Convert.ToUInt32(Math.Floor(TempValue));
                    break;
                default:
                    throw new Exception(string.Format("Crysis2Profile: attempted to read an unsupported data type: {0}.", Type));
            }
            return Value;
        }
        private void Write(Crysis2Types Type, uint Value)
        {
            switch (Type)
            {
                case Crysis2Types.UInt32:
                    this.IO.Out.Write(Value);
                    break;
                case Crysis2Types.UInt16:
                    this.IO.Out.Write((ushort)Value);
                    break;
                case Crysis2Types.Uint8:
                    this.IO.Out.Write((byte)Value);
                    break;
                case Crysis2Types.Float:
                    this.IO.Out.Write((float)Value);
                    break;
                default:
                    throw new Exception(string.Format("Crysis2Profile: attempted to write an unsupported data type: {0}.", Type));
            }
        }

        private byte[] CryptSetting(byte[] Input, bool Decrypt)
        {
            var AesCrypto = new AesCryptoServiceProvider();
            AesCrypto.IV = new byte[0x10];
            AesCrypto.Key = AESKey;
            AesCrypto.Mode = CipherMode.CBC;
            AesCrypto.Padding = PaddingMode.None;
            ICryptoTransform cTransform = null;
            if (Decrypt)
            {
                cTransform = AesCrypto.CreateDecryptor();
            }
            else
            {
                cTransform = AesCrypto.CreateEncryptor();
            }
            return cTransform.TransformFinalBlock(Input, 0, Input.Length);
        }

        public void SaveToFile(string FileName)
        {
            this.IO.Stream.Save(FileName);
        }
        public void SaveFromFile(string FileName)
        {
            byte[] SettingsData = File.ReadAllBytes(FileName);
            if (SettingsData.Length == this.IO.Stream.Length)
            {
                this.IO.Out.SeekTo(0x00);
                this.IO.Out.Write(SettingsData);
            }
        }
    }

    public class Cry2Crypt
    {
        private byte[] Context;

        private static byte[] Cry2CryptoTable =
        {
            0x77, 0x89, 0x70, 0x0D, 0x0E, 0x0A, 0x64, 0xA1, 0x3F, 0x01, 0x54, 0x28, 0x41, 0xB4, 0xB7, 0xBD, 
            0xDC, 0xAF, 0x1E, 0x8B, 0xBB, 0x18, 0xDA, 0x31, 0x1C, 0x74, 0x8F, 0xA8, 0xF2, 0x9E, 0xCA, 0xDD, 
            0xE9, 0x04, 0x33, 0x92, 0x6D, 0xCF, 0x5D, 0x30, 0x75, 0x09, 0x02, 0xA6, 0xE7, 0x94, 0x81, 0x9F, 
            0xFD, 0xA4, 0xC8, 0x29, 0x46, 0xB0, 0x78, 0x2D, 0xC5, 0xD3, 0xD9, 0x96, 0x15, 0x3C, 0x68, 0x72, 
            0x4C, 0x25, 0xA5, 0x59, 0x5E, 0x1D, 0x60, 0x06, 0x21, 0x5B, 0x51, 0xD8, 0x7D, 0x88, 0x48, 0x27, 
            0x44, 0x23, 0x26, 0xE3, 0x56, 0xAE, 0x24, 0x6B, 0xFE, 0x38, 0x6E, 0x50, 0xB9, 0xC3, 0x47, 0xF7, 
            0x5C, 0xB8, 0x93, 0xC1, 0x87, 0xC2, 0xF3, 0xF8, 0x55, 0x8D, 0x39, 0xA7, 0x52, 0x32, 0x34, 0x3B, 
            0xA9, 0x7A, 0xA3, 0xFC, 0xC0, 0xF9, 0x7C, 0x4E, 0xBA, 0x16, 0xA0, 0xED, 0x42, 0xF5, 0x12, 0x5F, 
            0xAA, 0x85, 0xE2, 0x8A, 0x82, 0x2B, 0xD4, 0x86, 0x3E, 0x84, 0xEF, 0x62, 0x07, 0x66, 0xE1, 0x17, 
            0xE8, 0xAB, 0x4D, 0xE5, 0xF1, 0xEC, 0x97, 0x67, 0x91, 0x4F, 0x1A, 0x3D, 0x13, 0xAD, 0x11, 0x43, 
            0x20, 0x76, 0xCB, 0xAC, 0x9D, 0xEA, 0x65, 0x83, 0xFF, 0x58, 0xCD, 0x9A, 0x08, 0xC6, 0xF6, 0x36, 
            0x8E, 0xCE, 0x3A, 0x0F, 0x98, 0xB6, 0xD2, 0x99, 0xC7, 0x1F, 0x5A, 0x37, 0xB5, 0xCC, 0xF0, 0xC9, 
            0x53, 0xBE, 0x79, 0x14, 0x45, 0xD1, 0x6A, 0xD6, 0x0C, 0x90, 0x7E, 0xFA, 0x95, 0xD0, 0x2E, 0xDB, 
            0xE6, 0xB2, 0x10, 0x71, 0x9B, 0x57, 0xBC, 0x6F, 0xE4, 0x63, 0x8C, 0xEB, 0xA2, 0x69, 0x0B, 0x00, 
            0xF4, 0xD7, 0x05, 0x19, 0x7B, 0x2C, 0x4B, 0x03, 0x80, 0x2F, 0xE0, 0xDF, 0x61, 0xB3, 0x4A, 0xD5, 
            0xC4, 0x6C, 0x35, 0x73, 0x9C, 0xB1, 0x1B, 0x2A, 0x22, 0xFB, 0xDE, 0x40, 0xEE, 0xBF, 0x7F, 0x49
        };

        private int TableIndex; // 0x204
        private int NextIndex; // 0x20C

        public Cry2Crypt()
        {
            this.Context = new byte[0x200];

            this.Init();
        }
        private void Init()
        {
            for (var x = 0; x < 0x200; x += 0x100)
                Array.Copy(Cry2CryptoTable, 0, this.Context, 0 + x, 0x100);

            this.TableIndex = 0;
            this.NextIndex = 0xEB;
        }
        public void Crypt(byte[] pInput, Stream pOutput, bool Encrypt)
        {
            if (pInput != null || pInput.Length > 0 || pOutput != null)
            {
                if (Encrypt)
                {
                    // Re-initialize the class
                    this.Init();
                }
                var writer = new EndianWriter(pOutput, EndianType.BigEndian);
                for (var x = 0; x < pInput.Length; x++)
                {
                    int SecondIndex = (this.TableIndex + 1) & 0xFF;

                    this.TableIndex = SecondIndex;

                    this.NextIndex = (this.NextIndex + this.Context[0x100 + SecondIndex]) & 0xFF;

                    byte A = this.Context[0x100 + this.NextIndex];
                    byte B = this.Context[0x100 + SecondIndex];

                    this.Context[0x100 + this.NextIndex] = B;
                    this.Context[0x100 + this.TableIndex] = A;

                    writer.Write((byte)(pInput[x] ^ (this.Context[0x100 + ((this.Context[0x100 + this.NextIndex] + this.Context[0x100 + this.TableIndex]) & 0xFF)]) & 0xFF));
                }
            }
        }

        public byte[] DecryptCryXBFile(byte[] EncryptedBuffer)
        {
            var MS = new MemoryStream();
            this.Crypt(EncryptedBuffer, MS, false);
            return MS.ToArray();            
        }
    }

    public class StreamCipher
    {
    	byte[]		m_StartS = new byte[256];
        byte[]		m_S = new byte[256];
        int			m_StartI;
        int			m_I;
        int			m_StartJ;
        int			m_J;

        public StreamCipher()
        {

        }

        public void Init(byte[] key, int keyLen)
        {
            int i, j;

            for (i = 0; i < 256; i++)
            {
                m_S[i] = (byte)i;
            }

            if (key != null)
            {
                for (i = j = 0; i < 256; i++)
                {
                    byte temp;

                    j = (j + key[i % keyLen] + m_S[i]) & 255;
                    temp = m_S[i];
                    m_S[i] = m_S[j];
                    m_S[j] = temp;
                }
            }

            m_I = m_J = 0;

            for (i = 0; i < 1024; i++)
            {
                GetNext();
            }

            Array.Copy(m_S, m_StartS, m_StartS.Length);

            m_StartI = m_I;
            m_StartJ = m_J;
        }

        private byte GetNext()
        {
            byte tmp;

            m_I = (m_I + 1) & 0xff;
            m_J = (m_J + m_S[m_I]) & 0xff;

            tmp = m_S[m_J];
            m_S[m_J] = m_S[m_I];
            m_S[m_I] = tmp;

            return m_S[(m_S[m_I] + m_S[m_J]) & 0xff];
        }

        public void ProcessBuffer(byte[] input, int inputLen, byte[] output)
        {
            Array.Copy(m_StartS, m_S, m_S.Length);
            m_I = m_StartI;
            m_J = m_StartJ;

            for (int i = 0; i < inputLen; i++)
            {
                output[i] = (byte)(input[i] ^ GetNext());
            }
        }
    }

    public class ProfileSave
    {
        private EndianIO IO, SaveFileIO;
        private Cry2Crypt CryptoEngine;

        public ProfileSave(EndianIO IO)
        {
            if(!IO.Opened)
                IO.Open();

            if (IO.In.ReadAsciiString(7) != "CRYeXB1")
            {
                throw new Exception("Crysis 2: invalid CryEngine save header.");
            }

            this.CryptoEngine = new Cry2Crypt();

            this.IO = IO;

            this.SaveFileIO = new EndianIO(this.CryptoEngine.DecryptCryXBFile(this.IO.In.ReadBytes(this.IO.Stream.Length - 0x7)), EndianType.BigEndian, true);
        }

        public void Save(byte[] Buffer)
        {
            var MS = new MemoryStream();
            this.CryptoEngine.Crypt(Buffer, MS, true);

            long FullLength = Buffer.Length + 7;
            if (FullLength != this.IO.Stream.Length)
            {
                this.IO.Stream.SetLength(FullLength);
            }

            this.IO.Out.SeekTo(0x0);
            this.IO.Out.WriteAsciiString("CRYeXB1", 7);
            this.IO.Out.Write(MS.ToArray());

            this.IO.Stream.Flush();
        }

        public MemoryStream ExtractDataBuffer()
        {
            return new MemoryStream(this.SaveFileIO.ToArray());
        }
        public void ExtractDataBufferToFile(string Filename)
        {
            this.SaveFileIO.Stream.Save(Filename);
        }
    }

    public class GameSave
    {
        private EndianIO IO, SaveFileIO, FileIO;
        private Cry2Crypt CryptoEngine;

        public GameSave(EndianIO IO)
        {
            if (!IO.Opened)
                IO.Open();

            if (IO.In.ReadAsciiString(7) != "CRYeXB1")
            {
                throw new Exception("Crysis 2: invalid CryEngine save header.");
            }

            this.CryptoEngine = new Cry2Crypt();

            this.IO = IO;

            this.Read();
        }

        private void Read()
        {
            SaveFileIO = new EndianIO(this.CryptoEngine.DecryptCryXBFile(this.IO.In.ReadBytes(this.IO.Stream.Length - 0x7)), EndianType.BigEndian, true);

            FileIO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            
            long len = SaveFileIO.Length - 0x30;

            while (len > 0)
            {
                int nComp = SaveFileIO.In.ReadInt32(), nDec = SaveFileIO.In.ReadInt32();
                FileIO.Out.Write(Ionic.Zlib.ZlibStream.UncompressBuffer(SaveFileIO.In.ReadBytes(nComp)));

                len -= (8 + nComp);
            }
        }
        public byte[] GetFileData()
        {
            return this.FileIO.ToArray();
        }
    }
}