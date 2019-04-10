$(function () {
    new Vue({
        el: '#app',
        data: {
            activeTop: 0,
            activeSide: 0,
            openNames: [],
            permissons: [],
            sidePermissions: [],
            breadcrumb: []
        },
        methods: {
            selectMenu: function (name) {
                if (name == 'logOut') {
                    $.$Post({
                        url: '/DefaultPage/LogOut',
                        success: function (result) {
                            var flag = result.Flag;
                            if (flag === 1) {
                                window.location.href = '/Login/Index';
                            }
                        }
                    });
                } else {
                    this.getSidePermissions(name);
                }
            },
            selectSideMenu: function (sn) {
                var $this = this;
                $.$Post({
                    url: '/DefaultPage/SelectSideMenu',
                    data: { sn: sn },
                    success: function (result) {
                        $('#iframe_content').attr('src', result);
                    }
                });
            },
            getTopPermissions: function () {
                var $this = this;
                $.$Post({
                    url: '/DefaultPage/GetTopPermissions',
                    success: function (result) {
                        if (result != null && result.length > 0) {
                            $this.activeTop = result[0].SerialNumber;
                        }
                        $this.permissons = result;
                        $this.$nextTick(() => {
                            $this.$refs.activeTop.updateOpened();
                            $this.$refs.activeTop.updateActiveName();

                        });
                    }
                });
            },
            getSidePermissions: function (serialNumber) {
                var $this = this;
                $.$Post({
                    url: '/DefaultPage/GetSidePermissions',
                    data: { sn: serialNumber },
                    success: function (result) {
                        if (result != null && result.length > 0) {
                            if (getQueryString("activeSide") == null) {
                                $this.activeSide = result[0].SerialNumber;
                            }
                            //$this.activeSide = 6;
                            //$this.openNames = [3, 5];
                        }
                        $this.sidePermissions = result;
                        $this.$nextTick(() => {
                            $this.$refs.activeSide.updateOpened();
                            $this.$refs.activeSide.updateActiveName();

                        });
                    }
                });
            },
            collapsedSider: function () {
                this.$refs.side1.toggleCollapse();
            },
            SetMenuDefault: function () {
                var openNames = getQueryString("openNames");
                var activeSide = getQueryString("activeSide");
                var nav = getQueryString("nav");
                this.activeSide = parseInt(activeSide) || 0;
                if (openNames != null) {
                    if (openNames.indexOf(',') != -1) {
                        var arr = [];
                        var s = openNames.split(',');
                        $.each(s, function (index, value) {
                            arr.push(parseInt(value));
                        });
                        this.openNames = arr;
                    }
                }
                this.$nextTick(() => {
                    this.$refs.activeSide.updateOpened();
                    this.$refs.activeSide.updateActiveName();

                });


            },
            getPermissionNav: function () {
                if (getQueryString("openNames") != null && getQueryString("activeSide") != null) {
                    var $this = this;
                    $.$Post({
                        url: '/DefaultPage/GetPermissionNav',
                        data: { openNames: getQueryString("openNames"), aS: getQueryString("activeSide") },
                        success: function (result) {
                            if (result.length > 0) {
                                $this.breadcrumb = result;
                            }
                        }
                    });
                }
            }
        },
        watch: {
            activeTop: function (val) {
                this.getSidePermissions(val);
            }
        },
        computed: {
            rotateIcon: function () {
                return [
                    'menu-icon',
                    this.isCollapsed ? 'rotate-icon' : ''
                ];
            }
        },
        mounted: function () {
            this.getTopPermissions();
            this.SetMenuDefault();
            this.getPermissionNav();
        }
    })
});