using System;
using System.Collections.Generic;
using System.Net.Sockets;
//using PBProjectBot.logger;
using MMo_Network.network.loginauth.recv;
using MMo_Network.network.loginauth.send;
using System.Windows;
using System.Windows.Threading;
using System.Threading;

namespace MMo_Network.network.loginauth
{
    public class AuthThread
    {
        private static AuthThread at = new AuthThread();
        public static AuthThread getInstance()
        {
            return at;
        }
        
        protected TcpClient lclient;
        protected NetworkStream nstream;
        protected System.Timers.Timer ltimer;
        public bool IsConnected = false;
        private byte[] buffer;
        public int channel = 3;
        //
        private int sessionID = 0;
        private int CryptKey = 0;
        private int hashcode = 0;
        //
        public AuthThread()
        {
            
        }

        public void setHashCode(int hash)
        {
            hashcode = hash;
        }

        public int getHashCode()
        {
            return hashcode;
        }

        public void setCryptKey(int Crypt)
        {
            CryptKey = Crypt;
        }
     
	    public int getCryptKey()
	    {
            return CryptKey;
	    }

        public int getSessionId()
        {
            return sessionID;
        }

        public void setSessionId(int session)
        {
            sessionID = session;
        }

        public void connect()
        {
            IsConnected = false;
            try
            {
                lclient = new TcpClient("127.0.0.1", 11000);
                nstream = lclient.GetStream();
            }
            catch (SocketException)
            {
                //CLogger.warning("Сервер не отвечает. Повтор...");
                if (ltimer == null)
                {
                    ltimer = new System.Timers.Timer();
                    ltimer.Interval = 5000;
                    ltimer.Elapsed += new System.Timers.ElapsedEventHandler(ltimer_Elapsed);
                }

                if (!ltimer.Enabled)
                    ltimer.Enabled = true;

                return;
            }
            finally
            {
                //MessageBox.Show("Connected.");
                if (ltimer != null && ltimer.Enabled)
                    ltimer.Enabled = false;
                IsConnected = true;
            }



            

            new System.Threading.Thread(read).Start();
        }

        private void ltimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ltimer.Stop();
            MessageBox.Show("Соединение с сервером прервано.\r\nЗавершение программы.", "");
            FormManager.getForm().CloseApplication();
        }

        public void read()
        {
            try
            {
                buffer = new byte[2];
                nstream.BeginRead(buffer, 0, 2, new AsyncCallback(OnReceiveCallbackStatic), null);
            }
            catch
            {
                MessageBox.Show("Соединение с сервером прервано.\r\nЗавершение программы.", "");
                //CLogger.error("AuthThread: " + e.Message);
                //termination();
            }
        }

        private void OnReceiveCallbackStatic(IAsyncResult result)
        {
            int rs = 0;
            try
            {
                if (nstream.CanRead)
                {
                    rs = nstream.EndRead(result);
                    if (rs > 0)
                    {
                        byte[] array = { buffer[0], buffer[1] };
                        ushort val = BitConverter.ToUInt16(array, 0);
                        if (nstream.DataAvailable)
                        {
                            buffer = new byte[val + 2];
                            nstream.BeginRead(buffer, 0, val + 2, new AsyncCallback(OnReceiveCallback), result.AsyncState);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                //CLogger.error("AuthThread: " + e.Message);
                //termination();
                //MessageBox.Show("Связь с сервером прекращена..\r\nЗавершение программы.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnReceiveCallback(IAsyncResult result)
        {
            nstream.EndRead(result);

            byte[] buff = new byte[buffer.Length];
            buffer.CopyTo(buff, 0);

            PacketHandlerAuth.handlePacket(this, buff);

            new System.Threading.Thread(read).Start();
        }

        //private void termination()
        //{
        //    if (paused)
        //        return;

        //    CLogger.error("AuthThread: reconnecting...");
        //    connect();
        //}

        public void sendPacket(GameServerNetworkPacket pk)
        {
            if (IsConnected)
            {
                try
                {
                    pk.write();

                    byte[] data = pk.ToByteArray();
                    Int16 size = Convert.ToInt16(data.Length - 2);

                    List<byte> list = new List<byte>(data.Length + 2);
                    list.AddRange(BitConverter.GetBytes(size));
                    list.AddRange(data);

                    nstream.Write(list.ToArray(), 0, list.Count);
                    nstream.Flush();
                }
                catch
                {
                    FormManager.getForm().wait(false);
                    MessageBox.Show("Связь с сервером прервалась.\r\nЗавершение программы.", "Ошибка!", MessageBoxButton.OK,MessageBoxImage.Error); 
                    FormManager.getForm().CloseApplication(); 
                }
            }
        }

        //private bool paused = false;
        //public void loginFail(string code)
        //{
        //    paused = true;
        //    CLogger.error("AuthThread: "+code+". Please check configuration, server paused." );
        //    try
        //    {
        //        nstream.Close();
        //        lclient.Close();
        //    }
        //    catch {}
        //}



        public void disconnect()
        {
            
            try
            {
                lclient.Close();
                nstream.Close();
            }
            catch { }
        }
    }
}
