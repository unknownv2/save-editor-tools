using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MedalOfHonor;
using ElectronicArts;

namespace Horizon.PackageEditors.Medal_Of_Honor
{
    public partial class MedalOfHonor : EditorControl
    {
        /// <summary>
        /// Our EA MCO2 Header for this package.
        /// </summary>
        private EA SaveHeader;
        /// <summary>
        /// The gamesave class to handle our MOH save.
        /// </summary>
        private MedalOfHonorSave GameSave;
        /// <summary>
        /// Our title ID for this game.
        /// </summary>
        //public static readonly string FID = "454108F7";

        /// <summary>
        /// Our main initialization point for this application.
        /// </summary>
        public MedalOfHonor()
        {
            //Initialize our component
            InitializeComponent();
            TitleID = FormID.MedalOfHonor;
            //Set our title ID.
            //
        }

        /// <summary>
        /// Overrides our opening/entry point into the container.
        /// </summary>
        /// <returns></returns>
        public override bool Entry()
        {
            //If we fail to open our save file from our container..
            if (!this.OpenStfsFile("MOHASAVEDGAME"))
            {
                //Return false.
                return false;
            }

            //Otherwise, read our save header
            this.ReadSaveHeader();

            //Success.
            return true;
        }
        /// <summary>
        /// This override allows us to save our changes made to our file.
        /// </summary>
        public override void Save()
        {
            //At the moment all this does is resign our save.. No editting done.
            this.GameSave.Save();
        }

        /// <summary>
        /// This stub reads our save header.
        /// </summary>
        private void ReadSaveHeader()
        {
            //Read our EA MC02 header from our IO.
            this.SaveHeader = new EA(this.IO);
            //Initialize our gamesave editting class using our IO and our EA MC02 header.
            this.GameSave = new MedalOfHonorSave(this.IO, this.SaveHeader);
        }
    }
}
