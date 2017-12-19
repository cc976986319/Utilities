using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.DialogueService.Models.Operate
{
    /// <summary>
    /// 成员新消息免打扰
    /// </summary>
    public class SetMute
    {
        /// <summary>
        /// 成员新消息免打扰参数，数组，最大支持10000个成员
        /// </summary>
        public List<UserMute> user_mute_list { get; private set; }

        public class UserMute
        {
            /// <summary>
            /// 成员UserID
            /// </summary>
            public string userid { get; set; }

            /// <summary>
            /// 免打扰状态，0关闭，1打开,默认为0。当打开时所有消息不提醒；当关闭时，以成员对会话的设置为准。
            /// </summary>
            public Status status { get; set; }
        }

        /// <summary>
        /// 免打扰状态
        /// </summary>
        public enum Status : int
        {
            /// <summary>
            /// 关闭
            /// </summary>
            Off = 0,
            /// <summary>
            /// 打开
            /// </summary>
            On = 1
        }
    }
}
