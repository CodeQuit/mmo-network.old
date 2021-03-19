using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using LGNetworkEngine.src;

namespace LGNetwork
{
   public class LGNetwork_News
    {
       private static readonly ILog Loggers = LogManager.GetLogger(typeof(LGNetwork_News));
        LGNetwork_FileServices LGF = new LGNetwork_FileServices();
        public string GetNews()
        {
            Loggers.Debug("LGNetwork_FileServices::GetNews() >>> ");
            string[] stage_1;
            string[] stage_2;

            string news_string;
            stage_1 = LGF.ListReader("news.lg");

            stage_2 = stage_1[0].Split(new char[] { '=' });
            if (stage_2[0] == "news")
            {
                news_string = stage_2[1];
                Loggers.Debug("LGNetwork_News::GetNews() <<< ");
                return news_string;
            }
            else
            {
                Loggers.Debug("LGNetwork_News::GetNews() <<< ");
                return news_string = null;
            }
        }
    }
}
