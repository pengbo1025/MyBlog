
//$.ajax({
//    url: "Api/AdminUser/Index.aspx?power=statistics",
//    type: "POST",
//    data: {},
//    success: function (json) {
        
//        var obj = $.parseJSON(json);

//        $("#sp_usercount").html(obj.user_count);
//        $("#sp_recharge_amount").html(obj.recharge_amount);
//        $("#sp_order_count_success").html(obj.order_count_success);
//        $("#sp_bouns_amount").html(obj.bouns_amount);
//        $("#sp_withdraw_count").html(obj.withdraw_count);
//    }
//});

function LoginOut() {
    layer.confirm('是否退出登录？', {
        btn: ['确认', '取消'],
        shade: [0.5, '#000'],
        icon: 3
    }, function () {
        window.location.href = "out.aspx";
    }, function () {

    });
}

