using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

namespace Horizon.Functions
{
    internal static class Global
    {
        internal static string generateCaptchaText()
        {
            char[] captchaChars = new char[]
            {
                'A', 'B', 'C', 'D', 'E', 'F',
                'G', 'H', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S',
                'T', 'U', 'V', 'W', 'X', 'Y',
                'Z', 'a', 'b', 'c', 'd', 'e',
                'f', 'g', 'h', 'i', 'j', 'k',
                'm', 'n', 'o', 'p', 'q', 'r',
                's', 't', 'u', 'v', 'w', 'x',
                'y', 'z', '0', '1', '2', '3',
                '4', '5', '6', '7', '8', '9'
            };
            string newString = String.Empty;
            int len = random.Next(6, 8);
            for (int x = 0; x < len; x++)
            {
                System.Threading.Thread.Sleep((x + 1) * 5);
                newString += captchaChars[random.Next(0, captchaChars.Length - 1)].ToString();
            }
            return newString.ToLower();
        }

        internal static Random random = new Random();
        internal static Image createCaptchaImage(string text)
        {
            Bitmap bitmap = new Bitmap(200, 60, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, 200, 60);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.ZigZag, Color.LightGray, Color.White);
            g.FillRectangle(hatchBrush, rect);
            Font font = new Font("Arial", 38, FontStyle.Bold);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            GraphicsPath path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4f;
            PointF[] points =
			{
				new PointF(random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v)
			};
            Matrix matrix = new Matrix();
            matrix.Translate(0, 0);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0f);
            hatchBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.LightGray, Color.DarkGray);
            g.FillPath(hatchBrush, path);
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30f); i++)
                g.FillEllipse(hatchBrush, random.Next(rect.Width), random.Next(rect.Height), random.Next(m / 50), random.Next(m / 50));
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();
            return bitmap;
        }

        internal static byte[] objectToByteArray(object obj)
        {
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray();
        }

        internal static bool compareArray<T>(T[] arr1, T[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;
            for (int x = 0; x < arr1.Length; x++)
                if (!arr1[x].Equals(arr2[x]))
                    return false;
            return true;
        }

        internal static string base64Decode(string input)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(input));
        }

        internal static byte[] makeHMACMD5(string key, string message)
        {
            return new HMACMD5(Encoding.ASCII.GetBytes(key)).ComputeHash(Encoding.ASCII.GetBytes(message));
        }

        internal static byte[] makeHMACSHA1(string key, string message)
        {
            return new HMACSHA1(Encoding.ASCII.GetBytes(key)).ComputeHash(Encoding.ASCII.GetBytes(message));
        }

        internal static byte[] hexStringToArray(string input)
        {
            byte[] output = new byte[input.Length / 2];
            for (int x = 0, i = 0; x < input.Length; x += 2, i++)
                output[i] = Convert.ToByte(input.Substring(x, 2), 16);
            return output;
        }

        internal static string arrayToString(byte[] input)
        {
            return Encoding.ASCII.GetString(input);
        }

        internal static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        internal static long DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (dateTime - start);
            return (long)span.TotalSeconds;
        }

        internal static string getFormatFromBytes(long bytes)
        {
            string[] orders = new string[] { "GB", "MB", "KB", "B" };
            long max = (long)Math.Pow(1024, orders.Length - 1);
            for (int x = 0; x < orders.Length; x++)
            {
                if (bytes >= max)
                    return string.Format(((x == orders.Length - 1) ? "{0:0}" : "{0:0.00}") + " {1}", (decimal)bytes / max, orders[x]);
                max /= 1024;
            }
            return "0 B";
        }

        internal static byte[] convertToBigEndian(byte[] input)
        {
            if(BitConverter.IsLittleEndian)
                Array.Reverse(input);

            return input;
        }

        internal static int ROTL32(int value, int bits)
        {
            return (int)(((value << bits) | (value >> (32 - bits))) & 0xFFFFFFFF);
        }

        internal static long ROTL64(long value, int bits)
        {
            return (long)(((value << bits) | (value >> (64 - bits))) & -1);
        }

        internal static void Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }
    }
    public class WinFile
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct LargeInteger
        {
            [FieldOffset(4)]
            public uint HighPart;
            [FieldOffset(0)]
            public uint LowPart;
            [FieldOffset(0)]
            public long QuadPart;
        }
        public enum FileInformationClass
        {
            FileBasicInformation = 0x04,
            FileRenameInformation = 0x0a,
            FileDispositionInformation = 0x0d,
            FilePositionInformation = 0x0e,
            FileAllocationInformation = 0x13,
            FileEndOfFileInformation = 0x14
        }

        public struct FileEndOfFileInfo
        {
            public LargeInteger EndOfFile;
        }

        public struct FileRenameInformation
        {
            public bool ReplaceIfExists;
            public ushort FileNameLength;
            public string FileName;
        }
        public struct FileDispositionInformation
        {
            public bool DeleteFile;
        }
    }
    public class DatagridViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;
        public DatagridViewColumnSorter() : this(SortOrder.None)
        {
        }
        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public DatagridViewColumnSorter(SortOrder Order)
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = Order;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            DataGridViewRow gridViewX, gridViewY;


            gridViewX = (DataGridViewRow)x;
            gridViewY = (DataGridViewRow)y;

            // Compare the two items
            compareResult = ObjectCompare.Compare(gridViewX.Cells[ColumnToSort].Value.ToString(), gridViewY.Cells[ColumnToSort].Value.ToString());

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }

    }
    public abstract class BitHelper
    {
        public static List<bool> LoadValue(object value, int bitcount)
        {
            var flags = new List<bool>();
            var temp = 1;
            var tempv = Convert.ToInt32(value);
            for (var i = 0; i < bitcount; i++)
            {
                try
                {
                    bool b = (temp & tempv) == temp ? true : false;
                    flags.Add(b);
                    temp <<= 1;
                }
                catch
                {
                    break;
                }
            }
            return flags;
        }

        public static int ConvertToWriteableInteger(List<bool> flags)
        {
            int intToWrite = 0;
            for (int z = 0; z < flags.Count; z++)
            {
                intToWrite = setBit(intToWrite, z, flags[z]);
            }
            return intToWrite;
        }
        public static int ConvertToWriteableInteger(bool[] flags)
        {
            int intToWrite = 0;
            for (int z = 0; z < flags.Length; z++)
            {
                intToWrite = setBit(intToWrite, z, flags[z]);
            }
            return intToWrite;
        }
        public static byte ConvertToWriteableByte(bool[] flags)
        {
            int intToWrite = 0;
            for (int z = 0; z < flags.Length; z++)
            {
                intToWrite = setBit(intToWrite, z, flags[z]);
            }
            return Convert.ToByte(intToWrite);
        }
        private static int setBit(int integer, int bitIndex, bool onOff)
        {
            if (onOff)
            {
                integer = (1 << bitIndex) | integer;
            }
            else
            {
                integer = ~(1 << bitIndex) & integer;
            }
            return integer;
        }
        public static bool[] ProduceBitmask(object value)
        {
            uint tempint = 1;
            var temp = new bool[8];
            uint tempvalue = Convert.ToByte(value);
            for (int i = 0; i < 8; i++)
            {
                temp[i] = (tempint & tempvalue) == tempint;
                tempint <<= 1;
            }
            return temp;
        }
    }
}