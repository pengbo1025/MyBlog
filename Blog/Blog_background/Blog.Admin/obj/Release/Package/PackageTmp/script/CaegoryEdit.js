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
            url: "/Api/Caegory.aspx?power=detail",
            type: "POST",
            data: { id: id }, 
            success: function (res) {
                var json = $.parseJSON(res);
               $("#ID").val(json.ID);
               $("#CategoryName").val(json.CategoryName);
               $("#Categry_describe").val(json.Categry_describe);

             
            }
        });
    }



});

$("#btn_save").click(function () {

   
             var CategoryName = $("#CategoryName").val();
             if (CategoryName == "") { 
                parent.layer.msg("分类名称不能为空", { icon: 2, shade: 0.7 });
                return;
            }
            formdata.append("CategoryName",CategoryName);
             var Categry_describe = $("#Categry_describe").val();
             if (Categry_describe == "") { 
                parent.layer.msg("分类描述不能为空", { icon: 2, shade: 0.7 });
                return;
            }
            formdata.append("Categry_describe",Categry_describe);
             formdata.append("ID", getpara("ID") == "" ? 0 : getpara("ID"));

   
    
    parent.layer.msg("请稍后...", { icon: 16, time: 0, shade: 0.7 });

    if (act == "add") {
        $.ajax({
            url: "/Api/Caegory.aspx?power=add",
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
            url: "/Api/Caegory.aspx?power=edit",
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

