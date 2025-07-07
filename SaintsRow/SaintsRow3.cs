using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SaintsRow3;

namespace Horizon.PackageEditors.SaintsRow
{
    public partial class SaintsRow3 : EditorControl
    {
        //public static readonly string FID = "5451086D";

        private SaintsRow3GameSave GameSave;
        private UpgradeData UpgradeManager;
        private string[] GameWeapons, GameVehicles;

        public SaintsRow3()
        {
            InitializeComponent();
            TitleID = FormID.SaintsRow3;
            
        }

        public override void enablePanels(bool enable)
        {
            base.enablePanels(enable);

            panelEx3.Enabled = enable && Server.User.isLogged && Server.User.isDiamond;
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("game.dat"))
                return false;

            this.GameSave = new SaintsRow3GameSave(this.IO);
            this.InitGameData();

            this.LoadSaveData();

            return true;
        }

        public override void Save()
        {
            if (dtgWeaponsList.Rows.Count > 32)
            {
                Functions.UI.messageBox(string.Format("Please remove {0} weapons. You cannot have more than 32.", this.dtgWeaponsList.Rows.Count - 32));
            }
            else
            {
                dtgWeaponsList.EndEdit();
                dtgUpgrades.EndEdit();

                GameSave.Money = intMoney.Value;
                List<SaintsRow3GameSave.WeaponEntry> weapons = new List<SaintsRow3GameSave.WeaponEntry>();
                foreach (DataGridViewRow row in dtgWeaponsList.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        uint WeaponIdent = Weapon_NameToIdent(Convert.ToString(row.Cells[0].Value));
                        weapons.Add(new SaintsRow3GameSave.WeaponEntry()
                        {
                            WeaponIdent = WeaponIdent,
                            Weapon1AmmoClip = Convert.ToUInt32(row.Cells[2].Value),
                            WeaponAmmoBarrel = Convert.ToUInt32(row.Cells[3].Value),
                            Weapon2AmmoClip = Convert.ToUInt32(row.Cells[2].Value),
                            InfiniteAmmo = Convert.ToBoolean(row.Cells[4].Value),
                            DualWield = Convert.ToBoolean(row.Cells[5].Value),
                        });
                        int level = 0;
                        for(int m = 0; m < Convert.ToInt32(row.Cells[1].Value) - 1;m++)
                        {
                            level += (1 << m);
                        }
                        if (level > 0)
                        {
                            if (this.GameSave.WeaponUpgradeTable.ContainsKey(WeaponIdent))
                            {
                                this.GameSave.WeaponUpgradeTable[WeaponIdent] = Horizon.Functions.BitHelper.ProduceBitmask(level);
                            }
                            else
                            {
                                this.GameSave.WeaponUpgradeTable.Add(WeaponIdent, Horizon.Functions.BitHelper.ProduceBitmask(level));
                            }
                        }
                    }
                }
                /*
                int vehicleIndex = 0;
                List<SaintsRow3GameSave.GarageEntry> vehicles = new List<SaintsRow3GameSave.GarageEntry>();
                foreach (DataGridViewRow vehicleRow in this.dtgGarageList.Rows)
                {
                    vehicles.Add(new SaintsRow3GameSave.GarageEntry()
                    {
                        GarageIndex = ++vehicleIndex,
                        Ident = Vehicle_NameToIdent((Convert.ToString(vehicleRow.Cells[0].Value))),
                        Unknown1 = 0xFF,
                        Flags = 0xFF
                    });
                }
                */
                if (Server.User.isLogged && Server.User.isDiamond)
                {
                    int x = 0;
                    foreach (DataGridViewRow row in this.dtgUpgrades.Rows)
                    {
                        this.UpgradeManager.Table_Available[UpgradeData.GameUpgrades[x++].Ident] = Convert.ToBoolean(row.Cells[2].Value);
                    }
                    this.GameSave.SaveUpgradeTable(this.UpgradeManager.SerializeTable(this.UpgradeManager.Table_Available), (Server.User.isDiamond) && (Server.User.isLogged));
                }

                this.GameSave.UnlimitedSprint = chkUnlmSprint.Checked;
                this.GameSave.UnlockAllWeapons = chkUnlockAllWeapons.Checked;
                this.GameSave.UpgradeAllWeapons = btnWeapMaxLevel.Checked;

                this.GameSave.SetWeaponsList(weapons);
                //this.GameSave.SetVehicleList(vehicles);

                if (this.btnUnlckAllGAblts.Checked)
                {
                    for(int i = 0; i < this.GameSave.GangAbilities_UnlockTable.Count; i++)
                    {
                        this.GameSave.GangAbilities_UnlockTable[this.GameSave.GangAbilities_UnlockTable.ElementAt(i).Key] = true;
                    }
                }
                this.GameSave.TotalRespectEarned = this.intTotalXp.Value;
                this.GameSave.LevelUpRespectRequired = this.intNextLevelXp.Value;
                this.GameSave.RespectLevel = this.cmbRespectLvl.SelectedIndex + 1;
                this.GameSave.Save();
            }
        }

        private void InitGameData()
        {
            if (this.cmbWeapons.Items.Count == 0)
            {
                this.GameWeapons = new string[]
                {
                    "45 Shepherd",
                    "KA-1 Kobra",
                    "Tek Z-10",
                    "D4TH Blossom",
                    "Cyber Blaster",
                    "Grave Digger",
                    "AS3 Ultimax",
                    "S3X Hammer",
                    "Shark-O-Matic",
                    "K-8 Krukov",
                    "AR-55",
                    "Viper Laser Rifle",
                    "TOGO-13",
                    "Annihilator",
                    "Mollusk Launcher",
                    "Satchel Charges",
                    "Flashbangs",
                    "Grenades",
                    "Electric Grenades",
                    "Molotovs",
                    "Cyber Buster",
                    "Sonic Boom",
                    "SA-3 Airstrike",
                    "Reaper Drone",
                    "RC Possessor",
                    "McManus 2015",
                    "Incinerator",
                    "Mini-Gun",
                    "GL G20",
                    "Shock Hammer",
                    "Riot Shield",
                    "The Penetrator",
                    "Apoca-Fist",
                    "Woodsman",
                    "Baseball Bat",
                    "Nocturne",
                    "Stun Gun", 
                    "Bling Shotgun"
                };
                this.cmbWeapons.Items.AddRange(this.GameWeapons);
            }
            if (this.cmbVehicles.Items.Count == 0)
            {
                this.GameVehicles = new string[]
                {
                    "Kenshin",
                    "Miami",
                    "Commander",
                    "Shark",
                    "Halberd",
                    "Emu",
                    "Attrazione",
                    "Vortex",
                    "Nelson",
                    "Sovereign",
                    "Hammerhead",
                    "Go!",
                    "Bootlegger",
                    "Phoenix",
                    "Torch",
                    "Cosmos",
                    "Peacemaker",
                    "The Duke",
                    "Wakazashi",
                    "Neuron",
                    "Taxi",
                    "Oppressor",
                    "Tornado",
                    "Vulture",
                    "Eagle",
                    "Eagle (Variant)",
                    "Stork",
                    "Westbury",
                    "Giant_Plane",
                    "Woodpecker",
                    "ASP",
                    "F-69 VTOL",
                    //"F-69 VTOL",
                    //"F-69 VTOL",
                    "Condor",
                    "N-Forcer",
                    "Nordberg",
                    "Kayak",
                    "Lockdown",
                    "Anchor",
                    "Keystone",
                    "Sandstorm",
                    "Kaneda",
                    "Specter",
                    "Ultor Interceptor",
                    "Widowmaker",
                    "Justice",
                    "Infuego",
                    "Ambulance",
                    "Bear",
                    "Knoxville",
                    "Status Quo",
                    "Ball",
                    "Challenger",
                    "Quasar",
                    "Titan",
                    "Scrubber",
                    "Steelport Municipal",
                    "Mule",
                    "Thorogood",
                    "Compensator",
                    "Criminal",
                    "Temptress",
                    "Blade",
                    "Zimos",
                    "AB Destroyer",
                    "Solar",
                    "Reaper",
                    "Atlantica",
                    "Estrada",
                    "Raycaster",
                    "Churchill",
                    "Thompson",
                    "Snipes 57",
                    "Alaskan",
                    "Bulldog",
                    "Toad",
                    "Donovan",
                    "Longhauler",
                    "Blaze",
                    "Peterliner",
                    "Side Shooter",
                    "Crusader",
                    "Saints Raider",
                    //"Cargo_Heist",
                    "Pony Cart",
                    //"Bulldog",
                    "Genki Manapult",
                    "Flatbed",
                    "Helims05",
                    "Gat Mobile",
                    "Hammer",
                    //"Car 57",
                    //"Giant_Plane2",
                    "Wraith",
                    //"Cargo2_M9",
                    "X-2 Phantom",
                    "Crusader (Prototype)",
                    //"N-Forcer",
                    "Saints Vtol",
                    "Saints N-Forcer",
                    "Saints Crusader",
                    "Bloody Cannoness",
                    "Nyte Blayde"
                };
                //this.cmbVehicles.Items.AddRange(GameVehicles);
            }
            if (this.dtgUpgrades.Rows.Count == 0)
            {
                if (Server.User.isLogged && Server.User.isDiamond)
                {
                    if (UpgradeManager == null)
                    {
                        var upgradeData = this.GameSave.GetUpgradeData();
                        UpgradeManager = new UpgradeData(upgradeData[0], upgradeData[1], SettingAsUIntArray(193));
                    }
                    foreach (KeyValuePair<uint, bool> unlock in UpgradeManager.Table_Available)
                    {
                        foreach (UpgradeEntry upgrade in UpgradeData.GameUpgrades)
                        {
                            if (unlock.Key == upgrade.Ident)
                            {
                                this.dtgUpgrades.Rows.Add(new object[]
                            {
                                upgrade.Name,
                                upgrade.Description,
                                UpgradeManager.Table_Available[upgrade.Ident],
                                UpgradeManager.Table_Unlocked[upgrade.Ident]
                            });
                            }
                        }
                    }
                }
            }
            if (this.cmbRespectLvl.Items.Count == 0)
            {
                for (int x = 1; x < 51; x++)
                {
                    this.cmbRespectLvl.Items.Add(x.ToString());
                }
            }
        }

        private void LoadSaveData()
        {
            this.intMoney.Value = this.GameSave.Money;
            for (int x = 0; x < this.GameSave.Weapons.Count; x++)
            {
                int level = Weapon_GetLevel(this.GameSave.Weapons[x].WeaponIdent);
                this.dtgWeaponsList.Rows.Add(new object[]
                {
                    this.GameSave.GetWeaponNameFromIdent(this.GameSave.Weapons[x].WeaponIdent),
                    level,
                    this.GameSave.Weapons[x].Weapon1AmmoClip,
                    this.GameSave.Weapons[x].WeaponAmmoBarrel,
                    this.GameSave.Weapons[x].InfiniteAmmo,
                    this.GameSave.Weapons[x].DualWield
                });
            }
            for (int x = 0; x < this.GameSave.Vehicles.Count; x++)
            {
                this.dtgGarageList.Rows.Add(new object[]
                {
                    this.GameSave.GetVehicleNameFromIdent(this.GameSave.Vehicles[x].Ident),
                });
                this.dtgGarageList.Rows[x].Tag = this.GameSave.Vehicles[x].Ident;
            }

            this.LoadAllies();

            this.chkUnlmSprint.Checked = this.GameSave.UnlimitedSprint;

            this.intNextLevelXp.Value = this.GameSave.LevelUpRespectRequired;
            this.intTotalXp.Value = this.GameSave.TotalRespectEarned;
            this.cmbRespectLvl.SelectedIndex = this.GameSave.RespectLevel - 1;
        }

        private void LoadAllies()
        {
            SetGangAbilityLock(0x36EE0544, this.allyVehicleDlvry);
            SetGangAbilityLock(0xB7850E41, this.allyPierce);
            SetGangAbilityLock(0xB622D8A7, this.allyShaundi);
            SetGangAbilityLock(0x844141A8, this.allySaintsBackup);
            SetGangAbilityLock(0xBE2738BF, this.allyZombieGat);
            SetGangAbilityLock(0x6EBDB1F9, this.allyAngelMuerteU);
            SetGangAbilityLock(0xC2D1F87A, this.allyAngelMuerteM);
            SetGangAbilityLock(0xFD69C22A, this.allyKinzie);
            SetGangAbilityLock(0x9E2A4F7A, this.allyZimos);
            SetGangAbilityLock(0xE9D4BE08, this.allyOleg);
            SetGangAbilityLock(0x70B46B6C, this.allyViola);
            SetGangAbilityLock(0x506B5825, this.allyJoshBirk);
            SetGangAbilityLock(0x9FB771B5, this.allyNyteblayde);
            //0x868AE7B9 - Mayor Tate
            SetGangAbilityLock(0x8C566825, this.allyTank);
            SetGangAbilityLock(0x4DA82B70, this.allyHeli);
            SetGangAbilityLock(0xBBCD65D7, this.allyVtol);
            SetGangAbilityLock(0x8D2E4E06, this.allySwatTeam);
            SetGangAbilityLock(0x111413EA, this.allyZombies);
            SetGangAbilityLock(0x264822D9, this.allyPoliceNotoriety);
            SetGangAbilityLock(0xFE16E74A, this.allyGangNotoriety);
            SetGangAbilityLock(0xB7FF1049, this.allyBurt);
            SetGangAbilityLock(0x7EC0BCDE, this.allyTaxi);
            SetGangAbilityLock(0xC0D405B3, this.allyAmbulance);
        }

        private void SetGangAbilityLock(uint Ident, DevComponents.DotNetBar.ButtonX Button)
        {
            Button.Checked = this.GameSave.GangAbilities_UnlockTable[Ident];
            Button.Tag = Ident;
        }

        private void UpdateGangAbilityLock(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX Button = sender as DevComponents.DotNetBar.ButtonX;
            if(Button.Tag != null)
                this.GameSave.GangAbilities_UnlockTable[Convert.ToUInt32(Button.Tag)] = Button.Checked;
        }

        private int Weapon_GetLevel(uint WeaponIdent)
        {
            if (this.GameSave.WeaponUpgradeTable.ContainsKey(WeaponIdent))
            {
                var levels = this.GameSave.WeaponUpgradeTable[WeaponIdent];
                for (int x = 2; x > 0; x--)
                    if (levels[x])
                        return (x + 2);
            }
            return 1;            
        }

        private uint Weapon_NameToIdent(string WeaponName)
        {
            switch (WeaponName.ToUpper())
            {
                case "CYBER BUSTER":
                    return 0xBC21AE05;
                case "SA-3 AIRSTRIKE":
                    return 0x5CADD275;
                case "AR-55":
                    return 0x316DEFBC;
                case "K-8 KRUKOV":
                    return 0x11A5E83A;
                case "VIPER LASER RIFLE":
                    return 0xC50C172C;
                case "REAPER DRONE":
                    return 0x7994319C;
                case "RC POSSESSOR":
                    return 0x1F6BEBAE;
                case "MCMANUS 2015":
                    return 0xC54E0699;
                case "SONIC BOOM":
                    return 0x64C1DEFE;
                case "ANNIHILATOR":
                    return 0xAA1127D0;
                case "45 SHEPHERD":
                    return 0xE89FF93D;
                case "GRAVE DIGGER":
                    return 0x24FE9255;
                case "AS3 ULTIMAX":
                    return 0xABB1BBF7;
                case "TEK Z-10":
                    return 0xB20742BC;
                case "D4TH BLOSSOM":
                    return 0x35B6FFCC;
                case "KA-1 KOBRA":
                    return 0xDCF52EE9;
                case "SATCHEL CHARGES":
                    return 0x21A320A2;
                case "FLASHBANGS":
                    return 0x519A6525;
                case "GRENADES":
                    return 0xF78C3CCF;
                case "ELECTRIC GRENADES":
                    return 0xDECCDDE8;
                case "MOLOTOVS":
                    return 0x24BF8E5E;
                case "INCINERATOR":
                    return 0x20479900;
                case "MINI-GUN":
                    return 0x5C9790F7;
                case "GL G20":
                    return 0x24DB87F6;
                case "BASEBALL BAT":
                    return 0x31F11B9C;
                case "WOODSMAN":
                    return 0x8E7E8459;
                case "MINIGUN/ROCKETS":
                    return 0xCEDCB131;
                case "MOUNTED .50 CAL":
                    return 0x21E70D88;
                case "CRICKET BAT":
                    return 0xBEE489EA;
                case "BEHEMOTH":
                    return 0xE89275AF;
                case "LASER BEAM":
                    return 0xCDDAA623;
                case "SHOCK HAMMER":
                    return 0x1F3D7223;
                case "STUN GUN":
                    return 0x61A134A8;
                case "S3X HAMMER":
                    return 0xF0576D43;
                case "BLOW-UP DOLL":
                    return 0x963DC16C;
                case "SWARM MISSILES":
                    return 0xAB64BEA5;
                case "THE PENETRATOR":
                    return 0xF9890F00;
                case "NOCTURNE":
                    return 0xACD228EA;
                case "BLING SHOTGUN":
                    return 0x26F45573;
                case "SHARK":
                    return 0x8EB3C10E;
                case "TIKI TORCH":
                    return 0xCA08BD6B;
                case "URINAL":
                    return 0x755C45FA;
                case "APOCA-FIST":
                    return 0xCBB32BBB;
                case "CYBER BLASTER":
                    return 0x634F1478;
                case "MOLLUSK LAUNCHER":
                    return 0x87F2BD7D;
                case "M2 GRENADE LAUNCHER":
                    return 0xA585B938;
                case "SHARK-O-MATIC":
                    return 0xEDAE1C09;
                case "TOGO-13":
                    return 0x55BC6D16;
                default:
                    return 0x00;
            }
        }

        private short Vehicle_NameToIdent(string VehicleName)
        {
            switch (VehicleName)
            {
                case "Kenshin":
                    return 0x00000000;
                case "Miami":
                    return 0x00000001;
                case "Commander":
                    return 0x00000002;
                case "Shark":
                    return 0x00000003;
                case "Halberd":
                    return 0x00000004;
                case "Emu":
                    return 0x00000005;
                case "Attrazione":
                    return 0x00000006;
                case "Vortex":
                    return 0x00000007;
                case "Nelson":
                    return 0x00000008;
                case "Sovereign":
                    return 0x00000009;
                case "Hammerhead":
                    return 0x0000000A;
                case "Go!":
                    return 0x0000000B;
                case "Bootlegger":
                    return 0x0000000C;
                case "Phoenix":
                    return 0x0000000D;
                case "Torch":
                    return 0x0000000E;
                case "Cosmos":
                    return 0x0000000F;
                case "Peacemaker":
                    return 0x00000010;
                case "The Duke":
                    return 0x00000011;
                case "Wakazashi":
                    return 0x00000012;
                case "Neuron":
                    return 0x00000013;
                case "Taxi":
                    return 0x00000014;
                case "Oppressor":
                    return 0x00000015;
                case "Tornado":
                    return 0x00000016;
                case "Vulture":
                    return 0x00000017;
                case "Eagle":
                    return 0x00000018;
                case "Eagle_Mad":
                    return 0x00000019;
                case "Stork":
                    return 0x0000001A;
                case "Westbury":
                    return 0x0000001B;
                case "Giant_Plane":
                    return 0x0000001C;
                case "Woodpecker":
                    return 0x0000001D;
                case "Asp":
                    return 0x0000001E;
                case "F-69 Vtol":
                    return 0x0000001F;
                    /*
                case "F-69 Vtol":
                    return 0x00000020;
                case "F-69 Vtol":
                    return 0x00000021;
                    */
                case "Condor":
                    return 0x00000022;
                case "N-Forcer":
                    return 0x00000023;
                case "Nordberg":
                    return 0x00000024;
                case "Kayak":
                    return 0x00000025;
                case "Lockdown":
                    return 0x00000026;
                case "Anchor":
                    return 0x00000027;
                case "Keystone":
                    return 0x00000028;
                case "Sandstorm":
                    return 0x00000029;
                case "Kaneda":
                    return 0x0000002A;
                case "Specter":
                    return 0x0000002B;
                case "Ultor Interceptor":
                    return 0x0000002C;
                case "Widowmaker":
                    return 0x0000002D;
                case "Justice":
                    return 0x0000002E;
                case "Infuego":
                    return 0x0000002F;
                case "Ambulance":
                    return 0x00000030;
                case "Bear":
                    return 0x00000031;
                case "Knoxville":
                    return 0x00000032;
                case "Status Quo":
                    return 0x00000033;
                case "Ball":
                    return 0x00000034;
                case "Challenger":
                    return 0x00000035;
                case "Quasar":
                    return 0x00000036;
                case "Titan":
                    return 0x00000037;
                case "Scrubber":
                    return 0x00000038;
                case "Steelport Municipal":
                    return 0x00000039;
                case "Mule":
                    return 0x0000003A;
                case "Thorogood":
                    return 0x0000003B;
                case "Compensator":
                    return 0x0000003C;
                case "Criminal":
                    return 0x0000003D;
                case "Temptress":
                    return 0x0000003E;
                case "Blade":
                    return 0x0000003F;
                case "Zimos":
                    return 0x00000040;
                case "AB Destroyer":
                    return 0x00000041;
                case "Solar":
                    return 0x00000042;
                case "Reaper":
                    return 0x00000043;
                case "Atlantica":
                    return 0x00000044;
                case "Estrada":
                    return 0x00000045;
                case "Raycaster":
                    return 0x00000046;
                case "Churchill":
                    return 0x00000047;
                case "Thompson":
                    return 0x00000048;
                case "Snipes 57":
                    return 0x00000049;
                case "Alaskan":
                    return 0x0000004A;
                case "Bulldog":
                    return 0x0000004B;
                case "Toad":
                    return 0x0000004C;
                case "Donovan":
                    return 0x0000004D;
                case "Longhauler":
                    return 0x0000004E;
                case "Blaze":
                    return 0x0000004F;
                case "Peterliner":
                    return 0x00000050;
                case "Side Shooter":
                    return 0x00000051;
                case "Crusader":
                    return 0x00000052;
                case "Saints Raider":
                    return 0x00000053;
                case "Cargo_Heist":
                    return 0x00000054;
                case "Pony Cart":
                    return 0x00000055;
                //case "Bulldog":
                    //return 0x00000056;
                case "Genki Manapult":
                    return 0x00000057;
                case "Flatbed":
                    return 0x00000058;
                case "Helims05":
                    return 0x00000059;
                case "Gat Mobile":
                    return 0x0000005A;
                case "Hammer":
                    return 0x0000005B;
                case "Car 57":
                    return 0x0000005C;
                case "Giant_Plane2":
                    return 0x0000005D;
                case "Wraith":
                    return 0x0000005E;
                case "Cargo2_M9":
                    return 0x0000005F;
                case "X-2 Phantom":
                    return 0x00000060;
                case "Crusader (Prototype)":
                    return 0x00000061;
                //case "N-Forcer":
                    //return 0x00000062;
                case "Saints Vtol":
                    return 0x00000063;
                case "Saints N-Forcer":
                    return 0x00000064;
                case "Saints Crusader":
                    return 0x00000065;
                case "Bloody Cannoness":
                    return 0x00000066;
                case "Nyte Blayde":
                    return 0x00000067;
                default:
                    throw new Exception("Saints Row 3: invalid vehicle identity");
            }
        }

        private void BtnClick_MaxMoney(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
        }

        private void BtnClick_AllInfiniteAmmo(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dtgWeaponsList.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells[4].Value = true;
                }
            }
        }

        private void BtnClick_MaxAmmo(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in this.dtgWeaponsList.SelectedRows)
            {
                row.Cells[2].Value = uint.MaxValue;
                row.Cells[3].Value = uint.MaxValue;
            }
        }

        private void BtnClick_UnlockAllUpgrades(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dtgUpgrades.Rows)
            {
                row.Cells[2].Value = true;
            }
        }

        private void BtnClick_ExtractVehicle(object sender, EventArgs e)
        {
            for (int x = 0; x < this.dtgGarageList.SelectedRows.Count; x++)
            {
                var sfd = new SaveFileDialog();
                sfd.FileName = string.Format("{0}.sr3_vehicle", Convert.ToString(this.dtgGarageList.SelectedRows[x].Cells[0].Value));
                if(sfd.ShowDialog() == DialogResult.OK)
                    this.GameSave.Vehicle_ExtractVehicleData(this.dtgGarageList.SelectedRows[x].Index).Save(sfd.FileName);
            }
        }

        private void BtnClick_InjectVehicle(object sender, EventArgs e)
        {
            if (this.dtgGarageList.SelectedRows.Count == 0)
                return;

            bool Failed = false;
            for (int x = 0; x < this.dtgGarageList.SelectedRows.Count; x++)
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!this.GameSave.Vehicle_InsertVehicleData(Convert.ToInt16(this.dtgGarageList.SelectedRows[x].Tag), this.dtgGarageList.SelectedRows[x].Index + 1,
                        File.ReadAllBytes(ofd.FileName)))
                    {
                        Horizon.Functions.UI.messageBox(string.Format(
                            "Injection failed at index {0}, file \"{1}\".", x, Convert.ToString(this.dtgGarageList.SelectedRows[x].Cells[0].Value)));
                        Failed = true;
                        break;
                    }
                }
            }
            if(!Failed)
            {
                Horizon.Functions.UI.messageBox(string.Format("Successfully injected {0} files!", this.dtgGarageList.SelectedRows.Count));
            }
        }

        private void dtgWeapon_EndCellEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.dtgWeaponsList.Rows[e.RowIndex].Cells[1].Value = Weapon_GetLevel(Weapon_NameToIdent(Convert.ToString(dtgWeaponsList.Rows[e.RowIndex].Cells[0].Value)));
            }
        }

        private void dgRowLimit_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.CheckRowCount();
        }

        private void dgRowLimit_UserDeleteRow(object sender, DataGridViewRowEventArgs e)
        {
            if (dtgWeaponsList.Rows != null && dtgWeaponsList.Rows.Count < 0x24)
            {
                dtgWeaponsList.AllowUserToAddRows = true;
            }
        }

        private void CheckRowCount()
        {
            if (dtgWeaponsList.Rows != null && dtgWeaponsList.Rows.Count == 0x24)
            {
                dtgWeaponsList.AllowUserToAddRows = false;
            }
            else if (!dtgWeaponsList.AllowUserToAddRows)
            {
                dtgWeaponsList.AllowUserToAddRows = true;
            }
        }
    }
}
 