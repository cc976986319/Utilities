using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Attributes;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise.AuthenticationPort
{
    /// <summary>
    /// OAuth验证接口
    /// </summary>
    public class OAuth
    {
        /// <summary>
        /// 重定向地址配置
        /// </summary>
        /// <param name="appid">企业的CorpID</param>
        /// <param name="redirect_url">授权后重定向的回调链接地址</param>
        /// <param name="agentid">企业应用的id</param>
        /// <param name="scope">应用授权作用域</param>
        /// <param name="state">重定向后会带上state参数，企业可以填写a-zA-Z0-9的参数值，长度不可超过128个字节</param>
        /// <returns></returns>
        public static string RedirectUrlSetting(string appid, string redirect_url, string agentid, Scope scope = Scope.snsapi_base, string state = "")
        {
            RedirectBody redirectBody = new RedirectBody()
            {
                Appid = appid,
                Redirect_uri = redirect_url,
                Agentid = agentid,
                Scope = scope,
                State = state,
            };

            return redirectBody.ToString();
        }

        /// <summary>
        /// 应用授权作用域
        /// </summary>
        public enum Scope
        {
            /// <summary>
            /// 静默授权，可获取成员的基础信息
            /// </summary>
            snsapi_base,
            /// <summary>
            /// 静默授权，可获取成员的详细信息，但不包含手机、邮箱
            /// </summary>
            snsapi_userinfo,
            /// <summary>
            /// 手动授权，可获取成员的详细信息，包含手机、邮箱
            /// </summary>
            snsapi_privateinfo
        }

        /// <summary>
        /// 重定向体
        /// </summary>
        public class RedirectBody
        {
            /// <summary>
            /// 企业的CorpID
            /// </summary>
            [Requisite("比填项")]
            public string Appid { get; set; }

            /// <summary>
            /// 授权后重定向的回调链接地址，请使用urlencode对链接进行处理
            /// </summary>
            [Requisite("比填项")]
            public string Redirect_uri { get; set; }

            /// <summary>
            /// 返回类型，此时固定为：code
            /// </summary>
            [Requisite("比填项")]
            public string Response_type { get; set; } = "code";

            /// <summary>
            /// 应用授权作用域。snsapi_base：静默授权，可获取成员的基础信息； snsapi_userinfo：静默授权，可获取成员的详细信息，但不包含手机、邮箱； snsapi_privateinfo：手动授权，可获取成员的详细信息，包含手机、邮箱。
            /// </summary>
            [Requisite("比填项")]
            public Scope Scope { get; set; } = Scope.snsapi_base;

            /// <summary>
            /// 企业应用的id。当scope是snsapi_userinfo或snsapi_privateinfo时，该参数必填。 注意redirect_uri的域名必须与该应用的可信域名一致。
            /// </summary>
            public string Agentid { get; set; } = "AGENTID";

            /// <summary>
            /// 重定向后会带上state参数，企业可以填写a-zA-Z0-9的参数值，长度不可超过128个字节
            /// </summary>
            [Requisite("比填项")]
            public string State { get; set; }

            /// <summary>
            /// 微信终端使用此参数判断是否需要带上身份信息
            /// </summary>
            [Requisite("比填项")]
            public string Wechat_Redirect { get; set; } = "#wechat_redirect";

            /// <summary>
            /// 检查
            /// </summary>
            /// <returns></returns>
            public void Checked()
            {
                this.RequisiteCheck();

                if (this.Scope == Scope.snsapi_userinfo || this.Scope == Scope.snsapi_privateinfo)
                {
                    if (string.IsNullOrEmpty(this.Agentid) || this.Agentid == "AGENTID") throw new ArgumentNullException("当scope是snsapi_userinfo或snsapi_privateinfo时，该参数必填");
                }
                if (Encoding.UTF8.GetBytes(this.State).Length > 128)
                    throw new ArgumentOutOfRangeException("state参数:长度不可超过128个字节");
            }

            /// <summary>
            /// 转换为字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                this.Checked();
                return "https://" + $"open.weixin.qq.com/connect/oauth2/authorize?appid={this.Appid}&redirect_uri={this.Redirect_uri}&response_type={this.Response_type}&scope={Enum.GetName(typeof(Scope), this.Scope)}&agentid={this.Agentid}&state={this.State}{this.Wechat_Redirect}";
            }
        }

        /// <summary>
        /// 根据code获取成员信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="code">通过成员授权获取到的code，每次成员授权带上的code将不一样，code只能使用一次，10分钟未被使用自动过期</param>
        /// <returns></returns>
        public static Member GetMemberByCode(string accessToken, string code)
        {
            string requestUri = "https://" + $"qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={accessToken}&code={code}";
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = webClient.DownloadData(requestUri);
                return bytes.ConvertTo<Member>();
            }
        }

        /// <summary>
        /// 成员信息
        /// </summary>
        public class Member : ResponseResult
        {
            /// <summary>
            /// 成员UserID
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// 非企业成员的标识，对当前企业号唯一
            /// </summary>
            public string OpenId { get; set; }

            /// <summary>
            /// 手机设备号(由微信在安装时随机生成，删除重装会改变，升级不受影响)
            /// </summary>
            public string DeviceId { get; set; }

            /// <summary>
            /// 成员票据，最大为512字节。scope为snsapi_userinfo或snsapi_privateinfo，且用户在应用可见范围之内时返回此参数。 后续利用该参数可以获取用户信息或敏感信息。
            /// </summary>
            public string user_ticket { get; set; }

            /// <summary>
            /// user_token的有效时间（秒），随user_ticket一起返回
            /// </summary>
            public int expires_in { get; set; }
        }

        /// <summary>
        /// 使用user_ticket获取成员详情
        /// </summary>
        /// <param name="accesstoken">调用接口凭证</param>
        /// <param name="user_ticket">成员票据</param>
        /// <returns></returns>
        public static MemberDetail GetMemberDetailByUserTicket(string accesstoken, string user_ticket)
        {
            UserTicket userTicket = new UserTicket(user_ticket);
            string requestUri = "https://" + $"qyapi.weixin.qq.com/cgi-bin/user/getuserdetail?access_token={accesstoken}";
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = webClient.UploadData(requestUri, userTicket.ToBytes());
                return bytes.ConvertTo<MemberDetail>();
            }
        }

        /// <summary>
        /// 成员详情
        /// </summary>
        public class MemberDetail : ResponseResult
        {
            /// <summary>
            /// 成员UserID
            /// </summary>
            public string userid { get; set; }

            /// <summary>
            /// 成员姓名
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 成员所属部门
            /// </summary>
            public int[] department { get; set; }

            /// <summary>
            /// 职位信息
            /// </summary>
            public string position { get; set; }

            /// <summary>
            /// 成员手机号，仅在用户同意snsapi_privateinfo授权时返回
            /// </summary>
            public string mobile { get; set; }

            /// <summary>
            /// 性别。0表示未定义，1表示男性，2表示女性
            /// </summary>
            public int gender { get; set; }

            /// <summary>
            /// 成员邮箱，仅在用户同意snsapi_privateinfo授权时返回
            /// </summary>
            public string email { get; set; }

            /// <summary>
            /// 头像url。注：如果要获取小图将url最后的”/0”改成”/64”即可
            /// </summary>
            public string avatar { get; set; }
        }

        /// <summary>
        /// 成员票据
        /// </summary>
        public class UserTicket
        {
            /// <summary>
            /// 实例化
            /// </summary>
            /// <param name="user_ticket">成员票据</param>
            public UserTicket(string user_ticket)
            {
                this.user_ticket = user_ticket;
            }

            /// <summary>
            /// 成员票据
            /// </summary>
            public string user_ticket { get; set; }
        }
    }
}
