using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Bastion;

namespace Horizon.PackageEditors.Bastion
{
    public partial class Bastion : EditorControl
    {
        //public static readonly string FID = "58410B66";
        public Bastion()
        {
            InitializeComponent();
            TitleID = FormID.Bastion;
            

            foreach (string weapon in weaponList)
                comboWeapon.Items.Add(weapon);
        }

        string[] weaponList = new string[] {
            "HealthPotion",
            "TouchdownSlam",
            "LevelUp",
            "PlayerOnHitWeapon",
            "PlayerOnKillWeapon",
            "PlayerDeath",
            "PlayerCounter",
            "CounterForceNova",
            "PlayerBlock",
            "PlayerRangedBlock",
            "PlayerRangedCounter",
            "PlayerLastStand",
            "RockFall",
            "RockFallLight",
            "RockFallHeavy",
            "RockFallLong",
            "RockStorm",
            "IceFall",
            "IceFallLong",
            "IceFallShort",
            "ScumPuddle",
            "InstantDamage",
            "InvisibleSlow",
            "InvisibleSlowStrong",
            "InvisibleSlowFinal",
            "InvisibleSlowFinal2",
            "InvisibleRamSlow",
            "InvisibleFullArmor",
            "InvisibleReflection",
            "BlankWeapon",
            "Hammer",
            "Hammer_Uppercut",
            "Hammer_Overhead",
            "Hammer_Charge",
            "Spear",
            "SpearThrow",
            "RamPound",
            "Machete",
            "MacheteThrow",
            "Flamethrower",
            "Longbow",
            "Repeater",
            "Revolvers",
            "Rifle",
            "Shotgun",
            "Mortar",
            "Cannon",
            "RamSwipe",
            "ShieldBash",
            "ShieldHold",
            "RamShieldBash",
            "RamShieldHold",
            "BlankDefense",
            "Mine",
            "Grenade",
            "PlayerAreaRoot",
            "Shield_SummonSquirt",
            "PlayerDopplewalk",
            "PlayerFullDeflection",
            "Hammer_Whirlwind",
            "Hammer_GroundPound",
            "Repeater_TwirlShot",
            "Repeater_TranqDart",
            "Longbow_BouncingShot",
            "Longbow_ArrowStorm",
            "Machete_BladeStorm",
            "Machete_GhostBlade",
            "Rifle_LaserShot",
            "Rifle_StickyBomb",
            "Flamethrower_Whirlwind",
            "Flamethrower_Nova",
            "Shotgun_BulletRain",
            "Shotgun_FullAuto",
            "Revolvers_MagnumShot",
            "Revolvers_FullAuto",
            "Mortar_SummonTurret",
            "Mortar_MultiShot",
            "Cannon_RocketStorm",
            "Cannon_Bomblettes",
            "Spear_Spin",
            "Spear_Jump",
            "Ram_Earthquake",
            "Roll",
            "Jump"
        };

        string[] listBox2Ind = new string[] {
            "CONSUMABLE",
            "UNKNOWN",
            "UNKNOWN",
            "UNKNOWN",
            "UNKNOWN",
            "UNKNOWN",
            "NONE",
            "NONE",
            "NONE",
            "NONE",
            "NONE",
            "NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
			"NONE",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "SECONDARY",
            "PRIMARY",
            "PRIMARY",
            "PRIMARY",
            "PRIMARY",
            "PRIMARY",
            "PRIMARY",
            "PRIMARY",
            "PRIMARY",
            "DEFENSE",
            "DEFENSE",
            "DEFENSE",
            "DEFENSE",
            "DEFENSE",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "SPECIAL",
            "MOVE",
            "MOVE"
        };

