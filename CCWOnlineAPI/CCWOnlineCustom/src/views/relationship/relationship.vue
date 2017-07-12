<template>
<div class="relationShipWrapper">
    <div class="search">
        <el-input v-model="searchMsg" placeholder="请输入搜索内容" style="width:200px;margin-right:10px;" @keyup.enter.native="search"></el-input>
        <el-button type="primary" :loading="loadFlag" @click="search" icon="search">搜索</el-button>
    </div>

    <el-table :data="list.staffs.imfo" border style="width: 100%">
        <el-table-column label="姓名" min-width="100" align="center">
          <template scope="scope">
            <span>{{ scope.row.name }}</span>
          </template>
        </el-table-column>
        <el-table-column label="部门" align="center" min-width="120">
          <template scope="scope">
            <span>{{ scope.row.dep }}</span>
          </template>
        </el-table-column>
        <el-table-column label="角色" align="center" min-width="120">
          <template scope="scope">
            <span>{{ scope.row.jop }}</span>
          </template>
        </el-table-column>
        <el-table-column label="状态" align="center" min-width="80">
          <template scope="scope">
            <span>{{ scope.row.status }}</span>
          </template>
        </el-table-column>-->
        <el-table-column label="操作" align="center" width="200">
          <template scope="scope">
            <el-button :key="scope.row.userid" :loading="!!scope.row.show" type="primary" size="small" @click="editOperate('operate',scope.row,scope.$index)">查看</el-button>
          </template>
        </el-table-column>
    </el-table>

    <div class="paginationW">
      <el-pagination @current-change="handleCurrentChange" :current-page="currentPage" :page-size="pageNum" layout="total, prev, pager, next, jumper" :total="totalPage">
      </el-pagination>
    </div>

    <el-dialog title="查看" :visible.sync="popshow">
      <el-form class="small-space" label-position="right" label-width="100px" style='width: 400px; margin:0 auto;'>
          <!-- <div class="rowContainer">
              <span>姓&nbsp;&nbsp;名：</span>
              <el-input placeholder="请输入内容" v-model="name" :disabled="true" style="width:192px;">
              </el-input>
          </div> -->

          <el-form-item label="姓名">
            <el-input v-model="name" :disabled="true" style='width: 217px;'></el-input>
          </el-form-item>

          <el-form-item label="部门">
            <el-input v-model="dep" :disabled="true" style='width: 217px;'></el-input>
          </el-form-item>

          <el-form-item label="岗位">
            <el-input v-model="jobs" :disabled="true" style='width: 217px;'></el-input>
          </el-form-item>

          <el-form-item label="上级">
            <el-autocomplete class="inline-input" v-model="chooseSup" :fetch-suggestions="querySearch" placeholder="请输入内容" :trigger-on-focus="true" @select="handleSelectSup" style='width: 217px;'
            >
            </el-autocomplete>
          </el-form-item>

            <el-form-item :label="'同事'+(tindex+1)" v-for="(elem,tindex) in this.chooseMate2" :key="elem">
              <el-autocomplete class="inline-input" v-model="chooseMate[tindex]" :fetch-suggestions="querySearchOne" placeholder="请输入内容" :trigger-on-focus="true" @select="handleSelect($event,tindex,elem)" style='width: 217px;'
              >
              </el-autocomplete>
              <el-button v-if="tindex == chooseMate.length - 1" type="primary" size="mini" icon="plus" @click="addMyMate(tindex)"></el-button>
              <el-button v-if="chooseMate.length != 1" type="danger" size="mini" icon="minus" @click="removeMyMate(tindex)"></el-button>
            </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer" style="text-align:center">
        <el-button type="primary" size="large" @click="closePop">确定</el-button>
      </div>

    </el-dialog>


</div>
</template>

<script>
import { getUserRelation,getUserChoosed,relationSubmit,getUserManager,getUserWorkmate } from '../../api/relationShip';

import _ from "lodash";

