using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utilities.WeChat.Subscription.Extensions
{
    /// <summary>
    /// Json序列化扩展
    /// </summary>
    public static class JsonSerializedExtension
    {
        /// <summary>
        /// 序列化为字符串
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="source">数据</param>
        /// <returns></returns>
        public static string ToJson<T>(this T source)
        {
            return new JavaScriptSerializer().Serialize(source);
        }

        /// <summary>
        /// 序列化为对象
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="jsonStr">json格式的字符串</param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string jsonStr)
        {
            return new JavaScriptSerializer().Deserialize<T>(jsonStr);
        }

        /// <summary>
        /// 序列化为对象
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="bytes">bytes对象</param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this byte[] bytes)
        {
            string jsonStr = Encoding.UTF8.GetString(bytes);
            return new JavaScriptSerializer().Deserialize<T>(jsonStr);
        }
    }
}
