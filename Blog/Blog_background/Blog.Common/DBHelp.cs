using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;    
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;



    /// <summary>
    /// 数据库访问访问辅助工具类
    /// </summary>
    public static class DBHelp
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public readonly static string ConnectionString = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        //public static string ConnectionString = "";


        static DBHelp()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //

        }

        /// <summary>
        /// 执行返回受的行数的TSQL语句
        /// </summary>
        /// <param name="sql">TSQL语句</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="comtype">命令执行器类型</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, string dbtype, CommandType comtype, params object[] param)
        {
            int result = 0;
            switch (dbtype)
            {
                case "SQL2005":
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand com = con.CreateCommand())
                        {
                            com.CommandType = comtype;
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    SqlParameter p = new SqlParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            result = com.ExecuteNonQuery();
                            con.Close();
                            com.Dispose();
                        }
                    }
                    break;
                case "OleDB":
                    using (OleDbConnection con = new OleDbConnection(ConnectionString))
                    {
                        using (OleDbCommand com = con.CreateCommand())
                        {
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    OleDbParameter p = new OleDbParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            if (con.State == ConnectionState.Closed)
                                con.Open();
                            result = com.ExecuteNonQuery();
                            con.Close();
                            com.Dispose();
                        }
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// 执行返回结果集中第一行第一列的TSQL语句
        /// </summary>
        /// <param name="sql">TSQL语句</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="comtype">命令执行器类型</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, string dbtype, CommandType comtype, params object[] param)
        {
            object result = null;
            switch (dbtype)
            {
                case "SQL2005":
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand com = conn.CreateCommand())
                        {
                            com.CommandType = comtype;
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    SqlParameter p = new SqlParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();
                            result = com.ExecuteScalar();
                            conn.Close();
                        }
                    }
                    break;
                case "OleDB":
                    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                    {
                        using (OleDbCommand com = conn.CreateCommand())
                        {
                            com.CommandType = comtype;
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    OleDbParameter p = new OleDbParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();
                            result = com.ExecuteScalar();
                            conn.Close();
                            com.Dispose();
                        }
                    }
                    break;
                default:
                    result = null;
                    break;
            }
            return result;   
        }

        /// <summary>
        /// 执行返回SqlDataReader对象的TSQL语句
        /// </summary>
        /// <param name="sql">TSQL语句</param>
        /// <param name="comtype">命令执行器类型</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteSqlReader(string sql, CommandType comtype, params object[] param)
        {
            SqlDataReader result = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = comtype;
                com.CommandText = sql;
                if (param != null && param.Length > 0)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        SqlParameter p = new SqlParameter("@" + i, param[i]);
                        com.Parameters.Add(p);
                    }
                }
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                result = com.ExecuteReader(CommandBehavior.CloseConnection);
                return result;
            }

        }

        /// <summary>
        /// 执行返回OleDataReader对象的TSQL语句
        /// </summary>
        /// <param name="sql">TSQL语句</param>
        /// <param name="comtype">命令执行器类型</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteOleReader(string sql, CommandType comtype, params object[] param)
        {
            OleDbDataReader result = null;
            //using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                OleDbConnection conn = new OleDbConnection(ConnectionString);
                using (OleDbCommand com = conn.CreateCommand())
                {
                    com.CommandType = comtype;
                    com.CommandText = sql;
                    if (param != null && param.Length > 0)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            OleDbParameter p = new OleDbParameter("@" + i, param[i]);
                            com.Parameters.Add(p);
                        }
                    }
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    result = com.ExecuteReader(CommandBehavior.CloseConnection);
                    return result;
                }
            }
        }

        /// <summary>
        /// 执行返回DataTable对象的TSQL语句
        /// </summary>
        /// <param name="sql">TSQL语句</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="comtype">命令执行器类型</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static DataTable ExecteDataTable(string sql,string dbtype,CommandType comtype, params object[] param)
        {
            DataTable result = new DataTable();
            switch (dbtype)
            {
                case "SQL2005":
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand com = conn.CreateCommand())
                        {
                            com.CommandType = comtype;
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    SqlParameter p = new SqlParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(com))
                            {
                                sda.Fill(result);
                            }
                        }
                    }
                    break;
                case "OleDB":
                    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                    {
                        using (OleDbCommand com = conn.CreateCommand())
                        {
                            com.CommandType = comtype;
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    OleDbParameter p = new OleDbParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            using (OleDbDataAdapter sda = new OleDbDataAdapter(com))
                            {
                                sda.Fill(result);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }


            return result;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum Databasetype
        {
            SQL2005,
            OleDB
        }

        /// <summary>
        /// 执行返回DataSet对象的TSQL语句
        /// </summary>
        /// <param name="sql">TSQL语句</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="comtype">命令执行器类型</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static DataSet ExecteDataSet(string sql,string dbtype,CommandType comtype, params object[] param)
        {
            DataSet result = new DataSet();

            switch (dbtype)
            {
                case "SQL2005":
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand com = conn.CreateCommand())
                        {
                            com.CommandType = comtype;
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    SqlParameter p = new SqlParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(com))
                            {
                                sda.Fill(result);
                            }
                        }
                    }
                    break;
                case "OleDB":
                    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                    {
                        using (OleDbCommand com = conn.CreateCommand())
                        {
                            com.CommandType = comtype;
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    OleDbParameter p = new OleDbParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            using (OleDbDataAdapter sda = new OleDbDataAdapter(com))
                            {
                                sda.Fill(result);
                            }
                        }
                    }
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        /// <summary>
        /// 用于分页的执行返回DataSet对象的TSQL语句
        /// </summary>
        /// <param name="sql">TSQL语句</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="tablename">表名</param>
        /// <param name="start">起始数值</param>
        /// <param name="size">每页数目</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static DataSet ExecteDataSet(string sql, string dbtype,string tablename, int start, int size, params object[] param)
        {
            DataSet result = new DataSet();
            switch (dbtype)
            {
                case "SQL2005":
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand com = conn.CreateCommand())
                        {
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    SqlParameter p = new SqlParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            using (SqlDataAdapter sda = new SqlDataAdapter(com))
                            {
                                sda.Fill(result, start, size, tablename);
                            }
                        }
                    }
                    break;
                case "OleDB":
                    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                    {
                        using (OleDbCommand com = conn.CreateCommand())
                        {
                            com.CommandText = sql;
                            if (param != null && param.Length > 0)
                            {
                                for (int i = 0; i < param.Length; i++)
                                {
                                    OleDbParameter p = new OleDbParameter("@" + i, param[i]);
                                    com.Parameters.Add(p);
                                }
                            }
                            using (OleDbDataAdapter sda = new OleDbDataAdapter(com))
                            {
                                sda.Fill(result, start, size, tablename);
                            }
                        }
                    }
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }



    }
