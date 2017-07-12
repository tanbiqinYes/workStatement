<template>
    <!--1-创意部-->
    <div class="container">
        <div class="search">
            <el-input v-model="searchMsg" placeholder="请输入内容" style="width:200px;margin-right:10px;" size="small"></el-input>
            <el-button type="primary" size="small" @click="search(searchMsg)">搜索</el-button>
        </div>
        <table border="1" bordercolor='#dfe6ec'>
            <thead>
                <tr>
                    <th v-for="head in list.tableHead">{{head}}</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="staff in list.staffs">
                    <td v-for="(option,$index) in staff" v-show="$index == 'uid' || $index == 'relation'?false:true">
                        <span v-if="$index == 'operate'">
                            <el-button type="success" size="small" v-show="option=='1'?true:false">已完成</el-button>
                            <el-button type="warning" size="small" @click="goToSubject(staff)" v-show="option=='0'?true:false">完成考评</el-button>
                            <el-button type="info" size="small" v-show="option=='2'?true:false">查看结果</el-button>
                        </span>
                        <span v-else-if="$index == 'reportOp'">
                            <el-button type="info" size="small" @click="goWorkreport(staff)">工作报告</el-button>
                        </span>
                        <span v-else>{{option}}</span>
                    </td>
                </tr>
            </tbody>
        </table>
    
        <div class="paginationW">
            <el-pagination @current-change="handleCurrentChange" :current-page="currentPage" :page-size="pageNum" layout="total, prev, pager, next, jumper" :total="totalPage">
            </el-pagination>
        </div>
    </div>
</template>

<script>
import { committeeGetStaff } from '../../api/department';
export default {
    data() {
        return {
            searchMsg: '',
             list: {
                tableHead: { name: '姓名', dep: '部门', job: '岗位', operate: '操作', reportOp: '操作' },
                staffs: ''
            },
            totalPage: 0,
            currentPage: 1,
            pageNum: 10,
            keyWord: "",
            orderby: "",
            department: '测试部',
        }
    },
    methods: {
        search(name) {
            let keyword = name;
            this.committeeGetStaffFilter(this.department, this.currentPage, this.pageNum, keyword, this.orderby)
        },
      goWorkreport(obj) {
            this.$router.push({ name: '工作报告列表',params:{name:obj.name,uid:obj.uid} });
        },
        goToSubject(obj) {
            console.log(obj);
            this.$router.push({ name: '能力修养考评', params: { uid: obj.uid } })

        },
        handleCurrentChange(val) {
            this.committeeGetStaffFilter(this.department, val, this.pageNum, "", "");
        },
        committeeGetStaffFilter(department, pagenum, pagesize, keyword, orderby) {
            committeeGetStaff({ department, pagenum, pagesize, keyword, orderby }).then(res => {
                let { data } = res;
                if (data) {
                    this.list.staffs = data.list.staffs
                    this.totalPage = data.total;
                }
                else{
                    this.$message({
                        message:'找不到相关数据',
                        type:'warning'
                    })
                }
            })
        }
    },
    created() {
        this.committeeGetStaffFilter(this.department, 0, this.pageNum, this.keyWord, this.orderby);
    }
}
</script>

<style>
.container {
    padding: 20px;
}

.search {
    display: inline-block;
    float: right;
    margin-bottom: 10px;
}

.search input {
    height: 28px;
    box-sizing: border-box;
    margin: 0;
    margin-right: 10px;
}

.search button {
    height: 28px;
}

table {
    border-collapse: collapse;
    width: 100%;
}

td,
th {
    height: 45px;
    border-color: #dfe6ec;
    font-size: 14px;
}

td {
    text-align: center;
}

th {
    background-color: #eef1f6;
}

.operateClass {
    padding: 3px 8px;
    border-radius: 5px;
    background: #199bd7;
    color: #fff;
    cursor: pointer;
}

.viweClass {
    padding: 3px 8px;
    border-radius: 5px;
    color: #000;
    cursor: pointer;
    border: 1px solid #000;
    box-sizing: border-box;
}
</style>

