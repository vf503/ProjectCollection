
/* 弹出窗口的javascript ------------------------------------------------------------------------start */

// 显示窗口
function ShowDialog(id) {
    var msgDiv = document.getElementById(id);
    if (msgDiv == null) {
        alert("没有找id=" + id + "的到弹出层");
    }
    msgDiv.style.display = 'block';
    //msgDiv.setAttribute("align", "center");
    msgDiv.style.position = "absolute";
    msgDiv.style.left = "50%";
    msgDiv.style.top = "50%";
    msgDiv.style.background = "white";
    var dialog = CreateDIV();
    dialog.appendChild(msgDiv);
    document.body.appendChild(dialog); //主要加上这句，把新建的DIV加到页面上。
}

// 创建背景
function CreateDIV() {
    var sWidth, sHeight;
    sWidth = document.body.offsetWidth;
    sHeight = screen.height;
    var bgDiv = document.createElement("div"); //创建一个DIV
    bgDiv.setAttribute('id', 'bgDiv');
    bgDiv.style.position = "absolute";
    bgDiv.style.top = "0";
    bgDiv.style.background = "#777";
    bgDiv.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
    bgDiv.style.opacity = "0.6";
    bgDiv.style.left = "0";
    bgDiv.style.width = sWidth + "px";
    bgDiv.style.height = sHeight + "px";
    bgDiv.style.zIndex = "10000";
    return bgDiv;
}

// 关闭窗口
function CloseDialog(id) {
    var bgDiv = document.getElementById("bgDiv");
    var msgDiv = document.getElementById(id);
    msgDiv.style.display = "none";
    bgDiv.removeChild(msgDiv);
    document.body.appendChild(msgDiv);
    document.body.removeChild(bgDiv);
}
/* 弹出窗口的javascript ------------------------------------------------------------------------end */