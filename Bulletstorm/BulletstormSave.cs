using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bulletstorm
{
    class Save
    {
        public int playerOffset;
        public int skillPoints;
        private int skillPointsOffset;

        public void LoadSave(EndianIO io)
        {
            // Open the IO
            io.Open();

            // Seek to and read the level name length
            io.SeekTo(0x8);
            int levelNameLength = io.In.ReadInt32();
            
            // Seek forward to the start of the checkpoint list
            io.Stream.Position += levelNameLength + 0x1f;

            // Loop past the checkpoint list
            int checkpointCount = io.In.ReadInt32();

            for (int i = 0; i < checkpointCount; i++)
            {
                int checkpointNameLen = io.In.ReadInt32();
                io.Stream.Position += checkpointNameLen + 2;
            }

            // Record the position of the player data
            playerOffset = (int)io.Stream.Position;

            // Seek forward to the skill points
            io.Stream.Position += 0x97;
            int strLen1 = io.In.ReadInt32();
            io.Stream.Position += 0x33 + strLen1;
            
            // Read in the skill points
            skillPointsOffset = (int)io.Stream.Position;
            skillPoints = io.In.ReadInt32();
        }

        public void WriteSave(EndianIO io)
        {
            io.Stream.Position = skillPointsOffset;
            io.Out.Write(skillPoints);
        }
    }
}
