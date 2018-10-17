using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zuimei.View.Api_Manager
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string key = context.Request.Form["key"];
            string fileName = context.Request.QueryString["filename"];
            string power = context.Request.QueryString["power"];

            #region 非空权限判断

            if (key != Utils.MD5("8776DE97A1B8B9078C3EE3319B7C0112"))
            {
                Log.WritePage("TOKEN_ERROR");
                return;
            }
            HttpFileCollection filecoll = context.Request.Files;
            if (filecoll.Count == 0)
            {
                Log.WritePage("NOT_FILE");
                return;
            }
            #endregion

            string imgUrl = context.Server.MapPath("~/upload/" + power + "/source/") + fileName + ".jpg";

            bool flag = false;

            switch (power.ToLower())
            {
                case "goods":
                    flag = true;
                    break;
            }

            HttpPostedFile upImage = filecoll[0];
            //保存source
            upImage.SaveAs(imgUrl);

            if (flag)
            {
                //生成缩略图
                ImageHelper.GetPicThumbnail(imgUrl, imgUrl.Replace("source", "thumb"), 0.5, 0.5, 50);
            }
            Log.WritePage("SUCCESS");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}