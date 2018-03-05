using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Data.Cron.Observers.Base
{
    /// <summary>
    /// 观察模型
    /// </summary>
    public abstract class Observer
    {
        /// <summary>
        /// cron解析器
        /// </summary>
        protected CronResolve CronResolve { get; set; }
        
        /// <summary>
        /// 解析操作
        /// </summary>
        /// <param name="cron">cron表达式字符串</param>
        public abstract void Resolve(string cron);

        /// <summary>
        /// 获取指定时间格式的cron表达式
        /// </summary>
        /// <param name="format">时间格式</param>
        /// <param name="cron">cron表达式</param>
        /// <returns></returns>
        protected static string GetTargetCronSlice(TimeFormat format, string cron)
        {
            return cron.Split(' ')[(int)format];
        }
    }
}
