using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
*/
namespace Utilities.Data.Cron.Observers.Resolve.Pipelines
{
    /// <summary>
    /// 解析管道
    /// </summary>
    public class ResolvePipeline : Pipeline
    {
        /// <summary>
        /// 60(分、秒)
        /// </summary>
        public int[] Sixty = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 };
        /// <summary>
        /// 24(小时)
        /// </summary>
        public int[] TwentyFour = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        /// <summary>
        /// 31(天)
        /// </summary>
        public int[] ThirtyOne = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        /// <summary>
        /// 12(月份)
        /// </summary>
        public int[] Twelve = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        /// <summary>
        /// 7(星期)
        /// </summary>
        public int[] Seven = { 0, 1, 2, 3, 4, 5, 6 };
        /// <summary>
        /// 星期
        /// </summary>
        public string[] DayOfWeeks_EN = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        /// <summary>
        /// 执行通道
        /// </summary>
        /// <param name="format">时间格式</param>
        /// <param name="data">待处理数据</param>
        /// <returns></returns>
        public override Tuple<bool, string, dynamic> ExcuteProccess(TimeFormat format, dynamic data)
        {
            int[] items = null;
            switch (format)
            {
                case TimeFormat.Second:
                    items = this.ResolveSecond(data);
                    break;
                case TimeFormat.Minute:
                    items = this.ResolveMinute(data);
                    break;
                case TimeFormat.Hour:
                    items = this.ResolveHour(data);
                    break;
                case TimeFormat.Day:
                    items = this.ResolveDay(data);
                    break;
                case TimeFormat.Month:
                    items = this.ResolveMonth(data);
                    break;
                case TimeFormat.DayOfWeek:
                    items = this.ResolveDayOfWeek(data);
                    break;
                case TimeFormat.Year:
                    items = this.ResolveYear(data);
                    break;
            }
            return new Tuple<bool, string, dynamic>(true, "解析完成", items);
        }

        /// <summary>
        /// 解析秒
        /// </summary>
        /// <param name="cron">秒的cron</param>
        /// <returns></returns>
        /// <remarks>【允许的通配符[, - * /]】</remarks>
        int[] ResolveSecond(string cron)
        {
            List<int> result = new List<int>();
            List<string> waitResolve = new List<string>() { cron };

            this.ResolveComma(result, waitResolve, Sixty);
            this.ResolveSlash(result, waitResolve, Sixty);
            this.ResolveHyphen(result, waitResolve, Sixty);
            this.ResolveAsterisk(result, waitResolve, Sixty);

            return result.Distinct().OrderBy(e => e).ToArray();
        }

        /// <summary>
        /// 解析分
        /// </summary>
        /// <param name="cron">分的cron</param>
        /// <returns></returns>
        /// <remarks>【允许的通配符[, - * /]】</remarks>
        int[] ResolveMinute(string cron)
        {
            List<int> result = new List<int>();
            List<string> waitResolve = new List<string>() { cron };

            this.ResolveComma(result, waitResolve, Sixty);
            this.ResolveSlash(result, waitResolve, Sixty);
            this.ResolveHyphen(result, waitResolve, Sixty);
            this.ResolveAsterisk(result, waitResolve, Sixty);

            return result.Distinct().OrderBy(e => e).ToArray();
        }

        /// <summary>
        /// 解析小时
        /// </summary>
        /// <param name="cron">小时的cron</param>
        /// <returns></returns>
        /// <remarks>【允许的通配符[, - * /]】</remarks>
        int[] ResolveHour(string cron)
        {
            List<int> result = new List<int>();
            List<string> waitResolve = new List<string>() { cron };

            this.ResolveComma(result, waitResolve, TwentyFour);
            this.ResolveSlash(result, waitResolve, TwentyFour);
            this.ResolveHyphen(result, waitResolve, TwentyFour);
            this.ResolveAsterisk(result, waitResolve, TwentyFour);

            return result.Distinct().OrderBy(e => e).ToArray();
        }

        /// <summary>
        /// 解析天
        /// </summary>
        /// <param name="cron">天的cron</param>
        /// <returns></returns>
        /// <remarks>【允许的通配符[, - * / L W C]】</remarks>
        int[] ResolveDay(string cron)
        {
            List<int> result = new List<int>();
            List<string> waitResolve = new List<string>() { cron };

            this.ResolveComma(result, waitResolve, Twelve);
            this.ResolveSlash(result, waitResolve, Twelve);
            this.ResolveHyphen(result, waitResolve, Twelve);
            this.ResolveAsterisk(result, waitResolve, Twelve);
            // L W C【属于天的特殊解析】

            return result.Distinct().Select(e => e + 1).OrderBy(e => e).ToArray();
        }

