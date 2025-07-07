using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Horizon.PackageEditors.Fable_2
{
    public class Fable2HeroSave
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>

        #region Game Save Values
       
        //BASIC VALUES
        public int Money { get; set; }
        public GenderOption Gender { get; set; }
        public float Morality { get; set; }
        public float Purity { get; set; }
        public int Renown { get; set; }

        //HERO ABILITY LEVELS
        public int HEROABILITY0 { get; set; }
        public int ABILITY_BRUTALSTYLES { get; set; }
        public int ABILITY_PHYSIQUE { get; set; }
        public int ABILITY_TOUGHNESS { get; set; }
        public int ABILITY_DEXTROUSSTYLES { get; set; }
        public int ABILITY_ACCURACY { get; set; }
        public int ABILITY_SPEED { get; set; }
        public int ABILITY_SHOCK { get; set; }
        public int ABILITY_INFERNO { get; set; }
        public int ABILITY_TIMECONTROL { get; set; }
        public int ABILITY_BLADES { get; set; }
        public int ABILITY_VORTEX { get; set; }
        public int ABILITY_CHAOS { get; set; }
        public int HEROABILITY13 { get; set; }
        public int HEROABILITY14 { get; set; }

        //EXPERIENCE VALUES
        public float EXPERIENCE0 { get; set; }
        public float TOTAL_EXPERIENCE0 { get; set; }
        public float EXPERIENCE1 { get; set; }
        public float TOTAL_EXPERIENCE1 { get; set; }
        public float EXPERIENCE2 { get; set; }
        public float TOTAL_EXPERIENCE2 { get; set; }
        public float EXPERIENCE3 { get; set; }
        public float TOTAL_EXPERIENCE3 { get; set; }

        //OTHER VALUES
        public float SECONDS_PLAYED_AS_HENCHMEN { get; set; }
        public bool CAN_UNLOCK_ACHIEVEMENTS { get; set; }
        public enum GenderOption : int
        {
            Male = 0x02,
            Female = 0x01
        }
        #endregion
    
        #region Constructor

        public Fable2HeroSave(EndianIO IO)
        {
            //Read our gamesave
            Read(IO);
        }

        #endregion

        #region Functions

        public void Read(EndianIO IO)
        {
            //Read our XML Data
            IO.In.BaseStream.Position = 0x00;
            string XML_Data = IO.In.ReadAsciiString((int)IO.In.ReadUInt32());


            //Read the values from our XML

            //Initialize our XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            //Load our plugin
            xmlDoc.Load(new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(XML_Data)));
            //Get the root to read the header information from
            XmlElement root = xmlDoc.DocumentElement;
            //Loop for each child in the root
            foreach (XmlNode valueNode in root.ChildNodes)
            {
                //Check our value name
                switch (valueNode.Name)
                {
                    case "Money":
                        Money = int.Parse(valueNode.InnerText);
                        break;
                    case "Gender":
                        Gender = (GenderOption)int.Parse(valueNode.InnerText);
                        break;
                    case "Morality":
                        Morality = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "Purity":
                        Purity = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "Renown":
                        Renown = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility0":
                        HEROABILITY0 = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility1":
                        ABILITY_BRUTALSTYLES = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility2":
                        ABILITY_PHYSIQUE = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility3":
                        ABILITY_TOUGHNESS = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility4":
                        ABILITY_DEXTROUSSTYLES = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility5":
                        ABILITY_ACCURACY = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility6":
                        ABILITY_SPEED = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility7":
                        ABILITY_SHOCK = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility8":
                        ABILITY_INFERNO = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility9":
                        ABILITY_TIMECONTROL = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility10":
                        ABILITY_BLADES = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility11":
                        ABILITY_VORTEX = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility12":
                        ABILITY_CHAOS = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility13":
                        HEROABILITY13 = int.Parse(valueNode.InnerText);
                        break;
                    case "HeroAbility14":
                        HEROABILITY14 = int.Parse(valueNode.InnerText);
                        break;

                    case "Experience0":
                        EXPERIENCE0 = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "TotalExperience0":
                        TOTAL_EXPERIENCE0 = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "Experience1":
                        EXPERIENCE1 = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "TotalExperience1":
                        TOTAL_EXPERIENCE1 = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "Experience2":
                        EXPERIENCE2 = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "TotalExperience2":
                        TOTAL_EXPERIENCE2 = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "Experience3":
                        EXPERIENCE3 = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "TotalExperience3":
                        TOTAL_EXPERIENCE3 = ReadInnerFloat(valueNode.InnerText);
                        break;

                    case "SecondsPlayedAsHenchman":
                        SECONDS_PLAYED_AS_HENCHMEN = ReadInnerFloat(valueNode.InnerText);
                        break;
                    case "CanUnlockAchievements":
                        CAN_UNLOCK_ACHIEVEMENTS = bool.Parse(valueNode.InnerText);
                        break;
                }
            }
        }
        private float ReadInnerFloat(string innerText)
        {
            //Split our string
            string[] tmpStr = innerText.Split(' ');
            //Return the first half parsed as a float
            return float.Parse(tmpStr[0]);
        }
        public void Write(EndianIO IO)
        {
            //Create a memory stream
            MemoryStream MS = new MemoryStream();
            //Initialize our XmlTextWriter
            XmlTextWriter xtw = new XmlTextWriter(MS, Encoding.ASCII);

            //Set our formatting to indent
            xtw.Formatting = Formatting.Indented;

            //Write our xml header
            xtw.WriteStartElement("HeroSave");

            //Write our values
            WriteValue(xtw, "Money", Money);
            WriteValue(xtw, "Gender", (int)Gender);
            WriteValue(xtw, "Morality", Morality);
            WriteValue(xtw, "Purity", Purity);
            WriteValue(xtw, "Renown", Renown);


            WriteValue(xtw, "HeroAbility0", HEROABILITY0);
            WriteValue(xtw, "HeroAbility1", ABILITY_BRUTALSTYLES);
            WriteValue(xtw, "HeroAbility2", ABILITY_PHYSIQUE);
            WriteValue(xtw, "HeroAbility3", ABILITY_TOUGHNESS);
            WriteValue(xtw, "HeroAbility4", ABILITY_DEXTROUSSTYLES);
            WriteValue(xtw, "HeroAbility5", ABILITY_ACCURACY);
            WriteValue(xtw, "HeroAbility6", ABILITY_SPEED);
            WriteValue(xtw, "HeroAbility7", ABILITY_SHOCK);
            WriteValue(xtw, "HeroAbility8", ABILITY_INFERNO);
            WriteValue(xtw, "HeroAbility9", ABILITY_TIMECONTROL);
            WriteValue(xtw, "HeroAbility10", ABILITY_BLADES);
            WriteValue(xtw, "HeroAbility11", ABILITY_VORTEX);
            WriteValue(xtw, "HeroAbility12", ABILITY_CHAOS);
            WriteValue(xtw, "HeroAbility13", HEROABILITY13);
            WriteValue(xtw, "HeroAbility14", HEROABILITY14);



            WriteValue(xtw, "Experience0", EXPERIENCE0);
            WriteValue(xtw, "TotalExperience0", TOTAL_EXPERIENCE0);
            WriteValue(xtw, "Experience1", EXPERIENCE1);
            WriteValue(xtw, "TotalExperience1", TOTAL_EXPERIENCE1);
            WriteValue(xtw, "Experience2", EXPERIENCE2);
            WriteValue(xtw, "TotalExperience2", TOTAL_EXPERIENCE2);
            WriteValue(xtw, "Experience3", EXPERIENCE3);
            WriteValue(xtw, "TotalExperience3", TOTAL_EXPERIENCE3);

            WriteValue(xtw, "SecondsPlayedAsHenchman", SECONDS_PLAYED_AS_HENCHMEN);
            WriteValue(xtw, "CanUnlockAchievements", CAN_UNLOCK_ACHIEVEMENTS);

            //Close our XML
            xtw.WriteEndElement();
            xtw.Close();
            //Get our XML Data
            string XML_Data = System.Text.ASCIIEncoding.ASCII.GetString(MS.ToArray());



            //Write our XML Size
            IO.Out.BaseStream.Position = 0x00;
            IO.Out.Write((uint)XML_Data.Length);
            //Write our XML
            IO.Out.WriteAsciiString(XML_Data,(int)XML_Data.Length);
            //Set our stream length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }
        private void WriteValue(XmlTextWriter xtw, string valName, int valVal)
        {
            //Write our value
            WriteValue(xtw, valName, "int", valVal.ToString());
        }
        private void WriteValue(XmlTextWriter xtw, string valName, bool valVal)
        {
            //Write our value
            WriteValue(xtw, valName, "bool", valVal.ToString());
        }
        private void WriteValue(XmlTextWriter xtw, string valName, float valVal)
        {
            //Get our bytes for this
            byte[] data = BitConverter.GetBytes(valVal);
            //Create our hex string
            string hex = "";
            //Loop for each byte
            foreach (byte b in data)
            {
                //Add our byte data
                string bStr = b.ToString("X");
                //While our length isnt 2
                while (bStr.Length != 2)
                    bStr = "0" + bStr;
                //Add it to our string
                hex = bStr + hex;
            }

            //Write our value
            WriteValue(xtw, valName, "float", valVal.ToString() + " " + hex);
        }
        private void WriteValue(XmlTextWriter xtw, string valName, string valType, string valVal)
        {
            //Write our value header
            xtw.WriteStartElement(valName);
            xtw.WriteAttributeString("type", valType);
            //Write our value
            xtw.WriteString(valVal);
            //Close our value bounds.
            xtw.WriteEndElement();
        }
        #endregion
    }
    public class Fable2PubInfo
    {
        /// <summary>
        /// Our IO to handle this save.
        /// </summary>

        #region Game Save Values

        //BASIC VALUES
        public string Name{get;set;}
        public int Gold_Balance { get; set; }
        public int Debt { get; set; }
        public int Rating { get; set; }
        public bool Is_Female { get; set; }
        public string Last_Played_Game_Name { get; set; }
        public string Last_Played_Variant { get; set; }
        public int Shared_Inventory_Mask { get; set; }
        public int Image_Index { get; set; }
        public int GUID { get; set; }
        public int Points_Earned0 { get; set; }
        public int Points_Earned1 { get; set; }
        public int Points_Earned2 { get; set; }
        public string Best_Tournament_Rank { get; set; }
        #endregion

        #region Constructor

        public Fable2PubInfo(EndianIO IO)
        {
            //Read our gamesave
            Read(IO);
        }

        #endregion

        #region Functions

        public void Read(EndianIO IO)
        {
            //Read our XML Data
            IO.In.BaseStream.Position = 0x02;
            string XML_Data = IO.In.ReadUnicodeString((int)(IO.In.BaseStream.Length - IO.In.BaseStream.Position));


            //Read the values from our XML

            //Initialize our XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            //Load our plugin
            xmlDoc.Load(new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(XML_Data)));
            //Get the root to read the header information from
            XmlElement root = xmlDoc.DocumentElement;
            //Loop for each child in the root
            foreach (XmlAttribute valueNode in root.Attributes)
            {
                //Check our value name
                switch (valueNode.Name)
                {
                    case "Name":
                        Name = valueNode.Value;
                        break;
                    case "GoldBalance":
                        Gold_Balance = int.Parse(valueNode.Value);
                        break;
                    case "Debt":
                        Debt = int.Parse(valueNode.Value);
                        break;
                    case "Rating":
                        Rating = int.Parse(valueNode.Value);
                        break;
                    case "IsFemale":
                        Is_Female = int.Parse(valueNode.Value) == 0x01;
                        break;
                    case "LastPlayedGameName":
                        Last_Played_Game_Name = valueNode.Value;
                        break;
                    case "LastPlayedVariant":
                        Last_Played_Variant = valueNode.Value;
                        break;
                    case "SharedInventoryMask":
                        Shared_Inventory_Mask = int.Parse(valueNode.Value);
                        break;
                    case "ImageIndex":
                        Image_Index = int.Parse(valueNode.Value);
                        break;
                    case "GUID":
                        GUID = int.Parse(valueNode.Value);
                        break;
                    case "PointsEarned0":
                        Points_Earned0 = int.Parse(valueNode.Value);
                        break;
                    case "PointsEarned1":
                        Points_Earned1 = int.Parse(valueNode.Value);
                        break;
                    case "PointsEarned2":
                        Points_Earned2 = int.Parse(valueNode.Value);
                        break;
                    case "BestTournamentRank":
                        Best_Tournament_Rank = valueNode.Value;
                        break;
                }
            }
        }
        public void Write(EndianIO IO)
        {
            //Create a memory stream
            MemoryStream MS = new MemoryStream();
            //Initialize our XmlTextWriter
            XmlTextWriter xtw = new XmlTextWriter(MS, Encoding.ASCII);

            //Set our formatting to indent
            xtw.Formatting = Formatting.Indented;

            //Write our xml header
            xtw.WriteStartElement("FablePubCharacter");
            xtw.WriteAttributeString("Name", Name);
            xtw.WriteAttributeString("GoldBalance", Gold_Balance.ToString());
            xtw.WriteAttributeString("Debt", Debt.ToString());
            xtw.WriteAttributeString("Rating", Rating.ToString());
            //If we are female
            if (Is_Female)
                xtw.WriteAttributeString("IsFemale", (1).ToString());
            else
                xtw.WriteAttributeString("IsFemale", (0).ToString());

            xtw.WriteAttributeString("LastPlayedGameName", Last_Played_Game_Name);
            xtw.WriteAttributeString("LastPlayedVariant", Last_Played_Variant);
            xtw.WriteAttributeString("SharedInventoryMask", Shared_Inventory_Mask.ToString());
            xtw.WriteAttributeString("ImageIndex", Image_Index.ToString());
            xtw.WriteAttributeString("GUID", GUID.ToString());

            xtw.WriteAttributeString("PointsEarned0", Points_Earned0.ToString());
            xtw.WriteAttributeString("PointsEarned1", Points_Earned1.ToString());
            xtw.WriteAttributeString("PointsEarned2", Points_Earned2.ToString());

            xtw.WriteAttributeString("BestTournamentRank", Best_Tournament_Rank);

            //Close our XML
            xtw.WriteEndElement();
            xtw.Close();
            //Get our XML Data
            string XML_Data = System.Text.ASCIIEncoding.ASCII.GetString(MS.ToArray());



            //Write our XML Size
            IO.Out.BaseStream.Position = 0x00;
            IO.Out.Write((ushort)0xFEFF);
            //Write our XML
            IO.Out.WriteUnicodeString(XML_Data,(int)XML_Data.Length);
            //Set our stream length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }

        #endregion
    }
}
