using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extension
{
    /// <summary>
    /// 映射接口
    /// </summary>
    /// <typeparam name="TIn">输入</typeparam>
    /// <typeparam name="TOut">输出</typeparam>
    public interface IMapping<TIn, TOut>
    {
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="entity">待映射数据</param>
        /// <returns></returns>
        TOut Mapping(TIn entity);
    }
}
