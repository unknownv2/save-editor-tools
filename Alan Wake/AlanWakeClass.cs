using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Alan_Wake
{
    public class AlanWakeClass
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values

        public uint Version { get; set; }
        public string Default_String { get; set; }

        public List<LevelOption> Normal_Unlocked_Levels { get; set; }
        public List<LevelOption> Hard_Unlocked_Levels { get; set; }
        public List<LevelOption> Nightmare_Unlocked_Levels { get; set; }

        public byte[] Unknown_Struct1 { get; set; }
        public byte[] Unknown_Struct2 { get; set; }
        public byte[] Unknown_Struct3 { get; set; }

        public List<ManuscriptPage> Manuscript_Pages { get; set; }

        public byte[] Unknown_Struct4 { get; set; }

        public const string ID_CANPYRAMIDS = "CANPYRAMIDS";
        public List<CanPyramidOption> CanPyramids_Found { get; set; }
        public const string ID_CHEST = "CHESTS";
        public List<ChestsOption> Chests_Found { get; set; }
        public const string ID_RADIO = "RADIOSHOWS";
        public List<RadioShowOption> Radios_Found { get; set; }
        public const string ID_SIGNS = "SIGNS";
        public List<SignOption> Signs_Found { get; set; }
        public const string ID_THERMOS = "THERMOS";
        public List<CoffeeThermosOption> Thermoses_Found { get; set; }
        public const string ID_TVSHOWS = "TVSHOWS";
        public List<TVShowOption> TVShows_Found { get; set; }
        public const string ID_COCONUTSONGS = "COCONUTSONGS";
        public List<byte> Coconut_Songs { get; set; }

        public Dictionary<string, List<byte>> Other_Structures { get; set; }


        public int Kills_With_Revolver { get; set; }
        public int Kills_With_Flaregun { get; set; }
        public int Kills_With_Shotgun { get; set; }
        public int Kills_With_Hunting_Rifle { get; set; }
        public int Poltergeist_Objects_Destroyed { get; set; }
        public int Birds_Killed { get; set; }
        public int Kills_With_Flashbangs { get; set; }
        public int Unknown1 { get; set; }
        public int Indirect_Kills { get; set; }
        public int Unknown2 { get; set; }
        public int Kills_With_Vehicles { get; set; }

        public bool Has_Save_Game { get; set; }
        public LevelOption Save_Game_Level { get; set; }
        #endregion

        #region Constructor

        public AlanWakeClass(EndianIO io)
        {
            //Set our IO
            IO = io;
            //Read our gamesave
            Read();
        }

        #endregion

        #region Functions

        public void Read()
        {
            //Clear our lists
            Normal_Unlocked_Levels = new List<LevelOption>();
            Hard_Unlocked_Levels = new List<LevelOption>();
            Nightmare_Unlocked_Levels = new List<LevelOption>();
            Manuscript_Pages = new List<ManuscriptPage>();
            Other_Structures = new Dictionary<string, List<byte>>();

            //Null our struct lists.
            CanPyramids_Found = null;
            Chests_Found = null;
            Radios_Found = null;
            Signs_Found = null;
            Thermoses_Found = null;
            TVShows_Found = null;
            Coconut_Songs = null;

            //Go to offset 0
            IO.In.BaseStream.Position = 0;
            //Read our version
            Version = IO.In.ReadUInt32();

            //<DATA UP TILL THE NEXT VALUE IS JUST VOIDS, WE DONT NEED TO ALTER THESE>

            //Go to our offset for the actual data we care about.
            IO.In.BaseStream.Position = 0x28;
            //Read our string based off our length byte.
            Default_String = IO.In.ReadAsciiString((int)IO.In.ReadByte());


            //Read the length for our normal difficulty unlocked levels.
            int normal_level_count = (int)IO.In.ReadByte();
            //Loop for each level
            for (int i = 0; i < normal_level_count; i++)
                //Read our level and add it to our list
                Normal_Unlocked_Levels.Add((LevelOption)IO.In.ReadByte());

            //Read the length for our hard difficulty unlocked levels.
            int hard_level_count = (int)IO.In.ReadByte();
            //Loop for each level
            for (int i = 0; i < hard_level_count; i++)
                //Read our level and add it to our list
                Hard_Unlocked_Levels.Add((LevelOption)IO.In.ReadByte());

            //Read the length for our nightmare difficulty unlocked levels.
            int nightmare_level_count = (int)IO.In.ReadByte();
            //Loop for each level
            for (int i = 0; i < nightmare_level_count; i++)
                //Read our level and add it to our list
                Nightmare_Unlocked_Levels.Add((LevelOption)IO.In.ReadByte());

            //Read our count for our next struct
            int structCount = (int)IO.In.ReadByte();
            //Read our struct.
            Unknown_Struct1 = IO.In.ReadBytes(structCount * 2);

            //Read our count for our next struct
            structCount = (int)IO.In.ReadByte();
            //Read our struct
            Unknown_Struct2 = IO.In.ReadBytes(structCount);

            //Read our struct count
            structCount = (int)IO.In.ReadByte();
            //Read our struct
            Unknown_Struct3 = IO.In.ReadBytes(structCount * 2);

            //Read our manuscript count
            int manuScriptPageCount = (int)IO.In.ReadByte();
            //Loop for each manuscript page
            for (int i = 0; i < manuScriptPageCount; i++)
                //Add our manuscript page
                Manuscript_Pages.Add((ManuscriptPage)IO.In.ReadByte());

            //Read our struct count
            structCount = (int)IO.In.ReadByte();
            //Read our struct
            Unknown_Struct4 = IO.In.ReadBytes(structCount);

            //Read our struct count
            structCount = (int)IO.In.ReadByte();

            //Read each struct
            for (int i = 0; i < structCount; i++)
            {
                //Read our string
                string ID = IO.In.ReadAsciiString(IO.In.ReadInt32());
                //Determine our type
                switch (ID)
                {
                    case ID_CANPYRAMIDS:
                        //Initialize our list
                        CanPyramids_Found = new List<CanPyramidOption>();
                        //Read our data length count
                        int dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            CanPyramids_Found.Add((CanPyramidOption)IO.In.ReadByte());
                        break;

                    case ID_CHEST:
                        //Initialize our list
                        Chests_Found = new List<ChestsOption>();
                        //Read our data length count
                        dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            Chests_Found.Add((ChestsOption)IO.In.ReadByte());
                        break;

                    case ID_RADIO:
                        //Initialize our list
                        Radios_Found = new List<RadioShowOption>();
                        //Read our data length count
                        dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            Radios_Found.Add((RadioShowOption)IO.In.ReadByte());
                        break;

                    case ID_SIGNS:
                        //Initialize our list
                        Signs_Found = new List<SignOption>();
                        //Read our data length count
                        dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            Signs_Found.Add((SignOption)IO.In.ReadByte());
                        break;

                    case ID_THERMOS:
                        //Initialize our list
                        Thermoses_Found = new List<CoffeeThermosOption>();
                        //Read our data length count
                        dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            Thermoses_Found.Add((CoffeeThermosOption)IO.In.ReadByte());
                        break;

                    case ID_TVSHOWS:
                        //Initialize our list
                        TVShows_Found = new List<TVShowOption>();
                        //Read our data length count
                        dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            TVShows_Found.Add((TVShowOption)IO.In.ReadByte());
                        break;
                    case ID_COCONUTSONGS:
                        //Initialize our list
                        Coconut_Songs = new List<byte>();
                        //Read our data length count
                        dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            Coconut_Songs.Add(IO.In.ReadByte());
                        break;
                    default:
                        //This will be used for other structures.
                        Other_Structures[ID] = new List<byte>();
                        //Read our data length count
                        dataLen = IO.In.ReadByte();
                        //Loop for each byte
                        for (int x = 0; x < dataLen; x++)
                            //Add our value
                            Other_Structures[ID].Add(IO.In.ReadByte());
                        break;
                }
            }

            //Read our remaining stats.
            Kills_With_Revolver = IO.In.ReadInt32();
            Kills_With_Flaregun = IO.In.ReadInt32();
            Kills_With_Shotgun = IO.In.ReadInt32();
            Kills_With_Hunting_Rifle = IO.In.ReadInt32();
            Poltergeist_Objects_Destroyed = IO.In.ReadInt32();
            Birds_Killed = IO.In.ReadInt32();
            Kills_With_Flashbangs = IO.In.ReadInt32();
            Unknown1 = IO.In.ReadInt32();
            Indirect_Kills = IO.In.ReadInt32();
            Unknown2 = IO.In.ReadInt32();
            Kills_With_Vehicles = IO.In.ReadInt32();

            //Check if we have a save game.
            Has_Save_Game = (IO.In.ReadByte() == 1);
            //If we have a save game
            if (Has_Save_Game)
                Save_Game_Level = (LevelOption)IO.In.ReadByte();
        }

        public void Write()
        {
            //Go to our offset for the actual data we care about.
            IO.Out.BaseStream.Position = 0x28;
            //Write our default string length
            IO.Out.Write((byte)Default_String.Length);
            //Write our default string
            IO.Out.WriteAsciiString(Default_String,(int)Default_String.Length);


            //Write our normal level count
            IO.Out.Write((byte)Normal_Unlocked_Levels.Count);
            //Loop for each level
            for (int i = 0; i < Normal_Unlocked_Levels.Count; i++)
                //Write our level
                IO.Out.Write((byte)Normal_Unlocked_Levels[i]);


            //Write our level count
            IO.Out.Write((byte)Hard_Unlocked_Levels.Count);
            //Loop for each level
            for (int i = 0; i < Hard_Unlocked_Levels.Count; i++)
                //Write our level
                IO.Out.Write((byte)Hard_Unlocked_Levels[i]);

            //Write our level count
            IO.Out.Write((byte)Nightmare_Unlocked_Levels.Count);
            //Loop for each level
            for (int i = 0; i < Nightmare_Unlocked_Levels.Count; i++)
                //Write our level
                IO.Out.Write((byte)Nightmare_Unlocked_Levels[i]);

            //Write our count for the struct
            IO.Out.Write((byte)(Unknown_Struct1.Length / 2));
            //Write our structure
            IO.Out.Write(Unknown_Struct1);

            //Write our count for the struct
            IO.Out.Write((byte)Unknown_Struct2.Length);
            //Write our structure
            IO.Out.Write(Unknown_Struct2);

            //Write our count for the struct
            IO.Out.Write((byte)(Unknown_Struct3.Length / 2));
            //Write our structure
            IO.Out.Write(Unknown_Struct3);

            //Write our manuscript count
            IO.Out.Write((byte)Manuscript_Pages.Count);
            //Loop for each manuscript page
            for (int i = 0; i < Manuscript_Pages.Count; i++)
                //Write our manuscript page
                IO.Out.Write((byte)Manuscript_Pages[i]);

            //Write our count for the struct
            IO.Out.Write((byte)Unknown_Struct4.Length);
            //Write our structure
            IO.Out.Write(Unknown_Struct4);

            //Create our structure count.
            byte structCount = 0;
            if (CanPyramids_Found != null)
                structCount++;
            if (Chests_Found != null)
                structCount++;
            if (Radios_Found != null)
                structCount++;
            if (Signs_Found != null)
                structCount++;
            if (Thermoses_Found != null)
                structCount++;
            if (TVShows_Found != null)
                structCount++;
            if (Coconut_Songs != null)
                structCount++;
            structCount += (byte)Other_Structures.Count;

            //Write our struct count
            IO.Out.Write(structCount);

            //If our structure isn't null..
            if (CanPyramids_Found != null)
            {
                //Write our string length
                IO.Out.Write((int)ID_CANPYRAMIDS.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID_CANPYRAMIDS, ID_CANPYRAMIDS.Length);
                //Write our structure count
                IO.Out.Write((byte)CanPyramids_Found.Count);
                //Loop for each value
                for (int i = 0; i < CanPyramids_Found.Count; i++)
                    //Write our value
                    IO.Out.Write((byte)CanPyramids_Found[i]);
            }
            //If our structure isn't null
            if (Chests_Found != null)
            {
                //Write our string length
                IO.Out.Write((int)ID_CHEST.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID_CHEST, ID_CHEST.Length);
                //Write our structure count
                IO.Out.Write((byte)Chests_Found.Count);
                //Loop for each value
                for (int i = 0; i < Chests_Found.Count; i++)
                    //Write our value
                    IO.Out.Write((byte)Chests_Found[i]);
            }
            //If our structure isn't null
            if (Radios_Found != null)
            {
                //Write our string length
                IO.Out.Write((int)ID_RADIO.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID_RADIO, ID_RADIO.Length);
                //Write our structure count
                IO.Out.Write((byte)Radios_Found.Count);
                //Loop for each value
                for (int i = 0; i < Radios_Found.Count; i++)
                    //Write our value
                    IO.Out.Write((byte)Radios_Found[i]);
            }
            //If our structure isn't null
            if (Signs_Found != null)
            {
                //Write our string length
                IO.Out.Write((int)ID_SIGNS.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID_SIGNS, ID_SIGNS.Length);
                //Write our structure count
                IO.Out.Write((byte)Signs_Found.Count);
                //Loop for each value
                for (int i = 0; i < Signs_Found.Count; i++)
                    //Write our value
                    IO.Out.Write((byte)Signs_Found[i]);
            }
            //If our structure isn't null.
            if (Thermoses_Found != null)
            {
                //Write our string length
                IO.Out.Write((int)ID_THERMOS.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID_THERMOS, ID_THERMOS.Length);
                //Write our structure count
                IO.Out.Write((byte)Thermoses_Found.Count);
                //Loop for each value
                for (int i = 0; i < Thermoses_Found.Count; i++)
                    //Write our value
                    IO.Out.Write((byte)Thermoses_Found[i]);
            }
            //If our structure isn't null.
            if (TVShows_Found != null)
            {
                //Write our string length
                IO.Out.Write((int)ID_TVSHOWS.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID_TVSHOWS, ID_TVSHOWS.Length);
                //Write our structure count
                IO.Out.Write((byte)TVShows_Found.Count);
                //Loop for each value
                for (int i = 0; i < TVShows_Found.Count; i++)
                    //Write our value
                    IO.Out.Write((byte)TVShows_Found[i]);
            }

            //If our structure isn't null..
            if (Coconut_Songs != null)
            {
                
                //Write our string length
                IO.Out.Write((int)ID_COCONUTSONGS.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID_COCONUTSONGS, ID_COCONUTSONGS.Length);
                //Write our structure count
                IO.Out.Write((byte)Coconut_Songs.Count);
                //Loop for each value
                for (int i = 0; i < Coconut_Songs.Count; i++)
                    //Write our value
                    IO.Out.Write(Coconut_Songs[i]);
            }

            //Now loop for our other structures
            foreach (string ID in Other_Structures.Keys)
            {
                //Write our string length
                IO.Out.Write((int)ID.Length);
                //Write our string
                IO.Out.WriteAsciiString(ID, ID.Length);
                //Write our structure count
                IO.Out.Write((byte)Other_Structures[ID].Count);
                //Loop for each value
                for (int i = 0; i < Other_Structures[ID].Count; i++)
                    //Write our value
                    IO.Out.Write(Other_Structures[ID][i]);
            }

            //Read our remaining stats.
            IO.Out.Write(Kills_With_Revolver);
            IO.Out.Write(Kills_With_Flaregun);
            IO.Out.Write(Kills_With_Shotgun);
            IO.Out.Write(Kills_With_Hunting_Rifle);
            IO.Out.Write(Poltergeist_Objects_Destroyed);
            IO.Out.Write(Birds_Killed);
            IO.Out.Write(Kills_With_Flashbangs);
            IO.Out.Write(Unknown1);
            IO.Out.Write(Indirect_Kills);
            IO.Out.Write(Unknown2);
            IO.Out.Write(Kills_With_Vehicles);

            //Write our save game status
            if (Has_Save_Game)
            {
                //Write our byte
                IO.Out.Write((byte)1);
                //Write our level byte
                IO.Out.Write((byte)Save_Game_Level);
            }
            else
            {
                //Write our byte
                IO.Out.Write((byte)0);
            }

            //Resize
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }

        public static string EnumStrToStr(string str)
        {
            //Split our string at each _
            string[] tmpStr = str.Split('_');
            //Create our resultant string
            string result = "";
            //Loop for each part
            for (int i = 0; i < tmpStr.Length; i++)
            {
                //If i == 0
                if (i == 0)
                    result += tmpStr[i] + ": ";
                else
                    result += tmpStr[i] + " ";
            }
            //Remove our last space
            if (tmpStr.Length > 0)
                result = result.Substring(0, result.Length - 1);
            return result;
        }
        public static string StrToEnumStr(string str)
        {
            //Return our string
            return str.Replace(":", "").Replace(" ", "_");
        }
        #endregion

        #region Structures and Classes
        /// <summary>
        /// This enum gives us a list of levels that we can put into a list in our gamesave to unlock.
        /// </summary>
        public enum LevelOption : int
            {
                Ep1_A_Writers_Dream = 0x01,
                Ep1_Welcome_To_Bright_Falls = 0x02,
                Ep1_Waking_Up_To_A_Nightmare = 0x03,
                Ep2_Bright_Falls_Sheriffs_Station = 0x04,
                Ep2_Elderwood_National_Park = 0x05,
                //Er... Gap.. I added it in my gamesave, nothing else appeared.. Reserved for DLC maybe?
                Ep3_On_The_Run = 0x09,
                Ep3_Mirror_Peak = 0x0A,
                Ep4_Cauldron_Lake_Lodge = 0x0B,
                Ep4_The_Anderson_Farm = 0x0C,
                Ep4_The_Night_It_All_Began = 0x0D,
                Ep5_Night_Life_in_Bright_Falls = 0x0E,
                Ep5_Bright_Falls_Light_And_Power = 0x0F,
                Ep6_On_the_Road_to_Cauldron_Lake = 0x10,
                Ep6_The_Dark_Place = 0x11
            }
         public enum ManuscriptPage
         {
             Ep1_The_Title_Page_of_the_Manuscript = 0x00,
             Ep1_Wake_Attacked_by_a_Shadowy_Murderer = 0x01,
             Ep1_Wake_Fights_a_Taken_with_Light = 0x02,
             Ep1_The_Dark_Prescence_Wakes_Up = 0x03,
             Ep1_Wake_Attacked_by_Birds = 0x04,
             Ep1_Wake_Finds_Pages = 0x05,
             Ep1_TV_in_the_Gas_Station = 0x06,
             Ep1_Wake_Lies_to_the_Sheriff = 0x07,
             Ep1_Stucky_Taken = 0x08,
             Ep1_Rose_Daydreams_About_Wake = 0x09,
             Ep1_Barrys_Arrival = 0x0A,
             Ep1_Toby_the_Dog = 0x0B,
             Ep1_Rose_is_a_Fan = 0x0C,
             Ep2_The_Sudden_Stop_1 = 0x0D,
             Ep2_The_Sudden_Stop_2 = 0x0E,
             Ep2_The_Dark_Presence_in_the_Diner = 0x0F,
             Ep2_Wake_at_Lovers_Peak = 0x10,
             Ep2_Alice_Sees_a_Shadow = 0x11,
             Ep2_Barry_Doubts_Wakes_Sanity = 0x12,
             Ep2_Rusty_Dying = 0x13,
             Ep2_Rusty_Attacked_by_the_Dark_Presence = 0x14,
             Ep2_Wake_Reaches_a_Safe_Haven_of_Light = 0x15,
             Ep2_Rustys_Final_Thoughts = 0x16,
             Ep2_Wake_Sees_the_Torch_Symbol = 0x17,
             Ep2_Nightingales_Arrival = 0x18,
             Ep2_Alices_Fear_of_the_Dark = 0x19,
             Ep2_Wake_Hears_a_Chainsaw = 0x1A,
             Ep2_Barry_in_Elderwood = 0x1B,
             Ep2_Nightingale_Fires_at_Wake = 0x1C,
             Ep2_Wake_at_the_Dark_Presences_Mercy = 0x1D,
             Ep2_Rose_and_Rusty = 0x1E,
             Ep2_Barry_Meets_Rose = 0x1F,
             Ep2_Sarah_Thinks_About_Wake = 0x20,
             Ep2_Deputies_at_the_Logging_Site = 0x21,
             Ep2_Wake_Feels_the_Dark_Presence = 0x22,
             Ep2_Wakes_Despair = 0x23,
             Ep3_Randolph_Calls_the_Police = 0x24,
             Ep3_The_Dark_Presence_Sleeps = 0x25,
             Ep3_Nightingale_in_the_Radio_Station = 0x26,
             Ep3_Sarah_Distrusts_Nightingale = 0x27,
             Ep3_Wake_Attacked_by_a_Possessed_Object = 0x28,
             Ep3_Wake_and_the_Dark_Presence_in_the_Lodge = 0x29,
             Ep3_Wake_Attacked_by_the_Dark_Presence = 0x2A,
             Ep3_Rose_Visited_by_the_Dark_Presence = 0x2B,
             Ep3_Rose_Touched_by_the_Dark_Presence = 0x2C,
             Ep3_Walter_Fights_Danny = 0x2D,
             Ep3_Wake_Attacked_by_a_Bulldozer = 0x2E,
             Ep3_Wake_and_Night_Springs = 0x2F,
             Ep3_Sarah_in_the_Radio_Station = 0x30,
             Ep3_Thomas_Zane_in_Love_with_Barbara_Jagger = 0x31,
             Ep3_Wake_Touched_by_the_Dark_Presence = 0x32,
             Ep3_Wake_and_Barry_in_the_Cell = 0x33,
             Ep3_Wake_and_Casey = 0x34,
             Ep3_Nightingale_in_the_Majestic = 0x35,
             Ep3_Mott_at_Cauldron_Lake = 0x36,
             Ep3_Wake_Wakes_Up_in_the_Lodge = 0x37,
             Ep3_Mott_on_the_Ferry = 0x38,
             Ep3_Hunters_Taken = 0x39,
             Ep3_Doc_Examines_Barry_and_Rose = 0x3A,
             Ep3_Wake_Reads_a_Page = 0x3B,
             Ep3_Tor_Hits_Nurse_Sinclair = 0x3C,
             Ep4_Thomas_Zanes_Writing_and_Assistant = 0x3D,
             Ep4_Barry_in_the_Lodge = 0x3E,
             Ep4_Hartman_Watches_Wake_Fall = 0x3F,
             Ep4_Hartmans_Mission = 0x40,
             Ep4_Wake_Sees_the_Old_Gods_Stage = 0x41,
             Ep4_Barry_Attacked_by_a_Taken = 0x42,
             Ep4_Mott_in_Charge = 0x43,
             Ep4_Mott_Fails_Hartman = 0x44,
             Ep4_Hartman_and_the_Power_Failure = 0x45,
             Ep4_Hartman_Sedates_Wake = 0x46,
             Ep4_Nightingale_Arrests_Wake = 0x47,
             Ep4_The_Patients_Escape_the_Lodge = 0x48,
             Ep4_The_Dark_Presence_at_Large = 0x49,
             Ep4_The_Anderson_Brothers_in_the_70s = 0x4A,
             Ep4_The_Mystery_of_the_Missing_Week = 0x4B,
             Ep4_Walter_at_the_Anderson_Farm = 0x4C,
             Ep4_Hartman_During_the_Missing_Week = 0x4D,
             Ep4_Hartman_Considers_Mott_and_Wake = 0x4E,
             Ep4_Mulligan_Questions_Nightingales_Orders = 0x4F,
             Ep4_Nightingale_Finds_the_Manuscript = 0x50,
             Ep5_Nightingale_Reads_the_Manuscript = 0x51,
             Ep5_Nightingale_Attacked_by_the_Dark_Presence = 0x52,
             Ep5_The_Dark_Presence_Set_Back = 0x53,
             Ep5_Cynthias_Wrok = 0x54,
             Ep5_The_Dark_Presence_Hunts_Wake = 0x55,
             Ep5_Alice_Trapped_in_the_Dark = 0x56,
             Ep5_Barry_in_the_Sheriffs_Station = 0x57,
             Ep5_Barry_in_the_General_Store = 0x58,
             Ep5_Wakes_Plan = 0x59,
             Ep5_The_Falling_Helicopter = 0x5A,
             Ep5_Zanes_Shoebox = 0x5B,
             Ep5_Cynthia_on_Her_Way_to_the_Dam = 0x5C,
             Ep5_The_Poet_and_the_Muse_Lyrics_4 = 0x5D,
             Ep5_Children_of_the_Elder_God_Lyrics_1 = 0x5E,
             Ep5_Children_of_the_Elder_God_Lyrics_2 = 0x5F,
             Ep6_The_Dark_Presence_Wants_to_Stop_Wake = 0x60,
             Ep6_The_Trail_of_the_Dark_Presence = 0x61,
             Ep6_Thomas_Zanes_Last_Dive = 0x62,
             Ep6_The_Dark_Place = 0x63,
             Ep6_The_Way_through_the_Dark_Place = 0x64,
             Ep6_The_Poet_and_the_Muse_Lyrics_1 = 0x65,
             Ep6_The_Poet_and_the_Muse_Lyrics_2 = 0x66,
             Ep6_The_Poet_and_the_Muse_Lyrics_3 = 0x67,
             Ep6_Sarah_and_Barry_in_the_Well_Lit_Room = 0x68,
             Ep6_Zanes_Poem = 0x69
         }
         public enum CanPyramidOption
         {
             CanPyramid1 = 0x01,
             CanPyramid2 = 0x02,
             CanPyramid3 = 0x03,
             CanPyramid4 = 0x04,
             CanPyramid5 = 0x05,
             CanPyramid6 = 0x06,
             CanPyramid7 = 0x07,
             CanPyramid8 = 0x08,
             CanPyramid9 = 0x09,
             CanPyramid10 = 0x0A,
             CanPyramid11 = 0x0B,
             CanPyramid12 = 0x0C
         }
         public enum ChestsOption
         {
             Chest1 = 0x01,
             Chest2 = 0x02,
             Chest3 = 0x03,
             Chest4 = 0x04,
             Chest5 = 0x05,
             Chest6 = 0x06,
             Chest7 = 0x07,
             Chest8 = 0x08,
             Chest9 = 0x09,
             Chest10 = 0x0A,
             Chest11 = 0x0B,
             Chest12 = 0x0C,
             Chest13 = 0x0D,
             Chest14 = 0x0E,
             Chest15 = 0x0F,
             Chest16 = 0x10,
             Chest17 = 0x11,
             Chest18 = 0x12,
             Chest19 = 0x13,
             Chest20 = 0x14,
             Chest21 = 0x15,
             Chest22 = 0x16,
             Chest23 = 0x17,
             Chest24 = 0x18,
             Chest25 = 0x19,
             Chest26 = 0x1A,
             Chest27 = 0x1B,
             Chest28 = 0x1C,
             Chest29 = 0x1D,
             Chest30 = 0x1E
         }
         public enum RadioShowOption
         {
             RadioShow1 = 0x01,
             RadioShow2 = 0x02,
             RadioShow3 = 0x03,
             RadioShow4 = 0x04,
             RadioShow5 = 0x05,
             RadioShow6 = 0x06,
             RadioShow7 = 0x07,
             RadioShow8 = 0x08,
             RadioShow9 = 0x09,
             RadioShow10 = 0x0A,
             RadioShow11 = 0x0B
         }
         public enum SignOption
         {
             Ep1_68th_Annual_Deerfest = 0x01,
             Ep2_Have_You_Seen_This_Main = 0x02,
             Ep2_Skeleton_of_Columbian_Mammoth = 0x03,
             Ep2_Moonshine_Cave = 0x04,
             Ep2_The_Great_Old_One = 0x05,
             Ep2_Tree_Dates_From_1846 = 0x06,
             Ep3_Bright_Falls_Coal_Mine_Mountain = 0x07,
             Ep3_Gray_Peak_Gorge = 0x08,
             Ep3_Cauldron_Lake = 0x09,
             Ep4_Sundial = 0x0A,
             Ep4_The_Creators_Dilemma = 0x0B,
             Ep4_Welcome_To_Cauldron_Lake_Lodge = 0x0C,
             Ep4_Statue_Suspended = 0x0D,
             Ep4_Gods_of_Asgard = 0x0E,
             Ep5_Hubert_and_Amos_Statue = 0x0F,
             Ep5_Fan_Note = 0x10,
             Ep5_Church_Events = 0x11,
             Ep5_Randall_Memorial_Bridge = 0x12,
             Ep5_Power_Plant_Warning = 0x13,
             Ep5_Flood_Gate_Warning = 0x14,
             Ep5_Dam_Notice = 0x15,
             Ep6_Majestic_Motel_Outside = 0x16,
             Ep6_Majestic_Motel_Inside = 0x17,
             Ep6_Larsens_Trespass_Warning = 0x18,
             Ep6_Junk_Pile_Sign = 0x19
         }
         public enum CoffeeThermosOption
         {
             CoffeeThermos1 = 0x01,
             CoffeeThermos2 = 0x02,
             CoffeeThermos3 = 0x03,
             CoffeeThermos4 = 0x04,
             CoffeeThermos5 = 0x05,
             CoffeeThermos6 = 0x06,
             CoffeeThermos7 = 0x07,
             CoffeeThermos8 = 0x08,
             CoffeeThermos9 = 0x09,
             CoffeeThermos10 = 0x0A,
             CoffeeThermos11 = 0x0B,
             CoffeeThermos12 = 0x0C,
             CoffeeThermos13 = 0x0D,
             CoffeeThermos14 = 0x0E,
             CoffeeThermos15 = 0x0F,
             CoffeeThermos16 = 0x10,
             CoffeeThermos17 = 0x11,
             CoffeeThermos18 = 0x12,
             CoffeeThermos19 = 0x13,
             CoffeeThermos20 = 0x14,
             CoffeeThermos21 = 0x15,
             CoffeeThermos22 = 0x16,
             CoffeeThermos23 = 0x17,
             CoffeeThermos24 = 0x18,
             CoffeeThermos25 = 0x19,
             CoffeeThermos26 = 0x1A,
             CoffeeThermos27 = 0x1B,
             CoffeeThermos28 = 0x1C,
             CoffeeThermos29 = 0x1D,
             CoffeeThermos30 = 0x1E,
             CoffeeThermos31 = 0x1F,
             CoffeeThermos32 = 0x20,
             CoffeeThermos33 = 0x21,
             CoffeeThermos34 = 0x22,
             CoffeeThermos35 = 0x23,
             CoffeeThermos36 = 0x24,
             CoffeeThermos37 = 0x25,
             CoffeeThermos38 = 0x26,
             CoffeeThermos39 = 0x27,
             CoffeeThermos40 = 0x28,
             CoffeeThermos41 = 0x29,
             CoffeeThermos42 = 0x2A,
             CoffeeThermos43 = 0x2B,
             CoffeeThermos44 = 0x2C,
             CoffeeThermos45 = 0x2D,
             CoffeeThermos46 = 0x2E,
             CoffeeThermos47 = 0x2F,
             CoffeeThermos48 = 0x30,
             CoffeeThermos49 = 0x31,
             CoffeeThermos50 = 0x32,
             CoffeeThermos51 = 0x33,
             CoffeeThermos52 = 0x34,
             CoffeeThermos53 = 0x35,
             CoffeeThermos54 = 0x36,
             CoffeeThermos55 = 0x37,
             CoffeeThermos56 = 0x38,
             CoffeeThermos57 = 0x39,
             CoffeeThermos58 = 0x3A,
             CoffeeThermos59 = 0x3B,
             CoffeeThermos60 = 0x3C,
             CoffeeThermos61 = 0x3D,
             CoffeeThermos62 = 0x3E,
             CoffeeThermos63 = 0x3F,
             CoffeeThermos64 = 0x40,
             CoffeeThermos65 = 0x41,
             CoffeeThermos66 = 0x42,
             CoffeeThermos67 = 0x43,
             CoffeeThermos68 = 0x44,
             CoffeeThermos69 = 0x45,
             CoffeeThermos70 = 0x46,
             CoffeeThermos71 = 0x47,
             CoffeeThermos72 = 0x48,
             CoffeeThermos73 = 0x49,
             CoffeeThermos74 = 0x4A,
             CoffeeThermos75 = 0x4B,
             CoffeeThermos76 = 0x4C,
             CoffeeThermos77 = 0x4D,
             CoffeeThermos78 = 0x4E,
             CoffeeThermos79 = 0x4F,
             CoffeeThermos80 = 0x50,
             CoffeeThermos81 = 0x51,
             CoffeeThermos82 = 0x52,
             CoffeeThermos83 = 0x53,
             CoffeeThermos84 = 0x54,
             CoffeeThermos85 = 0x55,
             CoffeeThermos86 = 0x56,
             CoffeeThermos87 = 0x57,
             CoffeeThermos88 = 0x58,
             CoffeeThermos89 = 0x59,
             CoffeeThermos90 = 0x5A,
             CoffeeThermos91 = 0x5B,
             CoffeeThermos92 = 0x5C,
             CoffeeThermos93 = 0x5D,
             CoffeeThermos94 = 0x5E,
             CoffeeThermos95 = 0x5F,
             CoffeeThermos96 = 0x60,
             CoffeeThermos97 = 0x61,
             CoffeeThermos98 = 0x62,
             CoffeeThermos99 = 0x63,
             CoffeeThermos100 = 0x64
         }
         public enum TVShowOption
         {
             TVShow1 = 0x01,
             TVShow2 = 0x02,
             TVShow3 = 0x03,
             TVShow4 = 0x04,
             TVShow5 = 0x05,
             TVShow6 = 0x06,
             TVShow7 = 0x07,
             TVShow8 = 0x08,
             TVShow9 = 0x09,
             TVShow10 = 0x0A,
             TVShow11 = 0x0B,
             TVShow12 = 0x0C,
             TVShow13 = 0x0D,
             TVShow14 = 0x0E
         }
        #endregion
    }
}
