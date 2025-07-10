using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using RedDeadRedemption;

namespace Horizon.PackageEditors.Red_Dead_Redemption
{
    public partial class RedDeadRedemption : EditorControl
    {
        //public static readonly string FID = "5454082B";
        private RdR GameSave;

        public RedDeadRedemption()
        {
            InitializeComponent();
            TitleID = FormID.RedDeadRedemption;
            
        }
        public override bool Entry()
        {
            this.IO = new EndianIO(this.Package.StfsContentPackage.GetFileStream("RDR2SAVE0.SAV"), EndianType.BigEndian);
            this.IO.Open();

            GameSave = new RdR(IO);

            return true;
        }
        public override void Save()
        {
            this.IO.Out.SeekTo(08);
            this.IO.Out.Write(GameSave.Save());

            this.IO.Stream.Flush();
        }

    }

}
