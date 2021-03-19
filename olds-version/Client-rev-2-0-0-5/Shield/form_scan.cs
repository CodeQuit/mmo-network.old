using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Shield
{
    public partial class form_scan : Form
    {
        public List<string> cl = new List<string>();

        public string[] active_ch;
        public form_scan()
        {
            InitializeComponent();
            cl.Add("CCleaner");
            cl.Add("wh");
            cl.Add("ArtMoney");
            this.Refresh();
            Thread th = new Thread(scan);
            th.Start();
        }

        public void scan()
        {
            int cheat = 0;
            //pc.ProcessName = "testc";
            Status_lang.Text = "Please wait. >>>";
            foreach(string i in cl)
            {
                Thread.Sleep(1000);
                if (forProcess(i))
                {
                    active_ch[cheat] = i;
                    cheat++;
                }
                
            }
            Status_lang.Text = "Game called!";
            Thread.Sleep(500);
            Close();
            //Hide();
        }

        public bool forProcess(string name)
        {
            Process pc = new Process();
            try
            {
                if (pc.ProcessName == name)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private void form_scan_Load(object sender, EventArgs e)
        {

        }
    }
}
