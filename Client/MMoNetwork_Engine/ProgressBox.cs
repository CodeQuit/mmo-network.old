using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMoNetworkEngine
{
    /// <summary>
    /// Кастомный прогресс бар (Класс в процессе разработки)
    /// </summary>
    public partial class ProgressBox : UserControl
    {
        public ProgressBox()
        {
            InitializeComponent();
        }

        public void value(int percent)
        {
            bar.Size = new System.Drawing.Size(percent, 20);
        }
    }
}
