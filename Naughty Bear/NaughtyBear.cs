using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Horizon.PackageEditors.Naughty_Bear
{
    public partial class NaughtyBear : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "464F07D8";

        public string[] LEVEL_NAMES_GAME_ORDER = {
                                        "Episode 1: The Party",
                                        "Episode 2: Top Teddy",
                                        "Episode 3: Big Ted is Watching",
                                        "Episode 4: Night of the Living Ted",
                                        "Episode 6: R153 0F R0B0-B34R",
                                        "Episode 7: When Aliens Attack",
                                        "Episode 5: The Oil Baron's Ball",
                                        "Unknown",
                                        "Unknown",
                                        "Unknown",
                                        "Unknown",
                                        "1-1 Killer: Party Massacre",
                                        "1-2 Friendly: The Peace Loving Party",
                                        "1-3 Insanity: The Craziest Party Ever",
                                        "1-4 Top Hat: Bring Your Own Hat",
                                        "2-1 Untouchable: Top Dodger",
                                        "2-2 Speed Run: Race of the Top",
                                        "2-3 Killer: Top Assassin",
                                        "2-4 Top Hat: Doubletop Teddy",
                                        "3-1 Killer: Big Ted is Struggling!",
                                        "3-2 Invisible: Counter-Espionage",
                                        "3-3 Insanity: Big Ted is Deranged!",
                                        "3-4 Top Hat: The Army's Secret Weapon",
                                        "4-1 Untouchable: Night of the Dodging Ted",
                                        "4-2 Speed Run: The Un-ted Hustle",
                                        "4-3 Killer: Zombear Buster",
                                        "4-4 Top Hat: The Reanimating Headpieces",//26,000
                                        "6-1 Invisible: R4D4R J4MM3R",
                                        "6-2 Friendly: NO F1GH71NG",
                                        "6-3 Killer: TR0UBL3-5H0073R",
                                        "6-4 Top Hat: PR0J3C7 70P-H47",
                                        "7-1 Speed Run: Beyond the Speed of Light",
                                        "7-2 Killer: Alien Exterminator",
                                        "7-3 Insanity: Space Madness!",
                                        "7-4 Top Hat: Outer-Space Chic",
                                        "5-1 Speed Run: Oil Race",
                                        "5-2 Friendly: The Amicable Baron",
                                        "5-3 Killer: The Butcher's Ball",
                                        "5-4 Top Hat: The Baron's Brand New Hat",
                                      };

        public string[] LEVEL_NAMES = {
                                    "Episode 1: The Party",
                                    "1-1 Killer: Party Massacre",
                                    "1-2 Friendly: The Peace Loving Party",
                                    "1-3 Insanity: The Craziest Party Ever",
                                    "1-4 Top Hat: Bring Your Own Hat",
                                    "Episode 2: Top Teddy",
                                    "2-1 Untouchable: Top Dodger",
                                    "2-2 Speed Run: Race of the Top",
                                    "2-3 Killer: Top Assassin",
                                    "2-4 Top Hat: Doubletop Teddy",
                                    "Episode 3: Big Ted is Watching",
                                    "3-1 Killer: Big Ted is Struggling!",
                                    "3-2 Invisible: Counter-Espionage",
                                    "3-3 Insanity: Big Ted is Deranged!",
                                    "3-4 Top Hat: The Army's Secret Weapon",
                                    "Episode 4: Night of the Living Ted",
                                    "4-1 Untouchable: Night of the Dodging Ted",
                                    "4-2 Speed Run: The Un-ted Hustle",
                                    "4-3 Killer: Zombear Buster",
                                    "4-4 Top Hat: The Reanimating Headpieces",
                                    "Episode 5: The Oil Baron's Ball",
                                    "5-1 Speed Run: Oil Race",
                                    "5-2 Friendly: The Amicable Baron",
                                    "5-3 Killer: The Butcher's Ball",
                                    "5-4 Top Hat: The Baron's Brand New Hat",
                                    "Episode 6: R153 0F R0B0-B34R",
                                    "6-1 Invisible: R4D4R J4MM3R",
                                    "6-2 Friendly: NO F1GH71NG",
                                    "6-3 Killer: TR0UBL3-5H0073R",
                                    "6-4 Top Hat: PR0J3C7 70P-H47",
                                    "Episode 7: When Aliens Attack",
                                    "7-1 Speed Run: Beyond the Speed of Light",
                                    "7-2 Killer: Alien Exterminator",
                                    "7-3 Insanity: Space Madness!",
                                    "7-4 Top Hat: Outer-Space Chic"
                                      };
        public int size { get; set; }
        public List<NaughtyBearUnlockEntry> LEVEL_SCORES;

        /// <summary>
        /// Our default constructor.
        /// </summary>
        public NaughtyBear()
        {
            InitializeComponent();
            TitleID = FormID.NaughtyBear;
            //Set our title ID
            
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile("SCORES.DAT"))
                return false;

            //Clear our list
            lstUnlockedLevels.Items.Clear();

            //Read our first value(1)
            IO.In.ReadInt32();
            //Read our checksum
            IO.In.ReadInt32();

            //Read our size
            size = IO.In.ReadInt32() - 4;

            //Create our level score array
            LEVEL_SCORES = new List<NaughtyBearUnlockEntry>();

            //Loop for our size
            for (int i = 0; i < size; i += 8)
            {
                //Get our index
                int index = i / 8;

                //Read our entry
                NaughtyBearUnlockEntry nbue = new NaughtyBearUnlockEntry();
                nbue.Read(IO);

                //Set our level score
                LEVEL_SCORES.Add(nbue);
            }

            
            foreach (string level_name in LEVEL_NAMES)
            {
                //Create our listview item
                ListViewItem lvi = new ListViewItem(level_name);
                List<string> ye = new List<string>();

                lvi.Checked = LEVEL_SCORES[GetIndexOfLevel(level_name)].Unlocked;

                lstUnlockedLevels.Items.Add(lvi);
            }
            //Read our spare int
            IO.In.ReadInt32();

            //Our file is read correctly.
            return true;
        }

        public override void Save()
        {
            //Go to the beginning
            IO.Out.BaseStream.Position = 0;

            //Write our first value
            IO.Out.Write((int)1);

            //Write our dummy checksum, we'll get back to this later
            IO.Out.Write((int)-1);

            //Write our length
            IO.Out.Write(size + 4);

            //Create our total score
            int totalScore = 0;

            //Loop for every level
            for (int i = 0; i < LEVEL_SCORES.Count; i++)
            {
                //Write our struct
                LEVEL_SCORES[i].Write(IO);

                //Add to our total score
                try { totalScore += LEVEL_SCORES[i].Value; }
                catch { totalScore = int.MaxValue; }
            }

            //Write our spare int
            IO.Out.Write(totalScore);

            //Set our stream length.
            IO.Stream.SetLength(IO.Out.BaseStream.Position);


            //Equate our checksum
            IO.In.BaseStream.Position = 0x0C;
            uint checksum = Checksum.CRC32.Calculate(IO.In.ReadBytes(IO.In.BaseStream.Length - IO.In.BaseStream.Position));
            //Go to our checksum offset and write the checksum
            IO.Out.BaseStream.Position = 4;
            IO.Out.Write(checksum);
        }
        private int GetIndexOfLevel(string levelName)
        {
            return new List<string>(LEVEL_NAMES_GAME_ORDER).IndexOf(levelName);
        }
        private void lstUnlockedLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If we have a value selected
            if(lstUnlockedLevels.SelectedItems.Count == 1)
                //Set our value
                intLevelScore.Value = LEVEL_SCORES[GetIndexOfLevel(lstUnlockedLevels.SelectedItems[0].Text)].Value;
        }

        private void intLevelScore_ValueChanged(object sender, EventArgs e)
        {
            //Set it in our value array
            if (lstUnlockedLevels.SelectedItems.Count == 1)
                LEVEL_SCORES[GetIndexOfLevel(lstUnlockedLevels.SelectedItems[0].Text)].Value = intLevelScore.Value;
        }

        public class NaughtyBearUnlockEntry
        {
            public bool Unlocked { get; set; }
            public int Value { get; set; }
            public void Read(EndianIO IO)
            {
                Unlocked = IO.In.ReadInt32() == 1;
                Value = IO.In.ReadInt32();
            }
            public void Write(EndianIO IO)
            {
                IO.Out.Write(Unlocked ? (int)1 : (int)0);
                IO.Out.Write(Value);
            }
        }

        private void lstUnlockedLevels_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(e.Item != null)
                if (!string.IsNullOrEmpty(e.Item.Text))
                {
                    LEVEL_SCORES[GetIndexOfLevel(e.Item.Text)].Unlocked = e.Item.Checked;
                }
        }

        private void cmdMaxAmmo1_Click(object sender, EventArgs e)
        {
            //Loop for each item
            for (int i = 0; i < lstUnlockedLevels.Items.Count; i++)
            {
                //Get our level name
                string levelName = lstUnlockedLevels.Items[i].Text;
                lstUnlockedLevels.Items[i].Checked = true;
                //Get our level entry
                NaughtyBearUnlockEntry nbue = LEVEL_SCORES[GetIndexOfLevel(levelName)];
                nbue.Value = int.MaxValue;
            }
            intLevelScore.Value = int.MaxValue;
        }
    }
}
