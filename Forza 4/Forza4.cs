using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using ForzaMotorsport;
using System.Data;

namespace Forza4
{
    public class Forza4Profile
    {
        private EndianIO IO;
        public EndianIO SaveIO;

        private byte[] AesKey, Creator, ShaSalt, AesIv = new byte[0x10], DataEx;
        private int FileSize;

        public ForzaMotorsport.ForzaProfile Profile;

        public Forza4Profile(EndianIO IO, ulong ProfileId, byte[] baseAesKey, byte[] baseShaKey)
        {
            if (!IO.Opened)
                IO.Open();

            this.IO = IO;

            this.Creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(ProfileId));

            this.InitializeSecurity(baseAesKey, baseShaKey);

            this.Read();
        }
        private void InitializeSecurity(byte[] baseAesKey, byte[] baseShaKey)
        {
            this.ShaSalt = GlobalForzaSecurity.TransformSessionKey(baseShaKey, this.Creator, 0);

            this.AesKey = GlobalForzaSecurity.TransformSessionKey(baseAesKey, this.Creator, 1);
        }
        private void Read()
        {
            this.IO.In.SeekTo(0x28);
            this.FileSize = GlobalForzaSecurity.UnObfuscateLength2(IO.In.ReadInt32(), IO.In.ReadInt32(), IO.In.ReadInt32(), IO.In.ReadInt32(), IO.In.ReadInt32());

            this.IO.In.SeekTo(0x28);
            this.DataEx = this.IO.In.ReadBytes(0x14); // encryption header

            byte[] SaveData = this.IO.In.ReadBytes(this.FileSize);
            // verify the profile data
            this.IO.In.SeekTo(0x8);
            byte[] StoredHash = this.IO.In.ReadBytes(0x20);

            if (!Horizon.Functions.Global.compareArray(StoredHash, Forza4KeyMarshal.HashData(this.ShaSalt, SaveData, DataEx)))
            {
                throw new ForzaException("save data has been tampered with.");
            }

            // copy the IV from the encryption header - @0x04
            Array.Copy(DataEx, 4, AesIv, 0, 0x10);
            // Decrypt the profile data
            GlobalForzaSecurity.AesCbc(this.AesKey, ref SaveData, AesIv, false);

            Profile = new ForzaProfile(this.SaveIO = new EndianIO(SaveData, EndianType.BigEndian, true));
        }
        public void Save()
        {
            SaveIO.Stream.Flush();
            byte[] SaveData = this.SaveIO.ToArray();
            GlobalForzaSecurity.AesCbc(this.AesKey, ref SaveData, AesIv, true);

            this.IO.Out.SeekTo(0x08);
            this.IO.Out.Write(Forza4KeyMarshal.HashData(this.ShaSalt, SaveData, DataEx));
            // we do not need to write back the encryption header because we are not modifying the file size
            this.IO.SeekTo(0x3C);
            this.IO.Out.Write(SaveData);
        }
    }
    public class Forza4PlayerDatabase : Forza4Database
    {
        public struct Forza4CareerGarageAffinity
        {
            public int GroupId;
            public int XP;
            public int TimeDriven;
            public int DistanceDriven;

        }

        private List<Forza4CareerGarageAffinity> StoredAffinity;

        public Forza4PlayerDatabase(EndianIO IO, byte[] AesKey, byte[] ShaKey, byte[] CreatorID, ForzaSQLite DataBase)
            : base(IO, AesKey, ShaKey, CreatorID, DataBase)
        {
 
        }

        public List<Forza4CareerGarageAffinity> GetAffinityEntries()
        {
            List<Forza4CareerGarageAffinity> Manufacturers = new List<Forza4CareerGarageAffinity>();
            int TableCount = Database.GetCount("Career_Affinity");
            if (TableCount > 0)
            {
                var Table = Database.RetrieveTableData("Career_Affinity");
                foreach (DataRow r in Table.Rows)
                {
                    var ent = new Forza4CareerGarageAffinity();
                    ent.GroupId = Convert.ToInt32(r["GroupId"]);
                    ent.XP = Convert.ToInt32(r["XP"]);
                    ent.TimeDriven = Convert.ToInt32(r["TimeDriven"]); // hours, minutes, seconds
                    ent.DistanceDriven = Convert.ToInt32(r["DistanceDriven"]);  // miles

                    Manufacturers.Add(ent);
                }
            }
            else
            {
                StoredAffinity = new List<Forza4CareerGarageAffinity>();
                return null;
            }

            //Manufacturers.Sort(CompareAffinityLevelsByXP);
            return (StoredAffinity =  Manufacturers);
        }
        private int CompareAffinityLevelsByXP(Forza4CareerGarageAffinity ManufacturerA, Forza4CareerGarageAffinity ManufacturerB)
        {
            return ManufacturerB.XP.CompareTo(ManufacturerA.XP);
        }

        public void SetAffinityEntries(List<Forza4CareerGarageAffinity> Manufacturers)
        {
            foreach(var affEntry in Manufacturers)
            {
                if (!StoredAffinity.Exists(entry => entry.GroupId == affEntry.GroupId))
                {
                    var affinity = new Dictionary<string, string>();
                    affinity.Add("GroupID", affEntry.GroupId.ToString());
                    affinity.Add("XP", affEntry.XP.ToString());
                    affinity.Add("TimeDriven", affEntry.TimeDriven.ToString());
                    affinity.Add("DistanceDriven", affEntry.DistanceDriven.ToString());

                    this.Database.Insert("Career_Affinity", affinity);
                }
                else
                {
                    var StoredEntry = StoredAffinity.Find(entry => entry.GroupId == affEntry.GroupId);
                    if ((StoredEntry.XP != affEntry.XP) || (StoredEntry.TimeDriven != affEntry.TimeDriven) || (StoredEntry.DistanceDriven != affEntry.DistanceDriven))
                    {
                        this.Database.UpdateTableData("Career_Affinity", "XP", affEntry.XP.ToString(), "GroupID", affEntry.GroupId.ToString());
                        this.Database.UpdateTableData("Career_Affinity", "TimeDriven", affEntry.TimeDriven.ToString(), "GroupID", affEntry.GroupId.ToString());
                        this.Database.UpdateTableData("Career_Affinity", "DistanceDriven", affEntry.DistanceDriven.ToString(), "GroupID", affEntry.GroupId.ToString());
                    }
                }
            }
        }

        public void DeleteGroup(int groupId)
        {
            this.Database.Remove("Career_Affinity", "GroupId", groupId);
        }
    }
    public class Forza4Screenshot
    {
        private EndianIO IO;

        private byte[] AesKey, ShaKey;

        private byte[] Creator, DataEx, AesIv = new byte[0x10];
        private int[] FSEncryptionTable = new int[4];

        private int FileSize;

        private GlobalForzaSecurity Forza4Security;

        public Forza4Screenshot(EndianIO IO, ulong CreatorId, Forza4KeyMarshal Marshal)
        {
            if (!IO.Opened)
                IO.Open();

            Creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(CreatorId));

            this.AesKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaAesKeyMarshal(ForzaFileTypes.Screenshots), Creator, 1);
            this.ShaKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaSHAKeyMarshal(ForzaFileTypes.Screenshots), Creator, 0);

            this.IO = IO;

            this.IO.In.SeekTo(0x20);
            DataEx = this.IO.In.ReadBytes(0x14);  // the first 4 bytes of DataEx contain an obfuscated file size, remainder is AesIV      

            this.IO.In.SeekTo(0x20);

            this.FileSize = GlobalForzaSecurity.UnObfuscateLength(this.IO.In.ReadInt32(), FSEncryptionTable[0] = this.IO.In.ReadInt32(), FSEncryptionTable[1] = this.IO.In.ReadInt32(), FSEncryptionTable[2] = this.IO.In.ReadInt32(), FSEncryptionTable[3] = this.IO.In.ReadInt32());

            Array.Copy(DataEx, 4, AesIv, 0, 0x10);

            Forza4Security = new GlobalForzaSecurity(ForzaVersion.Forza4,this.AesKey, this.ShaKey);
        }
        public byte[] Read()
        {
            this.IO.In.SeekTo(0);
            byte[] StoredHash = this.IO.In.ReadBytes(0x20);

            this.IO.In.SeekTo(0x34);

            // round filesize because AES operates on blocks of 16 bytes
            byte[] SaveData = this.IO.In.ReadBytes((this.FileSize + 0x0F) & 0xFFFFFFF0);

            if (!Horizon.Functions.Global.compareArray(StoredHash, Forza4Security.HashData(this.ShaKey, SaveData, DataEx)))
            {
                throw new Exception("ForzaScreenshot: save data has been tampered with.");
            }

            GlobalForzaSecurity.AesCbc(this.AesKey, ref SaveData, AesIv, false);

            return SaveData;
        }
        public byte[] Write(byte[] ImageData)
        {
            this.IO.Close();

            return ForzaMotorsport.ForzaScreenshot.EncryptScreenshot(ImageData, ref DataEx, FSEncryptionTable, this.AesKey, this.AesIv, this.ShaKey, ref FileSize, ForzaVersion.Forza4);
        }
    }
    public class Forza4Database
    {
        protected EndianIO IO;
        private byte[] AesKey, ShaKey;
        private GlobalForzaSecurity SecurityProcessor;
        protected ForzaSQLite Database;

        public Forza4Database(EndianIO IO, byte[] AesKey, byte[] ShaKey, byte[] CreatorID, ForzaSQLite DataBase)
        {
            if (!IO.Opened) // Player Database
                IO.Open();

            this.Database = DataBase;

            this.IO = IO;

            this.AesKey = GlobalForzaSecurity.TransformSessionKey(AesKey, CreatorID, 3);
            this.ShaKey = GlobalForzaSecurity.TransformSessionKey(ShaKey, CreatorID, 2);

            SecurityProcessor = new GlobalForzaSecurity(ForzaVersion.Forza4, this.AesKey, this.ShaKey);

            this.IO.SeekTo(0x08);

            Read(SecurityProcessor.DecryptData(this.IO.In.ReadBytes(this.IO.Stream.Length - 0x08)).ToArray());
        }
        public Forza4Database(EndianIO IO, Forza4KeyMarshal Marshal, byte[] CreatorID, ForzaSQLite DataBase)
        {
            if (!IO.Opened) // LiveryDatabase
                IO.Open();

            this.Database = DataBase;

            this.IO = IO;

            this.AesKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaAesKeyMarshal(ForzaFileTypes.PlayerDatabase), CreatorID, 3);
            this.ShaKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaSHAKeyMarshal(ForzaFileTypes.PlayerDatabase), CreatorID, 2);

            SecurityProcessor = new GlobalForzaSecurity(ForzaVersion.Forza4, this.AesKey, this.ShaKey);

            Read(SecurityProcessor.DecryptData(this.IO.ToArray()).ToArray());
        }
        public Forza4Database(EndianIO IO, Forza4KeyMarshal Marshal, byte[] CreatorID)
        {
            if (!IO.Opened) // LiveryDatabase
                IO.Open();

            this.IO = IO;

            this.AesKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaAesKeyMarshal(ForzaFileTypes.PlayerDatabase), CreatorID, 3);
            this.ShaKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaSHAKeyMarshal(ForzaFileTypes.PlayerDatabase), CreatorID, 2);

            SecurityProcessor = new GlobalForzaSecurity(ForzaVersion.Forza4, this.AesKey, this.ShaKey);

            Read(SecurityProcessor.DecryptData(this.IO.ToArray()).ToArray());
        }
        public DataTable GetDataTable(string TableName)
        {
            return Database.RetrieveTableData(TableName);
        }

        private void Read(byte[] Input)
        {
             byte[] DatabaseData = ForzaDatabase.Decompress(Input);

            string FileName = System.IO.Path.GetTempFileName();

            FileStream DbFile = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            DbFile.Write(DatabaseData, 0, DatabaseData.Length);
            DbFile.Flush();

            if (Database != null)
                Database.SetDataSource(FileName, DbFile);
            else
                Database = new ForzaSQLite(FileName, DbFile);
        }
        public byte[] Extract()
        {
            return this.Database.Extract();
        }
        public void Save(bool LiveryDatabase)
        {
            byte[] DataFile = Extract();
            int dataLength = DataFile.Length, pos = 0;

            var ms = new MemoryStream();
            var dbwriter = new EndianWriter(ms, EndianType.BigEndian);
            dbwriter.Write(dataLength);

            while (dataLength > 0)
            {
                int readlen = dataLength;
                if (readlen >= 0x200000)
                    readlen = 0x200000;

                byte[] sdata = new byte[readlen];
                Array.Copy(DataFile, pos, sdata, 0, readlen);
 
                byte[] CompressedData = ForzaDatabase.Compress(sdata);

                dbwriter.Write(CompressedData.Length);
                dbwriter.Write(sdata.Length);
                dbwriter.Write(CompressedData);

                pos += readlen;
                dataLength -= readlen;
            }

            byte[] out_data = SecurityProcessor.EncryptData(ms.ToArray());
            dbwriter.Close();
            if (LiveryDatabase)
            {
                this.IO.Out.SeekTo(0x00);
                this.IO.Out.Write(out_data);
            }
            else
            {
                this.IO.Out.SeekTo(8);
                this.IO.Out.Write(out_data);
                this.IO.Out.SeekTo(4);
                this.IO.Out.Write((int)out_data.Length);
            }
        }
    }
    public class Forza4Garage : Forza4Database
    {
        public class Forza4GarageCar
        {
            public int CarId;
            public int Key;
            public string OriginalOwner;
            public int NumberOfOwners;
            public string ThumbnailPath;
            public Forza4CarbinReader CarReader;

            public bool IsModified;
        }

        public struct Forza4Car
        {
            public string ThumbnailPath;
            public int CarId;
        }

        public class Forza4GaragerSort : IComparer<Forza4GarageCar>
        {
            public int Compare(Forza4GarageCar a, Forza4GarageCar b)
            {
                if (a.Key > b.Key) return 1;
                else if (a.Key < b.Key) return -1;
                else return 0;
            }
        }
        public long ProfileId;

        private List<Forza4GarageCar> StoredCars;

        public Forza4Garage(EndianIO IO, byte[] AesKey, byte[] ShaKey, byte[] CreatorId, ForzaSQLite DataBase)
            : base(IO, AesKey, ShaKey, CreatorId, DataBase)
        {
            ProfileId = CreatorId.ToLong(true);
        }

        public List<Forza4GarageCar> GetGarageCars()
        {
            //Database.ExtractCarBins();

            StoredCars = new List<Forza4GarageCar>();
            var Table = Database.RetrieveTableData("Career_Garage");

            /*
            StringReader txtReader = new StringReader(Database.GetShema("Career_Garage"));

            var txtWriter = new StreamWriter(@"C:\Users\Thierry\Desktop\Game Projects\Forza Motorsport 4\Garage_Schema2.txt");
            string line;
            while((line = txtReader.ReadLine()) != null)
            {
                //line = line.Remove(line.Length - 2, 2);
                //line = line.Remove(0, 1);
                txtWriter.WriteLine(string.Format("{0}", line));
            }
            txtWriter.Close();
            */

            foreach (DataRow car in Table.Rows)
            {
                StoredCars.Add(new Forza4GarageCar() 
                { 
                    CarId = Convert.ToInt32(car["CarId"]),
                    OriginalOwner = Convert.ToString(car["OriginalOwner"]),
                    NumberOfOwners = Convert.ToInt32(car["NumOwners"]),
                    Key = Convert.ToInt32(car["Id"]),
                    ThumbnailPath = Convert.ToString(car["Thumbnail"])
                });
            }
            if (StoredCars.Count > 0x00)
            {
                Forza4GaragerSort carSort = new Forza4GaragerSort();
                StoredCars.Sort(carSort);
            }
            return StoredCars;
        }

        public List<Forza4Car> SetGarageList(List<Forza4GarageCar> GarageList, out List<string> RemovableThumbPaths)
        {
            int CarIndex = StoredCars[StoredCars.Count - 1].Key;
            List<Forza4Car> NewCarThumbPaths = new List<Forza4Car>();
            RemovableThumbPaths = new List<string>();
            for(int x = 0; x < GarageList.Count; x++)
            {
                var Car = GarageList[x];

                if (Car.IsModified && Car.CarReader != null)
                {
                    var rCar = Car.CarReader;
                    string ThumbnailPath = string.Empty;
                    if (Car.Key != 0x00)
                    {
                        if (Exists(Car.Key, -1))
                        {
                            bool Removed = this.Database.Remove("Career_Garage", "Id", Car.Key);
                            RemovableThumbPaths.Add(Car.ThumbnailPath.Remove(0, 18));
                        }
                    }
                    var NewCar = new Dictionary<string, string>();
                    ThumbnailPath = string.Format(@"{0:X16}:\Thumbnails\Thumbnail_{1}.xdc", ProfileId, ++CarIndex);
                    while (rCar.Read())
                    {
                        switch (rCar.GetValueName())
                        {
                            case "Id":
                                NewCar.Add("Id", CarIndex.ToString());
                                break;
                            case "CarId":
                                NewCar.Add("CarId", Car.CarId.ToString());
                                break;
                            case "CarGroup":
                                NewCar.Add("CarGroup", rCar.GetNextValue().Replace("-1", "NULL"));
                                break;
                            case "TireBrand":
                                NewCar.Add("TireBrand", rCar.GetNextValue().Replace("-1", "NULL"));
                                break;
                            case "Thumbnail":
                                NewCar.Add("Thumbnail", ThumbnailPath);
                                break;
                            case "OriginalOwner":
                                NewCar.Add("OriginalOwner", Car.OriginalOwner);
                                break;
                            case "NumOwners":
                                NewCar.Add("NumOwners", Car.NumberOfOwners.ToString());
                                rCar.SkipNextValue();
                                break;
                            default:
                                NewCar.Add(rCar.GetValueName(), rCar.GetNextValue());
                                break;
                        }
                    }
                    rCar.Close();

                    this.Database.Insert("Career_Garage", NewCar);

                    NewCarThumbPaths.Add(new Forza4Car()
                    {
                        CarId = Car.CarId,
                        ThumbnailPath = ThumbnailPath.Remove(0, 18)
                    });
                    Car.Key = CarIndex;
                }
            }
            return NewCarThumbPaths;
        }

        public bool Exists(int CarKey, int CarId)
        {
            if (CarKey != -1 && CarId != -1)
                return StoredCars.Exists(entry => ((entry.Key == CarKey) && (entry.CarId == CarId)));
            if (CarKey != -1)
                return StoredCars.Exists(entry => entry.Key == CarKey);
            if (CarId != -1)
                return StoredCars.Exists(entry => entry.CarId == CarId);

            throw new ForzaException("invalid values supplied for garage searching.");
        }
    }
    public class Forza4CarbinReader
    {
        private Crysis2.StreamCipher _streamCipher = new Crysis2.StreamCipher();
        private byte[] _key = new byte[0x10];

        private EndianReader _keyInfo;
        private string _dataType;
        private string _dataName;
        private string _info;
        private StringReader _schemaInfo;

        public Forza4CarbinReader(string Schema, EndianReader Reader, byte[] Key)
        {
            if (Key != null)
            {
                Array.Copy(Key, 0, _key, 0x00, 0x10);
            }

            SetCarReader(Reader);

            if (Schema != null && Schema != string.Empty)
                _schemaInfo = new StringReader(Schema);
        }

        public void SetCarReader(EndianReader Reader)
        {
            if (Reader != null)
            {
                _streamCipher.Init(_key, 0x10);
                byte[] CarBin = Reader.BaseStream.ToArray();
                _streamCipher.ProcessBuffer(CarBin, 0x1E8, CarBin);
                _keyInfo = new EndianReader(CarBin, EndianType.BigEndian) ;
            }
        }

        public string GetNextValue()
        {
            object value = GetValue();
            if (value != null)
            {
                switch (_dataType)
                {
                    case "INT":
                    case "INTEGER":
                        return Convert.ToInt32(value).ToString();
                    case "REAL":
                        return Convert.ToSingle(value).ToString();
                    case "TEXT":
                        return value.ToString();
                }
            }
            return null;
        }

        public string GetValueName()
        {
            return _dataName;
        }

        public object GetValue()
        {
            if(_info != null)
            {
                switch (_dataType)
                {
                    case "INT":
                    case "INTEGER":
                        return _keyInfo.ReadInt32();
                    case "REAL":
                        return _keyInfo.ReadSingle();
                    case "TEXT":
                        return _keyInfo.ReadStringNullTerminated();
                    case "NULL":
                        return string.Empty;
                }
            }
            return null;
        }

        public void SkipNextValue()
        {
            GetValue();
        }

        public bool Read()
        {
            string line;
            if ((line = _schemaInfo.ReadLine()) != null)
            {
                _info = line;
                //Schema = [{ValueName}]={ValueType}
                _dataName = _info.Substring(_info.IndexOf('[') + 1, _info.IndexOf(']') - 1);
                _dataType = _info.Substring(_info.IndexOf('=') + 1, (_info.Length - _info.IndexOf('=')) - 2);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Close()
        {
            _keyInfo.Close();
            _schemaInfo.Close();

            _dataName = null;
            _dataType = null;
            _info = null;
        }

        public void SaveTo(string FileName)
        {
            _keyInfo.BaseStream.Save(FileName);
        }
    }
    public class Forza4LiveryDatabase : Forza4Database
    {
        public Forza4LiveryDatabase(EndianIO IO, Forza4KeyMarshal Marshal, byte[] CreatorID) : base(IO, Marshal, CreatorID)
        {
        }
        public List<string> GetFilenames(ForzaFileTypes FileType)
        {
            List<string> Filenames = new List<string>();
            var Table = Database.RetrieveTableData("MetaData");
            foreach (DataRow r in Table.Rows)
            {
                if (!Convert.ToBoolean(r["IsNewFile"]))
                {
                    switch (FileType)
                    {
                        case ForzaFileTypes.Vinyls:
                            if(r["Folder"].ToString().Contains("LivGroups"))
                                Filenames.Add(string.Format("{0}\\{1:D4}_layers{2}", r["Title"].ToString(), Convert.ToUInt32(r["Layers"]) & 0xFFFF, ".lvg"));
                            break;
                        case ForzaFileTypes.CarSetups:
                            if (r["Folder"].ToString().Contains("CarSetup"))
                                Filenames.Add(string.Format("{0}{1}", r["Folder"].ToString().Substring(0xA), r["DataFileName"].ToString()));
                            break;
                        case ForzaFileTypes.Liveries:
                            if(r["Folder"].ToString().Contains("LivCatalog"))
                                Filenames.Add(string.Format("{0}\\{1}\\{2:D4}_layers_{3:D4}.{4:D4}", Convert.ToUInt32(r["Ordinal"]), r["Title"].ToString(), Convert.ToUInt32(r["NumLayers"]) & 0xFFFF, Convert.ToUInt32(r["Ordinal"]), "liv"));
                            break;
                    }
                }
            }
            return Filenames;
        }
        public List<string> GetOwners(ForzaFileTypes Type)
        {
            List<string> Owners = new List<string>();
            var Table = Database.RetrieveTableData("MetaData");
            foreach (DataRow r in Table.Rows)
            {
                if (!Convert.ToBoolean(r["IsNewFile"]))
                {
                    switch (Type)
                    {
                        case ForzaFileTypes.Vinyls:
                        case ForzaFileTypes.Liveries:
                            Owners.Add(r["Owner"].ToString());
                            break;
                        case ForzaFileTypes.CarSetups:
                            Owners.Add(r["Creator"].ToString());
                            break;
                    }
                }
            }
            return Owners;
        }
        public List<string> GetSaveIds()
        {
            List<string> SaveIds = new List<string>();
            var Table = Database.RetrieveTableData("MetaData");
            foreach (DataRow r in Table.Rows)
            {
                if (!Convert.ToBoolean(r["IsNewFile"]))
                {
                    SaveIds.Add(r["SaveID"].ToString());
                }
            }
            return SaveIds;
        }
        public List<string> GetLocks()
        {
            List<string> Locks = new List<string>();
            var Table = Database.RetrieveTableData("MetaData");
            foreach (DataRow r in Table.Rows)
            {
                if (!Convert.ToBoolean(r["IsNewFile"]))
                {
                    Locks.Add(Convert.ToBoolean(r["IsLocked"]).ToString());
                }
            }
            return Locks;
        }

        public object SearchMetaDataTableForSetting(string SearchString, string Setting)
        {
            var Table = Database.RetrieveTableData("MetaData");
            foreach (DataRow r in Table.Rows)
            {
                if (r["SaveID"].ToString() == SearchString)
                {
                    return r[Setting];
                }
            }
            return null;
        }
        public object SearchTableForSetting(string SearchString, string Setting, DataTable Table)
        {
            foreach (DataRow r in Table.Rows)
            {
                if (r["SaveID"].ToString() == SearchString)
                {
                    return r[Setting];
                }
            }
            return null;
        }
        public object SearchTableForSetting(string SearchString, string Setting, string TableName)
        {
            var Table = Database.RetrieveTableData(TableName);
            foreach (DataRow r in Table.Rows)
            {
                if (r["SaveID"].ToString() == SearchString)
                {
                    return r[Setting];
                }
            }
            return null;
        }
        public object SearchDatabaseForSetting(string SearchString, string Setting, ForzaFileTypes LivType)
        {
            List<string> Owners = new List<string>();
            var Table = Database.RetrieveTableData("MetaData");
            foreach (DataRow r in Table.Rows)
            {
                switch (LivType)
                {
                    case ForzaFileTypes.Vinyls:
                        if (string.Format("{0}\\{1:D4}_layers{2}", r["Title"].ToString(), Convert.ToUInt32(r["Layers"]) & 0xFFFF, ".lvg") == SearchString)
                        {
                            return r[Setting];
                        }
                        break;
                    case ForzaFileTypes.CarSetups:
                        if (r["Folder"].ToString() == string.Format("{0}{1}", "CarSetup:\\", SearchString.Substring(0, SearchString.LastIndexOf("\\") + 1)))
                        {
                            return r[Setting];
                        }
                        break;
                    case ForzaFileTypes.Liveries:
                        if (string.Format("{0}\\{1}\\{2:D4}_layers_{3:D4}.{4:D4}", Convert.ToUInt32(r["Ordinal"]), r["Title"].ToString(), Convert.ToUInt32(r["NumLayers"]) & 0xFFFF, Convert.ToUInt32(r["Ordinal"]), "liv") == SearchString)
                        {
                            return r[Setting];
                        }
                        break;
                }
            }
            return null;
        }
        public void UpdateSetting(string Record, string RecordId, string Value)
        {
            this.Database.UpdateTableData("MetaData", Record, Value, "SaveID", RecordId);
        }
    }
    public class Forza4CarSetup : Forza4Livery
    {
        public bool IsUnlocked;
        public Forza4CarSetup(EndianIO IO, Forza4KeyMarshal KeyMarshal, byte[] CreatorId)
            : base(IO, ForzaFileTypes.CarSetups, KeyMarshal, CreatorId)
        {
            this.SaveIO.In.SeekTo(0x01);
            this.IsUnlocked = !Convert.ToBoolean(this.SaveIO.In.ReadByte() & 1);
        }
        public void UnlockLockCarSetup(bool Unlocked)
        {
            IsUnlocked = Unlocked;
            UnlockLockCarSetup();
        }
        public void UnlockLockCarSetup()
        {
            this.SaveIO.In.SeekTo(0x01);
            byte flag = this.SaveIO.In.ReadByte();
            flag = IsUnlocked ? (byte)(flag & 0xFE) : (byte)(flag | 1);
            this.SaveIO.Out.SeekTo(0x01);
            this.SaveIO.Out.Write(flag);
        }
    }
    public class Forz4CarDesign : Forza4Livery
    {
        public bool IsUnlocked;

        public Forz4CarDesign(EndianIO IO, Forza4KeyMarshal KeyMarshal, byte[] CreatorId)
            : base(IO, ForzaFileTypes.Liveries, KeyMarshal, CreatorId)
        {
            if (this.SaveIO.In.ReadInt32() != 0x63726C76)
            {
                throw new ForzaException("invalid livery magic.");
            }
            this.SaveIO.In.SeekTo(0x08);
            IsUnlocked = !Convert.ToBoolean(this.SaveIO.In.ReadInt32() & 1);
        }
        public void UnlockLockCarDesign(bool Unlocked)
        {
            IsUnlocked = Unlocked;
            UnlockLockCarDesign();
        }
        public void UnlockLockCarDesign()
        {
            this.SaveIO.In.SeekTo(0x08);
            int flag = this.SaveIO.In.ReadInt32();
            flag = IsUnlocked ? (byte)(flag & 0xFFFFFFFE) : (byte)(flag | 1);
            this.SaveIO.Out.SeekTo(0xb);
            this.SaveIO.Out.WriteByte(flag & 0xFF);
        }
    }
    public class Forza4LiveryGroup : Forza4Livery
    {
        public bool IsUnlocked;

        public Forza4LiveryGroup(EndianIO IO, Forza4KeyMarshal KeyMarshal, byte[] CreatorId)
            : base(IO, ForzaFileTypes.Vinyls, KeyMarshal, CreatorId)
        {
            if (this.SaveIO.In.ReadInt32() != 0x6C767967)
            {
                throw new ForzaException("invalid layer group magic.");
            }
            this.SaveIO.In.SeekTo(0x1C);
            IsUnlocked = !Convert.ToBoolean(this.SaveIO.In.ReadByte() & 1);
        }
        public byte[] UnlockLockLivery(bool Unlock)
        {
            IsUnlocked = Unlock;
            return UnlockLockLivery(); 
        }
        public byte[] UnlockLockLivery()
        {
            this.SaveIO.In.SeekTo(0x1C);
            byte AuthorCount = this.SaveIO.In.ReadByte();
            var memoryStream = new MemoryStream();
            if (IsUnlocked)
            {
                var livWriter = new EndianWriter(memoryStream, EndianType.BigEndian);
                this.SaveIO.In.SeekTo(0x00);
                livWriter.Write(this.SaveIO.In.ReadBytes(0x1C));
                livWriter.WriteByte(0x00);
                this.SaveIO.In.BaseStream.Position += (1 + (AuthorCount * 8));
                livWriter.Write(this.SaveIO.In.ReadBytes(this.FileSize - this.SaveIO.In.BaseStream.Position));
                this.FileSize = (int)memoryStream.Length;
                livWriter.Close();
            }
            else
            {
                if (AuthorCount == 0)
                {
                    var livWriter = new EndianWriter(memoryStream, EndianType.BigEndian);
                    this.SaveIO.In.SeekTo(0x00);
                    livWriter.Write(this.SaveIO.In.ReadBytes(0x1c));
                    livWriter.WriteByte(0x01);
                    livWriter.Write(new byte[8]);
                    this.SaveIO.In.BaseStream.Position += 1;
                    livWriter.Write(this.SaveIO.In.ReadBytes(this.FileSize - this.SaveIO.In.BaseStream.Position));
                    this.FileSize = (int)memoryStream.Length;
                    livWriter.Close();
                }
                else
                {
                    return this.SaveIO.ToArray();
                }
            }
            return memoryStream.ToArray();
        }
    }
    public class Forza4LiveryCatalog : Forza4Livery
    {
        public bool IsUnlocked;

        public Forza4LiveryCatalog(EndianIO IO, Forza4KeyMarshal KeyMarshal, byte[] CreatorId)
            : base(IO, ForzaFileTypes.Liveries, KeyMarshal, CreatorId)
        {
            if (this.SaveIO.In.ReadInt32() != 0x63726C76)
            {
                throw new ForzaException("invalid livery magic.");
            }

            this.SaveIO.In.SeekTo(0xB);
            IsUnlocked = !Convert.ToBoolean(this.SaveIO.In.ReadByte() & 1);
        }
        public void UnlockLockLivery(bool Unlocked)
        {
            IsUnlocked = Unlocked;
            UnlockLockLivery();
        }
        public void UnlockLockLivery()
        {
            this.SaveIO.In.SeekTo(0xB);
            byte flag = this.SaveIO.In.ReadByte();
            flag = IsUnlocked ? (byte)(flag & 0xFE) : (byte)(flag | 1);
            this.SaveIO.Out.SeekTo(0xb);
            this.SaveIO.Out.Write(flag);
        }
    }

    public class Forza4Livery
    {
        protected EndianIO IO, SaveIO;
        private ForzaFileTypes LiveryType;
        private Forza4KeyMarshal Marshal;
        private GlobalForzaSecurity Forza4Security;
        public int FileSize;
        private byte[] AesKey, ShaKey;

        public Forza4Livery(EndianIO IO, ForzaFileTypes FileType, Forza4KeyMarshal KeyMarshal, byte[] CreatorId)
        {
            if (!IO.Opened)
                IO.Open();

            this.IO = IO;

            this.LiveryType = FileType;

            this.Marshal = KeyMarshal;

            this.CreateSessionKeys(CreatorId);

            this.Forza4Security = new GlobalForzaSecurity(ForzaVersion.Forza4, this.AesKey, this.ShaKey);

            this.SaveIO = Forza4Security.DecryptData(this.IO.ToArray());

            FileSize = Forza4Security.GetFilesize();
        }
        private void CreateSessionKeys(byte[] Creator)
        {
            switch (LiveryType)
            {
                case ForzaFileTypes.CarSetups:
                    this.AesKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaAesKeyMarshal(LiveryType), Creator, 2);
                    this.ShaKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaSHAKeyMarshal(LiveryType), Creator, 5);
                    break;
                case ForzaFileTypes.Liveries: // also designs
                    this.AesKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaAesKeyMarshal(LiveryType), Creator, 5);
                    this.ShaKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaSHAKeyMarshal(LiveryType), Creator, 1);
                    break;
                case ForzaFileTypes.Vinyls:
                    this.AesKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaAesKeyMarshal(LiveryType), Creator, 2);
                    this.ShaKey = GlobalForzaSecurity.TransformSessionKey(Marshal.ForzaSHAKeyMarshal(LiveryType), Creator, 7);
                    break;
                default:
                    throw new ForzaException("attempted to load an unknown Forza livery type.");
            }
        }
        public void Save()
        {
            // Encrypt save data
            SaveIO.Stream.Flush();

            this.IO.Out.SeekTo(0);
            this.IO.Out.Write(Forza4Security.EncryptData(Extract()));

            this.Close();
        }
        public void Close()
        {
            if (IO.Opened)
                IO.Close();
            if (SaveIO.Opened)
                SaveIO.Close();
        }
        public byte[] Extract()
        {
            this.SaveIO.In.SeekTo(0x00);
            return this.SaveIO.In.ReadBytes(this.Forza4Security.GetFilesize());
        }
        public byte[] Inject(byte[] LiveryData)
        {
            this.Close();
            return Forza4Security.EncryptData(LiveryData);
        }
    }
    public class Forza4KeyMarshal
    {
        private Dictionary<string, int> KeyTable;
        private int KeyIterator;
        private List<byte[]> KeyStack;


        public Forza4KeyMarshal() 
        {
            KeyTable = new Dictionary<string, int>();
            KeyStack = new List<byte[]>();
        }
        public Forza4KeyMarshal(string FileTypeName, byte[] AesKey, byte[] ShaKey)
        {
            KeyTable = new Dictionary<string, int>();
            KeyStack = new List<byte[]>();

            AddKey(FileTypeName + "AES", AesKey);
            AddKey(FileTypeName + "SHA", ShaKey);
        }
        public void AddKey(string KeyIdent, byte[] Key)
        {
            KeyTable.Add(KeyIdent, KeyIterator++);

            KeyStack.Add(Key);
        }
        public byte[] ForzaAesKeyMarshal(ForzaFileTypes Type)
        {
            switch (Type)
            {
                case ForzaFileTypes.Screenshots:
                    return KeyStack[KeyTable["ScreenshotAES"]];
                case ForzaFileTypes.CarSetups:
                    return KeyStack[KeyTable["CarSetupAES"]];
                case ForzaFileTypes.Liveries:
                    return KeyStack[KeyTable["LiveryCatalogAES"]];
                case ForzaFileTypes.Vinyls:
                    return KeyStack[KeyTable["LiveryGroupAES"]];
                case ForzaFileTypes.ForzaProfile:
                    return KeyStack[KeyTable["ProfileAES"]];
                case ForzaFileTypes.PlayerDatabase:
                    return KeyStack[KeyTable["DatabaseAES"]];
                default:
                    throw new Exception("ForzaSecurity: Attempted to retrieve a key from an invalid index.");
            }
        }
        public byte[] ForzaSHAKeyMarshal(ForzaFileTypes Type)
        {
            switch (Type)
            {
                case ForzaFileTypes.Screenshots:
                    return KeyStack[KeyTable["ScreenshotSHA"]];
                case ForzaFileTypes.CarSetups:
                    return KeyStack[KeyTable["CarSetupSHA"]];
                case ForzaFileTypes.Liveries:
                    return KeyStack[KeyTable["LiveryCatalogSHA"]];
                case ForzaFileTypes.Vinyls:
                    return KeyStack[KeyTable["LiveryGroupSHA"]];
                case ForzaFileTypes.ForzaProfile:
                    return KeyStack[KeyTable["ProfileSHA"]];
                case ForzaFileTypes.PlayerDatabase:
                    return KeyStack[KeyTable["DatabaseSHA"]];
                default:
                    throw new Exception("ForzaSecurity: Attempted to retrieve a key from an invalid index.");
            }
        }
        public static byte[] HashData(byte[] ShaKey, byte[] Data, byte[] EncryptionHeader)
        {
            SHA256Managed FinalHasher = new SHA256Managed(), TempHasher = new SHA256Managed();

            int len = ShaKey.Length;
            byte[] tempbuffer = new byte[len * 2];
            for (var x = 0; x < len; x++)
            {
                tempbuffer[x] = (byte)(ShaKey[x] ^ 0x5C);
                tempbuffer[x + 0x20] = 0x5C;
            }
            FinalHasher.TransformBlock(tempbuffer, 0, 0x40, null, 0);

            for (var x = 0; x < len; x++)
            {
                tempbuffer[x] = (byte)(ShaKey[x] ^ 0x36);
                tempbuffer[x + 0x20] = 0x36;
            }
            TempHasher.TransformBlock(tempbuffer, 0, 0x40, null, 0);
            TempHasher.TransformBlock(EncryptionHeader, 0, 0x14, null, 0);
            TempHasher.TransformFinalBlock(Data, 0, Data.Length);

            FinalHasher.TransformFinalBlock(TempHasher.Hash, 0, 0x20);

            return FinalHasher.Hash;
        }
    }
}