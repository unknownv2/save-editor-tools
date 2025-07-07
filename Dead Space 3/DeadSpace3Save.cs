using System;
using System.Collections.Generic;
using System.IO;
using ElectronicArts;

namespace DeadSpace
{
    public class DeadSpace3Save
    {
        public class InventoryItem
        {
            public Guid Guid;
            public int Count;
            public bool Unknown1;
            public byte[] Unknown2;
        }

        private readonly EndianIO _io;
        private EA _eaSecurityHeader;
        private EndianIO g_SaveIO;
        private EndianIO g_Header;
        public List<InventoryItem> RigInventory { get; set; }
        public List<InventoryItem> SafeInventory { get; set; }
        public List<Guid> Suits { get; set; }
        public Guid EquippedSuit { get; set; }
        private SaveBlockManager Manager;

        public int OpenSlotCount { get; set; }
        public int Tungsten { get; set; }
        public int Semiconductor { get; set; }
        public int ScrapMetal { get; set; }
        public int SomaticGel { get; set; }
        public int Transducer { get; set; }
        public int RationSeals { get; set; }

        private byte[] Footer;

        internal DeadSpace3Save(EndianIO io)
        {
            _io = io;
            Read();
        }

        void Read()
        {
            _eaSecurityHeader = new EA(_io);
            var header = _io.In.ReadBytes(_eaSecurityHeader.Header.Block1len);
            if (!Security.VerifySave(ref header, _io.In.ReadBytes(_eaSecurityHeader.Header.Block2len), out g_SaveIO) || g_SaveIO == null)
                throw new Exception("Dead Space 3: invalid save data detected!");

            g_Header = new EndianIO(header, EndianType.BigEndian, true);
            g_Header.SeekTo(0x10);
            uint dataStart = g_Header.In.ReadUInt32();
            uint dataLength = g_Header.In.ReadUInt32();

            Manager = new SaveBlockManager(g_SaveIO.In, dataStart, dataLength);
            g_SaveIO.SeekTo(dataStart + dataLength);
            Footer = g_SaveIO.In.ReadBytes(g_Header.In.SeekNReadInt32(0x0C) - (dataStart + dataLength));
            var reader = new EndianReader(Manager.GetBlock(0xBC2179DD, new Guid(new byte[16])), EndianType.BigEndian);
            OpenSlotCount = reader.ReadInt32();
            Tungsten = reader.ReadInt32();
            Semiconductor = reader.ReadInt32();
            ScrapMetal = reader.ReadInt32();
            SomaticGel = reader.ReadInt32();
            Transducer = reader.ReadInt32();
            RationSeals = reader.ReadInt32();
            /*
            RigInventory = ReadInventoryListing(reader);
            reader = new EndianReader(Manager.GetBlock(0x695615C0, new Guid(new byte[16])), EndianType.BigEndian);
            reader.SeekTo(((reader.ReadInt32() + (((reader.ReadInt32()) + (reader.ReadInt32() * 9)))) * 0x18) + 0x28);
            SafeInventory = ReadInventoryListing(reader);
            reader = new EndianReader(Manager.GetBlock(0xF7AEC8A0, new Guid(new byte[16])), EndianType.BigEndian);
            EquippedSuit = new Guid(reader.ReadBytes(0x10).ToHexString());

            ReadSuitList(reader);
            */
        }

        public void Save()
        {
            // write the resources
            var writer = new EndianIO(Manager.GetBlock(0xBC2179DD, new Guid(new byte[16])), EndianType.BigEndian, true);
            writer.SeekTo(0);
            writer.Out.Write(OpenSlotCount);
            writer.Out.Write(Tungsten);
            writer.Out.Write(Semiconductor);
            writer.Out.Write(ScrapMetal);
            writer.Out.Write(SomaticGel);
            writer.Out.Write(Transducer);
            writer.Out.Write(RationSeals);

            Manager.InjectBlock(0xBC2179DD, new Guid(new byte[16]), writer.ToArray());

            /*
            // write the Rig inventory (active)
            writer.Out.Write(RigInventory.Count);
            foreach (var inventoryItem in RigInventory)
            {
                writer.Out.Write(Horizon.Functions.Global.hexStringToArray(inventoryItem.Guid.ToString().Replace("-", "")));
                writer.Out.Write(inventoryItem.Count);
                writer.Out.Write(inventoryItem.Unknown1);
                writer.Out.Write(new byte[7]);
            }
            
            // write the Safe inventory (passive)
            writer = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            var block = Manager.GetBlock(0x695615C0, new Guid(new byte[16]));
            writer.Out.Write(block.ReadBytes(0, ((block.ReadInt32(0) + (((block.ReadInt32(4)) + (block.ReadInt32(8) * 9)))) * 0x18) + 0x28));
            writer.Out.Write(SafeInventory.Count);
            foreach (var inventoryItem in SafeInventory)
            {
                writer.Out.Write(Horizon.Functions.Global.hexStringToArray(inventoryItem.Guid.ToString().Replace("-", "")));
                writer.Out.Write(inventoryItem.Count);
                writer.Out.Write(inventoryItem.Unknown1);
                writer.Out.Write(inventoryItem.Unknown2 ?? new byte[7]);
            }

            Manager.InjectBlock(0x695615C0, new Guid(new byte[16]), writer.ToArray());
            //Manager.InjectBlock(0xF7AEC8A0, new Guid(new byte[16]), Horizon.Functions.Global.hexStringToArray(EquippedSuit.ToString().Replace("-", "")));
            */
            WriteToFile(Manager.Export());
            writer.Close();
        }

