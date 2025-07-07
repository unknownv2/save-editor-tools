using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Horizon.Functions;
using System.Windows.Forms;
using System.Net;
using Horizon.Forms;
using System.Xml.XPath;
using System.IO;

namespace Horizon.Server
{
    internal class Request
    {
        private string opt = String.Empty;
        internal Request(string action)
        {
            initRequest(action, String.Empty);
        }

        internal Request(string action, string optionalParameters)
        {
            initRequest(action, optionalParameters);
        }

        private void initRequest(string a, string p)
        {
            addVersion();
            addParam("a", a);
            opt = p;
            addLoggedParams();
        }

        private void addVersion()
        {
            addParam("v", Config.clientVersion);
        }

        // Add parameters so the server can identify the user.
        private void addLoggedParams()
        {
            if (User.isLogged)
                addParam("s", (string)Config.getSetting("session_id"));
        }

        // Add a parameter to send to the server.
        private Dictionary<string, object> parameters = new Dictionary<string, object>();
        internal void addParam(string key, object value)
        {
            if (parameters.ContainsKey(key))
                parameters[key] = value;
            else
                parameters.Add(key, value);
        }

        // Delete a paramater from the request dictionary.
        internal void delParam(string key)
        {
            if (parameters.ContainsKey(key))
                parameters.Remove(key);
        }

        // Build and encode all the parameters in a format the server can read.
        private string buildParameters()
        {
            string output = String.Empty;
            foreach (KeyValuePair<string, object> param in parameters)
            {
                output += "." + param.Key + ":";
                if (param.Value.GetType() == typeof(byte[]))
                    output += Convert.ToBase64String((byte[])param.Value);
                else
                    output += (param.Value.ToString()).Base64Encode();
            }
            return output.Remove(0, 1);
        }

        private static bool checkNetwork()
        {
            if (Runtime.Includes.RuntimeDLL.Length == 0 || !Connection.validIP() || Security.isDebugging() || Security.isRunningBadProcess())
            {
                if (Program.doneLoading && User.isLogged)
                    Client.logOut();
                return false;
            }
            return true;
        }

        private WebClient createRequest(string referer)
        {
            var wc = new WebClient();
            var machine = Security.getMachineIdent();
            wc.Headers.Add("user-agent", machine);
            wc.Headers.Add("x-machine", (machine + "nc87762njsis9912hjjs").Hash(HashType.SHA1));
            wc.Headers.Add("referer", referer);
            wc.Headers.Add("content-type", dataLength.ToString(CultureInfo.InvariantCulture));
            return wc;
        }

        private byte[] rawData = null;
        internal void setRawData(byte[] data)
        {
            rawData = data;
            addParam("data_hash", (rawData.Hash(HashType.MD5).ToHexString() + Config.clientSalt).Hash(HashType.MD5));
        }

        internal delegate void DataRequestCallback(byte[] data);
        internal virtual void downloadDataAsyncRequest(DataRequestCallback callback)
        {
            downloadDataAsyncRequest(callback, null);
        }

        private int dataLength;
        internal virtual void downloadDataAsyncRequest(DataRequestCallback callback, ProgressBar progressBar)
        {
            if (!checkNetwork())
                return;
            string parameters = buildParameters();
            byte[] encParams = encryptParameters(parameters);
            WebClient wc = createRequest(getReferer(parameters));
            _requestProgress = progressBar;
            if (progressBar != null)
            {
                _requestProgress.Tag = false;
                _requestProgress.Style = ProgressBarStyle.Blocks;
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            }
            wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wc_DownloadDataCompleted);
            wc.DownloadDataAsync(new Uri(Config.baseURL + "?b=" + Convert.ToBase64String(encParams).Replace('+', '-').Replace('/', '_')), callback);
        }

