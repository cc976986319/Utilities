using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise.Contacts
{
    /// <summary>
    /// 成员
    /// </summary>
    public class Member
    {
        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="body">成员信息</param>
        /// <param name="requestUrl">请求路径</param>
        /// <returns></returns>
        public ResponseResult Create(string accessToken, MemberBody body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/create")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="body">成员信息</param>
        /// <param name="requestUrl">请求路径</param>
        /// <returns></returns>
        public ResponseResult Update(string accessToken, MemberBody body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/update")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">成员UserID。对应管理端的帐号</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public ResponseResult Delete(string accessToken, string userId, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/delete")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&userid={userId}");
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="body">要删除的成员列表</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public ResponseResult BatchDelete(string accessToken, BatchDeleteBody body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">成员UserID。对应管理端的帐号</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public Result Get(string accessToken, string userId, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/get")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&userid={userId}");
                return bytes.ConvertTo<Result>();
            }
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部成员，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加,未填写则默认为4</param>
        /// <returns></returns>
        public Result_SimpleList SimpleList(string accessToken, int departmentId, Fetch_Child fetchChild, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist", params MemberStatus[] status)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&department_id={departmentId}&fetch_child={(int)fetchChild}&status={status.ToIntArray().Superposition()}");
                return bytes.ConvertTo<Result_SimpleList>();
            }
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部成员，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加,未填写则默认为4</param>
        /// <returns></returns>
        public Result_SimpleList SimpleList(string accessToken, int departmentId, Fetch_Child fetchChild, params MemberStatus[] status)
        {
            return this.SimpleList(accessToken, departmentId, fetchChild, "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist", status);
        }

        /// <summary>
        /// 获取部门成员(详情)
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部成员，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加,未填写则默认为4</param>
        /// <returns></returns>
        public Result_List List(string accessToken, int departmentId, Fetch_Child fetchChild, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist", params MemberStatus[] status)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&department_id={departmentId}&fetch_child={(int)fetchChild}&status={status.ToIntArray().Superposition()}");
                return bytes.ConvertTo<Result_List>();
            }
        }

        /// <summary>
        /// 获取部门成员(详情)
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部成员，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加,未填写则默认为4</param>
        /// <returns></returns>
        public Result_List List(string accessToken, int departmentId, Fetch_Child fetchChild, params MemberStatus[] status)
        {
            return this.List(accessToken, departmentId, fetchChild, "https://qyapi.weixin.qq.com/cgi-bin/user/list", status);
        }

        /// <summary>
        /// 成员数据
        /// </summary>
        public class MemberBody
        {
            /// <summary>
            /// 成员UserID。对应管理端的帐号，企业内必须唯一。不区分大小写，长度为1~64个字节
            /// </summary>
            public string userid { get; set; }

            /// <summary>
            /// 成员名称。长度为1~64个字节
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 成员所属部门id列表,不超过20个
            /// </summary>
            public List<int> department { get; set; }

            /// <summary>
            /// 职位信息。长度为0~64个字节
            /// </summary>
            public string position { get; set; }

            /// <summary>
            /// 手机号码。企业内必须唯一，mobile/weixinid/email三者不能同时为空
            /// </summary>
            public string mobile { get; set; }

            /// <summary>
            /// 性别。1表示男性，2表示女性
            /// </summary>
            public string gender { get; set; }

            /// <summary>
            /// 邮箱。长度为0~64个字节。企业内必须唯一
            /// </summary>
            public string email { get; set; }

            /// <summary>
            /// 微信号。企业内必须唯一。（注意：是微信号，不是微信的名字）
            /// </summary>
            public string weixinid { get; set; }

            /// <summary>
            /// 启用/禁用成员。1表示启用成员，0表示禁用成员
            /// </summary>
            public int enable { get; set; }

            /// <summary>
            /// 成员头像的mediaid，通过多媒体接口上传图片获得的mediaid
            /// </summary>
            public string avatar_mediaid { get; set; }

            /// <summary>
            /// 扩展属性。扩展属性需要在WEB管理端创建后才生效，否则忽略未知属性的赋值
            /// </summary>
            public Extension extattr { get; set; }

            /// <summary>
            /// 转换
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public static MemberBody ConvertTo(Result result)
            {
                string value = result.ToString();

                return value.ConvertTo<MemberBody>();
            }
        }

        /// <summary>
        /// 扩展
        /// </summary>
        public class Extension
        {
            /// <summary>
            /// 扩展属性集合
            /// </summary>
            public List<Attr> attrs { get; set; }

            /// <summary>
            /// 扩展属性
            /// </summary>
            public class Attr
            {
                /// <summary>
                /// 名称
                /// </summary>
                public string name { get; set; }

                /// <summary>
                /// 值
                /// </summary>
                public string value { get; set; }
            }
        }

        /// <summary>
        /// 批量删除对象
        /// </summary>
        public class BatchDeleteBody
        {
            /// <summary>
            /// 成员UserID列表。对应管理端的帐号。（最多支持200个）
            /// </summary>
            public List<string> useridlist { get; set; }
        }

        /// <summary>
        /// 结果
        /// </summary>
        public class Result : ResponseResult
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
            public List<int> department { get; set; }

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
            public string gender { get; set; }

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
            /// 关注状态: 1=已关注，2=已禁用，4=未关注
            /// </summary>
            public int status { get; set; }

            /// <summary>
            /// 扩展属性。第三方仅通讯录套件可获取
            /// </summary>
            public Extension extattr { get; set; }
        }

        /// <summary>
        /// 部门成员结果
        /// </summary>
        public class Result_SimpleList : ResponseResult
        {
            /// <summary>
            /// 成员列表
            /// </summary>
            public List<User> userlist { get; set; }

        }

        /// <summary>
        /// 部门成员结果(详情)
        /// </summary>
        public class Result_List : ResponseResult
        {
            /// <summary>
            /// 成员列表
            /// </summary>
            public List<MemberBody> userlist { get; set; }
        }

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
            /// 成员所属部门
            /// </summary>
            public List<int> department { get; set; }
        }

        /// <summary>
        /// 成员获取
        /// </summary>
        public enum Fetch_Child : int
        {
            /// <summary>
            /// 获取当前部门
            /// </summary>
            GetCurrent = 0,
            /// <summary>
            /// 获取全部(包含子部门)
            /// </summary>
            GetAll = 1
        }

        /// <summary>
        /// 成员类型
        /// </summary>
        public enum MemberStatus : int
        {
            All = 0,
            /// <summary>
            /// 关注
            /// </summary>
            Attention = 1,
            /// <summary>
            /// 禁用
            /// </summary>
            Disabled = 2,
            /// <summary>
            /// 未关注
            /// </summary>
            NotAttention = 4
        }
    }
}
