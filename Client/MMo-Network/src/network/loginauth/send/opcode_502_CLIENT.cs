using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MMo_Network.network.loginauth.send
{
    class opcode_502_CLIENT : GameServerNetworkPacket
    {
        private string _login, _password;
        private int version;
        public opcode_502_CLIENT(string login, string password)
        {
            _login = login;
            _password = password;
          
        }

        protected internal override void write()
        {
            writeH(502);
            writeC(_login.Length);
            writeC(_password.Length);
            writeS(_login);
            writeS(_password);
            writeC(0);
          
        }
    }
}
