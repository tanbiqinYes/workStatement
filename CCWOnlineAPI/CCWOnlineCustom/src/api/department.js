import fetch from 'utils/fetch';


// 部门主管点击部门主管
export function managerGetStaff(params) {
//   return fetch.get({url:'/api/home/DepAccount',params});
  return fetch({
    url: '/api/home/DepAccount',
    method: 'get',
    params
  });
}

// 委员会点击部门
export function committeeGetStaff(params) {
    return fetch({
    url: '/api/home/TopAccount',
    method: 'get',
    params
  });
}


// 部门主管和委员会点击todo进入列表
export function listAccount(params){
    return fetch({
    url: '/api/Home/ShelfAccount',
    method: 'get',
    params
  });
}