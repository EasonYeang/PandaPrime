/**
 * 生成唯一的GUID
 */
function getGuid() {
    var s4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return s4() + s4() + s4() + "-" + s4();
}

/**
 * 刷新验证码
 */
function refreshCode() {
    var oldSrc = $('#codeImg').attr('src');
    var newSrc = oldSrc.split('?')[0] + '?' + getGuid();
    $('#codeImg').attr('src', newSrc);
}

/**
 * 增加AntiForgeryToken
 * @param {any} data
 */
function AddAntiForgeryToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
};

/**
 * 获取URL参数
 * @param {any} name
 */
function getQueryString(name) {
    var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}

/**
 * 时间戳
 */
function timeStamp() {
    return (new Date()).valueOf();
}