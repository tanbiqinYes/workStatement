<template>
<div>
    <div>
        <h1>{{exam.title}}</h1>
        <!--<div v-for="list in exam.shijuan">
            <div v-if="list.examinfo.questiontype =='职业修养'">
                <h2>{{list.examinfo.reporttitle}}</h2>
                <div v-for="(timu,$index) in list.examinfo.questioninfo">
                    <h4>{{$index+1}}.{{timu.questionname}}</h4>
                    <ul ref="radioa">
                        <li v-for="option in timu.qilist" style="padding-left:25px;position:relative">
                            <el-radio class="radio" v-model="timu.check" :label="option.id" style="position:absolute;top:0;left:0"></el-radio>
                            <span>{{option.questionitemname}}</span>
                        </li>
                    </ul>
                </div>
            </div>

            <div v-else>
                <h2>{{list.examinfo.reporttitle}}</h2>
                <div v-for="leixing in list.examinfo.questioninfo">
                    <h3>{{leixing.questionname}}</h3>
                    <div v-for="(timu,$index) in leixing.qdlist">
                        <h4>{{$index+1}}.{{timu.questionname}}</h4>
                        <ul ref="radiob">
                            <li v-for="option in timu.qilist" style="padding-left:25px;position:relative">
                                <el-radio class="radio changeRadio" v-model="timu.check" :label="option.id" style="position:absolute;top:0;left:0"></el-radio>
                                <span>{{option.questionitemname}}</span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>-->

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
                <div v-for="leixing in list.examinfo.questioninfo" style="margin-bottom:15px;">
                    <h3>{{leixing.questionname}}</h3>
                    <div v-for="(timu,$index) in leixing.qdlist" style="margin-bottom:20px;">
                        <h4 >{{$index+1}}.{{timu.questionname}}</h4>
                        <table bordercolor='#dfe6ec' border="1" ref="radiob">
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
    <div v-show="showSelf">
        <h4>自我评价: </h4>
        
        <el-input type="textarea" :disabled="showdis" :autosize="{ minRows: 4, maxRows: 4}" placeholder="请输入内容" v-model="selfEvaluation" style="margin-top:10px;">
        </el-input>
    </div>

    <!--<textarea v-model="selfEvaluation"></textarea>-->
    <div style="display: flex;justify-content: center;margin:15px 0;">
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
            showSelf:true,
            showdis:false,
            selfScore:[],
            tableHead:[{value:'评分标准'},{value:'标准分'}],
            selfEvaluation:'',
            exam:{}
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
            if(totalScore.length == length){
                if(this.showSelf == false){
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
                }else{
                    if(this.selfEvaluation == ''){   
                        this.$alert('请完成个人评价', '警告', {
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
                            "selfvalue": this.selfEvaluation,
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
                    // if(this.selfEvaluation == ''){   
                    //     this.$alert('请完成个人评价', '警告', {
                    //         confirmButtonText: '确定',
                    //         callback: action => {
                    //             // this.$message({
                    //             //     type: 'info',
                    //             //     message: `action: ${action}`
                    //             // });
                    //         }
                    //     });
                    //     return false;
                    // }else{
                    //     let examdata = totalScore;
                    //     let otheraccountid = this.$route.params.uid;
                    //     let evaluation = {
                    //         "selfvalue": this.exam.evaluation.selfvalue,
                    //         "highvalue": this.headEvaluation,
                    //         "committeevalue":''
                    //     }
                    //     let params = {
                    //         "examdata":examdata,
                    //         "otheraccountid":otheraccountid,
                    //         "evaluation":evaluation
                    //     }
                    //     this.SubmitFilter(params);
                    // }
            }
        },
        getUserSubjectFilter(userid){
            let a = {otheraccountid:userid}
            getUserSubject(a).then(res =>{
                let {data} =res;
                this.exam = data.exam;
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
    },
    created(){
        let senduserid = this.$route.params.uid;
        let type = this.$route.params.type;
        this.getUserSubjectFilter(senduserid);
        if(type ==3 || type ==4){
            this.showSelf = false;
        }
    }
}
</script>

<style scoped>
ul,li{
    margin: 0;
    padding: 0;
}
li{
    display: block;
    margin: 5px 0;
}
textarea{
    width: 100%;
    height: 80px;
    box-sizing: border-box;
}
.el-radio__label{
    display: none;
}

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
.fontleft{
    text-align: left;
    padding: 5px 10px;
    width: 90%;
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
h4{
    margin: 0;
    padding: 10px;
    padding-left: 10px;
    background: #50bfff;
    color: white;
}
</style>
<style>
.el-radio__label{
    display: none;
}
</style>
