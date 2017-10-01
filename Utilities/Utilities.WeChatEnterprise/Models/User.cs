using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChatEnterprise.ENUM;

namespace Utilities.WeChatEnterprise.Models
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// 成员UserID。对应管理端的帐号
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 成员所属部门id列表
        /// </summary>
        public int[] department { get; set; }

        /// <summary>
        /// 职位信息
        /// </summary>
        public string position { get; set; }

        /// <summary>
        /// 手机号码。第三方仅通讯录套件可获取
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 性别。0表示未定义，1表示男性，2表示女性
        /// </summary>
        public Sex gender { get; set; }

        /// <summary>
        /// 邮箱。第三方仅通讯录套件可获取
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string weixinid { get; set; }

        /// <summary>
        /// 头像url。注：如果要获取小图将url最后的"/0"改成"/64"即可
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 头像url（小图）
        /// </summary>
        public string avatar_small
        {
            get
            {
                if (string.IsNullOrEmpty(this.avatar)) return null;
                return this.avatar + "64";
            }
        }

        /// <summary>
        /// 关注状态: 1=已关注，2=已冻结，4=未关注
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 扩展属性。第三方仅通讯录套件可获取
        /// </summary>
        public Extattr extattr { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public class Extattr
        {
            /// <summary>
            /// 扩展列表
            /// </summary>
            public Attr[] attrs { get; set; }

            public class Attr
            {
                /// <summary>
                /// 扩展名称
                /// </summary>
                public string name { get; set; }

                /// <summary>
                /// 填充值
                /// </summary>
                public string value { get; set; }
            }
        }
    }
}
