using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace zuimei.View.AdminCallback
{
    /// <summary>
    /// 多文件上传
    /// </summary>
    public class MultiUpload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string power = context.Request.QueryString["power"];
            string key = context.Request.Form["key"];
            string indentityID = context.Request.Form["indentityID"];

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

            string imgUrl = context.Server.MapPath("~/upload/" + power + "/source/") + indentityID + "_" + Guid.NewGuid().ToString() + ".jpg";
            HttpPostedFile upImage = filecoll[0];
            upImage.SaveAs(imgUrl);
            ImageHelper.GetPicThumbnail(imgUrl, imgUrl.Replace("source", "thumb"), 0.6, 0.6, 80);

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