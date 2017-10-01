using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChatEnterprise.Tools.WeChat
{
    /// <summary>
    /// 密码
    /// </summary>
    class Cryptography
    {
        public static UInt32 HostToNetworkOrder(UInt32 inval)
        {
            UInt32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            return outval;
        }

        public static Int32 HostToNetworkOrder(Int32 inval)
        {
            Int32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            return outval;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="encodingAESKey">公众平台上，开发者设置的EncodingAESKey</param>
        /// <param name="corpid">企业号的CorpID</param>
        /// <returns></returns>
        public static string AES_decrypt(string input, string encodingAESKey, ref string corpid)
        {
            byte[] Key;
            Key = Convert.FromBase64String(encodingAESKey + "=");
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            byte[] btmpMsg = AES_decrypt(input, Iv, Key);

            int len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);


            byte[] bMsg = new byte[len];
            byte[] bCorpid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            Array.Copy(btmpMsg, 20 + len, bCorpid, 0, btmpMsg.Length - 20 - len);
            string oriMsg = Encoding.UTF8.GetString(bMsg);
            corpid = Encoding.UTF8.GetString(bCorpid);


            return oriMsg;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="encodingAESKey">公众平台上，开发者设置的EncodingAESKey</param>
        /// <param name="corpid">企业号的CorpID</param>
        /// <returns></returns>
        public static string AES_encrypt(string input, string encodingAESKey, string corpid)
        {
            byte[] key;
            key = Convert.FromBase64String(encodingAESKey + "=");
            byte[] iv = new byte[16];
            Array.Copy(key, iv, 16);
            string Randcode = CreateRandCode(16);
            byte[] bRand = Encoding.UTF8.GetBytes(Randcode);
            byte[] bCorpid = Encoding.UTF8.GetBytes(corpid);
            byte[] btmpMsg = Encoding.UTF8.GetBytes(input);
            byte[] bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(btmpMsg.Length));
            byte[] bMsg = new byte[bRand.Length + bMsgLen.Length + bCorpid.Length + btmpMsg.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(btmpMsg, 0, bMsg, bRand.Length + bMsgLen.Length, btmpMsg.Length);
            Array.Copy(bCorpid, 0, bMsg, bRand.Length + bMsgLen.Length + btmpMsg.Length, bCorpid.Length);

            return AES_encrypt(bMsg, iv, key);

        }

        /// <summary>
        /// 创建随机码
        /// </summary>
        /// <param name="codeLen">码长度</param>
        /// <returns></returns>
        private static string CreateRandCode(int codeLen)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }
            string[] arr = codeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="iv">加密向量</param>
        /// <param name="key">加密密钥</param>
        /// <returns></returns>
        private static string AES_encrypt(string input, byte[] iv, byte[] key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = ms.ToArray();
            }
            string Output = Convert.ToBase64String(xBuff);
            return Output;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input">要加密的对象</param>
        /// <param name="iv">加密向量</param>
        /// <param name="key">加密密钥</param>
        /// <returns></returns>
        private static string AES_encrypt(byte[] input, byte[] iv, byte[] key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            //aes.Padding = PaddingMode.PKCS7;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            #region 自己进行PKCS7补位，用系统自己带的不行
            byte[] msg = new byte[input.Length + 32 - input.Length % 32];
            Array.Copy(input, msg, input.Length);
            byte[] pad = KCS7Encoder(input.Length);
            Array.Copy(pad, 0, msg, input.Length, pad.Length);
            #endregion

            #region 注释的也是一种方法，效果一样
            //ICryptoTransform transform = aes.CreateEncryptor();
            //byte[] xBuff = transform.TransformFinalBlock(msg, 0, msg.Length);
            #endregion

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    cs.Write(msg, 0, msg.Length);
                }
                xBuff = ms.ToArray();
            }

            string Output = Convert.ToBase64String(xBuff);
            return Output;
        }

        /// <summary>
        /// KCS7解码器
        /// </summary>
        /// <param name="text_length"></param>
        /// <returns></returns>
        private static byte[] KCS7Encoder(int text_length)
        {
            int block_size = 32;
            // 计算需要填充的位数
            int amount_to_pad = block_size - (text_length % block_size);
            if (amount_to_pad == 0)
            {
                amount_to_pad = block_size;
            }
            // 获得补位所用的字符
            char pad_chr = ToChar(amount_to_pad);
            string tmp = "";
            for (int index = 0; index < amount_to_pad; index++)
            {
                tmp += pad_chr;
            }
            return Encoding.UTF8.GetBytes(tmp);
        }

        /// <summary>
        /// 将数字转化成ASCII码对应的字符，用于对明文进行补码
        /// </summary>
        /// <param name="number">需要转化的数字</param>
        /// <returns>转化得到的字符</returns>
        static char ToChar(int number)
        {
            byte target = (byte)(number & 0xFF);
            return (char)target;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input">待解密字符串</param>
        /// <param name="iv">向量</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        private static byte[] AES_decrypt(string input, byte[] iv, byte[] key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = key;
            aes.IV = iv;
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(input);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = decode2(ms.ToArray());
            }
            return xBuff;
        }

        /// <summary>
        /// 解密2
        /// </summary>
        /// <param name="decrypted"></param>
        /// <returns></returns>
        private static byte[] decode2(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}
