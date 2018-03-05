using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/*
“,”：枚举通配符，代表在指定的时间约定触发，比如"1,3,5"代表星期天、星期二和星期四触发
“*”：为通配符，代表所有
“/”：为间隔符，特别单位，表示为“每”如“0/15”表示每隔15分钟执行一次,“0”表示为从“0”分开始, “3/20”表示表示每隔20分钟执行一次，“3”表示从第3分钟开始执行
“-”：字符被用来指定一个范围。如：“10-12”在小时域意味着“10点、11点、12点”。
“?”：表示每月的某一天，或第周的某一天
“L”：用于每月，或每周，表示为每月的最后一天，或每个月的最后星期几如“6L”表示“每月的最后一个星期五”
“W”：表示为最近工作日，如“15W”放在每月（day-of-month）字段上表示为“到本月15日最近的工作日”
“C”：允许在日期域和星期域出现。这个字符依靠一个指定的“日历”。也就是说这个表达式的值依赖于相关的“日历”的计算结果，如果没有“日历”关联，则等价于所有包含的“日历”。如：日期域是“5C”表示关联“日历”中第一天，或者这个月开始的第一天的后5天。星期域是“1C”表示关联“日历”中第一天，或者星期的第一天的后1天，也就是周日的后一天（周一）。
“#”：是用来指定“的”每月第n个工作日,例 在每周（day-of-week）这个字段中内容为"6#3" or "FRI#3" 则表示“每月第三个星期五”
*/
namespace Utilities.Data.Cron.Observers.Resolve.Pipelines
{
    /// <summary>
    /// 格式验证管道
    /// </summary>
    public class VerifyFormatPipeline : Pipeline
    {
        /// <summary>
        /// 执行通道
        /// </summary>
        /// <param name="format">时间格式</param>
        /// <param name="data">待处理数据</param>
        /// <returns></returns>
        public override Tuple<bool, string, dynamic> ExcuteProccess(TimeFormat format, dynamic data)
        {
            Tuple<bool, string> tuple = null;
            switch (format)
            {
                case TimeFormat.Second:
                    tuple = this.VerifySecond(data);
                    break;
                case TimeFormat.Minute:
                    tuple = this.VerifyMinute(data);
                    break;
                case TimeFormat.Hour:
                    tuple = this.VerifyHour(data);
                    break;
                case TimeFormat.Day:
                    tuple = this.VerifyDay(data);
                    break;
                case TimeFormat.Month:
                    tuple = this.VerifyMonth(data);
                    break;
                case TimeFormat.DayOfWeek:
                    tuple = this.VerifyDayOfWeek(data);
                    break;
                case TimeFormat.Year:
                    tuple = this.VerifyYear(data);
                    break;
            }

            if (NextPipeline != null && tuple != null && tuple.Item1)
                return this.NextPipeline.ExcuteProccess(format, data);
            return new Tuple<bool, string, dynamic>(tuple.Item1, tuple.Item2, null);
        }

        /// <summary>
        /// 验证秒的格式
        /// </summary>
        /// <param name="cron">秒的cron切片</param>
        /// <returns></returns>
        protected virtual Tuple<bool, string> VerifySecond(string cron)
        {
            List<Match> items = SymbolSlice(cron, ",|\\*|-|/").ToList();
            var symbols = ReadingSymbol(items);
            string[] validSymbols = { ",", "-", "*", "/", "*/" };
            bool isValid = IsValidFormat(symbols, symbol => validSymbols.Contains(symbol));
            if (isValid)
                return new Tuple<bool, string>(true, "验证成功");

            return new Tuple<bool, string>(false, "存在无效符号，请检查。秒只包含一下特殊符号【, - * /】");
        }

        /// <summary>
        /// 验证分的格式
        /// </summary>
        /// <param name="cron">分的cron切片</param>
        /// <returns></returns>
        protected virtual Tuple<bool, string, string[]> VerifyMinute(string cron)
        {
            SymbolSlice(cron, ",|\\*|-|/");
            return null;
        }

        /// <summary>
        /// 验证小时的格式
        /// </summary>
        /// <param name="cron">小时的cron切片</param>
        /// <returns></returns>
        protected virtual Tuple<bool, string, string[]> VerifyHour(string cron)
        {
            SymbolSlice(cron, ",|\\*|-|/");
            return null;
        }

        /// <summary>
        /// 验证天的格式
        /// </summary>
        /// <param name="cron">天的cron切片</param>
        /// <returns></returns>
        protected virtual Tuple<bool, string, string[]> VerifyDay(string cron)
        {
            return null;
        }

        /// <summary>
        /// 验证月的格式
        /// </summary>
        /// <param name="cron">月的cron切片</param>
        /// <returns></returns>
        protected virtual Tuple<bool, string, string[]> VerifyMonth(string cron)
        {
            SymbolSlice(cron, ",|\\*|-|/");
            return null;
        }

        /// <summary>
        /// 验证星期的格式
        /// </summary>
        /// <param name="cron">星期的cron切片</param>
        /// <returns></returns>
        protected virtual Tuple<bool, string, string[]> VerifyDayOfWeek(string cron)
        {
            return null;
        }

        /// <summary>
        /// 验证年的格式
        /// </summary>
        /// <param name="cron">年的cron切片</param>
        /// <returns></returns>
        protected virtual Tuple<bool, string, string[]> VerifyYear(string cron)
        {
            SymbolSlice(cron, ",|\\*|-|/");
            return null;
        }

        /// <summary>
        /// 符号切片
        /// </summary>
        Func<string, string, IEnumerable<Match>> SymbolSlice = GetSymbolSlice;
        /// <summary>
        /// 获取符号切片
        /// </summary>
        /// <param name="cron">表达式</param>
        /// <param name="pattern">符号的正则表达式</param>
        /// <returns></returns>
        static IEnumerable<Match> GetSymbolSlice(string cron, string pattern)
        {
            var matchs = Regex.Matches(cron, pattern);
            foreach (Match match in matchs)
            {
                if (!string.IsNullOrEmpty(match.Value))
                    yield return match;
            }
        }

        /// <summary>
        /// 读取符号
        /// </summary>
        /// <param name="matchs">特殊符号切片</param>
        /// <returns></returns>
        string[] ReadingSymbol(List<Match> matchs)
        {
            List<string> items = null;
            if (matchs != null && matchs.Count > 0)
            {
                items = new List<string>();
                string current = string.Empty;// 当前符号
                for (int i = 0; i < matchs.Count; i++)
                {
                    current = $"{current}{matchs[i].Value}";
                    var currentIndex = matchs[i].Index;
                    if (i + 1 == matchs.Count)
                        items.Add(current);
                    else
                    {
                        var nextIndex = matchs[i + 1].Index;
                        if (currentIndex + 1 != nextIndex)
                        {
                            items.Add(current);
                            current = string.Empty;
                        }
                    }
                }
                return items.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 有效格式
        /// </summary>
        /// <param name="items">数据源</param>
        /// <param name="func">委托比较</param>
        /// <returns></returns>
        bool IsValidFormat(IEnumerable<string> items, Func<string, bool> func)
        {
            bool isValid = true;
            foreach (string item in items)
            {
                isValid = func(item);
                if (!isValid)
                    break;
            }
            return isValid;
        }
    }
}
