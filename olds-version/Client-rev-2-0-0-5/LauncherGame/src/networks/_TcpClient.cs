using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using log4net;
using MMO_NETWORK_LAUNCHER.src.networks.lib;
using MMO_NETWORK_LAUNCHER.src.networks.packet.send;
using MMO_NETWORK_LAUNCHER.src.model;

namespace MMO_NETWORK_LAUNCHER.src.networks
{
    public class _TcpClient
    {
        protected TcpClient lclient;
        protected NetworkStream nstream;
        protected System.Timers.Timer ltimer;
        public bool IsConnected = false;
        private byte[] buffer;

        public _TcpClient()
        {
            connect();
        }

        public void connect()
        {
            IsConnected = false;
            try
            {
                lclient = new TcpClient("localhost", 11000);
                nstream = lclient.GetStream();
            }
            catch (SocketException)
            {}

            if (ltimer != null && ltimer.Enabled)
                ltimer.Enabled = false;

            IsConnected = true;


            //sendPacket(new ());

            new System.Threading.Thread(read).Start();
        }

        public void read()
        {
            try
            {
                buffer = new byte[2];
                nstream.BeginRead(buffer, 0, 2, new AsyncCallback(OnReceiveCallbackStatic), null);
            }
            catch (Exception e)
            {
                termination();
            }
        }

        private void OnReceiveCallbackStatic(IAsyncResult result)
        {
            int rs = 0;
            try
            {
                rs = nstream.EndRead(result);
                if (rs > 0)
                {
                    short length = BitConverter.ToInt16(buffer, 0);
                    buffer = new byte[length];
                    nstream.BeginRead(buffer, 0, length, new AsyncCallback(OnReceiveCallback), result.AsyncState);
                }
            }
            catch (Exception e)
            {
                termination();
            }
        }

        private void OnReceiveCallback(IAsyncResult result)
        {
            nstream.EndRead(result);

            byte[] buff = new byte[buffer.Length];
            buffer.CopyTo(buff, 0);

            PacketsLauncher.handlePacket(this, buff);

            new System.Threading.Thread(read).Start();
        }

        private void termination()
        {
            if (paused)
                return;

            connect();
        }

        public void sendPacket(SendAuthPacket pk)
        {
            pk.write();

            List<byte> blist = new List<byte>();
            byte[] db = pk.ToByteArray();

            short len = (short)db.Length;
            blist.AddRange(BitConverter.GetBytes(len));
            blist.AddRange(db);

            nstream.Write(blist.ToArray(), 0, blist.Count);
            nstream.Flush();
        }

        private bool paused = false;
        //public void loginOk(int code)
        //{
        //    PrivateData.AuthCode = code;
        //}

        public int PacketAuth(string login, string password)
        {
            sendPacket(new ServerAuth_SEND(login,password));
            return PrivateData.AuthCode;
        }
    }
}
