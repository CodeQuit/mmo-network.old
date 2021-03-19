using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ShieldDLL
{
    public class Shield
    {
        Reader r = new Reader();
        public Shield()
        { }


        public int INIT()
        {
            return r.read();
        }
    }
}
