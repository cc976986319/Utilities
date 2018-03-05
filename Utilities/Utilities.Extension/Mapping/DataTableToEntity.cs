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
    /// 数据表映射到实体
    /// </summary>
    public class DataTableToEntity<TOut> :IMapping<DataTable, IEnumerable<TOut>> where TOut : new()
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public DataTableToEntity()
        {
            this.DataRowToEntity = new DataRowToEntity<TOut>();
        }

        /// <summary>
        /// 数据行映射实体
        /// </summary>
        DataRowToEntity<TOut> DataRowToEntity { get; set; }

        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="table">待映射数据</param>
        /// <returns></returns>
        public IEnumerable<TOut> Mapping(DataTable table)
        {
            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    yield return this.DataRowToEntity.Mapping(row);
                }
            }
        }

        /// <summary>
        /// 映射值
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static IEnumerable<TOut> MappingValue(DataTable table)
        {
            return new DataTableToEntity<TOut>().Mapping(table);
        }
    }
}
