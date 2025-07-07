using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using SleepingDogs;

namespace Horizon.PackageEditors.Sleeping_Dogs
{
    public partial class SleepingDogs : EditorControl
    {
        private SleepingDogsSave GameSave;

        public SleepingDogs()
        {
            InitializeComponent();
            TitleID = FormID.SleepingDogs;
#if INT2
            btnInjectData.Visible = true;
#endif
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            GameSave = new SleepingDogsSave(IO);
 
            var playerStat = new Node("Stats");
            playerStat.Nodes.AddRange(new[]
                {
                    CreateInt32Node("HK", "Hong Kong Dollars"),
                    CreateInt32Node("FaceXP", "Face XP"),
                    CreateInt32Node("TriadXP", "Triad XP"),
                    CreateInt32Node("PoliceXP", "Police XP"),
                    CreateDoubleNode("CurrentHealth", "Health", 400),
                    CreateInt32Node("MeleeTrainingPoints", "Total Collected Statues To Return", 11),
                    CreateInt32Node("TriadPoints", "Lifetime Triad Upgrade Points", 10),
                    CreateInt32Node("CopPoints", "Lifetime Cop Upgrade Points", 10)
                });

            listMain.Nodes.Add(playerStat);

            listMain.Nodes.Add(
                CreateUpgradeNode("Cop Upgrades",
                new[] { "SlimJim", "ActionDismount", "ActionHijackReducedHeat", "IncreasedRammingDamage", "PoliceTrunkKey"},
                new[] { "FastDisarm", "SlowMotionFollowUp", "RecoilCompensator", "IncreasedFocus", "OverpressureAmmo" }));

            listMain.Nodes.Add(
                CreateUpgradeNode("Melee Training Upgrades",
                new[] { "TackleStrike", "LegBreak", "DisarmTackle", "StunGrappleFollowUp", "ArmBreak" },
                new [] {"SweepKick", "ChargeKneeStun", "SunStrikeFollowUp", "SpinningHeelKick", "JumpingPowerRoundhouseKick", "DoubleJumpKick", "DimMak"}
                ));

            listMain.Nodes.Add(
                CreateUpgradeNode("Triad Upgrades",
                new[] {"StrikeResistance", "RisingKick", "MeleeWeaponResistance", "CounterRecovery", "ClimbingElbowStrike"},
                new[] { "StrikeDamageBonus", "SupriseExchange", "MeleeWeaponSprintAttacks", "ChargeKickFollowup", "MeleeWeaponBoost" }
                )
            );

            return true;
        }

        public override void Save()
        {
            GameSave.CollectAll = cmdCollectAll.Checked;

            GameSave.Save();
        }

        private void UnlockAllUpgrades(object sender, EventArgs e)
        {
            var upgradeNodeNames = new [] {"Cop Upgrades", "Melee Training Upgrades", "Triad Upgrades"};
            var upgrades = listMain.Nodes;
           
            foreach (Node node in upgrades)
            {
                if (node == null ||!upgradeNodeNames.Any((s => node.Text.Contains(s)))) continue;
                
                if (!node.HasChildNodes) continue;
                foreach (Node tier in node.Nodes)
                {
                    foreach (Node n in tier.Nodes)
                        (n.HostedItem as CheckBoxItem).Checked = true;
                }
            }
        }

        private Node CreateUpgradeNode(string nodeTitle, string[] lowTierUpgrades, string[] highTierUpgrades)
        {
            var node = new Node(nodeTitle);
            var ltUpgrades = new Node("Low Tier");
            var htUpgrades = new Node("High Tier");

            foreach (var p in lowTierUpgrades)
                InsertBoolNode(ltUpgrades, p, GetTitleFromKey(p), null);

            foreach (var p in highTierUpgrades)
                InsertBoolNode(htUpgrades, p, GetTitleFromKey(p), null);

            node.Nodes.Add(ltUpgrades);
            node.Nodes.Add(htUpgrades);

            return node;
        }

        private string GetTitleFromKey(string key)
        {
            return Regex.Replace(key, "([a-z])([A-Z])", "$1 $2");
        }

        private void BtnClickInjectData(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            GameSave.Inject(File.ReadAllBytes(ofd.FileName));
        }

        private void InsertBoolNode(Node host, string key, string title, string description)
        {
            var ckInput = new CheckBoxItem {Name = key, Tag = host, Checked = GameSave.PlayerStats.BitValue(key)};
            ckInput.CheckedChanged += ckInput_CheckedChanged;

            if (description != null)
                title += "<br></br>" + FatxHandle.makeGrayText(description);

            ckInput.Text = title;

            var node = new Node {HostedItem = ckInput};

            host.Nodes.Add(node);

        }
        private Node CreateInt32Node(string key, string title)
        {
            return CreateInt32Node(key, title, int.MaxValue);
        }

        private Node CreateDoubleNode(string key, string title)
        {
            return CreateDoubleNode(key, title, float.MaxValue);
        }

        private Node CreateInt32Node(string key, string title, int maxValue)
        {
            var intInput = new IntegerInput
                {
                    Tag = key,
                    Value = GameSave.PlayerStats[key],
                    MinValue = 0,
                    MaxValue = maxValue,
                    ShowUpDown = true
                };
            intInput.ValueChanged += Int32_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = intInput });

            return node;
        }

        private Node CreateDoubleNode(string key, string title, double maxValue)
        {
            var doubleInput = new DoubleInput
            {
                Tag = key,
                Value = GameSave.PlayerStats.FloatValue(key),
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true
            };
            doubleInput.ValueChanged += Float_ValueChange;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = doubleInput });

            return node;
        }

        private void ckInput_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            var checkBox = (CheckBoxItem)sender;

            if (checkBox.CheckState == CheckState.Indeterminate)
                return;

            GameSave.PlayerStats.Set(checkBox.Name, checkBox.CheckState == CheckState.Checked);
        }

        private void Int32_ValueChanged(object sender, EventArgs e)
        {
            var i = sender as IntegerInput;
            if(i != null)
                GameSave.PlayerStats[(string)i.Tag] = i.Value;
        }

        private void Float_ValueChange(object sender, EventArgs e)
        {
            var i = sender as DoubleInput;
            if (i != null)
                GameSave.PlayerStats.Set((string)i.Tag,Convert.ToSingle(i.Value));
        }
    }
}
