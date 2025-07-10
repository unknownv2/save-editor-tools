using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using CallofDuty;

namespace Horizon.Controls
{
    class DvarButton : ButtonX
    {
        public Dvar[] Dvars;

        public void SetDvars(object[] dvars)
        {
            this.Dvars = new Dvar[dvars.Length / 2];
            for (int x = 0; x < dvars.Length; x++)
            {
                Dvar dvar = new Dvar();
                dvar.Name = dvars[x++].ToString();
                dvar.Value = dvars[x].ToString();
                this.Dvars[x / 2] = dvar;
            }
        }

        public DvarButton()
        {
            this.AutoCheckOnClick = true;
            this.FocusCuesEnabled = false;
            this.Shape = new RoundRectangleShapeDescriptor();
        }
    }
}
