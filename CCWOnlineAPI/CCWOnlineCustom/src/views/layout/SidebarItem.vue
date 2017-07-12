<template>
    <div>
        <template v-for="item in routes">
            <router-link v-if="!item.hidden&&item.noDropdown&&item.children.length>0" :to="item.path+'/'+item.children[0].path" v-show="item.path=='/evaluation'?false:true" @click.native="flushCom($event,item.path+'/'+item.children[0].path)">
                <el-menu-item :index="item.path+'/'+item.children[0].path">
                    <wscn-icon-svg v-if='item.icon' :icon-class="item.icon" /> {{item.children[0].name}}
                </el-menu-item>
            </router-link>
            <el-submenu :index="item.name" v-if="!item.noDropdown&&!item.hidden">
                <template slot="title">
                    <wscn-icon-svg v-if='item.icon' :icon-class="item.icon" /> {{item.name}}
                </template>
                <template v-for="child in item.children" v-if='!child.hidden'>
                    <sidebar-item class='menu-indent' v-if='child.children&&child.children.length>0' :routes='[child]'> </sidebar-item>
                    <router-link :key="child.path" v-else class="menu-indent" :to="item.path+'/'+child.path" @click.native="flushCom($event,item.path+'/'+child.path)">
                        <el-menu-item :index="item.path+'/'+child.path">
                            <wscn-icon-svg v-if='child.icon' :icon-class="child.icon" />{{child.name}}
                        </el-menu-item>
                    </router-link>
                </template>
            </el-submenu>
        </template>
    </div>
</template>

<script>

    export default {
      name: 'SidebarItem',
      data(){
        return{
          fromMenuPath:this.$route.path
        }
      },
      props: {
        routes: {
          type: Array
        }
      },
      methods:{
          show(a){
              console.log(a);
          },

          flushCom(event,topath){
            event.preventDefault();
            event.stopPropagation();
            // 菜单强制刷新
            // console.log(this.fromMenuPath)
            // console.log(this.$route)
            // console.log(this.$route.path)
            if (this.fromMenuPath == topath){
              this.$router.push({ path: topath, query: { "_t": Date.parse(new Date()) }})
            }
            this.fromMenuPath = topath;
            return false;
          }
      }
    }
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
    .wscn-icon {
        margin-right: 10px;
    }
    .hideSidebar .menu-indent{
        display: block;
        text-indent: 10px;
    }
</style>

<style>
.el-submenu .el-menu{
  transition: all .28s ease-out;
      margin-left: 0px;
}
.el-submenu .el-menu .el-menu-item{
      text-indent: 10px;
}
.hideSidebar .el-submenu .el-menu{
    margin-left: -30px;
}
.hideSidebar .menu-indent{
  text-indent: 0px !important;
}
</style>
