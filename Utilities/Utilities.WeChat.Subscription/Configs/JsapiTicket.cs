using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Subscription.Extensions;

namespace Utilities.WeChat.Subscription.Configs
{
    /// <summary>
    /// 微信JS接口的临时票据返回结果
    /// </summary>
    public class JsapiTicket : RequestResult
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public JsapiTicket() { }

        /// <summary>
        /// 管理组标识符
        /// </summary>
        public string group_id { get; set; }

        /// <summary>
        /// ticket是企业号号用于调用微信JS接口的临时票据
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 有效期7200秒，开发者必须在自己的服务全局缓存
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 请求获取JS接口的临时票据
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <returns></returns>
        public static JsapiTicket Request(string url)
        {
            bool result = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=0KUkc4o8IGfDxPreHFJDkqMTzeWB1-PPTluo5FH2FREGrzRfiPt44an50vO8tb_l6GSmQ6mhGNc7ZlabNxU6gAxEjyPS71WcBLBTCqcRhSAXFKgAGAGOT&type=jsapi" == url;

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData(url);
                string jsonstr = Encoding.UTF8.GetString(bytes);
                var data = jsonstr.DeserializeObject<JsapiTicket>();
                return data;
            }
        }
    }
}
