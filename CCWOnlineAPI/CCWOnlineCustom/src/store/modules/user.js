import { loginByNickname, logout, getInfo } from 'api/login';
import Cookies from 'js-cookie';

const user = {
  state: {
    user: '',
    status: '',
    userName: '',
    code: '',
    roleName: '',
    uid: undefined,
    auth_type: '',
    access_token: Cookies.get('Admin-Token'),
    name: '',
    avatar: '',
    introduction: '',
    roles: [],
    setting: {
      articlePlatform: []
    }
  },

  mutations: {
    SET_AUTH_TYPE: (state, type) => {
      state.auth_type = type;
    },
    SET_CODE: (state, code) => {
      state.code = code;
    },
    SET_TOKEN: (state, access_token) => {
      state.access_token = access_token;
    },
    SET_UID: (state, uid) => {
      state.uid = uid;
    },
    SET_NICKNAME: (state, userName) => {
      state.userName = userName;
    },
    SET_INTRODUCTION: (state, introduction) => {
      state.introduction = introduction;
    },
    SET_SETTING: (state, setting) => {
      state.setting = setting;
    },
    SET_STATUS: (state, status) => {
      state.status = status;
    },
    SET_NAME: (state, name) => {
      state.name = name;
    },
    SET_ROLENAME: (state, roleName) => {
      state.roleName = roleName;
    },
    SET_AVATAR: (state, avatar) => {
      state.avatar = avatar;
    },
    SET_ROLES: (state, roles) => {
      state.roles = roles;
    },
    LOGIN_SUCCESS: () => {
      console.log('login success')
    },
    LOGOUT_USER: state => {
      state.user = '';
    }
  },

  actions: {
    // 邮箱登录
    LoginByNickname({ commit }, userInfo) {
      const userName = userInfo.nickname.trim();
      return new Promise((resolve, reject) => {
        loginByNickname(userName, userInfo.password).then(response => {
          // console.log(response.data)
          const data = response.data;
          Cookies.set('Admin-Token', data.access_token);
          commit('SET_TOKEN', data.access_token);
          commit('SET_NICKNAME', userName);
          resolve();
        }).catch(error => {
          reject(error);
        });
      });
    },


    // 获取用户信息
    GetInfo({ commit, state }) {
      return new Promise((resolve, reject) => {
        getInfo(state.access_token).then(response => {
          const data = response.data;
          commit('SET_ROLES', [data.role]);
          commit('SET_NAME', data.username);
          // commit('SET_ROLENAME', data.roleName);
          commit('SET_AVATAR', data.userimage != "" ? data.userimage : "https://gitlab.com/uploads/system/user/avatar/1213140/avatar.png");
          // commit('SET_UID', data.uid);
          // commit('SET_INTRODUCTION', data.introduction);
          resolve(response);
        }).catch(error => {
          reject(error);
        });
      });
    },

    // 第三方验证登录
    LoginByThirdparty({ commit, state }, code) {
      return new Promise((resolve, reject) => {
        commit('SET_CODE', code);
        loginByThirdparty(state.status, state.email, state.code, state.auth_type).then(response => {
          commit('SET_TOKEN', response.data.token);
          Cookies.set('Admin-Token', response.data.token);
          resolve();
        }).catch(error => {
          reject(error);
        });
      });
    },


    // 登出
    LogOut({ commit, state }) {
      return new Promise((resolve, reject) => {
        // logout(state.token).then(() => {
        //   commit('SET_TOKEN', '');
        //   commit('SET_ROLES', []);
        //   Cookies.remove('Admin-Token');
        //   resolve();
        // }).catch(error => {
        //   reject(error);
        // });

          commit('SET_TOKEN', '');
          commit('SET_ROLES', []);
          Cookies.remove('Admin-Token');
          resolve();
      });
    },

    // 前端 登出
    FedLogOut({ commit }) {
      return new Promise(resolve => {
        commit('SET_TOKEN', '');
        Cookies.remove('Admin-Token');
        resolve();
      });
    },

    // 动态修改权限
    ChangeRole({ commit }, role) {
      return new Promise(resolve => {
        commit('SET_ROLES', [role]);
        commit('SET_TOKEN', role);
        Cookies.set('Admin-Token', role);
        resolve();
      })
    }
  }
};

export default user;
