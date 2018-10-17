/**  版本信息模板在安装目录下，可自行修改。
* News_ContentDao.cs
*
* 功 能： N/A
* 类 名： News_ContentDao
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
    /// 数据访问类:News_Content
    /// </summary>
    public partial class News_ContentDao
    {


        #region  ExtensionMethod
      
        /// <summary>
        ///  获取帖子列表页面的数据显示
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public List<string[]> GetList1(int id)
        {
            List<string[]> list = new List<string[]>();
            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT * FROM News where News.CategoryID=@CategoryID order by ID desc",
                new SqlParameter("@CategoryID", id));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                    item["ID"].ToString(),
                    item["Title"].ToString(),
                    item["Release_time"].ToString(),
                    item["Release_people"].ToString(),
                    item["Click"].ToString()
                });
            }

            //int NewsID = int.Parse(list[0][0].ToString());

            return list;
        }

        /// <summary>
        /// 根绝新闻ID获取标题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTitle(int NewsID)
        {
            return SQLHelper.ExecuteScalar(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT Title FROM News where News.ID=@NewsID",
                            new SqlParameter("@NewsID", NewsID)).ToString();
        }

        /// <summary>
        /// 上面那个GetList1是用来通过分类ID跳转到新闻列表页面进行显示，这个方法是在首页做帖子列表显示
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetIndexList(int qNum)
        {
            List<string[]> list = new List<string[]>();
            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT * FROM (SELECT  ROW_NUMBER() OVER (order by T.ID desc)AS Row, * FROM News T where T.IsHost='True') TT WHERE TT.Row between @qNum and @sNum;",
                new SqlParameter("@qNum", qNum),
                new SqlParameter("@sNum", qNum + 4));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                    item["ID"].ToString(),
                    item["Title"].ToString(),
                    item["Release_time"].ToString(),
                    item["Release_people"].ToString(),
                    item["Click"].ToString()
                });
            }

            //int NewsID = int.Parse(list[0][0].ToString());

            return list;
        }

        /// <summary>
        /// 获取列表框里的文字
        /// </summary>
        /// <returns></returns>
        public static string GetDetailsText(int NewsID)
        {
            return SQLHelper.ExecuteScalar(sql封装.SqlHelper.SqlConn, CommandType.Text,
                  "SELECT top 1 News_Content.N_Content FROM News_Content where News_Content.NewsID=@NewsID and News_Content.attributes='文本'order by News_Content.weight desc", new SqlParameter("@NewsID", NewsID)).ToString();
        }

        /// <summary>
        /// 获取列表框里的图片
        /// </summary>
        /// <returns></returns>
        public static string GetDetailsPic(int NewsID)
        {
            object o = SQLHelper.ExecuteScalar(sql封装.SqlHelper.SqlConn, CommandType.Text,
                  "SELECT top 1 News_Content.N_Content FROM News_Content where News_Content.NewsID=@NewsID and News_Content.attributes='图片'order by News_Content.weight desc", new SqlParameter("@NewsID", NewsID));
            if (o != null)
            {
                return o.ToString();
            }
            else
            {
                return "http://qqdb.oss-cn-beijing.aliyuncs.com/upload/blog/source/8bfcd65c-ce78-428c-9377-2a8bceb2c0d6.jpg";
            }
        }

        /// <summary>
        /// 在分类列表页面绑定上面的分类动画
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<string[]> GetCategory(int ID)
        {
            List<string[]> list = new List<string[]>();
            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT Caegory.* FROM Caegory where Caegory.ID=@ID", new SqlParameter("@ID", ID));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                  item["CategoryName"].ToString(),
                  item["Categry_describe"].ToString()
                });
            }

            return list;
        }


        /// <summary>
        /// 根绝新闻ID或许新闻内容的详细
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public List<string[]> GetNewsDetails(int NewsID)
        {
            List<string[]> list = new List<string[]>();

            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT News_Content.* FROM News_Content where NewsID=@NewsID",
                new SqlParameter("@NewsID", NewsID));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                  item["ID"].ToString(),
                  item["NewsID"].ToString(),
                  item["N_Content"].ToString(),
                  item["attributes"].ToString(),
                  item["weight"].ToString(),
                  item["State"].ToString(),
                  item["Headings"].ToString(),
                });
            }

            return list;
        }

        /// <summary>
        /// 用于绑定详细页面头部的时间跟浏览量
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<string[]> GetNews(int ID)
        {
            List<string[]> list = new List<string[]>();

            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT News.* FROM News where ID=@ID",
                new SqlParameter("@ID", ID));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                  item["Release_time"].ToString(),
                  item["Click"].ToString(),
                });
            }

            return list;
        }

        /// <summary>
        /// 绑定留言
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetReview()
        {
            List<string[]> list = new List<string[]>();

            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT top 8 Reviews.* FROM Reviews order by Reviews.ID desc");

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                    item["ID"].ToString(),
                    item["NewsID"].ToString(),
                    item["ReviewContent"].ToString(),
                    item["ReviewName"].ToString(),
                    item["Email"].ToString(),
                    item["replyID"].ToString()
                });
            }

            return list;
        }


        /// <summary>
        /// 绑定此新闻下的留言
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetReview1(int NewsID)
        {
            List<string[]> list = new List<string[]>();

            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT top 8 Reviews.* FROM Reviews where NewsID=@NewsID",
                new SqlParameter("@NewsID", NewsID));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                    item["ID"].ToString(),
                    item["NewsID"].ToString(),
                    item["ReviewContent"].ToString(),
                    item["ReviewName"].ToString(),
                    item["Email"].ToString(),
                    item["replyID"].ToString()
                });
            }

            return list;
        }


        /// <summary>
        /// 点赞表的逻辑
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="Number"></param>
        public void Adddianzan(int NewsID, int Number)
        {
            List<string[]> list = new List<string[]>();
            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT * FROM Give_a_like  where NewsID=@NewsID", new SqlParameter("@NewsID", NewsID));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                  item["NewsID"].ToString(),
                  item["Number"].ToString()
                });
            }
            if (NewsID == int.Parse(list[0][0].ToString()))
            {
                Number = int.Parse(list[0][1].ToString()) + Number;

                SQLHelper.ExecuteNonQuery(sql封装.SqlHelper.SqlConn, CommandType.Text, "UPDATE    Give_a_like SET Number =@Number where NewsID=@NewsID",
                    new SqlParameter("@Number", Number),
                    new SqlParameter("@NewsID", NewsID));

            }
            else
            {
                SQLHelper.ExecuteNonQuery(sql封装.SqlHelper.SqlConn, CommandType.Text, "INSERT INTO Give_a_like (NewsID, Number) VALUES (@NewsID,@Number)",
                    new SqlParameter("@NewsID", NewsID),
                    new SqlParameter("@Number", Number));

            }
        }

        /// <summary>
        /// 绑定点赞数量
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public static string Getdianzai(int NewsID)
        {
            List<string[]> list = new List<string[]>();
            DataSet ds = SQLHelper.ExecuteDataset(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT NewsID FROM Give_a_like");

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new string[]{
                  item["NewsID"].ToString()
                });
            }


            //这个未知的List如何取NewsID来进行判断   还有刷新
            foreach (var item in list)
            {
                if (int.Parse(item[0].ToString()) == NewsID)
                {
                    return SQLHelper.ExecuteScalar(sql封装.SqlHelper.SqlConn, CommandType.Text,
                              "SELECT Give_a_like.Number FROM Give_a_like where NewsID=@NewsID", new SqlParameter("@NewsID", NewsID)).ToString();
                }

            }
            return "0";
        }

        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="ReviewContent"></param>
        /// <param name="ReviewName"></param>
        /// <param name="Email"></param>
        /// <param name="replyID"></param>
        public void AddReview(int NewsID, string ReviewContent, string ReviewName, string Email, int replyID)
        {
            SQLHelper.ExecuteNonQuery(sql封装.SqlHelper.SqlConn, CommandType.Text, "INSERT INTO Reviews(NewsID, ReviewContent, ReviewName, Email,replyID) VALUES (@NewsID,@ReviewContent,@ReviewName,@Email,@replyID)",
                new SqlParameter("@NewsID", NewsID),
                new SqlParameter("@ReviewContent", ReviewContent),
                new SqlParameter("@ReviewName", ReviewName),
                new SqlParameter("@Email", Email),
                new SqlParameter("@replyID", replyID));
        }

        public static string GetReviewCount(int NewsID)
        {
            return SQLHelper.ExecuteScalar(sql封装.SqlHelper.SqlConn, CommandType.Text, "SELECT COUNT(*) FROM Reviews where NewsID=@NewsID",
                 new SqlParameter("@NewsID", NewsID)).ToString();
        }

        #endregion  ExtensionMethod
    }
}

