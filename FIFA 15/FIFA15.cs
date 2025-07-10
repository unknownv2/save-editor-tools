using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FIFA;

namespace Horizon.PackageEditors.FIFA_15
{
    public partial class FIFA15 : EditorControl
    {
        private FIFA15_GameSave GameSave;
        private FIFA15_GameSave.VirtualProPlayer Player;
        public FIFA15()
        {
            InitializeComponent();
            TitleID = FormID.FIFA15;

            LoadHeightItems();
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            GameSave = new FIFA15_GameSave(IO, "Fifa generic error message saving");
            Player = GameSave.Player;

            // load player data/skills
            txtFirstName.Text = Player.FirstName;
            txtLastName.Text = Player.LastName;
            txtJerseyName.Text = Player.JerseyName;
            txtKnownAs.Text = Player.CommonName;

            var heights = Enum.GetValues(typeof(FIFA14_GameSave.HeightIndex));
            var x = -1;
            for (var i = 0; i < heights.Length; i++)
            {
                var heightA = Convert.ToInt32(heights.GetValue(i));
                var heightB = i != (heights.Length - 1) ? Convert.ToInt32(heights.GetValue(i + 1)) : heightA + 1;
                if (Player.Height >= heightA && Player.Height < heightB)
                {
                    x = i;
                    break;
                }
            }
            if (x == -1)
            {
                Functions.UI.errorBox("Error: Could not determine player height!");
                return false;
            }

            comboHeight.SelectedIndex = x;
            comboPreferredFoot.SelectedIndex = Player.PreferredFoot > 0 ? 1 : 0;
            intJerseyNum.Value = Player.JerseyNumber;

            // load Goalkeeper skills
            intGKDiving.Value = Player.GK_Diving;
            intGKHandling.Value = Player.GK_Handling;
            intGKKicking.Value = Player.GK_Kicking;
            intGKReflexes.Value = Player.GK_Reflexes;
            intGKPositioning.Value = Player.GK_Positioning;

            // Load Ball Skills
            intBallControl.Value = Player.BallControl;
            intCurve.Value = Player.Curve;
            intDribbling.Value = Player.Dribbling;

            // Load Defence Skills
            intMarking.Value = Player.Marking;
            intSlidingTackle.Value = Player.SlidingTackle;
            intStandingTackle.Value = Player.StandingTackle;

            // Load Mental Skills
            intAggression.Value = Player.Aggression;
            intAttackPos.Value = Player.AttackPositioning;
            intInterceptions.Value = Player.Interceptions;
            intVision.Value = Player.Vision;

            // Load Passing Skills
            intCrossing.Value = Player.Crossing;
            intLongPassing.Value = Player.LongPassing;
            intShortPassing.Value = Player.ShortPassing;

            // Load Physical Skills
            intAcceleration.Value = Player.Acceleration;
            intAgility.Value = Player.Agility;
            intBalance.Value = Player.Balance;
            intJumping.Value = Player.Jumping;
            intReactions.Value = Player.Reactions;
            intSprintSpeed.Value = Player.SprintSpeed;
            intStamina.Value = Player.Stamina;

            // Load Shooting Skills
            intFinishing.Value = Player.Finishing;
            intFreeKickAccuracy.Value = Player.FreeKickAccuracy;
            intHeadingAccuracy.Value = Player.HeadingAccuracy;
            intLongShots.Value = Player.LongShots;
            intPenalties.Value = Player.Penalties;
            intShotPower.Value = Player.ShotPower;
            intVolleys.Value = Player.Volleys;

            return true;
        }
        public override void Save()
        {
            Player.FirstName = txtFirstName.Text;
            Player.LastName = txtLastName.Text;
            Player.JerseyName = txtJerseyName.Text;
            Player.CommonName = txtKnownAs.Text;

            Player.PreferredFoot = comboPreferredFoot.SelectedIndex;
            //Player.JerseyNumber = intJerseyNum.Value;
            Player.Height = (int)((FIFA13_GameSave.HeightIndex)(Enum.Parse(typeof(FIFA13_GameSave.HeightIndex), "a" + comboHeight.SelectedItem.ToString().Replace("\' ", "_").Replace("\"", ""))));

            // Set Ball Skills
            Player.BallControl = (byte)intBallControl.Value;
            Player.Curve = (byte)intCurve.Value;
            Player.Dribbling = (byte)intDribbling.Value;

            // Set Defence Skills
            Player.Marking = (byte)intMarking.Value;
            Player.SlidingTackle = (byte)intSlidingTackle.Value;
            Player.StandingTackle = (byte)intStandingTackle.Value;

            // Set Mental Skills
            Player.Aggression = (byte)intAggression.Value;
            Player.AttackPositioning = (byte)intAttackPos.Value;
            Player.Interceptions = (byte)intInterceptions.Value;
            Player.Vision = (byte)intVision.Value;

            // Set Passing Skills
            Player.Crossing = (byte)intCrossing.Value;
            Player.LongPassing = (byte)intLongPassing.Value;
            Player.ShortPassing = (byte)intShortPassing.Value;

            // Set Physical Skills
            Player.Acceleration = (byte)intAcceleration.Value;
            Player.Agility = (byte)intAgility.Value;
            Player.Balance = (byte)intBalance.Value;
            Player.Jumping = (byte)intJumping.Value;
            Player.Reactions = (byte)intReactions.Value;
            Player.SprintSpeed = (byte)intSprintSpeed.Value;
            Player.Stamina = (byte)intStamina.Value;

            // Set Shooting Skills
            Player.Finishing = (byte)intFinishing.Value;
            Player.FreeKickAccuracy = (byte)intFreeKickAccuracy.Value;
            Player.HeadingAccuracy = (byte)intHeadingAccuracy.Value;
            Player.LongShots = (byte)intLongShots.Value;
            Player.Penalties = (byte)intPenalties.Value;
            Player.ShotPower = (byte)intShotPower.Value;
            Player.Volleys = (byte)intVolleys.Value;

            // Set Goalkeeper Skills
            Player.GK_Diving = (byte)intGKDiving.Value;
            Player.GK_Handling = (byte)intGKHandling.Value;
            Player.GK_Kicking = (byte)intGKKicking.Value;
            Player.GK_Reflexes = (byte)intGKReflexes.Value;
            Player.GK_Positioning = (byte)intGKPositioning.Value;

            GameSave.Save();
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

        private void CmdMaxPlayerSkillsClick(object sender, EventArgs e)
        {
            MaxAllValues(vpPlayerSkillsGrp, null);
        }

        private void CmdMaxPlayerSkillsAttribute1Click(object sender, EventArgs e)
        {
            MaxAllValues(vpBallSkillsGrp, null);
            MaxAllValues(vpDefenceGrp, null);
            MaxAllValues(vpMentalGrp, null);
            MaxAllValues(vpPassingGrp, null);
        }

        private void CmdMaxPlayerSkillsAttribute2Click(object sender, EventArgs e)
        {
            MaxAllValues(vpPhysicalGrp, null);
            MaxAllValues(vpShootingGrp, null);
        }
    }
}
