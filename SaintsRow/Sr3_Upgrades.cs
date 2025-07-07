using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using SaintsRow3;

namespace Horizon.PackageEditors.SaintsRow
{
    internal struct UpgradeEntry
    {
        public uint Ident;
        public string Name;
        public string Description;
    }
    internal struct UpgradeEntryData
    {
        public string Name;
        public string Description;
    }
    internal class UpgradeData
    {
        public static readonly int MaxUpgradeCount = 312;
        public Dictionary<uint, bool> Table_Available, Table_Unlocked;
        public static UpgradeEntry[] GameUpgrades;

        public UpgradeData(byte[] identTable, byte[] unlockTable, uint[] identArray)
        {
            GameUpgrades = new UpgradeEntry[identArray.Length];
            for(int x = 0; x < identArray.Length; x++)
            {
                GameUpgrades[x].Ident = identArray[x];
                GameUpgrades[x].Name = Upgrades[x].Name;
                GameUpgrades[x].Description = Upgrades[x].Description;
            }
            ParseBooleanTable(identTable, unlockTable, 0, Table_Unlocked = new Dictionary<uint, bool>());
            ParseBooleanTable(identTable, unlockTable, 1, Table_Available = new Dictionary<uint, bool>());
        }
        private void ParseBooleanTable(byte[] identities, byte[] tableBuffer, int tableIndex, Dictionary<uint, bool> tableOutput)
        {
            EndianReader upgradeReader = new EndianReader(identities, EndianType.BigEndian), tableReader = new EndianReader(tableBuffer, EndianType.BigEndian);
            int i = 0;
            for (int j = 0; j < MaxUpgradeCount / 8; j++)
            {
                tableReader.SeekTo((tableIndex * 0x2C) + (i++ >> 3));
                bool[] unlocks = Functions.BitHelper.ProduceBitmask(tableReader.ReadByte());
                for (int x = 0; x < 8; x++)
                {
                    tableOutput.Add(upgradeReader.ReadUInt32(), unlocks[x]);
                }
            }
        }
        public byte[] SerializeTable(Dictionary<uint, bool> table)
        {
            var ms = new MemoryStream();
            var i = 0;
            var unlocks = new bool[8];
            foreach (KeyValuePair<uint, bool> upgrade in table)
            {
                if (i == 8)
                {
                    ms.WriteByte(Functions.BitHelper.ConvertToWriteableByte(unlocks));
                    unlocks = new bool[8];
                    i = 0;
                }
                unlocks[i++] = upgrade.Value;
            }
            return ms.ToArray();
        }

