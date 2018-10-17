<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="zuimei.Admin.Login" %>
<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
   <meta charset="utf-8" />
   <title>管理后台-登录</title>
   <meta content="width=device-width, initial-scale=1.0" name="viewport" />
   <meta content="" name="description" />
   <meta content="" name="author" />
   <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
   <link href="assets/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
   
   <link href="css/style.css" rel="stylesheet" />
   <link href="css/style-responsive.css" rel="stylesheet" />
   <link href="css/style-default.css" rel="stylesheet" id="style_color" />
</head>
<body class="lock">
    <div class="lock-header">
        <a class="center" id="logo" href="index.aspx">
            <%--<img class="center" alt="logo" src="img/logo.png">--%>
        </a>
    </div>
    <div class="login-wrap">
        <div class="metro single-size red">
            <div class="locked">
                <i class="icon-lock"></i>
                <span>登录</span>
            </div>
        </div>
        <div class="metro double-size yellow">
            <div class="input-append lock-input">
               
                <input type="text" class="rule_username" placeholder="管理员帐号" maxlength="20" style="IME-MODE: disabled;" value="admin"  />
            </div>
        </div>
        <div class="metro double-size green">
            <div class="input-append lock-input"<%-- style="margin-top:40px;"--%>>
                <input type="password" class="rule_password" placeholder="密码" maxlength="20" style="IME-MODE: disabled;" value="987" />
            </div>
        </div>
        <div class="metro single-size terques login">
            <form action="index.aspx">
                <button type="button" class="btn login-btn">
                    登录
                    <i class=" icon-long-arrow-right"></i>
                </button>
            </form>
        </div>
        <div class="login-footer">
            
        </div>
    </div>
</body>
<script src="js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script src="js/layer/layer.js" type="text/javascript"></script>
<script src="js/util.js" type="text/javascript"></script>
<script src="script/login.js" type="text/javascript"></script>
<script>
    getBrowserInfo();
    function getBrowserInfo() {
        var agent = navigator.userAgent.toLowerCase();

        var regStr_ie = /msie [\d.]+;/gi;
        var regStr_ff = /firefox\/[\d.]+/gi
        var regStr_chrome = /chrome\/[\d.]+/gi;
        var regStr_saf = /safari\/[\d.]+/gi;

        //IE
        if (agent.indexOf("msie") > 0) {
            var verinfo = (agent.match(regStr_ie) + "").replace(/[^0-9.]/ig, "");
            if (parseFloat(verinfo) < 9) {
                $(".login-footer").html("IE浏览器建议使用9.0以上版本进行访问");
            }
            return "";
        }

        //firefox
        if (agent.indexOf("firefox") > 0) {
            return agent.match(regStr_ff);
        }
        //Chrome
        if (agent.indexOf("chrome") > 0) {
            return agent.match(regStr_chrome);
        }

        //Safari
        if (agent.indexOf("safari") > 0 && agent.indexOf("chrome") < 0) {
            return agent.match(regStr_saf);
        }
    }
</script>
</html>