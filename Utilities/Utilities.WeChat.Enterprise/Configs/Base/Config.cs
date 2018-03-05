using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Utilities.WeChat.Enterprise.Configs.Base
{
    /// <summary>
    /// 配置
    /// </summary>
    public abstract class Config : ResponseResult
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="accessToken">全局唯一票据</param>
        /// <param name="url">需要配置的页面路径</param>
        /// <param name="appId">企业Id</param>
        /// <param name="beta">测试版本</param>
        /// <param name="debug">开启调试模式</param>
        public Config(string accessToken, string url, string appId, bool beta, bool debug)
        {
            this.accessToken = accessToken;
            this.appId = appId;
            this.beta = beta;
            this.debug = debug;
            this.url = url;
            this.CreateSignature();
        }

        /// <summary>
        /// 需要配置信息的地址
        /// </summary>
        public string url { get; private set; }

        /// <summary>
        /// 全局唯一票据
        /// </summary>
        public string accessToken { get; private set; }

        /// <summary>
        /// 测试版本
        /// </summary>
        public bool beta { get; private set; }

        /// <summary>
        ///  开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
        /// </summary>
        public bool debug { get; private set; }

        /// <summary>
        ///  必填，企业号的唯一标识，此处填写企业号corpid
        /// </summary>
        public string appId { get; private set; }

        /// <summary>
        /// 必填，生成签名的时间戳
        /// </summary>
        public long timestamp { get; private set; } = Convert.ToInt64((DateTime.Now - Convert.ToDateTime("1970-01-01")).TotalSeconds);

        /// <summary>
        /// 必填，生成签名的随机串
        /// </summary>
        public string nonceStr { get; private set; } = GetNonceStr();

        /// <summary>
        /// 必填，签名，见附录1
        /// </summary>
        public string signature { get; private set; }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetNonceStr()
        {
            char[] nocestr = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 's', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'S', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string result = string.Empty;
            Random random = new Random();
            for (int i = 0; i <= 15; i++)
            {
                int length = random.Next(nocestr.Length - 1);
                result = string.Format("{0}{1}", result, nocestr[length]);
            }
            return result;
        }

        /// <summary>
        /// 生成JS接口的临时票据
        /// </summary>
        /// <returns></returns>
        protected abstract JsapiTicket CreateJsapiTicket();

        /// <summary>
        /// 生成签名
        /// </summary>
        private void CreateSignature()
        {
            JsapiTicket jsapiTicket = this.CreateJsapiTicket();

            string string1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapiTicket.ticket, this.nonceStr, this.timestamp, url);

            this.signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1").ToLower();
        }
    }
}
