using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.XPath;
using Horizon.Functions;

namespace Horizon.Server
{
    internal static class Connection
    {
        internal static bool isOnline = false;

        // Check the IP address of the server to the actual one.
        internal static bool validIP()
        {
            return true;
        }

        // Change the AES keys. Sent from the server.
        internal static void doAESXML(XPathNavigator nav)
        {
            string hash = nav.Value;
            nav.MoveToFirstAttribute();
            string newKey = Security.safeDecryptToString(nav.Value);
            nav.MoveToParent();
            if (hash == (newKey.Reverse() + Config.clientSalt.Base64Encode()).Hash(HashType.SHA1))
                Config.clientAES = Encoding.ASCII.GetBytes(newKey);
        }
    }
}
