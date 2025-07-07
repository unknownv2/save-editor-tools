using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ElectronicArts;

namespace DeadSpace
{
    public class DeadSpace2Save
    {
        private EndianIO IO;

        public DeadSpace2Save(EndianIO io)
        {
            IO = io;
            Read();
        }

        private EndianIO SaveIO;
        private EndianIO PlayerIO;
        private EndianIO PlayerInventory, PlayerStats;
        private EA _eaSecurityHeader;
        private BlockManager BlockManager;
        private PlayerEntryManager PlayerEntries;

        // Player Stats
        public int Credits { get; set; }
        public int Nodes { get; set; }

        private void Read()
        {
            _eaSecurityHeader = new EA(IO);
            if (!VerifySecurityHeader())
                throw new Exception("Dead Space 2: invalid save header detected.");

            IO.SeekTo(_eaSecurityHeader.Header.Block1len + 0x1C);

            SaveIO = new EndianIO(IO.In.ReadBytes(_eaSecurityHeader.Header.Block2len), EndianType.BigEndian, true);
            BlockManager = new BlockManager(SaveIO);

            PlayerIO = new EndianIO(BlockManager.Extract(BlockManager.Find(block => block.Id == 0x9B5E2816)), EndianType.BigEndian, true);

            PlayerEntries = new PlayerEntryManager(PlayerIO, Version.DeadSpace2);

            PlayerInventory = PlayerEntries.Extract(PlayerEntries.Find(playerEntry => playerEntry.Id == 0xA3A270E1));
            PlayerInventory.SeekTo(4);
            Credits = PlayerInventory.In.ReadInt32();

            PlayerStats = PlayerEntries.Extract(PlayerEntries.Find(playerEntry => playerEntry.Id == 0x25E88D3D));
            Nodes = PlayerStats.In.SeekNReadInt32(0x0C);
        }

        public void Save()
        {
            // write the player inventory and info
            PlayerInventory.SeekTo(4);
            PlayerInventory.Out.Write(Credits);

            PlayerStats.SeekTo(0x0C);
            PlayerStats.Out.Write(Nodes);

            PlayerEntries.Inject(PlayerEntries.Find(playerEntry => playerEntry.Id == 0xA3A270E1), PlayerInventory.ToArray());
            PlayerEntries.Inject(PlayerEntries.Find(playerEntry => playerEntry.Id == 0x25E88D3D), PlayerStats.ToArray());

            BlockManager.Inject(BlockManager.Find(block => block.Id == 0x9B5E2816), PlayerEntries.Export());

            IO.SeekTo(0x1C +_eaSecurityHeader.Header.Block1len);
            IO.Out.Write(BlockManager.Export());

            FixSecurityHeader();
            _eaSecurityHeader.FixChecksums();
        }

        private void FixSecurityHeader()
        {
            IO.SeekTo(EA.EA_Header.HEADER_SIZE);
            var securityHeader = new EndianIO(IO.In.ReadBytes(_eaSecurityHeader.Header.Block1len), EndianType.BigEndian, true);
            var newHeader = securityHeader.ToArray();
            newHeader.WriteInt32(0x04, 0);
            var sum = DeadSpaceSum(newHeader, 0);
            IO.SeekTo(_eaSecurityHeader.Header.Block1len + 0x1C);
            sum = DeadSpaceSum(IO.In.ReadBytes(securityHeader.In.SeekNReadInt32(0x6C)), sum);
            IO.SeekTo(EA.EA_Header.HEADER_SIZE + 4);
            IO.Out.Write(sum);
            IO.Stream.Flush();
        }

        private bool VerifySecurityHeader()
        {
            IO.SeekTo(EA.EA_Header.HEADER_SIZE);
            var securityHeader = new EndianIO(IO.In.ReadBytes(_eaSecurityHeader.Header.Block1len), EndianType.BigEndian, true);
            var newHeader = securityHeader.ToArray();
            var originalSum = securityHeader.In.SeekNReadInt32(0x04);
            newHeader.WriteInt32(0x04, 0);
            var sum = DeadSpaceSum(newHeader, 0);
            IO.SeekTo(_eaSecurityHeader.Header.Block1len + 0x1C);
            return DeadSpaceSum(IO.In.ReadBytes(securityHeader.In.SeekNReadInt32(0x6C)), sum) == originalSum;
        }

        private int DeadSpaceSum(IEnumerable<byte> input, int sum)
        {
            return input.Aggregate(sum, (current, t) => (current*0x1003F) + t);
        }
    }
}
