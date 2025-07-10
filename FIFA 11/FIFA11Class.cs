using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.FIFA_11
{
    //SAVE SIZE MUST BE DIVISIBLE BY 0xFFF4
    public class FIFA11Class
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values

        //Basic Player Info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public string KitName { get; set; }

        public int WeightPounds { get; set; }
        public HeightIndex HeightInches { get; set; }
        public DefaultFootIndex DefaultFoot { get; set; }

        //Attributes
        public byte Aggression { get; set; }
        public byte TacticalAwareness { get; set; }
        public byte Mental { get; set; }
        public byte Vision { get; set; }

        //Stats
        public int CleanSheetStreak { get; set; }
        public int GamesPlayed { get; set; }
        public int WinCount { get; set; }
        public int DrawCount { get; set; }
        public int LossCount { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalsFor { get; set; }


        //Physical data
        public byte Acceleration { get; set; }
        public byte Agility { get; set; }
        public byte Balance { get; set; }
        public byte Jumping { get; set; }
        public byte Reactions { get; set; }
        public byte SprintSpeed { get; set; }
        public byte Stamina { get; set; }
        public byte Strength { get; set; }

        //Position data
        public byte BallControl { get; set; }
        public byte Crossing { get; set; }
        public byte Dribbling { get; set; }
        public byte Finishing { get; set; }
        public byte LongShots1 { get; set; }
        public byte LongShots2 { get; set; }
        public byte HeadingAccuracy { get; set; }
        public byte LongPassing { get; set; }
        public byte ShortPassing { get; set; }
        public byte Marking { get; set; }
        public byte ShotPower { get; set; }

        public byte StandingTackle { get; set; }
        public byte SlidingTackle { get; set; }
        public byte GK_Diving { get; set; }
        public byte GK_Handling { get; set; }
        public byte GK_Kicking { get; set; }
        public byte GK_Reflexes { get; set; }
        public byte GK_Positioning { get; set; }

        public byte Volleys { get; set; }

        #endregion

        #region Constructor

        public FIFA11Class(EndianIO io)
        {
            //Set our IO
            IO = io;
            //Verify first
            Verify();
            //Read our gamesave
            Read();
        }

        public void Verify()
        {
            //Go to our checksum offset
            IO.In.BaseStream.Position = 0x10;
            uint currentSig = IO.In.ReadUInt32();
            uint expectedSig = CalculateChecksum();
            //Compare our checksum
            if (currentSig != expectedSig && false)
                throw new Exception("Could not verify FIFA11 save successfully.");
        }
        public void Resign()
        {
            //Calculate our checksum
            uint checksum = CalculateChecksum();
            //Go to our checksum offset
            IO.Out.BaseStream.Position = 0x10;
            //Write our checksum
            IO.Out.Write(checksum);
        }
        private uint CalculateChecksum()
        {
            //Read our gamesave buffer
            IO.In.BaseStream.Position = 0x1C;
            byte[] saveData = IO.In.ReadBytes(IO.In.BaseStream.Length - IO.In.BaseStream.Position);
            return ElectronicArts.EACRC32.Calculate_Alt2(saveData, saveData.Length, 0x00);
        }
        public void Read()
        {
            //Basic info
            IO.In.BaseStream.Position = 0x553DE;
            FirstName = IO.In.ReadAsciiString(0x20);
            IO.In.BaseStream.Position = 0x5542B;
            LastName = IO.In.ReadAsciiString(0x20);
            IO.In.BaseStream.Position = 0x55478;
            KnownAs = IO.In.ReadAsciiString(0x20);
            IO.In.BaseStream.Position = 0x554C5;
            KitName = IO.In.ReadAsciiString(0x20);

            IO.In.BaseStream.Position = 0x55781;
            HeightInches = (HeightIndex)IO.In.ReadInt32();
            WeightPounds = IO.In.ReadInt32(); //-20
            IO.In.BaseStream.Position = 0x5579D;
            DefaultFoot = (int)DefaultFoot > 1 ? 0 : (DefaultFootIndex)DefaultFoot;

            //Physical Offsets
            IO.In.BaseStream.Position = 0x557C6;
            Acceleration = IO.In.ReadByte();
            SprintSpeed = IO.In.ReadByte();
            Agility = IO.In.ReadByte();
            Balance = IO.In.ReadByte();
            Jumping = IO.In.ReadByte();
            Stamina = IO.In.ReadByte();
            Strength = IO.In.ReadByte();
            Reactions = IO.In.ReadByte();
            Aggression = IO.In.ReadByte();
            TacticalAwareness = IO.In.ReadByte();
            Mental = IO.In.ReadByte();
            Vision = IO.In.ReadByte();

            //Position offsets
            IO.In.BaseStream.Position = 0x557D2;
            BallControl = IO.In.ReadByte();
            Crossing = IO.In.ReadByte();
            Dribbling = IO.In.ReadByte();
            Finishing = IO.In.ReadByte();
            LongShots1 = IO.In.ReadByte();
            HeadingAccuracy = IO.In.ReadByte();
            LongPassing = IO.In.ReadByte();
            ShortPassing = IO.In.ReadByte();
            Marking = IO.In.ReadByte();
            ShotPower = IO.In.ReadByte();
            LongShots2 = IO.In.ReadByte();
            StandingTackle = IO.In.ReadByte();
            SlidingTackle = IO.In.ReadByte();
            Volleys = IO.In.ReadByte();

            //Traditional Play Style
            IO.In.BaseStream.Position = 0x557E2;
            GK_Diving = IO.In.ReadByte();
            GK_Handling = IO.In.ReadByte();
            GK_Kicking = IO.In.ReadByte();
            GK_Reflexes = IO.In.ReadByte();
            GK_Positioning = IO.In.ReadByte();

            //Stats
            IO.In.BaseStream.Position = 0x374E;
            WinCount = IO.In.ReadInt32();
            DrawCount = IO.In.ReadInt32();
            LossCount = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3736;
            GamesPlayed = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3806;
            CleanSheetStreak = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3862;
            GoalsAgainst = IO.In.ReadInt32();
            IO.In.BaseStream.Position = 0x3776;
            GoalsFor = IO.In.ReadInt32();

        }
        public void Write(bool unlockAllTraits)
        {
            //Basic info
            IO.Out.BaseStream.Position = 0x553DE;
            IO.Out.WriteAsciiString(FirstName, 0x21);
            IO.Out.BaseStream.Position = 0x5542B;
            IO.Out.WriteAsciiString(LastName, 0x21);
            IO.Out.BaseStream.Position = 0x55478;
            IO.Out.WriteAsciiString(KnownAs, 0x21);
            IO.Out.BaseStream.Position = 0x554C5;
            IO.Out.WriteAsciiString(KitName, 0x21);

            IO.Out.BaseStream.Position = 0x55781;
            IO.Out.Write((int)HeightInches);
            IO.Out.Write(WeightPounds); //+20
            IO.Out.BaseStream.Position = 0x5579D;
            IO.Out.Write((int)DefaultFoot);

            //Physical Offsets
            IO.Out.BaseStream.Position = 0x557C6;
            IO.Out.Write(Acceleration);
            IO.Out.Write(SprintSpeed);
            IO.Out.Write(Agility);
            IO.Out.Write(Balance);
            IO.Out.Write(Jumping);
            IO.Out.Write(Stamina);
            IO.Out.Write(Strength);
            IO.Out.Write(Reactions);
            IO.Out.Write(Aggression);
            IO.Out.Write(TacticalAwareness);
            IO.Out.Write(Mental);
            IO.Out.Write(Vision);

            //Position offsets
            IO.Out.BaseStream.Position = 0x557D2;
            IO.Out.Write(BallControl);
            IO.Out.Write(Crossing);
            IO.Out.Write(Dribbling);
            IO.Out.Write(Finishing);
            IO.Out.Write(LongShots1);
            IO.Out.Write(HeadingAccuracy);
            IO.Out.Write(LongPassing);
            IO.Out.Write(ShortPassing);
            IO.Out.Write(Marking);
            IO.Out.Write(ShotPower);
            IO.Out.Write(LongShots2);
            IO.Out.Write(StandingTackle);
            IO.Out.Write(SlidingTackle);
            IO.Out.Write(Volleys);

            //Traditional Play Style
            IO.Out.BaseStream.Position = 0x557E2;
            IO.Out.Write(GK_Diving);
            IO.Out.Write(GK_Handling);
            IO.Out.Write(GK_Kicking);
            IO.Out.Write(GK_Reflexes);
            IO.Out.Write(GK_Positioning);

            //Stats
            IO.Out.BaseStream.Position = 0x374E;
            IO.Out.Write(WinCount);
            IO.Out.Write(DrawCount);
            IO.Out.Write(LossCount);
            IO.Out.BaseStream.Position = 0x3736;
            IO.Out.Write(GamesPlayed);
            IO.Out.BaseStream.Position = 0x3806;
            IO.Out.Write(CleanSheetStreak);
            IO.Out.BaseStream.Position = 0x3862;
            IO.Out.Write(GoalsAgainst);
            IO.Out.BaseStream.Position = 0x3776;
            IO.Out.Write(GoalsFor);

            //If we unlock all traits..
            if (unlockAllTraits)
            {
                IO.Out.BaseStream.Position = 0x55820;
                IO.Out.Write(
                    new byte[] { 
                        0x01, 0x0E, 0x01, 0x00,
                        0x00, 0x00, 0x00, 0x00,
                        0x00, 0x00, 0x60, 0x00,
                        0x06, 0x00, 0x00, 0x01,
                        0x10, 0x00, 0x00, 0x00,
                        0x00, 0x04, 0x00, 0x00,
                        0x00, 0x00, 0x00, 0x00,
                        0x0A });
            }

            //Resign
            Resign();
        }
        #endregion

        public enum DefaultFootIndex : int
        {
            Left = 0x00,
            Right = 0x01
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
            a6_7 = 200
        }
    }
}
