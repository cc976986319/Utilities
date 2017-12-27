using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Ioc.Attributes
{
    /// <summary>
    /// 设置接口实现类自定义标注属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class MappingAttribute : Attribute
    {
        /// <summary>
        /// 类地址(相对程序集的绝对路径)
        /// </summary>
        public string FullTypeName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describtion { get; set; }

        /// <summary>
        /// 设置实现类自定义标注属性
        /// </summary>
        /// <param name="fullName">类地址(相对程序集的绝对路径)</param>
        /// <param name="describtion">描述</param>
        public MappingAttribute(string fullName, string describtion)
        {
            this.FullTypeName = fullName;
            this.Describtion = describtion;
        }
    }
}
