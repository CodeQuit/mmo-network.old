using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// Класс записи логов
    /// </summary>
    public class Logger
    {
        public static Type SuperClass;
        /// <summary>
        /// Запись логов, конструктор класса
        /// </summary>
        /// <param name="classnamme">Имя класса</param>
        public Logger(Type Class)
        {
            SuperClass = Class;
        }

        public void log(string method, string strings)
        {
            FileStream fs = new FileStream("MMO.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString() + " " + SuperClass.Name + "::" + method + " " + strings);
            sw.Close();
            fs.Dispose();
        }
    }
}
