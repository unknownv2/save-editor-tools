using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Horizon.PackageClasses
{
    /// <summary>
    /// Parses and builds well-formed Xbox 360 configuration files.
    /// Do not use this on configuration files that users did or can directly edit!
    /// </summary>
    internal sealed class ConfigFile
    {
        private SetType defaultSet;
        private XboxButtonMap ButtonMap;
        private string UnbindValue = null;

        private static string StripQuotes(string data)
        {
            return data.Replace("\"", String.Empty);
        }

        /// <summary>
        /// Retrieves the parts of a configuration data line.
        /// </summary>
        /// <param name="dataLine">A string of configuration data to be split.</param>
        /// <returns>The elements of the given data line.</returns>
        private static string[] GetElements(string dataLine)
        {
            string[] dataParts = dataLine.Split('"');
            List<string> elements = new List<string>();
            for (int x = 0; x < dataParts.Length; x++)
            {
                if (x % 2 == 0)
                {
                    string[] parts = dataParts[x].Split(' ');
                    foreach (string part in parts)
                    {
                        string trimmed = part.Trim();
                        if (trimmed.Length != 0)
                            elements.Add(trimmed);
                    }
                }
                else
                    elements.Add(dataParts[x].Trim());
            }
            if (elements.Count < 3)
                throw new ConfigException(string.Format("Invalid number of elements [{0}]", dataLine));
            return elements.ToArray();
        }

        /// <summary>
        /// Container for a single SET or SETA element of a configuration file.
        /// </summary>
        internal class SetElement
        {
            /// <summary>
            /// The elelement type. Either "set" or "seta".
            /// </summary>
            private SetType Set;

            /// <summary>
            /// The setting mapped to this configuration element.
            /// </summary>
            internal string DVAR;

            /// <summary>
            /// The value that corresponds to this element's DVAR.
            /// </summary>
            internal string Value;

            internal SetElement(SetType SetValue, string DVAR, string Value)
            {
                Set = SetValue;
                this.DVAR = DVAR;
                this.Value = Value;
            }

            internal SetElement(string Data, int[] SetCounter)
            {
                string[] dataParts = GetElements(Data);
                if (dataParts[0] == "set")
                {
                    Set = SetType.set;
                    SetCounter[0]++;
                }
                else if (dataParts[0] == "seta")
                {
                    Set = SetType.seta;
                    SetCounter[1]++;
                }
                else
                    throw new ConfigException(string.Format("Unknown element type encountered [{0}]", dataParts[0]));
                DVAR = StripQuotes(dataParts[1]);
                Value = StripQuotes(dataParts[2]);
            }

            /// <summary>
            /// Deserializes this element back into a string.
            /// </summary>
            /// <returns>A well-formed Xbox 360 configration SET or SETA element.</returns>
            internal string Build()
            {
                return String.Format("{0} {1} \"{2}\"", Set, StripQuotes(DVAR).Trim(), StripQuotes(Value).Trim());
            }
        }

        /// <summary>
        /// Container for a single BIND element of a configuration file.
        /// </summary>
        internal sealed class BindElement
        {
            /// <summary>
            /// The Xbox 360 controller button this element is mapped to.
            /// </summary>
            internal string Button;

            /// <summary>
            /// A list of values that are executed when this element's button is pressed.
            /// </summary>
            internal BindValueList Values;

            internal BindElement(string Button, BindValueList Values)
            {
                this.Button = Button;
                this.Values = Values;
            }

            internal BindElement(string Data)
            {
                Values = new BindValueList();
                string[] dataParts = GetElements(Data);
                Button = dataParts[1];
                for (int x = 2; x < dataParts.Length; x++)
                    Values.Add(dataParts[x]);
            }

            /// <summary>
            /// Deserializes this element back into a string.
            /// </summary>
            /// <returns>A well-formed Xbox 360 configration BIND element.</returns>
            internal string Build()
            {
                if (Values.Count == 0)
                    throw new ConfigException(string.Format("No values found for button BIND [{0}]", Button));
                string dataLine = String.Format("bind \"{0}\"", Button);
                foreach (object value in Values)
                    dataLine += String.Format(" \"{0}\"", StripQuotes(value.ToString()).Trim());
                return dataLine;
            }
        }

        /// <summary>
        /// Contains all of the SET and SETA elements of the configuration.
        /// </summary>
        internal List<SetElement> Sets = new List<SetElement>();

        /// <summary>
        /// Contains all of the BIND elements of the configuration.
        /// </summary>
        internal List<BindElement> Binds = new List<BindElement>();

        /// <summary>
        /// Gets the values of a binded button.
        /// </summary>
        /// <param name="bindButton">The button to search for.</param>
        /// <returns>The values binded to the specified button. Returns null if not found.</returns>
        internal BindValueList this[XboxButton BindButton]
        {
            get
            {
                if (ButtonMap.ContainsKey(BindButton))
                {
                    string button = ButtonMap[BindButton];
                    foreach (BindElement bind in Binds)
                        if (bind.Button == button)
                            return bind.Values;
                }
                return null;
            }
            set
            {
                if (ButtonMap.ContainsKey(BindButton))
                {
                    string button = ButtonMap[BindButton];
                    for (int x = 0; x > Binds.Count; x++)
                        if (Binds[x].Button == button)
                            Binds[x].Values = value;
                }
            }
        }

        /// <summary>
        /// Gets the value of a DVAR.
        /// </summary>
        /// <param name="DVAR">The DVAR to search for.</param>
        /// <returns>The value of the DVAR specified. Returns null if not found.</returns>
        internal string this[string DVAR]
        {
            get
            {
                foreach (SetElement set in Sets)
                    if (set.DVAR == DVAR)
                        return set.Value;
                return null;
            }
            set
            {
                for (int x = 0; x < Sets.Count; x++)
                    if (Sets[x].DVAR == DVAR)
                        Sets[x].Value = value;
            }
        }

        /// <summary>
        /// Creates an empty Xbox 360 configuration file using the default SET type "set".
        /// </summary>
        internal ConfigFile()
            : this(SetType.set)
        {

        }

        /// <summary>
        /// Creates an empty Xbox 360 configuration file.
        /// </summary>
        /// <param name="DefaultSetType">Sets the type of SET element this configuration will use.</param>
        internal ConfigFile(SetType DefaultSetType)
            : this(DefaultSetType, null)
        {

        }

        /// <summary>
        /// Creates an empty Xbox 360 configuration file with the specified button map and default SET element type ("set").
        /// </summary>
        /// <param name="ButtonMap">A map of buttons used to easily manage button binds.</param>
        internal ConfigFile(XboxButtonMap ButtonMap)
            : this(SetType.set, ButtonMap)
        {

        }

        /// <summary>
        /// Creates an empty Xbox 360 configuration file with the specified button map.
        /// </summary>
        /// <param name="DefaultSetType">Sets the type of SET element this configuration will use.</param>
        /// <param name="ButtonMap">A map of buttons used to easily manage button binds.</param>
        internal ConfigFile(SetType DefaultSetType, XboxButtonMap ButtonMap)
        {
            defaultSet = DefaultSetType;
            this.ButtonMap = ButtonMap ?? new XboxButtonMap();
        }

        /// <summary>
        /// Parses the given well-formed Xbox 360 configuration file with no bind element management.
        /// </summary>
        /// <param name="configData">The configuration file to be parsed.</param>
        internal ConfigFile(string ConfigData)
            : this(ConfigData, null)
        {

        }

        /// <summary>
        /// Parses the given well-formed Xbox 360 configuration file.
        /// </summary>
        /// <param name="configData">The configuration file to be parsed.</param>
        /// <param name="buttonMap">A map of buttons used to easily manage button binds.</param>
        internal ConfigFile(string ConfigData, XboxButtonMap ButtonMap)
        {
            this.ButtonMap = ButtonMap ?? new XboxButtonMap();
            string[] data = ConfigData.Split(Environment.NewLine);
            if (data.Length == 1)
                data = ConfigData.Split((char)0x0A);
            int[] setWeight = new int[2];
            foreach (string dataLine in data)
            {
                string configLine = dataLine.Trim(), dataLowered = configLine.ToLower();
                if (dataLowered == "unbindall")
                    UnbindValue = configLine;
                else if (configLine.Length > 5)
                {
                    if (configLine.Substring(0, 2) == "//")
                        continue;
                    if (dataLowered.Substring(0, 4) == "bind")
                        Binds.Add(new BindElement(configLine));
                    else
                        Sets.Add(new SetElement(configLine, setWeight));
                }
            }
            defaultSet = setWeight[0] >= setWeight[1] ? SetType.set : SetType.seta;
        }

        /// <summary>
        /// The default configuration file new line character as a string.
        /// </summary>
        private static readonly string NewLine = new String(new char[] { (char)0x0A });

        /// <summary>
        /// Builds a configuration file using the SET and BIND elements in the class.
        /// </summary>
        /// <returns>A well-formed Xbox 360 configuration file.</returns>
        public override string ToString()
        {
            return Build();
        }

        /// <summary>
        /// Builds a configuration file using the SET and BIND elements in the class.
        /// </summary>
        /// <returns>A well-formed Xbox 360 configuration file.</returns>
        internal string Build()
        {
            string output = UnbindValue == null ? String.Empty : (UnbindValue + NewLine);
            foreach (BindElement bind in Binds)
                output += bind.Build() + NewLine;
            foreach (SetElement set in Sets)
                output += set.Build() + NewLine;
            return output;
        }

        /// <summary>
        /// Adds a setting to configuration file.
        /// </summary>
        /// <param name="SetValue">The type of SET element to be added.</param>
        /// <param name="DVAR">The name of the setting to be added.</param>
        /// <param name="Value">The value that will be mapped to the specified DVAR.</param>
        internal void AddSetting(SetType SetValue, string DVAR, object Value)
        {
            DVAR = StripQuotes(DVAR.Trim());
            if (this[DVAR] != null)
                throw new ConfigException(string.Format("Attempted to add a DVAR that already exists in the configuration [{0}]", DVAR));
            Sets.Add(new SetElement(SetValue, DVAR, StripQuotes(Value.ToString().Trim())));
        }

        /// <summary>
        /// Adds a setting to configuration file using the most-used SET type.
        /// </summary>
        /// <param name="DVAR">The name of the setting to be added.</param>
        /// <param name="Value">The value that will be mapped to the specified DVAR.</param>
        internal void AddSetting(string DVAR, object Value)
        {
            AddSetting(defaultSet, DVAR, Value);
        }

        /// <summary>
        /// Removes a setting from the configration file if it exists.
        /// </summary>
        /// <param name="DVAR">The name of the setting to be removed.</param>
        internal void RemoveSetting(string DVAR)
        {
            for (int x = 0; x < Sets.Count; x++)
                if (Sets[x].DVAR == DVAR)
                {
                    Sets.RemoveAt(x);
                    break;
                }
        }

        /// <summary>
        /// Binds the given value to a Xbox 360 controller button.
        /// </summary>
        /// <param name="Button">The button the given values will be binded to.</param>
        /// <param name="Value">The value that gets executed when the button is pressed.</param>
        internal void BindButton(XboxButton Button, object Value)
        {
            BindValueList values = new BindValueList();
            values.Add(Value);
            BindButton(Button, values);
        }

        /// <summary>
        /// Binds the given values to a Xbox 360 controller button.
        /// </summary>
        /// <param name="Button">The button the given values will be binded to.</param>
        /// <param name="Values">A list of values that get executed when the button is pressed.</param>
        internal void BindButton(XboxButton Button, BindValueList Values)
        {
            if (!ButtonMap.ContainsKey(Button))
                throw new ConfigException(string.Format("Attempted to bind a button that doesn't exist in the given button map [{0}]", Button));
            if (this[Button] != null)
                throw new ConfigException(string.Format("Attempted to bind a button that already exists in the configuration [{0}]", Button));
            Binds.Add(new BindElement(ButtonMap[Button], Values));
        }

        /// <summary>
        /// Removes a Xbox 360 controller button from the configuration's BIND collection.
        /// </summary>
        /// <param name="Button">The button that will be removed from the collection.</param>
        internal void UnbindButton(XboxButton Button)
        {
            if (!ButtonMap.ContainsKey(Button))
                throw new ConfigException(string.Format("Attempted to unbind a button that doesn't exist in the given button map [{0}]", Button));
            string buttonString = ButtonMap[Button];
            for (int x = 0; x < Binds.Count; x++)
                if (Binds[x].Button == buttonString)
                {
                    Binds.RemoveAt(x);
                    break;
                }
        }
    }

    /// <summary>
    /// The type of SET element in a Xbox 360 configuration file.
    /// </summary>
    internal enum SetType
    {
        set,
        seta
    }

    /// <summary>
    /// Stores a map of Xbox 360 controller buttons to their corresponding bind strings.
    /// </summary>
    internal class XboxButtonMap : Dictionary<XboxButton, string>
    {

    }

    /// <summary>
    /// A list of values that will be binded to a Xbox 360 controller button.
    /// </summary>
    internal class BindValueList : List<object>
    {

    }

    /// <summary>
    /// A list of Xbox 360 controller buttons that data can be binded to.
    /// </summary>
    internal enum XboxButton
    {
        Start,
        Back,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        RightStick,
        LeftStick,
        A,
        B,
        X,
        Y,
        RightShoulder,
        LeftShoulder,
        RightTrigger,
        LeftTrigger,
        LeftThumbstickDown,
        LeftThumbstickLeft,
        LeftThumbstickRight,
        LeftThumbstickUp,
        RightThumbstickDown,
        RightThumbstickLeft,
        RightThumbstickRight,
        RightThumbstickUp,
        RightThumbstickX,
        RightThumbstickY,
        LeftThumbstickX,
        LeftThumbstickY
    }
}
