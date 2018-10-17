/**  版本信息模板在安装目录下，可自行修改。
* CaegoryDao.cs
*
* 功 能： N/A
* 类 名： CaegoryDao
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/1 14:33:28   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Blog.Model;
using sql封装;

namespace Blog.DAL
{
	/// <summary>
	/// 数据访问类:Caegory
	/// </summary>
	public partial class CaegoryDao
	{
		public CaegoryDao()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
            string sql = "select top 1 ID from Caegory order by ID desc";
            object obj = DBHelp.ExecuteScalar(sql, DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text);
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
            string sql = "select count(1) from Caegory where [ID]=@0";
            object obj = DBHelp.ExecuteScalar(sql, DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text,ID);
            if (obj == null)
                return false;
            else if (obj.ToString() == "0")
                return false;
            else
                return true;
		}

        public List<CaegoryInfo> DataSetToList(DataSet ds)
        {
            List<CaegoryInfo> list = new List<CaegoryInfo>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(DataRowToModel(row));
            }
            return list;
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CaegoryInfo model)
		{
            string sql = "insert into Caegory([CategoryName],[Categry_describe])" +
            " values (@0,@1) select @@identity";

			object obj = DBHelp.ExecuteScalar(sql,DBHelp.Databasetype.SQL2005.ToString(),CommandType.Text,model.CategoryName,model.Categry_describe);
              
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CaegoryInfo model)
		{

            string sql = "UPDATE [Caegory] SET [CategoryName] = @0,[Categry_describe] = @1 WHERE [ID]=@2";

            int rows = DBHelp.ExecuteNonQuery(sql, DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text,model.CategoryName,model.Categry_describe,model.ID );
 
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
            string sql = "delete from Caegory where [ID]=@0";
			
			int rows=DBHelp.ExecuteNonQuery(sql,DBHelp.Databasetype.SQL2005.ToString(),CommandType.Text,ID);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
            string sql = "delete from Caegory where [ID]  in (" + idlist + ") ";

            int rows = DBHelp.ExecuteNonQuery(sql, DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CaegoryInfo GetModel(int ID)
		{
            string sql = "select  top 1 * from Caegory where [ID]=@0";

			CaegoryInfo model=new CaegoryInfo();

            DataSet ds = DBHelp.ExecteDataSet(sql, DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text, ID);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CaegoryInfo DataRowToModel(DataRow row)
		{
			CaegoryInfo model=new CaegoryInfo();
			if (row != null)
			{
								if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				
				if(row["CategoryName"]!=null)
				{
					model.CategoryName=row["CategoryName"].ToString();
				}
				
				if(row["Categry_describe"]!=null)
				{
					model.Categry_describe=row["Categry_describe"].ToString();
				}
				

			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            string sql = "select *  FROM Caegory ";
		
			if(strWhere.Trim()!="")
			{
				sql+=" where "+strWhere;
			}
            return DBHelp.ExecteDataSet(sql, DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text);
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
            string sql = "select {0} * from Caegory {1} {2}";

            string t = "";
            string w = "";
            string o = "";
			if(Top>0)
			{
				t = " top "+Top.ToString();
			}
	
			if(strWhere.Trim()!="")
			{
				w = " where "+strWhere;
			}

            if (filedOrder.Trim() != "")
            {
                o = " order by " + filedOrder;
            }


            return DBHelp.ExecteDataSet(string.Format(sql, t, w, o), DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text);
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
            string sql = "select count(1) FROM Caegory {0}";
            string w = ""; 
            
            if (strWhere.Trim() != "")
            {
                w = " where " + strWhere;
            }
            object obj = DBHelp.ExecuteScalar(string.Format(sql,w), DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text);

			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
            string sql = "SELECT * FROM ( SELECT ROW_NUMBER() OVER ({0})AS Row, T.*  from Caegory T {1}) TT WHERE TT.Row between {2} and {3};SELECT COUNT(1) AS 'count' FROM Caegory T {1}";
            string o = "";
            string w = "";
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                o = "order by T." + orderby;
            }
            else
            {
                o = "order by T.ID desc";
            }

            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                w = " WHERE " + strWhere;
            }
            sql = string.Format(sql, o, w, startIndex, endIndex);


            return DBHelp.ExecteDataSet(sql, DBHelp.Databasetype.SQL2005.ToString(), CommandType.Text);
		}

        /*
         <summary>
         分页获取数据列表
         </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Caegory";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

		#endregion  BasicMethod
		#region  ExtensionMethod

        //public int MyProperty { get; set; }


        //public DataSet GetCategory(int CategoryID)
        //{ 
        
        //}


		#endregion  ExtensionMethod
	}
}

