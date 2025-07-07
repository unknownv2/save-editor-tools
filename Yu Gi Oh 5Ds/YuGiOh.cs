using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YuGiOhDecadeDuels
{
    internal class CardListEntry
    {
        public List<bool> Unlocked;
        public List<bool> New;

        public CardListEntry(int mask)
        {
            Unlocked = Horizon.Functions.BitHelper.LoadValue((mask >> 0x18) & 0xFF, 8);
            New = Horizon.Functions.BitHelper.LoadValue((mask >> 0x10) & 0xFF, 8);
        }

        public byte[] ToArray()
        {
            byte[] card = new byte[4]; // should not be more than 32 bits
            card[0] = Horizon.Functions.BitHelper.ConvertToWriteableByte(Unlocked.ToArray());
            card[1] = Horizon.Functions.BitHelper.ConvertToWriteableByte(New.ToArray());

            return card;
        }

        public void UnlockAll()
        {
            bool isNew = false;
            for (int i = 1; i < 0x04; i++)
            {
                if(Unlocked[i])
                    continue;
                Unlocked[i] = true;
                isNew = true;
            }
            New[1] = isNew;
        }
    }

    internal class SaveGame
    {
        private EndianIO IO;
        public List<ushort> MainDeck;
        public List<CardListEntry> UnlockedCards;

        public bool IsPlus { get; set; }

        internal SaveGame(EndianIO io)
        {
            if(!io.Opened)
                io.Open();

            IO = io;
        }

        public bool Read()
        {
            IsPlus = true;
            if((IO.In.SeekNReadInt32(0) != 0x58410A03))
                throw new YugiohException("invalid savegame header detected.");

            // means we have to load the original save data
            var sizeMask = IO.In.ReadInt32();
            var dataSize = IO.In.ReadInt32();
            if ((sizeMask != 0x005F5111) || (dataSize != 0x00005E51))
            {
                IsPlus = false;
            }

            var saveData = IO.ToArray();
            var storedSum = IO.In.SeekNReadUInt32(0x0C);
            saveData.WriteInt32(0x0C, 0);
            if (storedSum != SaveChecksum(saveData))
                throw new YugiohException("invalid savegame checksum detected.");

            if (!ReadMainDeck())
                return false;

            ReadCardUnlocks();

            return true;
        }

        bool ReadMainDeck()
        {
            MainDeck = new List<ushort>();

            if(IsPlus)
            {
                IO.SeekTo(0xCA);
                var deckCount = IO.In.ReadInt16();
                IO.Position += 4;
                for (int i = 0; i < deckCount; i++)
                {
                    MainDeck.Add(IO.In.ReadUInt16());
                }
            }
            else
            {
                IO.SeekTo(0xC6);
                var deckCount = IO.In.ReadInt16();
                IO.Position += 4;
                for (int i = 0; i < deckCount; i++)
                {
                    MainDeck.Add(IO.In.ReadUInt16());
                }
            }
            return true;
        }

        void ReadCardUnlocks()
        {
            UnlockedCards = new List<CardListEntry>();
            IO.SeekTo(IsPlus ? 0x20F0 : 0x1138);
            for (int i = 0; i < (IsPlus ? 0xF3C : 0x3FD); i++)
            {
                UnlockedCards.Add(new CardListEntry(IO.In.ReadInt32()));
            }
        }

        uint SaveChecksum(byte[] data)
        {
            return Checksum.CRC32.CalculateAlt(data);
        }

        public void Save()
        {
            // write the card list table
            IO.SeekTo(IsPlus ? 0x20F0 : 0x1138);
            foreach (var card in UnlockedCards)
            {
                IO.Out.Write(card.ToArray());
            }
            // generate and write the new checksum
            IO.Out.SeekNWrite(0x0C, 0);
            var saveData = IO.ToArray();
            IO.Out.SeekNWrite(0x0C, SaveChecksum(saveData));
        }
    }
    internal class YugiohException : Exception
    {
        internal YugiohException(string message)
            : base("Yu-Gi-Oh 5D's Decade Duels:" + message)
        {

        }
    }

    /*class Save
    {
        uint magic; // the save magic, 0x58410A03
        int size; // the save size, should always be 0x219D
        uint checksum; // the checksum.  this is a CRC32 except it's not not'd at the end.  get it, not not'd?
        public Card[] cards;
        public Card[] deck;
        public Card[] sideDeck;
        //public string deckName;
        public uint[] cardsUnlocked;

        // Structure for a card
        public struct Card
        {
            public ushort Id;
            public string Name;
        }

        public void LoadFile(ref EndianIO io)
        {
            io.Open();

            io.Stream.Position = 0; // Seek to 0 just to be safe

            // Do the header validation.  Because we can.
            magic = io.In.ReadUInt32();
            if (magic != 0)
                throw new Exception("Invalid Magic!");

            uint temp1 = io.In.ReadUInt32();
            if (temp1 != 0)
                throw new Exception("Unk Int1 is Invalid!");

            size = io.In.ReadInt32();
            if (size != 0)
                throw new Exception("Size is Invalid!");

            #region CardInfo
            cards = new Card[1018];
            cards[0].Id = 3902;
            cards[0].Name = "Sheep Token";
            cards[1].Id = 3903;
            cards[1].Name = "Sheep Token";
            cards[2].Id = 3904;
            cards[2].Name = "Sheep Token";
            cards[3].Id = 3905;
            cards[3].Name = "Sheep Token";
            cards[4].Id = 3914;
            cards[4].Name = "Metal Fiend Token";
            cards[5].Id = 3915;
            cards[5].Name = "Ojama Token";
            cards[6].Id = 3916;
            cards[6].Name = "Ojama Token";
            cards[7].Id = 3917;
            cards[7].Name = "Ojama Token";
            cards[8].Id = 3918;
            cards[8].Name = "Lamb Token";
            cards[9].Id = 3919;
            cards[9].Name = "Lamb Token";
            cards[10].Id = 3922;
            cards[10].Name = "Emissary of Darkness Token";
            cards[11].Id = 3925;
            cards[11].Name = "Fluff Token";
            cards[12].Id = 3926;
            cards[12].Name = "Fluff Token";
            cards[13].Id = 3936;
            cards[13].Name = "Grinder Token";
            cards[14].Id = 3940;
            cards[14].Name = "Doomsday Token";
            cards[15].Id = 3943;
            cards[15].Name = "Metabo Token";
            cards[16].Id = 3944;
            cards[16].Name = "Oyster Token";
            cards[17].Id = 3948;
            cards[17].Name = "Rose Token";
            cards[18].Id = 4007;
            cards[18].Name = "Blue-Eyes White Dragon";
            cards[19].Id = 4023;
            cards[19].Name = "Right Leg of the Forbidden One";
            cards[20].Id = 4024;
            cards[20].Name = "Left Leg of the Forbidden One";
            cards[21].Id = 4025;
            cards[21].Name = "Right Arm of the Forbidden One";
            cards[22].Id = 4026;
            cards[22].Name = "Left Arm of the Forbidden One";
            cards[23].Id = 4027;
            cards[23].Name = "Exodia the Forbidden One";
            cards[24].Id = 4028;
            cards[24].Name = "Summoned Skull";
            cards[25].Id = 4036;
            cards[25].Name = "Zombie Warrior";
            cards[26].Id = 4054;
            cards[26].Name = "Sangan";
            cards[27].Id = 4095;
            cards[27].Name = "Catapult Turtle";
            cards[28].Id = 4108;
            cards[28].Name = "Mask of Darkness";
            cards[29].Id = 4121;
            cards[29].Name = "Kamionwizard";
            cards[30].Id = 4174;
            cards[30].Name = "Darkfire Dragon";
            cards[31].Id = 4310;
            cards[31].Name = "Axe of Despair";
            cards[32].Id = 4352;
            cards[32].Name = "Ookazi";
            cards[33].Id = 4353;
            cards[33].Name = "Tremendous Fire";
            cards[34].Id = 4354;
            cards[34].Name = "Swords of Revealing Light";
            cards[35].Id = 4386;
            cards[35].Name = "Blue-Eyes Ultimate Dragon";
            cards[36].Id = 4421;
            cards[36].Name = "Mechanicalchaser";
            cards[37].Id = 4501;
            cards[37].Name = "Musician King";
            cards[38].Id = 4568;
            cards[38].Name = "Needle Worm";
            cards[39].Id = 4597;
            cards[39].Name = "Morphing Jar";
            cards[40].Id = 4608;
            cards[40].Name = "Penguin Soldier";
            cards[41].Id = 4657;
            cards[41].Name = "Kunai with Chain";
            cards[42].Id = 4663;
            cards[42].Name = "Megamorph";
            cards[43].Id = 4667;
            cards[43].Name = "Crush Card Virus";
            cards[44].Id = 4692;
            cards[44].Name = "Widespread Ruin";
            cards[45].Id = 4694;
            cards[45].Name = "Bad Reaction to Simochi";
            cards[46].Id = 4742;
            cards[46].Name = "Blast Sphere";
            cards[47].Id = 4749;
            cards[47].Name = "Barrel Dragon";
            cards[48].Id = 4758;
            cards[48].Name = "Jinzo";
            cards[49].Id = 4762;
            cards[49].Name = "Reflect Bounder";
            cards[50].Id = 4786;
            cards[50].Name = "Dunames Dark Witch";
            cards[51].Id = 4795;
            cards[51].Name = "Copycat";
            cards[52].Id = 4803;
            cards[52].Name = "Brain Control";
            cards[53].Id = 4804;
            cards[53].Name = "Negate Attack";
            cards[54].Id = 4817;
            cards[54].Name = "Mind Control";
            cards[55].Id = 4818;
            cards[55].Name = "Scapegoat";
            cards[56].Id = 4821;
            cards[56].Name = "Card Destruction";
            cards[57].Id = 4835;
            cards[57].Name = "Fissure";
            cards[58].Id = 4837;
            cards[58].Name = "Polymerization";
            cards[59].Id = 4842;
            cards[59].Name = "Monster Reborn";
            cards[60].Id = 4851;
            cards[60].Name = "Ultimate Offering";
            cards[61].Id = 4856;
            cards[61].Name = "Tribute to The Doomed";
            cards[62].Id = 4861;
            cards[62].Name = "Solemn Judgment";
            cards[63].Id = 4862;
            cards[63].Name = "Magic Jammer";
            cards[64].Id = 4863;
            cards[64].Name = "Seven Tools of the Bandit";
            cards[65].Id = 4865;
            cards[65].Name = "Just Desserts";
            cards[66].Id = 4866;
            cards[66].Name = "Royal Decree";
            cards[67].Id = 4876;
            cards[67].Name = "Robbin' Goblin";
            cards[68].Id = 4880;
            cards[68].Name = "Wall of Illusion";
            cards[69].Id = 4886;
            cards[69].Name = "Waboku";
            cards[70].Id = 4887;
            cards[70].Name = "Mirror Force";
            cards[71].Id = 4891;
            cards[71].Name = "Heavy Storm";
            cards[72].Id = 4893;
            cards[72].Name = "Gravekeeper's Servant";
            cards[73].Id = 4895;
            cards[73].Name = "Upstart Goblin";
            cards[74].Id = 4897;
            cards[74].Name = "Final Destiny";
            cards[75].Id = 4905;
            cards[75].Name = "Rush Recklessly";
            cards[76].Id = 4908;
            cards[76].Name = "Chain Energy";
            cards[77].Id = 4909;
            cards[77].Name = "Mystical Space Typhoon";
            cards[78].Id = 4910;
            cards[78].Name = "Giant Trunade";
            cards[79].Id = 4914;
            cards[79].Name = "Banisher of the Light";
            cards[80].Id = 4915;
            cards[80].Name = "Giant Rat";
            cards[81].Id = 4916;
            cards[81].Name = "Senju of the Thousand Hands";
            cards[82].Id = 4917;
            cards[82].Name = "UFO Turtle";
            cards[83].Id = 4921;
            cards[83].Name = "Giant Germ";
            cards[84].Id = 4922;
            cards[84].Name = "Nimble Momonga";
            cards[85].Id = 4923;
            cards[85].Name = "Spear Cretin";
            cards[86].Id = 4924;
            cards[86].Name = "Shining Angel";
            cards[87].Id = 4926;
            cards[87].Name = "Mother Grizzly";
            cards[88].Id = 4927;
            cards[88].Name = "Flying Kamakiri #1";
            cards[89].Id = 4929;
            cards[89].Name = "Sonic Bird";
            cards[90].Id = 4930;
            cards[90].Name = "Mystic Tomato";
            cards[91].Id = 4932;
            cards[91].Name = "Gaia Power";
            cards[92].Id = 4933;
            cards[92].Name = "Umiiruka";
            cards[93].Id = 4934;
            cards[93].Name = "Molten Destruction";
            cards[94].Id = 4935;
            cards[94].Name = "Rising Air Current";
            cards[95].Id = 4936;
            cards[95].Name = "Luminous Spark";
            cards[96].Id = 4937;
            cards[96].Name = "Mystic Plasma Zone";
            cards[97].Id = 4938;
            cards[97].Name = "Messenger of Peace";
            cards[98].Id = 4951;
            cards[98].Name = "DNA Surgery";
            cards[99].Id = 4956;
            cards[99].Name = "Ceasefire";
            cards[100].Id = 4957;
            cards[100].Name = "Light of Intervention";
            cards[101].Id = 4962;
            cards[101].Name = "Magical Hats";
            cards[102].Id = 4963;
            cards[102].Name = "Nobleman of Crossout";
            cards[103].Id = 4965;
            cards[103].Name = "The Shallow Grave";
            cards[104].Id = 4968;
            cards[104].Name = "Prohibition";
            cards[105].Id = 4969;
            cards[105].Name = "Morphing Jar #2";
            cards[106].Id = 4988;
            cards[106].Name = "Dust Tornado";
            cards[107].Id = 4993;
            cards[107].Name = "Mirror Wall";
            cards[108].Id = 4998;
            cards[108].Name = "Obelisk the Tormentor";
            cards[109].Id = 5004;
            cards[109].Name = "Vorse Raider";
            cards[110].Id = 5008;
            cards[110].Name = "Anti-Spell Fragrance";
            cards[111].Id = 5009;
            cards[111].Name = "Riryoku";
            cards[112].Id = 5016;
            cards[112].Name = "King Tiger Wanghu";
            cards[113].Id = 5017;
            cards[113].Name = "Command Knight";
            cards[114].Id = 5021;
            cards[114].Name = "Birdface";
            cards[115].Id = 5023;
            cards[115].Name = "Airknight Parshath";
            cards[116].Id = 5031;
            cards[116].Name = "Injection Fairy Lily";
            cards[117].Id = 5099;
            cards[117].Name = "Soul Exchange";
            cards[118].Id = 5106;
            cards[118].Name = "Mask of Restrict";
            cards[119].Id = 5113;
            cards[119].Name = "Fairy Box";
            cards[120].Id = 5114;
            cards[120].Name = "Torrential Tribute";
            cards[121].Id = 5118;
            cards[121].Name = "De-Fusion";
            cards[122].Id = 5120;
            cards[122].Name = "Nightmare's Steelcage";
            cards[123].Id = 5123;
            cards[123].Name = "Card of Safe Return";
            cards[124].Id = 5124;
            cards[124].Name = "Magic Cylinder";
            cards[125].Id = 5125;
            cards[125].Name = "Solemn Wishes";
            cards[126].Id = 5127;
            cards[126].Name = "Cold Wave";
            cards[127].Id = 5129;
            cards[127].Name = "Limiter Removal";
            cards[128].Id = 5133;
            cards[128].Name = "Magic Drain";
            cards[129].Id = 5134;
            cards[129].Name = "Gravity Bind";
            cards[130].Id = 5145;
            cards[130].Name = "Goblin Attack Force";
            cards[131].Id = 5162;
            cards[131].Name = "Creature Swap";
            cards[132].Id = 5168;
            cards[132].Name = "Mystic Box";
            cards[133].Id = 5170;
            cards[133].Name = "Ground Collapse";
            cards[134].Id = 5197;
            cards[134].Name = "Fire Princess";
            cards[135].Id = 5201;
            cards[135].Name = "Dancing Fairy";
            cards[136].Id = 5204;
            cards[136].Name = "Cure Mermaid";
            cards[137].Id = 5210;
            cards[137].Name = "Jar of Greed";
            cards[138].Id = 5212;
            cards[138].Name = "United We Stand";
            cards[139].Id = 5213;
            cards[139].Name = "Mage Power";
            cards[140].Id = 5214;
            cards[140].Name = "Offerings to the Doomed";
            cards[141].Id = 5217;
            cards[141].Name = "Lightning Vortex";
            cards[142].Id = 5236;
            cards[142].Name = "Foolish Burial";
            cards[143].Id = 5248;
            cards[143].Name = "Kycoo the Ghost Destroyer";
            cards[144].Id = 5250;
            cards[144].Name = "Bazoo the Soul-Eater";
            cards[145].Id = 5251;
            cards[145].Name = "Soul of Purity and Light";
            cards[146].Id = 5252;
            cards[146].Name = "Spirit of Flames";
            cards[147].Id = 5253;
            cards[147].Name = "Aqua Spirit";
            cards[148].Id = 5254;
            cards[148].Name = "The Rock Spirit";
            cards[149].Id = 5255;
            cards[149].Name = "Garuda the Wind Spirit";
            cards[150].Id = 5256;
            cards[150].Name = "Gilasaurus";
            cards[151].Id = 5276;
            cards[151].Name = "Fusion Gate";
            cards[152].Id = 5290;
            cards[152].Name = "Spell Shattering Arrow";
            cards[153].Id = 5298;
            cards[153].Name = "Nightmare Wheel";
            cards[154].Id = 5301;
            cards[154].Name = "Dark Ruler Ha Des";
            cards[155].Id = 5318;
            cards[155].Name = "Marauding Captain";
            cards[156].Id = 5323;
            cards[156].Name = "Exiled Force";
            cards[157].Id = 5327;
            cards[157].Name = "The A. Forces";
            cards[158].Id = 5328;
            cards[158].Name = "Reinforcement of the Army";
            cards[159].Id = 5330;
            cards[159].Name = "The Warrior Returning Alive";
            cards[160].Id = 5343;
            cards[160].Name = "A Wingbeat of Giant Dragon";
            cards[161].Id = 5345;
            cards[161].Name = "Stamping Destruction";
            cards[162].Id = 5347;
            cards[162].Name = "Dragon's Rage";
            cards[163].Id = 5350;
            cards[163].Name = "Emergency Provisions";
            cards[164].Id = 5352;
            cards[164].Name = "Dragged Down into the Grave";
            cards[165].Id = 5355;
            cards[165].Name = "Shrink";
            cards[166].Id = 5361;
            cards[166].Name = "Kelbek";
            cards[167].Id = 5367;
            cards[167].Name = "Silent Doom";
            cards[168].Id = 5374;
            cards[168].Name = "Inaba White Rabbit";
            cards[169].Id = 5380;
            cards[169].Name = "Hino-Kagu-Tsuchi";
            cards[170].Id = 5381;
            cards[170].Name = "Asura Priest";
            cards[171].Id = 5386;
            cards[171].Name = "Heart of Clear Water";
            cards[172].Id = 5387;
            cards[172].Name = "A Legendary Ocean";
            cards[173].Id = 5391;
            cards[173].Name = "Second Coin Toss";
            cards[174].Id = 5399;
            cards[174].Name = "Royal Oppression";
            cards[175].Id = 5400;
            cards[175].Name = "Bottomless Trap Hole";
            cards[176].Id = 5401;
            cards[176].Name = "Ominous Fortunetelling";
            cards[177].Id = 5409;
            cards[177].Name = "Kaiser Sea Horse";
            cards[178].Id = 5410;
            cards[178].Name = "Vampire Lord";
            cards[179].Id = 5412;
            cards[179].Name = "Sasuke Samurai";
            cards[180].Id = 5414;
            cards[180].Name = "Dark Dust Spirit";
            cards[181].Id = 5418;
            cards[181].Name = "Swarm of Scarabs";
            cards[182].Id = 5419;
            cards[182].Name = "Swarm of Locusts";
            cards[183].Id = 5423;
            cards[183].Name = "Pyramid Turtle";
            cards[184].Id = 5426;
            cards[184].Name = "Don Zaloog";
            cards[185].Id = 5427;
            cards[185].Name = "Des Lacooda";
            cards[186].Id = 5430;
            cards[186].Name = "Book of Life";
            cards[187].Id = 5431;
            cards[187].Name = "Book of Taiyou";
            cards[188].Id = 5432;
            cards[188].Name = "Book of Moon";
            cards[189].Id = 5435;
            cards[189].Name = "Call of the Mummy";
            cards[190].Id = 5439;
            cards[190].Name = "Ordeal of a Traveler";
            cards[191].Id = 5442;
            cards[191].Name = "Needle Ceiling";
            cards[192].Id = 5446;
            cards[192].Name = "Trap Dustshoot";
            cards[193].Id = 5448;
            cards[193].Name = "Reckless Greed";
            cards[194].Id = 5452;
            cards[194].Name = "Exarion Universe";
            cards[195].Id = 5474;
            cards[195].Name = "Toon Table of Contents";
            cards[196].Id = 5476;
            cards[196].Name = "Toon Gemini Elf";
            cards[197].Id = 5477;
            cards[197].Name = "Toon Cannon Soldier";
            cards[198].Id = 5482;
            cards[198].Name = "Puppet Master";
            cards[199].Id = 5485;
            cards[199].Name = "Lord Poison";
            cards[200].Id = 5486;
            cards[200].Name = "Spell of Pain";
            cards[201].Id = 5496;
            cards[201].Name = "Lava Golem";
            cards[202].Id = 5498;
            cards[202].Name = "Machine Duplication";
            cards[203].Id = 5502;
            cards[203].Name = "Five-Headed Dragon";
            cards[204].Id = 5505;
            cards[204].Name = "Enemy Controller";
            cards[205].Id = 5509;
            cards[205].Name = "Gravekeeper's Spy";
            cards[206].Id = 5526;
            cards[206].Name = "Spirit Reaper";
            cards[207].Id = 5528;
            cards[207].Name = "Reaper on the Nightmare";
            cards[208].Id = 5531;
            cards[208].Name = "Dark Room of Nightmare";
            cards[209].Id = 5533;
            cards[209].Name = "Necrovalley";
            cards[210].Id = 5537;
            cards[210].Name = "Terraforming";
            cards[211].Id = 5540;
            cards[211].Name = "Royal Tribute";
            cards[212].Id = 5543;
            cards[212].Name = "Barrel Behind the Door";
            cards[213].Id = 5544;
            cards[213].Name = "Raigeki Break";
            cards[214].Id = 5560;
            cards[214].Name = "Interdimensional Matter Transporter";
            cards[215].Id = 5561;
            cards[215].Name = "Goblin Zombie";
            cards[216].Id = 5586;
            cards[216].Name = "Giant Orc";
            cards[217].Id = 5607;
            cards[217].Name = "Poison of the Old Man";
            cards[218].Id = 5609;
            cards[218].Name = "Dark Core";
            cards[219].Id = 5611;
            cards[219].Name = "Metalsilver Armor";
            cards[220].Id = 5614;
            cards[220].Name = "Wave-Motion Cannon";
            cards[221].Id = 5620;
            cards[221].Name = "Secret Barrel";
            cards[222].Id = 5622;
            cards[222].Name = "Rivalry of Warlords";
            cards[223].Id = 5627;
            cards[223].Name = "Final Attack Orders";
            cards[224].Id = 5630;
            cards[224].Name = "Spell Absorption";
            cards[225].Id = 5631;
            cards[225].Name = "Diffusion Wave-Motion";
            cards[226].Id = 5632;
            cards[226].Name = "Fiend's Sanctuary";
            cards[227].Id = 5649;
            cards[227].Name = "Skilled Dark Magician";
            cards[228].Id = 5650;
            cards[228].Name = "Apprentice Magician";
            cards[229].Id = 5651;
            cards[229].Name = "Old Vindictive Magician";
            cards[230].Id = 5653;
            cards[230].Name = "Magical Marionette";
            cards[231].Id = 5655;
            cards[231].Name = "Breaker the Magical Warrior";
            cards[232].Id = 5658;
            cards[232].Name = "Royal Magical Library";
            cards[233].Id = 5661;
            cards[233].Name = "Des Koala";
            cards[234].Id = 5663;
            cards[234].Name = "Magical Merchant";
            cards[235].Id = 5669;
            cards[235].Name = "Big Bang Shot";
            cards[236].Id = 5671;
            cards[236].Name = "Mass Driver";
            cards[237].Id = 5675;
            cards[237].Name = "My Body as a Shield";
            cards[238].Id = 5677;
            cards[238].Name = "Mega Ton Magical Cannon";
            cards[239].Id = 5686;
            cards[239].Name = "Metal Reflect Slime";
            cards[240].Id = 5688;
            cards[240].Name = "Magical Stone Excavation";
            cards[241].Id = 5719;
            cards[241].Name = "D.D. Warrior Lady";
            cards[242].Id = 5725;
            cards[242].Name = "Shooting Star Bow - Ceal";
            cards[243].Id = 5729;
            cards[243].Name = "Twin Swords of Flashing Light - Tryce";
            cards[244].Id = 5735;
            cards[244].Name = "Non-Spellcasting Area";
            cards[245].Id = 5738;
            cards[245].Name = "Ojama Trio";
            cards[246].Id = 5740;
            cards[246].Name = "Skill Drain";
            cards[247].Id = 5743;
            cards[247].Name = "Soul Taker";
            cards[248].Id = 5752;
            cards[248].Name = "Magical Dimension";
            cards[249].Id = 5760;
            cards[249].Name = "D.D. Trainer";
            cards[250].Id = 5762;
            cards[250].Name = "Archfiend Soldier";
            cards[251].Id = 5766;
            cards[251].Name = "Dark Scorpion - Meanae the Thorn";
            cards[252].Id = 5767;
            cards[252].Name = "Outstanding Dog Marron";
            cards[253].Id = 5768;
            cards[253].Name = "Great Maju Garzett";
            cards[254].Id = 5769;
            cards[254].Name = "Iron Blacksmith Kotetsu";
            cards[255].Id = 5782;
            cards[255].Name = "Dark Master - Zorc";
            cards[256].Id = 5784;
            cards[256].Name = "Contract with the Abyss";
            cards[257].Id = 5785;
            cards[257].Name = "Contract with the Dark Master";
            cards[258].Id = 5788;
            cards[258].Name = "Final Countdown";
            cards[259].Id = 5799;
            cards[259].Name = "Sakuretsu Armor";
            cards[260].Id = 5803;
            cards[260].Name = "Nightmare Penguin";
            cards[261].Id = 5810;
            cards[261].Name = "Chiron the Mage";
            cards[262].Id = 5817;
            cards[262].Name = "Strike Ninja";
            cards[263].Id = 5822;
            cards[263].Name = "D.D. Scout Plane";
            cards[264].Id = 5823;
            cards[264].Name = "Berserk Gorilla";
            cards[265].Id = 5824;
            cards[265].Name = "Freed the Brave Wanderer";
            cards[266].Id = 5827;
            cards[266].Name = "Chaos Necromancer";
            cards[267].Id = 5829;
            cards[267].Name = "Inferno";
            cards[268].Id = 5830;
            cards[268].Name = "Fenrir";
            cards[269].Id = 5831;
            cards[269].Name = "Gigantes";
            cards[270].Id = 5832;
            cards[270].Name = "Silpheed";
            cards[271].Id = 5833;
            cards[271].Name = "Chaos Sorcerer";
            cards[272].Id = 5834;
            cards[272].Name = "Gren Maju Da Eiza";
            cards[273].Id = 5837;
            cards[273].Name = "Heart of the Underdog";
            cards[274].Id = 5838;
            cards[274].Name = "Wild Nature's Release";
            cards[275].Id = 5849;
            cards[275].Name = "Reload";
            cards[276].Id = 5850;
            cards[276].Name = "Soul Absorption";
            cards[277].Id = 5853;
            cards[277].Name = "Cursed Seal of the Forbidden Spell";
            cards[278].Id = 5855;
            cards[278].Name = "Spatial Collapse";
            cards[279].Id = 5857;
            cards[279].Name = "Zero Gravity";
            cards[280].Id = 5869;
            cards[280].Name = "Magician's Valkyria";
            cards[281].Id = 5871;
            cards[281].Name = "Giga Gagagigo";
            cards[282].Id = 5872;
            cards[282].Name = "Mad Dog of Darkness";
            cards[283].Id = 5873;
            cards[283].Name = "Neo Bug";
            cards[284].Id = 5881;
            cards[284].Name = "Manticore of Darkness";
            cards[285].Id = 5882;
            cards[285].Name = "Stealth Bird";
            cards[286].Id = 5884;
            cards[286].Name = "Enraged Battle Ox";
            cards[287].Id = 5888;
            cards[287].Name = "Hyper Hammerhead";
            cards[288].Id = 5893;
            cards[288].Name = "Amphibious Bugroth MK-3";
            cards[289].Id = 5895;
            cards[289].Name = "Levia-Dragon - Daedalus";
            cards[290].Id = 5898;
            cards[290].Name = "Mataza the Zapper";
            cards[291].Id = 5900;
            cards[291].Name = "Manju of the Ten Thousand Hands";
            cards[292].Id = 5904;
            cards[292].Name = "Stray Lambs";
            cards[293].Id = 5905;
            cards[293].Name = "Smashing Ground";
            cards[294].Id = 5908;
            cards[294].Name = "Salvage";
            cards[295].Id = 5914;
            cards[295].Name = "Compulsory Evacuation Device";
            cards[296].Id = 5915;
            cards[296].Name = "A Hero Emerges";
            cards[297].Id = 5918;
            cards[297].Name = "Begone, Knave!";
            cards[298].Id = 5919;
            cards[298].Name = "DNA Transplant";
            cards[299].Id = 5921;
            cards[299].Name = "Trap Jammer";
            cards[300].Id = 5927;
            cards[300].Name = "Abyss Soldier";
            cards[301].Id = 5932;
            cards[301].Name = "D.D. Assailant";
            cards[302].Id = 5935;
            cards[302].Name = "Zoma the Spirit";
            cards[303].Id = 5951;
            cards[303].Name = "The Agent of Judgment - Saturn";
            cards[304].Id = 5956;
            cards[304].Name = "Soul-Absorbing Bone Tower";
            cards[305].Id = 5961;
            cards[305].Name = "Legendary Jujitsu Master";
            cards[306].Id = 5964;
            cards[306].Name = "Blowback Dragon";
            cards[307].Id = 5965;
            cards[307].Name = "Zaborg the Thunder Monarch";
            cards[308].Id = 5972;
            cards[308].Name = "Lady Ninja Yae";
            cards[309].Id = 5974;
            cards[309].Name = "Solar Flare Dragon";
            cards[310].Id = 5975;
            cards[310].Name = "White Magician Pikeru";
            cards[311].Id = 5982;
            cards[311].Name = "The Sanctuary in the Sky";
            cards[312].Id = 5990;
            cards[312].Name = "Wall of Revealing Light";
            cards[313].Id = 5993;
            cards[313].Name = "Beckoning Light";
            cards[314].Id = 5994;
            cards[314].Name = "Draining Shield";
            cards[315].Id = 6000;
            cards[315].Name = "Marshmallon";
            cards[316].Id = 6001;
            cards[316].Name = "Doomcaliber Knight";
            cards[317].Id = 6003;
            cards[317].Name = "Shield Crush";
            cards[318].Id = 6006;
            cards[318].Name = "Legacy of Yata-Garasu";
            cards[319].Id = 6040;
            cards[319].Name = "Double Coston";
            cards[320].Id = 6042;
            cards[320].Name = "Night Assailant";
            cards[321].Id = 6044;
            cards[321].Name = "King of the Swamp";
            cards[322].Id = 6054;
            cards[322].Name = "Level Limit - Area B";
            cards[323].Id = 6073;
            cards[323].Name = "The End of Anubis";
            cards[324].Id = 6075;
            cards[324].Name = "Necroface";
            cards[325].Id = 6078;
            cards[325].Name = "Return from the Different Dimension";
            cards[326].Id = 6093;
            cards[326].Name = "Charcoal Inpachi";
            cards[327].Id = 6098;
            cards[327].Name = "Horus the Black Flame Dragon LV4";
            cards[328].Id = 6099;
            cards[328].Name = "Horus the Black Flame Dragon LV6";
            cards[329].Id = 6100;
            cards[329].Name = "Horus the Black Flame Dragon LV8";
            cards[330].Id = 6103;
            cards[330].Name = "Mystic Swordsman LV2";
            cards[331].Id = 6105;
            cards[331].Name = "Armed Dragon LV3";
            cards[332].Id = 6106;
            cards[332].Name = "Armed Dragon LV5";
            cards[333].Id = 6107;
            cards[333].Name = "Armed Dragon LV7";
            cards[334].Id = 6111;
            cards[334].Name = "Ninja Grandmaster Sasuke";
            cards[335].Id = 6114;
            cards[335].Name = "Mobius the Frost Monarch";
            cards[336].Id = 6117;
            cards[336].Name = "Howling Insect";
            cards[337].Id = 6118;
            cards[337].Name = "Masked Dragon";
            cards[338].Id = 6120;
            cards[338].Name = "Unshaven Angler";
            cards[339].Id = 6121;
            cards[339].Name = "The Trojan Horse";
            cards[340].Id = 6129;
            cards[340].Name = "Dark Factory of Mass Production";
            cards[341].Id = 6133;
            cards[341].Name = "Level Up!";
            cards[342].Id = 6136;
            cards[342].Name = "Two-Man Cell Battle";
            cards[343].Id = 6137;
            cards[343].Name = "Big Wave Small Wave";
            cards[344].Id = 6142;
            cards[344].Name = "Spirit Barrier";
            cards[345].Id = 6147;
            cards[345].Name = "Mind Crush";
            cards[346].Id = 6151;
            cards[346].Name = "Green Gadget";
            cards[347].Id = 6155;
            cards[347].Name = "Red Gadget";
            cards[348].Id = 6156;
            cards[348].Name = "Yellow Gadget";
            cards[349].Id = 6161;
            cards[349].Name = "Gold Sarcophagus";
            cards[350].Id = 6168;
            cards[350].Name = "Magician's Circle";
            cards[351].Id = 6178;
            cards[351].Name = "Ultimate Insect LV3";
            cards[352].Id = 6185;
            cards[352].Name = "Sasuke Samurai #4";
            cards[353].Id = 6186;
            cards[353].Name = "Harpie Lady 1";
            cards[354].Id = 6189;
            cards[354].Name = "Raging Flame Sprite";
            cards[355].Id = 6190;
            cards[355].Name = "Thestalos the Firestorm Monarch";
            cards[356].Id = 6197;
            cards[356].Name = "Gaia Soul the Combustible Collective";
            cards[357].Id = 6198;
            cards[357].Name = "Fox Fire";
            cards[358].Id = 6200;
            cards[358].Name = "Fusilier Dragon, the Dual-Mode Beast";
            cards[359].Id = 6201;
            cards[359].Name = "Dekoichi the Battlechanted Locomotive";
            cards[360].Id = 6207;
            cards[360].Name = "Harpies' Hunting Ground";
            cards[361].Id = 6213;
            cards[361].Name = "Monster Reincarnation";
            cards[362].Id = 6217;
            cards[362].Name = "Divine Wrath";
            cards[363].Id = 6218;
            cards[363].Name = "Xing Zhen Hu";
            cards[364].Id = 6219;
            cards[364].Name = "Rare Metalmorph";
            cards[365].Id = 6233;
            cards[365].Name = "Divine Dragon Ragnarok";
            cards[366].Id = 6235;
            cards[366].Name = "Insect Knight";
            cards[367].Id = 6236;
            cards[367].Name = "Sacred Phoenix of Nephthys";
            cards[368].Id = 6237;
            cards[368].Name = "Hand of Nephthys";
            cards[369].Id = 6238;
            cards[369].Name = "Ultimate Insect LV5";
            cards[370].Id = 6239;
            cards[370].Name = "Granmarg the Rock Monarch";
            cards[371].Id = 6244;
            cards[371].Name = "Behemoth the King of All Animals";
            cards[372].Id = 6252;
            cards[372].Name = "Armed Samurai - Ben Kei";
            cards[373].Id = 6254;
            cards[373].Name = "Golem Sentry";
            cards[374].Id = 6256;
            cards[374].Name = "The Light - Hex-Sealed Fusion";
            cards[375].Id = 6257;
            cards[375].Name = "The Dark - Hex-Sealed Fusion";
            cards[376].Id = 6259;
            cards[376].Name = "Whirlwind Prodigy";
            cards[377].Id = 6260;
            cards[377].Name = "Flame Ruler";
            cards[378].Id = 6262;
            cards[378].Name = "Rescue Cat";
            cards[379].Id = 6264;
            cards[379].Name = "Gatling Dragon";
            cards[380].Id = 6265;
            cards[380].Name = "King Dragun";
            cards[381].Id = 6278;
            cards[381].Name = "Threatening Roar";
            cards[382].Id = 6279;
            cards[382].Name = "Phoenix Wing Wind Blast";
            cards[383].Id = 6280;
            cards[383].Name = "Good Goblin Housekeeping";
            cards[384].Id = 6281;
            cards[384].Name = "Beast Soul Swap";
            cards[385].Id = 6283;
            cards[385].Name = "D.D. Dynamite";
            cards[386].Id = 6284;
            cards[386].Name = "Deck Devastation Virus";
            cards[387].Id = 6287;
            cards[387].Name = "Vampire's Curse";
            cards[388].Id = 6291;
            cards[388].Name = "Overpowering Eye";
            cards[389].Id = 6298;
            cards[389].Name = "Kaibaman";
            cards[390].Id = 6310;
            cards[390].Name = "Elemental Hero Avian";
            cards[391].Id = 6311;
            cards[391].Name = "Elemental Hero Burstinatrix";
            cards[392].Id = 6312;
            cards[392].Name = "Elemental Hero Clayman";
            cards[393].Id = 6313;
            cards[393].Name = "Elemental Hero Sparkman";
            cards[394].Id = 6316;
            cards[394].Name = "Ancient Gear Beast";
            cards[395].Id = 6319;
            cards[395].Name = "Ultimate Insect LV7";
            cards[396].Id = 6332;
            cards[396].Name = "D.D. Survivor";
            cards[397].Id = 6339;
            cards[397].Name = "Batteryman AA";
            cards[398].Id = 6340;
            cards[398].Name = "Des Wombat";
            cards[399].Id = 6342;
            cards[399].Name = "Reshef the Dark Being";
            cards[400].Id = 6345;
            cards[400].Name = "Elemental Hero Thunder Giant";
            cards[401].Id = 6348;
            cards[401].Name = "Battery Charger";
            cards[402].Id = 6351;
            cards[402].Name = "Final Ritual of the Ancients";
            cards[403].Id = 6356;
            cards[403].Name = "Hero Signal";
            cards[404].Id = 6380;
            cards[404].Name = "Green Baboon, Defender of the Forest";
            cards[405].Id = 6386;
            cards[405].Name = "Steamroid";
            cards[406].Id = 6387;
            cards[406].Name = "Drillroid";
            cards[407].Id = 6390;
            cards[407].Name = "Cyber Dragon";
            cards[408].Id = 6396;
            cards[408].Name = "Cyber Twin Dragon";
            cards[409].Id = 6398;
            cards[409].Name = "Power Bond";
            cards[410].Id = 6399;
            cards[410].Name = "Skyscraper";
            cards[411].Id = 6400;
            cards[411].Name = "Summoner Monk";
            cards[412].Id = 6410;
            cards[412].Name = "Van'Dalgyon the Dark Dragon Lord";
            cards[413].Id = 6417;
            cards[413].Name = "Cyber Archfiend";
            cards[414].Id = 6418;
            cards[414].Name = "Goblin Elite Attack Force";
            cards[415].Id = 6421;
            cards[415].Name = "Indomitable Fighter Lei Lei";
            cards[416].Id = 6427;
            cards[416].Name = "Tyranno Infinity";
            cards[417].Id = 6429;
            cards[417].Name = "Ebon Magician Curran";
            cards[418].Id = 6430;
            cards[418].Name = "D.D.M. - Different Dimension Master";
            cards[419].Id = 6431;
            cards[419].Name = "Fusion Recovery";
            cards[420].Id = 6432;
            cards[420].Name = "Miracle Fusion";
            cards[421].Id = 6433;
            cards[421].Name = "Dragon's Mirror";
            cards[422].Id = 6438;
            cards[422].Name = "Fire Darts";
            cards[423].Id = 6439;
            cards[423].Name = "Spiritual Earth Art - Kurogane";
            cards[424].Id = 6440;
            cards[424].Name = "Spiritual Water Art - Aoi";
            cards[425].Id = 6441;
            cards[425].Name = "Spiritual Fire Art - Kurenai";
            cards[426].Id = 6442;
            cards[426].Name = "Spiritual Wind Art - Miyabi";
            cards[427].Id = 6445;
            cards[427].Name = "Rising Energy";
            cards[428].Id = 6448;
            cards[428].Name = "Dimension Wall";
            cards[429].Id = 6458;
            cards[429].Name = "Divine Sword - Phoenix Blade";
            cards[430].Id = 6467;
            cards[430].Name = "Elemental Hero Shining Flare Wingman";
            cards[431].Id = 6468;
            cards[431].Name = "Level Modulation";
            cards[432].Id = 6477;
            cards[432].Name = "Elemental Hero Bladedge";
            cards[433].Id = 6478;
            cards[433].Name = "Elemental Hero Wildheart";
            cards[434].Id = 6479;
            cards[434].Name = "Hydrogeddon";
            cards[435].Id = 6488;
            cards[435].Name = "Elemental Hero Wildedge";
            cards[436].Id = 6500;
            cards[436].Name = "Rapid-Fire Magician";
            cards[437].Id = 6501;
            cards[437].Name = "Beiige, Vanguard of Dark World";
            cards[438].Id = 6503;
            cards[438].Name = "Brron, Mad King of Dark World";
            cards[439].Id = 6504;
            cards[439].Name = "Sillva, Warlord of Dark World";
            cards[440].Id = 6505;
            cards[440].Name = "Goldd, Wu-Lord of Dark World";
            cards[441].Id = 6511;
            cards[441].Name = "Pot of Avarice";
            cards[442].Id = 6512;
            cards[442].Name = "Dark World Lightning";
            cards[443].Id = 6515;
            cards[443].Name = "Gateway to Dark World";
            cards[444].Id = 6516;
            cards[444].Name = "The Forces of Darkness";
            cards[445].Id = 6517;
            cards[445].Name = "Dark Deal";
            cards[446].Id = 6524;
            cards[446].Name = "Armed Changer";
            cards[447].Id = 6527;
            cards[447].Name = "Elemental Hero Necroshade";
            cards[448].Id = 6532;
            cards[448].Name = "Magical Blast";
            cards[449].Id = 6541;
            cards[449].Name = "Magical Mallet";
            cards[450].Id = 6542;
            cards[450].Name = "Inferno Reckless Summon";
            cards[451].Id = 6546;
            cards[451].Name = "Mist Body";
            cards[452].Id = 6547;
            cards[452].Name = "Axe Dragonute";
            cards[453].Id = 6558;
            cards[453].Name = "Gorz the Emissary of Darkness";
            cards[454].Id = 6582;
            cards[454].Name = "Damage Condenser";
            cards[455].Id = 6588;
            cards[455].Name = "Proto-Cyber Dragon";
            cards[456].Id = 6599;
            cards[456].Name = "Chainsaw Insect";
            cards[457].Id = 6600;
            cards[457].Name = "Anteatereatingant";
            cards[458].Id = 6602;
            cards[458].Name = "Doom Dozer";
            cards[459].Id = 6603;
            cards[459].Name = "Treeborn Frog";
            cards[460].Id = 6612;
            cards[460].Name = "Ruin, Queen of Oblivion";
            cards[461].Id = 6613;
            cards[461].Name = "Demise, King of Armageddon";
            cards[462].Id = 6617;
            cards[462].Name = "End of the World";
            cards[463].Id = 6643;
            cards[463].Name = "Elemental Hero Wild Wingman";
            cards[464].Id = 6644;
            cards[464].Name = "Elemental Hero Necroid Shaman";
            cards[465].Id = 6653;
            cards[465].Name = "Elemental Hero Neos";
            cards[466].Id = 6654;
            cards[466].Name = "Dandylion";
            cards[467].Id = 6660;
            cards[467].Name = "Destiny Hero - Diamond Dude";
            cards[468].Id = 6663;
            cards[468].Name = "Cyber Phoenix";
            cards[469].Id = 6669;
            cards[469].Name = "Misfortune";
            cards[470].Id = 6670;
            cards[470].Name = "Grand Convergence";
            cards[471].Id = 6671;
            cards[471].Name = "H - Heated Heart";
            cards[472].Id = 6672;
            cards[472].Name = "E - Emergency Call";
            cards[473].Id = 6673;
            cards[473].Name = "R - Righteous Justice";
            cards[474].Id = 6674;
            cards[474].Name = "O - Oversoul";
            cards[475].Id = 6682;
            cards[475].Name = "Macro Cosmos";
            cards[476].Id = 6688;
            cards[476].Name = "Majestic Mech - Ohka";
            cards[477].Id = 6689;
            cards[477].Name = "Majestic Mech - Goryu";
            cards[478].Id = 6690;
            cards[478].Name = "Bountiful Artemis";
            cards[479].Id = 6692;
            cards[479].Name = "Herald of Purple Light";
            cards[480].Id = 6693;
            cards[480].Name = "Herald of Green Light";
            cards[481].Id = 6695;
            cards[481].Name = "Banisher of the Radiance";
            cards[482].Id = 6696;
            cards[482].Name = "Voltanis the Adjudicator";
            cards[483].Id = 6703;
            cards[483].Name = "Batteryman D";
            cards[484].Id = 6705;
            cards[484].Name = "Celestial Transformation";
            cards[485].Id = 6707;
            cards[485].Name = "Dimensional Fissure";
            cards[486].Id = 6709;
            cards[486].Name = "Icarus Attack";
            cards[487].Id = 6710;
            cards[487].Name = "Miraculous Descent";
            cards[488].Id = 6712;
            cards[488].Name = "Forced Back";
            cards[489].Id = 6722;
            cards[489].Name = "Hysteric Party";
            cards[490].Id = 6728;
            cards[490].Name = "Elemental Hero Woodsman";
            cards[491].Id = 6731;
            cards[491].Name = "Elemental Hero Terra Firma";
            cards[492].Id = 6733;
            cards[492].Name = "Elemental Hero Ocean";
            cards[493].Id = 6734;
            cards[493].Name = "Chimeratech Overdragon";
            cards[494].Id = 6738;
            cards[494].Name = "Submarineroid";
            cards[495].Id = 6746;
            cards[495].Name = "No Entry!!";
            cards[496].Id = 6747;
            cards[496].Name = "Supercharge";
            cards[497].Id = 6753;
            cards[497].Name = "Destiny Hero - Dasher";
            cards[498].Id = 6755;
            cards[498].Name = "Destiny Hero - Defender";
            cards[499].Id = 6757;
            cards[499].Name = "Destiny Hero - Dogma";
            cards[500].Id = 6759;
            cards[500].Name = "Mausoleum of the Emperor";
            cards[501].Id = 6769;
            cards[501].Name = "Neo-Spacian Air Hummingbird";
            cards[502].Id = 6770;
            cards[502].Name = "Neo-Spacian Grand Mole";
            cards[503].Id = 6772;
            cards[503].Name = "Neo-Spacian Dark Panther";
            cards[504].Id = 6775;
            cards[504].Name = "Neo Space";
            cards[505].Id = 6776;
            cards[505].Name = "Black Stego";
            cards[506].Id = 6777;
            cards[506].Name = "Ultimate Tyranno";
            cards[507].Id = 6782;
            cards[507].Name = "Future Fusion";
            cards[508].Id = 6783;
            cards[508].Name = "Overload Fusion";
            cards[509].Id = 6784;
            cards[509].Name = "Elemental Hero Stratos";
            cards[510].Id = 6798;
            cards[510].Name = "Black Ptera";
            cards[511].Id = 6800;
            cards[511].Name = "Alien Grey";
            cards[512].Id = 6804;
            cards[512].Name = "Alien Warrior";
            cards[513].Id = 6811;
            cards[513].Name = "Fossil Excavation";
            cards[514].Id = 6816;
            cards[514].Name = "Super Conductor Tyranno";
            cards[515].Id = 6817;
            cards[515].Name = "Hunting Instinct";
            cards[516].Id = 6818;
            cards[516].Name = "Survival Instinct";
            cards[517].Id = 6819;
            cards[517].Name = "Big Evolution Pill";
            cards[518].Id = 6821;
            cards[518].Name = "Light and Darkness Dragon";
            cards[519].Id = 6822;
            cards[519].Name = "Tail Swipe";
            cards[520].Id = 6823;
            cards[520].Name = "Jurassic World";
            cards[521].Id = 6879;
            cards[521].Name = "Snipe Hunter";
            cards[522].Id = 6881;
            cards[522].Name = "Vanity's Fiend";
            cards[523].Id = 6901;
            cards[523].Name = "Instant Fusion";
            cards[524].Id = 6904;
            cards[524].Name = "Chain Strike";
            cards[525].Id = 6908;
            cards[525].Name = "Degenerate Circuit";
            cards[526].Id = 6912;
            cards[526].Name = "Justi-Break";
            cards[527].Id = 6916;
            cards[527].Name = "Accumulated Fortune";
            cards[528].Id = 6918;
            cards[528].Name = "Black Horn of Heaven";
            cards[529].Id = 6927;
            cards[529].Name = "Elemental Hero Heat";
            cards[530].Id = 6932;
            cards[530].Name = "Ancient Gear Gadjiltron Dragon";
            cards[531].Id = 6939;
            cards[531].Name = "Card Trooper";
            cards[532].Id = 6941;
            cards[532].Name = "Burial from a Different Dimension";
            cards[533].Id = 6949;
            cards[533].Name = "Destiny Hero - Malicious";
            cards[534].Id = 6950;
            cards[534].Name = "Destiny Draw";
            cards[535].Id = 6960;
            cards[535].Name = "Gene-Warped Warwolf";
            cards[536].Id = 6961;
            cards[536].Name = "Frostosaurus";
            cards[537].Id = 6963;
            cards[537].Name = "The Six Samurai - Yariza";
            cards[538].Id = 6964;
            cards[538].Name = "The Six Samurai - Zanji";
            cards[539].Id = 6965;
            cards[539].Name = "The Six Samurai - Nisashi";
            cards[540].Id = 6966;
            cards[540].Name = "The Six Samurai - Yaichi";
            cards[541].Id = 6967;
            cards[541].Name = "The Six Samurai - Kamon";
            cards[542].Id = 6968;
            cards[542].Name = "The Six Samurai - Irou";
            cards[543].Id = 6969;
            cards[543].Name = "Great Shogun Shien";
            cards[544].Id = 6974;
            cards[544].Name = "Kahkki, Guerilla of Dark World";
            cards[545].Id = 6975;
            cards[545].Name = "Gren, Tactician of Dark World";
            cards[546].Id = 6980;
            cards[546].Name = "D.D. Crow";
            cards[547].Id = 6989;
            cards[547].Name = "Elemental Hero Air Neos";
            cards[548].Id = 6990;
            cards[548].Name = "Elemental Hero Grand Neos";
            cards[549].Id = 6994;
            cards[549].Name = "A Cell Scatter Burst";
            cards[550].Id = 6995;
            cards[550].Name = "Ancient Rules";
            cards[551].Id = 6996;
            cards[551].Name = "Advanced Ritual Art";
            cards[552].Id = 6999;
            cards[552].Name = "Twister";
            cards[553].Id = 7000;
            cards[553].Name = "Legendary Ebon Steed";
            cards[554].Id = 7002;
            cards[554].Name = "Dark World Dealings";
            cards[555].Id = 7004;
            cards[555].Name = "Skyscraper 2 - Hero City";
            cards[556].Id = 7007;
            cards[556].Name = "Return of the Six Samurai";
            cards[557].Id = 7010;
            cards[557].Name = "The Transmigration Prophecy";
            cards[558].Id = 7013;
            cards[558].Name = "Birthright";
            cards[559].Id = 7015;
            cards[559].Name = "Cloak and Dagger";
            cards[560].Id = 7017;
            cards[560].Name = "Blizzard Dragon";
            cards[561].Id = 7035;
            cards[561].Name = "Angel O7";
            cards[562].Id = 7038;
            cards[562].Name = "Hunter Owl";
            cards[563].Id = 7046;
            cards[563].Name = "Neo-Parshath, the Sky Paladin";
            cards[564].Id = 7047;
            cards[564].Name = "Meltiel, Sage of the Sky";
            cards[565].Id = 7048;
            cards[565].Name = "Harvest Angel of Wisdom";
            cards[566].Id = 7050;
            cards[566].Name = "Nova Summoner";
            cards[567].Id = 7052;
            cards[567].Name = "Gellenduo";
            cards[568].Id = 7058;
            cards[568].Name = "Elemental Hero Voltic";
            cards[569].Id = 7071;
            cards[569].Name = "Crystal Beast Amethyst Cat";
            cards[570].Id = 7072;
            cards[570].Name = "Crystal Beast Amber Mammoth";
            cards[571].Id = 7073;
            cards[571].Name = "Crystal Beacon";
            cards[572].Id = 7074;
            cards[572].Name = "Crystal Beast Ruby Carbuncle";
            cards[573].Id = 7075;
            cards[573].Name = "Crystal Beast Emerald Tortoise";
            cards[574].Id = 7076;
            cards[574].Name = "Crystal Beast Topaz Tiger";
            cards[575].Id = 7077;
            cards[575].Name = "Crystal Beast Cobalt Eagle";
            cards[576].Id = 7078;
            cards[576].Name = "Crystal Beast Sapphire Pegasus";
            cards[577].Id = 7079;
            cards[577].Name = "Ancient City - Rainbow Ruins";
            cards[578].Id = 7080;
            cards[578].Name = "Rare Value";
            cards[579].Id = 7082;
            cards[579].Name = "Volcanic Shell";
            cards[580].Id = 7083;
            cards[580].Name = "Volcanic Scattershot";
            cards[581].Id = 7086;
            cards[581].Name = "Blaze Accelerator";
            cards[582].Id = 7093;
            cards[582].Name = "Spell Striker";
            cards[583].Id = 7094;
            cards[583].Name = "Exploder Dragon";
            cards[584].Id = 7096;
            cards[584].Name = "Destiny Hero - Plasma";
            cards[585].Id = 7103;
            cards[585].Name = "Volcanic Slicer";
            cards[586].Id = 7104;
            cards[586].Name = "Volcanic Hammerer";
            cards[587].Id = 7105;
            cards[587].Name = "Elemental Hero Captain Gold";
            cards[588].Id = 7107;
            cards[588].Name = "Warrior of Atlantis";
            cards[589].Id = 7108;
            cards[589].Name = "Destroyersaurus";
            cards[590].Id = 7109;
            cards[590].Name = "Zeradias, Herald of Heaven";
            cards[591].Id = 7111;
            cards[591].Name = "Harpie Queen";
            cards[592].Id = 7112;
            cards[592].Name = "Sky Scourge Enrise";
            cards[593].Id = 7113;
            cards[593].Name = "Sky Scourge Norleras";
            cards[594].Id = 7114;
            cards[594].Name = "Sky Scourge Invicil";
            cards[595].Id = 7117;
            cards[595].Name = "Raiza the Storm Monarch";
            cards[596].Id = 7122;
            cards[596].Name = "Crystal Blessing";
            cards[597].Id = 7123;
            cards[597].Name = "Crystal Abundance";
            cards[598].Id = 7124;
            cards[598].Name = "Crystal Promise";
            cards[599].Id = 7127;
            cards[599].Name = "Field Barrier";
            cards[600].Id = 7128;
            cards[600].Name = "A Cell Breeding Device";
            cards[601].Id = 7130;
            cards[601].Name = "Crystal Raigeki";
            cards[602].Id = 7134;
            cards[602].Name = "Backs to the Wall";
            cards[603].Id = 7143;
            cards[603].Name = "Botanical Lion";
            cards[604].Id = 7145;
            cards[604].Name = "Grandmaster of the Six Samurai";
            cards[605].Id = 7151;
            cards[605].Name = "Recurring Nightmare";
            cards[606].Id = 7152;
            cards[606].Name = "Sword of Dark Rites";
            cards[607].Id = 7153;
            cards[607].Name = "Eradicator Epidemic Virus";
            cards[608].Id = 7166;
            cards[608].Name = "Snake Rain";
            cards[609].Id = 7171;
            cards[609].Name = "Rainbow Dragon";
            cards[610].Id = 7176;
            cards[610].Name = "Elemental Hero Darkbright";
            cards[611].Id = 7178;
            cards[611].Name = "Necro Gardna";
            cards[612].Id = 7192;
            cards[612].Name = "Renge, Gatekeeper of Dark World";
            cards[613].Id = 7195;
            cards[613].Name = "Elemental Hero Neos Alius";
            cards[614].Id = 7202;
            cards[614].Name = "Doom Shaman";
            cards[615].Id = 7203;
            cards[615].Name = "King Pyron";
            cards[616].Id = 7208;
            cards[616].Name = "Crystal Seer";
            cards[617].Id = 7209;
            cards[617].Name = "Neo Space Pathfinder";
            cards[618].Id = 7210;
            cards[618].Name = "Frost and Flame Dragon";
            cards[619].Id = 7211;
            cards[619].Name = "Desert Twister";
            cards[620].Id = 7215;
            cards[620].Name = "Zombie Master";
            cards[621].Id = 7220;
            cards[621].Name = "Summoner's Art";
            cards[622].Id = 7221;
            cards[622].Name = "Creature Seizure";
            cards[623].Id = 7223;
            cards[623].Name = "Symbols of Duty";
            cards[624].Id = 7224;
            cards[624].Name = "Amulet of Ambition";
            cards[625].Id = 7228;
            cards[625].Name = "Common Charity";
            cards[626].Id = 7234;
            cards[626].Name = "Gift Card";
            cards[627].Id = 7243;
            cards[627].Name = "Volcanic Rocket";
            cards[628].Id = 7246;
            cards[628].Name = "Herald of Creation";
            cards[629].Id = 7247;
            cards[629].Name = "Decoy Dragon";
            cards[630].Id = 7248;
            cards[630].Name = "Trade-In";
            cards[631].Id = 7252;
            cards[631].Name = "Elemental Hero Plasma Vice";
            cards[632].Id = 7254;
            cards[632].Name = "Over Limit";
            cards[633].Id = 7256;
            cards[633].Name = "Swing of Memories";
            cards[634].Id = 7257;
            cards[634].Name = "Evil Hero Inferno Wing";
            cards[635].Id = 7258;
            cards[635].Name = "Evil Hero Malicious Edge";
            cards[636].Id = 7259;
            cards[636].Name = "Evil Hero Infernal Gainer";
            cards[637].Id = 7260;
            cards[637].Name = "Evil Hero Lightning Golem";
            cards[638].Id = 7261;
            cards[638].Name = "Evil Hero Dark Gaia";
            cards[639].Id = 7262;
            cards[639].Name = "Dark Fusion";
            cards[640].Id = 7284;
            cards[640].Name = "Gladiator Beast Murmillo";
            cards[641].Id = 7285;
            cards[641].Name = "Gladiator Beast Bestiari";
            cards[642].Id = 7286;
            cards[642].Name = "Gladiator Beast Laquari";
            cards[643].Id = 7287;
            cards[643].Name = "Gladiator Beast Hoplomus";
            cards[644].Id = 7289;
            cards[644].Name = "Gladiator Beast Secutor";
            cards[645].Id = 7297;
            cards[645].Name = "Enishi, Shien's Chancellor";
            cards[646].Id = 7298;
            cards[646].Name = "Spirit of the Six Samurai";
            cards[647].Id = 7299;
            cards[647].Name = "Alien Telepath";
            cards[648].Id = 7300;
            cards[648].Name = "Alien Hypno";
            cards[649].Id = 7303;
            cards[649].Name = "Gladiator Beast Heraklinos";
            cards[650].Id = 7310;
            cards[650].Name = "Gladiator Beast's Battle Manica";
            cards[651].Id = 7315;
            cards[651].Name = "A Cell Incubator";
            cards[652].Id = 7317;
            cards[652].Name = "Release from Stone";
            cards[653].Id = 7318;
            cards[653].Name = "Light-Imprisoning Mirror";
            cards[654].Id = 7319;
            cards[654].Name = "Shadow-Imprisoning Mirror";
            cards[655].Id = 7323;
            cards[655].Name = "Double-Edged Sword Technique";
            cards[656].Id = 7336;
            cards[656].Name = "Abyssal Kingshark";
            cards[657].Id = 7346;
            cards[657].Name = "Beast King Barbaros";
            cards[658].Id = 7347;
            cards[658].Name = "Splendid Venus";
            cards[659].Id = 7349;
            cards[659].Name = "Dark Bribe";
            cards[660].Id = 7351;
            cards[660].Name = "Phantom of Chaos";
            cards[661].Id = 7352;
            cards[661].Name = "Nurse Reficule the Fallen One";
            cards[662].Id = 7359;
            cards[662].Name = "Mezuki";
            cards[663].Id = 7376;
            cards[663].Name = "Evil Hero Infernal Prodigy";
            cards[664].Id = 7378;
            cards[664].Name = "Evil Hero Wild Cyclone";
            cards[665].Id = 7379;
            cards[665].Name = "Evil Hero Infernal Sniper";
            cards[666].Id = 7380;
            cards[666].Name = "Evil Hero Malicious Fiend";
            cards[667].Id = 7381;
            cards[667].Name = "Dark Calling";
            cards[668].Id = 7385;
            cards[668].Name = "Grave Squirmer";
            cards[669].Id = 7386;
            cards[669].Name = "Grinder Golem";
            cards[670].Id = 7388;
            cards[670].Name = "Crystal Pair";
            cards[671].Id = 7389;
            cards[671].Name = "Hand Destruction";
            cards[672].Id = 7390;
            cards[672].Name = "Crystal Release";
            cards[673].Id = 7392;
            cards[673].Name = "Exodius the Ultimate Forbidden Lord";
            cards[674].Id = 7397;
            cards[674].Name = "Thunder King Rai-Oh";
            cards[675].Id = 7399;
            cards[675].Name = "Burden of the Mighty";
            cards[676].Id = 7400;
            cards[676].Name = "Dimensional Prison";
            cards[677].Id = 7403;
            cards[677].Name = "Chimeratech Fortress Dragon";
            cards[678].Id = 7404;
            cards[678].Name = "Fog King";
            cards[679].Id = 7405;
            cards[679].Name = "Fossil Dyna Pachycephalo";
            cards[680].Id = 7409;
            cards[680].Name = "Yubel";
            cards[681].Id = 7410;
            cards[681].Name = "Yubel - Terror Incarnate";
            cards[682].Id = 7411;
            cards[682].Name = "Yubel - The Ultimate Nightmare";
            cards[683].Id = 7413;
            cards[683].Name = "Cyber Valley";
            cards[684].Id = 7415;
            cards[684].Name = "Volcanic Counter";
            cards[685].Id = 7419;
            cards[685].Name = "The Dark Creator";
            cards[686].Id = 7421;
            cards[686].Name = "Dark Armed Dragon";
            cards[687].Id = 7422;
            cards[687].Name = "Dark Crusader";
            cards[688].Id = 7423;
            cards[688].Name = "Armageddon Knight";
            cards[689].Id = 7424;
            cards[689].Name = "Doomsday Horror";
            cards[690].Id = 7425;
            cards[690].Name = "Obsidian Dragon";
            cards[691].Id = 7426;
            cards[691].Name = "Shadowpriestess of Ohm";
            cards[692].Id = 7427;
            cards[692].Name = "Gigaplant";
            cards[693].Id = 7430;
            cards[693].Name = "The Immortal Bushi";
            cards[694].Id = 7432;
            cards[694].Name = "Gladiator Beast Darius";
            cards[695].Id = 7434;
            cards[695].Name = "Superancient Deepsea King Coelacanth";
            cards[696].Id = 7436;
            cards[696].Name = "The Calculator";
            cards[697].Id = 7445;
            cards[697].Name = "Super Polymerization";
            cards[698].Id = 7447;
            cards[698].Name = "Instant Neo Space";
            cards[699].Id = 7453;
            cards[699].Name = "Dark Eruption";
            cards[700].Id = 7454;
            cards[700].Name = "Fires of Doomsday";
            cards[701].Id = 7456;
            cards[701].Name = "Chain Summoning";
            cards[702].Id = 7459;
            cards[702].Name = "Gladiator Beast's Battle Archfiend Shield";
            cards[703].Id = 7460;
            cards[703].Name = "Gladiator Proving Ground";
            cards[704].Id = 7463;
            cards[704].Name = "Rainbow Life";
            cards[705].Id = 7466;
            cards[705].Name = "Chain Material";
            cards[706].Id = 7471;
            cards[706].Name = "Escape from the Dark Dimension";
            cards[707].Id = 7473;
            cards[707].Name = "Drastic Drop Off";
            cards[708].Id = 7474;
            cards[708].Name = "All-Out Attacks";
            cards[709].Id = 7476;
            cards[709].Name = "Offering to the Snake Deity";
            cards[710].Id = 7477;
            cards[710].Name = "Cry Havoc!";
            cards[711].Id = 7482;
            cards[711].Name = "Test Tiger";
            cards[712].Id = 7487;
            cards[712].Name = "Royal Firestorm Guards";
            cards[713].Id = 7488;
            cards[713].Name = "Veil of Darkness";
            cards[714].Id = 7489;
            cards[714].Name = "Security Orb";
            cards[715].Id = 7490;
            cards[715].Name = "Fire Trooper";
            cards[716].Id = 7491;
            cards[716].Name = "Elemental Hero Prisma";
            cards[717].Id = 7493;
            cards[717].Name = "Hidden Armory";
            cards[718].Id = 7516;
            cards[718].Name = "Caius the Shadow Monarch";
            cards[719].Id = 7517;
            cards[719].Name = "Dimensional Alchemist";
            cards[720].Id = 7519;
            cards[720].Name = "D.D.R. - Different Dimension Reincarnation";
            cards[721].Id = 7520;
            cards[721].Name = "By Order of the Emperor";
            cards[722].Id = 7522;
            cards[722].Name = "Evil Dragon Ananta";
            cards[723].Id = 7557;
            cards[723].Name = "Red-Eyes Darkness Metal Dragon";
            cards[724].Id = 7558;
            cards[724].Name = "Gaia Plate the Earth Giant";
            cards[725].Id = 7560;
            cards[725].Name = "Tethys, Goddess of Light";
            cards[726].Id = 7561;
            cards[726].Name = "Dark Grepher";
            cards[727].Id = 7562;
            cards[727].Name = "Darklord Zerato";
            cards[728].Id = 7565;
            cards[728].Name = "Metabo Globster";
            cards[729].Id = 7566;
            cards[729].Name = "Golden Flying Fish";
            cards[730].Id = 7567;
            cards[730].Name = "Prime Material Dragon";
            cards[731].Id = 7568;
            cards[731].Name = "Lonefire Blossom";
            cards[732].Id = 7570;
            cards[732].Name = "Allure of Darkness";
            cards[733].Id = 7571;
            cards[733].Name = "Athena";
            cards[734].Id = 7572;
            cards[734].Name = "Hecatrice";
            cards[735].Id = 7573;
            cards[735].Name = "Valhalla, Hall of the Fallen";
            cards[736].Id = 7574;
            cards[736].Name = "Honest";
            cards[737].Id = 7581;
            cards[737].Name = "Arcana Force 0 - The Fool";
            cards[738].Id = 7591;
            cards[738].Name = "Jain, Lightsworn Paladin";
            cards[739].Id = 7592;
            cards[739].Name = "Lyla, Lightsworn Sorceress";
            cards[740].Id = 7593;
            cards[740].Name = "Garoth, Lightsworn Warrior";
            cards[741].Id = 7594;
            cards[741].Name = "Lumina, Lightsworn Summoner";
            cards[742].Id = 7595;
            cards[742].Name = "Ryko, Lightsworn Hunter";
            cards[743].Id = 7596;
            cards[743].Name = "Wulf, Lightsworn Beast";
            cards[744].Id = 7597;
            cards[744].Name = "Celestia, Lightsworn Angel";
            cards[745].Id = 7598;
            cards[745].Name = "Gragonith, Lightsworn Dragon";
            cards[746].Id = 7599;
            cards[746].Name = "Judgment Dragon";
            cards[747].Id = 7600;
            cards[747].Name = "Dark Valkyria";
            cards[748].Id = 7601;
            cards[748].Name = "Substitoad";
            cards[749].Id = 7602;
            cards[749].Name = "Unifrog";
            cards[750].Id = 7603;
            cards[750].Name = "Batteryman Charger";
            cards[751].Id = 7604;
            cards[751].Name = "Batteryman Industrial Strength";
            cards[752].Id = 7605;
            cards[752].Name = "Batteryman Micro-Cell";
            cards[753].Id = 7615;
            cards[753].Name = "Destiny End Dragoon";
            cards[754].Id = 7617;
            cards[754].Name = "Gladiator Beast Gyzarus";
            cards[755].Id = 7625;
            cards[755].Name = "Solar Recharge";
            cards[756].Id = 7627;
            cards[756].Name = "Wetlands";
            cards[757].Id = 7628;
            cards[757].Name = "Quick Charger";
            cards[758].Id = 7629;
            cards[758].Name = "Short Circuit";
            cards[759].Id = 7634;
            cards[759].Name = "Ribbon of Rebirth";
            cards[760].Id = 7636;
            cards[760].Name = "Limit Reverse";
            cards[761].Id = 7644;
            cards[761].Name = "Glorious Illusion";
            cards[762].Id = 7646;
            cards[762].Name = "Froggy Forcefield";
            cards[763].Id = 7647;
            cards[763].Name = "Portable Battery Pack";
            cards[764].Id = 7652;
            cards[764].Name = "Summon Limit";
            cards[765].Id = 7658;
            cards[765].Name = "Emperor Sem";
            cards[766].Id = 7659;
            cards[766].Name = "Kuraz the Light Monarch";
            cards[767].Id = 7661;
            cards[767].Name = "Hanewata";
            cards[768].Id = 7663;
            cards[768].Name = "Oyster Meister";
            cards[769].Id = 7666;
            cards[769].Name = "Gigantic Cephalotus";
            cards[770].Id = 7667;
            cards[770].Name = "Nettles";
            cards[771].Id = 7668;
            cards[771].Name = "Queen of Thorns";
            cards[772].Id = 7671;
            cards[772].Name = "Miracle Fertilizer";
            cards[773].Id = 7673;
            cards[773].Name = "Herald of Orange Light";
            cards[774].Id = 7675;
            cards[774].Name = "Twin-Barrel Dragon";
            cards[775].Id = 7683;
            cards[775].Name = "Geartown";
            cards[776].Id = 7687;
            cards[776].Name = "Junk Synchron";
            cards[777].Id = 7688;
            cards[777].Name = "Speed Warrior";
            cards[778].Id = 7689;
            cards[778].Name = "Magna Drago";
            cards[779].Id = 7690;
            cards[779].Name = "Frequency Magician";
            cards[780].Id = 7694;
            cards[780].Name = "Scrap-Iron Scarecrow";
            cards[781].Id = 7696;
            cards[781].Name = "Junk Warrior";
            cards[782].Id = 7697;
            cards[782].Name = "Gaia Knight, the Force of Earth";
            cards[783].Id = 7698;
            cards[783].Name = "Colossal Fighter";
            cards[784].Id = 7700;
            cards[784].Name = "Nitro Synchron";
            cards[785].Id = 7701;
            cards[785].Name = "Quillbolt Hedgehog";
            cards[786].Id = 7707;
            cards[786].Name = "Sinister Sprocket";
            cards[787].Id = 7708;
            cards[787].Name = "Dark Resonator";
            cards[788].Id = 7710;
            cards[788].Name = "Jutte Fighter";
            cards[789].Id = 7711;
            cards[789].Name = "Handcuffs Dragon";
            cards[790].Id = 7712;
            cards[790].Name = "Montage Dragon";
            cards[791].Id = 7713;
            cards[791].Name = "Gonogo";
            cards[792].Id = 7714;
            cards[792].Name = "Mind Master";
            cards[793].Id = 7716;
            cards[793].Name = "Krebons";
            cards[794].Id = 7717;
            cards[794].Name = "Mind Protector";
            cards[795].Id = 7718;
            cards[795].Name = "Psychic Commander";
            cards[796].Id = 7719;
            cards[796].Name = "Psychic Snail";
            cards[797].Id = 7721;
            cards[797].Name = "Destructotron";
            cards[798].Id = 7722;
            cards[798].Name = "Gladiator Beast Equeste";
            cards[799].Id = 7733;
            cards[799].Name = "Nitro Warrior";
            cards[800].Id = 7734;
            cards[800].Name = "Stardust Dragon";
            cards[801].Id = 7735;
            cards[801].Name = "Red Dragon Archfiend";
            cards[802].Id = 7736;
            cards[802].Name = "Goyo Guardian";
            cards[803].Id = 7737;
            cards[803].Name = "Magical Android";
            cards[804].Id = 7738;
            cards[804].Name = "Thought Ruler Archfiend";
            cards[805].Id = 7742;
            cards[805].Name = "Battle Tuned";
            cards[806].Id = 7743;
            cards[806].Name = "De-Synchro";
            cards[807].Id = 7747;
            cards[807].Name = "Emergency Teleport";
            cards[808].Id = 7750;
            cards[808].Name = "Unstable Evolution";
            cards[809].Id = 7751;
            cards[809].Name = "Recycling Batteries";
            cards[810].Id = 7752;
            cards[810].Name = "Book of Eclipse";
            cards[811].Id = 7754;
            cards[811].Name = "Graceful Revival";
            cards[812].Id = 7755;
            cards[812].Name = "Defense Draw";
            cards[813].Id = 7763;
            cards[813].Name = "Gladiator Beast War Chariot";
            cards[814].Id = 7766;
            cards[814].Name = "Judgment of Thunder";
            cards[815].Id = 7768;
            cards[815].Name = "Fish Depth Charge";
            cards[816].Id = 7770;
            cards[816].Name = "Dark Simorgh";
            cards[817].Id = 7782;
            cards[817].Name = "Divine Fowl King Alector";
            cards[818].Id = 7787;
            cards[818].Name = "Power Filter";
            cards[819].Id = 7788;
            cards[819].Name = "Dupe Frog";
            cards[820].Id = 7789;
            cards[820].Name = "Psychic Overload";
            cards[821].Id = 7790;
            cards[821].Name = "Beast Machine King Barbaros Ür";
            cards[822].Id = 7795;
            cards[822].Name = "Guardian of Order";
            cards[823].Id = 7797;
            cards[823].Name = "Ehren, Lightsworn Monk";
            cards[824].Id = 7799;
            cards[824].Name = "Magical Exemplar";
            cards[825].Id = 7806;
            cards[825].Name = "Elemental Hero Divine Neos";
            cards[826].Id = 7844;
            cards[826].Name = "Tree Otter";
            cards[827].Id = 7848;
            cards[827].Name = "Doomkaiser Dragon";
            cards[828].Id = 7849;
            cards[828].Name = "Plaguespreader Zombie";
            cards[829].Id = 7850;
            cards[829].Name = "The White Stone of Legend";
            cards[830].Id = 7851;
            cards[830].Name = "Revived King Ha Des";
            cards[831].Id = 7853;
            cards[831].Name = "Hand of the Six Samurai";
            cards[832].Id = 7854;
            cards[832].Name = "Red-Eyes Zombie Dragon";
            cards[833].Id = 7855;
            cards[833].Name = "Malevolent Mech - Goku En";
            cards[834].Id = 7856;
            cards[834].Name = "Paladin of the Cursed Dragon";
            cards[835].Id = 7857;
            cards[835].Name = "Zombie World";
            cards[836].Id = 7858;
            cards[836].Name = "Imperial Iron Wall";
            cards[837].Id = 7864;
            cards[837].Name = "Avenging Knight Parshath";
            cards[838].Id = 7865;
            cards[838].Name = "Counselor Lily";
            cards[839].Id = 7867;
            cards[839].Name = "Telekinetic Charging Cell";
            cards[840].Id = 7868;
            cards[840].Name = "Charge of the Light Brigade";
            cards[841].Id = 7870;
            cards[841].Name = "Turbo Synchron";
            cards[842].Id = 7871;
            cards[842].Name = "Mad Archfiend";
            cards[843].Id = 7873;
            cards[843].Name = "Copy Plant";
            cards[844].Id = 7874;
            cards[844].Name = "Morphtronic Celfon";
            cards[845].Id = 7877;
            cards[845].Name = "Morphtronic Boomboxen";
            cards[846].Id = 7878;
            cards[846].Name = "Morphtronic Cameran";
            cards[847].Id = 7879;
            cards[847].Name = "Morphtronic Radion";
            cards[848].Id = 7883;
            cards[848].Name = "Search Striker";
            cards[849].Id = 7890;
            cards[849].Name = "Storm Caller";
            cards[850].Id = 7891;
            cards[850].Name = "Psychic Jumper";
            cards[851].Id = 7893;
            cards[851].Name = "Tytannial, Princess of Camellias";
            cards[852].Id = 7897;
            cards[852].Name = "Turbo Warrior";
            cards[853].Id = 7898;
            cards[853].Name = "Black Rose Dragon";
            cards[854].Id = 7899;
            cards[854].Name = "Iron Chain Dragon";
            cards[855].Id = 7900;
            cards[855].Name = "Psychic Lifetrancer";
            cards[856].Id = 7903;
            cards[856].Name = "Mark of the Rose";
            cards[857].Id = 7904;
            cards[857].Name = "Black Garden";
            cards[858].Id = 7906;
            cards[858].Name = "Morphtronic Accelerator";
            cards[859].Id = 7911;
            cards[859].Name = "Teleport";
            cards[860].Id = 7912;
            cards[860].Name = "Psychokinesis";
            cards[861].Id = 7914;
            cards[861].Name = "The World Tree";
            cards[862].Id = 7916;
            cards[862].Name = "Secret Village of the Spellcasters";
            cards[863].Id = 7920;
            cards[863].Name = "Urgent Tuning";
            cards[864].Id = 7922;
            cards[864].Name = "Prideful Roar";
            cards[865].Id = 7928;
            cards[865].Name = "Psychic Trigger";
            cards[866].Id = 7934;
            cards[866].Name = "Gozen Match";
            cards[867].Id = 7937;
            cards[867].Name = "Kasha";
            cards[868].Id = 7943;
            cards[868].Name = "Flip Flop Frog";
            cards[869].Id = 7982;
            cards[869].Name = "Red-Eyes Wyvern";
            cards[870].Id = 7986;
            cards[870].Name = "Tuningware";
            cards[871].Id = 7987;
            cards[871].Name = "Armory Arm";
            cards[872].Id = 7990;
            cards[872].Name = "Shield Wing";
            cards[873].Id = 7991;
            cards[873].Name = "Vice Dragon";
            cards[874].Id = 7998;
            cards[874].Name = "Summon Reactor ・SK";
            cards[875].Id = 7999;
            cards[875].Name = "Flying Fortress SKY FIRE";
            cards[876].Id = 8000;
            cards[876].Name = "Trap Reactor ・Y FI";
            cards[877].Id = 8001;
            cards[877].Name = "Black Salvo";
            cards[878].Id = 8002;
            cards[878].Name = "Spell Reactor ・RE";
            cards[879].Id = 8004;
            cards[879].Name = "Turret Warrior";
            cards[880].Id = 8005;
            cards[880].Name = "Debris Dragon";
            cards[881].Id = 8006;
            cards[881].Name = "Hyper Synchron";
            cards[882].Id = 8008;
            cards[882].Name = "Trap Eater";
            cards[883].Id = 8009;
            cards[883].Name = "Twin-Sword Marauder";
            cards[884].Id = 8010;
            cards[884].Name = "Dark Tinker";
            cards[885].Id = 8011;
            cards[885].Name = "Blackwing - Gale the Whirlwind";
            cards[886].Id = 8012;
            cards[886].Name = "Blackwing - Bora the Spear";
            cards[887].Id = 8013;
            cards[887].Name = "Blackwing - Sirocco the Dawn";
            cards[888].Id = 8014;
            cards[888].Name = "Twilight Rose Knight";
            cards[889].Id = 8015;
            cards[889].Name = "Morphtronic Boarden";
            cards[890].Id = 8016;
            cards[890].Name = "Morphtronic Slingen";
            cards[891].Id = 8024;
            cards[891].Name = "Lifeforce Harmonizer";
            cards[892].Id = 8025;
            cards[892].Name = "Gladiator Beast Samnite";
            cards[893].Id = 8028;
            cards[893].Name = "Dimension Fortress Weapon";
            cards[894].Id = 8033;
            cards[894].Name = "Alien Overlord";
            cards[895].Id = 8034;
            cards[895].Name = "Alien Ammonite";
            cards[896].Id = 8035;
            cards[896].Name = "Dark Strike Fighter";
            cards[897].Id = 8036;
            cards[897].Name = "Blackwing Armor Master";
            cards[898].Id = 8037;
            cards[898].Name = "Hyper Psychic Blaster";
            cards[899].Id = 8038;
            cards[899].Name = "Arcanite Magician";
            cards[900].Id = 8039;
            cards[900].Name = "Cosmic Fortress Gol'gar";
            cards[901].Id = 8041;
            cards[901].Name = "Vengeful Servant";
            cards[902].Id = 8042;
            cards[902].Name = "Star Blast";
            cards[903].Id = 8045;
            cards[903].Name = "Morphtronic Map";
            cards[904].Id = 8050;
            cards[904].Name = "Telekinetic Power Well";
            cards[905].Id = 8051;
            cards[905].Name = "Indomitable Gladiator Beast";
            cards[906].Id = 8053;
            cards[906].Name = "Super Solar Nutrient";
            cards[907].Id = 8054;
            cards[907].Name = "Six Scrolls of the Samurai";
            cards[908].Id = 8055;
            cards[908].Name = "Verdant Sanctuary";
            cards[909].Id = 8057;
            cards[909].Name = "Mysterious Triangle";
            cards[910].Id = 8064;
            cards[910].Name = "Ebon Arrow";
            cards[911].Id = 8066;
            cards[911].Name = "Fake Explosion";
            cards[912].Id = 8071;
            cards[912].Name = "Psychic Tuning";
            cards[913].Id = 8074;
            cards[913].Name = "Wall of Thorns";
            cards[914].Id = 8075;
            cards[914].Name = "Planet Pollutant Virus";
            cards[915].Id = 8080;
            cards[915].Name = "Overdrive Teleporter";
            cards[916].Id = 8082;
            cards[916].Name = "Rai-Mei";
            cards[917].Id = 8085;
            cards[917].Name = "Tempest Magician";
            cards[918].Id = 8088;
            cards[918].Name = "Violet Witch";
            cards[919].Id = 8089;
            cards[919].Name = "Tragoedia";
            cards[920].Id = 8090;
            cards[920].Name = "Ancient Fairy Dragon";
            cards[921].Id = 8122;
            cards[921].Name = "Elemental Hero Gaia";
            cards[922].Id = 8129;
            cards[922].Name = "Elemental Hero Absolute Zero";
            cards[923].Id = 8132;
            cards[923].Name = "Endymion, the Master Magician";
            cards[924].Id = 8133;
            cards[924].Name = "Disenchanter";
            cards[925].Id = 8134;
            cards[925].Name = "Defender, the Magical Knight";
            cards[926].Id = 8135;
            cards[926].Name = "Magical Citadel of Endymion";
            cards[927].Id = 8136;
            cards[927].Name = "Spell Power Grasp";
            cards[928].Id = 8137;
            cards[928].Name = "Power Tool Dragon";
            cards[929].Id = 8139;
            cards[929].Name = "Dark Voltanis";
            cards[930].Id = 8142;
            cards[930].Name = "Alien Kid";
            cards[931].Id = 8143;
            cards[931].Name = "Totem Dragon";
            cards[932].Id = 8144;
            cards[932].Name = "Royal Swamp Eel";
            cards[933].Id = 8146;
            cards[933].Name = "Code A Ancient Ruins";
            cards[934].Id = 8156;
            cards[934].Name = "Strong Wind Dragon";
            cards[935].Id = 8157;
            cards[935].Name = "Dark Verger";
            cards[936].Id = 8161;
            cards[936].Name = "Evil Thorn";
            cards[937].Id = 8162;
            cards[937].Name = "Blackwing - Blizzard the Far North";
            cards[938].Id = 8163;
            cards[938].Name = "Blackwing - Shura the Blue Flame";
            cards[939].Id = 8164;
            cards[939].Name = "Blackwing - Kalut the Moon Shadow";
            cards[940].Id = 8165;
            cards[940].Name = "Blackwing - Elphin the Raven";
            cards[941].Id = 8166;
            cards[941].Name = "Morphtronic Remoten";
            cards[942].Id = 8167;
            cards[942].Name = "Morphtronic Videon";
            cards[943].Id = 8168;
            cards[943].Name = "Morphtronic Scopen";
            cards[944].Id = 8169;
            cards[944].Name = "Gadget Arms";
            cards[945].Id = 8170;
            cards[945].Name = "Torapart";
            cards[946].Id = 8171;
            cards[946].Name = "Earthbound Immortal Aslla piscu";
            cards[947].Id = 8172;
            cards[947].Name = "Earthbound Immortal Ccapac Apu";
            cards[948].Id = 8175;
            cards[948].Name = "Koa'ki Meiru Guardian";
            cards[949].Id = 8177;
            cards[949].Name = "Koa'ki Meiru Ice";
            cards[950].Id = 8181;
            cards[950].Name = "Reinforced Human Psychic Borg";
            cards[951].Id = 8182;
            cards[951].Name = "Master Gig";
            cards[952].Id = 8183;
            cards[952].Name = "Emissary from Pandemonium";
            cards[953].Id = 8185;
            cards[953].Name = "Alien Dog";
            cards[954].Id = 8186;
            cards[954].Name = "Spined Gillman";
            cards[955].Id = 8187;
            cards[955].Name = "Deep Sea Diva";
            cards[956].Id = 8188;
            cards[956].Name = "Mermaid Archer";
            cards[957].Id = 8191;
            cards[957].Name = "G.B. Hunter";
            cards[958].Id = 8192;
            cards[958].Name = "Exploder Dragonwing";
            cards[959].Id = 8193;
            cards[959].Name = "Blackwing Armed Wing";
            cards[960].Id = 8195;
            cards[960].Name = "Trident Dragion";
            cards[961].Id = 8196;
            cards[961].Name = "Sea Dragon Lord Gishilnodon";
            cards[962].Id = 8197;
            cards[962].Name = "One for One";
            cards[963].Id = 8199;
            cards[963].Name = "Thorn of Malice";
            cards[964].Id = 8202;
            cards[964].Name = "Against the Wind";
            cards[965].Id = 8203;
            cards[965].Name = "Black Whirlwind";
            cards[966].Id = 8204;
            cards[966].Name = "Junk Box";
            cards[967].Id = 8205;
            cards[967].Name = "Double Tool C&D";
            cards[968].Id = 8206;
            cards[968].Name = "Morphtronic Repair Unit";
            cards[969].Id = 8210;
            cards[969].Name = "Psychic Path";
            cards[970].Id = 8213;
            cards[970].Name = "Forbidden Chalice";
            cards[971].Id = 8218;
            cards[971].Name = "Overdoom Line";
            cards[972].Id = 8219;
            cards[972].Name = "Wicked Rebirth";
            cards[973].Id = 8220;
            cards[973].Name = "Delta Crow - Anti Reverse";
            cards[974].Id = 8222;
            cards[974].Name = "Fake Feather";
            cards[975].Id = 8223;
            cards[975].Name = "Trap Stun";
            cards[976].Id = 8224;
            cards[976].Name = "Morphtronic Bind";
            cards[977].Id = 8227;
            cards[977].Name = "Attack of the Cornered Rat";
            cards[978].Id = 8228;
            cards[978].Name = "Proof of Powerlessness";
            cards[979].Id = 8230;
            cards[979].Name = "Grave of the Super Ancient Organism";
            cards[980].Id = 8232;
            cards[980].Name = "Mirror of Oaths";
            cards[981].Id = 8263;
            cards[981].Name = "Snowman Eater";
            cards[982].Id = 8268;
            cards[982].Name = "Supersonic Skull Flame";
            cards[983].Id = 8269;
            cards[983].Name = "Skull Flame";
            cards[984].Id = 8270;
            cards[984].Name = "Burning Skull Head";
            cards[985].Id = 8271;
            cards[985].Name = "Skull Conductor";
            cards[986].Id = 8272;
            cards[986].Name = "Infernity Archfiend";
            cards[987].Id = 8273;
            cards[987].Name = "Infernity Dwarf";
            cards[988].Id = 8274;
            cards[988].Name = "Infernity Guardian";
            cards[989].Id = 8275;
            cards[989].Name = "Infernity Destroyer";
            cards[990].Id = 8280;
            cards[990].Name = "Powered Tuner";
            cards[991].Id = 8292;
            cards[991].Name = "Kuribon";
            cards[992].Id = 8293;
            cards[992].Name = "Sunny Pixie";
            cards[993].Id = 8294;
            cards[993].Name = "Sunlight Unicorn";
            cards[994].Id = 8295;
            cards[994].Name = "Blackwing - Mistral the Silver Shield";
            cards[995].Id = 8296;
            cards[995].Name = "Blackwing - Vayu the Emblem of Honor";
            cards[996].Id = 8297;
            cards[996].Name = "Blackwing - Fane the Steel Chain";
            cards[997].Id = 8303;
            cards[997].Name = "Infernity Beast";
            cards[998].Id = 8307;
            cards[998].Name = "Earthbound Immortal Cusillu";
            cards[999].Id = 8308;
            cards[999].Name = "Earthbound Immortal Chacu Challhua";
            cards[1000].Id = 8315;
            cards[1000].Name = "Shiny Black C";
            cards[1001].Id = 8318;
            cards[1001].Name = "Fishborg Blaster";
            cards[1002].Id = 8320;
            cards[1002].Name = "Armored Axon Kicker";
            cards[1003].Id = 8321;
            cards[1003].Name = "Genetic Woman";
            cards[1004].Id = 8329;
            cards[1004].Name = "Ancient Crimson Ape";
            cards[1005].Id = 8333;
            cards[1005].Name = "Ancient Sacred Wyvern";
            cards[1006].Id = 8335;
            cards[1006].Name = "Release Restraint Wave";
            cards[1007].Id = 8336;
            cards[1007].Name = "Silver Wing";
            cards[1008].Id = 8338;
            cards[1008].Name = "Ancient Forest";
            cards[1009].Id = 8345;
            cards[1009].Name = "Hydro Pressure Cannon";
            cards[1010].Id = 8346;
            cards[1010].Name = "Water Hazard";
            cards[1011].Id = 8347;
            cards[1011].Name = "Brain Research Lab";
            cards[1012].Id = 8351;
            cards[1012].Name = "Ancient Leaf";
            cards[1013].Id = 8352;
            cards[1013].Name = "Fossil Dig";
            cards[1014].Id = 8353;
            cards[1014].Name = "Skill Successor";
            cards[1015].Id = 8355;
            cards[1015].Name = "Pixie Ring";
            cards[1016].Id = 8358;
            cards[1016].Name = "Discord";
            cards[1017].Id = 8362;
            cards[1017].Name = "Battle Teleportation";

            #endregion

            // Read in the deck and sidedeck sizes
            io.SeekTo(0xC6);
            deck = new Card[io.In.ReadInt32()];
            sideDeck = new Card[io.In.ReadInt32()];
            // Read the deck
            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = FindCard(io.In.ReadUInt16());
            }

            // Read the unlocked cards
            io.SeekTo(0x1138);
            cardsUnlocked = new uint[0x3FD];
            for (int i = 0; i < 0x3FD; i++)
            {
                cardsUnlocked[i] = io.In.ReadUInt32();
            }
        }

        public void SaveFile(ref EndianIO io)
        {
            // Write the deck
            io.SeekTo(0xC6);
            io.Out.Write(deck.Length);
            io.Out.Write(sideDeck.Length);
                
            foreach (Card card in deck)
                io.Out.Write(card.Id);

            // Write the unlocks
            io.SeekTo(0x1138);
            foreach (uint unlock in cardsUnlocked)
                io.Out.Write(unlock);

            io.Stream.Position = 0; // Seek to 0
                
            byte[] data = io.In.ReadBytes(0x219D); // Read the whole save into a byte array

            // Zero out the existing checksum in the save
            data[0xC] = 0;
            data[0xD] = 0;
            data[0xE] = 0;
            data[0xF] = 0;

            // Calculate the checksum
            checksum = Calculate(data);

            // Write the checksum to the save
            io.Stream.Position = 0xC;
            io.Out.Write(checksum);
        }

        #region CRCTable
        private static readonly UInt32[] CRCTable =
        {
            0x00000000, 0x77073096, 0xee0e612c, 0x990951ba, 0x076dc419,
            0x706af48f, 0xe963a535, 0x9e6495a3, 0x0edb8832, 0x79dcb8a4,
            0xe0d5e91e, 0x97d2d988, 0x09b64c2b, 0x7eb17cbd, 0xe7b82d07,
            0x90bf1d91, 0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de,
            0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 0x136c9856,
            0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9,
            0xfa0f3d63, 0x8d080df5, 0x3b6e20c8, 0x4c69105e, 0xd56041e4,
            0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b,
            0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3,
            0x45df5c75, 0xdcd60dcf, 0xabd13d59, 0x26d930ac, 0x51de003a,
            0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599,
            0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924,
            0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d, 0x76dc4190,
            0x01db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x06b6b51f,
            0x9fbfe4a5, 0xe8b8d433, 0x7807c9a2, 0x0f00f934, 0x9609a88e,
            0xe10e9818, 0x7f6a0dbb, 0x086d3d2d, 0x91646c97, 0xe6635c01,
            0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed,
            0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950,
            0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3,
            0xfbd44c65, 0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2,
            0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a,
            0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5,
            0xaa0a4c5f, 0xdd0d7cc9, 0x5005713c, 0x270241aa, 0xbe0b1010,
            0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f,
            0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17,
            0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad, 0xedb88320, 0x9abfb3b6,
            0x03b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x04db2615,
            0x73dc1683, 0xe3630b12, 0x94643b84, 0x0d6d6a3e, 0x7a6a5aa8,
            0xe40ecf0b, 0x9309ff9d, 0x0a00ae27, 0x7d079eb1, 0xf00f9344,
            0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb,
            0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a,
            0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5,
            0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1,
            0xa6bc5767, 0x3fb506dd, 0x48b2364b, 0xd80d2bda, 0xaf0a1b4c,
            0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef,
            0x4669be79, 0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236,
            0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe,
            0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31,
            0x2cd99e8b, 0x5bdeae1d, 0x9b64c2b0, 0xec63f226, 0x756aa39c,
            0x026d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x05005713,
            0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0x0cb61b38, 0x92d28e9b,
            0xe5d5be0d, 0x7cdcefb7, 0x0bdbdf21, 0x86d3d2d4, 0xf1d4e242,
            0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1,
            0x18b74777, 0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c,
            0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45, 0xa00ae278,
            0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7,
            0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc, 0x40df0b66,
            0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9,
            0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605,
            0xcdd70693, 0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8,
            0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b,
            0x2d02ef8d
        };
        #endregion

        // The method that does the magic
        private static uint Calculate(byte[] Value)
        {
            UInt32 CRCVal = 0xffffffff;
            for (int i = 0; i < Value.Length; i++)
            {
                CRCVal = (CRCVal >> 8) ^ CRCTable[(CRCVal & 0xff) ^ Value[i]];
            }

            return CRCVal;
        }

        public Card FindCard(ushort Id)
        {
            foreach (Card card in cards)
            {
                if (card.Id == Id)
                    return card;
            }
            throw new Exception("Card with Id " + Id.ToString() + " not Found!");
        }

        public int FindCardIndex(ushort Id)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].Id == Id)
                    return i;
            }
            throw new Exception("Card with Id " + Id.ToString() + " not Found!");
        }
    }*/
}
