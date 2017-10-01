using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities.WeChat.Subscription.Microsoft;

namespace Utilities.WeChat.Subscription.NewsManagement.Common
{
    public class Link : Base.Entity
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// 实例化
        /// </summary>
        protected Link() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        public Link(string xml)
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
            this.Title = xmlDocument.FirstChild["Title"].InnerText;
            this.Description = xmlDocument.FirstChild["Description"].InnerText;
            this.Url = xmlDocument.FirstChild["Url"].InnerText;
        }
    }
}
