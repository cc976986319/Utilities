﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Container.Simple
{
    /// <summary>
    /// 派生类扫描容器
    /// </summary>
    /// <typeparam name="T">指定类型，即原型(基类)</typeparam>
    public class DerivedClassScanContainer<T> where T : class
    {
        #region Single 
        /// <summary>
        /// 实例化
        /// </summary>
        DerivedClassScanContainer() { }

        /// <summary>
        /// 容器
        /// </summary>
        static DerivedClassScanContainer<T> Container { get; set; }

        /// <summary>
        /// 锁
        /// </summary>
        static object _lock { get; set; }

        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static DerivedClassScanContainer<T> Single()
        {
            if (Container == null)
            {
                lock (_lock)
                {
                    if (Container == null)
                        Container = new DerivedClassScanContainer<T>();
                }
            }
            return Container;
        }
        #endregion

        /// <summary>
        /// 容器
        /// </summary>
        public List<Type> Types
        {
            get
            {
                if (this._Types == null)
                    this.Register();
                return this._Types;
            }
        }

        /// <summary>
        /// 实际容器
        /// </summary>
        private List<Type> _Types { get; set; }

        /// <summary>
        /// 注册
        /// </summary>
        protected virtual void Register()
        {
            _Types = (from t in Assembly.GetExecutingAssembly().GetTypes()
                      where IsDerivedClass(t, typeof(T))
                      select t).ToList();
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public virtual dynamic CreateInstance(string className)
        {
            foreach (Type type in this.Types)
            {
                if (className == type.Name)
                {
                    return type.Assembly.CreateInstance(type.FullName);
                }
            }
            return null;
        }

        /// <summary>
        /// 是指定的派生类
        /// </summary>
        /// <param name="type">待判断类型</param>
        /// <param name="baseType">基类型</param>
        /// <returns></returns>
        protected virtual bool IsDerivedClass(Type type, Type baseType)
        {
            var basetype = type.BaseType;
            while (basetype != null)
            {
                if (basetype.Equals(baseType))
                {
                    return true;
                }
                basetype = basetype.BaseType;
            }
            return false;
        }
    }
}
