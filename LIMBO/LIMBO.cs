using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.LIMBO
{
    public partial class LIMBO : EditorControl
    {
       /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "584109D1";
        private bool changing = false;
        /// <summary>
        /// Our list of properties for this save.
        /// </summary>
        private List<string> SaveProperties;
        /// <summary>
        /// Our bool indicating if we loaded our properties.
        /// </summary>
        private bool PropertiesLoaded
        {
            get { return SaveProperties != null; }
        }
        /// <summary>
        /// Our point at which we are saved at and our progression.
        /// </summary>
        private int SavePoint
        {
            get
            {   
                //If our properties aren't loaded
                if (!PropertiesLoaded)
                    //Return 0
                    return 0;

                //Get our string
                string result = GetValue("savepointreached");
                //If result is null
                if (result == null)
                    //Get our other save point value
                    result = GetValue("lastsavepoint");
                //Return our parsed integer of the value
                return int.Parse(result);
            }
            set
            {
                //If our properties are loaded
                if (PropertiesLoaded)
                {
                    //Set our value
                    SetValue("savepointreached", value.ToString());
                    SetValue("lastsavepoint", value.ToString());
                }
            }

        }
        /// <summary>
        /// An array of integers portraying chapter start.
        /// </summary>
        private int[] ChapterStart = { 22, 51, 61, 72, 91, 121, 151, 173, 201, 211, 231, 251, 262, 281, 301, 311, 321, 324, 341, 381, 391, 411, 431, 451 };
       
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public LIMBO()
        {
            InitializeComponent();
            TitleID = FormID.LIMBO;
            //Set our title ID
            
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. (shadowcopy.props contains information on game completion & manuscripts), while savegame.aws contains information on your currently saved game.
            if (!this.OpenStfsFile("savegame.txt"))
                return false;

           
            //Read our properties
            SaveProperties = new List<string>(IO.In.ReadAsciiString((int)IO.In.BaseStream.Length).Split('\n'));

            //Remove any blank entries.
            SaveProperties.Remove("");

            //Get our save point
            intSavePoint.Value = SavePoint;

            //Our file is read correctly.
            return true;
        }


        public override void Save()
        {
            //Set our Value
            SavePoint = intSavePoint.Value;
            //Set our position
            IO.Out.BaseStream.Position = 0;
            //Create our result string
            string result = "";
            //Loop for each property.
            foreach (string p in SaveProperties)
                result += p + "\n";
            //Write our string
            IO.Out.WriteAsciiString(result, result.Length);
            //Set our length of our save.
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }

        private string GetValue(string key)
        {
            //Loop through each property
            foreach (string prop in SaveProperties)
            {
                //If our property isnt null
                if (prop != "")
                {
                    //If our keys are alike
                    if (key == GetKeyFromProp(prop))
                        //Return our key
                        return GetValueFromProp(prop);
                }
            }
            //Otherwise return null
            return null;
        }
        private void SetValue(string key, string value)
        {
            //Loop through each property
            for (int i = 0; i < SaveProperties.Count; i++)
            {
                //Get our temp string
                //If our property is our key
                if (GetKeyFromProp(SaveProperties[i]) == key)
                {
                    //Set our property.
                    SaveProperties[i] = key + " = \"" + value + "\"";
                    //Return.
                    return;
                }
            }
            //Add our property.
            SaveProperties.Add(key + " = \"" + value + "\"");
        }

        private string GetKeyFromProp(string prop)
        {
            //Split our property
            string[] tmpProp = prop.Split('=');

            //Return our key
            return tmpProp[0].Substring(0, tmpProp[0].Length - 1);

        }
        private string GetValueFromProp(string prop)
        {
            //Split our property
            string[] tmpProp = prop.Split('=');

            //Return our value
            return tmpProp[tmpProp.Length - 1].Substring(2, tmpProp[tmpProp.Length - 1].Length - 3);
        }

        private int GetChapterIndexForPoint(int point)
        {
            //Loop backwards through our array
            for (int i = ChapterStart.Length - 1; i >= 0; i--)
                //If our point is further in this chapter or at the beginning.
                if (point >= ChapterStart[i])
                    //Return the chapter index
                    return i;
            //Otherwise return 0
            return 0;
        }

        private void intSavePoint_ValueChanged(object sender, EventArgs e)
        {
            //Set our bool
            changing = true;
            //Set our chapter
            intChapter.Value = GetChapterIndexForPoint(intSavePoint.Value);
            //Set our bool
            changing = false;
            //Calculate our percent
            int percent = (int)(((double)(intSavePoint.Value - intSavePoint.Minimum) / (double)(intSavePoint.Maximum - intSavePoint.Minimum)) * 100);
            //Show our percent
            intSavePoint.Text = "Progress " + percent.ToString() + "%";
        }

        private void intChapter_ValueChanged(object sender, EventArgs e)
        {
            //Set our text
            intChapter.Text = "Chapter (" + (intChapter.Value + 1) + "/" + (intChapter.Maximum + 1) + ")";

            //If we aren't changing because of the other track bar.
            if(!changing)
                //Set our last save point
                intSavePoint.Value = ChapterStart[intChapter.Value];
        }

    }
}
