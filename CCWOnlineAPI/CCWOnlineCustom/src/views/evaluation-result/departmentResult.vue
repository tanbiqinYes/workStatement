<template>
    <div class="check">
        <h3>{{checkData.title}}</h3>
        <p>
            <a style="display:none" href="javascript:;void(0)">打印</a>
        </p>
        <table>
            <tr>
                <th>姓名</th>
                <th>部门</th>
                <th>职业修养考核得分</th>
                <th>职业能力考核得分</th>
                <th>综合职业考核得分</th>
                <th>综合评定等级</th>
                <th>考核系数</th>
            </tr>
            <tr v-for="item in checkData.checkList">
                <td>{{item.name}}</td>
                <td>{{item.department}}</td>
                <td>{{item.score[0]}}</td>
                <td>{{item.score[1]}}</td>
                <td>{{item.score[2]}}</td>
                <td>{{item.score[3]}}</td>
                <td>{{item.score[4]}}</td>
            </tr>
        </table>
        <div class="paginationW">
            <el-pagination @current-change="handleCurrentChange" :current-page.sync="checkData.currentPage" :page-size="pagenum" layout="total, prev, pager, next, jumper" :total="checkData.total">
            </el-pagination>
        </div>
    </div>
</template>
<script>
import { ShowResultList } from '../../api/departmentResult.js';
import { ShowManagerList } from '../../api/departmentResult.js';
export default {
    name: 'check',
    data() { 
        return {
            pagenum: 10,
            checkData: {
                total: 10,
                currentPage: 1,
                title: '',
                checkList: ''
                // [
                //     {
                //         name: '张三',
                //         department: '前端研发部',
                //         score: ['3', '3', '3', '3', '3']
                //     },
                //     {
                //         name: '李四',
                //         department: '前端研发部',
                //         score: ['4', '4', '4', '4', '4']
                //     },
                //     {
                //         name: '王五',
                //         department: '前端研发部',
                //         score: ['5', '5', '5', '5', '5']
                //     },
                //     {
                //         name: '马六',
                //         department: '客户部',
                //         score: ['6', '6', '6', '6', '6']
                //     },
                //     {
                //         name: '赵七',
                //         department: '客户部',
                //         score: ['7', '7', '7', '7', '7']
                //     },
                //     {
                //         name: '陈八',
                //         department: '客户部',
                //         score: ['8', '8', '8', '8', '8']
                //     }

                // ]
            }
        }
    },
    created() {
        //初始化列表
        let data = this.$route.params;
          //考评委员会
        if (data.from == 'commitee') {
            this.ShowManagerListFilter(1, this.pagenum, data.year, data.qutor);
            console.log('来自考评委员会的请求成功')
        }
         //部门主管
        else {

            this.ShowResultListFilter(1, this.pagenum);
        }

    },
    methods: {
        handleCurrentChange(val) {
            let role = this.$route.params;
            //考评委员会主管翻页
            if (role.from == 'commitee') {
                this.ShowManagerListFilter(this.checkData.currentPage, this.pagenum, role.year, role.qutor);
            }
             //部门主管翻页
            else {
                this.ShowResultListFilter(this.checkData.currentPage, this.pagenum);
            }
        },
        //初始化 请求
        ShowResultListFilter(pagenum, pagesize) {
            ShowResultList({ pagenum, pagesize }).then(res => {
                let { data } = res;
                console.log(res);
                if (data) {
                    this.checkData = data.checkData;
                    this.checkData.checkList = data.checkData.checkList;
                    console.log(this.checkData);
                }
                else {
                    this.$message({
                        message: "没对应的数据！",
                        type: 'error'
                    });
                }
            });
        },
        ShowManagerListFilter(pagenum, pagesize, qutor, year) {
            ShowManagerList({ pagenum, pagesize, qutor, year }).then(res => {
                let { data } = res;
                console.log(res);
                if (data) {
                    this.checkData = data.checkData;
                    this.checkData.checkList = data.checkData.checkList;
                    console.log(this.checkData);
                }
                else {
                    this.$message({
                        message: "没对应的数据！",
                        type: 'error'
                    });
                }
            });
        },
    }
}
</script>

<style scoped>
.check {
    background: white;
}

.check h3 {
    font-size: 24px;
    text-align: center;
    margin: 20px 0;
}

.check p {
    text-align: right;
    padding: 0 20px;
}

.check p a {
    display: inline-block;
    vertical-align: top;
    line-height: 35px;
    text-align: center;
    width: 80px;
    border-radius: 3px;
    background: #20a0ff;
    color: white;
    text-decoration: none;
}

.check table {
    width: 100%;
    table-layout: fixed;
    line-height: 35px;
    border-collapse: collapse;
    margin: 20px 0;
}

.check th,
.check td {
    border: solid 1px #dadada;
    text-align: center;
}

.check td.departement {
    text-align: left;
    box-sizing: border-box;
    padding-left: 20px;
    background: #e4e8f1;
}

.check td:nth-child(even) {
    background: #f6f6f6;
}

.check th {
    background: #20a0ff;
    color: white;
}
</style>
