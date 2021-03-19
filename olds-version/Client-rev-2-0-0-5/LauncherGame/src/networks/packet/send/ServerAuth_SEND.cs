using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMO_NETWORK_LAUNCHER.src.networks.lib;

namespace MMO_NETWORK_LAUNCHER.src.networks.packet.send
{
    class ServerAuth_SEND : SendAuthPacket
	{
        string _login;
        string _password;
        public ServerAuth_SEND(string login, string password)
        {
            _login = login;
            _password = password;
        }

        protected internal override void write()
        {
            writeH(2034);
            writeC(_login.Length);
            writeS(_login);
            writeC(_password.Length);
            writeS(_password);
        }
    }
}
