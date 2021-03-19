using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace network
{
    public class LGNetwork_TcpConnect
    {
       /// <summary>
       /// Send and Recive data
       /// </summary>
       /// <param name="server">server name</param>
       /// <param name="port">port</param>
       /// <param name="protocol_revision">protocol revision</param>
       /// <param name="opcode">opcode</param>
       /// <param name="data">data</param>
       /// <returns></returns>
       public string[] TCP_Line(string server, int port, int protocol_revision, int opcode, string data)
       {
           return TCPSender(server, port, protocol_revision, opcode, data);
       }

       /// <summary>
       /// Send opcod, null and Recive
       /// </summary>
       /// <param name="server">server name</param>
       /// <param name="port">port</param>
       /// <param name="protocol_revision">protocol revision</param>
       /// <param name="opcode">opcode</param>
       /// <param name="data">data</param>
       /// <returns></returns>
       public string[] TCP_Line(string server, int port, int protocol_revision, int opcode)
       {
           int _null = 0;
           return TCPSender(server, port, protocol_revision, opcode, _null.ToString());
       }

        /// <summary>
        /// Send and Recive data
        /// </summary>
        /// <param name="opcode">Send opcode</param>
        /// <param name="data">Send data</param>
        /// <returns>Recive data</returns>
        private string[] TCPSender(string server, int port, int protocol_revision, int opcode, string data)
        {
            int port_server_autorize = 80;
            string ServerAutorize = server;

            byte[] bytes = new byte[1024];
            string[] packet = new string[255];

            string protocol_revision_text = Convert.ToString(protocol_revision);
            string opcode_text = Convert.ToString(opcode);

            //Соединяемся с удаленным устройством
            try
            {
                //Устанавливаем удаленную конечную точку для сокета
                IPHostEntry ipHost = Dns.Resolve(ServerAutorize);
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port_server_autorize);
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                //Соединяем сокет с удаленной конечной точкой
                sender.Connect(ipEndPoint);

                byte[] msg = Encoding.ASCII.GetBytes(protocol_revision_text + " " + opcode_text + " " + data);

                //отправляем данные через сокет
                int bytesSent = sender.Send(msg);

                //Получаем ответ от удаленного устройства
                int bytesRec = sender.Receive(bytes);
                string datapacket = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                packet = datapacket.Split(new Char[] { ' ' });
                Console.WriteLine(">>>" + datapacket);

                //Освобождаем сокет
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (Exception e)
            {

            }

            return packet;
        }
    }
}