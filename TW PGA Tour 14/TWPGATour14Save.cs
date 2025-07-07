using System;
using System.IO;
using ElectronicArts;

namespace TigerWoods
{
    public class PGATour14Save
    {
        private readonly EndianIO _io;
        private EA _eaHeader;

        public string Golfer { get; set; }
        public string Profile { get; set; }
        public int Experience { get; set; }
        public int AttributePoints { get; set; }

        public int Power { get; set; }
        public int Accuracy { get; set; }
        public int Workability { get; set; }
        public int Spin { get; set; }
        public int Recovery { get; set; }
        public int Putting { get; set; }

        public PGATour14Save(EndianIO io)
        {
            _io = io;
            Read();
        }

        private void Read()
        {
            // load the default EA game header
            _eaHeader = new EA(_io);
            _io.SeekTo(0x20);
            var golfer = _io.In.ReadAsciiString(0x20);

            _io.SeekTo(0x48);
            Golfer = _io.In.ReadAsciiString(0x20); 
            if(String.Compare(golfer, Golfer, StringComparison.Ordinal) != 0)
                throw new Exception("TW PGA Tour 14: invalid golfer file detected.");

            Profile = _io.In.ReadAsciiString(0x20);

            _io.SeekTo(0x000100E0);
            Experience = _io.In.ReadInt32();
            AttributePoints = 500 - _io.In.ReadInt32(); // stores total spent points

            _io.SeekTo(0x100A4);
            Power = (int)(Math.Round(_io.In.ReadSingle() * 100));
            Accuracy = (int)(Math.Round(_io.In.ReadSingle() * 100));
            Workability = (int) (Math.Round(_io.In.ReadSingle() * 100));
            Spin = (int) (Math.Round(_io.In.ReadSingle() * 100));
            Recovery = (int) (Math.Round(_io.In.ReadSingle() * 100));
            Putting = (int) (Math.Round(_io.In.ReadSingle() * 100));
        }

        public void Save()
        {
            // change golfer and title
            _io.SeekTo(0x20);
            _io.Out.WriteAsciiString(Golfer, 0x20);

            _io.SeekTo(0x48);
            _io.Out.WriteAsciiString(Golfer, 0x20);

            _io.SeekTo(0x000100E0);
            _io.Out.Write(Experience);
            _io.Out.Write(500 - AttributePoints);

            _io.SeekTo(0x100A4);
            _io.Out.Write((float)Power / 100);
            _io.Out.Write((float)Accuracy/100);
            _io.Out.Write((float)Workability/100);
            _io.Out.Write((float)Spin/100);
            _io.Out.Write((float)Recovery/100);
            _io.Out.Write((float)Putting/100);
           
            // resign save game
            _eaHeader.FixChecksums();
        }
    }
}
