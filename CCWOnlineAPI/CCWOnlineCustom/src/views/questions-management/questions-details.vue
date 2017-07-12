<template>
    <div class="paper-details">
        <!--职业修养-->
        <div v-if="message.questiontype=='职业修养'">
            <h3>{{message.reportyear+'年第'+message.reportquarter+'季度'+message.questiontype}}</h3>
            <div v-for="(level1,level1Index) in message.questioninfo" :key="level1.id">
                <h4>{{level1.questionname+'（'+level1.questionvalue+'分）'}}</h4>
                <table class="condition-tab">
                    <tr>
                        <td>评分标准</td>
                        <td width="80" class="textCenter">标准分</td>
                    </tr>
                    <tr v-for="(level2,level2Index) in level1.qilist">
                        <td>{{level2.questionitemname}}</td>
                        <td class="textCenter">{{level2.questionitemvalue}}</td>
                    </tr>
                </table>
                <p class="edite-main">
                    <span style="display:none" @click="editeQuestion(level1Index,message.questiontype,0)">编辑</span>
                    <span v-show="message.id==0" @click="deleteQuestion(level1Index,message.questiontype,0)">删除</span>
                </p>
            </div>
        </div>
        <div v-else>
            <h3>{{message.reporttitle}}</h3>
            <div v-for="(list1,list1Index) in message.questioninfo">
                <h4>{{list1.questionname+'（'+list1.questionvalue+'分）'}}</h4>
                <div v-for="(list2,list2Index) in list1.qdlist">
                    <h5>{{(list2Index+1)+'.'+list2.questionname+'（'+list2.questionvalue+'分）'}}</h5>
                    <table class="condition-tab">
                        <tr>
                            <td>评判标准</td>
                            <td width="80" class="textCenter">标准分</td>
                        </tr>
                        <tr v-for="(list3,list2Index) in list2.qilist">
                            <td>{{list3.questionitemname}}</td>
                            <td class="textCenter">{{list3.questionitemvalue}}</td>
                        </tr>
                    </table>
                    <p class="edite-main">
                        <span style="display:none" @click="editeQuestion(list1Index,message.questiontype,list2Index)">编辑</span>
                        <span v-show="message.id==0" @click="deleteQuestion(list1Index,message.questiontype,list2Index)">删除</span>
                    </p>
                </div>
            </div>
        </div>
        <div class="main-btn">
            <span v-show="message.id==0" @click="addQuestion(message.questiontype)">添加题目</span>
            <span v-show="message.id==0" @click="submitPaper()">提交</span>
            <span @click="returnList()">返回</span>
        </div>
        <div class="cover" v-show="showCover">
            <div class="add-main">
                <table>
                    <tr v-show="message.questiontype!='职业修养'">
                        <td width="100">考核类型</td>
                        <td>
                            <el-select v-model="coverData.addType" placeholder="请选择" @change="relationAbilityScore(coverData.addType)">
                                <el-option v-for="item in coverData.options" :key="item.value" :label="item.label" :value="item.value">
                                </el-option>
                            </el-select>
                        </td>
                        <td class="textCenter" width="60">分值</td>
                        <td width="180">
                            <!--<el-input v-model="coverData.totalScore" placeholder="请输入"></el-input>-->
                            <el-input-number v-model="coverData.totalScore" :min="1" :max="maxScore.typescore" :disabled="maxScore.typestate"></el-input-number>
                        </td>
                    </tr>
                    <tr>
                        <td width="100">考核名称</td>
                        <td>
                            <el-input v-model="coverData.title" placeholder="请输入" @change="setscore()"></el-input>
                        </td>
                        <td class="textCenter" width="60">分值</td>
                        <td width="180">
                            <!--<el-input v-model="coverData.score" placeholder="请输入"></el-input>-->
                            <el-input-number v-model="coverData.score" :min="1" :max="maxScore.namescore"></el-input-number>
                        </td>
                    </tr>
                    <tr v-for="(condition,index) in coverData.conditons">
                        <td>评分标准{{index+1}}</td>
                        <td>
                            <el-input v-model="condition.name" placeholder="请输入"></el-input>
                        </td>
                        <td class="textCenter">分值</td>
                        <td v-if="index<coverData.conditons.length-1">
                            <el-input-number v-model="condition.score" :min="1" :max="coverData.score"></el-input-number>
                        </td>
                        <td v-else>
                            <el-input-number v-model="coverData.score" disabled="disabled"></el-input-number>
                        </td>
                    </tr>
                </table>
                <p class="add-option">
                    <span @click="addOption()">添加</span>
                    <span @click="deleteOption()">删除</span>
                    <span @click="cancelDetail()">取消</span>
                    <span @click="pushQuestion(message.questiontype)">确定</span>
                </p>
            </div>
        </div>
    </div>
