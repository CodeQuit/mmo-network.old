using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using network;
using System.Threading;
using MMO_NETWORK_LAUNCHER.src.networks.lib;
using MMO_NETWORK_LAUNCHER.src.networks.packet.resv;

namespace MMO_NETWORK_LAUNCHER.src.networks
{
	class PacketsLauncher
	{
        public static void handlePacket(_TcpClient login, byte[] buff)
        {
            int id = Convert.ToInt16(buff[0] + buff[1]);
            ReceiveAuthPacket msg = null;
            switch (id)
            {
                case 0x07f1:
                    msg = new ServerAuth_RESV(login, buff);
                    break;

                default:
                    break;
            }

            if (msg == null)
            {
                return;
            }

            //if (!login.IsConnected)
            //    return;

            new Thread(new ThreadStart(msg.run)).Start();
        }
	}
}
