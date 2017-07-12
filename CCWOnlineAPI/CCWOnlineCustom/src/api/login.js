import fetch from 'utils/fetch';
import qs from 'qs';

export function loginByNickname(nickname, password) {
  const data = {
    "grant_type":"password",
    "username":nickname,
    "password":password
  };
  // return fetch.post('/token',data);
    return fetch({
      url: '/token',
      method: 'post',
      // params:data,
      data:qs.stringify(data),
      headers: {'Content-Type': 'application/x-www-form-urlencoded'},
    });
}

export function logout() {
  return fetch({
    url: '/login/logout',
    method: 'post'
  });
}

export function getInfo(token) {
  return fetch({
    url: '/api/account/UserInfoAsync',
    method: 'get'
  });
}
