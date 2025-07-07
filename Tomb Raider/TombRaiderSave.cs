using System.Collections.Generic;
using System.IO;

namespace TombRaider
{
    public class TombRaiderSave
    {
        public EndianIO IO;
        public Dictionary<uint, int> PlayerItems;
        public int SkillPoints;

        public TombRaiderSave(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            PlayerItems = new Dictionary<uint, int>();

            IO.SeekTo(0x2678);
            var invCount = IO.In.ReadInt32();
            for (var i = 0; i < invCount; i++)
            {
                PlayerItems.Add(IO.In.ReadUInt32(), IO.In.ReadInt16());
                IO.In.BaseStream.Position += 2;
            }
            SkillPoints = IO.In.SeekNReadInt32(0x288C);
        }

        public void Save()
        {
            IO.SeekTo(0x2678);
            IO.Out.Write(PlayerItems.Count);
            foreach (var playerItem in PlayerItems)
            {
                IO.Out.Write(playerItem.Key);
                IO.Out.Write((short)(playerItem.Value & 0xFFFF));
                IO.Position += 2;
            }
            IO.Out.SeekNWrite(0x288C, SkillPoints);
        }
    }
}