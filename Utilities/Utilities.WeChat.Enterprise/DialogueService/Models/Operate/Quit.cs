using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.DialogueService.Models.Operate
{
    /// <summary>
    /// 退出会话
    /// </summary>
    public class Quit
    {
        /// <summary>
        /// 会话id
        /// </summary>
        public string chatid { get; set; }

        /// <summary>
        /// 操作人userid
        /// </summary>
        public string op_user { get; set; }
    }
}
