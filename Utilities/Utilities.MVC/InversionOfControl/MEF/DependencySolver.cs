using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Utilities.MVC.InversionOfControl.MEF
{
    /// <summary>
    /// 依赖注入器
    /// </summary>
    /// <example>
    /// <code>
    ///将一下三列代码追加到Global.asax文件
    ///protected void Application_Start(){
    ///     DirectoryCatalog catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
    ///     MefDependencySolver solver = new MefDependencySolver(catalog);
    ///     DependencyResolver.SetResolver(solver);
    ///}
    /// </code>
    /// </example>
    public class DependencySolver : IDependencyResolver
    {
        private readonly ComposablePartCatalog _catalog;
        private string HttpContextKey { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="catalog"></param>
        private DependencySolver(ComposablePartCatalog catalog)
        {
            this.HttpContextKey = "MefContainerKey";

            _catalog = catalog;
        }

        /// <summary>
        /// 解析注入
        /// </summary>
        public static DependencySolver Resolver
        {
            get
            {
                DirectoryCatalog catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);

                return new DependencySolver(catalog);
            }
        }

        /// <summary>
        /// 容器
        /// </summary>
        public CompositionContainer Container
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(HttpContextKey))
                {
                    HttpContext.Current.Items.Add(HttpContextKey, new CompositionContainer(_catalog));
                }
                CompositionContainer container = (CompositionContainer)HttpContext.Current.Items[HttpContextKey];
                HttpContext.Current.Application["Container"] = container;
                return container;
            }
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            string contractName = AttributedModelServices.GetContractName(serviceType);
            return Container.GetExportedValueOrDefault<object>(contractName);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetExportedValues<object>(serviceType.FullName);
        }
    }
}
