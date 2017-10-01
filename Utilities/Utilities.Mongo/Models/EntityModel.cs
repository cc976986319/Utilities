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
    public class EntityModel
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public EntityModel()
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
        public static string GetCollectionName<TEntity>() where TEntity : EntityModel
        {
            Attributes.MarkAttribute collection = typeof(TEntity).GetCustomAttribute<Attributes.MarkAttribute>();
            if (collection == null)
                return typeof(TEntity).Name;

            return collection.Name;
        }
    }
}
