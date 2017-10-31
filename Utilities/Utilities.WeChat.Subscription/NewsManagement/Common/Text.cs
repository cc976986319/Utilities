using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities.WeChat.Subscription.Extensions;

namespace Utilities.WeChat.Subscription.NewsManagement.Common
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class Text : Base.Entity
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        protected Text() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        public Text(string xml)
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
            this.Content = xmlDocument.FirstChild["Content"].InnerText;
        }
    }
}
