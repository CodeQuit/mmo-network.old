using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Cryptography;

namespace LGNetworkEngine.src
{
    public class LGNetwork_FileServices
    {
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(LGNetwork_FileServices));
        static string settings_lg = "Config.ini";

        #region Configuration
        private static string server_auth;
        private static string server_web;
        private static string web_path;
        private static string port;

        //Пути к логам
        public static string debug_file = "Log\\DebugMetods.log";
        public static string debugLG = LGNetwork_FileServices.LG_Path + "Log\\LauncherGame.log";

        //Путь к программе
        public static string LG_Path = "";

        //Чтение и декодировка завершено?
        public static bool completed = false;

        #endregion


        /// <summary>
        /// Расчет контрольной суммы
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        public static string delLastpattern(string s, char c)
        {
            string[] temp = s.Split(c);
            string output = string.Empty;

            for (int i = 0; i < (temp.Length - 1); i++)
            {
                output += temp[i] + "\\";
            }

            return output;
        }

        /// <summary>
        /// Выполнение начальных файловых функций программы.
        /// </summary>
        public bool Init()
        {
            Loggers.Debug("LGNetwork_FileServices::Init >>> ");

            try
            {

                getDecodingConfiguration(settings_lg);
                LG_Path = Directory.GetCurrentDirectory().ToString();
            }
            catch(Exception e)
            {
                Loggers.Fatal("LGNetwork_FileServices::Initialization >>>base64decode<<<");
                Loggers.Debug(": " + e.ToString());
            }
            if (!Directory.Exists("temp"))
            {
                try { Directory.CreateDirectory("temp"); }
                catch { }
            }
            if (!Directory.Exists("Log"))
            {
                try { Directory.CreateDirectory("Log"); }
                catch { }
            }
            if (server_auth != null && server_web != null)
            {
                Loggers.Debug("LGNetwork_FileServices::Init == true ");
                Loggers.Debug("LGNetwork_FileServices::Init <<< ");
                return true;
            }
            else
            {
                Loggers.Debug("LGNetwork_FileServices::Init == false ");
                Loggers.Debug("LGNetwork_FileServices::Init <<< ");
                return false;
            }
        }

        public LGNetwork_FileServices()
        {}

        /// <summary>
        /// Система шифрования текста.
        /// </summary>
        public void getDecodingConfiguration(string name)
        {
            Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration(string) >>> ");
            int current_line = 0;
            string data;
            try
            {
                string rev = null;
                string current_rev = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                string title = null;
                string _title = null;

                string _stage_1 = null, _stage_2 = null, _stage_3 = null;
                string[] _stage_final_ms = null;

                StreamReader file = new StreamReader(name);
                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration " + name);
                while ((data = file.ReadLine()) != null)
                {
                    //Проверяем 1 раз
                    if (rev == null)
                    {
                        Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration >>>>>searhing rev>>>>>");

                        _stage_1 = base64Decode(data);
                        if (_stage_1 != null)
                        {
                            Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration " + ">>>>>decoding data stage1 != null");
                            _stage_2 = base64Decode(_stage_1);
                        }
                        
                        if (_stage_2 != null)
                        {
                            Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration " + ">>>>>decoding data stage2 != null");
                            _stage_3 = base64Decode(_stage_2);
                        }

                        if (_stage_3 != null)
                        {
                            Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration " + ">>>>>decoding data stage3 != null");
                            rev = _stage_3;
                        }
                        
                        Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration <<<===<<<");
                        _stage_1 = null;
                        _stage_2 = null;
                        _stage_3 = null;

                        Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration >>>>>" + rev + " == rev" + current_rev + "");
                    }
                    else if (rev == "rev" + current_rev)
                    {
                        if (_title == "server")
                        {
                            _stage_1 = null;
                            _stage_2 = null;
                            _stage_3 = null;

                            _stage_1 = base64Decode(data);
                            _stage_2 = base64Decode(_stage_1);
                            _stage_3 = base64Decode(_stage_2);
                            _stage_final_ms = _stage_3.Split(new char[] { '=' });

                            if (_stage_final_ms[0] == "Auth") ///TODO Auth rename to server
                            {
                                server_auth = _stage_final_ms[1];
                            }
                            else if (_stage_final_ms[0] == "web") ///TODO web rename to site
                            {
                                server_web = _stage_final_ms[1];
                            }
                            else if (_stage_final_ms[0] == "path")
                            {
                                web_path = _stage_final_ms[1];
                            }
                            else if (_stage_final_ms[0] == "port")
                            {
                                port = _stage_final_ms[1];
                            }
                            else
                            {
                                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration >>>>>parametr_not_found " + _stage_final_ms[0] + "");
                            }
                        }
                        else
                            Loggers.Warn("LGNetwork_FileServices::getDecodingConfiguration >>>>>section != null");

                        if (title == null)
                        {
                            _stage_1 = base64Decode(data);
                            if (_stage_1 != null)
                            {
                                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration " + ">>>>>decoding data stage1 != null");
                                _stage_2 = base64Decode(_stage_1);
                            }
                            if (_stage_2 != null)
                            {
                                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration " + ">>>>>decoding data stage2 != null");
                                _stage_3 = base64Decode(_stage_2);
                            }
                            if (_stage_3 != null)
                            {
                                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration " + ">>>>>decoding data stage3 != null");
                                title = _stage_3;
                            }

                            string[] _t = title.Split(new char[] { '[', ']' });
                            _title = _t[1];

                            _stage_1 = null;
                            _stage_2 = null;
                            _stage_3 = null;
                        }
                    }
                    else
                    {
                        Loggers.Fatal("LGNetwork_FileServices::getDecodingConfiguration >>>>> " + current_rev + " != " + rev + " file " + settings_lg);
                        MessageBox.Show("Error " + current_rev + " != " + rev + " Обратитесь за помощью к администратору системы LauncherGame.");
                    }
                    current_line++;
                }
            }
            catch (FileNotFoundException)
            {
                Loggers.Fatal("LGNetwork_FileServices::getDecodingConfiguration " + settings_lg + " != " + "null");
                MessageBox.Show("LGNetwork_FileServices::getDecodingConfiguration >>>>>File not found " + settings_lg);
            }
            catch (FormatException)
            {
                Loggers.Fatal("LGNetwork_FileServices::getDecodingConfiguration " + settings_lg + " != base64");
                MessageBox.Show("LGNetwork_FileServices::getDecodingConfiguration >>>>>Cannot decode file " + settings_lg);
            }
            catch (Exception e)
            {
                Loggers.Fatal("LGNetwork_FileServices::getDecodingConfiguration >>>>>" + settings_lg + " lenght != " + "0");
                MessageBox.Show("File invalid " + settings_lg + " lenght == 0");
            }

            if (current_line == 0)
                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration >>>>>current_line != 0");
            else
                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration >>>>>Line = " + current_line);

            Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration <<< ");
        }

        public string[] ListReader(string name)
        {
            string[] data = null;
            StreamReader file = new StreamReader(name);
            int line = 0;
            Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration line " + line.ToString());
            while ((data[line] = file.ReadLine()) != null)
            {
                Loggers.Debug("LGNetwork_FileServices::getDecodingConfiguration line " + data[line].ToString());
                line++;
            }
            return data;
        }

        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode " + e.Message);
            }
        }

        public string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode " + e.Message);
            }
        }

        public static string getServer_Auth()
        {
            return server_auth;
        }

        public static string getServer_Web()
        {
            return server_web;
        }

        public static string getWeb_Path()
        {
            return web_path;
        }

        public static string getPort()
        {
            return port;
        }
    }
}
