<template>
    <el-menu class="navbar" mode="horizontal">
        <div class="logoName">
          <span></span>员工绩效考核系统
        </div>
        <el-dropdown class="avatar-container" trigger="click">
            <div class="avatar-wrapper">
                <img class="user-avatar" :src="avatar+'?imageView2/1/w/80/h/80'">
                <i class="el-icon-caret-bottom"/>
                <div class="helloName">欢迎您，{{roleName}}-{{name}}</div>
            </div>
            <el-dropdown-menu class="user-dropdown" slot="dropdown">
                <router-link  class='inlineBlock' to="/">
                    <el-dropdown-item>
                        首页
                    </el-dropdown-item>
                </router-link>
                <router-link  class='inlineBlock' to="/admin/profile">
                    <el-dropdown-item>
                        设置
                    </el-dropdown-item>
                </router-link>
                <el-dropdown-item divided><span @click="logout" style="display:block;">退出登录</span></el-dropdown-item>
            </el-dropdown-menu>
        </el-dropdown>
    </el-menu>
</template>

<script>
    import { mapGetters } from 'vuex';

    export default {
      components: {
        // Levelbar,
        // Hamburger,
        // ErrLog
      },
      data() {
        return {
          // log: errLogStore.state.errLog
        }
      },
      computed: {
        ...mapGetters([
          'sidebar',
          'name',
          'avatar',
          'roleName'
        ])
      },
      methods: {
        toggleSideBar() {
          this.$store.dispatch('ToggleSideBar')
        },
        logout() {
          this.$store.dispatch('LogOut').then(() => {
            location.reload();// 为了重新实例化vue-router对象 避免bug
          });
        }
      }
    }
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
    .navbar {
        height: 60px;
        line-height: 60px;
        border-radius: 0px !important;
        z-index:999;
        background: #fff;
        .logoName{
          color: #ffffff;
          width: 230px;
          background: #51d2b7;
          text-align: center;
          float: left;
          span{
            display: inline-block;
            height: 60px;
            background: #324157;
            width: 0px;
            float: left;
            transition: all .28s ease-out;
          }
        }
        .hamburger-container {
            line-height: 58px;
            height: 50px;
            float: left;
            padding: 0 10px;
        }
        .errLog-container {
            display: inline-block;
            position: absolute;
            right: 150px;
        }
        .avatar-container {
            height: 50px;
            display: inline-block;
            position: absolute;
            right: 50px;
            .helloName{
              position: absolute;
              right: 52px;
              display: inline-block;
              white-space: nowrap;
              top: 0px;
            }
            .avatar-wrapper {
                cursor: pointer;
                padding-top:10px;
                position: relative;
                box-sizing: border-box;
                .user-avatar {
                    width: 40px;
                    height: 40px;
                    border-radius: 40px;
                }
                .el-icon-caret-bottom {
                    position: absolute;
                    right: -20px;
                    top: 25px;
                    font-size: 12px;
                }
            }
        }
    }
</style>
<style lang="less">
  .hideSidebar .logoName span{
      width: 40px !important;
  }
</style>
