using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.Models
{
    /// <summary>
    /// 应用消息
    /// </summary>
    public class ApplicationMessage
    {
        /// <summary>
        /// 成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string toparty { get; set; }

        /// <summary>
        /// 标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string totag { get; set; }

        /// <summary>
        /// 消息类型，此时固定为：text （支持消息型应用跟主页型应用）
        /// </summary>
        public string msgtype { get; set; }

        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        public int agentid { get; set; }

        /// <summary>
        /// 	表示是否是保密消息，0表示否，1表示是，默认0
        /// </summary>
        public int safe { get; set; }

        #region 文本消息
        public Text text { get; set; }

        public class Text
        {
            /// <summary>
            /// 消息内容，最长不超过2048个字节，注意：主页型应用推送的文本消息在微信端最多只显示20个字（包含中英文）
            /// </summary>
            public string content { get; set; }
        }
        #endregion

        #region 图片消息
        public Image image { get; set; }

        public class Image
        {
            /// <summary>
            /// 图片媒体文件id，可以调用上传临时素材或者永久素材接口获取,永久素材media_id必须由发消息的应用创建
            /// </summary>
            public string media_id { get; set; }
        }
        #endregion

        #region 音频消息
        public Voice voice { get; set; }

        public class Voice
        {
            /// <summary>
            /// 语音文件id，可以调用上传临时素材或者永久素材接口获取
            /// </summary>
            public string media_id { get; set; }
        }
        #endregion

        #region 视频消息
        public Video video { get; set; }

        public class Video
        {
            /// <summary>
            /// 视频媒体文件id，可以调用上传临时素材或者永久素材接口获取
            /// </summary>
            public string media_id { get; set; }

            /// <summary>
            /// 视频消息的标题，不超过128个字节，超过会自动截断
            /// </summary>
            public string title { get; set; }

            /// <summary>
            /// 视频消息的描述，不超过512个字节，超过会自动截断
            /// </summary>
            public string description { get; set; }
        }
        #endregion

        #region 文件消息
        public File file { get; set; }

        public class File
        {
            /// <summary>
            /// 媒体文件id，可以调用上传临时素材或者永久素材接口获取
            /// </summary>
            public string media_id { get; set; }
        }
        #endregion

        #region 新闻消息
        public News news { get; set; }

        public class News
        {
            /// <summary>
            /// 图文消息，一个图文消息支持1到8条图文
            /// </summary>
            public List<Article> articles { get; set; }

            public class Article
            {
                /// <summary>
                /// 标题，不超过128个字节，超过会自动截断
                /// </summary>
                public string title { get; set; }

                /// <summary>
                /// 描述，不超过512个字节，超过会自动截断
                /// </summary>
                public string description { get; set; }

                /// <summary>
                /// 点击后跳转的链接。
                /// </summary>
                public string url { get; set; }

                /// <summary>
                /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片
                /// </summary>
                public string picurl { get; set; }
            }
        }
        #endregion

        #region 符合新闻消息
        /// <summary>
        /// 图文消息，一个图文消息支持1到8个图文
        /// </summary>
        public MpNews mpnews { get; set; }

        public class MpNews
        {
            /// <summary>
            /// 图文消息，一个图文消息支持1到8个图文
            /// </summary>
            public List<Article> articles { get; set; }

            public class Article
            {
                /// <summary>
                /// 图文消息的标题，不超过128个字节，超过会自动截断
                /// </summary>
                public string title { get; set; }

                /// <summary>
                /// 图文消息缩略图的media_id, 可以在上传多媒体文件接口中获得。此处thumb_media_id即上传接口返回的media_id
                /// </summary>
                public string thumb_media_id { get; set; }

                /// <summary>
                /// 图文消息的作者，不超过64个字节
                /// </summary>
                public string author { get; set; }

                /// <summary>
                /// 图文消息点击“阅读原文”之后的页面链接
                /// </summary>
                public string content_source_url { get; set; }

                /// <summary>
                /// 图文消息的内容，支持html标签，不超过666 K个字节
                /// </summary>
                public string content { get; set; }

                /// <summary>
                /// 图文消息的描述，不超过512个字节，超过会自动截断
                /// </summary>
                public string digest { get; set; }

                /// <summary>
                /// 是否显示封面，1为显示，0为不显示
                /// </summary>
                public string show_cover_pic { get; set; }
            }
        }
        #endregion        

        /// <summary>
        /// 转换为Json格式的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 转换为字节对象
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString());
        }

        /// <summary>
        /// 验证数据格式
        /// </summary>
        public void VerifyFormat()
        {
            // 此处写数据格式的验证
        }
    }
}
