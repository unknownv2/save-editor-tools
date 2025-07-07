using System;
using DeadSpace;

namespace Horizon.PackageEditors.Dead_Space_2
{
    public partial class DeadSpace2 : EditorControl
    {
        private DeadSpace2Save GameSave;
        //public static readonly string FID = "454108DF";

        public DeadSpace2()
        {
            InitializeComponent();
            TitleID = FormID.DeadSpace2;
            
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;
     
            GameSave = new DeadSpace2Save(IO);

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
