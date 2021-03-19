using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMo_Network.network;

namespace MMo_Network.network.loginauth.send
{
    class opcode_506_CLIENT : GameServerNetworkPacket
    {
        int idNews = 0;
        public opcode_506_CLIENT(int id)
        {
            idNews = id;
        }

        protected internal override void write()
        {
            writeH(506);
            writeC(idNews); //айди игры.
            //writeD(0);
        }
    }
}
