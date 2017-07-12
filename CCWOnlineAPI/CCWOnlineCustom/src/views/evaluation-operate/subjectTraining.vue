<template>
<div>
    <ul>
        <li class="subjeckList" v-for="list in lists">
            <h4>{{list.num}}、{{list.title}}</h4>
            <ul>
                <li v-for="option in list.name">
                    <input type="radio" :value="option.weight" v-model="list.picked">
                    <span>{{option.subtitle}}</span>
                </li>
            </ul>
            <span>此题得分：{{list.picked}}</span>
        </li>
    </ul>
    <span>总共得分：{{selfScore}}</span>
    <h4>自我评价: </h4>
    <textarea v-model="selfEvaluation"></textarea>
    <span>{{selfEvaluation}}</span><br>
    <div style="display: flex;justify-content: center">
        <el-button type="info" @click="total()">提交</el-button>
    </div>
</div>
</template>

<script>
export default {
    data(){
        return{
            selfScore:[],
            selfEvaluation:'',
            lists:[
                {
                    num:1,
                    title:'知识和技能',
                    picked:[],
                    name:[
                        {subtitle:'有限的知识和技能，拥有从事本专业工作的基本知识或某一领域的某些知识点，这些知识未在工作中实践过',weight:5,score:''},
                        {subtitle:'具备基础知识和必要技能，集中于本专业工作的基本知识或某一领域的某些知识点，这些知识在工作中多次实践过',weight:10,score:''},
                        {subtitle:'具备全面良好的知识和必要技能，主要领域精通，这些知识有相当了解',weight:15,score:''},
                        {subtitle:'在本专业领域具有精通、全面的知识和技能',weight:20,score:''}
                    ]
                },
                {
                    num:2,
                    title:'技能和知识',
                    picked:[],
                    name:[
                        {subtitle:'有限的知识和技能，拥有从事本专业工作的基本知识或某一领域的某些知识点，这些知识未在工作中实践过',weight:5,score:''},
                        {subtitle:'具备基础知识和必要技能，集中于本专业工作的基本知识或某一领域的某些知识点，这些知识在工作中多次实践过',weight:10,score:''},
                        {subtitle:'具备全面良好的知识和必要技能，主要领域精通，这些知识有相当了解',weight:15,score:''},
                        {subtitle:'在本专业领域具有精通、全面的知识和技能',weight:20,score:''}
                    ]
                }
            ]
        }
    },
    methods:{
        totle(){
            this.selfScore = [];
            for(let i = 0;i<this.lists.length;i++){
                this.selfScore.push(this.lists[i].picked)
            }
            this.selfScore.push(this.selfEvaluation)
        }
    }
}
</script>

<style scope>
ul,li{
    margin: 0;
    padding: 0;
}
li{
    display: block;
}
textarea{
    width: 100%;
    height: 80px;
    box-sizing: border-box;
}
</style>
