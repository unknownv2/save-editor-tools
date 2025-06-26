using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gears2;
using Horizon.Functions;

namespace Horizon.PackageEditors.Gears_of_War_2
{
    public partial class GearsOfWar2 : EditorControl
    {
        //public static readonly string FID = "4D53082D";
        private Save save;
        private string[] weaponsArray = new string[]
        {
            "Assault Rifle",
            "COG Pistol",
            "Frag Grenade",
            "Smoke Grenade",
            "Shotgun",
            "Sniper Rifle",
            "Locust Assault Rifle",
            "Locust Pistol",
            "Locust Burst Pistol Base"
        };

        public GearsOfWar2()
        {
            InitializeComponent();
            TitleID = FormID.GearsOfWar2;
            
            foreach (string weap in weaponsArray)
            {
                comboBoxEx1.Items.Add(weap);
                comboBoxEx2.Items.Add(weap);
                comboBoxEx3.Items.Add(weap);
                comboBoxEx4.Items.Add(weap);
            }
        }

        private int getSelectedIndex(string realWeaponName)
        {
            for (int x = 0; x < weaponsArray.Length; x++)
                if (makeRealWeaponName(weaponsArray[x]) == realWeaponName)
                    return x;
            return 0;
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("Gears2Checkpoint0.sav"))
            {
                UI.errorBox("This editor only supports checkpoint save games!");
                return false;
            }

            save = new Save();
            save.loadSave(ref IO);

            if (save.weapons.Length >= 1)
            {
                comboBoxEx1.Enabled = true;
                comboBoxEx1.SelectedIndex = getSelectedIndex(save.weapons[0].name);

                integerInput1.Enabled = true;
                integerInput1.Value = save.weapons[0].ammo;

                if (save.weapons.Length >= 2)
                {
                    comboBoxEx2.Enabled = true;
                    comboBoxEx2.SelectedIndex = getSelectedIndex(save.weapons[1].name);

                    integerInput2.Enabled = true;
                    integerInput2.Value = save.weapons[1].ammo;

                    if (save.weapons.Length >= 3)
                    {
                        comboBoxEx3.Enabled = true;
                        comboBoxEx3.SelectedIndex = getSelectedIndex(save.weapons[2].name);

                        integerInput3.Enabled = true;
                        integerInput3.Value = save.weapons[2].ammo;

                        if (save.weapons.Length >= 4)
                        {
                            comboBoxEx4.Enabled = true;
                            comboBoxEx4.SelectedIndex = getSelectedIndex(save.weapons[3].name);

                            integerInput4.Enabled = true;
                            integerInput4.Value = save.weapons[3].ammo;
                        }
                    }
                }
            }

            return true;
        }

        private string makeRealWeaponName(string newName)
        {
            return pre + newName.Replace(" ", String.Empty);
        }

        private string pre = "GearGame.GearWeap_";
        public override void Save()
        {
            if (save.weapons.Length >= 1)
            {
                save.weapons[0].name = makeRealWeaponName(comboBoxEx1.SelectedItem.ToString());
                save.weapons[0].nameLen = save.weapons[0].name.Length + 1;
                if (save.weapons.Length >= 2)
                {
                    save.weapons[1].name = makeRealWeaponName(comboBoxEx2.SelectedItem.ToString());
                    save.weapons[1].nameLen = save.weapons[1].name.Length + 1;
                    if (save.weapons.Length >= 3)
                    {
                        save.weapons[2].name = makeRealWeaponName(comboBoxEx3.SelectedItem.ToString());
                        save.weapons[2].nameLen = save.weapons[2].name.Length + 1;
                        if (save.weapons.Length >= 4)
                        {
                            save.weapons[3].name = makeRealWeaponName(comboBoxEx4.SelectedItem.ToString());
                            save.weapons[3].nameLen = save.weapons[3].name.Length + 1;
                        }
                    }
                }
            }
            //EndianIO IOx = save.writeSave();
            //this.IO.Stream.SetLength(IOx.Stream.Length);
            //this.IO.Out.Write(IOx.In.ReadBytes(IO.Stream.Length));
            save.writeSave(ref this.IO);
        }

        private void integerInput1_ValueChanged(object sender, EventArgs e)
        {
            save.weapons[0].ammo = integerInput1.Value;
        }

        private void integerInput2_ValueChanged(object sender, EventArgs e)
        {
            save.weapons[1].ammo = integerInput2.Value;
        }

        private void integerInput3_ValueChanged(object sender, EventArgs e)
        {
            save.weapons[2].ammo = integerInput3.Value;
        }

        private void integerInput4_ValueChanged(object sender, EventArgs e)
        {
            save.weapons[3].ammo = integerInput4.Value;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            integerInput1.Value = integerInput1.MaxValue;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            integerInput2.Value = integerInput2.MaxValue;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            integerInput3.Value = integerInput3.MaxValue;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            integerInput4.Value = integerInput4.MaxValue;
        }
    }
}
