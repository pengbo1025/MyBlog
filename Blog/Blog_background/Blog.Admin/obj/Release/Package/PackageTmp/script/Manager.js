
$(document).ready(function () {
    //5分钟进行轮询 Session登录状态
    (function () {
        setTimeout(function () {
            IsLogin();
        }, 300000);
    })();

    //是否登录
    function IsLogin() {
        $.ajax({
            type: "POST",
            url: "Api/RuleManager.aspx?power=islogin",
            data: {},
            success: function (val) {

                if (val == "NOT_LOGIN") {
                    layer.msg("登录超时或失败,请重新登录!", { shade: [1.0, '#000'], icon: 2, time: 1000 });

                    $("body").css("display", "inline");
                    setTimeout(function () {
                        window.location.href = "Login.aspx";
                    }, 1000);
                }
                $(document).css("display", "block");
            }
        });
    }

    //获取权限菜单
    (function GetRuleMenu() {
      
        $.ajax({
            url: "../menu.html",
            type: "GET",
            success: function (data)
            {

                var currentHref = window.location.href.substring(location.href.lastIndexOf('/') + 1);
              
                $(".sidebar-menu").html(data);
                $(".sidebar-menu").append(
                    "<li class=\"sub-menu\">" +
                        "<a href=\"javascript:;\" onclick=\"LoginOut();\" class=\"\">" +
                            "<span>退出登录</span>" +
                        "</a>" +
                    "</li>"
                );

                $("ul .sub a").parent().removeAttr("class", "active");
               
                var $list = $("ul .sub a[href='" + currentHref + "']");
               
                if ($list.length > 0) {
                    $list.parent().attr("class", "sub-menu active");
                    $list.parent().parent().parent().attr("class", "sub-menu active"); 
                }
                else {
                    $("#parli_000").attr("class", "sub-menu active");
                }
                ShildStyleJS();
            }
        })
            


     

        //if (act_parentid != 0) {
        //    $("#parli_" + act_parentid).attr("class", "sub-menu active");
        //} else {
        //    $("#parli_000").attr("class", "sub-menu active");
        //}
        //if (act_id != 0) {
        //    $("#li_" + act_id).attr("class", "active");
        //}
       

    })();

    //菜单样式
    function ShildStyleJS() {

        $('#sidebar .sub-menu > a').click(function () {
          
            var last = $('.sub-menu.open', $('#sidebar'));
            last.removeClass("open");
            $('.arrow', last).removeClass("open");
            $('.sub', last).slideUp(200);
            var sub = $(this).next();
            if (sub.is(":visible")) {
                $('.arrow', jQuery(this)).removeClass("open");
                $(this).parent().removeClass("open");
                sub.slideUp(200);
            } else {
                $('.arrow', jQuery(this)).addClass("open");
                $(this).parent().addClass("open");
                sub.slideDown(200);
            }
            var o = ($(this).offset());
            diff = 200 - o.top;
            if (diff > 0)
                $(".sidebar-scroll").scrollTo("-=" + Math.abs(diff), 500);
            else
                $(".sidebar-scroll").scrollTo("+=" + Math.abs(diff), 500);
        });
    }

    //弹出框配置
    (function () {
        layer.config({
            extend: '/skin/moon/style.css',
            skin: 'layer-ext-moon',
            shade: 0.8,
            shift: 5,
        });
    })();


});