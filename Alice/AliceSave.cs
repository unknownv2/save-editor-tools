using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Alice
{
    internal class AliceSave
    {
        private EndianIO IO;
        internal AliceSave(EndianIO io)
        {
            IO = io;
            Read();
        }

        internal string LevelName;
        internal Level[] Levels;
        internal int Teeth;

        private void Read()
        {
            IO.Position = 0x08;

            Levels = new Level[IO.In.ReadInt32()];

            LevelName = IO.In.ReadString(IO.In.ReadInt32());

            IO.Position += 0x04;

            for (int x = 0; x < Levels.Length; x++)
            {
                Levels[x] = new Level();

                int levelLength = IO.In.ReadInt32();

                if (levelLength == 1)
                    levelLength = IO.In.ReadInt32();

                Levels[x].Name = IO.In.ReadString(levelLength);

                Levels[x].Collectables = new List<Collectable>(IO.In.ReadInt32());

                for (int i = 0; i < Levels[x].Collectables.Capacity; i++)
                {
                    Collectable item = new Collectable();
                    item.Name = IO.In.ReadString(IO.In.ReadInt32());
                    while (true)
                    {
                        int attrib = IO.In.ReadInt32();
                        if (attrib == -1)
                            break;
                        item.Attributes.Add(attrib);
                    }
                    Levels[x].Collectables.Add(item);
                }
            }

            IO.Position += 0x13C;

            Teeth = IO.In.ReadInt32();
        }

        internal void Save()
        {
            IO.Position = 0x08;

            IO.Out.Write(Levels.Length);

            int levelLength = LevelName.Length + 1;
            IO.Out.Write(levelLength);
            IO.Out.WriteAsciiString(LevelName, levelLength);

            IO.Position += 0x04;

            for (int x = 0; x < Levels.Length; x++)
            {
                levelLength = Levels[x].Name.Length + 1;

                if (Levels[x].Name == LevelName)
                    IO.Out.Write(1);

                IO.Out.Write(levelLength);
                IO.Out.WriteAsciiString(Levels[x].Name, levelLength);

                IO.Out.Write(Levels[x].Collectables.Count);

                foreach (Collectable item in Levels[x].Collectables)
                {
                    levelLength = item.Name.Length + 1;
                    IO.Out.Write(levelLength);
                    IO.Out.WriteAsciiString(item.Name, levelLength);
                    for (int i = 0; i < item.Attributes.Count; i++)
                        IO.Out.Write(item.Attributes[i]);
                    IO.Out.Write(-1);
                }
            }

            IO.Position += 0x13C;

            IO.Out.Write(Teeth);
        }

        internal enum CollectableType : int
        {
            Memory,
            Secret_Pickup
        }

        internal class Level
        {
            internal string Name;
            internal List<Collectable> Collectables;
        }

        internal class Collectable
        {
            internal string Name;
            internal List<int> Attributes = new List<int>();

            internal string[] NameParts
            {
                get
                {
                    return Name.Split('_');
                }
                set
                {
                    StringBuilder str = new StringBuilder();
                    foreach (string part in value)
                        str.Append("_" + part);
                    Name = str.Remove(0, 1).ToString();
                }
            }
        }
    }
}
