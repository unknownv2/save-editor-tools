using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Crackdown
{
    class CrackdownClass
    {
        public EndianIO io;
        public CrakBlock[] Blocks;
        public int AgilityOrbs;
        public int HiddenOrbs;
        public int[] OrbFlags;

        public struct CrakBlock
        {
            public uint Magic; // Magic "CRAK" int.
            public uint Version; // The version of the CRAK block.  Alway 6.
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
        }

        public void WriteStats(int AgilityOrbs, int HiddenOrbs)
        {
            int StatsIndex = FindBlock(0x10);
            EndianIO stats = new EndianIO(Blocks[StatsIndex].Data, EndianType.BigEndian);
            stats.Open();
            stats.Stream.Position = 0x8C; // Seek to the hidden and agility orbs
            stats.Out.Write(HiddenOrbs);
            stats.Out.Write(AgilityOrbs);
            stats.Close();
            
            Blocks[StatsIndex].Checksum = CRAKCalculate(Blocks[StatsIndex].Data);
        }

        public void WriteOrbFlags(int Flags)
        {
            for (int i = 0; i < OrbFlags.Length; i++)
            {
                OrbFlags[i] = Flags;
            }
            int OrbIndex = FindBlock(0xC);
            EndianIO orbs = new EndianIO(Blocks[OrbIndex].Data, EndianType.BigEndian);
            orbs.Open();
            orbs.Stream.Position = 0;
            foreach (int fl in OrbFlags)
                orbs.Out.Write(Flags);
            orbs.Close();
            Blocks[OrbIndex].Checksum = CRAKCalculate(Blocks[OrbIndex].Data);
        }

        public void WriteSave()
        {
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
