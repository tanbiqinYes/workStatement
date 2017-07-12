import fetch from 'utils/fetch';

// 获取用户列表
export function getUserRelation(params) {
  return fetch({
    url: '/api/UserRelation/UserList',
    method: 'get',
    params
  });
}

// 获取已选择数组choose
export function getUserChoosed(params) {
  return fetch({
    url: '/api/UserRelation/UserInfo',
    method: 'get',
    params
  });
}

// 提交接口submit
export function relationSubmit(params) {
  return fetch.post('/api/UserRelation/CreateRelation',params);
}

// 获取上级数组
export function getUserManager(params) {
  return fetch({
    url: '/api/UserRelation/AllManage',
    method: 'get',
    params
  });
}

// 获取同事数组
export function getUserWorkmate(params) {
  return fetch({
    url: '/api/UserRelation/AllCollege',
    method: 'get',
    params
  });
}