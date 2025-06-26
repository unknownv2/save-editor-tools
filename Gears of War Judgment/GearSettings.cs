using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Horizon.PackageEditors.Gears_of_War_Judgment
{
    internal enum SettingsDataType
    {
        Empty,
        Int32,
        Int64,
        Double,
        String,
        Float,
        Blob,
        DateTime
    }

    internal abstract class GearSettings
    {
        internal readonly ulong OnlineXUID;

        protected class Map
        {
            internal int ID;
            internal OnlineProfilePropertyOwner Owner;
            internal OnlineDataAdvertisementType AdvertisementType;
            internal SettingsDataType DataType;
            internal object Value;
        }

        protected enum OnlineProfilePropertyOwner : byte
        {
            None,
            OnlineService,
            Game
        }

        protected enum OnlineDataAdvertisementType : byte
        {
            DontAdvertise,
            OnlineService,
            QoS,
            OnlineServiceAndQoS
        }

        protected readonly List<Map> Settings;

        private static byte[] CalculateHash(ulong xuid, byte[] compressedData, int compressedLength, int decompressedLength)
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.Out.Write(xuid);
            io.Out.Write(decompressedLength);
            io.Out.Write(compressedData, compressedLength);
            io.Close();

            return io.ToArray().Hash(HashType.SHA1);
        }

        private AesManaged CreateAesManaged()
        {
            var a = new AesManaged();
            a.Mode = CipherMode.ECB;
            a.BlockSize = 128;
            a.KeySize = 256;
            a.Key = _encryptionKey;
            a.Padding = PaddingMode.Zeros;
            return a;
        }

        private readonly byte[] _encryptionKey;
        private readonly Dictionary<string, int> _stringMap;

        protected GearSettings(byte[] encryptionKey, byte[] data, ulong onlineXuid, bool secured, Dictionary<string, int> stringMap)
        {
            _encryptionKey = encryptionKey;

            if (secured)
            {
                var compressedLength = data.ReadInt32(data.Length - 4) - 24;

                var dio = new EndianIO(new CryptoStream(new MemoryStream(data), CreateAesManaged().CreateDecryptor(), CryptoStreamMode.Read), EndianType.BigEndian, true);
                var hash = dio.In.ReadBytes(20);
                var decompressedLength = dio.In.ReadInt32();
                var compData = dio.In.ReadBytes(compressedLength);
                dio.Close();

                if (!hash.SequenceEqual(CalculateHash(onlineXuid, compData, compressedLength, decompressedLength)))
                    throw new Exception("GoWJ: Online storage corrupted.");

                var ms = new MemoryStream();
                LZO.LZO1X.Decompress(compData, ms);
                data = ms.ToArray();
            }


            _stringMap = stringMap;

            OnlineXUID = onlineXuid;

            var io = new EndianIO(data, EndianType.BigEndian, true);

            var numSettings = io.In.ReadInt32();

            Settings = new List<Map>(numSettings);

            while (numSettings-- != 0)
            {
                var m = new Map();
                m.Owner = (OnlineProfilePropertyOwner)io.In.ReadByte();
                m.ID = io.In.ReadInt32();
                m.DataType = (SettingsDataType)io.In.ReadByte();

                switch (m.DataType)
                {
                    case SettingsDataType.Empty:
                        m.Value = null;
                        break;
                    case SettingsDataType.Int32:
                        m.Value = io.In.ReadInt32();
                        break;
                    case SettingsDataType.Int64:
                        m.Value = io.In.ReadInt64();
                        break;
                    case SettingsDataType.Double:
                        m.Value = io.In.ReadDouble();
                        break;
                    case SettingsDataType.String:
                        var len = io.In.ReadInt32();
                        m.Value = len == 0 ? "" : io.In.ReadString(len + 1);
                        break;
                    case SettingsDataType.Float:
                        m.Value = io.In.ReadSingle();
                        break;
                    case SettingsDataType.Blob:
                        m.Value = io.In.ReadBytes(io.In.ReadInt32());
                        break;
                    case SettingsDataType.DateTime:
                        m.Value = DateTime.MinValue; // not sure yet
                        break;
                    default:
                        throw new Exception(string.Format("GearProfileSettings: Unknown data type [0x{0:X2}]", (byte)m.DataType));
                }

                m.AdvertisementType = (OnlineDataAdvertisementType)io.In.ReadByte();

                Settings.Add(m);
            }

            io.Close();
        }

        protected abstract void IncrementSync();

        internal byte[] ToArray(bool secure)
        {
            IncrementSync();

            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            io.Out.Write(Settings.Count);

            foreach (var s in Settings)
            {
                io.Out.Write((byte)s.Owner);
                io.Out.Write(s.ID);
                io.Out.Write((byte)s.DataType);

                switch (s.DataType)
                {
                    case SettingsDataType.Empty:
                        break;
                    case SettingsDataType.Int32:
                        io.Out.Write((int)s.Value);
                        break;
                    case SettingsDataType.Int64:
                        io.Out.Write((long)s.Value);
                        break;
                    case SettingsDataType.Float:
                        io.Out.Write((float)s.Value);
                        break;
                    case SettingsDataType.Double:
                        io.Out.Write((double)s.Value);
                        break;
                    case SettingsDataType.String:
                        var str = (string)s.Value;
                        if (string.IsNullOrEmpty(str))
                            io.Out.Write(0);
                        else
                        {
                            var len = str.Length + 1;
                            io.Out.Write(len);
                            io.Out.WriteAsciiString(str, len);
                        }
                        break;
                    case SettingsDataType.Blob:
                        var arr = (byte[])s.Value;
                        io.Out.Write(arr.Length);
                        io.Out.Write(arr);
                        break;
                    case SettingsDataType.DateTime:
                        //io.Out.Write((DateTime)s.Value); ??
                        break;
                }

                io.Out.Write((byte)s.AdvertisementType);
            }

            io.Close();

            var buffer = io.ToArray();

            if (!secure)
                return buffer;

            var decompressedLength = buffer.Length;

            var ms = new MemoryStream();
            LZO.LZO1X.Compress(buffer, (uint)buffer.Length, ms);
            ms.Close();

            buffer = ms.ToArray();

            var fileLength = buffer.Length + 24;

            ms = new MemoryStream();
            io = new EndianIO(new CryptoStream(ms, CreateAesManaged().CreateEncryptor(), CryptoStreamMode.Write), EndianType.BigEndian, true);
            io.Out.Write(CalculateHash(OnlineXUID, buffer, buffer.Length, decompressedLength));
            io.Out.Write(decompressedLength);
            io.Out.Write(buffer);
            io.Close();

            buffer = ms.ToArray();

            io = new EndianIO(new MemoryStream(buffer.Length + 4), EndianType.BigEndian, true);
            io.Out.Write(buffer);
            io.Out.Write(fileLength);
            io.Close();

            return io.ToArray();
        }

        internal void Export(string filePath)
        {
            var sb = new StringBuilder();

            Settings.Sort((a, b) => a.ID.CompareTo(b.ID));

            foreach (var s in Settings)
            {
                object value;
                switch (s.DataType)
                {
                    case SettingsDataType.Empty:
                        value = "[empty]";
                        break;
                    case SettingsDataType.DateTime:
                        throw new Exception();
                    case SettingsDataType.Blob:
                        value = ((byte[])s.Value).ToHexString();
                        break;
                    default:
                        value = s.Value;
                        break;
                }
                sb.AppendLine(string.Format("{0:D4}\t{1}\t{2}", s.ID, ReverseLookup(s.ID), value));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private string ReverseLookup(int id)
        {
            foreach (var x in _stringMap.Where(x => x.Value == id))
                return x.Key;
            return "";
        }

        internal bool ContainsKey(string key)
        {
            return this._stringMap.ContainsKey(key);
        }

        private Map GetMapByID(int id)
        {
            return Settings.FirstOrDefault(s => s.ID == id);
        }

        internal int this[string key]
        {
            get
            {
                var map = GetMapByID(_stringMap[key]);

                if (map == null)
                    return 0;

                if (map.DataType != SettingsDataType.Int32)
                    throw new Exception();

                return (int)map.Value;
            }
            set
            {
                var map = GetMapByID(_stringMap[key]);

                if (map == null)
                    Set(key, value);
                else
                {
                    if (map.DataType != SettingsDataType.Int32)
                        throw new Exception();

                    map.Value = value;
                }
            }
        }

        internal void Set(string key, object value)
        {
            var id = _stringMap[key];

            var map = GetMapByID(id);

            if (map == null)
            {
                map = new Map();
                map.Owner = OnlineProfilePropertyOwner.Game;
                map.ID = id;

                if (value == null)
                    map.DataType = SettingsDataType.Empty;
                else if (value is int || value is Enum)
                    map.DataType = SettingsDataType.Int32;
                else if (value is long)
                    map.DataType = SettingsDataType.Int64;
                else if (value is float)
                    map.DataType = SettingsDataType.Float;
                else if (value is double)
                    map.DataType = SettingsDataType.Double;
                else if (value is string)
                    map.DataType = SettingsDataType.String;
                else if (value is byte[])
                    map.DataType = SettingsDataType.Blob;
                else if (value is DateTime)
                    map.DataType = SettingsDataType.DateTime;
                else
                    throw new Exception(string.Format("GearProfileSettings: Unknown data type ({0})", map.Value.GetType()));

                map.AdvertisementType = OnlineDataAdvertisementType.DontAdvertise;

                Settings.Add(map);
            }

            map.Value = value;
        }

        protected object ObjectValue(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            return map == null ? null : map.Value;
        }

        protected int Int32Value(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            if (map == null)
                return 0;

            return (int)map.Value;
        }

        protected long Int64Value(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            if (map == null)
                return 0;

            return (long)map.Value;
        }

        protected double DoubleValue(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            if (map == null)
                return 0;

            return (double)map.Value;
        }

        protected string StringValue(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            if (map == null)
                return "";

            return (string)map.Value;
        }

        internal float FloatValue(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            if (map == null)
                return 0;

            return (float)map.Value;
        }

        internal byte[] BlobValue(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            if (map == null)
                return null;

            return (byte[])map.Value;
        }

        protected DateTime DateTimeValue(string key)
        {
            var map = GetMapByID(_stringMap[key]);

            if (map == null)
                return DateTime.MinValue;

            return (DateTime)map.Value;
        }

        protected bool BitIsSet(int integerValue, int bitIndex)
        {
            return (integerValue & (1 << bitIndex)) != 0;
        }

        protected int GetNumBitsSet(string key)
        {
            var bitCount = 0;
            var val = Int32Value(key);

            while (val != 0)
            {
                val &= val - 1;
                bitCount++;
            }

            return bitCount;
        }
    }
}
