var formdata = new FormData();
var len = 0;
var noimg = "/img/noimg.png";
var TypeImg = document.getElementById("TypeImg");

//TypeImg.addEventListener("change", function (evt) {
//    len = this.files.length;
//    var i = 0, img, reader, file;
//    for (; i < len; i++) {
//        file = this.files[i];
//        if (!!file.type.match(/image.*/)) {
//            if (file.size >= 2097152) {
//                alert('文件超出大小！请上传2M以内的图片文件');
//                return;
//            }
//            if (window.FileReader) {
//                reader = new FileReader();
//                reader.onloadend = function (e) {
//                    //showUploadedItem(e.target.result, file.fileName);
//                };
//                reader.readAsDataURL(file);
//            }
//            if (formdata) {
//                isChangeImg = 1;
//                formdata.append("images[]", file);
//            }
//        } else {
//            alert('文件格式错误');
//            return;
//        }
//        if (len == 0) {
//            return;
//        }
//    }
//});

var act = "add";
var edit_id = 0;
var isChangeImg = 0;

$(document).ready(function () {
    act = getpara("act");
    if (act == "edit") {
        var id = getpara("id");
        
        $.ajax({
            url: "/Api/News.aspx?power=detail",
            type: "POST",
            data: { id: id }, 
            success: function (res) {
                var json = $.parseJSON(res);
               $("#ID").val(json.ID);
               $("#CategoryID").val(json.CategoryID);
               $("#Title").val(json.Title);
               $("#Release_time").val(json.Release_time);
               $("#Release_people").val(json.Release_people);
               $("#Click").val(json.Click);
                  if (json.IsHost)$("#IsHost").attr("checked","checked");
                  if (json.State)$("#State").attr("checked","checked");

             
            }
        });
    }



});

$("#btn_save").click(function () {

   
             var CategoryID = $("#CategoryID").val();
             if (CategoryID == "") { 
                parent.layer.msg("分类ID不能为空", { icon: 2, shade: 0.7 });
                return;
            }
            formdata.append("CategoryID",CategoryID);
             var Title = $("#Title").val();
             if (Title == "") { 
                parent.layer.msg("新闻标题不能为空", { icon: 2, shade: 0.7 });
                return;
            }
            formdata.append("Title",Title);
             var Release_time = $("#Release_time").val();
             if (Release_time == "") { 
                parent.layer.msg("发布时间不能为空", { icon: 2, shade: 0.7 });
                return;
            }
            formdata.append("Release_time",Release_time);
             var Release_people = $("#Release_people").val();
             if (Release_people == "") { 
                parent.layer.msg("发布人不能为空", { icon: 2, shade: 0.7 });
                return;
            }
            formdata.append("Release_people",Release_people);
             var Click = $("#Click").val();
             if (Click == "") { 
                parent.layer.msg("点击量不能为空", { icon: 2, shade: 0.7 });
                return;
            }
            formdata.append("Click",Click);
                 formdata.append("IsHost", ($("#IsHost").attr("checked")?"true":"false"));

                 formdata.append("State", ($("#State").attr("checked")?"true":"false"));

             formdata.append("ID", getpara("ID") == "" ? 0 : getpara("ID"));

   
    
    parent.layer.msg("请稍后...", { icon: 16, time: 0, shade: 0.7 });

    if (act == "add") {
        $.ajax({
            url: "/Api/News.aspx?power=add",
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
            url: "/Api/News.aspx?power=edit",
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

