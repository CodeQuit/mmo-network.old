using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMo_Network.src.models
{
    class ClientFile
    {
        public String Name, Hash;

        public ClientFile(string name, string hash)
        {
            this.Name = name;
            this.Hash = hash;
        }
    }
}
