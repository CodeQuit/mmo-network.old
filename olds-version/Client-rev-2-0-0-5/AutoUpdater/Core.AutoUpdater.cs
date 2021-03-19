//Copyright DarkTeam.zapto.org
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Text;

namespace AutoUpdater
{
    public class Core
    {
        // Класс-структура, который мы будем передавать как аргумент в событие,
        // которое вызывается после проверки версии сервера
        public class AutoUpdateEventArg
        {
            // Флаг отражающий успех или провал попытки получить версию сервера
            public Boolean UpdateSuccess { get; set; }
            // Флаг отражающий нужно ли нам обновление
            public Boolean NeedToUpdate { get; set; }

            public AutoUpdateEventArg(bool UpdateSuccess, bool NeedToUpdate)
            {
                this.UpdateSuccess = UpdateSuccess;
                this.NeedToUpdate = NeedToUpdate;
            }
        }

        // Создание нового делегата
        public delegate void AutoUpdateCheckDelegate(AutoUpdateEventArg arg);
        // Создание события на базе делегата
        public event AutoUpdateCheckDelegate CheckForUpdatesCompleted;
        // Метод вызова события внутри класса инициатора
        private void OnCheckForUpdatesCompleted(AutoUpdateEventArg arg)
        {
            if (CheckForUpdatesCompleted != null) CheckForUpdatesCompleted(arg);
        }

        // Объект класса настроек
        private Settings settings;
        // Объект класса WebClient
        private WebClient downloader;

        // Свойства
        public WebClient Downloader { get { return downloader; } }
        public String CurrentVersion { get; set; }
        public String ServerVersion { get; set; }

        public Core()
        {
            downloader = new WebClient();

            settings = new Settings();
            // Загружаем файл настроек
            settings.Load();

            // Устанавилваем начальные значения версий
            this.ServerVersion = "0";
            this.CurrentVersion = settings.Version;
        }

        // Метод, для проверки на сколько успешно загрузились настройки
        public bool CheckSettings()
        {
            // Если хотябы одно из 3х свойств настроек пустое
            if (String.IsNullOrEmpty(settings.LocalMainExe)             ||
                String.IsNullOrEmpty(settings.UpdateServerMainExeUrl)   ||
                String.IsNullOrEmpty(settings.UpdateServerVersionsUrl))
            {
                // возвращаем false;
                return false;
            }
            else
                return true;
        }

        // Метод для запуска главного файла (не автообновления)
        public void RunMainExe()
        {
            try
            {
                using (Process p = new Process() { StartInfo = new ProcessStartInfo(settings.LocalMainExe) })
                {
                    p.Start();
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch
            {
                Process p = new Process();
                p.StartInfo.FileName = @"MMO-NETWORK-Launcher.exe";
            //    p.Start();
                //Tools.InfoBowShow("Ошибка при инициализации приложения!");
                System.Windows.Forms.Application.Exit();
            }
        }

        // Метод проверки наличиия обновления на сервере
        public void CheckForUpdates()
        {

            createbat();
            //execute the batch file
            if (File.Exists("update.exe"))
            {
                File.Delete("update.exe");
            }
            OnCheckForUpdatesCompleted(new AutoUpdateEventArg(true, true));
            //Process u = Process.Start("update.bat");
            //Process.GetCurrentProcess().Kill();


            //// Запускаем наш поток, чтобы он выполнил код выше
            //checkerThread.Start();
        }

        public void textbat(string Textb)
        {
            //create a batch file
            StreamWriter sw; // объект потока для записи
            StringBuilder builder; // построитель строк
            builder = new StringBuilder();

            // создаем поток для записи - file.txt с добавлением в конец файла, кодировка UTF8
            using (sw = new StreamWriter(@"update.bat", true, Encoding.ASCII))
            {
                sw.WriteLine(Textb); // запись строки
                sw.Write(builder.ToString()); // запись сформированного списка строк
                // сбрасываем буфера и даем доступ к файлу
                sw.Close();
            }
        }

        public void createbat()
        {

            //Создание бат файла и заполнение его содержимым
            //если файл присутствует удаляем, создаем новый и заполняем его содержимым
                if (File.Exists("update.bat"))
                {
                    File.Delete("update.bat");
                    textbat("@echo off");
                    textbat("start MMO-NETWORK-Launcher.exe");
                    textbat("exit");
                }
                else
                {
                    textbat("@echo off");
                    textbat("start MMO-NETWORK-Launcher.exe");
                    textbat("exit");
                }
            
            
        }

        // Метод для скачивания обновленной версии файла с сервера
        public void DownloadUpdates()
        {
            // Запускаем асинхронное скачивание файла
            // Указываем URL - что качать и путь куда сохранить
            downloader.DownloadFileAsync(new Uri(settings.UpdateServerMainExeUrl), settings.LocalMainExe);
            // Добавляем обработчик события
            downloader.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(downloader_DownloadFileCompleted);
        }

        // Если скачивание обновленного файла прошло успешно, что сохраняем в настройки версию с сервера
        private void downloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            settings.Version = ServerVersion;
            settings.Save();
        }
    }
}
