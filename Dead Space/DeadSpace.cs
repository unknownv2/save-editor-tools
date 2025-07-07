using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeadSpace;

namespace Horizon.PackageEditors.Dead_Space
{
    public partial class DeadSpace : EditorControl
    {
        //public static readonly string FID = "45410857";
        public DeadSpace()
        {
            InitializeComponent();
            TitleID = FormID.DeadSpace;
            
        }

        //private Save save;
        private DeadSpace1Save GameSave;

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;
            GameSave = new DeadSpace1Save(IO);

            intCredits.Value = GameSave.Credits;
            intNodes.Value = GameSave.Nodes;

            return true;
        }

        public override void Save()
        {
            GameSave.Credits = intCredits.Value;
            GameSave.Nodes = intNodes.Value;

            GameSave.Save();
        }
        private void CmdMaxCredits(object sender, EventArgs e)
        {
            intCredits.Value = intCredits.MaxValue;
        }
        private void CmdMaxNodes(object sender, EventArgs e)
        {
            intNodes.Value = intNodes.MaxValue;
        }
    }
}
