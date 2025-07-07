using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using DevComponents.AdvTree;
using System.IO;
using ForzaMotorsport;
using Forza4;
using Horizon.Server;

namespace Horizon.PackageEditors.Forza_4
{
    public partial class Forza4Profile : EditorControl
    {
        //public static readonly string FID = "4D530910";

        private Forza4.Forza4Profile ForzaProfile;
        private Forza4PlayerDatabase PlayerDatabase;
        private EndianIO PlayerDatabaseIO;
        private List<List<string>> LiveryFolders;
        private Forz4CarDesign Livery;
        private Forza4KeyMarshal KeyMarshal;
        private Forza4Garage CarGarage;
        private byte[] Creator, CarBinKey;

        private static int[] AffinityLevels, PlayerLevels, UnicornCarId;
        internal struct CarManufacturerEntry
        {
            internal int GroupId;
            internal string Manufacturer;
        }
        //private static List<string> Manufacturers;
        private static List<CarManufacturerEntry> CarMakers;
        //private static Dictionary<int, string> CarManufacturers;

        private string GarageSchema;
        //private static string[] CarListing;
        private static string[] UnicornCarListing;
        private static Dictionary<int, string> CarData;
        private Dictionary<int, string> CarThumbUrls;
        private Dictionary<string, int>  CarNameData;
        private ForzaSQLite Database = new ForzaSQLite(null, null);
        private List<Forza4Garage.Forza4GarageCar> CarList;

        private static Dictionary<int, byte[]> _carThumbs, _carBins; 
        public Forza4Profile()
        {
            InitializeComponent();
            TitleID = FormID.Forza4Profile;
            
        }

        public override bool Entry()
        {
            if (!this.OpenStfsFile("ForzaProfile"))
                return false;

            byte[] CreatorId = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(this.Package.Header.Metadata.Creator));

            PlayerDatabaseIO = this.Package.StfsContentPackage.GetEndianIO("PlayerDatabase");
            this.PlayerDatabase = new Forza4PlayerDatabase(PlayerDatabaseIO, 
                SettingAsByteArray(110),
                SettingAsByteArray(200), CreatorId, Database);

            this.ForzaProfile = new Forza4.Forza4Profile(this.IO, this.Package.Header.Metadata.Creator,
                SettingAsByteArray(135), SettingAsByteArray(147));

            this.CarGarage = new Forza4Garage(PlayerDatabaseIO, SettingAsByteArray(110), SettingAsByteArray(200), CreatorId, Database);

            if (!this.LoadSaveData())
                return false;

            this.LoadTreeSettings();
            this.LoadForzaLiveryKeys();
            this.LoadLiveries();

            if (!this.LoadGarageData())
                return false;

            if (!this.LoadAffinityData())
                return false;

            return true;
        }

        public override void Save()
        {
            this.SaveGarageData();
            this.SaveAffinityData();

            this.UpdateNode("Main;Credits", this.numCredits.Value.ToString());
            this.UpdateNode("Main;XP", this.numXP.Value.ToString());
            this.UpdateNode("Main;Level", GetLevelFromExp(this.numXP.Value).ToString());

            this.WriteTreeSettings();

            this.ForzaProfile.Save(); // encrypt and fix the hash of the modified save buffer
        }