        private BastionSave save;
        public override bool Entry()
        {
            if (!OpenStfsFile("save.sav"))
                return false;

            save = new BastionSave();
            save.LoadSave(this.IO);

            // Load the stats
            intLevel.Value = save.GetCounterValue("GENERAL", "EXPERIENCE_LEVEL");
            intXP.Value = save.GetCounterValue("GENERAL", "EXPERIENCE");
            intMoney.Value = save.GetCounterValue("GENERAL", "MONEY");

            listWeapons.Items.Clear();

            foreach (BastionSave.CurrentWeapon weapon in save.currentWeapons)
            {
                listWeapons.Items.Add(new ListViewItem(new string[] { weapon.weapon, weapon.slot }));
            }

            if (listWeapons.Items.Count == 0)
                panelSet.Enabled = false;
            else
                listWeapons.Items[0].Selected = true;

            return true;
        }

        public override void Save()
        {
            if (cmdUnlockMaps.Checked)
                unlockMaps();

            if (cmdUnlockUpgrades.Checked)
                unlockUpgrades();

            if (cmdUnlockWeapons.Checked)
                unlockWeapons();

            // Set the stats
            save.SetCounterValue("GENERAL", "EXPERIENCE_LEVEL", intLevel.Value);
            save.SetCounterValue("GENERAL", "EXPERIENCE", intXP.Value);
            save.SetCounterValue("GENERAL", "MONEY", intMoney.Value);

            save.WriteSave(this.IO);
        }

        private void unlockWeapons()
        {
            for (int i = 0; i < save.flags.Length; i++)
            {
                if (save.flags[i].flag == "WEAPONS_UNLOCKED")
                {
                    // Clear the unlocked weapons
                    Array.Resize(ref save.flags[i].names, weaponList.Length);

                    for (int x = 0; x < weaponList.Length; x++)
                        save.flags[i].names[x] = weaponList[x];
                }
            }

            save.storedWeapons.Clear();
            foreach (string weapon in weaponList)
                save.storedWeapons.Add(weapon);

            // This code will remain here as a monument to how retarded I am
                /*
                if (save.flags[i].flag == "WEAPONS_VIEWED")
                {
                    // Clear the unlocked weapons
                    Array.Resize(ref save.flags[i].names, weaponList.Length);

                    for (int x = 0; x < weaponList.Length; x++)
                        save.flags[i].names[x] = weaponList[x];
                }

                if (save.flags[i].flag == "WEAPONS_FIRED")
                {
                    // Clear the unlocked weapons
                    Array.Resize(ref save.flags[i].names, weaponList.Length);

                    for (int x = 0; x < weaponList.Length; x++)
                        save.flags[i].names[x] = weaponList[x];
                }

                if (save.flags[i].flag == "SEEDS_FOUND")
                {
                    Array.Resize(ref save.flags[i].names, save.flags[i].names.Length + 3);

                    save.flags[i].names[save.flags[i].names.Length - 3] = "Flamethrower_Plant";
                    save.flags[i].names[save.flags[i].names.Length - 2] = "Mortar_Plant";
                    save.flags[i].names[save.flags[i].names.Length - 1] = "Cannon_Plant";
                }

                if (save.flags[i].flag == "STORE_ITEMS_VIEWED")
                {
                    Array.Resize(ref save.flags[i].names, save.flags[i].names.Length + 3);

                    save.flags[i].names[save.flags[i].names.Length - 3] = "Flamethrower_Plant";
                    save.flags[i].names[save.flags[i].names.Length - 2] = "Mortar_Plant";
                    save.flags[i].names[save.flags[i].names.Length - 1] = "Cannon_Plant";
                }
            }

            // holy fuck i'm dumb, forgot i had get and set counter value functions
            for (int i = 0; i < save.counters.Length; i++)
            {
                if (save.counters[i].name == "SEEDS")
                {
                    for (int x = 0; x < save.counters[i].counters.Length; x++)
                    {
                        if (save.counters[i].counters[x].name == "Flamethrower_Plant")
                            save.counters[i].counters[x].value = 0;
                    }
                }

            }
            save.SetCounterValue("STRUCTURE_RANKS", "WeaponRack", 2);
            save.SetCounterValue("STRUCTURE_RANKS", "WeaponStore", 2);
            save.SetCounterValue("STRUCTURE_RANKS", "ItemPurchaseStructure", 2);

            save.storedWeapons.Add("Flamethrower");
                 */
        }

