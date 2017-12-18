using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Subscription.WebpageDevelop
{
    /// <summary>
    /// 网页授权access_token
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public AccessToken() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="appid">公众号的唯一标识</param>
        /// <param name="secret">公众号的appsecret</param>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <param name="grant_type">默认填写为authorization_code</param>
        public AccessToken(string appid, string secret, string code, string grant_type = "authorization_code")
        {
            this.RequestParameter = new Request(appid, secret, code, grant_type);
        }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; } = "https://api.weixin.qq.com/sns/oauth2/access_token";

        /// <summary>
        /// 刷新地址
        /// </summary>
        public string RefreshUrl { get; set; } = "https://api.weixin.qq.com/sns/oauth2/refresh_token";

        /// <summary>
        /// 验证地址
        /// </summary>
        public string VerifyUrl { get; set; } = "https://api.weixin.qq.com/sns/auth";

        /// <summary>
        /// 请求参数
        /// </summary>
        public Request RequestParameter { get; set; }

        /// <summary>
        /// 刷新参数
        /// </summary>
        public Refresh RefreshParameter { get; set; }

        /// <summary>
        /// 刷新
        /// </summary>
        public class Refresh
        {
            /// <summary>
            /// 实例化
            /// </summary>
            /// <param name="appid">公众号的唯一标识</param>
            /// <param name="refresh_token">填写通过access_token获取到的refresh_token参数</param>
            /// <param name="grant_type">默认填写为refresh_token</param>
            public Refresh(string appid, string refresh_token, string grant_type = "refresh_token")
            {
                this.appid = appid;
                this.grant_type = grant_type;
                this.refresh_token = refresh_token;
                this.ValidityTime = DateTime.Now.AddDays(29).AddHours(23).AddMinutes(50);
            }

            /// <summary>
            /// 公众号的唯一标识
            /// </summary>
            public string appid { get; set; }

            /// <summary>
            /// 填写为refresh_token
            /// </summary>
            public string grant_type { get; set; }

            /// <summary>
            /// 填写通过access_token获取到的refresh_token参数  
            /// </summary>
            public string refresh_token { get; set; }

            /// <summary>
            /// 有效时间
            /// </summary>
            private DateTime ValidityTime { get; set; }

            /// <summary>
            /// 允许刷新
            /// </summary>
            /// <returns></returns>
            public bool IsAllowRefresh()
            {
                if (this.ValidityTime > DateTime.Now)
                    return true;
                return false;
            }

            /// <summary>
            /// 转换为对应的字符串
            /// </summary>
            /// <returns></returns>
            public sealed override string ToString()
            {
                return $"appid={this.appid}&grant_type={this.grant_type}&refresh_token={this.refresh_token}";
            }
        }

        /// <summary>
        /// 请求参数
        /// </summary>
        public class Request
        {
            /// <summary>
            /// 实例化
            /// </summary>
            /// <param name="appid">公众号的唯一标识</param>
            /// <param name="secret">公众号的appsecret</param>
            /// <param name="code">填写第一步获取的code参数</param>
            /// <param name="grant_type">默认填写为authorization_code</param>
            public Request(string appid, string secret, string code, string grant_type = "authorization_code")
            {
                this.appid = appid;
                this.secret = secret;
                this.code = code;
                this.grant_type = grant_type;
            }

            /// <summary>
            /// 公众号的唯一标识
            /// </summary>
            public string appid { get; set; }

            /// <summary>
            /// 公众号的appsecret
            /// </summary>
            public string secret { get; set; }

            /// <summary>
            /// 填写第一步获取的code参数
            /// </summary>
            public string code { get; set; }

            /// <summary>
            /// 填写为authorization_code
            /// </summary>
            public string grant_type { get; set; }

            /// <summary>
            /// 转换为对应的字符串
            /// </summary>
            /// <returns></returns>
            public sealed override string ToString()
            {
                return $"appid={this.appid}&secret={this.secret}&code={this.code}&grant_type={this.grant_type}";
            }
        }

        /// <summary>
        /// 结果
        /// </summary>
        public class Result : RequestResult
        {
            /// <summary>
            /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
            /// </summary>
            public string access_token { get; set; }

            /// <summary>
            /// access_token接口调用凭证超时时间，单位（秒）
            /// </summary>
            public int expires_in { get; set; }

            /// <summary>
            /// 用户刷新access_token
            /// </summary>
            public string refresh_token { get; set; }

            /// <summary>
            /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 用户授权的作用域，使用逗号（,）分隔
            /// </summary>
            public string scope { get; set; }
        }

        /// <summary>
        /// 获取网页授权接口调用凭证
        /// </summary>
        /// <param name="appid">公众号的唯一标识</param>
        /// <param name="secret">公众号的appsecret</param>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <param name="grant_type">默认填写为authorization_code</param>
        public Result GetAccessToken(string appid, string secret, string code, string grant_type = "authorization_code")
        {
            this.RequestParameter = new Request(appid, secret, code, grant_type);
            return this.GetAccessToken();
        }

        /// <summary>
        /// 获取网页授权接口调用凭证
        /// </summary>
        public Result GetAccessToken()
        {
            string requestUrl = $"{this.RequestUrl}?{this.RequestParameter.ToString()}";
            Result result = RequestResult.UploadData<Result>(requestUrl);
            if (result != null && result.errcode == 0)
                if (this.RefreshParameter == null)
                {
                    this.RefreshParameter = new Refresh(this.RequestParameter.appid, result.refresh_token);
                }
            return result;
        }

        /// <summary>
        /// 刷新网页授权接口调用凭证
        /// </summary>
        /// <param name="appid">公众号的唯一标识</param>
        /// <param name="refresh_token">填写通过access_token获取到的refresh_token参数</param>
        /// <param name="grant_type">默认填写为refresh_token</param>
        public Result RefreshAccessToken(string appid, string refresh_token, string grant_type = "refresh_token")
        {
            this.RefreshParameter = new Refresh(appid, refresh_token, grant_type);
            return this.RefreshAccessToken();
        }

        /// <summary>
        /// 刷新网页授权接口调用凭证
        /// </summary>
        public Result RefreshAccessToken()
        {
            string refresUrl = $"{this.RefreshUrl}?{this.RefreshParameter.ToString()}";

            return RequestResult.UploadData<Result>(refresUrl);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="accessToken">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="openid">用户的唯一标识</param>
        /// <returns></returns>
        public RequestResult VerifyAccessToken(string accessToken, string openid)
        {
            string requestUrl = $"{this.VerifyUrl}?access_token={accessToken}&openid={openid}";
            return RequestResult.UploadData<RequestResult>(requestUrl, "GET");
        }
    }
}
