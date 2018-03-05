using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.WeChat.Enterprise.Microsoft
{
    /// <summary>
    /// webConfig扩展
    /// </summary>
    public static class WebConfigExtension
    {
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
    }
}
