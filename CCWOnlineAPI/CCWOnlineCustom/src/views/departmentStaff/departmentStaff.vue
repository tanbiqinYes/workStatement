<template>
<div class="tableCont">
    <table border="1" bordercolor="#dfe6ec">
        <thead>
            <tr>
                <th v-for="head in list.tableHead">{{head}}</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="staff in list.staffs">
                <td v-for="(option,$index) in staff" v-show="$index == 'uid' || $index =='relation'?false:true">  
                    <span v-if="$index == 'operate'">
                        <el-button type="success" size="small" v-show="option=='1'?true:false">已完成</el-button>
                        <el-button type="danger" size="small" @click="goToSubject(staff)" v-show="option=='0' && staff.relation=='1'?true:false">完成考评</el-button>
                        <el-button type="warning" size="small" @click="goToSubject(staff)" v-show="option=='0' && staff.relation=='0'?true:false">查看</el-button>
                        <el-button type="info" size="small" v-show="option=='2'?true:false" @click="goToresult(staff.uid)">查看结果</el-button>
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
      <el-pagination @current-change="handleCurrentChange" :current-page="currentPage" :page-size="pageSize" layout="total, prev, pager, next, jumper" :total="totalPage">
      </el-pagination>
    </div>
</div>
</template>

<script>
import {managerGetStaff} from '../../api/department'
export default {
    data(){
        return{
            list:{
                tableHead:{name:'姓名',dep:'部门',job:'岗位',operate:'操作',reportOp:'操作'},
                "staffs": []
            },
            totalPage:0,
            currentPage: 1,
            pageSize: 10,
            keyWord:""
        }
    },
    created(){
        this.managerGetStaffFilter(this.pageSize,0,"","");
    },
    methods:{
        goToresult(userid){
            this.$router.push({path:'/evaluation-result/detail',query:{userid:userid}})
        },
        goWorkreport(obj){
            this.$router.push({name: '工作报告列表',params:{name:obj.name,uid:obj.uid}});
        },
        goToSubject(obj){
            console.log(obj);   
            this.$router.push({name:'能力修养考评',params:{uid:obj.uid}})          
        },

        handleCurrentChange(val) {
            console.log(`当前页: ${val}`);
            this.managerGetStaffFilter(this.pageSize,val, "", "");
        },

        // 接口函数 int pagesize(每页数量), int pagenum(页码), string keyword(搜索), string orderby(排序)
        managerGetStaffFilter(pagesize,pagenum,keyword,orderby){
            pagenum = pagenum ? pagenum : 1;
            pagesize = pagesize ? pagesize : 10;
            keyword = keyword ? keyword : "";
            orderby = orderby ? orderby : "";
            managerGetStaff({pagesize,pagenum,keyword,orderby}).then(res =>{
                let {data} = res;
                console.log(data)
                this.list.staffs = data.list.staffs
                this.totalPage = data.total;
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
.operateClass{
    padding: 3px 8px;
    border-radius: 5px;
    background: #199bd7;
    color: #fff;
    cursor: pointer;

}
.viweClass{
    padding: 3px 8px;
    border-radius: 5px;
    color: #000;
    cursor: pointer;
    border: 1px solid #000;
    box-sizing: border-box;
}
.el-button+.el-button{
    margin: 0
}
</style>
