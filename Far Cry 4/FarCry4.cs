using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FarCry;
using Horizon.Functions;

namespace Horizon.PackageEditors.Far_Cry_4
{
    public partial class FarCry4 : EditorControl
    {
        private FarCry4Save SaveGame;
        private static string _campaignSavePlugin;
        private List<fValue> _saveProperties;
        CustomClass myProperties = new CustomClass();
        public FarCry4()
        {
            InitializeComponent();
            TitleID = FormID.FarCry4;
#if INT2
            btnExtract.Visible = true;
#endif
        }

        public override bool Entry()
        {
            if (!OpenStfsFile(0x00))
                return false;

            SaveGame = new FarCry4Save(IO);

            //if (_campaignSavePlugin == null)
            //    _campaignSavePlugin = File.ReadAllText(@"E:\Projects\Far Cry 4\CampaignSave.fc4");

            if (_campaignSavePlugin == null)
                _campaignSavePlugin = Global.base64Decode(SettingAsString(48));

            CreateComponents();

            return true;
        }
        public override void Save()
        {
            for (var i = 0; i < myProperties.Count; i++)
            {
                var attribute = myProperties[i];
                SetItemAttribute(SaveGame.MainEntry, attribute.Path, attribute.Value, attribute.Name, attribute.Attributes);
            }

            SaveGame.Save();
        }

        private void BtnClickExtract(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { FileName = "save.bin" };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            SaveGame.Export().Save(sfd.FileName);
        }

        private void CreateComponents()
        {
            XmlParser parser;
            /*
#if INT2
            parser = new XmlParser(@"C:\Users\Thierry\Desktop\Game Projects\Far Cry 3\CampaignSave.fc3", true);
#else
            parser = new XmlParser(_campaignSavePlugin, false);
#endif
            */
            parser = new XmlParser(_campaignSavePlugin, false);
            _saveProperties = parser.ValuesList;
            foreach (fPropertyBag propertyBag in _saveProperties)
            {
                var bagAccess = FarCry3Attribute.GetAccessIdsFromPath(string.Format("{0}\\{1}", "SaveGameFileRoot", propertyBag.Name));
                foreach (fValue attribute in propertyBag.Values)
                {
                    AddValueToPropertyGrid(attribute, propertyBag, bagAccess);
                }
            }
            propGridCampaignSave.SelectedObject = myProperties;
        }
        private void AddValueToPropertyGrid(fValue attribute, fPropertyBag propertyBag, List<uint> bagAccess)
        {
            if (!attribute.Visible)
                return;

            if (string.IsNullOrEmpty(propertyBag.Name))
                propertyBag.Name = "SaveGameFileRoot";

            switch (attribute.Attributes)
            {
                case fValue.ObjectAttributes.Boolean:
                    myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                        Convert.ToBoolean(GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name)[0x00]), false, true, attribute.Attributes, bagAccess));
                    break;
                case fValue.ObjectAttributes.Int32:
                    myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                        GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name).ToInt32(false), false, true, attribute.Attributes, bagAccess));
                    break;
                case fValue.ObjectAttributes.Float:
                    myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                        new EndianReader(GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name), EndianType.LittleEndian).ReadSingle(), false, true, attribute.Attributes, bagAccess));
                    break;
                case fValue.ObjectAttributes.Array:
                    switch (attribute.Name)
                    {
                        /*
                    case "WikiItems":
                        myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                            new WikiItems(GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name)), false, true, attribute.Attributes));
                        break;
                        */
                        default:
                            myProperties.Add(new CustomProperty(propertyBag.Name, attribute.Name,
                                GetItemAttribute(SaveGame.MainEntry, bagAccess, attribute.Name), false, true, attribute.Attributes, bagAccess));
                            break;
                    }

                    break;
                case fValue.ObjectAttributes.PropertyBag:
                    {
                        var mainProperty = (fPropertyBag)attribute;
                        var tempAccess = new List<uint>(bagAccess);
                        tempAccess.AddRange(FarCry3Attribute.GetAccessIdsFromPath(attribute.Name));
                        foreach (fValue childAttribute in mainProperty.Values)
                        {
                            AddValueToPropertyGrid(childAttribute, mainProperty, tempAccess);
                        }

                    }
                    break;
            }
        }
        /*
        private void DisplayStats()
        {                        
            _ammoPathIds = new uint[]
                                        {
                                            0x9c44b3a3,
                                            0xD27446A8,
                                            0xa99a06b3,
                                            0x9c989aa7,
                                            0x6252fdff,
                                            0xa115f62d,
                                            0x911dd85f,
                                            0x33dcc895,
                                            0x33dcc895,
                                            0x20dfc649
                                        };

            _ammoAccessIds = new uint[]
                                    {
                                        0x2cecf817
                                    };
  
        }
        */
        private byte[] GetItemAttribute(FarCry3SaveEntry entry, List<uint> accessIds, string attribute)
        {
            Dictionary<uint, int[]> searchDict = new Dictionary<uint, int[]>();
            FarCry3SaveEntry originalEntry = entry;
            int accessSearchIndex = 0x00;
            while ((entry = FindItemEntryFromId(entry, accessIds, searchDict, ref accessSearchIndex)) == null)
            {
                entry = originalEntry;
            }
            return SaveGame.GetEntryData(entry.Attributes[FarCry3Attribute.GetIdent(attribute)]);
        }
        private FarCry3SaveEntry FindItemEntryFromId(FarCry3SaveEntry entry, List<uint> accessIds, Dictionary<uint, int[]> searchDict, ref int accessSearchIndex)
        {
            uint currentSearchId = accessIds[accessSearchIndex];
            foreach (uint accessId in accessIds)
            {
                if (!searchDict.ContainsKey(accessId) && entry != null)
                    searchDict.Add(accessId, new[] { 0x00, entry.Children.Count });

                entry = SaveGame.GetEntryFromItemId(accessId, entry, searchDict.ContainsKey(accessId) ? searchDict[accessId][0x00] : 0x00);

                if (entry != null) continue;
                if (searchDict[currentSearchId][0x00] < (searchDict[currentSearchId][0x01] - 1))
                    searchDict[accessIds[accessSearchIndex]][0x00]++;
                else
                {
                    searchDict[accessIds[accessSearchIndex]][0x00] = 0x00;
                    accessSearchIndex++;
                }

                break;
            }
            return entry;
        }
        private void SetItemAttribute(FarCry3SaveEntry entry, List<uint> accessIds, object value, string property, fValue.ObjectAttributes attributes)
        {
            Dictionary<uint, int[]> searchDict = new Dictionary<uint, int[]>();
            FarCry3SaveEntry originalEntry = entry;
            int accessSearchIndex = 0x00;
            while ((entry = FindItemEntryFromId(entry, accessIds, searchDict, ref accessSearchIndex)) == null)
            {
                entry = originalEntry;
            }
            switch (attributes)
            {
                case fValue.ObjectAttributes.Int32:
                    SaveGame.SetAttribute(entry, Convert.ToInt32(value), FarCry3Attribute.GetIdent(property));
                    break;
                case fValue.ObjectAttributes.Boolean:
                    SaveGame.SetAttribute(entry, Convert.ToBoolean(value), FarCry3Attribute.GetIdent(property));
                    break;
                case fValue.ObjectAttributes.Float:
                    SaveGame.SetAttribute(entry, Convert.ToSingle(value), FarCry3Attribute.GetIdent(property));
                    break;
            }
        }
    }
}
