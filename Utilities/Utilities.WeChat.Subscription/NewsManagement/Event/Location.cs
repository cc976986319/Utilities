using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities.WeChat.Subscription.NewsManagement.Event
{
    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class Location : Base.Entity
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }

        protected override void ConvertTo(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            base.SetBaseValue(xmlDocument);

            this.Event = xmlDocument.FirstChild["Event"].InnerText;
            this.Latitude = xmlDocument.FirstChild["Latitude"].InnerText;
            this.Longitude = xmlDocument.FirstChild["Longitude"].InnerText;
            this.Precision = xmlDocument.FirstChild["Precision"].InnerText;
        }
    }
}
