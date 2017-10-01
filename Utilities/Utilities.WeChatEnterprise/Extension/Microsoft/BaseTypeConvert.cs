using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.Extension.Microsoft
{
    /// <summary>
    /// 基本类型转换
    /// </summary>
    public static class BaseTypeConvert
    {
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="bytes">字节对象</param>
        /// <returns></returns>
        public static string ParseString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 转换为指定对象
        /// </summary>
        /// <typeparam name="T">容器</typeparam>
        /// <param name="bytes">字节对象</param>
        /// <returns></returns>
        public static T ConvertTo<T>(this byte[] bytes) where T : class
        {
            return JsonConvert.DeserializeObject<T>(bytes.ParseString());
        }

        /// <summary>
        /// 转换为字节对象
        /// </summary>
        /// <param name="value">字符串对象</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}
