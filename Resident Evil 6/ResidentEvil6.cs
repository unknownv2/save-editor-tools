using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.AdvTree;
using ResidentEvil6;

namespace Horizon.PackageEditors.Resident_Evil_6
{
    public partial class ResidentEvil6 : EditorControl
    {
        //public static readonly string FID = "43430819";
        private GameSave _gameSave;
        private List<string> _skillSetListing, _skillSetDescription;
        private string[] _characters, _weapons, _ammunition, _accessories;
        private int _lastInventoryIndex = -1, _lastNodeIndex = -1, _lastCharacterIndex = -1;

        public ResidentEvil6()
        {
            InitializeComponent();
            TitleID = FormID.ResidentEvil6;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("savedata.bin"))
                return false;

            _gameSave = new GameSave(IO);

            CreateStringInfo();
            InitializeControls();

            LoadSkills();
            DisplayStats();

            return true;
        }

        public override void Save()
        {
            _gameSave.SkillPoints = intSkillPoints.Value;
            // flush the current slot
            SetSkillSlot(_lastInventoryIndex);
            // generate the new skill unlock mask list
            var skillSet = (from DataGridViewRow row in dgvSillSet.Rows 
                            where !row.IsNewRow
                            let hidden = Convert.ToBoolean(row.Cells[0x01].Value) 
                            let unlocked = Convert.ToBoolean(row.Cells[0x02].Value)
                            select (byte) (unlocked ? 0x03 : (hidden ? 0x00 : 0x02))).ToList();
            // does the user want all 8 slots unlocked
            _gameSave.AllSkillSetsUnlocked = btnUnlockAllSkillSets.Checked;
            _gameSave.SetSkillSet(skillSet);

            // flush any remaining edits in the inventory
            SetInventoryEntryFromInformation();

            // final write to the save file
            _gameSave.Save();
        }

