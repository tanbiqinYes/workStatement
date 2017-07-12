<template>
  <div class="report">
    <table>
      <tr>
        <td colspan="2">时间：{{userInfo.UserTime}}</td>
        <td colspan="2">工作总结类型：{{userInfo.UserType}}</td>
      </tr>
      <tr>
        <td>姓名：{{userInfo.UserName}}</td>
        <td>岗位：{{userInfo.UserPost}}</td>
        <td>职位：{{userInfo.UserJob}}</td>
        <td>部门：{{userInfo.UserDepartment}}</td>
      </tr>
    </table>
    <ul>
      <li v-for="list in questionInfo">
        <h3>{{list.title}}</h3>
        <el-input v-if="list.edite" type="textarea" :autosize="{ minRows: 5, maxRows: 200}" placeholder="请输入内容" v-model="list.text"></el-input>
        <div v-else class="textshow">
          {{list.text}}
        </div>
      </li>
    </ul>
    <div class="button">
      <span v-if="isEdite" @click="submit()">确认</span>
      <span @click="returnToDoList()">返回</span>
    </div>
  </div>
</template>

<script>
import { GetReportData } from '../../api/workReport.js';
import { PutReportData } from '../../api/workReport.js';
export default {
  name: 'report',
  data() {
    return {
      userInfo: {},
      questionInfo: 
      [
        // {
        //   edite: true,
        //   text: "",
        //   title: "Q1"
        // },
        // {
        //   edite: true,
        //   text: "",
        //   title: "Q2"
        // },
        // {
        //   edite: true,
        //   text: "",
        //   title: "Q3"
        // }
      ],
      // isEdite: false,
      isEdite: true,
      reportId: '',
      upload: false,
      action:[]  //记录题目是否包含‘不填’
    }
  },
  created() {
    let reportid = this.$route.params.id;
    this.reportId = reportid;
    //初始化列表
    this.GetReportDataFilter(reportid);  
  },
  methods: {
    testText(){
      for(let i=0;i<this.questionInfo.length;i++){
        let result=false;
        let info=this.questionInfo[i].title;
        if(info.indexOf('不填')>-1){
          result=true;
        }
        this.action.push(result);
      }
      console.log(this.action)
    },
    //返回
    returnToDoList() {
      this.$router.push({ path: '/todo/index' })
    },
    //提交
    submit() {
      if (this.upload) {
        return false
      }
      let Id = this.reportId;
      let AccountId = this.userInfo.AccountId;
      let AnswerInfo = this.questionInfo;
      for (let i = 0; i < AnswerInfo.length; i++) {
        let text = AnswerInfo[i].text;
        text = text.replace(/\s/g, '');
        if(this.action[i]){
          continue
        }
        else if (text == "\n" || text == "") {
          this.$message({
            message: '请回答第' + (i + 1) + '个问题！',
            type: 'warning'
          })
          return false
        }
      }
      this.upload = true;
      this.PutReportDataWay({ Id, AccountId, AnswerInfo })
    },
    //初始化 请求
    GetReportDataFilter(id) {
      GetReportData({ reportid: id }).then(res => {
        console.log(res)
        if (res.status == 200) {
          this.userInfo = res.data.UserInfoOut;
          this.questionInfo = res.data.QuestionInfoOut;
          this.isEdite = this.questionInfo[0].edite;
          console.log(res.data.QuestionInfoOut)
             this.testText()
        }
        else {
          this.$message({
            message: "没对应的数据！",
            type: 'error'
          });
        }
      })
        .catch(err => {
          this.$message({
            message: "暂时还没有工作报告试卷信息！",
            type: 'error'
          });
          this.$router.push({ path: '/todo/index' })
        });
    },
    PutReportDataWay(reportid, userid, data) {
      PutReportData(reportid, userid, data).then(res => {
        console.log(res.data)
        if (res.status == 200) {
          this.$message({
            message: '提交成功',
            type: 'success'
          });
          this.$router.push({ path: '/todo/index' })
        }
        else {
          this.$message({
            message: "提交出错",
            type: 'error'
          });
        }
      });
    }
  }
}
</script>

<style scoped>
.button {
  text-align: right;
  margin: 20px 0;
}

.textshow{
  box-sizing: border-box;
  padding: 5px;
  border: solid 1px #dadada;
  min-height: 100px;
  line-height: 24px;
  border-radius: 2px;
}

.button span {
  display: inline-block;
  line-height: 35px;
  width: 80px;
  background: #20a0ff;
  color: white;
  margin: 0 20px;
  vertical-align: top;
  text-align: center;
  border-radius: 3px;
  cursor: pointer;
}

.report table {
  width: 100%;
  table-layout: fixed;
  border-collapse: collapse;
}

.report table td {
  border: solid 1px #dadada;
  box-sizing: border-box;
  padding: 0 10px;
  height: 40px;
  text-align: left;
}

ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
}

li>h3 {
  margin: 0;
  padding: 10px;
  background: #f4f4f4
}
</style>
