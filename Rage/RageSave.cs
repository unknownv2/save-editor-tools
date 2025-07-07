using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Horizon.PackageEditors.Rage
{
    public partial class RageSave : EditorControl
    {
        //public static readonly string FID = "425307EC";
        private bool ReadSaveState = false;
        public RageSave()
        {
            InitializeComponent();
            TitleID = FormID.RageSave;
            
        }
        public override bool Entry()
        {
            if (!this.OpenStfsFile("inventory.rage"))
                return false;

            Game.Rage.RageInventorySave Inventory = new Game.Rage.RageInventorySave(this.IO);

            if (ReadSaveState)
            {
                if (this.OpenStfsFile(3))
                {
                    Game.Rage.RageInstanceSave SaveInstance = new Game.Rage.RageInstanceSave(this.IO);

                #if INT2
                    SaveFileDialog sfd = new SaveFileDialog();
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        SaveInstance.ExtractSaveData().Save(sfd.FileName);
                    }
                #endif
                }
            }

            this.richTextBox1.Text = CreateInventoryListing(Inventory.ParseBinaryInventory());

            return true;
        }
        public override void Save()
        {
            
        }
        private string CreateInventoryListing(string Inventory)
        {
            StringBuilder inv = new StringBuilder();
            TextReader reader = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(Inventory)));
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if(line.Contains("{"))
                {
                    while ((line = reader.ReadLine()) != "}")
                    {
                        inv.AppendLine(string.Format("{{\n attachment: \"{0}\"; \n}}", line));
                    }
                }
                else
                {
                    inv.AppendLine(string.Format("item: \"{0}\"", line));
                }
            }

            return inv.ToString();
        }
        private void ParseInventoryListing(string Inventory)
        {

        }
    }
}
