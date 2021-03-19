using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace MMO_NETWORK_LAUNCHER.src.windows
{
    public partial class UserInfo : Form
    {
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(UserInfo));
        public UserInfo(string username, string email, string age, string money)
        {
            InitializeComponent();
            if (username == "connection_has_not_passed" && email == "connection_has_not_passed" && age == "connection_has_not_passed")
            {
                UserName.Text = "Соединение не прошло.";
                UserName.ForeColor = System.Drawing.SystemColors.AppWorkspace;
                UserEmail.Text = "Соединение не прошло.";
                UserEmail.ForeColor = System.Drawing.SystemColors.AppWorkspace;
                UserAge.Text = "Соединение не прошло.";
                UserAge.ForeColor = System.Drawing.SystemColors.AppWorkspace;
                Money.Text = "Соединение не прошло.";
                Money.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            }
            else
            {
                UserName.Text = username;
                UserEmail.Text = email;
                UserAge.Text = age;
                Money.Text = money;
                Money.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            }
        }

        private void CloseUserInfo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
