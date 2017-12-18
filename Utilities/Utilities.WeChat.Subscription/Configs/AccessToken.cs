using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Subscription.Configs
{
    /// <summary>
    /// 普通的AccessToken
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; } = "https://api.weixin.qq.com/cgi-bin/token";

        /// <summary>
        /// 请求参数
        /// </summary>
        public Request RequestParameter { get; set; }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，即appsecret</param>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <returns></returns>
        public Result GetAccessToken(string appid, string secret, string grant_type = "client_credential")
        {
            Request request = new Request(appid, secret, grant_type);

            return GetAccessToken(request);
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public Result GetAccessToken(Request request)
        {
            this.RequestParameter = request;
            string requestUrl = $"{this.RequestUrl}?{request.ToString()}";
            Result result = RequestResult.Download<Result>(requestUrl);
            return result;
        }

        /// <summary>
        /// 结果
        /// </summary>
        public class Result : RequestResult
        {
            /// <summary>
            /// 获取到的凭证
            /// </summary>
            public string access_token { get; set; }

            /// <summary>
            /// 凭证有效时间，单位：秒
            /// </summary>
            public int expires_in { get; set; }
        }

        /// <summary>
        /// 请求参数
        /// </summary>
        public class Request
        {
            /// <summary>
            /// 实例化
            /// </summary>
            /// <param name="appid">第三方用户唯一凭证</param>
            /// <param name="secret">第三方用户唯一凭证密钥，即appsecret</param>
            /// <param name="grant_type">获取access_token填写client_credential</param>
            public Request(string appid, string secret, string grant_type = "client_credential")
            {
                this.appid = appid;
                this.secret = secret;
                this.grant_type = grant_type;
            }

            /// <summary>
            /// 获取access_token填写client_credential
            /// </summary>
            public string grant_type { get; set; }

            /// <summary>
            /// 第三方用户唯一凭证
            /// </summary>
            public string appid { get; set; }

            /// <summary>
            /// 第三方用户唯一凭证密钥，即appsecret
            /// </summary>
            public string secret { get; set; }

            /// <summary>
            /// 转换为字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"grant_type={this.grant_type}&appid={this.appid}&secret={this.secret}";
            }
        }

    }
}
