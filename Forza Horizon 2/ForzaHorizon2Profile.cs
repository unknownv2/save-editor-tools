using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ForzaMotorsport;
using System.IO;
using Horizon.Server;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.Forza_Horizon_2
{
    public partial class ForzaHorizon2Profile : EditorControl
    {
        private ForzaHorizon.ForzaHorizonProfile _forzaProfile;
        private static int[] _playerLevels;
        private EndianIO _playerDatabaseIO;
        private ForzaGarage _garage;
        private ForzaSQLite _forzaDatabase = new ForzaSQLite(null, null);
        private int[] _secretCarIds;
        private List<int> _carIds;
        private string[] _secretCarListing;
        private List<ForzaGarage.ForzaGarageCar> _carList;
        private Dictionary<string, int> _carIdList;
        private Dictionary<int, string> _carNameList, _carThumbNameList;
        private static Dictionary<int, byte[]> _carBins, _carThumbs;
        private static byte[] _carbinKey;
        private string _garageSchema;
        private static byte[][] _forzaKeyStack;

        public ForzaHorizon2Profile()
        {
            InitializeComponent();
            TitleID = FormID.ForzaHorizon2Profile;

#if INT2
            buttonItem1.Visible = buttonItem2.Visible = ribbonTabItem2.Visible = true;
            
#endif
        }

        public override bool Entry()
        {
            if (!OpenStfsFile("ForzaProfile"))
                return false;

            if (_forzaKeyStack == null)
            {
                _forzaKeyStack = new[] { Encoding.ASCII.GetBytes("Due to conflicting NAT settings,"), Encoding.ASCII.GetBytes(" unable to connect to the other "),
                    Encoding.ASCII.GetBytes("player's console at this time.\0D") };
            }

            //_carbinKey = File.ReadAllBytes(@"G:\Projects\Forza\Forza Horizon 2\Saves\2\Traffic Cars\carbinKey.bin");

            _forzaProfile = new ForzaHorizon.ForzaHorizonProfile(IO, Package.Header.Metadata.Creator,
                Encoding.ASCII.GetBytes(" unable to conne"), _forzaKeyStack,
                new EndianReader(Package.StfsContentPackage.GetFileStream("VersionFlags"), EndianType.BigEndian).SeekNReadInt32(0x8));

            if (!LoadSaveData())
                return false;

            LoadTreeSettings();

            // garage editor only for Diamond Users
            if (User.isDiamond)
            {
                _playerDatabaseIO = Package.StfsContentPackage.GetEndianIO("PlayerDatabase");
                _garage = new ForzaGarage(_playerDatabaseIO,
                    new byte[] { 0x20, 0x61, 0x74, 0x20, 0x74, 0x68, 0x69, 0x73, 0x20, 0x74, 0x69, 0x6D, 0x65, 0x2E, 0x00, 0x44 },
                    Encoding.ASCII.GetBytes("ct to the other player's console"), Package.Header.Metadata.Creator,
                    _forzaDatabase, ForzaVersion.ForzaHorizon);

                InitializeGarageInfo();
                ParseGarageInformation();
            }
            else
            {
                ribbonTabItem1.Visible = false; // hide garage tab
            }

            return true;
        }

        public override void Save()
        {
            // flush player database edits
            // before we save the garage, make sure the user is Diamond
            if (User.isDiamond)
            {
                if(_garage != null)
                {
                    SaveGarageInformation();
                    _garage.Save(false);
                }
            }

            UpdateNode("Main;Credits", numCredits.Value.ToString());
            UpdateNode("Main;XP", numXP.Value.ToString());
            UpdateNode("Main;Level", GetLevelFromExp(numXP.Value).ToString());

            WriteTreeSettings();

            _forzaProfile.Save();
        }
        private void InitSaveData()
        {
            if (_playerLevels == null)
            {
                _playerLevels = new []
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
            if (cmbLevel.Items.Count != 0) return;

            for (int x = 0; x < 1000; x++)
            {
                cmbLevel.Items.Add(x.ToString());
            }
        }
        private bool LoadSaveData()
        {
            try
            {
                InitSaveData();

                _forzaProfile.SaveIO.In.SeekTo(_forzaProfile.Profile.GetEntryAddress("Credits"));
                numCredits.Value = _forzaProfile.SaveIO.In.ReadUInt32();
                _forzaProfile.SaveIO.In.SeekTo(_forzaProfile.Profile.GetEntryAddress("XP"));
                uint Experience = _forzaProfile.SaveIO.In.ReadUInt32();
                _forzaProfile.SaveIO.In.SeekTo(_forzaProfile.Profile.GetEntryAddress("Level"));
                int level = _forzaProfile.SaveIO.In.ReadInt32();
                cmbLevel.Text = level.ToString();
                numXP.Value = Experience;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static int GetLevelFromExp(decimal Exp)
        {
            for (int x = 0; x < 999; x++)
            {
                if (Exp < _playerLevels[x + 1])
                    return x;
            }
            return 999;
        }

        private void LoadTreeSettings()
        {
            advPropertyTree.Nodes.Clear();
            advPropertyTree.BeginUpdate();

            for (int i = 0; i < _forzaProfile.Profile.ProfileSchemaEntries.Count; )
            {
                string parent = _forzaProfile.Profile.ProfileSchemaEntries[i].Name;
                if (_forzaProfile.Profile.ProfileSchemaEntries[i].Type == ForzaTypes.PropertyBag)
                {
                    int childCount = _forzaProfile.Profile.ProfileSchemaEntries[i++].PropertyChildCount;
                    for (int x = 0; x < childCount; x++)
                    {
                        AddSetting(parent + "\\" + _forzaProfile.Profile.ProfileSchemaEntries[i].Name, FormatSettingValue(_forzaProfile.Profile.ProfileSchemaEntries[i++]), advPropertyTree);
                    }
                }
                else
                {
                    AddSetting("Other\\" + _forzaProfile.Profile.ProfileSchemaEntries[i].Name, FormatSettingValue(_forzaProfile.Profile.ProfileSchemaEntries[i++]), advPropertyTree);
                }
            }

            advPropertyTree.EndUpdate();
        }
        private void WriteTreeSettings()
        {
            foreach (Node PropertyBagNode in this.advPropertyTree.Nodes)
            {
                this.WriteProperty(PropertyBagNode);
            }
        }
        private void WriteProperty(Node propertyBagNode)
        {
            foreach (Node propertyNode in propertyBagNode.Nodes)
            {
                var property = _forzaProfile.Profile.GetEntry(propertyNode.Name.Split(';')[1]);
                _forzaProfile.SaveIO.SeekTo(property.Address);
                WritePropertyValue(_forzaProfile.SaveIO.Out, property.Type, propertyNode.Cells[1].Text);
            }
        }
        private void UpdateNode(string nodeName, string value)
        {
            if (this.advPropertyTree.Nodes.Count > 0)
            {
                var node = this.advPropertyTree.Nodes.Find(nodeName, true)[0];
                node.BeginEdit();
                node.Cells[1].Text = value.ToString();
                node.EndEdit(false);
            }
        }
        private static string FormatSettingValue(ForzaProfileEntry entry)
        {
            switch (entry.Type)
            {
                case ForzaTypes.CarId:
                case ForzaTypes.UInt32:
                    return BitConverter.ToUInt32(Horizon.Functions.Global.convertToBigEndian(entry.Value), 0).ToString();
                case ForzaTypes.Bool:
                    return BitConverter.ToBoolean(Horizon.Functions.Global.convertToBigEndian(entry.Value), 0).ToString();
                case ForzaTypes.Uint16:
                    return BitConverter.ToUInt16(Horizon.Functions.Global.convertToBigEndian(entry.Value), 0).ToString();
                case ForzaTypes.UInt8:
                    return entry.Value[0].ToString();
                case ForzaTypes.Float32:
                    return BitConverter.ToSingle(Horizon.Functions.Global.convertToBigEndian(entry.Value), 0).ToString();
                case ForzaTypes.UInt64:
                    return BitConverter.ToUInt64(Horizon.Functions.Global.convertToBigEndian(entry.Value), 0).ToString();
                default:
                    throw new ForzaException(string.Format("could not format property type {0:D}", entry.Type));
            }
        }
        private static void WritePropertyValue(EndianWriter writer, ForzaTypes type, string value)
        {
            switch (type)
            {
                case ForzaTypes.CarId:
                case ForzaTypes.UInt32:
                    writer.Write(uint.Parse(value));
                    break;
                case ForzaTypes.Bool:
                    writer.Write(bool.Parse(value));
                    break;
                case ForzaTypes.Uint16:
                    writer.Write(ushort.Parse(value));
                    break;
                case ForzaTypes.UInt8:
                    writer.Write(byte.Parse(value));
                    break;
                case ForzaTypes.Float32:
                    writer.Write(float.Parse(value));
                    break;
                case ForzaTypes.UInt64:
                    writer.Write(ulong.Parse(value));
                    break;
                default:
                    throw new ForzaException(string.Format("could not parse property type {0:D}", type));
            }
        }
        private static void AddSetting(string setting, string value, AdvTree tree)
        {
            string[] setChilds = setting.Split('\\');
            string curPath = String.Empty;
            Node parentNode = null;
            for (int x = 0; x < setChilds.Length; x++)
            {
                curPath += setChilds[x];
                Node findNode = tree.FindNodeByName(curPath);
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
                    parentNode = parentNode == null ? tree.Nodes[tree.Nodes.Add(newNode)] : parentNode.Nodes[parentNode.Nodes.Add(newNode)];
                }
                else
                    parentNode = findNode;
                curPath += tree.PathSeparator;
            }
        }
        // Sync the values from the 'Simple' Tab to the 'Advanced' Tab
        private void EditPropCrValidated(object sender, EventArgs e)
        {
            UpdateNode("Main;Credits", (sender as NumericUpDown).Value.ToString());
        }
        private void EditPropXpValidated(object sender, EventArgs e)
        {
            UpdateNode("Main;XP", (sender as NumericUpDown).Value.ToString());
        }
        private void EditPropLvlValidated(object sender, EventArgs e)
        {
            UpdateNode("Main;Level", (sender as DevComponents.DotNetBar.Controls.ComboBoxEx).SelectedIndex.ToString());
        }
        private void CmbChangePlayerLevel(object sender, EventArgs e)
        {
            numXP.Value = _playerLevels[cmbLevel.SelectedIndex];
        }

        private void InitializeGarageInfo()
        {
            if(_carbinKey == null)
            {
                _carbinKey = SettingAsByteArray(105);
                //if(_carbinKey == null)
                    //_carbinKey = File.ReadAllBytes(@"G:\Projects\Forza\Forza Horizon 2\Saves\2\Traffic Cars\carbinKey.bin");
            }

            _secretCarListing = new[]
                                    {
                                        "2012 Kenworth Box Truck",
                                        "2011 Ford Ka",
                                        "2011 Volkswagen Fox",
                                        "2011 Ford Transit",
                                        "2012 Bus",
                                        "2010 FIAT Punto Evo SPORT",
                                        "2008 Toyota Yaris S",
                                        "2009 Ford Fiesta Zetec S",
                                        "2009 Mercedes-Benz A200 Turbo Coupe",
                                        "2003 Volkswagen Bora VR6",
                                        "2009 Alfa Romeo Brera Italia Independent",
                                        "2004 Mercedes-Benz C32 AMG",
                                        "2005 Subaru Legacy B4 2.0 GT",
                                        "2004 Audi S4",                                
                                    };
            _secretCarIds = new[]
                                {
                                    50002, //box truck
                                    1358,
                                    1363,
                                    50001, // transit
                                    50003, // bus
                                    1257,
                                    1047,
                                    1082, //zetec
                                    1089,
                                    283,
                                    1127,
                                    286,
                                    379,
                                    428
 
                                };

            _carIds = new List<int>();
            _carIds.AddRange(_secretCarIds);

            _carNameList = new Dictionary<int, string>();
            _carIdList = new Dictionary<string, int>();
            _carThumbNameList = new Dictionary<int, string>();

            // Add the traffic cars to the main car lists
            for (var i = 0; i < _secretCarListing.Length; i++)
            {
                _carNameList.Add(_secretCarIds[i], _secretCarListing[i]);
                _carThumbNameList.Add(_secretCarIds[i], string.Format("Thumbnail_{0}.xdc", _secretCarIds[i]));
                _carIdList.Add(_secretCarListing[i], _secretCarIds[i]);
            }

            // Add the traffic cars to the drop-box for user editing
            if (clmnCarList.Items.Count == 0x00)
            {
                clmnCarList.Items.AddRange(_secretCarListing);
            }

            
            _garageSchema = Encoding.ASCII.GetString(new WebClient().DownloadData(
                string.Format("{0}/forza_horizon_2/garage/{1}", Config.baseURL, "Garage_Schema.txt")));
            

            //_garageSchema = File.ReadAllText(@"G:\Projects\Forza\Forza Horizon 2\Saves\2\Traffic Cars\Career_Garage.txt");

            // for storing the temporarily downloaded car data
            if (_carBins == null)
                _carBins = new Dictionary<int, byte[]>();
            if (_carThumbs == null)
                _carThumbs = new Dictionary<int, byte[]>();
        }

        private void ParseGarageInformation()
        {
            treeGarage.Nodes.Clear();
            var carsInGarage = _garage.GetGarageCars();
            _carList = new List<ForzaGarage.ForzaGarageCar>();
            foreach (var forzaGarageCar in carsInGarage)
            {
                if (!_carIds.Contains(forzaGarageCar.CarId))
                    continue;

                // load garage v2
                //treeGarage.Nodes.Add(new Node(_carNameList[forzaGarageCar.CarId]) {Tag = forzaGarageCar});

                // load garage v1
                dgvCarGarage.Rows.Add(new object[]
                                          {
                                              _carNameList[forzaGarageCar.CarId],
                                              forzaGarageCar.OriginalOwner,
                                              forzaGarageCar.NumberOfOwners
                                          });
                _carList.Add(forzaGarageCar);
            }
            

        }

        private void SaveGarageInformation()
        {
            // no need to edit anything
            if (_carList.Count == 0x00)
                return;

            // make sure any rows being edited are saved
            dgvCarGarage.EndEdit();
            foreach (var garageCar in _carList)
            {
                if (!garageCar.IsModified) continue;
                if (!_carBins.ContainsKey(garageCar.CarId))
                {
                    
                    _carBins.Add(garageCar.CarId,
                                 new WebClient().DownloadData(string.Format("{0}/forza_horizon_2/garage/Car_{1}.bin",
                                                                            Config.baseURL,
                                                                            garageCar.CarId)));
                    
                    //_carBins.Add(garageCar.CarId, File.ReadAllBytes(string.Format(@"G:\Projects\Forza\Forza Horizon 2\Saves\Garage\Car_{0}.bin", garageCar.CarId)));
                }
                garageCar.CarReader = new ForzaCarbinReader(_garageSchema, new EndianReader(_carBins[garageCar.CarId], EndianType.BigEndian), _carbinKey);
            }
            FinishGarageEdit();
        }

        private void FinishGarageEdit()
        {
            List<string> removableThumbPaths;
            var newCars = _garage.SetGarageList(_carList, out removableThumbPaths);
            foreach (var thumbnailPath in removableThumbPaths)
            {
                Package.StfsContentPackage.DeleteFile(thumbnailPath);
            }
            foreach (var car in newCars)
            {
                if (!_carThumbs.ContainsKey(car.CarId))
                {
                    
                    _carThumbs.Add(car.CarId,
                        new WebClient().DownloadData(string.Format("{0}/forza_horizon_2/garage/Thumbnails/{1}", Config.baseURL, _carThumbNameList[car.CarId])));
                    
                    //_carThumbs.Add(car.CarId, File.ReadAllBytes(string.Format(@"G:\Projects\Forza\Forza Horizon 2\Saves\Garage\Thumbnails\Thumbnail_{0}.xdc", car.CarId)));
                }
                Package.StfsContentPackage.CreateFileFromArray(car.ThumbnailPath, _carThumbs[car.CarId]);
            }
        }

        private void AddCarsListToGarage(IEnumerable<int> carIdList)
        {
            foreach (var carId in carIdList)
            {
                if(_carList.FindIndex(c => c.CarId == carId) != -1)
                    continue;

                var newCar = new ForzaGarage.ForzaGarageCar
                {
                    CarId = carId,
                    OriginalOwner = Package.Header.Metadata.DisplayName,
                    NumberOfOwners = 1,
                    CarReader = null,
                    Key = 0,
                    ThumbnailPath = null,
                    IsModified = true
                };
                dgvCarGarage.Rows.Add(new object[]
                                          {
                                              _carNameList[carId],
                                              newCar.OriginalOwner,
                                              newCar.NumberOfOwners
                                          });
                _carList.Add(newCar);
            }
        }
        private void BtnClickAddAllTrafficCarsToGarage(object sender, EventArgs e)
        {
            AddCarsListToGarage(_secretCarIds);
        }

        private void DgvEndCarPropertyEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == _carList.Count)
                _carList.Add(new ForzaGarage.ForzaGarageCar());

            var carRow = dgvCarGarage.Rows[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0x00:
                    if (carRow.Cells[0].Value != null)
                        _carList[e.RowIndex].CarId = _carIdList[Convert.ToString(carRow.Cells[0].Value)];
                    break;
                case 0x01:
                    if (carRow.Cells[1].Value != null)
                        _carList[e.RowIndex].OriginalOwner = Convert.ToString(carRow.Cells[1].Value);
                    break;
                case 0x02:
                    if (carRow.Cells[2].Value != null)
                        _carList[e.RowIndex].NumberOfOwners = Convert.ToInt32(carRow.Cells[2].Value);
                    break;
            }
            _carList[e.RowIndex].IsModified = true;
        }

        //[Conditional("INT2")]
        private void BtnClickDumpCarbins(object sender, EventArgs e)
        {
            var writer = new ForzaCarbinWriter(_forzaDatabase.RetrieveTableData("Career_Garage"), _carbinKey);
            writer.ExportToDirectory(@"G:\Projects\Forza\Forza Horizon 2\Saves\Garage");
            return;
            var reader = _forzaDatabase.RetrieveTableData("Career_Garage").CreateDataReader();
            while(reader.Read())
            {
                string filePath = Convert.ToString(reader["Thumbnail"]).Remove(0, 18);
                Package.StfsContentPackage.ExtractFileToArray(filePath).Save(string.Format(@"G:\Projects\Forza\Forza Horizon 2\Saves\Garage\Thumbnails\Thumbnail_{0}.xdc",
                    Convert.ToInt32(reader["CarId"])));
            }
        }

        private void BtnClickExtractPlayerDatabase(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog {FileName = "PlayerDatabase"};
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            _garage.Extract().Save(sfd.FileName);
        }
    }
}
