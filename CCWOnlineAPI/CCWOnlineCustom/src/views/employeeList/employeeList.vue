<template>
<div>
    <table border="1" bordercolor="#dfe6ec">
        <thead>
            <tr>
                <th v-for="head in list.tableHead">{{head}}</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="staff in list.staffs">
                <td v-for="(option,$index) in staff" v-show="$index == 'uid' || $index =='relation' || $index=='reportOp'?false:true">  
                    <span v-if="$index == 'operate'">
                        <el-button type="success" size="small" v-show="option=='1'?true:false">已完成</el-button>
                        <el-button type="warning" size="small" @click="goToSubject(staff)" v-show="option=='0'?true:false">完成考评</el-button>
                        <el-button type="info" size="small" v-show="option=='2'?true:false">查看结果</el-button>
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
import {managerGetStaff} from '../../api/department'
import {listAccount} from '../../api/department'
export default {
    data(){
        return{
            pageNum:10,
            list:{
                tableHead:{name:'姓名',dep:'部门',job:'岗位',operate:'操作'},
                staffs:[]
            }
        }
    },
    methods:{
        goToSubject(obj){
            console.log(obj);
            let role = this.$store.getters.roles[0];
            if(role == "考评委员会"){
                this.$router.push({name:'能力修养考评',params:{uid:obj.uid}})
            }else if(role == "部门主管"){
                this.$router.push({name:'能力修养考评',params:{uid:obj.uid}})
            }if(role == "组长"){
                this.$router.push({name:'能力修养考评',params:{uid:obj.uid}})
            }
            
        },
        handleCurrentChange(val) {
            this.managerGetStaffFilter(this.pageNum, val,  "", "");
        },
        // 作为主管
        managerGetStaffFilter(pagesize,pagenum,keyword,orderby){
            pagenum = pagenum ? pagenum : 1;
            pagesize = pagesize ? pagesize : 10;
            keyword = keyword ? keyword : "";
            orderby = orderby ? orderby : "";
            listAccount({pagesize,pagenum,keyword,orderby}).then(res =>{
                let {data} = res;
                console.log(data)
                this.list.staffs = data.list.staffs
                this.totalPage = data.total;
            })
        },
        // 作为委员会
        listAccountFilter(pagesize,pagenum,keyword,orderby){
            pagenum = pagenum ? pagenum : 1;
            pagesize = pagesize ? pagesize : 10;
            keyword = keyword ? keyword : "";
            orderby = orderby ? orderby : "";
            listAccount({pagesize,pagenum,keyword,orderby}).then(res =>{
                let {data} = res;
                console.log(data)
                this.list.staffs = data.list.staffs
                this.totalPage = data.total;
            })
        }
        // handleCurrentChange(val) {
        //     console.log(`当前页: ${val}`);
        //     this.getUserListFilter(val, this.pageNum, "", "");
        // },
    },
    created(){
        let role = this.$store.getters.roles[0];
        if(role == '部门主管'){
            this.managerGetStaffFilter(this.pageSize,0,"","");
        }else{
            this.listAccountFilter(this.pageSize,0,"","");
        }
        
    }
}
</script>

<style scoped>
table{
    border-collapse:collapse;
    width: 100%; 
    font-size: 14px;
}
tbody,thead{
    border-color: #dfe6ec;
}
td,th{
    text-align: center;
    height: 45px;
    border-color: #dfe6ec;
}
td{
    text-align: center;
}
th{
    background-color: #eef1f6;
}
.el-button+.el-button{
    margin: 0
}

</style>
