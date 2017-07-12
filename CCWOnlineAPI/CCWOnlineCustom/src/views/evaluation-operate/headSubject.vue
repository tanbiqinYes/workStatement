<template>
<div>
    <div>
        <h1>{{exam.title}}</h1>
        <div v-for="list in exam.shijuan">
            
            <div v-if="list.examinfo.questiontype =='职业修养'">
                <h2>{{list.examinfo.reporttitle}}</h2>
                <div v-for="(timu,$index) in list.examinfo.questioninfo" style="margin-bottom:20px;">
                    <h4 >{{$index+1}}.{{timu.questionname}}</h4>
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
                                <td>
                                    <!--<input type="radio" :value="option.id" v-model="timu.check">-->
                                    <el-radio class="radio changeRadio" v-model="timu.check" :label="option.id" ></el-radio>
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
                        <h4 >{{$index+1}}.{{timu.questionname}}</h4>
                        <table bordercolor='#dfe6ec' border="1" ref="radiob" >
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
                                    <td>
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

    <h4>员工自我评价</h4>
    <!--<p class="selfEvaluation">{{selfEvaluation}}</p>-->
    <el-input :disabled="true" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" placeholder="请输入内容" v-model="selfEvaluation">
    </el-input>
    <h4>部门负责人评语</h4>
    <!--<textarea v-model="headEvaluation"></textarea>-->
    
    <el-input :disabled="showis" type="textarea" :autosize="{ minRows: 4, maxRows: 4}" placeholder="请输入内容" v-model="headEvaluation" style="margin-bottom:20px;">
    </el-input>

    <div ></div>

    <div style="display: flex;justify-content: center;margin-bottom:15px;" v-show="showSubmit">
        <el-button type="info" @click="total()">提交</el-button>
    </div>
</div>
</template>

<script>
import {getUserSubject} from '../../api/subject';

import {Submit} from '../../api/subject';
export default {
    data(){
        return{
            showis:false,
            // 主管打分提交数组
            headScore:[],
            // 个人自我评价文字
            selfEvaluation:'这是自我介绍哈哈哈哈',
            // 主管评价
            headEvaluation:'',
            // 是否显示提交按钮
            showSubmit:true,
            // 题目列表
            lists:[
                {
                    // 题目序号
                    num:1,
                    title:'知识和技能',
                    // 主管radio选项
                    picked:[],
                    // table的th
                    tableTitle:[
                        {subtitle:'评分标准',weight:'标准分',selfscore:'自评',headscore:'直接上级'}
                    ],
                    // 题目内容（题干，权重，员工自打分，主管打分选项）
                    name:[
                        {subtitle:'有限的知识和技能，拥有从事本专业工作的基本知识或某一领域的某些知识点，这些知识未在工作中实践过',weight:5,selfscore:'',headscore:''},
                        {subtitle:'具备基础知识和必要技能，集中于本专业工作的基本知识或某一领域的某些知识点，这些知识在工作中多次实践过',weight:10,selfscore:'',headscore:''},
                        {subtitle:'具备全面良好的知识和必要技能，主要领域精通，这些知识有相当了解',weight:15,selfscore:'√',headscore:''},
                        {subtitle:'在本专业领域具有精通、全面的知识和技能',weight:20,selfscore:'',headscore:''}
                    ]
                },
                {
                    num:2,
                    title:'技能和知识',
                    picked:[],
                    tableTitle:[
                        {subtitle:'评分标准',weight:'标准分',selfscore:'自评',headscore:'直接上级'}
                    ],
                    name:[
                        {subtitle:'有限的知识和技能，拥有从事本专业工作的基本知识或某一领域的某些知识点，这些知识未在工作中实践过',weight:5,selfscore:'',headscore:''},
                        {subtitle:'具备基础知识和必要技能，集中于本专业工作的基本知识或某一领域的某些知识点，这些知识在工作中多次实践过',weight:10,selfscore:'√',headscore:''},
                        {subtitle:'具备全面良好的知识和必要技能，主要领域精通，这些知识有相当了解',weight:15,selfscore:'',headscore:''},
                        {subtitle:'在本专业领域具有精通、全面的知识和技能',weight:20,selfscore:'',headscore:''}
                    ]
                }
            ],
            exam: {}
        }
    },
    created(){
        let uid = this.$route.params.uid;
        this.getUserSubjectFilter(uid);
        let relation = this.$route.params.relation;
        if(relation == 0){
            this.showSubmit = false;
            this.showis = true
        }  
    },
    methods:{
        total(){

            let totalScore = [];
            var timu = this.exam.shijuan[0].examinfo.questioninfo;
            console.log(timu);
            // for(let i = 0;i<this.lists.length;i++){
            //     this.totalScore.push(this.lists[i].picked)
            // }
            outermost:
            for(let i=0;i<this.exam.shijuan.length;i++){
                if (this.exam.shijuan[i].examinfo.questiontype == '职业修养') {
                    for (let j = 0; j < this.exam.shijuan[i].examinfo.questioninfo.length; j++) {
                        if (this.exam.shijuan[i].examinfo.questioninfo[j].check == '') {
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
                            totalScore.push(this.exam.shijuan[i].examinfo.questioninfo[j].check)
                        }
                    }
                }else{
                    for (let j = 0; j < this.exam.shijuan[i].examinfo.questioninfo.length; j++) {
                        for (let k = 0; k < this.exam.shijuan[i].examinfo.questioninfo[j].qdlist.length;k++){
                            if (this.exam.shijuan[i].examinfo.questioninfo[j].qdlist[k].check == '') {
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
                                totalScore.push(this.exam.shijuan[i].examinfo.questioninfo[j].qdlist[k].check)
                            }
                        }
                    }
                }              
            }
            let length = parseInt(this.$refs.radioa.length)+parseInt(this.$refs.radiob.length)
            console.log(length);

            if(totalScore.length == length){
                if(this.headEvaluation == ''){   
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
                }else{
                    let examdata = totalScore;
                    let otheraccountid = this.$route.params.uid;
                    let evaluation = {
                        "selfvalue": this.exam.evaluation.selfvalue,
                        "highvalue": this.headEvaluation,
                        "committeevalue":''
                    }
                    let params = {
                        "examdata":examdata,
                        "otheraccountid":otheraccountid,
                        "evaluation":evaluation
                    }
                    this.SubmitFilter(params);
                }
            }  
        },
        getUserSubjectFilter(userid){
            let a = {otheraccountid:userid}
            getUserSubject(a).then(res =>{
                let {data} =res;
                console.log(data);
                this.exam = data.exam;
                this.selfEvaluation = this.exam.evaluation.selfvalue
            })
        },
        SubmitFilter(params){
            Submit(params).then(res =>{
                if(res.status == 200){
                    this.$alert('提交成功', '提示', {
                        confirmButtonText: '确定',
                        callback: action => {
                            this.$router.push({name:'待办事项页'})
                        }
                    });
                }else{
                    this.$alert('提交失败', '警告', {
                        confirmButtonText: '确定'
                    });
                }
            })
        }
    }
}
</script>

<style scoped>
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
.changeRadio .el-radio__label{
    display: none;
}
.thWidth{
    width: 75%;
}
</style>
<style>
.el-radio__label{
    display: none;
}
</style>


