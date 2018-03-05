using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ProxyTools.Factorys
{
    /// <summary>
    /// www.66ip.cn网站的代理类
    /// </summary>
    public class Proxy_66IP : ProxyFactory
    {
        /// <summary>
        /// 实例化www.66ip.cn代理工厂
        /// </summary>
        /// <param name="proxyWebSiteUrl">代理网站地址</param>
        public Proxy_66IP(string proxyWebSiteUrl) : base(proxyWebSiteUrl) { }

        /// <summary>
        /// 读取代理
        /// </summary>
        public override void ReadingProxys()
        {
            //HtmlWeb htmlWeb = new HtmlWeb();
            //HtmlDocument document = htmlWeb.Load(this.ProxyWebSiteUrl);
        }
    }
}
