<template>
  <div class="tastManagement">
    <div v-for="(item,index) in tableData" class="quarterList" >
      <h3>
        第{{index | upperWord}}季度
      </h3>
      <el-table :data="item" border style="width: 100%">
        <el-table-column label="收件人" align="center" min-width="95">
          <template scope="scope">
            <span v-show="!scope.row.edit">{{ scope.row.roleName }}</span>
          </template>
        </el-table-column>
        <el-table-column label="考核时间" align="center" min-width="200">
          <template scope="scope">
            <el-date-picker :disabled="scope.row.Quater < nowQuarter" size="small" v-model="scope.row.startDate" type="date" placeholder="选择日期" :picker-options="pickerOptions0">
            </el-date-picker>
            <span style="margin:0 10px;">——</span>
            <el-date-picker :disabled="scope.row.Quater < nowQuarter" size="small" v-model="scope.row.endDate" type="date" placeholder="选择日期" :picker-options="pickerOptions0">
            </el-date-picker>
          </template>
        </el-table-column>
      </el-table>

    </div>

    <div class="emailContent" v-show="showDiv">
      <p>考核邮件内容：(填写date-range代表对应考核时间范围)</p>
      <el-input type="textarea" :rows="5" placeholder="请输入内容" v-model="emailContent">
      </el-input>
    </div>

    <div class="reportEmailContent" v-show="showDiv">
      <p class="">
        工作报告邮件发送时间
        <el-date-picker size="small" class="reportEmailTime" v-model="reportEmailTime" type="date" placeholder="选择日期" :picker-options="pickerOptions0">
        </el-date-picker>
      </p>
      <p>工作报告邮件内容：</p>
      <el-input type="textarea" :rows="5" placeholder="请输入内容" v-model="reportEmailContent">
      </el-input>
    </div>

      <div class="OperateBtn" v-show="showDiv">
        <!-- <el-button size="large">返 回</el-button> -->
        <el-button :loading="loadBtn2" size="large" type="primary" @click="submitForm">提 交</el-button>
      </div>

  </div>
</template>


