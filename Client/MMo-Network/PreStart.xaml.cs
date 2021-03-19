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
using System.Timers;
using System.Reflection;
using System.Windows.Media.Animation;

namespace MMo_Network
{
    /// <summary>
    /// Логика взаимодействия для PreStart.xaml
    /// </summary>
    public partial class PreStart : Window
    {
        Timer win2 = new Timer();
        Timer win = new Timer();
        
        public PreStart()
        {
            InitializeComponent();
            //System.Threading.Thread.Sleep(5000);
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            version_text.Content = "Версия: " + version;
            win.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            win.Interval = 10;
            win2.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            win2.Interval = 7500;
            win2.Enabled = true;
        }

        public void OnTimedEvent2(object source, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new System.Threading.ThreadStart(delegate
            {
                this.Opacity = this.Opacity - 0.01;
                if (this.Opacity <= 0)
                {
                    win.Stop();
                    win.Enabled = false;
                    Close();
                }
            }));
        }
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new System.Threading.ThreadStart(delegate
            {
                win.Enabled = true;
                win2.Enabled = false;
            }));
        }
    }
}
