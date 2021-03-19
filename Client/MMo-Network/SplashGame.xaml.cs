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

namespace MMo_Network
{
    /// <summary>
    /// Логика взаимодействия для SplashGame.xaml
    /// </summary>
    public partial class SplashGame : Window
    {
        string imgpath = null;
        Timer win2 = new Timer();
        Timer win = new Timer();

        public SplashGame(string image_path)
        {
            InitializeComponent();
            imgpath = image_path;

            //Image I = new Image();
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory + "data\\content\\splash\\" + imgpath + ".jpg");
            //Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "data\\content\\splash\\" + imgpath + ".jpg");
            //BitmapImage bitmap = new BitmapImage(uri);
            //ImageGame.Source = bitmap;
            win.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            win.Interval = 10;
            win2.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            win2.Interval = 5500;
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
