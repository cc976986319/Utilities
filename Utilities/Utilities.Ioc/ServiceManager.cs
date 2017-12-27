using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Ioc
{
    /// <summary>
    /// 服务管理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceManager<T> : IDisposable where T : class
    {
        /// <summary>
        /// 应用域
        /// </summary>
        private AppDomain ctorProxy = null;

        /// <summary>
        /// 应用程序运行域容器
        /// </summary>
        public AppDomain CotrProxy
        {
            get { return ctorProxy; }
        }

        /// <summary>
        /// 代理
        /// </summary>
        private T proxy = default(T);

        /// <summary>
        /// 代理容器
        /// </summary>
        public T Proxy
        {
            get
            {
                if (proxy == null)
                {
                    proxy = (T)InitProxy(GetAssemblyPlugs, this.fullTypeName);
                }
                return proxy;
            }
        }

        /// <summary>
        /// 外挂插件程序集目录路径
        /// </summary>
        private string assemblyPlugs;

        /// <summary>
        /// 类地址(相对程序集的绝对路径)
        /// </summary>
        private string fullTypeName;

        /// <summary>
        /// 获取外挂插件程序集目录路径
        /// </summary>
        public string GetAssemblyPlugs
        {
            get
            {
                assemblyPlugs = "PrivatePath".AppSetting();
                if (string.IsNullOrEmpty(assemblyPlugs))// 设置外挂插件程序集目录路径
                {
                    assemblyPlugs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
                }
                if (!Directory.Exists(assemblyPlugs))
                {
                    Directory.CreateDirectory(assemblyPlugs);
                }
                return assemblyPlugs;
            }
        }

        /// <summary>
        /// 获取类地址(相对程序集的绝对路径)
        /// </summary>
        public string GetFullTypeName
        {
            get
            {
                if (string.IsNullOrEmpty(this.fullTypeName))
                {
                    Type meetType = typeof(T);
                    this.fullTypeName = meetType.GetFullTypeName();// 设置类地址(相对程序集的绝对路径)
                }
                return fullTypeName;
            }
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="fullName">类地址(相对程序集的绝对路径)</param>
        public ServiceManager(string fullName)
        {
            this.fullTypeName = fullName;
            if (proxy == null)
            {
                proxy = (T)InitProxy(GetAssemblyPlugs, fullName);
            }
        }

        /// <summary>
        /// 初始化代理
        /// </summary>
        /// <param name="assemblyPlugs">外挂插件程序集目录路径</param>
        /// <param name="fullTypeName">类地址(相对程序集的绝对路径)</param>
        /// <returns></returns>
        private T InitProxy(string assemblyPlugs, string fullTypeName = "")
        {
            this.fullTypeName = fullTypeName;// 设置类地址
            try
            {
                AppDomainSetup ads = new AppDomainSetup();
                ads.ApplicationName = "RMB";
                //应用程序根目录
                ads.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                //子目录（相对形式）在AppDomainSetup中加入外部程序集的所在目录，多个目录用分号间隔
                ads.PrivateBinPath = assemblyPlugs;
                //设置缓存目录
                ads.CachePath = ads.ApplicationBase;
                //获取或设置指示影像复制是打开还是关闭
                ads.ShadowCopyFiles = "true";
                //获取或设置目录的名称，这些目录包含要影像复制的程序集
                ads.ShadowCopyDirectories = ads.ApplicationBase;
                ads.DisallowBindingRedirects = false;
                ads.DisallowCodeDownload = true;
                ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

                // 建新应用程序域凭据
                Evidence adevidence = AppDomain.CurrentDomain.Evidence;

                // 创建第二应用域
                ctorProxy = AppDomain.CreateDomain("AD #2", adevidence, ads);

                string assemblyName = Assembly.GetExecutingAssembly().GetName().FullName;

                TransparentAgent factory = (TransparentAgent)ctorProxy.CreateInstanceAndUnwrap(assemblyName, typeof(TransparentAgent).FullName);

                string assemblyPath = ctorProxy.SetupInformation.PrivateBinPath;

                if (string.IsNullOrEmpty(fullTypeName))
                    return factory.Create<T>(assemblyPath, GetFullTypeName, new object[0]);// 创建实例

                return factory.Create<T>(assemblyPath, fullTypeName, new object[0]);// 创建实例
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        public void Unload()
        {
            try
            {
                if (ctorProxy != null)
                {
                    AppDomain.Unload(ctorProxy);
                    ctorProxy = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            this.Unload();
        }
    }
}
