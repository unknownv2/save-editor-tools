using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace Horizon.PackageEditors.Gears_of_War_Judgment.Profile
{
    using TagMap = Dictionary<string, string>;
    using Int32Map = Dictionary<string, int>;

    public partial class ProfileEditor : EditorControl
    {
        public ProfileEditor()
        {
            _busy = true;

            InitializeComponent();
            TitleID = FormID.GearsOfWarJudgment;

#if INT2
            cmdExportSettings.Visible = true;
#endif

            cbMedalTier.Items.AddRange(new object[] {
                "Bronze", "Silver", "Gold", "Onyx"
            });

            var i = new int[0];
            PopulateComboBox(cbTitle, Titles, i);
            PopulateComboBox(cbMedal, Medals, i);
            PopulateComboBox(cbCog, COGCharacters, i);

            _busy = false;
        }

        private void Bool_ValueChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            var i = (CheckBoxItem)sender;

            _profile[(string)i.Tag] = i.Checked ? 1 : 0;
        }

        private void Float_ValueChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            var i = (DoubleInput)sender;

            _profile.Set((string)i.Tag, (float)i.Value);
        }

        private void Int32_ValueChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            var i = (IntegerInput)sender;

            _profile[(string)i.Tag] = i.Value;
        }

        private const string None = "-- None --", UnknownFormat = "Unknown DLC[{0}]";
        private ProfileSettings _profile;
        private bool _busy;

        private void OnLoad()
        {
            _busy = true;


            //---------------------------START MAIN STATS----------------------------


            listMain.Nodes.Clear();

            foreach (var cat in Stats)
            {
                var catNode = new Node(cat.Key);

                foreach (var stat in cat.Value)
                {
                    var currentKey = stat.Key;

                    if (currentKey == "GoodHostRatio")
                    {
                        var doubleInput = new DoubleInput();
                        doubleInput.Tag = stat.Key;
                        doubleInput.Value = _profile.FloatValue(currentKey);
                        doubleInput.MinValue = 0;
                        doubleInput.MaxValue = 999999999;
                        doubleInput.Increment = 0.01;
                        doubleInput.ShowUpDown = true;
                        doubleInput.ValueChanged += Float_ValueChanged;

                        var node = new Node(stat.Value);
                        node.Cells.Add(new Cell { HostedControl = doubleInput });
                        catNode.Nodes.Add(node);
                    }
                    else if (cat.Key == "Flags")
                    {
                        var ckInput = new CheckBoxItem();
                        ckInput.Tag = stat.Key;

                        if (_profile[currentKey] == 1)
                            ckInput.CheckState = CheckState.Checked;

                        ckInput.CheckedChanged += Bool_ValueChanged;
                        ckInput.Text = stat.Value;

                        var node = new Node();
                        node.HostedItem = ckInput;
                        catNode.Nodes.Add(node);
                    }
                    else
                        catNode.Nodes.Add(CreateInt32Node(currentKey, stat.Value, 999999999));
                }

                listMain.Nodes.Add(catNode);
            }

            /*var gameTypesNode = new Node("Game Types");

            foreach (var gameType in GameTypeStats)
            {
                var typeNode = new Node(gameType.Key);

                foreach (var stat in gameType.Value)
                    typeNode.Nodes.Add(CreateInt32Node(stat.Key, stat.Value, 999999999));

                gameTypesNode.Nodes.Add(typeNode);
            }

            gameTypesNode.Nodes.Sort();
            listMain.Nodes.Add(gameTypesNode);*/


            //---------------------------START SKINS----------------------------


            listMain.Nodes.Add(CreateComboBoxNodes("Character Skins", "PawnShader_{0}", COGCharacterNames, CharacterSkins, DLCCharacterSkins));
            listMain.Nodes.Add(CreateComboBoxNodes("Weapon Skins", "WeaponSkin_{0}", WeaponNames, WeaponSkins, DLCWeaponSkins));


            //---------------------------START CHARACTERS----------------------------


            RefreshComboBox(cbCog, "PreferredCOGCharacter", COGCharacters);


            //---------------------------START COLLECTABLES----------------------------


            listCogTags.Nodes.Clear();

            int x = 1;

            var paddingImage = new Bitmap(1, 30);

            foreach (var act in Collectables)
            {
                var actNode = new Node();

                var checkAll = new CheckBoxItem();
                checkAll.Text = ActTitles[act.Key];
                checkAll.Tag = actNode;
                checkAll.CheckedChanged += CheckAll_CheckedChanged;
                actNode.HostedItem = checkAll;

                foreach (var collectable in act.Value.Cast<string[]>())
                {
                    var node = new Node();
                    node.Image = paddingImage;
                    node.Tag = x;

                    var ckInput = new CheckBoxItem();
                    ckInput.Tag = node;

                    if ((_profile[CollectableToKey(x)] & CollectableToMask(x)) != 0)
                        ckInput.CheckState = CheckState.Checked;

                    ckInput.CheckedChanged += Collectable_CheckedChanged;
                    ckInput.Text = string.Format("<b>{0}</b><br></br>{1}", collectable[0], FatxHandle.makeGrayText(collectable[1]));
                    node.HostedItem = ckInput;

                    actNode.Nodes.Add(node);

                    x++;
                }

                UpdateCheckAllNode(actNode);

                listCogTags.Nodes.Add(actNode);
            }


            //---------------------------START TITLES----------------------------


            RefreshComboBox(cbTitle, "SelectedTitle", Titles);

            
            //---------------------------START MEDALS----------------------------


            var currentMedal = _profile["SelectedMedal"];

            cbMedalTier.Enabled = false;

            _busy = true;

            if (Medals.ContainsValue(currentMedal))
                cbMedal.SelectedItem = Medals.First(m => m.Value == currentMedal).Key;
            else
            {
                foreach (var medal in Medals.Where(m => !SingleMedals.Contains(m.Value)))
                {
                    int medalVal = medal.Value, p3 = medalVal + 3;

                    if (currentMedal <= medalVal || currentMedal > p3)
                        continue;

                    cbMedalTier.Enabled = true;

                    if (currentMedal == p3)
                        cbMedalTier.SelectedIndex = 3;
                    else if (currentMedal == medalVal + 2)
                        cbMedalTier.SelectedIndex = 2;
                    else if (currentMedal == medalVal + 1)
                        cbMedalTier.SelectedIndex = 1;
                    else
                        cbMedalTier.SelectedIndex = 0;

                    cbMedal.SelectedItem = medal.Key;
                }

                if (!cbMedalTier.Enabled)
                {
                    var unkMedal = string.Format(UnknownFormat, currentMedal);
                    cbMedal.Items.Add(unkMedal);
                    Medals.Add(unkMedal, currentMedal);
                }
            }

            _busy = false;

            cbMedal_SelectedIndexChanged(null, null);


            //---------------------------START CAMPAIGN----------------------------


            listChapters.Nodes.Clear();

            var campaignArr = _profile.BlobValue("ChaptersScore");

            if (campaignArr == null || campaignArr.Length == 0)
            {
                campaignArr = new byte[61];
                campaignArr[3] = 57;
            }
            else if (campaignArr.Length != 61)
            {
                Array.Resize(ref campaignArr, 61);
                campaignArr[3] = 57;
            }

            _campaign = campaignArr;

            for (x = 0; x < Chapters.Count; x++)
            {
                var actOffset = 10 * x;

                var actNode = new Node(ActTitles[x]);

                var currentAct = Chapters[x];

                for (int c = 0, chapterOffset = actOffset; c < currentAct.Length; c++, chapterOffset++)
                {
                    var chapterNode = new Node();
                    chapterNode.Tag = chapterOffset;

                    var ckUnlocked = new CheckBoxItem();
                    ckUnlocked.Tag = chapterNode;
                    ckUnlocked.Text = (c + 1).ToString() + ": " + currentAct[c];

                    var unlocked = IsChapterUnlocked(chapterOffset);

                    if (chapterOffset == 0)
                    {
                        unlocked = true;
                        ckUnlocked.Enabled = false;
                    }

                    if (unlocked)
                    {
                        _unlockedChapters++;
                        ckUnlocked.CheckState = CheckState.Checked;
                    }

                    chapterNode.HostedItem = ckUnlocked;

                    ckUnlocked.CheckedChanged += ChapterUnlocked_CheckedChanged;


                    var combo = new ComboBoxEx();
                    combo.Tag = chapterNode;
                    combo.Items.AddRange(new object[] { "--Not Beaten--", "Casual", "Normal", "Hardcore", "Insane" });
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;
                    combo.FlatStyle = FlatStyle.Standard;
                    combo.FocusCuesEnabled = false;
                    combo.SelectedIndex = GetHighestDifficulty(chapterOffset) + 1;
                    combo.SelectedIndexChanged += CompletedDifficulty_SelectedIndexChanged;
                    combo.Enabled = unlocked;

                    chapterNode.ExpandVisibility = combo.SelectedIndex == 0 ? eNodeExpandVisibility.Hidden : eNodeExpandVisibility.Visible;

                    chapterNode.Cells.Add(new Cell { HostedControl = combo });


                    // CO-OP NODE


                    var ckCoop = new CheckBoxItem();
                    ckCoop.Tag = chapterOffset;
                    ckCoop.Text = "Completed in 2-3 player Co-Op";

                    if (IsCompletedCoOp(chapterOffset, "CoopCompletedChapters"))
                        ckCoop.CheckState = CheckState.Checked;

                    ckCoop.CheckedChanged += CoOp_CheckedChanged;

                    var coopNode = new Node();
                    coopNode.HostedItem = ckCoop;
                    coopNode.Selectable = unlocked;
                    chapterNode.Nodes.Add(coopNode);


                    // FULL CO-OP NODE


                    var ckFullCoop = new CheckBoxItem();
                    ckFullCoop.Tag = chapterOffset;
                    ckFullCoop.Text = "Completed in 4-player Co-Op";

                    if (IsCompletedCoOp(chapterOffset, "FullCoopCompletedChapters"))
                        ckFullCoop.CheckState = CheckState.Checked;

                    ckFullCoop.CheckedChanged += FullCoOp_CheckedChanged;

                    var fullNode = new Node();
                    fullNode.HostedItem = ckFullCoop;
                    fullNode.Selectable = unlocked;
                    chapterNode.Nodes.Add(fullNode);


                    // DIFFICULTIES

                    foreach (var diff in Difficulties)
                    {
                        var node = new Node((diff.Value == "Hard" ? "Hardcore" : diff.Value) + " Stars");
                        node.Tag = diff.Key;

                        var intInput = CreateIntegerInput(3);
                        intInput.Tag = node;
                        intInput.Value = GetStarCount(chapterOffset, diff.Key);
                        intInput.ValueChanged += Star_ValueChanged;

                        node.Cells.Add(new Cell { HostedControl = intInput });

                        node.Selectable = unlocked;

                        chapterNode.Nodes.Add(node);
                    }

                    actNode.Nodes.Add(chapterNode);
                }

                listChapters.Nodes.Add(actNode);
            }

            cmdUnlockChapters.Enabled = _unlockedChapters != 42;
        }

        private int _unlockedChapters = 0;

        private void CompletedDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = (ComboBoxEx)sender;
            var diff = cb.SelectedIndex;
            var node = (Node)cb.Tag;
            var chapter = (int)node.Tag;

            for (int x = 0; x < 4; x++)
            {
                var completed = x < diff;

                var key = ChapterToSettingKey(chapter, "CompletedChaptersArcade" + Difficulties[x]);
                var value = 1 << ChapterToRelativeOffset(chapter);

                if (completed)
                    _profile[key] |= value;
                else
                    _profile[key] &= ~value;
            }

            if (diff == 0)
            {
                ((CheckBoxItem)node.Nodes[0].HostedItem).CheckState = CheckState.Unchecked;
                ((CheckBoxItem)node.Nodes[1].HostedItem).CheckState = CheckState.Unchecked;

                node.Expanded = false;
                node.ExpandVisibility = eNodeExpandVisibility.Hidden;
            }
            else
            {
                node.Expanded = true;
                node.ExpandVisibility = eNodeExpandVisibility.Visible;
            }
        }

        private int GetHighestDifficulty(int chapter)
        {
            for (int x = 0; x < 4; x++)
                if (!IsChapterCompleted(chapter, Difficulties[x]))
                    return x - 1;
            return 3;
        }

        private void FullCoOp_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            CoOpChanged(sender, "CoopCompletedChapters");
        }

        private void CoOp_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            CoOpChanged(sender, "FullCoopCompletedChapters");
        }

        private bool IsCompletedCoOp(int chapter, string partialKey)
        {
            return (_profile[ChapterToSettingKey(chapter, partialKey)] & (1 << ChapterToRelativeOffset(chapter))) != 0;
        }

        private void CoOpChanged(object sender, string partialKey)
        {
            var ck = (CheckBoxItem)sender;
            var completed = ck.Checked;
            var chapter = (int)ck.Tag;

            var key = ChapterToSettingKey(chapter, partialKey);
            var value = 1 << ChapterToRelativeOffset(chapter);

            if (completed)
                _profile[key] |= value;
            else
                _profile[key] &= ~value;
        }

        private void Star_ValueChanged(object sender, EventArgs e)
        {
            var intInput = (IntegerInput)sender;
            var node = (Node)intInput.Tag;
            var diff = (int)node.Tag;
            var chapter = (int)node.Parent.Tag;

            SetStartCount(chapter, diff, intInput.Value);
        }

        private void ChapterUnlocked_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            var ck = (CheckBoxItem)sender;
            var node = (Node)ck.Tag;
            var chapter = (int)node.Tag;
            var key = ChapterToSettingKey(chapter, "UnlockedChapterAccess");
            var mask =  1 << ChapterToRelativeOffset(chapter);
            var check = ck.Checked;

            if (check)
            {
                _profile[key] |= mask;
                _unlockedChapters++;
            }

            else
            {
                _profile[key] &= ~mask;
                _unlockedChapters--;
            }

            cmdUnlockChapters.Enabled = _unlockedChapters != 42;

            var cb = (ComboBoxEx)node.Cells[1].HostedControl;
            if (check)
            {
                cb.Enabled = true;
                if (cb.SelectedIndex != 0)
                    node.ExpandVisibility = eNodeExpandVisibility.Visible;
            }
            else
            {
                cb.Enabled = false;
                node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                node.Expanded = false;
            }
        }

        private void SetStartCount(int chapter, int difficulty, int stars)
        {
            chapter += 4;

            var currentValue = _campaign[chapter];

            var shift = difficulty * 2;

            currentValue &= (byte)~(3 << shift);

            currentValue |= (byte)((stars & 3) << shift);

            _campaign[chapter] = currentValue;
        }

        private int GetStarCount(int chapter, int difficulty)
        {
            return (_campaign[chapter + 4] >> (difficulty * 2)) & 3; 
        }

        private bool IsChapterUnlocked(int chapter)
        {
            return (_profile[ChapterToSettingKey(chapter, "UnlockedChapterAccess")] & (1 << ChapterToRelativeOffset(chapter))) != 0;
        }

        private bool IsChapterCompleted(int chapter, string difficulty)
        {
            var val = _profile[ChapterToSettingKey(chapter, "CompletedChaptersArcade" + difficulty)];

            return (val & (1 << ChapterToRelativeOffset(chapter))) != 0;
        }

        private static int ChapterToRelativeOffset(int chapter)
        {
            while (chapter >= 32)
                chapter -= 32;

            return chapter;
        }

        private static string ChapterToSettingKey(int chapter, string prefix)
        {
            return prefix + (chapter / 32 + 1);
        }

        private byte[] _campaign;

        private Node CreateInt32Node(string key, string title, int maxValue)
        {
            var intInput = CreateIntegerInput(maxValue);
            intInput.Tag = key;
            intInput.Value = _profile[key];
            intInput.ValueChanged += Int32_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = intInput });

            return node;
        }

        private static IntegerInput CreateIntegerInput(int maxValue)
        {
            var intInput = new IntegerInput();
            intInput.MinValue = 0;
            intInput.MaxValue = maxValue;
            intInput.ShowUpDown = true;
            return intInput;
        }

        private void CheckAll_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (_busy)
                return;

            var ck = (CheckBoxItem)sender;
            var node = (Node)ck.Tag;

            var newCheckState = ck.CheckState;

            foreach (Node n in node.Nodes)
                ((CheckBoxItem)n.HostedItem).CheckState = newCheckState;
        }

        private void UpdateCheckAllNode(Node node)
        {
            bool all = true, any = false;
            foreach (Node n in node.Nodes)
            {
                var state = ((CheckBoxItem)n.HostedItem).CheckState;
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
        }

        private static string CollectableToKey(int cId)
        {
            return "DiscoverFound" + ((cId <= 31) ? "1" : "2");
        }

        private static int CollectableToMask(int cId)
        {
            if (cId > 31)
                cId -= 32;

            return 1 << cId;
        }

        private void Collectable_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            var ckItem = (CheckBoxItem)sender;
            var node = (Node)ckItem.Tag;
            var cId = (int)node.Tag;
            var found = ckItem.CheckState == CheckState.Checked;

            var key = CollectableToKey(cId);
            var mask = CollectableToMask(cId);

            var currentValue = _profile[key];

            if (found)
                currentValue |= mask;
            else
                currentValue &= ~mask;

            _profile[key] = currentValue;

            UpdateCheckAllNode(node.Parent);
        }

        private void cbMedal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            _busy = true;

            var selectedItem = (string)cbMedal.SelectedItem;

            var newValue = Medals[selectedItem];

            if (SingleMedals.Contains(newValue))
            {
                cbMedalTier.Enabled = false;
                cbMedalTier.SelectedIndex = 3;
            }
            else
            {
                cbMedalTier.Enabled = true;
                newValue += cbMedalTier.SelectedIndex;
            }
            
            _profile["SelectedMedal"] = newValue;

            _busy = false;
        }

        private Node CreateComboBoxNodes(string nodeTitle, string keyFormat, TagMap keys, Int32Map values, int[] hidden)
        {
            var rootNode = new Node(nodeTitle);

            foreach (var key in keys)
            {
                var currentKey = string.Format(keyFormat, key.Value);

                if (!_profile.ContainsKey(currentKey))
                    continue;

                var comboBox = CreateComboBox(string.Format(keyFormat, key.Value), values, hidden);

                var node = new Node(key.Key);
                node.Cells.Add(new Cell { HostedControl = comboBox });
                rootNode.Nodes.Add(node);
            }

            rootNode.Nodes.Sort();

            return rootNode;
        }

        private ComboBox CreateComboBox(string key, Int32Map values, IEnumerable<int> hidden)
        {
            var cbox = new ComboBoxEx();
            cbox.DropDownStyle = ComboBoxStyle.DropDownList;
            cbox.FlatStyle = FlatStyle.Standard;
            cbox.FocusCuesEnabled = false;

            PopulateComboBox(cbox, values, hidden);
            RefreshComboBox(cbox, key, values);

            cbox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            return cbox;
        }

        private static void PopulateComboBox(ComboBox comboBox, Int32Map values, IEnumerable<int> hidden)
        {
            foreach (var value in values.Where(v => !hidden.Contains(v.Value)))
                comboBox.Items.Add(value.Key);

            comboBox.Sorted = true;
        }

        private void RefreshComboBox(ComboBox comboBox, string key, Int32Map values)
        {
            comboBox.Tag = new ComboBoxMap(key, values);
            
            var currentValue = _profile[key];

            string selectedItem = null;
            foreach (var value in values.Where(value => value.Value == currentValue))
                selectedItem = value.Key;

            if (selectedItem == null)
            {
                selectedItem = string.Format(UnknownFormat, currentValue);
                values.Add(selectedItem, currentValue);
                comboBox.Items.Add(selectedItem);
            }

            comboBox.Sorted = true;

            _busy = true;
            if (!comboBox.Items.Contains(selectedItem))
                comboBox.Items.Add(selectedItem);
            comboBox.SelectedItem = selectedItem;
            _busy = false;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_busy)
                return;

            var comboBox = (ComboBox)sender;

            var map = (ComboBoxMap)comboBox.Tag;

            _profile[map.Key] = map.EnumMap[(string)comboBox.SelectedItem];
        }

        public override bool Entry()
        {
            if (!loadAllTitleSettings(EndianType.BigEndian))
                return false;

#if INT2
            var encryptionKey = Encoding.ASCII.GetBytes("@srAnD0m@s!7ge7z7@k37ha7m0Dd3rz ");
#else
            var encryptionKey = SettingAsByteArray(52);   
#endif

            _profile = new ProfileSettings(encryptionKey, IO.ToArray(), Account.Info.XuidOnline, true);

            OnLoad();

            return true;
        }

        public override void Save()
        {
            _profile.Set("ChaptersScore", _campaign);

            writeAllTitleSettings(_profile.ToArray(true));
        }

        private class ComboBoxMap
        {
            internal readonly string Key;
            internal readonly Int32Map EnumMap;

            internal ComboBoxMap(string key, Int32Map enumMap)
            {
                Key = key;
                EnumMap = enumMap;
            }
        }

        private static readonly Dictionary<string, TagMap> Stats = new Dictionary<string, TagMap>
        {
            { "Main Stats", new TagMap {
                { "GoodHostRatio", "Good Host Ratio" },
                { "NumEnemiesKilled", "Enemies Killed" },
                { "MpMatchesPlayed", "Matches Played" },
                { "RecModeMatchesPlayed", "Recreation Matches Played" },
                { "RecModeTotalXp", "Recreation Mode XP" },
                { "MPMapsCompleted", "Maps Completed" },
                { "ArcadeChaptersPlayed", "Campaign Sections Played" },
                { "DisconnectCount", "Ranked Disconnects" },
                { "QuitDemerits", "Games Quit" },
                { "StandByDemerits", "Games Standby" }
            }},
            { "Prize Boxes", new TagMap {
                { "NormalPrizeBoxCount", "Normal" },
                { "EpicPrizeBoxCount", "Rare" }, // the game messed up the internal names
                { "RarePrizeBoxCount", "Epic" }
            }},
            { "Flags", new TagMap {
                { "IsBanned", "Banned" },
                { "bIsEpicDev", "Epic Developer" },
                { "bIsRiftDev", "Rift Developer" },
                { "RecModeGraduate", "Recreation Mode Graduate" },
                { "HasRookieGear", "Rookie Gear Unlocked" },
                { "HasVeteranGear", "Veteran Gear Unlocked" },
                { "HasGears1Camp", "Has Gears of War 1" },
                { "HasGears2Camp", "Has Gears of War 2" },
                { "HasGears3Camp", "Has Gears of War 3" },
                { "HasGearsPCCheevo", "Has Gears of War PC" }
            }},
        };

        private static readonly List<string> ActTitles = new List<string>
        {
            "I: The Museum of Military Glory",
            "II: Halvo Bay Military Academy",
            "III: Seahorse Hills",
            "IV: Onyx Point",
            "V: Downtown Halvo Bay",
            "VI: The Courthouse",
            "Aftermath"
        };

        private static readonly Dictionary<int, string> Difficulties = new Dictionary<int, string>
        {
            { 0, "Casual" },
            { 1, "Normal" },
            { 2, "Hard" },
            { 3, "Insane" }
        };

        private static readonly List<string[]> Chapters = new List<string[]>
        {
            new[] {
                "Old Town",
                "Riverwalk District",
                "Museum Gardens",
                "Great Hall",
                "Kaskur Wing",
                "East Wing",
                "Archives",
                "Vaults"                
            },
            new[] {
                "Enfield Bridge",
                "Courtyard",
                "R&D Labs",
                "Monroe Commons",
                "Atrium",
                "Crash Site"           
            },
            new[] {
                "Amador Park",
                "Magadha Villa",
                "Soleno Villa",
                "Windward Way",
                "Risea Estate",
                "Guest Bungalows",
                "Elliot's Mansion"      
            },
            new[] {
                "Fortress",
                "Container Terminal",
                "Motor Pools",
                "The Cliffs",
                "Central Base",
                "Central Control",
                "Beach"      
            },
            new[] {
                "Wharf District",
                "Parade Grounds",
                "Upper State Street",
                "State Street Rooftops",
                "First Avenue Rooftops",
                "Museum Square Rooftops",
                "Overlook"      
            },
            new[] {
                "Grand Courtroom",
                "Halls of Judgment",
                "Terrace",
                "North Entrance",
                "Main Entrance",
                "Great Staircase",
                "Plaza for the Tyran Dead"      
            },
        };

        private static readonly Dictionary<int, object[]> Collectables = new Dictionary<int,object[]>
        {
            { 0, new object[] {
	            new[] { "E. Kogan, SGT, Jacinto", "K.I.A. when a glowing Wretch that pounced on him was shot and exploded" },
	            new[] { "J. Los, CPL, Kinnerlake", "K.I.A. from friendly fire when he accidentally ran in front of his fire team" },
	            new[] { "L. Gaffney, PFC, Halvo Bay", "K.I.A. in Locust ambush, was first to die" },
	            new[] { "S. Panturo, PFC, Ilima", "K.I.A. in a Locust grenade trap" },
	            new[] { "A. Guinot, MSgt, Jacinto", "K.I.A. from Corpser attack, was impaled" },
	            new[] { "S. Dascher, SPC, East Timgad", "K.I.A. by Locust OneShot" },
	            new[] { "R. Wood, 2LT, Vonner Bay", "K.I.A. by Kantus when she attempted to take it hand-to-hand" },
	            new[] { "T. Loomis, 1LT, Halvo Bay", "K.I.A. by Scorcher when cornered with his men" },
            }},
            { 1, new object[] {
	            new[] { "W. Felhofer, PFC, Hanover", "K.I.A. by Boomshot round while attempting to draw enemy fire" },
	            new[] { "A. Sullivan, SSGT, Andius", "K.I.A. by multiple Tickers in coordinated attack" },
	            new[] { "G. Bixhorn, MAJ, Jacinto", "K.I.A. after Reaver attack, took several hours to expire from injury" },
	            new[] { "T. Perham, PFC, Hatton", "K.I.A. during encounter with Ragers, was beaten to death" },
	            new[] { "D. Andrade, 1LT, Montevado", "K.I.A. after he fell into an Emergence Hole" },
	            new[] { "M. Alexieff, SPC, Halvo Bay", "K.I.A. in battle against Karn, was last Gear standing in the Academy" },
            }},
            { 2, new object[] {
	            new[] { "M. Reid, PFC, Ephyra", "K.I.A. when a Boomshot round exploded his transport" },
	            new[] { "N. Grachev, PVT, East Timgad", "K.I.A. when a stray bullet ricocheted" },
	            new[] { "K. Axford, PVT, Vonner Bay", "Shot for cowardice and desertion by commanding officer" },
	            new[] { "J. Carlson, CPT, Halvo Bay", "K.I.A. when he sacrificed himself by leaping upon an enemy grenade" },
	            new[] { "K. Sutton, PVT, New Sherrith", "K.I.A. by Mortar round while trying to revive a downed comrade" },
	            new[] { "I. Hogina, PFC, Autrin", "K.I.A. during a ferocious Locust firefight, apparent battlefield suicide" },
	            new[] { "G. Frost, SPC, Jacinto", "K.I.A. while trying to prevent Locust from killing civilian families" },
            }},
            { 3, new object[] {
	            new[] { "B. Michandani, PFC, Lake Station", "K.I.A. during Locust attack on Onyx Point, was attempting to protect a weapons cache" },
	            new[] { "T. O'Neill, CW2, Andius", "K.I.A. by sniper while trying to radio back to mainland for reinforcements" },
	            new[] { "C. White, CPT, Halvo Bay", "K.I.A. by Cleaver-wielding Theron Guards" },
	            new[] { "H. Molnar, SGT, Ephyra", "K.I.A. while trying to set a grenade trap" },
	            new[] { "B. Newman, PFC, Port Caval", "K.I.A. during Ink grenade attack, suffocated" },
	            new[] { "L. Zhang, 2LT, Jacinto", "K.I.A. after ordering a retreat back to Central Base" },
	            new[] { "S. Garoon, 2LT, Soteroa", "K.I.A. after successfully locking down elevator to Onyx Point's main armory" },
            }},
            { 4, new object[] {
	            new[] { "D. Yalovsky, SPC, Halvo Bay", "K.I.A. from enemy fire while attempting to prevent a civilian from falling into an Emergence Hole" },
	            new[] { "J. Moreno, MAJ, Ephyra", "K.I.A. when cornered by a Cleaver-wielding Locust" },
	            new[] { "N. Vietzen, SGT, Andius", "K.I.A. when a makeshift bridge across two adjacent rooftops collapsed" },
	            new[] { "S. Bishop, COL, Gerrenhalt", "K.I.A. by Torque Bow round while trying to divert civilians toward better cover" },
	            new[] { "J. Corbin, 1LT, Jacinto", "K.I.A. by enemy fire as she was escorting civilians toward higher ground" },
	            new[] { "P. Suurs, CPT, Hanover", "K.I.A. when a Serapede crept up behind him" },
	            new[] { "J. Salton, PFC, Autrin", "K.I.A. by Reaver-fired rockets while attempting to protect civilians" },
            }},
            { 5, new object[] {
	            new[] { "B. Clarkson, 1LT, Tollen", "K.I.A. during Cadet Hendrick's testimony" },
	            new[] { "T. Carlson, 1LT, Halvo Bay", "K.I.A. during Lt. Baird's first testimony" },
	            new[] { "S. Mograbi, PFC, Jacinto", "K.I.A. during Private Cole's testimony" },
	            new[] { "S. Katyal, SPC, Ephyra", "K.I.A. during Private Paduk's testimony" },
	            new[] { "G. Valera, WO1, Ilima", "K.I.A. when the final COG lines inside of the Courthouse broke" },
	            new[] { "K. Barrick, PVT, Port Lorrence", "K.I.A. during Lt. Baird's second testimony" },
	            new[] { "D. Jones, CPT, Andius", "K.I.A shortly after trying to warn Col. Loomis to postpone Kilo Squad's tribunal" },
            }},
            { 6, new object[] {
	            new[] { "D. Rovik, BG, Ephyra", "K.I.A. during original Locust attack on Halvo Bay" },
	            new[] { "H. Mahawar, SPC, Soteroa", "K.I.A. by Karn during Locust attack on Halvo Bay" },
	            new[] { "M. Blythe, SGT, Kinnerlake", "K.I.A. while protecting the COG Engineering Corps during its body-recovery effort in Halvo Bay" },
	            new[] { "D. Strome, PVT, Halvo Bay", "K.I.A. by Karn during original Locust invasion of Halvo Bay" },
	            new[] { "D. Galvin, CPL, Mercy", "K.I.A. during final evacuation of Halvo Bay" },
	            new[] { "M. Cohen, 1LT, Ephyra", "K.I.A. during Col. Loomis's last stand in Halvo Bay" },
            }},
        };

        private static readonly Int32Map COGCharacters = new Int32Map
        {
            { "Baird (Judgment)", 0 },
            { "Cole (Judgment)", 1 },
            { "Sofia (Judgment)", 2 },
            { "Garron (Judgment)", 3 },
            { "Ezra Loomis", 6 },
            { "Cole (Aftermath)", 11 },
            { "Garron (Aftermath)", 12 },
            { "Minh Young Kim", 13 },
            { "Tai Kaliso", 14 },
            //{ "Clayton Carmine", 17 }
        };

        private static readonly TagMap COGCharacterNames = new TagMap
        {
            { "Baird (Judgment)", "BairdJack" },
            { "Cole (Judgment)", "ColeJack" },
            { "Sofia (Judgment)", "SofiaJack" },
            { "Garron (Judgment)", "GarronJack" },
            { "Young Marcus", "YoungMarcus" },
            { "Young Dom", "YoungDom" },
            { "Ezra Loomis", "Loomis" },
            { "Male Onyx Guard", "MaleOnyx" },
            { "Female Onyx Guard", "FemaleOnyx" },
            { "Anya", "Anya" },
            { "Baird", "Baird" },
            { "Cole", "Cole" },
            { "Garron", "Paduk" },
            { "Minh Young Kim", "Mihn" },
            { "Tai Kaliso", "Tai" },
            { "Trishka", "Trishka" },
            { "Grayson", "Grayson" },
            { "Clayton Carmine", "Clayton" },
            { "Alex Brand", "AlaxBrand" },
            { "Jungle Tai", "JungleTai" },
            { "Epic Reaper", "EpicReaper" }
        };

        private static readonly TagMap WeaponNames = new TagMap
        {
            { "Booshka", "NadeLauncher" },
            { "Lancer", "Lancer" },
            { "Hammerburst", "Hammerburst" },
            { "Retro Lancer", "Retro" },
            { "Gnasher", "Gnasher" },
            { "Sawed-Off Shotgun", "SawedOff" },
            { "Markza", "Marksman" },
            { "Classic Hammerburst", "HammerburstRetro" }
        };

        private static readonly int[] DLCWeaponSkins = new[]
        {
            8, 12, 13, 14, 21, 22, 25, 26,
            27, 28, 31, 36, 38, 39, 40, 43,
            41, 9, 10, 32, 37, 49, 50, 15, 23
        };

        private static readonly Int32Map WeaponSkins = new Int32Map
        {
            { None, 0 },
            { "Gold", 1 },
            { "Chrome", 2 },
            { "Crimson Omen", 3 },
            { "Xbox", 4 },
            { "Onyx", 5 },
            { "Team Pulse", 6 },
            { "Team Metal", 7 },
            { "Aurora", 8 },
            { "Paintball", 9 }, //??
            { "Big Game", 10 }, //??
            { "Candy", 12 },
            { "Electrostatic", 13 },
            { "Imperial", 14 },
            { "Zebra", 15 }, //??
            { "Brisk", 16 },
            { "8 Ball", 21 },
            { "Car 13", 22 },
            { "Hazardous", 23 },
            { "Melon Farmer", 25 },
            { "Mod Culture", 26 },
            { "Wood", 27 },
            { "Double Rainbow", 28 },
            { "Ares Bronze", 29 },
            { "Burn Spray", 30 },
            { "Groovy", 31 },
            { "Lambent", 32 }, //??
            { "PinkRabbit", 36 },
            { "Hotrod", 37 }, //??
            { "UIR", 38 },
            { "Comic", 39 },
            { "Shark", 40 },
            { "Flames of Judgment", 41 }, //?
            { "Web", 42 }, //?
            { "X-Ray", 43 },
            { "Rib Cage", 44 },
            { "Epic", 45 },
            { "Reaper Slayer", 46 },
            { "Hypno", 47 },
            { "Cascade", 48 },
            { "Snowtopped", 49 }, //?
            { "Chlorophyll", 50 }, //?,
            //{ "DLC[1070]", 1070 },
            //{ "DLC[1071]", 1071 },
            //{ "DLC[1072]", 1072 },
            //{ "DLC[1084]", 1084 },
        };

        private static readonly int[] DLCCharacterSkins = new[]
        {
            71, 73, 74, 75, 77, 78, 79, 80,
            83, 84, 87, 90, 91, 92, 94, 96,
            99, 100, 103, 105, 106, 108,
            81, 85, 89, 107, 113
        };

        private static readonly Int32Map CharacterSkins = new Int32Map
        {
            { None, -1 },
            { "Default", 0 },
            { "Bacon", 71 },
            { "Bee", 72 },
            { "Program", 73 },
            { "Ladybug", 74 },
            { "Clown", 75 },
            { "Candy Cane", 77 },
            { "Deadly Cute", 78 },
            { "Groovy", 79 },
            { "Paintball", 80 },
            { "Big Game", 81 }, //??
            { "Mummy", 82 },
            { "Neon", 83 },
            { "Inmate", 84 },
            { "Dark Carnival", 85 }, //??
            { "Skeletal (Glow)", 86 },
            { "Skeletal", 87 },
            { "Copper", 88 },
            { "Arctic Armor", 89 }, //??
            { "Harlequin", 90 },
            { "Astro-Devision", 91 },
            { "Tiger Stripes", 92 },
            { "HDPD Blue", 94 },
            { "Zombie", 96 },
            { "Chrome", 97 },
            { "Gold", 98 },
            { "Super Hero", 99 },
            { "Cascade", 100 },
            { "Burning", 103 },
            { "Cel Shaded", 104 },
            { "Cracked", 105 },
            { "Digital", 106 },
            { "Plaid", 107 }, //??
            { "Double Rainbow", 108 },
            { "Brisk", 113 },
            /*{ "DLC[1082]", 1082 },
            { "DLC[1084]", 1084 },
            { "DLC[1088]", 1088 },
            { "DLC[1089]", 1089 },
            { "DLC[1091]", 1091 },
            { "DLC[1092]", 1092 },
            { "DLC[1093]", 1093 },
            { "DLC[1094]", 1094 },
            { "DLC[1097]", 1097 },*/
        };

        private void cmdExportSettings_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            _profile.Export(sfd.FileName);
        }

        private static readonly int[] SingleMedals = new[]
        {
            -1, 153, 168, 2, 223, 146, 304, 305, 152
        };

        private static readonly Int32Map Medals = new Int32Map
        {
            { None, -1 },
            { "Beta Tester", 152 },
	        { "EPIC", 153 },
	        { "Gears of War 3 Developer", 168 },
	        { "They're Coming!", 146 },
	        { "VIP", 304 },
	        { "Epic Reaper", 305 },
	        { "MVP", 9 },
	        { "Field Service", 164 },
	        { "Veteran", 13 },
	        { "Match Winner", 17 },
	        { "Headshot", 21 },
	        { "Heavy Weapons", 25 },
	        { "Explosives", 29 },
	        { "Finisher", 33 },
	        { "Medic", 37 },
	        { "Active Reloader", 41 },
	        { "Melee Master", 298 },
	        { "Markza", 73 },
	        { "Classic Hammerburst", 77 },
	        { "Tripwire Crossbow", 69 },
	        { "Lancer", 45 },
	        { "Hammerburst", 49 },
	        { "Retro Lancer", 53 },
	        { "Gnasher Shotgun", 57 },
	        { "Sawed-off Shotgun", 61 },
	        { "Pistols", 65 },
	        { "Spotter", 81 },
	        { "Pyro", 85 },
	        { "Shock Trooper", 106 },
	        { "Grenade Master", 89 },
	        { "Special Teams", 93 },
	        { "Master-at-Arms", 97 },
	        { "Rifleman", 101 },
	        { "Seriously Judgmental", 2 },
	        { "Embry Star", 223 },
	        { "War Supporter", 4 },
	        { "Survivalist", 230 },
	        { "Doorman", 234 },
	        { "Search and Destroy", 238 },
	        { "Fort Fixer", 242 },
	        { "Debuffer", 246 },
	        { "Ammo Giver", 250 },
	        { "Healer", 254 },
	        { "Tour of Duty", 258 },
	        { "Survivor", 262 },
	        { "Coalition", 270 },
	        { "Defender", 266 },
	        { "Horder", 274 },
	        { "Lockdown", 278 },
	        { "Fort Destroyer", 282 },
	        { "Anarchist", 286 },
	        { "Slayer", 290 },
	        { "Champion", 294 }
        };

        private static readonly Int32Map Titles = new Int32Map
        {
            { None, -1 },
            { "Beta Tester", 152 },
	        { "I am EPIC", 153 },
	        { "I Made This!", 168 },
	        { "Be Gentle with Me", 146 },
	        { "VIP", 304 },
	        { "Slayer of Samael", 305 },
	        { "Officer and a Gentleman", 9 },
	        { "All-Star Weekend", 10 },
	        { "Humblebragger", 11 },
	        { "His Royal Airness", 12 },
	        { "Good at Gears", 164 },
	        { "Honorary Fenix", 165 },
	        { "Super Elite", 166 },
	        { "Gears Deity", 167 },
	        { "Shell-Shocked", 13 },
	        { "Twitchtastic", 14 },
	        { "Been through Hell", 15 },
	        { "War Hero", 16 },
	        { "Meh", 17 },
	        { "Win-Bag", 18 },
	        { "Who's a Winner?", 19 },
	        { "Professional Victor", 20 },
	        { "Skeet Skeet", 21 },
	        { "Sharpshooter", 22 },
	        { "Headcase", 23 },
	        { "Patty Domes", 24 },
	        { "Weight Problem", 25 },
	        { "Getting Heavy", 26 },
	        { "Whoa, That's Heavy", 27 },
	        { "Five Tons of Ouch", 28 },
	        { "Pow!", 29 },
	        { "Boom!!", 30 },
	        { "Kaboom!!!", 31 },
	        { "Mushroom Cloud!!!!", 32 },
	        { "Eliminated", 33 },
	        { "Assassin", 34 },
	        { "Liquidated", 35 },
	        { "Angel of Death", 36 },
	        { "Paramedic", 37 },
	        { "Ambulance Chaser", 38 },
	        { "He's Dead, Jim", 39 },
	        { "Dr. Awesome, M.D.", 40 },
	        { "Good Timing", 41 },
	        { "Damage, Inc.", 42 },
	        { "Would You Like Bullets with That?", 43 },
	        { "Secret Sauce", 44 },
	        { "Punched Out", 298 },
	        { "Mortal Combatant", 299 },
	        { "Smash Brother", 300 },
	        { "God Hands", 301 },
	        { "Marksman", 73 },
	        { "Trigger Man", 74 },
	        { "Confirmed Killer", 75 },
	        { "Master Camper", 76 },
	        { "A Good Vintage", 77 },
	        { "Classically Trained", 78 },
	        { "Dilettante", 79 },
	        { "Stay Classy", 80 },
	        { "Not My Idea of Courage", 69 },
	        { "An Elegant Weapon", 70 },
	        { "That's No Moon", 71 },
	        { "Let's Blow This Thing", 72 },
	        { "Chainsaw Accident", 45 },
	        { "Chainsaw Debacle", 46 },
	        { "Chainsaw Fiasco", 47 },
	        { "Chainsaw Massacre", 48 },
	        { "To Burst, Or Not To Burst", 49 },
	        { "HammerThirst", 50 },
	        { "You're the Nail", 51 },
	        { "Hammerburst Hero", 52 },
	        { "Vintage", 53 },
	        { "Old-Fashioned", 54 },
	        { "Antique", 55 },
	        { "Retro is OP", 56 },
	        { "Guh-Nasher", 57 },
	        { "Wall Bouncer", 58 },
	        { "Get Bodied", 59 },
	        { "Shotgun Nation", 60 },
	        { "Both Barrels", 61 },
	        { "Closed Casketeer", 62 },
	        { "Lookin' Down the Barrel", 63 },
	        { "Wearin' Barrels", 64 },
	        { "Pea-Shooter", 65 },
	        { "Make My Day", 66 },
	        { "Revolver Solver", 67 },
	        { "Pistol-Packing Mama", 68 },
	        { "Spotted Ya!", 81 },
	        { "Hey, He's Over Here!", 82 },
	        { "Human Lighthouse", 83 },
	        { "The Most Dangerous Game", 84 },
	        { "First Degree Burns", 85 },
	        { "Likes It Rare", 86 },
	        { "Hellfire", 87 },
	        { "Watch It All Burn", 88 },
	        { "Quick on the Trigger", 106 },
	        { "Mr. Itchy", 107 },
	        { "Bad Manners", 108 },
	        { "Up in Your Bidness", 109 },
	        { "Grenadier", 89 },
	        { "Pin-Puller", 90 },
	        { "Grenade Chef", 91 },
	        { "Frag King", 92 },
	        { "Nowhere Man", 93 },
	        { "Can Do It All", 94 },
	        { "Multi-Talented", 95 },
	        { "Master of Your Domain", 96 },
	        { "Man-at-Arms", 97 },
	        { "Master-at-Arms", 98 },
	        { "Master-at-Arms Elite", 99 },
	        { "God-at-Arms", 100 },
	        { "Rifle Specialist", 101 },
	        { "Rifle Expert", 102 },
	        { "Rifle Artisan", 103 },
	        { "Rifle Master", 104 },
	        { "Seriously Judgmental", 2 },
	        { "Allfather", 223 },
	        { "Socialite", 4 },
	        { "Man about Town", 5 },
	        { "Party Animal", 6 },
	        { "Head to Rehab", 7 },
	        { "Invisible Kid", 230 },
	        { "Tough Guy", 231 },
	        { "Survival Artist", 232 },
	        { "Inconceivable!", 233 },
	        { "What's This Button Do?", 234 },
	        { "Knob-Turner", 235 },
	        { "Are You the Keymaster?", 236 },
	        { "Press X to Jason", 237 },
	        { "Totaled", 238 },
	        { "In the Area", 239 },
	        { "COGmower", 240 },
	        { "Barbarian at the Gate", 241 },
	        { "Grease Monkey", 242 },
	        { "Come at Me, Bro", 243 },
	        { "Fixinator", 244 },
	        { "Don't Tase Me", 245 },
	        { "Crystal Ball Rubber", 246 },
	        { "X-Ray Eyes", 247 },
	        { "On Your Knees!", 248 },
	        { "Chump Knocker", 249 },
	        { "Friend with Benefits", 250 },
	        { "Back-Scratcher", 251 },
	        { "Quid Pro Quo", 252 },
	        { "Magnificent Bastard", 253 },
	        { "Street Apothecary", 254 },
	        { "Bring Out Yer Dead!", 255 },
	        { "Bacon Saver", 256 },
	        { "Lover and Fighter", 257 },
	        { "Medal of Duty", 258 },
	        { "Seen Some Action", 259 },
	        { "Combat Veteran", 260 },
	        { "General Chaos", 261 },
	        { "Oxygen Addict", 262 },
	        { "Hard to Kill", 263 },
	        { "Black Chopper Watcher", 264 },
	        { "There Can Be Only One", 265 },
	        { "That's a Lotta Blood", 270 },
	        { "Bloodbather", 271 },
	        { "Killing Machine", 272 },
	        { "Fog of War", 273 },
	        { "Wave Ridin'", 266 },
	        { "Back Off, Sucka!", 267 },
	        { "Defensive Lineman", 268 },
	        { "Defender of the Hole", 269 },
	        { "Might Have a Problem", 274 },
	        { "Seeking Help", 275 },
	        { "Certified Horder", 276 },
	        { "Kind of a Big Deal", 277 },
	        { "Defense in Depth", 278 },
	        { "Billy Blockade", 279 },
	        { "Locust FAIL", 280 },
	        { "Slave to the COG", 281 },
	        { "Wrecking Ball", 282 },
	        { "Master of Disaster", 283 },
	        { "Welcome to Smashtown", 284 },
	        { "Loot Brute", 285 },
	        { "Needs a Breath Mint", 286 },
	        { "Head-Turner", 287 },
	        { "Truly Wretched", 288 },
	        { "Master Orator", 289 },
	        { "Body Counter", 290 },
	        { "Noobicide", 291 },
	        { "Captain Bodybag", 292 },
	        { "Space Cowboy", 293 },
	        { "Bench Warmer", 294 },
	        { "Bag of Awesome", 295 },
	        { "Wreckin' Shop", 296 },
	        { "Beastmaster", 297 }
        };

        private void cmdUnlockChapters_Click(object sender, EventArgs e)
        {
            foreach (Node act in listChapters.Nodes)
                foreach (Node section in act.Nodes)
                    ((CheckBoxItem)section.HostedItem).CheckState = CheckState.Checked;
        }
    }
}
