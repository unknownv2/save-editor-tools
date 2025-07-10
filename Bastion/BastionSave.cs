using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bastion
{
    class BastionSave
    {
        private uint checksum; // Adler32 Checksum
        byte[] saveData; // The save data
        BinaryReader save; // The save data io
        public Location[] locations; // The array of locations
        public Flags[] flags; // Set flags
        public CounterList[] counters; // Save counters
        public List<string> storedWeapons; // Player's stored weapons
        public CurrentWeapon[] currentWeapons; // The player's current weapons

        public void LoadSave(EndianIO io)
        {
            // Seek to the start of the file
            io.Stream.Position = 0;

            // Read the checksum
            checksum = io.In.ReadUInt32();

            // Read the rest of the save data into saveData
            saveData = io.In.ReadBytes((int)io.Stream.Length - 4);

            // Open the save IO
            save = new BinaryReader(new MemoryStream(saveData)); // Using binary reader here because the save is in little endian

            // Check if the save is corrupt.  This is a weird check
            if (save.ReadInt32() != 2)
                throw new Exception("Invalid save magic!");

            // Load the locations into an array
            int locationCount = save.ReadInt32();
            locations = new Location[locationCount];
            for (int i = 0; i < locationCount; i++)
            {
                locations[i] = LoadLocation();
            }

            // Load flags into an array
            int flagCount = save.ReadInt32();
            flags = new Flags[flagCount];
            for (int i = 0; i < flagCount; i++)
            {
                flags[i].flag = save.ReadString();
                //string flagValue = save.ReadString();
                int flagCount2 = save.ReadInt32();
                flags[i].names = new string[flagCount2];
                //if(i == 0)
                //    save.Stream.Position += 1;
                for (int x = 0; x < flagCount2; x++)
                {
                    flags[i].names[x] = save.ReadString();
                    /*
                    Flag f = new Flag();
                    f.flag = flagValue;
                    f.name = save.ReadString();
                    flagList.Add(f);
                     */
                }
            }
            //flags = flagList.ToArray();

            // Load the counters
            int counterListCount = save.ReadInt32();
            counters = new CounterList[counterListCount];
            for (int i = 0; i < counterListCount; i++)
            {
                counters[i].name = save.ReadString();
                int counterCount = save.ReadInt32();
                counters[i].counters = new Counter[counterCount];
                for (int x = 0; x < counterCount; x++)
                {
                    counters[i].counters[x].name = save.ReadString();
                    counters[i].counters[x].value = save.ReadInt32();
                    counters[i].counters[x].fileTime = save.ReadUInt64();
                }
            }

            // Load the player's stored weapons
            int storedWeaponCount = save.ReadInt32();
            storedWeapons = new List<string>();
            for (int i = 0; i < storedWeaponCount; i++)
            {
                storedWeapons.Add(save.ReadString());
            }

            // Load the player's active weapons
            int currentWeaponCount = save.ReadInt32();
            currentWeapons = new CurrentWeapon[currentWeaponCount];
            for (int i = 0; i < currentWeaponCount; i++)
            {
                currentWeapons[i].weapon = save.ReadString();
                currentWeapons[i].slot = save.ReadString();
            }
        }

        public int GetCounterValue(string counterList, string variable)
        {
            foreach (CounterList list in counters)
            {
                if (list.name == counterList)
                {
                    foreach (Counter count in list.counters)
                    {
                        if (count.name == variable)
                            return count.value;
                    }
                }
            }
            throw new Exception(counterList + "." + variable + " Not Found!");
        }

        public void SetCounterValue(string counterList, string variable, int value)
        {
            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i].name == counterList)
                {
                    for (int x = 0; x < counters[i].counters.Length; x++)
                    {
                        if (counters[i].counters[x].name == variable)
                        {
                            counters[i].counters[x].value = value;
                            return;
                        }
                    }
                }
            }
            throw new Exception(counterList + "." + variable + " Not Found!");
        }

        public Location LoadLocation()
        {
            Location loc = new Location();
            
            // Read in the location data
            loc.name = save.ReadString();
            loc.unk1 = save.ReadUInt32();
            loc.mapName = save.ReadString();
            loc.timeStamp = save.ReadUInt64();
            loc.activated = new List<int>();
            int activatedCount = save.ReadInt32();
            for (int i = 0; i < activatedCount; i++)
            {
                loc.activated.Add(save.ReadInt32());
            }
            loc.killed = new List<int>();
            int killedCount = save.ReadInt32();
            for (int i = 0; i < killedCount; i++)
            {
                loc.killed.Add(save.ReadInt32());
            }
            loc.exploredTiles = new List<int>();
            int exploredTileCount = save.ReadInt32();
            for (int i = 0; i < exploredTileCount; i++)
            {
                loc.exploredTiles.Add(save.ReadInt32());
            }
            loc.plotIds = new List<int>();
            int plotIdCount = save.ReadInt32();
            for (int i = 0; i < plotIdCount; i++)
            {
                loc.plotIds.Add(save.ReadInt32());
            }
            loc.plantNames = new List<string>();
            int plantNameCount = save.ReadInt32();
            for (int i = 0; i < plantNameCount; i++)
            {
                loc.plantNames.Add(save.ReadString());
            }
            int loopCount = save.ReadInt32();
            loc.unk3 = new List<int>();
            loc.fileTimes = new List<ulong>();
            for (int i = 0; i < loopCount; i++)
            {
                loc.unk3.Add(save.ReadInt32());
                loc.fileTimes.Add(save.ReadUInt64());
            }

            return loc;
        }

        public void WriteSave(EndianIO io)
        {
            // Create a binary writer for bw
            BinaryWriter bw = new BinaryWriter(new MemoryStream(saveData));
            
            // Write the save magic
            bw.Write((int)2);
            bw.Write(locations.Length);

            // Write the locations
            foreach (Location loc in locations)
            {
                bw.Write(loc.name);
                bw.Write(loc.unk1);
                bw.Write(loc.mapName);
                bw.Write(loc.timeStamp);
                bw.Write(loc.activated.Count);

                foreach (int activated in loc.activated)
                    bw.Write(activated);

                bw.Write(loc.killed.Count);
                foreach (int killed in loc.killed)
                    bw.Write(killed);

                bw.Write(loc.exploredTiles.Count);
                foreach (int explored in loc.exploredTiles)
                    bw.Write(explored);

                bw.Write(loc.plotIds.Count);
                foreach (int plotId in loc.plotIds)
                    bw.Write(plotId);

                bw.Write(loc.plantNames.Count);
                foreach (string plantName in loc.plantNames)
                    bw.Write(plantName);

                bw.Write(loc.unk3.Count);
                for (int i = 0; i < loc.unk3.Count; i++)
                {
                    bw.Write(loc.unk3[i]);
                    bw.Write(loc.fileTimes[i]);
                }

                
            }
            // Write the flags
            bw.Write(flags.Length);
            foreach (Flags flag in flags)
            {
                bw.Write(flag.flag);
                bw.Write(flag.names.Length);
                foreach (string name in flag.names)
                    bw.Write(name);
            }

            // Write the counters
            bw.Write(counters.Length);

            foreach (CounterList list in counters)
            {
                bw.Write(list.name);
                bw.Write(list.counters.Length);
                foreach (Counter counter in list.counters)
                {
                    bw.Write(counter.name);
                    bw.Write(counter.value);
                    bw.Write(counter.fileTime);
                }
            }

            // Write stored weapons
            bw.Write(storedWeapons.Count);

            foreach (string weapon in storedWeapons)
                bw.Write(weapon);

            // Write active weapons
            bw.Write(currentWeapons.Length);
            foreach (CurrentWeapon weapon in currentWeapons)
            {
                bw.Write(weapon.weapon);
                bw.Write(weapon.slot);
            }

            bw.Close();

            checksum = Adler32(saveData);

            io.Stream.Position = 0;
            io.Out.Write(checksum);
            io.Out.Write(saveData);
        }

        public uint Adler32(byte[] data)
        {
            uint a = 1;
            uint b = 0;
            
            for (int i = 0; i < data.Length; i++)
            {
                a = (a + (uint)data[i]) % 65521;
                b = (b + a) % 65521;
            }

            return (b << 16) | a;
        }

        public struct Location
        {
            public uint unk1;
            public string name;
            public string mapName;
            public ulong timeStamp;
            public List<int> activated;
            public List<int> killed;
            public List<int> exploredTiles;
            public List<int> plotIds;
            public List<string> plantNames;
            public List<int> unk3;
            public List<ulong> fileTimes;
        }

        public struct Flags
        {
            public string flag;
            public string[] names;
        }

        public struct CounterList
        {
            public string name;
            public Counter[] counters;
        }

        public struct Counter
        {
            public string name;
            public int value;
            public ulong fileTime;
        }

        public struct CurrentWeapon
        {
            public string weapon;
            public string slot;
        }
    }
}
