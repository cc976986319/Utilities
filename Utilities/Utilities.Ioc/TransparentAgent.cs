using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Ioc
{
    /// <summary>
    /// 透明代理
    /// </summary>
    public class TransparentAgent : MarshalByRefObject
    {
        /// <summary>
        /// 绑定类型
        /// </summary>
        private const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;

        public TransparentAgent() { }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic Create(string assemblyFile, string typeName, object[] args)
        {
            return (dynamic)Activator.CreateInstanceFrom(assemblyFile, typeName, false, flags, null, args, null, null).Unwrap();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyPath"></param>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T Create<T>(string assemblyPath, string typeName, object[] args)
        {
            string assemblyFile = assemblyPath.LoadAssemblyFile(typeName);
            return (T)Activator.CreateInstanceFrom(assemblyFile, typeName, false, flags, null, args, null, null).Unwrap();
        }
    }
}
