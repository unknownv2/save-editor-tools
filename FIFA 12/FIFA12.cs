using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FIFA;

namespace Horizon.PackageEditors.FIFA_12
{
    public partial class FIFA12 : EditorControl
    {
        //public static readonly string FID = "45410967";
        private FIFA12_SaveGame GameSave;
        private FIFA12_SaveGame.VirtualProPlayer Player;
        public FIFA12()
        {
            InitializeComponent();
            TitleID = FormID.FIFA12;
            //Set our title ID
            
            //Load our height items.
            LoadHeightItems();
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
                return false;

            this.GameSave = new FIFA12_SaveGame(IO, this.SettingAsString(156), this.SettingAsString(35));
 
            Player = GameSave.Player;

            // load player data/skills
            txtFirstName.Text = Player.FirstName;
            txtLastName.Text = Player.LastName;
            txtJerseyName.Text = Player.JerseyName;
            txtKnownAs.Text = Player.CommonName;

            intAggression.Value = Player.Aggression;
            intTacticalAwareness.Value = Player.TacticalAwareness;
            intPositioning.Value = Player.Positioning;
            intVision.Value = Player.Vision;

            var heights = Enum.GetValues(typeof(FIFA12_SaveGame.HeightIndex));
            int x = -1;
            while (Convert.ToInt32(heights.GetValue(++x)) != Player.Height) ;

            comboHeight.SelectedIndex = x;
            comboPreferredFoot.SelectedIndex = Player.PreferredFoot > 0 ? 1 : 0;
            intJerseyNum.Value = Player.JerseyNumber;

            // load physical skills
            intAcceleration.Value = Player.Acceleration;
            intSprintSpeed.Value = Player.SprintSpeed;
            intAgility.Value = Player.Agility;
            intBalance.Value = Player.Balance;
            intJumping.Value = Player.Jumping;
            intStamina.Value = Player.Stamina;
            intStrength.Value = Player.Strength;
            intReactions.Value = Player.Reactions;

            //load position skills
            intBallControl.Value = Player.BallControl;
            intCrossing.Value = Player.Crossing;
            intDribbling.Value = Player.Dribbling;
            intFinishing.Value = Player.Finishing;
            intFreeKickAccuracy.Value = Player.FreeKickAccuracy;
            intHeadingAccuracy.Value = Player.HeadingAccuracy;
            intLongPassing.Value = Player.LongPassing;
            intShortPassing.Value = Player.ShortPassing;
            intMarking.Value = Player.Marking;
            intShotPower.Value = Player.ShotPower;
            intPowerShotAccuracy.Value = Player.PowerShotAccuracy;
            intStandingTackle.Value = Player.StandingTackle;
            intSlidingTackle.Value = Player.SlidingTackle;
            intVolleys.Value = Player.Volleys;
            intCurve.Value = Player.Curve;
            intPenalties.Value = Player.Penalties;

            // load Goalkeeper skills
            intGKDiving.Value = Player.GK_Diving;
            intGKHandling.Value = Player.GK_Handling;
            intGKKicking.Value = Player.GK_Kicking;
            intGKReflexes.Value = Player.GK_Reflexes;
            intGKPositioning.Value = Player.GK_Positioning;

            return true;
        }

        public override void Save()
        {
            // load player data/skillset
            Player.FirstName = this.txtFirstName.Text;
            Player.LastName = txtLastName.Text;
            Player.JerseyName = txtJerseyName.Text;
            Player.CommonName = txtKnownAs.Text;

            Player.PreferredFoot = comboPreferredFoot.SelectedIndex;
            Player.JerseyNumber = intJerseyNum.Value;

            Player.Aggression = (byte)intAggression.Value;
            Player.TacticalAwareness = (byte)intTacticalAwareness.Value;
            Player.Positioning= (byte)intPositioning.Value;
            Player.Vision = (byte)intVision.Value;
            Player.Height = (int)((FIFA12_SaveGame.HeightIndex)(Enum.Parse(typeof(FIFA12_SaveGame.HeightIndex), "a" + comboHeight.SelectedItem.ToString().Replace("\' ", "_").Replace("\"", ""))));

            // set physical skills
            Player.Acceleration = (byte)intAcceleration.Value;
            Player.SprintSpeed = (byte)intSprintSpeed.Value;
            Player.Agility = (byte)intAgility.Value;
            Player.Balance = (byte)intBalance.Value;
            Player.Jumping = (byte)intJumping.Value;
            Player.Stamina = (byte)intStamina.Value;
            Player.Strength = (byte)intStrength.Value;
            Player.Reactions = (byte)intReactions.Value;

            // load position skills
            Player.BallControl = (byte)intBallControl.Value;
            Player.Crossing = (byte)intCrossing.Value;
            Player.Dribbling = (byte)intDribbling.Value;
            Player.Finishing = (byte)intFinishing.Value;
            Player.FreeKickAccuracy = (byte)intFreeKickAccuracy.Value;
            Player.HeadingAccuracy = (byte)intHeadingAccuracy.Value;
            Player.LongPassing = (byte)intLongPassing.Value;
            Player.ShortPassing = (byte)intShortPassing.Value;
            Player.Marking = (byte)intMarking.Value;
            Player.ShotPower = (byte)intShotPower.Value;
            Player.PowerShotAccuracy = (byte)intPowerShotAccuracy.Value;
            Player.StandingTackle = (byte)intStandingTackle.Value;
            Player.SlidingTackle = (byte)intSlidingTackle.Value;
            Player.Volleys = (byte)intVolleys.Value;
            Player.Curve = (byte)intCurve.Value;
            Player.Penalties = (byte)intPenalties.Value;

            // set goalkeeper settings
            Player.GK_Diving = (byte)intGKDiving.Value;
            Player.GK_Handling = (byte)intGKHandling.Value;
            Player.GK_Kicking = (byte)intGKKicking.Value;
            Player.GK_Reflexes = (byte)intGKReflexes.Value;
            Player.GK_Positioning = (byte)intGKPositioning.Value;

            this.GameSave.Write();
        }

        private void BtnClick_MaxPositionValues(object sender, EventArgs e)
        {
            MaxAllValues(this.vpPosSkillsGrp, null);
        }

        private void BtnClick_MaxPhysicalPlayerSkillsValues(object sender, EventArgs e)
        {
            MaxAllValues(this.vpPhysicalSkillsGrp, null);
        }

        private void BtnClick_MaxPlayerSkillValues(object sender, EventArgs e)
        {
            MaxAllValues(this.vpPlayerSkillsGrp, null);
        }

        private void LoadHeightItems()
        {
            //Clear our existing items
            comboHeight.Items.Clear();

            //Get the names of our enum items.
            string[] names = Enum.GetNames(typeof(FIFA12_SaveGame.HeightIndex));

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
    }
}
