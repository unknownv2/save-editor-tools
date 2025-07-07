using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ElectronicArts;

namespace DeadSpace
{
    class Save
    {
        internal Item[] Items;
        internal Block[] Blocks;
        internal int inventoryOffset;
        internal byte[] data;

        internal struct Block
        {
            internal uint ID; // The block ID
            internal int Size; // The block size
            internal byte[] Data; // The block data
        }

        internal struct Item
        {
            internal int Count; // The item count
            internal uint Unk1;
            internal ulong Unk2; // Unknown 1
            internal ulong Unk3; // Unknown 2
            internal int Index; // The item index
        }

        private static readonly UInt32[] crc32Table = {
            0x00000000, 0x04C11DB7, 0x09823B6E, 0x0D4326D9, 0x130476DC, 0x17C56B6B, 0x1A864DB2, 0x1E475005,
            0x2608EDB8, 0x22C9F00F, 0x2F8AD6D6, 0x2B4BCB61, 0x350C9B64, 0x31CD86D3, 0x3C8EA00A, 0x384FBDBD,
            0x4C11DB70, 0x48D0C6C7, 0x4593E01E, 0x4152FDA9, 0x5F15ADAC, 0x5BD4B01B, 0x569796C2, 0x52568B75,
            0x6A1936C8, 0x6ED82B7F, 0x639B0DA6, 0x675A1011, 0x791D4014, 0x7DDC5DA3, 0x709F7B7A, 0x745E66CD,
            0x9823B6E0, 0x9CE2AB57, 0x91A18D8E, 0x95609039, 0x8B27C03C, 0x8FE6DD8B, 0x82A5FB52, 0x8664E6E5,
            0xBE2B5B58, 0xBAEA46EF, 0xB7A96036, 0xB3687D81, 0xAD2F2D84, 0xA9EE3033, 0xA4AD16EA, 0xA06C0B5D,
            0xD4326D90, 0xD0F37027, 0xDDB056FE, 0xD9714B49, 0xC7361B4C, 0xC3F706FB, 0xCEB42022, 0xCA753D95,
            0xF23A8028, 0xF6FB9D9F, 0xFBB8BB46, 0xFF79A6F1, 0xE13EF6F4, 0xE5FFEB43, 0xE8BCCD9A, 0xEC7DD02D,
            0x34867077, 0x30476DC0, 0x3D044B19, 0x39C556AE, 0x278206AB, 0x23431B1C, 0x2E003DC5, 0x2AC12072,
            0x128E9DCF, 0x164F8078, 0x1B0CA6A1, 0x1FCDBB16, 0x018AEB13, 0x054BF6A4, 0x0808D07D, 0x0CC9CDCA,
            0x7897AB07, 0x7C56B6B0, 0x71159069, 0x75D48DDE, 0x6B93DDDB, 0x6F52C06C, 0x6211E6B5, 0x66D0FB02,
            0x5E9F46BF, 0x5A5E5B08, 0x571D7DD1, 0x53DC6066, 0x4D9B3063, 0x495A2DD4, 0x44190B0D, 0x40D816BA,
            0xACA5C697, 0xA864DB20, 0xA527FDF9, 0xA1E6E04E, 0xBFA1B04B, 0xBB60ADFC, 0xB6238B25, 0xB2E29692,
            0x8AAD2B2F, 0x8E6C3698, 0x832F1041, 0x87EE0DF6, 0x99A95DF3, 0x9D684044, 0x902B669D, 0x94EA7B2A,
            0xE0B41DE7, 0xE4750050, 0xE9362689, 0xEDF73B3E, 0xF3B06B3B, 0xF771768C, 0xFA325055, 0xFEF34DE2,
            0xC6BCF05F, 0xC27DEDE8, 0xCF3ECB31, 0xCBFFD686, 0xD5B88683, 0xD1799B34, 0xDC3ABDED, 0xD8FBA05A,
            0x690CE0EE, 0x6DCDFD59, 0x608EDB80, 0x644FC637, 0x7A089632, 0x7EC98B85, 0x738AAD5C, 0x774BB0EB,
            0x4F040D56, 0x4BC510E1, 0x46863638, 0x42472B8F, 0x5C007B8A, 0x58C1663D, 0x558240E4, 0x51435D53,
            0x251D3B9E, 0x21DC2629, 0x2C9F00F0, 0x285E1D47, 0x36194D42, 0x32D850F5, 0x3F9B762C, 0x3B5A6B9B,
            0x0315D626, 0x07D4CB91, 0x0A97ED48, 0x0E56F0FF, 0x1011A0FA, 0x14D0BD4D, 0x19939B94, 0x1D528623,
            0xF12F560E, 0xF5EE4BB9, 0xF8AD6D60, 0xFC6C70D7, 0xE22B20D2, 0xE6EA3D65, 0xEBA91BBC, 0xEF68060B,
            0xD727BBB6, 0xD3E6A601, 0xDEA580D8, 0xDA649D6F, 0xC423CD6A, 0xC0E2D0DD, 0xCDA1F604, 0xC960EBB3,
            0xBD3E8D7E, 0xB9FF90C9, 0xB4BCB610, 0xB07DABA7, 0xAE3AFBA2, 0xAAFBE615, 0xA7B8C0CC, 0xA379DD7B,
            0x9B3660C6, 0x9FF77D71, 0x92B45BA8, 0x9675461F, 0x8832161A, 0x8CF30BAD, 0x81B02D74, 0x857130C3,
            0x5D8A9099, 0x594B8D2E, 0x5408ABF7, 0x50C9B640, 0x4E8EE645, 0x4A4FFBF2, 0x470CDD2B, 0x43CDC09C,
            0x7B827D21, 0x7F436096, 0x7200464F, 0x76C15BF8, 0x68860BFD, 0x6C47164A, 0x61043093, 0x65C52D24,
            0x119B4BE9, 0x155A565E, 0x18197087, 0x1CD86D30, 0x029F3D35, 0x065E2082, 0x0B1D065B, 0x0FDC1BEC,
            0x3793A651, 0x3352BBE6, 0x3E119D3F, 0x3AD08088, 0x2497D08D, 0x2056CD3A, 0x2D15EBE3, 0x29D4F654,
            0xC5A92679, 0xC1683BCE, 0xCC2B1D17, 0xC8EA00A0, 0xD6AD50A5, 0xD26C4D12, 0xDF2F6BCB, 0xDBEE767C,
            0xE3A1CBC1, 0xE760D676, 0xEA23F0AF, 0xEEE2ED18, 0xF0A5BD1D, 0xF464A0AA, 0xF9278673, 0xFDE69BC4,
            0x89B8FD09, 0x8D79E0BE, 0x803AC667, 0x84FBDBD0, 0x9ABC8BD5, 0x9E7D9662, 0x933EB0BB, 0x97FFAD0C,
            0xAFB010B1, 0xAB710D06, 0xA6322BDF, 0xA2F33668, 0xBCB4666D, 0xB8757BDA, 0xB5365D03, 0xB1F740B4
        };

