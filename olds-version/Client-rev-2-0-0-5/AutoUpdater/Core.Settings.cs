//Copyright DarkTeam.zapto.org
using System;
using System.IO;
using System.Xml;

namespace AutoUpdater
{
    public class Settings
    {
        // Путь к файлу с настройками
        private String configFile = "updater.xml";

        // Свойства для передачи данных из файла настроек
        public String UpdateServerVersionsUrl { get; set; }
        public String UpdateServerMainExeUrl { get; set; }
        public String LocalMainExe { get; set; }
        public String Version { get; set; }

        public Settings() { Version = "0"; }

        #region :: Methods ::
        public void Save()
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                {
                    xdoc.AppendChild(xdoc.CreateNode(XmlNodeType.XmlDeclaration, "", ""));

                    XmlNode conf = xdoc.CreateElement("ConfigurationFile");
                    {
                        XmlNode lNode = xdoc.CreateElement("UpdateServerVersionsUrl");
                        XmlNode lNodeText = xdoc.CreateTextNode(UpdateServerVersionsUrl);

                        lNode.AppendChild(lNodeText);
                        conf.AppendChild(lNode);

                        XmlNode pNode = xdoc.CreateElement("UpdateServerMainExeUrl");
                        XmlNode pNodeText = xdoc.CreateTextNode(UpdateServerMainExeUrl);

                        pNode.AppendChild(pNodeText);
                        conf.AppendChild(pNode);

                        XmlNode mNode = xdoc.CreateElement("LocalMainExe");
                        XmlNode mNodeText = xdoc.CreateTextNode(LocalMainExe);

                        mNode.AppendChild(mNodeText);
                        conf.AppendChild(mNode);

                        XmlNode vNode = xdoc.CreateElement("Version");
                        XmlNode vNodeText = xdoc.CreateTextNode(Version.ToString());

                        vNode.AppendChild(vNodeText);
                        conf.AppendChild(vNode);
                    }
                    xdoc.AppendChild(conf);
                }
                xdoc.Save(configFile);
            }
            catch { }
        }

        public void Load()
        {
            //if (File.Exists(configFile))
            //{
            //    try
            //    {
            //        XmlDocument xdoc = new XmlDocument();
            //        xdoc.Load(configFile);

            //        XmlNode reader = xdoc.SelectSingleNode("ConfigurationFile/UpdateServerVersionsUrl");
            //        //if (reader != null)
            //        //    UpdateServerVersionsUrl = reader.InnerText;

            //        //reader = xdoc.SelectSingleNode("ConfigurationFile/UpdateServerMainExeUrl");
            //        //if (reader != null)
            //        //    UpdateServerMainExeUrl = reader.InnerText;

            //        //reader = xdoc.SelectSingleNode("ConfigurationFile/LocalMainExe");
            //        //if (reader != null)
            //        //    LocalMainExe = reader.InnerText;



            //        reader = xdoc.SelectSingleNode("ConfigurationFile/Version");
            //    }
            //    catch { }
            //}

            UpdateServerVersionsUrl = "http://localhost/newupdater/versions.file";
            UpdateServerMainExeUrl = "http://localhost/newupdater/update.exe";
            LocalMainExe = "update.bat";
            Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        #endregion
    }
}
