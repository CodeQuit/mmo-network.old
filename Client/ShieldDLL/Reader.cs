using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;

namespace ShieldDLL
{
    class Reader
    {
        int cheat = 255;
        public string[] active_ch = null;
        public int read()
        {
           StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\Data\\BlackList.mhi");
           string line;
            while (!sr.EndOfStream)
            {
                line=sr.ReadLine();
                String[] filedata = line.Split(new char[]{';'});
                if (forProcess(filedata[0]))
                {
                    if (filedata[1] == "P100020!")
                    {
                        active_ch[cheat] = line;
                        cheat = 100020;
                        Thread.Sleep(200);
                    }
                }
            }
            sr.Close();
            return cheat;
        }

        public string CRC(string file)
        {
            MD5 myHash = new MD5CryptoServiceProvider();
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            myHash.ComputeHash(br.ReadBytes((int)fs.Length));
            return Convert.ToBase64String(myHash.Hash);
        }

        public bool forProcess(string name)
        {
            Process pc = new Process();
            try
            {
                if (pc.ProcessName == name)
                {
                    //string sss = pc.MainWindowTitle;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
