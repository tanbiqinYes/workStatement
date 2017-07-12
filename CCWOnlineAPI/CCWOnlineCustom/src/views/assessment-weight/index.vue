<template>
  <div class="assessmentWeight">

    <el-table :data="tableData" border style="width: 100%">
      <el-table-column label="评分类别" align="center">
        <template scope="scope">
          <span v-show="!scope.row.edit">{{ scope.row.PercentName }}</span>
          <el-input v-show="scope.row.edit" v-model="scope.row.PercentName"></el-input>
        </template>
      </el-table-column>
      <el-table-column label="分值比例" align="center">
        <template scope="scope">
          <span v-show="!scope.row.edit">{{ scope.row.PercentValue }}%</span>
          <el-input v-show="scope.row.edit" v-model="scope.row.PercentValue"></el-input>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="200" align="center">
        <template scope="scope">
          <el-button size="small" type="primary" @click="handleEdit(scope.$index, scope.row)">{{!scope.row.edit ? "编辑" : "完成"}}</el-button>
          <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

      <div class="addLine">
        <div class="addLineChild">
          <label for="">评分类别</label>
          <el-input v-model="temp.PercentName" style='width: 217px;'></el-input>
        </div>
        <div class="addLineChild">
          <label for="">分值比例</label>
          <el-input v-model="temp.PercentValue" style='width: 217px;'></el-input>
        </div>
        <el-button @click="handleAddLine()">添 加</el-button>
      </div>

      <div class="OperateBtn" >
       <el-button :loading="loadBtn2" size="large" type="primary" @click="submitForm">提 交</el-button>
       <!-- <el-button size="large" @click="handleRestLine()">重 置</el-button> -->
      </div>

  </div>
</template>

<script>
  import { GetAllData, PutConfigData } from '../../api/assessmentWeight';
  export default {
    data() {
      return {
        tableData:null,
        loadBtn:false,
        loadBtn2:false,
        temp:{
          PercentName:"",
          PercentValue:""
        },
        delData:[],
        edit:false

      }
    },

    created(){
      //do something after creating vue instance
    	this.getAllDataFilter();
    },

    methods: {

      submitForm(){
        if (this.edit){
          this.$message({
            message: "请完成编辑",
            type: 'error'
          });
          return false;
        }
        this.loadBtn2 = true;
        var total = 0;
        var params = [];
        var self =this;
        this.tableData.forEach(function (el,index) {
          total += parseFloat(el.PercentValue);
            // el.edit = el.edit ? el.edit : false;
            self.$set(el,"edit",el.edit ? el.edit : false);
          if (!el.Opera){
            el.Opera = 'no';
          }
          params.push(el);
          if (isNaN(el.PercentValue)){
            self.$message({
              message: el.PercentName+"所对应“分值比例”值不正确",
              type: 'error'
            });
            return false;
          }
        });
        if (isNaN(total)){
          this.loadBtn2 = false;
          return false;
        }
        else if (total != 100){
          this.$message({
            message: "分值比例总和不为100%",
            type: 'error'
          });
          this.loadBtn2 = false;
          return false;
        }

        if (this.delData.length > 0){
          this.delData.forEach(function (el,index) {
            params.push(el);
          });
        }

        PutConfigData(params).then(res => {
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
        });

      },

      handleRestLine(){
        this.temp.PercentName = '';
        this.temp.PercentValue = '';
      },

      handleAddLine(){
        if (this.validateValue(this.temp)){
          var PercentName = this.temp.PercentName;
          var PercentValue = parseFloat(this.temp.PercentValue);
          var Opera = 'add';
          var Id = 0;
          this.tableData.push({Id,PercentName,PercentValue,Opera});
          this.temp.PercentName = "";
          this.temp.PercentValue = "";
        }
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
        else if (value <= 0 || value > 100){
            this.$message({
              message: "所填写分值比例值范围为（大于0%小于等于100%或者填写大于0小于等于100会自动添加百分号）",
              type: 'error'
            });
            return false;
        }
        return true;

      },

      handleEdit(index, row) {
        // console.log(index, row);
        if (row.edit && this.validateValue(row)){
            if(row.Id != 0){
              row.Opera = 'update';
            }
            this.edit = !row.edit;
            this.$set(row,"edit",!row.edit);
            row.PercentValue = parseFloat(row.PercentValue);
        }
        else if(!row.edit){
          this.edit = !row.edit;
          this.$set(row,"edit",!row.edit);
        }
      },

      handleDelete(index, row) {
        // console.log(index, row);
        row.Opera = 'del';
        var id = row.Id;
        var arr = this.tableData.splice(index,1);
        if (id != 0){
          this.delData.push(arr[0]);
        }
      },

      getAllDataFilter(){
        this.loadBtn = true;

        GetAllData().then(res => {
              let { data } = res;
              if (data){
                this.tableData = data;
                this.loadBtn = false;
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
  .assessmentWeight {
    width: 100%;
    margin: 0 auto;
    max-width: 850px;
    .OperateBtn{
      text-align: center;
      padding-bottom: 30px;
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
    .addLine{
      margin-top: 30px;
      .addLineChild{
        float: left;
        margin-right: 30px;
        label{
          margin-right: 19px;
          color: rgba(50, 65, 87, 0.87);
        }

      }
    }
  }
</style>
