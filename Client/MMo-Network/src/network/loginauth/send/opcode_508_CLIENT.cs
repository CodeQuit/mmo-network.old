using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMo_Network.network;

namespace MMo_Network.network.loginauth.send
{
    class opcode_508_CLIENT : GameServerNetworkPacket
    {
        private int _id;
        private string _name;
        public opcode_508_CLIENT(int id, string name)
        {
            _id = id;
            _name = name;
        }

        protected internal override void write()
        {
            writeH(508);
            writeC(_id);
            writeS(_name, _name.Length);
        }
    }
}
