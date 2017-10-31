using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WindowSystem
{
    /// <summary>
    /// 计算机信息
    /// </summary>
    public class ComputerInfo
    {
        /// <summary>
        /// 计算机名称
        /// </summary>
        public static string ComputerName { get { return Dns.GetHostName(); } }

        /// <summary>
        /// 首个IPv4信息
        /// </summary>
        public static string FristIPv4 { get { return _FristIPv4.ToString(); } }

        /// <summary>
        /// 首个IPv4信息
        /// </summary>
        public static IPAddress _FristIPv4 { get { return GetIpAddress(AddressFamily.InterNetwork).FirstOrDefault(); } }

        /// <summary>
        /// 所有的IPv4地址
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IPAddress> IPv4() { return GetIpAddress(AddressFamily.InterNetwork); }

        /// <summary>
        /// 获取首个IPv6信息
        /// </summary>
        public static string FristIPv6 { get { return _FristIPv6.ToString(); } }

        /// <summary>
        /// 获取首个IPv6信息
        /// </summary>
        public static IPAddress _FristIPv6 { get { return GetIpAddress(AddressFamily.InterNetworkV6).FirstOrDefault(); } }

        /// <summary>
        /// 所有的IPv6地址
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IPAddress> IPv6() { return GetIpAddress(AddressFamily.InterNetworkV6); }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="type">地址类型</param>
        /// <returns></returns>
        private static IEnumerable<IPAddress> GetIpAddress(AddressFamily type)
        {
            string hostName = Dns.GetHostName();

            return Dns.GetHostAddresses(hostName).Where(e => e.AddressFamily == type);
        }

        /// <summary>
        /// 是使用端口
        /// </summary>
        /// <param name="port">要检测的端口</param>
        public static bool IsUsePort(int port)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                    return true;
            }
            return false;
        }
    }
}
