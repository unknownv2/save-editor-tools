using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Horizon.Functions;

namespace Horizon
{
    internal static class FormSettings
    {
        private static Dictionary<string, Dictionary<byte, byte[]>> Forms = new Dictionary<string, Dictionary<byte, byte[]>>();
        internal static void addForm(string id, string hash)
        {
            Forms.Add(id, new Dictionary<byte, byte[]>());
            addSetting(id, 0, hash);
        }

        internal static void removeForm(string id)
        {
            Forms.Remove(id);
        }

        internal static void clearDictionary()
        {
            Forms.Clear();
        }

        internal static bool formExists(string id)
        {
            return Forms.ContainsKey(id);
        }

        private static byte[] decEnc(byte[] arr)
        {
            byte formXor = (byte)((char)Server.Config.getSetting("form_xor"));
            byte[] ar2 = new byte[arr.Length];
            Array.Copy(arr, ar2, arr.Length);
            for (long x = 0; x < ar2.Length; x++)
                ar2[x] ^= (byte)(formXor ^ Server.Config.clientIV[x % Server.Config.clientIV.Length]);
            return ar2;
        }

        internal static void addSetting(string id, byte key, object value)
        {
            Forms[id].Add(key, decEnc(Global.objectToByteArray(value)));
        }

        internal static object getSetting(string id, byte key)
        {
            return decEnc(Forms[id][key]).ToObject();
        }

        internal static bool validHash(string id, string hash)
        {
            return formExists(id) ? (string)getSetting(id, 0) == hash : false;
        }

        internal static void parseSetting(string id, ref System.Xml.XPath.XPathNavigator nav)
        {
            string value = nav.Value;
            nav.MoveToFirstAttribute();
            byte key = byte.Parse(nav.Value);
            nav.MoveToNextAttribute();
            switch (nav.Value)
            {
                case "s":
                    addSetting(id, key, value);
                    break;
                case "i":
                    addSetting(id, key, int.Parse(value));
                    break;
                case "a":
                    addSetting(id, key, Global.hexStringToArray(value));
                    break;
                case "l":
                    addSetting(id, key, long.Parse(value));
                    break;
                case "b":
                    addSetting(id, key, bool.Parse(value));
                    break;
                case "u":
                    string[] vals_s = value.Split(',');
                    uint[] vals = new uint[vals_s.Length];
                    for (int x = 0; x < vals_s.Length; x++)
                        vals[x] = uint.Parse(vals_s[x]);
                    addSetting(id, key, vals);
                    break;
            }
            value = "";
        }
    }
}
