using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Data.Cron
{
    /// <summary>
    /// 解析cron表达式
    /// </summary>
    /// <param name="cron">cron表达式字符串</param>
    public delegate void ResolveCronExpression(string cron);

    /// <summary>
    /// cron解析器
    /// </summary>
    public class CronResolve
    {
        /// <summary>
        /// 实例化cron解析器
        /// </summary>
        /// <param name="expression">cron表达式</param>
        public CronResolve(CronExpression expression)
        {
            this.CronExpression = expression;
        }

        /// <summary>
        /// cron表达式解析事件
        /// </summary>
        public event ResolveCronExpression CronExpressionResolveEvent;

        /// <summary>
        /// cron表达式
        /// </summary>
        internal CronExpression CronExpression { get; set; }

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="cron">cron表达式字符串</param>
        public CronExpression Resolve(string corn)
        {
            this.CronExpression.CronString = corn;
            if (CronExpressionResolveEvent == null)
                throw new ArgumentNullException("没有解析者经行订阅");
            CronExpressionResolveEvent(corn);

            return CronExpression;
        }
    }
}
