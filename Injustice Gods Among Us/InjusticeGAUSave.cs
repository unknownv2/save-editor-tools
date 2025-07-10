using System.IO;
namespace DCComics
{
    public class InjusticeGAUSave
    {
        private readonly EndianIO _io;

        public int Experience { get; set; }
        public int AccessCards { get; set; }
        public int ArmoryKeys { get; set; }
        public int TotalStars { get; set; }

     
        public InjusticeGAUSave(EndianIO io)
        {
            _io = io;
            Read();
        }
        
        private void Read()
        {
            Experience = _io.In.SeekNReadInt32(0x1CC);
            _io.SeekTo(0xF248);
            AccessCards = _io.In.ReadInt32();
            ArmoryKeys = _io.In.ReadInt32();
            TotalStars = _io.In.SeekNReadInt32(0x000101F8);
        }

        public void Save()
        {
            _io.Out.SeekNWrite(0x1CC, Experience);
            _io.SeekTo(0xF248);
            _io.Out.Write(AccessCards);
            _io.Out.Write(ArmoryKeys);
            _io.Out.SeekNWrite(0x000101F8, TotalStars);
        }

        public void UnlockAll()
        {
            _io.Out.SeekTo(0x10200);
            for (var i = 0; i < 0x21; i++)
                _io.Out.Write(-1);
            _io.Out.SeekTo(0xF1C8);
            for (var i = 0; i < 4; i++)
                _io.Out.Write(-1);
        }
    }
}
