using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.Extension.ContactsSelector.Base
{
    /// <summary>
    /// 选择器
    /// </summary>
    public abstract class Selector
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public DataType Type { get; set; }

        /// <summary>
        /// 选中
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 头像(不填写默认为腾讯默认图片)
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType : int
        {
            /// <summary>
            /// 成员
            /// </summary>
            Member = 0,
            /// <summary>
            /// 标签
            /// </summary>
            Tag = 1,
            /// <summary>
            /// 部门
            /// </summary>
            Department = 2
        }

        /// <summary>
        /// 设置头像默认
        /// </summary>
        /// <param name="avator">头像地址。如果不填，这调用开发时默认值(可能导致无效)</param>
        public abstract void SetDefaultAvator(string avator);
    }
}
