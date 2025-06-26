using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.PackageClasses
{
    public class DvarCollection : Dictionary<string, string>
    {
        private Dictionary<string, string> dvarsL = new Dictionary<string, string>();

        public new bool ContainsKey(string dvar)
        {
            if (base.ContainsKey(dvar))
                return true;

            return this.dvarsL.ContainsKey(dvar.ToLower());
        }

        public new void Remove(string dvar)
        {
            string dvarL = dvar.ToLower();
            if (!dvarsL.ContainsKey(dvarL))
                return;

            base.Remove(dvarsL[dvarL]);
            this.dvarsL.Remove(dvarL);
        }

        public new string this[string dvar]
        {
            get
            {
                string dL = dvar.ToLower();
                return this.dvarsL.ContainsKey(dL) ? base[dvarsL[dL]] : null;
            }
            set
            {
                string dL = dvar.ToLower();
                if (this.dvarsL.ContainsKey(dL))
                    base[this.dvarsL[dL]] = value;
                else
                {
                    this.dvarsL.Add(dL, dvar);
                    base.Add(dvar, value);
                }
            }
        }
        public DvarCollection(Dictionary<string, string> dictionary)
            : base(dictionary)
        {

        }
        public DvarCollection(EndianIO IO)
        {
            int dvarLength;
            while ((dvarLength = IO.In.ReadInt32()) != -1)
                this[IO.In.ReadString(dvarLength)] = IO.In.ReadString(IO.In.ReadInt32());
        }

        public void Write(EndianIO IO)
        {
            foreach (KeyValuePair<string, string> dvar in this)
            {
                IO.Out.Write(dvar.Key.Length);
                IO.Out.Write(Encoding.ASCII.GetBytes(dvar.Key));
                IO.Out.Write(dvar.Value.Length);
                IO.Out.Write(Encoding.ASCII.GetBytes(dvar.Value));
            }
            IO.Out.Write(-1);
        }
    }
}
