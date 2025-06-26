using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CallofDuty
{
    public class Dvar
    {
        public ListViewItem ListViewItem
        {
            get
            {
                return new ListViewItem(new string[] { Name, Value });
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NameLc = value.ToLower();
            }
        }
        public string NameLc { get; private set; }

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                ValueLc = value.ToLower();
            }
        }
        public string ValueLc { get; private set; }
    }
}
