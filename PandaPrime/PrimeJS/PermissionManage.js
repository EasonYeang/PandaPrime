$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            searchText: '',
            pageIndex: 1,
            pageSize: 10,
            total: 0,
            cols: [
                {
                    title: '目录名称',
                    key: 'Title'
                },
                {
                    title: '目录编号',
                    key: 'Number'
                },
                {
                    title: '图标',
                    key: 'Icon'
                },
                {
                    title: '路径',
                    key: 'Path'
                }
            ],
            tableData: [],
            fullscreenLoading: true
        },
        methods: {
            handleSizeChange: function (val) {
                console.log(`每页 ${val} 条`);
            },
            handleCurrentChange: function (val) {
                console.log(`当前页: ${val}`);
            },
            getPermissionList: function () {
                var $this = this;
                $.axios({
                    url: '/PermissionManage/GetTableData',
                    data: { pageNumber: $this.pageIndex, pageSize: $this.pageSize }
                }).then((response) => {
                    var data = response.data;
                    if (data != null) {
                        $this.tableData = data.rows;
                        $this.total = data.TC;
                    } else {
                        $this.tableData = [];
                    }
                    console.log(response);
                }).catch((error) => {
                    console.log(error);
                });
            }

        },
        beforeCreate: function () {
        },
        mounted: function () {
            this.getPermissionList();
        }
    });
});