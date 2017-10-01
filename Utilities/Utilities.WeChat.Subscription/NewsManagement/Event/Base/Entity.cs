using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities.WeChat.Subscription.Microsoft;

namespace Utilities.WeChat.Subscription.NewsManagement.Event.Base
{
    /// <summary>
    /// 基础实体(用于解析Xml)
    /// </summary>
    public abstract class Entity : BaseEntity
    {
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 设置基本值
        /// </summary>
        /// <param name="document"></param>
        protected virtual void SetBaseValue(XmlDocument document)
        {
            this.ToUserName = document.FirstChild["ToUserName"].InnerText;
            this.FromUserName = document.FirstChild["FromUserName"].InnerText;
            this.CreateTime = document.FirstChild["CreateTime"].InnerText.ToInt();
            this.MsgType = document.FirstChild["MsgType"].InnerText;
        }

        public static string GetEventType(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument.FirstChild["Event"].InnerText;
        }
    }
}
