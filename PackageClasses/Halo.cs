using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using XboxDataBaseFile;

namespace Halo
{
    public enum HaloGame
    {
        HaloReach,
        Halo4
    }

    public enum CampaignDifficulty
    {
        Easy,
        Normal,
        Heroic,
        Legendary
    }

    public class CampaignLevelProgress
    {
        private static int Easy = 0, Normal = 1, Heroic = 2, Legendary = 3;
        public bool[] Solo;
        public bool[] CoOp;
        public bool[] RallyPoint;

        // completed difficulties
        public bool EasySolo
        {
            get
            {
                return Solo[Easy];
            }
            set
            {
                Solo[Easy] = value;
            }
        }
        public bool NormalSolo
        {
            get
            {
                return Solo[Normal];
            }
            set
            {
                Solo[Normal] = value;
            }
        }
        public bool HeroicSolo
        {
            get
            {
                return Solo[Heroic];
            }
            set
            {
                Solo[Heroic] = value;
            }
        }
        public bool LegendarySolo
        {
            get
            {
                return Solo[Legendary];
            }
            set
            {
                Solo[Legendary] = value;
            }
        }
        public bool EasyCoOp
        {
            get
            {
                return CoOp[Easy];
            }
            set
            {
                CoOp[Easy] = value;
            }
        }
        public bool NormalCoOp
        {
            get
            {
                return CoOp[Normal];
            }
            set
            {
                CoOp[Normal] = value;
            }
        }
        public bool HeroicCoOp
        {
            get
            {
                return CoOp[Heroic];
            }
            set
            {
                CoOp[Heroic] = value;
            }
        }
        public bool LegendaryCoOp
        {
            get
            {
                return CoOp[Legendary];
            }
            set
            {
                CoOp[Legendary] = value;
            }
        }

        public string LevelName;
        public bool Unlocked;
    }

    public abstract class HaloSettings
    {
        private readonly DataFile _dataFile;
        private readonly EndianIO _io;
        public EndianIO SettingsIO;

        private readonly ulong[] _profileSettingIds = new ulong[] { 0x63E83FFF, 0x63E83FFE, 0x63E83FFD };
        private readonly int[] _profileSettingSizes;
        private readonly int _hashLocation;
        private bool _isValid;

        protected HaloSettings(EndianIO io, HaloGame game)
        {
            switch (game)
            {
                case HaloGame.HaloReach:
                    _hashLocation = 0xAB8;
                    break;
                case HaloGame.Halo4:
                    _hashLocation = 0xA28;
                    break;
            }

            _io = io;

            _dataFile = new DataFile(_io);
            _dataFile.Read();

            _profileSettingSizes = new int[3];
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);
            for (int x = 0; x < 3; x++)
            {
                var er = new EndianReader(new MemoryStream(
                    _dataFile.ReadTitleSetting(
                    new DataFileId
                       {
                           Id = _profileSettingIds[x],
                           Namespace = Namespace.SETTINGS
                       })), EndianType.BigEndian);

                _profileSettingSizes[x] = (int)er.BaseStream.Length;
                ew.Write(er.ReadBytes((int)er.BaseStream.Length));
                er.Close();
            }
            // open the IO with the full settings data
            SettingsIO = new EndianIO(ms, EndianType.BigEndian, true);
            _isValid = VerifyData();
        }

        public void SaveToFile()
        {
            // re-hash setting data
            var data = SettingsIO.ToArray();
            HorizonCrypt.memset(data, _hashLocation, 0x99, 0x14);
            var sha = new SHA1Managed();
            sha.TransformFinalBlock(data, 0, data.Length);

            SettingsIO.Out.SeekTo(_hashLocation);
            SettingsIO.Out.Write(sha.Hash);

            SettingsIO.SeekTo(0);
            for (int x = 0; x < 3; x++)
            {
                DataFileId tagId = new DataFileId()
                {
                    Id = _profileSettingIds[x],
                    Namespace = Namespace.SETTINGS
                };
                _dataFile.WriteTitleSetting(tagId, SettingsIO.In.ReadBytes(_profileSettingSizes[x]));
            }
        }

        private bool VerifyData()
        {
            var data = SettingsIO.ToArray();
            HorizonCrypt.memset(data, _hashLocation, 0x99, 0x14);
            var sha = new SHA1Managed();
            sha.TransformFinalBlock(data, 0, data.Length);
            SettingsIO.SeekTo(_hashLocation);
            return HorizonCrypt.ArrayEquals(SettingsIO.In.ReadBytes(0x14), sha.Hash);
        }

        public byte[] Export()
        {
            return SettingsIO.ToArray();
        }

        public void Import(byte[] data)
        {
            if(data.Length != SettingsIO.Length)
                return;

            SettingsIO = new EndianIO(data, EndianType.BigEndian, true);
        }
    }
}
