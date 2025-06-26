using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Dirt
{
    // Helper functions for the DiRT save files
    public class DirtSave
    {
        public static uint Crc_Seed = 0x4A090E98;

        public static int CalculateStringHash(string stringValue)
        {
            if (stringValue != string.Empty && stringValue.Length > 0)
            {
                int hash = 0x1505, sum = 0, len = stringValue.Length;
                string upper_str = stringValue.ToUpper();
                for (int j = 0; j < len; j++)
                {
                    byte ip = (byte)upper_str[j];
                    if (ip >= 0x41 && ip <= 0x5A)
                        ip += 0x20;

                    sum = hash << 5;
                    sum += hash;
                    hash = ip + sum;
                }
                return hash;
            }
            return -1;
        }

        public static string ObfuscateString(string originalString)
        {
            string obfString = string.Empty;

            if (originalString.Length > 0x00)
            {
                uint obf_a = 0xBA2E8BA3;
                int obf_b = 0x4EC4EC4F, len = originalString.Length;
                string upper_str = originalString.ToUpper();
                string obf_key = "YGPNELSQZIK";
                long pf_a, pf_b, prod;
                byte[] string_out = new byte[len];
                for (int x = 0; x < len; x++)
                {
                    byte ip = (byte)upper_str[x];
                    if (ip >= 0x41 && ip <= 0x5A)
                    {
                        pf_a = x & 0xFFFFFFFF;
                        pf_b = obf_a & 0xFFFFFFFF;
                        prod = pf_a * pf_b;
                        long c = (prod >> 32) & 0xFFFFFFFF;
                        long d = c >> 3;
                        long e = d * 0xB;
                        int f = x - (int)(e & 0xFFFFFFFF);
                        c = obf_key[f] & 0xFF;
                        c += ip;
                        c -= 0x82;
                        pf_a = (c & 0xFFFFFFFF);
                        pf_b = (obf_b & 0xFFFFFFFF);
                        prod = pf_a * pf_b;
                        long g = (prod >> 32) & 0xFFFFFFFF;
                        g = g >> 3;
                        d = g >> 31;
                        d += g;
                        e = d * 0x1A;
                        g = c - e;
                        long lf = g + 0x41;
                        string_out[x] = (byte)(lf);
                    }
                    else
                    {
                        string_out[x] = ip;
                    }
                }
                obfString = Horizon.Functions.Global.arrayToString(string_out);
            }

            return obfString;
        }
    }

    // Seen so far in EGO 2.0 (updated engine starting with Dirt 3) save files
    public class DirtSecuritySave
    {
        public struct FileEntry
        {
            public string FileName;
            public int FilenameHash;
            public string ObfFilename;
        }

        public class SecurityInfoFile
        {
            public struct SecEntry
            {
                public int HashFileName;
                public int FileSize;
                public uint Checksum;
            }

            public uint Checksum;
            public int FileCount;
            public List<SecEntry> Entries;
            private List<FileEntry> HashFileList;

            private EndianIO IO;

            public SecurityInfoFile(EndianIO IO, List<FileEntry> fileListing)
            {
                if (fileListing != null)
                    HashFileList = fileListing;

                if (IO != null)
                {
                    if (!IO.Opened)
                        IO.Open();

                    this.IO = IO;
                }
                Checksum = IO.In.ReadUInt32();

                // Verify the security data
                var saveData = IO.In.ReadBytes(IO.Stream.Length - 0x04);
                if (ElectronicArts.EACRC32.Calculate_Alt2(saveData, saveData.Length, Dirt.DirtSave.Crc_Seed) != Checksum)
                    throw new Dirt.DirtException("invalid save data detected.");

                IO.In.SeekTo(0x04);
                FileCount = IO.In.ReadInt32();
                Entries = new List<SecEntry>();

                for (var x = 0; x < FileCount; x++)
                {
                    Entries.Add(new SecEntry
                    {

                        HashFileName = IO.In.ReadInt32(),
                        FileSize = IO.In.ReadInt32(),
                        Checksum = IO.In.ReadUInt32()
                    });
                }
            }

            public SecEntry GetFileEntry(string fileName)
            {
                return Entries.Find(entry => entry.HashFileName == (HashFileList.Find(file => file.FileName == fileName).FilenameHash));
            }

            public void UpdateSecurityEntry(SecEntry newEntry)
            {
                this.Entries[this.Entries.FindIndex(entry => entry.HashFileName == newEntry.HashFileName)] = newEntry;
            }

            public void Save()
            {
                IO.Out.SeekTo(0x08);
                for (var x = 0; x < FileCount; x++)
                {
                    IO.Out.BaseStream.Position += 0x04; // We never edit the filename hash
                    IO.Out.Write(Entries[x].FileSize);
                    IO.Out.Write(Entries[x].Checksum);
                }

                // Fix file checksum
                IO.In.SeekTo(0x04);
                var saveData = IO.In.ReadBytes(IO.Stream.Length - 0x04);
                var sum = ElectronicArts.EACRC32.Calculate_Alt2(saveData, saveData.Length, Dirt.DirtSave.Crc_Seed);
                IO.Out.SeekTo(0x00);
                IO.Out.Write(sum);
            }
        }

        protected EndianIO IO;

        public SecurityInfoFile.SecEntry FileInfo;

        public DirtSecuritySave(EndianIO IO, SecurityInfoFile.SecEntry securityInfo)
        {
            if (IO != null)
            {
                if (!IO.Opened)
                    IO.Open();

                this.IO = IO;
            }

            FileInfo = securityInfo;

            VerifySaveData();
        }

        private void VerifySaveData()
        {
            if (CalculateChecksum() != FileInfo.Checksum)
                throw new DirtException("invalid save file detected.");
        }

        public virtual void Flush()
        {

        }

        public void Save()
        {
            this.Flush();

            FileInfo.Checksum = CalculateChecksum();
        }

        private uint CalculateChecksum()
        {
            IO.In.SeekTo(0x00);
            var saveData = IO.In.ReadBytes(FileInfo.FileSize);
            return ElectronicArts.EACRC32.Calculate_Alt2(saveData, saveData.Length, Dirt.DirtSave.Crc_Seed);
        }
    }

    public class DirtSecurityHelper
    {
        private readonly List<DirtSecuritySave.FileEntry> HashFileList = new List<DirtSecuritySave.FileEntry>();

        public DirtSecurityHelper()
        {
            AddFileToDirectoryListing("SECUINFO");
            AddFileToDirectoryListing("INPUT2SAVE");
            AddFileToDirectoryListing("NETSAFETYRATING");
            AddFileToDirectoryListing("RACEHISTORY");
            AddFileToDirectoryListing("VIDEOPLAYEDMANAGER");
            AddFileToDirectoryListing("ASYNCCHALLENGES");
            AddFileToDirectoryListing("CAREER MANAGER SDU NAME");
            AddFileToDirectoryListing("PROFILE");
            AddFileToDirectoryListing("REWARDS_SAVE");
            AddFileToDirectoryListing("VEHICLESETTINGSMANAGER");
            AddFileToDirectoryListing("VEHICLESETUPSAVEDDATA");
            AddFileToDirectoryListing("EVENTSPEECH");
            AddFileToDirectoryListing("NETRACEEVENTS");
            AddFileToDirectoryListing("CAREERPARAMS");
            AddFileToDirectoryListing("EGONET TRANSLATIONS");
            AddFileToDirectoryListing("PADDING");
            AddFileToDirectoryListing("UNLOCKS");
            AddFileToDirectoryListing("VERSION");
        }
        public DirtSecurityHelper(IEnumerable<string> fileList)
        {
            foreach (string file in fileList)
            {
                AddFileToDirectoryListing(file);
            }
        }
        public void AddFileToDirectoryListing(string fileName)
        {
            HashFileList.Add(new DirtSecuritySave.FileEntry()
            {
                FilenameHash = Dirt.DirtSave.CalculateStringHash(fileName),
                ObfFilename = Dirt.DirtSave.ObfuscateString(fileName),
                FileName = fileName
            });
        }

        public string GetObfuscatedNameFromFilename(string filename)
        {
            return HashFileList.Find(file => file.FileName == filename).ObfFilename;
        }

        public List<DirtSecuritySave.FileEntry> GetFileListing()
        {
            return HashFileList.Count > 0 ? HashFileList : null;
        }
    }
    public class DirtException : Exception
    {
        internal DirtException(string Message)
            : base("DiRT:" + Message)
        {
        }
    }

}
