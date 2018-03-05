using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Data.Cron
{
    /// <summary>
    /// 时间格式
    /// </summary>
    public enum TimeFormat : int
    {
        /// <summary>
        /// 秒
        /// </summary>
        Second = 0,
        /// <summary>
        /// 分
        /// </summary>
        Minute = 1,
        /// <summary>
        /// 小时
        /// </summary>
        Hour = 2,
        /// <summary>
        /// 天
        /// </summary>
        Day = 3,
        /// <summary>
        /// 月
        /// </summary>
        Month = 4,
        /// <summary>
        /// 周
        /// </summary>
        DayOfWeek = 5,
        /// <summary>
        /// 年
        /// </summary>
        Year = 6
    }
}
