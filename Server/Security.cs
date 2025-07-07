using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using Horizon.Functions;
using System.IO;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;

namespace Horizon.Server
{
    internal static class Security
    {
        internal static void executeChallenge(string[] refs, string challengeCode)
        {
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.AddRange(refs);
            compParams.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            compParams.GenerateInMemory = true;
            compParams.IncludeDebugInformation
                = compParams.GenerateExecutable
                = false;
            CompilerResults cr = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(compParams, challengeCode);
            if (cr.Errors.Count == 0)
            {
                try
                {
                    cr.CompiledAssembly.GetModules()[0].GetTypes()[0].GetMethods()[0].Invoke(null, null);
                    return;
                }
                catch
                {

                }
            }
            Process.GetCurrentProcess().Kill();
        }

        // Creates the AES crypto service provider used to encrypt and decrypt data.
        private static AesCryptoServiceProvider getAESCrypto(byte[] key, byte[] iv)
        {
            return new AesCryptoServiceProvider()
                {
                    Mode = CipherMode.CBC,
                    Key = key,
                    IV = iv,
                    Padding = PaddingMode.None
                };
        }

        // Encrypt a string using AES. Returns a string.
        internal static byte[] encryptAES(string input, byte[] key, byte[] iv)
        {
            AesCryptoServiceProvider aes = getAESCrypto(key, iv);
            aes.Padding = PaddingMode.Zeros;
            return aes.CreateEncryptor(aes.Key, aes.IV).TransformFinalBlock(Encoding.ASCII.GetBytes(input), 0, input.Length);
        }

        // Decrypt an AES encrypted string. Returns a string.
        internal static string decryptAES(string input, byte[] key, byte[] iv)
        {
            byte[] decMe = fixBlock(Encoding.ASCII.GetBytes(input));
            return Global.arrayToString(getAESCrypto(key, iv).CreateDecryptor(
                key, iv).TransformFinalBlock(decMe, 0, decMe.Length)).Replace("\0", String.Empty);
        }

        // Decrypt an AES encrypted byte array. Returns a byte[].
        internal static byte[] decryptAES(byte[] input, byte[] key, byte[] iv)
        {
            byte[] decMe = fixBlock(input);
            return getAESCrypto(key, iv).CreateDecryptor().TransformFinalBlock(decMe, 0, decMe.Length);
        }

        // Decrypt a base64 encoded string into a byte array.
        internal static byte[] safeDecryptToArray(string input)
        {
            return decryptAES(Convert.FromBase64String(input), Config.clientAES, Config.clientIV);
        }

        // Decrypt a base64 encoded string into a string.
        internal static string safeDecryptToString(string input)
        {
            return Encoding.ASCII.GetString(decryptAES(Convert.FromBase64String(input), Config.clientAES, Config.clientIV));
        }

        // Pad a byte array that's about to be decrypted. Blocks must be a multiple of 16.
        private static byte[] fixBlock(byte[] input)
        {
            long mod = input.Length % 16;
            byte[] output = new byte[input.Length + (mod == 0 ? 0 : 16 - mod)];
            for (int x = new int(); x < input.Length; x++)
                output[x] = input[x];
            return output;
        }

        internal static byte[] getFormAESKey(string hash)
        {
            return Encoding.ASCII.GetBytes(
                Global.makeHMACMD5(
                    (hash + (string)Config.getSetting("session_id")).Hash(HashType.SHA256),
                        Config.clientSalt).ToHexString());
        }

        internal static string decryptFormXML(string id, string hash, string data)
        {
            return Encoding.ASCII.GetString(Security.decryptAES(Convert.FromBase64String(data), getFormAESKey(hash), Config.clientIV)).Replace("\0", String.Empty);
        }

        internal static bool validFormHash(string id, string xml, string hash)
        {
            return hash == Global.makeHMACSHA1(
                ((string)Config.getSetting("session_id") + getMachineIdent()).Hash(HashType.SHA256), id + xml).ToHexString();
        }

        // Creates the Initialization Vector used to encrypt and decrypt data sent to and from the server.
        internal static byte[] makeIV()
        {
            HashType[] types = new HashType[] { 
                HashType.SHA384,
                HashType.SHA1,
                HashType.SHA256,
                HashType.MD5
            };
            string hashMe = Security.getMachineIdent() + Config.clientSalt;
            string output = String.Empty;
            for (byte x = new byte(); x < types.Length; x++)
            {
                string newHash = hashMe.Hash(types[x]);
                output += newHash.Substring(newHash.Length - 9, 4).Reverse();
            }
            return Encoding.ASCII.GetBytes(output.Reverse());
        }

        // Create unique machine identifier from motherboard serial number.
        private static readonly string firstMachine = getMachineIdent();
        internal static string getMachineIdent()
        {
            string[][] tables = new string[][] {
                new string[] { "SerialNumber", "Win32_BIOS" },
                new string[] { "Product, SerialNumber", "Win32_BaseBoard" },
                new string[] { "DeviceID, UniqueId", "Win32_Processor" },
                new string[] { "DeviceID", "Win32_MotherboardDevice" }
            };
            string machineIdent = "H360";
            foreach (string[] table in tables)
                foreach (ManagementObject obj in new ManagementObjectSearcher(String.Format("SELECT {0} FROM {1}", table)).Get())
                    foreach (PropertyData data in obj.Properties)
                        if (data.Value != null)
                            machineIdent += data.Value.ToString().Base64Encode(true).Reverse().Replace(".", String.Empty);
            string currentMachine = (machineIdent + Environment.ProcessorCount.ToString()).Hash(HashType.SHA1);
            if (firstMachine == null)
                return currentMachine;
            if (firstMachine == currentMachine)
                return currentMachine;
            return String.Empty;
        }

        internal static bool isDebugging()
        {
            #if INT2
                return !Debugger.IsAttached;
            #else
                try { return Debugger.IsAttached; }
                catch (Exception) { return true; }
            #endif
        }

        private static string[] badProcesses = new string[] { "fiddler", "ollydbg", "wireshark", "codeview", "colasoft packet builder" };
        internal static bool isRunningBadProcess()
        {
            foreach (Process theProcess in Process.GetProcesses())
            {
                string titleWindow = theProcess.MainWindowTitle.ToLower(),
                    procName = theProcess.ProcessName.ToLower();
                foreach (string badProcess in badProcesses)
                    if (titleWindow.Contains(badProcess) || procName.Contains(badProcess))
                        return true;
            }
            return false;
        }

        // Return the client's external IP address.
        internal static string getIPAddress()
        {
            return new WebClient().DownloadString(Config.serverURL + "ip.php");
        }
    }
}
