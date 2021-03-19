using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using network;
using MMO_NETWORK_LAUNCHER.src.networks.lib;
using MMO_NETWORK_LAUNCHER.src.model;

namespace MMO_NETWORK_LAUNCHER.src.networks.packet.resv
{
   public class ServerAuth_RESV : ReceiveAuthPacket
	{
        //_TcpClient tp;
        public ServerAuth_RESV(_TcpClient login, byte[] db)
        {
            base.makeme(login, db);
        }

        int code;
        public override void read()
        {
            code = readD();
        }

        public override void run()
        {
            //tp.loginOk(code);
            PrivateData.AuthCode = code;
        }
    }
}
