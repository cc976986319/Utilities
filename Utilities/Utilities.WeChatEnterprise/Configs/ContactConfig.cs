using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.WeChatEnterprise.Configs.Base;
using Utilities.WeChatEnterprise.ResponseResults;

namespace Utilities.WeChatEnterprise.Configs
{
    /// <summary>
    /// 通讯录配置
    /// </summary>
    public class ContactConfig : Config
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="accessToken">全局唯一票据</param>
        /// <param name="url">需要配置的页面路径</param>
        /// <param name="appId">企业Id</param>
        /// <param name="beta">测试版本</param>
        /// <param name="debug">开启调试模式</param>
        public ContactConfig(string accessToken, string url, string appId, bool beta, bool debug) : base(accessToken, url, appId, beta, debug) { }

        /// <summary>
        /// 生成JS接口的临时票据
        /// </summary>
        /// <param name="accessToken">全局唯一票据</param>
        /// <returns></returns>
        protected override JsapiTicket CreateJsapiTicket(string accessToken)
        {
            string requestUrl = $"https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={accessToken}";
            // string requestUrl = $"https://qyapi.weixin.qq.com/cgi-bin/ticket/get?access_token={accessToken}&type=contact";
            JsapiTicket responseResult = JsapiTicket.Request(requestUrl);
            return responseResult;
        }
    }
}
