using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;

namespace Horizon.Controls
{
    class DvarSlider : Panel
    {
        private string _dvar;
        public string Dvar
        {
            get
            {
                return this._dvar;
            }
            set
            {
                if (value != null)
                {
                    this._dvar = value;
                    this.DvarLc = value.ToLower();
                }
            }
        }

        public ListViewItem ListViewItem
        {
            get
            {
                return new ListViewItem(new string[] { this.Dvar, this.Value.ToString() });
            }
        }

        public string DvarLc { get; private set; }

        private bool _suspend = false;
        public void BeginUpdate()
        {
            this._suspend = true;
        }

        public void EndUpdate()
        {
            this._suspend = false;
        }

        private string _dvarName;
        public string Title
        {
            get
            {
                return this._dvarName;
            }
            set
            {
                this._dvarName = value;
                this.RefreshTitle();
            }
        }

        private void RefreshTitle()
        {
            StringBuilder sb = new StringBuilder(this._dvarName);
            sb.Append(" (");
            if (this.CheckBox.Checked)
            {
                string value = this.Slider.Value.ToString();
                if (this._decimalPlaces > 0)
                {
                    value = value.Insert(value.Length - this.DecimalPlaces, ".");
                    if (value[0] == '.')
                        value = "0" + value;
                }
                sb.Append(value);
            }
            else
                sb.Append("Default");
            sb.Append(')');
            this.Slider.Text = sb.ToString();
        }

        private float ValueAsFloat
        {
            get
            {
                return this.ValueToFloat(this.Slider.Value);
            }
        }

        private float ValueToFloat(int nval)
        {
            string value = nval.ToString();
            value = value.Insert(value.Length - this._decimalPlaces, ".");
            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        private int FloatToValue(float fval)
        {
            if (this._decimalPlaces > 0)
            {

                string value = fval.ToString(".".PadRight(this._decimalPlaces + 1, '0'));
                value = value.Remove(value.Length - this._decimalPlaces - 1, 1);
                return int.Parse(value);
            }
            else
                return (int)fval;
        }

        public float Value
        {
            get
            {
                return this.ValueAsFloat;
            }
            set
            {
                this._suspend = true;
                this.Slider.Value = this.FloatToValue(value);
                this._suspend = false;
                this.RefreshTitle();
            }
        }

        public float Maximum
        {
            get
            {
                return this.ValueToFloat(this.Slider.Maximum);
            }
            set
            {
                this.Slider.Maximum = this.FloatToValue(value);
            }
        }

        public float Minimum
        {
            get
            {
                return this.ValueToFloat(this.Slider.Minimum);
            }
            set
            {
                this.Slider.Minimum = this.FloatToValue(value);
            }
        }

        private int _decimalPlaces;
        public int DecimalPlaces
        {
            get
            {
                return this._decimalPlaces;
            }
            set
            {
                this._decimalPlaces = value;
                this.RefreshTitle();
            }
        }

        public bool Checked
        {
            get
            {
                return this.CheckBox.Checked;
            }
            set
            {
                this.CheckBox.Checked = value;
                this.RefreshTitle();
            }
        }

        private Slider Slider;
        private CheckBoxX CheckBox;

        public DvarSlider()
        {
            this.BackColor = Color.Transparent;

            this.Slider = new Slider();
            this.Slider.FocusCuesEnabled = false;
            this.Slider.LabelPosition = eSliderLabelPosition.Top;
            this.Slider.Location = new Point(1, 1);
            this.Slider.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);
            this.Slider.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.Slider.Maximum = 1000;
            this.Slider.Step = 1;
            this.Controls.Add(this.Slider);

            this.CheckBox = new CheckBoxX();
            this.CheckBox.Size
                = this.CheckBox.MinimumSize
                = this.CheckBox.MaximumSize
                = new Size(16, 15);
            this.CheckBox.FocusCuesEnabled = false;
            this.CheckBox.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.Controls.Add(this.CheckBox);

            this.CheckBox.BringToFront();

            this.SizeChanged += new EventHandler(DvarSlider_SizeChanged);
            this.Slider.ValueChanged += new EventHandler(Slider_ValueChanged);
            this.CheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshTitle();
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            if (this._suspend)
                return;
            this.CheckBox.Checked = true;
            this.RefreshTitle();
        }

        private void DvarSlider_SizeChanged(object sender, EventArgs e)
        {
            int y = this.Size.Height / 2 - 19;
            if (y >= 0)
                y = 1;
            this.CheckBox.Location = new Point(this.Size.Width - 42, y);
        }
    }
}
