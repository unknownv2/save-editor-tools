using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gears2
{
    class Save
    {
        public string levelName;
        public weapon[] weapons;
        // io;
        //int saveSize;
        int playerDataOffset;
        byte[] playerData;
        byte[] playerDataStart;
        byte[] playerDataEnd;
        byte[] saveEnd;
        int playerDataLen;

        public struct weapon
        {
            public int nameLen;
            public string name;
            public int ammo;
            public int position;
        }

        public void loadSave(ref EndianIO io)
        {
            io.Open();

            // seek to the start of the save, just to be safe
            io.Stream.Position = 4;
            // read level name
            int levelLen = io.In.ReadInt32();
            levelName = io.In.ReadString(levelLen);

            // skip to the start of the checkpoint list
            io.Stream.Position += 0x1e;

            // skip over the checkpoint stuff
            int checkpointCnt = io.In.ReadInt32();

            for (int i = 0; i < checkpointCnt; i++)
            {
                int strLen = io.In.ReadInt32();
                io.Stream.Position += strLen + 2;
            }

            // skip forward a bit
            io.Stream.Position += 4;

            // skip the next 2 string
            for (int i = 0; i < 2; i++)
            {
                int strLen = io.In.ReadInt32();
                io.Stream.Position += strLen;
            }

            // save the player data offset
            playerDataOffset = (int)io.Stream.Position;

            // read the player data length
            playerDataLen = io.In.ReadInt32();
            io.Stream.Position -= 4;
            // read the player data
            playerData = io.In.ReadBytes(playerDataLen);
            // create an endian io for player data
            EndianIO playerio = new EndianIO(playerData, EndianType.BigEndian);
            playerio.Open();
            playerio.Stream.Position = 0x58;
            int strLen2 = playerio.In.ReadInt32();
            playerio.Stream.Position += strLen2 + 1;
            int strLen3 = playerio.In.ReadInt32();
            playerio.Stream.Position += strLen3 + 4;
            
            //io.Stream.Position += 0x6B;

            // record weapon offset
            //playerDataOffset = (int)io.Stream.Position;
            
            // record the len of the first block
            int playerDataBlockLen1 = (int)playerio.Stream.Position;
            // seek back to the start of the player block
            playerio.Stream.Position = 0;
            playerDataStart = playerio.In.ReadBytes(playerDataBlockLen1);

            // read the weapon count
            int weapCount = playerio.In.ReadInt32();
            weapons = new weapon[weapCount];

            // read in the weapon data
            for (int i = 0; i < weapCount; i++)
            {
                weapons[i].nameLen = playerio.In.ReadInt32();
                weapons[i].name = playerio.In.ReadAsciiString(weapons[i].nameLen);
                weapons[i].ammo = playerio.In.ReadInt32();
                playerio.Stream.Position += 1;
                weapons[i].position = playerio.In.ReadInt32();
                playerio.Stream.Position += 1;
            }

            // read the rest of the data
            playerDataEnd = playerio.In.ReadBytes(playerio.Stream.Length - playerio.Stream.Position);
            playerio.Close();

            // read the rest of the save
            saveEnd = io.In.ReadBytes(io.Stream.Length - io.Stream.Position);
        }

        private void rebuildPlayerData()
        {
            int size = 0;
            size += playerDataStart.Length + 4; // add the size for the start and the weapon count
            
            // calculate the size 
            for (int i = 0; i < weapons.Length; i++)
            {
                size += 4; // add 4 for the name len
                size += weapons[i].nameLen; // add the name length
                size += 10; // add 10 for the name and position
            }

            // add the end part length
            size += playerDataEnd.Length;

            // set the player data one to the one we just calculated
            playerDataLen = size;

            playerData = new byte[size];

            // rewrite the player data
            EndianIO playerio = new EndianIO(playerData, EndianType.BigEndian);
            playerio.Open();

            playerio.Out.Write(playerDataStart);
            playerio.Out.Write(weapons.Length);

            // write the weapons
            for (int i = 0; i < weapons.Length; i++)
            {
                playerio.Out.Write(weapons[i].nameLen);
                playerio.Out.WriteAsciiString(weapons[i].name, weapons[i].nameLen);
                playerio.Out.Write(weapons[i].ammo);
                playerio.Stream.Position += 1;
                playerio.Out.Write(weapons[i].position);
                playerio.Stream.Position += 1;
            }

            // write player data end
            playerio.Out.Write(playerDataEnd);

            // write the player size
            playerio.Stream.Position = 0;
            playerio.Out.Write((int)playerio.Stream.Length);

            playerio.Close();
        }

        public EndianIO writeSave(ref EndianIO io)
        {
            rebuildPlayerData();
            io.Stream.SetLength(playerDataOffset + playerDataLen + saveEnd.Length);
            io.SeekTo(playerDataOffset);
            io.Out.Write(playerData);
            io.Out.Write(saveEnd);
            io.Stream.Position = 0;
            return io;
        }

    }
}
