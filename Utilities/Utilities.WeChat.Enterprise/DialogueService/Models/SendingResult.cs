using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.DialogueService.Models
{
    /// <summary>
    /// 会话内容发送结果
    /// </summary>
    public class SendingResult : ResponseResult
    {
        /// <summary>
        /// 列表中不存在的成员会返回在invaliduser里，剩余合法成员会继续执行。
        /// </summary>
        public List<string> invaliduser { get; set; }
    }
}
