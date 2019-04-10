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
            //-------------------------
            , cols: [],
            tableData: [],
            total: 0,
            loading: true,
            formInline: {
                user: '',
                password: ''
            },
            formModal: false,
            formValidate: {
                Title: '',
                Path: '',
                Icon: '',
                order: ''
            },
            ruleValidate: {
                Title: [{
                    required: true, message: '目录名称不能为空', trigger: 'blur'
                }],
                Path: [{
                    required: true, message: '目录路径不能为空', trigger: 'blur'
                }]
            },
            pplist: []
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
                        window.location = result;
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
            }
            //-------------------------
            , handleSubmit(name) {
                this.$refs[name].validate((valid) => {
                    if (valid) {
                        this.$Message.success('Success!');
                    } else {
                        this.$Message.error('Fail!');
                    }
                })
            },
            handleRefresh: function () {
                this.getTableData();
            },
            getTableData: function () {
                var $this = this;
                $this.loading = true;
                $.$Post({
                    url: '/PermissionManage/GetTableData',
                    success: function (result) {
                        if (result != null) {
                            $this.cols = result.cols;
                            $this.tableData = result.tableData;
                            $this.total = result.total;
                        }
                    },
                    always: function () {
                        $this.loading = false;
                    }
                });
            },
            showForm: function () {
                this.formModal = true;
            },
            asyncOK: function () {
                //setTimeout(() => {
                //    this.formModal = false;
                //}, 2000);
                this.$refs['formValidate'].validate((valid) => {
                    if (valid) {
                        this.$Message.success('Success!');
                    } else {
                        this.$Message.error('Fail!');
                    }
                })
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
            this.getTableData();
        }
    })
});