<template>
    <div class="question_list">
        <table class="search_main">
            <tr>
                <td width="60">年份：</td>
                <td>
                    <el-date-picker v-model="value1" align="right" type="year" placeholder="请选择" format="yyyy">
                    </el-date-picker>
                </td>
                <td width="60">季度：</td>
                <td>
                    <el-select v-model="value2" clearable placeholder="请选择">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                        </el-option>
                    </el-select>
                </td>
                <td width="90">
                    <span class="search" @click="searchKey(value1,value2)">搜索</span>
                </td>
                <td width="90">
                    <span class="add_paper" @click="addPaper()">添加试卷</span>
                </td>
    
            </tr>
        </table>
        <table class="paper_list" v-show="testPaper!=''">
            <thead>
                <tr>
                    <th>名称</th>
                    <th width="20%">类型</th>
                    <th width="80">年</th>
                    <th width="80">季度</th>
                    <th width="180">操作</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item,index) in testPaper" :key='item.id'>
                    <td>{{item.title}}</td>
                    <td>{{item.type}}</td>
                    <td>{{item.year}}</td>
                    <td>{{item.quarter}}</td>
                    <td>
                        <span class='look' @click="lookDetails(index)">查看</span>
                        <span class='look deletepaper' @click="deletePaper(index)">删除</span>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="page-main">
            <el-pagination @current-change="handleCurrentChange" :current-page.sync="currentPage" :page-size="pageNum" layout="total, prev, pager, next, jumper" :total="totalPage">
            </el-pagination>
        </div>
    </div>
</template>

<script>
import { getPaperList } from '../../api/questionsManagement.js';
import { getSearchPaperList } from '../../api/questionsManagement.js';
import { deletePaperQuestion } from '../../api/questionsManagement.js'
export default {
    name: 'question_list',
    data() {
        return {
            currentPage: 1,
            pageNum: 10,
            totalPage: 1,
            value1: '',
            options: [{
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
            value2: '',
            testPaper: ''
        }
    },
    methods: {
        //删除试卷
        deletePaper(index) {

            this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                let id = this.testPaper[index].id;
                let action = 'exam';
                deletePaperQuestion({ id, action }).then(res => {
                    if (res.status == 204) {
                        this.$message({ message: "删除成功", type: 'success' })
                        let year = this.value1;
                        let quarter = this.value2;
                        if (!year) {
                            year = 0;
                        }
                        else {
                            year = year.getFullYear();
                        }
                        if (!quarter) {
                            quarter = 0
                        }
                        let pageindex = 1;
                        let pagesize = 10;
                        let searchkey = {
                            year: year,
                            quarter: quarter
                        };
                        let orderby = {};
                        this.getPaperListFilter(this.currentPage, this.pageNum, searchkey, orderby);
                    }
                    else {
                        this.$message({ message: "删除失败", type: 'error' })
                    }
                }).catch((err) => {
                    //返回
                    this.$message({ message: '网络错误', type: 'warning' })
                })
            })
        },
        //查看详情
        lookDetails(index) {
            this.$router.push({ path: 'details', query: { data: this.testPaper[index].id, from: 'lookPaper' } })
        },
        //搜索
        searchKey(year, quarter) {
            if (!year) {
                year = 0;
            }
            else {
                year = year.getFullYear();
            }
            if (!quarter) {
                quarter = 0
            }
            let pageindex = 1;
            let pagesize = 10;
            let searchkey = {
                year: year,
                quarter: quarter
            };
            let orderby = {};
            this.getPaperListFilter(pageindex, pagesize, searchkey, {});
        },
        //添加试卷
        addPaper() {
            this.$router.push({ path: 'addPaper' })
        },
        //初始化试卷列表 请求
        getPaperListFilter(pageindex, pagesize, searchkey, orderby) {
            getPaperList({ pageindex, pagesize, searchkey, orderby }).then(res => {
                if (res.status == 200) {
                    this.testPaper = res.data.data;
                    this.totalPage = res.data.total;
                    if (res.data.data.length == 0) {
                        this.$message({
                            message: "没有搜索到相关数据！",
                            type: 'warning'
                        });
                    }
                }
                else {
                    this.$message({
                        message: "没此项目的数据！",
                        type: 'error'
                    });
                }
            });
        },
        handleSizeChange(val) {
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange(val) {
            this.getPaperListFilter(this.currentPage, this.pageNum, {}, {});
        },

    },
    created() {
        //初始化试卷列表
        this.getPaperListFilter(1, 10, {}, {});
    },
}
</script>

<style>
.question_list .search_main {
    margin: 20px 0;
    text-align: left
}

.question_list .page-main {
    margin: 0;
    padding: 20px 0;
}

.question_list .search_main td {
    padding-left: 10px;
}

.question_list .search_main td span {
    display: inline-block;
    width: 80px;
    text-align: center;
    line-height: 36px;
    background: #20a0ff;
    color: white;
    border-radius: 2px;
    cursor: pointer;
}

.search_main .el-select,
.search_main .el-date-editor.el-input {
    width: 100%;
}

.question_list table {
    width: 100%;
    table-layout: fixed;
    border-collapse: collapse;
}

.question_list .paper_list th,
.question_list .paper_list td {
    height: 40px;
    border: solid 1px #dadada;
    text-align: center;
    font-size: 14px;
}

.question_list .paper_list th {
    background: #f4f4f4;
    font-size: 16px;
}

.question_list .paper_list tbody tr:hover {
    background: #f6f6f6
}

.question_list .paper_list td .look {
    display: inline-block;
    /*width: 60px;*/
    /*text-align: center;*/
    /*line-height: 24px;*/
    background: #20a0ff;
    color: white;
    /*border-radius: 2px;*/
    cursor: pointer;
    margin: 0 5px;
    padding: 7px 9px;
    font-size: 12px;
    border-radius: 4px;
}

.question_list .paper_list td .deletepaper {
    background: #ff4949
}
</style>
