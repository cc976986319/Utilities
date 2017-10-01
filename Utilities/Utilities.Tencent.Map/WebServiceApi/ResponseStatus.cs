using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Tencent.Map.WebServiceApi
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum ResponseStatus : int
    {
        /// <summary>
        /// 0为正常
        /// </summary>
        Success = 0,
        /// <summary>
        /// 310请求参数信息有误
        /// </summary>
        ParameterException = 310,
        /// <summary>
        /// 311key格式错误
        /// </summary>
        KeyFormatError = 311,
        /// <summary>
        /// 306请求有护持信息请检查字符串
        /// </summary>
        CheckString = 306,
        /// <summary>
        /// 110请求来源未被授权
        /// </summary>
        Unauthorized = 110
    }
}
