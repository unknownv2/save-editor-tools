using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Horizon.Functions;

namespace Horizon.Server
{
    internal static class User
    {
        internal static bool wasAutoLoggedIn = false;
        internal static bool isLogged = false;
        internal static bool isDiamond = false;

        // Sends the server login information: SMF hash and a random number.
        internal static bool tryLogin(string username, string password)
        {
            if (password == null)
                return false;
            Config.addSetting("client_code", Global.random.Next().ToString(CultureInfo.InvariantCulture));
            Request req = new Request("login");
            req.addParam("user", username.Base64Encode());
            req.addParam("pass", password);
            req.addParam("code", (string)Config.getSetting("client_code"));
            return req.doRequest();
        }

        internal static void doCheckUp()
        {
            if (isLogged && isDiamond)
                new Request("checkup").doRequestAsync();
        }
    }
}
