using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace System
{
    internal enum HashType : byte
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }

    internal static class Extensions
    {
        internal static void Dispose(this IEnumerable<IDisposable> collection)
        {
            foreach (IDisposable item in collection)
                if (item != null)
                    item.Dispose();
        }

        internal static string Hash(this string message, HashType algo)
        {
            return Encoding.ASCII.GetBytes(message).Hash(algo).ToHexString();
        }

        internal static byte[] Hash(this byte[] sourceBytes, HashType algo)
        {
            switch (algo)
            {
                case HashType.MD5:
                    return MD5CryptoServiceProvider.Create().ComputeHash(sourceBytes);
                case HashType.SHA1:
                    return SHA1Managed.Create().ComputeHash(sourceBytes);
                case HashType.SHA256:
                    return SHA256Managed.Create().ComputeHash(sourceBytes);
                case HashType.SHA384:
                    return SHA384Managed.Create().ComputeHash(sourceBytes);
                default:
                    return SHA512Managed.Create().ComputeHash(sourceBytes);
            }
        }

        private static MemoryStream tempStream = new MemoryStream();
        internal static byte[] ToByteArray(this Image image)
        {
            tempStream.SetLength(0);
            image.Save(tempStream, ImageFormat.Png);
            return tempStream.ToArray();
        }
        internal static byte[] ToByteArray(this Image image, ImageFormat Format)
        {
            tempStream.SetLength(0);
            image.Save(tempStream, Format);
            return tempStream.ToArray();
        }
        internal static object ToObject(this byte[] arr)
        {
            return new BinaryFormatter().Deserialize(new MemoryStream(arr));
        }

        internal static string[] Split(this string input, string separator)
        {
            return input.Split(new string[] { separator }, StringSplitOptions.None);
        }

        internal static string ToHexString(this string text, byte length)
        {
            StringBuilder hex = new StringBuilder(text.Length * length);
            foreach (char c in text)
                hex.Append(((uint)c).ToString(String.Format("X{0}", length)));
            return hex.ToString();
        }

        internal static string ToHexString(this string text)
        {
            return text.ToHexString(2);
        }

        internal static string UrlEncode(this string text)
        {
            StringBuilder hex = new StringBuilder(text.Length * 2);
            foreach (char c in text)
                hex.Append("%" + String.Format("{0:x2}", (uint)System.Convert.ToUInt32(((int)c).ToString())));
            return hex.ToString();
        }

        internal static void Save(this Stream IO, string fileName)
        {
            if (IO.CanSeek)
                IO.Position = 0;
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            byte[] buffer = new byte[5242880];
            int numRead;
            while ((numRead = IO.Read(buffer, 0, 5242880)) > 0)
                fs.Write(buffer, 0, numRead);
            fs.Close();
        }

        internal static byte[] ToArray(this Stream IO)
        {
            if (IO.CanSeek)
                IO.Position = 0;
            MemoryStream ms = new MemoryStream();
            byte[] buffer = new byte[5242880];
            int numRead;
            while ((numRead = IO.Read(buffer, 0, 5242880)) > 0)
                ms.Write(buffer, 0, numRead);
            byte[] ret = ms.ToArray();
            ms.Close();
            return ret;
        }

        internal static void Save(this byte[] array, string fileName)
        {
            File.WriteAllBytes(fileName, array);
        }

        internal static string Reverse(this string input)
        {
            char[] str = input.ToCharArray();
            Array.Reverse(str);
            return new string(str);
        }

        internal static string ToHexString(this byte[] input)
        {
            StringBuilder hex = new StringBuilder(input.Length * 2);
            for (int x = 0; x < input.Length; x++)
                hex.Append(input[x].ToString("x2"));
            return hex.ToString();
        }

        internal static string Base64Encode(this string input)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(input));
        }

        internal static string Base64Encode(this string input, bool url)
        {
            if (url)
                return input.Base64Encode().Replace('+', '-').Replace('/', '_').Replace('=', '.');
            return input.Base64Encode();
        }

        internal static long ToLong(this byte[] input, bool bigEndian)
        {
            var buffer = input.ToArray();
            if (!bigEndian)
                Array.Reverse(buffer);
            long output = 0;
            for (int x = new int(); x < buffer.Length; x++)
            {
                output <<= 8;
                output |= buffer[x];
            }
            return output;
        }

        internal static byte[] ReadBytes(this byte[] input, object o_index, object o_length)
        {
            int length = Convert.ToInt32(o_length);
            int index = Convert.ToInt32(o_index);
            byte[] output = new byte[length];
            Array.Copy(input, index, output, 0, length);
            return output;
        }

        internal static int ToInt32(this byte[] input, bool bigEndian)
        {
            return (int)input.ToLong(bigEndian);
        }
        internal static uint ToUInt32(this byte[] input, bool bigEndian)
        {
            return (uint)input.ToLong(bigEndian);
        }
        internal static int ReadInt8(this byte[] data, int offset)
        {
            return (int)data[offset];
        }

        internal static int ReadInt16(this byte[] data, int offset)
        {
            return (data[offset] << 8) | data[offset + 1];
        }

        internal static int ReadInt32(this byte[] data, int offset)
        {
            return (data[offset] << 24) | (data[offset + 1] << 16) | (data[offset + 2] << 8) | data[offset + 3];
        }

        internal static uint ReadUInt32(this byte[] data, int offset)
        {
            return (uint)((data[offset] << 24) | (data[offset + 1] << 16) | (data[offset + 2] << 8) | data[offset + 3]);
        }

        internal static long ReadInt64(this byte[] data, int offset)
        {
            int num = (data[3] | (data[2] << 8) | (data[1] << 16) | (data[0] << 24));
            int num2 = (data[7] | (data[6] << 8) | (data[5] << 16) | (data[4] << 24));

            return ((num2 << 32) | num);
        }

        internal static void WriteInt8(this byte[] data, int offset, int value)
        {
            data[offset] = (byte)(value & 0xFF);
        }

        internal static void WriteInt16(this byte[] data, int offset, int value)
        {
            data[offset] = (byte)((value & 0x0000FF00) >> 8);
            data[offset + 1] = (byte)(value & 0x000000FF);
        }

        internal static void WriteInt32(this byte[] data, int offset, int value)
        {
            data[offset] = (byte)((value & 0xFF000000) >> 24);
            data[offset + 1] = (byte)((value & 0x00FF0000) >> 16);
            data[offset + 2] = (byte)((value & 0x0000FF00) >> 8);
            data[offset + 3] = (byte)(value & 0x000000FF);
        }

        public static uint RotateLeft(this uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }

        public static uint RotateRight(this uint value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }
    }
}
