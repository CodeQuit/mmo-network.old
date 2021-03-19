//Copyright DarkTeam.zapto.org
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;

namespace RusLang {
	
	public class ResourceManager {

        private static readonly ILog Loggers = LogManager.GetLogger(typeof(ResourceManager));
		private System.Resources.ResourceManager _res_mgr;

		public ResourceManager(string locale) {
			_res_mgr = new System.Resources.ResourceManager(locale + ".res", Assembly.LoadFrom(locale + ".dll"));
		}

        public string localization_lang_01
        {
			get {
                return _res_mgr.GetString("localization_lang_01");
			}
		}

        public string localization_lang_02 
        {
			get {
                return _res_mgr.GetString("localization_lang_02");
			}
		}

        public string localization_lang_03
        {
            get
            {
                return _res_mgr.GetString("localization_lang_03");
            }
        }

        public string localization_lang_04
        {
            get
            {
                return _res_mgr.GetString("localization_lang_04");
            }
        }

        public string localization_lang_22
        {
            get
            {
                return _res_mgr.GetString("localization_lang_22");
            }
        }

        public string localization_lang_24
        {
            get
            {
                return _res_mgr.GetString("localization_lang_24");
            }
        }
        public string localization_lang_25
        {
            get
            {
                return _res_mgr.GetString("localization_lang_25");
            }
        }
        public string localization_lang_18
        {
            get
            {
                return _res_mgr.GetString("localization_lang_18");
            }
        }
        public string VersionCheck
        {
            get
            {
                return _res_mgr.GetString("VersionCheck");
            }
        }
        public string localization_lang_05
        {
            get
            {
                return _res_mgr.GetString("localization_lang_05");
            }
        }
        public string localization_lang_11
        {
            get
            {
                return _res_mgr.GetString("localization_lang_11");
            }
        }

        public string localization_lang_21
        {
            get
            {
                return _res_mgr.GetString("localization_lang_21");
            }
        }

        public string localization_lang_26
        {
            get
            {
                return _res_mgr.GetString("localization_lang_26");
            }
        }
        public string localization_lang_12
        {
            get
            {
                return _res_mgr.GetString("localization_lang_12");
            }
        }
        public string linkAbout
        {
            get
            {
                return _res_mgr.GetString("linkAbout");
            }
        }
        public string UserNameTEXT
        {
            get
            {
                return _res_mgr.GetString("UserNameTEXT");
            }
        }
        public string localization_lang_23
        {
            get
            {
                return _res_mgr.GetString("localization_lang_23");
            }
        }
        public string localization_lang_06
        {
            get
            {
                return _res_mgr.GetString("localization_lang_06");
            }
        }
	}
}
