using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageEditors.Brutal_Legend
{
    public class BrutalLegendCodeHandler
    {
        #region Game Save Values
        private uint MAGIC { get; set; }
        private uint VERSION { get; set; }
        public string DATA { get; set; }
        #endregion
    
        #region Constructor

        public BrutalLegendCodeHandler(EndianIO IO)
        {
            //Read our gamesave
            Read(IO);
        }

        #endregion

        #region Functions

        public void Read(EndianIO IO)
        {
            //Set our position
            IO.In.BaseStream.Position = 0x00;
            //Read our magic
            MAGIC = IO.In.ReadUInt32();
            //Read our version
            VERSION = IO.In.ReadUInt32();
            //Read our data
            DATA = IO.In.ReadAsciiString((int)IO.In.ReadUInt32());
        }

        public void Write(EndianIO IO)
        {
            //Go to our position
            IO.Out.BaseStream.Position = 0x00;
            //Write our magic
            IO.Out.Write(MAGIC);
            //Write our version
            IO.Out.Write(VERSION);
            //Write our length
            IO.Out.Write((uint)DATA.Length + 1);
            //Write our string
            IO.Out.WriteAsciiString(DATA, DATA.Length + 1);
            //Set our length
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }

        public string FormatStringToCode(string str)
        {
            //Create our resultant string
            string result = str;

            //Replace
            result = result.Replace(",", ", ").Replace("=", " = ").Replace(";", ";\n")
                .Replace("{", "\n{\n").Replace("}", "\n}\n");

            //Split each like
            string[] lines = result.Split('\n');
            //Set our result as blank
            result = "";
            //Set our indentation count
            int indentCount = 0;
            //Loop for each line
            foreach (string line in lines)
            {
                //If our line isnt blank
                if (line.Length > 0)
                {
                    //Check our brace
                    if (line.Contains("}"))
                        indentCount--;

                    //If our line is just ;
                    if (line == ";")
                    {
                        //If our last character is a new line
                        if (result[result.Length - 1] == '\n')
                            //Remove the new line character
                            result = result.Substring(0, result.Length - 1);
                        result += line + "\n";
                    }
                    else
                    {
                        //Create our indented line..
                        string indentedLine = line;
                        //Loop for our count
                        for (int i = 0; i < indentCount; i++)
                            indentedLine = "\t" + indentedLine;
                        //Add our line to the result
                        result += indentedLine + "\n";
                    }

                    //Check our brace
                    if (line.Contains("{"))
                        indentCount++;
                }
            }
            //Return our result.
            return result;
        }

        public string FormatCodeToString(string code)
        {
            return code.Replace(" ", "").Replace("\n", "").Replace("\t", "");
        }
        #endregion

        #region Classes

        #endregion
    }
}
