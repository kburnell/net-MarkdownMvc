using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Demo.Utilities {

    public class ConfigurationReader {

        public static string EmailAddressGeneral => GetApplicationOption_String("emailAddressGeneral");

        public static string ConferenceFacebookUrl => GetApplicationOption_String("facebookUrl");

        public static string ConferenceSlackUrl => GetApplicationOption_String("publicSlackUrl");

        public static Dictionary<string, string> AllAsDictionary {
            get {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings.AllKeys.ToDictionary(x => x, x => appSettings[x]);
            }
        }

        private static string GetApplicationOption_String(string optionName, string defaultValue = null)
        {
            //Check for override if debugging
#if DEBUG
            //Configuration values can be overriden using a query string parameter with the config key
            //Example: /?submissionsOpen=true
            if (HttpContext.Current != null)
            {
                var overrideValues = HttpContext.Current.Request.Params;
                if (overrideValues.Count != 0)
                {
                    var overrideValue = overrideValues[optionName];
                    if (overrideValue != null)
                    {
                        return overrideValue;
                    }
                }
            }
#endif
            var configValue = ConfigurationManager.AppSettings.Get(optionName);

            if (!string.IsNullOrWhiteSpace(configValue))
            {
                return configValue;
            }

            return defaultValue;
        }

    }

}