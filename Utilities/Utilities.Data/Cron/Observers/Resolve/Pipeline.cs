using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Data.Cron.Observers.Resolve
{
    /// <summary>
    /// 管道(管道模型)
    /// </summary>
    public abstract class Pipeline
    {
        /// <summary>
        /// 下一管道
        /// </summary>
        public Pipeline NextPipeline { get; protected set; }

        /// <summary>
        /// 执行通道
        /// </summary>
        /// <param name="format">时间格式</param>
        /// <param name="data">待处理数据</param>
        /// <returns></returns>
        public abstract Tuple<bool, string, dynamic> ExcuteProccess(TimeFormat format, dynamic data);

        /// <summary>
        /// 设置下一管道
        /// </summary>
        /// <param name="nextPipeline">下一处理管道</param>
        /// <returns></returns>
        public Pipeline SetNextPipeline(Pipeline nextPipeline)
        {
            if (nextPipeline != null)
            {
                this.NextPipeline = nextPipeline;
            }
            return this.NextPipeline;
        }
    }
}
