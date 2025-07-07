using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DevilMayCry4
{
    class Save
    {
        public Slot[] SaveSlots;

        public struct Slot
        {
            public int RedOrbs;
            public int Orbs;
            public int Level;
            public int Score;
        }

        public void LoadSave(EndianIO io)
        {
            io.Open();

            // initialize the save slots
            SaveSlots = new Slot[16];

            // Seek to the save data
            io.Stream.Position = 0x3740;

            // Read in the save info
            for (int i = 0; i < 16; i++)
            {
                // Seek forward and read the red orbs
                io.Stream.Position += 0x10;
                SaveSlots[i].RedOrbs = io.In.ReadInt32();

                // Seek forward and read the orbs
                io.Stream.Position += 0x20;
                SaveSlots[i].Orbs = io.In.ReadInt32();

                // Seek forward and reac the level
                io.Stream.Position += 0x34;
                SaveSlots[i].Level = io.In.ReadInt32();

                // Seek forward and read the score
                io.Stream.Position += 0x73C;
                SaveSlots[i].Score = io.In.ReadInt32();

                // Seek forward to the end of the block
                io.Stream.Position += 0x838;
            }
        }

        public void WriteSave(EndianIO io)
        {
            io.SeekTo(0x3740); // Seek to the save start

            for (int i = 0; i < 16; i++)
            {
                // Seek to and write the 
                io.Stream.Position += 0x10;
                io.Out.Write(SaveSlots[i].RedOrbs);

                // Seek forward and write the orbs
                io.Stream.Position += 0x20;
                io.Out.Write(SaveSlots[i].Orbs);
                io.Out.Write(SaveSlots[i].RedOrbs);

                // Seek forward and write the level
                io.Stream.Position += 0x30;
                io.Out.Write(SaveSlots[i].Level);

                // Seel forward and write the second occurence of the orbs
                io.Stream.Position += 0x64;
                io.Out.Write(SaveSlots[i].Orbs);

                // Seek forward and write the score
                io.Stream.Position += 0x6D4;
                io.Out.Write(SaveSlots[i].Score);

                // Seek forward to the end of the block
                io.Stream.Position += 0x838;
            }
        }
    }
}
