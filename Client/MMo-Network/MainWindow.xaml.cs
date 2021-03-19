using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using network;
using MMo_Network.src.models;
using LGNetworkEngine.src;
using System.Windows.Threading;
using System.Drawing;
using System.Reflection;
using MMo_Network.src.services;
using MMo_Network.src.exceptions;
using MMo_Network.network.loginauth;
using MMo_Network.network.loginauth.send;
using Engine;

namespace MMo_Network
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthThread at = new AuthThread();
        //WebBrowser GameInformation = new WebBrowser();
        MMoNetwork_HTTP tp = new MMoNetwork_HTTP();
        Register register = new Register();
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        public static int gameindex = -1;
        public static string versionClient = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public int GameCurrentIndex = 0;
        public static string ServerVersion;
        List<ClientFile> clientFiles = new List<ClientFile>();
        string[] lines;
        public int maxFiles;
        public int filesToUpdate = 0;
        public int filesRemains = 0;
        public string UPDATE_URL;
        public static string MainDirSavePath;
        public static string pathGame = "";
        string Login, Password;
        string gamepath = "game_patch_";
        public List<GamesInfo> gsinfo_pub = new List<GamesInfo>();
        System.Windows.Forms.FolderBrowserDialog FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
        ShieldDLL.Shield sh = new ShieldDLL.Shield();
        //
        Logger MMO = new Logger(typeof(MainWindow));

        public MainWindow()
        {
            if (File.Exists("MMO.log"))
                File.Delete("MMO.log");

            MMO.log("MainWindow", "========================================================");
            MMO.log("MainWindow", "Starting MMo-Network Client version " + versionClient);
            MMO.log("MainWindow", "Copyright MMo-Network.ru @ 2012 ");
            MMO.log("MainWindow", "========================================================");
            PreStart pr = new PreStart();
            pr.ShowDialog();
            //pr.ShowInTaskbar = true;
            InitializeComponent();
            GameList_box.SelectedIndex = -1;
            //ShowAssemblyVersion();

            //Thread m = new Thread(inMaintenance);
            //m.Name = "inMaintenance";
            //m.Start();

            string LoginRemember = register.loadReg("LoginRemember");

            if (LoginRemember != null)
            {
                login_box.Text = LoginRemember;
            }

            //скрываем главную форму, без этого графич косяки
            MainForm.Visibility = Visibility.Hidden;

            //Устанавливаем в текстовую строку версию клиента
            version.Content = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            //Подгружаем настройки трея...
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Click += new EventHandler(notifyIcon_Click);
            notifyIcon.Icon = Properties.Resources.icons;
            notifyIcon.Text = "MMo-Network Client";
            notifyIcon.Visible = true;
            onlyvpn.Visibility = Visibility.Hidden;
            Show();

            
            at.connect();

            if (!at.IsConnected)
            {
                MessageBox.Show("Соединение с сервером не удалось.", "Ошибка.");
                Application.Current.Shutdown(0);
            }
            //Проверяем версию программы.
            getversion();
            
        }

        public void setNetworkInfo(int sess, int crypt, int hash)
        {
            MMO.log("setNetworkInfo", "Set Network Info >>>");
            at.setSessionId(sess);
            at.setCryptKey(crypt);
            at.setHashCode(hash);
            MMO.log("setNetworkInfo", ">>>>> Set Network Info");
            MMO.log("setNetworkInfo", "<<<");
        }

        /// <summary>
        /// Закрывает приложение и его формы
        /// </summary>
        public void CloseApplication()
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                at.disconnect();
                this.Close();
                Application.Current.Shutdown();
            }));
        }

        public void ShowAssemblyVersion()
        {
            MMO.log("ShowAssemblyVersion", "==========================<Library>==========================");
            Process pro = Process.GetCurrentProcess();
            ProcessModuleCollection pMc = pro.Modules;
            for (int i = 0; i < pMc.Count; i++)
            {
                ProcessModule pm = pMc[i];
                MMO.log("ShowAssemblyVersion", pm.FileVersionInfo.FileName + " " + "version: " + pm.FileVersionInfo.FileVersion);
            }
            MMO.log("ShowAssemblyVersion", "==========================<Library>==========================");
        }

        public void inMaintenance()
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                wait(true);
            }));
            Thread.Sleep(100);
            CheckServer();
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                wait(false);
            }));
        }

        void notifyIcon_Click(object sender, EventArgs e)
        {
            show();
        }

        public string[] getNews(int gameid)
        {
            string[] dat = new string[2];
            try
            {
                //фикс значения
                //int _t = gameid + 1;
                ////
                //string data = tp.GetData("launcher/news/" + _t);
                //JObject Json = JObject.Parse(data);
                //string News = (string)Json["news"];
                //string Date = (string)Json["date"];
                //MMoNetwork_FileServices.WriteLog("MMo-Network::getNews()", 1);

                //dat[0] = Date;
                //dat[1] = News;
                return dat;
            }
            catch
            {
                MMO.log("getNews", " == null");
                return dat;
            }
        }

        public void hide()
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
                   {
                       this.WindowState = WindowState.Minimized;
                       try
                       {
                           notifyIcon.Visible = true;
                           this.Hide();
                           this.ShowInTaskbar = false;
                       }
                       catch
                       {
                           this.ShowInTaskbar = true;
                       }

                   }));
        }

        public void show()
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
                   {
                       this.Show();
                       this.ShowInTaskbar = true;
                       this.WindowState = WindowState.Normal;
                   }));
        }

        //Инструкции по обновлению игры
        public void setPath(string maindir, string web, string game)
        {
            MainDirSavePath = maindir + "\\";
            UPDATE_URL = "http://" + web + "/update/game/" + game + "/";

            //this.Invoke((MethodInvoker)delegate()
            //{
            //    progressBar_FULL.Visible = true;
            //    Status_update_localize.Visible = true;
            //    status_update.Visible = true;
            //});
            if (clientFiles.Count > 0)
            {
                clientFiles.Clear();
            }
        }

        private void Enter_Form_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        { }

        private void GameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { }

        public void wait(bool visible)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
                  {
                      if (visible == true)
                      {
                          background_loading.Visibility = Visibility.Visible;
                          Loading.Visibility = Visibility.Visible;
                      }
                      else
                      {
                          background_loading.Visibility = Visibility.Hidden;
                          Loading.Visibility = Visibility.Hidden;
                      }
                  }));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            logout();
        }

        /// <summary>
        /// Проверка работы сервера(профилактика).
        /// </summary>
        public void CheckServer()
        {
            MMO.log("CheckServer", ">>>");
            try
            {
                //string data = tp.GetData("launcher/inMaintenance");
                //JObject Json = JObject.Parse(data);
                //string Maintenance = (string)Json["Maintenance"];

                //if (Maintenance == "true")
                //{
                //    //Profilact.Visible = false;
                //}
                //else if (data == "false")
                //{
                //    //Profilact.Visible = true;
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            MMO.log("CheckServer", "<<<");
        }

        public void getversion()
        {
            //string data = tp.GetData("launcher/launcher_rev");
            //string vers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //JObject Json = JObject.Parse(data);
            //string server_version = (string)Json["version"];

            //if (server_version != vers)
            //{
            //    AutoUpdater au = new AutoUpdater();
            //    au.ShowDialog();
            //    Close();
            //}
        }

        /// <summary>
        /// Получение списка игр
        /// </summary>
        public void GameList(List<GamesInfo> gi)
        {
            MMO.log("GameList", ">>>");
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                for (int i = 0; i < gi.Count; i++)
                {
                    GameList_box.Items.Add(gi[i].name);
                    gsinfo_pub.Add(gi[i]);
                }
            }));
            MMO.log("GameList", "<<<");
        }

        /// <summary>
        /// Универсальный метод запуска....
        /// </summary>
        public void GameRun(string path, string arg, string name, string exe)
        {
            if (path != null && File.Exists(path + "\\" + exe + ".exe"))
            {
                try
                {
                    Dispatcher.BeginInvoke(new ThreadStart(delegate
                    {
                        SplashGame spl = new SplashGame(exe);
                        spl.ShowDialog();
                        minimize(name);
                        Process _process = new Process();
                        _process.StartInfo.FileName = path + "\\" + exe;
                        _process.StartInfo.Arguments = arg;
                        _process.StartInfo.UseShellExecute = true;
                        _process.StartInfo.WorkingDirectory = path;
                        _process.Start();
                    }));
                }
                catch (Exception eee)
                {
                    MMO.log("GameRun", ">>>>> FATAL ERROR in START GAME");
                    MMO.log("GameRun", ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    MMO.log("GameRun", eee.ToString());
                    MMO.log("GameRun", ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                }
            }
            else
            {
                MessageBox.Show("Cannot run game", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //public void ShieldScan(int id)
        //{
        //    tp.GetData("launcher/ShieldScan/" + id + "/" + AccountInfo.login);
        //}

        public void GameStart(int state)
        {
            switch (state)
            {

                case 0:
                    {
                        string path = null;
                        int gameindexNew = gameindex + 1;
                        path = register.loadReg(gamepath + gameindexNew.ToString());

                        string name = gsinfo_pub[gameindex].name;
                        string exe = gsinfo_pub[gameindex].exename;
                        int args = Convert.ToInt32(gsinfo_pub[gameindex].argument);

                        wait(false);

                        if (path == null)
                        {
                            MessageBox.Show("Path to the game cannot be found.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                            Dispatcher.BeginInvoke(new ThreadStart(delegate
                            {
                                StartGame.IsEnabled = true;
                            }));
                            return;
                        }

                        switch (args)
                        {
                            case 1024:
                                GameRun(path, "", name, exe);
                                break;

                            case 1025:
                                GameRun(path, AccountInfo.login + " " + AccountInfo.password, name, exe);
                                break;

                            default:
                                MessageBox.Show("Invalid Information on start");
                                Dispatcher.BeginInvoke(new ThreadStart(delegate
                                {
                                    StartGame.IsEnabled = true;
                                }));
                                break;
                        }
                        break;
                    }
                default:
                    wait(false);
                    MessageBox.Show("Произошла ошибка.\r\nПопробуйте еще раз.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Dispatcher.BeginInvoke(new ThreadStart(delegate
                    {
                        StartGame.IsEnabled = true;
                    }));
                    break;
            }

        }
        //Dispatcher.BeginInvoke(new ThreadStart(delegate
        //{
        //    Uri uri = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" + "Resources/maitenance.png", UriKind.Absolute);
        //    BitmapImage bitmap = new BitmapImage(uri);
        //    news_page.Source = bitmap;
        //}));

        //MessageBox.Show("An error has occurred.\r\nPlease try again ..", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

        /// <summary>
        /// Метод сворачивания программы.   
        /// </summary>
        public void minimize(string game)
        {
            notifyIcon.ShowBalloonTip(5000, "MMo-Network.", "Игра " + game + " запускается...", System.Windows.Forms.ToolTipIcon.Info);
            hide();
        }

        /// <summary>
        /// Прочие методы.
        /// </summary>
        private void trayicon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            //trayicon.Visible = false;
        }

        /// <summary>
        /// Загрузчик контрольной суммы бля...
        /// </summary>
        public void downloadHashes()
        {
            try
            {
                WebClient webClient = new WebClient();
                Uri ui = new Uri(UPDATE_URL + "hash.txt");
                webClient.DownloadFile(ui, MainDirSavePath + "hash.txt");
                lines = File.ReadAllLines(MainDirSavePath + "hash.txt");
                File.Delete(MainDirSavePath + "hash.txt");
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Could not to connect to update server", "Attention!");
            }
        }

        public void Update_now()
        {
            //update_thread_bool = true;
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
            //this.Invoke((MethodInvoker)delegate()
            //{
            //    progressBar_FULL.Maximum = maxFiles;
            //});

            foreach (ClientFile clientFile in clientFiles)
            {
                string path = (MainDirSavePath + clientFile.Name);
                bool exists = System.IO.File.Exists(path);

                if (!exists || isUpdateRequired(path, clientFile.Hash))
                {
                    downloadFile(clientFile.Name);
                    if (filesRemains == 0)
                    {
                        //update_thread_bool = false;
                    }
                }
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
                    string path = MMoNetwork_FileServices.delLastpattern(filePath, '\\');
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
                web.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                web.DownloadFileAsync(url, MainDirSavePath + fileName);
                //this.Invoke((MethodInvoker)delegate()
                //{ status_update.Text = fileName; });
            }
            catch { }
        }

        public bool isUpdateRequired(string path, string hashcode)
        {
            FileInfo file = new FileInfo(path);
            string hash = MMoNetwork_FileServices.GetHashString(file.Length.ToString());

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

            if (maxFiles < progress)
            {
                progress = maxFiles;
            }

            //this.Invoke((MethodInvoker)delegate()
            //{
            //progressBar_FULL.Value = progress;
            //});
            if (filesRemains == 0)
            {
                onUpdateEnd();
            }

        }

        public void onUpdateEnd()
        {
            StartGame.Visibility = Visibility.Visible;
            //update_thread_bool = false;
            setStatusUp("Update completed!"); ;
            //progressBar1.Value = 100;
        }


        public void setStatusUp(string status)
        {
            //this.Invoke((MethodInvoker)delegate()
            //{
            //this.Refresh();
            //status_update.Text = status;
            //this.Refresh();
            //});
        }

        private void LoginForm_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void MainForm_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void Main_form_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Check()
        {
            if (login_box.Text.Length > 3 && Password_box.Password.Length > 3)
            {
                login_box.IsEnabled = false;
                Password_box.IsEnabled = false;
                Enter_Form.IsEnabled = false;
                Login = login_box.Text;
                Password = Password_box.Password;

                string generateV = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
                generateV += Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
                generateV += Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
                generateV += Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
                int revision = Convert.ToInt32(generateV);

                wait(true);

                at.sendPacket(new opcode_502_CLIENT(login_box.Text, Password_box.Password));
            }
            else
            {
                System.Windows.MessageBox.Show("You did not enter your username or password.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void StatusLogin(long status)
        {
            switch (status)
            {

                case 0:
                    {
                        Dispatcher.BeginInvoke(new ThreadStart(delegate
                              {
                                  AccountInfo.login = login_box.Text;
                                  AccountInfo.password = Password_box.Password;
                                  wait(false);
                                  LoginForm.Visibility = Visibility.Hidden;
                                  login_box.Visibility = Visibility.Hidden;
                                  Password_box.Visibility = Visibility.Hidden;
                                  Enter_Form.Visibility = Visibility.Hidden;
                                  LoginForm.IsEnabled = false;
                                  login_box.IsEnabled = false;
                                  Password_box.IsEnabled = false;
                                  MainForm.Visibility = Visibility.Visible;
                                  username.Content = AccountInfo.login;
                              }));
                        at.sendPacket(new opcode_504_CLIENT());
                        break;
                    }

                case -2147483390:
                    {
                        Dispatcher.BeginInvoke(new ThreadStart(delegate
                        {
                            LoginForm.Visibility = Visibility.Visible;
                            login_box.Visibility = Visibility.Visible;
                            Password_box.Visibility = Visibility.Visible;
                            Enter_Form.Visibility = Visibility.Visible;
                            MessageBox.Show("Неверный логин или пароль.","Ошибка.",MessageBoxButton.OK,MessageBoxImage.Error);
                            login_box.IsEnabled = true;
                            Password_box.IsEnabled = true;
                            Enter_Form.IsEnabled = true;
                            LoginForm.IsEnabled = true;
                            wait(false);
                        }));
                        break;
                    }

                default:
                    MessageBox.Show("Status: " + status.ToString());
                    break;

            }
        }

        public void setNews(string news, string date)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                news_text.Content = news;
                date_text.Content = date;
            }));
        }

        private void Enter_Form_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Check();
        }

        private void Main_form_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }



        private void Settings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { }

        private void StartGame_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (gameindex >= 0)
            {
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    StartGame.IsEnabled = false;
                }));

                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    wait(true);
                }));

                Thread.Sleep(1000);

                at.sendPacket(new opcode_508_CLIENT(gameindex + 1, "noname"));
            }
            else
            {
                MessageBox.Show("The game is not selected.", "Error.");
            }
        }

        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (gameindex >= 0)
            {
                SetPathWindow wind = new SetPathWindow(gameindex);
                wind.ShowDialog();
            }
            else
            {
                MessageBox.Show("The game is not selected.", "Error.");
            }
        }

        private void GameList_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gameindex = GameList_box.SelectedIndex;
            if (gameindex > -1)
            {
                string Game = GameList_box.Items[gameindex].ToString();
                string img = AppDomain.CurrentDomain.BaseDirectory + "data\\content\\" + gsinfo_pub[gameindex].exename + ".jpg";
                FileInfo fi = new FileInfo(img);
                if (File.Exists(img) && fi.Length > 0)
                {
                    Uri uri = new Uri(img);
                    BitmapImage bitmap = new BitmapImage(uri);
                    news_page.Source = bitmap;
                }
                else
                {
                    MMO.log("GameList_box_SelectionChanged", ">>>>> Image not found " + gsinfo_pub[gameindex].exename);
                }
            }
            else
            {
                MMO.log("GameList_box_SelectionChanged", ">>>>> gameindex < 0");
            }

            //Thread thr = new Thread(ThreadNews);
            //thr.Name = "ThreadNews";
            //thr.Start();

            at.sendPacket(new opcode_506_CLIENT(gameindex));

        }

        //public void ThreadNews()
        //{
        //    Dispatcher.BeginInvoke(new ThreadStart(delegate
        //    {
        //        wait(true);
        //    }));
        //    //Получение новостей с сервера ммо нетворк.
        //    Thread.Sleep(200);
        //    Dispatcher.BeginInvoke(new ThreadStart(delegate
        //    {

        //        string[] data = getNews(gameindex);
        //        date_text.Content = data[0];
        //        news_text.Content = data[1];
        //        wait(false);
        //    }));
        //}

        private void StartGame_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void close_img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseApplication();
        }

        private void minimize_img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hide();
        }

        private void image2_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void Main_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Enter_Form.IsVisible == true)
            {
                Check();
            }
        }

        private void minimize_img_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        public void logout()
        {

            if (AccountInfo.login != null)
            {
                tp.GetData("launcher/logout/" + AccountInfo.login);
                AccountInfo.login = null;

                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    wait(true);
                    MMO.log("logout", "gsinfo_pub::Clear()");
                    gsinfo_pub.Clear();
                    MMO.log("logout", ">>>>> set CurrentToPosition = -1");
                    GameList_box.Items.MoveCurrentToPosition(-1);
                    MMO.log("logout", ">>>>> GameList_box.Items.Clear()");
                    GameList_box.Items.Clear();
                }));


                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    MMO.log("logout", ">>>>> logout NOW!");
                    wait(false);
                    LoginForm.Visibility = Visibility.Visible;
                    login_box.Visibility = Visibility.Visible;
                    Password_box.Visibility = Visibility.Visible;
                    Enter_Form.Visibility = Visibility.Visible;
                    LoginForm.IsEnabled = true;
                    login_box.IsEnabled = true;
                    Password_box.IsEnabled = true;
                    MainForm.Visibility = Visibility.Hidden;
                }));

            }
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Thread thl = new Thread(logout);
            thl.Name = "logout";
            thl.Start();
        }
    }
}
