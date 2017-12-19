using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChat.Enterprise.DialogueService.Models.Message.Types;

namespace Utilities.WeChat.Enterprise.DialogueService.Models.Message
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 私有实例
        /// </summary>
        private Message() { }

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
        /// 文本消息
        /// </summary>
        public Text text { get; private set; }

        /// <summary>
        /// 图片消息
        /// </summary>
        public Image image { get; private set; }

        /// <summary>
        /// 文件消息
        /// </summary>
        public File file { get; private set; }

        /// <summary>
        /// 语音消息
        /// </summary>
        public Voice voice { get; private set; }

        /// <summary>
        /// 链接消息
        /// </summary>
        public Link link { get; private set; }

        /// <summary>
        /// 接收信息
        /// </summary>
        public class Receiver
        {
            public Receiver(string id, bool isSingle = true)
            {
                this.id = id;
                this.type = isSingle ? "single" : "group";
            }

            public string type { get; set; }

            public string id { get; set; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        static Message Install(Receiver receiver, string sender)
        {
            return new Message()
            {
                receiver = receiver,
                sender = sender
            };
        }

        /// <summary>
        /// 文本消息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static Message Text(Text text, Receiver receiver, string sender)
        {
            Message message = Install(receiver, sender);
            message.text = text;
            return message;
        }

        /// <summary>
        /// 图片消息
        /// </summary>
        /// <param name="image"></param>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static Message Image(Image image, Receiver receiver, string sender)
        {
            Message message = Install(receiver, sender);
            message.image = image;
            return message;
        }

        /// <summary>
        /// 文件消息
        /// </summary>
        /// <param name="file"></param>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static Message File(File file, Receiver receiver, string sender)
        {
            Message message = Install(receiver, sender);
            message.file = file;
            return message;
        }

        /// <summary>
        /// 语音消息
        /// </summary>
        /// <param name="voice"></param>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static Message Voice(Voice voice, Receiver receiver, string sender)
        {
            Message message = Install(receiver, sender);
            message.voice = voice;
            return message;
        }

        /// <summary>
        /// 链接消息
        /// </summary>
        /// <param name="link"></param>
        /// <param name="receiver"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static Message Link(Link link, Receiver receiver, string sender)
        {
            Message message = Install(receiver, sender);
            message.link = link;
            return message;
        }
    }
}
