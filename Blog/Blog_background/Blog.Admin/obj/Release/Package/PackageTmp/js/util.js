//document.oncontextmenu=new Function("event.returnValue=false;");
//document.onselectstart=new Function("event.returnValue=false;");


//配置网站地址
var wwwUrl = "http://www.cszhenghui.com/";

function imgerr(ref) {
    ref.src = "img/demo.png";
}

String.prototype.Trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); }
String.prototype.trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); }

Request = { QueryString: function (item) { var svalue = location.search.match(new RegExp("[\?\&]" + item + "=([^\&]*)(\&?)", "i")); var Qvalue = svalue ? svalue[1] : svalue; if (Qvalue == null) { return ""; } else { return Qvalue.Trim(); } } }

Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) return i;
    }
    return -1;
};
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};

//获取cookie
function getcookie(name) {

    var cookieArray = document.cookie.split("; "); //得到分割的cookie名值对    
    var cookie = new Object();
    for (var i = 0; i < cookieArray.length; i++) {
        var arr = cookieArray[i].split("=");       //将名和值分开    
        if (arr[0] == name) return unescape(arr[1]); //如果是指定的cookie，则返回它的值    
    }
    return "";
}
function setcookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

//获取当前日期字符串
function nowstr() {
    var d = new Date();
    var nownum = d.getFullYear().toString() + (d.getMonth() + 1 < 10 ? "0" + (d.getMonth() + 1).toString() : d.getMonth() + 1).toString() + (d.getDate() < 10 ? "0" + d.getDate().toString() : d.getDate()).toString() + (d.getHours() < 10 ? "0" + d.getHours().toString() : d.getHours()).toString() + (d.getMinutes() < 10 ? "0" + d.getMinutes().toString() : d.getMinutes()).toString() + (d.getSeconds() < 10 ? "0" + d.getSeconds().toString() : d.getSeconds()).toString() + (d.getMilliseconds() < 10 ? "00" + d.getMilliseconds().toString() : (d.getMilliseconds() < 100 ? "0" + d.getMilliseconds().toString() : d.getMilliseconds())).toString();
    return nownum;
}

//截取浏览器参数
function getpara(para) {
    var hhy_url = document.location.search;
    var hhy_a = hhy_url.split("?");
    var _str = hhy_a[1];
    var _site = document.location.href;
    if (typeof (_str) != 'undefined') {
        var _a = _str.split("&");
        for (var i = 0; i < _a.length; i++) {
            _a[i] = _a[i].split("=");
            if (_a[i][0].toLowerCase() == para.toLowerCase()) {
                _site = _a[i][1];
            }
        }
    }
    if (_site == null || _site == '') {
        _site = "";
    }
    if (_site == document.location.href) {
        return "";
    }
    return _site;
}

//生成GUID
function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
}

function SelectPage(intSearchPageIndex) {
    intPageIndex = intSearchPageIndex;
    LoadData(intPageIndex);
}

function PageList(strFunName) {
    var intPageCount = parseInt((parseInt(intRecordCount) + parseInt(intPageSize) - 1) / parseInt(intPageSize));

    //alert(intPageCount);

    if (intPageCount <= 1) {
        return "";
    }
    var intPageCurrent = 10;
    var intStartPage = (parseInt((intPageIndex + intPageCurrent - 1) / intPageCurrent) - 1) * intPageCurrent + 1;
    var intEndPage = intStartPage + intPageCurrent - 1;
    if (intPageIndex % intPageCurrent == 0) {
        intStartPage++;
        intEndPage++;
    }
    if (intPageIndex % intPageCurrent - 1 == 0 && intPageIndex != 1) {
        intStartPage--;
        intEndPage--;
    }
    if (intEndPage >= intPageCount) {
        intEndPage = intPageCount;
    }

    var intUpPage = intPageIndex - 1;
    var intDownPage = intPageIndex + 1;

    var strUpPage = "<li class=\"prev\"><a href='javascript:;'  onclick=\"" + strFunName + "(" + intUpPage + ")\" >上一页</a></li>";
    var strDownPage = "<li class=\"next\"><a href='javascript:;'  onclick=\"" + strFunName + "(" + intDownPage + ")\" >下一页</a></li>";

    if (intUpPage < 1) {
        intUpPage = 1;
        strUpPage = "<li class=\"prev disabled\"><a href=\"#\">上一页</a></li>";
    }
    if (intDownPage > intPageCount) {
        intDownPage = intPageCount;
        strDownPage = "<li class=\"next disabled\"><a href=\"#\">下一页</a></li>";
    }
    var strPageList = strUpPage;

    for (var i = intStartPage; i <= intEndPage; i++) {
        if (intPageIndex == i) {
            strPageList += "<li class=\"active\"><a href=\"#\">" + i + "</a></li>";
        }
        else {
            strPageList += "<li><a href=\"#\" onclick=\"javascript:" + strFunName + "(" + i + ");\">" + i + "</a></li>";
        }
    }
    strPageList = strPageList + strDownPage;

    //alert(strPageList);
    return strPageList;
}

