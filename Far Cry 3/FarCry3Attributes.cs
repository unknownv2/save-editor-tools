using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Linq;

namespace FarCry
{
    public class fValue
    {
        public enum ObjectAttributes
        {
            Boolean,
            Byte,
            Int16,
            UInt16,
            Int32,
            UInt32,
            Int64,
            UInt64,
            Float,
            PropertyBag,
            Array,
            Ident,
            None
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public ObjectAttributes Attributes { get; set; }
        public bool Visible { get; set; }
        public fValue()
        {
            Attributes = ObjectAttributes.None;
        }
    }
    public class fFloat : fValue
    {
        public fFloat()
        {
            Attributes = ObjectAttributes.Float;
        }
    }
    public class fByte : fValue
    {
        public fByte()
        {
            Attributes = ObjectAttributes.Byte;
        }
    }
    public class fBoolean : fValue
    {
        public fBoolean()
        {
            Attributes = ObjectAttributes.Boolean;
        }
    }
    public class fInt16 : fValue
    {
        public fInt16()
        {
            Attributes = ObjectAttributes.Int16;
        }
    }

    public class fUInt16 : fValue
    {
        public fUInt16()
        {
            Attributes = ObjectAttributes.UInt16;
        }
    }

    public class fInt32 : fValue
    {
        public fInt32()
        {
            Attributes = ObjectAttributes.Int32;
        }
    }

    public class fUInt32 : fValue
    {
        public fUInt32()
        {
            Attributes = ObjectAttributes.UInt32;
        }
    }
    public class fIdent : fValue
    {
        public fIdent()
        {
            Attributes = ObjectAttributes.Ident;
        }
    }
    public class fPropertyBag : fValue
    {
        private List<fValue> _values = new List<fValue>();

        public List<fValue> Values { get { return _values; } set { _values = value; } }

        public fPropertyBag()
        {
            Attributes = ObjectAttributes.PropertyBag;
        }
    }

    public class fArray : fValue
    {
        public List<fValue> Values = new List<fValue>();
        
        public fArray()
        {
            Attributes = ObjectAttributes.Array;
        }

        public fArray(ObjectAttributes objectType)
        {
            switch (objectType)
            {
                   case ObjectAttributes.UInt32:
                    Values = new List<fUInt32>().Cast<fValue>().ToList();
                    break;
            }

            Attributes = ObjectAttributes.Array;
        }
    }

    public class XmlParser
    {
        private List<fValue> _valuelist;
        //private float _version;
        private string _file;

        public List<fValue> ValuesList { get { return _valuelist; } set { _valuelist = value; } }

        public XmlParser(string file, bool isFile)
        {
            _valuelist = new List<fValue>();
            _file = file;

            Parse(isFile);
        }

