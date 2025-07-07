using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Horizon.Functions;

namespace Horizon.PackageEditors.Super_Meat_Boy
{
    class SuperMeatBoySave
    {
        public SuperMeatBoySave(EndianIO io)
        {
            _char = io.In.ReadInt32();
            _chap = io.In.ReadInt32();
            TotalDeaths = io.In.ReadInt32();
            Constant = io.In.ReadInt32();
            DemoPadding = io.In.ReadBytes(12);
            for (int x = 0; x < 5; x++)
                Chapters[x] = new Chapter(io, 20, 20, 12);
            Chapters[5] = new Chapter(io, 5, 5, 0);
            Chapters[6] = new Chapter(io, 20, 20, 0);
            Chapters[7] = new Chapter(io, 20, 20, 0);
            Chapters[8] = new Chapter(io, 20, 12, -1);
            for (int x = 0; x < Chapters.Length; x++)
                Chapters[x].ReadLevels(io);
        }

        public void Write(EndianIO io)
        {
            io.Stream.Position = 0;
            io.Out.Write(_char);
            io.Out.Write(_chap);
            io.Out.Write(TotalDeaths);
            io.Out.Write(Constant);
            io.Out.Write(DemoPadding);
            for (int x = 0; x < Chapters.Length; x++)
            {
                Chapters[x].UpdateCounters();
                Chapters[x].Write(io);
            }
            for (int x = 0; x < Chapters.Length; x++)
                Chapters[x].WriteLevels(io);
        }

        private int _char;
        public bool getCharacterUnlock(int character)
        {
            return (_char & (1 << character)) >> character == 1;
        }

        public void setCharacterUnlock(int character, bool unlocked)
        {
            if (unlocked)
                _char |= (1 << character);
            else
                _char &= ~(1 << character);
        }

        /*public enum Character : int
        {
            MeatBoy = 0,
            MeatBoy8Bit = 1,
            MeatBoy4Color = 2,
            MeatBoy4Bit = 3,
            BandageGirl = 4,
            RottenMeatBoy = 5,
            DrFetus = 6,
            MeatNinja = 7,
            Tim = 10,
            CommanderVideo = 11,
            Spelunky = 12,
            PinkKnight = 14,
            Flywrench = 16,
            Jill = 18,
            TheNinja = 19,
            AlienHominid = 21,
            TheKid = 22,
            Gish = 23,
            Ogmo = 24,
            MeatBoyBandageGirl = 26
        }*/ // Sadly, I never needed to use the actual names.

        private int _chap;
        public bool getChapterUnlock(int chapter)
        {
            return (_chap & (1 << chapter)) >> chapter == 1;
        }

        public void setChapterUnlock(int chapter, bool unlocked)
        {
            if (unlocked)
                _chap |= (1 << chapter);
            else
                _chap &= ~(1 << chapter);
        }

        /*public enum ChapterType : int
        {
            TheForest = 0,
            TheHospital = 1,
            TheSaltFactory = 2,
            Hell = 3,
            Rapture = 4,
            TheEnd = 5,
            TheCottonAlley = 6,
            TheDemo = 7,
            TehInternets = 8,
        }*/ // Didn't have to use these either =\

        public string[] getLevelTags(int c)
        {
            if (Chapters[c].LevelTags != null)
                return Chapters[c].LevelTags;
            Chapters[c].LevelTags = new string[Chapters[c].Levels.Length];
            int warpsPassed = 0;
            if (Chapters[c]._numWarpZoneLevels == -1)
                for (int x = 0; x < Chapters[c].Levels.Length; x++)
                {
                    Chapters[c].LevelTags[x] = LevelType.Light.ToString() + ": ";
                    int big = x / Chapters[c]._numLightLevels;
                    Chapters[c].LevelTags[x] += String.Format("{0}-{1}", big + 1, x - (big * Chapters[c]._numLightLevels) + 1);
                }
            else
                for (int x = 0; x < Chapters[c].Levels.Length; x++)
                {
                    Chapters[c].LevelTags[x] = (Chapters[c].Levels[x].Type == LevelType.WarpZone
                        ? "Warp Zone" : Chapters[c].Levels[x].Type.ToString()) + ": ";
                    if (Chapters[c].Levels[x].Type == LevelType.Light)
                        Chapters[c].LevelTags[x] += (x + 1).ToString();
                    else if (Chapters[c].Levels[x].Type == LevelType.Dark)
                        Chapters[c].LevelTags[x] += (x + 1 - Chapters[c]._numLightLevels).ToString();
                    else
                    {
                        int big = warpsPassed / 3;
                        Chapters[c].LevelTags[x] += String.Format("{0}-{1}", big + 1, ++warpsPassed - (big * 3));
                    }
                }
            return Chapters[c].LevelTags;
        }

        public int TotalDeaths;
        private int Constant;
        private byte[] DemoPadding;
        public Chapter[] Chapters = new Chapter[9];

