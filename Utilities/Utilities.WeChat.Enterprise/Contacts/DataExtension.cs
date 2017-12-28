using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.Contacts
{
    /// <summary>
    /// 数据扩展
    /// </summary>
    public static class DataExtension
    {
        /// <summary>
        /// 转换为整形数组
        /// </summary>
        /// <param name="status">成员类型</param>
        /// <returns></returns>
        public static int[] ToIntArray(this IEnumerable<Member.MemberStatus> status)
        {
            return status.Select(e => (int)e).ToArray();
        }
    }
}
