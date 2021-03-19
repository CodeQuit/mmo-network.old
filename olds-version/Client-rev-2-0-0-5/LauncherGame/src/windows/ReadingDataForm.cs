using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace MMO_NETWORK_LAUNCHER.src.windows
{
    public partial class ReadingDataForm : Form
    {
        public ReadingDataForm()
        {
            InitializeComponent();
            System.Timers.Timer tmr2 = new System.Timers.Timer();
            tmr2.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            tmr2.Interval = 2500; //Устанавливаем интервал в 5 сек.
            tmr2.Enabled = true; //Вкючаем таймер.
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                this.Close();
            });
        }
    }
}
