using System.IO;

namespace XBLA
{
    public class MotorcrossMadnessSave
    {
        private readonly EndianIO _io;

        public int Cash { get; set; }
        public int Experience { get; set; }
        public int SkillLevel { get; set; }

        public MotorcrossMadnessSave(EndianIO io)
        {
            _io = io;
            Read();
        }

        private void Read()
        {
            _io.SeekTo(0x34);
            Cash = _io.In.ReadInt32();
            Experience = _io.In.ReadInt32();
            SkillLevel = _io.In.ReadInt32();
        }

        public void Save()
        {
            _io.SeekTo(0x34);
            _io.Out.Write(Cash);
            _io.Out.Write(Experience);
            _io.Out.Write(SkillLevel);
        }
    }
}
