<template>
    <div class="evaluation">
        <table>
            <tr>
                <td width="70%">名称</td>
                <td width="30%">操作</td>
            </tr>
            <tr v-for="(list,index) in evaluationList">
                <td>{{list.title}}</td>
                <td>
                    <span v-bind:class="list.operation?'':'isOperation'" v-text="list.operation?'已发布':'发布'" @click="release(index)"></span>
                    <span class="check_over" @click="checkOver(index)">查看</span>
                </td>
            </tr>
        </table>
        <div class="paginationW">
            <el-pagination @current-change="handleCurrentChange" :current-page.sync="currentPage" :page-size="pagenum" layout="total, prev, pager, next, jumper" :total="total">
            </el-pagination>
        </div>
    </div>
</template>

<script>
import { ManageerTable } from '../../api/departmentResult.js';
import { ChangePublish } from '../../api/departmentResult.js';
export default {
    data() {
        return {
            total: 10,
            currentPage: 1,
            pagesize: 1,
            pagenum: 10,
            evaluationList: ''
        }
    },
    created() {
        //初始化列表
        this.ManageerTableFilter(1, this.pagenum);
    },
    methods: {
        //发布
        release: function (index) {
            var reportyear = this.evaluationList[index].reportyear;
            var reportquarter = this.evaluationList[index].reportquarter;
            var operation = this.evaluationList[index].operation;
            if (operation) {
                return false
            }
            else {
<<<<<<< .mine
                 ChangePublish({year:reportyear,qutor:reportquarter}).then(res => {
                  let { data } = res;
                  if (data.result){
                    this.evaluationList[index].operation = true;
                    this.$message({
                        message:'发布成功！',
                        type:'success'
                    })
                  }
                  else{
                     this.$message({
                        message:'发布失败！',
                        type:'error'
                    })
                  }
                });
=======
                let data = { 
                    year: reportyear, 
                    qutor: reportquarter 
                };
                ChangePublish(data).then(res => {
                    let { data } = res;
                    if (data.result) {
                        this.evaluationList[index].operation = true;
                        this.$message({
                            message: '发布成功！',
                            type: 'success'
                        })
                    }
                    else {
                        this.$message({
                            message: '发布失败！',
                            type: 'error'
                        })
                    }
                });
>>>>>>> .r478
            }

        },
        checkOver(index) {
            var year = this.evaluationList[index].reportyear;
            var qutor = this.evaluationList[index].reportquarter;
            this.$router.push({ name: '所有部门考核结果', params: { year: year, qutor: qutor, from: 'commitee' } })

        },
        handleCurrentChange(val) {
            this.ManageerTableFilter(this.currentPage, this.pagenum);
        },
        //初始化 请求
        ManageerTableFilter(pagenum, pagesize) {
            ManageerTable({ pagenum, pagesize }).then(res => {
                let { data } = res;
                console.log(res);
                if (data) {
                    this.total = data.total;
                    this.evaluationList = data.evaluationList;
                    console.log(data);
                }
                else {
                    this.$message({
                        message: "没对应的数据！",
                        type: 'error'
                    });
                }
            });
        }


    }
}
</script>

<style scoped>
.evaluation table {
    width: 100%;
    table-layout: fixed;
    border-collapse: collapse;
    text-align: center;
}

.evaluation table td {
    height: 45px;
    border: solid 1px #dadada;
}

.evaluation table td span {
    display: inline-block;
    width: 80px;
    line-height: 24px;
    margin: 0 10px;
    border-radius: 2px;
}

.evaluation table td .check_over {
    border: solid 1px #dadada;
    cursor: pointer;
}

.evaluation table td .isOperation {
    background: #20a0ff;
    color: white;
    cursor: pointer;
    line-height: 26px;
}
.evaluation .paginationW{
    margin: 20px 0;
}
</style>
