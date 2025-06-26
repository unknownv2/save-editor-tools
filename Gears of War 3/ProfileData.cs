using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horizon.Functions;
using System.IO;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;

namespace Horizon.PackageEditors.Gears_of_War_3
{
    public partial class ProfileData : EditorControl
    {
        //public static readonly string FID = "4D5308AP";
        public ProfileData()
        {
            PlayerData.InitializeVars();

            InitializeComponent();
            TitleID = FormID.GearsOfWar3;

            populateComboBox(cbCog, typeof(COGCharacter), 0);
            populateComboBox(cbLocust, typeof(LocusCharacter), 0);

            populateComboBox(cbRifle, typeof(AssaultRifle), 0);
            populateComboBox(cbShotgun, typeof(Shotgun), 0);

            populateComboBox(cbLancer, typeof(SkinID), 0);
            populateComboBox(cbHammerburst, typeof(SkinID), 2000);
            populateComboBox(cbRetroLancer, typeof(SkinID), 1000);
            populateComboBox(cbGnasher, typeof(SkinID), 3000);
            populateComboBox(cbSawedOff, typeof(SkinID), 4000);

            cbMedal.Items.Add(CreateMedalItem("- Not Selected -", (EntryID)(-1)));
            cbTitle.Items.Add(CreateMedalItem("- Not Selected -", (EntryID)(-1)));

            string[] medalTiers = new string[]
            {
                "Bronze",
                "Silver",
                "Gold",
                "Onyx"
            };

            foreach (PlayerData.MedalData medal in PlayerData.Medals)
            {
                if (medal.EntryIDs.Length == 1)
                {
                    cbMedal.Items.Add(CreateMedalItem(medal.Name, medal.EntryIDs[0]));
                    cbTitle.Items.Add(CreateMedalItem(medal.Titles[0], medal.EntryIDs[0]));
                }
                else
                {
                    for (int x = 0; x < medal.EntryIDs.Length; x++)
                    {
                        cbMedal.Items.Add(CreateMedalItem(string.Format("{0} - {1}", medal.Name, medalTiers[x]), medal.EntryIDs[x]));
                        cbTitle.Items.Add(CreateMedalItem(medal.Titles[x], medal.EntryIDs[x]));
                    }
                }
            }

            cbMedal.Sorted = true;
            cbTitle.Sorted = true;

            cbMedal.SelectedIndex = 0;
            cbTitle.SelectedIndex = 0;
        }

        private static ComboBoxItem CreateMedalItem(string name, EntryID medalId)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Text = name.Replace("SawedOff", "Sawed-off");
            item.Tag = (int)medalId;
            return item;
        }

        private static void populateComboBox(ComboBoxEx cb, Type enumType, int idAddition)
        {
            string[] values = Enum.GetNames(enumType);
            Array keys = Enum.GetValues(enumType);

            cb.Items.Clear();
            for (int x = 0; x < values.Length; x++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = values[x].Replace('_', ' ').Replace("SawedOff", "Sawed-off");
                int enumValue = (int)keys.GetValue(x);
                item.Tag = (enumValue == -1) ? -1 : enumValue + idAddition;
                cb.Items.Add(item);
            }
            cb.SelectedIndex = 0;
        }

        private enum COGCharacter
        {
            Marcus_Fenix = 0,
            Augustus_Cole = 1,
            Damon_Baird = 2,
            Dominic_Santiago = 3,
            Clayton_Carmine = 4,
            Dizzy_Wallin = 5,
            Samantha_Byrne = 6,
            Chairman_Prescott = 7,
            Anya_Stroud = 8,
            Jace_Stratton = 9,
            Cole_Train = 10,
            Onyx_Guard = 11,
            Aaron_Griffin = 12,
            Bernadette_Mataki = 13,
            Commando_Dom = 14,
            Superstar_Cole = 15,
            Unarmored_Marcus = 16,
            Mechanic_Baird = 18,
            Anthony_Carmine = 19,
            Classic_Marcus = 21,
            Classic_Baird = 22,
            Classic_Dom = 23,
            Classic_Cole = 24,
            COG_Gear = 25,
            Golden_Gear = 26,
            Big_Rig_Dizzy = 27,
            Civilian_Anya = 31,
            Adam_Fenix = 32,
            Victor_Hoffman = 33,
            Benjamin_Carmine = 34
        }

