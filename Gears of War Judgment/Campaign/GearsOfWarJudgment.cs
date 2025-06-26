using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Gears_of_War_Judgment.Campaign
{
    public partial class GearsOfWarJudgment : EditorControl
    {
        //public static readonly string FID = "4D530A26";
        public GearsOfWarJudgment()
        {
            InitializeComponent();
            TitleID = FormID.GearsOfWarJudgment;
            

            foreach (string[] weapon in Weapons)
            {
                cbWeapon1.Items.Add(weapon[1]);
                cbWeapon2.Items.Add(weapon[1]);
                cbWeapon4.Items.Add(weapon[1]);
                cbWeapon3.Items.Add(weapon[1]);
            }

            //foreach (string[] model in Models)
            //    cbModel.Items.Add(model[1]);

            cbDifficulty.Items.Add("Casual");
            cbDifficulty.Items.Add("Normal");
            cbDifficulty.Items.Add("Hardcore");
            cbDifficulty.Items.Add("Insane");

            #if INT2
            cmdExtractStructs.Visible = true;
            cmdExtractState.Visible = true;
            #endif
        }

        private struct ComboMeta
        {
            internal GearGame GameSave;
            internal string FileName;
        }

        private GearGame GameSave;
        private List<ComboMeta> GameSaves;

        public override bool Entry()
        {
            GameSaves = new List<ComboMeta>();
            for (int x = 0; x < Package.StfsContentPackage.DirectoryEntries.Count; x++)
                if (OpenStfsFile(x) && IO.Stream.Length != 0)
                    GameSaves.Add(new ComboMeta()
                    {
                        GameSave = new GearGame(IO),
                        FileName = Package.StfsContentPackage.DirectoryEntries[x].FileName
                    });

            if (GameSaves.Count == 0)
                throw new Exception("No checkpoints found in save!");

            comboSaves.Items.Clear();
            for (int x = 1; x < GameSaves.Count + 1; x++)
                comboSaves.Items.Add("Checkpoint " + x);

            isBusy = true;
            comboSaves.SelectedIndex = 0;
            isBusy = false;

            loadCheckpoint(0);

            return true;
        }

        private string tagToName(string[][] collection, string tag, params ComboBoxEx[] comboBoxes)
        {
            foreach (var item in collection.Where(item => item[0] == tag))
                return item[1];

            foreach (var cb in comboBoxes.Where(cb => !cb.Items.Contains(tag)))
                cb.Items.Add(tag);

            return tag;
        }

        private string nameToTag(string[][] collection, string name)
        {
            foreach (string[] item in collection)
                if (item[1] == name)
                    return item[0];
            return name;
        }

        private readonly static string[][] Models = new[]
        {
            new[] { "GearGameContent.GearPawn_LocustLancerGuard", "Cyclops" },
            new[] { "GearGameContent.GearPawn_LocustSniper", "Sniper" },
            new[] { "GearGameContent.GearPawn_LocustHunterBase", "Grenadier" },
            new[] { "GearGameContent.GearPawn_LocustTheron", "Theron" },
            new[] { "GearGameContent.GearPawn_LocustPalaceGuard", "Palace Guard" },
            new[] { "GearGameContent.GearPawn_LocustDroneSavage", "Savage Drone" },
            new[] { "GearGameContent.GearPawn_LocustDroneSavageSiege", "Savage Drone (Siege)" },
            new[] { "GearGameContent.GearPawn_LocustHunterSavageFlame", "Savage Grenadier (Flame)" },
            new[] { "GearGameContent.GearPawn_LocustHunterSavageSiege", "Savage Grenadier (Siege)" },
            new[] { "GearGameContent.GearPawn_LocustHunterSavageHvB", "Savage Grenadier (Overrun)" },
            new[] { "GearGameContent.GearPawn_LocustBeastRiderHvB", "Beast Rider" },
            new[] { "GearGameContent.GearPawn_LocustGeneralRaam", "RAAM" },
            new[] { "GearGameContent.GearPawn_COGHoffman", "Hoffman" },
            new[] { "GearGameContent.GearPawn_LocustBoomerFlame", "Flame Boomer" },
            new[] { "GearGameContent.GearPawn_LocustGunker", "Gunker" },
            new[] { "GearGameContent.GearPawn_LocustBoomerGatling", "Grinder" },
            new[] { "GearGameContent.GearPawn_LocustEliteBoomerFlailHvB", "Mauler" },
            new[] { "GearGameContent.GearPawn_LocustDarkWretch", "Lambent Wretch" },
            new[] { "GearGameContent.GearPawn_LambentDrone", "Lambent Drudge" },
            new[] { "GearGameContent.GearPawn_LambentLocustFormer", "Lambent Drone" },
            new[] { "GearGameContent.GearPawn_Drudge", "Drudge" },
            new[] { "GearGameContent.GearPawn_DrudgeHeadWorm", "Headsnake" },
            new[] { "GearGameContent.GearPawn_LocustBerserker", "Berserker" },
            new[] { "GearGameContent.GearPawn_LocustReaverDriver", "Reaver Driver" },
            new[] { "GearGameContent.GearPawn_LocustReaverDriver_PalaceGuard", "Reaver Driver (Palace Guard)" },
            new[] { "GearGameContent.GearPawn_LocustIthycus", "Mangler" },
            new[] { "GearGameContent.GearPawn_COGDom", "Dom" },
            new[] { "GearGameContent.GearPawn_CCarmine" , "C. Carmine" },
            new[] { "GearGameContent.GearPawn_LocustBrumak", "Brumak" },
            new[] { "GearGameContent.GearPawn_LocustBrumakDriver", "Brumak Driver" },
            new[] { "GearGameContent.GearPawn_NPCCOGBase", "Stranded" },
            new[] { "GearGameContent.GearPawn_LocustNakedTicker", "Wild Ticker" },
            new[] { "GearGameContent.GearPawn_LocustSerapede", "Serapede" },
            new[] { "GearGameContent.GearPawn_LocustMyrrah", "Myrrah" },
            new[] { "GearGameContent.GearPawn_LocustKantusArmor", "Armored Kantus" },
            new[] { "GearGameContent.GearPawn_LambentPolyp", "Polyp" },
            new[] { "GearGameContent.GearPawn_LocustCorpserLarvaUndergroundNakedContent", "Corpser" },
            new[] { "GearGameContent.GearPawn_SecurityBotFlying", "Shrieker" },
            new[] { "GearGameContent.GearPawn_LocustSpeedy", "Bolter" },
            new[] { "GearGameContent.GearPawn_LocustFlameDrone", "Flame Grenadier" },
            new[] { "GearGameContent.GearPawn_EpicReaper", "Epic Reaper" },
            new[] { "GearGameContent.GearPawn_COGBairdJack", "Baird (Young)" },
            new[] { "GearGameContent.GearPawn_COGGusJack", "Cole (Young)" },
            new[] { "GearGameContent.GearPawn_COGSofiaJack", "Sofia" },
            new[] { "GearGameContent.GearPawn_COGGarronJack", "Paduk" },
            new[] { "GearGameContent.GearPawn_COGGarronEpilogue", "Paduk (Epilogue)" },
            new[] { "GearGameContent.GearPawn_COGLoomis", "Loomis" },
            new[] { "GearGameContent.GearPawn_COGBaird", "Baird" },
            new[] { "GearGameContent.GearPawn_COGGus", "Cole" },
            new[] { "GearGameContent.GearPawn_COGMinh", "Kim" },
            new[] { "GearGameContent.GearPawn_COGTai", "Tai" },
            new[] { "GearGameContent.GearPawn_COGAnya", "Anya" },
            new[] { "GearGameContent.GearPawn_COGOnyxGuard", "Onyx Guard" },
            new[] { "GearGameContent.GearPawn_LocustBoomer", "Boomer" },
            new[] { "GearGameContent.GearPawn_LocustBoomerSavage", "Savage Boomer" },
            new[] { "GearGameContent.GearPawn_LocustBoomerMechanic", "Worker" },
            new[] { "GearGameContent.GearPawn_LocustInkWretch", "Ink Wretch" },

            new[] { "GearGame.GearPawn_LocustSneakerBase", "Sneaker" },
            new[] { "GearGame.GearPawn_EscortBase", "Repair" },
            new[] { "GearGame.GearPawn_LocustDroneSavageBase", "Drone" },
            new[] { "GearGame.GearPawn_LocustWretchBase", "Wretch" },
            new[] { "GearGame.GearPawn_LambentHumanBase", "Former" },
            new[] { "GearGame.GearPawn_LocustTickerBase", "Ticker" },
            new[] { "GearGame.GearPawn_LocustKantusBase", "Kantus" },
            new[] { "GearGame.GearPawn_LocustBabyCorpserBase", "Baby Corpser" },
            new[] { "GearGame.GearPawn_SilverbackBase", "Silverback" },
            new[] { "GearGame.GearPawn_LocustNemacystBase", "Nemacyst" },

            new[] { "GearGameContent.Vehicle_ReaverNoGunner", "Reaver" }
        };

        private readonly static string[][] Weapons = new[]
        {
            new[] { "GearGame.GearWeap_Melee", "Melee" },
            new[] { "GearGame.GearWeap_AssaultRifle", "Lancer" },
            new[] { "GearGame.GearWeap_AssaultRifle_MultiTurret", "Lancer Turret" },
            new[] { "GearGame.GearWeap_NadeLauncher", "Booshka" },
            new[] { "GearGame.GearWeap_Boomshot_MultiTurret", "Boomshot Turret" },
            new[] { "GearGame.GearWeap_Boomshot", "Boomshot" },
            new[] { "GearGame.GearWeap_Boomer_FlailBase", "Flail" },
            new[] { "GearGame.GearWeap_AssaultRifle_LMG", "Light Machine Gun" },
            new[] { "GearGame.GearWeap_AxeRifleBase", "Breechshot" },
            new[] { "GearGame.GearWeap_BowBase", "Torque Bow" },
            new[] { "GearGame.GearWeap_RipperBase", "Ripper" },
            new[] { "GearGame.GearWeap_COGPistol", "Snub Pistol" },
            new[] { "GearGame.GearWeap_Blowtorch", "Repair Tool" },
            new[] { "GearGame.GearWeap_SmokeGrenade", "Smoke Grenade" },
            new[] { "GearGame.GearWeap_HODBase", "Hammer of Dawn" },
            new[] { "GearGame.GearWeap_LocustAssaultRifle", "Hammerburst" },
            new[] { "GearGame.GearWeap_HammerburstMarkOneBase", "Classic HB" },
            new[] { "GearGame.GearWeap_LocustAssaultRifle_MultiTurret", "Hammerburst Turret" },
            new[] { "GearGame.GearWeap_RetroLancer", "Retro Lancer" },
            new[] { "GearGame.GearWeap_RetroLancer_MultiTurret", "Retro Lancer Turret" },
            new[] { "GearGame.GearWeap_LocustPistol", "Boltok Pistol" },
            new[] { "GearGame.GearWeap_LocustBurstPistolBase", "Gorgon Pistol" },
            new[] { "GearGame.GearWeap_Shotgun", "Gnasher Shotgun" },
            new[] { "GearGame.GearWeap_Shotgun_MultiTurret", "Shotgun Turret" },
            new[] { "GearGame.GearWeap_Shotgun_SawedOff", "Sawed-off Shotgun" },
            new[] { "GearGame.GearWeap_Marksman", "Markza" },
            new[] { "GearGame.GearWeap_SniperRifle", "Longshot" },
            new[] { "GearGame.GearWeap_TroikaBase", "Troika Turret" },
            new[] { "GearGame.GearWeap_Silverback_MinigunBase", "Silverback Minigun" },
            new[] { "GearGame.GearWeap_Silverback_RocketLauncherBase", "Silverback Rocket" },
            new[] { "GearGame.GearWeap_WretchMelee", "Wretch Melee" },
            new[] { "GearGame.GearWeap_FlameThrowerBase", "Scorcher" },
            new[] { "GearGame.GearWeap_HeavySniperBase", "OneShot" },
            new[] { "GearGame.GearWeap_DiggerLauncher", "Digger" },
            new[] { "GearGame.GearWeap_TripWireBowBase", "Tripwire Crossbow" },
            new[] { "GearGame.GearWeap_FragGrenade", "Frag Grenade" },

            new[] { "GearGameContentWeapons.GearWeap_FireExtinguisher", "Extinguisher" },
            new[] { "GearGameContentWeapons.GearWeap_SpotGrenade", "Spot Grenade" },

            new[] { null, "-- None --" }
        };

        private List<GearPC> PC;
        private List<GearAI> AI;

        private void loadCheckpoint(int saveNum)
        {
            GameSave = GameSaves[saveNum].GameSave;
            PC = GameSave.GetGearPCRecords();
            AI = GameSave.GetGearAIRecords();

            isBusy = true;

            listChars.Nodes.Clear();

            for (int x = 0; x < PC.Count; x++)
                listChars.Nodes.Add(createCharacterNode(tagToName(Models, PC[x].PawnClassName), PC[x]));

            for (int x = 0; x < AI.Count; x++)
                listChars.Nodes.Add(createCharacterNode((AI[x].PlayerName.Length == 0) ? tagToName(Models, AI[x].PawnClassName) : AI[x].PlayerName, AI[x]));

            listChars.SelectedIndex = 0;

            cbDifficulty.SelectedIndex = (byte)GameSave.Difficulty;

            isBusy = false;

            if (PC.Count != 0)
                loadCharacter(PC[0]);
            else if (AI.Count != 0)
                loadCharacter(AI[0]);
            else
                throw new Exception("No characters found in checkpoint!");
        }

        private void listChars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listChars.SelectedIndex == -1 || isBusy)
                return;

            loadCharacter(listChars.Nodes[listChars.SelectedIndex].Tag);
        }

        private GearPC CurrentPlayer;
        private GearAI CurrentAI;

        private void loadCharacter(object charStruct)
        {
            saveCurrentData();

            if (charStruct is GearPC)
            {
                CurrentPlayer = (GearPC)charStruct;
                CurrentAI = null;

                cmdMaxStats.Enabled = true;

                intScore.Enabled = true;
                intTeamScore.Enabled = true;
                intKills.Enabled = true;
                intDeaths.Enabled = true;
                intRevives.Enabled = true;

                intScore.Value = CurrentPlayer.Score;
                intTeamScore.Value = CurrentPlayer.TeamScore;
                intKills.Value = CurrentPlayer.Kills;
                intDeaths.Value = CurrentPlayer.Deaths;
                intRevives.Value = CurrentPlayer.Revives;
                intHealth.Value = (int)((decimal)CurrentPlayer.PawnHealthPct * 100);

                populateWeapons(CurrentPlayer.InventoryRecords);
                //var model = tagToName(Models, CurrentPlayer.PawnClassName, cbModel);
                //cbModel.SelectedItem = model;
                txtName.Text = "Not Available";
                txtName.ReadOnly = true;
                loadLocation(CurrentPlayer.Location);
            }
            else
            {
                CurrentAI = (GearAI)charStruct;
                CurrentPlayer = null;

                cmdMaxStats.Enabled = false;

                intScore.Enabled = false;
                intTeamScore.Enabled = false;
                intKills.Enabled = false;
                intDeaths.Enabled = false;
                intRevives.Enabled = false;

                intScore.Value = 0;
                intTeamScore.Value = 0;
                intKills.Value = 0;
                intDeaths.Value = 0;
                intRevives.Value = 0;

                intHealth.Value = (int)((decimal)CurrentAI.PawnHealthPct * 100);

                populateWeapons(CurrentAI.InventoryRecords);
                //var model = tagToName(Models, CurrentAI.PawnClassName, cbModel);
                //cbModel.SelectedItem = model;
                txtName.Text = CurrentAI.PlayerName;
                txtName.ReadOnly = false;
                loadLocation(CurrentAI.Location);
            }
        }

        private void saveCurrentData()
        {
            if (CurrentPlayer != null)
            {
                CurrentPlayer.Score = intScore.Value;
                CurrentPlayer.TeamScore = intTeamScore.Value;
                CurrentPlayer.Kills = intKills.Value;
                CurrentPlayer.Deaths = intDeaths.Value;
                CurrentPlayer.Revives = intRevives.Value;
                CurrentPlayer.PawnHealthPct = (float)intHealth.Value / 100;

                CurrentPlayer.Location = getCurrentLocation();
                //CurrentPlayer.PawnClassName = nameToTag(Models, (string)cbModel.SelectedItem);
                saveWeapons(CurrentPlayer.InventoryRecords);
            }
            else if (CurrentAI != null)
            {
                CurrentAI.PawnHealthPct = (float)intHealth.Value / 100;

                CurrentAI.Location = getCurrentLocation();
                CurrentAI.PlayerName = txtName.Text;
                //CurrentAI.PawnClassName = nameToTag(Models, (string)cbModel.SelectedItem);
                saveWeapons(CurrentAI.InventoryRecords);
            }
        }

        private void saveWeapons(List<InventoryRecord> weapons)
        {
            saveWeapon(weapons, WeaponSlot.Grenade, cbWeapon1.SelectedIndex,
                cbWeapon1.SelectedItem, intAmmo1.Value, intClipAmmo1.Value, rbWeapon1.Checked);

            saveWeapon(weapons, WeaponSlot.Primary1, cbWeapon2.SelectedIndex,
                cbWeapon2.SelectedItem, intAmmo2.Value, intClipAmmo2.Value, rbWeapon2.Checked);

            saveWeapon(weapons, WeaponSlot.Secondary, cbWeapon4.SelectedIndex,
                cbWeapon4.SelectedItem, intAmmo4.Value, intClipAmmo4.Value, rbWeapon4.Checked);

            saveWeapon(weapons, WeaponSlot.Primary2, cbWeapon3.SelectedIndex,
                cbWeapon3.SelectedItem, intAmmo3.Value, intClipAmmo3.Value, rbWeapon3.Checked);
        }

        private void saveWeapon(List<InventoryRecord> weapons, WeaponSlot slot,
            int index, object selectedWeapon, int ammo, int clipAmmo, bool equipped)
        {
            var weapon = getWeapon(weapons, slot);
            if (weapon != null && index == 0)
                weapons.Remove(weapon);
            else if (index != 0)
            {
                if (weapon == null)
                {
                    weapon = new InventoryRecord(slot);
                    weapons.Add(weapon);
                }
                weapon.InventoryClassPath = nameToTag(Weapons, (string)selectedWeapon);
                weapon.AmmoCount = ammo;
                weapon.SpareAmmoCount = clipAmmo;
                weapon.IsActiveWeapon = equipped;
            }
        }

        private InventoryRecord getWeapon(IEnumerable<InventoryRecord> weapons, WeaponSlot slot)
        {
            return weapons.FirstOrDefault(weapon => weapon.CharacterSlot == slot);
        }

        private Vector getCurrentLocation()
        {
            return new Vector
            {
                X = (float)floatX.Value,
                Y = (float)floatY.Value,
                Z = (float)floatZ.Value
            };
        }

        private void loadLocation(Vector location)
        {
            floatX.Value = location.X;
            floatY.Value = location.Y;
            floatZ.Value = location.Z;
        }

        private void populateWeapon(IEnumerable<InventoryRecord> weapons, WeaponSlot slot,
            ComboBoxEx comboBox, IntegerInput intAmmo, IntegerInput intClipAmmo, RadioButton radioButton)
        {
            var weapon = getWeapon(weapons, slot);
            if (weapon == null)
                comboBox.SelectedIndex = 0;
            else
            {
                comboBox.SelectedItem = tagToName(Weapons, weapon.InventoryClassPath, cbWeapon1, cbWeapon2, cbWeapon4, cbWeapon3);
                intAmmo.Value = weapon.AmmoCount;
                intClipAmmo.Value = weapon.SpareAmmoCount;
                radioButton.Checked = weapon.IsActiveWeapon;
            }
        }

        private void populateWeapons(List<InventoryRecord> weapons)
        {
            isBusy = true;
            populateWeapon(weapons, WeaponSlot.Grenade, cbWeapon1, intAmmo1, intClipAmmo1, rbWeapon1);
            populateWeapon(weapons, WeaponSlot.Primary1, cbWeapon2, intAmmo2, intClipAmmo2, rbWeapon2);
            populateWeapon(weapons, WeaponSlot.Secondary, cbWeapon4, intAmmo4, intClipAmmo4, rbWeapon4);
            populateWeapon(weapons, WeaponSlot.Primary2, cbWeapon3, intAmmo3, intClipAmmo3, rbWeapon3);
            isBusy = false;

            cbWeapon1_SelectedIndexChanged(null, null);
            cbWeapon2_SelectedIndexChanged(null, null);
            cbWeapon3_SelectedIndexChanged(null, null);
            cbWeapon4_SelectedIndexChanged(null, null);
        }

        private Node createCharacterNode(string charName, object tag)
        {
            Node node = new Node(charName);
            node.Tag = tag;
            return node;
        }

        private bool isBusy;
        private void comboSaves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSaves.SelectedIndex == -1 || isBusy)
                return;

            saveCurrentData();

            loadCheckpoint(comboSaves.SelectedIndex);
        }

        public override void Save()
        {
            saveCurrentData();
            foreach (var c in GameSaves)
            {
                var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
                c.GameSave.Write(io);
                io.Close();

                Package.StfsContentPackage.InjectFileFromArray(c.FileName, io.ToArray());
            }
        }

        private void cmdExtractStructs_Click(object sender, EventArgs e)
        {
#if INT2
            //dumpTags("GearGameContentWeapons.GearWeap_");
            //return;
            var fbd = new FolderBrowserDialog();
            if (IO == null || fbd.ShowDialog() != DialogResult.OK)
                return;

            foreach (var s in GameSave.ActorRecords)
                System.IO.File.WriteAllBytes(fbd.SelectedPath + "\\" + s.Name.Replace(':', '.'), s.GetData());
#endif
        }

