using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ProxyTools
{
    /// <summary>
    /// 代理
    /// </summary>
    public class Proxy
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 所属地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 匿名类型
        /// </summary>
        public string AnonymityType { get; set; }

        /// <summary>
        /// 响应速度
        /// </summary>
        public long? ResponseSpeed { get; set; }

        /// <summary>
        /// 最后响应时间
        /// </summary>
        public DateTime? LastVerifyTime { get; set; }
    }
}
