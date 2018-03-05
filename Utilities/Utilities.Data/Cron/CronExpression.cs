using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Data.Cron.Observers;
/* 
*    *     *      *    *     *     *
┬    ┬    ┬    ┬    ┬    ┬    ┬
│    │    │    │    │    │    │
│    │    │    │    │    │    │
│    │    │    │    │    │    │
│    │    │    │    │    │    └ 年                                          【允许的通配符[, - * /] 非必填】
│    │    │    │    │    └ 周 (0 - 7) (0 or 7 is Sun)                        【允许的通配符[, - * / L C #]】
│    │    │    │    └───── 月 (1 - 12)                                   【允许的通配符[, - * /]】
│    │    │    └────────── 天 of month (1 - 31)                      【允许的通配符[, - * / L W C]】
│    │    └─────────────── 小时 (0 - 23)                         【允许的通配符[, - * /]】
│    └──────────────────── 分 (0 - 59)                       【允许的通配符[, - * /]】
└───────────────────────── 秒 (0 - 59, optional)         【允许的通配符[, - * /]】

“,”：枚举通配符，代表在指定的时间约定触发，比如"1,3,5"代表星期天、星期二和星期四触发
“*”：为通配符，代表所有
“/”：为间隔符，特别单位，表示为“每”如“0/15”表示每隔15分钟执行一次,“0”表示为从“0”分开始, “3/20”表示表示每隔20分钟执行一次，“3”表示从第3分钟开始执行
“-”：字符被用来指定一个范围。如：“10-12”在小时域意味着“10点、11点、12点”。
“?”：占位符，表示每月的某一天，或第周的某一天
“L”：用于每月，或每周，表示为每月的最后一天，或每个月的最后星期几如“6L”表示“每月的最后一个星期五”
“W”：表示为最近工作日，如“15W”放在每月（day-of-month）字段上表示为“到本月15日最近的工作日”
“C”：允许在日期域和星期域出现。这个字符依靠一个指定的“日历”。也就是说这个表达式的值依赖于相关的“日历”的计算结果，如果没有“日历”关联，则等价于所有包含的“日历”。如：日期域是“5C”表示关联“日历”中第一天，或者这个月开始的第一天的后5天。星期域是“1C”表示关联“日历”中第一天，或者星期的第一天的后1天，也就是周日的后一天（周一）。
“#”：是用来指定“的”每月第n个工作日,例 在每周（day-of-week）这个字段中内容为"6#3" or "FRI#3" 则表示“每月第三个星期五”
*/
namespace Utilities.Data.Cron
{
    /// <summary>
    /// cron表达式
    /// </summary>
    public class CronExpression
    {
        CronExpression() { }

        /// <summary>
        /// 解析cron表达式
        /// </summary>
        /// <param name="cron">cron表达式字符串</param>
        /// <returns></returns>
        public static CronExpression ResolveCron(string cron)
        {
            CronResolve resolve = new CronResolve(new CronExpression());// cron解析器

            // 注册解析成员
            SecondObserver secondObserver = new SecondObserver(resolve);// 秒
            MinuteObserver minuteObserver = new MinuteObserver(resolve);// 分
            HourObserver hourObserver = new HourObserver(resolve);// 小时
            DayObserver dayObserver = new DayObserver(resolve);// 天
            MonthObserver monthObserver = new MonthObserver(resolve);// 月
            DayOfWeekObserver dayOfWeekObserver = new DayOfWeekObserver(resolve);// 星期
            YearObserver yearObserver = new YearObserver(resolve);// 年

            return resolve.Resolve(cron);// 解析
        }

        /// <summary>
        /// 后十次执行时间
        /// </summary>
        /// <returns></returns>
        public List<DateTime> NextTenTimeExtension()
        {
            string[] symbols = { "L", "M", "C", "#" };

            bool isExist = symbols.Any(e => this.CronString.IndexOf(e) >= 0);

            return null;
        }

        /// <summary>
        /// Cron表达式字符串
        /// </summary>
        public string CronString { get; internal set; }

        /// <summary>
        /// Cron切片
        /// </summary>
        public string[] CronSlice { get { return string.IsNullOrEmpty(this.CronString) ? null : this.CronString.Split(' '); } }

        /// <summary>
        /// 有指定分片的cron表达式
        /// </summary>
        /// <param name="format">时间格式</param>
        /// <returns></returns>
        bool IsHaveTargetCronSlice(TimeFormat format)
        {
            return string.IsNullOrEmpty(this.CronString) || this.CronSlice.Length > (int)format;
        }

        /// <summary>
        /// 获取指定cron切片
        /// </summary>
        /// <param name="format">时间格式</param>
        /// <returns></returns>
        public string GetTargetCronClice(TimeFormat format)
        {
            if (this.IsHaveTargetCronSlice(format))
                return this.CronSlice[(int)format];
            return null;
        }

        /// <summary>
        /// 秒（可执行时间片）
        /// </summary>
        public int[] Seconds { get; internal set; }

        /// <summary>
        /// 分（可执行时间片）
        /// </summary>
        public int[] Minutes { get; internal set; }

        /// <summary>
        /// 小时（可执行时间片）
        /// </summary>
        public int[] Hours { get; internal set; }

        /// <summary>
        /// 天（可执行时间片）
        /// </summary>
        public int[] Days { get; internal set; }

        /// <summary>
        /// 月（可执行时间片）
        /// </summary>
        public int[] Months { get; internal set; }

        /// <summary>
        /// 星期（可执行时间片）
        /// </summary>
        public int[] DayOfWeeks { get; internal set; }

        /// <summary>
        /// 年（可执行时间片）
        /// </summary>
        public int[] Years { get; internal set; }
    }
}
