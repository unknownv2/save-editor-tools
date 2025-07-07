using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace Horizon.Library.Gamercard
{
    class Gamercard
    {
        public string AccountStatus, State, Gamertag, ProfileUrl,
            Country, Location, Bio, ReputationImageUrl, Zone, TileUrl;
        public double Reputation;
        public int GamerScore;
        public PresenceInfoStruct PresenceInfo = new PresenceInfoStruct();
        public struct PresenceInfoStruct
        {
            public bool Valid;
            public string Info;
            public string Info2;
            public DateTime LastSeen;
            public bool Online;
            public string StatusText;
            public string Title;
        }

        public List<XboxUserGameInfo> RecentGames = new List<XboxUserGameInfo>();
        public struct XboxUserGameInfo
        {
            public GameInfo Info;
            public DateTime LastPlayed;
            public int Achievements;
            public int GamerScore;
            public string DetailsURL;
        }

        public struct GameInfo
        {
            public string Name;
            public int TotalAchievements;
            public int TotalGamerScore;
            public string Image32Url;
            public string Image64Url;
        }

        private static string apiUrl = "http://xboxapi.duncanmackenzie.net/gamertag.ashx";
        public Gamercard(string gamertag)
        {
            XPathNavigator nav = new XPathDocument(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(
                apiUrl + "?GamerTag=" + gamertag.ToLower().Replace(' ', '+')))).CreateNavigator();
            nav.MoveToRoot();
            nav.MoveToFirstChild();
            if (nav.HasChildren)
            {
                nav.MoveToFirstChild();
                do
                {
                    switch (nav.Name)
                    {
                        case "AccountStatus": AccountStatus = nav.Value; break;
                        case "PresenceInfo":
                            if (nav.HasChildren)
                            {
                                nav.MoveToFirstChild();
                                do
                                {
                                    switch (nav.Name)
                                    {
                                        case "Valid": PresenceInfo.Valid = nav.ValueAsBoolean; break;
                                        case "Info": PresenceInfo.Info = nav.Value; break;
                                        case "Info2": PresenceInfo.Info2 = nav.Value; break;
                                        case "LastSeen": PresenceInfo.LastSeen = DateTime.Parse(nav.Value); break;
                                        case "Online": PresenceInfo.Online = nav.ValueAsBoolean; break;
                                        case "StatusText": PresenceInfo.StatusText = nav.Value; break;
                                        case "Title": PresenceInfo.Title = nav.Value; break;
                                    }
                                }
                                while (nav.MoveToNext());
                                nav.MoveToParent();
                            }
                            break;
                        case "State": State = nav.Value; break;
                        case "Gamertag": Gamertag = nav.Value; break;
                        case "ProfileUrl": ProfileUrl = nav.Value; break;
                        case "TileUrl": TileUrl = nav.Value; break;
                        case "Country": Country = nav.Value; break;
                        case "Reputation": Reputation = nav.ValueAsDouble; break;
                        case "Bio": Bio = nav.Value; break;
                        case "Location": Location = nav.Value; break;
                        case "ReputationImageUrl": ReputationImageUrl = nav.Value; break;
                        case "GamerScore": GamerScore = nav.ValueAsInt; break;
                        case "Zone": Zone = nav.Value; break;
                        case "RecentGames":
                            if (nav.HasChildren)
                            {
                                nav.MoveToFirstChild();
                                do
                                {
                                    if (nav.HasChildren)
                                    {
                                        XboxUserGameInfo Game = new XboxUserGameInfo();
                                        nav.MoveToFirstChild();
                                        do
                                        {
                                            switch (nav.Name)
                                            {
                                                case "Game":
                                                    if (nav.HasChildren)
                                                    {
                                                        Game.Info = new GameInfo();
                                                        nav.MoveToFirstChild();
                                                        do
                                                        {
                                                            switch (nav.Name)
                                                            {
                                                                case "Name": Game.Info.Name = nav.Value; break;
                                                                case "TotalAchievements": Game.Info.TotalAchievements = nav.ValueAsInt; break;
                                                                case "TotalGamerScore": Game.Info.TotalGamerScore = nav.ValueAsInt; break;
                                                                case "Image32Url": Game.Info.Image32Url = nav.Value; break;
                                                                case "Image64Url": Game.Info.Image64Url = nav.Value; break;
                                                            }
                                                        }
                                                        while (nav.MoveToNext());
                                                        nav.MoveToParent();
                                                    }
                                                    break;
                                                case "LastPlayed": Game.LastPlayed = DateTime.Parse(nav.Value); break;
                                                case "Achievements": Game.Achievements = nav.ValueAsInt; break;
                                                case "GamerScore": Game.GamerScore = nav.ValueAsInt; break;
                                                case "DetailsURL": Game.DetailsURL = nav.Value; break;
                                            }
                                        }
                                        while (nav.MoveToNext());
                                        RecentGames.Add(Game);
                                        nav.MoveToParent();
                                    }
                                }
                                while (nav.MoveToNext());
                                nav.MoveToParent();
                            }
                                break;
                    }
                }
                while (nav.MoveToNext());
            }
        }
    }
}
