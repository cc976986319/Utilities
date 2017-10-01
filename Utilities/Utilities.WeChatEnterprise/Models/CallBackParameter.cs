using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.Models
{
    /// <summary>
    /// 回调链接参数
    /// </summary>
    public class CallBackParameter
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="msg_signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">加密的随机字符串</param>
        public CallBackParameter(string msg_signature, long timestamp, long nonce, string echostr)
        {
            this.msg_signature = msg_signature;
            this.timestamp = timestamp;
            this.nonce = nonce;
            this.echostr = echostr;
        }

        /// <summary>
        /// 微信加密签名，msg_signature结合了企业填写的token、请求中的timestamp、nonce参数、加密的消息体
        /// </summary>
        public string msg_signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public long nonce { get; set; }

        /// <summary>
        /// 加密的随机字符串，以msg_encrypt格式提供。需要解密并返回echostr明文，解密后有random、msg_len、msg、$CorpID四个字段，其中msg即为echostr明文
        /// </summary>
        public string echostr { get; set; }
    }
}
