using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FarCry
{
    public class WikiItems
    {
        public int ItemCount { get; set; }
        public List<Item> Items { get; set; } 

        public struct Item
        {
            public ushort EncylopediaCategory { get; set; }
            public ushort EncyclopediaSubcategory { get; set; }
        }

        public WikiItems(byte[] data)
        {
            Items = new List<Item>();
            var reader = new EndianReader(data, EndianType.LittleEndian);
            ItemCount = reader.ReadInt32();
            for (int x = 0; x < ItemCount; x++)
            {
                Items.Add(new Item
                              {
                                  EncylopediaCategory = reader.ReadUInt16(),
                                  EncyclopediaSubcategory = reader.ReadUInt16()
                              });
            }
        }
    }
}
