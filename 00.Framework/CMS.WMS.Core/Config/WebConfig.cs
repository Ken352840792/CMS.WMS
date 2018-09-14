using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Core
{
    public class WebConfig : IConfig
    {
        public string ConnectionStrings(string defaultName = "default")
        {
           return ConfigurationManager.ConnectionStrings[defaultName].ConnectionString;
        }

        public string GetAppSetting(string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }
    }
}
