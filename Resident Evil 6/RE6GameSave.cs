using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace ResidentEvil6
{
    public class GameSave
    {
        public class InventoryContext
        {
            public Dictionary<Inventory.Class, string> classes;
            public Dictionary<Inventory.Accessory, string> dAccessories;
            public Dictionary<Inventory.Ammunition, string> dAmmunition;
            public Dictionary<Inventory.Weapon, string> dWeapons;
 
            //public OrderedDictionary Weapons, Accessories, Ammunition;
            public Dictionary<string, Inventory.Accessory> rAccessories = new Dictionary<string, Inventory.Accessory>();
            public Dictionary<string, Inventory.Weapon> rWeapons = new Dictionary<string, Inventory.Weapon>();
            public Dictionary<string, Inventory.Ammunition>  rAmmunition = new Dictionary<string, Inventory.Ammunition>();

            public InventoryContext()
            {
                classes = new Dictionary<Inventory.Class, string>
                {
                    {Inventory.Class.None, "None"},
                    {Inventory.Class.Weapon, "Weapon"},
                    {Inventory.Class.Ammunition, "Ammunition"},
                    {Inventory.Class.Accessory, "Accessory"}
                };
                dAccessories = new Dictionary<Inventory.Accessory, string>
                                   {
                                       {Inventory.Accessory.None, "None"},
                                       {Inventory.Accessory.HerbGreen, "Herb (Green)"},
                                       {Inventory.Accessory.HerbRed, "Herb (Red)"},
                                       {Inventory.Accessory.HerbGG, "Herb (G+G)"},
                                       {Inventory.Accessory.HerbGR, "Herb (G+R)"},
                                       {Inventory.Accessory.HerbGGG, "Herb (G+G+G)"}
                                   };
                dAmmunition= new Dictionary<Inventory.Ammunition, string>
                                 {
                                    {Inventory.Ammunition.None, "None"},
                                     {Inventory.Ammunition.Ammo9mm, "9mm Ammo"},
                                     {Inventory.Ammunition.Shells12Gauge, "12-Gauge Shells"},
                                     {Inventory.Ammunition.Ammo762mmNATO, "7.62mm NATO Ammo"},
                                     {Inventory.Ammunition.Ammo556mmNATO, "5.56mm NATO Ammo"},
                                     {Inventory.Ammunition.Ammo127mm, "12.7mm Ammo"},
                                     {Inventory.Ammunition.Ammo50ActionExpressMagnum, ".50 Action-Express Magnum Ammo"},
                                     {Inventory.Ammunition.Ammo500SWMagnumAmmo, ".500 S&W Magnum Ammo"},
                                     {Inventory.Ammunition.Rounds40mmExplosive, "40mm Explosive Rounds"},
                                     {Inventory.Ammunition.Rounds40mmAcid, "40mm Acid Rounds"},
                                     {Inventory.Ammunition.Rounds40mmNitrogen, "40mm Nitrogen Rounds"},
                                     {Inventory.Ammunition.Rocket73mmExplosive, "73mm Explosive Rocket"},
                                     {Inventory.Ammunition.ArrowsNormal, "Arrows (Normal)"},
                                     {Inventory.Ammunition.ArrowsPipeBomb, "Arrows (Pipe Bomb)"},
                                     {Inventory.Ammunition.Shells10Gauge, "10-Gauge Shells"}
                                 };
                var weaponsEnumList = Enum.GetValues(typeof(Inventory.Weapon)).Cast<Inventory.Weapon>().ToList();
                var weaponsList = new string[]
                  {
                    "Hand-To-Hand",
                    "Nine-Oh-Nine (909)",
                    "Picador",
                    "Wing Shooter",
                    "Shotgun",
                    "Assault Shotgun",
                    "Hydra",
                    "Lightning Hawk",
                    "Elephant Killer",
                    "Sniper Rifle",
                    "Semi-Auto Sniper Rifle",
                    "Anti-Materiel Rifle",
                    "Ammo Box 50",
                    "Triple Shot",
                    "MP-AF",
                    "Assault Rifle for Special Tactics",
                    "Bear Commander",
                    "Assault Rifle RN",
                    "Grenade Launcher",
                    "Hand Grenade",
                    "Incendiary Grenade",
                    "Flash Grenade",
                    "Remote Bomb",
                    "Crossbow",
                    "Survival Knife",
                    "Combat Knife",
                    "Stun Rod",
                    "Rocket Launcher",
                    //"Gun Turret1",
                    "Health Tablet",
                    //"Gun Turret2",
                    //"Gadget",
                    "First Aid Spray",
                    //"Turret",
                  };

                dWeapons = new Dictionary<Inventory.Weapon, string>();
                for (int i = 0; i < weaponsList.Length; i++)
                {
                    dWeapons.Add(weaponsEnumList[i], weaponsList[i]);
                }

                foreach(KeyValuePair<Inventory.Accessory, String> keyValue in dAccessories)
                {
                    rAccessories.Add(keyValue.Value, keyValue.Key);
                }
                foreach (KeyValuePair<Inventory.Weapon, String> keyValue in dWeapons)
                {
                    rWeapons.Add(keyValue.Value, keyValue.Key);
                }
                foreach (KeyValuePair<Inventory.Ammunition, String> keyValue in dAmmunition)
                {
                    rAmmunition.Add(keyValue.Value, keyValue.Key);
                }
            }

        }
        public class Inventory
        {
            public enum Character : int
            {
                LeonSKennedy,
                HelenaHarper,
                ChrisRedfield,
                PiersNivans,
                JakeMuller,
                SherryBirkin,
                AdaWong
            }

            public enum Class
            {
                None,
                Weapon,
                Ammunition,
                Accessory,
                SkillPoints,
                Key
            }

            // two triple shots, and gun turrets
            public enum Weapon
            {
                HandToHand,
                NineOhNine909,
                Picador,
                WingShooter,
                Shotgun,
                AssaultShotgun,
                Hydra,
                LightningHawk,
                ElephantKiller,
                SniperRifle,
                SemiAutoSniperRifle,
                AntiMaterielRifle,
                AmmoBox50,
                TripleShot1,
                MPAF,
                AssaultRifleforSpecialTactics,
                BearCommander,
                AssaultRifleRN,
                GrenadeLauncher,
                HandGrenade,
                IncendiaryGrenade,
                FlashGrenade,
                RemoteBomb,
                Crossbow,
                SurvivalKnife,
                CombatKnife,
                StunRod,
                RocketLauncher,
                //GunTurret1,
                HealthTablet = 0x1D,
                //GunTurret2,
                //Gadget,
                FirstAidSpray = 0x20,
                //Turret,
                VTOLMissile = 0x22,
                VTOLMachineGun,
                AdasHelicopterMissiles,
                AdasHelicopterMachineGun,
                MutatedHand,
                Deborah,
                TripleShot2
            }

            public enum Ammunition
            {
                None,
                Ammo9mm,
                Shells12Gauge,
                Ammo762mmNATO,
                Ammo556mmNATO,
                Ammo127mm,
                Ammo50ActionExpressMagnum,
                Ammo500SWMagnumAmmo,
                Rounds40mmExplosive,
                Rounds40mmAcid,
                Rounds40mmNitrogen,
                Rocket73mmExplosive,
                ArrowsNormal,
                ArrowsPipeBomb,
                Shells10Gauge
            }

            public enum Accessory
            {
                None,
                NullHerbG,
                NullHerbR,
                HerbGreen,
                HerbRed,
                HerbGG,
                HerbGR,
                HerbGGG
            }

            public class Entry
            {
                public short EntryId;
                public Class ClassId{get { return (Class) (EntryId >> 0x08);}}
                public byte ItemId {get { return (byte) (EntryId & 0xFF);}}
                public short Amount;

                public string ClassName;
                public string ItemName;
            }

            public List<Entry> Items;
            public Inventory(EndianIO io, InventoryContext hContext)
            {
                Items = new List<Entry>();
                for (var i = 0; i < InventoryItemCount; i++)
                {
                    var entry = new Entry()
                     {
                          EntryId = (short)(io.In.ReadInt16() & 0xFFF),
                          Amount = io.In.ReadInt16()
                     };
                    // -2 for the amount means we have a null slot
                    if(entry.Amount != -2)
                    {
                        entry.ClassName = (string)hContext.classes[entry.ClassId];
                        switch (entry.ClassId)
                        {
                            case Class.Accessory:
                                entry.ItemName = (string)hContext.dAccessories[(Accessory)entry.ItemId];
                                break;
                            case Class.Ammunition:
                                entry.ItemName = (string) hContext.dAmmunition[(Ammunition)entry.ItemId];
                                break;
                            case Class.Weapon:
                                entry.ItemName = (string)hContext.dWeapons[(Weapon)entry.ItemId];
                                break;;
                        }
                    }
                    else
                    {
                        entry.ClassName = entry.ItemName = "None";
                    }
                    Items.Add(entry);
                }
            }
        }

        private readonly EndianIO _io;
        private readonly InventoryContext _inventoryContext;
        public const int SkillCount = 0x60, InventoryItemCount = 0x18, SlotCount = 0x08;

        public int SkillPoints { get; set; }
        public List<byte> SkillSet { get; set; }
        public bool AllSkillSetsUnlocked { get; set; }
        public List<Inventory> CharacterInventories { get; set; }
        public List<int[]> CampaignSkillSettings { get; set; }
        public List<int> MercenarySkillSettings { get; set; }

        public GameSave(EndianIO io)
        {
            if(io != null)
                _io = io;

#if !INT2
            Verify();
#endif
            // initialize all of our handles
            _inventoryContext = new InventoryContext();
            CampaignSkillSettings = new List<int[]>();
            MercenarySkillSettings = new List<int>();

            // begin parsing the save data
            Read();
        }

        private void Verify()
        {
            if (CalculateChecksum() != _io.In.ReadInt32((long)0x08))
                throw new Exception("Resident Evil 6: Savedata is invalid or has been tampered with.");
        }

        private void Read()
        {
            // read the skill points
            SkillPoints = _io.In.SeekNReadInt32(0x6F8);
            AllSkillSetsUnlocked = (_io.In.SeekNReadInt32(0xBC) & 0x02) != 0x00;
            // parse the skill set table
            SkillSet = new List<byte>();
            _io.SeekTo(0x700);
            for (var x = 0; x < SkillCount; x++)
                SkillSet.Add(_io.In.ReadByte());

            CharacterInventories = new List<Inventory>
               {
                   ReadInventory(Inventory.Character.LeonSKennedy),
                   ReadInventory(Inventory.Character.HelenaHarper),
                   ReadInventory(Inventory.Character.ChrisRedfield),
                   ReadInventory(Inventory.Character.PiersNivans),
                   ReadInventory(Inventory.Character.JakeMuller),
                   ReadInventory(Inventory.Character.SherryBirkin),
                   ReadInventory(Inventory.Character.AdaWong)
               };

            _io.SeekTo(0x76C);
            for (int i = 0; i < SlotCount; i++)
            {
                CampaignSkillSettings.Add(new [] { _io.In.ReadInt32(), _io.In.ReadInt32(), _io.In.ReadInt32()});
            }
            _io.SeekTo(0x914);
            for (int i = 0; i < SlotCount; i++)
            {
                MercenarySkillSettings.Add(_io.In.ReadInt32());
                // Mercenaries mode only has one item per slot, so skip the next two entries
                _io.Stream.Position += 0x08;
            }
        }

        private Inventory ReadInventory(Inventory.Character character)
        {
            _io.SeekTo(0x4AF4 + ((int)character * 0x70));
            // skip inventory listing header
            _io.Position += 0x10;
            return new Inventory(_io, _inventoryContext);
        }

        public void Save()
        {
            _io.Out.SeekNWrite(0x6F8, SkillPoints);
            _io.Out.SeekNWrite(0xBC, (_io.In.SeekNReadInt32(0xBC) | (AllSkillSetsUnlocked ? 0x02 : 0x00)));

            // write back the skill settings
            // Campaign skill settings
            _io.SeekTo(0x76C);
            foreach (var campaignSkillSetting in CampaignSkillSettings)
            {
                _io.Out.Write(campaignSkillSetting[0]);
                _io.Out.Write(campaignSkillSetting[1]);
                _io.Out.Write(campaignSkillSetting[2]);
            }
            // Mercenaries skill settings
            _io.SeekTo(0x914);
            foreach (var mercenarySkillSetting in MercenarySkillSettings)
            {
                _io.Out.Write(mercenarySkillSetting);
                _io.Stream.Position += 0x08;
            }

            _io.SeekTo(0x4AF4);
            // write the inventory listing
            foreach (var inventory in CharacterInventories)
            {
                // skip past the header for each inventory
                _io.Position += 0x10;
                foreach(var item in inventory.Items)
                {
                    _io.Out.Write(item.EntryId);
                    _io.Out.Write(item.Amount);
                }
            }

            // Write back the new checksum
            _io.Out.SeekNWrite(0x08, CalculateChecksum());
            
        }

        public void SetSkillSet(List<byte> newSkillSet)
        {
            _io.SeekTo(0x701);
            foreach (byte unlockMask in newSkillSet)
            {
                _io.Out.Write(unlockMask);
            }
        }

        public string[] GetClassItems(Inventory.Class cClass)
        {
            string[] classItems = null;
            switch (cClass)
            {
                case Inventory.Class.Weapon:
                    {
                        classItems = new string[_inventoryContext.dWeapons.Count];
                        _inventoryContext.dWeapons.Values.CopyTo(classItems, 0x00);
                        break;
                    }
                case Inventory.Class.Ammunition:
                    {
                       classItems = new string[_inventoryContext.dAmmunition.Count];
                        _inventoryContext.dAmmunition.Values.CopyTo(classItems, 0x00);
                        break;
                    }
                case Inventory.Class.Accessory:
                    {
                        classItems = new string[_inventoryContext.dAccessories.Count];
                        _inventoryContext.dAccessories.Values.CopyTo(classItems, 0x00);
                        break;
                    }
            }

            return classItems;
        }

        public short GetEntryIdFromItem(Inventory.Class classId, string item)
        {
            byte itemId = 0x00;
            switch(classId)
            {
               case Inventory.Class.Weapon:
                    itemId = (byte)_inventoryContext.rWeapons[item];
                    break;
                    case Inventory.Class.Ammunition:
                    itemId = (byte) _inventoryContext.rAmmunition[item];
                    break;
                case Inventory.Class.Accessory:
                    itemId = (byte) _inventoryContext.rAccessories[item];
                    break;
            }
            
            return (short)((((byte)classId << 0x08) & 0xFF00) | (itemId & 0xFF));
        }

        private int CalculateChecksum()
        {
            int sum = 0, len = _io.In.ReadInt32(0x04) - 0x10;
            _io.In.SeekTo(0x10);
            for (var x = 0; x < len; x += 4)
                sum += _io.In.ReadInt32();
            return sum;
        }
    }
}
