using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Magic2013
{
    public class DeckEntry
    {
        public int Index;
        public short Flags;
        public byte CardsInDeck;
        public byte CardsInSideDeck;
        public byte NumberOfCardsUnlocked;
    }
    public struct BlockEntry
    {
        public int Length;
        public byte[] Data;
    }
    public class Profile
    {
        private EndianIO IO;
        private List<BlockEntry> BlockEntries;
        public List<DeckEntry> Decks;
 
        public Profile(EndianIO io)
        {
            IO = io;
            Read();
            ReadDeckData();
        }

        private void Read()
        {
            var totalLen = IO.In.ReadInt32() - 4;
            BlockEntries = new List<BlockEntry>();
            while (totalLen > 0)
            {
                var len = IO.In.ReadInt32();
                if (len > 0)
                {
                    var block = new BlockEntry { Length = len };
                    block.Data = IO.In.ReadBytes(block.Length);

                    BlockEntries.Add(block);

                    totalLen -= block.Length;
                }
                totalLen -= 4;
            }
        }

        private void ReadDeckData()
        {
            var io = new EndianIO(BlockEntries.Find(block => block.Length == 0xAE0).Data, EndianType.BigEndian, true);
            Decks = new List<DeckEntry>();
            io.SeekTo(0x13e);
            var deckCount = io.In.ReadByte();
            for (var i = 0; i < deckCount; i++)
            {
                io.SeekTo(0x140 + (i * 0x78));

                var deck = new DeckEntry
                    {
                        Index = io.In.ReadInt32(),
                        Flags = io.In.ReadInt16(),
                        CardsInDeck = io.In.ReadByte(),
                        CardsInSideDeck = io.In.ReadByte(),
                        NumberOfCardsUnlocked = io.In.ReadByte()
                    };

                Decks.Add(deck);
            }
        }
    }
}
