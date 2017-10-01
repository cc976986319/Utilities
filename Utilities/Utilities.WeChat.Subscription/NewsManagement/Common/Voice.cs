using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities.WeChat.Subscription.Microsoft;

namespace Utilities.WeChat.Subscription.NewsManagement.Common
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class Voice : Base.Entity
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }
        
        /// <summary>
        /// 实例化
        /// </summary>
        protected Voice() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        public Voice(string xml)
        {
            this.ConvertTo(xml);
        }

        /// <summary>
        /// 转换Xml数据
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        protected override void ConvertTo(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            base.SetBaseValue(xmlDocument);

            this.MsgType = xmlDocument.FirstChild["MsgType"].InnerText;
            this.MediaId = xmlDocument.FirstChild["MediaId"].InnerText;
            this.Format = xmlDocument.FirstChild["Format"].InnerText;
            this.Recognition = xmlDocument.FirstChild["Recognition"].InnerText;
        }
    }
}
