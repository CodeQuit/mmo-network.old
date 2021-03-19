using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMo_Network.network;
using MMo_Network.network.loginauth;
using MMo_Network.src.models;

namespace MMo_Network.network.loginauth.recv
{
    class opcode_505_SERVER : ReceiveAuthPacket
    {
        private int game_count;
        private List<GamesInfo> gi = new List<GamesInfo>();
        private GamesInfo gs;
        public opcode_505_SERVER(AuthThread login, byte[] db)
        {
            base.makeme(login, db);
        }

        public override void read()
        {
            game_count = readD();
            //for (int i = 0; i < game_count; i++)
            //{
                gs = new GamesInfo();
                readC();
                int ii = readC();
                int iii = readC();
                gs.exename = readS(ii);
                gs.name = readS(iii);
                gs.path_to_img = gs.exename;
                gi.Add(gs);
            //}
        }

        public override void run()
        {
            //MessageBox.Show("RUN!");
            FormManager.getForm().GameList(gi);
        }
    }
}
