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

namespace Horizon.PackageEditors.Saint_s_Row_GOOH
{
    public partial class SaintsRowGOOH : EditorControl
    {
        private SRGOOHGameSave _gameSave;
        // number of allowed weapon slots
        private const int MaxWeaponSlotCount = 7;
        public SaintsRowGOOH()
        {
            InitializeComponent();
            TitleID = FormID.SaintsRowGOOH;
            InitData();
        }

        private void InitData()
        {
            clmnCmbWeapons.Items.Clear();

            if (clmnCmbWeapons.Items.Count == 0)
            {
                clmnCmbWeapons.Items.AddRange(GetWeaponList().ToArray());
                //clmnCmbWeapons.Items.AddRange(GetWeaponList().ToArray());
            }

            if (cmbWrathLvl.Items.Count != 0) return;

            for (int x = 1; x < 51; x++)
            {
                cmbWrathLvl.Items.Add(x.ToString(CultureInfo.InvariantCulture));
            }
        }
        public override bool Entry()
        {
            if (!OpenStfsFile("game.dat"))
                return false;

            _gameSave = new SRGOOHGameSave(IO);
            LoadSave();
            return true;
        }

        public override void Save()
        {
            //make sure any canges are committed
            dtgUpgrades.EndEdit();
            dtgWeaponsList.EndEdit();

            // write weapon list
            var weapons = new SRIVInventory();
            weapons.AddRange(from DataGridViewRow weaponRow in dtgWeaponsList.Rows
                where !string.IsNullOrEmpty((string) weaponRow.Cells[0].Value)
                select new WeaponEntry
                {
                    WeaponIdent = GetWeaponIdentFromName(Convert.ToString(weaponRow.Cells[0].Value)), Weapon1AmmoClip = Convert.ToUInt32(weaponRow.Cells[1].Value),
                    Weapon2AmmoClip = Convert.ToUInt32(weaponRow.Cells[2].Value), WeaponAmmoBarrel = Convert.ToUInt32(weaponRow.Cells[3].Value),
                    HasInfiniteAmmo = Convert.ToBoolean(weaponRow.Cells[4].Value), IsDualWieldable = Convert.ToBoolean(weaponRow.Cells[5].Value)
                });
            
            // set new weapon list
            _gameSave.Inventory = weapons;

            // edit the upgrades available table
            int x = 0;
            foreach (DataGridViewRow row in dtgUpgrades.Rows)
            {
                _gameSave.UpgradeData.TableAvailable[_gameSave.UpgradeData.GameUpgrades[x++].Ident] = Convert.ToBoolean(row.Cells[2].Value);
            }
            x = 0;
            // edit the upgrades unlocked table
            foreach (DataGridViewRow row in dtgUpgrades.Rows)
            {
                _gameSave.UpgradeData.TableUnlocked[_gameSave.UpgradeData.GameUpgrades[x++].Ident] = Convert.ToBoolean(row.Cells[3].Value);
            }

            // set player stats
            _gameSave.Money = intMoney.Value;
            _gameSave.TotalExperience = intTotalXP.Value;
            _gameSave.SoulClusters = intClusters.Value;
            _gameSave.WrathLevel = cmbWrathLvl.SelectedIndex;

            // push save edits
            _gameSave.Save();
        }

