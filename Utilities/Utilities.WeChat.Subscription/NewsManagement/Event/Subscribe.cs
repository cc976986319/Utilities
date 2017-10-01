using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities.WeChat.Subscription.NewsManagement.Event
{
    /// <summary>
    /// 订阅
    /// </summary>
    public class Subscribe : Base.Entity
    {
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 转换Xml数据
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        protected override void ConvertTo(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            base.SetBaseValue(xmlDocument);

            this.Event = xmlDocument.FirstChild["Event"].InnerText;
            this.EventKey = xmlDocument.FirstChild["EventKey"].InnerText;
            this.Ticket = xmlDocument.FirstChild["Ticket"].InnerText;
        }
    }
}
