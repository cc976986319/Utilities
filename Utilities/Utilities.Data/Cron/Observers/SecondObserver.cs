﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Data.Cron.Observers.Resolve;

namespace Utilities.Data.Cron.Observers
{
    /// <summary>
    /// 秒 观察者
    /// </summary>
    /// <remarks>
    ///秒    分   小时   天   月    周     年
    ///*     *     *     *     *     *     *
    ///┬    ┬    ┬    ┬    ┬    ┬    ┬
    ///│
    ///│
    ///└───────────────────────── 秒(0 - 59, optional)         【允许的通配符[, -* /]】
    /// </remarks>
    public class SecondObserver : Base.Observer
    {
        /// <summary>
        /// 实例化 秒 观察者
        /// </summary>
        /// <param name="resolve">cron解析器</param>
        public SecondObserver(CronResolve resolve)
        {
            this.CronResolve = resolve;
            resolve.CronExpressionResolveEvent += new ResolveCronExpression(this.Resolve);
        }

        /// <summary>
        /// 解析操作
        /// </summary>
        /// <param name="cron">cron表达式字符串</param>
        public override void Resolve(string cron)
        {
            string _cron = GetTargetCronSlice(TimeFormat.Second, cron);
            Tuple<bool, string, dynamic> value = PipelineController.SecondDisposePipeline(_cron);
            if (value.Item1)
                this.CronResolve.CronExpression.Days = value.Item3;
        }
    }
}
