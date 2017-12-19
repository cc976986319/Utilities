using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.DialogueService.Models
{
    /// <summary>
    /// 获取会话
    /// </summary>
    public class GetDialogue : ResponseResult
    {
        /// <summary>
        /// 会话信息
        /// </summary>
        public Operate.Create chat_info { get; set; }
    }
}
