//Copyright DarkTeam.zapto.org
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace AutoUpdater
{
    public partial class Main : Form
    {
        private Core core;

        public Main()
        {
            InitializeComponent();

            core = new Core();
            core.CheckForUpdatesCompleted += new Core.AutoUpdateCheckDelegate(core_CheckForUpdatesCompleted);
            core.Downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Downloader_DownloadProgressChanged);
            core.Downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Downloader_DownloadFileCompleted);

            this.Load += new EventHandler(Main_Load);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (core.CheckSettings())
            {
                Thread.Sleep(2000);
                this.lStatus.Text = "Проверка наличия обновлений...";
                this.progressBar1.Style = ProgressBarStyle.Marquee;
                this.core.CheckForUpdates();
            }
            else
            {
                this.Opacity = 0;
                Tools.InfoBowShow("Не правильно настроен конфигурационный файл!");
                Application.Exit();
            }
        }

        private void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            core.RunMainExe();
        }

        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke(new EventHandler(delegate
            {
                lStatus.Text = String.Format("Загружено: {0} Кбайт / {1} Кбайт", e.BytesReceived / 1024, e.TotalBytesToReceive / 1024);
                progressBar1.Value = e.ProgressPercentage;
            }));
        }

        private void core_CheckForUpdatesCompleted(Core.AutoUpdateEventArg arg)
        {
            this.Invoke(new EventHandler(delegate
            {
                this.progressBar1.Style = ProgressBarStyle.Blocks;
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
                    MessageBox.Show(ex.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    core.RunMainExe();
                }
            }
            else
            {
                MessageBox.Show("Не удалось проверить доступность новой версии!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                core.RunMainExe();
            }
        }

        private void Main_Load_1(object sender, EventArgs e)
        {

        }
    }
}
