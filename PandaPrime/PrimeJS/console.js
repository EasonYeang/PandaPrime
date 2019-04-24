$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            permissionList: [],
            activeIndex2: '1'
        },
        methods: {
            getTopPermissions: function () {
                var $this = this;
                axios({
                    method: 'post',
                    url: '/DefaultPage/GetTopPermissions'
                }).then(function (response) {
                    if (response != null) {
                        var data = response.data;
                        if (data != null && data.length > 0) {
                            $this.permissionList = data;
                        }
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            },
            handleOpen: function (key, keyPath) {
                console.log(key, keyPath);
            },
            handleClose: function (key, keyPath) {
                console.log(key, keyPath);
            },
            handleSelect: function (key, keyPath) {
                console.log(key, keyPath);
            }
        },
        mounted: function () {
            this.getTopPermissions();
        }
    });
});