</template>

<script>
import { getPaperDetails } from '../../api/questionsManagement.js'
import { postPaperMessage } from '../../api/questionsManagement.js'
import _ from 'lodash';
export default {
    name: 'paper-details',
    data() {
        return {
            maxScore: {
                typescore: 100,
                namescore: 100,
                typestate: true,
                namestate: false,
            },
            hasRevise: false,
            upload: false, //检测上传次数
            showCover: false, //显示遮罩层 的题目编辑
            fromEdite: false,  //判断状态是  编辑 添加
            totalScoreState: '',
            fromEditeId: [],
            coverData: {
                addType: '',
                totalScore: '',
                score: '',
                title: '',
                options: [],
                conditons: []

            },
            message: ''
        }
    },
    methods: {
        setscore() {
            if (this.message.questiontype != '职业修养'&&this.message.questioninfo.length==0) {
                this.maxScore.namescore = this.coverData.totalScore;
            }

        },
        //计算分值是否合规
        totalScoreIsEnough() {
            // return false
            console.log('进入算分')
            let sum = 0;
            if (this.message.questiontype == '职业修养') {
                let getArray = this.message.questioninfo;
                let getValue = this.message.questiontypevalue;
                for (let i = 0; i < getArray.length; i++) {
                    sum += getArray[i].questionvalue
                }
                if (sum < getValue) {
                    this.totalScoreState = '职业修养';
                }
                else {
                    this.totalScoreState = '';
                }
            }
            else {
                let getTotalValue = this.message.questiontypevalue;
                let getOutArray = this.message.questioninfo;
                //判断职业能力下的 知识技能+管理知识=100？
                for (let i = 0; i < getOutArray.length; i++) {
                    let typescore = getOutArray[i].questionvalue
                    let name = getOutArray[i].questionname;
                    let itemsum = 0;
                    sum += typescore;
                    //计算 知识技能 or 管理  下的题目总分是否合规
                    for (let j = 0; j < getOutArray[i].qdlist.length; j++) {
                        itemsum += getOutArray[i].qdlist[j].questionvalue
                    }
                    if (itemsum < typescore) {
                        this.totalScoreState = name;
                        console.log(name)
                        return false;
                    }
                }
                if (sum < getTotalValue) {
                    this.totalScoreState = '职业能力';
                    return false;
                }
                else {
                    this.totalScoreState = '';
                }
            }
        },
        //试卷列表进入页面时 请求试卷信息
        getMessage(id) {
            getPaperDetails({ id }).then(res => {
                let { data } = res;
                if (data) {
                    console.log(data)
                    this.message = data;
                }
                else {
                    this.$message({
                        message: "没此项目的数据！",
                        type: 'error'
                    });
                }
            });
        },
        //修改问题
        editeQuestion(outerIndex, questionType, innerIndex) {
            // this.countScoreFromEdite(outerIndex, questionType, innerIndex)
            this.fromEdite = true;
            this.fromEditeId = [outerIndex, innerIndex]
            let tempData;
            let conditionlist = [];
            this.showCover = true;
            if (questionType == '职业修养') {
                console.log('修改职业修养')
                tempData = _.cloneDeep(this.message.questioninfo[outerIndex]);
                console.log(tempData)
                for (let i = 0; i < tempData.qilist.length; i++) {
                    conditionlist.push({
                        name: tempData.qilist[i].questionitemname,
                        score: tempData.qilist[i].questionitemvalue,
                        id: tempData.qilist[i].id
                    })
                }
                console.log(conditionlist)
                this.coverData.addType = this.message.questiontype;
                this.coverData.score = this.message.questioninfo[outerIndex].questionvalue;
                this.coverData.title = this.message.questioninfo[outerIndex].questionname;
                this.coverData.conditons = conditionlist;
            }
            else {
                console.log('修改其它')
                tempData = _.cloneDeep(this.message.questioninfo[outerIndex].qdlist[innerIndex]);
                for (let i = 0; i < tempData.qilist.length; i++) {
                    conditionlist.push({
                        name: tempData.qilist[i].questionitemname,
                        score: tempData.qilist[i].questionitemvalue,
                        id: tempData.qilist[i].id
                    })
                }
                this.coverData = {
                    addType: this.message.questioninfo[outerIndex].questionname,
                    totalScore: this.message.questioninfo[outerIndex].questionvalue,
                    score: tempData.questionvalue,
                    title: tempData.questionname,
                    options: [
                        {
                            value: this.message.questioninfo[outerIndex].questionname,
                            label: this.message.questioninfo[outerIndex].questionname
                        }
                    ],
                    conditons: conditionlist
                }

            }

        },
        //添加问题
        addQuestion(questionType) {
            this.showCover = true;
            this.fromEdite = false;
            if (questionType == '职业修养') {
                this.coverData = {
                    score: 100,
                    title: '',
                    totalScore:100,
                    conditons: [
                        {
                            name: '',
                            score: 0
                        },
                        {
                            name: '',
                            score: 0
                        }
                    ],
                };
                this.relationCultureScore()
            }
            else {
                this.coverData = {
                    addType: '',
                    totalScore: 0,
                    score: 0,
                    title: '',
                    options: [
                        {
                            value: '专业知识技能',
                            label: '专业知识技能'
                        },
                        {
                            value: '管理与领导知识技能',
                            label: '管理与领导知识技能'
                        }
                    ],
                    conditons: [
                        {
                            name: '',
                            score: 0
                        },
                        {
                            name: '',
                            score: 0
                        }
                    ],
                },
                    this.maxScore.typescore = this.message.questiontypevalue;
            }



        },
        //删除问题
        deleteQuestion(index, type, dex) {
            if (type == '职业修养') {
                this.message.questioninfo.splice(index, 1);
            }
            else {
                this.message.questioninfo[index].qdlist.splice(dex, 1);
                if (this.message.questioninfo[index].qdlist.length == 0) {
                    this.message.questioninfo.splice(index, 1)
                }
            }
        },
        //添加评判标准
        addOption() {
            if (this.fromEdite) {

            }
            this.coverData.conditons.push({
                name: '',
                score: 0,
                id: 0
            })
        },
        //删除评分标准
        deleteOption() {
            if (this.coverData.conditons.length > 2) {
                this.coverData.conditons.splice(this.coverData.conditons.length - 1, 1)
            }
            else {
                this.$message('至少两个评分标准')
            }
        },
        //取消添加
        cancelDetail() {
            this.showCover = false;
        },
        //确认添加问题
        pushQuestion(messageType) {
            this.coverData.conditons[this.coverData.conditons.length - 1].score = this.coverData.score;
            if (this.fromEdite) {
                let dexArray = this.fromEditeId;
                let editelist = [];
                if (this.message.questiontype == '职业修养') {
                    if (this.coverData.title == '') {
                        this.$message('请输入考核名称')
                        return false
                    }
                    for (let i = 0; i < this.coverData.conditons.length; i++) {
                        if (this.coverData.conditons[i].name == '') {
                            this.$message('请输入第' + (i + 1) + '评分标准')
                            return false
                        }
                    }
                    console.log('验证完成');
                    if (this.message.questioninfo[dexArray[0]].questionname != this.coverData.title) {
                        this.message.questioninfo[dexArray[0]].id = 0
                    }
                    this.message.questioninfo[dexArray[0]].questionname = this.coverData.title;
                    this.message.questioninfo[dexArray[0]].questionvalue = this.coverData.score;
                    for (let k = 0; k < this.coverData.conditons.length; k++) {
                        editelist.push({
                            id: this.coverData.conditons[k].id,
                            questionid: this.message.questioninfo[dexArray[0]].id,
                            questionitemname: this.coverData.conditons[k].name,
                            questionitemvalue: this.coverData.conditons[k].score,
                            sequence: k + 1
                        })
                    }
                    this.message.questioninfo[dexArray[0]].qilist = editelist;
                }
                else {
                    if (this.coverData.addType == '') {
                        this.$message('请选择考核类型')
                        return false
                    }
                    if (this.coverData.title == '') {
                        this.$message('请输入考核名称')
                        return false
                    }
                    for (let i = 0; i < this.coverData.conditons.length; i++) {
                        if (this.coverData.conditons[i].name == '') {
                            this.$message('请输入第' + (i + 1) + '评分标准')
                            return false
                        }
                    }
                    console.log('验证通过');
                    if (this.message.questioninfo[dexArray[0]].qdlist[dexArray[1]].questionname != this.coverData.title) {
                        this.message.questioninfo[dexArray[0]].qdlist[dexArray[1]].id = 0
                    }
                    this.message.questioninfo[dexArray[0]].qdlist[dexArray[1]].questionname = this.coverData.title;
                    this.message.questioninfo[dexArray[0]].qdlist[dexArray[1]].questionvalue = this.coverData.score;
                    for (let j = 0; j < this.coverData.conditons.length; j++) {
                        editelist.push({
                            id: this.coverData.conditons[j].id,
                            questionid: this.message.questioninfo[dexArray[0]].qdlist[dexArray[1]].id,
                            questionitemname: this.coverData.conditons[j].name,
                            questionitemvalue: this.coverData.conditons[j].score,
                            sequence: j + 1
                        })
                    }
                    this.message.questioninfo[dexArray[0]].qdlist[dexArray[1]].qilist = editelist;

                }
            }
            else {
                if (messageType == '职业修养') {
                    if (this.coverData.title == '') {
                        this.$message('请输入考核名称')
                        return false
                    }
                    for (let i = 0; i < this.coverData.conditons.length; i++) {
                        if (this.coverData.conditons[i].name == '') {
                            this.$message('请输入第' + (i + 1) + '评分标准')
                            return false
                        }
                    }
                    console.log('验证完成');
                    let optionlist = [];
                    for (let j = 0; j < this.coverData.conditons.length; j++) {
                        optionlist.push({
                            id: 0,
                            questionid: 0,
                            questionitemname: this.coverData.conditons[j].name,
                            questionitemvalue: this.coverData.conditons[j].score,
                            sequence: j + 1
                        })
                    }
                    this.message.questioninfo.push({
                        id: 0,
                        questiontypeid: 0,
                        qtype: 2,
                        questionname: this.coverData.title,
                        questionvalue: this.coverData.score,
                        sequence: this.message.questioninfo.length + 1,
                        qilist: optionlist
                    })
                }
                else {
                    if (this.coverData.addType == '') {
                        this.$message('请选择考核类型')
                        return false
                    }
                    if (this.coverData.title == '') {
                        this.$message('请输入考核名称')
                        return false
                    }
                    for (let i = 0; i < this.coverData.conditons.length; i++) {
                        if (this.coverData.conditons[i].name == '') {
                            this.$message('请输入第' + (i + 1) + '评分标准')
                            return false
                        }
                    }
                    console.log('验证通过')
                    let typeIndex = -1;
                    for (let k = 0; k < this.message.questioninfo.length; k++) {
                        if (this.coverData.addType == this.message.questioninfo[k].questionname) {
                            typeIndex = k;
                        }
                    }

                    //新建类型
                    if (typeIndex == -1) {
                        console.log('进入创建type')
                        this.message.questioninfo.push({
                            id: 0,
                            questiontypeid: 0,
                            qtype: 0,
                            questionname: this.coverData.addType,
                            questionvalue: this.coverData.totalScore,
                            sequence: this.message.questioninfo.length + 1,
                            qdlist: []
                        });
                        for (let y = 0; y < this.message.questioninfo.length; y++) {
                            if (this.coverData.addType == this.message.questioninfo[y].questionname) {
                                typeIndex = y;
                            }
                        }
                    }

                    let optionlist2 = [];
                    for (let p = 0; p < this.coverData.conditons.length; p++) {
                        optionlist2.push({
                            id: 0,
                            questionid: 0,
                            questionitemname: this.coverData.conditons[p].name,
                            questionitemvalue: this.coverData.conditons[p].score,
                            sequence: p + 1
                        })
                    }
                    this.message.questioninfo[typeIndex].qdlist.push({
                        id: 0,
                        questiontypeid: 0,
                        qtype: 2,
                        questionname: this.coverData.title,
                        questionvalue: this.coverData.score,
                        sequence: this.message.questioninfo[typeIndex].qdlist.length + 1,
                        qilist: optionlist2
                    })

                }
            }

            this.hasRevise = true;
            this.showCover = false;
        },
        //返回列表
        returnList() {
            if (this.hasRevise) {
                if (!this.upload) {
                    this.$confirm('您还未提交修改，确认要离开吗?', '提示', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                    }).then(() => {
                        this.$router.push({ path: 'test-library' })
                    }).catch(() => {
                        this.$message({
                            type: 'info',
                            message: '已取消'
                        });
                    });
                }
            }
            else {
                this.$router.push({ path: 'test-library' })
            }

        },
        //提交试卷
        submitPaper() {
            this.totalScoreIsEnough();
            if (this.totalScoreState) {
                this.$message({
                    message: this.totalScoreState + '分值不够，请继续添加题目',
                    type: 'error'
                })
                return false
            }
            if (this.upload) {
                this.$message({
                    message: "请勿重复提交",
                    type: 'warning'
                })
                return false
            }
            let data = {
                examinfo: {
                    id: this.message.id,
                    reportyear: this.message.reportyear,
                    reportquarter: this.message.reportquarter,
                    questiontype: this.message.questiontype,
                    questiontypevalue: this.message.questiontypevalue,
                    reporttitle: this.message.reporttitle,
                },
                questioninfo: this.message.questioninfo
            };
            postPaperMessage(data).then(res => {
                console.log(res.statusText)
                if (res.statusText) {
                    this.$message({
                        message: "提交成功",
                        type: 'success'
                    });
                    this.$router.push({ path: 'test-library' })

                }
                else {
                    this.$message({
                        message: "提交失败",
                        type: 'error'
                    });
                }
            });
        },
        //编辑问题时候算分
        countScoreFromEdite(outerIndex, questionType, innerIndex) {
            return false
            let sumScore = 0;
            if (questionType == '职业修养') {
                for (let i = 0; i < this.message.questioninfo.length; i++) {
                    if (i == outerIndex) {
                        continue
                    }
                    sumScore += this.message.questioninfo[i].questionvalue
                }
                this.maxScore.namescore = 100 - sumScore;
                this.$message('最多输入' + this.maxScore.namescore + '分')
            }
            else {
                for (let j = 0; j < this.message.questioninfo.length; j++) {
                    if (j == outerIndex) {
                        continue
                    }
                    sumScore += this.message.questioninfo[j].questionvalue
                }
                this.maxScore.typescore = 100 - sumScore;
                this.$message('最多输入' + this.maxScore.typescore + '分')
                let sum = 0;
                for (let k = 0; k < this.message.questioninfo[outerIndex].qdlist.length; k++) {
                    if (k == innerIndex) {
                        continue
                    }
                    sum += this.message.questioninfo[k].questionvalue
                }
                this.maxScore.namescore = 100 - sumScore;
                this.$message('最多输入' + this.maxScore.namescore + '分')
            }
        },
        //添加问题时算职业修养分
        relationCultureScore() {
            let maxNameScore = this.message.questiontypevalue;
            let sum = 0;
            console.log(this.message.questioninfo.length)
            if (this.message.questioninfo.length > 0) {
                for (let i = 0; i < this.message.questioninfo.length; i++) {
                    sum += this.message.questioninfo[i].questionvalue
                }
                maxNameScore = this.message.questiontypevalue - sum;
                if (maxNameScore == 0) {
                    this.$message({
                        message: '职业修养题目分值已满!',
                        type: 'warning'
                    })
                    this.showCover = false;
                }
                else {
                    this.$message('职业修养还可以添加' + maxNameScore + '分值的题目')
                }
            }
            this.maxScore.namescore = maxNameScore;
        },
        //添加问题时算职业能力分
        relationAbilityScore(typename) {
              console.log(typename)
            if(!typename){
                  console.log(1111)
                return false
            }
            let maxTypeScore = 0;
            let maxNameScore = 0;
            //计算类型分  最大值 or 固定值
            console.log(typename)
            // if (typename=='专业知识技能'||typename=='管理与领导知识技能') {
                let getTypeIndex = -1;
                let getTypeValue = 0;
                let sum = 0;
                for (let i = 0; i < this.message.questioninfo.length; i++) {
                    if (this.message.questioninfo[i].questionname == typename) {
                        getTypeIndex = i;
                        getTypeValue = this.message.questioninfo[i].questionvalue;
                    }
                    sum += this.message.questioninfo[i].questionvalue;
                }
                //存在同类型
                if (getTypeIndex >= 0) {
                    maxTypeScore = this.message.questioninfo[getTypeIndex].questionvalue;
                    this.coverData.totalScore = getTypeValue;
                    this.maxScore.typestate = true;
                    let singleSum = 0;
                    for (let j = 0; j < this.message.questioninfo[getTypeIndex].qdlist.length; j++) {
                        singleSum += this.message.questioninfo[getTypeIndex].qdlist[j].questionvalue;
                    }
                    maxNameScore = getTypeValue - singleSum;
                    //分值满 不能添加题目
                    if (maxNameScore <= 0) {
                        this.$message({
                            message: typename + '题目分值已满!',
                            type: 'warning'
                        });
                        this.showCover = false;
                    }
                    //可以添加题目
                    else {
                        this.$message(typename + '还可以添加' + maxNameScore + '分值的题目')
                    }

                }
                //不存在同类型
                else {
                    maxTypeScore = this.message.questiontypevalue - sum;
                    maxNameScore = maxTypeScore;
                    this.coverData.totalScore = 0;
                    this.maxScore.typestate = false;
                }
            // }
            this.maxScore.typescore = maxTypeScore;
            this.maxScore.namescore = maxNameScore;
        }
    },
    created() {
        let queryFrom = this.$route.query.from;
        let queryData = this.$route.query.data;
        console.log(queryFrom);
        //创建新试卷进入页面
        if (queryFrom == 'creatPaper') {
            this.message = queryData
        }
        //试卷列表 进入页面
        else if (queryFrom == 'lookPaper') {
            this.getMessage(queryData)
        }
        else {
            this.$router.push({ path: 'test-library' })
        }

    }

}
</script>

