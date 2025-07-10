using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Horizon.PackageEditors.Just_Cause_2
{
    public partial class JustCause2 : EditorControl
    {
       /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "534307E7";
        private uint WeaponParts_Offset { get; set; }
        private uint VehicleParts_Offset { get; set; }
        private uint ArmorParts_Offset { get; set; }
        private uint Money_Offset { get; set; }
        private uint Chaos_Offset { get; set; }
        private System.IO.EndianType eType { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public JustCause2()
        {
            InitializeComponent();
            TitleID = FormID.JustCause2;
            //Set our title ID
            
            intMoney.MaxValue = 2000000000;
            intChaos.MaxValue = 2000000000;
            intWeaponParts.MaxValue = 70000;
            intVehicleParts.MaxValue = 70000;
            intArmorParts.MaxValue = 70000;
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile(0))
                return false;

            //Set our endian
            SetEndian();

            //Set our position
            IO.In.BaseStream.Position = 0x08;
            uint chaos = IO.In.ReadUInt32(eType);
            uint money = IO.In.ReadUInt32(eType);

            //Read our values
            intChaos.Value = (int)chaos;
            intMoney.Value = (int)money;

            //Go to our table offset
            int tableOff = 0x00039A00;

            //Create our value types
            int[] valTypes = {
                                 0, 1, 2, 3, 4
                             };

            //Find our offsets
            Dictionary<int, int> valOffsets = FindValues(valTypes, tableOff, 0x2000);

            //Loop through our value offsets
            
            //Find our data
            WeaponParts_Offset = (uint)valOffsets[0];//(uint)FindData(startOff, 0x2000, 0, false) + 6;
            ArmorParts_Offset = (uint)valOffsets[1];//(uint)FindData(startOff, 0x2000, 1, false) + 6;
            VehicleParts_Offset = (uint)valOffsets[2];//(uint)FindData(startOff, 0x2000, 2, false) + 6;
            Money_Offset = (uint)valOffsets[3];//(uint)FindData(startOff, 0x2000, 3, false) + 6;
            Chaos_Offset = (uint)valOffsets[4];//(uint)FindData(startOff, 0x2000, 4, false) + 6;
            valOffsets = FindValues(valTypes, (int)WeaponParts_Offset - 6, 0x2000 - (int)(WeaponParts_Offset - tableOff));
            if(valOffsets.Keys.Contains(1))
                ArmorParts_Offset = (uint)valOffsets[1];
            if (valOffsets.Keys.Contains(2))
                VehicleParts_Offset = (uint)valOffsets[2];
            if (valOffsets.Keys.Contains(3))
                Money_Offset = (uint)valOffsets[3];
            if (valOffsets.Keys.Contains(4))
                Chaos_Offset = (uint)valOffsets[4];
            IO.In.BaseStream.Position = WeaponParts_Offset;
            intWeaponParts.Value = (int)IO.In.ReadUInt32(eType);
            IO.In.BaseStream.Position = VehicleParts_Offset;
            intVehicleParts.Value = (int)IO.In.ReadUInt32(eType);
            IO.In.BaseStream.Position = ArmorParts_Offset;
            intArmorParts.Value = (int)IO.In.ReadUInt32(eType);
            IO.In.BaseStream.Position = Money_Offset;
            intMoney.Value = (int)IO.In.ReadUInt32(eType);
            IO.In.BaseStream.Position = Chaos_Offset;
            intChaos.Value = (int)IO.In.ReadUInt32(eType);

            //Verify our data
            if (!VerifyValues(chaos, money) && false)
            {
                //Throw our exception
                throw new Exception("Money or Chaos values did not match what it was in the header. \nEither the data could not be found, or it was previously modded incorrectly.");
            }

            //Our file is read correctly.
            return true;
        }
       
        public override void Save()
        {
            //Set our position
            IO.Out.BaseStream.Position = 0x08;
            //Write our values
            IO.Out.Write((uint)intChaos.Value, eType);
            IO.Out.Write((uint)intMoney.Value, eType);
            //Go to our chaos position
            IO.Out.BaseStream.Position = Chaos_Offset;
            IO.Out.Write((uint)intChaos.Value, eType);
            //Go to our money position
            IO.Out.BaseStream.Position = Money_Offset;
            IO.Out.Write((uint)intMoney.Value, eType);
            //Go to our parts offsets
            IO.Out.BaseStream.Position = WeaponParts_Offset;
            IO.Out.Write((uint)intWeaponParts.Value, eType);
            IO.Out.BaseStream.Position = VehicleParts_Offset;
            IO.Out.Write((uint)intVehicleParts.Value, eType);
            IO.Out.BaseStream.Position = ArmorParts_Offset;
            IO.Out.Write((uint)intArmorParts.Value, eType);
        }
        private void SetEndian()
        {
            //Set our position
            IO.In.BaseStream.Position = 0x00;
            int val = IO.In.ReadInt32(EndianType.BigEndian);
            //If our int is 2
            if (val == 0x00000002)
                eType = EndianType.BigEndian;
            else if (val == 0x20000000)
                eType = EndianType.LittleEndian;
            else
            {
                IO.In.BaseStream.Position = 0x28;
                if (IO.In.ReadInt32(EndianType.BigEndian) > 0x00100000)
                    eType = EndianType.LittleEndian;
                else
                    eType = EndianType.BigEndian;
            }
        }
        private int FindStartingOffset(int startOff, int length)
        {
            //Go to our start offset
            for (int i = startOff; i < startOff + length; i++ )
            {
                //Set our position
                IO.In.BaseStream.Position = i;
                if (IO.In.ReadUInt32(eType) == 0x04)
                     return i - 2;
            }
            return -1;
        }
        private int FindData(int startOff, int length, short ValType, bool inIntervals)
        {
            //Go to our start offset
            for (int i = startOff; i < startOff + length;)
            {
                //Set our position
                IO.In.BaseStream.Position = i;
                //If our read short is 3
                if (IO.In.ReadInt16(eType) == ValType)
                    if (IO.In.ReadUInt32(eType) == 0x04)
                            return i;
                if (!inIntervals)
                    i++;
                else
                    i += 0x0A;
            }
            return -1;
        }
        private int FindData(int startOff, int length, short ValType, uint Data)
        {
            //Go to our start offset
            for (int i = startOff; i < startOff + length; i++)
            {
                //Set our position
                IO.In.BaseStream.Position = i;
                //If our read short is 3
                if (IO.In.ReadInt16(eType) == ValType)
                    if (IO.In.ReadUInt32(eType) == 0x04)
                        if (IO.In.ReadUInt32(eType) == Data)
                            return i;
            }
            return -1;
        }
        private Dictionary<int, int> FindValues(int[] valueTypes, int startOff, int len)
        {
            //Get our array as a list
            List<int> valTypes = new List<int>(valueTypes);
            //Create our dictionary
            Dictionary<int, int> ValueOffsets = new Dictionary<int, int>();
            //Go to our start offset
            for (int i = startOff; i < startOff + len; i++)
            {
                //If our valtypes is blank
                if (valTypes.Count == 0)
                    break;

                //Set our position
                IO.In.BaseStream.Position = i;
                //Read our value type
                int valType = (int)IO.In.ReadInt16(eType);
                //If our read short is a valid valuetype
                if (valTypes.Contains(valType))
                    if (IO.In.ReadUInt32(eType) == 0x04)
                    {
                        ValueOffsets[valType] = i + 6;
                        valTypes.Remove(valType);
                        i += 0x09;
                    }
            }
            return ValueOffsets;
        }
        private bool VerifyValues(uint chaos, uint money)
        {
            //Go to our chaos offset
            IO.In.BaseStream.Position = Chaos_Offset;
            if (IO.In.ReadUInt32(eType) != chaos)
                return false;
            //Go to our money offset
            IO.In.BaseStream.Position = Money_Offset;
            if (IO.In.ReadUInt32(eType) != money)
                return false;
            return true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            intMoney.Value = intMoney.MaxValue;
            intChaos.Value = intChaos.MaxValue;
            intArmorParts.Value = intArmorParts.MaxValue;
            intWeaponParts.Value = intWeaponParts.MaxValue;
            intVehicleParts.Value = intVehicleParts.MaxValue;
        }
    }
}