        internal static uint Calculate(byte[] Data)
        {
            uint r11 = ((uint)Data[0] << 24) + ((uint)Data[1] << 16) + ((uint)Data[2] << 8) + (uint)Data[3];
            r11 = ~r11;
            for (int i = 0; i < Data.Length - 4; i++)
            {
                uint r7 = (uint)((((ulong)r11 << 10) & 0x3FC00000000) >> 32) / 4;
                ushort r6 = (ushort)Data[4 + i];
                r11 <<= 8;
                r11 |= r6;
                r7 = crc32Table[r7];
                r11 = r7 ^ r11;
            }
            return ~r11;
        }

        internal void LoadSave(EndianIO save)
        {
            // Read section two
            save.Stream.Position = 0x494;
            data = save.In.ReadBytes(0x64000);

            // Create the section IO
            EndianIO io = new EndianIO(data, EndianType.BigEndian);
            io.Open();

            // Initialize the block array
            Blocks = new Block[0];

            // Seek to the start of the blocks
            io.Stream.Position = 0x34;

            uint nextId = io.In.ReadUInt32(); // Read the ID for the next block
            // Loop until the block ID is 0
            while (nextId != 0)
            {
                Array.Resize(ref Blocks, Blocks.Length + 1); // Add a block to the block array
                
                // Read in the block info
                Blocks[Blocks.Length - 1].ID = nextId;
                io.Stream.Position += 4;
                Blocks[Blocks.Length - 1].Size = io.In.ReadInt32();
                Blocks[Blocks.Length - 1].Data = io.In.ReadBytes(Blocks[Blocks.Length - 1].Size);

                // Read in the next block ID
                nextId = io.In.ReadUInt32();
            }

            
            // Find and load the inventory
            int inventoryIndex = FindBlock(0x9B5E2815);

            // Create an inventory IO
            EndianIO iv = new EndianIO(Blocks[inventoryIndex].Data, EndianType.BigEndian);
            iv.Open();

            // Seek to the first block in the inventory
            iv.Stream.Position = 0x14;

            uint nextId1 = iv.In.ReadUInt32(); // Read in the 
            
            // Loop through to find the items 
            while (nextId1 != 0xA3A270DE)
            {
                iv.Stream.Position += 4;
                int sizet = GetRealSize(iv.In.ReadInt32());
                iv.Stream.Position += sizet;
                nextId1 = iv.In.ReadUInt32();
            }

            // Record the inventory offset
            inventoryOffset = (int)iv.Stream.Position + 0xC;

            iv.Stream.Position = inventoryOffset - 8;

            // Get the item count
            int itemCount = (GetRealSize(iv.In.ReadInt32()) / 0x1c) - 1;
            
            // Create the item array
            Items = new Item[itemCount];

            iv.Stream.Position += 4;

            // Read in the items
            for (int i = 0; i < itemCount; i++)
            {
                Items[i].Count = iv.In.ReadInt32();
                Items[i].Unk1 = iv.In.ReadUInt32();
                Items[i].Unk2 = iv.In.ReadUInt64();
                Items[i].Unk3 = iv.In.ReadUInt64();
                Items[i].Index = iv.In.ReadInt32();
            }

            // Close the inventory io
            iv.Close();
            
        }

