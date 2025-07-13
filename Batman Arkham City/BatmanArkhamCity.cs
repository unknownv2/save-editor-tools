using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Batman_Arkham_City
{
    public partial class BatmanArkhamCity : EditorControl
    {
        //public static readonly string FID = "57520802";
        public BatmanArkhamCity()
        {
            InitializeComponent();
            TitleID = FormID.BatmanArkhamCity;
            

#if INT2
            cmdExtractState.Visible = true;
#endif
        }

        private ArkhamSave ark;
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;

            ark = new ArkhamSave(IO);

            return true;
        }

        public override void Save()
        {
            ark.Save();
        }

        private void cmdExtractState_Click(object sender, EventArgs e)
        {
#if INT2
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (IO == null || fbd.ShowDialog() != DialogResult.OK)
                return;

            string baseName = fbd.SelectedPath + "\\Segment";

            ark.Data.GetSingleSegmentIO(0).ToArray().Save(baseName + 0);
            ark.Data.GetSingleSegmentIO(1).ToArray().Save(baseName + 1);
            ark.Data.GetSegmentIO(2).ToArray().Save(baseName + "Main");
#endif
        }
    }
}
