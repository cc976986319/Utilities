using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChatEnterprise.Extension.Microsoft;
using Utilities.WeChatEnterprise.Models;
using Utilities.WeChatEnterprise.ResponseResults;

namespace Utilities.WeChatEnterprise
{
    /// <summary>
    /// 认证操作
    /// </summary>
    public class AuthenticationOperate
    {
        /// <summary>
        /// 根据Code值获取用户信息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="code">通过成员授权获取到的code，每次成员授权带上的code将不一样，code只能使用一次，10分钟未被使用自动过期</param>
        /// <returns></returns>
        public GetUserInfoByCode GetUserInfoByCode(string access_token, string code)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={access_token}&code={code}";
            WebClient webClient = new WebClient();
            byte[] responseResult = webClient.DownloadData(url);
#if DEBUG
            var data = responseResult.ConvertTo<GetUserInfoByCode>();
#endif
            return responseResult.ConvertTo<GetUserInfoByCode>();
        }

        public User GetUserDetailByUser_ticket(string access_token, string user_ticket)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/getuserdetail?access_token={access_token}";
            WebClient webclient = new WebClient();
            byte[] responseResult = webclient.UploadData(url, "POST", ("{user_ticket:'" + user_ticket + "'}").ToBytes());
#if DEBUG
            var data = responseResult.ConvertTo<User>();
#endif
            return responseResult.ConvertTo<User>();
        }
    }
}
