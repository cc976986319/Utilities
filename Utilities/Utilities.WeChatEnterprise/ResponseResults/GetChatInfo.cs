using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.ResponseResults
{
    /// <summary>
    /// 获取聊天消息
    /// </summary>
    public class GetChatInfo : ResponseResult
    {
        /// <summary>
        /// 会话信息
        /// </summary>
        public Models.ChatInfo chat_info { get; set; }
    }
}