        private void unlockMaps()
        {
            for (int i = 0; i < save.flags.Length; i++)
            {
                if (save.flags[i].flag == "MAPS_UNLOCKED")
                {
                    string[] maps = new string[] {
                        "Attack01",
                        "Challenge01",
                        "Challenge02",
                        "Challenge03",
                        "Challenge04",
                        "Challenge05",
                        "Challenge06",
                        "Challenge07",
                        "Challenge08",
                        "Challenge09",
                        "Challenge10",
                        "Challenge11",
                        "Challenge12",
                        "Crossroads01",
                        "Crossroads02",
                        "End01",
                        "Falling01",
                        "FinalArena01",
                        "FinalChase01",
                        "FinalRam01",
                        "FinalZulf01",
                        "Fortress01",
                        "Gauntlet01",
                        "Holdout01",
                        "Hunt01",
                        "Moving01",
                        "Onslaught01",
                        "Onslaught02",
                        "Onslaught03",
                        "Platforms01",
                        "ProtoIntro01",
                        "ProtoIntro01a",
                        "ProtoIntro01b",
                        "ProtoTown03",
                        "Rescue01",
                        "Scenes01",
                        "Scenes02",
                        "Scorched01",
                        "Shrine01",
                        "Siege01",
                        "Survivor01",
                        "Survivor02",
                        "Voyage01"
                    };

                    Array.Resize(ref save.flags[i].names, 43);
                    for (int x = 0; x < 43; x++)
                    {
                        save.flags[i].names[x] = maps[x];
                    }
                }
            }
        }

