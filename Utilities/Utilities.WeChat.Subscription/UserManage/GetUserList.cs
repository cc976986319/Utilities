using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Subscription.UserManage
{
    /// <summary>
    /// 获取用户列表
    /// </summary>
    public class GetUserList
    {
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 第一个拉取的OPENID，不填默认从头开始拉取
        /// </summary>
        public string next_openid { get; set; }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public OpenIdResponseResult GetUsers()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] result = client.DownloadData($"https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&next_openid=NEXT_OPENID");
                return RequestResult.Parse<OpenIdResponseResult>(result);
            }
        }

        /// <summary>
        /// 关注者列表
        /// </summary>
        public class OpenIdResponseResult : RequestResult
        {
            /// <summary>
            /// 关注该公众账号的总用户数
            /// </summary>
            public int total { get; set; }

            /// <summary>
            /// 拉取的OPENID个数，最大值为10000
            /// </summary>
            public int count { get; set; }

            /// <summary>
            /// 列表数据，OPENID的列表
            /// </summary>
            public string[] data { get; set; }

            /// <summary>
            /// 拉取列表的最后一个用户的OPENID
            /// </summary>
            public OpenId next_openid { get; set; }

            public class OpenId
            {
                public string[] openid { get; set; }
            }
        }
    }
}