        public class Chapter
        {
            public string[] LevelTags;
            public int _numLightLevels;
            public int _numDarkLevels;
            public int _numWarpZoneLevels;

            public byte LightLevelsComplete;
            public byte DarkLevelsComplete;
            public byte Bandages;
            public byte WarpZoneLevelsCompleted;
            private short _flags;
            public byte WarpZonesUnlocked;
            private byte[] _padding;

            public bool BossComplete
            {
                get { return ((_flags & 0x04) >> 2) == 1; }
                set
                {
                    if (value)
                        _flags |= 0x04;
                    else
                        _flags &= 0xFB;
                }
            }

            public bool MemoryGlitchUnlocked
            {
                get { return (_flags & 0x01) == 1; }
                set
                {
                    if (value)
                        _flags |= 0x01;
                    else
                        _flags &= 0xFE;
                }
            }

            public Chapter(EndianIO io, int light, int dark, int warp)
            {
                _numLightLevels = light;
                _numDarkLevels = dark;
                _numWarpZoneLevels = warp;
                LightLevelsComplete = io.In.ReadByte();
                DarkLevelsComplete = io.In.ReadByte();
                Bandages = io.In.ReadByte();
                WarpZoneLevelsCompleted = io.In.ReadByte();
                _flags = io.In.ReadInt16();
                WarpZonesUnlocked = io.In.ReadByte();
                _padding = io.In.ReadBytes(5);
            }

            public void Write(EndianIO io)
            {
                io.Out.Write(LightLevelsComplete);
                io.Out.Write(DarkLevelsComplete);
                io.Out.Write(Bandages);
                io.Out.Write(WarpZoneLevelsCompleted);
                io.Out.Write(_flags);
                io.Out.Write(WarpZonesUnlocked);
                io.Out.Write(_padding);
            }

            public void UpdateCounters()
            {
                LightLevelsComplete = DarkLevelsComplete = Bandages
                    = WarpZoneLevelsCompleted = WarpZonesUnlocked = 0;
                for (int x = 0; x < Levels.Length; x++)
                {
                    if (Levels[x].Completed)
                    {
                        if (Levels[x].Type == LevelType.Light)
                            LightLevelsComplete++;
                        else if (Levels[x].Type == LevelType.Dark)
                            DarkLevelsComplete++;
                        else
                            WarpZoneLevelsCompleted++;
                    }
                    if (Levels[x].BandageObtained)
                        Bandages++;
                    if (Levels[x].WarpZoneUnlocked)
                        WarpZonesUnlocked++;
                }
                if (Bandages > 255)
                    Bandages = 255;
            }

            public Level[] Levels;
            public void ReadLevels(EndianIO io)
            {
                Levels = new Level[_numWarpZoneLevels == -1 ? _numLightLevels * _numDarkLevels : _numLightLevels + _numDarkLevels + _numWarpZoneLevels];
                if (_numWarpZoneLevels == -1)
                    for (int x = 0; x < Levels.Length; x++)
                        Levels[x] = new Level(io, LevelType.Light);
                else
                {
                    int i = 0;
                    for (int x = 0; x < _numLightLevels; x++)
                        Levels[i++] = new Level(io, LevelType.Light);
                    for (int x = 0; x < _numDarkLevels; x++)
                        Levels[i++] = new Level(io, LevelType.Dark);
                    for (int x = 0; x < _numWarpZoneLevels; x++)
                        Levels[i++] = new Level(io, LevelType.WarpZone);
                }
            }

            public void WriteLevels(EndianIO io)
            {
                for (int x = 0; x < Levels.Length; x++)
                    Levels[x].Write(io);
            }
        }

        public class Level
        {
            public LevelType Type;
            public float TimeCompleted;
            private int _flags;
            private int Constant;

            public bool BandageObtained
            {
                get { return (_flags & 0x01) == 1; }
                set
                {
                    if (value)
                        _flags |= 0x01;
                    else
                        _flags &= 0xFE;
                }
            }

            public bool Completed
            {
                get { return ((_flags & 0x02) >> 1) == 1; }
                set
                {
                    if (value)
                        _flags |= 0x02;
                    else
                    {
                        _flags &= 0xFD;
                        WarpZoneUnlocked = BandageObtained = value;
                    }
                }
            }

            public bool WarpZoneUnlocked
            {
                get { return ((_flags & 0x08) >> 3) == 1; }
                set
                {
                    if (value)
                        _flags |= 0x08;
                    else
                        _flags &= 0xF7;
                }
            }

            public Level(EndianIO io, LevelType type)
            {
                Type = type;
                TimeCompleted = io.In.ReadSingle();
                _flags = io.In.ReadInt32();
                Constant = io.In.ReadInt32();
            }

            public void Write(EndianIO io)
            {
                io.Out.Write(TimeCompleted);
                io.Out.Write(_flags);
                io.Out.Write(Constant);
            }
        }

        public enum LevelType
        {
            Light,
            Dark,
            WarpZone
        }
    }
}