        private void LoadSave()
        {
            // clear all trees and gridviews
            dtgWeaponsList.Rows.Clear();
            advTreePlayer.Nodes.Clear();
            dtgUpgrades.Rows.Clear();
            advTreeMultipliers.Nodes.Clear();

            // display player stats
            intMoney.Value = _gameSave.Money;
            intTotalXP.Value = _gameSave.TotalExperience;
            intClusters.Value = _gameSave.SoulClusters;
            cmbWrathLvl.SelectedIndex = _gameSave.WrathLevel;

            var powers = new Node("Powers");
            foreach (
                var power in
                    Enum.GetValues(typeof(SRGOOHPowersEnum))
                        .Cast<SRGOOHPowersEnum>())
            {
                InsertPowersBoolNode(powers, Regex.Replace(power.ToString(), "([a-z])([A-Z])", "$1 $2"), power);
            }
            advTreePlayer.Nodes.Add(powers);

            // load weapon list
            foreach (var inventoryItem in _gameSave.Inventory)
            {
                string name;
                try
                {
                    name = SrGOOHSaveData.InventoryList.Find(t => t.Hash == inventoryItem.WeaponIdent).DisplayName;
                }
                catch
                {
                    name = "UNKNOWN WEAPON";
                }
                dtgWeaponsList.Rows.Add(new object[]
                    {
                        name,
                        inventoryItem.Weapon1AmmoClip,
                        inventoryItem.Weapon2AmmoClip,
                        inventoryItem.WeaponAmmoBarrel,
                        inventoryItem.HasInfiniteAmmo,
                        inventoryItem.IsDualWieldable,
                    });
            }
            if (dtgWeaponsList.RowCount > MaxWeaponSlotCount)
                dtgWeaponsList.AllowUserToAddRows = false;

            for (int i = 0; i < _gameSave.UpgradeData.GameUpgrades.Length; i++)
            {
                uint ident = _gameSave.UpgradeData.GameUpgrades[i].Ident;
                var unlocked = _gameSave.UpgradeData.TableUnlocked[ident];
                var available = _gameSave.UpgradeData.TableAvailable[ident];
                var unlockablesEntry = SrGOOHSaveData.Unlockables.Find(u => u.Hash == ident);
                dtgUpgrades.Rows.Add(new object[]
                    {
                        unlockablesEntry.DisplayName,
                        unlockablesEntry.Description,
                        available,
                        unlocked
                    });
            }

            foreach (var multiplier in _gameSave.GlobalMultipliers.Values.Keys)
            {
                if (multiplier != SRGOOHMultipliersTypes.HourlyCacheTransferRate)
                    advTreeMultipliers.Nodes.Add(CreateMultipliersDoubleNode(multiplier, float.MaxValue));
            }
            
            advTreePlayer.ExpandAll();
        }

        private Node CreateMultipliersDoubleNode(SRGOOHMultipliersTypes type, double maxValue)
        {
            var doubleInput = new DoubleInput
            {
                Tag = type,
                Value = _gameSave.GlobalMultipliers.Values[type],
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true
            };

            doubleInput.ValueChanged += gblMultiplier_DoubleValueChanged;
            var node = new Node(Regex.Replace(type.ToString(), "([a-z])([A-Z])",
                "$1 $2").Replace("_", " "));
            node.Cells.Add(new Cell {HostedControl = doubleInput});

            return node;
        }
        private void gblMultiplier_DoubleValueChanged(object sender, EventArgs e)
        {
            var dblInput = sender as DoubleInput;
            if (dblInput != null)
            {
                _gameSave.GlobalMultipliers.Values[(SRGOOHMultipliersTypes)dblInput.Tag] = (float)dblInput.Value;
            }
        }

        private void ckPowerUnlock_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxItem = (sender as CheckBoxItem);
            if (checkBoxItem == null) 
                return;

            var id = (SRGOOHPowersEnum)checkBoxItem.Tag;
            _gameSave.Powers[id] = checkBoxItem.Checked;
        }
        private void InsertPowersBoolNode(Node host, string title, SRGOOHPowersEnum id)
        {
            var ckInput = new CheckBoxItem { Tag = id, Checked = _gameSave.Powers[id], Text = title };
            ckInput.CheckedChanged += ckPowerUnlock_CheckedChanged;
            host.Nodes.Add(new Node { HostedItem = ckInput });
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
        private List<string> GetWeaponList()
        {
            return SrGOOHSaveData.InventoryList.FindAll(t => t.DisplayName != string.Empty).Select(item => item.DisplayName).ToList();
        }

        private uint GetWeaponIdentFromName(string name)
        {
            foreach (var inventoryItem in SrGOOHSaveData.InventoryList)
            {
                if (inventoryItem.DisplayName == name)
                    return inventoryItem.Hash;
            }
            throw new Exception(string.Format("invalid weapon name requested {0}", name));
        }

        private void dtgWeaponsList_RowCountChanged(object sender, DataGridViewRowEventArgs e)
        {
            CheckRowCount();
        }
        /// <summary>
        /// Make sure the user cannot create more rows than the maximum weapon count
        /// </summary>
        private void CheckRowCount()
        {
            if (dtgWeaponsList.Rows != null && dtgWeaponsList.Rows.Count > MaxWeaponSlotCount)
            {
                dtgWeaponsList.AllowUserToAddRows = false;
            }
            else if (!dtgWeaponsList.AllowUserToAddRows)
            {
                dtgWeaponsList.AllowUserToAddRows = true;
            }
        }

        private void cmdMaxWrath_Click(object sender, EventArgs e)
        {
            intTotalXP.Value = intTotalXP.MaxValue;
            cmbWrathLvl.SelectedIndex = cmbWrathLvl.Items.Count - 1;
        }
        private void cmdMaxMoney_Click(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
        }
        private void cmdMaxSoulClusters_Click(object sender, EventArgs e)
        {
            intClusters.Value = intClusters.MaxValue;
        }
    }
}
