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
                    <span @click="editeQuestion(level1Index,message.questiontype,0)">编辑</span>
                    <span class="deletebtn" @click="deleteQuestion(level1Index,message.questiontype,0)">删除</span>
                </p>
            </div>
        </div>
        <div v-else>
            <h3>{{message.reportyear+'年第'+message.reportquarter+'季度'+message.questiontype}}</h3>
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
                        <span @click="editeQuestion(list1Index,message.questiontype,list2Index)">编辑</span>
                        <span class="deletebtn" @click="deleteQuestion(list1Index,message.questiontype,list2Index)">删除</span>
                    </p>
                </div>
            </div>
        </div>
        <div class="main-btn">
            <span @click="addQuestion()">添加题目</span>
            <span @click="submitPaper()">提交</span>
            <span v-show="backToList" @click="returnList()" style="background:#ff4949">返回</span>
        </div>
        <div class="cover" v-show="showCover">
            <div class="add-main">
                <table>
                    <tr v-show="message.questiontype!='职业修养'">
                        <td width="100">考核类型</td>
                        <td>
                            <el-select v-model="newdataState.typename" placeholder="请选择" @change="choseType(newdataState.typename)">
                                <el-option v-for="item in newdataState.typeoption" :key="item.value" :label="item.label" :value="item.value">
                                </el-option>
                            </el-select>
                        </td>
                        <td class="textCenter" width="60">分值</td>
                        <td width="180">
                            <el-input-number v-model="newdataState.typescore" :min="1" :max="maxScore.typescore" :disabled="maxScore.typestate"></el-input-number>
                        </td>
                        <td width="90"></td>
                    </tr>
                    <tr>
                        <td width="100">考核名称</td>
                        <td>
                            <el-input v-model="newdata.questionname" placeholder="请输入"></el-input>
                            <!--@change="setscore()"-->
                        </td>
                        <td class="textCenter" width="60">分值</td>
                        <td width="180">
                            <el-input-number v-model="newdata.questionvalue" :min="0" :max="maxScore.namescore"></el-input-number>
                        </td>
                        <td width="90"></td>
                    </tr>
                    <tr v-for="(condition,index) in newdata.qilist">
                        <td>评分标准{{index+1}}</td>
                        <td>
                            <el-input v-model="condition.questionitemname" placeholder="请输入"></el-input>
                        </td>
                        <td class="textCenter">分值</td>
                        <td v-if="index<newdata.qilist.length-1">
                            <el-input-number v-model="condition.questionitemvalue" :min="1" :max="newdata.questionvalue"></el-input-number>
                        </td>
                        <td v-else>
                            <el-input-number v-model="newdata.questionvalue" disabled="disabled"></el-input-number>
                        </td>
                        <td width="90" style="padding-left:10px;">
                            <el-button size="small" type="warning" @click="deleteOption(index)">
                                <i class="el-icon-minus"></i>
                            </el-button>
                            <el-button type="primary" size="small" v-if="index==newdata.qilist.length-1" @click="addOption()">
                                <i class="el-icon-plus"></i>
                            </el-button>
    
                        </td>
                    </tr>
                </table>
                <p class="add-option">
                    <span @click="cancelDetail()" v-show="fromEditeId.action" style="background:#ff4949">取消</span>
                    <span @click="pushQuestion(message.questiontype)">确定</span>
                </p>
            </div>
        </div>
    </div>
</template>

