using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extension.Mapping
{
    /// <summary>
    /// 实体映射到实体
    /// </summary>
    /// <typeparam name="TIn">输入</typeparam>
    /// <typeparam name="TOut">输出</typeparam>
    public class EntityToEntity<TIn, TOut> : IMapping<TIn, TOut>
    {
        /// <summary>
        /// 字段缓存
        /// </summary>
        readonly Func<TIn, TOut> cache = ReadingAndWrite();

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
        public TOut Mapping(TIn entity)
        {
            return cache(entity);
        }

        /// <summary>
        /// 映射值
        /// </summary>
        /// <param name="entity">数据行</param>
        /// <returns></returns>
        public static TOut MappingValue(TIn entity)
        {
            return new EntityToEntity<TIn, TOut>().Mapping(entity);
        }

        /// <summary>
        /// 映射值
        /// </summary>
        /// <param name="items">数据集</param>
        /// <returns></returns>
        public static IEnumerable<TOut> MappingValues(IEnumerable<TIn> items)
        {
            EntityToEntity<TIn, TOut> mapping = new EntityToEntity<TIn, TOut>();
            foreach (var item in items)
            {
                yield return mapping.Mapping(item);
            }
        }
    }
}