        private void InitSaveData()
        {
            if (PlayerLevels == null)
            {
                PlayerLevels = new int[]
                {
                    0,5000,10000,18000,26000,35000,44000,53000,63000,73000,84000,95000,107000,119000,132000,146000,161000,177000,194000,212000,231000,251000,272000,294000,317000,341000,366000,392000,419000,447000,476000,506000,537000,569000,602000,636000,671000,707000,744000,782000,822000,865000,911250,960750,1013500,1069750,1129500,1193250,1261000,1332750,1392500,1451500,1510500,1569500,1628500,1687500,1746500,1805500,1864500,1923500,1982500,2040500,2098500,2156500,2214500,2272500,2330500,2388500,2446500,2504500,2562500,2619500,2676500,2733500,2790500,2847500,2904500,2961500,3018500,3075500,3132500,3187500,3242500,3297500,3352500,3407500,3462500,3517500,3572500,3627500,3682500,3735000,3787500,3840000,3892500,3945000,3997500,4050000,4102500,4155000,4207500,4257500,4307500,4357500,4407500,4457500,4507500,4557500,4607500,4657500,4707500,4755000,4802500,4850000,4897500,4945000,4992500,5040000,5087500,5135000,5182500,5227500,5272500,5317500,5362500,5407500,5452500,5497500,5542500,5587500,5632500,5675000,5717500,5760000,5802500,5845000,5887500,5930000,5972500,6015000,6057500,6100000,6142500,6185000,6227500,6270000,6310000,6350000,6400000,6450000,6500000,
                    6600000,6700000,6800000,6900000,7000000,7100000,7200000,7300000,7400000,7500000,7600000,7700000,7800000,7900000,8000000,8100000,8200000,8300000,8400000,8500000,8600000,8700000,8800000,8900000,9000000,9100000,9200000,9300000,9400000,9500000,9600000,9700000,9800000,9900000,10000000,10100000,10200000,10300000,10400000,10500000,10600000,10700000,10800000,10900000,11000000,11100000,11200000,11300000,11400000,11500000,11600000,11700000,11800000,11900000,12000000,12100000,12200000,12300000,12400000,12500000,12600000,12700000,12800000,12900000,13000000,13100000,13200000,13300000,13400000,13500000,13600000,13700000,13800000,13900000,14000000,14100000,14200000,14300000,14400000,14500000,14600000,14700000,14800000,14900000,15000000,15100000,15200000,15300000,15400000,15500000,15600000,15700000,15800000,15900000,16000000,16100000,16200000,16300000,16400000,16500000,16700000,16900000,17100000,17300000,17500000,17700000,17900000,18100000,18300000,18500000,18700000,18900000,19100000,19300000,19500000,19700000,19900000,20100000,20300000,20500000,20700000,20900000,21100000,21300000,21500000,21700000,21900000,22100000,22300000,22500000,22700000,
                    22900000,23100000,23300000,23500000,23700000,23900000,24100000,24300000,24500000,24700000,24900000,25100000,25300000,25500000,25700000,25900000,26100000,26300000,26500000,26700000,26900000,27100000,27300000,27500000,27700000,27900000,28100000,28300000,28500000,28700000,28900000,29100000,29300000,29500000,29700000,29900000,30100000,30300000,30500000,30700000,30900000,31100000,31300000,31500000,31700000,31900000,32100000,32300000,32500000,32700000,32900000,33100000,33300000,33500000,33700000,33900000,34100000,34300000,34500000,34700000,34900000,35100000,35300000,35500000,35700000,35900000,36100000,36300000,36500000,36900000,37300000,37700000,38100000,38500000,38900000,39300000,39700000,40100000,40500000,40900000,41300000,41700000,42100000,42500000,42900000,43300000,43700000,44100000,44500000,44900000,45300000,45700000,46100000,46500000,46900000,47300000,47700000,48100000,48500000,48900000,49300000,49700000,50100000,50500000,50900000,51300000,51700000,52100000,52500000,52900000,53300000,53700000,54100000,54500000,54900000,55300000,55700000,56100000,56500000,56900000,57300000,57700000,58100000,58500000,58900000,59300000,59700000,
                    60100000,60500000,60900000,61300000,61700000,62100000,62500000,62900000,63300000,63700000,64100000,64500000,64900000,65300000,65700000,66100000,66500000,66900000,67300000,67700000,68100000,68500000,68900000,69300000,69700000,70100000,70500000,70900000,71300000,71700000,72100000,72500000,72900000,73300000,73700000,74100000,74500000,74900000,75300000,75700000,76100000,76500000,77300000,78100000,78900000,79700000,80500000,81300000,82100000,82900000,83700000,84500000,85300000,86100000,86900000,87700000,88500000,89300000,90100000,90900000,91700000,92500000,93300000,94100000,94900000,95700000,96500000,97300000,98100000,98900000,99700000,100500000,101300000,102100000,102900000,103700000,104500000,105300000,106100000,106900000,107700000,108500000,109300000,110100000,110900000,111700000,112500000,113300000,114100000,114900000,115700000,116500000,117300000,118100000,118900000,119700000,120500000,121300000,122100000,122900000,123700000,124500000,125300000,126100000,126900000,127700000,128500000,129300000,130100000,130900000,131700000,132500000,133300000,134100000,134900000,135700000,136500000,137300000,138100000,138900000,139700000,140500000,
                    141300000,142100000,142900000,143700000,144500000,145300000,146100000,146900000,147700000,148500000,149300000,150100000,150900000,151700000,152500000,153300000,154100000,154900000,155700000,156500000,158100000,159700000,161300000,162900000,164500000,166100000,167700000,169300000,170900000,172500000,174100000,175700000,177300000,178900000,180500000,182100000,183700000,185300000,186900000,188500000,190100000,191700000,193300000,194900000,196500000,198100000,199700000,201300000,202900000,204500000,206100000,207700000,209300000,210900000,212500000,214100000,215700000,217300000,218900000,220500000,222100000,223700000,225300000,226900000,228500000,230100000,231700000,233300000,234900000,236500000,238100000,239700000,241300000,242900000,244500000,246100000,247700000,249300000,250900000,252500000,254100000,255700000,257300000,258900000,260500000,262100000,263700000,265300000,266900000,268500000,270100000,271700000,273300000,274900000,276500000,278100000,279700000,281300000,282900000,284500000,286100000,287700000,289300000,290900000,292500000,294100000,295700000,297300000,298900000,300500000,302100000,303700000,305300000,306900000,308500000,
                    310100000,311700000,313300000,314900000,316500000,318100000,319700000,321300000,322900000,324500000,326100000,327700000,329300000,330900000,332500000,334100000,335700000,337300000,338900000,340500000,342100000,343700000,345300000,346900000,348500000,350100000,351700000,353300000,354900000,356500000,358100000,359700000,361300000,362900000,364500000,366100000,367700000,369300000,370900000,372500000,374100000,375700000,377300000,378900000,380500000,382100000,383700000,385300000,386900000,388500000,390100000,391700000,393300000,394900000,396500000,398100000,399700000,401300000,402900000,404500000,406100000,407700000,409300000,410900000,412500000,414100000,415700000,417300000,418900000,420500000,422100000,423700000,425300000,426900000,428500000,430100000,431700000,433300000,434900000,436500000,438100000,439700000,441300000,442900000,444500000,446100000,447700000,449300000,450900000,452500000,454100000,455700000,457300000,458900000,460500000,462100000,463700000,465300000,466900000,468500000,470100000,471700000,473300000,474900000,476500000,478100000,479700000,481300000,482900000,484500000,486100000,487700000,489300000,490900000,492500000,
                    494100000,495700000,497300000,498900000,500500000,502100000,503700000,505300000,506900000,508500000,510100000,511700000,513300000,514900000,516500000,518100000,519700000,521300000,522900000,524500000,526100000,527700000,529300000,530900000,532500000,534100000,535700000,537300000,538900000,540500000,542100000,543700000,545300000,546900000,548500000,550100000,551700000,553300000,554900000,556500000,558100000,559700000,561300000,562900000,564500000,566100000,567700000,569300000,570900000,572500000,574100000,575700000,577300000,578900000,580500000,582100000,583700000,585300000,586900000,588500000,590100000,591700000,593300000,594900000,596500000,598100000,599700000,601300000,602900000,604500000,606100000,607700000,609300000,610900000,612500000,614100000,615700000,617300000,618900000,620500000,622100000,623700000,625300000,626900000,628500000,630100000,631700000,633300000,634900000,636500000,638100000,639700000,641300000,642900000,644500000,646100000,647700000,649300000,650900000,652500000,654100000,655700000,657300000,658900000,660500000,662100000,663700000,665300000,666900000,668500000,670100000,671700000,673300000,674900000,676500000,
                    678100000,679700000,681300000,682900000,684500000,686100000,687700000,689300000,690900000,692500000,694100000,695700000,697300000,698900000,700500000,702100000,703700000,705300000,706900000,708500000,710100000,711700000,713300000,714900000,716500000,718100000,719700000,721300000,722900000,724500000,726100000,727700000,729300000,730900000,732500000,734100000,735700000,737300000,738900000,740500000,742100000,743700000,745300000,746900000,748500000,750100000,751700000,753300000,754900000,756500000,758100000,759700000,761300000,762900000,764500000,766100000,767700000,769300000,770900000,772500000,774100000,775700000,777300000,778900000,780500000,782100000,783700000,785300000,786900000,788500000,790100000,791700000,793300000,794900000,796500000,798100000,799700000,801300000,802900000,804500000,806100000,807700000,809300000,810900000,812500000,814100000,815700000,817300000,818900000,820500000,822100000,823700000,825300000,826900000,828500000,830100000,831700000,833300000,834900000,836500000,838100000,839700000,841300000,842900000,844500000,846100000,847700000,849300000,850900000,852500000,854100000,855700000,857300000,858900000,860500000,
                    862100000,863700000,865300000,866900000,868500000,870100000,871700000,873300000,875000000

                };
            }
            if (this.cmbLevel.Items.Count == 0)
            {
                for (int x = 0; x < 1000; x++)
                {
                    this.cmbLevel.Items.Add(x.ToString());
                }
            }
        }
        private bool LoadSaveData()
        {
            try
            {
                this.InitSaveData();

                this.ForzaProfile.SaveIO.In.SeekTo(this.ForzaProfile.Profile.GetEntryAddress("Credits"));
                this.numCredits.Value = this.ForzaProfile.SaveIO.In.ReadUInt32();
                this.ForzaProfile.SaveIO.In.SeekTo(this.ForzaProfile.Profile.GetEntryAddress("XP"));
                uint Experience = this.ForzaProfile.SaveIO.In.ReadUInt32();
                this.ForzaProfile.SaveIO.In.SeekTo(this.ForzaProfile.Profile.GetEntryAddress("Level"));
                int level = this.ForzaProfile.SaveIO.In.ReadInt32();
                if (GetLevelFromExp(Experience) == level)
                {
                    this.cmbLevel.Text = level.ToString();
                    this.numXP.Value = Experience;
                }
                else
                {
                    throw new ForzaException("invalid Player level and experience detected.");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private int GetLevelFromExp(decimal Exp)
        {
            for (int x = 0; x < 999; x++)
            {
                if (Exp < PlayerLevels[x + 1])
                    return x;
            }
            return 999;
        }
        private void LoadTreeSettings()
        {
            this.advPropertyTree.Nodes.Clear();
            this.advPropertyTree.BeginUpdate();

            for (int i = 0; i < this.ForzaProfile.Profile.ProfileSchemaEntries.Count;)
            {
                string Parent = this.ForzaProfile.Profile.ProfileSchemaEntries[i].Name;
                if (this.ForzaProfile.Profile.ProfileSchemaEntries[i].Type == ForzaTypes.PropertyBag)
                {
                    int ChildCount = this.ForzaProfile.Profile.ProfileSchemaEntries[i++].PropertyChildCount;
                    for (int x = 0; x < ChildCount; x++)
                    {
                        this.addSetting(Parent + "\\" + this.ForzaProfile.Profile.ProfileSchemaEntries[i].Name, FormatSettingValue(this.ForzaProfile.Profile.ProfileSchemaEntries[i++]), advPropertyTree);
                    }
                }
                else
                {
                    this.addSetting("Other\\" + this.ForzaProfile.Profile.ProfileSchemaEntries[i].Name, FormatSettingValue(this.ForzaProfile.Profile.ProfileSchemaEntries[i++]), advPropertyTree);
                }
            }

            this.advPropertyTree.EndUpdate();
        }
        private void WriteTreeSettings()
        {
            foreach (Node PropertyBagNode in this.advPropertyTree.Nodes)
            {
                this.WriteProperty(PropertyBagNode);
            }
        }
        private void WriteProperty(Node PropertyBagNode)
        {
            foreach (Node propertyNode in PropertyBagNode.Nodes)
            {
                var Property = this.ForzaProfile.Profile.GetEntry(propertyNode.Name.Split(';')[1]);
                this.ForzaProfile.SaveIO.SeekTo(Property.Address);
                this.WritePropertyValue(this.ForzaProfile.SaveIO.Out, Property.Type, propertyNode.Cells[1].Text);
            }
        }
        private string FormatSettingValue(ForzaProfileEntry Entry)
        {
            switch (Entry.Type)
            {
                case ForzaTypes.CarId:
                case ForzaTypes.UInt32:
                    return BitConverter.ToUInt32(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.Bool:
                    return BitConverter.ToBoolean(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.Uint16:
                    return BitConverter.ToUInt16(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.UInt8:
                    return Entry.Value[0].ToString();
                case ForzaTypes.Float32:
                    return BitConverter.ToSingle(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                case ForzaTypes.UInt64:
                    return BitConverter.ToUInt64(Horizon.Functions.Global.convertToBigEndian(Entry.Value), 0).ToString();
                default:
                    throw new ForzaException(string.Format("could not format property type {0:D}", Entry.Type));
            }
        }
        private void WritePropertyValue(EndianWriter writer, ForzaTypes Type, string Value)
        {
            switch (Type)
            {
                case ForzaTypes.CarId:
                case ForzaTypes.UInt32:
                    writer.Write(uint.Parse(Value));
                    break;
                case ForzaTypes.Bool:
                    writer.Write(bool.Parse(Value));
                    break;
                case ForzaTypes.Uint16:
                    writer.Write(ushort.Parse(Value));
                    break;
                case ForzaTypes.UInt8:
                    writer.Write(byte.Parse(Value));
                    break;
                case ForzaTypes.Float32:
                    writer.Write(float.Parse(Value));
                    break;
                case ForzaTypes.UInt64:
                    writer.Write(ulong.Parse(Value));
                    break;
                default:
                    throw new ForzaException(string.Format("could not parse property type {0:D}", Type));
            }
        }
        private void addSetting(string setting, string value, AdvTree Tree)
        {
            string[] setChilds = setting.Split('\\');
            string curPath = String.Empty;
            Node parentNode = null;
            for (int x = 0; x < setChilds.Length; x++)
            {
                curPath += setChilds[x];
                Node findNode = Tree.FindNodeByName(curPath);
                if (findNode == null)
                {
                    Node newNode = new Node(setChilds[x]);
                    newNode.Name = curPath;
                    newNode.ExpandVisibility = eNodeExpandVisibility.Auto;
                    if (x == setChilds.Length - 1)
                    {
                        Cell newCell = new Cell(value);
                        newCell.Editable = true;
                        newNode.Cells.Add(newCell);
                    }
                    parentNode = parentNode == null ? Tree.Nodes[Tree.Nodes.Add(newNode)] : parentNode.Nodes[parentNode.Nodes.Add(newNode)];
                }
                else
                    parentNode = findNode;
                curPath += Tree.PathSeparator;
            }
        }
        private void addSetting(string setting, List<string> values, AdvTree Tree)
        {
            string[] setChilds = setting.Split('\\');
            string curPath = String.Empty;
            Node parentNode = null;
            for (int x = 0; x < setChilds.Length; x++)
            {
                curPath += setChilds[x];
                Node findNode = Tree.FindNodeByName(curPath);
                if (findNode == null)
                {
                    Node newNode = new Node(setChilds[x]);
                    newNode.Name = curPath;
                    newNode.ExpandVisibility = eNodeExpandVisibility.Auto;
                    if (x == setChilds.Length - 1)
                    {
                        for (int i = 0; i < values.Count; i++)
                        {
                            Cell newCell = new Cell(values[i]);
                            newCell.Editable = true;
                            newNode.Cells.Add(newCell);
                        }
                    }
                    parentNode = parentNode == null ? Tree.Nodes[Tree.Nodes.Add(newNode)] : parentNode.Nodes[parentNode.Nodes.Add(newNode)];
                }
                else
                    parentNode = findNode;
                curPath += Tree.PathSeparator;
            }
        }
        // Sync the values from the 'Simple' Tab to the 'Advanced' Tab
        private void EditPropCrValidated(object sender, EventArgs e)
        {
            this.UpdateNode("Main;Credits", (sender as NumericUpDown).Value.ToString());
        }
        private void EditPropXPValidated(object sender, EventArgs e)
        {
            this.UpdateNode("Main;XP", (sender as NumericUpDown).Value.ToString());
        }
        private void EditPropLvlValidated(object sender, EventArgs e)
        {
            this.UpdateNode("Main;Level", (sender as DevComponents.DotNetBar.Controls.ComboBoxEx).SelectedIndex.ToString());
        }

        private void UpdateNode(string NodeName, string Value)
        {
            if (this.advPropertyTree.Nodes.Count > 0)
            {
                var node = this.advPropertyTree.Nodes.Find(NodeName, true)[0];
                node.BeginEdit();
                node.Cells[1].Text = Value.ToString();
                node.EndEdit(false);
            }
        }

        // Sync the values from the 'Advanced' Tab to the 'Simple' Tab
        private void EditPropertyTreeCellEdit(object sender, CellEditEventArgs e)
        {
            if (e.Cell.Parent.Cells[0].Text != (e.Cell.Text))
            {
                switch (e.Cell.Parent.Name)
                {
                    case "Main;Credits":
                        this.numCredits.Value = decimal.Parse(e.NewText);
                        break;
                    case "Main;XP":
                        this.numXP.Value = decimal.Parse(e.NewText);
                        break;
                    case "Main;Level":
                        this.cmbLevel.SelectedIndex = int.Parse(e.NewText);
                        break;
                }
            }
        }
        private void CmbLevel_SelectedLevelChanged(object sender, EventArgs e)
        {            
            this.numXP.Value = PlayerLevels[this.cmbLevel.SelectedIndex];
        }

        // Livery loading functions
        private void LoadForzaLiveryKeys()
        {
            if (KeyMarshal == null) // so we dont re-initialize the key marshal when a new file is opened in an old form
            {
                KeyMarshal = new Forza4KeyMarshal();

                this.KeyMarshal.AddKey("LiveryCatalogAES", SettingAsByteArray(127));
                this.KeyMarshal.AddKey("LiveryCatalogSHA", SettingAsByteArray(170));
            }
        }
        private void LoadLiveries()
        {
            this.Creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(this.Package.Header.Metadata.Creator));

            int LiveryCount = 0;
            uint SearchOffset = 0;
            LiveryFolders = new List<List<string>>();
            var FolderFcb = this.Package.StfsContentPackage.GetFileStream("Liveries").Fcb;
            var DirectoryEntry = new XContent.StfsDirectoryEntry();

            List<uint> FileSizes = new List<uint>();
            while(this.Package.StfsContentPackage.StfsFindNextDirectoryEntry(FolderFcb, SearchOffset, null, ref DirectoryEntry) == 0)
            {
                LiveryFolders.Add(new List<string>());
                LiveryFolders[LiveryCount].Add("Liveries");
                LiveryFolders[LiveryCount].Add(this.Package.StfsContentPackage.StfsFindNextDirectoryName(FolderFcb, SearchOffset));
                LiveryFolders[LiveryCount].Add("Liveries\\" + LiveryFolders[LiveryCount++][1]);

                SearchOffset = DirectoryEntry.DirectoryEntryByteOffset + 0x40;
                FileSizes.Add(DirectoryEntry.FileBounds.Filesize);
            }

            for (int x = 0; x < LiveryFolders.Count; x++)
            {
                this.addSetting(LiveryFolders[x][1], (FileSizes[x] - 52).ToString() + " bytes", advLiveriesTree);
            }
        }
        private void LoadLiveryFromTreeIndex()
        {
            CloseLivery();

            Livery = new Forz4CarDesign(this.Package.StfsContentPackage.GetEndianIO(LiveryFolders[advLiveriesTree.SelectedIndex][2]), KeyMarshal, Creator);

            btnIsUnlocked.Checked = Livery.IsUnlocked;
        }
        private void CloseLivery()
        {
            if (Livery != null)
                Livery.Close();
        }

        private void BtnClick_LiveryLock(object sender, EventArgs e)
        {
            if (Livery == null)
            {
                Horizon.Functions.UI.messageBox("Please select a livery file before you attempt to unlock one.");
            }
            else
            {
                this.btnIsUnlocked.Checked = !this.btnIsUnlocked.Checked;
            }
        }
        private void BtnClick_LiveryLockChanged(object sender, EventArgs e)
        {
            Livery.IsUnlocked = (sender as DevComponents.DotNetBar.ButtonX).Checked;            
        }
        private void BtnClick_LiverySave(object sender, EventArgs e)
        {
            Livery.UnlockLockCarDesign();
            Livery.Save();
        }
        private void BtnClick_UnlockAllLiveries(object sender, EventArgs e)
        {
            CloseLivery();
            int unlockedCount = 0;

            for (int x = 0; x < this.LiveryFolders.Count; x++)
            {
                Livery = new Forz4CarDesign(this.Package.StfsContentPackage.GetEndianIO(LiveryFolders[x][2]), KeyMarshal, Creator);
                if (!Livery.IsUnlocked)
                {
                    Livery.UnlockLockCarDesign(true);
                    Livery.Save();
                    unlockedCount++;
                }
            }
            if (unlockedCount > 0)
            {
                LoadLiveryFromTreeIndex();
                Horizon.Functions.UI.messageBox(string.Format("Successfully unlocked {0} liveries!", unlockedCount));
            }
            else
            {
                Horizon.Functions.UI.messageBox("Could not find any locked liveries.");
            }
        }
        private void BtnClick_LiveryExtractBinary(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Livery.Extract().Save(sfd.FileName);
            }
        }
        private void BtnClick_LiveryInjectBinary(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Package.StfsContentPackage.InjectFileFromArray(LiveryFolders[advLiveriesTree.SelectedIndex][2], Livery.Inject(new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read).ToArray()));
                LoadLiveryFromTreeIndex();
            }
        }
        private void TreeClick_SelectedLivIndexChanged(object sender, EventArgs e)
        {
            LoadLiveryFromTreeIndex();
        }
        private void BtnClick_AddAllUnicornCars(object sender, EventArgs e)
        {
            this.AddAllUnicornCars();
        }

        //Affinity functions
        /*
        private string GroupIdToManufacturer(int GroupId)
        {
            if (GroupId > 105)
                throw new ForzaException(string.Format("Manufacturer GroupId is out of range. Report=0x{0:X8}.", GroupId));

            switch (GroupId)
            {
                case 1:
                    return "Acura";
                case 2:
                    return "Aston Martin";
                case 3:
                    return "Audi";
                case 4:
                    return "Bentley";
                case 6:
                    return "BMW";
                case 7:
                    return "Buick";
                case 8:
                    return "Cadillac";
                case 9:
                    return "Chevrolet";
                case 10:
                    return "Chrysler";
                case 11:
                    return "Dodge";
                case 13:
                    return "Ferrari";
                case 14:
                    return "Ford";
                case 16:
                    return "Honda";
                case 17:
                    return "Hyundai";
                case 18:
                    return "Infiniti";
                case 19:
                    return "Jaguar";
                case 20:
                    return "Koenigsegg";
                case 21:
                    return "Lamborghini";
                case 22:
                    return "Lancia";
                case 23:
                    return "Lexus";
                case 24:
                    return "Lotus";
                case 25:
                    return "Maserati";
                case 26:
                    return "Mazda";
                case 27:
                    return "McLaren";
                case 28:
                    return "Mercedes";
                case 29:
                    return "MINI";
                case 30:
                    return "Mitsubishi";
                case 31:
                    return "Nissan";
                case 32:
                    return "Opel";
                case 34:
                    return "Pagani";
                case 35:
                    return "Panoz";
                case 36:
                    return "Peugeot";
                case 38:
                    return "Pontiac";
                case 39:
                    return "Porsche";
                case 41:
                    return "Renault";
                case 42:
                    return "Saab";
                case 43:
                    return "Saleen";
                case 44:
                    return "Saturn";
                case 45:
                    return "Scion";
                case 46:
                    return "SEAT"; 
                case 47:
                    return "Shelby";
                case 48:
                    return "Subaru";
                case 49:
                    return "Toyota";
                case 50:
                    return "TVR";
                case 51:
                    return "Vauxhall";
                case 52:
                    return "Volkswagen";
                case 53:
                    return "Volvo";
                case 54:
                    return "Alfa Romeo";
                case 55:
                    return "Land Rover";
                case 56:
                    return "Citroen";
                case 57:
                    return "Fiat";
                case 58:
                    return "Bugatti";
                case 59:
                    return "Holden";
                case 60:
                    return "Gumpert";
                case 61:
                    return "Bertone";
                case 62:
                    return "Devon";
                case 63:
                    return "Joss";
                case 64:
                    return "Morgan";
                case 65:
                    return "Mosler";
                case 66:
                    return "Radical";
                case 67:
                    return "Rossion";
                case 68:
                    return "Spada";
                case 69:
                    return "Spyker";
                case 70:
                    return "SSC";
                case 71:
                    return "Wiesmann";
                case 72:
                    return "Noble";
                case 74:
                    return "Kia";
                case 75:
                    return "Suzuki";
                case 76:
                    return "AMC";
                case 77:
                    return "DeTomaso";
                case 78:
                    return "DeLorean";
                case 79:
                    return "Mercury";
                case 80:
                    return "GMC";
                case 81:
                    return "Tesla";
                case 82:
                    return "Oldsmobile";
                case 85:
                    return "Hummer";
                case 86:
                    return "AMG";
                case 87:
                    return "Eagle";
                case 88:
                    return "Jeep";
                case 89:
                    return "Plymouth";
                case 90:
                    return "Abarth";
                case 91:
                    return "RUF";
                case 92:
                    return "Ascari";
                case 95:
                    return "smart";
                case 96:
                    return "Ultima";
                case 97:
                    return "Austin-Healey";
                case 98:
                    return "MG";
                case 99:
                    return "Test 99";
                case 101:
                    return "Hudson";
                case 102:
                    return "Hennessey";
                case 104:
                    return "Viper";
                case 105:
                    return "Lincoln";
                default:
                    return string.Empty;
                    //throw new ForzaException(string.Format("unsupported groupId: {0:D4}",GroupId));
            }
        }
        */
        private int ManufacturerXPToLevel(int Exp)
        {
            for (int x = 0; x < 50; x++)
            {
                if (Exp < AffinityLevels[x+1])
                    return x;
            }
            return 50;
        }
        private string ConvertSecondsToTime(int seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds);

            return answer;
        }
        private int ConvertTimeToSeconds(string Time)
        {
            TimeSpan t;
            TimeSpan.TryParse(Time.Replace("h", "").Replace("m", "").Replace("s", ""), out t);
            return (int)t.TotalSeconds;
        }
        private double ConvertMetersToMiles(int meters)
        {
            double MeterToMile = 0.000621371192;
            double Meters = (double)meters;
            return (Meters * MeterToMile);
            //string miles = string.Format("{0:0.##} miles", (Meters * MeterToMile));
            //return miles;
        }

        [Conditional("INT2")]
        private void GenerateManufacturerBin()
        {
            List<string> Manufacturers = new List<string>();
            Manufacturers.AddRange(new string[]
             {
                 "",
                 "Acura",
                 "Aston Martin",
                 "Audi",
                 "Bentley",
                 "",
                 "BMW",
                 "Buick",
                 "Cadillac",
                 "Chevrolet",
                 "Chrysler",
                 "Dodge",
                 "",
                 "Ferrari",
                 "Ford",
                 "",
                 "Honda",
                 "Hyundai",
                 "Infiniti",
                 "Jaguar",
                 "Koenigsegg",
                 "Lamborghini",
                 "Lancia",
                 "Lexus",
                 "Lotus",
                 "Maserati",
                 "Mazda",
                 "McLaren",
                 "Mercedes",
                 "MINI",
                 "Mitsubishi",
                 "Nissan",
                 "Opel",
                 "",
                 "Pagani",
                 "Panoz",
                 "Peugeot",
                 "",
                 "Pontiac",
                 "Porsche",
                 "",
                 "Renault",
                 "Saab",
                 "Saleen",
                 "Saturn",
                 "Scion",
                 "SEAT",
                 "Shelby",
                 "Subaru",
                 "Toyota",
                 "TVR",
                 "Vauxhall",
                 "Volkswagen",
                 "Volvo",
                 "Alfa Romeo",
                 "Land Rover",
                 "Citroen",
                 "Fiat",
                 "Bugatti",
                 "Holden",
                 "Gumpert",
                 "Bertone",
                 "Devon",
                 "Joss",
                 "Morgan",
                 "Mosler",
                 "Radical",
                 "Rossion",
                 "Spada",
                 "Spyker",
                 "SSC",
                 "Wiesmann",
                 "Noble",
                 "",
                 "Kia",
                 "Suzuki",
                 "AMC",
                 "DeTomaso",
                 "DeLorean",
                 "Mercury",
                 "GMC",
                 "Tesla",
                 "Oldsmobile",
                 "",
                 "",
                 "Hummer",
                 "AMG",
                 "Eagle",
                 "Jeep",
                 "Plymouth",
                 "Abarth",
                 "RUF",
                 "Ascari",
                 "",
                 "",
                 "smart",
                 "Ultima",
                 "Austin-Healey",
                 "MG",
                 "Triumph",
                 "",
                 "Hudson",
                 "Hennessey",
                 "",
                 "Viper",
                 "Lincoln"
             });

            EndianIO io = new EndianIO(@"C:\Users\Thierry\Desktop\CarManufacturers.bin", EndianType.BigEndian, true);
            for (int x = 0; x < Manufacturers.Count; x++)
            {
                io.Out.Write(x);
                io.Out.WriteAsciiString(Manufacturers[x], Manufacturers[x].Length + 1);
            }
            io.Close();
        }

        private void InitAffinityData()
        {
            if (AffinityLevels == null)
            {
                AffinityLevels = new int[] 
             {   
                 0, 2500, 9000, 16000, 24500, 35000, 48000, 64000,
                 85000,112000,146000,186500,233400,286400,346000,412000,485500,570000,658500,765000,
                 890000,1025000,1162500,1265000,1342500,1392500,1442500,1492500,1542500,1592500,1642500,
                 1692500,1742500,1792500,1842500,1892500,1942500,1992500,2042500,2092500,2142500,2192500,
                 2242500,2292500,2342500,2392500,2442500,2492500,2542500,2592500,2642500
             };

            }

            if(CarMakers == null)
            {
                var carManufacturerReader = new EndianIO(new WebClient().DownloadData(string.Format("{0}/forza4/CarManufacturers.bin", Config.baseURL)), EndianType.BigEndian, true);
                if (carManufacturerReader.Opened)
                {
                    CarMakers = new List<CarManufacturerEntry>();
                    while (carManufacturerReader.Position < carManufacturerReader.Length)
                    {
                        int groupId = carManufacturerReader.In.ReadInt32();
                        string manufacturer = carManufacturerReader.In.ReadNullTerminatedString();
                        //if (groupId == 0x00) manufacturer = "test1";
                        if(!string.IsNullOrEmpty(manufacturer))
                            CarMakers.Add(new CarManufacturerEntry() { GroupId = groupId, Manufacturer = manufacturer });
                    }
                }
            }
        }

        private void InitAffinityGrid()
        {
            this.InitAffinityData();

            // initialize the grid controls

            // add 50 levels
            this.Column4.Items.Clear();
            for (int x = 0; x < 51; x++)
            {
                this.Column4.Items.Add(x.ToString());
            }
            var carMakers = new List<string>();
            for (int x = 0; x < CarMakers.Count; x++)
                carMakers.Add(CarMakers[x].Manufacturer);

            // add the Manufacturer database list
            this.Column1.Items.AddRange(carMakers.ToArray());
        }

        private bool LoadAffinityData()
        {
            this.InitAffinityGrid();

            var manufacturers = this.PlayerDatabase.GetAffinityEntries();
            if (manufacturers != null)
            {
                for (int x = 0; x < manufacturers.Count; x++)
                {
                    // error cleaning up
                    if (manufacturers[x].GroupId == 0x00)
                    {
                        this.PlayerDatabase.DeleteGroup(0x00);
                        continue;
                    }

                    string Manufacturer = FindManufacturerNameFromId(manufacturers[x].GroupId);
                    if (Manufacturer == string.Empty) return false;
                    dgvAffinity.Rows.Add(new object[] { 
                         Manufacturer,
                         ManufacturerXPToLevel(manufacturers[x].XP),
                         manufacturers[x].XP, 
                         ConvertSecondsToTime(manufacturers[x].TimeDriven), 
                         ConvertMetersToMiles(manufacturers[x].DistanceDriven)
                     }); // manufacturer, level, xp, time driven, distance driven                
                }
            }
            else
            {
                Functions.UI.messageBox("Player does not have any Affinity data. You can still add new Manufacturer Affinity.", "Warning!", MessageBoxIcon.Warning);
            }

            return true;
        }
        private void SaveAffinityData()
        {
            var PlayerAffinity = new List<Forza4PlayerDatabase.Forza4CareerGarageAffinity>();
            foreach (DataGridViewRow row in dgvAffinity.Rows)
            {
                if (!row.IsNewRow)
                {
                    PlayerAffinity.Add(new Forza4PlayerDatabase.Forza4CareerGarageAffinity()
                        {
                            GroupId = FindManufacturerIdFromName(Convert.ToString(row.Cells[0].Value)),
                            XP = Convert.ToInt32(row.Cells[2].Value),
                            TimeDriven = ConvertTimeToSeconds(Convert.ToString(row.Cells[3].Value)),
                            DistanceDriven = (int)(Convert.ToDouble(row.Cells[4].Value) * 1609.344)
                        });
                }
            }

            this.PlayerDatabase.SetAffinityEntries(PlayerAffinity);
            this.PlayerDatabase.Save(false);
        }
        private void BtnClick_ExtractPlayerDatabase(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.FileName = "PlayerDatabase.bin";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.PlayerDatabase.Extract().Save(sfd.FileName);
            }
        }
        private void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // handle a level change
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                this.dgvAffinity.Rows[e.RowIndex].Cells[2].Value = AffinityLevels[Convert.ToInt32(this.dgvAffinity.Rows[e.RowIndex].Cells[1].Value)];
            }
        }
        private void Affinity_MaxAll(object sender, EventArgs e)
        {
            int update_count = 0;
            foreach (DataGridViewRow row in dgvAffinity.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (Convert.ToInt32(row.Cells[1].Value) <= 50)
                    {
                        row.Cells[1].Value = 50;
                        update_count++;
                    }
                }
            }
            if (update_count > 0)
            {
                Horizon.Functions.UI.messageBox(string.Format("Updated {0} Affinity levels.", update_count));
            }
            else
            {
                Horizon.Functions.UI.messageBox("All Affinity levels are already 50 or above.", string.Empty, MessageBoxIcon.Information);
            }
        }
        private void Affinity_AddAll(object sender, EventArgs e)
        {
            var carmakers = new List<string>();
            foreach (CarManufacturerEntry entry in CarMakers)
                carmakers.Add(entry.Manufacturer);

            // remove exisiting entries so we don't add duplicates
            foreach (DataGridViewRow row in dgvAffinity.Rows)
            {
                if (!row.IsNewRow)
                    carmakers.Remove(Convert.ToString(row.Cells[0].Value));
            }
            if (carmakers.Count > 0)
            {
                foreach (string manufacturer in carmakers)
                {
                    dgvAffinity.Rows.Add(new object[]
                    { 
                           manufacturer,
                           0,
                           0, 
                           ConvertSecondsToTime(0), 
                           ConvertMetersToMiles(0)
                     });
                     // manufacturer, level, xp, time driven, distance driven
                }
            }
            else
            {
                Horizon.Functions.UI.messageBox("No new manufacturers to add!", "", MessageBoxIcon.Exclamation);
            }
        }
        private int FindManufacturerIdFromName(string manufacturer)
        {
            return CarMakers.Find(entry => entry.Manufacturer == manufacturer).GroupId;
        }
        private string FindManufacturerNameFromId(int groupId)
        {
            return CarMakers.Find(entry => entry.GroupId == groupId).Manufacturer;
        }
         // Garage Functions
        private void InitGarageData()
        {
            /* Currently Unused
            CarListing = new string[]
            {
                "1969 Toyota 2000GT",
                "2004 Peugeot 206",
                "1964 Ferrari 250 GTO",
                "1954 Mercedes 300SL",
                "1967 Ferrari 330 P4",
                "1964 Ferrari F355",
                "1999 Ferrari 360",
                "1991 Ferrari 512TR",
                "2002 575M Maranello",


            };

            int[] CarIds = new int[]
            {
                247,
                248,
                249,
                251,
                252,
                253,
                254,
                255,
                257,
            };
            */

            UnicornCarListing = new []
                {
                    "2002 BMW M3-GTR",
                    "1969 Chevy Camaro SS",
                    "2003 Ferrari 360 CS",
                    "2007 Ferrari 430 S",
                    "2007 Shelby GT500",
                    "2004 Mugen Civic",
                    "2002 Mugen Integra",
                    "2005 Honda NSX-R GT",
                    "2007 Murcielago LP640",
                    "2007 Gallardo S",
                    "2007 RX-7 Spirit R",
                    "2006 HKS Lancer Evo",
                    "2000 Top Secret S15",
                    "1993 MINE'S GT-R R32",
                    "2002 MINE'S GT-R R34",
                    "2006 Subaru S204",
                    "1998 Top Secret Supra",
                    "NULL Car"
                };

            string[] ServerThumbUrls = new []
                {
                    "2002BMW_M3.xdc",
                    "1969Chevy_Camaro.xdc",
                    "2003Ferrari_360.xdc",
                    "2007Ferrari_430.xdc",
                    "2007Shelby_GT500.xdc",
                    "2004Mugen_Civic.xdc",
                    "2002Mugen_Integra.xdc",
                    "2005Honda_NSXRGT.xdc",
                    "2007Mercielag_LP640.xdc",
                    "2007Gallardo_S.xdc",
                    "2007RX7_SpiritR.xdc",
                    "2006HKS_LancerEvo.xdc",
                    "2000_TopSecret_S15.xdc",
                    "1993MINES_GTRR32.xdc",
                    "2002MINES_GTRR34.xdc",
                    "2006Subaru_S204.xdc",
                    "1998TopSecret_Supra.xdc",
                    "NULL_Car.xdc"
                };

            UnicornCarId = new int[]
                {
                    565,
                    289,
                    297,
                    1022,
                    617,
                    400,
                    401,
                    569,
                    1017,
                    1004,
                    425,
                    361,
                    473,
                    394,
                    395,
                    586,
                    475,
                    1215
                };

            CarThumbUrls = new Dictionary<int, string>();
            for (int x = 0; x < ServerThumbUrls.Length; x++)
                CarThumbUrls.Add(UnicornCarId[x], ServerThumbUrls[x]);

            CarData = new Dictionary<int, string>();
            CarNameData = new Dictionary<string, int>();

            for (int x = 0; x < UnicornCarListing.Length; x++)
                CarData.Add(UnicornCarId[x], UnicornCarListing[x]);

            for (int x = 0; x < UnicornCarListing.Length; x++)
                CarNameData.Add(UnicornCarListing[x], UnicornCarId[x]);

            this.Column6.Items.Clear();
            this.Column6.Items.AddRange(UnicornCarListing);

            this.GarageSchema = Encoding.ASCII.GetString(new WebClient().DownloadData(
                string.Format("{0}/forza4/{1}", Horizon.Server.Config.baseURL, "Garage_Schema.txt")));

            CarBinKey = SettingAsByteArray(205);
            CarList = new List<Forza4Garage.Forza4GarageCar>();

            if(_carBins == null)
                _carBins = new Dictionary<int, byte[]>();
            if(_carThumbs == null)
                _carThumbs = new Dictionary<int, byte[]>();
        }

        private bool LoadGarageData()
        {
            this.InitGarageData();
            var cars = CarGarage.GetGarageCars();
            this.CarList = new List<Forza4Garage.Forza4GarageCar>();
            if (cars != null)
            {
                foreach(Forza4Garage.Forza4GarageCar car in cars)
                {
                    if (UnicornCarId.Contains(car.CarId))
                    {
                        this.dgvCarGarage.Rows.Add(new object[] {
                        CarData[car.CarId],
                        car.OriginalOwner,
                        car.NumberOfOwners
                        });
                        this.CarList.Add(car);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveGarageData()
        {
            // Thumbnail Server URL: Horizon.Server.Config.baseURL + "forza4/unicorns/" + fileName
            dgvCarGarage.EndEdit();
            foreach (Forza4Garage.Forza4GarageCar garageCar in CarList)
            {
                if (garageCar.IsModified)
                {
                    if(!_carBins.ContainsKey(garageCar.CarId))
                    {
                        _carBins.Add(garageCar.CarId, new WebClient().DownloadData(string.Format("{0}/forza4/carbins/Car_{1}.bin", 
                                                             Server.Config.baseURL, garageCar.CarId)));
                    }
                    garageCar.CarReader = new Forza4CarbinReader(GarageSchema, new EndianReader(_carBins[garageCar.CarId], EndianType.BigEndian), CarBinKey);
                }
            }

            if(CarList.Count > 0x00)
                FinishGarageEdit(CarList);
        }

        private void AddAllUnicornCars()
        {
            int carListCount = CarList.Count;
            foreach (int Car in UnicornCarId)
            {
                if (!CarGarage.Exists(-1, Car))
                {
                    var car = new Forza4Garage.Forza4GarageCar
                                  {
                                      OriginalOwner = Package.Header.Metadata.DisplayName,
                                      CarId = Car,
                                      NumberOfOwners = 0x01,
                                      IsModified = true
                                  };
                    CarList.Add(car);

                    dgvCarGarage.Rows.Add(new object[] {
                    CarData[Car],
                    car.OriginalOwner,
                    car.NumberOfOwners
                    });
                }
            }
            if (carListCount == CarList.Count)
            {
                Functions.UI.messageBox("All Unicorn cars are already in the garage.");
            }
        }

        private void FinishGarageEdit(List<Forza4Garage.Forza4GarageCar> CarList)
        {
            List<string> RemovablePaths;
            var NewCars = CarGarage.SetGarageList(CarList, out RemovablePaths);
            foreach (string ThumbnailPath in RemovablePaths)
            {
                this.Package.StfsContentPackage.DeleteFile(ThumbnailPath);
            }
            foreach (Forza4Garage.Forza4Car car in NewCars)
            {
                if(!_carThumbs.ContainsKey(car.CarId))
                {
                    _carThumbs.Add(car.CarId, new WebClient().DownloadData(string.Format("{0}/forza4/unicorns/{1}", Horizon.Server.Config.baseURL, this.CarThumbUrls[car.CarId])));
                }
                this.Package.StfsContentPackage.CreateFileFromArray(car.ThumbnailPath, _carThumbs[car.CarId]);
            }
        }

        private void dgvCarGarage_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == this.CarList.Count)
            {
                this.CarList.Add(new Forza4Garage.Forza4GarageCar());
            }

            var CarRow = (sender as DevComponents.DotNetBar.Controls.DataGridViewX).Rows[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0x00:
                    if (CarRow.Cells[0].Value != null)
                        this.CarList[e.RowIndex].CarId = CarNameData[Convert.ToString(CarRow.Cells[0].Value)];                        
                    break;
                case 0x01:
                    if (CarRow.Cells[1].Value != null)
                        this.CarList[e.RowIndex].OriginalOwner = Convert.ToString(CarRow.Cells[1].Value);                        
                    break;
                case 0x02:
                    if (CarRow.Cells[2].Value != null)
                    this.CarList[e.RowIndex].NumberOfOwners = Convert.ToInt32(CarRow.Cells[2].Value);
                    break;
            }
            CarList[e.RowIndex].IsModified = true;            
        }
    }
}
