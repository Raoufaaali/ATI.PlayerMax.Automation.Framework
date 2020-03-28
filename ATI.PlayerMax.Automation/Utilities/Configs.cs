using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATI.PlayerMax.Automation.Utilities
{
    sealed class Configs
    {
        public static string MAM_BROWSER = ConfigurationManager.AppSettings["MAM_BROWSER"];
        public static string MAM_MODE = ConfigurationManager.AppSettings["MAM_MODE"];

        public static string MOBILE_DEVICE = ConfigurationManager.AppSettings["MOBILE_DEVICE"];
        public static string MOBILE_MODE = ConfigurationManager.AppSettings["MOBILE_MODE"];

        public static string PERFECTO_SECURITYTOKEN = ConfigurationManager.AppSettings["PERFECTO_SECURITYTOKEN"];
        public static string PERFECTO_HOST = ConfigurationManager.AppSettings["PERFECTO_HOST"]; 

        /*
        public static string PERFECTO_SECURITYTOKEN = GetSensitiveConfig("PERFECTO_SECURITYTOKEN");
        public static string PERFECTO_HOST = GetSensitiveConfig("PERFECTO_HOST");
        public static string PERFECTO_DEVICELIST = ConfigurationManager.AppSettings["PERFECTO_DEVICELIST"];
        */
        public static string MAM_URL = ConfigurationManager.AppSettings["MAM_URL"];
        public static string ANDROID_APK = ConfigurationManager.AppSettings["ANDROID_APK"];
        public static string IOS_IPA = ConfigurationManager.AppSettings["IOS_IPA"];

        public static string SAUCELABS_REMOTESERVER = ConfigurationManager.AppSettings["SAUCELABS_REMOTESERVER"];
        public static string SAUCELABS_USERNAME = ConfigurationManager.AppSettings["SAUCELABS_USERNAME"];
        public static string SAUCELABS_ACCESSKEY = ConfigurationManager.AppSettings["SAUCELABS_ACCESSKEY"];

        private Configs()
        {
        }

        private static string GetSensitiveConfig(string configKey)
        {
            //Try to read the configs from the environmnet variable, if null, read from the App.config
            string configValue = "";
            configValue = Environment.GetEnvironmentVariable(configKey, EnvironmentVariableTarget.Machine);
            //Perfeco reporting if perfeco is used
            if (configValue == null)
            {
                configValue = ConfigurationManager.AppSettings[configKey];
            }      
    
            return configValue;
        }



    }
}
