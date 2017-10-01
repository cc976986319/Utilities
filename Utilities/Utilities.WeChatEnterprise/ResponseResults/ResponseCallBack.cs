using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.ResponseResults
{
    /// <summary>
    /// 响应回调
    /// </summary>
    public class ResponseCallBack
    {
        /// <summary>
        /// 响应回调
        /// </summary>
        /// <param name="msg_encrypt">经过加密的密文</param>
        /// <param name="msg_signature">签名串</param>
        /// <param name="timestamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="nonce">随机串，可以自己生成，也可以用URL参数的nonce</param>
        /// <returns></returns>
        public string ResponseXml(string msg_encrypt, string msg_signature, string timestamp, string nonce)
        {
            string result = $"<xml><Encrypt><![CDATA[{msg_encrypt}]]></Encrypt><MsgSignature><![CDATA[{msg_signature}]]></MsgSignature><TimeStamp>{timestamp}</TimeStamp><Nonce><![CDATA[{nonce}]]></Nonce></xml>";
            return result;
        }

        /// <summary>
        /// 响应回调
        /// </summary>
        /// <param name="toUser">企业号的CorpID</param>
        /// <param name="agentId">接收的应用id</param>
        /// <param name="msg_encrypt">经过加密的密文</param>
        /// <returns></returns>
        public CallBack.xml ResponseXml(string toUser, string agentId, string msg_encrypt)
        {
            //string result = $"<xml><ToUserName><![CDATA[{toUser}]]></ToUserName><AgentID><![CDATA[{agentId}]]></AgentID><Encrypt><![CDATA[{msg_encrypt}]]></Encrypt></xml>";

            CallBack.xml result = new CallBack.xml(agentId, toUser, msg_encrypt);
            return result;
        }
    }
}
