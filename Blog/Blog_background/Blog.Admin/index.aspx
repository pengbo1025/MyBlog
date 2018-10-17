<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="zuimei.Admin.index" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<head>
    <meta charset="utf-8" />
    <title>后台管理系统</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="Mosaddek" name="author" />
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="assets/bootstrap/css/bootstrap-fileupload.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/style-responsive.css" rel="stylesheet" />
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/style-default.css" rel="stylesheet" id="style_color" />
</head>
<body class="fixed-top">
    <div id="header" class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container-fluid">
                <a class="brand" href="index.aspx">
                    <%--<img src="img/logo.png" alt="Metro Lab" />--%>
                </a>
                <a class="btn btn-navbar collapsed" id="main_menu_trigger" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="arrow"></span>
                </a>
                <div class="top-nav"></div>
            </div>
        </div>
    </div>
    <div id="container" class="row-fluid">
        <div class="sidebar-scroll">
            <div id="sidebar" class="nav-collapse collapse">

                <ul class="sidebar-menu">
                   
         
                </ul>
            </div>
        </div>
        <div id="main-content">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span12">
                        <h3 class="page-title">控制台
                        </h3>
                        <ul class="breadcrumb">
                            <li>
                                <a href="#">首页</a>
                                <span class="divider">/</span>
                            </li>
                            <li class="active">控制台
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="metro-nav">
                        <div class="metro-nav-block nav-block-orange">
                            <a data-original-title="" href="User.aspx">
                                <div class="info"><span id="sp_usercount">-</span></div>
                                <div class="status">用户总数</div>
                            </a>
                        </div>
                        <div class="metro-nav-block nav-block-yellow">
                            <a data-original-title="" href="Fin_Recharge.aspx">
                                <div class="info">￥<span id="sp_recharge_amount">-</span></div>
                                <div class="status">充值总金额</div>
                            </a>
                        </div>
                        <div class="metro-nav-block nav-block-green">
                            <a data-original-title="" href="Order.aspx">
                                <div class="info"><span id="sp_order_count_success">-</span></div>
                                <div class="status">总成交订单数</div>
                            </a>
                        </div>
                        <div class="metro-nav-block nav-light-green">
                            <a data-original-title="" href="Fin_Withdraw.aspx">
                                <div class="info"><span id="sp_withdraw_count">-</span></div>
                                <div class="status">总提现数</div>
                            </a>
                        </div>
                    </div>
                    <div class="space10"></div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer">
    </div>
    <!-- ie8 fixes -->
    <!--[if lt IE 9]>
           <script src="js/excanvas.js"></script>
          <script src="js/respond.js"></script>
          <![endif]-->
</body>
<script src="js/jquery-1.8.2.min.js"></script>
<script src="js/jquery.nicescroll.js" type="text/javascript"></script>
<script type="text/javascript" src="assets/jquery-slimscroll/jquery-ui-1.9.2.custom.min.js"></script>
<script type="text/javascript" src="assets/jquery-slimscroll/jquery.slimscroll.min.js"></script>
<script src="assets/bootstrap/js/bootstrap.min.js"></script>

<script src="assets/jquery-easy-pie-chart/jquery.easy-pie-chart.js" type="text/javascript"></script>
<script src="js/jquery.sparkline.js" type="text/javascript"></script>
<script src="assets/chart-master/Chart.js"></script>
<script src="js/jquery.scrollTo.min.js"></script>
<script src="js/common-scripts.js"></script>
<script src="js/layer/layer.js" type="text/javascript"></script>
<script src="js/util.js" type="text/javascript"></script>
<script src="script/Manager.js" type="text/javascript"></script>
<script src="script/index.js" type="text/javascript"></script>
<script>
    $("#div_exit").click(function () {

        layer.confirm('确认退出管理后台？', {
            btn: ['确认', '取消'],
            shade: [0.6, '#000']
        }, function () {
            window.location.href = "out.aspx";
        });
    });
</script>
</html>
