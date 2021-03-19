using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMo_Network.network;
using System.Windows.Threading;
using System.Threading;

namespace MMo_Network.network.loginauth.recv
{
    class opcode_500_SERVER : ReceiveAuthPacket
    {
        private int hash = 0;
        private int id = 0;
        private int key = 0;

        public opcode_500_SERVER(AuthThread login, byte[] db)
        {
            base.makeme(login, db);
            
        }
        public override void read()
        {
            id = readD();
            key = readH();
            hash = readH();
        }

        public override void run()
        {
            FormManager.getForm().setNetworkInfo(id, key, hash);
        }
    }
}
