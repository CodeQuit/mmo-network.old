using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMo_Network.network;

namespace MMo_Network.network.loginauth.send
{
    class opcode_504_CLIENT : GameServerNetworkPacket
    {
        public opcode_504_CLIENT()
        {
            
        }

        protected internal override void write()
        {
            writeH(504);
        }
    }
}
