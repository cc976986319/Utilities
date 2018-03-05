using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ProxyTools
{
    /// <summary>
    /// 代理工厂
    /// </summary>
    public abstract class ProxyFactory
    {
        /// <summary>
        /// 实例化代理工厂
        /// </summary>
        /// <param name="proxyWebSiteUrl">代理网站地址</param>
        public ProxyFactory(string proxyWebSiteUrl)
        {
            this.ProxyWebSiteUrl = proxyWebSiteUrl;
        }

        /// <summary>
        /// 代理网站地址
        /// </summary>
        public string ProxyWebSiteUrl { get; private set; }

        /// <summary>
        /// 代理信息
        /// </summary>
        public List<Proxy> Proxys { get; private set; }

        /// <summary>
        /// 读取代理信息
        /// </summary>
        public abstract void ReadingProxys();
    }
}
