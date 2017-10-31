using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities.WeChat.Subscription.Extensions;

namespace Utilities.WeChat.Subscription.NewsManagement
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 接收方微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方微信号，若为普通用户，则是一个OpenID
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型，link、location、video、shortvideo、voice、image、text、event
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 转换Xml数据
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        protected abstract void ConvertTo(string xml);
        
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="xml">xml格式的字符串</param>
        /// <returns></returns>
        public static string GetDataType(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument.FirstChild["MsgType"].InnerText;
        }
    }
}
