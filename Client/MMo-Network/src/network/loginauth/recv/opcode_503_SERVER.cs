using MMo_Network.network.loginauth.send;
using System;
using System.Windows.Forms;
using MMo_Network.src.models;

namespace MMo_Network.network.loginauth.recv
{
    class opcode_503_SERVER : ReceiveAuthPacket
    {
        private long login_status = 0;
        
        public opcode_503_SERVER(AuthThread login, byte[] db)
        {
            base.makeme(login, db);
        }

        public override void read()
        {
            login_status = readQ();
        }

        public override void run()
        {
            FormManager.getForm().StatusLogin(login_status);
        }
    }
}
