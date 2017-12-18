using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Attributes
{
    /// <summary>
    /// 必填特性
    /// </summary>
    public class RequiredAttribute : Attribute
    {
        /// <summary>
        /// 必填验证
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static KeyValuePair<bool, List<string>> Required<TSource>(TSource source) where TSource : class
        {
            KeyValuePair<bool, List<string>> result = new KeyValuePair<bool, List<string>>();
            List<PropertyInfo> propertyInfos = source.GetType().GetProperties().Where(e => e.GetCustomAttributes<RequiredAttribute>().Any()).ToList();
            if (propertyInfos != null && propertyInfos.Count > 0)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    dynamic value1 = propertyInfo.GetValue(source);

                }

                return new KeyValuePair<bool, List<string>>();
            }
            else
                throw new MissingFieldException("Missing RequiredAttribute Fields");
        }
    }
}