        internal void WriteSave(EndianIO save)
        {
            int inventoryIndex = FindBlock(0x9B5E2815);

            // Open the inventory block
            EndianIO iv = new EndianIO(Blocks[inventoryIndex].Data, EndianType.BigEndian);
            iv.Open();

            // Seek to the items
            iv.Stream.Position = inventoryOffset;
            
            // Write the items
            foreach (Item item in Items)
            {
                iv.Out.Write(item.Count);
                iv.Out.Write(item.Unk1);
                iv.Out.Write(item.Unk2);
                iv.Out.Write(item.Unk3);
                iv.Out.Write(item.Index);
            }

            // Close the inventory io
            iv.Close();

            // Create the section io
            EndianIO io = new EndianIO(data, EndianType.BigEndian);
            io.Open();
            
            // Seek to the start of the block
            io.Stream.Position = 0x34;

            // Loop through and write the blocks
            foreach (Block block in Blocks)
            {
                io.Out.Write(block.ID);
                io.Stream.Position += 4;
                io.Out.Write(block.Size);
                io.Out.Write(block.Data);
            }

            io.Close();

            save.SeekTo(0x494);
            save.Out.Write(data);

            uint dataSum = Calculate(data);
            save.SeekTo(0x14);
            save.Out.Write(dataSum);
            save.Stream.Position = 0;
            uint headerSum = Calculate(save.In.ReadBytes(0x18));
            save.Out.Write(headerSum);
        }

        private int FindBlock(uint ID)
        {
            for (int i = 0; i < Blocks.Length; i++)
            {
                if (Blocks[i].ID == ID)
                    return i;
            }

            throw new Exception("Cannot find block with ID 0x" + ID.ToString("X"));
        }

        private int GetRealSize(int Size)
        {
            return Size + 0x18;
        }

        internal long DeadSum(byte[] data)
        {
            long r3 = 0;

            for (int i = 0; i < data.Length; i++)
            {
                int r3int = (int)(r3 & 0xFFFFFFFF);
                long r3long = r3int;
                long r9 = r3long * 0x1003f; //(ulong)((r3 & 0xFFFFFFFF) * (r8 & 0xFFFFFFFF));
                r3 = r9 + data[i];
            }

            return r3;
        }

        internal long DeadSumPart2TheSuperDeadening(long seed, byte[] data, int size)
        {
            //long sum = 0;

            for (int i = 0; i < size; i++)
            {
                int r3int = (int)(seed & 0xFFFFFFFF);
                long r3long = r3int;
                long r9 = r3long * 0x1003f;
                seed = r9 + data[i];
            }
            return seed;
        }

