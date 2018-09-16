using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Util
{
   public static class ConfigHelp
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppsettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取数据库配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionStr(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static SqlConnection GetSqlConnection() {
          return  new SqlConnection(GetConnectionStr("default"));
        }


    }
}
