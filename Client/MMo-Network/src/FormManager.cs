using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MMo_Network.network;

namespace MMo_Network
{
    class FormManager
    {
        private static MainWindow mw;
        private static FormManager instance;

        public static FormManager getInstance()
        {
            if (instance == null)
                instance = new FormManager();
            return instance;
        }

        public FormManager()
        {
            mw = new MainWindow();
        }

        public static MainWindow getForm()
        {
            return mw;
        }
    }
}
