using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.DAL;
using Blog.Model;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/
        Blog.DAL.News_ContentDao c = new News_ContentDao();
       
        

        public ActionResult PostIndex()
        {
            int cid = int.Parse(Request.QueryString["cid"].ToString());

            ViewBag.list = c.GetList1(cid);

            ViewBag.Review = c.GetReview();

            return View();
        }

        public ActionResult PostDetails()
        {
            int NewsID = int.Parse(Request.QueryString["NewsID"].ToString());

            ViewBag.Details = c.GetNewsDetails(NewsID);

            ViewBag.NewsList = c.GetNews(NewsID);

            ViewBag.Review = c.GetReview();

            ViewBag.dianzan = News_ContentDao.Getdianzai(NewsID);

            ViewBag.Review1 = c.GetReview1(NewsID);
            return View();
        }

        public ActionResult Adddianzan(int newsid, int number = 1)
        {
            c.Adddianzan(newsid, number);
            return View();
        }

        public ActionResult AddReview(int NewsID, string ReviewContent, string ReviewName, string Email, int replyID = 1)
        {
            try
            {
                c.AddReview(NewsID, ReviewContent, ReviewName, Email,replyID);

                return Content("ok");
            }
            catch (Exception)
            {
                return Content("no");
                throw;
            }



        }
    }
}
