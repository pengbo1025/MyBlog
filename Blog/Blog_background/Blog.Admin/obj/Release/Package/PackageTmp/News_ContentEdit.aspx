<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News_Content.aspx.cs" Inherits="Blog.View.News_Content" %>


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
<body style="background: #fff">
    <div class="widget-body">
        <div class="row-fluid">
            <div class="control-group">
                <label class="control-label">新闻ID</label>
                <div class="controls">
                    <input type="text" class="span6  tooltips" data-trigger="hover" id="NewsID" style="width: 200px;" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">新闻内容</label>
                <input type="checkbox" class="cbox"/>
                <label class="wenzi">添加文字</label>
                

                <div class="controls_1">
                   <textarea class="span6  tooltips" id="N_Content">
                       
                   </textarea>
                </div>
               
                <div class="controls_2" style="display: none;">
                    <label class="control-label">图片路径</label>
                    <div class="controls">
                        <div data-provides="fileupload" class="fileupload fileupload-new">
                            <div style="width: 100px; height: 100px;" class="fileupload-new thumbnail">
                                <img alt="" src="img/noimg.png" style="height: 100%;" id="imgUrl" />
                            </div>
                            <div style="max-width: 100px; max-height: 100px; line-height: 20px;" class="fileupload-preview fileupload-exists thumbnail"></div>
                            <div>
                                <span class="btn btn-file"><span class="fileupload-new">选择图片</span>
                                    <span class="fileupload-exists">更换</span>
                                    <input type="file" class="default" accept=".jpg,.jpeg,.png,.bmp" id="TypeImg" /></span>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="control-group">
                <label class="control-label">新闻属性</label>
                <div class="controls">
                    <select class="span6  tooltips" data-trigger="hover" id="attributes" style="width: 200px;">
                        <option value="文本">文本</option>
                        <option value="图片">图片</option>
                    </select>
                    <%--<input type="text" " />--%>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">新闻权重</label>
                <div class="controls">
                    <input type="text" class="span6  tooltips" data-trigger="hover" id="weight" style="width: 200px;" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">是否禁用</label>
                <div class="controls">
                    <input type="checkbox" id="State" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">小标题</label>
                <div class="controls">
                    <input type="checkbox" id="Headings" />
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

<script src="/script/News_ContentEdit.js?v=<%=new Random().Next() %>"></script>
</html>
