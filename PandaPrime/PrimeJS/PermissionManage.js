$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            tableData: [],
            parentPermissionList: [],
            formModal: false,
            formValidate: {
                Title: '',
                PSN: '',
                Path: '',
                Icon: '',
                Order: '',
                Lv: 0
            },
            ruleValidate: {
                Title: [
                    { required: true, message: '目录名称不能为空', trigger: 'blur' }
                ],
                PSN: [
                    { required: true, message: '父目录不能为空', trigger: 'blur' }
                ],
            },
            seen: false,
        },
        methods: {
            getTableData: function () {
                var $this = this;
                $this.loading = true;
                $.$Post({
                    url: '/PermissionManage/GetTableData',
                    success: function (result) {
                        if (result != null) {
                            $this.tableData = result;
                        }
                    }
                });
            },
            handleDelete: function (index, row) {
                this.$confirm('正在执行删除操作，是否继续？', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    var $this = this;
                    $.$Post({
                        url: '/PermissionManage/OnDelete',
                        data: { id: row.ID },
                        success: function (result) {
                            if (result != null) {
                                var flag = result.Flag;
                                var msg = result.Message;
                                $this.$Message.success(msg);
                                if (flag === 1) {
                                    $this.getTableData();
                                }
                            }
                        }
                    });
                }).catch(() => {

                });
            },
            handleRefresh: function () {
                this.getTableData();
            },
            showForm: function () {
                this.formModal = true;
            },
            asyncOK: function () {
                this.$refs['formValidate'].validate((valid) => {
                    if (valid) {
                        var $this = this;
                        $.$Post({
                            url: '/PermissionManage/AddOrUpdate',
                            data: { pm: $this.formValidate, id: 0 },
                            success: function (result) {
                                if (result != null) {
                                    var flag = result.Flag;
                                    var msg = result.Message;
                                    if (flag === 1) {
                                        $this.$Message.success(msg);
                                        $this.formModal = false;
                                    } else {
                                        $this.$Message.error(msg);
                                    }
                                }
                            }
                        });
                    }
                })
            },
            onSelectAll: function (selection) {
                if (selection.length > 1) {
                    this.seen = true;
                } else {
                    this.seen = false;
                }
            },
            onSelect: function (selection, row) {
                if (selection.length > 1) {
                    this.seen = true;
                } else {
                    this.seen = false;
                }
            },
            getParentPermissionList() {
                //父级目录下拉列表
                var $this = this;
                $.$Post({
                    url: '/PermissionManage/GetParentPermissionList',
                    data: { parentLevel: $this.parentLevel },
                    success: function (result) {
                        if (result != null && result.length > 0) {
                            $this.parentPermissionList = result;
                        }
                    }
                });
            }
        },
        mounted: function () {
            this.getTableData();
            this.getParentPermissionList();
        }
    });
});