        private void CreateStringInfo()
        {
            _skillSetListing = new List<string>();
            _skillSetListing.AddRange(new[]
              {
                  "None",
                  "Firearm Lv. 1",
                  "Firearm Lv. 2",
                  "Firearm Lv. 3",
                  "Melee Lv. 1",
                  "Melee Lv. 2",
                  "Melee Lv. 3",
                  "Defense Lv. 1",
                  "Defense Lv. 2",
                  "Defense Lv. 3",
                  "Critical Hit Lv. 1",
                  "Critical Hit Lv. 2",
                  "Critical Hit Lv. 3",
                  "Piercing Lv. 1",
                  "Piercing Lv. 2",
                  "Piercing Lv. 3",
                  "J'avo Killer Lv. 1",
                  "J'avo Killer Lv. 2",
                  "Zombie Hunter Lv. 1",
                  "Zombie Hunter Lv. 2",
                  "Grenade Power-Up",
                  "Handgun Master",
                  "Shotgun Master",
                  "Magnum Master",
                  "Sniper Master",
                  "Machine Pistol Master",
                  "Assault Rifle Master",
                  "Grenade Launcher Master",
                  "Crossbow Master",
                  "Infinite Handgun",
                  "Infinite Shotgun",
                  "Infinite Magnum",
                  "Infinite Sniper Rifle",
                  "Infinite Machine Pistol",
                  "Infinite Assault Rifle",
                  "Infinite Grenade Launcher",
                  "Infinite Crossbow",
                  "AR Ammo Pickup Increase",
                  "Shotgun Shell Pickup Increase",
                  "Magnum Ammo Pickup Increase",
                  "Rifle Ammo Pickup Increase",
                  "Grenade Pickup Increase",
                  "Arrow Pickup Increase",
                  "Last Shot",
                  "Stealth Movement",
                  "Quick Reload",
                  "Lock-On Lv. 1",
                  "Lock-On Lv. 2",
                  "Rock Steady Lv. 1",
                  "Rock Steady Lv. 2",
                  "Breakout",
                  "Item Drop Increase",
                  "Recovery Lv. 1",
                  "Recovery Lv. 2",
                  "Combat Gauge Boost Lv. 1",
                  "Combat Gauge Boost Lv. 2",
                  "Eagle Eye",
                  "Team-Up",
                  "Field Medic Lv. 1",
                  "Field Medic Lv. 2",
                  "Lone Wolf",
                  "Shooting Wild",
                  "Go for Broke!",
                  "Time Bonus +",
                  "Combo Bonus +",
                  "Limit Breaker",
                  "Blitz Play",
                  "Quick Shot Damage Increase",
                  "Power Counter",
                  "Second Wind",
                  "Martial Arts Master",
                  "Target Master",
                  "Last Stand",
                  "Revenge",
                  "Preemptive Strike",
                  "Gunslinger",
                  "Dying Breath",
                  "Pharmacist",
                  "Medic",
                  "First Responder",
                  "Take It Easy",
                  "Natural Healing",
                  "Creature Point Boost",
                  "Creature Point Increase",
                  "Training",
                  "CREATURE OFFENSE LV. 1",
                  "CREATURE OFFENSE LV. 2",
                  "CREATURE OFFENSE LV. 3",
                  "CREATURE DEFENSE LV. 1",
                  "CREATURE DEFENSE LV. 2",
                  "CREATURE DEFENSE LV. 3",
                  "CREATURE HEALTH LV. 1",
                  "CREATURE HEALTH LV. 2",
                  "CREATURE HEALTH LV. 3",
                  "CREATURE STAMINA LV. 1",
                  "CREATURE STAMINA LV. 2"
              });
            _skillSetDescription = new List<string>();
            _skillSetDescription.AddRange(new[]
              {
                  "No skills selected.",
                  "Slightly increases firearm power.",
                  "Increases firearm power.",
                  "Greatly increases firearm power.",
                  "Slightly increases power during melee attacks.",
                  "Increases power during melee attacks.",
                  "Greatly increases power during melee attacks.",
                  "Slightly reduces damage received.",
                  "Reduces damage received.",
                  "Greatly reduces damage received.",
                  "Slightly increases the chances of scoring a critical hit.",
                  "Increases the chances of scoring a critical hit.",
                  "Greatly increases the chances of scoring a critical hit.",
                  "Slightly increases firearm piercing potential.",
                  "Increases firearm piercing potential.",
                  "Greatly increases firearm piercing potential.",
                  "Increases strength of attacks on a J'avo.",
                  "Greatly increases strength of attacks on a J'avo.",
                  "Increases strength of attacks on a zombie.",
                  "Greatly increases strength of attacks on a zombie.",
                  "Increases the power of grenades.",
                  "Increases the power of handguns.",
                  "Increases the power of shotguns.",
                  "Increases the power of Magnums.",
                  "Increases the power of sniper rifles.",
                  "Increases the power of machine pistols.",
                  "Increases the power of assault rifles.",
                  "Increases the power of grenade launchers.",
                  "Increases the power of crossbows.",
                  "Provides infinite ammo for handguns.",
                  "Provides an infinite number of shotgun shells.",
                  "Provides infinite ammo for Magnums.",
                  "Provides infinite ammo for sniper rifles.",
                  "Provides infinite ammo for machine pistols.",
                  "Provides infinite ammo for assault rifles.",
                  "Provides infinite number of grenades for grenade launchers.",
                  "Provides infinite number of crossbow arrows.",
                  "Allows you to pick up an increased amount of 5.56 NATO ammo.",
                  "Allows you to pick up an increased number of 10- and 12-gauge shotgun shells."
                  ,
                  "Allows you to pick up an increased amount of .50 Action-Express and .500 S&W Magnum ammo."
                  ,
                  "Allows you to pick up an increased amount of 7.62 NATO and 12.7mm ammo."
                  ,
                  "Allows you to pick up an increased number of 40mm explosive, acid, and nitrogen rounds."
                  ,
                  "Allows you to pick up an increased number of normal and pipe bomb arrows."
                  ,
                  "Greatly increases the strength of your final remaining shot.",
                  "Gain the skill to move silently.",
                  "Reload your weapons quickly.",
                  "Steadies hand when shooting.",
                  "Greatly steadies hand when shooting.",
                  "Reduces recoil after shooting.",
                  "Greatly reduces recoil after shooting.",
                  "Allows you to break free easily from an enemy's grasp.",
                  "Causes more defeated enemies to drop items.",
                  "Speeds up recovery time when dying.",
                  "Greatly speeds up recovery time when dying.",
                  "Increases your Combat Gauge by 3 blocks.",
                  "Increases your Combat Gauge by 5 blocks.",
                  "Adds an extra level of magnification to sniper rifle scope.",
                  "Strengthens your partner's attacks when you are near each other. (Single player only)"
                  ,
                  "Allows your partner to give you a few health tablets when you're rescued. (Single player only)"
                  ,
                  "Allows your partner to give you some health tablets when you're rescued. (Single player only)"
                  ,
                  "Keeps your partner from helping you when you're in trouble. (Single player only)"
                  ,
                  "Removes your targeting sight, but increases your firepower.",
                  "Makes it easier to pull off combos as time runs out.",
                  "Increases time awarded by a Time Bonus.",
                  "Increases time for a Combo Bonus.",
                  "Increases the points earned for surpassing 50 combos.",
                  "Increases your attack power if you attack immediately after another player."
                  ,
                  "Increases power of quick shots.",
                  "Greatly increases the power of counters.",
                  "Increases power to firearms and melee attacks when life gauge is low."
                  ,
                  "Increases strength of physical attacks, but the power of firearm-related attacks is greatly reduced."
                  ,
                  "Increases strength of firearm-related attacks, but the power of physical attacks is greatly reduced."
                  ,
                  "Greatly increases attack power, but attacks on you hurt three times as much."
                  ,
                  "Increases your attack power against the person who last defeated you."
                  ,
                  "Increases attack power when you attack from behind.",
                  "Increases firing and reload speed.",
                  "Greatly increases attack power when dying, but recovery time is comparably shortened."
                  ,
                  "Increases the potency of health tablets.",
                  "Heals a teammate that's far away when you use a health tablet.",
                  "Makes it easier to rescue dying teammates.",
                  "Increases speed of natural recovery when using cover.",
                  "Increases speed of natural recovery.",
                  "Gives you a large number of Creature Points at start of the game.",
                  "Increases speed at which you gain Creature Points.",
                  "Become impervious to damage except for instant deaths and infinite weapons. No score awarded."
                  ,
                  "Slightly increases offense when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Increases offense when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Greatly increases offense when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Slightly increases defense when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Increases defense when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Greatly increases defense when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Slightly increases health when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Increases health when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Greatly increases health when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Slightly increases stamina when playing as a creature. For use in Agent Hunt only."
                  ,
                  "Increases stamina when playing as a creature. For use in Agent Hunt only."
              }
            );

            _characters = new[]
                 {
                     "Leon S. Kennedy",
                     "Helena Harper",
                     "Chris Redfield",
                     "Piers Nivans",
                     "Jake Muller", 
                     "Sherry Birkin",
                     "Ada Wong",                                         
                 };
            _weapons = _gameSave.GetClassItems(GameSave.Inventory.Class.Weapon);
            _ammunition = _gameSave.GetClassItems(GameSave.Inventory.Class.Ammunition);
            _accessories = _gameSave.GetClassItems(GameSave.Inventory.Class.Accessory);
        }

