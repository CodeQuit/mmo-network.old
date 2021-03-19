using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace network
{
    public class LGNetwork_HTTP
    {
       //public string GetData(string data)
       public string GetData(string data)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://mmo-network.ru/launch/" + data + "/");
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                //MessageBox.Show("Подключение к серверу не удалось, попробуйте позже.", "Ошибка");
                return "ERROR";
            }
        }
    }
}
