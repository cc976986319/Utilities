'use strict'
var delay = null;
/*
延迟操作
@@ method:要延时执行的方法
@@ delayTime:延时时间，毫秒为单位
*/
function DelayOperate(method, delayTime) {
    if ('number' == typeof (delayTime)) {
        if (delay != null)
            clearTimeout(delay);
        delay = setTimeout(method, delayTime);
    }
};


// 用例
//<input type="text" oninput="" />

//DelayOperate(function () {
//    if (self.IsDepartment()) {
//        var requestUrl = '/Api/Department/Query?value=' + value;
//        $.post(requestUrl, null, function (ret) {
//            if (ret != null)
//                self.Departments(ret);
//            else
//                self.Departments([]);
//        });
//    } else {
//        var requestUrl = '/Api/Employee/Query?value=' + value;
//        $.post(requestUrl, null, function (ret) {
//            if (ret != null) {
//                self.Employeess(ret);
//            }
//            else
//                self.Employeess([]);
//        });
//    }
//}, 500);