#if INT2
        private void dumpTags(string prefix)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            string bin = System.IO.File.ReadAllText(ofd.FileName).Replace("\0\0", "|").Replace("\0", "");
            int f, l = 0;
            List<string> sb = new List<string>();
            while ((f = bin.IndexOf(prefix, l)) != -1)
            {
                string mod = bin.Substring(f, bin.IndexOf('|', f) - f);
                if (!sb.Contains(mod))
                    sb.Add(mod);
                l = f + 1;
            }
            System.IO.File.WriteAllLines("C:\\tag_dump.txt", sb.ToArray());
        }
#endif

        private void cmdMaxAmmo_Click(object sender, EventArgs e)
        {
            if (intAmmo1.Enabled)
                intAmmo1.Value = intAmmo1.MaxValue;
            if (intAmmo2.Enabled)
                intAmmo2.Value = intAmmo2.MaxValue;
            if (intAmmo4.Enabled)
                intAmmo4.Value = intAmmo4.MaxValue;
            if (intAmmo3.Enabled)
                intAmmo3.Value = intAmmo3.MaxValue;
        }

        private void updateEquippedWeapons()
        {
            if (!rbWeapon1.Checked && !rbWeapon2.Checked && !rbWeapon4.Checked && !rbWeapon3.Checked)
            {
                if (rbWeapon1.Enabled)
                    rbWeapon1.Checked = true;
                else if (rbWeapon2.Enabled)
                    rbWeapon2.Checked = true;
                else if (rbWeapon4.Enabled)
                    rbWeapon4.Checked = true;
                else if (rbWeapon3.Enabled)
                    rbWeapon3.Checked = true;
            }
        }

        private void cbWeapon1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon1.SelectedIndex == 0)
            {
                intAmmo1.Value = 0;
                intAmmo1.Enabled = false;
                intClipAmmo1.Value = 0;
                intClipAmmo1.Enabled = false;
                rbWeapon1.Checked = false;
                rbWeapon1.Enabled = false;
            }
            else
            {
                intAmmo1.Enabled = true;
                intClipAmmo1.Enabled = true;
                rbWeapon1.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cbWeapon2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon2.SelectedIndex == 0)
            {
                intAmmo2.Value = 0;
                intAmmo2.Enabled = false;
                intClipAmmo2.Value = 0;
                intClipAmmo2.Enabled = false;
                rbWeapon2.Checked = false;
                rbWeapon2.Enabled = false;
            }
            else
            {
                intAmmo2.Enabled = true;
                intClipAmmo2.Enabled = true;
                rbWeapon2.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cbWeapon3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon4.SelectedIndex == 0)
            {
                intAmmo4.Value = 0;
                intAmmo4.Enabled = false;
                intClipAmmo4.Value = 0;
                intClipAmmo4.Enabled = false;
                rbWeapon4.Checked = false;
                rbWeapon4.Enabled = false;
            }
            else
            {
                intAmmo4.Enabled = true;
                intClipAmmo4.Enabled = true;
                rbWeapon4.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cbWeapon4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;
            if (cbWeapon3.SelectedIndex == 0)
            {
                intAmmo3.Value = 0;
                intAmmo3.Enabled = false;
                intClipAmmo3.Value = 0;
                intClipAmmo3.Enabled = false;
                rbWeapon3.Checked = false;
                rbWeapon3.Enabled = false;
            }
            else
            {
                intAmmo3.Enabled = true;
                intClipAmmo3.Enabled = true;
                rbWeapon3.Enabled = true;
            }
            updateEquippedWeapons();
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            Functions.UI.messageBox("The game will not function correctly if you choose a weapon or model that is not used\n"
                + "in the current chapter under normal conditions.", "Gears of War Judgment Notice", MessageBoxIcon.Information);
        }

        private void cmdExtractState_Click(object sender, EventArgs e)
        {
#if INT2
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var io = new EndianIO(ofd.FileName, EndianType.BigEndian, true);
            io.Position = 0;
            var bts = io.In.ReadBytes(io.Length - 4);

            var ms = new MemoryStream();
            LZO.LZO1X.Decompress(bts, ms);
            ms.ToArray().Save(ofd.FileName + ".bin");
            ms.Close();
#endif
        }

        private void cbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBusy)
                return;

            GameSave.Difficulty = (DifficultyLevel)cbDifficulty.SelectedIndex;
        }

        private void cmdMaxStats_Click(object sender, EventArgs e)
        {
            intScore.Value = intScore.MaxValue;
            intTeamScore.Value = intTeamScore.MaxValue;
            intKills.Value = intKills.MaxValue;
            intDeaths.Value = 0;
            intRevives.Value = intRevives.MaxValue;
        }

        private void cmdMaxClipAmmo_Click(object sender, EventArgs e)
        {
            if (intClipAmmo1.Enabled)
            {
                intAmmo1.Value = 255;
                intClipAmmo1.Value = -16777216;
            }

            if (intAmmo2.Enabled)
            {
                intAmmo2.Value = 255;
                intClipAmmo2.Value = -16777216;
            }

            if (intClipAmmo4.Enabled)
            {
                intAmmo4.Value = 255;
                intClipAmmo4.Value = -16777216;
            }

            if (intClipAmmo3.Enabled)
            {
                intAmmo3.Value = 255;
                intClipAmmo3.Value = -16777216;
            }
        }
    }
}
