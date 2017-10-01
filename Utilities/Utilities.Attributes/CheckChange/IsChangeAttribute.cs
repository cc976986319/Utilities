using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Attributes.CheckChange
{
    /// <summary>
    /// 改变检查特性
    /// </summary>
    public class IsChangeAttribute : Attribute
    {
        /// <summary>
        /// 改变判断
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <typeparam name="TValue">数据值类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="value">数据值</param>
        /// <returns></returns>
        public static bool Change<TSource, TValue>(TSource source, TValue value) where TSource : class where TValue : class
        {
            List<PropertyInfo> propertyInfos = source.GetType().GetProperties().Where(e => e.GetCustomAttributes<IsChangeAttribute>().Any()).ToList();
            if (propertyInfos != null && propertyInfos.Count > 0)
            {
                bool isChange = false;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    dynamic value1 = propertyInfo.GetValue(source);
                    dynamic value2 = value.GetType().GetProperty(propertyInfo.Name).GetValue(value);
                    if (value1 != value2)
                    {
                        propertyInfo.SetValue(source, value2);
                        isChange = true;
                    }
                }

                return isChange;
            }
            else
                throw new MissingFieldException("Missing ChangeCheckAttribute Fields");
        }
    }
}
