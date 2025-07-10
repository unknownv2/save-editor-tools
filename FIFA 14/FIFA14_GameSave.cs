using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace FIFA
{
    internal class FIFA14_GameSave
    {
        private readonly EndianIO IO;
        public VirtualProPlayer Player;
        private readonly string FirstSaltMsg;

        public FIFA14_GameSave(EndianIO io, string salt)
        {
            if (io == null) return;

            IO = io;

            FirstSaltMsg = salt;

            Verify();

            Player = new VirtualProPlayer(IO);
        }

        private void Verify()
        {
            //Go to our checksum offset
            IO.In.BaseStream.Position = 0x10;
            uint currentSig = IO.In.ReadUInt32();
            uint expectedSig = CalculateChecksum();
            //Compare our checksum
            if (currentSig != expectedSig)
                throw new Exception("Could not verify FIFA12 save successfully.");

            IO.In.BaseStream.Position = IO.In.ReadInt32(0x1C) + 0x18 - 0x40;
            byte[] storedHash = IO.In.ReadBytes(0x40);
            if (!Horizon.Functions.Global.compareArray(storedHash, CalculateSaveHash()))
                throw new Exception("Could not verify FIFA14 save successfully.");
        }

        public void Save()
        {
            Player.Save();
            Resign();
        }

        private void Resign()
        {
            //calculate save hash
            var hashLength = (IO.In.ReadInt32(0x1C) - 0x40);
            var hashDigest = CalculateSaveHash();
            IO.Out.SeekTo(0x18 + hashLength);
            IO.Out.Write(hashDigest);
            // calculate checksum
            uint checksum = CalculateChecksum();
            //Go to our checksum offset
            IO.Out.BaseStream.Position = 0x10;
            //Write our checksum
            IO.Out.Write(checksum);
        }
        private uint CalculateChecksum()
        {
            IO.In.BaseStream.Position = 0x1C;
            byte[] saveData = IO.In.ReadBytes(IO.In.BaseStream.Length - IO.In.BaseStream.Position);
            return ElectronicArts.EACRC32.Calculate_Alt2(saveData, saveData.Length, 0x00);
        }
        private byte[] CalculateSaveHash()
        {
            IO.In.BaseStream.Position = 0x1C;
            byte[] saveData = IO.In.ReadBytes(IO.In.PeekInt32() - 0x40);
            HorizonCrypt.memset(saveData, saveData.Length - 0x04, 0x00, 0x04);

            SHA512Managed sha = new SHA512Managed();
            sha.TransformBlock(Encoding.ASCII.GetBytes(FirstSaltMsg), 0, 0x21, null, 0);
            sha.TransformBlock(new byte[] { 0 }, 0, 1, null, 0x00);
            sha.TransformFinalBlock(saveData, 0, saveData.Length);

            return sha.Hash;
        }
        public enum HeightIndex : int
        {
            a5_2 = 158,
            a5_3 = 159,
            a5_4 = 162,
            a5_5 = 164,
            a5_6 = 167,
            a5_7 = 169,
            a5_8 = 172,
            a5_9 = 174,
            a5_10 = 177,
            a5_11 = 180,
            a6_0 = 182,
            a6_1 = 185,
            a6_2 = 187,
            a6_3 = 190,
            a6_4 = 192,
            a6_5 = 195,
            a6_6 = 197,
            a6_7 = 200,
            a6_8 = 202,
            a6_9 = 205,
            a6_10 = 208
        }

        public class VirtualProPlayer
        {
            readonly EndianIO IO;

            public string FirstName; // 0x4D
            public string LastName; // 0x4D
            public string CommonName; // 0x4D
            public string JerseyName; // 0x4D

            //public string JerseyStampingName; // 0x4D
            //public string FullName; // 0x4D
            //public string DisplayName; // 0x4D

            public int Height;
            public int PreferredFoot;

            public byte Aggression { get; set; }
            public byte TacticalAwareness { get; set; }
            public byte AttackPositioning { get; set; }
            public byte Vision { get; set; }

            public byte Acceleration { get; set; }
            public byte Agility { get; set; }
            public byte Balance { get; set; }
            public byte Jumping { get; set; }
            public byte Reactions { get; set; }
            public byte SprintSpeed { get; set; }
            public byte Stamina { get; set; }
            public byte Strength { get; set; }
            public byte Interceptions { get; set; }
            public byte BallControl { get; set; }
            public byte Crossing { get; set; }
            public byte Dribbling { get; set; }
            public byte Finishing { get; set; }
            public byte FreeKickAccuracy { get; set; }
            public byte HeadingAccuracy { get; set; }
            public byte LongPassing { get; set; }
            public byte ShortPassing { get; set; }
            public byte Marking { get; set; }
            public byte ShotPower { get; set; }
            public byte LongShots { get; set; }
            public byte PowerShotAccuracy { get; set; }
            public byte StandingTackle { get; set; }
            public byte SlidingTackle { get; set; }
            public byte Volleys { get; set; }
            public byte Curve { get; set; }
            public byte Penalties { get; set; }

            public byte GK_Diving { get; set; }


            public byte GK_Handling { get; set; }
            public byte GK_Kicking { get; set; }
            public byte GK_Reflexes { get; set; }
            public byte GK_Positioning { get; set; }

            public int JerseyNumber { get; set; }

            public VirtualProPlayer(EndianIO io)
            {
                if (io == null) return;

                IO = io;
                Read();
            }

            private void Read()
            {
                IO.In.SeekTo(0x21);
                FirstName = IO.In.ReadString(0x4D);
                LastName = IO.In.ReadString(0x4D);
                CommonName = IO.In.ReadString(0x4D);

                JerseyName = IO.In.ReadString(0x4D);
                //JerseyStampingName = IO.In.ReadString(0x4D);
                //FullName = IO.In.ReadString(0x4D);
                //DisplayName = IO.In.ReadString(0x4D);

                IO.In.SeekTo(0x3C8);
                Height = IO.In.ReadInt32();

                IO.In.SeekTo(0x3E4);
                PreferredFoot = IO.In.ReadInt32();

                // Start of Attributes
                IO.In.SeekTo(0x441);
                Acceleration = IO.In.ReadByte();
                SprintSpeed = IO.In.ReadByte();
                Agility = IO.In.ReadByte();
                Balance = IO.In.ReadByte();
                Jumping = IO.In.ReadByte();
                Stamina = IO.In.ReadByte();
                IO.In.BaseStream.Position++; // Unknown value
                Reactions = IO.In.ReadByte();
                Aggression = IO.In.ReadByte();
                Interceptions = IO.In.ReadByte();
                AttackPositioning = IO.In.ReadByte();
                Vision = IO.In.ReadByte();
                BallControl = IO.In.ReadByte();
                Crossing = IO.In.ReadByte();
                Dribbling = IO.In.ReadByte();
                Finishing = IO.In.ReadByte();
                FreeKickAccuracy = IO.In.ReadByte();
                HeadingAccuracy = IO.In.ReadByte();
                LongPassing = IO.In.ReadByte();
                ShortPassing = IO.In.ReadByte();
                Marking = IO.In.ReadByte();
                ShotPower = IO.In.ReadByte();
                LongShots = IO.In.ReadByte();
                StandingTackle = IO.In.ReadByte();
                SlidingTackle = IO.In.ReadByte();
                Volleys = IO.In.ReadByte();
                Curve = IO.In.ReadByte();
                Penalties = IO.In.ReadByte();
                GK_Diving = IO.In.ReadByte();
                GK_Handling = IO.In.ReadByte();
                GK_Kicking = IO.In.ReadByte();
                GK_Reflexes = IO.In.ReadByte();
                GK_Positioning = IO.In.ReadByte();

                IO.In.SeekTo(0x4C0);
                JerseyNumber = IO.In.ReadInt32();
            }

            public void Save()
            {
                IO.Out.SeekTo(0x21);
                IO.Out.WriteAsciiString(FirstName, 0x4D);
                IO.Out.WriteAsciiString(LastName, 0x4D);
                IO.Out.WriteAsciiString(CommonName, 0x4D);
                IO.Out.WriteAsciiString(JerseyName, 0x4D);

                IO.Out.SeekTo(0x3C8);
                IO.Out.Write(Height);

                IO.Out.SeekTo(0x3E4);
                IO.Out.Write(PreferredFoot);

                IO.Out.SeekTo(0x441);
                IO.Out.Write(Acceleration);
                IO.Out.Write(SprintSpeed);
                IO.Out.Write(Agility);
                IO.Out.Write(Balance);
                IO.Out.Write(Jumping);
                IO.Out.Write(Stamina);
                IO.Out.BaseStream.Position++;
                IO.Out.Write(Reactions);
                IO.Out.Write(Aggression);
                IO.Out.Write(Interceptions);
                IO.Out.Write(AttackPositioning);
                IO.Out.Write(Vision);
                IO.Out.Write(BallControl);
                IO.Out.Write(Crossing);
                IO.Out.Write(Dribbling);
                IO.Out.Write(Finishing);
                IO.Out.Write(FreeKickAccuracy);
                IO.Out.Write(HeadingAccuracy);
                IO.Out.Write(LongPassing);
                IO.Out.Write(ShortPassing);
                IO.Out.Write(Marking);
                IO.Out.Write(ShotPower);
                IO.Out.Write(LongShots);
                IO.Out.Write(StandingTackle);
                IO.Out.Write(SlidingTackle);
                IO.Out.Write(Volleys);
                IO.Out.Write(Curve);
                IO.Out.Write(Penalties);
                IO.Out.Write(GK_Diving);
                IO.Out.Write(GK_Handling);
                IO.Out.Write(GK_Kicking);
                IO.Out.Write(GK_Reflexes);
                IO.Out.Write(GK_Positioning);

                //IO.Out.SeekTo(0x4C0);
                //IO.Out.Write(JerseyNumber);
            }
        }
    }
}