        public void WriteToFile(EndianIO saveData)
        {
            var save = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            // update save data length
            var preloadLen = g_Header.In.SeekNReadUInt32(0x10);
            //var newSaveLength = (int)(preloadLen + saveData.Length + Footer.Length);
            g_SaveIO.SeekTo(0);
            save.Out.Write(g_SaveIO.In.ReadBytes(preloadLen));
            save.Out.Write(saveData.ToArray());
            save.Out.Write(Footer);

            // updated save data sizes
            //g_Header.Out.SeekNWrite(0x0C, newSaveLength);
            //g_Header.Out.SeekNWrite(0x4C, newSaveLength);

            g_Header.Out.SeekNWrite(0x08, Security.CalculateSum(g_Header.Stream, save.Stream, (int)save.Length));

            _io.Out.SeekTo(0x1C);
            _io.Out.Write(g_Header.ToArray());
            _io.Out.Write(save.ToArray());
            _eaSecurityHeader.FixChecksums();
        }

        private List<InventoryItem> ReadInventoryListing(EndianReader reader)
        {
            var inventoryItemCount = reader.ReadInt32();

            if (inventoryItemCount <= 0) return null;

            var inventory = new List<InventoryItem>();
            for (int i = 0; i < inventoryItemCount; i++)
            {
                inventory.Add(new InventoryItem
                {
                    Guid = new Guid(reader.ReadBytes(0x10).ToHexString()),
                    Count = reader.ReadInt32(),
                    Unknown1 = reader.ReadBoolean(),
                    Unknown2 = reader.ReadBytes(7)
                });
            }
            return inventory;
        }

        private void ReadSuitList(EndianReader reader)
        {
            Suits = new List<Guid>();
        }
    }

    class Security
    {
        internal static bool VerifySave(ref byte[] saveHeader, byte[] saveData, out EndianIO io)
        {
            io = null;
            var header = new EndianIO(saveHeader, EndianType.BigEndian, true);

            // verify static header information
            if (header.In.ReadUInt32() != 0x9DAA30A6 || header.In.ReadInt32() != 0x0B)
                return false;

            var checksum = header.In.ReadInt32();
            var bodyLength = header.In.ReadInt32();

            header.Out.SeekNWrite(0x8, 0x00);
            header.SeekTo(0);

            var saveGameData = saveData.ReadBytes(0, bodyLength);
            var save = new EndianReader(saveGameData, EndianType.BigEndian);
            // clear the stored checksum value

            int sum = CalculateSum(header.Stream, save.BaseStream, bodyLength);
            save.Close();
            io = new EndianIO(saveGameData, EndianType.BigEndian, true);
            return (checksum == sum);
        }
        internal static int CalculateSum(Stream header, Stream save, int saveLength)
        {
            // clear the checksum
            header.Seek(8, SeekOrigin.Begin);
            header.Write(new byte[4], 0, 4);
            header.Seek(0, SeekOrigin.Begin);
            // calculate the sum
            int sum = 0;
            const int mul = 0x1003F;
            for (int i = 0; i < 0x80; i++)
            {
                sum = ((((sum) * mul)) + header.ReadByte());
            }
            save.Position = 0;
            for (int i = 0; i < saveLength; i++)
            {
                sum = ((((sum) * mul)) + save.ReadByte());
            }
            return sum;
        }
    }

    public class SaveBlockManager : List<SaveBlockManager.SaveBlock>
    {
        public class SaveBlock
        {
            public uint BlockId;
            public int Length;
            public Guid Guid;
            public byte[] Data;
        }


        public SaveBlockManager(EndianReader reader, long startPosition, long dataLength)
        {
            var len = 0;
            SaveBlock saveBlock;
            reader.SeekTo(startPosition);
            while (len  < dataLength)
            {
                var blockId = reader.ReadUInt32();
                saveBlock = new SaveBlock
                    {
                        BlockId = blockId,
                        Length = reader.ReadInt32(),
                        Guid = new Guid(reader.ReadBytes(0x10).ToHexString())
                    };
                saveBlock.Data = reader.ReadBytes(saveBlock.Length);
                Add(saveBlock);
                len += (0x18 + saveBlock.Length);
            }
        }

        public byte[] GetBlock(uint blockId, Guid guid)
        {
            return Find(block => (block.BlockId == blockId) && block.Guid.CompareTo(guid) == 0).Data;
        }

        public void InjectBlock(uint blockId, Guid guid, byte[] data)
        {
            var saveBlock = Find(block => (block.BlockId == blockId) && block.Guid.CompareTo(guid) == 0);
            if (saveBlock == null)
                throw new Exception(string.Format("Could not find a block matching the value '{0:X8}'!", blockId));

            saveBlock.Data = data;
            saveBlock.Length = data.Length;
        }

        public EndianIO Export()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            foreach (var saveBlock in this)
            {
                io.Out.Write(saveBlock.BlockId);
                io.Out.Write(saveBlock.Length);
                io.Out.Write(Horizon.Functions.Global.hexStringToArray(saveBlock.Guid.ToString().Replace("-", "")));
                io.Out.Write(saveBlock.Data);
            }
            return io;
        }
    }
}
