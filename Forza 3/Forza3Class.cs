using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO.Compression;
using System.Data.SQLite;
using System.Data;
using ForzaMotorsport;

namespace Forza3
{
    public class Forza3Profile
    {
        private EndianIO IO;
        public EndianIO SaveIO;

        internal static byte[][] HmacKeyStack;
        internal static byte[][] AesKeyStack;
        private byte[] ProfileAesKey, ProfileHMACKey, CreatorId, AesIv, DataEx;
        private long FileSize;

        public ForzaMotorsport.ForzaProfile BaseProfile;
        public List<ForzaProfileEntry> ProfileSchemaEntries
        {
            get
            {
                return BaseProfile.ProfileSchemaEntries;
            }
        }

        public Forza3Profile(EndianIO ProfileIO, int Version, ulong ProfileId)
        {
            this.CreatorId = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(ProfileId));

            if (CreatorId.Length != 8)
                throw new ForzaException("invalid profile id detected.");

            this.IO = ProfileIO;

            this.SetupVersion(Version);

            if (this.IO.Opened)
                this.IO.Open();

            this.Read();
        }

        private void SetupVersion(int Version)
        {
            if (Version > 2)
            {
                throw new ForzaException("Unknown version of Forza 3! Please report this to: https://github.com/unknownv2");
            }

            //Create an HMAC-SHA Creator-Unique key based on the save's version number.
            this.ProfileHMACKey = ForzaSecurity.TransformSessionKey(HmacKeyStack[Version], this.CreatorId,  0);
        }

        private void Read()
        {
            //Decrypt and verify save, then pass on decrypted save data to be properly read.
            if (this.IO.In.ReadUInt32() != 0x636D7373)
            {
                throw new ForzaException("detected invalid magic.");
            }

            this.ProfileAesKey = ForzaSecurity.TransformSessionKey(AesKeyStack[0], this.CreatorId,  1);

            this.IO.In.SeekTo(0x1C);

            this.FileSize = ForzaSecurity.UnObfuscateLength2(this.IO.In.ReadInt32(),this.IO.In.ReadInt32(),this.IO.In.ReadInt32(),this.IO.In.ReadInt32(),this.IO.In.ReadInt32()); //Calculate the size of the file data

            // Validate current forza profile data
            this.IO.In.SeekTo(0x08);
            byte[] HmacSha = this.IO.In.ReadBytes(0x14); // Hmac covering header and save data            
            this.DataEx = this.IO.In.ReadBytes(0x14); // Header data, also contains the Aes-IV
            this.IO.In.SeekTo(0x30);
            byte[] SaveData = this.IO.In.ReadBytes(this.FileSize); // Actual save data

            if (!Horizon.Functions.Global.compareArray(HmacSha, ForzaSecurity.HmacSha(this.ProfileHMACKey, DataEx, SaveData)))
            {
                throw new ForzaException("profile data is invalid.");
            }
            
            Array.Copy(DataEx, 4, this.AesIv = new byte[0x10], 0, 0x10);

            ForzaSecurity.AesCbc(this.ProfileAesKey, ref SaveData, AesIv, false);

            //Read the save data now
            BaseProfile = new ForzaMotorsport.ForzaProfile(this.SaveIO = new EndianIO(SaveData, EndianType.BigEndian));
        }

