$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            sidePermissionList: [],
            src: '../../BaseInfo/Universe/welcome.html',
            activeNav: 3
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
            clickSide: function (detail) {
                var filePath = detail.FilePath;
                if (detail.Level === 1) {
                    this.activeNav = detail.SerialNumber;
                } else {
                    this.calActiveNav(detail.ParentSN);
                }
                if (filePath != null) {
                    if (filePath.indexOf('?') === -1) {
                        filePath = `${filePath}?timeStamp=${timeStamp()}`;
                    } else {
                        filePath = `${filePath}&timeStamp=${timeStamp()}`;
                    }
                    this.src = filePath;
                }
            },
            calActiveNav: function (psn) {
                var THIS = this;
                $.axios({
                    url: '/DefaultPage/CalActiveNav',
                    data: { psn: psn }
                }).then((response) => {
                    var data = response.data;
                    if (data != null) {
                        THIS.activeNav = data;
                    }
                }).catch((error) => { console.log(error) });
            }
        },
        mounted: function () {
            this.getSidePermissionList(1);
        }
    });

});