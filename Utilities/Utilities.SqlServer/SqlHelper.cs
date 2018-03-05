using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extension.Microsoft;

namespace Utilities.SqlServer
{
    /// <summary>
    /// sql助手
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="Tout">输出数据</typeparam>
        /// <param name="connectionName">数据库链接字符串名</param>
        /// <param name="sql">查询sql</param>
        /// <param name="convert">自定义转换器</param>
        /// <returns></returns>
        public static Tout FindByTable<Tout>(string connectionName, string sql, Func<DataTable, Tout> convert)
        {
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;
            try
            {
                using (connection = new SqlConnection(connectionName.ReadingConnectionStrings()))
                {
                    using (adapter = new SqlDataAdapter(sql, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table != null && table.Rows != null && table.Rows.Count > 0)
                        {
                            return convert(table);
                        }
                    }
                }
                return default(Tout);
            }
            catch (Exception ex)
            {
                return default(Tout);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="Tout">输出数据</typeparam>
        /// <param name="connectionName">数据库链接字符串名</param>
        /// <param name="sql">查询sql</param>
        /// <param name="convert">自定义转换器</param>
        /// <returns></returns>
        public static IEnumerable<Tout> FindByRows<Tout>(string connectionName, string sql, Func<DataRow, Tout> convert)
        {
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;
            DataTable table = new DataTable();
            try
            {
                using (connection = new SqlConnection(connectionName.ReadingConnectionStrings()))
                {
                    using (adapter = new SqlDataAdapter(sql, connection))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    yield return convert(row);
                }
            }
        }
    }
}
