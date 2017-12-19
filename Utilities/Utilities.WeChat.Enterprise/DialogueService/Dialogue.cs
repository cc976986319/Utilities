using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Utilities.WeChat.Enterprise.DialogueService.Models;
using Utilities.WeChat.Enterprise.DialogueService.Models.Message;
using Utilities.WeChat.Enterprise.DialogueService.Models.Operate;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise.DialogueService
{
    /// <summary>
    /// 会话
    /// </summary>
    public class Dialogue
    {
        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="body"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public ResponseResult Create(string accessToken, Create body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/chat/create")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 获取会话
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="chatid"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public GetDialogue Get(string accessToken, string chatid, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/chat/get")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.DownloadData($"{requestUrl}?access_token={accessToken}&chatid={chatid}");
                return bytes.ConvertTo<GetDialogue>();
            }
        }

        /// <summary>
        /// 修改会话信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="body"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public ResponseResult Update(string accessToken, Update body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/chat/update")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 退出会话
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="body"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public ResponseResult Quit(string accessToken, Quit body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/chat/quit")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 清除会话未读状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="body"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public ResponseResult ClearNotify(string accessToken, ClearNotify body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/chat/clearnotify")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }
        
        /// <summary>
        /// 发送聊天信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="body"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        /// <remarks>消息支持文本、图片、文件、语音、链接，在发送时需要区分群聊和单聊。如果接收人不存在，则发送失败。在企业IM端发送的消息，在同步到发送者的微信上时，不会有提醒。可以通过文本消息下发表情（下载微信表情转换表 http://qydev.weixin.qq.com/download/wx-emoticon.xlsx）</remarks>
        public ResponseResult Send(string accessToken, Message body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/chat/send")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<ResponseResult>();
            }
        }

        /// <summary>
        /// 设置成员新消息免打扰
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="body"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        /// <remarks>该接口可设置成员接收到的消息是否提醒。主要场景是用于对接企业im的在线状态，如成员处于在线状态时，可以设置该成员的消息免打扰。当成员离线时，关闭免打扰状态，对微信端进行提醒。</remarks>
        public Models.SendingResult SetMute(string accessToken, SetMute body, string requestUrl = "https://qyapi.weixin.qq.com/cgi-bin/chat/setmute")
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = client.UploadData($"{requestUrl}?access_token={accessToken}", "POST", body.ToBytes());
                return bytes.ConvertTo<SendingResult>();
            }
        }
    }
}
