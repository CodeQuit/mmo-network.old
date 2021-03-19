//Copyright DarkTeam.zapto.org
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MMO_NETWORK_LAUNCHER.src.services;
using MMO_NETWORK_LAUNCHER.src.windows;
using log4net;
using network;
using RusLang;
using LGNetworkEngine.src.LGNetwork_Network;
using LGNetwork;
using LGNetworkEngine.src;
using Ionic.Zip;
using System.Text;
using System.Collections.Generic;
using MMO_NETWORK_LAUNCHER.src.services.updater;
using MMO_NETWORK_LAUNCHER.Properties;
using Shield;
using MMO_NETWORK_LAUNCHER.src.networks;
using MMO_NETWORK_LAUNCHER.src.model;

namespace MMO_NETWORK_LAUNCHER
{
    public partial class LauncherGame : Form
    {
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(LauncherGame));

        LGNETWORK_FILESERVICE_XML XML = new LGNETWORK_FILESERVICE_XML();

        #region Переменные 
        //Индекс игры.
        public static int gameindex;
        //Статус игры.
        public static int GameCurrentIndex;
        //Переменная под IP.
        public string Clientip;

        //Версия на сервере
        public static string ServerVersion;
        //Фикс для обновления
        public int fixUpdate = 0;
        //Пути к играм
        public string Write_Aion_Path;
        public string Write_Lineage2_Path;
        public string Write_PointBlank_Path;
        public string Write_WOW_Path;

        string _tmp_login;
        string _tmp_password;

        //_TcpClient tcp = new _TcpClient();
        //LGNetworkEngine.src.LGNetwork_Network.

        public static string[] _revision = { "alpha", "betta", "release" };
        public static string versionClient = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        //
        bool update_thread_bool = false;
        //
        List<ClientFile> clientFiles = new List<ClientFile>();
        string[] lines;

        public int maxFiles;
        public int filesToUpdate = 0;
        public int filesRemains = 0;
        public string UPDATE_URL;

        public static string MainDirSavePath;
        public static string pathGame = "";
        //

        #endregion

