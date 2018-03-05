using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Contacts;
using System.Web.Script.Serialization;

namespace Utilities.WeChat.Enterprise.Extension.ContactsSelector
{
    /// <summary>
    /// 成员选择器
    /// </summary>
    public class MemberSelector : Base.Selector
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="avator">头像地址。如果不填，这调用开发时默认值(可能导致无效)</param>
        public MemberSelector(string avator = "")
        {
            this.Type = DataType.Member;
            this.SetDefaultAvator(avator);
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        public List<string> DeptId { get; set; }

        ///// <summary>
        ///// 部门编号
        ///// </summary>
        //public string _DeptId { get; set; }

        /// <summary>
        /// 特殊(单选：单独选择的成员)
        /// </summary>
        public bool Special { get; set; }

        /// <summary>
        /// 设置默认头像
        /// </summary>
        /// <param name="avator">头像地址。如果不填，这调用开发时默认值(可能导致无效)</param>
        public override void SetDefaultAvator(string avator)
        {
            if (string.IsNullOrEmpty(avator))
                this.Avator = "http://shp.qpic.cn/bizmp/9oicVyrquRicPdaa1M5bZZfpMj91zGHP36R1m3y3aca9uwsUlGA0CHQw/64";
            else
                this.Avator = avator;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="result">成员结果</param>
        /// <param name="special">特殊(单选：单独选择的成员)</param>
        /// <returns></returns>
        public static MemberSelector ConvertTo(Member.Result result, bool special = false)
        {
            if (result != null && result.errcode == 0)
            {
                return new MemberSelector(result.avatar)
                {
                    DeptId = result.department.Select(e => e.ToString()).ToList(),
                    //_DeptId = new JavaScriptSerializer().Serialize(dept),
                    Id = result.userid,
                    Name = result.name,
                    //Avator = result.avatar,
                    Special = special
                };
            }
            return null;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="result">成员结果</param>
        /// <param name="special">特殊(单选：单独选择的成员)</param>
        /// <returns></returns>
        public static IEnumerable<MemberSelector> ConvertTo(Member.Result_List result, bool special = false)
        {
            if (result != null && result.errcode == 0)
            {
                if (result.userlist != null && result.userlist.Count > 0)
                {
                    foreach (Member.Result item in result.userlist)
                    {
                        if (item != null)
                        {
                            yield return new MemberSelector(item.avatar)
                            {
                                Id = item.userid,
                                DeptId = item.department.Select(e => e.ToString()).ToList(),
                                //_DeptId = new JavaScriptSerializer().Serialize(dept),
                                Name = item.name,
                                //Avator = item.avatar,
                                Special = special
                            };
                        }
                    }
                }
            }
        }
    }
}
