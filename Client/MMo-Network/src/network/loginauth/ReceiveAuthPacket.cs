using System;
using MMo_Network.network.loginauth;
using System.Text;

namespace MMo_Network.network
{
    public abstract class ReceiveAuthPacket
    {
        private byte[] _packet;
        private int _offset;
        public AuthThread login;
        public void makeme(AuthThread login, byte[] packet)
        {
            this.login = login;
            _packet = packet;
            _offset = 2;
            read();
        }

        public byte readC()
        {
            byte result = _packet[_offset];
            _offset += 1;
            return result;
        }

        public short readH()
        {
            short result = BitConverter.ToInt16(_packet, _offset);
            _offset += 2;
            return result;
        }

        public int readD()
        {
            int result = BitConverter.ToInt32(_packet, _offset);
            _offset += 4;
            return result;
        }

        public long readQ()
        {
            long result = BitConverter.ToInt64(_packet, _offset);
            _offset += 8;
            return result;
        }

        public byte[] readB(int Length)
        {
            byte[] result = new byte[Length];
            Array.Copy(_packet, _offset, result, 0, Length);
            _offset += Length;
            return result;
        }

        public double readF()
        {
            double result = BitConverter.ToDouble(_packet, _offset);
            _offset += 8;
            return result;
        }

        public string readS(int len)
        {
            string result = "";
            try
            {
                //encoding.GetEncoding(1251).GetString(_buffer, _offset, Length);
                result = Encoding.GetEncoding(1251).GetString(_packet, _offset, len);
                int idx = result.IndexOf((char)0x00);
                if (idx != -1)
                {
                    result = result.Substring(0, idx);
                }
                _offset += len;
            }
            catch (Exception ex)
            {
                Console.WriteLine("while reading string from packet, " + ex.Message + " " + ex.StackTrace);
            }
            return result;
        }

        public abstract void read();
        public abstract void run();

    }
}
