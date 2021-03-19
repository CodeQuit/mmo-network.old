using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Threading;
using System.IO;
using NBug.Enums;

namespace MMo_Network
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _syncObject;
        private const string _syncObjectName = "{C9CCCA4A-9451-4946-A231-B6C722BC7735}";
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            FormManager.getInstance();

            bool createdNew;
            _syncObject = new Mutex(true, _syncObjectName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Программа уже запущена.", "Ошибка");
                Application.Current.Shutdown(0);
            }

            //AppDomain.CurrentDomain.UnhandledException += NBug.Handler.UnhandledException;
            //Application.Current.DispatcherUnhandledException += NBug.Handler.DispatcherUnhandledException;
            //NBug.Settings.Destination1 = "Type=Mail;From=bug@pbdev.ru;To=tracker@pbdev.ru;Smtp=smtp.pbdev.ru;";
            //NBug.Settings.UIMode = UIMode.Full;
            //NBug.Settings.WriteLogToDisk = true;
        }
    }
}
