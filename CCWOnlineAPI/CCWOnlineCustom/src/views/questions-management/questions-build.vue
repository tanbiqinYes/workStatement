<template>
    <div class="build">
        <table ref="tab_list">
            <tr>
                <td width="80">考核类别</td>
                <td>
                    <el-select v-model="value" placeholder="请选择">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                        </el-option>
                    </el-select>
                </td>
                <td width="60">总分</td>
                <td>
                    <el-input-number v-model="totalScore" :min="1" :max="100"></el-input-number>
                </td>
                <!--<td :rowspan="this.conditions.length+2">-->
                <!--<span @click="addOption()">添加</span>
                                                    <span @click="deleteOption()">删除</span>-->
                <!--</td>-->
            </tr>
        </table>
        <table v-for="(item,index) in questionInfo">
            <tr v-show="value!='职业道德'">
                <td width="80">考核名称</td>
                <td>
                    <el-input v-model="item.title" placeholder="请输入内容"></el-input>
                </td>
                <td width="60">分值</td>
                <td>
                    <el-input-number v-model="item.score" :min="1" :max="totalScore"></el-input-number>
                </td>
            </tr>
            <tr v-for="(list,dex) in item.conditions">
                <td width="80">评判标准{{dex+1}}</td>
                <td>
                    <el-input v-model="list.text" placeholder="请输入内容"></el-input>
                </td>
                <td width="60">分值</td>
                <td>
                    <el-input-number v-model="list.score" :min="1" :max="item.score"></el-input-number>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <span @click="addOption(index)">添加标准</span>
                    <span @click="deleteOption(index)">删除标准</span>
                    <span @click="deleteQuestion(index)">删除问题</span>
                </td>
            </tr>
        </table>
        <p>
            <span @click="addQuestion()" v-show="value!='职业道德'">添加问题</span>
            <span @click="backBefore()">返回</span>
            <span @click="preview(questionInfo,value)">预览</span>
        </p>
    </div>
</template>

<script>
export default {
    name: 'build',
    data() {
        return {
            options: [{
                value: '职业道德',
                label: '职业道德'
            }, {
                value: '专业知识和技能',
                label: '专业知识和技能'
            }],
            value: '',
            paperId: '',
            totalScore: 1,
            questionInfo: [
                {
                    title: '',
                    score: 0,
                    conditions: [
                        {
                            score: 0,
                            text: '',
                        },
                        {
                            score: 0,
                            text: '',
                        }
                    ]
                },
            ]

        }
    },
    methods: {

        //增加问题
        addQuestion() {
            this.questionInfo.push(
                {
                    title: '',
                    score: 0,
                    conditions: [
                        {
                            score: 0,
                            text: '',
                        },
                        {
                            score: 0,
                            text: '',
                        }
                    ]
                }
            )
        },
        //删除问题
        deleteQuestion(index) {
            if (this.questionInfo.length == 1) {
              this.$message('不能再删除了！')
                return false
            }
            else {
                this.questionInfo.splice(index, 1);
            }
        },
        //增加选项
        addOption(index) {
            this.questionInfo[index].conditions.push({
                score: 0,
                text: '',
            });
        },
        //删除选项
        deleteOption(index) {
            if (this.questionInfo[index].conditions.length > 2) {
                this.questionInfo[index].conditions.splice(this.questionInfo[index].conditions.length - 1, 1);
            }
            else {
                this.$message('不能再删除了！')
                return false
            }
        },
        //返回
        backBefore() {
            console.log('返回')
        },
        //预览
        preview(questionInfo, value) {
            if (value == '') {
                this.$message({
                    message: '请选择考核类别！',
                    type: 'warning'
                })
                return false;
            }
            for (let i = 0; i < questionInfo.length; i++) {
                let current = questionInfo[i];
                if (current.title == '' && value != '职业道德') {
                    this.$message({
                        message: '请填写第' + (i + 1) + '个考核名称！',
                        type: 'warning'
                    })
                    return false;
                }
                for (let j = 0; j < current.conditions.length; j++) {
                    if (current.conditions[j].text == '') {
                        this.$message({
                            message: '请完善第 ' + (i + 1) + ' 个问题的第 ' + (j + 1) + ' 个选项！',
                            type: 'warning'
                        })
                        return false;
                    }
                }
            }
            if(value=='职业道德'){
               console.log(questionInfo)
            }
           

        }
    },
    created() {
        let getData = this.$route.query;
        let action = getData.action;
        //添加新题
        if (action == 'new') {
            this.totalScore = getData.totalScore;
            this.paperId = getData.paperId;
        }
        //渲染旧题
        else {

        }
    }
}
</script>

<style>
.build table {
    table-layout: fixed;
    width: 100%;
}

.build table td {
    padding: 10px 0;
}

.build table td:nth-child(3) {
    text-align: right;
    padding-right: 10px;
}

.build .el-select {
    width: 100%;
}

.build p {
    padding: 0;
}

.build p span {
    display: inline-block;
    margin: 0px 5px;
    width: 80px;
    border-radius: 2px;
    line-height: 36px;
    background: #e80029;
    color: white;
    text-align: center;
    cursor: pointer;
}

.build td>span {
    display: inline-block;
    margin: 5px;
    width: 80px;
    border-radius: 2px;
    line-height: 36px;
    background: #20a0ff;
    color: white;
    text-align: center;
    cursor: pointer;
}
</style>
