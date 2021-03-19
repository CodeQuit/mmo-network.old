using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMo_Network.network;
using System.Windows.Forms;

namespace MMo_Network.network.loginauth.recv
{
    class opcode_507_SERVER : ReceiveAuthPacket
    {
        //private DateTime date;
        private string Text;
        private int d, m, y;
        //private byte[] buff;

        public opcode_507_SERVER(AuthThread login, byte[] db)
        {
            base.makeme(login, db);
        }

        public override void read()
        {
            Text = readS(255); //Новость
            d = readC();
            m = readC();
            y = readH();
            //date = DateTime.Parse(date1);
        }

        public override void run()
        {
            //MessageBox.Show(Text);
            string date = d + "-" + m + "-" + y;
            FormManager.getForm().setNews(Text, date);
        }
    }
}
