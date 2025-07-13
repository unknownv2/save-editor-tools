using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BioShock;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using Horizon.Functions;
using Horizon.Server;

namespace Horizon.PackageEditors.BioShock_Infinite
{
    public partial class BioShockInfinite : EditorControl
    {
        private BioShockInfiniteSave GameSave;

        public BioShockInfinite()
        {
            InitializeComponent();
            TitleID = FormID.BioshockInfinite;
#if INT2
            btnExtract.Visible = true;
            btnInjectSaveData.Visible = true;
#endif
            cmbSlotBox.Items.AddRange(new object[] { "Hat", "Shirt", "Pants", "Boots" });
            cmbSlotBox.SelectedIndexChanged += CmbBoxValueChanged;
            cmbSlotBox.TextChanged += CmbBoxValueChanged;
        }

        private static string _xmlInfo;
        private BsXmlInfoParser _bsInfo;

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            GameSave = new BioShockInfiniteSave(IO);

            // retrive xml infor for describing the Gears
            if (_xmlInfo == null)
                _xmlInfo = new WebClient().DownloadString(Config.baseURL + "bioshock_inf/Gears.XML");

            // load stats, upgrades, and gears
            LoadBsNodes();

            return true;
        }

        public override void Save()
        {
            GameSave.Save();
        }

