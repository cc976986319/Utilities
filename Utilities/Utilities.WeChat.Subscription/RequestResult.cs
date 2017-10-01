using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Subscription
{
    /// <summary>
    /// 请求结果
    /// </summary>
    public class RequestResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="T">指定的对象，但必须继承于RequestResult</typeparam>
        /// <param name="json">json字符串对象</param>
        /// <returns></returns>
        public static T Parse<T>(string json) where T : RequestResult
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="T">指定的对象，但必须继承于RequestResult</typeparam>
        /// <param name="bytes">字节对象</param>
        /// <returns></returns>
        public static T Parse<T>(byte[] bytes) where T : RequestResult
        {
            string json = Encoding.UTF8.GetString(bytes);
            return Parse<T>(json);
        }
    }
}
