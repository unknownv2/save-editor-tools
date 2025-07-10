using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Joe_Danger_SE
{
    class Save
    {
        EndianIO io;
        public List<string> tours;
        public Level[] levels;

        public struct Level
        {
            public uint flags;
            public int medal;
            public long score;
            public Score[] highScores;
        }

        public struct Score
        {
            public string name;
            public long score;
        }

        public Save(EndianIO io)
        {
            this.io = io;
            levels = new Level[0x50];
        }

        public void LoadSave()
        {
            // Seek to the tours
            io.SeekTo(0xB0);
            tours = new List<string>();
            string nextStr = io.In.ReadNullTerminatedString();
            
            while (nextStr != string.Empty)
            {
                tours.Add(nextStr);
                io.Stream.Position += 0x1F - nextStr.Length;
                nextStr = io.In.ReadNullTerminatedString();
            }

            // Seek to the start of the levels
            io.SeekTo(0x8C0);

            // Loop through and read the levels
            for (int i = 0; i < 0x50; i++)
            {
                // Read in the flags
                levels[i].flags = io.In.ReadUInt32();
                
                // Seek past the null data
                io.Stream.Position += 0x40;

                // Read in the medal
                levels[i].medal = io.In.ReadInt32();

                // Read in the score
                levels[i].score = io.In.ReadInt64();

                // Seek to the scores
                io.Stream.Position += 0x20;

                // Create the high score array
                levels[i].highScores = new Score[10];

                // Read the scores
                for (int x = 0; x < 10; x++)
                {
                    levels[i].highScores[x].score = io.In.ReadInt64();
                }

                // Loop through and read the names
                for (int x = 0; x < 10; x++)
                {
                    levels[i].highScores[x].name = io.In.ReadNullTerminatedString();
                    io.Stream.Position += 0x80 - (levels[i].highScores[x].name.Length + 1);
                }

                io.Stream.Position += 0x80;
            }
        }

        public void WriteSave()
        {
            io.SeekTo(0xB0);

            foreach (string tour in tours)
                io.Out.WriteAsciiString(tour, 0x20);

            io.SeekTo(0x8C0);
            
            // Loop through and write the levels
            for (int i = 0; i < 0x50; i++)
            {
                //Dictionary<string, long> oldDict = levels[i].highScores;
                //levels[i].highScores = (from entry in oldDict orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
                

                io.Out.Write(levels[i].flags);

                io.Stream.Position += 0x40;

                io.Out.Write(levels[i].medal);
                io.Out.Write(levels[i].score);

                io.Stream.Position += 0x20;

                foreach (Score item in levels[i].highScores)
                    io.Out.Write(item.score);

                foreach (Score item in levels[i].highScores)
                {
                    io.Out.Write(item.name);
                    io.Stream.Position += (0x80 - item.name.Length);
                }

                io.Stream.Position += 0x80;
            }
        }
    }
}
