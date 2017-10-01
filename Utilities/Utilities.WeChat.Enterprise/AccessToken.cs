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
    /// 企业号的全局唯一票据
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public AccessToken() { }

        /// <summary>
        /// 获取全局唯一票据
        /// </summary>
        /// <param name="corpid">企业Id</param>
        /// <param name="corpsecret">管理组的凭证密钥</param>
        /// <returns></returns>
        public static AccessToken GetAccessToken(string corpid, string corpsecret)
        {
            WebClient client = new WebClient();
            byte[] responseResult = client.DownloadData($"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={corpid}&corpsecret={corpsecret}");

#if DEBUG
            var data = responseResult.ConvertTo<AccessToken>();
            return data;
#endif
#if !DEBUG
            return responseResult.ConvertTo<AccessToken>();
#endif
        }

        /// <summary>
        /// 获取到的凭证。长度为64至512个字节
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }
    }
}
