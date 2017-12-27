using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mongo.Models
{
    /// <summary>
    /// Mongo实体(基实体)
    /// </summary>
    /// <remarks>
    /// [BsonIgnore]:字段忽略映射到数据库特性
    /// [BsonElement]:设置字段映射到数据库字段名
    /// </remarks>
    public class MongoEntity
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <remarks>
        /// [BsonIgnore]:是忽略本字段特性
        /// [BsonElement("Name")]:是标识字段名称(数据库字段名称)
        /// </remarks>
        public MongoEntity()
        {
            this._id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 编号(数据编号)
        /// </summary>
        public string _id { get; set; }

        /// <summary>
        /// 获取数据集名称
        /// </summary>
        /// <returns></returns>
        public static string GetCollectionName<TEntity>() where TEntity : MongoEntity
        {
            Attributes.CollectionAttribute collection = typeof(TEntity).GetCustomAttribute<Attributes.CollectionAttribute>();
            if (collection == null)
                return typeof(TEntity).Name;

            return collection.Name;
        }
    }
}