<script>
import { getPaperDetails } from '../../api/questionsManagement.js'
import { postPaperMessage } from '../../api/questionsManagement.js'
import { deletePaperQuestion } from '../../api/questionsManagement.js'
import _ from 'lodash';
export default {
    name: 'paper-details',
    data() {
        return {
            maxScore: {
                typescore: 100,
                namescore: 100,
                typestate: false,
                namestate: false,
            },
            hasRevise: false,
            upload: false,      //检测上传次数
            showCover: false,   //显示遮罩层 的题目编辑
            fromEdite: false,   //判断状态是  编辑 添加
            totalScoreState: '',
            backToList:true,
            fromEditeId: {
                outdex: 0,
                index: 0,
                type: '',
                action: true
            },    //编辑题目 缓存序号
            newdata: {},        //题目选项 数据
            newdataState: {     //考核类别状态数据
                typescore: 0,
                typename: '',
                typeoption: [
                    {
                        value: '专业知识技能',
                        label: '专业知识技能',
                    },
                    {
                        value: '管理能力',
                        label: '管理能力',
                    }
                ]
            },
            message: ''
        }
    },
    methods: {
        //提交时 计算分值是否合规
        totalScoreIsEnough() {
            let sum = 0;
            let value = this.message.questiontypevalue;
            for (let i = 0; i < this.message.questioninfo.length; i++) {
                sum += this.message.questioninfo[i].questionvalue;
            }
            if (sum == value) {
                //可以提交
                this.totalScoreState = '分值足够'
            }
            else if (sum > value) {
                //分值超出
                this.totalScoreState = '总分值超出，请删除题目或减少题目分值';
                return false
            }
            else {
                //分值不够
                this.totalScoreState = '总分值不够，请继续添加题目';
                return false
            }
            if (this.message.questiontype != '职业修养') {
                for (let i = 0; i < this.message.questioninfo.length; i++) {
                    let typevalue = this.message.questioninfo[i].questionvalue;
                    let typename = this.message.questioninfo[i].questionname;
                    let typesum = 0;
                    for (let j = 0; j < this.message.questioninfo[i].qdlist.length; j++) {
                        typesum += this.message.questioninfo[i].qdlist[j].questionvalue;
                    }
                    console.log('typesum+typevalue')
                    console.log(typesum)
                    console.log(typevalue)
                    if (typesum == typevalue) {
                        //可以提交
                        this.totalScoreState = '分值足够'
                    }
                    else if (typesum > typevalue) {
                        //分值超出
                        this.totalScoreState = typename + '分值超出，请删除题目或减少题目分值';
                        return false
                    }
                    else {
                        //分值不够
                        this.totalScoreState = typename + '分值不够，请继续添加题目';
                        return false
                    }
                }
            }
        },
        //添加题目时候   算分
        countRemainScore() {
            let type = this.message.questiontype;
            let value = this.message.questiontypevalue;
            let sum = 0;
            let remain = 0;
            if (type == '职业修养') {
                for (let i = 0; i < this.message.questioninfo.length; i++) {
                    sum += this.message.questioninfo[i].questionvalue;
                }
                remain = value - sum;
                if (remain > 0) {
                    this.$message('职业修养还可以添加' + remain + '分的题目');
                    this.maxScore.namescore = remain;
                }

                else {
                    this.$message({ message: '职业修养分值已满，无法继续添加题目', type: 'warning' });
                    this.showCover = false;
                }
            }
            else {
                let lengthAll = this.message.questioninfo.length;
                if (lengthAll == 1) {
                    //只有一种类型
                }
                else if (lengthAll == 2) {
                    //两种类型都有
                    let tempscore = [];
                    for (let j = 0; j < 2; j++) {
                        let totalscore = this.message.questioninfo[j].questionvalue;
                        let typename = this.message.questioninfo[j].questionname
                        let score = 0;
                        for (let k = 0; k < this.message.questioninfo[j].qdlist.length; k++) {
                            score += this.message.questioninfo[j].qdlist[k].questionvalue;
                        }
                        tempscore[j] = totalscore - score;
                    }
                    for (let h = 0; h < tempscore.length; h++) {
                        remain += tempscore[h]
                    }
                    if (remain == 0) {
                        this.$message({ message: '题目分值已满，无法继续添加题目', type: 'warning' });
                        this.showCover = false;
                    }
                }
                else {
                    //一种类型都没有
                    this.maxScore.namescore = value;
                    this.maxScore.typescore = value;
                }
            }
        },
        //针对  职业能力  加题算分
        choseType(value) {
            let infolength = this.message.questioninfo.length;
            let totalvalue = this.message.questiontypevalue;
            //有一个类别
            if (infolength == 1) {
                let temp = this.message.questioninfo[0];
                //类别一样
                if (value == temp.questionname) {
                    this.newdataState.typescore = temp.questionvalue;
                }
                //存在另外一个类别
                else {
                    this.newdataState.typescore = this.message.questiontypevalue - temp.questionvalue;
                }
            }
            //两个类别都存在
            else if (infolength == 2) {
                for (let j = 0; j < 2; j++) {
                    if (value == this.message.questioninfo[j].questionname) {
                        let current = this.message.questioninfo[j];
                        this.newdataState.typescore = current.questionvalue;
                    }
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
            this.fromEditeId.outdex = outerIndex;
            this.fromEditeId.index = innerIndex;
            this.fromEditeId.type = questionType;
            let tempdata = '';
            if (questionType == '职业修养') {
                tempdata = _.cloneDeep(this.message.questioninfo[outerIndex]);
            }
            else {
                tempdata = _.cloneDeep(this.message.questioninfo[outerIndex].qdlist[innerIndex]);
                let name = this.message.questioninfo[outerIndex].questionname;
                let score = this.message.questioninfo[outerIndex].questionvalue;
                this.newdataState = {
                    typescore: score,
                    typename: name,
                    typeoption: [
                        {
                            value: name,
                            label: name,
                        }
                    ]
                };
            }
            this.newdata = tempdata;
            this.fromEdite = true;
            this.showCover = true;
        },
        //添加问题
        addQuestion() {
            this.showCover = true;
            this.fromEdite = false;
            this.fromEditeId.action = true;
            this.countRemainScore();
            //重置缓存数据
            this.newdata = {
                id: 0,
                qtype: 2,
                questiontypeid: 0,
                questionname: '',
                questionvalue: 0,
                sequence: 0,
                qilist: [
                    {
                        id: 0,
                        questionid: 0,
                        questionitemname: '',
                        questionitemvalue: 0,
                        sequence: 0
                    },
                    {
                        id: 0,
                        questionid: 0,
                        questionitemname: '',
                        questionitemvalue: 0,
                        sequence: 0
                    }
                ],

            };
            //重置类型数据
            this.newdataState = {
                typescore: 1,
                typename: '',
                typeoption: [
                    {
                        value: '专业知识技能',
                        label: '专业知识技能',
                    },
                    {
                        value: '管理能力',
                        label: '管理能力',
                    }
                ]
            }
        },
        //删除问题
        deleteQuestion(index, type, dex) {
            this.$confirm('此操作将永久删除该题目, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                //执行删除操作
                let id = 0;
                let action = 'question';
                if (type == '职业修养') {
                    id = this.message.questioninfo[index].id;
                }
                else {
                    id = this.message.questioninfo[index].qdlist[dex].id;
                }
                if (id == 0) {
                    if (type == '职业修养') {
                        this.message.questioninfo.splice(index, 1);
                    }
                    else {
                        this.message.questioninfo[index].qdlist.splice(dex, 1);
                        if (this.message.questioninfo[index].qdlist.length == 0) {
                            this.message.questioninfo.splice(index, 1)
                        }
                    }
                    this.$message({ message: '删除成功', type: 'success' })
                }
                else {
                    //执行ajax函数
                    deletePaperQuestion({ id, action }).then(res => {
                        console.log(res.status)
                        if (res.status==204) {
                            this.$message({ message: "删除成功", type: 'success' })
                            this.backToList=false;
                            if (type == '职业修养') {
                                this.message.questioninfo.splice(index, 1);
                            }
                            else {
                                this.message.questioninfo[index].qdlist.splice(dex, 1);
                                if (this.message.questioninfo[index].qdlist.length == 0) {
                                    this.message.questioninfo.splice(index, 1)
                                }
                            }

                        }
                        else {
                            this.$message({ message: "删除失败", type: 'error' })
                        }
                    }).catch(() => {
                        //返回
                        this.$message({ message: '网络错误', type: 'warning' })
                    })
                }
            }).catch(() => {
                //返回

            });

        },
        //添加评判标准
        addOption() {
            this.newdata.qilist.push({
                id: 0,
                questionid: 0,
                questionitemname: '',
                questionitemvalue: '',
                sequence: 0
            })
        },
        //删除评分标准
        deleteOption(index) {
            if (this.newdata.qilist.length < 3) {
                this.$message({ message: '至少两个评分标准，不能再删除了', type: 'warning' })
                return false
            }
            let current = this.newdata.qilist[index].id;
            if (current == 0) {
                this.newdata.qilist.splice(index, 1)
                this.$message({ message: '删除成功', type: 'success' })
            }
            else {
                this.$confirm('此操作将永久删除该选项, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    let id = current;
                    let action = 'item';
                    //执行ajax
                    deletePaperQuestion({ id, action }).then(res => {
                        if (res.statusText) {
                            this.$message({ message: "删除成功", type: 'success' })
                            this.fromEditeId.action = false;
                            this.newdata.qilist.splice(index, 1)
                        }
                        else {
                            this.$message({ message: "删除失败", type: 'error' })
                        }
                    }).catch(() => {
                        //返回
                        this.$message({ message: '网络错误', type: 'warning' })
                    })
                }).catch(() => {
                    //返回
                    this.$message({ message: '取消删除', type: 'warning' })
                });
            }
        },
        //取消添加
        cancelDetail() {
            this.showCover = false;
        },
        //确认添加问题
        pushQuestion(messageType) {
            this.upload=false;
            let lengthLast = this.newdata.qilist.length;
            this.newdata.qilist[lengthLast - 1].questionitemvalue = this.newdata.questionvalue;
            //验证
            if (messageType != '职业修养' && this.newdataState.typename == "") {
                this.$message({ message: '请选择考核类别！', type: 'info' })
                return false
            }
            else if (this.newdata.questionname == "") {
                this.$message({ message: '请填写题目名称！', type: 'info' })
                return false
            }
            for (let k = 0; k < this.newdata.qilist.length; k++) {
                if (this.newdata.qilist[k].questionitemname == "") {
                    this.$message({ message: '请填第' + (k + 1) + '个评分标准名称！', type: 'info' })
                    return false
                }
            }
            //修改题目  确认
            if (this.fromEdite) {
                let outdex = this.fromEditeId.outdex;
                let index = this.fromEditeId.index;
                let type = this.fromEditeId.type;
                if (type == '职业修养') {
                    this.message.questioninfo[outdex] = this.newdata;
                }
                else {
                    let data = this.newdataState;
                    this.message.questioninfo[outdex].qdlist[index] = this.newdata;
                    for (let y = 0; y < this.message.questioninfo.length; y++) {
                        if (this.message.questioninfo[y].questionname == this.newdataState.typename) {
                            this.message.questioninfo[y].questionvalue = this.newdataState.typescore;
                        }
                    }
                }
            }
            //添加题目 确认
            else {
                if (messageType == '职业修养') {
                    this.message.questioninfo.push(this.newdata)
                    console.log('进入职业修养确认')
                }
                else {
                    console.log('进入职业能力确认')
                    let typename = this.newdataState.typename;
                    let typescore = this.newdataState.typescore;
                    let state = null;
                    //查找存在相同 类型的序号
                    for (let i = 0; i < this.message.questioninfo.length; i++) {
                        if (this.message.questioninfo[i].questionname == typename) {
                            state = i;
                            break
                        }
                    }
                    if (state == null) {
                        this.message.questioninfo.push({
                            id: 0,
                            qdlist: [],
                            qtype: 0,
                            questiontypeid: 0,
                            sequence: 0,
                            questionname: typename,
                            questionvalue: typescore,
                        });
                        for (let i = 0; i < this.message.questioninfo.length; i++) {
                            if (this.message.questioninfo[i].questionname == typename) {
                                state = i;
                                break
                            }
                        }
                    }
                    this.message.questioninfo[state].qdlist.push(this.newdata)

                }
            }
            this.hasRevise = true;
            this.showCover = false;
            this.fromEditeId.action = true;
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
                    })
                }
            }
            else {
                this.$router.push({ path: 'test-library' })
            }

        },
        //提交试卷
        submitPaper() {
            this.totalScoreIsEnough();
            if (this.totalScoreState != '分值足够') {
                this.$message({
                    message: this.totalScoreState,
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
            this.upload=true;
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
        countScoreCanAdd() {
            let type = this.message.questiontype;
            let score = this.message.questiontypevalue;
            let sum = 0;
            let result1 = null;
            let result2 = null;
            if (type == '职业修养') {
                for (let i = 0; i < this.message.questioninfo.lenght; i++) {
                    sum += this.message.questioninfo[i].questionvalue;
                }
                if (sum >= score) {
                    //分值超出
                    result1 = '职业修养'
                }
            }
            else {
                for (let j = 0; j < this.message.questioninfo.lenght; j++) {
                    let value = this.message.questioninfo[j].questionvalue;
                    let name = this.message.questioninfo[j].questionname;
                    sum += value;
                    let innerSum = 0;
                    for (let k = 0; k < this.message.questioninfo[j].qdlist.length; k++) {
                        innerSum += this.message.questioninfo[j].qdlist[k].questionvalue;
                    }
                    if (innerSum >= value) {
                        result2 = name;
                    }
                }
                if (sum >= score && !result2) {
                    result1 = '职业能力'
                }
            }
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

.paper-details .edite-main .deletebtn {
    background: #ff4949;
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
    z-index: 1993;
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(20, 20, 20, 0.6)
}

.paper-details .cover .add-main {
    width: 80%;
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
    z-index: 1993!important;
}
</style>

