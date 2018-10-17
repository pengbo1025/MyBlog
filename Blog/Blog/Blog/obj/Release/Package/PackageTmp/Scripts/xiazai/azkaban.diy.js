function warning(){

if(navigator.userAgent.indexOf("MSIE")>0)  {

art.dialog.alert('复制成功！若要转载请务必保留原文链接，申明来源，谢谢合作！');

} else {alert("复制成功！若要转载请务必保留原文链接，申明来源，谢谢合作！");}}

document.body.oncopy=function(){warning();}