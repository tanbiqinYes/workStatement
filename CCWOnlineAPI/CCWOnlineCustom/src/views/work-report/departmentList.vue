<template>
    <div class="manager">
        <div class="date_main">
            <span>时间：</span>
            <el-date-picker v-model="datetime" type="month" placeholder="选择时间">
            </el-date-picker>
            <span>姓名：</span>
            <el-input class="search_name" v-model="username" placeholder="请输入内容"></el-input>
            <span class="search" @click="searchKey()">搜索</span>
        </div>
        <table>
            <thead>
                <tr>
                    <td>姓名</td>
                    <td>部门</td>
                    <td>岗位</td>
                    <td>日期</td>
                    <td>操作</td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(list,index) in userList">
                    <td>{{list.name}}</td>
                    <td>{{list.department}}</td>
                    <td>{{list.post}}</td>
                    <td>{{list.time}}</td>
                    <td>
                        <span :class="list.operation?'look_report':''" v-text="list.operation?'查看':'未提交'" @click="lookReport(index)"></span>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="paginationW">
          <el-pagination @current-change="handleCurrentChange" :current-page.sync="pageIndex" :page-size="pageSize" layout="total, prev, pager, next, jumper" :total="count">
          </el-pagination>
        </div>
    </div>
</template>

<script>

import { GetDepartmentData, GetReportData } from '../../api/workReport.js';

export default {
    name: 'manager',
    data() {
        return {
            count: 0,
            pageIndex: 1,
            pageSize:10,
            currentPage: 1,
            datetime:'',
            username:'',
            userList: ''
        }
    },
    created() {
        //初始化列表
        this.GetDepartmentDataFilter(this.pageIndex, this.pageSize, this.datetime, this.username);
    },
    methods: {
        handleCurrentChange(val) {
            this.GetDepartmentDataFilter(this.pageIndex, this.pageSize, this.datetime, this.username);
        },
        //初始化 请求
        GetDepartmentDataFilter(pageIndex, pageSize, datetime, username) {
            var datetime = this.datetime;
            var username = this.username;
            if(this.datetime) {
              let year = this.datetime.getFullYear();  
              let month = this.datetime.getMonth()+1; 
              if(month<10){
                month='0'+month;
              } 
              datetime=year+'-'+month;
            }
            pageIndex = pageIndex ? pageIndex : 0;
            pageSize = pageSize ? pageSize : 10;
            GetDepartmentData({ pageIndex, pageSize, datetime, username }).then(res => {
                let { data } = res;
                console.log(res);
                if (data) {
                    console.log(data);
                    this.count = data.count;
                    this.userList = data.userList;
                }
                else {
                    console.log("没有查询到对应数据");
                    this.userList='';
                    this.$message({
                        message: "没有查询到对应数据",
                        type: 'error'
                    });
                }
            })
            .catch(err=>{
              if (err.response.status == 404){
                   this.count = 0;
                   this.userList='';
                this.$message({
                  message: "没有查询到对应数据",
                  type: 'error'
                });
              }
            });
        },
        //查看
        lookReport: function (index) {
          console.log(this.userList[index].reportid);
          var reportid = this.userList[index].reportid;
          if (this.userList[index].operation) {
            this.$router.push({name:'工作报告详情',params:{id:reportid}})
          }
          else {
            this.$message('未提交，无法查看')
          }

        },
        //搜索
        searchKey: function () {
          this.GetDepartmentDataFilter(1, this.pageSize, this.datetime, this.username);
        }
    }
}
</script>

<style>
.search_name {
    width: 193px;
}

.date_main {
  margin-bottom: 20px;
}

.manager .search {
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

.manager table {
    width: 100%;
    table-layout: fixed;
    border-collapse: collapse;
    text-align: center;
    margin: 20px 0
}

.manager table td {
    height: 40px;
    border: solid 1px #dadada;
}

.manager td span {
    display: inline-block;
    width: 60px;
    line-height: 24px;
    vertical-align: top;
}

.look_report {
    background: #20a0ff;
    color: white;
    border-radius: 3px;
    cursor: pointer;
}
</style>
