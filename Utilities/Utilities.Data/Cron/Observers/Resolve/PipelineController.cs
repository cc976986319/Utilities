using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Data.Cron.Observers.Resolve
{
    /// <summary>
    /// 管道控制器
    /// </summary>
    public class PipelineController
    {
        /// <summary>
        /// 秒 处理管道
        /// </summary>
        public static dynamic SecondDisposePipeline(string secondCron)
        {
            Pipeline pipeline = new Pipelines.VerifyFormatPipeline();// 初始化管道
            pipeline.SetNextPipeline(new Pipelines.ResolvePipeline());// 设计管道
            return pipeline.ExcuteProccess(TimeFormat.Second, secondCron);// 执行
        }

        /// <summary>
        /// 分 处理管道
        /// </summary>
        public static dynamic MinuteDisposePipeline(string minuteCron)
        {
            Pipeline pipeline = new Pipelines.VerifyFormatPipeline();// 初始化管道
            pipeline.SetNextPipeline(new Pipelines.ResolvePipeline());// 设计管道
            return pipeline.ExcuteProccess(TimeFormat.Minute, minuteCron);// 执行
        }

        /// <summary>
        /// 小时 处理管道
        /// </summary>
        public static dynamic HourDisposePipeline(string hourCron)
        {
            Pipeline pipeline = new Pipelines.VerifyFormatPipeline();// 初始化管道
            pipeline.SetNextPipeline(new Pipelines.ResolvePipeline());// 设计管道
            return pipeline.ExcuteProccess(TimeFormat.Hour, hourCron);// 执行
        }

        /// <summary>
        /// 天 处理管道
        /// </summary>
        public static dynamic DayDisposePipeline(string dayCron)
        {
            Pipeline pipeline = new Pipelines.VerifyFormatPipeline();// 初始化管道
            pipeline.SetNextPipeline(new Pipelines.ResolvePipeline());// 设计管道
            return pipeline.ExcuteProccess(TimeFormat.Day, dayCron);// 执行
        }

        /// <summary>
        /// 月 处理管道
        /// </summary>
        public static dynamic MonthDisposePipeline(string monthCron)
        {
            Pipeline pipeline = new Pipelines.VerifyFormatPipeline();// 初始化管道
            pipeline.SetNextPipeline(new Pipelines.ResolvePipeline());// 设计管道
            return pipeline.ExcuteProccess(TimeFormat.Month, monthCron);// 执行
        }

        /// <summary>
        /// 星期 处理管道
        /// </summary>
        public static dynamic DayOfWeekDisposePipeline(string dayOfWeekCron)
        {
            Pipeline pipeline = new Pipelines.VerifyFormatPipeline();// 初始化管道
            pipeline.SetNextPipeline(new Pipelines.ResolvePipeline());// 设计管道
            return pipeline.ExcuteProccess(TimeFormat.DayOfWeek, dayOfWeekCron);// 执行
        }

        /// <summary>
        /// 年 处理管道
        /// </summary>
        public static dynamic YearDisposePipeline(string yearCron)
        {
            Pipeline pipeline = new Pipelines.VerifyFormatPipeline();// 初始化管道
            pipeline.SetNextPipeline(new Pipelines.ResolvePipeline());// 设计管道
            return pipeline.ExcuteProccess(TimeFormat.Year, yearCron);// 执行
        }
    }
}
