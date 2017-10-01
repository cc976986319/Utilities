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
    /// 图片消息
    /// </summary>
    public class Image : Base.Entity
    {
        /// <summary>
        /// 图片链接（由系统生成）
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
        
        /// <summary>
        /// 实例化
        /// </summary>
        protected Image() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        public Image(string xml)
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
            this.PicUrl = xmlDocument.FirstChild["PicUrl"].InnerText;
            this.MediaId = xmlDocument.FirstChild["MediaId"].InnerText;
        }
    }
}
