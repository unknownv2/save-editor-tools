using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Horizon.Functions;

namespace Volition
{
    internal enum SaintsRowGameType
    {
        SaintsRowIV,
        SaintsRowGOOH
    }
    internal struct XboxTableDefinition
    {
        internal string Name;
        internal string DisplayName;
        internal string Description;
        internal uint Hash;
    }
    internal class SaintsRowUpgradeData
    {
        internal struct UpgradeEntry
        {
            public uint Ident;
        }

        public Dictionary<uint, bool> TableUnlocked, TableNew, TableAvailable;
        public UpgradeEntry[] GameUpgrades;
        private readonly int _maxUpgradeCount;
        private readonly int _tableIndexSize;


        public SaintsRowUpgradeData(byte[] unlockTable, uint[] identArray, int maxUpgradeCount, SaintsRowGameType gameType)
        {
            _maxUpgradeCount = maxUpgradeCount;

            switch (gameType)
            {
                case SaintsRowGameType.SaintsRowIV:
                    _tableIndexSize = 0x30;
                    break;
                case SaintsRowGameType.SaintsRowGOOH:
                    _tableIndexSize = 0x31;
                    break;
            }
            GameUpgrades = new UpgradeEntry[identArray.Length];
            for (int x = 0; x < identArray.Length; x++)
            {
                GameUpgrades[x].Ident = identArray[x];
            }
            ParseBooleanTable(identArray, unlockTable, 0, TableUnlocked = new Dictionary<uint, bool>());
            ParseBooleanTable(identArray, unlockTable, 1, TableAvailable = new Dictionary<uint, bool>());
            ParseBooleanTable(identArray, unlockTable, 2, TableNew = new Dictionary<uint, bool>());
        }
        public SaintsRowUpgradeData(byte[] unlockTable, List<uint> identArray, int maxUpgradeCount, SaintsRowGameType gameType)
        {
            _maxUpgradeCount = maxUpgradeCount;

            switch (gameType)
            {
                case SaintsRowGameType.SaintsRowIV:
                    _tableIndexSize = 0x30;
                    break;
                case SaintsRowGameType.SaintsRowGOOH:
                    _tableIndexSize = 0x31;
                    break;
            }
            GameUpgrades = new UpgradeEntry[identArray.Count];
            for (int x = 0; x < identArray.Count; x++)
            {
                GameUpgrades[x].Ident = identArray[x];
            }
            ParseBooleanTable(identArray, unlockTable, 0, TableUnlocked = new Dictionary<uint, bool>());
            ParseBooleanTable(identArray, unlockTable, 1, TableAvailable = new Dictionary<uint, bool>());
            ParseBooleanTable(identArray, unlockTable, 2, TableNew = new Dictionary<uint, bool>());
        }

        private void ParseBooleanTable(List<uint> identities, byte[] tableBuffer, int tableIndex,
                               Dictionary<uint, bool> tableOutput)
        {
            EndianReader tableReader = new EndianReader(tableBuffer, EndianType.BigEndian);
            var i = 0;
            for (var j = 0; j < _tableIndexSize; j++)
            {
                tableReader.SeekTo((tableIndex * _tableIndexSize) + (i >> 3));
                var unlocks = BitHelper.ProduceBitmask(tableReader.ReadByte());
                for (var x = 0; x < 8; x++)
                {
                    if (i >= identities.Count)
                        return;

                    tableOutput.Add(identities[i++], unlocks[x]);
                }
            }
        }
        private void ParseBooleanTable(uint[] identities, byte[] tableBuffer, int tableIndex,
                                       Dictionary<uint, bool> tableOutput)
        {
            EndianReader tableReader = new EndianReader(tableBuffer, EndianType.BigEndian);
            var i = 0;
            for (var j = 0; j < (_maxUpgradeCount / 8) + 1; j++)
            {
                tableReader.SeekTo((tableIndex * _tableIndexSize) + (i >> 3));
                var unlocks = BitHelper.ProduceBitmask(tableReader.ReadByte());
                for (var x = 0; x < 8; x++)
                {
                    if (i < _maxUpgradeCount)
                        tableOutput.Add(identities[i++], unlocks[x]);
                }
            }
        }

        private byte[] SerializeTable(Dictionary<uint, bool> table)
        {
            var ms = new MemoryStream();
            var i = 0;
            var unlocks = new bool[8];
            ms.SetLength(_tableIndexSize);
            foreach (KeyValuePair<uint, bool> upgrade in table)
            {
                if (i == 8)
                {
                    ms.WriteByte(BitHelper.ConvertToWriteableByte(unlocks));
                    unlocks = new bool[8];
                    i = 0;
                }
                unlocks[i++] = upgrade.Value;
            }
            if (i != 8)
            {
                ms.WriteByte(BitHelper.ConvertToWriteableByte(unlocks));
            }
            
            return ms.ToArray();
        }

        public byte[] ToArray()
        {
            EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.Out.Write(SerializeTable(TableUnlocked));
            io.Out.Write(SerializeTable(TableAvailable));
            io.Out.Write(SerializeTable(TableNew));

            return io.ToArray();
        }
    }
}
