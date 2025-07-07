using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YuGiOhDecadeDuels;

namespace YuGiOhMilleniumDuels
{
    internal class CardListEntry
    {
        public List<bool> Unlocked;
        public bool New;

        public CardListEntry(byte mask)
        {
            var bitmask = Horizon.Functions.BitHelper.ProduceBitmask(mask);
            Unlocked = new List<bool>();
            New = bitmask[7];
            for (int i = 0; i < 3; i++)
            {
                Unlocked.Add(bitmask[4+i]);
            }
        }

        public byte ToArray()
        {
            bool[] mask = new bool[8];
            
            for (int i = 0; i < 3; i++)
            {
                mask[i + 4] = Unlocked[i];
            }
            mask[7] = New;
            byte card = Horizon.Functions.BitHelper.ConvertToWriteableByte(mask);// should not be more than 8 bits

            return card;
        }

        public void UnlockAll()
        {
            if ((Unlocked[0] || Unlocked[1] || Unlocked[2]) == false)
                New = true;

            for (int i = 0; i < 0x03; i++)
            {
                if(Unlocked[i])
                    continue;
                Unlocked[i] = true;
            }
        }
    }
    internal class SaveGame
    {
        private readonly EndianIO IO;
        public List<ushort> MainDeck;
        public List<CardListEntry> UnlockedCards;

        internal SaveGame(EndianIO io)
        {
            if(!io.Opened)
                io.Open();

            IO = io;
        }

        public bool Read()
        {
            if ((IO.In.SeekNReadInt32(0) != 0x58411442))
                throw new YugiohException("invalid header detected for a YuGiOh:MD savegame.");


            var saveData = IO.ToArray();
            var storedSum = IO.In.SeekNReadUInt32(0x0C);
            saveData.WriteInt32(0x0C, 0);
            if (storedSum != SaveChecksum(saveData))
                throw new YugiohException("invalid savegame checksum detected.");

            //if (!ReadMainDeck())
                //return false;

            ReadCardUnlocks();

            return true;
        }

        bool ReadMainDeck()
        {
            MainDeck = new List<ushort>();

            return true;
        }

        void ReadCardUnlocks()
        {
            UnlockedCards = new List<CardListEntry>();
            IO.SeekTo(0x00002200);
            for (int i = 0; i < (0x17C6); i++)
            {
                UnlockedCards.Add(new CardListEntry(IO.In.ReadByte()));
            }
        }

        uint SaveChecksum(byte[] data)
        {
            return Checksum.CRC32.CalculateAlt(data);
        }

        public void Save()
        {
            // write the card list table
            IO.SeekTo(0x00002200);
            foreach (var card in UnlockedCards)
            {
                IO.Out.Write(card.ToArray());
            }
            // generate and write the new checksum
            IO.Out.SeekNWrite(0x0C, 0);
            var saveData = IO.ToArray();
            IO.Out.SeekNWrite(0x0C, SaveChecksum(saveData));
        }
    }
}
