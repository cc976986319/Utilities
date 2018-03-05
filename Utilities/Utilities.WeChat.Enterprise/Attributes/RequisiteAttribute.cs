using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Utilities.WeChat.Enterprise.Attributes
{
    /// <summary>
    /// 比填项特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class RequisiteAttribute : Attribute
    {
        /// <summary>
        /// 比填项实例
        /// </summary>
        public RequisiteAttribute() { }

        /// <summary>
        /// 比填项实例
        /// </summary>
        /// <param name="describe">必填描述</param>
        /// <param name="checkValueType">检查值类型，不允许填写默认值</param>
        public RequisiteAttribute(string describe = "", bool checkValueType = false)
        {
            this.Describe = describe;
            this.CheckValueType = checkValueType;
        }

        /// <summary>
        /// 检查值类型，不允许填写默认值
        /// </summary>
        public bool CheckValueType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        public static void Check<T>(T item) where T : class
        {
            List<PropertyInfo> propertyInfos = item.GetType().GetProperties().Where(e => e.GetCustomAttributes<RequisiteAttribute>().Any()).ToList();
            if (propertyInfos != null && propertyInfos.Count > 0)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    RequisiteAttribute requisite = propertyInfo.GetCustomAttributes<RequisiteAttribute>().FirstOrDefault();
                    if (!requisite.CheckValueType)
                        if (propertyInfo.PropertyType.IsValueType) continue;// 值类型跳过
                    dynamic defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;
                    dynamic value = propertyInfo.GetValue(item);
                    if (value == defaultValue)
                    {
                        if (requisite != null && !string.IsNullOrEmpty(requisite.Describe))
                            throw new FormatException($"{propertyInfo.Name}为必填字段。必填原因为:{requisite.Describe}");
                        else
                            throw new FormatException($"{propertyInfo.Name}为必填字段");
                    }
                }
            }
        }
    }
}
