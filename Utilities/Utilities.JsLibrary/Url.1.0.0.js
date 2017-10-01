'use strict'
/*******************
*   作者：任茂波
*   备注：用户获取url信息
*******************/
var url = function () {
    var self = this;
    self.ReadingParams = function () {
        var value = location.search;
        if (typeof (value) == 'string') {
            var index = value.indexOf('?')
            if (index == 0)
                value = value.replace('?', '');
            else
                value = value.substring(index + 1, value.length);

            var items = value.split('&');
            var data = {};
            for (var i = 0; i < items.length; i++) {
                var item = items[i].split('=');
                if (item.length == 2)
                    data[item[0]] = item[1];
            }
            return data;
        }
        return null;
    };
    self.uri = {
        url: location.href,
        host: location.host,
        hostname: location.hostname,
        port: location.port,
        pathname: location.pathname,
        protocol: location.protocol,
        search: location.search,
        params: self.ReadingParams()
    };
    return self.uri;
}();