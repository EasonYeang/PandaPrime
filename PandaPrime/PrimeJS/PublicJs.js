jQuery.extend({
    $Post: function (options) {
        var lists = {
            url: options.url,
            data: options.data || {},
            type: options.type || "POST",
            dataType: options.dataType || "JSON",
            async: options.async === false ? false : true,
            //contentType: options.contentType || "application/json",
            cache: options.cache === false ? false : true
        }
        $.ajax({
            type: lists.type,
            dataType: lists.dataType,
            url: lists.url,
            beforeSend: function (XHR) {
                XHR.setRequestHeader("Token", (localStorage.getItem("Token") || ""));
            },
            //contentType: lists.contentType,
            data: lists.data,
            async: lists.async,
            cache: lists.cache,
            success: function (data) {
                if (lists.dataType.toUpperCase() == "XML") {
                    data = $(data).text();
                    data = JSON.parse(data);
                    data = data.d == null ? data : data.d;
                    if (typeof data != "object") {
                        try {
                            data = JSON.parse(data);
                        } catch (e) {
                            data = data;
                        }
                    }
                } else if (lists.dataType.toUpperCase() == "JSON") {
                    data = data.d == null ? data : data.d;
                    if (data != "" && data != null) {
                        if (typeof data != "object") {
                            try {
                                data = JSON.parse(data);
                            } catch (e) {
                                data = data;
                            }
                        }
                    }
                }
                options.success && options.success(data);
            },
            complete: function (data) {
                options.complete && options.complete(data);
            }
        }).always(function () {
            options.always && options.always();
        });

    }
});

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