        /// <summary>
        /// 解析月
        /// </summary>
        /// <param name="cron">月的cron</param>
        /// <returns></returns>
        /// <remarks>【允许的通配符[, - * /]】</remarks>
        int[] ResolveMonth(string cron)
        {
            List<int> result = new List<int>();
            List<string> waitResolve = new List<string>() { cron };

            this.ResolveComma(result, waitResolve, Twelve);
            this.ResolveSlash(result, waitResolve, Twelve);
            this.ResolveHyphen(result, waitResolve, Twelve);
            this.ResolveAsterisk(result, waitResolve, Twelve);

            return result.Distinct().Select(e => e + 1).OrderBy(e => e).ToArray();
        }

        /// <summary>
        /// 解析星期
        /// </summary>
        /// <param name="cron">星期的cron</param>
        /// <returns></returns>
        /// <remarks>【允许的通配符[, - * / L C #]】</remarks>
        int[] ResolveDayOfWeek(string cron)
        {
            List<int> result = new List<int>();
            List<string> waitResolve = new List<string>() { cron };

            this.ResolveComma(result, waitResolve, Twelve);
            this.ResolveSlash(result, waitResolve, Twelve);
            this.ResolveHyphen(result, waitResolve, Twelve);
            this.ResolveAsterisk(result, waitResolve, Twelve);
            // 【L C #】属于星期的特殊解析

            return result.Distinct().OrderBy(e => e).ToArray();
        }

        /// <summary>
        /// 解析年
        /// </summary>
        /// <param name="cron">年的cron</param>
        /// <returns></returns>
        /// <remarks>【允许的通配符[, - * /] 非必填】</remarks>
        int[] ResolveYear(string cron)
        {
            int[] years = new int[120];// 120轮回(中国天干地支)
            for (int i = 0; i < 120; i++)
            {
                if (i == 0)
                    years[i] = DateTime.Now.Year;
                else
                    years[i] = years[i - 1] + 1;
            }
            List<int> result = new List<int>();
            List<string> waitResolve = new List<string>() { cron };

            this.ResolveComma(result, waitResolve, years);
            this.ResolveSlash(result, waitResolve, years);
            this.ResolveHyphen(result, waitResolve, years);
            this.ResolveAsterisk(result, waitResolve, years);

            return result.Distinct().OrderBy(e=>e).ToArray();
        }

        /// <summary>
        /// 解析逗号（,）
        /// </summary>
        /// <param name="resolveResult">解析结果</param>
        /// <param name="nextResolve">待解析列表</param>
        /// <param name="crons">待解析表达式</param>
        internal void ResolveComma(List<int> resolveResult, List<string> nextResolve, int[] source)
        {
            List<string> crons = nextResolve.Where(e => e.IndexOf(',') > 0).ToList();
            foreach (string cron in crons)
            {
                string[] items = cron.Split(',');
                foreach (string item in items)
                {
                    int value = -1;
                    bool isAdd = int.TryParse(item, out value);
                    if (isAdd)
                    {
                        resolveResult.Add(value);
                    }
                    else
                    {
                        nextResolve.Add(item);
                    }
                }
            }
            int removeCount = nextResolve.RemoveAll(e => e.IndexOf(',') > 0);// 移除数量
        }

        /// <summary>
        /// 解析斜线（/）
        /// </summary>
        /// <param name="resolveResult">解析结果</param>
        /// <param name="nextResolve">待解析列表</param>
        /// <param name="crons">待解析表达式</param>
        internal void ResolveSlash(List<int> resolveResult, List<string> nextResolve, int[] source)
        {
            List<string> _crons = nextResolve.Where(e => e.IndexOf("/") > 0).ToList();
            foreach (string cron in _crons)
            {
                string[] items = cron.Split('/');
                int left = items[0] == "*" ? 0 : int.Parse(items[0]), rigth = int.Parse(items[1]);
                int value = left;
                while (value < source.Length)
                {
                    resolveResult.Add(value);
                    value += rigth;
                }
            }
            int removeCount = nextResolve.RemoveAll(e => e.IndexOf("/") > 0);// 移除数量
        }

        /// <summary>
        /// 解析链接符（-）
        /// </summary>
        /// <param name="resolveResult">解析结果</param>
        /// <param name="nextResolve">待解析列表</param>
        /// <param name="crons">待解析表达式</param>
        internal void ResolveHyphen(List<int> resolveResult, List<string> nextResolve, int[] source)
        {
            List<string> _crons = nextResolve.Where(e => e.IndexOf("-") > 0).ToList();
            foreach (string cron in _crons)
            {
                string[] items = cron.Split('-');
                int left = int.Parse(items[0]), rigth = int.Parse(items[1]);
                for (int i = left; i <= rigth; i++)
                {
                    resolveResult.Add(i);
                }
            }
            int removeCount = nextResolve.RemoveAll(e => e.IndexOf("/") > 0);// 移除数量
        }

        /// <summary>
        /// 解析星号（*）
        /// </summary>
        /// <param name="resolveResult"></param>
        /// <param name="nextResolve"></param>
        internal void ResolveAsterisk(List<int> resolveResult, List<string> nextResolve, int[] source)
        {
            string cron = nextResolve.Where(e => e.IndexOf("*") > 0).FirstOrDefault();
            if (cron == "*") resolveResult.AddRange(source);
        }
    }
}