        private void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            ((DataRequestCallback)e.UserState).Invoke(e.Result);
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if ((bool)_requestProgress.Tag)
                ((WebClient)sender).CancelAsync();
            else
            {
                if (e.TotalBytesToReceive != -1)
                    _requestProgress.Maximum = (int)e.TotalBytesToReceive;
                if (e.BytesReceived <= _requestProgress.Maximum)
                    _requestProgress.Value = (int)e.BytesReceived;
            }
        }

        internal virtual bool doRequestAsync()
        {
            return doRequestAsync(null);
        }

        private ProgressBar _requestProgress;
        internal virtual bool doRequestAsync(ProgressBar progressBar)
        {
            if (!checkNetwork())
                return false;
            string parameters = buildParameters();
            byte[] encParams = encryptParameters(parameters);
            WebClient wc = createRequest(getReferer(parameters));
            _requestProgress = progressBar;
            if (progressBar != null)
            {
                _requestProgress.Tag = false;
                _requestProgress.Style = ProgressBarStyle.Blocks;
                wc.UploadProgressChanged += new UploadProgressChangedEventHandler(wc_UploadProgressChanged);
            }
            wc.UploadDataCompleted += new UploadDataCompletedEventHandler(wc_UploadDataCompleted);
            wc.UploadDataAsync(new Uri(Config.baseURL + opt), encParams);
            return true;
        }

        private void wc_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if ((bool)_requestProgress.Tag)
                ((WebClient)sender).CancelAsync();
            else
            {
                _requestProgress.Maximum = (int)e.TotalBytesToSend;
                _requestProgress.Value = (int)e.BytesSent;
            }
        }

        private void wc_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Result != null && e.Result.Length != 0)
                parseRequestData(e.Result);
            ((WebClient)sender).Dispose();
            if (_requestProgress != null)
            {
                _requestProgress.Value = 0;
                _requestProgress.Style = ProgressBarStyle.Continuous;
            }
        }

        private byte[] encryptParameters(string parameters)
        {
            byte[] enc = Security.encryptAES(parameters, Config.clientAES, Config.clientIV);
            dataLength = enc.Length;
            if (rawData != null)
            {
                Array.Resize(ref enc, enc.Length + rawData.Length);
                Array.Copy(rawData, 0, enc, dataLength, rawData.Length);
            }
            return enc;
        }

        private string getReferer(string parameters)
        {
            return (Security.getMachineIdent() + parameters + Config.clientSalt).Hash(HashType.SHA1).Reverse()
                + (Runtime.Includes.RuntimeDLL.Length > 20 ? Runtime.Includes.RuntimeDLL
                : Runtime.Includes.RuntimeDLL.Hash(HashType.MD5));
        }

        internal virtual bool doRequest()
        {
            if (!checkNetwork())
                return false;
            string parameters = buildParameters();
            byte[] encParams = encryptParameters(parameters);
            WebClient wc = createRequest(getReferer(parameters));
            byte[] data;
            try
            {
                data = wc.UploadData(Config.baseURL + opt, encParams);
            }
            catch
            {
                Program.killLoadingLogo();
                if (!Program.doneLoading || !Main.mainForm.InvokeRequired)
                    UI.errorBox("Failed to connect to the server!");
                return false;
            }
            wc.Dispose();
            return parseRequestData(data);
        }

        private static bool parseRequestData(byte[] data)
        {
            if (data.Length <= 40)
                return false;
            #if INT2
            string int2String = Encoding.ASCII.GetString(data);
            #endif
            byte[] encryptedData = new byte[data.Length - 40];
            Array.Copy(data, 40, encryptedData, 0x00, encryptedData.Length);
            string hashAsString = Encoding.ASCII.GetString(data, 0, 40);
            bool enableCompression = data[0] == 0x20 && User.isLogged && User.isDiamond;
            string xml = Encoding.ASCII.GetString(enableCompression ? Ionic.Zlib.ZlibStream.UncompressBuffer(encryptedData)
                : Security.decryptAES(encryptedData, Config.clientAES, Config.clientIV)).Replace("\0", String.Empty);
            if (enableCompression && Config.enableCompression)
            {
                Config.enableCompression = false;
                if (hashAsString.Remove(0, 1) != (xml + Security.getMachineIdent().Reverse() + Config.clientSalt).Hash(HashType.SHA1).Substring(1))
                    return false;
            }
            else if (hashAsString != (xml + Security.getMachineIdent().Reverse() + Config.clientSalt).Hash(HashType.SHA1))
                return false;
            #if INT2
            if (int2String.Length > 0)
                int2String = String.Empty;
            #endif
            XPathNavigator nav = new XPathDocument(new MemoryStream(Encoding.ASCII.GetBytes(xml))).CreateNavigator();
            nav.MoveToRoot();
            nav.MoveToFirstChild();
            if (nav.HasChildren)
            {
                nav.MoveToFirstChild();
                do
                    switch (nav.Name)
                    {
                        case "setting":
                            Config.caseSetting(nav);
                            break;
                        case "logout":
                            if (User.isLogged)
                                Client.logOut();
                            break;
                        case "aes":
                            Connection.doAESXML(nav);
                            break;
                        case "init":
                            return Program.doXMLInit(nav);
                        case "meta":
                            GameAdder.updateMetaStorage(nav);
                            break;
                        case "titledata":
                            GameAdder.addTitleData(nav);
                            break;
                        case "login":
                            if (!Client.doLogin(nav))
                                return false;
                            break;
                        case "reg":
                            if (Main.mainForm.regForm != null && !Main.mainForm.regForm.isHidden)
                                Main.mainForm.regForm.regCallback(nav);
                            break;
                        case "form":
                            FormConfig.loadDiamondForm(nav);
                            break;
                        case "close":
                            FormHandle.restrictEditor(nav.Value);
                            break;
                        case "message":
                            if (!Program.doneLoading)
                                Program.killLoadingLogo();
                            UI.messageBox(nav.Value.Replace(@"\", String.Empty), "Notice", MessageBoxIcon.Information);
                            if (!Program.doneLoading)
                                return false;
                            break;
                        case "process":
                            System.Diagnostics.Process.Start(nav.Value);
                            break;
                        case "news":
                            Main.mainForm.setNewsURL(nav.Value);
                            break;
                        case "denied":
                            UI.messageBox("Access denied.", "Not Allowed", MessageBoxIcon.Error);
                            break;
                        case "update":
                            Client.doUpdate(nav);
                            break;
                        case "challenge":
                            string cData = nav.Value;
                            nav.MoveToFirstAttribute();
                            Security.executeChallenge(nav.Value.Split('|'), Global.base64Decode(cData));
                            nav.MoveToParent();
                            break;
                        case "return":
                            return nav.ValueAsInt == 1;
                        case "kill":
                            Application.Exit();
                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                            return false;
                        default:
                            return false;
                    }
                while (nav.MoveToNext());
                return true;
            }
            return false;
        }

        internal static void parseFormXML(string id, string xml)
        {
            XPathNavigator nav = new XPathDocument(new MemoryStream(Encoding.ASCII.GetBytes(xml))).CreateNavigator();
            nav.MoveToRoot();
            nav.MoveToFirstChild();
            if (nav.HasChildren)
            {
                nav.MoveToFirstChild();
                do
                    switch (nav.Name)
                    {
                        case "v":
                            FormSettings.parseSetting(id, ref nav);
                            break;
                    }
                while (nav.MoveToFollowing(XPathNodeType.Element));
            }
        }
    }
}
