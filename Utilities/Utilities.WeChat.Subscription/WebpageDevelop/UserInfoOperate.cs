using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Subscription.WebpageDevelop
{
    /// <summary>
    /// 用户信息操作
    /// </summary>
    public class UserInfoOperate
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; } = "https://api.weixin.qq.com/sns/userinfo";

        /// <summary>
        /// 请求参数
        /// </summary>
        public RequestParameter Parameter { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="parameter">参数</param>
        public UserInfoOperate(RequestParameter parameter)
        {
            this.Parameter = parameter;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="access_token">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="openid">用户的唯一标识</param>
        /// <param name="lang">返回国家地区语言版本(默认：zh_CN)，zh_CN 简体，zh_TW 繁体，en 英语</param>
        public UserInfoOperate(string access_token, string openid, ENUM.Language lang = ENUM.Language.zh_CN)
        {
            this.Parameter = new RequestParameter(access_token, openid, lang);
        }

        /// <summary>
        /// 请求参数
        /// </summary>
        public class RequestParameter
        {
            /// <summary>
            /// 实例化
            /// </summary>
            /// <param name="access_token">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
            /// <param name="openid">用户的唯一标识</param>
            /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
            public RequestParameter(string access_token, string openid, ENUM.Language lang)
            {
                this.access_token = access_token;
                this.openid = openid;
                this.lang = lang;
            }

            /// <summary>
            /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
            /// </summary>
            public string access_token { get; set; }

            /// <summary>
            /// 用户的唯一标识
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语
            /// </summary>
            public ENUM.Language lang { get; set; }

            /// <summary>
            /// 转换为指定的字符串
            /// </summary>
            /// <returns></returns>
            public sealed override string ToString()
            {
                return $"access_token={this.access_token}&openid={this.openid}&lang={Enum.GetName(typeof(ENUM.Language), lang)} ";
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public class UserInfo : RequestResult
        {
            /// <summary>
            /// 用户的唯一标识
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 用户昵称
            /// </summary>
            public string nickname { get; set; }

            /// <summary>
            /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
            /// </summary>
            public int sex { get; set; }

            /// <summary>
            /// 用户个人资料填写的省份
            /// </summary>
            public string province { get; set; }

            /// <summary>
            /// 普通用户个人资料填写的城市
            /// </summary>
            public string city { get; set; }

            /// <summary>
            /// 国家，如中国为CN
            /// </summary>
            public string country { get; set; }

            /// <summary>
            /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
            /// </summary>
            public string headimgurl { get; set; }

            /// <summary>
            /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
            /// </summary>
            public string[] privilege { get; set; }

            /// <summary>
            /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
            /// </summary>
            public string unionid { get; set; }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserInfo()
        {
            return RequestResult.Download<UserInfo>($"{this.RequestUrl}?{this.Parameter.ToString()}");
        }
    }
}
