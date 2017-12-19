using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.DialogueService.Models.Operate
{
    /// <summary>
    /// 清除未读会话
    /// </summary>
    public class ClearNotify
    {
        public ClearNotify(string op_user, string id, bool isSingle)
        {
            this.op_user = op_user;
            this.chat = new Chat(id, isSingle);
        }

        public ClearNotify(string op_user, Chat chat)
        {
            this.op_user = op_user;
            this.chat = chat;
        }

        /// <summary>
        /// 会话所有者的userid
        /// </summary>
        public string op_user { get; set; }

        /// <summary>
        /// 会话
        /// </summary>
        public Chat chat { get; private set; }

        public class Chat
        {
            public Chat(string id, bool isSingle = true)
            {
                this.id = id;
                this.type = isSingle ? "single" : "group";
            }

            /// <summary>
            /// 会话类型：single|group，分别表示：群聊|单聊
            /// </summary>
            public string type { get; private set; } = "single";
            /// <summary>
            /// 会话值，为userid|chatid，分别表示：成员id|会话id
            /// </summary>
            public string id { get; set; }
        }
    }
}
