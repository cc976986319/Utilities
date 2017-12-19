using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise.ConversationService.Models
{
    /// <summary>
    /// 会话
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="corpid">企业Id</param>
        /// <param name="corpsecret">管理组的凭证密钥</param>
        public Conversation(string corpid, string corpsecret)
        {
            this.AccessToken = AccessToken.GetAccessToken(corpid, corpsecret);
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="accessToken">企业号的全局唯一票据</param>
        public Conversation(AccessToken accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// 全局唯一签名
        /// </summary>
        private AccessToken AccessToken { get; set; }

        /// <summary>
        /// 会话id。字符串类型，最长32个字符。只允许字符0-9及字母a-zA-Z,如果值内容为64bit无符号整型：要求值范围在[1, 2 ^ 63)之间，[2 ^ 63, 2 ^ 64)为系统分配会话id区间
        /// </summary>
        public string chatid { get; set; }

        /// <summary>
        /// 会话标题
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 管理员userid，必须是该会话userlist的成员之一
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        /// 会话成员列表，成员用userid来标识。会话成员必须在3人或以上，2000人以下
        /// </summary>
        public string[] userlist { get; set; }

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="conversation">会话</param>
        /// <returns></returns>
        public static ResponseResult Create(Conversation conversation)
        {
            using (WebClient webclient = new WebClient())
            {
                webclient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] responseResult = webclient.UploadData($"https://qyapi.weixin.qq.com/cgi-bin/chat/create?access_token={conversation.AccessToken.access_token}", "Post", conversation.ToBytes());
                string result = responseResult.ParseString();
                var data = result.ConvertTo<ResponseResult>();
                return data;
            }
        }

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="chatid">会话id</param>
        /// <param name="name">会话标题</param>
        /// <param name="owner">管理员userid</param>
        /// <param name="userlist">会话成员列表</param>
        /// <returns></returns>
        public ResponseResult Create(string chatid, string name, string owner, IEnumerable<string> userlist)
        {
            this.chatid = chatid;
            this.name = name;
            this.owner = owner;
            this.userlist = userlist.ToArray();

            using (WebClient webclient = new WebClient())
            {
                webclient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] responseResult = webclient.UploadData($"https://qyapi.weixin.qq.com/cgi-bin/chat/create?access_token={this.AccessToken.access_token}", "Post", this.ToBytes());
                string result = responseResult.ParseString();
                var data = result.ConvertTo<ResponseResult>();
                return data;
            }
        }
    }
}
