using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.WeChat.Enterprise;
using Utilities.WeChat.Enterprise.Contacts;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

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
