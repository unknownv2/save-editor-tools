using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using System.IO;

namespace Horizon.PackageEditors.Diablo_III
{
    public partial class DiabloIII : EditorControl
    {

        /// <summary>
        /// Our default constructor.
        /// </summary>
        public DiabloIII()
        {
            InitializeComponent();

            //Set our title ID
            TitleID = FormID.Diablo3;
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            // Open our preferences.
            if (!this.OpenStfsFile("prefs.dat"))
                return false;
            string prefsData = IO.In.ReadAsciiString((int)IO.In.BaseStream.Length);
            LoadPreferences(prefsData);

            // Open our profile file.
            if (!this.OpenStfsFile("profile.dat"))
                return false;

            byte[] profileData = IO.In.ReadBytes(IO.In.BaseStream.Length);
            profileData = Decrypt(profileData);
            File.WriteAllBytes("C:\\decProfile.dat", profileData);

            // So far what i've seen.
            // first byte = 0x0A
            // second = filesize past this byte.
          

            // Open our file.
            if (!this.OpenStfsFile("account.dat"))
                return false;

            byte[] accData = IO.In.ReadBytes(IO.In.BaseStream.Length);
            accData = Decrypt(accData);
            File.WriteAllBytes("C:\\decAccount.dat", accData);
           

            //Our file is read correctly.
            return true;
        }

        private void LoadPreferences(string prefsData)
        {
            // Populate our list with values
            listValues.Nodes.Clear();
            string[] prefsLines = prefsData.Replace("\r", "").Split('\n'); // remove return char incase some bad editor added it in.
            foreach (string prefsLine in prefsLines)
            {
                if (string.IsNullOrEmpty(prefsLine))
                    break;

                // Split key from value
                string[] prefLineParts = prefsLine.Split(new char[] { ' ' }, 2);
                string key = prefLineParts[0];
                string val = prefLineParts[1].Replace("\"", ""); // remove quotation
                Node valNode = new Node(key);
                valNode.Cells.Add(new Cell(val));
                listValues.Nodes.Add(valNode);
            }
        }

        private static byte[] Decrypt(byte[] data)
        {
            // Start with a static integer used for our cypher.
            ulong curKey = 0x305f92d82ec9a01b;

            // Loop for each byte to decrypt in our array.
            for (int i = 0; i < data.Length; i++)
            {
                // Decrypt by xoring with the lowest byte of our curKey against our data.
                data[i] = (byte)((curKey & 0xFF) ^ data[i]);

                // Update our key by xoring it against our decrypted data, and rotating our key.
                curKey = curKey ^ data[i];
                curKey = (curKey << 56) | (curKey >> 8);
            }
            return data;
        }

        private static byte[] Encrypt(byte[] data)
        {
            // Start with a static integer used for our cypher.
            ulong curKey = 0x305f92d82ec9a01b;

            // Loop for each byte to encrypt in our array.
            for (int i = 0; i < data.Length; i++)
            {
                byte oldData = data[i];
                data[i] = (byte)((curKey & 0xFF) ^ data[i]); // xor data
                curKey = curKey ^ oldData;
                curKey = (curKey << 56) | (curKey >> 8);
            }
            return data;
        }

        public override void Save()
        {
            //Set our position
            //IO.Out.BaseStream.Position = 0;

            // Write crap

            //Set our length of our save.
            //IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }
    }
}