        private void InitializeControls()
        {
            cmbCharacterInv.Items.AddRange(_characters);

            // we don't want to include the last 12, which are specific to the Agent Hunt mode only
            var newSkillList = _skillSetListing.GetRange(0, 85).ToArray();
            cmbCmpSkill1.Items.AddRange(newSkillList);
            cmbCmpSkill2.Items.AddRange(newSkillList);
            cmbCmpSkill3.Items.AddRange(newSkillList);
            cmbMercSkill.Items.AddRange(newSkillList);

            // create 8 skill slots for editing
            for (int i = 0; i < GameSave.SlotCount; i++)
                cmbSkillSlot.Items.Add((i + 1).ToString());

            cmbClass.Items.AddRange(new [] { "None", "Weapon", "Ammunition", "Accessory"});
            cmbSkillSlot.SelectedIndex = 0x00;
            cmbCharacterInv.SelectedIndex = 0x00;
        }

        private void LoadSkills()
        {
            // Make sure we skip the Training Skill
            for (int i = 1; i < GameSave.SkillCount; i++)
            {
                dgvSillSet.Rows.Add
                    (new object[]
                         {
                             _skillSetListing[i],
                             (_gameSave.SkillSet[i] & 0x02) == 0x00,
                             (_gameSave.SkillSet[i] > 0x2)
                         }
                    );
                dgvSillSet.Rows[i - 1].Cells[0x00].ToolTipText = _skillSetDescription[i];
            }
            btnUnlockAllSkillSets.Checked = _gameSave.AllSkillSetsUnlocked;
        }

