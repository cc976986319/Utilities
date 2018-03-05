using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.Attributes
{
    /// <summary>
    /// 调用扩展
    /// </summary>
    public static class InvokingExtension
    {
        /// <summary>
        /// 比填字段检查
        /// </summary>
        /// <typeparam name="T">检查对象</typeparam>
        /// <param name="source">检查数据</param>
        public static void RequisiteCheck<T>(this T source) where T : class
        {
            RequisiteAttribute.Check<T>(source);
        }
    }
}
