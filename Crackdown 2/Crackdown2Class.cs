using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Crackdown2
{
    class Crackdown2Class
    {
        public EndianIO io;
        public CrakBlock[] Blocks;
        public short agility;
        public short driving;
        public short explosives;
        public short strength;
        public short firearms;

        public struct CrakBlock
        {
            public uint Magic; // Magic "CRAK" int.
            public uint Version; // The version of the CRAK block.  Alway 0x24.
            public uint Id; // The Id of the CRAK block.
            public uint Checksum; // The checksum of the CRAK block.
            public int Size; // The size of the CRAK relative to offset 0x14.
            public byte[] Data; // The data of the CRAK block.
        }

        private uint CRAKCalculate(byte[] Data)
        {
            uint sum = 0;
            for (int x = 0; x < Data.Length; x++)
            {
                uint num1 = Data[x];
                uint num3 = num1 | sum;
                sum &= num1;
                sum = num3 & ~(sum);
            }
            return sum;
        }

        public void LoadSave(EndianIO io)
        {
            this.io = io;
            io.Open();
            io.Stream.Position = 0;
            Blocks = new CrakBlock[0];
            while (io.Stream.Position < io.Stream.Length)
            {
                Array.Resize(ref Blocks, Blocks.Length + 1);
                CrakBlock cblock = new CrakBlock();
                cblock.Magic = io.In.ReadUInt32();
                cblock.Version = io.In.ReadUInt32();
                cblock.Id = io.In.ReadUInt32();
                cblock.Checksum = io.In.ReadUInt32();
                cblock.Size = io.In.ReadInt32();
                cblock.Data = io.In.ReadBytes(cblock.Size);
                Blocks[Blocks.Length - 1] = cblock;
                if (cblock.Id == 1)
                    break;
            }
            int statIndex = FindBlock(1);
            EndianIO stats = new EndianIO(Blocks[statIndex].Data, EndianType.BigEndian);
            stats.Open();
            stats.SeekTo(1);
            agility = (short)stats.In.ReadByte();
            stats.Stream.Position += 9;
            driving = (short)stats.In.ReadByte();
            stats.Stream.Position += 9;
            explosives = (short)stats.In.ReadByte();
            stats.Stream.Position += 9;
            strength = (short)stats.In.ReadByte();
            stats.Stream.Position += 9;
            firearms = (short)stats.In.ReadByte();
            stats.Close();
            /*
            // Now we find the block that contains the stats and load the hidden orbs and agility orbs
            int StatsIndex = FindBlock(0x10);
            EndianIO stats = new EndianIO(Blocks[StatsIndex].Data, EndianType.BigEndian);
            stats.Open();
            stats.Stream.Position = 0x8C; // Seek to the hidden and agility orbs
            HiddenOrbs = stats.In.ReadInt32();
            AgilityOrbs = stats.In.ReadInt32();
            stats.Close();
            
            // Now we find the list of orb flags
            int OrbIndex = FindBlock(0xC);
            EndianIO orbs = new EndianIO(Blocks[OrbIndex].Data, EndianType.BigEndian);
            orbs.Open();
            orbs.Stream.Position = 0;
            OrbFlags = new int[800];
            for (int i = 0; i < 800; i++)
                OrbFlags[i] = orbs.In.ReadInt32();
            orbs.Close();
             */
        }

        public void WriteStats(int AgilityOrbs, int HiddenOrbs)
        {
            /*
            int StatsIndex = FindBlock(0x10);
            EndianIO stats = new EndianIO(Blocks[StatsIndex].Data, EndianType.BigEndian);
            stats.Open();
            stats.Stream.Position = 0x8C; // Seek to the hidden and agility orbs
            stats.Out.Write(HiddenOrbs);
            stats.Out.Write(AgilityOrbs);
            stats.Close();
            
            Blocks[StatsIndex].Checksum = CRAKCalculate(Blocks[StatsIndex].Data);
             */
        }

        public void WriteSave()
        {
            int statIndex = FindBlock(1);
            EndianIO stats = new EndianIO(Blocks[statIndex].Data, EndianType.BigEndian);
            stats.Open();
            stats.SeekTo(1);
            stats.Out.Write((byte)agility);
            stats.Out.Write((byte)agility);
            stats.Stream.Position += 8;
            stats.Out.Write((byte)driving);
            stats.Out.Write((byte)driving);
            stats.Stream.Position += 8;
            stats.Out.Write((byte)explosives);
            stats.Out.Write((byte)explosives);
            stats.Stream.Position += 8;
            stats.Out.Write((byte)strength);
            stats.Out.Write((byte)strength);
            stats.Stream.Position += 8;
            stats.Out.Write((byte)firearms);
            stats.Out.Write((byte)firearms);
            stats.Close();
            Blocks[statIndex].Checksum = CRAKCalculate(Blocks[statIndex].Data);
            io.Stream.Position = 0;
            foreach (CrakBlock block in Blocks)
            {
                io.Out.Write(block.Magic);
                io.Out.Write(block.Version);
                io.Out.Write(block.Id);
                io.Out.Write(block.Checksum);
                io.Out.Write(block.Size);
                io.Out.Write(block.Data);
            }
        }

        private int FindBlock(uint Id)
        {
            for (int i = 0; i < Blocks.Length; i++)
            {
                if (Blocks[i].Id == Id)
                    return i;
            }
            throw new Exception("Cannot find block with id " + Id.ToString("X"));
        }
    }
}
