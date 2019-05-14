$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            sidePermissionList: [],
            src: '../../BaseInfo/Universe/welcome.html'
        },
        methods: {
            getSidePermissionList: function (key) {
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
            },
            logOut: function () {
                $.axios({
                    url: '/DefaultPage/LogOut'
                }).then((response) => {
                    var data = response.data;
                    if (data != null) {
                        if (data.Flag === 1) {
                            window.location = '/Login/Index';
                        }
                    }
                }).catch((error) => { console.log(error) });
            },
            clickSide: function (filePath) {
                console.log(filePath);
                if (filePath != null) {
                    if (filePath.indexOf('?') === -1) {
                        filePath = `${filePath}?timeStamp=${timeStamp()}`;
                    } else {
                        filePath = `${filePath}&timeStamp=${timeStamp()}`;
                    }
                    this.src = filePath;
                }
            }
        },
        mounted: function () {
            this.getSidePermissionList(1);
            console.log(this);

            window.layui.use('element', function () {
                var element = layui.element;
            });
        }
    });

});