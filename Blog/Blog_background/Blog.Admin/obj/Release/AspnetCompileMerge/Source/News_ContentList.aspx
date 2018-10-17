<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_ContentList.aspx.cs" Inherits="Blog.View.News_Content" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> 
<html lang="en"> 
<!--<![endif]-->

<head>
   <meta charset="utf-8" />
   <meta content="width=device-width, initial-scale=1.0" name="viewport" />
   <meta content="" name="description" />
   <meta content="" name="author" />
   <link href="/assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
   <link href="/assets/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
   <link href="/assets/bootstrap/css/bootstrap-fileupload.css" rel="stylesheet" />
   
   <link href="/css/style.css" rel="stylesheet" />
   <link href="/css/style-responsive.css" rel="stylesheet" />
   <link href="/css/style-default.css" rel="stylesheet" id="style_color" />
   <link href="/js/date97/skin/WdatePicker.css" rel="stylesheet" />
   <title>News_Content管理</title>
</head>
<body class="fixed-top">
   <div id="header" class="navbar navbar-inverse navbar-fixed-top">
       <div class="navbar-inner">
           <div class="container-fluid">
               <a class="brand" href="index.aspx">
                   <img src="img/logo.png" alt="Metro Lab" />
               </a>
               <a class="btn btn-navbar collapsed" id="main_menu_trigger" data-toggle="collapse" data-target=".nav-collapse">
                   <span class="icon-bar"></span>
                   <span class="icon-bar"></span>
                   <span class="icon-bar"></span>
                   <span class="arrow"></span>
               </a>
               <div class="top-nav "></div>
           </div>
       </div>
   </div>
   <div id="container" class="row-fluid">
      <div class="sidebar-scroll">
          <div id="sidebar" class="nav-collapse collapse">
              <div class="navbar-inverse"></div>
              <ul class="sidebar-menu">
                  
              </ul>
          </div>
      </div>
      <div id="main-content">
         <div class="container-fluid">
            <div class="row-fluid">
               <div class="span12">
                   <ul class="breadcrumb">
                       <li>
                           <a href="#">后台</a>
                           <span class="divider">/</span>
                       </li>
                       <li>
                           <a href="#">管理</a>
                           <span class="divider">/</span>
                       </li>
                       <li class="active">
                          News_Content列表
                       </li>
                       <li class="pull-right search-wrap"></li>
                   </ul>
               </div>
            </div>
            <div id="page-wraper">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget red">
                            <div class="widget-title">
                                <h4><i class="icon-reorder"></i>查询条件</h4>
                            </div>
                            <div class="widget-body">
                              
                                <div class="row-fluid">
</div><div class="row-fluid">
                                    <div class="span3">
                                             <div class="control-group">
                                                <div class="controls controls-row">新闻内容ID<br />
                                                    <input type="text" class="input-block-level" placeholder="" maxlength="15" id="ID" autocomplete="off" />
                                                </div>
                                            </div>
                                        </div>
</div>


                                <button class="btn  btn-success" type="button" id="btn_search">查    询</button>
                                <button class="btn  btn-primary" onclick="Add();" >添加News_Content</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget orange">
                            <div class="widget-title">
                                <h4>列表</h4>
                            </div>
                            <div class="widget-body">
                                <table class="table table-striped table-bordered table-advance table-hover" id="ul_list"></table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span6">
                        <div class="dataTables_paginate paging_bootstrap pagination">
                            <ul id="pageHtml"></ul>
                        </div>
                    </div>
                </div>
            </div>      
         </div>
      </div>
   </div>
   <div id="footer">
       
   </div>
   <script src="/js/jquery-1.8.3.min.js"></script>
   <script src="/js/jquery.nicescroll.js" type="text/javascript"></script>
   <script src="/assets/bootstrap/js/bootstrap.min.js"></script>
   <script src="/js/jquery.scrollTo.min.js"></script>
   <script src="/js/common-scripts.js"></script>

</body>
    <script src="/js/date97/lang/zh-cn.js"></script>
    <script src="/js/date97/WdatePicker.js"></script>

    <script src="/js/layer/layer.js" type="text/javascript"></script>
<script src="js/util.js"></script>
    <script src="script/Manager.js" type="text/javascript"></script>
    <script src="/script/News_Content.js?v=<%=new Random().Next() %>"></script>
</html>


