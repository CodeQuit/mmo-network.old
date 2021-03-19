//Copyright DarkTeam.zapto.org
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Globalization;
using log4net.Config;
using log4net;
using System.IO;
using MMO_NETWORK_LAUNCHER.src.services.test;

namespace MMO_NETWORK_LAUNCHER
{
    static class Program
    {
        private static Mutex _syncObject;
        private const string _syncObjectName = "{d1952c68-8cfc-4749-beec-d47a8e6cb414}";
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew;
            _syncObject = new Mutex(true, _syncObjectName, out createdNew);
            XmlConfigurator.Configure();

            if (!createdNew)
            {
                MessageBox.Show("Программа уже запущена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Loggers.Debug("======================================================");
            Loggers.Debug("Starting Launcher Game... Build " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            Loggers.Debug("======================================================");
            //SetupExceptionHandler();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LauncherGame());
            }
            catch (Exception)
            {

            }
        }
    }
}
