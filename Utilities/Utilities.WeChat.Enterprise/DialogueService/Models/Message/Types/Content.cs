using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.DialogueService.Models.Message.Types
{
    /// <summary>
    /// 基础类型
    /// </summary>
    public abstract class Content
    {
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    public class Text : Content
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class Image : Content
    {
        /// <summary>
        /// 图片media_id，可以调用上传素材文件接口获取
        /// </summary>
        public string media_id { get; set; }
    }

    /// <summary>
    /// 文件消息
    /// </summary>
    public class File : Content
    {
        /// <summary>
        /// 文件media_id，可以调用上传素材文件接口获取。文件须大于4字节
        /// </summary>
        public string media_id { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class Voice : Content
    {
        /// <summary>
        /// 语音media_id，可以调用上传素材文件接口获取。size须大于4字节
        /// </summary>
        public string media_id { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class Link : Content
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
}
