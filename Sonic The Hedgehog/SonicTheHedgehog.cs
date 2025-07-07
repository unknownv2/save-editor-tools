using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Sonic_The_Hedgehog
{
    public partial class SonicTheHedgehog : EditorControl
    {
        //public static readonly string FID = "534507D6";
        public SonicTheHedgehog()
        {
            InitializeComponent();
            TitleID = FormID.SonicTheHedgehog;
            
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("SonicNextSaveData.bin"))
                return false;
            IO.SeekTo(4);
            intLivesSonic.Value = IO.In.ReadInt32();
            intLivesShadow.Value = IO.In.ReadInt32();
            intLivesSilver.Value = IO.In.ReadInt32();
            intLivesFinal.Value = IO.In.ReadInt32();
            IO.SeekTo(132);
            intMoneySonic.Value = IO.In.ReadInt32();
            intMoneyShadow.Value = IO.In.ReadInt32();
            intMoneySilver.Value = IO.In.ReadInt32();
            intMoneyFinal.Value = IO.In.ReadInt32();
            return true;
        }

        public override void Save()
        {
            IO.SeekTo(4);
            IO.Out.Write(intLivesSonic.Value);
            IO.Out.Write(intLivesShadow.Value);
            IO.Out.Write(intLivesSilver.Value);
            IO.Out.Write(intLivesFinal.Value);
            IO.SeekTo(132);
            IO.Out.Write(intMoneySonic.Value);
            IO.Out.Write(intMoneyShadow.Value);
            IO.Out.Write(intMoneySilver.Value);
            IO.Out.Write(intMoneyFinal.Value);
        }

        private void cmdMaxSonic_Click(object sender, EventArgs e)
        {
            intLivesSonic.Value = intLivesSonic.MaxValue;
            intMoneySonic.Value = intMoneySonic.MaxValue;
        }

        private void cmdMaxShadow_Click(object sender, EventArgs e)
        {
            intLivesShadow.Value = intLivesShadow.MaxValue;
            intMoneyShadow.Value = intMoneyShadow.MaxValue;
        }

        private void cmdMaxSilver_Click(object sender, EventArgs e)
        {
            intLivesSilver.Value = intLivesSilver.MaxValue;
            intMoneySilver.Value = intMoneySilver.MaxValue;
        }

        private void cmdMaxFinal_Click(object sender, EventArgs e)
        {
            intLivesFinal.Value = intLivesFinal.MaxValue;
            intMoneyFinal.Value = intMoneyFinal.MaxValue;
        }
    }
}
