using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NarutoUNS3
{
    public class NarutoUNS3Save
    {
        private EndianIO IO;

        public int AcquiredRyo { get; set; }
        public int Ryo { get; set; }

        public int HeroPoints { get; set; }
        public int LegendPoints { get; set; }

        public CollectionManager CollectionManager;

        public NarutoUNS3Save(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            AcquiredRyo = IO.In.SeekNReadInt32(0x130);
            Ryo = IO.In.SeekNReadInt32(0x1C818);
            IO.SeekTo(0x00019E94);
            HeroPoints = IO.In.ReadInt32();
            LegendPoints = IO.In.ReadInt32();

            CollectionManager = new CollectionManager(IO);
        }

        public void Save()
        {
            IO.Out.SeekNWrite(0x130, AcquiredRyo);
            IO.Out.SeekNWrite(0x1C818, Ryo);
            IO.Out.SeekTo(0x19E94);
            IO.Out.Write(HeroPoints);
            IO.Out.Write(LegendPoints);
        }
    }

    public class ItemManager
    {
        
    }

    public enum CollectionItemType
    {
        NinjaInfoCard,
        Title,
        SubstitutionItem,
        Audio,
        Music,
        UltimateJutsu
    }

    public class CollectionManager
    {
        private const int CollectionLength = 0x80;
        private EndianIO IO;

        public CollectionManager(EndianIO io)
        {
            IO = io;
        }

        public void UnlockAll(CollectionItemType type)
        {
            var unlockOffset = (((int) type*0x80) + 4);
            Unlock(unlockOffset, unlockOffset + 0x300);
        }

        private void Unlock(int unlockOffset, int newOffset)
        {
            for (var i = 0; i < CollectionLength; i++)
            {
                IO.SeekTo(0x0001AF64 + unlockOffset + (i >> 3));
                IO.Out.WriteByte(1 << (i & 7));
                IO.SeekTo(0x0001AF64 + newOffset + (i >> 3));
                IO.Out.WriteByte(1 << (i & 7));
            }
        }
    }
}