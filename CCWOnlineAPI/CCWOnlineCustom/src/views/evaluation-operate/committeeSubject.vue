<template>
<div>
    <table border="1" bordercolor='#dfe6ec' class="titletable" v-show="showtable">
        <tr class="titletr">
            <th width="120">姓名</th>
            <th>职业修养考核得分</th>
            <th>职业能力考核得分</th>
            <th>综合职业考核得分</th>
            <th>综合评定等级</th>
            <th>考核系数</th>
        </tr>
        <tr>
            <!--<td>{{exam.score.name}}</td>
            <td>{{exam.score.score[0]}}</td>
            <td>{{exam.score.score[1]}}</td>
            <td>{{exam.score.score[2]}}</td>
            <td>{{exam.score.score[3]}}</td>
            <td>{{exam.score.score[4]}}</td>-->
        </tr>
    </table>
    <div>
        <h1>{{exam.title}}</h1>
        <div v-for="list in exam.shijuan">
            
            <div v-if="list.examinfo.questiontype =='职业修养'">
                <h2>{{list.examinfo.reporttitle}}</h2>
                <div v-for="(timu,$index) in list.examinfo.questioninfo" style="margin-bottom:20px;">
                    <h4>{{$index+1}}.{{timu.questionname}}</h4>
                    <table border="1" bordercolor='#dfe6ec' ref="radioa">
                        <thead>
                            <tr>
                                <th v-for="thead in timu.accountlist">{{thead.value}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="option in timu.qilist">
                                <td class="fontleft">{{option.questionitemname}}</td>
                                <td v-for="show in option.answer">
                                    <i v-show="show.value" class="el-icon-check"></i>
                                </td>
                                <td v-show="exam.type == '4'?false:true">
                                    <!--<input type="radio" :value="option.id" v-model="timu.check">-->
                                    <el-radio class="radio changeRadio" v-model="timu.check" :label="option.id"></el-radio>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div v-else>
                <h2>{{list.examinfo.reporttitle}}</h2>
                <div v-for="leixing in list.examinfo.questioninfo">
                    <h3>{{leixing.questionname}}</h3>
                    <div v-for="(timu,$index) in leixing.qdlist" style="margin-bottom:20px;">
                        <h4>{{$index+1}}.{{timu.questionname}}</h4>
                        <table bordercolor='#dfe6ec' border="1" ref="radioa">
                            <thead>
                                <tr>
                                    <th v-for="thead in timu.accountlist">{{thead.value}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="option in timu.qilist">
                                    <td class="fontleft">{{option.questionitemname}}</td>
                                    <td v-for="show in option.answer">
                                        <i v-show="show.value" class="el-icon-check"></i>
                                    </td>
                                    <td v-show="exam.type == '4'?false:true">
                                        <!--<input type="radio" :value="option.id" v-model="timu.check">-->
                                        <el-radio class="radio changeRadio" v-model="timu.check" :label="option.id"></el-radio>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>  
        </div>
    </div>

    
    <h4 v-show="showself">员工自我评价</h4>
    <!--<p class="selfEvaluation">{{selfEvaluation}}</p>-->
    <el-input :disabled="selfdisable" v-show="showself" type="textarea" :autosize="{ minRows: 2}" placeholder="请输入内容" v-model="selfEvaluation"></el-input>

    <h4 v-show="showhead">部门负责人评语</h4>
    <!--<p class="selfEvaluation">{{headEvaluation}}</p>-->
    <el-input :disabled="headdisable" v-show="showhead" type="textarea" :autosize="{ minRows: 2}" placeholder="请输入内容" v-model="headEvaluation"></el-input>
    <h4 v-show="showcommittee">考评委员会评语</h4>
    <!--<textarea v-model="committeeEvaluation"></textarea>-->
    <el-input v-show="showcommittee" type="textarea" :autosize="{ minRows: 4}" placeholder="请输入内容" v-model="committeeEvaluation"></el-input>

    <div style="display: flex;justify-content: center;margin:15px 0;" v-show="showsubmit">
        <el-button type="info" @click="total()" :disabled="submitable">提交</el-button>
    </div>

    <div style="display: flex;justify-content: center;margin:15px 0;" v-show="goback">
        <el-button type="info" @click="goback()">返回</el-button>
    </div>
</div>
</template>

<script>
import {getUserSubject} from '../../api/subject';

import {Submit} from '../../api/subject';
export default {
    data(){
        return{
            // 控制文本框显隐
            showself:false,
            showhead:false,
            showcommittee:false,
            showtable:false,

            // 控制文本框disable
            selfdisable:true,
            headdisable:true,
            submitable:false,
            // 控制提交按钮
            showsubmit:false,

            // 控制返回按钮
            goback:false,

            // 打分提交数组
            showerror:false,
            totalScore:[],
            
            // 个人自我评价文字
            selfEvaluation:'',
            // 主管评价
            headEvaluation:'',
            // 考评委员会评价
            committeeEvaluation:'',
            // 题目列表
            exam: {}
        }
    },
    created(){
        let uid = this.$route.params.uid;
        this.getUserSubjectFilter(uid);
    },
    methods:{
        goback(){
            this.$router.go(-1);
        },
        total(){

            var timu = this.exam.shijuan[0].examinfo.questioninfo;
            console.log(timu);
            // for(let i = 0;i<this.lists.length;i++){
            //     this.totalScore.push(this.lists[i].picked)
            // }

            // 根据提供回来的check 改变验证。
            outermost:
            for(let i=0;i<this.exam.shijuan.length;i++){
                if (this.exam.shijuan[i].examinfo.questiontype == '职业修养') {
                    for (let j = 0; j < this.exam.shijuan[i].examinfo.questioninfo.length; j++) {
                        if (this.exam.shijuan[i].examinfo.questioninfo[j].check == 0) {
                            this.exam.shijuan[i].examinfo.questioninfo[j].showErr = true;
                            this.$alert('请完成《' + (this.exam.shijuan[i].examinfo.reporttitle) + '》下的《'+(this.exam.shijuan[i].examinfo.questioninfo[j].questionname)+'》中的'+'》第（' + (j + 1) + '）个题目', '警告', {
                                confirmButtonText: '确定',
                                callback: action => {
                                    // this.$message({
                                    //     type: 'info',
                                    //     message: `action: ${action}`
                                    // });
                                }
                            });
                            break outermost;
                        }else{
                            this.totalScore.push(this.exam.shijuan[i].examinfo.questioninfo[j].check)
                        }
                    }
                }else{
                    for (let j = 0; j < this.exam.shijuan[i].examinfo.questioninfo.length; j++) {
                        for (let k = 0; k < this.exam.shijuan[i].examinfo.questioninfo[j].qdlist.length;k++){
                            if (this.exam.shijuan[i].examinfo.questioninfo[j].qdlist[k].check == 0) {
                                this.exam.shijuan[i].examinfo.questioninfo[j].qdlist[k].showErr = true;
                                this.$alert('请完成《' + (this.exam.shijuan[i].examinfo.reporttitle) + '》下的《'+(this.exam.shijuan[i].examinfo.questioninfo[j].questionname)+'》中的《'+(this.exam.shijuan[i].examinfo.questioninfo[j].qdlist[k].questionname)+'》第（' + (k + 1) + '）个题目', '警告', {
                                    confirmButtonText: '确定',
                                    callback: action => {
                                        // this.$message({
                                        //     type: 'info',
                                        //     message: `action: ${action}`
                                        // });
                                    }
                                });
                                break outermost;
                            }else{
                                this.totalScore.push(this.exam.shijuan[i].examinfo.questioninfo[j].qdlist[k].check)
                            }
                        }
                    }
                }              
            }
            let lengtha = this.$refs.radioa.length;
            let lengthb = this.totalScore.length;
            // var lengtha = parseInt(this.$refs.radioa.length)+parseInt(this.$refs.radiob.length);
            if(lengthb == lengtha){
                if(this.exam.type == 0){
                    // 自评判断selfEvaluation
                    if (this.selfEvaluation == '') {
                        this.$alert('请完成自我评价', '警告', {
                            confirmButtonText: '确定',
                            callback: action => {
                                // this.$message({
                                //     type: 'info',
                                //     message: `action: ${action}`
                                // });
                            }
                        });
                        return false;
                    } else {
                        let examdata = this.totalScore;
                        let otheraccountid = this.$route.params.uid;
                        let evaluation = {
                            "selfvalue": this.selfEvaluation,
                            "highvalue": this.headEvaluation,
                            "committeevalue": this.committeeEvaluation
                        }
                        let params = {
                            "examdata": examdata,
                            "otheraccountid": otheraccountid,
                            "evaluation": evaluation
                        }
                        this.SubmitFilter(params);
                    }
                }else if(this.exam.type == 1){
                    let examdata = this.totalScore;
                    let otheraccountid = this.$route.params.uid;
                    let evaluation = {
                        "selfvalue": this.selfEvaluation,
                        "highvalue": this.headEvaluation,
                        "committeevalue": this.committeeEvaluation
                    }
                    let params = {
                        "examdata": examdata,
                        "otheraccountid": otheraccountid,
                        "evaluation": evaluation
                    }
                    this.SubmitFilter(params);
                }else if(this.exam.type == 2){
                    // 判断主管评价headEvaluation
                    if (this.headEvaluation == '') {
                        this.$alert('请完成部门负责人评语', '警告', {
                            confirmButtonText: '确定',
                            callback: action => {
                                // this.$message({
                                //     type: 'info',
                                //     message: `action: ${action}`
                                // });
                            }
                        });
                        return false;
                    } else {
                        let examdata = this.totalScore;
                        let otheraccountid = this.$route.params.uid;
                        let evaluation = {
                            "selfvalue": this.selfEvaluation,
                            "highvalue": this.headEvaluation,
                            "committeevalue": this.committeeEvaluation
                        }
                        let params = {
                            "examdata": examdata,
                            "otheraccountid": otheraccountid,
                            "evaluation": evaluation
                        }
                        this.SubmitFilter(params);
                    }
                }else if(this.exam.type == 3){
                    if (this.committeeEvaluation == '') {
                        this.$alert('请完成考评委员会评语', '警告', {
                            confirmButtonText: '确定',
                            callback: action => {
                                // this.$message({
                                //     type: 'info',
                                //     message: `action: ${action}`
                                // });
                            }
                        });
                        return false;
                    } else {
                        let examdata = this.totalScore;
                        let otheraccountid = this.$route.params.uid;
                        let evaluation = {
                            "selfvalue": this.selfEvaluation,
                            "highvalue": this.headEvaluation,
                            "committeevalue": this.committeeEvaluation
                        }
                        let params = {
                            "examdata": examdata,
                            "otheraccountid": otheraccountid,
                            "evaluation": evaluation
                        }
                        this.SubmitFilter(params);
                    }
                }

                
            }
        },
        getUserSubjectFilter(userid){
            let a = {otheraccountid:userid}
            getUserSubject(a).then(res =>{
                let {data} =res;
                console.log(data);
                this.exam = data.exam;
                this.showtable = true;
                if(data.exam.type == 0){
                    this.showsubmit = true;
                    this.showself = true;
                    this.selfdisable = false;  
                }else if(data.exam.type == 1){
                    this.showsubmit = true;
                    this.showself = false;
                }else if(data.exam.type == 2){
                    this.showsubmit = true;
                    this.showself = true;
                    this.selfdisable = true;
                    this.showhead = true;
                    this.headdisable = false;
                    this.selfEvaluation = this.exam.evaluation.selfvalue
                }else if(data.exam.type == 3){
                    this.showsubmit = true;
                    this.showself = true;
                    this.showhead = true;
                    this.showcommittee = true;
                    this.selfdisable = true;
                    this.headdisable = true;
                    this.selfEvaluation = this.exam.evaluation.selfvalue
                    this.headEvaluation = this.exam.evaluation.highvalue
                }else if(data.exam.type == 4){
                    this.showsubmit = false;
                    this.goback = true;
                }
                
            })
        },
        SubmitFilter(params){
            this.submitable = true,
            Submit(params).then(res =>{
                if(res.status == 200){
                    this.$alert('提交成功', '提示', {
                        confirmButtonText: '确定',
                        callback: action => {
                            // this.$router.push({path:'/todo/index'})
                            this.$router.go(-1);
                        }
                    });
                }else{
                    this.submitable = false,
                    this.$alert('提交失败', '警告', {
                        confirmButtonText: '确定'
                    });
                }
            })
        }
    }
}
</script>

<style scope>
table{
    border-collapse:collapse;
    width: 100%; 
}
td,th{
    height: 45px;
    border-color: #dfe6ec;
    font-size: 14px;
}
td{
    text-align: center;
}
th{
    background-color: #eef1f6;
}
.titletable .titletr th{
    background: #95B3D7;
    font-size: 16px;
}
.fontleft{
    text-align: left;
    padding: 5px 10px;
    width: 75%;
}
.fontleft span{
    margin-left: 5px;
}
.selfEvaluation{
    padding: 5px;
    border: 1px solid rgb(169,169,169);
}
.el-radio__label{
    display: none;
}

.warnAlert{
    margin: 5px 0;
    width: 120px;
    display: flex;
    justify-content: space-cbetween;
    align-items: baseline;
    color: red;
}
.warnAlert span{
    margin-left: 5px;
}
</style>
