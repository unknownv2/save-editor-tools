using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Fable_2
{
    public partial class Fable2 : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "4D5307F1";
        public Fable2HeroSave FABLE2_HEROSAVE { get; set; }
        public Fable2PubInfo FABLE2_PUBINFO { get; set; }
        public Fable2()
        {
            InitializeComponent();
            TitleID = FormID.Fable2;
            //Set our title ID
            
            //Set our maxes
            intMoney.MaxValue = 999999999;
            intMoney.MinValue = 0;
            intRenown.MaxValue = int.MaxValue;
            intRenown.MinValue = 0;
            floatMorality.MinValue = -1000;
            floatMorality.MaxValue = 1000;
            floatPurity.MinValue = -1000;
            floatPurity.MaxValue = 1000;
        }


        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file.
            if (!this.OpenStfsFile("herosave.bin"))
                return false;

            //Initialize our class
            FABLE2_HEROSAVE = new Fable2HeroSave(IO);

            //Open our file.
            if (!this.OpenStfsFile("Fable2PubInfo.xml"))
                return false;

            //Initialize our class
            FABLE2_PUBINFO = new Fable2PubInfo(IO);

            //Set our info
            intMoney.Value = FABLE2_HEROSAVE.Money;
            intRenown.Value = FABLE2_HEROSAVE.Renown;
            floatMorality.Value = FABLE2_HEROSAVE.Morality;
            floatPurity.Value = FABLE2_HEROSAVE.Purity;
            chkCanUnlockAchievements.Checked = FABLE2_HEROSAVE.CAN_UNLOCK_ACHIEVEMENTS;

            //Set our abilities
            intBrutalStyles.Value = FABLE2_HEROSAVE.ABILITY_BRUTALSTYLES;
            intPhysique.Value = FABLE2_HEROSAVE.ABILITY_PHYSIQUE;
            intToughness.Value = FABLE2_HEROSAVE.ABILITY_TOUGHNESS;

            intDextrousStyles.Value = FABLE2_HEROSAVE.ABILITY_DEXTROUSSTYLES;
            intAccuracy.Value = FABLE2_HEROSAVE.ABILITY_ACCURACY;
            intSpeed.Value = FABLE2_HEROSAVE.ABILITY_SPEED;

            intShock.Value = FABLE2_HEROSAVE.ABILITY_SHOCK;
            intInferno.Value = FABLE2_HEROSAVE.ABILITY_INFERNO;
            intTimeControl.Value = FABLE2_HEROSAVE.ABILITY_TIMECONTROL;
            intBlades.Value = FABLE2_HEROSAVE.ABILITY_BLADES;
            intVortex.Value = FABLE2_HEROSAVE.ABILITY_VORTEX;
            intChaos.Value = FABLE2_HEROSAVE.ABILITY_CHAOS;
            intForcePush.Value = FABLE2_HEROSAVE.HEROABILITY13;
            intRaiseDead.Value = FABLE2_HEROSAVE.HEROABILITY14;

            //Our file is read correctly.
            return true;
        }

        public override void Save()
        {
            //Set our info
            FABLE2_HEROSAVE.Money = intMoney.Value;
            FABLE2_PUBINFO.Gold_Balance = FABLE2_HEROSAVE.Money;
            FABLE2_HEROSAVE.Renown = intRenown.Value;
            FABLE2_HEROSAVE.Morality = (float)floatMorality.Value;
            FABLE2_HEROSAVE.Purity = (float)floatPurity.Value;
            FABLE2_HEROSAVE.CAN_UNLOCK_ACHIEVEMENTS = chkCanUnlockAchievements.Checked;
           
            //Set our abilities
            FABLE2_HEROSAVE.ABILITY_BRUTALSTYLES = intBrutalStyles.Value;
            FABLE2_HEROSAVE.ABILITY_PHYSIQUE = intPhysique.Value;
            FABLE2_HEROSAVE.ABILITY_TOUGHNESS = intToughness.Value;

            FABLE2_HEROSAVE.ABILITY_DEXTROUSSTYLES = intDextrousStyles.Value;
            FABLE2_HEROSAVE.ABILITY_ACCURACY = intAccuracy.Value;
            FABLE2_HEROSAVE.ABILITY_SPEED = intSpeed.Value;

            FABLE2_HEROSAVE.ABILITY_SHOCK = intShock.Value;
            FABLE2_HEROSAVE.ABILITY_INFERNO = intInferno.Value;
            FABLE2_HEROSAVE.ABILITY_TIMECONTROL = intTimeControl.Value;
            FABLE2_HEROSAVE.ABILITY_BLADES = intBlades.Value;
            FABLE2_HEROSAVE.ABILITY_VORTEX = intVortex.Value;
            FABLE2_HEROSAVE.ABILITY_CHAOS = intChaos.Value;
            FABLE2_HEROSAVE.HEROABILITY13 = intForcePush.Value;
            FABLE2_HEROSAVE.HEROABILITY14 = intRaiseDead.Value;
            //Open our file.
            this.OpenStfsFile("herosave.bin");
            //Save
            FABLE2_HEROSAVE.Write(IO);

            //Open our file.
            this.OpenStfsFile("Fable2PubInfo.xml");
            //Save
            FABLE2_PUBINFO.Write(IO);
        }

        private void btnMaxMoney_Click(object sender, EventArgs e)
        {
            //Set our max value
            intMoney.Value = intMoney.MaxValue;
        }

        private void btnMaxRenown_Click(object sender, EventArgs e)
        {
            //Set our max value
            intRenown.Value = intRenown.MaxValue;
        }

        private void btnGoodPerson_Click(object sender, EventArgs e)
        {
            floatMorality.Value = floatMorality.MaxValue;
            floatPurity.Value = floatPurity.MaxValue;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            floatMorality.Value = (floatMorality.MinValue + floatMorality.MaxValue) / 2;
            floatPurity.Value = (floatPurity.MinValue + floatPurity.MaxValue) / 2;
        }

        private void btnBadPerson_Click(object sender, EventArgs e)
        {
            floatMorality.Value = floatMorality.MinValue;
            floatPurity.Value = floatPurity.MinValue;
        }
        private void MaxAllCombos(DevComponents.DotNetBar.Controls.GroupPanel gp)
        {
            //Loop for each control in this groupbox
            foreach (Control c in gp.Controls)
            {
                //If this control is of type..
                if (c.GetType() == typeof(DevComponents.Editors.IntegerInput))
                {
                    //Set the value as the max
                    DevComponents.Editors.IntegerInput i = ((DevComponents.Editors.IntegerInput)c);
                    i.Value = i.MaxValue;
                }
            }   
        }
        private void btnMaxStrength_Click(object sender, EventArgs e)
        {
            MaxAllCombos(groupPanel1);
        }

        private void btnMaxSkill_Click(object sender, EventArgs e)
        {
            MaxAllCombos(groupPanel2);
        }

        private void btnMaxWill_Click(object sender, EventArgs e)
        {
            MaxAllCombos(groupPanel3);
        }
    }
}
