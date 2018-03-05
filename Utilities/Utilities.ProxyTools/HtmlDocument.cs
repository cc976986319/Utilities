using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ProxyTools
{
    /// <summary>
    /// 网页文本
    /// </summary>
    public class HtmlDocument : IDisposable
    {
        /// <summary>
        /// 网页地址
        /// </summary>
        public string WebUrl { get; private set; }

        /// <summary>
        /// 读取网页
        /// </summary>
        /// <returns></returns>
        public string ReadingHtml()
        {
            return "";
        }

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放(不写析构函数的模式)
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
                handle.Dispose();
            disposed = true;
        }
    }
}
