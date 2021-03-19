//Copyright DarkTeam.zapto.org
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Net.Sockets;
using System.Net;
using log4net;
using System.Reflection;
using MMO_NETWORK_LAUNCHER.src.windows;

namespace MMO_NETWORK_LAUNCHER
{
    public partial class PreLoading : Form
    {
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(PreLoading));
        System.Timers.Timer tmr2 = new System.Timers.Timer();
        public PreLoading()
        {
            InitializeComponent();
            version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            tmr2.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            tmr2.Interval = 7000;
            tmr2.Enabled = true;
            //BackColor = Color.Lime;
            //TransparencyKey = Color.Lime;
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                this.tmr2.Stop();
                this.tmr2.Close();
                this.Close();
            });
        }
    }
}
