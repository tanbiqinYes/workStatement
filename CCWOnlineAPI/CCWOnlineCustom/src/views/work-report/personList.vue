<template>
    <div class="personList">
        <h3>{{username}}</h3>
        <div class="date_main">
            <span>时间：</span>
            <el-date-picker v-model="value1" type="month" placeholder="选择开始月份">
            </el-date-picker>
            <span>—</span>
            <el-date-picker v-model="value2" type="month" placeholder="选择结束月份">
            </el-date-picker>
            <span class="search" @click="searchKey(value1,value2)">搜索</span>
        </div>
        <table>
            <tr>
                <th>姓名</th>
                <th>部门</th>
                <th>岗位</th>
                <th>日期</th>
                <th>操作</th>
            </tr>
            <tr v-for="(list,index) in workList">
                <td>{{list.name}}</td>
                <td>{{list.department}}</td>
                <td>{{list.post}}</td>
                <td>{{list.time}}</td>
                <td>
                    <span v-if="list.operation" class='check_over' @click="checkOver(index)">查看</span>
                    <span v-else>未提交</span>
                </td>
            </tr>
        </table>
        <div class="paginationW">
            <el-pagination @current-change="handleCurrentChange" :current-page.sync="pageindex" :page-size="pagesize" layout="total, prev, pager, next, jumper" :total="total">
            </el-pagination>
        </div>
    </div>
</template>

<script>
import { GetSelfData } from '../../api/workReport.js';
export default {
    name: 'personList',
    data() {
        return {
            value1: '',
            value2: '',
            total: 0,
            pageindex: 1,
            pagesize: 10,
            username: '张三',
            workList: ''
        }
    },
    created() {
        let data = this.$route.params;
        this.username = data.name;
        //初始化列表
        this.GetSelfDataState(data.uid, "", "", 1, 10);
    },
    methods: {
        checkOver: function (index) {
            let reportid=this.workList[index].reportid;
             this.$router.push({name:'工作报告详情',params:{id:reportid}})
        },
        searchKey: function (value1, value2) {
            let startyear;
            let startmonth;
            let endyear;
            let endmonth;
            let startdate;
            let enddate;
            if (value1) {
                startyear = value1.getFullYear();
                startmonth = value1.getMonth() + 1;
                if (startmonth < 10) {
                    startmonth = '0' + startmonth;
                }
                startdate = startyear+'-' + startmonth;
            }
            else {
                startdate = value1;
            }
               if (value2) {
                endyear = value2.getFullYear();
                endmonth = value2.getMonth() + 1;
                if (endmonth < 10) {
                    endmonth = '0' + endmonth;
                }
                enddate = endyear +'-'+ endmonth;
            }
            else {
                enddate = value2;
            }
             this.GetSelfDataState(this.$route.params.uid, startdate, enddate, this.pageindex, this.pagesize)
        },
        //初始化 请求
        GetSelfDataState(accountid, startdate, enddate, pageindex, pagesize) {

            GetSelfData({ accountid, startdate, enddate, pageindex, pagesize }).then(res => {
                console.log(res);
                if (res.status == 200) {
                    this.workList = res.data.userList;
                    this.total = res.data.count;

                }
                else {
                    this.$message({
                        message: "没对应的数据！",
                        type: 'error'
                    });
                }
            })
                .catch(err => {
                    return false
                    if (err.response.status == 404) {
                        this.total = 0;
                        this.workList = '';
                        this.$message({
                            message: "没有查询到对应数据",
                            type: 'error'
                        });
                    }
                });
        },
        handleCurrentChange() {
           
        }
    }
}
</script>

<style scoped>
.personList .date_main {
    margin: 20px;
}

.personList h3 {
    margin: 10px 0;
    padding: 10px 20px;
    border-bottom: solid 1px #20a0ff;
}

.personList table {
    width: 100%;
    table-layout: fixed;
    border-collapse: collapse;
}

.personList table td,
.personList table th {
    border: solid 1px #dadada;
    box-sizing: border-box;
    padding: 0 10px;
    height: 40px;
    text-align: left;
    text-align: center;
}

.personList td span {
    display: inline-block;
    width: 60px;
    line-height: 24px;
    vertical-align: top;
}

.personList td .check_over {
    background: #20a0ff;
    border-radius: 2px;
    color: white;
    cursor: pointer
}

.personList .search {
    display: inline-block;
    width: 80px;
    text-align: center;
    vertical-align: top;
    line-height: 36px;
    background: #20a0ff;
    color: white;
    margin: 0 20px;
    border-radius: 3px;
    cursor: pointer;
}
.personList .paginationW{
    margin: 20px 0
}
</style>
