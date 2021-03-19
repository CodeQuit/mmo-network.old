//Copyright DarkTeam.zapto.org
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using log4net;

namespace MMO_NETWORK_LAUNCHER.src.service.Debugger
{
    /// <summary>
    /// Logs errors to a window shown on screen
    /// </summary>
    public static class Debugger
    {
        private static readonly ILog Loggers = LogManager.GetLogger(typeof(Debugger));
        /// <summary>
        /// Logs the specified error.
        /// </summary>
        /// <param name="error">The error to log.</param>
        public static void LogError(string error)
        {

            Form errorForm = new Form();

            errorForm.Width = 651;
            errorForm.Height = 537;
            errorForm.StartPosition = FormStartPosition.Manual;
            errorForm.TopLevel = true;
            errorForm.TopMost = true;
            errorForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            errorForm.Text = "An error has occured:";

            RichTextBox errorBox = new RichTextBox();
            errorForm.Controls.Add(errorBox);

            errorBox.Top = 12;
            errorBox.Left = 19;
            errorBox.Width = 532;
            errorBox.Height = 185;

            //errorBox.Text = error2;

            errorBox.Font = new System.Drawing.Font("Courier New", 10);
            errorBox.ReadOnly = true;
            errorBox.WordWrap = false;
            errorBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            //errorBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            errorBox.AppendText(error);

            RichTextBox SystemBox = new RichTextBox();
            errorForm.Controls.Add(SystemBox);

            SystemBox.Top = 200;
            SystemBox.Left = 19;
            SystemBox.Width = 532;
            SystemBox.Height = 283;

            SystemBox.AppendText("Application:       " + Application.ProductName + "\n");
            SystemBox.AppendText("Version:           " + Application.ProductVersion + "\n");
            SystemBox.AppendText("Date:              " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n");
            SystemBox.AppendText("Computer name:     " + SystemInformation.ComputerName + "\n");
            SystemBox.AppendText("User name:         " + SystemInformation.UserName + "\n");
            SystemBox.AppendText("OS:                " + Environment.OSVersion.ToString() + "\n");
            SystemBox.AppendText("Culture:           " + CultureInfo.CurrentCulture.Name + "\n");
            SystemBox.AppendText("Resolution:        " + SystemInformation.PrimaryMonitorSize.ToString() + "\n");

            Button errorOk = new Button();
            errorForm.Controls.Add(errorOk);
            errorOk.Top = errorForm.ClientRectangle.Height - 25;
            errorOk.Left = errorForm.ClientRectangle.Width - 5 - errorOk.Width;
            errorOk.Text = "&OK";
            errorOk.DialogResult = DialogResult.Cancel;
            errorForm.CancelButton = errorOk;
            errorForm.AcceptButton = errorOk;

            Button errorSave = new Button();
            errorForm.Controls.Add(errorSave);

            errorSave.Top = errorForm.ClientRectangle.Height - 50;
            errorSave.Left = errorForm.ClientRectangle.Width - 5 - errorSave.Width;
            errorSave.Text = "&Save";

            errorForm.ShowDialog();
        }
    }
}

   