        private enum LocusCharacter
        {
            Drone = 0,
            Sniper = 1,
            Grenadier_Elite = 2,
            Grenadier = 3,
            Theron_Guard = 4,
            Beast_Rider = 5,
            Flame_Grenadier = 6,
            Kantus = 7,
            Myrrah = 8,
            Savage_Grenadier = 9,
            Savage_Drone = 10,
            Savage_Theron_Guard = 11,
            Savage_Theron = 13,
            Savage_Kantus = 14,
            Savage_Grenadier_Elite = 15,
            Miner = 18,
            Spotter = 19,
            Golden_Miner = 20,
            Hunter = 21,
            Hunter_Elite = 22,
            Golden_Hunter = 23,
            General_Raam = 25
        }

        private enum AssaultRifle
        {
            Hammerburst = 10,
            Lancer = 13,
            Retro_Lancer = 37
        }

        private enum Shotgun
        {
            Gnasher_Shotgun = 8,
            SawedOff_Shotgun = 36
        }

        private enum SkinID
        {
            Never_Set = -1,
            None = 0,
            Gold = 1,
            Chrome = 2,
            Crimson_Omen = 3,
            Tiger_Stripe = 4,
            Desert_Digital = 6,
            Deadly_Cute = 7,
            Garish = 8,
            Blood = 9,
            Team_Distress = 10,
            Flower = 11,
            Haze = 12,
            Tribal = 13,
            Urban_Digital = 14,
            Xbox = 15,
            Epic = 20,
            Flame = 21,
            Onyx = 22,
            Electric = 23,
            Imulsion = 24,
            Ocean = 25,
            Oil_Slick = 26,
            Infected_Omen = 27,
            Gold_Omen = 28,
            Team_Insignia = 29,
            Desert_Camo = 30,
            Arctic_Camo = 31,
            Jungle_Digital = 32,
            Team_Pulse = 33,
            Rainbow = 34,
            Team_Metal = 35,
            Thunderstorm = 36,

            DLC_Childs_Play = 50,
            DLC_Liquid_Metal = 55,
        }

        private PlayerStorage Stats;
        public override bool Entry()
        {
            if (!loadAllTitleSettings(EndianType.BigEndian))
                return false;

            Stats = new PlayerStorage(IO, Account.Info.XuidOnline, true);

            /*intCog.Value = Stats[EntryID.Selected_COG_Character];
            intLocust.Value = Stats[EntryID.Selected_Locust_Character];

            intRifle.Value = Stats[EntryID.Selected_Assault_Rifle];
            intShotgun.Value = Stats[EntryID.Selected_Shotgun];

            intLancer.Value = Stats[EntryID.Lancer_Skin];
            intHammerburst.Value = Stats[EntryID.Hammerburst_Skin];
            intRetroLancer.Value = Stats[EntryID.Retro_Lancer_Skin];
            intGnasher.Value = Stats[EntryID.Gnasher_Shotgun_Skin];
            intSawedOff.Value = Stats[EntryID.SawedOff_Shotgun_Skin];*/


            setComboBoxSelection(cbCog, Stats[EntryID.Selected_COG_Character]);
            setComboBoxSelection(cbLocust, Stats[EntryID.Selected_Locust_Character]);

            setComboBoxSelection(cbRifle, Stats[EntryID.Selected_Assault_Rifle]);
            setComboBoxSelection(cbShotgun, Stats[EntryID.Selected_Shotgun]);

            setComboBoxSelection(cbLancer, Stats[EntryID.Lancer_Skin]);
            setComboBoxSelection(cbHammerburst, Stats[EntryID.Hammerburst_Skin]);
            setComboBoxSelection(cbRetroLancer, Stats[EntryID.Retro_Lancer_Skin]);
            setComboBoxSelection(cbGnasher, Stats[EntryID.Gnasher_Shotgun_Skin]);
            setComboBoxSelection(cbSawedOff, Stats[EntryID.SawedOff_Shotgun_Skin]);

            setComboBoxSelection(cbMedal, Stats[EntryID.Selected_Medal]);
            setComboBoxSelection(cbTitle, Stats[EntryID.Selected_Title]);

            return true;
        }

