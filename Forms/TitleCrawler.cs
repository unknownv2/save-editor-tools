using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using Horizon.Functions;
using XboxDataBaseFile;
using XContent;
using Horizon.Properties;
using System.IO;

namespace Horizon.Forms
{
    public partial class TitleCrawler : Office2007RibbonForm
    {
        public TitleCrawler()
        {
            MdiParent = Main.mainForm;
            InitializeComponent();
        }

        private Dictionary<string, List<string>> tidList;
        private List<string> mainList;
        private void initializeDictionary()
        {
            mainList = new List<string>();
            tidList = new Dictionary<string, List<string>>();
        }

        //private static string noGamesFound = "NoGamesFound";
        private string[] marketplaceURLs = new string[] {
            "http://marketplace.xbox.com/{0}/Games/Xbox360Games?PageSize=90&Page={1}",
            "http://marketplace.xbox.com/{0}/Games/XboxArcadeGames?PageSize=90&Page={1}"
        };

        private static string catalogUrl = "http://catalog-cdn.xboxlive.com/Catalog/Catalog.asmx/Query?methodName=FindGames&Names=Locale&Values=en-US&Names=LegalLocale&Values=en-US&Names=Store&Values=1&Names=PageSize&Values=100&Names=PageNum&Values={0}&Names=AvatarBodyTypes&Values=3&Names=AvatarBodyTypes&Values=1&Names=DetailView&Values=2&Names=OfferFilterLevel&Values=1&Names=UserTypes&Values=1&Names=MediaTypes&Values=1&Names=MediaTypes&Values=5&Names=MediaTypes&Values=18&Names=MediaTypes&Values=19&Names=MediaTypes&Values=20&Names=MediaTypes&Values=21&Names=MediaTypes&Values=22&Names=MediaTypes&Values=23&Names=MediaTypes&Values=30&Names=MediaTypes&Values=34&Names=MediaTypes&Values=37&Names=MediaTypes&Values=47&Names=OfferTargetMediaTypes&Values=22&Names=ImageFormats&Values=4&Names=ImageFormats&Values=5&Names=ImageSizes&Values=15&Names=ImageSizes&Values=23&Names=OrderBy&Values=1&Names=OrderDirection&Values=1";

        private void cmdCrawl_Click(object sender, EventArgs e)
        {
            cmdCrawl.Enabled = false;
            cmdImport.Enabled = false;
            initializeDictionary();

            WebClient wb = new WebClient();
            int numPages = (int)Math.Ceiling(decimal.Parse(wb.DownloadString(String.Format(catalogUrl, 50000)).Split("<live:totalItems>")[1].Substring(0, 5)) / 100) + 1;

            pbCrawl.Maximum = numPages;
            pbCrawl.Value = 0;

            for (int x = 1; x < numPages; x++)
            {
                string[] dat = wb.DownloadString(String.Format(catalogUrl, x)).Split("<live:titleId>");
                for (int i = 1; i < dat.Length; i++)
                {
                    string tit = int.Parse(dat[i].Substring(0, dat[i].IndexOf('<'))).ToString("X8");
                    if (!mainList.Contains(tit))
                        mainList.Add(tit);
                }
                pbCrawl.Value++;
                Application.DoEvents();
            }


            /*foreach (string reg in Server.GameAdder.titleRegions)
            {
                for (byte x = 0; x < marketplaceURLs.Length; x++)
                {
                    short curPage = 1;
                    while (true)
                    {
                        string htmlData = new WebClient().DownloadString(String.Format(marketplaceURLs[x], reg, curPage));
                        if (htmlData.Contains(noGamesFound))
                            break;
                        string[] splitData = htmlData.Split(new string[] {
                            "ProductBox\" href=\"/" + reg + "/Product/" },
                            StringSplitOptions.None);
                        for (int z = 1; z < splitData.Length; z++)
                        {
                            string[] subData = splitData[z].Split('"');
                            string titleID = subData[0].Substring(subData[0].Length - 8, 8).ToUpper();
                            tidList[reg].Add(titleID);
                            if (!mainList.Contains(titleID))
                                mainList.Add(titleID);
                        }
                        curPage++;
                    }
                    pbCrawl.Value++;
                    Application.DoEvents();
                }
            }*/



            UI.messageBox("Done crawling! " + mainList.Count.ToString() + " Title IDs found.\nClick OK to continue.", "Done Crawling", MessageBoxIcon.Information);
            cmdExtractData.Enabled = true;
            tabStep2.Visible = true;
            tabStep3.Visible = true;
            tabStep2.Select();
            cmdCrawl.Enabled = true;
            cmdImport.Enabled = true;
            rcCrawler.Refresh();
        }

