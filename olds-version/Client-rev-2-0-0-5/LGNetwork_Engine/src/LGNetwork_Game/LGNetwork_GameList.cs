using System;
using System.Collections.Generic;
using System.Text;

namespace LGNetworkEngine.src
{
    class LGNetwork_GameList
    {
        /// <summary>
        /// Uncompleted function
        /// </summary>
        public string[] _stage_pre;
        public string[] stage_1;

        public string[] stage_2;


        LGNetwork_FileServices LGF = new LGNetwork_FileServices();
        public void reader(string filename)
        {
            _stage_pre = LGF.ListReader("game_list.ini");
            for (int i = 0; i < 10; i++)
            {
                stage_1 = _stage_pre[i].Split(new char[] { '=' });
            }
        }
    }
}
