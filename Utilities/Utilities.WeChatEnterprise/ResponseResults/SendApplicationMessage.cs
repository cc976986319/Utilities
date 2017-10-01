using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.ResponseResults
{
    /// <summary>
    /// 发送应用消息结果
    /// </summary>
    public class SendApplicationMessage : ResponseResult
    {
        /// <summary>
        /// 无效工号
        /// </summary>
        public string invaliduser { get; set; }

        /// <summary>
        /// 无效工号
        /// </summary>
        public string[] _invaliduser
        {
            get
            {
                if (string.IsNullOrEmpty(invaliduser)) return null;
                return invaliduser.Split('|');
            }
        }

        /// <summary>
        /// 无效人员部门编号
        /// </summary>
        public string invalidparty { get; set; }

        /// <summary>
        /// 无效人员部门编号
        /// </summary>
        public string[] _invalidparty
        {
            get
            {
                if (string.IsNullOrEmpty(invalidparty)) return null;
                return invalidparty.Split('|');
            }
        }

        /// <summary>
        /// 无效标签
        /// </summary>
        public string invalidtag { get; set; }

        /// <summary>
        /// 无效标签
        /// </summary>
        public string[] _invalidtag
        {
            get
            {
                if (string.IsNullOrEmpty(invalidtag)) return null;
                return invalidtag.Split('|');
            }
        }
    }
}