export default {
    data(){
        return{
            name:'',
            dep:'',
            jobs:'',
            loadFlag:false,
            totalPage:0,
            currentPage: 1,
            pageNum: 10,
            keyWord:"",
            searchMsg:'',
            searchMsgSuccess:'',
            submitMsg:[],
            popshow:false,
            seePopBtn:false,
            chooseSup:'',
            chooseSupId:'',
            chooseMate:[],
            chooseMate2:[],
            chooseId:['','',''],
            choose:[],
            index:'',
            manager:[],
            workmate:[],
            totalPage:0,
            tableHead:{
                imfo:{name:'姓名',dep:'部门',job:'岗位',operate:'操作'}
            },
            list:{
                staffs:{
                    imfo:[

                    ]
                }
            }
        }
    },
    created(){
        let userid = 'e6444210-f0a0-466c-a7a1-ccc4620af262';
        this.getUserListFilter(1, this.pageNum, "", "");
        this.getUserWorkmateFilter();
    },

    filters:{
      filterUndefined(value){
        if (value == undefined) return "";
        else return value;
      },
    },

    methods:{

        removeMyMate(index){
          // console.log(index);
          this.chooseMate.splice(index,1);
          this.chooseMate2.splice(index,1);
        },

        addMyMate(index){
          // console.log(index)
          if (index == 4){
            this.$message({
              message: "最多添加5个同事！",
              type: 'error'
            });
            return false;
          }
          this.chooseMate.push("");
          this.chooseMate2.push("");
        },
        //
        // updateValue(val,index,ele){
        //   // console.log(val)
        //     // console.log(this.$refs['input0'].value)
        //   // console.log(index)
        //   // this.$nextTick(function () {
        //   //   // val.focus();
        //   //   console.log(2)
        //   // })
        //   this.chooseMate2.splice(index,1,val.target.value)
        //   this.chooseMate[index] = val
        //   // ele = val;
        // },

        handleSelect(val,index) {
        // console.log(val)
        // console.log(index)
            this.chooseMate.splice(index,1,val.value)
            this.chooseMate2.splice(index,1,val.value)
            this.chooseId[index] = val.id;
        },

        // 下拉框操作
        querySearch(queryString, cb) {
            // 调用远程上级接口

            var restaurants = this.manager;
            var results = queryString ? restaurants.filter(this.createFilter(queryString)) : restaurants;
            // 调用 callback 返回建议列表的数据
            cb(results);
        },
        querySearchOne(queryString, cb) {
            // 调用远程同事接口
            var restaurants = this.workmate;
            var results = queryString ? restaurants.filter(this.createFilter(queryString)) : restaurants;
            cb(results);
        },
        createFilter(queryString) {
            return (restaurant) => {
                return (restaurant.value.indexOf(queryString) === 0);
            };
        },
        handleSelectSup(item) {
            if (this.choose[0]){
              this.choose[0].id  = item.id;
              this.choose[0].name  = item.value;
            }
            else{
              this.choose.splice(0,1,{id:item.id,name:item.value})
            }
            // console.log(item)
        },

        isSelectUndefined(index){
          if (index == 0) return 0;
          if (this.chooseMate[index] == "" || this.chooseMate[index] == undefined){
            var aa = this.isSelectUndefined(index - 1);
            return aa;
          }
          else{
            // console.log(index + 1)
            return index;
          }

        },

        handleSelect0(item) {
            this.chooseId[0] = item.id;
            // console.log(item)
        },
        handleSelect1(item) {
            this.chooseId[this.isSelectUndefined(1)] = item.id;
            ;
            // console.log(item)
        },
        handleSelect2(item) {
            this.chooseId[this.isSelectUndefined(2)] = item.id;
            // console.log(item)
        },
        handleSelect3(item) {
            this.chooseId[this.isSelectUndefined(3)] = item.id;
            // console.log(item)
        },
        handleSelect4(item) {
            this.chooseId[this.isSelectUndefined(4)] = item.id;
            // console.log(item)
        },
        handleCurrentChange(val) {
            // console.log(`当前页: ${val}`);
            this.getUserListFilter(val, this.pageNum, this.searchMsgSuccess, "",true);
        },
        // 搜索操作
        search() {
            let str = this.searchMsg;
            this.currentPage = 1;
            this.loadFlag = true;
            // console.log(str);
            this.getUserListFilter(1, this.pageNum, str, "");
        },

        // 编辑操作
        editOperate(key,obj,index){
            if(key == 'operate'){
                // 请求ajax 调用已选择接口 获取choose[]
                // console.log(obj)
                // console.log(index)
                let userid = obj.userid;
                this.getUserChoosedFilter(userid,obj);
                this.index = index;
                this.name = obj.name;
                this.dep = obj.dep;
                this.jobs = obj.jop;
                // obj.show = true;
                this.$set(obj,"show",true)
                // this.popshow = true;
            }else{
                return '';
            }
        },

        isCommonData(arr){
          var flag = false;
          arr.forEach(function(el,index) {
            var po = _.indexOf(arr,el,index+1);
            if (po != -1 && po != index){
              flag = true;
              return false;
            }
          });

          return flag;
        },

        isExitMan(users,man){
          var self = this;
          var flag = true;

          _.each(users,function(el,index) {
            if (el.value == man){
              self.chooseSupId = el.id;
              flag = false;
              return false;
            }
          });

          return flag;
        },

        isMateMan(arr){
          var flag = false;
          var tman = "";
          var errF = true;
          var self =this;

          // alert('同事不存在');
          _.each(arr,function(el,index) {
            if (el == self.name){
              flag = true;
              errF = false;
              self.$message({
                  message: "同事不能选择自己",
                  type: 'error'
                });
              return false;
            }
            var num = _.findIndex(self.workmate, function(chr) {
              return chr.value == el;
            });
            if (num == -1) {
                var nextV = arr[index+1];
                if ((el == "" || el == undefined) && (nextV != "" && nextV != undefined)){
                  tman = index + 1;
                  flag = true;
                  errF = false;
                    self.$message({
                      message: "同事"+tman+"需要填写",
                      type: 'error'
                    });
                  return false;
                }
                else if (el != "" && el != undefined){
                  tman = el;
                  flag = true;
                  return false;
                }
            }

          });
          if (flag && errF){
            this.$message({
              message: tman+"同事不存在",
              type: 'error'
            });
          }

          return flag;
        },

        // 点击确定操作
        closePop(){
            let relationChoosed = [];
            const self = this;
            for(let i = 0;i<this.chooseMate.length;i++){
                if(this.chooseMate[i] !== '' && this.chooseMate[i] !== undefined){
                    relationChoosed.push(this.chooseMate[i])
                }else{

                }
            }

            if(Boolean(this.chooseSup) == false){
                  this.$message({
                    message: "直接上级未选择",
                    type: 'error'
                  });
                return false;
            }
            else if(this.chooseSup == this.name){
                  this.$message({
                    message: "上级不能选择自己",
                    type: 'error'
                  });
                return false;
            }else if(this.isExitMan(this.manager,this.chooseSup)){
                this.$message({
                  message: "所填上级不存在",
                  type: 'error'
                });
                return false;
            }else if(relationChoosed.length<1){
                this.$message({
                  message: "同事数量不能小于1个",
                  type: 'error'
                });
                return false;
            }else if(this.isCommonData(relationChoosed)){
                this.$message({
                  message: "两名同事不能为同一个人",
                  type: 'error'
                });
                return false;
            }else if(relationChoosed.indexOf(this.chooseSup) != -1){
                this.$message({
                  message: "上级和同事不能为同一人",
                  type: 'error'
                });
                return false;
            }else if(this.isMateMan(this.chooseMate)){
                return false;
            }else{
                relationChoosed.unshift(this.chooseSup);
                // 将relationChoosed提交
                // this.list.staffs.imfo[this.index].choose = relationChoosed;

                // console.log(this.chooseId);
                let submitArr = [];
                let chooseSupId = [];

                for(let i = 0;i<this.chooseMate.length;i++){
                    if(this.chooseId[i] == '' || this.chooseId[i] == undefined){
                        var realId =  0;
                        _.each(this.workmate,function(el,index) {
                          if (el.value == self.chooseMate[i]){
                            submitArr.push(el.id)
                            return false;
                          }
                        });
                    }else{
                        submitArr.push(this.chooseId[i])
                    }
                }
                // console.log(submitArr);
                // let userid = this.$store.getters.uid;
                console.log(this.list.staffs.imfo[this.index]);
                if(this.list.staffs.imfo[this.index].status == '未完成'){
                    this.list.staffs.imfo[this.index].status = '已完成';
                }
                let UserId = this.list.staffs.imfo[this.index].userid;
                let RelationId = this.chooseSupId;
                let Colleague = submitArr;
                // console.log(submitArr)
                let params = {UserId,RelationId,Colleague}
                this.relationSubmitFilter(params);
                this.popshow = false;
            }
            this.chooseSup='';
            this.chooseMate = [];
        },

        // 点击背景操作
        closePopup(e){
            if(e.target.className == 'wrapper'){
                this.popshow = false;
                this.chooseSup='';
                this.chooseMate = [];
            }

        },

        //
        getUserListFilter(pagenum,pagesize,keyword,orderby,noreplace){
            this.loadBtn = true;
            pagenum = pagenum ? pagenum : 0;
            pagesize = pagesize ? pagesize : 10;
            keyword = keyword ? keyword : "";
            orderby = orderby ? orderby : "";
            const params = {pagenum,pagesize,keyword,orderby};
            getUserRelation(params).then(res=>{
                let {data} = res;
                // console.log(data.total);
                this.list.staffs.imfo = data.list.staffs.imfo;
                this.totalPage = data.total;
                this.loadFlag = false;
                if (!noreplace) this.searchMsgSuccess = this.searchMsg;
            })
            .catch(err=>{
              if (err.response.status == 404){
                this.$message({
                  message: "没有查询到数据",
                  type: 'error'
                });
              }
              this.loadFlag = false;
            });
        },
        getUserChoosedFilter(userid,obj){
            getUserChoosed({userid}).then(res =>{
                let {data} = res;
                // console.log(_.isEmpty(data.choose))
                // console.log(data.choose)
                console.log(data);
                this.manager = data.manage;
                this.chooseMate = [""];
                this.chooseMate2 = [""];
                if(data.choose == '' || _.isEmpty(data.choose)){
                    this.choose = data.choose;
                    this.chooseSup = '';
                    // console.log(12324)
                    this.chooseMate = [""];
                    this.chooseMate2 = [""];
                    this.chooseId = [];
                }else{
                    this.choose = data.choose;
                    this.chooseSup = data.choose[0].name;
                    for(let i=1;i<data.choose.length;i++){
                        this.chooseMate[i-1] = data.choose[i].name;
                        this.chooseMate2[i-1] = data.choose[i].name;
                    }
                    if (data.choose.length < 2){
                      this.chooseMate = [""];
                      this.chooseMate2 = [""];
                    }
                    this.chooseId = [];
                }
              this.popshow = true;
              obj.show = false;
            })
        },
        // getUserManagerFilter(){
        //     getUserManager().then(res =>{
        //         let {data} = res;
        //         this.manager = data;
        //     })
        // },
        getUserWorkmateFilter(){
            getUserWorkmate().then(res =>{
                let {data} = res;
                this.workmate = data;
            })
        },
        relationSubmitFilter(params){
            relationSubmit(params).then(res =>{
              if (res.status == 200){
                this.$message({
                  message: "确认成功！",
                  type: 'success'
                });
              }
            })
        }
    }
}
</script>

