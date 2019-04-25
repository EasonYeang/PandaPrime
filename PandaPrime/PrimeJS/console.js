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
                }).catch(function (error) {
                    console.log(error);
                });
            },
            onSelectSide: function (key, keyPath) {
                console.log(key, keyPath);
            },
            onSelectTop: function (key) {
                var $this = this;
                $.axios({
                    url: '/DefaultPage/GetSidePermissions',
                    data: { sn: key }
                }).then(function (response) {
                    if (response !== null) {
                        var data = response.data;
                        if (data !== null && data.length > 0) {
                            $this.sidePermissionList = data;
                        } else {
                            $this.sidePermissionList = [];
                        }
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            }
        },
        watch: {
            topActive: function (val, oldVal) {
                this.onSelectTop(val);
            }
        },
        mounted: function () {
            this.getTopPermissions();
        }
    });
});