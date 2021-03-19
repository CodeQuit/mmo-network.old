using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace EncodeDecodeConfiguration
{
    public partial class MainForm : Form
    {
        string temp;
        //string _temp;
        static string ver = null;
        string filename = null;

        RSACryptoServiceProvider rsa;

        string[] data = new string[5];

        public MainForm()
        {
            InitializeComponent();
        }

        private void Encode_Click(object sender, EventArgs e)
        {
            string pre_stage = textBox.Text;
            string _stage_01, _stage_02, _stage_03;
            string final_data;

            _stage_01 = base64Encode(pre_stage);
            _stage_02 = base64Encode(_stage_01);
            _stage_03 = base64Encode(_stage_02);

            final_data = _stage_03;

            textBox.Text = final_data;
        }

        private void Decode_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Начало дешифровки..." + "\r\n");
            temp = textBox.Text;
            string pre_stage = temp;

            string _stage_01, _stage_02, _stage_03;

            string final_data;

            _stage_03 = base64Decode(pre_stage);
            _stage_02 = base64Decode(_stage_03);
            _stage_01 = base64Decode(_stage_02);

            final_data = _stage_01;

            textBox.Text = final_data;
        }

        private void ClickDialogOK(object sender, CancelEventArgs e)
        {

            //string pre_stage = textBox.Text;
            string _stage_01, _stage_02, _stage_03;
            //string final_data;

            int line = 0;

            //Инициализируем переменную в которую запишем имя файла.
            filename = openFileDialog.FileName;

            //Определим длину файла.
            //Ининциализируем переменную StreamReader с названием file_len.
            StreamReader file_len = new StreamReader(filename);
            //Прочитаем длину и запишем в текстовую строку int_1.
            int_1.Text = file_len.ReadToEnd().Length.ToString();
            //Закроем файл, иначе не сможем повторно его прочитать и записать...
            file_len.Close();

            //Повторно инициализируем StreamReader с названием file.
            StreamReader file = new StreamReader(filename);
            //Напишем текущую операцию...
            textBox1.AppendText("Чтение файла...." + "\r\n");

            //Начинаем циклично читать файл пока не закончатся строки...
            while ((temp = file.ReadLine()) != null)
            {
                
                //Проверим переменную ver, если она равно null попробуем прочитать версию из файла
                if (ver == null)
                {
                    //Пишем текущую операцию.
                    textBox1.AppendText("Анализируем строку" + "\r\n");
                    //Расшифровываем считанную строку.

                    _stage_01 = base64Decode(temp);
                    _stage_02 = base64Decode(_stage_01);
                    _stage_03 = base64Decode(_stage_02);

                    ver = _stage_03;
                    //Проверяем поддерживается ли данная версия....
                    //Если не поддерживается - отменяем, если поддерживается продолжаем.
                    if (ver != "rev1.0.1.2")
                    {
                        textBox1.AppendText("Версия не поддерживается... " + ver + " \r\n");
                        break;
                    }
                    else
                    {
                        int_2.Text = ver;
                        continue;
                    }
                }

                //Пишем текущую операцию.
                textBox1.AppendText("Строка " + temp + " дешифруем..." + "\r\n");
                //Версия поддерживается, начинаем расшивровывать...

                _stage_01 = base64Decode(temp);
                _stage_02 = base64Decode(_stage_01);
                _stage_03 = base64Decode(_stage_02);


                //test
                data[line] = _stage_03;

                //Пишем текущую операцию.
                textBox1.AppendText("Дешифрованная строка " + _stage_03 + "\r\n");
                //Записываем результат дешифровки.
                textBox.AppendText(_stage_03 + "\r\n");
                line++;
            }
        }


        public string[] GetDecodeText(string strings)
        {
            char[] version = new char[1];

            version[0] = '\x0058';

            string hash = version.GetHashCode().ToString();

            string _stage_01, _stage_02, _stage_03;
            textBox1.AppendText("Строка " + strings + " дешифруем..." + "\r\n");
            _stage_01 = base64Decode(strings);
            textBox1.AppendText("Строка " + _stage_01 + " дешифруем..." + "\r\n");
            _stage_02 = base64Decode(_stage_01);
            textBox1.AppendText("Строка " + _stage_02 + " дешифруем..." + "\r\n");
            _stage_03 = base64Decode(_stage_02);

            string final_stage = _stage_03;
            string[] data = final_stage.Split(new char[]{ '$' });
            MessageBox.Show("data = " + data.Length);
            return data;
        }

        /// <summary>
        /// Загрузчик файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFile_Click(object sender, EventArgs e)
        {
            //Перед открытием файла очистим место под текст.
            textBox.Clear();

            //Очистим переменную с версией, что бы не было проблем при открытии файлов с другой версией.
            int_2.Text = "";
            ver = null;

            //Откроем диалог в котором предлагается выбрать файл.
            openFileDialog.ShowDialog();
        }

        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode " + e.Message);
            }
        }

        public string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode " + e.Message);
            }
        }

        public string encoded3XLG(string str)
        {
            string _stage_01 = base64Decode(str);
            string _stage_02 = base64Decode(_stage_01);
            string _stage_03 = base64Decode(_stage_02);
            return _stage_03;
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            

            StreamWriter st = new StreamWriter(filename + ".dec");
            st.WriteLine(encoded3XLG("rev1.0.1.2"));
            for (int i = 0; i < data.Length; i++)
            {
                if (data[0 + i] != null || data[0 + i] != "")
                {
                    st.WriteLine(encoded3XLG(data[0 + i]));
                    textBox1.AppendText(data[0 + i] + "\r\n");
                }
                //if (data.Length == i)
                //{
                //    st.WriteLine("*/*");
                //}
            }
            st.Close();
        }

        public string DecryptData(string data2Decrypt, string version)
        {
            string _stage_final;

            string[] _t = data2Decrypt.Split(new char[] { '$' });
            if (version == _t[1])
            {
                //textBox1.AppendText("Experiment function: " + _t[0] + " \r\n");
                //textBox1.AppendText("Experiment function: " + _t[1] + " \r\n");
                //textBox1.AppendText("Experiment function: " + _t[2] + " \r\n");

                _stage_final = base64Decode(_t[2]);

                return _stage_final;
            }
            else
                return null;

        }

        public string EncryptData(string data2Encrypt, string version)
        {
            string data_r = version + base64Encode(data2Encrypt);
            return data_r;
        }

        private void NewDecript_Click(object sender, EventArgs e)
        {
            string _test = DecryptData(textBox.Text, "rev1.0.1.5");//GetDecodeText(textBox.Text);
            string _stage_02_final = "";
            try
            {
                string[] _stage_01;
                if (_test.Length > 0)
                {
                    textBox.Text = "";

                    for (int it = 0; it <= _test.Length; it++)
                    {
                        _stage_01 = _test.Split(new char[] { '$' });
                        try
                        {
                            if (_stage_01[it] != "")
                            {
                                _stage_02_final = _stage_02_final + _stage_01[it] + "$";

                                string[] _stage_03_final = _stage_02_final.Split(new char[] { '=' });
                            }
                        }
                        catch
                        { }
                    }
                    textBox.AppendText(_stage_02_final);
                    textBox1.AppendText(_stage_02_final);
                }
            }
            catch (Exception n)
            {
                textBox1.AppendText("Ex: " + n.ToString());
            }
        }

        private void NewEncript_Click(object sender, EventArgs e)
        {
            string enc = EncryptData(textBox.Text, "LauncherGameCript$rev1.0.1.5$");
            try
            {
                if (enc != null)
                {
                    textBox1.AppendText(enc);
                }
            }
            catch { }

        }

        //LauncherGameCript$rev1.0.1.5$
        //VnpOT2JHTnVXbXhqYkRCclJGRndRbVJZVW05UVYxSm9ZMjEwTUZwWFJuUk1ibkJvWTBoU2RreHRPWGxhZVZGT1EyNWtiRmxxTVd0WldFcHlaRWRXYUdKVE5UWlpXRUl3WW5rMWRtTnRZMnM9

        //VnpOT2JHTnVXbXhqYkRCclJGRndRbVJZVW05UVYxSm9ZMjEwTUZwWFJuUk1ibkJvWTBoU2RreHRPWGxhZVZGT1EyNWtiRmxxTVd0WldFcHlaRWRXYUdKVE5UWlpXRUl3WW5rMWRtTnRZMnM9
    }
}
