using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BKSystem.IO;

namespace NinjaGaiden
{
    public class NinjaGaiden3RESave
    {
        private EndianIO IO;

        public NinjaGaiden3Skills UnlockManager;

        public int KarmaCurrentRyu { get; set; }
        public int KarmaCurrentAyane { get; set; }
        public int KarmaScore { get; set; }

        public NinjaGaiden3RESave(EndianIO io)
        {
            IO = io;
            Read();
        }

        private void Read()
        {
            // skill unlock bit table
            IO.SeekTo(0x1170);
            UnlockManager = new NinjaGaiden3Skills(new EndianIO(new MemoryStream(IO.In.ReadBytes(0x200)), EndianType.BigEndian, true));

            KarmaCurrentRyu = IO.In.SeekNReadInt32(0x11F8);
            KarmaCurrentAyane = IO.In.SeekNReadInt32(0x1200);
            KarmaScore = IO.In.SeekNReadInt32(0x121C);
        }

        public void Save()
        {
            // write the skill unlock table back
            IO.SeekTo(0x1170);
            IO.Out.Write(UnlockManager.Serialize());

            IO.Out.SeekNWrite(0x11F8, KarmaCurrentRyu);
            IO.Out.SeekNWrite(0x1200, KarmaCurrentAyane);
            IO.Out.SeekNWrite(0x121C, KarmaScore);
        }
    }

    public enum NinjaGaiden3SkillRyu
    {
        //Feats
        ExtraHealth1 = 0x0C,
        ExtraHealth2 = 0x1F,
        ExtraHealth3 = 0xB7,
        ExtraHealth4 = 0xB8,
        ExtraHealth5 = 0xB9,
        CounterAttack = 0x18,
        FourRings = 0xD7,
        GuillotineThrow = 0xA,
        CicadaSurge = 0x0B,
        Meditation = 0xC0,
        FlyingSwallow = 0x17,
        WindPath = 0x19,
        FalconsEye = 0x20,

        // Weapons
        DragonSword1 = 3,
        DragonSword2 = 4,
        DragonSword3 = 0xf,
        FalconsTalons1 = 5,
        FalconsTalons2 = 6,
        FalconsTalons3 = 0x10,
        EclipseScythe1 = 1,
        EclipseScythe2 = 9,
        EclipseScythe3 = 0x12,
        DualKatana1 = 0,
        DualKatana2 = 8,
        DualKatana3 = 0x11,
        LunarStaff1 = 0xBA,
        LunarStaff2 = 0xBB,
        LunarStaff3 = 0xBC,
        KusariGama1 = 0xBD,
        KusariGama2 = 0xBE,
        KusariGama3 = 0xBF,

        //Ninpos
        Inferno1 = 0x07,
        Inferno2 = 0x0D,
        Inferno3 = 0x13,
        TrueInferno = 0xC1,
        WindBlades1 = 0x02,
        WindBlades2 = 0x0E,
        WindBlades3 = 0x14,
        PiercingVoid1 = 0x1C,
        PiercingVoid2 = 0x1D,
        PiercingVoid3 = 0x1E,

        // Costumes
        LegendaryBlackFalcon = 0xD5,
        TraditionalDarkBlue = 0xCC,
        DragonMuscleSuit = 0xCD,
        FiendHayabusa = 0xCE,

        // Hidden Custumes

        //RealizedNightmare = 0x10F, - Have a saved game from NG3
        //SpiritOfTheFighter = 0x110, - Unlocked with pre-order
    }

    public enum NinjaGaiden3SkillsAyane
    {
        //Feats
        ExtraHealth1 = 0xC2,
        ExtraHealth2 = 0xC3,
        ExtraHealth3 = 0xC4,
        ExtraHealth4 = 0xC5,
        ExtraHealth5 = 0xC6,
        CounterAttack = 0xC8,
        GuillotineThrow = 0xCA,
        FlyingSwallow = 0xC7,
        WindPath = 0xC9,
        Meditation = 0xCB,

        //Weapon
        FumaKodachi1 = 0x1A,
        FumaKodachi2 = 0xD9,
        FumaKodachi3 = 0xDA,

        // Ninpo
        RagingMountainGod1 = 0x1B,
        RagingMountainGod2 = 0xDB,
        RagingMountainGod3 = 0xDC,

        // Costumess
        NocturnalButterFly = 0xD6,
        PhantomButterFly = 0xCF,
        BlossomOfMiyama = 0xD0,
        RagingGodNineOfViolet = 0xD1,
    }

    public class NinjaGaiden3Skills
    {
        public const string INVALID_ARRAY = "Cannot stream data. Invalid stream data detected.";

        private EndianIO IO;
        //private BitStream BitIO;

        private int _bitTableIndex = -1;
        private long _bitMask;

        private const int _skillUnlockCount = 0x200;
        private bool[] _skillUnlocks = new bool[_skillUnlockCount];

        public bool Unknown1 { get; set; }

        public bool JinranMaru
        {
            get { return _skillUnlocks[0x3C]; }
            set { _skillUnlocks[0x3C] = value; }
        }

        public bool FalconsTalons { get; set; }
        
        public NinjaGaiden3Skills(EndianIO io)
        {
            IO = io;
        }
        /// <summary>
        /// The bit table is divided into 64-bit arrays.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsUnlocked(object oIndex)
        {
            var index = Convert.ToInt32(oIndex);
            var newTableIndex = index >> 6;
            if (newTableIndex != _bitTableIndex)
            {
                //if (BitIO != null)
                    //BitIO.Close();

                IO.SeekTo(newTableIndex << 3);
                _bitMask = IO.In.ReadInt64();

                //BitIO = new BitStream(BitConverter.GetBytes(IO.In.ReadInt64()));

                // so we don't need to read the bit array every time
                _bitTableIndex = newTableIndex;
            }
            var bilocal = index & 0x3F;
            var uMask = (long)1 << bilocal;
            return (_bitMask & uMask) != 0;

            //return _skillUnlocks[index];
        }

        public void SetLock(object oIndex, bool bLock)
        {
            var index = Convert.ToInt32(oIndex);
            var newTableIndex = index >> 6;
            if (newTableIndex != _bitTableIndex)
            {
                IO.SeekTo(newTableIndex << 3);
                _bitMask = IO.In.ReadInt64();

                // so we don't need to read the bit array every time
                _bitTableIndex = newTableIndex;
            }
            var bilocal = index & 0x3F;
            var uMask = (long)1 << bilocal;
            IO.SeekTo(newTableIndex << 3);
            IO.Out.Write(_bitMask |= uMask);
        }

        public byte[] Serialize()
        {
            IO.Stream.Flush();
            return IO.ToArray();
        }
    }
}