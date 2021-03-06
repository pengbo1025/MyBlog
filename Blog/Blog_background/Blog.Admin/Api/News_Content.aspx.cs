﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using Blog.Model;
using Blog.DAL;
using Aliyun.OSS;
using zuimei.Common;
using System.Configuration;

namespace Blog.Api
{
    public partial class News_Content : System.Web.UI.Page
    {
        News_ContentDao dao = new News_ContentDao();
        protected void Page_Load(object sender, EventArgs e)
        {
            string power = Request.QueryString["power"];
            switch (power.ToLower())
            {

                case "list":
                    List();//=========？查询条件？===========
                    break;
                case "delete":
                    Delete();
                    break;
                case "add":
                    Add();//=========？图片大文本上传？===========
                    break;
                case "edit":
                    edit();//=========？图片大文本上传？===========
                    break;
                case "detail":
                    Detail();
                    break;
            }

        }


        private void Detail()
        {
            string id = Request["id"];

            News_ContentInfo model = dao.GetModel(int.Parse(id));
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            Log.WritePage(json);
        }

        private void Delete()
        {
            string id = Request.Form["id"];
            bool result = dao.Delete(int.Parse(id));
            Log.WritePage(result ? "SUCCESS" : "Error");
        }

        private void Add()
        {
            if (bool.Parse(Request.Form["checked"]) == false)
            {
                News_ContentInfo model = new News_ContentInfo();
                model.ID = int.Parse(Request.Form["ID"]);
                model.NewsID = int.Parse(Request.Form["id"]);
                model.N_Content = Request.Form["N_Content"];
                model.attributes = Request.Form["attributes"];
                model.weight = int.Parse(Request.Form["weight"]);
                model.State = Convert.ToBoolean(Request.Form["State"]);
                model.Headings = Convert.ToBoolean(Request.Form["Headings"]);


                //大文本处理         
                int id = dao.Add(model);
                Log.WritePage(id > 0 ? "SUCCESS" : "Error");
            }
            else
            {
                News_ContentInfo model = new News_ContentInfo();
                model.ID = int.Parse(Request.Form["ID"]);
                model.NewsID = int.Parse(Request.Form["id"]);
                string url = ConfigurationManager.AppSettings["url"].ToString();
                model.N_Content = url + AliyunOSS.UploadPictrue("blog");
                model.attributes = Request.Form["attributes"];
                model.weight = int.Parse(Request.Form["weight"]);
                model.State = Convert.ToBoolean(Request.Form["State"]);
                model.Headings = Convert.ToBoolean(Request.Form["Headings"]);

                int id = dao.Add(model);
                Log.WritePage(id > 0 ? "SUCCESS" : "Error");
            }

        }

        private void edit()
        {
            News_ContentInfo model = new News_ContentInfo();
            model.ID = int.Parse(Request.Form["ID"]);
            model.NewsID = int.Parse(Request.Form["NewsID"]);
            model.N_Content = Request.Form["N_Content"];
            model.attributes = Request.Form["attributes"];
            model.weight = int.Parse(Request.Form["weight"]);
            model.State = Convert.ToBoolean(Request.Form["State"]);
            model.Headings = Convert.ToBoolean(Request.Form["Headings"]);



            //上传图片处理

            //string imgUrl = ImageHelper.UploadImg("fuligoods");
            //if (imgUrl != "") model.imgUrl = imgUrl;

            bool result = dao.Update(model);
            Log.WritePage(result ? "SUCCESS" : "Error");
        }

        private void List()
        {

            int pageIndex = int.Parse(Request.Form["pageIndex"]);
            int pageSize = int.Parse(Request.Form["pageSize"]);
            string ID = Request.Form["ID"];

            //在前台传过来的值，判断是通过新闻帖子过来的还是直接访问内容的
            string cid = Request.Form["cid"];


            //查询
            string where = "1=1";

            if (!string.IsNullOrEmpty(ID))
                where += " and ID like '%" + ID + "%' ";

            if (!string.IsNullOrEmpty(cid))
                where += " and NewsID = '" + cid + "'";



            DataSet ds = dao.GetListByPage(where, "ID desc", (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);




            string json = Newtonsoft.Json.JsonConvert.SerializeObject(ds);
            Log.WritePage(json);
        }
    }
}