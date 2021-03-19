using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LGNetworkEngine.src.LGNetwork_Network
{
   public static class LGNetwork_InternetCheck
    {
        //Импортируем библиотеку для проверки соединения с интернетом
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Creating a function that uses the API function...
        /// </summary>
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        /// <summary>
        /// Проверка интернета.
        /// </summary>
        public static bool IsConnected(string webservices)
        {
            System.Uri Url = new System.Uri("http://" + webservices + "/");
            System.Net.WebRequest WebReq;
            System.Net.WebResponse Resp;
            WebReq = System.Net.WebRequest.Create(Url);

            try
            {
                Resp = WebReq.GetResponse();
                Resp.Close();
                WebReq = null;
                return true;
            }

            catch (Exception)
            {
                WebReq = null;
                return false;
            }
        }
    }
}
