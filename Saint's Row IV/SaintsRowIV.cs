using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using Volition;

namespace Horizon.PackageEditors.Saint_s_Row_IV
{
    public partial class SaintsRowIV : EditorControl
    {
        private SRIVGameSave gameSave;

        // number of allowed weapon slots
        private const int MaxWeaponSlotCount = 7;

        public SaintsRowIV()
        {
            TitleID = FormID.SaintsRow4;
            InitializeComponent();
            InitData();
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("game.dat"))
                return false;

            gameSave = new SRIVGameSave(IO);

            LoadSave();

            return true;
        }

        public override void Save()
        {
            //make sure any canges are committed
            dtgUpgrades.EndEdit();
            dgvCheats.EndEdit();
            dtgWeaponsList.EndEdit();

            // write weapon list
            var weapons = new SRIVInventory();
            weapons.AddRange(from DataGridViewRow weaponRow in dtgWeaponsList.Rows
                             where !string.IsNullOrEmpty((string)weaponRow.Cells[0].Value)
                             select new WeaponEntry
                             {
                                 WeaponIdent = GetWeaponIdentFromName(Convert.ToString(weaponRow.Cells[0].Value)),
                                 Weapon1AmmoClip = Convert.ToUInt32(weaponRow.Cells[1].Value),
                                 Weapon2AmmoClip = Convert.ToUInt32(weaponRow.Cells[2].Value),
                                 WeaponAmmoBarrel = Convert.ToUInt32(weaponRow.Cells[3].Value),
                                 HasInfiniteAmmo = Convert.ToBoolean(weaponRow.Cells[4].Value),
                                 IsDualWieldable = Convert.ToBoolean(weaponRow.Cells[5].Value)
                             });

            // save cheat list
            foreach (DataGridViewRow cheatRow in dgvCheats.Rows)
            {
                if (!Convert.ToBoolean(cheatRow.Cells[2].Value)) continue;

                var hash = (uint) cheatRow.Tag;
                if(!gameSave.Cheats.Contains(hash))
                    gameSave.Cheats.Add(hash);
            }
            // set new weapon list
            gameSave.Inventory = weapons;

            // set player stats
            gameSave.Money = intMoney.Value;
            gameSave.LevelUpRespectRequired = intNextLevelXp.Value;
            gameSave.TotalRespectEarned = intTotalXp.Value;
            gameSave.DataClusters = intDataClusters.Value;
            gameSave.RespectLevel = cmbRespectLvl.SelectedIndex;
            
            // edit the upgrades available table
            int x = 0;
            foreach (DataGridViewRow row in dtgUpgrades.Rows)
            {
                gameSave.UpgradeData.TableAvailable[gameSave.UpgradeData.GameUpgrades[x++].Ident] = Convert.ToBoolean(row.Cells[2].Value);
            }
            x = 0;
            // edit the upgrades unlocked table
            foreach (DataGridViewRow row in dtgUpgrades.Rows)
            {
                gameSave.UpgradeData.TableUnlocked[gameSave.UpgradeData.GameUpgrades[x++].Ident] = Convert.ToBoolean(row.Cells[3].Value);
            }

            gameSave.CheatsEnabled = chkAreCheatsEnabled.Checked;
            gameSave.Save();
        }

