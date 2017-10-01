using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities.WeChatEnterprise.Tools.WeChat
{
    /// <summary>
    /// 微信商业隐藏消息
    /// </summary>
    public class WXBizMsgCrypt
    {
        /// <summary>
        /// 公众平台上，开发者设置的Token
        /// </summary>
        string Token;

        /// <summary>
        /// 公众平台上，开发者设置的EncodingAESKey
        /// </summary>
        string EncodingAESKey;

        /// <summary>
        /// 企业号的CorpID
        /// </summary>
        string CorpID;

        /// <summary>
        /// 微信商业隐藏消息错误码
        /// </summary>
        enum WXBizMsgCryptErrorCode
        {
            /// <summary>
            /// 成功
            /// </summary>
            WXBizMsgCrypt_OK = 0,
            /// <summary>
            /// 签名验证错误
            /// </summary>
            WXBizMsgCrypt_ValidateSignature_Error = -40001,
            /// <summary>
            /// xml解析失败
            /// </summary>
            WXBizMsgCrypt_ParseXml_Error = -40002,
            /// <summary>
            /// sha加密生成签名失败
            /// </summary>
            WXBizMsgCrypt_ComputeSignature_Error = -40003,
            /// <summary>
            /// AESKey 非法
            /// </summary>
            WXBizMsgCrypt_IllegalAesKey = -40004,
            /// <summary>
            /// corpid 校验错误
            /// </summary>
            WXBizMsgCrypt_ValidateCorpid_Error = -40005,
            /// <summary>
            /// AES 加密失败
            /// </summary>
            WXBizMsgCrypt_EncryptAES_Error = -40006,
            /// <summary>
            /// AES 解密失败
            /// </summary>
            WXBizMsgCrypt_DecryptAES_Error = -40007,
            /// <summary>
            /// 解密后得到的buffer非法
            /// </summary>
            WXBizMsgCrypt_IllegalBuffer = -40008,
            /// <summary>
            /// base64加密异常
            /// </summary>
            WXBizMsgCrypt_EncodeBase64_Error = -40009,
            /// <summary>
            /// base64解密异常
            /// </summary>
            WXBizMsgCrypt_DecodeBase64_Error = -40010
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="token">公众平台上，开发者设置的Token</param>
        /// <param name="encodingAESKey">公众平台上，开发者设置的EncodingAESKey</param>
        /// <param name="corpID">企业号的CorpID</param>
        public WXBizMsgCrypt(string token, string encodingAESKey, string corpID)
        {
            Token = token;
            EncodingAESKey = encodingAESKey;
            CorpID = corpID;
        }

        /// <summary>
        /// 验证URL
        /// </summary>
        /// <param name="msgSignature">签名串，对应URL参数的msg_signature</param>
        /// <param name="timeStamp">时间戳，对应URL参数的timestamp</param>
        /// <param name="nonce">随机串，对应URL参数的nonce</param>
        /// <param name="echoStr">随机串，对应URL参数的echostr</param>
        /// <param name="replyEchoStr">解密之后的echostr，当return返回0时有效</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public int VerifyURL(string msgSignature, string timeStamp, string nonce, string echoStr, ref string replyEchoStr)
        {
            int ret = 0;
            if (EncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            ret = VerifySignature(Token, timeStamp, nonce, echoStr, msgSignature);
            if (0 != ret)
            {
                return ret;
            }
            replyEchoStr = "";
            string cpid = "";
            try
            {
                replyEchoStr = Cryptography.AES_decrypt(echoStr, EncodingAESKey, ref cpid); //m_sCorpID);
            }
            catch (Exception)
            {
                replyEchoStr = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
            }
            if (cpid != CorpID)
            {
                replyEchoStr = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateCorpid_Error;
            }
            return 0;
        }

        /// <summary>
        /// 检验消息的真实性，并且获取解密后的明文
        /// </summary>
        /// <param name="msgSignature">签名串，对应URL参数的msg_signature</param>
        /// <param name="timeStamp">时间戳，对应URL参数的timestamp</param>
        /// <param name="nonce">随机串，对应URL参数的nonce</param>
        /// <param name="postData">密文，对应POST请求的数据</param>
        /// <param name="msg">解密后的原文，当return返回0时有效</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public int DecryptMsg(string msgSignature, string timeStamp, string nonce, string postData, ref string msg)
        {
            if (EncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            XmlDocument doc = new XmlDocument();
            XmlNode root;
            string sEncryptMsg;
            try
            {
                doc.LoadXml(postData);
                root = doc.FirstChild;
                sEncryptMsg = root["Encrypt"].InnerText;
            }
            catch (Exception)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ParseXml_Error;
            }
            //verify signature
            int ret = 0;
            ret = VerifySignature(Token, timeStamp, nonce, sEncryptMsg, msgSignature);
            if (ret != 0)
                return ret;
            //decrypt
            string cpid = "";
            try
            {
                msg = Cryptography.AES_decrypt(sEncryptMsg, EncodingAESKey, ref cpid);
            }
            catch (FormatException)
            {
                msg = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecodeBase64_Error;
            }
            catch (Exception)
            {
                msg = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
            }
            if (cpid != CorpID)
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateCorpid_Error;
            return 0;
        }

        /// <summary>
        /// 将企业号回复用户的消息加密打包
        /// </summary>
        /// <param name="replyMsg">企业号待回复用户的消息，xml格式的字符串</param>
        /// <param name="timeStamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="nonce">随机串，可以自己生成，也可以用URL参数的nonce</param>
        /// <param name="encryptMsg">加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串,当return返回0时有效</param>
        /// <returns>成功0，失败返回对应的错误码</returns>
        public int EncryptMsg(string replyMsg, string timeStamp, string nonce, ref string encryptMsg)
        {
            if (EncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            string raw = "";
            try
            {
                raw = Cryptography.AES_encrypt(replyMsg, EncodingAESKey, CorpID);
            }
            catch (Exception)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_EncryptAES_Error;
            }
            string MsgSigature = "";
            int ret = 0;
            ret = GenarateSinature(Token, timeStamp, nonce, raw, ref MsgSigature);
            if (0 != ret)
                return ret;
            encryptMsg = "";

            string EncryptLabelHead = "<Encrypt><![CDATA[";
            string EncryptLabelTail = "]]></Encrypt>";
            string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            string MsgSigLabelTail = "]]></MsgSignature>";
            string TimeStampLabelHead = "<TimeStamp><![CDATA[";
            string TimeStampLabelTail = "]]></TimeStamp>";
            string NonceLabelHead = "<Nonce><![CDATA[";
            string NonceLabelTail = "]]></Nonce>";
            encryptMsg = encryptMsg + "<xml>" + EncryptLabelHead + raw + EncryptLabelTail;
            encryptMsg = encryptMsg + MsgSigLabelHead + MsgSigature + MsgSigLabelTail;
            encryptMsg = encryptMsg + TimeStampLabelHead + timeStamp + TimeStampLabelTail;
            encryptMsg = encryptMsg + NonceLabelHead + nonce + NonceLabelTail;
            encryptMsg += "</xml>";
            return 0;
        }

        /// <summary>
        /// 字典排序
        /// </summary>
        public class DictionarySort : System.Collections.IComparer
        {
            /// <summary>
            /// 比较
            /// </summary>
            /// <param name="left">左边</param>
            /// <param name="right">右边</param>
            /// <returns></returns>
            public int Compare(object left, object right)
            {
                string sLeft = left as string;
                string sRight = right as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    else if (sLeft[index] > sRight[index])
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="token">公众平台上，开发者设置的Token</param>
        /// <param name="timeStamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="nonce">随机串，可以自己生成，也可以用URL参数的nonce</param>
        /// <param name="msgEncrypt">消息加密</param>
        /// <param name="sigture">签名</param>
        /// <returns></returns>
        private static int VerifySignature(string token, string timeStamp, string nonce, string msgEncrypt, string sigture)
        {
            string hash = "";
            int ret = 0;
            ret = GenarateSinature(token, timeStamp, nonce, msgEncrypt, ref hash);
            if (ret != 0)
                return ret;
            if (hash == sigture)
                return 0;
            else
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateSignature_Error;
            }
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="token">公众平台上，开发者设置的Token</param>
        /// <param name="timeStamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="nonce">随机串，可以自己生成，也可以用URL参数的nonce</param>
        /// <param name="msgEncrypt">消息加密</param>
        /// <param name="msgSignature">消息签名</param>
        /// <returns></returns>
        public static int GenarateSinature(string token, string timeStamp, string nonce, string msgEncrypt, ref string msgSignature)
        {
            ArrayList AL = new ArrayList();
            AL.Add(token);
            AL.Add(timeStamp);
            AL.Add(nonce);
            AL.Add(msgEncrypt);
            AL.Sort(new DictionarySort());
            string raw = "";
            for (int i = 0; i < AL.Count; ++i)
            {
                raw += AL[i];
            }

            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider();
                enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(raw);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ComputeSignature_Error;
            }
            msgSignature = hash;
            return 0;
        }
    }
}
