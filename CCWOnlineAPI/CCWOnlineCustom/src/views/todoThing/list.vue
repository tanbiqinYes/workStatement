<template>
    <div class="listContent">
        <p class="listLeft">{{todo.title}},<span href="#">{{clickAlert}}</span></p>
        <p class="listRight">{{todo.time}}</p>
    </div>
</template>

<script>
export default {
    props:{
        todo:{
            type:Object
        }
    },
    data(){
        
    },
    
}
</script>

<style scoped>
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
