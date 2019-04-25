$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            topPermissionList: [],
            sidePermissionList: [],
            topActive: ''
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
                            $this.topActive = data[0].SerialNumber.toString();
                        }
                    }
                    console.log($this);
                }).catch(function (error) {
                    console.log(error);
                });
            },
            onSelectSide: function (key, keyPath) {
                console.log(key, keyPath);
            },
            onSelectTop: function (key, keyPath) {
                var $this = this;
                $.axios({
                    url: '/DefaultPage/GetSidePermissions',
                    data: { sn: key }
                }).then(function (response) {
                    if (response !== null) {
                        var data = response.data;
                        if (data !== null && data.length > 0) {
                            $this.sidePermissionList = data;
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