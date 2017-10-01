using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChatEnterprise.ENUM;
using Utilities.WeChatEnterprise.Extension.Microsoft;
using Utilities.WeChatEnterprise.ResponseResults;

namespace Utilities.WeChatEnterprise
{
    /// <summary>
    /// 通讯录操作
    /// </summary>
    public class ContactsOperate
    {
        /// <summary>
        /// 获取人员详细列表
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="department_id">获取的部门id</param>
        /// <param name="fetch_child">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部成员，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加,未填写则默认为4</param>
        /// <returns></returns>
        public virtual GetUserDetail GetUserDetailList(string access_token, string department_id, IsRecursion fetch_child, UserStatus status = UserStatus.UnConcern)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={access_token}&department_id={department_id}&fetch_child={(int)fetch_child}&status={(int)status}";
            WebClient webClient = new WebClient();
            byte[] responseResult = webClient.DownloadData(url);
#if DEBUG
            var data = responseResult.ConvertTo<GetUserDetail>();
#endif
            return responseResult.ConvertTo<GetUserDetail>();
        }
    }
}
