using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMo_Network.network;

namespace MMo_Network.network.loginauth.recv
{
    class opcode_509_SERVER : ReceiveAuthPacket
    {
        public opcode_509_SERVER(AuthThread login, byte[] db)
        {
            base.makeme(login, db);
        }

        private int STATE_GAME = 0;
        public override void read()
        {
            STATE_GAME = readC();
        }

        public override void run()
        {
            FormManager.getForm().GameStart(STATE_GAME - 1);
        }
    }
}
