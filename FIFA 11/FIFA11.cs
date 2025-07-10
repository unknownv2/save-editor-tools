using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.FIFA_11
{
    public partial class FIFA11 : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "454108F3";

        private FIFA11Class FIFA11_Class { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public FIFA11()
        {
            InitializeComponent();
            TitleID = FormID.FIFA11;

            //Set our title ID
            

            //Load our height items.
            LoadHeightItems();
        }

        private void LoadHeightItems()
        {
            //Clear our existing items
            comboHeight.Items.Clear();

            //Get the names of our enum items.
            string[] names = Enum.GetNames(typeof(FIFA11Class.HeightIndex));

            //Loop for each name.
            foreach (string name in names)
            {
                //Add it
                comboHeight.Items.Add(name.Replace("_", "\' ").Replace("a", "") + "\"");
            }
        }

        public void MaxAllValues(Control panel, List<Control> excludeList)
        {
            //If our list is empty.. Create it
            if (excludeList == null)
                excludeList = new List<Control>();
            //If our panel isn't null
            if (panel != null)
            {
                //Loop through each control
                foreach (Control c in panel.Controls)
                {
                    //If our control isn't in our exclude list
                    if (!excludeList.Contains(c))
                    {
                        //Try to..
                        try
                        {
                            //Get our control type
                            if (c.GetType() == typeof(NumericUpDown))
                            {
                                NumericUpDown nud = (NumericUpDown)c;
                                nud.Value = nud.Maximum;
                            }
                            else if (c.GetType() == typeof(DevComponents.DotNetBar.Controls.Slider))
                            {
                                DevComponents.DotNetBar.Controls.Slider s = (DevComponents.DotNetBar.Controls.Slider)c;
                                s.Value = s.Maximum;
                            }
                            else
                            {
                                //Unknown? Might be a panel.. Lets try maxing this aswell, calls for recursiveness.. ;P
                                MaxAllValues(c, excludeList);
                            }


                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile(0))
                return false;

            //Init our class
            FIFA11_Class = new FIFA11Class(IO);

            //Set our general data
            txtFirstName.Text = FIFA11_Class.FirstName;
            txtLastName.Text = FIFA11_Class.LastName;
            txtKnownAs.Text = FIFA11_Class.KnownAs;
            txtKitName.Text = FIFA11_Class.KitName;
            intWeight.Value = FIFA11_Class.WeightPounds;
            comboHeight.SelectedIndex = new List<string>(Enum.GetNames(typeof(FIFA11Class.HeightIndex))).IndexOf(FIFA11_Class.HeightInches.ToString());
            comboDefaultFoot.SelectedIndex = (int)FIFA11_Class.DefaultFoot;

            //Set our Physical Data
            intAcceleration.Value = FIFA11_Class.Acceleration;
            intAgility.Value = FIFA11_Class.Agility;
            intBalance.Value = FIFA11_Class.Balance;
            intJumping.Value = FIFA11_Class.Jumping;
            intReactions.Value = FIFA11_Class.Reactions;
            intSprintSpeed.Value = FIFA11_Class.SprintSpeed;
            intStamina.Value = FIFA11_Class.Stamina;
            intStrength.Value = FIFA11_Class.Strength;

            intAggression.Value = FIFA11_Class.Aggression;
            intTacticalAwareness.Value = FIFA11_Class.TacticalAwareness;
            intMental.Value = FIFA11_Class.Mental;
            intVision.Value = FIFA11_Class.Vision;

            //Set our position data
            intBallControl.Value = FIFA11_Class.BallControl;
            intCrossing.Value = FIFA11_Class.Crossing;
            intDribbling.Value = FIFA11_Class.Dribbling;
            intFinishing.Value = FIFA11_Class.Finishing;
            intLongShots1.Value = FIFA11_Class.LongShots1;
            intHeadingAccuracy.Value = FIFA11_Class.HeadingAccuracy;
            intLongPassing.Value = FIFA11_Class.LongPassing;
            intShortPassing.Value = FIFA11_Class.ShortPassing;
            intMarking.Value = FIFA11_Class.Marking;
            intShotPower.Value = FIFA11_Class.ShotPower;
            intLongShots2.Value = FIFA11_Class.LongShots2;
            intStandingTackle.Value = FIFA11_Class.StandingTackle;
            intSlidingTackle.Value = FIFA11_Class.SlidingTackle;
            intVolleys.Value = FIFA11_Class.Volleys;

            //Stats
            intWinCount.Value = FIFA11_Class.WinCount;
            intLossCount.Value = FIFA11_Class.LossCount;
            intDrawCount.Value = FIFA11_Class.DrawCount;
            intGoalsAgainst.Value = FIFA11_Class.GoalsAgainst;
            intGoalsFor.Value = FIFA11_Class.GoalsFor;
            intGamesPlayed.Value = FIFA11_Class.GamesPlayed;
            intCleanSheetStreak.Value = FIFA11_Class.CleanSheetStreak;

            //Our file is read correctly.
            return true;
        }

        public override void Save()
        {
            //Set our general data
            FIFA11_Class.FirstName = txtFirstName.Text;
            FIFA11_Class.LastName = txtLastName.Text;
            FIFA11_Class.KnownAs = txtKnownAs.Text;
            FIFA11_Class.KitName = txtKitName.Text;
            FIFA11_Class.WeightPounds = (int)intWeight.Value;
            FIFA11_Class.HeightInches = ((FIFA11Class.HeightIndex)(Enum.Parse(typeof(FIFA11Class.HeightIndex), "a" + comboHeight.SelectedItem.ToString().Replace("\' ", "_").Replace("\"", ""))));
            FIFA11_Class.DefaultFoot = (FIFA11Class.DefaultFootIndex)comboDefaultFoot.SelectedIndex;

            //Set our Physical Data
            FIFA11_Class.Acceleration = (byte)intAcceleration.Value;
            FIFA11_Class.Agility = (byte)intAgility.Value;
            FIFA11_Class.Balance = (byte)intBalance.Value;
            FIFA11_Class.Jumping = (byte)intJumping.Value;
            FIFA11_Class.Reactions = (byte)intReactions.Value;
            FIFA11_Class.SprintSpeed = (byte)intSprintSpeed.Value;
            FIFA11_Class.Stamina = (byte)intStamina.Value;
            FIFA11_Class.Strength = (byte)intStrength.Value;


            FIFA11_Class.Aggression = (byte)intAggression.Value;
            FIFA11_Class.TacticalAwareness = (byte)intTacticalAwareness.Value;
            FIFA11_Class.Mental = (byte)intMental.Value;
            FIFA11_Class.Vision = (byte)intVision.Value;

            //Set our position data
            FIFA11_Class.BallControl = (byte)intBallControl.Value;
            FIFA11_Class.Crossing = (byte)intCrossing.Value;
            FIFA11_Class.Dribbling = (byte)intDribbling.Value;
            FIFA11_Class.Finishing = (byte)intFinishing.Value;
            FIFA11_Class.LongShots1 = (byte)intLongShots1.Value;
            FIFA11_Class.HeadingAccuracy = (byte)intHeadingAccuracy.Value;
            FIFA11_Class.LongPassing = (byte)intLongPassing.Value;
            FIFA11_Class.ShortPassing = (byte)intShortPassing.Value;
            FIFA11_Class.Marking = (byte)intMarking.Value;
            FIFA11_Class.ShotPower = (byte)intShotPower.Value;
            FIFA11_Class.LongShots2 = (byte)intLongShots2.Value;
            FIFA11_Class.StandingTackle = (byte)intStandingTackle.Value;
            FIFA11_Class.SlidingTackle = (byte)intSlidingTackle.Value;
            FIFA11_Class.Volleys = (byte)intVolleys.Value;

            //Stats
            FIFA11_Class.WinCount = (int)intWinCount.Value;
            FIFA11_Class.LossCount = (int)intLossCount.Value;
            FIFA11_Class.DrawCount = (int)intDrawCount.Value;
            FIFA11_Class.GoalsAgainst = (int)intGoalsAgainst.Value;
            FIFA11_Class.GoalsFor = (int)intGoalsFor.Value;
            FIFA11_Class.GamesPlayed = (int)intGamesPlayed.Value;
            FIFA11_Class.CleanSheetStreak = (int)intCleanSheetStreak.Value;

            //Use our class to save
            FIFA11_Class.Write(checkUnlockAll.Checked);
        }

        private void cmdMaxPhysicalData_Click(object sender, EventArgs e)
        {
            MaxAllValues(groupPanel1, null);
        }

        private void cmdMaxPositionSkills_Click(object sender, EventArgs e)
        {
            MaxAllValues(groupPanel2, null);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            MaxAllValues(groupPanel4, new List<Control>(new Control[] { intWeight }));
        }
    }
}
