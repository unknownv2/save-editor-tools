using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BioShock
{
    public class BioShockCompressedBlock
    {
        public int FullBlockLength;
        public uint Magic;
        public int MaxBlobLength;
        public int CompressedLen1;
        public int DecompressedLen1;
        public int CompressedLen2;
        public int DecompressedLen2;
        public long Position;

        public BioShockCompressedBlock(EndianReader reader)
        {
            Magic = reader.ReadUInt32();
            MaxBlobLength = reader.ReadInt32();
            CompressedLen1 = reader.ReadInt32();
            DecompressedLen1 = reader.ReadInt32();
            CompressedLen2 = reader.ReadInt32();
            DecompressedLen2 = reader.ReadInt32();
            Position = reader.BaseStream.Position;

            reader.BaseStream.Position += CompressedLen1;
        }
    }

    public class PlayerItems
    {
        private Dictionary<ItemId, XItem> _items;

        public PlayerItems()
        {
            
        }

        public PlayerItems(Dictionary<ItemId, XItem> items)
        {
            _items = items;
        }
        public void AddItem(XItem item)
        {
            if(!_items.ContainsKey(item.Id))
                _items.Add(item.Id, item);
        }

        public Dictionary<ItemId, XItem> Items 
        { 
            get { return _items; }
            set { _items = value; }
        }
        public int Count
        {
            get
            {
                return _items.Values.Where(item => item.Active).Sum(item => 1);
            }
        }
        public int Length
        {
            get
            {
                return _items.Values.Where(item => item.Active).Sum(item => (8 + 4 + 1 + 4 + 4 + item.DataLength));
            }
        }

        public XItem this[ItemId id]
        {
            get
            {
                return _items.ContainsKey(id) ? _items[id] : AddItem(id, false);
            }
            set
            {
                if (_items.ContainsKey(id))
                    _items[id] = value;
                else
                    AddItem(value);
            }
        }

        private XItem AddItem(ItemId id, bool active)
        {
            var item = new XItem
                {
                    Id = id,
                    Value = 0,
                    DataLength = 0,
                    Unk1 = 1,
                    Unk2 = 0,
                    Unk3 = 0,
                    Unk4 = 0,
                    Unk5 = 0,
                    Active = active
                };
            _items.Add(item.Id, item);
            return item;
        }

        public byte[] ToArray()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            io.Out.Write(Count);
            foreach (var item in Items.Values)
            {
                if (!item.Active) continue;

                io.Out.Write(item.ToArray());
            }
            return io.ToArray();
        }
    }

    public class XItem
    {
        public enum XGearSlot
        {
            Null = 0x00,
            Hat = 0x0A,
            Shirt = 0x0B,
            Boots = 0x0C,
            Pants = 0x0D
        }

        public ItemId Id;
        public int Value;

        public byte Unk1;
        public byte Unk2;
        public byte Unk3;
        public byte Unk4;
        public byte Unk5;

        public int DataLength;
        public byte[] Data;

        public long Position;

        public bool Active;
        public bool IsGear { get { return ((ulong) Id >> 30) != 0; }}

        // If it is a gear, lets get the slot its allocated to
        public XGearSlot Slot;

        public XItem()
        {
        }

        public XItem(EndianReader reader)
        {
            var id = reader.ReadUInt64();
            var isGear = (id >> 0x30) != 0;

            if (isGear)
            {
                Slot = (XGearSlot)((id >> 3) & 0x1F);
                id &= 0xFFFFFFFFFFFFFF00;
            }
            Id = (ItemId)id;
            Value = reader.ReadInt32();
            Unk1 = reader.ReadByte();
            Unk2 = reader.ReadByte();
            Unk3 = reader.ReadByte();
            Unk4 = reader.ReadByte();
            Unk5 = reader.ReadByte();
            DataLength = reader.ReadInt32();
            Active = true;

            if (DataLength != 0)
                Data = reader.ReadBytes(DataLength);
        }

        public byte[] ToArray()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            var id = (ulong) Id;
            if (IsGear)
            {
                id &= 0xFFFFFFFFFFFFFF00;
                // Allocate to 'Hat' slot if no slot is set
                if(Slot == XGearSlot.Null)
                    Slot = XGearSlot.Hat;

                id |= (ulong)((int) Slot) << 3;
            }

            io.Out.Write(id);
            io.Out.Write(Value);
            io.Out.Write(Unk1);
            io.Out.Write(Unk2);
            io.Out.Write(Unk3);
            io.Out.Write(Unk4);
            io.Out.Write(Unk5);
            io.Out.Write(DataLength);
            if (DataLength != 0)
                io.Out.Write(Data);

            return io.ToArray();
        }
    }

    public enum ItemId : long
    {
        // stats
        SilverCoins = -1,
        Lockpicks = -2,

        // Item Ammo
        HeaterAmmo = 0x58,
        VolleyGunAmmo = 0x79,
        HailFireAmmo = 0x98,
        SaltsUpgrade = 0xEB,
        RPGAmmo = 0xED,
        CarbineAmmo = 0x124,
        HealthUpgrade = 0x114,
        SniperRifleAmmo = 0x140,
        ShotgunAmmo = 0x142,
        ShieldUpgrade = 0x149,
        MachineGunAmmo = 0x1C1,
        PistolAmmo = 0x1E4,
        HandCannonAmmo = 0x1E7,
        RepeaterAmmo = 0x1F6,
        BurstgunAmmo = 0x0248,

        //Vigors
        MurderOfCrows = 0xA9,
        ShockJockey = 0x0F,
        BuckingBronco = 0x07,
        ReturnToSender = 0x212,
        Undertow = 0x08,
        Charge = 0x02,
        Possession = 0x01,
        DevilsKiss = 0x09,

        //Vigors Upgrades
        BroncoAid = 0x0000020A,
        BroncoBoost = 0x000001B1,

        ChargeAid = 0x00000072,
        ChargeBoost = 0x0000013C,

        CrowsTrapAid = 0x14D,
        CrowsBoost = 0x0000018B,

        DevilsKissAid = 0x0000014A,
        DevilsKissBoost = 0x0000019C,

        PossessionAid = 0x000000C6,
        PossessionForLess = 0x000000CB,

        SenderAid = 0x00000152,
        SendForLess = 0x0000014E,

        ShockChainAid = 0x000000BA,
        ShockDurationAid = 0x000000C9,

        UndertowAid = 0x00000199,
        UndertowBoost = 0x00000012,

        // Weapons
        //Repeater = 0x00000107,

        // Weapon Upgrades
        //RepeaterDamange
        
        /* Gears 
         * Hats     =   0x50
         * Shirts   =   0x58
         * Pants    =   0x68
         * Boots    =   0x60
         */
        /*
        BloodToSalt = 0x0001000010000000,
        // Hats
        AmmoCap = 0x0001000018800000,
        BurningHalo = 0x0001000016600000,
        ElectricPunch = 0x000100001B600000,
        ElectricTouch = 0x0001000016800000,
        ExtraExtra = 0x0001000018C00000,
        GearHead = 0x0001000017000000,
        HillsRunnersHat = 0x0001000018400000,
        QuickHanded = 0x000100001A200000,
        RisingBloodlust = 0x0001000011E00000,
        ShelteredLife = 0x0001000019800000,
        SpareTheRod = 0x0001000018E00000,
        Storm = 0x0001000010C00000,
        ThrottleControl = 0x100001a000000,
        PeakRunnersHat = 0x000100001B000000,

        // Shirts
        AmmoAdvantage = 0x000100001B400000,
        
        BulletBoon = 0x0001000019400000,
        CoatOfArms = 0x0001000012000000,
        DropCloth = 0x0001000018200000,
        Executioner = 0x0001000010400000,
        Executioner2 = 0x0001000013600000,
        NitroVest = 0x0001000018000580,
        Pyromaniac = 0x0001000011600000,
        ScavengersVest = 0x0001000017600000,
        ShockJacket = 0x0001000016A00000,
        SkyLineAccuracy = 0x0001000010600000,
        SugarRush = 0x0001000019E00000,
        WinterShield = 0x000100001A400000,

        // Pants
        AngryStompers = 0x0001000017A00000,
        BrittleSkinned = 0x0001000017C00000,
        BullRush = 0x0001000019200000,
        DeadlyLungers = 0x0001000017E00000,
        FireBird = 0x0001000011C00000,
        GhostPossee = 0x0001000019C00000,
        GhostSoldier = 0x000100001B200000,
        HeadMaster = 0x0001000012C00000,
        HealthForSalts = 0x0001000010800000,
        LastManStanding = 0x0001000010A00000,
        SkyLineReloader = 0x0001000017200000,
        SpectralSidekick = 0x0001000011000000,
        UrgentCare = 0x0001000019A00000,

        // Boots
        Betrayer = 0x0001000017800000,
        
        EagleStrike = 0x0001000015400000,
        FitAsAFiddle = 0x0001000017400000,
        FleetFeet = 0x0001000018600000,
        HandymanNemesis = 0x0001000018A00000,
        KillToLive = 0x0001000012400000,
        NewtonsLaw = 0x0001000019000000,
        Noreaster = 0x0001000011200000,
        Overkill = 0x0001000019600000,
        TunnelVision = 0x0001000016E00000,

        SkyLineMeleeBoost = 0x000100001A600000,
        VampyrsEmbrace2 = 0x0001000015C00000,

        NeighborhoodChemist2 = 0x0001000012E00000,
        ElectricAura2 = 0x0001000016C00000,
        FieryAura2 = 0x0001000013A00000,
        FadeToBlack2 = 0x0001000013800000
        */

// ReSharper disable InconsistentNaming
        BLOODTOSALT = 0x0001000010000000,
        SPOOFBOOSTER = 0x0001000010200000,
        EXECUTIONER = 0x0001000010400000,
        SKY_LINEACCURACY = 0x0001000010600000,
        HEALTHFORSALTS = 0x0001000010800000,
        LASTMANSTANDING = 0x0001000010A00000,
        STORM = 0x0001000010C00000,
        OVERDRIVE = 0x0001000010E00000,
        SPECTRALSIDEKICK = 0x0001000011000000,
        NOREASTER = 0x0001000011200000,
        FADETOBLACK = 0x0001000011400000,
        PYROMANIAC = 0x0001000011600000,
        ICE_FLECKEDVIGOR = 0x0001000011800000,
        HAMMERBLOWS = 0x0001000011A00000,
        FIREBIRD = 0x0001000011C00000,
        RISINGBLOODLUST = 0x0001000011E00000,
        COATOFHARMS = 0x0001000012000000,
        THIRDHAND2 = 0x0001000012200000,
        KILLTOLIVE = 0x0001000012400000,
        MACHINEWHISPERER = 0x0001000012600000,
        BREACHCOAT = 0x0001000012800000,
        STUNNING = 0x0001000012A00000,
        HEADMASTER = 0x0001000012C00000,
        NEIGHBORHOODCHEMIST2 = 0x0001000012E00000,
        SPOOFBOOSTER2 = 0x0001000013000000,
        BLOOD_POWEREDVIGORS2 = 0x0001000013200000,
        CRITHACKING2 = 0x0001000013400000,
        EXECUTIONER2 = 0x0001000013600000,
        FADETOBLACK2 = 0x0001000013800000,
        FIERYAURA2 = 0x0001000013A00000,
        FIGHTERACE2 = 0x0001000013C00000,
        HAMMERBLOWS2 = 0x0001000013E00000,
        HEADHUNTER2 = 0x0001000014000000,
        ICE_FLECKEDVIGOR2 = 0x0001000014200000,
        LEFTHANDSPELLSLOVE2 = 0x0001000014400000,
        MACHINEWHISPERER2 = 0x0001000014600000,
        OVERDRIVE2 = 0x0001000014800000,
        PHOENIX2 = 0x0001000014A00000,
        RIGHTHANDSPELLSHATE2 = 0x0001000014C00000,
        RISINGBLOODLUST2 = 0x0001000014E00000,
        SECONDCHANCE2 = 0x0001000015000000,
        SKY_LINEACCURACY2 = 0x0001000015200000,
        EAGLESTRIKE = 0x0001000015400000,
        STORM2 = 0x0001000015600000,
        STUNNING2 = 0x0001000015800000,
        TAINTEDLOVE2 = 0x0001000015A00000,
        VAMPYRSEMBRACE2 = 0x0001000015C00000,
        CritHacking = 0x0001000015E00000,
        ThirdHand = 0x0001000016000000,
        MachineWhisperer = 0x0001000016200000,
        SoulTrap = 0x0001000016400000,
        BURNINGHALO = 0x0001000016600000,
        ELECTRICTOUCH = 0x0001000016800000,
        SHOCKJACKET = 0x0001000016A00000,
        ELECTRICAURA2 = 0x0001000016C00000,
        TUNNELVISION = 0x0001000016E00000,
        GEARHEAD = 0x0001000017000000,
        SKY_LINERELOADER = 0x0001000017200000,
        FITASAFIDDLE = 0x0001000017400000,
        SCAVENGERSVEST = 0x0001000017600000,
        BETRAYER = 0x0001000017800000,
        ANGRYSTOMPERS = 0x0001000017A00000,
        BRITTLE_SKINNED = 0x0001000017C00000,
        DEADLYLUNGERS = 0x0001000017E00000,
        NITROVEST = 0x0001000018000000,
        DROPCLOTH = 0x0001000018200000,
        HILLRUNNERSHAT = 0x0001000018400000,
        FLEETFEET = 0x0001000018600000,
        AMMOCAP = 0x0001000018800000,
        HANDYMANNEMESIS = 0x0001000018A00000,
        EXTRAEXTRA = 0x0001000018C00000,
        SPARETHEROD = 0x0001000018E00000,
        NEWTONSLAW = 0x0001000019000000,
        BULLRUSH = 0x0001000019200000,
        BULLETBOON = 0x0001000019400000,
        OVERKILL = 0x0001000019600000,
        SHELTEREDLIFE = 0x0001000019800000,
        URGENTCARE = 0x0001000019A00000,
        GHOSTPOSSE = 0x0001000019C00000,
        SUGARRUSH = 0x0001000019E00000,
        THROTTLECONTROL = 0x000100001A000000,
        QUICKHANDED = 0x000100001A200000,
        WINTERSHIELD = 0x000100001A400000,
        SKY_LINEMELEEBOOST = 0x000100001A600000,
        SpeedBoost1 = 0x000100001A800000,
        PEAKRUNNERSHAT = 0x000100001B000000,
        GHOSTSOLDIER = 0x000100001B200000,
        AMMOADVANTAGE = 0x000100001B400000,
        ELECTRICPUNCH = 0x000100001B600000,
        AmateurDrinker = 0x000100001B800000,
        AmateurMedic = 0x000100001BA00000,
        Shielding = 0x000100001BC00000,
        SpeedBoost2 = 0x000100001BE00000,
        BlindPatriotism = 0x000100001C000000,
        MachineProbe = 0x000100001C200000,
        PirateSignal = 0x000100001C400000,
        Jamming = 0x000100001C600000,
        Shorthand = 0x000100001C800000,
        FocusedHacker = 0x000100001CA00000,
        BornforFailure = 0x000100001CC00000,
        SOS = 0x000100001CE00000,
        Amplifier = 0x000100001D000000,
        HackersDelight = 0x000100001D200000,
        CircuitDilation = 0x000100001D400000,
        EasyRescuer = 0x000100001D600000,
        StableTears = 0x000100001D800000,
        SecondChance = 0x000100001DA00000,
        Talisman = 0x000100001DC00000,
        Hammerblows = 0x000100001DE00000,
        TheLevine = 0x000100001E000000,
        AssistBronco = 0x000100001E200000,
        AssistChill = 0x000100001E400000,
        AssistShock = 0x000100001E600000,
        AssistDamage = 0x000100001E800000,
        AssistBatteries = 0x000100001EA00000,
        BRONCOSTORM = 0x000100001EC00000,
        IceStorm = 0x000100001EE00000,
        ShockStorm = 0x000100001F000000,
        Enthrall = 0x000100001F200000,
        RightHandSpellsHate = 0x000100001F400000,
        LeftHandSpellsLove = 0x000100001F600000,
        ExpeditiousRetreat = 0x000100001F800000,
        ContagiousPosse = 0x000100001FA00000,
        ReinforcedShield = 0x000100001FC00000,
        CRITICALHACKER = 0x000100001FE00000,
// ReSharper restore InconsistentNaming

    }
    public class BioShockInfiniteSave
    {
        private EndianIO IO;
        private EndianIO _saveFile;
        private readonly List<BioShockCompressedBlock> compressedBlocks;

        public Dictionary<ItemId, XItem> StatItems;

        public PlayerItems PlayerItems;

        private long FirstCompressedBlockPosition = -1, ItemListPosition = -1;

        private long ItemDataLength;

        private bool savePlayerState = true;

        public long[] XPlayerLocations = new long[2];
        public int[] XPlayerPointers = new int[2];

        public BioShockInfiniteSave(EndianIO io)
        { 
            IO = io;
            compressedBlocks = new List<BioShockCompressedBlock>();

            Read();

            ReadPlayerState();
        }

        private void Read()
        {
            int ct;
            //int val;
            //BioShockCompressedBlock bscBlock;

            // verify the header
            if (IO.In.ReadUInt32() != 0xC24860DA || IO.In.ReadInt32() < 0 || IO.In.ReadUInt32() != 0xA13580C2)
                throw new BioShockException("invalid save header.");
            IO.In.ReadInt32();
            // read a random table
            var rtablePos = IO.In.ReadInt32();
            ReadAsciiString32();
            IO.In.ReadByte();
            for (int x = 0; x < 3; x++)
            {
                ct = IO.In.ReadInt32();
                for (int i = 0; i < ct; i++)
                {
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                }
            }
            ct = IO.In.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt32();
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
            }

            IO.In.ReadInt64();
            IO.In.ReadByte();
            var cBlockCount = IO.In.ReadInt32();
            for (int j = 0; j < cBlockCount; j++)
            {
                IO.In.ReadInt32();
                IO.In.ReadInt32();

                ReadUnk3();

                if(FirstCompressedBlockPosition == -1)
                    FirstCompressedBlockPosition = IO.Position;

                ReadCompressedBlocks();
                var endOfFirstPlayerBlock = IO.Position;
                ct = IO.In.ReadInt32();
                for (int i = 0; i < ct; i++)
                {
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                    IO.In.ReadInt32();
                }
                ct = IO.In.ReadInt32();
                for (var i = 0; i < ct; i++)
                {
                    switch (ReadAsciiString32())
                    {
                        case "Arc_XPlayer.XSinglePlayerPlayerController":
                            XPlayerLocations[0] = (IO.Position - endOfFirstPlayerBlock) + 0x3C;
                            break;
                        case ("Arc_XPlayer.XPlayerPawn"):
                            XPlayerLocations[1] = (IO.Position - endOfFirstPlayerBlock) + 0x38;
                            break;
                    }
                    
                    for (var x = 0; x < 0x10; x++)
                    {
                        if (x == 0xf && XPlayerLocations[0] != -1 && XPlayerPointers[0] == 0)
                            XPlayerPointers[0] = IO.In.ReadInt32();
                        else if (x == 0xe && XPlayerLocations[1] != -1 && XPlayerPointers[1] == 0)
                            XPlayerPointers[1] = IO.In.ReadInt32();
                        else
                            IO.In.ReadInt32();
                    }
                }
                ReadCompressedBlocks();
                // stop 3
                IO.In.ReadInt32();
                IO.In.ReadByte();
                ct = IO.In.ReadInt32();
                for (int i = 0; i < ct; i++)
                {
                    IO.In.ReadInt32();
                }
            }
            IO.In.ReadInt32();
            IO.In.ReadInt32();
            IO.In.ReadByte();
            IO.In.ReadByte();
            IO.In.ReadByte();

            IO.In.ReadInt32();
            IO.In.ReadInt32();

            IO.In.ReadInt32();

            ct = IO.In.ReadInt32();
            while (ct-- > 0)
            {
                ReadUnk4();

                IO.In.ReadInt32();
                IO.In.ReadInt32();
            }
            ct = IO.In.ReadInt32();
            while (ct-- > 0)
            {
                ReadUnk4();

                IO.In.ReadInt32();
                IO.In.ReadInt32();
            }
            ct = IO.In.ReadInt32();
            while (ct-- > 0)
            {
                ReadUnk4();
            }

            var ctr = IO.In.ReadInt32();
            for (var i = 0; i < ctr; i++)
            {
                ReadUnk2();

                IO.In.ReadInt32();

                ReadUnk4();

                var tr = IO.In.ReadInt32();
                for (var t = 0; t < tr; t++)
                {
                    if (IO.In.ReadInt32() != -1)
                        IO.In.ReadInt32();

                    ReadUnk1();
                    ReadUnk1();
                    IO.In.ReadByte();
                    var c = IO.In.ReadInt32();
                    for (var j = 0; j < c; j++)
                    {
                        ReadUnk1();
                    }
                    IO.In.ReadInt32();
                    ReadCompressedBlocks();
                    ReadCompressedBlocks();
                }
            }
            ctr = IO.In.ReadInt32();
            for (var i = 0; i < ctr; i++)
            {
                ReadUnk4();
                var h = IO.In.ReadInt32();
                for (var j = 0; j < h; j++)
                {
                    ReadUnk1();
                }
            }
            
            IO.SeekTo(rtablePos);
            ct = IO.In.ReadInt32();
            for (var i = 0; i < ct; i++)
            {
                IO.In.ReadInt32();
            }
            ct = IO.In.ReadInt32();
            for (var i = 0; i < ct; i++)
            {
                IO.In.ReadInt32();
                IO.In.ReadAsciiString(IO.In.ReadInt32());
            }
        }
        
        private void ReadPlayerState()
        {
            _saveFile = new EndianIO(InflateCompressedStream(), EndianType.BigEndian, true);

            var player = new EndianIO(_saveFile.In.ReadBytes(_saveFile.In.ReadInt32()), EndianType.BigEndian, true);
            var reader = player.In;
            int ct;

            if (reader.ReadUInt32() == 0xA13580C2)
            {
                reader.ReadInt32();
            }
            reader.ReadByte();

            reader.ReadInt32();
            reader.ReadInt32();
            for (int i = 0; i < 3; i++)
            {
                reader.ReadInt32();
            }
            for (int i = 0; i < 3; i++)
            {
                reader.ReadInt32();
            }
            ct = reader.ReadInt32();
            for (int x = 0; x < ct; x++)
            {
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
            }
            var items = new Dictionary<ItemId, XItem>();
            ItemListPosition = reader.BaseStream.Position;
            ct = reader.ReadInt32();
            /*
            var txtWriter =
                new StreamWriter(
                    @"F:\Projects\BioShock\BioShock Infinite\Saves\xBASSxMONSTA's Bioshock Infinite Save (Story Completed)\items.txt");
            */
            for (var i = 0; i < ct; i++)
            {
                var item = new XItem(reader);

                items.Add(item.Id, item);
            }
            //txtWriter.Close();

            ItemDataLength = reader.BaseStream.Position - (ItemListPosition + 4);

            PlayerItems = new PlayerItems(items);

            ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                reader.ReadInt64();
            }
            ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                reader.ReadInt64();
                if (reader.ReadByte() != 0)
                    reader.ReadInt32();
                reader.ReadByte();
            }
            ct = reader.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                reader.ReadInt32();
                reader.ReadInt32();
            }
            // player stats
            if (reader.ReadUInt32() == 0xA13580C2)
            {
                reader.ReadInt32();
            }
            reader.ReadInt64();
            reader.ReadInt64();
            reader.ReadInt64();
            reader.ReadInt64();

            reader.ReadInt64();
            reader.ReadInt64();
            reader.ReadInt64();
            reader.ReadInt64();

            reader.ReadByte();
            reader.ReadUInt32();
            StatItems = new Dictionary<ItemId, XItem>
                {
                    {
                        ItemId.SilverCoins, new XItem
                            {
                                Id = ItemId.SilverCoins,
                                Position = reader.BaseStream.Position,
                                Value = reader.ReadInt32()
                            }
                    }
                };
            reader.BaseStream.Position += 0x18;
            StatItems.Add(ItemId.Lockpicks,
                      new XItem
                          {
                              Id = ItemId.Lockpicks,
                              Position = reader.BaseStream.Position,
                              Value = reader.ReadInt32()
                          });
        }

        private byte[] WritePlayerState()
        {
            // write state items before transferring data
            foreach (var statItem in StatItems.Values)
            {
                _saveFile.SeekTo(4 + statItem.Position);
                _saveFile.Out.Write(statItem.Value);
            }

            var playerState = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            _saveFile.SeekTo(0);
            var psLen = _saveFile.In.ReadInt32();

            // temporary placeholder for the length
            playerState.Out.Write(0);
            playerState.Out.Write(_saveFile.In.ReadBytes(ItemListPosition));

            playerState.Out.Write(PlayerItems.ToArray());

            // skip table length + item counter
            _saveFile.Position += (ItemDataLength + 4);
            // write remainder data (footer)
            playerState.Out.Write(_saveFile.In.ReadBytes(psLen - (ItemListPosition + ItemDataLength + 4)));
            playerState.Out.SeekNWrite(0, (int)(playerState.Length - 4));

            for (var i = 0; i < XPlayerPointers.Length; i++)
            {
                XPlayerPointers[i]  += (int)(PlayerItems.Length - ItemDataLength);
            }
 
            return playerState.ToArray();
        }

        public void Save()
        {
            var fullCompBlock = WriteStreamToSaveFile(savePlayerState ? WritePlayerState() : _saveFile.ToArray());

            var tablePtr = IO.In.SeekNReadInt32(0x10);
            var newIO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            IO.SeekTo(0);
            newIO.Out.Write(IO.In.ReadBytes(FirstCompressedBlockPosition));
            newIO.Out.Write(fullCompBlock);
            var cBlockLen = IO.In.ReadInt32();
            IO.Position += cBlockLen;
            newIO.Out.Write(IO.In.ReadBytes(tablePtr - (FirstCompressedBlockPosition + (cBlockLen + 4))));
            var newTablePtr = newIO.Position;
            newIO.Out.Write(IO.In.ReadBytes(IO.Length - IO.Position));

            newIO.Out.SeekNWrite(0x10, (int)newTablePtr);
            newIO.Out.SeekNWrite((FirstCompressedBlockPosition + fullCompBlock.Length + XPlayerLocations[0]), XPlayerPointers[0]);
            newIO.Out.SeekNWrite((FirstCompressedBlockPosition + fullCompBlock.Length + XPlayerLocations[1]), XPlayerPointers[1]);
            var newSave = newIO.ToArray();
            newIO.Close();

            IO.Stream.SetLength(newSave.Length);
            IO.SeekTo(0);
            IO.Out.Write(newSave);
            IO.Stream.Flush();
        }

        private byte[] WriteStreamToSaveFile(byte[] fstPlayerState)
        {
            var block = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            var dBlockLength = fstPlayerState.ReadInt32(0);  // decompressed buffer length
            // compress the first block *ONLY* ( we don't edit the rest)

            var compressedBuffer = Ionic.Zlib.ZlibStream.CompressBuffer(fstPlayerState);

            // temporary full block length
            block.Out.Write(compressedBuffer.Length + 0x18);
            block.Out.Write(0x9E2A83C1);
            block.Out.Write(0x020000);
            block.Out.Write(compressedBuffer.Length);
            block.Out.Write(dBlockLength + 4);
            block.Out.Write(compressedBuffer.Length);
            block.Out.Write(dBlockLength + 4);
            block.Out.Write(compressedBuffer);

            return block.ToArray();
        }

        public void OverwriteStream(byte[] streamData)
        {
            _saveFile.Close();
            _saveFile = new EndianIO(new MemoryStream(streamData), EndianType.BigEndian, true );
      
            // so we don't overwrite anything we modified manually
            savePlayerState = false;
        }

        private string ReadAsciiString32()
        {
            return IO.In.ReadAsciiString(IO.In.ReadInt32());
        }
        private void ReadUnk1()
        {
            IO.In.ReadInt32();
            IO.In.ReadInt32();
        }
        private void ReadUnk2()
        {
            var ct = IO.In.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                IO.In.ReadInt32();
            }
        }
        private void ReadUnk3()
        {
            var ct = IO.In.ReadInt32();
            for (int i = 0; i < ct; i++)
            {
                ReadUnk1();
            }
        }
        private void ReadUnk4()
        {
            ReadUnk1();
            ReadUnk1();
            IO.In.ReadByte();
            ReadUnk1();
        }
        private void ReadCompressedBlocks()
        {
            BioShockCompressedBlock bscBlock;
            var fullBlockLength = IO.In.ReadInt32();
            if(fullBlockLength == 0)
                return; 
            do
            {
                bscBlock = new BioShockCompressedBlock(IO.In);
                compressedBlocks.Add(bscBlock);
            } while ((fullBlockLength -= (0x18 + bscBlock.CompressedLen1)) > 0);
        }
        public byte[] InflateCompressedStream()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            foreach (var compressedBlock in compressedBlocks)
            {
                IO.SeekTo(compressedBlock.Position);
                io.Out.Write(Ionic.Zlib.ZlibStream.UncompressBuffer(IO.In.ReadBytes(compressedBlock.CompressedLen1)));
            }
            return io.ToArray();
        }
    }
    internal class BioShockException : Exception
    {
        internal BioShockException(string message)
            : base("BioShock: " + message)
        {

        }
    }
}
