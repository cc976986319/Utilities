using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities.Ioc.Attributes;

namespace Utilities.Ioc
{
    /// <summary>
    /// 数据扩展
    /// </summary>
    public static class DataExtension
    {
        /// <summary>
        /// 应用程序配置
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns></returns>
        public static string AppSetting(this string key)
        {
            string configVaule = ConfigurationManager.AppSettings[key];
            if (configVaule != null && configVaule != "")
            {
                return configVaule.ToString();
            }
            return "";
        }

        /// <summary>
        /// 获取泛型类中指定属性值
        /// </summary>
        /// <returns></returns>
        public static string GetFullTypeName(this Type type)
        {
            object[] attrList = type.GetCustomAttributes(typeof(MappingAttribute), false);
            if (attrList != null)
            {
                MappingAttribute categoryInfo = (MappingAttribute)attrList[0];
                return categoryInfo.FullTypeName;
            }
            return "";
        }

        /// <summary>
        /// 加载程序集文件
        /// </summary>
        /// <param name="assemblyPlugs">外挂插件程序集目录路径</param>
        /// <param name="typeName">程序集名称</param>
        /// <returns></returns>
        public static string LoadAssemblyFile(this string assemblyPlugs, string typeName)
        {
            string path = string.Empty;
            DirectoryInfo directory = new DirectoryInfo(assemblyPlugs);
            foreach (FileInfo file in directory.GetFiles("*.dll"))
            {
                Assembly assembly = Assembly.LoadFile(file.FullName);
                Type type = assembly.GetType(typeName, false);
                if (type != null)
                {
                    path = file.FullName;
                }
            }
            return path;
        }
    }
}
