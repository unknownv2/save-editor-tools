using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Game.Rage
{
    public interface idInventoryItem
    {

    }
    public class RageInstanceSave
    {
        private EndianIO IO, SaveIO;

        public RageInstanceSave(EndianIO IO)
        {
            if (!IO.Opened)
                IO.Open();

            this.IO = IO;

            this.InflateSaveData();
            this.ReadSave();
        }
        private void InflateSaveData()
        {
            long SaveLen = this.IO.Length;
            MemoryStream MS = new MemoryStream(), DeflatedBlockBuffer = new MemoryStream();
            do
            {
                int templen = 0x1FFFC;
                if (templen >= SaveLen)
                    templen = (int)SaveLen - 4;

                byte[] SaveTempData = this.IO.In.ReadBytes(templen);

                // verify the compressed block data
                int sum = RageGameSave.idSaveFile_CalcSum(SaveTempData);
                int storedsum = this.IO.In.ReadInt32(EndianType.LittleEndian);

                if (sum != storedsum)
                {
                    throw new RageException("invalid/damaged compressed block buffer.");
                }

                DeflatedBlockBuffer.Write(SaveTempData, 0, templen);
                SaveLen -= (4 + templen);

            } while (SaveLen > 0);

            byte[] temp = new byte[0x10000];
            int len = 0;

            var DeflateStream = new DeflateStream(DeflatedBlockBuffer, CompressionMode.Decompress);

            while ((len = DeflateStream.Read(temp, 0, 0x10000)) > 0)
            {
                MS.Write(temp, 0, len);
            }
            DeflateStream.Close();

            this.SaveIO = new EndianIO(MS, EndianType.BigEndian, true);
        }
        private byte[] DeflateSaveData()
        {
            MemoryStream MS = new MemoryStream();
            var InflateStream = new DeflateStream(new MemoryStream(this.SaveIO.ToArray()), CompressionMode.Compress);

            byte[] temp = new byte[0x10000];
            int len = 0;

            while ((len = InflateStream.Read(temp, 0, 0x10000)) > 0)
            {
                MS.Write(temp, 0, len);
            }
            InflateStream.Close();

            EndianIO IO = new EndianIO(MS, EndianType.BigEndian, true), idSaveFile = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            
            long savelen = MS.Length;
            do
            {
                int templen = 0x1FFFC;
                if (templen >= savelen)
                    templen = (int)savelen;

                var saveData = IO.In.ReadBytes(templen);
                int sum = RageGameSave.idSaveFile_CalcSum(saveData);
                idSaveFile.Out.Write(saveData);
                idSaveFile.Out.Write(sum);

                savelen -= templen;
            } while (savelen > 0);

            IO.Close();
            return idSaveFile.ToArray();
        }
        private void ReadSave()
        {

        }
        private string ReadRageString()
        {
            return RageGameSave.idSaveFile_ReadString(this.SaveIO.In);
        }
        public byte[] ExtractSaveData()
        {
            return this.SaveIO.ToArray();
        }
    }
    public class RageInventorySave
    {
        private EndianIO IO;
        private StringBuilder Inventory;

        private long InventoryAddress;
        public RageInventorySave(EndianIO IO)
        {
            if (!IO.Opened)
                IO.Open();

            this.IO = IO;

            this.IO.In.BaseStream.Position = 4;

            this.ReadInventorySave();
        } 
        private void ReadInventorySave()
        {
            var reader = IO.In;
            uint BdFd = 0xBAADF00D;

            string aStr = ReadRageString();
            aStr = ReadRageString();
            aStr = ReadRageString();


            if (reader.ReadUInt32() != BdFd)
            {
                throw new RageException("Save/load issue with the object");
            }

            byte a = reader.ReadByte();
            if (a != 0x00)
            {
                int c = reader.ReadInt32();
                aStr = ReadRageString();
                aStr = ReadRageString();
                int b = reader.ReadInt32();
                for (int x = 0; x < b; x++)
                    aStr = ReadRageString();

                a = reader.ReadByte();
                a = reader.ReadByte();
                a = reader.ReadByte();
                c = reader.ReadInt32();
                aStr = ReadRageString();
                aStr = ReadRageString();
                c = reader.ReadInt32();
                aStr = ReadRageString();
                c = reader.ReadInt32();
            }

            if (reader.ReadUInt32() != BdFd)
            {
                throw new RageException("Save/load issue with the object");
            }

            int d = reader.ReadInt32();
            a = reader.ReadByte();
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                reader.ReadInt32();
                aStr = ReadRageString();
                reader.ReadInt32();
                int e = reader.ReadInt32();
                for (int i = 0; i < e; i++)
                {
                    aStr = ReadRageString();
                    reader.ReadInt32();
                }
            }
            reader.ReadInt32();
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                aStr = ReadRageString();
            }
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
                int e = reader.ReadInt32();
                for (int i = 0; i < e; i++)
                    reader.ReadByte();
                e = reader.ReadInt32();
                for (int i = 0; i < e; i++)
                    reader.ReadByte();
            }
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
                reader.ReadInt32();
                reader.ReadInt32();
            }
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
                reader.ReadInt32();
            }
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
            }
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
                int e = reader.ReadInt32();
                for (int i = 0; i < e; i++)
                    reader.ReadInt32();
            }
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt64();
            }
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
                int e = reader.ReadInt32();
                for (int i = 0; i < e; i++)
                    ReadRageString();
            }
            a = reader.ReadByte();
            reader.ReadInt32();
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
            }
            reader.ReadByte();
            d = reader.ReadInt32();
            for (int x = 0; x < d; x++)
            {
                ReadRageString();
                ReadRageString();
                int e = reader.ReadInt32();
                for (int i = 0; i < e; i++)
                {
                    for(int j = 0; j < 9; j++)
                        reader.ReadInt32();

                    ReadRageString();

                    reader.ReadByte();
                    reader.ReadByte();
                    reader.ReadInt32();
                    reader.ReadInt32();
                }
                reader.ReadInt32();
            }
            InventoryAddress = reader.BaseStream.Position;
            Inventory = new StringBuilder();
            // start of inventory
            int InventoryItemCount = reader.ReadInt32(); // inventory item count
            for (int x = 0; x < InventoryItemCount; x++)
            {
                string item = ReadRageString();
                int itemCOunt = reader.ReadInt32();
                reader.ReadInt32();
                Inventory.AppendLine(string.Format("{0} = {1};", item, itemCOunt));
                if(item.Contains("weapon/")) // idWeapon
                {
                    string subItem = ReadRageString();
                    int subItemCount = reader.ReadInt32();
                    Inventory.AppendLine(string.Format("{{\n{0} = {1}\n}}", subItem, subItemCount));
                    reader.ReadBytes(6);
                }
                else if (item.Contains("keys/")) // idVehicleKey
                {
                    reader.ReadInt32();
                    reader.ReadInt32();
                    reader.ReadByte();
                    reader.ReadByte();
                    reader.ReadBytes(0x0c);
                    reader.ReadBytes(0x24);
                    reader.ReadByte();
                    reader.ReadInt32();
                    reader.ReadByte();
                    d = reader.ReadInt32();
                    for (int i = 0; i < d; i++)
                    {
                        string itm = ReadRageString();
                        Inventory.AppendLine(string.Format("{{\n{0}\n}}", itm));
                    }
                    d = reader.ReadInt32();
                    for (int i = 0; i < d; i++)
                    {
                        string subItem = ReadRageString();
                        int subItemCount = reader.ReadInt32();
                        Inventory.AppendLine(string.Format("{{\n{0} = {1}\n}}", subItem, subItemCount));
                    }
                    reader.ReadByte();
                    aStr = ReadRageString();
                    int subitmcnt = reader.ReadInt32();
                    Inventory.AppendLine(string.Format("{{\n{0}\n}}", aStr));
                }
            }

            for(int x = 0; x < 6; x++)
                reader.ReadSingle();

            reader.ReadByte();

            for (int x = 0; x < 3; x++)
                reader.ReadSingle();

            for (int x = 0; x < 0x123; x++)
                reader.ReadInt32();

            string EquippedItem = ReadRageString();
            ReadRageString();

            for (int x = 0; x < 4; x++)
                ReadRageString();
            for (int x = 0; x < 4; x++)
                ReadRageString();
            for (int x = 0; x < 4; x++)
                ReadRageString();

            string Faction = ReadRageString();

            reader.ReadByte();

            // Next section
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();

            reader.ReadInt32();

            if (reader.ReadUInt32() != BdFd)
            {
                throw new RageException("Save/load issue with the object");
            }
        }
        public string ParseBinaryInventory()
        {
            return this.Inventory.ToString();
        }
        public void WriteInventory(byte[] InventoryData)
        {
            this.IO.Out.SeekTo(InventoryAddress);
            this.IO.Out.Write(InventoryData);
        }
        private string ReadRageString()
        {
            return RageGameSave.idSaveFile_ReadString(this.IO.In);
        }
    }
    public struct RageGameSave
    {
        public static string idSaveFile_ReadString(EndianReader reader)
        {
            int nStrCount = reader.ReadInt32(EndianType.LittleEndian);
            if (nStrCount > 0)
                return reader.ReadAsciiString(nStrCount);
            else
                return string.Empty;
        }
        /* Calculate the save's checksum*/
        public static int idSaveFile_CalcSum(byte[] Input)
        {
            //transforms an MD5 digest of the save buffer
            var Md5Hash = System.Security.Cryptography.MD5.Create().ComputeHash(Input);

            int A = (Md5Hash[0x0E] & -65281) | (Horizon.Functions.Global.ROTL32(Md5Hash[0x0F], 8) & 0xFF00);
            int B = (Md5Hash[0x0A] & -65281) | (Horizon.Functions.Global.ROTL32(Md5Hash[0x0B], 8) & 0xFF00);
            int C = (Md5Hash[0x06] & -65281) | (Horizon.Functions.Global.ROTL32(Md5Hash[0x07], 8) & 0xFF00);
            int D = (Md5Hash[0x0D] & 0xFF) | (Horizon.Functions.Global.ROTL32((A & 0xFFFF), 8) & -256);
            int F = (Md5Hash[0x09] & 0xFF) | (Horizon.Functions.Global.ROTL32((B & 0xFFFF), 8) & -256);
            int I = (Md5Hash[0x05] & 0xFF) | (Horizon.Functions.Global.ROTL32(C & 0xFFFF, 8) & -256);
            int G = (Md5Hash[0x0C] & 0xFF) | (Horizon.Functions.Global.ROTL32(D, 8) & -256);
            int E = (Md5Hash[0x08] & 0xFF) | (Horizon.Functions.Global.ROTL32(F, 8) & -256);
            int H = (Md5Hash[0x04] & 0xFF) | (Horizon.Functions.Global.ROTL32(I, 8) & -256);

            C = (Md5Hash[0x01] & 0xFF) | (Horizon.Functions.Global.ROTL32(((Md5Hash[0x02] & -65281) | (Horizon.Functions.Global.ROTL32(Md5Hash[0x03], 8) & 0xFF00)) & 0xFFFF, 8) & -256);
            return (((G ^ E) ^ H) ^ ((Md5Hash[0x00] & 0xFF) | (Horizon.Functions.Global.ROTL32(C, 8) & -256)));
        }
    }
    internal class RageException : Exception
    {
        internal RageException(string message)
            : base("Rage: " + message)
        {

        }
    }
}