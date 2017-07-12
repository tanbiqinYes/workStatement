<template>
    <div class="staffManagement">
        <div class="operateBtn">
            <el-input @keyup.enter.native="handlesoso" style="width: 200px;" class="filter-item" placeholder="请输入搜索内容" v-model="inputsoso">
            </el-input>
    
            <el-button :loading="loadBtnso" style="margin-left:10px;" class="filter-item" type="primary" icon="search" @click="handlesoso">
                搜索
            </el-button>
    
            <el-button class="filter-item" type="primary" icon="document" @click="headleAddStaff">
                添加员工
            </el-button>
    
        </div>
    
        <el-table :data="tableData.data" border style="width: 100%">
            <el-table-column label="姓名" min-width="100" align="center">
                <template scope="scope">
                    <span>{{ scope.row.username }}</span>
                </template>
            </el-table-column>
            <el-table-column label="部门" align="center" min-width="120">
                <template scope="scope">
                    <span>{{ scope.row.depart }}</span>
                </template>
            </el-table-column>
            <el-table-column label="岗位" align="center" min-width="120">
                <template scope="scope">
                    <span>{{ scope.row.work }}</span>
                </template>
            </el-table-column>
            <el-table-column label="角色" align="center" min-width="110">
                <template scope="scope">
                    <span>{{ scope.row.role }}</span>
                </template>
            </el-table-column>
            <el-table-column label="级别" align="center" min-width="70">
                <template scope="scope">
                    <span>{{ scope.row.userlevel }}</span>
                </template>
            </el-table-column>
            <el-table-column label="账号" align="center" min-width="120">
                <template scope="scope">
                    <span>{{ scope.row.accountname }}</span>
                </template>
            </el-table-column>
            <el-table-column label="邮箱" align="center" min-width="130">
                <template scope="scope">
                    <span>{{ scope.row.email }}</span>
                </template>
            </el-table-column>
            <el-table-column label="操作" width="180" align="center">
                <template scope="scope">
                    <el-button size="small" type="primary" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                    <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
    
        <div class="paginationW">
            <el-pagination @current-change="handleCurrentChange" :current-page.sync="currentPage" :page-size="pageNum" layout="total, prev, pager, next, jumper" :total="totalPage">
            </el-pagination>
        </div>
    
        <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
            <el-form class="small-space" :model="temp" label-position="right" label-width="100px" style='width: 400px; margin:0 auto;'>
    
                <el-form-item label="姓名">
                    <el-input v-model="temp.username" style='width: 217px;'></el-input>
                </el-form-item>
    
                <el-form-item label="部门">
                    <el-select class="filter-item" v-model="temp.depart" placeholder="请选择">
                        <el-option v-for="item in departOptions" :key="item" :label="item" :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
    
                <el-form-item label="岗位">
                    <el-input class="filter-item" v-model="temp.work" style='width: 217px;'></el-input>
                </el-form-item>
    
                <el-form-item label="角色">
                    <el-select class="filter-item" v-model="temp.role" placeholder="请选择">
                        <el-option v-for="item in roleOptions" :key="item" :label="item" :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
    
                <el-form-item label="级别">
                    <el-select class="filter-item" v-model="temp.userlevel" placeholder="请选择">
                        <el-option v-for="item in levelOptions" :key="item" :label="item" :value="item">
                        </el-option>
                    </el-select>
                </el-form-item>
    
                <el-form-item label="账号" v-show="dialogStatus == 'create'">
                    <el-input v-model="temp.accountname" style='width: 217px;'></el-input>
                </el-form-item>
    
                <el-form-item label="邮箱">
                    <el-input v-model="temp.email" style='width: 217px;'></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer" style="text-align:center">
                <el-button :loading="loadBtn2" v-if="dialogStatus=='create'" size="large" type="primary" @click="addStaff">添 加</el-button>
                <el-button :loading="loadBtn2" v-else type="primary" size="large" @click="updateStaff">更 新</el-button>
                <el-button @click="dialogFormVisible = false" size="large">取 消</el-button>
            </div>
        </el-dialog>
    
    </div>
</template>

<script>
import { getUserList, Register, DelUser, EditUser } from '../../api/staffManager';
import { mapGetters } from 'vuex';
import { isEmail } from '../../utils/validate';