        private static readonly UpgradeEntryData[] Upgrades = new []
        {
            new UpgradeEntryData() { Name = "Ammo - Explosive", Description = "Carry 25% more explosive ammo"},
            new UpgradeEntryData() { Name = "Ammo - Explosive 2", Description = "Carry 50% more explosive ammo"},
            new UpgradeEntryData() { Name = "Ammo - Explosive 3", Description = "Carry 75% more explosive ammo"},
            new UpgradeEntryData() { Name = "Ammo - Explosive 4", Description = "Unlimited explosive ammo"},
            new UpgradeEntryData() { Name = "Ammo - Grenades", Description = "Carry 25% more grenades"},
            new UpgradeEntryData() { Name = "Ammo - Grenades 2", Description = "Carry 50% more grenades"},
            new UpgradeEntryData() { Name = "Ammo - Grenades 3", Description = "Carry 100% more grenades"},
            new UpgradeEntryData() { Name = "Ammo - Grenades 4", Description = "Unlimited grenade ammo"},
            new UpgradeEntryData() { Name = "Ammo - Pistol", Description = "Carry 25% more pistol ammo"},
            new UpgradeEntryData() { Name = "Ammo - Pistol 2", Description = "Carry 50% more pistol ammo"},
            new UpgradeEntryData() { Name = "Ammo - Pistol 3", Description = "Carry 75% more pistol ammo"},
            new UpgradeEntryData() { Name = "Ammo - Pistol 4", Description = "Unlimited pistol ammo"},
            new UpgradeEntryData() { Name = "Ammo - Rifle", Description = "Carry 25% more rifle ammo"},
            new UpgradeEntryData() { Name = "Ammo - Rifle 2", Description = "Carry 50% more rifle ammo"},
            new UpgradeEntryData() { Name = "Ammo - Rifle 3", Description = "Carry 75% more rifle ammo"},
            new UpgradeEntryData() { Name = "Ammo - Rifle 4", Description = "Unlimited rifle ammo"},
            new UpgradeEntryData() { Name = "Ammo - Shotgun", Description = "Carry 25% more shotgun ammo"},
            new UpgradeEntryData() { Name = "Ammo - Shotgun 2", Description = "Carry 50% more shotgun ammo"},
            new UpgradeEntryData() { Name = "Ammo - Shotgun 3", Description = "Carry 75% more shotgun ammo"},
            new UpgradeEntryData() { Name = "Ammo - Shotgun 4", Description = "Unlimited shotgun ammo"},
            new UpgradeEntryData() { Name = "Ammo - Smg", Description = "Carry 25% more SMG ammo"},
            new UpgradeEntryData() { Name = "Ammo - Smg 2", Description = "Carry 50% more SMG ammo"},
            new UpgradeEntryData() { Name = "Ammo - Smg 3", Description = "Carry 75% more SMG ammo"},
            new UpgradeEntryData() { Name = "Ammo - Smg 4", Description = "Unlimited SMG ammo"},
            new UpgradeEntryData() { Name = "Ammo - Special", Description = "Carry 25% more special ammo"},
            new UpgradeEntryData() { Name = "Ammo - Special 2", Description = "Carry 50% more special ammo"},
            new UpgradeEntryData() { Name = "Ammo - Special 3", Description = "Carry 75% more special ammo"},
            new UpgradeEntryData() { Name = "Ammo - Special 4", Description = "Unlimited special ammo"},
            new UpgradeEntryData() { Name = "Bloodsucker Pack", Description = "New Downloadable Content skills and reward tiers are now available!"},
            new UpgradeEntryData() { Name = "Bonus - Cash", Description = "10% bonus to ALL Cash earned for taking over the Morningstar HQ"},
            new UpgradeEntryData() { Name = "Bonus - Cash Boost", Description = "Increase all Cash received by 5%"},
            new UpgradeEntryData() { Name = "Bonus - Cash Boost 2", Description = "Increase all Cash received by 10%"},
            new UpgradeEntryData() { Name = "Bonus - Cash Boost 3", Description = "Increase all Cash received by 15%"},
            new UpgradeEntryData() { Name = "Bonus - Cash Boost Vip", Description = "Increase all Cash received by 25%"},
            new UpgradeEntryData() { Name = "Bonus - Ho Business", Description = "Hourly City Income of $1,000 for keeping the Morningstar hos"},
            new UpgradeEntryData() { Name = "Bonus - Hourly City Income", Description = "Hourly City Income of $500"},
            new UpgradeEntryData() { Name = "Bonus - Hourly City Income 2", Description = "Hourly City Income of $1,000"},
            new UpgradeEntryData() { Name = "Bonus - Hourly City Income 3", Description = "Hourly City Income of $1,500"},
            new UpgradeEntryData() { Name = "Bonus - Hourly City Income Vip", Description = "Hourly City Income of $2,500"},
            new UpgradeEntryData() { Name = "Bonus - Lump Sum Deposit", Description = "$25,000 Cash Payout to the Saints for returning the Hos"},
            new UpgradeEntryData() { Name = "Bonus - Respect", Description = "5% bonus to ALL Respect earned"},
            new UpgradeEntryData() { Name = "Bonus - Respect", Description = "10% bonus to ALL Respect earned for destroying the Morningstar HQ"},
            new UpgradeEntryData() { Name = "Bonus - Respect 2", Description = "10% bonus to ALL Respect earned"},
            new UpgradeEntryData() { Name = "Bonus - Respect 3", Description = "15% bonus to ALL Respect earned"},
            new UpgradeEntryData() { Name = "Bonus - Respect Vip", Description = "20% bonus to ALL Respect earned"},
            new UpgradeEntryData() { Name = "Character Customize", Description = "Customize characters"},
            new UpgradeEntryData() { Name = "City Takeover", Description = "Completes all City Takeover gameplay for an entire Hood automatically"},
            new UpgradeEntryData() { Name = "City Takeover", Description = "Completes all City Takeover gameplay for an entire Hood automatically"},
            new UpgradeEntryData() { Name = "City Takeover", Description = "Completes city takeover automatically"},
            new UpgradeEntryData() { Name = "Clothing Store Discount", Description = "Receive a 15% discount at clothing stores"},
            new UpgradeEntryData() { Name = "Collectible Finder", Description = "Collectible items are highlighted in the world"},
            new UpgradeEntryData() { Name = "Crib - 3 Count Casino", Description = "The 3 Count is now a Saints Crib. Access your Weapons Cache, Clothes, and Customize your Gang here."},
            new UpgradeEntryData() { Name = "Crib - Angel", Description = "Angel's place is now a Saints Crib. Access your Weapons Cache, and Clothes here."},
            new UpgradeEntryData() { Name = "Crib - Burns Hill Reactors", Description = "The Burns Hill Reactor is now a Saints Crib. Access your Weapons Cache, Clothes, and Customize your Gang here."},
            new UpgradeEntryData() { Name = "Crib - Kinzie", Description = "Kinzie's place is now a Saints Crib. Access your Weapons Cache, and Clothes here."},
            new UpgradeEntryData() { Name = "Crib - Safeword", Description = "Safeword is now a Saints  Crib. Access your Weapons Cache, Clothes, and Customize your Gang here."},
            new UpgradeEntryData() { Name = "Crib - Saints Penthouse", Description = "The Penthouse is now a Saints Crib. Access your Weapons Cache, Clothes, and Customize your Gang here."},
            new UpgradeEntryData() { Name = "Crib - Shaundi's Ex's Apartment", Description = "Shaundi's Ex's Apartment is now a Saints Crib. Access your Weapons Cache, and Clothes here."},
            new UpgradeEntryData() { Name = "Crib - Zimos' Pad", Description = "Zimos' Pad is now a Saints Crib. Access your Weapons Cache, and Clothes here."},
            new UpgradeEntryData() { Name = "Damage - Bullet", Description = "5% reduction in damage taken from bullets"},
            new UpgradeEntryData() { Name = "Damage - Bullet 2", Description = "10% reduction in damage taken from bullets"},
            new UpgradeEntryData() { Name = "Damage - Bullet 3", Description = "20% reduction in damage taken from bullets"},
            new UpgradeEntryData() { Name = "Damage - Bullet 4", Description = "You will not take any damage from bullets"},
            new UpgradeEntryData() { Name = "Damage - Explosive", Description = "5% reduction in damage taken from Explosives"},
            new UpgradeEntryData() { Name = "Damage - Explosive 2", Description = "10% reduction in damage taken from Explosives"},
            new UpgradeEntryData() { Name = "Damage - Explosive 3", Description = "20% reduction in damage taken from Explosives"},
            new UpgradeEntryData() { Name = "Damage - Explosive 4", Description = "You will be immune to explosive damage"},
            new UpgradeEntryData() { Name = "Damage - Fall", Description = "10% reduction in damage taken from Falls"},
            new UpgradeEntryData() { Name = "Damage - Fall 2", Description = "30% reduction in damage taken from Falls"},
            new UpgradeEntryData() { Name = "Damage - Fall 3", Description = "50% reduction in damage taken from Falls"},
            new UpgradeEntryData() { Name = "Damage - Fall 4", Description = "You will not take ANY damage from Falling"},
            new UpgradeEntryData() { Name = "Damage - Fire", Description = "10% reduction in damage taken from Fire"},
            new UpgradeEntryData() { Name = "Damage - Fire 2", Description = "30% reduction in damage taken from Fire"},
            new UpgradeEntryData() { Name = "Damage - Fire 3", Description = "50% reduction in damage taken from Fire"},
            new UpgradeEntryData() { Name = "Damage - Fire 4", Description = "You will not take ANY damage from Fire"},
            new UpgradeEntryData() { Name = "Damage - Vehicle", Description = "10% reduction in damage when hit by a vehicle."},
            new UpgradeEntryData() { Name = "Damage - Vehicle 2", Description = "30% reduction in damage when hit by a vehicle."},
            new UpgradeEntryData() { Name = "Damage - Vehicle 3", Description = "50% reduction in damage when hit by a vehicle."},
            new UpgradeEntryData() { Name = "Damage - Vehicle 4", Description = "You will not take ANY damage when hit by a vehicle."},
            new UpgradeEntryData() { Name = "Discount - Vehicles", Description = "50% lower vehicle customization cost from Matt Millers' hacking"},
            new UpgradeEntryData() { Name = "Discount - Weapons", Description = "25% lower weapon store prices from Matt Millers' hacking"},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 10% bonus to Hourly City Income from the Carver Island district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 5% bonus to Hourly City Income from the Carver Island district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 15% bonus to Hourly City Income from the Carver Island district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 10% bonus to Hourly City Income from the New Colvin district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 10% bonus to Hourly City Income from the Stanfield district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 10% bonus to Hourly City Income from the Downtown district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 15% bonus to Hourly City Income from the Downtown district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 15% bonus to Hourly City Income from the Stanfield district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 5% bonus to Hourly City Income from the New Colvin district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 15% bonus to Hourly City Income from the New Colvin district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 5% bonus to Hourly City Income from the Downtown district."},
            new UpgradeEntryData() { Name = "District Cash Per Hour", Description = "You now receive a 5% bonus to Hourly City Income from the Stanfield district."},
            new UpgradeEntryData() { Name = "Dual Wield - Pistols", Description = "Dual Wield Pistols"},
            new UpgradeEntryData() { Name = "Dual Wield - Smgs", Description = "Dual Wield SMGS"},
            new UpgradeEntryData() { Name = "Escort", Description = "You have unlocked more of the ESCORT Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Escort", Description = "You have unlocked more of the ESCORT Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Explosions - No Ragdoll", Description = "You will never be ragdolled when hit by explosions"},
            new UpgradeEntryData() { Name = "Explosive Combat Pack", Description = "New Downloadable Content available in your Crib Wardrobe and Weapon Stash."},
            new UpgradeEntryData() { Name = "Faster Reloads 1", Description = "You reload all weapons faster"},
            new UpgradeEntryData() { Name = "Faster Reloads 2", Description = "You reload all weapons faster"},
            new UpgradeEntryData() { Name = "Faster Reloads 3", Description = "You reload all weapons faster"},
            new UpgradeEntryData() { Name = "Funtime! Pack", Description = "New Downloadable Content is available in your Crib Wardrobe, Weapon Stash, and Garage."},
            new UpgradeEntryData() { Name = "Gang - Followers", Description = "Recruit One Saint Gang Member as a Follower."},
            new UpgradeEntryData() { Name = "Gang - Followers 2", Description = "Recruit Two Saint followers. Press {RECRUIT_DISMISS_IMG}  to recruit nearby Saints."},
            new UpgradeEntryData() { Name = "Gang - Followers 3", Description = "Recruit Three Saint followers. Press {RECRUIT_DISMISS_IMG}  to recruit nearby Saints."},
            new UpgradeEntryData() { Name = "Gang - Health Increase", Description = "All followers get a 10% health increase"},
            new UpgradeEntryData() { Name = "Gang - Health Increase 2", Description = "All followers get a 20% health increase"},
            new UpgradeEntryData() { Name = "Gang - Health Increase 3", Description = "All followers get a 30% health increase"},
            new UpgradeEntryData() { Name = "Gang - Revive Timer", Description = "All followers take 10 seconds longer to bleed out"},
            new UpgradeEntryData() { Name = "Gang - Revive Timer 2", Description = "All followers take 20 seconds longer to bleed out"},
            new UpgradeEntryData() { Name = "Gang - Revive Timer 3", Description = "All followers take 30 seconds longer to bleed out"},
            new UpgradeEntryData() { Name = "Gang - Weapons - Rifles", Description = "Upgrade your gang members to carry Rifles"},
            new UpgradeEntryData() { Name = "Gang - Weapons - Shotguns", Description = "Upgrade your gang members to carry Shotguns"},
            new UpgradeEntryData() { Name = "Gang - Weapons - Smgs", Description = "Upgrade your gang members to carry SMGS"},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Morningstar in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Deckers in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Space Saints in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Mascots in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Hos in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Guardsmen in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Luchadores in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "Gang Customization is now available at the Saints HQ"},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Gimps in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Wrestlers in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Strippers in your gang."},
            new UpgradeEntryData() { Name = "Gang Customization", Description = "You can now have Cops in your gang."},
            new UpgradeEntryData() { Name = "Garage - New Vehicles", Description = "Luchadore vehicles are available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Garage - New Vehicles", Description = "Morningstar vehicles are available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Garage - New Vehicles", Description = "Decker vehicles are available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Health - Regen", Description = "Health regenerates slightly faster"},
            new UpgradeEntryData() { Name = "Health - Regen 2", Description = "Health regenerates faster"},
            new UpgradeEntryData() { Name = "Health - Regen 3", Description = "Health regenerates much faster"},
            new UpgradeEntryData() { Name = "Health - Regen 4", Description = "Health regenerates extremely fast"},
            new UpgradeEntryData() { Name = "Health - Upgrade", Description = "Player health is increased by 25%"},
            new UpgradeEntryData() { Name = "Health - Upgrade 2", Description = "Player health is increased by 50%"},
            new UpgradeEntryData() { Name = "Health - Upgrade 3", Description = "Player health is increased by 75%"},
            new UpgradeEntryData() { Name = "Health - Upgrade 4", Description = "Player health is increased by 100%"},
            new UpgradeEntryData() { Name = "Heli Assault", Description = "You have unlocked more of the HELI ASSAULT Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Homie - Angel Dela Muerte", Description = "Angel can be found in your Cell Phone \"PHONE\" menu. He will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Burt Reynolds", Description = "Mayor Burt Reynolds can be found in your Cell Phone \"PHONE\" menu. He will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Health Increase 1", Description = "Homies get a small health boost"},
            new UpgradeEntryData() { Name = "Homie - Health Increase 2", Description = "Homies get a medium health boost"},
            new UpgradeEntryData() { Name = "Homie - Health Increase 3", Description = "Homies get a large health boost"},
            new UpgradeEntryData() { Name = "Homie - Heli", Description = "Heli Homie can be found in your Cell Phone \"PHONE\" menu. He will deliver a helicopter to you in combat."},
            new UpgradeEntryData() { Name = "Homie - Josh Birk", Description = "Josh can be found in your Cell Phone \"PHONE\" menu. He will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Kinzie Kensington", Description = "Kinzie can be found in your Cell Phone \"PHONE\" menu. She will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Nyteblayde", Description = "Nyteblayde can be found in your Cell Phone \"PHONE\" menu. He will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Oleg", Description = "Oleg can be found in your Cell Phone \"PHONE\" menu. He will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Pierce Washington", Description = "Pierce can be found in your Cell Phone \"PHONE\" menu. He will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Saints Backup", Description = "Saints arrive to help in combat"},
            new UpgradeEntryData() { Name = "Homie - Shaundi", Description = "Shaundi can be found in your Cell Phone \"PHONE\" menu. She will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Swat Backup", Description = "Call for a SWAT team to help in combat"},
            new UpgradeEntryData() { Name = "Homie - Tank", Description = "Tank Homie can be found in your Cell Phone \"PHONE\" menu. He will deliver a tank to you in combat."},
            new UpgradeEntryData() { Name = "Homie - Vehicle Delivery", Description = "Have your vehicles delivered directly to you"},
            new UpgradeEntryData() { Name = "Homie - Viola", Description = "Viola can be found in your Cell Phone \"PHONE\" menu. She will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Vtol", Description = "VTOL Homie can be found in your Cell Phone \"PHONE\" menu. He will deliver a VTOL to you in combat."},
            new UpgradeEntryData() { Name = "Homie - Zimos", Description = "Zimos can be found in your Cell Phone \"PHONE\" menu. He will help the Saints in combat."},
            new UpgradeEntryData() { Name = "Homie - Zombie Backup", Description = "Zombies arrive to help in combat"},
            new UpgradeEntryData() { Name = "Homie - Zombie Gat", Description = "BRAAAAAAAINS"},
            new UpgradeEntryData() { Name = "Instant Reload - Pistols", Description = "Instantly reload your pistols"},
            new UpgradeEntryData() { Name = "Instant Reload - Rifles", Description = "Instantly reload your rifles"},
            new UpgradeEntryData() { Name = "Instant Reload - Shotguns", Description = "Instantly reload your shotguns"},
            new UpgradeEntryData() { Name = "Instant Reload - Smgs", Description = "Instantly reload your SMGS"},
            new UpgradeEntryData() { Name = "Insurance Fraud", Description = "You have unlocked more of the INSURANCE FRAUD Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Invincible Pack", Description = "New cheats available in the cell phone."},
            new UpgradeEntryData() { Name = "Item - Gas Mask", Description = "The gas mask has been added to your Crib Wardrobe"},
            new UpgradeEntryData() { Name = "Item - Killbane's Mask", Description = "Killbane's Mask is now available to wear"},
            new UpgradeEntryData() { Name = "Item - Shark Bite Hat", Description = "Intimidate your enemies by wearing the jaws of a great white shark on your head."},
            new UpgradeEntryData() { Name = "Item - Space Costume", Description = "The space costume has been added to your Crib Wardrobe"},
            new UpgradeEntryData() { Name = "Mayhem", Description = "You have unlocked more of the MAYHEM Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Melee - Muscles", Description = "Melee-ing enemies throws them slightly further"},
            new UpgradeEntryData() { Name = "Melee - Muscles 2", Description = "Melee-ing enemies throws them further"},
            new UpgradeEntryData() { Name = "Melee - Muscles 3", Description = "Melee-ing enemies throws them much further"},
            new UpgradeEntryData() { Name = "Melee - Power", Description = "15% increase in the melee damage you deal"},
            new UpgradeEntryData() { Name = "Melee - Power 2", Description = "30% increase in the melee damage you deal"},
            new UpgradeEntryData() { Name = "Melee - Power 3", Description = "50% increase in the melee damage you deal"},
            new UpgradeEntryData() { Name = "Money Shot Pack", Description = "New Downloadable Content available in your Crib Wardrobe, Weapon Cache, and Garage."},
            new UpgradeEntryData() { Name = "Notoriety - Deckers", Description = "Your Deckers Notoriety will decay at a slightly faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Deckers 2", Description = "Your Deckers Notoriety will decay at a faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Deckers 3", Description = "Your Deckers Notoriety will decay at a much faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Luchadores", Description = "Your Luchadore Notoriety will decay at a slightly faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Luchadores 2", Description = "Your Luchadore Notoriety will decay at a faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Luchadores 3", Description = "Your Luchadore Notoriety will decay at a much faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Morningstar", Description = "Your Morningstar Notoriety will decay at a slightly faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Morningstar", Description = "Your Morningstar Notoriety will decay at a slightly faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Morningstar 2", Description = "Your Morningstar Notoriety will decay at a faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Morningstar 3", Description = "Your Morningstar Notoriety will decay at a much faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Police", Description = "Your Police Notoriety will decay at a slightly faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Police 2", Description = "Your Police Notoriety will decay at a faster rate"},
            new UpgradeEntryData() { Name = "Notoriety - Police 3", Description = "Your Police Notoriety will decay at a much faster rate"},
            new UpgradeEntryData() { Name = "Notoriety Wipe - Gang", Description = "When called, wipes out all Gang notoriety"},
            new UpgradeEntryData() { Name = "Notoriety Wipe - Law", Description = "Call Mayor Burt Reynolds to wipe Law Enforcement notoriety"},
            new UpgradeEntryData() { Name = "Notoriety Wipe - Police", Description = "When called, wipes Police notoriety"},
            new UpgradeEntryData() { Name = "Nyte Blayde Pack", Description = "New Downloadable Content available in your Crib Wardrobe and Garage."},
            new UpgradeEntryData() { Name = "Outfit - Altar Boy", Description = "The official Nyte Blayde Altar Boy Outfit."},
            new UpgradeEntryData() { Name = "Outfit - Bloody Cannoness", Description = "The official Nyte Blayde Bloody Cannoness outfit."},
            new UpgradeEntryData() { Name = "Outfit - Blowup Doll", Description = "The blowup doll outfit has been added to your Crib Wardrobe"},
            new UpgradeEntryData() { Name = "Outfit - Cardinal Outfit", Description = "The Cardinal outfit is now available to wear"},
            new UpgradeEntryData() { Name = "Outfit - Funtime!", Description = "Every day can be MURDERTIME FUNTIME! with this outfit."},
            new UpgradeEntryData() { Name = "Outfit - Future Soldier Outfit", Description = "Protective battle wear."},
            new UpgradeEntryData() { Name = "Outfit - Intergalactic Warrior", Description = "The ultimate space princess outfit complete with gloves, skirt, and boots."},
            new UpgradeEntryData() { Name = "Outfit - Kabuki Warrior", Description = "Bring out your inner samurai with this full body outfit."},
            new UpgradeEntryData() { Name = "Outfit - Knight Of Steelport", Description = "Full medieval style armor."},
            new UpgradeEntryData() { Name = "Outfit - Mocap Suit", Description = "The mocap suit outfit has been added to your Crib Wardrobe"},
            new UpgradeEntryData() { Name = "Outfit - Stag Armor", Description = "STAG Armor is now available to wear"},
            new UpgradeEntryData() { Name = "Outfit - Toilet", Description = "The toilet outfit has been added to your Crib Wardrobe"},
            new UpgradeEntryData() { Name = "Outfit - Warrior Princess", Description = "Warrior gear that's revealing and protective."},
            new UpgradeEntryData() { Name = "Outfit - Z Style", Description = "You'll look just like Zimos himself  with this getup."},
            new UpgradeEntryData() { Name = "Prof. Genki", Description = "You have unlocked more of the PROF. GENKI Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 15% bonus to all Respect earned for upgrading the Mega Brothel crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 5% bonus to all Respect earned for acquiring the Powder crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 10% bonus to all Respect earned for upgrading the Powder crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 15% bonus to all Respect earned for upgrading the Three Count crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 15% bonus to all Respect earned for upgrading the Powder crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 5% bonus to all Respect earned for acquiring the Nuke Plant crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 5% bonus to all Respect earned for acquiring the Three Count crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 10% bonus to all Respect earned for upgrading the Nuke Plant crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 10% bonus to all Respect earned for upgrading the Mega Brothel crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 5% bonus to all Respect earned for acquiring the Mega Brothel crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 15% bonus to all Respect earned for upgrading the Nuke Plant crib."},
            new UpgradeEntryData() { Name = "Respect Bonus", Description = "You now receive a 10% bonus to all Respect earned for upgrading the Three Count crib."},
            new UpgradeEntryData() { Name = "Revive - Speed", Description = "Revive followers 10% faster"},
            new UpgradeEntryData() { Name = "Revive - Speed 2", Description = "Revive followers 20% faster"},
            new UpgradeEntryData() { Name = "Revive - Speed 3", Description = "Revive followers 30% faster"},
            new UpgradeEntryData() { Name = "Rewards - Decker", Description = "Rewards tied to Decker mission choices are now in effect."},
            new UpgradeEntryData() { Name = "Rewards - Luchador", Description = "Rewards tied to Luchador mission choices have been granted. Check your Crib Wardrobe and Weapon Stash."},
            new UpgradeEntryData() { Name = "Rewards - Morningstar", Description = "Rewards tied to Morningstar mission choices are now in effect."},
            new UpgradeEntryData() { Name = "Rewards - Stag", Description = "Rewards tied to STAG mission choices have been granted. Check your Phonebook and Crib Weapon Stash and Garage."},
            new UpgradeEntryData() { Name = "Saintsbook", Description = "Find Assassination Targets, Vehicle Theft Lists, and Challenges in SAINTSBOOK on your Cell Phone."},
            new UpgradeEntryData() { Name = "Shark Attack Pack", Description = "New Downloadable Content available in your Crib Wardrobe and Weapon Stash."},
            new UpgradeEntryData() { Name = "Skill - Nitrous", Description = "Every vehicle you drive has nitrous"},
            new UpgradeEntryData() { Name = "Skill - Pickpocket", Description = "Bump people to steal their money"},
            new UpgradeEntryData() { Name = "Skill - Scavenger", Description = "Twice as much cash is dropped by people"},
            new UpgradeEntryData() { Name = "Skill - Scavenger Vip", Description = "Four times as much cash is dropped by people"},
            new UpgradeEntryData() { Name = "Skill - Vampire", Description = "Suck the blood of your human shield to regain health while killing them"},
            new UpgradeEntryData() { Name = "Snatch", Description = "You have unlocked more of the SNATCH Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Snatch", Description = "You have unlocked more of the SNATCH Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Special Ops Vehicle Pack", Description = "New Downloadable Content available in your Crib Garage and Helipad."},
            new UpgradeEntryData() { Name = "Sprint - Increase", Description = "Sprint 25% longer"},
            new UpgradeEntryData() { Name = "Sprint - Increase 2", Description = "Sprint 50% longer"},
            new UpgradeEntryData() { Name = "Sprint - Increase 3", Description = "Sprint 75% longer"},
            new UpgradeEntryData() { Name = "Sprint - Increase 4", Description = "Sprint 100% longer"},
            new UpgradeEntryData() { Name = "Sprint - Increase 5", Description = "Unlimited Sprint, forever, no stopping"},
            new UpgradeEntryData() { Name = "Stronghold - 3 Count", Description = "Customize this Stronghold at the '3 Count' Crib to receive huge bonuses."},
            new UpgradeEntryData() { Name = "Stronghold - Burns Hill Reactors", Description = "Customize this Stronghold at the 'Burns Hill Reactors' Crib to receive huge bonuses."},
            new UpgradeEntryData() { Name = "Stronghold - Safeword", Description = "Customize this Stronghold at the 'Safeword Crib' to receive huge bonuses."},
            new UpgradeEntryData() { Name = "Suit - Ultor Assassin", Description = "Anyone representing Ultor needs to look the part.  This sexy one piece puts the class back in assassination."},
            new UpgradeEntryData() { Name = "Swap - Cash For Respect", Description = "Pay cash for 4,000 respect"},
            new UpgradeEntryData() { Name = "Swap - Cash For Respect 2", Description = "Pay cash for 8,000 respect"},
            new UpgradeEntryData() { Name = "Swap - Cash For Respect 3", Description = "Pay cash for 12,000 respect"},
            new UpgradeEntryData() { Name = "Tank Mayhem", Description = "You have unlocked more of the TANK MAYHEM Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Trafficking", Description = "You have unlocked more of the TRAFFICKING Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Trail Blazing", Description = "You have unlocked more of the TRAIL BLAZING Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Trail Blazing", Description = "You have unlocked more of the TRAIL BLAZING Activity. Check your MAP for locations."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Nuke Plant crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for acquiring the Powder crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Powder crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for acquiring the Mega Brothel crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Mega Brothel crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Mega Brothel crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for acquiring the Nuke Plant crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Three Count crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Three Count crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for acquiring the Three Count crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Powder crib."},
            new UpgradeEntryData() { Name = "Transfer Limit", Description = "Your City Income Transfer Limit is increased for upgrading the Nuke Plant crib."},
            new UpgradeEntryData() { Name = "Upgrades", Description = "Purchase new Abilities, Bonuses, and Skills in the UPGRADES menu on your Cell Phone."},
            new UpgradeEntryData() { Name = "Vehicle - Aircraft", Description = "You now have a set of Aircraft available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Cyber Tank", Description = "You now have the cyber tank available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Cyrus' Vtol", Description = "You now have Cyrus' VTOL available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Genki Manapult", Description = "The only vehicle that shoots a real, live human out of a cannon!"},
            new UpgradeEntryData() { Name = "Vehicle - Gunship", Description = "You now have this helicopter available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Jet Bike", Description = "You now have the prototype Jet Bike available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Mars Rover", Description = "You now have the Mars Rover available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Nyte Blayde Vehicles", Description = "The Bloody Cannoness's Bike and the Nyte Blayde Mobile. Speed. Power. Style."},
            new UpgradeEntryData() { Name = "Vehicle - Prototype Tank", Description = "You now have a prototype tank available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Saints Military Vehicles", Description = "Saints Military Vehicles."},
            new UpgradeEntryData() { Name = "Vehicle - Spotlight", Description = "You now have this helicopter available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Stag N-Forcer", Description = "You now have a STAG N-FORCER available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Stag Tank", Description = "You now have a STAG tank available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Stag Vtol", Description = "You now have a STAG VTOL available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle - Tank", Description = "Retrieve the Tank at your crib garage."},
            new UpgradeEntryData() { Name = "Vehicle - Ultor Interceptor", Description = "Before the technology was licensed to STAG, the Ultor Interceptor was the original jetbike. "},
            new UpgradeEntryData() { Name = "Vehicle - Watercraft", Description = "You now have a set of Watercraft available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Vehicle Customize", Description = "Customize vehicles"},
            new UpgradeEntryData() { Name = "Vehicles - Cyberspace", Description = "You now have the cyberspace car and bike available at your Crib Garage"},
            new UpgradeEntryData() { Name = "Warrior Pack", Description = "New Downloadable Content outfits are available in your Crib Wardrobe."},
            new UpgradeEntryData() { Name = "Weapon - Apoca-Fists", Description = "Killbane's Wrestling Gloves, the Apoca-Fists, are now available at your Crib Weapon Stash"},
            new UpgradeEntryData() { Name = "Weapon - Bling Shotgun", Description = "Every kill with this weapon snags you extra respect."},
            new UpgradeEntryData() { Name = "Weapon - Chainsaw", Description = "Equip the Chainsaw at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Cyber Buster", Description = "Equip the Cyber Buster at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Genki Launcher", Description = "Singing, exploding, mind-controlling octopi!"},
            new UpgradeEntryData() { Name = "Weapon - M2 Grenade Launcher", Description = "Launch a plethora of grenades for maximum explosions."},
            new UpgradeEntryData() { Name = "Weapon - Penetrator", Description = "Equip the Penetrator at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Rc Possessor", Description = "Take Remote Control of civilian vehicles with the RC POSSESSOR, now available at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Reaper Drone", Description = "Equip the Reaper Drone Weapon at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Sa3 Airstrike", Description = "Equip the SA3 Airstrike Designator at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Satchel Charges", Description = "Equip the Satchel Charges at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Shark-O-Matic", Description = "Chum your enemies and see what comes up!"},
            new UpgradeEntryData() { Name = "Weapon - Sonic Boom", Description = "Equip the Sonic Boom weapon at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Sonic Boom", Description = "Equip the Sonic Boom weapon at your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon - Togo-13", Description = "Ultor has made the murder of dignitaries even easier with this fully automatic sniper rifle."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Weapon Cache Ammo", Description = "You store 10% more ammo in your Crib Weapon Cache."},
            new UpgradeEntryData() { Name = "Z Style Pack", Description = "New Downloadable Content is available in your Crib Wardrobe and Weapon Stash."}
        };
    }

}