function random(min, max) {
    return Math.floor(min + Math.random() * (max - min));
}

//金额Input控制  1.00
function Price2FiexdFormat(jsControlStr) {

    $(jsControlStr).attr("maxlength", 10);

    $(jsControlStr).on('keyup', function (event) {
        var $amountInput = $(this);
        event = window.event || event;
        if (event.keyCode == 37 | event.keyCode == 39) {
            return;
        }
        $amountInput.val($amountInput.val().replace(/[^\d.]/g, "").
            //只允许一个小数点              
            replace(/^\./g, "").replace(/\.{2,}/g, ".").
            //只能输入小数点后两位
            replace(".", "$#$").replace(/\./g, "").replace("$#$", ".").replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3'));
    });
    $(jsControlStr).on('blur', function () {
        var $amountInput = $(this);
        $amountInput.val(($amountInput.val().replace(/\.$/g, "")));
    });
}

//数字Input控制  123
function IntFormat(jsControlStr) {

    $(jsControlStr).attr("maxlength", 6);

    $(jsControlStr).on('keyup', function (event) {
        var $amountInput = $(this);
        event = window.event || event;
        if (event.keyCode == 37 | event.keyCode == 39) {
            return;
        }
        $amountInput.val($amountInput.val().replace(/[^\d]/g, ""));
    });
    $(jsControlStr).on('blur', function () {
        var $amountInput = $(this);
        $amountInput.val(($amountInput.val().replace(/[^\d]/g, "")));
    });
}


//数字Input控制  123
function IntFormat(jsControlStr, length) {

    $(jsControlStr).attr("maxlength", length);

    $(jsControlStr).on('keyup', function (event) {
        var $amountInput = $(this);
        event = window.event || event;
        if (event.keyCode == 37 | event.keyCode == 39) {
            return;
        }
        $amountInput.val($amountInput.val().replace(/[^\d]/g, ""));
    });
    $(jsControlStr).on('blur', function () {
        var $amountInput = $(this);
        $amountInput.val(($amountInput.val().replace(/[^\d]/g, "")));
    });
}

/*验证数字*/
function RegNumber(strNumber) {
    var regNumber = /^\d+$/;
    if (regNumber.test(strNumber)) {
        return true;
    }
    else {
        return false;
    }
}

/*验证数值*/
function RegDouble(strDouble) {
    var regDouble = /^[-\+]?\d+(\.\d+)?$/;
    if (regDouble.test(strDouble)) {
        return true;
    }
    else {
        return false;
    }
}

/*加载JS*/
function InvokeJs(strUrl) {
    var datTime = new Date();
    var strTime = datTime.getTime();
    var eleUrl = document.createElement("script");
    eleUrl.setAttribute("type", "text/javascript");
    eleUrl.setAttribute("id", "scr" + strTime);
    eleUrl.setAttribute("src", strUrl + "?Time=" + strTime);
    document.body.appendChild(eleUrl);
}

/*日期格式化*/
function DateFormat(datDateTime) {
    var intYear = datDateTime.getYear();
    var intMonth = datDateTime.getMonth();
    var intDate = datDateTime.getDate() - 1;
    var intHour = datDateTime.getHours();
    var intMinute = datDateTime.getMinutes();
    var strResult = "";
    if (intYear > 0) {
        strResult = intYear + "年前";
    }
    if (intMonth > 0 && strResult == "") {
        strResult = intMonth + "个月前";
    }
    if (intDate > 0 && strResult == "") {
        strResult = intDate + "天前";
    }
    if (intHour > 0 && strResult == "") {
        strResult = intHour + "小时前";
    }
    if (intMinute > 0 && strResult == "") {
        strResult = intMinute + "分钟前";
    }
    if (intMinute == 0 && strResult == "") {
        strResult = "1分钟内";
    }
    return strResult;
}


function LoginOut() {
    layer.confirm('是否退出登录？', {
        btn: ['确认', '取消'],
        shade: [0.5, '#000'],
        icon: 3
    }, function () {
        window.location.href = "Logout.aspx";
    }, function () {

    });
}
function UrlDecode(zipStr) {
    var uzipStr = "";
    for (var i = 0; i < zipStr.length; i++) {
        var chr = zipStr.charAt(i);
        if (chr == "+") {
            uzipStr += " ";
        } else if (chr == "%") {
            var asc = zipStr.substring(i + 1, i + 3);
            if (parseInt("0x" + asc) > 0x7f) {
                uzipStr += decodeURI("%" + asc.toString() + zipStr.substring(i + 3, i + 9).toString());
                i += 8;
            } else {
                uzipStr += AsciiToString(parseInt("0x" + asc));
                i += 2;
            }
        } else {
            uzipStr += chr;
        }
    }
    return uzipStr;
}