        private void InitData()
        {
            if (clmnCmbWeapons.Items.Count == 0)
                clmnCmbWeapons.Items.AddRange(GetWeaponList().ToArray());

            if (cmbRespectLvl.Items.Count != 0) return;

            for (int x = 1; x < 51; x++)
            {
                cmbRespectLvl.Items.Add(x.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void LoadSave()
        {
            // clear all trees and gridviews
            dtgWeaponsList.Rows.Clear();
            dgvCheats.Rows.Clear();
            advTreePlayer.Nodes.Clear();
            dtgUpgrades.Rows.Clear();
            advTreeMultipliers.Nodes.Clear();

            // load weapon list
            foreach (var inventoryItem in gameSave.Inventory)
            {
                if(inventoryItem.Weapon1AmmoClip + inventoryItem.Weapon2AmmoClip + inventoryItem.WeaponAmmoBarrel  == 0)
                    continue;

                dtgWeaponsList.Rows.Add(new object[]
                    {
                        SRIVSaveData.InventoryItems[inventoryItem.WeaponIdent].DisplayName,
                        inventoryItem.Weapon1AmmoClip,
                        inventoryItem.Weapon2AmmoClip,
                        inventoryItem.WeaponAmmoBarrel,
                        inventoryItem.HasInfiniteAmmo,
                        inventoryItem.IsDualWieldable,
                    });
            }

            if (dtgWeaponsList.RowCount > MaxWeaponSlotCount)
                dtgWeaponsList.AllowUserToAddRows = false;

            chkAreCheatsEnabled.Checked = gameSave.CheatsEnabled;

            intMoney.Value = gameSave.Money;
            intNextLevelXp.Value = gameSave.LevelUpRespectRequired;
            intTotalXp.Value = gameSave.TotalRespectEarned;
            intDataClusters.Value = gameSave.DataClusters;
            cmbRespectLvl.SelectedIndex = gameSave.RespectLevel;

            foreach (XboxTableDefinition cheat in SRIVSaveData.Cheats)
            {
                //SRIVSaveData.XboxTableDefinition cheatDefinition = SRIVSaveData.Cheats.Find(t => t.Hash == cheat);
                dgvCheats.Rows.Add(new object[]
                    {
                        cheat.DisplayName,
                        cheat.Description,
                        gameSave.Cheats.Contains(cheat.Hash)
                    });
                dgvCheats.Rows[dgvCheats.Rows.Count - 1].Tag = cheat.Hash;
            }

            Node powers = new Node("Powers");
            foreach (
                var power in
                    Enum.GetValues(typeof (SRIVPowersEnum))
                        .Cast<SRIVPowersEnum>())
            {
                InsertPowersBoolNode(powers, Regex.Replace(power.ToString(), "([a-z])([A-Z])", "$1 $2"), power);
            }
            advTreePlayer.Nodes.Add(powers);

            for (int i = 0; i < gameSave.UpgradeData.GameUpgrades.Length; i++)
            {
                uint ident = gameSave.UpgradeData.GameUpgrades[i].Ident;
                var unlocked = gameSave.UpgradeData.TableUnlocked[ident];
                var available = gameSave.UpgradeData.TableAvailable[ident];
                dtgUpgrades.Rows.Add(new object[]
                    {
                        SRIVSaveData.UnlockablesNames[ident],
                        SRIVSaveData.UnlockablesDescriptions[ident],
                        available,
                        unlocked
                    });
            }

            foreach (
                var multiplier in
                    Enum.GetValues(typeof (MultipliersTypes))
                        .Cast<MultipliersTypes>())
            {
                if (multiplier != MultipliersTypes.HourlyCacheTransferRate)
                    advTreeMultipliers.Nodes.Add(CreateMultipliersDoubleNode(multiplier, float.MaxValue));
            }

            advTreeMultipliers.Nodes.Add(CreatePowersIntegerNode(MultipliersTypes.HourlyCacheTransferRate,
                                                                 (ushort)
                                                                 gameSave.GlobalMultipliers.Values[
                                                                     MultipliersTypes.HourlyCacheTransferRate],
                                                                 ushort.MaxValue));


            advTreePlayer.ExpandAll();
        }
        private List<string> GetWeaponList()
        {
            var weaponList = new List<string>();

            foreach (var item in SRIVSaveData.InventoryItems)
            {
                var inv = item.Value;
                switch (inv.Category)
                {
                        case SRIVTableData.InventoryItemCategory.Explosive:
                        case SRIVTableData.InventoryItemCategory.Melee:
                        case SRIVTableData.InventoryItemCategory.Pistol:
                        case SRIVTableData.InventoryItemCategory.Rifle:
                        case SRIVTableData.InventoryItemCategory.SMG:
                        case SRIVTableData.InventoryItemCategory.Shotgun:
                        case SRIVTableData.InventoryItemCategory.Special:
                        case SRIVTableData.InventoryItemCategory.Grenade:
                        case SRIVTableData.InventoryItemCategory.WieldableProps:
                            if(!string.IsNullOrEmpty(inv.DisplayName))
                                weaponList.Add(inv.DisplayName);
                        break;
                }
            }
            return weaponList;
        }
        private uint GetWeaponIdentFromName(string name)
        {
            foreach (var inventoryItem in SRIVSaveData.InventoryItems)
            {
                if (inventoryItem.Value.DisplayName == name)
                    return inventoryItem.Key;
            }
            throw new Exception(string.Format("invalid weapon name requested {0}", name));
        }

        private void BtnClick_MakeAllUpgradesAvailable(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtgUpgrades.Rows)
            {
                row.Cells[2].Value = true;
            }
        }
        private void BtnClick_UnlockAllUpgrades(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtgUpgrades.Rows)
            {
                row.Cells[3].Value = true;
            }
        }

        /// <summary>
        /// Make sure the user cannot create more rows than the maximum weapon count
        /// </summary>
        private void CheckRowCount()
        {
            if (dtgWeaponsList.Rows != null && dtgWeaponsList.RowCount > MaxWeaponSlotCount)
            {
                dtgWeaponsList.AllowUserToAddRows = false;
            }
            else if (!dtgWeaponsList.AllowUserToAddRows)
            {
                dtgWeaponsList.AllowUserToAddRows = true;
            }
        }


        private Node CreatePowersIntegerNode( MultipliersTypes type, int value, int maxValue)
        {
            var intInput = new IntegerInput { Tag = type, MinValue = 0, MaxValue = maxValue, Value = value };
            intInput.ValueChanged += gblMultiplier_IntValueChanged;

            var node = new Node(Regex.Replace(type.ToString(), "([a-z])([A-Z])",
                                                      "$1 $2"));
            
            node.Cells.Add(new Cell { HostedControl = intInput });
            return node;
        }

        private void InsertPowersBoolNode(Node host, string title, SRIVPowersEnum id)
        {
            var ckInput = new CheckBoxItem {Tag = id, Checked = gameSave.Powers[id], Text = title};
            ckInput.CheckedChanged += ckPowerUnlock_CheckedChanged;
            host.Nodes.Add(new Node {HostedItem = ckInput});
        }

        private Node CreateMultipliersDoubleNode(MultipliersTypes type, double maxValue)
        {
            var doubleInput = new DoubleInput
                {
                    Tag = type,
                    Value = gameSave.GlobalMultipliers.Values[type],
                    MinValue = 0,
                    MaxValue = maxValue,
                    ShowUpDown = true
                };

            doubleInput.ValueChanged += gblMultiplier_DoubleValueChanged;
            var node = new Node(Regex.Replace(type.ToString(), "([a-z])([A-Z])",
                                                      "$1 $2"));
            node.Cells.Add(new Cell { HostedControl = doubleInput });
 
            return node;
        }
        private void ckPowerUnlock_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxItem = (sender as CheckBoxItem);
            if (checkBoxItem != null)
            {
                var id = (SRIVPowersEnum)checkBoxItem.Tag;
                gameSave.Powers[id] = checkBoxItem.Checked;
            }
        }

