<template>
    <div class="evaluation_details" v-if="publish">
        <h2>{{title}}</h2>
        <p>
            <span>打印</span>
        </p>
        <div class="professional_ethics">
            <h3>{{topMessage.title}}</h3>
            <table>
                <tr>
                    <th v-for="title in topMessage.thead">{{title}}</th>
                </tr>
                <tr v-for="toplist in topMessage.tbody">
                    <td>{{toplist.name}}</td>
                    <td v-for="score in toplist.score">{{score}}</td>
                </tr>
                <tr class="colorStyle">
                    <td>{{topMessage.totalname}}</td>
                    <td :colspan="topMessage.thead.length-1">{{topMessage.totalscore}}</td>
                </tr>
            </table>
        </div>
        <div class="professional_ability">
            <h3>{{middleMessage.title}}</h3>
            <table>
                <tr>
                    <th v-for="item in middleMessage.thead">{{item}}</th>
                </tr>
                <tr v-for="(skill,index) in middleMessage.ability">
                    <td v-if="index==0" :rowspan="middleMessage.ability.length">{{middleMessage.abilityScore.title}}</td>
                    <td>{{skill.name}}</td>
                    <td v-for="skillscore in skill.score">{{skillscore}}</td>
                </tr>
                <tr>
                    <td colspan="2">合计</td>
                    <td v-for="countscore in middleMessage.abilityScore.score">{{countscore}}</td>
                </tr>
                <tr>
                    <td colspan="2">{{middleMessage.abilityScore.name}}</td>
                    <td :colspan="middleMessage.abilityScore.score.length">{{middleMessage.abilityScore.totalscore}}</td>
                </tr>
                <tbody v-if="middleMessage.manage.length>0">
                <tr v-for="(manage,dex) in middleMessage.manage" >
                    <td v-if="dex==0" :rowspan="middleMessage.manage.length">{{middleMessage.manageScore.title}}</td>
                    <td>{{manage.name}}</td>
                    <td v-for="managescore in manage.score">{{managescore}}</td>
                </tr>
                <tr>
                    <td colspan="2">合计</td>
                    <td v-for="countscore2 in middleMessage.manageScore.score">{{countscore2}}</td>
                </tr>
                <tr>
                    <td colspan="2">{{middleMessage.manageScore.name}}</td>
                    <td :colspan="middleMessage.manageScore.score.length">{{middleMessage.manageScore.totalscore}}</td>
                </tr>
                  </tbody>
                <tr class="colorStyle">
                    <td colspan="2">职业能力考核得分</td>
                    <td :colspan="middleMessage.abilityScore.score.length">{{middleMessage.totalscore}}</td>
                </tr>
            </table>
        </div>
        <div class="professional_info">
            <table>
                <tr class="colorStyle">
                    <td colspan="2">综合职业考核得分</td>
                    <td :colspan="middleMessage.abilityScore.score.length">{{bottomMessage.score}}</td>
                </tr>
                <tr class="colorStyle">
                    <td colspan="2">综合评定等级</td>
                    <td :colspan="middleMessage.abilityScore.score.length">{{bottomMessage.level}}</td>
                </tr>
                <tr class="colorStyle">
                    <td colspan="2">考核系数</td>
                    <td :colspan="middleMessage.abilityScore.score.length">{{bottomMessage.ratio}}</td>
                </tr>
            </table>
        </div>
    
    </div>
</template>

<script>
import { ShowResult } from "../../api/departmentResult.js"
export default {
    name: 'evaluation_details',
    data() {
        return {
            title: '季度考核汇总表',
            publish:false,
            topMessage: {},
            // {
            //     title: '职业修养季度考核',
            //     totalscore: 24,
            //     totalname: '职业修养得分',
            //     thead: ['', '自评', '直接上级', '同事1', '同事2', '考评委员会'],
            //     tbody: [
            //         {
            //             name: '爱国敬业',
            //             score: [1, 2, 3, 4, 5]
            //         },
            //         {
            //             name: 'ddd业',
            //             score: [1, 2, 3, 4, 5]
            //         },
            //         {
            //             name: '合计',
            //             score: [8, 8, 8, 8, 8]
            //         },
            //     ],
            // },
            middleMessage:{},
            //  {
            //     title: '季度职业能力考核',
            //     totalscore: 40,
            //     thead: ['', '', '自评', '直接上级', '同事1', '同事2', '考评委员会'],
            //     abilityScore: {
            //         name: '专业知识技能得分',
            //         title: '专业知识技能(50分)',
            //         totalscore: 20,
            //         score: [5, 5, 5, 5, 5]
            //     },
            //     manageScore: {
            //         name: '管理能力得分',
            //         title: '管理能力(50分)',
            //         totalscore: 20,
            //         score: [5, 5, 5, 5, 5]
            //     },
            //     ability: [
            //         {

            //             name: '知识和技能',
            //             score: [1, 1, 1, 1, 1]
            //         },
            //         {
            //             name: '工作经验',
            //             score: [2, 2, 2, 2, 2]
            //         }
            //     ],
            //     manage: [
            //         {
            //             name: '管理能力',
            //             score: [3, 3, 3, 3, 3]
            //         },
            //         {
            //             name: '管理经验',
            //             score: [4, 4, 4, 4, 4]
            //         }
            //     ]
            // },
            bottomMessage: {},
            // {
            //     score: 10,
            //     level: '不合格E',
            //     ratio: 0.2
            // }
        }

    },
    methods: {
          getShowResult() {
            ShowResult().then(res => {
                console.log(res.data)
                if (res.status == 200) {
                    this.publish=true;
                    this.topMessage = res.data.topMessage;
                    this.middleMessage = res.data.middleMessage;
                    this.bottomMessage = res.data.bottomMessage;
                }
                else {
                    this.$message({
                        message: "考核结果未公布，请耐心等待！",
                        type: 'warning'
                    });
                }
            })
                .catch(err => {
                    this.$message({
                        message: "考核结果未公布，请耐心等待！",
                        type: 'warning'
                    });
                });
        }
    },
    created() {
        this.getShowResult()
    }
}
</script>

<style scoped>
.evaluation_details {
    background: white;
}

.evaluation_details p {
    margin: 20px 0;
    text-align: right;
    padding: 0;
}

.evaluation_details p span {
    display: inline-block;
    line-height: 35px;
    width: 80px;
    background: #20a0ff;
    color: white;
    text-align: center;
    vertical-align: top;
    border-radius: 3px;
    margin: 0 20px;
    cursor: pointer;
}

.evaluation_details h2 {
    margin: 0 0 20px 0;
    padding: 0;
    text-align: center;
}

.evaluation_details h3 {
    text-align: center;
    background: #f4f4f4;
    margin: 0;
    padding: 20px;
}

.evaluation_details table {
    width: 100%;
    table-layout: fixed;
    border-collapse: collapse;
    text-align: center;
    font-size: 14px;
}

.evaluation_details .professional_info table {
    margin-top: 30px;
}

.evaluation_details table .colorStyle {
    background: #20a0ff;
    color: white;
}

.evaluation_details table td,
.evaluation_details table th {
    height: 40px;
    border: solid #dadada 1px;
    box-sizing: border-box;
    padding: 0 10px;
}
.evaluation_details .professional_info{
    margin-bottom: 30px;
}
</style>
