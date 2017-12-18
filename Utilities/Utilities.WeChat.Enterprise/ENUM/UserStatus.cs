using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.ENUM
{
    /// <summary>
    /// 人员状态(枚举)
    /// </summary>
    public enum UserStatus : int
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 关注
        /// </summary>
        Concern = 1,
        /// <summary>
        /// 冻结
        /// </summary>
        Freeze = 2,
        /// <summary>
        /// 未关注
        /// </summary>
        UnConcern = 4,
    }
}
