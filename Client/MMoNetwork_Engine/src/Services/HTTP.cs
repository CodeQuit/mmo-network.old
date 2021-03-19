using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Windows.Documents;
using LGNetworkEngine.src;
using Engine;

namespace network
{
    public class MMoNetwork_HTTP
    {
        public string host = "http://localhost:8087/server/"; //Адрес подключения к серверу...
        Logger MMO = new Logger(typeof(MMoNetwork_HTTP));
        public MMoNetwork_HTTP()
        { }

        public string GetData(string data)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(host + "/" + data + "/");
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch
            {
                return "ERROR";
            }
        }

        public void downloadFile(string path, string fileName)
        {
            try
            {
                WebClient web = new WebClient();
                Uri url = new Uri(host + "/" + path + fileName);
                web.DownloadFileAsync(url, "Data\\Content\\" + fileName);
                MMO.log("MainWindow", ">>>>> " + host + "/" + path + fileName);
            }
            catch { }
        }
    }
}