        internal uint VerifySection(byte[] data, byte[] data2)
        {
            uint magic8 = getUInt(ref data, 0);
            if (magic8 != 8)
            {
                return 0;
            }

            int savepoint1 = 8;
            int len1 = 0;
            for (int i = 0; i < 0x20; i++)
            {
                if (data[savepoint1 + i] == 0)
                {
                    len1 = i;
                    break;
                }
            }

            int savepoint2 = 0x28;
            int len2 = 0;
            for (int i = 0; i < 0x20; i++)
            {
                if (data[savepoint2 + i] == 0)
                {
                    len2 = i;
                    break;
                }
            }

            byte[] dest = new byte[data.Length];

            storeUInt(ref dest, 0, getUInt(ref data, 0));
            storeUInt(ref dest, 4, getUInt(ref data, 4));

            int secpoint1 = 8;
            for (int i = 0; i < 8; i++)
            {
                storeUInt(ref dest, secpoint1, getUInt(ref data, secpoint1));
                secpoint1 += 4;
            }

            int secpoint2 = 0x28;
            for (int i = 0; i < 8; i++)
            {
                storeUInt(ref dest, secpoint2, getUInt(ref data, secpoint2));
                secpoint2 += 4;
            }

            storeUInt(ref dest, 0x48, getUInt(ref data, 0x48));
            storeUInt(ref dest, 0x4c, getUInt(ref data, 0x4c));
            storeUInt(ref dest, 0x50, getUInt(ref data, 0x50));
            storeUInt(ref dest, 0x54, getUInt(ref data, 0x54));
            storeUInt(ref dest, 0x58, getUInt(ref data, 0x58));
            storeUInt(ref dest, 0x5c, getUInt(ref data, 0x5c));
            storeUInt(ref dest, 0x60, getUInt(ref data, 0x60));
            storeUInt(ref dest, 0x64, getUInt(ref data, 0x64));
            storeUInt(ref dest, 0x68, getUInt(ref data, 0x68));
            storeUInt(ref dest, 0x6c, getUInt(ref data, 0x6c));

            Array.Copy(data, 0x70, dest, 0x70, 0x100);

            storeUInt(ref dest, 4, 0); // 0 the checksum

            ulong sum1 = (ulong)DeadSum(dest); // A NEW CHECKSUM HAS ARRIVED

            uint sec2size = getUInt(ref data, 0x6C);

            ulong sum2 = (ulong)DeadSumPart2TheSuperDeadening((long)sum1, data2, (int)sec2size);

            return (uint)sum2 & 0xffffffff;
        }

        internal void storeUInt(ref byte[] data, int index, uint word)
        {
            data[index] = (byte)((word & 0xff000000) >> 24);
            data[index + 1] = (byte)((word & 0xff0000) >> 16);
            data[index + 2] = (byte)((word & 0xff00) >> 8);
            data[index + 3] = (byte)(word & 0xff);
        }

        internal uint getUInt(ref byte[] array, int index)
        {
            return (uint)(array[index] << 24 | array[index + 1] << 16 | array[index + 2] << 8 | array[index + 3]);
        }
    }

    public class DeadSpace1Save
    {
        private EndianIO IO;
        private EndianIO SaveIO;
        private EndianIO PlayerIO;
        private EndianIO PlayerInventory, PlayerStats;
        private EA _eaSecurityHeader;
        private BlockManager BlockManager;
        private PlayerEntryManager PlayerEntries;

        // Player Stats
        public int Credits { get; set; }
        public int Nodes { get; set; }

        public DeadSpace1Save(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            _eaSecurityHeader = new EA(IO);

            IO.SeekTo(_eaSecurityHeader.Header.Block1len + 0x1C);

            SaveIO = new EndianIO(IO.In.ReadBytes(_eaSecurityHeader.Header.Block2len), EndianType.BigEndian, true);
            BlockManager = new BlockManager(SaveIO);

            PlayerIO = new EndianIO(BlockManager.Extract(BlockManager.Find(block => block.Id == 0x9B5E2815)), EndianType.BigEndian, true);

            PlayerEntries = new PlayerEntryManager(PlayerIO, Version.DeadSpace1);

            PlayerInventory = PlayerEntries.Extract(PlayerEntries.Find(playerEntry => playerEntry.Id == 0xA3A270DE));
            Credits = PlayerInventory.In.ReadInt32();

            PlayerStats = PlayerEntries.Extract(PlayerEntries.Find(playerEntry => playerEntry.Id == 0x7B80C13A));
            Nodes = PlayerStats.In.SeekNReadInt32(0x0C);
        }

