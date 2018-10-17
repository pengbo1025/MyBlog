var allowLogin = true;
var ErrMsg = "";

//判断浏览器是否支持后台
(function () {
    $("body").css("display", "none");
    var TestFormData = null;
    try {
        TestFormData = new FormData();
        TestFormData.append("suppert_browers", "yes");
        $("body").css("display", "");
    } catch (e) {
        ErrMsg = "当前浏览器版本过低，请选择更高级别的浏览器进行访问！<br/>";
        if (e.toString() == "ReferenceError: “FormData”未定义") {
            //ErrMsg += "错误:ReferenceError";
        }
        layer.alert(ErrMsg, { title: '错误提示', shade: [0.6, '#000'], icon: 2, time: 0 });
        allowLogin = false;
    }
})();


$(".rule_username,.rule_password,.rule_type")
    .focus(function () {
        document.onkeydown = function (event) {
            var e = event || window.event || arguments.callee.caller.arguments[0];
            if (e && e.keyCode == 13) {
                Login();
            }
        }
    })
    .blur(function () {
        document.onkeydown = function (event) { }
    });

$(".login-btn").click(function () {
    Login();
});

function Login() {
    if (allowLogin == false) {
        layer.alert(ErrMsg, { title: '错误提示', shade: [0.6, '#000'], icon: 2, time: 0 });
        return;
    }

    var username = $(".rule_username").val().trim();
    var password = $(".rule_password").val().trim();


    var errmsg = "";
    if (username == "") {
        errmsg = "管理员账户不能为空";
    } else if (password == "") {
        errmsg = "管理员密码不能为空";
    }
    if (errmsg != "") {
        layer.msg(errmsg, { shade: [0.6, '#000'], icon: 2 });
        return;
    }

    $.ajax({
        type: "POST",
        url: "Api/RuleManager.aspx?power=login",
        data: {
            username: username,
            password: password
        }, success: function (val) {
            if (val == "SUCCESS") {
                layer.msg("登录成功", { shade: [0.6, '#000'], icon: 1, time: 1000 });
                setTimeout(function () {
                    window.parent.location.href = "main.htm?act=manager";
                }, 1000);
            } else {
                layer.msg("用户名密码错误，登录失败", { shade: [0.6, '#000'], icon: 2 });
            }
        }, error: function (e) {

        }
    });
}

