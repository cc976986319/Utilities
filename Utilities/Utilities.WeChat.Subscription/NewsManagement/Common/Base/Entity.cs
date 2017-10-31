using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities.WeChat.Subscription.Extensions;

namespace Utilities.WeChat.Subscription.NewsManagement.Common.Base
{
    /// <summary>
    /// 基础实体(用于解析Xml)
    /// </summary>
    public abstract class Entity : BaseEntity
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }

        /// <summary>
        /// 设置基本值
        /// </summary>
        /// <param name="document"></param>
        protected virtual void SetBaseValue(XmlDocument document)
        {
            this.ToUserName = document.FirstChild["ToUserName"].InnerText;
            this.FromUserName = document.FirstChild["FromUserName"].InnerText;
            this.CreateTime = document.FirstChild["CreateTime"].InnerText.ToInt();
            this.MsgId = document.FirstChild["MsgId"].InnerText.ToInt64();
        }

        /// <summary>
        /// 转换为Json格式的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
