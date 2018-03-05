using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Contacts;
using Utilities.WeChat.Enterprise.Extension.ContactsSelector;
using Utilities.WeChat.Enterprise.Extension.ContactsSelector.Base;
using static Utilities.WeChat.Enterprise.Application.GetResult.UserInfo;

namespace Utilities.WeChat.Enterprise.Extension
{
    /// <summary>
    /// 通讯录扩展
    /// </summary>
    public class ContactsExtension
    {
        /// <summary>
        /// 获取选择器
        /// </summary>
        /// <param name="accessTokenFunc">此处需要匿名返回一个accessToken</param>
        /// <param name="agentid">企业应用编号</param>
        /// <returns></returns>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public List<Selector> GetSelectorAll(Func<string> accessTokenFunc, int agentid)
        {
            string accessToken = accessTokenFunc();
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException("没有AccessToken。请对accessTokenFunc经行实现");

            return GetSelectorAll(accessToken, agentid);
        }

        /// <summary>
        /// 获取选择器
        /// </summary>
        /// <param name="accessTokenFunc">accessToken</param>
        /// <param name="agentid">企业应用编号</param>
        /// <returns></returns>
        public List<Selector> GetSelectorAll(string accessToken, int agentid)
        {
            List<Selector> result = null;
            if (string.IsNullOrEmpty(accessToken)) throw new NullReferenceException("请填写accessToken参数");

            Application application = new Application();
            Application.GetResult agent = application.Get(accessToken, agentid);// 获取应用信息

            if (agent != null && agent.errcode == 0)
            {
                result = new List<Selector>();
                var agent_tags = agent.allow_tags;// 标签
                if (agent_tags != null && agent_tags.tagid != null)
                {
                    var tags = GetTags(accessToken, agent_tags.tagid);
                    if (tags != null && tags.Count > 0) result.AddRange(tags);
                }
                var agent_depts = agent.allow_partys;// 部门
                if (agent_depts != null && agent_depts.partyid != null)
                {
                    var dept = GetDepartmentsAll(accessToken, agent_depts.partyid);
                    if (dept != null && dept.Count > 0) result.AddRange(dept);
                    var member = GetMembersAll(accessToken, agent_depts.partyid);
                    if (member != null && member.Count > 0) result.AddRange(member);
                }
                var agent_members = agent.allow_userinfos;// 成员
                if (agent_members != null && agent_members.user != null)
                {
                    var memeber = GetMembers(accessToken, agent_members.user);
                    if (memeber != null && memeber.Count > 0) result.AddRange(memeber);
                }

            }

            return result;
        }

