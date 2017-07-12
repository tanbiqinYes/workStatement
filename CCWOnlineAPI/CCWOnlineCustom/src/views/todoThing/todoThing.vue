<template>
<div class="todo">
    <h3 class="todolistsTitle">待办事项</h3>
    <ul class="todolistsContent">
        <li v-for="(todo,$index) in lists" class="todolist">

        <p class="listLeft">{{todo.title}},
            <el-button type="text" v-show="todo.isfinish=='1'" style="font-size:16px;" @click="showMS($index,todo)">点击查看</el-button>
            <el-button type="text" v-show="todo.isfinish=='0'" style="font-size:16px;" @click="showMS($index,todo)">点击完成</el-button>
        </p>
        <p class="listRight">{{todo.time}}</p>

        </li>
    </ul>
    <p v-show="showTodo">无待办事项</p>
</div>
</template>

<script>
import atodolist from "./list.vue";
import {getTodoList} from '../../api/todoList';

export default {
    data(){
        return{
            showTodo:false,
            lists:[

            ]
        }
    },
    components:{
        atodolist
    },
    created(){
        this.getTodoListFilter();
    },
    methods:{
        // 判断类型和完成情况跳页面
        showMS(index,todo){
            let role = this.$store.getters.roles[0];
            if(role == '普通员工'){
                if(todo.isfinish == 0){
                    if(todo.type == 1 || todo.type == 2){
                        // 能力
                        // let uid = this.$store.getters.uid;
                        this.$router.push({name:'能力修养考评',params:{uid:todo.userid}})
                    }else if(todo.type == 3 || todo.type == 4){
                        // 同事能力
                        this.$router.push({name:'能力修养考评',params:{uid:todo.userid}})
                    }else if(todo.type == 5){
                        // 完成工作报告
                       this.$router.push({name:'工作报告详情',params:{id:0}})
                    }else{
                        // 查看考核结果
                        this.$router.push({name:'考核结果'})
                    }
                }
            }
            if(role == '部门主管'){
                if(todo.isfinish == 0){
                    if(todo.type == 1 || todo.type == 2){
                        // 能力
                        // let uid = this.$store.getters.uid;
                        this.$router.push({name:'能力修养考评',params:{uid:todo.userid}})
                    }else if(todo.type == 3 || todo.type == 4){
                        // 同事能力
                        this.$router.push({name:'待办事项员工列表页'})
                    }else if(todo.type == 5){
                        this.$router.push({name:'工作报告详情',params:{id:0}})
                    }
                }
            }
            if(role == '组长'){
                if(todo.isfinish == 0){
                    if(todo.type == 1 || todo.type == 2){
                        // 能力
                        // let uid = this.$store.getters.uid;
                        this.$router.push({name:'能力修养考评',params:{uid:todo.userid}})
                    }else if(todo.type == 3 || todo.type == 4){
                        // 同事能力
                        this.$router.push({name:'待办事项员工列表页'})
                    }else if(todo.type == 5){
                        this.$router.push({name:'工作报告详情',params:{id:0}})
                    }
                }
            }
            if(role == '考评委员会'){
                if(todo.isfinish == 0){
                    if(todo.type == 1 || todo.type == 2){
                        // 能力
                        // let uid = this.$store.getters.uid;
                        this.$router.push({name:'能力修养考评',params:{uid:todo.userid}})
                    }else if(todo.type == 3 || todo.type == 4){
                        // 同事能力
                        this.$router.push({name:'待办事项员工列表页'})
                    }
                }
            }
            if(role == '管理员'){

                    if(todo.type == 1){
                        this.$router.push({name:'考评关系'})
                    }else if(todo.type == 2){
                        this.$router.push({name:'任务管理'})
                    }else{
                        this.$router.push({name:'考核权重'})
                    }

            }
        },

        getTodoListFilter(){
            getTodoList().then(res =>{
                let {data} = res;
                console.log(res);
                this.lists = data;
                if(this.lists.length == 0){
                    this.showTodo = true;
                }
            })
        }
    }
}
</script>

<style scoped>
.todolistsTitle{
    color: #a57b7c;
}
.todolistsContent{
    margin: 0;
    padding: 0;
}
.todolistsContent .todolist{
    display: flex;
    align-items: center;
    justify-content: space-between;
}
p{
    margin: 0;
    padding: 0;
}
.listContent{
    display: flex;
    justify-content: space-between;
}
.listLeft,.listRight{
    display: inline-block;
}
.listLeft span{
    color: #0e6fcd;
}
</style>