        private void Parse(bool isFile)
        {
            //Initialize our XmlDocument for parsing
            XmlDocument xmlDoc = new XmlDocument();
            if (isFile)
            {
                //Load our plugin
                xmlDoc.Load(_file);
            }
            else
            {
                xmlDoc.LoadXml(_file);
            }
            XmlElement root = xmlDoc.DocumentElement;
            //_version = int.Parse(root.Attributes["version"].Value);
            foreach (XmlNode node in root.ChildNodes)
            {
                if (bool.Parse(node.Attributes["visible"].Value))
                    _valuelist.Add(ReadNode(node));
            }
        }
        private fValue ReadNode(XmlNode xmlNode)
        {
            switch (xmlNode.Name.ToLower())
            {
                case "struct":
                    {
                        var propertyBag = new fPropertyBag { Name = xmlNode.Attributes["path"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                        foreach (XmlNode node in xmlNode.ChildNodes)
                        {
                            propertyBag.Values.Add(ReadNode(node));
                        }
                        return propertyBag;
                    }
                case "bool":
                case "boolean":
                    {
                        return new fBoolean { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "int8":
                case "byte":
                    {
                        return new fByte { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "int16":
                case "short":
                    {
                        return new fInt16 { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "uint16":
                case "ushort":
                    {
                        return new fUInt16 { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "int32":
                case "long":
                case "int":
                    {
                        return new fInt32 { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "uint32":
                case "ulong":
                case "uint":
                    {
                        return new fUInt32 { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "single":
                case "float":
                case "float32":
                    {
                        return new fFloat { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "array":
                    {
                        //return new fArray((fValue.ObjectAttributes)(Enum.Parse(typeof(fValue.ObjectAttributes), xmlNode.Attributes["type"].Value, true)))
                        //{ Name = xmlNode.Attributes["name"].Value };
                        return new fArray { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }

            }
            return null;
        }
    }

    /// <summary>
    /// CustomClass (Which is binding to property grid)
    /// </summary>
    public class CustomClass : CollectionBase, ICustomTypeDescriptor
    {
        /// <summary>
        /// Add CustomProperty to Collectionbase List
        /// </summary>
        /// <param name="Value"></param>
        public void Add(CustomProperty Value)
        {
            base.List.Add(Value);
        }

        /// <summary>
        /// Remove item from List
        /// </summary>
        /// <param name="Name"></param>
        public void Remove(string Name)
        {
            foreach (CustomProperty prop in base.List)
            {
                if (prop.Name == Name)
                {
                    base.List.Remove(prop);
                    return;
                }
            }
        }

        /// <summary>
        /// Indexer
        /// </summary>
        public CustomProperty this[int index]
        {
            get { return (CustomProperty) base.List[index]; }
            set { base.List[index] = (CustomProperty) value; }
        }

        #region "TypeDescriptor Implementation"

        /// <summary>
        /// Get Class Name
        /// </summary>
        /// <returns>String</returns>
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        /// <summary>
        /// GetAttributes
        /// </summary>
        /// <returns>AttributeCollection</returns>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        /// <summary>
        /// GetComponentName
        /// </summary>
        /// <returns>String</returns>
        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        /// <summary>
        /// GetConverter
        /// </summary>
        /// <returns>TypeConverter</returns>
        public TypeConverter GetConverter()
        {
            //return ExpandableObjectConverter;
            return TypeDescriptor.GetConverter(this, true);
        }

        /// <summary>
        /// GetDefaultEvent
        /// </summary>
        /// <returns>EventDescriptor</returns>
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        /// <summary>
        /// GetDefaultProperty
        /// </summary>
        /// <returns>PropertyDescriptor</returns>
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        /// <summary>
        /// GetEditor
        /// </summary>
        /// <param name="editorBaseType">editorBaseType</param>
        /// <returns>object</returns>
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
            for (int i = 0; i < this.Count; i++)
            {

                CustomProperty prop = (CustomProperty) this[i];
                newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);

            }

            return new PropertyDescriptorCollection(newProps);
        }

        public PropertyDescriptorCollection GetProperties()
        {

            return TypeDescriptor.GetProperties(this, true);

        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

    }

    /// <summary>
    /// Custom property class 
    /// </summary>
    public class CustomProperty
    {
        private string sName = string.Empty;
        private bool bReadOnly;
        private bool bVisible = true;
        private object objValue;
        private string _category;
        private List<uint> _path;

        public CustomProperty(string category, string sName, object value, bool bReadOnly, bool bVisible, fValue.ObjectAttributes attributes, List<uint> path)
        {
            this.sName = sName;
            this.objValue = value;
            this.bReadOnly = bReadOnly;
            this.bVisible = bVisible;
            _category = category;
            Attributes = attributes;
            Path = path;
        }

        public fValue.ObjectAttributes Attributes { get; set; }
        public bool ReadOnly
        {
            get { return bReadOnly; }
        }

        public string Name
        {
            get { return sName; }
        }

        public bool Visible
        {
            get { return bVisible; }
        }

        public object Value
        {
            get { return objValue; }
            set { objValue = value; }
        }
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public List<uint> Path { get { return _path; } set { _path = value; } }
    }

    /// <summary>
    /// Custom PropertyDescriptor
    /// </summary>
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        private readonly CustomProperty m_Property;

        public CustomPropertyDescriptor(ref CustomProperty myProperty, Attribute[] attrs)
            : base(myProperty.Name, attrs)
        {
            m_Property = myProperty;
        }

        #region PropertyDescriptor specific

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return null; }
        }

        public override object GetValue(object component)
        {
            return m_Property.Value;
        }

        public override string Description
        {
            get { return m_Property.Name; }
        }

        public override string Category
        {
            get { return m_Property.Category; }
        }

        public override string DisplayName
        {
            get { return m_Property.Name; }

        }

        public override bool IsReadOnly
        {
            get { return m_Property.ReadOnly; }
        }

        public override void ResetValue(object component)
        {
            //Have to implement
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override void SetValue(object component, object value)
        {
            m_Property.Value = value;
        }

        public override Type PropertyType
        {
            get { return m_Property.Value.GetType(); }
        }

        #endregion
    }

    public class FarCry3Attribute
    {
        public static uint GetIdent(string fString)
        {
            return Checksum.CRC32.Calculate(Encoding.ASCII.GetBytes(fString));
        }

        public static List<uint> GetAccessIdsFromPath(string fPath)
        {
            var accessIds = new List<uint>();
            do
            {
                string elementName;
                ObDissectName(fPath, out elementName, out fPath);
                accessIds.Add(GetIdent(elementName));
            } while (fPath.Length != 0x00);
            return accessIds;
        }

        public static void ObDissectName(string path, out string firstName, out string remainingName)
        {
            if (path.Length == 0x00)
            {
                firstName = "";
                remainingName = "";
                return;
            }

            var startIndex = path[0x00] == '\\' ? 0x01 : 0x00;
            var idx = path.IndexOf('\\', startIndex);

            if (idx == -1)
            {
                firstName = path.Substring(startIndex, path.Length - startIndex);
                remainingName = "";
            }
            else
            {
                firstName = path.Substring(startIndex, idx - startIndex);
                remainingName = path.Substring(++idx, path.Length - idx);
            }
        }
    }
}
    