<script>
  import { GetAllTask, PutTask } from '../../api/taskManagement';
  import _ from 'lodash';
  import { parseTime } from '../../utils';
  export default {
    data() {
      return {
        tableData:[],
        pickerOptions0: {
          // disabledDate(time) {
          //   return time.getTime() < Date.now() - 8.64e7;
          // }
        },
        loadBtn:false,
        loadBtn2:false,
        emailContent:"",
        reportEmailContent:"",
        reportEmailTime:"",
        EmailId:null,
        nowQuarter:0,
        showDiv:!1,


      }
    },

    filters:{
      upperWord(value){
        if (value == 0){
          return "一";
        }
        else if (value == 1){
          return "二";
        }
        else if (value == 2){
          return "三";
        }
        else{
          return "四";
        }
      }

    },

    created(){
      //do something after creating vue instance
    	this.GetAllTaskFilter();
    },

    methods: {

      submitForm(){

        this.loadBtn2 = true;
        var params = {};
        var self =this;
        var overFlag = false;
        params.emailContent = this.emailContent;
        params.reportemail = this.reportEmailContent;
        params.SendEmailDate = parseTime(this.reportEmailTime,'{y}-{m}-{d}');
        params.EmailId = this.EmailId;
        params.TaskData = [];

        _.each(this.tableData,function (el,index) {
          _.each(el,function(el2,inex) {

            var obj = {};
            obj.Id = el2.id;
            obj.RoleId = el2.roleId;
            obj.Quater = el2.Quater;
            obj.StartDate = parseTime(el2.startDate,'{y}-{m}-{d}');
            obj.EndDate = parseTime(el2.endDate,'{y}-{m}-{d}');
            if (el2.startDate == "" || el2.endDate == ""){
              self.$message({
                message: "第"+el2.Quater+"季度收件人 "+el2.roleName+" 请填写时间范围",
                type: 'error',
                duration: 5*1000
              });
              overFlag = true;
              return false;

            }
            if (Date.parse(new Date(obj.StartDate)) > Date.parse(new Date(obj.EndDate))){

                self.$message({
                  message: "第"+el2.Quater+"季度收件人 "+el2.roleName+" 填写的结束时间早于了开始时间",
                  type: 'error',
                  duration: 5*1000
                });
                overFlag = true;
                return false;
            }
            params.TaskData.push(obj);
          });
          if (overFlag) return false;
        });

        if (params.emailContent == ""){
           self.$message({
              message: "考核邮件内容请填写",
              type: 'error',
              duration: 5*1000
            });
            overFlag = true;
        }
        else if (this.reportEmailTime == ""){
           self.$message({
              message: "工作报告邮件发送时间请填写",
              type: 'error',
              duration: 5*1000
            });
            overFlag = true;
        }
        else if (params.reportemail == ""){
           self.$message({
              message: "工作报告邮件内容请填写",
              type: 'error',
              duration: 5*1000
            });
            overFlag = true;
        }

        if (overFlag){
          this.loadBtn2 = false;
          return false;
        }

        // console.log(params)
        PutTask(params).then(res => {
              let { data } = res;
              if (res.status == 200){
                // this.tableData = data;
                // console.log(21342)
                this.$message({
                  message: "提交成功！",
                  type: 'success'
                });
                this.loadBtn2 = false;
              }
              else{
                this.$message({
                  message: "返回数据出错！",
                  type: 'error'
                });
	    					this.loadBtn2 = false;
              }
        })
        .catch(err=>{
          // console.log(err)
            this.$message({
              message: err.response.data,
              type: 'error'
            });
          this.loadBtn2 = false;
        })

      },

      validateValue(row){

        var value = parseFloat(row.PercentValue);
        var name = row.PercentName;
        if (name == ""){
          this.$message({
            message: "所填写“评分类别”为空",
            type: 'error'
          });
          return false;
        }
        if (isNaN(value)){
          this.$message({
            message: "所填写“分值比例”值非数字",
            type: 'error'
          });
          return false;
        }
        else if (value < 0 || value > 100){
            this.$message({
              message: "所填写分值比例值范围为（0%-100%或者填写0-100会自动添加百分号）",
              type: 'error'
            });
            return false;
        }
        return true;

      },

      GetAllTaskFilter(){
        this.loadBtn = true;
        var self = this;

        GetAllTask().then(res => {
              let { data } = res;
              if (data){
                // this.tableData = data;
                if (data.isExist){
                  data.allTask.forEach(function(el,index) {
                    for (var i = 0; i < 4;i++){
                      if (el.Quater == (i+1)){
                        if (self.tableData[i]){
                          self.tableData[i].push(el);
                        }
                        else{
                          self.tableData.splice(i,1,[]);
                          self.tableData[i].push(el);
                        }
                        break;
                      }

                    }

                  });

                  this.emailContent = data.EmailContent;
                  this.reportEmailContent = data.ReportEmailContent;
                  this.reportEmailTime = data.sendEmailDate;
                  this.EmailId = data.EmailId;
                  this.nowQuarter = parseInt(new Date(data.timeNow).getMonth() / 3 + 1);
                }
                else{
                  for (var i = 0; i < 4;i++){
                    this.tableData.splice(i,1,_.cloneDeep(data.allTask))
                    this.tableData[i].forEach(function (el,index) {
                      el.Quater = i + 1;
                    });
                  }

                  this.emailContent = data.EmailContent;
                  this.reportEmailContent = data.ReportEmailContent;
                  this.reportEmailTime = data.sendEmailDate;
                  this.EmailId = data.EmailId;
                }
                this.loadBtn = false;
                if (!this.showDiv) this.showDiv = true;
              }
              else{
                this.$message({
                  message: "没此项目的数据！",
                  type: 'error'
                });
	    					this.loadBtn = false;
              }
        });
      }

    }
  }
</script>

<style lang="scss">
  .tastManagement {
    width: 100%;
    margin: 0 auto;
    max-width: 850px;
    .OperateBtn{
      text-align: center;
      padding-bottom: 80px;
      padding-top: 50px;
      .el-button+.el-button{
        margin-left: 35px;
      }
    }
    .el-table{
      td,th{
        height:45px;
      }
    }
    .el-dialog__footer{
      padding: 0px 20px 40px;
    }
    .el-form-item__label{
      padding-right: 25px;
    }
    .quarterList{
      margin-bottom: 40px;
      h3{
        text-align: center;
      }
    }
    .reportEmailContent{
      padding-top: 24px;
      .reportEmailTime{
        margin-left: 15px;
      }
    }
  }
</style>
