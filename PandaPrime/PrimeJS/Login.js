$(function () {
    $('#signIn').click(function () {
        var account = $.trim($('#Account').val());
        var password = $.trim($('#Password').val());
        var validateCode = $('#ValidateCode').val();

        if (account.length == 0) {
            bootbox.alert({
                size: "small",
                message: "请输入账户信息！"
            });
            refreshCode();
            return;
        }
        if (password.length == 0) {
            bootbox.alert({
                size: "small",
                message: "请输入密码！"
            });
            refreshCode();
            return;
        }
        if (validateCode.length == 0) {
            bootbox.alert({
                size: "small",
                message: "请输入验证码！"
            });
            refreshCode();
            return;
        }

        $.$Post({
            url: '/Login/Login',
            data: { Account: account, Password: password, ValidateCode: validateCode },
            success: function (data) {
                if (data != null) {
                    if (data.Flag == 0) {
                        bootbox.alert({
                            size: "small",
                            message: data.Message,
                            callback: function () {
                                if (data.Flag == 0) {
                                    refreshCode();
                                }
                            }
                        });
                    } else {
                        window.location.href = "../BaseInfo/Universe/console.html";
                    }
                }
            }
        });      
    });
});