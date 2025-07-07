using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Magic2013;

namespace Horizon.PackageEditors.Magic_2013
{
    public partial class Magic2013 : EditorControl
    {
        private readonly string[] DeckNames = new string[]
            {
                "Celestial Light",
                "Born of Flame",
                "Pack Instinct",
                "Dream Puppets",
                "Goblin Gangland",
                "Obedient Dead",
                "Peacekeepers",
                "Exalted Darkness",
                "Ancient Wilds",
                "Crosswinds",
                "Aura Servants",
                "Sepulchral Strength",
                "Mindstorms",
                "Grinning Malice",
                "Collective Might",
                "Act of War",
                "Rogues' Gallery",
                "Berserker Rage",
                "Grim Possession",
                //"Sky and Scale",
                //"Mana Mastery"
            };

        private struct DeckListing
        {
            public int Index;
            public byte CardsInDeck;
            public string Name;
        }

        private List<DeckListing> DeckIndexes;

        private Profile ProfileData;

        private DeckEntry SelectedDeck;
        //private int SelectedIndex = -1;

        public Magic2013()
        {
            InitializeComponent();
            TitleID = FormID.Magic2013;
        }

        public override bool Entry()
        {
            if (!loadAllTitleSettings(EndianType.BigEndian))
                return false;

            ProfileData = new Profile(IO);
            DeckIndexes = new List<DeckListing>();
            DeckIndexes.AddRange( new []
            {
                new DeckListing { Index = 1, CardsInDeck = 36, Name = "Celestial Light"},
                new DeckListing { Index = 2, CardsInDeck = 36, Name = "Born of Flame"},
                new DeckListing { Index = 3, CardsInDeck = 36, Name = "Pack Instinct"},
                new DeckListing { Index = 4, CardsInDeck = 35, Name = "Dream Puppets"},
                new DeckListing { Index = 5, CardsInDeck = 36, Name = "Goblin Gangland"},
                new DeckListing { Index = 6, CardsInDeck = 0x23, Name = "Obedient Dead"},
                new DeckListing { Index = 7, CardsInDeck = 0x23, Name = "Peacekeepers"},
                new DeckListing { Index = 8, CardsInDeck = 0x28, Name = "Exalted Darkness"},
                new DeckListing { Index = 9, CardsInDeck = 0x23, Name = "Ancient Wilds"},
                new DeckListing { Index = 10, CardsInDeck = 0x23, Name = "Crosswinds"},
                new DeckListing { Index = 0x0C, CardsInDeck = 0x28, Name = "Aura Servants"},
                new DeckListing { Index = 0x0D, CardsInDeck = 0x28, Name = "Sepulchral Strength"},
                new DeckListing { Index = 0xE, CardsInDeck = 0x27, Name = "Mindstorms"},
                new DeckListing { Index = 0x0F, CardsInDeck = 0x28, Name = "Grinning Malice"},
                new DeckListing { Index = 0x10, CardsInDeck = 0x28, Name = "Collective Might"},
                new DeckListing { Index = 0x51, CardsInDeck = 0x27, Name = "Act of War"},
                new DeckListing { Index = 0x52, CardsInDeck = 0x27, Name = "Rogues' Gallery"},
                new DeckListing { Index = 0x53, CardsInDeck = 0x27, Name = "Berserker Rage"},
                new DeckListing { Index = 0x54, CardsInDeck = 0x27, Name = "Grim Possession"}
            });

            var inactiveDecks = DeckIndexes.ToList();
            foreach (var deck in ProfileData.Decks)
            {
                listViewUnlockedDecks.Items.Add(new ListViewItem() 
                {
                    Text = DeckIndexes.Find(entry => entry.Index == deck.Index).Name,
                    Tag = DeckIndexes.Find(entry => entry.Index == deck.Index)
                });
                inactiveDecks.Remove(inactiveDecks.Find(entry => entry.Index == deck.Index));
            }

            foreach (var inactiveDeck in inactiveDecks)
            {
                listViewLockedDeck.Items.Add(new ListViewItem() {Text = inactiveDeck.Name, ForeColor = Color.Gray, Tag = inactiveDeck});
            }
            
            return true;
        }

        public override void Save()
        {
            
        }

        private void DeckListViewIndexChanged(object sender, EventArgs e)
        {
            if(listViewUnlockedDecks.SelectedItems.Count <= 0)
                return;
            
            var indexedDeck = (DeckListing)listViewUnlockedDecks.SelectedItems[0].Tag;
            // save the current data
            if (SelectedDeck != null)
            {
                SelectedDeck.Flags |= (short)(chkUnlockAllCards.Checked ? 0x02 : ~0x02);
                SelectedDeck.Flags |= (short) (chkPremiumFoil.Checked ? 0x01 : ~0x01);
            }
            if (ProfileData.Decks.Exists(entry => entry.Index == indexedDeck.Index))
            {
                SelectedDeck = ProfileData.Decks.Find(entry => entry.Index == indexedDeck.Index);
            }
            else
            {
                //ProfileData.Decks.Add(new DeckEntry() { Index = indexedDeck.Index, CardsInDeck = indexedDeck.CardsInDeck, Flags = 0x08});
            }
        }

        private void BtnClickUnlockDeck(object sender, EventArgs e)
        {
            var listDeck = listViewLockedDeck.SelectedItems[0];
            var indexedDeck = (DeckListing)listDeck.Tag;

            ProfileData.Decks.Add(new DeckEntry { Index = indexedDeck.Index, CardsInDeck = indexedDeck.CardsInDeck, Flags = 0x08 });

            listViewUnlockedDecks.Items.Add(listDeck);
            listViewLockedDeck.Items.Remove(listDeck);
        }

        private void BtnClickLockDeck(object sender, EventArgs e)
        {
            var listDeck = listViewLockedDeck.SelectedItems[0];
            var indexedDeck = (DeckListing)listDeck.Tag;

            ProfileData.Decks.Remove(ProfileData.Decks.Find(entry => entry.Index == indexedDeck.Index));

            listViewLockedDeck.Items.Add(listDeck);
            listViewUnlockedDecks.Items.Remove(listDeck);
        }
    }
}
