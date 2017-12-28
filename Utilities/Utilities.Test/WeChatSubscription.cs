using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.WeChat.Subscription.Configs;
using Utilities.WeChat.Subscription.Configs.Base;
using System.Net;
using System.Text;

namespace Utilities.Test
{
    /// <summary>
    /// 微信订阅号
    /// </summary>
    [TestClass]
    public class WeChatSubscription
    {
        [TestMethod]
        public void GetJsConfig()
        {
            // 125.70.253.16
            // invalid ip 221.237.157.35, not in whitelist hint: [_1f44a0230nfo1]
            // {"access_token":"0KUkc4o8IGfDxPreHFJDkqMTzeWB1-PPTluo5FH2FREGrzRfiPt44an50vO8tb_l6GSmQ6mhGNc7ZlabNxU6gAxEjyPS71WcBLBTCqcRhSAXFKgAGAGOT","expires_in":7200}
            //AccessToken token = new AccessToken();
            //var result = token.GetAccessToken("wxa7d73b23e23e0de7", "ea863d03c28dea05849fb2d3f8db45fa");

            Config config = new Js_SDKConfig("0KUkc4o8IGfDxPreHFJDkqMTzeWB1-PPTluo5FH2FREGrzRfiPt44an50vO8tb_l6GSmQ6mhGNc7ZlabNxU6gAxEjyPS71WcBLBTCqcRhSAXFKgAGAGOT", "http://rmb.centaline-sc.com/static/centaline_club/s1/wx/my-discount.html?id=c376e43e-edfb-4e34-9e76-567a0bee8a2c", "wxa7d73b23e23e0de7", true, true);
        }

        [TestMethod]
        public void GetAccessToken()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxa7d73b23e23e0de7&secret=ea863d03c28dea05849fb2d3f8db45fa");
                string jsonstr = Encoding.UTF8.GetString(bytes);
            }
        }

        [TestMethod]
        public void GetJsapi_ticket()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=0KUkc4o8IGfDxPreHFJDkqMTzeWB1-PPTluo5FH2FREGrzRfiPt44an50vO8tb_l6GSmQ6mhGNc7ZlabNxU6gAxEjyPS71WcBLBTCqcRhSAXFKgAGAGOT&type=jsapi");
                string jsonstr = Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