        public void Save()
        {
            // write the player inventory and info
            PlayerInventory.SeekTo(0);
            PlayerInventory.Out.Write(Credits);

            PlayerStats.SeekTo(0x0C);
            PlayerStats.Out.Write(Nodes);

            PlayerEntries.Inject(PlayerEntries.Find(playerEntry => playerEntry.Id == 0xA3A270DE), PlayerInventory.ToArray());
            PlayerEntries.Inject(PlayerEntries.Find(playerEntry => playerEntry.Id == 0x7B80C13A), PlayerStats.ToArray());

            BlockManager.Inject(BlockManager.Find(block => block.Id == 0x9B5E2815), PlayerEntries.Export());

            IO.SeekTo(0x1C +_eaSecurityHeader.Header.Block1len);
            IO.Out.Write(BlockManager.Export());

            _eaSecurityHeader.FixChecksums();
        }
    }

    public class PlayerEntry
    {
        public uint Id;
        public int Length;
        public long Position;
    }
    public class PlayerEntryManager : List<PlayerEntry>
    {
        private EndianIO IO;

        public PlayerEntryManager(EndianIO io, Version gameVersion)
        {
            IO = io;

            var entryCount = IO.In.ReadInt32();
            for (var i = 0; i < entryCount; i++)
            {
                switch (gameVersion)
                {
                    case Version.DeadSpace1:
                        var entry = new PlayerEntry();
                        IO.Position += 0x10;
                        entry.Id = IO.In.ReadUInt32();
                        IO.Position += 0x4;
                        entry.Length = IO.In.ReadInt32();
                        IO.Position += 8;
                        entry.Position = IO.Position;
                        IO.Position += (entry.Length);
                        Add(entry);
                    break;
                    case Version.DeadSpace2:
                        entry = new PlayerEntry();
                        IO.Position += 0x10;
                        entry.Id = IO.In.ReadUInt32();
                        IO.Position += 0x6;
                        entry.Length = IO.In.ReadInt16();
                        entry.Position = IO.Position;
                        IO.Position += (entry.Length);
                        Add(entry);
                    break;
                }
            }
        }
        public EndianIO Extract(PlayerEntry entry)
        {
            IO.SeekTo(entry.Position);
            return new EndianIO(IO.In.ReadBytes(entry.Length), EndianType.BigEndian, true);
        }
        public void Inject(PlayerEntry entry, byte[] data)
        {
            if (entry.Length == data.Length)
            {
                IO.SeekTo(entry.Position);
                IO.Out.Write(data);
            }
            else
            {
                
            }
        }
        public byte[] Export()
        {
            return IO.ToArray();
        }
    }
    public class Block
    {
        public long Position;
        public uint Id;
        public int Unknown1;
        public int Length;
    }

    public class BlockManager : List<Block>
    {
        private EndianIO IO;

        public BlockManager(EndianIO io)
        {
            IO = io;

            var blockCount = io.In.ReadInt32();
            for (var i = 0; i < blockCount; i++)
            {
                var block = new Block { Id = io.In.ReadUInt32(), Unknown1 = io.In.ReadInt32(), Length = io.In.ReadInt32(), Position = io.Position};
                io.Position += block.Length;
                Add(block);
            }
        }

        public byte[] Extract(Block block)
        {
            IO.SeekTo(block.Position);
            return IO.In.ReadBytes(block.Length);
        }

        public void Inject(Block block, byte[] data)
        {
            if (block.Length == data.Length)
            {
                IO.SeekTo(block.Position);
                IO.Out.Write(data);
            }
            else
            {
                
            }
        }

        public byte[] Export()
        {
            return IO.ToArray();
        }
    }

    public enum Version
    {
        DeadSpace1,
        DeadSpace2,
        DeadSpace3
    }
}
