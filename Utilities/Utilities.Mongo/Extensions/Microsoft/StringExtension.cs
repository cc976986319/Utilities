using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mongo.Extensions.Microsoft
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 获取ConnectionString
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static string ReadingConnectionStrings(this string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
