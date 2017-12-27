using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mongo.Attributes
{
    /// <summary>
    /// 文档集名称特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CollectionAttribute : Attribute
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public CollectionAttribute() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">文档名称</param>
        public CollectionAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">文档名称</param>
        /// <param name="describe">描述</param>
        public CollectionAttribute(string name, string describe)
        {
            this.Name = name;
            this.Describe = describe;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
    }
}