        private void LoadInventory(int character)
        {
            // clear old inventory entries
            advInventory.Nodes.Clear();
            // load Inventory Tree
            var inventory = _gameSave.CharacterInventories[character];
            foreach (var item in inventory.Items)
            {
                var node = new Node(item.ClassName);
                var amounts = ParseAmountFromEntry(item);
                node.Nodes.AddRange(new[]
                {
                    new Node(item.ItemName),
                    new Node(amounts[0x00].ToString()),
                    new Node(amounts[0x01].ToString()),
                    new Node(amounts[0x02].ToString()) 
                });
                advInventory.Nodes.Add(node);
            }
        }

        private void FillInventoryEntryInformation(Node entry)
        {
            var item = _gameSave.CharacterInventories[cmbCharacterInv.SelectedIndex].Items[entry.Index];
            if(item.Amount != -2)
            {
                cmbClass.SelectedIndex = (int) item.ClassId;
                cmbItem.Text = item.ItemName;
                var amounts = ParseAmountFromEntry(item);
                intInpAmount1.Value = amounts[0x00];
                intInpAmount2.Value = amounts[0x01];
                intInpAmount3.Value = amounts[0x02];
            }
            else
            {
                cmbClass.SelectedIndex = 0x00;
                cmbItem.SelectedIndex = 0x00;
                intInpAmount1.Value = intInpAmount2.Value = intInpAmount3.Value = 0x00;
            }
            // set the last node index so we don't update the information multiple times
            _lastNodeIndex = entry.Index;
        }

        private void SetInventoryEntryFromInformation()
        {
            if (_lastNodeIndex == -1 || _lastCharacterIndex == -1)
                return;

            // update the entry in the inventory listing
            var item = _gameSave.CharacterInventories[_lastCharacterIndex].Items[_lastNodeIndex];
            GameSave.Inventory.Class classId = (GameSave.Inventory.Class) cmbClass.SelectedIndex;
            item.EntryId = _gameSave.GetEntryIdFromItem(classId, cmbItem.Text);
            var amounts = new[] {intInpAmount1.Value, intInpAmount2.Value, intInpAmount3.Value};
            if (item.EntryId != 0x00)
                SetAmountForEntry(item, amounts);
            else
                item.Amount = -2;

            // update the names for easy retrieval when reloading an inventory
            item.ClassName = cmbClass.Text;
            item.ItemName = cmbItem.Text;

            // update the information in the inventory tree
            var nodeItem = advInventory.Nodes[_lastNodeIndex];
            nodeItem.Text = cmbClass.Text;
            nodeItem.Nodes[0x0].Text = cmbItem.Text;
            for (var i = 0; i < amounts.Length; i++)
                nodeItem.Nodes[i + 1].Text = amounts[i].ToString();
        }

        private int[] ParseAmountFromEntry(GameSave.Inventory.Entry item)
        {
            var amounts = new int[0x03];
            var amount = item.Amount == -2 ? 0x00 : item.Amount;
            switch (item.EntryId)
            {
                case 0x103:
                case 0x110:
                case 0x117:
                    amounts[0x00] = (amount & 0xFF);
                    amounts[0x01] = ((amount >> 0x08) & 0xFF);
                    break;
                case 0x112:
                    amounts[0x00] = amount & 0x1F;
                    amounts[0x01] = ((amount >> 0x05) & 0x1F);
                    amounts[0x02] = ((amount >> 0x0A) & 0x1F);
                    break;
                default:
                    amounts[0x0] = amount;
                    break;
            }
            return amounts;
        }