        private void gblMultiplier_DoubleValueChanged(object sender, EventArgs e)
        {
            var dblInput = sender as DoubleInput;
            if (dblInput != null)
            {
                gameSave.GlobalMultipliers.Values[(MultipliersTypes)dblInput.Tag] = (float)dblInput.Value;
            }
        }
        private void gblMultiplier_IntValueChanged(object sender, EventArgs e)
        {
            var intInput = sender as IntegerInput;
            if (intInput != null)
            {
                gameSave.GlobalMultipliers.Values[(MultipliersTypes) intInput.Tag] = intInput.Value;
            }
        }

        private void ChkB_UnlockAllCheats(object sender, EventArgs e)
        {
            var i = sender as CheckBoxX;
            if (i == null || !i.Checked) return;

            foreach (DataGridViewRow row in dgvCheats.Rows)
            {
                row.Cells[2].Value = true;
            }
        }

        private void cmdMaxRespect_Click(object sender, EventArgs e)
        {
            intTotalXp.Value = intTotalXp.MaxValue;
        }

        private void cmdMaxMoney_Click(object sender, EventArgs e)
        {
            intMoney.Value = intTotalXp.MaxValue;
        }
        private void cmdMaxDataClusters_Click(object sender, EventArgs e)
        {
            intDataClusters.Value = intTotalXp.MaxValue;
        }

        private void dtgWeaponsList_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            CheckRowCount();
        }
    }

    public enum ValueType
    {
        Weapon,
        PlayerStat
    }
}