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
    /// 地理位置消息
    /// </summary>
    public class Location : Base.Entity
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// 实例化
        /// </summary>
        protected Location() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        public Location(string xml)
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
            this.Location_X = xmlDocument.FirstChild["Location_X"].InnerText;
            this.Location_Y = xmlDocument.FirstChild["Location_Y"].InnerText;
            this.Scale = xmlDocument.FirstChild["Scale"].InnerText;
            this.Label = xmlDocument.FirstChild["Label"].InnerText;
        }
    }
}
