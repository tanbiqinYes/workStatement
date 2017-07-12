<template>
    <div class="app-wrapper el-row" :class="{hideSidebar:!sidebar.opened}">
        <div class="header-zone el-col el-col-24">
          <headerBar/>
        </div>
        <div class="content-zone el-col el-col-24">
            <div class="sidebar-wrapper">
                <Sidebar class="sidebar-container"/>
            </div>
            <div class="main-container">
                <Navbar/>
                <App-main/>
            </div>
        </div>
    </div>
</template>

<script>
    import { Navbar, Sidebar, AppMain, headerBar } from 'views/layout';

    export default {
      name: 'layout',
      components: {
        Navbar,
        Sidebar,
        AppMain,
        headerBar
      },
      computed: {
        sidebar() {
          return this.$store.state.app.sidebar;
        }
      }
    }
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
    @import "src/styles/mixin.scss";
    .el-menu--dark .el-menu-item, .el-menu--dark .el-submenu__title{
      color: #e4e8f1;
    }
    .app-wrapper {
        @include clearfix;
        position: absolute;
        top: 0px;
        bottom: 0px;
        width: 100%;
        &.hideSidebar {
            .sidebar-wrapper {
                transform: translate(-190px, 0);
                .sidebar-container {
                    transform: translate(182px, 0);
                }
                // &:hover {
                //     transform: translate(0, 0);
                //     .sidebar-container {
                //         transform: translate(0, 0);
                //     }
                // }
            }
            .main-container{
                margin-left: 40px;
            }
        }
        .header-zone{
          height: 60px;
          line-height: 60px;
          color: #fff;
          box-shadow: 0 1px 3px rgba(0,0,0,.1);
          box-sizing: border-box;
          -webkit-sizing: border-box;
        }
        .content-zone{
          position: absolute;
          top: 60px;
          bottom: 0px;
          overflow: hidden;
        }
        .sidebar-wrapper {
            width: 230px;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            z-index: 1001;
            overflow: hidden;
            transition: all .28s ease-out;
        }
        .sidebar-container {
            transition: all .28s ease-out;
            /*position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: -17px;
            overflow-y: scroll;*/
            overflow-y: auto;
            // padding-top: 50px;
            height: 100%;
        }
        .main-container {
            height: 100%;
            transition: all .28s ease-out;
            margin-left: 230px;
            position: relative;
        }
    }
</style>
