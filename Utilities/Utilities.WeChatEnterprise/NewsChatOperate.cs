using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChatEnterprise.Extension.Microsoft;
using Utilities.WeChatEnterprise.ResponseResults;

namespace Utilities.WeChatEnterprise
{
    /// <summary>
    /// 会话聊天操作
    /// </summary>
    public class NewsChatOperate
    {
        /// <summary>
        /// 获取会话信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="chatId">会话编号</param>
        /// <returns></returns>
        public virtual GetChatInfo GetChatInfo(string accessToken, string chatId)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/chat/get?access_token={accessToken}&chatid={chatId}";
            WebClient client = new WebClient();
            byte[] responseResult = client.DownloadData(url);
            return responseResult.ConvertTo<GetChatInfo>();
        }

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="chatInfo">会话消息</param>
        /// <returns></returns>
        public virtual ResponseResult CreateChatInfo(string accessToken, Models.ChatInfo chatInfo)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/chat/create?access_token={accessToken}";
            WebClient client = new WebClient();
            byte[] responseResult = client.UploadData(url, "Post", Encoding.UTF8.GetBytes(chatInfo.ToString()));
            return responseResult.ConvertTo<ResponseResult>();
        }
    }
}
