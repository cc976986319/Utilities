using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Net.WebClientExtension
{
    /// <summary>  
    /// 带Cookie的WebClient  
    /// </summary>  
    public class CookieWebClient : WebClient
    {
        /// <summary>
        /// Cookie容器  
        /// </summary>
        public CookieContainer Cookies;

        /// <summary>  
        /// 实例化
        /// </summary>  
        public CookieWebClient()
        {
            this.Cookies = new CookieContainer();
        }

        /// <summary>
        /// 获取请求
        /// </summary>
        /// <param name="address">请求地址</param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = Cookies;
            }
            return request;
        }
    }
}
