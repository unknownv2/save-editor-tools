using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using Horizon.Functions;
using System.Drawing;

namespace Horizon.PackageEditors.Gears_of_War_Judgment.Stats
{
    using TagMap = Dictionary<string, string>;

    public partial class StatsEditor : EditorControl
    {
        private const int MAX_LEVEL = 50;
        private const int MAX_PRESTIGE = 12;

        private const string STR_MODE_SP = "Campaign";
        private const string STR_MODE_MP = "Versus";
        private const string STR_MODE_HB = "OverRun";
        private const string STR_MODE_SU = "Survival";

        private PlayerPersistentData _player;
        private bool _busy;

        public StatsEditor()
        {
            InitializeComponent();
            TitleID = FormID.GearsOfWarJudgment;

#if INT2
            cmdExportStats.Visible = true;
#endif

            for (var x = 0; x <= MAX_PRESTIGE; x++)
                cbPrestige.Items.Add(x);

            cbPrestige.SelectedIndexChanged += (a, b) => { _player["PrestigeLevel"] = cbPrestige.SelectedIndex; };

            for (var x = 1; x <= MAX_LEVEL; x++)
                cbRank.Items.Add(x);

            cbPrestige.SelectedIndexChanged += cbRank_SelectedIndexChanged;
            cbRank.SelectedIndexChanged += cbRank_SelectedIndexChanged;
            intXP.ValueChanged += intXP_ValueChanged;

            ckGears1.Tag = "Gear1CampFinished";
            ckGears2.Tag = "Gear2CampFinished";
            ckGears3.Tag = "Gear3CampFinished";
            ckInfiniteMode.Tag = "InfiniteModeReached";
            intDoubleXP.Tag = "NumDoubleXPTokens";
            intDoubleXPTotal.Tag = "TotalDoubleXPTokens";
            intRibbons.Tag = "Ribbons";
            RegisterControls(ckGears1, ckGears2, ckGears3, ckInfiniteMode, intDoubleXP, intDoubleXPTotal, intRibbons);

            cbModeMain.Items.AddRange(new object[]
            {
                STR_MODE_SP,
                STR_MODE_MP,
                STR_MODE_HB,
                STR_MODE_SU
            });

            cbModeMain.SelectedIndexChanged += cbModeMain_SelectedIndexChanged;

            #region String Building
            /*var sb = new StringBuilder();
            sb.Append("private static readonly Int32Map Titles = new Int32Map\r\n{");
            foreach (var medal in Medals)
            {
                var medalName = medal.Key;

                if (medal.Value.Count == 2)
                    sb.Append("\r\n\t{ \"" + medal.Value.First(t => t.Key != "awName").Key + "\", " +
                              PlayerPersistentData.KeyToID(medal.Key) + " },");
                else
                {
                    int x = 1;
                    foreach (var title in medal.Value.Where(t => t.Key != "awName"))
                    {
                        sb.Append("\r\n\t{ \"" + title.Key + "\", " +
                              PlayerPersistentData.KeyToID(medal.Key + "_" + x) + " },");
                        x++;
                    }
                }
            }
            sb.Append("};");
            Clipboard.SetText(sb.ToString());*/


            /*var sb = new StringBuilder();
            sb.Append("private static readonly Int32Map Medals = new Int32Map\r\n{");
            foreach (var medal in Medals)
            {
                var medalName = medal.Value["awName"];

                var key = medal.Key;

                if (medal.Value.Count != 2)
                    key += "_1";

                
                sb.Append("\r\n\t{ \"" + medalName + "\", " +
                              PlayerPersistentData.KeyToID(key) + " },");
            }
            sb.Append("};");
            Clipboard.SetText(sb.ToString());*/
            #endregion
        }

        private void OnLoad()
        {
            var needsView = _player["NeedsViewed"] != 0;

            for (var x = 1; !needsView && x <= 31; x++)
                if (_player["NeedsViewed_" + x] != 0)
                    needsView = true;

            cmdMarkViewed.Checked = false;
            cmdMarkViewed.Enabled = needsView;

            _busy = true;

            var prestige = _player["PrestigeLevel"];

            if (prestige > MAX_PRESTIGE)
                prestige = MAX_PRESTIGE;
            else if (prestige < 0)
                prestige = 0;

            _player["PrestigeLevel"] = prestige;

            cbPrestige.SelectedIndex = prestige;

            var rank = _player["Level"];

            if (rank > MAX_LEVEL || rank < 1)
            {
                if (rank > MAX_LEVEL)
                    rank = MAX_LEVEL;
                else if (rank < 1)
                    rank = 1;

                _player["Rank"] = rank;

                _busy = false;
            }
            else
                intXP.Value = _player["XP"];
            
            cbRank.SelectedIndex = rank - 1;

            intXP.Value = _player["XP"];

            _busy = false;

            var xpLevel = XPToLevel(prestige, intXP.Value);

            if (xpLevel != rank)
                cbRank.SelectedIndex = xpLevel - 1;

            DefaultCombo(cbModeMain, cbModeMain_SelectedIndexChanged);

            LoadValuesToPanel(panelMainValues);


            //----------------------START MEDALS--------------------------

            var comparer = new CheckNodeComparer();

            listAwards.Nodes.Clear();

            var medalNodes = new Node();

            var checkEverythingMedal = new CheckBoxItem("ckMedals", "Medals / Awards");
            checkEverythingMedal.Tag = medalNodes;
            checkEverythingMedal.CheckedChanged += checkAll_CheckedChanged;
            medalNodes.HostedItem = checkEverythingMedal;

            foreach (var medal in Medals)
            {
                var medalNode = new Node(medal.Value["awName"]);

                var keyCounter = 1;

                foreach (var title in medal.Value.Where(m => m.Key != "awName"))
                {
                    var currentKey = medal.Key;

                    if (medal.Value.Count != 2)
                        currentKey += "_" + keyCounter;

                    InsertBoolNode(medalNode, currentKey, title.Key, title.Value);

                    if (medal.Value.Count == 2)
                        break;

                    keyCounter++;
                }

                var checkAll = new CheckBoxItem("ck_" + medal.Key, medal.Value["awName"]);
                checkAll.Tag = medalNode;
                checkAll.CheckedChanged += checkAll_CheckedChanged;
                medalNode.HostedItem = checkAll;

                UpdateCheckAllNode(medalNode);

                medalNodes.Nodes.Add(medalNode);
            }

            UpdateCheckAllNode(medalNodes);
            medalNodes.Nodes.Sort(comparer);
            listAwards.Nodes.Add(medalNodes);


            //----------------------START WEAPON SKINS--------------------------


            var weaponSkinNodes = new Node();

            var checkEverythingWeaponSkins = new CheckBoxItem("ckWeaponSkins", "Weapon Skins");
            checkEverythingWeaponSkins.Tag = weaponSkinNodes;
            checkEverythingWeaponSkins.CheckedChanged += checkAll_CheckedChanged;
            weaponSkinNodes.HostedItem = checkEverythingWeaponSkins;

            foreach (var skin in WeaponSkins)
            {
                var skinNode = new Node(skin.Value);
                var currentPartialKey = "Award_" + skin.Key;

                foreach (var weapon in Weapons)
                {
                    var currentKey = currentPartialKey + "Skin_" + weapon.Key;
                    if (_player.ContainsKey(currentKey))
                        InsertBoolNode(skinNode, currentKey, weapon.Value, null);
                }

                var checkAll = new CheckBoxItem("ck_" + skin.Key, skin.Value);
                checkAll.Tag = skinNode;
                checkAll.CheckedChanged += checkAll_CheckedChanged;
                skinNode.HostedItem = checkAll;

                UpdateCheckAllNode(skinNode);

                skinNode.Nodes.Sort(comparer);
                weaponSkinNodes.Nodes.Add(skinNode);
            }

            UpdateCheckAllNode(weaponSkinNodes);
            weaponSkinNodes.Nodes.Sort(comparer);
            listAwards.Nodes.Add(weaponSkinNodes);


            //----------------------START CHARACTER SKINS--------------------------


            var skinNodes = new Node();

            var checkEverythingSkins = new CheckBoxItem("ckCharSkins", "Character Skins");
            checkEverythingSkins.Tag = skinNodes;
            checkEverythingSkins.CheckedChanged += checkAll_CheckedChanged;
            skinNodes.HostedItem = checkEverythingSkins;

            foreach (var skin in CharacterSkins)
                InsertBoolNode(skinNodes, string.Format("Award_{0}ArmSkin", skin.Key), skin.Value, null);

            UpdateCheckAllNode(skinNodes);
            skinNodes.Nodes.Sort(comparer);
            listAwards.Nodes.Add(skinNodes);


            //----------------------START CHARACTER UNLOCKS--------------------------


            var charNodes = new Node();

            var checkEverythingChar = new CheckBoxItem("ckCharUnlocks", "Character Unlocks");
            checkEverythingChar.Tag = charNodes;
            checkEverythingChar.CheckedChanged += checkAll_CheckedChanged;
            charNodes.HostedItem = checkEverythingChar;

            foreach (var character in UnlockableCharacters)
                InsertBoolNode(charNodes, "Award_Char" + character.Key, character.Value, null);

            UpdateCheckAllNode(charNodes);
            charNodes.Nodes.Sort(comparer);
            listAwards.Nodes.Add(charNodes);


            //----------------------START EVENTS--------------------------


            var eventNodes = new Node();

            var checkEverythingEvent = new CheckBoxItem("ckEvents", "Event Participation");
            checkEverythingEvent.Tag = eventNodes;
            checkEverythingEvent.CheckedChanged += checkAll_CheckedChanged;
            eventNodes.HostedItem = checkEverythingEvent;

            for (var x = 1; x < 60; x++)
                InsertBoolNode(eventNodes, "Award_Event" + x, "Event " + x, null);

            UpdateCheckAllNode(eventNodes);
            listAwards.Nodes.Add(eventNodes);

            listAwards.Nodes.Sort(comparer);


            //----------------------START RIBBONS--------------------------

            var paddingImage = new Bitmap(1, 30);
            foreach (var ribbon in Ribbons)
            {
                var node = CreateInt32Node(ribbon.Key, string.Format("<b>{0}</b><br></br>{1}", ribbon.Value[0], FatxHandle.makeGrayText(ribbon.Value[1])));
                node.Image = paddingImage;
                listRibbons.Nodes.Add(node);
            }
            
            listRibbons.Nodes.Sort();
        }

        private class CheckNodeComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                var nodeX = (Node)x;
                var nodeY = (Node)y;

