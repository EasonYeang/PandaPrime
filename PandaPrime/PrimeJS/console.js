$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            topPermissionList: [],
            activeIndex2: '1'
        },
        methods: {
            getTopPermissions: function () {
                var $this = this;
                $.axios({
                    url: '/DefaultPage/GetTopPermissions',
                }).then(function (response) {
                    if (response !== null) {
                        var data = response.data;
                        if (data !== null && data.length > 0) {
                            $this.topPermissionList = data;
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
            onSelectTop: function (key, keyPath) {
                var $this = this;
                axios({
                    method: 'post',
                    url: '/DefaultPage/GetSidePermissions'
                }).then(function (response) {
                    if (response !== null) {
                        var data = response.data;
                        if (data !== null && data.length > 0) {
                            $this.permissionList = data;
                        }
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            }
        },
        mounted: function () {
            this.getTopPermissions();
        }
    });
});