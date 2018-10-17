﻿var intPageIndex = 1;
var intRecordCount = 0;
var intPageSize = 10;
var noimg = "/img/noimg.png";

//var brandJSON = "";
//var classJSON = "";


$(document).ready(function () {

    LoadData(1);
});

function LoadData(index) {
    $("html,body").animate({ scrollTop: 0 });
    intPageIndex = index;
    $("#ul_list").html("<div style=\"width:30px;margin:0 auto;margin-top:20px;\"><img src=\"img/loading.gif\" /></div>");
    setTimeout(function () {
        getdata();
    }, 500);
}

$("#btn_search").click(function () {
    LoadData(1);
});

function getdata() {
    //var id = getpara("id");

    $.ajax({
        type: "POST",
        url: "/Api/News_Content.aspx?power=list",
        data: {
            pageIndex: intPageIndex,
            pageSize: intPageSize,
            ID: $("#ID").val(),
            //在这里传个cid判断是通过新闻帖子过来的还是直接访问内容的
            cid: getpara("id"),
        }, success: function (val) {

            var json = $.parseJSON(val);
            var data = json.Table;
            intRecordCount = json.Table1[0].count;

            var _html = "";
            var _height = 0;

            _html +=
                    "<thead>" +
                        "<tr>" +
              "<th>新闻内容ID</th>" +
              "<th>新闻ID</th>" +
              "<th>新闻内容</th>" +
              "<th>新闻属性</th>" +
              "<th>新闻权重</th>" +
              "<th>是否禁用</th>" +
              "<th>小标题</th>" +
             "<th width=\"70px\">操作</th>" +
                        "</tr>" +
                    "</thead>" +
                    "<tbody>";
            for (var i = 0; i < data.length; i++) {
                _height += 58;
                var imgUrl = "";

                var td = "";
                if (data[i].attributes == "文本") {
                    if (data[i].N_Content.length > 10) {
                        //td = "鼠标移入查看详细";
                        //$("#chakan").on("mouseover", "td", function () {
                        //    //data = table.row($(this).parents("tr")).data();
                        //    //layer.msg(data[6]);
                        //    alert("11");
                        //});

                        td = data[i].N_Content;


                    }

                }
                else {
                    td = "<img src='" + data[i].N_Content + "' style='height:120px;width:120px;' />"
                }


                //if (data[i].imgUrl != "")
                //    imgUrl = data[i].imgUrl.toString().replace("source", "thumb");
                //else
                //    imgUrl = noimg;

                //(imgUrl.indexOf(wwwUrl) > -1 ? imgUrl : wwwUrl + imgUrl)
                _html += " < tr style='text-align: center;' > " +
                    "<td>" + data[i].ID + "</td>" +
                    "<td>" + data[i].NewsID + "</td>" +
                    "<td class='chakan' style='width:200px;'>" + td + "</td>" +
                "<td>" + data[i].attributes + "</td>" +
                "<td>" + data[i].weight + "</td>" +
                "<td><input type=\"checkbox\" disabled=\"disabled\" " + (data[i].State ? "checked=\"checked\"" : "") + " /></td>" + "<td><input type=\"checkbox\" disabled=\"disabled\" " + (data[i].Headings ? "checked=\"checked\"" : "") + " /></td>" + "<td>" +
                      "<a class=\"btn btn-primary\" onclick=\"Update(" + data[i].ID + ");\">编辑</a> " +
                      "<br/>" + "<br/>" + "<a class=\"btn btn-danger\" href=\"javascript:;\" onclick=\"Delete(" + data[i].ID + ");\">删除</a>" +
               "</td>" +
      "</tr>";

            }
            if (data.length == 0) {
                _html += "<tr><td colspan=20><div style=\"width:200px;margin:0 auto;margin-top:20px;\">暂无数据</div></td></tr>";
            }
            _html += "</tbody>";
            $("#ul_list").css("display", "none");
            $("#ul_list").animate({ opacity: 'show', height: _height + "px" }, 'slow').html(_html);
            var pageHtml = PageList("SelectPage");
            $("#pageHtml").html(pageHtml);
        }
    });
}


//删除商品
function Delete(id) {
    layer.confirm('确认删除？', {
        title: '删除提示',
        btn: ['确认', '取消']
    }, function () {
        $.ajax({
            url: "/Api/News_Content.aspx?power=delete",
            type: "POST",
            data: { id: id },
            success: function (res) {
                if (res == "SUCCESS") {
                    layer.msg('删除成功', { shade: [0.5, '#000'], icon: 1 });
                    LoadData(intPageIndex);
                }
            }
        });
    });
}


function Update(id) {
    layer.open({
        type: 2,
        title: '编辑',
        shadeClose: true,
        scrollbar: false,
        area: ['900px', '660px'],
        content: 'News_ContentEdit.aspx?act=edit&id=' + id
    });
}

function Add() {
    layer.open({
        type: 2,
        title: '添加',
        shadeClose: true,
        scrollbar: false,
        area: ['900px', '660px'],
        content: 'News_ContentEdit.aspx?act=add&id=' + getpara("id")
    });
}

function Callback(msg) {
    layer.closeAll();
    if (msg == "ADD_SUCCESS") {
        layer.msg('添加成功', { shade: [0.6, '#000'], icon: 1 });
        LoadData(intPageIndex);
    } else if (msg == "EDIT_SUCCESS") {
        layer.msg('编辑成功', { shade: [0.6, '#000'], icon: 1 });
        LoadData(intPageIndex);
    } else {
        layer.msg('记录已存在', { shade: [0.6, '#000'], icon: 2 });
    }
}