        private void SetAmountForEntry(GameSave.Inventory.Entry item, int[] amounts)
        {
            switch (item.EntryId)
            {
                case 0x103:
                case 0x110:
                case 0x117:
                    item.Amount = (short) ((amounts[0x00] & 0xFF) | (((amounts[0x01] & 0xFF) << 0x08) & 0xFF00));
                    break;
                case 0x112:
                    item.Amount = (short)((amounts[0x00] & 0x1F) | ((amounts[0x01] << 0x05) & 0x3E0) | ((amounts[0x02] << 0x0A) & 0x7C00));
                    break;
                default:
                    item.Amount = (short)(amounts[0x00] & 0xFFFF);
                    break;
            }
        }

        private void ChangeSkillSlot(int slotIndex)
        {
            var campaignSkills = _gameSave.CampaignSkillSettings[slotIndex];
            cmbCmpSkill1.SelectedIndex = campaignSkills[0x00];
            cmbCmpSkill2.SelectedIndex = campaignSkills[0x01];
            cmbCmpSkill3.SelectedIndex = campaignSkills[0x02];
            cmbMercSkill.SelectedIndex = _gameSave.MercenarySkillSettings[slotIndex];
            // set the last slot index so we can update it
            _lastInventoryIndex = slotIndex;
        }

        private void SetSkillSlot(int slot)
        {
            var campaignSkills = _gameSave.CampaignSkillSettings[slot];
            campaignSkills[0x00] = cmbCmpSkill1.SelectedIndex;
            campaignSkills[0x01] = cmbCmpSkill2.SelectedIndex;
            campaignSkills[0x02] = cmbCmpSkill3.SelectedIndex;
            _gameSave.MercenarySkillSettings[slot] = cmbMercSkill.SelectedIndex;
        }

        private void DisplayStats()
        {
            intSkillPoints.Value = _gameSave.SkillPoints;
        }

        private void BtnClickUnlockAllSkills(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSillSet.Rows)
                if (!row.IsNewRow)
                    row.Cells[0x02].Value = true;
        }

        private void CmbChangeCurrentCharacterInventory(object sender, EventArgs e)
        {
            if (_lastCharacterIndex != -1 && _lastNodeIndex != -1)
                SetInventoryEntryFromInformation();

            LoadInventory(cmbCharacterInv.SelectedIndex);
            _lastCharacterIndex = cmbCharacterInv.SelectedIndex;
            _lastNodeIndex = -1;

            cmbItem.SelectedIndex = cmbClass.SelectedIndex = 0x00;
            intInpAmount1.Value = intInpAmount2.Value = intInpAmount3.Value = 0x00;
        }

        private void CmbChangeCurrentSlot(object sender, EventArgs e)
        {
            if (_lastInventoryIndex != -1)
                SetSkillSlot(_lastInventoryIndex);

            ChangeSkillSlot(cmbSkillSlot.SelectedIndex);
        }

        private void CmbSelectedClassChanged(object sender, EventArgs e)
        {
            // Clear all previous entries
            cmbItem.Items.Clear();
            // Fill the item listing
            GameSave.Inventory.Class currentClass = (GameSave.Inventory.Class) cmbClass.SelectedIndex;
            switch (currentClass)
            {
                    case GameSave.Inventory.Class.None:
                    cmbItem.Items.Add("None");
                    break;
                    case GameSave.Inventory.Class.Weapon:
                    cmbItem.Items.AddRange(_weapons);
                    break;
                    case GameSave.Inventory.Class.Ammunition:
                    cmbItem.Items.AddRange(_ammunition);
                    break;
                case GameSave.Inventory.Class.Accessory:
                    cmbItem.Items.AddRange(_accessories);
                    break;
            }
        }

        private void TreeSelectedInventoryItemChanged(object sender, EventArgs e)
        {
            if (advInventory.SelectedNode == null || (advInventory.SelectedNode.Nodes.Count < 2) || advInventory.SelectedNode.Index == _lastNodeIndex)
                return;

            if (_lastNodeIndex != -1)
                SetInventoryEntryFromInformation();

            FillInventoryEntryInformation(advInventory.SelectedNode);
        }
    }
}
