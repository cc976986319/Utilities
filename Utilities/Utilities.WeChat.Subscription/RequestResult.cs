using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Subscription.Extensions;

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
        /// 中文错误描述
        /// </summary>
        public string _errmsg
        {
            get { try { return DictionaryReport.GlobalBackCode[this.errcode]; } catch (Exception ex) { return errmsg; } }
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="T">指定的对象，但必须继承于RequestResult</typeparam>
        /// <param name="json">json字符串对象</param>
        /// <returns></returns>
        public static T Parse<T>(string json) where T : RequestResult
        {
            return json.DeserializeObject<T>();
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

        /// <summary>
        /// 上传请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestType">请求类型，默认POST</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static T UploadData<T>(string url, string requestType = "POST", string content = "") where T : RequestResult
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = webClient.UploadData(url, requestType, Encoding.UTF8.GetBytes(content));
                return RequestResult.Parse<T>(bytes);
            }
        }

        /// <summary>
        /// 下载请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static T Download<T>(string url) where T : RequestResult
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = webClient.DownloadData(url);
                return RequestResult.Parse<T>(bytes);
            }
        }
    }
}
