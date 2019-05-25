$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            sidePermissionList: [],
            title: '控制台'
        },
        methods: {
            handleClick: function (e, item) {
                var tagName = e.currentTarget.tagName;
                if (tagName === 'LI') {
                    $(e.currentTarget).addClass('active').siblings().removeClass('active');
                } else if (tagName === 'A') {
                    $(e.currentTarget).parent().addClass('active').siblings().removeClass('active');
                    $(e.currentTarget).parents('.treeview').siblings().find('li').removeClass('active');
                }
                if (item != null) {
                    $('#iframecon').attr('src', item.FilePath);
                    this.title = item.Name;
                }
            },
            getSidePermissionList: function (key) {
                var $this = this;
                $.axios({
                    url: '/DefaultPage/GetSidePermissions',
                    data: { sn: 0 }
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
        },
        mounted: function () {
            this.getSidePermissionList();
        }
    });
});

/**
 * iframe高度自适应
 */
function iFrameHeight() {
    var ifm = document.getElementById("iframecon");
    var subWeb = document.frames ? document.frames["iframepage"].document :
        ifm.contentDocument;
    if (ifm != null && subWeb != null) {
        ifm.height = subWeb.body.scrollHeight;
    }
}