        public void Save()
        {
            byte[] SaveData = this.SaveIO.ToArray();
            ForzaSecurity.AesCbc(this.ProfileAesKey, ref SaveData, this.AesIv, true);

            this.IO.Out.SeekTo(0x08);
            this.IO.Out.Write(ForzaSecurity.HmacSha(this.ProfileHMACKey, DataEx, SaveData));
            this.IO.Out.SeekTo(0x30);
            this.IO.Out.Write(SaveData);
        }

    } // Done
    public class ForzaScreenshot
    {
        /* Forza 3 Screenshot Metadata - Currently Uneeded
        public class ForzaScreenshotMetadata
        {
            private byte[] HmacKey;
            private byte[] AesKey;

            private byte[] IV;
            private byte[] DataEx;

            private EndianIO IO;
            private EndianIO MetadataIO;

            public string Creator;
            private int FileSize;
            private int UnObfuscatedFileSize;

            public ForzaScreenshotMetadata(EndianIO IO, byte[] HmacKey, byte[] AesKey)
            {
                this.HmacKey = HmacKey;
                this.AesKey = AesKey;

                this.IO = IO;

                this.Read();
            }
            private void Read()
            {
                this.IO.In.SeekTo(0x00);
                byte[] HmacSha = this.IO.In.ReadBytes(0x14); // Hmac covering header and save data
                this.IO.In.SeekTo(0x14);
                this.DataEx = this.IO.In.ReadBytes(0x14); // Header data, also contains the Aes-IV

                this.IO.In.SeekTo(0x14);
                this.UnObfuscatedFileSize = ForzaSecurity.UnObfuscateLength(this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32());

                this.FileSize = UnObfuscatedFileSize;

                this.IO.In.SeekTo(0x18);
                this.IV = this.IO.In.ReadBytes(0x10);
                this.IO.In.SeekTo(0x28);

                byte[] Metadata = this.IO.In.ReadBytes((FileSize + 0x0F) & 0xFFFFFFF0);

                if (!Horizon.Functions.Global.compareArray(HmacSha, ForzaSecurity.HmacSha(this.HmacKey, DataEx, Metadata)))
                {
                    throw new Exception("Forza 3 metadata data was found to be invalid.");
                }

                ForzaSecurity.AesCbc(this.AesKey, ref Metadata, IV, false);

                MetadataIO = new EndianIO(Metadata, EndianType.BigEndian, true);

                MetadataIO.In.SeekTo(0x18);
                int CreatorStringLength = MetadataIO.In.ReadInt32();
                Creator = MetadataIO.In.ReadString(CreatorStringLength);
            }
            public void Save()
            {
                var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

                MetadataIO.In.SeekTo(0x00);
                io.Out.Write(this.IO.In.ReadBytes(0x18));

                io.Out.Write(Creator.Length);
                io.Out.WriteAsciiString(Creator, Creator.Length);

                MetadataIO.In.SeekTo(0x18);
                int CreatorStringLength = MetadataIO.In.ReadInt32();
                MetadataIO.In.BaseStream.Position += (CreatorStringLength);

                io.Out.Write(MetadataIO.In.ReadBytes(this.FileSize - MetadataIO.In.BaseStream.Position));

                int RoundedFileSize = (int)((io.Stream.Length + 0x0F) & 0xFFFFFFF0);

                if (this.FileSize != io.Stream.Length)
                {
                    this.IO.Out.BaseStream.SetLength(0x28 + RoundedFileSize);

                    this.IO.In.SeekTo(0x18);
                    int ObfuscatedFileSize = ForzaSecurity.ObfuscateLength((int)io.Stream.Length, this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32());

                    this.IO.Out.SeekTo(0x14);
                    this.IO.Out.Write(ObfuscatedFileSize);

                    this.IO.In.SeekTo(0x14);
                    this.DataEx = this.IO.In.ReadBytes(0x14);

                    this.IO.In.SeekTo(0x18);

                    this.UnObfuscatedFileSize = ForzaSecurity.UnObfuscateLength(ObfuscatedFileSize, this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32());

                    this.FileSize = (int)io.Stream.Length;
                }

                int remainder = (int)(RoundedFileSize - io.Stream.Length);
                if (remainder != 0x00)
                {
                    byte[] confounder = new byte[remainder];
                    new Random().NextBytes(confounder);
                    io.Out.Write(confounder);
                }

                var PlainData = io.ToArray();
                io.Close();

                ForzaSecurity.AesCbc(this.AesKey, ref PlainData, this.IV, true);
                byte[] HmacHash = ForzaSecurity.HmacSha(this.HmacKey, DataEx, PlainData);

                this.IO.Out.SeekTo(0x00);
                this.IO.Out.Write(HmacHash);
                this.IO.Out.SeekTo(0x28);
                this.IO.Out.Write(PlainData);

            }
        }
        */
        private byte[] AesKey, HmacKey, IV = new byte[0x10];

        private byte[] DataEx, Creator;

        private EndianIO IO;

        private int FileSize;

        private int[] FSEncryptionHeader = new int[4];
        //public ForzaScreenshotMetadata Metadata;

        public ForzaScreenshot(EndianIO IO, ulong Creator) : this(IO, null, Creator) { }
        public ForzaScreenshot(EndianIO IO, EndianIO MetadataIO, ulong Creator)
        {
            if (!IO.Opened)
                IO.Open();

            this.IO = IO;

            this.Creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(Creator));

            this.HmacKey = ForzaSecurity.TransformSessionKey(ForzaSecurity.ForzaHmacKeyMarshal(ForzaFileTypes.Screenshots), this.Creator, 0x00);
            this.AesKey = ForzaSecurity.TransformSessionKey(ForzaSecurity.ForzaAesKeyMarshal(ForzaFileTypes.Screenshots), this.Creator, 0x01);

            if (MetadataIO != null)
            {
                if (!MetadataIO.Opened)
                    MetadataIO.Open();

               //this.Metadata = new ForzaScreenshotMetadata(MetadataIO, this.HmacKey, this.AesKey);
            }

            this.IO.In.SeekTo(0x14);
            this.DataEx = this.IO.In.ReadBytes(0x14); // Header data, also contains the Aes-IV

            this.IO.In.SeekTo(0x14);
            this.FileSize = ForzaSecurity.UnObfuscateLength(this.IO.In.ReadInt32(), FSEncryptionHeader[0] = this.IO.In.ReadInt32(),
                FSEncryptionHeader[1] = this.IO.In.ReadInt32(), FSEncryptionHeader[2] = this.IO.In.ReadInt32(), FSEncryptionHeader[3] = this.IO.In.ReadInt32());

            Array.Copy(DataEx, 4, IV, 0, 0x10);
        }
        public byte[] Read()
        {
            this.IO.In.SeekTo(0x00);
            byte[] HmacSha = this.IO.In.ReadBytes(0x14); // Hmac covering header and save data

            this.IO.In.SeekTo(0x28);

            byte[] ScreenshotData = this.IO.In.ReadBytes((this.FileSize + 0x0F) & 0xFFFFFFF0);

            if (!Horizon.Functions.Global.compareArray(HmacSha, ForzaSecurity.HmacSha(this.HmacKey, DataEx, ScreenshotData)))
            {
                throw new Exception("Forza 3 photo data was found to be invalid.");
            }    

            ForzaSecurity.AesCbc(this.AesKey, ref ScreenshotData, this.IV, false);

            return ScreenshotData;
        }
        public byte[] Write(byte[] ImageData)
        {
            this.IO.Close();

            return ForzaMotorsport.ForzaScreenshot.EncryptScreenshot(ImageData, ref DataEx, this.FSEncryptionHeader, this.AesKey, this.IV, this.HmacKey, ref FileSize, ForzaVersion.Forza3);
        }
    } // Work on photo metadata structure

    public class PlayerDatabase // Needs to be cleaned up
    {
        #region Properties

        #region Keys
        private byte[] BaseDatabaseKeyAES = new byte[0x10] { 0x2F, 0xB6, 0x14, 0xEF, 0x19, 0x6F, 0x8A, 0x44, 0x92, 0x35, 0xB7, 0x9D, 0xE6, 0x64, 0x8C, 0xE9 };
        private byte[] BaseDatabaseKeyHMAC = new byte[0x10] { 0xEC, 0xF6, 0xAE, 0x8A, 0x81, 0x16, 0xF5, 0xA8, 0xFD, 0xBD, 0x1F, 0xFA, 0x65, 0x1B, 0x12, 0x70 };
        #endregion

        #region SecureKeys
        private byte[] DatabaseAESKey;
        private byte[] DatabaseHMACKey;
        #endregion

        private EndianIO IO; // Encrypted Database
        private EndianIO DatabaseIO; // Decrypted(Compressed) Database
        private EndianIO SqlIO; // Uncompressed (SQL) Database

        private int FileSize;
        private int FullSize;
        #endregion

        public PlayerDatabase(Stream PlayerDatabaseStream, byte[] ProfileId)
        {
            // Open an Endian IO for the encrypted PlayerDatabase data
            this.IO = new EndianIO(PlayerDatabaseStream, EndianType.BigEndian, true);

            // Transform our SecureKeys
            this.DatabaseAESKey = ForzaSecurity.TransformSessionKey(this.BaseDatabaseKeyAES, ProfileId, 3);
            this.DatabaseHMACKey = ForzaSecurity.TransformSessionKey(this.BaseDatabaseKeyHMAC, ProfileId, 2);
        }

        public void Read()
        {
            this.IO.In.SeekTo(0x00);
            if (this.IO.In.ReadUInt32() != 0x636D7373)
            {
                throw new Exception("Forza 3: Player Database data has invalid header.");
            }

            this.FullSize = this.IO.In.ReadInt32();

            this.IO.In.SeekTo(0x1C);
            this.FileSize = ForzaSecurity.UnObfuscateLength2(this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32());

            this.IO.In.SeekTo(0x08);
            byte[] HmacSha = this.IO.In.ReadBytes(0x14); // Hmac covering header and save data

            this.IO.In.SeekTo(0x1C);
            byte[] DataEx = this.IO.In.ReadBytes(0x14); // Header data, also contains the Aes-IV

            this.IO.In.SeekTo(0x30);
            byte[] DatabaseData = this.IO.In.ReadBytes(this.FileSize); // Actual save data

            if (!Horizon.Functions.Global.compareArray(HmacSha, ForzaSecurity.HmacSha(this.DatabaseHMACKey, DataEx, DatabaseData)))
            {
                throw new Exception("Forza 3: Player Database data could not be verified.");
            }  

            this.DecryptPlayerDatabase();
        }

        public void InjectDB(byte[] Database, EndianIO PlayerDatabaseIO)
        {
            if (!PlayerDatabaseIO.Opened)
                PlayerDatabaseIO.Open();

            this.SqlIO.Stream.SetLength(Database.Length);
            this.SqlIO.Out.SeekTo(0x00);
            this.SqlIO.Out.Write(Database);

            this.CompressToDatabase();

            this.IO.In.SeekTo(0x00);

            PlayerDatabaseIO.Out.SeekTo(0x00);
            PlayerDatabaseIO.Out.Write(this.IO.In.ReadBytes(this.FullSize + 8));
            PlayerDatabaseIO.Close();
        }

        private void DecryptPlayerDatabase()
        {
            this.IO.In.SeekTo(0x20);
            byte[] IV = this.IO.In.ReadBytes(0x10);
            byte[] Data = this.IO.In.ReadBytes(this.FileSize);

            var aes = new AesCryptoServiceProvider();
            aes.IV = IV;
            aes.Key = this.DatabaseAESKey;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;

            // Open an Endian IO for the decryted PlayerDatabase data
            this.DatabaseIO = new EndianIO(aes.CreateDecryptor().TransformFinalBlock(Data, 0, Data.Length), EndianType.BigEndian);

            this.DecompressData(this.DatabaseIO);
        }

        private void DecompressData(EndianIO In)
        {
            if(!In.Opened)
                In.Open();

            In.In.SeekTo(0x04);
            int CompressedSize = In.In.ReadInt32();
            int DecompressedSize = In.In.ReadInt32();

            byte[] DecompressedData = new byte[DecompressedSize];

            var DeflateStream = new DeflateStream(new MemoryStream(In.In.ReadBytes(CompressedSize)), CompressionMode.Decompress);
            DeflateStream.Read(DecompressedData, 0, DecompressedSize);
            DeflateStream.Close();

            In.Close();

            this.SqlIO = new EndianIO(DecompressedData, EndianType.BigEndian, true);
        }

        public string CreateDatabase()
        {
            string FileName = System.IO.Path.GetTempFileName();

            var EndianWriter = new EndianWriter(new FileStream(FileName, FileMode.Open), EndianType.BigEndian);

            EndianWriter.Write(this.SqlIO.ToArray());

            EndianWriter.Close();

            return FileName;
        }
        // Extract the plain database to an array
        public byte[] ExtractDatabase()
        {
            return this.SqlIO.ToArray();
        }

        private void PerformHashing()
        {
            this.FixHeader();

            this.IO.In.SeekTo(0x1C);
            var DataEx = this.IO.In.ReadBytes(0x14);
            var DatabaseData = this.IO.In.ReadBytes(this.FileSize);

            var HmacHash = ForzaSecurity.HmacSha(this.DatabaseHMACKey, DataEx, DatabaseData);

            this.IO.Out.SeekTo(0x08);
            this.IO.Out.Write(HmacHash);
        }
        private void FixHeader()
        {
            this.IO.In.SeekTo(0x20);
            int ObfuscatedFileSize = ForzaSecurity.ObfuscateLength((int)this.DatabaseIO.Stream.Length, this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32());

            this.IO.Out.SeekTo(0x1C);
            this.IO.Out.Write(ObfuscatedFileSize);

            this.IO.Out.SeekTo(0x04);
            this.IO.Out.Write(FullSize);
        }
        private void CompressToDatabase()
        {
            var CompressedData = CompressData();
            int DecompressedSize = (int)this.SqlIO.Stream.Length, CompressedSize = CompressedData.Length, BodySize = (CompressedSize + 0x0C);
            int RoundedFileSize = (int)((BodySize + 0x0F) & 0xFFFFFFF0), Remainder = RoundedFileSize - BodySize;

            var memorystream = new MemoryStream();
            memorystream.SetLength(RoundedFileSize);

            memorystream.Write(CompressedData, 0, CompressedSize);

            if (Remainder != 0x00)
            {
                var confounder = new byte[Remainder];
                new Random().NextBytes(confounder);
                memorystream.Write(confounder, 0x00, confounder.Length);
            }

            memorystream.Close();

            var DatabaseData = memorystream.ToArray();

            this.DatabaseIO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            this.DatabaseIO.Stream.SetLength(DatabaseData.Length);

            this.DatabaseIO.Out.SeekTo(0x0);

            this.DatabaseIO.Out.Write(DecompressedSize);
            this.DatabaseIO.Out.Write(CompressedSize);
            this.DatabaseIO.Out.Write(DecompressedSize);

            this.DatabaseIO.Out.Write(DatabaseData, DatabaseData.Length - 0x0C);

            this.EncryptPlayerDatabase(this.DatabaseIO.ToArray());

            this.PerformHashing();
        }
        private byte[] CompressData()
        {
            var MS = new MemoryStream();
            var SqlStream = new MemoryStream(this.SqlIO.ToArray());
            var DeflateStream = new DeflateStream(MS, CompressionMode.Compress);

            var data = new byte[4096];
            int length;

            while ((length = SqlStream.Read(data, 0, 4096)) > 0)
            {
                DeflateStream.Write(data, 0, length);
            }

            SqlStream.Close();
            DeflateStream.Close();

            return MS.ToArray();
        }
        private void EncryptPlayerDatabase(byte[] DataBuffer)
        {
            this.IO.In.SeekTo(0x20);
            var IV = this.IO.In.ReadBytes(0x10);

            var aes = new AesCryptoServiceProvider();
            aes.IV = IV;
            aes.Key = this.DatabaseAESKey;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;

            this.IO.Out.SeekTo(0x30);
            this.IO.Out.Write(aes.CreateEncryptor().TransformFinalBlock(DataBuffer, 0, DataBuffer.Length));
            this.IO.Out.Flush();

            this.FileSize = DataBuffer.Length;
            this.FullSize = FileSize + 0x28;
        }

    }

    public class Forza3Livery // needs to be cleaned up
    {
        #region Properties

        public EndianIO IO;

        private long FileSize;

        private byte[] Creator;
        private byte[] DataEx;
        private byte[] IV;

        private int AesIndex;
        private int HmacIndex;
        #endregion

        #region SecureKeys

        private byte[] BaseKeyAes;
        private byte[] BaseKeyHmac;

        byte[] AESKEY;
        byte[] HMACKEY;
        #endregion

        #region Methods
        //For ordinary liveries
        public Forza3Livery(string Creator, byte[] Data, ForzaFileTypes Type)
        {
            this.Creator = Horizon.Functions.Global.hexStringToArray(Creator);

            this.InitializeSecurity(Type);

            this.IO = new EndianIO(Data, EndianType.BigEndian, true);

            this.IO.In.SeekTo(0x14);
            this.FileSize = ForzaSecurity.UnObfuscateLength(this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32(), this.IO.In.ReadInt32());

            this.LoadFileData();
        }
        private void InitializeSecurity(ForzaFileTypes Type)
        {
            switch (Type)
            {
                case ForzaFileTypes.Vinyls:
                    this.AesIndex = 2;
                    this.HmacIndex = 7;

                    this.BaseKeyAes = new byte[0x10] { 0x2C, 0xAB, 0x7C, 0xD3, 0x72, 0x63, 0xE6, 0xF2, 0xD1, 0x91, 0x39, 0x0A, 0x02, 0xE1, 0xD5, 0xC1 };
                    this.BaseKeyHmac = new byte[0x10] { 0x50, 0x76, 0xFA, 0xCB, 0x20, 0x8B, 0xE8, 0xAF, 0x3E, 0x80, 0x84, 0x46, 0x11, 0x27, 0x2B, 0xCA };

                    break;

                case ForzaFileTypes.Liveries:
                    this.AesIndex = 5;
                    this.HmacIndex = 1;

                    this.BaseKeyAes = new byte[0x10] { 0x13, 0xB9, 0x90, 0xB1, 0x33, 0x60, 0xAF, 0x48, 0xFA, 0x0B, 0x3E, 0x1A, 0x26, 0xBF, 0x42, 0x36 };
                    this.BaseKeyHmac = new byte[0x10] { 0x7F, 0x7D, 0x5E, 0x05, 0x98, 0x64, 0xC8, 0x79, 0x2E, 0x20, 0x6F, 0xFF, 0xDD, 0x0A, 0x42, 0x91 };

                    break;

                case ForzaFileTypes.SFPurchaseHistory:
                    this.AesIndex = 1;
                    //this.HmacIndex = 0;

                    this.BaseKeyAes = new byte[0x10] { 0xFE, 0xC4, 0x43, 0xAD, 0xFD, 0x44, 0x52, 0xBC, 0x38, 0x17, 0xD8, 0x8C, 0xD2, 0xAC, 0x1C, 0xFE };
                    this.BaseKeyHmac = new byte[0x10] { 0x24, 0xCF, 0x3F, 0x5F, 0x38, 0x1A, 0x3E, 0xD7, 0x9B, 0xD1, 0x70, 0xD7, 0x13, 0x95, 0x0D, 0x46 };

                    break;

                case ForzaFileTypes.CarSetups:
                    this.AesIndex = 2;
                    this.HmacIndex = 5;

                    this.BaseKeyAes = new byte[0x10] { 0xFE, 0x0B, 0x6F, 0x3C, 0x5C, 0x05, 0x6E, 0x49, 0x4C, 0x31, 0x95, 0xE5, 0xE9, 0x7B, 0x22, 0xF4 };
                    this.BaseKeyHmac = new byte[0x10] { 0x60, 0x40, 0x08, 0x3A, 0x68, 0xFD, 0xB3, 0x27, 0xCD, 0xF6, 0x29, 0x7F, 0xBF, 0x41, 0x7F, 0xBB };

                    break;

                default:
                    throw new Exception(string.Format("Forza: unsupported livery type detected: 0x{0:X8}.", Type));

            }

            this.ReloadSecureKeys();
        }

        private void LoadFileData()
        {
            this.IO.In.SeekTo(0x00);
            byte[] HmacSha = this.IO.In.ReadBytes(0x14); // Hmac covering header and save data

            this.IO.In.SeekTo(0x14);
            this.DataEx = this.IO.In.ReadBytes(0x14); // Header data, also contains the Aes-IV
            this.IO.In.SeekTo(0x18);
            this.IV = this.IO.In.ReadBytes(0x10);

            byte[] LiveryData = this.IO.In.ReadBytes((this.FileSize + 0x0F) & 0xFFFFFFF0);

            if (!Horizon.Functions.Global.compareArray(HmacSha, ForzaSecurity.HmacSha(this.HMACKEY, DataEx, LiveryData)))
            {
                throw new Exception("Forza: livery  data was found to be invalid.");
            }

            DecryptSecureFile(this.IV, LiveryData);
        }

        private void ReloadSecureKeys()
        {
            this.AESKEY = ForzaSecurity.TransformSessionKey(this.BaseKeyAes, this.Creator, AesIndex);
            this.HMACKEY = ForzaSecurity.TransformSessionKey(this.BaseKeyHmac, this.Creator, HmacIndex);
        }

        //Sets the new XUID and generates the new encryption keys
        public void SetNewCreator(byte[] NewCreator)
        {
            if (NewCreator != null && NewCreator.Length == 0x08)
            {
                this.Creator = NewCreator;
                this.ReloadSecureKeys();
            }
        }

        public void PerformHashing()
        {
            this.IO.In.SeekTo(0x14);
            byte[] header = this.IO.In.ReadBytes(0x14);
            byte[] body = this.IO.In.ReadBytes((int)this.FileSize);

            this.FixHash(header, body);
        }
        private void FixHash(byte[] header, byte[] body)
        {
            var sha = new HMACSHA1();
            sha.Key = this.HMACKEY;
            sha.TransformBlock(header, 0, 0x14, null, 0);
            sha.TransformFinalBlock(body, 0, body.Length);

            this.IO.Out.SeekTo(0);
            this.IO.Out.Write(sha.Hash);
        }

        public void UnlockCar()
        {
            int z = 0;
            this.IO.Out.SeekTo(0x30);
            this.IO.Out.Write(z);
        }
        public void UnlockDesign()
        {
            this.IO.Out.SeekTo(0x033);
            this.IO.Out.Write((byte)0);
        }
        public void UnlockCarSetup()
        {
            this.IO.Out.SeekTo(0x29);
            this.IO.Out.Write((byte)0);
        }
        public void UnlockLivery()
        {
            this.IO.Out.SeekTo(0x044);
            this.IO.Out.Write((byte)0);
            this.IO.Out.Write((short)2);
            this.IO.In.SeekTo(0);
            byte[] head = this.IO.In.ReadBytes(0x48);
            this.IO.In.BaseStream.Position += 8;
            byte[] foot = this.IO.In.ReadBytes((int)this.FileSize - 0x50);
            this.IO.Out.SeekTo(0);
            this.IO.Out.Write(head);
            this.IO.Out.Write(foot);
            this.IO.Out.Write(0xFFFFFFFFFFFFFFFF);
        }
        public void UnlockVinyl()
        {

        }

        public void PerformAesCBC(bool encrypt)
        {
            try
            {
                this.IO.In.SeekTo(0x14);
                byte[] Header = this.IO.In.ReadBytes(0x14);
                this.IO.In.SeekTo(0x18);
                byte[] IV = this.IO.In.ReadBytes(0x10);
                byte[] Data = this.IO.In.ReadBytes((int)this.FileSize);
                if (encrypt)
                {
                    this.EncryptSecureFile(IV, Data);
                    this.PerformHashing();
                }
                else this.DecryptSecureFile(IV, Data);
            }
            catch { throw new Exception("An error occured while reading or writing to the file, discard it."); }
        }
        private void EncryptSecureFile(byte[] IV, byte[] DecryptedData)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.IV = IV;
            aes.Key = this.AESKEY;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            ICryptoTransform cTransform = aes.CreateEncryptor();
            this.IO.Out.SeekTo(0x28);
            this.IO.Out.Write(cTransform.TransformFinalBlock(DecryptedData, 0, DecryptedData.Length));
        }
        private void DecryptLayerFile(byte[] IV, byte[] EncryptedData)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.IV = IV;
            aes.Key = this.AESKEY;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            ICryptoTransform cTransform = aes.CreateDecryptor();
            this.IO.In.SeekTo(0x28);
            this.IO.Out.Write(cTransform.TransformFinalBlock(EncryptedData, 0, EncryptedData.Length));
        }
        private void DecryptSecureFile(byte[] IV, byte[] EncryptedData)
        {
            var aes = new AesCryptoServiceProvider();
            aes.IV = IV;
            aes.Key = this.AESKEY;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            ICryptoTransform cTransform = aes.CreateDecryptor();
            this.IO.In.SeekTo(0x28);

            //File.WriteAllBytes(@"C:\Users\Thierry\Desktop\Game Projects\Forza 3\Saves\Liveries\secureLivery.bin", cTransform.TransformFinalBlock(EncryptedData, 0, EncryptedData.Length));
            this.IO.Out.Write(cTransform.TransformFinalBlock(EncryptedData, 0, EncryptedData.Length));
        }

        public byte[] ExtractFileData()
        {
            this.IO.In.SeekTo(0x28);
            return this.IO.In.ReadBytes((int)this.IO.Stream.Length - 0x28);
        }

        private long calc_forza_byte_len()
        {

            Int64 num, num2, num3, num4, num5, num6;

            this.IO.In.SeekTo(0x1C);
            num = this.IO.In.ReadInt32();
            num2 = this.IO.In.ReadInt32();
            num3 = this.IO.In.ReadInt32();
            num4 = this.IO.In.ReadInt32();
            num5 = this.IO.In.ReadInt32();

            num3 ^= num2;
            num3 ^= num4;
            num3 ^= num5;
            num6 = num3 ^ num;
            num3 = num6 + 0x0F;
            return num3 & 0xFFFFFFF0;
        }

        /*
        private void calc_forza_key(byte[] input_key, byte[] profile_id, ref byte[] key, int start_index)
        {
            long num = 0, num2 = 0, num3 = 0;
            for (int x = new int(); x < 0x10; x++)
            {
                num3 = x & 0x0F;
                num2 = (x + start_index) & 7;
                num3 = input_key[num3];
                num = profile_id[num2];
                num3 = num ^ num3;
                key[x] = (byte)num3;
            }
        }
        */
        #endregion
    }
    public class ForzaSecurity
    {
        internal static byte[][] AesKeyStack;
        internal static byte[][] HmacKeyStack;
        public static byte[] ForzaAesKeyMarshal(ForzaFileTypes Type)
        {
            switch (Type)
            {
                case ForzaFileTypes.ForzaProfile:
                    return AesKeyStack[0];
                case ForzaFileTypes.Screenshots:
                    return AesKeyStack[1];
                case ForzaFileTypes.PlayerDatabase:
                    return AesKeyStack[2];
                default:
                    throw new Exception("ForzaSecurity: Attempted to retrieve a key from an invalid index.");
            }
        }
        public static byte[] ForzaHmacKeyMarshal(ForzaFileTypes Type)
        {
            switch (Type)
            {
                case ForzaFileTypes.Screenshots:
                    return HmacKeyStack[0];
                case ForzaFileTypes.PlayerDatabase:
                    return HmacKeyStack[1];
                default:
                    throw new Exception("ForzaSecurity: Attempted to retrieve a key from an invalid index.");
            }
        }

        //public ForzaSecurity(
        public static void AesCbc(byte[] AESKey, ref byte[] Data, byte[] IV, bool Encrypt)
        {
            AesCryptoServiceProvider AesCrypto = new AesCryptoServiceProvider();
            AesCrypto.IV = IV;
            AesCrypto.Key = AESKey;
            AesCrypto.Mode = CipherMode.CBC;
            AesCrypto.Padding = PaddingMode.None;
            ICryptoTransform cTransform;

            if (Encrypt)
            {
                cTransform = AesCrypto.CreateEncryptor();
            }
            else
            {
                cTransform = AesCrypto.CreateDecryptor();
            }
            Data = cTransform.TransformFinalBlock(Data, 0, Data.Length);
        }
        public static byte[] HmacSha(byte[] HmacKey, byte[] Input1, byte[] Input2)
        {
            HMACSHA1 ShaHmac = new HMACSHA1();
            ShaHmac.Key = HmacKey;
            ShaHmac.TransformBlock(Input1, 0, Input1.Length, null, 0);
            ShaHmac.TransformFinalBlock(Input2, 0, Input2.Length);
            return ShaHmac.Hash;
        }
        public static byte[] TransformSessionKey(byte[] BaseKey, byte[] CreatorId, int ObfStartIndex)
        {
            byte[] key = new byte[0x10];
            Int64 num = 0, num2 = 0, num3 = 0;
            for (int x = new int(); x < 0x10; x++)
            {
                num3 = x & 0x0F;
                num2 = (x + ObfStartIndex) & 7;
                num3 = BaseKey[num3];
                num = CreatorId[num2];
                num3 = num ^ num3;
                key[x] = (byte)num3;
            }
            return key;
        }
        public static int UnObfuscateLength(int dwInput1, int dwInput2, int dwInput3, int dwInput4, int dwInput5)
        {
            dwInput3 ^= dwInput2;
            dwInput3 ^= dwInput4;
            dwInput3 ^= dwInput5;
            dwInput5 = dwInput3 ^ dwInput1;
            return dwInput5;
        }
        public static int UnObfuscateLength2(int dwInput1, int dwInput2, int dwInput3, int dwInput4, int dwInput5)
        {
            return (int)((UnObfuscateLength(dwInput1, dwInput2, dwInput3, dwInput4, dwInput5) + 0x0F) & 0xFFFFFFF0);
        }
        public static int ObfuscateLength(int Length, int dwInput1, int dwInput2, int dwInput3, int dwInput4)
        {
            int dwInput5 = dwInput1 ^ dwInput2;
            dwInput5 ^= dwInput3;
            dwInput5 ^= dwInput4;
            dwInput5 ^= Length;
            return dwInput5;
        }
    }
}