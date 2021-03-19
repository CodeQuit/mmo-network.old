//Copyright DarkTeam.zapto.org
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Windows.Forms;
using log4net;

namespace MMO_NETWORK_LAUNCHER
{
    public class ShutdownHook
    {
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(ShutdownHook));
        /// <summary>
        /// Метод завершения программы.
        /// </summary>
        public static void Shutdown()
        {
            try
            {
                Process[] ps1 = System.Diagnostics.Process.GetProcessesByName("LauncherGame");
                foreach (Process p1 in ps1)
                {
                    //Console.WriteLine("Closing process...{0}", p1.ProcessName);
                    //ErrorShow.ErrorShow.EnterError("ErrorShutdown");
                    p1.Kill();
                }
            }
            catch
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Метод перезагрузки программы.
        /// </summary>
        public static void Restart(string argument)
        {
            //Process _LG = new Process();
            //_LG.StartInfo.FileName = "moduleCheck.exe";
            //_LG.StartInfo.Arguments = argument;
            //_LG.StartInfo.UseShellExecute = false;
            //_LG.Start();
            //Shutdown();
        }

    }
}
