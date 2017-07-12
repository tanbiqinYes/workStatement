import { param2Obj } from 'utils';

const userMap = {
  admin: {
    role: '管理员',
    access_token: 'admin',
    introduction: '管理员',
    roleName: '管理员',
    avatar: 'https://gitlab.com/uploads/system/user/avatar/1213140/avatar.png',
    username: '蓉儿',
    userimage: 'https://gitlab.com/uploads/system/user/avatar/1213140/avatar.png',
    uid: '001'
  },
  committee: {
    role: ['committee'],
    access_token: 'committee',
    introduction: '考评委员会',
    roleName: '考评委员会',
    avatar: 'https://gitlab.com/uploads/system/user/avatar/1213140/avatar.png',
    userName: '荆轲',
    uid: '002'

  },
  manager: {
    role: ['manager'],
    access_token: 'manager',
    introduction: '部门主管',
    roleName: '部门主管',
    avatar: 'https://gitlab.com/uploads/system/user/avatar/1213140/avatar.png',
    userName: '张三',
    uid: '003'

  },
  staff: {
    role: ['staff'],
    access_token: 'staff',
    introduction: '员工',
    roleName: '员工',
    avatar: 'https://gitlab.com/uploads/system/user/avatar/1213140/avatar.pngg',
    userName: '李四',
    uid: '004'
  },
  committeeManager: {
    role: ['committeeManager'],
    access_token: 'committeeManager',
    introduction: '考评委员会兼部门主管',
    roleName: '考评委员会兼部门主管',
    avatar: 'https://gitlab.com/uploads/system/user/avatar/1213140/avatar.png',
    userName: '王二',
    uid: '005'
  }
}

export default {
  loginbynickname: config => {
    const str = config.body;

    var reg = new RegExp("(^|&)username=([^&]*)(&|$)", "i");
    var r = str.match(reg);
    console.log(r[2])
      if (r != null) {
    console.log(userMap[r[2]])
          return userMap[r[2]];
      }
      else return userMap["admin"];
  },
  getInfo: config => {
    console.log(userMap["admin"])
     return userMap["admin"];
    // const { token } = param2Obj(config.url);
    // if (userMap[token]) {
    //   return userMap[token];
    // } else {
    //   return Promise.reject('a');
    // }
  },
  logout: () => 'success'
};
