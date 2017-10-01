using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChatEnterprise.Models;

namespace Utilities.WeChatEnterprise.ResponseResults
{
    /// <summary>
    /// 用户详情(响应结果)
    /// </summary>
    public class GetUserDetail : ResponseResult
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        public User[] userlist { get; set; }
    }
}
