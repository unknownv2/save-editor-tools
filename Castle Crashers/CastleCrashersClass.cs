using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XboxDataBaseFile;

namespace TheBehemoth
{
    public class CastleCrashersProfile
    {
        private const int CharacterCount = 31;

        private EndianIO IO;
        public List<CastleCrashersCharacter> Characters;

        public int Gold { get; set; }
        public bool AllWeaponsUnlocked { get; set; }

        private readonly ulong[] _profileSettingIds = new ulong[] { 0x63E83FFF, 0x63E83FFE };

        private DataFile ProfileFile;
        private readonly int[] profileSettingSizes;

        public CastleCrashersProfile(EndianIO io)
        {

            Characters = new List<CastleCrashersCharacter>();

            ProfileFile = new DataFile(io);
            ProfileFile.Read();
            profileSettingSizes = new int[2];
            IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            for (var x = 0; x < _profileSettingIds.Length; x++)
            {
                var er = new EndianReader(new MemoryStream(
                    ProfileFile.ReadTitleSetting(
                    new DataFileId
                    {
                        Id = _profileSettingIds[x],
                        Namespace = Namespace.SETTINGS
                    })), EndianType.BigEndian);

                profileSettingSizes[x] = (int)er.BaseStream.Length;
                IO.Out.Write(er.ReadBytes((int)er.BaseStream.Length));
                er.Close();
            }

            Read();
        }

        private void Read()
        {
            Gold = IO.In.SeekNReadInt32(0x45);

            IO.SeekTo(0x60);
            // populate the character list
            for (var i = 0; i < CharacterCount; i++)
                Characters.Add(new CastleCrashersCharacter(IO.In));
        }

        public void Save()
        {
            if (AllWeaponsUnlocked)
            {
                IO.SeekTo(0x31);
                for (var i = 0; i < 0x14; i++)
                    IO.Out.WriteByte(0xFF);
                IO.SeekTo(0x348);
                for (var i = 0; i < 8; i++)
                    IO.Out.WriteByte(0xFF);
            }

            IO.SeekTo(0x45);
            IO.Out.Write(Gold);

            IO.SeekTo(0x60);
            for (var x = 0; x < CharacterCount; x++)
                IO.Out.Write(Characters[x].ToArray());

            IO.SeekTo(0);
            for (var x = 0; x < _profileSettingIds.Length; x++)
            {
                var tagId = new DataFileId
                {
                    Id = _profileSettingIds[x],
                    Namespace = Namespace.SETTINGS
                };
                ProfileFile.WriteTitleSetting(tagId, IO.In.ReadBytes(profileSettingSizes[x]));
            }            
        }

        public void Inject(byte[] data)
        {
            
        }
    }

    public class CastleCrashersCharacter
    {
        public bool IsUnlocked { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Weapon { get; set; }
        public int Pet { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int Agility { get; set; }
        public int LevelProgress1 { get; set; }
        public int LevelProgress2 { get; set; }
        public bool IsBossUnlocked { get; set; }
        public int Potions { get; set; }
        public int Bombs { get; set; }
        public int Sandwiches { get; set; }
        public int Collectibles { get; set; }
        public bool InsaneModeEnabled { get; set; }
        public int InsaneModeLevelProgress1 { get; set; }
        public int InsaneModeLevelProgress2 { get; set; }
        public bool InsaneModeIsBossUnlocked { get; set; }
        public int Skull { get; set; }

        public CastleCrashersCharacter(EndianReader reader)
        {
            IsUnlocked = ((reader.ReadByte() >> 0x7) & 1) != 0;
            Level = reader.ReadByte();
            Experience = reader.ReadInt32();
            Weapon = reader.ReadByte();
            Pet = reader.ReadByte();
            Strength = reader.ReadByte();
            Defense = reader.ReadByte();
            Magic = reader.ReadByte();
            Agility = reader.ReadByte();
            LevelProgress1 = reader.ReadByte();
            LevelProgress2 = reader.ReadByte();
            IsBossUnlocked = reader.ReadByte() != 0;
            Potions = reader.ReadByte();
            Bombs = reader.ReadByte();
            Sandwiches = reader.ReadByte();
            Collectibles = reader.ReadByte();
            InsaneModeEnabled = reader.ReadByte() != 0;
            InsaneModeLevelProgress1 = reader.ReadByte();
            InsaneModeLevelProgress2 = reader.ReadByte();
            InsaneModeIsBossUnlocked = reader.ReadByte() != 0;
            Skull = reader.ReadByte();
        }

        public byte[] ToArray()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            io.Out.WriteByte(IsUnlocked ? 0x80 : 0x00);
            io.Out.WriteByte(Level);
            io.Out.Write(Experience);
            io.Out.WriteByte(Weapon);
            io.Out.WriteByte(Pet);
            io.Out.WriteByte(Strength);
            io.Out.WriteByte(Defense);
            io.Out.WriteByte(Magic);
            io.Out.WriteByte(Agility);
            io.Out.WriteByte(LevelProgress1);
            io.Out.WriteByte(LevelProgress2);
            io.Out.WriteByte(IsBossUnlocked);
            io.Out.WriteByte(Potions);
            io.Out.WriteByte(Bombs);
            io.Out.WriteByte(Sandwiches);
            io.Out.WriteByte(Collectibles);
            io.Out.WriteByte(InsaneModeEnabled);
            io.Out.WriteByte(InsaneModeLevelProgress1);
            io.Out.WriteByte(InsaneModeLevelProgress2);
            io.Out.WriteByte(InsaneModeIsBossUnlocked);
            io.Out.WriteByte(Skull);

            return io.ToArray();
        }
    }

    public enum CastleCrashersCharacters
    {
        Green_Knight,
        Red_Knight,
        Blue_Knight,
        Orange_Knight,
        Gray_Knight,
        Barbarian,
        Thief,
        Fencer,
        Beekeeper,
        Gear_Knight,
        Alien_Hominid,
        The_King,
        Brute,
        Druic_Knight,
        Saracen,
        Royal_Guard,
        Black_Knight,
        Peasant,
        Bear_Shaman,
        Necromancer,
        Periwinkle_Knight,
        Civilian,
        Unmasked_Gray_Knight,
        Fire_Demon,
        Skeleton,
        Eskimo,
        Ninja,
        Wizard_Mage,
        Pink_Knight,
        Black_Smith,
        Hatty_Hattington
    }
}