        private void BtnClickExtractBuffer(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            GameSave.InflateCompressedStream().Save(sfd.FileName);
        }
        private void BtnClickInjectBuffer(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            GameSave.OverwriteStream(System.IO.File.ReadAllBytes(ofd.FileName));
        }
        private Node CreateInt32Node(XItem item, string title)
        {
            return CreateInt32Node(item, title, int.MaxValue);
        }
        private Node CreateInt32Node(XItem item, string title, int maxValue)
        {
            var intInput = new IntegerInput
            {
                Tag = item,
                Value = item.Value,
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true
            };
            intInput.ValueChanged += Int32_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = intInput });

            return node;
        }

        private void Int32_ValueChanged(object sender, EventArgs e)
        {
            var i = sender as IntegerInput;
            if (i == null)
                return;

            var xItem = i.Tag as XItem;
            if (xItem == null)
                return;

            xItem.Value = i.Value;
        }

        private void InsertBoolNode(Node host, XItem item, string title, string description)
        {
            var ckInput = new CheckBoxItem { Name = title, Tag = item, Checked = item.Active };
            ckInput.CheckedChanged += ckInput_CheckedChanged;

            if (description != null)
                title += "<br></br>" + FatxHandle.makeGrayText(description);

            ckInput.Text = title;

            var node = new Node { HostedItem = ckInput };

            host.Nodes.Add(node);
        }

        private void ckInput_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            var checkBox = sender as CheckBoxItem;

            if (checkBox == null)
                return;

            if (checkBox.CheckState == CheckState.Indeterminate)
                return;

            var playerItem = checkBox.Tag as XItem;
            if (playerItem == null) return;

            playerItem.Active = checkBox.Checked;

            if (playerItem.Value == 0x00)
                playerItem.Value = 0x01;
        }

        private void CmbBoxValueChanged(object sender, EventArgs e)
        {
            var xItem = listGears.SelectedNode.Tag as XItem;
            if (xItem == null)
                return;

            xItem.Slot = (XItem.XGearSlot)Enum.Parse(typeof(XItem.XGearSlot), cmbSlotBox.Text);
        }

        private void CreateGearNode(Node host, XItem item, string title)
        {
            CreateGearNode(host, item, title, null);
        }
        private void CreateGearNode(Node host, XItem item, string title, string description)
        {
            var ckInput = new CheckBoxItem { Tag = item, Checked = item.Active };
            ckInput.CheckedChanged += ckInput_CheckedChanged;

            if (description != null)
                title += "<br></br>" + FatxHandle.makeGrayText(description);

            var node = new Node { Text = title, Tag = item };

            node.Cells.Add(new Cell { HostedItem = ckInput });

            host.Nodes.Add(node);
        }

        private void BtnClickUnlockAllGears(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show(
                "Adding all gears can and will most likely give you trouble with progression in the story mode since some that are added are supposed to be hidden from the user and may not be complete." +
                                               " Are you sure you want to add them all? Continue only if you are sure.", "Warning!", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;
            foreach (Node node in listGears.Nodes)
            {
                CheckAllChildNodes(node, true);
            }

            UI.messageBox("All Gears unlocked!");
        }

        private static void CheckAllChildNodes(Node treeNode, bool nodeChecked)
        {
            foreach (Node node in treeNode.Nodes)
            {
                var checkBox = node.Cells[1].HostedItem as CheckBoxItem;
                if (checkBox == null)
                    continue;
                if (checkBox.Checked == nodeChecked)
                    continue;

                checkBox.Checked = nodeChecked;

                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void GearsTreeSelectedNodeChanged(object sender, EventArgs e)
        {
            var xItem = listGears.SelectedNode.Tag as XItem;
            if (xItem == null)
                return;

            if (xItem.Active)
            {
                if (xItem.Slot == XItem.XGearSlot.Null)
                    xItem.Slot = XItem.XGearSlot.Hat;

                cmbSlotBox.Text = xItem.Slot.ToString();
            }
        }

        private void GearsTitleListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listGearNames.SelectedItems.Count > 0)
                txtGearDesc.Text = _bsInfo.GetDescription(listGearNames.SelectedItems[0].Text);
        }

        private void LoadBsNodes()
        {
            var statsNode = new Node("Coins and Lock Picks") { Expanded = true };
            statsNode.Nodes.Add(CreateInt32Node(GameSave.StatItems[ItemId.SilverCoins], "Silver Eagles", 999999999));
            statsNode.Nodes.Add(CreateInt32Node(GameSave.StatItems[ItemId.Lockpicks], "Lock Picks", 999));

            listMain.Nodes.Add(statsNode);

            var ammoNode = new Node("Weapon Ammo") { Expanded = true };

            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.CarbineAmmo], "Carbine"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.VolleyGunAmmo], "Volley Gun"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.HandCannonAmmo], "Hand Cannon"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.MachineGunAmmo], "Machine Gun"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.PistolAmmo], "Pistol"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.RPGAmmo], "RPG"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.ShotgunAmmo], "Shotgun"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.SniperRifleAmmo], "Sniper Rifle"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.BurstgunAmmo], "Burstgun"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.HailFireAmmo], "Hail Fire"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.RepeaterAmmo], "Repeater"));
            ammoNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.HeaterAmmo], "Heater"));

            listMain.Nodes.Add(ammoNode);

            var upgradeNode = new Node("Infusions") { Expanded = true };
            upgradeNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.SaltsUpgrade], "Salts", 10));
            upgradeNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.HealthUpgrade], "Health", 10));
            upgradeNode.Nodes.Add(CreateInt32Node(GameSave.PlayerItems[ItemId.ShieldUpgrade], "Shield", 10));

            listMain.Nodes.Add(upgradeNode);

            var vigorsNode = new Node("Vigors") { Expanded = true };
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.MurderOfCrows], "Murder of Crows", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.ShockJockey], "Shock Jockey", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.BuckingBronco], "Bucking Bronco", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.ReturnToSender], "Return To Sender", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.Undertow], "Undertow", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.Charge], "Charge", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.Possession], "Possession", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.DevilsKiss], "Devil's Kiss", null);

            listMain.Nodes.Add(vigorsNode);

            vigorsNode = new Node("Vigor Upgrades") { Expanded = true };
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.BroncoAid], "Bronco Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.BroncoBoost], "Bronco Boost", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.ChargeAid], "Charge Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.ChargeBoost], "Charge Boost", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.CrowsTrapAid], "Crow's Trap Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.CrowsBoost], "Crow's Boost", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.DevilsKissAid], "Devil's Kiss Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.DevilsKissBoost], "Devil's Kiss Boost", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.PossessionAid], "Possession Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.PossessionForLess], "Possession For Less", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.SenderAid], "Sender Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.SendForLess], "Sender for Less", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.ShockChainAid], "Shock Chain Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.ShockDurationAid], "Shock Duration Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.UndertowAid], "Undertow Aid", null);
            InsertBoolNode(vigorsNode, GameSave.PlayerItems[ItemId.UndertowBoost], "Undertow Boost", null);

            listMain.Nodes.Add(vigorsNode);

            var gearsNode = new Node("Gears") { Expanded = true };

            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BLOODTOSALT], "BLOOD TO SALT");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SPOOFBOOSTER], "SPOOF BOOSTER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.EXECUTIONER], "EXECUTIONER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SKY_LINEACCURACY], "SKY-LINE ACCURACY");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.HEALTHFORSALTS], "HEALTH FOR SALTS");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.LASTMANSTANDING], "LAST MAN STANDING");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.STORM], "STORM");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.OVERDRIVE], "OVERDRIVE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SPECTRALSIDEKICK], "VAMPIRE'S EMBRACE"); // SPECTRAL SIDEKICK
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.NOREASTER], "NOR'EASTER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.FADETOBLACK], "FADE TO BLACK");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.PYROMANIAC], "PYROMANIAC");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.ICE_FLECKEDVIGOR], "ICE-FLECKED VIGOR");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.HAMMERBLOWS], "HAMMERBLOWS");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.FIREBIRD], "FIRE BIRD");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.RISINGBLOODLUST], "RISING BLOODLUST");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.COATOFHARMS], "COAT OF HARMS");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.THIRDHAND2], "DEATH FROM ABOVE"); // THIRD HAND 2
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.KILLTOLIVE], "KILL TO LIVE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.MACHINEWHISPERER], "MACHINE WHISPERER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BREACHCOAT], "BREACH COAT");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.STUNNING], "STUNNING");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.HEADMASTER], "HEAD MASTER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.NEIGHBORHOODCHEMIST2], "NEIGHBORHOOD CHEMIST 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SPOOFBOOSTER2], "SPOOF BOOSTER 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BLOOD_POWEREDVIGORS2], "BLOOD-POWERED VIGORS 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.CRITHACKING2], "CRIT HACKING 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.EXECUTIONER2], "EXECUTIONER 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.FADETOBLACK2], "FADE TO BLACK 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.FIERYAURA2], "FIERY AURA 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.FIGHTERACE2], "FIGHTER ACE 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.HAMMERBLOWS2], "HAMMERBLOWS 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.HEADHUNTER2], "HEADHUNTER 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.ICE_FLECKEDVIGOR2], "ICE-FLECKED VIGOR 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.LEFTHANDSPELLSLOVE2], "LEFT HAND SPELLS LOVE 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.MACHINEWHISPERER2], "MACHINE WHISPERER 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.OVERDRIVE2], "OVERDRIVE 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.PHOENIX2], "PHOENIX 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.RIGHTHANDSPELLSHATE2], "RIGHT HAND SPELLS HATE 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.RISINGBLOODLUST2], "RISING BLOODLUST 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SECONDCHANCE2], "SECOND CHANCE 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SKY_LINEACCURACY2], "SKY-LINE ACCURACY 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.EAGLESTRIKE], "EAGLE STRIKE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.STORM2], "STORM 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.STUNNING2], "STUNNING 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.TAINTEDLOVE2], "TAINTED LOVE 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.VAMPYRSEMBRACE2], "VAMPYR'S EMBRACE 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BURNINGHALO], "BURNING HALO");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.ELECTRICTOUCH], "ELECTRIC TOUCH");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SHOCKJACKET], "SHOCK JACKET");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.ELECTRICAURA2], "ELECTRIC AURA 2");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.TUNNELVISION], "TUNNEL VISION");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.GEARHEAD], "GEAR HEAD");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SKY_LINERELOADER], "SKY-LINE RELOADER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.FITASAFIDDLE], "FIT AS A FIDDLE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SCAVENGERSVEST], "SCAVENGER'S VEST");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BETRAYER], "BETRAYER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.ANGRYSTOMPERS], "ANGRY STOMPERS");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BRITTLE_SKINNED], "BRITTLE-SKINNED");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.DEADLYLUNGERS], "DEADLY LUNGERS");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.NITROVEST], "NITRO VEST");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.DROPCLOTH], "DROP CLOTH");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.HILLRUNNERSHAT], "HILL RUNNER'S HAT");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.FLEETFEET], "FLEET FEET");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.AMMOCAP], "AMMO CAP");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.HANDYMANNEMESIS], "HANDYMAN NEMESIS");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.EXTRAEXTRA], "EXTRA! EXTRA!");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SPARETHEROD], "SPARE THE ROD");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.NEWTONSLAW], "NEWTON'S LAW");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BULLRUSH], "BULL RUSH");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.BULLETBOON], "BULLET BOON");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.OVERKILL], "OVERKILL");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SHELTEREDLIFE], "SHELTERED LIFE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.URGENTCARE], "URGENT CARE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.GHOSTPOSSE], "GHOST POSSE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SUGARRUSH], "SUGAR RUSH");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.THROTTLECONTROL], "THROTTLE CONTROL");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.QUICKHANDED], "QUICK HANDED");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.WINTERSHIELD], "WINTER SHIELD");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.SKY_LINEMELEEBOOST], "SKY-LINE MELEE BOOST");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.PEAKRUNNERSHAT], "PEAK RUNNER'S HAT");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.GHOSTSOLDIER], "GHOST SOLDIER");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.AMMOADVANTAGE], "AMMO ADVANTAGE");
            CreateGearNode(gearsNode, GameSave.PlayerItems[ItemId.ELECTRICPUNCH], "ELECTRIC PUNCH");

            listGears.Nodes.Add(gearsNode);
        }

        private void GearInfoClicked(object sender, EventArgs e)
        {
            if (_bsInfo != null) return;
            if (string.IsNullOrEmpty(_xmlInfo))
                return;

            _bsInfo = new BsXmlInfoParser(_xmlInfo, false);
            string title;
            while (!string.IsNullOrEmpty((title = _bsInfo.GetNextName())))
            {
                listGearNames.Items.Add(title);
            }
        }
    }

    public class BsXmlInfoParser
    {
        public struct XGear
        {
            public string Title;
            public string Description;
        }
        //private float _version;
        private string _file;
        private List<XGear> _xgears;
        private int _index;

        public BsXmlInfoParser(string file, bool isFile)
        {
            _file = file;
            _xgears = new List<XGear>();

            Parse(isFile);
        }

        private void Parse(bool isFile)
        {
            XmlTextReader reader = !isFile ? new XmlTextReader(new StringReader(_file)) : new XmlTextReader(_file);

            while ((reader.Read()))
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "Gear")
                            _xgears.Add(ReadNode(reader));
                        break;
                }
            }
        }

        private XGear ReadNode(XmlReader reader)
        {
            if (reader.Name != "Gear")
                throw new Exception("invalid entry found in Gear listing.");

            var element = string.Empty;
            var gear = new XGear();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        element = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (element)
                        {
                            case "title":
                                gear.Title = reader.Value;
                                break;
                            case "description":
                                gear.Description = reader.Value;
                                return gear;
                        }
                        break;
                }
            }

            return gear;
        }

        public string GetNextName()
        {
            if (_index >= _xgears.Count)
            {
                // reset index
                _index = 0;
                return null;
            }
            return _xgears[_index++].Title;
        }
        public string[] GetAllNames()
        {
            var names = new List<string>();
            foreach (var gear in _xgears)
            {
                names.Add(gear.Title);
            }
            return names.ToArray();
        }

        public string GetDescription(string title)
        {
            return _xgears.Find(xgear => xgear.Title == title).Description;
        }
    }
}