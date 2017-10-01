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
    /// 应用消息操作
    /// </summary>
    public class ApplicationMessageOperate
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="message">消息数据</param>
        /// <returns></returns>
        public virtual SendApplicationMessage Sending(string access_token, ApplicationMessage message)
        {
            message.VerifyFormat();

            string url = $"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={access_token}";
            WebClient webClient = new WebClient();
            byte[] responseResult = webClient.UploadData(url, "Post", message.ToBytes());
#if DEBUG
            var data = responseResult.ConvertTo<SendApplicationMessage>();
#endif
            return responseResult.ConvertTo<SendApplicationMessage>();
        }
    }
}
