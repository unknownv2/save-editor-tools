using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Horizon.PackageEditors.NHL_12
{
    public partial class NHL12_CardEditor : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "45410964";


        public NHL12_CardEditor()
        {
            InitializeComponent();
            TitleID = FormID.NHL12_CardEditor;

            //Set our title ID
            

        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file.
            if (!this.OpenStfsFile(0))
                return false;

            //Verify our save
            IO.In.BaseStream.Position = 0;
            if (IO.In.ReadNullTerminatedString() != "HockeyCards")
            {
                //We failed to verify our save.
                IO.Close();
                return false;
            }

            comboBoxEx1.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                //Get our player position
                comboBoxEx1.Items.Add(((((i - (i % 3)) / 2) % 3 == 0) ? "Player " : "Goalie ") + (i % 3 + (i >= 6 ? 3 : 0) + 1).ToString());
            }

            //Our file is read correctly.
            return true;
        }
        public override void Save()
        {

        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IO == null)
                return;

            //Read our image
            pictureBox1.Image = BytesToImage(ReadImage(comboBoxEx1.SelectedIndex));

        }

        private Image BytesToImage(byte[] biteme)
        {
            //Get our image
            MemoryStream ms = new MemoryStream(biteme);
            Image i = Image.FromStream(ms);
            ms.Close();

            //Return our image
            return i;
        }
        private byte[] ReadImage(int index)
        {
            //Get our image size.
            IO.In.BaseStream.Position = 0x2C + (comboBoxEx1.SelectedIndex * 0x3E808) + 0x3E800;
            int imgSize = IO.In.ReadInt32();

            //Get our image
            IO.In.BaseStream.Position = 0x2C + (comboBoxEx1.SelectedIndex * 0x3E808);
            return IO.In.ReadBytes(imgSize);
        }
        private bool WriteImage(int index, byte[] data)
        {
            //Write our data
            if (data.Length <= 0x3E800)
            {
                //Write our image
                IO.Out.BaseStream.Position = 0x2C + (comboBoxEx1.SelectedIndex * 0x3E808);
                IO.Out.Write(data);

                //Write our size
                IO.Out.BaseStream.Position = 0x2C + (comboBoxEx1.SelectedIndex * 0x3E808) + 0x3E800;
                IO.Out.Write((int)data.Length);

                //Return our status
                return true;
            }
            return false;
        }

        public void Verify()
        {
            //Go to our checksum offset
            IO.In.BaseStream.Position = 0x10;
            uint currentSig = IO.In.ReadUInt32();
            uint expectedSig = CalculateChecksum();

            //Compare our checksum
            if (currentSig != expectedSig && false)
                throw new Exception("Could not verify NHL12 save successfully.");
        }
        public void Resign()
        {
            //Calculate our checksum
            uint checksum = CalculateChecksum();
            //Go to our checksum offset
            IO.Out.BaseStream.Position = 0x10;
            //Write our checksum
            IO.Out.Write(checksum);
        }

        private uint CalculateChecksum()
        {
            //Read our gamesave buffer
            IO.In.BaseStream.Position = 0x1C;
            byte[] saveData = IO.In.ReadBytes(IO.In.BaseStream.Length - IO.In.BaseStream.Position);
            return ElectronicArts.EACRC32.Calculate_Alt2(saveData, saveData.Length, 0x00);
        }
    }
}