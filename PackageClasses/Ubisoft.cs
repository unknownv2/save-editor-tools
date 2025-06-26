using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarCry;
using System.Xml;

namespace Ubisoft
{
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
            //_version = float.Parse(root.Attributes["version"].Value);
            foreach (XmlNode node in root.ChildNodes)
            {
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
                case "single":
                case "float":
                case "float32":
                    {
                        return new fFloat { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "id":
                case "ident":
                    {
                        return new fIdent{ Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "array":
                    {
                        //return new fArray((fValue.ObjectAttributes)(Enum.Parse(typeof(fValue.ObjectAttributes), xmlNode.Attributes["type"].Value, true)))
                        //{ Name = xmlNode.Attributes["name"].Value };
                        return new fArray { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
                case "value":
                    {
                        return new fValue { Name = xmlNode.Attributes["name"].Value, Visible = bool.Parse(xmlNode.Attributes["visible"].Value) };
                    }
            }
            return null;
        }
    }
    public class Dunia
    {
        public class SaveAttributes
        {
            public uint ItemId;
            public long Position;
            public int ItemLength;
        }
        public class SaveEntry
        {
            public uint ItemId;
            public Dictionary<uint, SaveAttributes> Attributes;
            public List<SaveEntry> Children;
        }
    }
}
