/**  版本信息模板在安装目录下，可自行修改。
* NewsDao.cs
*
* 功 能： N/A
* 类 名： NewsDao
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

namespace Blog.DAL
{
    /// <summary>
    /// 数据访问类:News
    /// </summary>
    public partial class NewsDao
    {


        #region  ExtensionMethod

        public static void AddGive(int id)
        {
            SQLHelper.ExecuteNonQuery(sql封装.SqlHelper.SqlConn, CommandType.Text, "INSERT INTO Give_a_like(NewsID,Number) VALUES (@NewsID,@Number)",
                new SqlParameter("@NewsID", id),
                new SqlParameter("@Number", 5));
        }

        #endregion  ExtensionMethod
    }
}

