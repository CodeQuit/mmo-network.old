using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMO_NETWORK_LAUNCHER.src.model
{
    public class PrivateData
    {
        public static string login = null;
        public static int AuthCode = 0;
        public static List<string> game_list = new List<string>();
        public static bool maintenanse = false;
    }
}
