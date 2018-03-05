using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extension.Microsoft;

namespace Utilities.SqlServer
{
    /// <summary>
    /// 大数据保存助手
    /// </summary>
    /// <typeparam name="TEntity">操作对象</typeparam>
    public class BigDataSaveHelper<TEntity> where TEntity : class
    {
        /// <summary>
        /// 实例化 大数据保存助手
        /// </summary>
        /// <param name="connectionName">链接字符串名</param>
        public BigDataSaveHelper(string connectionName)
        {
            this.ConnectionString = connectionName.ReadingConnectionStrings();
            this.DataTable = new DataTable();
            this.InstallDataTableStruct();
        }

        /// <summary>
        /// 链接字符串
        /// </summary>
        internal string ConnectionString { get; private set; }

        /// <summary>
        /// 操作对象类型
        /// </summary>
        readonly Type Type = typeof(TEntity);

        /// <summary>
        /// 数据表
        /// </summary>
        public DataTable DataTable { get; private set; }

        /// <summary>
        /// 初始化表结构
        /// </summary>
        internal void InstallDataTableStruct()
        {
            foreach (PropertyInfo property in GetMappedPropertys())
            {
                this.DataTable.Columns.Add(property.Name, property.PropertyType);
            }
        }

        /// <summary>
        /// 添加数据集
        /// </summary>
        /// <param name="items">数据集</param>
        internal void AddRange(IEnumerable<TEntity> items)
        {
            foreach (TEntity item in items)
            {
                DataRow row = this.DataTable.NewRow();
                foreach (PropertyInfo field in GetMappedPropertys())
                {
                    row[field.Name] = this.Type.GetProperty(field.Name).GetValue(item, null);
                }
                this.DataTable.Rows.Add(row);
            }
        }

        /// <summary>
        /// 保存到数据库中
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="items">要保存的数据集</param>
        public void SaveToDataBase(string tableName, IEnumerable<TEntity> items)
        {
            this.AddRange(items);

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(this.ConnectionString))
            {
                bulkCopy.DestinationTableName = tableName;
                foreach (PropertyInfo field in GetMappedPropertys())
                {
                    bulkCopy.ColumnMappings.Add(field.Name, field.Name);
                }

                bulkCopy.WriteToServer(this.DataTable);
            }
        }

        /// <summary>
        /// 获取映射字段
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<PropertyInfo> GetMappedPropertys()
        {
            foreach (PropertyInfo field in typeof(TEntity).GetProperties())
            {
                if (null == field.GetCustomAttribute<NotMappedAttribute>())
                    yield return field;
            }
        }
    }
}
