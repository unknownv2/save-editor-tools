using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Lost_Odyssey
{
    public partial class LostOdyssey : EditorControl
    {
        //public static readonly string FID = "4D5307FA";
        public LostOdyssey()
        {
            InitializeComponent();
            TitleID = FormID.LostOdyssey;
            
        }
        public override bool Entry()
        {
            if (!this.OpenStfsFile(0))
            {
                return false;
            }
            this.Decrypt();

            return true;
        }
        /// <summary>
        /// Decrypt the Lost Odyssey save and verify it.
        /// </summary>
        private void Decrypt()
        {
	        //Read out header values
            this.IO.In.SeekTo(0x0C);
	        ushort wHeaderLength1, wHeaderLength2;
	        wHeaderLength1 = this.IO.In.ReadUInt16(); // General header length
            wHeaderLength2 = this.IO.In.ReadUInt16(); // Intermediate header length
            this.IO.In.SeekTo(0x014);
            uint dwOrigSaveDataSum = this.IO.In.ReadUInt32(); // Body checksum
            int dwOrigHeaderSum = this.IO.In.ReadInt32(); /// Header checksum

	        //Calculate header checksum
            this.IO.Out.SeekTo(0x18) ;
            this.IO.Out.Write(0); // Erase the stored, current checksum
	        int dwHeaderSum = 0, dwTotalHeaderLength = (wHeaderLength2+ wHeaderLength1) ; // get total header size
            this.IO.In.SeekTo(0);
	        for (int x = new int();x < dwTotalHeaderLength; x++) // calculate header checksum
		        dwHeaderSum += (this.IO.In.ReadByte() ^ x);	
	        if (dwOrigHeaderSum != dwHeaderSum) //check if the header data has not been corrupted
	        {
		        System.Diagnostics.Debug.WriteLine("The gamesave's header is invalid.");
	        }
            this.IO.In.SeekTo(0x10);
	        int dwSaveDataSize = this.IO.In.ReadInt32(); // get the size of the actual save data

            this.IO.In.SeekTo(dwTotalHeaderLength);
            byte[] SaveData = this.IO.In.ReadBytes(dwSaveDataSize);
	        //now we decrypt and verify the actual save data (a pretty simple cipher)
	        uint dwSaveDataSum = 0;
            byte bXori = 0, bVal;
	        for (uint x = new int(); x < dwSaveDataSize; x++)
	        {
                bVal = (byte)(((SaveData[x] ^ 0xBB) - x) ^ bXori);	// decryption p1
                bXori = SaveData[x];	// decryption p2
		        SaveData[x] = bVal;		// decryption p3

		        dwSaveDataSum += (SaveData[x] ^ x); // calculating data checksum as it decrypts
	        }
	        if (dwOrigSaveDataSum != dwSaveDataSum) //check to see if the save data has not been "corrupted"
	        {
                System.Diagnostics.Debug.WriteLine("The gamesave's savedata is invalid.");
	        }
            this.IO.Out.SeekTo(dwTotalHeaderLength);
            this.IO.Out.Write(SaveData);
        }
        /// <summary>
        /// Encrypt the Lost Odyssey Save and recalculate the checksums.
        /// </summary>
        private void Encrypt()
        {
            this.IO.In.SeekTo(0x0C);
            ushort wHeaderLength1, wHeaderLength2;
            wHeaderLength1 = this.IO.In.ReadUInt16(); // General header length
            wHeaderLength2 = this.IO.In.ReadUInt16(); // Intermediate header length

            this.IO.In.SeekTo(0x10);
            uint SaveDataLength = this.IO.In.ReadUInt32();
            uint dwHeaderSum = 0, dwTotalHeaderLength = (uint)(wHeaderLength2 + wHeaderLength1);

            this.IO.In.SeekTo(dwTotalHeaderLength);
            byte[] SaveData = this.IO.In.ReadBytes((int)SaveDataLength);
            int dwSaveDataSum = 0;
            byte bXori = 0;
            for (int x = new int(); x < SaveDataLength; x++)
            {
                dwSaveDataSum += (SaveData[x] ^ x);
                SaveData[x] = (byte)(((SaveData[x] ^ bXori) + x) ^ 0xBB);
                bXori = SaveData[x];
            }
            this.IO.Out.SeekTo(dwTotalHeaderLength);
            this.IO.Out.Write(SaveData);
            this.IO.Out.SeekTo(0x14);
            this.IO.Out.Write(dwSaveDataSum);
            this.IO.Out.Write(0);
            this.IO.In.SeekTo(0);
            for (uint x = new int(); x < dwTotalHeaderLength; x++) // calculate header checksum
                dwHeaderSum += (this.IO.In.ReadByte() ^ x);
            this.IO.Out.SeekTo(0x18);
            this.IO.Out.Write(dwHeaderSum);
        }
    }
}
