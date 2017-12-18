function ConvertToChinese(number) {
    var num = ['零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖'];
    var unit = ['', '拾', '佰', '仟', '万', '拾', '佰', '仟', '亿', '拾', '佰', '仟', '万', '亿'];
    var value = parseFloat(number);
    if (value != NaN) {
        value = value + '';
        var _value = '';
        // 转换为大写
        value.split('').forEach(function (v, i) {
            _value += v == '.' ? '.' : num[v];
        });
        var data = [];
        _value.split('.')[0].split('').reverse().forEach(function (v, i) {
            if (v != '零') {
                data.push(v + unit[i]);
            } else {
                if (unit[i] == '万') data.push('万')
                else if (unit[i] == '亿') data.push('亿');
                else data.push('零');
            }
        });
        // 此方式部分浏览器无法正常执行，无法使用的时候，请使用注释代码
        return data.reverse().toString().replace(new RegExp(",", "gm"), '').replace(new RegExp(/零{1,}/g, 'gm'), '零').replace(new RegExp('零$', 'gm'), '') + (_value.split('.')[1] ? '.' + _value.split('.')[1] : '');
        //return data.reverse().toString().replace(new RegExp(",", "gm"), '').replace(new RegExp('零{1,}', 'gm'), '零').replace(new RegExp('零$', 'gm'), '') + (_value.split('.')[1] ? '.' + _value.split('.')[1] : '');
    }
    return NaN;
};