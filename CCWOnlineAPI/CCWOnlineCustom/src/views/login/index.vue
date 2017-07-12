<template>
    <div class="login-container">
        <el-form autoComplete="on" :model="loginForm" :rules="loginRules" ref="loginForm" label-position="left"
                 label-width="0px"
                 class="card-box login-form">
            <h3 class="title">系统登录</h3>
            <!-- <img src="../../assets/401.gif" alt=""> -->
            <el-form-item prop="nickname">
                <span class="svg-container"><wscn-icon-svg icon-class="zhanghu"/></span>
                <el-input name="nickname" type="text" v-model="loginForm.nickname" autoComplete="on"
                          placeholder="账号"></el-input>
            </el-form-item>
            <el-form-item prop="password" class="password">
                <span class="svg-container"><wscn-icon-svg icon-class="mima"/></span>
                <el-input name="password" type="password" @keyup.enter.native="handleLogin" v-model="loginForm.password" autoComplete="on" placeholder="密码"></el-input>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" style="width:100%;" :loading="loading" @click.native.prevent="handleLogin">
                    登录
                </el-button>
            </el-form-item>
            <!-- <div class='tips'>账号：staff、manager、committee、admin、<p style="text-indent:3em; margin:0px;">committeeManager</p> 密码：随便填,但大于6位</div> -->
            <!-- <router-link to="/sendpwd" class="forget-pwd">
                忘记密码?(或首次登录)
            </router-link> -->
        </el-form>
        <el-dialog title="第三方验证" :visible.sync="showDialog">
            邮箱登录成功,请选择第三方验证
            <socialSign></socialSign>
        </el-dialog>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    // import { isWscnEmail } from 'utils/validate';
    // import { getQueryObject } from 'utils';
    import socialSign from './socialsignin';

    export default {
      components: { socialSign },
      name: 'login',
      data() {
        const validateEmail = (rule, value, callback) => {
          if (value.length < 1) {
            callback(new Error('请填写账号'));
          } else {
            callback();
          }
        };
        const validatePass = (rule, value, callback) => {
          if (value.length < 6) {
            callback(new Error('密码不能小于6位'));
          } else {
            callback();
          }
        };
        return {
          loginForm: {
            nickname: '',
            password: ''
          },
          loginRules: {
            nickname: [
                { required: true, trigger: 'blur', validator: validateEmail }
            ],
            password: [
                { required: true, trigger: 'blur', validator: validatePass }
            ]
          },
          loading: false,
          showDialog: false
        }
      },
      computed: {
        ...mapGetters([
          'auth_type'
        ])
      },
      methods: {
        handleLogin() {
          var self = this;
          this.$refs.loginForm.validate(valid => {
            if (valid) {
              this.loading = true;
              this.$store.dispatch('LoginByNickname', this.loginForm).then(() => {
                // console.log(234214)
                this.loading = false;
                this.$router.push({ path: '/' });
                // this.showDialog = true;
              }).catch(err => {
                // this.$message.error(err);
                if (err.response && err.response.status == 400){
                    self.$message({
                          message: err.response.data.error_description,
                          type: 'error',
                          duration: 5 * 1000
                        });
                }
                else{
                  self.$message({
                        message: "登录失败: 未知的用户名或错误密码或服务器错误！",
                        type: 'error',
                        duration: 5 * 1000
                      });
                }
                this.loading = false;
              });
            } else {
              console.log('error submit!!');
              this.loading = false;
              return false;
            }
          });
        },
        afterQRScan() {
          // const hash = window.location.hash.slice(1);
          // const hashObj = getQueryObject(hash);
          // const originUrl = window.location.origin;
          // history.replaceState({}, '', originUrl);
          // const codeMap = {
          //   wechat: 'code',
          //   tencent: 'code'
          // };
          // const codeName = hashObj[codeMap[this.auth_type]];
          // if (!codeName) {
          //   alert('第三方登录失败');
          // } else {
          //   this.$store.dispatch('LoginByThirdparty', codeName).then(() => {
          //     this.$router.push({ path: '/' });
          //   });
          // }
        }
      },
      created() {
        // window.addEventListener('hashchange', this.afterQRScan);
      },
      destroyed() {
        // window.removeEventListener('hashchange', this.afterQRScan);
      }
    }
</script>

<style rel="stylesheet/scss" lang="scss">
    @import "src/styles/mixin.scss";
    .tips{
      font-size: 14px;
      color: #fff;
      margin-bottom: 5px;
      /*background: url(../../assets/401.gif);*/
    }
    .login-container {
        @include relative;
        height: 100vh;
        background-color: #2d3a4b;

        input:-webkit-autofill {
            -webkit-box-shadow: 0 0 0px 1000px #293444 inset !important;
            -webkit-text-fill-color: #fff !important;
        }
        input {
            background: transparent;
            border: 0px;
            -webkit-appearance: none;
            border-radius: 0px;
            padding: 12px 5px 12px 8px;
            color: #eeeeee;
            height: 47px;
        }
        .el-input {
            display: inline-block;
            height: 47px;
            width: 85%;
        }
        .svg-container {
            padding: 6px 5px 6px 15px;
            color: #889aa4;
        }

        .title {
            font-size: 26px;
            font-weight: 400;
            color: #eeeeee;
            margin: 0px auto 40px auto;
            text-align: center;
            font-weight: bold;
        }

        .login-form {
            position: absolute;
            left: 0;
            right: 0;
            width: 400px;
            padding: 35px 35px 15px 35px;
            margin: 120px auto;
        }

        .el-form-item {
            border: 1px solid rgba(255, 255, 255, 0.1);
            background: rgba(0, 0, 0, 0.1);
            border-radius: 5px;
            color: #454545;
        }

        .forget-pwd {
            color: #fff;
        }

        .wscn-icon{
          width: 1.5em;
          height: 1.5em;
          vertical-align: -0.4em;
        }

        .password .wscn-icon{
          width: 1.4em;
          height: 1.4em;
          vertical-align: -0.4em;
        }
    }

</style>
