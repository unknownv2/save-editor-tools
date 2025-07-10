using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using Horizon.Functions;
using System.IO;

namespace Horizon.PackageEditors.Skyrim
{
    public partial class Skyrim : EditorControl
    {
        //public static readonly string FID = "425307E6";
        private bool _injectedPlayerRecord, _injectedPlayerNPCRecord;
        public Skyrim()
        {
            InitializeComponent();
            TitleID = FormID.Skyrim;
#if INT2
            tabActiveEffects.Visible = true;
#endif
        }

        private SkyrimSave saveObj;
        private PlayerRecord PlayerRecord;
        private NPCRecord NPCRecord;

        //private static XmlParser skillsParser;
        private PlayerRecord.InventoryItem CurrentInventoryItem;

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            IO.EndianType = EndianType.LittleEndian;

            saveObj = new SkyrimSave(IO);

            ReadSave();

            pcPath = null;

            return true;
        }

        private string pcPath = null;
        private void cmdOpenPCSave_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog {Filter = "Skyrim Saves (*.ess, *.dat)|*.ess;*.dat"};

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            this.pcPath = ofd.FileName;

            IO = new EndianIO(this.pcPath, EndianType.LittleEndian, true);

            saveObj = new SkyrimSave(IO);

            this.ReadSave();

            IO.Close();

            setPackageOverride(this.pcPath);

