<template>
    <div class="work_index">
        <div>
            <span>时间：</span>
            <el-date-picker v-model="value1" type="month" placeholder="选择开始月份">
            </el-date-picker>
            <span>—</span>
            <el-date-picker v-model="value2" type="month" placeholder="选择结束月份">
            </el-date-picker>
            <span class="search" @click="searchDate(value1,value2)">搜索</span>
        </div>
        <ul class="work_report_list">
            <li v-for="(list,index) in dateList" :key="list.id">{{list.time}}工作报告，
                <a href="javascript:;void(0)" @click="lookOver(index)">点击查看&gt;&gt;</a>
            </li>
        </ul>
    </div>
</template>

<script>
import { getPersonList } from '../../api/workReport.js';
export default {
    name: 'work_index',
    data() {
        return {
            value1: '',
            value2: '',
            dateList: ''
            //  [
            //     {
            //         time: '2017年4月',
            //         id: ''
            //     },
            //     {
            //         time: '2017年5月',
            //         id: ''
            //     },
            //     {
            //         time: '2017年6月',
            //         id: ''
            //     }
            // ]
        }
    },
    methods: {
        lookOver(index) {
            let reportid = this.dateList[index].Id;
            this.$router.push({ name: '工作报告详情', params:{id:reportid} })
        },
        searchDate(value1, value2) {
            if(!value1){
                this.$message('请选择开始时间')
                return fasle
            }
             if(!value2){
                this.$message('请选择结束时间')
                return fasle
            }
            let startYear = value1.getFullYear();
            let endYear = value2.getFullYear();
            let startMonth = value1.getMonth() + 1;
            let endMonth = value2.getMonth() + 1;
            getPersonList({ startYear, startMonth, endYear, endMonth }).then(res => {
                let { data } = res;
                if (res.status==200) { 
                    this.dateList = res.data.dateList;
                    console.log(res);
                }
                else {
                    this.$message({
                        message: "没有搜索到相关数据！",
                        type: 'error'
                    });
                }
            })
                .catch(err => {
                    this.$message({
                        message: "没有搜索到相关数据！",
                        type: 'error'
                    })
                });

        },
        getPersonListState({ startYear, startMonth, endYear, endMonth }) {
            getPersonList({ startYear, startMonth, endYear, endMonth }).then(res => {
                console.log(res)
                if (res.status == 200) {
                    this.dateList=res.data;
                }
                else {
                    this.$message({
                        message: "没有相关数据！",
                        type: 'error'
                    });
                }
            })
                .catch(err => {
                    this.$message({
                        message: "没有相关数据！",
                        type: 'error'
                    });
                })
                ;
        }
    },
    created() {
        let date = new Date();
        let endYear = date.getFullYear();
        let endMonth = date.getMonth() + 1;
        let startYear = endYear;
        let startMonth = 1;
        this.getPersonListState({ startYear: 0, endYear: 0, startMonth: 0, endMonth: 0 })
    }
}
</script>

<style>
.work_index .search {
    display: inline-block;
    width: 80px;
    text-align: center;
    vertical-align: top;
    line-height: 36px;
    background: #20a0ff;
    color: white;
    margin: 0 20px;
    border-radius: 3px;
    cursor: pointer;
}

.work_report_list {
    margin: 20px 0;
    padding: 0;
}

.work_report_list li {
    line-height: 35px;
    list-style-type: none;
}

.work_report_list li a {
    display: inline-block;
    vertical-align: top;
    line-height: 35px;
    color: #20a0ff;
}
</style>
