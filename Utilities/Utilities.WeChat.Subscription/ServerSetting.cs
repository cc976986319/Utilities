using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Subscription
{
    /// <summary>
    /// 服务器配置
    /// </summary>
    public class ServerSetting
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">加密字符串</param>
        /// <returns></returns>
        public static string VerifyResult(string signature, string timestamp, string nonce, string echostr, string token)
        {
            string value = GetSignature(timestamp, nonce, token);
            if (signature == value)
            {
                return echostr;//返回验证通过的加密字符串
            }
            else
            {
                return $"failed:{signature},{ value}。请注意保持Token一致。";
            }
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        protected static string GetSignature(string timestamp, string nonce, string token)
        {
            string[] items = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();// 混淆排序
            string value = string.Join("", items);// 粘贴连接
            SHA1 sha1 = SHA1.Create();
            byte[] data = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));

            StringBuilder enText = new StringBuilder();
            foreach (var b in data)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            return enText.ToString();
        }
    }
}
