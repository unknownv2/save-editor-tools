using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crackdown2;

namespace Horizon.PackageEditors.Crackdown_2
{
    public partial class Crackdown2 : EditorControl
    {
        //public static readonly string FID = "4D5308BC";
        public Crackdown2()
        {
            InitializeComponent();
            TitleID = FormID.Crackdown2;
            
        }

        Crackdown2Class XSave;
        public override bool Entry()
        {
            if (!OpenStfsFile(0))
                return false;
            XSave = new Crackdown2Class();
            XSave.LoadSave(IO);
            integerInput1.Value = XSave.agility;
            integerInput2.Value = XSave.firearms;
            integerInput3.Value = XSave.strength;
            integerInput4.Value = XSave.explosives;
            integerInput5.Value = XSave.driving;
            return true;
        }

        public override void Save()
        {
            XSave.agility = (short)integerInput1.Value;
            XSave.firearms = (short)integerInput2.Value;
            XSave.strength = (short)integerInput3.Value;
            XSave.explosives = (short)integerInput4.Value;
            XSave.driving = (short)integerInput5.Value;
            XSave.WriteSave();
        }
    }
}
