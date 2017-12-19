using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.DialogueService.Models.Operate
{
    /// <summary>
    /// 创建会话
    /// </summary>
    public class Create
    {
        /// <summary>
        /// 会话id。字符串类型，最长32个字符。只允许字符0-9及字母a-zA-Z, 如果值内容为64bit无符号整型：要求值范围在[1, 2 ^ 63)之间，[2 ^ 63, 2 ^ 64)为系统分配会话id区间
        /// </summary>
        public string chatid { get; set; }

        /// <summary>
        /// 会话标题
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 管理员userid，必须是该会话userlist的成员之一
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// 会话成员列表，成员用userid来标识。会话成员必须在3人或以上，2000人以下
        /// </summary>
        public List<string> userlist { get; set; }
    }
}
