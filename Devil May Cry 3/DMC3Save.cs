using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Capcom
{
    public class DMC3Save
    {
        public enum Difficulty
        {
            Easy = 0x00,
            Normal,
            Hard,
            VeryHard,
            DanteMustDie,
            HeavenOrHell
        }

        public enum MissionRank
        {
            NULL = -1,
            D = 0x00,
            C,
            B,
            A,
            S,
            SS,
            SSS
        }

        public class SaveSlot
        {
            public Dictionary<int, List<int>> MissionRanking = new Dictionary<int, List<int>>();

            public int SlotNumber { get; set; }
            public int SlotIndex
            {
                get { return (SlotNumber - 1); }
            }

            public int HighestMissionAttained;
            public int CurrentMission;
            public int PreviousMission;

            public int RedOrbs { get; set; }

            public byte VitalStarS { get; set; }
            public byte VitalStarL { get; set; }
            public byte DevilStar { get; set; }
            public byte HolyWater { get; set; }

            public byte BlueOrb { get; set; }
            public byte PurpleOrb { get; set; }
            public byte GoldOrb { get; set; }

            public bool IsEmpty;
            
            public void CopySlot(SaveSlot NewSlot)
            {
                this.HighestMissionAttained = NewSlot.HighestMissionAttained;
                this.CurrentMission = NewSlot.CurrentMission;
                this.PreviousMission = NewSlot.PreviousMission;

                this.RedOrbs = NewSlot.RedOrbs;

                this.VitalStarS = NewSlot.VitalStarS;
                this.VitalStarL = NewSlot.VitalStarL;
                this.DevilStar = NewSlot.DevilStar;
                this.HolyWater = NewSlot.HolyWater;

                this.BlueOrb = NewSlot.BlueOrb;
                this.PurpleOrb = NewSlot.PurpleOrb;
                this.GoldOrb = NewSlot.GoldOrb;

                this.IsEmpty = false;
            }
        }

        private EndianIO IO;

        public bool Character_Vergil_IsUnlocked;

        public List<SaveSlot> SaveSlots;
        /*
         * Save Section Sizes :
         * 1: 0x134
         * 2: 0x0A*0x3C
         * 3: 0x0708
         */

        public DMC3Save(EndianIO IO)
        {
            if (IO != null)
                this.IO = IO;

            this.VerifySave();

            SaveSlots = new List<SaveSlot>();

            this.Read();
        }

        private void VerifySave()
        {
            this.IO.In.SeekTo(0x3B8);
            for (int x = 0; x < 10; x++)
            {
                byte[] sectionData = IO.In.ReadBytes(0x070C);
                bool verified = VerifyDmc3Sum(sectionData, 0x0708);

                if (!verified)
                    throw new Exception("invalid save data detected.");
            }
        }

        private void Read()
        {
            this.IO.In.SeekTo(0x9);
            this.Character_Vergil_IsUnlocked = (this.IO.In.ReadByte() & 0x10) != 0;

            for (int x = 0; x < 10; x++)
            {
                IO.In.SeekTo(0x138 + (x * 0x40));
                SaveSlot slot = new SaveSlot();
                IO.In.BaseStream.Position += 0x08;

                slot.HighestMissionAttained = IO.In.ReadInt32();
                if (slot.HighestMissionAttained != 0)
                {
                    slot.CurrentMission = IO.In.ReadInt32();
                    slot.PreviousMission = IO.In.ReadInt32();

                    // read saved game data
                    int slotOffset = (0x70C * x);
                    this.IO.In.SeekTo(0x3BC + slotOffset);
                    slot.RedOrbs = IO.In.ReadInt32();

                    this.IO.In.SeekTo(0x3C5 + slotOffset);
                    slot.GoldOrb = this.IO.In.ReadByte();

                    this.IO.In.SeekTo(0x3C7 + slotOffset);
                    slot.BlueOrb = this.IO.In.ReadByte();
                    slot.PurpleOrb = this.IO.In.ReadByte();

                    this.IO.In.SeekTo(0x3D0 + slotOffset);
                    slot.VitalStarL = this.IO.In.ReadByte();
                    slot.VitalStarS = this.IO.In.ReadByte();
                    slot.DevilStar = this.IO.In.ReadByte();
                    slot.HolyWater = this.IO.In.ReadByte();

                    this.IO.In.SeekTo(0xA26 + slotOffset);
                    for (int j = 0; j < 6; j++)
                    {
                        List<int> ranks = new List<int>();
                        for(int k = 0; k < 20; k++)
                        {
                            byte t = this.IO.In.ReadByte();
                            ranks.Add(t != 0xff ? t: 6);
                        }

                        if(ranks.Count > 0)
                            slot.MissionRanking.Add(j, ranks);
                    }
                }
                else
                {
                    slot.IsEmpty = true;
                }

                slot.SlotNumber = x + 1;
                SaveSlots.Add(slot);
            }
        }

        public void Save()
        {
            this.IO.In.SeekTo(0x09);
            int flags = this.IO.In.ReadByte() | ((this.Character_Vergil_IsUnlocked) ? 0x10 : 0);
            this.IO.Out.SeekNWrite(0x09, (byte)flags);

            // Fix header data checksum
            this.IO.In.SeekTo(0x00);
            byte[] Header = IO.In.ReadBytes(0x138);
            Header.WriteInt16(0x136, 0x00);
            int sum = this.CalculateDmc3Sum(Header, 0x134);
            this.IO.Out.SeekNWrite(0x134, sum);            

            for (int x = 0; x < SaveSlots.Count; x++)
            {
                if (!SaveSlots[x].IsEmpty)
                {
                    var slot = SaveSlots[x];

                    int slotOffset = (0x70C * SaveSlots[x].SlotIndex);
                    this.IO.Out.SeekTo(0x3BC + slotOffset);
                    this.IO.Out.Write(SaveSlots[x].RedOrbs);

                    this.IO.Out.SeekTo(0x3C5 + slotOffset);
                    this.IO.Out.WriteByte(SaveSlots[x].GoldOrb);

                    this.IO.Out.BaseStream.Position += 1;
                    this.IO.Out.WriteByte(SaveSlots[x].BlueOrb);
                    this.IO.Out.WriteByte(SaveSlots[x].PurpleOrb);

                    this.IO.Out.SeekTo(0x3D0 + slotOffset);
                    this.IO.Out.WriteByte(SaveSlots[x].VitalStarL);
                    this.IO.Out.WriteByte(SaveSlots[x].VitalStarS);
                    this.IO.Out.WriteByte(SaveSlots[x].DevilStar);
                    this.IO.Out.WriteByte(SaveSlots[x].HolyWater);

                    //this.IO.Out.SeekTo(0x3B8 + (slotOffset + 0x682));

                    //for (int i = 0; i< 20; i++)
                        //this.IO.Out.WriteByte(0x05); // SSS

                    //this.IO.Out.SeekTo(0x3B8 + (slotOffset + 0x6EA));
                    //this.IO.Out.WriteByte(SaveSlots[x].HighestMissionAttained);

                    this.IO.Out.SeekTo(0x3B8 + (slotOffset + 0x66E));
                    for (int i = 0; i < 6; i++)
                    {
                        var ranks = slot.MissionRanking[i];
                        for (int j = 0; j < 20; j++)
                        {
                            IO.Out.WriteByte(ranks[j] != 6 ? ranks[j] : 0xff);
                        }
                    }

                    // Fix the checksum for the edited section
                    this.IO.In.SeekTo(0x3B8 + slotOffset);
                    byte[] SectionData = IO.In.ReadBytes(0x070C);
                    SectionData.WriteInt16(0x070A, 0x00);

                    sum = this.CalculateDmc3Sum(SectionData, 0x0708);
                    this.IO.Out.SeekNWrite(0xAC0 + slotOffset, sum);

                    slotOffset = (0x40 * SaveSlots[x].SlotIndex);
                    this.IO.Out.SeekTo(0x138 + (slotOffset + 8));
                    this.IO.Out.Write(SaveSlots[x].HighestMissionAttained);

                    this.IO.Out.Write(SaveSlots[x].CurrentMission);
                    this.IO.Out.Write(SaveSlots[x].PreviousMission);

                    // calculate slot checksum
                    this.IO.In.SeekTo(0x138 + slotOffset);
                    byte[] SlotHeaderData = IO.In.ReadBytes(0x40);
                    SlotHeaderData.WriteInt16(0x3E, 0x00);
                    sum = this.CalculateDmc3Sum(SlotHeaderData, 0x3C);
                    this.IO.Out.SeekNWrite(0x174 + slotOffset, sum);
                }
            }
        }

        public void DeleteSaveSlot(int slot) // slot = Slot Index
        {
            this.IO.Out.SeekTo(0x138 + (slot * 0x40));
            this.IO.Out.Write(new byte[0x3C]);
            this.IO.Out.Write(0x0000FFFF);
        }

        public void CopySaveSlot(int oldSlot, int newSlot)
        {
            this.IO.In.SeekTo(0x138 + ( oldSlot * 0x40));
            byte[] SlotHeader = this.IO.In.ReadBytes(0x40);

            this.IO.In.SeekTo(0x3B8 + ( oldSlot * 0x70C));
            byte[] SlotData = this.IO.In.ReadBytes(0x70C);

            this.IO.Out.SeekTo(0x138 + (newSlot * 0x40));
            this.IO.Out.Write(SlotHeader);

            this.IO.Out.SeekTo(0x3B8 + (newSlot * 0x70C));
            this.IO.Out.Write(SlotData);
        }

        private bool VerifyDmc3Sum(byte[] buffer, int count)
        {
            int ctr = count + 4, ct = ctr >> 1, a = 0, b = 0, c = 0, d, e, f, idx = 0;
            if (ct >= 2)
            {
                d = ctr - 4;
                d >>= 2;
                d++;
                ct = d << 2;
                ctr -= ct;
                e = d;
                for (int x = 0; x < e; x++)
                {
                    ct = buffer.ReadInt16(idx);
                    d = buffer.ReadInt16(idx + 2);
                    idx += 4;
                    a += ct;
                    c += d;
                }
            }

            if (ctr > 1)
            {
                b = buffer.ReadInt16(idx);
                idx += 2;
                ctr -= 2;
            }

            a += c;

            bool cont = (ctr > 0);
            ctr = a + b;
            if(cont)
            {
                f = buffer.ReadInt16(idx);
                f &= -256;
                ctr += f;
            }

            f = ctr >> 16;
            ctr &= 0xFFFF;
            f += ctr;
            ctr = f >> 16;
            f += ctr;
            f &= 0xFFFF;
            a = f - (1 << 0x10);
            a++;

            return a == 0;
        }

        private int CalculateDmc3Sum(byte[] buffer, int count)
        {
            int ctr = count + 4, ct = ctr >> 1, sum = 0, a = 0, b = 0, c = 0, d, e, f, idx = 0;
            if (ct >= 2)
            {
                d = ctr - 4;
                d >>= 2;
                d++;
                ct = d << 2;
                ctr -= ct;
                e = d;
                for (int x = 0; x < e; x++)
                {
                    ct = buffer.ReadInt16(idx);
                    d = buffer.ReadInt16(idx + 2);
                    idx += 4;
                    a += ct;
                    c += d;
                }
            }

            if (ctr > 1)
            {
                b = buffer.ReadInt16(idx);
                idx += 2;
                ctr -= 2;
            }

            a += c;

            bool cont = (ctr > 0);
            ctr = a + b;
            if (cont)
            {
                f = buffer.ReadInt16(idx);
                f &= -256;
                ctr += f;
            }

            f = ctr >> 16;
            ctr &= 0xFFFF;
            f += ctr;
            ctr = f >> 16;
            f += ctr;
            sum = ~f;

            return 0x00010000 | (sum & 0xFFFF);
        }
    }
}