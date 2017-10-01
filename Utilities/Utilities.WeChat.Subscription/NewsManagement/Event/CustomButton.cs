using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities.WeChat.Subscription.NewsManagement.Event
{
    /// <summary>
    /// 自定义菜单事件
    /// </summary>
    public class CustomButton : Base.Entity
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        protected override void ConvertTo(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            base.SetBaseValue(xmlDocument);

            this.Event = xmlDocument.FirstChild["Event"].InnerText;
            this.EventKey = xmlDocument.FirstChild["EventKey"].InnerText;
        }
    }
}
