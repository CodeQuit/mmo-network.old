using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MMo_Network.src;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;


namespace MMo_Network
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AutoUpdater : Window
    {
        private Core core;
        public AutoUpdater()
        {
            InitializeComponent();
            core = new Core();
            core.CheckForUpdatesCompleted += new Core.AutoUpdateCheckDelegate(core_CheckForUpdatesCompleted);
            core.Downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Downloader_DownloadProgressChanged);
            core.Downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Downloader_DownloadFileCompleted);
            core.CheckForUpdates();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            if (core.CheckSettings())
            {
                this.core.CheckForUpdates();
            }
            else
            {
                this.Opacity = 0;
                System.Windows.Forms.MessageBox.Show("Не правильно настроен конфигурационный файл!");
                Close();
            }
        }

        private void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            core.RunMainExe();
            Close();
        }

        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                //lStatus.Text = String.Format("Загружено: {0} Кбайт / {1} Кбайт", e.BytesReceived / 1024, e.TotalBytesToReceive / 1024);
                ProgressUpdate.Value = e.ProgressPercentage;
            }));
        }

        private void core_CheckForUpdatesCompleted(Core.AutoUpdateEventArg arg)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                
            }));

            if (arg.UpdateSuccess)
            {
                try
                {
                    if (arg.NeedToUpdate)
                        core.DownloadUpdates();
                    else
                        core.RunMainExe();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Информация");
                    core.RunMainExe();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Не удалось проверить доступность новой версии!", "Информация");
                core.RunMainExe();
            }
        }

        private void ProgressUpdate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