            enablePanels(true);
        }

        private void ReadSave()
        {
            var playerForm = saveObj.ChangeForms.FindForm(FormType.ACHR, 20);

            if (playerForm == null)
                throw new Exception("Player form not found!");

            var npcForm = saveObj.ChangeForms.FindForm(FormType.NPC_, 7);
            
            if(npcForm == null)
                throw new Exception("NPC form not found.");

            PlayerRecord = new PlayerRecord(playerForm);
            NPCRecord = new NPCRecord(npcForm);

            txtPlayerRecordFlags.Text = BitConverter.GetBytes(playerForm.Flags).ToHexString();

            // record loaded
            LoadPlayerData();
            listGlobalData.Items.Clear();
            for (int i = 0; i < 3; i++)
            {
                listGlobalData.Items.Add(new ListViewItem(i.ToString()));
            }
        }

        public override void Save()
        {
            // save edited player data
            if (!_injectedPlayerRecord)
                SavePlayerData();
            if (!_injectedPlayerNPCRecord)
                SavePlayerNPCData();

            if (pcPath != null)
            {
                File.Delete(pcPath);
                IO = new EndianIO(pcPath, EndianType.LittleEndian, true);
                saveObj.Write(IO);
                IO.Close();
                return;
            }

            var io = new EndianIO(new MemoryStream(), EndianType.LittleEndian, true);
            saveObj.Write(io);
            io.Close();

            IO.Position = 0;
            IO.Out.Write(io.ToArray());
        }

        private void LoadPlayerData()
        {
            // clear views and trees
            listPlayerStats.Nodes.Clear();
            listSkills.Nodes.Clear();
            listPerks.Nodes.Clear();
            listSpellList.Items.Clear();
            listSpellActive.Items.Clear();
            listInventory.Items.Clear();
            listInventoryActive.Items.Clear();
            listShouts.Nodes.Clear();
            treeMagic.Nodes.Clear();
            listEffects.Items.Clear();
            treeDragonShouts.Nodes.Clear();

            // Player Stats
            intGold.Value = 140 + PlayerRecord.Inventory[PlayerRecord.FormId.Gold].Value;
            intLockpicks.Value = 10 + PlayerRecord.Inventory[PlayerRecord.FormId.Lockpick].Value;

            // NPC Values
            intLevel.Value = NPCRecord.Level;
            txtPlayerName.Text = NPCRecord.Name;

            listPlayerStats.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Magicka, "Magicka", float.MaxValue));
            listPlayerStats.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Health, "Health", float.MaxValue));
            listPlayerStats.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Stamina, "Stamina", float.MaxValue));
            //listPlayerStats.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.CurrentCarryWeight, "Current Carry Weight", float.MaxValue));
            //listPlayerStats.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.BaseCarryWeight, "Base Carry Weight", float.MaxValue));
            //listPlayerStats.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.PerksToIncrease, "Perks to increase", float.MaxValue));

            // load the skills
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Alteration, "Alteration", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Conjuration, "Conjuration", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Destruction, "Destruction", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Illusion, "Illusion", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Restoration, "Restoration", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Enchanting, "Enchanting", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Archery, "Archery", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Block, "Block", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.HeavyArmor, "Heavy Armor", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.OneHanded, "One-Handed", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.TwoHanded, "Two-Handed", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Smithing, "Smithing", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Alchemy, "Alchemy", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.LightArmor, "Light Armor", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.LockPicking, "Lockpicking", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.PickPocket, "Pickpocket", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Sneak, "Sneak", 100));
            listSkills.Nodes.Add(CreateDoubleNode(PlayerRecord.PlayerAttribute.Speech, "Speech", 100));
            
            LoadPlayerInventory();
            LoadPlayerPerks();
            LoadPlayerSpells();
            LoadPlayerShouts();
            //LoadEffects();
        }

        private void SavePlayerData()
        {
            PlayerRecord.Inventory[PlayerRecord.FormId.Gold].Value = (int) (intGold.Value - 140);
            PlayerRecord.Inventory[PlayerRecord.FormId.Lockpick].Value = (int) (intLockpicks.Value - 10);

            saveObj.Header.Name = txtPlayerName.Text;

            PlayerRecord.Save();
            
        }
        private void SavePlayerNPCData()
        {
            NPCRecord.SetLevel((int)intLevel.Value);
            NPCRecord.Name = txtPlayerName.Text;

            saveObj.Header.Level = (int)intLevel.Value;

            NPCRecord.Save();
        }
        private void LoadEffects()
        {
            foreach (var playerEffect in PlayerRecord.Effects)
            {
                listEffects.Items.Add(TransformRefId(playerEffect.Id).ToString("X"));
            }
        }

        private void LoadPlayerInventory()
        {
            foreach (var item in PlayerRecord.Inventory.Items)
            {
                var refId = TransformRefId(item.IRef);
                listInventoryActive.Items.Add(new ListViewItem(SkyDictionaries.Items.ContainsKey(refId) ? SkyDictionaries.Items[refId].Name : refId.ToString("X")) { Tag = refId});
            }


            foreach (KeyValuePair<int, SkyBase> id in SkyDictionaries.Items)
            {
                var listItem = new ListViewItem(id.Value.Name) {Tag = id.Key};
                listInventory.Items.Add(listItem);
            }
        }

        private void LoadPlayerSpells()
        {
            foreach (KeyValuePair<string, Dictionary<string, string[]>> magic in SkyDictionaries.Magic)
            {
                if(magic.Key == "Word")
                    continue;
                var spellNode = new Node(magic.Key);
                foreach (KeyValuePair<string, string[]> spell in magic.Value)
                {
                    if (spell.Value.Length > 1)
                    {
                        var sNode = new Node(spell.Key);
                        foreach (string s in spell.Value)
                        {
                            InsertBoolNode(sNode, s, (int)Enum.Parse(typeof(MagicEnums), s), ValueType.PlayerSpell);
                        }
                        spellNode.Nodes.Add(sNode);
                    }
                    else
                    {
                        InsertBoolNode(spellNode, spell.Key, (int)Enum.Parse(typeof(MagicEnums), spell.Value[0]),ValueType.PlayerSpell);
                    }
                }
                treeMagic.Nodes.Add(spellNode);
            }
            foreach (var item in PlayerRecord.Spells.SpellsList)
            {
                if (Enum.IsDefined(typeof(MagicEnums), (MagicEnums)TransformRefId(item)))
                    listSpellActive.Items.Add(((MagicEnums)TransformRefId(item)).ToString());
            }
            var formIds = Enum.GetValues(typeof(MagicEnums));
            foreach (MagicEnums id in formIds)
            {
                listSpellList.Items.Add(id.ToString());
            }
        }

        private void LoadPlayerShouts()
        {
            var shouts = new Node("Shouts");
            foreach (KeyValuePair<string, string[]> word in SkyDictionaries.Magic["Word"])
            {
                InsertBoolNode(shouts, word.Key, (int)Enum.Parse(typeof(MagicEnums), word.Value[0]), ValueType.PlayerShout);
            }

            listShouts.Nodes.Add(shouts);
            shouts = new Node("Dragon Shouts");
            foreach (KeyValuePair<string, string> dragonShout in SkyDictionaries.DragonShouts)
            {
                InsertBoolNode(shouts, dragonShout.Key, (int)Enum.Parse(typeof(ShoutEnums), dragonShout.Value), ValueType.PlayerDragonShout);
            }

            treeDragonShouts.Nodes.Add(shouts);
        }

        private void LoadPlayerPerks()
        {
            foreach (var skillTree in SkyDictionaries.Perks)
            {
                var skill = new Node(skillTree.Key);
                foreach (var perk in SkyDictionaries.Perks[skillTree.Key])
                {
                    if (perk.Value.Length> 1)
                    {
                        var perkNode = new Node(perk.Key);
                        var i = 0;
                        Array.Sort(perk.Value);
                        foreach (var child in perk.Value)
                        {
                            InsertBoolNode(perkNode, string.Format("{0} ({1}/{2})", perk.Key, ++i, perk.Value.Length),
                                           (int)Enum.Parse(typeof(PerkEnums), child), ValueType.PlayerPerk);
                        }
                        skill.Nodes.Add(perkNode);
                    }
                    else
                    {
                        InsertBoolNode(skill, perk.Key, (int)Enum.Parse(typeof(PerkEnums), perk.Value[0]), ValueType.PlayerPerk);
                    }
                }
                listPerks.Nodes.Add(skill);
            }
        }
       
        private void cmdExtractGlobalData_Click(object sender, EventArgs e)
        {
            var item = listGlobalData.SelectedIndices[0];
            var sfd = new SaveFileDialog {FileName = string.Format("DataTable_{0:X1}.bin", item)};
            if(sfd.ShowDialog() != DialogResult.OK)
                return;

            var io = new EndianIO(sfd.FileName, EndianType.BigEndian, true);
            switch (item)
            {
                case 0:
                    saveObj.DataTable1.Save(io);

                    break;
                case 1:
                    saveObj.DataTable2.Save(io);
                    break;
                case 2:
                    saveObj.DataTable3.Save(io);
                    break;

            }
            io.Close();
        }

        private void cmdInjectGlobalData_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if(ofd.ShowDialog() != DialogResult.OK)
                return;

            var gdIndex = listGlobalData.SelectedIndices[0];
            var io = new EndianIO(ofd.FileName, EndianType.BigEndian, true);
            switch (gdIndex)
            {
                case 0:
                    saveObj.DataTable1.Inject(io);
                    break;
                case 1:
                    saveObj.DataTable2.Inject(io);
                    break;
                case 2:
                    saveObj.DataTable3.Inject(io);
                    break;
            }
            io.Close();
        }

        private void cmdExtractEffect_Click(object sender, EventArgs e)
        {
            if(listEffects.SelectedItems.Count == 0)
                return;
            foreach (int selectedItem in listEffects.SelectedIndices)
            {
                var sfd = new SaveFileDialog();
                sfd.FileName = listEffects.Items[selectedItem].Text;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                File.WriteAllBytes(sfd.FileName, PlayerRecord.Effects[selectedItem].Data);
            }
        }

        private void cmdInjectEffect_Click(object sender, EventArgs e)
        {
            if (listEffects.SelectedItems.Count == 0)
                return;
            foreach (int selectedItem in listEffects.SelectedIndices)
            {
                var ofd = new OpenFileDialog();
                if(ofd.ShowDialog() != DialogResult.OK)
                    return;

                PlayerRecord.Effects[selectedItem].Inject(File.ReadAllBytes(ofd.FileName));
            }

        }
        private void cmdExtractPlayerRecord_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog {FileName = "PlayerRecord.bin"};

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var playerForm = saveObj.ChangeForms.FindForm(FormType.ACHR, 20);

            File.WriteAllBytes(sfd.FileName, playerForm.GetData());
        }

        private void cmdExtractPlayerNPCRecord_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { FileName = "NPCPlayerRecord.bin" };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllBytes(sfd.FileName, NPCRecord.ToArray());
        }

        private void cmdExtractRecords_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            string basePath = fbd.SelectedPath;

            foreach (ChangeForm changeForm in saveObj.ChangeForms)
            {
                string formTypePath = Path.Combine(basePath, changeForm.Type.ToString());

                if (!Directory.Exists(formTypePath))
                    Directory.CreateDirectory(formTypePath);

                string fileName = string.Format("{0}_{1}_0x{2:X8}.bin", changeForm.Type, changeForm.Id.FormID, changeForm.Flags);

                File.WriteAllBytes(Path.Combine(formTypePath, fileName), changeForm.GetData());
            }
        }

        private void cmdInjectPlayerRecord_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            saveObj.ChangeForms.FindForm(FormType.ACHR, 20).SetData(File.ReadAllBytes(ofd.FileName), true);
            _injectedPlayerRecord = true;
        }

        private void cmdInjectPlayerNPCRecord_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

             saveObj.ChangeForms.FindForm(FormType.NPC_, 7).SetData(File.ReadAllBytes(ofd.FileName), true);
            _injectedPlayerNPCRecord = true;
        }

        private void cmdAddItemToInventory_Click(object sender, EventArgs e)
        {
            if(listInventory.SelectedItems.Count > 0)
            {
                var item = listInventory.SelectedItems[0];
                var formId = (int) item.Tag;
                RefID id;
                if(CreateRefId(formId, out id))
                {
                    if (!PlayerRecord.Inventory.Items.Exists(i => i.IRef.FormID == id.FormID))
                    {
                        PlayerRecord.Inventory.AddItem(id, 1);
                        listInventoryActive.Items.Add(new ListViewItem(item.Text) { Tag = formId });
                    }
                    else
                    {
                        UI.messageBox("The item is already in the inventory.");
                    }
                }
            }
        }

        private void cmdAddAmountToItemInInventory_Click(object sender, EventArgs e)
        {
            if (listInventoryActive.SelectedItems.Count > 0)
            {
                var item = listInventoryActive.SelectedItems[0];
                var formId = (int)item.Tag;
                RefID id;
                if (CreateRefId(formId, out id))
                {
                    if (PlayerRecord.Inventory.Items.Exists(i => i.IRef.FormID == id.FormID))
                    {
                        var inventoryItem = PlayerRecord.Inventory.Items.Find(i => i.IRef.FormID == id.FormID);
                        inventoryItem.Value = intItemValue.Value;
                    }
                    else
                    {
                        UI.messageBox("ERROR: Could not find the item in the inventory.");
                    }
                }
            }
        }

        private void cmdAddSpellToActiveList_Click(object sender, EventArgs e)
        {
            if (listSpellList.SelectedItems.Count == 1)
            {
                var itemName = listSpellList.SelectedItems[0].Text;
                RefID id;
                if(CreateRefId((int)Enum.Parse(typeof(MagicEnums), itemName), out id))
                {
                    PlayerRecord.Spells.AddItem(id);
                    listSpellActive.Items.Add(itemName);
                }
            }
        }

        private void cmdRemoveSpell_Click(object sender, EventArgs e)
        {
            if (listSpellActive.SelectedItems.Count == 1)
            {
                var item = listSpellActive.SelectedItems[0];
                RefID id;
                if (CreateRefId((int)Enum.Parse(typeof(MagicEnums), item.Text), out id))
                {
                    PlayerRecord.Spells.RemoveItem(id);
                    listSpellActive.Items.Remove(item);
                }
            }
        }

        private void cmdUnlockAllPerks_Click(object sender, EventArgs e)
        {
            var perkNodes = listPerks.Nodes;

            foreach (Node node in perkNodes)
            {
                CheckAllChildNodes(node, true);
            }
        }

        private void cmdUnlockAllShouts_Click(object sender, EventArgs e)
        {
            foreach (Node node in listShouts.Nodes)
            {
                CheckAllChildNodes(node, true);
            }
            foreach (Node node in treeDragonShouts.Nodes)
            {
                CheckAllChildNodes(node, true);
            }
        }

        private void cmdMaxAllSkillLevels_Click(object sender, EventArgs e)
        {
            foreach (Node skillNode in listSkills.Nodes)
            {
                var dbInput = (skillNode.Cells[1].HostedControl as DoubleInput);
                if(dbInput!= null)
                {
                    dbInput.Value = dbInput.MaxValue;
                }
            }
        }

        private void cmdAddItemToInventoryById_Click(object sender, EventArgs e)
        {
            var id = int.Parse(txtInvItemId.Text, NumberStyles.HexNumber, CultureInfo.CurrentCulture);
            RefID refId;
            if(CreateRefId(id, out refId))
            {
                if(!PlayerRecord.Inventory.Items.Exists(i => i.IRef.FormID == refId.FormID))
                {
                    PlayerRecord.Inventory.AddItem(refId, 1);

                    // add the item to the 'active' inventory
                    listInventoryActive.Items.Add(SkyDictionaries.Items.ContainsKey(id) ? SkyDictionaries.Items[id].Name : id.ToString("X"));
                    
                }
                else
                {
                    UI.messageBox("The item is already in the inventory.");
                }
            }
        }

        private void cmdRemoveItemFromInventory_Click(object sender, EventArgs e)
        {
            if (listInventoryActive.SelectedItems.Count == 1)
            {
                var item = listInventoryActive.SelectedItems[0];
                var formId = (int) item.Tag;
                RefID id;
                if (CreateRefId(formId, out id))
                {
                    PlayerRecord.Inventory.Items.Remove(PlayerRecord.Inventory.Items.Find(i => i.IRef.FormID == id.FormID));

                    listInventoryActive.Items.Remove(item);
                }
            }
        }

        private void cmdAddSpellById_Click(object sender, EventArgs e)
        {
            var id = int.Parse(txtMagicId.Text, NumberStyles.HexNumber, CultureInfo.CurrentCulture);
            if (!PlayerRecord.Spells.SpellsList.Exists(refId => refId.FormID == ParseRefId(id)))
            {
                RefID refId;
                if (CreateRefId(id, out refId))
                    PlayerRecord.Spells.SpellsList.Add(refId);
            }
        }
        private void cmdAddEffectById_Click(object sender, EventArgs e)
        {
            var id = int.Parse(txtEffectId.Text, NumberStyles.HexNumber, CultureInfo.CurrentCulture);
            RefID refId;
            if (CreateRefId(id, out refId))
                PlayerRecord.Effects.Add(refId);
        }
        private void InventorySelectedIndexChanged(object sender, EventArgs e)
        {
            if(listInventoryActive.SelectedItems.Count == 1)
            {
                if (CurrentInventoryItem != null)
                {
                    CurrentInventoryItem.Value = intItemValue.Value;
                }
                var item = listInventoryActive.SelectedIndices[0];
                CurrentInventoryItem = PlayerRecord.Inventory[item];

                intItemValue.Value = CurrentInventoryItem.Value;
            }
        }

        private static void CheckAllChildNodes(Node treeNode, bool nodeChecked)
        {
            foreach (Node node in treeNode.Nodes)
            {
                if ((node.HostedItem as CheckBoxItem) != null)
                    (node.HostedItem as CheckBoxItem).Checked = nodeChecked;

                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private Node CreateDoubleNode(PlayerRecord.PlayerAttribute key, string title, double maxValue)
        {
            return CreateDoubleNode((uint)key, title, maxValue, ValueType.PlayerSkill);
        }

        private Node CreateDoubleNode(uint key, string title, double maxValue, ValueType valueType)
        {
            var doubleInput = new DoubleInput {Tag = key};
            switch (valueType)
            {
                    case ValueType.PlayerSkill:
                    doubleInput.Value = PlayerRecord.Skills[(PlayerRecord.PlayerAttribute)key];
                    doubleInput.ValueChanged += SkillDouble_ValueChanged;
                    break;
            }
            
            doubleInput.MinValue = 0;
            doubleInput.MaxValue = maxValue;
            doubleInput.ShowUpDown = true;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = doubleInput });

            return node;
        }

        private void InsertBoolNode(Node host, string title, int id, ValueType valueType)
        {
            var ckInput = new CheckBoxItem { Tag = id };

            switch (valueType)
            {
                    case ValueType.PlayerPerk:
                    ckInput.Checked = PlayerRecord.Perks.PerkTable.Exists(item => item.Id.FormID == ParseRefId(id));
                    ckInput.CheckedChanged += ckInputPerk_CheckedChanged;
                    break;
                    case ValueType.PlayerShout:
                    {
                        if (saveObj.ChangeForms.Exists(record => record.Id.FormID == (id & 0x3FFFFF)))
                        {
                            ckInput.Checked =
                                saveObj.ChangeForms.Find(record => record.Id.FormID == (id & 0x3FFFFF)).GetData()[0x2] !=
                                0;
                        }
                        
                    }
                    ckInput.CheckedChanged += ckInputShout_CheckedChanged;
                    break;
                case ValueType.PlayerDragonShout:
                    {
                        ckInput.Checked = NPCRecord.NPCDragonShouts.Exists(refId => refId.FormID == ParseRefId(id));
                        ckInput.CheckedChanged += ckInputDragonShout_CheckedChanged;
                    }
                    break;
                    case ValueType.PlayerSpell:
                    {
                        ckInput.Checked = PlayerRecord.Spells.SpellsList.Exists(refId => refId.FormID == ParseRefId(id));
                        ckInput.CheckedChanged += ckInputSpell_CheckedChanged;
                    }
                    break;
                    case ValueType.PlayerActiveEffect:
                    {
                        ckInput.Checked = PlayerRecord.Spells.SpellsList.Exists(refId => refId.FormID == ParseRefId(id));
                        ckInput.CheckedChanged += ckInputActiveEffect_CheckedChanged;
                    }
                    break;
            }

            ckInput.Text = title;

            var node = new Node { HostedItem = ckInput };

            host.Nodes.Add(node);

        }
        private void ckInputPerk_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBoxItem)sender;

            if (checkBox.CheckState == CheckState.Indeterminate || checkBox.Tag == null)
                return;

            var tagId = (int) checkBox.Tag;
            RefID refId;
            if(CreateRefId(tagId, out refId))
            {
                if (checkBox.Checked)
                {
                    if (!PlayerRecord.Perks.PerkTable.Exists(item => item.Id.FormID == refId.FormID))
                    {
                        PlayerRecord.Perks.PerkTable.Add(new PlayerRecord.PlayerPerks.PerkTableEntry { Id = refId, Value = 1 });
                        PlayerRecord.Perks.Perks.Add(refId);
                    }
                }
                else
                {
                    if (PlayerRecord.Perks.PerkTable.Exists(item => item.Id.FormID == refId.FormID))
                    {
                        PlayerRecord.Perks.PerkTable.Remove(
                            PlayerRecord.Perks.PerkTable.Find(item => item.Id.FormID == refId.FormID));
                        PlayerRecord.Perks.Perks.Remove(PlayerRecord.Perks.Perks.Find(item => item.FormID == refId.FormID));
                    }
                }
            }
        }

        private void ckInputShout_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBoxItem)sender;

            if (checkBox.CheckState == CheckState.Indeterminate || checkBox.Tag == null)
                return;

            var tagId = (int) checkBox.Tag;
            RefID refId;
            if (CreateRefId(tagId, out refId))
            {
                if (checkBox.Checked)
                {
                    if (!saveObj.ChangeForms.Exists(changeForm => changeForm.Id.FormID == refId.FormID))
                    {
                        var changeForm = new ChangeForm(ChangeForm.DataType.Int8)
                            {
                                Id = refId,
                                Flags = 0x01,
                                Type = FormType.WOOP,
                                Version = PlayerRecord.Version
                            };
                        changeForm.SetData(new byte[] {0x49, 0x00, 0x01, 0x00, 0x00, 0x00}, false);
                        saveObj.ChangeForms.Add(changeForm);
                    }
                    else
                    {
                        var form = saveObj.ChangeForms.Find(changeForm => changeForm.Id.FormID == refId.FormID);
                        var data = form.GetData();
                        data[0x02] = 0x01;
                        form.SetData(data, false);
                    }
                }
                else
                {
                    if (saveObj.ChangeForms.Exists(changeForm => changeForm.Id.FormID == refId.FormID))
                    {
                        var form = saveObj.ChangeForms.Find(changeForm => changeForm.Id.FormID == refId.FormID);
                        var data = form.GetData();
                        data[0x02] = 0x00;
                        form.SetData(data, false);
                    }
                }
            }
        }

        private void ckInputDragonShout_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBoxItem) sender;

            if (checkBox.CheckState == CheckState.Indeterminate || checkBox.Tag == null)
                return;

            var tagId = (int) checkBox.Tag;
            RefID refId;
            if( CreateRefId(tagId, out refId))
            {
                if (checkBox.Checked)
                {
                    NPCRecord.NPCDragonShouts.Add(refId);
                }
                else
                {
                    NPCRecord.NPCDragonShouts.Remove(refId);
                }
            }
        }

        private void ckInputSpell_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBoxItem)sender;

            if (checkBox.CheckState == CheckState.Indeterminate || checkBox.Tag == null)
                return;

            var tagId = (int)checkBox.Tag;
            RefID refId;
            if(CreateRefId(tagId, out refId))
            {
                if (checkBox.Checked)
                {
                    if(!PlayerRecord.Spells.SpellsList.Contains(refId))
                        PlayerRecord.Spells.AddItem(refId);
                }
                else
                {
                    PlayerRecord.Spells.RemoveItem(refId);
                }
            }
        }

        private void ckInputActiveEffect_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBoxItem)sender;

            if (checkBox.CheckState == CheckState.Indeterminate || checkBox.Tag == null)
                return;

            var tagId = (int)checkBox.Tag;
            RefID refId;
            if (checkBox.Checked && CreateRefId(tagId, out refId))
            {
                PlayerRecord.Effects.Add(refId);
                
                PlayerRecord.Spells.AddItem(refId);
            }
        }
        private void searchBoxSpells_TextChanged(object sender, EventArgs e)
        {
            // Call FindItemWithText with the contents of the textbox.
            ListViewItem foundItem =
                listSpellList.FindItemWithText(txtSpellsSearchBox.Text, false, 0, true);
            if (foundItem != null)
            {
                listSpellList.TopItem = foundItem;
            }
        }

        private void searchBoxInventory_TextChanged(object sender, EventArgs e)
        {
            // Call FindItemWithText with the contents of the textbox.
            ListViewItem foundItem =
                listInventory.FindItemWithText(txtInventorySearchBox.Text, false, 0, true);
            if (foundItem != null)
            {
                listInventory.TopItem = foundItem;

            }
        }
        private void SkillDouble_ValueChanged(object sender, EventArgs e)
        {
            var i = sender as DoubleInput;
            if (i != null)
            {
                PlayerRecord.Skills[(PlayerRecord.PlayerAttribute) (uint)i.Tag] = i.Value;
            }
        }

        private bool CreateRefId(int formId, out RefID refId)
        {
            int pluginIndex = (formId >> 0x18) & 0xFF, pIndex = -1, id = formId & 0x3FFFFF;
            switch (pluginIndex)
            {
                    // Default Skyrim
                case 0:
                    {
                        refId = new RefID { FormType = FormOrigin.Skyrim, FormID = id};
                        return true;
                    }
                    // Dawnguard
                case 2:
                    {
                        pIndex = saveObj.PluginList.FindIndex("Dawnguard.esm");
                    }
                    break;
                case 3:
                    {
                        pIndex = saveObj.PluginList.FindIndex("Hearthfires.esm");
                    }
                    break;
                case 4:
                    {
                        pIndex = saveObj.PluginList.FindIndex("Dragonborn.esm");
                    }
                    break;
                    
            }
            if (pIndex == -1)
            {
                refId = new RefID();
                UI.errorBox("Attempted to add an item from a DLC that is not loaded in the save file.");
                return false;
            }
            if(saveObj.FormIDs.Exists(id, pIndex))
            {
                refId = new RefID
                    {
                        FormType = FormOrigin.Index,
                        FormID = saveObj.FormIDs.FindIndex(id, pIndex)
                    };
            }
            else // add the id if its not there, add it to the table
            {
                refId = new RefID
                    {
                        FormType = FormOrigin.Index,
                        FormID = saveObj.FormIDs.Add(id,pIndex)
                    };
            }
            return true;
        }

        private int ParseRefId(int formId)
        {
            var pluginIndex = (formId >> 0x18) & 0xFF;
            switch (pluginIndex)
            {
                case 0:
                    return formId & 0x3FFFFF;
                default:
                    {
                        if (saveObj.FormIDs.Exists(formId, GetLocalDlcIndex(pluginIndex)))
                            return saveObj.FormIDs.FindIndex(formId, GetLocalDlcIndex(pluginIndex));
                        return 0;
                    }
            }
        }

        private int TransformRefId(RefID id)
        {
            switch (id.FormType)
            {
                case FormOrigin.Skyrim:
                    return id.FormID;
                case FormOrigin.Index:
                    {
                        return saveObj.FormIDs[id.FormID - 1].Id.FormID | (saveObj.FormIDs[id.FormID - 1].PluginIndex << 0x18);
                    }
            }
            throw new Exception("invalid form id not listed.");
        }

        private int GetLocalDlcIndex(int pluginIndex)
        {
            switch (pluginIndex)
            {
                    // Dawnguard
                case 2:
                    {
                        return saveObj.PluginList.FindIndex("Dawnguard.esm");
                    }
                    // Hearthfire
                case 3:
                    {
                        return saveObj.PluginList.FindIndex("Hearthfires.esm");
                    }
                    // Dragonborn
                case 4:
                    {
                        return saveObj.PluginList.FindIndex("Dragonborn.esm");
                    }
            }
            return 0;
        }

        public enum ValueType
        {
            Default,

            PlayerSkill,
            PlayerPerk,
            PlayerShout,
            PlayerDragonShout,
            PlayerSpell,
            PlayerActiveEffect
        }
    }

    public class XmlParser
    {
        public class Perk
        {
            public string PerkName;
            public List<string> Children = new List<string>();
        }

        public class SkillTree
        {
            public List<Perk> Perks = new List<Perk>();
            public string SkillName;
        }

        private List<SkillTree> _valuelist;
        private string _file;

        public List<SkillTree> ValuesList
        {
            get { return _valuelist; }
            set { _valuelist = value; }
        }

        public XmlParser(string file, bool isFile)
        {
            _valuelist = new List<SkillTree>();
            _file = file;

            Parse(isFile);
        }

        private void Parse(bool isFile)
        {
            //Initialize our XmlDocument for parsing
            var xmlDoc = new XmlDocument();
            if (isFile)
            {
                //Load our plugin
                xmlDoc.Load(_file);
            }
            else
            {
                xmlDoc.LoadXml(_file);
            }
            var root = xmlDoc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                _valuelist.Add(ReadNode(node));
            }
        }

        private SkillTree ReadNode(XmlNode xmlNode)
        {
            var skillTree = new SkillTree();
            switch (xmlNode.Name.ToLower())
            {
                case "skill":
                    skillTree.SkillName = xmlNode.Attributes["title"].Value;
                    foreach (XmlNode node in xmlNode.ChildNodes)
                    {
                        var perk = new Perk();
                        perk.PerkName = node.Attributes["desc"].Value;
                        //perk.EditorName = node.Attributes["name"].Value;
                        foreach (XmlNode children in node.ChildNodes)
                        {
                            perk.Children.Add(children.Attributes["name"].Value);
                        }
                        skillTree.Perks.Add(perk);
                    }
                    break;
            }
            return skillTree;
        }
    }
}