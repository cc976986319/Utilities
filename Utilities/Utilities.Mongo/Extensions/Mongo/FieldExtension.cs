using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities.Mongo.Models;

namespace Utilities.Mongo.Extensions.Mongo
{
    /// <summary>
    /// 字段扩展
    /// </summary>
    public static class FieldExtension
    {
        /// <summary>
        /// 标识为修改
        /// </summary>
        /// <typeparam name="TDocument">文档：<see cref="{TDocument}"/>必须继承于<see cref="{EntityModel}"/></typeparam>
        /// <param name="document"></param>
        /// <returns></returns>
        public static UpdateDefinition<TDocument> MarkUpdateDefinition<TDocument>(this TDocument document) where TDocument : MongoEntity
        {
            var updateDefinitionList = MarkUpdateDefinitionByField(document).ToList();// 获取需要标识的字段

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<TDocument>().Combine(updateDefinitionList);// 组装

            return updateDefinitionBuilder;

            IEnumerable<UpdateDefinition<TDocument>> MarkUpdateDefinitionByField(TDocument _document)
            {
                PropertyInfo[] propertyInfos = _document.GetType().GetProperties().Where(e => e.Name != "_id").ToArray();// 获取非编号的所有字段

                foreach (PropertyInfo propertyInfo in propertyInfos)// 标识为修改标识
                {
                    if (propertyInfo.PropertyType.IsArray || typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))// 字段包含子文档
                    {
                        var value = propertyInfo.GetValue(_document) as IList;

                        var filedName = propertyInfo.Name;

                        yield return Builders<TDocument>.Update.Set(filedName, value);
                    }
                    else
                    {
                        var value = propertyInfo.GetValue(_document);
                        if (propertyInfo.PropertyType == typeof(decimal))
                            value = value.ToString();

                        var filedName = propertyInfo.Name;

                        yield return Builders<TDocument>.Update.Set(filedName, value);
                    }
                }
            };
        }
    }
}
