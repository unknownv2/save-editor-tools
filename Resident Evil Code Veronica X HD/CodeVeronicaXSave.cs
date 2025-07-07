using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Capcom
{
    internal class CodeVeronicaXData
    {
        internal static List<string> ItemList = new List<string>
        {
            "Empty",
            "Rocket Launcher",
            "Assault Rifle",
            "Sniper Rifle",
            "Shotgun",
            "Handgun",
            "Grenade Launcher",
            "Bow Gun",
            "Combat Knife",
            "M93R Handgun",
            "M93R Fully-Automatic",
            "Linear Launcher",
            "Hangun Bullets",
            "Magnum Bullets",
            "Shotgun Shells",
            "Grenade Rounds",
            "Acid Rounds",
            "Flame Rounds",
            "Bow Gun Arrows",
            "M93R Part",
            "First Aid Spray",
            "Green Herb",
            "Red Herb",
            "Blue Herb",
            "Green+Green Herb Mix",
            "Red+Green Herb Mix",
            "Blue+Green Herb Mix",
            "Green+Blue Herb Mix",
            "Green+Green+Green Herb Mix",
            "Green+Blue+Red Herb Mix",
            "Magnum Bullets",
            "Ink Ribbon",
            "Magnum",
            "Gold Luggers",
            "Sub Machine Gun",
            "Bow Gun Powder",
            "Gun Power Arrow",
            "BOW Gas Rounds",
            "Machine Gun Bullets",
            "Gas Mask",
            "Rifle Bullets",
            "Duralumin Case 1",
            "Assault Rifle Bullets",
            "Alexander's Pierce",
            "Alexander's Jewel",
            "Alfred's Ring",
            "Alfred's Jewel",
            "Prisoner's Diary (Placeholder)",
            "Director's Memo",
            "Instructions",
            "Lockpick",
            "Glass Eye",
            "Piano Roll",
            "Steering Wheel",
            "Crane Key",
            "Lighter",
            "Eeagle Plate",
            "Side Pack",
            "Map (Placeholder)",
            "Hawk Emblem",
            "Queen Ant Object",
            "King Ant Object",
            "Biohazard Card",
            "Duralumin Case 2",
            "Detonator",
            "Control Lever",
            "Gold Dragonfly",
            "Silver Key",
            "Gold Key",
            "Army Proof",
            "Navy Proof",
            "Air Force Proof",
            "Key Tag",
            "ID Card",
            "Map (Placeholder)",
            "Airport Key",
            "Emblem Card",
            "Skeleton Picture",
            "Music Box Plate",
            "Dragonfly Object",
            "Album",
            "Halberd",
            "Extinguisher",
            "Briefcase",
            "Padlock Key",
            "TG-01",
            "Sp. Alloy Emblem 1",
            "Valve Handle",
            "Octa Valve Handle",
            "Machine Room Key",
            "Mining Room Key",
            "Bar Code Sticker",
            "Sterile Room Key",
            "Door Knob",
            "Battery Pack",
            "Hemostatic",
            "Turn Table Key",
            "Chemical Storage Key",
            "Clement Alpha",
            "Clement Sigma",
            "Tank Object",
            "Sp. Alloy Emblem 2",
            "Alfred's Memo (Placeholder)",
            "Rusted Sword",
            "Hemostatic 2",
            "Security Card 2",
            "Security File",
            "Queent Ant Relief",
            "Alexia's Jewel",
            "Queen Ant Relief (Placeholder)",
            "King Ant Relief (Placeholder)",
            "Red Jewel",
            "Blue Jewel",
            "Socket",
            "Square Valve Handle",
            "Serum",
            "Earthenware Vase",
            "Paperweight",
            "Silver Dragonfly (Now Wings)",
            "Silver Dragonfly (With Wings)",
            "Dragonfly Wing Object",
            "Crystal",
            "Gold Dragonfly (1 Wing)",
            "Gold Dragonfly (2 Wings)",
            "God Dragonfly (3 Wings)",
            "File",
            "Plant Pot",
            "Picture B (Placeholder)",
            "Duralumin Case 3",
            "Duralumin Case 4",
            "Bow Gun Powder",
            "Enhanced Handgun",
            "Memo (Placeholder)",
            "Board Clip",
            "Card (Placeholder)",
            "Newspaper Clip (Placeholder)",
            "Luger Replica",
            "Queen Ant Relief (Placeholder)",
            "Family Picture",
            "File (Placeholder)",
            "Remote Controller (Placeholder)",
            "None (?)",
            "Calico Bullets",
            "Clement Mixture",
            "Playing Manual"

        };
    }
    internal enum CodeVeronicaXEditorTypes
    {
        None,
        Item,
        ItemList,
        ItemBox
    }

    internal enum CodeVeronicaXCharacters
    {
        Claire,
        Chris,
        Steve,
        Wesker
    }
    internal class CodeVeronicaXItemSlot
    {
        internal bool IsInfinite;
        internal byte ItemId;
        internal ushort ItemCount;

        internal CodeVeronicaXItemSlot(uint item)
        {
            IsInfinite = (item & 0x8000000) != 0;
            ItemId = (byte) ((item >> 0x10) & 0xFF);
            ItemCount = (ushort) (item & 0xFFFF);
        }

        internal void Write(EndianWriter writer)
        {
            int item = 0;
            item |= IsInfinite ? 0x8000000 : 0x00;
            item |= (ItemId << 0x10);
            item |= (ItemCount & 0xFFFF);
            writer.Write(item);
        }
    }
    internal class CodeVeronicaXCharacterInventory
    {
        internal bool IsEmpty;
        internal int Unknown1;
        internal ushort EquippedWeapon, Unknown2;

        internal List<CodeVeronicaXItemSlot> Items = new List<CodeVeronicaXItemSlot>();

        internal CodeVeronicaXCharacterInventory(EndianIO io)
        {
            Unknown1 = io.In.ReadInt32();
            EquippedWeapon = io.In.ReadUInt16();
            Unknown2 = io.In.ReadUInt16();
            for (int i = 0; i < 10; i++)
            {
                Items.Add(new CodeVeronicaXItemSlot(io.In.ReadUInt32()));
            }
        }

        internal byte[] Write()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            io.Out.Write(Unknown1);
            io.Out.Write(EquippedWeapon);
            io.Out.Write(Unknown2);

            foreach (var itemSlot in Items)
            {
                itemSlot.Write(io.Out);
            }
            
            return io.ToArray();
        }
    }

    internal class CodeVeronicaXSaveSlot
    {
        internal bool IsEmpty;
        internal int Unknown1;
        internal ushort EquippedWeapon, Unknown2;
        internal short ClaireHealth, ChrisHealth, SteveHealth, WeskerHealth;
        internal CodeVeronicaXCharacters CurrentCharacter;

        internal List<CodeVeronicaXCharacterInventory> CharacterInventories = new List<CodeVeronicaXCharacterInventory>(3);

        internal List<CodeVeronicaXItemSlot> ItemBox = new List<CodeVeronicaXItemSlot>();

        // so we know if we need to back this slot instead of writing all of them back
        internal bool Modified; 

        //internal uint Health;
        internal CodeVeronicaXSaveSlot(EndianIO io)
        {
            // so we know what to display/read and write
            IsEmpty = io.In.ReadInt32() == 0x00;
            io.Position += 4;
            CurrentCharacter = (CodeVeronicaXCharacters)io.In.ReadByte();
            // read character inventories (Claire, Chris, and Steve)
            io.Position += 0x1EB;
            for (int i = 0; i < 4; i++)
            {
                CharacterInventories.Add(new CodeVeronicaXCharacterInventory(io));
                io.Position += 0x10;
            }
            // read the inventory in the Item Box
            //io.Position += 0x40;
            for (int i = 0; i < 128; i++)
            {
                ItemBox.Add(new CodeVeronicaXItemSlot(io.In.ReadUInt32()));
            }
            io.Position += 0x324;

            ClaireHealth = io.In.ReadInt16();
            ChrisHealth = io.In.ReadInt16();
            SteveHealth = io.In.ReadInt16();
            WeskerHealth = io.In.ReadInt16();
        }

        internal void Write(EndianIO io)
        {
            io.Position += 8;
            io.Out.Write((byte)CurrentCharacter);

            io.Position += 0x1EB;
            foreach (var inventory in CharacterInventories)
            {
                io.Out.Write(inventory.Write());
                io.Position += 0x10;
            }
            //io.Position += 0x40;
            foreach (var itemSlot in ItemBox)
            {
                itemSlot.Write(io.Out);
            }
            io.Position += 0x324;
            io.Out.Write(ClaireHealth);
            io.Out.Write(ChrisHealth);
            io.Out.Write(SteveHealth);
            io.Out.Write(WeskerHealth);
        }
    }
    internal class CodeVeronicaXSave
    {
        private readonly EndianIO _io;
        internal List<CodeVeronicaXSaveSlot> SaveSlots;
 
        internal CodeVeronicaXSave(EndianIO io)
        {
            if (io == null)
                throw new Exception("Package file I/O was not found!");

            if (!io.Opened)
                io.Open();

            _io = io;

            SaveSlots = new List<CodeVeronicaXSaveSlot>();

            // parse save data and slot data
            Read();
        }

        private void Read()
        {
            // save slots start at 0x08 [Filled slot has header flag 0x0A]
            
            for (int i = 0; i < 15; i++)
            {
                _io.SeekTo(0x08 + (i * 0x838));
                SaveSlots.Add(new CodeVeronicaXSaveSlot(_io));
            }
        }

        internal void Save()
        {
            for (int i = 0; i < 15; i++)
            {
                _io.SeekTo(0x08 + (i * 0x838));
                if(!SaveSlots[i].IsEmpty && SaveSlots[i].Modified )
                {
                    SaveSlots[i].Write(_io);
                }
            }
        }
    }
}
