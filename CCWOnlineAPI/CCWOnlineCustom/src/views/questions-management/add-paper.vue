<template>
    <div class="new_paper">
        <table>
            <tr>
                <td width='80'>名称：</td>
                <td>
                    <el-input v-model="reporttitle" placeholder="请输入内容"></el-input>
                </td>
            </tr>
            <tr>
                <td>类型：</td>
                <td>
                    <el-select v-model="questiontype" placeholder="请选择">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                        </el-option>
                    </el-select>
                </td>
            </tr>
            <tr>
                <td>年份：</td>
                <td>
                    <el-date-picker v-model="reportyear" align="right" type="year" placeholder="请选择">
                    </el-date-picker>
                </td>
            </tr>
            <tr>
                <td>季度：</td>
                <td>
                    <el-select v-model="reportquarter" placeholder="请选择">
                        <el-option v-for="item in quarters" :key="item.value" :label="item.label" :value="item.value">
                        </el-option>
                    </el-select>
                </td>
            </tr>
            <tr>
                <td>总分：</td>
                <td>
                    <el-input-number v-model="questiontypevalue" :min="1" :max="100"></el-input-number>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="textCenter">
                    <span @click="goToPaperDetail()">确认</span>
                    <span @click="backToList()">取消</span>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
import { postPaperMessage } from '../../api/questionsManagement.js';
import { createdPaper } from '../../api/questionsManagement.js';
export default {
    name: 'new_paper',
    data() {
        return {
            reporttitle: '',
            questiontype: '',
            reportyear: '',
            reportquarter: '',
            questiontypevalue: 100,
            options: [{
                value: '职业修养',
                label: '职业修养'
            }, {
                value: '职业能力',
                label: '职业能力'
            }],
            quarters: [{
                value: 1,
                label: '第一季度'
            }, {
                value: 2,
                label: '第二季度'
            }, {
                value: 3,
                label: '第三季度'
            }, {
                value: 4,
                label: '第四季度'
            }],
        }
    },
    methods: {

        backToList() {
            this.$router.push({ path: 'test-library' })
        },
        goToPaperDetail() {
            if (!this.reporttitle) {
                this.$message('请输入名称');
                return false
            }
            else if (!this.questiontype) {
                this.$message('请选择类型');
                return false
            }
            else if (!this.reportyear) {
                this.$message('请输入年份');
                return false
            }
            else if (!this.reportquarter) {
                this.$message('请选择季度');
                return false
            }
            let data = {
                year: this.reportyear.getFullYear(),
                quarter: this.reportquarter,
                questiontype: this.questiontype
            };
            createdPaper(data).then(res => {
                console.log(res.data.result)
                if (res.data.result) {
                    this.$router.push({
                        path: 'details',
                        query: {
                            from: 'creatPaper',
                            data: {
                                id: 0,
                                reportyear: this.reportyear.getFullYear(),
                                reportquarter: this.reportquarter,
                                questiontype: this.questiontype,
                                questiontypevalue: this.questiontypevalue,
                                reporttitle: this.reporttitle,
                                questioninfo: []
                            }
                        }
                    })

                }
                else {
                    this.$message({
                        message: this.reportyear.getFullYear() + '年第' + this.reportquarter + '季度' + this.questiontype + '已存在',
                        type: 'error'
                    });
                }
            });

        }
    }
}
</script>

<style>
.new_paper table {
    width: 50%;
    max-width: 450px;
    min-width: 350px;
    table-layout: fixed;
}

.new_paper .textCenter {
    text-align: center;
}

.new_paper table td {
    height: 40px;
    padding: 10px 10px 10px 0;
}

.new_paper .el-select,
.new_paper .el-date-editor.el-input,
.new_paper .el-input-number {
    width: 100%;
}

.new_paper table td>span {
    display: inline-block;
    width: 80px;
    line-height: 36px;
    background: #20a0ff;
    color: white;
    margin: 0 10px;
    text-align: center;
    border-radius: 2px;
    cursor: pointer;
}
</style>
