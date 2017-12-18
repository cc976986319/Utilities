using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.JsSDK
{
    /// <summary>
    /// SOTER生物认证后台接口
    /// </summary>
    public class Soter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account_token">企业号接口调用凭证</param>
        public void Verify_signature(string account_token)
        {

        }

        public class Signature
        {
            public string openid { get; set; }

            public string json_string { get; set; }

            public string json_signature { get; set; }
        }
    }
}
