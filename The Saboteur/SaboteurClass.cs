using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.The_Saboteur
{
    namespace Save
    {
        public class Saboteur
        {
            public EndianIO IO { get; set; }
            private bool ulp = false;
            //save data
            public int Contraband { get; set; }
            public bool UnlockPerks { get { return ulp; } set { ulp = value; } }
            public SaboteurSave_1 Save1 {get; set;}
            public List<AmmoEntry> Weapons;
            public Saboteur(EndianIO saveIO)
            {
                saveIO.Open();

                this.IO = saveIO;
                //begin save reading
                this.Read();

                this.LoadValues();

            }

            /// <summary>
            /// Setup public properties.
            /// </summary>
            private void LoadValues()
            {
                this.Contraband = Save1.sab2.Contraband;
            }
            /// <summary>
            /// Initiate save reading.
            /// </summary>
            private void Read()
            {
                Save1 = new SaboteurSave_1(this.IO.In);
                RetrieveNamesFromAmmoIds();
            }
            /// <summary>
            /// Save current buffer with corrected checksum
            /// </summary>
            /// <returns></returns>
            public byte[] Save()
            {
                this.IO.Out.SeekTo(Save1.AmmoLocation);
                for (var x = 0; x < Save1.Ammunition.Length/2; x++)
                {
                    this.IO.Out.BaseStream.Position += 4;
                    this.IO.Out.Write(Save1.Ammunition[x, 1]);
                }
                this.IO.Out.SeekTo(Save1.sab2.ContraBandLocation);
                this.IO.Out.Write(this.Contraband);
                //fix security
                int sum = this.Checksum(); //get checksum

                this.IO.Out.SeekTo(0x04);
                this.IO.Out.Write(sum); //write checksum

                //return for writing
                return this.IO.ToArray();
            }
            /// <summary>
            /// Fixes the internal save's checksum.
            /// </summary>
            /// <returns></returns>
            private int Checksum()
            {
                this.IO.In.SeekTo(0x08);
                int sum = 0;
                for (int x = new int(); x < 0x00026798; x++)
                {
                    sum += (int)this.IO.In.ReadSByte(); // specifically signed, regular byte won't work
                }
                return sum;
            }
            /// <summary>
            /// Convert ammo ids to ammo names
            /// </summary>
            private void RetrieveNamesFromAmmoIds()
            {
                this.Weapons = new List<AmmoEntry>();
                for (var x = 0; x < this.Save1.Ammunition.Length/2; x++)
                {
                    AmmoEntry Entry = new AmmoEntry();
                    Entry.AmmoId = this.Save1.Ammunition[x, 0];
                    switch (this.Save1.Ammunition[x, 0])
                    {
                        case 0xFE8AA46C:
                            Entry.AmmoCategoryName = "Dynamite";
                            break;
                        case 0xC03C335E:
                            Entry.AmmoCategoryName = "Silenced Pistol";
                            break;
                        case 0x476E9B4E:
                            Entry.AmmoCategoryName = "Pistol";
                            break;
                        case 0xA61BB508:
                            Entry.AmmoCategoryName = "Machine Gun";
                            break;
                        case 0x4BE67C8B:
                            Entry.AmmoCategoryName = "Rifle";
                            break;
                        case 0xDB1BC08F:
                            Entry.AmmoCategoryName = "Shotgun";
                            break;
                        //case 0xB5EECEA7:
                            //Entry.WeaponName = "Unknown";
                            //break;
                    }
                    this.Weapons.Add(Entry);
                }
            }
            public string GetAmmoName(uint AmmoId)
            {
                for (var x = 0; x < this.Weapons.Count; x++)
                    if (this.Weapons[x].AmmoId == AmmoId)
                        return this.Weapons[x].AmmoCategoryName;

                return null;
            }
        }
        public struct AmmoEntry
        {
            public string AmmoCategoryName;
            public uint AmmoId;
        }
        public class SaboteurSave_1
        {
            public SaboteurSave_2 sab2;
            int ctr = 0;
            private EndianReader reader;
            int Magic; // 0x53563031
            int Checksum, a;
            float float1;
            int b;
            byte[] c; // 0x62 - > two bytes each entry =  0x31
            int d, dz;
            byte g, h;
            int i, j;
            byte[] k; // length = j << 3
            int entry_count_type4; //for sab_entry_type4
            int a1, b1, c1;
            byte[] d1, e1; // 0x40
            int f1, g1, h1;
            int[] j1;
            int k1;
            byte[] l1;
            int m1, n1;
            int[,] o1;
            int p1;
            public uint[,] Ammunition; // {1} = id, {2} = ammo
            int r1;
            int[,] s1;
            int entry_count_type5;
            int t1, u1, v1;
            string[] w1;
            int x1;
            public long AmmoLocation;
            public SaboteurSave_1(EndianReader reader)
            {
                this.reader = reader;

                this.Read();
            }
            private void Read()
            {
                Magic = reader.ReadInt32(); Checksum = reader.ReadInt32(); a = reader.ReadInt32();
                float1 = reader.ReadSingle();
                b = reader.ReadInt32();
                c = reader.ReadBytes(0x62);
                d = reader.ReadInt32(); dz = reader.ReadInt32();
                for (ctr = 0; ctr < dz; ctr++)
                {
                    sab_entry_type6 ent;
                    ent.a = reader.ReadByte();
                    if (ent.a == 1)
                    {
                        ent.c = reader.ReadInt32();
                        ent.d = reader.ReadSingle();
                    }
                    ent.b = reader.ReadBytes(0x40);
                }

                g = reader.ReadByte(); h = reader.ReadByte();
                for (ctr = 0; ctr < h; ctr++)
                {
                    sab_entry_type7 ent = new sab_entry_type7();
                    ent.a = reader.ReadInt32();
                    ent.b = reader.ReadByte();
                    ent.c = reader.ReadInt32(); ent.d = reader.ReadInt32();
                }
                i = reader.ReadInt32(); j = reader.ReadInt32();
                k = reader.ReadBytes(j << 3);
                sab_entry_type4 up = new sab_entry_type4();
                up.a = reader.ReadInt32(); up.b = reader.ReadInt32(); up.c = reader.ReadInt32();
                up.d = reader.ReadInt32();
                up.e = reader.ReadBytes(0x10); up.f = reader.ReadBytes(0x10);
                up.g = new int[4];
                for (int op = 0; op < 4; op++)
                {
                    up.g[op] = reader.ReadInt32();
                }
                up.h = reader.ReadInt32();
                if (up.h != 0)
                {
                    up.i = new int[up.h];
                    for (int sp = 0; sp < up.h; sp++)
                    {
                        up.i[sp] = reader.ReadInt32();
                    }
                }
                up.j = reader.ReadByte(); up.k = reader.ReadByte(); up.l = reader.ReadByte(); up.m = reader.ReadByte();
                entry_count_type4 = reader.ReadInt32();
                for (int tT = 0; tT < entry_count_type4; tT++)
                {
                    sab_entry_type4 ent = new sab_entry_type4();
                    int count = reader.ReadInt32(); reader.ReadInt32();
                    for (int gG = 0; gG < count; gG++)
                    {
                        reader.ReadInt32();
                        ent.a = reader.ReadInt32(); ent.b = reader.ReadInt32(); ent.c = reader.ReadInt32();
                        ent.d = reader.ReadInt32();
                        ent.e = reader.ReadBytes(0x10); ent.f = reader.ReadBytes(0x10);
                        ent.g = new int[4];
                        for (int op = 0; op < 4; op++)
                        {
                            ent.g[op] = reader.ReadInt32();
                        }
                        ent.h = reader.ReadInt32();
                        if (ent.h != 0)
                        {
                            ent.i = new int[ent.h];
                            for (int sp = 0; sp < ent.h; sp++)
                            {
                                ent.i[sp] = reader.ReadInt32();
                            }
                        }
                        ent.j = reader.ReadByte(); ent.k = reader.ReadByte(); ent.l = reader.ReadByte(); ent.m = reader.ReadByte();
                    }
                }
                a1 = reader.ReadInt32(); b1 = reader.ReadInt32(); c1 = reader.ReadInt32();
                d1 = reader.ReadBytes(0x40); e1 = reader.ReadBytes(0x40);
                f1 = reader.ReadInt32(); g1 = reader.ReadInt32();
                if (g1 != 0)
                {
                    for (int shit = 0; shit < 6; shit++)
                    {
                        reader.ReadInt32();
                    }
                    reader.ReadBytes(0x30);
                }
                h1 = reader.ReadInt32();
                j1 = new int[h1];
                for (int xz = 0; xz < h1; xz++)
                {
                    j1[xz] = reader.ReadInt32();
                }
                k1 = reader.ReadInt32();
                l1 = new byte[k1];
                for (int xz = 0; xz < k1; xz++)
                {
                    l1[xz] = reader.ReadByte();
                }
                m1 = reader.ReadInt32(); n1 = reader.ReadInt32();
                o1 = new int[n1, 4];
                for (int bb = 0; bb < n1; bb++)
                {
                    o1[bb, 0] = reader.ReadInt32();
                    o1[bb, 1] = reader.ReadInt32();
                    o1[bb, 2] = reader.ReadInt32();
                    o1[bb, 3] = reader.ReadInt32();
                }
                p1 = reader.ReadInt32();
                this.AmmoLocation = reader.BaseStream.Position;
                this.Ammunition = new uint[p1, 2];
                for (ctr = 0; ctr < p1; ctr++)
                {
                    Ammunition[ctr, 0] = reader.ReadUInt32();
                    Ammunition[ctr, 1] = reader.ReadUInt32();
                }
                r1 = reader.ReadInt32();
                s1 = new int[r1, 1];
                for (ctr = 0; ctr < r1; ctr++)
                {
                    s1[ctr, 0] = reader.ReadInt32();
                }
                entry_count_type5 = reader.ReadInt32();
                for (ctr = 0; ctr < entry_count_type5; ctr++)
                {
                    sab_entry_type5 ent = new sab_entry_type5();
                    ent.a = reader.ReadInt32();
                    ent.b = reader.ReadByte();
                    ent.c = reader.ReadInt32();
                }
                t1 = reader.ReadInt32(); u1 = reader.ReadInt32();
                //read SV05 now
                SaboteurSV05 sv05 = new SaboteurSV05(reader);
                v1 = reader.ReadInt32();
                w1 = new string[v1];
                for (ctr = 0; ctr < v1; ctr++)
                {
                    w1[ctr] = reader.ReadStringNullTerminated();
                }
                x1 = reader.ReadInt32();
                for (int x = 0; x < x1; x++)
                {
                    reader.ReadInt32();
                }
                //read the second half of the save
                sab2 = new SaboteurSave_2(reader);
            }

        }
        public class SaboteurSV05
        {
            EndianReader reader;
            int ctr = 0;
            string SV05;
            uint code; //codecode means start of entry
            int entryCounter, option, _option, dead, _dead; // deadfood means end of entry
            byte opt2;
            public SaboteurSV05(EndianReader reader)
            {
                this.reader = reader;

                this.Read();
            }
            private void Read()
            {
                SV05 = reader.ReadStringNullTerminated();
                while (true)
                {
                    code = reader.ReadUInt32();
                    if (code == 0x00C0DEC0)
                    {
                        reader.BaseStream.Position -= 3;
                        continue;
                    }
                    else if (code != 0xC0DEC0DE)
                    {
                        reader.BaseStream.Position -= 4;
                        System.Diagnostics.Debug.WriteLine("Code value was not correct."
                            + string.Format(" Reader position was: 0x{0}.", reader.BaseStream.Position.ToString("X")));
                        break;
                    }
                    entryCounter = reader.ReadInt32();
                    for (ctr = 0; ctr < entryCounter; ctr++)
                    {
                        option = reader.ReadInt32();
                        if (option > 5) continue;

                        opt2 = reader.ReadByte();

                        if (opt2 != 0) { reader.ReadInt32(); }
                        else { reader.ReadStringNullTerminated(); };

                        this.SelectOption(option);

                    }
                    dead = reader.ReadInt32();
                }
                if (code != 0)
                {
                    reader.ReadStringNullTerminated();
                }
                else reader.BaseStream.Position += 1;
            }
            private void ReadType4()
            {
                uint _code = reader.ReadUInt32();
                int _entryCounter = reader.ReadInt32();
                for (int i = 0; i < _entryCounter; i++)
                {
                    _option = reader.ReadInt32();
                    if (_option > 5) continue;

                    byte _opt2 = reader.ReadByte();

                    if (_opt2 != 0) { reader.ReadInt32(); }
                    else { reader.ReadStringNullTerminated(); };

                    this.SelectOption(_option);
                }
                _dead = reader.ReadInt32();
            }
            private void SelectOption(int option)
            {
                switch (option)
                {
                    case 0:
                        reader.ReadStringNullTerminated();
                        break;
                    case 1:
                        reader.ReadSingle();
                        break;
                    case 2:
                        reader.ReadByte();
                        break;
                    case 3:
                        reader.ReadInt32();
                        break;
                    case 4:
                        this.ReadType4();
                        break;
                }
            }
        }
        public class SaboteurSave_2 // second half of the save
        {
            int ctr = 0;
            EndianReader reader;
            byte a;
            byte b;
            int entry_count_type1; // for sab_entry_type1
            int entry_count_type2; // for sab_entry_type2
            short c;
            // 0x400 empty bytes
            byte d;
            byte e;
            byte f;
            static int entry_count_type3 = 0xA; // for sab_entry_type3
            byte g;
            byte[] h;
            public long ContraBandLocation;
            public int Contraband, ContrabandBackup, ContrabadMod;
            public SaboteurSave_2(EndianReader reader)
            {
                this.reader = reader;

                this.Read();
            }
            private void Read()
            {
                a = reader.ReadByte(); b = reader.ReadByte();
                entry_count_type1 = reader.ReadInt32();
                for (ctr = 0; ctr < entry_count_type1; ctr++)
                {
                    sab_entry_type1 ent = new sab_entry_type1();
                    ent.a = reader.ReadInt32();
                    ent.b = reader.ReadByte(); ent.c = reader.ReadByte(); ent.d = reader.ReadByte();
                    ent.e = reader.ReadBytes(0x10);
                    ent.f = reader.ReadInt32(); ent.g = reader.ReadInt32();
                    ent.h = reader.ReadByte(); ent.i = reader.ReadByte();
                }
                //read game floats
                entry_count_type2 = reader.ReadInt32();
                for (ctr = 0; ctr < entry_count_type2; ctr++)
                {
                    sab_entry_type2 ent = new sab_entry_type2();
                    ent.a = reader.ReadSingle(); ent.b = reader.ReadSingle(); ent.c = reader.ReadSingle();
                    ent.d = reader.ReadSingle(); ent.e = reader.ReadSingle();
                    ent.f = reader.ReadByte();
                }
                c = reader.ReadInt16(); reader.BaseStream.Position += 0x400; //readplayerstats
                h = new byte[c];
                for (ctr = 0; ctr < c; ctr++)
                {
                    h[ctr] = reader.ReadByte();
                }
                d = reader.ReadByte(); e = reader.ReadByte(); f = reader.ReadByte();
                /* read the perk entries, still have to figure this out ;x */
                for (ctr = 0; ctr < entry_count_type3; ctr++)
                {
                    Perks ent = new Perks();
                }
                reader.BaseStream.Position += 0x96; /*placeholder for the perks */
                g = reader.ReadByte();
                this.ContraBandLocation = reader.BaseStream.Position;
                this.Contraband = reader.ReadInt32(); this.ContrabandBackup = reader.ReadInt32();
                this.ContrabadMod = reader.ReadInt32(); //added on to Contraband, if ctrbnd + mod > ctrbndBACK, ctrbnd = 0
            }
        }
        public struct sab_entry_type1
        {
            public int a;
            public byte b, c, d;
            public byte[] e; // 0x10
            public int f, g;
            public byte h, i;
        } // step 1 of second half
        public struct sab_entry_type2
        {
            public float a, b, c, d, e;
            public byte f;
        } // step 2 of second half
        public class Perks //perks : X3 rounds
        {
            public bool GoldUnlocked, SilverUnlocked, BronzeUnlocked;
            public Perks()
            {

            }

        }
        public struct sab_entry_type4 // entries near the beginning
        {
            public int a, b, c, d;
            public byte[] e, f; // 0x10
            public int[] g; //4 INTs
            public int h;// if(!h) go to readBYTES
            public int[] i; // count = h
            //readBYTES:
            public byte j, k, l, m;
        }
        public struct sab_entry_type5
        {
            public int a, c;
            public byte b;
        }
        public struct sab_entry_type6
        {
            public byte a;
            public byte[] b; //0x40
            public int c;
            public float d;
        }
        public struct sab_entry_type7
        {
            public int a, c, d;
            public byte b;
        }
    }
}
