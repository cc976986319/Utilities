using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Contacts;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise
{
    /// <summary>
    /// 企业应用
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 获取企业号应用
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="agentid">企业应用的id</param>
        /// <param name="requestUrl">请求地址</param>
        /// <remarks>该API用于获取企业号某个应用的基本信息，包括头像、昵称、帐号类型、认证类型、可见范围等信息</remarks>
        /// <returns></returns>
        public GetResult Get(string accessToken, int agentid, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/agent/get")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&agentid={agentid}");
                return bytes.ConvertTo<GetResult>();
            }
        }

        /// <summary>
        /// 设置企业号应用
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="body">应用信息</param>
        /// <param name="requestUrl">请求地址</param>
        /// <remarks>该API用于设置企业应用的选项设置信息，如：地理位置上报等。第三方服务商不能调用该接口设置授权的主页型应用。</remarks>
        /// <returns></returns>
        public ResponseResult Set(string accessToken, Body body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/agent/set")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 获取应用概况列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="requestUrl">请求地址</param>
        /// <remarks>该API 用于获取secret所在管理组内的应用概况，会返回管理组内应用的id及名称、头像等信息</remarks>
        /// <returns></returns>
        public ListResult List(string accessToken, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/agent/list")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}");
                return bytes.ConvertTo<ListResult>();
            }
        }

        /// <summary>
        /// 范围(调用频率过大，不建议调用此方法。或做缓存处理)
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="agentid">企业应用的id</param>
        /// <returns></returns>
        public ApplicationBody Scope(string accessToken, int agentid)
        {
            var data = this.Get(accessToken, agentid);
            ApplicationBody body = new ApplicationBody(accessToken, data);
            body.Install();
            return body;
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public class GetResult : ResponseResult
        {
            /// <summary>
            /// 企业应用id
            /// </summary>
            public string agentid { get; set; }

            /// <summary>
            /// 企业应用名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 企业应用方形头像
            /// </summary>
            public string square_logo_url { get; set; }

            /// <summary>
            /// 企业应用圆形头像
            /// </summary>
            public string round_logo_url { get; set; }

            /// <summary>
            /// 企业应用详情
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 企业应用可见范围（人员），其中包括userid和关注状态state
            /// </summary>
            public UserInfo allow_userinfos { get; set; }

            /// <summary>
            /// 企业应用可见范围（部门）
            /// </summary>
            public Party allow_partys { get; set; }

            /// <summary>
            /// 企业应用可见范围（标签）
            /// </summary>
            public Tags allow_tags { get; set; }

            /// <summary>
            /// 企业应用是否被禁用
            /// </summary>
            public int close { get; set; }

            /// <summary>
            /// 企业应用可信域名
            /// </summary>
            public string redirect_domain { get; set; }

            /// <summary>
            /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
            /// </summary>
            public LocationFlag report_location_flag { get; set; }

            /// <summary>
            /// 是否接收用户变更通知。0：不接收；1：接收
            /// </summary>
            public InformType isreportuser { get; set; }

            /// <summary>
            /// 是否上报用户进入应用事件。0：不接收；1：接收
            /// </summary>
            public InformType isreportenter { get; set; }

            /// <summary>
            /// 应用类型。1：消息型；2：主页型
            /// </summary>
            public ApplicationType type { get; set; }

            /// <summary>
            /// 关联会话url
            /// </summary>
            public string chat_extension_url { get; set; }

            /// <summary>
            /// 用户信息
            /// </summary>
            public class UserInfo
            {
                public List<User> user { get; set; }

                /// <summary>
                /// 成员信息
                /// </summary>
                public class User
                {
                    public string userid { get; set; }

                    public string status { get; set; }
                }
            }

            /// <summary>
            /// 部门信息
            /// </summary>
            public class Party
            {
                public List<int> partyid { get; set; }
            }

            /// <summary>
            /// 标签信息
            /// </summary>
            public class Tags
            {
                public List<int> tagid { get; set; }
            }
        }

        /// <summary>
        /// 列表结果
        /// </summary>
        public class ListResult : ResponseResult
        {
            public List<Agent> agentlist { get; set; }

            public class Agent
            {
                /// <summary>
                /// 应用id
                /// </summary>
                public int agentid { get; set; }

                /// <summary>
                /// 应用名称
                /// </summary>
                public string name { get; set; }

                /// <summary>
                /// 方形头像url
                /// </summary>
                public string square_logo_url { get; set; }

                /// <summary>
                /// 圆形头像url
                /// </summary>
                public string round_logo_url { get; set; }
            }
        }

        /// <summary>
        /// 应用消息
        /// </summary>
        public class Body
        {
            /// <summary>
            /// 企业应用的id
            /// </summary>
            public string agentid { get; set; }

            /// <summary>
            /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
            /// </summary>
            public LocationFlag report_location_flag { get; set; }

            /// <summary>
            /// 企业应用头像的mediaid，通过多媒体接口上传图片获得mediaid，上传后会自动裁剪成方形和圆形两个头像
            /// </summary>
            public string logo_mediaid { get; set; }

            /// <summary>
            /// 企业应用名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 企业应用详情
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 企业应用可信域名
            /// </summary>
            public string redirect_domain { get; set; }

            /// <summary>
            /// 是否接收用户变更通知。0：不接收；1：接收。
            /// </summary>
            public InformType isreportuser { get; set; }

            /// <summary>
            /// 是否上报用户进入应用事件。0：不接收；1：接收。
            /// </summary>
            public InformType isreportenter { get; set; }

            /// <summary>
            /// 主页型应用url。url必须以http或者https开头。消息型应用无需该参数
            /// </summary>
            public string home_url { get; set; }

            /// <summary>
            /// 关联会话url。设置该字段后，企业会话"+"号将出现该应用，点击应用可直接跳转到此url，支持jsapi向当前会话发送消息。
            /// </summary>
            public string chat_extension_url { get; set; }
        }

        /// <summary>
        /// 地理位置标记
        /// </summary>
        public enum LocationFlag : int
        {
            /// <summary>
            /// 不上报
            /// </summary>
            NotInform = 0,
            /// <summary>
            /// 进入上报
            /// </summary>
            IntoInform = 1,
            /// <summary>
            /// 持续上报
            /// </summary>
            ContinueInform = 2
        }

        /// <summary>
        /// 通知类型
        /// </summary>
        public enum InformType : int
        {
            /// <summary>
            /// 不接受
            /// </summary>
            NotAccept = 0,
            /// <summary>
            /// 接受
            /// </summary>
            Accept = 1
        }

        /// <summary>
        /// 应用类型
        /// </summary>
        public enum ApplicationType : int
        {
            /// <summary>
            /// 消息型
            /// </summary>
            Message = 1,
            /// <summary>
            /// 主页型
            /// </summary>
            HomePage = 2
        }

        /// <summary>
        /// 应用消息体
        /// </summary>
        public class ApplicationBody
        {
            GetResult GetResult { get; set; }
            string AccessToken { get; set; }

            public ApplicationBody(string accessToken, GetResult result)
            {
                this.GetResult = result;
                this.AccessToken = accessToken;
            }

            #region ApplicationInfo
            /// <summary>
            /// 企业应用id
            /// </summary>
            public string agentid { get { return this.GetResult.agentid; } }

            /// <summary>
            /// 企业应用名称
            /// </summary>
            public string name { get { return this.GetResult.name; } }

            /// <summary>
            /// 企业应用方形头像
            /// </summary>
            public string square_logo_url { get { return this.GetResult.square_logo_url; } }

            /// <summary>
            /// 企业应用圆形头像
            /// </summary>
            public string round_logo_url { get { return this.GetResult.round_logo_url; } }

            /// <summary>
            /// 企业应用详情
            /// </summary>
            public string description { get { return this.GetResult.description; } }

            /// <summary>
            /// 企业应用是否被禁用
            /// </summary>
            public int close { get { return this.GetResult.close; } }

            /// <summary>
            /// 企业应用可信域名
            /// </summary>
            public string redirect_domain { get { return this.GetResult.redirect_domain; } }

            /// <summary>
            /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
            /// </summary>
            public LocationFlag report_location_flag { get { return this.GetResult.report_location_flag; } }

            /// <summary>
            /// 是否接收用户变更通知。0：不接收；1：接收
            /// </summary>
            public InformType isreportuser { get { return this.GetResult.isreportuser; } }

            /// <summary>
            /// 是否上报用户进入应用事件。0：不接收；1：接收
            /// </summary>
            public InformType isreportenter { get { return this.GetResult.isreportenter; } }

            /// <summary>
            /// 应用类型。1：消息型；2：主页型
            /// </summary>
            public ApplicationType type { get { return this.GetResult.type; } }

            /// <summary>
            /// 关联会话url
            /// </summary>
            public string chat_extension_url { get { return this.GetResult.chat_extension_url; } }
            #endregion

            /// <summary>
            /// 部门信息
            /// </summary>
            public List<DepartmentBody> Departments { get; set; }

            /// <summary>
            /// 标签信息
            /// </summary>
            public List<Tags.Result.Tag> Tags { get; set; }

            /// <summary>
            /// 成员
            /// </summary>
            public List<Member.MemberBody> Members { get; set; }

            /// <summary>
            /// 部门
            /// </summary>
            public class DepartmentBody : Contacts.Department.RequestBody
            {
                /// <summary>
                /// 成员
                /// </summary>
                public List<Member.MemberBody> Members { get; set; }

                /// <summary>
                /// 子级部门
                /// </summary>
                public List<DepartmentBody> Kids { get; set; }

                /// <summary>
                /// 转换
                /// </summary>
                /// <param name="body"></param>
                /// <param name="members"></param>
                /// <returns></returns>
                public static DepartmentBody ConvertTo(Department.RequestBody body, List<Member.MemberBody> members)
                {
                    if (body == null) throw new ArgumentNullException("body paramter is null value");
                    return new DepartmentBody()
                    {
                        id = body.id,
                        Members = members,
                        name = body.name,
                        order = body.order,
                        parentid = body.parentid
                    };
                }
            }

            /// <summary>
            /// 获取所有部门
            /// </summary>
            /// <returns></returns>
            List<Department.RequestBody> GetAllDepartment()
            {
                List<Department.RequestBody> departments = null;
                if (this.GetResult != null && this.GetResult.errcode == 0 && this.GetResult.allow_partys != null && this.GetResult.allow_partys.partyid.Any())
                {
                    departments = new List<Department.RequestBody>();
                    Department department = new Department();
                    foreach (int partyid in GetResult.allow_partys.partyid)
                    {
                        var data = department.List(this.AccessToken, partyid);
                        if (data.errcode == 0 && data.department != null && data.department.Count > 0)
                            departments.AddRange(data.department);
                    }
                }
                return departments;
            }

            /// <summary>
            /// 获取所有成员
            /// </summary>
            /// <returns></returns>
            List<Member.MemberBody> GetAllMember()
            {
                List<Member.MemberBody> members = null;
                if (this.GetResult != null && this.GetResult.errcode == 0 && this.GetResult.allow_partys != null && this.GetResult.allow_partys.partyid.Any())
                {
                    members = new List<Member.MemberBody>();
                    Member member = new Member();
                    foreach (int partyid in GetResult.allow_partys.partyid)
                    {
                        var data = member.List(this.AccessToken, partyid, Member.Fetch_Child.GetAll, Member.MemberStatus.All);
                        if (data.errcode == 0 && data.userlist != null && data.userlist.Count > 0)
                            members.AddRange(data.userlist);
                    }
                }
                return members;
            }

            DepartmentBody CreateDepartemnt(int deptid, List<Department.RequestBody> departments, List<Member.MemberBody> members)
            {
                DepartmentBody body = null;
                var dept = departments.FirstOrDefault(e => e.id == deptid);
                if (dept != null)
                {
                    var member = members.Where(e => e.department.Contains(deptid)).ToList();
                    body = DepartmentBody.ConvertTo(dept, member);

                    List<int> deptids = departments.Where(e => e.parentid == deptid).Select(e => e.id).ToList();
                    if (deptids != null && deptids.Count > 0)
                    {
                        body.Kids = new List<DepartmentBody>();
                        foreach (int id in deptids)
                        {
                            var department = this.CreateDepartemnt(id, departments, members);
                            body.Kids.Add(department);
                        }
                    }
                }
                return body;
            }

            /// <summary>
            /// 初始化数据
            /// </summary>
            public void Install()
            {
                if (this.GetResult != null && this.GetResult.errcode == 0)
                {
                    if (this.GetResult.allow_partys != null && this.GetResult.allow_partys.partyid != null && this.GetResult.allow_partys.partyid.Count > 0)
                    {
                        // 所有部门
                        var allDepartment = this.GetAllDepartment();
                        var allMember = this.GetAllMember();
                        this.Departments = new List<DepartmentBody>();
                        foreach (int partyid in this.GetResult.allow_partys.partyid)
                        {
                            DepartmentBody body = this.CreateDepartemnt(partyid, allDepartment, allMember);
                            this.Departments.Add(body);
                        }
                    }
                    if (this.GetResult.allow_tags != null && this.GetResult.allow_tags.tagid != null && this.GetResult.allow_tags.tagid.Count > 0)
                    {
                        Tags tags = new Tags();
                        var allTags = tags.FindTagsCollection(this.AccessToken);
                        this.Tags = new List<Contacts.Tags.Result.Tag>();
                        foreach (int tagid in this.GetResult.allow_tags.tagid)
                        {
                            var tag = allTags.taglist.FirstOrDefault(e => e.tagid == tagid);
                            this.Tags.Add(tag);
                        }
                    }
                    if (this.GetResult.allow_userinfos != null && this.GetResult.allow_userinfos.user != null && this.GetResult.allow_userinfos.user.Count > 0)
                    {
                        this.Members = new List<Member.MemberBody>();
                        Member member = new Member();
                        foreach (var user in this.GetResult.allow_userinfos.user)
                        {
                            var _member = member.Get(this.AccessToken, user.userid);
                            this.Members.Add(Member.MemberBody.ConvertTo(_member));
                        }
                    }
                }
            }
        }
    }
}
