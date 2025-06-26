using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horizon.Functions;

namespace Horizon.PackageEditors.Gears_of_War
{
    public partial class GearsOfWar : EditorControl
    {
        //public static readonly string FID = "4D5307D5";
        public GearsOfWar()
        {
            InitializeComponent();
            TitleID = FormID.GearsOfWar;
            
        }

        private struct weapon
        {
            public int strLen;
            public string name;
            public int ammo;
            public int clip;
            public int offset;
        }

        private weapon[] weapons;
        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
                return false;

            IO.SeekTo(8);
            int nextStrLen = IO.In.ReadInt32();
            string nextStr = IO.In.ReadAsciiString(nextStrLen);
            weapons = new weapon[0];
            while (IO.Stream.Position < IO.Stream.Length)
            {
                nextStrLen = IO.In.ReadInt32();
                nextStr = IO.In.ReadAsciiString(nextStrLen);
                if (nextStr.Length > 16)
                {
                    if (nextStr.Substring(0, 16) == "WarfareGame.Weap")
                    {
                        Array.Resize(ref weapons, weapons.Length + 1);
                        weapons[weapons.Length - 1].name = nextStr;
                        weapons[weapons.Length - 1].strLen = nextStrLen;

                        weapons[weapons.Length - 1].offset = (int)IO.Stream.Position - nextStrLen - 4;
                        weapons[weapons.Length - 1].ammo = IO.In.ReadInt32();
                        weapons[weapons.Length - 1].clip = IO.In.ReadInt32();
                    }
                }
            }
            foreach (weapon weap in weapons)
                comboBoxEx1.Items.Add(weap.name.ToString().Replace("WarfareGame.Weap_", String.Empty));

            if (comboBoxEx1.Items.Count == 0)
            {
                UI.errorBox("No weapons were found in this save!");
                return false;
            }

            comboBoxEx1.SelectedIndex = 0;
            return true;
        }

        public override void Save()
        {
            foreach (weapon weap in weapons)
            {
                IO.SeekTo(weap.offset);
                IO.Out.Write(weap.strLen);
                IO.Out.WriteAsciiString(weap.name, weap.strLen);
                IO.Out.Write(weap.ammo);
                IO.Out.Write(weap.clip);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            integerInput1.Value = weapons[comboBoxEx1.SelectedIndex].ammo;
        }

        private void integerInput1_ValueChanged(object sender, EventArgs e)
        {
            weapons[comboBoxEx1.SelectedIndex].ammo = (int)integerInput1.Value;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            integerInput1.Value = integerInput1.MaxValue;
        }
    }
}
