using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    /// <summary>
    /// 打印帮助类，定义各种方便的打印方法，便于调试
    /// </summary>
    public class Dumper
    {

        /// <summary>
        /// 格式化输出datatable的内容到Debug
        /// </summary>
        /// <param name="Dt"></param>
        public static void DumpDataTableToDebug(DataTable Dt)
        {
            StringBuilder debug = new StringBuilder(" ------------------------------------\n");

            if (Dt != null)
            {
                int DtRowsCount = Dt.Rows.Count;
                int DtColumnsCount = Dt.Columns.Count;
                debug.Append("[ 表名：" + Dt.TableName + " 行数：" + DtRowsCount + " 列数：" + DtColumnsCount + " ]\n");
                if (DtRowsCount < 1) debug.Append(" 无表格行数据！\n");
                else
                {
                    debug.Append("            |");
                    for (int j = 0; j < DtColumnsCount; j++)
                    {
                        string ColumnName = Dt.Columns[j].ColumnName.ToString();
                        string ColumnType = Dt.Columns[j].DataType.ToString();
                        debug.Append(" " + ColumnName + " | ");
                    }
                    debug.Append("\n");
                    for (int i = 0; i < DtRowsCount; i++)
                    {
                        debug.Append("     " + (i + 1) + "      |");
                        for (int j = 0; j < DtColumnsCount; j++)
                        {
                            string ColumnName = Dt.Columns[j].ColumnName.ToString();
                            string ColumnType = Dt.Columns[j].DataType.ToString();
                            debug.Append(" " + Dt.Rows[i][ColumnName].ToString() + " | ");
                        }
                        debug.Append("\n");
                    }
                }
            }
            else
            {
                debug.Append("   操蛋！ DataTable竟然是NULL。。。。。。");
            }

            Debug.WriteLine(debug.ToString());
        }

        /// <summary>
        /// 纯字符串的dump
        /// </summary>
        /// <param name="dump"></param>
        public static void dumpToDebug(string dump) {
            Debug.WriteLine(dump);
        }

    }
}
