using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using Blog.Model;
using Blog.DAL;

namespace Blog.Api
{
    public partial class News : System.Web.UI.Page
    {
       
        NewsDao dao = new NewsDao();
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
 
            NewsInfo model = dao.GetModel(int.Parse(id));
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
            NewsInfo model = new NewsInfo();
            model.ID= int.Parse(Request.Form["ID"]);
            model.CategoryID= int.Parse(Request.Form["CategoryID"]);
            model.Title= Request.Form["Title"];
            if (Request.Form["Release_time"].Trim() == "")
                model.Release_time = DateTime.Now;
            else
                model.Release_time= Convert.ToDateTime(Request.Form["Release_time"]);
            model.Release_people= Request.Form["Release_people"];
            model.Click= int.Parse(Request.Form["Click"]);
            model.IsHost= Convert.ToBoolean( Request.Form["IsHost"]);
            model.State= Convert.ToBoolean( Request.Form["State"]);

            //大文本处理
          
            int id = dao.Add(model);
            /////////////////////

            NewsDao.AddGive(id);

            Log.WritePage(id>0 ? "SUCCESS" : "Error");
        }

        private void edit()
        {
            NewsInfo model = new NewsInfo();
            model.ID= int.Parse(Request.Form["ID"]);
            model.CategoryID= int.Parse(Request.Form["CategoryID"]);
            model.Title= Request.Form["Title"];
            if (Request.Form["Release_time"].Trim() == "")
                model.Release_time = DateTime.Now;
            else
                model.Release_time= Convert.ToDateTime(Request.Form["Release_time"]);
            model.Release_people= Request.Form["Release_people"];
            model.Click= int.Parse(Request.Form["Click"]);
            model.IsHost= Convert.ToBoolean( Request.Form["IsHost"]);
            model.State= Convert.ToBoolean( Request.Form["State"]);

       

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


              //查询
            string where ="1=1";

            if (!string.IsNullOrEmpty(ID))  
            where += " and ID like '%"+ID+"%' ";



            DataSet ds = dao.GetListByPage(where, "ID desc", (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
      
            
            

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(ds);
            Log.WritePage(json);
        }
    }
}