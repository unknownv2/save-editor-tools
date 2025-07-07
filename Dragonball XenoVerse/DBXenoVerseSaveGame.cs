using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Horizon.Functions;

namespace Bandai
{
    internal enum DBXVAttributes
    {
        MaxHealth,
        MaxKi,
        BasicAttacks,
        StrikeSupers,
        KiBlastSupers,
        MaxStamina
    }

    internal enum BagItemType
    {
        CostumeTopItem = 0x00, // shirts
        CostumeBottomItem = 0x01, // pants
        CostumeGlove = 0x02,
        CostumeShoe = 0x03,
        Accessories = 0x04,
        ZSoul = 0x05,
        MixingItem = 0x06,
        ImportantItem = 0x07,
        Capsule = 0x08,

        // not read as part of the bag loop, so we use another class 'SkillListing'
        Skill = 0x09 
        
        /*
        CostumeTopItem = 0x00,
        Accessories = 0x04,
        ZSoul = 0x05,
        MixingItem = 0x06,
        Capsule = 0x08,
        Skill = 0x09
        */
    }
    internal enum DBXVPlayerStats
    {
        Zeni,
        Level,
        Experience,
        AttributePoints,
    }
    internal struct BagItem
    {
        internal BagItemType Type;
        internal uint ItemId;
        internal uint ItemCount;
        internal uint Unknown;
    }
    internal struct SkillEntry
    {
        internal bool Unlocked;
        internal uint UnlockedSkillId;
        internal bool Seen;
        internal uint SeenSkillId;
    }
    internal class SkillListing : List<SkillEntry>
    {
        private const int SkillMaxCount = 0x400;
        internal SkillListing(EndianIO io)
        {
            for (var i = 0; i < SkillMaxCount; i++)
            {
                Add(new SkillEntry
                {
                    Unlocked = io.In.ReadInt32() == 1, UnlockedSkillId = io.In.ReadUInt32(), Seen = io.In.ReadInt32() == 1,
                    SeenSkillId = io.In.ReadUInt32()
                });
            }
        }
    }

    internal class DBXXVCharacterEntry
    {
        internal bool IsEmpty { get { return string.IsNullOrEmpty(Name); }}
        internal bool IsTransplanted { get { return TransplantData != null; }}
        internal byte[] TransplantData;
        internal string Name;
        internal Dictionary<DBXVPlayerStats, uint> PlayerStats = new Dictionary<DBXVPlayerStats, uint>();
        internal Dictionary<DBXVAttributes, uint> PlayerAttributes = new Dictionary<DBXVAttributes, uint>(); 

        internal DBXXVCharacterEntry(EndianReader reader)
        {
            reader.BaseStream.Position += 0x24;

            Name = reader.ReadAsciiString(0x40);
            reader.BaseStream.Position += 0x28;

            PlayerStats[DBXVPlayerStats.Level] = reader.ReadUInt32();
            PlayerStats[DBXVPlayerStats.Experience] = reader.ReadUInt32();
            PlayerStats[DBXVPlayerStats.AttributePoints] = reader.ReadUInt32();

            foreach (DBXVAttributes attribute in Enum.GetValues(typeof(DBXVAttributes)))
            {
                PlayerAttributes[attribute] = reader.ReadUInt32();
            }

            reader.BaseStream.Position += 0x2C0;
        }

        internal byte[] ToArray()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian,true);
            io.Stream.SetLength(0x370);
            if (IsTransplanted)
            {
                io.Out.Write(TransplantData);
            }
            io.SeekTo(0x00);
            io.Out.WriteAsciiString(Name, 0x40);
            io.Position += 0x28;

            io.Out.Write(PlayerStats[DBXVPlayerStats.Level]);
            io.Out.Write(PlayerStats[DBXVPlayerStats.Experience]);
            io.Out.Write(PlayerStats[DBXVPlayerStats.AttributePoints]);

            foreach (DBXVAttributes attribute in Enum.GetValues(typeof(DBXVAttributes)))
            {
                io.Out.Write(PlayerAttributes[attribute]);
            }

