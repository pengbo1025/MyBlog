using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Common;
using System.Data;
namespace zuimei.Admin.Api
{
   public partial class RuleManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string power = Request.QueryString["power"];
            switch (power.ToLower())
            {
                case "login":
                    Login();
                    break;
                case "islogin":
                    IsLogin();
                    break;
            }
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        private void IsLogin()
        {
            Log.WritePage(_Common.GetSession("Admin") == null ? "NOT_LOGIN" : "SUCCESS");
        }

        /// <summary>
        /// 后台登录
        /// </summary>
        private void Login()
        {
           
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            if (Utils.SqlFilter2(username) || Utils.SqlFilter2(password))
            {
                Log.WritePage("FAIL");
            }
            else
            {
                password = Utils.MD5(password);
                if (username == "admin" && password == "202CB962AC59075B964B07152D234B70")
                {
                    _Common.SetSession("Admin", username);
                    Log.WritePage("SUCCESS");
                }
                else
                {
                    Log.WritePage("FAIL");
                }
            }
        }

    }
}