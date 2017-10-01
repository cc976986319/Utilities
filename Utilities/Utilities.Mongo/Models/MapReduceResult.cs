using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mongo.Models
{
    /// <summary>
    /// MapReduce实体
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ReduceResult<TResult>
    {
        /// <summary>
        /// 编号（Key）
        /// </summary>
        public dynamic _id { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public TResult value { get; set; } = default(TResult);
    }
}
