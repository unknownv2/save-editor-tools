using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ubisoft
{
    internal class ACIISaveGame
    {
        private EndianIO IO;
        internal ObjectDeserializer ObjectManager;

        internal ACIISaveGame(EndianIO io)
        {
            IO = io;
            Read();
        }
        private void Read()
        {
            IO.Position = 0;
            IO.EndianType = EndianType.BigEndian;

            // read main segment
            if ((AssassinsCreedGameVerison)IO.In.ReadInt32() != AssassinsCreedGameVerison.II || IO.In.ReadInt32() != 0x00FEDBAC)
                throw new ACException("invalid save main segment header detected.");

            ObjectManager = new ObjectDeserializer(IO);
        }

        internal void Save()
        {
            ObjectManager.Save();
        }

        internal class ObjectDeserializer
        {
            internal long MoneyPosition = -1;
            public int Money = -1;
            internal string PlayerName;

            EndianIO IO;

            public ObjectDeserializer(EndianIO io)
            {
                IO = io;

                IO.EndianType = EndianType.LittleEndian;
                //IO.Position = 0;

                Read();
            }

            internal void Read()
            {
                //read main save header
                uint Type;

                var NbClassVersionsInfo = IO.In.ReadByte();
                for (int i = 0; i < NbClassVersionsInfo; i++)
                {
                    var VersionClassID = IO.In.ReadUInt32();
                    var Version = IO.In.ReadInt16();
                }
                var ObjectName = IO.In.ReadUInt32();
                var ObjectID = IO.In.ReadUInt32();
                var InstancingMode = IO.In.ReadByte();
                if (InstancingMode == 0x01)
                {
                    var FatherID = IO.In.ReadUInt32();
                }

                Type = IO.In.ReadUInt32();
                if (Type == 0) throw new Exception("Assassin's Creed II: invalid save data detected!");

                var totalObjectSize = IO.In.ReadInt32();
                var relativeObjectSize = IO.In.ReadInt32();
                if (totalObjectSize > 0)
                {
                    switch (Type)
                    {
                        case 0xBDBE3B52: // SaveGame
                            ReadSaveGameObject(IO.In);
                            break;
                    }
                }
            }
            internal void Save()
            {
                IO.SeekTo(MoneyPosition);
                IO.Out.Write(Money);
            }
            private void ReadSaveGameObject(EndianReader reader)
            {
                var gameObject = new ManagedObject();

                // 1-4 = Int32
                // 5 = Boolean
                // 7 = String
                for (int x = 0; x < 10; x++)
                    gameObject = new ManagedObject(reader);

                PlayerName = ObjectReader.ReadAsciiString(new EndianReader(gameObject.Value, EndianType.LittleEndian));

                for (int x = 0; x < 5; x++)
                    gameObject = new ManagedObject(reader);

                var count = LoadObjectType1(reader);
                for (int i = 0; i < count; i++)
                {
                    // 826F4368
                }

                count = LoadObjectType1(reader);
                for (int i = 0; i < count; i++)
                {
                    var PtrStatus = reader.ReadByte();
                    var NbClassVersionsInfo = IO.In.ReadByte();
                    for (int j = 0; j < NbClassVersionsInfo; j++)
                    {
                        var VersionClassID = IO.In.ReadUInt32();
                        var Version = IO.In.ReadInt16();
                    }
                    var ObjectName = IO.In.ReadUInt32();
                    var ObjectID = IO.In.ReadUInt32();
                    var InstancingMode = IO.In.ReadByte();
                    if (InstancingMode == 0x01)
                    {
                        var FatherID = IO.In.ReadUInt32();
                    }

                    var Type = IO.In.ReadUInt32();
                    if (Type <= 0) continue;

                    var totalObjectSize = IO.In.ReadInt32();
                    var relativeObjectSize = IO.In.ReadInt32();
                    if (Type == 0x94D6F8F1)
                        ReadAssassinSaveGameDataObject();
                }
            }

            private void ReadAssassinSaveGameDataObject()
            {
                ManagedObject gameObject;

                for (int i = 0; i < 11; i++)
                {
                    gameObject = new ManagedObject(IO.In);
                }
                gameObject = new ManagedObject(IO.In, true);
                var sbObj = new SGObjectEntry(IO.In);
                ReadPlayerProgressionSaveDataObject();
            }

            private void ReadPlayerProgressionSaveDataObject()
            {
                PPGObjectEntry ppgObject;
                var ct = LoadObjectType1(IO.In);
                for (var x = 0; x < ct; x++)
                {
                    IO.In.ReadInt32();
                }
                ct = LoadObjectType1(IO.In);
                for (var x = 0; x < ct; x++)
                {
                    ppgObject = new PPGObjectEntry(IO.In);
                    if (x == 0)
                    {
                        var pos = IO.In.BaseStream.Position;
                        var gameObject = new ManagedObject(IO.In);
                        gameObject = new ManagedObject(IO.In, true);
                        ReadLogicalInventory(IO.In);
                        IO.In.BaseStream.Position += (pos + (ppgObject.PropertiesLength + 4) - (IO.In.BaseStream.Position));
                    }
                    else
                    {
                        IO.Position += (ppgObject.ObjectLength - 4);
                    }
                }
                ct = LoadObjectType1(IO.In);
                for (var x = 0; x < ct; x++)
                {
                    ppgObject = new PPGObjectEntry(IO.In);
                    IO.Position += (ppgObject.ObjectLength - 4);
                }
                ct = LoadObjectType1(IO.In);
                for (var x = 0; x < ct; x++)
                {
                    IO.In.ReadInt32();
                }
                for (var i = 0; i < 2; i++)
                {
                    var gameObject = new ManagedObject(IO.In);
                }
                ct = LoadObjectType1(IO.In);
                for (var x = 0; x < ct; x++)
                {
                    ppgObject = new PPGObjectEntry(IO.In);
                    IO.Position += (ppgObject.ObjectLength - 4);
                }
            }
            private void ReadLogicalInventory(EndianReader reader)
            {
                var sbObj = new SGObjectEntry(reader);
                var objCount = LoadObjectType1(reader);
                for (int x = 0; x < objCount; x++)
                {
                    var ppgObject = new PPGObjectEntry(reader);
                    switch (ppgObject.Type)
                    {
                        case 0x8CE3886:
                            InventoryRechargeableContainer(reader, ppgObject.ObjectLength);
                            break;
                    }
                }

            }
            private void InventoryRechargeableContainer(EndianReader reader, int length)
            {
                var pos = reader.BaseStream.Position;

                // InventoryItemContainer
                var gameObject = new ManagedObject(reader);
                gameObject = new ManagedObject(reader);
                gameObject = new ManagedObject(reader);

                if (MoneyPosition == -1)
                {
                    MoneyPosition = reader.BaseStream.Position - 4;
                    Money = gameObject.Value.ToInt32(false);
                }

                reader.BaseStream.Position += (length - (pos + 4));

            }
            private int LoadObjectType1(EndianReader reader)
            {
                var gameObject = new ManagedObject(reader, true);

                byte proc = reader.ReadByte();
                int size = 0;
                switch (proc)
                {
                    case 1:
                        size = reader.ReadInt32();
                        break;
                }

                return size & 0x3FFF;
            }
        }
    }
}