        private XContentPackage Package;
        private ProfileFile Profile;
        private PEC Pec;
        private List<TitlePlayedRecord> TitlePlayedRecords;
        private void cmdLoadProfile_Click(object sender, EventArgs e)
        {
            if (loadNewProfile(true))
            {
                foreach (string tid in mainList)
                    if (!isBanned(tid) && !isInProfile(tid))
                    {
                        if (tid.Length != 8)
                            continue;

                        if (tid.Substring(0, 4) == "5855")
                            continue;

                        var meta = new Server.GameAdder.TitleMetaInfo();
                        meta.FemaleAwards = 0;
                        meta.MaleAwards = 0;
                        meta.TID = tid;
                        meta.TitleName = "HMB";
                        meta.TotalAchievements = 10;
                        meta.TotalCredit = 100;
                        titleAdder.AddTitleToListCache(meta);
                    }
                titleAdder.FlushTitleList();
                if (this.Pec != null)
                {
                    this.Pec.Flush();
                    this.Pec.Save();
                }
                Package.Flush();
                Package.Save(true);
                Package.CloseIO(true);
                UI.messageBox("Your profile now has every game on the marketplace. Make sure there are hundreds of titles named \"HMB\" on your games list.\nSign into LIVE and sit on the dashboard for 5 minutes, then recover the account and proceed to step 3!", "Titles Added", MessageBoxIcon.Information);
                tabStep3.Visible = true;
                tabStep3.Select();
                rcCrawler.Refresh();
            }
        }

        private static string[] bannedTIDs = new string[] {
            "FFFE07DF", "FFFE07D1", "584B07E5", "584B07E6",
            "584B0839", "FFFE07FA", "FFFE07D2", "FFFE07DE",
            "FFFE07FA", "FFFE07FB", "584807E1", "584807E3",
            "584B083A"
        };
        private bool isBanned(string tid)
        {
            return bannedTIDs.Contains(tid);
        }