                if (nodeX.HostedItem == null || nodeY.HostedItem == null)
                    return string.CompareOrdinal(nodeX.Text, nodeY.Text);

                return string.Compare(nodeX.HostedItem.Text, nodeY.HostedItem.Text, StringComparison.Ordinal);
            }
        }

        private void checkAll_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (_busy)
                return;

            var ck = (CheckBoxItem)sender;
            var node = (Node)ck.Tag;

            var newCheckState = ck.CheckState;

            foreach (Node n in node.Nodes)
                ((CheckBoxItem)n.HostedItem).CheckState = newCheckState;

            if (node.Parent != null)
                UpdateCheckAllNode(node.Parent);
        }

        private void UpdateCheckAllNode(Node node)
        {
            bool all = true, any = false;
            foreach (Node n in node.Nodes)
            {
                var state = ((CheckBoxItem) n.HostedItem).CheckState;
                switch (state)
                {
                    case CheckState.Checked:
                        any = true;
                        break;
                    case CheckState.Indeterminate:
                        any = true;
                        all = false;
                        break;
                    default:
                        all = false;
                        break;
                }
            }

            _busy = true;
            ((CheckBoxItem)node.HostedItem).CheckState = all ? CheckState.Checked : (any ? CheckState.Indeterminate : CheckState.Unchecked);
            _busy = false;

            if (node.Parent != null)
                UpdateCheckAllNode(node.Parent);
        }

        private void cbModeMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gameMode = (string)cbModeMain.SelectedItem;

            lblMain.Text = gameMode + " Mode Stats";

            listMain.Nodes.Clear();

            listMain.Nodes.AddRange(GetNodeCollection(gameMode, null));

            if (gameMode != STR_MODE_HB && gameMode != STR_MODE_SU)
                return;

            var cogNode = new Node("COG");
            cogNode.Nodes.AddRange(GetNodeCollection(gameMode, "COG"));
            listMain.Nodes.Add(cogNode);

            if (gameMode != STR_MODE_HB)
                return;

            var locNode = new Node("Locust");
            locNode.Nodes.AddRange(GetNodeCollection(gameMode, "Loc"));
            listMain.Nodes.Add(locNode);
        }

        private Node[] GetNodeCollection(string gameMode, string team)
        {
            var gameModeFormat = GameModeToFormat(gameMode);

            if (team != null)
            {
                gameModeFormat = team + gameModeFormat;
                gameMode = team + gameMode;
            }

            var nodeList = new List<Node>();

            var sum = new Node("Summary");


            //----------------------START SUMMARY--------------------------


            if (gameMode == STR_MODE_SU)
                sum.Nodes.Add(CreateInt32Node("HighestWaveReached", "Highest Wave Reached", 10));

            foreach (var stat in MainStats)
            {
                var currentKey = string.Format(gameModeFormat, stat.Key);

                if (_player.ContainsKey(currentKey))
                    sum.Nodes.Add(CreateInt32Node(currentKey, stat.Value));
            }

            sum.Nodes.Sort();
            nodeList.Add(sum);


            //----------------------START KILLS SUMMARY--------------------------


            var weaponSummary = new Node("Kills Summary");

            foreach (var spec in KillTypes)
            {
                var specNodes = new List<Node>();

                foreach (var stat in WeaponStats)
                {
                    var currentKey = string.Format(gameModeFormat, spec.Key + stat.Key);

                    if (_player.ContainsKey(currentKey))
                        specNodes.Add(CreateInt32Node(currentKey, stat.Value));
                }

                if (specNodes.Count == 0)
                    continue;

                Node specNode;

                if (specNodes.Count == 1)
                {
                    specNode = specNodes[0];
                    specNode.Text = spec.Value + " " + specNode.Text;
                }
                else
                {
                    specNode = new Node(spec.Value);
                    specNode.Nodes.AddRange(specNodes.ToArray());
                }

                specNode.Nodes.Sort();

                weaponSummary.Nodes.Add(specNode);
            }

            if (weaponSummary.Nodes.Count != 0)
            {
                weaponSummary.Nodes.Sort();
                nodeList.Add(weaponSummary);
            }


            //----------------------START OBJECTIVES--------------------------


            if (gameMode == STR_MODE_HB || gameMode == STR_MODE_SU)
            {
                var objNode = new Node("Objectives");

                foreach (var objStat in Objectives)
                {
                    var currentKey = string.Format(gameModeFormat, objStat.Key);

                    if (_player.ContainsKey(currentKey))
                        objNode.Nodes.Add(CreateInt32Node(currentKey, objStat.Value));
                }

                objNode.Nodes.Sort();
                nodeList.Add(objNode);
            }


            //----------------------START ENEMIES KILLED--------------------------


            var enemiesNode = new Node("Enemies Killed");

            if (gameMode == "Loc" + STR_MODE_HB)
            {
                foreach (var cog in KillableCOG)
                {
                    var currentKey = cog.Key + "KilledHB";

                    if (_player.ContainsKey(currentKey))
                        enemiesNode.Nodes.Add(CreateInt32Node(currentKey, cog.Value + " Kills"));
                }
            }
            else if (gameMode == "COG" + STR_MODE_HB || gameMode == "COG" + STR_MODE_SU)
            {
                foreach (var beast in Beasts)
                {
                    var currentKey = beast.Key + "KilledHB";

                    if (_player.ContainsKey(currentKey))
                        enemiesNode.Nodes.Add(CreateInt32Node(currentKey, beast.Value + " Kills"));
                }

                if (gameMode == "COG" + STR_MODE_HB)
                    enemiesNode.Nodes.Add(CreateInt32Node("ChampionSpawnsHB", "2nd Tier Locust Kills"));
            }
            else
            {
                foreach (var enemy in Characters)
                {
                    var currentKey = string.Format(gameModeFormat, enemy.Key + "Kills");

                    if (_player.ContainsKey(currentKey))
                        enemiesNode.Nodes.Add(CreateInt32Node(currentKey, enemy.Value + " Kills"));
                }
            }

            if (enemiesNode.Nodes.Count != 0)
            {
                enemiesNode.Nodes.Sort();
                nodeList.Add(enemiesNode);
            }


            //----------------------START CLASSES--------------------------


            var classNodes = new Node("Classes");

            foreach (var classs in ClassesCOG)
            {
                var classNode = new Node(classs.Key);

                foreach (var stat in MainStats.Concat(classs.Value))
                {
                    var currentKey = string.Format(gameModeFormat, classs.Key + stat.Key);

                    if (_player.ContainsKey(currentKey))
                        classNode.Nodes.Add(CreateInt32Node(currentKey, stat.Value));
                }

                if (classNode.Nodes.Count == 0)
                    continue;

                classNode.Nodes.Sort();
                classNodes.Nodes.Add(classNode);
            }

            if (classNodes.Nodes.Count != 0)
            {
                classNodes.Nodes.Sort();
                nodeList.Add(classNodes);
            }


            //----------------------START WEAPONS--------------------------


            var weaponsNode = new Node("Weapon Stats");

            foreach (var weapon in Weapons)
            {
                var weaponNode = new Node(weapon.Value);

                foreach (var stat in WeaponStats)
                {
                    var currentKey = string.Format(gameModeFormat, weapon.Key + stat.Key);

                    if (_player.ContainsKey(currentKey))
                        weaponNode.Nodes.Add(CreateInt32Node(currentKey, stat.Value));
                }

                // Executions are in a different format.

                foreach (var execution in ExecutionWeapons)
                {
                    if (gameMode == STR_MODE_SP && weapon.Key == "Gnasher" && execution.Key == "Shotgun")
                    {
                        const string shotgunKey = "ExecutionQuickShotgunSP";

                        if (_player.ContainsKey(shotgunKey))
                        {
                            weaponNode.Nodes.Add(CreateInt32Node(shotgunKey, "Executions"));
                            continue;
                        }
                    }

                    if (weapon.Key != execution.Value)
                        continue;

                    var currentKey = string.Format(gameModeFormat, "ExecutionLong" + execution.Key);

                    if (_player.ContainsKey(currentKey))
                        weaponNode.Nodes.Add(CreateInt32Node(currentKey, "Executions"));
                }

                var executionKey = string.Format(gameModeFormat, "ExecutionLong" + weapon.Key);

                if (_player.ContainsKey(executionKey))
                    weaponNode.Nodes.Add(CreateInt32Node(executionKey, "Executions"));


                if (weaponNode.Nodes.Count == 0)
                    continue;

                weaponNode.Nodes.Sort();
                weaponsNode.Nodes.Add(weaponNode);
            }

            // Inconsistent naming.
            if (gameMode == STR_MODE_SP || gameMode == STR_MODE_MP)
            {
                var stimNade = new Node("Stim-Gas Grenade");
                stimNade.Nodes.Add(CreateInt32Node(string.Format(gameModeFormat, "COGMedicAllyHealed"),"Ally Health Replenished"));

                if (gameMode == STR_MODE_SP)
                    stimNade.Nodes.Add(CreateInt32Node(string.Format(gameModeFormat, "COGMedicRevives"), "Allies Revived"));
                else
                {
                    var spotNade = new Node("Spot Grenade");
                    spotNade.Nodes.Add(CreateInt32Node(string.Format(gameModeFormat, "COGScoutDebuffs"), "Enemies Spotted"));
                    weaponsNode.Nodes.Add(spotNade);
                }

                weaponsNode.Nodes.Add(stimNade);
            }

            if (weaponsNode.Nodes.Count != 0)
            {
                weaponsNode.Nodes.Sort();
                nodeList.Add(weaponsNode);
            }


            //----------------------START PRIZE BOXES--------------------------


            if (gameMode == STR_MODE_SP)
            {
                var prizeNodes = new Node("Opened Prize Boxes");
                prizeNodes.Nodes.Add(CreateInt32Node("PrizeBox_Normal", "Normal Prize Boxes Opened"));
                prizeNodes.Nodes.Add(CreateInt32Node("PrizeBox_Rare", "Rare Prize Boxes Opened"));
                prizeNodes.Nodes.Add(CreateInt32Node("PrizeBox_Epic", "Epic Prize Boxes Opened"));
                nodeList.Add(prizeNodes);
            }


            //----------------------START GAME TYPES--------------------------


            if (gameMode == STR_MODE_MP)
            {
                var gameTypesNode = new Node("Game Types");

                foreach (var gt in GameTypes)
                {
                    var gtNode = new Node(gt.Value);

                    foreach (var stat in GameTypeStats)
                    {
                        var currentKey = gt.Key + stat.Key;

                        if (_player.ContainsKey(currentKey))
                            gtNode.Nodes.Add(CreateInt32Node(currentKey, stat.Value));
                    }

                    gtNode.Nodes.Sort();
                    gameTypesNode.Nodes.Add(gtNode);
                }

                gameTypesNode.Nodes.Sort();
                nodeList.Add(gameTypesNode);
            }


            //----------------------START LOCUSTS--------------------------


            if (gameMode == "Loc" + STR_MODE_HB)
            {
                var locNodes = new Node("Players");

                foreach (var locust in ClassesLocust)
                {
                    var locNode = new Node(Beasts[locust.Key]);

                    foreach (var stat in MainStats.Concat(locust.Value))
                    {
                        var currentKey = locust.Key + stat.Key + "HB";

                        if (_player.ContainsKey(currentKey))
                           locNode.Nodes.Add(CreateInt32Node(currentKey, stat.Value));
                    }

                    locNode.Nodes.Sort();
                    locNodes.Nodes.Add(locNode);
                }

                locNodes.Nodes.Sort();
                nodeList.Add(locNodes);
            }

            return nodeList.ToArray();
        }

        private void InsertBoolNode(Node host, string key, string title, string description)
        {
            var ckInput = new CheckBoxItem();
            ckInput.Name = key;
            ckInput.Tag = host;
            ckInput.Checked = _player[key] == 1;
            ckInput.CheckedChanged += ckInput_CheckedChanged;

            if (description != null)
                title += "<br></br>" + FatxHandle.makeGrayText(description);

            ckInput.Text = title;

            var node = new Node();
            node.HostedItem = ckInput;

            host.Nodes.Add(node);
        }

        private void ckInput_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            var checkBox = (CheckBoxItem)sender;

            if (checkBox.CheckState == CheckState.Indeterminate)
                return;

            _player[checkBox.Name] = checkBox.CheckState == CheckState.Checked ? 1 : 0;

            UpdateCheckAllNode((Node)checkBox.Tag);
        }

        private Node CreateInt32Node(string key, string title)
        {
            return CreateInt32Node(key, title, 999999999);
        }

        private Node CreateInt32Node(string key, string title, int maxValue)
        {
            var intInput = new IntegerInput();
            intInput.Tag = key;
            intInput.Value = _player[key];
            intInput.MinValue = 0;
            intInput.MaxValue = maxValue;
            intInput.ShowUpDown = true;
            intInput.ValueChanged += Int32_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = intInput });

            return node;
        }

        private static readonly Dictionary<string, TagMap> ClassesCOG = new Dictionary<string, TagMap>
        {
            { "Soldier", new TagMap {
                { "AbilityUsed", "Ammo Boxes Deployed" },
                { "TotalAmmoGiven", "Total Ammo Replenished" },
                { "OwnAmmoGiven", "Own Ammo Replenished" },
                { "AllyAmmoGiven", "Ally Ammo Replenished" }
            }},
            { "Engineer", new TagMap {
                { "AbilityUsed", "Sentries Deployed" },
                { "SentryKills", "Sentry Kills" },
                { "FortRepaired", "Fortification Repair Points" },
                { "WallRepaired", "Barrier Repair Points" },
                { "TurretRepaired", "Turret Repair Points" }
            }},
            { "Scout", new TagMap {
                { "AbilityUsed", "Spot Grenades Deployed" },
                { "Debuffs", "Total Debuffs" },
                { "DebuffKills", "Debuffed Kills" },
                { "Climbs", "Climbs" }
            }},
            { "Medic", new TagMap {
                { "AbilityUsed", "Stim-Gas Grenades Deployed" },
                { "TotalHealed", "Total Health Replenished" },
                { "OwnHealed", "Own Health Replenished" },
                { "AllyHealed", "Ally Health Replenished" }
            }}
        };

        private static readonly TagMap Beasts = new TagMap
        {
            { "Beast1", "Ticker" },
            { "Beast2", "Wretch" },
            { "Beast3", "Grenadier" },
            { "Beast4", "Kantus" },
            { "Beast5", "Rager" },
            { "Beast6", "Serapede" },
            { "Beast7", "Mauler" },
            { "Beast8", "Corpser" },
            { "Beast9", "Boomer" },
            { "Beast10", "Bloodmount" }
        };

        private static readonly Dictionary<string, TagMap> ClassesLocust = new Dictionary<string, TagMap>
        {
            { "Beast1", new TagMap {
                { "AbilityUsed", "Dashed" },
                { "GrenadesEaten", "Grenades Eaten" },
                { "Explodes", "Exploded" },
                { "AllyAmmoGiven", "Ally Ammo Replenished" }
            }},
            { "Beast2", new TagMap {
                { "AbilityUsed", "Screamed" },
                { "Climbed", "Climbs" },
                { "Stuns", "COG Stunned" },
                { "StunKilled", "Stunned COGs Killed" }
            }},
            { "Beast3", new TagMap {
                { "AbilityUsed", "Frag Grenades Thrown" },
                { "GrenadeKills", "Frag Grenade Kills" },
                { "TickersFed", "Tickers Fed" }
            }},
            { "Beast4", new TagMap {
                { "AbilityUsed", "Ally Health Replenished" },
                { "Revives", "Allies Revived" },
                { "AllyHealed", "Allies Healed" },
                { "Heals", "Total Heals" }
            }},
            { "Beast5", new TagMap {
                { "AbilityUsed", "Enraged" },
                { "EnrageKills", "Enraged Kills" },
                { "Headshots", "Headshots" }
            }},
            { "Beast6", new TagMap {
                { "AbilityUsed", "Electric Attacks" },
                { "RearUpKills", "Reared-Up Kills" }
            }},
            { "Beast7", new TagMap {
                { "AbilityUsed", "Shield Spins" },
                { "DeflectKills", "Deflected Bullet Kills" },
                { "GrindKills", "Shield Grind Kills" }
            }},
            { "Beast8", new TagMap {
                { "AbilityUsed", "Burrowed" },
                { "Distance", "Distance Traveled While Burrowed" },
                { "SentriesKnocked", "Sentries Destroyed While Burrowed" }
            }}
        };

        private static readonly TagMap KillableCOG = new TagMap
        {
            { "COGSoldier", "COG Soldier" },
            { "COGEngineer", "COG Engineer" },
            { "COGMedic", "COG Medic" },
            { "COGScout", "COG Scout" }
        };

        private static readonly TagMap Weapons = new TagMap
        {
            { "Lancer", "Lancer" },
            { "Hammerburst", "Hammerburst" },
            { "HammerburstMarkOne", "Retro Hammerburst" },
            { "CogPistol", "Snub Pistol" },
            { "Boomshot", "Boomshot" },
            { "Scorcher", "Scorcher" },
            { "TorqueBow", "Torque Bow" },
            { "Cleaver", "Cleaver" },
            { "DiggerLauncher", "Digger Launcher" },
            { "Mulcher", "Mulcher" },
            { "Mortar", "Mortar" },
            { "Longshot", "Longshot" },
            { "HeavySniper", "OneShot" },
            { "Gorgon", "Gorgon Pistol" },
            { "Boltok", "Boltok Pistol" },
            { "RetroLancer", "Retro Lancer" },
            { "Gnasher", "Gnasher" },
            { "SawedOffShotgun", "Sawed-off Shotgun" },
            { "TripWireBow", "Tripwire Crossbow" },
            { "BoltActionRifle", "Breechshot" },
            { "UIRMarksman", "Markza" },
            { "Marksman", "Markza" },
            { "NadeLauncher", "Booshka" },
            { "CoopMinigun", "Vulcan" },
            { "HammerOfDawn", "Hammer of Dawn" },
            { "HammerburstMkOne", "Classic Hammerburst" },
            { "Blowtorch", "Repair Tool" },
            { "Silverback", "Silverback" },
            { "HvBLocLancer", "Locust Lancer" },
            { "HvBLocGnasher", "Locust Gnasher" },
            { "HvBLocChainsaw", "Locust Chainsaw" },
            { "Bayonet", "Bayonet" },
            { "Chainsaw", "Chainsaw" },
            { "Turret", "Turret" },
            { "FragGrenade", "Frag Grenade" },
            { "IncendiaryGrenade", "Incendiary Grenade" },
            { "InkGrenade", "Ink Grenade" },
        };

        private static readonly TagMap ExecutionWeapons = new TagMap
        {
            { "Shotgun", "Gnasher" },
            { "Bow", "TorqueBow" },
            { "BurstPistol", "Gorgon" },
            { "Digger", "DiggerLauncher" },
            { "Elephant", "HeavySniper" },
            { "FlameThrower", "Scorcher" },
            { "SawedOff", "SawedOffShotgun" }
        };

        private static readonly TagMap KillTypes = new TagMap
        {
            { "Rifle", "Rifle" },
            { "Shotgun", "Shotgun" },
            { "Pistol", "Pistol" },
            { "Pistols", "Pistol" },
            { "Heavy", "Heavy Weapon" },
            { "Pickup", "Pickup Weapon" },
            { "StartingWeapon", "Starting Weapon" },
            { "Explosive", "Explosive" },
            { "Incendiary", "Incendiary" },
            { "FriendlyFire", "Friendly Fire" },
            { "Melee", "Melee" },
            { "Grenade", "Grenade" },
            { "Shield", "Holding Shield" }
        };

        private static readonly TagMap WeaponStats = new TagMap
        {
            { "Kills", "Kills" },
            { "ARSuperSuccess", "Perfect Active Reloads" },
            { "ARSuccess", "Successful Active Reloads" },
            { "HoldingDeath", "Deaths Holding" },
            { "Downs", "Downs" },
            { "MinigunKills", "Minigun Kills" },
            { "RocketLauncherKills", "Rocket Launcher Kills" },
            { "KickKills", "Kick Kills" },
            { "StompKills", "Stomp Kills" },
            { "Tags", "Tags" },
            { "Martyr", "Martyrs" }
        };

        private static readonly TagMap MainStats = new TagMap
        {
            { "SectionsPlayed", "Sections Played" },
            { "Kills", "Kills" },
            { "Deaths", "Deaths" },
            { "Downs", "Downs" },
            { "Revives", "Revives" },
            { "Headshots", "Headshots" },
            { "Executions", "Executions" },
            { "TimeInCover", "Time Spent in Cover" },
            { "HighScore", "High Score" },
            { "TotalScore", "Total Score" },
            { "TeamKills", "Team Kills" },
            { "Suicides", "Suicides" },
            { "Assists", "Assists" },
            { "SpottedPlayer", "Players Spotted" },
            { "RankedDisconnections", "Ranked Disconnects" },
            { "InstantRespawns", "Instant Respawns" },
            { "WavesCompleted", "Waves Completed" },
            { "BattlePointsEarned", "Battle Points Earned" },
            { "BattlePointsSpent", "Battle Points Spent" },
            { "CoverTime", "Time Spent in Cover" },
            { "ShootOfHelmets", "Helmets Shot Off" },
            { "WeaponARAttempts", "Active Reload Attempts" },
            { "WeaponARSuccess", "Successful Active Reloads" },
            { "WeaponARSuperSuccess", "Perfect Active Reloads" },
            { "KillsWithHostage" , "Kills Holding a Captive" },
            { "ChaptersPlayed", "Sections Played" },
            { "ChaptersNotDying", "Sections Beat Without Dying"},
            { "Doorman", "Objects Manipulated" },
            { "TimeAlive", "Time Alive" },
            { "Spawns", "Spawns" }
        };

        private static readonly TagMap Objectives = new TagMap
        {
            { "ObjectivesDefended", "Objectives Defended" },
            { "ObjectivesLost", "Objectives Lost" },
            { "FortsDestroyed", "Forts Destroyed" },
            { "ObjectivesDestroyed", "Objectives Destroyed" },
            { "EHoleCapsDestroyed", "E-hole Caps Destroyed" },
            { "GeneratorsDestroyed", "Generators Destroyed" },
            { "WallDestroyed", "Barriers Destroyed" },
            { "TurretDestroyed", "Turrets Destroyed" },
            { "SentriesDestroyed", "Sentries Destroyed" }
        };

        private static readonly TagMap GameTypes = new TagMap
        {
            { "FFA", "Free-for-All" },
            { "TDM", "Team Deathmatch" },
            { "Domination", "Domination" },
            { "HvB", "OverRun" },
            { "Survival", "Survival" },
            { "Execution", "Execution" }
        };

        private static readonly TagMap GameTypeStats = new TagMap
        {
            { "Played", "Played" },
            { "Wins", "Wins" },
            { "First", "MVPs" },
            { "Losses", "Losses" },
            { "Draws", "Draws" },
            { "BattlePointsEarned", "Battle Points Earned" },
            { "RingCaptures", "Ring Captures" },
            { "RingBreaks", "Ring Breaks" },
            { "Defender", "Matches as Defender" }
        };

        private static readonly TagMap Characters = new TagMap
        {
            { "Bloodmount", "Bloodmount" },
            { "Wretch", "Wretch" },
            { "Drone", "Drone" },
            { "Sniper", "Sniper" },
            { "Ticker", "Ticker" },
            { "Hunter", "Grenadier" },
            { "Theron", "Theron Guard" },
            { "Kantus", "Kantus" },
            { "Boomer", "Boomer" },
            { "Berserker", "Berserker" },
            { "CorpserMini", "Corpser" },
            { "Serapede", "Serapede" },
            { "DarkWretch", "Lambent Wretch" },
            { "LambentHuman", "Former" },
            { "Reaver", "Reaver" },
            { "GunShrimp", "Shrieker" },
            { "Devito", "Rager" },
            { "Nemacyst", "Nemacyst" },
            { "LambentLocustFormer", "Lambent Drone" },
            { "BoomerButcher", "Butcher" },
            { "SavageBoomer", "Savage Boomer" },
            { "KantusArmor", "Armored Kantus" },
            { "SavageDrone", "Savage Drone" },
            { "SavageHunter", "Savage Grenadier" },
            { "PalaceGuard", "Palace Guard" },
            { "BoomerFlail", "Mauler" },
            { "BoomerGatling", "Grinder" }
        };

        private static string GameModeToFormat(string gameMode)
        {
            switch (gameMode)
            {
                case STR_MODE_SP:
                    return "{0}SP";
                case STR_MODE_MP:
                    return "{0}MP";
                case STR_MODE_HB:
                    return "{0}HB";
                case STR_MODE_SU:
                    return "{0}SU";
            }

            return null;
        }

        private void intXP_ValueChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            var prestige = _player["PrestigeLevel"];
            var rank = XPToLevel(prestige, intXP.Value);

            _busy = true;

            _player["XP"] = intXP.Value;
            _player["Level"] = rank;
            cbRank.SelectedIndex = rank - 1;

            _busy = false;
        }

        private void cbRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            _busy = true;

            var prestige = cbPrestige.SelectedIndex;
            _player["PrestigeLevel"] = prestige;

            var level = cbRank.SelectedIndex + 1;
            _player["Level"] = level;

            intXP.Value = LevelToXPMap[prestige][level - 1];
            _player["XP"] = intXP.Value;

            _busy = false;
        }

        private int XPToLevel(int currentPrestige, int currentXp)
        {
            const int xpGrowth = SecondLevelXP / 2;

            int xpNeededForCurrentLevel = 0, xpNeededForNextLevel;
            int nextLevelStepSize = xpGrowth, currLevel = 1, nextLevel = 2;

            do
            {
                var scaleBy = XPMultiplier[nextLevel / 5] * PrestigeMultiplier[currentPrestige];
                nextLevelStepSize += (int)(xpGrowth * scaleBy);
                xpNeededForNextLevel = xpNeededForCurrentLevel + nextLevelStepSize;

                nextLevel++;

                if (xpNeededForNextLevel <= currentXp && ++currLevel < MAX_LEVEL)
                    xpNeededForCurrentLevel = xpNeededForNextLevel;

            } while ((xpNeededForNextLevel <= currentXp) && currLevel <= MAX_LEVEL);

            if (currLevel == MAX_LEVEL + 1)
                return MAX_LEVEL;

            return currLevel;
        }

        private static readonly decimal[] XPMultiplier = {
            1.0m,
	        0.60m,
	        0.70m,
        	0.350m,
	        0.350m,
	        0.350m,
	        0.350m,
	        0.350m,
	        0.350m,
	        0.350m,
	        0.350m
        };

        private static readonly decimal[] PrestigeMultiplier = {
            1.0m,
            1.10m,
            1.20m,
            1.30m,
            1.40m,
            1.50m,
            1.60m,
            1.70m,
            1.80m,
            1.90m,
            2.0m,
            2.10m,
            2.20m,
            2.30m
        };

        private const int SecondLevelXP = 1000;

        private static void DefaultCombo(ComboBoxEx comboBoxEx, Action<object, EventArgs> selectedIndexChanged)
        {
            if (comboBoxEx.SelectedIndex == 0)
                selectedIndexChanged(null, null);
            else
                comboBoxEx.SelectedIndex = 0;
        }

        private void LoadValuesToPanel(Control panel)
        {
            _busy = true;

            foreach (var c in panel.Controls.OfType<IntegerInput>().Where(ct => ct.Name != "intXP"))
                c.Value = c.Enabled && c.Tag != null ? _player[(string)c.Tag] : 0;

            foreach (var c in panel.Controls.OfType<CheckBoxX>())
                c.Checked = c.Enabled && c.Tag != null && _player[(string)c.Tag] == 1;
            
            _busy = false;
        }

        private void RegisterControls(params Control[] controls)
        {
            foreach (var c in controls.OfType<IntegerInput>())
                c.ValueChanged += Int32_ValueChanged;

            foreach (var c in controls.OfType<CheckBoxX>())
                c.CheckedChanged += CheckedChanged;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            var i = (CheckBoxX)sender;

            _player[(string)i.Tag] = i.Checked ? 1 : 0;
        }

        private void Int32_ValueChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            var i = (IntegerInput)sender;

            _player[(string)i.Tag] = i.Value;
        }

        public override bool Entry()
        {
            var profileData = ProfileManager.fetchRealProfile(Package.Header.Metadata.Creator);

            if (profileData == null)
            {
                if (UI.messageBox("This tool requires data from your saved game's corresponding Gamer Profile.\n\n"
                    + "If you have this profile on a memory device, insert it into your computer, let it load, then press OK.\n\n"
                    + "If you have this profile on your computer, hit OK to open it.", "Profile Not Found",
                    MessageBoxIcon.Exclamation, MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return false;

            openProfile:

                profileData = ProfileManager.fetchRealProfile(Package.Header.Metadata.Creator);

                if (profileData == null)
                {
                    var ofd = new OpenFileDialog();
                    ofd.FileName = Package.Header.Metadata.Creator.ToString("X16");
                    if (ofd.ShowDialog() != DialogResult.OK)
                        return false;

                    var tempPackage = new XContent.XContentPackage();
                    if (!tempPackage.LoadPackage(ofd.FileName))
                        goto openProfile;

                    if (tempPackage.Header.Metadata.Creator != Package.Header.Metadata.Creator)
                        UI.errorBox("You have selected the incorrect gamer profile!");
                    else
                    {
                        try
                        {
                            ProfileManager.addProfileToCache(tempPackage, new XProfile.XProfileAccount(tempPackage.StfsContentPackage.ExtractFileToArray("Account")));
                            profileData = ProfileManager.fetchRealProfile(tempPackage.Header.Metadata.Creator);
                        }
                        catch
                        {
                            UI.errorBox("Corrupted gamer profile selected!");
                        }
                    }

                    tempPackage.CloseIO(true);
                }
            }

            if (profileData == null)
                return false;

            if (!OpenStfsFile("PlayerStorage.dat"))
                return false;

            _player = new PlayerPersistentData(SettingAsByteArray(52), IO.ToArray(), profileData.XUID, true);

            OnLoad();

            return true;
        }

        public override void Save()
        {
            _player["NeedsViewed"] = 0;

            if (cmdMarkViewed.Checked)
            {
                for (var x = 1; x <= 31; x++)
                    _player["NeedsViewed_" + x] = 0;

                cmdMarkViewed.Checked = false;
                cmdMarkViewed.Enabled = false;
            }

            Package.StfsContentPackage.InjectFileFromArray("PlayerStorage.dat", _player.ToArray(true));
        }

        private void cmdExportStats_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            _player.Export(sfd.FileName);
        }

        internal static readonly Dictionary<string, Dictionary<string, string>> Medals = new Dictionary<string,Dictionary<string,string>>
        {
            //{ "Award_BetaTester", new Dictionary<string, string> {
            //    { "awName", "Beta Tester" },
            //    { "Beta Tester", "Participated in the Beta." },
            //}},
            { "Award_EPIC", new Dictionary<string, string> {
                { "awName", "EPIC" },
                { "I am EPIC", "You are an employee of Epic Games, Inc." },
            }},
            { "Award_RiftDev", new Dictionary<string, string> {
                { "awName", "Gears of War 3 Developer" },
                { "I Made This!", "You helped make this game possible." },
            }},
            { "Award_TheyreComing", new Dictionary<string, string> {
		        { "awName", "They're Coming!" },
		        { "Be Gentle with Me", "Welcome to OverRun! Completed one OverRun match." },
	        }},
	        { "Award_VIP", new Dictionary<string, string> {
		        { "awName", "VIP" },
		        { "VIP", "Signed up for VIP membership." },
	        }},
	        { "Award_GrimReaper", new Dictionary<string, string> {
		        { "awName", "Epic Reaper" },
		        { "Slayer of Samael", "Killed the Epic Reaper in an online match." },
	        }},
	        { "Award_MVP", new Dictionary<string, string> {
		        { "awName", "MVP" },
		        { "Officer and a Gentleman", "Earn 5 MVP ribbons." },
		        { "All-Star Weekend", "Earn 25 MVP ribbons." },
		        { "Humblebragger", "Earn 100 MVP ribbons." },
		        { "His Royal Airness", "Earn 500 MVP ribbons." },
	        }},
	        { "Award_FieldService", new Dictionary<string, string> {
		        { "awName", "Field Service" },
		        { "Good at Gears", "Reach Level 10." },
		        { "Honorary Fenix", "Reach Level 20." },
		        { "Super Elite", "Reach Level 30." },
		        { "Gears Deity", "Reach Level 50." },
	        }},
	        { "Award_Veteran", new Dictionary<string, string> {
		        { "awName", "Veteran" },
		        { "Shell-Shocked", "Play 100 matches." },
		        { "Twitchtastic", "Play 500 matches." },
		        { "Been through Hell", "Play 2000 matches." },
		        { "War Hero", "Play 4000 matches." },
	        }},
	        { "Award_Winner", new Dictionary<string, string> {
		        { "awName", "Match Winner" },
		        { "Meh", "Win 50 matches." },
		        { "Win-Bag", "Win 250 matches." },
		        { "Who's a Winner?", "Win 1000 matches." },
		        { "Professional Victor", "Win 3000 matches." },
	        }},
	        { "Award_Marksman", new Dictionary<string, string> {
		        { "awName", "Headshot" },
		        { "Skeet Skeet", "Get 100 headshots." },
		        { "Sharpshooter", "Get 500 headshots." },
		        { "Headcase", "Get 1500 headshots." },
		        { "Patty Domes", "Get 4000 headshots." },
	        }},
	        { "Award_Heavy", new Dictionary<string, string> {
		        { "awName", "Heavy Weapons" },
		        { "Weight Problem", "Get 50 heavy weapon kills." },
		        { "Getting Heavy", "Get 200 heavy weapon kills." },
		        { "Whoa, That's Heavy", "Get 500 heavy weapon kills." },
		        { "Five Tons of Ouch", "Get 1000 heavy weapon kills." },
	        }},
	        { "Award_Grenadier", new Dictionary<string, string> {
		        { "awName", "Explosives" },
		        { "Pow!", "Get 100 explosive kills." },
		        { "Boom!!", "Get 500 explosive kills." },
		        { "Kaboom!!!", "Get 2000 explosive kills." },
		        { "Mushroom Cloud!!!!", "Get 6000 explosive kills." },
	        }},
	        { "Award_Executions", new Dictionary<string, string> {
		        { "awName", "Finisher" },
		        { "Eliminated", "Get 10 executions in campaign." },
		        { "Assassin", "Get 50 executions in campaign." },
		        { "Liquidated", "Get 100 executions in campaign." },
		        { "Angel of Death", "Get 500 executions in campaign." },
	        }},
	        { "Award_Medic", new Dictionary<string, string> {
		        { "awName", "Medic" },
		        { "Paramedic", "Revive 10 squadmates." },
		        { "Ambulance Chaser", "Revive 50 squadmates." },
		        { "He's Dead, Jim", "Revive 200 squadmates." },
		        { "Dr. Awesome, M.D.", "Revive 500 squadmates." },
	        }},
	        { "Award_Reloader", new Dictionary<string, string> {
		        { "awName", "Active Reloader" },
		        { "Good Timing", "500 perfect active reloads." },
		        { "Damage, Inc.", "2500 perfect active reloads." },
		        { "Would You Like Bullets with That?", "10000 perfect active reloads." },
		        { "Secret Sauce", "25000 perfect active reloads." },
	        }},
	        { "Award_MeleeMaster", new Dictionary<string, string> {
		        { "awName", "Melee Master" },
		        { "Punched Out", "Kill 100 opponents with melee." },
		        { "Mortal Combatant", "Kill 500 opponents with melee." },
		        { "Smash Brother", "Kill 1000 opponents with melee." },
		        { "God Hands", "Kill 3000 opponents with melee." },
	        }},
	        { "Award_UIRMarksman", new Dictionary<string, string> {
		        { "awName", "Markza" },
		        { "Marksman", "Get 250 Markza kills." },
		        { "Trigger Man", "Get 1000 Markza kills." },
		        { "Confirmed Killer", "Get 3000 Markza kills." },
		        { "Master Camper", "Get 6000 Markza kills." },
	        }},
	        { "Award_HammerburstMkOne", new Dictionary<string, string> {
		        { "awName", "Classic Hammerburst" },
		        { "A Good Vintage", "Get 250 Classic Hammerburst kills." },
		        { "Classically Trained", "Get 1000 Classic Hammerburst kills." },
		        { "Dilettante", "Get 3000 Classic Hammerburst kills." },
		        { "Stay Classy", "Get 6000 Classic Hammerburst kills." },
	        }},
	        { "Award_TripWireBow", new Dictionary<string, string> {
		        { "awName", "Tripwire Crossbow" },
		        { "Not My Idea of Courage", "Get 10 It's A Trap! ribbons." },
		        { "An Elegant Weapon", "Get 50 It's A Trap! ribbons." },
		        { "That's No Moon", "Get 100 It's A Trap! ribbons." },
		        { "Let's Blow This Thing", "Get 500 It's A Trap! ribbons." },
	        }},
	        { "Award_Lancer", new Dictionary<string, string> {
		        { "awName", "Lancer" },
		        { "Chainsaw Accident", "Get 250 Lancer kills." },
		        { "Chainsaw Debacle", "Get 1000 Lancer kills." },
		        { "Chainsaw Fiasco", "Get 3000 Lancer kills." },
		        { "Chainsaw Massacre", "Get 6000 Lancer kills." },
	        }},
	        { "Award_HammerBurst", new Dictionary<string, string> {
		        { "awName", "Hammerburst" },
		        { "To Burst, Or Not To Burst", "Get 250 Hammerburst kills." },
		        { "HammerThirst", "Get 1000 Hammerburst kills." },
		        { "You're the Nail", "Get 3000 Hammerburst kills." },
		        { "Hammerburst Hero", "Get 6000 Hammerburst kills." },
	        }},
	        { "Award_RetroLancer", new Dictionary<string, string> {
		        { "awName", "Retro Lancer" },
		        { "Vintage", "Get 250 Retro Lancer kills." },
		        { "Old-Fashioned", "Get 1000 Retro Lancer kills." },
		        { "Antique", "Get 3000 Retro Lancer kills." },
		        { "Retro is OP", "Get 6000 Retro Lancer kills." },
	        }},
	        { "Award_Gnasher", new Dictionary<string, string> {
		        { "awName", "Gnasher Shotgun" },
		        { "Guh-Nasher", "Get 250 Gnasher Shotgun kills." },
		        { "Wall Bouncer", "Get 1000 Gnasher Shotgun kills." },
		        { "Get Bodied", "Get 3000 Gnasher Shotgun kills." },
		        { "Shotgun Nation", "Get 6000 Gnasher Shotgun kills." },
	        }},
	        { "Award_SawedOff", new Dictionary<string, string> {
		        { "awName", "Sawed-off Shotgun" },
		        { "Both Barrels", "Get 250 Sawed-off Shotgun kills." },
		        { "Closed Casketeer", "Get 1000 Sawed-off Shotgun kills." },
		        { "Lookin' Down the Barrel", "Get 3000 Sawed-off Shotgun kills." },
		        { "Wearin' Barrels", "Get 6000 Sawed-off Shotgun kills." },
	        }},
	        { "Award_Pistol", new Dictionary<string, string> {
		        { "awName", "Pistols" },
		        { "Pea-Shooter", "Get 100 pistol kills." },
		        { "Make My Day", "Get 500 pistol kills." },
		        { "Revolver Solver", "Get 1000 pistol kills." },
		        { "Pistol-Packing Mama", "Get 3000 pistol kills." },
	        }},
	        { "Award_Spotter", new Dictionary<string, string> {
		        { "awName", "Spotter" },
		        { "Spotted Ya!", "Spot 1000 enemies." },
		        { "Hey, He's Over Here!", "Spot 3000 enemies." },
		        { "Human Lighthouse", "Spot 7000 enemies." },
		        { "The Most Dangerous Game", "Spot 15000 enemies." },
	        }},
	        { "Award_Incendiary", new Dictionary<string, string> {
		        { "awName", "Pyro" },
		        { "First Degree Burns", "Kill 25 enemies with fire." },
		        { "Likes It Rare", "Kill 100 enemies with fire." },
		        { "Hellfire", "Kill 500 enemies with fire." },
		        { "Watch It All Burn", "Kill 1000 enemies with fire." },
	        }},
	        { "Award_FirstBlood", new Dictionary<string, string> {
		        { "awName", "Shock Trooper" },
		        { "Quick on the Trigger", "Earn 100 First Blood ribbons." },
		        { "Mr. Itchy", "Earn 300 First Blood ribbons." },
		        { "Bad Manners", "Earn 1000 First Blood ribbons." },
		        { "Up in Your Bidness", "Earn 3000 First Blood ribbons." },
	        }},
	        { "Award_GrenadeMaster", new Dictionary<string, string> {
		        { "awName", "Grenade Master" },
		        { "Grenadier", "Kill 200 enemies with any grenade." },
		        { "Pin-Puller", "Kill 800 enemies with any grenade." },
		        { "Grenade Chef", "Kill 2000 enemies with any grenade." },
		        { "Frag King", "Kill 4500 enemies with any grenade." },
	        }},
	        { "Award_SpecialTeams", new Dictionary<string, string> {
		        { "awName", "Special Teams" },
		        { "Nowhere Man", "Get 50 map-based weapon kills." },
		        { "Can Do It All", "Get 250 map-based weapon kills." },
		        { "Multi-Talented", "Get 1000 map-based weapon kills." },
		        { "Master of Your Domain", "Get 3000 map-based weapon kills." },
	        }},
	        { "Award_MasterAtArms", new Dictionary<string, string> {
		        { "awName", "Master-at-Arms" },
		        { "Man-at-Arms", "Kill 1500 enemies with starting weapons." },
		        { "Master-at-Arms", "Kill 6000 enemies with starting weapons." },
		        { "Master-at-Arms Elite", "Kill 18000 enemies with starting weapons." },
		        { "God-at-Arms", "Kill 36000 enemies with starting weapons." },
	        }},
	        { "Award_Rifleman", new Dictionary<string, string> {
		        { "awName", "Rifleman" },
		        { "Rifle Specialist", "Kill 1000 enemies with starting rifles." },
		        { "Rifle Expert", "Kill 4000 enemies with starting rifles." },
		        { "Rifle Artisan", "Kill 12000 enemies with starting rifles." },
		        { "Rifle Master", "Kill 24000 enemies with starting rifles." },
	        }},
	        { "Award_Seriously", new Dictionary<string, string> {
		        { "awName", "Seriously Judgmental" },
		        { "Seriously Judgmental", "Earn Seriously Judgmental achievement." },
	        }},
	        { "Award_EmbryStar", new Dictionary<string, string> {
		        { "awName", "Embry Star" },
		        { "Allfather", "Earn 20 Onyx Medals." },
	        }},
	        { "Award_WarSupport", new Dictionary<string, string> {
		        { "awName", "War Supporter" },
		        { "Socialite", "Play in 5 Gears events." },
		        { "Man about Town", "Play in 10 Gears events." },
		        { "Party Animal", "Play in 20 Gears events." },
		        { "Head to Rehab", "Play in 30 Gears events." },
	        }},
	        { "Award_Survivalist", new Dictionary<string, string> {
		        { "awName", "Survivalist" },
		        { "Invisible Kid", "Complete 10 sections without reloading from a checkpoint." },
		        { "Tough Guy", "Complete 20 sections without reloading from a checkpoint." },
		        { "Survival Artist", "Complete 30 sections without reloading from a checkpoint." },
		        { "Inconceivable!", "Complete 40 sections without reloading from a checkpoint." },
	        }},
	        { "Award_Doorman", new Dictionary<string, string> {
		        { "awName", "Doorman" },
		        { "What's This Button Do?", "Manipulate 50 objects in campaign." },
		        { "Knob-Turner", "Manipulate 100 objects in campaign." },
		        { "Are You the Keymaster?", "Manipulate 150 objects in campaign." },
		        { "Press X to Jason", "Manipulate 200 objects in campaign." },
	        }},
	        { "Award_ObjectivesDestroyed", new Dictionary<string, string> {
		        { "awName", "Search and Destroy" },
		        { "Totaled", "Destroy 50 E-Hole Plugs or Generators in OverRun." },
		        { "In the Area", "Destroy 150 E-Hole Plugs or Generators in OverRun." },
		        { "COGmower", "Destroy 300 E-Hole Plugs or Generators in OverRun." },
		        { "Barbarian at the Gate", "Destroy 500 E-Hole Plugs or Generators in OverRun." },
	        }},
	        { "Award_FortsRepaired", new Dictionary<string, string> {
		        { "awName", "Fort Fixer" },
		        { "Grease Monkey", "Repair 50000 fortification HP." },
		        { "Come at Me, Bro", "Repair 100000 fortification HP." },
		        { "Fixinator", "Repair 150000 fortification HP." },
		        { "Don't Tase Me", "Repair 200000 fortification HP." },
	        }},
	        { "Award_EnemiesDebuffed", new Dictionary<string, string> {
		        { "awName", "Debuffer" },
		        { "Crystal Ball Rubber", "Debuff 100 times." },
		        { "X-Ray Eyes", "Debuff 500 times." },
		        { "On Your Knees!", "Debuff 2000 times." },
		        { "Chump Knocker", "Debuff 6000 times." },
	        }},
	        { "Award_AmmoGiven", new Dictionary<string, string> {
		        { "awName", "Ammo Giver" },
		        { "Friend with Benefits", "Supply Ammo 100 times." },
		        { "Back-Scratcher", "Supply Ammo 500 times." },
		        { "Quid Pro Quo", "Supply Ammo 2000 times." },
		        { "Magnificent Bastard", "Supply Ammo 6000 times." },
	        }},
	        { "Award_FriendsHealed", new Dictionary<string, string> {
		        { "awName", "Healer" },
		        { "Street Apothecary", "As a Kantus,  heal or revive 250 teammates in OverRun." },
		        { "Bring Out Yer Dead!", "As a Kantus,  heal or revive 750 teammates in OverRun." },
		        { "Bacon Saver", "As a Kantus,  heal or revive 1500 teammates in OverRun." },
		        { "Lover and Fighter", "As a Kantus,  heal or revive 3000 teammates in OverRun." },
	        }},
	        { "Award_CampaignTour", new Dictionary<string, string> {
		        { "awName", "Tour of Duty" },
		        { "Medal of Duty", "Complete campaign on Casual." },
		        { "Seen Some Action", "Complete campaign on Normal." },
		        { "Combat Veteran", "Complete campaign on Hardcore." },
		        { "General Chaos", "Complete campaign on Insane." },
	        }},
	        { "Award_Infinite", new Dictionary<string, string> {
		        { "awName", "Survivor" },
		        { "Oxygen Addict", "Complete all 10 waves of Survival on Casual difficulty." },
		        { "Hard to Kill", "Complete all 10 waves of Survival on Normal difficulty." },
		        { "Black Chopper Watcher", "Complete all 10 waves of Survival on Hardcore difficulty." },
		        { "There Can Be Only One", "Complete all 10 waves of Survival on Insane difficulty." },
	        }},
	        { "Award_Coalition", new Dictionary<string, string> {
		        { "awName", "Coalition" },
		        { "That's a Lotta Blood", "Kill 250 Locust in Survival." },
		        { "Bloodbather", "Kill 1000 Locust in Survival." },
		        { "Killing Machine", "Kill 3000 Locust in Survival." },
		        { "Fog of War", "Kill 6000 Locust in Survival." },
	        }},
	        { "Award_Defender", new Dictionary<string, string> {
		        { "awName", "Defender" },
		        { "Wave Ridin'", "Defend the first E-Hole until Wave 5 a total of 3 times." },
		        { "Back Off, Sucka!", "Defend the first E-Hole until Wave 5 a total of 6 times." },
		        { "Defensive Lineman", "Defend the first E-Hole until Wave 5 a total of 12 times." },
		        { "Defender of the Hole", "Defend the first E-Hole until Wave 5 a total of 25 times." },
	        }},
	        { "Award_Horder", new Dictionary<string, string> {
		        { "awName", "Horder" },
		        { "Might Have a Problem", "Play 20 waves of Survival." },
		        { "Seeking Help", "Play 100 waves of Survival." },
		        { "Certified Horder", "Play 400 waves of Survival." },
		        { "Kind of a Big Deal", "Play 1000 waves of Survival." },
	        }},
	        { "Award_ClassAct", new Dictionary<string, string> {
		        { "awName", "Lockdown" },
		        { "Defense in Depth", "Defend 5 Generators in OverRun." },
		        { "Billy Blockade", "Defend 25 Generators in OverRun." },
		        { "Locust FAIL", "Defend 100 Generators in OverRun." },
		        { "Slave to the COG", "Defend 200 Generators in OverRun." },
	        }},
	        { "Award_FortDestroyer", new Dictionary<string, string> {
		        { "awName", "Fort Destroyer" },
		        { "Wrecking Ball", "Destroy 100 fortifications in OverRun." },
		        { "Master of Disaster", "Destroy 500 fortifications in OverRun." },
		        { "Welcome to Smashtown", "Destroy 1000 fortifications in OverRun." },
		        { "Loot Brute", "Destroy 2000 fortifications in OverRun." },
	        }},
	        { "Award_Anarchist", new Dictionary<string, string> {
		        { "awName", "Anarchist" },
		        { "Needs a Breath Mint", "Stun 100 opponents as a Wretch." },
		        { "Head-Turner", "Stun 500 opponents as a Wretch." },
		        { "Truly Wretched", "Stun 1000 opponents as a Wretch." },
		        { "Master Orator", "Stun 2000 opponents as a Wretch." },
	        }},
	        { "Award_Slayer", new Dictionary<string, string> {
		        { "awName", "Slayer" },
		        { "Body Counter", "Kill 250 opponents as COG or Locust." },
		        { "Noobicide", "Kill 1000 opponents as COG or Locust." },
		        { "Captain Bodybag", "Kill 3000 opponents as COG or Locust." },
		        { "Space Cowboy", "Kill 6000 opponents as COG or Locust." },
	        }},
	        { "Award_Champion", new Dictionary<string, string> {
		        { "awName", "Champion" },
		        { "Bench Warmer", "Play as a 2nd Tier Locust 10 times." },
		        { "Bag of Awesome", "Play as a 2nd Tier Locust 100 times." },
		        { "Wreckin' Shop", "Play as a 2nd Tier Locust 250 times." },
		        { "Beastmaster", "Play as a 2nd Tier Locust 500 times." },
	        }}
        };

        private static readonly Dictionary<string, string[]> Ribbons = new Dictionary<string,string[]>
        {
            { "Award_MVP", new[] {
		        "MVP",
		        "Highest point total for the match."
	        }},
	        { "Award_FirstBlood", new[] {
		        "First Blood",
		        "Earned the first kill of the round."
	        }},
	        { "Award_Fragtastic", new[] {
		        "Clusterluck",
		        "Killed multiple opponents or enemies with one grenade."
	        }},
	        { "Award_Retribution", new[] {
		        "Retribution",
		        "Killed your nemesis."
	        }},
	        { "Award_DeathFromBeyond", new[] {
		        "Death from Beyond",
		        "Killed an opponent or an enemy after you have died."
	        }},
	        { "Award_Spree25", new[] {
		        "The Unicorn",
		        "Killed 25 opponents or enemies without dying."
	        }},
	        { "Award_Spree20", new[] {
		        "Invincible",
		        "Killed 20 opponents or enemies without dying."
	        }},
	        { "Award_Spree15", new[] {
		        "Unstoppable",
		        "Killed 15 opponents or enemies without dying."
	        }},
	        { "Award_Spree10", new[] {
		        "Rampage",
		        "Killed 10 opponents or enemies without dying."
	        }},
	        { "Award_Spree5", new[] {
		        "Spree",
		        "Killed 5 opponents or enemies without dying."
	        }},
	        { "Award_HelpingHand", new[] {
		        "Executive Assistant",
		        "Assisted 10 kills in a round."
	        }},
	        { "Award_ChainBang", new[] {
		        "Lumberjack",
		        "Chainsawed 3 opponents or enemies in a row."
	        }},
	        { "Award_Charge", new[] {
		        "Charge!",
		        "Retro charged 3 opponents or enemies in a row."
	        }},
	        { "Award_Expendable", new[] {
		        "F.I.F.O.",
		        "First to die in a round."
	        }},
	        { "Award_TeamPlayer", new[] {
		        "Team Player",
		        "Most assists in a match."
	        }},
	        { "Award_LastKill", new[] {
		        "Coup de Grace",
		        "Final kill of the match."
	        }},
	        { "Award_ForwardObserver", new[] {
		        "Military Intelligence",
		        "3 opponents spotted ending in a kill."
	        }},
	        { "Award_FFAGold", new[] {
		        "Gold",
		        "1st in a Free For All match."
	        }},
	        { "Award_FFASilver", new[] {
		        "Silver",
		        "2nd in a Free For All match."
	        }},
	        { "Award_FFABronze", new[] {
		        "Bronze",
		        "3rd in a Free For All match."
	        }},
	        { "Award_GrenadeAttach", new[] {
		        "Takin' You with Me",
		        "Killed an opponent or an enemy after being grenade tagged."
	        }},
	        { "Award_Boombadier", new[] {
		        "Boombardier",
		        "Killed multiple enemies with a single Boomshot blast."
	        }},
	        { "Award_SawnOff", new[] {
		        "Nothin' but Bits",
		        "Killed multiple enemies with one Sawed-off Shotgun blast."
	        }},
	        { "Award_Roadblock", new[] {
		        "Roadblock",
		        "Stopped a Retro charge with the Sawed-off Shotgun."
	        }},
	        { "Award_RoadieRun", new[] {
		        "Oscar Mike",
		        "Killed a Roadie Running opponent or enemy with a headshot."
	        }},
	        { "Award_Evade", new[] {
		        "Headless Evadesman",
		        "Killed an evading opponent or enemy with a headshot."
	        }},
	        { "Award_Ole", new[] {
		        "Ole!",
		        "Grenade tagged a Retro charging opponent."
	        }},
	        { "Award_HatTrick", new[] {
		        "Hat Trick",
		        "Scored 3 headshots in a row without dying."
	        }},
	        { "Award_YoureIt", new[] {
		        "You're It!",
		        "Tagged grenade kill from over 50 feet."
	        }},
	        { "Award_TheDouble", new[] {
		        "The Double",
		        "Killed 2 opponents quickly in a row."
	        }},
	        { "Award_TheTriple", new[] {
		        "The Triple",
		        "Killed 3 opponents quickly in a row."
	        }},
	        { "Award_TheQuad", new[] {
		        "The Quad",
		        "Killed 4 opponents quickly in a row."
	        }},
	        { "Award_TheQuinn", new[] {
		        "The Grayson",
		        "Killed 5 opponents quickly in a row."
	        }},
	        { "Award_Vigilant", new[] {
		        "Flawless Victory",
		        "Won a match with no deaths and 10+ kills."
	        }},
	        { "Award_Solid", new[] {
		        "In the Black",
		        "More kills than deaths in a match."
	        }},
	        { "Award_SmoothOperator", new[] {
		        "Smooth Operator",
		        "Highest K/D ratio in a match."
	        }},
	        { "Award_HardTarget", new[] {
		        "Artful Dodger",
		        "Fewest deaths in a match."
	        }},
	        { "Award_BulletMagnet", new[] {
		        "Rough Day",
		        "Most deaths in a match."
	        }},
	        { "Award_Nemesis", new[] {
		        "Nemesis",
		        "Killed the same opponent 5 times."
	        }},
	        { "Award_TrickShot", new[] {
		        "William Tell Overture",
		        "1 Torque Bow headshot, followed by a direct hit kill."
	        }},
	        { "Award_HailMary", new[] {
		        "Hail Mary",
		        "Boomshot kill from over 100 feet."
	        }},
	        { "Award_SwiftVengeance", new[] {
		        "Vengeance Is Yours",
		        "Revenge-killed your last killer."
	        }},
	        { "Award_Evasive", new[] {
		        "Evasive",
		        "Least damage taken in a match."
	        }},
	        { "Award_Contender", new[] {
		        "Contender",
		        "Most melee hits in a match."
	        }},
	        { "Award_SprayAndPray", new[] {
		        "Spray and Pray",
		        "Most blindfire kills in a match."
	        }},
	        { "Award_Headhunter", new[] {
		        "Headmaster",
		        "Most headshot kills in a match."
	        }},
	        { "Award_CarminesStar", new[] {
		        "Carmine's Star",
		        "Most headshot deaths in a match."
	        }},
	        { "Award_Grenadier", new[] {
		        "Grenadier",
		        "Most grenade kills in a match."
	        }},
	        { "Award_Pistoleer", new[] {
		        "Sureshot",
		        "Most pistol kills in a match."
	        }},
	        { "Award_KillJoy", new[] {
		        "Shut It Down!",
		        "Ended an opponent's kill streak."
	        }},
	        { "Award_DeathFromAbove", new[] {
		        "Death from Above",
		        "Killed multiple opponents with a single HOD blast."
	        }},
	        { "Award_SpecialDelivery", new[] {
		        "Special Delivery",
		        "Killed an opponent with a bag & tag."
	        }},
	        { "Award_NoWait", new[] {
		        "No, Wait!",
		        "Killed a player while they reloaded."
	        }},
	        { "Award_ShowedUp", new[] {
		        "Participant",
		        "Earned no other ribbons in a match."
	        }},
	        { "Award_CloseShave", new[] {
		        "Close Shave",
		        "Won a match with a margin of 1."
	        }},
	        { "Award_Imprecision", new[] {
		        "Death Blossom",
		        "Killed 5 consecutive opponents or enemies while blindfiring."
	        }},
	        { "Award_Capper", new[] {
		        "Capper",
		        "Most points captured in a match."
	        }},
	        { "Award_BornLeader", new[] {
		        "Born Leader",
		        "Stay in the lead for the entire match."
	        }},
	        { "Award_Breaker", new[] {
		        "Breaker Breaker",
		        "Most capture points broken."
	        }},
	        { "Award_Dominator", new[] {
		        "Dominator",
		        "Won a match with all three points captured."
	        }},
	        { "Award_OpportunityKnocks", new[] {
		        "Opportunity Knocks",
		        "Killed 10 players while they took damage from another player."
	        }},
	        { "Award_Sticky", new[] {
		        "Ew, Sticky",
		        "Killed 5 players with a direct sticky grenade hit."
	        }},
	        { "Award_SticksToKids", new[] {
		        "Codependent",
		        "Killed multiple opponents with a grenade stuck to the player."
	        }},
	        { "Award_FireWalkWithMe", new[] {
		        "Fire Walk With Me",
		        "Killed 5 players with fire without dying."
	        }},
	        { "Award_PopGoesTheWeasel", new[] {
		        "Pop Goes the Weasel",
		        "Blew up 3 enemies at once (Ticker)."
	        }},
	        { "Award_Indigestion", new[] {
		        "Indigestion",
		        "Killed an enemy with a swallowed grenade (Ticker)."
	        }},
	        { "Award_Distractor", new[] {
		        "Stunner",
		        "Multiple enemies you stunned were killed (Wretch)."
	        }},
	        { "Award_KantusShaman", new[] {
		        "Team Shaman",
		        "Healed 4 teammates at once (Kantus)."
	        }},
	        { "Award_KantusSavior", new[] {
		        "Team Savior",
		        "Revived 3 teammates at once (Kantus)."
	        }},
	        { "Award_Pillager", new[] {
		        "Homewrecker",
		        "Destroyed 5 fortifications in a round."
	        }},
	        { "Award_TestDriver", new[] {
		        "Sample Platter",
		        "Played as 5 different Locust in a round."
	        }},
	        { "Award_Pointman", new[] {
		        "Point Monster",
		        "Earned the most points in the round while playing as Locust."
	        }},
	        { "Award_RightPlace", new[] {
		        "Right Place, Right Time",
		        "Killed 5 Locust with your well placed Sentry."
	        }},
	        { "Award_Mole", new[] {
		        "Tunnel Rat",
		        "Dug under a COG fortification and damaged the objective."
	        }},
	        { "Award_BatteringRam", new[] {
		        "Battering Ram",
		        "Dealt 50% of damage to the generator in one round of OverRun."
	        }},
	        { "Award_Trickster", new[] {
		        "Oakley",
		        "Kicked a Ticker and shoot it out of the air."
	        }},
	        { "Award_Interrupter", new[] {
		        "Beg Your Pardon",
		        "Chainsawed a Kantus while it was healing."
	        }},
	        { "Award_OneBeastArmy", new[] {
		        "One Beast Army",
		        "Destroy all 3 objectives in an Overrun round."
	        }},
	        { "Award_Spotter", new[] {
		        "Spotter",
		        "5 Spot Grenade tagged enemies killed in a round of OverRun."
	        }},
	        { "Award_Emergency", new[] {
		        "HMO",
		        "Restored 20,000 points of health to your teammates."
	        }},
	        { "Award_ChainOfCommand", new[] {
		        "Chain of Command",
		        "Killed 5 enemies debuffed by a Scout."
	        }},
	        { "Award_SolidDefender", new[] {
		        "Iron Curtain",
		        "Protected the generator for 10 minutes."
	        }},
	        { "Award_Feeder", new[] {
		        "Grenade Feeder",
		        "Fed 3 tickers a grenade."
	        }},
	        { "Award_Eater", new[] {
		        "Insatiable",
		        "Ate 10 grenades."
	        }},
	        { "Award_SuicideBomber", new[] {
		        "One More Thing...",
		        "Killed multiple players with a frag grenade while DBNO."
	        }},
	        { "Award_Infiltrator", new[] {
		        "Tree House Predator",
		        "Killed a Scout on a perch."
	        }},
	        { "Award_CounterStrike", new[] {
		        "Stay Down",
		        "Melee-killed a Wretch while it's climbing."
	        }},
	        { "Award_BruteForce", new[] {
		        "Brute Force",
		        "Melee-killed a Corpser while it's attacking an objective."
	        }},
	        { "Award_PavingTheWay", new[] {
		        "Paving The Way",
		        "Destroyed a sentry while burrowed."
	        }},
	        { "Award_ThatLovingHealing", new[] {
		        "That Loving Healing",
		        "Healed a Kantus who is healing another team member."
	        }},
	        { "Award_BackUpPlan", new[] {
		        "Scoutmaster",
		        "Headshotted a Scout with the Breechshot."
	        }},
	        { "Award_EmergencyRoom", new[] {
		        "Nice Try",
		        "Won a chainsaw duel against a buffed Medic."
	        }},
	        { "Award_MeatSpin", new[] {
		        "Spinning Plates",
		        "Gibbed 3 COG  with a single Spin-Shield use."
	        }},
	        { "Award_Misdirection", new[] {
		        "Slaughterhouse",
		        "Killed a COG with reflected bullets."
	        }},
	        { "Award_NickOfTime", new[] {
		        "Nick Of Time",
		        "Destroyed the objective with 10 seconds remaining."
	        }},
	        { "Award_Beat10Waves", new[] {
		        "Survivor",
		        "Completed all 10 waves of a Survival match."
	        }},
	        { "Award_MechJockey", new[] {
		        "Nice Suit",
		        "Killed 10 enemies with a Silverback."
	        }},
	        { "Award_QuickKicker", new[] {
		        "And the Kick Is Up!",
		        "Kicked 5 small enemies."
	        }},
	        { "Award_SkeetSkeet", new[] {
		        "Pull!",
		        "Killed a ground bursting enemy in the air."
	        }},
	        { "Award_TheCloser", new[] {
		        "Plug That Hole",
		        "Closed an E-Hole with explosives."
	        }},
	        { "Award_Flamegrilled", new[] {
		        "Flamebroiled",
		        "Killed 3 enemies in a row with the Scorcher."
	        }},
	        { "Award_StayDown", new[] {
		        "Rage Denied",
		        "Killed an enraged Rager with a melee attack."
	        }},
	        { "Award_Skewered", new[] {
		        "Shish-kashot",
		        "Killed at least two enemies with a single OneShot round."
	        }},
	        { "Award_NoneShallPass", new[] {
		        "None Shall Pass",
		        "Killed 10 enemies in a row with Troika, Chaingun or Mulcher."
	        }},
	        { "Award_LikeABoss", new[] {
		        "I'm Your Huckleberry",
		        "Killed 5 enemies in a row with any type of pistol."
	        }},
	        { "Award_OnceMoreUntoTheBreech", new[] {
		        "Once More Unto The Breech",
		        "Killed 5 enemies in a row with the Breechshot."
	        }},
	        { "Award_ItsATrap", new[] {
		        "It's A Trap!",
		        "Killed multiple enemies with a single Tripwire Crossbow shot."
	        }},
	        { "Award_CanIKickIt", new[] {
		        "Can I Kick It?",
		        "Mantle kicked 5 enemies resulting in a kill."
	        }},
        };

        private static readonly TagMap WeaponSkins = new TagMap
        {
            { "Chrome", "Chrome" },
            { "BurnSpray", "Burn Spray" },
            { "Gold", "Gold" },
            { "RibCage", "Rib Cage" },
            { "Onyx", "Onyx" },
            { "XBox", "Xbox" },
            { "Reaper", "Reaper Slayer" }
        };

        private static readonly TagMap CharacterSkins = new TagMap
        {
            { "SkeletalGlow", "Skeletal (Glow)" },
            { "Copper", "Copper" },
            { "Chrome", "Chrome" },
            { "Bee", "Bee" },
            { "Gold", "Gold" },
            { "Mummy", "Mummy" },
            { "CelShaded", "Cel Shaded" }
        };

        private static readonly TagMap UnlockableCharacters = new TagMap
        {
            { "PadukG3", "Garron (Aftermath)" },
            { "ColeG3", "Cole (Aftermath)" },
            { "TaiRaam", "Tai Kaliso" },
            { "MinhRaam", "Minh Young Kim" },
            { "LoomisJudgment", "Ezra Loomis" }
        };

        private static readonly List<int[]> LevelToXPMap = new List<int[]> {
	        new[] { 0, 1000, 2500, 4500, 6800, 9400, 12300, 15500, 19000, 22850, 27050, 31600, 36500, 41750, 47175, 52775, 58550, 64500, 70625, 76925, 83400, 90050, 96875, 103875, 111050, 118400, 125925, 133625, 141500, 149550, 157775, 166175, 174750, 183500, 192425, 201525, 210800, 220250, 229875, 239675, 249650, 259800, 270125, 280625, 291300, 302150, 313175, 324375, 335750, 347300 },
	        new[] { 0, 1050, 2650, 4800, 7280, 10090, 13230, 16700, 20500, 24685, 29255, 34210, 39550, 45275, 51192, 57301, 63602, 70095, 76780, 83657, 90726, 97987, 105440, 113085, 120922, 128951, 137172, 145585, 154190, 162987, 171976, 181157, 190530, 200095, 209852, 219801, 229942, 240275, 250800, 261517, 272426, 283527, 294820, 306305, 317982, 329851, 341912, 354165, 366610, 379247 },
	        new[] { 0, 1100, 2800, 5100, 7760, 10780, 14160, 17900, 22000, 26520, 31460, 36820, 42600, 48800, 55210, 61830, 68660, 75700, 82950, 90410, 98080, 105960, 114050, 122350, 130860, 139580, 148510, 157650, 167000, 176560, 186330, 196310, 206500, 216900, 227510, 238330, 249360, 260600, 272050, 283710, 295580, 307660, 319950, 332450, 345160, 358080, 371210, 384550, 398100, 411860 },
	        new[] { 0, 1150, 2950, 5400, 8240, 11470, 15090, 19100, 23500, 28355, 33665, 39430, 45650, 52325, 59227, 66356, 73712, 81295, 89105, 97142, 105406, 113897, 122615, 131560, 140732, 150131, 159757, 169610, 179690, 189997, 200531, 211292, 222280, 233495, 244937, 256606, 268502, 280625, 292975, 305552, 318356, 331387, 344645, 358130, 371842, 385781, 399947, 414340, 428960, 443807 },
	        new[] { 0, 1200, 3100, 5700, 8720, 12160, 16020, 20300, 25000, 30190, 35870, 42040, 48700, 55850, 63245, 70885, 78770, 86900, 95275, 103895, 112760, 121870, 131225, 140825, 150670, 160760, 171095, 181675, 192500, 203570, 214885, 226445, 238250, 250300, 262595, 275135, 287920, 300950, 314225, 327745, 341510, 355520, 369775, 384275, 399020, 414010, 429245, 444725, 460450, 476420 },
	        new[] { 0, 1250, 3250, 6000, 9200, 12850, 16950, 21500, 26500, 32025, 38075, 44650, 51750, 59375, 67262, 75411, 83822, 92495, 101430, 110627, 120086, 129807, 139790, 150035, 160542, 171311, 182342, 193635, 205190, 217007, 229086, 241427, 254030, 266895, 280022, 293411, 307062, 320975, 335150, 349587, 364286, 379247, 394470, 409955, 425702, 441711, 457982, 474515, 491310, 508367 },
	        new[] { 0, 1300, 3400, 6300, 9680, 13540, 17880, 22700, 28000, 33860, 40280, 47260, 54800, 62900, 71280, 79940, 88880, 98100, 107600, 117380, 127440, 137780, 148400, 159300, 170480, 181940, 193680, 205700, 218000, 230580, 243440, 256580, 270000, 283700, 297680, 311940, 326480, 341300, 356400, 371780, 387440, 403380, 419600, 436100, 452880, 469940, 487280, 504900, 522800, 540980 },
	        new[] { 0, 1350, 3550, 6600, 10160, 14230, 18810, 23900, 29500, 35695, 42485, 49870, 57850, 66425, 75297, 84466, 93932, 103695, 113755, 124112, 134766, 145717, 156965, 168510, 180352, 192491, 204927, 217660, 230690, 244017, 257641, 271562, 285780, 300295, 315107, 330216, 345622, 361325, 377325, 393622, 410216, 427107, 444295, 461780, 479562, 497641, 516017, 534690, 553660, 572927 },
	        new[] { 0, 1400, 3700, 6900, 10640, 14920, 19740, 25100, 31000, 37530, 44690, 52480, 60900, 69950, 79315, 88995, 98990, 109300, 119925, 130865, 142120, 153690, 165575, 177775, 190290, 203120, 216265, 229725, 243500, 257590, 271995, 286715, 301750, 317100, 332765, 348745, 365040, 381650, 398575, 415815, 433370, 451240, 469425, 487925, 506740, 525870, 545315, 565075, 585150, 605540 },
	        new[] { 0, 1450, 3850, 7200, 11120, 15610, 20670, 26300, 32500, 39365, 46895, 55090, 63950, 73475, 83332, 93521, 104042, 114895, 126080, 137597, 149446, 161627, 174140, 186985, 200162, 213671, 227512, 241685, 256190, 271027, 286196, 301697, 317530, 333695, 350192, 367021, 384182, 401675, 419500, 437657, 456146, 474967, 494120, 513605, 533422, 553571, 574052, 594865, 616010, 637487 },
	        new[] { 0, 1500, 4000, 7500, 11600, 16300, 21600, 27500, 34000, 41200, 49100, 57700, 67000, 77000, 87350, 98050, 109100, 120500, 132250, 144350, 156800, 169600, 182750, 196250, 210100, 224300, 238850, 253750, 269000, 284600, 300550, 316850, 333500, 350500, 367850, 385550, 403600, 422000, 440750, 459850, 479300, 499100, 519250, 539750, 560600, 581800, 603350, 625250, 647500, 670100 },
	        new[] { 0, 1550, 4150, 7800, 12080, 16990, 22530, 28700, 35500, 43035, 51305, 60310, 70050, 80525, 91367, 102576, 114152, 126095, 138405, 151082, 164126, 177537, 191315, 205460, 219972, 234851, 250097, 265710, 281690, 298037, 314751, 331832, 349280, 367095, 385277, 403826, 422742, 442025, 461675, 481692, 502076, 522827, 543945, 565430, 587282, 609501, 632087, 655040, 678360, 702047 },
	        new[] { 0, 1600, 4300, 8100, 12560, 17680, 23460, 29900, 37000, 44870, 53510, 62920, 73100, 84050, 95385, 107105, 119210, 131700, 144575, 157835, 171480, 185510, 199925, 214725, 229910, 245480, 261435, 277775, 294500, 311610, 329105, 346985, 365250, 383900, 402935, 422355, 442160, 462350, 482925, 503885, 525230, 546960, 569075, 591575, 614460, 637730, 661385, 685425, 709850, 734660 },
        };

        private void cmdMaxRibbons_Click(object sender, EventArgs e)
        {
            foreach (Node node in listRibbons.Nodes)
                ((IntegerInput) node.Cells[1].HostedControl).Value = intRibbonCount.Value;
        }
    }
}
