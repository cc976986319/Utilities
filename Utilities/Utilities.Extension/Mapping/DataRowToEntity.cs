using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extension.Mapping
{
    /// <summary>
    /// 数据行映射到实体
    /// </summary>
    /// <typeparam name="TOut">实体类型</typeparam>
    public class DataRowToEntity<TOut> : IMapping<DataRow, TOut> where TOut : new()
    {
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="entity">待映射数据</param>
        /// <returns></returns>
        public TOut Mapping(DataRow entity)
        {
            TOut model = new TOut();
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                dynamic value = null;
                if (entity.Table.Columns.IndexOf(property.Name) >= 0)
                {
                    if (entity[property.Name] == DBNull.Value)
                        value = null;
                    else
                        value = entity[property.Name];
                }
                property.SetValue(model, value, null);
            }
            return model;
        }
        
        /// <summary>
        /// 映射值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        public static TOut MappingValue(DataRow row)
        {
            return new DataRowToEntity<TOut>().Mapping(row);
        }
    }
}
