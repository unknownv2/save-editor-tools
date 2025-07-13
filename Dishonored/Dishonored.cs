using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dishonored;

namespace Horizon.PackageEditors.Dishonored
{
    public partial class Dishonored : EditorControl
    {
        //public static readonly string FID = "425307E3";
        private GameSave _gameSave;
        public Dishonored()
        {
            InitializeComponent();
            TitleID = FormID.Dishonored;
            
        }
        public override bool Entry()
        {
            if (!OpenStfsFile("PAYLOAD"))
                return false;

            _gameSave = new GameSave(IO);

            return true;
        }

        public override void Save()
        {
            
        }
    }
}
