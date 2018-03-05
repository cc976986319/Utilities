using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Contacts;
using static Utilities.WeChat.Enterprise.Contacts.Tags.Result;

namespace Utilities.WeChat.Enterprise.Extension.ContactsSelector
{
    /// <summary>
    /// 标签选择器
    /// </summary>
    public class TagSelector : Base.Selector
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="avator">头像地址</param>
        public TagSelector(string avator = "")
        {
            this.Type = DataType.Tag;
            this.SetDefaultAvator(avator);
        }

        /// <summary>
        /// 设置默认头像
        /// </summary>
        /// <param name="avator">头像地址。</param>
        public override void SetDefaultAvator(string avator)
        {
            if (string.IsNullOrEmpty(avator))
                this.Avator = "https://res.wx.qq.com/mmocbiz/zh_CN/tmt/pc/dist/img/icon_tag_80_b7b32967.png";
            else
                this.Avator = avator;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="list">标签列表</param>
        /// <returns></returns>
        public static IEnumerable<TagSelector> ConvertTo(List<Tag> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (Tag tag in list)
                {
                    if (tag != null)
                        yield return new TagSelector()
                        {
                            Id = (tag.tagid).ToString(),
                            Name = tag.tagname
                        };
                }
            }
        }
    }
}
