using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Alice
{
    public partial class Alice : EditorControl
    {
        //public static readonly string FID = "45410916";
        public Alice()
        {
            InitializeComponent();
            TitleID = FormID.Alice;
        }

        private AliceSave Game;
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            Game = new AliceSave(IO);
            intTeeth.Value = Game.Teeth;

            return true;
        }

        public override void Save()
        {
            Game.Teeth = intTeeth.Value;
            Game.Save();
        }
    }
}