        /// <summary>
        /// 获取选择器(单层)
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="agentid">应用编号</param>
        /// <param name="deptid">部门编号，不填默认为-1（默认为获取应用级，同时反馈标签和人员）</param>
        /// <returns></returns>
        public List<Selector> GetSelector(string accessToken, int agentid, int deptid = -1)
        {
            List<Selector> result = null;
            if (string.IsNullOrEmpty(accessToken)) throw new NullReferenceException("请填写accessToken参数");

            Application application = new Application();
            Application.GetResult agent = application.Get(accessToken, agentid);// 获取应用信息

            if (agent != null && agent.errcode == 0)
            {
                result = new List<Selector>();
                var agent_depts = agent.allow_partys;// 部门
                if (agent_depts != null && agent_depts.partyid != null)
                {
                    var dept = GetDepartments(accessToken, agent_depts.partyid, deptid);
                    if (dept != null && dept.Count > 0) result.AddRange(dept);
                }
                if (deptid == -1)
                {
                    var agent_tags = agent.allow_tags;// 标签
                    if (agent_tags != null && agent_tags.tagid != null)
                    {
                        var tags = GetTags(accessToken, agent_tags.tagid);
                        if (tags != null && tags.Count > 0) result.AddRange(tags);
                    }
                    var agent_members = agent.allow_userinfos;// 成员
                    if (agent_members != null && agent_members.user != null)
                    {
                        var memeber = GetMembers(accessToken, agent_members.user);
                        if (memeber != null && memeber.Count > 0) result.AddRange(memeber);
                    }
                }
                else
                {
                    var memeber = GetMembers(accessToken, deptid);
                    if (memeber != null && memeber.Count > 0) result.AddRange(memeber);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="partyid">部门编号集合</param>
        /// <returns></returns>
        static List<DeptSelector> GetDepartmentsAll(string accessToken, List<int> partyid)
        {
            if (partyid == null || partyid.Count == 0) return null;

            List<DeptSelector> depts = new List<DeptSelector>();
            Department department = new Department();
            foreach (int _partyid in partyid)
            {
                var dept = department.List(accessToken, _partyid);
                var items = DeptSelector.ConvertTo(dept).ToList();
                if (items != null && items.Count > 0)
                {
                    var item = items.FirstOrDefault(e => e.Id == _partyid.ToString());
                    if (item != null)
                        item.Special = true;// 标识为顶级节点
                    depts.AddRange(items);
                }
            }
            return depts;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="partyid">部门编号集合</param>
        /// <returns></returns>
        static List<DeptSelector> GetDepartments(string accessToken, List<int> partyid, int deptid = -1)
        {
            if (partyid == null || partyid.Count == 0) return null;

            List<DeptSelector> depts = new List<DeptSelector>();
            Department department = new Department();
            foreach (int _partyid in partyid)
            {
                var dept = department.List(accessToken, _partyid);// 部门信息
                if (dept != null && dept.errcode == 0)
                {
                    if (deptid == -1)
                    {
                        Department.RequestBody body = dept.department.FirstOrDefault(e => e.id == _partyid);
                        var item = DeptSelector.ConvertTo(body, true);
                        if (item != null)
                            depts.Add(item);
                    }
                    else
                    {
                        var bodys = dept.department.Where(e => e.parentid == deptid).ToList();
                        var items = DeptSelector.ConvertTo(bodys).ToList();
                        if (items != null && items.Count > 0)
                            depts.AddRange(items);
                    }
                }
            }
            return depts;
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="partyid"></param>
        /// <returns></returns>
        static List<MemberSelector> GetMembersAll(string accessToken, List<int> partyid)
        {
            if (partyid == null || partyid.Count == 0) return null;

            List<MemberSelector> members = new List<MemberSelector>();
            Member member = new Member();
            foreach (int _partyid in partyid)
            {
                var users = member.List(accessToken, _partyid, Member.Fetch_Child.GetAll, Member.MemberStatus.All);
                var items = MemberSelector.ConvertTo(users).ToList();
                if (items != null && items.Count > 0)
                {
                    members.AddRange(items);
                }
            }
            return members;
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="partyid"></param>
        /// <returns></returns>
        static List<MemberSelector> GetMembers(string accessToken, int deptid = -1)
        {
            List<MemberSelector> members = new List<MemberSelector>();
            Member member = new Member();
            var users = member.List(accessToken, deptid, Member.Fetch_Child.GetCurrent, Member.MemberStatus.All);
            var items = MemberSelector.ConvertTo(users).ToList();
            if (items != null && items.Count > 0)
            {
                members.AddRange(items);
            }
            return members;
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userinfos"></param>
        /// <returns></returns>
        static List<MemberSelector> GetMembers(string accessToken, List<User> userinfos)
        {
            if (userinfos == null || userinfos.Count == 0) return null;

            List<MemberSelector> members = new List<MemberSelector>();
            Member member = new Member();
            foreach (User user in userinfos)
            {
                var _member = member.Get(accessToken, user.userid);
                if (_member != null && _member.errcode == 0)
                {
                    members.Add(MemberSelector.ConvertTo(_member, true));
                }
            }
            return members;
        }

        /// <summary>
        /// 获取标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagid"></param>
        /// <returns></returns>
        static List<TagSelector> GetTags(string accessToken, List<int> tagid)
        {
            if (tagid == null || tagid.Count == 0) return null;

            Tags tags = new Tags();
            Tags.Result result = tags.FindTagsCollection(accessToken);
            if (result != null && result.errcode == 0)
            {
                var items = result.taglist.Where(e => tagid.Any(a => a == e.tagid)).ToList();
                return TagSelector.ConvertTo(items).ToList();
            }
            return null;
        }
    }
}
