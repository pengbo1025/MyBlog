<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Caegory.aspx.cs" Inherits="Blog.View.Caegory" %>


<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> 
<html lang="cn"> 
<!--<![endif]-->

<head>
   <meta charset="utf-8" />
   <meta content="width=device-width, initial-scale=1.0" name="viewport" />
   <meta content="" name="description" />
   <meta content="" name="author" />
   <link href="/assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
   <link href="/assets/bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
   <link href="/assets/bootstrap/css/bootstrap-fileupload.css" rel="stylesheet" />
    <link href="/js/date97/skin/WdatePicker.css" rel="stylesheet" />
    <link href="/js/kindeditor/themes/simple/simple.css" rel="stylesheet" type="text/css" />
   <link href="/css/style.css" rel="stylesheet" />
</head>
<body style="background:#fff">
   <div class="widget-body">
<div class="row-fluid">
        <div class="control-group">
                <label class="control-label">分类名称</label>
                <div class="controls">
                    <input type="text" class="span6  tooltips" data-trigger="hover" id="CategoryName" style="width:200px;" />
                </div>
            </div>
        <div class="control-group">
                <label class="control-label">分类描述</label>
                <div class="controls">
                    <input type="text" class="span6  tooltips" data-trigger="hover" id="Categry_describe" style="width:200px;" />
                </div>
            </div>


        <div class="form-actions">
            <button type="button" class="btn btn-success" id="btn_save">提交</button>
        </div>
   </div>
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
    <script src="/js/layer/extend/layer.ext.js"></script>
    <script src="/js/util.js" type="text/javascript"></script>
    <%--<script src="script/Manager.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="/assets/bootstrap/js/bootstrap-fileupload.js"></script>
     <script src="/js/kindeditor/kindeditor-min.js" type="text/javascript"></script>
   
     <script src="/script/CaegoryEdit.js?v=<%=new Random().Next() %>"></script>
</html>