        private void unlockUpgrades()
        {
            for (int i = 0; i < save.flags.Length; i++)
            {
                if (save.flags[i].flag == "UPGRADES_UNLOCKED")
                {
                    string[] upgrades = new string[]
                    {
                        "PotionCapacityAndPotency",
                        "LifeSteal",
                        "ImprovedTouchdown",
                        "PlayerOnHitWeapon",
                        "OnKillBuff",
                        "ExtraLives",
                        "PlusCounterDamage",
                        "CounterHeal",
                        "Longbow_ChargeTime",
                        "Longbow_NumPenetrations",
                        "Longbow_Damage",
                        "Longbow_Poison",
                        "Longbow_ChargeTime_2",
                        "Longbow_NumPenetrations_2",
                        "Longbow_Damage_2",
                        "Longbow_Poison_2",
                        "Longbow_Knockback",
                        "Longbow_Stun",
                        "Repeater_ClipSize",
                        "Repeater_ReloadTime",
                        "Repeater_Damage",
                        "Repeater_Spray",
                        "Repeater_ClipSize_2",
                        "Repeater_ReloadTime_2",
                        "Repeater_Damage_2",
                        "Repeater_Spray_2",
                        "Repeater_Bouncing",
                        "Repeater_Homing",
                        "Revolvers_ClipSize",
                        "Revolvers_SpreadDown",
                        "Revolvers_ReloadTime",
                        "Revolvers_Damage",
                        "Revolvers_Richochet",
                        "Revolvers_Penetrations",
                        "Revolvers_SpeedPenalty",
                        "Revolvers_Damage_2",
                        "Revolvers_Knockback",
                        "Revolvers_ArmorPenetration",
                        "Rifle_Knockback",
                        "Rifle_SpeedPenalty",
                        "Rifle_Damage_1",
                        "Rifle_ChargeTime",
                        "Rifle_CritChance",
                        "Rifle_Cooldown",
                        "Rifle_Damage_2",
                        "Rifle_ChargeTime_2",
                        "Rifle_DamageRadius",
                        "Rifle_ArmorPenetration",
                        "Shotgun_SpreadDown",
                        "Shotgun_SpreadUp",
                        "Shotgun_Damage",
                        "Shotgun_Knocback",
                        "Shotgun_TaperReduction",
                        "Shotgun_ReloadTime",
                        "Shotgun_Knocback_2",
                        "Shotgun_DoubleShot",
                        "Shotgun_Slow",
                        "Mortar_DamageRadius",
                        "Mortar_ReloadTime",
                        "Mortar_ChargeTime",
                        "Mortar_CritChance",
                        "Mortar_DamageRadius_2",
                        "Mortar_Damage",
                        "Mortar_PowershotBonus",
                        "Mortar_CritChance_2",
                        "Mortar_DamageField",
                        "Mortar_SelfImmunity",
                        "Cannon_Knockback",
                        "Cannon_NoAutoFire",
                        "Cannon_ChargeTime",
                        "Cannon_DamageRadius",
                        "Cannon_Cooldown",
                        "Cannon_Damage",
                        "Cannon_ChargeTime_2",
                        "Cannon_DamageRadius_2",
                        "Cannon_Homing",
                        "Cannon_DamageField",
                        "SpecialWeaponAmmo",
                        "SpecialWeaponDamage",
                        "Hammer_Damage",
                        "Hammer_CritChance",
                        "Hammer_Damage_2",
                        "Hammer_CritMultiplier",
                        "Hammer_ArmorPenetration",
                        "Hammer_Damage_3",
                        "Hammer_ChargeBonus",
                        "Hammer_UppercutKnockup",
                        "Hammer_OverheadStun",
                        "Hammer_CritChanceAndDamage",
                        "Repeater_Damage",
                        "Repeater_Damage_2",
                        "Repeater_Bouncing",
                        "Repeater_Homing",
                        "Shotgun_Damage",
                        "Shotgun_Damage_2",
                        "Spear_CritChance",
                        "Spear_CritDamage",
                        "Spear_Knockback",
                        "Spear_Damage",
                        "Spear_ArmorPenetration",
                        "Spear_ThrowReloadTime",
                        "Spear_Cooldown",
                        "Spear_ThrowRoot",
                        "Spear_ThrowReloadTime_2",
                        "Spear_ThrowNumProjectiles",
                        "Shield_CounterWindow",
                        "Shield_CounterMultiplier",
                        "Machete_CritChance",
                        "Machete_CritChance_2",
                        "Machete_Bleed",
                        "Machete_Bleed_2",
                        "Machete_CritMultiplier",
                        "Machete_CritMultiplier_2",
                        "Machete_ThrowSplit",
                        "Machete_ThrowSplit_2",
                        "Machete_AttackSpeed",
                        "Machete_ThrowPowershot",
                        "Flamethrower_ClipRegen",
                        "Flamethrower_SpreadUp",
                        "Flamethrower_Burn",
                        "Flamethrower_Damage",
                        "Flamethrower_ClipRegen_2",
                        "Flamethrower_Slow",
                        "Flamethrower_Cooldown",
                        "Flamethrower_ArmorUp",
                        "Flamethrower_Speed",
                        "Flamethrower_Turret",
                        "StunImmunity",
                        "RollDamage"
                    };

                    Array.Resize(ref save.flags[i].names, 129);
                    for (int x = 0; x < 129; x++)
                    {
                        save.flags[i].names[x] = upgrades[x];
                    }
                }
            }
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            MaxValues(groupPanel1);
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listWeapons.SelectedIndices.Count == 0)
            {
                panelSet.Enabled = false;
                return;
            }

            panelSet.Enabled = true;

            comboWeapon.Text = save.currentWeapons[listWeapons.SelectedIndices[0]].weapon;
            lblSlot.Text = save.currentWeapons[listWeapons.SelectedIndices[0]].slot;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSlot.Text = listBox2Ind[comboWeapon.SelectedIndex];
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            save.currentWeapons[listWeapons.SelectedIndices[0]].weapon = (string)comboWeapon.SelectedItem;
            save.currentWeapons[listWeapons.SelectedIndices[0]].slot = lblSlot.Text;

            listWeapons.Items[listWeapons.SelectedIndices[0]].Text = (string)comboWeapon.SelectedItem;
            listWeapons.Items[listWeapons.SelectedIndices[0]].SubItems[1].Text = lblSlot.Text;
        }

        private void cmdUnlockWeapons_Click(object sender, EventArgs e)
        {

        }
    }
}