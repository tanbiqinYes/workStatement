import fetch from 'utils/fetch';

//个人考核结果
export function ShowResult() {
  return fetch({
    url: '/api/ExamList/ShowResult',
    method: 'get',
    
  });
}


//部门考核结果
export function ShowResultList(params) {
  return fetch({
    url: '/api/ExamList/ShowResultList',
    method: 'get',
    params
  });
}

//所有部门考核结果
export function ShowManagerList(params) {
  return fetch({
    url: '/api/ExamList/ShowManagerList',
    method: 'get',
    params
  });
}
//考评委员会
export function ManageerTable(params) {
  return fetch({
    url: '/api/ExamList/ManageerTable',
    method: 'get',
    params
  });
}

//考评委员会 - 发布
export function ChangePublish(data) {
  return fetch({
    url: 'api/ExamList/ChangePublish',
    method: 'put',
    data
  });
  //  return fetch.put('/api/ExamList/ChangePublish',params);

}


