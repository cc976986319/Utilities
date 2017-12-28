using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utilities.WeChat.Enterprise.Microsoft
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
#if DEBUG
            string json = bytes.ParseString();
#endif
            return new JavaScriptSerializer().Deserialize<T>(bytes.ParseString());
        }

        /// <summary>
        /// 转换为指定对象
        /// </summary>
        /// <typeparam name="T">容器</typeparam>
        /// <param name="value">json字符串</param>
        /// <returns></returns>
        public static T ConvertTo<T>(this string value) where T : class
        {
            return new JavaScriptSerializer().Deserialize<T>(value);
        }

        /// <summary>
        /// 转换为字节对象
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static byte[] ToBytes(this object value)
        {
            return Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(value));
        }

        /// <summary>
        /// 整形列表叠加
        /// </summary>
        /// <param name="items">整形列表</param>
        /// <returns></returns>
        public static int Superposition(this IEnumerable<int> items)
        {
            return items.Sum(e => e);
        }
    }
}