<style scoped>
.paper-details h3 {
    margin: 0;
    padding: 20px 0;
    text-align: center;
}

.paper-details h4 {
    margin: 0;
    padding: 10px;
    background: #20a0ff;
    color: white;
}

.paper-details h5 {
    margin: 0;
    padding: 10px;
    background: #f4f4f4;
}

.paper-details .condition-tab,
.paper-details .add-main table {
    table-layout: fixed;
    width: 100%;
    border-collapse: collapse;
}

.paper-details .add-main table td {
    padding: 10px 0;
}

.textCenter {
    text-align: center;
}

.paper-details .condition-tab td {
    height: 40px;
    border: solid 1px #dadada;
    padding: 0 10px;
}

.paper-details .edite-main {
    text-align: right;
    margin: 10px 0 20px 0;
}

.paper-details .edite-main>span {
    display: inline-block;
    width: 80px;
    text-align: center;
    line-height: 30px;
    background: #20a0ff;
    color: white;
    border-radius: 2px;
    cursor: pointer;
    margin: 0 0 0 5px;
}

.paper-details .main-btn {
    text-align: right;
    margin: 20px 0;
}

.paper-details .main-btn>span {
    display: inline-block;
    width: 80px;
    text-align: center;
    line-height: 30px;
    background: #20a0ff;
    color: white;
    border-radius: 2px;
    cursor: pointer;
    margin: 0 0 0 5px;
}

.paper-details .cover {
    z-index: 9999;
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(20, 20, 20, 0.6)
}

.paper-details .cover .add-main {
    width: 70%;
    max-height: 400px;
    overflow-y: auto;
    background: white;
    top: 50%;
    left: 50%;
    position: absolute;
    transform: translate(-50%, -50%);
    padding: 10px;
    border-radius: 2px;
}

.paper-details .el-select {
    width: 100%;
}

.paper-details .add-option {
    margin: 0;
    padding: 10px 0;
    text-align: right;
}

.paper-details .add-option span {
    display: inline-block;
    width: 80px;
    text-align: center;
    line-height: 30px;
    background: #20a0ff;
    color: white;
    border-radius: 2px;
    cursor: pointer;
    margin: 0 0 0 5px;
}
</style>

<style>
.el-select-dropdown,
.el-message {
    z-index: 9999!important;
}
</style>