        private TitleAdder tadder;
        private bool loadNewProfile(bool adder)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Package = new XContentPackage();
                if (!Package.LoadPackage(ofd.FileName))
                {
                    UI.messageBox("Invalid package!", "Invalid", MessageBoxIcon.Error);
                    return false;
                }
                if (Package.Header.Metadata.ContentType != ContentTypes.Profile)
                {
                    UI.messageBox("This package is not an Xbox 360 profile!", "Not a Profile", MessageBoxIcon.Error);
                    return false;
                }
                for (byte x = 0; x < 5; x++)
                    if (Package.Header.Signature.ConsoleSignature.Cert.ConsoleId[x] != Package.Header.Metadata.ConsoleId[x])
                    {
                        UI.messageBox("This package has been tampered with!\nPlease load a freshly recovered account.", "Not Fresh", MessageBoxIcon.Error);
                        return false;
                    }
                Profile = new ProfileFile(Package, 0xfffe07d1);
                Profile.Read();
                TitlePlayedRecords = new List<TitlePlayedRecord>();
                if (adder)
                {
                    titleAdder = new TitlePlayedAdder(Profile);
                    if ((this.Package.StfsContentPackage.GetDirectoryEntryIndex("PEC") != -1))
                    {
                        Pec = new XContent.PEC(this.Package.StfsContentPackage.GetEndianIO("PEC"));
                    }
                    tadder = new TitleAdder(this.Package.StfsContentPackage, Pec);
                }
                List<DataFileRecord> TitleRecords = Profile.DataFile.FindDataEntries(Namespace.TITLES);
                for (int x = 0; x < TitleRecords.Count; x++)
                    if (TitleRecords[x].Id.Id != 0)
                        TitlePlayedRecords.Add(new TitlePlayedRecord(Profile.DataFile.SeekToRecord(TitleRecords[x].Entry)));
                return true;
            }
            return false;
        }

        private TitlePlayedAdder titleAdder;
        private bool isInProfile(string TID)
        {
            uint uTID = uint.Parse(TID, System.Globalization.NumberStyles.HexNumber);
            foreach (TitlePlayedRecord title in TitlePlayedRecords)
                if (title.TitleId == uTID)
                    return true;
            return false;
        }

        private void cmdExtractData_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select the default tile";
            ofd.Filter = "PNG|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string defaultTile = Convert.ToBase64String(File.ReadAllBytes(ofd.FileName));
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK && loadNewProfile(false))
                {
                    PEC PEC = new PEC(Package.StfsContentPackage.GetEndianIO("PEC"));
                    string meta = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?>\n<meta>\n";
                    for (int x = 0; x < TitlePlayedRecords.Count; x++)
                    {
                        if (TitlePlayedRecords[x].TitleId != 0 && !isBanned(TitlePlayedRecords[x].TitleId.ToString("X")))
                        {
                            //string regs = String.Empty;
                            //for (int z = 0; z < Server.GameAdder.titleRegions.Length; z++)
                            //    if (tidList[Server.GameAdder.titleRegions[z]].Contains(TitlePlayedRecords[x].TitleId.ToString("X")))
                            //        regs += z.ToString().PadLeft(2, '0');
                            meta += "\t<title t=\"" + TitlePlayedRecords[x].TitleId.ToString("X")
                                + "\" r=\"0\" h=\""
                            + TitlePlayedRecords[x].AchievementsPossible.ToString() + "\" g=\""
                            + TitlePlayedRecords[x].CredPossible.ToString() + "\" a=\""
                            + TitlePlayedRecords[x].AllAvatarAwards.Possible.ToString() + "\" m=\""
                            + TitlePlayedRecords[x].MaleAvatarAwards.Possible.ToString() + "\" f=\""
                            + TitlePlayedRecords[x].FemaleAvatarAwards.Possible.ToString() + "\">"
                            + TitlePlayedRecords[x].TitleName.ToHexString(4) + "</title>\n";

                            DataFile dataFile = new DataFile(Package.StfsContentPackage.GetEndianIO(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[x].TitleId)));
                            dataFile.Read();
                            AchievementTracker achTracker = new AchievementTracker(Profile, dataFile, TitlePlayedRecords[x]);
                            achTracker.Read();
                            byte[] titleThumb = dataFile.ReadRecord(new DataFileId() { Namespace = Namespace.IMAGES, Id = 0x0000000000008000 });
                            string titleData = "<titledata a=\"" + TitlePlayedRecords[x].TitleId.ToString("X") + "\" b=\""
                                + ((titleThumb == null) ? defaultTile : Convert.ToBase64String(titleThumb)) + "\">\n";
                            for (int z = 0; z < achTracker.Achievements.Count; z++)
                            {
                                titleData += "\t<ach a=\"" + achTracker.Achievements[z].cred.ToString() + "\" b=\""
                                    + achTracker.Achievements[z].id.ToString() + "\" c=\"" + achTracker.Achievements[z].imageId.ToString()
                                    + "\" d=\"" + achTracker.Achievements[z].flags.ToString() + "\">\n";
                                titleData += "\t\t<x>" + achTracker.Achievements[z].Label.ToHexString(4) + "</x>\n";
                                titleData += "\t\t<y>" + achTracker.Achievements[z].Unachieved.ToHexString(4) + "</y>\n";
                                titleData += "\t\t<z>" + achTracker.Achievements[z].Description.ToHexString(4) + "</z>\n";
                                titleData += "\t</ach>\n";
                            }
                            if (TitlePlayedRecords[x].AllAvatarAwards.Possible > 0)
                            {
                                DataFile dataFileAv = new DataFile(PEC.StfsPec.GetEndianIO(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[x].TitleId)));
                                dataFileAv.Read();
                                AvatarAssetTracker avTracker = new AvatarAssetTracker(Profile, dataFileAv, TitlePlayedRecords[x]);
                                for (int z = 0; z < avTracker.Awards.Count; z++)
                                {
                                    titleData += "\t<aw a=\"" + avTracker.Awards[z].id.ToHexString() + "\" b=\""
                                        + avTracker.Awards[z].Id.ToString() + "\" c=\"" + avTracker.Awards[z].imageId.ToString()
                                        + "\" d=\"" + avTracker.Awards[z].flags.ToString() + "\" e=\"" + avTracker.Awards[z].reserved.ToString()
                                        + "\" f=\"" + avTracker.Awards[z].cb.ToString() + "\">\n";
                                    titleData += "\t\t<x>" + avTracker.Awards[z].Name.ToHexString(4) + "</x>\n";
                                    titleData += "\t\t<y>" + avTracker.Awards[z].UnawardedText.ToHexString(4) + "</y>\n";
                                    titleData += "\t\t<z>" + avTracker.Awards[z].Description.ToHexString(4) + "</z>\n";
                                    titleData += "\t</aw>\n";
                                }
                            }
                            File.WriteAllText(fbd.SelectedPath + @"\" + TitlePlayedRecords[x].TitleId.ToString("X") + ".xml", titleData + "</titledata>");
                        }
                    }
                    meta += "</meta>";
                    File.WriteAllText(fbd.SelectedPath + @"\Meta.xml", meta);
                    UI.messageBox("You have successfully dumped every game! Send that folder to Cheater912 ASAP!", "Done", MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                initializeDictionary();
                cmdCrawl.Enabled = false;
                cmdExport.Enabled = false;
                cmdExtractData.Enabled = false;
                string[] newTitles = File.ReadAllLines(ofd.FileName);
                mainList.AddRange(newTitles);
                tabStep2.Visible = true;
                tabStep3.Visible = true;
                tabStep2.Select();
                cmdCrawl.Enabled = true;
                cmdExtractData.Enabled = true;
                rcCrawler.Refresh();
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
                File.WriteAllLines(sfd.FileName, mainList.ToArray());
        }
    }
}