using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Contacts;

namespace Utilities.WeChat.Enterprise.Extension.ContactsSelector
{
    /// <summary>
    /// 部门选择器
    /// </summary>
    public class DeptSelector : Base.Selector
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="avator">头像地址。如果不填，这调用开发时默认值(可能导致无效)</param>
        public DeptSelector(string avator = "")
        {
            this.Type = DataType.Department;
            this.SetDefaultAvator(avator);
        }

        /// <summary>
        /// 父级标签
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 特殊(单选：顶级节点)
        /// </summary>
        public bool Special { get; set; }

        /// <summary>
        /// 设置默认头像
        /// </summary>
        /// <param name="avator">头像地址。如果不填，这调用开发时默认值(可能导致无效)</param>
        public override void SetDefaultAvator(string avator)
        {
            if (string.IsNullOrEmpty(avator))
                this.Avator = "https://res.wx.qq.com/mmocbiz/zh_CN/tmt/pc/dist/img/icon-organization-24_714a2dc7.png";
            else
                this.Avator = avator;
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="result">获取部门列表结果</param>
        /// <returns></returns>
        public static IEnumerable<DeptSelector> ConvertTo(Department.Result_List result, bool special = false)
        {
            if (result != null && result.errcode == 0)
            {
                return ConvertTo(result.department, special);
            }
            return null;
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="list"></param>
        /// <param name="special"></param>
        /// <returns></returns>
        public static IEnumerable<DeptSelector> ConvertTo(List<Department.RequestBody> list, bool special = false)
        {

            if (list != null && list.Count > 0)
            {
                foreach (var department in list)
                {
                    if (department != null)
                        yield return new DeptSelector()
                        {
                            Id = (department.id).ToString(),
                            Name = department.name,
                            ParentId = (department.parentid).ToString()
                        };
                }
            }
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="requestBody"></param>
        /// <param name="special">特殊(单选：顶级节点)</param>
        /// <returns></returns>
        public static DeptSelector ConvertTo(Department.RequestBody requestBody, bool special = false)
        {
            if (requestBody != null)
            {
                return new DeptSelector()
                {
                    Id = (requestBody.id).ToString(),
                    Name = requestBody.name,
                    ParentId = (requestBody.parentid).ToString(),
                    Special = special
                };
            }
            return null;
        }
    }
}
