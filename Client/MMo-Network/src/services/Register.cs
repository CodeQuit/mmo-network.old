using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace MMo_Network.src.services
{
    public class Register
    {
        RegistryKey key;
        public void createReg(string name, string value)
        {
            try
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("MMo-Network");
                key.SetValue(name, value);
                key.Close();
            }
            catch
            {

            }
        }

        public string loadReg(string name)
        {
            try
            {
                object value = Registry.CurrentUser.OpenSubKey("MMo-Network").GetValue(name);
                string val = value.ToString();
                return val;
            }
            catch
            {
                return null;
            }
        }
    }
}
