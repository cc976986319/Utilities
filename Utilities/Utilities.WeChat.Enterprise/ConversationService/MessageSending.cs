using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.Microsoft;

namespace Utilities.WeChat.Enterprise.ConversationService
{
    /// <summary>
    /// 消息发送
    /// </summary>
    public class MessageSending
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="corpid">企业Id</param>
        /// <param name="corpsecret">管理组的凭证密钥</param>
        public MessageSending(string corpid, string corpsecret)
        {
            this.AccessToken = AccessToken.GetAccessToken(corpid, corpsecret);
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="accessToken">企业号的全局唯一票据</param>
        public MessageSending(AccessToken accessToken)
        {
            this.AccessToken = accessToken;
        }
        
        private AccessToken AccessToken { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public Receiver receiver { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; private set; }

        /// <summary>
        /// text消息
        /// </summary>
        public ContentBody1 text { get; set; }

        /// <summary>
        /// image消息
        /// </summary>
        public ContentBody2 image { get; set; }

        /// <summary>
        /// file消息
        /// </summary>
        public ContentBody2 file { get; set; }

        /// <summary>
        /// voice消息
        /// </summary>
        public ContentBody2 voice { get; set; }

        /// <summary>
        /// link消息
        /// </summary>
        public ContentBody3 link { get; set; }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="id">聊天编号</param>
        /// <param name="sender">发送人</param>
        /// <param name="content">消息内容</param>
        /// <param name="isSingle">类型：默认单聊</param>
        /// <returns></returns>
        public ResponseResult SendText(string id, string sender, string content, bool isSingle = true)
        {
            this.SetBaseSetting(id, sender, "text", isSingle);

            this.text = new ContentBody1() { content = content };

            return this.Sending();
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="id">聊天编号</param>
        /// <param name="sender">发送人</param>
        /// <param name="media_id">media_id，可以调用上传素材文件接口获取</param>
        /// <param name="isSingle">类型：默认单聊</param>
        public ResponseResult SendImage(string id, string sender, string media_id, bool isSingle = true)
        {
            this.SetBaseSetting(id, sender, "image", isSingle);

            this.image = new ContentBody2() { media_id = media_id };

            return this.Sending();
        }

        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="id">聊天编号</param>
        /// <param name="sender">发送人</param>
        /// <param name="media_id">media_id，可以调用上传素材文件接口获取</param>
        /// <param name="isSingle">类型：默认单聊</param>
        public ResponseResult SendFile(string id, string sender, string media_id, bool isSingle = true)
        {
            this.SetBaseSetting(id, sender, "file", isSingle);

            this.file = new ContentBody2() { media_id = media_id };

            return this.Sending();
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="id">聊天编号</param>
        /// <param name="sender">发送人</param>
        /// <param name="media_id">media_id，可以调用上传素材文件接口获取</param>
        /// <param name="isSingle">类型：默认单聊</param>
        public ResponseResult SendVoice(string id, string sender, string media_id, bool isSingle = true)
        {
            this.SetBaseSetting(id, sender, "voice", isSingle);

            this.voice = new ContentBody2() { media_id = media_id };

            return this.Sending();
        }

        /// <summary>
        /// 发送链接消息
        /// </summary>
        /// <param name="id">聊天编号</param>
        /// <param name="sender">发送人</param>
        /// <param name="title">消息标题，不超过128个字节</param>
        /// <param name="description">	消息描述，不超过512个字节</param>
        /// <param name="url">跳转的url</param>
        /// <param name="thumb_media_id">图片media_id，可以调用上传素材文件接口获取</param>
        /// <param name="isSingle">类型：默认单聊</param>
        /// <returns></returns>
        public ResponseResult SendLink(string id, string sender, string title, string description, string url, string thumb_media_id, bool isSingle = true)
        {
            this.SetBaseSetting(id, sender, "link", isSingle);

            this.link = new ContentBody3() { title = title, description = description, url = url, thumb_media_id = thumb_media_id };

            return this.Sending();
        }

        /// <summary>
        /// 设置基本配置
        /// </summary>
        /// <param name="id">聊天编号</param>
        /// <param name="sender">发送人</param>
        /// <param name="msgtype">消息类型</param>
        /// <param name="isSingle">类型：默认单聊</param>
        private void SetBaseSetting(string id, string sender, string msgtype, bool isSingle)
        {
            if (isSingle)
                this.receiver = new Receiver() { id = id, type = "single" };
            else
                this.receiver = new Receiver() { id = id, type = "group" };
            this.msgtype = msgtype;
            this.sender = sender;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        protected virtual ResponseResult Sending()
        {
            using (WebClient webclient = new WebClient())
            {
                webclient.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                byte[] responseResult = webclient.UploadData($"https://qyapi.weixin.qq.com/cgi-bin/chat/send?access_token={this.AccessToken.access_token}", "POST", this.ToBytes());
                string result = responseResult.ParseString();
                return result.ConvertTo<ResponseResult>();
            }
        }

        #region Kid Class
        /// <summary>
        /// 接收人
        /// </summary>
        public class Receiver
        {
            /// <summary>
            /// 接收人类型：single|group，分别表示：群聊|单聊
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 接收人的值，为userid|chatid，分别表示：成员id|会话id
            /// </summary>
            public string id { get; set; }
        }

        /// <summary>
        /// 内容体1
        /// </summary>
        public class ContentBody1
        {
            /// <summary>
            /// 消息内容
            /// </summary>
            public string content { get; set; }
        }

        /// <summary>
        /// 内容体2
        /// </summary>
        public class ContentBody2
        {
            /// <summary>
            /// media_id，可以调用上传素材文件接口获取。size须大于4字节
            /// </summary>
            public string media_id { get; set; }
        }

        /// <summary>
        /// 内容体3
        /// </summary>
        public class ContentBody3
        {
            /// <summary>
            /// 消息标题，不超过128个字节
            /// </summary>
            public string title { get; set; }

            /// <summary>
            /// 消息描述，不超过512个字节
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 跳转的url
            /// </summary>
            public string url { get; set; }

            /// <summary>
            /// 图片media_id，可以调用上传素材文件接口获取
            /// </summary>
            public string thumb_media_id { get; set; }
        }
        #endregion
    }
}
