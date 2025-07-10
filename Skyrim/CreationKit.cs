using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Skyrim
{
    static class CreationKit
    {
        internal static void ExportTxtToBin(string exportTxt, string outputBin)
        {
            string[] skyData = File.ReadAllLines(exportTxt);

            EndianIO IO = new EndianIO(outputBin, EndianType.LittleEndian, true);
            IO.Out.Write(skyData.Length - 1);

            for (int x = 1; x < skyData.Length; x++)
            {
                string[] current = skyData[x].Split("\t");

                string recordType = current[0];
                IO.Out.Write(Encoding.ASCII.GetBytes(recordType));

                switch (recordType)
                {
                    case "WEAP":
                        IO.Out.Write(current[1]);
                        IO.Out.Write(formIdStringToInt(current[2]));
                        IO.Out.Write(int.Parse(current[11])); // attack damage
                        break;
                }

            }

            IO.Close();
        }

        private static int formIdStringToInt(string formId)
        {
            byte[] temp = Horizon.Functions.Global.hexStringToArray(formId.Remove(9, 1).Remove(0, 1));
            Array.Reverse(temp);
            return BitConverter.ToInt32(temp, 0);
        }
    }
}