<style scoped>
.search{
    /*float: right;*/
    padding-bottom: 30px;
    padding-top: 18px;
}
.search input{
    /*height: 28px;*/
    box-sizing: border-box;
    margin: 0;
    padding: 0;
    margin-right: 10px;
}
.search button{
    /*height: 28px;*/
}
table{
    border-collapse:collapse;
    width: 100%;
    border-color: #dfe6ec;
}
tbody,thead{
    border-color: #dfe6ec;
}
td,th{
    height: 40px;
    border-color: #dfe6ec;
    height: 45px;
    font-size: 14px;
}
td{
    text-align: center;
}
th{
    background-color: #eef1f6;
}
.operateClass{
    padding: 3px 8px;
    border-radius: 5px;
    background: #199bd7;
    color: #fff;
    cursor: pointer;

}
.viweClass{
    padding: 3px 8px;
    border-radius: 5px;
    color: #000;
    cursor: pointer;
    border: 1px solid #000;
    box-sizing: border-box;
}
.wrapper{
    position: fixed;
    top: 0;
    left: 0;
    height: 100%;
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    background: rgba(17, 17, 17, 0.7);
    z-index: 1000;
}
.btnContainer{
    text-align: center;
    margin-top: 10px;
}
.inputContainer{

}
.rowContainer{
    margin-bottom: 10px;
}
.rowContainer span{
    color: #fff;
    margin-right: 10px;
}
.splitPage{
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 20px;
}
.paginationW{
    padding: 30px 0px 50px;
}
</style>

<style lang="scss">
.relationShipWrapper{
  .el-dialog__body{
    padding: 20px 20px;
  }
  .el-dialog__footer{
    padding: 0px 20px 40px;
  }
  .el-form-item__label{
    padding-right: 25px;
  }
  .el-button+.el-button{
    margin-left: 0px;
  }

}
</style>
