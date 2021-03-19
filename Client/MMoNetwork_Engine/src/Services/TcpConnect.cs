using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace network
{
    public class MMoNetwork_TcpConnect
    {
        Socket sender;
        byte[] data2 = new byte[1024];
        public MMoNetwork_TcpConnect()
        {
            IPHostEntry ipHost = Dns.Resolve("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 39190);
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (!sender.Connected)
                    sender.Connect(ipEndPoint);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public byte[] reader(int i)
        {
            if (sender.Connected)
            {
                int data = sender.Receive(data2);
                return data2;
            }
            else
                return null;
        }


        public void send(byte[] sendB)
        {
            if (sender.Connected)
            {
                sender.Send(sendB);
            }
        }

        public void ConnectClose()
        {
            if (sender.Connected)
                sender.Close();
        }

        //private string[] TCPSender(string server, int port, int protocol_revision, int opcode, string data)
        //{
        //    int port_server_autorize = 80;
        //    string ServerAutorize = server;
        //    string protocol_revision_text = Convert.ToString(protocol_revision);
        //    string opcode_text = Convert.ToString(opcode);

        //    //Соединяемся с удаленным устройством
        //    try
        //    {
        //        //Устанавливаем удаленную конечную точку для сокета
        //        IPHostEntry ipHost = Dns.Resolve(ServerAutorize);
        //        IPAddress ipAddr = ipHost.AddressList[0];
        //        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port_server_autorize);
        //        Socket sender = new Socket(AddressFamily.InterNetwork,
        //            SocketType.Stream, ProtocolType.Tcp);
        //        //Соединяем сокет с удаленной конечной точкой
        //        sender.Connect(ipEndPoint);

        //        byte[] msg = Encoding.ASCII.GetBytes(protocol_revision_text + " " + opcode_text + " " + data);

        //        //отправляем данные через сокет
        //        int bytesSent = sender.Send(msg);

        //        //Получаем ответ от удаленного устройства
        //        int bytesRec = sender.Receive(bytes);
        //        string datapacket = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        //        packet = datapacket.Split(new Char[] { ' ' });
        //        Console.WriteLine(">>>" + datapacket);

        //        //Освобождаем сокет

        //    }
        //    catch (Exception e)
        //    {

        //    }

        //}
    }
}