        private static void setComboBoxSelection(ComboBoxEx cb, int value)
        {
            for (int x = 0; x < cb.Items.Count; x++)
            {
                ComboBoxItem item = (ComboBoxItem)cb.Items[x];
                if ((int)item.Tag == value)
                {
                    cb.SelectedIndex = x;
                    return;
                }
            }
            ComboBoxItem newItem = new ComboBoxItem();
            newItem.Text = string.Format("Unknown DLC [{0}]", value);
            newItem.Tag = value;
            cb.Items.Add(newItem);
            setComboBoxSelection(cb, value);
        }

        private int getComboBoxSelection(ComboBoxEx cb)
        {
            return (int)((ComboBoxItem)cb.Items[cb.SelectedIndex]).Tag;
        }

        public override void Save()
        {
            Stats[EntryID.Selected_COG_Character] = getComboBoxSelection(cbCog);
            Stats[EntryID.Selected_Locust_Character] = getComboBoxSelection(cbLocust);

            Stats[EntryID.Selected_Assault_Rifle] = getComboBoxSelection(cbRifle);
            Stats[EntryID.Selected_Shotgun] = getComboBoxSelection(cbShotgun);

            Stats[EntryID.Lancer_Skin] = getComboBoxSelection(cbLancer);
            Stats[EntryID.Hammerburst_Skin] = getComboBoxSelection(cbHammerburst);
            Stats[EntryID.Retro_Lancer_Skin] = getComboBoxSelection(cbRetroLancer);
            Stats[EntryID.Gnasher_Shotgun_Skin] = getComboBoxSelection(cbGnasher);
            Stats[EntryID.SawedOff_Shotgun_Skin] = getComboBoxSelection(cbSawedOff);

            Stats[EntryID.Selected_Medal] = getComboBoxSelection(cbMedal);
            Stats[EntryID.Selected_Title] = getComboBoxSelection(cbTitle);

            /*Stats[EntryID.Selected_COG_Character] = intCog.Value;
            Stats[EntryID.Selected_Locust_Character] = intLocust.Value;

            Stats[EntryID.Selected_Assault_Rifle] = intRifle.Value;
            Stats[EntryID.Selected_Shotgun] = intShotgun.Value;

            Stats[EntryID.Lancer_Skin] = intLancer.Value;
            Stats[EntryID.Hammerburst_Skin] = intHammerburst.Value;
            Stats[EntryID.Retro_Lancer_Skin] = intRetroLancer.Value;
            Stats[EntryID.Gnasher_Shotgun_Skin] = intGnasher.Value;
            Stats[EntryID.SawedOff_Shotgun_Skin] = intSawedOff.Value;*/

            Stats[EntryID.Profile_SyncID]++;

            writeAllTitleSettings(Stats.ToArray(true));
        }

        private void cmdNotice_Click(object sender, EventArgs e)
        {
            UI.messageBox("1. Characters that are not normally obtainable in-game will APPEAR to be Marcus Fenix in every in-game menu.\n\n"
                + "2. GOING TO THE OPTIONS MENU WILL RESET THE CHARACTER TO MARCUS FENIX.\n\n"
                + "3. Any skin with DLC before its name NEEDS certain DLC for it to work.\n\n"
                + "4. Be sure to sign out or go to the dashboard before you load your modded profile.",
                "Gears of War 3 Notice", MessageBoxIcon.Information);
        }
    }
}
