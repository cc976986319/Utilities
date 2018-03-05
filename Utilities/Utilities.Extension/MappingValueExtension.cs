using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extension
{
    /// <summary>
    /// 映射值
    /// </summary>
    public static class MappingValueExtension<TIn, TOut>
    {
        /// <summary>
        /// 字段缓存
        /// </summary>
        static readonly Func<TIn, TOut> cache = ReadingAndWrite();

        /// <summary>
        /// 读取并写入
        /// </summary>
        /// <returns></returns>
        static Func<TIn, TOut> ReadingAndWrite()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            foreach (var item in typeof(TOut).GetProperties())// 获取输出对象的字段或属性
            {
                if (!item.CanWrite)// 判断是否可写
                    continue;
                if (typeof(TIn).GetProperty(item.Name) != null)// 添加映射属性
                {
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });
            return lambda.Compile();
        }

        /// <summary>
        /// 映射值
        /// </summary>
        /// <param name="tIn">映射源</param>
        /// <returns>映射结果</returns>
        public static TOut Mapping(TIn tIn)
        {
            return cache(tIn);
        }
    }

    /// <summary>
    /// 映射值
    /// </summary>
    public static class MappingValueExtension
    {
        /// <summary>
        /// 映射值
        /// </summary>
        /// <typeparam name="TOut">输出参数</typeparam>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        public static TOut Mapping<TOut>(this DataRow row) where TOut : new()
        {
            TOut model = new TOut();
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                object value = null;
                if (row.Table.Columns.IndexOf(property.Name) >= 0)
                {
                    if (row[property.Name] == DBNull.Value)
                        value = null;
                    else
                        value = row[property.Name];
                }
                property.SetValue(model, value, null);
            }
            return model;
        }

        /// <summary>
        /// 映射值
        /// </summary>
        /// <typeparam name="TOut">输出参数</typeparam>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static IEnumerable<TOut> Mapping<TOut>(this DataTable table) where TOut : new()
        {
            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    yield return row.Mapping<TOut>();
                }
            }
        }
    }
}
