using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.Models
{
    /// <summary>
    /// 会话消息
    /// </summary>
    public class ChatInfo
    {
        /// <summary>
        /// 会话id
        /// </summary>
        public string chatid { get; set; }

        /// <summary>
        /// 会话标题
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 管理员userid
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// 会话成员列表，成员用userid来标识
        /// </summary>
        public string[] userlist { get; set; }

        /// <summary>
        /// 序列化为Json格式的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
