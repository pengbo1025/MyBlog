using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace sql封装
{
    public class SqlHelper
    {
        public static string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        
        public static SqlDataReader ExecuteReader(string constr, string cmdstr, CommandType type, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.CommandType = type;

            if (ps.Length > 0)
            {
                cmd.Parameters.AddRange(ps);
            }

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }


        public static int ExecuteNonQuery(string constr, string cmdstr, CommandType type, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.CommandType = type;

            if (ps.Length > 0)
            {
                cmd.Parameters.AddRange(ps);
            }

            int i = cmd.ExecuteNonQuery();

            return i;
        }


        public static DataSet ExecuteDataSet(string constr, string cmdstr, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(constr);

            SqlDataAdapter sda = new SqlDataAdapter(cmdstr, conn);

            if (ps.Length > 0)
            {
                sda.SelectCommand.Parameters.AddRange(ps);
            }

            DataSet ds = new DataSet();

            sda.Fill(ds);

            return ds;
        }


        public static object ExecuteScalar(string constr, string cmdstr, CommandType type, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdstr, conn);

            cmd.CommandType = type;

            if (ps.Length > 0)
            {
                cmd.Parameters.AddRange(ps);
            }

            object o = cmd.ExecuteScalar();

            return o;
        }

    }
}
