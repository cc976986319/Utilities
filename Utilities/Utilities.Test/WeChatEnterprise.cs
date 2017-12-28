using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.WeChat.Enterprise;
using Utilities.WeChat.Enterprise.Contacts;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Utilities.Test
{
    /// <summary>
    /// 微信企业号测试
    /// </summary>
    [TestClass]
    public class WeChatEnterprise
    {
        /// <summary>
        /// 获取标签
        /// </summary>
        [TestMethod]
        public void GetTags()
        {
            AccessToken accessToken = AccessToken.GetAccessToken("wxf4cdff3841975b54", "xBV8VWdvKYb8f4EyIbfYB6zPR0IYwD12JyiGjCADPWrVAGRXVGiANrKAOa1Jm21S");
            Tags tags = new Tags();
            var data = tags.FindTagsCollection(accessToken.access_token);
        }

        /// <summary>
        /// 创建标签
        /// </summary>
        [TestMethod]
        public void CreateTag()
        {
            AccessToken accessToken = AccessToken.GetAccessToken("wxf4cdff3841975b54", "xBV8VWdvKYb8f4EyIbfYB6zPR0IYwD12JyiGjCADPWrVAGRXVGiANrKAOa1Jm21S");
            Tags tags = new Tags();
            var data = tags.CreateTags(accessToken.access_token, new Tags.RequestBody_Tags()
            {
                //tagid = 0,
                tagname = "房友圈拓盘专属",
            });
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        [TestMethod]
        public void GetMember()
        {
            AccessToken accessToken = AccessToken.GetAccessToken("wxf4cdff3841975b54", "xBV8VWdvKYb8f4EyIbfYB6zPR0IYwD12JyiGjCADPWrVAGRXVGiANrKAOa1Jm21S");
            Tags tags = new Tags();
            var data = tags.FindTagsMember(accessToken.access_token, 9);
        }

        /// <summary>
        /// 获取应用详细信息
        /// </summary>
        [TestMethod]
        public void ApplicationList()
        {
            AccessToken accessToken = AccessToken.GetAccessToken("wxf4cdff3841975b54", "xBV8VWdvKYb8f4EyIbfYB6zPR0IYwD12JyiGjCADPWrVAGRXVGiANrKAOa1Jm21S");
            Application application = new Application();
            var result = application.List(accessToken.access_token);
            List<Application.GetResult> list = new List<Application.GetResult>();
            if (result.errcode == 0)
            {
                foreach (var agent in result.agentlist)
                {
                    var data = application.Get(accessToken.access_token, agent.agentid);
                    list.Add(data);
                }
                string value = new JavaScriptSerializer().Serialize(list);
            }
        }

        [TestMethod]
        public void GetUserList()
        {
            AccessToken accessToken = AccessToken.GetAccessToken("wxf4cdff3841975b54", "xBV8VWdvKYb8f4EyIbfYB6zPR0IYwD12JyiGjCADPWrVAGRXVGiANrKAOa1Jm21S");
            Member member = new Member();
            var data = member.SimpleList(accessToken.access_token, "1", Member.Fetch_Child.GetAll, Member.MemberStatus.Disabled, Member.MemberStatus.NotAttention);
        }

        [TestMethod]
        public void Test()
        {
            DateTime date = DateTime.Now;


            var data = new JavaScriptSerializer().Serialize(date);
            data = Regex.Replace(data, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
        }
    }
}
