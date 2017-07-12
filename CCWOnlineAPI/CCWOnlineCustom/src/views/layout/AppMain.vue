<template>
    <div class="contentScroll">
      <section class="app-main">
          <transition name="fade" mode="out-in">
              <router-view :key="key"></router-view>
          </transition>
      </section>
    </div>
</template>

<script>
    export default {
      name: 'AppMain',
      computed: {
        key() {
          return this.$route.name !== undefined
                        ? this.$route.name + +new Date()
                        : this.$route + +new Date()
        }
      }
    }
</script>

<style scoped>
.contentScroll{
    position: absolute;
    top: 0px;
    bottom: 0px;
    overflow: auto;
    left: 0;
    right: 0;
    padding: 60px 30px 0px 30px;
}
</style>