        #region Метод "Выход из приложения" 
        protected override void OnClosing(CancelEventArgs e)
        {
            string data = tp.GetData("logout/" + localization_lang_14.Text);
            if (data == "OK")
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Выход неудался, попробуйте еще раз.", "Ошибка.");
            }
        }
        #endregion


        //Инструкции по обновлению игры
        public void setPath(string maindir, string web, string game)
        {
            MainDirSavePath = maindir + "\\";
            UPDATE_URL = "http://" + web + "/update/game/" + game + "/";
            //MessageBox.Show("Update url: " + UPDATE_URL);
            //MessageBox.Show("main dir: " + maindir);
            this.Invoke((MethodInvoker)delegate()
            {
                progressBar_FULL.Visible = true;
                Status_update_localize.Visible = true;
                status_update.Visible = true;
            });
            if (clientFiles.Count > 0)
            {
                clientFiles.Clear();
            }
        }


        /// <summary>
        /// Загрузчик контрольной суммы бля...
        /// </summary>
        public void downloadHashes()
        {
            try
            {
                WebClient webClient = new WebClient();
                //MessageBox.Show(MainDirSavePath + "hash.txt");
                //MessageBox.Show(UPDATE_URL + "hash.txt");
                Uri ui = new Uri(UPDATE_URL + "hash.txt");
                
                webClient.DownloadFile(ui, MainDirSavePath + "hash.txt");

               

                lines = File.ReadAllLines(MainDirSavePath + "hash.txt");
                File.Delete(MainDirSavePath + "hash.txt");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Could not to connect to update server", "Attention!");
            }
        }

        public void Update_now()
        {
            update_thread_bool = true;
            downloadHashes();

            if (lines == null)
            {
                return;
            }



            foreach (String line in lines)
            {
                string[] fileInfo = line.Split(':');
                clientFiles.Add(new ClientFile(fileInfo[0], fileInfo[1]));
            }

            checkFolders(clientFiles);

            foreach (ClientFile clientFile in clientFiles)
            {
                string path = (MainDirSavePath + clientFile.Name);
                bool exists = System.IO.File.Exists(path);

                if (!exists || isUpdateRequired(path, clientFile.Hash))
                {
                    filesToUpdate++;
                    filesRemains++;
                }
            }

            maxFiles = filesRemains;
            this.Invoke((MethodInvoker)delegate()
            {
                progressBar_FULL.Maximum = maxFiles;
            });
            //MessageBox.Show(maxFiles.ToString());

            foreach (ClientFile clientFile in clientFiles)
            {
                string path = (MainDirSavePath + clientFile.Name);
                bool exists = System.IO.File.Exists(path);

                if (!exists || isUpdateRequired(path, clientFile.Hash))
                {
                        downloadFile(clientFile.Name);
                        if (filesRemains == 0)
                        {
                            update_thread_bool = false;
                        }
                }
                //MessageBox.Show(clientFile.Name);
            }

            if (filesRemains == 0)
                onUpdateEnd();

            
        }

        void checkFolders(List<ClientFile> list)
        {
            foreach (ClientFile clientFile in list)
            {
                string filePath = (MainDirSavePath + "\\" + clientFile.Name);
                string[] patterns = filePath.Split('\\');
                int num = patterns.Length;

                if (num > 0)
                {
                    string path = LGNetwork_FileServices.delLastpattern(filePath, '\\');
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                }
            }
        }

        public void downloadFile(string fileName)
        {
            try
            {
                WebClient web = new WebClient();
                Uri url = new Uri(UPDATE_URL + fileName);
                web.DownloadFileCompleted+= new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                web.DownloadFileAsync(url, MainDirSavePath + fileName);
                this.Invoke((MethodInvoker)delegate()
                { status_update.Text = fileName; });
            }
            catch { }
        }

        public bool isUpdateRequired(string path, string hashcode)
        {
            FileInfo file = new FileInfo(path);
            string hash = LGNetwork_FileServices.GetHashString(file.Length.ToString());

            if (!hash.Equals(hashcode))
                return true;
            else
                return false;

        }
        int progress;
        public void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            filesRemains--;
            progress = progress + 1;
            //MessageBox.Show(progress.ToString());

            if (maxFiles < progress)
            {
                progress = maxFiles;
            }

            this.Invoke((MethodInvoker)delegate()
            {
                this.Refresh();
                progressBar_FULL.Value = progress;
                this.Refresh();
            });   
            if (filesRemains == 0)
            {
                onUpdateEnd();
            }

        }

        public void onUpdateEnd()
        {
            start_game.Enabled = true;
            update_thread_bool = false;
            setStatusUp("Update completed!"); ;
            //progressBar1.Value = 100;
        }


        public void setStatusUp(string status)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                this.Refresh();
                status_update.Text = status;
                this.Refresh();
            });
        }

       

        ///
        /// <summary>
        /// Установка.. Язык.. Русский
        /// </summary>
        public void LangRus()
        {
            try
            {
                BindResourses("russian");
            }
            catch (FileNotFoundException e4)
            {
                MessageText(1005);
                Loggers.Error(e4.ToString());
            }
        }

        public void SetParametrForm()
        {
            //this.Enter_MMO.Location = new System.Drawing.Point(304, 208);
            /////
            //this.localization_lang_14.Location = new System.Drawing.Point(305, 129);
            //this.localization_lang_15.Location = new System.Drawing.Point(304, 166);
            //this.localization_lang_13.Location = new System.Drawing.Point(626, 392);
            /////
            //this.Login_panel.Size = new System.Drawing.Size(822, 413);
            //this.Login_panel.Location = new System.Drawing.Point(-2, 0);
            /////
            //this.Profilact.Size = new System.Drawing.Size(822, 413);
            //this.Profilact.Location = new System.Drawing.Point(-2, 0);

            //this.RememberBox.Location = new System.Drawing.Point(370, 190);

            //Profilact
            //784; 430
            //ZButtonEnter 304; 208
            //ZButtonCancel 397; 208
            //textBox1 305; 129
            //textBox2 304; 166
            //Stats 626; 392

            //822; 413
        }

        /// <summary>
        /// Сервис авторизации.
        /// </summary>
        public bool Connector()
        {
            Loggers.Debug("LauncherGame::Connector >>>");
            string Login = localization_lang_14.Text;
            string Password = localization_lang_15.Text;
            Loggers.Debug("LauncherGame::Connector::login=" + Login + "::password=" + Password);

            //tcp.PacketAuth(Login, Password);


            //while (true)
            //{
            //    Thread.Sleep(200);
            //    if (PrivateData.AuthCode > 0)
            //    {
            //        break;
            //    }
            //}

            //switch (PrivateData.AuthCode)
            //{
            //    case 3000:
            //        PlayerInfo.playername = Login;
            //        _tmp_login = Login;
            //        _tmp_password = Password;
            //        this.Invoke((MethodInvoker)delegate()
            //        {
            //            this.Login_panel.Visible = false;
            //            this.localization_lang_14.Enabled = true;
            //            this.localization_lang_15.Enabled = true;
            //            this.Enter_MMO.Enabled = true;
            //        });
            //        Loggers.Debug("auth => 3000");
            //        Loggers.Debug("LauncherGame::Connector <<<");
            //        return true;

            //    case 3001:
            //        this.Invoke((MethodInvoker)delegate()
            //        {
            //            this.Login_panel.Visible = true;
            //            this.localization_lang_14.Enabled = true;
            //            this.localization_lang_15.Enabled = true;
            //            this.Enter_MMO.Enabled = true;
            //        });
            //        MessageBox.Show("Неверный логин или пароль.", "Ошибка.");
            //        Loggers.Debug("auth => 3001");
            //        Loggers.Debug("LauncherGame::Connector <<<");
            //        return false;

            //    case 3002:
            //        this.Invoke((MethodInvoker)delegate()
            //        {
            //            this.Login_panel.Visible = true;
            //            this.localization_lang_14.Enabled = true;
            //            this.localization_lang_15.Enabled = true;
            //            this.Enter_MMO.Enabled = true;
            //        });
            //        MessageBox.Show("Аккаунт заблокирован.", "Ошибка.");
            //        Loggers.Debug("auth => 3002");
            //        Loggers.Debug("LauncherGame::Connector <<<");
            //        return false;

            //    default:
            //        return false;

            //}

            string data = tp.GetData("ru/" + Login + "/" + Password);

            if (data == null)
            {
                Loggers.Debug("data == null");
            }
            Thread.Sleep(2000);
            switch (data)
            {
                case "OK":
                    PlayerInfo.playername = Login;
                    _tmp_login = Login;
                    _tmp_password = Password;
                    //PlayerInfo.LGMoney = Convert.ToInt32(data[4]);
                    Thread.Sleep(2000);
                    this.Invoke((MethodInvoker)delegate()
                    {
                        this.Login_panel.Visible = false;
                        this.localization_lang_14.Enabled = true;
                        this.localization_lang_15.Enabled = true;
                        this.Enter_MMO.Enabled = true;
                    });
                    Loggers.Debug("auth => OK");
                    Loggers.Debug("LauncherGame::Connector <<<");
                    return true;

                case "NO":
                    this.Invoke((MethodInvoker)delegate()
                    {
                        this.Login_panel.Visible = true;
                        this.localization_lang_14.Enabled = true;
                        this.localization_lang_15.Enabled = true;
                        this.Enter_MMO.Enabled = true;
                    });
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка.");
                    Loggers.Debug("auth => NO");
                    Loggers.Debug("LauncherGame::Connector <<<");
                    return false;

                case "BLOCK":
                    this.Invoke((MethodInvoker)delegate()
                    {
                        this.Login_panel.Visible = true;
                        this.localization_lang_14.Enabled = true;
                        this.localization_lang_15.Enabled = true;
                        this.Enter_MMO.Enabled = true;
                    });
                    MessageBox.Show("Аккаунт заблокирован.", "Ошибка.");
                    Loggers.Debug("auth => BLOCK");
                    Loggers.Debug("LauncherGame::Connector <<<");
                    return false;

                default:
                    this.Invoke((MethodInvoker)delegate()
                    {
                        Login_panel.Visible = true;
                        localization_lang_14.Enabled = true;
                        localization_lang_15.Enabled = true;
                        Enter_MMO.Enabled = true;
                    });
                    MessageBox.Show("Подключение к серверу не прошло, попробуйте позже", "Ошибка.");
                    Loggers.Debug("LauncherGame::Connector <<<");
                    return false;
            }
        }

        LGNetwork_HTTP tp = new LGNetwork_HTTP();

        /// <summary>
        /// Проверка работы сервера(профилактика).
        /// </summary>
        public void CheckServer()
        {
            Loggers.Debug("LauncherGame::CheckServer >>>");

            string data = tp.GetData("server");

            if (data == null)
            {
                ShutdownHook.Shutdown();
            }

            if (data == "online")
            {
                Profilact.Visible = false;
            }
            else if (data == "offline")
            {
                Profilact.Visible = true;
            }
            Loggers.Debug("LauncherGame::CheckServer <<<");
        }

        /// <summary>
        /// Обработка информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxUpdate_Click(object sender, EventArgs e)
        {
            CheckServer();
        }

        private void Authform_Exit(object sender, FormClosingEventArgs e)
        {
            ShutdownHook.Shutdown();
        }

        /// <summary>
        /// Установка.. Язык.. Русский
        /// </summary>
        /// <param name="locale"></param>
        public void BindResourses(string locale)
        {
            Loggers.Debug("LauncherGame::BindResourses >>>");
            ResourceManager res_man = new ResourceManager(locale);
            game_3.Text = res_man.localization_lang_24;
            game_4.Text = res_man.localization_lang_25;
            localization_lang_05.Text = res_man.localization_lang_05;
            start_game.Text = res_man.localization_lang_12;
            localization_lang_23.Text = res_man.localization_lang_23;
            Enter_MMO.Text = res_man.localization_lang_03;
            localization_lang_06.Text = res_man.localization_lang_06;
            Loggers.Debug("LauncherGame::BindResourses <<<");
        }

        #region LauncherGame 
        /// <summary>
        /// Основа.
        /// </summary>
        public LauncherGame()
        {
            Loggers.Debug("LauncherGame::LauncherGame >>>");


            

            

            //Инициализировать файловую систему...
            LGNetwork_FileServices fs = new LGNetwork_FileServices();
            if (fs.Init() != true)
            {
                MessageBox.Show("LauncherGame::LauncherGame >>>FileSystem init failed<<<");
                Application.Exit();
            }

            //Диалоги ----> НУБАМ НЕ ЛАЗИТЬ СЮДА!!!
            Form Preload = new PreLoading();
            Preload.ShowDialog();

            //Отображаем форму :)
            InitializeComponent();
            this.Show();
            //_TcpClient tcp = new _TcpClient();
            LangRus();

            if (Properties.Settings.Default.LoginRemember != null || Properties.Settings.Default.LoginRemember != "")
            {
                localization_lang_14.Text = Properties.Settings.Default.LoginRemember;
                RememberBox.Checked = true;
            }
            else
            {
                RememberBox.Checked = false;
            }

            getversion();
            SetParametrForm();
            CheckServer();
            //version.Text = versionClient;
            GameList();

            if (game1 == "1")
                game_1.Enabled = true;
            else
                game_1.Enabled = false;

            if (game2 == "1")
                game_2.Enabled = true;
            else
                game_2.Enabled = false;

            if (game3 == "1")
                game_3.Enabled = true;
            else
                game_3.Enabled = false;

            if (game4 == "1")
                game_4.Enabled = true;
            else
                game_4.Enabled = false;

            webBrowser.SendToBack();

            //Обновляем форму, чтобы небыло пропавших элементов.
            this.Refresh();

            Loggers.Debug("LauncherGame::LauncherGame <<<");
        }
        #endregion

        #region Обработчики

        #endregion

        //Игры
        public static string game1;
        public static string game2;
        public static string game3;
        public static string game4;
        
        //Профилактика
        public static string GameProf1;
        public static string GameProf2;
        public static string GameProf3;
        public static string GameProf4;

        public void getversion()
        {
            string data = tp.GetData("launcher_rev");
            string vers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (data != "ERROR")
            {
                if (data != vers)
                {
                    Process pc = new Process();
                    pc.StartInfo.FileName = "AutoUpdater.exe";
                    pc.Start();
                    Close();
                }
            }
            else
            {
                Profilact.Visible = true;
            }
        }

        /// <summary>
        /// Получение списка игр
        /// </summary>
        public void GameList()
        {
            Loggers.Debug("LauncherGame::GameList >>>");

            string data = tp.GetData("glist");
            if (data != "ERROR")
            {
                string[] dat_t = data.Split(new Char[] { ' ' });

                game1 = dat_t[0];
                game2 = dat_t[1];
                game3 = dat_t[2];
                game4 = dat_t[3];

                if (dat_t[4] == "offline" && GameCurrentIndex == 1)
                {
                    //panelProfGame.Visible = true;
                    start_game.Enabled = false;
                }
                else
                {
                    //panelProfGame.Visible = false;
                    start_game.Enabled = true;
                }

                if (dat_t[5] == "offline" && GameCurrentIndex == 2)
                {
                    //panelProfGame.Visible = true;
                    start_game.Enabled = false;
                }
                else
                {
                    //panelProfGame.Visible = false;
                    start_game.Enabled = true;
                }

                if (dat_t[6] == "offline" && GameCurrentIndex == 3)
                {
                    //panelProfGame.Visible = true;
                    start_game.Enabled = false;
                }
                else
                {
                    //panelProfGame.Visible = false;
                    start_game.Enabled = true;
                }

                if (dat_t[7] == "offline" && GameCurrentIndex == 4)
                {
                    //panelProfGame.Visible = true;
                    start_game.Enabled = false;
                }
                else
                {
                    //panelProfGame.Visible = false;
                    start_game.Enabled = true;
                }
            }

            Loggers.Debug("LauncherGame::GameList <<<");
        }

        #region General metods 

        /// <summary>
        ///Обрабатываем нажатие кнопки - выводим страницу на экран, присваиваем индекс.
        /// </summary>
        public void GameButton_1_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://" + LGNetwork_FileServices.getServer_Web() + "/launch/game_mmonetwork/AION/");
            gameindex = 1;
            GameCurrentIndex = 1;

            GameList();

            if (GameCurrentIndex == 1)
            {
            }
        }

        /// <summary>
        /// Обрабатываем нажатие кнопки.
        /// </summary>
        private void GameButton_2_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://" + LGNetwork_FileServices.getServer_Web() + "/launch/game_mmonetwork/Lineage2/");
            gameindex = 2;
            GameCurrentIndex = 2;

            GameList();
 
            if (GameCurrentIndex == 2)
            {
            }
        }

        /// <summary>
        /// Универсальный метод запуска....
        /// </summary>
        public void GameRun(string path, string arg, string name, string exe)
        {
            if (path != null && File.Exists(path + "\\" + exe))
            {
                minimize(name);
                Shield_class.Init();
                Process _process = new Process();
                _process.StartInfo.FileName = path + "\\" + exe;
                _process.StartInfo.Arguments = arg;
                _process.StartInfo.UseShellExecute = true;
                _process.StartInfo.WorkingDirectory = path;
                _process.Start();
            }
            else
            {
                MessageText(1006);
            }
        }

        /// <summary>
        /// Указание пути к игре.
        /// </summary>
        public void PathToGame_Click(object sender, EventArgs e)
        {
            if (gameindex == 0)
            {
                MessageText(1001);
            }
            if (gameindex == 1)
            {
                DialogResult dr1 = FolderDialog.ShowDialog();
                if (dr1 == DialogResult.OK)
                {
                    Write_Aion_Path = FolderDialog.SelectedPath;
                    Properties.Settings.Default.AionPath = Write_Aion_Path;
                    Properties.Settings.Default.Save();
                }
            }
            if (gameindex == 2)
            {
                DialogResult dr2 = FolderDialog.ShowDialog();
                if (dr2 == DialogResult.OK)
                {
                    Write_Lineage2_Path = FolderDialog.SelectedPath;
                    Properties.Settings.Default.Lineage2Path = Write_Lineage2_Path;
                    Properties.Settings.Default.Save();
                }
            }

            if (gameindex == 3)
            {
                DialogResult dr3 = FolderDialog.ShowDialog();
                if (dr3 == DialogResult.OK)
                {
                    Write_PointBlank_Path = FolderDialog.SelectedPath;
                    Properties.Settings.Default.PointBlankPath = Write_PointBlank_Path;
                    Properties.Settings.Default.Save();
                }
            }

            if (gameindex == 4)
            {
                DialogResult dr4 = FolderDialog.ShowDialog();
                if (dr4 == DialogResult.OK)
                {
                    Write_WOW_Path = FolderDialog.SelectedPath;
                    Properties.Settings.Default.WowPath = Write_WOW_Path;
                    Properties.Settings.Default.Save();
                }
            }
        }

        public string getStatusGame(int id)
        {
            try
            {
                string data = tp.GetData("gamestartid/" + id + "/" + _tmp_login);
                return data;
            }
            catch
            {
                return "ERR#02";
            }
        }

        /// <summary>
        /// Запускаем игру в зависимости от ID.
        /// Поидее нужно будет дорабsотать запуск игры
        /// </summary>
        private void StartGame_Click(object sender, EventArgs e)
        {
            this.Refresh();
            start_game.Enabled = false;
            wait_text.Visible = true;
            GameWait.Visible = true;
            this.Refresh();
            Thread th = new Thread(GameStart);
            th.Start();
            this.Refresh();
        }


        public void GameStart()
        {
            string data = getStatusGame(gameindex);
            this.Invoke((MethodInvoker)delegate()
            {
                this.Refresh();
            });

            Thread.Sleep(5000);

            if (data == "game_ready")
            {
                switch (gameindex)
                {
                    case 1:
                        {
                            Loggers.Debug("gameindex == 1");
                            setPath(Properties.Settings.Default.AionPath, "mmo-network.ru", "AION");
                            Update_now();
                            GameRun(Properties.Settings.Default.AionPath, "", "AION", "AION.exe");
                            break;
                        }
                    case 2:
                        {
                            Loggers.Debug("gameindex == 2");
                            setPath(Properties.Settings.Default.Lineage2Path, "mmo-network.ru", "Lineage2");
                            Update_now();

                            GameRun(Properties.Settings.Default.Lineage2Path, "", "LINEAGE2", "lineage2.exe");
                            break;
                        }
                    case 3:
                        {
                            //http://mmo-network.ru/
                            Loggers.Debug("gameindex == 3");
                            string path = Properties.Settings.Default.PointBlankPath;
                            setPath(path, "mmo-network.ru", "PointBlank");
                            //MessageBox.Show("");
                            Update_now();
                            while (true)
                            {
                                if (!update_thread_bool)
                                {
                                    start_game.Enabled = true;
                                    break;
                                }
                            }
                            //Update_now();
                            if (update_thread_bool == false)
                            {
                                GameRun(path, localization_lang_14.Text + " " + localization_lang_15.Text, "POINT BLANK", "PointBlank.exe");
                            }
                            break;
                        }
                    case 4:
                        {
                            Loggers.Debug("gameindex == 4");
                            setPath(Properties.Settings.Default.WowPath, "mmo-network.ru", "WOW");
                            Update_now();

                            GameRun(Properties.Settings.Default.WowPath, "", "WOW", "wow.exe");
                            break;
                        }
                    default:
                        Loggers.Debug("gameindex > 4 || gameindex < 1");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Произошла ошибка...Попробуйте еще раз..");
            }

            this.Invoke((MethodInvoker)delegate()
            {
                wait_text.Visible = false;
                start_game.Enabled = true;
                GameWait.Visible = false;
            });
        }
 
        /// <summary>
        /// Метод сворачивания программы.   
        /// </summary>
        public void minimize(string game)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                this.WindowState = FormWindowState.Minimized;
                trayicon.Visible = this.WindowState == FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                trayicon.ShowBalloonTip(5000, "LauncherGame.", "Игра " + game + " запускается...", ToolTipIcon.Info);
            });
        }

        /// <summary>
        /// Прочие методы.
        /// </summary>
        private void trayicon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            trayicon.Visible = false;
        }

        /// <summary>
        /// Закрытие формы.
        /// </summary>
        private void LauncherGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            string data = tp.GetData("logout/" + localization_lang_14.Text);
            if (data == "OK")
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Выход неудался, попробуйте еще раз.", "Ошибка.");
            }
        }

        /// <summary>
        /// Метод Загрузки формы.
        /// </summary>
        private void LauncherGame_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Метод проверки порта.
        /// </summary>
        public void portopen(string addressServer)
        {
            string hostname = LGNetwork_FileServices.getServer_Web();
            int portno = 80;
            IPAddress ipa = (IPAddress)Dns.GetHostAddresses(hostname)[0];

            try
            {
                System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                sock.Connect(ipa, portno);
                sock.Close();
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                if (ex.ErrorCode == 10061)  // Port is unused and could not establish connection 
                {
                    //webBrowser.Navigate(FileServices.LG_Path + "\\Data\\HTML\\ErrorConnectiontoServer.htm");
                }
                else
                {
                    //webBrowser.Navigate(FileServices.LG_Path + "\\Data\\HTML\\ErrorConnectiontoServer.htm");
                }
                src.service.Debugger.Debugger.LogError(ex.ToString());
            }
        }

        #endregion

        private void Exit_Click(object sender, EventArgs e)
        {
            string data = tp.GetData("logout");
            if (data == "OK")
            {
                //MessageBox.Show(data);
                Login_panel.Visible = true;
            }
            else
            {
                MessageBox.Show("Выход неудался, попробуйте еще раз.", "Ошибка.");
            }
        }

        private void GameButton_3_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://" + LGNetwork_FileServices.getServer_Web() + "/launch/game_mmonetwork/POINTBLANK/");
            gameindex = 3;
            GameCurrentIndex = 3;

            GameList();

            if (GameCurrentIndex == 3)
            {
            }
        }

        private void GameButton_4_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://" + LGNetwork_FileServices.getServer_Web() + "/launch/game_mmonetwork/WOW/");
            gameindex = 4;
            GameCurrentIndex = 4;

            GameList();

            if (GameCurrentIndex == 4)
            {
            }
        }

        public void MessageText(int text)
        {
            switch (text)
            {
                case 1000:
                    MessageBox.Show("Ошибка при обработке данных...", "Ошибка.");
                    break;

                case 1001:
                    MessageBox.Show("Игра не выбрана, выберите игру для запуска", "Ошибка");
                    break;

                case 1002:
                    MessageBox.Show("При выполнении процедуры загрузки программы произошли ошибки. Продолжение невозможно.", "Ошибка.");
                    break;

                case 1003:
                    MessageBox.Show("Увы, не удалось подключиться к серверу, проверьте подключение к интернету.", "Ошибка.");
                    break;

                case 1004:
                    MessageBox.Show("Путь к игре не указан. Укажите путь к игре.", "Ошибка");
                    break;
                        
                case 1005:
                    MessageBox.Show("Приложение запустилось некорректно т.к. отсутствует библиотека Rus.dll", "Ошибка.");
                    break;

                case 1006:
                    MessageBox.Show("Путь к игре неверный. Укажите путь к игре.", "Ошибка");
                    break;

                case 1007:
                    MessageBox.Show("Игра не доступна на сервере.", "Сообщение.");
                    break;
            }
        }


        public void showMainForm()
        {
            if (Connector() == true)
            {
                //Form fm = new ReadingDataForm();
                //fm.Show();

                if (PlayerInfo.playername != null)
                {
                    webBrowser.Navigate("http://" + LGNetwork_FileServices.getServer_Web() + "/AutorizeSystem/News.html");

                    portopen("http://mmo-network.ru/");
                    //Form ProgressFormData = new ReadingDataForm();
                    //ProgressFormData.ShowDialog();

                    //Проверка, прошла ли авторизация и получен ли никнейм.
                    if (PlayerInfo.playername == null)
                    {
                        //this.Hide();
                        Application.Exit();
                    }

                    //Устанавливаем инфу по плееру =)
                    this.Invoke((MethodInvoker)delegate()
                    {
                        this.player_name.Text = PlayerInfo.playername;
                        this.LGPoint.Text = PlayerInfo.LGMoney.ToString();
                    });

                    if (PlayerInfo.playername.ToLower() == "admin")
                        player_name.ForeColor = Color.Red;
                    else if (PlayerInfo.playername.ToLower() == "moderator")
                        player_name.ForeColor = Color.Green;
                    else
                        player_name.ForeColor = Color.Black;
                }
                else
                {
                    MessageText(1002);
                }
            }
        }

        private void pictureBoxUpdate_Click_1(object sender, EventArgs e)
        {
            CheckServer();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.FileName = "http://mmo-network.ru/users/login";
            pr.Start();
        }

        private void Enter_MMO_Click(object sender, EventArgs e)
        {
            if (localization_lang_14.TextLength > 3 && localization_lang_14.TextLength > 3)
            {
                Login_panel.Visible = true;
                localization_lang_14.Enabled = false;
                localization_lang_15.Enabled = false;
                Enter_MMO.Enabled = false;
                Thread net = new Thread(showMainForm);
                net.Start();

                if (RememberBox.Checked == true)
                {
                    //если remember box стоит с галочкой то запоминаем логин, если нет то не запоминаем(настройки хранятся у клиента)
                    Properties.Settings.Default.LoginRemember = localization_lang_14.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.LoginRemember = null;
                    Properties.Settings.Default.Save();
                }

            }
            else
            {
                MessageBox.Show("Не введено имя пользователя или пароль.", "Ошибка");
            }
        }

        private void Register_link_Click(object sender, EventArgs e)
        {
            Process pc = new Process();
            pc.StartInfo.FileName = "http://mmo-network.ru/users/register/";
            pc.Start();
        }

        private void minimize_transp_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            trayicon.Visible = this.WindowState == FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void close_transp_Click(object sender, EventArgs e)
        {
            string data = tp.GetData("logout/" + localization_lang_14.Text);
            if (data == "OK")
            {
                Application.Exit();
            }
            else
            {
                //MessageBox.Show("Выход неудался, попробуйте еще раз.", "Ошибка.");
                Application.Exit();
            }
        }


    }
}