export default {
    data() {
        return {
            tableData: { data: null },
            inputsoso: "",
            inputsosucess: "",
            loadBtn: false,
            loadBtnso: false,
            loadBtn2: false,
            loadBtnCancle: false,
            dialogFormVisible: false,
            dialogStatus: 'update',
            textMap: {
                update: '编辑员工',
                create: '添加员工'
            },
            // levelOptions: ['1', '2', '3'],
            // // roleOptions: ['管理员', '主管'],
            // departOptions: ['工程部', '创意部', '研发部'],
            positionOptions: ['UI设计', 'web前端开发'],
            temp: {
                username: "",
                userid: "",
                depart: '',
                role: "",
                accountname: '',
                userlevel: "",
                work: "",
                email: ""
            },
            tempIndex: -1,
            totalPage: 0,
            currentPage: 1,
            pageNum: 10,
            keyWord: ""

        }
    },

    computed: {
        ...mapGetters([
            'roleOptions',
            'departOptions',
            'levelOptions',
        ])
    },

    created() {
        //do something after creating vue instance
        this.getUserListFilter(1, this.pageNum);
    },

    methods: {

        handlesoso() {
            this.loadBtnso = true;
            this.currentPage = 1;
            this.getUserListFilter(1, this.pageNum, this.inputsoso, "");
        },

        handleCurrentChange(val) {
            // console.log(`当前页: ${val}`);
            // this.currentPage = val;
            this.getUserListFilter(this.currentPage, this.pageNum, this.inputsosucess, "", true);
        },

        handleEdit(index, row) {
            // console.log(index, row);
            this.temp = Object.assign({}, row);
            this.tempIndex = index;
            this.dialogStatus = 'update';
            this.dialogFormVisible = true;
        },

        handleDelete(index, row) {
            // console.log(index, row);DelUser
            if (this.loadBtnCancle) return false;
            else this.loadBtnCancle = true;

            var self = this;

            this.$confirm(`确认删除${row.username}这个员工?`, '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                var userid = row.userid;
                var DominoName = row.accountname;
                DelUser({ userid, DominoName }).then(res => {
                    let { data } = res;
                    if (res.status == 200) {
                        this.tableData.data.splice(index, 1);
                        this.getUserListFilter(this.currentPage, this.pageNum, this.inputsosucess, "");
                        this.$message({
                            message: "删除成功！",
                            type: 'success'
                        });
                        this.loadBtnCancle = false;
                    }
                    else {
                        this.$message({
                            message: "没此请求的返回数据！",
                            type: 'error'
                        });
                        this.loadBtnCancle = false;
                    }
                })
                    .catch(error => {
                        if (error.response && error.response.status == 400) {
                            this.$message({
                                message: error.response.data.ModelState,
                                type: 'error',
                                duration: 5 * 1000
                            });
                        }
                        else {
                            this.$message({
                                message: error,
                                type: 'error',
                                duration: 5 * 1000
                            });
                        }
                        this.loadBtnCancle = false;
                    });


            }).catch((err) => {
                self.$message({
                    message: err == 'cancel' ? "这是理智的选择！" : err.toString(),
                    type: 'success'
                });
                this.loadBtnCancle = false;
            });


        },

        headleAddStaff() {
            this.temp = {
                username: "",
                userid: "",
                depart: '',
                role: "",
                accountname: '',
                userlevel: "",
                work: ""
            };
            this.tempIndex = -1;
            this.dialogStatus = 'create';
            this.dialogFormVisible = true;
        },

        validateForm(flag) {
            var overFlag = true;
            var msg = "";

            var UserName = this.temp.username;
            var Depart = this.temp.depart;
            var Work = this.temp.work;
            var Role = this.temp.role;
            var userlevel = this.temp.userlevel;
            var DominoName = this.temp.accountname;
            var email = this.temp.email;

            if (UserName == "" || UserName == undefined) {
                msg = "请填写姓名";
                overFlag = false;
            }
            else if (Depart == "" || Depart == undefined) {
                msg = "请选择部门";
                overFlag = false;
            }
            else if (Work == "" || Work == undefined) {
                msg = "请填写岗位";
                overFlag = false;
            }
            else if (Role == "" || Role == undefined) {
                msg = "请选择角色";
                overFlag = false;
            }
            else if (userlevel == "" || userlevel == undefined) {
                msg = "请选择级别";
                overFlag = false;
            }
            else if ((DominoName == "" || DominoName == undefined) && flag != 'update') {
                msg = "请填写账号";
                overFlag = false;
            }
            else if (!isEmail(email)) {
                msg = "填写邮件格式不正确";
                overFlag = false;
            }

            if (!overFlag) {
                this.$message({
                    message: msg,
                    type: 'error'
                });
                return false;
            }

            return true;
        },

        addStaff() {
            // console.log(this.tempIndex)
            var Email = this.temp.email;
            var DominoName = this.temp.accountname;
            var Role = this.temp.role;
            var Work = this.temp.work;
            var Depart = this.temp.depart;
            var UserName = this.temp.username;
            var Level = this.temp.userlevel;
            this.loadBtn2 = true;
            if (!this.validateForm()) {
                this.loadBtn2 = false;
                return false;
            }

            Register({ Email, DominoName, Role, Work, Depart, UserName, Level }).then(res => {
                let { data } = res;
                if (data) {
                    // this.tableData = data;
                    this.inputsoso = "";
                    this.getUserListFilter(0, this.pageNum, "", "");
                    this.currentPage = 1;
                    this.dialogFormVisible = false;
                    this.$message({
                        message: "添加成功！",
                        type: 'success'
                    });

                    this.loadBtn2 = false;
                }
                else {
                    this.$message({
                        message: "没此项目的数据！",
                        type: 'error'
                    });
                    this.loadBtn2 = false;
                }
            })
                .catch(error => {
                    if (error.response && error.response.status == 400) {
                        this.$message({
                            message: error.response.data.ModelState,
                            type: 'error',
                            duration: 5 * 1000
                        });
                    }
                    else {
                        this.$message({
                            message: error,
                            type: 'error',
                            duration: 5 * 1000
                        });
                    }
                    this.loadBtn2 = false;
                });

        },

        updateStaff() {
            var Email = this.temp.email;
            var Role = this.temp.role;
            var Work = this.temp.work;
            var Depart = this.temp.depart;
            var UserName = this.temp.username;
            var UserId = this.temp.userid;
            var Level = this.temp.userlevel;
            this.loadBtn2 = true;
            if (!this.validateForm("update")) {
                this.loadBtn2 = false;
                return false;
            }

            EditUser({ Email, UserId, Role, Work, Depart, UserName, Level }).then(res => {
                let { data } = res;
                if (res.status == 200) {
                    // this.tableData = data;
                    // this.getUserListFilter(0,10,"","");
                    var obj = _.cloneDeep(this.temp);
                    this.tableData.data.splice(this.tempIndex, 1, obj);
                    this.dialogFormVisible = false;
                    this.$message({
                        message: "编辑成功！",
                        type: 'success'
                    });

                    this.loadBtn2 = false;
                }
                else {
                    this.$message({
                        message: "没此项目的数据！",
                        type: 'error'
                    });
                    this.loadBtn2 = false;
                }
            })
                .catch(error => {
                    if (error.response && error.response.status == 400) {
                        this.$message({
                            message: error.response.data.ModelState,
                            type: 'error',
                            duration: 5 * 1000
                        });
                    }
                    else {
                        this.$message({
                            message: error,
                            type: 'error',
                            duration: 5 * 1000
                        });
                    }
                    this.loadBtn2 = false;
                });
        },

        getUserListFilter(pagenum, pagesize, keyword, orderby, noreplace) {
            this.loadBtn = true;
            pagenum = pagenum ? pagenum : 0;
            pagesize = pagesize ? pagesize : 10;
            keyword = keyword ? keyword : "";
            orderby = orderby ? orderby : "";
            const params = { pagenum, pagesize, keyword, orderby };
            getUserList(params).then(res => {
                let { data } = res;
                if (res.status == 200) {
                    this.$set(this.tableData, "data", data.data);
                    this.totalPage = data.total;
                    if (!noreplace) this.inputsosucess = this.inputsoso;
                    this.loadBtn = false;
                    this.loadBtnso = false;
                }
                else {
                    this.$message({
                        message: "没此项目的数据！",
                        type: 'error'
                    });
                    this.loadBtn = false;
                    this.loadBtnso = false;
                }
            })
                .catch(err => {
                    if (err.response.status == 404) {
                        this.$message({
                            message: "没有查询到数据",
                            type: 'error'
                        });
                    }
                    this.loadBtn = false;
                    this.loadBtnso = false;
                });

        }

    }
}
</script>

<style lang="scss">
.staffManagement {
    width: 100%;
    margin: 0 auto;
    /*max-width: 1200px;*/
    .operateBtn {
        padding-bottom: 30px;
        padding-top: 18px;
    }
    .paginationW {
        padding: 30px 0px 50px;
    }
    .el-table {
        td,
        th {
            height: 45px;
            .cell {
                line-height: 22.5px;
            }
        }
    }
    .el-dialog__footer {
        padding: 0px 20px 40px;
    }
    .el-form-item__label {
        padding-right: 25px;
    }
}
</style>
