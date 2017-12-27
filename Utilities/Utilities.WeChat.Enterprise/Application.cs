using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise
{
    /// <summary>
    /// 企业应用
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 获取企业号应用
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="agentid">企业应用的id</param>
        /// <param name="requestUrl">请求地址</param>
        /// <remarks>该API用于获取企业号某个应用的基本信息，包括头像、昵称、帐号类型、认证类型、可见范围等信息</remarks>
        /// <returns></returns>
        public GetResult Get(string accessToken, int agentid, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/agent/get")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&agentid={agentid}");
                return bytes.ConvertTo<GetResult>();
            }
        }

        /// <summary>
        /// 设置企业号应用
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="body">应用信息</param>
        /// <param name="requestUrl">请求地址</param>
        /// <remarks>该API用于设置企业应用的选项设置信息，如：地理位置上报等。第三方服务商不能调用该接口设置授权的主页型应用。</remarks>
        /// <returns></returns>
        public ResponseResult Set(string accessToken, Body body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/agent/set")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 获取应用概况列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="requestUrl">请求地址</param>
        /// <remarks>该API 用于获取secret所在管理组内的应用概况，会返回管理组内应用的id及名称、头像等信息</remarks>
        /// <returns></returns>
        public ListResult List(string accessToken, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/agent/list")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}");
                return bytes.ConvertTo<ListResult>();
            }
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public class GetResult : ResponseResult
        {
            /// <summary>
            /// 企业应用id
            /// </summary>
            public string agentid { get; set; }

            /// <summary>
            /// 企业应用名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 企业应用方形头像
            /// </summary>
            public string square_logo_url { get; set; }

            /// <summary>
            /// 企业应用圆形头像
            /// </summary>
            public string round_logo_url { get; set; }

            /// <summary>
            /// 企业应用详情
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 企业应用可见范围（人员），其中包括userid和关注状态state
            /// </summary>
            public UserInfo allow_userinfos { get; set; }

            /// <summary>
            /// 企业应用可见范围（部门）
            /// </summary>
            public Party allow_partys { get; set; }

            /// <summary>
            /// 企业应用可见范围（标签）
            /// </summary>
            public Tags allow_tags { get; set; }

            /// <summary>
            /// 企业应用是否被禁用
            /// </summary>
            public int close { get; set; }

            /// <summary>
            /// 企业应用可信域名
            /// </summary>
            public string redirect_domain { get; set; }

            /// <summary>
            /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
            /// </summary>
            public LocationFlag report_location_flag { get; set; }

            /// <summary>
            /// 是否接收用户变更通知。0：不接收；1：接收
            /// </summary>
            public InformType isreportuser { get; set; }

            /// <summary>
            /// 是否上报用户进入应用事件。0：不接收；1：接收
            /// </summary>
            public InformType isreportenter { get; set; }

            /// <summary>
            /// 应用类型。1：消息型；2：主页型
            /// </summary>
            public ApplicationType type { get; set; }

            /// <summary>
            /// 关联会话url
            /// </summary>
            public string chat_extension_url { get; set; }

            /// <summary>
            /// 用户信息
            /// </summary>
            public class UserInfo
            {
                public List<User> user { get; set; }

                /// <summary>
                /// 成员信息
                /// </summary>
                public class User
                {
                    public string userid { get; set; }

                    public string status { get; set; }
                }
            }

            /// <summary>
            /// 部门信息
            /// </summary>
            public class Party
            {
                public List<int> partyid { get; set; }
            }

            /// <summary>
            /// 标签信息
            /// </summary>
            public class Tags
            {
                public List<int> tagid { get; set; }
            }
        }

        /// <summary>
        /// 列表结果
        /// </summary>
        public class ListResult : ResponseResult
        {
            public List<Agent> agentlist { get; set; }

            public class Agent
            {
                /// <summary>
                /// 应用id
                /// </summary>
                public string agentid { get; set; }

                /// <summary>
                /// 应用名称
                /// </summary>
                public string name { get; set; }

                /// <summary>
                /// 方形头像url
                /// </summary>
                public string square_logo_url { get; set; }

                /// <summary>
                /// 圆形头像url
                /// </summary>
                public string round_logo_url { get; set; }
            }
        }

        /// <summary>
        /// 应用消息
        /// </summary>
        public class Body
        {
            /// <summary>
            /// 企业应用的id
            /// </summary>
            public string agentid { get; set; }

            /// <summary>
            /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
            /// </summary>
            public LocationFlag report_location_flag { get; set; }

            /// <summary>
            /// 企业应用头像的mediaid，通过多媒体接口上传图片获得mediaid，上传后会自动裁剪成方形和圆形两个头像
            /// </summary>
            public string logo_mediaid { get; set; }

            /// <summary>
            /// 企业应用名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 企业应用详情
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 企业应用可信域名
            /// </summary>
            public string redirect_domain { get; set; }

            /// <summary>
            /// 是否接收用户变更通知。0：不接收；1：接收。
            /// </summary>
            public InformType isreportuser { get; set; }

            /// <summary>
            /// 是否上报用户进入应用事件。0：不接收；1：接收。
            /// </summary>
            public InformType isreportenter { get; set; }

            /// <summary>
            /// 主页型应用url。url必须以http或者https开头。消息型应用无需该参数
            /// </summary>
            public string home_url { get; set; }

            /// <summary>
            /// 关联会话url。设置该字段后，企业会话"+"号将出现该应用，点击应用可直接跳转到此url，支持jsapi向当前会话发送消息。
            /// </summary>
            public string chat_extension_url { get; set; }
        }

        /// <summary>
        /// 地理位置标记
        /// </summary>
        public enum LocationFlag : int
        {
            /// <summary>
            /// 不上报
            /// </summary>
            NotInform = 0,
            /// <summary>
            /// 进入上报
            /// </summary>
            IntoInform = 1,
            /// <summary>
            /// 持续上报
            /// </summary>
            ContinueInform = 2
        }

        /// <summary>
        /// 通知类型
        /// </summary>
        public enum InformType : int
        {
            /// <summary>
            /// 不接受
            /// </summary>
            NotAccept = 0,
            /// <summary>
            /// 接受
            /// </summary>
            Accept = 1
        }

        /// <summary>
        /// 应用类型
        /// </summary>
        public enum ApplicationType : int
        {
            /// <summary>
            /// 消息型
            /// </summary>
            Message = 1,
            /// <summary>
            /// 主页型
            /// </summary>
            HomePage = 2
        }
    }
}
