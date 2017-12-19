using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utilities.WeChat.Enterprise
{
    /// <summary>
    /// 响应类型(返回基类)
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 序列化为Json格式的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}
