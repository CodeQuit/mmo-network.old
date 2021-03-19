using System;
using System.Threading;
using MMo_Network.network.loginauth;
using MMo_Network.network.loginauth.recv;
using System.Windows.Forms;


namespace MMo_Network.network
{
    public class PacketHandlerAuth
    {
        public static void handlePacket(AuthThread login, byte[] buff)
        {
            int opcode = 0;

            try
            {
                opcode = BitConverter.ToInt16(buff, 0);
            }
            catch
            {
                //Console.WriteLine(e.ToString());
            }

            //if(opcode > 0)
                //Console.WriteLine("handlepacket: opcode: " + opcode + "; size " + buff.Length);

            ReceiveAuthPacket msg = null;
            switch (opcode)
            {
                case 500:
                    msg = new opcode_500_SERVER(login, buff);
                    break;

                case 503:
                    msg = new opcode_503_SERVER(login, buff);
                    break;

                case 505:
                    msg = new opcode_505_SERVER(login, buff);
                    break;

                case 507:
                    msg = new opcode_507_SERVER(login, buff);
                    break;

                case 509:
                    msg = new opcode_509_SERVER(login, buff);
                    break;

                default:
                    MessageBox.Show(opcode.ToString());
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
