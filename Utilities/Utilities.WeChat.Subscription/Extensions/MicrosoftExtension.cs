using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Subscription.Extensions
{
    /// <summary>
    /// 微软扩展
    /// </summary>
    public static class MicrosoftExtension
    {
        /// <summary>
        /// 转换为整数
        /// </summary>
        /// <param name="value">待转值</param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        /// <summary>
        /// 转换为16位整数
        /// </summary>
        /// <param name="value">待转值</param>
        /// <returns></returns>
        public static Int16 ToInt16(this string value)
        {
            return Int16.Parse(value);
        }

        /// <summary>
        /// 转换为32位整数
        /// </summary>
        /// <param name="value">待转值</param>
        /// <returns></returns>
        public static Int32 ToInt32(this string value)
        {
            return Int32.Parse(value);
        }

        /// <summary>
        /// 转换为64位整数
        /// </summary>
        /// <param name="value">待转值</param>
        /// <returns></returns>
        public static Int64 ToInt64(this string value)
        {
            return Int64.Parse(value);
        }

        /// <summary>
        /// 获取应用配置
        /// </summary>
        /// <param name="key">节点名</param>
        /// <returns></returns>
        public static string GetAppSetting(this string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取数据量链接
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public static string GetConnectionString(this string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
