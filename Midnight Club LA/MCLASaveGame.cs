using System;
using System.Collections.Generic;
using System.IO;


namespace Rockstar
{
    internal struct MCLASaveEntry
    {
        internal string EntryStringID;
        internal byte BuildVersion;
        internal uint DataSize;
        internal long Position;
    }

    internal class MCLACareer
    {
        private readonly EndianIO _io;
        internal int Money;

        internal MCLACareer(EndianIO io)
        {
            _io = io;
            byte[] unknown = io.In.ReadBytes(4);
            if(io.In.ReadByte() != 0x0B)
                throw new Exception("Career version has changed. Ignoring load and starting a rage_new career");

            uint ct = io.In.ReadUInt32();
            io.Position += (ct*0x30);
            Money = io.In.ReadInt32();
        }
    }

    internal class MCLASaveGame
    {
        private readonly EndianIO _io;

        internal List<MCLASaveEntry> Entries; 
        internal int GameVersion;
        internal int EntryCount;
        internal MCLACareer Career;

        internal MCLASaveGame(EndianIO io)
        {
            _io = io;
            Read();
        }

        internal void Save()
        {
            
        }

        internal void Read()
        {
            GameVersion = _io.In.ReadInt32();
            EntryCount = _io.In.ReadInt32();
            Entries = new List<MCLASaveEntry>();

            for (int i = 0; i < EntryCount; i++)
            {
                MCLASaveEntry saveEntry;
                saveEntry.EntryStringID = _io.In.ReadAsciiString(_io.In.ReadInt32()); // we need 'Career'
                saveEntry.BuildVersion = _io.In.ReadByte();
                saveEntry.DataSize = _io.In.ReadUInt32();
                saveEntry.Position = _io.Position;
                _io.Position += saveEntry.DataSize;
                Entries.Add(saveEntry);
            }
            _io.Position = Entries.Find(t => t.EntryStringID == "Career").Position;
            Career = new MCLACareer(_io);
        }
    }

}
