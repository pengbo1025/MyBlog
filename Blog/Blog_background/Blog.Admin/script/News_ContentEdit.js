var formdata = new FormData();
var len = 0;
var noimg = "/img/noimg.png";
var TypeImg = document.getElementById("TypeImg");

TypeImg.addEventListener("change", function (evt) {
    len = this.files.length;
    var i = 0, img, reader, file;
    for (; i < len; i++) {
        file = this.files[i];
        if (!!file.type.match(/image.*/)) {
            if (file.size >= 2097152) {
                alert('文件超出大小！请上传2M以内的图片文件');
                return;
            }
            if (window.FileReader) {
                reader = new FileReader();
                reader.onloadend = function (e) {
                    //showUploadedItem(e.target.result, file.fileName);
                };
                reader.readAsDataURL(file);
            }
            if (formdata) {
                isChangeImg = 1;
                formdata.append("images[]", file);
            }
        } else {
            alert('文件格式错误');
            return;
        }
        if (len == 0) {
            return;
        }
    }
});

var act = "add";
var edit_id = 0;
var isChangeImg = 0;

$(document).ready(function () {
    act = getpara("act");
    if (act == "edit") {
        var id = getpara("id");

        $.ajax({
            url: "/Api/News_Content.aspx?power=detail",
            type: "POST",
            data: { id: id },
            success: function (res) {
                var json = $.parseJSON(res);
                $("#ID").val(json.ID);
                $("#NewsID").val(json.NewsID);
                $("#N_Content").val(json.N_Content);
                $("#attributes").val(json.attributes);
                $("#weight").val(json.weight);
                if (json.State) $("#State").attr("checked", "checked");
                if (json.Headings) $("#Headings").attr("checked", "checked");


            }
        });
    }
    else {
        $("#NewsID").val(getpara("id"));


    }

    $(".cbox").change(function () {
        //alert("11");
        Get();
    })

    function Get() {
        //alert($(".cbox")[0].checked);
        //alert($(".cbox").attr("checked"));
        if (($(".cbox")[0].checked) == true) {
            $(".wenzi").text("上传图片");
            $(".controls_1").attr("style", "display:none;");
            $(".controls_2").attr("style", "display:block;");
            //.style.display = "none";

            //alert($(".cbox")[0].checked);
        }
        else {
            $(".wenzi").text("添加文字");
            $(".controls_1").attr("style", "display:block;");
            $(".controls_2").attr("style", "display:none;");
            //alert("false");
            //alert($(".cbox")[0].checked);
        }
    }

});

$("#btn_save").click(function () {
    var val = $(".cbox")[0].checked;

    formdata.append("checked", val);
    //alert($(".cbox")[0].checked);
    var NewsID = $("#NewsID").val();
    if (NewsID == "") {
        parent.layer.msg("新闻ID不能为空", { icon: 2, shade: 0.7 });
        return;
    }
    formdata.append("NewsID", NewsID);
    var N_Content = $("#N_Content").val();
    if (N_Content == "") {
        parent.layer.msg("新闻内容不能为空", { icon: 2, shade: 0.7 });
        return;
    }
    formdata.append("N_Content", N_Content);
    var attributes = $("#attributes").val();
    if (attributes == "") {
        parent.layer.msg("新闻属性不能为空", { icon: 2, shade: 0.7 });
        return;
    }
    formdata.append("attributes", attributes);
    var weight = $("#weight").val();
    if (weight == "") {
        parent.layer.msg("新闻权重不能为空", { icon: 2, shade: 0.7 });
        return;
    }
    formdata.append("weight", weight);
    formdata.append("State", ($("#State").attr("checked") ? "true" : "false"));

    formdata.append("Headings", ($("#Headings").attr("checked") ? "true" : "false"));

    formdata.append("ID", getpara("ID") == "" ? 0 : getpara("ID"));



    parent.layer.msg("请稍后...", { icon: 16, time: 0, shade: 0.7 });

    if (act == "add") {
        $.ajax({
            url: "/Api/News_Content.aspx?power=add",
            type: "POST",
            data: formdata,
            processData: false,
            contentType: false,
            success: function (res) {

                parent.Callback("ADD_SUCCESS");
            }
        });
    } else {
        $.ajax({
            url: "/Api/News_Content.aspx?power=edit",
            type: "POST",
            data: formdata,
            processData: false,
            contentType: false,
            success: function (res) {

                parent.Callback("EDIT_SUCCESS");
            }
        });
    }
});