            return io.ToArray();
        }

        internal void Write(EndianIO io)
        {
            if (IsTransplanted)
            {
                io.Out.Write(TransplantData);
                io.Position -= 0x370;
            }

            io.Position += 0x24;
            io.Out.WriteAsciiString(Name, 0x40);
            io.Position += 0x28;

            io.Out.Write(PlayerStats[DBXVPlayerStats.Level]);
            io.Out.Write(PlayerStats[DBXVPlayerStats.Experience]);
            io.Out.Write(PlayerStats[DBXVPlayerStats.AttributePoints]);

            foreach (DBXVAttributes attribute in Enum.GetValues(typeof(DBXVAttributes)))
            {
                io.Out.Write(PlayerAttributes[attribute]);
            }

            io.Position += 0x2C0;
        }
        internal void Clone(DBXXVCharacterEntry clone)
        {
            clone.Name = Name;
            clone.PlayerStats[DBXVPlayerStats.Level] = PlayerStats[DBXVPlayerStats.Level];
            clone.PlayerStats[DBXVPlayerStats.Experience] = PlayerStats[DBXVPlayerStats.Experience];
            clone.PlayerStats[DBXVPlayerStats.AttributePoints] = PlayerStats[DBXVPlayerStats.AttributePoints];

            foreach (DBXVAttributes attribute in Enum.GetValues(typeof (DBXVAttributes)))
            {
                clone.PlayerAttributes[attribute] = PlayerAttributes[attribute];
            }
        }
    }
    internal class DBXenoVerseSaveGame
    {
        private readonly EndianIO _io;
        internal List<int> Attributes = new List<int>();
        internal List<DBXXVCharacterEntry> CharacterEntries = new List<DBXXVCharacterEntry>();
        internal bool CharacterSlotsUnlocked ;

        internal uint Zeni;

        internal DBXenoVerseSaveGame(EndianIO io)
        {
            _io = io;

            if (_io == null || !_io.Opened)
            {
                throw new Exception("Dragon Ball XenoVerse: invalid savegame I/O detected!");
            }

            Read();
        }

        internal void Save()
        {
            // write back player stats
            _io.Out.SeekNWrite(0x08, Zeni);

            _io.Out.SeekNWrite(0x40, CharacterSlotsUnlocked ? 0x08 : 0x07);

            _io.SeekTo(0x00030AE4);
            foreach (var characterEntry in CharacterEntries)
            {
                if (characterEntry.IsEmpty) continue;
                characterEntry.Write(_io);
            }

            //write player attributes
            /*
            _io.SeekTo(0x00030B7C);
            foreach (DBXVAttributes attribute in Enum.GetValues(typeof(DBXVAttributes)))
            {
                _io.Out.Write(PlayerAttributes[attribute]);
            }

            _io.SeekTo(0x00030B08);
            _io.Out.WriteAsciiString(PlayerName, 32);
            _io.SeekTo(0x00030B70);
            _io.Out.Write(PlayerStats[DBXVPlayerStats.Level]);
            _io.Out.Write(PlayerStats[DBXVPlayerStats.Experience]);
            _io.Out.Write(PlayerStats[DBXVPlayerStats.AttributePoints]);
            */
        }

        private void Read()
        {
            _io.SeekTo(0x08);
            Zeni = _io.In.ReadUInt32();

            CharacterSlotsUnlocked =  (_io.In.SeekNPeekByte(0x43) == 0x08);

            // read player attributes
            _io.SeekTo(0x00030AE4);
            for (int i = 0; i < 8; i++)
            {
                CharacterEntries.Add(new DBXXVCharacterEntry(_io.In));
            }

            // read skill unlocks
            _io.SeekTo(0x01CAD4); // there are 0x400 entries

            // Bag - starts @0x13A94

            // Clothing = 0x013AA4
            // Accessories = 0x00017A94
            // Z-Souls = 0x00018A94
            // Mixing Items = 0x00019A94
            // Capsules = 0x0001BA94

            // unlock table
        }

        internal void UnlockAllItemsByType(BagItemType type)
        {
            // get position of bag item
            _io.SeekTo(0x13A94 * (uint)(type) * 0x1000 + 4);
            // position 4 is where the item id begins
            for (int i = 0; i < 0x100; i++)
            {
                _io.Out.Write(i); // write item index id
                _io.Out.Write(99); // write max value
                _io.Position += 8; // move forward 8 bytes to correct position
            }
        }

        internal void Transplant(int originalIndex, int newIndex, DBXXVCharacterEntry clone)
        {
            _io.SeekTo(0x00030AE4 + (originalIndex * 0x370));
            clone.TransplantData = _io.In.ReadBytes(0x370);
        }
    }
}
