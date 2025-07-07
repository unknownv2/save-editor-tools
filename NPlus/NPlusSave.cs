using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.NPlus
{
    class NPlusSave
    {
        public NPlusSave(byte[] data)
        {
            addEpisode(Episode.EpisodeType.TuT, 1);
            addEpisodes(Episode.EpisodeType.TuT, 50, 4);
            addEpisodes(Episode.EpisodeType.Epi, 100, 50);
            addEpisodes(Episode.EpisodeType.EpiC, 100, 10);
            addEpisodes(Episode.EpisodeType.Exp, 200, 10);
            addEpisodes(Episode.EpisodeType.LP1, 100, 30);
            addEpisodes(Episode.EpisodeType.LP1C, 100, 10);
            addEpisodes(Episode.EpisodeType.LP2, 100, 30);
            addEpisodes(Episode.EpisodeType.LP3, 100, 30);
            addEpisodes(Episode.EpisodeType.LP3C, 100, 10);
            EndianIO io = new EndianIO(data, EndianType.BigEndian, true);
            byte[] tD = io.In.ReadBytes(0x36);
            Header = new Settings(tD);
            int numEpisodes = io.In.ReadInt32();
            for (int x = 0; x < numEpisodes; x++)
            {
                byte[] tData = io.In.ReadBytes(0x0E);
                Episode tEpi = new Episode(tData);
                Episode.EpisodeType type = tEpi.episodeType;
                for (int i = 0; i < Episodes.Count; i++)
                    if (Episodes[i].episodeNumber == tEpi.episodeNumber
                        && Episodes[i].episodeType == type)
                        Episodes[i].copyEpisode(tEpi);
            }
            io.Close();
        }

        private void addEpisodes(Episode.EpisodeType type, int start, int amount)
        {
            for (int x = start; x < start + amount; x++)
                addEpisode(type, x);
        }

        private void addEpisode(Episode.EpisodeType type, int episodeNumber)
        {
            Episodes.Add(new Episode(type, episodeNumber));
        }

        public byte[] ToArray()
        {
            EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            io.Out.Write(Header.ToArray());
            long tOff = io.Stream.Position;
            io.Out.Write(0x00);
            int c = 0;
            for (int x = 0; x < Episodes.Count; x++)
                if (Episodes[x].unlockedSolo || Episodes[x].unlockedMultiplayer)
                {
                    io.Out.Write(Episodes[x].ToArray());
                    c++;
                }
            io.Stream.Position = tOff;
            io.Out.Write(c);
            byte[] tData = io.ToArray();
            io.Close();
            return tData;
        }

        public class Settings
        {
            private int Magic;
            public float SoundVolume;
            public float MusicVolume;
            public bool VibrationEnabled;
            public int CharacterModel;
            public bool AllowUserMaps;
            private byte[] Unknown;
            public bool BloodEnabled;
            private int UnknownInt1;
            public int SaveNumber;
            public int CompletedLevels;
            private int UnknownInt2;
            private int UnknownInt3;
            private int UnknownInt4;

            public Settings(byte[] data)
            {
                EndianIO io = new EndianIO(data, EndianType.BigEndian, true);
                Magic = io.In.ReadInt32();
                SoundVolume = io.In.ReadSingle();
                MusicVolume = io.In.ReadSingle();
                VibrationEnabled = io.In.ReadBoolean();
                CharacterModel = io.In.ReadInt32();
                AllowUserMaps = io.In.ReadBoolean();
                Unknown = io.In.ReadBytes(11);
                BloodEnabled = io.In.ReadBoolean();
                UnknownInt1 = io.In.ReadInt32();
                SaveNumber = io.In.ReadInt32();
                CompletedLevels = io.In.ReadInt32();
                UnknownInt2 = io.In.ReadInt32();
                UnknownInt3 = io.In.ReadInt32();
                UnknownInt4 = io.In.ReadInt32();
                io.Close();
            }

            public byte[] ToArray()
            {
                EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
                io.Out.Write(Magic);
                io.Out.Write(SoundVolume);
                io.Out.Write(MusicVolume);
                io.Out.Write(VibrationEnabled);
                io.Out.Write(CharacterModel);
                io.Out.Write(AllowUserMaps);
                io.Out.Write(Unknown);
                io.Out.Write(BloodEnabled);
                io.Out.Write(UnknownInt1);
                io.Out.Write(SaveNumber);
                io.Out.Write(CompletedLevels);
                io.Out.Write(UnknownInt2);
                io.Out.Write(UnknownInt3);
                io.Out.Write(UnknownInt4);
                return io.ToArray();
            }
        }

        public Settings Header;
        public List<Episode> Episodes = new List<Episode>();
        public class Episode
        {
            public enum EpisodeType
            {
                TuT,
                Epi,
                EpiC,
                Exp,
                LP1,
                LP1C,
                LP2,
                LP2C,
                LP3,
                LP3C
            }
            public bool bRight
            {
                get { return ((_flag & 0x40) >> 6) == 1; }
                set
                {
                    if (value)
                        _flag |= 0x40;
                    else
                        _flag &= 0xBF;
                }
            }
            public bool bLeft
            {
                get { return ((_flag & 0x80) >> 7) == 1; }
                set
                {
                    if (value)
                        _flag |= 0x80;
                    else
                        _flag &= 0x7F;
                }
            }
            public EpisodeType episodeType
            {
                get
                {
                    if (isDLC)
                    {
                        bool leftSet = bLeft;
                        if (leftSet && bRight)
                            return isCoOp ? EpisodeType.LP3C : EpisodeType.LP3;
                        if (isCoOp)
                            return leftSet ? EpisodeType.LP2C : EpisodeType.LP1C;
                        else
                            return leftSet ? EpisodeType.LP2 : EpisodeType.LP1;
                    }
                    else
                    {
                        if (isCoOp)
                            return EpisodeType.EpiC;
                        else
                        {
                            if (!bLeft && !bRight)
                                return EpisodeType.TuT;
                            return bRight ? EpisodeType.Epi : EpisodeType.Exp;
                        }
                    }
                }
                set
                {
                    if (isDLC = !(value == EpisodeType.Epi || value == EpisodeType.TuT || value == EpisodeType.EpiC || value == EpisodeType.Exp))
                    {
                        if (value == EpisodeType.LP1 || value == EpisodeType.LP1C)
                        {
                            isCoOp = value == EpisodeType.LP1C;
                            bLeft = false;
                            bRight = true;
                        }
                        else if (value == EpisodeType.LP2 || value == EpisodeType.LP2C)
                        {
                            isCoOp = value == EpisodeType.LP2C;
                            bLeft = true;
                            bRight = false;
                        }
                        else
                        {
                            isCoOp = value == EpisodeType.LP3C;
                            bLeft = true;
                            bRight = true;
                        }
                    }
                    else
                    {
                        isCoOp = value == EpisodeType.EpiC;
                        bLeft = value == EpisodeType.Exp;
                        bRight = (value == EpisodeType.Epi || value == EpisodeType.EpiC);
                    }
                    epiStringChanged = true;
                }
            }

            private bool epiStringChanged = true;
            private string _episodeString;
            public string episodeString
            {
                get
                {
                    if (!epiStringChanged)
                        return _episodeString;
                    string epiS = String.Empty;
                    switch (episodeNumber)
                    {
                        case 1:
                            epiS = "00";
                            break;
                        case 50:
                            epiS = "01";
                            break;
                        case 51:
                            epiS = "02";
                            break;
                        case 52:
                            epiS = "03";
                            break;
                        case 53:
                            epiS = "04";
                            break;
                        default:
                            epiS = episodeNumber.ToString().Substring(1);
                            break;
                    }
                    epiStringChanged = false;
                    return _episodeString = String.Format("{0} {1}", episodeType.ToString(), epiS);
                }
            }
            public bool isCoOp
            {
                get { return ((_flag & 0x10) >> 4) == 1; }
                set
                {
                    if (value)
                        _flag |= 0x10;
                    else
                        _flag &= 0xEF;
                }
            }
            public bool hasMultiplayer
            {
                get
                {
                    return !(!isDLC && ((bLeft && !bRight) || (!bLeft && !bRight)));
                }
            }
            public bool unlockedSolo
            {
                get { return (_flag & 0x01) == 1; }
                set
                {
                    if (value)
                        _flag |= 0x01;
                    else
                    {
                        _flag &= 0xFE;
                        completedSolo = false;
                    }
                }
            }
            public bool completedSolo
            {
                get { return ((_flag & 0x02) >> 1) == 1; }
                set
                {
                    if (value)
                        _flag |= 0x02;
                    else
                        _flag &= 0xFD;
                }
            }
            public bool unlockedMultiplayer
            {
                get { return ((_flag & 0x04) >> 2) == 1; }
                set
                {
                    if (value)
                        _flag |= 0x04;
                    else
                    {
                        _flag &= 0xFB;
                        completedMultiplayer = false;
                    }
                }
            }
            public bool completedMultiplayer
            {
                get { return ((_flag & 0x08) >> 3) == 1; }
                set
                {
                    if (value)
                        _flag |= 0x08;
                    else
                        _flag &= 0xF7;
                }
            }
            public int episodeNumber;
            public bool isDLC;
            private byte _flag = (byte)0x00;
            public float soloTime;
            public float multiplayerTime;

            public void copyEpisode(Episode epi)
            {
                _flag = epi._flag;
                soloTime = epi.soloTime;
                multiplayerTime = epi.multiplayerTime;
            }

            public Episode(byte[] data)
            {
                EndianIO io = new EndianIO(data, EndianType.BigEndian, true);
                isDLC = io.In.ReadBoolean();
                _flag = io.In.ReadByte();
                episodeNumber = io.In.ReadInt32();
                soloTime = io.In.ReadSingle();
                multiplayerTime = io.In.ReadSingle();
                io.Close();
            }

            public Episode(EpisodeType type, int episodeNum)
            {
                episodeNumber = episodeNum;
                episodeType = type;
            }

            public byte[] ToArray()
            {
                EndianIO io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
                io.Out.Write(isDLC);
                io.Out.Write(_flag);
                io.Out.Write(episodeNumber);
                io.Out.Write(completedSolo ? soloTime : 0);
                io.Out.Write(completedMultiplayer ? multiplayerTime : 0);
                return io.ToArray();
            }
        }
    }
}
