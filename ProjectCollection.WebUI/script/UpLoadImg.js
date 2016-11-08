/*
在需要上传图片的地方添加ID为file的标签
*/

//$(function () {

//    /*
//    $("#file").append("<div><input type='file' name='fileload' id='fileload' onchange='javascript:setImagePreview();' style='width:0px;height:0px' />"
//    + "<input type='button' name='Btn_loadFile' id='Btn_loadFile' value='上传图片'/></div>"
//    + "<div id='localImag'><img name='img' alt='' src='' id='preview' width='-1' height='-1' style='display: none' /></div>"
//    );
//    */
//    $("#Btn_loadFile").click(function () {
//        $("#fileload").click();
//    });
//});
function setImagePreview() {
    var docObj = document.getElementById("fileload");
    var imgObjPreview = document.getElementById("preview");
    //    var docObj = document.getElementById("cphContent_fileload"); 
    if (docObj.files && docObj.files[0]) {
        //图片异常的捕捉，防止用户修改后缀来伪造图片
        var img = new Image();
        img.src = window.URL.createObjectURL(docObj.files[0]);
        img.onerror = function () {
            alert("您上传的图片格式不正确，请重新选择!");
            return false;
        };
        //火狐下，直接设img属性
        imgObjPreview.style.display = 'block';
        imgObjPreview.style.width = '180px';
        imgObjPreview.style.height = '50px';
        //imgObjPreview.src = docObj.files[0].getAsDataURL(); 
        //火狐7以上版本不能用上面的getAsDataURL()方式获取，需要一下方式  
        imgObjPreview.src = window.URL.createObjectURL(docObj.files[0]);

    } else {
        //IE下，使用滤镜
        docObj.select();
        var localImagId = document.getElementById("localImag");
        document.getElementById("Btn_loadFile").focus();
        var imgSrc = document.selection.createRange().text;
        document.getElementById("Btn_loadFile").blur();
        //必须设置初始大小
        localImagId.style.width = "180px";
        localImagId.style.height = "50px";
        //图片异常的捕捉，防止用户修改后缀来伪造图片
        try {
            localImagId.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
            localImagId.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
        } catch (e) {
            alert("您上传的图片格式不正确，请重新选择!");
            return false;
        }
        imgObjPreview.style.display = 'none';
        document.selection.empty();
    }
    return true;
} 