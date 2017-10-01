using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extension.Microsoft
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        #region Config操作
        #region 读写App.Setting节点
        /// <summary>
        /// 读取App节点
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string ReadAppSetting(this string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 修改App节点
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void WriteAppSettings(this string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 创建App节点
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void CreatAppSettings(this string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 删除App节点
        /// </summary>
        /// <param name="key">key</param>
        public static void RemoveAppSettings(this string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        #region 读写ConnectionStrings节点
        /// <summary>
        /// 获取App节点
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static string ReadingConnectionStrings(this string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        #endregion
        #endregion

        #region 基础数据转换操作
        /// <summary>
        /// 转换为布尔类型
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            return bool.Parse(value);
        }

        /// <summary>
        /// 转换为整形
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        /// <summary>
        /// 转换为16位整形
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static Int16 ToInt16(this string value)
        {
            return Int16.Parse(value);
        }

        /// <summary>
        /// 转换为32位整形
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static Int32 ToInt32(this string value)
        {
            return Int32.Parse(value);
        }

        /// <summary>
        /// 转换为64位整形
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static Int64 ToInt64(this string value)
        {
            return Int64.Parse(value);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }

        /// <summary>
        /// 转换为单精度浮点
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static float ToFloat(this string value)
        {
            return float.Parse(value);
        }

        /// <summary>
        /// 转换为双精度浮点
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            return double.Parse(value);
        }

        /// <summary>
        /// 转换为字节（默认为UTF-8编码）
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <returns></returns>
        public static byte[] ToByte(this string value)
        {
            return ToByte(value, Encoding.UTF8);
        }

        /// <summary>
        /// 转换为字节
        /// </summary>
        /// <param name="value">待转换字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static byte[] ToByte(this string value, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(value);
        }
        #endregion
    }
}
