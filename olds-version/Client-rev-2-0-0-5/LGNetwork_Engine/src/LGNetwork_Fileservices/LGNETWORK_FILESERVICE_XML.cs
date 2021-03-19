using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using log4net;
using System.Windows.Forms;

namespace MMO_NETWORK_LAUNCHER.src.services
{
    /// <summary>
    /// Класс работы с xml в ядре.
    /// </summary>
    public class LGNETWORK_FILESERVICE_XML
    {
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(LGNETWORK_FILESERVICE_XML));
        
        /// <summary>
        /// Метод создание папки config и создания файла gamefilename
        /// </summary>
        public void xml_INIT(string filename)
        {
            Loggers.Debug("function::module::xml::xml_INIT()");
            try
            {
                if (!Directory.Exists("config"))
                {
                    Directory.CreateDirectory("config");
                    if (!File.Exists("config\\" + filename + ".xml"))
                    {
                        File.Create("config\\" + filename + ".xml");
                    }
                }
            }
            catch { Loggers.Error("creating directory::result => failed /xml_INIT()"); }
            finally { Loggers.Info("creating directory::result => ok /xml_INIT()"); } 
        }


        public void xml_create(string file, string pathstring)
        {
            Loggers.Debug("function::module::xml::xml_create()");
            try
            {
                if(File.Exists("config\\" + file + ".xml"))
                {
                    File.Delete("config\\" + file + ".xml");
                }

                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                XmlNode configuration = doc.CreateElement("configuration");
                doc.AppendChild(configuration);

                doc.Value = pathstring;

                doc.Save("config\\" + file + ".xml");
            }
            catch
            { Loggers.Error("creating xml config::result => failed /xml_create()"); }
            finally { Loggers.Info("creating xml config::result => ok /xml_create()"); }
        }

        public string xml_read(string file)
        {
            Loggers.Debug("function::module::xml::xml_read()");
            try
            {
                string path = "";
                XmlTextReader reader = new XmlTextReader("config\\" + file + ".xml");
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text: // Вывести текст в каждом элементе.
                            
                            path = reader.Value;
                            //MessageBox.Show(path);
                            break;
                    }
                }
                reader.Close();
                return path;
            }
            catch { Loggers.Error("reading xml config::result => failed /xml_read()"); return "failed"; }
            finally { Loggers.Info("reading xml config::result => ok /xml_read()"); }
        }

        public bool Delete(string filename)
        {
            Loggers.Debug("function::module::xml::Delete()");
            try
            {
                File.Delete("config\\" + filename + ".xml");
                return true;
            }
            catch { return false; }
        }
    }
}
