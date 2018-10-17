using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.DAL;
using Blog.Model;
using System.Configuration;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        Blog.DAL.News_ContentDao c = new News_ContentDao();
        public ActionResult Index()
        {
            //绑定首页的新闻列表
            // ViewBag.list = c.GetIndexList();

            //ViewBag.c = ConfigurationManager.AppSettings["url"].ToString();

            ViewBag.Review = c.GetReview();
            int current = 1;
            //ViewBag.dianzan = c.Getdianzai(NewsID);
            ViewBag.GetList = c.GetIndexList(current);
            return View();
        }

        public ActionResult PartialIndex(int current = 1)
        {

            ViewBag.GetList = c.GetIndexList(current);

            ViewBag.Review = c.GetReview();

            return PartialView();
        }


        /// <summary>
        /// 个人介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult Introduce()
        {
            ViewBag.Review = c.GetReview();

            return View();
        }

    }
}
