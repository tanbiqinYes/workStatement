import fetch from 'utils/fetch';

//返回符合条件的员工列表:pagenum pagesize keyword orderby
export function getUserList(params) {
  return fetch({
    url: '/api/account/UserList',
    method: 'get',
    params
  });
}

//添加用户，返回json数据格式:Email,DominoName,Role,Work,Depart,UserName
export function Register(params) {
  return fetch.post('/api/account/Register',params);
}

//删除用户信息，更新用户状态:UserId,Role,Email,Depart,Work
export function DelUser(params) {
    return fetch({
      url: '/api/account/DelUser',
      method: 'delete',
      params
    });
}

//提交用户修改:userid,DominoName
export function EditUser(params) {
  return fetch.put('/api/account/EditUser',params);
}
