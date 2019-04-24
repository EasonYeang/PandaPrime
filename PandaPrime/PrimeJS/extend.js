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

    },
    axios: function (options) {
        var pms = {
            method: options.method || "POST",
            url: options.url,
            data: options.data,
            responseType: options.responseType || 'json'
        }
        return axios({
            method: pms.method,
            url: pms.url,
            data: pms.data,
            responseType: pms.responseType
        });
    }
});