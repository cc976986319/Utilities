﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Utilities.WeChat.Enterprise.Configs
{
    /// <summary>
    /// 卡券配置
    /// </summary>
    public class CardConfig : Base.Config
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="accessToken">全局唯一票据</param>
        /// <param name="url">需要配置的页面路径</param>
        /// <param name="appId">企业Id</param>
        /// <param name="beta">测试版本</param>
        /// <param name="debug">开启调试模式</param>
        public CardConfig(string accessToken, string url, string appId, bool beta, bool debug) : base(accessToken, url, appId, beta, debug) { }

        /// <summary>
        /// 生成JS接口的临时票据
        /// </summary>
        /// <returns></returns>
        protected override JsapiTicket CreateJsapiTicket()
        {
            string key = $"{this.appId}{this.accessToken}wx_card_Ticket";
            JsapiTicket responseResult = null;
            if (HttpRuntime.Cache[key] == null)
            {
                string requestUrl = "https://" + $"qyapi.weixin.qq.com/cgi-bin/ticket/get?access_token={this.accessToken}&type=wx_card";
                responseResult = JsapiTicket.Request(requestUrl);
                if (responseResult.errcode == 0)
                {
                    int outTime = responseResult.expires_in - 60;// 过期时间
                    if (outTime > 0)
                        HttpRuntime.Cache.Add(key, responseResult, null, DateTime.Now.AddSeconds(outTime), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
                this.errcode = responseResult.errcode;
                this.errmsg = responseResult.errmsg;
            }
            else
                responseResult = (JsapiTicket)HttpRuntime.Cache[key];
            return responseResult;
        }
    }
}
